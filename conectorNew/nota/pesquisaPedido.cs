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
    public partial class pesquisaPedido : Form
    {
        public pesquisaPedido()
        {
            InitializeComponent();
            construtor = 0;
        }
        public pesquisaPedido(string aux)
        {
            InitializeComponent();
            cmbTypePesquisaPedido.Enabled = false;
            chkTypePesquisaPedido.Enabled = false;
            auxIdSituacao = aux;
            construtor = 1;
            switch (aux)
            {
                case "7":
                    cmbTypePesquisaPedido.SelectedIndex = 7;
                    break;
            }
        }
        //##########################################Bloco de Variaveis encapsuladas##############################
        private dados banco = new dados();
        private mainReport relatorio;
        private conector_full_variable alwaysVariables = new conector_full_variable();
        private int aux, i, j, countField, countRows, flagSemaforo, auxConsistencia; //variavel do loop.
        private Decimal auxNumeric;
        private string flagNumeric;
        private int posSeparator;
        private string auxIdParamentroFaturamento;
        private string auxIdLoja;
        private string auxIdDestino;
        private string auxIdPedido;
        private string auxGridDestino;
        private string auxQttyItens;
        private string auxTotalPedido;
        private string auxTotalLiquido;
        private string auxIdSituacao = "-1";
        private string auxEmissao;
        private string auxDesconto;
        private string auxGridDescricaoLoja;
        private string auxIdCliente;
        private pesquisaPessoa pesquisa;
        private int construtor = 0; //0 contrutor normal 1 transferencia
        //##########################################End Variaveis
        //#########################################Bloco Procedimento de banco##################################
        public void conector_find_pedido()
        {
            dgvPesquisaPesquisaPedido.Rows.Clear();
            auxConsistencia = 0;
            try
            {
                banco.abreConexao();
                banco.startTransaction("conector_find_pedido");
                banco.addParametro("find", txtPesquisaPedido.Text == "" ? "0" : txtPesquisaPedido.Text);
                banco.addParametro("tipo", Convert.ToString(aux));
                banco.addParametro("loja", auxIdLoja == "" ? "0" : auxIdLoja);
                banco.addParametro("cliente", auxIdCliente == "" ? "0" : auxIdCliente);
                banco.addParametro("situacao", auxIdSituacao == "" ? "-1" : auxIdSituacao);
                banco.addParametro("operacao", auxIdParamentroFaturamento == "" ? "0" : auxIdParamentroFaturamento);
                banco.addParametro("dataInicial", String.Format("{0:yyyyMMdd}", dtpInicialPesquisaPedido.Value));
                banco.addParametro("dataFinal", String.Format("{0:yyyyMMdd}", dtpFinalPesquisaPedido.Value));
                banco.procedimentoSet();
            }
            catch (Exception erro)
            { MessageBox.Show(erro.Message, "Caro Usuário"); auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = banco.retornaSet().Tables[0].Columns.Count;
                    countRows = banco.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        dgvPesquisaPesquisaPedido.AllowUserToAddRows = false;
                        for (i = 0; i < countRows; i++)
                        {
                            dgvPesquisaPesquisaPedido.Rows.Add();
                            for (j = 0; j < countField; j++)
                            {
                                if (j != 11)
                                {
                                    dgvPesquisaPesquisaPedido.Rows[i].Cells[j].Value = Convert.ToString(banco.retornaSet().Tables[0].Rows[i][j]);
                                }
                                else if(j == 11 && (banco.retornaSet().Tables[0].Rows[i][j].ToString() == "vermelho"))
                                {
                                    dgvPesquisaPesquisaPedido.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (dgvPesquisaPesquisaPedido.RowCount < 1)
                        {
                            dgvPesquisaPesquisaPedido.Rows.Add();   
                        }
                    }
                }
                banco.fechaConexao();
                dgvPesquisaPesquisaPedido.Select();
            }
        }
        public void conector_find_paramentroFaturamento()
        {
            cmbOperacaoPesquisaPedido.Items.Clear();
            countField = 0;
            auxConsistencia = 0;
            try
            {
                banco.abreConexao();
                banco.startTransaction("conector_find_paramentroFaturamento");
                banco.addParametro("tipo", "1");
                banco.addParametro("find", "@");
                banco.procedimentoSet();
            }
            catch (Exception erro)
            { MessageBox.Show(erro.Message, "Erro não identificado, entre contato como revendedor"); auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = banco.retornaSet().Tables[0].Columns.Count;
                    countRows = banco.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        for (i = 0; i < countRows; i++)
                        {
                            cmbOperacaoPesquisaPedido.Items.Add(banco.retornaSet().Tables[0].Rows[i][1]);
                        }
                    }
                }
                banco.fechaConexao();
            }
            cmbOperacaoPesquisaPedido.SelectedIndex = -1;
        }
        public void conector_find_loja()
        {
            cmbLojaPesquisaPedido.Items.Clear();
            countField = 0;
            auxConsistencia = 0;
            try
            {
                banco.abreConexao();
                banco.startTransaction("conector_find_loja");
                banco.addParametro("tipo", "3");
                banco.addParametro("find_loja", "0");
                banco.procedimentoSet();
            }
            catch (Exception erro)
            { MessageBox.Show(erro.Message, "Caro usúario"); auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = banco.retornaSet().Tables[0].Columns.Count;
                    countRows = banco.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        for (i = 0; i < countRows; i++)
                        {
                            cmbLojaPesquisaPedido.Items.Add(banco.retornaSet().Tables[0].Rows[i][1]);
                        }
                    }
                }
                banco.fechaConexao();
            }
        }
        //#########################################End banco########################################
        //#########################################Bloco proparteis ################################

        public string GridDestino
        {
            get
            {
                return auxGridDestino;
            }
            set
            {
                auxGridDestino = value;
            }
        }

        public string IdPedido
        {
            get
            {
                return auxIdPedido;
            }
            set
            {
                auxIdPedido = value;
            }
        }
        public string IdDestino
        {
            get
            {
                return auxIdDestino;
            }
            set
            {
                auxIdDestino = value;
            }
        }
        public string QttyItens
        {
            get
            {
                return auxQttyItens;
            }
            set
            {
                auxQttyItens = value;
            }
        }
        public string TotalPedido
        {
            get
            {
                return auxTotalPedido;
            }
            set
            {
                auxTotalPedido = value;
            }
        }
        public string TotalLiquido
        {
            get
            {
                return auxTotalLiquido;
            }
            set
            {
                auxTotalLiquido = value;
            }
        }
        public string Emissao
        {
            get
            {
                return auxEmissao;
            }
            set
            {
                auxEmissao = value;
            }
        }
        public string Desconto
        {
            get
            {
                return auxDesconto;
            }
            set
            {
                auxDesconto = value;
            }
        }
        public string GridIdLoja
        {
            get
            {
                return auxIdLoja;
            }
            set
            {
                auxIdLoja = value;
            }
        }
        public string GridDescricaoLoja
        {
            get
            {
                return auxGridDescricaoLoja;
            }
            set
            {
                auxGridDescricaoLoja = value;
            }
        }
        //#########################################End proparteis
        //#########################################Bloco de controle obj()#######################################
        void clearObj()
        {
            dgvPesquisaPesquisaPedido.Rows.Clear();
            txtPesquisaPedido.Clear();
            txtDescClientePesquisaPedido.Clear();
            if (dgvPesquisaPesquisaPedido.RowCount < 1)
            {
                dgvPesquisaPesquisaPedido.Rows.Add();
            }
            auxIdCliente = "0";
        }
        void statusObj(Boolean flag)
        {
            chkExpiradasPesquisaPedido.Enabled = flag;
            //chkLojaPesquisaPedido.Enabled = flag;
            //chkTypePesquisaPedido.Enabled = flag;
            //chkOperacaoPesquisaPedido.Enabled = flag;
            cmbOperacaoPesquisaPedido.Enabled = flag;
            cmbTypePesquisaPedido.Enabled = flag;
            cmbLojaPesquisaPedido.Enabled = flag;
            txtPesquisaPedido.Enabled = flag;
        }
        //#########################################End controle obj() ################################
        private void pesquisaPedido_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                ProcessTabKey(true);
