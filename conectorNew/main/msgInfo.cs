using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace conectorPDV001
{
    public partial class msgInfo : Form
    {
        public msgInfo()
        {
            InitializeComponent();
        }
        public msgInfo(short tef, string msg)
        {
            InitializeComponent();
            txtMsgInfo.ReadOnly = true;
            txtMsgInfo.Text = msg;
            modoTef = tef;
            if ("String was not recognized as a valid DateTime." == msg)
            {
                return;
            }
        }
        public msgInfo(string msg)
        {
            InitializeComponent();
            txtMsgInfo.ReadOnly = true;
            txtMsgInfo.Text = msg;
        }
        public msgInfo(short tef, string msg, short time)
        {
            InitializeComponent();
            txtMsgInfo.ReadOnly = true;
            msgInfo1();
            //modoTef = tef;
        }
        public msgInfo(bool visivel, int time, string aux, string aux1, string aux2)
        {
            InitializeComponent();
            label1.Visible = visivel;
            label2.Visible = visivel;
            clock.Enabled = true;
            meta = time;
            //modoTef = tef;
        }
        //###########################################Variaveis Encapsuladas##################################################
        private short modoTef = 0;
        private int num = 0;
        private int meta = 0;
        //###########################################End Variaveis Encapsuladas##############################################
        //###########################################Variaveis Encapsuladas##################################################
        private void msgInfo1()
        {
            this.ClientSize = new System.Drawing.Size(708, 543); //this.msgInfo.Size = new System.Drawing.Size(508, 343);
            this.txtMsgInfo.Size = new System.Drawing.Size(708, 543);
            this.gpbMsgInfor.Size = new System.Drawing.Size(708, 543);
            txtMsgInfo.Text = "MENU DE ATALHOS DO TECLADO\r\n\r\n F1 - INSERI CLIENTE NA ABERTURA DE CUPOM\r\n F2 - CANCELA CUPOM FISCAL\r\n F3 - CANCELA ÚLTIMO ITEM INSERIDO\r\n F4 - CANCELA ITEM ALEATÓRIO\r\n F5 - ACRÉSCIMO DE ITEM\r\n F6 - DESCONTO DE ITEM\r\n F7 - DESCONTO DE ITEM ALEATÓRIO\r\n F8 - ACRÉSCIMO DE ITEM ALEATÓRIO\r\n F9 - CARREGA DAV/PRÉ-VENDA\r\n F10 - FINALIZAÇÃO/RECEBIMENTO CUPOM FISCAL\r\n F11 - SAIR DO SISTEMA\r\n F12 - RECEBIMENTO DE PRESTAÇÃO\r\n ESPAÇO - FECHA[TOTALIZA] CUPOM FISCAL\r\n Ctrl + F - CONSULTA PRODUTO\r\n Ctrl + W - MENU FISCAL\r\n  Ctrl + M - RESUMO DE VENDAS\r\n  ESC - RETORNO DE FUNÇÃO/MENU SUPERVISOR \r\n\r\n ESC - SAIR";
        }
        //###########################################End Variaveis Encapsuladas##############################################
        private void msgInfo_Load(object sender, EventArgs e)
        {
            cronometroInfo.Start();
        }

        private void msgInfo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F11)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Dispose();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Dispose();
            }else if (e.KeyCode == Keys.F12 && modoTef == 1)
            {
                this.DialogResult = DialogResult.Yes;
                this.Dispose();
            }
            else if (e.KeyCode == Keys.F12)
            {
                this.DialogResult = DialogResult.Yes;
                this.Dispose();
            }
        }

        private void txtMsgInfo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMsgInfo_Validated(object sender, EventArgs e)
        {
            txtMsgInfo.Select();
        }

        private void cronometroInfo_Tick(object sender, EventArgs e)
        {
            if (modoTef == 1)
            {
                this.Close();
            }
            else if(modoTef == 2)
            {
                System.Threading.Thread.Sleep(6000);
                this.Close();
            }            
        }

        private void clock_Tick(object sender, EventArgs e)
        {
            label1.Text = num.ToString();
            num = num + 1;
            if (num == meta)
            {
                label1.Text = label1.Text + " [TEMPO ESGOTADO - UMA NOTA TENTATIVA INICIARÁ - AGUARDE] ";
                this.Dispose();
            }
        }
    }
}
