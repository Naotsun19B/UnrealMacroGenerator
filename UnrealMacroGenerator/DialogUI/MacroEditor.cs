using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace UnrealMacroGenerator.DialogUI
{
    public partial class MacroEditor : Form
    {
        public string MacroString { get; private set; }
        private string MacroName = string.Empty;

        public MacroEditor(string MacroType)
        {
            InitializeComponent();
            MacroName = MacroType;
            Lbl_EditingMacroName.Text = "Open " + MacroName + " documentation for : ";

            InitializeList(MacroType);
        }

        public MacroEditor(string MacroType, string EditTarget)
        {
            InitializeComponent();
            InitializeList(MacroType);
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
                Label Title = new Label();
                Title.Text = MetaSpecifier.Data;
                Title.ForeColor = Color.White;
                Title.Margin = new Padding(3, 5, 3, 0);
                Title.AutoSize = true;

                Tlp_MetaSpecifiers.RowCount++;
                Tlp_MetaSpecifiers.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
                Tlp_MetaSpecifiers.Controls.Add(Title);

                if(MetaSpecifier.Type == InputType.Specifier)
                {
                    CheckBox Input = new CheckBox();
                    Input.Tag = InputType.Specifier;
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
                    Control Input = Tlp_MetaSpecifiers.GetControlFromPosition(1, Index);
                    InputType Tag = (InputType)Input.Tag;

                    // Specifier
                    if (Tag == InputType.Specifier)
                    {
                        CheckBox Specifier = (CheckBox)Input;
                        if (Specifier.Checked)
                        {
                            MetaSpecifiersString += Name.Text + ", ";
                        }
                    }
                    // TextBox
                    else if (Tag == InputType.TextBox)
                    {
                        TextBox TextBox = (TextBox)Input;
                        if (!string.IsNullOrWhiteSpace(TextBox.Text) && !string.IsNullOrEmpty(TextBox.Text))
                        {
                            MetaSpecifiersString += Name.Text + " = \"" + TextBox.Text + "\", ";
                        }
                    }
                    // CheckBox
                    else if (Tag == InputType.CheckBox)
                    {
                        CheckBox CheckBox = (CheckBox)Input;
                        if (CheckBox.Checked)
                        {
                            MetaSpecifiersString += Name.Text + " = true, ";
                        }
                    }
                    // NumericUpDown
                    else if (Tag == InputType.NumericUpDown)
                    {
                        NumericUpDown NumericUpDown = (NumericUpDown)Input;
                        if (!string.IsNullOrWhiteSpace(NumericUpDown.Text) && !string.IsNullOrEmpty(NumericUpDown.Text))
                        {
                            MetaSpecifiersString += Name.Text + " = " + NumericUpDown.Text + ", ";
                        }
                    }
                    // NumericUpDownFloat
                    else if (Tag == InputType.NumericUpDownFloat)
                    {
                        NumericUpDown NumericUpDownFloat = (NumericUpDown)Input;
                        if (!string.IsNullOrWhiteSpace(NumericUpDownFloat.Text) && !string.IsNullOrEmpty(NumericUpDownFloat.Text))
                        {
                            MetaSpecifiersString += Name.Text + " = " + NumericUpDownFloat.Text + ", ";
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
                MacroString += "meta = (" + MetaSpecifiersString + ")";
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
    }
}
