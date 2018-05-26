using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Forms;


namespace conectorBema
{
    class conectorECF
    {
        public conectorECF()
        {
            Erros = "";
        }

        
        public static string Erros = "";
        public string _Erros
        {
            get
            {
                return Erros;
            }
            set
            {
                Erros = value;
            }
        }

        public static int ST3 = 0;
        public int _ST3
        {
            get
            {
                return ST3;
            }
            set
            {
                ST3 = value;
            }
        }



        #region Funções de tratamento de erro

        /// <summary>
        /// Função para analizar os retorno da impressora (ST1, ST2 e ST3).
        /// </summary>
        public static bool Analisa_RetornoImpressora([MarshalAs(UnmanagedType.VBByRefStr)] ref string stringACK, [MarshalAs(UnmanagedType.VBByRefStr)] ref string  stringST1, [MarshalAs(UnmanagedType.VBByRefStr)] ref string stringST2, [MarshalAs(UnmanagedType.VBByRefStr)] ref string stringST3)
        {
            bool valida = true;
            int ACK, ST1, ST2, ST3;
            ACK = ST1 = ST2 = ST3 =  ACK = 0;
            Erros = "";
            //Bematech_FI_HabilitaDesabilitaRetornoEstendidoMFD("1");
            Bematech_FI_RetornoImpressoraMFD(ref ACK, ref ST1, ref ST2, ref ST3);
            //Bematech_FI_RetornoImpressoraCV0909(ref ACK, ref ST1, ref ST2, ref ST3, ref ST4);

            #region Tratando o ACK
            if (ACK == 0)
            {
                valida = false;
                stringACK = "Erro de Comunicação !" + '\x0D';
            }
            #endregion

            #region Tratando o ST1
            if (ST1 >= 128)
            {
                valida = false;
                ST1 = ST1 - 128;
                Erros += "BIT 7 - Fim de Papel" + '\x0D';
            }
            if (ST1 >= 64)
            {
                ST1 = ST1 - 64;
                Erros += "BIT 6 - Pouco Papel" + '\x0D';
            }
            if (ST1 >= 66)
            {
                ST1 = ST1 - 66;
                Erros += "Pouco Papel - Troque a Bobina" + '\x0D';
            }
            if (ST1 >= 32)
            {
                valida = false;
                ST1 = ST1 - 32;
                Erros += "BIT 5 - Erro no Relógio" + '\x0D';
            }
            if (ST1 >= 16)
            {
                valida = false;
                ST1 = ST1 - 16;
                Erros += "BIT 4 - Impressora em ERRO" + '\x0D';
            }
            if (ST1 >= 8)
            {
                valida = false;
                ST1 = ST1 - 8;
                Erros += "BIT 3 - CMD não iniciado com ESC" + '\x0D';
            }
            if (ST1 >= 4)
            {
                valida = false;
                ST1 = ST1 - 4;
                Erros += "BIT 2 - Comando Inexistente" + '\x0D';
            }
            if (ST1 >= 2)
            {
                ST1 = ST1 - 2;
                Erros += "BIT 1 - Cupom Aberto" + '\x0D';
            }
            if (ST1 >= 1)
            {
                valida = false;
                ST1 = ST1 - 1;
                Erros += "BIT 0 - Nº de Parâmetros Inválidos" + '\x0D';
            }
            if (Erros != "")
            {
                stringST1 = Erros;
                Erros = "";
            }
            #endregion

            #region Tratando o ST2
            if (ST2 >= 128)
            {
                valida = false;
                ST2 = ST2 - 128;
                Erros += "BIT 7 - Tipo de Parâmetro Inválido" + '\x0D';
            }

            if (ST2 >= 64)
            {
                valida = false;
                ST2 = ST2 - 64;
                Erros += "BIT 6 - Memória Fiscal Lotada" + '\x0D';
            }
            if (ST2 >= 32)
            {
                valida = false;
                ST2 = ST2 - 32;
                Erros += "BIT 5 - CMOS não Volátil" + '\x0D';
            }
            if (ST2 >= 16)
            {
                valida = false;
                ST2 = ST2 - 16;
                Erros += "BIT 4 - Alíquota Não Programada" + '\x0D';
            }
            if (ST2 >= 8)
            {
                valida = false;
                ST2 = ST2 - 8;
                Erros += "BIT 3 - Alíquotas lotadas" + '\x0D';
            }
            if (ST2 >= 4)
            {
                valida = false;
                ST2 = ST2 - 4;
                Erros += "BIT 2 - Cancelamento ñ Permitido" + '\x0D';
            }
            if (ST2 >= 2)
            {
                valida = false;
                ST2 = ST2 - 2;
                Erros += "BIT 1 - CGC/IE não Programados" + '\x0D';
            }
            if (ST2 >= 1)
            {
                ST2 = ST2 - 1;
                Erros += "BIT 0 - Comando não Executado" + '\x0D';
            }
            if (Erros != "")
            {
                stringST2 = Erros;
                Erros = "";
            }
            #endregion

            #region

            if (ST3 >= 66)
            {
                valida = false;
                ST3 = ST3 - 66;
                Erros = "";
                Erros = "ECF Aguardando Redução Z" + '\x0D';
            }else
            if (ST3 >= 12)
            {
                valida = false;
                ST3 = ST3 - 12;
                Erros = "";
                Erros = "IMPRESSORA COM CABEÇA LEVANTADA" + '\x0D';
            }else
            if (ST3 >= 2)
            {
                valida = false;
                ST3 = ST3 - 2;
                Erros += "ERRO DESCONHECIDO" + '\x0D';
            }
            else
             /*if (ST3 >= 0)
            {
                ST3 = ST3 - 0;
                Erros = "";
                Erros = "COMANDO OK" + '\x0D';
            }else*/
            if (ST3 >= 1)
            {
                ST3 = ST3 - 1;
                Erros = "";
                Erros = "COMANDO INVÁLIDO" + '\x0D';
            }else


if (ST3 >= 2)
            {
                ST3 = ST3 - 2;
                Erros = "";
                Erros = "ERRO DESCONHECIDO" + '\x0D';
            }else

 if (ST3 >= 3)
            {
                ST3 = ST3 - 3;
                Erros = "";
                Erros = "NÚMERO DE PARÂMETRO INVÁLIDO" + '\x0D';
            }else



if (ST3 >= 4)
            {
                ST3 = ST3 - 4;
                Erros = "";
                Erros = "TIPO DE PARÂMETRO INVÁLIDO" + '\x0D';
            }else

 if (ST3 >= 5)
            {
                ST3 = ST3 - 5;
                Erros = "";
                Erros = "TODAS ALÍQUOTAS JÁ PROGRAMADAS" + '\x0D';
            }else

if (ST3 >= 6)
            {
                ST3 = ST3 - 6;
                Erros = "";
                Erros = "TOTALIZADOR NÃO FISCAL JÁ PROGRAMADO" + '\x0D';
            }else

if (ST3 >= 7)
            {
                ST3 = ST3 - 7;
                Erros = "";
                Erros = "CUPOM FISCAL ABERTO" + '\x0D';
            }else

if (ST3 >= 8)
            {
                ST3 = ST3 - 8;
                Erros = "";
                Erros = "CUPOM FISCAL FECHADO" + '\x0D';
            }else

if (ST3 >= 9)
            {
                ST3 = ST3 - 9;
                Erros = "";
                Erros = "ECF OCUPADO" + '\x0D';
            }else

if (ST3 >= 10)
            {
                ST3 = ST3 - 10;
                Erros = "";
                Erros = "IMPRESSORA EM ERRO" + '\x0D';
            }else

if (ST3 >= 11)
            {
                ST3 = ST3 - 11;
                Erros = "";
                Erros = "IMPRESSORA SEM PAPEL" + '\x0D';
            }else

if (ST3 >= 12)
            {
                ST3 = ST3 - 12;
                Erros = "";
                Erros = "IMPRESSORA COM CABEÇA LEVANTADA" + '\x0D';
            }else

if (ST3 >= 13)
            {
                ST3 = ST3 - 13;
                Erros = "";
                Erros = "IMPRESSORA OFF LINE" + '\x0D';
            }else

if (ST3 >= 14)
            {
                ST3 = ST3 - 14;
                Erros = "";
                Erros = "ALÍQUOTA NÃO PROGRAMADA" + '\x0D';
            }else

if (ST3 >= 15)
            {
                ST3 = ST3 - 15;
                Erros = "";
                Erros = "TERMINADOR DE STRING FALTANDO" + '\x0D';
            }else

if (ST3 >= 16)
            {
                ST3 = ST3 - 16;
                Erros = "";
                Erros = "ACRÉSCIMO OU DESCONTO MAIOR QUE O TOTAL DO CUPOM FISCAL" + '\x0D';
            }else

if (ST3 >= 17)
            {
                ST3 = ST3 - 17;
                Erros = "";
                Erros = "CUPOM FISCAL SEM ITEM VENDIDO" + '\x0D';
            }else


if (ST3 >= 18)
            {
                ST3 = ST3 - 18;
                Erros = "";
                Erros = "COMANDO NÃO EFETIVADO" + '\x0D';
            }else


    if (ST3 >= 19)
    {
        ST3 = ST3 - 19;
        Erros = "";
        Erros = "SEM ESPAÇO PARA NOVAS FORMAS DE PAGAMENTO" + '\x0D';
    }
    else


        if (ST3 >= 20)
        {
            ST3 = ST3 - 20;
            Erros = "";
            Erros = "FORMA DE PAGAMENTO NÃO PROGRAMADA" + '\x0D';
        }
        else

            if (ST3 >= 21)
            {
                ST3 = ST3 - 21;
                Erros = "";
                Erros = "ÍNDICE MAIOR QUE NÚMERO DE FORMA DE PAGAMENTO" + '\x0D';
            }
            else


                if (ST3 >= 22)
                {
                    ST3 = ST3 - 22;
                    Erros = "";
                    Erros = "FORMAS DE PAGAMENTO ENCERRADAS" + '\x0D';
                }
                else


                    if (ST3 >= 23)
                    {
                        ST3 = ST3 - 23;
                        Erros = "";
                        Erros = "CUPOM NÃO TOTALIZADO" + '\x0D';
                    }
                    else


                        if (ST3 >= 24)
                        {
                            ST3 = ST3 - 24;
                            Erros = "";
                            Erros = "COMANDO MAIOR QUE 7Fh (127d)" + '\x0D';
                        }
                        else



                            if (ST3 >= 25)
                            {
                                ST3 = ST3 - 25;
                                Erros = "";
                                Erros = "CUPOM FISCAL ABERTO E SEM ÍTEM" + '\x0D';
                            }
                            else



                                if (ST3 >= 26)
                                {
                                    ST3 = ST3 - 26;
                                    Erros = "";
                                    Erros = "CANCELAMENTO NÃO IMEDIATAMENTE APÓS" + '\x0D';
                                }
                                else



                                    if (ST3 >= 27)
                                    {
                                        ST3 = ST3 - 27;
                                        Erros = "";
                                        Erros = "CANCELAMENTO JÁ EFETUADO" + '\x0D';
                                    }
                                    else


                                        if (ST3 >= 28)
                                        {
                                            ST3 = ST3 - 28;
                                            Erros = "";
                                            Erros = "COMPROVANTE DE CRÉDITO OU DÉBITO NÃO PERMITIDO OU JÁ EMITIDO" + '\x0D';
                                        }
                                        else


                                            if (ST3 >= 29)
                                            {
                                                ST3 = ST3 - 29;
                                                Erros = "";
                                                Erros = "MEIO DE PAGAMENTO NÃO PERMITE TEF" + '\x0D';
                                            }
                                            else


                                                if (ST3 >= 30)
                                                {
                                                    ST3 = ST3 - 30;
                                                    Erros = "";
                                                    Erros = "SEM COMPROVANTE NÃO FISCAL ABERTO" + '\x0D';
                                                }
                                                else


                                                    if (ST3 >= 31)
                                                    {
                                                        ST3 = ST3 - 31;
                                                        Erros = "";
                                                        Erros = "COMPROVANTE DE CRÉDITO OU DÉBITO JÁ ABERTO" + '\x0D';
                                                    }
                                                    else


                                                        if (ST3 >= 32)
                                                        {
                                                            ST3 = ST3 - 32;
                                                            Erros = "";
                                                            Erros = "REIMPRESSÃO NÃO PERMITIDA" + '\x0D';
                                                        }
                                                        else


                                                            if (ST3 >= 33)
                                                            {
                                                                ST3 = ST3 - 33;
                                                                Erros = "";
                                                                Erros = "COMPROVANTE NÃO FISCAL JÁ ABERTO" + '\x0D';
                                                            }
                                                            else

                                                                if (ST3 >= 34)
                                                                {
                                                                    ST3 = ST3 - 34;
                                                                    Erros = "";
                                                                    Erros = "TOTALIZADOR NÃO FISCAL NÃO PROGRAMADO" + '\x0D';
                                                                }
                                                                else

                                                                    if (ST3 >= 35)
                                                                    {
                                                                        ST3 = ST3 - 35;
                                                                        Erros = "";
                                                                        Erros = "CUPOM NÃO FISCAL SEM ÍTEM VENDIDO" + '\x0D';
                                                                    }
                                                                    else

                                                                        if (ST3 >= 36)
                                                                        {
                                                                            ST3 = ST3 - 36;
                                                                            Erros = "";
                                                                            Erros = "ACRÉSCIMO E DESCONTO MAIOR QUE TOTAL CNF" + '\x0D';
                                                                        }
                                                                        else

                                                                            if (ST3 >= 37)
                                                                            {
                                                                                ST3 = ST3 - 37;
                                                                                Erros = "";
                                                                                Erros = "MEIO DE PAGAMENTO NÃO INDICADO" + '\x0D';
                                                                            }
                                                                            else

                                                                                if (ST3 >= 38)
                                                                                {
                                                                                    ST3 = ST3 - 38;
                                                                                    Erros = "";
                                                                                    Erros = "MEIO DE PAGAMENTO DIFERENTE DO TOTAL DO RECEBIMENTO" + '\x0D';
                                                                                }
                                                                                else

                                                                                    if (ST3 >= 39)
                                                                                    {
                                                                                        ST3 = ST3 - 39;
                                                                                        Erros = "";
                                                                                        Erros = "NÃO PERMITIDO MAIS DE UMA SANGRIA OU SUPRIMENTO" + '\x0D';
                                                                                    }
                                                                                    else

                                                                                        if (ST3 >= 40)
                                                                                        {
                                                                                            ST3 = ST3 - 40;
                                                                                            Erros = "";
                                                                                            Erros = "RELATÓRIO GERENCIAL JÁ PROGRAMADO" + '\x0D';
                                                                                        }
                                                                                        else

                                                                                            if (ST3 >= 41)
                                                                                            {
                                                                                                ST3 = ST3 - 41;
                                                                                                Erros = "";
                                                                                                Erros = "RELATÓRIO GERENCIAL NÃO PROGRAMADO" + '\x0D';
                                                                                            }
                                                                                            else

                                                                                                if (ST3 >= 42)
                                                                                                {
                                                                                                    ST3 = ST3 - 42;
                                                                                                    Erros = "";
                                                                                                    Erros = "RELATÓRIO GERENCIAL NÃO PERMITIDO" + '\x0D';
                                                                                                }
                                                                                                else

                                                                                                    if (ST3 >= 43)
                                                                                                    {
                                                                                                        ST3 = ST3 - 43;
                                                                                                        Erros = "";
                                                                                                        Erros = "MFD NÃO INICIALIZADA" + '\x0D';
                                                                                                    }
                                                                                                    else


                                                                                                        if (ST3 >= 44)
                                                                                                        {
                                                                                                            ST3 = ST3 - 44;
                                                                                                            Erros = "";
                                                                                                            Erros = "MFD AUSENTE" + '\x0D';
                                                                                                        }
                                                                                                        else

                                                                                                            if (ST3 >= 45)
                                                                                                            {
                                                                                                                ST3 = ST3 - 45;
                                                                                                                Erros = "";
                                                                                                                Erros = "MFD SEM NÚMERO DE SÉRIE" + '\x0D';
                                                                                                            }
                                                                                                            else

                                                                                                                if (ST3 >= 46)
                                                                                                                {
                                                                                                                    ST3 = ST3 - 46;
                                                                                                                    Erros = "";
                                                                                                                    Erros = "MFD JÁ INICIALIZADA" + '\x0D';
                                                                                                                }
                                                                                                                else

                                                                                                                    if (ST3 >= 47)
                                                                                                                    {
                                                                                                                        ST3 = ST3 - 47;
                                                                                                                        Erros = "";
                                                                                                                        Erros = "MFD LOTADA" + '\x0D';
                                                                                                                    }
                                                                                                                    else

                                                                                                                        if (ST3 >= 48)
                                                                                                                        {
                                                                                                                            ST3 = ST3 - 48;
                                                                                                                            Erros = "";
                                                                                                                            Erros = "CUPOM NÃO FISCAL ABERTO" + '\x0D';
                                                                                                                        }
                                                                                                                        else

                                                                                                                            if (ST3 >= 49)
                                                                                                                            {
                                                                                                                                ST3 = ST3 - 49;
                                                                                                                                Erros = "";
                                                                                                                                Erros = "MEMÓRIA FISCAL DESCONECTADA" + '\x0D';
                                                                                                                            }
                                                                                                                            else

                                                                                                                                if (ST3 >= 50)
                                                                                                                                {
                                                                                                                                    ST3 = ST3 - 50;
                                                                                                                                    Erros = "";
                                                                                                                                    Erros = "MEMÓRIA FISCAL SEM NÚMERO DE SÉRIE DA MFD" + '\x0D';
                                                                                                                                }
                                                                                                                                else

                                                                                                                                    if (ST3 >= 51)
                                                                                                                                    {
                                                                                                                                        ST3 = ST3 - 51;
                                                                                                                                        Erros = "";
                                                                                                                                        Erros = "MEMÓRIA FISCAL LOTADA" + '\x0D';
                                                                                                                                    }
                                                                                                                                    else


                                                                                                                                        if (ST3 >= 52)
                                                                                                                                        {
                                                                                                                                            ST3 = ST3 - 52;
                                                                                                                                            Erros = "";
                                                                                                                                            Erros = "DATA INICIAL INVÁLIDA" + '\x0D';
                                                                                                                                        }
                                                                                                                                        else


                                                                                                                                            if (ST3 >= 53)
                                                                                                                                            {
                                                                                                                                                ST3 = ST3 - 53;
                                                                                                                                                Erros = "";
                                                                                                                                                Erros = "DATA FINAL INVÁLIDA" + '\x0D';
                                                                                                                                            }
                                                                                                                                            else


                                                                                                                                                if (ST3 >= 54)
                                                                                                                                                {
                                                                                                                                                    ST3 = ST3 - 54;
                                                                                                                                                    Erros = "";
                                                                                                                                                    Erros = "CONTADOR DE REDUÇÃO Z INICIAL INVÁLIDO" + '\x0D';
                                                                                                                                                }
                                                                                                                                                else

                                                                                                                                                    if (ST3 >= 55)
                                                                                                                                                    {
                                                                                                                                                        ST3 = ST3 - 55;
                                                                                                                                                        Erros = "";
                                                                                                                                                        Erros = "CONTADOR DE REDUÇÃO Z FINAL INVÁLIDO" + '\x0D';
                                                                                                                                                    }
                                                                                                                                                    else


                                                                                                                                                        if (ST3 >= 56)
                                                                                                                                                        {
                                                                                                                                                            ST3 = ST3 - 56;
                                                                                                                                                            Erros = "";
                                                                                                                                                            Erros = "ERRO DE ALOCAÇÃO" + '\x0D';
                                                                                                                                                        }
                                                                                                                                                        else


                                                                                                                                                            if (ST3 >= 57)
                                                                                                                                                            {
                                                                                                                                                                ST3 = ST3 - 57;
                                                                                                                                                                Erros = "";
                                                                                                                                                                Erros = "DADOS DO RTC INCORRETOS" + '\x0D';
                                                                                                                                                            }
                                                                                                                                                            else



                                                                                                                                                                if (ST3 >= 58)
                                                                                                                                                                {
                                                                                                                                                                    ST3 = ST3 - 58;
                                                                                                                                                                    Erros = "";
                                                                                                                                                                    Erros = "DATA ANTERIOR AO ÚLTIMO DOCUMENTO EMITIDO" + '\x0D';
                                                                                                                                                                }
                                                                                                                                                                else



                                                                                                                                                                    if (ST3 >= 59)
                                                                                                                                                                    {
                                                                                                                                                                        ST3 = ST3 - 59;
                                                                                                                                                                        Erros = "";
                                                                                                                                                                        Erros = "FORA DE INTERVENÇÃO TÉCNICA" + '\x0D';
                                                                                                                                                                    }
                                                                                                                                                                    else



                                                                                                                                                                        if (ST3 >= 60)
                                                                                                                                                                        {
                                                                                                                                                                            ST3 = ST3 - 60;
                                                                                                                                                                            Erros = "";
                                                                                                                                                                            Erros = "EM INTERVENÇÃO TÉCNICA" + '\x0D';
                                                                                                                                                                        }
                                                                                                                                                                        else



                                                                                                                                                                            if (ST3 >= 61)
                                                                                                                                                                            {
                                                                                                                                                                                ST3 = ST3 - 61;
                                                                                                                                                                                Erros = "";
                                                                                                                                                                                Erros = "ERRO NA MEMÓRIA DE TRABALHO" + '\x0D';
                                                                                                                                                                            }
                                                                                                                                                                            else



                                                                                                                                                                                if (ST3 >= 62)
                                                                                                                                                                                {
                                                                                                                                                                                    ST3 = ST3 - 62;
                                                                                                                                                                                    Erros = "";
                                                                                                                                                                                    Erros = "JÁ HOUVE MOVIMENTO NO DIA" + '\x0D';
                                                                                                                                                                                }
                                                                                                                                                                                else



                                                                                                                                                                                    if (ST3 >= 63)
                                                                                                                                                                                    {
                                                                                                                                                                                        ST3 = ST3 - 63;
                                                                                                                                                                                        Erros = "";
                                                                                                                                                                                        Erros = "BLOQUEIO POR RZ" + '\x0D';
                                                                                                                                                                                    }
                                                                                                                                                                                    else



                                                                                                                                                                                        if (ST3 >= 64)
                                                                                                                                                                                        {
                                                                                                                                                                                            ST3 = ST3 - 64;
                                                                                                                                                                                            Erros = "";
                                                                                                                                                                                            Erros = "FORMA DE PAGAMENTO ABERTA" + '\x0D';
                                                                                                                                                                                        }
                                                                                                                                                                                        else



                                                                                                                                                                                            if (ST3 >= 65)
                                                                                                                                                                                            {
                                                                                                                                                                                                ST3 = ST3 - 65;
                                                                                                                                                                                                Erros = "";
                                                                                                                                                                                                Erros = "AGUARDANDO PRIMEIRO PROPRIETÁRIO" + '\x0D';
                                                                                                                                                                                            }
                                                                                                                                                                                            else



                                                                                                                                                                                                if (ST3 >= 66)
                                                                                                                                                                                                {
                                                                                                                                                                                                    ST3 = ST3 - 66;
                                                                                                                                                                                                    Erros = "";
                                                                                                                                                                                                    Erros = "AGUARDANDO RZ" + '\x0D';
                                                                                                                                                                                                }
                                                                                                                                                                                                else


                                                                                                                                                                                                    if (ST3 >= 67)
                                                                                                                                                                                                    {
                                                                                                                                                                                                        ST3 = ST3 - 67;
                                                                                                                                                                                                        Erros = "";
                                                                                                                                                                                                        Erros = "ECF OU LOJA IGUAL A ZERO" + '\x0D';
                                                                                                                                                                                                    }
                                                                                                                                                                                                    else



                                                                                                                                                                                                        if (ST3 >= 68)
                                                                                                                                                                                                        {
                                                                                                                                                                                                            ST3 = ST3 - 68;
                                                                                                                                                                                                            Erros = "";
                                                                                                                                                                                                            Erros = "CUPOM ADICIONAL NÃO PERMITIDO" + '\x0D';
                                                                                                                                                                                                        }
                                                                                                                                                                                                        else



                                                                                                                                                                                                            if (ST3 >= 69)
                                                                                                                                                                                                            {
                                                                                                                                                                                                                ST3 = ST3 - 69;
                                                                                                                                                                                                                Erros = "";
                                                                                                                                                                                                                Erros = "DESCONTO MAIOR QUE TOTAL VENDIDO EM ICMS" + '\x0D';
                                                                                                                                                                                                            }
                                                                                                                                                                                                            else



                                                                                                                                                                                                                if (ST3 >= 70)
                                                                                                                                                                                                                {
                                                                                                                                                                                                                    ST3 = ST3 - 70;
                                                                                                                                                                                                                    Erros = "";
                                                                                                                                                                                                                    Erros = "RECEBIMENTO NÃO FISCAL NULO NÃO PERMITIDO" + '\x0D';
                                                                                                                                                                                                                }
                                                                                                                                                                                                                else



                                                                                                                                                                                                                    if (ST3 >= 71)
                                                                                                                                                                                                                    {
                                                                                                                                                                                                                        ST3 = ST3 - 71;
                                                                                                                                                                                                                        Erros = "";
                                                                                                                                                                                                                        Erros = "ACRÉSCIMO OU DESCONTO MAIOR QUE TOTAL NÃO FISCAL" + '\x0D';
                                                                                                                                                                                                                    }
                                                                                                                                                                                                                    else



                                                                                                                                                                                                                        if (ST3 >= 72)
                                                                                                                                                                                                                        {
                                                                                                                                                                                                                            ST3 = ST3 - 72;
                                                                                                                                                                                                                            Erros = "";
                                                                                                                                                                                                                            Erros = "MEMÓRIA FISCAL LOTADA PARA NOVO CARTUCHO" + '\x0D';
                                                                                                                                                                                                                        }
                                                                                                                                                                                                                        else




                                                                                                                                                                                                                            if (ST3 >= 73)
                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                ST3 = ST3 - 73;
                                                                                                                                                                                                                                Erros = "";
                                                                                                                                                                                                                                Erros = "ERRO DE GRAVAÇÃO NA MF" + '\x0D';
                                                                                                                                                                                                                            }
                                                                                                                                                                                                                            else


