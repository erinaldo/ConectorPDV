namespace conectorPDV001
{
    partial class pesquisaCFOP
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(pesquisaCFOP));
            this.lbDescricao = new System.Windows.Forms.Label();
            this.txtDescricaoCfop = new System.Windows.Forms.TextBox();
            this.dgvPesquisaCFOP = new System.Windows.Forms.DataGridView();
            this.CFOP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descricao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.margem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.comprador = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbFindTodosCFOP = new System.Windows.Forms.RadioButton();
            this.rbFindDescricaoCFOP = new System.Windows.Forms.RadioButton();
            this.rbCodigoFindCFOP = new System.Windows.Forms.RadioButton();
            this.btnPesquisaBanco = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPesquisaCFOP)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbDescricao
            // 
            this.lbDescricao.AutoSize = true;
            this.lbDescricao.Location = new System.Drawing.Point(22, 15);
            this.lbDescricao.Name = "lbDescricao";
            this.lbDescricao.Size = new System.Drawing.Size(55, 13);
            this.lbDescricao.TabIndex = 74;
            this.lbDescricao.Text = "Descrição";
            // 
            // txtDescricaoCfop
            // 
            this.txtDescricaoCfop.Location = new System.Drawing.Point(79, 12);
            this.txtDescricaoCfop.Name = "txtDescricaoCfop";
            this.txtDescricaoCfop.Size = new System.Drawing.Size(392, 20);
            this.txtDescricaoCfop.TabIndex = 73;
            // 
            // dgvPesquisaCFOP
            // 
            this.dgvPesquisaCFOP.AllowUserToDeleteRows = false;
            this.dgvPesquisaCFOP.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPesquisaCFOP.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvPesquisaCFOP.BackgroundColor = System.Drawing.SystemColors.Info;
            this.dgvPesquisaCFOP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPesquisaCFOP.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CFOP,
            this.descricao,
            this.margem,
            this.status,
            this.comprador});
            this.dgvPesquisaCFOP.GridColor = System.Drawing.Color.Gray;
            this.dgvPesquisaCFOP.Location = new System.Drawing.Point(1, 88);
            this.dgvPesquisaCFOP.Name = "dgvPesquisaCFOP";
            this.dgvPesquisaCFOP.RowHeadersVisible = false;
            this.dgvPesquisaCFOP.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPesquisaCFOP.Size = new System.Drawing.Size(551, 187);
            this.dgvPesquisaCFOP.TabIndex = 72;
            this.dgvPesquisaCFOP.DoubleClick += new System.EventHandler(this.dgvPesquisaCFOP_DoubleClick);
            this.dgvPesquisaCFOP.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPesquisaCFOP_CellClick);
            this.dgvPesquisaCFOP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dgvPesquisaCFOP_KeyPress);
            this.dgvPesquisaCFOP.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgvPesquisaCFOP_KeyUp);
            // 
            // CFOP
            // 
            this.CFOP.HeaderText = "CFOP";
            this.CFOP.Name = "CFOP";
            this.CFOP.ReadOnly = true;
            // 
            // descricao
            // 
            this.descricao.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.descricao.HeaderText = "Descrição";
            this.descricao.Name = "descricao";
            this.descricao.ReadOnly = true;
            this.descricao.Width = 200;
            // 
            // margem
            // 
            this.margem.HeaderText = "Tipo";
            this.margem.Name = "margem";
            this.margem.ReadOnly = true;
            // 
            // status
            // 
            this.status.HeaderText = "Inativo";
            this.status.Name = "status";
            this.status.ReadOnly = true;
            this.status.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.status.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // comprador
            // 
            this.comprador.HeaderText = "Modelo";
            this.comprador.Name = "comprador";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbFindTodosCFOP);
            this.groupBox1.Controls.Add(this.rbFindDescricaoCFOP);
            this.groupBox1.Controls.Add(this.rbCodigoFindCFOP);
            this.groupBox1.Location = new System.Drawing.Point(1, 39);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(551, 45);
            this.groupBox1.TabIndex = 76;
            this.groupBox1.TabStop = false;
            // 
            // rbFindTodosCFOP
            // 
            this.rbFindTodosCFOP.AutoSize = true;
            this.rbFindTodosCFOP.Checked = true;
            this.rbFindTodosCFOP.Location = new System.Drawing.Point(404, 17);
            this.rbFindTodosCFOP.Name = "rbFindTodosCFOP";
            this.rbFindTodosCFOP.Size = new System.Drawing.Size(55, 17);
            this.rbFindTodosCFOP.TabIndex = 2;
            this.rbFindTodosCFOP.TabStop = true;
            this.rbFindTodosCFOP.Text = "Todos";
            this.rbFindTodosCFOP.UseVisualStyleBackColor = true;
            this.rbFindTodosCFOP.CheckedChanged += new System.EventHandler(this.rbFindTodosCFOP_CheckedChanged);
            // 
            // rbFindDescricaoCFOP
            // 
            this.rbFindDescricaoCFOP.AutoSize = true;
            this.rbFindDescricaoCFOP.Location = new System.Drawing.Point(246, 17);
            this.rbFindDescricaoCFOP.Name = "rbFindDescricaoCFOP";
            this.rbFindDescricaoCFOP.Size = new System.Drawing.Size(73, 17);
            this.rbFindDescricaoCFOP.TabIndex = 1;
            this.rbFindDescricaoCFOP.Text = "Descrição";
            this.rbFindDescricaoCFOP.UseVisualStyleBackColor = true;
            this.rbFindDescricaoCFOP.CheckedChanged += new System.EventHandler(this.rbFindDescricaoCFOP_CheckedChanged);
            // 
            // rbCodigoFindCFOP
            // 
            this.rbCodigoFindCFOP.AutoSize = true;
            this.rbCodigoFindCFOP.Location = new System.Drawing.Point(100, 17);
            this.rbCodigoFindCFOP.Name = "rbCodigoFindCFOP";
            this.rbCodigoFindCFOP.Size = new System.Drawing.Size(58, 17);
            this.rbCodigoFindCFOP.TabIndex = 0;
            this.rbCodigoFindCFOP.Text = "Codigo";
            this.rbCodigoFindCFOP.UseVisualStyleBackColor = true;
            this.rbCodigoFindCFOP.CheckedChanged += new System.EventHandler(this.rbCodigoFindCFOP_CheckedChanged);
            // 
            // btnPesquisaBanco
            // 
            this.btnPesquisaBanco.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPesquisaBanco.Image = ((System.Drawing.Image)(resources.GetObject("btnPesquisaBanco.Image")));
            this.btnPesquisaBanco.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPesquisaBanco.Location = new System.Drawing.Point(463, 10);
            this.btnPesquisaBanco.Name = "btnPesquisaBanco";
            this.btnPesquisaBanco.Size = new System.Drawing.Size(89, 23);
            this.btnPesquisaBanco.TabIndex = 75;
            this.btnPesquisaBanco.Text = "Pesquisar";
            this.btnPesquisaBanco.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPesquisaBanco.UseVisualStyleBackColor = true;
            this.btnPesquisaBanco.Click += new System.EventHandler(this.btnPesquisaAliquota_Click);
            // 
            // pesquisaCFOP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 276);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnPesquisaBanco);
            this.Controls.Add(this.lbDescricao);
            this.Controls.Add(this.txtDescricaoCfop);
            this.Controls.Add(this.dgvPesquisaCFOP);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "pesquisaCFOP";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "pesquisaCFOP";
            this.Load += new System.EventHandler(this.pesquisaCFOP_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.pesquisaCFOP_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPesquisaCFOP)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button btnPesquisaBanco;
        private System.Windows.Forms.Label lbDescricao;
        private System.Windows.Forms.TextBox txtDescricaoCfop;
        private System.Windows.Forms.DataGridView dgvPesquisaCFOP;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbFindTodosCFOP;
        private System.Windows.Forms.RadioButton rbFindDescricaoCFOP;
        private System.Windows.Forms.RadioButton rbCodigoFindCFOP;
        private System.Windows.Forms.DataGridViewTextBoxColumn CFOP;
        private System.Windows.Forms.DataGridViewTextBoxColumn descricao;
        private System.Windows.Forms.DataGridViewTextBoxColumn margem;
        private System.Windows.Forms.DataGridViewCheckBoxColumn status;
        private System.Windows.Forms.DataGridViewTextBoxColumn comprador;
    }
}