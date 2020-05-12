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

namespace UnrealMacroGenerator.EditCommand
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class EditCommand
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 256;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("7849dfd8-ddf5-4ea4-90a0-16c7ea35b0cf");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly AsyncPackage package;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditCommand"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        /// <param name="commandService">Command service to add command to, not null.</param>
        private EditCommand(AsyncPackage package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new MenuCommand(this.EditMacroCallback, menuCommandID);
            commandService.AddCommand(menuItem);
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static EditCommand Instance
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
                return this.package;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static async System.Threading.Tasks.Task InitializeAsync(AsyncPackage package)
        {
            // Switch to the main thread - the call to AddCommand in EditCommand's constructor requires
            // the UI thread.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService = await package.GetServiceAsync((typeof(IMenuCommandService))) as OleMenuCommandService;
            Instance = new EditCommand(package, commandService);
        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// </summary>
        /// <param name="Sender">Event sender.</param>
        /// <param name="Args">Event args.</param>
        private void EditMacroCallback(object Sender, EventArgs Args)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            DTE Dte = (DTE)ServiceProvider.GetService(typeof(DTE));
            Assumes.Present(Dte);
            var ActiveDocument = Dte.ActiveDocument;
            
            if (ActiveDocument != null)
            {
                // 一旦カーソル位置の行を全て選択
                var Selection = (TextSelection)ActiveDocument.Selection;
                Selection.SelectLine();

                // サポートしているマクロであるか確認しマクロの種類を特定
                string TargetType = string.Empty;
                string[] MacroTypes = XmlFunctionLibrary.GetMacroTypes();
                foreach (var MacroType in MacroTypes)
                {
                    if (Selection.Text.Contains(MacroType))
                    {
                        TargetType = MacroType;
                        break;
                    }
                }

                // マクロの部分だけ選択する
                string TargetParameters = string.Empty;
                if (!string.IsNullOrEmpty(TargetType))
                {
                    // マクロの行の先頭に移動
                    Selection.StartOfLine();
                    Selection.LineUp();

                    // マクロの位置まで選択
                    while (true)
                    {
                        if (Selection.Text.Contains(TargetType))
                        {
                            break;
                        }
                        Selection.WordRight(true);
                    }
                    Selection.WordLeft();
                    Selection.WordRight(true);

                    // マクロの閉じカッコまで選択
                    bool bIsInString = false;
                    int Depth = 0;
                    while (true)
                    {
                        Selection.CharRight(true);

                        var LastChar = Selection.Text[Selection.Text.Length - 1];

                        // マクロ名以降のみを取得
                        TargetParameters += LastChar;

                        // 文字列中はカウントしない
                        if (LastChar == '\"')
                        {
                            bIsInString = !bIsInString;
                        }

                        // カッコの深さをカウント
                        if (!bIsInString && LastChar == '(')
                        {
                            Depth++;
                        }
                        else if (!bIsInString && LastChar == ')')
                        {
                            Depth--;
                        }

                        // カッコの数が合ったら終了
                        if (Depth <= 0)
                        {
                            break;
                        }

                        // カッコの数が合わなかった時の対策
                        if(!bIsInString && LastChar == '\n')
                        {
                            break;
                        }
                    }
                }
                
                // エディタUIを起動
                if (!string.IsNullOrEmpty(TargetType) && !string.IsNullOrEmpty(TargetParameters))
                {
                    MacroEditor EditorDialog = new MacroEditor(TargetType, TargetParameters);
                    EditorDialog.ShowDialog();
                    if (EditorDialog.DialogResult == DialogResult.OK)
                    {
                        if (ActiveDocument != null)
                        {
                            Selection.Text = EditorDialog.MacroString;

                            // カーソルの行を更新
                            Selection.SelectLine();
                            Selection.SmartFormat();
                            Selection.EndOfLine();
                        }
                    }
                }
            }
        }
    }
}
