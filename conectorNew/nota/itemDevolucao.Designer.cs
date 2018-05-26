namespace conectorPDV001
{
    partial class itemDevolucao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(itemDevolucao));
            this.dgvItensNotaFiscalDevItem = new System.Windows.Forms.DataGridView();
            this.ItemNfe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descricao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NcmNfe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cstItemNotaFiscal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CFOPNfe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnidNfe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QtdeNfe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VUnitNfe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VTotalNfe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BCICMSNfe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VICMSNfe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VIPINfe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AliquotaNfe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AliqIpNfe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSairOuCancelarItemEntradaNota = new System.Windows.Forms.Button();
            this.btnOkItemEntradaNota = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItensNotaFiscalDevItem)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvItensNotaFiscalDevItem
            // 
            this.dgvItensNotaFiscalDevItem.AllowUserToOrderColumns = true;
            this.dgvItensNotaFiscalDevItem.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvItensNotaFiscalDevItem.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvItensNotaFiscalDevItem.BackgroundColor = System.Drawing.Color.LightYellow;
            this.dgvItensNotaFiscalDevItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItensNotaFiscalDevItem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ItemNfe,
            this.Codigo,
            this.Descricao,
            this.NcmNfe,
            this.cstItemNotaFiscal,
            this.CFOPNfe,
            this.UnidNfe,
            this.QtdeNfe,
            this.VUnitNfe,
            this.VTotalNfe,
            this.BCICMSNfe,
            this.VICMSNfe,
            this.VIPINfe,
            this.AliquotaNfe,
            this.AliqIpNfe});
            this.dgvItensNotaFiscalDevItem.Location = new System.Drawing.Point(-1, 0);
            this.dgvItensNotaFiscalDevItem.Name = "dgvItensNotaFiscalDevItem";
            this.dgvItensNotaFiscalDevItem.RowHeadersVisible = false;
            this.dgvItensNotaFiscalDevItem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItensNotaFiscalDevItem.Size = new System.Drawing.Size(1241, 376);
            this.dgvItensNotaFiscalDevItem.TabIndex = 17;
            this.dgvItensNotaFiscalDevItem.DoubleClick += new System.EventHandler(this.dgvItensNotaFiscalDevItem_DoubleClick);
            this.dgvItensNotaFiscalDevItem.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItensNotaFiscalDevItem_CellClick);
            this.dgvItensNotaFiscalDevItem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvItensNotaFiscalDevItem_KeyDown);
            this.dgvItensNotaFiscalDevItem.SelectionChanged += new System.EventHandler(this.dgvItensNotaFiscalDevItem_SelectionChanged);
            // 
            // ItemNfe
            // 
            this.ItemNfe.HeaderText = "Item";
            this.ItemNfe.Name = "ItemNfe";
            this.ItemNfe.ReadOnly = true;
            // 
            // Codigo
            // 
            this.Codigo.HeaderText = "Codigo";
            this.Codigo.Name = "Codigo";
            this.Codigo.ReadOnly = true;
            // 
            // Descricao
            // 
            this.Descricao.HeaderText = "Descricao";
            this.Descricao.Name = "Descricao";
            this.Descricao.ReadOnly = true;
            // 
            // NcmNfe
            // 
            this.NcmNfe.HeaderText = "NCM";
            this.NcmNfe.Name = "NcmNfe";
            this.NcmNfe.ReadOnly = true;
            // 
            // cstItemNotaFiscal
            // 
            this.cstItemNotaFiscal.HeaderText = "CST";
            this.cstItemNotaFiscal.Name = "cstItemNotaFiscal";
            this.cstItemNotaFiscal.ReadOnly = true;
            // 
            // CFOPNfe
            // 
            this.CFOPNfe.HeaderText = "CFOP";
            this.CFOPNfe.Name = "CFOPNfe";
            this.CFOPNfe.ReadOnly = true;
            // 
            // UnidNfe
            // 
            this.UnidNfe.HeaderText = "Unid.";
            this.UnidNfe.Name = "UnidNfe";
            this.UnidNfe.ReadOnly = true;
            // 
            // QtdeNfe
            // 
            this.QtdeNfe.HeaderText = "Qtde.";
            this.QtdeNfe.Name = "QtdeNfe";
            this.QtdeNfe.ReadOnly = true;
            // 
            // VUnitNfe
            // 
            this.VUnitNfe.HeaderText = "V. Unit ";
            this.VUnitNfe.Name = "VUnitNfe";
            this.VUnitNfe.ReadOnly = true;
            // 
            // VTotalNfe
            // 
            this.VTotalNfe.HeaderText = "V. total";
            this.VTotalNfe.Name = "VTotalNfe";
            this.VTotalNfe.ReadOnly = true;
            // 
            // BCICMSNfe
            // 
            this.BCICMSNfe.HeaderText = "BC ICMS";
            this.BCICMSNfe.Name = "BCICMSNfe";
            this.BCICMSNfe.ReadOnly = true;
            // 
            // VICMSNfe
            // 
            this.VICMSNfe.HeaderText = "V. ICMS";
            this.VICMSNfe.Name = "VICMSNfe";
            this.VICMSNfe.ReadOnly = true;
            // 
            // VIPINfe
            // 
            this.VIPINfe.HeaderText = "V. IPI";
            this.VIPINfe.Name = "VIPINfe";
            this.VIPINfe.ReadOnly = true;
            // 
            // AliquotaNfe
            // 
            this.AliquotaNfe.HeaderText = "Aliq. ICMS";
            this.AliquotaNfe.Name = "AliquotaNfe";
            this.AliquotaNfe.ReadOnly = true;
            // 
            // AliqIpNfe
            // 
            this.AliqIpNfe.HeaderText = "Aliq. IPI";
            this.AliqIpNfe.Name = "AliqIpNfe";
            this.AliqIpNfe.ReadOnly = true;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.groupBox1.Controls.Add(this.btnSairOuCancelarItemEntradaNota);
            this.groupBox1.Controls.Add(this.btnOkItemEntradaNota);
            this.groupBox1.Location = new System.Drawing.Point(-1, 372);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1241, 51);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            // 
            // btnSairOuCancelarItemEntradaNota
            // 
            this.btnSairOuCancelarItemEntradaNota.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSairOuCancelarItemEntradaNota.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSairOuCancelarItemEntradaNota.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnSairOuCancelarItemEntradaNota.Image = ((System.Drawing.Image)(resources.GetObject("btnSairOuCancelarItemEntradaNota.Image")));
            this.btnSairOuCancelarItemEntradaNota.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSairOuCancelarItemEntradaNota.Location = new System.Drawing.Point(390, 11);
            this.btnSairOuCancelarItemEntradaNota.Name = "btnSairOuCancelarItemEntradaNota";
            this.btnSairOuCancelarItemEntradaNota.Size = new System.Drawing.Size(225, 34);
            this.btnSairOuCancelarItemEntradaNota.TabIndex = 24;
            this.btnSairOuCancelarItemEntradaNota.Text = "Cancelar";
            this.btnSairOuCancelarItemEntradaNota.UseVisualStyleBackColor = true;
            this.btnSairOuCancelarItemEntradaNota.Click += new System.EventHandler(this.btnSairOuCancelarItemEntradaNota_Click);
            // 
            // btnOkItemEntradaNota
            // 
            this.btnOkItemEntradaNota.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOkItemEntradaNota.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOkItemEntradaNota.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnOkItemEntradaNota.Image = ((System.Drawing.Image)(resources.GetObject("btnOkItemEntradaNota.Image")));
            this.btnOkItemEntradaNota.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOkItemEntradaNota.Location = new System.Drawing.Point(644, 11);
            this.btnOkItemEntradaNota.Name = "btnOkItemEntradaNota";
            this.btnOkItemEntradaNota.Size = new System.Drawing.Size(225, 34);
            this.btnOkItemEntradaNota.TabIndex = 23;
            this.btnOkItemEntradaNota.Text = "Carrega Todos";
            this.btnOkItemEntradaNota.UseVisualStyleBackColor = true;
            this.btnOkItemEntradaNota.Click += new System.EventHandler(this.btnOkItemEntradaNota_Click);
            // 
            // itemDevolucao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1240, 423);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvItensNotaFiscalDevItem);
            this.Name = "itemDevolucao";
            this.Text = "itemDevolucao";
            this.Load += new System.EventHandler(this.itemDevolucao_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItensNotaFiscalDevItem)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.DataGridView dgvItensNotaFiscalDevItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemNfe;
        private System.Windows.Forms.DataGridViewTextBoxColumn Codigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descricao;
        private System.Windows.Forms.DataGridViewTextBoxColumn NcmNfe;
        private System.Windows.Forms.DataGridViewTextBoxColumn cstItemNotaFiscal;
        private System.Windows.Forms.DataGridViewTextBoxColumn CFOPNfe;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnidNfe;
        private System.Windows.Forms.DataGridViewTextBoxColumn QtdeNfe;
        private System.Windows.Forms.DataGridViewTextBoxColumn VUnitNfe;
        private System.Windows.Forms.DataGridViewTextBoxColumn VTotalNfe;
        private System.Windows.Forms.DataGridViewTextBoxColumn BCICMSNfe;
        private System.Windows.Forms.DataGridViewTextBoxColumn VICMSNfe;
        private System.Windows.Forms.DataGridViewTextBoxColumn VIPINfe;
        private System.Windows.Forms.DataGridViewTextBoxColumn AliquotaNfe;
        private System.Windows.Forms.DataGridViewTextBoxColumn AliqIpNfe;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSairOuCancelarItemEntradaNota;
        private System.Windows.Forms.Button btnOkItemEntradaNota;
    }
}