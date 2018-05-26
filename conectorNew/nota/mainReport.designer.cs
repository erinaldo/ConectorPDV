namespace conector
{
    partial class mainReport
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
            this.CrystalReport11 = new conector.report.clienteLista();
            this.clienteLista1 = new conector.report.clienteLista();
            this.destalhesEntrada1 = new conectorPDV001.report.destalhesEntrada();
            this.reportView = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.BoletoLayout1 = new conector.report.BoletoLayout();
            this.testeReport1 = new conector.report.extratoConta();
            this.SuspendLayout();
            // 
            // reportView
            // 
            this.reportView.ActiveViewIndex = 0;
            this.reportView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.reportView.DisplayGroupTree = false;
            this.reportView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportView.Location = new System.Drawing.Point(0, 0);
            this.reportView.Name = "reportView";
            this.reportView.ReportSource = this.BoletoLayout1;
            this.reportView.Size = new System.Drawing.Size(799, 566);
            this.reportView.TabIndex = 0;
            // 
            // mainReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 566);
            this.Controls.Add(this.reportView);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "mainReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Relatorio";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.mainReport_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mainReport_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer reportView;
        private conector.report.clienteLista CrystalReport11;
        private conector.report.clienteLista clienteLista1;
        private conector.report.destalhesEntrada destalhesEntrada1;
        private conector.report.extratoConta testeReport1;
        private conector.report.BoletoLayout BoletoLayout1;
        //private pedidoVenda pedidoVenda1;
    }
}

