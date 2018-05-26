using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using System.IO;
//using conectorECFBema;
using Microsoft.VisualBasic;

namespace conectorPDV001
{
    class conectorTEF
    {
        public conectorTEF()
        {
            InicializaVariaveisTEF();
        }
        public conectorTEF(string coo, string store, string caixa)
        {
            InicializaVariaveisTEF();
            fiscal_Cupom_coo = coo;
            fiscal_store = store;
            fiscal_numero_caixa = caixa;            
        }
        //##############################################Bloco de Variaves Encapsuladas###########################################################
            private Boolean auxbImpressaoGerencial;   //Imprimir o comprovante TEF no rel. gerencial
            private Boolean auxbTemImpressao;        //Indica se tem texto para ser impresso no TEF
            private string auxRede;                  //Nome da rede que está sendo realizada a transação TEF
            private string auxTipo;                  //Tipo Transacao
            private string auxParcelamento;          //Tipo Parcelamento
            private string auxQttyParcela;           //Qtty Parcela
            private string auxNSU;                   //Numero Sequencial Unico da transação TEF
            private string auxValorTEF;              //Valor da transação TEF
            private string auxFinalizacao;           //Campo 27 do intpos.001
            private string auxMsgOperador;           //Mensagem a ser exibida para o operador 
            private string fiscal_MSG = "";
            private string auxObs;
            Process myProcess;
            ProcessStartInfo ProcessInfo;
            List<string> mensagemLinha = new List<string>();
            private string mensagem;
            private int fiscal_retorno;
            public const string vbLf = "\n";
            private int ataque = 0;
            private string cupom = "";
            private string coo_file = "";
            private string fiscal_Cupom_coo = new string('\x20', 6);
            private string fiscal_store = new string('\x20', 4);
            private string fiscal_numero_caixa = new string('\x20', 4);            
            private string path;
            private conector_full_variable alwaysVariables = new conector_full_variable();
            private string profile;
            const string folderTef = @"c:\conector\tef\";
            private string NumSequencialTEFOld;
            private dados banco = new dados();
            private instrucaol instrucao;
            private conexaoECF functionECF = new conexaoECF();
            private string strCard = "";
            private int auxConsistencia = 0;
        //##############################################END Bloco de Variaves Encapsuladas#######################################################

        //##############################################Procedimento de Banco####################################################################
           /* public void conectorPDV_inc_cartao(string adm, string ped, string parcela, string qttyParcelas, string typeCartao, string bandeira, string value, string fiscal_cupom, string fiscal_caixa, string auxMaquineta, string obs)
            {
                auxConsistencia = 0;
                try
                {
                    banco.abreConexao();
                    banco.startTransaction("conectorPDV_inc_cartao");
                    banco.addParametro("inc_idAdministradora", adm);// rever situacao tef dedicado / receberá o default system
                    banco.addParametro("inc_idloja", alwaysVariables.Store);
                    banco.addParametro("inc_inclusao", String.Format("{0:yyyyMMdd}", DateTime.Now));
                    //banco.addParametro("inc_alteracao", null);
                    banco.addParametro("inc_cupom", fiscal_cupom.Replace("\0","").Trim());
                    banco.addParametro("inc_pedido", ped);
                    banco.addParametro("inc_terminal", fiscal_caixa.Replace("\0","").Trim());
                    banco.addParametro("inc_emissao", String.Format("{0:yyyyMMdd}", DateTime.Now));
                    //banco.addParametro("inc_vencimento", String.Format("{0:yyyyMMdd}", dtpVencimentoCartao.Value));
                    banco.addParametro("inc_pagamento", "00000000");
                    banco.addParametro("inc_status", "0");
                    banco.addParametro("inc_observacao", obs);
                    banco.addParametro("inc_parcela", parcela);
                    banco.addParametro("inc_qttyParcela", qttyParcelas);
                    banco.addParametro("inc_typeCartao", typeCartao);
                    banco.addParametro("inc_bandeira", bandeira);
                    banco.addParametro("inc_valor", value);
                    banco.addParametro("inc_networkCard", auxMaquineta);
                    banco.addParametro("inc_origem", "c");
                    banco.addParametro("inc_batimento", "n");
                    banco.procedimentoRead();
                    if (banco.retornaRead().Read() == true)
                    {

                    }
                }
                catch (Exception erro)
                {
                    //MessageBox.Show(erro.Message, "Caro Usuário"); 
                    auxConsistencia = 1; 
                }
                finally
                {
                    banco.fechaConexao();
                    if (auxConsistencia == 0)
                    {

                    }

                }
            }*/
            public void conectorPDV_tefInst(string cupom, string store, string cx, string inc_3, string inc_10, string inc_11, string inc_17, string inc_18)
            {
                auxConsistencia = 0;
                try
                {
                    banco.abreConexao();
                    banco.startTransaction("conectorPDV_tefInst");
                    banco.addParametro("inc_numeroCupom", cupom.Replace("\0", ""));
                    banco.addParametro("inc_idloja", store.Replace("\0",""));
                    banco.addParametro("inc_terminal", cx);
                    banco.addParametro("inc_dataVenda", String.Format("{0:yyyyMMdd}", DateTime.Now));
                    banco.addParametro("inc_situacao", "0");
                    banco.addParametro("inc_00300", inc_3);
                    banco.addParametro("inc_01000", inc_10);
                    banco.addParametro("inc_01100", inc_11);
                    banco.addParametro("inc_01700", inc_17);
                    banco.addParametro("inc_01800", inc_18);
                    banco.procedimentoRead();
                    if (banco.retornaRead().Read() == true)
                    {

                    }
                }
                catch (Exception erro)
                {
                    //MessageBox.Show(erro.Message, "Caro Usuário"); 
                    auxConsistencia = 1;
                }
                finally
                {
                    banco.fechaConexao();
                    if (auxConsistencia == 0)
                    {
                        strCard = "";
                        instrucao.setVetorCupom();
                        strCard = instrucao.getCartao(alwaysVariables.UserName, alwaysVariables.Senha, Convert.ToDouble(fiscal_store.Replace("\0", "").Trim()) > 0 ? fiscal_store.Replace("\0", "").Trim() : alwaysVariables.Store, fiscal_Cupom_coo.Replace("\0", "").Trim(), String.Format("{0:yyyyMMdd}", DateTime.Now), cx.Replace("\0", "").Trim());
                        instrucao.carregaInstrucaoMovimentoVenda(instrucao._cartao, strCard, 0);
                        //instrucao.compactScript(instrucao._cartao, "cartao");
                    }

                }
            }
        //##############################################End Procedimento de Banco################################################################
        //##############################################Dicas de Metodos e Funções On-Line#######################################################
            //Fonte http://csharpbrasil.com.br/csharp/dica-funcao-asc-e-chr-com-c-sharp/
        public char Chr(int codigo)  
        {  
            return (char)codigo;  
        }