//                conector_find_pedido();
            }
        }

        private void pesquisaPedido_Load(object sender, EventArgs e)
        {
            auxIdCliente = "0";
            auxIdParamentroFaturamento = "0";
            auxIdLoja = alwaysVariables.Store;
            conector_find_loja();
            cmbLojaPesquisaPedido.Enabled = true;
            cmbLojaPesquisaPedido.Text = alwaysVariables.RazaoStore;
            conector_find_paramentroFaturamento();
            //statusObj(false);
            aux = 1;
            conector_find_pedido();
            if (dgvPesquisaPesquisaPedido.RowCount > 1)
            {
                dgvPesquisaPesquisaPedido.Select();
            }
            else
            {
                btnPesquisaPedidoVenda.Select();
            }
        }

        private void txtPesquisaTerminal_Validated(object sender, EventArgs e)
        {
            if (txtPesquisaPedido.Text != "")
            {
                auxNumeric = Convert.ToInt32(txtPesquisaPedido.Text);//currency
                flagNumeric = String.Format("{0:F0}", auxNumeric);
                txtPesquisaPedido.Text = flagNumeric.Replace(",", "").Trim();
                if (auxNumeric > 0)
                {
                    rbCodigoPesquisaPedido.Checked = true;
                    txtPesquisaPedido.Text = flagNumeric.Replace(",", "").Trim();
                    conector_find_pedido();
                }
                dgvPesquisaPesquisaPedido.Rows.Clear();
                if (dgvPesquisaPesquisaPedido.RowCount < 1)
                {
                    dgvPesquisaPesquisaPedido.Rows.Add();
                }
            }
        }

        private void txtPesquisaTerminal_KeyPress(object sender, KeyPressEventArgs e)
        {
            posSeparator = txtPesquisaPedido.Text.IndexOf(".");
            if (e.KeyChar == ',') // Se for digitado "," ser  alterado para "."
                e.KeyChar = '.';
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

        private void cmbOperacaoPesquisaPedido_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbOperacaoPesquisaPedido.Text != "")
            {
                auxConsistencia = 0;
                try
                {
                    banco.abreConexao();
                    banco.startTransaction("conector_find_paramentroFaturamento");
                    banco.addParametro("tipo", "3");
                    banco.addParametro("find", cmbOperacaoPesquisaPedido.Text);
                    banco.procedimentoSet();
                }
                catch (Exception erro)
                { MessageBox.Show(erro.Message, "Erro não identificado, entre contato como revendedor"); auxConsistencia = 1; }
                finally
                {
                    if (auxConsistencia == 0)
                    {
                        auxIdParamentroFaturamento = Convert.ToString(banco.retornaSet().Tables[0].Rows[0][0]);
                    }
                    banco.fechaConexao();
                    btnPesquisaPedidoVenda.Select();
                }
            }
        }

        private void cmbLojaTerminal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbLojaPesquisaPedido.Text != "")
            {
                auxConsistencia = 0;
                try
                {
                    banco.abreConexao();
                    banco.startTransaction("conector_find_loja");
                    banco.addParametro("tipo", "2");
                    banco.addParametro("find_loja", cmbLojaPesquisaPedido.Text);
                    banco.procedimentoSet();
                }
                catch (Exception erro)
                { MessageBox.Show(erro.Message, "Erro não identificado, entre contato como revendedor"); auxConsistencia = 1; }
                finally
                {
                    if (auxConsistencia == 0)
                    {
                        GridIdLoja = Convert.ToString(banco.retornaSet().Tables[0].Rows[0][0]);
                        GridDescricaoLoja = Convert.ToString(banco.retornaSet().Tables[0].Rows[0][1]);
                    }
                    banco.fechaConexao();
                    btnPesquisaPedidoVenda.Select();
                }
            }//end if
        }

        private void btnPesquisaCliente_Click(object sender, EventArgs e)
        {
                conector_find_pedido();
        }

        private void rbPesquisaPedidoTotal_CheckedChanged(object sender, EventArgs e)
        {
            clearObj();
            txtPesquisaPedido.ReadOnly = true;
            statusObj(false);
            if (rbPesquisaPedidoTotal.Checked == true)
            {
                lbDescricao.Text = "Todos";
                aux = 1;
                auxIdParamentroFaturamento = "0";
                auxIdLoja = "0";
                //auxIdSituacao = "0";
                cmbOperacaoPesquisaPedido.SelectedIndex = -1;
                cmbLojaPesquisaPedido.SelectedIndex = -1;
                cmbTypePesquisaPedido.SelectedIndex = -1;
                chkLojaPesquisaPedido.Checked = true;
                chkOperacaoPesquisaPedido.Checked = true;
                chkTypePesquisaPedido.Checked = true;
            }
            if (dgvPesquisaPesquisaPedido.RowCount < 1)
            {
                dgvPesquisaPesquisaPedido.Rows.Add();
            }
            btnPesquisaPedidoVenda.Select();
        }

        private void rbCodigoPesquisaPedido_CheckedChanged(object sender, EventArgs e)
        {
            clearObj();
            txtPesquisaPedido.ReadOnly = false;
            statusObj(true);
            if (rbCodigoPesquisaPedido.Checked == true)
            {
                lbDescricao.Text = "N.o Pedido";
                aux = 2;
                chkLojaPesquisaPedido.Checked = true;
                chkOperacaoPesquisaPedido.Checked = true;
                chkTypePesquisaPedido.Checked = true;
                cmbTypePesquisaPedido.Enabled = false;
                cmbOperacaoPesquisaPedido.Enabled = false;
                cmbLojaPesquisaPedido.Enabled = false;
            }
            if (dgvPesquisaPesquisaPedido.RowCount < 1)
            {
                dgvPesquisaPesquisaPedido.Rows.Add();
            }
            txtPesquisaPedido.Select();
        }

        private void chkLojaPesquisaPedido_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLojaPesquisaPedido.Checked == true)
            {
                cmbLojaPesquisaPedido.SelectedIndex = -1;
                cmbLojaPesquisaPedido.Text = "";
                //cmbLojaPesquisaPedido.Enabled = false;
                auxIdLoja = "0";
            }
            else
            {
                cmbLojaPesquisaPedido.SelectedIndex = 0;
                cmbLojaPesquisaPedido.Enabled = true;
            }
            dgvPesquisaPesquisaPedido.Rows.Clear();
            if (dgvPesquisaPesquisaPedido.RowCount < 1)
            {
                dgvPesquisaPesquisaPedido.Rows.Add();
            }
            btnPesquisaPedidoVenda.Select();
        }

        private void chkOperacaoPesquisaPedido_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOperacaoPesquisaPedido.Checked == true)
            {
                cmbOperacaoPesquisaPedido.SelectedIndex = -1;
                cmbOperacaoPesquisaPedido.Text = "";
                //cmbOperacaoPesquisaPedido.Enabled = false;
                auxIdParamentroFaturamento = "0";
            }
            else
            {
                cmbOperacaoPesquisaPedido.SelectedIndex = 0;
                cmbOperacaoPesquisaPedido.Enabled = true;
            }
            dgvPesquisaPesquisaPedido.Rows.Clear();
            if (dgvPesquisaPesquisaPedido.RowCount < 1)
            {
                dgvPesquisaPesquisaPedido.Rows.Add();
            }
            btnPesquisaPedidoVenda.Select();
        }

        private void chkTypePesquisaPedido_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTypePesquisaPedido.Checked == true)
            {
                cmbTypePesquisaPedido.SelectedIndex = -1;
                cmbTypePesquisaPedido.Text = "";
                //cmbTypePesquisaPedido.Enabled = false;
                if (construtor == 0)
                {
                    auxIdSituacao = "-1";
                }
            }
            else
            {
                cmbTypePesquisaPedido.SelectedIndex = 1;
                cmbTypePesquisaPedido.Enabled = true;
            }
            dgvPesquisaPesquisaPedido.Rows.Clear();
            if (dgvPesquisaPesquisaPedido.RowCount < 1)
            {
                dgvPesquisaPesquisaPedido.Rows.Add();
            }
            btnPesquisaPedidoVenda.Select();
        }

        private void dgvPesquisaPesquisaPedido_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvPesquisaPesquisaPedido.RowCount > 0 && dgvPesquisaPesquisaPedido.CurrentRow.Cells[0].Value != null)
            {
                auxIdPedido = dgvPesquisaPesquisaPedido.CurrentRow.Cells[0].Value.ToString();
                auxIdDestino = dgvPesquisaPesquisaPedido.CurrentRow.Cells[2].Value.ToString();
                auxGridDescricaoLoja = dgvPesquisaPesquisaPedido.CurrentRow.Cells[3].Value.ToString();
                auxIdDestino = dgvPesquisaPesquisaPedido.CurrentRow.Cells[4].Value.ToString();
                auxGridDestino = dgvPesquisaPesquisaPedido.CurrentRow.Cells[5].Value.ToString();
                auxQttyItens = dgvPesquisaPesquisaPedido.CurrentRow.Cells[6].Value.ToString();
                auxEmissao = dgvPesquisaPesquisaPedido.CurrentRow.Cells[7].Value.ToString();
                auxDesconto = dgvPesquisaPesquisaPedido.CurrentRow.Cells[10].Value.ToString();
                auxTotalLiquido = dgvPesquisaPesquisaPedido.CurrentRow.Cells[9].Value.ToString();
                auxTotalPedido = dgvPesquisaPesquisaPedido.CurrentRow.Cells[8].Value.ToString();
            }
        }

        private void dgvPesquisaPesquisaPedido_DoubleClick(object sender, EventArgs e)
        {
            if (dgvPesquisaPesquisaPedido.RowCount > 0 && dgvPesquisaPesquisaPedido.CurrentRow.Cells[0].Value != null)
            {
                auxIdPedido = dgvPesquisaPesquisaPedido.CurrentRow.Cells[0].Value.ToString();
            }
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }

        private void btnPesquisaClientePedidoVenda_Click(object sender, EventArgs e)
        {
            
            pesquisa = new pesquisaPessoa();
            if (pesquisa.ShowDialog(this) == DialogResult.OK)
            {
                auxIdCliente = pesquisa.Gridchave;
                txtDescClientePesquisaPedido.Text = pesquisa.GridNome;
                dgvPesquisaPesquisaPedido.Rows.Clear();
                if (dgvPesquisaPesquisaPedido.RowCount < 1)
                {
                    dgvPesquisaPesquisaPedido.Rows.Add();
                }
            }
            btnPesquisaPedidoVenda.Select();
        }

        private void dgvPesquisaPesquisaPedido_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }

        private void dgvPesquisaPesquisaPedido_KeyUp(object sender, KeyEventArgs e)
        {
            if (dgvPesquisaPesquisaPedido.CurrentRow != null && dgvPesquisaPesquisaPedido.RowCount > 0 && dgvPesquisaPesquisaPedido.CurrentRow.Cells[0].Value != null)
            {
                auxIdPedido = dgvPesquisaPesquisaPedido.CurrentRow.Cells[0].Value.ToString();
                auxIdDestino = dgvPesquisaPesquisaPedido.CurrentRow.Cells[2].Value.ToString();
                auxGridDescricaoLoja = dgvPesquisaPesquisaPedido.CurrentRow.Cells[3].Value.ToString();
                auxIdDestino = dgvPesquisaPesquisaPedido.CurrentRow.Cells[4].Value.ToString();
                auxGridDestino = dgvPesquisaPesquisaPedido.CurrentRow.Cells[5].Value.ToString();
                auxQttyItens = dgvPesquisaPesquisaPedido.CurrentRow.Cells[6].Value.ToString();
                auxEmissao = dgvPesquisaPesquisaPedido.CurrentRow.Cells[7].Value.ToString();
                auxDesconto = dgvPesquisaPesquisaPedido.CurrentRow.Cells[10].Value.ToString();
                auxTotalLiquido = dgvPesquisaPesquisaPedido.CurrentRow.Cells[9].Value.ToString();
                auxTotalPedido = dgvPesquisaPesquisaPedido.CurrentRow.Cells[8].Value.ToString();
            }
        }

        private void cmbTypePesquisaPedido_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTypePesquisaPedido.Text != "")
            {
                auxIdSituacao = (cmbTypePesquisaPedido.Text.Substring(0, 1));
            }
            else
            {
                if (construtor == 0)
                {
                    auxIdSituacao = "-1";   
                }
            }
            btnPesquisaPedidoVenda.Select();
        }

        private void dtpInicialPesquisaPedido_ValueChanged(object sender, EventArgs e)
        {
            btnPesquisaPedidoVenda.Select();
        }

        private void dtpFinalPesquisaPedido_ValueChanged(object sender, EventArgs e)
        {
            dgvPesquisaPesquisaPedido.Rows.Clear();
            if (dgvPesquisaPesquisaPedido.RowCount < 1)
            {
                dgvPesquisaPesquisaPedido.Rows.Add();
            }
            btnPesquisaPedidoVenda.Select();
        }

        private void reimpressaoPesquisaPedido_Click(object sender, EventArgs e)
        {
            if (chkLojaPesquisaPedido.Checked == false)
            {
                if (cmbLojaPesquisaPedido.Text != "")
                {
                    if (auxIdPedido != null)
                    {
                        if (auxIdLoja != null)
                        {
                            relatorio = new mainReport();
                            relatorio.pedidoReserva(auxIdLoja, auxIdPedido);
                        }
                        else
                        {
                            MessageBox.Show("Loja inválida ou não informada. ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dgvPesquisaPesquisaPedido.Select();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Reserva inválida ou não informada. ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dgvPesquisaPesquisaPedido.Select();
                    }
                }
                else
                {
                    MessageBox.Show("Pelo ou menos uma loja deve estar selecionada. ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbLojaPesquisaPedido.Select();
                }
            }
            else
            {
                MessageBox.Show("Check todas as lojas não pode esta marcado, verifique. ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                chkLojaPesquisaPedido.Select();
            }
        }

        private void chkExpiradasPesquisaPedido_CheckedChanged(object sender, EventArgs e)
        {
            dgvPesquisaPesquisaPedido.Rows.Clear();
            if (dgvPesquisaPesquisaPedido.RowCount < 1)
            {
                dgvPesquisaPesquisaPedido.Rows.Add();
            }
        }
    }
}
