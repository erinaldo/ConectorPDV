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
    public partial class networkCard : Form
    {
        public networkCard()
        {
            InitializeComponent();
            privativo = 1;
        }
        public networkCard(string parcela, string valorApurado)
        {
            InitializeComponent();
            auxNrParcelas = parcela;
            valueFull = valorApurado;
            privativo = 0;
        }
        //#####################################################Variaveis encapsuladas########################################
        private Decimal auxNumeric;
        private int privativo = 0;
        private string flagNumeric;
        private int posSeparator;
        private Int16 auxConsistencia = 0;
        private int flagSemaforo;
        private dados banco = new dados();
        private int i, j, countField, countRows; //variavel do loop.
        private string auxIdConectCard;
        private string valueFull;
        private string auxNrParcelas;
        private string auxIdContaDeposito;
        private string origem = "f";
        //#####################################################End variaveis#################################################

        //#####################################################Procedimento de banco#########################################
        //#####################################################End Procedimento de banco#####################################

        //#####################################################Controle de Objetos###########################################
        public string Equipamento
        {
            get
            {
                return auxIdConectCard;
            }
            set
            {
                auxIdConectCard = value;
            }
        }
        public string value_informado
        {
            get
            {
                return flagNumeric;
            }
            set
            {
                flagNumeric = value;
            }
        }
        void clearObj()
        {
            txtValorApuradoTefDedicadoNetworkCard.Text = "0.00";
            txtValorInformadoTefDedicadoNetworkCard.Text = "0.00";
            txtParcelamentoTefDedicadoNetworkCard.Text = "0.00";
            lbValorParcelaTefDedicadoNetworkCard.Text = "";
            txtValorApuradoTefDiscadoNetworkCard.Text = "0.00";
            txtValorInformadoTefDiscadoNetworkCard.Text = "0.00";
            txtParcelamentoTefDiscadoNetworkCard.Text = "0.00";
            lbValorParcelaTefDiscadoNetworkCard.Text = "";
        }
        //#####################################################End Controle de objetos#######################################
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void networkCard_Load(object sender, EventArgs e)
        {
            clearObj();
        }

        private void btnPesquisaTefDiscadoNetworkCard_Click(object sender, EventArgs e)
        {
            auxIdConectCard = "2";
            if (privativo == 0)
            {
                ptbTefDiscadoNetworkCard.Visible = false;
                txtValorApuradoTefDiscadoNetworkCard.Text = valueFull;
                txtParcelamentoTefDiscadoNetworkCard.Text = auxNrParcelas;
                txtValorInformadoTefDiscadoNetworkCard.Select();
                
            }else
            {
                this.Dispose();
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnPesquisaTefDedicadoNetworkCard_Click(object sender, EventArgs e)
        {
            auxIdConectCard = "3";
            if (privativo == 0)
            {
                ptbTefDedicadoNetworkCard.Visible = false;
                txtValorApuradoTefDedicadoNetworkCard.Text = valueFull;
                txtParcelamentoTefDedicadoNetworkCard.Text = auxNrParcelas;
                txtValorInformadoTefDedicadoNetworkCard.Select();
            }
            else
            {
                this.Dispose();
                this.DialogResult = DialogResult.OK;
            }
            
        }

        private void btnPesquisaPosCieloNetworkCard_Click(object sender, EventArgs e)
        {
            auxIdConectCard = "1";
            this.Dispose();
            this.DialogResult = DialogResult.OK;
        }
        private void btnCancelaTefDedicadoNetworkCard_Click(object sender, EventArgs e)
        {
            msgInfo msg = new msgInfo("Cancelado a operação sem finalizar o cartão.");
            if (msg.ShowDialog() == DialogResult.Cancel)
            {
                auxIdConectCard = "";
                auxIdContaDeposito = "";
                this.DialogResult = DialogResult.Cancel;
                this.Dispose();
            }
            else
            {
                ptbTefDedicadoNetworkCard.Visible = true;
                btnTefDedicadoConfirmaNetworkCard.Select();
            }

        }

        private void btnFinalizaTefDedicadoNetworkCard_Click(object sender, EventArgs e)
        {
            if (lbValorParcelaTefDedicadoNetworkCard.Text != "")
            {
                string text = (Convert.ToDecimal(lbValorParcelaTefDedicadoNetworkCard.Text == "" ? "0" : lbValorParcelaTefDedicadoNetworkCard.Text) * Convert.ToInt32(auxNrParcelas)).ToString();
                auxNumeric = Convert.ToDecimal(text);//currency
                flagNumeric = String.Format("{0:F2}", auxNumeric);
                if (txtValorInformadoTefDedicadoNetworkCard.Text == txtValorApuradoTefDedicadoNetworkCard.Text && lbValorParcelaTefDedicadoNetworkCard.Text != "" && lbValorParcelaTefDedicadoNetworkCard.Text != "0.00" && flagNumeric == txtValorApuradoTefDedicadoNetworkCard.Text)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Dispose();
                }
            }
            else
            {
                msgInfo msg = new msgInfo("Caro Cliente - " + "Deve-se confirmar primeiramente antes de finalizar o processo!"); msg.ShowDialog();
            }
            
        }

        private void btnTefDedicadoConfirmaNetworkCard_Click(object sender, EventArgs e)
        {
            if (txtValorInformadoTefDedicadoNetworkCard.Text != "0.00" && txtValorInformadoTefDedicadoNetworkCard.Text != "" && Convert.ToDecimal(txtValorInformadoTefDedicadoNetworkCard.Text) > 0)
            {
                lbValorParcelaTefDedicadoNetworkCard.Text = (Convert.ToDecimal(txtValorInformadoTefDedicadoNetworkCard.Text) / Convert.ToInt32(auxNrParcelas)).ToString();
                auxNumeric = Convert.ToDecimal(lbValorParcelaTefDedicadoNetworkCard.Text);//currency
                flagNumeric = String.Format("{0:F2}", auxNumeric);
                lbValorParcelaTefDedicadoNetworkCard.Text = flagNumeric.Replace(",", ".").Trim();
            }
            else
            {
                msgInfo msg = new msgInfo("Caro Cliente - " + "Transação não pode prosseguir, pois o valor apurado e diferente valor informado!"); msg.ShowDialog();
                txtValorInformadoTefDedicadoNetworkCard.Select();
            }
        }

        private void txtValorApuradoTefDedicadoNetworkCard_Validated(object sender, EventArgs e)
        {
            if (txtValorApuradoTefDedicadoNetworkCard.Text != "")
            {
                auxNumeric = Convert.ToDecimal(txtValorApuradoTefDedicadoNetworkCard.Text);//currency
                flagNumeric = String.Format("{0:F2}", auxNumeric);
                txtValorApuradoTefDedicadoNetworkCard.Text = flagNumeric.Replace(",", ".").Trim();
            }
            else
            {
                txtValorApuradoTefDedicadoNetworkCard.Text = "0.00";
            }
        }

        private void txtValorInformadoTefDedicadoNetworkCard_Validated(object sender, EventArgs e)
        {
            if (txtValorInformadoTefDedicadoNetworkCard.Text != "")
            {
                auxNumeric = Convert.ToDecimal(txtValorInformadoTefDedicadoNetworkCard.Text);//currency
                flagNumeric = String.Format("{0:F2}", auxNumeric);
                txtValorInformadoTefDedicadoNetworkCard.Text = flagNumeric.Replace(",", ".").Trim();
            }
            else
            {
                txtValorInformadoTefDedicadoNetworkCard.Text = "0.00";
            }
        }

        private void txtParcelamentoNetworkCard_Validated(object sender, EventArgs e)
        {
            if (txtParcelamentoTefDedicadoNetworkCard.Text != "")
            {
                auxNumeric = Convert.ToDecimal(txtParcelamentoTefDedicadoNetworkCard.Text);//currency
                flagNumeric = String.Format("{0:F2}", auxNumeric);
                txtParcelamentoTefDedicadoNetworkCard.Text = flagNumeric.Replace(",", ".").Trim();
            }
            else
            {
                txtParcelamentoTefDedicadoNetworkCard.Text = "0";
            }
        }

        private void txtValorApuradoTefDedicadoNetworkCard_KeyPress(object sender, KeyPressEventArgs e)
        {
            posSeparator = txtValorApuradoTefDedicadoNetworkCard.Text.IndexOf(".");
            //Se não for um numero e não  tecla backspace e nao for virgula, barra:
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (Char)Keys.Back && e.KeyChar != '.')
            {
                e.Handled = true;
                return;
            }
            if (e.KeyChar == '.' && posSeparator > -1)
            {
                e.Handled = true;
                return;
            }
        }

        private void txtValorInformadoTefDedicadoNetworkCard_KeyPress(object sender, KeyPressEventArgs e)
        {
            posSeparator = txtValorInformadoTefDedicadoNetworkCard.Text.IndexOf(".");
            //Se não for um numero e não  tecla backspace e nao for virgula, barra:
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (Char)Keys.Back && e.KeyChar != '.')
            {
                e.Handled = true;
                return;
            }
            if (e.KeyChar == '.' && posSeparator > -1)
            {
                e.Handled = true;
                return;
            }
        }

        private void txtParcelamentoTefDedicadoNetworkCard_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Se não for um numero e não  tecla backspace e nao for virgula, barra:
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (Char)Keys.Back && e.KeyChar != '.')
            {
                e.Handled = true;
                return;
            }
            if (e.KeyChar == '.' && posSeparator > -1)
            {
                e.Handled = true;
                return;
            }
        }

        private void txtValorApuradoTefDiscadobtnCancelaTefDedicadoNetworkCard_Validated(object sender, EventArgs e)
        {
            if (txtValorApuradoTefDiscadoNetworkCard.Text != "")
            {
                auxNumeric = Convert.ToDecimal(txtValorApuradoTefDiscadoNetworkCard.Text);//currency
                flagNumeric = String.Format("{0:F2}", auxNumeric);
                txtValorApuradoTefDiscadoNetworkCard.Text = flagNumeric.Replace(",", ".").Trim();
            }
            else
            {
                txtValorApuradoTefDiscadoNetworkCard.Text = "0.00";
            }
        }

        private void txtValorInformadoTefDiscadoNetworkCard_Validated(object sender, EventArgs e)
        {
            if (txtValorInformadoTefDiscadoNetworkCard.Text != "")
            {
                auxNumeric = Convert.ToDecimal(txtValorInformadoTefDiscadoNetworkCard.Text);//currency
                flagNumeric = String.Format("{0:F2}", auxNumeric);
                txtValorInformadoTefDiscadoNetworkCard.Text = flagNumeric.Replace(",", ".").Trim();
            }
            else
            {
                txtValorInformadoTefDiscadoNetworkCard.Text = "0.00";
            }
        }

        private void txtParcelamentoTefDiscadoNetworkCard_Validated(object sender, EventArgs e)
        {
            if (txtParcelamentoTefDiscadoNetworkCard.Text != "")
            {
                auxNumeric = Convert.ToDecimal(txtParcelamentoTefDiscadoNetworkCard.Text);//currency
                flagNumeric = String.Format("{0:F2}", auxNumeric);
                txtParcelamentoTefDiscadoNetworkCard.Text = flagNumeric.Replace(",", ".").Trim();
            }
            else
            {
                txtParcelamentoTefDiscadoNetworkCard.Text = "0.00";
            }
        }

        private void btnConfirmaTefDiscadobtnCancelaTefDedicadoNetworkCard_Click(object sender, EventArgs e)
        {
            if (txtValorInformadoTefDiscadoNetworkCard.Text != "0.00" && txtValorInformadoTefDiscadoNetworkCard.Text != "" && Convert.ToDecimal(txtValorInformadoTefDiscadoNetworkCard.Text) > 0)
            {
                lbValorParcelaTefDiscadoNetworkCard.Text = (Convert.ToDecimal(txtValorInformadoTefDiscadoNetworkCard.Text) / Convert.ToInt32(auxNrParcelas)).ToString();
                auxNumeric = Convert.ToDecimal(lbValorParcelaTefDiscadoNetworkCard.Text);//currency
                flagNumeric = String.Format("{0:F2}", auxNumeric);
                lbValorParcelaTefDiscadoNetworkCard.Text = flagNumeric.Replace(",", ".").Trim();
            }
            else
            {
                msgInfo msg = new msgInfo("Caro Cliente - " + "Transação não pode prosseguir, pois o valor apurado e diferente valor informado!"); msg.ShowDialog();
                txtValorInformadoTefDiscadoNetworkCard.Select();
            }
        }

        private void btnFinalizaTefDiscadobtnCancelaTefDedicadoNetworkCard_Click(object sender, EventArgs e)
        {
            if (lbValorParcelaTefDiscadoNetworkCard.Text != "")
            {
                string text = (Convert.ToDecimal(lbValorParcelaTefDiscadoNetworkCard.Text == "" ? "0" : lbValorParcelaTefDiscadoNetworkCard.Text) * Convert.ToInt32(auxNrParcelas)).ToString();
                auxNumeric = Convert.ToDecimal(text);//currency
                flagNumeric = String.Format("{0:F2}", auxNumeric);
                if (txtValorInformadoTefDiscadoNetworkCard.Text == txtValorApuradoTefDiscadoNetworkCard.Text && lbValorParcelaTefDiscadoNetworkCard.Text != "" && lbValorParcelaTefDiscadoNetworkCard.Text != "0.00" && flagNumeric == txtValorApuradoTefDiscadoNetworkCard.Text)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Dispose();
                }
            }
            else
            {
                msgInfo msg = new msgInfo("Caro Cliente - " + "Deve-se confirmar primeiramente antes de finalizar o processo!"); msg.ShowDialog();
                btnConfirmaTefDiscadoNetworkCard.Select();
            }
        }

        private void btnCancelaTefDiscadoNetworkCard_Click(object sender, EventArgs e)
        {
            msgInfo msg = new msgInfo("Cancelado a operação sem finalizar o cartão.");
            if (msg.ShowDialog(this) == DialogResult.Cancel)
            {
                auxIdConectCard = "";
                auxIdContaDeposito = "";
                this.DialogResult = DialogResult.Cancel;
                this.Dispose();
            }
            else
            {
                ptbTefDiscadoNetworkCard.Visible = true;
                btnConfirmaTefDiscadoNetworkCard.Select();
            }
        }

        private void networkCard_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                ProcessTabKey(true);
            }
            else if (e.KeyCode == Keys.F10)
            {

            }
        }
    }
}