        public int Asc(string letra)
        {
            return (int)(Convert.ToChar(letra));
        }
        //##############################################End Dicas de Metodos e Funções On-Line###################################################
        //##############################################Metodos, Funçoes e Propertes#############################################################
        public void funcaoADM()
        {
            string NumSequencialTEF = String.Format("{0:MMddHHmmss}", DateTime.Now); //numero sequencia da transacao TEF
            string Mensagem = ""; //solicitacao enviada

            ApagaArquivoResposta();//apaga o intpos.001

            Mensagem = "000-000 = ADM" + System.Environment.NewLine;
            Mensagem += "001-000 = " + NumSequencialTEF + System.Environment.NewLine;
            Mensagem += "999-999 = 0";

            //gera o arquivo de solicitacao
            if (!GeraArquivoSolicitacao(Mensagem))
            {
                return;
            }

            //verifica se a transacao foi ou não aprovada
            
            if (TransacaoAprovadaAdm() != true)
            {
                if (auxMsgOperador != "")
                {
                    //exibe mensagem ao operador;
                    msgInfo msg = new msgInfo(1, auxMsgOperador); msg.ShowDialog();
                    ApagaArquivoResposta();
                }
            }
            else
            {
                auxbImpressaoGerencial = true;
                imprimir(ataque, "", "", "ADM", "0",null,null);
                auxbImpressaoGerencial = false;
            }

        }
        public void preparaFileTef(string caminho, string coo)
        {
                StreamWriter sw = new StreamWriter(caminho + "TEFexe" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(alwaysVariables.Store)) + ".bat", false);
                //                    sw.Write("exit");
                sw.Write(" cd " + caminho + " && ");
                sw.Write(" del " + caminho + "%date:~0,2%%date:~3,2%%date:~6,4%.txt && ");
                sw.Write(" dir /b /s " + caminho + "*" + coo + ".tmp  >  " + caminho + "%date:~0,2%%date:~3,2%%date:~6,4%.txt && ");
                sw.Write(" exit ");
                sw.Close();
                profile = caminho + String.Format("{0:ddMMyyyy}", DateTime.Now) + ".txt";
                exeProcesso(caminho + "TEFexe" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(alwaysVariables.Store)) + ".bat");
        }
        protected void exeProcesso(string stringExe)
        {
            ProcessInfo = new ProcessStartInfo("cmd.exe", "/K " + stringExe);
            ProcessInfo.CreateNoWindow = true;
            ProcessInfo.UseShellExecute = true;
            ProcessInfo.WindowStyle = ProcessWindowStyle.Hidden;
            myProcess = Process.Start(ProcessInfo);
            myProcess.WaitForExit();
            if (myProcess != null)
            {
                myProcess.Close();
            }
            if (File.Exists(stringExe))
            {
                File.Delete(stringExe);
            }
        }
        public void carregaFileTef()
        {
            if (File.Exists(profile))
            {
                using (StreamReader texto = new StreamReader(profile))
                {

                    while ((mensagem = texto.ReadLine()) != null)
                    {
                        mensagemLinha.Add(mensagem.Replace("\\", "//"));
                        //mensagemLinha.Add(mensagem);
                    }
                }
                File.Delete(profile);
            }
        }

        public void carregaFileTefAdm(string adm)
        {
            if (File.Exists(adm))
            {
                using (StreamReader texto = new StreamReader(adm))
                {

                    while ((mensagem = texto.ReadLine()) != null)
                    {
                        mensagemLinha.Add(mensagem.Replace("\\", "//"));
                        //mensagemLinha.Add(mensagem);
                    }
                    texto.Close();
                }
                
            }
        }

        private void InicializaVariaveisTEF()
        {
            Directory.CreateDirectory(folderTef + String.Format("{0:yyyyMMdd}", DateTime.Now));
            //inicializa as variaveis
            auxValorTEF = "";
            auxRede = "";
            auxTipo = "";
            auxQttyParcela = "0";
            auxParcelamento = "0";
            auxNSU = "";
            auxbTemImpressao = false;
            auxFinalizacao = "";
            auxMsgOperador = "";
            auxObs = "";
            instrucao = new instrucaol();
        }
            private Boolean ImprimeComprovante(int teste, string type)
            {
                string a_3, a_10, a_11, a_17, a_18;
                a_3 = a_10 = a_11 = a_17 = a_18 = "";
                bool stop = false;
                ataque = teste;
                //lê o arquivo de resposta e verifica se a transação foi aprovada
                string PathRESP = @"C:\TEF_DIAL\RESP\intpos.001";
                string PathTef = folderTef + String.Format("{0:yyyyMMdd}", DateTime.Now) + "\\";
                StreamReader ArqResposta;
                string LinhaArquivo;
                int Contador;
                int RetECF = 0;
                if (type == "ADM")
                {
                    carregaFileTefAdm(PathRESP);
                }
                else
                {
                    preparaFileTef(PathTef, coo_file);
                    carregaFileTef();
                }
                

                if (!auxbImpressaoGerencial)
                {
                    if (ataque == 0)
                    {
                        //RetECF = conectorECF.Bematech_FI_AbreComprovanteNaoFiscalVinculado(type, "", "");
                        functionECF.conectorECF_AbreComprovanteNaoFiscalVinculado(alwaysVariables.ModeloEcf, type, "", "", ref fiscal_MSG, ref RetECF, alwaysVariables.ECF_Ligada);
                        stop = true;
                    }
                    else
                    {
                        //RetECF = conectorECF.Bematech_FI_AbreComprovanteNaoFiscalVinculado(type, auxValorTEF, cupom);
                        functionECF.conectorECF_AbreComprovanteNaoFiscalVinculado(alwaysVariables.ModeloEcf, type, auxValorTEF, cupom, ref fiscal_MSG, ref RetECF, alwaysVariables.ECF_Ligada);
                    }
                    if (RetECF == 0)//impressora desligada
                    {
                        return false;
                    }
                }

                //trava o mouse e o teclado
                 //conectorECF.Bematech_FI_IniciaModoTEF();
                functionECF.conectorECF_IniciaModoTEF(alwaysVariables.ModeloEcf, ref fiscal_MSG, ref RetECF, alwaysVariables.ECF_Ligada);
                 if (mensagemLinha.Count == 1)
                 {
                     if (!File.Exists(@"C:\0.0"))
                     {
                         System.IO.File.Copy(mensagemLinha[0], @"C:\0.0");
                     }
                 }
                if (mensagemLinha.Count > 0 && type != "ADM")
                {
                    for (int j = 0; j < mensagemLinha.Count; j++)
                    {
                        if (j < mensagemLinha.Count)
                        {
                            stop = true;   
                        }
                        ArqResposta = File.OpenText(mensagemLinha[j]);

                        for (int i = 0; i < 2; i++)
                        {
                            ArqResposta = File.OpenText(mensagemLinha[j]);
                            LinhaArquivo = ArqResposta.ReadLine();
                            while (LinhaArquivo != null)
                            {
                                if (LinhaArquivo.Substring(0, 3) == "029")
                                {
                                    LinhaArquivo = LinhaArquivo.Substring(11, LinhaArquivo.Length - 12);
                                    LinhaArquivo = LinhaArquivo.Replace(Chr(34).ToString(), "");

                                    if (LinhaArquivo == "")
                                    {
                                        LinhaArquivo = vbLf;
                                    }
                                    if (auxbImpressaoGerencial)
                                    {
                                        //RetECF = conectorECF.Bematech_FI_RelatorioGerencial(LinhaArquivo);
                                        functionECF.conectorECF_RelatorioGerencial(alwaysVariables.ModeloEcf, LinhaArquivo, ref fiscal_MSG, ref RetECF, alwaysVariables.ECF_Ligada);
                                    }
                                    else
                                    {
                                        //RetECF = conectorECF.Bematech_FI_UsaComprovanteNaoFiscalVinculado(LinhaArquivo + "\n"); Alterado Flavio, foco comprimir espaço
                                        //RetECF = conectorECF.Bematech_FI_UsaComprovanteNaoFiscalVinculadoTEF(LinhaArquivo);
                                        functionECF.conectorECF_RelatorioGerencial(alwaysVariables.ModeloEcf, LinhaArquivo, ref fiscal_MSG, ref RetECF, alwaysVariables.ECF_Ligada);
                                    }
                                    if (RetECF != 1 && RetECF != -27)
                                    {
                                        ArqResposta.Close();
                                        ArqResposta = null;
                                        //destrava o mouse e o teclado
                                        //conectorECF.Bematech_FI_FinalizaModoTEF();
                                        functionECF.conectorECF_FinalizaModoTEF(alwaysVariables.ModeloEcf, ref fiscal_MSG, ref RetECF, alwaysVariables.ECF_Ligada);
                                        return false;
                                    }
                                }
                                LinhaArquivo = ArqResposta.ReadLine();
                                if (LinhaArquivo != null)
                                {
                                    switch (LinhaArquivo.Substring(0, 7))
                                    {
                                        case "003-000":
                                            a_3 = LinhaArquivo.Substring(10, LinhaArquivo.Length - 10);
                                            break;
                                        case "010-000":
                                            a_10 = LinhaArquivo.Substring(10, LinhaArquivo.Length - 10);
                                            switch (a_10)
                                            {
                                                case "VISANET":
                                                    alwaysVariables.card_band = "01";
                                                    alwaysVariables.card_cnpj = "01027058000191";
                                                    break;
                                                case "REDECARD":
                                                    alwaysVariables.card_cnpj = "01425787000104";
                                                    alwaysVariables.card_band = "02";
                                                    break;
                                                default:
                                                    alwaysVariables.card_band = "99";
                                                    alwaysVariables.card_cnpj = "17351180000159";
                                                    break;
                                            }
                                            break;
                                        case "011-000":
                                            a_11 = LinhaArquivo.Substring(10, LinhaArquivo.Length - 10);
                                            break;
                                        case "017-000":
                                            a_17 = LinhaArquivo.Substring(10, LinhaArquivo.Length - 10);
                                            break;
                                        case "018-000":
                                            a_18 = LinhaArquivo.Substring(10, LinhaArquivo.Length - 10);
                                            break;
                                        case "013-000":
                                            alwaysVariables.card_aut = LinhaArquivo.Substring(10, LinhaArquivo.Length - 10);
                                            break;
                                    }
                                }

                            }//While
                            if (stop == true)
                            {
                                conectorPDV_tefInst(fiscal_Cupom_coo, fiscal_store, fiscal_numero_caixa, a_3, a_10, a_11, a_17, a_18);
                                stop = false;
                            }
                            

                            //se for a primeira via do comprovante salta 5 linhas
                            //entre um comprovante e outro e aguarda 5 segundos
                            //para o operador destacar o comprovante
                            if (i == 1)
                            {
                                //salta 5 linhas entre uma via e outra
                                LinhaArquivo = "\n\n\n\n\n";
                                if (auxbImpressaoGerencial)
                                {
                                    //RetECF = conectorECF.Bematech_FI_RelatorioGerencial(LinhaArquivo);
                                    functionECF.conectorECF_RelatorioGerencial(alwaysVariables.ModeloEcf, LinhaArquivo, ref fiscal_MSG, ref RetECF, alwaysVariables.ECF_Ligada);
                                }
                                else
                                {
                                    //RetECF = conectorECF.Bematech_FI_UsaComprovanteNaoFiscalVinculadoTEF(LinhaArquivo);
                                    functionECF.conectorECF_UsaComprovanteNaoFiscalVinculado(alwaysVariables.ModeloEcf, LinhaArquivo, ref fiscal_MSG, ref RetECF, alwaysVariables.ECF_Ligada);
                                }
                                if (RetECF != 1 && RetECF != -27)
                                {
                                    ArqResposta.Close();
                                    ArqResposta = null;
                                    //destrava o mouse e o teclado
                                    //conectorECF.Bematech_FI_FinalizaModoTEF();
                                    functionECF.conectorECF_FinalizaModoTEF(alwaysVariables.ModeloEcf, ref fiscal_MSG, ref RetECF, alwaysVariables.ECF_Ligada);
                                }

                                Espera(4000);//espera 4 segundos;
                            }
                            if (ArqResposta != null)
                            {
                                ArqResposta.Close();
                                File.Delete(@"C:\TEF_DIAL\RESP\" + "\\" + "0" + ".0");
                            }
                            else
                            {
                                return false;
                            }
                            ArqResposta = null;

                        }//end for
                    }
                }
                else
                {
                    for (int i = 0; i < 1; i++)
                    {
                        ArqResposta = File.OpenText(PathRESP);
                        LinhaArquivo = ArqResposta.ReadLine();
                        while (LinhaArquivo != null)
                        {
                            if (LinhaArquivo.Substring(0, 3) == "029")
                            {
                                LinhaArquivo = LinhaArquivo.Substring(11, LinhaArquivo.Length - 12);
                                LinhaArquivo = LinhaArquivo.Replace(Chr(34).ToString(), "");


                                if (LinhaArquivo == "")
                                {
                                    LinhaArquivo = vbLf;
                                }
                                if (auxbImpressaoGerencial)
                                {
                                    functionECF.conectorECF_RelatorioGerencial(alwaysVariables.ModeloEcf, LinhaArquivo, ref fiscal_MSG, ref RetECF, alwaysVariables.ECF_Ligada);
                                    //RetECF = conectorECF.Bematech_FI_RelatorioGerencial(LinhaArquivo);
                                }
                                else
                                {
                                    //RetECF = conectorECF.Bematech_FI_UsaComprovanteNaoFiscalVinculadoTEF(LinhaArquivo + "\n");
                                    functionECF.conectorECF_UsaComprovanteNaoFiscalVinculado(alwaysVariables.ModeloEcf, LinhaArquivo + "\n", ref fiscal_MSG, ref RetECF, alwaysVariables.ECF_Ligada);
                                }
                                if (RetECF != 1 && RetECF != -27)
                                {
                                    ArqResposta.Close();
                                    ArqResposta = null;
                                    //destrava o mouse e o teclado
                                    //conectorECF.Bematech_FI_FinalizaModoTEF();
                                    functionECF.conectorECF_FinalizaModoTEF(alwaysVariables.ModeloEcf, ref fiscal_MSG, ref RetECF, alwaysVariables.ECF_Ligada);
                                    return false;
                                }
                            }
                            LinhaArquivo = ArqResposta.ReadLine();

                        }//While
                        //se for a primeira via do comprovante salta 5 linhas
                        //entre um comprovante e outro e aguarda 5 segundos
                        //para o operador destacar o comprovante
                        if (i == 1)
                        {
                            //salta 5 linhas entre uma via e outra
                            LinhaArquivo = "\n\n\n\n\n";
                            if (auxbImpressaoGerencial)
                            {
                                //RetECF = conectorECF.Bematech_FI_RelatorioGerencial(LinhaArquivo);
                                functionECF.conectorECF_RelatorioGerencial(alwaysVariables.ModeloEcf, LinhaArquivo, ref fiscal_MSG, ref RetECF, alwaysVariables.ECF_Ligada);
                            }
                            else
                            {
                                functionECF.conectorECF_UsaComprovanteNaoFiscalVinculado(alwaysVariables.ModeloEcf, LinhaArquivo, ref fiscal_MSG, ref RetECF, alwaysVariables.ECF_Ligada);
                                //RetECF = conectorECF.Bematech_FI_UsaComprovanteNaoFiscalVinculadoTEF(LinhaArquivo);
                            }
                            if (RetECF != 1 && RetECF != -27)
                            {
                                ArqResposta.Close();
                                ArqResposta = null;
                                ApagaArquivoResposta();
                                //destrava o mouse e o teclado
                                //conectorECF.Bematech_FI_FinalizaModoTEF();
                                functionECF.conectorECF_FinalizaModoTEF(alwaysVariables.ModeloEcf, ref fiscal_MSG, ref RetECF, alwaysVariables.ECF_Ligada);
                            }

                            Espera(4000);//espera 4 segundos;
                        }

                        ArqResposta.Close();
                        File.Delete(@"C:\TEF_DIAL\RESP\" + "\\" + "0" + ".0");
                        if (i == 1)
                        {
                            //ApagaArquivoResposta();   
                        }
                        ArqResposta = null;

                    }//end for
                }
                //RetECF = conectorECF.Bematech_FI_AcionaGuilhotinaMFD(1);
                //functionECF.conectorECF_AcionaGuilhotinaMFD(alwaysVariables.ModeloEcf, 1 , ref fiscal_MSG, ref RetECF, alwaysVariables.ECF_Ligada);
                //RetECF = conectorECF.Bematech_FI_FechaComprovanteNaoFiscalVinculado();
                functionECF.conectorECF_FechaComprovanteNaoFiscalVinculado(alwaysVariables.ModeloEcf, ref fiscal_MSG, ref RetECF, alwaysVariables.ECF_Ligada);
                mensagemLinha = new List<string>();
                //destrava o mouse e o teclado
                //conectorECF.Bematech_FI_FinalizaModoTEF();
                functionECF.conectorECF_FinalizaModoTEF(alwaysVariables.ModeloEcf, ref fiscal_MSG, ref RetECF, alwaysVariables.ECF_Ligada);

                return true;
            }
            private Boolean GeraArquivoSolicitacao(string Mensagem)
            {
                string PathREQ = @"C:\TEF_DIAL\REQ";   //path do arquivo de solicitacao
                string PathRESP = @"C:\TEF_DIAL\RESP"; //path do arquivo de resposta
                StreamWriter Arquivo;       //arquivo de solicitacao
                try
                {
                    if (File.Exists(PathREQ + "\\intpos.tmp"))
                    {
                        File.Delete(PathREQ + "\\intpos.tmp");
                    }

                    if (File.Exists(@"C:\\TEF_DIAL\\REQ\\intpos.001"))
                    {
                        File.Delete(@"C:\\TEF_DIAL\\REQ\\intpos.001");
                    }
                    if (File.Exists(@"C:\0.0"))
                    {
                        File.Delete(PathREQ + "\\intpos.tmp");
                        File.Delete(@"C:\\TEF_DIAL\\REQ\\intpos.001");
                    }

                    //cria o arquivo de solicitação temporariamente
                    Arquivo = File.CreateText(PathREQ + "\\intpos.tmp");
                    Arquivo.Write(Mensagem);
                    Arquivo.Close();
                    Arquivo = null;
                    //renomeia o arquivo para intpos.001
                    System.IO.File.Move(PathREQ + "\\intpos.tmp", PathREQ + "\\intpos.001");

                    //aguarda até 7 segundos para receber a confirmação de recebimento 
                    //da(solicitacao). Se não receber exibe a mensagem de gerenciador
                    //padrão inativo
                    int contador = 0;
                    while ((!File.Exists(PathRESP + "\\intpos.sts")) && contador < 70)
                    {
                        Espera(100);
                        contador++;
                    }

                    if (contador < 70)
                    {
                        File.Delete(PathRESP + "\\intpos.sts");
                        return true;
                    }
                    else
                    {
                        if (File.Exists(PathREQ + "\\intpos.001"))
                        {
                            File.Delete(PathREQ + "\\intpos.001");
                        }
                        msgInfo msg = new msgInfo("O Gerenciador Padrão não está ativo!"); msg.ShowDialog();
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    msgInfo msg = new msgInfo("ERRO FATAL...! " + ex.ToString()); msg.ShowDialog();
                    return false;
                }
            }
            //verifica se a transacao foi ou não aprovada
            public Boolean TransacaoAprovada(string coo, string type, string ped)
            {
                //lê o arquivo de resposta e verifica se a transação foi aprovada
                string PathRESP = @"C:\TEF_DIAL\RESP";
                StreamReader ArqResposta;
                string LinhaArquivo;
                string PathTef = folderTef + String.Format("{0:yyyyMMdd}", DateTime.Now);
                string NumSequencialTEF = String.Format("{0:MMddHHmmss}", DateTime.Now);
                Boolean ok = false;

                //aguarda receber o arquivo de resposta IntPos.001

                while (true)
                {
                    if (File.Exists(PathRESP + "\\intpos.001"))
                    {
                        //renomeia o arquivo para intpos.001
                        if (coo != "0")
                        {
                            System.IO.File.Copy(PathRESP + "\\intpos.001", PathTef + "\\" + NumSequencialTEF + type + coo + ".tmp");   
                        }
                        break;
                    }
                }
                /*try
                {
                    ArqResposta = new StreamReader(PathRESP + "\\intpos.001", System.Text.Encoding.UTF7);
                }
                catch (Exception)
                {
                    
                    msgInfo msg = new msgInfo("Arquivo não encontrado, uma verificação será realizada."); msg.ShowDialog();
                    ArqResposta = new StreamReader(PathRESP + "\\intpos.001", System.Text.Encoding.UTF7);
                }*/
                ArqResposta = new StreamReader(PathRESP + "\\intpos.001", System.Text.Encoding.UTF7);
                LinhaArquivo = ArqResposta.ReadLine();
                while (LinhaArquivo != null)
                {
                    //campo 003 - VALOR
                    if (LinhaArquivo.Substring(0, 3) == "003")
                    {
                        auxValorTEF = LinhaArquivo.Substring(10);
                    }
                    //campo 009 - verifica se a transação foi aprovada
                    if (LinhaArquivo.Substring(0, 3) == "009")
                    {
                        if (LinhaArquivo.Substring(10, LinhaArquivo.Length - 10) == "0")
                        {
                            ok = true;
                        }
                        else
                        {
                            ok = false;
                        }
                    }

                    if (LinhaArquivo.Substring(0, 3) == "011") 
                    {
                        switch (LinhaArquivo.Substring(0, 3))
                        {
                            case "10":
                                auxTipo = "2"; //Credito
                                break;
                            case "11":
                                auxTipo = "2"; //Credito
                                break;
                            case "12":
                                auxTipo = "4"; //Credito Par. Emissao
                                break;
                            case "20":
                                auxTipo = "1"; //Debito
                                break;
                            case "22":
                                auxTipo = "1"; //Debito
                                break;
                            case "21":
                                auxTipo = "1"; //Debito
                                break;
                            case "24":
                                auxTipo = "1"; //Debito
                                break;
                            case "40": // CDC
                                auxTipo = "5";
                                break;
                            case "60": // VOUCHE
                                auxTipo = "3";
                                break;
                            default:
                                auxTipo = "99"; // Não Localizado
                                break;
                        }
                    }else if (LinhaArquivo.Substring(0, 3) == "010") //campo 010 - REDE
                    {
                        auxRede = LinhaArquivo.Substring(10);
                    }
                    else if (LinhaArquivo.Substring(0, 3) == "012")
                    {
                        auxNSU = LinhaArquivo.Substring(10); //campo 012 - NSU
                    }
                    else if (LinhaArquivo.Substring(0, 3) == "017")
                    {
                        auxParcelamento = LinhaArquivo.Substring(10); //campo 012 - NSU
                    }
                    else if (LinhaArquivo.Substring(0, 3) == "018")
                    {
                        auxQttyParcela = LinhaArquivo.Substring(10); //campo 018
                    }
                    else if (LinhaArquivo.Substring(0, 3) == "027")
                    {
                        auxFinalizacao = LinhaArquivo.Substring(10); //campo 027 - Finalizacao
                    }
                    else if (LinhaArquivo.Substring(0, 3) == "028")
                    {
                        if (Convert.ToInt32(LinhaArquivo.Substring(10, LinhaArquivo.Length - 10)) > 0)
                        {
                            auxbTemImpressao = true; //campo 028 - qtde de linhas para serem impressas   
                        }
                    }
                    else if (LinhaArquivo.Substring(0, 3) == "030")
                    {
                        auxMsgOperador = LinhaArquivo.Substring(10); //campo 030 - Mensagem ao operador
                    }
                    else if (LinhaArquivo.Substring(0, 3) == "711-003")
                    {
                        auxObs =LinhaArquivo.Substring(15);
                    }
                    else if (LinhaArquivo.Substring(0, 3) == "711-003")
                    {
                        //auxObs += "VENDA CREDITO A VISTA";
                        auxObs += LinhaArquivo;
                    }
                    LinhaArquivo = ArqResposta.ReadLine();
                }
                if (Convert.ToDecimal(auxValorTEF.Replace(",",".")) > 0)
                {
                    //conectorPDV_inc_cartao(alwaysVariables.Adm, ped, auxParcelamento, auxQttyParcela, auxTipo, "1", auxValorTEF, coo, alwaysVariables.NumeroCaixa, "3", auxObs);   
                    ConfirmaTransacao(); //Alteração Multi Cartões Flávio
                }
                ArqResposta.Close();
                ArqResposta = null;
                ApagaArquivoResposta();
                return ok;
            }


            public Boolean TransacaoAprovadaAdm()
            {
                //lê o arquivo de resposta e verifica se a transação foi aprovada
                string PathRESP = @"C:\TEF_DIAL\RESP";
                StreamReader ArqResposta;
                string LinhaArquivo;
                string PathTef = folderTef + String.Format("{0:yyyyMMdd}", DateTime.Now);
                string NumSequencialTEF = String.Format("{0:MMddHHmmss}", DateTime.Now);
                Boolean ok = false;

                //aguarda receber o arquivo de resposta IntPos.001

                while (true)
                {
                    //NaoConfirmaTransacao(0);
                    if (File.Exists(PathRESP + "\\intpos.001"))
                    {
                        //renomeia o arquivo para intpos.001
                            System.IO.File.Copy(PathRESP + "\\intpos.001", PathTef + "\\" + NumSequencialTEF + "ADM.tmp");
                        break;
                    }
                }
                ArqResposta = new StreamReader(PathRESP + "\\intpos.001", System.Text.Encoding.UTF7);
                LinhaArquivo = ArqResposta.ReadLine();

                while (LinhaArquivo != null)
                {
                    //campo 003 - VALOR
                    if (LinhaArquivo.Substring(0, 3) == "003")
                    {
                        auxValorTEF = LinhaArquivo.Substring(10);
                    }
                    //campo 009 - verifica se a transação foi aprovada
                    if (LinhaArquivo.Substring(0, 3) == "009")
                    {
                        if (LinhaArquivo.Substring(10, LinhaArquivo.Length - 10) == "0")
                        {
                            ok = true;
                        }
                        else
                        {
                            ok = false;
                        }
                    }
                    //campo 010 - REDE

                    if (LinhaArquivo.Substring(0, 3) == "010")
                    {
                        auxRede = LinhaArquivo.Substring(10);
                    }
                    else if (LinhaArquivo.Substring(0, 3) == "012")
                    {
                        auxNSU = LinhaArquivo.Substring(10); //campo 012 - NSU
                    }
                    else if (LinhaArquivo.Substring(0, 3) == "027")
                    {
                        auxFinalizacao = LinhaArquivo.Substring(10); //campo 027 - Finalizacao
                    }
                    else if (LinhaArquivo.Substring(0, 3) == "028")
                    {
                        if (Convert.ToInt32(LinhaArquivo.Substring(10, LinhaArquivo.Length - 10)) > 0)
                        {
                            auxbTemImpressao = true; //campo 028 - qtde de linhas para serem impressas   
                        }
                    }
                    else if (LinhaArquivo.Substring(0, 3) == "030")
                    {
                        auxMsgOperador = LinhaArquivo.Substring(10); //campo 030 - Mensagem ao operador
                    }
                    LinhaArquivo = ArqResposta.ReadLine();
                }
                ArqResposta.Close();
                ArqResposta = null;
                return ok;
            }

            public Boolean enviaSolicitacao(string valorTEF, string coo, string type, string pedido)
            {

                string NumSequencialTEF;
                string Mensagem;

                //inicializa as variaveis usadas no TEF
                InicializaVariaveisTEF();

                try
                {
                    if (File.Exists(@"C:\\TEF_DIAL\\RESP\\intpos.001"))
                    {
                        File.Delete(@"C:\\TEF_DIAL\\RESP\\intpos.001");
                    }
                }
                catch (Exception ex)
                {

                }

                NumSequencialTEF = String.Format("{0:MMddHHmmss}", DateTime.Now);

                //auxvalorTEF = auxvalorTEF.Replace(",", "")
                //monta o arquivo de solicitação do TEF

                        Mensagem = "000-000 = CRT" + System.Environment.NewLine;
                        Mensagem += "001-000 = " + NumSequencialTEF + System.Environment.NewLine;
                        Mensagem += "003-000 = " + valorTEF.Replace(",", "").Trim() + System.Environment.NewLine;
                        Mensagem += "706-000 = 3" + System.Environment.NewLine;
                        Mensagem += "999-999 = 0";

                //gera o arquivo de solicitacao
                   if (!GeraArquivoSolicitacao(Mensagem))
                   {
                       return false;
                   }

                //verifica se a transacao foi ou não aprovada
                   
                
                if (TransacaoAprovada(coo.Replace("\0", "").Trim(),type,pedido) != true)
                   {
                       if (auxMsgOperador != "")
                       {
                           //exibe mensagem ao operador;
                           msgInfo msg = new msgInfo(1,auxMsgOperador); msg.ShowDialog();
                           ApagaArquivoResposta();   
                       }
                       return false;
                   }
                   else
                   {
                       return true;
                   }
            }
            public Boolean NaoConfirmaTransacao(string aux, string rede)
            {
                string NumSequencialTEF;
                string Mensagem;

                NumSequencialTEF = String.Format("{0:MMddHHmmss}", DateTime.Now);

                //auxvalorTEF = auxvalorTEF.Replace(",", "")
                //monta o arquivo de solicitação do TEF
                Mensagem = "000-000 = NCN" + System.Environment.NewLine;
                Mensagem += "001-000 = " + NumSequencialTEF + System.Environment.NewLine;
                Mensagem += "010-000 = " + rede + System.Environment.NewLine;
                //Mensagem += "012-000 = " + auxNSU + System.Environment.NewLine;
                //Mensagem += "027-000 = " + auxFinalizacao + System.Environment.NewLine;
                Mensagem += "027-000 = " + aux + System.Environment.NewLine;
                Mensagem += "733-000 = 211" + System.Environment.NewLine;
               // Mensagem += "738-000 = aaa" + System.Environment.NewLine;
                Mensagem += "999-999 = 0";
                /*Mensagem = "000-000 = CRT" + System.Environment.NewLine;
                Mensagem += "001-000 = " + NumSequencialTEF + System.Environment.NewLine;
                Mensagem += "003-000 = " + auxValorTEF.Replace(",", "").Trim() + System.Environment.NewLine;
                Mensagem += "999-999 = 0";
                 ou
                 * 
                 * Mensagem = "000-000 = CNC" + vbCrLf + _
                "001-000 = " + NumSeqCNCTEF + vbCrLf + _
                "003-000 = " + Trim(ValorTEF.Replace(",", "")) + vbCrLf + _
                "010-000 = " + Rede + vbCrLf + _
                "012-000 = " + NSU + vbCrLf + _
                "022-000 = " + DT_Comprovante + vbCrLf + _
                "023-000 = " + HT_Comprovante + vbCrLf + _
                "701-000 = " + Trim(NomeAplicativo) + "," + Trim(VerAplicativo) + vbCrLf + _
                "706-000 = " + Trim(frmPDVMAX_CaixaLivre.HabTEFFunc) + vbCrLf + _
                "716-000 = " + Trim(RazaoSocial) + vbCrLf
                 
                 */



                if (GeraArquivoSolicitacao(Mensagem))
                {
                    if (auxValorTEF != "")
                    {
                        msgInfo msg = new msgInfo("Última Transação TEF inconsistente, verifique o STATUS na central de cartões, informações. Rede: " + auxRede + " NSU: " + auxNSU + " VALOR: " + auxValorTEF); msg.ShowDialog();
                    }
                    else
                    {
                        msgInfo msg = new msgInfo("Última Transação TEF inconsistente, verifique o STATUS na central de cartões, informações."); msg.ShowDialog();
                    }

                    //se a confirmacao for aceita, apaga o intpos.001
                    ApagaArquivoResposta();
                    if (auxRede != "")
                    {
                        if (File.Exists(@"C:\0.0"))
                        {
                            File.Delete(@"C:\0.0");
                        }
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
            public void imprimir(int teste, string valor, string nro, string type, string nro2, string store, string caixa)
            {
                fiscal_store = store;
                fiscal_numero_caixa = caixa;            
                auxValorTEF = valor;
                fiscal_Cupom_coo = nro2;
                cupom = nro;
                coo_file = nro2;
                ataque = teste;
                string result = "";
                if (auxbTemImpressao)
                {
                    if (auxMsgOperador != "")
                    {
                        //exibe mensagem ao operador;
                        msgInfo msg = new msgInfo(1, auxMsgOperador); msg.ShowDialog();
                    }
                    while (true)
                    {
                        if (ImprimeComprovante(teste, type) != true)
                        {
                            msgInfo msg = new msgInfo(1, "IMPRESSORA FISCAL NÃO RESPONDE, TENTE IMPRIMIR NOVAMENTE PRESSIONANDO A TECLA [ F12 ].");
                            if (msg.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
                            {

                                //Envia o NSU
                                NaoConfirmaTransacao(auxFinalizacao, auxRede);

                                //functionECF.conectorECF_AcionaGuilhotinaMFD(alwaysVariables.ModeloEcf, 1 , ref fiscal_MSG, ref RetECF, alwaysVariables.ECF_Ligada);
                                //RetECF = conectorECF.Bematech_FI_FechaComprovanteNaoFiscalVinculado();
                                //fecha o comprovante
                                //fiscal_retorno = conectorECF.Bematech_FI_FechaComprovanteNaoFiscalVinculado();
                                functionECF.conectorECF_FechaComprovanteNaoFiscalVinculado(alwaysVariables.ModeloEcf, ref fiscal_MSG, ref fiscal_retorno, alwaysVariables.ECF_Ligada);
                                //apaga o intpos.001
                                ApagaArquivoResposta();
                                break;//sair do While
                            }
                            else
                            {
                                //functionECF.conectorECF_AcionaGuilhotinaMFD(alwaysVariables.ModeloEcf, 1 , ref fiscal_MSG, ref RetECF, alwaysVariables.ECF_Ligada);
                                //RetECF = conectorECF.Bematech_FI_FechaComprovanteNaoFiscalVinculado();
                                functionECF.conectorECF_FechaComprovanteNaoFiscalVinculado(alwaysVariables.ModeloEcf, ref fiscal_MSG, ref fiscal_retorno,alwaysVariables.ECF_Ligada);

                                auxbImpressaoGerencial = true;

                            }

                        }
                        else
                        {
                            //envia o CNF
                            ConfirmaTransacao(); //Alteração Multi Cartões Flávio

                            //apaga o intpos.001
                            ApagaArquivoResposta();
                            break; //Exit While
                        }
                    }//End While
                    auxbImpressaoGerencial = false;
                }//End If
                //zera as variaveis TEF
                InicializaVariaveisTEF();

            }
            public Boolean ConfirmaTransacao()
            {
                
                string Mensagem;

                string NumSequencialTEF = String.Format("{0:MMddHHmmss}", DateTime.Now);
                if(NumSequencialTEFOld == null)
                NumSequencialTEFOld = NumSequencialTEF;

                Mensagem = "000-000 = CNF" + System.Environment.NewLine;
                Mensagem += "001-000 = " + NumSequencialTEF + System.Environment.NewLine;
                Mensagem += "010-000 = " + auxRede + System.Environment.NewLine;
                Mensagem += "012-000 = " + auxNSU + System.Environment.NewLine;
                Mensagem += "027-000 = " + auxFinalizacao + System.Environment.NewLine;
                Mensagem += "706-000 = 3" + System.Environment.NewLine;
                Mensagem += "999-999 = 0";

                if (GeraArquivoSolicitacao(Mensagem))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            private void Espera(double Milesegundos)
            {
                DateTime TempoInicial;
                DateTime TempoFinal;
                TimeSpan DiferencaTempo;
                Double Diferenca;
                TempoInicial = DateTime.Now;
                do
                {
                    TempoFinal = DateTime.Now;
                    DiferencaTempo = TempoFinal.Subtract(TempoInicial);
                    Diferenca = DiferencaTempo.TotalMilliseconds;

                } while (Diferenca < Milesegundos);
            }
            private void ApagaArquivoResposta()
            {
                try
                {
                    if (File.Exists(@"C:\TEF_DIAL\RESP\intpos.001"))
                    {
                        File.Delete(@"C:\TEF_DIAL\RESP\intpos.001");
                    }
                }
                catch (Exception ex)
                {
                    //throw;
                }
            }

        //#############################################End Metodos, Funçoes e Propertes###########################################################

    }
}
