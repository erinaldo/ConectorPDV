using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

/// <summary>
/// DESENVOLVEDOR : Nadia
/// LISTA DE FUNCOES E METODOS
/// S SIMPLES B BANCO
/// B - conector_find_veiculo
/// S - conector_clear_objetos
/// DATA : 10/01/2014
/// ENFOQUE : Declarar todas as funcoes deste formulario.
/// </summary>

namespace conectorPDV001
{
    public partial class pesquisaVeiculo : Form
    {
        public pesquisaVeiculo()
        {
            InitializeComponent();
        }
        #region BLOCO DAS VARIAVEIS
        private int aux;
        private int auxConsistencia;
        private int i, j, countField, countRows; //variavel do loop.
        private dados banco = new dados();
        private string auxGridCodigo, auxGridDescricao, auxGridTipo;
        private string auxGridUf, auxGridDescricaoTipo, auxGridPlaca;
        #endregion

        #region BLOCO DE PROCEDIMENTOS DO BANCO
        /// <summary>
        /// DESENVOLVEDOR : Flavio
        /// FUNCAO {conector_find_veiculo} Sem Parametros
        /// DATA : 10/01/14
        /// ENFOQUE : reutilizar metodo para buscar veiculo usando procedure do banco de dados.
        /// </summary>
        public void conector_find_veiculo()
        {
            auxConsistencia = 0;
            try
            {
                banco.abreConexao();
                banco.startTransaction("conector_find_veiculo");
                banco.addParametro("tipo", Convert.ToString(aux));
                banco.addParametro("find", txtPesquisaVeiculo.Text);
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
                    dgvPesquisaVeiculo.AllowUserToAddRows = false;
                    if (countRows > 0)
                    {
                        for (i = 0; i < countRows; i++)
                        {
                            dgvPesquisaVeiculo.Rows.Add();
                            for (j = 0; j < countField; j++)
                            {
                                dgvPesquisaVeiculo.Rows[i].Cells[j].Value = Convert.ToString(banco.retornaSet().Tables[0].Rows[i][j]);
                            }
                        }
                    }
                }
                dgvPesquisaVeiculo.Select();
                banco.fechaConexao();
            }
        }
#endregion

        #region Bloco de Properties, encapsulamento variaveis
        public string GridCodigo
        {
            get
            {
                return auxGridCodigo;
            }
            set
            {
                auxGridCodigo = value;
            }
        }

        public string GridPlaca
        {
            get
            {
                return auxGridPlaca;
            }
            set
            {
                auxGridPlaca = value;
            }
        }

        public string GridDescricao
        {
            get
            {
                return auxGridDescricao;
            }
            set
            {
                auxGridDescricao = value;
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

        public string GridTipo
        {
            get
            {
                return auxGridTipo;
            }
            set
            {
                auxGridTipo = value;
            }
        }

        public string GridDescricaoTipo
        {
            get
            {
                return auxGridDescricaoTipo;
            }
            set
            {
                auxGridDescricaoTipo = value;
            }
        }
        #endregion

        #region BLOCO DE PROCEDIMENTOS SIMPLES
        //##############################################################Bloco de procedimento simples ################################
        /// DESENVOLVEDOR : Flavio
        /// FUNCAO {clearObjetos} sem parametros 
        /// ALTERACAO 10/01/14 NADIA old {clearObjetos} new {conector_clear_objetos}
        /// ENFOQUE : Reutilizar metodo para limpar icones ou atribuir valores default.
        /// MODULOS ALTERADOS: rbFindVeiculo_CheckedChanged,rbFindDescricaoVeiculo_CheckedChanged,rbCodigoFindVeiculo_CheckedChanged.
        /// </summary>
        void conector_clear_objetos()
        {
            dgvPesquisaVeiculo.Rows.Clear();
            txtPesquisaVeiculo.Clear();
        }

        #endregion

        private void pesquisaVeiculo_Load(object sender, EventArgs e)
        {
            txtPesquisaVeiculo.ReadOnly = true;
            aux = 4;
        }

        private void btnFindVeiculo_Click(object sender, EventArgs e)
        {
            dgvPesquisaVeiculo.Rows.Clear();
            conector_find_veiculo();
        }

        private void rbFindVeiculo_CheckedChanged(object sender, EventArgs e)
        {
            txtPesquisaVeiculo.ReadOnly = true;
            conector_clear_objetos();
            if (rbFindVeiculo.Checked == true)
            {
                lbDescricao.Text = "Todos";
                aux = 4;               
            }
        }

        private void rbFindDescricaoVeiculo_CheckedChanged(object sender, EventArgs e)
        {
            txtPesquisaVeiculo.ReadOnly = false;
            conector_clear_objetos();
            if (rbFindDescricaoVeiculo.Checked == true)
            {
                lbDescricao.Text = "Descrição";
                aux = 6;
                txtPesquisaVeiculo.Select();
            }
        }

        private void rbCodigoFindVeiculo_CheckedChanged(object sender, EventArgs e)
        {
            txtPesquisaVeiculo.ReadOnly = false;
            conector_clear_objetos();
            if (rbCodigoFindVeiculo.Checked == true)
            {
                lbDescricao.Text = "Código";
                aux = 5;
                txtPesquisaVeiculo.Select();
            }
        }

        private void dgvPesquisaVeiculo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvPesquisaVeiculo.RowCount > 0 && dgvPesquisaVeiculo.CurrentRow.Cells[0].Value != null)
            {
                GridPlaca = dgvPesquisaVeiculo.CurrentRow.Cells[2].Value.ToString();
                GridCodigo = dgvPesquisaVeiculo.CurrentRow.Cells[0].Value.ToString();
                GridDescricao = dgvPesquisaVeiculo.CurrentRow.Cells[1].Value.ToString();
                GridTipo = dgvPesquisaVeiculo.CurrentRow.Cells[5].Value.ToString();
                GridDescricaoTipo = dgvPesquisaVeiculo.CurrentRow.Cells[4].Value.ToString();
            }
        }

