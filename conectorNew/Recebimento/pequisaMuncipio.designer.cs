namespace conectorPDV001
{
    partial class pequisaMuncipio
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbFindMunicipio = new System.Windows.Forms.RadioButton();
            this.rbFindDescricaoMunicipio = new System.Windows.Forms.RadioButton();
            this.rbCodigoFindMunicipio = new System.Windows.Forms.RadioButton();
            this.dgvPesquisaMunicipio = new System.Windows.Forms.DataGridView();
            this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descricao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uf = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtPesquisaMunicipio = new System.Windows.Forms.TextBox();
            this.lbDescricao = new System.Windows.Forms.Label();
            this.btnFindMunicipio = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPesquisaMunicipio)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbFindMunicipio);
            this.groupBox1.Controls.Add(this.rbFindDescricaoMunicipio);
            this.groupBox1.Controls.Add(this.rbCodigoFindMunicipio);
            this.groupBox1.Location = new System.Drawing.Point(1, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(562, 45);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            // 
            // rbFindMunicipio
            // 
            this.rbFindMunicipio.AutoSize = true;
            this.rbFindMunicipio.Location = new System.Drawing.Point(418, 17);
            this.rbFindMunicipio.Name = "rbFindMunicipio";
            this.rbFindMunicipio.Size = new System.Drawing.Size(55, 17);
            this.rbFindMunicipio.TabIndex = 2;
            this.rbFindMunicipio.Text = "Todos";
            this.rbFindMunicipio.UseVisualStyleBackColor = true;
            this.rbFindMunicipio.CheckedChanged += new System.EventHandler(this.rbFindMunicipio_CheckedChanged);
            // 
            // rbFindDescricaoMunicipio
            // 
            this.rbFindDescricaoMunicipio.AutoSize = true;
            this.rbFindDescricaoMunicipio.Checked = true;
            this.rbFindDescricaoMunicipio.Location = new System.Drawing.Point(256, 17);
            this.rbFindDescricaoMunicipio.Name = "rbFindDescricaoMunicipio";
            this.rbFindDescricaoMunicipio.Size = new System.Drawing.Size(73, 17);
            this.rbFindDescricaoMunicipio.TabIndex = 1;
            this.rbFindDescricaoMunicipio.TabStop = true;
            this.rbFindDescricaoMunicipio.Text = "Descrição";
            this.rbFindDescricaoMunicipio.UseVisualStyleBackColor = true;
            this.rbFindDescricaoMunicipio.CheckedChanged += new System.EventHandler(this.rbFindDescricaoMunicipio_CheckedChanged);
            // 
            // rbCodigoFindMunicipio
            // 
            this.rbCodigoFindMunicipio.AutoSize = true;
            this.rbCodigoFindMunicipio.Location = new System.Drawing.Point(85, 17);
            this.rbCodigoFindMunicipio.Name = "rbCodigoFindMunicipio";
            this.rbCodigoFindMunicipio.Size = new System.Drawing.Size(58, 17);
            this.rbCodigoFindMunicipio.TabIndex = 0;
            this.rbCodigoFindMunicipio.Text = "Codigo";
            this.rbCodigoFindMunicipio.UseVisualStyleBackColor = true;
            this.rbCodigoFindMunicipio.CheckedChanged += new System.EventHandler(this.rbCodigoFindMunicipio_CheckedChanged);
            // 
            // dgvPesquisaMunicipio
            // 
            this.dgvPesquisaMunicipio.AllowUserToDeleteRows = false;
            this.dgvPesquisaMunicipio.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvPesquisaMunicipio.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvPesquisaMunicipio.BackgroundColor = System.Drawing.SystemColors.Info;
            this.dgvPesquisaMunicipio.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.codigo,
            this.descricao,
            this.uf});
            this.dgvPesquisaMunicipio.GridColor = System.Drawing.Color.Gray;
            this.dgvPesquisaMunicipio.Location = new System.Drawing.Point(1, 71);
            this.dgvPesquisaMunicipio.Name = "dgvPesquisaMunicipio";
            this.dgvPesquisaMunicipio.RowHeadersVisible = false;
            this.dgvPesquisaMunicipio.Size = new System.Drawing.Size(562, 160);
            this.dgvPesquisaMunicipio.TabIndex = 13;
            this.dgvPesquisaMunicipio.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPesquisaMunicipio_CellContentClick);
            this.dgvPesquisaMunicipio.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPesquisaMunicipio_CellContentClick);
            // 
            // codigo
            // 
            this.codigo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.codigo.HeaderText = "Codigo";
            this.codigo.Name = "codigo";
            this.codigo.ReadOnly = true;
            this.codigo.Width = 125;
            // 
            // descricao
            // 
            this.descricao.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.descricao.HeaderText = "Cidade";
            this.descricao.Name = "descricao";
            this.descricao.ReadOnly = true;
            this.descricao.Width = 365;
            // 
            // uf
            // 
            this.uf.HeaderText = "Unidade Federativa";
            this.uf.Name = "uf";
            this.uf.ReadOnly = true;
            this.uf.Width = 125;
            // 
            // txtPesquisaMunicipio
            // 
            this.txtPesquisaMunicipio.Location = new System.Drawing.Point(65, 2);
            this.txtPesquisaMunicipio.Name = "txtPesquisaMunicipio";
            this.txtPesquisaMunicipio.Size = new System.Drawing.Size(408, 20);
            this.txtPesquisaMunicipio.TabIndex = 12;
            this.txtPesquisaMunicipio.TextChanged += new System.EventHandler(this.txtPesquisaMunicipio_TextChanged);
            // 
            // lbDescricao
            // 
            this.lbDescricao.AutoSize = true;
            this.lbDescricao.Location = new System.Drawing.Point(7, 5);
            this.lbDescricao.Name = "lbDescricao";
            this.lbDescricao.Size = new System.Drawing.Size(55, 13);
            this.lbDescricao.TabIndex = 11;
            this.lbDescricao.Text = "Descrição";
            // 
            // btnFindMunicipio
            // 
            this.btnFindMunicipio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFindMunicipio.Location = new System.Drawing.Point(470, 0);
            this.btnFindMunicipio.Name = "btnFindMunicipio";
            this.btnFindMunicipio.Size = new System.Drawing.Size(92, 23);
            this.btnFindMunicipio.TabIndex = 10;
            this.btnFindMunicipio.Text = "Pesquisa";
            this.btnFindMunicipio.UseVisualStyleBackColor = true;
            this.btnFindMunicipio.Click += new System.EventHandler(this.btnFindMunicipio_Click);
            // 
            // pequisaMuncipio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(563, 232);
            this.Controls.Add(this.btnFindMunicipio);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvPesquisaMunicipio);
            this.Controls.Add(this.txtPesquisaMunicipio);
            this.Controls.Add(this.lbDescricao);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "pequisaMuncipio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "pequisaMuncipio";
            this.Load += new System.EventHandler(this.pequisaMuncipio_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.pequisaMuncipio_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPesquisaMunicipio)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbFindMunicipio;
        private System.Windows.Forms.RadioButton rbFindDescricaoMunicipio;
        private System.Windows.Forms.RadioButton rbCodigoFindMunicipio;
        private System.Windows.Forms.DataGridView dgvPesquisaMunicipio;
        private System.Windows.Forms.TextBox txtPesquisaMunicipio;
        private System.Windows.Forms.Label lbDescricao;
        private System.Windows.Forms.Button btnFindMunicipio;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn descricao;
        private System.Windows.Forms.DataGridViewTextBoxColumn uf;
    }
}