                                                                                                                                                                                                                                if (ST3 >= 74)
                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                    ST3 = ST3 - 74;
                                                                                                                                                                                                                                    Erros = "";
                                                                                                                                                                                                                                    Erros = "ERRO DE GRAVAÇÃO NA MFD" + '\x0D';
                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                else


                                                                                                                                                                                                                                    if (ST3 >= 75)
                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                        ST3 = ST3 - 75;
                                                                                                                                                                                                                                        Erros = "";
                                                                                                                                                                                                                                        Erros = "DADOS DO RTC ANTERIORES AO ÚLTIMO DOC ARMAZENADO" + '\x0D';
                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                    else


                                                                                                                                                                                                                                        if (ST3 >= 76)
                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                            ST3 = ST3 - 76;
                                                                                                                                                                                                                                            Erros = "";
                                                                                                                                                                                                                                            Erros = "MEMÓRIA FISCAL SEM ESPAÇO PARA GRAVAR LEITURAS DA MFD" + '\x0D';
                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                        else

                                                                                                                                                                                                                                            if (ST3 >= 77)
                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                ST3 = ST3 - 77;
                                                                                                                                                                                                                                                Erros = "";
                                                                                                                                                                                                                                                Erros = "MEMÓRIA FISCAL SEM ESPAÇO PARA GRAVAR VERSAO DO SB" + '\x0D';
                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                            else


                                                                                                                                                                                                                                                if (ST3 >= 78)
                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                    ST3 = ST3 - 78;
                                                                                                                                                                                                                                                    Erros = "";
                                                                                                                                                                                                                                                    Erros = "DESCRIÇÃO IGUAL A DEFAULT NÃO PERMITIDO" + '\x0D';
                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                else


                                                                                                                                                                                                                                                    if (ST3 >= 79)
                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                        ST3 = ST3 - 79;
                                                                                                                                                                                                                                                        Erros = "";
                                                                                                                                                                                                                                                        Erros = "EXTRAPOLADO NÚMERO DE REPETIÇÕES PERMITIDAS" + '\x0D';
                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                    else


                                                                                                                                                                                                                                                        if (ST3 >= 80)
                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                            ST3 = ST3 - 80;
                                                                                                                                                                                                                                                            Erros = "";
                                                                                                                                                                                                                                                            Erros = "SEGUNDA VIA DO COMPROVANTE DE CRÉDITO OU DÉBITO NÃO PERMITIDO" + '\x0D';
                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                        else


                                                                                                                                                                                                                                                            if (ST3 >= 81)
                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                ST3 = ST3 - 81;
                                                                                                                                                                                                                                                                Erros = "";
                                                                                                                                                                                                                                                                Erros = "PARCELAMENTO FORA DA SEQUÊNCIA" + '\x0D';
                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                            else


                                                                                                                                                                                                                                                                if (ST3 >= 82)
                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                    ST3 = ST3 - 82;
                                                                                                                                                                                                                                                                    Erros = "";
                                                                                                                                                                                                                                                                    Erros = "COMPROVANTE DE CRÉDITO OU DÉBITO ABERTO" + '\x0D';
                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                else


                                                                                                                                                                                                                                                                    if (ST3 >= 83)
                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                        ST3 = ST3 - 83;
                                                                                                                                                                                                                                                                        Erros = "";
                                                                                                                                                                                                                                                                        Erros = "TEXTO COM SEQUÊNCIA DE ESC INVÁLIDA" + '\x0D';
                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                    else


                                                                                                                                                                                                                                                                        if (ST3 >= 84)
                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                            ST3 = ST3 - 84;
                                                                                                                                                                                                                                                                            Erros = "";
                                                                                                                                                                                                                                                                            Erros = "TEXTO COM SEQUÊNCIA DE ESC INCOMPLETA" + '\x0D';
                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                        else


                                                                                                                                                                                                                                                                            if (ST3 >= 85)
                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                ST3 = ST3 - 85;
                                                                                                                                                                                                                                                                                Erros = "";
                                                                                                                                                                                                                                                                                Erros = "VENDA COM VALOR NULO" + '\x0D';
                                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                                            else


                                                                                                                                                                                                                                                                                if (ST3 >= 86)
                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                    ST3 = ST3 - 86;
                                                                                                                                                                                                                                                                                    Erros = "";
                                                                                                                                                                                                                                                                                    Erros = "ESTORNO DE VALOR NULO" + '\x0D';
                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                                else


                                                                                                                                                                                                                                                                                    if (ST3 >= 87)
                                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                                        ST3 = ST3 - 87;
                                                                                                                                                                                                                                                                                        Erros = "";
                                                                                                                                                                                                                                                                                        Erros = "FORMA DE PAGAMENTO DIFERENTE DO TOTAL DA SANGRIA" + '\x0D';
                                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                                    else


                                                                                                                                                                                                                                                                                        if (ST3 >= 88)
                                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                                            ST3 = ST3 - 88;
                                                                                                                                                                                                                                                                                            Erros = "";
                                                                                                                                                                                                                                                                                            Erros = "REDUÇÃO NÃO PERMITIDA EM INTERVENÇÃO TÉCNICA" + '\x0D';
                                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                                        else


                                                                                                                                                                                                                                                                                            if (ST3 >= 89)
                                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                                ST3 = ST3 - 89;
                                                                                                                                                                                                                                                                                                Erros = "";
                                                                                                                                                                                                                                                                                                Erros = "AGUARDANDO RZ PARA ENTRADA EM INTERVENÇÃO TÉCNICA" + '\x0D';
                                                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                                                            else


                                                                                                                                                                                                                                                                                                if (ST3 >= 90)
                                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                                    ST3 = ST3 - 90;
                                                                                                                                                                                                                                                                                                    Erros = "";
                                                                                                                                                                                                                                                                                                    Erros = "FORMA DE PAGAMENTO COM VALOR NULO NÃO PERMITIDO" + '\x0D';
                                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                                                else


                                                                                                                                                                                                                                                                                                    if (ST3 >= 91)
                                                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                                                        ST3 = ST3 - 91;
                                                                                                                                                                                                                                                                                                        Erros = "";
                                                                                                                                                                                                                                                                                                        Erros = "ACRÉSCIMO E DESCONTO MAIOR QUE VALOR DO ÍTEM" + '\x0D';
                                                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                                                    else


                                                                                                                                                                                                                                                                                                        if (ST3 >= 92)
                                                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                                                            ST3 = ST3 - 92;
                                                                                                                                                                                                                                                                                                            Erros = "";
                                                                                                                                                                                                                                                                                                            Erros = "AUTENTICAÇÃO NÃO PERMITIDA" + '\x0D';
                                                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                                                        else


                                                                                                                                                                                                                                                                                                            if (ST3 >= 93)
                                                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                                                ST3 = ST3 - 93;
                                                                                                                                                                                                                                                                                                                Erros = "";
                                                                                                                                                                                                                                                                                                                Erros = "TIMEOUT NA VALIDAÇÃO" + '\x0D';
                                                                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                                                                            else


                                                                                                                                                                                                                                                                                                                if (ST3 >= 94)
                                                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                                                    ST3 = ST3 - 94;
                                                                                                                                                                                                                                                                                                                    Erros = "";
                                                                                                                                                                                                                                                                                                                    Erros = "COMANDO NÃO EXECUTADO EM IMPRESSORA BILHETE DE PASSAGEM" + '\x0D';
                                                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                                                                else


                                                                                                                                                                                                                                                                                                                    if (ST3 >= 95)
                                                                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                                                                        ST3 = ST3 - 95;
                                                                                                                                                                                                                                                                                                                        Erros = "";
                                                                                                                                                                                                                                                                                                                        Erros = "COMANDO NÃO EXECUTADO EM IMPRESSORA DE CUPOM FISCAL" + '\x0D';
                                                                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                                                                    else


                                                                                                                                                                                                                                                                                                                        if (ST3 >= 96)
                                                                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                                                                            ST3 = ST3 - 96;
                                                                                                                                                                                                                                                                                                                            Erros = "";
                                                                                                                                                                                                                                                                                                                            Erros = "CUPOM NÃO FISCAL FECHADO" + '\x0D';
                                                                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                                                                        else


                                                                                                                                                                                                                                                                                                                            if (ST3 >= 97)
                                                                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                                                                ST3 = ST3 - 97;
                                                                                                                                                                                                                                                                                                                                Erros = "";
                                                                                                                                                                                                                                                                                                                                Erros = "PARÂMETRO NÃO ASCII EM CAMPO ASCII" + '\x0D';
                                                                                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                                                                                            else


                                                                                                                                                                                                                                                                                                                                if (ST3 >= 98)
                                                                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                                                                    ST3 = ST3 - 98;
                                                                                                                                                                                                                                                                                                                                    Erros = "";
                                                                                                                                                                                                                                                                                                                                    Erros = "PARÂMETRO NÃO ASCII NUMÉRICO EM CAMPO ASCII NUMÉRICO" + '\x0D';
                                                                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                                                                                else


                                                                                                                                                                                                                                                                                                                                    if (ST3 >= 99)
                                                                                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                                                                                        ST3 = ST3 - 99;
                                                                                                                                                                                                                                                                                                                                        Erros = "";
                                                                                                                                                                                                                                                                                                                                        Erros = "TIPO DE TRANSPORTE INVÁLIDO" + '\x0D';
                                                                                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                                                                                    else


                                                                                                                                                                                                                                                                                                                                        if (ST3 >= 100)
                                                                                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                                                                                            ST3 = ST3 - 100;
                                                                                                                                                                                                                                                                                                                                            Erros = "";
                                                                                                                                                                                                                                                                                                                                            Erros = "DATA E HORA INVÁLIDA" + '\x0D';
                                                                                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                                                                                        else


                                                                                                                                                                                                                                                                                                                                            if (ST3 >= 101)
                                                                                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                                                                                ST3 = ST3 - 101;
                                                                                                                                                                                                                                                                                                                                                Erros = "";
                                                                                                                                                                                                                                                                                                                                                Erros = "SEM RELATÓRIO GERENCIAL OU COMPROVANTE DE CRÉDITO OU DÉBITO ABERTO" + '\x0D';
                                                                                                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                                                                                                            else



                                                                                                                                                                                                                                                                                                                                                if (ST3 >= 102)
                                                                                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                                                                                    ST3 = ST3 - 102;
                                                                                                                                                                                                                                                                                                                                                    Erros = "";
                                                                                                                                                                                                                                                                                                                                                    Erros = "NÚMERO DO TOTALIZADOR NÃO FISCAL INVÁLIDO" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                                                                                                else


                                                                                                                                                                                                                                                                                                                                                    if (ST3 >= 103)
                                                                                                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                                                                                                        ST3 = ST3 - 103;
                                                                                                                                                                                                                                                                                                                                                        Erros = "";
                                                                                                                                                                                                                                                                                                                                                        Erros = "PARÂMETRO DE ACRÉSCIMO OU DESCONTO INVÁLIDO" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                                                                                                    else


                                                                                                                                                                                                                                                                                                                                                        if (ST3 >= 104)
                                                                                                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                                                                                                            ST3 = ST3 - 104;
                                                                                                                                                                                                                                                                                                                                                            Erros = "";
                                                                                                                                                                                                                                                                                                                                                            Erros = "ACRÉSCIMO OU DESCONTO EM SANGRIA OU SUPRIMENTO NÃO PERMITIDO" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                                                                                                        else


                                                                                                                                                                                                                                                                                                                                                            if (ST3 >= 105)
                                                                                                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                                                                                                ST3 = ST3 - 105;
                                                                                                                                                                                                                                                                                                                                                                Erros = "";
                                                                                                                                                                                                                                                                                                                                                                Erros = "NÚMERO DO RELATÓRIO GERENCIAL INVÁLIDO" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                                                                                                                            else


                                                                                                                                                                                                                                                                                                                                                                if (ST3 >= 106)
                                                                                                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                                                                                                    ST3 = ST3 - 106;
                                                                                                                                                                                                                                                                                                                                                                    Erros = "";
                                                                                                                                                                                                                                                                                                                                                                    Erros = "FORMA DE PAGAMENTO ORIGEM NÃO PROGRAMADA" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                                                                                                                else


                                                                                                                                                                                                                                                                                                                                                                    if (ST3 >= 107)
                                                                                                                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                                                                                                                        ST3 = ST3 - 107;
                                                                                                                                                                                                                                                                                                                                                                        Erros = "";
                                                                                                                                                                                                                                                                                                                                                                        Erros = "FORMA DE PAGAMENTO DESTINO NÃO PROGRAMADA" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                                                                                                                    else



                                                                                                                                                                                                                                                                                                                                                                        if (ST3 >= 108)
                                                                                                                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                                                                                                                            ST3 = ST3 - 108;
                                                                                                                                                                                                                                                                                                                                                                            Erros = "";
                                                                                                                                                                                                                                                                                                                                                                            Erros = "ESTORNO MAIOR QUE FORMA PAGAMENTO" + '\x0D';

                                                                                                                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                                                                                                                        else

                                                                                                                                                                                                                                                                                                                                                                            if (ST3 >= 109)
                                                                                                                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                                                                                                                ST3 = ST3 - 109;
                                                                                                                                                                                                                                                                                                                                                                                Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                Erros = "CARACTER NUMÉRICO NA CODIFICAÇÃO GT NÃO PERMITIDO" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                                                                                                                                            else



                                                                                                                                                                                                                                                                                                                                                                                if (ST3 >= 110)
                                                                                                                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                                                                                                                    ST3 = ST3 - 110;
                                                                                                                                                                                                                                                                                                                                                                                    Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                    Erros = "ERRO NA INICIALIZAÇÃO DA MF" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                                                                                                                                else



                                                                                                                                                                                                                                                                                                                                                                                    if (ST3 >= 111)
                                                                                                                                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                                                                                                                                        ST3 = ST3 - 111;
                                                                                                                                                                                                                                                                                                                                                                                        Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                        Erros = "NOME DO TOTALIZADOR EM BRANCO NÃO PERMITIDO" + '\x0D';


                                                                                                                                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                                                                                                                                    else

                                                                                                                                                                                                                                                                                                                                                                                        if (ST3 >= 112)
                                                                                                                                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                                                                                                                                            ST3 = ST3 - 112;
                                                                                                                                                                                                                                                                                                                                                                                            Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                            Erros = "DATA E HORA ANTERIORES AO ÚLTIMO DOC ARMAZENADO" + '\x0D';

                                                                                                                                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                                                                                                                                        else

                                                                                                                                                                                                                                                                                                                                                                                            if (ST3 >= 113)
                                                                                                                                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                                                                                                                                ST3 = ST3 - 113;
                                                                                                                                                                                                                                                                                                                                                                                                Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                Erros = "PARÂMETRO DE ACRÉSCIMO OU DESCONTO INVÁLIDO" + '\x0D';

                                                                                                                                                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                                                                                                                                                            else


                                                                                                                                                                                                                                                                                                                                                                                                if (ST3 >= 114)
                                                                                                                                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                                                                                                                                    ST3 = ST3 - 114;
                                                                                                                                                                                                                                                                                                                                                                                                    Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                    Erros = "ÍTEM ANTERIOR AOS TREZENTOS ÚLTIMOS" + '\x0D';


                                                                                                                                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                                                                                                                                                else


                                                                                                                                                                                                                                                                                                                                                                                                    if (ST3 >= 115)
                                                                                                                                                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                                                                                                                                                        ST3 = ST3 - 115;
                                                                                                                                                                                                                                                                                                                                                                                                        Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                        Erros = "ÍTEM NÃO EXISTE OU JÁ CANCELADO" + '\x0D';


                                                                                                                                                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                                                                                                                                                    else

                                                                                                                                                                                                                                                                                                                                                                                                        if (ST3 >= 116)
                                                                                                                                                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                                                                                                                                                            ST3 = ST3 - 116;
                                                                                                                                                                                                                                                                                                                                                                                                            Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                            Erros = "CÓDIGO COM ESPAÇOS NÃO PERMITIDO" + '\x0D';

                                                                                                                                                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                                                                                                                                                        else