        private void dgvPesquisaVeiculo_DoubleClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }

        private void dgvPesquisaVeiculo_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }

        private void dgvPesquisaVeiculo_KeyUp(object sender, KeyEventArgs e)
        {
            if (dgvPesquisaVeiculo.RowCount > 0 && dgvPesquisaVeiculo.CurrentRow.Cells[0].Value != null)
            {
                GridPlaca = dgvPesquisaVeiculo.CurrentRow.Cells[2].Value.ToString();
                GridCodigo = dgvPesquisaVeiculo.CurrentRow.Cells[0].Value.ToString();
                GridDescricao = dgvPesquisaVeiculo.CurrentRow.Cells[1].Value.ToString();
                GridTipo = dgvPesquisaVeiculo.CurrentRow.Cells[5].Value.ToString();
                GridDescricaoTipo = dgvPesquisaVeiculo.CurrentRow.Cells[4].Value.ToString();
            }
        }
        // <summary>
        /// DESENVOLVEDOR : FLAVIO
        /// EVENTO KEYDOWN_pesquisaVeiculo 
        /// ALTERACAO 28/02/2014 NADIA : inclusao de novas teclas de comando atraves do codigo ASCI
        /// ENFOQUE : Esc - Fecha a tela
        ///           Enter - Aciona a tecla Enter para executar funcao Tab.
        ///           F2 - Pesquisa a partir do CODIGO
        ///           F3 - Pesquisa a partir da DESCRICAO
        ///           F4 - Pesquisa TODOS
        /// </summary>
        private void pesquisaVeiculo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Dispose();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                ProcessTabKey(true);
                conector_find_veiculo();
            }
            else if (e.KeyCode == Keys.F2)
            {
                txtPesquisaVeiculo.ReadOnly = false;
                conector_clear_objetos();
                rbCodigoFindVeiculo.Checked = true;
                lbDescricao.Text = "Código";
                aux = 5;
                txtPesquisaVeiculo.Select();                
            }
            else if (e.KeyCode == Keys.F3)
            {
                txtPesquisaVeiculo.ReadOnly = false;
                conector_clear_objetos();
                rbFindDescricaoVeiculo.Checked = true;
                lbDescricao.Text = "Descrição";
                aux = 6;
                txtPesquisaVeiculo.Select();                
            }
            else if (e.KeyCode == Keys.F4)
            {
                txtPesquisaVeiculo.ReadOnly = true;
                conector_clear_objetos();
                rbFindVeiculo.Checked = true;
                lbDescricao.Text = "Todos";
                aux = 4;
                btnFindVeiculo.Select();
            }
        }
    }
}
