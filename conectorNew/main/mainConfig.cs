using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using conectorECFBema; //Flavio Old
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
//#######################Modulo Web/Net
using System.Net;
using System.Web;
//#######################End Modulo Web/Net

namespace conectorPDV001
{
    public partial class mainConfig : Form
    {
        public mainConfig()
        {
            InitializeComponent();
            /*WebConectorServer.Service ex = new WebConectorServer.Service();
            ex.Url = "http://192.168.1.1:8010/WSR.apw";*/
        }

        public mainConfig(int modo)
        {
            InitializeComponent();
            alwaysVariables.ModoOperacao = modo;
            alwaysVariables.ModeloEcf = Convert.ToInt32(getValue("Printer", "modeloEcf", fileSecret));
            alwaysVariables.Desconhecido = getValue("banco_smartPDV", "desconhecido", fileSecret);
            alwaysVariables.UrlWebConector = getValue("banco_smartPDV", "urlWebConector", fileSecret);
            alwaysVariables.AtoCotepe = getValue("banco_smartPDV", "atocotepe", fileSecret);
            alwaysVariables.CopyLocal = getValue("banco_smartPDV", "copyLocal", fileSecret);
            alwaysVariables.LocalHostSuper = getValue("banco", "super_server", fileSecret);
            modoInicializacao(alwaysVariables.ModoOperacao);
        }
        //######################################################VARIAVEIS ENCAPSULADAS#######################################################
        // Declaração das funções não gerenciadas: GetPrivateProfileString e 
        // WritePrivateProfileString
        [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileString")]
        private static extern int GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, int nSize, string lpFileName);
        [DllImport("kernel32.dll", EntryPoint = "WritePrivateProfileString")]
        private static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);
        private string fileSecret;
        const string folderMFDGrand = @"c:\conector\MFD\Grand";
        
        private DateTime dataAgora = DateTime.Now;
        private DateTime dataliberacao;
        private TimeSpan ts;
        StreamWriter file;
        private Decimal auxNumeric;
        private algMd5 key = new algMd5();
        private conectorExport0202 export;
        private conexaoECF functionECF = new conexaoECF();
        private int i;
        private int z;
        private string flagNumeric;
        private int posSeparator;
        private int countRows = 0;
        private int countField = 0;
        private int auxConsistencia = 0;
        private bool controle = false;
        private dados banco = new dados();
        private crypt cryptografia;
        private int valida = 0;
        //private ConectorCF ecf;
        private instrucaol instrucao;
        MG001.superClass super;
        List<string> mensagemLinha;
        string[,] vetorAliquotaLastZ = new string[2, 15];
        string[,] matrizGrd;
        private string mensagem;
        private string fiscal_MSG;
        private string path;
        const string folderCrypt = @"c:\conector\PDV\";
        const string folderTrans = @"c:\conector\Transmissao\";
        const string folderRepc = @"c:\conector\Recepcao\";
        const string folderTef = @"c:\conector\tef\";
        private conector_full_variable alwaysVariables = new conector_full_variable();
        private settings hash = new settings();
        //############Cabeçalho Impressora fiscal#########
        private string strCupom="";
        private timedate fuso;
        private int fiscal_retorno;
        private int fiscal_retornoZ;//STRING com uma posição para receber "1" (RZ pendente) ou "0" (RZ emitida). 
        private int fiscal_flag = 0;/*Cupom fiscal aberto - 1; Fechamento das formas de pagamento iniciado - 2;Horário de verão habilitado - 4;
                                     * Já houve redução Z no dia - 8; Valor não utilizado nos retornos - 16; Permite cancelamento do cupom fiscal - 32;
                                     * Valor não utilizado nos retornos - 64; Memória fiscal sem espaço - 128*/
        private string fiscal_numero_serie = new string('\x20', 20);
        //private string fiscal_numero_serie = new string('\x20', 15);
        private string fiscal_store = new string('\x20', 4);
        private string fiscal_numero_caixa = new string('\x20', 4);
        private string fiscal_data_movimento = new string('\x20', 6);
        private string fiscal_data_printer = new string('\x20', 6);
        private string fiscal_hora_printer = new string('\x20', 6);
        private string fiscal_GT = new string('\x20', 18);
        private string fiscal_GT_Crypt = new string('\x20', 20);
        private string fiscal_GT_compare = new string('\x20', 18);
        private string fiscal_trunca_arredonda = new string('\x20', 2);//  Variável string com 1 posição para receber o flag de truncamento
        private string fiscal_SubTotal = new string('\x20', 14);
        private string fiscal_ValorDescontos = new string('\x20', 14);
        private string fiscal_ValorCancelamentos = new string('\x20', 14);
        private string fiscal_NumReducoes = new string('\x20', 4);
        private string fiscal_Cupom = new string('\x20', 14);
        private string fiscal_Zpendente = new string('\x20', 1);
        private string fiscal_NumCuponsCanc = new string('\x20', 4);
        private string fiscal_CGC = new string('\x20',18);
		private string fiscal_IE = new string('\x20',15);
        private string fiscal_md5 = new string('\x20', 33);
        private string fiscal_dtusuario_last = new string('\x20', 20);
        private string fiscal_dtsoft_basico = new string('\x20', 20);
        private string fiscal_letramf_adicional = new string('\x20', 2);
        private string fiscal_marca = new string('\x20', 15);
        private string fiscal_modelo = new string('\x20', 20);
        private string fiscal_tipo_ecf = new string('\x20', 7);
        private string fiscal_VersaoFirmware = new string('\x20', 7);
        private string fiscal_NumeroSubstituicoesProprietario = new string('\x20', 4);
        private string fiscal_DadosUltimaReducaoMFD = new string('\x20', 1278);
        private string fiscal_data_last_reducaoZ = new string('\x20', 6);
        private string fiscal_hora_last_reducaoZ = new string('\x20', 6);
        private string fiscal_vendaBruta_last_reducaoZ = new string('\x20', 18);
        //############Dados Last Reducao
        private string fiscal_last_reducao_grandTotal = new string('\x20', 18);
        private string fiscal_last_reducao_T18 = new string('\x20', 14);
        private string fiscal_last_reducao_T12 = new string('\x20', 14);
        private string fiscal_last_reducao_T07 = new string('\x20', 14);
        private string fiscal_last_reducao_crz = new string('\x20', 4);
        private string fiscal_last_reducao_coo = new string('\x20', 9);
        private string fiscal_last_reducao_cro = new string('\x20', 4);
        private string fiscal_last_reducao_dataMovimento = new string('\x20', 7);
        private string fiscal_last_reducao_dataEmissao = new string('\x20', 6);
        private string fiscal_last_reducao_horaEmissao = new string('\x20', 7);
        private string fiscal_last_reducao_vendaBruta = new string('\x20', 7);
        private string fiscal_last_reducao_ValorAcumulado = new string('\x20',13);
        private string fiscal_last_reducao_St_ICMS = new string('\x20',14);
        private string fiscal_last_reducao_St_ISSQN = new string('\x20', 14);
        private string fiscal_last_reducao_Isento_ICMS = new string('\x20', 14);
        private string fiscal_last_reducao_naoIncide_ICMS = new string('\x20', 14);
        private string fiscal_last_reducao_naoIncide_ISSQN = new string('\x20', 14);
        private string fiscal_last_reducao_Isento_ISSQN = new string('\x20', 14);
        private string fiscal_last_reducao_desconto_ISSQN = new string('\x20', 14);
        private string fiscal_last_reducao_desconto_ICMS = new string('\x20', 14);
        private string fiscal_last_reducao_acrescimo_ISSQN = new string('\x20', 14);
        private string fiscal_last_reducao_acrescimo_ICMS = new string('\x20', 14);
        private string fiscal_last_reducao_cancelamento_ISSQN = new string('\x20', 14);
        private string fiscal_last_reducao_cancelamento_ICMS = new string('\x20', 14);
        private string fiscal_last_reducao_cancelamento_not_fiscal =new string('\x20', 14);
        private string fiscal_last_reducao_parcial_not_icms = new string('\x20', 14);
        private string fiscal_last_reducao_sangria = new string('\x20', 14);
        private string fiscal_last_reducao_suprimento = new string('\x20', 14);
        private string fiscal_last_reducao_desconto_not_fiscal = new string('\x20', 14);
        private string fiscal_last_reducao_acrescimo_not_fiscal = new string('\x20', 14);
        //############End Dados Last Reducao
        //############END Cabeçalho Impressora fiscal#####
        //############Process
        private Process MyProcess = new Process();
        const int SLEEP_AMOUNT = 100;
        private bool eventHandled;
        private int elapsedTime;
        private int tentativa = 1;
        private int update;
        private int statusBanco;
        private string banco_serie;
        private string banco_numero_caixa;
        private string banco_operador;
        private string banco_sequencia;
        private string strComando;
        //############End Process
        //######################################################VARIAVEIS ENCAPSULADAS#######################################################
        //#########################################################Procedimento de Banco#####################################################
        public string conectorPDV_clear_log_pedido()
        {
            auxConsistencia = 0;
            string result = "0";
            try
            {
                this.lbErroMainConfig.Invoke((MethodInvoker)delegate { this.lbErroMainConfig.Invoke((MethodInvoker)delegate { lbErroMainConfig.Text = banco.abreConexao(); }); });
                banco.startTransaction("conectorPDV_clear_log_pedido");
                if (!File.Exists(@"C:\conector\nfce_temp.txt"))
                {
                    banco.procedimentoRead();
                    if (banco.retornaRead().Read() == true)
                    {
                        result = banco.retornaRead().GetString(0).ToString();
                    }
                }
            }
            catch (Exception e)
            {
                auxConsistencia = 1;
                msgInfo msg = new msgInfo("Caro Cliente - " + e.Message); msg.ShowDialog(); auxConsistencia = 1;
            }
            finally
            {
                banco.fechaConexao();
                if (auxConsistencia == 1)
                {
                    result = "0";
                }
            }
            return result;
        }
        public int conector_update_lineMD5_P2(string codigo, string cripto)
        {
            int retorno = 0;
            try
            {
                auxConsistencia = 0;
                this.lbErroMainConfig.Invoke((MethodInvoker)delegate { this.lbErroMainConfig.Invoke((MethodInvoker)delegate { lbErroMainConfig.Text = banco.abreConexao(); }); });
                banco.singleTransaction("update `conectorpdv`.`tabela_produto` set cripto=?cripto where  codigo=?codigo");
                banco.addParametro("?cripto", cripto);
                banco.addParametro("?codigo", codigo);
                banco.procedimentoRead();
            }
            catch (Exception erro)
            {
                retorno = 0;
                auxConsistencia = 1;
            }
            finally
            {
                banco.fechaConexao();
                if (auxConsistencia == 0)
                {
                    retorno = 1;
                }
            }
            return retorno;
        }
        public void conectorPDV_gera_crypt_produto()
        {
            string[] vetor = new string[1] { "P2" };
            string[,] recarga; //Matriz Bidimencionada
            for (int m = 0; m < vetor.Length; m++)
            {
                if (vetor[m] != "#")
                {
                    try
                    {
                        auxConsistencia = 0;
                        this.lbErroMainConfig.Invoke((MethodInvoker)delegate { this.lbErroMainConfig.Invoke((MethodInvoker)delegate { lbErroMainConfig.Text = banco.abreConexao(); }); });
                        banco.startTransaction("conectorPDV_PAFECF_Tabela");
                        banco.addParametro("varType", vetor[m]);
                        banco.addParametro("store", alwaysVariables.Store);
                        banco.procedimentoSet();

                    }
                    catch (Exception erro) { auxConsistencia = 1; }
                    finally
                    {
                        if (auxConsistencia == 0)
                        {
                            countField = banco.retornaSet().Tables[0].Columns.Count;
                            countRows = banco.retornaSet().Tables[0].DefaultView.Count;
                            if (countRows > 0)
                            {
                                recarga = new string[countRows, countField];
                                for (int i = 0; i < countRows; i++)//Linha
                                {
                                    for (int j = 0; j < countField; j++) //Coluna
                                    {
                                        recarga[i, j] = Convert.ToString(banco.retornaSet().Tables[0].Rows[i][j]);
                                    }
                                }
                                banco.fechaConexao();

                                for (int w = 0; w < recarga.GetUpperBound(0); w++)//Linha
                                {
                                    conector_update_lineMD5_P2(recarga[w, 1], key.GetMd5Sum(export.registro_tipo_p2(vetor[m], recarga[w, 0], recarga[w, 1], recarga[w, 2], recarga[w, 3], recarga[w, 4], recarga[w, 5], recarga[w, 6], recarga[w, 7], recarga[w, 8])));
                                }
                            }
                        }
                        banco.fechaConexao();
                    }
                }
            }
        }
        public void conectorPDV_inc_movimentodia(string store, string numerocaixa, string movimento, string operador, string serie, string nf_ad, string modelo, string crz, string coo, string cro, string emissao, string hora, string vb, string par_desc)
        {
            try
            {
                auxConsistencia = 0;
                this.lbErroMainConfig.Invoke((MethodInvoker)delegate { this.lbErroMainConfig.Invoke((MethodInvoker)delegate { lbErroMainConfig.Text = banco.abreConexao(); }); });
                banco.startTransaction("conectorPDV_inc_movimentodia");
                banco.addParametro("inc_idloja", store);
                banco.addParametro("inc_numeroCaixa", numerocaixa.Replace("\0","").Trim());
                banco.addParametro("inc_movimento", movimento);
                banco.addParametro("inc_operador", operador);
                banco.addParametro("inc_numero_serie", serie.Replace("\0","").Trim());
                banco.addParametro("inc_mf_adicional", nf_ad.Replace("\0","").Trim());
                banco.addParametro("inc_modelo_ecf", modelo.Replace("\0","").Trim());
                banco.addParametro("inc_crz", crz.Replace("\0","").Trim());
                banco.addParametro("inc_coo", coo.Replace("\0","").Trim());
                banco.addParametro("inc_cro", cro.Replace("\0","").Trim());
                banco.addParametro("inc_dataEmissao", emissao);
                banco.addParametro("inc_horaEmissao", hora);
                banco.addParametro("inc_venda_bruta", vb.Replace("\0","").Trim());
                banco.addParametro("inc_par_desconto", par_desc);
                banco.procedimentoRead();
            }
            catch (Exception erro)
            {
                msgInfo msg = new msgInfo(1, "Erro não identificado, entre contato como revendedor! " + erro.Message); msg.ShowDialog();
                auxConsistencia = 1;
            }
            finally
            {
                banco.fechaConexao();
                if (auxConsistencia == 0)
                {
                    if (auxConsistencia == 0)
                    {
                            strCupom = "";
                            instrucao.setVetorCupom();
                            strCupom = instrucao.getMapa(alwaysVariables.UserName, alwaysVariables.Senha, Convert.ToDouble(fiscal_store.Replace("\0", "").Trim()) > 0 ? fiscal_store.Replace("\0", "").Trim() : alwaysVariables.Store, coo.Replace("\0","").Trim(), emissao, numerocaixa.Replace("\0","").Trim(), crz.Replace("\0","").Trim());
                            instrucao.carregaInstrucaoMovimentoVenda(instrucao._movimentoDia, strCupom, 0);
                            //instrucao.compactScript(instrucao._movimentoDia, "mapa");
                    }


                    conectorPDV_inc_detalhe_reducao(alwaysVariables.Store, fiscal_numero_serie, crz.Replace("\0","").Trim(), nf_ad.Replace("\0","").Trim(), fiscal_modelo, banco_operador, fiscal_last_reducao_Isento_ICMS, "I1",1);
                    conectorPDV_inc_detalhe_reducao(alwaysVariables.Store, fiscal_numero_serie, crz.Replace("\0","").Trim(), nf_ad.Replace("\0","").Trim(), fiscal_modelo, banco_operador, fiscal_last_reducao_naoIncide_ICMS, "N1",2);
                    conectorPDV_inc_detalhe_reducao(alwaysVariables.Store, fiscal_numero_serie, crz.Replace("\0","").Trim(), nf_ad.Replace("\0","").Trim(), fiscal_modelo, banco_operador, fiscal_last_reducao_St_ICMS, "F1",3);
                    conectorPDV_inc_detalhe_reducao(alwaysVariables.Store, fiscal_numero_serie, crz.Replace("\0","").Trim(), nf_ad.Replace("\0","").Trim(), fiscal_modelo, banco_operador, fiscal_last_reducao_Isento_ISSQN, "IS1",4);
                    conectorPDV_inc_detalhe_reducao(alwaysVariables.Store, fiscal_numero_serie, crz.Replace("\0","").Trim(), nf_ad.Replace("\0","").Trim(), fiscal_modelo, banco_operador, fiscal_last_reducao_naoIncide_ISSQN, "NS1" ,5);
                    conectorPDV_inc_detalhe_reducao(alwaysVariables.Store, fiscal_numero_serie, crz.Replace("\0","").Trim(), nf_ad.Replace("\0","").Trim(), fiscal_modelo, banco_operador, fiscal_last_reducao_St_ISSQN, "FS1",6);
                    conectorPDV_inc_detalhe_reducao(alwaysVariables.Store, fiscal_numero_serie, crz.Replace("\0","").Trim(), nf_ad.Replace("\0","").Trim(), fiscal_modelo, banco_operador, fiscal_last_reducao_desconto_ICMS, "DT",7);
                    conectorPDV_inc_detalhe_reducao(alwaysVariables.Store, fiscal_numero_serie, crz.Replace("\0","").Trim(), nf_ad.Replace("\0","").Trim(), fiscal_modelo, banco_operador, fiscal_last_reducao_desconto_ISSQN, "DS",8);
                    conectorPDV_inc_detalhe_reducao(alwaysVariables.Store, fiscal_numero_serie, crz.Replace("\0","").Trim(), nf_ad.Replace("\0","").Trim(), fiscal_modelo, banco_operador, fiscal_last_reducao_acrescimo_ICMS, "AT",9);
                    conectorPDV_inc_detalhe_reducao(alwaysVariables.Store, fiscal_numero_serie, crz.Replace("\0","").Trim(), nf_ad.Replace("\0","").Trim(), fiscal_modelo, banco_operador, fiscal_last_reducao_acrescimo_ISSQN, "AS",10);
                    conectorPDV_inc_detalhe_reducao(alwaysVariables.Store, fiscal_numero_serie, crz.Replace("\0","").Trim(), nf_ad.Replace("\0","").Trim(), fiscal_modelo, banco_operador, fiscal_last_reducao_cancelamento_ICMS, "CT",11);
                    conectorPDV_inc_detalhe_reducao(alwaysVariables.Store, fiscal_numero_serie, crz.Replace("\0","").Trim(), nf_ad.Replace("\0","").Trim(), fiscal_modelo, banco_operador, fiscal_last_reducao_cancelamento_ISSQN, "CS",12);
                    for (int x = 0; x < 1; x++)
                    {
                        for (int l = 0; l < 15; l++)
                        {
                            if (Convert.ToDecimal(vetorAliquotaLastZ[x,l].Replace(",",".")) != 0)
                            {
                                conectorPDV_inc_detalhe_reducao(alwaysVariables.Store, fiscal_numero_serie, crz.Replace("\0","").Trim(), nf_ad.Replace("\0","").Trim(), fiscal_modelo, banco_operador, vetorAliquotaLastZ[1, l].Replace(",", "."), (l + 1).ToString().PadLeft(2, '0') + "T" + vetorAliquotaLastZ[0, l].Replace(",",""), 13 + l);
                                conectorPDV_INC_detalhe_reducao_aliquota(alwaysVariables.Store, fiscal_numero_serie, fiscal_last_reducao_dataMovimento.Substring(0, 2) + "/" + fiscal_last_reducao_dataMovimento.Substring(2, 2) + "/" + String.Format("{0:ddMMyyyy}", DateTime.Now).Substring(4, 2) + fiscal_last_reducao_dataMovimento.Substring(4, 2), crz.Replace("\0","").Trim(), vetorAliquotaLastZ[0, l].Replace(",", "."), "t", "0.00", vetorAliquotaLastZ[1, l].Replace(",", "."), l);
                            }
                        }
                    }
                    /* MODO ESTATICO ==> conectorPDV_inc_detalhe_reducao(alwaysVariables.Store, fiscal_numero_serie, crz.Replace("\0","").Trim(), nf_ad.Replace("\0","").Trim(), fiscal_modelo, banco_operador, fiscal_last_reducao_T18, "01T1800");
                    conectorPDV_inc_detalhe_reducao(alwaysVariables.Store, fiscal_numero_serie, crz.Replace("\0","").Trim(), nf_ad.Replace("\0","").Trim(), fiscal_modelo, banco_operador, fiscal_last_reducao_T12, "02T1200");
                    conectorPDV_inc_detalhe_reducao(alwaysVariables.Store, fiscal_numero_serie, crz.Replace("\0","").Trim(), nf_ad.Replace("\0","").Trim(), fiscal_modelo, banco_operador, fiscal_last_reducao_T07, "03T0700");
                    conectorPDV_INC_detalhe_reducao_aliquota(alwaysVariables.Store, fiscal_numero_serie, fiscal_last_reducao_dataMovimento.Substring(0, 2) + "/" + fiscal_last_reducao_dataMovimento.Substring(2, 2) + "/" + String.Format("{0:ddMMyyyy}", DateTime.Now).Substring(4, 2) + fiscal_last_reducao_dataMovimento.Substring(4, 2), crz.Replace("\0","").Trim(), "18", "t", "0.00", fiscal_last_reducao_T18.Replace(",", "."));
                    conectorPDV_INC_detalhe_reducao_aliquota(alwaysVariables.Store, fiscal_numero_serie, fiscal_last_reducao_dataMovimento.Substring(0, 2) + "/" + fiscal_last_reducao_dataMovimento.Substring(2, 2) + "/" + String.Format("{0:ddMMyyyy}", DateTime.Now).Substring(4, 2) + fiscal_last_reducao_dataMovimento.Substring(4, 2), crz.Replace("\0","").Trim(), "12", "t", "0.00", fiscal_last_reducao_T12.Replace(",", "."));
                    conectorPDV_INC_detalhe_reducao_aliquota(alwaysVariables.Store, fiscal_numero_serie, fiscal_last_reducao_dataMovimento.Substring(0, 2) + "/" + fiscal_last_reducao_dataMovimento.Substring(2, 2) + "/" + String.Format("{0:ddMMyyyy}", DateTime.Now).Substring(4, 2) + fiscal_last_reducao_dataMovimento.Substring(4, 2), crz.Replace("\0","").Trim(), "7", "t", "0.00", fiscal_last_reducao_T07.Replace(",", "."));*/

                    conectorPDV_INC_detalhe_reducao_aliquota(alwaysVariables.Store, fiscal_numero_serie, fiscal_last_reducao_dataMovimento.Substring(0, 2) + "/" + fiscal_last_reducao_dataMovimento.Substring(2, 2) + "/" + String.Format("{0:ddMMyyyy}", DateTime.Now).Substring(4, 2) + fiscal_last_reducao_dataMovimento.Substring(4, 2), crz.Replace("\0","").Trim(), "0", "s", "0.00", fiscal_last_reducao_St_ICMS.Replace(",", "."),15);
                    conectorPDV_INC_detalhe_reducao_aliquota(alwaysVariables.Store, fiscal_numero_serie, fiscal_last_reducao_dataMovimento.Substring(0, 2) + "/" + fiscal_last_reducao_dataMovimento.Substring(2, 2) + "/" + String.Format("{0:ddMMyyyy}", DateTime.Now).Substring(4, 2) + fiscal_last_reducao_dataMovimento.Substring(4, 2), crz.Replace("\0","").Trim(), "0", "i", "0.00", fiscal_last_reducao_Isento_ICMS.Replace(",", "."),16);
                }
            }
        }