                                                                                                                                                                                                                                                                                                                                                                                                            if (ST3 >= 117)
                                                                                                                                                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                                                                                                                                                ST3 = ST3 - 117;
                                                                                                                                                                                                                                                                                                                                                                                                                Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                Erros = "DESCRICAO SEM CARACTER ALFABÉTICO NÃO PERMITIDO" + '\x0D';

                                                                                                                                                                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                                                                                                                                                                            else

                                                                                                                                                                                                                                                                                                                                                                                                                if (ST3 >= 118)
                                                                                                                                                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                                                                                                                                                    ST3 = ST3 - 118;
                                                                                                                                                                                                                                                                                                                                                                                                                    Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                    Erros = "ACRÉSCIMO MAIOR QUE VALOR DO ÍTEM" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                                                                                                                                                                else


                                                                                                                                                                                                                                                                                                                                                                                                                    if (ST3 >= 119)
                                                                                                                                                                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                                                                                                                                                                        ST3 = ST3 - 119;
                                                                                                                                                                                                                                                                                                                                                                                                                        Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                        Erros = "DESCONTO MAIOR QUE VALOR DO ÍTEM" + '\x0D';

                                                                                                                                                                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                                                                                                                                                                    else

                                                                                                                                                                                                                                                                                                                                                                                                                        if (ST3 >= 120)
                                                                                                                                                                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                                                                                                                                                                            ST3 = ST3 - 120;
                                                                                                                                                                                                                                                                                                                                                                                                                            Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                            Erros = "DESCONTO EM ISS NÃO PERMITIDO" + '\x0D';


                                                                                                                                                                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                                                                                                                                                                        else

                                                                                                                                                                                                                                                                                                                                                                                                                            if (ST3 >= 121)
                                                                                                                                                                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                                                                                                                                                                ST3 = ST3 - 121;
                                                                                                                                                                                                                                                                                                                                                                                                                                Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                Erros = "ACRÉSCIMO EM ÍTEM JÁ EFETUADO" + '\x0D';

                                                                                                                                                                                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                                                                                                                                                                                            else


                                                                                                                                                                                                                                                                                                                                                                                                                                if (ST3 >= 122)
                                                                                                                                                                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                                                                                                                                                                    ST3 = ST3 - 122;
                                                                                                                                                                                                                                                                                                                                                                                                                                    Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                    Erros = "DESCONTO EM ÍTEM JÁ EFETUADO" + '\x0D';

                                                                                                                                                                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                                                                                                                                                                                else

                                                                                                                                                                                                                                                                                                                                                                                                                                    if (ST3 >= 123)
                                                                                                                                                                                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                                                                                                                                                                                        ST3 = ST3 - 123;
                                                                                                                                                                                                                                                                                                                                                                                                                                        Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                        Erros = "ERRO NA MEMÓRIA FISCAL CHAMAR CREDENCIADO" + '\x0D';

                                                                                                                                                                                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                                                                                                                                                                                    else


                                                                                                                                                                                                                                                                                                                                                                                                                                        if (ST3 >= 124)
                                                                                                                                                                                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                                                                                                                                                                                            ST3 = ST3 - 124;
                                                                                                                                                                                                                                                                                                                                                                                                                                            Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                            Erros = "AGUARDANDO GRAVAÇÃO NA MEMÓRIA FISCAL" + '\x0D';

                                                                                                                                                                                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                                                                                                                                                                                        else


                                                                                                                                                                                                                                                                                                                                                                                                                                            if (ST3 >= 125)
                                                                                                                                                                                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                                                                                                                                                                                ST3 = ST3 - 125;
                                                                                                                                                                                                                                                                                                                                                                                                                                                Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                Erros = "CARACTER REPETIDO NA CODIFICAÇÃO DO GT" + '\x0D';

                                                                                                                                                                                                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                                                                                                                                                                                                            else


                                                                                                                                                                                                                                                                                                                                                                                                                                                if (ST3 >= 126)
                                                                                                                                                                                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                                                                                                                                                                                    ST3 = ST3 - 126;
                                                                                                                                                                                                                                                                                                                                                                                                                                                    Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                    Erros = "VERSÃO JÁ GRAVADA NA MEMÓRIA FISCAL" + '\x0D';

                                                                                                                                                                                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                                                                                                                                                                                                else


                                                                                                                                                                                                                                                                                                                                                                                                                                                    if (ST3 >= 127)
                                                                                                                                                                                                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                                                                                                                                                                                                        ST3 = ST3 - 127;
                                                                                                                                                                                                                                                                                                                                                                                                                                                        Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                        Erros = "ESTOURO DE CAPACIDADE NO CHEQUE" + '\x0D';

                                                                                                                                                                                                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                                                                                                                                                                                                    else

                                                                                                                                                                                                                                                                                                                                                                                                                                                        if (ST3 >= 128)
                                                                                                                                                                                                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                                                                                                                                                                                                            ST3 = ST3 - 128;
                                                                                                                                                                                                                                                                                                                                                                                                                                                            Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                            Erros = "TIMEOUT NA LEITURA DO CHEQUE" + '\x0D';

                                                                                                                                                                                                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                                                                                                                                                                                                        else


                                                                                                                                                                                                                                                                                                                                                                                                                                                            if (ST3 >= 129)
                                                                                                                                                                                                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                ST3 = ST3 - 129;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                Erros = "MÊS INVÁLIDO" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                                                                                                                                                                                                                            else


                                                                                                                                                                                                                                                                                                                                                                                                                                                                if (ST3 >= 130)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                    ST3 = ST3 - 130;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                    Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                    Erros = "COORDENADA INVÁLIDA" + '\x0D';


                                                                                                                                                                                                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                else

                                                                                                                                                                                                                                                                                                                                                                                                                                                                    if (ST3 >= 131)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ST3 = ST3 - 131;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                        Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                        Erros = "SOBREPOSIÇÃO DE TEXTO" + '\x0D';

                                                                                                                                                                                                                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                    else
                                                                                                                                                                                                                                                                                                                                                                                                                                                                        if (ST3 >= 132)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                            ST3 = ST3 - 132;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                            Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                            Erros = "SOBREPOSIÇÃO DE TEXTO NO VALOR" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                        else

                                                                                                                                                                                                                                                                                                                                                                                                                                                                            if (ST3 >= 133)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                ST3 = ST3 - 133;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                Erros = "SOBREPOSIÇÃO DE TEXTO NO EXTENSO" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                            else

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                if (ST3 >= 134)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    ST3 = ST3 - 134;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    Erros = "SOBREPOSIÇÃO DE TEXTO NO FAVORECIDO" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                else


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    if (ST3 >= 135)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ST3 = ST3 - 135;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        Erros = "SOBREPOSIÇÃO DE TEXTO NA LOCALIDADE" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    else

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        if (ST3 >= 136)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            ST3 = ST3 - 136;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            Erros = "SOBREPOSIÇÃO DE TEXTO NO OPCIONAL" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        else

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            if (ST3 >= 137)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                ST3 = ST3 - 137;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                Erros = "SOBREPOSIÇÃO DE TEXTO NO DIA" + '\x0D';

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            else

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                if (ST3 >= 138)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    ST3 = ST3 - 138;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    Erros = "SOBREPOSIÇÃO DE TEXTO NO MÊS" + '\x0D';

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                else

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    if (ST3 >= 139)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ST3 = ST3 - 139;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        Erros = "SOBREPOSIÇÃO DE TEXTO NO ANO" + '\x0D';

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    else

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        if (ST3 >= 140)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            ST3 = ST3 - 140;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            Erros = "USANDO MFD DE OUTRO ECF" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        else

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            if (ST3 >= 141)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                ST3 = ST3 - 141;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                Erros = "PRIMEIRO DADO DIFERENTE DE ESC OU 1C" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            else


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                if (ST3 >= 142)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    ST3 = ST3 - 142;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    Erros = "NÃO PERMITIDO ALTERAR SEM INTERVENÇÃO TÉCNICA" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                else

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    if (ST3 >= 143)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ST3 = ST3 - 143;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        Erros = "DADOS DA ÚLTIMA RZ CORROMPIDOS" + '\x0D';

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    else

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        if (ST3 >= 144)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            ST3 = ST3 - 144;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            Erros = "COMANDO NÃO PERMITIDO NO MODO INICIALIZAÇÃO" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        else


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            if (ST3 >= 145)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                ST3 = ST3 - 145;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                Erros = "AGUARDANDO ACERTO DE RELÓGIO" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            else

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                if (ST3 >= 146)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    ST3 = ST3 - 146;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    Erros = "MFD JÁ INICIALIZADA PARA OUTRA MF" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                else

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    if (ST3 >= 147)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ST3 = ST3 - 147;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        Erros = "AGUARDANDO ACERTO DO RELÓGIO OU DESBLOQUEIO PELO TECLADO" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    else


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        if (ST3 >= 148)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            ST3 = ST3 - 148;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            Erros = "VALOR FORMA DE PAGAMENTO MAIOR QUE MÁXIMO PERMITIDO" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        else


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            if (ST3 >= 149)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                ST3 = ST3 - 149;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                Erros = "RAZÃO SOCIAL EM BRANCO" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            else


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                if (ST3 >= 150)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    ST3 = ST3 - 150;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    Erros = "NOME DE FANTASIA EM BRANCO" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                else


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    if (ST3 >= 151)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ST3 = ST3 - 151;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        Erros = "ENDEREÇO EM BRANCO" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    else


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        if (ST3 >= 152)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            ST3 = ST3 - 152;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            Erros = "ESTORNO DE CDC NÃO PERMITIDO" + '\x0D';

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        else

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            if (ST3 >= 153)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                ST3 = ST3 - 153;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                Erros = "DADOS DO PROPRIETÁRIO IGUAIS AO ATUAL" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            else


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                if (ST3 >= 154)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    ST3 = ST3 - 154;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    Erros = "ESTORNO DE FORMA DE PAGAMENTO NÃO PERMITIDO" + '\x0D';

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                else

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    if (ST3 >= 155)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ST3 = ST3 - 155;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        Erros = "DESCRIÇÃO FORMA DE PAGAMENTO IGUAL JÁ PROGRAMADA" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    else


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        if (ST3 >= 156)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            ST3 = ST3 - 156;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            Erros = "ACERTO DE HORÁRIO DE VERÃO SÓ IMEDIATAMENTE APÓS RZ" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        else


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            if (ST3 >= 157)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                ST3 = ST3 - 157;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                Erros = "IT NÃO PERMITIDA MF RESERVADA PARA RZ" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            else


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                if (ST3 >= 158)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    ST3 = ST3 - 158;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    Erros = "SENHA CNPJ INVÁLIDA" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                else


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    if (ST3 >= 159)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ST3 = ST3 - 159;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        Erros = "TIMEOUT NA INICIALIZAÇÃO DA NOVA MF" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    else


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        if (ST3 >= 160)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            ST3 = ST3 - 160;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            Erros = "NÃO ENCONTRADO DADOS NA MFD" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        else


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            if (ST3 >= 161)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                ST3 = ST3 - 161;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                Erros = "SANGRIA OU SUPRIMENTO DEVEM SER ÚNICOS NO CNF" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            else


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                if (ST3 >= 162)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    ST3 = ST3 - 162;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    Erros = "ÍNDICE DA FORMA DE PAGAMENTO NULO NÃO PERMITIDO" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                else


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    if (ST3 >= 163)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ST3 = ST3 - 163;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        Erros = "UF DESTINO INVÁLIDA" + '\x0D';

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    else

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        if (ST3 >= 164)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            ST3 = ST3 - 164;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            Erros = "TIPO DE TRANSPORTE INCOMPATÍVEL COM UF DESTINO" + '\x0D';


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        else

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            if (ST3 >= 165)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                ST3 = ST3 - 165;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                Erros = "DESCRIÇÃO DO PRIMEIRO ÍTEM DO BILHETE DE PASSAGEM DIFERENTE DE TARIFA" + '\x0D';

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            else

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                if (ST3 >= 166)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    ST3 = ST3 - 166;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    Erros = "AGUARDANDO IMPRESSÃO DE CHEQUE OU AUTENTICAÇÃO" + '\x0D';


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                else

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    if (ST3 >= 167)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ST3 = ST3 - 167;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        Erros = "NÃO PERMITIDO PROGRAMAÇAO CNPJ IE COM ESPAÇOS EM BRANCO" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    else


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        if (ST3 >= 168)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            ST3 = ST3 - 168;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            Erros = "NÃO PERMITIDO PROGRAMAÇÃO UF COM ESPAÇOS EM BRANCO" + '\x0D';

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        else

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            if (ST3 >= 169)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                ST3 = ST3 - 169;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                Erros = "NÚMERO DE IMPRESSÕES DA FITA DETALHE NESTA INTERVENÇÃO TÉCNICA ESGOTADO" + '\x0D';

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            else

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                if (ST3 >= 170)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    ST3 = ST3 - 170;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    Erros = "CF JÁ SUBTOTALIZADO" + '\x0D';

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                else

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    if (ST3 >= 171)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ST3 = ST3 - 171;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        Erros = "CUPOM NÃO SUBTOTALIZADO" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    else


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        if (ST3 >= 172)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            ST3 = ST3 - 172;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            Erros = "ACRÉSCIMO EM SUBTOTAL JÁ EFETUADO" + '\x0D';

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        else

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            if (ST3 >= 173)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                ST3 = ST3 - 173;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                Erros = "DESCONTO EM SUBTOTAL JÁ EFETUADO" + '\x0D';

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            else

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                if (ST3 >= 174)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    ST3 = ST3 - 174;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    Erros = "ACRÉSCIMO NULO NÃO PERMITIDO" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                else


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    if (ST3 >= 175)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ST3 = ST3 - 175;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        Erros = "DESCONTO NULO NÃO PERMITIDO" + '\x0D';

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    else

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        if (ST3 >= 176)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            ST3 = ST3 - 176;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            Erros = "CANCELAMENTO DE ACRÉSCIMO OU DESCONTO EM SUBTOTAL NÃO PERMITIDO" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        else


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            if (ST3 >= 177)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                ST3 = ST3 - 177;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                Erros = "DATA INVÁLIDA" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            else

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                if (ST3 >= 178)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    ST3 = ST3 - 170;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    Erros = "VALOR DO CHEQUE NULO NÃO PERMITIDO" + '\x0D';

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                else

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    if (ST3 >= 179)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ST3 = ST3 - 179;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        Erros = "VALOR DO CHEQUE INVÁLIDO" + '\x0D';

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    else

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        if (ST3 >= 180)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            ST3 = ST3 - 180;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            Erros = "CHEQUE SEM LOCALIDADE NÃO PERMITIDO" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        else

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            if (ST3 >= 181)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                ST3 = ST3 - 181;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                Erros = "CANCELAMENTO ACRÉSCIMO EM ÍTEM NÃO PERMITIDO" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            else

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                if (ST3 >= 182)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    ST3 = ST3 - 182;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    Erros = "CANCELAMENTO DESCONTO EM ÍTEM NÃO PERMITIDO" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                else


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    if (ST3 >= 183)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ST3 = ST3 - 183;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        Erros = "NÚMERO MÁXIMO DE ÍTENS ATINGIDO" + '\x0D';

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    else

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        if (ST3 >= 184)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            ST3 = ST3 - 184;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            Erros = "NÚMERO DE ÍTEM NULO NÃO PERMITIDO" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        else


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            if (ST3 >= 185)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                ST3 = ST3 - 185;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                Erros = "MAIS QUE DUAS ALÍQUOTAS DIFERENTES NO BILHETE DE PASSAGEM NÃO PERMITIDO" + '\x0D';

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            else


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                if (ST3 >= 186)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    ST3 = ST3 - 186;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    Erros = "ACRÉSCIMO OU DESCONTO EM ITEM NÃO PERMITIDO" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                else


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    if (ST3 >= 187)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ST3 = ST3 - 187;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        Erros = "CANCELAMENTO DE ACRÉSCIMO OU DESCONTO EM ITEM NÃO PERMITIDO" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    else


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        if (ST3 >= 188)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            ST3 = ST3 - 188;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            Erros = "CLICHE JÁ IMPRESSO" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        else

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            if (ST3 >= 189)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                ST3 = ST3 - 189;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                Erros = "TEXTO OPCIONAL DO CHEQUE EXCEDEU O MÁXIMO PERMITIDO" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            else


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                if (ST3 >= 190)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    ST3 = ST3 - 190;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    Erros = "IMPRESSÃO AUTOMÁTICA NO VERSO NÃO PERMITIDO NESTE EQUIPAMENTO" + '\x0D';

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                else

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    if (ST3 >= 191)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ST3 = ST3 - 191;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        Erros = "TIMEOUT NA INSERÇÃO DO CHEQUE" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    else


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        if (ST3 >= 192)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            ST3 = ST3 - 192;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            Erros = "OVERFLOW NA CAPACIDADE DE TEXTO DO COMPROVANTE DE CRÉDITO OU DÉBITO" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        else


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            if (ST3 >= 193)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                ST3 = ST3 - 193;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                Erros = "PROGRAMAÇÃO DE ESPAÇOS ENTRE CUPONS MENOR QUE O MÍNIMO PERMITIDO" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            else


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                if (ST3 >= 194)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    ST3 = ST3 - 194;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    Erros = "EQUIPAMENTO NÃO POSSUI LEITOR DE CHEQUE" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                else



                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    if (ST3 >= 195)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ST3 = ST3 - 195;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        Erros = "PROGRAMAÇÃO DE ALÍQUOTA COM VALOR NULO NÃO PERMITIDO" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    else


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        if (ST3 >= 196)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            ST3 = ST3 - 196;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            Erros = "PARÂMETRO BAUD RATE INVÁLIDO" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        else


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            if (ST3 >= 197)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                ST3 = ST3 - 197;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                Erros = "CONFIGURAÇÃO PERMITIDA SOMENTE PELA PORTA DOS FISCO" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            else


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                if (ST3 >= 198)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    ST3 = ST3 - 198;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    Erros = "VALOR TOTAL DO ITEM EXCEDE 11 DÍGITOS" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                else


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    if (ST3 >= 199)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ST3 = ST3 - 199;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        Erros = "PROGRAMAÇÃO DA MOEDA COM ESPAÇOS EM BRACO NÃO PERMITIDO" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    else


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        if (ST3 >= 200)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            ST3 = ST3 - 200;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            Erros = "CASAS DECIMAIS DEVEM SER PROGRAMADAS COM 2 OU 3" + '\x0D';


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        else

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            if (ST3 >= 201)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                ST3 = ST3 - 201;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                Erros = "NÃO PERMITE CADASTRAR USUÁRIOS DIFERENTES NA MESMA MFD" + '\x0D';

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            else

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                if (ST3 >= 202)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    ST3 = ST3 - 202;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    Erros = "IDENTIFICAÇÃO DO CONSUMIDOR NÃO PERMITIDA PARA SANGRIA OU SUPRIMENTO" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                else


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    if (ST3 >= 203)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ST3 = ST3 - 203;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        Erros = "CASAS DECIMAIS EM QUANTIDADE MAIOR DO QUE A PERMITIDA" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    else


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        if (ST3 >= 204)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            ST3 = ST3 - 204;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            Erros = "CASAS DECIMAIS DO UNITÁRIO MAIOR DO QUE O PERMITIDA" + '\x0D';

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        else


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            if (ST3 >= 205)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                ST3 = ST3 - 205;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                Erros = "POSIÇÃO RESERVADA PARA ICMS" + '\x0D';

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            else

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                if (ST3 >= 206)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    ST3 = ST3 - 206;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    Erros = "POSIÇÃO RESERVADA PARA ISS" + '\x0D';

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                else

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    if (ST3 >= 207)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ST3 = ST3 - 207;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        Erros = "TODAS AS ALÍQUOTAS COM A MESMA VINCULAÇÃO NÃO PERMITIDO" + '\x0D';

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    else

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        if (ST3 >= 208)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            ST3 = ST3 - 208;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            Erros = "DATA DE EMBARQUE ANTERIOR A DATA DE EMISSÃO" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        else


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            if (ST3 >= 209)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                ST3 = ST3 - 209;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                Erros = "ALÍQUOTA DE ISS NÃO PERMITIDA SEM INSCRIÇÃO MUNICIPAL" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            else


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                if (ST3 >= 210)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    ST3 = ST3 - 210;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    Erros = "RETORNO PACOTE CLICHE FORA DA SEQUÊNCIA" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                else


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    if (ST3 >= 211)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ST3 = ST3 - 211;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        Erros = "ESPAÇO PARA ARMAZENAMENTO DO CLICHE ESGOTADO" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    else

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        if (ST3 >= 212)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            ST3 = ST3 - 212;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            Erros = "CLICHE GRÁFICO NÃO DISPONÍVEL PARA CONFIRMAÇÃO" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        else

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            if (ST3 >= 213)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                ST3 = ST3 - 213;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                Erros = "CRC DO CLICHE GRÁFICO DIFERENTE DO INFORMADO" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            else


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                if (ST3 >= 214)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    ST3 = ST3 - 214;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    Erros = "INTERVALO INVÁLIDO" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                else


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    if (ST3 >= 215)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ST3 = ST3 - 215;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        Erros = "USUÁRIO JÁ PROGRAMADO" + '\x0D';

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    else

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        if (ST3 >= 216)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            ST3 = ST3 - 216;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            Erros = "DETECTADA ABERTURA DO EQUIPAMENTO" + '\x0D';
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        else


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            if (ST3 >= 217)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                ST3 = ST3 - 217;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                Erros = "";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                Erros = "CANCELAMENTO DE ACRÉSCIMO/DESCONTO NÃO PERMITIDO" + '\x0D';

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            }
            if (Erros != "")
            {
                stringST3 = Erros;
            }
            #endregion

