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
    public partial class pequisaMuncipio : Form
    {
        public pequisaMuncipio()
        {
            InitializeComponent();
        }
        public pequisaMuncipio(string table)
            :base()
        {
            newTables = table;
            this.InitializeComponent();
            lbDescricao.Text = "Descricao";
            aux = 1;
        }
        //###################################################Bloco de variaveis encapsuladas############################################################
        private int i, j, countField, countRows; //variavel do loop.
        private dados banco = new dados();
        private string newMunicipio;
        private string newCity;
        private string newTables;
        private string newUf;
        private string newCodigoMunicipio;
        private int aux = 1;
        private int auxConsistencia = 0;
        //############################################### Bloco de Properties, encapsulamento variaveis#################################################
        public string GridCodigoMunicipio
        {
            get
            {
                return newCodigoMunicipio;
            }
            set
            {
                newCodigoMunicipio = value;
            }
        }
        public string GridUf
        {
            get
            {
                return newUf;
            }
            set
            {
                newUf = value;
            }
        }
        public string GridCity
        {
            get
            {
                return newCity;
            }
            set
            {
                newCity = value;
            }
        }
        //#######################################################Bloco de procedimentos de banco##############################################################
        private void conectorPDV_find_municipio()
        {
            dgvPesquisaMunicipio.Rows.Clear();
            auxConsistencia = 0;
            try
            {
                banco.abreConexao();
                banco.startTransaction("conectorPDV_find_municipio");
                banco.addParametro("tipo", Convert.ToString(aux));
                banco.addParametro("find_municipio", txtPesquisaMunicipio.Text == "" ? "0" : txtPesquisaMunicipio.Text);
                banco.addParametro("tabela", newTables);
                banco.procedimentoSet();
            }
            catch (Exception erro) { msgInfo msg = new msgInfo(erro.Message); msg.ShowDialog(); auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = banco.retornaSet().Tables[0].Columns.Count;
                    countRows = banco.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        dgvPesquisaMunicipio.AllowUserToAddRows = false;
                        for (i = 0; i < countRows; i++)
                        {
                            dgvPesquisaMunicipio.Rows.Add();
                            for (j = 0; j < countField; j++)
                            {
                                dgvPesquisaMunicipio.Rows[i].Cells[j].Value = Convert.ToString(banco.retornaSet().Tables[0].Rows[i][j]);
                            }
                            //dgvPesquisaGrupo.DataSource = banco.retornaSet().Tables[0].DefaultView;
                        }
                    }
                    else if (dgvPesquisaMunicipio.RowCount < 1)
                    {
                        dgvPesquisaMunicipio.Rows.Add();
                    }
                }
                banco.fechaConexao();
            }
        }

        private void pequisaMuncipio_Load(object sender, EventArgs e)
        {
            txtPesquisaMunicipio.Text = newMunicipio;
            //conector_find_municipio();
        }

        private void rbFindDescricaoMunicipio_CheckedChanged(object sender, EventArgs e)
        {
            txtPesquisaMunicipio.ReadOnly = false;
            clearObjetos();
            if (rbFindDescricaoMunicipio.Checked == true)
            {
                lbDescricao.Text = "Descrição";
                aux = 1;
            }
        }

        private void rbCodigoFindMunicipio_CheckedChanged(object sender, EventArgs e)
        {
            txtPesquisaMunicipio.ReadOnly = false;
            clearObjetos();
            if (rbCodigoFindMunicipio.Checked == true)
            {
                lbDescricao.Text = "Codigo";
                aux = 2;
            }
        }
        //####################################################### Bloco de rotinas simples "void"###########################################################
        void clearObjetos()
        {
            dgvPesquisaMunicipio.Rows.Clear();
            txtPesquisaMunicipio.Clear();
        }
        //#######################################################END Bloco de rotinas simples "void"#######################################################
        private void rbFindMunicipio_CheckedChanged(object sender, EventArgs e)
        {
            txtPesquisaMunicipio.ReadOnly = true;
            clearObjetos();
            if (rbFindMunicipio.Checked == true)
            {
                lbDescricao.Text = "Todos";
                aux = 10;
            }

        }

        private void dgvPesquisaMunicipio_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvPesquisaMunicipio.RowCount > 0)
            {
                GridCodigoMunicipio = dgvPesquisaMunicipio.CurrentRow.Cells[0].Value.ToString();
                GridCity = dgvPesquisaMunicipio.CurrentRow.Cells[1].Value.ToString();
                GridUf = dgvPesquisaMunicipio.CurrentRow.Cells[2].Value.ToString();
                this.DialogResult = DialogResult.OK;
                this.Dispose();
            }
        }

        private void btnFindMunicipio_Click(object sender, EventArgs e)
        {
            conectorPDV_find_municipio();
        }

        private void txtPesquisaMunicipio_TextChanged(object sender, EventArgs e)
        {

        }

        private void pequisaMuncipio_KeyDown(object sender, KeyEventArgs e)
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
        }
    }
}
