using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace UnrealMacroGenerator.DialogUI
{
    public partial class LogEditor : Form
    {
        // 入力フォーマット指定子
        private static readonly string[] InputFormatSpecifiers =
        {
            "%c", "%s", "%d", "%u", "%o", "%x", "%f", "%hd", "%ld", "%hu", "%lu", "%lo", "%lx", "%lf"
        };

        // 結果保存用
        public string MacroString { get; private set; }

        // マクロの種類
        private string MacroName = string.Empty;

        // ドキュメントのURL
        private string DocumentLink = string.Empty;

        // 編集対象
        private string EditTarget = string.Empty;

        public LogEditor(string MacroType, string TargetString = null)
        {
            InitializeComponent();

            MacroName = MacroType;
            EditTarget = TargetString;
        }

        private void OnEditorLoad(object Sender, EventArgs Args)
        {
            // 初期化
            InitializeList();

            // ドキュメントをロード 
            DocumentLink = SettingsFunctionLibrary.GetDocumentLink("Logging");

            // 編集モードで開いた場合
            if(!string.IsNullOrEmpty(EditTarget))
            {
                ReflectParameterInList();
            }
        }

        private void InitializeList()
        {
            Tlp_Arguments.SuspendLayout();
            Tlp_Arguments.RowCount = 0;
            Tlp_Arguments.RowStyles.Clear();

            string[] SelecterNames = new string[2];
            Label[] Labels = new Label[] { Lbl_Selecter1, Lbl_Selecter2 };
            List<string[]> SelecterItems = new List<string[]>();
            ComboBox[] Selecters = new ComboBox[] { Cb_Selecter1, Cb_Selecter2 };

            if(MacroName == "UE_LOG")
            {
                SelecterItems.Add(SettingsFunctionLibrary.GetLogCategory());
                SelecterItems.Add(XmlFunctionLibrary.GetLogVerbosity());
                SelecterNames[0] = "CategoryName";
                SelecterNames[1] = "Verbosity";
                Lbl_Input.Text = "Format";
                Tb_Input.TextChanged += new EventHandler(OnTextChanged);

                Cb_Selecter1.SelectedText = "LogTemp";
                Cb_Selecter2.SelectedText = "Log";
            }
            else
            {
                var Verbosities = XmlFunctionLibrary.GetLogVerbosity();
                SelecterItems.Add(Verbosities);
                SelecterItems.Add(Verbosities);
                SelecterNames[0] = "DefaultVerbosity";
                SelecterNames[1] = "CompileTimeVerbosity";
                Cb_Selecter1.SelectedText = "Log";
                Cb_Selecter2.SelectedText = "All";
                Lbl_Input.Text = "CategoryName";
                Lbl_Arguments.Text = "MacroType";

                string[] MacroTypes = XmlFunctionLibrary.GetMacroTypes(false, false, true);
                List<string> LogMacroTypes = new List<string>();
                foreach(var MacroType in MacroTypes)
                {
                    if (MacroType.Contains("LOG_CATEGORY")) 
                    {
                        LogMacroTypes.Add(MacroType);
                    }
                }

                ComboBox Type = new ComboBox();
                Type.Items.AddRange(LogMacroTypes.ToArray());
                Type.Width = 300;
                Type.SelectedIndexChanged += new EventHandler(OnMacroTypeChanged);

                if(!string.IsNullOrEmpty(EditTarget))
                {
                    Type.Text = MacroName;
                }
                else
                {
                    Type.SelectedIndex = 0;
                    MacroName = Type.Text;
                }

                Tlp_Arguments.Controls.Add(Type);
            }

            for(int Index = 0; Index < Selecters.Length; Index++)
            {
                if(!string.IsNullOrEmpty(SelecterNames[Index]))
                {
                    Labels[Index].Text = SelecterNames[Index];
                    Selecters[Index].Items.AddRange(SelecterItems[Index]);
                }
                else
                {
                    Labels[Index].Visible = false;
                    Selecters[Index].Visible = false;
                }
            }
            
            Tlp_Arguments.Dock = DockStyle.Top;
            Tlp_Arguments.AutoSize = true;
            Tlp_Arguments.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Tlp_Arguments.Padding = new Padding(0, 0, SystemInformation.VerticalScrollBarWidth, 0);

            Tlp_Arguments.ResumeLayout();
        }

        private void ReflectParameterInList()
        {
            // カッコと空白を取り除く
            string TrimmedTarget = StringFunctionLibrary.RemoveChars(EditTarget, new char[] { '(', ')', ' ', ';' });

            // カンマで分ける
            List<string> ParsedParameters = StringFunctionLibrary.SplitParameterByComma(TrimmedTarget);

            if (MacroName == "UE_LOG")
            {
                // TEXTを取り除く
                for (int Index = 0; Index < ParsedParameters.Count; Index++)
                {
                    if (ParsedParameters[Index].Length > 4)
                    {
                        string Head = ParsedParameters[Index].Substring(0, 4);
                        if (Head == "TEXT")
                        {
                            ParsedParameters[Index] = ParsedParameters[Index].Remove(0, 4);
                        }
                    }
                }

                // 項目数が足りなければエラー
                if (ParsedParameters.Count < 3)
                {
                    MessageBox.Show(
                                "Macro structure is abnormal",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error
                                );
                    Close();
                    return;
                }

                // カテゴリを設定
                Cb_Selecter1.Text = ParsedParameters[0];

                // 詳細度を設定
                Cb_Selecter2.Text = ParsedParameters[1];

                // フォーマット文字列を設定
                Tb_Input.Text = ParsedParameters[2].TrimStart('\"').TrimEnd('\"');

                // 引数を設定
                if (ParsedParameters.Count > 3)
                {
                    ParsedParameters.RemoveRange(0, 3);
                    var Controls = Tlp_Arguments.Controls;
                    for (int Index = 0; Index < Controls.Count; Index++)
                    {
                        if (Controls[Index] is TextBox TextBox)
                        {
                            TextBox.Text = ParsedParameters[Index];
                        }
                    }
                }
            }
            else
            {
                if(ParsedParameters.Count == 1)
                {
                    Tb_Input.Text = ParsedParameters[0];
                    Cb_Selecter1.Visible = false;
                    Cb_Selecter2.Visible = false;
                    Lbl_Selecter1.Visible = false;
                    Lbl_Selecter2.Visible = false;
                }
                else if(ParsedParameters.Count == 3)
                {
                    Tb_Input.Text = ParsedParameters[0];
                    Cb_Selecter1.Text = ParsedParameters[1];
                    Cb_Selecter2.Text = ParsedParameters[2];
                }
                else 
                {
                    MessageBox.Show(
                                "Macro structure is abnormal",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error
                                );
                    Close();
                    return;
                }
            }
        }

        private void AdjustArgumentsTable(string Format)
        {
            // フォーマット文字列内の入力フォーマット指定子を数える
            int SpecifierCount = StringFunctionLibrary.CountOfString(Format, InputFormatSpecifiers);

            Tlp_Arguments.SuspendLayout();
            int Different = Math.Abs(SpecifierCount - Tlp_Arguments.RowCount);

            //増えた
            if (SpecifierCount > Tlp_Arguments.RowCount)
            {
                for (int Index = 0; Index < Different; Index++)
                {
                    TextBox Input = new TextBox();
                    Input.ScrollBars = ScrollBars.Horizontal;
                    Input.BorderStyle = BorderStyle.FixedSingle;
                    Input.Width = 240;
                    Tlp_Arguments.RowCount++;
                    Tlp_Arguments.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
                    Tlp_Arguments.Controls.Add(Input);
                }
            }
            // 減った
            else if (SpecifierCount < Tlp_Arguments.RowCount)
            {
                for (int Index = 0; Index < Different; Index++)
                {
                    Tlp_Arguments.Controls.RemoveAt(Tlp_Arguments.Controls.Count - 1);
                    Tlp_Arguments.RowStyles.RemoveAt(Tlp_Arguments.RowCount - 1);
                    Tlp_Arguments.RowCount--;
                }
            }

            Tlp_Arguments.ResumeLayout();
        }

        private void OnTextChanged(object Sender, EventArgs Args)
        {
            AdjustArgumentsTable(Tb_Input.Text);
        }

        private void OnMacroTypeChanged(object Sender, EventArgs Args)
        {
            if(Sender is ComboBox MacroType)
            {
                MacroName = MacroType.Text;
                bool bShowSelecter = (MacroName != "DEFINE_LOG_CATEGORY");
                Lbl_Selecter1.Visible = bShowSelecter;
                Lbl_Selecter2.Visible = bShowSelecter;
                Cb_Selecter1.Visible = bShowSelecter;
                Cb_Selecter2.Visible = bShowSelecter;
            }
        }

        private void OnDocumentLinkClicked(object Sender, LinkLabelLinkClickedEventArgs Args)
        {
            if (!string.IsNullOrEmpty(DocumentLink))
            {
                System.Diagnostics.Process.Start(DocumentLink);
            }
        }

        private void OnEnterPushed(object Sender, KeyEventArgs Args)
        {
            // 改行させない
            if (Args.KeyCode == Keys.Enter)
            {
                Args.SuppressKeyPress = true;
                Args.Handled = true;
            }
        }

        private void OnOKButtonClicked(object Sender, EventArgs Args)
        {
            MacroString += MacroName + "(";

            if (MacroName == "UE_LOG")
            {
                // カテゴリと詳細度とフォーマット文字列を連結
                MacroString += Cb_Selecter1.Text + "," + Cb_Selecter2.Text + ",TEXT(\"" + Tb_Input.Text + "\")";

                // 引数を連結
                List<string> Arguments = new List<string>();
                var Controls = Tlp_Arguments.Controls;
                foreach (var Control in Controls)
                {
                    if (Control is TextBox TextBox)
                    {
                        Arguments.Add(TextBox.Text);
                    }
                }
                if (Arguments.Count != 0)
                {
                    foreach (var Argument in Arguments)
                    {
                        MacroString += "," + Argument;
                    }
                }
            }
            else
            {
                // 定義したカテゴリ名を連結
                MacroString += Tb_Input.Text;

                // デフォルトとコンパイル時の詳細度を連結
                if (MacroName != "DEFINE_LOG_CATEGORY")
                {
                    MacroString += "," + Cb_Selecter1.Text + "," + Cb_Selecter2.Text;
                }
            }

            MacroString += ")";

            if(string.IsNullOrEmpty(EditTarget))
            {
                MacroString += ";";
            }
        }
    }
}
