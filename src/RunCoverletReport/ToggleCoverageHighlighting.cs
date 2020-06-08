using System;
using System.ComponentModel.Design;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using RunCoverletReport.CoverageResults;
using Task = System.Threading.Tasks.Task;

namespace RunCoverletReport
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class ToggleCoverageHighlighting
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 4129;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("0e49fe34-81c1-41aa-a08f-40a829edde58");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly AsyncPackage package;
        private MenuCommand MenuItem;

        /// <summary>
        /// Initializes a new instance of the <see cref="ToggleCoverageHighlighting"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        /// <param name="commandService">Command service to add command to, not null.</param>
        private ToggleCoverageHighlighting(AsyncPackage package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandID = new CommandID(CommandSet, CommandId);
            this.MenuItem = new MenuCommand(this.Execute, menuCommandID);
            this.MenuItem.Checked = true;
            commandService.AddCommand(this.MenuItem);
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static ToggleCoverageHighlighting Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
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
        public static async Task InitializeAsync(AsyncPackage package)
        {
            // Switch to the main thread - the call to AddCommand in ToggleCoverageHighlighting's constructor requires
            // the UI thread.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new ToggleCoverageHighlighting(package, commandService);
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
            ThreadHelper.ThrowIfNotOnUIThread();
            
            CoverageResultsProvider.ShowSyntaxHighlighting = !CoverageResultsProvider.ShowSyntaxHighlighting;
            this.MenuItem.Checked = CoverageResultsProvider.ShowSyntaxHighlighting;

            string message = string.Format(CultureInfo.CurrentCulture, "Coverage Highlighting is {0}", CoverageResultsProvider.ShowSyntaxHighlighting ? "on." : "off.");
            string title = "Toggle Code Coverage Highlighting";

            //// Show a message box to prove we were here
            //VsShellUtilities.ShowMessageBox(
            //    this.package,
            //    message,
            //    title,
            //    OLEMSGICON.OLEMSGICON_INFO,
            //    OLEMSGBUTTON.OLEMSGBUTTON_OK,
            //    OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
        }
    }
}
