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
    public partial class pesquisaFornecedor : Form
    {
        public pesquisaFornecedor()
        {
            InitializeComponent();
        }
        //Bloco de Variaveis
        private conector_full_variable alwaysVariables = new conector_full_variable();
        private int aux, auxTipoPessoa;
        private int i, j, countField, countRows;
        private dados banco = new dados();
        private string auxCodigoMunicipio, auxCombo, auxGridchave, auxGridNomeFisica, auxGridRazao, auxGridNomeRural, auxGridCnpj, auxGridCpFisica, auxGridIdentidadeFisica, auxGridIeJuridica, auxGridEmpresa, auxGridSexoFisica, auxGridCivilFisica, auxGridNascimentoFisica, auxGridDataAbertura, auxGridFantasia, auxGridUf, auxGridCpf_1Produtor, auxGridIdentidadeProdutor, auxGridNascimentoProduto, auxGridSexo1Produto, auxGridCivil1, auxGridIdTipoFornecedor, auxGridChaveLoja, auxGridIdtipoPessoa, auxGridIdUsuario, auxGridIdAtividade, auxGridObs, auxGridDataEmissao, auxGridDataAlteracao, auxGridChaveEstado, auxGridChaveSexoFisica, auxGridChaveSexoCivil, auxGridIeProdutor, auxGridIdsexoProdutor, auxGridIdCivilProdutor, auxGridStatus;
        void clearObjetos()
        {
            cmbPesquisaFornecedor.SelectedIndex = -1;
            dgvPesquisaFornecedor.Rows.Clear();
            txtInformacaoFornecedor.Clear();
        }
        // Bloco de Properties, encapsulamento variaveis
        public int TipoPessoa
        {
            get
            {
                return auxTipoPessoa;
            }
            set
            {
                auxTipoPessoa = value;
            }
        }
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
        public string GridNomeFisica
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
        public string GridRazao
        {
            get
            {
                return auxGridRazao;
            }
            set
            {
                auxGridRazao = value;
            }
        }
        public string GridNomeRural
        {
            get
            {
                return auxGridNomeRural;
            }
            set
            {
                auxGridNomeRural = value;
            }
        }
        public string GridCnpj
        {
            get
            {
                return auxGridCnpj;
            }
            set
            {
                auxGridCnpj = value;
            }
        }
        public string GridCpFisica
        {
            get
            {
                return auxGridCpFisica;
            }
            set
            {
                auxGridCpFisica = value;
            }
        }
        public string GridIdentidadeFisica
        {
            get
            {
                return auxGridIdentidadeFisica;
            }
            set
            {
                auxGridIdentidadeFisica = value;
            }
        }
        public string GridIeJuridica
        {
            get
            {
                return auxGridIeJuridica;
            }
            set
            {
                auxGridIeJuridica = value;
            }
        }
        public string GridEmpresa
        {
            get
            {
                return auxGridEmpresa;
            }
            set
            {
                auxGridEmpresa = value;
            }
        }
        public string GridSexoFisica
        {
            get
            {
                return auxGridSexoFisica;
            }
            set
            {
                auxGridSexoFisica = value;
            }
        }
        public string GridCivilFisica
        {
            get
            {
                return auxGridCivilFisica;
            }
            set
            {
                auxGridCivilFisica = value;
            }
        }
        public string GridNascimentoFisica
        {
            get
            {
                return auxGridNascimentoFisica;
            }
            set
            {
                auxGridNascimentoFisica = value;
            }
        }
        public string GridDataAbertura
        {
            get
            {
                return auxGridDataAbertura;
            }
            set
            {
                auxGridDataAbertura = value;
            }
        }
        public string GridFantasia
        {
            get
            {
                return auxGridFantasia;
            }
            set
            {
                auxGridFantasia = value;
            }
        }
        public string GridUf
        {
            get
            {
                return auxGridUf;
            }
            set
            {
                auxGridUf = value;
            }
        }

        public string GridCpf_1Produtor
        {
            get
            {
                return auxGridCpf_1Produtor;
            }
            set
            {
                auxGridCpf_1Produtor = value;
            }
        }
        public string GridIdentidadeProdutor
        {
            get
            {
                return auxGridIdentidadeProdutor;
            }
            set
            {
                auxGridIdentidadeProdutor = value;
            }
        }
        public string GridNascimentoProduto
        {
            get
            {
                return auxGridNascimentoProduto;
            }
            set
            {
                auxGridNascimentoProduto = value;
            }
        }
        public string GridSexo1Produto
        {
            get
            {
                return auxGridSexo1Produto;
            }
            set
            {
                auxGridSexo1Produto = value;
            }
        }
        public string GridCivil1
        {
            get
            {
                return auxGridCivil1;
            }
            set
            {
                auxGridCivil1 = value;
            }
        }
        public string GridIdTipoFornecedor
        {
            get
            {
                return auxGridIdTipoFornecedor;
            }
            set
            {
                auxGridIdTipoFornecedor = value;
            }
        }
        public string GridChaveLoja
        {
            get
            {
                return auxGridChaveLoja;
            }
            set
            {
                auxGridChaveLoja = value;
            }
        }
        public string GridIdUsuario
        {
            get
            {
                return auxGridIdUsuario;
            }
            set
            {
                auxGridIdUsuario = value;
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

        public string GridObs
        {
            get
            {
                return auxGridObs;
            }
            set
            {
                auxGridObs = value;
            }
        }
        public string GridDataEmissao
        {
            get
            {
                return auxGridDataEmissao;
            }
            set
            {
                auxGridDataEmissao = value;
            }
        }
        public string GridDataAlteracao
        {
            get
            {
                return auxGridDataAlteracao;
            }
            set
            {
                auxGridDataAlteracao = value;
            }
        }
        public string GridChaveEstado
        {
            get
            {
                return auxGridChaveEstado;
            }
            set
            {
                auxGridChaveEstado = value;
            }
        }
        public string GridChaveSexoFisica
        {
            get
            {
                return auxGridChaveSexoFisica;
            }
            set
            {
                auxGridChaveSexoFisica = value;
            }
        }
        public string GridChaveSexoCivil
        {
            get
            {
                return auxGridChaveSexoCivil;
            }
            set
            {
                auxGridChaveSexoCivil = value;
            }
        }
        public string GridIeProdutor
        {
            get
            {
                return auxGridIeProdutor;
            }
            set
            {
                auxGridIeProdutor = value;
            }
        }
        public string GridIdsexoProdutor
        {
            get
            {
                return auxGridIdsexoProdutor;
            }
            set
            {
                auxGridIdsexoProdutor = value;
            }
        }
        public string GridIdCivilProdutor
        {
            get
            {
                return auxGridIdCivilProdutor;
            }
            set
            {
                auxGridIdCivilProdutor = value;
            }
        }
        public string GridIdCodigoMunicipio
        {
            get
            {
                return auxCodigoMunicipio;
            }
            set
            {
                auxCodigoMunicipio = value;
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
        //################################################Bloco procedimentos de Banco###############################################
        private void conector_find_fornecedor()
        {
            dgvPesquisaFornecedor.Rows.Clear();
            int auxConsistencia = 0;
            countField = 0;
            int confere = 0;
            try
            {
                banco.abreConexao();
                banco.startTransaction("conector_find_cliente");
                banco.addParametro("tipo", Convert.ToString(aux));
                banco.addParametro("find_cliente", txtInformacaoFornecedor.Text == "" ? "0" : txtInformacaoFornecedor.Text);
                banco.addParametro("tipo_cliente", auxTipoPessoa.ToString());
                banco.addParametro("find_atividade", "2");
                banco.procedimentoSet();
            }
            catch (Exception erro)
            { MessageBox.Show(erro.Message, "Caro cliente"); auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = banco.retornaSet().Tables[0].Columns.Count;
                    countRows = banco.retornaSet().Tables[0].DefaultView.Count;
                    dgvPesquisaFornecedor.AllowUserToAddRows = false;
                    confere = banco.retornaSet().Tables.Count;
                    if (confere > 0)
                    {
                        for (i = 0; i < countRows; i++)
                        {
                            dgvPesquisaFornecedor.Rows.Add();
                            for (j = 0; j < countField; j++)
                            {
                                if (j == 4)
                                {
                                    dgvPesquisaFornecedor.Rows[i].Cells[j].Value = Convert.ToBoolean(banco.retornaSet().Tables[0].Rows[i][j]);
                                }
                                else
                                {
                                    dgvPesquisaFornecedor.Rows[i].Cells[j].Value = Convert.ToString(banco.retornaSet().Tables[0].Rows[i][j]);
                                }
                            }
                            //dgvPesquisaSetor.DataSource = banco.retornaSet().Tables[0].DefaultView;
                        }
                    }
                    if (dgvPesquisaFornecedor.RowCount < 1)
                    {
                        dgvPesquisaFornecedor.Rows.Add();
                    }
                }
                dgvPesquisaFornecedor.Select();
                banco.fechaConexao();
            }

        }
        //#############################################################End banco############################################################
        private void pesquisaFornecedor_Load(object sender, EventArgs e)
        {
            auxTipoPessoa = 2;
            aux = 1;
            cmbPesquisaFornecedor.SelectedIndex = 0;
            chkTodosFornecedores.Checked = true;
        }

        private void btnPesquisaPessoa_Click(object sender, EventArgs e)
        {
            conector_find_fornecedor();
        }

        private void dgvPesquisaFornecedor_DoubleClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }

        private void cmbPesquisaFornecedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbPesquisaFornecedor.Text)
            {
                case "Código":
                    dgvPesquisaFornecedor.Rows.Clear();
                    aux = 1;
                    break;
                case "Nome Pessoal Fisica":
                    dgvPesquisaFornecedor.Rows.Clear();
                    aux = 2;
                    break;
                case "Razão Social":
                    dgvPesquisaFornecedor.Rows.Clear();
                    aux = 3;
                    break;
                case "CNPJ":
                    dgvPesquisaFornecedor.Rows.Clear();
                    aux = 4;
                    break;
                case "CPF Pessoa Física":
                    dgvPesquisaFornecedor.Rows.Clear();
                    aux = 5;
                    break;
                case "Nome Produtor Rural":
                    dgvPesquisaFornecedor.Rows.Clear();
                    aux = 6;
                    break;
                case "CPF Produtor Rural":
                    dgvPesquisaFornecedor.Rows.Clear();
                    aux = 7;
                    break;
                default:
                    dgvPesquisaFornecedor.Rows.Clear();
                    aux = 0;
                    break;
            }
            if (dgvPesquisaFornecedor.RowCount < 1)
            {
                dgvPesquisaFornecedor.Rows.Add();
            }
        }

        private void chkTodosClientes_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTodosFornecedores.Checked == true)
            {
                cmbPesquisaFornecedor.SelectedIndex = -1;
                cmbPesquisaFornecedor.Enabled = false;
                dgvPesquisaFornecedor.Rows.Clear();
                aux = 10;

            }
            else { cmbPesquisaFornecedor.Enabled = true; }
        }

        private void dgvPesquisaFornecedor_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvPesquisaFornecedor.RowCount > 0 && dgvPesquisaFornecedor.CurrentRow.Cells[0].Value != null)
            {
                auxGridchave = dgvPesquisaFornecedor.CurrentRow.Cells[0].Value.ToString();
                auxGridRazao = dgvPesquisaFornecedor.CurrentRow.Cells[1].Value.ToString();
                auxGridCnpj = dgvPesquisaFornecedor.CurrentRow.Cells[5].Value.ToString();
            }
        }

        private void rdbPessoaJuridicaFornecedor_CheckedChanged(object sender, EventArgs e)
        {
            auxTipoPessoa = 2;
        }

        private void rbPessoaRuralFornecedor_CheckedChanged(object sender, EventArgs e)
        {
            auxTipoPessoa = 3;
        }

        private void rdbPessoaFisicaFornecedor_CheckedChanged(object sender, EventArgs e)
        {
            auxTipoPessoa = 1;
        }

        private void pesquisaFornecedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
            else if (e.KeyCode == Keys.F5)
            {

            }
            else if (e.KeyCode == Keys.Enter)
            {
                ProcessTabKey(true);
                conector_find_fornecedor();
            }
        }

        private void dgvPesquisaFornecedor_Click(object sender, EventArgs e)
        {
            
if (dgvPesquisaFornecedor.RowCount > 0 && dgvPesquisaFornecedor.CurrentRow.Cells[0].Value != null)
            {

                auxGridchave = dgvPesquisaFornecedor.CurrentRow.Cells[0].Value.ToString();
                auxGridRazao = dgvPesquisaFornecedor.CurrentRow.Cells[1].Value.ToString();
                auxGridCnpj = dgvPesquisaFornecedor.CurrentRow.Cells[5].Value.ToString();
            }
        }

        private void dgvPesquisaFornecedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }

        private void dgvPesquisaFornecedor_KeyUp(object sender, KeyEventArgs e)
        {
            if (dgvPesquisaFornecedor.RowCount > 1 && dgvPesquisaFornecedor.CurrentRow.Cells[0].Value != null)
            {
                auxGridchave = dgvPesquisaFornecedor.CurrentRow.Cells[0].Value.ToString();
                auxGridRazao = dgvPesquisaFornecedor.CurrentRow.Cells[1].Value.ToString();
                auxGridCnpj = dgvPesquisaFornecedor.CurrentRow.Cells[5].Value.ToString();
            }
        }

        private void pesquisaFornecedor_KeyUp(object sender, KeyEventArgs e)
        {

        }
    }
}
