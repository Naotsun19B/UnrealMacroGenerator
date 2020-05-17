using System;
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
    public partial class DelegateEditor : Form
    {
        private static readonly string[] ParamNumbers =
        {
            "OneParam", "TwoParams", "ThreeParams", "FourParams", "FiveParams", "SixParams", "SevenParams", "EightParams", "NineParams"
        };

        // 結果保存用
        public string MacroString { get; private set; }

        // マクロの種類
        private string MacroName = string.Empty;

        // ドキュメントのURL
        private string DocumentLink = string.Empty;

        // 編集対象
        private string EditTarget = string.Empty;

        public DelegateEditor(string MacroType, string TargetString = null)
        {
            InitializeComponent();

            MacroName = MacroType;
            EditTarget = TargetString;
        }

        private void OnEditorLoad(object Sender, EventArgs Args)
        {
            // ドキュメントをロード
            DocumentLink = SettingsFunctionLibrary.GetDocumentLink("Delegate");

            InitializeList();

            if(!string.IsNullOrEmpty(EditTarget))
            {
                ReflectParameterInList();
            }
        }

        private void InitializeList()
        {
            Tlp_Arguments.SuspendLayout();

            Tlp_Arguments.RowCount = 0;
            Tlp_Arguments.Controls.Clear();
            Tlp_Arguments.RowStyles.Clear();
            Tlp_Arguments.Dock = DockStyle.Top;
            Tlp_Arguments.AutoSize = true;
            Tlp_Arguments.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Tlp_Arguments.Padding = new Padding(0, 0, SystemInformation.VerticalScrollBarWidth, 0);

            // 追加・削除ボタンを作成
            Tlp_Arguments.RowCount++;
            Tlp_Arguments.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            Tlp_Arguments.Controls.Add(CreateArgumentsButton(true));
            Button RemoveButton = CreateArgumentsButton(false);
            RemoveButton.Enabled = false;
            Tlp_Arguments.Controls.Add(RemoveButton);

            Tlp_Arguments.ResumeLayout();
        }

        private void ReflectParameterInList()
        {
            bool bIsEvent = MacroName.Contains("_EVENT");
            bool bHasRetVal = MacroName.Contains("_RetVal");
            bool bIsDynamic = MacroName.Contains("_DYNAMIC");
            bool bIsMulticast = MacroName.Contains("_MULTICAST");
            int ArgumentsCount = 0;
            for(int Index = 0; Index < ParamNumbers.Length; Index++)
            {
                if(MacroName.Contains(ParamNumbers[Index]))
                {
                    ArgumentsCount = Index + 1;
                    break;
                }
            }

            // チェックボックスなどを設定
            Cb_IsEvent.Checked = bIsEvent;
            Cb_HasRetVal.Checked = bHasRetVal;
            Cb_IsDynamic.Checked = bIsDynamic;
            Cb_IsMulticast.Checked = bIsMulticast;
            ReflectCheckBox();

            // カッコと空白を取り除く
            string TrimmedTarget = StringFunctionLibrary.RemoveChars(EditTarget, new char[] { '(', ')', ' ', ';' });

            // カンマで分ける
            List<string> ParsedParameters = StringFunctionLibrary.SplitParameterByComma(TrimmedTarget);

            // 項目数が足りなければエラー
            if (((bIsEvent || bHasRetVal) && ParsedParameters.Count < 2) || 
               (!(bIsEvent || bHasRetVal) && ParsedParameters.Count < 1)) 
            {
                MessageBox.Show(
                                "Macro structure is abnormal",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error
                                );
                Close();
            }

            int ParameterIndex = 0;

            // Type
            if(bIsEvent || bHasRetVal)
            {
                Tb_Type.Text = ParsedParameters[ParameterIndex++];
            }

            // Name
            Tb_Name.Text = ParsedParameters[ParameterIndex++];

            // Arguments
            if(ArgumentsCount > 0)
            {
                for(int Row = 0; Row < ArgumentsCount; Row++)
                {
                    AddArgumets();
                }

                var Controls = Tlp_Arguments.Controls;
                foreach(var Control in Controls)
                {
                    if(Control is TextBox TextBox && TextBox.Enabled && ParameterIndex < ParsedParameters.Count)
                    {
                        TextBox.Text = ParsedParameters[ParameterIndex++];
                    }
                }
            }
        }

        private void ReflectCheckBox()
        {
            bool bIsEvent = (Cb_IsEvent.Checked && Cb_IsEvent.Visible);
            bool bHasRetVal = (Cb_HasRetVal.Checked && Cb_HasRetVal.Visible);
            bool bIsDynamic = (Cb_IsDynamic.Checked && Cb_IsDynamic.Visible);
            bool bIsMulticast = (Cb_IsMulticast.Checked && Cb_IsMulticast.Visible);

            // Name
            Lbl_Name.Text = (bIsEvent ? "Event Name" : "Delegate Name");

            // Type
            Lbl_Type.Text = (bIsEvent ? "Owning Type" : "Return Value Type");
            Lbl_Type.Visible = (bIsEvent || bHasRetVal);
            Tb_Type.Visible = (bIsEvent || bHasRetVal);

            // Event
            Cb_IsEvent.Visible = (!bHasRetVal && !bIsDynamic && !bIsMulticast);

            // RetVal
            Cb_HasRetVal.Visible = (!bIsEvent && !bIsMulticast);

            // Dynamic
            Cb_IsDynamic.Visible = !bIsEvent;

            // Multicast
            Cb_IsMulticast.Visible = (!bIsEvent && !bHasRetVal);

            // ArgumentsName
            Lbl_Arguments.Text = (bIsDynamic ? "ArgumentsType         ArgumentsName" : "ArgumentsType");
            for (int Row = 0; Row < Tlp_Arguments.RowCount - 1; Row++)
            {
                var TextBox = Tlp_Arguments.GetControlFromPosition(1, Row);
                TextBox.Enabled = bIsDynamic;
            }
        }

        private void OnCheckBoxClicked(object Sender, EventArgs Args)
        {
            ReflectCheckBox();
        }

        private void AddArgumets()
        {
            Tlp_Arguments.SuspendLayout();

            // 項目を追加
            Tlp_Arguments.Controls.RemoveAt(Tlp_Arguments.Controls.Count - 1);
            Tlp_Arguments.Controls.RemoveAt(Tlp_Arguments.Controls.Count - 1);
            Tlp_Arguments.RowStyles.RemoveAt(Tlp_Arguments.RowCount - 1);
            Tlp_Arguments.RowCount--;

            Tlp_Arguments.RowCount++;
            TextBox ArgumetsType = new TextBox();
            ArgumetsType.Tag = InputType.String;
            ArgumetsType.ScrollBars = ScrollBars.Horizontal;
            ArgumetsType.BorderStyle = BorderStyle.FixedSingle;
            ArgumetsType.Width = 150;
            TextBox ArgumentsName = new TextBox();
            ArgumentsName.Tag = InputType.String;
            ArgumentsName.ScrollBars = ScrollBars.Horizontal;
            ArgumentsName.BorderStyle = BorderStyle.FixedSingle;
            ArgumentsName.Width = 150;
            ArgumentsName.Enabled = (Cb_IsDynamic.Checked && Cb_IsDynamic.Visible);
            Tlp_Arguments.Controls.AddRange(new TextBox[] { ArgumetsType, ArgumentsName });
            Tlp_Arguments.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));

            // 追加・削除ボタンを作成
            Tlp_Arguments.RowCount++;
            Tlp_Arguments.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            Button AddButton = CreateArgumentsButton(true);
            if (Tlp_Arguments.RowCount - 1 >= 9)
            {
                AddButton.Enabled = false;
            }
            Tlp_Arguments.Controls.Add(AddButton);
            Tlp_Arguments.Controls.Add(CreateArgumentsButton(false));

            Tlp_Arguments.ResumeLayout();
        }

        private void OnAddArgumentsButtonClicked(object Sender, EventArgs Args)
        {
            AddArgumets();
        }

        private void OnRemoveArgumentsButtonClicked(object Sender, EventArgs Args)
        {
            Tlp_Arguments.SuspendLayout();

            // 最後の項目を削除
            for (int Index = 0; Index < 2; Index++) 
            {
                Tlp_Arguments.Controls.RemoveAt(Tlp_Arguments.Controls.Count - 1);
                Tlp_Arguments.Controls.RemoveAt(Tlp_Arguments.Controls.Count - 1);
                Tlp_Arguments.RowStyles.RemoveAt(Tlp_Arguments.RowCount - 1);
                Tlp_Arguments.RowCount--;
            }

            // 追加・削除ボタンを作成
            Tlp_Arguments.RowCount++;
            Tlp_Arguments.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            Button RemoveButton = CreateArgumentsButton(false);
            if (Tlp_Arguments.RowCount - 1 == 0)
            {
                RemoveButton.Enabled = false;
            }
            Tlp_Arguments.Controls.Add(CreateArgumentsButton(true));
            Tlp_Arguments.Controls.Add(RemoveButton);

            Tlp_Arguments.ResumeLayout();
        }

        private Button CreateArgumentsButton(bool bIsAdd)
        {
            Button ArgumentsButton = new Button();

            ArgumentsButton.FlatStyle = FlatStyle.Popup;
            ArgumentsButton.BackColor = Color.FromArgb(70, 70, 70);
            ArgumentsButton.ForeColor = Color.White;
            ArgumentsButton.Width = 150;
            ArgumentsButton.Text = (bIsAdd ? " + Add" : " - Remove");
            ArgumentsButton.Click += (bIsAdd ? new EventHandler(OnAddArgumentsButtonClicked) : new EventHandler(OnRemoveArgumentsButtonClicked));

            return ArgumentsButton;
        }

        private void OnDocumentLinkClicked(object Sender, LinkLabelLinkClickedEventArgs Args)
        {
            if (!string.IsNullOrEmpty(DocumentLink))
            {
                System.Diagnostics.Process.Start(DocumentLink);
            }
        }

        private void OnOKButtonClicked(object Sender, EventArgs Args)
        {
            // マクロ名を構成
            MacroName = "DECLARE";
            if (Cb_IsDynamic.Checked && Cb_IsDynamic.Visible) 
            {
                MacroName += "_DYNAMIC";
            }
            if (Cb_IsMulticast.Checked && Cb_IsMulticast.Visible) 
            {
                MacroName += "_MULTICAST";
            }
            if (Cb_IsEvent.Checked && Cb_IsEvent.Visible)
            {
                MacroName += "_EVENT";
            }
            else
            {
                MacroName += "_DELEGATE";
            }
            if (Cb_HasRetVal.Checked && Cb_HasRetVal.Visible) 
            {
                MacroName += "_RetVal";
            }
            if(Tlp_Arguments.RowCount > 1)
            {
                MacroName += "_" + ParamNumbers[Tlp_Arguments.RowCount - 2];
            }

            // マクロの中身を連結
            MacroString = MacroName + "(";

            if((Cb_IsEvent.Checked && Cb_IsEvent.Visible) || (Cb_HasRetVal.Checked && Cb_HasRetVal.Visible))
            {
                MacroString += Tb_Type.Text + ",";
            }

            // Delegate名の先頭はFでないとコンパイルエラーになる
            if(!(Cb_IsEvent.Checked && Cb_IsEvent.Visible) && Tb_Name.Text.Length > 0 && Tb_Name.Text[0] != 'F')
            {
                Tb_Name.Text = "F" + Tb_Name.Text;
            }
            MacroString += Tb_Name.Text;

            var Controls = Tlp_Arguments.Controls;
            foreach(var Control in Controls)
            {
                if(Control is TextBox TextBox && TextBox.Enabled)
                {
                    MacroString += "," + TextBox.Text;
                }
            }

            MacroString += ")";

            if (string.IsNullOrEmpty(EditTarget))
            {
                MacroString += ";";
            }
        }
    }
}
