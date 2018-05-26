namespace conectorPDV001
{
    partial class pesquisaPessoa
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdbRural = new System.Windows.Forms.RadioButton();
            this.rdbJuridica = new System.Windows.Forms.RadioButton();
            this.rdbFisica = new System.Windows.Forms.RadioButton();
            this.btnPesquisaPessoa = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.chkTodosClientes = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbPesquisaCliente = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtInformacao = new System.Windows.Forms.TextBox();
            this.dgvPesquisaPessoa = new System.Windows.Forms.DataGridView();
            this.VarCodigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VarNomeRazao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VarUf = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VarNascimentoAbertura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VarInativa = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.VARCPFCNPJ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VarIdentidadeIE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VarInclusao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VarLojaCadastro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VarIdTipoPessoa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VarDescricaoPessao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VarAtividade = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPesquisaPessoa)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.btnPesquisaPessoa);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.chkTodosClientes);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmbPesquisaCliente);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtInformacao);
            this.groupBox1.Location = new System.Drawing.Point(1, -5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(883, 114);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdbRural);
            this.groupBox2.Controls.Add(this.rdbJuridica);
            this.groupBox2.Controls.Add(this.rdbFisica);
            this.groupBox2.Location = new System.Drawing.Point(592, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(149, 77);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            // 
            // rdbRural
            // 
            this.rdbRural.AutoSize = true;
            this.rdbRural.Location = new System.Drawing.Point(30, 56);
            this.rdbRural.Name = "rdbRural";
            this.rdbRural.Size = new System.Drawing.Size(93, 17);
            this.rdbRural.TabIndex = 2;
            this.rdbRural.Text = "Produtor Rural";
            this.rdbRural.UseVisualStyleBackColor = true;
            this.rdbRural.CheckedChanged += new System.EventHandler(this.rdbRural_CheckedChanged);
            // 
            // rdbJuridica
            // 
            this.rdbJuridica.AutoSize = true;
            this.rdbJuridica.Location = new System.Drawing.Point(30, 33);
            this.rdbJuridica.Name = "rdbJuridica";
            this.rdbJuridica.Size = new System.Drawing.Size(61, 17);
            this.rdbJuridica.TabIndex = 1;
            this.rdbJuridica.Text = "Juridica";
            this.rdbJuridica.UseVisualStyleBackColor = true;
            this.rdbJuridica.CheckedChanged += new System.EventHandler(this.rdbJuridica_CheckedChanged);
            // 
            // rdbFisica
            // 
            this.rdbFisica.AutoSize = true;
            this.rdbFisica.Checked = true;
            this.rdbFisica.Location = new System.Drawing.Point(30, 10);
            this.rdbFisica.Name = "rdbFisica";
            this.rdbFisica.Size = new System.Drawing.Size(52, 17);
            this.rdbFisica.TabIndex = 0;
            this.rdbFisica.TabStop = true;
            this.rdbFisica.Text = "Fisica";
            this.rdbFisica.UseVisualStyleBackColor = true;
            this.rdbFisica.CheckedChanged += new System.EventHandler(this.rdbFisica_CheckedChanged);
            // 
            // btnPesquisaPessoa
            // 
            this.btnPesquisaPessoa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPesquisaPessoa.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPesquisaPessoa.ForeColor = System.Drawing.Color.Maroon;
            this.btnPesquisaPessoa.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPesquisaPessoa.Location = new System.Drawing.Point(744, 9);
            this.btnPesquisaPessoa.Name = "btnPesquisaPessoa";
            this.btnPesquisaPessoa.Size = new System.Drawing.Size(135, 68);
            this.btnPesquisaPessoa.TabIndex = 2;
            this.btnPesquisaPessoa.Text = "Pesquisa";
            this.btnPesquisaPessoa.UseVisualStyleBackColor = true;
            this.btnPesquisaPessoa.Click += new System.EventHandler(this.btnPesquisaPessoa_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(0, 71);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(883, 13);
            this.label9.TabIndex = 14;
            this.label9.Text = "_________________________________________________________________________________" +
                "_________________________________________________________________";
            // 
            // chkTodosClientes
            // 
            this.chkTodosClientes.AutoSize = true;
            this.chkTodosClientes.Location = new System.Drawing.Point(527, 19);
            this.chkTodosClientes.Name = "chkTodosClientes";
            this.chkTodosClientes.Size = new System.Drawing.Size(56, 17);
            this.chkTodosClientes.TabIndex = 13;
            this.chkTodosClientes.Text = "Todos";
            this.chkTodosClientes.UseVisualStyleBackColor = true;
            this.chkTodosClientes.CheckedChanged += new System.EventHandler(this.chkTodosClientes_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(715, 91);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(119, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "F6 - CPF Produtor Rural";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(546, 91);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(116, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "F5 - CPF Pessoa Fisica";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(169, 91);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "F4 - CNPJ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(398, 91);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "F3 - Razão Social";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(68, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "F2 - Nome";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(276, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "F1 - Codigo";
            // 
            // cmbPesquisaCliente
            // 
            this.cmbPesquisaCliente.BackColor = System.Drawing.SystemColors.Control;
            this.cmbPesquisaCliente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPesquisaCliente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbPesquisaCliente.FormattingEnabled = true;
            this.cmbPesquisaCliente.Items.AddRange(new object[] {
            "Código",
            "Nome Pessoal Fisica",
            "Razão Social",
            "Nome Produtor Rural",
            "CNPJ",
            "CPF Pessoa Física",
            "CPF Produtor Rural"});
            this.cmbPesquisaCliente.Location = new System.Drawing.Point(137, 16);
            this.cmbPesquisaCliente.Name = "cmbPesquisaCliente";
            this.cmbPesquisaCliente.Size = new System.Drawing.Size(384, 21);
            this.cmbPesquisaCliente.TabIndex = 6;
            this.cmbPesquisaCliente.SelectedIndexChanged += new System.EventHandler(this.cmbPesquisaCliente_SelectedIndexChanged);
            this.cmbPesquisaCliente.TextChanged += new System.EventHandler(this.cmbPesquisaCliente_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Entrada de informações";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(62, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Tipo pesquisa";
            // 
            // txtInformacao
            // 
            this.txtInformacao.Location = new System.Drawing.Point(137, 48);
            this.txtInformacao.Name = "txtInformacao";
            this.txtInformacao.Size = new System.Drawing.Size(452, 20);
            this.txtInformacao.TabIndex = 3;
            // 
            // dgvPesquisaPessoa
            // 
            this.dgvPesquisaPessoa.AllowUserToDeleteRows = false;
            this.dgvPesquisaPessoa.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvPesquisaPessoa.BackgroundColor = System.Drawing.SystemColors.Info;
            this.dgvPesquisaPessoa.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.VarCodigo,
            this.VarNomeRazao,
            this.VarUf,
            this.VarNascimentoAbertura,
            this.VarInativa,
            this.VARCPFCNPJ,
            this.VarIdentidadeIE,
            this.VarInclusao,
            this.VarLojaCadastro,
            this.VarIdTipoPessoa,
            this.VarDescricaoPessao,
            this.VarAtividade});
            this.dgvPesquisaPessoa.GridColor = System.Drawing.Color.Black;
            this.dgvPesquisaPessoa.Location = new System.Drawing.Point(1, 105);
            this.dgvPesquisaPessoa.Name = "dgvPesquisaPessoa";
            this.dgvPesquisaPessoa.RowHeadersVisible = false;
            this.dgvPesquisaPessoa.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPesquisaPessoa.Size = new System.Drawing.Size(883, 394);
            this.dgvPesquisaPessoa.TabIndex = 4;
            this.dgvPesquisaPessoa.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPesquisaSetor_CellDoubleClick);
            this.dgvPesquisaPessoa.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPesquisaPessoa_CellClick);
            this.dgvPesquisaPessoa.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dgvPesquisaPessoa_KeyPress);
            this.dgvPesquisaPessoa.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgvPesquisaPessoa_KeyUp);
            // 
            // VarCodigo
            // 
            this.VarCodigo.HeaderText = "Codigo";
            this.VarCodigo.Name = "VarCodigo";
            this.VarCodigo.ReadOnly = true;
            // 
            // VarNomeRazao
            // 
            this.VarNomeRazao.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.VarNomeRazao.HeaderText = "Nome/Razão";
            this.VarNomeRazao.Name = "VarNomeRazao";
            this.VarNomeRazao.ReadOnly = true;
            this.VarNomeRazao.Width = 350;
            // 
            // VarUf
            // 
            this.VarUf.HeaderText = "UF";
            this.VarUf.Name = "VarUf";
            this.VarUf.ReadOnly = true;
            // 
            // VarNascimentoAbertura
            // 
            this.VarNascimentoAbertura.HeaderText = "Nascimento/Abertura";
            this.VarNascimentoAbertura.Name = "VarNascimentoAbertura";
            this.VarNascimentoAbertura.ReadOnly = true;
            // 
            // VarInativa
            // 
            this.VarInativa.HeaderText = "Inativa";
            this.VarInativa.Name = "VarInativa";
            this.VarInativa.ReadOnly = true;
            this.VarInativa.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.VarInativa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // VARCPFCNPJ
            // 
            this.VARCPFCNPJ.HeaderText = "CPF/CNPJ";
            this.VARCPFCNPJ.Name = "VARCPFCNPJ";
            this.VARCPFCNPJ.ReadOnly = true;
            // 
            // VarIdentidadeIE
            // 
            this.VarIdentidadeIE.HeaderText = "Identidade/IE";
            this.VarIdentidadeIE.Name = "VarIdentidadeIE";
            this.VarIdentidadeIE.ReadOnly = true;
            // 
            // VarInclusao
            // 
            this.VarInclusao.HeaderText = "Inclusão";
            this.VarInclusao.Name = "VarInclusao";
            this.VarInclusao.ReadOnly = true;
            // 
            // VarLojaCadastro
            // 
            this.VarLojaCadastro.HeaderText = "Loja";
            this.VarLojaCadastro.Name = "VarLojaCadastro";
            this.VarLojaCadastro.ReadOnly = true;
            // 
            // VarIdTipoPessoa
            // 
            this.VarIdTipoPessoa.HeaderText = "Tipo";
            this.VarIdTipoPessoa.Name = "VarIdTipoPessoa";
            this.VarIdTipoPessoa.ReadOnly = true;
            // 
            // VarDescricaoPessao
            // 
            this.VarDescricaoPessao.HeaderText = "Cadastro";
            this.VarDescricaoPessao.Name = "VarDescricaoPessao";
            this.VarDescricaoPessao.ReadOnly = true;
            // 
            // VarAtividade
            // 
            this.VarAtividade.HeaderText = "Atividade";
            this.VarAtividade.Name = "VarAtividade";
            this.VarAtividade.ReadOnly = true;
            // 
            // pesquisaPessoa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 500);
            this.Controls.Add(this.dgvPesquisaPessoa);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "pesquisaPessoa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "pesquisaCliente";
            this.Load += new System.EventHandler(this.pesquisaPessoa_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.pesquisaPessoa_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPesquisaPessoa)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtInformacao;
        private System.Windows.Forms.ComboBox cmbPesquisaCliente;
        private System.Windows.Forms.DataGridView dgvPesquisaPessoa;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkTodosClientes;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rdbRural;
        private System.Windows.Forms.RadioButton rdbJuridica;
        private System.Windows.Forms.RadioButton rdbFisica;
        private System.Windows.Forms.Button btnPesquisaPessoa;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridViewTextBoxColumn VarCodigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn VarNomeRazao;
        private System.Windows.Forms.DataGridViewTextBoxColumn VarUf;
        private System.Windows.Forms.DataGridViewTextBoxColumn VarNascimentoAbertura;
        private System.Windows.Forms.DataGridViewCheckBoxColumn VarInativa;
        private System.Windows.Forms.DataGridViewTextBoxColumn VARCPFCNPJ;
        private System.Windows.Forms.DataGridViewTextBoxColumn VarIdentidadeIE;
        private System.Windows.Forms.DataGridViewTextBoxColumn VarInclusao;
        private System.Windows.Forms.DataGridViewTextBoxColumn VarLojaCadastro;
        private System.Windows.Forms.DataGridViewTextBoxColumn VarIdTipoPessoa;
        private System.Windows.Forms.DataGridViewTextBoxColumn VarDescricaoPessao;
        private System.Windows.Forms.DataGridViewTextBoxColumn VarAtividade;
    }
}