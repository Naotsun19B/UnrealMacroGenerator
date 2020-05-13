using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace UnrealMacroGenerator.DialogUI
{
    public partial class MacroEditor : Form
    {
        // 結果保存用
        public string MacroString { get; private set; }

        // マクロの種類
        private string MacroName = string.Empty;

        // ドキュメントのURL
        private string DocumentLink = string.Empty;

        // テンプレート
        private string TemplateString = string.Empty;

        // 編集モードで開いたか
        private bool bIsEditMode = false;

        // パラメータ名とUIの対応表
        private Dictionary<string, int> CachedMacroSpecifiersUI = new Dictionary<string, int>();
        private Dictionary<string, TextBox> CachedAdvancedSettingsUI = new Dictionary<string, TextBox>();
        private Dictionary<string, Control> CachedMetaSpecifiersUI = new Dictionary<string, Control>();

        // 詳細指定子とメタ指定子のリストの最小項目数
        private const int AdvancedSettingsMin = 5;
        private const int MetaSpecifiersMin = 11;

        public MacroEditor(string MacroType, string EditTarget = null)
        {
            InitializeComponent();

            // 初期化
            MacroName = MacroType;
            Llbl_Document.Text = "Open " + MacroName + " document";

            DocumentLink = XmlFunctionLibrary.GetDocumentationLink(MacroType);
            if (DocumentLink == string.Empty)
            {
                DocumentLink = XmlFunctionLibrary.GetDocumentationLink("Meta");
            }

            InitializeList(MacroType);

            // 編集モードならパラメータをUIに反映させる
            if(EditTarget != null)
            {
                bIsEditMode = true;
                ReflectParameterInList(EditTarget);
            }

            // テンプレートのチェックボックスの設定
            TemplateString = XmlFunctionLibrary.GetTemplateString(MacroName);
            if(string.IsNullOrEmpty(TemplateString) || bIsEditMode)
            {
                Cb_WithTemplate.Enabled = false;
                Cb_WithTemplate.Checked = false;
                Cb_WithTemplate.Visible = false;
            }
        }

        private void InitializeList(string MacroType)
        {
            MacroSpecifierData TableData = XmlFunctionLibrary.GetMacroSpecifierData(MacroType);

            // 通常指定子のリストを初期化
            Cl_MacroSpecifiers.Items.AddRange(TableData.MacroSpecifiers);
            for(int Index = 0; Index < TableData.MacroSpecifiers.Length; Index++)
            {
                CachedMacroSpecifiersUI.Add(TableData.MacroSpecifiers[Index], Index);
            }

            // 詳細指定子のリストを初期化
            Tlp_AdvancedSettings.Dock = DockStyle.Top;
            Tlp_AdvancedSettings.SuspendLayout();
            Tlp_AdvancedSettings.RowCount = 0;
            Tlp_AdvancedSettings.RowStyles.Clear();
            Tlp_AdvancedSettings.AutoSize = true;
            Tlp_AdvancedSettings.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Tlp_AdvancedSettings.Padding = new Padding(0, 0, SystemInformation.VerticalScrollBarWidth, 0);

            foreach (var AdvancedSetting in TableData.AdvancedSettings)
            {
                Label Title = new Label();
                Title.Text = AdvancedSetting;
                Title.ForeColor = Color.White;
                Title.Margin = new Padding(3, 5, 3, 0);
                Title.AutoSize = true;

                TextBox Input = new TextBox();
                Input.ScrollBars = ScrollBars.Horizontal;
                Input.BorderStyle = BorderStyle.FixedSingle;

                Tlp_AdvancedSettings.RowCount++;
                Tlp_AdvancedSettings.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
                Tlp_AdvancedSettings.Controls.Add(Title);
                Tlp_AdvancedSettings.Controls.Add(Input);

                CachedAdvancedSettingsUI.Add(AdvancedSetting, Input);
            }
            // 項目が少なかった時の埋め合わせ
            if(Tlp_AdvancedSettings.RowCount < AdvancedSettingsMin)
            {
                int Count = AdvancedSettingsMin - Tlp_AdvancedSettings.RowCount;
                for (int Row = 0; Row < Count; Row++)
                {
                    Tlp_AdvancedSettings.RowCount++;
                    Tlp_AdvancedSettings.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
                }
            }

            Tlp_AdvancedSettings.ResumeLayout();

            // メタ指定子のリストを初期化
            Tlp_MetaSpecifiers.Dock = DockStyle.Top;
            Tlp_MetaSpecifiers.SuspendLayout();
            Tlp_MetaSpecifiers.RowCount = 0;
            Tlp_MetaSpecifiers.RowStyles.Clear();
            Tlp_MetaSpecifiers.AutoSize = true;
            Tlp_MetaSpecifiers.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Tlp_MetaSpecifiers.Padding = new Padding(0, 0, SystemInformation.VerticalScrollBarWidth, 0);

            foreach (var MetaSpecifier in TableData.MetaSpecifiers)
            {
                Label Name = new Label();
                Name.Text = MetaSpecifier.Data;
                Name.ForeColor = Color.White;
                Name.Margin = new Padding(3, 5, 3, 0);
                Name.AutoSize = true;

                Tlp_MetaSpecifiers.RowCount++;
                Tlp_MetaSpecifiers.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
                Tlp_MetaSpecifiers.Controls.Add(Name);

                if(MetaSpecifier.Type == InputType.Specifier)
                {
                    CheckBox Input = new CheckBox();
                    Input.Tag = InputType.Specifier;
                    Name.Tag = Input;
                    Name.Click += new EventHandler(OnCheckBoxLabelClicked);
                    Tlp_MetaSpecifiers.Controls.Add(Input);
                    CachedMetaSpecifiersUI.Add(MetaSpecifier.Data, Input);
                }
                else if(MetaSpecifier.Type == InputType.TextBox)
                {
                    TextBox Input = new TextBox();
                    Input.Tag = InputType.TextBox;
                    Input.ScrollBars = ScrollBars.Horizontal;
                    Input.BorderStyle = BorderStyle.FixedSingle;
                    Tlp_MetaSpecifiers.Controls.Add(Input);
                    CachedMetaSpecifiersUI.Add(MetaSpecifier.Data, Input);
                }
                else if (MetaSpecifier.Type == InputType.CheckBox)
                {
                    CheckBox Input = new CheckBox();
                    Input.Tag = InputType.CheckBox;
                    Name.Tag = Input;
                    Name.Click += new EventHandler(OnCheckBoxLabelClicked);
                    Tlp_MetaSpecifiers.Controls.Add(Input);
                    CachedMetaSpecifiersUI.Add(MetaSpecifier.Data, Input);
                }
                else if (MetaSpecifier.Type == InputType.NumericUpDown)
                {
                    NumericUpDown Input = new NumericUpDown();
                    Input.Tag = InputType.NumericUpDown;
                    Input.BorderStyle = BorderStyle.FixedSingle;
                    Input.Text = string.Empty;
                    Tlp_MetaSpecifiers.Controls.Add(Input);
                    CachedMetaSpecifiersUI.Add(MetaSpecifier.Data, Input);
                }
                else if (MetaSpecifier.Type == InputType.NumericUpDownFloat)
                {
                    NumericUpDown Input = new NumericUpDown();
                    Input.Tag = InputType.NumericUpDownFloat;
                    Input.BorderStyle = BorderStyle.FixedSingle;
                    Input.DecimalPlaces = 2;
                    Input.Text = string.Empty;
                    Input.Increment = (decimal)0.5;
                    Tlp_MetaSpecifiers.Controls.Add(Input);
                    CachedMetaSpecifiersUI.Add(MetaSpecifier.Data, Input);
                }
            }
            // 項目が少なかった時の埋め合わせ
            if (Tlp_MetaSpecifiers.RowCount < MetaSpecifiersMin)
            {
                int Count = MetaSpecifiersMin - Tlp_MetaSpecifiers.RowCount;
                for (int Row = 0; Row < Count; Row++)
                {
                    Tlp_MetaSpecifiers.RowCount++;
                    Tlp_MetaSpecifiers.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
                }
            }

            Tlp_MetaSpecifiers.ResumeLayout();
        }

        private void ReflectParameterInList(string EditTarget)
        {
            // カッコと空白を取り除く
            string TrimmedTarget = string.Empty;
            bool bIsInString = false;
            for (int Index = 0; Index < EditTarget.Length; Index++)
            {
                if (EditTarget[Index] == '\"')
                {
                    bIsInString = !bIsInString;
                }

                if (bIsInString || (EditTarget[Index] != '(' && EditTarget[Index] != ')' && EditTarget[Index] != ' '))
                {
                    TrimmedTarget += EditTarget[Index];
                }
            }
            // 空のマクロなら初期状態でUIを起動
            if(string.IsNullOrEmpty(TrimmedTarget))
            {
                return;
            }

            // カンマで分ける
            List<string> ParsedParameters = SplitParameterByComma(TrimmedTarget);

            // meta=を取り除く
            for (int Index = 0; Index < ParsedParameters.Count; Index++)
            {
                if (ParsedParameters[Index].Length > 5)
                {
                    string Head = ParsedParameters[Index].Substring(0, 5);
                    if (Head == "meta=" || Head == "Meta=")
                    {
                        ParsedParameters[Index] = ParsedParameters[Index].Remove(0, 5);
                    }
                }
            }

            // UIに反映させる
            foreach (var Parameter in ParsedParameters)
            {
                if (!ReflectParameterInMacroSpecifiers(Parameter) &&
                    !ReflectParameterInAdvancedSettings(Parameter) &&
                    !ReflectParameterInMetaSpecifiers(Parameter)) 
                {
                    MessageBox.Show(
                            "\"" + Parameter + "\" is an illegal specifier",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                            );
                    Close();
                }
            }
        }

        private bool ReflectParameterInMacroSpecifiers(string Parameter)
        {
            // 項目の文字列と一致したらチェックする
            try
            {
                int Index = CachedMacroSpecifiersUI[Parameter];
                Cl_MacroSpecifiers.SetItemChecked(Index, true);
            }
            catch (KeyNotFoundException)
            {
                return false;
            }

            return true;
        }

        private bool ReflectParameterInAdvancedSettings(string Parameter)
        {
            // 名前と値を分ける
            string Name = string.Empty;
            string Value = string.Empty;
            SplitSpecifir(Parameter, out Name, out Value);

            try
            {
                TextBox Input = CachedAdvancedSettingsUI[Name];
                Input.Text = Value;
            }
            catch (KeyNotFoundException)
            {
                return false;
            }

            return true;
        }

        private bool ReflectParameterInMetaSpecifiers(string Parameter)
        {
            // 名前と値を分ける
            string Name = string.Empty;
            string Value = string.Empty;
            SplitSpecifir(Parameter, out Name, out Value);

            try
            {
                if (CachedMetaSpecifiersUI[Name] is Control Input && Input.Tag is InputType Type)
                {
                    if (Type == InputType.Specifier && Input is CheckBox Specifier)
                    {
                        Specifier.Checked = true;
                    }
                    else if (Type == InputType.TextBox && Input is TextBox TextBox)
                    {
                        TextBox.Text = Value;
                    }
                    else if (Type == InputType.CheckBox && Input is CheckBox CheckBox)
                    {
                        CheckBox.Checked = true;
                    }
                    else if (Type == InputType.NumericUpDown && Input is NumericUpDown NumericUpDown)
                    {
                        NumericUpDown.Text = Value;
                    }
                    else if (Type == InputType.NumericUpDownFloat && Input is NumericUpDown NumericUpDownFloat)
                    {
                        NumericUpDownFloat.Text = Value;
                    }
                }
            }
            catch (KeyNotFoundException)
            {
                return false;
            }

            return true;
        }

        private void OnOKButtonClicked(object Sender, EventArgs Args)
        {
            // 通常指定子の連結
            string MacroSpecifirsString = string.Empty;
            var CheckedItems = Cl_MacroSpecifiers.CheckedItems;
            foreach(var Item in CheckedItems)
            {
                MacroSpecifirsString += Item.ToString() + ", ";
            }

            // 詳細指定子の連結
            string AdvancedSettingsString = string.Empty;
            for(int Row = 0; Row < Tlp_AdvancedSettings.RowCount; Row++)
            {
                TextBox Input = Tlp_AdvancedSettings.GetControlFromPosition(1, Row) as TextBox;
                if (Input != null && !string.IsNullOrWhiteSpace(Input.Text) && !string.IsNullOrEmpty(Input.Text))
                {
                    Label Name = Tlp_AdvancedSettings.GetControlFromPosition(0, Row) as Label;
                    if(Name != null)
                    {
                        AdvancedSettingsString += Name.Text + "=\"" + Input.Text + "\",";
                    }
                }
            }

            // メタ指定子の連結
            string MetaSpecifiersString = string.Empty;
            for (int Row = 0; Row < Tlp_MetaSpecifiers.RowCount; Row++)
            {
                Label Name = Tlp_MetaSpecifiers.GetControlFromPosition(0, Row) as Label;
                if (Name != null)
                {
                    Control Input = Tlp_MetaSpecifiers.GetControlFromPosition(1, Row);
                    InputType Tag = (InputType)Input.Tag;

                    // Specifier
                    if (Tag == InputType.Specifier && Input is CheckBox Specifier)
                    {
                        if (Specifier.Checked)
                        {
                            MetaSpecifiersString += Name.Text + ",";
                        }
                    }
                    // TextBox
                    else if (Tag == InputType.TextBox && Input is TextBox TextBox)
                    {
                        if (!string.IsNullOrWhiteSpace(TextBox.Text) && !string.IsNullOrEmpty(TextBox.Text))
                        {
                            MetaSpecifiersString += Name.Text + "=\"" + TextBox.Text + "\",";
                        }
                    }
                    // CheckBox
                    else if (Tag == InputType.CheckBox && Input is CheckBox CheckBox)
                    {
                        if (CheckBox.Checked)
                        {
                            MetaSpecifiersString += Name.Text + "=true,";
                        }
                    }
                    // NumericUpDown
                    else if (Tag == InputType.NumericUpDown && Input is NumericUpDown NumericUpDown)
                    { 
                        if (!string.IsNullOrWhiteSpace(NumericUpDown.Text) && !string.IsNullOrEmpty(NumericUpDown.Text))
                        {
                            MetaSpecifiersString += Name.Text + "=" + NumericUpDown.Text + ",";
                        }
                    }
                    // NumericUpDownFloat
                    else if (Tag == InputType.NumericUpDownFloat && Input is NumericUpDown NumericUpDownFloat)
                    {
                        if (!string.IsNullOrWhiteSpace(NumericUpDownFloat.Text) && !string.IsNullOrEmpty(NumericUpDownFloat.Text))
                        {
                            MetaSpecifiersString += Name.Text + "=" + NumericUpDownFloat.Text + ",";
                        }
                    }
                }
            }
            MetaSpecifiersString = MetaSpecifiersString.TrimEnd(',', ' ');
            
            // 全て連結させてマクロを完成させる
            MacroString += MacroName + "(";
            MacroString += MacroSpecifirsString + AdvancedSettingsString;
            if(!string.IsNullOrWhiteSpace(MetaSpecifiersString) && !string.IsNullOrEmpty(MetaSpecifiersString))
            {
                MacroString += "meta=(" + MetaSpecifiersString + ")";
            }
            else
            {
                MacroString = MacroString.TrimEnd(',', ' ');
            }
            MacroString += ")";

            // 生成モードならテンプレートもつける
            if(!bIsEditMode && Cb_WithTemplate.Checked && !string.IsNullOrEmpty(TemplateString))
            {
                MacroString += TemplateString;
            }
        }

        private void OnCheckBoxLabelClicked(object Sender, EventArgs Args)
        {
            if(Sender is Label Name)
            {
                if(Name.Tag is CheckBox Input)
                {
                    Input.Checked = !Input.Checked;
                }
            }
        }

        private void SplitSpecifir(string Specifir, out string Name, out string Value)
        {
            var Split = Specifir.Split('=');
            Name = Split[0];
            Value = string.Empty;

            if (Split.Length >= 2)
            {
                Value = Split[1];
            }
            
            // 文字列中の=で分割してしまった場合
            if (Split.Length >= 3)
            {
                for (int Index = 2; Index < Split.Length; Index++)
                {
                    Value += "=" + Split[Index];
                }
            }

            // Valueの前後の"を取り除く
            if (Split.Length >= 2)
            {
                Value = Value.TrimStart('\"');
                Value = Value.TrimEnd('\"');
            }
        }

        private List<string> SplitParameterByComma(string Parameter)
        {
            // 文字列中のカンマを無視してカンマで分割する
            List<string> Splited = new List<string>();

            bool bIsInString = false;
            int PrevCommaIndex = -1;
            for (int Index = 0; Index < Parameter.Length; Index++)
            {
                if(Parameter[Index] == '\"')
                {
                    bIsInString = !bIsInString;
                }

                if(!bIsInString && Parameter[Index] == ',')
                {
                    Splited.Add(Parameter.Substring(PrevCommaIndex + 1, Index - PrevCommaIndex - 1));
                    PrevCommaIndex = Index;
                }
            }

            if (PrevCommaIndex > -1)
            {
                for (int Index = Parameter.Length - 1; Index > 0; Index--)
                {
                    if (Parameter[Index] == '\"')
                    {
                        bIsInString = !bIsInString;
                    }

                    if (!bIsInString && Parameter[Index] == ',')
                    {
                        Splited.Add(Parameter.Substring(Index + 1, Parameter.Length - Index - 1));
                        break;
                    }
                }
            }
            // 項目が1つの場合
            else
            {
                Splited.Add(Parameter);
            }

            return Splited;
        }

        private void OnDocumentLinkClicked(object Sender, LinkLabelLinkClickedEventArgs Args)
        {
            System.Diagnostics.Process.Start(DocumentLink);
        }
    }
}
