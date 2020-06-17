namespace RunCoverletReport
{
    using System;
    using System.ComponentModel.Design;
    using System.Diagnostics;
    using System.IO;
    using EnvDTE;
    using EnvDTE80;
    using Microsoft.VisualStudio.Shell;
    using Microsoft.VisualStudio.Shell.Interop;
    using RunCoverletReport.CoverageResults;
    using Task = System.Threading.Tasks.Task;

    /// <summary>
    /// Command handler.
    /// </summary>
    internal sealed class ReportCoverageCommand
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0100;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("0e49fe34-81c1-41aa-a08f-40a829edde58");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly AsyncPackage package;

        /// <summary>
        /// Prevents a default instance of the <see cref="ReportCoverageCommand"/> class from being created.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        /// <param name="commandService">Command service to add command to, not null.</param>
        private ReportCoverageCommand(AsyncPackage package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new MenuCommand(this.Execute, menuCommandID);
            commandService.AddCommand(menuItem);

            CoverageResultsProvider.Initialise(package as RunCoverletReportPackage);
        }

        /// <summary>
        /// Gets the instance of the command..
        /// </summary>
        public static ReportCoverageCommand Instance { get; private set; }

        /// <summary>
        /// Gets the service provider from the owner package..
        /// </summary>
        private Microsoft.VisualStudio.Shell.IAsyncServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        public static async Task InitializeAsync(AsyncPackage package)
        {
            // Switch to the main thread - the call to AddCommand in ReportCoverageCommand's constructor requires
            // the UI thread.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            var commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;

            Instance = new ReportCoverageCommand(package, commandService);
        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void Execute(object sender, EventArgs e)
        {
            var dte = (DTE2)this.ServiceProvider.GetServiceAsync(typeof(DTE)).Result;
            var webBrowserSvc = (IVsWebBrowsingService)this.ServiceProvider.GetServiceAsync(typeof(IVsWebBrowsingService)).Result;

            this.RunCoverage(dte, webBrowserSvc);
        }

        /// <summary>
        /// The OpenReport.
        /// </summary>
        /// <param name="webBrowserSvc">The webBrowserSvc<see cref="IVsWebBrowsingService"/>.</param>
        /// <param name="report">The report<see cref="string"/>.</param>
        private void OpenReport(IVsWebBrowsingService webBrowserSvc, string report)
        {
            // ThreadHelper.ThrowIfNotOnUIThread();
            try
            {
                ThreadHelper.Generic.BeginInvoke(() => webBrowserSvc.Navigate(report, 0, out _));
            }
            catch { }
        }

        /// <summary>
        /// The ParseTestResults.
        /// </summary>
        /// <param name="testOutputFolder">The testOutputFolder<see cref="string"/>.</param>
        private void ParseTestResults(string testOutputFolder)
        {
            var reader = new CoverageReader();

            var file = $"{testOutputFolder}coverage.cobertura.xml";

            var coverageResults = reader.ReadFile(file);

            CoverageResultsProvider.Instance?.SetResults(coverageResults);
        }

        /// <summary>
        /// The RunCoverage.
        /// </summary>
        /// <param name="dte">The dte<see cref="DTE2"/>.</param>
        /// <param name="webBrowserSvc">The webBrowserSvc<see cref="IVsWebBrowsingService"/>.</param>
        private void RunCoverage(DTE2 dte, IVsWebBrowsingService webBrowserSvc)
        {

            var slnFile = dte.Solution.FileName;
            var testOutputFolder = $"{Path.GetTempPath()}{Guid.NewGuid().ToString().ToLowerInvariant()}\\";

            Debug.WriteLine("---CoverletRunner: Set output folder to " + testOutputFolder);

            this.RunCoverageTool(slnFile, testOutputFolder);
            var report = this.RunCoverageReporter(testOutputFolder);

            this.ParseTestResults(testOutputFolder);

            this.OpenReport(webBrowserSvc, report);
        }

        /// <summary>
        /// The RunCoverageReporter.
        /// </summary>
        /// <param name="testOutputFolder">The testOutputFolder<see cref="string"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        private string RunCoverageReporter(string testOutputFolder)
        {
            // report
            try
            {
                var args = $"-reports:{testOutputFolder}/**/*.cobertura.xml -targetdir:{testOutputFolder}/coverageReport -reporttypes:Html";
                Debug.WriteLine("---CoverletRunner: running command: reportgenerator " + args);

                var procStartInfo = new ProcessStartInfo("reportgenerator", args)
                {
                    UseShellExecute = true,
                    RedirectStandardOutput = false,
                    CreateNoWindow = false
                };

                var process = new System.Diagnostics.Process
                {
                    StartInfo = procStartInfo
                };
                process.Start();
                process.WaitForExit();

                // return the report
                return $"{testOutputFolder}coverageReport\\index.htm";
            }
            catch (Exception ex)
            {
                VsShellUtilities.ShowMessageBox(
                this.package,
                "Unable to generate coverage report. Make sure report generator is installed as a dotnet global tool\r\n\r\n" + ex.Message,
                "Failed to create coverage report",
                OLEMSGICON.OLEMSGICON_INFO,
                OLEMSGBUTTON.OLEMSGBUTTON_OK,
                OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
                throw;
            }
        }

        /// <summary>
        /// The RunCoverageTool.
        /// </summary>
        /// <param name="slnFile">The slnFile<see cref="string"/>.</param>
        /// <param name="testOutputFolder">The testOutputFolder<see cref="string"/>.</param>
        private void RunCoverageTool(string slnFile, string testOutputFolder)
        {
            try
            {
                string exludeAssembliesArg = string.Empty;
                if (!string.IsNullOrWhiteSpace(CoverageResultsProvider.Instance.ExcludeAssembliesPattern))
                {
                    exludeAssembliesArg = $"/p:Exclude=\"{CoverageResultsProvider.Instance.ExcludeAssembliesPattern}\"";
                }

                var args = $"test \"{slnFile}\" /p:CollectCoverage=true /p:CoverletOutput=\"{testOutputFolder}coverage\" {exludeAssembliesArg} /p:CoverletOutputFormat=\"json%2ccobertura\" /p:MergeWith=\"{testOutputFolder}coverage.json\" -m:1";
                Debug.WriteLine("---CoverletRunner: running command: dotnet " + args);

                var procStartInfo = new ProcessStartInfo("dotnet", args)
                {
                    UseShellExecute = true,
                    RedirectStandardOutput = false,
                    CreateNoWindow = false
                };

                var process = new System.Diagnostics.Process
                {
                    StartInfo = procStartInfo
                };

                process.Start();
                process.WaitForExit();
            }
            catch (Exception)
            {
                VsShellUtilities.ShowMessageBox(
                this.package,
                "Unable to run dotnet test command. Make sure dotnet test works from a command line",
                "Failed to create coverage report",
                OLEMSGICON.OLEMSGICON_INFO,
                OLEMSGBUTTON.OLEMSGBUTTON_OK,
                OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);

                throw;
            }
        }
    }
}
