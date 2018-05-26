namespace conectorPDV001
{
    partial class insertTypePepleo
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
            this.btnFormConsultaCliente = new System.Windows.Forms.Button();
            this.btnFormPesquisaCliente = new System.Windows.Forms.Button();
            this.btnFormPesquisaLoja = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnFormConsultaCliente
            // 
            this.btnFormConsultaCliente.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnFormConsultaCliente.FlatAppearance.BorderSize = 0;
            this.btnFormConsultaCliente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFormConsultaCliente.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFormConsultaCliente.ForeColor = System.Drawing.Color.Lime;
            this.btnFormConsultaCliente.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnFormConsultaCliente.ImageIndex = 88;
            this.btnFormConsultaCliente.Location = new System.Drawing.Point(2, 0);
            this.btnFormConsultaCliente.Name = "btnFormConsultaCliente";
            this.btnFormConsultaCliente.Size = new System.Drawing.Size(200, 126);
            this.btnFormConsultaCliente.TabIndex = 160;
            this.btnFormConsultaCliente.Text = "CLIENTE";
            this.btnFormConsultaCliente.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnFormConsultaCliente.UseVisualStyleBackColor = true;
            this.btnFormConsultaCliente.Click += new System.EventHandler(this.btnFormConsultaCliente_Click);
            // 
            // btnFormPesquisaCliente
            // 
            this.btnFormPesquisaCliente.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnFormPesquisaCliente.FlatAppearance.BorderSize = 0;
            this.btnFormPesquisaCliente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFormPesquisaCliente.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFormPesquisaCliente.ForeColor = System.Drawing.Color.Lime;
            this.btnFormPesquisaCliente.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnFormPesquisaCliente.ImageIndex = 88;
            this.btnFormPesquisaCliente.Location = new System.Drawing.Point(200, 0);
            this.btnFormPesquisaCliente.Name = "btnFormPesquisaCliente";
            this.btnFormPesquisaCliente.Size = new System.Drawing.Size(200, 126);
            this.btnFormPesquisaCliente.TabIndex = 161;
            this.btnFormPesquisaCliente.Text = "FORNECEDOR";
            this.btnFormPesquisaCliente.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnFormPesquisaCliente.UseVisualStyleBackColor = true;
            this.btnFormPesquisaCliente.Click += new System.EventHandler(this.btnFormPesquisaCliente_Click);
            // 
            // btnFormPesquisaLoja
            // 
            this.btnFormPesquisaLoja.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnFormPesquisaLoja.FlatAppearance.BorderSize = 0;
            this.btnFormPesquisaLoja.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFormPesquisaLoja.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFormPesquisaLoja.ForeColor = System.Drawing.Color.Lime;
            this.btnFormPesquisaLoja.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnFormPesquisaLoja.ImageIndex = 88;
            this.btnFormPesquisaLoja.Location = new System.Drawing.Point(399, 0);
            this.btnFormPesquisaLoja.Name = "btnFormPesquisaLoja";
            this.btnFormPesquisaLoja.Size = new System.Drawing.Size(200, 126);
            this.btnFormPesquisaLoja.TabIndex = 162;
            this.btnFormPesquisaLoja.Text = "LOJA";
            this.btnFormPesquisaLoja.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnFormPesquisaLoja.UseVisualStyleBackColor = true;
            this.btnFormPesquisaLoja.Click += new System.EventHandler(this.btnFormPesquisaLoja_Click);
            // 
            // insertTypePepleo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(600, 126);
            this.Controls.Add(this.btnFormPesquisaLoja);
            this.Controls.Add(this.btnFormPesquisaCliente);
            this.Controls.Add(this.btnFormConsultaCliente);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "insertTypePepleo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "insertTypePepleo";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.insertTypePepleo_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button btnFormConsultaCliente;
        internal System.Windows.Forms.Button btnFormPesquisaCliente;
        internal System.Windows.Forms.Button btnFormPesquisaLoja;
    }
}