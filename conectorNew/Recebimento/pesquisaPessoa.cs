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
    public partial class pesquisaPessoa : Form
    {
        public pesquisaPessoa()
        {
            InitializeComponent();
        }

        //################################################################Bloco de Variaveis###########################################################
        private int aux, auxTipoPessoa;
        private int auxConsistencia = 0;
        private int i, j, countField, countRows, auxTipopessoa; //variavel do loop.
        private dados banco = new dados();
        private string auxGridchave, auxGridNomeFisica, auxGridIdAtividade, auxGridStatus;
        void clearObjetos()
        {
            cmbPesquisaCliente.SelectedIndex = -1;
            dgvPesquisaPessoa.Rows.Clear();
            txtInformacao.Clear();
        }
        //################################################################END Bloco de Variaveis###########################################################
        // ######################################################Bloco de Properties, encapsulamento variaveis###########################################
        public string Gridchave
        {
            get
            {
                return auxGridchave;
            }
            set
            {
                auxGridchave = value;
            }
        }
        public string GridIdAtividade
        {
            get
            {
                return auxGridIdAtividade;
            }
            set
            {
                auxGridIdAtividade = value;
            }
        }
        public string GridStatus
        {
            get
            {
                return auxGridStatus;
            }
            set
            {
                auxGridStatus = value;
            }
        }
        public string GridNome
        {
            get
            {
                return auxGridNomeFisica;
            }
            set
            {
                auxGridNomeFisica = value;
            }
        }

        private void pesquisaPessoa_Load(object sender, EventArgs e)
        {
            auxTipoPessoa = 1;
            aux = 1;
            cmbPesquisaCliente.SelectedIndex = 0;
            txtInformacao.Select();

        }
        //Bloco procedimentos de Banco
        private void conectorPDV_find_pessoa()
        {
            dgvPesquisaPessoa.Rows.Clear();
            countRows = 0;
            auxConsistencia = 0;
            try
            {
                banco.abreConexao();
                banco.startTransaction("conectorPDV_find_pessoa");
                banco.addParametro("tipo", Convert.ToString(aux));
                banco.addParametro("find_cliente", txtInformacao.Text == "" ? "0" : txtInformacao.Text);
                banco.addParametro("tipo_cliente", Convert.ToString(auxTipoPessoa));
                banco.addParametro("find_atividade", "1");
                banco.procedimentoSet();
            }
            catch (Exception erro)
            { msgInfo msg = new msgInfo(erro.Message); msg.ShowDialog(); auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = banco.retornaSet().Tables[0].Columns.Count;
                    countRows = banco.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        dgvPesquisaPessoa.AllowUserToAddRows = false;
                        for (i = 0; i < countRows; i++)
                        {
                            dgvPesquisaPessoa.Rows.Add();
                            for (j = 0; j < countField; j++)
                            {
                                if (j == 4)
                                {
                                    dgvPesquisaPessoa.Rows[i].Cells[j].Value = Convert.ToBoolean(banco.retornaSet().Tables[0].Rows[i][j]);
                                }
                                else
                                {
                                    dgvPesquisaPessoa.Rows[i].Cells[j].Value = Convert.ToString(banco.retornaSet().Tables[0].Rows[i][j]);
                                }
                            }
                            //dgvPesquisaSetor.DataSource = banco.retornaSet().Tables[0].DefaultView;
                        }
                    }
                    else if (dgvPesquisaPessoa.RowCount < 1)
                    {
                        dgvPesquisaPessoa.Rows.Add();
                    }
                }
                banco.fechaConexao();
                if (auxConsistencia == 0)
                {
                    dgvPesquisaPessoa.Select();
                }
            }

        }
        private void btnPesquisaPessoa_Click(object sender, EventArgs e)
        {
            conectorPDV_find_pessoa();
        }

        private void dgvPesquisaSetor_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }

        private void chkTodosClientes_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTodosClientes.Checked == true)
            {
                cmbPesquisaCliente.SelectedIndex = -1;
                cmbPesquisaCliente.Enabled = false;
                dgvPesquisaPessoa.Rows.Clear();
                aux = 10;

            }
            else { cmbPesquisaCliente.Enabled = true; }
        }

        private void cmbPesquisaCliente_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void rdbFisica_CheckedChanged(object sender, EventArgs e)
        {
            auxTipoPessoa = 1;

        }

        private void rdbJuridica_CheckedChanged(object sender, EventArgs e)
        {
            auxTipoPessoa = 2;
        }

        private void rdbRural_CheckedChanged(object sender, EventArgs e)
        {
            auxTipoPessoa = 3;
            
        }

        private void cmbPesquisaCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbPesquisaCliente.Text)
            {
                case "Código":
                    dgvPesquisaPessoa.Rows.Clear();
                    rdbFisica.Checked = true; //sugestao
                    aux = 1;
                    break;
                case "Nome Pessoal Fisica":
                    dgvPesquisaPessoa.Rows.Clear();
                    rdbFisica.Checked = true; //sugestao
                    aux = 2;
                    break;
                case "Razão Social":
                    dgvPesquisaPessoa.Rows.Clear();
                    rdbJuridica.Checked = true; //sugestao
                    aux = 3;
                    break;
                case "CNPJ":
                    dgvPesquisaPessoa.Rows.Clear();
                    rdbJuridica.Checked = true; //sugestao
                    aux = 4;
                    break;
                case "CPF Pessoa Física":
                    dgvPesquisaPessoa.Rows.Clear();
                    rdbFisica.Checked = true; //sugestao
                    aux = 5;
                    break;
                case "Nome Produtor Rural":
                    rdbRural.Checked = true; //sugestao
                    dgvPesquisaPessoa.Rows.Clear();
                    aux = 6;
                    break;
                case "CPF Produtor Rural":
                    rdbRural.Checked = true; //sugestao
                    dgvPesquisaPessoa.Rows.Clear();
                    aux = 7;
                    break;
                default:
                    dgvPesquisaPessoa.Rows.Clear();
                    aux = 0;
                    break;
            }
        }

        private void dgvPesquisaPessoa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvPesquisaPessoa.RowCount >= 1 && dgvPesquisaPessoa.CurrentRow.Cells[0].Value != null)
            {
                auxGridchave = dgvPesquisaPessoa.CurrentRow.Cells[0].Value.ToString();
                auxGridStatus = dgvPesquisaPessoa.CurrentRow.Cells[4].Value.ToString();
                auxGridNomeFisica = dgvPesquisaPessoa.CurrentRow.Cells[1].Value.ToString();
                auxGridIdAtividade = dgvPesquisaPessoa.CurrentRow.Cells[11].Value.ToString();
            }
        }

        private void dgvPesquisaPessoa_KeyUp(object sender, KeyEventArgs e)
        {
            if (dgvPesquisaPessoa.RowCount >= 1 && dgvPesquisaPessoa.CurrentRow.Cells[0].Value != null)
            {
                auxGridchave = dgvPesquisaPessoa.CurrentRow.Cells[0].Value.ToString();
                auxGridStatus = dgvPesquisaPessoa.CurrentRow.Cells[4].Value.ToString();
                auxGridNomeFisica = dgvPesquisaPessoa.CurrentRow.Cells[1].Value.ToString();
                auxGridIdAtividade = dgvPesquisaPessoa.CurrentRow.Cells[11].Value.ToString();
            }
        }

        private void dgvPesquisaPessoa_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }

        private void pesquisaPessoa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                ProcessTabKey(true);
                conectorPDV_find_pessoa();
            }
        }
    }
}
