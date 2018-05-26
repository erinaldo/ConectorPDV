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
/// S - conector_clear_objetos
/// B - conector_find_loja
/// DATA : 10/02/14
/// ENFOQUE : Declarar todas as funcoes deste formulario.
/// </summary>

namespace conectorPDV001
{
    public partial class pesquisaLoja : Form
    {
        public pesquisaLoja()
        {
            InitializeComponent();
        }
        // >>>>>>>>>>>>>>>> Bloco de variaveis <<<<<<<<<<<<<<<<<<<<<<<<<

        private int aux = 3;
        private int i, j, countField, countRows; //variavel do loop.
        private string auxGridRamo, auxGridTypeLoja, auxGridaliquotaPis, auxGridAliquotaCofins, auxGridControlaEstoque, auxGridTypeCalculo, auxGridEmpresaTroca, auxGridAliquotaInss, auxGridAliquotaIss, auxGridMatriz, auxGridDeposito, auxGridSerieNota, auxGridNumeroNota, auxGridAtualizaCusto, auxGridLogradouro, auxGridCodigo, auxGridRazao, auxGridFantasia, auxGridCgc, auxGridIe, auxGridIeMunicipio, auxGridType, auxGridCodigoEstado, auxGridLojaUf, auxGridCodigoMunicipio, auxGridStatus, auxGridBairro, auxGridCompl, auxGridMunicipio, auxGridEstado, auxGridNumber, auxGridCEP, auxGridCodEnd, auxGridSeq, auxGridIdBairro, auxGridTypeEnd;
        private dados banco = new dados();
        private int auxConsistencia = 0;

