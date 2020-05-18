using System;
using System.Windows.Forms;

namespace UnrealMacroGenerator.DialogUI
{
    public partial class EditorSelecter : Form
    {
        public string MacroType { get; private set; }

        public EditorSelecter()
        {
            InitializeComponent();
            
            Lb_Selecter.Items.AddRange(XmlFunctionLibrary.GetMacroTypes(true, false, false));
            Lb_Selecter.SelectedIndex = 0;
        }

        private void OnSelectedIndexChanged(object Sender, EventArgs Args)
        {
            MacroType = Lb_Selecter.SelectedItem.ToString();
        }
    }
}
