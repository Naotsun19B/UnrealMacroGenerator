﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        // 引数リストの最小項目数
        private static readonly int ArgumentsMin = 18;

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
            Cb_CategoryName.Items.AddRange(XmlFunctionLibrary.GetLogCategory());
            Cb_Verbosity.Items.AddRange(XmlFunctionLibrary.GetLogVerbosity());
            Cb_CategoryName.SelectedText = "LogTemp";
            Cb_Verbosity.SelectedText = "Log";

            Tlp_Arguments.Dock = DockStyle.Top;
            Tlp_Arguments.RowCount = 0;
            Tlp_Arguments.RowStyles.Clear();
            Tlp_Arguments.AutoSize = true;
            Tlp_Arguments.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Tlp_Arguments.Padding = new Padding(0, 0, SystemInformation.VerticalScrollBarWidth, 0);
            if (Tlp_Arguments.RowCount < ArgumentsMin)
            {
                int Count = ArgumentsMin - Tlp_Arguments.RowCount;
                for (int Row = 0; Row < Count; Row++)
                {
                    Tlp_Arguments.RowCount++;
                    Tlp_Arguments.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
                }
            }

            // ドキュメントをロード
            DocumentLink = XmlFunctionLibrary.GetDocumentationLink(MacroName);

            // 編集モードで開いた場合
            if(!string.IsNullOrEmpty(EditTarget))
            {
                ReflectParameterInList();
            }
        }

        private void ReflectParameterInList()
        {
            // カッコと空白を取り除く
            string TrimmedTarget = FunctionLibrary.RemoveChars(EditTarget, new char[] { '(', ')', ' ', ';' });

            // カンマで分ける
            List<string> ParsedParameters = FunctionLibrary.SplitParameterByComma(TrimmedTarget);

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
            if(ParsedParameters.Count < 3)
            {
                MessageBox.Show(
                            "Macro structure is abnormal",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                            );
                Close();
            }

            // カテゴリを設定
            Cb_CategoryName.Text = ParsedParameters[0];

            // 詳細度を設定
            Cb_Verbosity.Text = ParsedParameters[1];

            // フォーマット文字列を設定
            Tb_Format.Text = ParsedParameters[2].TrimStart('\"').TrimEnd('\"');

            // 引数を設定
            if (ParsedParameters.Count > 3)
            {
                ParsedParameters.RemoveRange(0, 3);
                AdjustArgumentsTable(Tb_Format.Text, ParsedParameters);
            }
        }

        private void AdjustArgumentsTable(string Format, List<string> InArguments = null)
        {
            // フォーマット文字列内の入力フォーマット指定子を数える
            int SpecifierCount = FunctionLibrary.CountOfString(Format, InputFormatSpecifiers);

            // 数が変わらないならここで終わり
            if ((SpecifierCount == 0 && Tlp_Arguments.RowCount == 0) || Tlp_Arguments.RowCount == SpecifierCount)
            {
                return;
            }

            // 入力データを控える
            List<string> Arguments = new List<string>();
            if (InArguments == null)
            {
                var Controls = Tlp_Arguments.Controls;
                foreach (var Control in Controls)
                {
                    if (Control is TextBox TextBox)
                    {
                        Arguments.Add(TextBox.Text);
                    }
                }
            }
            else
            {
                Arguments = InArguments;
            }

            // TextBoxの数を変更
            Tlp_Arguments.SuspendLayout();
            Tlp_Arguments.RowCount = 0;
            Tlp_Arguments.Controls.Clear();
            Tlp_Arguments.RowStyles.Clear();

            for (int Index = 0; Index < SpecifierCount; Index++)
            {
                TextBox Input = new TextBox();
                Input.ScrollBars = ScrollBars.Horizontal;
                Input.BorderStyle = BorderStyle.FixedSingle;
                Input.Width = 240;
                if (Index < Arguments.Count)
                {
                    Input.Text = Arguments[Index];
                }

                Tlp_Arguments.RowCount++;
                Tlp_Arguments.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
                Tlp_Arguments.Controls.Add(Input);
            }

            // 項目が少なかった時の埋め合わせ
            if (Tlp_Arguments.RowCount < ArgumentsMin)
            {
                int Count = ArgumentsMin - Tlp_Arguments.RowCount;
                for (int Row = 0; Row < Count; Row++)
                {
                    Tlp_Arguments.RowCount++;
                    Tlp_Arguments.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
                }
            }

            Tlp_Arguments.ResumeLayout();
        }

        private void OnTextChanged(object Sender, EventArgs Args)
        {
            AdjustArgumentsTable(Tb_Format.Text);
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

            // カテゴリと詳細度とフォーマット文字列を連結
            MacroString += Cb_CategoryName.Text + "," + Cb_Verbosity.Text + ",TEXT(\"" + Tb_Format.Text + "\")";

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
            if(Arguments.Count != 0)
            {
                foreach(var Argument in Arguments)
                {
                    MacroString += "," + Argument;
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
