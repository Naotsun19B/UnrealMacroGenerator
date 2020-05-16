using System;
using System.ComponentModel.Design;
using System.Globalization;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Utilities;
using Microsoft.VisualStudio.TextManager.Interop;
using Microsoft.VisualStudio.Editor;
using Microsoft;
using System.Windows.Forms;
using UnrealMacroGenerator.DialogUI;

namespace UnrealMacroGenerator.Command
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class GenerateCommand
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0100;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("3a9cdb9a-28f9-4f19-bd3e-cf8e5a1e3599");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly AsyncPackage Package;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateCommand"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="Package">Owner package, not null.</param>
        /// <param name="CommandService">Command service to add command to, not null.</param>
        private GenerateCommand(AsyncPackage Package, OleMenuCommandService CommandService)
        {
            this.Package = Package ?? throw new ArgumentNullException(nameof(Package));
            CommandService = CommandService ?? throw new ArgumentNullException(nameof(CommandService));

            var MenuCommandID = new CommandID(CommandSet, CommandId);
            var MenuItem = new MenuCommand(this.GenerateMacroCallback, MenuCommandID);
            CommandService.AddCommand(MenuItem);
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static GenerateCommand Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private IServiceProvider ServiceProvider
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
        public static async System.Threading.Tasks.Task InitializeAsync(AsyncPackage Package)
        {
            // Switch to the main thread - the call to AddCommand in GenerateCommand's constructor requires
            // the UI thread.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(Package.DisposalToken);

            OleMenuCommandService CommandService = await Package.GetServiceAsync((typeof(IMenuCommandService))) as OleMenuCommandService;
            Instance = new GenerateCommand(Package, CommandService);
        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// </summary>
        /// <param name="Sender">Event sender.</param>
        /// <param name="Args">Event args.</param>
        private void GenerateMacroCallback(object Sender, EventArgs Args)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            // マクロの種類を取得
            EditorSelecter SelecterDialog = new EditorSelecter();
            SelecterDialog.ShowDialog();

            if (SelecterDialog.DialogResult == DialogResult.OK)
            {
                // エディタを起動
                DialogResult DialogResult = DialogResult.None;
                string EditResult = string.Empty;
                switch (XmlFunctionLibrary.GetEditorType(SelecterDialog.MacroType))
                {
                    case EditorType.MacroEditor:
                        MacroEditor MacroEditor = new MacroEditor(SelecterDialog.MacroType);
                        MacroEditor.ShowDialog();
                        DialogResult = MacroEditor.DialogResult;
                        EditResult = MacroEditor.MacroString;
                        break;

                    case EditorType.LogEditor:
                        LogEditor LogEditor = new LogEditor(SelecterDialog.MacroType);
                        LogEditor.ShowDialog();
                        DialogResult = LogEditor.DialogResult;
                        EditResult = LogEditor.MacroString;
                        break;

                    case EditorType.DelegateEditor:
                        DelegateEditor DelegateEditor = new DelegateEditor(SelecterDialog.MacroType);
                        DelegateEditor.ShowDialog();
                        DialogResult = DelegateEditor.DialogResult;
                        EditResult = DelegateEditor.MacroString;
                        break;
                }

                if (DialogResult == DialogResult.OK && !string.IsNullOrEmpty(EditResult))
                {
                    // カーソル位置に結果の文字列を挿入
                    DTE Dte = (DTE)ServiceProvider.GetService(typeof(DTE));
                    Assumes.Present(Dte);
                    var Selection = (TextSelection)Dte.ActiveDocument.Selection;
                    Selection.Text = EditResult;

                    // カーソルの行を更新
                    Selection.SelectLine();
                    Selection.SmartFormat();
                    Selection.EndOfLine();
                }
            }
        }
    }
}
