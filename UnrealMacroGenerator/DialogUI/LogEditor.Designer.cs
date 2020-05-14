namespace UnrealMacroGenerator.DialogUI
{
    partial class LogEditor
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
            this.Btn_Cancel = new System.Windows.Forms.Button();
            this.Btn_OK = new System.Windows.Forms.Button();
            this.Flp_Document = new System.Windows.Forms.FlowLayoutPanel();
            this.Llbl_Document = new System.Windows.Forms.LinkLabel();
            this.Pnl_Parameters = new System.Windows.Forms.Panel();
            this.Tb_Format = new System.Windows.Forms.TextBox();
            this.Lbl_Format = new System.Windows.Forms.Label();
            this.Cb_Verbosity = new System.Windows.Forms.ComboBox();
            this.Cb_CategoryName = new System.Windows.Forms.ComboBox();
            this.Lbl_Verbosity = new System.Windows.Forms.Label();
            this.Lbl_CategoryName = new System.Windows.Forms.Label();
            this.Tlp_Arguments = new System.Windows.Forms.TableLayoutPanel();
            this.Tlp_ArgsPannel = new System.Windows.Forms.TableLayoutPanel();
            this.Lbl_Arguments = new System.Windows.Forms.Label();
            this.Flp_Document.SuspendLayout();
            this.Pnl_Parameters.SuspendLayout();
            this.Tlp_ArgsPannel.SuspendLayout();
            this.SuspendLayout();
            // 
            // Btn_Cancel
            // 
            this.Btn_Cancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.Btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Btn_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Btn_Cancel.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Btn_Cancel.ForeColor = System.Drawing.Color.White;
            this.Btn_Cancel.Location = new System.Drawing.Point(511, 494);
            this.Btn_Cancel.Name = "Btn_Cancel";
            this.Btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.Btn_Cancel.TabIndex = 4;
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
            this.Btn_OK.Location = new System.Drawing.Point(414, 494);
            this.Btn_OK.Name = "Btn_OK";
            this.Btn_OK.Size = new System.Drawing.Size(75, 23);
            this.Btn_OK.TabIndex = 3;
            this.Btn_OK.Text = "OK";
            this.Btn_OK.UseVisualStyleBackColor = false;
            this.Btn_OK.Click += new System.EventHandler(this.OnOKButtonClicked);
            // 
            // Flp_Document
            // 
            this.Flp_Document.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.Flp_Document.Controls.Add(this.Llbl_Document);
            this.Flp_Document.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Flp_Document.ForeColor = System.Drawing.Color.White;
            this.Flp_Document.Location = new System.Drawing.Point(12, 494);
            this.Flp_Document.Name = "Flp_Document";
            this.Flp_Document.Size = new System.Drawing.Size(384, 26);
            this.Flp_Document.TabIndex = 8;
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
            this.Llbl_Document.Size = new System.Drawing.Size(189, 16);
            this.Llbl_Document.TabIndex = 0;
            this.Llbl_Document.TabStop = true;
            this.Llbl_Document.Text = "Open Logging document";
            this.Llbl_Document.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnDocumentLinkClicked);
            // 
            // Pnl_Parameters
            // 
            this.Pnl_Parameters.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.Pnl_Parameters.Controls.Add(this.Tb_Format);
            this.Pnl_Parameters.Controls.Add(this.Lbl_Format);
            this.Pnl_Parameters.Controls.Add(this.Cb_Verbosity);
            this.Pnl_Parameters.Controls.Add(this.Cb_CategoryName);
            this.Pnl_Parameters.Controls.Add(this.Lbl_Verbosity);
            this.Pnl_Parameters.Controls.Add(this.Lbl_CategoryName);
            this.Pnl_Parameters.Location = new System.Drawing.Point(12, 12);
            this.Pnl_Parameters.Name = "Pnl_Parameters";
            this.Pnl_Parameters.Size = new System.Drawing.Size(287, 473);
            this.Pnl_Parameters.TabIndex = 9;
            // 
            // Tb_Format
            // 
            this.Tb_Format.Location = new System.Drawing.Point(15, 143);
            this.Tb_Format.Multiline = true;
            this.Tb_Format.Name = "Tb_Format";
            this.Tb_Format.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Tb_Format.Size = new System.Drawing.Size(257, 312);
            this.Tb_Format.TabIndex = 5;
            this.Tb_Format.TextChanged += new System.EventHandler(this.OnTextChanged);
            this.Tb_Format.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnEnterPushed);
            // 
            // Lbl_Format
            // 
            this.Lbl_Format.AutoSize = true;
            this.Lbl_Format.ForeColor = System.Drawing.Color.White;
            this.Lbl_Format.Location = new System.Drawing.Point(12, 124);
            this.Lbl_Format.Name = "Lbl_Format";
            this.Lbl_Format.Size = new System.Drawing.Size(57, 16);
            this.Lbl_Format.TabIndex = 4;
            this.Lbl_Format.Text = "Format";
            // 
            // Cb_Verbosity
            // 
            this.Cb_Verbosity.FormattingEnabled = true;
            this.Cb_Verbosity.Location = new System.Drawing.Point(151, 66);
            this.Cb_Verbosity.Name = "Cb_Verbosity";
            this.Cb_Verbosity.Size = new System.Drawing.Size(121, 24);
            this.Cb_Verbosity.TabIndex = 3;
            // 
            // Cb_CategoryName
            // 
            this.Cb_CategoryName.FormattingEnabled = true;
            this.Cb_CategoryName.Location = new System.Drawing.Point(151, 14);
            this.Cb_CategoryName.Name = "Cb_CategoryName";
            this.Cb_CategoryName.Size = new System.Drawing.Size(121, 24);
            this.Cb_CategoryName.TabIndex = 2;
            // 
            // Lbl_Verbosity
            // 
            this.Lbl_Verbosity.AutoSize = true;
            this.Lbl_Verbosity.ForeColor = System.Drawing.Color.White;
            this.Lbl_Verbosity.Location = new System.Drawing.Point(12, 74);
            this.Lbl_Verbosity.Name = "Lbl_Verbosity";
            this.Lbl_Verbosity.Size = new System.Drawing.Size(72, 16);
            this.Lbl_Verbosity.TabIndex = 1;
            this.Lbl_Verbosity.Text = "Verbosity";
            // 
            // Lbl_CategoryName
            // 
            this.Lbl_CategoryName.AutoSize = true;
            this.Lbl_CategoryName.ForeColor = System.Drawing.Color.White;
            this.Lbl_CategoryName.Location = new System.Drawing.Point(12, 22);
            this.Lbl_CategoryName.Name = "Lbl_CategoryName";
            this.Lbl_CategoryName.Size = new System.Drawing.Size(108, 16);
            this.Lbl_CategoryName.TabIndex = 0;
            this.Lbl_CategoryName.Text = "CategoryName";
            // 
            // Tlp_Arguments
            // 
            this.Tlp_Arguments.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Tlp_Arguments.AutoScroll = true;
            this.Tlp_Arguments.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.Tlp_Arguments.ColumnCount = 1;
            this.Tlp_Arguments.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 62.82528F));
            this.Tlp_Arguments.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.17472F));
            this.Tlp_Arguments.Location = new System.Drawing.Point(12, 27);
            this.Tlp_Arguments.Name = "Tlp_Arguments";
            this.Tlp_Arguments.RowCount = 1;
            this.Tlp_Arguments.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.Tlp_Arguments.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 442F));
            this.Tlp_Arguments.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 442F));
            this.Tlp_Arguments.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 442F));
            this.Tlp_Arguments.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 442F));
            this.Tlp_Arguments.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 442F));
            this.Tlp_Arguments.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 442F));
            this.Tlp_Arguments.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 442F));
            this.Tlp_Arguments.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 442F));
            this.Tlp_Arguments.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 442F));
            this.Tlp_Arguments.Size = new System.Drawing.Size(257, 442);
            this.Tlp_Arguments.TabIndex = 10;
            // 
            // Tlp_ArgsPannel
            // 
            this.Tlp_ArgsPannel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.Tlp_ArgsPannel.ColumnCount = 1;
            this.Tlp_ArgsPannel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.Tlp_ArgsPannel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.Tlp_ArgsPannel.Controls.Add(this.Lbl_Arguments, 0, 0);
            this.Tlp_ArgsPannel.Controls.Add(this.Tlp_Arguments, 0, 1);
            this.Tlp_ArgsPannel.Location = new System.Drawing.Point(305, 13);
            this.Tlp_ArgsPannel.Name = "Tlp_ArgsPannel";
            this.Tlp_ArgsPannel.RowCount = 2;
            this.Tlp_ArgsPannel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.084746F));
            this.Tlp_ArgsPannel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 94.91525F));
            this.Tlp_ArgsPannel.Size = new System.Drawing.Size(281, 472);
            this.Tlp_ArgsPannel.TabIndex = 11;
            // 
            // Lbl_Arguments
            // 
            this.Lbl_Arguments.AutoSize = true;
            this.Lbl_Arguments.ForeColor = System.Drawing.Color.White;
            this.Lbl_Arguments.Location = new System.Drawing.Point(3, 3);
            this.Lbl_Arguments.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.Lbl_Arguments.Name = "Lbl_Arguments";
            this.Lbl_Arguments.Size = new System.Drawing.Size(80, 16);
            this.Lbl_Arguments.TabIndex = 11;
            this.Lbl_Arguments.Text = "Arguments";
            // 
            // LogEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.ClientSize = new System.Drawing.Size(598, 528);
            this.ControlBox = false;
            this.Controls.Add(this.Tlp_ArgsPannel);
            this.Controls.Add(this.Pnl_Parameters);
            this.Controls.Add(this.Flp_Document);
            this.Controls.Add(this.Btn_Cancel);
            this.Controls.Add(this.Btn_OK);
            this.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LogEditor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.OnEditorLoad);
            this.Flp_Document.ResumeLayout(false);
            this.Flp_Document.PerformLayout();
            this.Pnl_Parameters.ResumeLayout(false);
            this.Pnl_Parameters.PerformLayout();
            this.Tlp_ArgsPannel.ResumeLayout(false);
            this.Tlp_ArgsPannel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Btn_Cancel;
        private System.Windows.Forms.Button Btn_OK;
        private System.Windows.Forms.FlowLayoutPanel Flp_Document;
        private System.Windows.Forms.LinkLabel Llbl_Document;
        private System.Windows.Forms.Panel Pnl_Parameters;
        private System.Windows.Forms.Label Lbl_CategoryName;
        private System.Windows.Forms.Label Lbl_Verbosity;
        private System.Windows.Forms.TextBox Tb_Format;
        private System.Windows.Forms.Label Lbl_Format;
        private System.Windows.Forms.ComboBox Cb_Verbosity;
        private System.Windows.Forms.ComboBox Cb_CategoryName;
        private System.Windows.Forms.TableLayoutPanel Tlp_Arguments;
        private System.Windows.Forms.TableLayoutPanel Tlp_ArgsPannel;
        private System.Windows.Forms.Label Lbl_Arguments;
    }
}