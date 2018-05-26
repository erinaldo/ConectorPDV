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
    public partial class pesquisaCFOP : Form
    {
        public pesquisaCFOP()
        {
            InitializeComponent();
        }
        //Bloco de Variaveis
        private int aux;
        private int i, j, countField, countRows, auxConsistencia; //variavel do loop.
        private dados banco = new dados();
        private string auxGridCodigo, auxGridDescricao;
        private string auxGridStatus, auxGridInput;
        ////////////////End Variaveis####################
        // Bloco de Properties, encapsulamento variaveis
        public string GridInput
        {
            get
            {
                return auxGridInput;
            }
            set
            {
                auxGridInput = value;
            }
        }
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
        //End Proparteis

        // Bloco de rotinas simples "void"
        /// <summary>
        /// DESENVOLVEDOR : FLAVIO
        /// FUNCAO {clearObjetos} sem parametros
        /// ALTERACAO 17/10/13 NADIA old {clearObjetos} new {conector_clear_objetos} 
        /// ENFOQUE : Limpa e retorna aos objetos valores default. 
        /// MODULOS ALTERADOS : rbCodigoFindCFOP_CheckedChanged,rbFindDescricaoCFOP_CheckedChanged,rbFindTodosCFOP_CheckedChanged.
        /// </summary>
        void conetor_clear_objetos()
        {
            dgvPesquisaCFOP.Rows.Clear();
            txtDescricaoCfop.Clear();
        }

        //Bloco Procedimentos de Banco
        /// <summary>
        /// DESENVOLVEDOR : Flavio
        /// FUNCAO {conector_find_CFOP}
        /// DATA : 17/10/13
        /// ENFOQUE : reutilizar metodo para buscar CFOP usando procedure do banco de dados.
        /// </summary>
        public void conector_find_CFOP()
        {
            dgvPesquisaCFOP.Rows.Clear();
            auxConsistencia = 0;

            try
            {
                banco.abreConexao();
                banco.startTransaction("conector_find_CFOP");
                banco.addParametro("parametro", Convert.ToString(aux));
                banco.addParametro("find", txtDescricaoCfop.Text);
                banco.procedimentoSet();
            }
            catch (Exception erro)
            { MessageBox.Show(erro.Message, "Caro Usuário");
            auxConsistencia = 1;
            }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = banco.retornaSet().Tables[0].Columns.Count;
                    countRows = banco.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        dgvPesquisaCFOP.AllowUserToAddRows = false;
                        for (i = 0; i < countRows; i++)
                        {
                            dgvPesquisaCFOP.Rows.Add();
                            for (j = 0; j < countField; j++)
                            {
                                if (j == 3)
                                {
                                    dgvPesquisaCFOP.Rows[i].Cells[j].Value = Convert.ToBoolean(banco.retornaSet().Tables[0].Rows[i][j]);
                                }
                                else
                                {
                                    dgvPesquisaCFOP.Rows[i].Cells[j].Value = Convert.ToString(banco.retornaSet().Tables[0].Rows[i][j]);
                                }
                            }
                        
                          }                    
                        //dgvPesquisaescolaridade.DataSource = banco.retornaSet().Tables[0].DefaultView;
                    }
                    else if (dgvPesquisaCFOP.RowCount < 1)
                    {
                        dgvPesquisaCFOP.Rows.Add();
                    }
                }
                
                banco.fechaConexao();
                dgvPesquisaCFOP.Select();
            }
        }
        /////////////##End Banco ############################      
        private void pesquisaCFOP_Load(object sender, EventArgs e)
        {
            txtDescricaoCfop.Select();
            aux = 3;
        }

        private void rbCodigoFindCFOP_CheckedChanged(object sender, EventArgs e)
        {
            txtDescricaoCfop.ReadOnly = false;
            conetor_clear_objetos();
            if (rbCodigoFindCFOP.Checked == true)
            {
                lbDescricao.Text = "Código";
                aux = 1;
            }
        }

        private void rbFindDescricaoCFOP_CheckedChanged(object sender, EventArgs e)
        {
            txtDescricaoCfop.ReadOnly = false;
            conetor_clear_objetos();
            if (rbFindDescricaoCFOP.Checked == true)
            {
                lbDescricao.Text = "Descrição";
                aux = 2;
            }
        }

        private void rbFindTodosCFOP_CheckedChanged(object sender, EventArgs e)
        {
            txtDescricaoCfop.ReadOnly = true;
            conetor_clear_objetos();
            if (rbFindDescricaoCFOP.Checked == true)
            {
                lbDescricao.Text = "Todos";
                aux = 3;
            }
        }

        private void btnPesquisaAliquota_Click(object sender, EventArgs e)
        {
            conector_find_CFOP();
        }

        private void dgvPesquisaCFOP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvPesquisaCFOP.RowCount > 0 && dgvPesquisaCFOP.CurrentRow.Cells[0].Value != null)
            {
                GridCodigo = dgvPesquisaCFOP.CurrentRow.Cells[0].Value.ToString();
                GridDescricao = dgvPesquisaCFOP.CurrentRow.Cells[1].Value.ToString();
                GridStatus = dgvPesquisaCFOP.CurrentRow.Cells[3].Value.ToString();
                GridInput = dgvPesquisaCFOP.CurrentRow.Cells[2].Value.ToString();    
            }
            
        }

        private void dgvPesquisaCFOP_DoubleClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }

        private void pesquisaCFOP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Dispose();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                ProcessTabKey(true);
                conector_find_CFOP();
            }
        }

        private void pesquisaCFOP_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }

        private void pesquisaCFOP_KeyUp(object sender, KeyEventArgs e)
        {
            if (dgvPesquisaCFOP.RowCount > 0 && dgvPesquisaCFOP.CurrentRow.Cells[0].Value != null)
            {
                GridCodigo = dgvPesquisaCFOP.CurrentRow.Cells[0].Value.ToString();
                GridDescricao = dgvPesquisaCFOP.CurrentRow.Cells[1].Value.ToString();
                GridStatus = dgvPesquisaCFOP.CurrentRow.Cells[3].Value.ToString();
                GridInput = dgvPesquisaCFOP.CurrentRow.Cells[2].Value.ToString();
            }
        }

        private void dgvPesquisaCFOP_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }

        private void dgvPesquisaCFOP_KeyUp(object sender, KeyEventArgs e)
        {
            if (dgvPesquisaCFOP.RowCount > 0 && dgvPesquisaCFOP.CurrentRow.Cells[0].Value != null)
            {
                GridCodigo = dgvPesquisaCFOP.CurrentRow.Cells[0].Value.ToString();
                GridDescricao = dgvPesquisaCFOP.CurrentRow.Cells[1].Value.ToString();
                GridStatus = dgvPesquisaCFOP.CurrentRow.Cells[3].Value.ToString();
                GridInput = dgvPesquisaCFOP.CurrentRow.Cells[2].Value.ToString();
            }
        }
    }
}
