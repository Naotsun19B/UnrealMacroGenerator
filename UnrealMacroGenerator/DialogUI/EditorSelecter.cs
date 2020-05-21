using System;
using System.Drawing;
using System.Windows.Forms;

namespace UnrealMacroGenerator.DialogUI
{
    public partial class EditorSelecter : Form
    {
        public string MacroType { get; private set; }

        public EditorSelecter()
        {
            InitializeComponent();
        }

        private void OnEditorLoad(object Sender, EventArgs Args)
        {
            Lb_Selecter.Items.AddRange(XmlFunctionLibrary.GetMacroTypes(true, false, false));
            Lb_Selecter.SelectedIndex = 0;

            InitializeEditorColor();
        }

        private void InitializeEditorColor()
        {
            Color MainFrameColor = SettingsFunctionLibrary.GetMainFrameColor();
            Color BackgroundColor = SettingsFunctionLibrary.GetBackgroundColor();
            Color TextColor = SettingsFunctionLibrary.GetTextColor();

            Lb_Selecter.BackColor = MainFrameColor;
            Btn_OK.BackColor = MainFrameColor;
            Btn_Cancel.BackColor = MainFrameColor;
            this.BackColor = BackgroundColor;
            Lb_Selecter.ForeColor = TextColor;
            Btn_OK.ForeColor = TextColor;
            Btn_Cancel.ForeColor = TextColor;
        }

        private void OnSelectedIndexChanged(object Sender, EventArgs Args)
        {
            MacroType = Lb_Selecter.SelectedItem.ToString();
        }
    }
}
