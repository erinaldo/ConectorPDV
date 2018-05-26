using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
//#######################Modulo Web/Net
using System.Net;
using System.Web;
//#######################End Modulo Web/Net

namespace conectorPDV001
{
    public partial class editorPrestacao : Form
    {
        public editorPrestacao()
        {
            InitializeComponent();
        }
        public editorPrestacao(string pessoa, string nome, string store)
        {
            InitializeComponent();
            auxIdClente = pessoa;
            auxNomeCliente = nome;
            auxIdLoja = store;
        }
        //######################################################Variaveis encapsuladas########################################################
        private Decimal auxNumeric;
        private string flagNumeric;
        private int posSeparator;
        private string auxIdLoja = "";
        private dados banco = new dados();
        private conector_full_variable alwaysVariables = new conector_full_variable();
        private int i, countField, countRows, j; //variavel do loop.
        private int auxConsistencia = 0;
        private string auxIdClente;
        private string nr_contratos = "0";
        private DataSet dsRDividento = new DataSet();
        private DataSet dsRPepleo = new DataSet();
        public string GridCliente
        {
            get
            {
                return auxIdClente;
            }
            set
            {
                auxIdClente = value;
            }
        }
        private string auxNomeCliente;
        public string GridNomeCliente
        {
            get
            {
                return auxNomeCliente;
            }
            set
            {
                auxNomeCliente = value;
            }
        }
        private string auxIdContrato;
        public string GridContrato
        {
            get
            {
                return auxIdContrato;
            }
            set
            {
                auxIdContrato = value;
            }
        }
        private string auxNrParcela;
        public string GridNrParcela
        {
            get
            {
                return auxNrParcela;
            }
            set
            {
                auxNrParcela = value;
            }
        }
        private string auxEmissao;
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
        private string auxVencimento;
        public string GridVencimento
        {
            get
            {
                return auxVencimento;
            }
            set
            {
                auxVencimento = value;
            }
        }
        private string auxValorPrestacao;
        public string GridValorPrestacao
        {
            get
            {
                return auxValorPrestacao;
            }
            set
            {
                auxValorPrestacao = value;
            }
        }
        private string auxAtraso = "0";
        public string GridPrazo
        {
            get
            {
                return auxAtraso;
            }
            set
            {
                auxAtraso = value;
            }
        }
        public string GridStore
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
        //######################################################END Variaveis encapsuladas####################################################
        //######################################################Controle de objetos###########################################################
        void conector_obj_webservice()
        {
            dgvParcelamentoEditorParcela.Rows.Clear();
            if (dgvParcelamentoEditorParcela.RowCount < 1)
            {
                dgvParcelamentoEditorParcela.Rows.Add();
            }
            msgInfo msg = new msgInfo("CARO USUÁRIO" + " - FIQUE CIENTE QUE SERÁ REALIZADO A CONEXÃO COM O SERVIDOR PARA ATUALIZAR A SUA BASE DE CONTRATOS, POIS VOCÊ ESTA SOLICITANTE UMA ATUALIZAÇÃO DA BASE LOCAL."); msg.ShowDialog();
            WebConectorServer.Service MyConector2 = new WebConectorServer.Service();

            auxConsistencia = 0;

            string test = "Host Unavailable";

            if (IsConnected() == true)
            {
                test = PingHost(MyConector2.Url);
                if (test == null) { test = "Host Unavailable"; }
            }
            else
            {
                test = "Host Unavailable";
            }

            if (test == "Host Unavailable")
            {
                msgInfo msg1 = new msgInfo("CARO USUÁRIO" + " - FIQUE CIENTE - SUA CONSULTA COM O SERVIDOR FALHOU, ATUALIZAÇÃO REALIZA LOCALMENTE."); msg1.ShowDialog();
                conectorPDV_exe_parcela();
                conectorPDV_find_pessoa();
                lblStatusConexaoEditor.Text = "LOCAL - OFF -LINE COM SERVIDOR";
                this.lblStatusConexaoEditor.ForeColor = System.Drawing.Color.Red;
            }
            else if (test == "Service Up")
            {
                if (auxConsistencia == 0)
                {

                    try
                    {
                        auxConsistencia = 0;
                        dsRDividento = MyConector2.ObterDividento(auxIdClente, "0", "0", "0", "2");
                        countRows = dsRDividento.Tables[0].DefaultView.Count;
                        countField = dsRDividento.Tables[0].Columns.Count;
                        lblStatusConexaoEditor.Text = "LOCAL - ON - LINE COM O SERVIDOR";
                        this.lblStatusConexaoEditor.ForeColor = System.Drawing.Color.Yellow;
                    }
                    catch (Exception)
                    {
                        auxConsistencia = 1;
                        msgInfo msg2 = new msgInfo("ERRO FATAL - " + "CONEXÃO COM WEBSERVICE FALHOU, REPITA A OPERAÇÃO E VERIFIQUE O STATUS DA INTERNET."); msg2.ShowDialog();

                    }
                }

                if (countRows > 0 && auxConsistencia == 0)
                {
                    for (i = 0; i < countRows; i++)
                    {
                        dgvParcelamentoEditorParcela.Rows.Add();
                        for (j = 0; j < countField; j++)
                        {
                            dgvParcelamentoEditorParcela.Rows[i].Cells[j].Value = Convert.ToString(dsRDividento.Tables[0].Rows[i][j]);
                            if ((j == 6) && (Convert.ToInt32(dsRDividento.Tables[0].Rows[i][j]) > 0))
                            {
                                //dgvFaturamento.Rows[i].DefaultCellStyle.BackColor = Color.Tan;
                                dgvParcelamentoEditorParcela.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                            }
                        }
                    }
                }
                if (auxConsistencia == 0)
                {

                    try
                    {
                        auxConsistencia = 0;
                        dsRPepleo = MyConector2.ObterPepleoSingle(auxIdClente);
                        countRows = dsRPepleo.Tables[0].DefaultView.Count;
                    }
                    catch (Exception)
                    {
                        auxConsistencia = 1;
                        msgInfo msg3 = new msgInfo("ERRO FATAL - " + "CONEXÃO COM WEBSERVICE FALHOU, REPITA A OPERAÇÃO E VERIFIQUE O STATUS DA INTERNET."); msg3.ShowDialog();
                        lblStatusConexaoEditor.Text = "LOCAL - OFF -LINE COM O SERVIDOR";
                        this.lblStatusConexaoEditor.ForeColor = System.Drawing.Color.Red;

                    }
                }

                if (countRows == 1 && auxConsistencia == 0)
                {
                    lbNumeroClienteEditorCrediario.Text = dsRPepleo.Tables[0].Rows[0][0].ToString();
                    lbNomeClienteEditorParcela.Text = dsRPepleo.Tables[0].Rows[0][1].ToString();
                }

            }
            MyConector2.Dispose();
            lblCountContratosEditor.Text = conector_verifica_exits_contrato(auxIdLoja, auxIdClente).ToString();
            dgvParcelamentoEditorParcela.Select();
        }
        //######################################################END Controle de objetos#######################################################
        //######################################################Procedimento de banco#########################################################
        public int conector_verifica_exits_contrato(string store, string client)
        {
            auxConsistencia = 0;
            int result = -1;
            try
            {
                banco.abreConexao();
                banco.singleTransaction("select count(*) from crediario where idLoja=?store and idCliente=?id and status not in(1,5)");
                banco.addParametro("?store", store);
                banco.addParametro("?id", client);
                banco.procedimentoRead();
                if (banco.retornaRead().Read() == true)
                {
                    result = Convert.ToInt32(banco.retornaRead().GetString(0));
                }
            }
            catch (Exception e)
            {
                auxConsistencia = 1;
                throw new Exception("ERRO BANCO DE DADOS: " + e.Message.ToString());
            }
            finally
            {
                banco.fechaConexao();
                if (auxConsistencia == 1)
                {
                    result = -1;
                }
            }
            return result;
        }
        public void conectorPDV_exe_parcela()
        {
            dgvParcelamentoEditorParcela.Rows.Clear();
            countField = 0;
            auxConsistencia = 0;
            try
            {
                banco.abreConexao();
                banco.startTransaction("conectorPDV_exe_parcela");
                banco.addParametro("tipo", "2");
                banco.addParametro("store", "0");
                banco.addParametro("contrato", "0");
                banco.addParametro("prestacao", "0");
                banco.addParametro("pessoa", auxIdClente);
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
                        dgvParcelamentoEditorParcela.AllowUserToAddRows = false;
                        for (i = 0; i < countRows; i++)
                        {
                            dgvParcelamentoEditorParcela.Rows.Add();
                            for (j = 0; j < countField; j++)
                            {
                                dgvParcelamentoEditorParcela.Rows[i].Cells[j].Value = Convert.ToString(banco.retornaSet().Tables[0].Rows[i][j]);
                                if ((j == 6) && (Convert.ToInt32(banco.retornaSet().Tables[0].Rows[i][j]) > 0))
                                {
                                    //dgvFaturamento.Rows[i].DefaultCellStyle.BackColor = Color.Tan;
                                    dgvParcelamentoEditorParcela.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                                }
                            }
                        }
                    }
                    else if (dgvParcelamentoEditorParcela.RowCount < 1)
                    {
                        dgvParcelamentoEditorParcela.Rows.Add();
                    }
                }
                banco.fechaConexao();
            }
        }
        private void conectorPDV_find_pessoa()
        {

            try
            {
                banco.abreConexao();
                banco.startTransaction("conectorPDV_find_pessoa");
                banco.addParametro("tipo", "1");
                banco.addParametro("find_cliente", auxIdClente);
                banco.addParametro("tipo_cliente", "x");
                banco.addParametro("find_atividade", "1");
                banco.procedimentoRead();
                if (banco.retornaRead().Read() == true)
                {
                    lbNumeroClienteEditorCrediario.Text = banco.retornaRead().GetString(0);
                    lbNomeClienteEditorParcela.Text = banco.retornaRead().GetString(1);
                }
            }
            catch (Exception erro)
            { MessageBox.Show(erro.Message, "Caro cliente"); }
            finally
            {
                banco.fechaConexao();
            }
        }
        //######################################################END Procedimento de banco#####################################################
        //############################################WEB - Import Método da API#####################################################
        [DllImport("wininet.dll")]
        private extern static Boolean InternetGetConnectedState(out int Description, int ReservedValue);
        // Um método que verifica se esta conectado
        public static Boolean IsConnected()
        {
            int Description;
            return InternetGetConnectedState(out Description, 0);
        }
        //############################################END WEB Import Método da API###################################################
        public static string PingHost(string args)//RECEBE COMO PARAMENTRO A URL SERVICE
        {
            HttpWebResponse res = null;

            try
            {
                // Create a request to the passed URI.  
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(args);
                req.Credentials = CredentialCache.DefaultNetworkCredentials;

                // Get the response object.  
                res = (HttpWebResponse)req.GetResponse();

                return "Service Up";
            }
            catch (Exception e)
            {
                msgInfo msg = new msgInfo("Source : " + e.Source + " Exception Source \n" + "Message : " + e.Message + " Exception Message \n" + " --->  Host Unavailable  <--- ");
                //MessageBox.Show("Source : " + e.Source, "Exception Source", MessageBoxButtons.OK);
                //MessageBox.Show("Message : " + e.Message, "Exception Message", MessageBoxButtons.OK);
                return "Host Unavailable";
            }
        }
        //#############################################################Metodos De Controle Webservice########################################
        private void editorPrestacao_Load(object sender, EventArgs e)
        {
            if (auxIdLoja == "")
            {
                auxIdLoja = alwaysVariables.Store;
            }
            int test = conector_verifica_exits_contrato(auxIdLoja, auxIdClente);
            if ( test > 0)
            {
                lblCountContratosEditor.Text = test.ToString(); ;
                conectorPDV_exe_parcela();
                conectorPDV_find_pessoa();
                lblStatusConexaoEditor.Text = "LOCAL - OFF -LINE";
                this.lblStatusConexaoEditor.ForeColor = System.Drawing.Color.Red;

            }
            else
            {
                conector_obj_webservice();
            }
            dgvParcelamentoEditorParcela.Select();
        }

        private void dgvParcelamentoEditorParcela_KeyUp(object sender, KeyEventArgs e)
        {
            /*if (dgvParcelamentoEditorParcela.RowCount > 0)
*/
        }

        private void dgvParcelamentoEditorParcela_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (auxIdContrato != null)
            {
                this.DialogResult = DialogResult.OK;
                this.Dispose();   
            }
        }

        private void dgvParcelamentoEditorParcela_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }

        private void dgvParcelamentoEditorParcela_MouseClick(object sender, MouseEventArgs e)
        {
            if (dgvParcelamentoEditorParcela.RowCount > 0 && dgvParcelamentoEditorParcela.CurrentRow.Cells[1].Value != null)
            {
                auxIdContrato = dgvParcelamentoEditorParcela.CurrentRow.Cells[1].Value.ToString();
                auxEmissao = dgvParcelamentoEditorParcela.CurrentRow.Cells[2].Value.ToString();
                auxNrParcela = dgvParcelamentoEditorParcela.CurrentRow.Cells[3].Value.ToString();
                auxValorPrestacao = dgvParcelamentoEditorParcela.CurrentRow.Cells[4].Value.ToString();
                auxVencimento = dgvParcelamentoEditorParcela.CurrentRow.Cells[5].Value.ToString();
                auxAtraso = dgvParcelamentoEditorParcela.CurrentRow.Cells[6].Value.ToString();
                auxIdLoja = dgvParcelamentoEditorParcela.CurrentRow.Cells[7].Value.ToString();
            }
        }

        private void dgvParcelamentoEditorParcela_Click(object sender, EventArgs e)
        {
            if (dgvParcelamentoEditorParcela.RowCount > 0 && dgvParcelamentoEditorParcela.CurrentRow.Cells[1].Value != null)
            {
                auxIdContrato = dgvParcelamentoEditorParcela.CurrentRow.Cells[1].Value.ToString();
                auxEmissao = dgvParcelamentoEditorParcela.CurrentRow.Cells[2].Value.ToString();
                auxNrParcela = dgvParcelamentoEditorParcela.CurrentRow.Cells[3].Value.ToString();
                auxValorPrestacao = dgvParcelamentoEditorParcela.CurrentRow.Cells[4].Value.ToString();
                auxVencimento = dgvParcelamentoEditorParcela.CurrentRow.Cells[5].Value.ToString();
                auxAtraso = dgvParcelamentoEditorParcela.CurrentRow.Cells[6].Value.ToString();
                auxIdLoja = dgvParcelamentoEditorParcela.CurrentRow.Cells[7].Value.ToString();
            }
        }

        private void dgvParcelamentoEditorParcela_KeyDown(object sender, KeyEventArgs e)
        {
            if (dgvParcelamentoEditorParcela.RowCount > 0 && dgvParcelamentoEditorParcela.CurrentRow.Cells[1].Value != null)
            {
                auxIdContrato = dgvParcelamentoEditorParcela.CurrentRow.Cells[1].Value.ToString();
                auxEmissao = dgvParcelamentoEditorParcela.CurrentRow.Cells[2].Value.ToString();
                auxNrParcela = dgvParcelamentoEditorParcela.CurrentRow.Cells[3].Value.ToString();
                auxValorPrestacao = dgvParcelamentoEditorParcela.CurrentRow.Cells[4].Value.ToString();
                auxVencimento = dgvParcelamentoEditorParcela.CurrentRow.Cells[5].Value.ToString();
                auxAtraso = dgvParcelamentoEditorParcela.CurrentRow.Cells[6].Value.ToString();
                auxIdLoja = dgvParcelamentoEditorParcela.CurrentRow.Cells[7].Value.ToString();
            } if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                ProcessTabKey(true);
            }
            else if (e.KeyCode == Keys.F10)
            {
                conector_obj_webservice();
            }
        }

        private void lklAtualizaWebServiceEditor_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            conector_obj_webservice();
        }

        private void editorPrestacao_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