        // <<<<<<<<<<<<<<<<<<  Bloco de Properties, encapsulamento variaveis  >>>>>>>>>>>>>>>>>>>>>>>>
        public string GridAtualizaCusto
        {
            get
            {
                return auxGridAtualizaCusto;
            }
            set
            {
                auxGridAtualizaCusto = value;
            }
        }
        public string GridNumeroNota
        {
            get
            {
                return auxGridNumeroNota;
            }
            set
            {
                auxGridNumeroNota = value;
            }
        }
        public string GridSerieNota
        {
            get
            {
                return auxGridSerieNota;
            }
            set
            {
                auxGridSerieNota = value;
            }
        }
        public string GridDeposito
        {
            get
            {
                return auxGridDeposito;
            }
            set
            {
                auxGridDeposito = value;
            }
        }
        public string GridMatriz
        {
            get
            {
                return auxGridMatriz;
            }
            set
            {
                auxGridMatriz = value;
            }
        }
        public string GridAliquotaIss
        {
            get
            {
                return auxGridAliquotaIss;
            }
            set
            {
                auxGridAliquotaIss = value;
            }
        }
        public string GridAliquotaInss
        {
            get
            {
                return auxGridAliquotaInss;
            }
            set
            {
                auxGridAliquotaInss = value;
            }
        }
        public string GridTypeCalculo
        {
            get
            {
                return auxGridTypeCalculo;
            }
            set
            {
                auxGridTypeCalculo = value;
            }
        }
        public string GridControlaEstoque
        {
            get
            {
                return auxGridControlaEstoque;
            }
            set
            {
                auxGridControlaEstoque = value;
            }
        }
        public string GridAliquotaCofins
        {
            get
            {
                return auxGridAliquotaCofins;
            }
            set
            {
                auxGridAliquotaCofins = value;
            }
        }
        public string GridaliquotaPis
        {
            get
            {
                return auxGridaliquotaPis;
            }
            set
            {
                auxGridaliquotaPis = value;
            }
        }
        public string GridTypeLoja
        {
            get
            {
                return auxGridTypeLoja;
            }
            set
            {
                auxGridTypeLoja = value;
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
        public string GridCgc
        {
            get
            {
                return auxGridCgc;
            }
            set
            {
                auxGridCgc = value;
            }
        }
        public string GridIe
        {
            get
            {
                return auxGridIe;
            }
            set
            {
                auxGridIe = value;
            }
        }

        public string GridIeMunicipio
        {
            get
            {
                return auxGridIeMunicipio;
            }
            set
            {
                auxGridIeMunicipio = value;
            }
        }
        public string GridType
        {
            get
            {
                return auxGridType;
            }
            set
            {
                auxGridType = value;
            }
        }
        public string GridCodigoEstado
        {
            get
            {
                return auxGridCodigoEstado;
            }
            set
            {
                auxGridCodigoEstado = value;
            }
        }

        public string GridLojaUf
        {
            get
            {
                return auxGridLojaUf;
            }
            set
            {
                auxGridLojaUf = value;
            }
        }
        public string GridCodigoMunicipio
        {
            get
            {
                return auxGridCodigoMunicipio;
            }
            set
            {
                auxGridCodigoMunicipio = value;
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
        public string GridBairro
        {
            get
            {
                return auxGridBairro;
            }
            set
            {
                auxGridBairro = value;
            }
        }
        public string GridCompl
        {
            get
            {
                return auxGridCompl;
            }
            set
            {
                auxGridCompl = value;
            }
        }
        public string GridMunicipio
        {
            get
            {
                return auxGridMunicipio;
            }
            set
            {
                auxGridMunicipio = value;
            }
        }
        public string GridEstado
        {
            get
            {
                return auxGridEstado;
            }
            set
            {
                auxGridEstado = value;
            }
        }
        public string GridNumber
        {
            get
            {
                return auxGridNumber;
            }
            set
            {
                auxGridNumber = value;
            }
        }
        public string GridCEP
        {
            get
            {
                return auxGridCEP;
            }
            set
            {
                auxGridCEP = value;
            }
        }
        public string GridCodEnd
        {
            get
            {
                return auxGridCodEnd;
            }
            set
            {
                auxGridCodEnd = value;
            }
        }
        public string GridLogradouro
        {
            get
            {
                return auxGridLogradouro;
            }
            set
            {
                auxGridLogradouro = value;
            }
        }
        public string GridSeq
        {
            get
            {
                return auxGridSeq;
            }
            set
            {
                auxGridSeq = value;
            }
        }
        public string GridIdBairro
        {
            get
            {
                return auxGridIdBairro;
            }
            set
            {
                auxGridIdBairro = value;
            }
        }
            public string GridEmpresaTroca
        {
            get
            {
                return auxGridEmpresaTroca;
            }
            set
            {
                auxGridEmpresaTroca = value;
            }
        }
            public string GridRamo
            {
                get
                {
                    return auxGridRamo;
                }
                set
                {
                    auxGridRamo = value;
                }
            }
        public string GridTipoEndereco
        {
            get
            {
                return auxGridTypeEnd;
            }
            set
            {
                auxGridTypeEnd = value;
            }
        }
        // Bloco de rotinas simples "void"
        /// <summary>
        /// DESENVOLVEDOR : Flavio
        /// FUNCAO {clearObjetos} Sem parametro
        /// ALTERACAO 10/02/2014 NADIA old {clearObjetos} new {conector_clear_objetos}
        /// ENFOQUE : Limpa os objetos ou atribui valores default.
        /// MODULOS ALTERADOS: rbCodigoFindLoja_CheckedChanged,rbFindDescricaoLoja_CheckedChanged,rbFindLoja_CheckedChanged.
        /// </summary>
        void conector_clear_objetos()
        {
            dgvPesquisaLoja.Rows.Clear();
            if (dgvPesquisaLoja.RowCount < 1)
            {
                dgvPesquisaLoja.Rows.Add();
            }
            txtPesquisaLoja.Clear();
        }
        // Bloco Procedimentos de Banco.
        /// <summary>
        /// DESENVOLVEDOR : Flavio
        /// FUNCAO {conector_find_loja} Sem Parametros
        /// DATA : 10/02/2014
        /// ENFOQUE : reutilizar metodo para buscar loja usando procedure do banco de dados.
        /// </summary>
        public void conector_find_loja()
        {
            dgvPesquisaLoja.Rows.Clear();
            countRows = 0;
            auxConsistencia = 0;//dgvPesquisaLoja.Rows.Clear();
            try
            {
                banco.abreConexao();
                banco.startTransaction("conector_find_loja");
                banco.addParametro("tipo", Convert.ToString(aux));
                banco.addParametro("find_loja", txtPesquisaLoja.Text == "" ? "0" : txtPesquisaLoja.Text);
                banco.procedimentoSet();
            }
            catch (Exception erro)
            { MessageBox.Show(erro.Message, "Erro não identificado, entre contato com o revendedor"); auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = banco.retornaSet().Tables[0].Columns.Count;
                    countRows = banco.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        dgvPesquisaLoja.AllowUserToAddRows = false;
                        for (i = 0; i < countRows; i++)
                        {
                            dgvPesquisaLoja.Rows.Add();
                            for (j = 0; j < countField; j++)
                            {
                                if (j == 23)
                                {
                                    dgvPesquisaLoja.Rows[i].Cells[j].Value = Convert.ToBoolean(banco.retornaSet().Tables[0].Rows[i][j]);
                                }
                                else
                                {
                                    dgvPesquisaLoja.Rows[i].Cells[j].Value = Convert.ToString(banco.retornaSet().Tables[0].Rows[i][j]);
                                }
                            }
                            //dgvPesquisaGrupo.DataSource = banco.retornaSet().Tables[0].DefaultView;
                        }
                    }
                    else if (dgvPesquisaLoja.RowCount < 1)
                    {
                        dgvPesquisaLoja.Rows.Add();
                    }
                }
                banco.fechaConexao();
            }
        }
        private void pesquisaLoja_Load(object sender, EventArgs e)
        {
            txtPesquisaLoja.ReadOnly = true;
            aux = 3;
        }

        private void rbCodigoFindLoja_CheckedChanged(object sender, EventArgs e)
        {
            txtPesquisaLoja.ReadOnly = false;
            conector_clear_objetos();
            if (rbCodigoFindLoja.Checked == true)
            {
                lbDescricao.Text = "Codigo";
                aux = 1;
            }
        }

        private void rbFindDescricaoLoja_CheckedChanged(object sender, EventArgs e)
        {
            txtPesquisaLoja.ReadOnly = false;
            conector_clear_objetos();
            if (rbFindDescricaoLoja.Checked == true)
            {
                lbDescricao.Text = "Descrição";
                aux = 2;
            }

        }

        private void rbFindLoja_CheckedChanged(object sender, EventArgs e)
        {
            txtPesquisaLoja.ReadOnly = true;
            conector_clear_objetos();
            if (rbFindLoja.Checked == true)
            {
                lbDescricao.Text = "Todos";
                aux = 3;
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnFindLoja_Click(object sender, EventArgs e)
        {
            conector_find_loja();
            dgvPesquisaLoja.Select();
        }

        private void dgvPesquisaLoja_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvPesquisaLoja_DoubleClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }

        private void dgvPesquisaLoja_KeyUp(object sender, KeyEventArgs e)
        {
            if (dgvPesquisaLoja.RowCount > 0 && dgvPesquisaLoja.CurrentRow.Cells[0].Value != null)
            {
                GridCodigo = dgvPesquisaLoja.CurrentRow.Cells[0].Value.ToString();
                GridRazao = dgvPesquisaLoja.CurrentRow.Cells[1].Value.ToString();
                GridFantasia = dgvPesquisaLoja.CurrentRow.Cells[2].Value.ToString();
                GridCgc = dgvPesquisaLoja.CurrentRow.Cells[3].Value.ToString();
                GridIe = dgvPesquisaLoja.CurrentRow.Cells[4].Value.ToString();
                GridIeMunicipio = dgvPesquisaLoja.CurrentRow.Cells[5].Value.ToString();
                GridType = dgvPesquisaLoja.CurrentRow.Cells[6].Value.ToString();
                GridCodigoEstado = dgvPesquisaLoja.CurrentRow.Cells[7].Value.ToString();
                GridLojaUf = dgvPesquisaLoja.CurrentRow.Cells[8].Value.ToString();
                GridCodigoMunicipio = dgvPesquisaLoja.CurrentRow.Cells[9].Value.ToString();
                GridTypeLoja = dgvPesquisaLoja.CurrentRow.Cells[10].Value.ToString();
                GridaliquotaPis = dgvPesquisaLoja.CurrentRow.Cells[11].Value.ToString();
                GridAliquotaCofins = dgvPesquisaLoja.CurrentRow.Cells[12].Value.ToString();
                GridControlaEstoque = dgvPesquisaLoja.CurrentRow.Cells[13].Value.ToString();
                GridTypeCalculo = dgvPesquisaLoja.CurrentRow.Cells[14].Value.ToString();
                GridEmpresaTroca = dgvPesquisaLoja.CurrentRow.Cells[15].Value.ToString();
                GridAliquotaInss = dgvPesquisaLoja.CurrentRow.Cells[16].Value.ToString();
                GridAliquotaIss = dgvPesquisaLoja.CurrentRow.Cells[17].Value.ToString();
                GridMatriz = dgvPesquisaLoja.CurrentRow.Cells[18].Value.ToString();
                GridDeposito = dgvPesquisaLoja.CurrentRow.Cells[19].Value.ToString();
                GridSerieNota = dgvPesquisaLoja.CurrentRow.Cells[20].Value.ToString();
                GridNumeroNota = dgvPesquisaLoja.CurrentRow.Cells[21].Value.ToString();
                GridAtualizaCusto = dgvPesquisaLoja.CurrentRow.Cells[22].Value.ToString();
                GridStatus = dgvPesquisaLoja.CurrentRow.Cells[23].Value.ToString();
                GridRamo = dgvPesquisaLoja.CurrentRow.Cells[24].Value.ToString();
                GridBairro = dgvPesquisaLoja.CurrentRow.Cells[25].Value.ToString();
                GridCompl = dgvPesquisaLoja.CurrentRow.Cells[26].Value.ToString();
                GridMunicipio = dgvPesquisaLoja.CurrentRow.Cells[27].Value.ToString();
                GridEstado = dgvPesquisaLoja.CurrentRow.Cells[28].Value.ToString();
                GridNumber = dgvPesquisaLoja.CurrentRow.Cells[29].Value.ToString();
                GridCEP = dgvPesquisaLoja.CurrentRow.Cells[30].Value.ToString();
                GridCodEnd = dgvPesquisaLoja.CurrentRow.Cells[31].Value.ToString();
                GridSeq = dgvPesquisaLoja.CurrentRow.Cells[32].Value.ToString();
                GridIdBairro = dgvPesquisaLoja.CurrentRow.Cells[33].Value.ToString();
                GridTipoEndereco = dgvPesquisaLoja.CurrentRow.Cells[34].Value.ToString();
                GridLogradouro = dgvPesquisaLoja.CurrentRow.Cells[35].Value.ToString();
            }
        }

        private void dgvPesquisaLoja_Click(object sender, EventArgs e)
        {
            if (dgvPesquisaLoja.RowCount > 0 && dgvPesquisaLoja.CurrentRow.Cells[0].Value != null)
            {
                GridCodigo = dgvPesquisaLoja.CurrentRow.Cells[0].Value.ToString();
                GridRazao = dgvPesquisaLoja.CurrentRow.Cells[1].Value.ToString();
                GridFantasia = dgvPesquisaLoja.CurrentRow.Cells[2].Value.ToString();
                GridCgc = dgvPesquisaLoja.CurrentRow.Cells[3].Value.ToString();
                GridIe = dgvPesquisaLoja.CurrentRow.Cells[4].Value.ToString();
                GridIeMunicipio = dgvPesquisaLoja.CurrentRow.Cells[5].Value.ToString();
                GridType = dgvPesquisaLoja.CurrentRow.Cells[6].Value.ToString();
                GridCodigoEstado = dgvPesquisaLoja.CurrentRow.Cells[7].Value.ToString();
                GridLojaUf = dgvPesquisaLoja.CurrentRow.Cells[8].Value.ToString();
                GridCodigoMunicipio = dgvPesquisaLoja.CurrentRow.Cells[9].Value.ToString();
                GridTypeLoja = dgvPesquisaLoja.CurrentRow.Cells[10].Value.ToString();
                GridaliquotaPis = dgvPesquisaLoja.CurrentRow.Cells[11].Value.ToString();
                GridAliquotaCofins = dgvPesquisaLoja.CurrentRow.Cells[12].Value.ToString();
                GridControlaEstoque = dgvPesquisaLoja.CurrentRow.Cells[13].Value.ToString();
                GridTypeCalculo = dgvPesquisaLoja.CurrentRow.Cells[14].Value.ToString();
                GridEmpresaTroca = dgvPesquisaLoja.CurrentRow.Cells[15].Value.ToString();
                GridAliquotaInss = dgvPesquisaLoja.CurrentRow.Cells[16].Value.ToString();
                GridAliquotaIss = dgvPesquisaLoja.CurrentRow.Cells[17].Value.ToString();
                GridMatriz = dgvPesquisaLoja.CurrentRow.Cells[18].Value.ToString();
                GridDeposito = dgvPesquisaLoja.CurrentRow.Cells[19].Value.ToString();
                GridSerieNota = dgvPesquisaLoja.CurrentRow.Cells[20].Value.ToString();
                GridNumeroNota = dgvPesquisaLoja.CurrentRow.Cells[21].Value.ToString();
                GridAtualizaCusto = dgvPesquisaLoja.CurrentRow.Cells[22].Value.ToString();
                GridStatus = dgvPesquisaLoja.CurrentRow.Cells[23].Value.ToString();
                GridRamo = dgvPesquisaLoja.CurrentRow.Cells[24].Value.ToString();
                GridBairro = dgvPesquisaLoja.CurrentRow.Cells[25].Value.ToString();
                GridCompl = dgvPesquisaLoja.CurrentRow.Cells[26].Value.ToString();
                GridMunicipio = dgvPesquisaLoja.CurrentRow.Cells[27].Value.ToString();
                GridEstado = dgvPesquisaLoja.CurrentRow.Cells[28].Value.ToString();
                GridNumber = dgvPesquisaLoja.CurrentRow.Cells[29].Value.ToString();
                GridCEP = dgvPesquisaLoja.CurrentRow.Cells[30].Value.ToString();
                GridCodEnd = dgvPesquisaLoja.CurrentRow.Cells[31].Value.ToString();
                GridSeq = dgvPesquisaLoja.CurrentRow.Cells[32].Value.ToString();
                GridIdBairro = dgvPesquisaLoja.CurrentRow.Cells[33].Value.ToString();
                GridTipoEndereco = dgvPesquisaLoja.CurrentRow.Cells[34].Value.ToString();
                GridLogradouro = dgvPesquisaLoja.CurrentRow.Cells[35].Value.ToString();
            }
        }

        private void pesquisaLoja_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
            else if (e.KeyCode == Keys.F1)
            {
            }
            else if (e.KeyCode == Keys.Enter)
            {
                ProcessTabKey(true);
                conector_find_loja();
            }
        }

        private void dgvPesquisaLoja_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }

        private void dgvPesquisaLoja_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }
    }
}
