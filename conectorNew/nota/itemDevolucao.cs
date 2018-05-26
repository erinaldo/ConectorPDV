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
    public partial class itemDevolucao : Form
    {
        public itemDevolucao()
        {
            InitializeComponent();
        }
        public itemDevolucao(string loja, string chave)
        {
            InitializeComponent();
            auxIdLoja = loja;
            auxIdEntrada = chave;
            auxIdEntradaItem = "0";
            countRows = 0;
            countField = 0;
        }
        //###############################################################Variaveis Encapsuladas###################################################################
        private dados banco = new dados();
        private int flagSemaforoFinanceiro, flagSemaforoImp, i, j, countField, countRows, flagSemaforo, auxConsistencia; //variavel do loop.
        private string auxIdLoja;
        private int ataque = 0;
        private string auxIdEntrada;
        private string auxIdEntradaItem = "0";
        private DataSet set;
        //@##############################################################End Variaveis########################################################################
        /// <summary>
        /// Procedimento de banco de dados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        protected void conector_find_itemEntrada(string tipo)
        {
            ataque = 0;
            dgvItensNotaFiscalDevItem.Rows.Clear();
            ataque = 1;
            countField = 0;
            try
            {
                banco.abreConexao();
                banco.startTransaction("conector_find_itemEntrada");
                banco.addParametro("tipo", tipo);
                banco.addParametro("idLoja", auxIdLoja);
                banco.addParametro("find", auxIdEntrada);
                //banco.addParametro("seq", tipo == "4" ? "0" : auxIdEntradaItem);
                banco.addParametro("seq", auxIdEntradaItem);
                banco.procedimentoSet();
            }
            catch (Exception erro)
            { MessageBox.Show(erro.Message, "Erro não identificado, entre contato como revendedor"); }
            finally
            {

                countField = banco.retornaSet().Tables[0].Columns.Count;
                countRows = banco.retornaSet().Tables[0].DefaultView.Count;
                dgvItensNotaFiscalDevItem.AllowUserToAddRows = false;
                if (tipo == "3")
                {
                    for (i = 0; i < countRows; i++)
                    {
                        dgvItensNotaFiscalDevItem.Rows.Add();
                        for (j = 0; j < countField; j++)
                        {
                            dgvItensNotaFiscalDevItem.Rows[i].Cells[j].Value = Convert.ToString(banco.retornaSet().Tables[0].Rows[i][j]);
                        }

                    }
                }
                else
                {
                    if (countRows > 0)
                    {
                        set = banco.retornaSet();
                    }
                }
                banco.fechaConexao();
            }
        }
        //#############################################End Banco de dados#######################################################################
        // Bloco de Properties, encapsulamento variaveis

        public int countLinhas
        {
            get
            {
                return countRows;
            }
            set
            {
                countRows = value;
            }
        }
        public DataSet setDados
        {
            get
            {
                return set;
            }
            set
            {
                set = value;
            }
        }
        public string chaveItem
        {
            get
            {
                return auxIdEntradaItem;
            }
            set
            {
                auxIdEntradaItem = value;
            }
        }
        //##########################################################End Properties###############################################################
        private void itemDevolucao_Load(object sender, EventArgs e)
        {
            ataque = 0;
            conector_find_itemEntrada("3");
            ataque = 1;
        }

        private void dgvItensNotaFiscalDevItem_DoubleClick(object sender, EventArgs e)
        {
            if (countRows != null && countRows > 0 )
            {
                if (dgvItensNotaFiscalDevItem.RowCount > 0)
                {
                    auxIdEntradaItem = dgvItensNotaFiscalDevItem.CurrentRow.Cells[0].Value.ToString();
                    conector_find_itemEntrada("4");
                    this.DialogResult = DialogResult.Retry;
                    this.Dispose();
                }   
            }
        }

        private void btnSairOuCancelarItemEntradaNota_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Dispose();
        }

        private void btnOkItemEntradaNota_Click(object sender, EventArgs e)
        {
            if (countRows != null && countRows > 0 )
            {
                if (dgvItensNotaFiscalDevItem.RowCount > 0)
                {
                    auxIdEntradaItem = "0"; // traz todos
                    conector_find_itemEntrada("4");
                    this.DialogResult = DialogResult.Retry;
                    this.Dispose();
                }
            }
            else
            {
                this.DialogResult = DialogResult.Abort;
                this.Dispose();
            }
        }

        private void dgvItensNotaFiscalDevItem_SelectionChanged(object sender, EventArgs e)
        {
            if (countRows > 0)
            {
                if (dgvItensNotaFiscalDevItem.RowCount > 0 && ataque == 1 && dgvItensNotaFiscalDevItem.CurrentRow.Cells[0].Value !=  null)
                {
                    auxIdEntradaItem = dgvItensNotaFiscalDevItem.CurrentRow.Cells[0].Value.ToString();
                }
            }
        }

        private void dgvItensNotaFiscalDevItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (countRows > 0)
            {
                if (dgvItensNotaFiscalDevItem.RowCount > 0)
                {
                    auxIdEntradaItem = dgvItensNotaFiscalDevItem.CurrentRow.Cells[0].Value.ToString();
                }
            }
        }

        private void dgvItensNotaFiscalDevItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (countRows > 0)
            {
                if (dgvItensNotaFiscalDevItem.RowCount > 0)
                {
                    auxIdEntradaItem = dgvItensNotaFiscalDevItem.CurrentRow.Cells[0].Value.ToString();
                }
            }
        }
    }
}
