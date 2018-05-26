namespace conectorPDV001
{
    partial class editorPrestacao
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvParcelamentoEditorParcela = new System.Windows.Forms.DataGridView();
            this.Column6 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.VarContrato = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VarEmissao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VarParcelas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VarValorAberto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vencimento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VarAtraso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VarLoja = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button4 = new System.Windows.Forms.Button();
            this.lbNomeClienteEditorParcela = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.lbNumeroClienteEditorCrediario = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblStatusConexaoEditor = new System.Windows.Forms.Label();
            this.lklAtualizaWebServiceEditor = new System.Windows.Forms.LinkLabel();
            this.lblCountContratosEditor = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvParcelamentoEditorParcela)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvParcelamentoEditorParcela
            // 
            this.dgvParcelamentoEditorParcela.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvParcelamentoEditorParcela.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvParcelamentoEditorParcela.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dgvParcelamentoEditorParcela.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvParcelamentoEditorParcela.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvParcelamentoEditorParcela.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column6,
            this.VarContrato,
            this.VarEmissao,
            this.VarParcelas,
            this.VarValorAberto,
            this.vencimento,
            this.VarAtraso,
            this.VarLoja});
            this.dgvParcelamentoEditorParcela.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvParcelamentoEditorParcela.GridColor = System.Drawing.Color.Lime;
            this.dgvParcelamentoEditorParcela.Location = new System.Drawing.Point(0, 98);
            this.dgvParcelamentoEditorParcela.Name = "dgvParcelamentoEditorParcela";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Lime;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvParcelamentoEditorParcela.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvParcelamentoEditorParcela.RowHeadersVisible = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Lime;
            this.dgvParcelamentoEditorParcela.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvParcelamentoEditorParcela.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvParcelamentoEditorParcela.Size = new System.Drawing.Size(708, 473);
            this.dgvParcelamentoEditorParcela.TabIndex = 301;
            this.dgvParcelamentoEditorParcela.Click += new System.EventHandler(this.dgvParcelamentoEditorParcela_Click);
            this.dgvParcelamentoEditorParcela.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvParcelamentoEditorParcela_KeyDown);
            this.dgvParcelamentoEditorParcela.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dgvParcelamentoEditorParcela_KeyPress);
            this.dgvParcelamentoEditorParcela.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvParcelamentoEditorParcela_MouseClick);
            this.dgvParcelamentoEditorParcela.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvParcelamentoEditorParcela_MouseDoubleClick);
            // 
            // Column6
            // 
            this.Column6.HeaderText = "OPTION";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column6.Width = 54;
            // 
            // VarContrato
            // 
            this.VarContrato.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.VarContrato.HeaderText = "CONTRATO";
            this.VarContrato.Name = "VarContrato";
            this.VarContrato.ReadOnly = true;
            this.VarContrato.Width = 192;
            // 
            // VarEmissao
            // 
            this.VarEmissao.HeaderText = "EMISSAO";
            this.VarEmissao.Name = "VarEmissao";
            this.VarEmissao.ReadOnly = true;
            this.VarEmissao.Width = 80;
            // 
            // VarParcelas
            // 
            this.VarParcelas.HeaderText = "PARCELA";
            this.VarParcelas.Name = "VarParcelas";
            this.VarParcelas.ReadOnly = true;
            this.VarParcelas.Width = 81;
            // 
            // VarValorAberto
            // 
            this.VarValorAberto.HeaderText = "PRESTAÇÃO";
            this.VarValorAberto.Name = "VarValorAberto";
            this.VarValorAberto.ReadOnly = true;
            this.VarValorAberto.Width = 97;
            // 
            // vencimento
            // 
            this.vencimento.HeaderText = "VENCIMENTO";
            this.vencimento.Name = "vencimento";
            this.vencimento.ReadOnly = true;
            this.vencimento.Width = 103;
            // 
            // VarAtraso
            // 
            this.VarAtraso.HeaderText = "ATRASO";
            this.VarAtraso.Name = "VarAtraso";
            this.VarAtraso.ReadOnly = true;
            this.VarAtraso.Width = 76;
            // 
            // VarLoja
            // 
            this.VarLoja.HeaderText = "LOJA";
            this.VarLoja.Name = "VarLoja";
            this.VarLoja.ReadOnly = true;
            this.VarLoja.Width = 58;
            // 
            // button4
            // 
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.Color.Lime;
            this.button4.Image = global::conectorPDV001.Properties.Resources.Boss3;
            this.button4.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button4.Location = new System.Drawing.Point(6, 10);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(48, 37);
            this.button4.TabIndex = 306;
            this.button4.UseVisualStyleBackColor = true;
            // 
            // lbNomeClienteEditorParcela
            // 
            this.lbNomeClienteEditorParcela.AutoSize = true;
            this.lbNomeClienteEditorParcela.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNomeClienteEditorParcela.ForeColor = System.Drawing.Color.Yellow;
            this.lbNomeClienteEditorParcela.Location = new System.Drawing.Point(249, 23);
            this.lbNomeClienteEditorParcela.Name = "lbNomeClienteEditorParcela";
            this.lbNomeClienteEditorParcela.Size = new System.Drawing.Size(43, 13);
            this.lbNomeClienteEditorParcela.TabIndex = 305;
            this.lbNomeClienteEditorParcela.Text = "NOME";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.Lime;
            this.label20.Location = new System.Drawing.Point(218, 22);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(11, 13);
            this.label20.TabIndex = 304;
            this.label20.Text = "-";
            // 
            // lbNumeroClienteEditorCrediario
            // 
            this.lbNumeroClienteEditorCrediario.AutoSize = true;
            this.lbNumeroClienteEditorCrediario.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNumeroClienteEditorCrediario.ForeColor = System.Drawing.Color.Yellow;
            this.lbNumeroClienteEditorCrediario.Location = new System.Drawing.Point(130, 22);
            this.lbNumeroClienteEditorCrediario.Name = "lbNumeroClienteEditorCrediario";
            this.lbNumeroClienteEditorCrediario.Size = new System.Drawing.Size(59, 13);
            this.lbNumeroClienteEditorCrediario.TabIndex = 303;
            this.lbNumeroClienteEditorCrediario.Text = "CLIENTE";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.Lime;
            this.label17.Location = new System.Drawing.Point(61, 22);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(67, 13);
            this.label17.TabIndex = 302;
            this.label17.Text = "CLIENTE :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Lime;
            this.label1.Location = new System.Drawing.Point(61, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 13);
            this.label1.TabIndex = 307;
            this.label1.Text = "TIPO DE CONSULTA:";
            // 
            // lblStatusConexaoEditor
            // 
            this.lblStatusConexaoEditor.AutoSize = true;
            this.lblStatusConexaoEditor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusConexaoEditor.ForeColor = System.Drawing.Color.Red;
            this.lblStatusConexaoEditor.Location = new System.Drawing.Point(198, 46);
            this.lblStatusConexaoEditor.Name = "lblStatusConexaoEditor";
            this.lblStatusConexaoEditor.Size = new System.Drawing.Size(46, 13);
            this.lblStatusConexaoEditor.TabIndex = 308;
            this.lblStatusConexaoEditor.Text = "LOCAL";
            // 
            // lklAtualizaWebServiceEditor
            // 
            this.lklAtualizaWebServiceEditor.AutoSize = true;
            this.lklAtualizaWebServiceEditor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lklAtualizaWebServiceEditor.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lklAtualizaWebServiceEditor.Location = new System.Drawing.Point(478, 46);
            this.lklAtualizaWebServiceEditor.Name = "lklAtualizaWebServiceEditor";
            this.lklAtualizaWebServiceEditor.Size = new System.Drawing.Size(173, 13);
            this.lklAtualizaWebServiceEditor.TabIndex = 309;
            this.lklAtualizaWebServiceEditor.TabStop = true;
            this.lklAtualizaWebServiceEditor.Text = "[ F10 - ATUALIZAÇÃO WEB ]";
            this.lklAtualizaWebServiceEditor.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lklAtualizaWebServiceEditor_LinkClicked);
            // 
            // lblCountContratosEditor
            // 
            this.lblCountContratosEditor.AutoSize = true;
            this.lblCountContratosEditor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCountContratosEditor.ForeColor = System.Drawing.Color.Red;
            this.lblCountContratosEditor.Location = new System.Drawing.Point(197, 68);
            this.lblCountContratosEditor.Name = "lblCountContratosEditor";
            this.lblCountContratosEditor.Size = new System.Drawing.Size(14, 13);
            this.lblCountContratosEditor.TabIndex = 311;
            this.lblCountContratosEditor.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Lime;
            this.label3.Location = new System.Drawing.Point(40, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(152, 13);
            this.label3.TabIndex = 310;
            this.label3.Text = "TOTAL DE CONTRATOS:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Lime;
            this.label4.Location = new System.Drawing.Point(478, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(135, 13);
            this.label4.TabIndex = 312;
            this.label4.Text = "SITUAÇÃO: ABERTOS";
            // 
            // editorPrestacao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(708, 571);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblCountContratosEditor);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lklAtualizaWebServiceEditor);
            this.Controls.Add(this.lblStatusConexaoEditor);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.lbNomeClienteEditorParcela);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.lbNumeroClienteEditorCrediario);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.dgvParcelamentoEditorParcela);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "editorPrestacao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "editorPrestacao";
            this.Load += new System.EventHandler(this.editorPrestacao_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.editorPrestacao_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvParcelamentoEditorParcela)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvParcelamentoEditorParcela;
        internal System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label lbNomeClienteEditorParcela;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label lbNumeroClienteEditorCrediario;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.DataGridViewButtonColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn VarContrato;
        private System.Windows.Forms.DataGridViewTextBoxColumn VarEmissao;
        private System.Windows.Forms.DataGridViewTextBoxColumn VarParcelas;
        private System.Windows.Forms.DataGridViewTextBoxColumn VarValorAberto;
        private System.Windows.Forms.DataGridViewTextBoxColumn vencimento;
        private System.Windows.Forms.DataGridViewTextBoxColumn VarAtraso;
        private System.Windows.Forms.DataGridViewTextBoxColumn VarLoja;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblStatusConexaoEditor;
        private System.Windows.Forms.LinkLabel lklAtualizaWebServiceEditor;
        private System.Windows.Forms.Label lblCountContratosEditor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}