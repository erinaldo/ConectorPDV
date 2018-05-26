using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;

namespace conectorPDV001
{
    class instrucaol : conector_full_variable //Exemplo de Herança
    {
        public instrucaol()
        {
            InitializeComponent();

            if (myPass != "")
            {
                if (myUsers != "")
                {
                    if (alwaysVariables.TypeComunicacao != "")
                    {
                        if (alwaysVariables.TypeComunicacao == "0")
                        {
                            transmissaoCaminho = alwaysVariables.TransmissaoWindows;
                            recepcaoCaminho = alwaysVariables.RecepcaoWindows;
                        }
                        else
                        {
                            transmissaoCaminho = alwaysVariables.TransmissaoLinux;
                            recepcaoCaminho = alwaysVariables.RecepcaoLinux;
                        }
                    }
                }
            }
        }
        
        //#########################################################Variavel Enpsulada########################################################
        private string myUsers;
        private string myPass;
        ProcessStartInfo ProcessInfo;
        //Process myProcess;
        List<string> mensagemLinha = new List<string>();
        List<string> mensagemLinhaReparo = new List<string>();
        private string fiscal_md5_line = "";
        private string mensagem;
        private string path;
        private string strCupom;
        private string profile;
        private int auxConsistencia = 0;
        private conector_full_variable alwaysVariables = new conector_full_variable();
        private algMd5 key = new algMd5();
        //#########################################################End Variavel Enpsulada####################################################
        //#########################################################Variavel & Constantes#######################################################
        private dados banco = new dados();
        const string folderMaster = @"c:\conector\";
        const string folderTrSlave = @"c:\conector\Transmissao";
        const string folderRcSlave = @"c:\conector\Recepcao";
        private string typeComunicacao = "0";// 0 - Windows 1 - linux
        private string transmissaoCaminho;
        private string recepcaoCaminho;
        protected string cupom_cabecalho;
        protected string cupom_cabecalhoCT;
        protected string cupom_detalhes;
        protected string cupom_detalhesCT;
        protected string cupom_movimento;
        protected string mapa_cabecalho;
        protected string mapa_movimento;
        protected string aliquotas;
        protected string fechamentoCaixa;
        protected string cupom_cartao;
        protected string cupom_cheque;
        protected string cupom_convenio;
        protected string detalhes_md5;
        protected string caminho_md5;
        protected string nfe;
        protected string nfce;
        protected string nfceItem;
        protected string nfImposto;
        int countCupom = 0;
        int countNfce = 0;
        int countNfImposto = 0;
        int countNfceItem = 0;
        int countNfe = 0;
        int countMapa = 0;
        //#########################################################Vetor Variavel Cupom
        string[] vetorCupomCabecalho = new string[48];
        string[] vetorCupomCartao = new string[48];
        string[] vetorCupomCheque = new string[48];
        string[] vetorCupomConvenio = new string[48];
        string[] vetorCupomCabecalhoCT = new string[48];
        string[] vetorCupomMovimento = new string[48];
        string[] vetorCupomDetalhes = new string[10];//MG.001
        string[] vetorCupomDetalhesCT = new string[10];
        string[] vetorMapa = new string[10];
        string[] vetorMapaMovimento = new string[10];
        string[] vetorAliquota = new string[10];
        string[] vetorfechamentoCaixa = new string[10];
        //#########################################################End Variavel & Constantes###################################################
        //#########################################################Vetor Variavel Nfce/Nfe
        string[] vetorNfe = new string[48];
        string[] vetorNfce = new string[48];
        string[] vetorNfceItem = new string[1000];
        string[] vetorNfImposto = new string[48];
        //#########################################################End Variavel & Constantes###################################################


