namespace conectorPDV001
{
    partial class consultaClienteNome
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
            this.dgvConsultaNome = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConsultaNome)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvConsultaNome
            // 
            this.dgvConsultaNome.AllowUserToAddRows = false;
            this.dgvConsultaNome.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvConsultaNome.BackgroundColor = System.Drawing.Color.LightSkyBlue;
            this.dgvConsultaNome.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.dgvConsultaNome.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvConsultaNome.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvConsultaNome.GridColor = System.Drawing.SystemColors.ButtonFace;
            this.dgvConsultaNome.Location = new System.Drawing.Point(0, 0);
            this.dgvConsultaNome.Name = "dgvConsultaNome";
            this.dgvConsultaNome.ReadOnly = true;
            this.dgvConsultaNome.RowHeadersVisible = false;
            this.dgvConsultaNome.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvConsultaNome.Size = new System.Drawing.Size(558, 413);
            this.dgvConsultaNome.TabIndex = 1;
            this.dgvConsultaNome.DoubleClick += new System.EventHandler(this.dgvConsultaNome_DoubleClick);
            this.dgvConsultaNome.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvConsultaNome_CellClick);
            this.dgvConsultaNome.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dgvConsultaNome_KeyPress);
            this.dgvConsultaNome.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgvConsultaNome_KeyUp);
            // 
            // consultaClienteNome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSkyBlue;
            this.ClientSize = new System.Drawing.Size(558, 413);
            this.Controls.Add(this.dgvConsultaNome);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "consultaClienteNome";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "consultaClienteNome";
            this.Load += new System.EventHandler(this.consultaClienteNome_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.consultaClienteNome_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvConsultaNome)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvConsultaNome;
    }
}