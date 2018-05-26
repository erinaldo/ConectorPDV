namespace conectorPDV001
{
    partial class pesquisaFinalizadora
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
            this.dgvPesquisaFinalizadora = new System.Windows.Forms.DataGridView();
            this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Razao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPesquisaFinalizadora)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvPesquisaFinalizadora
            // 
            this.dgvPesquisaFinalizadora.AllowUserToDeleteRows = false;
            this.dgvPesquisaFinalizadora.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvPesquisaFinalizadora.BackgroundColor = System.Drawing.SystemColors.Info;
            this.dgvPesquisaFinalizadora.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPesquisaFinalizadora.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.codigo,
            this.Razao});
            this.dgvPesquisaFinalizadora.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPesquisaFinalizadora.GridColor = System.Drawing.Color.Black;
            this.dgvPesquisaFinalizadora.Location = new System.Drawing.Point(0, 0);
            this.dgvPesquisaFinalizadora.Name = "dgvPesquisaFinalizadora";
            this.dgvPesquisaFinalizadora.RowHeadersVisible = false;
            this.dgvPesquisaFinalizadora.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPesquisaFinalizadora.Size = new System.Drawing.Size(534, 441);
            this.dgvPesquisaFinalizadora.TabIndex = 19;
            this.dgvPesquisaFinalizadora.DoubleClick += new System.EventHandler(this.dgvPesquisaFinalizadora_DoubleClick);
            this.dgvPesquisaFinalizadora.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvPesquisaFinalizadora_KeyDown);
            this.dgvPesquisaFinalizadora.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dgvPesquisaFinalizadora_KeyPress);
            // 
            // codigo
            // 
            this.codigo.HeaderText = "Codigo";
            this.codigo.Name = "codigo";
            this.codigo.ReadOnly = true;
            // 
            // Razao
            // 
            this.Razao.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Razao.HeaderText = "Descrição";
            this.Razao.Name = "Razao";
            this.Razao.ReadOnly = true;
            this.Razao.Width = 300;
            // 
            // pesquisaFinalizadora
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 441);
            this.Controls.Add(this.dgvPesquisaFinalizadora);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "pesquisaFinalizadora";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "pesquisaFinalizadora";
            this.Load += new System.EventHandler(this.pesquisaFinalizadora_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.pesquisaFinalizadora_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPesquisaFinalizadora)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvPesquisaFinalizadora;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Razao;
    }
}