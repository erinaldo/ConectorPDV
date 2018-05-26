namespace conectorPDV001
{
    partial class cep
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
            this.btnCarregarCep = new System.Windows.Forms.Button();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.txtComplementoCep = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCodMun = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCidadeCep = new System.Windows.Forms.TextBox();
            this.txtUfCep = new System.Windows.Forms.TextBox();
            this.lblUf = new System.Windows.Forms.Label();
            this.lblCidade = new System.Windows.Forms.Label();
            this.txtBairroCep = new System.Windows.Forms.TextBox();
            this.lblBairro = new System.Windows.Forms.Label();
            this.txtRuaAvenidaCep = new System.Windows.Forms.TextBox();
            this.lblRuaAvenida = new System.Windows.Forms.Label();
            this.GroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCarregarCep
            // 
            this.btnCarregarCep.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCarregarCep.Location = new System.Drawing.Point(0, 186);
            this.btnCarregarCep.Name = "btnCarregarCep";
            this.btnCarregarCep.Size = new System.Drawing.Size(479, 30);
            this.btnCarregarCep.TabIndex = 12;
            this.btnCarregarCep.Text = "Carregar informações do cep.";
            this.btnCarregarCep.UseVisualStyleBackColor = true;
            this.btnCarregarCep.Click += new System.EventHandler(this.btnCarregarCep_Click);
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.txtComplementoCep);
            this.GroupBox1.Controls.Add(this.label2);
            this.GroupBox1.Controls.Add(this.txtCodMun);
            this.GroupBox1.Controls.Add(this.label1);
            this.GroupBox1.Controls.Add(this.txtCidadeCep);
            this.GroupBox1.Controls.Add(this.txtUfCep);
            this.GroupBox1.Controls.Add(this.lblUf);
            this.GroupBox1.Controls.Add(this.lblCidade);
            this.GroupBox1.Controls.Add(this.txtBairroCep);
            this.GroupBox1.Controls.Add(this.lblBairro);
            this.GroupBox1.Controls.Add(this.txtRuaAvenidaCep);
            this.GroupBox1.Controls.Add(this.lblRuaAvenida);
            this.GroupBox1.Location = new System.Drawing.Point(0, -1);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(479, 188);
            this.GroupBox1.TabIndex = 11;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Consulta CEP";
            // 
            // txtComplementoCep
            // 
            this.txtComplementoCep.Location = new System.Drawing.Point(111, 120);
            this.txtComplementoCep.Name = "txtComplementoCep";
            this.txtComplementoCep.ReadOnly = true;
            this.txtComplementoCep.Size = new System.Drawing.Size(336, 20);
            this.txtComplementoCep.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Complemento";
            // 
            // txtCodMun
            // 
            this.txtCodMun.Location = new System.Drawing.Point(111, 153);
            this.txtCodMun.Name = "txtCodMun";
            this.txtCodMun.ReadOnly = true;
            this.txtCodMun.Size = new System.Drawing.Size(139, 20);
            this.txtCodMun.TabIndex = 11;
            this.txtCodMun.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 156);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Codigo Municipio";
            // 
            // txtCidadeCep
            // 
            this.txtCidadeCep.Location = new System.Drawing.Point(111, 90);
            this.txtCidadeCep.Name = "txtCidadeCep";
            this.txtCidadeCep.ReadOnly = true;
            this.txtCidadeCep.Size = new System.Drawing.Size(336, 20);
            this.txtCidadeCep.TabIndex = 7;
            // 
            // txtUfCep
            // 
            this.txtUfCep.Location = new System.Drawing.Point(378, 152);
            this.txtUfCep.Name = "txtUfCep";
            this.txtUfCep.ReadOnly = true;
            this.txtUfCep.Size = new System.Drawing.Size(69, 20);
            this.txtUfCep.TabIndex = 9;
            this.txtUfCep.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblUf
            // 
            this.lblUf.AutoSize = true;
            this.lblUf.Location = new System.Drawing.Point(355, 155);
            this.lblUf.Name = "lblUf";
            this.lblUf.Size = new System.Drawing.Size(21, 13);
            this.lblUf.TabIndex = 5;
            this.lblUf.Text = "UF";
            // 
            // lblCidade
            // 
            this.lblCidade.AutoSize = true;
            this.lblCidade.Location = new System.Drawing.Point(68, 93);
            this.lblCidade.Name = "lblCidade";
            this.lblCidade.Size = new System.Drawing.Size(40, 13);
            this.lblCidade.TabIndex = 4;
            this.lblCidade.Text = "Cidade";
            // 
            // txtBairroCep
            // 
            this.txtBairroCep.Location = new System.Drawing.Point(111, 57);
            this.txtBairroCep.Name = "txtBairroCep";
            this.txtBairroCep.ReadOnly = true;
            this.txtBairroCep.Size = new System.Drawing.Size(336, 20);
            this.txtBairroCep.TabIndex = 3;
            // 
            // lblBairro
            // 
            this.lblBairro.AutoSize = true;
            this.lblBairro.Location = new System.Drawing.Point(75, 60);
            this.lblBairro.Name = "lblBairro";
            this.lblBairro.Size = new System.Drawing.Size(34, 13);
            this.lblBairro.TabIndex = 2;
            this.lblBairro.Text = "Bairro";
            // 
            // txtRuaAvenidaCep
            // 
            this.txtRuaAvenidaCep.Location = new System.Drawing.Point(111, 24);
            this.txtRuaAvenidaCep.MaxLength = 80;
            this.txtRuaAvenidaCep.Name = "txtRuaAvenidaCep";
            this.txtRuaAvenidaCep.ReadOnly = true;
            this.txtRuaAvenidaCep.Size = new System.Drawing.Size(336, 20);
            this.txtRuaAvenidaCep.TabIndex = 1;
            this.txtRuaAvenidaCep.TextChanged += new System.EventHandler(this.txtRuaAvenidaCep_TextChanged);
            // 
            // lblRuaAvenida
            // 
            this.lblRuaAvenida.AutoSize = true;
            this.lblRuaAvenida.Location = new System.Drawing.Point(47, 27);
            this.lblRuaAvenida.Name = "lblRuaAvenida";
            this.lblRuaAvenida.Size = new System.Drawing.Size(61, 13);
            this.lblRuaAvenida.TabIndex = 0;
            this.lblRuaAvenida.Text = "Logradouro";
            // 
            // cep
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 216);
            this.Controls.Add(this.btnCarregarCep);
            this.Controls.Add(this.GroupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "cep";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "cep";
            this.Load += new System.EventHandler(this.cep_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cep_KeyDown);
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button btnCarregarCep;
        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.TextBox txtCidadeCep;
        internal System.Windows.Forms.TextBox txtUfCep;
        internal System.Windows.Forms.Label lblUf;
        internal System.Windows.Forms.Label lblCidade;
        internal System.Windows.Forms.TextBox txtBairroCep;
        internal System.Windows.Forms.Label lblBairro;
        internal System.Windows.Forms.TextBox txtRuaAvenidaCep;
        internal System.Windows.Forms.Label lblRuaAvenida;
        internal System.Windows.Forms.TextBox txtCodMun;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.TextBox txtComplementoCep;
        internal System.Windows.Forms.Label label2;
    }
}