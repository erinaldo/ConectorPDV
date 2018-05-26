namespace conectorPDV001
{
    partial class telefone
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(telefone));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnPesquisaCliente = new System.Windows.Forms.Button();
            this.txtFoneComplemento = new System.Windows.Forms.MaskedTextBox();
            this.txtIdCliente = new System.Windows.Forms.TextBox();
            this.label36 = new System.Windows.Forms.Label();
            this.txtClienteFone = new System.Windows.Forms.TextBox();
            this.chkFonePriori = new System.Windows.Forms.CheckBox();
            this.cmbFoneType = new System.Windows.Forms.ComboBox();
            this.mskTelefone = new System.Windows.Forms.MaskedTextBox();
            this.mskFoneRamal = new System.Windows.Forms.MaskedTextBox();
            this.mskDDD = new System.Windows.Forms.MaskedTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tolFerramentasFornecedor = new System.Windows.Forms.ToolStrip();
            this.inserirFone = new System.Windows.Forms.ToolStripButton();
            this.pesquisarFone = new System.Windows.Forms.ToolStripButton();
            this.salvarFone = new System.Windows.Forms.ToolStripButton();
            this.cancelarFone = new System.Windows.Forms.ToolStripButton();
            this.relatorioFone = new System.Windows.Forms.ToolStripSplitButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvPesquisaFone = new System.Windows.Forms.DataGridView();
            this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Razao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uf = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cnpj = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Padro = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ramal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IEm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TipoCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idFoneType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.tolFerramentasFornecedor.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPesquisaFone)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnPesquisaCliente);
            this.groupBox1.Controls.Add(this.txtFoneComplemento);
            this.groupBox1.Controls.Add(this.txtIdCliente);
            this.groupBox1.Controls.Add(this.label36);
            this.groupBox1.Controls.Add(this.txtClienteFone);
            this.groupBox1.Controls.Add(this.chkFonePriori);
            this.groupBox1.Controls.Add(this.cmbFoneType);
            this.groupBox1.Controls.Add(this.mskTelefone);
            this.groupBox1.Controls.Add(this.mskFoneRamal);
            this.groupBox1.Controls.Add(this.mskDDD);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 43);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(502, 213);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnPesquisaCliente
            // 
            this.btnPesquisaCliente.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPesquisaCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPesquisaCliente.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnPesquisaCliente.Image = global::conectorPDV001.Properties.Resources.Procurar24X24;
            this.btnPesquisaCliente.Location = new System.Drawing.Point(132, 19);
            this.btnPesquisaCliente.Name = "btnPesquisaCliente";
            this.btnPesquisaCliente.Size = new System.Drawing.Size(34, 30);
            this.btnPesquisaCliente.TabIndex = 92;
            this.btnPesquisaCliente.UseVisualStyleBackColor = true;
            this.btnPesquisaCliente.Click += new System.EventHandler(this.btnPesquisaCliente_Click);
            // 
            // txtFoneComplemento
            // 
            this.txtFoneComplemento.Location = new System.Drawing.Point(116, 177);
            this.txtFoneComplemento.Name = "txtFoneComplemento";
            this.txtFoneComplemento.Size = new System.Drawing.Size(357, 20);
            this.txtFoneComplemento.TabIndex = 94;
            this.txtFoneComplemento.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFoneComplemento.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.txtFoneComplemento_MaskInputRejected);
            this.txtFoneComplemento.TextChanged += new System.EventHandler(this.txtFoneComplemento_TextChanged);
            // 
            // txtIdCliente
            // 
            this.txtIdCliente.Location = new System.Drawing.Point(64, 24);
            this.txtIdCliente.Name = "txtIdCliente";
            this.txtIdCliente.ReadOnly = true;
            this.txtIdCliente.Size = new System.Drawing.Size(70, 20);
            this.txtIdCliente.TabIndex = 93;
            this.txtIdCliente.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(20, 27);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(42, 13);
            this.label36.TabIndex = 91;
            this.label36.Text = "Pessoa";
            // 
            // txtClienteFone
            // 
            this.txtClienteFone.Location = new System.Drawing.Point(165, 26);
            this.txtClienteFone.Name = "txtClienteFone";
            this.txtClienteFone.ReadOnly = true;
            this.txtClienteFone.Size = new System.Drawing.Size(307, 20);
            this.txtClienteFone.TabIndex = 90;
            this.txtClienteFone.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // chkFonePriori
            // 
            this.chkFonePriori.AutoSize = true;
            this.chkFonePriori.Location = new System.Drawing.Point(118, 158);
            this.chkFonePriori.Name = "chkFonePriori";
            this.chkFonePriori.Size = new System.Drawing.Size(60, 17);
            this.chkFonePriori.TabIndex = 21;
            this.chkFonePriori.Text = "Padrão";
            this.chkFonePriori.UseVisualStyleBackColor = true;
            this.chkFonePriori.CheckedChanged += new System.EventHandler(this.chkFonePriori_CheckedChanged);
            // 
            // cmbFoneType
            // 
            this.cmbFoneType.BackColor = System.Drawing.SystemColors.Control;
            this.cmbFoneType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFoneType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbFoneType.FormattingEnabled = true;
            this.cmbFoneType.Location = new System.Drawing.Point(117, 131);
            this.cmbFoneType.Name = "cmbFoneType";
            this.cmbFoneType.Size = new System.Drawing.Size(357, 21);
            this.cmbFoneType.TabIndex = 20;
            this.cmbFoneType.SelectedIndexChanged += new System.EventHandler(this.cmbFoneType_SelectedIndexChanged);
            this.cmbFoneType.TextChanged += new System.EventHandler(this.cmbFoneType_TextChanged);
            this.cmbFoneType.Click += new System.EventHandler(this.cmbFoneType_Click);
            // 
            // mskTelefone
            // 
            this.mskTelefone.Location = new System.Drawing.Point(117, 81);
            this.mskTelefone.Mask = "0000-0000";
            this.mskTelefone.Name = "mskTelefone";
            this.mskTelefone.Size = new System.Drawing.Size(100, 20);
            this.mskTelefone.TabIndex = 19;
            this.mskTelefone.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mskTelefone.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.mskTelefone_MaskInputRejected);
            this.mskTelefone.TextChanged += new System.EventHandler(this.mskTelefone_TextChanged);
            // 
            // mskFoneRamal
            // 
            this.mskFoneRamal.Location = new System.Drawing.Point(117, 106);
            this.mskFoneRamal.Name = "mskFoneRamal";
            this.mskFoneRamal.Size = new System.Drawing.Size(79, 20);
            this.mskFoneRamal.TabIndex = 18;
            this.mskFoneRamal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mskFoneRamal.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.mskFoneRamal_MaskInputRejected);
            this.mskFoneRamal.TextChanged += new System.EventHandler(this.mskFoneRamal_TextChanged);
            // 
            // mskDDD
            // 
            this.mskDDD.Location = new System.Drawing.Point(118, 56);
            this.mskDDD.Mask = "(99-99)";
            this.mskDDD.Name = "mskDDD";
            this.mskDDD.Size = new System.Drawing.Size(47, 20);
            this.mskDDD.TabIndex = 17;
            this.mskDDD.Text = "00";
            this.mskDDD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mskDDD.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.mskDDD_MaskInputRejected);
            this.mskDDD.TextChanged += new System.EventHandler(this.mskDDD_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(42, 180);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Complemento";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(86, 135);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Tipo";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(77, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Ramal";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(65, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Telefone";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(84, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "DDD";
            // 
            // tolFerramentasFornecedor
            // 
            this.tolFerramentasFornecedor.ImageScalingSize = new System.Drawing.Size(33, 33);
            this.tolFerramentasFornecedor.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tolFerramentasFornecedor.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.inserirFone,
            this.pesquisarFone,
            this.salvarFone,
            this.cancelarFone,
            this.relatorioFone});
            this.tolFerramentasFornecedor.Location = new System.Drawing.Point(0, 0);
            this.tolFerramentasFornecedor.Name = "tolFerramentasFornecedor";
            this.tolFerramentasFornecedor.Size = new System.Drawing.Size(523, 40);
            this.tolFerramentasFornecedor.TabIndex = 4;
            // 
            // inserirFone
            // 
            this.inserirFone.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.inserirFone.Image = ((System.Drawing.Image)(resources.GetObject("inserirFone.Image")));
            this.inserirFone.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.inserirFone.Name = "inserirFone";
            this.inserirFone.Size = new System.Drawing.Size(37, 37);
            this.inserirFone.Text = "Inserir Telefone";
            this.inserirFone.Click += new System.EventHandler(this.inserirFone_Click);
            // 
            // pesquisarFone
            // 
            this.pesquisarFone.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pesquisarFone.Image = ((System.Drawing.Image)(resources.GetObject("pesquisarFone.Image")));
            this.pesquisarFone.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pesquisarFone.Name = "pesquisarFone";
            this.pesquisarFone.Size = new System.Drawing.Size(37, 37);
            this.pesquisarFone.Text = "Procurar Telefone";
            this.pesquisarFone.Click += new System.EventHandler(this.pesquisarFone_Click);
            // 
            // salvarFone
            // 
            this.salvarFone.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.salvarFone.Image = ((System.Drawing.Image)(resources.GetObject("salvarFone.Image")));
            this.salvarFone.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.salvarFone.Name = "salvarFone";
            this.salvarFone.Size = new System.Drawing.Size(37, 37);
            this.salvarFone.Text = "Salvar Telefone";
            this.salvarFone.Click += new System.EventHandler(this.salvarFone_Click);
            // 
            // cancelarFone
            // 
            this.cancelarFone.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cancelarFone.Image = ((System.Drawing.Image)(resources.GetObject("cancelarFone.Image")));
            this.cancelarFone.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cancelarFone.Name = "cancelarFone";
            this.cancelarFone.Size = new System.Drawing.Size(37, 37);
            this.cancelarFone.Text = "Cancela alteração no cadastro de telefone";
            this.cancelarFone.Click += new System.EventHandler(this.cancelarFone_Click);
            // 
            // relatorioFone
            // 
            this.relatorioFone.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.relatorioFone.Image = ((System.Drawing.Image)(resources.GetObject("relatorioFone.Image")));
            this.relatorioFone.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.relatorioFone.Name = "relatorioFone";
            this.relatorioFone.Size = new System.Drawing.Size(49, 37);
            this.relatorioFone.Text = "Relatórios";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvPesquisaFone);
            this.groupBox2.Location = new System.Drawing.Point(12, 257);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(502, 141);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Lista";
            // 
            // dgvPesquisaFone
            // 
            this.dgvPesquisaFone.AllowUserToDeleteRows = false;
            this.dgvPesquisaFone.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvPesquisaFone.BackgroundColor = System.Drawing.SystemColors.Info;
            this.dgvPesquisaFone.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPesquisaFone.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.codigo,
            this.Razao,
            this.uf,
            this.cnpj,
            this.Padro,
            this.ramal,
            this.IE,
            this.IEm,
            this.TipoCliente,
            this.idFoneType});
            this.dgvPesquisaFone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPesquisaFone.GridColor = System.Drawing.Color.Black;
            this.dgvPesquisaFone.Location = new System.Drawing.Point(3, 16);
            this.dgvPesquisaFone.Name = "dgvPesquisaFone";
            this.dgvPesquisaFone.RowHeadersVisible = false;
            this.dgvPesquisaFone.Size = new System.Drawing.Size(496, 122);
            this.dgvPesquisaFone.TabIndex = 19;
            this.dgvPesquisaFone.Click += new System.EventHandler(this.dgvPesquisaFone_Click);
            this.dgvPesquisaFone.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgvPesquisaFone_KeyUp);
            this.dgvPesquisaFone.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPesquisaFone_CellContentClick);
            // 
            // codigo
            // 
            this.codigo.HeaderText = "Codigo";
            this.codigo.Name = "codigo";
            this.codigo.ReadOnly = true;
            // 
            // Razao
            // 
            this.Razao.HeaderText = "DDD";
            this.Razao.Name = "Razao";
            this.Razao.ReadOnly = true;
            // 
            // uf
            // 
            this.uf.HeaderText = "Telefone";
            this.uf.Name = "uf";
            this.uf.ReadOnly = true;
            // 
            // cnpj
            // 
            this.cnpj.HeaderText = "Tipo";
            this.cnpj.Name = "cnpj";
            this.cnpj.ReadOnly = true;
            // 
            // Padro
            // 
            this.Padro.HeaderText = "Padrão";
            this.Padro.Name = "Padro";
            // 
            // ramal
            // 
            this.ramal.HeaderText = "ramal";
            this.ramal.Name = "ramal";
            this.ramal.ReadOnly = true;
            // 
            // IE
            // 
            this.IE.HeaderText = "Complemento";
            this.IE.Name = "IE";
            this.IE.ReadOnly = true;
            // 
            // IEm
            // 
            this.IEm.HeaderText = "Codigo_Cliente";
            this.IEm.Name = "IEm";
            this.IEm.ReadOnly = true;
            // 
            // TipoCliente
            // 
            this.TipoCliente.HeaderText = "TipoCliente";
            this.TipoCliente.Name = "TipoCliente";
            this.TipoCliente.ReadOnly = true;
            // 
            // idFoneType
            // 
            this.idFoneType.HeaderText = "idFoneType";
            this.idFoneType.Name = "idFoneType";
            this.idFoneType.ReadOnly = true;
            // 
            // telefone
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 403);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.tolFerramentasFornecedor);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "telefone";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "telefone";
            this.Load += new System.EventHandler(this.telefone_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.telefone_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tolFerramentasFornecedor.ResumeLayout(false);
            this.tolFerramentasFornecedor.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPesquisaFone)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkFonePriori;
        private System.Windows.Forms.ComboBox cmbFoneType;
        private System.Windows.Forms.MaskedTextBox mskTelefone;
        private System.Windows.Forms.MaskedTextBox mskFoneRamal;
        private System.Windows.Forms.MaskedTextBox mskDDD;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.ToolStrip tolFerramentasFornecedor;
        internal System.Windows.Forms.ToolStripButton inserirFone;
        internal System.Windows.Forms.ToolStripButton pesquisarFone;
        internal System.Windows.Forms.ToolStripButton salvarFone;
        internal System.Windows.Forms.ToolStripButton cancelarFone;
        internal System.Windows.Forms.ToolStripSplitButton relatorioFone;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvPesquisaFone;
        internal System.Windows.Forms.TextBox txtIdCliente;
        private System.Windows.Forms.Button btnPesquisaCliente;
        private System.Windows.Forms.Label label36;
        internal System.Windows.Forms.TextBox txtClienteFone;
        private System.Windows.Forms.MaskedTextBox txtFoneComplemento;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Razao;
        private System.Windows.Forms.DataGridViewTextBoxColumn uf;
        private System.Windows.Forms.DataGridViewTextBoxColumn cnpj;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Padro;
        private System.Windows.Forms.DataGridViewTextBoxColumn ramal;
        private System.Windows.Forms.DataGridViewTextBoxColumn IE;
        private System.Windows.Forms.DataGridViewTextBoxColumn IEm;
        private System.Windows.Forms.DataGridViewTextBoxColumn TipoCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn idFoneType;

    }
}