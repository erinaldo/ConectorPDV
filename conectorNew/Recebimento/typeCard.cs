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
    public partial class typeCard : Form
    {
        public typeCard()
        {
            InitializeComponent();
        }
        public typeCard(string test)
        {
            InitializeComponent();
            this.BackColor = System.Drawing.Color.Blue;
        }
        //###################################################Variaveis Encapsuladas#############################################
        private string type = "0";
        //###################################################End Variaveis #####################################################
        //###################################################Metodo de Controle#################################################
        public string Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }
        //###################################################End Controle#######################################################
        private void typeCard_Load(object sender, EventArgs e)
        {
            type = "0";
        }

        private void lnkDebitoTypeCard_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            type = "1";
            this.Dispose();
            this.DialogResult = DialogResult.OK;
        }

        private void lnkCreditoTypeCard_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            type = "2";
            this.Dispose();
            this.DialogResult = DialogResult.OK;
        }

        private void lnkTicketTypeCard_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            type = "3";
            this.Dispose();
            this.DialogResult = DialogResult.OK;
        }

        private void typeCard_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Dispose();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                ProcessTabKey(true);
            }
            else if (e.KeyCode == Keys.F2)
            {
                type = "1";
                this.Dispose();
                this.DialogResult = DialogResult.OK;
            }
            else if (e.KeyCode == Keys.F3)
            {
                type = "2";
                this.Dispose();
                this.DialogResult = DialogResult.OK;
            }
            else if (e.KeyCode == Keys.F4)
            {
                type = "3";
                this.Dispose();
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