            /*if (Erros.Length != 0)
            {
                System.Windows.Forms.MessageBox.Show(Erros, "Erro na Execução do Comando", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else return true;*/
            return valida;
        }

        public static void Analisa_RetornoImpressoraNaoFiscal(int IRetorno, ref string messagem)
        {
            string MSG = "";
                string MSGCaption = "Atenção";
                MessageBoxIcon MSGIco = MessageBoxIcon.Information;
            #region
            switch (IRetorno)
            {
                case -64:
                    MSG = "Estorno de Comprovante de Débito ou Crédito permitido";
                    MSGCaption = "Erro";
                    MSGIco = MessageBoxIcon.Error;
                    break;
                case -128:
                    MSG = "VERIFIQUE O STATUS DA IMPRESSORA";
                    MSGCaption = "Erro";
                    MSGIco = MessageBoxIcon.Error;
                    break;
                case -32:
                    MSG = "Permite cancelamento do CNF";
                    MSGCaption = "Erro";
                    MSGIco = MessageBoxIcon.Error;
                    break;
                case -16:
                    MSG = "Impressora em ERRO";
                    break;
                case -4:
                    MSG = "Relatório Gerencial Aberto";
                    break;
                case -2:
                    MSG = "Comprovante Aberto";
                    MSGCaption = "Erro";
                    MSGIco = MessageBoxIcon.Error;
                    break;
                case -1:
                    MSG = "Comprovante Não-Fiscal Aberto";
                    break;
            }
            #endregion

            if (MSG.Length != 0)
            {
                messagem = MSG; //System.Windows.Forms.MessageBox.Show(MSG, MSGCaption, MessageBoxButtons.OK, MSGIco);
            }
            else
            {
                messagem = "";
            }
        }       

        /// <summary>
        /// Função que analiza o retorno da função.
        /// </summary>
        /// <param name="IRetorno">Inteiro com o valor a ser analizado.</param>
        public static void Analisa_iRetorno(int IRetorno, ref string messagem)
        {
            string MSG = "";
            string MSGCaption = "Atenção";
            MessageBoxIcon MSGIco = MessageBoxIcon.Information;

            switch (IRetorno)
            {
                case 0:
                    MSG = "Erro de Comunicação !";
                    MSGCaption = "Erro";
                    MSGIco = MessageBoxIcon.Error;
                    break;
                case -1:
                    MSG = "Erro de Execução na Função. Verifique!";
                    MSGCaption = "Erro";
                    MSGIco = MessageBoxIcon.Error;
                    break;
                case -2:
                    MSG = "Parâmetro Inválido !";
                    MSGCaption = "Erro";
                    MSGIco = MessageBoxIcon.Error;
                    break;
                case -3:
                    MSG = "Alíquota não programada !";
                    break;
                case -4:
                    MSG = "Arquivo BemaFI32.INI não encontrado. Verifique!";
                    break;
                case -5:
                    MSG = "Erro ao Abrir a Porta de Comunicação";
                    MSGCaption = "Erro";
                    MSGIco = MessageBoxIcon.Error;
                    break;
                case -6:
                    MSG = "Impressora Desligada ou Desconectada.";
                    break;
                case -7:
                    MSG = "Banco Não Cadastrado no Arquivo BemaFI32.ini";
                    break;
                case -8:
                    MSG = "Erro ao Criar ou Gravar no Arquivo Retorno.txt ou Status.txt.";
                    MSGCaption = "Erro";
                    MSGIco = MessageBoxIcon.Error;
                    break;
                case -18:
                    MSG = "Não foi possível abrir arquivo INTPOS.001!";
                    break;
                case -19:
                    MSG = "Parâmetros diferentes!";
                    break;
                case -20:
                    MSG = "Transação cancelada pelo Operador!";
                    break;
                case -21:
                    MSG = "A Transação não foi aprovada!";
                    break;
                case -22:
                    MSG = "Não foi possível terminar a Impressão!";
                    break;
                case -23: MSG = "Não foi possível terminar a Operação!";
                    break;
                case -24: MSG = "Não foi possível terminal a Operação!";
                    break;
                case -25: MSG = "Totalizador não fiscal não programado.";
                    break;
                case -26: MSG = "Transação já Efetuada!";
                    break;
                case -27: //Analisa_RetornoImpressora(ref ACK, ref ST1, ref ST2, ref ST3);
                    break;
                case -28: MSG = "Não há Informações para serem Impressas!";
                    break;
            }
            if (MSG.Length != 0)
            {
                messagem = MSG; //System.Windows.Forms.MessageBox.Show(MSG, MSGCaption, MessageBoxButtons.OK, MSGIco);
            }
            else
            {
                messagem = "";
            }
        }


        #endregion

        #region  IMPORT DAS FUNÇÕES DA BEMAFI32.DLL
        /*
		 ===============================================================================
			********************************************************************************

								DECLARAÇÃO DAS FUNÇÕES DA sign_bema.DLL
  
			********************************************************************************
		 ===============================================================================
		*/
        /// <summary>
        /// Gera as chaves pública e privada.
        /// </summary>
        /// http://partners.bematech.com.br/2009/06/edicao-43-edicao-extra-sign_bemadll/
        /// <param name="cChavePublica">STRING para receber a chave Pública.</param>
        /// <param name="cChavePrivada">STRING para receber a chave Privada.</param>
        /// <returns>INTEIRO - Retorno da impressora.</returns>
        [DllImport("sign_bema.dll")]
        public static extern int genkkey([MarshalAs(UnmanagedType.VBByRefStr)] ref String cChavePublica, [MarshalAs(UnmanagedType.VBByRefStr)] ref String cChavePrivada);

        /// <summary>
        /// Seleciona o tipo de bliblioteca a ser aplicada na geração das chaves.
        /// </summary>
        /// <param name="iTipo">INTEIRO com o tipo de biblioteca, onde: 0-usa a biblioteca OpenSSL, 1-usa a biblioteca Miracl</param>
        /// <returns>INTEIRO - Retorno da impressora.</returns>
        [DllImport("sign_bema.dll")]
        public static extern int setLibType(int iTipo);

        /// <summary>
        /// Gera a assinatura EAD do arquivo a partir das chaves pública e privada informadas.
        /// </summary>
        /// <param name="cNomeArquivo">STRING com o cominho+nome do arquivo que será usado na geração do registro EAD.</param>
        /// <param name="cChavePublica">STRING com a chave pública gerada.</param>
        /// <param name="cChavePrivada">STRING com a chave privada gerada.</param>
        /// <param name="cRegistroEAD">STRING com o tamanho de 256 bytes para receber o registro EAD criado</param>
        /// <param name="iGrava">INTEIRO para indicar se o registro EAD será ou não incluído ao final do arquivo informado, onde: 0-não incluir, 1-incluir.</param>
        /// <returns>INTEIRO - Retorno da impressora.</returns>
        [DllImport("sign_bema.dll")]
        public static extern int generateEAD(string cNomeArquivo, string cChavePublica, string cChavePrivada, [MarshalAs(UnmanagedType.VBByRefStr)] ref String cRegistroEAD, int iGrava);

        /// <summary>
        /// Valida o arquivo com o registro EAD gravado em seu final. Se o registro EAD estiver OK, a função retornará 1 (um), caso contrário retornará 0 (zero).
        /// </summary>
        /// <param name="cNomeArquivo">STRING com o cominho+nome do arquivo que será usado na geração do registro EAD.</param>
        /// <param name="cChavePublica">STRING com a chave pública gerada.</param>
        /// <param name="cChavePrivada">STRING com a chave privada gerada.</param>
        /// <returns>INTEIRO - Retorno da impressora.</returns>
        [DllImport("sign_bema.dll")]
        public static extern int validateFile(string cNomeArquivo, string cChavePublica, string cChavePrivada);

        /// <summary>
        /// Gera o MD5 do arquivo executável da aplicação.
        /// </summary>
        /// <param name="cNomeArquivo">STRING com o cominho+nome do arquivo que será usado na geração do MD5, com o tamanho de até 512 caracteres.</param>
        /// <param name="cMD5">STRING inicializada com 33 espaço para receber o MD5 gerado.</param>
        /// <returns>INTEIRO - Retorno da impressora.</returns>
        [DllImport("sign_bema.dll")]
        public static extern int md5FromFile(string cNomeArquivo, [MarshalAs(UnmanagedType.VBByRefStr)] ref String cMD5);
        #endregion

        #region IMPORT DAS FUNÇÕES DA BEMAFI32.DLL
        /*
		 ===============================================================================
			********************************************************************************

								DECLARAÇÃO DAS FUNÇÕES DA BEMAFI32.DLL
  
			********************************************************************************
		 ===============================================================================
		*/

        /// <summary>
        /// Função do PAF-ECF
        /// </summary>
        #region
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_AbreDocumentoAuxiliarVenda(string cIndiceGerencial, string cTituloDAV, string cNumeroDAV, string cNomeEmitente, string cCNPJ_CPFEmitente, string cNomeDestinatario, string CNPJ_CPFDestinatario);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_UsaDocumentoAuxiliarVenda(string cMercadoria, string cValorUnitario, string cValorTotal);

        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_FechaDocumentoAuxiliarVenda(string cTotal);

        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_TerminaFechamentoCupomPreVenda(string cMD5, string cNumeroPreVenda, string cMensagemPromocional);

        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_DAVEmitidosRelatorioGerencial(string cIndiceGerencial, string cDataInicial, string cDataFinal);

        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_DAVEmitidosArquivo(string cNomeArquivo, string cDataInicial, string cDataFinal, string cChavePublica, string cChavePrivada);

        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_LeituraMemoriaFiscalSerialReducaoPAFECF(string cCRZInicial, string cCRZFinal, string cFlagLeitura, string cChavePublica, string cChavePrivada);

        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_IdentificacaoPAFECF(string cIndiceGerencial, string cNumeroLaudo, string cCNPJDesenvolvedor, string cRazaoSocial, string cEndereco, string cTelefone, string cContato, string cNomeComercial, string cVersao, string cPrincipalExecutavel, string cMD5PrincipalExecutavel, string cDemaisArquivos, string cMD5DemaisArquivos, string cNumerosFabricacao);

        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_GrandeTotalCriptografado([MarshalAs(UnmanagedType.VBByRefStr)] ref String cGTCriptografado);

        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_GrandeTotalDescriptografado(string cGTCriptografado, [MarshalAs(UnmanagedType.VBByRefStr)] ref String cGTDescriptografado);

        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_AbreRelatorioMeiosPagamento(string cIndiceGerencial);

        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_UsaRelatorioMeiosPagamento(string cIdentificacao, string cTipoDocumento, string cValorAcumulado, string cData);

        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_UsaRelatorioMeiosPagamaneto(string cIdentificacao, string cTipoDocumento, string cValorAcumulado, string cData);

        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_FechaRelatorioMeiosPagamento();

        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_NomeiaRelatorioMeiosDePagamento();

        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_NomeiaRelatorioDocumentoAuxiliarDeVenda();

        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_NomeiaRelatorioDAVEmitidos();

        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_NomeiaRelatorioIdentificacaoPAFECF();

        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_NomeiaRelatoriosPAFECF();

        #endregion
        #region Funções de Inicialização
        /// <summary>
        /// Altera o símbolo da moeda programada na Impressora Fiscal. 
        /// </summary>
        /// <param name="SimboloMoeda">STRING contendo o símbolo da moeda. O $ (cifrão) é inserido automaticamente.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>

        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_AlteraSimboloMoeda(string SimboloMoeda);
        /// <summary>
        /// Programa alíquota tributária na Impressora Fiscal. 
        /// </summary>
        /// <param name="Aliquota">STRING com o valor da alíquota a ser programada</param>
        /// <param name="ICMS_ISS">INTEIRO com o valor 0 (zero) para vincular a alíquota ao ICMS e 1 (um) para vincular ao ISS</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_ProgramaAliquota(string Aliquota, int ICMS_ISS);
        /// <summary>
        /// Programa departamento na impressora.
        /// </summary>
        /// <param name="Indice">INTEIRO com a posição em que o Departamento será cadastrado. </param>
        /// <param name="Departamento">STRING com até 10 caracteres com o nome do departamento. </param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        /// 
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_IniciaModoTEF();
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_FinalizaModoTEF();
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_NomeiaDepartamento(int Indice, string Departamento);
        /// <summary>
        /// Programa Totalizador Não Sujeito ao ICMS. 
        /// </summary>
        /// <param name="Indice">INTEIRO com a posição em que o totalizador será programado. </param>
        /// <param name="Totalizador">STRING até 19 caracteres com o nome do totalizador.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_NomeiaTotalizadorNaoSujeitoIcms(int Indice, string Totalizador);
        /// <summary>
        /// Programa o espaçamento de linhas entre os cupons.
        /// </summary>
        /// <param name="Linhas">INTEIRO entre 0 e 255 indicando o número de linhas. O valor default da impressora é 8 linhas.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_LinhasEntreCupons(int Linhas);
        /// <summary>
        /// Programa o espaçamento entre as linhas impressas no cupom
        /// </summary>
        /// <param name="Dots">INTEIRO entre 0 e 255 indicando o espaço (dots) entre as linhas. O valor default da impressora é 0.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_EspacoEntreLinhas(int Dots);
        /// <summary>
        /// Permite tornar a impressão mais forte nos equipamentos baseados na MP-20 FI II.
        /// </summary>
        /// <param name="ForcaImpacto">INTEIRO com o valor da força de impacto das agulhas que pode ser: 
        ///			<br>1 – Impacto fraco (default) </br>
        ///			<br>2 – Impacto médio </br> 
        ///			<br>3 – Impacto forte </br></param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_ForcaImpactoAgulhas(int ForcaImpacto);
        /// <summary>
        /// Programa e desprograma o horário de verão. Se a impressora já estiver no horário de verão o mesmo será desprogramado atrasando o relógio em 1 (uma) hora, caso contrário será adiantado 1 (uma) hora.
        /// </summary>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_ProgramaHorarioVerao();
        /// <summary>
        /// Programa o modo arrendondamento na impressora. Este arredondamento se refere à venda de item com quantidade fracionária.
        /// </summary>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_ProgramaArredondamento();
        /// <summary>
        /// Programa o modo truncamento na impressora.
        /// </summary>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_ProgramaTruncamento();
        #endregion

        #region Funções do Cupom Fiscal
        /// <summary>
        /// Abre o cupom fiscal na impressora.
        /// </summary>
        /// <param name="CGC_CPF">STRING até 29 caracteres com o CNPJ ou CPF do cliente.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_AbreCupom(string CGC_CPF);
        /// <summary>
        /// Vende item após a abertura do cupom fiscal. Essa função permite também a venda de itens com 3 casas decimais no valor unitário.
        /// </summary>
        /// <param name="Codigo">STRING até 13 caracteres com o código do produto.</param>
        /// <param name="Descricao">STRING até 29 caracteres com a descrição do produto.</param>
        /// <param name="Aliquota">STRING com o valor ou o índice da alíquota tributária. Se for o valor deve ser informado com o tamanho de 4 caracteres ou 5 com a vírgula. Se for o índice da alíquota deve ser 2 caracteres. Ex. (18,00 para o valor ou 05 para o índice).</param>
        /// <param name="TipoQuantidade">1 (um) caracter indicando o tipo de quantidade. I - Inteira e F - Fracionária.</param>
        /// <param name="Quantidade">STRING com até 4 dígitos para quantidade inteira e 7 dígitos para quantidade fracionária. Na quantidade fracionária são 3 casas decimais.</param>
        /// <param name="CasasDecimais">INTEIRO indicando o número de casas decimais para o valor unitário (2 ou 3).</param>
        /// <param name="ValorUnitario">STRING até 8 dígitos para valor unitário.</param>
        /// <param name="TipoDesconto">1 (um) caracter indicando a forma do desconto. '$' desconto por valor e '%' desconto percentual</param>
        /// <param name="Desconto">String com até 8 dígitos para desconto por valor (2 casas decimais) e 4 dígitos para desconto percentual.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_VendeItem(string Codigo, string Descricao, string Aliquota, string TipoQuantidade, string Quantidade, int CasasDecimais, string ValorUnitario, string TipoDesconto, string Desconto);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_VendaBruta([MarshalAs(UnmanagedType.VBByRefStr)] ref string varValor);
        /// <summary>
        /// Essa função permite a venda de itens com entrada de departamento, desconto e unidade de medida.
        /// </summary>
        /// <param name="Codigo">STRING até 49 caracteres com o código do produto.</param>
        /// <param name="Descricao">STRING até 201 caracteres com a descrição do produto.</param>
        /// <param name="Aliquota">STRING com o valor ou o índice da alíquota tributária. Se for o valor deve ser informado com o tamanho de 4 caracteres ou 5 com a vírgula. Se for o índice da alíquota deve ser 2 caracteres. Ex. (18,00 para o valor ou 05 para o índice)</param>
        /// <param name="ValorUnitario">STRING com até 9 dígitos para o valor (tres casas decimais).</param>
        /// <param name="Quantidade"> STRING com até 7 dígitos para a quantidade. Na venda com departamento a quantidade é fracionária e são 3 casas decimais.</param>
        /// <param name="Acrescimo">STRING com o acréscimo por valor com até 10 dígitos (2 casas decimais).</param>
        /// <param name="Desconto">STRING com o desconto por valor com até 10 dígitos (2 casas decimais).</param>
        /// <param name="IndiceDepartamento">STRING com o índice do departamento com 2 dígitos.</param>
        /// <param name="UnidadeMedida">STRING com no máximo 2 caracteres para a unidade de medida. Caso não seja passado nenhum caracter a unidade não é impressa.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_VendeItemDepartamento(string Codigo, string Descricao, string Aliquota, string ValorUnitario, string Quantidade, string Acrescimo, string Desconto, string IndiceDepartamento, string UnidadeMedida);
        /// <summary>
        /// Cancela o último item vendido.
        /// </summary>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_CancelaItemAnterior();
        /// <summary>
        /// Cancela qualquer item dentre os cem (100) últimos itens vendidos.
        /// </summary>
        /// <param name="NumeroItem">STRING com o número do item a ser cancelado com no máximo 3 dígitos.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_CancelaItemGenerico(string NumeroItem);
        /// <summary>
        /// Cancela o último cupom emitido.
        /// </summary>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_CancelaCupom();
        /// <summary>
        /// Permite fechar o cupom de forma resumida, ou seja, sem acréscimo ou desconto no cupom e com apenas uma forma de pagamento. Essa função lê o subtotal do cupom para fecha-lo.
        /// </summary>
        /// <param name="FormaPagamento">STRING com a forma de pagamento com no máximo 16 caracteres.</param>
        /// <param name="Mensagem">STRING com a mensagem promocional com até 384 caracteres (8 linhas X 48 colunas), para a impressora fiscal MP-20 FI II, e 320 caracteres (8 linhas X 40 colunas), para a impressora fiscal MP-40 FI II.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_FechaCupomResumido(string FormaPagamento, string Mensagem);
        /// <summary>
        /// Fecha o cupom fiscal com a impressão da mensagem promocional.
        /// </summary>
        /// <param name="FormaPagamento">STRING com o nome da forma de pagamento com no máximo 16 caracteres.</param>
        /// <param name="AcrescimoDesconto">Indica se haverá acréscimo ou desconto no cupom. 'A' para acréscimo e 'D' para desconto.</param>
        /// <param name="TipoAcrescimoDesconto">Indica se o acréscimo ou desconto é por valor ou por percentual. '$' para desconto por valor e '%' para percentual.</param>
        /// <param name="ValorAcrescimoDesconto">STRING com no máximo 14 dígitos para acréscimo ou desconto por valor e 4 dígitos para acréscimo ou desconto por percentual.</param>
        /// <param name="ValorPago">STRING com o valor pago com no máximo 14 dígitos.</param>
        /// <param name="Mensagem">STRING com a mensagem promocional com até 384 caracteres (8 linhas X 48 colunas), para a impressora fiscal MP-20 FI II, e 320 caracteres (8 linhas X 40 colunas), para a impressora fiscal MP-40 FI II.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_FechaCupom(string FormaPagamento, string AcrescimoDesconto, string TipoAcrescimoDesconto, string ValorAcrescimoDesconto, string ValorPago, string Mensagem);
        /// <summary>
        /// Inicia o fechamento do cupom com o uso das formas de pagamento.
        /// </summary>
        /// <param name="AcrescimoDesconto">Indica se haverá acréscimo ou desconto no cupom. 'A' para acréscimo e 'D' para desconto.</param>
        /// <param name="TipoAcrescimoDesconto">Indica se o acréscimo ou desconto é por valor ou por percentual. '$' para desconto por valor e '%' para percentual.</param>
        /// <param name="ValorAcrescimoDesconto">STRING com no máximo 14 dígitos para acréscimo ou desconto por valor e 4 dígitos para acréscimo ou desconto por percentual.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_IniciaFechamentoCupom(string AcrescimoDesconto, string TipoAcrescimoDesconto, string ValorAcrescimoDesconto);
        /// <summary>
        /// Imprime a(s) forma(s) de pagamento e o(s) valor(es) pago(s) nessa forma.
        /// </summary>
        /// <param name="FormaPagamento">STRING com a forma de pagamento com no máximo 16 caracteres.</param>
        /// <param name="ValorFormaPagamento">STRING com o valor da forma de pagamento com até 14 dígitos.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_EfetuaFormaPagamento(string FormaPagamento, string ValorFormaPagamento);
        /// <summary>
        /// Imprime a(s) forma(s) de pagamento e o(s) valor(es) pago(s). Permite a impressão de comentários na(s) forma(s) de pagamento.
        /// </summary>
        /// <param name="FormaPagamento">STRING com a forma de pagamento com no máximo 16 caracteres.</param>
        /// <param name="ValorFormaPagamento">STRING com o valor da forma de pagamento com até 14 dígitos.</param>
        /// <param name="Descricao">STRING com a descrição da forma de pagamento com no máximo 80 caracteres.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_EfetuaFormaPagamentoDescricaoForma(string FormaPagamento, string ValorFormaPagamento, string Descricao);
        /// <summary>
        /// Termina o fechamento do cupom com mensagem promocional.
        /// </summary>
        /// <param name="Mensagem">STRING com a mensagem promocional com até 384 caracteres (8 linhas X 48 colunas), para a impressora fiscal MP-20 FI II, e 320 caracteres (8 linhas X 40 colunas), para a impressora fiscal MP-40 FI II.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_TerminaFechamentoCupom(string Mensagem);
        /// <summary>
        /// Permite estornar valores de uma forma de pagamento e inserir em outra.
        /// </summary>
        /// <param name="FormaOrigem">STRING com a forma de pagamento de onde o valor será estornado, com até 16 caracteres.</param>
        /// <param name="FormaDestino">STRING com a forma de pagamento onde o valor será inserido, com até 16 caracteres.</param>
        /// <param name="Valor"> STRING com o valor a ser estornado com até 14 dígitos. Não pode ser maior que o total da forma de pagamento de origem.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_EstornoFormasPagamento(string FormaOrigem, string FormaDestino, string Valor);
        /// <summary>
        /// Esta função permite aumentar a descrição do item até 200 caracteres.
        /// </summary>
        /// <param name="Descricao">STRING com a descrição do item com até 200 caracteres.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_AumentaDescricaoItem(string Descricao);
        /// <summary>
        /// Imprime a unidade de medida após a quantidade do produto na venda de item.
        /// </summary>
        /// <param name="UnidadeMedida">STRING com a unidade de medida até 2 caracteres.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_UsaUnidadeMedida(string UnidadeMedida);
        #endregion

        #region Funções dos Relatórios Fiscais
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_ArquivoMFD(string NomeArquivoOrigem, string cDataInicial, string cDataFinal, string cTipoDownload, string Usuario, int Parametrizacao, string cChavePublica, string cChavePrivada, int TipoGeracao);

        //Public Declare Function Bematech_FI_EspelhoMFD Lib “BEMAFI32.DLL” ( ByVal cNomeArquivoDestino As String, ByVal cDadoInicial As String, ByVal cDadoFinal As String, ByVal cTipoDownload As String, ByVal cUsuario As String, ByVal cChavePublica As String, ByVal cChavePrivada As String ) As Integer
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_EspelhoMFD(string cNomeArquivoDestino, string cDadoInicial, string cDadoFinal, string cTipoDownload, string cUsuario, string cChavePublica, string cChavePrivada);
        /// <summary>
        /// Emite a Leitura X na impressora.
        /// </summary>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_LeituraX();
        /// <summary>
        /// Recebe os dados da Leitura X pela serial e grava em arquivo texto.
        /// </summary>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_LeituraXSerial();
        /// <summary>
        /// Emite a Redução Z na impressora. Permite ajustar o relógio interno da impressora em até 5 minutos.
        /// </summary>
        /// <param name="Data">STRING com a Data atual da impressora no formato ddmmaa ou dd/mm/aa, dd/mm/aaaa ou dd/mm/aa.</param>
        /// <param name="Hora">STRING com a Hora a ser alterada no formato hhmmss ou hh:mm:ss.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_ReducaoZ(string Data, string Hora);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Texto"></param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_RelatorioGerencial(string Texto);
        /// <summary>
        /// 
        /// </summary>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        /// 
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_FechaRelatorioGerencial();
        /// <summary>
        /// Emite a leitura da memória fiscal da impressora por intervalo de datas.
        /// </summary>
        /// <param name="DataInicial">STRING com a Data inicial no formato ddmmaa, dd/mm/aa, ddmmaaaa ou dd/mm/aaaa.</param>
        /// <param name="DataFinal">STRING com a Data final no formato ddmmaa, dd/mm/aa, ddmmaaaa ou dd/mm/aaaa.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_LeituraMemoriaFiscalData(string DataInicial, string DataFinal);
        /// <summary>
        /// Emite a leitura da memória fiscal da impressora por intervalo de reduções.
        /// </summary>
        /// <param name="ReducaoInicial">STRING com o Número da redução inicial com até 4 dígitos.</param>
        /// <param name="ReducaoFinal">STRING com o Número da redução final com até 4 dígitos.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_LeituraMemoriaFiscalReducao(string ReducaoInicial, string ReducaoFinal);
        /// <summary>
        /// Recebe os dados da memória fiscal por intervalo de datas pela serial e grava em arquivo texto.
        /// </summary>
        /// <param name="DataInicial">STRING com a Data inicial no formato ddmmaa, dd/mm/aa, ddmmaaaa ou dd/mm/aaaa.</param>
        /// <param name="DataFinal">STRING com a Data final no formato ddmmaa, dd/mm/aa, ddmmaaaa ou dd/mm/aaaa.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_LeituraMemoriaFiscalSerialData(string DataInicial, string DataFinal);
        /// <summary>
        /// Recebe os dados da leitura da memória fiscal por intervalo de reduções pela serial e grava em arquivo texto.
        /// </summary>
        /// <param name="ReducaoInicial">STRING com o Número da reducao inicial com até 4 dígitos.</param>
        /// <param name="ReducaoFinal">STRING com o Número da reducao final com até 4 dígitos.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_LeituraMemoriaFiscalSerialReducao(string ReducaoInicial, string ReducaoFinal);

        #endregion

        #region Funções das Operações Não Fiscais
        /// <summary>
        /// Imprime o comprovante não fiscal não vinculado.
        /// </summary>
        /// <param name="IndiceTotalizador">STRING com o Indice do totalizador para recebimento parcial com até 2 dígitos.</param>
        /// <param name="Valor">STRING com o Valor do recebimento com até 14 dígitos (duas casas decimais).</param>
        /// <param name="FormaPagamento">STRING com a Forma de pagamento com até 16 caracteres.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_RecebimentoNaoFiscal(string IndiceTotalizador, string Valor, string FormaPagamento);
        /// <summary>
        /// Abre o comprovante não fiscal vinculado.
        /// </summary>
        /// <param name="FormaPagamento">Forma de pagamento com até 16 caracteres.</param>
        /// <param name="Valor">Valor pago na forma de pagamento com até 14 dígitos (2 casas decimais).</param>
        /// <param name="NumeroCupom">Número do cupom a que se refere o comprovante com até 6 dígitos.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_AbreComprovanteNaoFiscalVinculado(string FormaPagamento, string Valor, string NumeroCupom);
        /// <summary>
        /// Imprime o comprovante não fiscal vinculado.
        /// </summary>
        /// <param name="Texto">STRING com o Texto a ser impresso no comprovante não fiscal vinculado com até 618 caracteres.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_UsaComprovanteNaoFiscalVinculado(string Texto);
        /// <summary>
        /// Encerrar o comprovante não fiscal vinculado.
        /// </summary>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        /// 
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_UsaComprovanteNaoFiscalVinculadoTEF(string Texto);
        /// <summary>
        /// Encerrar o comprovante não fiscal vinculado.
        /// </summary>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_FechaComprovanteNaoFiscalVinculado();
        /// <summary>
        /// Faz uma sangria na impressora (retirada de dinheiro).
        /// </summary>
        /// <param name="Valor">STRING com o Valor da sangria com até 14 dígitos (2 casas decimais).</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_Sangria(string Valor);
        /// <summary>
        /// Faz um suprimento na impressora (entrada de dinheiro).
        /// </summary>
        /// <param name="Valor">STRING com o Valor do suprimento com até 14 dígitos (2 casas decimais).</param>
        /// <param name="FormaPagamento">STRING com a Forma de pagamento com até 16 caracteres. Se não for informada, o suprimento será feito em Dinheiro.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_Suprimento(string Valor, string FormaPagamento);
        #endregion

        #region Funções de Informações da Impressora
        /// <summary>
        /// Retorna a valor acumulado dos acréscimos efetuados nos cupons. 
        /// </summary>
        /// <param name="ValorAcrescimos">Variável string com 14 posições para receber o valor dos acréscimos.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_Acrescimos([MarshalAs(UnmanagedType.VBByRefStr)] ref string ValorAcrescimos);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_ImprimeConfiguracoesImpressora();
        /// <summary>
        /// Retorna o valor acumulado dos itens e dos cupons cancelados.
        /// </summary>
        /// <param name="ValorCancelamentos">Variável string com 14 posições para receber o valor dos cancelamentos com 2 casas decimais.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_Cancelamentos([MarshalAs(UnmanagedType.VBByRefStr)] ref string ValorCancelamentos);
        /// <summary>
        /// Retorna o CGC e a Inscrição Estadual do cliente/proprietário cadastrado na impressora.
        /// </summary>
        /// <param name="CGC">Variável string com 18 posições para receber o CGC.</param>
        /// <param name="IE">Variável string com 15 posições para receber a Inscrição Estadual.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_CGC_IE([MarshalAs(UnmanagedType.VBByRefStr)] ref string CGC, [MarshalAs(UnmanagedType.VBByRefStr)] ref string IE);
        /// <summary>
        /// Retorna o clichê do proprietário cadastrado na impressora.
        /// </summary>
        /// <param name="Cliche">Variável string com 186 posições para receber clichê cadastrado.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_ClicheProprietario([MarshalAs(UnmanagedType.VBByRefStr)] ref string Cliche);
        /// <summary>
        /// Retorna o número de bilhetes de passagem emitidos.
        /// </summary>
        /// <param name="ContadorPassagem">Variável string com 6 posições para receber o número de passagens emitidas.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_ContadorBilhetePassagem(string ContadorPassagem);
        /// <summary>
        /// Retorna o número de vezes em que os totalizadores não sujeitos ao ICMS foram usados.
        /// </summary>
        /// <param name="Contadores">Variável string com 44 posições para receber os contadores dos totalizadores.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_ContadoresTotalizadoresNaoFiscais([MarshalAs(UnmanagedType.VBByRefStr)] ref string Contadores);
        /// <summary>
        /// Retorna os dados da impressora no momento da última Redução Z.
        /// </summary>
        /// <param name="DadosReducao">Retorna os dados da impressora no momento da última Redução Z.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_DadosUltimaReducao([MarshalAs(UnmanagedType.VBByRefStr)] ref string DadosReducao);
        /// <summary>
        /// Retorna a data e a hora atual da impressora.
        /// </summary>
        /// <param name="Data">Variável string com 6 posições para receber a data atual da impressora no formato ddmmaa.</param>
        /// <param name="Hora">Variável string com 6 posições para receber a hora atual da impressora no formato hhmmss.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_DataHoraImpressora([MarshalAs(UnmanagedType.VBByRefStr)] ref string Data, [MarshalAs(UnmanagedType.VBByRefStr)] ref string Hora);
        /// <summary>
        /// Retorna a data da última Redução Z.
        /// </summary>
        /// <param name="Data">Variável string com 6 posições para receber a data da última redução no formato ddmmaa.</param>
        /// <param name="Hora">Variável string com 6 posições parar eceber a hora da última redução no formato hhmmss.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_DataHoraReducao([MarshalAs(UnmanagedType.VBByRefStr)] ref string Data, [MarshalAs(UnmanagedType.VBByRefStr)] ref string Hora);
        /// <summary>
        /// Retorna a data do último movimento.
        /// </summary>
        /// <param name="Data">Variável string com 6 posições para receber a data do movimento no formato ddmmaa.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_DataMovimento([MarshalAs(UnmanagedType.VBByRefStr)] ref string Data);
        /// <summary>
        /// Retorna a valor acumulado dos descontos.
        /// </summary>
        /// <param name="ValorDescontos">Variável string com 14 posições para receber o valor dos descontos com 2 casas decimais.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_Descontos([MarshalAs(UnmanagedType.VBByRefStr)] ref string ValorDescontos);
        /// <summary>
        /// Retorna um número referente ao flag fiscal da impressora. Veja discriminação abaixo.
        /// </summary>
        /// <param name="Flag">Variável inteira para receber um número representando o flag fiscal da impressora. Veja discriminação abaixo.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_StatusEstendidoMFD(ref int Flag);

        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_FlagsFiscais(ref int Flag);
        /// <summary>
        /// Retorna o valor do Grande Total da impressora.
        /// </summary>
        /// <param name="GrandeTotal">Variável string com 18 posições para receber o valor do grande total com 2 casas decimais.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_GrandeTotal([MarshalAs(UnmanagedType.VBByRefStr)] ref string GrandeTotal);
        /// <summary>
        /// Retorna o tempo em minutos que a impressora está ligada.
        /// </summary>
        /// <param name="Minutos">Variável string com 4 posições para receber os minutos em que a impressora está ligada.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_MinutosLigada([MarshalAs(UnmanagedType.VBByRefStr)] ref string Minutos);
        /// <summary>
        /// Retorna o tempo em minutos que a impressora está ou esteve imprimindo.
        /// </summary>
        /// <param name="Minutos">Variável string com 4 posições para receber os minutos em impressão.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_MinutosImprimindo([MarshalAs(UnmanagedType.VBByRefStr)] ref string Minutos);
        /// <summary>
        /// Retorna o número de linhas impressas após o status de Pouco Papel.
        /// </summary>
        /// <param name="Linhas">Variável inteira para receber a quantidade de linhas impressas.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_MonitoramentoPapel(ref int Linhas);
        /// <summary>
        /// Retorna o número do caixa cadastrado na impressora.
        /// </summary>
        /// <param name="NumeroCaixa">Variável string com 4 posições para receber o número do caixa cadastrado na impressora.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_NumeroCaixa([MarshalAs(UnmanagedType.VBByRefStr)] ref string NumeroCaixa);
        /// <summary>
        /// Retorna o número do cupom.
        /// </summary>
        /// <param name="NumeroCupom">Variável string com 6 posições para receber o número do último cupom.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_NumeroCupom([MarshalAs(UnmanagedType.VBByRefStr)] ref string NumeroCupom);
        /// <summary>
        /// Retorna o número de cupons cancelados.
        /// </summary>
        /// <param name="NumeroCancelamentos">Variável STRING com o tamanho de 4 bytes para receber o número de cupons cancelados.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_NumeroCuponsCancelados([MarshalAs(UnmanagedType.VBByRefStr)] ref string NumeroCancelamentos);
        /// <summary>
        /// Retorna o número de intervenções técnicas realizadas na impressora.
        /// </summary>
        /// <param name="NumeroIntervencoes">Variável string com 4 posições para receber o número de intervenções.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_NumeroIntervencoes([MarshalAs(UnmanagedType.VBByRefStr)] ref string NumeroIntervencoes);
        /// <summary>
        /// Retorna o número da loja cadastrado na impressora.
        /// </summary>
        /// <param name="NumeroLoja">Variável string com 4 posições para receber o número da loja cadastrado na impressora.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_NumeroLoja([MarshalAs(UnmanagedType.VBByRefStr)] ref string NumeroLoja);
        /// <summary>
        /// Retorna o número de operações não fiscais executadas na impressora.
        /// </summary>
        /// <param name="NumeroOperacoes">Variável string com 6 posições para receber o número de operações.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_NumeroOperacoesNaoFiscais([MarshalAs(UnmanagedType.VBByRefStr)] ref string NumeroOperacoes);
        /// <summary>
        /// Retorna o número de reduções Z realizadas na impressora.
        /// </summary>
        /// <param name="NumeroReducoes">Variável string com 4 posições para receber o número de Reduções Z.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_NumeroReducoes([MarshalAs(UnmanagedType.VBByRefStr)] ref string NumeroReducoes);
        /// <summary>
        /// Retorna o número de série da impressora.
        /// </summary>
        /// <param name="NumeroSerie">Variável string com o tamanho de 15 posições para receber o número de série.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_NumeroSerie([MarshalAs(UnmanagedType.VBByRefStr)] ref string NumeroSerie);
        /// <summary>
        /// Retorna o número de substituições de proprietário.
        /// </summary>
        /// <param name="NumeroSubstituicoes">Variável string com 4 posições para receber o número de substituições.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_NumeroSubstituicoesProprietario([MarshalAs(UnmanagedType.VBByRefStr)] ref string NumeroSubstituicoes);
        /// <summary>
        /// Retorna as alíquotas cadastradas na impressora.
        /// </summary>
        /// <param name="Aliquotas">Variável string com o tamanho de 79 posições para receber as alíquotas.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_RetornoAliquotas([MarshalAs(UnmanagedType.VBByRefStr)] ref string Aliquotas);
        /// <summary>
        /// Retorna o símbolo da moeda cadastrado na impressora.
        /// </summary>
        /// <param name="SimboloMoeda">Variável string com 2 posições para receber o símbolo da moeda.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_SimboloMoeda([MarshalAs(UnmanagedType.VBByRefStr)] ref string SimboloMoeda);
        /// <summary>
        /// Retorna o valor do subtotal do cupom.
        /// </summary>
        /// <param name="SubTotal">Variável string com o tamanho de 14 posições para receber o subtotal do cupom.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_SubTotal([MarshalAs(UnmanagedType.VBByRefStr)] ref string SubTotal);
        /// <summary>
        /// Retorna o número do último item vendido.
        /// </summary>
        /// <param name="NumeroItem">Variável string com 4 posições para receber o número do último item vendido.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_UltimoItemVendido([MarshalAs(UnmanagedType.VBByRefStr)] ref string NumeroItem);
        /// <summary>
        /// Retorna o valor acumulado em uma determinada forma de pagamento.
        /// </summary>
        /// <param name="Forma">Variável STRING com até 16 posições com a descrição da Forma de Pagamento que deseja retornar o seu valor.</param>
        /// <param name="ValorForma">Variável STRING com 14 posições para receber a informação.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_ValorFormaPagamento(string Forma, [MarshalAs(UnmanagedType.VBByRefStr)] ref string ValorForma);
        /// <summary>
        /// Retorna o valor pago no último cupom.
        /// </summary>
        /// <param name="ValorCupom">Variável string com 14 posições para receber o valor pago no último cupom.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_ValorPagoUltimoCupom([MarshalAs(UnmanagedType.VBByRefStr)] ref string ValorCupom);
        /// <summary>
        /// Retorna o valor acumulado em um determinado totalizador não fiscal.
        /// </summary>
        /// <param name="Totalizador">Variável STRING com até 19 posições com a descrição do Totalizador.</param>
        /// <param name="ValorTotalizador">Variável STRING com 14 posições para receber a informação.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_ValorTotalizadorNaoFiscal(string Totalizador, [MarshalAs(UnmanagedType.VBByRefStr)] ref string ValorTotalizador);
        /// <summary>
        /// Retorna as alíquotas de vinculação ao ISS.
        /// </summary>
        /// <param name="Flag">Variável string com 79 posições para receber as alíquotas vinculadas ao Iss.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_VerificaAliquotasIss([MarshalAs(UnmanagedType.VBByRefStr)] ref string Flag);
        /// <summary>
        /// Verifica se a Eprom está conectada.
        /// </summary>
        /// <param name="Flag">Variável string com 2 posição para receber o flag de Eprom conectada. Onde: 
        /// <br></br>1 - Eprom conectada 
        ///	<br></br>0 - Eprom desconectada. 
        ///	</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_VerificaEpromConectada([MarshalAs(UnmanagedType.VBByRefStr)] ref string Flag);
        /// <summary>
        /// Retorna os departamentos e seus valores acumulados.
        /// </summary>
        /// <param name="Departamentos">Variável string com 1019 posições para receber as informações dos departamentos.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_VerificaDepartamentos([MarshalAs(UnmanagedType.VBByRefStr)] ref string Departamentos);
        /// <summary>
        /// Retorna o estado da impressora.
        /// </summary>
        /// <param name="ACK">Variável inteira para receber o primeiro byte.</param>
        /// <param name="ST1">Variável inteira para receber o segundo byte</param>
        /// <param name="ST2">Variável inteira para receber o terceiro byte</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_VerificaEstadoImpressora(ref int ACK, ref int ST1, ref int ST2);
        /// <summary>
        /// Retorna as formas de pagamento e seus valores acumulados.
        /// </summary>
        /// <param name="Formas">Variável string com 3016 posições para receber as formas programadas.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_VerificaFormasPagamento([MarshalAs(UnmanagedType.VBByRefStr)] ref string Formas);
        /// <summary>
        /// Retorna os índices das alíquotas de ISS. 
        /// </summary>
        /// <param name="Modo">Variável string com o tamanho de 48 posições para receber os índices das alíquotas de ISS.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_VerificaIndiceAliquotasIss([MarshalAs(UnmanagedType.VBByRefStr)] ref string Flag);
        /// <summary>
        /// Verifica se a impressora está em modo normal ou em intervenção técnica
        /// </summary>
        /// <param name="Modo">Variável string com 1 posição para receber o modo de operação da impressora. Onde: 
        ///		<br></br>1 - Modo normal 
        ///		<br></br>0 - Intervenção técnica.
        ///	</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_VerificaModoOperacao([MarshalAs(UnmanagedType.VBByRefStr)] ref string Modo);
        /// <summary>
        /// Retorna os recebimentos não fiscais não vinculados programados na impressora.
        /// </summary>
        /// <param name="Recebimentos">Variável string com 2200 posições para receber as informações.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_VerificaRecebimentoNaoFiscal([MarshalAs(UnmanagedType.VBByRefStr)] ref string Recebimentos);
        /// <summary>
        /// Retorna o tipo de impressora.
        /// </summary>
        /// <param name="TipoImpressora">Variável inteira para receber o tipo da impressora (veja abaixo no help os valores retornados).</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_VerificaTipoImpressora(ref int TipoImpressora);
        /// <summary>
        /// Retorna a descrição dos totalizadores não fiscais programados na impressora.
        /// </summary>
        /// <param name="Totalizadores">Variável string com 179 posições para receber a descrição dos totalizadores não fiscais programados</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_VerificaTotalizadoresNaoFiscais([MarshalAs(UnmanagedType.VBByRefStr)] ref string Totalizadores);
        /// <summary>
        /// Retorna os totalizadores parciais cadastrados na impressora.
        /// </summary>
        /// <param name="Totalizadores">Variável string com o tamanho de 445 posições para receber os totalizadores parciais cadastrados.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_VerificaTotalizadoresParciais([MarshalAs(UnmanagedType.VBByRefStr)] ref string Totalizadores);
        /// <summary>
        /// Retorna 1 se a impressora estiver no modo truncamento e 0 se estiver no modo arredondamento.
        /// </summary>
        /// <param name="Flag">Variável string com 1 posição para receber o flag de truncamento</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_VerificaTruncamento([MarshalAs(UnmanagedType.VBByRefStr)] ref string Flag);
        /// <summary>
        /// Retorna a versão do firmware da impressora.
        /// </summary>
        /// <param name="VersaoFirmware">Variável string com o tamanho de 4 posições para receber a versão do firmware.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_VersaoFirmware([MarshalAs(UnmanagedType.VBByRefStr)] ref string VersaoFirmware);
        /// <summary>
        /// Imprime configurações da impressora fiscal em um relatório gerencial. Será emitida uma leitura X antes. Veja abaixo em "Observações" as informações que serão impressas. 
        /// </summary>
        /// <returns></returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_ImprimeConfiguracoes();

        #endregion

        #region Funções de Autenticação e Gaveta de Dinheiro
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_AcionaGuilhotinaCV0909(int iTipoCorte);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_AcionaGuilhotinaMFD(int iTipoCorte);
        /// <summary>
        /// Permite a autenticação de documentos.
        /// </summary>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_Autenticacao();
        /// <summary>
        /// Programa um caracter gráfico para autenticação.
        /// </summary>
        /// <param name="Parametros">STRING com os 18 valores para programação do caracter gráfico, separados por vírgula. </param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_ProgramaCaracterAutenticacao(string Parametros);
        /// <summary>
        /// Abre a gaveta de dinheiro.
        /// </summary>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_AcionaGaveta();
        /// <summary>
        /// Retorna se a gaveta está fechada ou aberta.
        /// </summary>
        /// <param name="EstadoGaveta">INTEIRO com a Variável para receber o estado da gaveta, onde: 
        ///		<br></br>Estado = 1 sensor em nível 1 (fechada) 
        ///		<br></br>Estado = 0 sensor em nível 0 (aberta) 
        ///	</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_VerificaEstadoGaveta(out int EstadoGaveta);
        #endregion

        #region Funções de Impressão de Cheques
        /// <summary>
        /// Cancela a impressão do cheque que está sendo aguardado pela impressora. O cheque que está em impressão não pode ser cancelado.
        /// </summary>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_CancelaImpressaoCheque();
        /// <summary>
        /// Imprime cheque na impressora MP-40 FI II Bematech e na impressora YANCO 8500.
        /// </summary>
        /// <param name="Banco">STRING com o Número do banco com 3 dígitos.</param>
        /// <param name="Valor">STRING com o Valor do cheque com até 14 dígitos.</param>
        /// <param name="Favorecido">STRING com o Favorecido com até 45 caracteres.</param>
        /// <param name="Cidade">STRING com a Cidade com até 27 caracteres.</param>
        /// <param name="Data">STRING com a Data no formato ddmmaa, dd/mm/aa, ddmmaaaa ou dd/mm/aaaa.</param>
        /// <param name="Mensagem">STRING com o Comentários até 120 caracteres. A mensagem será impressa 1 (uma) linha após a cidade.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_ImprimeCheque(string Banco, string Valor, string Favorecido, string Cidade, string Data, string Mensagem);
        /// <summary>
        /// Imprime cópia do último cheque impresso.
        /// </summary>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_ImprimeCopiaCheque();
        /// <summary>
        /// Inclui o nome da cidade e do favorecido no arquivo de configuração BEMAFI32.INI.
        /// </summary>
        /// <param name="Cidade">STRING com o Nome da cidade com até 27 caracteres.</param>
        /// <param name="Favorecido">STRING com o Nome do favorecido com até 45 caracteres.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_IncluiCidadeFavorecido(string Cidade, string Favorecido);
        /// <summary>
        /// Programa o nome da moeda no plural para a impressão de cheques. Ex. (Reais)
        /// </summary>
        /// <param name="MoedaPlural">STRING com o Nome da moeda no plural com até 22 caracteres.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_ProgramaMoedaPlural(string MoedaPlural);
        /// <summary>
        /// Programa o nome da moeda no singular para a impressão de cheques. Ex. (Real)
        /// </summary>
        /// <param name="MoedaSingular">STRING com o Nome da Moeda no singular com até 19 caracteres.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_ProgramaMoedaSingular(string MoedaSingular);
        /// <summary>
        /// Verifica o status do cheque.
        /// </summary>
        /// <param name="StatusCheque">Variável inteira para receber o status do cheque.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_VerificaStatusCheque(ref int StatusCheque);
        #endregion

        #region Outras Funções
        /// <summary>
        /// Faz a abertura do caixa emitindo um suprimento e uma leitura X. Essa função grava o COO inicial e o Grande Total inicial que serão usados na função Bematech_FI_RelatorioTipo60Mestre. Portanto, se você for emitir o relatório "tipo 60 mestre" é obrigatório o uso dessa função.
        /// </summary>
        /// <param name="Valor">STRING com o Valor do suprimento com até 14 dígitos (2 casas decimais). Informe o valor "0" para não fazer suprimento.</param>
        /// <param name="FormaPagto">STRING com a Forma de pagamento com até 16 caracteres. Se não for informado, o suprimento será feito em Dinheiro.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_AberturaDoDia(string Valor, string FormaPagto);
        /// <summary>
        /// Abre a porta serial para comunicação entre a impressora e o micro. 
        /// </summary>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_AbrePortaSerial();
        /// <summary>
        /// Faz o fechamento do dia emitindo uma Redução Z. Essa função grava o COO final e o Grande Total final que serão usados na função Bematech_FI_RelatorioTipo60Mestre.
        /// </summary>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_FechamentoDoDia();
        /// <summary>
        /// Fecha a porta serial.
        /// </summary>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_FechaPortaSerial();
        /// <summary>
        /// Imprime os departamentos e seus valores acumulados em um relatório gerencial. Será emitida uma leitura X antes. Essas informações eram impressas na leitura X até a versão 3.0 e foram retiradas por solicitação do fisco. 
        /// </summary>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_ImprimeDepartamentos();
        /// <summary>
        /// Gera o relatório "Mapa Resumo" referente ao movimento do dia. As informações serão geradas no arquivo RETORNO.TXT no diretório configurado no parâmetro "path" do arquivo ini. O diretório default configurado é o diretório raiz (C:\).
        /// </summary>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_MapaResumo();
        /// <summary>
        /// Gera o relatório "Tipo 60 analítico" exigido pelo convênio de ICMS 85/2001. As informações serão geradas no arquivo RETORNO.TXT no diretório configurado no parâmetro "path" do arquivo ini. O diretório default é o diretório raiz (C:\).
        /// </summary>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_RelatorioTipo60Analitico();
        /// <summary>
        /// Gera o relatório "Tipo 60 Mestre" exigido pelo convênio de ICMS 85/2001. As informações serão geradas no arquivo RETORNO.TXT no diretório configurado no parâmetro "path" do arquivo ini. O diretório default é o diretório raiz (C:\).
        /// </summary>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_RelatorioTipo60Mestre();
        /// <summary>
        /// Lê o retorno da impressora referente ao último comando enviado. 
        /// </summary>
        /// <param name="ACK">Variável INTEIRA para receber o primeiro byte.</param>
        /// <param name="ST1">Variável INTEIRA para receber o segundo byte.</param>
        /// <param name="ST2">Variável INTEIRA para receber o terceiro byte.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_RetornoImpressora(ref int ACK, ref int ST1, ref int ST2);
        /// <summary>
        /// Verifica se a impressora está ligada ou conectada no computador.
        /// </summary>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_VerificaImpressoraLigada();
        /// <summary>
        /// Reseta a impressora em caso de erro.
        /// </summary>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_ResetaImpressora();
        /// <summary>
        /// Abre o cupom na impressora bilhete de passagem.
        /// </summary>
        /// <param name="ImprimeValorFinal">"1" - Imprime o valor pago no final do cupom. "0" - Não Imprime o valor pago no final do cupom.</param>
        /// <param name="ImprimeEnfatizado">"1" - Imprime as informações "EMBARQUE, POLTRONA e PLATAFORMA" enfatizadas. "0" - Não Imprime as informações enfatizadas (negrito).</param>
        /// <param name="Embarque">STRING com até 40 caracteres com o local de embarque.</param>
        /// <param name="Destino">STRING com até 40 caracteres com o local de destino.</param>
        /// <param name="Linha">STRING com até 40 caracteres com o nome da linha (Ex. Curitiba x São Paulo - Executivo).</param>
        /// <param name="Prefixo">STRING com até 40 caracteres.</param>
        /// <param name="Agente">STRING com até 40 caracteres com o nome do agente.</param>
        /// <param name="Agencia">STRING com até 40 caracteres com o nome da agência.</param>
        /// <param name="Data">STRING com a data de embarque no formato ddmmaa, dd/mm/aa, ddmmaaaa ou dd/mm/aaaa.</param>
        /// <param name="Hora">STIRNG com a hora do embarque no formato hhmmss ou hh:mm:ss.</param>
        /// <param name="Poltrona">STRING com até 2 caracteres com o número da poltrona.</param>
        /// <param name="Plataforma">STRING com até 3 caracteres com o número da poltrona.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_AbreBilhetePassagem(string ImprimeValorFinal, string ImprimeEnfatizado, string Embarque, string Destino, string Linha, string Prefixo, string Agente, string Agencia, string Data, string Hora, string Poltrona, string Plataforma);
        /// <summary>
        /// Imprime um carnê de pagamento.
        /// </summary>
        /// <param name="Titulo">STRING com o titulo para o carnê, impresso centralizado e expandido em cada parcela. Limitado em 20 caracteres.</param>
        /// <param name="Parcelas">STRING com o(s) valor(es) de cada parcela, separadas por ';' (ponto virgula), com duas casas decimais obrigatóriamente. Formatos válidos: "23,23;1.200,00" ou "2323;120000". Ver observações abaixo</param>
        /// <param name="Datas">STRING com a(s) data(s) de vencimento das parcelas separadas por ';'. Formato válidos: "10/10/2003;10112003; ".</param>
        /// <param name="Quantidade">INTEGER com a quantidade de Parcelas. Deve ser diferente de zero.</param>
        /// <param name="Texto">STRING com o texto livre com até 200 caracteres.</param>
        /// <param name="Cliente">STRING com o nome do cliente com até 30 caracteres</param>
        /// <param name="RG_CPF">STRING com o número do RG/CPF do cliente. Pode ser nulo ou vazio.</param>
        /// <param name="Cupom">STRING com o COO do Cupom Fiscal com 6 caracteres.</param>
        /// <param name="Vias">INTEGER com a quantidade de Vias. (1 ou 2 apenas).</param>
        /// <param name="Assina">INTEGER para habilitar ou não a assinatura do cliente, onde: 
        ///		<br></br>1: Habilita a impressão de uma linha tracejada para a assinatura do cliente. 
        ///		<br></br>0: Não habilita a impressão da linha tracejada para a assinatura do cliente. 
        ///	</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_ImpressaoCarne(string Titulo, string Parcelas, string Datas, int Quantidade, string Texto, string Cliente, string RG_CPF, string Cupom, int Vias, int Assina);
        #endregion

        #region Funções para a Impressora Restaurante
        /// <summary>
        /// Abre o cupom de conferência de mesa e imprime os itens registrados nessa mesa. Essa função mantém o cupom de conferência aberto permitindo registrar outros itens na mesa. Só são permitidos registros com o mesmo número da mesa a qual foi aberta o cupom de conferência.
        /// </summary>
        /// <param name="Mesa">STRING com o número da Mesa com até 4 dígitos.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FIR_AbreConferenciaMesa(string Mesa);
        /// <summary>
        /// Abre o cupom fiscal na impressora restaurante e imprime os itens registrados na mesa. Se a mesa for "0000", abre o cupom e aguarda a venda dos itens.
        /// </summary>
        /// <param name="Mesa">STRING com o número da Mesa com até 4 dígitos.</param>
        /// <param name="CGC_CPF">STRING com até 29 caracteres.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FIR_AbreCupomRestaurante(string Mesa, string CGC_CPF);
        /// <summary>
        /// Essa função cancela um registro de venda da mesa informada.
        /// </summary>
        /// <param name="Mesa">STRING com o número da Mesa até 4 dígitos.</param>
        /// <param name="Codigo">STRING com o código do item até 14 dígitos.</param>
        /// <param name="Descricao">STRING com a descrição do item até 17 caracteres.</param>
        /// <param name="Aliquota">STRING com o valor ou o índice da alíquota tributária. Se for o valor deve ser informado com o tamanho de 4 caracteres ou 5 com a vírgula. Se for o índice da alíquota deve ser 2 caracteres. Ex. (18,00 para o valor ou 05 para o índice).</param>
        /// <param name="Quantidade">STRING com até 6 dígitos (são três casas decimais).</param>
        /// <param name="ValorUnitario">STRING com até 8 dígitos (são duas casas decimais).</param>
        /// <param name="FlagAcrescimoDesconto"> "A" para acréscimo ou "D" para desconto.</param>
        /// <param name="ValorAcrescimoDesconto"> STRING com até 8 dígitos (são duas casas decimais). Se não tiver acréscimo nem desconto use "0" no valor.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FIR_CancelaVenda(string Mesa, string Codigo, string Descricao, string Aliquota, string Quantidade, string ValorUnitario, string FlagAcrescimoDesconto, string ValorAcrescimoDesconto);
        /// <summary>
        /// Emite um cupom de conferência de mesa. Essa função reúne as funções Bematech_FIR_AbreConferenciaMesa e Bematech_FIR_FechaConferenciaMesa. Ela abre e fecha o cupom de conferência não permitindo registrar produtos nesse cupom de conferência.
        /// </summary>
        /// <param name="Mesa">STRING com o número da Mesa com até 4 dígitos.</param>
        /// <param name="FlagAcrescimoDesconto">"A" para acréscimo e "D" para desconto.</param>
        /// <param name="TipoAcrescimoDesconto">"$" para acréscimo ou desconto por valor e "%" para percentual.</param>
        /// <param name="ValorAcrescimoDesconto">STRING com no máximo 14 dígitos para acréscimo ou desconto por valor e 4 dígitos para acréscimo ou desconto por percentual (são duas casas decimais).</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FIR_ConferenciaMesa(string Mesa, string FlagAcrescimoDesconto, string TipoAcrescimoDesconto, string ValorAcrescimoDesconto);
        /// <summary>
        /// Permite que a conta seja dividida por todos os clientes. Essa função termina o fechamento do cupom fiscal e imprime um cupom para cada cliente.
        /// </summary>
        /// <param name="NumeroCupons">STRING com até 2 dígitos com o número de cupons em que a conta será divida. O número mínimo de cupons é 2 e o máximo é 20.</param>
        /// <param name="ValorPago">STRING com os valores pagos por cada cliente. Os valores devem ter no máximo 14 dígitos e serem separados por ponto e vírgula ";". Ex.: 10,00; 5,00</param>
        /// <param name="CGC_CPF">STRING com o CPF dos clientes. Os CPF's devem ter no máximo 29 caracteres e serem separados por ponto e vírgula ";"</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FIR_ContaDividida(string NumeroCupons, string ValorPago, string CGC_CPF);
        /// <summary>
        /// Retorna os itens do cardápio pela serial com as seguintes informações: Código, Descrição, Alíquota e Quantidade.
        /// </summary>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FIR_CardapioPelaSerial();
        /// <summary>
        /// Fecha o cupom de conferência de mesa. Essa função permite incluir um acréscimo ou desconto sobre o valor total vendido na mesa. 
        /// </summary>
        /// <param name="FlagAcrescimoDesconto">"A" para acréscimo ou "D" para desconto.</param>
        /// <param name="TipoAcrescimoDesconto">"$" para acréscimo ou desconto por valor e "%" para percentual.</param>
        /// <param name="ValorAcrescimoDesconto">STRING com no máximo 14 dígitos para acréscimo ou desconto por valor e 4 dígitos para acréscimo ou desconto por percentual (são duas casas decimais).</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FIR_FechaConferenciaMesa(string FlagAcrescimoDesconto, string TipoAcrescimoDesconto, string ValorAcrescimoDesconto);
        /// <summary>
        /// Essa função permite fechar o cupom fiscal com formas de pagamento e permite dividir a conta por todos os clientes.
        /// </summary>
        /// <param name="NumeroCupons">STRING com até 2 dígitos com o número de cupons em que a conta será divida. O número mínimo de cupons é 2 e o máximo é 20.</param>
        /// <param name="FlagAcrescimoDesconto">Indica se haverá acréscimo ou desconto no cupom. "A" para acréscimo ou "D" para desconto.</param>
        /// <param name="TipoAcrescimoDesconto">Indica se o acréscimo ou desconto é por valor ou por percentual. "$" para desconto por valor ou "%" para percentual.</param>
        /// <param name="ValorAcrescimoDesconto">STRING com no máximo 14 dígitos para acréscimo ou desconto por valor e 4 dígitos para acréscimo ou desconto percentual.</param>
        /// <param name="FormasPagamento">STRING com as formas de pagamento. As formas devem ser separadas por ponto e vírgula (";") se for utilizada mais de uma, e deve ter no máximo 16 caracteres cada. Ex: Dinheiro;Cartão. É permitido a utilização de até 20 formas.</param>
        /// <param name="ValorFormasPagamento">STRING com os valores das formas de pagamento. Os valores devem ter no máximo 14 dígitos e serem separados por ponto e vírgula ";".</param>
        /// <param name="ValorPagoCliente">STRING com os valores pagos por cada cliente. Obedecem à mesma situação acima.</param>
        /// <param name="CGC_CPF">STRING com o CPF dos clientes. Os CPF's devem ter no máximo 29 caracteres e serem separados por ponto e vírgula ";".</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FIR_FechaCupomContaDividida(string NumeroCupons, string FlagAcrescimoDesconto, string TipoAcrescimoDesconto, string ValorAcrescimoDesconto, string FormasPagamento, string ValorFormasPagamento, string ValorPagoCliente, string CGC_CPF);
        /// <summary>
        /// Fecha o cupom fiscal na impressora restaurante com acréscimo ou desconto, usando apenas uma forma de pagamento.
        /// </summary>
        /// <param name="FormaPagamento">STRING com o nome da forma de pagamento com no máximo 16 caracteres.</param>
        /// <param name="FlagAcrescimoDesconto">Indica se haverá acréscimo ou desconto no cupom. "A" para acréscimo ou "D" para desconto.</param>
        /// <param name="TipoAcrescimoDesconto">Indica se o acréscimo ou desconto é por valor ou por percentual. "$" para desconto por valor ou "%" para percentual.</param>
        /// <param name="ValorAcrescimoDesconto">STRING com no máximo 14 dígitos para acréscimo ou desconto por valor e 4 dígitos para acréscimo ou desconto por percentual.</param>
        /// <param name="ValorFormaPagto">STRING com o Valor pago com no máximo 14 dígitos.</param>
        /// <param name="Mensagem">STRING com a Mensagem promocional com até 490 caracteres.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FIR_FechaCupomRestaurante(string FormaPagamento, string FlagAcrescimoDesconto, string TipoAcrescimoDesconto, string ValorAcrescimoDesconto, string ValorFormaPagto, string Mensagem);
        /// <summary>
        ///Imprime os itens do cardápio com as seguintes informações: Código, Descrição, Alíquota e Quantidade. 
        /// </summary>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FIR_ImprimeCardapio();
        /// <summary>
        /// Faz um registro de venda na mesa informada e cadastra o item no cardápio com o código informado se ele ainda não existir.
        /// </summary>
        /// <param name="Mesa">STRING com o número da Mesa até 4 dígitos.</param>
        /// <param name="Codigo">STRING com o código do item até 14 dígitos.</param>
        /// <param name="Descricao">STRING com a descrição do item até 17 caracteres.</param>
        /// <param name="Aliquota">STRING com o valor ou o índice da alíquota tributária. Se for o valor deve ser informado com o tamanho de 4 caracteres ou 5 com a vírgula. Se for o índice da alíquota deve ser 2 caracteres. Ex. (18,00 para o valor ou 05 para o índice).</param>
        /// <param name="Quantidade">STRING com até 6 dígitos (são três casas decimais).</param>
        /// <param name="ValorUnitario">STRING com até 8 dígitos (são duas casas decimais).</param>
        /// <param name="FlagAcrescimoDesconto">"A" para acréscimo ou "D" para desconto.</param>
        /// <param name="ValorAcrescimoDesconto">STRING com até 8 dígitos (são duas casas decimais). Se não tiver acréscimo nem desconto use "0" no valor.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FIR_RegistraVenda(string Mesa, string Codigo, string Descricao, string Aliquota, string Quantidade, string ValorUnitario, string FlagAcrescimoDesconto, string ValorAcrescimoDesconto);
        /// <summary>
        /// Retorna os registros de venda da mesa pela porta serial.
        /// </summary>
        /// <param name="Mesa">STRING com o número da Mesa com até 4 dígitos.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FIR_RegistroVendaSerial(string Mesa);
        /// <summary>
        /// Imprime um relatório das mesas que estão abertas.
        /// </summary>
        /// <param name="TipoRelatorio">INTEIRO, onde: 0 para relatorio parcial ou 1 para relatório completo.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FIR_RelatorioMesasAbertas(int TipoRelatorio);
        /// <summary>
        /// Retorna, pela porta serial da impressora, o relatório das mesas que estão abertas.
        /// </summary>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FIR_RelatorioMesasAbertasSerial();
        /// <summary>
        /// Permite a transferência parcial ou total dos itens registrados em uma mesa para outra.
        /// </summary>
        /// <param name="MesaOrigem">STRING com o número da Mesa de origem com até 4 dígitos.</param>
        /// <param name="Codigo">STRING com o código do item com até 14 dígitos.</param>
        /// <param name="Descricao">STRING com a Descrição do item com até 17 caracteres.</param>
        /// <param name="Aliquota">STRING com o valor ou o índice da alíquota tributária. Se for o valor deve ser informado com o tamanho de 4 caracteres ou 5 com a vírgula. Se for o índice da alíquota deve ser 2 caracteres. Ex. (18,00 para o valor ou 05 para o índice ).</param>
        /// <param name="Quantidade">STRING com até 6 dígitos (são três casas decimais).</param>
        /// <param name="ValorUnitario">STRING com até 8 dígitos (são duas casas decimais).</param>
        /// <param name="FlagAcrescimoDesconto">"A" para acréscimo e "D" para desconto.</param>
        /// <param name="ValorAcrescimoDesconto"> STRING com até 8 dígitos (são duas casas decimais). Se não tiver acréscimo nem desconto use "0" no valor.</param>
        /// <param name="MesaDestino">STRING com o número da Mesa de destino com até 4 dígitos.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FIR_TransferenciaItem(string MesaOrigem, string Codigo, string Descricao, string Aliquota, string Quantidade, string ValorUnitario, string FlagAcrescimoDesconto, string ValorAcrescimoDesconto, string MesaDestino);
        /// <summary>
        /// Faz a transferência dos registros de venda da mesa de origem para a mesa de destino, se a mesa de destino já tiver itens registrados os registros serão acrescentados. 
        /// </summary>
        /// <param name="MesaOrigem">STRING com o código da Mesa de origem com até 4 dígitos.</param>
        /// <param name="MesaDestino">STRING com o código da Mesa de destino com até 4 dígitos.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FIR_TransferenciaMesa(string MesaOrigem, string MesaDestino);
        /// <summary>
        /// Retorna a quantidade de bytes livres na memória da impressora para registros de venda ou itens de cardápio.
        /// </summary>
        /// <param name="Bytes">Variável string com o tamanho de 6 posições para o valor correspondente ao bytes de memória livres.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FIR_VerificaMemoriaLivre(string Bytes);
        /// <summary>
        /// Permite fechar o cupom de forma resumida, ou seja, sem acréscimo ou desconto no cupom e com apenas uma forma de pagamento. Essa função lê o subtotal do cupom para fechá-lo.
        /// </summary>
        /// <param name="FormaPagamento">STRING com a Forma de pagamento com no máximo 16 caracteres.</param>
        /// <param name="Mensagem">STRING com a Mensagem promocional com até 490 caracteres.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FIR_FechaCupomResumidoRestaurante(string FormaPagamento, string Mensagem);
        #endregion

        #region Funções da Impressora Fiscal MFD
        /// <summary>
        /// Abre o cupom na impressora bilhete de passagem MFD.
        /// </summary>
        /// <param name="Embarque">STRING com até 40 caracteres com o local de embarque.</param>
        /// <param name="Destino">STRING com até 40 caracteres com o local de destino.</param>
        /// <param name="Linha">STRING com até 40 caracteres com o nome da linha (Ex. Curitiba x São Paulo – Executivo</param>
        /// <param name="Agencia">STRING com até 40 caracteres com o nome da agência.</param>
        /// <param name="Data">STRING com a data de embarque no formato ddmmaa, dd/mm/aa, ddmmaaaa ou dd/mm/aaaa.</param>
        /// <param name="Hora">STRING com a hora do embarque no formato hhmmss ou hh:mm:ss.</param>
        /// <param name="Poltrona">STRING com até 2 caracteres com o número da poltrona.</param>
        /// <param name="Plataforma">STRING com até 3 caracteres com o número da plataforma.</param>
        /// <param name="TipoPassagem"> STRING com: 
        ///		<br></br>0 (zero) - passagem Rodoviário Intermunicipal; 
        ///		<br></br>1 (um) - passagem Ferroviário Intermunicipal; 
        ///		<br></br>2 (dois) - passagem Aquaviário Intermunicipal; 
        ///		<br></br>3 (três) - passagem Rodoviário Interestadual; 
        ///		<br></br>4 (quatro) - passagem Ferroviário Interestadual; 
        ///		<br></br>5 (cinco) - passagem Aquaviário Interestadual; 
        ///		<br></br>6 (seis) - passagem Rodoviário Internacional; 
        ///		<br></br>7 (sete) - passagem Ferroviário Internacional ou; 
        ///		<br></br>8 (oito) - passagem Aquaviário Internacional. 
        /// </param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_AbreBilhetePassagemMFD(string Embarque, string Destino, string Linha, string Agencia, string Data, string Hora, string Poltrona, string Plataforma, string TipoPassagem);
        /// <summary>
        /// Abre o Comprovante Não Fiscal Vinculado
        /// </summary>
        /// <param name="FormaPagamento">STRING com a Forma de Pagamento com até 16 caracteres.</param>
        /// <param name="Valor"> STRING com o Valor Pago na forma de pagamento do cupom a que se refere o comprovante, com até 14 dígitos (2 casas decimais).</param>
        /// <param name="NumeroCupom">STRING com o Número do cupom a que se refere o comprovante com até 6 dígitos</param>
        /// <param name="CGC">STRING com até 29 caracteres com o CGC ou CPF do cliente.</param>
        /// <param name="nome">STRING com até 30 caracteres com o nome do cliente.</param>
        /// <param name="Endereco">STRING com até 80 caracteres com o endereço do cliente. </param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_AbreComprovanteNaoFiscalVinculadoMFD(string FormaPagamento, string Valor, string NumeroCupom, string CGC, string nome, string Endereco);
        /// <summary>
        /// Abre o cupom fiscal na impressora MFD. 
        /// </summary>
        /// <param name="CGC">STRING até 29 caracteres com o CGC ou CPF do cliente.</param>
        /// <param name="Nome">STRING até 30 caracteres com o nome do cliente.</param>
        /// <param name="Endereco">STRING até 80 caracteres com o endereço do cliente.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_AbreCupomMFD(string CGC, string Nome, string Endereco);
        /// <summary>
        /// Abre o comprovante não fiscal não vinculado para que sejam lançados os recebimentos não fiscais.
        /// </summary>
        /// <param name="CGC">STRING até 29 caracteres com o CGC ou CPF do cliente.</param>
        /// <param name="Nome">STRING até 30 caracteres com o nome do cliente.</param>
        /// <param name="Endereco">STRING até 80 caracteres com o endereço do cliente.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_AbreRecebimentoNaoFiscalMFD(string CGC, string Nome, string Endereco);
        /// <summary>
        /// Abre Relatório Gerencial, na impressora fiscal MFD.
        /// </summary>
        /// <param name="Indice">STRING numérica com o valor entre 1 e 30, com o índice do relatório.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        /// Public int Bematech_FI_GeraRegistrosSpedMFD (string arq_origem, string arq_destino, string data_inicial, string data_final, string perfil, string CFOP, string lac_fiscal, string pis, string cofins)
        /// 
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_GeraRegistrosSpedMFD(string arq_origem, string arq_destino, string data_inicial, string data_final, string perfil, string CFOP, string lac_fiscal, string pis, string cofins);

        /*Aperto ===> Public Declare Function Bematech_FI_GeraRegistrosSpedCompleto Lib "BEMAFI32.DLL" 
        ( ByVal cArquivoMFD as string,  ByVal cArquivoTXT as string, ByVal cDataInicial as string, ByVal cDataFinal as string, 
         * ByVal cPerfil as string, ByVal cCFOP as string, ByVal cCODOBSFiscal as string, ByVal cAliqPIS as string, ByVal cAliqCOFINS as string,
         * ByVal cEmpresa as string, ByVal cCodMunicipio as string ) as Integer*/

        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_GeraRegistrosSpedCompleto(string cArquivoMFD, string cArquivoTXT, string cDataInicial, string cDataFinal, string cPerfil, string cCFOP, string cCODOBSFiscal, string cAliqPIS, string cAliqCOFINS, string cEmpresa, string cCodMunicipio);

        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_AbreRelatorioGerencialMFD(string Indice);
        /// <summary>
        /// Efetua acréscimo ou desconto em qualquer item enquanto o cupom fiscal não estiver totalizado.
        /// </summary>
        /// <param name="Item">STRING numérica até 3 dígitos com o número do item.</param>
        /// <param name="AcrescimoDesconto">Indica se é acréscimo ou desconto. 'A' para acréscimo ou 'D' para desconto.</param>
        /// <param name="TipoAcrescimoDesconto">Indica se o acréscimo ou desconto é por valor ou por percentual. '$' para desconto por valor e '%' para percentual.</param>
        /// <param name="ValorAcrescimoDesconto">STRING com no máximo 14 dígitos para acréscimo ou desconto por valor e 4 dígitos para acréscimo ou desconto percentual.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_AcrescimoDescontoItemMFD(string Item, string AcrescimoDesconto, string TipoAcrescimoDesconto, string ValorAcrescimoDesconto);
        /// <summary>
        /// Efetua acréscimo ou desconto em subtotal do recebimento não fiscal.
        /// </summary>
        /// <param name="cFlag">STRING com "A" para Acréscimo ou "'D" para Desconto.</param>
        /// <param name="cTipo">STRING com "$" para acréscimo ou desconto por valor, ou "%" para acréscimo ou desconto por percentual.</param>
        /// <param name="cValor">STRING com no máximo 14 dígitos para o valor ou 4 dígitos para o percentual.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_AcrescimoDescontoSubtotalRecebimentoMFD(string cFlag, string cTipo, string cValor);
        /// <summary>
        /// Efetua acréscimo ou desconto em subtotal do cupom. 
        /// </summary>
        /// <param name="cFlag">STRING com "A" para Acréscimo ou "D" para Desconto.</param>
        /// <param name="cTipo">STRING com "$" para Acréscimo ou Desconto por valor, ou "%" para Acréscimo ou Desconto percentual.</param>
        /// <param name="cValor">STRING com o valor no máximo de 14 dígitos para Acréscimo ou Desconto, ou valor com 4 dígitos para Acréscimo ou Desconto por percentual.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_AcrescimoDescontoSubtotalMFD(string cFlag, string cTipo, string cValor);
        /// <summary>
        /// Permite a autenticação de documentos.
        /// </summary>
        /// <param name="Linhas">STRING numérica com valor entre 1 e 99 com o número de linhas que serão saltadas para imprimir o texto.</param>
        /// <param name="Texto">STRING com até 48 caracteres com o texto a ser impresso.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_AutenticacaoMFD(string Linhas, string Texto);
        /// <summary>
        /// Cancela a acréscimo ou a desconto dado no item.
        /// </summary>
        /// <param name="cFlag">STRING com "A" para cancelar o Acréscimo ou "D" para cancelar o Desconto.</param>
        /// <param name="cItem">STRING de até 3 dígitos com o número do item a ser cancelado restrito aos 300 últimos registros efetuados.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_CancelaAcrescimoDescontoItemMFD(string cFlag, string cItem);
        /// <summary>
        /// Cancela acréscimo e desconto efetuados em subtotal do cupom. 
        /// </summary>
        /// <param name="cFlag">STRING com "A" para cancelar o Acréscimo ou "D" para cancelar o Desconto, dado no subtotal.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_CancelaAcrescimoDescontoSubtotalMFD(string cFlag);
        /// <summary>
        /// Cancela acréscimo e desconto efetuados em subtotal do recebimento não fiscal.
        /// </summary>
        /// <param name="cFlag">STRING com "A" para cancelar o Acréscimo ou "D" para cancelar o Desconto, dado no subtotal do recebimento.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_CancelaAcrescimoDescontoSubtotalRecebimentoMFD(string cFlag);
        /// <summary>
        /// Cancela o último cupom emitido.
        /// </summary>
        /// <param name="CGC">STRING até 29 caracteres com o CGC ou CPF do cliente. </param>
        /// <param name="Nome">STRING até 30 caracteres com o nome do cliente.</param>
        /// <param name="Endereco">STRING até 80 caracteres com o endereço do cliente.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_CancelaCupomMFD(string CGC, string Nome, string Endereco);
        /// <summary>
        /// Cancela o recebimento não fiscal.
        /// </summary>
        /// <param name="CGC">STRING até 29 caracteres com o CGC ou CPF do cliente</param>
        /// <param name="Nome">STRING até 30 caracteres com o nome do cliente.</param>
        /// <param name="Endereco">STRING até 80 caracteres com o endereço do cliente.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_CancelaRecebimentoNaoFiscalMFD(string CGC, string Nome, string Endereco);
        /// <summary>
        /// Retorna o número de comprovantes não fiscais não emitidos.
        /// </summary>
        /// <param name="Comprovantes">Variável STRING com o tamanho de 4 posições para receber a informação.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_ComprovantesNaoFiscaisNaoEmitidosMFD(string Comprovantes);
        /// <summary>
        ///  Retorna o CNPJ do cliente cadastrado na impressora. 
        /// </summary>
        /// <param name="CNPJ">Variável STRING com o tamanho de 20 posições para receber o CNPJ.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_CNPJMFD(string CNPJ);
        /// <summary>
        /// Retorna o número de comprovantes de crédito emitidos. 
        /// </summary>
        /// <param name="Comprovantes">Variável STRING com o tamanho de 4 posições para receber a informação.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_ContadorComprovantesCreditoMFD([MarshalAs(UnmanagedType.VBByRefStr)] ref string Comprovantes);
        /// <summary>
        /// Retorna o número de cupons fiscais emitidos.
        /// </summary>
        /// <param name="CuponsEmitidos">Variável STRING com o tamanho de 6 posições para receber a informação</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_ContadorCupomFiscalMFD([MarshalAs(UnmanagedType.VBByRefStr)] ref string CuponsEmitidos);
        /// <summary>
        /// Retorna o número de vezes em que foi impressa a fita detalhe. 
        /// </summary>
        /// <param name="ContadorFita">Variável STRING com o tamanho de 6 posições para receber a informação.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_ContadorFitaDetalheMFD(string ContadorFita);
        /// <summary>
        /// Retorna o número de operações não fiscais canceladas. 
        /// </summary>
        /// <param name="OperacoesCanceladas">Variável STRING com o tamanho de 4 posições para receber a informação.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_ContadorOperacoesNaoFiscaisCanceladasMFD(string OperacoesCanceladas);
        /// <summary>
        /// Retorna o número de relatórios gerenciais emitidos.
        /// </summary>
        /// <param name="Relatorios">Variável STRING com o tamanho de 6 posições para receber a informação</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_ContadorRelatoriosGerenciaisMFD([MarshalAs(UnmanagedType.VBByRefStr)] ref string Relatorios);
        /// <summary>
        /// Retorna o número de vezes em que os totalizadores não sujeitos ao ICMS foram usados.
        /// </summary>
        /// <param name="Contadores">Variável STRING com 149 posições para receber as informações. </param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_ContadoresTotalizadoresNaoFiscaisMFD(string Contadores);
        /// <summary>
        /// Emite um cupom adicional com as informações do COO e valor do cupom fiscal.
        /// </summary>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_CupomAdicionalMFD();
        /// <summary>
        /// Retorna os dados da impressora no momento da última redução Z.
        /// </summary>
        /// <param name="DadosReducao">Variável STRING com o tamanho de 1278 posições para receber os dados da última redução.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_DadosUltimaReducaoMFD([MarshalAs(UnmanagedType.VBByRefStr)] ref string DadosReducao);
        /// <summary>
        /// Retorna a data e hora do último documento armazenado na MFD no formato dd/mm/aa hh/mm/ss (sem barras e espaço). 
        /// </summary>
        /// <param name="cDataHora">Variável STRING com o tamanho de 12 posições para receber os dados.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_DataHoraUltimoDocumentoMFD(string cDataHora);
        /// <summary>
        /// Imprime a(s) forma(s) de pagamento e o(s) valor(es) pago(s) nessa forma.
        /// </summary>
        /// <param name="FormaPagamento">STRING com a forma de pagamento com no máximo 16 caracteres.</param>
        /// <param name="ValorFormaPagamento">STRING com o valor da forma de pagamento com até 14 dígitos.</param>
        /// <param name="Parcelas">STRING numérica entre 1 e 24 com o número de parcelas em que o pagamento será realizado.</param>
        /// <param name="DescricaoFormaPagto">STRING com a descrição da forma de pagamento com no máximo 80 caracteres</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_EfetuaFormaPagamentoMFD(string FormaPagamento, string ValorFormaPagamento, string Parcelas, string DescricaoFormaPagto);
        /// <summary>
        /// Efetua o recebimento não fiscal.
        /// </summary>
        /// <param name="IndiceTotalizador">STRING com o Índice do Totalizador com até 2 dígitos para o recebimento.</param>
        /// <param name="ValorRecebimento">STRING com o Valor do recebimento com até 14 dígitos (duas casas decimais).</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_EfetuaRecebimentoNaoFiscalMFD(string IndiceTotalizador, string ValorRecebimento);
        /// <summary>
        /// Estorna os lançamentos de um comprovante de crédito ou débito vinculado. Deve ser executado imediatamente após a impressão do comprovante vinculado.
        /// </summary>
        /// <param name="CGC">STRING até 29 caracteres com o CGC ou CPF do cliente.</param>
        /// <param name="Nome">STRING até 30 caracteres com o nome do cliente.</param>
        /// <param name="Endereco">STRING até 80 caracteres com o endereço do cliente.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_EstornoNaoFiscalVinculadoMFD(string CGC, string Nome, string Endereco);
        /// <summary>
        /// Termina o fechamento do recebimento não fiscal. 
        /// </summary>
        /// <param name="Mensagem">STRING com a Mensagem promocional com até 490 caracteres.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_FechaRecebimentoNaoFiscalMFD(string Mensagem);
        /// <summary>
        /// Habilita e desabilita o retorno estendido na MFD. O retorno estendido é ACK, ST1, ST2 e ST3. Caso não seja habilitado, será retornado apenas ACK, ST1 e ST2 como na impressora fiscal matricial MP-20 FI II ou MP-40 FI II.
        /// </summary>
        /// <param name="FlagRetorno">STRING com o valor um (1) para habilitar ou zero (0) para desabilitar o retorno estendido.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_HabilitaDesabilitaRetornoEstendidoMFD(string FlagRetorno);
        /// <summary>
        /// Imprime cheque na impressora MFD. Somente na impressora MP 6000.
        /// </summary>
        /// <param name="NumeroBanco">STRING com o Número do banco com 3 dígitos.</param>
        /// <param name="Valor">STRING com o Valor do cheque com até 14 dígitos.</param>
        /// <param name="Favorecido">STRING com o Favorecido com até 45 caracteres.</param>
        /// <param name="Cidade">STRING com a Cidade com até 27 caracteres.</param>
        /// <param name="Data">STRING com a Data no formato ddmmaa, dd/mm/aa, ddmmaaaa ou dd/mm/aaaa.</param>
        /// <param name="Mensagem">STRING com Comentários até 120 caracteres. A mensagem será impressa uma (1) linha após a cidade caso não tenha sido indicada para impressão no verso.</param>
        /// <param name="ImpressaoVerso">STRING com o valor zero (0) para impressão da mensagem na frente do cheque e o valor um (1) para impressão no verso.</param>
        /// <param name="Linhas">STRING com um valor entre 0 e 35 com o número de linhas a serem saltadas antes da impressão da mensagem (só é utilizada na impressão da mensagem no verso).</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_ImprimeChequeMFD(string NumeroBanco, string Valor, string Favorecido, string Cidade, string Data, string Mensagem, string ImpressaoVerso, string Linhas);
        /// <summary>
        /// Inicia o fechamento do cupom fiscal. Permite acréscimo e desconto no fechamento do cupom.
        /// </summary>
        /// <param name="AcrescimoDesconto">STRING que indica se haverá acréscimo no cupom, desconto ou ambos. "A" para acréscimo, "D" para desconto e "X" para acréscimo e desconto.</param>
        /// <param name="TipoAcrescimoDesconto">STRING que indica se o acréscimo ou desconto é por valor ou por percentual. "$" para desconto por valor e "%" para percentual.</param>
        /// <param name="ValorAcrescimo">STRING com no máximo 14 dígitos para acréscimo por valor e 4 dígitos para acréscimo percentual.</param>
        /// <param name="ValorDesconto">STRING com no máximo 14 dígitos para desconto por valor e 4 dígitos para desconto percentual.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_IniciaFechamentoCupomMFD(string AcrescimoDesconto, string TipoAcrescimoDesconto, string ValorAcrescimo, string ValorDesconto);
        /// <summary>
        /// Inicia o fechamento do recebimento não fiscal. Permite acréscimo e desconto no fechamento do recebimento.
        /// </summary>
        /// <param name="AcrescimoDesconto">STRING que indica se haverá acréscimo no cupom, desconto ou ambos. "A" para acréscimo, "D" para desconto e "X" para acréscimo e desconto.</param>
        /// <param name="TipoAcrescimoDesconto">STRING que indica se o acréscimo ou desconto é por valor ou por percentual. "$" para desconto por valor e "%" para percentual.</param>
        /// <param name="ValorAcrescimo">STRING com no máximo 14 dígitos para acréscimo por valor e 4 dígitos para acréscimo percentual.</param>
        /// <param name="ValorDesconto">STRING com no máximo 14 dígitos para desconto por valor e 4 dígitos para desconto percentual.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_IniciaFechamentoRecebimentoNaoFiscalMFD(string AcrescimoDesconto, string TipoAcrescimoDesconto, string ValorAcrescimo, string ValorDesconto);
        /// <summary>
        /// Retorna a incrição estadual do cliente cadatrada na impressora.
        /// </summary>
        /// <param name="InscricaoEstadual">Variável STRING com o tamanho de 20 posições para receber a informação.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_InscricaoEstadualMFD(string InscricaoEstadual);
        /// <summary>
        /// Retorna a incrição municipal do cliente cadatrada na impressora.
        /// </summary>
        /// <param name="InscricaoMunicipal">Variável STRING com o tamanho de 20 posições para receber a informação.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_InscricaoMunicipalMFD(string InscricaoMunicipal);
        /// <summary>
        /// Realiza a leitura do código CMC7 do cheque. 
        /// </summary>
        /// <param name="CodigoCMC7">Variável STRING com 36 posições para receber o código CMC7.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_LeituraChequeMFD(string CodigoCMC7);
        /// <summary>
        /// Emite a leitura da memória fiscal da impressora por intervalo de datas.
        /// </summary>
        /// <param name="DataInicial">STRING para receber a Data inicial no formato ddmmaa, dd/mm/aa, ddmmaaaa ou dd/mm/aaaa.</param>
        /// <param name="DataFinal">STRING para receber a Data final no formato ddmmaa, dd/mm/aa, ddmmaaaa ou dd/mm/aaaa.</param>
        /// <param name="FlagLeitura">STRING com o valor "s" para leitura simplificada e "c" para leitura completa.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_LeituraMemoriaFiscalDataMFD(string DataInicial, string DataFinal, string FlagLeitura);
        /// <summary>
        /// Emite a leitura da memória fiscal da impressora por intervalo de reduções.
        /// </summary>
        /// <param name="ReducaoInicial">Emite a leitura da memória fiscal da impressora por intervalo de reduções</param>
        /// <param name="ReducaoFinal">STRING com o Número da reducao final com até 4 dígitos. </param>
        /// <param name="FlagLeitura">STRING com o valor "s" para leitura simplificada e "c" para leitura completa.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_LeituraMemoriaFiscalReducaoMFD(string ReducaoInicial, string ReducaoFinal, string FlagLeitura);
        /// <summary>
        /// Recebe os dados da memória fiscal por intervalo de datas pela serial e grava em arquivo texto.
        /// </summary>
        /// <param name="DataInicial">STRING para receber a Data inicial no formato ddmmaa, dd/mm/aa, ddmmaaaa ou dd/mm/aaaa.</param>
        /// <param name="DataFinal">STRING para receber a Data final no formato ddmmaa, dd/mm/aa, ddmmaaaa ou dd/mm/aaaa.</param>
        /// <param name="FlagLeitura">STRING com o valor "s" para leitura simplificada e "c" para leitura completa.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_LeituraMemoriaFiscalSerialDataMFD(string DataInicial, string DataFinal, string FlagLeitura);
        /// <summary>
        /// Recebe os dados da leitura da memória fiscal por intervalo de reduções pela serial e grava em arquivo texto.
        /// </summary>
        /// <param name="ReducaoInicial">STRING com o Número da reducao inicial com até 4 dígitos.</param>
        /// <param name="ReducaoFinal">STRING com o Número da reducao final com até 4 dígitos.</param>
        /// <param name="FlagLeitura">STRING com o valor "s" para leitura simplificada e "c" para leitura completa.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_LeituraMemoriaFiscalSerialReducaoMFD(string ReducaoInicial, string ReducaoFinal, string FlagLeitura);
        /// <summary>
        /// Gera o relatório "Mapa Resumo" referente ao movimento do dia. As informações serão geradas no arquivo RETORNO.TXT no diretório configurado no parâmetro "path" do arquivo ini. O diretório default configurado é o diretório raiz (C:\).
        /// </summary>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_MapaResumoMFD();
        /// <summary>
        /// Retorna a marca, o modelo e o tipo da impressora.
        /// </summary>
        /// <param name="Marca">Variável STRING com 15 posições para receber a marca da impressora.</param>
        /// <param name="Modelo">Variável STRING com 20 posições para receber o modelo.</param>
        /// <param name="Tipo">Variável STRING com 7 posições para receber o tipo da impressora.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_MarcaModeloTipoImpressoraMFD([MarshalAs(UnmanagedType.VBByRefStr)] ref string Marca, [MarshalAs(UnmanagedType.VBByRefStr)] ref string Modelo, [MarshalAs(UnmanagedType.VBByRefStr)] ref  string Tipo);
        /// <summary>
        /// Retorna o tempo em que a impressora emitiu documentos fiscais.
        /// </summary>
        /// <param name="Minutos">Variável STRING com o tamanho de 4 posições para receber a informação.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_MinutosEmitindoDocumentosFiscaisMFD(string Minutos);
        /// <summary>
        /// Programa Relatório Gerencial. A impressora possui um relatório default pré-programado: "Abertura de Caixa", no índice "01".
        /// </summary>
        /// <param name="Indice">STRING numérica com valor entre 2 e 30 para o índice do relatório.</param>
        /// <param name="Descricao">STRING com até 17 caracteres com o nome do relatório.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_NomeiaRelatorioGerencialMFD(string Indice, string Descricao);

        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_DataHoraGravacaoUsuarioSWBasicoMFAdicional([MarshalAs(UnmanagedType.VBByRefStr)] ref string dataUsuario, [MarshalAs(UnmanagedType.VBByRefStr)] ref string dataSWBasico, [MarshalAs(UnmanagedType.VBByRefStr)] ref string MFAdicional);

        /// <summary>
        /// Retorna o número de série da impressora MFD. 
        /// </summary>
        /// <param name="NumeroSerie">Variável STRING com o tamanho de 20 posições para receber a informação.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_NumeroSerieMFD([MarshalAs(UnmanagedType.VBByRefStr)] ref string NumeroSerie);
        /// <summary>
        /// Retorna o número de série da memória de fita detalhe (MFD). 
        /// </summary>
        /// <param name="NumeroSerieMFD">Variável STRING com o tamanho de 20 posições para receber a informação.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_NumeroSerieMemoriaMFD(string NumeroSerieMFD);
        /// <summary>
        /// Retorna o percentual livre da Memória Fita Detalhe (MFD) no formato XX,XX% (com a virgula e o %). 
        /// </summary>
        /// <param name="cMemoriaLivre">Variável STRING com o tamanho de 6 posições para receber os dados.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_PercentualLivreMFD(string cMemoriaLivre);
        /// <summary>
        /// Programa as formas de pagamento. 
        /// </summary>
        /// <param name="FormaPagto">STRING até 16 caracteres com a forma de pagamento.</param>
        /// <param name="OperacaoTef">STRING com 0 (zero) ou 1 (um) indicando se a forma de pagamento permite operação TEF ou não, onde: 
        ///		<br></br>1 - permite operação TEF 
        ///		<br></br>0 - não permite operação TEF. 
        ///	</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_ProgramaFormaPagamentoMFD(string FormaPagto, string OperacaoTef);
        /// <summary>
        /// Retorna o número de reduções restantes na impressora.
        /// </summary>
        /// <param name="Reducoes">Variável STRING com o tamanho de 4 posições para receber a informação.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_ReducoesRestantesMFD(string Reducoes);
        /// <summary>
        /// Reimprime o comprovante não fiscal vinculado. Será executado, somente, se o comando for enviado imediatamente após a impressão do comprovante.
        /// </summary>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_ReimpressaoNaoFiscalVinculadoMFD();
        /// <summary>
        /// Gera o relatório "Tipo 60 analítico" exigido pelo convênio de ICMS 85/2001. As informações serão geradas no arquivo RETORNO.TXT no diretório configurado no parâmetro "path" do arquivo ini. O diretório default é o diretório raiz (C:\).
        /// </summary>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_RelatorioTipo60AnaliticoMFD();
        /// <summary>
        /// Lê o retorno estendido da impressora (ACK, ST1, ST2 e ST3) referente ao último comando enviado.
        /// </summary>
        /// <param name="ACK">Variável inteira para receber o primeiro bytes de status da impressora.</param>
        /// <param name="ST1">Variável inteira para receber o segundo bytes de status da impressora.</param>
        /// <param name="ST2">Variável inteira para receber o terceiro bytes de status da impressora.</param>
        /// <param name="ST3">Variável inteira para receber o quarto bytes de status da impressora.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_RetornoImpressoraCV0909(ref int ACK, ref int ST1, ref int ST2, ref int ST3, ref int ST4);
        /// <summary>
        /// Lê o retorno estendido da impressora (ACK, ST1, ST2 e ST3) referente ao último comando enviado.
        /// </summary>
        /// <param name="ACK">Variável inteira para receber o primeiro bytes de status da impressora.</param>
        /// <param name="ST1">Variável inteira para receber o segundo bytes de status da impressora.</param>
        /// <param name="ST2">Variável inteira para receber o terceiro bytes de status da impressora.</param>
        /// <param name="ST3">Variável inteira para receber o quarto bytes de status da impressora.</param>
        /// /// <param name="ST4">Variável inteira para receber o quinto bytes de status da impressora.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_RetornoImpressoraMFD(ref int ACK, ref int ST1, ref int ST2, ref int ST3);
        /// <summary>
        /// Imprime a segunda via do comprovante não fiscal vinculado. Deve ser executada imediatamente após a emissão da primeira via.
        /// </summary>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_SegundaViaNaoFiscalVinculadoMFD();
        /// <summary>
        /// Subtotaliza o cupom fiscal, ou seja, inicia o fechamento imprimindo o valor total do cupom.
        /// </summary>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_SubTotalizaCupomMFD();
        /// <summary>
        /// Subtotaliza o recebimento não fiscal (comprovante não fiscal não vinculado), ou seja, inicia o fechamento imprimindo o valor total do recebimento.
        /// </summary>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_SubTotalizaRecebimentoMFD();
        /// <summary>
        /// Retorna o quantidade de bytes livres na MFD.
        /// </summary>
        /// <param name="cMemoriaLivre">Variável STRING com o tamanho de 10 posições para receber os dados.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_TotalLivreMFD(string cMemoriaLivre);
        /// <summary>
        /// Retorna o tamanho total da MFD em bytes.
        /// </summary>
        /// <param name="cTamanhoMFD">Variável STRING com o tamanho de 10 posições para receber os dados.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_TamanhoTotalMFD(string cTamanhoMFD);
        /// <summary>
        /// Retorna o tempo em que a impressora está operacional.
        /// </summary>
        /// <param name="TempoOperacional">Variável STRING com o tamanho de 4 posições para receber a informação.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_TempoOperacionalMFD(string TempoOperacional);
        /// <summary>
        /// Totaliza o cupom fiscal habilitando o uso das formas de pagamento.
        /// </summary>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_TotalizaCupomMFD();
        /// <summary>
        /// Totaliza o recebimento não fiscal habilitando o uso das formas de pagamento.
        /// </summary>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_TotalizaRecebimentoMFD();
        /// <summary>
        /// Imprime as informações do Relatório Gerencial.
        /// </summary>
        /// <param name="Texto">STRING Texto a ser impresso no relatório com até 618 caracteres. </param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_UsaRelatorioGerencialMFD(string Texto);
        /// <summary>
        /// Retorna o valor acumulado em uma determinada forma de pagamento
        /// </summary>
        /// <param name="Forma">Variável STRING com até 16 posições com a descrição da Forma de Pagamento que deseja retornar o seu valor.</param>
        /// <param name="ValorForma">Variável STRING com 14 posições para receber a informação.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_ValorFormaPagamentoMFD(string Forma, string ValorForma);
        /// <summary>
        /// Retorna o valor acumulado em um determinado totalizador não fiscal.
        /// </summary>
        /// <param name="Totalizador">Variável STRING com até 19 posições com a descrição do Totalizador.</param>
        /// <param name="ValorTotalizador">Variável STRING com 14 posições para receber a informação.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_ValorTotalizadorNaoFiscalMFD(string Totalizador, string ValorTotalizador);
        /// <summary>
        /// Retorna o estado da impressora.
        /// </summary>
        /// <param name="ACK">Variável inteira para receber o primeiro byte.</param>
        /// <param name="ST1">Variável inteira para receber o segundo byte.</param>
        /// <param name="ST2">Variável inteira para receber o terceiro byte.</param>
        /// <param name="ST3">Variável inteira para receber o quarto byte.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_VerificaEstadoImpressoraMFD(ref int ACK, ref int ST1, ref int ST2, ref int ST3);
        /// <summary>
        /// Retorna as formas de pagamento e seus valores acumulados.
        /// </summary>
        /// <param name="FormasPagamento">Variável string com 3016 posições para receber as formas programadas.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_VerificaFormasPagamentoMFD(string FormasPagamento);
        /// <summary>
        /// Retorna os recebimentos não fiscais não vinculados programados na impressora.
        /// </summary>
        /// <param name="Recebimentos">Variável STRING com 1077 posições para receber as informações.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_VerificaRecebimentoNaoFiscalMFD(string Recebimentos);
        /// <summary>
        /// Retorna os relatórios gerenciais programados e seus valores acumulados.
        /// </summary>
        /// <param name="Relatorios">Variável STRING com 659 posições para receber as informações.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_VerificaRelatorioGerencialMFD(string Relatorios);
        /// <summary>
        /// Retorna a descrição dos totalizadores não fiscais programados na impressora.
        /// </summary>
        /// <param name="Totalizadores">Variável STRING com 599 posições para receber a descrição dos totalizadores não fiscais programados.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_VerificaTotalizadoresNaoFiscaisMFD(string Totalizadores);
        /// <summary>
        /// Retorna os totalizadores parciais da impressora.
        /// </summary>
        /// <param name="Totalizadores">Variável STRING com o tamanho de 889 posições para receber os totalizadores parciais.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_VerificaTotalizadoresParciaisMFD(string Totalizadores);
        /// <summary>
        /// Retorna a versão do firmware da impressora MFD.
        /// </summary>
        /// <param name="VersaoFirmware">Variável STRING com o tamanho de 6 posições para receber a informação.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_VersaoFirmwareMFD([MarshalAs(UnmanagedType.VBByRefStr)] ref string VersaoFirmware);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_VerificaZPendente(string StatusRZ);

        /// <summary>
        /// Programando Contadores.
        /// </summary>
        /// <param name="Contadores">.</param>
        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        //public static extern int Bematech_FI_ProgramaContadorNFCe(string cIndice, string cContador);
        //http://partners.bematech.com.br/bemacast/Lists/Postagens/Post.aspx?ID=6154  flash tip 168
        [DllImport("BemaFi32.dll")]
        public static extern int VendeItemCompleto(String Codigo, String EAN13, String Descricao, String IndiceDepartamento, String Aliquota, String UnidadeMedida, String TipoQuantidade, String CasasDecimaisQtde, String Quantidade, String CasasDecimaisValor, String ValorUnitario, String TipoDesconto, String ValorAcrescimo, String ValorDesconto, String ArredondaTrunca, String NCM, String CFOP, String InformacaoAdicional, String CST_ICMS, String OrigemProduto, String ItemListaServico, String CodigoISS, String NaturezaOperacaoISS, String IndicadorIncentivoFiscal, String CodigoIBGE, String CSOSN, String ValorBaseCalculoSimples, String ValorICMSRetidoSimples, String ModalidadeBaseCalculo, String PercentualReducaoBase, String ModalidadeBC, String PercentualMargemICMS, String PercentualBCICMS, String ValorReducaoBCICMS, String ValorAliquotaICMS, String ValorICMS, String ValorICMSDesonerado, String MotivoDesoneracaoICMS, String AliquotaCalculoCredito, String ValorCreditoICMS, String Reservado01, String Reservado02, String Reservado03, String Reservado04, String Reservado05, String Reservado06, String Reservado07, String Reservado08, String Reservado09, String Reservado10, String Reservado11, String Reservado12, String Reservado13, String Reservado14, String Reservado15,String Reservado16, String Reservado17, String Reservado18, String Reservado19, String Reservado20, String Reservado21, String Reservado22, String Reservado23);

        [DllImport("BemaFi32.dll")]
        public static extern int TerminaFechamentoCupomNFCe(String message, String taxes);

        [DllImport("BemaFi32.dll")]
        public static extern int ChaveAcessoNFCe(String index, String counter, String accessKey);

        [DllImport("BemaFi32.dll")]
        public static extern int UltimaChaveAcessoNFCe(String accessKey);

        [DllImport("BemaFi32.dll")]
        public static extern int StatusUltimaNFCe(String status);

        [DllImport("BemaFi32.dll")]
        public static extern int StatusUltimoCancelamentoNFCe(String status);

        [DllImport("BemaFi32.dll")]
        public static extern int ProgramaContadorNFCe(String index, String counter);

        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_DownloadMF(String Arquivo);

        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_DownloadMFD(String Arquivo, string TipoDownload, string ParametroInicial, string ParametroFinal, string UsuarioECF);

        [DllImport("BemaFi32.dll")]
        public static extern int DadosEnvioNFCe(String layoutType, String sendType, String email);

        [DllImport("MP2032.dll")]
        public static extern int ImprimeCodigoQRCODE(int errorCorrectionLevel, int moduleSize, int codeType, int QRCodeVersion, int encodingModes, String codeQr);

        #endregion
        // Fim da Declaração ///////////////////////////////////////////////////////////
        #endregion	

    }
}
