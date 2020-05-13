namespace UnrealMacroGenerator.DialogUI
{
    partial class MacroEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Btn_OK = new System.Windows.Forms.Button();
            this.Btn_Cancel = new System.Windows.Forms.Button();
            this.Cl_MacroSpecifiers = new System.Windows.Forms.CheckedListBox();
            this.ScrollPanel_MetaSpecifiers = new System.Windows.Forms.Panel();
            this.Tlp_MetaSpecifiers = new System.Windows.Forms.TableLayoutPanel();
            this.ScrollPanel_AdvancedSettings = new System.Windows.Forms.Panel();
            this.Tlp_AdvancedSettings = new System.Windows.Forms.TableLayoutPanel();
            this.Cb_WithTemplate = new System.Windows.Forms.CheckBox();
            this.Llbl_Document = new System.Windows.Forms.LinkLabel();
            this.Flp_Document = new System.Windows.Forms.FlowLayoutPanel();
            this.ScrollPanel_MetaSpecifiers.SuspendLayout();
            this.ScrollPanel_AdvancedSettings.SuspendLayout();
            this.Flp_Document.SuspendLayout();
            this.SuspendLayout();
            // 
            // Btn_OK
            // 
            this.Btn_OK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.Btn_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Btn_OK.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Btn_OK.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Btn_OK.ForeColor = System.Drawing.Color.White;
            this.Btn_OK.Location = new System.Drawing.Point(452, 419);
            this.Btn_OK.Name = "Btn_OK";
            this.Btn_OK.Size = new System.Drawing.Size(75, 23);
            this.Btn_OK.TabIndex = 1;
            this.Btn_OK.Text = "OK";
            this.Btn_OK.UseVisualStyleBackColor = false;
            this.Btn_OK.Click += new System.EventHandler(this.OnOKButtonClicked);
            // 
            // Btn_Cancel
            // 
            this.Btn_Cancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.Btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Btn_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Btn_Cancel.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Btn_Cancel.ForeColor = System.Drawing.Color.White;
            this.Btn_Cancel.Location = new System.Drawing.Point(542, 419);
            this.Btn_Cancel.Name = "Btn_Cancel";
            this.Btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.Btn_Cancel.TabIndex = 2;
            this.Btn_Cancel.Text = "Cancel";
            this.Btn_Cancel.UseVisualStyleBackColor = false;
            // 
            // Cl_MacroSpecifiers
            // 
            this.Cl_MacroSpecifiers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.Cl_MacroSpecifiers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Cl_MacroSpecifiers.CheckOnClick = true;
            this.Cl_MacroSpecifiers.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Cl_MacroSpecifiers.ForeColor = System.Drawing.Color.White;
            this.Cl_MacroSpecifiers.FormattingEnabled = true;
            this.Cl_MacroSpecifiers.HorizontalScrollbar = true;
            this.Cl_MacroSpecifiers.Location = new System.Drawing.Point(13, 13);
            this.Cl_MacroSpecifiers.Name = "Cl_MacroSpecifiers";
            this.Cl_MacroSpecifiers.Size = new System.Drawing.Size(240, 398);
            this.Cl_MacroSpecifiers.TabIndex = 3;
            // 
            // ScrollPanel_MetaSpecifiers
            // 
            this.ScrollPanel_MetaSpecifiers.AutoScroll = true;
            this.ScrollPanel_MetaSpecifiers.Controls.Add(this.Tlp_MetaSpecifiers);
            this.ScrollPanel_MetaSpecifiers.Location = new System.Drawing.Point(260, 138);
            this.ScrollPanel_MetaSpecifiers.Name = "ScrollPanel_MetaSpecifiers";
            this.ScrollPanel_MetaSpecifiers.Size = new System.Drawing.Size(360, 273);
            this.ScrollPanel_MetaSpecifiers.TabIndex = 5;
            // 
            // Tlp_MetaSpecifiers
            // 
            this.Tlp_MetaSpecifiers.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Tlp_MetaSpecifiers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.Tlp_MetaSpecifiers.ColumnCount = 2;
            this.Tlp_MetaSpecifiers.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 62.59947F));
            this.Tlp_MetaSpecifiers.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.40053F));
            this.Tlp_MetaSpecifiers.Location = new System.Drawing.Point(-20, 0);
            this.Tlp_MetaSpecifiers.Name = "Tlp_MetaSpecifiers";
            this.Tlp_MetaSpecifiers.RowCount = 1;
            this.Tlp_MetaSpecifiers.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.Tlp_MetaSpecifiers.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.Tlp_MetaSpecifiers.Size = new System.Drawing.Size(377, 270);
            this.Tlp_MetaSpecifiers.TabIndex = 0;
            // 
            // ScrollPanel_AdvancedSettings
            // 
            this.ScrollPanel_AdvancedSettings.AutoScroll = true;
            this.ScrollPanel_AdvancedSettings.Controls.Add(this.Tlp_AdvancedSettings);
            this.ScrollPanel_AdvancedSettings.Location = new System.Drawing.Point(260, 13);
            this.ScrollPanel_AdvancedSettings.Name = "ScrollPanel_AdvancedSettings";
            this.ScrollPanel_AdvancedSettings.Size = new System.Drawing.Size(360, 119);
            this.ScrollPanel_AdvancedSettings.TabIndex = 6;
            // 
            // Tlp_AdvancedSettings
            // 
            this.Tlp_AdvancedSettings.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Tlp_AdvancedSettings.AutoScroll = true;
            this.Tlp_AdvancedSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.Tlp_AdvancedSettings.ColumnCount = 2;
            this.Tlp_AdvancedSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 62.82528F));
            this.Tlp_AdvancedSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.17472F));
            this.Tlp_AdvancedSettings.Location = new System.Drawing.Point(-20, 3);
            this.Tlp_AdvancedSettings.Name = "Tlp_AdvancedSettings";
            this.Tlp_AdvancedSettings.RowCount = 1;
            this.Tlp_AdvancedSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.Tlp_AdvancedSettings.Size = new System.Drawing.Size(377, 116);
            this.Tlp_AdvancedSettings.TabIndex = 0;
            // 
            // Cb_WithTemplate
            // 
            this.Cb_WithTemplate.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.Cb_WithTemplate.Checked = true;
            this.Cb_WithTemplate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Cb_WithTemplate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Cb_WithTemplate.Location = new System.Drawing.Point(293, 3);
            this.Cb_WithTemplate.Name = "Cb_WithTemplate";
            this.Cb_WithTemplate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Cb_WithTemplate.Size = new System.Drawing.Size(120, 20);
            this.Cb_WithTemplate.TabIndex = 1;
            this.Cb_WithTemplate.Text = "With template";
            this.Cb_WithTemplate.UseVisualStyleBackColor = true;
            // 
            // Llbl_Document
            // 
            this.Llbl_Document.DisabledLinkColor = System.Drawing.Color.White;
            this.Llbl_Document.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Llbl_Document.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.Llbl_Document.Location = new System.Drawing.Point(3, 3);
            this.Llbl_Document.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.Llbl_Document.Name = "Llbl_Document";
            this.Llbl_Document.Size = new System.Drawing.Size(284, 16);
            this.Llbl_Document.TabIndex = 0;
            this.Llbl_Document.TabStop = true;
            this.Llbl_Document.Text = "Open UnrealMacro document";
            this.Llbl_Document.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnDocumentLinkClicked);
            // 
            // Flp_Document
            // 
            this.Flp_Document.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.Flp_Document.Controls.Add(this.Llbl_Document);
            this.Flp_Document.Controls.Add(this.Cb_WithTemplate);
            this.Flp_Document.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Flp_Document.ForeColor = System.Drawing.Color.White;
            this.Flp_Document.Location = new System.Drawing.Point(13, 418);
            this.Flp_Document.Name = "Flp_Document";
            this.Flp_Document.Size = new System.Drawing.Size(421, 24);
            this.Flp_Document.TabIndex = 7;
            // 
            // MacroEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.ClientSize = new System.Drawing.Size(633, 448);
            this.ControlBox = false;
            this.Controls.Add(this.Flp_Document);
            this.Controls.Add(this.ScrollPanel_AdvancedSettings);
            this.Controls.Add(this.ScrollPanel_MetaSpecifiers);
            this.Controls.Add(this.Cl_MacroSpecifiers);
            this.Controls.Add(this.Btn_Cancel);
            this.Controls.Add(this.Btn_OK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MacroEditor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ScrollPanel_MetaSpecifiers.ResumeLayout(false);
            this.ScrollPanel_AdvancedSettings.ResumeLayout(false);
            this.Flp_Document.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Btn_OK;
        private System.Windows.Forms.Button Btn_Cancel;
        private System.Windows.Forms.CheckedListBox Cl_MacroSpecifiers;
        private System.Windows.Forms.Panel ScrollPanel_MetaSpecifiers;
        private System.Windows.Forms.TableLayoutPanel Tlp_MetaSpecifiers;
        private System.Windows.Forms.Panel ScrollPanel_AdvancedSettings;
        private System.Windows.Forms.TableLayoutPanel Tlp_AdvancedSettings;
        private System.Windows.Forms.CheckBox Cb_WithTemplate;
        private System.Windows.Forms.LinkLabel Llbl_Document;
        private System.Windows.Forms.FlowLayoutPanel Flp_Document;
    }
}