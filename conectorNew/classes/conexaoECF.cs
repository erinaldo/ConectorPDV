using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using conectorBema;
using conectorECFDaruma;
using conectorElgin;
using conectorEpson;
using conectorSWEDA;
using conectorBema64;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;


namespace conectorPDV001
{
    class conexaoECF
    {
        public conexaoECF()
        {
            fiscal_flag = 0;
        }
        #region /*Variaveis*/
        private int fiscal_flag;
        private string fiscal_cache = new string('\x20', 15);
        private string fiscal_data_last_reducaoZ = new string('\x20', 6);
        private string fiscal_hora_last_reducaoZ = new string('\x20', 6);
        private string fiscal_SubTotal = new string('\x20', 14);
        private string fiscal_trunca_arredonda = new string('\x20', 2);//  Variável string com 1 posição para receber o flag de truncamento
        private string fiscal_registroEAD = new string('\x20', 256);
        private string fiscal_numero_serie = new string('\x20', 20);
        private string fiscal_GT = new string('\x20', 18);
        private string fiscal_GT_Crypt = new string('\x20', 20);
        private string fiscal_GT_compare = new string('\x20', 18);
        private string fiscal_store = new string('\x20', 4);
        private string fiscal_numero_caixa = new string('\x20', 4);
        private string fiscal_data_movimento = new string('\x20', 6);
        private string fiscal_data_printer = new string('\x20', 6);
        private string fiscal_hora_printer = new string('\x20', 6);
        private string fiscal_Cupom = new string('\x20', 14);
        private string fiscal_Cupom_coo = new string('\x20', 6);
        private string fiscal_Cupom_ccf = new string('\x20', 6);
        private string fiscal_Cupom_gnf = new string('\x20', 6);
        private string fiscal_Cupom_gng = new string('\x20', 6);
        private string fiscal_Cupom_crz = new string('\x20', 6);
        private string fiscal_Cupom_cdc = new string('\x20', 14);
        private string fiscal_Zpendente = new string('\x20', 1);
        private string fiscal_NumCuponsNaoFiscal = new string('\x20', 6);
        private string fiscal_TotalNaoFiscal = new string('\x20', 15);
        private string fiscal_totalSangrias = new string('\x20', 15);
        private string fiscal_md5_line = "";
        private string fiscal_MSG = "";
        private string fiscal_CGC = new string('\x20', 18);
        private string fiscal_IE = new string('\x20', 18);
        private string fiscal_dtusuario_last = new string('\x20', 20);
        private string fiscal_dtsoft_basico = new string('\x20', 20);
        private string fiscal_letramf_adicional = new string('\x20', 2);
        private string fiscal_marca = new string('\x20', 15);
        private string fiscal_modelo = new string('\x20', 20);
        private string fiscal_tipo_ecf = new string('\x20', 7);
        private string fiscal_VersaoFirmware = new string('\x20', 7);
        private string fiscal_NumeroSubstituicoesProprietario = new string('\x20', 4);
        private string fiscal_DadosUltimaReducaoMFD = new string('\x20', 1278);
        //############Dados Last Reducao
        private string fiscal_vendaBruta_last_reducaoZ = new string('\x20', 18);
        private string fiscal_last_reducao_crz = new string('\x20', 4);
        private string fiscal_last_reducao_coo = new string('\x20', 9);
        private string fiscal_last_reducao_cro = new string('\x20', 4);
        private string fiscal_last_reducao_dataMovimento = new string('\x20', 7);
        private string fiscal_last_reducao_dataEmissao = new string('\x20', 6);
        private string fiscal_last_reducao_horaEmissao = new string('\x20', 7);
        private string fiscal_last_reducao_vendaBruta = new string('\x20', 7);
        private string fiscal_last_reducao_ValorAcumulado = new string('\x20', 13);
        private string fiscal_last_reducao_St_ICMS = new string('\x20', 14);
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
        private string fiscal_last_reducao_cancelamento_not_fiscal = new string('\x20', 14);
        private string fiscal_last_reducao_parcial_not_icms = new string('\x20', 14);
        private string fiscal_last_reducao_sangria = new string('\x20', 14);
        private string fiscal_last_reducao_suprimento = new string('\x20', 14);
        private string fiscal_last_reducao_desconto_not_fiscal = new string('\x20', 14);
        private string fiscal_last_reducao_acrescimo_not_fiscal = new string('\x20', 14);
        private string fiscal_last_reducao_grandetotal = new string('\x20', 14);
        private string fiscal_last_reducao_T18 = new string('\x20', 14);
        private string fiscal_last_reducao_T12 = new string('\x20', 14);
        private string fiscal_last_reducao_T07 = new string('\x20', 14);
        private string ACK, ST1, ST2, ST3;
        //############End Dados Last Reducao
        //private string fiscal_Z = "";
        private string fiscal_ValorDescontos = new string('\x20', 14);
        private string fiscal_ValorCancelamentos = new string('\x20', 14);
        private string fiscal_NumReducoes = new string('\x20', 4);
        private string fiscal_reinicio = new string('\x20', 4);
        private string fiscal_NumCuponsCanc = new string('\x20', 4);
        private string keys_valida_private = new string('\x20', 256);
        private string keys_valida_publica = new string('\x20', 256);
        //############END Cabeçalho Impressora fiscal#####
        #endregion

        //fiscal_retorno = conectorECF.Bematech_FI_LeituraMemoriaFiscalDataMFD(firt.ToShortDateString(), last.ToShortDateString(), "c");
        public string conectorECF_Erros(int hardware)
        {
            string erros = "";
            switch (hardware)
            {
                case 1:
                    erros = conectorECF.Erros;
                    break;
                case 6:
                    erros = conectorECF64.Erros;
                    break;
                default:
                    erros = "";
                    break;
            }
            return erros;
        }

