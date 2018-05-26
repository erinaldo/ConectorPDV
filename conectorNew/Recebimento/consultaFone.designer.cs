namespace conectorPDV001
{
    partial class consultaFone
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
            this.dgvConsultaFone = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConsultaFone)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvConsultaFone
            // 
            this.dgvConsultaFone.AllowUserToAddRows = false;
            this.dgvConsultaFone.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvConsultaFone.BackgroundColor = System.Drawing.Color.LightSkyBlue;
            this.dgvConsultaFone.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.dgvConsultaFone.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvConsultaFone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvConsultaFone.GridColor = System.Drawing.SystemColors.ButtonFace;
            this.dgvConsultaFone.Location = new System.Drawing.Point(0, 0);
            this.dgvConsultaFone.Name = "dgvConsultaFone";
            this.dgvConsultaFone.ReadOnly = true;
            this.dgvConsultaFone.RowHeadersVisible = false;
            this.dgvConsultaFone.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvConsultaFone.Size = new System.Drawing.Size(479, 393);
            this.dgvConsultaFone.TabIndex = 0;
            this.dgvConsultaFone.DoubleClick += new System.EventHandler(this.dgvConsultaFone_DoubleClick);
            this.dgvConsultaFone.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvConsultaFone_CellClick);
            this.dgvConsultaFone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dgvConsultaFone_KeyPress);
            this.dgvConsultaFone.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgvConsultaFone_KeyUp);
            this.dgvConsultaFone.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvConsultaFone_CellContentClick);
            // 
            // consultaFone
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 393);
            this.Controls.Add(this.dgvConsultaFone);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "consultaFone";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "consultaFone";
            this.Load += new System.EventHandler(this.consultaFone_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.consultaFone_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvConsultaFone)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvConsultaFone;
    }
}