        //#########################################################Metodos, Funçoes e Propartes #################################################
        private void InitializeComponent()
        {
            Directory.CreateDirectory(folderMaster);
            Directory.CreateDirectory(folderTrSlave);
            Directory.CreateDirectory(folderRcSlave);
            myUsers = UserName;
            myPass = Senha;
            path = String.Format("{0:ddMMyyyy}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "TRANSMISSAO" + ".rar";
        }
        protected void exeProcesso(string stringExe, string stringMD5, Int16 tipo)
        {

            Process myProcess;
            ProcessInfo = new ProcessStartInfo("cmd.exe", "/K " + stringExe);
            ProcessInfo.CreateNoWindow = true;
            ProcessInfo.UseShellExecute = true;
            ProcessInfo.WindowStyle = ProcessWindowStyle.Hidden;
            myProcess = Process.Start(ProcessInfo);
            try
            {
                if (tipo == 0)
                { myProcess.WaitForExit(); }
                else
                {
                    Thread.Sleep(18000);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                myProcess.Close();
            }
            if (stringMD5 != "#")
            {
                detalhes_md5 = key.retornoFileMD5(stringMD5);   
            }
            if (myProcess != null)
            {
                myProcess.Close();
            }
            if (File.Exists(stringExe))
            {
                try
                {
                    File.Delete(stringExe);
                }
                catch (Exception)
                {
                    
                }
            }
        }
        public static string Right(string param, int length)
        {
            //start at the index based on the lenght of the sting minus
            //the specified lenght and assign it a variable
            string result = param.Substring(param.Length - length, length);
            //return the result of the operation
            return result;
        }
        public string _strCupom
        {
            get
            {
                return strCupom;
            }
            set
            {
                strCupom = value;
            }
        }
        public string[] _cabecalho
        {
            get
            {
                return vetorCupomCabecalho;
            }
            set
            {
                vetorCupomCabecalho = value;
            }
        }
        public string[] _cabecalhoCT
        {
            get
            {
                return vetorCupomCabecalhoCT;
            }
            set
            {
                vetorCupomCabecalhoCT = value;
            }
        }
        public string[] _detalhes
        {
            get
            {
                return vetorCupomDetalhes;
            }
            set
            {
                vetorCupomDetalhes = value;
            }
        }
        public string[] _cartao
        {
            get
            {
                return vetorCupomCartao;
            }
            set
            {
                vetorCupomCartao = value;
            }
        }

        public string[] _nfe
        {
            get
            {
                return vetorNfe;
            }
            set
            {
                vetorNfe = value;
            }
        }

        public string[] _nfce
        {
            get
            {
                return vetorNfce;
            }
            set
            {
                vetorNfce = value;
            }
        }

        public string[] _nfceItem
        {
            get
            {
                return vetorNfceItem;
            }
            set
            {
                vetorNfceItem = value;
            }
        }

        public string[] _nfImposto
        {
            get
            {
                return vetorNfImposto;
            }
            set
            {
                vetorNfImposto = value;
            }
        }
        public string[] _cheque
        {
            get
            {
                return vetorCupomCheque;
            }
            set
            {
                vetorCupomCheque = value;
            }
        }

        public string[] _convenio
        {
            get
            {
                return vetorCupomConvenio;
            }
            set
            {
                vetorCupomConvenio = value;
            }
        }
        public string[] _detalhesCT
        {
            get
            {
                return vetorCupomDetalhesCT;
            }
            set
            {
                vetorCupomDetalhesCT = value;
            }
        }

        public string[] _movimentoDia
        {
            get
            {
                return vetorMapa;
            }
            set
            {
                vetorMapa = value;
            }
        }
        public string _caminhoMD5
        {
            get
            {
                return caminho_md5;
            }
            set
            {
                caminho_md5 = value;
            }
        }
        public string _detalhesMD5
        {
            get
            {
                return detalhes_md5;
            }
            set
            {
                detalhes_md5 = value;
            }
        }

        public string[] _movimentoResumo
        {
            get
            {
                return vetorMapaMovimento;
            }
            set
            {
                vetorMapaMovimento = value;
            }
        }

        public string[] _aliquotas
        {
            get
            {
                return vetorAliquota;
            }
            set
            {
                vetorAliquota = value;
            }
        }

        public string[] _fechamentoCaixa
        {
            get
            {
                return vetorfechamentoCaixa;
            }
            set
            {
                vetorfechamentoCaixa = value;
            }
        }

        public string[] _movimento
        {
            get
            {
                return vetorCupomMovimento;
            }
            set
            {
                vetorCupomMovimento = value;
            }
        }
        public void carregaSql()
        {
            if (File.Exists(profile))
            {
                using (StreamReader texto = new StreamReader(profile))
                {

                    while ((mensagem = texto.ReadLine()) != null)
                    {
                        mensagemLinha.Add(mensagem.Replace("\\","//"));
                        //mensagemLinha.Add(mensagem);
                    }
                }
                File.Delete(profile);
            }
        }

        public void carregaListaReparo()
        {
            DirectoryInfo Dir = new DirectoryInfo(@"c:\conector\transmissao\");
            mensagemLinhaReparo = new List<string>();
            // Busca automaticamente todos os arquivos em todos os subdiretórios
            FileInfo[] Files = Dir.GetFiles("*.sql", SearchOption.AllDirectories);
            foreach (FileInfo File in Files)
            {
                mensagemLinhaReparo.Add(File.FullName);
            }
            if (mensagemLinhaReparo.Count > 0)
            {
                string strTemp = "c://conector//Transmissao//" + "ReparoExeCupom" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + ".bat";
                StreamWriter sw = new StreamWriter(strTemp, false);
                //sw.Write("set data=CONFIG%date:~0,2%%date:~3,2%%date:~6,4% && ");

                for (int h = 0; h < mensagemLinhaReparo.Count; h++)
                {
                    try
                    {
                        if (!File.Exists(strTemp))
                        {
                            sw = new StreamWriter(strTemp, false);
                            sw.Write(" cd " + recepcaoCaminho + " && ");
                        }
                        //string test = mensagemLinhaReparo[h].ToString().Substring(0, mensagemLinhaReparo[h].IndexOf("."));
                        if (mensagemLinhaReparo.Count > 0)
                        {
                            sw.Write("cd " + "c://conector//Transmissao//" + " && ");
                            sw.Write("del " + mensagemLinhaReparo[h].ToString().Substring(0, mensagemLinhaReparo[h].IndexOf(".")) + ".rar" + " && ");
                            sw.Write("\"" + alwaysVariables.ecfPrinter + "\"" + " a -ibck -o+ -r -V1500000 \"" + mensagemLinhaReparo[h].ToString().Substring(0, mensagemLinhaReparo[h].IndexOf(".")) + ".rar" + "\"  \"" + mensagemLinhaReparo[h].ToString().Substring(0, mensagemLinhaReparo[h].IndexOf(".")) + ".sql\" && ");
                            sw.Write("del " + mensagemLinhaReparo[h].ToString().Substring(0, mensagemLinhaReparo[h].IndexOf(".")) + ".sql" + " && ");
                        }
                        if (h != 0 && (h % 2) == 0)
                        {

                            sw.Write(" exit ");

                            sw.Close();
                            processo.exeProcesso(strTemp, "#", 0, ref detalhes_md5);
                        }
                    }
                    catch (Exception erro)
                    {
                        File.Delete(strTemp);
                    }
                }
                if (File.Exists(strTemp))
                {
                    sw.Write(" exit ");
                    sw.Close();
                    processo.exeProcesso(strTemp, "#", 0, ref detalhes_md5);
                }
            }
            mensagemLinhaReparo = null;
        }
        public void executaXmlBat()
        {
            if (mensagemLinha.Count > 0)
            {
                string strTroca = "";
                string strTemp = "c:\\conector\\Recepcao\\XMLexe" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + ".bat";
                StreamWriter sw = new StreamWriter(strTemp, false);
                sw.Write(" cd c:\\conector\\Recepcao\\ && ");
                for (int i = 0; i < mensagemLinha.Count; i++)
                {
                    strTroca = mensagemLinha[i].Substring(50, mensagemLinha[i].IndexOf(".") - 50) == "licenca_ecf" ? "liberacao" : mensagemLinha[i].Substring(50, mensagemLinha[i].IndexOf(".") - 50);
                    sw.Write(" mysql --user=" + alwaysVariables.UserName + " -A conectorPDV  --execute=\"LOAD XML LOCAL INFILE '" + mensagemLinha[i] + "' INTO TABLE conectorPDV." + strTroca + "\" --password=" + alwaysVariables.Senha + " && ");
                    sw.Write(" del c:\\conector\\Recepcao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + mensagemLinha[i].Substring(50, mensagemLinha[i].IndexOf(".") - 50) + ".xml && ");
                }
                sw.Write(" exit ");
                sw.Close();
                exeProcesso(strTemp, "#",0);
            }
        }
        public void executaSqlBat(Int16 tipo)
        {
            if (mensagemLinha.Count > 0)
            {
                string strTroca = "";
                StreamWriter sw = new StreamWriter(recepcaoCaminho + "SQLexe" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + ".bat", false);
                sw.Write(" cd " + recepcaoCaminho + " && ");
                for (int i = 0; i < mensagemLinha.Count; i++)
                {
                    strTroca = mensagemLinha[i].Substring(50, mensagemLinha[i].IndexOf(".") - 50) == "licenca_ecf" ? "liberacao" : mensagemLinha[i].Substring(50, mensagemLinha[i].IndexOf(".") - 50) == "liberacao_ok" ? "licenca_ecf" : mensagemLinha[i].Substring(50, mensagemLinha[i].IndexOf(".") - 50);
                    sw.Write(" mysql --user=" + alwaysVariables.UserName + " -A conectorPDV  --execute=\"SET foreign_key_checks=0; LOAD DATA LOCAL INFILE '" + mensagemLinha[i] + "' REPLACE INTO TABLE conectorPDV." + strTroca + " FIELDS TERMINATED BY '|' " + "\" --password=" + alwaysVariables.Senha + " && ");
                    sw.Write(" del " + recepcaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + mensagemLinha[i].Substring(50, mensagemLinha[i].IndexOf(".") - 50) + ".sql && ");
                }
                sw.Write(" SET foreign_key_checks=1; && exit ");
                sw.Close();
                processo.exeProcesso(recepcaoCaminho + "SQLexe" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + ".bat", "#", tipo, ref detalhes_md5);

            }
        }
        public void executaSql()
        {
            if (mensagemLinha.Count > 0)
            {
                for (int i = 0; i < mensagemLinha.Count; i++)
                {
                    conector_import_resource(mensagemLinha[i], mensagemLinha[i].Substring(50, mensagemLinha[i].IndexOf(".") - 50));
                }
                mensagemLinha.Clear();
            } 
        }
        public void compactScript(string[] vetor, string type)
        {
            /*string strTemp = "c://conector//Transmissao//" + "CompactExeCupom" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + type + ".bat";
            StreamWriter sw = new StreamWriter(strTemp, false);
            //sw.Write("set data=CONFIG%date:~0,2%%date:~3,2%%date:~6,4% && ");
            if (vetor[0] == null)
            {
                sw.Write("exit");
            }
            else
            {
                sw.Write("cd " + "c://conector//Transmissao//" + " && ");
                sw.Write("del " + "c:\\conector\\Transmissao\\" + strCupom + ".rar" + " &&");
                sw.Write("\"" + alwaysVariables.ecfPrinter + "\"" + " a -ibck -o+ -r -V1500000 \"" + "c://conector//Transmissao//" + strCupom + ".rar" + "\"  \"" + "c://conector//Transmissao//" + strCupom + ".sql\" && ");
                sw.Write("del " + "c:\\conector\\Transmissao\\" + strCupom + ".sql" + " && exit");
            }
            
            sw.Close();
            exeProcesso(strTemp, "#",0);*/
        }
        public void preparaXml(string exe)
        {
            if (File.Exists(exe))
            {
                StreamWriter sw = new StreamWriter("c:\\conector\\Recepcao\\RCexe" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + ".bat", false);
                //                    sw.Write("exit");
                sw.Write(" cd c:\\conector\\Recepcao\\ && ");
                sw.Write(" del c:\\conector\\Recepcao\\*.xml && ");
                sw.Write(" del c:\\conector\\Recepcao\\TRconector* && ");
                sw.Write("\"" + alwaysVariables.ecfPrinter + "\"" + " e -ibck  -kb \"c:\\conector\\Recepcao\\" + String.Format("{0:ddMMyyyy}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "TRANSMISSAO.rar\"" + " \"c:\\conector\\Recepcao\\\" " + " && ");
                //sw.Write("\"C:\\Arquivos de programas\\WinRAR\\RAR.EXE\" x -y \"c:\\conector\\Recepcao\\" + String.Format("{0:ddMMyyyy}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "TRANSMISSAO.rar\"" + " && ");
                sw.Write(" del c:\\conector\\Recepcao\\" + String.Format("{0:ddMMyyyy}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "TRANSMISSAO.rar" + " && ");
                sw.Write("\"" + alwaysVariables.ecfPrinter + "\"" + " e -ibck -kb \"c:\\conector\\Recepcao\\*.rar\" " + " \"c:\\conector\\Recepcao\\\" " + "  && ");
                sw.Write(" dir /b /s c:\\conector\\Recepcao\\*.xml  >  c:\\conector\\Recepcao\\%date:~0,2%%date:~3,2%%date:~6,4%.txt && ");
                sw.Write(" del c:\\conector\\Recepcao\\*.rar && exit");
                sw.Close();
                profile = "c:\\conector\\Recepcao\\" + String.Format("{0:ddMMyyyy}", DateTime.Now) + ".txt";
                exeProcesso("c:\\conector\\Recepcao\\RCexe" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + ".bat", "#",0);

            }
        }
        public void preparaSql(string exe)
        {
            if (File.Exists(exe))
            {
                StreamWriter sw = new StreamWriter(recepcaoCaminho + "RCexe" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + ".bat", false);
                //                    sw.Write("exit");
                sw.Write(" cd " + recepcaoCaminho + " && ");
                sw.Write(" del " + recepcaoCaminho + "*.sql && ");
                sw.Write(" del " + recepcaoCaminho + "TRconector* && ");
                sw.Write("\"" + alwaysVariables.ecfPrinter + "\"" + " e -ibck  -kb \"" + exe + "\"" + " \"" + recepcaoCaminho + "\" " + " && ");
                //sw.Write("\"C:\\Arquivos de programas\\WinRAR\\RAR.EXE\" x -y \"c:\\conector\\Recepcao\\" + String.Format("{0:ddMMyyyy}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "TRANSMISSAO.rar\"" + " && ");
                sw.Write(" del " + exe + " && ");
                sw.Write("\"" + alwaysVariables.ecfPrinter + "\"" + " e -ibck -kb \"" + recepcaoCaminho + "*.rar\" " + " \"" + recepcaoCaminho + "\" " + "  && ");
                sw.Write(" dir /b /s " + recepcaoCaminho + "*.sql  >  " + recepcaoCaminho +"%date:~0,2%%date:~3,2%%date:~6,4%.txt && ");
                sw.Write(" del " + recepcaoCaminho + "*.rar && exit");
                sw.Close();
                profile = recepcaoCaminho + String.Format("{0:ddMMyyyy}", DateTime.Now) + ".txt";
                exeProcesso(recepcaoCaminho + "RCexe" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + ".bat", "#",0);
            }
        }
        public void carregaInstrucao(string[] vetor, string type)
        {
            if (vetor.Length > 0)
            {
                StreamWriter sw = new StreamWriter("c:\\conector\\Transmissao\\conector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + type + ".bat", false);
                for (int i = 0; i < vetor.Length; i++)
                {
                    if (vetor[i] == null)
                    {
                        sw.Write("exit" + "&&");
                    }
                    else
                    {
                        sw.Write(vetor[i] + " \r\n");
                    }
                }
                
                sw.Close();
                exeProcesso("c:\\conector\\Transmissao\\conector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + type + ".bat", "#",0);
            }
        }

        public void carregaInstrucaoMovimentoVenda(string[] vetor, string caminho, Int16 md)
        {
            string strTemp = "";
            string test = "";
            strTemp = strCupom = caminho;
            if (vetor.Length > 0)
            {
                strTemp = "c:\\conector\\Transmissao\\" + caminho + ".bat";
                StreamWriter sw = new StreamWriter(strTemp, false);
                for (int i = 0; i < vetor.GetLength(0); i++)
                {
                    if (vetor[i] == null)
                    {
                        sw.Write("exit" + "&&");
                        break;
                    }
                    else
                    {
                        sw.Write(vetor[i] + " \r\n");
                    }
                }

                if (md == 1)
                {
                    test = caminho_md5;
                }
                else
                {
                    test = "#";
                }
                sw.Close();
                processo.exeProcesso(strTemp, test, 0, ref detalhes_md5);
            }
        }
        public void setVetorCupom()
        {
            countCupom = 0;
            countNfce = 0;
            countNfImposto = 0;
            countNfe = 0;
            countNfceItem = 0;
            vetorNfImposto = new string[48];
            vetorCupomCabecalho = new string[48];
            vetorCupomCabecalhoCT = new string[48];
            vetorCupomDetalhes = new string[100];
            vetorCupomDetalhesCT = new string[10];
            vetorCupomMovimento = new string[48];
            vetorCupomCartao = new string[48];
            vetorCupomConvenio = new string[48];
            vetorCupomCheque = new string[48];
            vetorMapa = new string[10];
            vetorMapaMovimento = new string[100];
            vetorAliquota = new string[10];
            vetorfechamentoCaixa = new string[10];
            vetorNfceItem = new string[1000];
            vetorNfce = new string[48];
            vetorNfe = new string[48];
        }
        public string getCupomCabecalho(string users, string acess, string store, string cupom, string data, string operador, string terminal, string seq)
        {
            //string retorno = "MVconector" + "C" + "0000" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:0000000000}", Convert.ToDouble(cupom.Trim('\0'))) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "cupom_cabecalho.sql";
            string retorno = "MVconector" + "C" + String.Format("{0:0000}", Convert.ToDouble(seq)) + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:0000000000}", Convert.ToDouble(cupom.Trim('\0'))) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "cupom_cabecalho.sql";
            cupom_cabecalho = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conectorPDV --execute=\"select * into outFile '" + transmissaoCaminho + "MVconector" + "C" + String.Format("{0:0000}", Convert.ToDouble(seq)) + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:0000000000}", Convert.ToDouble(cupom.Trim('\0'))) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "cupom_cabecalho.sql" + "' fields terminated by '|' from conectorpdv.cupom_cabecalho where loja=" + store.Trim('\0') + " and numeroCupom=" + cupom.Trim('\0') + " and terminal=" + terminal.Trim('\0') + " and operador=" + operador + " and dataVenda=" + data + "\"";
            vetorCupomCabecalho[countCupom++] = cupom_cabecalho;
            return retorno.Substring(0,retorno.IndexOf("."));
        }

        public string getCupomCabecalhoCT(string users, string acess, string store, string cupom, string data, string operador, string terminal)
        {
            string retorno = "MVconector" + "C" + "0000" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:0000000000}", Convert.ToDouble(cupom.Trim('\0'))) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "cupom_cabecalho.sql";
            cupom_cabecalhoCT = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conectorPDV --execute=\"select * into outFile '" + transmissaoCaminho + "MVconector" + "C" + "0000" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:0000000000}", Convert.ToDouble(cupom.Trim('\0'))) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "cupom_cabecalho.sql" + "' fields terminated by '|' from conectorpdv.cupom_cabecalho where loja=" + store.Trim('\0') + " and numeroCupom=" + cupom.Trim('\0') + " and terminal=" + terminal.Trim('\0') + " and operador=" + operador + " and dataVenda=" + data + "\"";
            vetorCupomCabecalhoCT[countCupom++] = cupom_cabecalhoCT;
            return retorno.Substring(0, retorno.IndexOf("."));
        }
        public string getCupomMovimento(string users, string acess, string store, string cupom, string data, string terminal, string seq)
        {
            string retorno = "MVconector" + "M" + String.Format("{0:0000}", Convert.ToDouble(seq)) + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:0000000000}", Convert.ToDouble(cupom)) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "cupom_movimento.sql";
            cupom_movimento = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conectorPDV --execute=\"select * into outFile '" + transmissaoCaminho + "MVconector" + "M" + String.Format("{0:0000}", Convert.ToDouble(seq)) + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:0000000000}", Convert.ToDouble(cupom)) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "cupom_movimento.sql" + "' fields terminated by '|' from conectorpdv.cupom_movimento where loja=" + store + " and numeroCupom=" + cupom + " and  terminal=" + terminal  + " and dataVenda=" + data + " and sequencia=" + seq + "\"";
            vetorCupomMovimento[countCupom++] = cupom_movimento;
            return retorno.Substring(0,retorno.IndexOf("."));
        }
        public string getCupomDetalhes(string users, string acess, string store, string cupom, string data, string terminal, string seq)
         {
            string retorno = "MVconector" + "D" + String.Format("{0:0000}", Convert.ToDouble(seq)) + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:0000000000}", Convert.ToDouble(cupom)) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "cupom_detalhes.sql";
            cupom_detalhes= "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conectorPDV --execute=\"select * into outFile '" + transmissaoCaminho + "MVconector" + "D" + String.Format("{0:0000}", Convert.ToDouble(seq)) + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:0000000000}", Convert.ToDouble(cupom)) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "cupom_detalhes.sql" + "' fields terminated by '|' from conectorpdv.cupom_detalhes where loja=" + store + " and numeroCupom=" + cupom + " and  terminal=" + terminal + " and dataVenda=" + data + " and sequencia=" + seq + "\"";
            caminho_md5 = transmissaoCaminho + "MVconector" + "D" + String.Format("{0:0000}", Convert.ToDouble(seq)) + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:0000000000}", Convert.ToDouble(cupom)) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "cupom_detalhes.sql";
            vetorCupomDetalhes[countCupom++] = cupom_detalhes;
            return retorno.Substring(0, retorno.IndexOf("."));
        }
        public string getCupomDetalhesCT(string users, string acess, string store, string cupom, string data, string terminal, string seq)
        {
            string retorno = "MVconector" + "D" + String.Format("{0:0000}", Convert.ToDouble(seq)) + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:0000000000}", Convert.ToDouble(cupom)) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "cupom_detalhes.sql";
            cupom_detalhesCT = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conectorPDV --execute=\"select * into outFile '" + transmissaoCaminho + "MVconector" + "D" + String.Format("{0:0000}", Convert.ToDouble(seq)) + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:0000000000}", Convert.ToDouble(cupom)) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "cupom_detalhes.sql" + "' fields terminated by '|' from conectorpdv.cupom_detalhes where loja=" + store + " and numeroCupom=" + cupom + " and  terminal=" + terminal + " and dataVenda=" + data + " and sequencia=" + seq + "\"";
            caminho_md5 = transmissaoCaminho + "MVconector" + "D" + String.Format("{0:0000}", Convert.ToDouble(seq)) + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:0000000000}", Convert.ToDouble(cupom)) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "cupom_detalhes.sql";
            vetorCupomDetalhesCT[countCupom++] = cupom_detalhesCT;
            return retorno.Substring(0, retorno.IndexOf("."));
        }

        public string getMapa(string users, string acess, string store, string cupom, string data, string terminal, string seq)
        {
            string retorno = "MVconector" + "Z" + String.Format("{0:0000}", Convert.ToDouble(seq == "" ? "0": seq)) + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:0000000000}", Convert.ToDouble(cupom)) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "movimentoDia.sql";
            mapa_cabecalho = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conectorPDV --execute=\"select * into outFile '" + transmissaoCaminho + "MVconector" + "Z" + String.Format("{0:0000}", Convert.ToDouble(seq == "" ? "0":seq)) + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:0000000000}", Convert.ToDouble(cupom)) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "movimentoDia.sql" + "' fields terminated by '|' from conectorpdv.movimentoDia where idloja=" + store + " and coo=" + cupom + " and  numeroCaixa=" + terminal + " and movimento=" + data + "\"";
            vetorMapa[countCupom++] = mapa_cabecalho;
            return retorno.Substring(0, retorno.IndexOf("."));
        }

        public string getMapaRegistro(string users, string acess, string store, string cupom, string data, string terminal, string seq)
        {
            string retorno = "MVconector" + "X" + String.Format("{0:0000}", Convert.ToDouble(seq)) + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:0000000000}", Convert.ToDouble(cupom)) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "detalhe_reducao.sql";
            mapa_movimento = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conectorPDV --execute=\"select * into outFile '" + transmissaoCaminho + "MVconector" + "X" + String.Format("{0:0000}", Convert.ToDouble(seq)) + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:0000000000}", Convert.ToDouble(cupom)) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "detalhe_reducao.sql" + "' fields terminated by '|' from conectorpdv.detalhe_reducao where idloja=" + store + " and crz=" + cupom + " and  numeroCaixa='" + terminal + "' AND valorAcumulado > 0 " + "\"";
            vetorMapaMovimento[countCupom++] = mapa_movimento;
            return retorno.Substring(0, retorno.IndexOf("."));
        }

        public string getAliquota(string users, string acess, string store, string cupom, string data, string terminal, string seq)
        {
            string retorno = "MVconector" + "T" + String.Format("{0:0000}", Convert.ToDouble(seq)) + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:0000000000}", Convert.ToDouble(cupom)) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "detalhe_reducao_aliquota.sql";
            aliquotas = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conectorPDV --execute=\"select * into outFile '" + transmissaoCaminho + "MVconector" + "T" + String.Format("{0:0000}", Convert.ToDouble(seq)) + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:0000000000}", Convert.ToDouble(cupom)) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "detalhe_reducao_aliquota.sql" + "' fields terminated by '|' from conectorpdv.detalhe_reducao_aliquota where idloja=" + store + " and crz=" + cupom + " and  numeroCaixa='" + terminal + "'" + "\"";
            vetorAliquota[countCupom++] = aliquotas;
            return retorno.Substring(0, retorno.IndexOf("."));
        }
        public string getFechamentoCaixa(string users, string acess, string store, string funcionario, string data, string terminal, string cupom ,string seq)
        {
            string retorno = "MVconector" + "F" + String.Format("{0:0000}", Convert.ToDouble(seq)) + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:0000000000}", Convert.ToDouble(cupom.Replace("\0",""))) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "fechamentoCaixa.sql";
            fechamentoCaixa = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conectorPDV --execute=\"select * into outFile '" + transmissaoCaminho + "MVconector" + "F" + String.Format("{0:0000}", Convert.ToDouble(seq)) + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:0000000000}", Convert.ToDouble(cupom.Replace("\0",""))) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "fechamentoCaixa.sql" + "' fields terminated by '|' from conectorpdv.fechamentoCaixa where loja=" + store + " and dataMovimento=" + data + " and terminal=" + terminal + " and funcionario=" + funcionario + " and  sequencia='" + seq + "'" + "\"";
            vetorfechamentoCaixa[countCupom++] = fechamentoCaixa;
            return retorno.Substring(0, retorno.IndexOf("."));
        }
        public string getCartao(string users, string acess, string store, string cupom, string data, string terminal)
        {
            string retorno = "MVconector" + "C" + "0000" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:0000000000}", Convert.ToDouble(cupom.Trim('\0'))) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "cartao.sql";
            cupom_cartao = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conectorPDV --execute=\"select * into outFile '" + transmissaoCaminho + "MVconector" + "C" + "0000" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:0000000000}", Convert.ToDouble(cupom.Trim('\0'))) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "cartao.sql" + "' fields terminated by '|' from conectorpdv.cartao where idloja=" + store.Trim('\0') + " and cupom=" + cupom.Trim('\0') + " and terminal=" + terminal.Trim('\0') + " and emissao=" + data + "\"";
            vetorCupomCartao[countCupom++] = cupom_cartao;
            return retorno.Substring(0, retorno.IndexOf("."));
        }

        public string getCheque(string users, string acess, string store, string cupom, string data, string terminal)
        {
            string retorno = "MVconector" + "H" + "0000" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:0000000000}", Convert.ToDouble(cupom.Trim('\0'))) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "cheque.sql";
            cupom_cheque = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conectorPDV --execute=\"select * into outFile '" + transmissaoCaminho + "MVconector" + "H" + "0000" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:0000000000}", Convert.ToDouble(cupom.Trim('\0'))) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "cheque.sql" + "' fields terminated by '|' from conectorpdv.cheque where idloja=" + store.Trim('\0') + " and cupom=" + cupom.Trim('\0') + " and terminal=" + terminal.Trim('\0') + " and emissao=" + data + "\"";
            vetorCupomCheque[countCupom++] = cupom_cheque;
            return retorno.Substring(0, retorno.IndexOf("."));
        }
        public string getConvenio(string users, string acess, string store, string cupom, string data, string terminal)
        {
            string retorno = "MVconector" + "C" + "0000" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:0000000000}", Convert.ToDouble(cupom.Trim('\0'))) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "convenioMovimento.sql";
            cupom_convenio = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conectorPDV --execute=\"select * into outFile '" + transmissaoCaminho + "MVconector" + "C" + "0000" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:0000000000}", Convert.ToDouble(cupom.Trim('\0'))) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "convenioMovimento.sql" + "' fields terminated by '|' from conectorpdv.convenioMovimento where idloja=" + store.Trim('\0') + " and cupom=" + cupom.Trim('\0') + " and terminal=" + terminal.Trim('\0') + " and emissao=" + data + "\"";
            vetorCupomConvenio[countCupom++] = cupom_convenio;
            return retorno.Substring(0, retorno.IndexOf("."));
        }
        public string getNf(string users, string acess, string store, string idNf, string data, string lote)
        {
            string retorno = "MVconector" + "B" + String.Format("{0:0000}", Convert.ToDouble(lote)) + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:0000000000}", Convert.ToDouble(idNf)) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "nf.sql";
            //nfe = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conectorPDV --execute=\"select * into outFile '" + transmissaoCaminho + "MVconector" + "B" + String.Format("{0:0000}", Convert.ToDouble(lote)) + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:0000000000}", Convert.ToDouble(idNf)) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "nf.sql" + "' fields terminated by '|' from conectorpdv.nf where loja=" + store + " and nf.nf=" + idNf + " and emissao=" + data + "\"";
            //nfe = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conectorPDV --execute=\"select '|' nf, `loja`,`idcliente`,`idparamentro`,`idtransportadora`,`cfop`,`idFuncionario`,`idusuario`,`idpedido`,`nr_nota`,`serie`,`acrescimo`,`baseIcms`,`baseIcmsIsento`,`valorIcmsSubstituicao`,`baseCalculoIcmsSubstituicao`,`baseIPI`,`baseCofins`,`basePis`,`emissao`,`saida`,`alteracao`,`hora`,`desconto`,`uf`,`itens`,`seguro`,`frete`,`typeFrete`,`valorIcms`,`valorIpi`,`valorPis`,`valorCofins`,`acrecismoValor`,`descontoValor`,`valorTotalLiquido`,`valorTotalNota`,`valorTotalProdutos`,`volumes`,`peso`,`contribuicaoSocial`,`quantidadePedido`,`quantidadeRecebida`,`impresso`,`nr_impressao`,`idTable_Codigo`,`modNotaFiscal`,`idSituacaoFiscal`,`emitiNfe`,`typenf`,`msg01`,`msg02`,`msg03`,`valorTotaServico`,`nr_nota_entrada`,`serie_entrada`,`statusNf`,`restituicao`,`iss`,`impostoRenda`,`funrural`,`valorTotalFunrural`,`geraDanfe`,`condPgto`,`chave_nfe`,`protocolo`,`motivo`,`versaoNfe`,`dataHoraRecbNfe` into outFile '" + transmissaoCaminho + "MVconector" + "B" + String.Format("{0:0000}", Convert.ToDouble(lote)) + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:0000000000}", Convert.ToDouble(idNf)) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "nf.sql" + "' fields terminated by '|' from conectorpdv.nf where loja=" + store + " and nf.nf=" + idNf + " and emissao=" + data + "\""; last
            nfe = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conectorPDV --execute=\"select nf, `loja`,`idcliente`,`idparamentro`,`idtransportadora`,`cfop`,`idFuncionario`,`idusuario`,`idpedido`,`nr_nota`,`serie`,`acrescimo`,`baseIcms`,`baseIcmsIsento`,`valorIcmsSubstituicao`,`baseCalculoIcmsSubstituicao`,`baseIPI`,`baseCofins`,`basePis`,`emissao`,`saida`,`alteracao`,`hora`,`desconto`,`uf`,`itens`,`seguro`,`frete`,`typeFrete`,`valorIcms`,`valorIpi`,`valorPis`,`valorCofins`,`acrecismoValor`,`descontoValor`,`valorTotalLiquido`,`valorTotalNota`,`valorTotalProdutos`,`volumes`,`peso`,`contribuicaoSocial`,`quantidadePedido`,`quantidadeRecebida`,`impresso`,`nr_impressao`,`idTable_Codigo`,`modNotaFiscal`,`idSituacaoFiscal`,`emitiNfe`,`typenf`,`msg01`,`msg02`,`msg03`,`valorTotaServico`,`nr_nota_entrada`,`serie_entrada`,`statusNf`,`restituicao`,`iss`,`impostoRenda`,`funrural`,`valorTotalFunrural`,`geraDanfe`,`condPgto`,`chave_nfe`,`protocolo`,`motivo`,`versaoNfe`,`dataHoraRecbNfe` into outFile '" + transmissaoCaminho + "MVconector" + "B" + String.Format("{0:0000}", Convert.ToDouble(lote)) + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:0000000000}", Convert.ToDouble(idNf)) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "nf.sql" + "' fields terminated by '|' from conectorpdv.nf where loja=" + store + " and nf.nf=" + idNf + " and emissao=" + data + "\"";
            vetorNfe[countNfe++] = nfe;
            return retorno.Substring(0, retorno.IndexOf("."));
        }

        public string getNfImposto(string users, string acess, string idNf, string data, string lote)
        {
            string retorno = "MVconector" + "G" + String.Format("{0:0000}", Convert.ToDouble(lote)) + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:0000000000}", Convert.ToDouble(idNf)) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "nfimposto.sql";
            //nfe = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conectorPDV --execute=\"select * into outFile '" + transmissaoCaminho + "MVconector" + "B" + String.Format("{0:0000}", Convert.ToDouble(lote)) + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:0000000000}", Convert.ToDouble(idNf)) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "nf.sql" + "' fields terminated by '|' from conectorpdv.nf where loja=" + store + " and nf.nf=" + idNf + " and emissao=" + data + "\"";
            nfImposto = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conectorPDV --execute=\"select '|', `idnf`,  `cfop`,  `cstIcms`,  `aliquota`,  `imposto`,  `baseIcmsIsentos`,  `reducao`,  `baseCalculo`,  `icms`,  `baseCalculoIcmsSt`,  `icmsSt`,  `valorIpi`,  `typeAliquota`,  `valor` into outFile '" + transmissaoCaminho + "MVconector" + "G" + String.Format("{0:0000}", Convert.ToDouble(lote)) + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:0000000000}", Convert.ToDouble(idNf)) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "nfImposto.sql" + "' fields terminated by '|' from conectorpdv.nfImposto where  idnf=" + idNf +"\"";
            vetorNfImposto[countNfImposto++] = nfImposto;
            return retorno.Substring(0, retorno.IndexOf("."));
        }
        public string getNfce(string users, string acess, string store, string idNf, string data, string lote)
        {
            string retorno = "MVconector" + "A" + String.Format("{0:0000}", Convert.ToDouble(lote)) + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:0000000000}", Convert.ToDouble(idNf)) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "nf.sql";
            //nfce = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conectorPDV --execute=\"select * into outFile '" + transmissaoCaminho + "MVconector" + "A" + String.Format("{0:0000}", Convert.ToDouble(lote)) + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:0000000000}", Convert.ToDouble(idNf)) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "nf.sql" + "' fields terminated by '|' from conectorpdv.nf where loja=" + store + " and nf.nf=" + idNf + " and emissao=" + data + "\"";
            //nfce = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conectorPDV --execute=\"select '|', nf, `loja`,`idcliente`,`idparamentro`,`idtransportadora`,`cfop`,`idFuncionario`,`idusuario`,`idpedido`,`nr_nota`,`serie`,`acrescimo`,`baseIcms`,`baseIcmsIsento`,`valorIcmsSubstituicao`,`baseCalculoIcmsSubstituicao`,`baseIPI`,`baseCofins`,`basePis`,`emissao`,`saida`,`alteracao`,`hora`,`desconto`,`uf`,`itens`,`seguro`,`frete`,`typeFrete`,`valorIcms`,`valorIpi`,`valorPis`,`valorCofins`,`acrecismoValor`,`descontoValor`,`valorTotalLiquido`,`valorTotalNota`,`valorTotalProdutos`,`volumes`,`peso`,`contribuicaoSocial`,`quantidadePedido`,`quantidadeRecebida`,`impresso`,`nr_impressao`,`idTable_Codigo`,`modNotaFiscal`,`idSituacaoFiscal`,`emitiNfe`,`typenf`,`msg01`,`msg02`,`msg03`,`valorTotaServico`,`nr_nota_entrada`,`serie_entrada`,`statusNf`,`restituicao`,`iss`,`impostoRenda`,`funrural`,`valorTotalFunrural`,`geraDanfe`,`condPgto`,`chave_nfe`,`protocolo`,`motivo`,`versaoNfe`,`dataHoraRecbNfe` into outFile '" + transmissaoCaminho + "MVconector" + "A" + String.Format("{0:0000}", Convert.ToDouble(lote)) + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:0000000000}", Convert.ToDouble(idNf)) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "nf.sql" + "' fields terminated by '|' from conectorpdv.nf where loja=" + store + " and nf.nf=" + idNf + " and emissao=" + data + "\""; Last
            nfce = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conectorPDV --execute=\"select  nf, `loja`,`idcliente`,`idparamentro`,`idtransportadora`,`cfop`,`idFuncionario`,`idusuario`,`idpedido`,`nr_nota`,`serie`,`acrescimo`,`baseIcms`,`baseIcmsIsento`,`valorIcmsSubstituicao`,`baseCalculoIcmsSubstituicao`,`baseIPI`,`baseCofins`,`basePis`,`emissao`,`saida`,`alteracao`,`hora`,`desconto`,`uf`,`itens`,`seguro`,`frete`,`typeFrete`,`valorIcms`,`valorIpi`,`valorPis`,`valorCofins`,`acrecismoValor`,`descontoValor`,`valorTotalLiquido`,`valorTotalNota`,`valorTotalProdutos`,`volumes`,`peso`,`contribuicaoSocial`,`quantidadePedido`,`quantidadeRecebida`,`impresso`,`nr_impressao`,`idTable_Codigo`,`modNotaFiscal`,`idSituacaoFiscal`,`emitiNfe`,`typenf`,`msg01`,`msg02`,`msg03`,`valorTotaServico`,`nr_nota_entrada`,`serie_entrada`,`statusNf`,`restituicao`,`iss`,`impostoRenda`,`funrural`,`valorTotalFunrural`,`geraDanfe`,`condPgto`,`chave_nfe`,`protocolo`,`motivo`,`versaoNfe`,`dataHoraRecbNfe` into outFile '" + transmissaoCaminho + "MVconector" + "A" + String.Format("{0:0000}", Convert.ToDouble(lote)) + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:0000000000}", Convert.ToDouble(idNf)) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "nf.sql" + "' fields terminated by '|' from conectorpdv.nf where loja=" + store + " and nf.nf=" + idNf + " and emissao=" + data + "\"";
            vetorNfe[countNfce++] = nfce;
            return retorno.Substring(0, retorno.IndexOf("."));
        }
        public string getNfceItem(string users, string acess, string store, string idNf, string idnfItem, string data, string seq)
        {
            string retorno = "MVconector" + "I" + String.Format("{0:0000}", Convert.ToDouble(seq)) + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:0000000000}", Convert.ToDouble(idnfItem)) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "nfItem.sql";
            // nfceItem = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conectorPDV --execute=\"select * into outFile '" + transmissaoCaminho + "MVconector" + "I" + String.Format("{0:0000}", Convert.ToDouble(seq)) + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:0000000000}", Convert.ToDouble(idnfItem)) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "nfItem.sql" + "' fields terminated by '|' from conectorpdv.nfItem where idnfItem=" + idnfItem + " and idnf=" + idNf + " and data=" + data + " and sequencia=" + seq + "\"";
            nfceItem = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conectorPDV --execute=\"select '|',`idnf`,`idProduto`,`valorLiquido`,`priceOriginal`,`priceVenda`,`priceCusto`,`estoque`,`data`,`peso`,`aliquota`,`icms`,`baseCalculo`,`reducao`,`quantidade`,`idunidadeMedida`,`cfop`,`cstIcms`,`cstPis`,`valorPis`,`basePis`,`cstCofins`,`valorCofins`,`baseCofins`,`cstIpi`,`ipi`,`ipiValor`,`valorIpi`,`baseIpi`,`desconto`,`descontoValor`,`acrescimo`,`acrescimoValor`,`aliquotaIcmsSt`,`baseCalculoIcmsSubstituicao`,`valorIcmsSubstituicao`,`reducaoIcmsSt`,`margem`,`valorTotalProduto`,`valorTotalNota`,`valorTotalLiquido`,`fornecedor`,`idsetor`,`tributacao`,`frete`,`valorFrete`,`typeAliquota`,`chaveEntrada`,`sequencia`,`seguro`,`idGenero`,`origemMercadoria` into outFile '" + transmissaoCaminho + "MVconector" + "I" + String.Format("{0:0000}", Convert.ToDouble(seq)) + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:0000000000}", Convert.ToDouble(idnfItem)) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "nfItem.sql" + "' fields terminated by '|' from conectorpdv.nfItem where idnfItem=" + idnfItem + " and idnf=" + idNf + " and data=" + data + " and sequencia=" + seq + "\"";
            vetorNfceItem[countNfceItem++] = nfceItem;
            return retorno.Substring(0, retorno.IndexOf("."));
        }
        //#########################################################End Metodos, Funçoes e Propart #######################################################################################################################################################################################################################
        //#########################################################Procedimento de Banco#################################################################################################################################################################################################################################
        public int conector_update_lineMD5(string cupom, string prod, string sequencia, string cx)
        {
            int retorno = 0;
            try
            {
                auxConsistencia = 0;
                banco.abreConexao();
                banco.singleTransaction("update `conectorpdv`.`cupom_detalhes` set cripto=?cripto where numeroCupom=?cupom and terminal=?cx and loja=?store and produto=?prod and sequencia=?sequencia");
                banco.addParametro("?cupom", cupom);
                banco.addParametro("?store", prod);
                banco.addParametro("?sequencia", sequencia);
                banco.addParametro("?terminal", cx);
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
        public string conector_import_resource(string caminho, string table)
        {
            if (table == "licenca_ecf") { table = "liberacao"; }
            string retorno = "Not found...!";
            //SET foreign_key_checks = 0;
            string test = "SET foreign_key_checks=0; ";
            test += " LOAD DATA LOCAL INFILE '";
            test += caminho;
            test += "' REPLACE INTO TABLE conectorPDV." + table + " FIELDS TERMINATED BY '|'; ";
            test += "SET foreign_key_checks=1; ";
            int auxConsistencia = 0;
            try
            {
                banco.abreConexao();
                banco.singleTransaction(test);
                if (File.Exists(caminho))
                {
                    banco.procedimentoText();
                }
                else
                {
                    retorno = "Caro Usuário: A instrução não foi executada.";
                }

            }
            catch (Exception erro)
            {
                retorno = "Caro Usuário: MSG Suporte => " + erro.ToString();
                auxConsistencia = 1;
            }
            finally
            {
                banco.fechaConexao();
                if (auxConsistencia == 0)
                {
                    retorno = "Concluído...!";
                    if (File.Exists(caminho))
                    {
                        File.Delete(caminho);
                    }
                }
            }
            return retorno;
        }
        public string conector_import_resourceXml(string caminho, string table)
        {
            if (table == "licenca_ecf") { table = "liberacao"; }
            string retorno = "Not found...!";
            //SET foreign_key_checks = 0;
            string test = "SET foreign_key_checks=0; ";
            test += " LOAD XML LOCAL INFILE '";
            test += caminho;
            test += "' INTO TABLE " + table + "; ";
            test += "SET foreign_key_checks=1; ";
            int auxConsistencia = 0;
            try
            {
                banco.abreConexao();
                banco.singleTransaction(test);
                if (File.Exists(caminho))
                {
                    banco.procedimentoRead();
                }
                else
                {
                    retorno = "Caro Usuário: A instrução não foi executada.";
                }

            }
            catch (Exception erro)
            {
                retorno = "Caro Usuário: MSG Suporte => " + erro.ToString();
                auxConsistencia = 1;
            }
            finally
            {
                banco.fechaConexao();
                if (auxConsistencia == 0)
                {
                    retorno = "Concluído...!";
                    if (File.Exists(caminho))
                    {
                        File.Delete(caminho);
                    }
                }
            }
            return retorno;
        }
        //#########################################################Procedimento de Banco#########################################################################################
    }
}