        public void conectorECF_AberturaDoDia(int hardware, string value, string finalizadora, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_AberturaDoDia(value, finalizadora);
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_AberturaDoDia(value, finalizadora);
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default:
                        fiscal_retorno = 0;
                        break;
                }
            }
        }

        public void conectorECF_GenerateEAD(int hardware, string cNomeArquivo, string cChavePublica, string cChavePrivada, [MarshalAs(UnmanagedType.VBByRefStr)] ref String cRegistroEAD, int iGrava, ref string log)
        {
            switch (hardware)
            {
                case 1:
                    conectorECF.generateEAD(cNomeArquivo, cChavePublica, cChavePrivada, ref cRegistroEAD, 0);
                    conectorECF.Analisa_iRetorno(iGrava, ref log);
                    break;
                case 6:
                    conectorECF64.generateEAD(cNomeArquivo, cChavePublica, cChavePrivada, ref cRegistroEAD, 0);
                    conectorECF64.Analisa_iRetorno(iGrava, ref log);
                    break;
                default:
                    iGrava = 0;
                    break;
            }
        }

        public void conectorECF_genkey(int hardware, [MarshalAs(UnmanagedType.VBByRefStr)] ref String cChavePublica, [MarshalAs(UnmanagedType.VBByRefStr)] ref String cChavePrivada, int iGrava, ref string log)
        {
            switch (hardware)
            {
                case 1:
                    iGrava = conectorECF.genkkey(ref keys_valida_publica, ref keys_valida_private);
                    conectorECF.Analisa_iRetorno(iGrava, ref log);
                    break;
                case 6:
                    iGrava = conectorECF64.genkkey(ref keys_valida_publica, ref keys_valida_private);
                    conectorECF64.Analisa_iRetorno(iGrava, ref log);
                    break;
                default:
                    iGrava = 0;
                    break;
            }
            cChavePublica = keys_valida_publica;
            cChavePrivada = keys_valida_private;
        }

        public void conectorECF_md5FromFile(int hardware, string cNomeArquivo, [MarshalAs(UnmanagedType.VBByRefStr)] ref String cMD5, int iGrava, ref string log)
        {
            string fiscal_md5 = new string('\x20', 33);
            switch (hardware)
            {
                case 1:
                    iGrava = conectorECF.md5FromFile(cNomeArquivo, ref fiscal_md5);
                    conectorECF.Analisa_iRetorno(iGrava, ref log);
                    break;
                case 6:
                    iGrava = conectorECF64.md5FromFile(cNomeArquivo, ref fiscal_md5);
                    conectorECF64.Analisa_iRetorno(iGrava, ref log);
                    break;
                default:
                    iGrava = 0;
                    break;
            }
            cMD5 = fiscal_md5;
        }

        public void conectorECF_LeituraMemoriaFiscalData(int hardware, string firt, string last, string tipo, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_LeituraMemoriaFiscalDataMFD(firt, last, tipo);//"c");
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        if (tipo == "0")
                        {
                            fiscal_retorno = swedaECF.ECF_LeituraMemoriaFiscalSerialData(firt, last);
                        }
                        else
                        {
                            fiscal_retorno = swedaECF.ECF_CapturaDocumentos("2", firt, last, "LMF CRZ Completa Captura.txt", "1");
                        }
                        swedaECF.Analisa_Retorno_Dll(fiscal_retorno);
                        swedaECF.Analisa_Retorno_ECF();
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_LeituraMemoriaFiscalDataMFD(firt, last, tipo);//"c");
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default:
                        fiscal_retorno = 0;
                        break;
                }
            }
        }
        public void conectorECF_Sangria(int hardware, string Valor, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_Sangria(Valor);
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        fiscal_retorno = darumaECF.iSangria_ECF_Daruma(Valor, "");
                        darumaECF.TrataRetorno(fiscal_retorno);
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        fiscal_retorno = swedaECF.ECF_Sangria(Valor);
                        swedaECF.Analisa_Retorno_Dll(fiscal_retorno);
                        swedaECF.Analisa_Retorno_ECF();
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_Sangria(Valor);
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default:
                        fiscal_retorno = 0;
                        break;
                }
            }
        }

        public void conectorECF_Imprimi_CODEQR(int hardware, int errorCorrectionLevel, int moduleSize, int codeType, int QRCodeVersion, int encodingModes, String codeQr, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.ImprimeCodigoQRCODE(errorCorrectionLevel, moduleSize, codeType, QRCodeVersion, encodingModes, codeQr);
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.ImprimeCodigoQRCODE(errorCorrectionLevel, moduleSize, codeType, QRCodeVersion, encodingModes, codeQr);
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default:
                        fiscal_retorno = 0;
                        break;
                }
            }
        }
        public void conectorECF_NumeroLoja(int hardware, ref string retorno_store, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_NumeroLoja(ref fiscal_store);
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        StringBuilder numero_loja = new StringBuilder(4);
                        fiscal_retorno = darumaECF.rRetornarInformacao_ECF_Daruma("129", numero_loja);
                        fiscal_store = numero_loja.ToString();
                        darumaECF.TrataRetorno(fiscal_retorno);
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_NumeroLoja(ref fiscal_store);
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default:
                        fiscal_retorno = 0;
                        break;
                }
            }
            retorno_store = fiscal_store;
        }
        //fiscal_retorno= conectorECF.Bematech_FI_VerificaImpressoraLigada();
        public void conectorECF_VerificaImpressoraLigada(int hardware, ref string log, ref int fiscal_retorno)
        {
            switch (hardware)
            {
                case 1:
                    fiscal_retorno = conectorECF.Bematech_FI_VerificaImpressoraLigada();
                    if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    fiscal_retorno = conectorECF64.Bematech_FI_VerificaImpressoraLigada();
                    if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                    break;
                default:
                    fiscal_retorno = 0;
                    break;
            }
        }
        public void conectorECF_HabilitaDesabilitaRetornoEstendidoMFD(int hardware, string tipo, ref string log, ref int fiscal_retorno)
        {
            switch (hardware)
            {
                case 1:
                    fiscal_retorno = conectorECF.Bematech_FI_HabilitaDesabilitaRetornoEstendidoMFD(tipo);
                    if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    fiscal_retorno = conectorECF64.Bematech_FI_HabilitaDesabilitaRetornoEstendidoMFD(tipo);
                    if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                    break;
                default:
                    fiscal_retorno = 0;
                    break;
            }
        }
        public void conectorECF_ArredondaTrunca(int hardware, ref string retorno_AT, short tipo, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_VerificaTruncamento(ref fiscal_trunca_arredonda);
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:

                        break;
                    case 5:
                        if (tipo == 0)
                        {
                            fiscal_retorno = swedaECF.ECF_ProgramaArredondamento();
                        }
                        else
                        {
                            fiscal_retorno = swedaECF.ECF_ProgramaTruncamento();
                        }

                        swedaECF.Analisa_Retorno_Dll(fiscal_retorno);
                        swedaECF.Analisa_Retorno_ECF();
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_VerificaTruncamento(ref fiscal_trunca_arredonda);
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default:
                        break;
                }
            }
            retorno_AT = fiscal_trunca_arredonda;
        }
        public void conectorECF_GrandeTotalDescriptografado(int hardware, string retorno_Gt, ref string retorno_Gt_compare, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_GrandeTotalDescriptografado(retorno_Gt, ref fiscal_GT_compare);
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_GrandeTotalDescriptografado(retorno_Gt, ref fiscal_GT_compare);
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default: fiscal_retorno = 0;
                        break;
                }
            }
            retorno_Gt_compare = fiscal_GT_compare;
        }
        //
        public void conectorECF_ReducaoZ(int hardware, string di, string df, string hr, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_ReducaoZ(di, df);//Paramentro 1 data/ Paramentro 2 data
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        fiscal_retorno = darumaECF.iReducaoZ_ECF_Daruma(di, df);
                        darumaECF.TrataRetorno(fiscal_retorno);
                        break;
                    case 3:
                        fiscal_retorno = epsonECF.EPSON_RelatorioFiscal_RZ(di, hr, 0, ref fiscal_Cupom_crz);
                        break;
                    case 4:
                        fiscal_retorno = elginECF.Elgin_ReducaoZ(di, hr);
                        break;
                    case 5:
                        fiscal_retorno = swedaECF.ECF_ReducaoZ(di, hr);
                        swedaECF.Analisa_Retorno_Dll(fiscal_retorno);
                        swedaECF.Analisa_Retorno_ECF();
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_ReducaoZ(di, df);//Paramentro 1 data/ Paramentro 2 data
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default:
                        fiscal_retorno = 0;
                        break;
                }
            }
        }
        public void conectorECF_MarcaModeloTipoImpressoraMFD(int hardware, ref string retorno_marca, ref string retorno_modelo, ref string retorno_tipo_ecf, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_MarcaModeloTipoImpressoraMFD(ref fiscal_marca, ref fiscal_modelo, ref fiscal_tipo_ecf);
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        StringBuilder marca = new StringBuilder(20);
                        StringBuilder tipo_ecf = new StringBuilder(7);
                        StringBuilder modelo = new StringBuilder(20);
                        fiscal_retorno = darumaECF.rRetornarInformacao_ECF_Daruma("79", tipo_ecf);
                        fiscal_retorno = darumaECF.rRetornarInformacao_ECF_Daruma("80", marca);
                        fiscal_retorno = darumaECF.rRetornarInformacao_ECF_Daruma("81", modelo);
                        retorno_tipo_ecf = tipo_ecf.ToString();
                        retorno_marca = marca.ToString();
                        retorno_modelo = modelo.ToString();
                        darumaECF.TrataRetorno(fiscal_retorno);
                        break;
                    case 4:
                        fiscal_retorno = elginECF.Elgin_MarcaModeloTipoImpressoraMFD(ref fiscal_marca, ref fiscal_modelo, ref fiscal_tipo_ecf);
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_MarcaModeloTipoImpressoraMFD(ref fiscal_marca, ref fiscal_modelo, ref fiscal_tipo_ecf);
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default:
                        fiscal_retorno = 0;
                        break;
                }
            }
            retorno_marca = fiscal_marca;
            retorno_modelo = fiscal_modelo;
            retorno_tipo_ecf = fiscal_tipo_ecf;
        }
        public void conectorECF_LeituraX(int hardware, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_LeituraX();
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        fiscal_retorno = darumaECF.iLeituraX_ECF_Daruma();
                        darumaECF.TrataRetorno(fiscal_retorno);
                        break;
                    case 3:
                        fiscal_retorno = epsonECF.EPSON_RelatorioFiscal_LeituraX();
                        break;
                    case 4:
                        fiscal_retorno = elginECF.Elgin_LeituraX();
                        break;
                    case 5:
                        fiscal_retorno = swedaECF.ECF_LeituraX();
                        swedaECF.Analisa_Retorno_Dll(fiscal_retorno);
                        swedaECF.Analisa_Retorno_ECF();
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_LeituraX();
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default:
                        fiscal_retorno = 0;
                        break;
                }
            }
        }
        public void conectorECF_SubTotal(int hardware, ref string retorno_SubTotal, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_SubTotal(ref fiscal_SubTotal);
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_SubTotal(ref fiscal_SubTotal);
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default:
                        fiscal_retorno = 0;
                        break;
                }
            }
            retorno_SubTotal = fiscal_SubTotal;
        }

        public void conectorECF_NomeiaRelatorioGerencial(int hardware, string Indice, string Descricao, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_NomeiaRelatorioGerencialMFD(Indice, Descricao);
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        fiscal_retorno = swedaECF.ECF_NomeiaRelatorioGerencialMFD(Indice, Descricao);
                        swedaECF.Analisa_Retorno_Dll(fiscal_retorno);
                        swedaECF.Analisa_Retorno_ECF();
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_NomeiaRelatorioGerencialMFD(Indice, Descricao);
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default:
                        fiscal_retorno = 0;
                        break;
                }
            }
        }

        public void conectorECF_Suprimento(int hardware, string Valor, string FormaPagamento, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_Suprimento(Valor, FormaPagamento);
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        fiscal_retorno = darumaECF.iSuprimento_ECF_Daruma(Valor, FormaPagamento);
                        darumaECF.TrataRetorno(fiscal_retorno);
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        fiscal_retorno = swedaECF.ECF_Suprimento("100,00", "DINHEIRO");
                        swedaECF.Analisa_Retorno_Dll(fiscal_retorno);
                        swedaECF.Analisa_Retorno_ECF();
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_Suprimento(Valor, FormaPagamento);
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default:
                        fiscal_retorno = 0;
                        break;
                }
            }
        }

        public void conectorECF_Descontos(int hardware, ref string retorno_Descontos, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_Descontos(ref fiscal_ValorDescontos);
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_Descontos(ref fiscal_ValorDescontos);
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default: fiscal_retorno = 0;
                        break;
                }
            }
            retorno_Descontos = fiscal_ValorDescontos;
        }
        public void conectorECF_NumeroReducoes(int hardware, ref string retorno_reducoes, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_NumeroReducoes(ref fiscal_NumReducoes);
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_NumeroReducoes(ref fiscal_NumReducoes);
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default: fiscal_retorno = 0;
                        break;
                }
            }
            retorno_reducoes = fiscal_NumReducoes;
        }

        public void conectorECF_NumeroReinicio(int hardware, ref string retorno_reinicio, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_NumeroIntervencoes(ref fiscal_reinicio);
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_NumeroIntervencoes(ref fiscal_reinicio);
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default: fiscal_retorno = 0;
                        break;
                }
            }
            retorno_reinicio = fiscal_reinicio;
        }
        public void conectorECF_ValorCancelamento(int hardware, ref string retorno_Canc, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_Cancelamentos(ref fiscal_ValorCancelamentos);
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_Cancelamentos(ref fiscal_ValorCancelamentos);
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default: fiscal_retorno = 0;
                        break;
                }
            }
            retorno_Canc = fiscal_ValorCancelamentos;
        }
        public void conectorECF_NumeroCuponsCancelados(int hardware, ref string retorno_NumeroCuponsCancelados, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_NumeroCuponsCancelados(ref fiscal_NumCuponsCanc);
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        StringBuilder NumCupomCanc = new StringBuilder("    ");
                        fiscal_retorno = swedaECF.ECF_NumeroOperacoesNaoFiscais(NumCupomCanc);
                        fiscal_NumCuponsCanc = NumCupomCanc.ToString();
                        swedaECF.Analisa_Retorno_Dll(fiscal_retorno);
                        swedaECF.Analisa_Retorno_ECF();
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_NumeroCuponsCancelados(ref fiscal_NumCuponsCanc);
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default: fiscal_retorno = 0;
                        break;
                }
            }
            retorno_NumeroCuponsCancelados = fiscal_NumCuponsCanc;
        }
        public void conectorECF_fiscal_flag(int hardware, ref int retorno_fiscal_flag, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1: 
                        fiscal_retorno = conectorECF.Bematech_FI_FlagsFiscais(ref fiscal_flag);
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        fiscal_retorno = darumaECF.rStatusImpressoraInt_ECF_Daruma(ref fiscal_flag);
                        darumaECF.TrataRetorno(fiscal_retorno);
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        StringBuilder statusCupomFiscal = new StringBuilder("  ");
                        fiscal_retorno = swedaECF.ECF_StatusCupomFiscal(statusCupomFiscal);
                        fiscal_flag = Convert.ToInt32(statusCupomFiscal.ToString());
                        swedaECF.Analisa_Retorno_Dll(fiscal_retorno);
                        swedaECF.Analisa_Retorno_ECF();
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_FlagsFiscais(ref fiscal_flag);
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default: fiscal_retorno = 0;
                        break;
                }
            }
            retorno_fiscal_flag = fiscal_flag;
        }
        public void conectorECF_VerificaZPendente(int hardware, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_VerificaZPendente("");
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        StringBuilder Str_ZPendente = new StringBuilder();
                        fiscal_retorno = darumaECF.rVerificarReducaoZ_ECF_Daruma(Str_ZPendente);
                        log = Str_ZPendente.ToString();
                        darumaECF.TrataRetorno(fiscal_retorno);
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        StringBuilder Zpendente = new StringBuilder("   ");
                        fiscal_retorno = swedaECF.ECF_VerificaZPendente(Zpendente);
                        // = Zpendente.ToString();
                        swedaECF.Analisa_Retorno_Dll(fiscal_retorno);
                        swedaECF.Analisa_Retorno_ECF();
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_VerificaZPendente("");
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default: fiscal_retorno = 0;
                        break;
                }
            }
        }
        //fiscal_retorno = conectorECF.Bematech_FI_ContadorCupomFiscalMFD(ref fiscal_Cupom_ccf);
        public void conectorECF_ContadorCupomFiscal(int hardware, ref string retorno_NumeroCupomCcf, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_ContadorCupomFiscalMFD(ref fiscal_Cupom_ccf);
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        StringBuilder numero_ccf = new StringBuilder(6);
                        fiscal_retorno = darumaECF.rRetornarInformacao_ECF_Daruma("30", numero_ccf);
                        fiscal_Cupom_ccf = numero_ccf.ToString();
                        darumaECF.TrataRetorno(fiscal_retorno);
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        StringBuilder NumCupom = new StringBuilder("      ");
                        fiscal_retorno = swedaECF.ECF_NumeroOperacoesNaoFiscais(NumCupom);
                        fiscal_Cupom_ccf = NumCupom.ToString();
                        swedaECF.Analisa_Retorno_Dll(fiscal_retorno);
                        swedaECF.Analisa_Retorno_ECF();
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_ContadorCupomFiscalMFD(ref fiscal_Cupom_ccf);
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default: fiscal_retorno = 0;
                        break;
                }
            }
            retorno_NumeroCupomCcf = fiscal_Cupom_ccf;
        }

        public void conectorECF_ValorTotalizadorNaoFiscal(int hardware, string descricao,ref string retorno_cache, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_ValorTotalizadorNaoFiscal(descricao, ref fiscal_cache);
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_ValorTotalizadorNaoFiscal(descricao, ref fiscal_cache);
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default: fiscal_retorno = 0;
                        break;
                }
            }
            retorno_cache = fiscal_cache;
        }
        public void conectorECF_AcionaGuilhotinaMFD(int hardware, int tipo, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_AcionaGuilhotinaMFD(tipo);
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_AcionaGuilhotinaMFD(tipo);
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default: fiscal_retorno = 0;
                        break;
                }
            }
        }
        //Bematech_FI_NumeroIntervencoes
        public void conectorECF_NumeroIntervencoes(int hardware, ref string retorno_NumeroIntervencoes, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_NumeroIntervencoes(ref fiscal_Cupom);
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_NumeroIntervencoes(ref fiscal_Cupom);
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default: fiscal_retorno = 0;
                        break;
                }
            }
            retorno_NumeroIntervencoes = fiscal_Cupom;
        }
        public void conectorECF_NumeroCupom(int hardware, ref string retorno_NumeroCupom, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_NumeroCupom(ref fiscal_Cupom);
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        StringBuilder numero_coo = new StringBuilder(6);
                        fiscal_retorno = darumaECF.rRetornarInformacao_ECF_Daruma("30", numero_coo);
                        fiscal_Cupom = fiscal_Cupom_coo = numero_coo.ToString();
                        darumaECF.TrataRetorno(fiscal_retorno);
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        StringBuilder RetornaCOO = new StringBuilder("      ");
                        fiscal_retorno = swedaECF.ECF_RetornaCOO(RetornaCOO);
                        fiscal_Cupom = fiscal_Cupom_coo = RetornaCOO.ToString();
                        swedaECF.Analisa_Retorno_Dll(fiscal_retorno);
                        swedaECF.Analisa_Retorno_ECF();
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_NumeroCupom(ref fiscal_Cupom);
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default: fiscal_retorno = 0;
                        break;
                }
            }
            retorno_NumeroCupom = fiscal_Cupom;
        }
        public void conectorECF_VendaBruta(int hardware, ref string retorno_VendaBruta, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_VendaBruta(ref fiscal_vendaBruta_last_reducaoZ);
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_VendaBruta(ref fiscal_vendaBruta_last_reducaoZ);
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default: fiscal_retorno = 0;
                        break;
                }
            }
            retorno_VendaBruta = fiscal_vendaBruta_last_reducaoZ;
        }
        public void conectorECF_NumReducoes(int hardware, ref string retorno_NumReducoes, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_Cancelamentos(ref fiscal_NumReducoes);
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_Cancelamentos(ref fiscal_NumReducoes);
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default: fiscal_retorno = 0;
                        break;
                }
            }
            retorno_NumReducoes = fiscal_NumReducoes;
        }
        //Bematech_FI_CancelaItemAnterior
        public void conectorECF_CancelaItemAnterior(int hardware, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_CancelaItemAnterior();
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        fiscal_retorno = darumaECF.iCFCancelarUltimoItem_ECF_Daruma();
                        darumaECF.TrataRetorno(fiscal_retorno);
                        break;
                    case 3:
                        fiscal_retorno = epsonECF.EPSON_Fiscal_Cancelar_Ultimo_Item();
                        epsonECF.atualizaRetorno(fiscal_retorno, "EPSON_Fiscal_Cancelar_Ultimo_Item");
                        break;
                    case 4:
                        fiscal_retorno = elginECF.Elgin_CancelaItemAnterior();
                        break;
                    case 5:
                        fiscal_retorno = swedaECF.ECF_CancelaItemAnterior();
                        swedaECF.Analisa_Retorno_Dll(fiscal_retorno);
                        swedaECF.Analisa_Retorno_ECF();
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_CancelaItemAnterior();
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default: fiscal_retorno = 0;
                        break;
                }
            }
        }

        public void conectorECF_AbreRelatorioGerencial(int hardware, string input_find, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_AbreRelatorioGerencialMFD(input_find);
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        fiscal_retorno = darumaECF.iRGAbrir_ECF_Daruma(input_find);
                        darumaECF.TrataRetorno(fiscal_retorno);
                        break;
                    case 3:
                        fiscal_retorno = epsonECF.EPSON_NaoFiscal_Abrir_Relatorio_Gerencial(input_find);
                        epsonECF.atualizaRetorno(fiscal_retorno, "EPSON_NaoFiscal_Abrir_Relatorio_Gerencial");
                        break;
                    case 4:
                        fiscal_retorno = elginECF.Elgin_AbreRelatorioGerencial();
                        break;
                    case 5:
                        fiscal_retorno = swedaECF.ECF_AbreRelatorioGerencial();
                        swedaECF.Analisa_Retorno_Dll(fiscal_retorno);
                        swedaECF.Analisa_Retorno_ECF();
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_AbreRelatorioGerencialMFD(input_find);
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default: fiscal_retorno = 0;
                        break;
                }
            }
        }
        public void conectorECF_AcionaGaveta(int hardware, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_AcionaGaveta();
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        fiscal_retorno = darumaECF.eAbrirGaveta_ECF_Daruma();
                        darumaECF.TrataRetorno(fiscal_retorno);
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_AcionaGaveta();
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default: fiscal_retorno = 0;
                        break;
                }
            }
        }

        public void VerificaEstadoGaveta(int hardware, ref string log, ref int iEstado, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_VerificaEstadoGaveta(out iEstado);
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        fiscal_retorno = darumaECF.eAbrirGaveta_ECF_Daruma();
                        darumaECF.TrataRetorno(fiscal_retorno);
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_VerificaEstadoGaveta(out iEstado);
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default: fiscal_retorno = 0;
                        break;
                }
            }
        }

        public void conectorECF_EspelhoMFD(int hardware, string cNomeArquivoDestino, string cDadoInicial, string cDadoFinal, string cTipoDownload, string cUsuario, string cChavePublica, string cChavePrivada, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_EspelhoMFD(cNomeArquivoDestino, cDadoInicial, cDadoFinal, cTipoDownload, cUsuario, cChavePublica, cChavePrivada);
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_EspelhoMFD(cNomeArquivoDestino, cDadoInicial, cDadoFinal, cTipoDownload, cUsuario, cChavePublica, cChavePrivada);
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default: fiscal_retorno = 0;
                        break;
                }
            }
        }
        public void conectorECF_DataHoraReducao(int hardware, ref string retorno_hora_last_reducaoZ, ref string retorno_data_last_reducaoZ, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_data_last_reducaoZ = new string('\x20', 6);
                        fiscal_hora_last_reducaoZ = new string('\x20', 6);
                        try
                        {
                            fiscal_retorno = conectorECF.Bematech_FI_DataHoraReducao(ref fiscal_data_last_reducaoZ, ref fiscal_hora_last_reducaoZ);
                        }
                        catch (Exception)
                        {
                        }
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        fiscal_data_last_reducaoZ = new string('\x20', 6);
                        fiscal_hora_last_reducaoZ = new string('\x20', 6);
                        try
                        {
                            fiscal_retorno = conectorECF64.Bematech_FI_DataHoraReducao(ref fiscal_data_last_reducaoZ, ref fiscal_hora_last_reducaoZ);
                        }
                        catch (Exception)
                        {
                        }
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default: fiscal_retorno = 0;
                        break;
                }
            }
            retorno_hora_last_reducaoZ = fiscal_hora_last_reducaoZ;
            retorno_data_last_reducaoZ = fiscal_data_last_reducaoZ;
        }
        public void conectorECF_Bematech_FI_VersaoFirmwareMFD(int hardware, ref string retorno_versao, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_VersaoFirmwareMFD(ref fiscal_VersaoFirmware);
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        StringBuilder versao = new StringBuilder(6);
                        fiscal_retorno = darumaECF.rRetornarInformacao_ECF_Daruma("83", versao);
                        retorno_versao = versao.ToString();
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_VersaoFirmwareMFD(ref fiscal_VersaoFirmware);
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default: fiscal_retorno = 0;
                        break;
                }
            }
            retorno_versao = fiscal_VersaoFirmware;
            
        }
        public void conectorECF_DataHoraGravacaoUsuarioSWBasicoMFAdicional(int hardware, ref string retorno_dtusuario_last, ref string retorno_dtsoft_basico, ref string retorno_letramf_adicional, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        try
                        {
                            fiscal_retorno = conectorECF.Bematech_FI_DataHoraGravacaoUsuarioSWBasicoMFAdicional(ref fiscal_dtusuario_last, ref fiscal_dtsoft_basico, ref fiscal_letramf_adicional);
                        }
                        catch (Exception ERRO)
                        {
                            fiscal_retorno = 1;
                            throw new Exception("Erro Software Basico ECF :" + ERRO.ToString());
                        }

                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        StringBuilder dt_versao = new StringBuilder(14);
                        StringBuilder dt_usuario = new StringBuilder(14);
                        fiscal_retorno = darumaECF.rRetornarInformacao_ECF_Daruma("93", dt_usuario);
                        fiscal_retorno = darumaECF.rRetornarInformacao_ECF_Daruma("85", dt_versao);
                        fiscal_dtsoft_basico = dt_versao.ToString();
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_DataHoraGravacaoUsuarioSWBasicoMFAdicional(ref fiscal_dtusuario_last, ref fiscal_dtsoft_basico, ref fiscal_letramf_adicional);
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default: fiscal_retorno = 0;
                        break;
                }
            }
            retorno_letramf_adicional = fiscal_letramf_adicional;
            retorno_dtsoft_basico = fiscal_dtsoft_basico;
            retorno_dtusuario_last = fiscal_dtusuario_last;
        }
        public void conectorECF_NumeroSubstituicoesProprietario(int hardware, ref string retorno_NumeroSubstituicoesProprietario, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_NumeroSubstituicoesProprietario(ref fiscal_NumeroSubstituicoesProprietario);
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_NumeroSubstituicoesProprietario(ref fiscal_NumeroSubstituicoesProprietario);
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default: fiscal_retorno = 0;
                        break;
                }
            }
            retorno_NumeroSubstituicoesProprietario = fiscal_NumeroSubstituicoesProprietario;
        }
        public void conectorECF_VersaoFirmwareMFD(int hardware, ref string retorno_VersaoFirmwareMFD, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_VersaoFirmware(ref fiscal_VersaoFirmware);
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_VersaoFirmware(ref fiscal_VersaoFirmware);
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default: fiscal_retorno = 0;
                        break;
                }
            }
            retorno_VersaoFirmwareMFD = fiscal_VersaoFirmware;
        }
        public void conectorECF_DadosUltimaReducaoMFD(int hardware, ref string retorno_DadosUltimaReducaoMFD, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_DadosUltimaReducaoMFD = new string('\x20', 1278);
                        try
                        {
                            fiscal_retorno = conectorECF.Bematech_FI_DadosUltimaReducaoMFD(ref fiscal_DadosUltimaReducaoMFD);
                        }
                        catch (Exception)
                        {

                        }
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        StringBuilder Str_DadosReducaoZ = new StringBuilder();
                        Str_DadosReducaoZ.Length = 2000;
                        fiscal_retorno = darumaECF.rRetornarDadosReducaoZ_ECF_Daruma(Str_DadosReducaoZ);
                        fiscal_DadosUltimaReducaoMFD = Str_DadosReducaoZ.ToString();
                        darumaECF.TrataRetorno(fiscal_retorno);
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        StringBuilder DataMovimUltimRZ = new StringBuilder("      ");
                        fiscal_retorno = swedaECF.ECF_DataMovimentoUltimaReducaoMFD(DataMovimUltimRZ);
                        fiscal_DadosUltimaReducaoMFD = DataMovimUltimRZ.ToString();
                        swedaECF.Analisa_Retorno_Dll(fiscal_retorno);
                        swedaECF.Analisa_Retorno_ECF();
                        break;
                    case 6:
                        fiscal_DadosUltimaReducaoMFD = new string('\x20', 1278);
                        fiscal_retorno = conectorECF64.Bematech_FI_DadosUltimaReducaoMFD(ref fiscal_DadosUltimaReducaoMFD);
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default: fiscal_retorno = 0;
                        break;
                }
            }
            retorno_DadosUltimaReducaoMFD = fiscal_DadosUltimaReducaoMFD;
        }
        public void conectorECF_GrandeTotal(int hardware, ref string retorno_Gt, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_GrandeTotal(ref fiscal_GT);
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        StringBuilder GrandeTotal = new StringBuilder("                  ");
                        fiscal_retorno = swedaECF.ECF_DataMovimentoUltimaReducaoMFD(GrandeTotal);
                        fiscal_GT = GrandeTotal.ToString();
                        swedaECF.Analisa_Retorno_Dll(fiscal_retorno);
                        swedaECF.Analisa_Retorno_ECF();
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_GrandeTotal(ref fiscal_GT);
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default: fiscal_retorno = 0;
                        break;
                }
            }
            retorno_Gt = fiscal_GT;
        }

        public void conectorECF_numero_serie(int hardware, ref string retorno, ref string log, ref int fiscal_retorno, int ligado)
        {
            StringBuilder NumeroSerie = new StringBuilder("               ");
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_NumeroSerieMFD(ref fiscal_numero_serie);
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        fiscal_retorno = darumaECF.rRetornarInformacao_ECF_Daruma("77", NumeroSerie);
                        fiscal_numero_serie = NumeroSerie.ToString();
                        darumaECF.TrataRetorno(fiscal_retorno);
                        break;
                    /*case 3:
                        fiscal_retorno = epsonECF.EPSON_NaoFiscal_Abrir_Relatorio_Gerencial(input_find);
                        epsonECF.atualizaRetorno(fiscal_retorno, "EPSON_NaoFiscal_Abrir_Relatorio_Gerencial");
                        break;
                    case 4:
                        fiscal_retorno = elginECF.Elgin_AbreRelatorioGerencial();
                        break;*/
                    case 5:
                        fiscal_retorno = swedaECF.ECF_NumeroSerie(NumeroSerie);
                        fiscal_numero_serie = NumeroSerie.ToString();
                        swedaECF.Analisa_Retorno_Dll(fiscal_retorno);
                        swedaECF.Analisa_Retorno_ECF();
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_NumeroSerieMFD(ref fiscal_numero_serie);
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default: fiscal_retorno = 0;
                        break;
                }
            }
            retorno = fiscal_numero_serie;
        }
        public void conectorECF_GrandeTotal_Crypt(int hardware, ref string retorno_GrandeTotal, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                fiscal_GT_Crypt = new string('\x20', 20);
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_GrandeTotalCriptografado(ref fiscal_GT_Crypt);
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_GrandeTotalCriptografado(ref fiscal_GT_Crypt);
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default: fiscal_retorno = 0;
                        break;
                }
            }
            retorno_GrandeTotal = fiscal_GT_Crypt;
        }
        public void conectorECF_FechaComprovanteNaoFiscalVinculado(int hardware, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_FechaComprovanteNaoFiscalVinculado();
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        fiscal_retorno = swedaECF.ECF_AbreRelatorioGerencial();
                        swedaECF.Analisa_Retorno_Dll(fiscal_retorno);
                        swedaECF.Analisa_Retorno_ECF();
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_FechaComprovanteNaoFiscalVinculado();
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default: fiscal_retorno = 0;
                        break;
                }
            }
        }
        public void conectorECF_UsaComprovanteNaoFiscalVinculado(int hardware, string msg, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_UsaComprovanteNaoFiscalVinculado(msg);
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        fiscal_retorno = swedaECF.ECF_UsaComprovanteNaoFiscalVinculado(msg);
                        swedaECF.Analisa_Retorno_Dll(fiscal_retorno);
                        swedaECF.Analisa_Retorno_ECF();
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_UsaComprovanteNaoFiscalVinculado(msg);
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default: fiscal_retorno = 0;
                        break;
                }
            }
        }

        public void conectorECF_UsaComprovanteNaoFiscalVinculadoTEF(int hardware, string msg, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_UsaComprovanteNaoFiscalVinculadoTEF(msg);
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        fiscal_retorno = swedaECF.ECF_UsaComprovanteNaoFiscalVinculadoTEF(msg);
                        swedaECF.Analisa_Retorno_Dll(fiscal_retorno);
                        swedaECF.Analisa_Retorno_ECF();
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_UsaComprovanteNaoFiscalVinculadoTEF(msg);
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default: fiscal_retorno = 0;
                        break;
                }
            }
        }

        public void conectorECF_FinalizaModoTEF(int hardware, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_FinalizaModoTEF();
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        fiscal_retorno = swedaECF.ECF_FinalizaModoTEF();
                        swedaECF.Analisa_Retorno_Dll(fiscal_retorno);
                        swedaECF.Analisa_Retorno_ECF();
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_FinalizaModoTEF();
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default: fiscal_retorno = 0;
                        break;
                }
            }
        }

        public void conectorECF_IniciaModoTEF(int hardware, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_IniciaModoTEF();
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        fiscal_retorno = swedaECF.ECF_IniciaModoTEF();
                        swedaECF.Analisa_Retorno_Dll(fiscal_retorno);
                        swedaECF.Analisa_Retorno_ECF();
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_IniciaModoTEF();
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default: fiscal_retorno = 0;
                        break;
                }
            }
        }
        public void conectorECF_AbreComprovanteNaoFiscalVinculado(int hardware, string FormaPagamento, string Valor, string NumeroCupom, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_AbreComprovanteNaoFiscalVinculado(FormaPagamento, Valor, NumeroCupom);
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        fiscal_retorno = swedaECF.ECF_AbreComprovanteNaoFiscalVinculado(FormaPagamento, Valor, NumeroCupom.ToString());
                        swedaECF.Analisa_Retorno_Dll(fiscal_retorno);
                        swedaECF.Analisa_Retorno_ECF();
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_AbreComprovanteNaoFiscalVinculado(FormaPagamento, Valor, NumeroCupom);
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default: fiscal_retorno = 0;
                        break;
                }
            }
        }
        public void conectorECF_CancelaCupom(int hardware, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_CancelaCupom();
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        fiscal_retorno = darumaECF.iCNFCancelar_ECF_Daruma();
                        darumaECF.TrataRetorno(fiscal_retorno);
                        break;
                    case 3:
                        fiscal_retorno = epsonECF.EPSON_Fiscal_Cancelar_Cupom();
                        break;
                    case 4:
                        fiscal_retorno = elginECF.Elgin_CancelaCupom();
                        break;
                    case 5:
                        fiscal_retorno = swedaECF.ECF_CancelaCupom();
                        swedaECF.Analisa_Retorno_Dll(fiscal_retorno);
                        swedaECF.Analisa_Retorno_ECF();
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_CancelaCupom();
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default:
                        fiscal_retorno = 0;
                        break;
                }
                //File.Delete(@"C:\conector\transmissao\semaforo.txt");
            }
        }

        public void conectorECF_CancelaItemGenerico(int hardware, string texto, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_CancelaItemGenerico(texto);
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        fiscal_retorno = darumaECF.iCFCancelarItem_ECF_Daruma(texto);
                        darumaECF.TrataRetorno(fiscal_retorno);
                        break;
                    case 3:
                        fiscal_retorno = epsonECF.EPSON_Fiscal_Cancelar_Item(texto);
                        epsonECF.atualizaRetorno(fiscal_retorno, "EPSON_Fiscal_Cancelar_Item");
                        break;
                    case 4:
                        fiscal_retorno = elginECF.Elgin_CancelaItemGenerico(texto);
                        break;
                    case 5:
                        fiscal_retorno = swedaECF.ECF_CancelaItemGenerico(texto);
                        swedaECF.Analisa_Retorno_Dll(fiscal_retorno);
                        swedaECF.Analisa_Retorno_ECF();
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_CancelaItemGenerico(texto);
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default:
                        fiscal_retorno = 0;
                        break;
                }
            }
        }
        //fiscal_retorno = conectorECF.Bematech_FI_ContadorComprovantesCreditoMFD(ref fiscal_Cupom_cdc);
        public void conectorECF_ContadorComprovantesCreditoMFD(int hardware, ref string retorno_cupom_cdc, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_ContadorComprovantesCreditoMFD(ref fiscal_Cupom_cdc);
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_ContadorComprovantesCreditoMFD(ref fiscal_Cupom_cdc);
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default: fiscal_retorno = 0;
                        break;
                }
            }
            retorno_cupom_cdc = fiscal_Cupom_cdc;
        }
        public void conectorECF_ContadorRelatoriosGerenciaisMFD(int hardware, ref string retorno_cupom_gng, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_ContadorRelatoriosGerenciaisMFD(ref fiscal_Cupom_gng);
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_ContadorRelatoriosGerenciaisMFD(ref fiscal_Cupom_gng);
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default: fiscal_retorno = 0;
                        break;
                }
            }
            retorno_cupom_gng = fiscal_Cupom_gng;
        }
        public void conectorECF_NumeroOperacoesNaoFiscais(int hardware, ref string retorno_NumCuponsNaoFiscal, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_NumeroOperacoesNaoFiscais(ref fiscal_NumCuponsNaoFiscal);
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        StringBuilder numero_gnf = new StringBuilder(6);
                        fiscal_retorno = darumaECF.rRetornarInformacao_ECF_Daruma("28", numero_gnf);
                        fiscal_NumCuponsNaoFiscal = numero_gnf.ToString();
                        darumaECF.TrataRetorno(fiscal_retorno);
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        StringBuilder NumCupomNaoFisc = new StringBuilder("      ");
                        fiscal_retorno = swedaECF.ECF_NumeroOperacoesNaoFiscais(NumCupomNaoFisc);
                        numero_gnf = NumCupomNaoFisc;
                        swedaECF.Analisa_Retorno_Dll(fiscal_retorno);
                        swedaECF.Analisa_Retorno_ECF();
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_NumeroOperacoesNaoFiscais(ref fiscal_NumCuponsNaoFiscal);
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default: fiscal_retorno = 0;
                        break;
                }
            }
            retorno_NumCuponsNaoFiscal = fiscal_NumCuponsNaoFiscal;
        }
        //RecebimentoNaoFiscal
        public void conectorECF_RecebimentoNaoFiscal(int hardware, string IndiceTotalizador, string Valor, string FormaPagamento, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_RecebimentoNaoFiscal(IndiceTotalizador, Valor, FormaPagamento);
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        fiscal_retorno = swedaECF.ECF_RecebimentoNaoFiscal(IndiceTotalizador, Valor, FormaPagamento);
                        swedaECF.Analisa_Retorno_Dll(fiscal_retorno);
                        swedaECF.Analisa_Retorno_ECF();
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_RecebimentoNaoFiscal(IndiceTotalizador, Valor, FormaPagamento);
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default: fiscal_retorno = 0;
                        break;
                }
            }
        }

        public void conectorECF_AbreRecebimentoNaoFiscalMFD(int hardware, string CGC, string Nome, string Endereco, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_AbreRecebimentoNaoFiscalMFD(CGC, Nome, Endereco);
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_AbreRecebimentoNaoFiscalMFD(CGC, Nome, Endereco);
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default: fiscal_retorno = 0;
                        break;
                }
            }
        }
        public void conectorECF_VerificaFormasPagamento(int hardware, ref string formas, ref string log, ref int fiscal_retorno, int ligado)
        {
            string fiscal_formas_pgto = new string('\x20', 3016);
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_VerificaFormasPagamento(ref fiscal_formas_pgto);
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_VerificaFormasPagamento(ref fiscal_formas_pgto);
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default: fiscal_retorno = 0;
                        break;
                }
            }
            formas = fiscal_formas_pgto;
        }

        public void conectorECF_UsaRelatorioMeiosPagamento(int hardware, string cIdentificacao, string cTipoDocumento, string cValorAcumulado, string cData, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_UsaRelatorioMeiosPagamento(cIdentificacao, cTipoDocumento, cValorAcumulado, cData);
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_UsaRelatorioMeiosPagamento(cIdentificacao, cTipoDocumento, cValorAcumulado, cData);
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default: fiscal_retorno = 0;
                        break;
                }
            }
        }
        public void conectorECF_FechaRelatorioMeiosPagamento(int hardware, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_FechaRelatorioMeiosPagamento();
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_FechaRelatorioMeiosPagamento();
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default: fiscal_retorno = 0;
                        break;
                }
            }
        }

        public void conectorECF_IdentificacaoPAFECF(int hardware, string cIndiceGerencial, string cNumeroLaudo, string cCNPJDesenvolvedor, string cRazaoSocial, string cEndereco, string cTelefone, string cContato, string cNomeComercial, string cVersao, string cPrincipalExecutavel, string cMD5PrincipalExecutavel, string cDemaisArquivos, string cMD5DemaisArquivos, string cNumerosFabricacao, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_IdentificacaoPAFECF(cIndiceGerencial, cNumeroLaudo, cCNPJDesenvolvedor, cRazaoSocial, cEndereco, cTelefone, cContato, cNomeComercial, cVersao, cPrincipalExecutavel, cMD5PrincipalExecutavel, cDemaisArquivos, cMD5DemaisArquivos, cNumerosFabricacao);
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_IdentificacaoPAFECF(cIndiceGerencial, cNumeroLaudo, cCNPJDesenvolvedor, cRazaoSocial, cEndereco, cTelefone, cContato, cNomeComercial, cVersao, cPrincipalExecutavel, cMD5PrincipalExecutavel, cDemaisArquivos, cMD5DemaisArquivos, cNumerosFabricacao);
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default: fiscal_retorno = 0;
                        break;
                }
            }
        }
        public void conectorECF_AbreRelatorioMeiosPagamento(int hardware, string cIndiceGerencial, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_AbreRelatorioMeiosPagamento(cIndiceGerencial);
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_AbreRelatorioMeiosPagamento(cIndiceGerencial);
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default: fiscal_retorno = 0;
                        break;
                }
            }
        }
        public void conectorECF_CancelaRecebimentoNaoFiscalMFD(int hardware, string CGC, string Nome, string Endereco, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_CancelaRecebimentoNaoFiscalMFD(CGC, Nome, Endereco);
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_CancelaRecebimentoNaoFiscalMFD(CGC, Nome, Endereco);
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default: fiscal_retorno = 0;
                        break;
                }
            }
        }

        public void conectorECF_ArquivoMFD(int hardware, string NomeArquivoOrigem, string cDataInicial, string cDataFinal, string cTipoDownload, string Usuario, int Parametrizacao, string cChavePublica, string cChavePrivada, int TipoGeracao, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_ArquivoMFD(NomeArquivoOrigem, cDataInicial, cDataFinal, cTipoDownload, Usuario, Parametrizacao, cChavePublica, cChavePrivada, TipoGeracao);
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        //1 data 2 coo
                        fiscal_retorno = swedaECF.ECF_DownloadMFD("DownloadMFD Completa", cTipoDownload, cDataInicial, cDataFinal, "CONECTORPDV");
                        swedaECF.Analisa_Retorno_Dll(fiscal_retorno);
                        swedaECF.Analisa_Retorno_ECF();
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_ArquivoMFD(NomeArquivoOrigem, cDataInicial, cDataFinal, cTipoDownload, Usuario, Parametrizacao, cChavePublica, cChavePrivada, TipoGeracao);
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default: fiscal_retorno = 0;
                        break;
                }
            }
        }

        public void conectorPDV_DownloadMFD(int hardware, string NomeArquivoOrigem, string cTipoDownload, string cDataInicial, string cDataFinal, string Usuario, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_DownloadMFD(NomeArquivoOrigem, cTipoDownload, cDataInicial, cDataFinal, Usuario);
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_DownloadMFD(NomeArquivoOrigem, cTipoDownload, cDataInicial, cDataFinal, Usuario);
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default: fiscal_retorno = 0;
                        break;
                }
            }
        }

        public void conectorPDV_DownloadMF(int hardware, string NomeArquivoOrigem, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_DownloadMF(NomeArquivoOrigem);
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_DownloadMF(NomeArquivoOrigem);
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default: fiscal_retorno = 0;
                        break;
                }
            }
        }

        public void conectorECF_AcrescimoDescontoSubtotal(int hardware, string cFlag, string cTipo, string cValor, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_AcrescimoDescontoSubtotalMFD(cFlag, cTipo, cValor);
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_AcrescimoDescontoSubtotalMFD(cFlag, cTipo, cValor);
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default: fiscal_retorno = 0;
                        break;
                }
            }
        }
        public void conectorECF_FechaRelatorioGerencial(int hardware, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_FechaRelatorioGerencial();
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        fiscal_retorno = swedaECF.ECF_FechaRelatorioGerencial();
                        swedaECF.Analisa_Retorno_Dll(fiscal_retorno);
                        swedaECF.Analisa_Retorno_ECF();
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_FechaRelatorioGerencial();
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default: fiscal_retorno = 0;
                        break;
                }
            }
        }

        public void conectorECF_VendeItem(int hardware, string Codigo, string Descricao, string Aliquota, string TipoQuantidade, string Quantidade, int CasasDecimais, string ValorUnitario, string TipoDesconto, string Desconto, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:

                        if (TipoQuantidade == "F")
                        {
                            ValorUnitario = String.Format("{0:F3}", Convert.ToDecimal(ValorUnitario.Replace(",","."))).Replace(".",",");
                            Quantidade = Quantidade.Replace(",","");
                            CasasDecimais = 3;
                        }
                        fiscal_retorno = conectorECF.Bematech_FI_VendeItem(Codigo, Descricao, Aliquota, TipoQuantidade, Quantidade, CasasDecimais, ValorUnitario, TipoDesconto, Desconto);
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        string Str_Aliquota, Str_Qtde, Str_ValorUnit;
                        string Str_Tipo_Desc_Acresc, Str_Valor_Desc_Acrec;
                        string Str_Codigo_Item, Str_UnidadeMedida, Str_Descricao;

                        Str_Aliquota = Aliquota.Trim();
                        Str_Qtde = Quantidade.Trim();
                        Str_ValorUnit = ValorUnitario.Trim();
                        Str_Tipo_Desc_Acresc = TipoDesconto.Trim();
                        Str_Valor_Desc_Acrec = Desconto.Trim();
                        Str_Codigo_Item = Codigo.Trim();
                        Str_UnidadeMedida = "UN";
                        Str_Descricao = Descricao.Trim();

                        fiscal_retorno = darumaECF.iCFVender_ECF_Daruma(Str_Aliquota, Str_Qtde, Str_ValorUnit, Str_Tipo_Desc_Acresc, Str_Valor_Desc_Acrec, Str_Codigo_Item, Str_UnidadeMedida, Str_Descricao);
                        darumaECF.TrataRetorno(fiscal_retorno);
                        break;
                    case 3:
                        fiscal_retorno = epsonECF.EPSON_Fiscal_Vender_Item(Codigo, Descricao, TipoQuantidade, CasasDecimais, "UN", ValorUnitario, CasasDecimais, Aliquota, 1);
                        epsonECF.atualizaRetorno(fiscal_retorno, "EPSON_Fiscal_Vender_Item");
                        break;
                    case 4:
                        break;
                    case 5:
                        fiscal_retorno = swedaECF.ECF_VendeItem(Codigo, Descricao, Aliquota, TipoQuantidade, Quantidade, CasasDecimais, ValorUnitario, TipoDesconto, Desconto);
                        swedaECF.Analisa_Retorno_Dll(fiscal_retorno);
                        swedaECF.Analisa_Retorno_ECF();
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_VendeItem(Codigo, Descricao, Aliquota, TipoQuantidade, Quantidade, CasasDecimais, ValorUnitario, TipoDesconto, Desconto);
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default: fiscal_retorno = 0;
                        break;
                }
            }
        }
        public void conectorECF_AbreCupom(int hardware, string texto, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_AbreCupom(texto);
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        fiscal_retorno = darumaECF.iCFAbrirPadrao_ECF_Daruma();
                        darumaECF.TrataRetorno(fiscal_retorno);
                        break;
                    case 3:
                        fiscal_retorno = epsonECF.EPSON_Fiscal_Abrir_CupomEX(texto, "", "");
                        epsonECF.atualizaRetorno(fiscal_retorno, "EPSON_Fiscal_Abrir_CupomEX");
                        break;
                    case 4:
                        break;
                    case 5:
                        fiscal_retorno = swedaECF.ECF_AbreCupom(texto); // Opcional
                        swedaECF.Analisa_Retorno_Dll(fiscal_retorno);
                        swedaECF.Analisa_Retorno_ECF();
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_AbreCupom(texto);
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default: fiscal_retorno = 0;
                        break;
                }
            }
        }

        public void conectorECF_AbreCupomMFD(int hardware, string texto, string texto1, string texto2, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                if (texto1.Length > 30)
                {
                    texto1 = texto1.Substring(0, 30);
                }
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_AbreCupomMFD(texto, texto1, texto2);
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        break;
                    case 3:
                        
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_AbreCupomMFD(texto, texto1, texto2);
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default: fiscal_retorno = 0;
                        break;
                }
                StreamWriter sw1 = new StreamWriter(@"C:\conector\transmissao\semaforo.txt", true, Encoding.ASCII);
            }
        }
        public void conectorECF_TerminaFechamentoCupom(int hardware, string msgFinalCupom, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_TerminaFechamentoCupom(msgFinalCupom);
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        fiscal_retorno = swedaECF.ECF_TerminaFechamentoCupom(msgFinalCupom);
                        swedaECF.Analisa_Retorno_Dll(fiscal_retorno);
                        swedaECF.Analisa_Retorno_ECF();
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_TerminaFechamentoCupom(msgFinalCupom);
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default: fiscal_retorno = 0;
                        break;
                }
                File.Delete(@"C:\conector\transmissao\semaforo.txt");
            }
        }

        public void conectorECF_AcrescimoDescontoItem(int hardware, string Item, string AcrescimoDesconto, string TipoAcrescimoDesconto, string ValorAcrescimoDesconto, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_AcrescimoDescontoItemMFD(Item, AcrescimoDesconto, TipoAcrescimoDesconto, ValorAcrescimoDesconto);
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_AcrescimoDescontoItemMFD(Item, AcrescimoDesconto, TipoAcrescimoDesconto, ValorAcrescimoDesconto);
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default: fiscal_retorno = 0;
                        break;
                }
            }
        }
        public void conectorECF_EfetuaFormaPagamento(int hardware, string FormaPagamento, string ValorFormaPagamento, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_EfetuaFormaPagamento(FormaPagamento, ValorFormaPagamento.Replace(".", ","));
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        fiscal_retorno = swedaECF.ECF_EfetuaFormaPagamento(FormaPagamento, ValorFormaPagamento);
                        swedaECF.Analisa_Retorno_Dll(fiscal_retorno);
                        swedaECF.Analisa_Retorno_ECF();
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_EfetuaFormaPagamento(FormaPagamento, ValorFormaPagamento.Replace(".", ","));
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default: fiscal_retorno = 0;
                        break;
                }
            }
        }
        public void conectorECF_IniciaFechamentoCupom(int hardware, string AcrescimoDesconto, string TipoAcrescimoDesconto, string ValorAcrescimoDesconto, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_IniciaFechamentoCupom(AcrescimoDesconto, TipoAcrescimoDesconto, ValorAcrescimoDesconto == "" ? "0" : ValorAcrescimoDesconto);
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_IniciaFechamentoCupom(AcrescimoDesconto, TipoAcrescimoDesconto, ValorAcrescimoDesconto == "" ? "0" : ValorAcrescimoDesconto);
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default: fiscal_retorno = 0;
                        break;
                }
            }
        }

        public void conectorECF_RelatorioGerencial(int hardware, string texto, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_RelatorioGerencial(texto);
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_RelatorioGerencial(texto);
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default: fiscal_retorno = 0;
                        break;
                }
            }
        }

        public bool conectorECF_Analisa_RetornoImpressora(int hardware, ref string ACK, ref string ST1, ref string ST2, ref string ST3, ref string log)
        {
            bool valida = false;
            switch (hardware)
            {
                case 1:
                    valida = conectorECF.Analisa_RetornoImpressora(ref ACK, ref ST1, ref ST2, ref ST3);
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    valida = conectorECF64.Analisa_RetornoImpressora(ref ACK, ref ST1, ref ST2, ref ST3);
                    break;
                default: valida = false;
                    break;
            }
            
            return valida;
        }
        public void conectorECF_CGC_IE(int hardware, ref string retorno_cgc, ref string retorno_ie, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_CGC_IE(ref fiscal_CGC, ref fiscal_IE);
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        StringBuilder numero_cgc = new StringBuilder(20);
                        StringBuilder numero_ie = new StringBuilder(20);
                        fiscal_retorno = darumaECF.rRetornarInformacao_ECF_Daruma("90", numero_cgc);
                        fiscal_retorno = darumaECF.rRetornarInformacao_ECF_Daruma("91", numero_ie);
                        fiscal_CGC = numero_cgc.ToString();
                        fiscal_IE = numero_ie.ToString();
                        darumaECF.TrataRetorno(fiscal_retorno);
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_CGC_IE(ref fiscal_CGC, ref fiscal_IE);
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default: fiscal_retorno = 0;
                        break;
                }
            }
            retorno_cgc = fiscal_CGC;
            retorno_ie = fiscal_IE;
        }

        public void conectorECF_NumeroCaixa(int hardware, ref string retorno_NumeroCaixa, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_NumeroCaixa(ref fiscal_numero_caixa);
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_NumeroCaixa(ref fiscal_numero_caixa);
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default: fiscal_retorno = 0;
                        break;
                }
            }
            retorno_NumeroCaixa = fiscal_numero_caixa;
        }
        public void conectorECF_DataMovimento(int hardware, ref string retorno_data_movimento, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_DataMovimento(ref fiscal_data_movimento);
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        StringBuilder DataMovimento = new StringBuilder("      ");
                        fiscal_retorno = swedaECF.ECF_DataMovimentoUltimaReducaoMFD(DataMovimento);
                        fiscal_data_movimento = DataMovimento.ToString();
                        swedaECF.Analisa_Retorno_Dll(fiscal_retorno);
                        swedaECF.Analisa_Retorno_ECF();
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_DataMovimento(ref fiscal_data_movimento);
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default: fiscal_retorno = 0;
                        break;
                }
            }
            retorno_data_movimento = fiscal_data_movimento;
        }
        public void conectorECF_DataHoraPrinter(int hardware, ref string retorno_data_printer, ref string retorno_hora_printer, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        try
                        {
                            fiscal_retorno = conectorECF.Bematech_FI_DataHoraImpressora(ref fiscal_data_printer, ref fiscal_hora_printer);
                        }
                        catch (Exception)
                        {
                        }
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        StringBuilder Str_Data = new StringBuilder(10);
                        StringBuilder Str_Hora = new StringBuilder(10);
                        fiscal_retorno = darumaECF.rDataHoraImpressora_ECF_Daruma(Str_Data, Str_Hora);
                        fiscal_data_printer = Str_Data.ToString();
                        fiscal_hora_printer = Str_Hora.ToString();
                        darumaECF.TrataRetorno(fiscal_retorno);
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        try
                        {
                            fiscal_retorno = conectorECF64.Bematech_FI_DataHoraImpressora(ref fiscal_data_printer, ref fiscal_hora_printer);
                        }
                        catch (Exception)
                        {
                        }
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default: fiscal_retorno = 0;
                        break;
                }
            }
            retorno_data_printer = fiscal_data_printer;
            retorno_hora_printer = fiscal_hora_printer;
        }
        //conectorECF.Bematech_FI_ProgramaFormaPagamentoMFD
        public void conectorECF_ProgramaFormaPagamentoMFD(int hardware, string descricao_finalizadora, string inseri_tef, ref string log, ref int fiscal_retorno, int ligado)
        {
            if (ligado == -6)
            {
                log = "ERRO FATAL - " + "SOFTWARE EM MODO CONSULTA";
            }
            else
            {
                switch (hardware)
                {
                    case 1:
                        fiscal_retorno = conectorECF.Bematech_FI_ProgramaFormaPagamentoMFD(descricao_finalizadora, inseri_tef);
                        if (fiscal_retorno != 1) { conectorECF.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        fiscal_retorno = swedaECF.ECF_ProgramaFormaPagamentoMFD(descricao_finalizadora, inseri_tef);
                        swedaECF.Analisa_Retorno_Dll(fiscal_retorno);
                        swedaECF.Analisa_Retorno_ECF();
                        break;
                    case 6:
                        fiscal_retorno = conectorECF64.Bematech_FI_ProgramaFormaPagamentoMFD(descricao_finalizadora, inseri_tef);
                        if (fiscal_retorno != 1) { conectorECF64.Analisa_iRetorno(fiscal_retorno, ref log); }
                        break;
                    default:
                        fiscal_retorno = 0;
                        break;
                }
            }
        }
    }
}
