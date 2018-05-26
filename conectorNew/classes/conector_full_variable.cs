using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace conectorPDV001
{
    class conector_full_variable
    {
        public conector_full_variable()
        {
            //_file = new StreamReader("C:\\conector\\MFD\\Grand\\grandFullPDV.txt", System.Text.Encoding.UTF7, true);   
        }
        //###############################################################Declaracao Variaveis e Metodos Globais#################################################
        private static StreamReader _file;
        public StreamReader ArqResposta
        {
            get { return _file; }
            set { _file = value; }
        }
        
        private static List<string> _ArqTotal = new List<string>();
        public List<string> ArqTotal
        {
            get { return _ArqTotal; }
            set { _ArqTotal = value; }
        }
        private static string _ECF_UTIL = "";
        public string ECF_UTIL
        {
            get { return _ECF_UTIL; }
            set { _ECF_UTIL = value; }
        }

        private static string _stringConector = "";
        public string StringConector
        {
            get { return _stringConector; }
            set { _stringConector = value; }
        }

        private static string _Atualizador = "";
        public string Atualizador
        {
            get { return _Atualizador; }
            set { _Atualizador = value; }
        }

        private static string _versaoSystem;
        public string VersaoSystem
        {
            get { return _versaoNfce; }
            set { _versaoNfce = value; }
        }

        private static string _localMonitor = "n";
        public string LocalMonitor
        {
            get { return _localMonitor; }
            set { _localMonitor = value; }
        }

        private static string _conectorServer;
        public string ConectorServer
        {
            get { return _conectorServer; }
            set { _conectorServer = value; }
        }

        private static string _versaoBanco;
        public string VersaoBanco
        {
            get { return _versaoBanco; }
            set { _versaoBanco = value; }
        }

        private static string _log;
        public string caminhoLog
        {
            get { return _log; }
            set { _log = value; }
        }

        private static string _compatilhamentoImport = "";
        public string EscritaCompatilhadaImport
        {
            get { return _compatilhamentoImport; }
            set { _compatilhamentoImport = value; }
        }
        private static string _palavraChave = "";
        public string PalavraChave
        {
            get { return _palavraChave; }
            set { _palavraChave = value; }
        }

        private static int _modeloEcf;
        public int ModeloEcf
        {
            get { 
                return _modeloEcf; 
            }
            set { 
                _modeloEcf = value; 
            }
        }

        private static int _ECF_Ligada = 1;
        public int ECF_Ligada
        {
            get { return _ECF_Ligada; }
            set { _ECF_Ligada = value; }
        }

        private static string _grandFull = "";
        public string GrandFull
        {
            get { return _grandFull; }
            set { _grandFull = value; }
        }

        private static string _tipoNf = "1";
        public string TipoNf
        {
            get { return _tipoNf; }
            set { _tipoNf = value; }
        }

        private static string _chaveNfce = "";
        public string chaveNfce
        {
            get { return _chaveNfce; }
            set { _chaveNfce = value; }
        }

        private static string _Entidade = "";
        public string Entidade
        {
            get { return _Entidade; }
            set { _Entidade = value; }
        }

        private static string _ModoNFce = "";
        public string ModoNFce
        {
            get { return _ModoNFce; }
            set { _ModoNFce = value; }
        }

        private static string _protocoloNfce = "";
        public string ProtocoloNfce
        {
            get { return _protocoloNfce; }
            set { _protocoloNfce = value; }
        }

        private static string _digVal = "";
        public string DigestValue
        {
            get { return _digVal; }
            set { _digVal = value; }
        }

        private static string _Link_Code_QR = "";
        public string Link_Code_QR
        {
            get { return _Link_Code_QR; }
            set { _Link_Code_QR = value; }
        }

        private static string _Link_Danfe = "";
        public string Link_Danfe
        {
            get { return _Link_Danfe; }
            set { _Link_Danfe = value; }
        }

        private static string _flagHomologacao = "";
        public string flagHomologacao
        {
            get { return _flagHomologacao; }
            set { _flagHomologacao = value; }
        }

        private static string _autorizacaoMd5 = "";
        public string MD5VALIDO
        {
            get { return _autorizacaoMd5; }
            set { _autorizacaoMd5 = value; }
        }
        private static string _codigoMsg = "";
        public string CodigoMsg
        {
            get { return _codigoMsg; }
            set { _codigoMsg = value; }
        }

        private static string _card_band = "99";
        public string card_band
        {
            get { return _card_band; }
            set { _card_band = value; }
        }

        private static string _card_aut = "";
        public string card_aut
        {
            get { return _card_aut; }
            set { _card_aut = value; }
        }

        private static string _card_cnpj = "";
        public string card_cnpj
        {
            get { return _card_cnpj; }
            set { _card_cnpj = value; }
        }
        private static string _motivoNfce = "";
        public string MotivoNfce
        {
            get { return _motivoNfce; }
            set { _motivoNfce = value; }
        }

        private static string _versaoNfce = "";
        public string versaoNfce
        {
            get { return _versaoNfce; }
            set { _versaoNfce = value; }
        }

        private static string _dataAutorizaNfce = "";
        public string dataAutorizaNfce
        {
            get { return _dataAutorizaNfce; }
            set { _dataAutorizaNfce = value; }
        }
        //ECF_MODELO_CRYP

        private static string _ECF_MODELO_CRYP = "";
        public string ECF_MODELO_CRYP
        {
            get { return _ECF_MODELO_CRYP; }
            set { _ECF_MODELO_CRYP = value; }
        }
        private static string _dataHoraRecbNfe = "";
        public string DataHoraRecbNfe
        {
            get { return _dataHoraRecbNfe; }
            set { _dataHoraRecbNfe = value; }
        }

        private static string _Recebimento = "n";
        public string Recebimento
        {
            get { return _Recebimento; }
            set { _Recebimento = value; }
        }

        private static string _RazaoHomoloNfe = "NF-E EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL";
        public string RazaoHomoloNfe
        {
            get { return _RazaoHomoloNfe; }
            set { _RazaoHomoloNfe = value; }
        }

        private static string _tipoAmbiente = "n";
        public string TipoAmbiente
        {
            get { return _tipoAmbiente; }
            set { _tipoAmbiente = value; }
        }

        private static string _Adm = "n";
        public string Adm
        {
            get { return _Adm; }
            set { _Adm = value; }
        }

        private static string _valueTrocoCard = "0";
        public string valueTrocoCard
        {
            get { return _valueTrocoCard; }
            set { _valueTrocoCard = value; }
        }

        private static string _UF;
        public string UF
        {
            get { return _UF; }
            set { _UF = value; }
        }

        private static string _flagG = "n";
        public string flagG
        {
            get { return _flagG; }
            set { _flagG = value; }
        }

        private static string _trocoCard = "";
        public string TrocoCard
        {
            get { return _trocoCard; }
            set { _trocoCard = value; }
        }

        private static string _CardCnpj = "";
        public string CardCnpj
        {
            get { return _CardCnpj; }
            set { _CardCnpj = value; }
        }

        private static string _CardTitle = "";
        public string CardTitle
        {
            get { return _CardTitle; }
            set { _CardTitle  = value; }
        }

        private static string _schema = "";
        public string Schema
        {
            get { return _schema; }
            set { _schema = value; }
        }

        private static string _LocalHostSuper = "";
        public string LocalHostSuper
        {
            get { return _LocalHostSuper; }
            set { _LocalHostSuper = value; }
        }

        private static string _userName = "";
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        private static string _senha = "";
        public string Senha
        {
            get { return _senha; }
            set { _senha = value; }
        }

        private static string _md5 = new string('\x20', 33);
        public string MD5_Main
        {
            get { return _md5; }
            set { _md5 = value; }
        }

        private static string _md5_exe = new string('\x20', 33);
        public string MD5_Main_conectorEXE
        {
            get { return _md5_exe; }
            set { _md5_exe = value; }
        }

        private static string _md5_32 = new string('\x20', 33);
        public string MD5_Main_32
        {
            get { return _md5_32; }
            set { _md5_32 = value; }
        }

        private static string _md5_64 = new string('\x20', 33);
        public string MD5_Main_64
        {
            get { return _md5_64; }
            set { _md5_64 = value; }
        }

        private static string _md5_mfd = new string('\x20', 33);
        public string MD5_Main_mfd
        {
            get { return _md5_mfd; }
            set { _md5_mfd = value; }
        }

        private static string _md_mfd3 = new string('\x20', 33);
        public string MD5_Main_mfd3
        {
            get { return _md_mfd3; }
            set { _md_mfd3  = value; }
        }

        private static string _boletoFrs = new string('\x20', 33);
        public string MD5_Main_boletoFrs
        {
            get { return _boletoFrs; }
            set { _boletoFrs = value; }
        }

        private static string _sophus = new string('\x20', 33);
        public string MD5_Main_sophus
        {
            get { return _sophus; }
            set { _sophus = value; }
        }

        private static string _cotepe = "0";
        public string AtoCotepe
        {
            get { return _cotepe; }
            set { _cotepe = value; }
        }

        private static string _copyLocal = "0";
        public string CopyLocal
        {
            get { return _copyLocal; }
            set { _copyLocal = value; }
        }

        private static int _modoOp = 0;
        public int ModoOperacao
        {
            get { return _modoOp; }
            set { _modoOp = value; }
        }

        private static int _gaveta = 0;
        public int gavetaEcf
        {
            get { return _gaveta; }
            set { _gaveta = value; }
        }

        private static string _conectorAmbient = new string('\x20', 33);
        public string MD5_Main_conectorAmbient
        {
            get { return _conectorAmbient; }
            set { _conectorAmbient = value; }
        }

        private static string _conectorBank = new string('\x20', 33);
        public string MD5_Main_conectorBank
        {
            get { return _conectorBank; }
            set { _conectorBank = value; }
        }

        private static string _conectorINTERFACEEPSON = new string('\x20', 33);
        public string MD5_Main_INTERFACEEPSON
        {
            get { return _conectorINTERFACEEPSON; }
            set { _conectorINTERFACEEPSON = value; }
        }


        private static string _conectorELGIN = new string('\x20', 33);
        public string MD5_Main_ELGIN
        {
            get { return _conectorELGIN; }
            set { _conectorELGIN = value; }
        }

        private static string _conectorSWEDA = new string('\x20', 33);
        public string MD5_Main_SWEDA
        {
            get { return _conectorSWEDA; }
            set { _conectorSWEDA = value; }
        }

        private static string _conectorDARUMA = new string('\x20', 33);
        public string MD5_Main_DARUMA
        {
            get { return _conectorDARUMA; }
            set { _conectorDARUMA = value; }
        }

        private static string _conectorBemaMFD = new string('\x20', 33);
        public string MD5_Main_BemaMFD
        {
            get { return _conectorBemaMFD; }
            set { _conectorBemaMFD = value; }
        }

        private static string _conectorBemaMFD3 = new string('\x20', 33);
        public string MD5_Main_BemaMFD3
        {
            get { return _conectorBemaMFD3; }
            set { _conectorBemaMFD3 = value; }
        }

        private static string _conectorCrypt = new string('\x20', 33);
        public string MD5_Main_conectorCrypt
        {
            get { return _conectorCrypt; }
            set { _conectorCrypt = value; }
        }

        private static string _conectorECF = new string('\x20', 33);
        public string MD5_Main_conectorECF
        {
            get { return _conectorECF; }
            set { _conectorECF = value; }
        }

        private static string _conectorInstrucao = new string('\x20', 33);
        public string MD5_Main_conectorInstrucao
        {
            get { return _conectorInstrucao; }
            set { _conectorInstrucao = value; }
        }

        private static string _conectorSetting = new string('\x20', 33);
        public string MD5_Main_conectorSetting
        {
            get { return _conectorSetting; }
            set { _conectorSetting = value; }
        }

        private static string _md5_two = new string('\x20', 33);
        public string MD5_Res
        {
            get { return _md5_two; }
            set { _md5_two = value; }
        }

        private static string _conectorSintegra = new string('\x20', 33);
        public string MD5_Main_conectorSintegra
        {
            get { return _conectorSintegra; }
            set { _conectorSintegra = value; }
        }

        private static string _conectorTef = new string('\x20', 33);
        public string MD5_Main_conectorTef
        {
            get { return _conectorTef; }
            set { _conectorTef = value; }
        }

        private static string _fiscal = new string('\x20', 33);
        public string MD5_Main_fiscal
        {
            get { return _fiscal; }
            set { _fiscal = value; }
        }

        private static string _keysPrivate = "";
        public string KeysPrivate
        {
            get { return _keysPrivate; }
            set { _keysPrivate = value; }
        }

        private static string _keysPublica = "";
        public string KeysPublica
        {
            get { return _keysPublica; }
            set { _keysPublica = value; }
        }

        private static string _keysEAD = "";
        public string KeysEAD
        {
            get { return _keysEAD; }
            set { _keysEAD = value; }
        }

        private static string _localHost = "";
        public string LocalHost
        {
            get { return _localHost; }
            set { _localHost = value; }
        }

        private static string _port = "";
        public string Port
        {
            get { return _port; }
            set { _port = value; }
        }

        private static string _pis = "";
        public string alquitoPis
        {
            get { return _pis; }
            set { _pis = value; }
        }

        private static string _Cofins = "";
        public string alquitoCofins
        {
            get { return _Cofins; }
            set { _Cofins = value; }
        }

        private static string _terminal = "";
        public string Terminal
        {
            get { return _terminal; }
            set { _terminal = value; }
        }

        private static string _autentica = "";
        public string Autentica
        {
            get { return _autentica; }
            set { _autentica = value; }
        }

        private static string _typeTef = "";
        public string typeTef
        {
            get { return _typeTef; }
            set { _typeTef = value; }
        }

        private static string _store = "";
        public string Store
        {
            get { return _store; }
            set { _store = value; }
        }
        private static string _CarregaPgto = "1";
        public string CarregaPgto
        {
            get { return _CarregaPgto; }
            set { _CarregaPgto = value; }
        }
        private static string _ecfPrinter = "";
        public string ecfPrinter
        {
            get { return _ecfPrinter; }
            set { _ecfPrinter = value; }
        }

        private static string _Desconhecido = "0";
        public string Desconhecido
        {
            get { return _Desconhecido; }
            set { _Desconhecido = value; }
        }

        private static string _urlWebConector = "http://localhost/service.asmx";
        public string UrlWebConector
        {
            get { return _urlWebConector; }
            set { _urlWebConector = value; }
        }

        public DateTime XDataTime(string _DataTime)//Data Portugues para data ingles
        {
            if (_DataTime.Length == 10)
            {
                return DateTime.ParseExact(_DataTime, "mm/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }
            else
            {
                return DateTime.ParseExact(_DataTime, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }
        }

        private static string _razaoStore = "";
        public string RazaoStore
        {
            get { return _razaoStore; }
            set { _razaoStore = value; }
        }

        private static int _trava = 0;
        public int trava
        {
            get { return _trava; }
            set { _trava = value; }
        }

        private static string _user = "";
        public string Usuario
        {
            get { return _user; }
            set { _user = value; }
        }
        private static string _login = "";
        public string Login
        {
            get { return _login; }
            set { _login = value; }
        }
        private static string _supervisor = "0";
        public string Supervisor
        {
            get { return _supervisor; }
            set { _supervisor = value; }
        }
        private static Int32 _prazoQuitacaoAfter = 30;
        public Int32 dayQuitacaoAfter
        {
            get { return _prazoQuitacaoAfter; }
            set { _prazoQuitacaoAfter = value; }
        }

        private static Int32 _prazoQuitacaoBefore = 365;
        public Int32 dayQuitacaoBefore
        {
            get { return _prazoQuitacaoBefore; }
            set { _prazoQuitacaoBefore = value; }
        }

        private static decimal _indiceSingleday = Convert.ToDecimal("0.02");
        public decimal IndiceSingleday
        {
            get { return _indiceSingleday; }
            set { _indiceSingleday = value; }
        }
        private static decimal _indiceAtrasoMora = 2;
        public decimal IndiceAtrasoMora
        {
            get { return _indiceAtrasoMora; }
            set { _indiceAtrasoMora = value; }
        }
        private static string _defaultBanco;
        public string BancoDefault
        {
            get { return _defaultBanco; }
            set { _defaultBanco = value; }
        }
        private static string _typeComissao;
        public string TypeComissao
        {
            get { return _typeComissao; }
            set { _typeComissao = value; }
        }
        private static Int32 _carenciaSingleDay = 0;
        public Int32 CarenciaSingleDay
        {
            get { return _carenciaSingleDay; }
            set { _carenciaSingleDay = value; }
        }
        private static Int32 _carenciaSingleMora = 0;
        public Int32 CarenciaSingleMora
        {
            get { return _carenciaSingleMora; }
            set { _carenciaSingleMora = value; }
        }
        private static string _idadeSpc = "18";
        public string IdadeSpc
        {
            get { return _idadeSpc; }
            set { _idadeSpc = value; }
        }
        private static string _altValuePrestacao = "1";
        public string AltValuePrestacao
        {
            get { return _altValuePrestacao; }
            set { _altValuePrestacao = value; }
        }
        private static string _altValueEntrada = "1";
        public string AltValueEntrada
        {
            get { return _altValueEntrada; }
            set { _altValueEntrada = value; }
        }
        private static string _logicaCredito = "0";
        public string LogicaCredito
        {
            get { return _logicaCredito; }
            set { _logicaCredito = value; }
        }
        private static decimal _descontoMax = 10;
        public decimal DescontoMaximoPrestacao
        {
            get { return _descontoMax; }
            set { _descontoMax = value; }
        }
        private static string _transmissaoWindows = "";
        public string TransmissaoWindows
        {
            get { return _transmissaoWindows; }
            set { _transmissaoWindows = value; }
        }

        private static string _transmissaoLinux = "";
        public string TransmissaoLinux
        {
            get { return _transmissaoLinux; }
            set { _transmissaoLinux = value; }
        }

        private static string _recepcaoLinux = "";
        public string RecepcaoLinux
        {
            get { return _recepcaoLinux; }
            set { _recepcaoLinux = value; }
        }

        private static string _recepcaoWindows = "";
        public string RecepcaoWindows
        {
            get { return _recepcaoWindows; }
            set { _recepcaoWindows = value; }
        }

        private static string _perfil = "";
        public string Perfil
        {
            get { return _perfil; }
            set { _perfil = value; }
        }

        private static string _typeComunicacao = "";
        public string TypeComunicacao
        {
            get { return _typeComunicacao; }
            set { _typeComunicacao = value; }
        }

        private static int _ImportCarga = 0;
        public int ImportCarga
        {
            get { return _ImportCarga; }
            set { _ImportCarga = value; }
        }

        private static string _cnpj = "";
        public string CNPJ
        {
            get { return _cnpj; }
            set { _cnpj = value; }
        }
        //######################################Configuracao Banco
        private static string _caixa = "";
        public string NumeroCaixa
        {
            get { return _caixa; }
            set { _caixa = value; }
        }
        private static string _ipCaixa = "";
        public string IpCaixa
        {
            get { return _ipCaixa; }
            set { _ipCaixa = value; }
        }

        private static string _aberturaTroco = "";
        public string AberturaTroco
        {
            get { return _aberturaTroco; }
            set { _aberturaTroco = value; }
        }
        private static string _imprimiCheque = "";
        public string ImprimiCheque
        {
            get { return _imprimiCheque; }
            set { _imprimiCheque = value; }
        }

        private static string _timeBlock = "";
        public string TimeBlock    
        {
            get { return _timeBlock; }
            set { _timeBlock = value; }
        }
        private static string _blockTime    = "";
        public string BlockTime
        {
            get { return _blockTime; }
            set { _blockTime = value; }
        }
        private static string _trocaMercadoria = "";
        public string TrocaMercadoria
        {
            get { return _trocaMercadoria; }
            set { _trocaMercadoria = value; }
        }
        private static string _carneRecebe = "";
        public string CarneRecebe
        {
            get { return _carneRecebe; }
            set { _carneRecebe = value; }
        }
        private static string _codigoEmpresaTef = "";
        public string CodigoEmpresaTef
        {
            get { return _codigoEmpresaTef; }
            set { _codigoEmpresaTef = value; }
        }
        private static string _trocoMax = "";
        public string TrocoMax
        {
            get { return _trocoMax; }
            set { _trocoMax = value; }
        }
        private static string _serie = "";
        public string Serie
        {
            get { return _serie; }
            set { _serie = value; }
        }
        private static string _serie_hash = "";
        public string Serie_Hash
        {
            get { return _serie_hash; }
            set { _serie_hash = value; }
        }

        private static string _serie_hash_hard = "";
        public string Serie_Hash_Hard
        {
            get { return _serie_hash_hard; }
            set { _serie_hash_hard = value; }
        }

        private static bool _serie_valido = false;
        public bool Serie_Valido
        {
            get { return _serie_valido; }
            set { _serie_valido = value; }
        }
        private static bool _gt_valido = false;
        public bool Gt_Valido
        {
            get { return _gt_valido; }
            set { _gt_valido = value; }
        }

        private static string _GT_BANCO = "";
        public string GT_BANCO
        {
            get { return _GT_BANCO; }
            set { _GT_BANCO = value; }
        }

        private static string _Identificacao_Ecf = "";
        public string Identificacao_Ecf
        {
            get { return _Identificacao_Ecf; }
            set { _Identificacao_Ecf = value; }
        }

        private static string _GT_CRZ = "";
        public string GT_CRZ
        {
            get { return _GT_CRZ; }
            set { _GT_CRZ = value; }
        }

        private static string _GT_CRO = "";
        public string GT_CRO
        {
            get { return _GT_CRO; }
            set { _GT_CRO = value; }
        }

        private static string _utilizaTeclado = "";
        public string UtilizaTeclado
        {
            get { return _utilizaTeclado; }
            set { _utilizaTeclado = value; }
        }
        private static string _utilizaTef = "";
        public string UtilizaTef
        {
            get { return _utilizaTef; }
            set { _utilizaTef = value; }
        }
        private static string _utilizaBalanca = "";
        public string UtilizaBalanca
        {
            get { return _utilizaBalanca; }
            set { _utilizaBalanca = value; }
        }
        private static string _utilizaEcf = "";
        public string UtilizaEcf
        {
            get { return _utilizaEcf; }
            set { _utilizaEcf = value; }
        }
        private static string _portEcf = "";
        public string PortEcf
        {
            get { return _portEcf; }
            set { _portEcf = value; }
        }
        private static string _portTef = "";
        public string PortTef
        {
            get { return _portTef; }
            set { _portTef = value; }
        }
        private static string _funcaoProgramada = "";
        public string FuncaoProgramada
        {
            get { return _funcaoProgramada; }
            set { _funcaoProgramada = value; }
        }
        private static string _msgTef = "";
        public string MsgTef
        {
            get { return _msgTef; }
            set { _msgTef = value; }
        }
        private static string _portBalanca = "";
        public string PortBalanca
        {
            get { return _portBalanca; }
            set { _portBalanca = value; }
        }

        private static string _idModeloPrinter = "";
        public string ModeloPrinter
        {
            get { return _idModeloPrinter; }
            set { _idModeloPrinter = value; }
        }
        private static string _statusPDV = "";
        public string StatusPDV
        {
            get { return _statusPDV; }
            set { _statusPDV = value; }
        }
        private static string _emitiVinculo = "";
        public string EmitiVinculo
        {
            get { return _emitiVinculo; }
            set { _emitiVinculo = value; }
        }
        private static string _VinculoCrediario = "";
        public string VinculoCrediario
        {
            get { return _VinculoCrediario; }
            set { _VinculoCrediario = value; }
        }
        private static string _VinculoConvenio = "";
        public string VinculoConvenio
        {
            get { return _VinculoConvenio; }
            set { _VinculoConvenio = value; }
        }
        private static string _VinculoCartaoCr = "";
        public string VinculoCartaoCf
        {
            get { return _VinculoCartaoCr; }
            set { _VinculoCartaoCr = value; }
        }
        private static string _VinculoCartaoDb = "";
        public string VinculoCartaoDb
        {
            get { return _VinculoCartaoDb; }
            set { _VinculoCartaoDb = value; }
        }
        private static string _alertaSangria = "";
        public string AlertaSangria
        {
            get { return _alertaSangria; }
            set { _alertaSangria = value; }
        }

        private static string _valueSangria = "";
        public string ValueSangria
        {
            get { return _valueSangria; }
            set { _valueSangria = value; }
        }
        private static int _chave_caixa = 0;
        public int CHAVE_CAIXA
        {
            get { return _chave_caixa; }
            set { _chave_caixa = value; }
        }

        private static string _liberacaocaoCredito;
        public string LiberacaocaoCredito
        {
            get { return _liberacaocaoCredito; }
            set { _liberacaocaoCredito = value; }
        }

        private static string _flagParametro;
        public string FlagParametro
        {
            get { return _flagParametro; }
            set { _flagParametro = value; }
        }

        private static string _limiteRendaBase = "0";
        public string LimiteRendaBase
        {
            get { return _limiteRendaBase; }
            set { _limiteRendaBase = value; }
        }

        private static string _categoriaLimite;
        public string CategoriaLimite
        {
            get { return _categoriaLimite; }
            set { _categoriaLimite = value; }
        }

        private static string _variacaoLimite;
        public string VariacaoLimite
        {
            get { return _variacaoLimite; }
            set { _variacaoLimite = value; }
        }

        //########################################Identificação PAF ECF
        private static string _PAF_Contato;
        public string PAF_Contato
        {
            get { return _PAF_Contato; }
            set { _PAF_Contato = value; }
        }
        private static string _PAF_contatoCom;
        public string PAF_contatoCom
        {
            get { return _PAF_contatoCom; }
            set { _PAF_contatoCom = value; }
        }

        private static string _PAF_laudo;
        public string PAF_laudo
        {
            get { return _PAF_laudo; }
            set { _PAF_laudo = value; }
        }
        private static string _PAF_Endereco;
        public string PAF_Endereco
        {
            get { return _PAF_Endereco; }
            set { _PAF_Endereco = value; }
        }
        private static string _PAF_NumeroAplicativo;
        public string PAF_NumeroAplicativo
        {
            get { return _PAF_NumeroAplicativo; }
            set { _PAF_NumeroAplicativo = value; }
        }

        private static string _PAF_CNPJ;
        public string PAF_CNPJ
        {
            get { return _PAF_CNPJ; }
            set { _PAF_CNPJ = value; }
        }

        private static string _PAF_IE;
        public string PAF_IE
        {
            get { return _PAF_IE; }
            set { _PAF_IE = value; }
        }

        private static string _PAF_IM;
        public string PAF_IM
        {
            get { return _PAF_IM; }
            set { _PAF_IM = value; }
        }
        private static string _PAF_RAZAO;
        public string PAF_RAZAO
        {
            get { return _PAF_RAZAO; }
            set { _PAF_RAZAO = value; }
        }
        private static string _PAF_FONE;
        public string PAF_TELEFONE
        {
            get { return _PAF_FONE; }
            set { _PAF_FONE = value; }
        }
        private static string _PAF_VERSAO;
        public string PAF_Versao
        {
            get { return _PAF_VERSAO; }
            set { _PAF_VERSAO = value; }
        }
        private static string _PAF_VERSAO_Spec;
        public string PAF_Versao_Spec
        {
            get { return _PAF_VERSAO_Spec; }
            set { _PAF_VERSAO_Spec = value; }
        }
        private static string _PAF_total;
        public string PAF_total
        {
            get { return _PAF_total; }
            set { _PAF_total = value; }
        }
        private static string _EstiloNfce;
        public string EstiloNfce
        {
            get { return _EstiloNfce; }
            set { _EstiloNfce = value; }
        }
        //###############################################################End Variaveis e Metodos Globais########################################################
        private dados banco1;

        public void carrega_infor(string store)
        {
            int retorno = 0;
            try
            {
                banco1 = new dados();
                banco1.abreConexao();
                if (banco1.statusSchema() == 1)
                {
                    banco1.singleTransaction("select versaoBanco, versaoSystem, conectorServer, localMachini from system");
                    banco1.procedimentoRead();
                    if (banco1.retornaRead().Read() == true)
                    {
                        VersaoBanco = banco1.retornaRead().GetString(0);
                        VersaoSystem = banco1.retornaRead().GetString(1);
                        ConectorServer = banco1.retornaRead().GetString(2);
                        LocalMonitor = banco1.retornaRead().GetString(3);
                    }
                    else retorno = 0;
                }
                else
                {
                    msgInfo msg = new msgInfo(1, "IMPOSSÍVEL ESTABELECER CONEXÃO"); msg.ShowDialog();
                    retorno = 0;
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                if (banco1 != null)
                {
                    banco1.fechaConexao();
                }
            }
        }
    }
}
