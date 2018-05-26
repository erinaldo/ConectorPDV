namespace conectorPDV001
{
    partial class consultaNota
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvConsultaNota = new System.Windows.Forms.DataGridView();
            this.chave = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nr_nota = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Serie = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cfop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fornecedor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Razao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Emissao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValorTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConsultaNota)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvConsultaNota
            // 
            this.dgvConsultaNota.AllowUserToOrderColumns = true;
            this.dgvConsultaNota.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvConsultaNota.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvConsultaNota.BackgroundColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvConsultaNota.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvConsultaNota.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvConsultaNota.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chave,
            this.Nr_nota,
            this.Serie,
            this.cfop,
            this.Fornecedor,
            this.Razao,
            this.Emissao,
            this.ValorTotal});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvConsultaNota.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvConsultaNota.Location = new System.Drawing.Point(1, 1);
            this.dgvConsultaNota.Margin = new System.Windows.Forms.Padding(2);
            this.dgvConsultaNota.Name = "dgvConsultaNota";
            this.dgvConsultaNota.ReadOnly = true;
            this.dgvConsultaNota.RowHeadersVisible = false;
            this.dgvConsultaNota.RowTemplate.Height = 24;
            this.dgvConsultaNota.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvConsultaNota.Size = new System.Drawing.Size(817, 251);
            this.dgvConsultaNota.TabIndex = 23;
            this.dgvConsultaNota.DoubleClick += new System.EventHandler(this.dgvConsultaNota_DoubleClick);
            this.dgvConsultaNota.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvConsultaNota_CellClick);
            this.dgvConsultaNota.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvConsultaNota_CellContentClick);
            // 
            // chave
            // 
            this.chave.HeaderText = "Chave";
            this.chave.Name = "chave";
            this.chave.ReadOnly = true;
            // 
            // Nr_nota
            // 
            this.Nr_nota.HeaderText = "Nr. Nota";
            this.Nr_nota.Name = "Nr_nota";
            this.Nr_nota.ReadOnly = true;
            // 
            // Serie
            // 
            this.Serie.HeaderText = "Série";
            this.Serie.Name = "Serie";
            this.Serie.ReadOnly = true;
            // 
            // cfop
            // 
            this.cfop.HeaderText = "CFOP";
            this.cfop.Name = "cfop";
            this.cfop.ReadOnly = true;
            // 
            // Fornecedor
            // 
            this.Fornecedor.HeaderText = "Fornecedor";
            this.Fornecedor.Name = "Fornecedor";
            this.Fornecedor.ReadOnly = true;
            // 
            // Razao
            // 
            this.Razao.HeaderText = "Razão";
            this.Razao.Name = "Razao";
            this.Razao.ReadOnly = true;
            // 
            // Emissao
            // 
            this.Emissao.HeaderText = "Emissão";
            this.Emissao.Name = "Emissao";
            this.Emissao.ReadOnly = true;
            // 
            // ValorTotal
            // 
            this.ValorTotal.HeaderText = "Total";
            this.ValorTotal.Name = "ValorTotal";
            this.ValorTotal.ReadOnly = true;
            // 
            // consultaNota
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 253);
            this.Controls.Add(this.dgvConsultaNota);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "consultaNota";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "consultaNota";
            this.Load += new System.EventHandler(this.consultaNota_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.consultaNota_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvConsultaNota)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.DataGridView dgvConsultaNota;
        private System.Windows.Forms.DataGridViewTextBoxColumn chave;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nr_nota;
        private System.Windows.Forms.DataGridViewTextBoxColumn Serie;
        private System.Windows.Forms.DataGridViewTextBoxColumn cfop;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fornecedor;
        private System.Windows.Forms.DataGridViewTextBoxColumn Razao;
        private System.Windows.Forms.DataGridViewTextBoxColumn Emissao;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValorTotal;

    }
}