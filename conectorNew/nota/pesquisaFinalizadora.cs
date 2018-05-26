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
    public partial class pesquisaFinalizadora : Form
    {
        public pesquisaFinalizadora()
        {
            InitializeComponent();
        }
        //Bloco de variaveis encapsuladas
        private int i, j, countField, countRows; //variavel do loop.
        private int auxConsistencia = 0;
        private dados banco = new dados();
        private string auxGridCodigo, auxGridDescricao;

        //Bloco de propertes
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
        //###############END PROPERTES
        //####################end Variaveis#######################
        //Bloco de procedimento de banco
        public void conector_find_finalizadora()
        {
            auxConsistencia = 0;
            dgvPesquisaFinalizadora.Rows.Clear();
            try
            {
                banco.abreConexao();
                banco.startTransaction("conector_find_finalizadora");
                banco.addParametro("tipo", "10");
                banco.addParametro("find", "10");
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
                    if (countRows > 1)
                    {
                        dgvPesquisaFinalizadora.AllowUserToAddRows = false;
                        for (i = 0; i < countRows; i++)
                        {
                            dgvPesquisaFinalizadora.Rows.Add();
                            for (j = 0; j < countField; j++)
                            {
                                if ((j == 0) || (j == 1))
                                {
                                    dgvPesquisaFinalizadora.Rows[i].Cells[j].Value = Convert.ToString(banco.retornaSet().Tables[0].Rows[i][j]);
                                }
                            }
                            //dgvPesquisaGrupo.DataSource = banco.retornaSet().Tables[0].DefaultView;
                        }
                    }
                    else if (dgvPesquisaFinalizadora.RowCount < 1)
                    {
                        dgvPesquisaFinalizadora.Rows.Add();
                    }
                }
                banco.fechaConexao();
            }
        }

        //#####################End Banco#############################

        private void pesquisaFinalizadora_Load(object sender, EventArgs e)
        {
            conector_find_finalizadora();
        }

        private void dgvPesquisaFinalizadora_DoubleClick(object sender, EventArgs e)
        {
            if (dgvPesquisaFinalizadora.RowCount != null && dgvPesquisaFinalizadora.RowCount > 0 && dgvPesquisaFinalizadora.CurrentRow.Cells[0].Value != null)
            {
                GridCodigo = dgvPesquisaFinalizadora.CurrentRow.Cells[0].Value.ToString();
                GridDescricao = dgvPesquisaFinalizadora.CurrentRow.Cells[1].Value.ToString();
                this.DialogResult = DialogResult.OK;
                this.Dispose();
            }

        }

        private void dgvPesquisaFinalizadora_KeyDown(object sender, KeyEventArgs e)
        {
            if (dgvPesquisaFinalizadora.RowCount != null && dgvPesquisaFinalizadora.RowCount > 0 && dgvPesquisaFinalizadora.CurrentRow.Cells[0].Value != null)
            {
                GridCodigo = dgvPesquisaFinalizadora.CurrentRow.Cells[0].Value.ToString();
                GridDescricao = dgvPesquisaFinalizadora.CurrentRow.Cells[1].Value.ToString();
            }
        }

        private void dgvPesquisaFinalizadora_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (dgvPesquisaFinalizadora.RowCount != null && dgvPesquisaFinalizadora.RowCount > 0 && dgvPesquisaFinalizadora.CurrentRow.Cells[0].Value != null)
            {
                this.DialogResult = DialogResult.OK;
                this.Dispose();
            }
        }

        private void pesquisaFinalizadora_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                ProcessTabKey(true);
                conector_find_finalizadora();
            }
        }
    }
}
