using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

/// <sumary>
/// DESENVOLVEDOR : Nadia
/// LISTA DE FUNÇÕES E METODOS
/// S SIMPLES B BANCO
/// B - conector_find_setor
/// B - conector_find_grupo
/// B - conector_find_categoria
/// B - conector_find_loja
/// B - conector_find_produto
/// S - conector_clear_obj

namespace conectorPDV001
{
    public partial class pesquisaProduto : Form
    {
        public pesquisaProduto()
        {
            InitializeComponent();
        }

        #region BLOCO DAS VARIAVEIS ENCAPSULDAS
        private string auxIdSetor = "0";
        private string auxIdGrupo = "0";
        private string auxIdCategoria = "0";
        private string newDescSetorProduto;
        private string newDescGrupoProduto;
        private string newDescCategoriaProduto;
        private string auxIdLoja;
        private string auxLoja;
        private string auxProduto;
        private string auxBarra;
        private string auxEstoque;
        private string auxIdFornecedor = "0";
        private string newDescFornecedor = "";
        private string auxDescProduto;
        private string auxPriceVenda;
        private dados banco = new dados();
        private pesquisaFornecedor pesquisa;
        private conector_full_variable alwaysVariables = new conector_full_variable();
        private int aux, i, auxConsistencia, j, countField, countRows; //variavel do loop.
        #endregion

        #region BLOCO DAS PROPERTIES
        public string Barra
        {
            get
            {
                return auxBarra;
            }
            set
            {
                auxBarra = value;
            }
        }
        public string PriceVenda
        {
            get
            {
                return auxPriceVenda;
            }
            set
            {
                auxPriceVenda = value;
            }
        }
        public string DescProduto
        {
            get
            {
                return auxDescProduto;
            }
            set
            {
                auxDescProduto = value;
            }
        }
        public string fornecedor
        {
            get
            {
                return auxIdFornecedor;
            }
            set
            {
                auxIdFornecedor = value;
            }
        }
        public string Produto
        {
            get
            {
                return auxProduto;
            }
            set
            {
                auxProduto = value;
            }
        }
        public string grupo
        {
            get
            {
                return auxIdGrupo;
            }
            set
            {
                auxIdGrupo = value;
            }
        }
        public string setor
        {
            get
            {
                return auxIdSetor;
            }
            set
            {
                auxIdSetor = value;
            }
        }
        public string categoria
        {
            get
            {
                return auxIdCategoria;
            }
            set
            {
                auxIdCategoria = value;
            }
        }
        public string loja
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
        public string descricaloja
        {
            get
            {
                return auxLoja;
            }
            set
            {
                auxLoja = value;
            }
        }
        public string estoque
        {
            get
            {
                return auxEstoque;
            }
            set
            {
                auxEstoque = value;
            }
        }
       
        #endregion

        #region BLOCO DE PROCEDIMENTOS DO BANCO
        /// <summary>
        /// DESENVOLVEDOR : Flavio
        /// FUNCAO : {conector_find_setor} sem parametro
        /// DATA : 11/10/13
        /// ENFOQUE : Reutilizacao de codigo para buscar setor com utilizacao de procedure.
        /// ALTERACAO : NADIA 15/10/13 - Adicao da variavel de consistencia. 
        /// </summary>
        public void conector_find_setor()
        {
            cmbPesquisaProdutoSetor.Items.Clear();
            countField = 0;
            auxConsistencia = 0;
            try
            {
                banco.abreConexao();
                banco.startTransaction("conector_find_setor");
                banco.addParametro("tipo", "3");
                banco.addParametro("find", "0");
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
                        for (i = 0; i < countRows; i++)
                        {
                            cmbPesquisaProdutoSetor.Items.Add(banco.retornaSet().Tables[0].Rows[i][1]);
                        }                         
                    }                                       
                }
                
