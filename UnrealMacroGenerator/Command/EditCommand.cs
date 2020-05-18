using System;
using System.ComponentModel.Design;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft;
using System.Windows.Forms;
using UnrealMacroGenerator.DialogUI;
using System.Collections.Generic;

namespace UnrealMacroGenerator.Command
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
        private readonly AsyncPackage Package;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditCommand"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="Package">Owner package, not null.</param>
        /// <param name="CommandService">Command service to add command to, not null.</param>
        private EditCommand(AsyncPackage Package, OleMenuCommandService CommandService)
        {
            this.Package = Package ?? throw new ArgumentNullException(nameof(Package));
            CommandService = CommandService ?? throw new ArgumentNullException(nameof(CommandService));

            var MenuCommandID = new CommandID(CommandSet, CommandId);
            var MenuItem = new MenuCommand(this.EditMacroCallback, MenuCommandID);
            CommandService.AddCommand(MenuItem);
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
                return this.Package;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="Package">Owner package, not null.</param>
        public static async System.Threading.Tasks.Task InitializeAsync(AsyncPackage Package)
        {
            // Switch to the main thread - the call to AddCommand in EditCommand's constructor requires
            // the UI thread.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(Package.DisposalToken);

            OleMenuCommandService CommandService = await Package.GetServiceAsync((typeof(IMenuCommandService))) as OleMenuCommandService;
            Instance = new EditCommand(Package, CommandService);
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

                // マクロの名前だけ選択
                string TargetType = string.Empty;
                List<string> MacroTypes = new List<string>(XmlFunctionLibrary.GetMacroTypes(true, true, false));
                foreach (var MacroType in MacroTypes)
                {
                    if (Selection.Text.Contains(MacroType))
                    {
                        TargetType = MacroType;
                        break;
                    }
                }

                Selection.StartOfLine();
                Selection.LineUp();

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

                TargetType = Selection.Text;

                // サポートしてなかったらエラー
                MacroTypes.Clear();
                MacroTypes.AddRange(XmlFunctionLibrary.GetMacroTypes(false, false, true));
                if (!MacroTypes.Contains(TargetType))
                {
                    string SupportedMacros = string.Empty;
                    for (int Index = 0; Index < MacroTypes.Count; Index++)
                    {
                        SupportedMacros += MacroTypes[Index] + "\r\n";
                    }

                    MessageBox.Show(
                            TargetType + " is not a supported macro",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                            );
                    return;
                }

                // マクロの中身だけ選択する
                string TargetParameters = string.Empty;
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
                    if (Selection.ActivePoint.AtEndOfLine)
                    {
                        break;
                    }
                }

                // 文字列中に"が入っている場合はエラー
                if (StringFunctionLibrary.CountOfChar(TargetParameters, '\"') % 2 != 0)
                {
                    MessageBox.Show(
                            "The string literal contains double quotes\r\n" +
                            "Remove the double quotes inside the string literal to edit the macro",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                            );
                    return;
                }

                if (!string.IsNullOrEmpty(TargetParameters))
                {
                    // エディタを起動
                    DialogResult DialogResult = DialogResult.None;
                    string EditResult = string.Empty;
                    switch (XmlFunctionLibrary.GetEditorType(TargetType))
                    {
                        case EditorType.MacroEditor:
                            MacroEditor MacroEditor = new MacroEditor(TargetType, TargetParameters);
                            MacroEditor.ShowDialog();
                            DialogResult = MacroEditor.DialogResult;
                            EditResult = MacroEditor.MacroString;
                            break;

                        case EditorType.LogEditor:
                            LogEditor LogEditor = new LogEditor(TargetType, TargetParameters);
                            LogEditor.ShowDialog();
                            DialogResult = LogEditor.DialogResult;
                            EditResult = LogEditor.MacroString;
                            break;

                        case EditorType.DelegateEditor:
                            DelegateEditor DelegateEditor = new DelegateEditor(TargetType, TargetParameters);
                            DelegateEditor.ShowDialog();
                            DialogResult = DelegateEditor.DialogResult;
                            EditResult = DelegateEditor.MacroString;
                            break;
                    }

                    if (ActiveDocument != null && DialogResult == DialogResult.OK && !string.IsNullOrEmpty(EditResult))
                    {
                        // カーソル位置に結果の文字列を挿入
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
}
