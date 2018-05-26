namespace conectorPDV001
{
    partial class pesquisaLoja
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
            this.rbFindLoja = new System.Windows.Forms.RadioButton();
            this.rbFindDescricaoLoja = new System.Windows.Forms.RadioButton();
            this.rbCodigoFindLoja = new System.Windows.Forms.RadioButton();
            this.dgvPesquisaLoja = new System.Windows.Forms.DataGridView();
            this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Razao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uf = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cnpj = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IEm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodigoEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lojauf = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cod_mun = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TypeLoja = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aliquotaPis = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AliquotaCofins = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ControlaEstoque = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TypeCalculo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmpresaTroca = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AliquotaInss = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AliquotaIss = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Matriz = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Deposito = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SerieNota = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumeroNota = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AtualizaCusto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lojastatus = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ramo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bairro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.complemento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.municipio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cep = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cod_endereco = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.seq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idcepbairro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idenderecoType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.endereco = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtPesquisaLoja = new System.Windows.Forms.TextBox();
            this.lbDescricao = new System.Windows.Forms.Label();
            this.btnFindLoja = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPesquisaLoja)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbFindLoja);
            this.groupBox1.Controls.Add(this.rbFindDescricaoLoja);
            this.groupBox1.Controls.Add(this.rbCodigoFindLoja);
            this.groupBox1.Location = new System.Drawing.Point(0, 20);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(672, 45);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // rbFindLoja
            // 
            this.rbFindLoja.AutoSize = true;
            this.rbFindLoja.Checked = true;
            this.rbFindLoja.Location = new System.Drawing.Point(493, 16);
            this.rbFindLoja.Name = "rbFindLoja";
            this.rbFindLoja.Size = new System.Drawing.Size(55, 17);
            this.rbFindLoja.TabIndex = 2;
            this.rbFindLoja.TabStop = true;
            this.rbFindLoja.Text = "Todos";
            this.rbFindLoja.UseVisualStyleBackColor = true;
            this.rbFindLoja.CheckedChanged += new System.EventHandler(this.rbFindLoja_CheckedChanged);
            // 
            // rbFindDescricaoLoja
            // 
            this.rbFindDescricaoLoja.AutoSize = true;
            this.rbFindDescricaoLoja.Location = new System.Drawing.Point(317, 16);
            this.rbFindDescricaoLoja.Name = "rbFindDescricaoLoja";
            this.rbFindDescricaoLoja.Size = new System.Drawing.Size(73, 17);
            this.rbFindDescricaoLoja.TabIndex = 1;
            this.rbFindDescricaoLoja.Text = "Descrição";
            this.rbFindDescricaoLoja.UseVisualStyleBackColor = true;
            this.rbFindDescricaoLoja.CheckedChanged += new System.EventHandler(this.rbFindDescricaoLoja_CheckedChanged);
            // 
            // rbCodigoFindLoja
            // 
            this.rbCodigoFindLoja.AutoSize = true;
            this.rbCodigoFindLoja.Location = new System.Drawing.Point(144, 16);
            this.rbCodigoFindLoja.Name = "rbCodigoFindLoja";
            this.rbCodigoFindLoja.Size = new System.Drawing.Size(58, 17);
            this.rbCodigoFindLoja.TabIndex = 0;
            this.rbCodigoFindLoja.Text = "Codigo";
            this.rbCodigoFindLoja.UseVisualStyleBackColor = true;
            this.rbCodigoFindLoja.CheckedChanged += new System.EventHandler(this.rbCodigoFindLoja_CheckedChanged);
            // 
            // dgvPesquisaLoja
            // 
            this.dgvPesquisaLoja.AllowUserToDeleteRows = false;
            this.dgvPesquisaLoja.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvPesquisaLoja.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvPesquisaLoja.BackgroundColor = System.Drawing.SystemColors.Info;
            this.dgvPesquisaLoja.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvPesquisaLoja.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.codigo,
            this.Razao,
            this.uf,
            this.cnpj,
            this.IE,
            this.IEm,
            this.type,
            this.CodigoEstado,
            this.lojauf,
            this.cod_mun,
            this.TypeLoja,
            this.aliquotaPis,
            this.AliquotaCofins,
            this.ControlaEstoque,
            this.TypeCalculo,
            this.EmpresaTroca,
            this.AliquotaInss,
            this.AliquotaIss,
            this.Matriz,
            this.Deposito,
            this.SerieNota,
            this.NumeroNota,
            this.AtualizaCusto,
            this.lojastatus,
            this.ramo,
            this.bairro,
            this.complemento,
            this.municipio,
            this.estado,
            this.number,
            this.cep,
            this.cod_endereco,
            this.seq,
            this.idcepbairro,
            this.idenderecoType,
            this.endereco});
            this.dgvPesquisaLoja.GridColor = System.Drawing.Color.Black;
            this.dgvPesquisaLoja.Location = new System.Drawing.Point(0, 64);
            this.dgvPesquisaLoja.Name = "dgvPesquisaLoja";
            this.dgvPesquisaLoja.RowHeadersVisible = false;
            this.dgvPesquisaLoja.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPesquisaLoja.Size = new System.Drawing.Size(672, 244);
            this.dgvPesquisaLoja.TabIndex = 18;
            this.dgvPesquisaLoja.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPesquisaLoja_CellDoubleClick);
            this.dgvPesquisaLoja.DoubleClick += new System.EventHandler(this.dgvPesquisaLoja_DoubleClick);
            this.dgvPesquisaLoja.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dgvPesquisaLoja_KeyPress);
            this.dgvPesquisaLoja.Click += new System.EventHandler(this.dgvPesquisaLoja_Click);
            this.dgvPesquisaLoja.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgvPesquisaLoja_KeyUp);
            this.dgvPesquisaLoja.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPesquisaLoja_CellContentClick);
            // 
            // codigo
            // 
            this.codigo.FillWeight = 538.1167F;
            this.codigo.HeaderText = "Codigo";
            this.codigo.Name = "codigo";
            this.codigo.ReadOnly = true;
            this.codigo.Width = 65;
            // 
            // Razao
            // 
            this.Razao.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Razao.FillWeight = 457.7346F;
            this.Razao.HeaderText = "Razão";
            this.Razao.Name = "Razao";
            this.Razao.ReadOnly = true;
            this.Razao.Width = 263;
            // 
            // uf
            // 
            this.uf.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.uf.FillWeight = 389.3678F;
            this.uf.HeaderText = "Abreviatura";
            this.uf.Name = "uf";
            this.uf.ReadOnly = true;
            this.uf.Width = 186;
            // 
            // cnpj
            // 
            this.cnpj.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.cnpj.FillWeight = 331.2205F;
            this.cnpj.HeaderText = "CNPJ";
            this.cnpj.Name = "cnpj";
            this.cnpj.ReadOnly = true;
            this.cnpj.Width = 99;
            // 
            // IE
            // 
            this.IE.FillWeight = 281.7647F;
            this.IE.HeaderText = "IE";
            this.IE.Name = "IE";
            this.IE.ReadOnly = true;
            this.IE.Width = 42;
            // 
            // IEm
            // 
            this.IEm.FillWeight = 239.7013F;
            this.IEm.HeaderText = "IE Municipal";
            this.IEm.Name = "IEm";
            this.IEm.ReadOnly = true;
            this.IEm.Width = 90;
            // 
            // type
            // 
            this.type.FillWeight = 203.9255F;
            this.type.HeaderText = "Tipo Empresa";
            this.type.Name = "type";
            this.type.ReadOnly = true;
            this.type.Width = 97;
            // 
            // CodigoEstado
            // 
            this.CodigoEstado.FillWeight = 173.4974F;
            this.CodigoEstado.HeaderText = "Codigo Estado";
            this.CodigoEstado.Name = "CodigoEstado";
            this.CodigoEstado.ReadOnly = true;
            this.CodigoEstado.Width = 101;
            // 
            // lojauf
            // 
            this.lojauf.FillWeight = 147.6175F;
            this.lojauf.HeaderText = "UF";
            this.lojauf.Name = "lojauf";
            this.lojauf.ReadOnly = true;
            this.lojauf.Width = 46;
            // 
            // cod_mun
            // 
            this.cod_mun.FillWeight = 125.6061F;
            this.cod_mun.HeaderText = "Codigo Municipio";
            this.cod_mun.Name = "cod_mun";
            this.cod_mun.ReadOnly = true;
            this.cod_mun.Width = 113;
            // 
            // TypeLoja
            // 
            this.TypeLoja.FillWeight = 106.8849F;
            this.TypeLoja.HeaderText = "TypeLoja";
            this.TypeLoja.Name = "TypeLoja";
            this.TypeLoja.ReadOnly = true;
            this.TypeLoja.Width = 76;
            // 
            // aliquotaPis
            // 
            this.aliquotaPis.FillWeight = 90.96204F;
            this.aliquotaPis.HeaderText = "aliquotaPis";
            this.aliquotaPis.Name = "aliquotaPis";
            this.aliquotaPis.ReadOnly = true;
            this.aliquotaPis.Width = 83;
            // 
            // AliquotaCofins
            // 
            this.AliquotaCofins.FillWeight = 77.41932F;
            this.AliquotaCofins.HeaderText = "AliquotaCofins";
            this.AliquotaCofins.Name = "AliquotaCofins";
            this.AliquotaCofins.ReadOnly = true;
            this.AliquotaCofins.Width = 99;
            // 
            // ControlaEstoque
            // 
            this.ControlaEstoque.FillWeight = 65.90089F;
            this.ControlaEstoque.HeaderText = "ControlaEstoque";
            this.ControlaEstoque.Name = "ControlaEstoque";
            this.ControlaEstoque.ReadOnly = true;
            this.ControlaEstoque.Width = 110;
            // 
            // TypeCalculo
            // 
            this.TypeCalculo.FillWeight = 56.10424F;
            this.TypeCalculo.HeaderText = "TypeCalculo";
            this.TypeCalculo.Name = "TypeCalculo";
            this.TypeCalculo.ReadOnly = true;
            this.TypeCalculo.Width = 91;
            // 
            // EmpresaTroca
            // 
            this.EmpresaTroca.FillWeight = 47.77195F;
            this.EmpresaTroca.HeaderText = "EmpresaTroca";
            this.EmpresaTroca.Name = "EmpresaTroca";
            this.EmpresaTroca.ReadOnly = true;
            this.EmpresaTroca.Width = 101;
            // 
            // AliquotaInss
            // 
            this.AliquotaInss.FillWeight = 40.68512F;
            this.AliquotaInss.HeaderText = "AliquotaInss";
            this.AliquotaInss.Name = "AliquotaInss";
            this.AliquotaInss.ReadOnly = true;
            this.AliquotaInss.Width = 89;
            // 
            // AliquotaIss
            // 
            this.AliquotaIss.FillWeight = 34.65762F;
            this.AliquotaIss.HeaderText = "AliquotaIss";
            this.AliquotaIss.Name = "AliquotaIss";
            this.AliquotaIss.ReadOnly = true;
            this.AliquotaIss.Width = 83;
            // 
            // Matriz
            // 
            this.Matriz.FillWeight = 29.53109F;
            this.Matriz.HeaderText = "Matriz";
            this.Matriz.Name = "Matriz";
            this.Matriz.ReadOnly = true;
            this.Matriz.Width = 60;
            // 
            // Deposito
            // 
            this.Deposito.FillWeight = 25.17086F;
            this.Deposito.HeaderText = "Deposito";
            this.Deposito.Name = "Deposito";
            this.Deposito.ReadOnly = true;
            this.Deposito.Width = 74;
            // 
            // SerieNota
            // 
            this.SerieNota.FillWeight = 21.46239F;
            this.SerieNota.HeaderText = "SerieNota";
            this.SerieNota.Name = "SerieNota";
            this.SerieNota.ReadOnly = true;
            this.SerieNota.Width = 79;
            // 
            // NumeroNota
            // 
            this.NumeroNota.FillWeight = 18.30824F;
            this.NumeroNota.HeaderText = "NumeroNota";
            this.NumeroNota.Name = "NumeroNota";
            this.NumeroNota.ReadOnly = true;
            this.NumeroNota.Width = 92;
            // 
            // AtualizaCusto
            // 
            this.AtualizaCusto.FillWeight = 15.62557F;
            this.AtualizaCusto.HeaderText = "AtualizaCusto";
            this.AtualizaCusto.Name = "AtualizaCusto";
            this.AtualizaCusto.ReadOnly = true;
            this.AtualizaCusto.Width = 96;
            // 
            // lojastatus
            // 
            this.lojastatus.FillWeight = 13.34389F;
            this.lojastatus.HeaderText = "STATUS";
            this.lojastatus.Name = "lojastatus";
            this.lojastatus.ReadOnly = true;
            this.lojastatus.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.lojastatus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.lojastatus.Width = 75;
            // 
            // ramo
            // 
            this.ramo.FillWeight = 11.40328F;
            this.ramo.HeaderText = "Ramo";
            this.ramo.Name = "ramo";
            this.ramo.ReadOnly = true;
            this.ramo.Width = 60;
            // 
            // bairro
            // 
            this.bairro.FillWeight = 9.752734F;
            this.bairro.HeaderText = "bairro";
            this.bairro.Name = "bairro";
            this.bairro.ReadOnly = true;
            this.bairro.Width = 58;
            // 
            // complemento
            // 
            this.complemento.FillWeight = 8.348909F;
            this.complemento.HeaderText = "Complemento";
            this.complemento.Name = "complemento";
            this.complemento.ReadOnly = true;
            this.complemento.Width = 96;
            // 
            // municipio
            // 
            this.municipio.FillWeight = 7.154929F;
            this.municipio.HeaderText = "Cidade";
            this.municipio.Name = "municipio";
            this.municipio.ReadOnly = true;
            this.municipio.Width = 65;
            // 
            // estado
            // 
            this.estado.FillWeight = 6.139414F;
            this.estado.HeaderText = "Estado";
            this.estado.Name = "estado";
            this.estado.ReadOnly = true;
            this.estado.Width = 65;
            // 
            // number
            // 
            this.number.FillWeight = 5.275701F;
            this.number.HeaderText = "Numero";
            this.number.Name = "number";
            this.number.ReadOnly = true;
            this.number.Width = 69;
            // 
            // cep
            // 
            this.cep.FillWeight = 4.54109F;
            this.cep.HeaderText = "CEP";
            this.cep.Name = "cep";
            this.cep.ReadOnly = true;
            this.cep.Width = 53;
            // 
            // cod_endereco
            // 
            this.cod_endereco.FillWeight = 3.916288F;
            this.cod_endereco.HeaderText = "Codigo Endereco";
            this.cod_endereco.Name = "cod_endereco";
            this.cod_endereco.ReadOnly = true;
            this.cod_endereco.Width = 114;
            // 
            // seq
            // 
            this.seq.FillWeight = 3.384878F;
            this.seq.HeaderText = "Sequencia";
            this.seq.Name = "seq";
            this.seq.ReadOnly = true;
            this.seq.Width = 83;
            // 
            // idcepbairro
            // 
            this.idcepbairro.FillWeight = 2.932901F;
            this.idcepbairro.HeaderText = "Codigo Bairro";
            this.idcepbairro.Name = "idcepbairro";
            this.idcepbairro.ReadOnly = true;
            this.idcepbairro.Width = 95;
            // 
            // idenderecoType
            // 
            this.idenderecoType.FillWeight = 2.548485F;
            this.idenderecoType.HeaderText = "Tipo Endereco";
            this.idenderecoType.Name = "idenderecoType";
            this.idenderecoType.ReadOnly = true;
            this.idenderecoType.Width = 102;
            // 
            // endereco
            // 
            this.endereco.FillWeight = 2.22153F;
            this.endereco.HeaderText = "Logradouro";
            this.endereco.Name = "endereco";
            this.endereco.ReadOnly = true;
            this.endereco.Width = 86;
            // 
            // txtPesquisaLoja
            // 
            this.txtPesquisaLoja.Location = new System.Drawing.Point(60, 1);
            this.txtPesquisaLoja.Name = "txtPesquisaLoja";
            this.txtPesquisaLoja.Size = new System.Drawing.Size(519, 20);
            this.txtPesquisaLoja.TabIndex = 17;
            // 
            // lbDescricao
            // 
            this.lbDescricao.AutoSize = true;
            this.lbDescricao.Location = new System.Drawing.Point(2, 4);
            this.lbDescricao.Name = "lbDescricao";
            this.lbDescricao.Size = new System.Drawing.Size(55, 13);
            this.lbDescricao.TabIndex = 16;
            this.lbDescricao.Text = "Descrição";
            // 
            // btnFindLoja
            // 
            this.btnFindLoja.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFindLoja.Location = new System.Drawing.Point(576, 0);
            this.btnFindLoja.Name = "btnFindLoja";
            this.btnFindLoja.Size = new System.Drawing.Size(96, 23);
            this.btnFindLoja.TabIndex = 15;
            this.btnFindLoja.Text = "Pesquisa";
            this.btnFindLoja.UseVisualStyleBackColor = true;
            this.btnFindLoja.Click += new System.EventHandler(this.btnFindLoja_Click);
            // 
            // pesquisaLoja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(673, 309);
            this.Controls.Add(this.btnFindLoja);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvPesquisaLoja);
            this.Controls.Add(this.txtPesquisaLoja);
            this.Controls.Add(this.lbDescricao);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "pesquisaLoja";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "pesquisaLoja";
            this.Load += new System.EventHandler(this.pesquisaLoja_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.pesquisaLoja_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPesquisaLoja)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbFindLoja;
        private System.Windows.Forms.RadioButton rbFindDescricaoLoja;
        private System.Windows.Forms.RadioButton rbCodigoFindLoja;
        private System.Windows.Forms.DataGridView dgvPesquisaLoja;
        private System.Windows.Forms.TextBox txtPesquisaLoja;
        private System.Windows.Forms.Label lbDescricao;
        private System.Windows.Forms.Button btnFindLoja;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Razao;
        private System.Windows.Forms.DataGridViewTextBoxColumn uf;
        private System.Windows.Forms.DataGridViewTextBoxColumn cnpj;
        private System.Windows.Forms.DataGridViewTextBoxColumn IE;
        private System.Windows.Forms.DataGridViewTextBoxColumn IEm;
        private System.Windows.Forms.DataGridViewTextBoxColumn type;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoEstado;
        private System.Windows.Forms.DataGridViewTextBoxColumn lojauf;
        private System.Windows.Forms.DataGridViewTextBoxColumn cod_mun;
        private System.Windows.Forms.DataGridViewTextBoxColumn TypeLoja;
        private System.Windows.Forms.DataGridViewTextBoxColumn aliquotaPis;
        private System.Windows.Forms.DataGridViewTextBoxColumn AliquotaCofins;
        private System.Windows.Forms.DataGridViewTextBoxColumn ControlaEstoque;
        private System.Windows.Forms.DataGridViewTextBoxColumn TypeCalculo;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmpresaTroca;
        private System.Windows.Forms.DataGridViewTextBoxColumn AliquotaInss;
        private System.Windows.Forms.DataGridViewTextBoxColumn AliquotaIss;
        private System.Windows.Forms.DataGridViewTextBoxColumn Matriz;
        private System.Windows.Forms.DataGridViewTextBoxColumn Deposito;
        private System.Windows.Forms.DataGridViewTextBoxColumn SerieNota;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumeroNota;
        private System.Windows.Forms.DataGridViewTextBoxColumn AtualizaCusto;
        private System.Windows.Forms.DataGridViewCheckBoxColumn lojastatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn ramo;
        private System.Windows.Forms.DataGridViewTextBoxColumn bairro;
        private System.Windows.Forms.DataGridViewTextBoxColumn complemento;
        private System.Windows.Forms.DataGridViewTextBoxColumn municipio;
        private System.Windows.Forms.DataGridViewTextBoxColumn estado;
        private System.Windows.Forms.DataGridViewTextBoxColumn number;
        private System.Windows.Forms.DataGridViewTextBoxColumn cep;
        private System.Windows.Forms.DataGridViewTextBoxColumn cod_endereco;
        private System.Windows.Forms.DataGridViewTextBoxColumn seq;
        private System.Windows.Forms.DataGridViewTextBoxColumn idcepbairro;
        private System.Windows.Forms.DataGridViewTextBoxColumn idenderecoType;
        private System.Windows.Forms.DataGridViewTextBoxColumn endereco;
    }
}