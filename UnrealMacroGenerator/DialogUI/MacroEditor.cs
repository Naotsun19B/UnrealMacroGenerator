using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace UnrealMacroGenerator.DialogUI
{
    public partial class MacroEditor : Form
    {
        public string MacroString { get; private set; }

        public MacroEditor(string MacroType)
        {
            InitializeComponent();
            MacroString = MacroType;
            Lbl_EditingMacroName.Text += MacroType;

            InitializeList(MacroType);
        }

        public MacroEditor(string MacroType, string EditTarget)
        {
            InitializeComponent();
            InitializeList(MacroType);
        }

        private void InitializeList(string MacroType)
        {
            MacroTableData TableData = XmlFunctionLibrary.GetMacroTableData(MacroType);
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
                Tlp_AdvancedSettings.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
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
                Label Title = new Label();
                Title.Text = MetaSpecifier.Data;
                Title.ForeColor = Color.White;
                Title.Margin = new Padding(3, 5, 3, 0);
                Title.AutoSize = true;

                Tlp_MetaSpecifiers.RowCount++;
                Tlp_MetaSpecifiers.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
                Tlp_MetaSpecifiers.Controls.Add(Title);

                if(MetaSpecifier.Type == InputType.Specifier)
                {
                    CheckBox Input = new CheckBox();
                    Input.TextAlign = ContentAlignment.BottomCenter; //Specifier
                    Tlp_MetaSpecifiers.Controls.Add(Input);
                }
                else if(MetaSpecifier.Type == InputType.TextBox)
                {
                    TextBox Input = new TextBox();
                    Input.ScrollBars = ScrollBars.Horizontal;
                    Input.BorderStyle = BorderStyle.FixedSingle;
                    Tlp_MetaSpecifiers.Controls.Add(Input);
                }
                else if (MetaSpecifier.Type == InputType.CheckBox)
                {
                    CheckBox Input = new CheckBox();
                    Input.TextAlign = ContentAlignment.BottomLeft; //CheckBox
                    Tlp_MetaSpecifiers.Controls.Add(Input);
                }
                else if (MetaSpecifier.Type == InputType.NumericUpDown)
                {
                    NumericUpDown Input = new NumericUpDown();
                    Input.Text = string.Empty;
                    Tlp_MetaSpecifiers.Controls.Add(Input);
                }
                else if (MetaSpecifier.Type == InputType.NumericUpDownFloat)
                {
                    NumericUpDown Input = new NumericUpDown();
                    Input.DecimalPlaces = 2;
                    Input.Text = string.Empty;
                    Tlp_MetaSpecifiers.Controls.Add(Input);
                }
            }

            Tlp_MetaSpecifiers.ResumeLayout();
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
            for(int Index = 0; Index < Tlp_AdvancedSettings.RowCount; Index++)
            {
                TextBox Input = Tlp_AdvancedSettings.GetControlFromPosition(1, Index) as TextBox;
                if (Input != null && !string.IsNullOrWhiteSpace(Input.Text) && !string.IsNullOrEmpty(Input.Text))
                {
                    Label Name = Tlp_AdvancedSettings.GetControlFromPosition(0, Index) as Label;
                    if(Name != null)
                    {
                        AdvancedSettingsString += Name.Text + " = \"" + Input.Text + "\", ";
                    }
                }
            }

            // メタ指定子の連結
            string MetaSpecifiersString = string.Empty;
            for (int Index = 0; Index < Tlp_MetaSpecifiers.RowCount; Index++)
            {
                Label Name = Tlp_MetaSpecifiers.GetControlFromPosition(0, Index) as Label;
                if (Name != null)
                {
                    // Specifier
                    CheckBox Specifier = Tlp_MetaSpecifiers.GetControlFromPosition(1, Index) as CheckBox;
                    if (Specifier != null && Specifier.TextAlign == ContentAlignment.BottomCenter)
                    {
                        if (Specifier.Checked)
                        {
                            MetaSpecifiersString += Name.Text + ", ";
                        }
                    }
                    // TextBox
                    TextBox TextBox = Tlp_MetaSpecifiers.GetControlFromPosition(1, Index) as TextBox;
                    if (TextBox != null)
                    {
                        if (!string.IsNullOrWhiteSpace(TextBox.Text) && !string.IsNullOrEmpty(TextBox.Text))
                        {
                            MetaSpecifiersString += Name.Text + " = \"" + TextBox.Text + "\", ";
                        }
                    }
                    // CheckBox
                    CheckBox CheckBox = Tlp_MetaSpecifiers.GetControlFromPosition(1, Index) as CheckBox;
                    if (CheckBox != null && CheckBox.TextAlign == ContentAlignment.BottomLeft)
                    {
                        if (CheckBox.Checked)
                        {
                            MetaSpecifiersString += Name.Text + " = true, ";
                        }
                    }
                    // NumericUpDown
                    NumericUpDown NumericUpDown = Tlp_MetaSpecifiers.GetControlFromPosition(1, Index) as NumericUpDown;
                    if (NumericUpDown != null && NumericUpDown.DecimalPlaces == 0)
                    {
                        if (!string.IsNullOrWhiteSpace(NumericUpDown.Text) && !string.IsNullOrEmpty(NumericUpDown.Text))
                        {
                            MetaSpecifiersString += Name.Text + " = " + NumericUpDown.Text + ", ";
                        }
                    }
                    // NumericUpDownFloat
                    NumericUpDown NumericUpDownFloat = Tlp_MetaSpecifiers.GetControlFromPosition(1, Index) as NumericUpDown;
                    if (NumericUpDownFloat != null && NumericUpDownFloat.DecimalPlaces > 0)
                    {
                        if (!string.IsNullOrWhiteSpace(NumericUpDownFloat.Text) && !string.IsNullOrEmpty(NumericUpDownFloat.Text))
                        {
                            MetaSpecifiersString += Name.Text + " = " + NumericUpDownFloat.Text + ", ";
                        }
                    }
                }
            }
            MetaSpecifiersString = MetaSpecifiersString.TrimEnd(',', ' ');
            
            // 全て連結させてマクロを完成させる
            MacroString += "(";
            MacroString += MacroSpecifirsString + AdvancedSettingsString;
            if(!string.IsNullOrWhiteSpace(MetaSpecifiersString) && !string.IsNullOrEmpty(MetaSpecifiersString))
            {
                MacroString += "meta = (" + MetaSpecifiersString + ")";
            }
            else
            {
                MacroString = MacroString.TrimEnd(',', ' ');
            }
            MacroString += ")";
        }
    }
}
