using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;

// namespace Sintegra
namespace conectorPDV001
{
    class sintegraClass
    {
        public sintegraClass() //Construtor.
        {
        }
        
        //#########################################################Variavel Enpsulada########################################################
        ProcessStartInfo ProcessInfo;
        Process myProcess;
        private dados banco = new dados();
        const string folderSintegra = @"c:\";
        private Int32 countField = 0;
        private Int32 countRows = 0;
        private Int32 posSeparator;
        private string[,] matriz; //Matriz Bidimencionada
        private string[,] matriz60M; //Matriz Bidimencionada
        string[] vetor = new string[3] { "60A", "60D", "60I" };
        string[] vetorInatial = new string[50] { "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#" };
        string strCaminho = @"c:\";
        private int i, j;
        private int auxConsistencia;
        private int _count = 0;

        public int count 
        {
            get { return _count; }
            set { _count = value; }
        }

        public string retorno
        {
            get { return strCaminho; }
            set { strCaminho = value; }
        }
        //#########################################################End Variavel Enpsulada####################################################
        //#########################################################Funções Sintegra##########################################################
        public Boolean iniciaSintegra(string caminho, [MarshalAs(UnmanagedType.VBByRefStr)] ref string referencia)
        {
            Boolean retorno = false;
            posSeparator = caminho.IndexOf(".");
            if (posSeparator == -1)
            {
                referencia = caminho + String.Format("{0:yyyyMMdd-HHmmss}", DateTime.Now) + ".txt";
            }
            else
            {
                referencia = caminho.Substring(0, caminho.LastIndexOf(".")) + ".txt";
            }
            if (!File.Exists(caminho))
            {
                StreamWriter sw = new StreamWriter(referencia, false);
                //sw.Write("");
                retorno = true;
                sw.Close();
            }
            else
            {
                retorno = false;
            }
            return retorno;
        }
        public Boolean geraSintegra(Boolean valida, string caminho, string[] auxTipo, string di, string df, string store, string finalidade, string modelo , string numeroECF, string numeroSequencial)
        {
            Boolean retorno = valida;
            if (File.Exists(caminho))
            {
                File.Delete(caminho);
                strCaminho = caminho;
                StreamWriter sw = new StreamWriter(caminho, true, Encoding.ASCII);
                
                //################################################################Instrução Banco
                auxConsistencia = 0;
                countField = 0;
                countRows = 0;
                for (int x = 0; x < auxTipo.Length; x++)
                { 
                    if (auxTipo[x] != "#" && Convert.ToInt32(auxTipo[x].Substring(0,2)) > 0)
                    {
                        try
                        {
                            auxConsistencia = 0;
                            banco.abreConexao();
                            banco.startTransaction("conectorPDV_sintegra");
                            banco.addParametro("tipo", auxTipo[x]);
                            banco.addParametro("di", di);
                            banco.addParametro("df", df);
                            banco.addParametro("store", store);
                            banco.addParametro("geraProdNf", auxTipo[6] == "#" ? "0" : "1");
                            banco.addParametro("geraProdCp", auxTipo[11] == "#" ? "0" : "1");
                            banco.procedimentoSet();
                        }
                        catch (Exception erro) { auxConsistencia = 1; }
                        finally
                        {
                            if (auxConsistencia == 0)
                            {
                                Int32 count = banco.retornaSet().Tables[0].DefaultView.Count;
                                countField = banco.retornaSet().Tables[0].Columns.Count;
                                countRows = banco.retornaSet().Tables[0].DefaultView.Count;
                                if (countRows > 0)
                                {
                                    matriz = new string[countRows, countField];

                                    for (i = 0; i < count; i++)//Linha
                                    {
                                        for (j = 0; j < countField; j++) //Coluna
                                        {
                                            matriz[i, j] = Convert.ToString(banco.retornaSet().Tables[0].Rows[i][j]);
                                        }
                                        switch (auxTipo[x])
                                        {
                                            case "10":
                                                sw.Write(Registro10(matriz[i, 0], matriz[i, 1], matriz[i, 2], matriz[i, 3], matriz[i, 4], matriz[i, 5], matriz[i, 6], matriz[i, 7], matriz[i, 8], matriz[i, 9], matriz[i, 10], "1", valida) + "\r\n");
                                                break;
                                            case "11":
                                                sw.Write(Registro11(matriz[i, 0], matriz[i, 1], matriz[i, 2], matriz[i, 3], matriz[i, 4], matriz[i, 5], matriz[i, 6], matriz[i, 7], valida) + "\r\n");
                                                break;
                                            case "50":
                                                sw.Write(Registro50(matriz[i, 0], matriz[i, 1], matriz[i, 2], matriz[i, 3], matriz[i, 4], matriz[i, 5], matriz[i, 6], matriz[i, 7], matriz[i, 8], matriz[i, 9], matriz[i, 10], matriz[i, 11], matriz[i, 12], matriz[i, 13], matriz[i, 14], matriz[i, 15], matriz[i, 16], valida) + "\r\n");
                                                break;
                                            case "51":
                                                sw.Write(Registro51(matriz[i, 0], matriz[i, 1], matriz[i, 2], matriz[i, 3], matriz[i, 4], matriz[i, 5], matriz[i, 6], matriz[i, 7], matriz[i, 8], matriz[i, 9], matriz[i, 10], matriz[i, 11], matriz[i, 12], matriz[i, 13], valida) + "\r\n");
                                                break;
                                            case "54":
                                                sw.Write(Registro54(matriz[i, 1], matriz[i, 2], matriz[i, 3], matriz[i, 4], matriz[i, 5], matriz[i, 6], matriz[i, 7], matriz[i, 8], matriz[i, 9], matriz[i, 10], matriz[i, 11], matriz[i, 12], matriz[i, 13], matriz[i, 14], matriz[i, 15], matriz[i, 16], valida) + "\r\n");
                                                break;
                                            case "70":
                                                sw.Write(Registro70(matriz[i, 0], matriz[i, 1], matriz[i, 2], matriz[i, 3], matriz[i, 4], matriz[i, 5], matriz[i, 6], matriz[i, 7], matriz[i, 8], matriz[i, 9], matriz[i, 10], matriz[i, 11], matriz[i, 12], matriz[i, 13], matriz[i, 14], matriz[i, 15], matriz[i, 16], valida) + "\r\n");
                                                break;
                                            case "75":
                                                sw.Write(Registro75("75", matriz[i, 2], matriz[i, 3], matriz[i, 4], matriz[i, 5], matriz[i, 6], matriz[i, 7], matriz[i, 8], matriz[i, 9], matriz[i, 10], matriz[i, 11], valida) + "\r\n");
                                                break;
                                            case "60M":
                                                matriz60M = new string[countRows, countField];
                                                for (int a = 0; a < countRows; a++)//Linha
                                                {
                                                    for (int b = 0; b < countField; b++) //Coluna
                                                    {
                                                        matriz60M[a, b] = Convert.ToString(banco.retornaSet().Tables[0].Rows[a][b]);
                                                    }
                                                }

                                                banco.fechaConexao();
                                                
                                                //sw.Write(Registro60M("60", "M", matriz[i, 0], matriz[i, 2], matriz[i, 1], "2D", matriz[i, 5], matriz[i, 6], matriz[i, 7], matriz[i, 8], matriz[i, 9], matriz[i, 10],"", valida) + "\r\n");

                                                for (int c = 0; c < count; c++)
                                                {
                                                    sw.Write(Registro60M("60", "M", matriz60M[c, 0], matriz60M[c, 2], matriz60M[c, 1], "2D", matriz60M[c, 5], matriz60M[c, 6], matriz60M[c, 7], matriz60M[c, 8], matriz60M[i, 9], matriz60M[c, 10], "", valida) + "\r\n");


                                                    matriz60M[c, 11] = matriz60M[c, 11].Insert(2, "/");
                                                    matriz60M[c, 11] = matriz60M[c, 11].Insert(5, "/");

                                                    for (int d = 0; d < vetor.Length; d++)
                                                    {
                                                        auxConsistencia = 0;
                                                        countField = 0;
                                                        countRows = 0;
                                                        if (vetor[d] != "#" && Convert.ToInt32(vetor[d].Substring(0, 2)) > 0)
                                                        {
                                                            try
                                                            {
                                                                auxConsistencia = 0;
                                                                banco.abreConexao();
                                                                banco.startTransaction("conectorPDV_sintegra");
                                                                banco.addParametro("tipo", vetor[d]);
                                                                banco.addParametro("di", String.Format("{0:yyyyMMdd}", Convert.ToDateTime(matriz60M[c, 11])));
                                                                banco.addParametro("df", String.Format("{0:yyyyMMdd}", Convert.ToDateTime(matriz60M[c, 11])));
                                                                banco.addParametro("store", store);
                                                                banco.addParametro("geraProdNf", auxTipo[6] == "#" ? "0" : "1");
                                                                banco.addParametro("geraProdCp", auxTipo[11] == "#" ? "0" : "1");
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
                                                                        matriz = new string[countRows, countField];

                                                                        for (int e = 0; e < countRows; e++)//Linha
                                                                        {
                                                                            for (int f = 0; f < countField; f++) //Coluna
                                                                            {
                                                                                matriz[e, f] = Convert.ToString(banco.retornaSet().Tables[0].Rows[e][f]);
                                                                            }
                                                                            switch (vetor[d])
                                                                            {

                                                                                case "60A":
                                                                                    sw.Write(Registro60A("60", "A", matriz[e, 0], matriz[e, 1], matriz[e, 2], matriz[e, 3], "", valida) + "\r\n");
                                                                                    break;
                                                                                case "60D":
                                                                                    sw.Write(Registro60D("60", "D", matriz[e, 0], numeroECF, matriz[e, 1], matriz[e, 2], matriz[e, 3], matriz[e, 4], matriz[e, 5], matriz[e, 6], "", valida) + "\r\n");
                                                                                    break;
                                                                                case "60I":
                                                                                    sw.Write(Registro60I("60", "I", matriz[e, 0], numeroECF, "2D", matriz[e, 1], matriz[e, 2], matriz[e, 3], matriz[e, 4], matriz[e, 5], matriz[e, 6], matriz[e, 7], matriz[e, 8], "", valida) + "\r\n");
                                                                                    break;
                                                                                /*case "60R":
                                                                                    sw.Write(Registro60R("60", "R", matriz[e, 0], matriz[e, 1], matriz[e, 2], matriz[e, 3], matriz[e, 4], matriz[e, 5], "", valida) + "\r\n");
                                                                                    break;*/
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                                banco.fechaConexao();
                                                            }
                                                        }

                                                    }
                                                }
                                                i = count;
                                                break;
                                            /*case "60A":
                                                sw.Write(Registro60A("60", "A", matriz[i, 0], matriz[i, 1], matriz[i, 2], matriz[i, 3], "", valida) + "\r\n");
                                                break;
                                            case "60D":
                                                sw.Write(Registro60D("60", "D", matriz[i, 0], numeroECF, matriz[i, 1], matriz[i, 2], matriz[i, 3], matriz[i, 4], matriz[i, 5], matriz[i, 6], "", valida) + "\r\n");
                                                break;
                                            case "60I":
                                                sw.Write(Registro60I("60", "I", matriz[i, 0], numeroECF,"2D", matriz[i, 1], matriz[i, 2], matriz[i, 3], matriz[i, 4], matriz[i, 5], matriz[i, 6], matriz[i, 7], matriz[i, 8], "", valida) + "\r\n");
                                                break;*/
                                            case "60R":
                                                if (valida == true)
                                                {
                                                    sw.Write(Registro60R("60", "R", matriz[i, 0], matriz[i, 1], matriz[i, 2], matriz[i, 3], matriz[i, 4], matriz[i, 5], "", valida) + "\r\n");
                                                }
                                                else
                                                {
                                                    valida = true;
                                                    auxTipo[x] = "#";
                                                }
                                                break;
                                            /*case "61":
                                                sw.Write(Registro61(matriz[i, 0], matriz[i, 1], matriz[i, 2], matriz[i, 3], matriz[i, 4], matriz[i, 5], matriz[i, 6], matriz[i, 7], matriz[i, 8], matriz[i, 9], matriz[i, 10], matriz[i, 11], matriz[i, 12], matriz[i, 13], matriz[i, 14], matriz[i, 15], valida) + "\r\n");
                                                break;
                                            case "61R":
                                                sw.Write(Registro61R(matriz[i, 0], matriz[i, 1], matriz[i, 2], matriz[i, 3], matriz[i, 4], matriz[i, 5], matriz[i, 6], matriz[i, 7], matriz[i, 8], valida) + "\r\n");
                                                break;*/
                                            case "61":
                                                sw.Write(Registro61(matriz[i, 0], "", "", matriz[i, 1], matriz[i, 2], matriz[i, 3], matriz[i, 4], matriz[i, 5], matriz[i, 6], matriz[i, 7], matriz[i, 8], matriz[i, 9], matriz[i, 10], matriz[i, 11], matriz[i, 12], "", true) + "\r\n");
                                                break;
                                            case "61R":
                                                sw.Write(Registro61R(matriz[i, 0], "R", matriz[i, 1], matriz[i, 2], matriz[i, 3], matriz[i, 4], matriz[i, 5], matriz[i, 6], "", true) + "\r\n");
                                                break;
                                            case "90":
                                                sw.Write(Registro90("90", matriz[i, 9], matriz[i, 10], matriz[i, 0], matriz[i, 1], matriz[i, 2], matriz[i, 3], matriz[i, 4], matriz[i, 5], matriz[i, 6], matriz[i, 7], matriz[i, 8], "1", true) + "\r");
                                                break;
                                        }
                                    }
                                }
                                else if (auxTipo[x] == "60M")
                                {
                                    if (count > 0)
                                    {
                                        valida = true;
                                    }
                                    else
                                    {
                                        valida = false;
                                    }
                                }
                            }
                            banco.fechaConexao();
                        }
                    }
                }
                //################################################################End Instrução Banco
                
                retorno = true;
                sw.Close();
            }
            else
            {
                retorno = false;
            }
            if (auxConsistencia == 0)
            {
                msgInfo msg = new msgInfo(1, "Caro Usuário: Arquivo gerado, caminho: " + caminho); msg.ShowDialog();
            }
            return retorno;
        }
        protected string Registro10(string type, string CGC_MF, string Insc_Est, string Nome_Contribuinte, string Municipio, string UF, string Fax, string Data_Inicial, string Data_Final, string Cod_Convenio, string Cod_Operacao, string Cod_Finalidade, Boolean valida)
        {
            if (valida == true)
            {
                //return type.PadLeft(2, '0') + CGC_MF.PadRight(14, ' ') + Insc_Est.PadRight(14, ' ') + Nome_Contribuinte.PadRight(35, ' ') + Municipio.PadRight(30, ' ') + UF.PadLeft(2, ' ') + Fax.PadLeft(10, ' ') + Data_Inicial.PadLeft(8, ' ') + Data_Final.PadLeft(8, ' ') + Cod_Convenio.PadLeft(1, ' ') + Cod_Operacao.PadLeft(1, ' ') + Cod_Finalidade.PadLeft(1, ' ');
                return type.PadLeft(2, '0') + CGC_MF.PadLeft(14, '0') + Insc_Est.PadRight(14, ' ') + Nome_Contribuinte.PadRight(35, ' ') + Municipio.PadRight(30, ' ') + UF.PadRight(2, ' ') + Fax.PadLeft(10, '0') + Data_Inicial.PadLeft(8, '0') + Data_Final.PadLeft(8, '0') + Cod_Convenio.PadRight(1, ' ') + Cod_Operacao.PadRight(1, ' ') + Cod_Finalidade.PadRight(1, ' ');
            }
            else
            {
                return null;
            }
        }
        protected string Registro11(string type, string Logradouro, string Nro, string Complemento, string Bairro, string CEP, string Nome_Contato, string Telefone, Boolean valida)
        {
            if (valida == true)
            {
                return type.PadLeft(2, '0') + Logradouro.PadRight(34, ' ') + Nro.PadLeft(5, '0') + Complemento.PadRight(22, ' ') + Bairro.PadRight(15, ' ') + CEP.PadRight(8, ' ') + Nome_Contato.PadRight(28, ' ') + Telefone.PadLeft(12, '0');
            }
            else
            {
                return null;
            }
        }
        protected string Registro50(string type, string CNPJ, string Insc_Est, string Data_Emissao_Recebimento, string UF, string Modelo, string Serie, string Nro, string CFOP, string Emitente, string Valor_Total, string Base_ICMS, string Valor_ICMS, string Isenta, string Outras, string Aliquota, string Situacao, Boolean valida)
        {
            if (valida == true)
            {
                return type.PadLeft(2, '0') + CNPJ.PadLeft(14, '0') + Insc_Est.PadRight(14, ' ') + Data_Emissao_Recebimento.PadLeft(8, '0') + UF.PadRight(2, ' ') + Modelo.PadLeft(2, '0') + Serie.PadRight(3, ' ') + Nro.PadLeft(6, '0') + CFOP.PadLeft(4, '0') + Emitente.PadRight(1, ' ') + Valor_Total.PadLeft(13, '0') + Base_ICMS.PadLeft(13, '0') + Valor_ICMS.PadLeft(13, '0') + Isenta.PadLeft(13, '0') + Outras.PadLeft(13, '0') + Aliquota.PadRight(4, '0') + Situacao.PadRight(1, ' ');
            }
            else
            {
                return null;
            }
        }
        protected string Registro51(string type, string CNPJ, string Insc_Est, string Data_Emissao_Recebimento, string UF, string Serie, string Nro, string CFOP, string Valor_Total, string Valor_IPI, string Isenta_IPI, string Outras_IPI, string Brancos, string Situacao, Boolean valida)
        {        
            if (valida == true)
            {
                return type.PadLeft(2, '0') + CNPJ.PadLeft(14, '0') + Insc_Est.PadRight(14, ' ') + Data_Emissao_Recebimento.PadLeft(8, '0') + UF.PadRight(2, ' ') + Serie.PadRight(3, ' ') + Nro.PadLeft(6, '0') + CFOP.PadLeft(4, '0') + Valor_Total.PadLeft(13, '0') + Valor_IPI.PadLeft(13, '0') + Isenta_IPI.PadLeft(13, '0') + Outras_IPI.PadLeft(13, '0') + Brancos.PadRight(20, ' ') + Situacao.PadRight(1, ' ');
            }
            else
            {
                return null;
            }
        }
        protected string Registro54(string type, string CNPJ, string Modelo, string Serie, string Nro, string CFOP, string CST, string Nro_Item, string Cod_Produto_Servico, string Quantidade, string Valor_Produto, string Valor_Desconto, string Base_ICMS, string Base_ICMS_S_Trib, string Valor_IPI, string Aliquota_ICMS, Boolean valida)
        {
            if (valida == true)
            {
                return type.PadLeft(2, '0') + CNPJ.PadLeft(14, '0') + Modelo.PadLeft(2, '0') + Serie.PadRight(3, ' ') + Nro.PadLeft(6, '0') + CFOP.PadLeft(4, '0') + CST.PadRight(3, ' ') + Nro_Item.PadLeft(3, '0') + Cod_Produto_Servico.PadRight(14, ' ') + Quantidade.PadLeft(11, '0') + Valor_Produto.PadLeft(12, '0') + Valor_Desconto.PadLeft(12, '0') + Base_ICMS.PadLeft(12, '0') + Base_ICMS_S_Trib.PadLeft(12, '0') + Valor_IPI.PadLeft(12, '0') + Aliquota_ICMS.PadRight(4, '0');
            }
            else
            {
                return null;
            }
        }
        protected string Registro75(string type, string Data_Inicial, string Data_Final, string Cod_Produto_Servico, string Descricao, string Cod_NCM, string UND_Medida, string Aliquota_IPI, string Aliquota_ICMS, string Reducao_Base_ICMS, string Base_ICMS, Boolean valida)
        {
            if (valida == true)
            {
                return type.PadLeft(2, '0') + Data_Inicial.PadLeft(8, '0') + Data_Final.PadLeft(8, '0') + Cod_Produto_Servico.PadRight(14, ' ') + Cod_NCM.PadRight(8, ' ') + Descricao.PadRight(53, ' ') + UND_Medida.PadRight(6, ' ') + Aliquota_IPI.PadLeft(5, '0') + Aliquota_ICMS.PadLeft(4, '0') + Reducao_Base_ICMS.PadLeft(5, '0') + Base_ICMS.PadLeft(13, '0');
            }
            else
            {
                return null;
            }
        }
        protected string Registro70(string type, string CNPJ, string Insc_Est, string Data_Emissao_Utilizacao, string UF, string Modelo, string Serie, string SubSerie, string Nro, string CFOP, string Valor_Total, string Base_ICMS, string Valor_ICMS, string Isenta, string Outras, string CIF_FOB, string Situacao, Boolean valida)
        {

            if (valida == true)
            {
                return type.PadLeft(2, '0') + CNPJ.PadLeft(14, '0') + Insc_Est.PadRight(14, ' ') + Data_Emissao_Utilizacao.PadLeft(8, '0') + UF.PadRight(2, ' ') + Modelo.PadLeft(2, '0') + Serie.PadRight(1, ' ') + SubSerie.PadRight(2, ' ') + Nro.PadLeft(6, '0') + CFOP.PadLeft(4, '0') + Valor_Total.PadLeft(13, '0') + Base_ICMS.PadLeft(14, '0') + Valor_ICMS.PadLeft(14, '0') + Isenta.PadLeft(14, '0') + Outras.PadLeft(14, '0') + CIF_FOB.PadLeft(1, '0') + Situacao.PadRight(1, ' ');
            }
            else
            {
                return null;
            }
        }
        protected string Registro60M(string type, string Sub_Tipo, string Data_Emissao, string Nro_Serie_Eqp, string numero_ordem_sequencial, string modelo, string coo,string coo1,string crz, string cro, string venda_bruta, string valor_geral_totalizador, string Brancos, Boolean valida)
        {
            if (valida == true)
            {
                return type.PadLeft(2, '0') + Sub_Tipo.PadRight(1, ' ') + Data_Emissao.PadLeft(8, '0') + Nro_Serie_Eqp.PadRight(20, ' ') + numero_ordem_sequencial.PadLeft(3, '0') + modelo.PadRight(2, ' ') + coo.PadLeft(6, '0') + coo1.PadLeft(6, '0') + crz.PadLeft(6, '0') + cro.PadLeft(3, '0') + venda_bruta.PadLeft(16, '0') + valor_geral_totalizador.PadLeft(16, '0') + Brancos.PadRight(37, ' ');
            }
            else
            {
                return null;
            }
        }
        protected string Registro60A(string type, string Sub_Tipo, string Data_Emissao, string Nro_Serie_Eqp, string S_Trib_Aliquota, string Valor_TParcial, string Brancos, Boolean valida)
        {
            if (valida == true)
            {
                return type.PadLeft(2, '0') + Sub_Tipo.PadRight(1, ' ') + Data_Emissao.PadLeft(8, '0') + Nro_Serie_Eqp.PadRight(20, ' ') + S_Trib_Aliquota.PadRight(4, ' ') + Valor_TParcial.PadLeft(12, '0') + Brancos.PadRight(79, ' ');
            }
            else
            {
                return null;
            }
        }
        protected string Registro60D(string type, string Sub_Tipo, string Data_Emissao, string Nro_Serie_Eqp, string Cod_Produto, string Quantidade, string Valor_Produto, string Base_ICMS, string S_Trib_Aliquota, string Valor_ICMS, string Brancos, Boolean valida)
        {
            if (valida == true)
            {
                return type.PadLeft(2, '0') + Sub_Tipo.PadRight(1, ' ') + Data_Emissao.PadLeft(8, '0') + Nro_Serie_Eqp.PadRight(20, ' ') + Cod_Produto.PadRight(14, ' ') + Quantidade.PadLeft(13, '0') + Valor_Produto.PadLeft(16, '0') + Base_ICMS.PadLeft(16, '0') + S_Trib_Aliquota.PadRight(4, ' ') + Valor_ICMS.PadLeft(13, '0') + Brancos.PadRight(19, ' ');
            }
            else
            {
                return null;
            }
        }
        protected string Registro60I(string type, string Sub_Tipo, string Data_Emissao, string Nro_Serie_Eqp, string Modelo_Doc, string COO, string Nro_Item, string Cod_Produto, string Quantidade, string Valor_Produto, string Base_ICMS, string S_Trib_Aliquota, string Valor_ICMS, string Brancos, Boolean valida)
        {
            if (valida == true)
            {
                return type.PadLeft(2, '0') + Sub_Tipo.PadRight(1, ' ') + Data_Emissao.PadLeft(8, '0') + Nro_Serie_Eqp.PadRight(20, ' ') + Modelo_Doc.PadRight(2, ' ') + COO.PadLeft(6, '0') + Nro_Item.PadLeft(3, '0') + Cod_Produto.PadRight(14, ' ') + Quantidade.PadLeft(13, '0') + Valor_Produto.PadLeft(13, '0') + Base_ICMS.PadLeft(12, '0') + S_Trib_Aliquota.PadRight(4, ' ') + Valor_ICMS.PadLeft(12, '0') + Brancos.PadRight(16, ' ');
            }
            else
            {
                return null;
            }
        }
        protected string Registro60R(string type, string Sub_Tipo, string Mes_Ano_Emissao, string Cod_Produto_Servico, string Quantidade, string Valor_Produto, string Base_ICMS, string S_Trib_Aliquota, string Brancos, Boolean valida)
        {
            if (valida == true)
            {
                return type.PadLeft(2, '0') + Sub_Tipo.PadRight(1, ' ') + Mes_Ano_Emissao.PadLeft(6, '0') + Cod_Produto_Servico.PadRight(14, ' ') + Quantidade.PadLeft(13, '0') + Valor_Produto.PadLeft(16, '0') + Base_ICMS.PadLeft(16, '0') + S_Trib_Aliquota.PadRight(4, ' ') + Brancos.PadRight(54, ' ');
            }
            else
            {
                return null;
            }
        }
        protected string Registro61(string type, string Brancos_2, string Brancos_3, string Data_Emissao, string Modelo, string Serie, string SubSerie, string Nro_Ordem_Inicio, string Nro_Ordem_Fim, string ValorTotal, string Base_ICMS, string Valor_ICMS, string Isenta, string Outras, string Aliquota, string Branco, Boolean valida)
        {
            if (valida == true)
            {
                return type.PadLeft(2, '0') + Brancos_2.PadRight(14, ' ') + Brancos_3.PadRight(14, ' ') + Data_Emissao.PadLeft(8, '0') + Modelo.PadLeft(2, '0') + Serie.PadRight(3, ' ') + SubSerie.PadRight(2, ' ') + Nro_Ordem_Inicio.PadLeft(6, '0') + Nro_Ordem_Fim.PadLeft(6, '0') + ValorTotal.PadLeft(13, '0') + Base_ICMS.PadLeft(13, '0') + Valor_ICMS.PadLeft(12, '0') + Isenta.PadLeft(13, '0') + Outras.PadLeft(13, '0') + Aliquota.PadRight(4, '0') + Branco.PadRight(1, ' ');
            }
            else
            {
                return null;
            }
        }
        protected string Registro61R(string type, string Sub_Tipo, string Mes_Ano_Emissao, string Cod_Produto_Servico, string Quantidade, string Valor_Produto, string Base_ICMS, string S_Trib_Aliquota, string Brancos, Boolean valida)
        {
            if (valida == true)
            {
                return type.PadLeft(3, '0') + /*Sub_Tipo.PadRight(1, ' ') +*/ Mes_Ano_Emissao.PadLeft(6, '0') + Cod_Produto_Servico.PadRight(14, ' ') + Quantidade.PadLeft(13, '0') + Valor_Produto.PadLeft(16, '0') + Base_ICMS.PadLeft(16, '0') + S_Trib_Aliquota.PadRight(4, '0') + Brancos.PadRight(54, ' ');
            }
            else
            {
                return null;
            }
        }

        protected string Registro90(string type, string CGC_MF, string Insc_Est, string R50, string R53, string R54, string R60, string R61, string R70, string R75, string R88, string R99, string branco, Boolean valida)
        {
            if (valida == true)
            {

                return type.PadLeft(2, '0') + CGC_MF.PadLeft(14, '0') + Insc_Est.PadRight(14, ' ') + "50" + R50.PadLeft(8, '0') + "53" + R53.PadLeft(8, '0') + "54" + R54.PadLeft(8, '0') + "60" + R60.PadLeft(8, '0') + "61" + R61.PadLeft(8, '0') + "70" + R70.PadLeft(8, '0') + "75" + R75.PadLeft(8, '0') + "88" + R88.PadLeft(8, '0') + "99" + R99.PadLeft(8, '0') + branco.PadLeft(6, ' ');
            }
            else
            {
                return null;
            }
        }
        /*protected string Registro88(string type, string Sub_Tipo, string Tipo_Info, string Insc_Est, string Data_Emissao_Recebimento, string UF, string Modelo, string Serie, string Nro, string Valor_Mercadoria, string Valor_ICMS_Diferido, string DI, string Data_DI, string escricao_Mercadoria)
        {
            return type.PadLeft(2, '0') +  Sub_Tipo.PadRight(2, ' ') + Tipo_Info.PadRight(2, ' ') + Insc_Est.PadRight(14, ' ') + Quantidade.PadLeft(13, '0') + Valor_Produto.PadLeft(16, '0') + Base_ICMS.PadLeft(16, '0') + S_Trib_Aliquota.PadRight(4, '0') + Brancos.PadRight(54, ' ');
        }*/
        //#########################################################Funções Sintegra##########################################################
    }
}