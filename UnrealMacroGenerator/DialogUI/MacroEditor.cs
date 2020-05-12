using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace UnrealMacroGenerator.DialogUI
{
    public partial class MacroEditor : Form
    {
        public string MacroString { get; private set; }
        private string MacroName = string.Empty;

        public MacroEditor(string MacroType, string EditTarget = null)
        {
            InitializeComponent();

            // 初期化
            MacroName = MacroType;
            Lbl_EditingMacroName.Text = "Open " + MacroName + " documentation for : ";
            InitializeList(MacroType);

            // 編集モードならパラメータをUIに反映させる
            if(EditTarget != null)
            {
                ReflectParameterInList(EditTarget);
            }
        }

        private void InitializeList(string MacroType)
        {
            MacroSpecifierData TableData = XmlFunctionLibrary.GetMacroSpecifierData(MacroType);
            Cl_MacroSpecifiers.Items.AddRange(TableData.MacroSpecifiers);

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
            }

            Tlp_AdvancedSettings.ResumeLayout(); 

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
                }
                else if(MetaSpecifier.Type == InputType.TextBox)
                {
                    TextBox Input = new TextBox();
                    Input.Tag = InputType.TextBox;
                    Input.ScrollBars = ScrollBars.Horizontal;
                    Input.BorderStyle = BorderStyle.FixedSingle;
                    Tlp_MetaSpecifiers.Controls.Add(Input);
                }
                else if (MetaSpecifier.Type == InputType.CheckBox)
                {
                    CheckBox Input = new CheckBox();
                    Input.Tag = InputType.CheckBox;
                    Name.Tag = Input;
                    Name.Click += new EventHandler(OnCheckBoxLabelClicked);
                    Tlp_MetaSpecifiers.Controls.Add(Input);
                }
                else if (MetaSpecifier.Type == InputType.NumericUpDown)
                {
                    NumericUpDown Input = new NumericUpDown();
                    Input.Tag = InputType.NumericUpDown;
                    Input.BorderStyle = BorderStyle.FixedSingle;
                    Input.Text = string.Empty;
                    Tlp_MetaSpecifiers.Controls.Add(Input);
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
            // 制御文字を取り除く
            TrimmedTarget = new string(TrimmedTarget.Where(Ch => !char.IsControl(Ch)).ToArray());

            // ,で分ける
            List<string> ParsedParameters = new List<string>(TrimmedTarget.Split(','));

            // カテゴリ分け
            List<string> MacroSpecifirs = new List<string>();
            List<string> AdvancedSettings = new List<string>();
            List<string> MetaSpecifiers = new List<string>();
            bool bIsMacroSpecifirs = false;
            foreach (var Parameter in ParsedParameters)
            {
                // 通常指定子と詳細指定子
                if(!bIsMacroSpecifirs)
                {
                    // メタ指定子かを判定
                    if(Parameter.Contains("meta=") || Parameter.Contains("Meta="))
                    {
                        bIsMacroSpecifirs = true;
                    }
                    // =を含んでいたら詳細指定子
                    else if(Parameter.Contains("="))
                    {
                        AdvancedSettings.Add(Parameter);
                    }
                    // 何も該当しなければ通常指定子
                    else
                    {
                        MacroSpecifirs.Add(Parameter);
                    }
                }
                // メタ指定子
                if (bIsMacroSpecifirs)
                {
                    MetaSpecifiers.Add(Parameter);
                }
            }
            // meta=を取り除く
            if(MetaSpecifiers.Count > 0)
            {
                MetaSpecifiers[0] = MetaSpecifiers[0].Remove(0, 5);
            }

            // UIに反映させる
            if(MacroSpecifirs.Count > 0)
            {
                ReflectParameterInMacroSpecifiers(MacroSpecifirs.ToArray());
            }
            if(AdvancedSettings.Count > 0)
            {
                ReflectParameterInAdvancedSettings(AdvancedSettings.ToArray());
            }
            if(MetaSpecifiers.Count > 0)
            {
                ReflectParameterInMetaSpecifiers(MetaSpecifiers.ToArray());
            }
        }

        private void ReflectParameterInMacroSpecifiers(string[] Parameters)
        {
            foreach (var Parameter in Parameters)
            {
                for (int Index = 0; Index < Cl_MacroSpecifiers.Items.Count; Index++)
                {
                    // 項目の文字列と一致したらチェックする
                    if (Cl_MacroSpecifiers.Items[Index] is string Name && Name == Parameter)
                    {
                        Cl_MacroSpecifiers.SetItemChecked(Index, true);
                    }
                }
            }
        }

        private void ReflectParameterInAdvancedSettings(string[] Parameters)
        {
            foreach (var Parameter in Parameters)
            {
                // 名前と値を分ける
                string Name = string.Empty;
                string Value = string.Empty;
                SplitParameter(Parameter, out Name, out Value);

                for (int Row = 0; Row < Tlp_AdvancedSettings.RowCount; Row++)
                {
                    if (Tlp_AdvancedSettings.GetControlFromPosition(0, Row) is Label Label)
                    {
                        if (Label.Text == Name)
                        {
                            if (Tlp_AdvancedSettings.GetControlFromPosition(1, Row) is TextBox TextBox)
                            {
                                TextBox.Text = Value;
                            }
                        }
                    }
                }
            }
        }

        private void ReflectParameterInMetaSpecifiers(string[] Parameters)
        {
            foreach (var Parameter in Parameters)
            {
                // 名前と値を分ける
                string Name = string.Empty;
                string Value = string.Empty;
                SplitParameter(Parameter, out Name, out Value);

                bool bIsFound = false;
                for (int Row = 0; Row < Tlp_MetaSpecifiers.RowCount; Row++)
                {
                    if (Tlp_MetaSpecifiers.GetControlFromPosition(0, Row) is Label Label)
                    {
                        if (Label.Text == Name)
                        {
                            bIsFound = true;
                            if (Tlp_MetaSpecifiers.GetControlFromPosition(1, Row) is Control Input &&
                                Input.Tag is InputType Type)
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
                    }
                }

                // メタ指定子の後ろの通常指定子や詳細指定子がある場合
                if(!bIsFound)
                {
                    ReflectParameterInMacroSpecifiers(new string[] { Parameter });
                    ReflectParameterInAdvancedSettings(new string[] { Parameter });
                }
            }
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
        }

        private void OnSpecifierLinkClicked(object Sender, LinkLabelLinkClickedEventArgs Args)
        {
            System.Diagnostics.Process.Start(XmlFunctionLibrary.GetDocumentationLink(MacroName));
        }

        private void OnMetaLinkClicked(object Sender, LinkLabelLinkClickedEventArgs Args)
        {
            System.Diagnostics.Process.Start(XmlFunctionLibrary.GetDocumentationLink("Meta"));
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

        private void SplitParameter(string Parameter, out string Name, out string Value)
        {
            var Split = Parameter.Split('=');
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
    }
}
