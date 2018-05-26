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
    class conectorSpedFisco
    {
        public conectorSpedFisco()//Construtor.
        {
            valida150 = true;
        }

        //#########################################################Variavel Enpsulada########################################################
        ProcessStartInfo ProcessInfo;
        Process myProcess;
        private dados banco = new dados();
        const string folderSped = @"c:\";
        private Int32 countField = 0;
        private Int32 countRows = 0;
        private Int32 posSeparator;
        private string[,] matriz; //Matriz Bidimencionada
        private string[,] matrizSub; //Matriz Bidimencionada
        private bool valida150;
        string[] vetor = new string[84] { "0000", "0001", "0005", "0100", "0150", "0175", "0190", "0200", "0205", "0206", "0220", "0400", "0450", "0460", "0990", "C001", "C100", "C105", "C110", "C111", "C112", "C113", "C114", "C115", "C116", "C120", "C130", "C140", "C141", "C170", "C179", "C190", "C195", "C300", "C310", "C320", "C321", "C350", "C370", "C390", "C400", "C405", "C410", "C420", "C425", "C460", "C465", "C470", "C490", "C495", "C800", "C850", "C860", "C890", "C990", "D001", "D100", "D190", "D195", "D990", "E001", "E100", "E110", "E116", "E990", "G001", "G110", "G125", "G130", "G140", "G990", "H001", "H005", "H010", "H020", "H990", "1001", "1010", "1600", "1990", "9001", "9900","9990","9999" };
        //string[] vetorInatial = new string[50] { "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#" };
        string[,] BL9900 = new string[84,2];
        string[,] bl0150;
        string[,] bl0175;
        string[,] bl0200;
        string[,] bl0205;
        string[,] blC100;
        string[,] blC170;
        string[,] blC190;
        string[,] blC400;
        string[,] blC405;
        string[,] blC420;
        string[,] blC425;
        string[,] blC460;
        string[,] blC470;
        string[,] blC490;
        string[,] blC300;
        string[,] blC320;
        string[,] blC321;
        string[,] blC350;
        string[,] blC370;
        string[,] blC390;
        int count0990 = 1;
        int countC990 = 1;
        int countD990 = 1;
        int countE990 = 1;
        int countH990 = 1;
        int countG990 = 1;
        int count1111 = 1;
        int count1600 = 1;
        int count9900 = 1;
        int count9990 = 1;
        int count9999 = 1;
        Int16 call_creditos = 0;
        Int16 call_debitos_nf = 0;
        Int16 call_debitos_cp = 0;
        private int i, j;
        private int auxConsistencia;
        private int _count = 0;
        private conector_full_variable alwaysVariables = new conector_full_variable();
        //#########################################################End Variavel Enpsulada####################################################

        //#########################################################Funções SPED##############################################################
        public Boolean iniciaSpedFiscal(string caminho, [MarshalAs(UnmanagedType.VBByRefStr)] ref string referencia)
        {
            Boolean retorno = false;
            count0990 = 1;
            countC990 = 1;
            countD990 = 1;
            countE990 = 1;
            countH990 = 1;
            countG990 = 1;
            count1111 = 1;
            count1600 = 1;
            count9900 = 1;
            count9990 = 1;
            count9999 = 1;
            posSeparator = caminho.IndexOf(".");
            if (posSeparator == -1)
            {
                referencia = caminho + "SPED-" + alwaysVariables.PAF_laudo + String.Format("{0:yyyyMMdd-HHmmss}", DateTime.Now) + ".txt";
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
        public Boolean geraSpedFiscal(Boolean valida, string caminho, string[] auxTipo, string di, string df, string store, string finalidade, string pessoa, string dh, string perfil, string atividade, int mov0, int movC, int movD, int movE, int movF, int movH, int mov1, int mov9, int movG, string motivo)
        {
            Boolean retorno = valida;

            if (File.Exists(caminho))
            {

                File.Delete(caminho);

                StreamWriter sw = new StreamWriter(caminho, true, Encoding.ASCII);

                //################################################################Instrução Banco
                auxConsistencia = 0;
                countField = 0;
                countRows = 0;
                for (int x = 0; x < auxTipo.Length; x++)
                {
                    auxConsistencia = 0;

                    if (auxTipo[x] != "#")
                    {

                        try
                        {
                            auxConsistencia = 0;
                            banco.abreConexao();
                            banco.startTransaction("conectorPDV_EFD_008");
                            banco.addParametro("tipo", auxTipo[x]);
                            banco.addParametro("find_loja", store);
                            banco.addParametro("find_pessoa", pessoa);
                            banco.addParametro("di", di);
                            banco.addParametro("df", df);
                            banco.addParametro("dateInv", dh);
                            banco.addParametro("finalidade", finalidade);
                            banco.addParametro("perfil", perfil.Replace("\r\n","").Trim());
                            banco.addParametro("atividade", atividade);
                            banco.addParametro("call_creditos", call_creditos.ToString());
                            banco.addParametro("call_debitos_nf", call_debitos_nf.ToString());
                            banco.addParametro("call_debitos_cp", call_debitos_cp.ToString());
                            banco.addParametro("invmovH", movH.ToString());

                            banco.procedimentoSet();
                        }
                        catch (Exception erro)
                        {

                            switch (auxTipo[x])
                            {
                                case "G001":
                                    sw.Write(registroG001(movG.ToString()) + "\r\n");
                                    countRows = 1;
                                    break;
                                case "G990":
                                    //sw.Write(registroG990(matriz[i, 0]) + "\r\n");
                                    sw.Write(registroG990(countG990.ToString()) + "\r\n");
                                    countRows = 1;
                                    break;
                                case "D990":
                                    //sw.Write(registroD990(matriz[i, 0]) + "\r\n");
                                    sw.Write(registroD990(countD990.ToString()) + "\r\n");
                                    countRows = 1;
                                    break;
                                case "1990":
                                    sw.Write(registro1990((count1111).ToString()) + "\r\n");
                                    countRows = 1;
                                    break;
                                case "C370":
                                    countRows = 0;
                                    countField = 0;
                                    break;
                                case "C321":
                                    countRows = 0;
                                    countField = 0;
                                    break;
                                case "C320":
                                    countRows = 0;
                                    countField = 0;
                                    break;
                                case "C390":
                                    countRows = 0;
                                    countField = 0;
                                    break;
                                case "C350":
                                    countRows = 0;
                                    countField = 0;
                                    break;
                                case "E100":
                                    sw.Write(registroE100(di, df) + "\r\n");
                                    countRows = 1;
                                    break;
                                case "E990":
                                    //sw.Write(registroE990(matriz[i, 0]) + "\r\n");
                                    sw.Write(registroE990(countE990.ToString()) + "\r\n");
                                    countRows = 1;
                                    break;
                                case "H990":
                                    sw.Write(registroH990(countH990.ToString()) + "\r\n");
                                    countRows = 1;
                                    break;
                                case "9001":
                                    //sw.Write(registro9001(matriz[i, 0]) + "\r\n");
                                    sw.Write(registro9001(mov9.ToString()) + "\r\n");
                                    countRows = 1;
                                    break;
                                case "9900":
                                    if (BL9900 != null)
                                    {
                                        for (int r = 0; r < BL9900.GetLength(0); r++)
                                        {
                                            if (BL9900[r, 0] != null && BL9900[r, 1] != null)
                                            {
                                                sw.Write(registro9900(BL9900[r, 0], BL9900[r, 1]) + "\r\n");
                                            }
                                        }
                                        sw.Write(registro9900("9900", (count9900 + 2).ToString()) + "\r\n");
                                        sw.Write(registro9900("9990", 1.ToString()) + "\r\n");
                                        sw.Write(registro9900("9999", 1.ToString()) + "\r\n");
                                    }
                                    break;
                                case "9990":
                                    sw.Write(registro9990((count9990 + 2).ToString()) + "\r\n");
                                    break;
                                case "9999":
                                    sw.Write(registro9999((count9999).ToString()) + "\r\n");
                                    break;
                            }

                            auxConsistencia = 1;
                        }
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

                                        switch (auxTipo[x])//Preenchimento Unico
                                        {
                                            case "0000":
                                                sw.Write(registro0000(matriz[i, 0], matriz[i, 1], matriz[i, 2], matriz[i, 3], matriz[i, 4], matriz[i, 5], matriz[i, 6], matriz[i, 7], matriz[i, 8], matriz[i, 9], matriz[i, 10], matriz[i, 11], matriz[i, 12], matriz[i, 13]) + "\r\n");
                                                break;
                                            case "0001":
                                                sw.Write(registro0001(mov0.ToString()) + "\r\n");
                                                break;
                                            case "0005":
                                                sw.Write(registro0005(matriz[i, 0], matriz[i, 1], matriz[i, 2], matriz[i, 3], matriz[i, 4], matriz[i, 5], matriz[i, 6], matriz[i, 7], matriz[i, 8]) + "\r\n");
                                                break;
                                            case "0100":
                                                sw.Write(registro0100(matriz[i, 0], matriz[i, 1], matriz[i, 2], matriz[i, 3], matriz[i, 4], matriz[i, 5], matriz[i, 6], matriz[i, 7], matriz[i, 8], matriz[i, 9], matriz[i, 10], matriz[i, 11], matriz[i, 12]) + "\r\n");
                                                break;
                                            case "0190":
                                                sw.Write(registro0190(matriz[i, 0], matriz[i, 1]) + "\r\n");
                                                break;
                                            case "0400":
                                                sw.Write(registro0400(matriz[i, 0], matriz[i, 1]) + "\r\n");
                                                break;
                                            case "0450":
                                                sw.Write(registro0450(matriz[i, 0], matriz[i, 1]) + "\r\n");
                                                break;
                                            case "0990":
                                                //sw.Write(registro0990(matriz[i, 0]) + "\r\n");
                                                sw.Write(registro0990(count0990.ToString()) + "\r\n");
                                                break;
                                            case "C001":
                                                sw.Write(registroC001(movC.ToString()) + "\r\n");
                                                break;
                                            case "C105":
                                                sw.Write(registroC105(matriz[i, 0], matriz[i, 1]) + "\r\n");
                                                break;
                                            case "C195":
                                                sw.Write(registroC195(matriz[i, 0], matriz[i, 1]) + "\r\n");
                                                break;
                                            /*case "C320":
                                                sw.Write(registroC320(matriz[i, 0], matriz[i, 1], matriz[i, 2], matriz[i, 3], matriz[i, 4], matriz[i, 5], matriz[i, 6], matriz[i, 7]) + "\r\n");
                                                break;
                                            case "C321":
                                                sw.Write(registroC370(matriz[i, 0], matriz[i, 1], matriz[i, 2], matriz[i, 3], matriz[i, 4], matriz[i, 5]) + "\r\n");
                                                break;
                                            case "C350":
                                                sw.Write(registroC350(matriz[i, 0], matriz[i, 1], matriz[i, 2], matriz[i, 3], matriz[i, 4], matriz[i, 5], matriz[i, 6], matriz[i, 7], matriz[i, 8], matriz[i, 9], matriz[i, 10]) + "\r\n");
                                                break;
                                            case "C370":
                                                sw.Write(registroC370(matriz[i, 0], matriz[i, 1], matriz[i, 2], matriz[i, 3], matriz[i, 4], matriz[i, 5]) + "\r\n");
                                                break;
                                            case "C390":
                                                sw.Write(registroC390(matriz[i, 0], matriz[i, 1], matriz[i, 2], matriz[i, 3]) + "\r\n");
                                                break;*/
                                            case "C990":
                                                //sw.Write(registroC990(matriz[i, 0]) + "\r\n");
                                                sw.Write(registroC990(countC990.ToString()) + "\r\n");
                                                break;
                                            case "D001":
                                                sw.Write(registroD001(movD.ToString()) + "\r\n");
                                                break;
                                            case "D100":
                                                sw.Write(registroD100(matriz[i, 0], matriz[i, 1], matriz[i, 2], matriz[i, 3], matriz[i, 4], matriz[i, 5], matriz[i, 6], matriz[i, 7], matriz[i, 8], matriz[i, 9], matriz[i, 10], matriz[i, 11], matriz[i, 12], matriz[i, 13], matriz[i, 14], matriz[i, 15], matriz[i, 16], matriz[i, 17], matriz[i, 18], matriz[i, 19], matriz[i, 20], matriz[i, 21]) + "\r\n");
                                                break;
                                            case "D190":
                                                sw.Write(registroD190(matriz[i, 0], matriz[i, 1], matriz[i, 2], matriz[i, 3], matriz[i, 4], matriz[i, 5], matriz[i, 6], matriz[i, 7]) + "\r\n");
                                                break;
                                            case "D990":
                                                //sw.Write(registroD990(matriz[i, 0]) + "\r\n");
                                                sw.Write(registroD990(countD990.ToString()) + "\r\n");
                                                break;
                                            case "E001":
                                                sw.Write(registroE001(movE.ToString()) + "\r\n");
                                                break;
                                            case "E100":
                                                sw.Write(registroE100(di, df) + "\r\n");
                                                break;
                                            case "E110":
                                                sw.Write(registroE110(matriz[i, 0], matriz[i, 1], matriz[i, 2], matriz[i, 3], matriz[i, 4], matriz[i, 5], matriz[i, 6], matriz[i, 7], matriz[i, 8], matriz[i, 9], matriz[i, 10], matriz[i, 11], matriz[i, 12], matriz[i, 13]) + "\r\n");
                                                break;
                                            case "E116":
                                                sw.Write(registroE116(matriz[i, 0], matriz[i, 1], matriz[i, 2], matriz[i, 3], matriz[i, 4], matriz[i, 5], matriz[i, 6], matriz[i, 7], matriz[i, 8]) + "\r\n");
                                                break;
                                            case "E990":
                                                //sw.Write(registroE990(matriz[i, 0]) + "\r\n");
                                                sw.Write(countE990.ToString() + "\r\n");
                                                break;
                                            case "G001":
                                                sw.Write(registroG001(movG.ToString()) + "\r\n");
                                                break;
                                            case "G990":
                                                //sw.Write(registroG990(matriz[i, 0]) + "\r\n");
                                                sw.Write(registroG990(countG990.ToString()) + "\r\n");
                                                break;
                                            case "H001":
                                                sw.Write(registroH001(movH.ToString()) + "\r\n");
                                                break;
                                            case "H005":
                                                sw.Write(registroH005(matriz[i, 0], matriz[i, 1].Replace(".",","), motivo) + "\r\n");
                                                break;
                                            case "H010":
                                                sw.Write(registroH010(matriz[i, 0], matriz[i, 1], matriz[i, 2], matriz[i, 3], matriz[i, 4], matriz[i, 5], matriz[i, 6], matriz[i, 7], matriz[i, 8], matriz[i, 9]) + "\r\n");
                                                break;
                                            case "H990":
                                                sw.Write(registroH990(countH990.ToString()) + "\r\n");
                                                break;
                                            case "1001":
                                                //sw.Write(registro1001(matriz[i, 0]) + "\r\n");
                                                sw.Write(registro1001(mov1.ToString()) + "\r\n");
                                                break;
                                            case "1010":
                                                if (matriz[i, 6] == "S")
                                                {
                                                    vetor[77] = "1600";
                                                }
                                                sw.Write(registro1010(matriz[i, 0], matriz[i, 1], matriz[i, 2], matriz[i, 3], matriz[i, 4], matriz[i, 5], matriz[i, 6], matriz[i, 7], matriz[i, 8]) + "\r\n");
                                                break;
                                            case "1600":
                                                sw.Write(registro1600(matriz[i, 0], matriz[i, 1], matriz[i, 2]) + "\r\n");
                                                break;
                                            case "1990":
                                                sw.Write(registro1990(count1111.ToString()) + "\r\n");
                                                break;
                                            case "9001":
                                                //sw.Write(registro9001(matriz[i, 0]) + "\r\n");
                                                sw.Write(registro9001(mov9.ToString()) + "\r\n");
                                                break;
                                            case "9900":
                                                if (BL9900 != null)
                                                {
                                                    for (int r = 0; r < BL9900.GetLength(0); r++)
                                                    {
                                                        if (BL9900[r, 0] != null && BL9900[r, 1] != null)
                                                        {
                                                            sw.Write(registro9900(BL9900[r, 0], BL9900[r, 1]) + "\r\n");
                                                        }
                                                    }
                                                    sw.Write(registro9900("9900", (count9900 + 1).ToString()) + "\r\n");
                                                    sw.Write(registro9900("9990", 1.ToString()) + "\r\n");
                                                    sw.Write(registro9900("9999", 1.ToString()) + "\r\n");
                                                }
                                                break;
                                            case "9990":
                                                sw.Write(registro9990((count9990 + 2).ToString()) + "\r\n");
                                                break;
                                            case "9999":
                                                sw.Write(registro9999((count9999).ToString()) + "\r\n");
                                                break;
                                        }
                                    }
                                    switch (auxTipo[x])//Preenchimento MultiDimencional
                                    {
                                        case "0150":
                                            bl0150 = new string[countRows, countField];
                                            bl0150 = matriz;
                                            //i = count;//termino
                                            //sw.Write(registro0150(matriz[i, 0], matriz[i, 1], matriz[i, 2], matriz[i, 3], matriz[i, 4], matriz[i, 5], matriz[i, 6], matriz[i, 7], matriz[i, 8], matriz[i, 9], matriz[i, 10], matriz[i, 11]) + "\r\n");
                                            break;
                                        case "0175":
                                            bl0175 = new string[countRows, countField];
                                            bl0175 = matriz;
                                            //i = count;//termino
                                            //sw.Write(registro0175(matriz[i, 0], matriz[i, 1], matriz[i, 2]) + "\r\n");
                                            break;
                                        case "0200":
                                            bl0200 = new string[countRows, countField];
                                            bl0200 = matriz;
                                            //i = count;//termino
                                            //sw.Write(registro0200(matriz[i, 0], matriz[i, 1], matriz[i, 2], matriz[i, 3], matriz[i, 4], matriz[i, 5], matriz[i, 6], matriz[i, 7], matriz[i, 8], matriz[i, 9], matriz[i, 10]) + "\r\n");
                                            break;
                                        case "0205":
                                            bl0205 = new string[countRows, countField];
                                            bl0205 = matriz;
                                            //i = count;//termino
                                            //sw.Write(registro0205(matriz[i, 0], matriz[i, 1], matriz[i, 2], matriz[i, 3]) + "\r\n");
                                            break;
                                        case "C100":
                                            blC100 = new string[countRows, countField];
                                            blC100 = matriz;
                                            //sw.Write(registroC100(matriz[i, 0], matriz[i, 1], matriz[i, 2], matriz[i, 3], matriz[i, 4], matriz[i, 5], matriz[i, 6], matriz[i, 7], matriz[i, 8], matriz[i, 9], matriz[i, 10], matriz[i, 11], matriz[i, 12], matriz[i, 13], matriz[i, 14], matriz[i, 15], matriz[i, 16], matriz[i, 17], matriz[i, 18], matriz[i, 19], matriz[i, 20], matriz[i, 21], matriz[i, 22], matriz[i, 23], matriz[i, 24], matriz[i, 25], matriz[i, 26], matriz[i, 27]) + "\r\n");
                                            break;
                                        case "C170":
                                            blC170 = new string[countRows, countField];
                                            blC170 = matriz;
                                            //sw.Write(registroC170(matriz[i, 0], matriz[i, 1], matriz[i, 2], matriz[i, 3], matriz[i, 4], matriz[i, 5], matriz[i, 6], matriz[i, 7], matriz[i, 8], matriz[i, 9], matriz[i, 10], matriz[i, 11], matriz[i, 12], matriz[i, 13], matriz[i, 14], matriz[i, 15], matriz[i, 16], matriz[i, 17], matriz[i, 18], matriz[i, 19], matriz[i, 20], matriz[i, 21], matriz[i, 22], matriz[i, 23], matriz[i, 24], matriz[i, 25], matriz[i, 26], matriz[i, 27], matriz[i, 28], matriz[i, 29], matriz[i, 30], matriz[i, 31], matriz[i, 32], matriz[i, 33], matriz[i, 34], matriz[i, 35]) + "\r\n");
                                            break;
                                        case "C190":// "C190", "C195", "C300", "C310", "C320", "C321", "C350", "C370", "C390", "C400", "C405", "C410", "C420", "C425", "C460", "C465", "C470", "C490", "C495", "C800", "C850", "C860", "C890", "C990", "D001", "D100", "D190", "D195", "D990", "E001", "E100", "E110", "E116", "E990", "G001", "G110", "G125", "G130", "G140", "G990", "H001", "H005", "H010", "H020", "H990", "1001", "1010", "1600", "1990", "9001", "9990"
                                            blC190 = new string[countRows, countField];
                                            blC190 = matriz;
                                            //sw.Write(registroC190(matriz[i, 0], matriz[i, 1], matriz[i, 2], matriz[i, 3], matriz[i, 4], matriz[i, 5], matriz[i, 6], matriz[i, 7], matriz[i, 8], matriz[i, 9], matriz[i, 10]) + "\r\n");
                                            break;
                                        case "C300":
                                            blC300 = new string[countRows, countField];
                                            blC300 = matriz;
                                            break;
                                        case "C320":
                                            blC320 = new string[countRows, countField];
                                            blC320 = matriz;
                                            break;
                                        case "C321":
                                            blC321 = new string[countRows, countField];
                                            blC321 = matriz;
                                            break;
                                        case "C350":
                                            blC350 = new string[countRows, countField];
                                            blC350 = matriz;
                                            break;
                                        case "C370":
                                            blC370 = new string[countRows, countField];
                                            blC370 = matriz;
                                            break;
                                        case "C390":
                                            blC390 = new string[countRows, countField];
                                            blC390 = matriz;
                                            break;
                                        case "C400":
                                            blC400 = new string[countRows, countField];
                                            blC400 = matriz;
                                            //sw.Write(registroC400(matriz[i, 0], matriz[i, 1], matriz[i, 2], matriz[i, 3]) + "\r\n");
                                            break;
                                        case "C405":
                                            blC405 = new string[countRows, countField];
                                            blC405 = matriz;
                                            //sw.Write(registroC405(matriz[i, 0], matriz[i, 1], matriz[i, 2]) + "\r\n");
                                            break;
                                        case "C420":
                                            blC420 = new string[countRows, countField];
                                            blC420 = matriz;
                                            //sw.Write(registroC420(matriz[i, 0], matriz[i, 1], matriz[i, 2], matriz[i, 3]) + "\r\n");
                                            break;
                                        case "C425":
                                            blC425 = new string[countRows, countField];
                                            blC425 = matriz;
                                            //sw.Write(registroC425(matriz[i, 0], matriz[i, 1], matriz[i, 2], matriz[i, 3], matriz[i, 4], matriz[i, 5]) + "\r\n");
                                            break;
                                        case "C460":
                                            blC460 = new string[countRows, countField];
                                            blC460 = matriz;
                                            //sw.Write(registroC460(matriz[i, 0], matriz[i, 1], matriz[i, 2], matriz[i, 3], matriz[i, 4], matriz[i, 5], matriz[i, 6], matriz[i, 7], matriz[i, 8]) + "\r\n");
                                            break;
                                        case "C470":
                                            blC470 = new string[countRows, countField];
                                            blC470 = matriz;
                                            //sw.Write(registroC470(matriz[i, 0], matriz[i, 1], matriz[i, 2], matriz[i, 3], matriz[i, 4], matriz[i, 5], matriz[i, 6], matriz[i, 7], matriz[i, 8], matriz[i, 9]) + "\r\n");
                                            break;
                                        case "C490":
                                            blC490 = new string[countRows, countField];
                                            blC490 = matriz;
                                            //sw.Write(registroC490(matriz[i, 0], matriz[i, 1], matriz[i, 2], matriz[i, 3], matriz[i, 4], matriz[i, 5], matriz[i, 6]) + "\r\n");
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (auxTipo[x])
                                    {
                                        case "G001":
                                            sw.Write(registroG001(movG.ToString()) + "\r\n");
                                            break;
                                        case "G990":
                                            //sw.Write(registroG990(matriz[i, 0]) + "\r\n");
                                            sw.Write(registroG990(countG990.ToString()) + "\r\n");
                                            break;
                                        case "D990":
                                            //sw.Write(registroD990(matriz[i, 0]) + "\r\n");
                                            sw.Write(registroD990(countD990.ToString()) + "\r\n");
                                            break;
                                        case "1990":
                                            sw.Write(registro1990(count1111.ToString()) + "\r\n");
                                            break;
                                        case "E990":
                                            //sw.Write(registroE990(matriz[i, 0]) + "\r\n");
                                            sw.Write(registroE990(countE990.ToString()) + "\r\n");
                                            break;
                                        case "H990":
                                            sw.Write(registroH990(countH990.ToString()) + "\r\n");
                                            break;
                                        case "9001":
                                            //sw.Write(registro9001(matriz[i, 0]) + "\r\n");
                                            sw.Write(registro9001(mov9.ToString()) + "\r\n");
                                            break;
                                        case "9900":
                                            if (BL9900 != null)
                                            {
                                                for (int r = 0; r < BL9900.GetLength(0); r++)
                                                {
                                                    if (BL9900[r, 0] != null && BL9900[r, 1] != null)
                                                    {
                                                        sw.Write(registro9900(BL9900[r, 0], BL9900[r, 1]) + "\r\n");
                                                    }
                                                }
                                                sw.Write(registro9900("9900", (count9900 + 1).ToString()) + "\r\n");
                                                sw.Write(registro9900("9990", 1.ToString()) + "\r\n");
                                                sw.Write(registro9900("9999", 1.ToString()) + "\r\n");
                                            }
                                            break;
                                        case "9990":
                                            sw.Write(registro9990((count9990 + 2).ToString()) + "\r\n");
                                            break;
                                        case "9999":
                                            sw.Write(registro9999((count9999).ToString()) + "\r\n");
                                            break;
                                    }
                                }
                            }
                            banco.fechaConexao();
                        }
                        if (auxTipo[x] == "0175")//Abertura dependencia
                        {
                            if (bl0150 != null && auxTipo[16] == "C100")
                            {
                                for (int w = 0; w < bl0150.GetLength(0); w++)
                                {
                                    if (valida150 == true)
                                    {
                                        sw.Write(registro0150(bl0150[w, 0], bl0150[w, 1], bl0150[w, 2], bl0150[w, 3], bl0150[w, 4], bl0150[w, 5], bl0150[w, 6], bl0150[w, 7], bl0150[w, 8], bl0150[w, 9], bl0150[w, 10], bl0150[w, 11]) + "\r\n");
                                    }
                                    if (w == 0 || BL9900[5, 1] == null)
                                    {
                                        BL9900[5, 1] = (1).ToString();
                                    }
                                    else
                                    {
                                        BL9900[5, 1] = (w + 1).ToString();
                                    }

                                    if (bl0175 != null)
                                    {
                                        for (int a = 0; a < bl0175.GetLength(0); a++)
                                        {
                                            if (bl0175[a, 0] != null)
                                            {
                                                if (bl0175[a, 3] == bl0150[w, 0])
                                                {
                                                    sw.Write(registro0175(bl0175[a, 0], bl0175[a, 1], bl0175[a, 2]) + "\r\n");
                                                    BL9900[6, 0] = "0175";
                                                    if (a == 0 || BL9900[6, 1] == null)
                                                    {
                                                        BL9900[6, 1] = (1).ToString();
                                                    }
                                                    else
                                                    {
                                                        BL9900[6, 1] = (a + 1).ToString();
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                valida150 = false;
                            }
                        }
                        else if (bl0150 != null && auxTipo[6] == "#")
                        {
                            for (int w = 0; w < bl0150.GetLength(0); w++)
                            {
                                sw.Write(registro0150(bl0150[w, 0], bl0150[w, 1], bl0150[w, 2], bl0150[w, 3], bl0150[w, 4], bl0150[w, 5], bl0150[w, 6], bl0150[w, 7], bl0150[w, 8], bl0150[w, 9], bl0150[w, 10], bl0150[w, 11]) + "\r\n");
                            }
                            bl0150 = null;
                        }
                        else if ((auxTipo[x] == "0200" && bl0200 != null) && auxTipo[9] == "#")
                        {
                            for (int w = 0; w < bl0200.GetLength(0); w++)
                            {
                                sw.Write(registro0200(bl0200[w, 0], bl0200[w, 1].Trim(), bl0200[w, 2].Trim(), bl0200[w, 3], bl0200[w, 4], bl0200[w, 5], bl0200[w, 6], bl0200[w, 7], bl0200[w, 8], bl0200[w, 9], bl0200[w, 10]) + "\r\n");
                            }
                        }
                        else if (auxTipo[x] == "0205")//Abertura dependencia
                        {
                            if (bl0200 != null)
                            {
                                for (int w = 0; w < bl0200.GetLength(0); w++)
                                {
                                    sw.Write(registro0200(bl0200[w, 0], bl0200[w, 1].Trim(), bl0200[w, 2].Trim(), bl0200[w, 3], bl0200[w, 4], bl0200[w, 5], bl0200[w, 6], bl0200[w, 7], bl0200[w, 8], bl0200[w, 9], bl0200[w, 10]) + "\r\n");
                                    if (BL9900[8, 1] == null)
                                    {
                                        BL9900[8, 0] = "0200";

                                        if (w == 0)
                                        {
                                            BL9900[8, 1] = (1).ToString();
                                        }
                                        else
                                        {
                                            BL9900[8, 1] = (w).ToString();
                                        }
                                    }
                                    else
                                    {
                                        BL9900[8, 1] = (w + 1).ToString();
                                    }
                                    if (bl0205 != null)
                                    {
                                        for (int a = 0; a < bl0205.GetLength(0); a++)
                                        {
                                            if (bl0205[a, 0] != null)
                                            {
                                                if (bl0205[a, 4] == bl0200[w, 0] && bl0200[w, 11] == bl0205[a, 5])
                                                {
                                                    sw.Write(registro0205(bl0205[a, 0], bl0205[a, 1], bl0205[a, 2], bl0205[a, 3]) + "\r\n");
                                                    BL9900[9, 0] = "0205";
                                                    if (BL9900[9, 1] == null)
                                                    {
                                                        if (a == 0)
                                                        {
                                                            BL9900[9, 1] = (1).ToString();
                                                        }
                                                        else
                                                        {
                                                            BL9900[9, 1] = (a).ToString();
                                                        }
                                                    }
                                                    else
                                                    {
                                                        BL9900[9, 1] = (a + 1).ToString();
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                            if (auxTipo[x] == "C321")//Abertura dependencia C300
                            {
                                if (blC300 != null)
                                {
                                    for (int c300 = 0; c300 < blC300.GetLength(0); c300++)
                                    {
                                        sw.Write(registroC300(blC300[c300, 0], blC300[c300, 1], blC300[c300, 2], blC300[c300, 3], blC300[c300, 4], blC300[c300, 5], blC300[c300, 6], blC300[c300, 7], blC300[c300, 8], blC300[c300, 9]) + "\r\n");
                                        if (BL9900[34, 0] == null)
                                        {
                                            BL9900[34, 0] = "C300";
                                            if (c300 == 0)
                                            {
                                                BL9900[34, 1] = (1).ToString();
                                            }
                                            else
                                            {
                                                BL9900[34, 1] = (c300).ToString();
                                            }
                                        }
                                        else
                                        {
                                            BL9900[34, 1] = (c300 + 1).ToString();
                                        }

                                        if (blC320 != null)
                                        {
                                            //Primeira dependencia
                                            for (int c320 = 0; c320 < blC320.GetLength(0); c320++)
                                            {
                                                if (blC320[c320, 0] != null)
                                                {
                                                    if (BL9900[35, 0] == null)
                                                    {
                                                        BL9900[35, 0] = "C320";
                                                        if (c320 == 0)
                                                        {
                                                            BL9900[35, 1] = (1).ToString();
                                                        }
                                                        else
                                                        {
                                                            BL9900[35, 1] = (c320).ToString();
                                                        }
                                                    }
                                                    else
                                                    {
                                                        BL9900[35, 1] = (c320 + 1).ToString();
                                                    }
                                                    sw.Write(registroC320(blC320[c320, 0], blC320[c320, 1], blC320[c320, 2], blC320[c320, 3], blC320[c320, 4], blC320[c320, 5], blC320[c320, 6], blC320[c320, 7]) + "\r\n");

                                                    //New Trecho
                                                    if (blC321 != null)
                                                    {
                                                        //Segunda dependencia
                                                        for (int c321 = 0; c321 < blC321.GetLength(0); c321++)
                                                        {
                                                            if (blC321[c321, 0] != null)
                                                            {
                                                                if (blC320[c320, 9] == blC321[c321, 10])
                                                                {
                                                                    if (BL9900[36, 0] == null)
                                                                    {
                                                                        BL9900[36, 0] = "C321";
                                                                        if (c321 == 0)
                                                                        {
                                                                            BL9900[36, 1] = (1).ToString();
                                                                        }
                                                                        else
                                                                        {
                                                                            BL9900[36, 1] = (c321).ToString();
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        BL9900[36, 1] = (c321 + 1).ToString();
                                                                    }
                                                                    sw.Write(registroC321(blC321[c321, 0], blC321[c321, 1], blC321[c321, 2], blC321[c321, 3], blC321[c321, 4], blC321[c321, 5], blC321[c321, 6], blC321[c321, 7], blC321[c321, 8]) + "\r\n");
                                                                }
                                                            }
                                                        }
                                                    }
                                                    //End Trecho

                                                }
                                            }
                                        }

                                        /*if (blC321 != null)
                                        {
                                            //Segunda dependencia
                                            for (int c321 = 0; c321 < blC321.GetLength(0); c321++)
                                            {
                                                if (blC321[c321, 0] != null)
                                                {
                                                    if (BL9900[36, 0] == null)
                                                    {
                                                        BL9900[36, 0] = "C321";
                                                        if (c321 == 0)
                                                        {
                                                            BL9900[36, 1] = (1).ToString();
                                                        }
                                                        else
                                                        {
                                                            BL9900[36, 1] = (c321).ToString();
                                                        }
                                                    }
                                                    else
                                                    {
                                                        BL9900[36, 1] = (c321 + 1).ToString();
                                                    }
                                                    sw.Write(registroC321(blC321[c321, 0], blC321[c321, 1], blC321[c321, 2], blC321[c321, 3], blC321[c321, 4], blC321[c321, 5], blC321[c321, 6], blC321[c321, 7], blC321[c321, 8]) + "\r\n");
                                                }
                                            }
                                        }*/
                                    }
                                }
                            } if (auxTipo[x] == "C390")//Abertura dependencia C300 Perfil A
                        {
                            if (blC350 != null)
                            {
                                for (int c350 = 0; c350 < blC350.GetLength(0); c350++)
                                {
                                    sw.Write(registroC350(blC350[c350, 0], blC350[c350, 1], blC350[c350, 2], blC350[c350, 3], blC350[c350, 4], blC350[c350, 5], blC350[c350, 6], blC350[c350, 7], blC350[c350, 8], blC350[c350, 9], blC350[c350, 10]) + "\r\n");
                                    if (BL9900[37, 0] == null)
                                    {
                                        BL9900[37, 0] = "C350";
                                        if (c350 == 0)
                                        {
                                            BL9900[37, 1] = (1).ToString();
                                        }
                                        else
                                        {
                                            BL9900[37, 1] = (c350).ToString();
                                        }
                                    }
                                    else
                                    {
                                        BL9900[37, 1] = (c350 + 1).ToString();
                                    }

                                    if (blC370 != null)
                                    {
                                        //Primeira dependencia
                                        for (int c370 = 0; c370 < blC370.GetLength(0); c370++)
                                        {
                                            if (blC350[c350, 2] == blC370[c370, 6] && blC350[c350, 3] == blC370[c370, 7])//Compara o numero da notaD
                                            {
                                                if (blC370[c370, 0] != null)
                                                {
                                                    if (BL9900[38, 0] == null)
                                                    {
                                                        BL9900[38, 0] = "C370";
                                                        if (c370 == 0)
                                                        {
                                                            BL9900[38, 1] = (1).ToString();
                                                        }
                                                        else
                                                        {
                                                            BL9900[38, 1] = (c370).ToString();
                                                        }
                                                    }
                                                    else
                                                    {
                                                        BL9900[38, 1] = (c370 + 1).ToString();
                                                    }
                                                    sw.Write(registroC370((c370 + 1).ToString(), blC370[c370, 1], blC370[c370, 2], blC370[c370, 3], blC370[c370, 4], blC370[c370, 5]) + "\r\n");

                                                    if (blC390 != null)
                                                    {
                                                        //Segunda dependencia
                                                        for (int c390 = 0; c390 < blC390.GetLength(0); c390++)
                                                        {
                                                            if (blC390[c390, 9] == blC370[c370, 6] && blC390[c390, 8] == blC370[c370, 7])//Compara o numero da notaD
                                                            {
                                                                if (blC390[c390, 0] != null)
                                                                {
                                                                    if (BL9900[39, 0] == null)
                                                                    {
                                                                        BL9900[39, 0] = "C390";
                                                                        if (c390 == 0)
                                                                        {
                                                                            BL9900[39, 1] = (1).ToString();
                                                                        }
                                                                        else
                                                                        {
                                                                            BL9900[39, 1] = (c390).ToString();
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        BL9900[39, 1] = (c390 + 1).ToString();
                                                                    }
                                                                    sw.Write(registroC390(blC390[c390, 0], blC390[c390, 1], blC390[c390, 2], blC390[c390, 3], blC390[c390, 4], blC390[c390, 5], blC390[c390, 6], blC390[c390, 7]) + "\r\n");
                                                                }
                                                            }
                                                        }
                                                    }
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }

                                /*Oldif (blC370 != null)
                                {
                                    //Primeira dependencia
                                    for (int c370 = 0; c370 < blC370.GetLength(0); c370++)
                                    {
                                        if (blC370[c370, 0] != null)
                                        {
                                            if (BL9900[38, 0] == null)
                                            {
                                                BL9900[38, 0] = "C370";
                                                if (c370 == 0)
                                                {
                                                    BL9900[38, 1] = (1).ToString();
                                                }
                                                else
                                                {
                                                    BL9900[38, 1] = (c370).ToString();
                                                }
                                            }
                                            else
                                            {
                                                BL9900[38, 1] = (c370 + 1).ToString();
                                            }
                                            sw.Write(registroC370((c370 + 1).ToString(), blC370[c370, 1], blC370[c370, 2], blC370[c370, 3], blC370[c370, 4], blC370[c370, 5]) + "\r\n");
                                        }
                                    }
                                }
                                if (blC390 != null)
                                {
                                    //Segunda dependencia
                                    for (int c390 = 0; c390 < blC390.GetLength(0); c390++)
                                    {
                                            if (BL9900[39, 0] == null)
                                            {
                                                BL9900[39, 0] = "C390";
                                                if (c390 == 0)
                                                {
                                                    BL9900[39, 1] = (1).ToString();
                                                }
                                                else
                                                {
                                                    BL9900[39, 1] = (c390).ToString();
                                                }
                                            }
                                            else
                                            {
                                                BL9900[39, 1] = (c390 + 1).ToString();
                                            }
                                            sw.Write(registroC390(blC390[c390, 0], blC390[c390, 1], blC390[c390, 2], blC390[c390, 3], blC390[c390, 4], blC390[c390, 5], blC390[c390, 6], blC390[c390, 7]) + "\r\n");
                                    }
                                }*/
                            }
                        }
                        else if (auxTipo[x] == "C190")//Abertura dependencia C100
                        {
                            if (blC100 != null)
                            {
                                int u = 0;
                                for (int w = 0; w < blC100.GetLength(0); w++)
                                {
                                    sw.Write(registroC100(blC100[w, 1], blC100[w, 2], blC100[w, 3], blC100[w, 4], blC100[w, 5], blC100[w, 6], blC100[w, 7], blC100[w, 8], blC100[w, 9], blC100[w, 10], blC100[w, 11], blC100[w, 12], blC100[w, 13], blC100[w, 14], blC100[w, 15], blC100[w, 16], blC100[w, 17], blC100[w, 18], blC100[w, 19], blC100[w, 20], blC100[w, 21], blC100[w, 22], blC100[w, 23], blC100[w, 24], blC100[w, 25], blC100[w, 26], blC100[w, 27], blC100[w, 28]) + "\r\n");
                                    if (BL9900[16, 1] == null)
                                    {
                                        BL9900[16, 0] = "C100";
                                        if (w == 0)
                                        {
                                            BL9900[16, 1] = (1).ToString();
                                        }
                                        else
                                        {
                                            BL9900[16, 1] = (w).ToString();
                                        }
                                    }
                                    else
                                    {
                                        BL9900[16, 1] = (w + 1).ToString();
                                    }
                                    if (blC170 != null)
                                    {
                                        //Primeira dependencia
                                        for (int a = 0; a < blC170.GetLength(0); a++)
                                        {
                                            if (blC170[a, 0] != null)
                                            {
                                                if (blC170[a, 36] == blC100[w, 7])
                                                {
                                                    BL9900[29, 0] = "C170";
                                                    if (BL9900[29, 1] == null)
                                                    {
                                                        if (a == 0)
                                                        {
                                                            BL9900[29, 1] = (1).ToString();
                                                        }
                                                        else
                                                        {
                                                            BL9900[29, 1] = (a).ToString();
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (w == 0)
                                                        {
                                                            BL9900[29, 1] = (a + 1).ToString();
                                                        }
                                                        else
                                                        {
                                                            BL9900[29, 1] = (Convert.ToInt32(BL9900[29, 1]) + 1).ToString();
                                                        }
                                                    }
                                                    sw.Write(registroC170(blC170[a, 0], blC170[a, 1], blC170[a, 2], blC170[a, 3], blC170[a, 4], blC170[a, 5], blC170[a, 6], blC170[a, 7], blC170[a, 8], blC170[a, 9], blC170[a, 10], blC170[a, 11], blC170[a, 12], blC170[a, 13], blC170[a, 14], blC170[a, 15], blC170[a, 16], blC170[a, 17], blC170[a, 18], blC170[a, 19], blC170[a, 20], blC170[a, 21], blC170[a, 22], blC170[a, 23], blC170[a, 24], blC170[a, 25], blC170[a, 26], blC170[a, 27], blC170[a, 28], blC170[a, 29], blC170[a, 30], blC170[a, 31], blC170[a, 32], blC170[a, 33], blC170[a, 34], blC170[a, 35]) + "\r\n");
                                                }
                                            }
                                        }
                                    }

                                    //Segunda dependencia
                                    if (blC190
                                        != null)
                                    {
                                        for (int b = 0; b < blC190.GetLength(0); b++)
                                        {
                                            if (blC190[b, 0] != null)
                                            {
                                                if (blC190[b, 11] == blC100[w, 7])
                                                {
                                                    BL9900[31, 0] = "C190";
                                                    if (BL9900[31, 1] == null)
                                                    {
                                                        if (u == 0)
                                                        {
                                                            BL9900[31, 1] = (1).ToString();
                                                        }
                                                        else
                                                        {
                                                            BL9900[31, 1] = (u).ToString();
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (w == 0)
                                                        {
                                                            BL9900[31, 1] = (u + 1).ToString();
                                                        }
                                                        else
                                                        {
                                                            BL9900[31, 1] = (Convert.ToInt32(BL9900[31, 1]) + 1).ToString();
                                                        }
                                                    }
                                                    sw.Write(registroC190(blC190[b, 0], blC190[b, 1], blC190[b, 2], blC190[b, 3], blC190[b, 4], blC190[b, 5], blC190[b, 6], blC190[b, 7], blC190[b, 8], blC190[b, 9], blC190[b, 10]) + "\r\n");
                                                    u++;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if (auxTipo[x] == "C490")//Abertura dependencia
                        {
                            if (blC400 != null)
                            {
                                for (int w = 0; w < blC400.GetLength(0); w++)
                                {
                                    sw.Write(registroC400(blC400[w, 0], blC400[w, 1], blC400[w, 2], blC400[w, 3]) + "\r\n");
                                    if (BL9900[40, 1] == null)
                                    {
                                        BL9900[40, 0] = "C400";
                                        if (w == 0)
                                        {
                                            BL9900[40, 1] = (1).ToString();
                                        }
                                        else
                                        {
                                            BL9900[40, 1] = (w).ToString();
                                        }
                                    }
                                    else
                                    {
                                        BL9900[40, 1] = (w + 1).ToString();
                                    }
                                    if (blC405 != null)
                                    {
                                        int c405 = 0;
                                        //Primeira dependencia
                                        for (int a = 0; a < blC405.GetLength(0); a++)
                                        {
                                            if (blC405[a, 0] != null)
                                            {
                                                if (blC405[a, 6] == blC400[w, 2])//Compara o numero de serie
                                                {
                                                    sw.Write(registroC405(blC405[a, 0], blC405[a, 1], blC405[a, 2], blC405[a, 3], blC405[a, 4], blC405[a, 5]) + "\r\n");
                                                    if (BL9900[41, 1] == null)
                                                    {
                                                        BL9900[41, 0] = "C405";
                                                        if (a == 0)
                                                        {
                                                            c405 = 1;
                                                            BL9900[41, 1] = (1).ToString();
                                                        }
                                                        else
                                                        {
                                                            c405++;
                                                            BL9900[41, 1] = (c405).ToString();
                                                        }
                                                    }
                                                    else
                                                    {
                                                        BL9900[41, 1] = (Convert.ToInt32(BL9900[41, 1]) + 1).ToString();
                                                        //BL9900[41, 1] = (a + 1).ToString();
                                                    }
                                                    //Segunda dependencia
                                                    if (blC420 != null)
                                                    {
                                                        int c420 = 0;
                                                        int c425 = 0;
                                                        for (int b = 0; b < blC420.GetLength(0); b++)
                                                        {
                                                            if (blC420[b, 0] != null)
                                                            {
                                                                if (blC420[b, 5] == blC405[a, 0])
                                                                {
                                                                    sw.Write(registroC420(blC420[b, 0], blC420[b, 1], blC420[b, 2], blC420[b, 3]) + "\r\n");
                                                                    if (BL9900[43, 1] == null)
                                                                    {
                                                                        BL9900[43, 0] = "C420";
                                                                        if (b == 0)
                                                                        {
                                                                            c420 = 1;
                                                                            BL9900[43, 1] = (1).ToString();
                                                                        }
                                                                        else
                                                                        {
                                                                            c420++;
                                                                            BL9900[43, 1] = (c420).ToString();
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        if (a == 0)
                                                                        {
                                                                            c420++;
                                                                            BL9900[43, 1] = (c420).ToString();
                                                                        }
                                                                        else
                                                                        {
                                                                            BL9900[43, 1] = (Convert.ToInt32(BL9900[43, 1]) + 1).ToString();
                                                                            c420 = Convert.ToInt32(BL9900[43, 1]);
                                                                        }
                                                                    }
                                                                    //Terceira dependencia
                                                                    if (blC425 != null)
                                                                    {
                                                                        for (int y = 0; y < blC425.GetLength(0); y++)
                                                                        {
                                                                            if (blC425[y, 0] != null)
                                                                            {
                                                                                if ((blC420[b, 0] == blC425[y, 7]) && (blC420[b, 5] == blC425[y, 8]))
                                                                                {
                                                                                    sw.Write(registroC425(blC425[y, 0], blC425[y, 1], blC425[y, 2], blC425[y, 3], blC425[y, 4], blC425[y, 5]) + "\r\n");
                                                                                    if (BL9900[44, 1] == null)
                                                                                    {
                                                                                        BL9900[44, 0] = "C425";
                                                                                        if (c425 == 0)
                                                                                        {
                                                                                            c425 = 1;
                                                                                            BL9900[44, 1] = (1).ToString();
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            c425++;
                                                                                            BL9900[44, 1] = (c425).ToString();
                                                                                        }
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        if (a == 0)
                                                                                        {
                                                                                            c425++;
                                                                                            BL9900[44, 1] = (c425).ToString();
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            BL9900[44, 1] = (Convert.ToInt32(BL9900[44, 1]) + 1).ToString();
                                                                                            c425 = Convert.ToInt32(BL9900[44, 1]);
                                                                                        }

                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }//End 420 e 425

                                                        //Begin 460 e 470
                                                        if (blC460 != null)
                                                        {
                                                            int c460 = 0;
                                                            int c470 = 0;
                                                            for (int p = 0; p < blC460.GetLength(0); p++)
                                                            {
                                                                if (blC460[p, 0] != null)
                                                                {
                                                                    if (blC460[p, 3] == blC405[a, 0])
                                                                    {
                                                                        sw.Write(registroC460(blC460[p, 0], blC460[p, 1], blC460[p, 2], blC460[p, 3], blC460[p, 4], blC460[p, 5], blC460[p, 6], blC460[p, 7], blC460[p, 8]) + "\r\n");
                                                                        if (BL9900[45, 1] == null)
                                                                        {
                                                                            BL9900[45, 0] = "C460";
                                                                            if (p == 0)
                                                                            {
                                                                                c460 = 1;
                                                                                BL9900[45, 1] = (1).ToString();
                                                                            }
                                                                            else
                                                                            {
                                                                                c460++;
                                                                                BL9900[45, 1] = (c460).ToString();
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            if (a == 0)
                                                                            {
                                                                                c460++;
                                                                                BL9900[45, 1] = (c460).ToString();
                                                                            }
                                                                            else
                                                                            {
                                                                                BL9900[45, 1] = (Convert.ToInt32(BL9900[45, 1]) + 1).ToString();
                                                                                c460 = Convert.ToInt32(BL9900[45, 1]);
                                                                            }
                                                                        }
                                                                        if (blC470 != null)
                                                                        {

                                                                            for (int q = 0; q < blC470.GetLength(0); q++)
                                                                            {
                                                                                if (blC470[q, 11] == blC405[a, 0])
                                                                                {
                                                                                    if (blC470[q, 0] != null)
                                                                                    {
                                                                                        if ((blC460[p, 2] == blC470[q, 10]) && (blC460[p, 3] == blC470[q, 11]))
                                                                                        {
                                                                                            sw.Write(registroC470(blC470[q, 0], blC470[q, 1], blC470[q, 2], blC470[q, 3], blC470[q, 4], blC470[q, 5], blC470[q, 6], blC470[q, 7], blC470[q, 8], blC470[q, 9]) + "\r\n");

                                                                                            if (BL9900[47, 1] == null)
                                                                                            {
                                                                                                BL9900[47, 0] = "C470";
                                                                                                if (q == 0)
                                                                                                {
                                                                                                    c470 = 1;
                                                                                                    BL9900[47, 1] = (1).ToString();
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    c470++;
                                                                                                    BL9900[47, 1] = (c470).ToString();
                                                                                                }
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                if (a == 0)
                                                                                                {
                                                                                                    c470++;
                                                                                                    BL9900[47, 1] = (c470).ToString();
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    BL9900[47, 1] = (Convert.ToInt32(BL9900[47, 1]) + 1).ToString();
                                                                                                    c470 = Convert.ToInt32(BL9900[47, 1]);
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }//End 420 e 425
                                                        }
                                                        //Quarta dependencia
                                                        if (blC490 != null)
                                                        {
                                                            for (int z = 0; z < blC490.GetLength(0); z++)
                                                            {
                                                                if (blC490[z, 0] != null)
                                                                {
                                                                    if ((blC400[w, 2] == blC490[z, 8]) && (blC405[a, 0] == blC490[z, 7]))
                                                                    {
                                                                        BL9900[49, 0] = "C490";
                                                                        if (BL9900[49, 1] == null)
                                                                        {
                                                                            if (w == 0)
                                                                            {
                                                                                BL9900[49, 1] = (1).ToString();
                                                                            }
                                                                            else
                                                                            {
                                                                                BL9900[49, 1] = (z).ToString();
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            if (z == 0)
                                                                            {
                                                                                BL9900[49, 1] = (z + 1).ToString();
                                                                            }
                                                                            else
                                                                            {
                                                                                BL9900[49, 1] = (Convert.ToInt32(BL9900[49, 1]) + 1).ToString();
                                                                            }
                                                                        }
                                                                        sw.Write(registroC490(blC490[z, 0], blC490[z, 1], blC490[z, 2], blC490[z, 3], blC490[z, 4], blC490[z, 5], blC490[z, 6]) + "\r\n");
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if (auxTipo[x] == "0150")//Abertura dependencia 
                        {
                            
                            if ((bl0150 != null && auxTipo[16] == "#") || (bl0150 != null && auxTipo[45] == "C460"))//Suprir dependencia do Cupom
                            {
                                for (int w = 0; w < bl0150.GetLength(0); w++)
                                {
                                    if (valida150 == true)
                                    {
                                        sw.Write(registro0150(bl0150[w, 0], bl0150[w, 1], bl0150[w, 2], bl0150[w, 3], bl0150[w, 4], bl0150[w, 5], bl0150[w, 6], bl0150[w, 7], bl0150[w, 8], bl0150[w, 9], bl0150[w, 10], bl0150[w, 11]) + "\r\n");
                                    }
                                    if (w == 0 || BL9900[5, 1] == null)
                                    {
                                        BL9900[5, 1] = (1).ToString();
                                    }
                                    else
                                    {
                                        BL9900[5, 1] = (w + 1).ToString();
                                    }

                                    if (bl0175 != null)
                                    {
                                        for (int a = 0; a < bl0175.GetLength(0); a++)
                                        {
                                            if (bl0175[a, 0] != null)
                                            {
                                                if (bl0175[a, 3] == bl0150[w, 0])
                                                {
                                                    sw.Write(registro0175(bl0175[a, 0], bl0175[a, 1], bl0175[a, 2]) + "\r\n");
                                                    BL9900[6, 0] = "0175";
                                                    if (a == 0 || BL9900[6, 1] == null)
                                                    {
                                                        BL9900[6, 1] = (1).ToString();
                                                    }
                                                    else
                                                    {
                                                        BL9900[6, 1] = (a + 1).ToString();
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                valida150 = false;
                            }
                        }
                        else if (auxTipo[x] == "0205")//Abertura dependencia
                        {
                            if ((bl0200 != null && auxTipo[16] == "#") && (bl0200 != null && auxTipo[45] == "C460"))//Suprir dependencia do somente cupom
                            {
                                for (int w = 0; w < bl0200.GetLength(0); w++)
                                {
                                    sw.Write(registro0200(bl0200[w, 0], bl0200[w, 1], bl0200[w, 2], bl0200[w, 3], bl0200[w, 4], bl0200[w, 5], bl0200[w, 6], bl0200[w, 7], bl0200[w, 8], bl0200[w, 9], bl0200[w, 10]) + "\r\n");
                                    if (BL9900[8, 1] == null)
                                    {
                                        BL9900[8, 0] = "0200";

                                        if (w == 0)
                                        {
                                            BL9900[8, 1] = (1).ToString();
                                        }
                                        else
                                        {
                                            BL9900[8, 1] = (w).ToString();
                                        }
                                    }
                                    else
                                    {
                                        BL9900[8, 1] = (w + 1).ToString();
                                    }
                                    if (bl0205 != null)
                                    {
                                        for (int a = 0; a < bl0205.GetLength(0); a++)
                                        {
                                            if (bl0205[a, 0] != null)
                                            {
                                                if (bl0205[a, 4] == bl0200[w, 0] && bl0200[w, 11] == bl0205[a, 5])
                                                {
                                                    sw.Write(registro0205(bl0205[a, 0], bl0205[a, 1], bl0205[a, 2], bl0205[a, 3]) + "\r\n");
                                                    BL9900[9, 0] = "0205";
                                                    if (BL9900[9, 1] == null)
                                                    {
                                                        if (a == 0)
                                                        {
                                                            BL9900[9, 1] = (1).ToString();
                                                        }
                                                        else
                                                        {
                                                            BL9900[9, 1] = (a).ToString();
                                                        }
                                                    }
                                                    else
                                                    {
                                                        BL9900[9, 1] = (a + 1).ToString();
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }//End If
                    if (auxTipo[x] != "#" &&
                        auxTipo[x] != "0175" &&
                        (auxTipo[x] != "0205") &&
                        auxTipo[x] != "C490" && auxTipo[x] != "C190" &&
                        auxTipo[x] != "C170" && auxTipo[x] != "C420" &&
                        auxTipo[x] != "C475" && auxTipo[x] != "C425" &&
                        auxTipo[x] != "C460" && auxTipo[x] != "C405" && auxTipo[x] != "C470")
                    {
                        BL9900[x, 0] = auxTipo[x];
                        BL9900[x, 1] = countRows.ToString();
                    }
                } //End For
                sw.Close();
            }//End if;
            return retorno;
        }//End Metodo
        /* SPED OLD
        public Boolean geraSpedFiscal(Boolean valida, string caminho, string[] auxTipo, string di, string df, string store, string finalidade, string pessoa, string dh, string perfil, string atividade, int mov0, int movC, int movD, int movE, int movF, int movH, int mov1, int mov9, int movG, string sp_di,string sp_df)
        {
            Boolean retorno = valida;
            
            if (File.Exists(caminho))
            {

                File.Delete(caminho);

                StreamWriter sw = new StreamWriter(caminho, true, Encoding.ASCII);

                //################################################################Instrução Banco
                auxConsistencia = 0;
                countField = 0;
                countRows = 0;
                for (int x = 0; x < auxTipo.Length; x++)
                {
                    auxConsistencia = 0;
                    
                    if (auxTipo[x] != "#")
                    {

                        try
                        {
                            auxConsistencia = 0;
                            banco.abreConexao();
                            banco.startTransaction("conectorPDV_EFD_008");
                            banco.addParametro("tipo", auxTipo[x]);
                            banco.addParametro("find_loja", store);
                            banco.addParametro("find_pessoa", pessoa);
                            banco.addParametro("di", di);
                            banco.addParametro("df", df);
                            banco.addParametro("dateInv", dh);
                            banco.addParametro("finalidade", finalidade);
                            banco.addParametro("perfil", perfil.Replace("\n","").Replace("\0","").Replace("\r","").Trim());
                            banco.addParametro("atividade", atividade);
                            banco.addParametro("call_creditos", call_creditos.ToString());
                            banco.addParametro("call_debitos_nf", call_debitos_nf.ToString());
                            banco.addParametro("call_debitos_cp", call_debitos_cp.ToString());
                            banco.procedimentoSet();
                        }
                        catch (Exception erro) {

                            switch (auxTipo[x])
                            {
                                case "G001":
                                    sw.Write(registroG001(movG.ToString()) + "\r\n");
                                    countRows = 1;
                                    break;
                                case "G990":
                                    //sw.Write(registroG990(matriz[i, 0]) + "\r\n");
                                    sw.Write(registroG990(countG990.ToString()) + "\r\n");
                                    countRows = 1;
                                    break;
                                case "D990":
                                    //sw.Write(registroD990(matriz[i, 0]) + "\r\n");
                                    sw.Write(registroD990(countD990.ToString()) + "\r\n");
                                    countRows = 1;
                                    break;
                                case "1990":
                                    sw.Write(registro1990((count1111).ToString()) + "\r\n");
                                    countRows = 1;
                                    break;
                                case "C370":
                                    countRows = 0;
                                    countField = 0;
                                    break;
                                case "C321":
                                    countRows = 0;
                                    countField = 0;
                                    break;
                                case "C320":
                                    countRows = 0;
                                    countField = 0;
                                    break;
                                case "C390":
                                    countRows = 0;
                                    countField = 0;
                                    break;
                                case "C350":
                                    countRows = 0;
                                    countField = 0;
                                    break;
                                case "E100":
                                    sw.Write(registroE100(sp_di, sp_df) + "\r\n");
                                    countRows = 1;
                                    break;
                                case "E990":
                                    //sw.Write(registroE990(matriz[i, 0]) + "\r\n");
                                    sw.Write(registroE990(countE990.ToString()) + "\r\n");
                                    countRows = 1;
                                    break;
                                case "H990":
                                    sw.Write(registroH990(countH990.ToString()) + "\r\n");
                                    countRows = 1;
                                    break;
                                case "9001":
                                    //sw.Write(registro9001(matriz[i, 0]) + "\r\n");
                                    sw.Write(registro9001(mov9.ToString()) + "\r\n");
                                    countRows = 1;
                                    break;
                                case "9900":
                                    if (BL9900 != null)
                                    {
                                        for (int r = 0; r < BL9900.GetLength(0); r++)
                                        {
                                            if (BL9900[r, 0] != null && BL9900[r, 1] != null)
                                            {
                                                sw.Write(registro9900(BL9900[r, 0], BL9900[r, 1]) + "\r\n");
                                            }
                                        }
                                        sw.Write(registro9900("9900", (count9900 + 2).ToString()) + "\r\n");
                                        sw.Write(registro9900("9990", 1.ToString()) + "\r\n");
                                        sw.Write(registro9900("9999", 1.ToString()) + "\r\n");
                                    }
                                    break;
                                case "9990":
                                    sw.Write(registro9990((count9990 + 2).ToString()) + "\r\n");
                                    break;
                                case "9999":
                                    sw.Write(registro9999((count9999).ToString()) + "\r\n");
                                    break;
                            }

                            auxConsistencia = 1;   
                        }
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
                                        
                                        switch (auxTipo[x])//Preenchimento Unico
                                        {
                                            case "0000":
                                                sw.Write(registro0000(matriz[i, 0], matriz[i, 1], matriz[i, 2], matriz[i, 3], matriz[i, 4], matriz[i, 5], matriz[i, 6], matriz[i, 7], matriz[i, 8], matriz[i, 9], matriz[i, 10], matriz[i, 11], matriz[i, 12], matriz[i, 13]) + "\r\n");
                                                break;
                                            case "0001":
                                                sw.Write(registro0001(mov0.ToString()) + "\r\n");
                                                break;
                                            case "0005":
                                                sw.Write(registro0005(matriz[i, 0], matriz[i, 1], matriz[i, 2], matriz[i, 3], matriz[i, 4], matriz[i, 5], matriz[i, 6], matriz[i, 7], matriz[i, 8]) + "\r\n");
                                                break;
                                            case "0100":
                                                sw.Write(registro0100(matriz[i, 0], matriz[i, 1], matriz[i, 2], matriz[i, 3], matriz[i, 4], matriz[i, 5], matriz[i, 6], matriz[i, 7], matriz[i, 8], matriz[i, 9], matriz[i, 10], matriz[i, 11], matriz[i, 12]) + "\r\n");
                                                break;
                                            case "0190":
                                                sw.Write(registro0190(matriz[i, 0], matriz[i, 1]) + "\r\n");
                                                break;
                                            case "0400":
                                                sw.Write(registro0400(matriz[i, 0], matriz[i, 1]) + "\r\n");
                                                break;
                                            case "0450":
                                                sw.Write(registro0450(matriz[i, 0], matriz[i, 1]) + "\r\n");
                                                break;
                                            case "0990":
                                                //sw.Write(registro0990(matriz[i, 0]) + "\r\n");
                                                sw.Write(registro0990(count0990.ToString()) + "\r\n");
                                                break;
                                            case "C001":
                                                sw.Write(registroC001(movC.ToString()) + "\r\n");
                                                break;
                                            case "C105":
                                                sw.Write(registroC105(matriz[i, 0], matriz[i, 1]) + "\r\n");
                                                break;
                                            case "C195":
                                                sw.Write(registroC195(matriz[i, 0], matriz[i, 1]) + "\r\n");
                                                break;
                                            case "C320":
                                                sw.Write(registroC320(matriz[i, 0], matriz[i, 1], matriz[i, 2], matriz[i, 3], matriz[i, 4], matriz[i, 5], matriz[i, 6], matriz[i, 7]) + "\r\n");
                                                break;
                                            case "C321":
                                                sw.Write(registroC370(matriz[i, 0], matriz[i, 1], matriz[i, 2], matriz[i, 3], matriz[i, 4], matriz[i, 5]) + "\r\n");
                                                break;
                                            case "C350":
                                                sw.Write(registroC350(matriz[i, 0], matriz[i, 1], matriz[i, 2], matriz[i, 3], matriz[i, 4], matriz[i, 5], matriz[i, 6], matriz[i, 7], matriz[i, 8], matriz[i, 9], matriz[i, 10]) + "\r\n");
                                                break;
                                            case "C370":
                                                sw.Write(registroC370(matriz[i, 0], matriz[i, 1], matriz[i, 2], matriz[i, 3], matriz[i, 4], matriz[i, 5]) + "\r\n");
                                                break;
                                            case "C390":
                                                sw.Write(registroC390(matriz[i, 0], matriz[i, 1], matriz[i, 2], matriz[i, 3]) + "\r\n");
                                                break;
                                            case "C990":
                                                //sw.Write(registroC990(matriz[i, 0]) + "\r\n");
                                                sw.Write(registroC990(countC990.ToString()) + "\r\n");
                                                break;
                                            case "D001":
                                                sw.Write(registroD001(movD.ToString()) + "\r\n");
                                                break;
                                            case "D100":
                                                sw.Write(registroD100(matriz[i, 0],matriz[i, 1],matriz[i, 2],matriz[i, 3],matriz[i, 4],matriz[i, 5],matriz[i, 6],matriz[i, 7],matriz[i, 8],matriz[i, 9],matriz[i, 10],matriz[i, 11],matriz[i, 12],matriz[i, 13],matriz[i, 14],matriz[i, 15],matriz[i, 16],matriz[i, 17],matriz[i, 18],matriz[i, 19],matriz[i, 20],matriz[i, 21]) + "\r\n");
                                                break;
                                            case "D190":
                                                sw.Write(registroD190(matriz[i, 0], matriz[i, 1], matriz[i, 2], matriz[i, 3], matriz[i, 4], matriz[i, 5], matriz[i, 6], matriz[i, 7]) + "\r\n");
                                                break;
                                            case "D990":
                                                //sw.Write(registroD990(matriz[i, 0]) + "\r\n");
                                                sw.Write(registroD990(countD990.ToString()) + "\r\n");
                                                break;
                                            case "E001":
                                                sw.Write(registroE001(movE.ToString()) + "\r\n");
                                                break;
                                            case "E100":
                                                sw.Write(registroE100(sp_di,sp_df) + "\r\n");
                                                break;
                                            case "E110":
                                                sw.Write(registroE110(matriz[i, 0], matriz[i, 1], matriz[i, 2], matriz[i, 3], matriz[i, 4], matriz[i, 5], matriz[i, 6], matriz[i, 7], matriz[i, 8], matriz[i, 9], matriz[i, 10], matriz[i, 11], matriz[i, 12], matriz[i, 13]) + "\r\n");
                                                break;
                                            case "E116":
                                                sw.Write(registroE116(matriz[i, 0], matriz[i, 1], matriz[i, 2], matriz[i, 3], matriz[i, 4], matriz[i, 5], matriz[i, 6], matriz[i, 7], matriz[i, 8]) + "\r\n");
                                                break;
                                            case "E990":
                                                //sw.Write(registroE990(matriz[i, 0]) + "\r\n");
                                                sw.Write(countE990.ToString() + "\r\n");
                                                break;
                                            case "G001":
                                                sw.Write(registroG001(movG.ToString()) + "\r\n");
                                                break;
                                            case "G990":
                                                //sw.Write(registroG990(matriz[i, 0]) + "\r\n");
                                                sw.Write(registroG990(countG990.ToString()) + "\r\n");
                                                break;
                                            case "H001":
                                                sw.Write(registroH001(movH.ToString()) + "\r\n");
                                                break;
                                            case "H005":
                                                sw.Write(registroH005(matriz[i, 0], matriz[i, 1], matriz[i, 2]) + "\r\n");
                                                break;
                                            case "H010":
                                                sw.Write(registroH010(matriz[i, 0], matriz[i, 1], matriz[i, 2], matriz[i, 3], matriz[i, 4], matriz[i, 5], matriz[i, 6], matriz[i, 7], matriz[i, 8]) + "\r\n");
                                                break;
                                            case "H990":
                                                sw.Write(registroH990(countH990.ToString()) + "\r\n");
                                                break;
                                            case "1001":
                                                //sw.Write(registro1001(matriz[i, 0]) + "\r\n");
                                                sw.Write(registro1001(mov1.ToString()) + "\r\n");
                                                break;
                                            case "1010":
                                                if (matriz[i, 6] == "S")
                                                {
                                                    vetor[77] = "1600";
                                                }
                                                sw.Write(registro1010(matriz[i, 0], matriz[i, 1], matriz[i, 2], matriz[i, 3], matriz[i, 4], matriz[i, 5], matriz[i, 6], matriz[i, 7], matriz[i, 8]) + "\r\n");
                                                break;
                                            case "1600":
                                                sw.Write(registro1600(matriz[i, 0], matriz[i, 1], matriz[i, 2]) + "\r\n");
                                                break;
                                            case "1990":
                                                sw.Write(registro1990(count1111.ToString()) + "\r\n");
                                                break;
                                            case "9001":
                                                //sw.Write(registro9001(matriz[i, 0]) + "\r\n");
                                                sw.Write(registro9001(mov9.ToString()) + "\r\n");
                                                break;
                                            case "9900":
                                                if (BL9900 != null)
                                                {
                                                    for (int r = 0; r < BL9900.GetLength(0); r++)
                                                    {
                                                        if (BL9900[r, 0] != null && BL9900[r, 1] != null)
                                                        {
                                                            sw.Write(registro9900(BL9900[r, 0], BL9900[r, 1]) + "\r\n");
                                                        }
                                                    }
                                                    sw.Write(registro9900("9900", (count9900 + 1).ToString()) + "\r\n");
                                                    sw.Write(registro9900("9990", 1.ToString()) + "\r\n"); 
                                                    sw.Write(registro9900("9999", 1.ToString()) + "\r\n"); 
                                                }
                                                break;
                                            case "9990":
                                                sw.Write(registro9990((count9990 + 2).ToString()) + "\r\n");
                                                break;
                                            case "9999":
                                                sw.Write(registro9999((count9999).ToString()) + "\r\n");
                                                break;
                                        }
                                    }
                                    switch (auxTipo[x])//Preenchimento MultiDimencional
                                    {
                                        case "0150":
                                            bl0150 = new string[countRows, countField];
                                            bl0150 = matriz;
                                            //i = count;//termino
                                            //sw.Write(registro0150(matriz[i, 0], matriz[i, 1], matriz[i, 2], matriz[i, 3], matriz[i, 4], matriz[i, 5], matriz[i, 6], matriz[i, 7], matriz[i, 8], matriz[i, 9], matriz[i, 10], matriz[i, 11]) + "\r\n");
                                            break;
                                        case "0175":
                                            bl0175 = new string[countRows, countField];
                                            bl0175 = matriz;
                                            //i = count;//termino
                                            //sw.Write(registro0175(matriz[i, 0], matriz[i, 1], matriz[i, 2]) + "\r\n");
                                            break;
                                        case "0200":
                                            bl0200 = new string[countRows, countField];
                                            bl0200 = matriz;
                                            //i = count;//termino
                                            //sw.Write(registro0200(matriz[i, 0], matriz[i, 1], matriz[i, 2], matriz[i, 3], matriz[i, 4], matriz[i, 5], matriz[i, 6], matriz[i, 7], matriz[i, 8], matriz[i, 9], matriz[i, 10]) + "\r\n");
                                            break;
                                        case "0205":
                                            bl0205 = new string[countRows, countField];
                                            bl0205 = matriz;
                                            //i = count;//termino
                                            //sw.Write(registro0205(matriz[i, 0], matriz[i, 1], matriz[i, 2], matriz[i, 3]) + "\r\n");
                                            break;
                                        case "C100":
                                            blC100 = new string[countRows, countField];
                                            blC100 = matriz;
                                            //sw.Write(registroC100(matriz[i, 0], matriz[i, 1], matriz[i, 2], matriz[i, 3], matriz[i, 4], matriz[i, 5], matriz[i, 6], matriz[i, 7], matriz[i, 8], matriz[i, 9], matriz[i, 10], matriz[i, 11], matriz[i, 12], matriz[i, 13], matriz[i, 14], matriz[i, 15], matriz[i, 16], matriz[i, 17], matriz[i, 18], matriz[i, 19], matriz[i, 20], matriz[i, 21], matriz[i, 22], matriz[i, 23], matriz[i, 24], matriz[i, 25], matriz[i, 26], matriz[i, 27]) + "\r\n");
                                            break;
                                        case "C170":
                                            blC170 = new string[countRows, countField];
                                            blC170 = matriz;
                                            //sw.Write(registroC170(matriz[i, 0], matriz[i, 1], matriz[i, 2], matriz[i, 3], matriz[i, 4], matriz[i, 5], matriz[i, 6], matriz[i, 7], matriz[i, 8], matriz[i, 9], matriz[i, 10], matriz[i, 11], matriz[i, 12], matriz[i, 13], matriz[i, 14], matriz[i, 15], matriz[i, 16], matriz[i, 17], matriz[i, 18], matriz[i, 19], matriz[i, 20], matriz[i, 21], matriz[i, 22], matriz[i, 23], matriz[i, 24], matriz[i, 25], matriz[i, 26], matriz[i, 27], matriz[i, 28], matriz[i, 29], matriz[i, 30], matriz[i, 31], matriz[i, 32], matriz[i, 33], matriz[i, 34], matriz[i, 35]) + "\r\n");
                                            break;
                                        case "C190":// "C190", "C195", "C300", "C310", "C320", "C321", "C350", "C370", "C390", "C400", "C405", "C410", "C420", "C425", "C460", "C465", "C470", "C490", "C495", "C800", "C850", "C860", "C890", "C990", "D001", "D100", "D190", "D195", "D990", "E001", "E100", "E110", "E116", "E990", "G001", "G110", "G125", "G130", "G140", "G990", "H001", "H005", "H010", "H020", "H990", "1001", "1010", "1600", "1990", "9001", "9990"
                                            blC190 = new string[countRows, countField];
                                            blC190 = matriz;
                                            //sw.Write(registroC190(matriz[i, 0], matriz[i, 1], matriz[i, 2], matriz[i, 3], matriz[i, 4], matriz[i, 5], matriz[i, 6], matriz[i, 7], matriz[i, 8], matriz[i, 9], matriz[i, 10]) + "\r\n");
                                            break;
                                        case "C400":
                                            blC400 = new string[countRows, countField];
                                            blC400 = matriz;
                                            //sw.Write(registroC400(matriz[i, 0], matriz[i, 1], matriz[i, 2], matriz[i, 3]) + "\r\n");
                                            break;
                                        case "C405":
                                            blC405 = new string[countRows, countField];
                                            blC405 = matriz;
                                            //sw.Write(registroC405(matriz[i, 0], matriz[i, 1], matriz[i, 2]) + "\r\n");
                                            break;
                                        case "C420":
                                            blC420 = new string[countRows, countField];
                                            blC420 = matriz;
                                            //sw.Write(registroC420(matriz[i, 0], matriz[i, 1], matriz[i, 2], matriz[i, 3]) + "\r\n");
                                            break;
                                        case "C425":
                                            blC425 = new string[countRows, countField];
                                            blC425 = matriz;
                                            //sw.Write(registroC425(matriz[i, 0], matriz[i, 1], matriz[i, 2], matriz[i, 3], matriz[i, 4], matriz[i, 5]) + "\r\n");
                                            break;
                                        case "C460":
                                            blC460 = new string[countRows, countField];
                                            blC460 = matriz;
                                            //sw.Write(registroC460(matriz[i, 0], matriz[i, 1], matriz[i, 2], matriz[i, 3], matriz[i, 4], matriz[i, 5], matriz[i, 6], matriz[i, 7], matriz[i, 8]) + "\r\n");
                                            break;
                                        case "C470":
                                            blC470 = new string[countRows, countField];
                                            blC470 = matriz;
                                            //sw.Write(registroC470(matriz[i, 0], matriz[i, 1], matriz[i, 2], matriz[i, 3], matriz[i, 4], matriz[i, 5], matriz[i, 6], matriz[i, 7], matriz[i, 8], matriz[i, 9]) + "\r\n");
                                            break;
                                        case "C490":
                                            blC490 = new string[countRows, countField];
                                            blC490 = matriz;
                                            //sw.Write(registroC490(matriz[i, 0], matriz[i, 1], matriz[i, 2], matriz[i, 3], matriz[i, 4], matriz[i, 5], matriz[i, 6]) + "\r\n");
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (auxTipo[x])
                                    {
                                        case "G001":
                                            sw.Write(registroG001(movG.ToString()) + "\r\n");
                                            break;
                                        case "G990":
                                            //sw.Write(registroG990(matriz[i, 0]) + "\r\n");
                                            sw.Write(registroG990(countG990.ToString()) + "\r\n");
                                            break;
                                        case "D990":
                                            //sw.Write(registroD990(matriz[i, 0]) + "\r\n");
                                            sw.Write(registroD990(countD990.ToString()) + "\r\n");
                                            break;
                                        case "1990":
                                            sw.Write(registro1990(count1111.ToString()) + "\r\n");
                                            break;
                                        case "E990":
                                            //sw.Write(registroE990(matriz[i, 0]) + "\r\n");
                                            sw.Write(registroE990(countE990.ToString()) + "\r\n");
                                            break;
                                        case "H990":
                                            sw.Write(registroH990(countH990.ToString()) + "\r\n");
                                            break;
                                        case "9001":
                                            //sw.Write(registro9001(matriz[i, 0]) + "\r\n");
                                            sw.Write(registro9001(mov9.ToString()) + "\r\n");
                                            break;
                                        case "9900":
                                            if (BL9900 != null)
                                            {
                                                for (int r = 0; r < BL9900.GetLength(0); r++)
                                                {
                                                    if (BL9900[r, 0] != null && BL9900[r, 1] != null)
                                                    {
                                                        sw.Write(registro9900(BL9900[r, 0], BL9900[r, 1]) + "\r\n");
                                                    }
                                                }
                                                sw.Write(registro9900("9900", (count9900 + 1).ToString()) + "\r\n");
                                                 sw.Write(registro9900("9990", 1.ToString()) + "\r\n"); 
                                                sw.Write(registro9900("9999", 1.ToString()) + "\r\n"); 
                                            }
                                            break;
                                        case "9990":
                                            sw.Write(registro9990((count9990 + 2).ToString()) + "\r\n");
                                            break;
                                        case "9999":
                                            sw.Write(registro9999((count9999).ToString()) + "\r\n");
                                            break;
                                    }
                                }
                            }
                            banco.fechaConexao();
                        }
                        if (auxTipo[x] == "0175")//Abertura dependencia
                        {
                            if (bl0150 != null && auxTipo[16] == "C100")
                            {
                                for (int w = 0; w < bl0150.GetLength(0); w++)
                                {
                                    if (valida150 == true)
                                    {
                                        sw.Write(registro0150(bl0150[w, 0], bl0150[w, 1], bl0150[w, 2], bl0150[w, 3], bl0150[w, 4], bl0150[w, 5], bl0150[w, 6], bl0150[w, 7], bl0150[w, 8], bl0150[w, 9], bl0150[w, 10], bl0150[w, 11]) + "\r\n");
                                    }
                                   if (w == 0 || BL9900[5, 1] == null)
                                   {
                                       BL9900[5, 1] = (1).ToString();
                                   }
                                   else
                                   {
                                       BL9900[5, 1] = (w+1).ToString();
                                   }
                                   
                                   if (bl0175 != null)
                                   {
                                       for (int a = 0; a < bl0175.GetLength(0); a++)
                                       {
                                           if (bl0175[a, 0] != null)
                                           {
                                               if (bl0175[a, 3] == bl0150[w, 0])
                                               {
                                                   sw.Write(registro0175(bl0175[a, 0], bl0175[a, 1], bl0175[a, 2]) + "\r\n");
                                                   BL9900[6, 0] = "0175";
                                                   if (a == 0 || BL9900[6, 1] == null)
                                                   {
                                                       BL9900[6, 1] = (1).ToString();
                                                   }
                                                   else
                                                   {
                                                       BL9900[6, 1] = (a+1).ToString();
                                                   }
                                               }
                                           }
                                       }
                                   }
                                }
                                valida150 = false;
                            }
                        }
                        else if (auxTipo[x] == "0205")//Abertura dependencia
                        {
                            if(bl0200 != null)
                            {
                                for (int w = 0; w < bl0200.GetLength(0); w++)
                                {
                                  sw.Write(registro0200(bl0200[w, 0], bl0200[w, 1], bl0200[w, 2], bl0200[w, 3], bl0200[w, 4], bl0200[w, 5], bl0200[w, 6], bl0200[w, 7], bl0200[w, 8], bl0200[w, 9], bl0200[w, 10]) + "\r\n");
                                  if (BL9900[8, 1] == null)
                                  {
                                      BL9900[8, 0] = "0200";

                                      if (w == 0)
                                      {
                                          BL9900[8, 1] = (1).ToString();
                                      }
                                      else
                                      {
                                          BL9900[8, 1] = (w).ToString();
                                      }
                                  }
                                  else
                                  {
                                      BL9900[8, 1] = (w + 1).ToString();
                                  }
                                  if (bl0205 != null)
                                  {
                                      for (int a = 0; a < bl0205.GetLength(0); a++)
                                      {
                                          if (bl0205[a, 0] != null)
                                          {
                                              if (bl0205[a, 4] == bl0200[w, 0] && bl0200[w,11] == bl0205[a,5])
                                              {
                                                  sw.Write(registro0205(bl0205[a, 0], bl0205[a, 1], bl0205[a, 2], bl0205[a, 3]) + "\r\n");
                                                  BL9900[9, 0] = "0205";
                                                  if (BL9900[9, 1] == null)
                                                  {
                                                      if (a == 0)
                                                      {
                                                          BL9900[9, 1] = (1).ToString();
                                                      }
                                                      else
                                                      {
                                                          BL9900[9, 1] = (a).ToString();
                                                      }
                                                  }
                                                  else
                                                  {
                                                      BL9900[9, 1] = (a + 1).ToString();
                                                  }
                                              }
                                          }
                                      }
                                  }
                                }
                            }
                        }else if (auxTipo[x] == "C190")//Abertura dependencia
                        {
                            if (blC100 != null)
                            {
                                int u = 0;
                                conector_full_variable alwaysVariables = new conector_full_variable();
                                for (int w = 0; w < blC100.GetLength(0); w++)
                                {
                                    sw.Write(registroC100(blC100[w, 1], blC100[w, 2], blC100[w, 3], blC100[w, 4], blC100[w, 5], blC100[w, 6], blC100[w, 7], blC100[w, 8], blC100[w, 9], blC100[w, 10], blC100[w, 11], blC100[w, 12], blC100[w, 13], blC100[w, 14], blC100[w, 15], blC100[w, 16], blC100[w, 17], blC100[w, 18], blC100[w, 19], blC100[w, 20], blC100[w, 21], blC100[w, 22], blC100[w, 23], blC100[w, 24], blC100[w, 25], blC100[w, 26], blC100[w, 27], blC100[w, 28]) + "\r\n");
                                    if (BL9900[16, 1] == null)
                                    {
                                        BL9900[16, 0] = "C100";
                                        if (w == 0)
                                        {
                                            BL9900[16, 1] = (1).ToString();
                                        }
                                        else
                                        {
                                            BL9900[16, 1] = (w).ToString();
                                        }
                                    }
                                    else
                                    {
                                        BL9900[16, 1] = (w + 1).ToString();
                                    }
                                     if (blC170 != null)
                                     {                                                                                 
                                         //Primeira dependencia
                                         for (int a = 0; a < blC170.GetLength(0); a++)
                                         {
                                             if (blC170[a, 0] != null)
                                             {
                                                 if (blC170[a, 36] == blC100[w, 7])
                                                 {
                                                     BL9900[29, 0] = "C170";
                                                     if (BL9900[29, 1] == null)
                                                     {
                                                         if (a == 0)
                                                         {
                                                             BL9900[29, 1] = (1).ToString();
                                                         }
                                                         else
                                                         {
                                                             BL9900[29, 1] = (a).ToString();
                                                         }
                                                     }
                                                     else
                                                     {
                                                         if (w == 0)
                                                         {
                                                             BL9900[29, 1] = (a + 1).ToString();
                                                         }
                                                         else
                                                         {
                                                             BL9900[29, 1] = (Convert.ToInt32(BL9900[29, 1]) + 1).ToString();
                                                         }
                                                     }
                                                     if ((blC100[w, 4] != "65" && blC100[w, 4] != "55"))
                                                     {
                                                         sw.Write(registroC170(blC170[a, 0], blC170[a, 1], blC170[a, 2], blC170[a, 3], blC170[a, 4], blC170[a, 5], blC170[a, 6], blC170[a, 7], blC170[a, 8], blC170[a, 9], blC170[a, 10], blC170[a, 11], blC170[a, 12], blC170[a, 13], blC170[a, 14], blC170[a, 15], blC170[a, 16], blC170[a, 17], blC170[a, 18], blC170[a, 19], blC170[a, 20], blC170[a, 21], blC170[a, 22], blC170[a, 23], blC170[a, 24], blC170[a, 25], blC170[a, 26], blC170[a, 27], blC170[a, 28], blC170[a, 29], blC170[a, 30], blC170[a, 31], blC170[a, 32], blC170[a, 33], blC170[a, 34], blC170[a, 35]) + "\r\n");
                                                     }
                                                 }
                                             }
                                         }
                                     }

                                    //Segunda dependencia
                                     if (blC190 != null)
                                     {
                                         for (int b = 0; b < blC190.GetLength(0); b++)
                                         {
                                             if (blC190[b, 0] != null)
                                             {
                                                 if (blC190[b, 11] == blC100[w, 7])
                                                 {
                                                     BL9900[31, 0] = "C190";
                                                     if (BL9900[31, 1] == null)
                                                     {
                                                         if (u == 0)
                                                         {
                                                             BL9900[31, 1] = (1).ToString();
                                                         }
                                                         else
                                                         {
                                                             BL9900[31, 1] = (u).ToString();
                                                         }
                                                     }
                                                     else
                                                     {
                                                         if (w == 0)
                                                         {
                                                             BL9900[31, 1] = (u + 1).ToString();
                                                         }
                                                         else
                                                         {
                                                             BL9900[31, 1] = (Convert.ToInt32(BL9900[31, 1]) + 1).ToString();
                                                         }
                                                     }
                                                     sw.Write(registroC190(blC190[b, 0], blC190[b, 1], blC190[b, 2], blC190[b, 3], blC190[b, 4], blC190[b, 5], blC190[b, 6], blC190[b, 7], blC190[b, 8], blC190[b, 9], blC190[b, 10]) + "\r\n");
                                                     u++;
                                                 }
                                             }
                                         }
                                     }
                                }
                            }
                        }else if (auxTipo[x] == "C490")//Abertura dependencia
                        {
                            if (blC400 != null)
                            {
                                for (int w = 0; w < blC400.GetLength(0); w++)
                                {
                                    sw.Write(registroC400(blC400[w, 0], blC400[w, 1], blC400[w, 2], blC400[w, 3]) + "\r\n");
                                    if (BL9900[40, 1] == null)
                                    {
                                        BL9900[40, 0] = "C400";
                                        if (w == 0)
                                        {
                                            BL9900[40, 1] = (1).ToString();
                                        }
                                        else
                                        {
                                            BL9900[40, 1] = (w).ToString();
                                        }
                                    }
                                    else
                                    {
                                        BL9900[40, 1] = (w + 1).ToString();
                                    }
                                    if (blC405 != null)
                                    {
                                        int c405 = 0;
                                        //Primeira dependencia
                                        for (int a = 0; a < blC405.GetLength(0); a++)
                                        {
                                            if (blC405[a, 0] != null)
                                            {
                                                if (blC405[a, 6] == blC400[w, 2])//Compara o numero de serie
                                                {
                                                    sw.Write(registroC405(blC405[a, 0], blC405[a, 1], blC405[a, 2], blC405[a, 3], blC405[a, 4], blC405[a, 5]) + "\r\n");
                                                    if (BL9900[41, 1] == null)
                                                    {
                                                        BL9900[41, 0] = "C405";
                                                        if (a == 0)
                                                        {
                                                            c405 = 1;
                                                            BL9900[41, 1] = (1).ToString();
                                                        }
                                                        else
                                                        {
                                                            c405++;
                                                            BL9900[41, 1] = (c405).ToString();
                                                        }
                                                    }
                                                    else
                                                    {
                                                        BL9900[41, 1] = (Convert.ToInt32(BL9900[41, 1]) + 1).ToString();
                                                        //BL9900[41, 1] = (a + 1).ToString();
                                                    }
                                                    //Segunda dependencia
                                                    if (blC420 != null)
                                                    {
                                                        int c420 = 0;
                                                        int c425 = 0;
                                                        for (int b = 0; b < blC420.GetLength(0); b++)
                                                        {
                                                            if (blC420[b, 0] != null)
                                                            {
                                                                if (blC420[b, 5] == blC405[a, 0])
                                                                {
                                                                    sw.Write(registroC420(blC420[b, 0], blC420[b, 1], blC420[b, 2], blC420[b, 3]) + "\r\n");
                                                                    if (BL9900[43, 1] == null)
                                                                    {
                                                                        BL9900[43, 0] = "C420";
                                                                        if (b == 0)
                                                                        {
                                                                            c420 = 1;
                                                                            BL9900[43, 1] = (1).ToString();
                                                                        }
                                                                        else
                                                                        {
                                                                            c420++;
                                                                            BL9900[43, 1] = (c420).ToString();
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        if (a == 0)
                                                                        {
                                                                            c420++;
                                                                            BL9900[43, 1] = (c420).ToString();
                                                                        }
                                                                        else
                                                                        {
                                                                            BL9900[43, 1] =(Convert.ToInt32(BL9900[43, 1]) + 1).ToString();
                                                                            c420 = Convert.ToInt32(BL9900[43, 1]);
                                                                        }
                                                                    }
                                                                    //Terceira dependencia
                                                                    if (blC425 != null)
                                                                    {
                                                                        for (int y = 0; y < blC425.GetLength(0); y++)
                                                                        {
                                                                            if (blC425[y, 0] != null)
                                                                            {
                                                                                if ((blC420[b, 0] == blC425[y, 7]) && (blC420[b, 5] == blC425[y, 8]))
                                                                                {
                                                                                    sw.Write(registroC425(blC425[y, 0], blC425[y, 1], blC425[y, 2], blC425[y, 3], blC425[y, 4], blC425[y, 5]) + "\r\n");
                                                                                    if (BL9900[44, 1] == null)
                                                                                    {
                                                                                        BL9900[44, 0] = "C425";
                                                                                        if (c425 == 0)
                                                                                        {
                                                                                            c425 = 1;
                                                                                            BL9900[44, 1] = (1).ToString();
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            c425++;
                                                                                            BL9900[44, 1] = (c425).ToString();
                                                                                        }
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        if (a == 0)
                                                                                        {
                                                                                            c425++;
                                                                                            BL9900[44, 1] = (c425).ToString();
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            BL9900[44, 1] = (Convert.ToInt32(BL9900[44, 1]) + 1).ToString();
                                                                                            c425 = Convert.ToInt32(BL9900[44, 1]);
                                                                                        }

                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }//End 420 e 425

                                                        //Begin 460 e 470
                                                                        if (blC460 != null)
                                                                        {
                                                                            int c460 = 0;
                                                                            int c470 = 0;
                                                                            for (int p = 0; p < blC460.GetLength(0); p++)
                                                                            {
                                                                                if (blC460[p, 0] != null)
                                                                                {
                                                                                    if (blC460[p, 3] == blC405[a, 0])
                                                                                    {
                                                                                        sw.Write(registroC460(blC460[p, 0], blC460[p, 1], blC460[p, 2], blC460[p, 3], blC460[p, 4], blC460[p, 5], blC460[p, 6], blC460[p, 7], blC460[p, 8]) + "\r\n");
                                                                                        if (BL9900[45, 1] == null)
                                                                                        {
                                                                                            BL9900[45, 0] = "C460";
                                                                                            if (p == 0)
                                                                                            {
                                                                                                c460 = 1;
                                                                                                BL9900[45, 1] = (1).ToString();
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                c460++;
                                                                                                BL9900[45, 1] = (c460).ToString();
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            if (a == 0)
                                                                                            {
                                                                                                c460++;
                                                                                                BL9900[45, 1] = (c460).ToString();
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                BL9900[45, 1] = (Convert.ToInt32(BL9900[45, 1]) + 1).ToString();
                                                                                                c460 = Convert.ToInt32(BL9900[45, 1]);
                                                                                            }
                                                                                        }
                                                                                        if (blC470 != null)
                                                                                        {
                                                                                            
                                                                                                for (int q = 0; q < blC470.GetLength(0); q++)
                                                                                                {
                                                                                                    if (blC470[q, 11] == blC405[a, 0])
                                                                                                    {
                                                                                                        if (blC470[q, 0] != null)
                                                                                                        {
                                                                                                            if ((blC460[p, 2] == blC470[q, 10]) && (blC460[p, 3] == blC470[q, 11]))
                                                                                                            {
                                                                                                                sw.Write(registroC470(blC470[q, 0], blC470[q, 1], blC470[q, 2], blC470[q, 3], blC470[q, 4], blC470[q, 5], blC470[q, 6], blC470[q, 7], blC470[q, 8], blC470[q, 9]) + "\r\n");

                                                                                                                if (BL9900[47, 1] == null)
                                                                                                                {
                                                                                                                    BL9900[47,0] = "C470";
                                                                                                                    if (q == 0)
                                                                                                                    {
                                                                                                                        c470 = 1;
                                                                                                                        BL9900[47, 1] = (1).ToString();
                                                                                                                    }
                                                                                                                    else
                                                                                                                    {
                                                                                                                        c470++;
                                                                                                                        BL9900[47, 1] = (c470).ToString();
                                                                                                                    }
                                                                                                                }
                                                                                                                else
                                                                                                                {
                                                                                                                    if (a == 0)
                                                                                                                    {
                                                                                                                        c470++;
                                                                                                                        BL9900[47, 1] = (c470).ToString();
                                                                                                                    }
                                                                                                                    else
                                                                                                                    {
                                                                                                                        BL9900[47, 1] = (Convert.ToInt32(BL9900[47, 1]) + 1).ToString();
                                                                                                                        c470 = Convert.ToInt32(BL9900[47, 1]);
                                                                                                                    }
                                                                                                                }
                                                                                                            }
                                                                                                        }
                                                                                                    }
                                                                                                }
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }//End 420 e 425
                                                                        }
                                                        //Quarta dependencia
                                                        if (blC490 != null)
                                                        {
                                                            for (int z = 0; z < blC490.GetLength(0); z++)
                                                            {
                                                                if (blC490[z, 0] != null)
                                                                {
                                                                    if ((blC400[w, 2] == blC490[z, 8]) && (blC405[a, 0] == blC490[z, 7]))
                                                                    {
                                                                        BL9900[49, 0] = "C490";
                                                                        if (BL9900[49, 1] == null)
                                                                        {
                                                                            if (w == 0)
                                                                            {
                                                                                BL9900[49, 1] = (1).ToString();
                                                                            }
                                                                            else
                                                                            {
                                                                                BL9900[49, 1] = (z).ToString();
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            if (z == 0)
                                                                            {
                                                                                BL9900[49, 1] = (z + 1).ToString();
                                                                            }
                                                                            else
                                                                            {
                                                                                BL9900[49, 1] = (Convert.ToInt32(BL9900[49, 1]) + 1).ToString();
                                                                            }
                                                                        }
                                                                        sw.Write(registroC490(blC490[z, 0], blC490[z, 1], blC490[z, 2], blC490[z, 3], blC490[z, 4], blC490[z, 5], blC490[z, 6]) + "\r\n");
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if (auxTipo[x] == "0150")//Abertura dependencia 
                        {
                            if ((bl0150 != null && auxTipo[16] == "#") || (bl0150 != null && auxTipo[45] == "C460"))//Suprir dependencia do Cupom
                            {
                                for (int w = 0; w < bl0150.GetLength(0); w++)
                                {
                                    if (valida150 == true)
                                    {
                                        sw.Write(registro0150(bl0150[w, 0], bl0150[w, 1], bl0150[w, 2], bl0150[w, 3], bl0150[w, 4], bl0150[w, 5], bl0150[w, 6], bl0150[w, 7], bl0150[w, 8], bl0150[w, 9], bl0150[w, 10], bl0150[w, 11]) + "\r\n");
                                    }
                                    if (w == 0 || BL9900[5, 1] == null)
                                    {
                                        BL9900[5, 1] = (1).ToString();
                                    }
                                    else
                                    {
                                        BL9900[5, 1] = (w + 1).ToString();
                                    }

                                    if (bl0175 != null)
                                    {
                                        for (int a = 0; a < bl0175.GetLength(0); a++)
                                        {
                                            if (bl0175[a, 0] != null)
                                            {
                                                if (bl0175[a, 3] == bl0150[w, 0])
                                                {
                                                    sw.Write(registro0175(bl0175[a, 0], bl0175[a, 1], bl0175[a, 2]) + "\r\n");
                                                    BL9900[6, 0] = "0175";
                                                    if (a == 0 || BL9900[6, 1] == null)
                                                    {
                                                        BL9900[6, 1] = (1).ToString();
                                                    }
                                                    else
                                                    {
                                                        BL9900[6, 1] = (a + 1).ToString();
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                valida150 = false;
                            }
                        }else if (auxTipo[x] == "0205")//Abertura dependencia
                        {
                            if ((bl0200 != null && auxTipo[16] == "#") && (bl0200 != null && auxTipo[45] == "C460"))//Suprir dependencia do somente cupom
                            {
                                for (int w = 0; w < bl0200.GetLength(0); w++)
                                {
                                  sw.Write(registro0200(bl0200[w, 0], bl0200[w, 1], bl0200[w, 2], bl0200[w, 3], bl0200[w, 4], bl0200[w, 5], bl0200[w, 6], bl0200[w, 7], bl0200[w, 8], bl0200[w, 9], bl0200[w, 10]) + "\r\n");
                                  if (BL9900[8, 1] == null)
                                  {
                                      BL9900[8, 0] = "0200";

                                      if (w == 0)
                                      {
                                          BL9900[8, 1] = (1).ToString();
                                      }
                                      else
                                      {
                                          BL9900[8, 1] = (w).ToString();
                                      }
                                  }
                                  else
                                  {
                                      BL9900[8, 1] = (w + 1).ToString();
                                  }
                                  if (bl0205 != null)
                                  {
                                      for (int a = 0; a < bl0205.GetLength(0); a++)
                                      {
                                          if (bl0205[a, 0] != null)
                                          {
                                              if (bl0205[a, 4] == bl0200[w, 0] && bl0200[w,11] == bl0205[a,5])
                                              {
                                                  sw.Write(registro0205(bl0205[a, 0], bl0205[a, 1], bl0205[a, 2], bl0205[a, 3]) + "\r\n");
                                                  BL9900[9, 0] = "0205";
                                                  if (BL9900[9, 1] == null)
                                                  {
                                                      if (a == 0)
                                                      {
                                                          BL9900[9, 1] = (1).ToString();
                                                      }
                                                      else
                                                      {
                                                          BL9900[9, 1] = (a).ToString();
                                                      }
                                                  }
                                                  else
                                                  {
                                                      BL9900[9, 1] = (a + 1).ToString();
                                                  }
                                              }
                                          }
                                      }
                                  }
                                }
                            }
                        }
                    }//End If
                    if (auxTipo[x] != "#" && auxTipo[x] != "0175" && (auxTipo[x] != "0205") && auxTipo[x] != "C490" && auxTipo[x] != "C190" && auxTipo[x] != "C170" && auxTipo[x] != "C420" && auxTipo[x] != "C475" && auxTipo[x] != "C425" && auxTipo[x] != "C460" && auxTipo[x] != "C405" && auxTipo[x] != "C470" && auxTipo[x] != "0200")
                    {
                       // BL9900[x, 0] = auxTipo[x];
                        BL9900[x, 1] = countRows.ToString();
                    }
                } //End For
                sw.Close();
            }//End if;

            BL9900 =  new string[84,2];
            bl0150 = null;
            bl0175 = null;
            bl0200 = null;
            bl0205 = null;
            blC100 = null;
            blC170 = null;
            blC190 = null;
            blC400 = null;
            blC405 = null;
            blC420 = null;
            blC425 = null;
            blC460 = null;
            blC470 = null;
            blC490 = null;
            return retorno;
        }//End Metodo
        SPED OLD*/
        //#########################################################END Funções SPED##########################################################
        //#########################################################Funções SPED##############################################################
        protected string registro0000(string COD_VER, string COD_FIN, string DT_INI, string DT_FIN, string NOME, string CNPJ, string CPF, string UF, string IE, string COD_MUN, string IM, string SUFRAMA, string IND_PERFIL, string IND_ATIV)
        {
            count0990++;
            count9999++;
            //return "|" + "0000" + "|" + COD_VER.PadLeft(3, '0') + "|" + COD_FIN.PadLeft(1, '0') + "|" + DT_INI.PadLeft(8, '0') + "|" + DT_FIN.PadLeft(8, '0') + "|" + NOME.PadRight(100, ' ') + "|" + CNPJ.PadLeft(14, '0') + "|" + CPF.PadLeft(11, '0') + "|" + UF.PadRight(2, ' ') + "|" + IE.PadRight(14, ' ') + "|" + COD_MUN.PadLeft(7, '0') + "|" + IM + "|" + SUFRAMA.PadRight(9, ' ') + "|" + IND_PERFIL.PadRight(1, ' ') + "|" + IND_ATIV.PadLeft(1, '0') + "|";
            return "|" + "0000" + "|" + COD_VER + "|" + COD_FIN + "|" + DT_INI.PadLeft(8, '0') + "|" + DT_FIN.PadLeft(8, '0') + "|" + NOME + "|" + CNPJ + "|" + CPF + "|" + UF + "|" + IE + "|" + COD_MUN + "|" + IM + "|" + SUFRAMA + "|" + IND_PERFIL + "|" + IND_ATIV + "|";
        }
        protected string registro0001(string IND_MOV)
        {
            count0990++;
            count9999++;
            return "|" + "0001" + "|" + IND_MOV + "|";
        }
        protected string registro0005(string FANTASIA, string CEP, string END, string NUM, string COMPL, string BAIRRO, string FONE, string FAX, string EMAIL)
        {
            count0990++;
            count9999++;
            return "|" + "0005" + "|" + FANTASIA + "|" + CEP.PadLeft(8, '0') + "|" + END + "|" + NUM + "|" + COMPL + "|" + BAIRRO + "|" + FONE + "|" + FAX + "|" + EMAIL + "|";
        }
        protected string registro0100(string NOME, string CPF, string CRC, string CNPJ, string CEP, string END, string NUM, string COMPL, string BAIRRO, string FONE, string FAX, string EMAIL, string COD_MUN)
        {
            count0990++;
            count9999++;
            return "|" + "0100" + "|" + NOME + "|" + CPF + "|" + CRC + "|" + CNPJ + "|" + CEP + "|" + END + "|" + NUM + "|" + COMPL + "|" + BAIRRO + "|" + FONE + "|" + FAX + "|" + EMAIL + "|" + COD_MUN + "|";
        }
        protected string registro0150(string COD_PART, string NOME, string COD_PAIS, string CNPJ, string CPF, string IE, string COD_MUN, string SUFRAMA, string END, string NUM, string COMPL, string BAIRRO)
        {
            count0990++;
            count9999++;
            return "|" + "0150" + "|" + COD_PART + "|" + NOME + "|" + COD_PAIS + "|" + CNPJ + "|" + CPF + "|" + IE + "|" + COD_MUN + "|" + SUFRAMA + "|" + END + "|" + NUM + "|" + COMPL + "|" + BAIRRO + "|";
        }
        protected string registro0175(string DT_ALT, string NR_CAMPO, string CONT_ANT)
        {
            count0990++;
            count9999++;
            return "|" + "0175" + "|" + DT_ALT.PadLeft(8, '0') + "|" + NR_CAMPO + "|" + CONT_ANT + "|";
        }
        protected string registro0190(string UNID, string DESCR)
        {
            count0990++;
            count9999++;
            return "|" + "0190" + "|" + UNID + "|" + DESCR + "|";
        }

        protected string registro0200(string COD_ITEM, string DESCR_ITEM, string COD_BARRA, string COD_ANT_ITEM, string UNID_INV, string TIPO_ITEM, string COD_NCM, string EX_IPI, string COD_GEN, string COD_LST, string ALIQ_ICMS)
        {
            count0990++;
            count9999++;
            //return "|" + "0200" + "|" + COD_ITEM.PadRight(60, ' ') + "|" + DESCR_ITEM + "|" + COD_BARRA + "|" + COD_ANT_ITEM.PadRight(60 , ' ') + "|" + UNID_INV.PadRight(6, ' ') + "|" + TIPO_ITEM.PadLeft(2, ' ') + "|" + COD_NCM.PadRight(8, ' ') + "|" + EX_IPI.PadRight(3, ' ') + "|" + COD_GEN.PadLeft(2, ' ') + "|" + COD_LST.PadLeft(8, '0') + "|" + ALIQ_ICMS.PadLeft(6,'0');
            return "|" + "0200" + "|" + COD_ITEM + "|" + DESCR_ITEM + "|" + COD_BARRA + "|" + COD_ANT_ITEM + "|" + UNID_INV + "|" + TIPO_ITEM + "|" + COD_NCM + "|" + EX_IPI + "|" + COD_GEN.PadLeft(2, '0') + "|" + COD_LST + "|" + ALIQ_ICMS.Replace(".",",") + "|";
        }
        protected string registro0205(string DESCR_ANT_ITEM, string DT_INI, string DT_FIM, string COD_ANT_ITEM)
        {
            count0990++;
            count9999++;
            return "|" + "0205" + "|" + DESCR_ANT_ITEM + "|" + DT_INI.PadLeft(8, '0') + "|" + DT_FIM.PadLeft(8, '0') + "|" + COD_ANT_ITEM + "|";
        }
        protected string registro0206(string COD_COMB)
        {
            count0990++;
            count9999++;
            return "|" + "0206" + "|" + COD_COMB + "|";
        }
        protected string registro0220(string UNID_CONV, string FAT_CONV)
        {
            count0990++;
            count9999++;
            return "|" + "0220" + "|" + UNID_CONV.PadRight(6,' ') + "|" + FAT_CONV;
        }
        protected string registro0400(string COD_NAT, string DESCR_NAT)
        {
            count0990++;
            count9999++;
           // return "|" + "0400" + "|" + COD_NAT.PadRight(10, ' ') + "|" + DESCR_NAT + "|";
            return "|" + "0400" + "|" + COD_NAT + "|" + DESCR_NAT + "|";
        }
        protected string registro0450(string COD_INF, string TXT)
        {
            count0990++;
            count9999++;
            //return "|" + "0450" + "|" + COD_INF.PadRight(6, ' ') + "|" + TXT + "|";
            return "|" + "0450" + "|" + COD_INF + "|" + TXT + "|";
        }
        protected string registro0460(string COD_OBS, string TXT)
        {
            count0990++;
            count9999++;
            return "|" + "0460" + "|" + COD_OBS.PadRight(6, ' ') + "|" + TXT + "|";
            return "|" + "0460" + "|" + COD_OBS + "|" + TXT + "|";
        }
        protected string registro0990(string QTD_LIN_0)
        {
            count0990++;
            count9999++;
            return "|" + "0990" + "|" + QTD_LIN_0 + "|";
        }
        protected string registroC001(string IND_MOV)
        {
            countC990++;
            count9999++;
            //return "|" + "C001" + "|" + IND_MOV.PadRight(1,' ') + "|";
            return "|" + "C001" + "|" + IND_MOV + "|";
        }
        protected string registroC100(string IND_OPER, string IND_EMIT, string COD_PART, string COD_MOD, string COD_SIT, string SER, string NUM_DOC, string CHV_NFE, string DT_DOC, string DT_E_S, string VL_DOC, string IND_PGTO, string VL_DESC, string VL_ABAT_NT, string VL_MERC, string IND_FRT, string VL_FRT, string VL_SEG, string VL_OUT_DA, string VL_BC_ICMS, string VL_ICMS, string VL_BC_ICMS_ST, string VL_ICMS_ST, string VL_IPI, string VL_PIS, string VL_COFINS, string VL_PIS_ST, string VL_COFINS_ST)
        {
            countC990++;
            count9999++;
            //return "|" + "C100" + "|" + IND_OPER.PadRight(1, ' ') + "|" + IND_EMIT.PadRight(1, ' ') + "|" + COD_PART.PadRight(100, ' ') + "|" + COD_MOD.PadRight(2, ' ') + "|" + COD_SIT.PadLeft(2, '0') + "|" + SER.PadRight(3, ' ') + "|" + NUM_DOC.PadLeft(9, '0') + "|" + CHV_NFE.PadLeft(44, '0') + "|" + DT_DOC.PadLeft(8, '0') + "|" + DT_E_S.PadLeft(8, '0') + "|" + VL_DOC + "|" + IND_PGTO.PadRight(1, ' ') + "|" + VL_DESC + "|" + VL_ABAT_NT + "|" + VL_MERC + "|" + IND_FRT.PadRight(1, ' ') + "|" + VL_FRT + "|" + VL_SEG + "|" + VL_OUT_DA + "|" + VL_BC_ICMS + "|" + VL_ICMS + "|" + VL_BC_ICMS_ST + "|" + VL_ICMS_ST + "|" + VL_IPI + "|" + VL_PIS + "|" + VL_COFINS + "|" + VL_PIS_ST + "|" + VL_COFINS_ST + "|";
            return "|" + "C100" + "|" + IND_OPER + "|" + IND_EMIT + "|" + COD_PART + "|" + COD_MOD + "|" + COD_SIT + "|" + SER + "|" + NUM_DOC + "|" + CHV_NFE + "|" + DT_DOC + "|" + DT_E_S + "|" + VL_DOC.Replace(".", ",") + "|" + IND_PGTO.PadRight(1, ' ') + "|" + VL_DESC.Replace(".", ",") + "|" + VL_ABAT_NT.Replace(".", ",") + "|" + VL_MERC.Replace(".", ",") + "|" + IND_FRT.PadRight(1, ' ') + "|" + VL_FRT.Replace(".", ",") + "|" + VL_SEG.Replace(".", ",") + "|" + VL_OUT_DA.Replace(".", ",") + "|" + VL_BC_ICMS.Replace(".", ",") + "|" + VL_ICMS.Replace(".", ",") + "|" + VL_BC_ICMS_ST.Replace(".", ",") + "|" + VL_ICMS_ST.Replace(".", ",") + "|" + VL_IPI.Replace(".", ",") + "|" + VL_PIS.Replace(".", ",") + "|" + VL_COFINS.Replace(".", ",") + "|" + VL_PIS_ST.Replace(".", ",") + "|" + VL_COFINS_ST.Replace(".", ",") + "|";
        }
        protected string registroC105(string OPER, string UF)
        {
            countC990++;
            count9999++;
            //return "|" + "C105" + "|" + OPER.PadRight(1, ' ') + UF.PadRight(2,' ') + "|";
            return "|" + "C105" + "|" + OPER + UF + "|";
        }
        protected string registroC110(string COD_INF, string TXT_COMPL)
        {
            countC990++;
            count9999++;
            //return "|" + "C110" + "|" + COD_INF.PadRight(6, ' ') + "|" + TXT_COMPL + "|";
            return "|" + "C110" + "|" + COD_INF + "|" + TXT_COMPL + "|";
        }
        protected string registroC111(string NUM_PROC, string IND_PROC)
        {
            countC990++;
            count9999++;
            //return "|" + "C111" + "|" + NUM_PROC.PadRight(15, ' ') + "|" + IND_PROC.PadRight(1, ' ') + "|";
            return "|" + "C111" + "|" + NUM_PROC + "|" + IND_PROC + "|";
        }
        protected string registroC112(string COD_DA, string UF, string NUM_DA, string COD_AUT, string VL_DA, string DT_VCTO, string DT_PGTO)
        {
            countC990++;
            count9999++;
            //return "|" + "C112" + "|" + COD_DA.PadRight(1, ' ') + "|" + UF.PadRight(2, ' ') + "|" + NUM_DA + "|" + COD_AUT + "|" + VL_DA + "|" + DT_VCTO.PadLeft(8, '0') + "|" + DT_PGTO.PadLeft(8, '0') + "|";
            return "|" + "C112" + "|" + COD_DA + "|" + UF + "|" + NUM_DA + "|" + COD_AUT + "|" + VL_DA.Replace(".", ",") + "|" + DT_VCTO + "|" + DT_PGTO + "|";
        }
        protected string registroC113(string IND_OPER, string IND_EMIT, string COD_PART, string COD_MOD, string SER, string SUB, string NUM_DOC, string DT_DOC)
        {
            countC990++;
            count9999++;
            //return "|" + "C113" + "|" + IND_OPER.PadRight(1, ' ') + "|" + IND_EMIT.PadRight(2, ' ') + "|" + COD_PART.PadRight(60, ' ') + "|" + COD_MOD.PadRight(2, ' ') + "|" + SER.PadRight(4, ' ') + "|" + SUB.PadLeft(3, '0') + "|" + NUM_DOC.PadLeft(9, '0') + "|" + DT_DOC.PadLeft(8, '0') + "|";
            return "|" + "C113" + "|" + IND_OPER + "|" + IND_EMIT + "|" + COD_PART + "|" + COD_MOD + "|" + SER + "|" + SUB + "|" + NUM_DOC + "|" + DT_DOC + "|";
        }
        protected string registroC114(string COD_MOD, string ECF_FAB, string ECF_CX, string NUM_DOC, string DT_DOC)
        {
            countC990++;
            count9999++;
            //return "|" + "C114" + "|" + COD_MOD.PadRight(2, ' ') + "|" + ECF_FAB.PadRight(21, ' ') + "|" + ECF_CX.PadLeft(3, '0') + "|" + NUM_DOC.PadLeft(9, '0') + "|" + DT_DOC.PadLeft(8, '0') + "|";
            return "|" + "C114" + "|" + COD_MOD + "|" + ECF_FAB + "|" + ECF_CX + "|" + NUM_DOC + "|" + DT_DOC + "|";
        }
        protected string registroC115(string IND_CARGA, string CNPJ_COL, string IE_COL, string CPF_COL, string COD_MUN_COL, string CNPJ_ENTG, string IE_ENTG, string CPF_ENTG, string COD_MUN_ENTG)
        {
            countC990++;
            count9999++;
            //return "|" + "C115" + "|" + IND_CARGA.PadLeft(1, '0') + "|" + CNPJ_COL.PadRight(14, '0') + "|" + IE_COL.PadRight(14, ' ') + "|" + CPF_COL.PadLeft(11, '0') + "|" + COD_MUN_COL.PadLeft(7, '0') + "|" + CNPJ_ENTG.PadRight(14, '0') + "|" + IE_ENTG.PadRight(14, ' ') + "|" + CPF_ENTG.PadLeft(11, '0') + "|" + COD_MUN_ENTG.PadLeft(7, '0') + "|";
            return "|" + "C115" + "|" + IND_CARGA + "|" + CNPJ_COL + "|" + IE_COL + "|" + CPF_COL + "|" + COD_MUN_COL + "|" + CNPJ_ENTG + "|" + IE_ENTG + "|" + CPF_ENTG + "|" + COD_MUN_ENTG + "|";
        }
        protected string registroC116(string COD_MOD, string NR_SAT, string CHV_CFE, string NUM_CFE, string DT_DOC)
        {
            countC990++;
            count9999++;
            //return "|" + "C116" + "|" + COD_MOD.PadRight(2, ' ') + "|" + NR_SAT.PadLeft(9, '0') + "|" + CHV_CFE.PadLeft(44, '0') + "|" + NUM_CFE.PadLeft(6, '0') + "|" + DT_DOC.PadLeft(8, '0') + "|";
            return "|" + "C116" + "|" + COD_MOD + "|" + NR_SAT + "|" + CHV_CFE + "|" + NUM_CFE + "|" + DT_DOC + "|";
        }
        protected string registroC120(string COD_DOC_IMP, string NUM_DOC__IMP, string PIS_IMP, string COFINS_IMP, string NUM_ACDRAW)
        {
            countC990++;
            count9999++;
            //return "|" + "C120" + "|" + COD_DOC_IMP.PadRight(1, ' ') + "|" + NUM_DOC__IMP.PadLeft(12, '0') + "|" + PIS_IMP + "|" + COFINS_IMP + "|" + NUM_ACDRAW.PadRight(20, ' ') + "|";
            return "|" + "C120" + "|" + COD_DOC_IMP + "|" + NUM_DOC__IMP + "|" + PIS_IMP + "|" + COFINS_IMP + "|" + NUM_ACDRAW + "|";
        }
        protected string registroC130(string VL_SERV_NT, string VL_BC_ISSQN, string VL_ISSQN, string VL_BC_IRRF, string VL_IRRF, string VL_BC_PREV, string VL_PREV)
        {
            countC990++;
            count9999++;
            return "|" + "C130" + "|" + VL_SERV_NT.Replace(".", ",") + "|" + VL_BC_ISSQN.Replace(".", ",") + "|" + VL_BC_ISSQN.Replace(".", ",") + "|" + VL_BC_IRRF.Replace(".", ",") + "|" + VL_IRRF.Replace(".", ",") + "|" + VL_BC_PREV.Replace(".", ",") + "|" + VL_PREV.Replace(".", ",") + "|";
        }
        protected string registroC140(string IND_EMIT, string IND_TIT, string DESC_TIT, string NUM_TIT, string QTD_PARC, string VL_TIT)
        {
            countC990++;
            count9999++;
            //return "|" + "C140" + "|" + IND_EMIT.PadRight(1, ' ') + "|" + IND_TIT.PadRight(2, ' ') + "|" + DESC_TIT + "|" + NUM_TIT + "|" + QTD_PARC.PadLeft(2, '0') + "|" + VL_TIT + "|";
            return "|" + "C140" + "|" + IND_EMIT + "|" + IND_TIT + "|" + DESC_TIT + "|" + NUM_TIT + "|" + QTD_PARC.Replace(".", ",") + "|" + VL_TIT.Replace(".", ",") + "|";
        }
        protected string registroC141(string NUM_PARC, string DT_VCTO, string VL_PARC)
        {
            countC990++;
            count9999++;
            //return "|" + "C141" + "|" + NUM_PARC.PadRight(1, ' ') + "|" + DT_VCTO.PadRight(2, ' ') + "|" + VL_PARC + "|";
            return "|" + "C141" + "|" + NUM_PARC + "|" + DT_VCTO + "|" + VL_PARC.Replace(".", ",") + "|";
        }
        protected string registroC170(string NUM_ITEM, string COD_ITEM, string DESCR_COMPL, string QTD, string UNID, string VL_ITEM, string VL_DESC, string IND_MOV, string CST_ICMS, string CFOP, string COD_NAT, string VL_BC_ICMS, string ALIQ_ICMS, string VL_ICMS, string VL_BC_ICMS_ST, string ALIQ_ST, string VL_ICMS_ST, string IND_APUR, string CST_IPI, string COD_ENQ, string VL_BC_IPI, string ALIQ_IPI, string VL_IPI, string CST_PIS, string VL_BC_PIS, string PER_ALIQ_PIS, string QUANT_BC_PIS, string VAR_ALIQ_PIS, string VL_PIS, string CST_COFINS, string VL_BC_COFINS, string PER_ALIQ_COFINS, string QUANT_BC_COFINS, string VAR_ALIQ_COFINS, string VL_COFINS, string COD_CTA)
        {
            countC990++;
            count9999++;
            //return "|" + "C170" + "|" + NUM_ITEM.PadLeft(3, '0') + "|" + COD_ITEM.PadRight(60, ' ') + "|" + DESCR_COMPL + "|" + QTD.Replace(".", ",") + "|" + UNID.PadRight(6, ' ') + "|" + VL_ITEM.Replace(".", ",") + "|" + VL_DESC.Replace(".", ",") + "|" + IND_MOV.PadRight(1, ' ') + "|" + CST_ICMS.PadLeft(3, '0') + "|" + CFOP.PadLeft(4, '0') + "|" + COD_NAT.PadRight(10, ' ') + "|" + VL_BC_ICMS.Replace(".", ",") + "|" + ALIQ_ICMS.PadLeft(6, '0') + "|" + VL_ICMS.Replace(".", ",") + "|" + VL_BC_ICMS_ST.Replace(".", ",") + "|" + ALIQ_ST.Replace(".", ",") + "|" + VL_ICMS_ST.Replace(".", ",") + "|" + IND_APUR.PadRight(1, ' ') + "|" + CST_IPI.PadRight(2, ' ') + "|" + COD_ENQ.PadRight(3, ' ') + "|" + VL_BC_IPI.Replace(".", ",") + "|" + ALIQ_IPI.PadLeft(6, '0').Replace(".", ",") + "|" + VL_IPI.Replace(".", ",") + "|" + CST_PIS.PadLeft(3, '0') + "|" + VL_BC_PIS.Replace(".", ",") + "|" + PER_ALIQ_PIS.PadLeft(8, '0').Replace(".", ",") + "|" + QUANT_BC_PIS.Replace(".", ",") + "|" + VAR_ALIQ_PIS.Replace(".", ",") + "|" + CST_COFINS.PadLeft(2, '0') + "|" + VL_BC_COFINS.Replace(".", ",") + "|" + PER_ALIQ_COFINS.Replace(".", ",") + "|" + QUANT_BC_COFINS.Replace(".", ",") + "|" + VAR_ALIQ_COFINS.Replace(".", ",") + "|" + VL_COFINS.Replace(".", ",") + "|" + COD_CTA + "|";
            return "|" + "C170" + "|" + (NUM_ITEM == "0" ? "1" : NUM_ITEM) + "|" + COD_ITEM + "|" + DESCR_COMPL + "|" + QTD.Replace(".", ",") + "|" + UNID + "|" + VL_ITEM.Replace(".", ",") + "|" + VL_DESC.Replace(".", ",") + "|" + IND_MOV + "|" + CST_ICMS + "|" + CFOP + "|" + COD_NAT + "|" + VL_BC_ICMS.Replace(".", ",") + "|" + ALIQ_ICMS.Replace(".", ",") + "|" + VL_ICMS.Replace(".", ",") + "|" + VL_BC_ICMS_ST.Replace(".", ",") + "|" + ALIQ_ST.Replace(".", ",") + "|" + VL_ICMS_ST.Replace(".", ",") + "|" + IND_APUR + "|" + CST_IPI + "|" + COD_ENQ + "|" + VL_BC_IPI.Replace(".", ",") + "|" + ALIQ_IPI.Replace(".", ",") + "|" + VL_IPI.Replace(".", ",") + "|" + CST_PIS + "|" + VL_BC_PIS.Replace(".", ",") + "|" + PER_ALIQ_PIS.Replace(".", ",") + "|" + QUANT_BC_PIS.Replace(".", ",") + "|" + VAR_ALIQ_PIS.Replace(".", ",")+ "|" + VL_PIS.Replace(".", ",") + "|" + CST_COFINS + "|" + VL_BC_COFINS.Replace(".", ",") + "|" + PER_ALIQ_COFINS.Replace(".", ",") + "|" + QUANT_BC_COFINS.Replace(".", ",") + "|" + VAR_ALIQ_COFINS.Replace(".", ",") + "|" + VL_COFINS.Replace(".", ",") + "|" + COD_CTA + "|";
        }
        protected string registroC179(string BC_ST_ORIG_DEST, string ICMS_ST_REP, string ICMS_ST_COMPL, string BC_RET, string ICMS_RET)
        {
            countC990++;
            count9999++;
            return "|" + "C179" + "|" + BC_ST_ORIG_DEST + "|" + ICMS_ST_REP + "|" + ICMS_ST_COMPL + "|" + BC_RET.Replace(".", ",") + "|" + ICMS_RET.Replace(".", ",") + "|";
        }
        protected string registroC190(string CST_ICMS, string CFOP, string ALIQ_ICMS, string VL_OPR, string VL_BC_ICMS, string VL_ICMS, string VL_BC_ICMS_ST, string VL_ICMS_ST, string VL_RED_BC, string VL_IPI, string COD_OBS)
        {
            countC990++;
            count9999++;
            call_creditos = 1;
            call_debitos_nf = 1;
            //return "|" + "C190" + "|" + CST_ICMS.PadLeft(3, '0') + "|" + CFOP.PadLeft(4, '0') + "|" + ALIQ_ICMS.PadLeft(6, '0') + "|" + VL_OPR + "|" + VL_BC_ICMS + "|" + VL_ICMS + "|" + VL_BC_ICMS_ST + "|" + VL_ICMS_ST + "|" + VL_IPI + "|" + COD_OBS.PadRight(6, ' ') + "|";
            return "|" + "C190" + "|" + CST_ICMS.PadLeft(3, '0') + "|" + CFOP + "|" + ALIQ_ICMS.Replace(".", ",") + "|" + VL_OPR.Replace(".", ",") + "|" + VL_BC_ICMS.Replace(".", ",") + "|" + VL_ICMS.Replace(".", ",") + "|" + VL_BC_ICMS_ST.Replace(".", ",") + "|" + VL_ICMS_ST.Replace(".", ",") + "|" + VL_RED_BC.Replace(".",",") + "|" + VL_IPI.Replace(".", ",") + "|" + COD_OBS + "|";
        }
        protected string registroC195(string COD_OBS, string TXT_COMPL)
        {
            countC990++;
            count9999++;
            //return "|" + "C195" + "|" + COD_OBS.PadRight(6, ' ') + "|" + TXT_COMPL + "|";
            return "|" + "C195" + "|" + COD_OBS + "|" + TXT_COMPL + "|";
        }
        protected string registroC300(string COD_MOD, string SER, string SUB, string NUM_DOC_INI, string NUM_DOC_FIN, string DT_DOC, string VL_DOC, string VL_PIS, string VL_COFINS, string COD_CTA)
        {
            countC990++;
            count9999++;
            //return "|" + "C300" + "|" + COD_MOD.PadRight(2, ' ') + "|" + SER.PadRight(4, ' ') + "|" + SUB.PadRight(3, ' ') + "|" + NUM_DOC_INI.PadLeft(6, '0') + "|" + NUM_DOC_FIN.PadLeft(6, '0') + "|" + DT_DOC.PadLeft(8, '0') + "|" + VL_DOC + "|" + VL_PIS + "|" + VL_COFINS + "|" + COD_CTA + "|";
            return "|" + "C300" + "|" + COD_MOD + "|" + SER + "|" + SUB + "|" + NUM_DOC_INI + "|" + NUM_DOC_FIN + "|" + DT_DOC + "|" + VL_DOC.Replace(".", ",") + "|" + VL_PIS.Replace(".", ",") + "|" + VL_COFINS.Replace(".", ",") + "|" + COD_CTA + "|";
        }
        protected string registroC310(string NUM_DOC_CANC)
        {
            countC990++;
            count9999++;
            return "|" + "C310" + "|" + NUM_DOC_CANC + "|";
        }
        protected string registroC320(string CST_ICMS, string CFOP, string ALIQ_ICMS, string VL_OPR, string VL_BC_ICMS, string VL_ICMS, string VL_RED_BC, string COD_OBS)
        {
            countC990++;
            count9999++;
            //return "|" + "C320" + "|" + CST_ICMS.PadLeft(3, '0') + "|" + CFOP.PadLeft(4, '0') + "|" + ALIQ_ICMS.PadLeft(6, '0') + "|" + VL_OPR + "|" + VL_BC_ICMS + "|" + VL_ICMS + "|" + VL_RED_BC + "|" + COD_OBS.PadRight(6,' ') + "|";
            return "|" + "C320" + "|" + CST_ICMS.PadLeft(3, '0') + "|" + CFOP.PadLeft(4, '0') + "|" + ALIQ_ICMS.PadLeft(6, '0') + "|" + VL_OPR.Replace(".", ",") + "|" + VL_BC_ICMS.Replace(".", ",") + "|" + VL_ICMS.Replace(".", ",") + "|" + VL_RED_BC.Replace(".", ",") + "|" + COD_OBS + "|";
        }
        protected string registroC321(string COD_ITEM, string QTD, string UNID, string VL_ITEM, string VL_DESC, string VL_BC_ICMS, string VL_ICMS, string VL_PIS, string VL_COFINS)
        {
            countC990++;
            count9999++;
            return "|" + "C321" + "|" + COD_ITEM + "|" + QTD.Replace(".", ",") + "|" + UNID + "|" + VL_ITEM.Replace(".", ",") + "|" + VL_DESC + "|" + VL_BC_ICMS.Replace(".", ",") + "|" + VL_ICMS.Replace(".", ",") + "|" + VL_PIS.Replace(".", ",") + "|" + VL_COFINS.Replace(".", ",") + "|";
        }
        protected string registroC350(string SER, string SUB_SER, string NUM_DOC, string DT_DOC,string CNPJ_CPF, string VL_MERC, string VL_DOC, string VL_DESC, string VL_PIS, string VL_COFINS, string COD_CTA)
        {
            countC990++;
            count9999++;
            return "|" + "C350" + "|" + SER + "|" + SUB_SER + "|" + NUM_DOC + "|" + DT_DOC.PadLeft(8, '0') + "|" + CNPJ_CPF.PadLeft(14, ' ') + "|" + VL_MERC.Replace(".", ",") + "|" + VL_DOC.Replace(".", ",") + "|" + VL_DESC.Replace(".", ",") + "|" + VL_PIS.Replace(".", ",") + "|" + VL_COFINS.Replace(".", ",") + "|" + COD_CTA + "|";
        }

        protected string registroC370(string NUM_ITEM, string COD_ITEM, string QTD, string UNID, string VL_ITEM, string VL_DESC)
        {
            countC990++;
            count9999++;
            //return "|" + "C370" + "|" + NUM_ITEM.PadLeft(3, '0') + "|" + COD_ITEM.PadRight(60, ' ') + "|" + QTD + "|" + UNID.PadRight(6, '0') + "|" + VL_ITEM + "|" + VL_DESC + "|";
            return "|" + "C370" + "|" + NUM_ITEM + "|" + COD_ITEM + "|" + QTD + "|" + UNID + "|" + VL_ITEM.Replace(".", ",") + "|" + VL_DESC.Replace(".", ",") + "|";
        }

        protected string registroC390(string CST_ICMS, string CFOP, string ALIQ_ICMS, string VL_OPR, string VL_BC_ICMS, string VL_ICMS, string VL_RED_BC, string COD_OBS)
        {
            countC990++;
            count9999++;
            call_debitos_cp = 1;
            return "|" + "C390" + "|" + CST_ICMS.PadLeft(3, '0') + "|" + CFOP.PadLeft(4, '0') + "|" + ALIQ_ICMS.PadLeft(6, '0') + "|" + VL_OPR.Replace(".", ",") + "|" + VL_BC_ICMS.Replace(".", ",") + "|" + VL_ICMS.Replace(".", ",") + "|" + VL_RED_BC.Replace(".", ",") + "|" + COD_OBS + "|";
        }
        protected string registroC400(string COD_MOD, string ECF_MOD, string ECF_FAB, string ECF_CX)
        {
            countC990++;
            count9999++;
            //return "|" + "C400" + "|" + COD_MOD.PadRight(2, ' ') + "|" + ECF_MOD.PadRight(20, ' ') + "|" + ECF_FAB.PadRight(21, ' ') + "|" + ECF_CX.PadLeft(3, '0') + "|";
            return "|" + "C400" + "|" + COD_MOD + "|" + ECF_MOD + "|" + ECF_FAB + "|" + ECF_CX + "|";
        }
        protected string registroC405(string DT_DOC, string CRO, string CRZ, string NUM_COO_FIN, string GT_FIN, string VL_BRT)
        {
            countC990++;
            count9999++;
            return "|" + "C405" + "|" + DT_DOC + "|" + CRO + "|" + CRZ + "|" + NUM_COO_FIN + "|" + VL_BRT.Replace(".", ",") + "|" + GT_FIN.Replace(".", ",") + "|";
        }
        protected string registroC410(string VL_PIS, string VL_COFINS)
        {
            countC990++;
            count9999++;
            return "|" + "C410" + "|" + VL_PIS.Replace(".", ",") + "|" + VL_COFINS.Replace(".", ",") + "|";
        }
        protected string registroC420(string COD_TOT_PAR, string VLR_ACUM_TOT, string NR_TOT, string DESCR_NR_TOT)
        {
            countC990++;
            count9999++;
            //return "|" + "C420" + "|" + COD_TOT_PAR.PadRight(7, ' ').Replace(".", ",") + "|" + VLR_ACUM_TOT.Replace(".", ",") + "|" + NR_TOT.PadLeft(2, '0') + "|" + DESCR_NR_TOT.Replace(".", ",") + "|";
            return "|" + "C420" + "|" + COD_TOT_PAR.Replace(".", ",") + "|" + VLR_ACUM_TOT.Replace(".", ",") + "|" + NR_TOT + "|" + DESCR_NR_TOT + "|";
        }
        protected string registroC425(string COD_ITEM, string QTD, string UNID, string VL_ITEM, string VL_PIS, string VL_COFINS)
        {
            countC990++;
            count9999++;
            //return "|" + "C425" + "|" + COD_ITEM.PadRight(60, ' ') + "|" + QTD.Replace(".", ",") + "|" + UNID.PadRight(6, ' ').Replace(".", ",") + "|" + VL_ITEM.Replace(".", ",").Replace(".", ",") + "|" + VL_PIS.Replace(".", ",") + "|" + VL_COFINS.Replace(".", ",") + "|";
            return "|" + "C425" + "|" + COD_ITEM + "|" + QTD.Replace(".", ",") + "|" + UNID.Replace(".", ",") + "|" + VL_ITEM.Replace(".", ",") + "|" + VL_PIS.Replace(".", ",") + "|" + VL_COFINS.Replace(".", ",") + "|";
        }
        protected string registroC460(string COD_MOD, string COD_SIT, string NUM_DOC, string DT_DOC, string VL_DOC, string VL_PIS, string VL_COFINS, string CPF_CNPJ, string NOM_ADQ)
        {
            countC990++;
            count9999++;
            //return "|" + "C460" + "|" + COD_MOD.PadRight(2, ' ') + "|" + COD_SIT.PadLeft(2, '0') + "|" + NUM_DOC.PadLeft(9, '0') + "|" + DT_DOC.PadLeft(8, '0') + "|" + VL_DOC + "|" + VL_PIS.Replace(".", ",") + "|" + VL_COFINS.Replace(".", ",") + "|" + CPF_CNPJ.PadLeft(14, '0') + "|" + NOM_ADQ.PadRight(60, ' ') + "|";
            return "|" + "C460" + "|" + COD_MOD + "|" + COD_SIT.PadLeft(2, '0') + "|" + NUM_DOC + "|" + DT_DOC + "|" + VL_DOC.Replace(".", ",") + "|" + VL_PIS.Replace(".", ",") + "|" + VL_COFINS.Replace(".", ",") + "|" + CPF_CNPJ + "|" + NOM_ADQ + "|";
        }
        protected string registroC465(string CHV_CFE, string NUM_CCF)
        {
            countC990++;
            count9999++;
            //return "|" + "C465" + "|" + CHV_CFE.PadLeft(44, '0') + "|" + NUM_CCF.PadLeft(9, '0') + "|";
            return "|" + "C465" + "|" + CHV_CFE + "|" + NUM_CCF + "|";
        }
        protected string registroC470(string COD_ITEM, string QTD, string QTD_CANC, string UNID, string VL_ITEM,string CST_ICMS, string CFOP, string ALIQ_ICMS, string VL_PIS, string VL_COFINS)
        {
            countC990++;
            count9999++;
            //return "|" + "C470" + "|" + COD_ITEM.PadRight(60, ' ').Replace(".", ",") + "|" + QTD.Replace(".", ",") + "|" + QTD_CANC.Replace(".", ",") + "|" + UNID.PadRight(6, ' ') + "|" + VL_ITEM.Replace(".", ",") + "|" + CST_ICMS.PadLeft(3, '0') + "|" + CFOP.PadLeft(4, '0') + "|" + ALIQ_ICMS.PadLeft(6, '0').Replace(".", ",") + "|" + VL_PIS.Replace(".", ",") + "|" + VL_COFINS.Replace(".", ",") + "|";
            return "|" + "C470" + "|" + COD_ITEM.Replace(".", ",") + "|" + QTD.Replace(".", ",") + "|" + QTD_CANC.Replace(".", ",") + "|" + UNID + "|" + VL_ITEM.Replace(".", ",") + "|" + CST_ICMS.PadLeft(3, '0') + "|" + CFOP + "|" + ALIQ_ICMS.Replace(".", ",") + "|" + VL_PIS.Replace(".", ",") + "|" + VL_COFINS.Replace(".", ",") + "|";
        }
        protected string registroC490(string CST_ICMS, string CFOP, string ALIQ_ICMS, string VL_OPR, string VL_BC_ICMS,string VL_ICMS, string COD_OBS)
        {
            countC990++;
            count9999++;
            call_debitos_cp = 1;
            //return "|" + "C490" + "|" + CST_ICMS.PadRight(3, ' ') + "|" + CFOP.PadLeft(4, '0') + "|" + ALIQ_ICMS.PadLeft(6, '0').Replace(".", ",") + "|" + VL_OPR.Replace(".", ",") + "|" + VL_BC_ICMS.Replace(".", ",") + "|" + VL_ICMS.Replace(".", ",") + "|" + COD_OBS.PadLeft(6, '0') + "|";
            return "|" + "C490" + "|" + CST_ICMS + "|" + CFOP + "|" + ALIQ_ICMS.Replace(".", ",") + "|" + VL_OPR.Replace(".", ",") + "|" + VL_BC_ICMS.Replace(".", ",") + "|" + VL_ICMS.Replace(".", ",") + "|" + COD_OBS + "|";
        }
        protected string registroC495(string ALIQ_ICMS, string COD_ITEM, string QTD, string QTD_CANC, string UNID, string VL_ITEM, string VL_DESC, string VL_CANC, string VL_ACMO, string VL_BC_ICMS, string VL_ICMS, string VL_ISEN, string VL_NT, string VL_ICMS_ST)
        {
            countC990++;
            count9999++;
            return "|" + "C495" + "|" + ALIQ_ICMS.PadLeft(6, '0').Replace(".", ",") + "|" + COD_ITEM.PadRight(60, ' ') + "|" + QTD + "|" + QTD_CANC + "|" + UNID.PadRight(6, ' ') + "|" + VL_ITEM.Replace(".", ",") + "|" + VL_DESC.Replace(".", ",") + "|" + VL_CANC.Replace(".", ",") + "|" + VL_ACMO.Replace(".", ",") + "|" + VL_BC_ICMS.Replace(".", ",") + "|" + VL_ICMS.Replace(".", ",") + "|" + VL_ISEN.Replace(".", ",") + "|" + VL_NT.Replace(".", ",") + "|" + VL_ICMS_ST.Replace(".", ",") + "|";
        }
        protected string registroC800(string COD_MOD, string COD_SIT, string NUM_CFE, string DT_DOC, string VL_CFE, string VL_PIS, string VL_COFINS, string CNPJ_CPF, string NR_SAT, string CHV_CFE, string VL_DESC, string VL_MERC, string VL_OUT_DA)
        {
            countC990++;
            count9999++;
            return "|" + "C800" + "|" + COD_MOD.PadRight(2, ' ') + "|" + NUM_CFE.PadLeft(2, '0') + "|" + DT_DOC.PadLeft(8,'0') + "|" + VL_CFE + "|" + VL_PIS + "|" + VL_COFINS + "|" + CNPJ_CPF.PadLeft(14,'0') + "|" + NR_SAT.PadLeft(9,'0') + "|" + CHV_CFE.PadLeft(44,'0') + "|" + VL_DESC + "|" + VL_MERC + "|" + VL_OUT_DA + "|";
        }
        protected string registroC850(string CST_ICMS, string CFOP, string ALIQ_ICMS, string VL_OPR, string VL_BC_ICMS, string VL_ICMS, string COD_OBS)
        {
            countC990++;
            count9999++;
            return "|" + "C850" + "|" + CST_ICMS.PadLeft(3, '0') + "|" + CFOP.PadLeft(4, '0') + "|" + ALIQ_ICMS.PadLeft(6, '0') + "|" + VL_OPR + "|" + VL_BC_ICMS + "|" + VL_ICMS + "|" + COD_OBS.PadRight(6, ' ') + "|";
        }
        protected string registroC860(string COD_MOD, string NR_SAT, string DT_DOC, string DOC_INI, string DOC_FIM)
        {
            countC990++;
            count9999++;
            return "|" + "C860" + "|" + COD_MOD.PadRight(2, ' ') + "|" + NR_SAT.PadLeft(9, '0') + "|" + DT_DOC.PadLeft(8, '0') + "|" + DOC_INI.PadLeft(6, '0') + "|" + DOC_FIM.PadLeft(6, '0') + "|";
        }
        protected string registroC890(string CST_ICMS, string CFOP, string ALIQ_ICMS, string VL_OPR, string VL_BC_ICMS, string VL_ICMS, string COD_OBS)
        {
            countC990++;
            count9999++;
            return "|" + "C890" + "|" + CST_ICMS.PadLeft(3, '0') + "|" + CFOP.PadLeft(4, '0') + "|" + ALIQ_ICMS.PadLeft(6, '0') + "|" + VL_OPR + "|" + VL_BC_ICMS + "|" + VL_ICMS + "|" + COD_OBS.PadRight(6, ' ') + "|";
        }
        protected string registroC990(string QTD_LIN_C)
        {
            countC990++;
            count9999++;
            return "|" + "C990" + "|" + QTD_LIN_C + "|";
        }

        protected string registroD001(string IND_MOV)
        {
            countD990++;
            count9999++;
            return "|" + "D001" + "|" + IND_MOV.PadRight(1,' ') + "|";
        }

        protected string registroD100(string IND_OPER, string IND_EMIT, string COD_PART, string COD_MOD, string COD_SIT, string SER, string SUB, string NUM_DOC, string CHV_CTE, string DT_DOC, string DT_A_P, string TP_CT_E, string CHV_CTE_REF, string VL_DOC, string VL_DESC, string IND_FRT, string VL_SERV, string VL_BC_ICMS, string VL_ICMS, string VL_NT, string COD_INF, string COD_CTA)
        {
            countD990++;
            count9999++;
            //return "|" + "D100" + "|" + IND_OPER.PadRight(1, ' ') + "|" + IND_EMIT.PadRight(1, ' ') + "|" + COD_PART.PadRight(60, ' ') + "|" + COD_MOD.PadRight(2, ' ') + "|" + COD_SIT.PadRight(2, ' ') + "|" + SER.PadRight(4, ' ') + "|" + SUB.PadRight(3, ' ') + "|" + NUM_DOC.PadLeft(9, '0') + "|" + CHV_CTE.PadLeft(44, '0') + "|" + DT_A_P.PadLeft(8, '0') + "|" + DT_A_P.PadLeft(8, ' ') + "|" + TP_CT_E.PadLeft(1, '0') + "|" + CHV_CTE_REF.PadLeft(44, '0') + "|" + VL_DOC.Replace(".", ",") + "|" + VL_DESC.Replace(".", ",") + "|" + IND_FRT.PadRight(1, '0') + "|" + VL_SERV.Replace(".", ",") + "|" + VL_BC_ICMS.Replace(".", ",") + "|" + VL_ICMS.Replace(".", ",") + "|" + VL_NT.Replace(".", ",") + "|" + COD_INF.PadRight(6, ' ') + "|" + COD_CTA + "|";
            return "|" + "D100" + "|" + IND_OPER + "|" + IND_EMIT + "|" + COD_PART + "|" + COD_MOD.PadRight(2, ' ') + "|" + COD_SIT.PadRight(2, ' ') + "|" + SER + "|" + SUB + "|" + NUM_DOC + "|" + CHV_CTE + "|" + DT_A_P.PadLeft(8, '0') + "|" + DT_A_P.PadLeft(8, ' ') + "|" + TP_CT_E + "|" + CHV_CTE_REF + "|" + VL_DOC.Replace(".", ",") + "|" + VL_DESC.Replace(".", ",") + "|" + IND_FRT + "|" + VL_SERV.Replace(".", ",") + "|" + VL_BC_ICMS.Replace(".", ",") + "|" + VL_ICMS.Replace(".", ",") + "|" + VL_NT.Replace(".", ",") + "|" + COD_INF + "|" + COD_CTA + "|";
        }
        protected string registroD190(string CST_ICMS, string CFOP, string ALIQ_ICMS, string VL_OPR, string VL_BC_ICMS, string VL_ICMS, string VL_RED_BC, string COD_OBS)
        {
            countD990++;
            count9999++;
            call_debitos_nf = 1;
            return "|" + "D190" + "|" + CST_ICMS.Replace(".", ",") + "|" + CFOP.PadLeft(4, '0') + "|" + ALIQ_ICMS.PadLeft(6, '0').Replace(".", ",") + "|" + VL_OPR.Replace(".", ",") + "|" + VL_BC_ICMS.Replace(".", ",") + "|" + VL_ICMS.Replace(".", ",") + "|" + VL_RED_BC.Replace(".", ",") + "|" + COD_OBS.PadRight(6, ' ') + "|";
        }
        protected string registroD195(string COD_OBS, string TXT_COMPL)
        {
            countD990++;
            count9999++;
            return "|" + "D195" + "|" + COD_OBS.PadLeft(6, '0') + "|" + TXT_COMPL + "|";
        }
        protected string registroD990(string QTD_LIN_D)
        {
            countD990++;
            count9999++;
            return "|" + "D990" + "|" + QTD_LIN_D+ "|";
        }

        protected string registroE001(string IND_MOV)
        {
            countE990++;
            count9999++;
            return "|" + "E001" + "|" + IND_MOV.PadRight(1, ' ') + "|";
        }
        protected string registroE100(string DT_INI, string DT_FIM)
        {
            countE990++;
            count9999++;
            string ALTER1 = DT_INI.Substring(6, 2) + DT_INI.Substring(4, 2) + DT_INI.Substring(0, 4);
            string ALTER2 = DT_FIM.Substring(6, 2) + DT_FIM.Substring(4, 2) + DT_FIM.Substring(0, 4);
            //return "|" + "E100" + "|" + String.Format("{0:ddMMyyyy}", Convert.ToDateTime(DT_INI)) + "|" + String.Format("{0:ddMMyyyy}", Convert.ToDateTime(DT_FIM)) + "|";
            return "|" + "E100" + "|" + ALTER1 + "|" + ALTER2 + "|";
        }

        protected string registroE110(string VL_TOT_DEBITOS, string VL_AJ_DEBITOS, string VL_TOT_AJ_DEBITOS, string VL_ESTORNOS_CRED, string VL_TOT_CREDITOS, string VL_AJ_CREDITOS, string VL_TOT_AJ_CREDITOS, string VL_ESTORNOS_DEB, string VL_SLD_CREDOR_ANT, string VL_SLD_APURADO, string VL_TOT_DED, string VL_ICMS_RECOLHER, string VL_SLD_CREDOR_TRANSPORTAR, string DEB_ESP)
        {
            countE990++;
            count9999++;
            return "|" + "E110" + "|" + VL_TOT_DEBITOS.Replace(".", ",") + "|" + VL_AJ_DEBITOS.Replace(".", ",") + "|" + VL_TOT_AJ_DEBITOS.Replace(".", ",") + "|" + VL_ESTORNOS_CRED.Replace(".", ",") + "|" + VL_TOT_CREDITOS.Replace(".", ",") + "|" + VL_AJ_CREDITOS.Replace(".", ",") + "|" + VL_TOT_AJ_CREDITOS.Replace(".", ",") + "|" + VL_ESTORNOS_DEB.Replace(".", ",") + "|" + VL_SLD_CREDOR_ANT.Replace(".", ",") + "|" + VL_SLD_APURADO.Replace(".", ",") + "|" + VL_TOT_DED.Replace(".", ",") + "|" + VL_ICMS_RECOLHER.Replace(".", ",") + "|" + VL_SLD_CREDOR_TRANSPORTAR.Replace(".", ",") + "|" + DEB_ESP + "|";
        }
        protected string registroE116(string COD_OR, string VL_OR, string DT_VCTO, string COD_REC, string NUM_PROC, string IND_PROC, string PROC, string TXT_COMPL, string MES_REF)
        {
            countE990++;
            count9999++;
            return "|" + "E116" + "|" + COD_OR + "|" + VL_OR.Replace(".",",") + "|" + DT_VCTO + "|" + COD_REC + "|" + NUM_PROC+ "|" + IND_PROC + "|" + PROC + "|" + TXT_COMPL + "|" + MES_REF + "|";
        }
        protected string registroE990(string QTD_LIN_E)
        {
            countE990++;
            count9999++;
            return "|" + "E990" + "|" + QTD_LIN_E + "|";
        }
        protected string registroG001(string IND_MOV)
        {
            countG990++;
            count9999++;
            return "|" + "G001" + "|" + IND_MOV.PadRight(1, ' ') + "|";
        }
        protected string registroG110(string DT_INI, string DT_FIM, string SALDO_IN_ICMS, string SOM_PARC, string VL_TRIB_EXP, string VL_TOTAL, string IND_PER_SAI, string ICMS_APROP, string SOM_ICMS_OC)
        {
            countG990++;
            count9999++;
            return "|" + "G110" + "|" + DT_INI.PadLeft(8, '0') + "|" + DT_FIM.PadLeft(8, '0') + "|" + SALDO_IN_ICMS + "|" + SOM_PARC + "|" + VL_TRIB_EXP + "|" + VL_TOTAL + "|" + IND_PER_SAI + "|" + ICMS_APROP + "|" + SOM_ICMS_OC + "|";
        }
        protected string registroG125(string COD_IND_BEM, string DT_MOV, string TIPO_MOV, string VL_IMOB_ICMS_OP, string VL_IMOB_ICMS_ST, string VL_IMOB_ICMS_FRT, string VL_IMOB_ICMS_DIF, string NUM_PARC, string VL_PARC_PASS)
        {
            countG990++;
            count9999++;
            return "|" + "G125" + "|" + COD_IND_BEM.PadRight(60, ' ') + "|" + DT_MOV.PadLeft(8, '0') + "|" + TIPO_MOV.PadRight(2, ' ') + "|" + VL_IMOB_ICMS_OP + "|" + VL_IMOB_ICMS_ST + "|" + VL_IMOB_ICMS_FRT + "|" + VL_IMOB_ICMS_DIF + "|" + NUM_PARC.PadLeft(3,'0') + "|" + VL_PARC_PASS + "|";
        }
        protected string registroG130(string IND_EMIT, string COD_PART, string COD_MOD, string SERIE, string NUM_DOC, string CHV_NFE_CTE, string DT_DOC)
        {
            countG990++;
            count9999++;
            return "|" + "G130" + "|" + IND_EMIT.PadRight(1, ' ') + "|" + COD_PART.PadRight(60, ' ') + "|" + COD_MOD.PadRight(2, ' ') + "|" + SERIE.PadRight(3, ' ') + "|" + NUM_DOC.PadLeft(9, '0') + "|" + CHV_NFE_CTE.PadLeft(44, '0') + "|" + DT_DOC.PadLeft(8, '0') + "|";
        }
        protected string registroG140(string NUM_ITEM, string COD_ITEM)
        {
            countG990++;
            count9999++;
            return "|" + "G140" + "|" + NUM_ITEM.PadLeft(3, '0') + "|" + COD_ITEM.PadRight(60, ' ') + "|";
        }
        protected string registroG990(string QTD_LIN_G)
        {
            countG990++;
            count9999++;
            return "|" + "G990" + "|" + QTD_LIN_G + "|";
        }
        protected string registroH001(string IND_MOV)
        {
            countH990++;
            count9999++;
            return "|" + "H001" + "|" + IND_MOV.PadRight(1, ' ') + "|";
        }
        protected string registroH005(string DT_INV, string VL_INV, string MOT_INV)
        {
            countH990++;
            count9999++;
            return "|" + "H005" + "|" + DT_INV.PadLeft(8,'0')+ "|" + VL_INV + "|" + MOT_INV.PadRight(2, ' ') + "|";
        }
        protected string registroH010(string COD_ITEM, string UNID, string QTD, string VL_UNIT, string VL_ITEM, string IND_PROP, string COD_PART, string TXT_COMPL, string COD_CTA, string VL_ITEM_IR)
        {
            countH990++;
            count9999++;
            return "|" + "H010" + "|" + COD_ITEM.PadRight(60, ' ').Trim() + "|" + UNID.PadRight(6, ' ').Trim() + "|" + QTD.Replace(".", ",") + "|" + VL_UNIT.Replace(".", ",") + "|" + VL_ITEM.Replace(".", ",") + "|" + IND_PROP.PadRight(1, ' ') + "|" + COD_PART.PadRight(1, ' ') + "|" + TXT_COMPL.Trim() + "|" + COD_CTA + "|" + VL_ITEM_IR + "|";
        }
        protected string registroH020(string CST_ICMS, string BC_ICMS, string VL_ICMS)
        {
            countH990++;
            count9999++;
            return "|" + "H020" + "|" + CST_ICMS.PadRight(60, ' ') + "|" + BC_ICMS.PadRight(6, ' ') + "|" + VL_ICMS + "|";
        }
        protected string registroH990(string QTD_LIN_H)
        {
            countH990++;
            count9999++;
            return "|" + "H990" + "|" + QTD_LIN_H + "|";
        }
        protected string registro1001(string IND_MOV)
        {
            count1111++;
            count9999++;
            return "|" + "1001" + "|" + IND_MOV.PadRight(1, ' ') + "|";
        }
        protected string registro1010(string IND_EXP, string IND_CCRF, string IND_COMB, string IND_USINA, string IND_VA, string IND_EE, string IND_CART, string IND_FORM, string IND_AER)
        {
            count1111++;
            count9999++;
            return "|" + "1010" + "|" + IND_EXP.PadRight(1, ' ') + "|" + IND_CCRF.PadRight(1, ' ') + "|" + IND_COMB.PadRight(1, ' ') + "|" + IND_USINA.PadRight(1, ' ') + "|" + IND_VA.PadRight(1, ' ') + "|" + IND_EE.PadRight(1, ' ') + "|" + IND_CART.PadRight(1, ' ') + "|" + IND_FORM .PadRight(1, ' ') + "|" + IND_AER.PadRight(1, ' ') + "|";
        }
        protected string registro1600(string COD_PART, string TOT_CREDITO, string TOT_DEBITO)
        {
            count1111++;
            count1600++;
            count9999++;
            return "|" + "1600" + "|" + COD_PART + "|" + TOT_CREDITO.Replace(".", ",") + "|" + TOT_DEBITO.Replace(".", ",") + "|";
        }
        protected string registro1990(string QTD_LIN_1)
        {
            count9999++;
            return "|" + "1990" + "|" + QTD_LIN_1 + "|";
        }
        protected string registro9001(string IND_MOV)
        {
            count9999++;
            return "|" + "9001" + "|" + IND_MOV.PadLeft(1,'0') + "|";
        }
        protected string registro9900(string REG_BL,string QTD_LIN_9)
        {
            count9900++;
            count9990++;
            count9999++;

            return "|" + "9900" + "|" + REG_BL + "|" + QTD_LIN_9 + "|";
        }
        protected string registro9990(string IND_MOV)
        {
            count9990++;
            count9999++;
            return "|" + "9990" + "|" + IND_MOV.PadLeft(1, '0') + "|";
        }
        protected string registro9999(string QTD_LIN_9999)
        {
            return "|" + "9999" + "|" + QTD_LIN_9999 + "|";
        }
        //#########################################################End Funções SPED##########################################################
    }
}
