namespace conectorPDV001
{
    partial class pesquisaFornecedor
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
            this.dgvPesquisaFornecedor = new System.Windows.Forms.DataGridView();
            this.chave = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VarNomeRazao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VarUf = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VarAbertura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VarInativa = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.VarCgc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VarIe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VarInclusao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VarLoja = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VarTipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VarCadastro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VarAtividade = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rdbPessoaJuridicaFornecedor = new System.Windows.Forms.RadioButton();
            this.rdbPessoaFisicaFornecedor = new System.Windows.Forms.RadioButton();
            this.rbPessoaRuralFornecedor = new System.Windows.Forms.RadioButton();
            this.txtInformacaoFornecedor = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkTodosFornecedores = new System.Windows.Forms.CheckBox();
            this.cmbPesquisaFornecedor = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPesquisaPessoa = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPesquisaFornecedor)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvPesquisaFornecedor
            // 
            this.dgvPesquisaFornecedor.AllowUserToDeleteRows = false;
            this.dgvPesquisaFornecedor.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvPesquisaFornecedor.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvPesquisaFornecedor.BackgroundColor = System.Drawing.SystemColors.Info;
            this.dgvPesquisaFornecedor.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chave,
            this.VarNomeRazao,
            this.VarUf,
            this.VarAbertura,
            this.VarInativa,
            this.VarCgc,
            this.VarIe,
            this.VarInclusao,
            this.VarLoja,
            this.VarTipo,
            this.VarCadastro,
            this.VarAtividade});
            this.dgvPesquisaFornecedor.GridColor = System.Drawing.Color.Black;
            this.dgvPesquisaFornecedor.Location = new System.Drawing.Point(1, 104);
            this.dgvPesquisaFornecedor.Name = "dgvPesquisaFornecedor";
            this.dgvPesquisaFornecedor.RowHeadersVisible = false;
            this.dgvPesquisaFornecedor.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPesquisaFornecedor.Size = new System.Drawing.Size(842, 398);
            this.dgvPesquisaFornecedor.TabIndex = 6;
            this.dgvPesquisaFornecedor.DoubleClick += new System.EventHandler(this.dgvPesquisaFornecedor_DoubleClick);
            this.dgvPesquisaFornecedor.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPesquisaFornecedor_CellClick);
            this.dgvPesquisaFornecedor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dgvPesquisaFornecedor_KeyPress);
            this.dgvPesquisaFornecedor.Click += new System.EventHandler(this.dgvPesquisaFornecedor_Click);
            this.dgvPesquisaFornecedor.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgvPesquisaFornecedor_KeyUp);
            // 
            // chave
            // 
            this.chave.HeaderText = "Codigo";
            this.chave.Name = "chave";
            this.chave.ReadOnly = true;
            this.chave.Width = 65;
            // 
            // VarNomeRazao
            // 
            this.VarNomeRazao.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.VarNomeRazao.HeaderText = "Nome/Razao";
            this.VarNomeRazao.Name = "VarNomeRazao";
            this.VarNomeRazao.ReadOnly = true;
            this.VarNomeRazao.Width = 250;
            // 
            // VarUf
            // 
            this.VarUf.HeaderText = "UF";
            this.VarUf.Name = "VarUf";
            this.VarUf.ReadOnly = true;
            this.VarUf.Width = 46;
            // 
            // VarAbertura
            // 
            this.VarAbertura.HeaderText = "Abertura/Nascimento";
            this.VarAbertura.Name = "VarAbertura";
            this.VarAbertura.ReadOnly = true;
            this.VarAbertura.Width = 133;
            // 
            // VarInativa
            // 
            this.VarInativa.HeaderText = "Inativo";
            this.VarInativa.Name = "VarInativa";
            this.VarInativa.ReadOnly = true;
            this.VarInativa.Width = 45;
            // 
            // VarCgc
            // 
            this.VarCgc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.VarCgc.HeaderText = "CNPJ/CPF";
            this.VarCgc.Name = "VarCgc";
            this.VarCgc.ReadOnly = true;
            this.VarCgc.Width = 150;
            // 
            // VarIe
            // 
            this.VarIe.HeaderText = "IE/Identidade";
            this.VarIe.Name = "VarIe";
            this.VarIe.ReadOnly = true;
            this.VarIe.Width = 97;
            // 
            // VarInclusao
            // 
            this.VarInclusao.HeaderText = "Inclusão";
            this.VarInclusao.Name = "VarInclusao";
            this.VarInclusao.ReadOnly = true;
            this.VarInclusao.Width = 72;
            // 
            // VarLoja
            // 
            this.VarLoja.HeaderText = "Loja";
            this.VarLoja.Name = "VarLoja";
            this.VarLoja.ReadOnly = true;
            this.VarLoja.Width = 52;
            // 
            // VarTipo
            // 
            this.VarTipo.HeaderText = "Tipo";
            this.VarTipo.Name = "VarTipo";
            this.VarTipo.ReadOnly = true;
            this.VarTipo.Width = 53;
            // 
            // VarCadastro
            // 
            this.VarCadastro.HeaderText = "Cadastro";
            this.VarCadastro.Name = "VarCadastro";
            this.VarCadastro.ReadOnly = true;
            this.VarCadastro.Width = 74;
            // 
            // VarAtividade
            // 
            this.VarAtividade.HeaderText = "Atividade";
            this.VarAtividade.Name = "VarAtividade";
            this.VarAtividade.ReadOnly = true;
            this.VarAtividade.Width = 76;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.txtInformacaoFornecedor);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.chkTodosFornecedores);
            this.groupBox1.Controls.Add(this.cmbPesquisaFornecedor);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnPesquisaPessoa);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(1, -2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(842, 114);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rdbPessoaJuridicaFornecedor);
            this.groupBox3.Controls.Add(this.rdbPessoaFisicaFornecedor);
            this.groupBox3.Controls.Add(this.rbPessoaRuralFornecedor);
            this.groupBox3.Location = new System.Drawing.Point(567, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(149, 77);
            this.groupBox3.TabIndex = 101;
            this.groupBox3.TabStop = false;
            // 
            // rdbPessoaJuridicaFornecedor
            // 
            this.rdbPessoaJuridicaFornecedor.AutoSize = true;
            this.rdbPessoaJuridicaFornecedor.Checked = true;
            this.rdbPessoaJuridicaFornecedor.Location = new System.Drawing.Point(30, 56);
            this.rdbPessoaJuridicaFornecedor.Name = "rdbPessoaJuridicaFornecedor";
            this.rdbPessoaJuridicaFornecedor.Size = new System.Drawing.Size(101, 17);
            this.rdbPessoaJuridicaFornecedor.TabIndex = 3;
            this.rdbPessoaJuridicaFornecedor.TabStop = true;
            this.rdbPessoaJuridicaFornecedor.Text = "Pessoa Jurídica";
            this.rdbPessoaJuridicaFornecedor.UseVisualStyleBackColor = true;
            this.rdbPessoaJuridicaFornecedor.CheckedChanged += new System.EventHandler(this.rdbPessoaJuridicaFornecedor_CheckedChanged);
            // 
            // rdbPessoaFisicaFornecedor
            // 
            this.rdbPessoaFisicaFornecedor.AutoSize = true;
            this.rdbPessoaFisicaFornecedor.Location = new System.Drawing.Point(30, 12);
            this.rdbPessoaFisicaFornecedor.Name = "rdbPessoaFisicaFornecedor";
            this.rdbPessoaFisicaFornecedor.Size = new System.Drawing.Size(92, 17);
            this.rdbPessoaFisicaFornecedor.TabIndex = 2;
            this.rdbPessoaFisicaFornecedor.Text = "Pessoa Física";
            this.rdbPessoaFisicaFornecedor.UseVisualStyleBackColor = true;
            this.rdbPessoaFisicaFornecedor.CheckedChanged += new System.EventHandler(this.rdbPessoaFisicaFornecedor_CheckedChanged);
            // 
            // rbPessoaRuralFornecedor
            // 
            this.rbPessoaRuralFornecedor.AutoSize = true;
            this.rbPessoaRuralFornecedor.Location = new System.Drawing.Point(30, 35);
            this.rbPessoaRuralFornecedor.Name = "rbPessoaRuralFornecedor";
            this.rbPessoaRuralFornecedor.Size = new System.Drawing.Size(93, 17);
            this.rbPessoaRuralFornecedor.TabIndex = 4;
            this.rbPessoaRuralFornecedor.Text = "Produtor Rural";
            this.rbPessoaRuralFornecedor.UseVisualStyleBackColor = true;
            this.rbPessoaRuralFornecedor.CheckedChanged += new System.EventHandler(this.rbPessoaRuralFornecedor_CheckedChanged);
            // 
            // txtInformacaoFornecedor
            // 
            this.txtInformacaoFornecedor.Location = new System.Drawing.Point(106, 52);
            this.txtInformacaoFornecedor.Name = "txtInformacaoFornecedor";
            this.txtInformacaoFornecedor.Size = new System.Drawing.Size(455, 20);
            this.txtInformacaoFornecedor.TabIndex = 103;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 102;
            this.label2.Text = "Entrada de Dados";
            // 
            // chkTodosFornecedores
            // 
            this.chkTodosFornecedores.AutoSize = true;
            this.chkTodosFornecedores.Location = new System.Drawing.Point(488, 21);
            this.chkTodosFornecedores.Name = "chkTodosFornecedores";
            this.chkTodosFornecedores.Size = new System.Drawing.Size(56, 17);
            this.chkTodosFornecedores.TabIndex = 13;
            this.chkTodosFornecedores.Text = "Todos";
            this.chkTodosFornecedores.UseVisualStyleBackColor = true;
            this.chkTodosFornecedores.CheckedChanged += new System.EventHandler(this.chkTodosClientes_CheckedChanged);
            // 
            // cmbPesquisaFornecedor
            // 
            this.cmbPesquisaFornecedor.BackColor = System.Drawing.SystemColors.Control;
            this.cmbPesquisaFornecedor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPesquisaFornecedor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbPesquisaFornecedor.FormattingEnabled = true;
            this.cmbPesquisaFornecedor.Items.AddRange(new object[] {
            "Código",
            "Nome Pessoal Fisica",
            "Razão Social",
            "Nome Produtor Rural",
            "CNPJ",
            "CPF Pessoa Física",
            "CPF Produtor Rural"});
            this.cmbPesquisaFornecedor.Location = new System.Drawing.Point(106, 17);
            this.cmbPesquisaFornecedor.Name = "cmbPesquisaFornecedor";
            this.cmbPesquisaFornecedor.Size = new System.Drawing.Size(361, 21);
            this.cmbPesquisaFornecedor.TabIndex = 6;
            this.cmbPesquisaFornecedor.SelectedIndexChanged += new System.EventHandler(this.cmbPesquisaFornecedor_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Tipo pesquisa";
            // 
            // btnPesquisaPessoa
            // 
            this.btnPesquisaPessoa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPesquisaPessoa.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPesquisaPessoa.ForeColor = System.Drawing.Color.Maroon;
            this.btnPesquisaPessoa.Image = global::conector.Properties.Resources.Procurar24X24;
            this.btnPesquisaPessoa.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPesquisaPessoa.Location = new System.Drawing.Point(719, 4);
            this.btnPesquisaPessoa.Name = "btnPesquisaPessoa";
            this.btnPesquisaPessoa.Size = new System.Drawing.Size(123, 77);
            this.btnPesquisaPessoa.TabIndex = 2;
            this.btnPesquisaPessoa.Text = "Pesquisa";
            this.btnPesquisaPessoa.UseVisualStyleBackColor = true;
            this.btnPesquisaPessoa.Click += new System.EventHandler(this.btnPesquisaPessoa_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(-20, 70);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(883, 13);
            this.label9.TabIndex = 110;
            this.label9.Text = "_________________________________________________________________________________" +
                "_________________________________________________________________";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(695, 90);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(119, 13);
            this.label8.TabIndex = 109;
            this.label8.Text = "F6 - CPF Produtor Rural";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(526, 90);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(116, 13);
            this.label7.TabIndex = 108;
            this.label7.Text = "F5 - CPF Pessoa Fisica";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(149, 90);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 107;
            this.label6.Text = "F4 - CNPJ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(378, 90);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 13);
            this.label5.TabIndex = 106;
            this.label5.Text = "F3 - Razão Social";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(48, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 105;
            this.label4.Text = "F2 - Nome";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(256, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 104;
            this.label3.Text = "F1 - Codigo";
            // 
            // pesquisaFornecedor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 500);
            this.Controls.Add(this.dgvPesquisaFornecedor);
            this.Controls.Add(this.groupBox1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "pesquisaFornecedor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "pesquisaFornecedor";
            this.Load += new System.EventHandler(this.pesquisaFornecedor_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.pesquisaFornecedor_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPesquisaFornecedor)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvPesquisaFornecedor;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkTodosFornecedores;
        private System.Windows.Forms.ComboBox cmbPesquisaFornecedor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnPesquisaPessoa;
        private System.Windows.Forms.GroupBox groupBox3;
        internal System.Windows.Forms.RadioButton rbPessoaRuralFornecedor;
        internal System.Windows.Forms.RadioButton rdbPessoaJuridicaFornecedor;
        internal System.Windows.Forms.RadioButton rdbPessoaFisicaFornecedor;
        private System.Windows.Forms.TextBox txtInformacaoFornecedor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn chave;
        private System.Windows.Forms.DataGridViewTextBoxColumn VarNomeRazao;
        private System.Windows.Forms.DataGridViewTextBoxColumn VarUf;
        private System.Windows.Forms.DataGridViewTextBoxColumn VarAbertura;
        private System.Windows.Forms.DataGridViewCheckBoxColumn VarInativa;
        private System.Windows.Forms.DataGridViewTextBoxColumn VarCgc;
        private System.Windows.Forms.DataGridViewTextBoxColumn VarIe;
        private System.Windows.Forms.DataGridViewTextBoxColumn VarInclusao;
        private System.Windows.Forms.DataGridViewTextBoxColumn VarLoja;
        private System.Windows.Forms.DataGridViewTextBoxColumn VarTipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn VarCadastro;
        private System.Windows.Forms.DataGridViewTextBoxColumn VarAtividade;
    }
}