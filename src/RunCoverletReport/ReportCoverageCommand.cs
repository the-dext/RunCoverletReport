namespace RunCoverletReport
{
    using EnvDTE;
    using EnvDTE80;
    using Microsoft.VisualStudio.Shell;
    using Microsoft.VisualStudio.Shell.Interop;
    using RunCoverletReport.CoverageResults;
    using System;
    using System.ComponentModel.Design;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
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
            // Switch to the main thread - the call to AddCommand in ReportCoverageCommand's
            // constructor requires the UI thread.
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
        /// <param name="webBrowserSvc">The webBrowserSvc <see cref="IVsWebBrowsingService"/>.</param>
        /// <param name="report">The report <see cref="string"/>.</param>
        private void OpenReport(IVsWebBrowsingService webBrowserSvc, string report)
        {
            // ThreadHelper.ThrowIfNotOnUIThread();
            try
            {
                ThreadHelper.Generic.BeginInvoke(() => webBrowserSvc.Navigate(report, 0, out _));
            }
            catch { }
        }

        private void ParseTestResults(string file)
        {
            var reader = new CoverageReader();
            var coverageResults = reader.ReadFile(file);
            CoverageResultsProvider.Instance?.SetResults(coverageResults);
        }

        /// <summary>
        /// The RunCoverage.
        /// </summary>
        /// <param name="dte">The dte <see cref="DTE2"/>.</param>
        /// <param name="webBrowserSvc">The webBrowserSvc <see cref="IVsWebBrowsingService"/>.</param>
        private void RunCoverage(DTE2 dte, IVsWebBrowsingService webBrowserSvc)
        {
            var slnFile = dte.Solution.FileName;
            string testOutputFolder = this.GetOutputFolder();
            Debug.WriteLine("---CoverletRunner: Set output folder to " + testOutputFolder);

            var useMSBuild = CoverageResultsProvider.Instance.Options.IntegrationType == Options.IntegrationType.MSBuild;

            (string cmd, string args) cmdArgs;
            if (!useMSBuild)
            {
                cmdArgs = this.GetCommandArgsForCoverletCollector(slnFile, testOutputFolder);
            }
            else
            {
                cmdArgs = this.GetCommandArgsForCoverletMSBuild(slnFile, testOutputFolder);
            }

            this.RunCoverageTool(cmdArgs.cmd, cmdArgs.args);

            if (!Directory.Exists(testOutputFolder))
            {
                var errorMessage = @"Unable to find test coverage output folder that Coverlet should have created.
1. Check that your solution builds. You may also want to check your unit tests pass.
2. Check you have both Coverlet and Report Generator setup correctly.
3. If you have set 'Restore NuGet Packages' to 'False', make sure you have restored them yourself.
4. If you have decided to use the 'Coverlet.Collector' NuGet package to collect code coverage make sure you have the 'Integration type' set to 'Collector'.
If you are using 'Coverlet.MSBuild' then make sure to select 'MSBuild' instead.
See 'Tools | Options | Run Coverlet Report' for settings.

Folder searched: " + testOutputFolder;

                this.ShowErrorMessage("Coverlet Output Not Found", errorMessage);
            }
            else
            {
                var report = this.RunCoverageReporter(testOutputFolder);
                this.OpenReport(webBrowserSvc, report.reportFile);
                this.ParseTestResults(report.coberturaXmlFile);
            }
        }

        private string GetOutputFolder()
        {
#if FIXED_OUTPUT_FOLDER
            var testOutputFolder = $"{Path.GetTempPath()}runcoverlet-report\\";
#else
            var testOutputFolder = $"{Path.GetTempPath()}{Guid.NewGuid().ToString().ToLowerInvariant()}\\";
#endif
            return testOutputFolder;
        }

        private (string cmd, string args) GetCommandArgsForCoverletMSBuild(string slnFile, string testOutputFolder)
        {
            string exludeAssembliesArg = string.Empty;
            if (!string.IsNullOrWhiteSpace(CoverageResultsProvider.Instance.Options.ExcludeAssembliesPattern))
            {
                exludeAssembliesArg = $"/p:Exclude=\"{CoverageResultsProvider.Instance.Options.ExcludeAssembliesPattern.Replace(",", "%2c")}\"";
            }

            string noRestoreArg = string.Empty;
            if (!CoverageResultsProvider.Instance.Options.RestorePackages)
            {
                noRestoreArg = " --no-restore";
            }

            string args = $"test \"{slnFile}\" /p:CollectCoverage=true /p:CoverletOutput=\"{testOutputFolder}coverage\" {exludeAssembliesArg} /p:CoverletOutputFormat=\"json%2ccobertura\" /p:MergeWith=\"{testOutputFolder}coverage.json\" -m:1{noRestoreArg}";

            return ("dotnet", args);
        }

        /// <summary>
        /// The RunCoverageReporter.
        /// </summary>
        /// <param name="testOutputFolder">The testOutputFolder <see cref="string"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        private (string reportFile, string coberturaXmlFile) RunCoverageReporter(string testOutputFolder)
        {
            // report
            try
            {
                var args = $"-reports:{testOutputFolder}/**/*.cobertura.xml -targetdir:{testOutputFolder}/coverageReport -reporttypes:Html;Cobertura";
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
                return ($"{testOutputFolder}coverageReport\\index.htm", $"{testOutputFolder}coverageReport\\Cobertura.xml");
            }
            catch (Exception ex)
            {
                VsShellUtilities.ShowMessageBox(
                this.package,
                "Unable to generate coverage report. Make sure report generator is installed as a dotnet global tool\r\n\r\n" + ex.Message,
                "Failed to Create Coverage Report",
                OLEMSGICON.OLEMSGICON_INFO,
                OLEMSGBUTTON.OLEMSGBUTTON_OK,
                OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
                throw;
            }
        }

        private (string cmd, string args) GetCommandArgsForCoverletCollector(string slnFile, string testOutputFolder)
        {
            string excludeAssembliesArg = string.Empty;
            if (!string.IsNullOrWhiteSpace(CoverageResultsProvider.Instance.Options.ExcludeAssembliesPattern))
            {
                excludeAssembliesArg = $"-- DataCollectionRunSettings.DataCollectors.DataCollector.Configuration.Exclude=\"{CoverageResultsProvider.Instance.Options.ExcludeAssembliesPattern.Replace(",", "%2c")}\"";
            }

            string noRestoreArg = string.Empty;
            if (!CoverageResultsProvider.Instance.Options.RestorePackages)
            {
                noRestoreArg = " --no-restore";
            }

            string args = $"test \"{slnFile}\" /p:CoverletOutputFormat=\"cobertura\" --collect:\"XPlat Code Coverage\" --results-directory:\"{testOutputFolder}coverage\"{noRestoreArg} {excludeAssembliesArg}";

            return ("dotnet", args);
        }

        /// <summary>
        /// The RunCoverageTool.
        /// </summary>
        /// <param name="slnFile">The slnFile <see cref="string"/>.</param>
        /// <param name="testOutputFolder">The testOutputFolder <see cref="string"/>.</param>
        private void RunCoverageTool(string cmd, string args)
        {
            try
            {
                Debug.WriteLine("---CoverletRunner: running command: dotnet " + args);

                var procStartInfo = new ProcessStartInfo(cmd, args)
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
                this.ShowErrorMessage("Failed to create coverage report", "Unable to run dotnet test command. Make sure dotnet test works from a command line");
                throw;
            }
        }

        private void ShowInfoMessage(string title, string message)
        {
            VsShellUtilities.ShowMessageBox(
            this.package,
            message,
            title,
            OLEMSGICON.OLEMSGICON_INFO,
            OLEMSGBUTTON.OLEMSGBUTTON_OK,
            OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
        }

        private void ShowErrorMessage(string title, string message)
        {
            VsShellUtilities.ShowMessageBox(
            this.package,
            message,
            title,
            OLEMSGICON.OLEMSGICON_CRITICAL,
            OLEMSGBUTTON.OLEMSGBUTTON_OK,
            OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
        }
    }
}