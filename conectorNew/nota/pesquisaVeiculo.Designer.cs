namespace conectorPDV001
{
    partial class pesquisaVeiculo
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
            this.rbFindVeiculo = new System.Windows.Forms.RadioButton();
            this.rbFindDescricaoVeiculo = new System.Windows.Forms.RadioButton();
            this.rbCodigoFindVeiculo = new System.Windows.Forms.RadioButton();
            this.dgvPesquisaVeiculo = new System.Windows.Forms.DataGridView();
            this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descricao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.placa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chaveTipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtPesquisaVeiculo = new System.Windows.Forms.TextBox();
            this.lbDescricao = new System.Windows.Forms.Label();
            this.btnFindVeiculo = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPesquisaVeiculo)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbFindVeiculo);
            this.groupBox1.Controls.Add(this.rbFindDescricaoVeiculo);
            this.groupBox1.Controls.Add(this.rbCodigoFindVeiculo);
            this.groupBox1.Location = new System.Drawing.Point(1, 23);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(641, 45);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            // 
            // rbFindVeiculo
            // 
            this.rbFindVeiculo.AutoSize = true;
            this.rbFindVeiculo.Checked = true;
            this.rbFindVeiculo.Location = new System.Drawing.Point(438, 17);
            this.rbFindVeiculo.Name = "rbFindVeiculo";
            this.rbFindVeiculo.Size = new System.Drawing.Size(76, 17);
            this.rbFindVeiculo.TabIndex = 2;
            this.rbFindVeiculo.TabStop = true;
            this.rbFindVeiculo.Text = "F4 - Todos";
            this.rbFindVeiculo.UseVisualStyleBackColor = true;
            this.rbFindVeiculo.CheckedChanged += new System.EventHandler(this.rbFindVeiculo_CheckedChanged);
            // 
            // rbFindDescricaoVeiculo
            // 
            this.rbFindDescricaoVeiculo.AutoSize = true;
            this.rbFindDescricaoVeiculo.Location = new System.Drawing.Point(268, 17);
            this.rbFindDescricaoVeiculo.Name = "rbFindDescricaoVeiculo";
            this.rbFindDescricaoVeiculo.Size = new System.Drawing.Size(94, 17);
            this.rbFindDescricaoVeiculo.TabIndex = 1;
            this.rbFindDescricaoVeiculo.Text = "F3 - Descrição";
            this.rbFindDescricaoVeiculo.UseVisualStyleBackColor = true;
            this.rbFindDescricaoVeiculo.CheckedChanged += new System.EventHandler(this.rbFindDescricaoVeiculo_CheckedChanged);
            // 
            // rbCodigoFindVeiculo
            // 
            this.rbCodigoFindVeiculo.AutoSize = true;
            this.rbCodigoFindVeiculo.Location = new System.Drawing.Point(113, 17);
            this.rbCodigoFindVeiculo.Name = "rbCodigoFindVeiculo";
            this.rbCodigoFindVeiculo.Size = new System.Drawing.Size(79, 17);
            this.rbCodigoFindVeiculo.TabIndex = 0;
            this.rbCodigoFindVeiculo.Text = "F2 - Codigo";
            this.rbCodigoFindVeiculo.UseVisualStyleBackColor = true;
            this.rbCodigoFindVeiculo.CheckedChanged += new System.EventHandler(this.rbCodigoFindVeiculo_CheckedChanged);
            // 
            // dgvPesquisaVeiculo
            // 
            this.dgvPesquisaVeiculo.AllowUserToDeleteRows = false;
            this.dgvPesquisaVeiculo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPesquisaVeiculo.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvPesquisaVeiculo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPesquisaVeiculo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.codigo,
            this.Descricao,
            this.placa,
            this.estado,
            this.tipo,
            this.chaveTipo});
            this.dgvPesquisaVeiculo.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dgvPesquisaVeiculo.Location = new System.Drawing.Point(1, 72);
            this.dgvPesquisaVeiculo.Name = "dgvPesquisaVeiculo";
            this.dgvPesquisaVeiculo.RowHeadersVisible = false;
            this.dgvPesquisaVeiculo.Size = new System.Drawing.Size(641, 189);
            this.dgvPesquisaVeiculo.TabIndex = 8;
            this.dgvPesquisaVeiculo.DoubleClick += new System.EventHandler(this.dgvPesquisaVeiculo_DoubleClick);
            this.dgvPesquisaVeiculo.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPesquisaVeiculo_CellClick);
            this.dgvPesquisaVeiculo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dgvPesquisaVeiculo_KeyPress);
            this.dgvPesquisaVeiculo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgvPesquisaVeiculo_KeyUp);
            // 
            // codigo
            // 
            this.codigo.HeaderText = "Codigo";
            this.codigo.Name = "codigo";
            this.codigo.ReadOnly = true;
            // 
            // Descricao
            // 
            this.Descricao.HeaderText = "Descricao";
            this.Descricao.Name = "Descricao";
            this.Descricao.ReadOnly = true;
            // 
            // placa
            // 
            this.placa.HeaderText = "Placa";
            this.placa.Name = "placa";
            this.placa.ReadOnly = true;
            // 
            // estado
            // 
            this.estado.HeaderText = "UF";
            this.estado.Name = "estado";
            this.estado.ReadOnly = true;
            // 
            // tipo
            // 
            this.tipo.HeaderText = "Tipo";
            this.tipo.Name = "tipo";
            this.tipo.ReadOnly = true;
            // 
            // chaveTipo
            // 
            this.chaveTipo.HeaderText = "Chave Tipo";
            this.chaveTipo.Name = "chaveTipo";
            this.chaveTipo.ReadOnly = true;
            // 
            // txtPesquisaVeiculo
            // 
            this.txtPesquisaVeiculo.Location = new System.Drawing.Point(65, 3);
            this.txtPesquisaVeiculo.Name = "txtPesquisaVeiculo";
            this.txtPesquisaVeiculo.Size = new System.Drawing.Size(498, 20);
            this.txtPesquisaVeiculo.TabIndex = 7;
            // 
            // lbDescricao
            // 
            this.lbDescricao.AutoSize = true;
            this.lbDescricao.Location = new System.Drawing.Point(7, 6);
            this.lbDescricao.Name = "lbDescricao";
            this.lbDescricao.Size = new System.Drawing.Size(55, 13);
            this.lbDescricao.TabIndex = 6;
            this.lbDescricao.Text = "Descrição";
            // 
            // btnFindVeiculo
            // 
            this.btnFindVeiculo.Location = new System.Drawing.Point(563, 1);
            this.btnFindVeiculo.Name = "btnFindVeiculo";
            this.btnFindVeiculo.Size = new System.Drawing.Size(73, 23);
            this.btnFindVeiculo.TabIndex = 5;
            this.btnFindVeiculo.Text = "Pesquisa";
            this.btnFindVeiculo.UseVisualStyleBackColor = true;
            this.btnFindVeiculo.Click += new System.EventHandler(this.btnFindVeiculo_Click);
            // 
            // pesquisaVeiculo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 262);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvPesquisaVeiculo);
            this.Controls.Add(this.txtPesquisaVeiculo);
            this.Controls.Add(this.lbDescricao);
            this.Controls.Add(this.btnFindVeiculo);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "pesquisaVeiculo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pesquisa Veiculo";
            this.Load += new System.EventHandler(this.pesquisaVeiculo_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.pesquisaVeiculo_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPesquisaVeiculo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbFindVeiculo;
        private System.Windows.Forms.RadioButton rbFindDescricaoVeiculo;
        private System.Windows.Forms.RadioButton rbCodigoFindVeiculo;
        private System.Windows.Forms.DataGridView dgvPesquisaVeiculo;
        private System.Windows.Forms.TextBox txtPesquisaVeiculo;
        private System.Windows.Forms.Label lbDescricao;
        private System.Windows.Forms.Button btnFindVeiculo;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descricao;
        private System.Windows.Forms.DataGridViewTextBoxColumn placa;
        private System.Windows.Forms.DataGridViewTextBoxColumn estado;
        private System.Windows.Forms.DataGridViewTextBoxColumn tipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn chaveTipo;
    }
}