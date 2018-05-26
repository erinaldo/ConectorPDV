using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.IO.Ports;
using System.Net;
using System.Diagnostics;

namespace conectorPDV001
{
    class eventos
    {
        public eventos()
        {
            RequestStop(false);
            instrucao = new instrucaol();
        }
        public eventos(bool ativa)
        {
            RequestStop(false);
            instrucao = new instrucaol();
            modoPdv(ativa);
        }

        #region //Variaveis

            public volatile bool _flagStop;
            private volatile bool _liberaCarga;
            private string[] regra = new string[14] { "nf", "nfce", "nfItem", "nfImposto", "cupom_cabecalho", "cupom_detalhes", "cupom_movimento", "movimentoDia", "detalhe_reducao", "detalhe_reducao_aliquota", "fechamentoCaixa", "cartao", "cheque", "convenioMovimento" };
            private string[] path = new string[14];
            private cupomImport Import = new cupomImport();
            private instrucaol instrucao;
            string[,] vetorCx;
            string[] vetorStore;
            List<string> leituraPasta = new List<string>();
            List<string> leituraPasta2 = new List<string>();
            private conector_full_variable alwaysVariables = new conector_full_variable();

        #endregion

        #region //Funções e Metodos
        public void RequestStop(bool para)
        {
            _flagStop = para;
        }