                banco.fechaConexao();
            }
        }
        /// <summary>
        /// DESENVOLVEDOR : Flavio
        /// FUNCAO : {conector_find_grupo} sem parametro
        /// DATA : 11/10/13
        /// ENFOQUE : Reutilizacao de codigo para buscar grupo com utilizacao de procedure.
        /// ALTERACAO : NADIA 15/10/13 - Adicao da variavel de consistencia. 
        /// </summary>
        public void conector_find_grupo()
        {
            cmbPesquisaProdutoGrupo.Items.Clear();
            cmbPesquisaProdutoCategoria.Items.Clear();
            txtIdPesquisaProdutoGrupo.Clear();
            txtIdPesquisaProdutoCategoria.Clear();
            countField = 0;
            countRows = 0;
            auxConsistencia = 0;
            try
            {
                banco.abreConexao();
                banco.startTransaction("conector_find_grupo");
                banco.addParametro("tipo", "10");
                banco.addParametro("find", auxIdSetor);
                banco.procedimentoSet();
            }
            catch (Exception erro)
            { MessageBox.Show(erro.Message, "Erro não identificado, entre contato com o revendedor");
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
                        for (i = 0; i < countRows; i++)
                        {
                            cmbPesquisaProdutoGrupo.Items.Add(banco.retornaSet().Tables[0].Rows[i][1]);
                        }                                            
                    }                    
                }                
                banco.fechaConexao();
            }
        }
        /// <summary>
        /// DESENVOLVEDOR : Flavio
        /// FUNCAO : {conector_find_categoria} sem parametro
        /// DATA : 11/10/13
        /// ENFOQUE : Reutilizacao de codigo para buscar categoria com utilizacao de procedure.
        /// ALTERACAO : NADIA 15/10/13 - Adicao da variavel de consistencia. 
        /// </summary>
        public void conector_find_categoria()
        {
            cmbPesquisaProdutoCategoria.Items.Clear();
            txtIdPesquisaProdutoCategoria.Clear();
            countField = 0;
            countRows = 0;
            auxConsistencia = 0;
            try
            {
                banco.abreConexao();
                banco.startTransaction("conector_find_categoria");
                banco.addParametro("tipo", "10");
                banco.addParametro("find", "0");
                banco.addParametro("find_setor", auxIdSetor);
                banco.addParametro("find_grupo", auxIdGrupo);
                banco.procedimentoSet();
            }
            catch (Exception erro)
            { MessageBox.Show(erro.Message, "Erro não identificado, entre contato com o revendedor");
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
                        for (i = 0; i < countRows; i++)
                        {
                            cmbPesquisaProdutoCategoria.Items.Add(banco.retornaSet().Tables[0].Rows[i][1]);
                        }                                            
                    }                    
                }                
                banco.fechaConexao();
            }
        }
        /// <summary>
        /// DESENVOLVEDOR : Flavio
        /// FUNCAO : {conector_find_loja} sem parametro
        /// DATA : 11/10/13
        /// ENFOQUE : Reutilizacao de codigo para procurar loja com utilizacao de procedure
        /// ALTERACAO : NADIA 15/10/13 - Adicao da variavel de consistencia. 
        /// </summary>
        public void conector_find_loja()
        {
            cmbPesquisaProdutoLoja.Items.Clear();
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
            { MessageBox.Show(erro.Message, "Erro não identificado, entre contato com o revendedor");
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
                        for (i = 0; i < countRows; i++)
                        {
                            cmbPesquisaProdutoLoja.Items.Add(banco.retornaSet().Tables[0].Rows[i][1]);
                        }                                           
                    }                    
                }                
                banco.fechaConexao();
            }
        }
        /// <summary>
        /// DESENVOLVEDOR : Flavio
        /// FUNCAO : {conector_find_produto} sem parametros
        /// DATA : 09/10/13
        /// ENFOQUE : Reutilizacao de codigo para procurar produto com utilizacao de procedure.
        /// </summary>
        private void conector_find_produto()
        {
            dgvPesquisaProduto.Rows.Clear();
            countField = 0;
            auxConsistencia = 0;
            try
            {
                banco.abreConexao();
                banco.startTransaction("conector_find_produto");
                banco.addParametro("tipo", Convert.ToString(aux));
                banco.addParametro("find", txtPesquisaProduto.Text);
                banco.addParametro("findIdLoja", auxIdLoja);
                banco.addParametro("findIdSetor", auxIdSetor);
                banco.addParametro("findIdGrupo", auxIdGrupo);
                banco.addParametro("findIdCategoria", auxIdCategoria);
                banco.addParametro("findIdFornecedor", auxIdFornecedor);
                banco.addParametro("findNumeroNota", "0");
                banco.addParametro("findDataInicial", "19000101");
                banco.addParametro("findDataFinal", "19000101");
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
                        dgvPesquisaProduto.AllowUserToAddRows = false;
                        for (i = 0; i < countRows; i++)
                        {
                            dgvPesquisaProduto.Rows.Add();
                            for (j = 0; j < countField; j++)
                            {
                                dgvPesquisaProduto.Rows[i].Cells[j].Value = Convert.ToString(banco.retornaSet().Tables[0].Rows[i][j]);
                            }
                            //dgvPesquisaSetor.DataSource = banco.retornaSet().Tables[0].DefaultView;
                        }
                    }
                    else
                    {
                        if (dgvPesquisaProduto.RowCount < 1)
                        {
                            dgvPesquisaProduto.Rows.Add();
                        }
                    }
                }
                else
                {
                    if (dgvPesquisaProduto.RowCount < 1)
                    {
                        dgvPesquisaProduto.Rows.Add();
                    }
                }
                dgvPesquisaProduto.Select();
                banco.fechaConexao();
            }
        }
        /// <summary>
        /// DESENVOLVEDOR : Flavio
        /// FUNCAO {conector_find_fornecedor} Com 3 Parametros (string)
        /// DATA : 14/03/2014
        /// ENFOQUE : reutilizar metodo para buscar cliente usando procedure do banco de dados.
        /// </summary>
        public void conector_find_fornecedor(string id) //Busca se situa e carrega as configurações
        {
            try
            {
                banco.abreConexao();
                banco.startTransaction("conector_find_cliente");
                banco.addParametro("tipo", "1");
                banco.addParametro("find_cliente", id);
                banco.addParametro("tipo_cliente", "2");
                banco.addParametro("find_atividade", "2");
                banco.procedimentoRead();
                if (banco.retornaRead().Read() == true)
                {
                    auxIdFornecedor = banco.retornaRead().GetString(0);
                    txtDescPesquisaProdutoFornecedor.Text = banco.retornaRead().GetString(1);
                }
                else
                {
                    MessageBox.Show("Fornecedor inválido.", "Caro Usuário", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception erro)
            { MessageBox.Show(erro.Message, "Caro cliente"); }
            finally
            {
                banco.fechaConexao();

            }
        }

        #endregion

        #region BLOCO DE PROCEDIMENTOS SIMPLES
        /// <summary>
        /// DESENVOLVEDOR : Flavio
        /// FUNCAO {clearObj} sem parametro
        /// ALTERACAO 10/10/13 NADIA old {clearObj} new {conector_clear_obj}
        /// ENFOQUE : Reutilizar metodo para limpar objetos. 
        /// MODULOS ALTERADOS: btnLimpa_Click.
        /// </summary>
        void conector_clear_obj()
        {
            cmbPesquisaProdutoSetor.SelectedIndex = -1;
            cmbPesquisaProdutoGrupo.SelectedIndex = -1;
            cmbPesquisaProdutoCategoria.SelectedIndex = -1;
            auxIdSetor = "0";
            auxIdGrupo = "0";
            auxIdCategoria = "0";
            auxIdFornecedor = "0";
            txtIdPesquisaProdutoCategoria.Clear();
            txtIdPesquisaProdutoFornecedor.Clear();
            txtIdPesquisaProdutoGrupo.Clear();
            txtIdPesquisaProdutoSetor.Clear();
            txtDescPesquisaProdutoFornecedor.Clear();
            txtPesquisaProduto.Clear();
            dgvPesquisaProduto.Rows.Clear();
            if (dgvPesquisaProduto.RowCount < 1)
            {
                dgvPesquisaProduto.Rows.Add();
            }
        }
        #endregion

        private void pesquisaProduto_Load(object sender, EventArgs e)
        {
            conector_find_loja();
            auxIdLoja = alwaysVariables.Store;
            cmbPesquisaProdutoLoja.Text = alwaysVariables.RazaoStore;
            conector_find_setor();
            cmbPesquisaProduto.SelectedIndex = 1;
            aux = 1;
        }

        private void cmbPesquisaProdutoSetor_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvPesquisaProduto.Rows.Clear();
            if (cmbPesquisaProdutoSetor.Text != "")
            {
                countField = 0;
                auxConsistencia = 0;
                try
                {
                    banco.abreConexao();
                    banco.startTransaction("conector_find_setor");
                    banco.addParametro("tipo", "1");
                    banco.addParametro("find", cmbPesquisaProdutoSetor.Text);
                    banco.procedimentoSet();
                }
                catch (Exception erro)
                { MessageBox.Show(erro.Message, "Mensagem do Sistema"); auxConsistencia = 1; }
                finally
                {
                    if (auxConsistencia == 0)
                    {
                        auxIdSetor = Convert.ToString(banco.retornaSet().Tables[0].Rows[0][0]);                       
                    }                    
                    banco.fechaConexao();
                        if (dgvPesquisaProduto.RowCount < 1)
                        {
                            dgvPesquisaProduto.Rows.Add();
                        }
                    
                }
            }//end if
            if ((cmbPesquisaProdutoSetor.Text != "") && cmbPesquisaProdutoSetor.SelectedIndex > -1)
            {
                conector_find_grupo();
                txtIdPesquisaProdutoSetor.Text = auxIdSetor;
            }
            if (dgvPesquisaProduto.RowCount < 1)
            {
                dgvPesquisaProduto.Rows.Add();
            }
        }

        private void cmbPesquisaProdutoGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvPesquisaProduto.Rows.Clear();
            if (cmbPesquisaProdutoGrupo.Text != "")
            {
                auxConsistencia = 0;
                try
                {
                    banco.abreConexao();
                    banco.startTransaction("conector_find_grupo");
                    banco.addParametro("tipo", "1");
                    banco.addParametro("find", cmbPesquisaProdutoGrupo.Text);
                    banco.procedimentoSet();
                }
                catch (Exception erro)
                { MessageBox.Show(erro.Message, "Erro não identificado, entre contato com o revendedor");
                auxConsistencia = 1;
                }
                finally
                {
                    if (auxConsistencia == 0)
                    {
                        auxIdGrupo = Convert.ToString(banco.retornaSet().Tables[0].Rows[0][0]);                        
                    }
                    banco.fechaConexao();
                }
            }//end if
            if (((cmbPesquisaProdutoGrupo.Text != "") && cmbPesquisaProdutoGrupo.SelectedIndex > -1) && ((cmbPesquisaProdutoSetor.Text != "") && cmbPesquisaProdutoSetor.SelectedIndex > -1))
            {
                conector_find_categoria();
                txtIdPesquisaProdutoGrupo.Text = auxIdGrupo;
            }
            if (dgvPesquisaProduto.RowCount < 1)
            {
                dgvPesquisaProduto.Rows.Add();
            }
        }
       
        private void cmbPesquisaProdutoCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvPesquisaProduto.Rows.Clear();
            if (cmbPesquisaProdutoCategoria.Text != "")
            {
                auxConsistencia = 0;
                try
                {
                    banco.abreConexao();
                    banco.startTransaction("conector_find_categoria");
                    banco.addParametro("tipo", "1");
                    banco.addParametro("find", cmbPesquisaProdutoCategoria.Text);
                    banco.addParametro("find_setor", auxIdSetor);
                    banco.addParametro("find_grupo", auxIdGrupo);

                    banco.procedimentoSet();
                }
                catch (Exception erro)
                { MessageBox.Show(erro.Message, "Erro não identificado, entre contato com o revendedor"); auxConsistencia = 1; }
                finally
                {
                    if (auxConsistencia == 0)
                    {
                        auxIdCategoria = Convert.ToString(banco.retornaSet().Tables[0].Rows[0][0]);                        
                    }                    
                    banco.fechaConexao();
                        if (dgvPesquisaProduto.RowCount < 1)
                        {
                            dgvPesquisaProduto.Rows.Add();
                        }
                }
            }//end if
            txtIdPesquisaProdutoCategoria.Text = auxIdCategoria;
            if (dgvPesquisaProduto.RowCount < 1)
            {
                dgvPesquisaProduto.Rows.Add();
            }
        }

        private void cmbPesquisaProdutoLoja_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvPesquisaProduto.Rows.Clear();
            if (cmbPesquisaProdutoLoja.Text != "")
            {
                auxConsistencia = 0;
                try
                {
                    banco.abreConexao();
                    banco.startTransaction("conector_find_loja");
                    banco.addParametro("tipo", "2");
                    banco.addParametro("find_loja", cmbPesquisaProdutoLoja.Text);
                    banco.procedimentoSet();
                }
                catch (Exception erro)
                { MessageBox.Show("Erro não identificado, entre contato como revendedor", erro.Message); auxConsistencia = 1; }
                finally
                {
                    if (auxConsistencia == 0)
                    {
                        auxIdLoja = Convert.ToString(banco.retornaSet().Tables[0].Rows[0][0]);                        
                    }                    
                    banco.fechaConexao();
                    auxLoja = cmbPesquisaProdutoLoja.Text;
                        if (dgvPesquisaProduto.RowCount < 1)
                        {
                            dgvPesquisaProduto.Rows.Add();
                        }
                }
            }//end if
        }

        private void btnPesquisaProduto_Click(object sender, EventArgs e)
        {
            conector_find_produto();
        }

        private void cmbPesquisaProduto_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbPesquisaProduto.Text)
            {
                case "Código":
                    dgvPesquisaProduto.Rows.Clear();
                    if (dgvPesquisaProduto.RowCount < 1)
                    {
                        dgvPesquisaProduto.Rows.Add();
                    }
                    lblPesquisa.Text = "Código";
                    aux = 2;
                    break;
                case "Descrição":
                    dgvPesquisaProduto.Rows.Clear();
                    if (dgvPesquisaProduto.RowCount < 1)
                    {
                        dgvPesquisaProduto.Rows.Add();
                    }
                    lblPesquisa.Text = "Descrição";
                    aux = 1;
                    break;
            }
        }

        private void dgvPesquisaProduto_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvPesquisaProduto.RowCount >= 1 && dgvPesquisaProduto.CurrentRow.Cells[0].Value != null)
            {
                auxProduto = dgvPesquisaProduto.CurrentRow.Cells[0].Value.ToString();
                auxDescProduto = dgvPesquisaProduto.CurrentRow.Cells[1].Value.ToString();
                auxBarra = dgvPesquisaProduto.CurrentRow.Cells[4].Value.ToString();
                auxPriceVenda = dgvPesquisaProduto.CurrentRow.Cells[5].Value.ToString();
                auxEstoque = dgvPesquisaProduto.CurrentRow.Cells[3].Value.ToString();
            }
        }

        private void dgvPesquisaProduto_DoubleClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }

        private void btnLimpa_Click(object sender, EventArgs e)
        {
            conector_clear_obj();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pesquisa = new pesquisaFornecedor();
            if (pesquisa.ShowDialog(this) == DialogResult.OK)
            {
                auxIdFornecedor = pesquisa.Gridchave;
                newDescFornecedor = pesquisa.GridRazao; 
                txtIdPesquisaProdutoFornecedor.Text = auxIdFornecedor;
                txtDescPesquisaProdutoFornecedor.Text = newDescFornecedor;
                dgvPesquisaProduto.Rows.Clear();
                if (dgvPesquisaProduto.RowCount < 1)
                {
                    dgvPesquisaProduto.Rows.Add();
                }
            }
        }

        private void txtIdPesquisaProdutoFornecedor_TextChanged(object sender, EventArgs e)
        {
            dgvPesquisaProduto.Rows.Clear();
            if (dgvPesquisaProduto.RowCount < 1)
            {
                dgvPesquisaProduto.Rows.Add();
            }
        }
        private void dgvPesquisaProduto_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        /// <summary>
        /// DESENVOLVEDOR : Nadia
        /// EVENTO KEYDOWN_pesquisaProduto 
        /// ALTERACAO 10/03/2014 NADIA : inclusao de novas teclas de comando atraves do codigo ASCI
        /// ENFOQUE : Esc - Fecha a tela
        ///           Enter - Aciona a tecla Enter para executar funcao Tab.
        ///           F2 - Pesquisa a partir do CODIGO
        ///           F3 - Pesquisa a partir da DESCRICAO
        /// </summary>
        private void pesquisaProduto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Dispose();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                ProcessTabKey(true);
                conector_find_produto();
            }
            else if (e.KeyCode == Keys.F2)
            {
                dgvPesquisaProduto.Rows.Clear();
                if (dgvPesquisaProduto.RowCount < 1)
                {
                    dgvPesquisaProduto.Rows.Add();
                }
                lblPesquisa.Text = "Código";
                cmbPesquisaProduto.Text = "Código";
                aux = 2;
                txtPesquisaProduto.Select();
            }
            else if (e.KeyCode == Keys.F3)
            {
                dgvPesquisaProduto.Rows.Clear();
                if (dgvPesquisaProduto.RowCount < 1)
                {
                    dgvPesquisaProduto.Rows.Add();
                }
                lblPesquisa.Text = "Descrição";
                cmbPesquisaProduto.Text = "Descrição";
                aux = 1;
                txtPesquisaProduto.Select();
            }
        }

        private void txtIdPesquisaProdutoSetor_Validated(object sender, EventArgs e)
        {
            if (txtIdPesquisaProdutoSetor.Text != "")
            {
                try
                {
                    auxConsistencia = 0;
                    banco.abreConexao();
                    banco.startTransaction("conector_find_setor");
                    banco.addParametro("tipo", "2");
                    banco.addParametro("find", txtIdPesquisaProdutoSetor.Text);
                    banco.procedimentoRead();
                    if (banco.retornaRead().Read() == true)
                    {
                        auxIdSetor = banco.retornaRead().GetString(0);
                        newDescSetorProduto = banco.retornaRead().GetString(1);
                    }
                    else
                    {
                        MessageBox.Show("Setor inválido.", "Caro Usuário", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        auxConsistencia = 1;
                    }
                }
                catch (Exception erro)
                { MessageBox.Show(erro.Message, "Caro cliente"); auxConsistencia = 1; }
                finally
                {
                    banco.fechaConexao();
                    if (auxConsistencia == 0)
                    {
                        cmbPesquisaProdutoSetor.Text = newDescSetorProduto;
                    }

                }//end if
                if ((cmbPesquisaProdutoSetor.Text != "") && cmbPesquisaProdutoSetor.SelectedIndex > -1)
                {
                    conector_find_grupo();
                }
            }
        }

        private void txtIdPesquisaProdutoGrupo_Validated(object sender, EventArgs e)
        {
            if (txtIdPesquisaProdutoGrupo.Text != "")
            {
                countField = 0;
                try
                {
                    auxConsistencia = 0;
                    banco.abreConexao();
                    banco.startTransaction("conector_find_grupo");
                    banco.addParametro("tipo", "2");
                    banco.addParametro("find", txtIdPesquisaProdutoGrupo.Text);
                    banco.procedimentoRead();
                    if (banco.retornaRead().Read() == true)
                    {
                        auxIdGrupo = banco.retornaRead().GetString(0);
                        newDescGrupoProduto = banco.retornaRead().GetString(1);
                    }
                    else
                    {
                        MessageBox.Show("Grupo inválido para esse setor.", "Caro Usuário", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        auxConsistencia = 1;
                    }
                }
                catch (Exception erro)
                { MessageBox.Show(erro.Message, "Caro cliente"); auxConsistencia = 1; }
                finally
                {
                    banco.fechaConexao();
                    if (auxConsistencia == 0)
                    {
                        cmbPesquisaProdutoGrupo.Text = newDescGrupoProduto;
                    }

                }//end if
                if (((cmbPesquisaProdutoGrupo.Text != "") && cmbPesquisaProdutoGrupo.SelectedIndex > -1) && ((cmbPesquisaProdutoGrupo.Text != "") && cmbPesquisaProdutoGrupo.SelectedIndex > -1))
                {
                    conector_find_categoria();
                    txtIdPesquisaProdutoGrupo.Text = auxIdGrupo;
                }
            }
        }

        private void txtIdPesquisaProdutoCategoria_Validated(object sender, EventArgs e)
        {
            if (txtIdPesquisaProdutoCategoria.Text != "")
            {
                try
                {
                    auxConsistencia = 0;
                    banco.abreConexao();
                    banco.startTransaction("conector_find_categoria");
                    banco.addParametro("tipo", "2");
                    banco.addParametro("find", txtIdPesquisaProdutoCategoria.Text);
                    banco.addParametro("find_setor", auxIdSetor);
                    banco.addParametro("find_grupo", auxIdGrupo);
                    banco.procedimentoRead();
                    if (banco.retornaRead().Read() == true)
                    {
                        auxIdCategoria = banco.retornaRead().GetString(0);
                        newDescCategoriaProduto = banco.retornaRead().GetString(1);
                    }
                    else
                    {
                        MessageBox.Show("Categoria inválida para esse grupo e setor.", "Caro Usuário", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        auxConsistencia = 1;
                    }
                }
                catch (Exception erro)
                { MessageBox.Show(erro.Message, "Caro cliente"); auxConsistencia = 1; }
                finally
                {
                    banco.fechaConexao();
                    if (auxConsistencia == 0)
                    {
                        cmbPesquisaProdutoCategoria.Text = newDescCategoriaProduto;
                    }
                }
            }
        }

        private void txtIdPesquisaProdutoFornecedor_Validated(object sender, EventArgs e)
        {
            if (txtIdPesquisaProdutoFornecedor.Text != "")
            {
                conector_find_fornecedor(txtIdPesquisaProdutoFornecedor.Text);
            }
        }

        private void pesquisaProduto_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void pesquisaProduto_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void dgvPesquisaProduto_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }

        private void dgvPesquisaProduto_KeyUp(object sender, KeyEventArgs e)
        {
            if (dgvPesquisaProduto.RowCount >= 1 && dgvPesquisaProduto.CurrentRow.Cells[0].Value != null)
            {
                auxProduto = dgvPesquisaProduto.CurrentRow.Cells[0].Value.ToString();
                auxDescProduto = dgvPesquisaProduto.CurrentRow.Cells[1].Value.ToString();
                auxBarra = dgvPesquisaProduto.CurrentRow.Cells[4].Value.ToString();
                auxPriceVenda = dgvPesquisaProduto.CurrentRow.Cells[5].Value.ToString();
                auxEstoque = dgvPesquisaProduto.CurrentRow.Cells[3].Value.ToString();
            }
        }
    }
}
