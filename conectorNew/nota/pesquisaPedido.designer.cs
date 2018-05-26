namespace conectorPDV001
{
    partial class pesquisaPedido
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(pesquisaPedido));
            this.chkTypePesquisaPedido = new System.Windows.Forms.CheckBox();
            this.cmbTypePesquisaPedido = new System.Windows.Forms.ComboBox();
            this.chkLojaPesquisaPedido = new System.Windows.Forms.CheckBox();
            this.cmbLojaPesquisaPedido = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbPesquisaPedidoTotal = new System.Windows.Forms.RadioButton();
            this.rbCodigoPesquisaPedido = new System.Windows.Forms.RadioButton();
            this.dgvPesquisaPesquisaPedido = new System.Windows.Forms.DataGridView();
            this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.varStatus = new System.Windows.Forms.DataGridViewButtonColumn();
            this.codigoOrigem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descricao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codigoDestino = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.destino = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.login = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataemissao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.discount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Liquido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtPesquisaPedido = new System.Windows.Forms.TextBox();
            this.lbDescricao = new System.Windows.Forms.Label();
            this.chkOperacaoPesquisaPedido = new System.Windows.Forms.CheckBox();
            this.cmbOperacaoPesquisaPedido = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFinalPesquisaPedido = new System.Windows.Forms.DateTimePicker();
            this.dtpInicialPesquisaPedido = new System.Windows.Forms.DateTimePicker();
            this.btnPesquisaPedidoVenda = new System.Windows.Forms.Button();
            this.chkExpiradasPesquisaPedido = new System.Windows.Forms.CheckBox();
            this.txtDescClientePesquisaPedido = new System.Windows.Forms.TextBox();
            this.btnPesquisaClientePedidoVenda = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.reimpressaoPesquisaPedido = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPesquisaPesquisaPedido)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkTypePesquisaPedido
            // 
            this.chkTypePesquisaPedido.AutoSize = true;
            this.chkTypePesquisaPedido.Checked = true;
            this.chkTypePesquisaPedido.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTypePesquisaPedido.Location = new System.Drawing.Point(975, 90);
            this.chkTypePesquisaPedido.Name = "chkTypePesquisaPedido";
            this.chkTypePesquisaPedido.Size = new System.Drawing.Size(68, 17);
            this.chkTypePesquisaPedido.TabIndex = 64;
            this.chkTypePesquisaPedido.Text = "Situação";
            this.chkTypePesquisaPedido.UseVisualStyleBackColor = true;
            this.chkTypePesquisaPedido.CheckedChanged += new System.EventHandler(this.chkTypePesquisaPedido_CheckedChanged);
            // 
            // cmbTypePesquisaPedido
            // 
            this.cmbTypePesquisaPedido.BackColor = System.Drawing.SystemColors.Control;
            this.cmbTypePesquisaPedido.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTypePesquisaPedido.Enabled = false;
            this.cmbTypePesquisaPedido.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbTypePesquisaPedido.FormattingEnabled = true;
            this.cmbTypePesquisaPedido.Items.AddRange(new object[] {
            "0 - Aberto",
            "1 - Emitido",
            "2 - Faturado",
            "3 - Fiscal",
            "4 - Finalizado",
            "5 - Cancelado",
            "6 - Expirado",
            "7 - transito"});
            this.cmbTypePesquisaPedido.Location = new System.Drawing.Point(614, 87);
            this.cmbTypePesquisaPedido.Name = "cmbTypePesquisaPedido";
            this.cmbTypePesquisaPedido.Size = new System.Drawing.Size(355, 21);
            this.cmbTypePesquisaPedido.TabIndex = 63;
            this.cmbTypePesquisaPedido.SelectedIndexChanged += new System.EventHandler(this.cmbTypePesquisaPedido_SelectedIndexChanged);
            // 
            // chkLojaPesquisaPedido
            // 
            this.chkLojaPesquisaPedido.AutoSize = true;
            this.chkLojaPesquisaPedido.Location = new System.Drawing.Point(976, 63);
            this.chkLojaPesquisaPedido.Name = "chkLojaPesquisaPedido";
            this.chkLojaPesquisaPedido.Size = new System.Drawing.Size(46, 17);
            this.chkLojaPesquisaPedido.TabIndex = 62;
            this.chkLojaPesquisaPedido.Text = "Loja";
            this.chkLojaPesquisaPedido.UseVisualStyleBackColor = true;
            this.chkLojaPesquisaPedido.CheckedChanged += new System.EventHandler(this.chkLojaPesquisaPedido_CheckedChanged);
            // 
            // cmbLojaPesquisaPedido
            // 
            this.cmbLojaPesquisaPedido.BackColor = System.Drawing.SystemColors.Control;
            this.cmbLojaPesquisaPedido.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLojaPesquisaPedido.Enabled = false;
            this.cmbLojaPesquisaPedido.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbLojaPesquisaPedido.FormattingEnabled = true;
            this.cmbLojaPesquisaPedido.Location = new System.Drawing.Point(614, 60);
            this.cmbLojaPesquisaPedido.Name = "cmbLojaPesquisaPedido";
            this.cmbLojaPesquisaPedido.Size = new System.Drawing.Size(355, 21);
            this.cmbLojaPesquisaPedido.TabIndex = 61;
            this.cmbLojaPesquisaPedido.SelectedIndexChanged += new System.EventHandler(this.cmbLojaTerminal_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbPesquisaPedidoTotal);
            this.groupBox1.Controls.Add(this.rbCodigoPesquisaPedido);
            this.groupBox1.Location = new System.Drawing.Point(8, 56);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(211, 78);
            this.groupBox1.TabIndex = 60;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tipo Pesquisa";
            // 
            // rbPesquisaPedidoTotal
            // 
            this.rbPesquisaPedidoTotal.AutoSize = true;
            this.rbPesquisaPedidoTotal.Checked = true;
            this.rbPesquisaPedidoTotal.Location = new System.Drawing.Point(106, 35);
            this.rbPesquisaPedidoTotal.Name = "rbPesquisaPedidoTotal";
            this.rbPesquisaPedidoTotal.Size = new System.Drawing.Size(55, 17);
            this.rbPesquisaPedidoTotal.TabIndex = 2;
            this.rbPesquisaPedidoTotal.TabStop = true;
            this.rbPesquisaPedidoTotal.Text = "Todas";
            this.rbPesquisaPedidoTotal.UseVisualStyleBackColor = true;
            this.rbPesquisaPedidoTotal.CheckedChanged += new System.EventHandler(this.rbPesquisaPedidoTotal_CheckedChanged);
            // 
            // rbCodigoPesquisaPedido
            // 
            this.rbCodigoPesquisaPedido.AutoSize = true;
            this.rbCodigoPesquisaPedido.Location = new System.Drawing.Point(37, 35);
            this.rbCodigoPesquisaPedido.Name = "rbCodigoPesquisaPedido";
            this.rbCodigoPesquisaPedido.Size = new System.Drawing.Size(58, 17);
            this.rbCodigoPesquisaPedido.TabIndex = 0;
            this.rbCodigoPesquisaPedido.Text = "Codigo";
            this.rbCodigoPesquisaPedido.UseVisualStyleBackColor = true;
            this.rbCodigoPesquisaPedido.CheckedChanged += new System.EventHandler(this.rbCodigoPesquisaPedido_CheckedChanged);
            // 
            // dgvPesquisaPesquisaPedido
            // 
            this.dgvPesquisaPesquisaPedido.AllowUserToDeleteRows = false;
            this.dgvPesquisaPesquisaPedido.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvPesquisaPesquisaPedido.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvPesquisaPesquisaPedido.BackgroundColor = System.Drawing.SystemColors.Info;
            this.dgvPesquisaPesquisaPedido.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvPesquisaPesquisaPedido.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.codigo,
            this.varStatus,
            this.codigoOrigem,
            this.descricao,
            this.codigoDestino,
            this.destino,
            this.login,
            this.dataemissao,
            this.total,
            this.discount,
            this.Liquido});
            this.dgvPesquisaPesquisaPedido.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgvPesquisaPesquisaPedido.Location = new System.Drawing.Point(0, 148);
            this.dgvPesquisaPesquisaPedido.Name = "dgvPesquisaPesquisaPedido";
            this.dgvPesquisaPesquisaPedido.RowHeadersVisible = false;
            this.dgvPesquisaPesquisaPedido.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPesquisaPesquisaPedido.Size = new System.Drawing.Size(1086, 403);
            this.dgvPesquisaPesquisaPedido.TabIndex = 59;
            this.dgvPesquisaPesquisaPedido.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPesquisaPesquisaPedido_CellClick);
            this.dgvPesquisaPesquisaPedido.DoubleClick += new System.EventHandler(this.dgvPesquisaPesquisaPedido_DoubleClick);
            this.dgvPesquisaPesquisaPedido.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dgvPesquisaPesquisaPedido_KeyPress);
            this.dgvPesquisaPesquisaPedido.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgvPesquisaPesquisaPedido_KeyUp);
            // 
            // codigo
            // 
            this.codigo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.codigo.Frozen = true;
            this.codigo.HeaderText = "Reversa";
            this.codigo.Name = "codigo";
            this.codigo.ReadOnly = true;
            // 
            // varStatus
            // 
            this.varStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.varStatus.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.varStatus.Frozen = true;
            this.varStatus.HeaderText = "STATUS";
            this.varStatus.Name = "varStatus";
            this.varStatus.ReadOnly = true;
            this.varStatus.Width = 200;
            // 
            // codigoOrigem
            // 
            this.codigoOrigem.HeaderText = "Cod. Origem";
            this.codigoOrigem.Name = "codigoOrigem";
            this.codigoOrigem.ReadOnly = true;
            this.codigoOrigem.Width = 90;
            // 
            // descricao
            // 
            this.descricao.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.descricao.HeaderText = "Origem";
            this.descricao.Name = "descricao";
            this.descricao.ReadOnly = true;
            this.descricao.Width = 190;
            // 
            // codigoDestino
            // 
            this.codigoDestino.HeaderText = "Cod. Destino";
            this.codigoDestino.Name = "codigoDestino";
            this.codigoDestino.ReadOnly = true;
            this.codigoDestino.Width = 93;
            // 
            // destino
            // 
            this.destino.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.destino.HeaderText = "Destino";
            this.destino.Name = "destino";
            this.destino.ReadOnly = true;
            this.destino.Width = 190;
            // 
            // login
            // 
            this.login.HeaderText = "N.o Items";
            this.login.Name = "login";
            this.login.ReadOnly = true;
            this.login.Width = 77;
            // 
            // dataemissao
            // 
            this.dataemissao.HeaderText = "Emissão";
            this.dataemissao.Name = "dataemissao";
            this.dataemissao.ReadOnly = true;
            this.dataemissao.Width = 71;
            // 
            // total
            // 
            this.total.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.total.HeaderText = "Total";
            this.total.Name = "total";
            this.total.ReadOnly = true;
            this.total.Width = 97;
            // 
            // discount
            // 
            this.discount.HeaderText = "Desconto";
            this.discount.Name = "discount";
            this.discount.ReadOnly = true;
            this.discount.Width = 78;
            // 
            // Liquido
            // 
            this.Liquido.HeaderText = "Preço Liquido";
            this.Liquido.Name = "Liquido";
            this.Liquido.ReadOnly = true;
            this.Liquido.Width = 97;
            // 
            // txtPesquisaPedido
            // 
            this.txtPesquisaPedido.Location = new System.Drawing.Point(73, 22);
            this.txtPesquisaPedido.Name = "txtPesquisaPedido";
            this.txtPesquisaPedido.Size = new System.Drawing.Size(146, 20);
            this.txtPesquisaPedido.TabIndex = 58;
            this.txtPesquisaPedido.Text = "0";
            this.txtPesquisaPedido.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPesquisaPedido.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPesquisaTerminal_KeyPress);
            this.txtPesquisaPedido.Validated += new System.EventHandler(this.txtPesquisaTerminal_Validated);
            // 
            // lbDescricao
            // 
            this.lbDescricao.AutoSize = true;
            this.lbDescricao.Location = new System.Drawing.Point(11, 25);
            this.lbDescricao.Name = "lbDescricao";
            this.lbDescricao.Size = new System.Drawing.Size(60, 13);
            this.lbDescricao.TabIndex = 57;
            this.lbDescricao.Text = "N.o Pedido";
            // 
            // chkOperacaoPesquisaPedido
            // 
            this.chkOperacaoPesquisaPedido.AutoSize = true;
            this.chkOperacaoPesquisaPedido.Checked = true;
            this.chkOperacaoPesquisaPedido.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOperacaoPesquisaPedido.Location = new System.Drawing.Point(975, 117);
            this.chkOperacaoPesquisaPedido.Name = "chkOperacaoPesquisaPedido";
            this.chkOperacaoPesquisaPedido.Size = new System.Drawing.Size(73, 17);
            this.chkOperacaoPesquisaPedido.TabIndex = 66;
            this.chkOperacaoPesquisaPedido.Text = "Operação";
            this.chkOperacaoPesquisaPedido.UseVisualStyleBackColor = true;
            this.chkOperacaoPesquisaPedido.CheckedChanged += new System.EventHandler(this.chkOperacaoPesquisaPedido_CheckedChanged);
            // 
            // cmbOperacaoPesquisaPedido
            // 
            this.cmbOperacaoPesquisaPedido.BackColor = System.Drawing.SystemColors.Control;
            this.cmbOperacaoPesquisaPedido.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOperacaoPesquisaPedido.Enabled = false;
            this.cmbOperacaoPesquisaPedido.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbOperacaoPesquisaPedido.FormattingEnabled = true;
            this.cmbOperacaoPesquisaPedido.Location = new System.Drawing.Point(614, 114);
            this.cmbOperacaoPesquisaPedido.Name = "cmbOperacaoPesquisaPedido";
            this.cmbOperacaoPesquisaPedido.Size = new System.Drawing.Size(355, 21);
            this.cmbOperacaoPesquisaPedido.TabIndex = 65;
            this.cmbOperacaoPesquisaPedido.SelectedIndexChanged += new System.EventHandler(this.cmbOperacaoPesquisaPedido_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.dtpFinalPesquisaPedido);
            this.groupBox2.Controls.Add(this.dtpInicialPesquisaPedido);
            this.groupBox2.Location = new System.Drawing.Point(250, 56);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(320, 79);
            this.groupBox2.TabIndex = 61;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Período";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(77, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Data Final";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(72, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Data Inicial";
            // 
            // dtpFinalPesquisaPedido
            // 
            this.dtpFinalPesquisaPedido.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFinalPesquisaPedido.Location = new System.Drawing.Point(135, 47);
            this.dtpFinalPesquisaPedido.Name = "dtpFinalPesquisaPedido";
            this.dtpFinalPesquisaPedido.Size = new System.Drawing.Size(95, 20);
            this.dtpFinalPesquisaPedido.TabIndex = 1;
            this.dtpFinalPesquisaPedido.ValueChanged += new System.EventHandler(this.dtpFinalPesquisaPedido_ValueChanged);
            // 
            // dtpInicialPesquisaPedido
            // 
            this.dtpInicialPesquisaPedido.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpInicialPesquisaPedido.Location = new System.Drawing.Point(135, 21);
            this.dtpInicialPesquisaPedido.Name = "dtpInicialPesquisaPedido";
            this.dtpInicialPesquisaPedido.Size = new System.Drawing.Size(95, 20);
            this.dtpInicialPesquisaPedido.TabIndex = 0;
            this.dtpInicialPesquisaPedido.ValueChanged += new System.EventHandler(this.dtpInicialPesquisaPedido_ValueChanged);
            // 
            // btnPesquisaPedidoVenda
            // 
            this.btnPesquisaPedidoVenda.BackColor = System.Drawing.SystemColors.Control;
            this.btnPesquisaPedidoVenda.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPesquisaPedidoVenda.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPesquisaPedidoVenda.Image = ((System.Drawing.Image)(resources.GetObject("btnPesquisaPedidoVenda.Image")));
            this.btnPesquisaPedidoVenda.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPesquisaPedidoVenda.Location = new System.Drawing.Point(970, 21);
            this.btnPesquisaPedidoVenda.Name = "btnPesquisaPedidoVenda";
            this.btnPesquisaPedidoVenda.Size = new System.Drawing.Size(115, 26);
            this.btnPesquisaPedidoVenda.TabIndex = 95;
            this.btnPesquisaPedidoVenda.Text = "Pesquisar";
            this.btnPesquisaPedidoVenda.UseVisualStyleBackColor = false;
            this.btnPesquisaPedidoVenda.Click += new System.EventHandler(this.btnPesquisaCliente_Click);
            // 
            // chkExpiradasPesquisaPedido
            // 
            this.chkExpiradasPesquisaPedido.AutoSize = true;
            this.chkExpiradasPesquisaPedido.Location = new System.Drawing.Point(223, 24);
            this.chkExpiradasPesquisaPedido.Name = "chkExpiradasPesquisaPedido";
            this.chkExpiradasPesquisaPedido.Size = new System.Drawing.Size(163, 17);
            this.chkExpiradasPesquisaPedido.TabIndex = 96;
            this.chkExpiradasPesquisaPedido.Text = "Pesquisa reservas expiradas.";
            this.chkExpiradasPesquisaPedido.UseVisualStyleBackColor = true;
            this.chkExpiradasPesquisaPedido.CheckedChanged += new System.EventHandler(this.chkExpiradasPesquisaPedido_CheckedChanged);
            // 
            // txtDescClientePesquisaPedido
            // 
            this.txtDescClientePesquisaPedido.Location = new System.Drawing.Point(647, 22);
            this.txtDescClientePesquisaPedido.Name = "txtDescClientePesquisaPedido";
            this.txtDescClientePesquisaPedido.ReadOnly = true;
            this.txtDescClientePesquisaPedido.Size = new System.Drawing.Size(322, 20);
            this.txtDescClientePesquisaPedido.TabIndex = 99;
            // 
            // btnPesquisaClientePedidoVenda
            // 
            this.btnPesquisaClientePedidoVenda.BackColor = System.Drawing.SystemColors.Control;
            this.btnPesquisaClientePedidoVenda.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPesquisaClientePedidoVenda.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPesquisaClientePedidoVenda.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnPesquisaClientePedidoVenda.Image = global::conector.Properties.Resources.Procurar24X24;
            this.btnPesquisaClientePedidoVenda.Location = new System.Drawing.Point(614, 17);
            this.btnPesquisaClientePedidoVenda.Name = "btnPesquisaClientePedidoVenda";
            this.btnPesquisaClientePedidoVenda.Size = new System.Drawing.Size(34, 30);
            this.btnPesquisaClientePedidoVenda.TabIndex = 98;
            this.btnPesquisaClientePedidoVenda.UseVisualStyleBackColor = false;
            this.btnPesquisaClientePedidoVenda.Click += new System.EventHandler(this.btnPesquisaClientePedidoVenda_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(574, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 97;
            this.label3.Text = "&Cliente";
            // 
            // reimpressaoPesquisaPedido
            // 
            this.reimpressaoPesquisaPedido.BackColor = System.Drawing.SystemColors.Control;
            this.reimpressaoPesquisaPedido.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.reimpressaoPesquisaPedido.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reimpressaoPesquisaPedido.ForeColor = System.Drawing.Color.Red;
            this.reimpressaoPesquisaPedido.Image = global::conector.Properties.Resources.PDFFile;
            this.reimpressaoPesquisaPedido.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.reimpressaoPesquisaPedido.Location = new System.Drawing.Point(392, 17);
            this.reimpressaoPesquisaPedido.Name = "reimpressaoPesquisaPedido";
            this.reimpressaoPesquisaPedido.Size = new System.Drawing.Size(178, 43);
            this.reimpressaoPesquisaPedido.TabIndex = 100;
            this.reimpressaoPesquisaPedido.Text = "Reimpressão";
            this.reimpressaoPesquisaPedido.UseVisualStyleBackColor = false;
            this.reimpressaoPesquisaPedido.Click += new System.EventHandler(this.reimpressaoPesquisaPedido_Click);
            // 
            // pesquisaPedido
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1086, 551);
            this.Controls.Add(this.reimpressaoPesquisaPedido);
            this.Controls.Add(this.btnPesquisaClientePedidoVenda);
            this.Controls.Add(this.btnPesquisaPedidoVenda);
            this.Controls.Add(this.txtDescClientePesquisaPedido);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chkExpiradasPesquisaPedido);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.chkOperacaoPesquisaPedido);
            this.Controls.Add(this.cmbOperacaoPesquisaPedido);
            this.Controls.Add(this.chkTypePesquisaPedido);
            this.Controls.Add(this.cmbTypePesquisaPedido);
            this.Controls.Add(this.chkLojaPesquisaPedido);
            this.Controls.Add(this.cmbLojaPesquisaPedido);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvPesquisaPesquisaPedido);
            this.Controls.Add(this.txtPesquisaPedido);
            this.Controls.Add(this.lbDescricao);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "pesquisaPedido";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "pesquisaPedido";
            this.Load += new System.EventHandler(this.pesquisaPedido_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.pesquisaPedido_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPesquisaPesquisaPedido)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkTypePesquisaPedido;
        internal System.Windows.Forms.ComboBox cmbTypePesquisaPedido;
        private System.Windows.Forms.CheckBox chkLojaPesquisaPedido;
        internal System.Windows.Forms.ComboBox cmbLojaPesquisaPedido;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbPesquisaPedidoTotal;
        private System.Windows.Forms.RadioButton rbCodigoPesquisaPedido;
        private System.Windows.Forms.DataGridView dgvPesquisaPesquisaPedido;
        private System.Windows.Forms.TextBox txtPesquisaPedido;
        private System.Windows.Forms.Label lbDescricao;
        private System.Windows.Forms.CheckBox chkOperacaoPesquisaPedido;
        internal System.Windows.Forms.ComboBox cmbOperacaoPesquisaPedido;
        private System.Windows.Forms.GroupBox groupBox2;
        internal System.Windows.Forms.Button btnPesquisaPedidoVenda;
        private System.Windows.Forms.DateTimePicker dtpFinalPesquisaPedido;
        private System.Windows.Forms.DateTimePicker dtpInicialPesquisaPedido;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkExpiradasPesquisaPedido;
        private System.Windows.Forms.TextBox txtDescClientePesquisaPedido;
        private System.Windows.Forms.Button btnPesquisaClientePedidoVenda;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigo;
        private System.Windows.Forms.DataGridViewButtonColumn varStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigoOrigem;
        private System.Windows.Forms.DataGridViewTextBoxColumn descricao;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigoDestino;
        private System.Windows.Forms.DataGridViewTextBoxColumn destino;
        private System.Windows.Forms.DataGridViewTextBoxColumn login;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataemissao;
        private System.Windows.Forms.DataGridViewTextBoxColumn total;
        private System.Windows.Forms.DataGridViewTextBoxColumn discount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Liquido;
        internal System.Windows.Forms.Button reimpressaoPesquisaPedido;
    }
}