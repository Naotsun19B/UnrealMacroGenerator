using System;
using System.ComponentModel.Design;
using System.Globalization;
using System.Windows.Forms;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Task = System.Threading.Tasks.Task;
using UnrealMacroGenerator.Properties;

namespace UnrealMacroGenerator.Command
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class ResetSettings
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 256;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("9b946524-db34-467e-bf4a-c08df0fc0aaf");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly AsyncPackage Package;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResetSettings"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="Package">Owner package, not null.</param>
        /// <param name="CommandService">Command service to add command to, not null.</param>
        private ResetSettings(AsyncPackage Package, OleMenuCommandService CommandService)
        {
            this.Package = Package ?? throw new ArgumentNullException(nameof(Package));
            CommandService = CommandService ?? throw new ArgumentNullException(nameof(CommandService));

            var MenuCommandID = new CommandID(CommandSet, CommandId);
            var MenuItem = new MenuCommand(this.ResetSettingsCallback, MenuCommandID);
            CommandService.AddCommand(MenuItem);
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static ResetSettings Instance
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
                return this.Package;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="Package">Owner package, not null.</param>
        public static async Task InitializeAsync(AsyncPackage Package)
        {
            // Switch to the main thread - the call to AddCommand in ResetSettings's constructor requires
            // the UI thread.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(Package.DisposalToken);

            OleMenuCommandService CommandService = await Package.GetServiceAsync((typeof(IMenuCommandService))) as OleMenuCommandService;
            Instance = new ResetSettings(Package, CommandService);
        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// </summary>
        /// <param name="Sender">Event sender.</param>
        /// <param name="Args">Event args.</param>
        private void ResetSettingsCallback(object Sender, EventArgs Args)
        {
            var Result = MessageBox.Show(
                            "All settings in Unreal Macro Generator will be default values\r\n" +
                            "Do you want to continue the operation ?",
                            "Confirmation",
                            MessageBoxButtons.OKCancel,
                            MessageBoxIcon.Question
                            );

            if (Result == DialogResult.OK)
            {
                Settings.Default.Reset();
                Settings.Default.Save();
            }
        }
    }
}
