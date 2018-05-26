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
    public partial class telefone : Form
    {
        public telefone()
        {
            InitializeComponent();
        }
        public telefone(string auxId, string auxName, string auxIdAtivi)
            :base()
        {
            auxIdAtividade = auxIdAtivi;
            auxIdCliente = auxId;
            auxNameCliente = auxName;
            this.InitializeComponent();
        }
        // Bloco de variavel
        private Int16 flagSemaforo;
        private int auxConsistencia;
        private int auxGrid = 0;
        private string newddd;
        private string auxIdFoneType;
        private string auxIdCliente = "";
        private string auxNameCliente;
        private string auxIdAtividade = "";
        private string newtelefone;
        private string newpriori = "false";
        private string newcomplemento;
        private string newidfone;
        private string newidcliente;
        private int cmbCount = 0;
        private string newidatividade;
        private string newramal;
        private string newFoneIdfonetype;
        private string newDescTipo;

        private int i, j, countField, countRows; //variavel do loop.
        private dados banco = new dados();
        private pesquisaPessoa pesquisa;

        //########################################################## Bloco de procedimentos de banco##########################################################
        private void conector_inc_fone()
        {
            auxConsistencia = 0;
            try
            {
                banco.abreConexao();
                banco.startTransaction("conectorPDV_inc_fone");
                banco.addParametro("inc_idcliente", txtIdCliente.Text);
                banco.addParametro("inc_idatividade", auxIdAtividade);
                banco.addParametro("inc_ddd", newddd);
                banco.addParametro("inc_telefone", newtelefone);
                banco.addParametro("inc_ramal", mskFoneRamal.Text);
                banco.addParametro("inc_idfonetype", auxIdFoneType);
                banco.addParametro("inc_complemento", txtFoneComplemento.Text);
                banco.addParametro("inc_priori", chkFonePriori.Checked == false ? "f" : "v");
                banco.procedimentoRead();
                if (banco.retornaRead().Read() == true)
                {
                    newidfone = banco.retornaRead().GetString(0);
                }
            }
            catch (Exception erro) { msgInfo msg = new msgInfo(erro.Message); msg.ShowDialog();
            auxConsistencia = 1;
            alteraIconesIncluir();
            }
            finally
            {
                if (auxConsistencia == 0)
                {
                    newddd = mskDDD.Text;
                    newtelefone = mskTelefone.Text;
                    newDescTipo = cmbFoneType.Text;
                    newramal = mskFoneRamal.Text;
                    newcomplemento = txtFoneComplemento.Text;
                    newpriori = chkFonePriori.Checked == false ? "f" : "v";
                    banco.fechaConexao();
                    flagSemaforo = 1;
                    dgvPesquisaFone.Rows.Clear();
                    conectorPDV_find_fone_set();
                    dgvPesquisaFone.Select();
                    alteraIconesSalvar();
                }
            };
        }
        private void conectorPDV_alt_fone()
        {
            auxConsistencia = 0;
            try
            {
                banco.abreConexao();
                banco.startTransaction("conectorPDV_alt_fone");
                banco.addParametro("new_idfone", newidfone);
                banco.addParametro("new_idcliente", txtIdCliente.Text);
                banco.addParametro("new_idatividade", auxIdAtividade);
                banco.addParametro("new_ddd", newddd);
                banco.addParametro("new_telefone", newtelefone);
                banco.addParametro("new_ramal", mskFoneRamal.Text);
                banco.addParametro("new_idfonetype", auxIdFoneType);
                banco.addParametro("new_complemento", txtFoneComplemento.Text);
                banco.addParametro("new_priori", chkFonePriori.Checked == false ? "f" : "v");
                banco.procedimentoRead();
            }
            catch (Exception erro)
            {
                msgInfo msg = new msgInfo(erro.Message); msg.ShowDialog();
                auxConsistencia = 1;
                alteraIconesIncluir();
            }
            finally
            {
                banco.fechaConexao();
                if (auxConsistencia == 0)
                {
                    newddd = mskDDD.Text;
                    newtelefone = mskTelefone.Text;
                    newDescTipo = cmbFoneType.Text;
                    newramal = mskFoneRamal.Text;
                    newcomplemento = txtFoneComplemento.Text;
                    flagSemaforo = 1;
                    dgvPesquisaFone.Rows.Clear();
                    conectorPDV_find_fone_set();
                    dgvPesquisaFone.Select();
                    alteraIconesSalvar();
                }
            };
        }
        public void conectorPDV_find_typeFone()
        {
            cmbFoneType.Items.Clear();
            auxConsistencia = 0;

            try
            {
                banco.abreConexao();
                banco.startTransaction("conectorPDV_find_typeFone");
                banco.addParametro("tipo", "1");
                banco.addParametro("find", "Default");
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
                        for (i = 0; i < countRows; i++)
                        {
                            cmbFoneType.Items.Add(banco.retornaSet().Tables[0].Rows[i][1]);
                        }
                    }
                }
                banco.fechaConexao();
            }
        }

        private void conectorPDV_find_fone_read()
        {
            try
            {
                banco.abreConexao();
                banco.startTransaction("conectorPDV_find_fone");
                banco.addParametro("find", auxIdCliente);
                banco.addParametro("tipo", "1");
                banco.procedimentoRead();
                if (banco.retornaRead().Read() == true)
                {
                    banco.retornaRead().Read();
                    newidfone = banco.retornaRead().GetString(0);
                    newddd = banco.retornaRead().GetString(1);
                    newtelefone = banco.retornaRead().GetString(2);
                    newDescTipo = banco.retornaRead().GetString(3);
                    newpriori = banco.retornaRead().GetString(4);
                    newramal = banco.retornaRead().GetString(5);
                    newcomplemento = banco.retornaRead().GetString(6);
                    newFoneIdfonetype = banco.retornaRead().GetString(7);
                    newidatividade = banco.retornaRead().GetString(8);
                    newidcliente = banco.retornaRead().GetString(9);
                }
            }
            catch (Exception erro) { msgInfo msg = new msgInfo(erro.Message); msg.ShowDialog(); auxConsistencia = 1; }
            finally
            {
                banco.fechaConexao();
                if (auxConsistencia > 0)
                {
                    if (auxIdCliente != "")
                    {
                        mskDDD.Text = newddd;
                        mskTelefone.Text = newtelefone;
                        chkFonePriori.Checked = Convert.ToBoolean(newpriori == "f" ? "false" : "true");
                        cmbFoneType.Text = newDescTipo;
                        mskFoneRamal.Text = newramal;
                        txtFoneComplemento.Text = newcomplemento;
                    }
                }
            }
        }
        private void conectorPDV_find_fone_set()
        {
            try
            {
                banco.abreConexao();
                banco.startTransaction("conectorPDV_find_fone");
                banco.addParametro("find", auxIdCliente);
                banco.addParametro("tipo", "1");
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
                        dgvPesquisaFone.AllowUserToAddRows = false;
                        for (i = 0; i < countRows; i++)
                        {
                            dgvPesquisaFone.Rows.Add();
                            for (j = 0; j < countField; j++)
                            {
                                if (j == 4)
                                {
                                    if (Convert.ToString(banco.retornaSet().Tables[0].Rows[i][j]) == "v")
                                    { dgvPesquisaFone.Rows[i].Cells[j].Value = true; }
                                    else { dgvPesquisaFone.Rows[i].Cells[j].Value = false; }
                                }
                                else
                                {
                                    dgvPesquisaFone.Rows[i].Cells[j].Value = Convert.ToString(banco.retornaSet().Tables[0].Rows[i][j]);
                                }
                            }
                        }
                    }
                }
                banco.fechaConexao();
            }
        }
        //###############################################################end banco#######################################################################
        //###########################################################Bloco de procedimento simples#######################################################
        void statusItens(Boolean flag)
        {
            mskDDD.Enabled = flag;
            mskTelefone.Enabled = flag;
            chkFonePriori.Enabled = flag;
            cmbFoneType.Enabled = flag;
            mskFoneRamal.Enabled = flag;
            txtFoneComplemento.Enabled = flag;
            //dgvPesquisaFone.Enabled = flag;
        }
        void clearObjetos()
        {
            mskDDD.Clear();
            mskTelefone.Clear();
            chkFonePriori.Checked = false;
            cmbFoneType.SelectedIndex = -1;
            mskFoneRamal.Clear();
            txtFoneComplemento.Clear();
            dgvPesquisaFone.Rows.Clear();
        }
        void zeraVariaveis()
        {
            newidfone = "";
            newddd = "";
            newtelefone = "";
            newDescTipo = "";
            newpriori = "false";
            newramal = "";
            newcomplemento = "";
            newFoneIdfonetype = "";
            newidatividade = "";
            newidcliente = "";
        }
        private void alteraIconesIncluir()
        {
            if(auxGrid == 0)
            {
                inserirFone.Enabled = false;
                pesquisarFone.Enabled = false;
                salvarFone.Enabled = true;
                cancelarFone.Enabled = true;
                relatorioFone.Enabled = false;
                cmbCount = 0;
        }
            auxGrid = 0;
         }
        private void alteraIconesSalvar()
        {
            inserirFone.Enabled = true;
            pesquisarFone.Enabled = true;
            salvarFone.Enabled = false;
            cancelarFone.Enabled = false;
            relatorioFone.Enabled = true;
        }
        private void alteraIconesCancelar()
        {
            mskDDD.Text = newddd;
            mskTelefone.Text = newtelefone;
            MessageBox.Show(newpriori);
            chkFonePriori.Checked = Convert.ToBoolean(newpriori);
            cmbFoneType.Text = newDescTipo;
            mskFoneRamal.Text = newramal;
            txtFoneComplemento.Text = newcomplemento;

            inserirFone.Enabled = true;
            pesquisarFone.Enabled = true;
            salvarFone.Enabled = false;
            cancelarFone.Enabled = false;
            relatorioFone.Enabled = true;

            flagSemaforo = 1;
        }
        private void telefone_Load(object sender, EventArgs e)
        {
            statusItens(false);
            clearObjetos();
            flagSemaforo = 1;
            auxConsistencia = 0;
            conectorPDV_find_typeFone();
            txtClienteFone.Text = auxNameCliente;
            txtIdCliente.Text = auxIdCliente;
            if (auxIdCliente != "")
            {
                //conector_find_fone_read();
                conectorPDV_find_fone_set();
                dgvPesquisaFone.Select();
            }
            else 
            {
                mskDDD.Select();
            }
            alteraIconesSalvar();
            
        }

        private void dgvPesquisaFone_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvPesquisaFone_KeyUp(object sender, KeyEventArgs e)
        {
            newidfone = dgvPesquisaFone.CurrentRow.Cells[0].Value.ToString();
            newddd = dgvPesquisaFone.CurrentRow.Cells[1].Value.ToString();
            newtelefone = dgvPesquisaFone.CurrentRow.Cells[2].Value.ToString();
            newDescTipo = dgvPesquisaFone.CurrentRow.Cells[3].Value.ToString();
            newpriori = dgvPesquisaFone.CurrentRow.Cells[4].Value.ToString();
            newramal = dgvPesquisaFone.CurrentRow.Cells[5].Value.ToString();
            newcomplemento = dgvPesquisaFone.CurrentRow.Cells[6].Value.ToString();
            newFoneIdfonetype = dgvPesquisaFone.CurrentRow.Cells[7].Value.ToString();
            newidatividade = dgvPesquisaFone.CurrentRow.Cells[8].Value.ToString();
            newidcliente = dgvPesquisaFone.CurrentRow.Cells[9].Value.ToString();
            auxGrid = 1;
            mskDDD.Text = newddd;
            auxGrid = 1;
            mskTelefone.Text = newtelefone;
            auxGrid = 1;
            chkFonePriori.Checked = Convert.ToBoolean(newpriori);
            auxGrid = 1;
            cmbFoneType.Text = newDescTipo;
            auxGrid = 1;
            mskFoneRamal.Text = newramal;
            auxGrid = 1;
            txtFoneComplemento.Text = newcomplemento;
            auxGrid = 0;
            statusItens(true);
        }

        private void dgvPesquisaFone_Click(object sender, EventArgs e)
        {
            newidfone = dgvPesquisaFone.CurrentRow.Cells[0].Value.ToString();
            newddd = dgvPesquisaFone.CurrentRow.Cells[1].Value.ToString();
            newtelefone = dgvPesquisaFone.CurrentRow.Cells[2].Value.ToString();
            newDescTipo = dgvPesquisaFone.CurrentRow.Cells[3].Value.ToString();
            newpriori = dgvPesquisaFone.CurrentRow.Cells[4].Value.ToString();
            newramal = dgvPesquisaFone.CurrentRow.Cells[5].Value.ToString();
            newcomplemento = dgvPesquisaFone.CurrentRow.Cells[6].Value.ToString();
            newFoneIdfonetype = dgvPesquisaFone.CurrentRow.Cells[7].Value.ToString();
            newidatividade = dgvPesquisaFone.CurrentRow.Cells[8].Value.ToString();
            newidcliente = dgvPesquisaFone.CurrentRow.Cells[9].Value.ToString();
            auxGrid = 1;
            mskDDD.Text = newddd;
            auxGrid = 1;
            mskTelefone.Text = newtelefone;
            auxGrid = 1;
            chkFonePriori.Checked = Convert.ToBoolean(newpriori);
            auxGrid = 1;
            cmbFoneType.Text = newDescTipo;
            auxGrid = 1;
            mskFoneRamal.Text = newramal;
            auxGrid = 1;
            txtFoneComplemento.Text = newcomplemento;
            auxGrid = 0;
            statusItens(true);

        }

        private void pesquisarFone_Click(object sender, EventArgs e)
        {
            pesquisa = new pesquisaPessoa();
            if (pesquisa.ShowDialog(this) == DialogResult.OK)
            {
                clearObjetos();
                zeraVariaveis();
                auxIdCliente = pesquisa.Gridchave;
                auxNameCliente = pesquisa.GridNome;
                auxIdAtividade = pesquisa.GridIdAtividade;
                txtClienteFone.Text = auxNameCliente;
                txtIdCliente.Text = auxIdCliente;
                if (flagSemaforo != 0)
                {
                    conectorPDV_find_fone_read();
                    conectorPDV_find_fone_set();
                    dgvPesquisaFone.Select();
                }
                
            }
            if ((newddd != "") && (newtelefone != ""))
            {
                statusItens(true);
            }
            else
            {
                statusItens(false);
                clearObjetos();
            }
            flagSemaforo = 1;
            alteraIconesSalvar();
        }

        private void btnPesquisaCliente_Click(object sender, EventArgs e)
        {
            pesquisa = new pesquisaPessoa();
            if (pesquisa.ShowDialog(this) == DialogResult.OK)
            {
                clearObjetos();
                zeraVariaveis();
                auxIdCliente = pesquisa.Gridchave;
                auxNameCliente = pesquisa.GridNome;
                auxIdAtividade = pesquisa.GridIdAtividade;
                txtClienteFone.Text = auxNameCliente;
                txtIdCliente.Text = auxIdCliente;
                if (flagSemaforo != 0)
                {
                    conectorPDV_find_fone_read();
                    conectorPDV_find_fone_set();
                    dgvPesquisaFone.Select();
                }

            }
            if ((newddd != "") && (newtelefone != ""))
            {
                statusItens(true);
            }
            else
            {
                statusItens(false);
                clearObjetos();
            }
            flagSemaforo = 1;
            alteraIconesSalvar();
        }

        private void inserirFone_Click(object sender, EventArgs e)
        {
            if (auxIdCliente != "")
            {
                auxConsistencia = 0;
                clearObjetos();
                alteraIconesIncluir();
                mskDDD.Select();
                newpriori = "false";
                flagSemaforo = 0;
                statusItens(true);
                mskDDD.Text = "00";
                mskDDD.Select();
            }
            else { MessageBox.Show("Para inserir um novo telefone, você precisa selecionar um cliente.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Warning); btnPesquisaCliente.Select(); }
        }

        private void cmbFoneType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFoneType.Text != "")
            {
                countField = 0;
                try
                {
                    banco.abreConexao();
                    banco.startTransaction("conector_find_typeFone");
                    banco.addParametro("tipo", "2");
                    banco.addParametro("find", cmbFoneType.Text);
                    banco.procedimentoSet();
                }
                catch (Exception erro)
                { MessageBox.Show("Erro não identificado, entre contato como revendedor", erro.Message); }
                finally
                {
                    auxIdFoneType = Convert.ToString(banco.retornaSet().Tables[0].Rows[0][0]);
                    banco.fechaConexao();
                }
            }//end if
            
        }

        private void salvarFone_Click(object sender, EventArgs e)
        {
            newtelefone = mskTelefone.Text;
            newtelefone = mskTelefone.Text.Replace(".", "");
            newtelefone = newtelefone.Replace("-", "");
            newtelefone = newtelefone.Replace("/", "");
            
            newddd = mskDDD.Text;
            newddd = mskDDD.Text.Replace(".", "");
            newddd = newddd.Replace("-", "");
            newddd = newddd.Replace("/", "");
            newddd = newddd.Replace("(", "");
            newddd = newddd.Replace(")", "");

            if (newddd.Substring(0, 2) == "00")
            {
                if (newtelefone.Trim().Length == 8)
                {
                    if (newddd.Trim().Length == 4)
                    {
                        if (cmbFoneType.Text != "")
                        {
                            if (flagSemaforo == 0)
                            {
                                conector_inc_fone();
                            }
                            else
                            {
                                conectorPDV_alt_fone();
                            }
                        }
                        else { MessageBox.Show("Para inserir um novo telefone, você precisa preenche o tipo respectivo ao telefone.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Warning); cmbFoneType.Select(); }
                    }
                    else { MessageBox.Show("Para inserir um novo telefone, você precisa preenche o DDD corretamente.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Warning); mskDDD.Select(); }
                }
                else { MessageBox.Show("Para inserir um novo telefone, você precisa preenche o telefone corretamente.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Warning); mskTelefone.Select(); }
            }
            else { MessageBox.Show("DDD deve começar com '00', exemplo você é de São Paulo '0011'.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Warning); mskDDD.Clear(); mskDDD.Text = "00"; mskDDD.Select(); }
        }

        private void cancelarFone_Click(object sender, EventArgs e)
        {
            if ((txtClienteFone.Text != "") && (newddd != ""))
            {
                statusItens(true);
            }
            else
            {
                statusItens(false);
                zeraVariaveis();
            }
            dgvPesquisaFone.Rows.Clear();
            conectorPDV_find_fone_set();
            alteraIconesCancelar();
        }

        private void mskDDD_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            
        }

        private void mskTelefone_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            
        }

        private void mskFoneRamal_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            
        }

        private void txtFoneComplemento_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
         
        }

        private void chkFonePriori_CheckedChanged(object sender, EventArgs e)
        {
            alteraIconesIncluir();
            if(chkFonePriori.Checked == true)
            { newpriori = "true"; }
            else
            { newpriori = "false"; }
            
        }

        private void cmbFoneType_TextChanged(object sender, EventArgs e)
        {
            if (cmbCount == 0)
            {
                alteraIconesIncluir();
            } cmbCount++;
        }

        private void mskTelefone_TextChanged(object sender, EventArgs e)
        {
            alteraIconesIncluir();
        }

        private void mskDDD_TextChanged(object sender, EventArgs e)
        {
            alteraIconesIncluir();
        }

        private void mskFoneRamal_TextChanged(object sender, EventArgs e)
        {
            alteraIconesIncluir();
        }

        private void txtFoneComplemento_TextChanged(object sender, EventArgs e)
        {
            alteraIconesIncluir();
        }

        private void cmbFoneType_Click(object sender, EventArgs e)
        {
            cmbCount = 0;
        }

        private void telefone_KeyDown(object sender, KeyEventArgs e)
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