        public void modoPdv(bool modo)
        {
            _liberaCarga = modo;
        }
        public string renomeiaLog()
        {
            return @"c:\conector\log\" + "conector" + String.Format("{0:yyyyMMdd-HHmmss}", DateTime.Now) + ".log";
        }
        public void Varrefolder()
        {
            Import = new cupomImport();
            alwaysVariables.caminhoLog = renomeiaLog();
            StreamWriter sw = new StreamWriter(alwaysVariables.caminhoLog, false);

            while (!_flagStop)
            {
                string msgErro = "";
                string[] fileRecepcao = Directory.GetFiles(@"c:\conector\recepcao\");
                string[] fileTransmissao = Directory.GetFiles(@"C:\conector\Transmissao\");


                if (!_liberaCarga)
                {
                    while (((fileRecepcao.GetLength(0) > 0) || (fileTransmissao.GetLength(0) > 0)) && _liberaCarga == false)
                    {
                        if ((fileRecepcao.GetLength(0) > 0))
                        {
                            for (int y = 0; y < fileRecepcao.GetLength(0); y++)
                            {
                                if (fileRecepcao != null)
                                {

                                    if ((fileRecepcao[y].ToString().IndexOf("TRANSMISSAO") != -1)&&(fileRecepcao[y].ToString().IndexOf("rar") != -1))
                                    {//Não atualizar o pdv com cupom_em aberto
                                        instrucao.preparaSql(fileRecepcao[y]);
                                        instrucao.carregaSql();
                                        instrucao.executaSqlBat(0);
                                        fileRecepcao = Directory.GetFiles(@"c:\conector\recepcao\");
                                    }
                                }
                            }
                        }

                        if (fileTransmissao.GetLength(0) > 0)
                        {
                            for (int i = 0; i < fileTransmissao.GetLength(0); i++)
                            {
                                if (fileTransmissao.GetLength(0) > 0)
                                {
                                    for (int y = 0; y < fileTransmissao.GetLength(0); y++)
                                    {
                                        if ((fileTransmissao[y].ToString().IndexOf("MVconector") != -1) && (fileTransmissao[y].ToString().IndexOf("rar") != -1))
                                        {
                                            if (Import.transmissao(fileTransmissao[y], alwaysVariables.Terminal, alwaysVariables.ConectorServer,ref msgErro))
                                            {
                                                sw.Write("Transmissao de Arquivo => " + "Store: " + alwaysVariables.Store + " TERMINAL: " + alwaysVariables.Terminal + " Envio: " + fileTransmissao[y].ToString() + " Processamento: " + String.Format("{0:dd-MM-yyyy HH:mm:ss}", DateTime.Now) + " Versao EXE." + alwaysVariables.VersaoSystem + " Versao Banco" + alwaysVariables.VersaoBanco + "\r\n");
                                            }
                                            else
                                            {
                                                sw.Write("Transmissao de Arquivo => " + "Store: " + alwaysVariables.Store + " TERMINAL: " + alwaysVariables.Terminal + " Envio: " + msgErro + " Processamento: " + String.Format("{0:dd-MM-yyyy HH:mm:ss}", DateTime.Now) + " Versao EXE." + alwaysVariables.VersaoSystem + " Versao Banco" + alwaysVariables.VersaoBanco + "\r\n");
                                            }
                                        }
                                    }
                                    fileTransmissao = Directory.GetFiles(@"C:\conector\Transmissao\");
                                }
                            }
                        }
                    }
                }
            }
            sw.Close();



            // block Import.compactaLog(alwaysVariables.caminhoLog);
        }

        #endregion


    }

    class balanca
    {
        public balanca()
        {
            serialPort1 = new SerialPort();
        }
        public balanca(string porta)
        {
            serialPort1 = new SerialPort();
            serialPort1.Close();
            serialPort1.PortName = porta == "" ? "COM": porta;
            serialPort1.BaudRate = 9600;
            serialPort1.Parity = Parity.None;
            serialPort1.DataBits = 8;
            serialPort1.StopBits = StopBits.One;
            serialPort1.ReadTimeout = 500;
            serialPort1.WriteTimeout = 500;
            try
            {
                serialPort1.Open();
            }
            catch (Exception)
            {

            }
        }
        #region //Variaveis

        private volatile bool _flagStop;
        private string[] path = new string[14];
        private conector_full_variable alwaysVariables = new conector_full_variable();
        string[] ports = SerialPort.GetPortNames();
        SerialPort serialPort1;
        string balanca_peso = "0,000";
        #endregion

        #region //Metodos e Funções

        public void RequestStop(bool para)
        {
            _flagStop = para;
        }

        public string RetornoPeso()
        {
            return balanca_peso;
        }
        public void buscaPeso()
        {
            foreach (string port in ports)
            {
                if (port == alwaysVariables.PortBalanca)
                {
                    RequestStop(false);
                    break;
                }
            }
            while (!_flagStop)
            {
                if (alwaysVariables.PortBalanca != "")
                {
                    if (!serialPort1.IsOpen)
                    {
                        OpenPorta(alwaysVariables.PortBalanca);   
                    }

                    if (alwaysVariables.PortBalanca != "")
                    {
                        while(serialPort1.IsOpen)
                        {
                            if (Pede() > 0)
                            {
                                balanca_peso = Recebe();// == "" ? "0,000": Recebe();
                                if (balanca_peso == "")
                                {
                                    balanca_peso = "0,000";
                                }

                                if (_flagStop)
                                {
                                    fecha();
                                    return;
                                }
                            }
                            else
                            {
                                fecha();
                            }
                        }
                    }

                }
            }
            fecha();
        }
        public void fecha()
        {

            if (serialPort1.PortName != "")
            {
                if (serialPort1.IsOpen)
                {
                    serialPort1.Close();
                }
            }
        }

        public string Recebe()
        {
            string ret = "";
            string ret1 = "";
            string ret2 = "";

            if (serialPort1.PortName != "")
            {
                if (serialPort1.IsOpen)
                {
                    byte[] data = new byte[serialPort1.BytesToRead];
                    serialPort1.Read(data, 0, data.Length);

                    string EntradaStr = "";
                    foreach (byte b in data)
                    {
                        if (b <= 57)
                        {
                            if (b >= 48)
                            {
                                char Letra = Convert.ToChar(b);
                                EntradaStr = EntradaStr + Convert.ToString(Letra);
                            }
                        }
                    }

                    if (EntradaStr.Length > 4)
                    {
                        ret = EntradaStr.Insert(EntradaStr.Length - 3, ".");
                    }

                }
                else
                {
                    return "";
                }
            }
            return ret;
        }
        public int Pede()
        {

            if (serialPort1.PortName != "")
            {
                serialPort1.Write(Convert.ToString((char)5));
                System.Threading.Thread.Sleep(250);
            }
            return serialPort1.BytesToRead;
        }
        void OpenPorta(string porta)
        {

            if (porta != "")
            {
                if (!serialPort1.IsOpen)
                {
                    try
                    {
                        serialPort1 = new SerialPort();
                        serialPort1.Close();
                        serialPort1.PortName = porta;
                        serialPort1.BaudRate = 9600;
                        serialPort1.Parity = Parity.None;
                        serialPort1.DataBits = 8;
                        serialPort1.StopBits = StopBits.One;
                        serialPort1.ReadTimeout = 500;
                        serialPort1.WriteTimeout = 500;
                        if (porta != "")
                        {
                            try
                            {
                                serialPort1.Open();
                            }
                            catch (Exception)
                            {

                            }
                        }
                    }
                    finally
                    {
                        bool _continue = true;
                    }

                }

            }

        }
        #endregion

    }
}