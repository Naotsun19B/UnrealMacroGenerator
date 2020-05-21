namespace UnrealMacroGenerator.DialogUI
{
    partial class DelegateEditor
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
            this.Pnl_Parameters = new System.Windows.Forms.Panel();
            this.Cb_IsMulticast = new System.Windows.Forms.CheckBox();
            this.Cb_IsDynamic = new System.Windows.Forms.CheckBox();
            this.Cb_HasRetVal = new System.Windows.Forms.CheckBox();
            this.Cb_IsEvent = new System.Windows.Forms.CheckBox();
            this.Lbl_Name = new System.Windows.Forms.Label();
            this.Lbl_Type = new System.Windows.Forms.Label();
            this.Tb_Type = new System.Windows.Forms.TextBox();
            this.Tb_Name = new System.Windows.Forms.TextBox();
            this.Flp_Document = new System.Windows.Forms.FlowLayoutPanel();
            this.Llbl_Document = new System.Windows.Forms.LinkLabel();
            this.Btn_Cancel = new System.Windows.Forms.Button();
            this.Btn_OK = new System.Windows.Forms.Button();
            this.Tlp_ArgsPannel = new System.Windows.Forms.TableLayoutPanel();
            this.Lbl_Arguments = new System.Windows.Forms.Label();
            this.Tlp_Arguments = new System.Windows.Forms.TableLayoutPanel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.Pnl_Parameters.SuspendLayout();
            this.Flp_Document.SuspendLayout();
            this.Tlp_ArgsPannel.SuspendLayout();
            this.Tlp_Arguments.SuspendLayout();
            this.SuspendLayout();
            // 
            // Pnl_Parameters
            // 
            this.Pnl_Parameters.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.Pnl_Parameters.Controls.Add(this.Cb_IsMulticast);
            this.Pnl_Parameters.Controls.Add(this.Cb_IsDynamic);
            this.Pnl_Parameters.Controls.Add(this.Cb_HasRetVal);
            this.Pnl_Parameters.Controls.Add(this.Cb_IsEvent);
            this.Pnl_Parameters.Controls.Add(this.Lbl_Name);
            this.Pnl_Parameters.Controls.Add(this.Lbl_Type);
            this.Pnl_Parameters.Controls.Add(this.Tb_Type);
            this.Pnl_Parameters.Controls.Add(this.Tb_Name);
            this.Pnl_Parameters.Location = new System.Drawing.Point(13, 13);
            this.Pnl_Parameters.Margin = new System.Windows.Forms.Padding(4);
            this.Pnl_Parameters.Name = "Pnl_Parameters";
            this.Pnl_Parameters.Size = new System.Drawing.Size(232, 205);
            this.Pnl_Parameters.TabIndex = 12;
            // 
            // Cb_IsMulticast
            // 
            this.Cb_IsMulticast.AutoSize = true;
            this.Cb_IsMulticast.ForeColor = System.Drawing.Color.White;
            this.Cb_IsMulticast.Location = new System.Drawing.Point(107, 41);
            this.Cb_IsMulticast.Name = "Cb_IsMulticast";
            this.Cb_IsMulticast.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Cb_IsMulticast.Size = new System.Drawing.Size(111, 20);
            this.Cb_IsMulticast.TabIndex = 7;
            this.Cb_IsMulticast.Text = "MULTICAST";
            this.Cb_IsMulticast.UseVisualStyleBackColor = true;
            this.Cb_IsMulticast.Click += new System.EventHandler(this.OnCheckBoxClicked);
            // 
            // Cb_IsDynamic
            // 
            this.Cb_IsDynamic.AutoSize = true;
            this.Cb_IsDynamic.ForeColor = System.Drawing.Color.White;
            this.Cb_IsDynamic.Location = new System.Drawing.Point(107, 15);
            this.Cb_IsDynamic.Name = "Cb_IsDynamic";
            this.Cb_IsDynamic.Size = new System.Drawing.Size(93, 20);
            this.Cb_IsDynamic.TabIndex = 6;
            this.Cb_IsDynamic.Text = "DYNAMIC";
            this.Cb_IsDynamic.UseVisualStyleBackColor = true;
            this.Cb_IsDynamic.Click += new System.EventHandler(this.OnCheckBoxClicked);
            // 
            // Cb_HasRetVal
            // 
            this.Cb_HasRetVal.AutoSize = true;
            this.Cb_HasRetVal.ForeColor = System.Drawing.Color.White;
            this.Cb_HasRetVal.Location = new System.Drawing.Point(20, 41);
            this.Cb_HasRetVal.Name = "Cb_HasRetVal";
            this.Cb_HasRetVal.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Cb_HasRetVal.Size = new System.Drawing.Size(72, 20);
            this.Cb_HasRetVal.TabIndex = 5;
            this.Cb_HasRetVal.Text = "RetVal";
            this.Cb_HasRetVal.UseVisualStyleBackColor = true;
            this.Cb_HasRetVal.Click += new System.EventHandler(this.OnCheckBoxClicked);
            // 
            // Cb_IsEvent
            // 
            this.Cb_IsEvent.AutoSize = true;
            this.Cb_IsEvent.ForeColor = System.Drawing.Color.White;
            this.Cb_IsEvent.Location = new System.Drawing.Point(20, 15);
            this.Cb_IsEvent.Name = "Cb_IsEvent";
            this.Cb_IsEvent.Size = new System.Drawing.Size(74, 20);
            this.Cb_IsEvent.TabIndex = 4;
            this.Cb_IsEvent.Text = "EVENT";
            this.Cb_IsEvent.UseVisualStyleBackColor = true;
            this.Cb_IsEvent.Click += new System.EventHandler(this.OnCheckBoxClicked);
            // 
            // Lbl_Name
            // 
            this.Lbl_Name.AutoSize = true;
            this.Lbl_Name.ForeColor = System.Drawing.Color.White;
            this.Lbl_Name.Location = new System.Drawing.Point(17, 76);
            this.Lbl_Name.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Lbl_Name.Name = "Lbl_Name";
            this.Lbl_Name.Size = new System.Drawing.Size(109, 16);
            this.Lbl_Name.TabIndex = 3;
            this.Lbl_Name.Text = "Delegate Name";
            // 
            // Lbl_Type
            // 
            this.Lbl_Type.AutoSize = true;
            this.Lbl_Type.ForeColor = System.Drawing.Color.White;
            this.Lbl_Type.Location = new System.Drawing.Point(17, 135);
            this.Lbl_Type.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Lbl_Type.Name = "Lbl_Type";
            this.Lbl_Type.Size = new System.Drawing.Size(93, 16);
            this.Lbl_Type.TabIndex = 2;
            this.Lbl_Type.Text = "Owning Type";
            this.Lbl_Type.Visible = false;
            // 
            // Tb_Type
            // 
            this.Tb_Type.Location = new System.Drawing.Point(20, 155);
            this.Tb_Type.Margin = new System.Windows.Forms.Padding(4);
            this.Tb_Type.Name = "Tb_Type";
            this.Tb_Type.Size = new System.Drawing.Size(194, 23);
            this.Tb_Type.TabIndex = 1;
            this.Tb_Type.Visible = false;
            // 
            // Tb_Name
            // 
            this.Tb_Name.Location = new System.Drawing.Point(20, 96);
            this.Tb_Name.Margin = new System.Windows.Forms.Padding(4);
            this.Tb_Name.Name = "Tb_Name";
            this.Tb_Name.Size = new System.Drawing.Size(194, 23);
            this.Tb_Name.TabIndex = 0;
            // 
            // Flp_Document
            // 
            this.Flp_Document.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.Flp_Document.Controls.Add(this.Llbl_Document);
            this.Flp_Document.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Flp_Document.ForeColor = System.Drawing.Color.White;
            this.Flp_Document.Location = new System.Drawing.Point(12, 225);
            this.Flp_Document.Name = "Flp_Document";
            this.Flp_Document.Size = new System.Drawing.Size(373, 26);
            this.Flp_Document.TabIndex = 15;
            // 
            // Llbl_Document
            // 
            this.Llbl_Document.AutoSize = true;
            this.Llbl_Document.DisabledLinkColor = System.Drawing.Color.White;
            this.Llbl_Document.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Llbl_Document.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Llbl_Document.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.Llbl_Document.Location = new System.Drawing.Point(3, 3);
            this.Llbl_Document.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.Llbl_Document.Name = "Llbl_Document";
            this.Llbl_Document.Size = new System.Drawing.Size(197, 16);
            this.Llbl_Document.TabIndex = 0;
            this.Llbl_Document.TabStop = true;
            this.Llbl_Document.Text = "Open delegate document";
            this.Llbl_Document.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnDocumentLinkClicked);
            // 
            // Btn_Cancel
            // 
            this.Btn_Cancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.Btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Btn_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Btn_Cancel.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Btn_Cancel.ForeColor = System.Drawing.Color.White;
            this.Btn_Cancel.Location = new System.Drawing.Point(494, 228);
            this.Btn_Cancel.Name = "Btn_Cancel";
            this.Btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.Btn_Cancel.TabIndex = 14;
            this.Btn_Cancel.Text = "Cancel";
            this.Btn_Cancel.UseVisualStyleBackColor = false;
            // 
            // Btn_OK
            // 
            this.Btn_OK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.Btn_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Btn_OK.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Btn_OK.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Btn_OK.ForeColor = System.Drawing.Color.White;
            this.Btn_OK.Location = new System.Drawing.Point(400, 228);
            this.Btn_OK.Name = "Btn_OK";
            this.Btn_OK.Size = new System.Drawing.Size(75, 23);
            this.Btn_OK.TabIndex = 13;
            this.Btn_OK.Text = "OK";
            this.Btn_OK.UseVisualStyleBackColor = false;
            this.Btn_OK.Click += new System.EventHandler(this.OnOKButtonClicked);
            // 
            // Tlp_ArgsPannel
            // 
            this.Tlp_ArgsPannel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.Tlp_ArgsPannel.ColumnCount = 1;
            this.Tlp_ArgsPannel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.Tlp_ArgsPannel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.Tlp_ArgsPannel.Controls.Add(this.Lbl_Arguments, 0, 0);
            this.Tlp_ArgsPannel.Controls.Add(this.Tlp_Arguments, 0, 1);
            this.Tlp_ArgsPannel.Location = new System.Drawing.Point(252, 13);
            this.Tlp_ArgsPannel.Name = "Tlp_ArgsPannel";
            this.Tlp_ArgsPannel.RowCount = 2;
            this.Tlp_ArgsPannel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.14634F));
            this.Tlp_ArgsPannel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85.85366F));
            this.Tlp_ArgsPannel.Size = new System.Drawing.Size(317, 205);
            this.Tlp_ArgsPannel.TabIndex = 16;
            // 
            // Lbl_Arguments
            // 
            this.Lbl_Arguments.AutoSize = true;
            this.Lbl_Arguments.ForeColor = System.Drawing.Color.White;
            this.Lbl_Arguments.Location = new System.Drawing.Point(3, 3);
            this.Lbl_Arguments.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.Lbl_Arguments.Name = "Lbl_Arguments";
            this.Lbl_Arguments.Size = new System.Drawing.Size(113, 16);
            this.Lbl_Arguments.TabIndex = 11;
            this.Lbl_Arguments.Text = "ArgumentsType";
            // 
            // Tlp_Arguments
            // 
            this.Tlp_Arguments.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Tlp_Arguments.AutoScroll = true;
            this.Tlp_Arguments.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.Tlp_Arguments.ColumnCount = 2;
            this.Tlp_Arguments.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.Tlp_Arguments.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.Tlp_Arguments.Controls.Add(this.button2, 0, 0);
            this.Tlp_Arguments.Controls.Add(this.button1, 0, 0);
            this.Tlp_Arguments.Location = new System.Drawing.Point(3, 31);
            this.Tlp_Arguments.Name = "Tlp_Arguments";
            this.Tlp_Arguments.RowCount = 1;
            this.Tlp_Arguments.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.Tlp_Arguments.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 304F));
            this.Tlp_Arguments.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 304F));
            this.Tlp_Arguments.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 304F));
            this.Tlp_Arguments.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 304F));
            this.Tlp_Arguments.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 304F));
            this.Tlp_Arguments.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 304F));
            this.Tlp_Arguments.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 304F));
            this.Tlp_Arguments.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 304F));
            this.Tlp_Arguments.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 304F));
            this.Tlp_Arguments.Size = new System.Drawing.Size(311, 171);
            this.Tlp_Arguments.TabIndex = 10;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.button2.Enabled = false;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(158, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(150, 23);
            this.button2.TabIndex = 15;
            this.button2.Text = "- Remove";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.OnRemoveArgumentsButtonClicked);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(3, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(149, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "+ Add";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.OnAddArgumentsButtonClicked);
            // 
            // DelegateEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(581, 263);
            this.ControlBox = false;
            this.Controls.Add(this.Tlp_ArgsPannel);
            this.Controls.Add(this.Flp_Document);
            this.Controls.Add(this.Btn_Cancel);
            this.Controls.Add(this.Btn_OK);
            this.Controls.Add(this.Pnl_Parameters);
            this.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DelegateEditor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.OnEditorLoad);
            this.Pnl_Parameters.ResumeLayout(false);
            this.Pnl_Parameters.PerformLayout();
            this.Flp_Document.ResumeLayout(false);
            this.Flp_Document.PerformLayout();
            this.Tlp_ArgsPannel.ResumeLayout(false);
            this.Tlp_ArgsPannel.PerformLayout();
            this.Tlp_Arguments.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel Pnl_Parameters;
        private System.Windows.Forms.TextBox Tb_Type;
        private System.Windows.Forms.TextBox Tb_Name;
        private System.Windows.Forms.Label Lbl_Type;
        private System.Windows.Forms.FlowLayoutPanel Flp_Document;
        private System.Windows.Forms.LinkLabel Llbl_Document;
        private System.Windows.Forms.Button Btn_Cancel;
        private System.Windows.Forms.Button Btn_OK;
        private System.Windows.Forms.CheckBox Cb_HasRetVal;
        private System.Windows.Forms.CheckBox Cb_IsEvent;
        private System.Windows.Forms.Label Lbl_Name;
        private System.Windows.Forms.CheckBox Cb_IsMulticast;
        private System.Windows.Forms.CheckBox Cb_IsDynamic;
        private System.Windows.Forms.TableLayoutPanel Tlp_ArgsPannel;
        private System.Windows.Forms.Label Lbl_Arguments;
        private System.Windows.Forms.TableLayoutPanel Tlp_Arguments;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
    }
}