namespace conectorPDV001
{
    partial class mainConfig
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
            this.components = new System.ComponentModel.Container();
            this.pgbWaitMainConfig = new System.Windows.Forms.ProgressBar();
            this.ptbMainConfig = new System.Windows.Forms.PictureBox();
            this.clock = new System.Windows.Forms.Timer(this.components);
            this.lbMsgMainConfig = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lbStatusMainConfig = new System.Windows.Forms.Label();
            this.lbNumeroSerieMainConfig = new System.Windows.Forms.Label();
            this.lbLojaMainConfig = new System.Windows.Forms.Label();
            this.lbMovimentoMainConfig = new System.Windows.Forms.Label();
            this.lbDataImpressoraMainConfig = new System.Windows.Forms.Label();
            this.lbHoraImpressoraMainConfig = new System.Windows.Forms.Label();
            this.lbDataSistemaMainConfig = new System.Windows.Forms.Label();
            this.lbHoraSistemaMainConfig = new System.Windows.Forms.Label();
            this.lbTentativasMainConfig = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lbCaixaMainConfig = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lbFormaPgtoMainConfig = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lbGTMainConfig = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lbPdvMainConfig = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lbDataLiberacaoMainConfig = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.lbDataExpiracaoMainConfig = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.gpbStatusMainConfig = new System.Windows.Forms.GroupBox();
            this.lblProcessoErros = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.lblVersao = new System.Windows.Forms.Label();
            this.lblProcesso = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblMenuFiscal = new System.Windows.Forms.Label();
            this.lblAtalhoFiscal = new System.Windows.Forms.Label();
            this.lbIpTerminalMainConfig = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.lblCargaMainConfig = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.lbErroMainConfig = new System.Windows.Forms.Label();
            this.backgroundInitial = new System.ComponentModel.BackgroundWorker();
            this.lblObs = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ptbMainConfig)).BeginInit();
            this.gpbStatusMainConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // pgbWaitMainConfig
            // 
            this.pgbWaitMainConfig.Location = new System.Drawing.Point(2, 568);
            this.pgbWaitMainConfig.Maximum = 8;
            this.pgbWaitMainConfig.Name = "pgbWaitMainConfig";
            this.pgbWaitMainConfig.Size = new System.Drawing.Size(782, 30);
            this.pgbWaitMainConfig.TabIndex = 0;
            // 
            // ptbMainConfig
            // 
            this.ptbMainConfig.Image = global::conectorPDV001.Properties.Resources.logo33k;
            this.ptbMainConfig.Location = new System.Drawing.Point(2, 2);
            this.ptbMainConfig.Name = "ptbMainConfig";
            this.ptbMainConfig.Size = new System.Drawing.Size(782, 537);
            this.ptbMainConfig.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ptbMainConfig.TabIndex = 1;
            this.ptbMainConfig.TabStop = false;
            this.ptbMainConfig.Click += new System.EventHandler(this.ptbMainConfig_Click);
            // 
            // clock
            // 
            this.clock.Interval = 6000;
            this.clock.Tick += new System.EventHandler(this.clock_Tick);
            // 
            // lbMsgMainConfig
            // 
            this.lbMsgMainConfig.AutoSize = true;
            this.lbMsgMainConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMsgMainConfig.ForeColor = System.Drawing.Color.Cyan;
            this.lbMsgMainConfig.Location = new System.Drawing.Point(3, 547);
            this.lbMsgMainConfig.Name = "lbMsgMainConfig";
            this.lbMsgMainConfig.Size = new System.Drawing.Size(93, 13);
            this.lbMsgMainConfig.TabIndex = 2;
            this.lbMsgMainConfig.Text = "CARREGANDO";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Cyan;
            this.label1.Location = new System.Drawing.Point(78, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "IMPRESSORA FISCAL";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Cyan;
            this.label2.Location = new System.Drawing.Point(78, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "STATUS:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Cyan;
            this.label3.Location = new System.Drawing.Point(32, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "NUMERO SÉRIE:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Cyan;
            this.label4.Location = new System.Drawing.Point(97, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "LOJA:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Cyan;
            this.label5.Location = new System.Drawing.Point(52, 138);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "MOVIMENTO:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Cyan;
            this.label6.Location = new System.Drawing.Point(9, 155);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(129, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "DATA IMPRESSORA:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Cyan;
            this.label7.Location = new System.Drawing.Point(38, 191);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(102, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "DATA SISTEMA:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Cyan;
            this.label8.Location = new System.Drawing.Point(36, 209);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(104, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "HORA SISTEMA:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Cyan;
            this.label9.Location = new System.Drawing.Point(9, 173);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(131, 13);
            this.label9.TabIndex = 10;
            this.label9.Text = "HORA IMPRESSORA:";
            // 
            // lbStatusMainConfig
            // 
            this.lbStatusMainConfig.AutoSize = true;
            this.lbStatusMainConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbStatusMainConfig.ForeColor = System.Drawing.Color.Yellow;
            this.lbStatusMainConfig.Location = new System.Drawing.Point(136, 72);
            this.lbStatusMainConfig.Name = "lbStatusMainConfig";
            this.lbStatusMainConfig.Size = new System.Drawing.Size(15, 13);
            this.lbStatusMainConfig.TabIndex = 12;
            this.lbStatusMainConfig.Text = "#";
            // 
            // lbNumeroSerieMainConfig
            // 
            this.lbNumeroSerieMainConfig.AutoSize = true;
            this.lbNumeroSerieMainConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNumeroSerieMainConfig.ForeColor = System.Drawing.Color.Yellow;
            this.lbNumeroSerieMainConfig.Location = new System.Drawing.Point(136, 89);
            this.lbNumeroSerieMainConfig.Name = "lbNumeroSerieMainConfig";
            this.lbNumeroSerieMainConfig.Size = new System.Drawing.Size(15, 13);
            this.lbNumeroSerieMainConfig.TabIndex = 13;
            this.lbNumeroSerieMainConfig.Text = "#";
            // 
            // lbLojaMainConfig
            // 
            this.lbLojaMainConfig.AutoSize = true;
            this.lbLojaMainConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLojaMainConfig.ForeColor = System.Drawing.Color.Yellow;
            this.lbLojaMainConfig.Location = new System.Drawing.Point(136, 105);
            this.lbLojaMainConfig.Name = "lbLojaMainConfig";
            this.lbLojaMainConfig.Size = new System.Drawing.Size(15, 13);
            this.lbLojaMainConfig.TabIndex = 14;
            this.lbLojaMainConfig.Text = "#";
            // 
            // lbMovimentoMainConfig
            // 
            this.lbMovimentoMainConfig.AutoSize = true;
            this.lbMovimentoMainConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMovimentoMainConfig.ForeColor = System.Drawing.Color.Yellow;
            this.lbMovimentoMainConfig.Location = new System.Drawing.Point(136, 138);
            this.lbMovimentoMainConfig.Name = "lbMovimentoMainConfig";
            this.lbMovimentoMainConfig.Size = new System.Drawing.Size(15, 13);
            this.lbMovimentoMainConfig.TabIndex = 15;
            this.lbMovimentoMainConfig.Text = "#";
            // 
            // lbDataImpressoraMainConfig
            // 
            this.lbDataImpressoraMainConfig.AutoSize = true;
            this.lbDataImpressoraMainConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDataImpressoraMainConfig.ForeColor = System.Drawing.Color.Yellow;
            this.lbDataImpressoraMainConfig.Location = new System.Drawing.Point(136, 155);
            this.lbDataImpressoraMainConfig.Name = "lbDataImpressoraMainConfig";
            this.lbDataImpressoraMainConfig.Size = new System.Drawing.Size(15, 13);
            this.lbDataImpressoraMainConfig.TabIndex = 16;
            this.lbDataImpressoraMainConfig.Text = "#";
            // 
            // lbHoraImpressoraMainConfig
            // 
            this.lbHoraImpressoraMainConfig.AutoSize = true;
            this.lbHoraImpressoraMainConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHoraImpressoraMainConfig.ForeColor = System.Drawing.Color.Yellow;
            this.lbHoraImpressoraMainConfig.Location = new System.Drawing.Point(137, 172);
            this.lbHoraImpressoraMainConfig.Name = "lbHoraImpressoraMainConfig";
            this.lbHoraImpressoraMainConfig.Size = new System.Drawing.Size(15, 13);
            this.lbHoraImpressoraMainConfig.TabIndex = 17;
            this.lbHoraImpressoraMainConfig.Text = "#";
            // 
            // lbDataSistemaMainConfig
            // 
            this.lbDataSistemaMainConfig.AutoSize = true;
            this.lbDataSistemaMainConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDataSistemaMainConfig.ForeColor = System.Drawing.Color.Yellow;
            this.lbDataSistemaMainConfig.Location = new System.Drawing.Point(137, 190);
            this.lbDataSistemaMainConfig.Name = "lbDataSistemaMainConfig";
            this.lbDataSistemaMainConfig.Size = new System.Drawing.Size(15, 13);
            this.lbDataSistemaMainConfig.TabIndex = 18;
            this.lbDataSistemaMainConfig.Text = "#";
            // 
            // lbHoraSistemaMainConfig
            // 
            this.lbHoraSistemaMainConfig.AutoSize = true;
            this.lbHoraSistemaMainConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHoraSistemaMainConfig.ForeColor = System.Drawing.Color.Yellow;
            this.lbHoraSistemaMainConfig.Location = new System.Drawing.Point(137, 208);
            this.lbHoraSistemaMainConfig.Name = "lbHoraSistemaMainConfig";
            this.lbHoraSistemaMainConfig.Size = new System.Drawing.Size(15, 13);
            this.lbHoraSistemaMainConfig.TabIndex = 19;
            this.lbHoraSistemaMainConfig.Text = "#";
            // 
            // lbTentativasMainConfig
            // 
            this.lbTentativasMainConfig.AutoSize = true;
            this.lbTentativasMainConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTentativasMainConfig.ForeColor = System.Drawing.Color.Yellow;
            this.lbTentativasMainConfig.Location = new System.Drawing.Point(753, 547);
            this.lbTentativasMainConfig.Name = "lbTentativasMainConfig";
            this.lbTentativasMainConfig.Size = new System.Drawing.Size(28, 13);
            this.lbTentativasMainConfig.TabIndex = 20;
            this.lbTentativasMainConfig.Text = "$$$\r\n";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Cyan;
            this.label11.Location = new System.Drawing.Point(677, 547);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(76, 13);
            this.label11.TabIndex = 21;
            this.label11.Text = "TENTATIVA";
            // 
            // lbCaixaMainConfig
            // 
            this.lbCaixaMainConfig.AutoSize = true;
            this.lbCaixaMainConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCaixaMainConfig.ForeColor = System.Drawing.Color.Yellow;
            this.lbCaixaMainConfig.Location = new System.Drawing.Point(136, 121);
            this.lbCaixaMainConfig.Name = "lbCaixaMainConfig";
            this.lbCaixaMainConfig.Size = new System.Drawing.Size(15, 13);
            this.lbCaixaMainConfig.TabIndex = 23;
            this.lbCaixaMainConfig.Text = "#";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Cyan;
            this.label13.Location = new System.Drawing.Point(91, 121);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(47, 13);
            this.label13.TabIndex = 22;
            this.label13.Text = "CAIXA:";
            // 
            // lbFormaPgtoMainConfig
            // 
            this.lbFormaPgtoMainConfig.AutoSize = true;
            this.lbFormaPgtoMainConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFormaPgtoMainConfig.ForeColor = System.Drawing.Color.Yellow;
            this.lbFormaPgtoMainConfig.Location = new System.Drawing.Point(137, 226);
            this.lbFormaPgtoMainConfig.Name = "lbFormaPgtoMainConfig";
            this.lbFormaPgtoMainConfig.Size = new System.Drawing.Size(15, 13);
            this.lbFormaPgtoMainConfig.TabIndex = 25;
            this.lbFormaPgtoMainConfig.Text = "#";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Cyan;
            this.label14.Location = new System.Drawing.Point(47, 227);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(92, 13);
            this.label14.TabIndex = 24;
            this.label14.Text = "FORMA PGTO:";
            // 
            // lbGTMainConfig
            // 
            this.lbGTMainConfig.AutoSize = true;
            this.lbGTMainConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbGTMainConfig.ForeColor = System.Drawing.Color.Yellow;
            this.lbGTMainConfig.Location = new System.Drawing.Point(138, 245);
            this.lbGTMainConfig.Name = "lbGTMainConfig";
            this.lbGTMainConfig.Size = new System.Drawing.Size(15, 13);
            this.lbGTMainConfig.TabIndex = 27;
            this.lbGTMainConfig.Text = "#";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Cyan;
            this.label12.Location = new System.Drawing.Point(32, 245);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(107, 13);
            this.label12.TabIndex = 26;
            this.label12.Text = "GRANDE TOTAL:";
            // 
            // lbPdvMainConfig
            // 
            this.lbPdvMainConfig.AutoSize = true;
            this.lbPdvMainConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPdvMainConfig.ForeColor = System.Drawing.Color.Yellow;
            this.lbPdvMainConfig.Location = new System.Drawing.Point(138, 264);
            this.lbPdvMainConfig.Name = "lbPdvMainConfig";
            this.lbPdvMainConfig.Size = new System.Drawing.Size(15, 13);
            this.lbPdvMainConfig.TabIndex = 29;
            this.lbPdvMainConfig.Text = "#";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.Cyan;
            this.label15.Location = new System.Drawing.Point(103, 263);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(36, 13);
            this.label15.TabIndex = 28;
            this.label15.Text = "PDV:";
            // 
            // lbDataLiberacaoMainConfig
            // 
            this.lbDataLiberacaoMainConfig.AutoSize = true;
            this.lbDataLiberacaoMainConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDataLiberacaoMainConfig.ForeColor = System.Drawing.Color.Yellow;
            this.lbDataLiberacaoMainConfig.Location = new System.Drawing.Point(138, 280);
            this.lbDataLiberacaoMainConfig.Name = "lbDataLiberacaoMainConfig";
            this.lbDataLiberacaoMainConfig.Size = new System.Drawing.Size(15, 13);
            this.lbDataLiberacaoMainConfig.TabIndex = 31;
            this.lbDataLiberacaoMainConfig.Text = "#";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.Cyan;
            this.label17.Location = new System.Drawing.Point(22, 280);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(117, 13);
            this.label17.TabIndex = 30;
            this.label17.Text = "DATA LIBERAÇÃO:";
            // 
            // lbDataExpiracaoMainConfig
            // 
            this.lbDataExpiracaoMainConfig.AutoSize = true;
            this.lbDataExpiracaoMainConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDataExpiracaoMainConfig.ForeColor = System.Drawing.Color.Yellow;
            this.lbDataExpiracaoMainConfig.Location = new System.Drawing.Point(138, 298);
            this.lbDataExpiracaoMainConfig.Name = "lbDataExpiracaoMainConfig";
            this.lbDataExpiracaoMainConfig.Size = new System.Drawing.Size(15, 13);
            this.lbDataExpiracaoMainConfig.TabIndex = 33;
            this.lbDataExpiracaoMainConfig.Text = "#";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Cyan;
            this.label19.Location = new System.Drawing.Point(22, 298);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(118, 13);
            this.label19.TabIndex = 32;
            this.label19.Text = "DATA EXPIRAÇÃO:";
            // 
            // gpbStatusMainConfig
            // 
            this.gpbStatusMainConfig.BackColor = System.Drawing.Color.Maroon;
            this.gpbStatusMainConfig.Controls.Add(this.lblObs);
            this.gpbStatusMainConfig.Controls.Add(this.label23);
            this.gpbStatusMainConfig.Controls.Add(this.lblProcessoErros);
            this.gpbStatusMainConfig.Controls.Add(this.label22);
            this.gpbStatusMainConfig.Controls.Add(this.lblVersao);
            this.gpbStatusMainConfig.Controls.Add(this.lblProcesso);
            this.gpbStatusMainConfig.Controls.Add(this.label21);
            this.gpbStatusMainConfig.Controls.Add(this.label10);
            this.gpbStatusMainConfig.Controls.Add(this.lblMenuFiscal);
            this.gpbStatusMainConfig.Controls.Add(this.lblAtalhoFiscal);
            this.gpbStatusMainConfig.Controls.Add(this.lbIpTerminalMainConfig);
            this.gpbStatusMainConfig.Controls.Add(this.label18);
            this.gpbStatusMainConfig.Controls.Add(this.lblCargaMainConfig);
            this.gpbStatusMainConfig.Controls.Add(this.label16);
            this.gpbStatusMainConfig.Controls.Add(this.label2);
            this.gpbStatusMainConfig.Controls.Add(this.lbDataExpiracaoMainConfig);
            this.gpbStatusMainConfig.Controls.Add(this.label3);
            this.gpbStatusMainConfig.Controls.Add(this.label1);
            this.gpbStatusMainConfig.Controls.Add(this.label19);
            this.gpbStatusMainConfig.Controls.Add(this.label4);
            this.gpbStatusMainConfig.Controls.Add(this.lbDataLiberacaoMainConfig);
            this.gpbStatusMainConfig.Controls.Add(this.label5);
            this.gpbStatusMainConfig.Controls.Add(this.label17);
            this.gpbStatusMainConfig.Controls.Add(this.label6);
            this.gpbStatusMainConfig.Controls.Add(this.lbPdvMainConfig);
            this.gpbStatusMainConfig.Controls.Add(this.label7);
            this.gpbStatusMainConfig.Controls.Add(this.label15);
            this.gpbStatusMainConfig.Controls.Add(this.label9);
            this.gpbStatusMainConfig.Controls.Add(this.lbGTMainConfig);
            this.gpbStatusMainConfig.Controls.Add(this.label8);
            this.gpbStatusMainConfig.Controls.Add(this.label12);
            this.gpbStatusMainConfig.Controls.Add(this.lbStatusMainConfig);
            this.gpbStatusMainConfig.Controls.Add(this.lbFormaPgtoMainConfig);
            this.gpbStatusMainConfig.Controls.Add(this.lbNumeroSerieMainConfig);
            this.gpbStatusMainConfig.Controls.Add(this.label14);
            this.gpbStatusMainConfig.Controls.Add(this.lbLojaMainConfig);
            this.gpbStatusMainConfig.Controls.Add(this.lbCaixaMainConfig);
            this.gpbStatusMainConfig.Controls.Add(this.lbMovimentoMainConfig);
            this.gpbStatusMainConfig.Controls.Add(this.label13);
            this.gpbStatusMainConfig.Controls.Add(this.lbDataImpressoraMainConfig);
            this.gpbStatusMainConfig.Controls.Add(this.lbHoraImpressoraMainConfig);
            this.gpbStatusMainConfig.Controls.Add(this.lbDataSistemaMainConfig);
            this.gpbStatusMainConfig.Controls.Add(this.lbHoraSistemaMainConfig);
            this.gpbStatusMainConfig.Location = new System.Drawing.Point(786, -4);
            this.gpbStatusMainConfig.Name = "gpbStatusMainConfig";
            this.gpbStatusMainConfig.Size = new System.Drawing.Size(280, 602);
            this.gpbStatusMainConfig.TabIndex = 34;
            this.gpbStatusMainConfig.TabStop = false;
            // 
            // lblProcessoErros
            // 
            this.lblProcessoErros.AutoSize = true;
            this.lblProcessoErros.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProcessoErros.ForeColor = System.Drawing.Color.Yellow;
            this.lblProcessoErros.Location = new System.Drawing.Point(139, 381);
            this.lblProcessoErros.Name = "lblProcessoErros";
            this.lblProcessoErros.Size = new System.Drawing.Size(15, 13);
            this.lblProcessoErros.TabIndex = 45;
            this.lblProcessoErros.Text = "#";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.ForeColor = System.Drawing.Color.Cyan;
            this.label22.Location = new System.Drawing.Point(85, 381);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(54, 13);
            this.label22.TabIndex = 44;
            this.label22.Text = "ERROS:";
            // 
            // lblVersao
            // 
            this.lblVersao.AutoSize = true;
            this.lblVersao.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersao.ForeColor = System.Drawing.Color.Yellow;
            this.lblVersao.Location = new System.Drawing.Point(139, 365);
            this.lblVersao.Name = "lblVersao";
            this.lblVersao.Size = new System.Drawing.Size(15, 13);
            this.lblVersao.TabIndex = 43;
            this.lblVersao.Text = "#";
            // 
            // lblProcesso
            // 
            this.lblProcesso.AutoSize = true;
            this.lblProcesso.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProcesso.ForeColor = System.Drawing.Color.Yellow;
            this.lblProcesso.Location = new System.Drawing.Point(139, 350);
            this.lblProcesso.Name = "lblProcesso";
            this.lblProcesso.Size = new System.Drawing.Size(15, 13);
            this.lblProcesso.TabIndex = 42;
            this.lblProcesso.Text = "#";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.Cyan;
            this.label21.Location = new System.Drawing.Point(54, 349);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(86, 13);
            this.label21.TabIndex = 41;
            this.label21.Text = "PROCESSOS:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Cyan;
            this.label10.Location = new System.Drawing.Point(89, 364);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(50, 13);
            this.label10.TabIndex = 40;
            this.label10.Text = "Versão:";
            // 
            // lblMenuFiscal
            // 
            this.lblMenuFiscal.AutoSize = true;
            this.lblMenuFiscal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMenuFiscal.ForeColor = System.Drawing.Color.Yellow;
            this.lblMenuFiscal.Location = new System.Drawing.Point(85, 522);
            this.lblMenuFiscal.Name = "lblMenuFiscal";
            this.lblMenuFiscal.Size = new System.Drawing.Size(89, 13);
            this.lblMenuFiscal.TabIndex = 39;
            this.lblMenuFiscal.Text = "MENU FISCAL";
            this.lblMenuFiscal.Visible = false;
            // 
            // lblAtalhoFiscal
            // 
            this.lblAtalhoFiscal.AutoSize = true;
            this.lblAtalhoFiscal.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAtalhoFiscal.ForeColor = System.Drawing.Color.Red;
            this.lblAtalhoFiscal.Location = new System.Drawing.Point(8, 534);
            this.lblAtalhoFiscal.Name = "lblAtalhoFiscal";
            this.lblAtalhoFiscal.Size = new System.Drawing.Size(249, 63);
            this.lblAtalhoFiscal.TabIndex = 38;
            this.lblAtalhoFiscal.Text = "CTrl + W";
            this.lblAtalhoFiscal.Visible = false;
            // 
            // lbIpTerminalMainConfig
            // 
            this.lbIpTerminalMainConfig.AutoSize = true;
            this.lbIpTerminalMainConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbIpTerminalMainConfig.ForeColor = System.Drawing.Color.Yellow;
            this.lbIpTerminalMainConfig.Location = new System.Drawing.Point(138, 334);
            this.lbIpTerminalMainConfig.Name = "lbIpTerminalMainConfig";
            this.lbIpTerminalMainConfig.Size = new System.Drawing.Size(15, 13);
            this.lbIpTerminalMainConfig.TabIndex = 37;
            this.lbIpTerminalMainConfig.Text = "#";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.Cyan;
            this.label18.Location = new System.Drawing.Point(51, 333);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(90, 13);
            this.label18.TabIndex = 36;
            this.label18.Text = "IP TERMINAL:";
            // 
            // lblCargaMainConfig
            // 
            this.lblCargaMainConfig.AutoSize = true;
            this.lblCargaMainConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCargaMainConfig.ForeColor = System.Drawing.Color.Yellow;
            this.lblCargaMainConfig.Location = new System.Drawing.Point(138, 316);
            this.lblCargaMainConfig.Name = "lblCargaMainConfig";
            this.lblCargaMainConfig.Size = new System.Drawing.Size(15, 13);
            this.lblCargaMainConfig.TabIndex = 35;
            this.lblCargaMainConfig.Text = "#";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.Cyan;
            this.label16.Location = new System.Drawing.Point(88, 316);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(53, 13);
            this.label16.TabIndex = 34;
            this.label16.Text = "CARGA:";
            // 
            // lbErroMainConfig
            // 
            this.lbErroMainConfig.AutoSize = true;
            this.lbErroMainConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbErroMainConfig.ForeColor = System.Drawing.Color.Cyan;
            this.lbErroMainConfig.Location = new System.Drawing.Point(97, 547);
            this.lbErroMainConfig.Name = "lbErroMainConfig";
            this.lbErroMainConfig.Size = new System.Drawing.Size(19, 13);
            this.lbErroMainConfig.TabIndex = 34;
            this.lbErroMainConfig.Text = "...";
            this.lbErroMainConfig.Click += new System.EventHandler(this.lbErroMainConfig_Click);
            // 
            // backgroundInitial
            // 
            this.backgroundInitial.WorkerReportsProgress = true;
            this.backgroundInitial.WorkerSupportsCancellation = true;
            this.backgroundInitial.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundInitial_DoWork);
            // 
            // lblObs
            // 
            this.lblObs.AutoSize = true;
            this.lblObs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblObs.ForeColor = System.Drawing.Color.Yellow;
            this.lblObs.Location = new System.Drawing.Point(139, 399);
            this.lblObs.Name = "lblObs";
            this.lblObs.Size = new System.Drawing.Size(15, 13);
            this.lblObs.TabIndex = 47;
            this.lblObs.Text = "#";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.ForeColor = System.Drawing.Color.Cyan;
            this.label23.Location = new System.Drawing.Point(44, 399);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(94, 13);
            this.label23.TabIndex = 46;
            this.label23.Text = "OBSERVAÇÃO:";
            // 
            // mainConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1067, 600);
            this.Controls.Add(this.lbErroMainConfig);
            this.Controls.Add(this.gpbStatusMainConfig);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.lbTentativasMainConfig);
            this.Controls.Add(this.lbMsgMainConfig);
            this.Controls.Add(this.ptbMainConfig);
            this.Controls.Add(this.pgbWaitMainConfig);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "mainConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "mainConfig";
            this.Load += new System.EventHandler(this.mainConfig_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mainConfig_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.ptbMainConfig)).EndInit();
            this.gpbStatusMainConfig.ResumeLayout(false);
            this.gpbStatusMainConfig.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar pgbWaitMainConfig;
        private System.Windows.Forms.PictureBox ptbMainConfig;
        private System.Windows.Forms.Timer clock;
        private System.Windows.Forms.Label lbMsgMainConfig;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lbStatusMainConfig;
        private System.Windows.Forms.Label lbNumeroSerieMainConfig;
        private System.Windows.Forms.Label lbLojaMainConfig;
        private System.Windows.Forms.Label lbMovimentoMainConfig;
        private System.Windows.Forms.Label lbDataImpressoraMainConfig;
        private System.Windows.Forms.Label lbHoraImpressoraMainConfig;
        private System.Windows.Forms.Label lbDataSistemaMainConfig;
        private System.Windows.Forms.Label lbHoraSistemaMainConfig;
        private System.Windows.Forms.Label lbTentativasMainConfig;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lbCaixaMainConfig;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lbFormaPgtoMainConfig;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lbGTMainConfig;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lbPdvMainConfig;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lbDataLiberacaoMainConfig;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lbDataExpiracaoMainConfig;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.GroupBox gpbStatusMainConfig;
        private System.Windows.Forms.Label lbErroMainConfig;
        private System.Windows.Forms.Label lblCargaMainConfig;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label lbIpTerminalMainConfig;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label lblMenuFiscal;
        private System.Windows.Forms.Label lblAtalhoFiscal;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblProcesso;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label lblVersao;
        private System.ComponentModel.BackgroundWorker backgroundInitial;
        private System.Windows.Forms.Label lblProcessoErros;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label lblObs;
        private System.Windows.Forms.Label label23;
    }
}