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
    public partial class consultaNota : Form
    {
        public consultaNota()
        {
            InitializeComponent();
        }
        public consultaNota(string nota, string serie, string loja, string fornecedor)
        {
            InitializeComponent();
            auxNota = nota;
            auxSerie = serie == "" ? "0" : serie;
            auxIdLoja = loja;
            auxIdFornecedor = fornecedor;
        }
        //######################################Variaveis Encapsulada####################################################
        private string auxNota;
        private string texto;
        private string auxRazao;
        private string auxChave;
        private string auxTotalNota;
        private string auxFornecedor;
        private string auxIdFornecedor;
        private string auxEmissao;
        private string auxSerie, auxIdLoja;
        private int i, j, countField, countRows; //variavel do loop.
        private int auxConsistencia = 0;
        private dados banco = new dados();
        private conector_full_variable alwaysVariables = new conector_full_variable();
        //######################################End Variaveis ###########################################################
        //######################################Procedimento de banco de dados###########################################


        public void conector_find_entrada()
        {
            dgvConsultaNota.Rows.Clear();
            dgvConsultaNota.AllowUserToAddRows = false;

            auxConsistencia = 0;

            texto = " select ";
            texto += " idEntrada as chave, ";
            texto += " nr_nota, ";
            texto += " Serie, ";
            texto += " cfop, ";
            texto += " entrada.idCliente as Fornecedor, ";
            texto += " juridica.razao, ";
            texto += " emissao, ";
            texto += " valorTotalNota ";
            texto += " from ";
            texto += " entrada ";
            texto += " inner join juridica on(entrada.idCliente=juridica.idCliente)";
            texto += " where";
            texto += " nr_nota= " + auxNota + " and serie = " + auxSerie + " and";
            texto += " entrada.idLoja = " + auxIdLoja + " and  entrada.idCliente = " + auxIdFornecedor;
            try
            {
                banco.abreConexao();
                banco.singleTransaction(texto);
                banco.procedimentoSet();
            }
            catch (Exception erro)
            { MessageBox.Show(erro.Message, "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Warning); auxConsistencia = 1; }
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
                            dgvConsultaNota.Rows.Add();
                            for (j = 0; j < countField; j++)
                            {
                                dgvConsultaNota.Rows[i].Cells[j].Value = Convert.ToString(banco.retornaSet().Tables[0].Rows[i][j]);
                            }
                        }
                    }
                    else if(dgvConsultaNota.RowCount < 1)
                    {
                        dgvConsultaNota.Rows.Add();
                    }
                }else if (dgvConsultaNota.RowCount < 1)
                {
                    dgvConsultaNota.Rows.Add();
                }
                banco.fechaConexao();
            }
        }
        //################################################End banco######################################################
        //###########################################Bloco Properteis####################################################
        public string GridChave
        {
            get
            {
                return auxChave;
            }
            set
            {
                auxChave = value;
            }
        }
        public string GridTotal
        {
            get
            {
                return auxTotalNota;
            }
            set
            {
                auxTotalNota = value;
            }
        }
        public string GridNota
        {
            get
            {
                return auxNota;
            }
            set
            {
                auxNota = value;
            }
        }
        public string GridRazao
        {
            get
            {
                return auxRazao;
            }
            set
            {
                auxRazao = value;
            }
        }
        public string GridSerie
        {
            get
            {
                return auxSerie;
            }
            set
            {
                auxSerie = value;
            }
        }
        public string GridEmissao
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
        //###########################################End Bloco###########################################################
        private void consultaNota_Load(object sender, EventArgs e)
        {
            conector_find_entrada();
        }

        private void btnPesquisaSimplificada_Click(object sender, EventArgs e)
        {
            
        }

        private void dgvConsultaNota_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvConsultaNota.RowCount > 0 && dgvConsultaNota.CurrentRow.Cells[0].Value != null)
            {
                auxChave = dgvConsultaNota.CurrentRow.Cells[0].Value.ToString();
                auxNota = dgvConsultaNota.CurrentRow.Cells[1].Value.ToString();
                auxTotalNota = dgvConsultaNota.CurrentRow.Cells[7].Value.ToString();
                auxSerie = dgvConsultaNota.CurrentRow.Cells[2].Value.ToString();
                auxFornecedor = dgvConsultaNota.CurrentRow.Cells[4].Value.ToString();
                auxRazao = dgvConsultaNota.CurrentRow.Cells[5].Value.ToString();
                auxEmissao = dgvConsultaNota.CurrentRow.Cells[6].Value.ToString();
            }
        }

        private void dgvConsultaNota_DoubleClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }

        private void dgvConsultaNota_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void consultaNota_KeyDown(object sender, KeyEventArgs e)
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
            else if (e.Alt && e.KeyCode == Keys.F4)
            {
                this.Dispose();
            }
        }
    }
}
