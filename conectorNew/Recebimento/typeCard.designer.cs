namespace conectorPDV001
{
    partial class typeCard
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
            this.lnkDebitoTypeCard = new System.Windows.Forms.LinkLabel();
            this.lnkCreditoTypeCard = new System.Windows.Forms.LinkLabel();
            this.lnkTicketTypeCard = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // lnkDebitoTypeCard
            // 
            this.lnkDebitoTypeCard.AutoSize = true;
            this.lnkDebitoTypeCard.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkDebitoTypeCard.LinkColor = System.Drawing.Color.Lime;
            this.lnkDebitoTypeCard.Location = new System.Drawing.Point(36, 64);
            this.lnkDebitoTypeCard.Name = "lnkDebitoTypeCard";
            this.lnkDebitoTypeCard.Size = new System.Drawing.Size(169, 25);
            this.lnkDebitoTypeCard.TabIndex = 0;
            this.lnkDebitoTypeCard.TabStop = true;
            this.lnkDebitoTypeCard.Text = "[ F2 ] - DEBITO ";
            this.lnkDebitoTypeCard.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkDebitoTypeCard_LinkClicked);
            // 
            // lnkCreditoTypeCard
            // 
            this.lnkCreditoTypeCard.AutoSize = true;
            this.lnkCreditoTypeCard.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkCreditoTypeCard.LinkColor = System.Drawing.Color.Lime;
            this.lnkCreditoTypeCard.Location = new System.Drawing.Point(258, 64);
            this.lnkCreditoTypeCard.Name = "lnkCreditoTypeCard";
            this.lnkCreditoTypeCard.Size = new System.Drawing.Size(185, 25);
            this.lnkCreditoTypeCard.TabIndex = 1;
            this.lnkCreditoTypeCard.TabStop = true;
            this.lnkCreditoTypeCard.Text = "[ F3 ] - CREDITO ";
            this.lnkCreditoTypeCard.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkCreditoTypeCard_LinkClicked);
            // 
            // lnkTicketTypeCard
            // 
            this.lnkTicketTypeCard.AutoSize = true;
            this.lnkTicketTypeCard.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkTicketTypeCard.LinkColor = System.Drawing.Color.Lime;
            this.lnkTicketTypeCard.Location = new System.Drawing.Point(493, 64);
            this.lnkTicketTypeCard.Name = "lnkTicketTypeCard";
            this.lnkTicketTypeCard.Size = new System.Drawing.Size(189, 25);
            this.lnkTicketTypeCard.TabIndex = 2;
            this.lnkTicketTypeCard.TabStop = true;
            this.lnkTicketTypeCard.Text = "[ F4 ] - VOUCHER";
            this.lnkTicketTypeCard.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkTicketTypeCard_LinkClicked);
            // 
            // typeCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(724, 155);
            this.Controls.Add(this.lnkTicketTypeCard);
            this.Controls.Add(this.lnkCreditoTypeCard);
            this.Controls.Add(this.lnkDebitoTypeCard);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "typeCard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "typeCard";
            this.Load += new System.EventHandler(this.typeCard_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.typeCard_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel lnkDebitoTypeCard;
        private System.Windows.Forms.LinkLabel lnkCreditoTypeCard;
        private System.Windows.Forms.LinkLabel lnkTicketTypeCard;
    }
}