        public void conectorPDV_aut(string cnpj, string caminho)
        {
            try
            {
                auxConsistencia = 0;
                this.lbErroMainConfig.Invoke((MethodInvoker)delegate { this.lbErroMainConfig.Invoke((MethodInvoker)delegate { lbErroMainConfig.Text = banco.abreConexao(); }); });
                banco.startTransaction("conectorPDV_aut");
                banco.addParametro("find", cnpj);
                banco.procedimentoSet();
            }
            catch (Exception erro)
            {
                msgInfo msg = new msgInfo(1, "Erro não identificado, entre contato como revendedor! " + erro.Message); msg.ShowDialog();
                auxConsistencia = 1;
            }
            finally
            {
                if (auxConsistencia == 0)
                {
                    StreamWriter sw = new StreamWriter(caminho, true, Encoding.ASCII);
                    countField = banco.retornaSet().Tables[0].Columns.Count;
                    countRows = banco.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        matrizGrd = new string[countRows, countField];
                        for (int i = 0; i < countRows; i++)//Linha
                        {
                            for (int j = 0; j < countField; j++) //Coluna
                            {
                                sw.Write(key.GetMd5Sum(banco.retornaSet().Tables[0].Rows[i][j].ToString().Substring(4,20) + "\r\n"));                                
                            }
                        }
                    }
                    sw.Close();
                }
                banco.fechaConexao();
                if (auxConsistencia == 0)
                {
                    cryptografia.encryptFile(alwaysVariables.PAF_total + "\\MD5" + ".txt", "MD5", 2);
                }
            }
            if (File.Exists(caminho))
            {
                File.Delete(caminho);
            }
        }

        public void conectorPDV_ecf_hardware(string find)
        {
            try
            {
                auxConsistencia = 0;
                this.lbErroMainConfig.Invoke((MethodInvoker)delegate { this.lbErroMainConfig.Invoke((MethodInvoker)delegate { lbErroMainConfig.Text = banco.abreConexao(); }); });
                banco.startTransaction("conectorPDV_ecf_hardware");
                banco.addParametro("find", find);
                banco.procedimentoRead();
                if (banco.retornaRead().Read() == true)
                {
                    alwaysVariables.Identificacao_Ecf = banco.retornaRead().GetString(2);
                }
            }
            catch (Exception erro)
            {
                msgInfo msg = new msgInfo(1, "Erro não identificado, entre contato como revendedor! " + erro.Message); msg.ShowDialog();
                auxConsistencia = 1;
            }
            finally
            {
                banco.fechaConexao();
                if (auxConsistencia == 0)
                {
                    
                }
            }
        }

        public void conectorPDV_regra_negocio(string tipo, string find, ref string cx, ref string serie, ref string cgc, ref string ie)
        {
            try
            {
                auxConsistencia = 0;
                this.lbErroMainConfig.Invoke((MethodInvoker)delegate { this.lbErroMainConfig.Invoke((MethodInvoker)delegate { lbErroMainConfig.Text = banco.abreConexao(); }); });
                banco.startTransaction("conectorPDV_regra_negocio");
                banco.addParametro("tipo", tipo);
                banco.addParametro("find", find);
                banco.procedimentoRead();
                if (banco.retornaRead().Read() == true)
                {
                    cx = banco.retornaRead().GetString(0);
                    serie = banco.retornaRead().GetString(1);
                    cgc = banco.retornaRead().GetString(2);
                    ie = banco.retornaRead().GetString(3);
                }
            }
            catch (Exception erro)
            {
                msgInfo msg = new msgInfo(1, "Erro não identificado, entre contato como revendedor! " + erro.Message); msg.ShowDialog();
                auxConsistencia = 1;
            }
            finally
            {
                banco.fechaConexao();
                if (auxConsistencia == 0)
                {

                }
            }
        }
        public void conectorPDV_regra_negocio(string tipo, string find, ref string marca, ref string modelo)
        {
            try
            {
                auxConsistencia = 0;
                this.lbErroMainConfig.Invoke((MethodInvoker)delegate { this.lbErroMainConfig.Invoke((MethodInvoker)delegate { lbErroMainConfig.Text = banco.abreConexao(); }); });
                banco.startTransaction("conectorPDV_regra_negocio");
                banco.addParametro("tipo", tipo);
                banco.addParametro("find", find);
                banco.procedimentoRead();
                if (banco.retornaRead().Read() == true)
                {
                    marca = banco.retornaRead().GetString(0);
                    modelo= banco.retornaRead().GetString(1);
                }
            }
            catch (Exception erro)
            {
                msgInfo msg = new msgInfo(1, "Erro não identificado, entre contato como revendedor! " + erro.Message); msg.ShowDialog();
                auxConsistencia = 1;
            }
            finally
            {
                banco.fechaConexao();
                if (auxConsistencia == 0)
                {

                }
            }
        }

        public void conectorPDV_inc_detalhe_reducao(string store, string numero, string crz, string letra, string modelo, string operador, string valorAcumulado, string totalizador, int count)
        {
            try
            {
                auxConsistencia = 0;
                this.lbErroMainConfig.Invoke((MethodInvoker)delegate { this.lbErroMainConfig.Invoke((MethodInvoker)delegate { lbErroMainConfig.Text = banco.abreConexao(); }); });
                banco.startTransaction("conectorPDV_inc_detalhe_reducao");
                banco.addParametro("inc_idloja", store);
                banco.addParametro("inc_numeroCaixa", numero.Replace("\0","").Trim());
                banco.addParametro("inc_crz", crz.Replace("\0","").Trim());
                banco.addParametro("inc_mf_letra", letra.Replace("\0","").Trim());
                banco.addParametro("inc_modelo", modelo.Replace("\0","").Trim());
                banco.addParametro("inc_operador", operador);
                if (Convert.ToDecimal(valorAcumulado.Replace(",", ".")) == 0)
                {
                    banco.addParametro("inc_valorAcumulado", "0");
                }
                else
                {
                    banco.addParametro("inc_valorAcumulado", valorAcumulado.Replace(",", "."));
                }
                banco.addParametro("inc_totalizador", totalizador);
                banco.procedimentoRead();
            }
            catch (Exception erro)
            {
                msgInfo msg = new msgInfo(1, "Erro não identificado, entre contato como revendedor! " + erro.Message); msg.ShowDialog();
                auxConsistencia = 1;
            }
            finally
            {
                banco.fechaConexao();
                if (auxConsistencia == 0)
                {
                    if (Convert.ToDecimal(valorAcumulado.Replace(",", ".")) > 0)
                    {
                        strCupom = "";
                        instrucao.setVetorCupom();
                        strCupom = instrucao.getMapaRegistro(alwaysVariables.UserName, alwaysVariables.Senha, Convert.ToDouble(fiscal_store.Replace("\0", "").Trim()) > 0 ? fiscal_store.Replace("\0", "").Trim() : alwaysVariables.Store, crz.Replace("\0","").Trim(), String.Format("{0:yyyyMMdd}", DateTime.Now), numero.Replace("\0","").Trim(), count.ToString());
                        instrucao.carregaInstrucaoMovimentoVenda(instrucao._movimentoResumo, strCupom,0);
                        //instrucao.compactScript(instrucao._movimentoResumo, "mapa_resumo");
                    }
                }
            }
        }

        public void conectorPDV_INC_detalhe_reducao_aliquota(string store, string numero, string movimento, string crz, string aliquota, string tipo, string reducao, string valor, int count)
        {
            try
            {
                auxConsistencia = 0;
                this.lbErroMainConfig.Invoke((MethodInvoker)delegate { this.lbErroMainConfig.Invoke((MethodInvoker)delegate { lbErroMainConfig.Text = banco.abreConexao(); }); });
                banco.startTransaction("conectorPDV_INC_detalhe_reducao_aliquota");
                banco.addParametro("inc_idloja", store);
                banco.addParametro("inc_numeroCaixa", numero.Replace("\0","").Trim());
                banco.addParametro("inc_movimento", String.Format("{0:yyyyMMdd}", (movimento)));
                banco.addParametro("inc_crz", crz.Replace("\0","").Trim());
                banco.addParametro("inc_aliquota",aliquota);
                banco.addParametro("inc_tipo", tipo);
                banco.addParametro("inc_reducao", reducao);
                banco.addParametro("inc_valor", valor);
                if (tipo == "i" || tipo == "s")
                {
                    valor = "0";
                }
                banco.addParametro("inc_base", valor);
                banco.procedimentoRead();
            }
            catch (Exception erro)
            {
                msgInfo msg = new msgInfo(1, "Erro não identificado, entre contato como revendedor! " + erro.Message); msg.ShowDialog();
                auxConsistencia = 1;
            }
            finally
            {
                banco.fechaConexao();
                if (auxConsistencia == 0)
                {
                    strCupom = "";
                    instrucao.setVetorCupom();
                    strCupom = instrucao.getAliquota(alwaysVariables.UserName, alwaysVariables.Senha, Convert.ToDouble(fiscal_store.Replace("\0", "").Trim()) > 0 ? fiscal_store.Replace("\0", "").Trim() : alwaysVariables.Store, crz.Replace("\0","").Trim(), String.Format("{0:yyyyMMdd}",  (movimento)), numero.Replace("\0","").Trim(),count.ToString());
                    instrucao.carregaInstrucaoMovimentoVenda(instrucao._aliquotas, strCupom,0);
                    //instrucao.compactScript(instrucao._aliquotas, "aliquota");
                }
            }
        }
        public void carrega_crediario(string id)
        {
            try
            {
                auxConsistencia = 0;
                this.lbErroMainConfig.Invoke((MethodInvoker)delegate { this.lbErroMainConfig.Invoke((MethodInvoker)delegate { lbErroMainConfig.Text = banco.abreConexao(); }); });
                if (banco.statusSchema() == 1)
                {
                    banco.startTransaction("conectorPDV_find_paramentro_crediario");
                    banco.addParametro("find", id);
                    banco.procedimentoRead();
                    if (banco.retornaRead().Read() == true)
                    {
                        alwaysVariables.dayQuitacaoAfter = Convert.ToInt32(banco.retornaRead().GetString(1));
                        alwaysVariables.dayQuitacaoBefore = Convert.ToInt32(banco.retornaRead().GetString(2));
                        alwaysVariables.IndiceSingleday = Convert.ToDecimal(banco.retornaRead().GetString(3));
                        alwaysVariables.IndiceAtrasoMora = Convert.ToDecimal(banco.retornaRead().GetString(4));
                        alwaysVariables.CarenciaSingleDay = Convert.ToInt32(banco.retornaRead().GetString(5));
                        alwaysVariables.CarenciaSingleMora = Convert.ToInt32(banco.retornaRead().GetString(6));
                        alwaysVariables.IdadeSpc = banco.retornaRead().GetString(7);
                        alwaysVariables.AltValuePrestacao = banco.retornaRead().GetString(8);
                        alwaysVariables.AltValueEntrada = banco.retornaRead().GetString(9);
                        alwaysVariables.LogicaCredito = banco.retornaRead().GetString(10);
                        alwaysVariables.DescontoMaximoPrestacao = Convert.ToDecimal(banco.retornaRead().GetString(11));
                        alwaysVariables.LiberacaocaoCredito = banco.retornaRead().GetString(12);
                        alwaysVariables.LimiteRendaBase = banco.retornaRead().GetString(13);
                        alwaysVariables.CategoriaLimite = banco.retornaRead().GetString(14);
                        alwaysVariables.VariacaoLimite = banco.retornaRead().GetString(15);
                        alwaysVariables.TrocoCard = banco.retornaRead().GetString(16);
                        alwaysVariables.Recebimento = banco.retornaRead().GetString(17);
                        alwaysVariables.Adm = banco.retornaRead().GetString(18);
                        alwaysVariables.flagG = banco.retornaRead().GetString(19);
                        alwaysVariables.alquitoPis = banco.retornaRead().GetString(21);
                        alwaysVariables.alquitoCofins = banco.retornaRead().GetString(22);
                        alwaysVariables.TipoAmbiente = banco.retornaRead().GetString(23);
                        alwaysVariables.RazaoHomoloNfe = banco.retornaRead().GetString(24);
                        alwaysVariables.FlagParametro = banco.retornaRead().GetString(25);
                        alwaysVariables.flagHomologacao = banco.retornaRead().GetString(26);
                        alwaysVariables.MD5VALIDO = banco.retornaRead().GetString(27);
                        alwaysVariables.gavetaEcf = Convert.ToInt32(banco.retornaRead().GetString(28));

                    }
                }
                else
                {
                    auxConsistencia = 1;
                }

            }
            catch (Exception erro)
            {
                msgInfo msg = new msgInfo(1, "Erro não identificado, entre contato como revendedor! " + erro.Message); msg.ShowDialog();
                auxConsistencia = 1;
            }
            finally
            {
                banco.fechaConexao();
            }

        }
        public void conectorPDV_serie(string caminho)
        {
            StreamWriter sw = new StreamWriter(caminho, true, Encoding.ASCII);
            //Venda Bruta 
            functionECF.conectorECF_VendaBruta(alwaysVariables.ModeloEcf, ref fiscal_vendaBruta_last_reducaoZ, ref fiscal_MSG, ref fiscal_retorno, alwaysVariables.ECF_Ligada);
            //CRZ
            functionECF.conectorECF_NumeroReducoes(alwaysVariables.ModeloEcf, ref fiscal_NumReducoes, ref fiscal_MSG, ref fiscal_retorno, alwaysVariables.ECF_Ligada);
            //CRO
            functionECF.conectorECF_NumeroIntervencoes(alwaysVariables.ModeloEcf, ref fiscal_last_reducao_cro, ref fiscal_MSG, ref fiscal_retorno, alwaysVariables.ECF_Ligada);
            //Serie
            functionECF.conectorECF_numero_serie(alwaysVariables.ModeloEcf, ref fiscal_numero_serie, ref fiscal_MSG, ref fiscal_retorno, alwaysVariables.ECF_Ligada);
            //fiscal_retorno = conectorECF.Bematech_FI_NumeroSerieMFD(ref fiscal_numero_serie);
            functionECF.conectorECF_GrandeTotal_Crypt(alwaysVariables.ModeloEcf, ref fiscal_GT_Crypt, ref fiscal_MSG, ref fiscal_retorno, alwaysVariables.ECF_Ligada);

            auxConsistencia = 0;
            countField = 0;
            countRows = 0;
            try
            {
                this.lbErroMainConfig.Invoke((MethodInvoker)delegate { this.lbErroMainConfig.Invoke((MethodInvoker)delegate { lbErroMainConfig.Text = banco.abreConexao(); }); });
                banco.startTransaction("conectorPDV_serie");
                if (fiscal_retorno == 1)
                {
                    banco.procedimentoSet();
                }
                else
                {
                    auxConsistencia = fiscal_retorno;
                }


            }
            catch (Exception erro)
            {
                auxConsistencia = 1;
            }
            finally
            {
                if (fiscal_retorno == 1)
                {
                    countField = banco.retornaSet().Tables[0].Columns.Count;
                    countRows = banco.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        matrizGrd = new string[countRows, countField];
                        for (int i = 0; i < countRows; i++)//Linha
                        {
                            for (int j = 0; j < countField; j++) //Coluna
                            {
                                matrizGrd[i, j] = Convert.ToString(banco.retornaSet().Tables[0].Rows[i][j]);
                            }
                            sw.Write(export.registro_serie(matrizGrd[i, 0], matrizGrd[i, 1], "0", fiscal_NumReducoes, fiscal_last_reducao_cro, fiscal_vendaBruta_last_reducaoZ + "\r\n"));
                        }
                    }
                    banco.fechaConexao();
                    if (auxConsistencia == 0)
                    {
                        sw.Close();
                        mensagemLinha = new List<string>();
                        using (StreamReader texto = new StreamReader(caminho))
                        {

                            while ((mensagem = texto.ReadLine()) != null)
                            {
                                if (mensagem.Substring(4, 20) == fiscal_numero_serie)
                                {
                                    mensagem = mensagem.Substring(0, 24) + fiscal_GT_Crypt;
                                    mensagemLinha.Add(mensagem);
                                }
                                else
                                {
                                    mensagemLinha.Add(mensagem);
                                }
                            }

                            texto.Close();

                            if (File.Exists(folderMFDGrand + "\\grandFullPDV" + ".txt"))
                            {
                                File.Delete(folderMFDGrand + "\\grandFullPDV" + ".txt");
                            }
                            StreamWriter final = new StreamWriter(folderMFDGrand + "\\grandFullPDV" + ".txt", true, Encoding.UTF8);

                            for (int j = 0; j < mensagemLinha.Count; j++)
                            {
                                final.Write(export.registro_serie_unica(mensagemLinha[j], fiscal_NumReducoes, fiscal_last_reducao_cro.Replace("\r", "").Replace("\n", "").Replace("\0", "").Trim(), fiscal_vendaBruta_last_reducaoZ + "\r\n"));
                            }
                            final.Close();

                            cryptografia.encryptFile(alwaysVariables.PAF_total + "\\grandFullPDV" + ".txt", "grandFullPDV", 2);
                            msgInfo msg = new msgInfo(1, "Caro Usuário: Arquivo gerado, caminho: " + folderMFDGrand + "\\grandFullPDV" + ".enc"); msg.ShowDialog();
                        }
                    }
                }
                else
                {
                    msgInfo msg = new msgInfo(1, "Falha na geração do arquivo, caminho: " + folderMFDGrand + "\\grandFullPDV" + ".enc"); msg.ShowDialog();
                }
            }
            sw.Close();
        }
        public int numero_pdv()
        {
            int retorno = 0; //0 para erro de conexao; 1 exits conexao;
            auxConsistencia = 0;
            try
            {
                this.lbErroMainConfig.Invoke((MethodInvoker)delegate { this.lbErroMainConfig.Invoke((MethodInvoker)delegate { lbErroMainConfig.Text = banco.abreConexao(); }); });
                banco.singleTransaction("select tab.nr_pdv from nr_pdv tab");
                banco.procedimentoRead();
                if (banco.retornaRead().Read() == true)
                {
                    retorno = Convert.ToInt32(banco.retornaRead().GetString(0));
                }
                else retorno = 0;
            }
            catch (Exception erro)
            {
                retorno = 0;
                auxConsistencia = 1;
            }
            finally
            {
                banco.fechaConexao();
                if (auxConsistencia == 0)
                {
                    alwaysVariables.Terminal = retorno.ToString();
                }
            }
            return retorno;
        }

        public void agrupa_liberacao(int cx)
        {
            try
            {
                auxConsistencia = 0;
                this.lbErroMainConfig.Invoke((MethodInvoker)delegate { this.lbErroMainConfig.Invoke((MethodInvoker)delegate { lbErroMainConfig.Text = banco.abreConexao(); }); });
                banco.singleTransaction("delete from liberacao where nr_pdv != ?Var");
                banco.addParametro("?Var", cx.ToString());
                if (cx > 0)
                {
                    banco.procedimentoRead();
                    if (banco.retornaRead().Read() == true)
                    {
                    }
                }
            }
            catch (Exception erro)
            {
                auxConsistencia = 1;
            }
            finally
            {
                banco.fechaConexao();
                if (auxConsistencia == 0)
                {
                    
                }
            }
        }
        public void conectorPDV_find_operador()
        {
            try
            {
                if (banco.statusSchema() == 0)
                {
                    banco.fechaConexao();
                }
                auxConsistencia = 0;
                this.lbErroMainConfig.Invoke((MethodInvoker)delegate { this.lbErroMainConfig.Invoke((MethodInvoker)delegate { lbErroMainConfig.Text = banco.abreConexao(); }); });
                banco.startTransaction("conectorPDV_find_operador");
                banco.addParametro("store", alwaysVariables.Store);
                if (Convert.ToDouble(fiscal_data_movimento.Replace("\0", "").Trim() == "" ? "0" : fiscal_data_movimento) > 0)
                {
                    banco.addParametro("find", String.Format("{0:yyyyMMdd}", Convert.ToDateTime(fiscal_data_movimento.Substring(0, 2) + "/" + fiscal_data_movimento.Substring(2, 2) + "/" + String.Format("{0:ddMMyyyy}", DateTime.Now).Substring(4, 2) + fiscal_data_movimento.Substring(4, 2))));
                }
                else if (Convert.ToDouble(fiscal_data_printer.Replace("\0", "").Trim() == "" ? "0" : fiscal_data_printer) > 0)
                {
                    banco.addParametro("find", String.Format("{0:yyyyMMdd}", Convert.ToDateTime(fiscal_data_printer.Substring(0, 2) + "/" + fiscal_data_printer.Substring(2, 2) + "/" + String.Format("{0:ddMMyyyy}", DateTime.Now).Substring(4, 2) + fiscal_data_printer.Substring(4, 2))));
                }
                else
                {
                    banco.addParametro("find", String.Format("{0:yyyyMMdd}", DateTime.Now));
                }
                banco.addParametro("pdv", fiscal_numero_caixa.Replace("\0","").Trim());
                banco.procedimentoRead();
                if (banco.retornaRead().Read() == true)
                {
                    alwaysVariables.Usuario = banco_operador = banco.retornaRead().GetString(0);
                    banco_sequencia = banco.retornaRead().GetString(1);
                }
                else
                {
                    banco_operador = "0";
                }
            }
            catch (Exception erro)
            { msgInfo msg = new msgInfo(1, "Caro Cliente - " + "ERRO FATAL - CONTACTE O SEU ADMINISTRADOR"); msg.ShowDialog(); auxConsistencia = 1; }
            finally
            {
                banco.fechaConexao();
                if (auxConsistencia == 0)
                {
                    alwaysVariables.Usuario = banco_operador;
                }
            }
        }
        public int find_info_banco(string varCx, string varStore)
        {
            auxConsistencia = 0;
            try
            {
                this.lbErroMainConfig.Invoke((MethodInvoker)delegate { this.lbErroMainConfig.Invoke((MethodInvoker)delegate { lbErroMainConfig.Text = banco.abreConexao(); }); });
                statusBanco = banco.statusSchema();
                banco.singleTransaction(strComando);
                banco.addParametro("?VarCx", varCx.Replace("\0",""));
                banco.addParametro("?VarStore", alwaysVariables.Store);
                if (Convert.ToDouble(varCx) > 0)
                {
                    banco.procedimentoRead();
                    if (banco.retornaRead().Read() == true)
                    {
                        banco_numero_caixa = banco.retornaRead().GetString(0);
                        this.lbPdvMainConfig.Invoke((MethodInvoker)delegate { lbPdvMainConfig.Text = banco.retornaRead().GetString(6); });
                        this.lbDataLiberacaoMainConfig.Invoke((MethodInvoker)delegate { lbDataLiberacaoMainConfig.Text = banco.retornaRead().GetString(1); });
                        this.lbDataExpiracaoMainConfig.Invoke((MethodInvoker)delegate { lbDataExpiracaoMainConfig.Text = banco.retornaRead().GetString(2); });
                        dataAgora = DateTime.Now;
                        dataliberacao = new DateTime(Convert.ToInt32(String.Format("{0:yyyyMMdd}", banco.retornaRead().GetString(9).Replace("-", "").Replace("/", "").Substring(0, 4))), Convert.ToInt32(String.Format("{0:yyyyMMdd}", banco.retornaRead().GetString(9).Replace("-", "").Replace("/", "").Substring(4, 2))), Convert.ToInt32(String.Format("{0:yyyyMMdd}", banco.retornaRead().GetString(9).Replace("-", "").Replace("/", "").Substring(6, 2))));
                        ts = dataAgora.Subtract(dataliberacao);
                        banco_serie = banco.retornaRead().GetString(4);
                        this.lbIpTerminalMainConfig.Invoke((MethodInvoker)delegate { lbIpTerminalMainConfig.Text = banco.retornaRead().GetString(3); });
                        alwaysVariables.Autentica = banco.retornaRead().GetString(7);
                        alwaysVariables.typeTef = banco.retornaRead().GetString(10);
                    }
                }
            }
            catch (Exception erro)
            {
                if (varCx == "    " || varCx == "0000")
                {
                    this.lbErroMainConfig.Invoke((MethodInvoker)delegate { lbErroMainConfig.Text = "NUMERO DO TERMINAL INVÁLIDO, REINICIE NOVAMENTE O SOFTWARE...!"; });
                }
                else
                {
                    this.lbErroMainConfig.Invoke((MethodInvoker)delegate { lbErroMainConfig.Text = erro.ToString(); });
                    auxConsistencia = 1;
                }
            }
            finally
            {
                banco.fechaConexao();
                if (auxConsistencia ==0)
                {
                    try
                    {
                        if (Convert.ToInt32(banco_numero_caixa) > 0)
                        {
                            alwaysVariables.CHAVE_CAIXA = chave_caixa(banco_numero_caixa);
                        }
                    }
                    catch (Exception erro)
                    {
                        this.lbErroMainConfig.Invoke((MethodInvoker)delegate { this.lbErroMainConfig.Invoke((MethodInvoker)delegate { lbErroMainConfig.Text = erro.ToString(); }); });
                        //lbErroMainConfig.Text = erro.ToString();
                        auxConsistencia = 1;
                    }
                }
            }
            if (auxConsistencia == 0)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
        public int liberacao()
        {
            int retorno = 0; //0 para erro de conexao; 1 exits conexao;
            try
            {
                this.lbErroMainConfig.Invoke((MethodInvoker)delegate { this.lbErroMainConfig.Invoke((MethodInvoker)delegate { lbErroMainConfig.Text = banco.abreConexao(); }); });
                banco.singleTransaction("select count(tab1.nr_pdv) from nr_pdv tab, liberacao tab1 where tab.nr_pdv=tab1.nr_pdv");
                banco.procedimentoRead();
                if (banco.retornaRead().Read() == true)
                {
                    retorno = Convert.ToInt32(banco.retornaRead().GetString(0));
                }
                else retorno = 0;
            }
            catch (Exception erro)
            {
                retorno = 0;
            }
            finally
            {
                banco.fechaConexao();
            }
            return retorno;
        }
        public int verifica_movimento_last(string VarLj, string VarCx, string Vardt)
        {
            int retorno = 0; //0 para erro de conexao; 1 exits conexao;
            try
            {
                this.lbErroMainConfig.Invoke((MethodInvoker)delegate { this.lbErroMainConfig.Invoke((MethodInvoker)delegate { lbErroMainConfig.Text = banco.abreConexao(); }); });
                banco.singleTransaction("select count(*) from movimentoDia where idLoja=?VarLj and numeroCaixa=?VarCx and movimento=?Vardt");
                banco.addParametro("?VarLj", VarLj);
                banco.addParametro("?VarCx", VarCx.Replace("\0","").Trim());
                banco.addParametro("?Vardt", Vardt);
                banco.procedimentoRead();
                if (banco.retornaRead().Read() == true)
                {
                    retorno = Convert.ToInt32(banco.retornaRead().GetString(0));
                }
                else retorno = 0;
            }
            catch (Exception erro)
            {
                retorno = 0;
            }
            finally
            {
                banco.fechaConexao();
            }
            return retorno;
        }

        public int chave_caixa(string nr_caixa)
        {
            int retorno = 0; //0 para erro de conexao; 1 exits conexao;
            try
            {
                this.lbErroMainConfig.Invoke((MethodInvoker)delegate { this.lbErroMainConfig.Invoke((MethodInvoker)delegate { lbErroMainConfig.Text = banco.abreConexao(); }); });
                banco.singleTransaction("select terminal from nr_pdv tab  inner join terminalecfconfig tab1 on(tab.nr_pdv = tab1.caixa) where nr_pdv=?VarCx");
                banco.addParametro("?VarCx", nr_caixa);
                banco.procedimentoRead();
                if (banco.retornaRead().Read() == true)
                {
                    retorno = Convert.ToInt32(banco.retornaRead().GetString(0));
                }
                else retorno = 0;
            }
            catch (Exception erro)
            {
                retorno = 0;
            }
            finally
            {
                banco.fechaConexao();
            }
            return retorno;
        }
        void instrucao_file()
        {
            File.Delete(folderMFDGrand + "\\grandFullPDV" + ".enc");
        }
        public void paramentroBanco()
        {
            int retorno = 0; //0 para erro de conexao; 1 exits conexao;
            try
            {
                this.lbErroMainConfig.Invoke((MethodInvoker)delegate { this.lbErroMainConfig.Invoke((MethodInvoker)delegate { lbErroMainConfig.Text = banco.abreConexao(); }); });
                banco.singleTransaction("select count(tab1.nr_pdv) from nr_pdv tab, liberacao tab1 where tab.nr_pdv=tab1.nr_pdv");
                banco.procedimentoRead();
                if (banco.retornaRead().Read() == true)
                {
                    retorno = Convert.ToInt32(banco.retornaRead().GetString(0));
                }
            }
            catch (Exception erro)
            {
            }
            finally
            {
                banco.fechaConexao();
            }
        }
        void conector_find_carrega_const(string chave)
        {
            try
            {
                this.lbErroMainConfig.Invoke((MethodInvoker)delegate { this.lbErroMainConfig.Invoke((MethodInvoker)delegate { lbErroMainConfig.Text = banco.abreConexao(); }); });
                banco.startTransaction("conectorPDV_find_terminalConfig");
                banco.addParametro("find", chave);
                banco.procedimentoRead();
                if (banco.retornaRead().Read() == true)
                {
                    alwaysVariables.Terminal = banco.retornaRead().GetString(1);
                    alwaysVariables.IpCaixa = banco.retornaRead().GetString(2);
                    alwaysVariables.AberturaTroco = banco.retornaRead().GetString(3);
                    alwaysVariables.ImprimiCheque = banco.retornaRead().GetString(4); ;
                    alwaysVariables.TimeBlock = banco.retornaRead().GetString(5); ;
                    alwaysVariables.BlockTime = banco.retornaRead().GetString(6);
                    alwaysVariables.TrocaMercadoria = banco.retornaRead().GetString(7);
                    alwaysVariables.CarneRecebe = banco.retornaRead().GetString(8);
                    alwaysVariables.CodigoEmpresaTef = banco.retornaRead().GetString(9);
                    alwaysVariables.TrocoMax = banco.retornaRead().GetString(10);
                    alwaysVariables.Serie = banco.retornaRead().GetString(11); //.Substring(0,15);
                    alwaysVariables.Serie_Hash = key.GetMd5Sum(banco.retornaRead().GetString(11));
                    alwaysVariables.UtilizaTeclado = banco.retornaRead().GetString(12);
                    alwaysVariables.UtilizaTef = banco.retornaRead().GetString(13);
                    alwaysVariables.UtilizaBalanca = banco.retornaRead().GetString(14);
                    alwaysVariables.UtilizaEcf = banco.retornaRead().GetString(15);
                    alwaysVariables.PortTef = banco.retornaRead().GetString(16);
                    alwaysVariables.PortBalanca = banco.retornaRead().GetString(17);
                    alwaysVariables.PortEcf = banco.retornaRead().GetString(18);
                    alwaysVariables.FuncaoProgramada = banco.retornaRead().GetString(19);
                    alwaysVariables.MsgTef = banco.retornaRead().GetString(20);
                    alwaysVariables.ModeloPrinter = banco.retornaRead().GetString(21);
                    alwaysVariables.StatusPDV = banco.retornaRead().GetString(22);
                    alwaysVariables.Autentica = banco.retornaRead().GetString(23);
                    alwaysVariables.EmitiVinculo = banco.retornaRead().GetString(24);
                    alwaysVariables.VinculoCrediario = banco.retornaRead().GetString(25);
                    alwaysVariables.VinculoConvenio = banco.retornaRead().GetString(26);
                    alwaysVariables.VinculoCartaoCf = banco.retornaRead().GetString(27);
                    alwaysVariables.VinculoCartaoDb = banco.retornaRead().GetString(28);
                    alwaysVariables.AlertaSangria = banco.retornaRead().GetString(30);
                    alwaysVariables.ValueSangria = banco.retornaRead().GetString(31);
                }

            }
            catch (Exception erro)
            {
                msgInfo msg = new msgInfo(1, "Erro não identificado, entre contato como revendedor! " + erro.Message); msg.ShowDialog();
                auxConsistencia = 1;
            }
            finally 
            {
                banco.fechaConexao();
               if(cryptografia.conectorPDV_aut_serial() == false)
               {  
                   msgInfo msg = new msgInfo(1, "Caro Cliente - " + "ERRO DE LEITURA MD5.ENC NÃO ENCONTRADO! UM NOVO ARQUIVO SERÁ GERADO...!"); msg.ShowDialog();
               }
            }
        }
        //#########################################################End Procedimento de Banco#################################################
        //#############################################################Declaração de Metodos#################################################
        
        public char Chr(int codigo)
        {
            return (char)codigo;
        }
        public int getStatus(int value)
        {
            fiscal_MSG = "";
  
            switch (value)
            {
                case 0:
                    if (alwaysVariables.ModoOperacao == 1)
                    {
                        functionECF.conectorECF_HabilitaDesabilitaRetornoEstendidoMFD(alwaysVariables.ModeloEcf, "1", ref fiscal_MSG, ref fiscal_retorno);
                        //fiscal_retorno = conectorECF.Bematech_FI_HabilitaDesabilitaRetornoEstendidoMFD("1");
                        functionECF.conectorECF_VerificaImpressoraLigada(alwaysVariables.ModeloEcf, ref fiscal_MSG, ref fiscal_retorno);
                        //fiscal_retorno= conectorECF.Bematech_FI_VerificaImpressoraLigada();
                        //conectorECF.Analisa_iRetorno(fiscal_retorno, ref fiscal_MSG);
                        if (fiscal_retorno > 0)
                        {
                            this.lbStatusMainConfig.Invoke((MethodInvoker)delegate { lbStatusMainConfig.Text = "LIGADA"; });
                            //lbStatusMainConfig.Text = "LIGADA.";
                            //valida++;
                        }
                        else
                        {
                            //lbStatusMainConfig.Text = "DESLIGADA.";
                            this.lbStatusMainConfig.Invoke((MethodInvoker)delegate { lbStatusMainConfig.Text = "DESLIGADA"; });
                            alwaysVariables.ECF_Ligada = fiscal_retorno;
                        }
                        this.lbErroMainConfig.Invoke((MethodInvoker)delegate { lbErroMainConfig.Text = fiscal_MSG; });
                    }
                    else
                    {
                        lbStatusMainConfig.Text = "ON-LINE";
                        //valida++;
                    }
                    if (alwaysVariables.ECF_Ligada != -6)//Impressora Ligada
                    {
                        this.lblProcesso.Invoke((MethodInvoker)delegate { lblProcesso.Text = "[ 1 ]"; });
                        valida++;
                    }
                    else
                    {
                        this.lblProcessoErros.Invoke((MethodInvoker)delegate { lblProcessoErros.Text = "[ 1 ]"; });
                    }
                    break;
                case 1: //Confere o numero de serie;
                    functionECF.conectorECF_VerificaImpressoraLigada(alwaysVariables.ModeloEcf, ref fiscal_MSG, ref fiscal_retorno);
                    if (alwaysVariables.ModoOperacao == 1)
                    {
                        if (alwaysVariables.ECF_Ligada != -6)//Impressora Ligada
                        {
                            fiscal_flag = 0;
                            functionECF.conectorECF_fiscal_flag(alwaysVariables.ModeloEcf, ref fiscal_flag, ref fiscal_MSG, ref fiscal_retorno, alwaysVariables.ECF_Ligada);
                            if (fiscal_flag != 32 && fiscal_flag != 33 && fiscal_flag != 35 && fiscal_flag != 37 && fiscal_flag != 36)
                            {
                                if (fiscal_flag == 0)
                                {
                                    super.conector_saidas_cupom();
                                    super.conector_saidas_cupom(1);
                                    super.conector_saidas_cupom(2);
                                    super.conector_saidas_cupom(3);
                                }

                                if (fiscal_dtsoft_basico.Trim() == "")
                                {
                                    functionECF.conectorECF_DataHoraGravacaoUsuarioSWBasicoMFAdicional(alwaysVariables.ModeloEcf, ref fiscal_dtusuario_last, ref fiscal_dtsoft_basico, ref fiscal_letramf_adicional, ref fiscal_MSG, ref fiscal_retorno, alwaysVariables.ECF_Ligada);
                                }
                                //fiscal_retorno = conectorECF.Bematech_FI_DataHoraGravacaoUsuarioSWBasicoMFAdicional(ref fiscal_dtusuario_last, ref fiscal_dtsoft_basico, ref fiscal_letramf_adicional);
                                fiscal_retorno = 1; // ERRO QUANDO CUPOM ABERTO
                                if (fiscal_retorno == 1)
                                {
                                    functionECF.conectorECF_MarcaModeloTipoImpressoraMFD(alwaysVariables.ModeloEcf, ref fiscal_marca, ref fiscal_modelo, ref fiscal_tipo_ecf, ref fiscal_MSG, ref fiscal_retorno, alwaysVariables.ECF_Ligada);
                                    alwaysVariables.ECF_MODELO_CRYP = key.GetMd5Sum((fiscal_modelo.Replace("\0", "").Trim()));
                                    //fiscal_retorno = conectorECF.Bematech_FI_MarcaModeloTipoImpressoraMFD(ref fiscal_marca, ref fiscal_modelo, ref fiscal_tipo_ecf);   
                                }
                                else
                                {
                                    fiscal_retorno = 1;
                                    this.lblObs.Invoke((MethodInvoker)delegate { lblObs.Text = "MARCA/MODELO"; });
                                    this.lbErroMainConfig.Invoke((MethodInvoker)delegate { lbErroMainConfig.Text = "MARCA E MODELO DIFERENTE DO CONFIGURADO!"; });
                                    this.lblProcesso.Invoke((MethodInvoker)delegate { lblProcesso.Text = "[ #2 ]"; });
                                }
                                if (fiscal_retorno == 1)
                                {
                                    fiscal_numero_serie = new string('\x20', 20);
                                    functionECF.conectorECF_numero_serie(alwaysVariables.ModeloEcf, ref fiscal_numero_serie, ref fiscal_MSG, ref fiscal_retorno, alwaysVariables.ECF_Ligada);
                                    //fiscal_retorno = conectorECF.Bematech_FI_NumeroSerieMFD(ref fiscal_numero_serie);
                                    if (fiscal_numero_serie.Trim() != "")
                                    {
                                        if (fiscal_retorno == 1)
                                        {
                                            functionECF.conectorECF_VendaBruta(alwaysVariables.ModeloEcf, ref fiscal_vendaBruta_last_reducaoZ, ref fiscal_MSG, ref fiscal_retorno, alwaysVariables.ECF_Ligada);
                                            //fiscal_retorno = conectorECF.Bematech_FI_VendaBruta(ref fiscal_vendaBruta_last_reducaoZ);
                                        }
                                        else
                                        {
                                            this.lbErroMainConfig.Invoke((MethodInvoker)delegate { lbErroMainConfig.Text = "VENDA BRUTA NÃO CONFERIDA!"; });
                                        }
                                        fiscal_MSG = "";
                                        alwaysVariables.Serie_Hash_Hard = key.GetMd5Sum(fiscal_numero_serie);
                                        conectorPDV_ecf_hardware(fiscal_numero_serie.Replace("\0", "").Trim());
                                    }
                                    else
                                    {//lbNumeroSerieMainConfig
                                        this.lbNumeroSerieMainConfig.Invoke((MethodInvoker)delegate { lbNumeroSerieMainConfig.Text = "DESCONHECIDA"; });
                                        this.lblProcesso.Invoke((MethodInvoker)delegate { lblProcesso.Text = "[ 2 ]"; });
                                    }
                                }
                                else
                                {
                                    this.lbNumeroSerieMainConfig.Invoke((MethodInvoker)delegate { lbNumeroSerieMainConfig.Text = "DESCONHECIDA"; });
                                    this.lblProcesso.Invoke((MethodInvoker)delegate { lblProcesso.Text = "[ 2 ]"; });
                                }
                                if (fiscal_retorno == 1)
                                {
                                    functionECF.conectorECF_VersaoFirmwareMFD(alwaysVariables.ModeloEcf, ref fiscal_VersaoFirmware, ref fiscal_MSG, ref fiscal_retorno, alwaysVariables.ECF_Ligada);
                                    //fiscal_retorno = conectorECF.Bematech_FI_VersaoFirmwareMFD(ref fiscal_VersaoFirmware);
                                }
                                if (fiscal_retorno == 1)
                                {
                                    functionECF.conectorECF_NumeroSubstituicoesProprietario(alwaysVariables.ModeloEcf, ref fiscal_NumeroSubstituicoesProprietario, ref fiscal_MSG, ref fiscal_retorno, alwaysVariables.ECF_Ligada);
                                    //fiscal_retorno = conectorECF.Bematech_FI_NumeroSubstituicoesProprietario(ref fiscal_NumeroSubstituicoesProprietario);
                                }
                                if (fiscal_retorno == 1)
                                {
                                    functionECF.conectorECF_DataHoraReducao(alwaysVariables.ModeloEcf, ref fiscal_hora_last_reducaoZ, ref fiscal_data_last_reducaoZ, ref fiscal_MSG, ref fiscal_retorno, alwaysVariables.ECF_Ligada);
                                    if (fiscal_data_last_reducaoZ.Trim() == "")
                                    {
                                        fiscal_MSG = "";
                                        try
                                        {
                                            this.lbErroMainConfig.Invoke((MethodInvoker)delegate { this.lbErroMainConfig.Invoke((MethodInvoker)delegate { lbErroMainConfig.Text = banco.abreConexao(); }); });
                                            banco.singleTransaction("select ifnull(max(crz),0), ifnull(date_format(movimento,'%d%m%y'),date_format(now(),'%d%m%y')) from conectorPDV.movimentoDia");
                                            banco.procedimentoRead();
                                            if (banco.retornaRead().Read() == true)
                                            {
                                                fiscal_data_last_reducaoZ = banco.retornaRead().GetString(1);
                                            }
                                            else fiscal_data_last_reducaoZ = String.Format("{0:ddMMyy}", DateTime.Now);
                                        }
                                        catch (Exception erro)
                                        {
                                            this.lbMsgMainConfig.Invoke((MethodInvoker)delegate { lbMsgMainConfig.Text = "ERRO ENTRADA DE DADOS INVALIDA...!"; });
                                        }
                                        finally
                                        {
                                            banco.fechaConexao();
                                            fiscal_retorno = 0;
                                            this.lblProcesso.Invoke((MethodInvoker)delegate { lblProcesso.Text = "[ 2 ]"; });
                                            valida++;
                                        }
                                    }
                                    else
                                    {
                                        this.lbErroMainConfig.Invoke((MethodInvoker)delegate { lbErroMainConfig.Text = "NOT FOUND. REDUÇÃO Z!"; });
                                        this.lblProcessoErros.Invoke((MethodInvoker)delegate { lblProcessoErros.Text = "[ #2 ]"; });
                                    }
                                    //fiscal_retorno = conectorECF.Bematech_FI_DataHoraReducao(ref fiscal_data_last_reducaoZ,ref fiscal_hora_last_reducaoZ);
                                }
                                if (fiscal_retorno == 1)
                                {
                                    functionECF.conectorECF_DadosUltimaReducaoMFD(alwaysVariables.ModeloEcf, ref fiscal_DadosUltimaReducaoMFD, ref fiscal_MSG, ref fiscal_retorno, alwaysVariables.ECF_Ligada);
                                    if (fiscal_retorno == 1 && fiscal_DadosUltimaReducaoMFD.Trim().Length > 0)
                                    {
                                        fiscal_retorno = 1;
                                        fiscal_last_reducao_grandTotal = fiscal_DadosUltimaReducaoMFD.Substring(315, 18).Insert(16, ",");

                                        vetorAliquotaLastZ[0, 0] = fiscal_DadosUltimaReducaoMFD.Substring(1207, 4).Insert(2, ",");
                                        vetorAliquotaLastZ[0, 1] = fiscal_DadosUltimaReducaoMFD.Substring(1211, 4).Insert(2, ",");
                                        vetorAliquotaLastZ[0, 2] = fiscal_DadosUltimaReducaoMFD.Substring(1215, 4).Insert(2, ",");
                                        vetorAliquotaLastZ[0, 3] = fiscal_DadosUltimaReducaoMFD.Substring(1219, 4).Insert(2, ",");
                                        vetorAliquotaLastZ[0, 4] = fiscal_DadosUltimaReducaoMFD.Substring(1223, 4).Insert(2, ",");
                                        vetorAliquotaLastZ[0, 5] = fiscal_DadosUltimaReducaoMFD.Substring(1227, 4).Insert(2, ",");
                                        vetorAliquotaLastZ[0, 6] = fiscal_DadosUltimaReducaoMFD.Substring(1231, 4).Insert(2, ",");
                                        vetorAliquotaLastZ[0, 7] = fiscal_DadosUltimaReducaoMFD.Substring(1235, 4).Insert(2, ",");
                                        vetorAliquotaLastZ[0, 8] = fiscal_DadosUltimaReducaoMFD.Substring(1239, 4).Insert(2, ",");
                                        vetorAliquotaLastZ[0, 9] = fiscal_DadosUltimaReducaoMFD.Substring(1243, 4).Insert(2, ",");
                                        vetorAliquotaLastZ[0, 10] = fiscal_DadosUltimaReducaoMFD.Substring(1247, 4).Insert(2, ",");
                                        vetorAliquotaLastZ[0, 11] = fiscal_DadosUltimaReducaoMFD.Substring(1251, 4).Insert(2, ",");
                                        vetorAliquotaLastZ[0, 12] = fiscal_DadosUltimaReducaoMFD.Substring(1255, 4).Insert(2, ",");
                                        vetorAliquotaLastZ[0, 13] = fiscal_DadosUltimaReducaoMFD.Substring(1259, 4).Insert(2, ",");
                                        vetorAliquotaLastZ[0, 14] = fiscal_DadosUltimaReducaoMFD.Substring(1263, 4).Insert(2, ",");

                                        vetorAliquotaLastZ[1, 0] = fiscal_last_reducao_T18 = fiscal_DadosUltimaReducaoMFD.Substring(334, 14).Insert(12, ",");
                                        vetorAliquotaLastZ[1, 1] = fiscal_last_reducao_T12 = fiscal_DadosUltimaReducaoMFD.Substring(348, 14).Insert(12, ",");
                                        vetorAliquotaLastZ[1, 2] = fiscal_last_reducao_T07 = fiscal_DadosUltimaReducaoMFD.Substring(362, 14).Insert(12, ",");
                                        vetorAliquotaLastZ[1, 3] = fiscal_DadosUltimaReducaoMFD.Substring(376, 14).Insert(12, ",");
                                        vetorAliquotaLastZ[1, 4] = fiscal_DadosUltimaReducaoMFD.Substring(390, 14).Insert(12, ",");
                                        vetorAliquotaLastZ[1, 5] = fiscal_DadosUltimaReducaoMFD.Substring(404, 14).Insert(12, ",");
                                        vetorAliquotaLastZ[1, 6] = fiscal_DadosUltimaReducaoMFD.Substring(418, 14).Insert(12, ",");
                                        vetorAliquotaLastZ[1, 7] = fiscal_DadosUltimaReducaoMFD.Substring(432, 14).Insert(12, ",");
                                        vetorAliquotaLastZ[1, 8] = fiscal_DadosUltimaReducaoMFD.Substring(446, 14).Insert(12, ",");
                                        vetorAliquotaLastZ[1, 9] = fiscal_DadosUltimaReducaoMFD.Substring(460, 14).Insert(12, ",");
                                        vetorAliquotaLastZ[1, 10] = fiscal_DadosUltimaReducaoMFD.Substring(474, 14).Insert(12, ",");
                                        vetorAliquotaLastZ[1, 11] = fiscal_DadosUltimaReducaoMFD.Substring(488, 14).Insert(12, ",");
                                        vetorAliquotaLastZ[1, 12] = fiscal_DadosUltimaReducaoMFD.Substring(502, 14).Insert(12, ",");
                                        vetorAliquotaLastZ[1, 13] = fiscal_DadosUltimaReducaoMFD.Substring(516, 14).Insert(12, ",");
                                        vetorAliquotaLastZ[1, 14] = fiscal_DadosUltimaReducaoMFD.Substring(530, 14).Insert(12, ",");

                                        fiscal_last_reducao_coo = fiscal_DadosUltimaReducaoMFD.Substring(13, 6);
                                        fiscal_last_reducao_cro = fiscal_DadosUltimaReducaoMFD.Substring(3, 4);
                                        fiscal_last_reducao_crz = fiscal_DadosUltimaReducaoMFD.Substring(8, 4);
                                        cryptografia = new crypt(fiscal_last_reducao_crz, fiscal_last_reducao_cro);
                                        fiscal_last_reducao_dataMovimento = fiscal_DadosUltimaReducaoMFD.Substring(1272, 6);
                                        fiscal_last_reducao_ValorAcumulado = fiscal_DadosUltimaReducaoMFD.Substring(315, 18).Insert(16, ",");
                                        fiscal_last_reducao_Isento_ICMS = fiscal_DadosUltimaReducaoMFD.Substring(559, 14).Insert(12, ",");
                                        fiscal_last_reducao_naoIncide_ICMS = fiscal_DadosUltimaReducaoMFD.Substring(574, 14).Insert(12, ",");
                                        fiscal_last_reducao_St_ICMS = fiscal_DadosUltimaReducaoMFD.Substring(589, 14).Insert(12, ",");
                                        fiscal_last_reducao_Isento_ISSQN = fiscal_DadosUltimaReducaoMFD.Substring(604, 14).Insert(12, ",");
                                        fiscal_last_reducao_naoIncide_ISSQN = fiscal_DadosUltimaReducaoMFD.Substring(619, 14).Insert(12, ",");
                                        fiscal_last_reducao_St_ISSQN = fiscal_DadosUltimaReducaoMFD.Substring(634, 14).Insert(12, ",");
                                        fiscal_last_reducao_desconto_ICMS = fiscal_DadosUltimaReducaoMFD.Substring(649, 14).Insert(12, ",");
                                        fiscal_last_reducao_desconto_ISSQN = fiscal_DadosUltimaReducaoMFD.Substring(664, 14).Insert(12, ",");
                                        fiscal_last_reducao_acrescimo_ICMS = fiscal_DadosUltimaReducaoMFD.Substring(679, 14).Insert(12, ",");
                                        fiscal_last_reducao_acrescimo_ISSQN = fiscal_DadosUltimaReducaoMFD.Substring(694, 14).Insert(12, ",");
                                        fiscal_last_reducao_cancelamento_ICMS = fiscal_DadosUltimaReducaoMFD.Substring(709, 14).Insert(12, ",");
                                        fiscal_last_reducao_cancelamento_ISSQN = fiscal_DadosUltimaReducaoMFD.Substring(724, 14).Insert(12, ",");
                                        fiscal_last_reducao_parcial_not_icms = fiscal_DadosUltimaReducaoMFD.Substring(739, 14).Insert(12, ",");
                                        fiscal_last_reducao_sangria = fiscal_DadosUltimaReducaoMFD.Substring(754, 14).Insert(12, ",");
                                        fiscal_last_reducao_suprimento = fiscal_DadosUltimaReducaoMFD.Substring(769, 14).Insert(12, ",");
                                        fiscal_last_reducao_cancelamento_not_fiscal = fiscal_DadosUltimaReducaoMFD.Substring(784, 14).Insert(12, ",");
                                        fiscal_last_reducao_desconto_not_fiscal = fiscal_DadosUltimaReducaoMFD.Substring(799, 14).Insert(12, ",");
                                        fiscal_last_reducao_acrescimo_not_fiscal = fiscal_DadosUltimaReducaoMFD.Substring(814, 14).Insert(12, ",");

                                        //functionECF.conectorECF_DataHoraPrinter(alwaysVariables.ModeloEcf, ref fiscal_data_printer, ref fiscal_hora_printer, ref fiscal_MSG, ref fiscal_retorno);
                                        if (fiscal_data_printer.Trim() == "")
                                        {
                                            functionECF.conectorECF_DataHoraReducao(alwaysVariables.ModeloEcf, ref fiscal_data_printer, ref fiscal_hora_printer, ref fiscal_MSG, ref fiscal_retorno, alwaysVariables.ECF_Ligada);
                                            if (fiscal_data_printer.Trim() == "")
                                            {
                                                fiscal_data_printer = String.Format("{0:ddMMyy}", DateTime.Now);
                                            }
                                        }
                                    }
                                    if (fiscal_retorno == 1 && fiscal_vendaBruta_last_reducaoZ.Trim() == "")
                                    {
                                        functionECF.conectorECF_VendaBruta(alwaysVariables.ModeloEcf, ref fiscal_vendaBruta_last_reducaoZ, ref fiscal_MSG, ref fiscal_retorno, alwaysVariables.ECF_Ligada);
                                        //fiscal_retorno = conectorECF.Bematech_FI_VendaBruta(ref fiscal_vendaBruta_last_reducaoZ);
                                    }
                                    if (fiscal_retorno == 1)
                                    {
                                        functionECF.conectorECF_ArredondaTrunca(alwaysVariables.ModeloEcf, ref fiscal_trunca_arredonda, 1, ref fiscal_MSG, ref fiscal_retorno, alwaysVariables.ECF_Ligada);
                                        //fiscal_retorno = conectorECF.Bematech_FI_VerificaTruncamento(ref fiscal_trunca_arredonda);
                                    }
                                }
                                functionECF.conectorECF_NumeroReducoes(alwaysVariables.ModeloEcf, ref fiscal_NumReducoes, ref fiscal_MSG, ref fiscal_retorno, alwaysVariables.ECF_Ligada);
                                if (fiscal_NumReducoes == "0000" && fiscal_retorno != 1)
                                {//Primeira inicialização
                                    fiscal_retorno = 1;
                                }
                                //conectorECF.Analisa_iRetorno(fiscal_retorno, ref fiscal_MSG);
                                if (fiscal_retorno == 1)
                                {
                                    fiscal_MSG = "";
                                    if (File.Exists(alwaysVariables.PAF_total + "\\grandFullPDV" + ".enc"))
                                    {
                                        functionECF.conectorECF_GrandeTotal(alwaysVariables.ModeloEcf, ref fiscal_GT, ref fiscal_MSG, ref fiscal_retorno, alwaysVariables.ECF_Ligada);
                                        //fiscal_retorno = conectorECF.Bematech_FI_GrandeTotal(ref fiscal_GT);
                                        if (fiscal_retorno == 1 && fiscal_GT.Trim() != "")
                                        {
                                            //fiscal_GT_Crypt = cryptografia.descryptFile(folderMFDGrand + "\\grandFullPDV" + ".enc", "\\grandFullPDV").Substring(0, 19) + "  ";
                                            // mensagem = cryptografia.descryptFile(folderMFDGrand + "\\grandFullPDV" + ".enc", "\\grandFullPDV").Replace("\0", "").Replace("\n", "").Replace("\r", "").Trim();
                                            cryptografia.descryptFile(folderMFDGrand + "\\grandFullPDV" + ".enc", "\\grandFullPDV");
                                            File.Delete(folderMFDGrand + "\\temp" + ".txt");
                                            if (alwaysVariables.ArqTotal.Count > 0)
                                            {
                                                functionECF.conectorECF_GrandeTotal(alwaysVariables.ModeloEcf, ref fiscal_GT, ref fiscal_MSG, ref fiscal_retorno, alwaysVariables.ECF_Ligada);
                                                //fiscal_retorno = conectorECF.Bematech_FI_NumeroSerieMFD(ref fiscal_numero_serie);
                                                if (fiscal_retorno == 1)
                                                {
                                                    for (int j = 0; j < alwaysVariables.ArqTotal.Count; j++)
                                                    {
                                                        if (alwaysVariables.ArqTotal[j].Length >= 24 && j == 0)
                                                        {
                                                            string test = alwaysVariables.ArqTotal[j].Substring(4, 20);
                                                            if ((alwaysVariables.ArqTotal[j].Substring(4, 20) == fiscal_numero_serie) || fiscal_numero_serie.Trim() == "EMULADOR")
                                                            {
                                                                string bof = alwaysVariables.ArqTotal[j].Substring(24, alwaysVariables.ArqTotal[j].Length - 24);
                                                                functionECF.conectorECF_GrandeTotalDescriptografado(alwaysVariables.ModeloEcf, alwaysVariables.ArqTotal[j].Substring(24, alwaysVariables.ArqTotal[j].Length - 24), ref fiscal_GT_compare, ref fiscal_MSG, ref fiscal_retorno, alwaysVariables.ECF_Ligada);
                                                                //fiscal_retorno = conectorECF.Bematech_FI_GrandeTotalDescriptografado();

                                                                if (fiscal_GT == fiscal_GT_compare || alwaysVariables.Serie.Trim() == fiscal_numero_serie.Trim())
                                                                {
                                                                    this.lbNumeroSerieMainConfig.Invoke((MethodInvoker)delegate { lbNumeroSerieMainConfig.Text = fiscal_numero_serie; });
                                                                    this.lblProcesso.Invoke((MethodInvoker)delegate { lblProcesso.Text = "[ 2 ]"; });
                                                                    valida++;
                                                                }
                                                                else
                                                                {
                                                                    this.lblObs.Invoke((MethodInvoker)delegate { lblObs.Text = "ERRO N.o SÉRIE"; });   
                                                                    this.lbErroMainConfig.Invoke((MethodInvoker)delegate { lbErroMainConfig.Text = "NUMERO DE SÉRIE INVÁLIDO, DIFERENTE DO INFORMADO...!"; });
                                                                    this.lblProcessoErros.Invoke((MethodInvoker)delegate { lblProcessoErros.Text = "[ 2 ]"; });
                                                                    valida++;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                instrucao_file();
                                                            }
                                                        }
                                                        else
                                                        {
                                                            this.lblObs.Invoke((MethodInvoker)delegate { lblObs.Text = "GT NÃO LOCALIZADO"; });   
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    this.lblObs.Invoke((MethodInvoker)delegate { lblObs.Text = "FALHA NO GT"; });   
                                                }
                                            }
                                            else
                                            {
                                                if (fiscal_vendaBruta_last_reducaoZ == "000000000000000000" && fiscal_GT == "000000000000000000" &&
                                                    fiscal_GT_Crypt.Trim() == "" && fiscal_last_reducao_cro == "0000" && fiscal_last_reducao_crz == "0000" && fiscal_vendaBruta_last_reducaoZ == "000000000000000000")
                                                {
                                                    this.lblProcesso.Invoke((MethodInvoker)delegate { lblProcesso.Text = "[ 2 ]"; });
                                                    valida++;
                                                }
                                                else
                                                {
                                                    this.lblProcessoErros.Invoke((MethodInvoker)delegate { lblProcessoErros.Text = "[ 2 ]"; });
                                                }
                                            }

                                            if (fiscal_GT_Crypt.Trim() != "")
                                            {
                                                if (fiscal_GT_Crypt == "0000000000000000000  ")
                                                {
                                                    msgInfo msg = new msgInfo(1, "Caro Cliente - " + "GRANDE INVÁLIDO OU CORROMPIDO! SOLICITE A AUTORIZAÇÃO PARA GERAÇÃO DO GRANDE TOTAL"); msg.ShowDialog();
                                                }
                                                else
                                                {
                                                    functionECF.conectorECF_GrandeTotalDescriptografado(alwaysVariables.ModeloEcf, fiscal_GT_Crypt, ref fiscal_GT_compare, ref fiscal_MSG, ref fiscal_retorno, alwaysVariables.ECF_Ligada);
                                                    //fiscal_retorno = conectorECF.Bematech_FI_GrandeTotalDescriptografado(fiscal_GT_Crypt, ref fiscal_GT_compare);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            ///Se existe o arquivo na pasta PAF-ECF
                                            if (File.Exists(alwaysVariables.PAF_total + "\\grandFullPDV" + ".enc"))
                                            {
                                                this.lbErroMainConfig.Invoke((MethodInvoker)delegate { lbErroMainConfig.Text = "TESTE DE VERIFICAÇÃO DO GRANDE TOTAL INCONSISTENTE, UMA NOVA VERIFICAÇÃO PODERÁ SER REALIZADA LOGO EM BREVE!"; });
                                                valida++;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        this.lbErroMainConfig.Invoke((MethodInvoker)delegate { lbErroMainConfig.Text = "CONFERINDO NUMERO DE SERIE...!"; });
                                        conectorPDV_serie(folderMFDGrand + "\\grandFullPDV" + ".txt");
                                    }
                                }
                                else
                                {
                                    this.lbNumeroSerieMainConfig.Invoke((MethodInvoker)delegate { lbNumeroSerieMainConfig.Text = fiscal_MSG; });
                                }
                                if (fiscal_MSG != "")
                                {
                                    this.lbErroMainConfig.Invoke((MethodInvoker)delegate { lbErroMainConfig.Text = fiscal_MSG; });
                                }
                            }
                            else
                            {
                                conectorPDV_regra_negocio("1", "", ref fiscal_numero_caixa, ref fiscal_numero_serie, ref fiscal_CGC, ref fiscal_IE);
                                if (fiscal_numero_serie.Trim() != "")
                                {
                                    alwaysVariables.Serie_Hash_Hard = key.GetMd5Sum(fiscal_numero_serie);
                                    conectorPDV_ecf_hardware(fiscal_numero_serie.Replace("\0", "").Trim());
                                }
                                if (fiscal_data_printer.Trim() == "")
                                {
                                    fiscal_data_printer = String.Format("{0:ddMMyy}", DateTime.Now);
                                }
                                this.lblProcesso.Invoke((MethodInvoker)delegate { lblProcesso.Text = "[ 2 ]"; });
                                valida++;
                            }
                        }
                        else
                        {
                            conectorPDV_regra_negocio("1", "", ref fiscal_numero_caixa, ref fiscal_numero_serie, ref fiscal_CGC, ref fiscal_IE);
                            if (fiscal_numero_serie.Trim() != "")
                            {
                                alwaysVariables.Serie_Hash_Hard = key.GetMd5Sum(fiscal_numero_serie);
                                conectorPDV_ecf_hardware(fiscal_numero_serie.Replace("\0", "").Trim());
                            }
                            if (fiscal_data_printer.Trim() == "")
                            {
                                fiscal_data_printer = String.Format("{0:ddMMyy}", DateTime.Now);
                            }
                            this.lblProcesso.Invoke((MethodInvoker)delegate { lblProcesso.Text = "[ 2 ]"; });
                            valida++;
                        }
                    }
                    else
                    {
                        lbNumeroSerieMainConfig.Text = "VirtualECF";
                        valida++;
                    }

                    break;
                case 2: //Confere loja cadastrada na intervenção
                    if (alwaysVariables.ModoOperacao == 1)
                    {
                        if (alwaysVariables.ECF_Ligada != -6)//Impressora Ligada
                        {
                            functionECF.conectorECF_NumeroLoja(alwaysVariables.ModeloEcf, ref fiscal_store, ref fiscal_MSG, ref fiscal_retorno, alwaysVariables.ECF_Ligada);
                            //fiscal_retorno = conectorECF.Bematech_FI_NumeroLoja(ref fiscal_store);
                            //conectorECF.Analisa_iRetorno(fiscal_retorno, ref fiscal_MSG);
                            try
                            {
                                functionECF.conectorECF_CGC_IE(alwaysVariables.ModeloEcf, ref fiscal_CGC, ref fiscal_IE, ref fiscal_MSG, ref fiscal_retorno, alwaysVariables.ECF_Ligada);
                            }
                            catch (Exception)
                            {

                            }

                            if (fiscal_retorno == 1)
                            {
                                //cadastrar loja
                                if (alwaysVariables.CNPJ == fiscal_CGC.Replace(",", "").Replace(".", "").Replace("-", "").Replace("/", "").Replace("\0", "").Trim())
                                {
                                    this.lbLojaMainConfig.Invoke((MethodInvoker)delegate { lbLojaMainConfig.Text = fiscal_store; });
                                    //lbLojaMainConfig.Text = fiscal_store;
                                    this.lblProcesso.Invoke((MethodInvoker)delegate { lblProcesso.Text = "[ 3.0 ]"; });
                                    valida++;
                                }
                                else
                                {//Liberar pos Tratar com impressora nova
                                    this.lbErroMainConfig.Invoke((MethodInvoker)delegate { this.lbErroMainConfig.Text = fiscal_MSG; });
                                    this.lblProcessoErros.Invoke((MethodInvoker)delegate { lblProcessoErros.Text = "[ 3.0 ]"; });
                                    //lbErroMainConfig.Text = fiscal_MSG;
                                }
                            }
                            else
                            {
                                this.lbLojaMainConfig.Invoke((MethodInvoker)delegate { lbLojaMainConfig.Text = "NÃO LOCALIZADA."; });
                                this.lblProcessoErros.Invoke((MethodInvoker)delegate { lblProcessoErros.Text = "[ 3.0 ]"; });
                            }
                            this.lbErroMainConfig.Invoke((MethodInvoker)delegate { this.lbErroMainConfig.Invoke((MethodInvoker)delegate { lbErroMainConfig.Text = fiscal_MSG; }); });
                        }
                        else
                        {
                            conectorPDV_regra_negocio("1", "", ref fiscal_numero_caixa, ref fiscal_numero_serie, ref fiscal_CGC, ref fiscal_IE);
                            valida++;
                        }
                    }
                    else
                    {
                        lbLojaMainConfig.Text = alwaysVariables.Store;
                        valida++;
                    }
                    break;
                 case 3: //Confere o numero do caixa cadastrado na intervenção
                    if (alwaysVariables.ModoOperacao == 1)
                    {
                        if (alwaysVariables.ECF_Ligada != -6)//Impressora Ligada
                        {

                            functionECF.conectorECF_NumeroCaixa(alwaysVariables.ModeloEcf, ref fiscal_numero_caixa, ref fiscal_MSG, ref fiscal_retorno, alwaysVariables.ECF_Ligada);
                            //fiscal_retorno = conectorECF.Bematech_FI_NumeroCaixa(ref fiscal_numero_caixa);
                            //conectorECF.Analisa_iRetorno(fiscal_retorno, ref fiscal_MSG);
                            if (fiscal_retorno == 1)
                            {
                                if (fiscal_numero_caixa.Trim() != "")
                                {
                                    if (Convert.ToInt32(alwaysVariables.Terminal) == Convert.ToInt32(fiscal_numero_caixa))
                                    {
                                        this.lbCaixaMainConfig.Invoke((MethodInvoker)delegate { lbCaixaMainConfig.Text = fiscal_numero_caixa; });
                                        alwaysVariables.Terminal = fiscal_numero_caixa;
                                        this.lblProcesso.Invoke((MethodInvoker)delegate { lblProcesso.Text = "[ 3.1 ]"; });
                                        valida++;
                                    }
                                    else
                                    {
                                        this.lbErroMainConfig.Invoke((MethodInvoker)delegate { lbErroMainConfig.Text = "NUMERO DO CAIXA CADASTRADO, DIFERENTE DO INFORMADO...!"; });
                                        this.lblProcessoErros.Invoke((MethodInvoker)delegate { lblProcessoErros.Text = "[ 3.1 ]"; });
                                    }
                                }
                                else
                                {
                                    this.lbErroMainConfig.Invoke((MethodInvoker)delegate { lbErroMainConfig.Text = "NUMERO DO CAIXA INVÁLIDO...!"; });
                                }
                            }
                            else
                            {
                                this.lbCaixaMainConfig.Invoke((MethodInvoker)delegate { lbCaixaMainConfig.Text = "NÃO LOCALIZADO"; });
                            }
                            //this.lblProcessoErros.Invoke((MethodInvoker)delegate { lblProcessoErros.Text = "[ 3 ]"; });
                            //this.lbErroMainConfig.Invoke((MethodInvoker)delegate { lbErroMainConfig.Text = fiscal_MSG; });
                        }
                        else
                        {
                            conectorPDV_regra_negocio("1", "", ref fiscal_numero_caixa, ref fiscal_numero_serie, ref fiscal_CGC, ref fiscal_IE);
                            this.lbCaixaMainConfig.Invoke((MethodInvoker)delegate { lbCaixaMainConfig.Text = fiscal_numero_caixa; });
                            alwaysVariables.Terminal = fiscal_numero_caixa;
                            this.lblProcesso.Invoke((MethodInvoker)delegate { lblProcesso.Text = "[ 3.1 ]"; });
                            valida++;
                        }
                    }
                    else
                    {
                        this.lbCaixaMainConfig.Invoke((MethodInvoker)delegate { lbCaixaMainConfig.Text = alwaysVariables.Terminal; });
                    }
                    break;
                 case 4://Confere se a movimento, senão configuração inicial para pdv, ok abre cupom
                    if (alwaysVariables.ModoOperacao == 1)
                    {
                        if (alwaysVariables.ECF_Ligada != -6)//Impressora Ligada
                        {

                            functionECF.conectorECF_DataMovimento(alwaysVariables.ModeloEcf, ref fiscal_data_movimento, ref fiscal_MSG, ref fiscal_retorno, alwaysVariables.ECF_Ligada);
                            //fiscal_retorno = conectorECF.Bematech_FI_DataMovimento(ref fiscal_data_movimento);
                            //conectorECF.Analisa_iRetorno(fiscal_retorno, ref fiscal_MSG);
                            if (fiscal_retorno == 1)
                            {
                                if (Convert.ToDouble(fiscal_data_movimento.Replace("\0", "").Trim() == "" ? "0" : fiscal_data_movimento) > 0)
                                {
                                    this.lbMovimentoMainConfig.Invoke((MethodInvoker)delegate { lbMovimentoMainConfig.Text = fiscal_data_movimento.Substring(0, 2) + "/" + fiscal_data_movimento.Substring(2, 2) + "/" + String.Format("{0:ddMMyyyy}", DateTime.Now).Substring(4, 2) + fiscal_data_movimento.Substring(4, 2); });
                                    valida++;
                                }
                                else
                                {
                                    this.lbMovimentoMainConfig.Invoke((MethodInvoker)delegate { lbMovimentoMainConfig.Text = "00/00/0000"; });
                                    this.lblProcesso.Invoke((MethodInvoker)delegate { lblProcesso.Text = "[ 4 ]"; });
                                    valida++;
                                }
                            }
                            else
                            {
                                this.lbMovimentoMainConfig.Invoke((MethodInvoker)delegate { lbMovimentoMainConfig.Text = "00/00/0000"; });
                            }
                            this.lbErroMainConfig.Invoke((MethodInvoker)delegate { lbErroMainConfig.Text = fiscal_MSG; });
                            
                        }
                        else
                        {
                            this.lblProcesso.Invoke((MethodInvoker)delegate { lblProcesso.Text = "[ 4 ]"; });
                            this.lblProcessoErros.Invoke((MethodInvoker)delegate { lblProcessoErros.Text = "[ 4 ]"; });
                            valida++;
                        }
                    }
                    else
                    {
                        this.lbMovimentoMainConfig.Invoke((MethodInvoker)delegate { lbMovimentoMainConfig.Text = "00/00/0000"; });
                        this.lblProcesso.Invoke((MethodInvoker)delegate { lblProcesso.Text = "[ 4 ]"; });
                        valida++;
                    }
                    break;
                  case 5: //Confere data e hora do ECF

                    if (alwaysVariables.ModoOperacao == 1)
                    {
                        if (alwaysVariables.ECF_Ligada != -6)//Impressora Ligada
                        {

                            functionECF.conectorECF_DataHoraPrinter(alwaysVariables.ModeloEcf, ref fiscal_data_printer, ref fiscal_hora_printer, ref fiscal_MSG, ref fiscal_retorno, alwaysVariables.ECF_Ligada);
                            //fiscal_retorno = conectorECF.Bematech_FI_DataHoraImpressora(ref fiscal_data_printer, ref fiscal_hora_printer);
                            //conectorECF.Analisa_iRetorno(fiscal_retorno, ref fiscal_MSG);
                            if (fiscal_retorno == 1)
                            {
                                this.lbDataImpressoraMainConfig.Invoke((MethodInvoker)delegate { lbDataImpressoraMainConfig.Text = fiscal_data_printer.Substring(0, 2) + "/" + fiscal_data_printer.Substring(2, 2) + "/" + String.Format("{0:ddMMyyyy}", DateTime.Now).Substring(4, 2) + fiscal_data_printer.Substring(4, 2); });
                                this.lbHoraImpressoraMainConfig.Invoke((MethodInvoker)delegate { lbHoraImpressoraMainConfig.Text = fiscal_hora_printer.Substring(0, 2) + ":" + fiscal_hora_printer.Substring(2, 2); });
                                this.lbDataSistemaMainConfig.Invoke((MethodInvoker)delegate { lbDataSistemaMainConfig.Text = DateTime.Now.ToShortDateString(); });
                                this.lbHoraSistemaMainConfig.Invoke((MethodInvoker)delegate { lbHoraSistemaMainConfig.Text = DateTime.Now.ToShortTimeString(); });
                                this.lblProcesso.Invoke((MethodInvoker)delegate { lblProcesso.Text = "[ 5 ]"; });
                                valida++;
                            }
                            else
                            {
                                this.lbDataImpressoraMainConfig.Invoke((MethodInvoker)delegate { lbDataImpressoraMainConfig.Text = DateTime.Now.ToShortDateString(); });
                                this.lbHoraImpressoraMainConfig.Invoke((MethodInvoker)delegate { lbHoraImpressoraMainConfig.Text = DateTime.Now.ToShortTimeString(); });
                                this.lbDataSistemaMainConfig.Invoke((MethodInvoker)delegate { lbDataSistemaMainConfig.Text = DateTime.Now.ToShortDateString(); });
                                this.lbHoraSistemaMainConfig.Invoke((MethodInvoker)delegate { lbHoraSistemaMainConfig.Text = DateTime.Now.ToShortTimeString(); });
                                this.lblProcessoErros.Invoke((MethodInvoker)delegate { lblProcessoErros.Text = "[ 5 ]"; });
                            }
                            this.lbErroMainConfig.Invoke((MethodInvoker)delegate { lbErroMainConfig.Text = fiscal_MSG; });
                            //lbErroMainConfig.Text = fiscal_MSG;
                        }
                        else
                        {
                            fiscal_data_printer = String.Format("{0:ddMMyy}", DateTime.Now);
                            this.lblProcesso.Invoke((MethodInvoker)delegate { lblProcesso.Text = "[ 5 ]"; });
                            valida++;
                        }
                    }
                    else
                    {
                        this.lblProcesso.Invoke((MethodInvoker)delegate { lblProcesso.Text = "[ 5 ]"; });
                        valida++;
                    }
                    break;
                case 6:
                    if (alwaysVariables.ModoOperacao == 1)
                    {
                        if (alwaysVariables.ECF_Ligada != -6)//Impressora Ligada
                        {
                            //fiscal_retorno = conectorECF.Bematech_FI_DataMovimento(ref fiscal_data_movimento);
                            functionECF.conectorECF_DataMovimento(alwaysVariables.ModeloEcf, ref fiscal_data_movimento, ref fiscal_MSG, ref fiscal_retorno, alwaysVariables.ECF_Ligada);
                            // conectorECF.Analisa_iRetorno(fiscal_retorno, ref fiscal_MSG);
                            if (fiscal_retorno == 1)
                            {
                                if (Convert.ToDouble(fiscal_data_movimento == "      " ? fiscal_data_movimento == "000000" ? "0" : fiscal_data_movimento : fiscal_data_movimento) <= 0)
                                {
                                    functionECF.conectorECF_ProgramaFormaPagamentoMFD(alwaysVariables.ModeloEcf, "Dinheiro", "0", ref fiscal_MSG, ref fiscal_retorno, alwaysVariables.ECF_Ligada);
                                    //fiscal_retorno = conectorECF.Bematech_FI_ProgramaFormaPagamentoMFD("Dinheiro", "0");
                                    //conectorECF.Analisa_iRetorno(fiscal_retorno, ref fiscal_MSG);
                                    this.lbErroMainConfig.Invoke((MethodInvoker)delegate { lbErroMainConfig.Text = fiscal_MSG; });
                                    functionECF.conectorECF_ProgramaFormaPagamentoMFD(alwaysVariables.ModeloEcf, "Cheque", "1", ref fiscal_MSG, ref fiscal_retorno, alwaysVariables.ECF_Ligada);
                                    //fiscal_retorno = conectorECF.Bematech_FI_ProgramaFormaPagamentoMFD("Cheque", "1");
                                    //conectorECF.Analisa_iRetorno(fiscal_retorno, ref fiscal_MSG);
                                    this.lbErroMainConfig.Invoke((MethodInvoker)delegate { lbErroMainConfig.Text = fiscal_MSG; });
                                    functionECF.conectorECF_ProgramaFormaPagamentoMFD(alwaysVariables.ModeloEcf, "Crediario", "1", ref fiscal_MSG, ref fiscal_retorno, alwaysVariables.ECF_Ligada);
                                    //fiscal_retorno = conectorECF.Bematech_FI_ProgramaFormaPagamentoMFD("Crediario", "1");
                                    //conectorECF.Analisa_iRetorno(fiscal_retorno, ref fiscal_MSG);
                                    this.lbErroMainConfig.Invoke((MethodInvoker)delegate { lbErroMainConfig.Text = fiscal_MSG; });
                                    functionECF.conectorECF_ProgramaFormaPagamentoMFD(alwaysVariables.ModeloEcf, "Convenio", "1", ref fiscal_MSG, ref fiscal_retorno, alwaysVariables.ECF_Ligada);
                                    //fiscal_retorno = conectorECF.Bematech_FI_ProgramaFormaPagamentoMFD("Convenio", "1");
                                    //conectorECF.Analisa_iRetorno(fiscal_retorno, ref fiscal_MSG);
                                    this.lbErroMainConfig.Invoke((MethodInvoker)delegate { lbErroMainConfig.Text = fiscal_MSG; });
                                    functionECF.conectorECF_ProgramaFormaPagamentoMFD(alwaysVariables.ModeloEcf, "Cartao Credito", "1", ref fiscal_MSG, ref fiscal_retorno, alwaysVariables.ECF_Ligada);
                                    //fiscal_retorno = conectorECF.Bematech_FI_ProgramaFormaPagamentoMFD("Cartao Credito", "1");
                                    //conectorECF.Analisa_iRetorno(fiscal_retorno, ref fiscal_MSG);
                                    this.lbErroMainConfig.Invoke((MethodInvoker)delegate { lbErroMainConfig.Text = fiscal_MSG; });
                                    functionECF.conectorECF_ProgramaFormaPagamentoMFD(alwaysVariables.ModeloEcf, "Cartao Debito", "1", ref fiscal_MSG, ref fiscal_retorno, alwaysVariables.ECF_Ligada);
                                    //fiscal_retorno = conectorECF.Bematech_FI_ProgramaFormaPagamentoMFD("Cartao Debito", "1");
                                    //conectorECF.Analisa_iRetorno(fiscal_retorno, ref fiscal_MSG);
                                    this.lbErroMainConfig.Invoke((MethodInvoker)delegate { lbErroMainConfig.Text = fiscal_MSG; });
                                    functionECF.conectorECF_ProgramaFormaPagamentoMFD(alwaysVariables.ModeloEcf, "Boleto", "0", ref fiscal_MSG, ref fiscal_retorno, alwaysVariables.ECF_Ligada);
                                    //fiscal_retorno = conectorECF.Bematech_FI_ProgramaFormaPagamentoMFD("Boleto", "0");
                                    //conectorECF.Analisa_iRetorno(fiscal_retorno, ref fiscal_MSG);
                                    this.lbErroMainConfig.Invoke((MethodInvoker)delegate { lbErroMainConfig.Text = fiscal_MSG; });
                                    functionECF.conectorECF_ProgramaFormaPagamentoMFD(alwaysVariables.ModeloEcf, "Duplicata", "0", ref fiscal_MSG, ref fiscal_retorno, alwaysVariables.ECF_Ligada);
                                    //fiscal_retorno = conectorECF.Bematech_FI_ProgramaFormaPagamentoMFD("Duplicata", "0");
                                    //conectorECF.Analisa_iRetorno(fiscal_retorno, ref fiscal_MSG);
                                    this.lbErroMainConfig.Invoke((MethodInvoker)delegate { lbErroMainConfig.Text = fiscal_MSG; });
                                    functionECF.conectorECF_ProgramaFormaPagamentoMFD(alwaysVariables.ModeloEcf, "Recebimento", "0", ref fiscal_MSG, ref fiscal_retorno, alwaysVariables.ECF_Ligada);
                                    //fiscal_retorno = conectorECF.Bematech_FI_ProgramaFormaPagamentoMFD("Recebimento", "0");
                                    //conectorECF.Analisa_iRetorno(fiscal_retorno, ref fiscal_MSG);
                                    this.lbErroMainConfig.Invoke((MethodInvoker)delegate { lbErroMainConfig.Text = fiscal_MSG; });
                                    functionECF.conectorECF_ProgramaFormaPagamentoMFD(alwaysVariables.ModeloEcf, "Vale", "0", ref fiscal_MSG, ref fiscal_retorno, alwaysVariables.ECF_Ligada);
                                    //fiscal_retorno = conectorECF.Bematech_FI_ProgramaFormaPagamentoMFD("Vale", "0");
                                    //conectorECF.Analisa_iRetorno(fiscal_retorno, ref fiscal_MSG);
                                    this.lbErroMainConfig.Invoke((MethodInvoker)delegate { lbErroMainConfig.Text = fiscal_MSG; });
                                    this.lblProcesso.Invoke((MethodInvoker)delegate { lblProcesso.Text = "[ 6 ]"; });
                                    valida++;
                                }
                                else
                                {
                                    if (fiscal_retorno == 1)
                                    {
                                        this.lbFormaPgtoMainConfig.Invoke((MethodInvoker)delegate { lbFormaPgtoMainConfig.Text = "OK!"; });
                                        this.lblProcesso.Invoke((MethodInvoker)delegate { lblProcesso.Text = "[ 6 ]"; });
                                        valida++;
                                    }
                                    else
                                    {
                                        this.lbFormaPgtoMainConfig.Invoke((MethodInvoker)delegate { lbFormaPgtoMainConfig.Text = "NÃO CARREGADA."; });
                                    }
                                }
                            }
                            else
                            {
                                this.lbErroMainConfig.Invoke((MethodInvoker)delegate { lbErroMainConfig.Text = "ERRO PASSO 6."; });
                            }
                        }
                        else
                        {
                            this.lblProcesso.Invoke((MethodInvoker)delegate { lblProcesso.Text = "[ 6 ]"; });
                            this.lblProcessoErros.Invoke((MethodInvoker)delegate { lblProcessoErros.Text = "[ 6 ]"; });
                            valida++;
                        }
                    }
                    else
                    {
                        this.lbErroMainConfig.Invoke((MethodInvoker)delegate { lbErroMainConfig.Text = "ERRO PASSO 6."; });
                        this.lbFormaPgtoMainConfig.Invoke((MethodInvoker)delegate { lbFormaPgtoMainConfig.Text = "OK!"; }); 
                        valida++;
                    }
                    break;
                case 7: //Carrega GrandTotal Fiscal do ECF
                    if (alwaysVariables.ModoOperacao == 1)
                    {
                        if (alwaysVariables.ECF_Ligada != -6)//Impressora Ligada
                        {
                            functionECF.conectorECF_GrandeTotal_Crypt(alwaysVariables.ModeloEcf, ref fiscal_GT_Crypt, ref fiscal_MSG, ref fiscal_retorno, alwaysVariables.ECF_Ligada);
                            //fiscal_retorno = conectorECF.Bematech_FI_GrandeTotal(ref fiscal_GT);
                            //conectorECF.Analisa_iRetorno(fiscal_retorno, ref fiscal_MSG);

                            if (fiscal_retorno == 1)
                            {
                                this.lbGTMainConfig.Invoke((MethodInvoker)delegate { lbGTMainConfig.Text = fiscal_GT; });
                                this.lblProcesso.Invoke((MethodInvoker)delegate { lblProcesso.Text = "[ 8 ]"; });
                                valida++;
                            }
                            else if (fiscal_GT == "000000000000000000" && fiscal_retorno == 1 && fiscal_vendaBruta_last_reducaoZ == "000000000000000000" && fiscal_GT == "000000000000000000" &&
                                               fiscal_GT_Crypt.Trim() == "" && fiscal_last_reducao_cro == "0000" && fiscal_last_reducao_crz == "0000" && fiscal_vendaBruta_last_reducaoZ == "000000000000000000")
                            {
                                this.lblProcesso.Invoke((MethodInvoker)delegate { lblProcesso.Text = "[ 8 ]"; });
                                valida++;
                            }
                            else
                            {
                                this.lbGTMainConfig.Invoke((MethodInvoker)delegate { lbGTMainConfig.Text = "ERRO"; });
                                this.lblProcessoErros.Invoke((MethodInvoker)delegate { lblProcessoErros.Text = "[ 8 ]"; });
                            }
                            this.lbErroMainConfig.Invoke((MethodInvoker)delegate { lbErroMainConfig.Text = fiscal_MSG; });
                        }
                        else
                        {
                            //select
                            valida++;
                        }
                    }
                    else
                    {
                        this.lbGTMainConfig.Invoke((MethodInvoker)delegate { lbGTMainConfig.Text = "0,00"; });
                        valida++;
                    }
                    break;
                case 8://Procedimento conferencia entre dados do ECF e cadastro do banco de dados
                    if (alwaysVariables.ModoOperacao == 1)
                    {
                        auxConsistencia = find_info_banco(fiscal_numero_caixa, alwaysVariables.Store);
                        if (auxConsistencia == 0)
                        {
                            if (alwaysVariables.ECF_Ligada != -6)//Impressora Ligada
                            {
                                functionECF.conectorECF_numero_serie(alwaysVariables.ModeloEcf, ref fiscal_numero_serie, ref fiscal_MSG, ref fiscal_retorno, alwaysVariables.ECF_Ligada);
                                //fiscal_retorno = conectorECF.Bematech_FI_NumeroSerieMFD(ref fiscal_numero_serie);
                                if (fiscal_numero_serie == "EMULADOR            ")
                                {
                                    this.lblProcesso.Invoke((MethodInvoker)delegate { lblProcesso.Text = "[ 9 ]"; });
                                    valida++;
                                }else 
                                if (fiscal_numero_serie.Trim() != alwaysVariables.Serie.Trim())
                                {
                                    this.lblProcessoErros.Invoke((MethodInvoker)delegate { lblProcessoErros.Text = "[ 9 ]"; });
                                    this.lbErroMainConfig.Invoke((MethodInvoker)delegate { lbErroMainConfig.Text = "NUMERO DE SERIE INVÁLIDO"; });
                                }
                                else if (Convert.ToInt32(banco_numero_caixa == "" ? "0" : banco_numero_caixa) != Convert.ToInt32(fiscal_numero_caixa == "    " ? "0" : fiscal_numero_caixa) ||
                                    ((banco_numero_caixa == "" ? "0" : banco_numero_caixa) == "0" && (fiscal_numero_caixa == "    " ? "0" : fiscal_numero_caixa) == "0"))
                                {
                                    this.lblProcessoErros.Invoke((MethodInvoker)delegate { lblProcessoErros.Text = "[ 9 ]"; });
                                    this.lbErroMainConfig.Invoke((MethodInvoker)delegate { lbErroMainConfig.Text = "NUMERO DO CAIXA DIFERENTE DO EQUIPAMENTO ECF"; });
                                }
                                else
                                {
                                    this.lblProcesso.Invoke((MethodInvoker)delegate { lblProcesso.Text = "[ 9 ]"; });
                                    valida++;
                                }
                            }
                            else
                            {
                                this.lblProcessoErros.Invoke((MethodInvoker)delegate { lblProcessoErros.Text = "[ 9 ]"; });
                                this.lblProcesso.Invoke((MethodInvoker)delegate { lblProcesso.Text = "[ 9 ]"; });
                                valida++;
                            }
                        } /*if (alwaysVariables.ECF_Ligada != -6)//Impressora Ligada
                        {
                            lblProcesso.Text = "[ 9 ]";
                            valida++;
                        }*/
                    }
                    else
                    {
                        this.lblProcesso.Invoke((MethodInvoker)delegate { lblProcesso.Text = "[ 9 ]"; });
                        valida++;
                    }
                    break;
                case 9:// Valor SubTotal Impressora Fiscal
                    if (alwaysVariables.ModoOperacao == 1)
                    {
                        functionECF.conectorECF_SubTotal(alwaysVariables.ModeloEcf, ref fiscal_SubTotal, ref fiscal_MSG, ref fiscal_retorno, alwaysVariables.ECF_Ligada);
                        //fiscal_retorno = conectorECF.Bematech_FI_SubTotal(ref fiscal_SubTotal);	
                        //conectorECF.Analisa_iRetorno(fiscal_retorno, ref fiscal_MSG);
                        this.lblProcesso.Invoke((MethodInvoker)delegate { lblProcesso.Text = "[ 10 ]"; });
                        valida++;
                    }
                    else
                    {
                        this.lblProcesso.Invoke((MethodInvoker)delegate { lblProcesso.Text = "[ 10 ]"; });
                        valida++;
                    }
                    break;
                case 10:// Valor Descontos Impressora Fiscal
                    if (alwaysVariables.ModoOperacao == 1)
                    {
                        functionECF.conectorECF_Descontos(alwaysVariables.ModeloEcf, ref fiscal_ValorDescontos, ref fiscal_MSG, ref fiscal_retorno, alwaysVariables.ECF_Ligada);
                        //fiscal_retorno = conectorECF.Bematech_FI_Descontos(ref fiscal_ValorDescontos);
                        //conectorECF.Analisa_iRetorno(fiscal_retorno, ref fiscal_MSG);
                        this.lblProcesso.Invoke((MethodInvoker)delegate { lblProcesso.Text = "[ 11 ]"; });
                        valida++;
                    }
                    else
                    {
                        this.lblProcesso.Invoke((MethodInvoker)delegate { lblProcesso.Text = "[ 11 ]"; });
                        valida++;
                    }
                    break;
                case 11:// Valor Cancelamentos Impressora Fiscal
                    if (alwaysVariables.ModoOperacao == 1)
                    {
                        functionECF.conectorECF_ValorCancelamento(alwaysVariables.ModeloEcf, ref fiscal_ValorCancelamentos, ref fiscal_MSG, ref fiscal_retorno, alwaysVariables.ECF_Ligada);
                        //fiscal_retorno = conectorECF.Bematech_FI_Cancelamentos(ref fiscal_ValorCancelamentos);
                        //conectorECF.Analisa_iRetorno(fiscal_retorno, ref fiscal_MSG);
                        this.lblProcesso.Invoke((MethodInvoker)delegate { lblProcesso.Text = "[ 12 ]"; });
                        valida++;
                    }
                    else
                    {
                        this.lblProcesso.Invoke((MethodInvoker)delegate { lblProcesso.Text = "[ 12 ]"; });
                        valida++;
                    }
                    break;
                case 12:// Numero de Reducões Impressora Fiscal
                    if (alwaysVariables.ModoOperacao == 1)
                    {
                        functionECF.conectorECF_ValorCancelamento(alwaysVariables.ModeloEcf, ref fiscal_NumReducoes, ref fiscal_MSG, ref fiscal_retorno, alwaysVariables.ECF_Ligada);
                        //fiscal_retorno = conectorECF.Bematech_FI_NumeroReducoes(ref fiscal_NumReducoes);
                        //conectorECF.Analisa_iRetorno(fiscal_retorno, ref fiscal_MSG);
                        this.lblProcesso.Invoke((MethodInvoker)delegate { lblProcesso.Text = "[ 13 ]"; });
                        valida++;
                    }
                    else
                    {
                        this.lblProcesso.Invoke((MethodInvoker)delegate { lblProcesso.Text = "[ 13 ]"; });
                        valida++;
                    }
                    break;
                case 13:// Numero de Cupom Cancelados Impressora Fiscal
                    if (alwaysVariables.ModoOperacao == 1)
                    {
                        functionECF.conectorECF_NumeroCuponsCancelados(alwaysVariables.ModeloEcf, ref fiscal_NumCuponsCanc, ref fiscal_MSG, ref fiscal_retorno, alwaysVariables.ECF_Ligada);
                        this.lblProcesso.Invoke((MethodInvoker)delegate { lblProcesso.Text = "[ 14 ]"; });
                        valida++;
                    }
                    else
                    {
                        this.lblProcesso.Invoke((MethodInvoker)delegate { lblProcesso.Text = "[ 14 ]"; });
                        valida++;
                    }
                    break;
                case 14:// Ultimo documento "Cupom" emitido
                    if (alwaysVariables.ModoOperacao == 1)
                    {
                        functionECF.conectorECF_NumeroCupom(alwaysVariables.ModeloEcf, ref fiscal_Cupom, ref fiscal_MSG, ref fiscal_retorno, alwaysVariables.ECF_Ligada);
                        this.lblProcesso.Invoke((MethodInvoker)delegate { lblProcesso.Text = "[ 15 ]"; });
                        valida++;
                    }
                    else
                    {
                        this.lblProcesso.Invoke((MethodInvoker)delegate { lblProcesso.Text = "[ 15 ]"; });
                        valida++;
                    }
                    break;
                case 15:
                    if (alwaysVariables.ModoOperacao == 1)
                    {
                        functionECF.conectorECF_VerificaZPendente(alwaysVariables.ModeloEcf, ref fiscal_MSG, ref fiscal_retorno, alwaysVariables.ECF_Ligada);
                        this.lblProcesso.Invoke((MethodInvoker)delegate { lblProcesso.Text = "[ 16 ]"; });
                        valida++;
                    }
                    else
                    {
                        this.lblProcesso.Invoke((MethodInvoker)delegate { lblProcesso.Text = "[ 16 ]"; });
                        valida++;
                    }
                    break;
                case 16:
                    if (alwaysVariables.ModoOperacao == 1)
                    {
                        fiscal_flag = 0;
                        functionECF.conectorECF_fiscal_flag(alwaysVariables.ModeloEcf, ref fiscal_flag, ref fiscal_MSG, ref fiscal_retorno, alwaysVariables.ECF_Ligada);                        
                        this.lblProcesso.Invoke((MethodInvoker)delegate { lblProcesso.Text = "[ 17 ]"; });
                        valida++;
                    }
                    else
                    {
                        this.lblProcesso.Invoke((MethodInvoker)delegate { lblProcesso.Text = "[ 17 ]"; });
                        valida++;
                    }
                    break;
                case 17:
                    //DateTime bof = Convert.ToDateTime("03/20/2017");
                    if (alwaysVariables.ModoOperacao == 1)
                    {
                        conectorPDV_find_operador();
                        if (alwaysVariables.ECF_Ligada != -6)//Impressora Ligada
                        {
                            if (fiscal_last_reducao_dataMovimento.Trim() != "" && fiscal_last_reducao_dataMovimento.Trim() != "" && fiscal_last_reducao_dataMovimento != "000000")
                            {
                                string test = "";
                                try
                                {
                                    test = String.Format("{0:yyyyMMdd}", Convert.ToDateTime(fiscal_last_reducao_dataMovimento.Substring(2, 2) + "/" + fiscal_last_reducao_dataMovimento.Substring(0, 2) + "/" + String.Format("{0:ddMMyyyy}", DateTime.Now).Substring(4, 2) + fiscal_data_printer.Substring(4, 2)));
                                }
                                catch (Exception)
                                {
                                    test = String.Format("{0:yyyyMMdd}", Convert.ToDateTime(fiscal_last_reducao_dataMovimento.Substring(0, 2) + "/" + fiscal_last_reducao_dataMovimento.Substring(2, 2) + "/" + String.Format("{0:ddMMyyyy}", DateTime.Now).Substring(4, 2) + fiscal_data_printer.Substring(4, 2)));
                                }
                                if (verifica_movimento_last(alwaysVariables.Store, fiscal_numero_caixa.Replace("\0","").Trim(), test) == 0)
                                {
                                    if (fiscal_DadosUltimaReducaoMFD.Trim().Length > 0)
                                    {
                                        string BOF = fiscal_last_reducao_dataMovimento.Substring(0, 2) + "/" + fiscal_last_reducao_dataMovimento.Substring(2, 2) + "/" + String.Format("{0:ddMMyyyy}", DateTime.Now).Substring(4, 2) + fiscal_data_printer.Substring(4, 2);
                                        try
                                        {
                                            conectorPDV_inc_movimentodia(alwaysVariables.Store, fiscal_numero_caixa, String.Format("{0:yyyyMMdd}", Convert.ToDateTime(fiscal_last_reducao_dataMovimento.Substring(2, 2) + "/" + fiscal_last_reducao_dataMovimento.Substring(0, 2) + "/" + String.Format("{0:ddMMyyyy}", DateTime.Now).Substring(4, 2) + fiscal_data_printer.Substring(4, 2))), banco_operador, fiscal_numero_serie, fiscal_letramf_adicional.Replace("\0", "").Trim(), fiscal_modelo.Replace("\0", "").Trim(), fiscal_last_reducao_crz.Replace("\0", "").Trim(), fiscal_last_reducao_coo.Replace("\0", "").Trim(), fiscal_last_reducao_cro.Replace("\0", "").Trim(), String.Format("{0:yyyyMMdd}", Convert.ToDateTime(fiscal_last_reducao_dataMovimento.Substring(2, 2) + "/" + fiscal_last_reducao_dataMovimento.Substring(0, 2) + "/" + String.Format("{0:ddMMyyyy}", DateTime.Now).Substring(4, 2) + fiscal_data_printer.Substring(4, 2))), String.Format("{0:hhmmss}", DateTime.Now), fiscal_vendaBruta_last_reducaoZ, "");
                                        }
                                        catch (Exception)
                                        {
                                            conectorPDV_inc_movimentodia(alwaysVariables.Store, fiscal_numero_caixa, String.Format("{0:yyyyMMdd}", Convert.ToDateTime(fiscal_last_reducao_dataMovimento.Substring(0, 2) + "/" + fiscal_last_reducao_dataMovimento.Substring(2, 2) + "/" + String.Format("{0:ddMMyyyy}", DateTime.Now).Substring(4, 2) + fiscal_data_printer.Substring(4, 2))), banco_operador, fiscal_numero_serie, fiscal_letramf_adicional.Replace("\0", "").Trim(), fiscal_modelo.Replace("\0", "").Trim(), fiscal_last_reducao_crz.Replace("\0", "").Trim(), fiscal_last_reducao_coo.Replace("\0", "").Trim(), fiscal_last_reducao_cro.Replace("\0", "").Trim(), String.Format("{0:yyyyMMdd}", Convert.ToDateTime(fiscal_last_reducao_dataMovimento.Substring(0, 2) + "/" + fiscal_last_reducao_dataMovimento.Substring(2, 2) + "/" + String.Format("{0:ddMMyyyy}", DateTime.Now).Substring(4, 2) + fiscal_data_printer.Substring(4, 2))), String.Format("{0:hhmmss}", DateTime.Now), fiscal_vendaBruta_last_reducaoZ, "");
                                        }
                                        
                                    }
                                }
                                this.lblProcesso.Invoke((MethodInvoker)delegate { lblProcesso.Text = "[ 18 ]"; });
                                valida++;
                            }
                            else
                            {
                                fiscal_last_reducao_dataMovimento = fiscal_data_last_reducaoZ;
                            }
                            if (banco_operador != "0")
                            {
                                conectorPDV_find_operador();
                                if (fiscal_last_reducao_dataMovimento.Trim() != "" && fiscal_last_reducao_dataMovimento.Trim() != "000000")
                                {
                                    cryptografia.conectorPDV_GT(String.Format("{0:yyyyMMdd}",                                                              (fiscal_last_reducao_dataMovimento.Substring(0, 2) + "/" + fiscal_last_reducao_dataMovimento.Substring(2, 2) + "/" + String.Format("{0:ddMMyyyy}", DateTime.Now).Substring(4, 2) + fiscal_data_printer.Substring(4, 2))), fiscal_store.Replace("\0", "").Trim(), fiscal_numero_caixa.Replace("\0", "").Trim(), banco_sequencia, banco_operador, fiscal_numero_serie);
                                }
                                this.lblProcesso.Invoke((MethodInvoker)delegate { lblProcesso.Text = "[ 18 ]"; });
                                valida++;
                            }
                            else if (fiscal_GT == "000000000000000000" && fiscal_retorno == 1 && fiscal_vendaBruta_last_reducaoZ == "000000000000000000" && fiscal_GT == "000000000000000000" &&
                                              fiscal_last_reducao_cro == "0000" && fiscal_last_reducao_crz == "0000" && fiscal_vendaBruta_last_reducaoZ == "000000000000000000")
                            {
                                this.lblProcesso.Invoke((MethodInvoker)delegate { lblProcesso.Text = "[ 18 ]"; });
                                valida++;
                            }
                        }
                        else
                        {
                            this.lblProcesso.Invoke((MethodInvoker)delegate { lblProcesso.Text = "[ 18 ]"; });
                            valida++;
                        }
                    }
                    else
                    {
                        this.lblProcesso.Invoke((MethodInvoker)delegate { lblProcesso.Text = "[ 18 ]"; });
                        valida++;
                    }
                    break;
                case 18: 
                    conectorPDV_clear_log_pedido();
                    if (ts.Days > 0)
                    {
                        this.lbMsgMainConfig.Invoke((MethodInvoker)delegate { lbMsgMainConfig.Text = "AGUARDANDO CARGA DE LIBERAÇÃO - SISTEMA EXPIRADO, RENOVE A SUA LICENÇA...!"; });
                        this.lblCargaMainConfig.Invoke((MethodInvoker)delegate { lblCargaMainConfig.Text = "NÃO LIBERADA"; });
                        string[] dirs = Directory.GetFiles(folderRepc, "*TRANSMISSAO*");
                        foreach (string dir in dirs)
                        {

                            if (File.Exists(dir))
                            {
                                this.lbMsgMainConfig.ForeColor = System.Drawing.Color.Cyan;
                                this.pgbWaitMainConfig.Style = System.Windows.Forms.ProgressBarStyle.Blocks;
                                instrucao.preparaSql(dir);
                                instrucao.carregaSql();
                                instrucao.executaSqlBat(0);
                                agrupa_liberacao(numero_pdv());
                                conectorPDV_gera_crypt_produto();
                                this.lbMsgMainConfig.Invoke((MethodInvoker)delegate { lbMsgMainConfig.Text = "RECEBENDO CARGA DE LIBERAÇÃO...!"; });
                                this.lbErroMainConfig.Invoke((MethodInvoker)delegate { lbErroMainConfig.Text = ""; });
                            }
                        }
                    }
                    else if (fiscal_GT == "000000000000000000" && fiscal_retorno == 1 && fiscal_vendaBruta_last_reducaoZ == "000000000000000000" && fiscal_GT == "000000000000000000" &&
                                             fiscal_last_reducao_cro == "0000" && fiscal_last_reducao_crz == "0000" && fiscal_vendaBruta_last_reducaoZ == "000000000000000000")
                    {
                        this.lblProcesso.Invoke((MethodInvoker)delegate { lblProcesso.Text = "[ 19 ]"; });
                        valida++;
                    }
                    else
                    {
                        this.lblProcesso.Invoke((MethodInvoker)delegate { lblProcesso.Text = "[ 19 ]"; });
                        valida++;
                    }
                    break;
            }
            return fiscal_retorno;
        }
        public static string getFile(string localizacao)
        {
            if (localizacao.IndexOf("\\conector") != -1)
            {
                localizacao = localizacao.Replace("\\conector", "");
            }
            return localizacao;
        }

        private void modoInicializacao(int tipo)
        {
            Directory.CreateDirectory(folderCrypt);
            Directory.CreateDirectory(folderTrans);
            Directory.CreateDirectory(folderRepc);
            Directory.CreateDirectory(folderMFDGrand);
            alwaysVariables.CopyLocal = getValue("banco_smartPDV", "copyLocal", fileSecret);
            alwaysVariables.Schema = getValue("banco_smartPDV", "schema", fileSecret);
            alwaysVariables.UserName = getValue("banco_smartPDV", "username", fileSecret);
            alwaysVariables.Senha = getValue("banco_smartPDV", "password", fileSecret);
            alwaysVariables.LocalHost = getValue("banco_smartPDV", "server", fileSecret);
            alwaysVariables.LocalHost = getValue("banco_smartPDV", "server", fileSecret);
            alwaysVariables.KeysPrivate = getValue("banco_smartPDV", "keysPrivada", fileSecret);
            alwaysVariables.KeysPublica = getValue("banco_smartPDV", "keysPublica", fileSecret);
            alwaysVariables.KeysEAD = getValue("banco_smartPDV", "EAD", fileSecret);
            alwaysVariables.Entidade = getValue("banco_smartPDV", "Entidade", fileSecret);
            alwaysVariables.ModoNFce = getValue("banco_smartPDV", "ModoNFce", fileSecret);
            alwaysVariables.TypeComunicacao = getValue("banco_smartPDV", "typeComunicao", fileSecret);
            alwaysVariables.TransmissaoWindows = getValue("banco_smartPDV", "transmissaoWindows", fileSecret);
            alwaysVariables.TransmissaoLinux = getValue("banco_smartPDV", "transmissaoLinux", fileSecret);
            alwaysVariables.RecepcaoLinux = getValue("banco_smartPDV", "recepcaoLinux", fileSecret);
            alwaysVariables.RecepcaoWindows = getValue("banco_smartPDV", "recepcaoWindows", fileSecret);
            alwaysVariables.EstiloNfce = getValue("banco_smartPDV", "EstiloNfce", fileSecret);
            alwaysVariables.Perfil = getValue("banco_smartPDV", "Perfil", fileSecret);
            alwaysVariables.Store = getValue("loja", "store", fileSecret);
            alwaysVariables.Atualizador = getValue("ftp", "atualizador", fileSecret);
            alwaysVariables.PalavraChave = getValue("ftp", "palavraChave", fileSecret);
            alwaysVariables.StringConector = getValue("ftp", "stringConector", fileSecret);
            alwaysVariables.PAF_Contato = getValue("SoftwareHouse", "contato", fileSecret);
            alwaysVariables.PAF_contatoCom = getValue("SoftwareHouse", "contatoCom", fileSecret);
            alwaysVariables.PAF_laudo = getValue("SoftwareHouse", "laudo", fileSecret);
            alwaysVariables.PAF_Endereco = getValue("SoftwareHouse", "Endereco", fileSecret);
            alwaysVariables.PAF_NumeroAplicativo = getValue("SoftwareHouse", "NumeroAplicativo", fileSecret);
            alwaysVariables.PAF_CNPJ = getValue("SoftwareHouse", "CNPJ", fileSecret);
            alwaysVariables.PAF_IE = getValue("SoftwareHouse", "IE", fileSecret);
            alwaysVariables.PAF_IM = getValue("SoftwareHouse", "IM", fileSecret);
            alwaysVariables.PAF_RAZAO = getValue("SoftwareHouse", "RazaoSocial", fileSecret);
            alwaysVariables.PAF_TELEFONE = getValue("SoftwareHouse", "Fone", fileSecret);
            alwaysVariables.PAF_Versao = getValue("SoftwareHouse", "Versao", fileSecret);
            alwaysVariables.PAF_Versao_Spec = getValue("SoftwareHouse", "Versao_spec", fileSecret);
            alwaysVariables.ModeloEcf = Convert.ToInt32(getValue("Printer", "modeloEcf", fileSecret));
            alwaysVariables.ecfPrinter = getValue("Printer", "ecfUtil", fileSecret);
            alwaysVariables.CarregaPgto = getValue("banco", "carregapgto", fileSecret);
            alwaysVariables.LocalHostSuper = getValue("banco", "super_server", fileSecret);
            functionECF.conectorECF_VerificaImpressoraLigada(alwaysVariables.ModeloEcf, ref fiscal_MSG, ref fiscal_retorno);
            alwaysVariables.ECF_Ligada = fiscal_retorno;
            try
            {
                cryptografia = new crypt();
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.ToString(), "Instancia 01");
            }
            lblVersao.Text = alwaysVariables.PAF_Versao_Spec;

            export = new conectorExport0202();


            //fileSecret = Path.Combine(Directory.GetCurrentDirectory(), "c:\\windows\\soberanu.ini");
            if (File.Exists("c:\\windows\\soberanu.ini"))
            {
                fileSecret = Path.Combine(Directory.GetCurrentDirectory(), "c:\\windows\\soberanu.ini");
            }
            else
            {
                fileSecret = Path.Combine(Directory.GetCurrentDirectory(), "soberanu.ini");
            }
            try
            {
                if (alwaysVariables.ECF_Ligada != -6)
                {
                    functionECF.conectorECF_numero_serie(alwaysVariables.ModeloEcf, ref fiscal_numero_serie, ref fiscal_MSG, ref fiscal_retorno, alwaysVariables.ECF_Ligada);
                }
                else
                {
                    //select
                }
            }
            catch (Exception)
            {

            }
            //fiscal_retorno = conectorECF.Bematech_FI_NumeroSerieMFD(ref fiscal_numero_serie);
            if (fiscal_numero_serie.Trim() != "")
            {
                alwaysVariables.Serie_Hash_Hard = key.GetMd5Sum(fiscal_numero_serie);
            }

            //Flavio Old Variavel Alteracao
            ///REQUISITO XI
            ///a) gerar, por meio do algoritmo Message Digest-5 (MD-5), código de autenticação para cada arquivo executável que realize os requisitos estabelecidos nesta especificação;
            ///
            //string caminho = Path.GetFullPath("dll");
            //DirectoryInfo dirInfo = new DirectoryInfo(caminho);
            //conectorPDV_PAFECF_chave(folderEletronico + "\\" + "conectorECF-" + fiscal_numero_serie.Replace("\0", "").Trim() + "-" + fiscal_data_movimento + String.Format("{0:hhmmss}", DateTime.Now) + ".txt", alwaysVariables.MD5_Main); }
            //string retorno = descryptFile(folderMFDGrand + "\\MD5" + ".enc", "\\MD5");
            /*if (File.Exists(@"C:\conector\MFD\arquivos\serial.enc"))
            {
                alwaysVariables.MD5_Main = key.retornoFileMD5(@"C:\conector\MFD\arquivos\serial.enc");
            }*/
            //alwaysVariables.MD5_Main = key.retornoFileMD5(folderEletronico + "\\" + "conectorECF-" + fiscal_numero_serie.Replace("\0", "").Trim());
            alwaysVariables.MD5_Main_conectorEXE = key.retornoFileMD5(Application.ExecutablePath);
            alwaysVariables.MD5_Main_boletoFrs = key.retornoFileMD5(@"C:\conector\dll\boletoFrs.dll");
            alwaysVariables.MD5_Main_conectorAmbient = key.retornoFileMD5(@"C:\conector\dll\conectorAmbient.dll");
            alwaysVariables.MD5_Main_conectorBank = key.retornoFileMD5(@"C:\conector\dll\conectorBank.dll");
            alwaysVariables.MD5_Main_conectorCrypt = key.retornoFileMD5(@"C:\conector\dll\conectorCrypt.dll");
            alwaysVariables.MD5_Main_conectorECF = key.retornoFileMD5(@"C:\conector\dll\conectorECF.dll");
            alwaysVariables.MD5_Main_conectorInstrucao = key.retornoFileMD5(@"C:\conector\dll\conectorInstrucao.dll");
            alwaysVariables.MD5_Main_conectorSetting = key.retornoFileMD5(@"C:\conector\dll\conectorSetting.dll");
            alwaysVariables.MD5_Main_conectorSintegra = key.retornoFileMD5(@"C:\conector\dll\conectorSintegra.dll");
            alwaysVariables.MD5_Main_conectorTef = key.retornoFileMD5(@"C:\conector\dll\conectorTef.dll");
            alwaysVariables.MD5_Main_fiscal = key.retornoFileMD5(@"C:\conector\dll\fiscal.dll");
            try
            {
                alwaysVariables.MD5_Main_32 = key.retornoFileMD5(@"C:\conector\dll\extende\BemaFI32.dll");
                alwaysVariables.MD5_Main_64 = key.retornoFileMD5(@"C:\conector\dll\extende\BemaFI64.dll");
            }
            catch (Exception)
            {

            }
            alwaysVariables.MD5_Main_mfd = key.retornoFileMD5(@"C:\conector\dll\extende\BemaMFD.dll");
            alwaysVariables.MD5_Main_mfd3 = key.retornoFileMD5(@"C:\conector\dll\extende\BemaMFD3.dll");

            alwaysVariables.MD5_Main_INTERFACEEPSON = key.retornoFileMD5(@"C:\conector\dll\InterfaceEpson.dll");
            alwaysVariables.MD5_Main_ELGIN = key.retornoFileMD5(@"C:\conector\dll\elgin.dll");
            alwaysVariables.MD5_Main_DARUMA = key.retornoFileMD5(@"C:\conector\dll\DarumaFrameWork.dll");
            alwaysVariables.MD5_Main_SWEDA = key.retornoFileMD5(@"C:\conector\dll\CONVECF.dll");
            alwaysVariables.MD5_Main_boletoFrs = key.retornoFileMD5(@"C:\conector\dll\boletoFrs.dll");
            alwaysVariables.MD5_Main_sophus = key.retornoFileMD5(@"C:\conector\dll\sophus.dll");


            strComando = "select tab3.nr_pdv, ";
            strComando += "       date_format(tab3.liberacao, '%d/%m/%Y'),";
            strComando += "       date_format(tab3.expiracao, '%d/%m/%Y'),";
            strComando += "       tab1.ipCaixa,";
            strComando += "       tab1.serie,";
            strComando += "       tab4.idStatusPDV,";
            strComando += "       tab4.descricao, ";
            strComando += "       tab1.autentica, ";
            strComando += "       tab3.liberacao, ";
            strComando += "       tab3.expiracao, ";
            strComando += "       tab1.typeTef ";
            strComando += "        from conectorPDV.terminal tab";
            strComando += "              inner join conectorPDV.terminalecfconfig tab1 on(tab.idTerminal = tab1.terminal)";
            strComando += "              inner join nr_pdv tab2 on(tab1.caixa = tab2.nr_pdv)";
            strComando += "              inner join conectorPDV.liberacao tab3 on(tab2.nr_pdv = tab3.nr_pdv and tab1.caixa = tab3.nr_pdv)";
            strComando += "              inner join conectorPdv.statusPDV tab4 on(tab1.statusPDV = tab4.idstatusPDV) where tab2.nr_pdv=?VarCx and tab.idLoja=?VarStore";

            instrucao = new instrucaol();

            if (!File.Exists(alwaysVariables.PAF_total + "\\grandFullPDV" + ".enc")) //Conferencia se há Grande Total e informa caso haja proble
            {
                msgInfo msg = new msgInfo(1, "Caro Cliente - " + "ERRO FATAL GRANDE TOTAL INVÁLIDO OU NÃO ENCONTRADO! SOLICITE A AUTORIZAÇÃO PARA GERAÇÃO DO GRANDE TOTAL"); msg.ShowDialog();
                //fiscal_retorno = conectorECF.Bematech_FI_GrandeTotalCriptografado(ref fiscal_GT_Crypt);
                if (alwaysVariables.ModoOperacao == 1)
                {
                    if (alwaysVariables.ECF_Ligada != -6)
                    {
                        functionECF.conectorECF_GrandeTotal_Crypt(alwaysVariables.ModeloEcf, ref fiscal_GT_Crypt, ref fiscal_MSG, ref fiscal_retorno, alwaysVariables.ECF_Ligada);
                        if (alwaysVariables.flagG == "s")
                        {
                            if (fiscal_retorno == 1)
                            {
                                mensagemLinha = new List<string>();

                                if (File.Exists(alwaysVariables.PAF_total + "grandFullPDV" + ".txt"))
                                {
                                    File.Delete(alwaysVariables.PAF_total + "grandFullPDV" + ".txt");
                                }

                                conectorPDV_serie(alwaysVariables.PAF_total + "grandFullPDV" + ".txt");
                            }
                        }
                    }
                }
            }
        }
        public static string getValue(string secao, string chave, string fileName)
        {
            int carateres = 257;
            StringBuilder buffer = new StringBuilder(carateres);
            string sdefault = "";
            if (GetPrivateProfileString(secao, chave, sdefault, buffer, carateres, fileName) != 0)
            {
                return buffer.ToString();
            }
            else
            {
                // Verifica o último erro Win32.
                int err = Marshal.GetLastWin32Error();
                return null;
            }
        }

        public static bool writeValue(string secao, string chave, string value, string filename)
        {
            return WritePrivateProfileString(secao, chave, value, filename);
        }
        public static string Left(string param, int length)
        {
            //we start at 0 since we want to get the characters starting from the
            //left and with the specified lenght and assign it to a variable
            string result = param.Substring(0, length);
            //return the result of the operation
            return result;
        }
        //public static string Right(string param, int length)
        public string Right(string param, int length)
        {
            //start at the index based on the lenght of the sting minus
            //the specified lenght and assign it a variable
            string result = param.Substring(param.Length - length, length);
            //return the result of the operation
            return result;
        }
        // Handle Exited event and display process information. 
        private void myProcess_Exited(object sender, System.EventArgs e)
        {
            eventHandled = true;
        }
        //#############################################################END Declaração de Metodos#############################################


        private void mainConfig_Load(object sender, EventArgs e)
        {

            fuso = new timedate();

            super = new MG001.superClass(alwaysVariables.Store);

            #region //OldConstrutor
                       //fileSecret = Path.Combine(Directory.GetCurrentDirectory(), "c:\\windows\\soberanu.ini");
            if (File.Exists("c:\\windows\\soberanu.ini"))
            {
                fileSecret = Path.Combine(Directory.GetCurrentDirectory(), "c:\\windows\\soberanu.ini");
            }
            else
            {
                fileSecret = Path.Combine(Directory.GetCurrentDirectory(), "soberanu.ini");
            }
            alwaysVariables.UrlWebConector = getValue("banco_smartPDV", "urlWebConector", fileSecret);
            alwaysVariables.ModoOperacao = Convert.ToInt32(getValue("banco_smartPDV", "ModoOperacao", fileSecret));
            alwaysVariables.Desconhecido = getValue("banco_smartPDV", "desconhecido", fileSecret);
            alwaysVariables.ModeloEcf = Convert.ToInt32(getValue("Printer", "modeloEcf", fileSecret));
            alwaysVariables.AtoCotepe = getValue("banco_smartPDV", "atocotepe", fileSecret);
            alwaysVariables.CopyLocal = getValue("banco_smartPDV", "copyLocal", fileSecret);


            carrega_crediario(alwaysVariables.Store);
            modoInicializacao(alwaysVariables.ModoOperacao);

            #endregion

            alwaysVariables.carrega_infor(alwaysVariables.Store);
            auxConsistencia = 0;
            Directory.CreateDirectory(folderTef);

            path = folderRepc + String.Format("{0:ddMMyyyy}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(alwaysVariables.Store)) + "TRANSMISSAO.rar";
            functionECF.conectorECF_VerificaImpressoraLigada(alwaysVariables.ModeloEcf, ref fiscal_MSG, ref fiscal_retorno);
            alwaysVariables.ECF_Ligada = fiscal_retorno;
            //fiscal_retorno = conectorECF.Bematech_FI_VerificaImpressoraLigada();
            //conectorECF.Analisa_iRetorno(fiscal_retorno, ref fiscal_MSG);
            elapsedTime = 0;

            //carrega_crediario(alwaysVariables.Store);
            if (fiscal_MSG != "" && fiscal_MSG != null)
            {
                lbErroMainConfig.ForeColor = System.Drawing.Color.Red;
                this.lbErroMainConfig.Invoke((MethodInvoker)delegate { lbErroMainConfig.Text = fiscal_MSG; });
                lbStatusMainConfig.Text = "ERRO COMUNICAÇÃO";
            }
            else
            {
                functionECF.conectorECF_VerificaImpressoraLigada(alwaysVariables.ModeloEcf, ref fiscal_MSG, ref fiscal_retorno);
                alwaysVariables.ECF_Ligada = fiscal_retorno;

                if (alwaysVariables.ECF_Ligada != -6)//Impressora Ligada
                {
                    functionECF.conectorECF_NumeroCaixa(alwaysVariables.ModeloEcf, ref fiscal_numero_caixa, ref fiscal_MSG, ref fiscal_retorno, alwaysVariables.ECF_Ligada);
                    //fiscal_retorno = conectorECF.Bematech_FI_NumeroCaixa(ref fiscal_numero_caixa);
                    //conectorECF.Analisa_iRetorno(fiscal_retorno, ref fiscal_MSG);
                    functionECF.conectorECF_NumeroLoja(alwaysVariables.ModeloEcf, ref fiscal_store, ref fiscal_MSG, ref fiscal_retorno,alwaysVariables.ECF_Ligada);
                    //conectorECF.Analisa_iRetorno(fiscal_retorno, ref fiscal_MSG);
                }
                else
                {
                    //Select
                }
                auxConsistencia = find_info_banco(fiscal_numero_caixa.Replace("\0",""), fiscal_store);
                if (auxConsistencia == 0 && alwaysVariables.CHAVE_CAIXA > 0)
                {
                    conector_find_carrega_const(alwaysVariables.CHAVE_CAIXA.ToString());//Validando numero de serie
                }
            }
            agrupa_liberacao(numero_pdv());
            
            carrega_crediario(alwaysVariables.Store);
            update = liberacao();
            
            if (update == 1)
            {
                pgbWaitMainConfig.Maximum = 18;
                eventHandled = true;
                if (ts.Days > 0)
                {
                    lbMsgMainConfig.Text = "SISTEMA EXPIRADO, RENOVE A SUA LICENÇA...!";
                    lblCargaMainConfig.Text = "NÃO LIBERADA";
                }
                else
                {
                    lblCargaMainConfig.Text = "LIBERADA";
                    this.lbMsgMainConfig.ForeColor = System.Drawing.Color.Cyan;
                    this.pgbWaitMainConfig.Style = System.Windows.Forms.ProgressBarStyle.Blocks;
                    string[] dirs = Directory.GetFiles(folderRepc, "*TRANSMISSAO*");
                    foreach (string dir in dirs)
                    {
                        if (File.Exists(dir))
                        {
                            lbMsgMainConfig.Text = "RECEBENDO CARGA"; lbErroMainConfig.Text = "";
                        }
                        else
                        {
                            lbMsgMainConfig.Text = "CARREGANDO"; //lbErroMainConfig.Text = "...";
                        }
                    }
                    /*for (int i = 0; i < 30; i++)
                    {
                        string test = path.Replace(".rar", "-" + i.ToString() + ".rar");
                        if (File.Exists(test))
                        { 
                            lbMsgMainConfig.Text = "RECEBENDO CARGA"; lbErroMainConfig.Text = ""; }
                        else
                        {
                            lbMsgMainConfig.Text = "CARREGANDO"; //lbErroMainConfig.Text = "...";
                        }
                    }*/
                }
            }
            else
            {
                eventHandled = false;
                lblCargaMainConfig.Text = "AGUARDANDO CARGA";
                lbMsgMainConfig.Text = "PDV NÃO LIBERADO - SOLICITE A CARGA DE ATUALIZAÇÃO!";
                lbErroMainConfig.Text = "";
                this.lbMsgMainConfig.ForeColor = System.Drawing.Color.Red;
                this.pgbWaitMainConfig.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
                pgbWaitMainConfig.Maximum = 300100;
            }
            MyProcess.Exited += new EventHandler(myProcess_Exited);
            clock.Start();// clock_Tick_tack()
            if (backgroundInitial.IsBusy != true)//backgroundConectorEstoque
            {
                // Start the asynchronous operation.
                backgroundInitial.RunWorkerAsync();
            }
            lbTentativasMainConfig.Text = tentativa.ToString();
        }

        private void conector_pdv()
        {
            if (alwaysVariables.ModoOperacao == 1 && alwaysVariables.ECF_Ligada != -6)
            {
                if (numero_pdv() == Convert.ToInt32(fiscal_numero_caixa.Replace("\0", "").Trim()))
                {
                    tentativa++;
                    if (tentativa == 6)
                    {
                        this.lbErroMainConfig.Invoke((MethodInvoker)delegate { lbErroMainConfig.Text = "TESTE DE VERIFICAÇÃO OCORRIDO COM SUCESSO!"; });
                    }
                    else
                    {
                        this.lbErroMainConfig.Invoke((MethodInvoker)delegate { lbErroMainConfig.Text = "TESTE DE VERIFICAÇÃO DO GRANDE TOTAL INCONSISTENTE, REINÍCIO!"; });
                    }
                    //this.Invoke((MethodInvoker)delegate { this.Location = new System.Drawing.Point(0, 0); });
                    //this.Invoke((MethodInvoker)delegate { this.Size = new System.Drawing.Size(0, 0); });
                    clock.Stop();
                    this.Hide();
                    auxConsistencia = 1; //Trava a carga anti-horaria
                    if (controle == true)
                    {
                        MG001.ConectorCF ecf = new MG001.ConectorCF(fiscal_numero_serie, fiscal_store, fiscal_numero_caixa, fiscal_data_movimento, fiscal_hora_printer, fiscal_data_printer, fiscal_retorno, fiscal_Cupom, fiscal_flag, banco_operador, lbIpTerminalMainConfig.Text, statusBanco, fiscal_GT, path, fiscal_CGC, fiscal_dtusuario_last, fiscal_dtsoft_basico, fiscal_letramf_adicional, fiscal_marca, fiscal_modelo, fiscal_tipo_ecf, fiscal_VersaoFirmware, fiscal_last_reducao_coo, fiscal_last_reducao_cro, fiscal_last_reducao_crz, fiscal_last_reducao_dataMovimento, fiscal_last_reducao_ValorAcumulado, fiscal_last_reducao_Isento_ICMS, fiscal_last_reducao_naoIncide_ICMS, fiscal_last_reducao_St_ICMS, fiscal_last_reducao_Isento_ISSQN, fiscal_last_reducao_naoIncide_ISSQN, fiscal_last_reducao_St_ISSQN, fiscal_last_reducao_desconto_ICMS, fiscal_last_reducao_desconto_ISSQN, fiscal_last_reducao_acrescimo_ICMS, fiscal_last_reducao_acrescimo_ISSQN, fiscal_last_reducao_cancelamento_ICMS, fiscal_last_reducao_cancelamento_ISSQN, fiscal_last_reducao_parcial_not_icms, fiscal_last_reducao_sangria, fiscal_last_reducao_suprimento, fiscal_last_reducao_cancelamento_not_fiscal, fiscal_last_reducao_desconto_not_fiscal, fiscal_last_reducao_acrescimo_not_fiscal, fiscal_trunca_arredonda, fiscal_last_reducao_T18, fiscal_last_reducao_T12, fiscal_last_reducao_T07, fiscal_last_reducao_grandTotal, vetorAliquotaLastZ);
                        controle = false;
                        ecf.ShowDialog();
                    }
                    Environment.Exit(1);
                    //this.Close();
                }
                else
                {
                    this.lbCaixaMainConfig.Invoke((MethodInvoker)delegate { lbCaixaMainConfig.Text += " NÃO LIBERADO."; });
                }
            }
            else
            {
                tentativa = 6;
                this.Hide();
                auxConsistencia = 1; //Trava a carga anti-horaria
                MG001.ConectorCF ecf = new MG001.ConectorCF(alwaysVariables.ModoOperacao);
                ecf.ShowDialog();
                Environment.Exit(1);
                //this.Close();

            }
        }
        private void clock_Tick(object sender, EventArgs e) 
        {
            if (controle == true)
            {
                conector_pdv();
            }
        }

        private void clock_Tick_tack()
        {
            pgbWaitMainConfig.Minimum = 0;

            if (Convert.ToInt16(fiscal_flag) == 0)
            {
                string[] dirs = Directory.GetFiles(folderRepc, "*TRANSMISSAO*");
                foreach (string dir in dirs)
                {
                    if (File.Exists(dir) && auxConsistencia == 0)
                    {
                        instrucao.preparaSql(dir);
                        instrucao.carregaSql();
                        instrucao.executaSqlBat(0);
                        agrupa_liberacao(numero_pdv());
                        this.lbMsgMainConfig.Invoke((MethodInvoker)delegate { lbMsgMainConfig.Text = "RECEBENDO CARGA DE ATUALIZAÇÃO, AGUARDE...!"; });
                        /*this.Hide();
                        msgInfo msg = new msgInfo(1"ATUALIZAÇÃO DO SISTEMA CONCLUÍDA, REINICIE O SISTEMA PARA RECUPERAR AS CONFIGURAÇÕES...!");
                        msg.ShowDialog();
                        this.Close();*/
                    }
                }
            }

            while (!eventHandled)
            {
                elapsedTime += SLEEP_AMOUNT;
                pgbWaitMainConfig.Value = elapsedTime;
                if (elapsedTime > 300000)
                {
                    tentativa = 6;
                    this.Dispose();
                    break;
                }

                if (liberacao() == 1)
                {
                    string[] dirs = Directory.GetFiles(folderRepc, "*TRANSMISSAO*");
                    foreach (string dir in dirs)
                    {

                        if (File.Exists(dir) && auxConsistencia == 0)
                        {
                            instrucao.preparaSql(dir);
                            instrucao.carregaSql();
                            instrucao.executaSqlBat(0);
                            agrupa_liberacao(numero_pdv());
                        }
                    }
                    eventHandled = true;
                    this.lbMsgMainConfig.Invoke((MethodInvoker)delegate { this.lbMsgMainConfig.ForeColor = System.Drawing.Color.Cyan; });
                    this.pgbWaitMainConfig.Invoke((MethodInvoker)delegate { this.pgbWaitMainConfig.Style = System.Windows.Forms.ProgressBarStyle.Blocks; });
                    this.lbMsgMainConfig.Invoke((MethodInvoker)delegate { lbMsgMainConfig.Text = "RECEBENDO CARGA DE ATUALIZAÇÃO, AGUARDE...!"; });
                    //lbErroMainConfig.Text = "...";
                    pgbWaitMainConfig.Value = 0;
                    pgbWaitMainConfig.Maximum = 8;
                    i = 0;
                    break;
                }
                Thread.Sleep(SLEEP_AMOUNT);
            }
            if (tentativa <= 5)
            {
                string[] dirs = Directory.GetFiles(folderRepc, "*TRANSMISSAO*");
                foreach (string dir in dirs)
                {

                    if (File.Exists(dir) && auxConsistencia == 0)
                    {
                        instrucao.preparaSql(dir);
                        instrucao.carregaSql();
                        instrucao.executaSqlBat(0);
                        agrupa_liberacao(numero_pdv());
                        this.lbMsgMainConfig.Invoke((MethodInvoker)delegate { lbMsgMainConfig.Text = "CARREGANDO"; });
                       //lbErroMainConfig.Text = "...";
                    }
                }
                for (z = 0; z <= 18; z++)
                {
                    this.pgbWaitMainConfig.Invoke((MethodInvoker)delegate { pgbWaitMainConfig.Value = z; });

                    auxConsistencia = getStatus(z);

                    if (fiscal_MSG != "")
                    {
                        if (z != 15) //rever funcao
                        {
                            this.lbErroMainConfig.Invoke((MethodInvoker)delegate { this.lbErroMainConfig.Invoke((MethodInvoker)delegate { lbErroMainConfig.Text = fiscal_MSG; }); });
                        }
                    }
                    else if (fiscal_flag == 8)
                    {
                        this.lbErroMainConfig.Invoke((MethodInvoker)delegate { lbErroMainConfig.Text = "ECF INOPERANTE - REDUÇÃO Z JÁ EMITIDA"; });
                    }
                }

                if ("BE091510100011288492" == fiscal_numero_serie || ((valida >= 19 && lbNumeroSerieMainConfig.Text != "Erro de Comunicação !") || (alwaysVariables.ModoOperacao == 0 && valida <= 19)) || (fiscal_numero_serie.Trim() == "EMULADOR"))
                {
                    if (alwaysVariables.AtoCotepe == "0")
                    {
                        //conector_pdv();
                        controle = true;
                    }
                    else
                    {
                        if (alwaysVariables.ModoOperacao == 1 && alwaysVariables.ECF_Ligada != -6)
                        {
                            if (numero_pdv() == Convert.ToInt32(fiscal_numero_caixa.Replace("\0", "").Trim()))
                            {
                                tentativa = 6;
                                this.Hide();
                                auxConsistencia = 1; //Trava a carga anti-horaria
                                ATO0202.ConectorCF02 ecf = new ATO0202.ConectorCF02(fiscal_numero_serie, fiscal_store, fiscal_numero_caixa, fiscal_data_movimento, fiscal_hora_printer, fiscal_data_printer, fiscal_retorno, fiscal_Cupom, fiscal_flag, banco_operador, lbIpTerminalMainConfig.Text, statusBanco, fiscal_GT, path, fiscal_CGC, fiscal_dtusuario_last, fiscal_dtsoft_basico, fiscal_letramf_adicional, fiscal_marca, fiscal_modelo, fiscal_tipo_ecf, fiscal_VersaoFirmware, fiscal_last_reducao_coo, fiscal_last_reducao_cro, fiscal_last_reducao_crz, fiscal_last_reducao_dataMovimento, fiscal_last_reducao_ValorAcumulado, fiscal_last_reducao_Isento_ICMS, fiscal_last_reducao_naoIncide_ICMS, fiscal_last_reducao_St_ICMS, fiscal_last_reducao_Isento_ISSQN, fiscal_last_reducao_naoIncide_ISSQN, fiscal_last_reducao_St_ISSQN, fiscal_last_reducao_desconto_ICMS, fiscal_last_reducao_desconto_ISSQN, fiscal_last_reducao_acrescimo_ICMS, fiscal_last_reducao_acrescimo_ISSQN, fiscal_last_reducao_cancelamento_ICMS, fiscal_last_reducao_cancelamento_ISSQN, fiscal_last_reducao_parcial_not_icms, fiscal_last_reducao_sangria, fiscal_last_reducao_suprimento, fiscal_last_reducao_cancelamento_not_fiscal, fiscal_last_reducao_desconto_not_fiscal, fiscal_last_reducao_acrescimo_not_fiscal, fiscal_trunca_arredonda, fiscal_last_reducao_T18, fiscal_last_reducao_T12, fiscal_last_reducao_T07, fiscal_last_reducao_grandTotal, vetorAliquotaLastZ);
                                ecf.ShowDialog();
                                Environment.Exit(1);
                                //this.Close();
                            }
                            else
                            {
                                this.lbCaixaMainConfig.Invoke((MethodInvoker)delegate { lbCaixaMainConfig.Text += " NÃO LIBERADO."; }); 
                                //lbCaixaMainConfig.Text += " NÃO LIBERADO.";
                            }
                        }
                        else
                        {
                            tentativa = 6;
                            this.Hide();
                            auxConsistencia = 1; //Trava a carga anti-horaria
                            ATO0202.ConectorCF02 ecf = new ATO0202.ConectorCF02(alwaysVariables.ModoOperacao);
                            ecf.ShowDialog();
                            Environment.Exit(1);
                            //this.Close();

                        }
                    }
                }
                else { valida = 0; }
                if (tentativa <= 5)
                {
                    tentativa++;
                    if (tentativa == 6)
                    {
                        lblAtalhoFiscal.Visible = true;
                        lblMenuFiscal.Visible = true;
                    }
                }
                else
                {
                    lblAtalhoFiscal.Visible = true;
                    lblMenuFiscal.Visible = true;
                }
            }
            this.lbTentativasMainConfig.Invoke((MethodInvoker)delegate { lbTentativasMainConfig.Text = tentativa.ToString(); }); 
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lbErroMainConfig_Click(object sender, EventArgs e)
        {

        }

        private void mainConfig_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F11)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Dispose();
            }
            else if (e.Control && e.KeyCode == Keys.W)
            {
                if(tentativa == 6)
                {
                    this.Hide();
                    auxConsistencia = 1; //Trava a carga anti-horaria
                    if (alwaysVariables.AtoCotepe == "0")
                    {
                        MG001.ConectorCF ecf = new MG001.ConectorCF();
                        ecf.ShowDialog();
                    }
                    else
                    {
                        ATO0202.ConectorCF02 ecf02 = new ATO0202.ConectorCF02();
                        ecf02.ShowDialog();
                    }
                    this.Close();
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                
            }
        }

        private void ptbMainConfig_Click(object sender, EventArgs e)
        {

        }

        private void backgroundInitial_DoWork(object sender, DoWorkEventArgs e)
        {
            clock_Tick_tack();
            this.backgroundInitial.CancelAsync();
            this.backgroundInitial.Dispose();
            GC.Collect();
            /*MyProcess.Exited += new EventHandler(myProcess_Exited);
            if (controle == true)
            {
                clock.Start();
            }*/
        }
    }
}

