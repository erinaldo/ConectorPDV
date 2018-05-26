namespace conectorPDV001
{
    partial class msgInfo
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
            this.components = new System.ComponentModel.Container();
            this.gpbMsgInfor = new System.Windows.Forms.GroupBox();
            this.txtMsgInfo = new System.Windows.Forms.TextBox();
            this.lbExitMsgInfo = new System.Windows.Forms.Label();
            this.cronometroInfo = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.clock = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.gpbMsgInfor.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpbMsgInfor
            // 
            this.gpbMsgInfor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.gpbMsgInfor.Controls.Add(this.label2);
            this.gpbMsgInfor.Controls.Add(this.label1);
            this.gpbMsgInfor.Controls.Add(this.txtMsgInfo);
            this.gpbMsgInfor.Controls.Add(this.lbExitMsgInfo);
            this.gpbMsgInfor.Location = new System.Drawing.Point(0, -6);
            this.gpbMsgInfor.Name = "gpbMsgInfor";
            this.gpbMsgInfor.Size = new System.Drawing.Size(1106, 113);
            this.gpbMsgInfor.TabIndex = 0;
            this.gpbMsgInfor.TabStop = false;
            // 
            // txtMsgInfo
            // 
            this.txtMsgInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.txtMsgInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMsgInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMsgInfo.ForeColor = System.Drawing.Color.White;
            this.txtMsgInfo.Location = new System.Drawing.Point(10, 17);
            this.txtMsgInfo.Multiline = true;
            this.txtMsgInfo.Name = "txtMsgInfo";
            this.txtMsgInfo.Size = new System.Drawing.Size(1084, 76);
            this.txtMsgInfo.TabIndex = 1;
            this.txtMsgInfo.TextChanged += new System.EventHandler(this.txtMsgInfo_TextChanged);
            this.txtMsgInfo.Validated += new System.EventHandler(this.txtMsgInfo_Validated);
            // 
            // lbExitMsgInfo
            // 
            this.lbExitMsgInfo.AutoSize = true;
            this.lbExitMsgInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbExitMsgInfo.ForeColor = System.Drawing.Color.White;
            this.lbExitMsgInfo.Location = new System.Drawing.Point(1033, 96);
            this.lbExitMsgInfo.Name = "lbExitMsgInfo";
            this.lbExitMsgInfo.Size = new System.Drawing.Size(68, 13);
            this.lbExitMsgInfo.TabIndex = 0;
            this.lbExitMsgInfo.Text = "F11 - EXIT";
            // 
            // cronometroInfo
            // 
            this.cronometroInfo.Interval = 2000;
            this.cronometroInfo.Tick += new System.EventHandler(this.cronometroInfo_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(49, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 63);
            this.label1.TabIndex = 2;
            this.label1.Text = "0";
            this.label1.Visible = false;
            // 
            // clock
            // 
            this.clock.Interval = 1000;
            this.clock.Tick += new System.EventHandler(this.clock_Tick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(147, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(921, 37);
            this.label2.TabIndex = 3;
            this.label2.Text = "AGUARDE ALGUNS SEGUNDOS OU PRESSIONE F11 - SAIR";
            this.label2.Visible = false;
            // 
            // msgInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(1106, 108);
            this.Controls.Add(this.gpbMsgInfor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "msgInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "msgInfo";
            this.Load += new System.EventHandler(this.msgInfo_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.msgInfo_KeyDown);
            this.gpbMsgInfor.ResumeLayout(false);
            this.gpbMsgInfor.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gpbMsgInfor;
        private System.Windows.Forms.TextBox txtMsgInfo;
        private System.Windows.Forms.Label lbExitMsgInfo;
        private System.Windows.Forms.Timer cronometroInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer clock;
        private System.Windows.Forms.Label label2;
    }
}