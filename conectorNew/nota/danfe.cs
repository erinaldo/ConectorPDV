using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using mva;
using fiscal;
using System.Security.Cryptography;
using conectorPDV001;
namespace conectorPDV001
{
    public partial class danfe : Form
    {
        public danfe()
        {
            InitializeComponent();
            zeraObj();
            auxIdLoja = alwaysVariables.Store;
            typeEmissaoNota = 0;
        }
        public danfe(string client, string loja, string user, string pdv, string funcionario, string config, Int16 typeFrete, string transporte, string idPedido, string descricaoFaturamento, string nome, string razao)
        {
            InitializeComponent();
            auxIdCliente = client;
            optionCodigoTransportadora = transporte;
            auxModalidadeFrete = typeFrete;
            auxIdParamentro = config;
            auxIdFuncionario = funcionario;
            auxIdLoja = loja;
            auxIdTerminal = pdv;
            auxIdUser = user;
            auxIdPedido = idPedido;
            auxDescricaoOrigem = razao;
            auxDescricaoDestino = nome;
            auxDescricaoFaturamento = descricaoFaturamento;
            tslUser.Text = tslUser.Text + ": " + user;
            tslLoja.Text = tslLoja.Text + ": " + loja;
            tslTerminal.Text = tslTerminal.Text + ": " + pdv;
            tslVendedor.Text = tslVendedor.Text + ": " + funcionario;
            tslPedido.Text = tslPedido.Text + ": " + idPedido;
            typeEmissaoNota = 1;
            btnImprimiNotaFiscal.Enabled = false;
            btnValidaNfeNotaFiscal.Enabled = false;
        }
        public danfe(string loja, string user, string pdv)
        {
            InitializeComponent();
            zeraObj();
            auxIdLoja = loja;
            auxIdTerminal = pdv;
            auxIdUser = user;
            tslUser.Text = tslUser.Text + ": " + user;
            tslLoja.Text = tslLoja.Text + ": " + loja;
            tslTerminal.Text = tslTerminal.Text + ": " + pdv;
            typeEmissaoNota = 0;
            //btnImprimiNotaFiscal.Enabled = false;
            //btnValidaNfeNotaFiscal.Enabled = false;
        }
        public danfe(string nr, string nf, string client, string loja, string user, string pdv, string funcionario, string config, Int16 typeFrete, string transporte, string idPedido, string descricaoFaturamento, string nome, string razao, bool test, Int16 printer)
        {
            InitializeComponent();
            auxIdNf = nf;
            flagSemaforo = 1;
            auxIdCliente = client;
            optionCodigoTransportadora = transporte;
            auxModalidadeFrete = typeFrete;
            auxIdParamentro = config;
            auxIdFuncionario = funcionario;
            auxIdLoja = loja;
            auxIdTerminal = pdv;
            auxIdUser = user;
            auxIdPedido = idPedido;
            auxDescricaoOrigem = razao;
            auxDescricaoDestino = nome;
            auxDescricaoFaturamento = descricaoFaturamento;
            tslUser.Text = tslUser.Text + ": " + user;
            tslLoja.Text = tslLoja.Text + ": " + loja;
            tslTerminal.Text = tslTerminal.Text + ": " + pdv;
            tslVendedor.Text = tslVendedor.Text + ": " + funcionario;
            tslPedido.Text = tslPedido.Text + ": " + idPedido;
            typeEmissaoNota = 1;
            if (nf != "")
            {
                if (Convert.ToDouble(nr) > 0)
                {
                    somenteLeitura(false);
                }
                else
                {
                    somenteLeitura(test);
                }
            }
            if (printer == 1)
            {
                    btnImprimiNotaFiscal.Enabled = true;
                    btnValidaNfeNotaFiscal.Enabled = true;
            }
            else
            {
                btnImprimiNotaFiscal.Enabled = false;
                btnValidaNfeNotaFiscal.Enabled = false;
            }
        }
        
        //##################################################### Variaveis Encapsuladas ###################################################
        private Decimal auxNumeric, qtty, discount, despesas;
        private DataSet returnSet;
        private string flagNumeric;
        private Int16 typeEmissaoNota = 0; //0 - alimentada manualmente 1 - gerada pelo pedido de venda
        private int i, j, countField, countRows, auxConsistencia, retorno; //variavel do loop.
        private int ataque = 0;
        private int sinal = 0;
        private string auxNfe = "n";
        string returnEMB, returnCST, returnPIS, returnCOFINS, returnIPI, enabled; //Variaveis de return quanto aos combos
        private int flagRetornoTotalItens; //Confere a quantidade de itens inseridos na nota
        private int flagSemaforo = 0;
        private int flagSemaforoItem = 0;
        private int posSeparator;
        private string auxTypeAtividade; //1 cliente, 2 fornecedor, 7 loja
        private string optionDDD;
        private geraNFe transmiteNfe = new geraNFe();
        private Boolean auxEmissao; // true - com pedido false - sem pedido
        private conector_full_variable alwaysVariables = new conector_full_variable();
        private string optionTelefone;
        private substituicao calculo = new substituicao();
        private imposto calculoImpostos = new imposto();
        private consultaCliente consulta;
        private pesquisaLoja consultaLoja;
        private itemDevolucao devItem;
        private pesquisaFornecedor consultaFornecedor;
        private insertTypePepleo typePepleo;
        private consultaTransporte findTransporte;
        private consultaNota openNotaEntrada;
        private string auxTypeVenda = "2"; //outros
        private string auxIdFuncionario;
        private string auxIdLoja;
        private string auxDescricaoOrigem;
        private string auxDescricaoDestino;
        private string auxDescricaoFaturamento;
        private string auxIdUser;
        private string auxCondicaoMetodo;
        private string flagGeraDanfe = "n";
        private string auxIdTerminal;
        private string auxIdCliente;
        private string auxIdPedido;
        private string auxSinalPedido = "verde";
        private string auxCaixaPedido;
        private string auxFinalPedido =  "3";// 0 - aberto 1 - emitido 2 - faturado 3 - fiscal 4 - finalizado 5 - cancelado 6 - leitura
        private string auxIdNf = "";
        private string auxIdEntrada = "";
        private string auxIdNfTransportadora = "";
        private string auxIdNfVeiculo = "";
        private string auxIdNfItem = "";
        private string auxOrigemItem = "0" ; // 0 nacional e 1 estrageira
        private string auxCstIpiProduto;
        private string auxCstPisProduto;
        private string auxCstCofinsProduto;
        private string auxIdGenero;
        private string optionSeguro;
        private string optionProdutoPisSt;
        private string optionProdutoPisCofinsSt;
        private string auxCstProduto;
        private string optionCstPisEmissaoPisCofinsProduto;
        private string optinoCstCofinsEmissaoPisCofinsProduto;
        private string optionCstIPIEmissaoProduto;
        private string auxIdUnidade;
        private string auxIdCodigoFiscal;
        private string auxIdModeloNotaFiscal;
        private string auxIdSituacaoFiscal;
        private string optionIdPgtoFornecedor;
        private string optionflagPgtoDevolucao;
        private dados banco = new dados();
        private int auxTipoPessoa;
        private pesquisaCFOP findCFOP;
        private pesquisaProduto pesquisaProd;
        private string auxIdProduto;
        private string auxIdCFOP = "";
        private string auxIdCFOPItemNotaFiscal = "";
        private string auxTypeCFOP;
        private string auxNrNota;
        private Int16 auxModalidadeFrete = 9;
        private string auxCalculoValorPis;
        private string auxCalculoValorCofins;
        private string auxCalculoValorAcrescimo; //DespesasAcessorias
        private string auxPorcentagemAcrescimo;
        private string auxCalculoValorTotalLiquido;
        private string auxCalculoSeguro;
        private string auxCalculoBasePis;
        private string auxCalculoBaseCofins;
        private string auxCalculoBaseIcmsIsento;
        private string auxCalculoTotalItens;
        private string auxPorcentagemDesconto;
        private string auxCalculoBaseIPI;
        private string auxTotalVolumeNf;
        private string auxCalculoContribucaoSocial;
        private string auxQttyPedido;
        private string auxQttyRecebida;
        private string auxCondPgto;
        //##################################################### MVA PARAMENTROS ##########################################################
        private decimal valorProduto;
        private string optionSomaProdutoFinanceiro;
        private string optionProdutoMVA;
        private string optionProdutoMVAInterestadual;
        private string optionProdutoMVAInterna;
        private string optionProdutoMVAAjusteMva;
        //#######################################################END MVA #################################################################

        //##################################################Paramentro do Item 'Produto'##################################################
        private string optionIdOperacaoEntrada;
        private string optionProdutoCodigoAliquota;
        private string optionProdutoIDPisCofins;
        private string optionProdutoValorLiquido = "0.00";
          private string optionProdutoDescricao;
          private string optionProdutopriceOriginal = "0.00";
          private string optionProdutoNCM;
          private string optionProdutoFornecedor;
          private string optionProdutopriceVenda;
          private string optionProdutopriceCusto;
          private string optionProdutoestoque;
          private string optionProdutodata;
          private string optionProdutopeso = "0";
          private string optionProdutoaliquota;
          private string optionProdutocstCalcBc;
          private string optionProdutocstCalcRed;
          private string optionProdutocsttypeCst; // 's - substituição\nt - tributado\ni - isenta' 
          private string optionProdutocstModalidade; // 'v - valor da operação\nm - margem valor agregado\np - pauta\nt - tabela preço'
          private string optionProdutoicms;
          private string optionProdutobaseCalculo = "0.00";
          private string optionProdutoreducao = "0.00";
          private string optionProdutoTypeAliquota;
          private string optionProdutoChaveEntradaItem;
          private string optionProdutoquantidade = "0.00";
          private string optionProdutoidunidadeMedida;
          private string optionProdutocfop;
          private string optionProdutocstIcms;
          private string optionProdutocstPis;
          private string optionProdutovalorPis = "0.00";
          private string optionProdutobasePis = "0.00";
          private string optionProdutocstCofins;
          private string optionProdutovalorCofins = "0.00";
          private string optionProdutobaseCofins = "0.00";
          private string optionProdutocstIpi;
          private string optionProdutoipi;
          private string optionProdutoipiValor = "0.00";
          private string optionProdutobaseIpi = "0.00";
          private string optionProdutodesconto = "0.00";
          private string optionProdutodescontoValor = "0.00";
          private string optionProdutoacrescimo  = "0.00";
          private string optionProdutoacrescimoValor  = "0.00";
          private string optionProdutoaliquotaIcmsSt;
          private string optionProdutobaseCalculoIcmsSubstituicao = "0.00";
          private string optionProdutovalorIcmsSubstituicao = "0.00";
          private string optionProdutoIcmsSt;
          private string optionProdutoIcmsStRecolher;
          private string optionProdutomargem;
          private string optionProdutovalorTotalProduto = "0.00";
          private string optionProdutovalorTotalNota = "0.00";
          private string optionProdutovalorTotalLiquido = "0.00";
          private string optionProdutoIdSetor;
        //##################################################End Paramentro do Item 'Produto'######################################
        //###########################Paramentro OperacaoEntrada##################################
        private string optionOEInCfopSugestãoOperacaoEntrada;
        private string optionOEOnCfopSugestãoOperacaoEntrada;

        //###########################Paramentro configuracao codigo barra########################
        private string auxIdUnItemNotaFiscal;
        private string auxUnItemNotaFiscal;
        private string optionBarraItemNotaFiscal;
        private string auxMultiplicador;
        private string optionIdBarraItemNotaFiscal;
        //####################################End codigo Barra ##################################
        //###########################Paramentro configuração do faturamento######################
        private string auxIdParamentro;
        private string auxEstoqueDestino;
        private string auxEstoqueOrigem;
        private string auxEstoqueDestinoKit;
        private string auxEstoqueOrigemKit;
        private string auxFlagEanDefault;
        private string auxFlagRestrigeCliente;
        private string auxFlagTypePrice;
        private string optionDescricaoFaturamento;
        private string optionStatusFaturamento; //0 = Inativo 1 = Ativo
        private string optionFlagParamentro; //v - venda; t - transferencia; r - troca; d - devolução; c - consumo; p - perda; q -  quebra; e - entrada; f - franquia; s - simples faturamento; n - conserto
        private string optionTodosOrigemFaturamento;
        private string optionFornecedorOrigemFaturamento;
        private string optionLojaOrigemFaturamento;
        private string optionTrocaOrigemFaturamento;
        private string optionClienteOrigemFaturamento;
        private string optionClienteCpfCnpj;
        private string optionTodosDestinoFaturamento;
        private string optionFornecedorDestinoFaturamento;
        private string optionLojaDestinoFaturamento;
        private string optionTrocaDestinoFaturamento;
        private string optionClienteDestinoFaturamento;
        private string optionVendaTypeFaturamento;
        private string optionTrocaTypeFaturamento;
        private string optionBrindeTypeFaturamento;
        private string optionConsertoTypeFaturamento;
        private string optionEntradaTypeFaturamento;
        private string optionTransferenciaTypeFaturamento;
        private string optionQuebraTypeFaturamento;
        private string optionOrcamentoTypeFaturamento;
        private string optionEntregaFuturaTypeFaturamento;
        private string optionConsumoTypeFaturamento;
        private string optionPerdaTypeFaturamento;
        private string optionProducaoTypeFaturamento;
        private string optionServicoTypeFaturamento;
        private string optionSimplesFaturamentoTypeFaturamento;
        private string optionDevolucaoTypeFaturamento;
        private string optionFranquiaTypeFaturamento;
        private string optionForceVendedorFaturamento;
        private string optionForceClienteFaturamento;
        private string optionForceMetodoFaturamento;
        private string optionForcetranspoteFaturamento;
        private string optionForceAtualizacaoFaturamento = "1";
        private string optionForcePagamentoFaturamento;
        private string optionforceAdressEntregaFaturamento;
        private string optionEstoqueDestinoEntradaFaturamento;
        private string optionEstoqueDestinoSaidaFaturamento;
        private string optionEstoqueDestinoNaoMovFaturamento;
        private string optionEstoqueOrigemEntradaFaturamento;
        private string optionEstoqueOrigemSaidaFaturamento;
        private string optionEstoqueOrigemNaoMovFaturamento;
        private string optionEstoqueNegativoBloqueiaFaturamento;
        private string optionEstoqueNegativoPermitiFaturamento;
        private string optionEstoqueNegativoMsgFaturamento;
        private string optionFixaOrigemFaturamento;
        private string optionFixaDestinoFaturamento;
        private string optionIdOrigemFixaFaturamento;
        private string optionIddestinoFixoFaturamento;
        private string optionDescDestinoFixoFaturamento;
        private string optionDescOrigemFixaFaturamento;
        private string optionRestrigeClienteFaturamento;
        private string optionCustoLiquidoFaturamento;
        private string optionEmissaoFixaFaturamento;
        private string optionEstoqueLojaFaturamento;
        private string optionAtualizaComNfFaturamento;
        private string optionOrigemDestinoIguaisFaturamento;
        private string optionExclusaoPedidosFaturamento;
        private string optionLiberaDescontoFaturamento;
        private string optiondigitaDescontoValorFaturamento;
        private string optionDigitaDescontoPercentualFaturamento;
        private string optionFlagExpiracaoFaturamento;
        private string optionDiasEspiracaoFaturamento;
        private string optionflagPermiteAtualizarFaturamento;
        private string optionflagAlteraLojaFaturamento;
        private string optionConfigDescontoGlobalFaturamento;
        private string optionDescontoSetorFaturamento;
        private string optionDescontoPdvFaturamento;
        private string optionModuloFaturamento;
        private string optionModuloVendedorFaturamento;
        private string optionModuloGerenteFaturamento;
        private string optionModuloDiretorFaturamento;
        private string optionModuloSupervisorFaturamento;
        private string optionEstoqueDestinoKitMovProdutoFaturamento;
        private string optionEstoqueDestinoKitMovItemFaturamento;
        private string optionEstoqueOrigemKitMovProdutoFaturamento;
        private string optionEstoqueOrigemKitMovItemFaturamento;
        private string optionflagDetalhesItemFaturamento;
        private string optionflagEstoqueFuturaFaturamento;
        private string optionRestrigeFinanceiroFaturamento;
        private string optionRestrigeAltpgtoFaturamento;
        private string optionLimiteCreditoFaturamento;
        private string optionCarenciaAprazoFaturamento;
        private string optionFlagTrocoFaturamento;
        private string optionGeraReceberFaturamento;
        private string optionGeraCrediarioFaturamento;
        private string optionGeraPargarDestinoFaturamento;
        private string optionGeraPargarOrigemFaturamento;
        private string optionGeraChequeFaturamento;
        private string optionFlagAlteraPrecoFaturamento;
        private string optionFlagAlteraComissaoFaturamento;
        private string optionFlagAlteraDescontoFaturamento;
        private string optionFlagAlteraIpiFaturamento;
        private string optionFlagAlteraVendedorFaturamento;
        private string optionFlagQttyZeradaFaturamento;
        private string optionFlagItemZeradaFaturamento;
        private string optionFlagEntradaFaturamento;
        private string optionFlagExplodeKitFaturamento;
        private string optionFlagMaxItemPedidoFaturamento;
        private string optionNumeroMaxItemFaturamento;
        private string optionFlagRepetItemFaturamento;
        private string optionFlagAltExcIncItemFaturamento;
        private string optionFlagBloquearItemFaturamento;
        private string optionFlagEanDefaultTodosFaturamento;
        private string optionFlagEanDefaultTransfFaturamento;
        private string optionFlagEanDefaultVendaFaturamento;
        private string optionFlagEanDefaultCompraFaturamento;
        private string optionBloqueiaQuantidadeFaturamento;
        private string optionFlagRestrigeClientePemitirFaturamento;
        private string optionFlagRestrigeClienteAutoFaturamento;
        private string optionFlagRestrigeClienteManualFaturamento;
        private string optionFlagRestrigeClienteMessagemFaturamento;
        private string optionFlagClienteAvistaFaturamento;
        private string optionFlagClienteDocCorretosFaturamento;
        private string optionFlagClienteEmailFaturamento;
        private string optionFlagTypePriceVendaFaturamento;
        private string optionFlagTypePriceCustoLiquidoFaturamento;
        private string optionFlagTypePriceCustoMedioFaturamento;
        private string optionFlagTypePriceCustoFabricaFaturamento;
        private string optionFlagAtualizaMedioFaturamento;
        private string optionFlagRatearDescontoFaturamento;
        private string optionFlagVerificaPriceCustoFaturamento;
        private string optionFlagComissaoProdutoFaturamento;
        private string optionFlagComissaoIgnoraFaturamento;
        private string optionFlagComissaoFaturamento;
        private string optionFlagComissaoBaixaFaturamento;
        private string optioncfop1;
        private string optioncfop2;
        private string optioncfop3;
        private string optioncfop4;
        private string optionflagCalculaIcms;
        private string optionflagCalculaPisCofins;
        private string optionflagCalculaIr;
        private string optionflagCalculaBaseSt;
        private string optionflagCalculoServicoProduto;
        private string optionflagCalculoServico;
        private string optionflagCalculaIpi;
        private string optionflagNFRestituicao;
        private string optionflagForceNfOrigem;
        private string optionflagTypeFrete;
        //#########################################End variaveis de configuração#######################
        //########################################Parametro configuracao Transportadora############
        private string optionCodigoTransportadora = "0";
        private string optionRazaoTransportadora;
        private string optionCnpjTransportadora;
        private string optionIeTransportadora;
        private string optionAbreviaturaTransportadora;
        private string optionCodigoMunicipioTransportadora;
        private string optionCepTransportadora;
        private string optionidCepBairroTransportadora;
        private string optionInativaTransportadora;
        private string optionEnderecoTransportadora;
        private string optionBairroTransportadora;
        private string optionNumberTransportadora;
        private string optionCidadeTransportadora;
        private string optionUfTransportadora;
        private string optionComplementoTransportadora;
        private string auxIdUfTransportadora;
        private string auxIdEnderecoTransportadora;
        private string optionAliquotaFrete;
        //###veiculo
        private string optionCFOPFrete;
        private string optionDescricaoTipoFrete;
        private string optionBaseCalculoFrete;
        private string optionIcmsFrete;
        private string optionValorTotalFrete;
        private string optionIdVeiculo;
        private string optionPlacaVeiculo;
        private string optionAnttVeiculo;
        private string optionVolumeQtty;
        private string optionVolumePesoNumber;
        private string optionVolumePesoBruto;
        private string optionVolumePesoLiquido;
        private string optionVolumeMarca;
        private string optionVolumeEspecie;
        private string auxGrididestado;
        private string auxGridUFVeiculo;
        private string optionIsentoIcms;
        //########################################End Transportadora
        //########################################Parametro configuração loja######################
        private string optionLojaRazao;
        private string optionLojaFantasia;
        private string optionLojaCgc;
        private string optionLojaIe;
        private string optionLojaIeMunicipio;
        private string optionLojaType;
        private string optionLojaCodigoEstado;
        private string optionLojaLojaUf;
        private string optionLojaCodigoMunicipio;
        private string optionLojaTypeLoja;
        private string optionLojaaliquotaPis;
        private string optionLojaAliquotaCofins;
        private string optionLojaControlaEstoque;
        private string optionLojaTypeCalculo;
        private string optionLojaEmpresaTroca;
        private string optionLojaAliquotaInss;
        private string optionLojaAliquotaIss;
        private string optionLojaMatriz;
        private string optionLojaDeposito;
        private string optionLojaSerieNota;
        private string optionLojaNumeroNota;
        private string optionLojaAtualizaCusto;
        private string optionLojaStatus;
        private string optionLojaRamo; // Altera a situação do imposto quanto ao ramo de atuação da empresa variaveis => r = lucro real "Todos os impostos e contribuição social " p = pequeno porte "minimiza os impostos" s = "simples minas" h = "super sim
        private string optionLojaBairro;
        private string optionLojaComplemento;
        private string optionLojaMunicipio;
        private string optionLojaEstado;
        private string optionLojaNumber;
        private string optionLojaCEP;
        private string optionLojaCodEnd;
        private string optionLojaSeq;
        private string optionLojaIdBairro;
        private string optionLojaTipoEndereco;
        private string optionLojaLogradouro;
        private string optionLojaIdPais; //se igual a 30 nacional, diferente estrangeiro
        private string optionLojaCodigoUf;
        //########################################End loja#########################################
        //########################################Parametro configuração endereço######################
        private string auxIdEndereco;
        private string optionUfEstado;
        private string optionBairroCliente;
        private string optionCityCliente;
        private string optionNumberCliente;
        private string optionComplementoCliente;
        private string optionLogradouroCliente;
        private string optionCepCliente;
        private string optionIdCepCliente;
        private string optionCodigUfCliente;
        //########################################END configuração endereço######################
        //########################################Parametro configuração endereçoEntrega######################
        private string auxIdEnderecoEntrega;
        private string optionEntregaUfEstado;
        private string optionEntregaBairroCliente;
        private string optionEntregaCityCliente;
        private string optionEntregaNumberCliente;
        private string optionEntregaComplementoCliente;
        private string optionEntregaLogradouroCliente;
        private string optionEntregaCepCliente;
        private string optionEntregaIdCepCliente;
        private string optionEntregaCodigMunicipio;
        //########################################END configuração endereçoEntrega######################
        //########################################Parametro configuração cliente######################
        private string optionClientechave;
        private string optionClientePais;
        private string optionClientePaisDescricao;
        private string optionClienteMail;
        private string optionClienteNomeRazao;
        private string optionClienteDocumentoIdentidadeIe;
        private string optionClienteEmpresa;
        private string optionClienteChaveCivil;
        private string optionClienteFantasia;
        private string optionClienteUf;
        private string optionClienteNascimentoAbertura;
        private string optionClienteSexo;
        private string optionClienteIdTipoFornecedor;
        private string optionClienteChaveLoja;
        private string optionClienteIdtipoPessoa;
        private string optionClienteIdUsuario;
        private string optionClienteIdAtividade;
        private string optionClienteObs;
        private string optionClienteDataEmissao;
        private string optionClienteDataAlteracao;
        private string optionClienteChaveEstado;
        private string optionClienteCivil;
        private string optionClienteIeProdutor;
        private string optionClienteChaveSexo;
        private string optionClienteStatus;
        private string optionClienteCodigoMunicipio;
        //########################################End cliente#########################################
        //#################################################### End variaveis #############################################################
        //############
        private List<dadosIdentificacaoNfe> listaIdentificacaoNfe = new List<dadosIdentificacaoNfe>();
        private List<dadosDocumentoFiscalDiferenciado> listaDocumentoFiscalDiferenciado = new List<dadosDocumentoFiscalDiferenciado>();
        private List<dadosIdentificacaoEmitenteNfe> listaIdentificacaoEmitenteNfe = new List<dadosIdentificacaoEmitenteNfe>();
        private List<dadosIdentificacaoDestinatárioNfe> listaIdentificacaoDestinatarioNfe = new List<dadosIdentificacaoDestinatárioNfe>();
        private List<dadosIdentificacaoLocalRetirada> listaIdentificacaoLocalRetirada = new List<dadosIdentificacaoLocalRetirada>();
        private List<dadosIdentificacaoLocalEntrega> listaIdentificacaoLocalEntrega = new List<dadosIdentificacaoLocalEntrega>();
        private List<dadosAutorizacaoObterXml> listaAutorizacaoObterXml = new List<dadosAutorizacaoObterXml>();
        private List<dadosDetalhamentoProdutosNfe> listaDetalhamentoProdutosNfe = new List<dadosDetalhamentoProdutosNfe>();
        private List<dadosProdutosServicosNfe> listaProdutosServicosNfe = new List<dadosProdutosServicosNfe>();
        private List<dadosProdutosServicosDeclaracaoImportacao> listaProdutosServicosDeclaracaoImportacao = new List<dadosProdutosServicosDeclaracaoImportacao>();
        private List<dadosProdutosServicosGrupoExportacao> listaProdutosServicosGrupoExportacao = new List<dadosProdutosServicosGrupoExportacao>();
        private List<dadosProdutosServicosPedidoCompra> listaProdutosServicosPedidoCompra =new List<dadosProdutosServicosPedidoCompra>();
        private List<dadosProdutosServicosGrupoDiversos> listaProdutosServicosGrupoDiversos = new List<dadosProdutosServicosGrupoDiversos>();
        private List<dadosDetalhamentoEspecificoVeiculosNovos> listaDetalhamentoEspecificoVeiculosNovos = new List<dadosDetalhamentoEspecificoVeiculosNovos>();
        private List<dadosDetalhamentoEspecificoMedicamentoMateriasPrimasFarmaceuticas> listaDetalhamentoEspecificoMedicamentoMateriasPrimasFarmaceuticas = new List<dadosDetalhamentoEspecificoMedicamentoMateriasPrimasFarmaceuticas>();
        private List<dadosDetalhamentoEspecificoArmamentos> listaDetalhamentoEspecificoArmamentos = new List<dadosDetalhamentoEspecificoArmamentos>();
        private List<dadosDetalhamentoEspecificoCombustiveis> listaDetalhamentoEspecificoCombustiveis = new List<dadosDetalhamentoEspecificoCombustiveis>();
        private List<dadosDetalhamentoEspecificoOperacaoPapelImune> listaDetalhamentoEspecificoOperacaoPapelImune = new List<dadosDetalhamentoEspecificoOperacaoPapelImune>();
        private List<dadosTributosIncidentesProdutoServico> listaTributosIncidentesProdutoServico = new List<dadosTributosIncidentesProdutoServico>();
        private List<dadosICMSNormalST> listaICMSNormalST = new List<dadosICMSNormalST>();
        private List<dadosImpostoProdutosIndustrializados> listaImpostoProdutosIndustrializados = new List<dadosImpostoProdutosIndustrializados>();
        private List<dadosImpostoImportacao> listaImpostoImportacao = new List<dadosImpostoImportacao>();
        private List<dadosPis> listaPis = new List<dadosPis>();
        private List<dadosPisST> listaPisST = new List<dadosPisST>();
        private List<dadosCofins> listaCofins = new List<dadosCofins>();
        private List<dadosCofinsST> listaCofinsST = new List<dadosCofinsST>();
        private List<dadosISSQN> listaISSQN = new List<dadosISSQN>();
        private List<dadosTributosDevolvidos> listaTributosDevolvidos = new List<dadosTributosDevolvidos>();
        private List<dadosInformacoesAdicionais> listaInformacoesAdicionais = new List<dadosInformacoesAdicionais>();
        private List<dadosTotalNFe> listaTotalNFe = new List<dadosTotalNFe>();
        private List<dadosTotalNFeISSQN> listaTotalNFeISSQN = new List<dadosTotalNFeISSQN>();
        private List<dadosTotalNFeRetencaoTributos> listaTotalNFeRetencaoTributos = new List<dadosTotalNFeRetencaoTributos>();
        private List<dadosInformacoesTransporteNFe> listaInformacoesTransporteNFe = new List<dadosInformacoesTransporteNFe>();
        private List<dadosDadosCobranca> listaDadosCobranca = new List<dadosDadosCobranca>();
        private List<dadosFormasPagamento> listaFormasPagamento = new List<dadosFormasPagamento>();
        private List<dadosInformacoesAdicionaisNFe> listaInformacoesAdicionaisNFe = new List<dadosInformacoesAdicionaisNFe>();
        private List<dadosInformacoesComercioExterior> listaInformacoesComercioExterior = new List<dadosInformacoesComercioExterior>();
        private List<dadosInformacoesCompras> listaInformacoesCompras = new List<dadosInformacoesCompras>();
        private List<dadosInformacoesRegistroAquisicaoCana> listaInformacoesRegistroAquisicaoCana =new List<dadosInformacoesRegistroAquisicaoCana>();
        private List<dadosAssinatura> listaAssinatura = new List<dadosAssinatura>();


        private dadosIdentificacaoNfe identificacaoNfe = new dadosIdentificacaoNfe(null,
null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);

        private dadosDocumentoFiscalDiferenciado documentoFiscalDiferenciado = new dadosDocumentoFiscalDiferenciado(null, null, null,
        null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);

        private dadosIdentificacaoEmitenteNfe identificacaoEmitenteNfe = new dadosIdentificacaoEmitenteNfe(null, null, null, null, null,
        null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);

        private dadosIdentificacaoFiscoEmitenteNfe identificacaoFiscoEmitenteNfe = new dadosIdentificacaoFiscoEmitenteNfe(null, null,
        null, null, null, null, null, null, null, null, null);

        private dadosIdentificacaoDestinatárioNfe identificacaoDestinatarioNfe = new dadosIdentificacaoDestinatárioNfe(null, null, null, null,
        null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);

        private dadosIdentificacaoLocalRetirada identificacaoLocalRetirada = new dadosIdentificacaoLocalRetirada(null, null, null,
        null, null, null, null, null, null);

        private dadosIdentificacaoLocalEntrega identificacaoLocalEntrega = new dadosIdentificacaoLocalEntrega(null, null, null,
        null, null, null, null, null, null);

        private dadosAutorizacaoObterXml autorizacaoObterXml = new dadosAutorizacaoObterXml(null, null, null);

        private dadosDetalhamentoProdutosNfe detalhamentoProdutosNfe = new dadosDetalhamentoProdutosNfe(null, null);

        private dadosProdutosServicosNfe produtosServicosNfe = new dadosProdutosServicosNfe(null, null, null, null, null, null, null,
        null, null, null, null, null, null, null, null, null, null, null, null, null);

        private dadosProdutosServicosDeclaracaoImportacao produtosServicosDeclaracaoImportacao = new dadosProdutosServicosDeclaracaoImportacao(null,
        null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);

        private dadosProdutosServicosGrupoExportacao produtosServicosGrupoExportacao = new dadosProdutosServicosGrupoExportacao(null, null,
        null, null, null);

        private dadosProdutosServicosPedidoCompra produtosServicosPedidoCompra = new dadosProdutosServicosPedidoCompra(null, null);

        private dadosProdutosServicosGrupoDiversos produtosServicosGrupoDiversos = new dadosProdutosServicosGrupoDiversos(null);

        private dadosDetalhamentoEspecificoVeiculosNovos detalhamentoEspecificoVeiculosNovos = new dadosDetalhamentoEspecificoVeiculosNovos(null, null,
        null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);

        private dadosDetalhamentoEspecificoMedicamentoMateriasPrimasFarmaceuticas detalhamentoEspecificoMedicamentoMateriasPrimasFarmaceuticas = new dadosDetalhamentoEspecificoMedicamentoMateriasPrimasFarmaceuticas(
        null, null, null, null, null, null);

        private dadosDetalhamentoEspecificoArmamentos detalhamentoEspecificoArmamentos = new dadosDetalhamentoEspecificoArmamentos(null,
        null, null, null, null);

        private dadosDetalhamentoEspecificoCombustiveis detalhamentoEspecificoCombustiveis = new dadosDetalhamentoEspecificoCombustiveis(
        null, null, null, null, null, null, null, null);

        private dadosDetalhamentoEspecificoOperacaoPapelImune detalhamentoEspecificoOperacaoPapelImune = new dadosDetalhamentoEspecificoOperacaoPapelImune(null);

        private dadosTributosIncidentesProdutoServico tributosIncidentesProdutoServico = new dadosTributosIncidentesProdutoServico(null);

        private dadosICMSNormalST iCMSNormalST = new dadosICMSNormalST(null, null, null, null, null, null, null, null, null, null, null, null,
        null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,
        null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,
        null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,
        null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,
        null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,
        null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);

        private dadosImpostoProdutosIndustrializados impostoProdutosIndustrializados = new dadosImpostoProdutosIndustrializados(null, null,
        null, null, null, null, null, null, null, null, null);

        private dadosImpostoImportacao impostoImportacao = new dadosImpostoImportacao(null, null, null, null);

        private dadosPis pis = new dadosPis(null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);

        private dadosPisST pisST = new dadosPisST(null, null, null, null, null);

        private dadosCofins cofins = new dadosCofins(null, null, null, null, null, null, null, null, null, null, null, null,
        null, null, null);

        private dadosCofinsST cofinsST = new dadosCofinsST(null, null, null, null, null);

        private dadosISSQN iSSQN = new dadosISSQN(null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);

        private dadosTributosDevolvidos tributosDevolvidos = new dadosTributosDevolvidos(null, null, null);

        private dadosInformacoesAdicionais informacoesAdicionais = new dadosInformacoesAdicionais(null);

        private dadosTotalNFe totalNFe = new dadosTotalNFe(null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);

        private dadosTotalNFeISSQN TotalNFeISSQN = new dadosTotalNFeISSQN(null, null, null, null, null, null, null, null, null, null, null, null);

        private dadosTotalNFeRetencaoTributos totalNFeRetencaoTributos = new dadosTotalNFeRetencaoTributos(null, null,
        null, null, null, null, null);

        private dadosInformacoesTransporteNFe informacoesTransporteNFe = new dadosInformacoesTransporteNFe(null, null, null, null, null,
        null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,
        null, null, null, null, null, null);

        private dadosDadosCobranca dadosCobranca = new dadosDadosCobranca(null, null, null, null, null, null, null, null);

        dadosFormasPagamento FormasPagamento = new dadosFormasPagamento(null, null, null, null, null, null);

        private dadosInformacoesAdicionaisNFe informacoesAdicionaisNFe = new dadosInformacoesAdicionaisNFe(null, null, null, null,
        null, null, null, null, null);

        private dadosInformacoesComercioExterior informacoesComercioExterior = new dadosInformacoesComercioExterior(null, null, null);

        private dadosInformacoesCompras informacoesCompras = new dadosInformacoesCompras(null, null, null);

        private dadosInformacoesRegistroAquisicaoCana informacoesRegistroAquisicaoCana = new dadosInformacoesRegistroAquisicaoCana(null, null,
        null, null, null, null, null, null, null, null, null, null, null);

        private dadosAssinatura assinatura = new dadosAssinatura(null, null, null, null);

        private dadosEnvioNfeLote envioNfeLote = new dadosEnvioNfeLote(null);


        //############End
        //#################################################### Procedimentos de Banco ####################################################
        public void conector_find_entrada(string findNota, string find)
        {
            try
            {
                auxConsistencia = 0;
                banco.abreConexao();
                banco.startTransaction("conector_find_entrada");
                banco.addParametro("tipo","4");
                banco.addParametro("findStatus", "");
                banco.addParametro("findId", "");
                banco.addParametro("findCliente", "");
                banco.addParametro("findLoja", auxIdLoja);
                banco.addParametro("findNota", findNota);
                banco.addParametro("findCfop", find);
                banco.addParametro("di", "");
                banco.addParametro("df", "");
                banco.procedimentoRead();
                if (banco.retornaRead().Read() == true)
                {
                    lbmsgRetornoEntradaNota.Text = banco.retornaRead().GetString(7);
                }
            }
            catch (Exception erro)
            { MessageBox.Show(erro.Message, "Caro cliente"); auxConsistencia = 1; }
            finally
            {
                banco.fechaConexao();
            }
        }
        public void conector_carrega_piscofins(string id)
        {
            auxConsistencia = 0;
            string test = "0"; // retorno 1 - verdadeiro 0 - falso
            string auxPreencheCombo = "";
            try
            {
                banco.abreConexao();
                banco.startTransaction("conector_find_pisCofins");
                banco.addParametro("tipo", "9");
                banco.addParametro("find", id);
                banco.procedimentoRead();
                if (banco.retornaRead().Read() == true)
                {
                    auxCstPisProduto = banco.retornaRead().GetString(20);
                    auxCstCofinsProduto = banco.retornaRead().GetString(21);
                    optionProdutoPisSt = banco.retornaRead().GetString(3);
                    optionProdutoPisCofinsSt = banco.retornaRead().GetString(2);
                    //newInativaPisCofins = banco.retornaRead().GetString(0);
                    test = "1";
                }
            }
            catch (Exception erro)
            { MessageBox.Show(erro.Message, "Caro Usuário"); auxConsistencia = 1; }
            finally
            {

                banco.fechaConexao();
                if (auxConsistencia == 0)
                {
                    if (test == "1")
                    {
                        if (auxCstPisProduto != "")
                        {
                            try
                            {
                                auxConsistencia = 0;
                                banco.abreConexao();
                                banco.startTransaction("conector_find_complementoFiscal");
                                banco.addParametro("tipo", "6");
                                banco.addParametro("find", auxCstPisProduto);
                                banco.procedimentoRead();
                                if (banco.retornaRead().Read() == true)
                                {
                                    auxPreencheCombo = banco.retornaRead().GetString(2);
                                }
                            }
                            catch (Exception erro)
                            { MessageBox.Show(erro.Message, "Caro Usuário"); auxConsistencia = 1; }
                            finally
                            {
                                banco.fechaConexao();
                                if (auxConsistencia == 0)
                                {
                                    cmbSituacaoTributariaPISItensNotaFiscal.Text = auxPreencheCombo;
                                    optionCstPisEmissaoPisCofinsProduto = auxPreencheCombo;
                                    auxPreencheCombo = "";   
                                }
                            }
                        }
                        if (auxCstCofinsProduto != "")
                        {
                            try
                            {
                                auxConsistencia = 0;
                                banco.abreConexao();
                                banco.startTransaction("conector_find_complementoFiscal");
                                banco.addParametro("tipo", "9");
                                banco.addParametro("find", auxCstCofinsProduto);
                                banco.procedimentoRead();
                                if (banco.retornaRead().Read() == true)
                                {
                                    auxPreencheCombo = banco.retornaRead().GetString(2);
                                }
                            }
                            catch (Exception erro)
                            { MessageBox.Show(erro.Message, "Caro Usuário"); auxConsistencia = 1; }
                            finally
                            {

                                banco.fechaConexao();
                                if (auxConsistencia == 0)
                                {
                                    cmbSituacaoTributariaCofinsItensNotaFiscal.Text = auxPreencheCombo;
                                    optinoCstCofinsEmissaoPisCofinsProduto = auxPreencheCombo;
                                    auxPreencheCombo = "";   
                                }
                            }
                        }
                    }
                }
            }
        }
        private void conector_find_produto(string produto) //Pesquisa e inseri o produto no automatico. "conector_inc_NFITEM"
        {
            string auxPreencheCombo;
            int test; 
            auxPreencheCombo = "";
            auxConsistencia = 0;
            test = 0; //0 fechar 1 libera
            try
            {
                banco.abreConexao();
                banco.startTransaction("conector_find_produto");
                banco.addParametro("tipo", "3");
                banco.addParametro("find", produto);
                banco.addParametro("findIdLoja", auxIdLoja);
                banco.addParametro("findIdSetor", "0");
                banco.addParametro("findIdGrupo", "0");
                banco.addParametro("findIdCategoria", "0");
                banco.addParametro("findIdFornecedor", "0");
                banco.addParametro("findNumeroNota", "0");
                banco.addParametro("findDataInicial", "19000101");
                banco.addParametro("findDataFinal", "19000101");
                banco.procedimentoRead();
                if (banco.retornaRead().Read() == true)
                {
                    auxIdProduto = banco.retornaRead().GetString(0);
                    optionProdutoValorLiquido = banco.retornaRead().GetString(25);
                    optionProdutoDescricao = banco.retornaRead().GetString(1);
                    optionProdutoFornecedor = banco.retornaRead().GetString(21);
                    optionProdutopriceOriginal = banco.retornaRead().GetString(62);
                    optionProdutopriceVenda = banco.retornaRead().GetString(25);
                    optionProdutopriceCusto = banco.retornaRead().GetString(40);
                    optionProdutoestoque = banco.retornaRead().GetString(19);
                    optionProdutoidunidadeMedida = banco.retornaRead().GetString(17);
                    optionProdutoipi = banco.retornaRead().GetString(98);
                    optionProdutoipiValor = banco.retornaRead().GetString(69);
                    optionProdutoNCM = banco.retornaRead().GetString(70);
                    optionProdutocstIcms = banco.retornaRead().GetString(65);
                    optionProdutoMVA = banco.retornaRead().GetString(90);
                    optionProdutoMVAInterestadual = banco.retornaRead().GetString(91);
                    optionProdutoMVAInterna = banco.retornaRead().GetString(92);
                    optionProdutoMVAAjusteMva = banco.retornaRead().GetString(93);
                    optionProdutoCodigoAliquota = banco.retornaRead().GetString(75);
                    optionProdutoIDPisCofins = banco.retornaRead().GetString(73);
                    optionProdutocstCalcBc = banco.retornaRead().GetString(94);
                    auxUnItemNotaFiscal = banco.retornaRead().GetString(18);
                    optionProdutoIdSetor = banco.retornaRead().GetString(22);
                    optionProdutocstCalcRed = banco.retornaRead().GetString(95);
                    optionProdutocsttypeCst = banco.retornaRead().GetString(96);
                    optionProdutocstModalidade = banco.retornaRead().GetString(97);
                    auxIdGenero = banco.retornaRead().GetString(79);
                    test = 1;
                }
                else
                {
                    MessageBox.Show("Produto inválido, ou não existe no cadastro! ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    zeraObjItem();
                    auxConsistencia = 1;
                    optionProdutoDescricao = "";
                    txtCodigoItensNotaFiscal.ReadOnly = false;
                    txtCodigoItensNotaFiscal.Select();
                }
            }
            catch (Exception erro)
            { MessageBox.Show(erro.Message, "Caro Usuario"); auxConsistencia = 1; }
            finally
            {
                banco.fechaConexao();
                if (auxConsistencia == 0)
                {
                    if (test == 1) //recebeu o devido retorno
                    {
                        txtCodigoItensNotaFiscal.ReadOnly = true;
                    }
                    if (optionProdutopriceVenda != "" && optionProdutopriceVenda != "0.00" && Convert.ToDecimal(optionProdutopriceVenda) > 0)
                    {


                        if (optionProdutoDescricao != "")
                        {
                            //####################################Carregando Paramentro do PIS/COFINS###################################
                            if (optionProdutoIDPisCofins != "" && Convert.ToDouble(optionProdutoIDPisCofins) > 0)
                            {
                                conector_carrega_piscofins(optionProdutoIDPisCofins);
                            }
                            //################################################END PIS/COFINS############################################
                            //####################################Carregando Paramentro da Aliquota#####################################
                            if (optionProdutoCodigoAliquota != "" || optionProdutoCodigoAliquota != "0")
                            {
                                try
                                {
                                    auxConsistencia = 0;
                                    banco.abreConexao();
                                    banco.startTransaction("conector_find_aliquota");
                                    banco.addParametro("tipo", "1");
                                    banco.addParametro("find", optionProdutoCodigoAliquota);
                                    banco.procedimentoRead();
                                    if (banco.retornaRead().Read() == true)
                                    {
                                        optionProdutoaliquota = banco.retornaRead().GetString(2);
                                        optionProdutoreducao = banco.retornaRead().GetString(3);
                                        optionProdutoTypeAliquota = banco.retornaRead().GetString(4);
                                        test = 1;
                                    }
                                }
                                catch (Exception erro)
                                { MessageBox.Show(erro.Message, "Caro Usuário"); auxConsistencia = 1; }
                                finally
                                {
                                    banco.fechaConexao();
                                    if (auxConsistencia == 0)
                                    {
                                        if (test == 1)
                                        {
                                            txtAliquotaIcmsItensNotaFiscal.Text = optionProdutoaliquota;
                                            txtReducaoBaseCalculoItensNotaFiscal.Text = optionProdutoreducao;
                                            if (Convert.ToDecimal(optionProdutoreducao) != 0 && optionProdutoreducao != "" && optionProdutoreducao != "0.00")
                                            {
                                                //Convert a aliquota de saida do produto
                                                optionProdutoaliquota = calculoImpostos.convertAliquotaReduzida(Convert.ToDecimal(optionProdutoaliquota), Convert.ToDecimal(optionProdutoreducao)).ToString();
                                                //aliquota de saida

                                            }
                                        } test = 0;
                                    }
                                }
                            }
                            //####################################End Paramentro da Aliquota#####################################
                            //#############################Carregando situação da Classificação tributaria##########################
                            if (optionProdutocstIcms != "")
                            {
                                try
                                {
                                    auxConsistencia = 0;
                                    banco.abreConexao();
                                    banco.startTransaction("conector_find_cst");
                                    banco.addParametro("tipo", "1");
                                    banco.addParametro("find", optionProdutocstIcms);
                                    banco.procedimentoSet();
                                }
                                catch (Exception erro)
                                { MessageBox.Show(erro.Message, "Messagem do Sistema"); auxConsistencia = 1; }
                                finally
                                {
                                    if (auxConsistencia == 0)
                                    {
                                        auxPreencheCombo = Convert.ToString(banco.retornaSet().Tables[0].Rows[0][2]);   
                                    }
                                    banco.fechaConexao();
                                    if (auxConsistencia == 0)
                                    {
                                        cmbTributoSituacaoTributariaItensNotaFiscal.Text = auxPreencheCombo;
                                    }
                                }
                            }

                            //#############################End CST ICMS#############################################################
                            //####################################CFOP por sugestão aplicada sobre a natureza da operação #######
                            switch (auxEstoqueOrigem)
                            {
                                case "s":
                                    if (optionProdutocstIcms == "060" || optionProdutocstIcms == "070" || optionProdutocstIcms == "010") //"significa CFOP por sugestao sobre ST"
                                    {
                                        switch (optionFlagParamentro)
                                        {
                                            case "v":
                                                if (optionClienteIdTipoFornecedor != "1")
                                                {
                                                    if (lbEmitenteUfNotaFiscal.Text == lbDestinatarioUFNotaFiscal.Text) //Mesmo estado, ou seja, dentro do estado
                                                    {
                                                        txtCFOPNotaFiscal.Text = txtCFOPItensNotaFiscal.Text = "5403"; //Sugestão para o estado do cliente/fornecedor dentro
                                                    }
                                                    else
                                                    {
                                                        txtCFOPNotaFiscal.Text = txtCFOPItensNotaFiscal.Text = "6403"; //Sugestão para o estado do cliente/fornecedor fora
                                                    }
                                                }
                                                else
                                                {
                                                     MessageBox.Show("Consulte o codigo fiscal de operação [CFOP], pois a sua sugestação não foi aceita, o fornecedor com a situação de industria pode ter incentivos. Agradeço pela atenção.", "Caro Usúario - Orientação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                     txtCFOPItensNotaFiscal.Select();
                                                }
                                                break;
                                            default:
                                                if (lbEmitenteUfNotaFiscal.Text == lbDestinatarioUFNotaFiscal.Text) //Mesmo estado, ou seja, dentro do estado
                                                {
                                                    txtCFOPNotaFiscal.Text =  txtCFOPItensNotaFiscal.Text = optioncfop3; //Sugestão para o estado do cliente/fornecedor dentro
                                                }
                                                else
                                                {
                                                    txtCFOPNotaFiscal.Text = txtCFOPItensNotaFiscal.Text = optioncfop4; //Sugestão para o estado do cliente/fornecedor fora
                                                }
                                                break;
                                        }
                                    }
                                    else //"significa CFOP por sugestao normal 000 e respectivas reduções como 020 "
                                    {
                                        if (lbEmitenteUfNotaFiscal.Text == lbDestinatarioUFNotaFiscal.Text) //Mesmo estado, ou seja, dentro do estado
                                        {
                                            txtCFOPNotaFiscal.Text =  txtCFOPItensNotaFiscal.Text = optioncfop3; //Sugestão para o estado do cliente/fornecedor dentro
                                        }
                                        else
                                        {
                                            txtCFOPNotaFiscal.Text = txtCFOPItensNotaFiscal.Text = optioncfop4; //Sugestão para o estado do cliente/fornecedor fora
                                        }
                                    }
                                    break;
                                case "e":
                                    if (optionProdutocstIcms == "060" || optionProdutocstIcms == "070" || optionProdutocstIcms == "010") //"significa CFOP por sugestao sobre ST"
                                    {
                                        switch (optionFlagParamentro)
                                        {
                                            case "v":
                                                if (optionClienteIdTipoFornecedor != "1")
                                                {
                                                    if (lbEmitenteUfNotaFiscal.Text == lbDestinatarioUFNotaFiscal.Text) //Mesmo estado, ou seja, dentro do estado
                                                    {
                                                        txtCFOPNotaFiscal.Text = txtCFOPItensNotaFiscal.Text = "1403"; //Sugestão para o estado do cliente/fornecedor dentro
                                                    }
                                                    else
                                                    {
                                                        txtCFOPNotaFiscal.Text = txtCFOPItensNotaFiscal.Text = "2403"; //Sugestão para o estado do cliente/fornecedor fora
                                                    }
                                                }
                                                else
                                                {
                                                    MessageBox.Show("Consulte o codigo fiscal de operação [CFOP], pois a sua sugestação não foi aceita, o fornecedor com a situação de industria pode ter incentivos. Agradeço pela atenção.", "Caro Usúario - Orientação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                    txtCFOPItensNotaFiscal.Select();
                                                }
                                                break;
                                            default:
                                                if (lbEmitenteUfNotaFiscal.Text == lbDestinatarioUFNotaFiscal.Text) //Mesmo estado, ou seja, dentro do estado
                                                {
                                                    txtCFOPNotaFiscal.Text = txtCFOPItensNotaFiscal.Text = optioncfop1; //Sugestão para o estado do cliente/fornecedor dentro
                                                }
                                                else
                                                {
                                                    txtCFOPNotaFiscal.Text = txtCFOPItensNotaFiscal.Text = optioncfop2; //Sugestão para o estado do cliente/fornecedor fora
                                                }
                                                break;
                                        }
                                    }
                                    else //"significa CFOP por sugestao normal 000 e respectivas reduções como 020 "
                                    {
                                        if (lbEmitenteUfNotaFiscal.Text == lbDestinatarioUFNotaFiscal.Text) //Mesmo estado, ou seja, dentro do estado
                                        {
                                            txtCFOPItensNotaFiscal.Text = optioncfop1; //Sugestão para o estado do cliente/fornecedor dentro
                                        }
                                        else
                                        {
                                            txtCFOPItensNotaFiscal.Text = optioncfop2; //Sugestão para o estado do cliente/fornecedor fora
                                        }
                                    }
                                    break;
                                default:
                                    break;
                            }
                            //####################################END CFOP por sugestao##########################################
                            //####################################Carregando Paramentro do PIS/COFINS###############################
                            try
                            {
                                auxConsistencia = 0;
                                banco.abreConexao();
                                banco.startTransaction("conector_find_piscofins");
                                banco.addParametro("tipo", "9");
                                banco.addParametro("find", optionProdutoIDPisCofins);
                                banco.procedimentoRead();
                                if (banco.retornaRead().Read() == true)
                                {
                                    optionProdutocstPis = banco.retornaRead().GetString(20);
                                    optionProdutocstCofins = banco.retornaRead().GetString(21);
                                }
                            }
                            catch (Exception erro)
                            { MessageBox.Show(erro.Message, "Caro Usuário"); auxConsistencia = 1; }
                            finally
                            {
                                banco.fechaConexao();
                            }
                            //####################################End Paramentro do PIS/COFINS######################################
                            //####################################Carregando o controle de variaveis################################
                            txtDescricaoItensNotaFiscal.Text = optionProdutoDescricao;
                            txtCodigoNCMItensNotaFiscal.Text = optionProdutoNCM;
                            txtValorUnComercialTributarioItensNotaFiscal.Text = optionProdutopriceVenda;
                            txtCFOPItensNotaFiscal.Select();
                            //####################################END controle################################
                            //####################################Unidade tributado default receita#####################################
                            cmbUnidadeDefaultItensNotaFiscal.Text = auxUnItemNotaFiscal;
                            //####################################End Unidade###########################################################
                        }
                    }
                    else
                    {
                        MessageBox.Show("Produto com preço zerado, ou inválido, favor revisar a precificação!", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        zeraObjItem();
                        txtCodigoItensNotaFiscal.Select();
                        txtCodigoItensNotaFiscal.ReadOnly = false;
                    }
                }
            }
        }
        public void conector_find_cst()
        {
            cmbTributoSituacaoTributariaItensNotaFiscal.Items.Clear();
            auxConsistencia = 0;
            try
            {
                banco.abreConexao();
                banco.startTransaction("conector_find_cst");
                banco.addParametro("tipo", "4");
                banco.addParametro("find", "0");
                banco.procedimentoSet();
            }
            catch (Exception erro)
            { MessageBox.Show(erro.Message, "Caro Usuário"); auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = banco.retornaSet().Tables[0].Columns.Count;
                    countRows = banco.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        for (i = 0; i < countRows; i++)
                        {
                            cmbTributoSituacaoTributariaItensNotaFiscal.Items.Add(banco.retornaSet().Tables[0].Rows[i][2]);
                        }
                    }
                }
                banco.fechaConexao();
            }
        }
        public void conector_find_un()
        {
            cmbUnidadeDefaultItensNotaFiscal.Items.Clear();
            auxConsistencia = 0;
            try
            {
                banco.abreConexao();
                banco.startTransaction("conector_find_un");
                banco.addParametro("tipo", "0");
                banco.addParametro("find", "0");
                banco.procedimentoSet();
            }
            catch (Exception erro)
            { MessageBox.Show(erro.Message, "Erro não identificado, entre contato como revendedor"); auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = banco.retornaSet().Tables[0].Columns.Count;
                    countRows = banco.retornaSet().Tables[0].DefaultView.Count;

                    for (i = 0; i < countRows; i++)
                    {
                        cmbUnidadeDefaultItensNotaFiscal.Items.Add(banco.retornaSet().Tables[0].Rows[i][2]);
                    }
                }
                banco.fechaConexao();
            }
        }
        public void conector_find_complementoFiscal(string tipo, string find)
        {
            auxConsistencia = 0;
            try
            {
                banco.abreConexao();
                banco.startTransaction("conector_find_complementoFiscal");
                banco.addParametro("tipo", tipo);
                banco.addParametro("find", find);
                banco.procedimentoSet();
            }
            catch (Exception erro)
            { MessageBox.Show(erro.Message, "Caro Usuário"); auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = banco.retornaSet().Tables[0].Columns.Count;
                    countRows = banco.retornaSet().Tables[0].DefaultView.Count;

                    for (i = 0; i < countRows; i++)
                    {
                        switch (tipo)
                        {
                            case "1":
                                cmbSituacaoTributariaIPIItensNotaFiscal.Items.Add(banco.retornaSet().Tables[0].Rows[i][2]);
                                break;
                            case "4":
                                cmbSituacaoTributariaPISItensNotaFiscal.Items.Add(banco.retornaSet().Tables[0].Rows[i][2]);
                                break;
                            case "7":
                                cmbSituacaoTributariaCofinsItensNotaFiscal.Items.Add(banco.retornaSet().Tables[0].Rows[i][2]);
                                break;
                        }
                    }
                }
                banco.fechaConexao();
            }
        }
        public void preenche_combo_paramentroFaturamento()
        {
            cmbNaturezaOperacaoNotaFiscal.Items.Clear();
            auxConsistencia = 0;
            try
            {
                banco.abreConexao();
                banco.startTransaction("conector_find_paramentroFaturamento");
                banco.addParametro("tipo", "1");
                banco.addParametro("find", "@");
                banco.procedimentoSet();
            }
            catch (Exception erro)
            { MessageBox.Show(erro.Message, "Erro não identificado, entre contato como revendedor"); auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {


                    countField = banco.retornaSet().Tables[0].Columns.Count;
                    countRows = banco.retornaSet().Tables[0].DefaultView.Count;

                    for (i = 0; i < countRows; i++)
                    {
                        cmbNaturezaOperacaoNotaFiscal.Items.Add(banco.retornaSet().Tables[0].Rows[i][1]);
                    }
                }
                banco.fechaConexao();
            }
            cmbNaturezaOperacaoNotaFiscal.SelectedIndex = -1;
        }
        public int conector_verifica_dataFrete(string id)
        {

            try
            {
                auxConsistencia = 0;
                banco.abreConexao();
                banco.singleTransaction("select count(*) from nfInforTransporte where  nf = ?nf");
                banco.addParametro("?nf", id);
                banco.procedimentoRead();
                if (banco.retornaRead().Read() == true)
                {
                    retorno = Convert.ToInt32(banco.retornaRead().GetString(0));
                }
                else retorno = 0;
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message, "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information); auxConsistencia = 1;
            }
            finally
            {
                banco.fechaConexao();
            }

            return retorno;
        }
        public int conector_verifica_typeCliente(string id)
        {
            auxConsistencia = 0;
            try
            {
                banco.abreConexao();
                banco.singleTransaction("select idTipoPessoa from cliente where  idCliente = ?id");
                banco.addParametro("?id", id);
                banco.procedimentoRead();
                if (banco.retornaRead().Read() == true)
                {
                    retorno = Convert.ToInt32(banco.retornaRead().GetString(0));
                }
                else retorno = 0;
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message, "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information); auxConsistencia = 1;
            }
            finally
            {
                banco.fechaConexao();
            }

            return retorno;
        }
        public int conector_verifica_atividade(string id)
        {
            auxConsistencia = 0;
            try
            {
                banco.abreConexao();
                banco.singleTransaction("select idAtividade from cliente where  idCliente = ?id");
                banco.addParametro("?id", id);
                banco.procedimentoRead();
                if (banco.retornaRead().Read() == true)
                {
                    retorno = Convert.ToInt32(banco.retornaRead().GetString(0));
                }
                else retorno = 0;
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message, "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information); auxConsistencia = 1;
            }
            finally
            {
                banco.fechaConexao();
            }

            return retorno;
        }
        public int conector_verifica(string produto)
        {
            auxConsistencia = 0;
            try
            {
                banco.abreConexao();
                banco.singleTransaction("select count(idProduto) as repeti from nfItem where (idProduto=?produto or 0=?produto) and idNf=?nf");
                banco.addParametro("?produto", produto);
                banco.addParametro("?nf", auxIdNf);
                banco.procedimentoRead();
                if (banco.retornaRead().Read() == true)
                {
                    retorno = Convert.ToInt32(banco.retornaRead().GetString(0));
                }
                else retorno = 0;
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message, "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information); auxConsistencia = 1;
            }
            finally
            {
                banco.fechaConexao();
            }

            return retorno;
        }
        public int conector_full_verificaItem()
        {

            try
            {
                banco.abreConexao();
                banco.singleTransaction("select count(*) as repeti from nfItem where idNf=?nf");
                banco.addParametro("?nf", auxIdNf);
                banco.procedimentoRead();
                if (banco.retornaRead().Read() == true)
                {
                    retorno = Convert.ToInt32(banco.retornaRead().GetString(0));
                }
                else retorno = 0;
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message, "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                banco.fechaConexao();
            }

            return retorno;
        }
        public void conector_carrega_paramentroFaturamento(string id)
        {
            auxConsistencia = 0;
            try
            {
                banco.abreConexao();
                banco.startTransaction("conector_find_paramentroFaturamento");
                banco.addParametro("tipo", "2");
                banco.addParametro("find", id);
                banco.procedimentoRead();
                if (banco.retornaRead().Read() == true)
                {
                    optionDescricaoFaturamento = banco.retornaRead().GetString(0);
                    optionFlagParamentro = banco.retornaRead().GetString(4);//v - venda; t - transferencia; r - troca; d - devolução; c - consumo; p - perda; q -  quebra; e - entrada; f - franquia; s - simples faturamento; n - conserto
                    optionStatusFaturamento = banco.retornaRead().GetString(1);
                    optionForceVendedorFaturamento = banco.retornaRead().GetString(5);
                    optionForceClienteFaturamento = banco.retornaRead().GetString(6);
                    optionForceMetodoFaturamento = banco.retornaRead().GetString(7);
                    optionForcetranspoteFaturamento = banco.retornaRead().GetString(8);
                    optionForceAtualizacaoFaturamento = banco.retornaRead().GetString(9);
                    optionForcePagamentoFaturamento = banco.retornaRead().GetString(10);
                    optionforceAdressEntregaFaturamento = banco.retornaRead().GetString(11);
                    auxEstoqueDestino = banco.retornaRead().GetString(12);
                    auxEstoqueOrigem = banco.retornaRead().GetString(13);
                    optionEstoqueNegativoBloqueiaFaturamento = banco.retornaRead().GetString(14);
                    optionFixaOrigemFaturamento = banco.retornaRead().GetString(15);
                    optionFixaDestinoFaturamento = banco.retornaRead().GetString(16);
                    optionIdOrigemFixaFaturamento = banco.retornaRead().GetString(17);
                    optionIddestinoFixoFaturamento = banco.retornaRead().GetString(18);
                    //             optionDescDestinoFixoFaturamento = banco.retornaRead().GetString();
                    //             optionDescOrigemFixaFaturamento = banco.retornaRead().GetString();
                    optionRestrigeClienteFaturamento = banco.retornaRead().GetString(19);
                    optionCustoLiquidoFaturamento = banco.retornaRead().GetString(20);
                    optionEmissaoFixaFaturamento = banco.retornaRead().GetString(21);
                    optionEstoqueLojaFaturamento = banco.retornaRead().GetString(22);
                    optionAtualizaComNfFaturamento = banco.retornaRead().GetString(23);
                    optionOrigemDestinoIguaisFaturamento = banco.retornaRead().GetString(24);
                    optionExclusaoPedidosFaturamento = banco.retornaRead().GetString(25);
                    optionLiberaDescontoFaturamento = banco.retornaRead().GetString(26);
                    optiondigitaDescontoValorFaturamento = banco.retornaRead().GetString(27);
                    optionDigitaDescontoPercentualFaturamento = banco.retornaRead().GetString(28);
                    optionFlagExpiracaoFaturamento = banco.retornaRead().GetString(29);
                    optionDiasEspiracaoFaturamento = banco.retornaRead().GetString(30);
                    optionflagPermiteAtualizarFaturamento = banco.retornaRead().GetString(31);
                    optionflagAlteraLojaFaturamento = banco.retornaRead().GetString(32);
                    optionModuloVendedorFaturamento = banco.retornaRead().GetString(34);
                    optionModuloGerenteFaturamento = banco.retornaRead().GetString(35);
                    optionModuloDiretorFaturamento = banco.retornaRead().GetString(36);
                    optionModuloSupervisorFaturamento = banco.retornaRead().GetString(37);
                    auxEstoqueDestinoKit = banco.retornaRead().GetString(38);
                    auxEstoqueOrigemKit = banco.retornaRead().GetString(39);
                    optionflagDetalhesItemFaturamento = banco.retornaRead().GetString(40);
                    optionflagEstoqueFuturaFaturamento = banco.retornaRead().GetString(41);
                    optionRestrigeFinanceiroFaturamento = banco.retornaRead().GetString(43);
                    optionRestrigeAltpgtoFaturamento = banco.retornaRead().GetString(44);
                    optionLimiteCreditoFaturamento = banco.retornaRead().GetString(45);
                    optionCarenciaAprazoFaturamento = banco.retornaRead().GetString(46);
                    optionFlagTrocoFaturamento = banco.retornaRead().GetString(47);
                    optionGeraReceberFaturamento = banco.retornaRead().GetString(48);
                    optionGeraCrediarioFaturamento = banco.retornaRead().GetString(49);
                    optionGeraPargarDestinoFaturamento = banco.retornaRead().GetString(50);
                    optionGeraPargarOrigemFaturamento = banco.retornaRead().GetString(51);
                    optionGeraChequeFaturamento = banco.retornaRead().GetString(52);
                    optionFlagAlteraPrecoFaturamento = banco.retornaRead().GetString(53);
                    optionFlagAlteraComissaoFaturamento = banco.retornaRead().GetString(54);
                    optionFlagAlteraDescontoFaturamento = banco.retornaRead().GetString(55);
                    optionFlagAlteraIpiFaturamento = banco.retornaRead().GetString(56);
                    optionFlagAlteraVendedorFaturamento = banco.retornaRead().GetString(57);
                    optionFlagQttyZeradaFaturamento = banco.retornaRead().GetString(58);
                    optionFlagItemZeradaFaturamento = banco.retornaRead().GetString(59);
                    optionFlagEntradaFaturamento = banco.retornaRead().GetString(60);
                    optionFlagExplodeKitFaturamento = banco.retornaRead().GetString(61);
                    optionFlagMaxItemPedidoFaturamento = banco.retornaRead().GetString(62);
                    optionNumeroMaxItemFaturamento = banco.retornaRead().GetString(63);
                    optionFlagRepetItemFaturamento = banco.retornaRead().GetString(64);
                    optionFlagAltExcIncItemFaturamento = banco.retornaRead().GetString(65);
                    optionFlagBloquearItemFaturamento = banco.retornaRead().GetString(66);
                    auxFlagEanDefault = banco.retornaRead().GetString(67);
                    optionBloqueiaQuantidadeFaturamento = banco.retornaRead().GetString(68);
                    auxFlagRestrigeCliente = banco.retornaRead().GetString(69);
                    optionFlagClienteAvistaFaturamento = banco.retornaRead().GetString(70);
                    optionFlagClienteDocCorretosFaturamento = banco.retornaRead().GetString(71);
                    optionFlagClienteEmailFaturamento = banco.retornaRead().GetString(72);
                    auxFlagTypePrice = banco.retornaRead().GetString(73);
                    optionFlagAtualizaMedioFaturamento = banco.retornaRead().GetString(74);
                    optionFlagRatearDescontoFaturamento = banco.retornaRead().GetString(75);
                    optionFlagVerificaPriceCustoFaturamento = banco.retornaRead().GetString(76);
                    optionFlagComissaoProdutoFaturamento = banco.retornaRead().GetString(77);
                    optionFlagComissaoIgnoraFaturamento = banco.retornaRead().GetString(78);
                    optionFlagComissaoFaturamento = banco.retornaRead().GetString(79);
                    optionFlagComissaoBaixaFaturamento = banco.retornaRead().GetString(80);
                    //             optionDescVendedorFaturamento = banco.retornaRead().GetString(1);
                    //             optionIdVendedorFaturamento = banco.retornaRead().GetString(1);
                    //             optionDescHistoricoFaturamento = banco.retornaRead().GetString(1);
                    auxIdCodigoFiscal = banco.retornaRead().GetString(81);
                    auxIdModeloNotaFiscal = banco.retornaRead().GetString(82);
                    auxIdSituacaoFiscal = banco.retornaRead().GetString(83);
                    optioncfop1 = banco.retornaRead().GetString(86);
                    optioncfop2 = banco.retornaRead().GetString(87);
                    optioncfop3 = banco.retornaRead().GetString(88);
                    optioncfop4 = banco.retornaRead().GetString(89);
                    optionflagCalculaIcms = banco.retornaRead().GetString(90);
                    optionflagCalculaPisCofins = banco.retornaRead().GetString(91);
                    optionflagCalculaIr = banco.retornaRead().GetString(92);
                    optionflagCalculaBaseSt = banco.retornaRead().GetString(93);
                    optionflagCalculoServicoProduto = banco.retornaRead().GetString(94);
                    optionflagCalculoServico = banco.retornaRead().GetString(95);
                    optionflagCalculaIpi = banco.retornaRead().GetString(96);
                    optionflagNFRestituicao = banco.retornaRead().GetString(97);
                    optionflagForceNfOrigem = banco.retornaRead().GetString(98);
                    optionflagTypeFrete = banco.retornaRead().GetString(99);
                    optionflagPgtoDevolucao = banco.retornaRead().GetString(102);
                    optionIdPgtoFornecedor = banco.retornaRead().GetString(103);
                    optionIdOperacaoEntrada = banco.retornaRead().GetString(107);
                }

            }
            catch (Exception erro)
            { MessageBox.Show(erro.Message, "Caro Usuário"); auxConsistencia = 1; }
            finally
            {
                banco.fechaConexao();
                if (auxConsistencia == 0)
                {
                    if (optionflagPgtoDevolucao == "1")
                    {   
                        if (Convert.ToInt32(optionIdPgtoFornecedor) > 0)
                        {
                            auxConsistencia = 0;
                            try
                            {
                                banco.abreConexao();
                                banco.startTransaction("conector_find_finalizadora");
                                banco.addParametro("tipo", "1");
                                banco.addParametro("find", optionIdPgtoFornecedor);
                                banco.procedimentoSet();
                            }
                            catch (Exception erro)
                            { MessageBox.Show(erro.Message, "Erro não identificado, entre contato como revendedor"); auxConsistencia = 1; }
                            finally
                            {
                                if (auxConsistencia == 0)
                                {
                                    txtDescricaoFormaPgtoNotaFiscal.Text = Convert.ToString(banco.retornaSet().Tables[0].Rows[0][1]);
                                    auxCondPgto = optionIdPgtoFornecedor;
                                }
                                banco.fechaConexao();
                            }
                        }
                    }
                    /* Alterado Flavio teste  * if (optionFlagParamentro == "e" || optionFlagParamentro == "d" || optionFlagParamentro == "r")
                    {
                        //entrada
                        txtTypeNotaFiscal.Text = "2";
                        txtCFOPNotaFiscal.Text = optioncfop1; //Sugestão para o estado do fornecedor
                    }
                    else
                    {
                        txtTypeNotaFiscal.Text = "1";
                        txtCFOPNotaFiscal.Text = optioncfop1; //Sugestão para o estado do fornecedor
                    }*/

                    switch (auxEstoqueOrigem)
                    {
                        case "s":
                            txtTypeNotaFiscal.Text = "1";
                            //txtCFOPNotaFiscal.Text = optioncfop1; //Sugestão para o estado do fornecedor
                            break;
                        case "e":
                            //entrada
                            txtTypeNotaFiscal.Text = "2";
                            //txtCFOPNotaFiscal.Text = optioncfop1; //Sugestão para o estado do fornecedor
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        public void conector_carrega_operacaoEntrada(string id)
        {

            try
            {
                banco.abreConexao();
                banco.startTransaction("conector_find_operacaoEntrada");
                banco.addParametro("tipo", "2");
                banco.addParametro("find", id);
                banco.procedimentoRead();
                if (banco.retornaRead().Read() == true)
                {

                    optionOEInCfopSugestãoOperacaoEntrada = banco.retornaRead().GetString(30);
                    optionOEOnCfopSugestãoOperacaoEntrada = banco.retornaRead().GetString(31);
                }

            }
            catch (Exception erro)
            { MessageBox.Show(erro.Message, "Caro Usuário"); }
            finally
            {
                banco.fechaConexao();
            }
        }
        public void conector_find_CFOP(string flagCfop, int input) // 0 cabeçalho nota 1 item nota fiscal
        {
            int verifica = 1; //0 libera 1 invalida;
            if (input == 0)
            {
                auxIdCFOP = "";
                auxTypeCFOP = "";
            }
            else
            {
                auxIdCFOPItemNotaFiscal = "";
            }

            try
            {
                auxConsistencia = 0;
                banco.abreConexao();
                banco.startTransaction("conector_find_CFOP");
                banco.addParametro("parametro", "4");
                banco.addParametro("find", flagCfop);
                banco.procedimentoRead();
                if (banco.retornaRead().Read() == true)
                {
                    if (input == 0)
                    {
                        auxTypeCFOP = banco.retornaRead().GetString(2);
                        //newStatusCFOP = banco.retornaRead().GetString(12) == "0" ? "false" : "true";
                        auxIdCFOP = banco.retornaRead().GetString(0);
                    }
                    else
                    {
                        if (auxTypeCFOP == banco.retornaRead().GetString(2))
                        {
                            auxIdCFOPItemNotaFiscal = banco.retornaRead().GetString(0);
                            txtQttyComercialTributavelItensNotaFiscal.Select();
                            verifica = 0;
                        }
                        else
                        {
                            verifica = 1;
                            MessageBox.Show("CFOP inválido. ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtCFOPItensNotaFiscal.Clear();
                            txtCFOPItensNotaFiscal.Select();
                        }

                    }
                }
                else
                {
                    MessageBox.Show("CFOP inválido. ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (input == 0)
                    {
                        txtCFOPNotaFiscal.Clear();
                        txtCFOPNotaFiscal.Select();
                    }
                    else
                    {
                        txtCFOPItensNotaFiscal.Clear();
                        txtCFOPItensNotaFiscal.Select();
                    }
                    verifica = 1;
                }

            }
            catch (Exception erro)
            { MessageBox.Show(erro.Message, "Caro Usuário"); auxConsistencia = 1; }
            finally
            {
                banco.fechaConexao();
                if (auxConsistencia == 0)
                {
                    if (verifica == 0)
                    {
                        if (txtTypeNotaFiscal.Text == "1" || txtTypeNotaFiscal.Text == "2")
                        {

                            if (auxTypeCFOP == "S")
                            {
                                if (input == 0)
                                {
                                    if (txtTypeNotaFiscal.Text == "1")
                                    {
                                        txtCFOPNotaFiscal.Text = auxIdCFOP;
                                        txtSerieNotaFiscal.Select();
                                    }
                                    else
                                    {
                                        MessageBox.Show("CFOP inválido para saída de mercadoria. ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        txtCFOPNotaFiscal.Clear();
                                        txtCFOPNotaFiscal.Select();
                                        tbcNotaSaida.SelectedIndex = 0;
                                    }
                                }
                            }
                            else if (auxTypeCFOP == "E")
                            {
                                if (input == 0)
                                {
                                    if (txtTypeNotaFiscal.Text == "2")
                                    {
                                        txtCFOPNotaFiscal.Text = auxIdCFOP;
                                        txtSerieNotaFiscal.Select();
                                    }
                                    else
                                    {
                                        if (input == 0)
                                        {
                                            MessageBox.Show("CFOP inválido para entrada de mercadoria. ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            txtCFOPNotaFiscal.Clear();
                                            txtCFOPNotaFiscal.Select();
                                            tbcNotaSaida.SelectedIndex = 0;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (input == 0)
                                {
                                    MessageBox.Show("CFOP inválido para entrada de mercadoria. ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    txtCFOPNotaFiscal.Clear();
                                    txtCFOPNotaFiscal.Select();
                                    tbcNotaSaida.SelectedIndex = 0;
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Tipo de nota invalido, deve ser '1 - Saída' ou '2 - Entrada'. ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtTypeNotaFiscal.Clear();
                            txtTypeNotaFiscal.Select();
                        }

                    }
                }
            }
        }
        private void conector_find_fone(string aux)
        {
            try
            {
                auxConsistencia = 0;
                banco.abreConexao();
                banco.startTransaction("conector_find_fone");
                banco.addParametro("find", aux);
                banco.addParametro("tipo", "2");
                banco.procedimentoRead();
                if (banco.retornaRead().Read() == true)
                {
                    optionDDD = banco.retornaRead().GetString(1);
                    optionTelefone = banco.retornaRead().GetString(2);
                }
            }
            catch (Exception erro) { MessageBox.Show(erro.Message, "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information); auxConsistencia = 1; }
            finally
            {
                banco.fechaConexao();

            };
        }
        public void conector_carrega_loja(string loja)
        {
            try
            {
                auxConsistencia = 0;
                banco.abreConexao();
                banco.startTransaction("conector_find_loja");
                banco.addParametro("tipo", "1");
                banco.addParametro("find_loja", loja);
                banco.procedimentoRead();
                if (banco.retornaRead().Read() == true)
                {
                    optionLojaRazao = banco.retornaRead().GetString(1);
                    optionLojaFantasia = banco.retornaRead().GetString(2);
                    optionLojaCgc = banco.retornaRead().GetString(3);
                    optionLojaIe = banco.retornaRead().GetString(4);
                    optionLojaIeMunicipio = banco.retornaRead().GetString(5);
                    optionLojaType = banco.retornaRead().GetString(6);
                    optionLojaCodigoEstado = banco.retornaRead().GetString(7);
                    optionLojaLojaUf = banco.retornaRead().GetString(8);
                    optionLojaCodigoMunicipio = banco.retornaRead().GetString(9);
                    optionLojaTypeLoja = banco.retornaRead().GetString(10);
                    optionLojaaliquotaPis = banco.retornaRead().GetString(11);
                    optionLojaAliquotaCofins = banco.retornaRead().GetString(12);
                    optionLojaControlaEstoque = banco.retornaRead().GetString(13);
                    optionLojaTypeCalculo = banco.retornaRead().GetString(14);
                    optionLojaEmpresaTroca = banco.retornaRead().GetString(15);
                    optionLojaAliquotaInss = banco.retornaRead().GetString(16);
                    optionLojaAliquotaIss = banco.retornaRead().GetString(17);
                    optionLojaMatriz = banco.retornaRead().GetString(18);
                    optionLojaDeposito = banco.retornaRead().GetString(19);
                    optionLojaSerieNota = banco.retornaRead().GetString(20);
                    optionLojaNumeroNota = banco.retornaRead().GetString(21);
                    optionLojaAtualizaCusto = banco.retornaRead().GetString(22);
                    optionLojaStatus = banco.retornaRead().GetString(23);
                    optionLojaRamo = banco.retornaRead().GetString(24);
                    optionLojaBairro = banco.retornaRead().GetString(25);
                    optionLojaComplemento = banco.retornaRead().GetString(26);
                    optionLojaMunicipio = banco.retornaRead().GetString(27);
                    optionLojaEstado = banco.retornaRead().GetString(28);
                    optionLojaNumber = banco.retornaRead().GetString(29);
                    optionLojaCEP = banco.retornaRead().GetString(30);
                    optionLojaCodEnd = banco.retornaRead().GetString(31);
                    optionLojaSeq = banco.retornaRead().GetString(32);
                    optionLojaIdBairro = banco.retornaRead().GetString(33);
                    optionLojaTipoEndereco = banco.retornaRead().GetString(34);
                    optionLojaLogradouro = banco.retornaRead().GetString(35);
                    optionLojaIdPais = banco.retornaRead().GetString(36);
                    optionLojaCodigoUf = banco.retornaRead().GetString(39);
                }
            }
            catch (Exception erro)
            { MessageBox.Show(erro.Message, "Erro não identificado, entre contato como revendedor"); auxConsistencia = 1; }
            finally
            {
                banco.fechaConexao();
                if (auxConsistencia == 0)
                {
                    if (optionLojaRazao != "")
                    {
                        string crt = optionLojaRamo;
/*                        dadosIdentificacaoEmitenteNfe identificacaoEmitenteNfe = new dadosIdentificacaoEmitenteNfe(null,
                            null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);*/
                        switch (optionLojaRamo)
                        {
                            case "s":
                            crt = "1";
                            break;
                            case "h":
                            crt = "2";
                            break;
                            case "r":
                            crt = "3";
                            break;
                        }
                        lbEmitenteRazaoNotaFiscal.Text = optionLojaRazao;
                        listaIdentificacaoEmitenteNfe.Add(identificacaoEmitenteNfe);
                        listaIdentificacaoEmitenteNfe[0].emit_xNome = optionLojaRazao;
                        listaIdentificacaoEmitenteNfe[0].emit_xFant = optionLojaFantasia;
                        listaIdentificacaoEmitenteNfe[0].emit_enderEmi_xBairro = lbEmitenteBairroNotaFiscal.Text = optionLojaBairro;
                        listaIdentificacaoEmitenteNfe[0].emit_enderEmi_xLgr = lbEmitenteLagradouroNotaFiscal.Text = optionLojaLogradouro;
                        listaIdentificacaoEmitenteNfe[0].emit_enderEmi_nro = optionLojaNumber;
                        listaIdentificacaoEmitenteNfe[0].emit_enderEmi_xCpl = optionLojaComplemento;
                        listaIdentificacaoEmitenteNfe[0].emit_enderEmi_xMun = lbEmitenteMunicipioNotaFiscal.Text = optionLojaMunicipio;
                        listaIdentificacaoEmitenteNfe[0].emit_enderEmi_cMun = optionLojaCodigoMunicipio;
                        listaIdentificacaoEmitenteNfe[0].emit_enderEmi_UF = lbEmitenteUfNotaFiscal.Text = optionLojaLojaUf;
                        listaIdentificacaoEmitenteNfe[0].emit_CNPJ = lbEmitenteCNPJNotaFiscal.Text = optionLojaCgc;
                        listaIdentificacaoEmitenteNfe[0].emit_enderEmi_CEP = lbEmitenteCEPNotaFiscal.Text = optionLojaCEP;
                        listaIdentificacaoEmitenteNfe[0].emit_enderEmi_IE = lbEmitenteINSNotaFiscal.Text = optionLojaIe;
                        listaIdentificacaoEmitenteNfe[0].emit_enderEmi_IM = lbEmitenteINSSTNotaFiscal.Text = optionLojaIeMunicipio;
                        listaIdentificacaoEmitenteNfe[0].emit_enderEmi_fone = lbEmitenteFoneNotaFiscal.Text;
                        listaIdentificacaoEmitenteNfe[0].emit_enderEmi_CRT = crt;
                        //listaIdentificacaoEmitenteNfe[0].emit_enderEmi_CNAE = option
                        // rever I.E substitu
                        conector_find_fone(loja);
                        if (auxIdLoja != "")
                        {
                            lbEmitenteFoneNotaFiscal.Text = "(" + optionDDD + ") - " + optionTelefone;
                            if (optionLojaRamo == "r")
                            {
                                cmbTributoRegimeItensNotaFiscal.Text = "Tributação Normal";
                            }
                            else
                            {
                                cmbTributoRegimeItensNotaFiscal.Text = "Simples Nacional";
                            }
                            if (optionLojaIdPais == "30")
                            {
                                cmbTributoOrigemItensNotaFiscal.Text = "Nacional";
                            }
                            else
                            {
                                //rever paramentro de importação
                            }
                        }
                    }
                }
            }
        }
        public void conector_find_entrega()
        {
            auxConsistencia = 0;
            countRows = 0;
            try
            {
                banco.abreConexao();
                banco.startTransaction("conector_find_entrega");
                banco.addParametro("find", auxIdCliente);
                banco.procedimentoSet();

            }
            catch (Exception erro)
            { MessageBox.Show(erro.Message, "Caro Usuario."); auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countRows = banco.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        //Carrega as variaveis para realização do comando update
                        auxIdEnderecoEntrega = Convert.ToString(banco.retornaSet().Tables[0].Rows[0][7]);
                        optionEntregaUfEstado = Convert.ToString(banco.retornaSet().Tables[0].Rows[0][6]);
                        optionEntregaBairroCliente = Convert.ToString(banco.retornaSet().Tables[0].Rows[0][3]);
                        optionEntregaCityCliente = Convert.ToString(banco.retornaSet().Tables[0].Rows[0][5]);
                        optionEntregaNumberCliente = Convert.ToString(banco.retornaSet().Tables[0].Rows[0][1]);
                        optionEntregaComplementoCliente = Convert.ToString(banco.retornaSet().Tables[0].Rows[0][2]);
                        optionEntregaLogradouroCliente = Convert.ToString(banco.retornaSet().Tables[0].Rows[0][0]);
                        optionEntregaCodigMunicipio = Convert.ToString(banco.retornaSet().Tables[0].Rows[0][4]);
                        if (auxIdEnderecoEntrega != "")
                        {
                            
                        }
                    }
                }
                banco.fechaConexao();
                if (auxConsistencia == 0)
                {
                    listaIdentificacaoLocalEntrega.Add(identificacaoLocalEntrega);
                    switch (optionClienteIdTipoFornecedor)
                        {
                            case "2":
                                listaIdentificacaoLocalEntrega[0].entrega_CNPJ = optionClienteCpfCnpj;
                                break;
                            default:
                                 listaIdentificacaoLocalEntrega[0].entrega_CPF  = optionClienteCpfCnpj;
                                break;
                        }
                    listaIdentificacaoLocalEntrega[0].entrega_xLgr = optionEntregaLogradouroCliente;
                    listaIdentificacaoLocalEntrega[0].entrega_nro = optionEntregaNumberCliente;
                    listaIdentificacaoLocalEntrega[0].entrega_xCpl = optionEntregaComplementoCliente;
                    listaIdentificacaoLocalEntrega[0].entrega_xBairro = optionEntregaBairroCliente;
                    listaIdentificacaoLocalEntrega[0].entrega_cMun = optionEntregaCodigMunicipio;
                    listaIdentificacaoLocalEntrega[0].entrega_xMun = optionEntregaCityCliente;
                    listaIdentificacaoLocalEntrega[0].entrega_UF = optionEntregaUfEstado;
                }
            }
        }
        public void conector_carrega_address()
        {
            auxConsistencia = 0;
            countRows = 0;
            try
            {
                banco.abreConexao();
                banco.startTransaction("conector_find_address");
                banco.addParametro("find_cliente", auxIdCliente);
                banco.addParametro("find_seq", "1");
                banco.procedimentoSet();
                
            }
            catch (Exception erro)
            { MessageBox.Show(erro.Message, "Caro Usuario."); auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countRows = banco.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        //Carrega as variaveis para realização do comando update
                        auxIdEndereco = Convert.ToString(banco.retornaSet().Tables[0].Rows[0][6]);
                        optionUfEstado = Convert.ToString(banco.retornaSet().Tables[0].Rows[0][3]);
                        optionBairroCliente = Convert.ToString(banco.retornaSet().Tables[0].Rows[0][0]);
                        optionCityCliente = Convert.ToString(banco.retornaSet().Tables[0].Rows[0][2]);
                        optionNumberCliente = Convert.ToString(banco.retornaSet().Tables[0].Rows[0][4]);
                        optionComplementoCliente = Convert.ToString(banco.retornaSet().Tables[0].Rows[0][1]);
                        optionLogradouroCliente = Convert.ToString(banco.retornaSet().Tables[0].Rows[0][10]);
                        optionCepCliente = Convert.ToString(banco.retornaSet().Tables[0].Rows[0][5]);
                        optionIdCepCliente = Convert.ToString(banco.retornaSet().Tables[0].Rows[0][11]);
                        optionCodigUfCliente = Convert.ToString(banco.retornaSet().Tables[0].Rows[0][12]);
                        if (auxIdEndereco != "")
                        {
                            lbDestinatarioCEPNotaFiscal.Text = optionCepCliente;
                            lbDestinatarioLogradouroNotaFiscal.Text = optionLogradouroCliente + ", " + optionNumberCliente;
                            lbDestinatarioBairroNotaFiscal.Text = optionBairroCliente;
                            lbDestinatarioUFNotaFiscal.Text = optionUfEstado;
                            lbDestinatarioMunicipioNotaFiscal.Text = optionCityCliente;
                        }
                    }
                }
                banco.fechaConexao();
                if (auxConsistencia == 0)
                {
                    conector_find_entrega();
                }
            }
        }

        public void conector_carrega_cliente(string idCliente)
        {
            int semaforo;
            semaforo = 0; //fecha o sinal
            try
            {
                auxConsistencia = 0;
                banco.abreConexao();
                banco.startTransaction("conector_find_cliente");
                banco.addParametro("tipo", "13");
                banco.addParametro("find_cliente", auxIdCliente);
                banco.addParametro("tipo_cliente", Convert.ToString(auxTipoPessoa));
                banco.addParametro("find_atividade", "1");
                banco.procedimentoRead();
                if (banco.retornaRead().Read() == true)
                {
                    optionClientechave = banco.retornaRead().GetString(0);
                    optionClienteNomeRazao = banco.retornaRead().GetString(1);
                    optionClienteCpfCnpj = banco.retornaRead().GetString(2);
                    optionClienteDocumentoIdentidadeIe = banco.retornaRead().GetString(3);
                    optionClienteNascimentoAbertura = banco.retornaRead().GetString(4);
                    optionClienteChaveSexo = banco.retornaRead().GetString(5);
                    optionClienteSexo = banco.retornaRead().GetString(6);
                    optionClienteChaveCivil = banco.retornaRead().GetString(7);
                    optionClienteCivil = banco.retornaRead().GetString(8);
                    optionClienteChaveLoja = banco.retornaRead().GetString(9);
                    optionClienteStatus = banco.retornaRead().GetString(10);
                    optionClienteEmpresa = banco.retornaRead().GetString(11); //Razao Empresa
                    optionClienteFantasia = banco.retornaRead().GetString(12);
                    optionClienteUf = banco.retornaRead().GetString(13);
                    optionClienteIdTipoFornecedor = banco.retornaRead().GetString(14);
                    optionClienteIdtipoPessoa = banco.retornaRead().GetString(15);
                    optionClienteIdUsuario = banco.retornaRead().GetString(16);
                    optionClienteIdAtividade = banco.retornaRead().GetString(17);
                    optionClienteObs = banco.retornaRead().GetString(18);
                    optionClienteDataEmissao = banco.retornaRead().GetString(19);
                    optionClienteDataAlteracao = banco.retornaRead().GetString(20);
                    optionClienteChaveEstado = banco.retornaRead().GetString(21);
                    optionClienteIeProdutor = banco.retornaRead().GetString(22);
                    optionClienteCodigoMunicipio = banco.retornaRead().GetString(55);
                    optionClientePais = banco.retornaRead().GetString(60);
                    optionClientePaisDescricao = banco.retornaRead().GetString(59);
                    optionClienteMail = banco.retornaRead().GetString(61);
                }
            }
            catch (Exception erro)
            { MessageBox.Show(erro.Message, "Caro cliente"); auxConsistencia = 1; }
            finally
            {
                banco.fechaConexao();
                if (auxConsistencia == 0)
                {
                    if (semaforo == 1)
                    {
                        semaforo = 0; //fecha o sinal retorna para reentender
                    }
                    if (optionClientechave != "")
                    {
                        conector_carrega_address();
                        lbDestinatarioUFNotaFiscal.Text = optionClienteUf;
                        lbDestinatarioCNPJNotaFiscal.Text = optionClienteCpfCnpj;
                        lbDestinatarioRazaoNotaFiscal.Text = optionClienteNomeRazao;
                        lbDestinatarioINSNotaFiscal.Text = optionClienteIeProdutor;
                        conector_find_fone(optionClientechave);
                        lbDestinatarioFoneNotaFiscal.Text = "(" + optionDDD + ") - " + optionTelefone;
                        listaIdentificacaoDestinatarioNfe.Add(identificacaoDestinatarioNfe);
                        switch (optionClienteIdTipoFornecedor)
                        {
                            case "2":
                                listaIdentificacaoDestinatarioNfe[0].dest_CNPJ = optionClienteCpfCnpj;
                                break;
                            default:
                                 listaIdentificacaoDestinatarioNfe[0].dest_CPF  = optionClienteCpfCnpj;
                                break;
                        }
                        listaIdentificacaoDestinatarioNfe[0].dest_xNome = optionClienteNomeRazao;
                        listaIdentificacaoDestinatarioNfe[0].dest_enderDest_xLgr = optionLogradouroCliente;
                        listaIdentificacaoDestinatarioNfe[0].dest_enderDest_nro = optionNumberCliente;
                        listaIdentificacaoDestinatarioNfe[0].dest_enderDest_xCpl = optionComplementoCliente;
                        listaIdentificacaoDestinatarioNfe[0].dest_enderDest_xBairro = optionBairroCliente;
                        listaIdentificacaoDestinatarioNfe[0].dest_enderDest_cMun = optionCodigUfCliente; //Rever
                        listaIdentificacaoDestinatarioNfe[0].dest_enderDest_xMun = optionCityCliente;
                        listaIdentificacaoDestinatarioNfe[0].dest_enderDest_UF = optionUfEstado;
                        listaIdentificacaoDestinatarioNfe[0].dest_enderDest_CEP = optionCepCliente;
                        listaIdentificacaoDestinatarioNfe[0].dest_enderDest_xPais= optionClientePaisDescricao; //Id Pais
                        listaIdentificacaoDestinatarioNfe[0].dest_enderDest_cPais = optionClientePais; //Descricao pais
                        if (optionClienteIdTipoFornecedor == "2")
                        {
                            listaIdentificacaoDestinatarioNfe[0].dest_enderDest_indIEDest = "1";
                            listaIdentificacaoDestinatarioNfe[0].dest_enderDest_IE = optionClienteDocumentoIdentidadeIe;
                        }
                        else if (optionClienteIdTipoFornecedor == "3")
                        {
                            if (optionClienteIeProdutor != "")
                            {
                                listaIdentificacaoDestinatarioNfe[0].dest_enderDest_indIEDest = "1";
                                listaIdentificacaoDestinatarioNfe[0].dest_enderDest_IE = optionClienteIeProdutor;
                            }
                            else
                            {
                                listaIdentificacaoDestinatarioNfe[0].dest_enderDest_indIEDest = "9";
                                listaIdentificacaoDestinatarioNfe[0].dest_enderDest_IE = optionClienteIeProdutor;
                            }
                        }
                        else if (optionClienteIdTipoFornecedor == "1")
                        {
                            listaIdentificacaoDestinatarioNfe[0].dest_enderDest_indIEDest = "2";
                            listaIdentificacaoDestinatarioNfe[0].dest_enderDest_IE = "ISENTO";
                        }
                        //SUFRAME
                        listaIdentificacaoDestinatarioNfe[0].dest_enderDest_ISUF = "";
                        //Inscricao Municipal
                        listaIdentificacaoDestinatarioNfe[0].dest_enderDest_IM = "";
                        listaIdentificacaoDestinatarioNfe[0].dest_enderDest_email = optionClienteMail;


                        if (txtCFOPNotaFiscal.Text == "")//Sugueri sugestao da operacao
                        {
                            switch (auxEstoqueOrigem)
                            {
                                case "s":
                                    if (lbDestinatarioUFNotaFiscal.Text == lbEmitenteUfNotaFiscal.Text)
                                    {
                                        txtCFOPNotaFiscal.Text = optioncfop3;
                                    }
                                    else
                                    {
                                        txtCFOPNotaFiscal.Text = optioncfop4;
                                    }
                                    break;

                                case "e":
                                    if (lbDestinatarioUFNotaFiscal.Text == lbEmitenteUfNotaFiscal.Text)
                                    {
                                        txtCFOPNotaFiscal.Text = optioncfop1;
                                    }
                                    else
                                    {
                                        txtCFOPNotaFiscal.Text = optioncfop2;
                                    }
                                    break;
                            }
                            if (txtCFOPNotaFiscal.Text != "")
                            {
                                conector_find_CFOP(txtCFOPNotaFiscal.Text, 0);
                            }
                        }
                    }
                }
            }

        }
        protected void conector_corregaConfig_itemNfSaida()
        {
            auxConsistencia = 0;
            enabled = "f"; //f fechado a aberto
            try
            {
                banco.abreConexao();
                banco.startTransaction("conector_find_itemNfSaida");
                banco.addParametro("tipo", "2");
                banco.addParametro("find_loja", auxIdLoja);
                banco.addParametro("find", auxIdNf);
                banco.addParametro("seq", auxIdNfItem);
                banco.procedimentoRead();
                if (banco.retornaRead().Read() == true)
                {
                    auxIdProduto = banco.retornaRead().GetString(0);
                    txtCodigoItensNotaFiscal.Text = banco.retornaRead().GetString(0);
                    txtDescricaoItensNotaFiscal.Text = banco.retornaRead().GetString(1);
                    auxUnItemNotaFiscal = banco.retornaRead().GetString(2);
                    txtEANBarraItensNotaFiscal.Text = banco.retornaRead().GetString(3);
                    auxIdNfItem = banco.retornaRead().GetString(5);
                    optionProdutoValorLiquido = banco.retornaRead().GetString(6);
                    optionProdutopriceOriginal = banco.retornaRead().GetString(7);
                    optionProdutopriceVenda = banco.retornaRead().GetString(8);
                    optionProdutopriceCusto = banco.retornaRead().GetString(9);
                    optionProdutoestoque = banco.retornaRead().GetString(10);
                    optionProdutopeso =  banco.retornaRead().GetString(11);
                    optionProdutoaliquota = banco.retornaRead().GetString(13);
                    txtValorIcmsItensNotaFiscal.Text = banco.retornaRead().GetString(14);
                    txtBaseIcmsItensNotaFiscal.Text = banco.retornaRead().GetString(15);
                    optionProdutoreducao = banco.retornaRead().GetString(16);
                    txtReducaoBaseCalculoItensNotaFiscal.Text =optionProdutoreducao;
                    txtQttyComercialTributavelItensNotaFiscal.Text = banco.retornaRead().GetString(17);
                    auxIdUnItemNotaFiscal = banco.retornaRead().GetString(18);
                    txtCFOPItensNotaFiscal.Text = banco.retornaRead().GetString(19);
                    returnCST = banco.retornaRead().GetString(20);
                    auxCstPisProduto = banco.retornaRead().GetString(21);
                    returnPIS = banco.retornaRead().GetString(51);
                    returnIPI = banco.retornaRead().GetString(53);
                    returnCOFINS = banco.retornaRead().GetString(52);
                    optionProdutovalorPis = banco.retornaRead().GetString(22);
                    optionProdutobasePis = banco.retornaRead().GetString(23);
                    auxCstCofinsProduto = banco.retornaRead().GetString(24);
                    optionProdutovalorCofins = banco.retornaRead().GetString(25);
                    optionProdutobaseCofins = banco.retornaRead().GetString(26);
                    auxCstIpiProduto = banco.retornaRead().GetString(27);
                    txtAliquotaIPIItensNotaFiscal.Text = banco.retornaRead().GetString(28);
                    txtValorIPIItensNotaFiscal.Text = banco.retornaRead().GetString(29);
                    txtValorBaseCalculoIPIItensNotaFiscal.Text = banco.retornaRead().GetString(30);
                    txtDescontoItensNotaFiscal.Text = banco.retornaRead().GetString(31);
                    optionProdutodescontoValor = banco.retornaRead().GetString(32);
                    txtOutrasDespesasAcessoriasItensNotaFiscal.Text = banco.retornaRead().GetString(33);
                    optionProdutoacrescimoValor = banco.retornaRead().GetString(34);
                    txtAliquotaIcmsSTItensNotaFiscal.Text = banco.retornaRead().GetString(35);
                    txtBaseIcmsSTItensNotaFiscal.Text = banco.retornaRead().GetString(36);
                    txtValorIcmsSTItensNotaFiscal.Text = banco.retornaRead().GetString(37);
                    txtReducaoBaseCalculoSTItensNotaFiscal.Text = banco.retornaRead().GetString(38);
                    txtValorBrutoItensNotaFiscal.Text = banco.retornaRead().GetString(40);
                    optionProdutovalorTotalNota = banco.retornaRead().GetString(41);
                    optionProdutovalorTotalLiquido = banco.retornaRead().GetString(42);
                    optionProdutoFornecedor = banco.retornaRead().GetString(43);
                    optionProdutoIdSetor = banco.retornaRead().GetString(44);
                    optionProdutoCodigoAliquota = banco.retornaRead().GetString(45);
                    auxCstIpiProduto = banco.retornaRead().GetString(48);
                    auxCstPisProduto = banco.retornaRead().GetString(49);
                    auxCstCofinsProduto = banco.retornaRead().GetString(50);
                    txtCodigoNCMItensNotaFiscal.Text = banco.retornaRead().GetString(54);
                    txtValorUnIPIItensNotaFiscal.Text = banco.retornaRead().GetString(55);
                    txtTotalFreteItensNotaFiscal.Text = banco.retornaRead().GetString(58);
                    auxIdGenero = banco.retornaRead().GetString(59);
                    txtTotalSeguroItensNotaFiscal.Text = banco.retornaRead().GetString(60);
                    enabled = "a"; //libera os combos
                    flagSemaforoItem = 1;
                }
            }
            catch (Exception erro)
            { MessageBox.Show(erro.Message, "Erro não identificado, entre contato como revendedor"); auxConsistencia = 1; }
            finally
            {
                banco.fechaConexao();
                if (auxConsistencia == 0)
                {
                    if (enabled == "a")
                    {
                        txtValorUnComercialTributarioItensNotaFiscal.Text = optionProdutopriceVenda;
                        cmbUnidadeDefaultItensNotaFiscal.Text = auxUnItemNotaFiscal;
                        txtAliquotaIcmsItensNotaFiscal.Text = optionProdutoaliquota;
                        cmbTributoSituacaoTributariaItensNotaFiscal.Text = returnCST;
                        cmbSituacaoTributariaPISItensNotaFiscal.Text = returnPIS;
                        cmbSituacaoTributariaCofinsItensNotaFiscal.Text = returnCOFINS;
                        cmbSituacaoTributariaIPIItensNotaFiscal.Text = returnIPI;
                        if (txtValorIPIItensNotaFiscal.Text != "" && txtValorIPIItensNotaFiscal.Text != "0.00")
                        {
                            txtQttyTotalUnDefaultIPIItensNotaFiscal.Text = txtQttyComercialTributavelItensNotaFiscal.Text;
                        }
                    }
                }
            }
        }
        protected void conector_find_itemNfSaida(string tipo)
        {
            if (tipo == "1")
            {
                dgvItensNotaFiscal.Rows.Clear();
            }
            countField = 0;
            auxConsistencia = 0;
            try
            {
                banco.abreConexao();
                banco.startTransaction("conector_find_itemNfSaida");
                banco.addParametro("tipo", tipo);
                banco.addParametro("find_loja", auxIdLoja);
                banco.addParametro("find", auxIdNf);
                banco.addParametro("seq", "0");
                banco.procedimentoSet();
            }
            catch (Exception erro)
            { MessageBox.Show(erro.Message, "Erro não identificado, entre contato como revendedor"); auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    if (tipo == "1")
                    {
                        countField = banco.retornaSet().Tables[0].Columns.Count;
                        countRows = banco.retornaSet().Tables[0].DefaultView.Count;
                        if (countRows > 0)
                        {
                            dgvItensNotaFiscal.AllowUserToAddRows = false;
                            for (i = 0; i < countRows; i++)
                            {
                                dgvItensNotaFiscal.Rows.Add();//Produtos e Servicos NFE
                                for (j = 0; j < countField; j++)
                                {
                                    dgvItensNotaFiscal.Rows[i].Cells[j].Value = Convert.ToString(banco.retornaSet().Tables[0].Rows[i][j]);

                                }

                            }
                        }
                        else
                        {
                            if (dgvItensNotaFiscal.RowCount < 1)
                            {
                                dgvItensNotaFiscal.Rows.Add();
                            }
                        }
                    }
                    else if (tipo == "5")
                    {
                        //dadosTributosIncidentesProdutoServico tributosIncidentesProdutoServico = new dadosTributosIncidentesProdutoServico(null);
                        countField = banco.retornaSet().Tables[0].Columns.Count;
                        countRows = banco.retornaSet().Tables[0].DefaultView.Count;
                        if (countRows > 0)
                        {
                            for (i = 0; i < countRows; i++)
                            {
                                listaProdutosServicosNfe.Add(produtosServicosNfe);
                                listaProdutosServicosNfe[i].prod_cProd = Convert.ToString(banco.retornaSet().Tables[0].Rows[i][0]);
                                listaProdutosServicosNfe[i].prod_cEAN = Convert.ToString(banco.retornaSet().Tables[0].Rows[i][1]);
                                listaProdutosServicosNfe[i].prod_xProd = Convert.ToString(banco.retornaSet().Tables[0].Rows[i][2]);
                                listaProdutosServicosNfe[i].prod_NCM = Convert.ToString(banco.retornaSet().Tables[0].Rows[i][3]);
                                listaProdutosServicosNfe[i].prod_EXTIPI = Convert.ToString(banco.retornaSet().Tables[0].Rows[i][4]);
                                listaProdutosServicosNfe[i].prod_CFOP= Convert.ToString(banco.retornaSet().Tables[0].Rows[i][5]);
                                listaProdutosServicosNfe[i].prod_uCom = Convert.ToString(banco.retornaSet().Tables[0].Rows[i][6]);
                                listaProdutosServicosNfe[i].prod_qCom = Convert.ToString(banco.retornaSet().Tables[0].Rows[i][7]);
                                listaProdutosServicosNfe[i].prod_vUnCom = Convert.ToString(banco.retornaSet().Tables[0].Rows[i][8]);
                                listaProdutosServicosNfe[i].prod_vProd = Convert.ToString(banco.retornaSet().Tables[0].Rows[i][9]);
                                listaProdutosServicosNfe[i].prod_cEANTrib = Convert.ToString(banco.retornaSet().Tables[0].Rows[i][10]);
                                listaProdutosServicosNfe[i].prod_uTrib = Convert.ToString(banco.retornaSet().Tables[0].Rows[i][11]);
                                listaProdutosServicosNfe[i].prod_qTrib = Convert.ToString(banco.retornaSet().Tables[0].Rows[i][12]);
                                listaProdutosServicosNfe[i].prod_vUnTrib = Convert.ToString(banco.retornaSet().Tables[0].Rows[i][13]);
                                listaProdutosServicosNfe[i].prod_vFrete = Convert.ToString(banco.retornaSet().Tables[0].Rows[i][14]);
                                listaProdutosServicosNfe[i].prod_vSeg = Convert.ToString(banco.retornaSet().Tables[0].Rows[i][15]);
                                listaProdutosServicosNfe[i].prod_vDesc = Convert.ToString(banco.retornaSet().Tables[0].Rows[i][16]);
                                listaProdutosServicosNfe[i].prod_vOutro = Convert.ToString(banco.retornaSet().Tables[0].Rows[i][17]);
                                listaProdutosServicosNfe[i].prod_indTot = Convert.ToString(banco.retornaSet().Tables[0].Rows[i][18]);
                            }
                        }
                    }
                }
                banco.fechaConexao();
            }
        }
        private void conector_find_finalizadora(string aux)
        {
            auxConsistencia = 0;
            try
            {
                banco.abreConexao();
                banco.startTransaction("conector_find_finalizadora");
                banco.addParametro("tipo", "1");
                banco.addParametro("find", aux);
                banco.procedimentoSet();
            }
            catch (Exception erro)
            { MessageBox.Show(erro.Message, "Erro não identificado, entre contato como revendedor"); auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countRows = banco.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        auxCondPgto = Convert.ToString(banco.retornaSet().Tables[0].Rows[0][0]);
                        txtDescricaoFormaPgtoNotaFiscal.Text = Convert.ToString(banco.retornaSet().Tables[0].Rows[0][1]);
                        switch (Convert.ToString(banco.retornaSet().Tables[0].Rows[0][8]))
                        {
                            case "AV":
                                auxTypeVenda = "0";
                                break;
                            case "AP":
                                auxTypeVenda = "1";
                                break;
                            default:
                                auxTypeVenda = "2";
                                break;
                        } 
                    }
                }
                banco.fechaConexao();
            }
        }
        protected void conector_del_itemNfSaida()
        {
            try
            {
                banco.abreConexao();
                banco.startTransaction("conector_del_itemNfSaida");
                banco.addParametro("item", auxIdProduto);
                banco.addParametro("nota", auxIdNf);
                banco.addParametro("idItem", auxIdNfItem);
                banco.procedimentoRead();
                if (banco.retornaRead().Read() == true)
                {
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message, "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                banco.fechaConexao();

            }
        }
        protected void conector_gera_nfItem(string nr_reserva, string typeNota)
        {
            Int16 valida = 0;
            auxConsistencia = 0;
            try
            {
                banco.abreConexao();
                banco.startTransaction("conector_gera_nfItem");
                banco.addParametro("pedido", nr_reserva);
                banco.addParametro("typeNota", typeNota);
                banco.procedimentoRead();
                if (banco.retornaRead().Read() == true)
                {
                    auxIdNf = banco.retornaRead().GetString(0);
                    valida = 1;
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message, "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                auxConsistencia = 1;
            }
            finally
            {
                banco.fechaConexao();
                if (auxConsistencia == 0)
                {
                    flagSemaforo = 1;
                    conector_update_status_reserva("vermelho", auxIdPedido, alwaysVariables.Store); //Muda o status de emitido para fiscal
                    tslPedido.Text = tslPedido.Text + ": " + auxIdPedido;
                    tbcNotaSaida.SelectedIndex = 1;
                    txtCodigoItensNotaFiscal.Select();
                    if (valida == 1)
                    {
                        switch (auxModalidadeFrete)
                        {
                            case 9:
                                lbTransporteRazaoNotaFiscal.Text = "";
                                lbCodigoAnttNotaFiscal.Text = "";
                                lbPlacaVeiculoNotaFiscal.Text = "";
                                lbTransporteUfNotaFiscal.Text = "";
                                lbUfVeiculoNotaFiscal.Text = "";
                                lbTransporteCNPJNotaFiscal.Text = "";
                                lbTransporteINSNotaFiscal.Text = "";
                                lbTransporteMunicipioNotaFiscal.Text = "";
                                lbTransporteLogradouroNotaFiscal.Text = "";
                                lbTransporteQuantidadeNotaFiscal.Text = "";
                                lbTransporteEspecieNotaFiscal.Text = "";
                                lbTransporteNumeroNotaFiscal.Text = "";
                                lbTransporteLogradouroNotaFiscal.Text = "";
                                break;
                            default:
                                conector_inc_nfinfortransporte();
                                if (auxIdNfTransportadora != "")
                                {
                                    conector_inc_nfInforVeiculo();
                                }
                                break;
                        }
                    }
                    sinal = 1;
                }
            }
        }
        protected void conector_inc_nf()
        {
            Int16 valida = 0;
            auxNrNota = mskNumberNotaFiscal.Text;
            auxNrNota = mskNumberNotaFiscal.Text.Replace(".", "");
            auxNrNota = auxNrNota.Replace("-", "");
            auxConsistencia = 0;
            try
            {
                banco.abreConexao();
                banco.startTransaction("conector_inc_nf");
                banco.addParametro("inc_loja", alwaysVariables.Store);
                banco.addParametro("inc_idcliente", auxIdCliente);
                banco.addParametro("inc_idparamentro", auxIdParamentro);
                banco.addParametro("inc_idtransportadora", optionCodigoTransportadora);
                banco.addParametro("inc_cfop", txtCFOPNotaFiscal.Text);
                banco.addParametro("inc_idFuncionario", auxIdFuncionario == null ? "1" : auxIdFuncionario);
                banco.addParametro("inc_idusuario", auxIdUser);
                banco.addParametro("inc_idpedido", auxIdPedido == null ? "0" : auxIdPedido);
                banco.addParametro("inc_nr_nota", auxNrNota);
                banco.addParametro("inc_serie", txtSerieNotaFiscal.Text);
                banco.addParametro("inc_acrescimo", auxPorcentagemAcrescimo);
                banco.addParametro("inc_baseicms", lbCalculoImpostoBaseCalculoNotaFiscal.Text);
                banco.addParametro("inc_baseIcmsIsento", auxCalculoBaseIcmsIsento);
                banco.addParametro("inc_valorIcmsSubstituicao", lbCalculoImpostoValorBaseIcmsSubstituicaoNotaFiscal.Text);
                banco.addParametro("inc_baseCalculoIcmsSubstituicao", lbCalculoImpostoValorIcmsSubstituicaoNotaFiscal.Text);
                banco.addParametro("inc_baseIPI", auxCalculoBaseIPI);
                banco.addParametro("inc_baseCofins", auxCalculoBaseCofins);
                banco.addParametro("inc_basePis", auxCalculoBasePis);
                banco.addParametro("inc_emissao", String.Format("{0:yyyyMMdd}", dtpEmissaoNotaFiscal.Value));
                banco.addParametro("inc_saida", String.Format("{0:yyyyMMdd}", dtpSaidaNotaFiscal.Value));
                banco.addParametro("inc_alteracao", String.Format("{0:yyyyMMdd}", DateTime.Now));
                banco.addParametro("inc_hora", "0000"); //Provisorio
                banco.addParametro("inc_desconto", auxPorcentagemDesconto);
                banco.addParametro("inc_uf", lbDestinatarioUFNotaFiscal.Text);
                banco.addParametro("inc_itens", auxCalculoTotalItens);
                banco.addParametro("inc_seguro", auxCalculoSeguro);
                banco.addParametro("inc_frete", lbCalculoImpostoValorFreteNotaFiscal.Text);
                banco.addParametro("inc_typeFrete", auxModalidadeFrete.ToString());
                banco.addParametro("inc_valorIcms", lbCalculoImpostoValorIcmsNotaFiscal.Text);
                banco.addParametro("inc_valorIpi", lbCalculoImpostoIPINotaFiscal.Text);
                banco.addParametro("inc_valorPis", auxCalculoValorPis);
                banco.addParametro("inc_valorCofins", auxCalculoValorCofins);
                banco.addParametro("inc_acrecismoValor", auxCalculoValorAcrescimo);
                banco.addParametro("inc_descontoValor", lbCalculoImpostoDescontoNotaFiscal.Text);
                banco.addParametro("inc_valorTotalLiquido", auxCalculoValorTotalLiquido);
                banco.addParametro("inc_valorTotalNota", lbCalculoImpostoValorTotalDaNotaFiscal.Text);
                banco.addParametro("inc_valorTotalProdutos", lbCalculoImpostoValorTotalProdutoNotaFiscal.Text);
                banco.addParametro("inc_volumes", auxTotalVolumeNf);
                banco.addParametro("inc_peso", lbTransportePesoBrutoNotaFiscal.Text);
                banco.addParametro("inc_contribuicaoSocial", auxCalculoContribucaoSocial);
                banco.addParametro("inc_quantidadePedido", auxQttyPedido);
                banco.addParametro("inc_quantidadeRecebida", auxQttyRecebida);
                banco.addParametro("inc_impresso", "0"); //provisorio  == false ? "0" : "1"
                banco.addParametro("inc_nr_impressao", "0"); //provisorio
                banco.addParametro("inc_idTable_Codigo", auxIdCodigoFiscal);
                banco.addParametro("inc_modNotaFiscal", auxIdModeloNotaFiscal);
                banco.addParametro("inc_idSituacaoFiscal", auxIdSituacaoFiscal);
                banco.addParametro("inc_emitiNfe", "0"); //provisorio recoferir setting
                banco.addParametro("inc_typenf", txtTypeNotaFiscal.Text);
                banco.addParametro("inc_Msg01", txtMsg01NotaFiscal.Text);
                banco.addParametro("inc_Msg02", txtMsg02NotaFiscal.Text);
                banco.addParametro("inc_Msg03", txtMsg03NotaFiscal.Text);
                banco.addParametro("inc_valorTotaServico", lbISSQNNotaFiscal.Text);
                banco.addParametro("inc_nr_nota_entrada", auxIdEntrada);
                banco.addParametro("inc_serie_entrada",txtSerieNfEntradaNotaSaida.Text);
                banco.addParametro("inc_statusNf", "0");
                banco.addParametro("inc_restituicao", "n");
                banco.addParametro("inc_condPgto", auxCondPgto);
                banco.procedimentoRead();
                if (banco.retornaRead().Read() == true)
                {
                    auxIdNf = banco.retornaRead().GetString(0);
                    valida = 1;
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message, "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                auxConsistencia = 1;
            }
            finally
            {
                banco.fechaConexao();
                if (auxConsistencia == 0)
                {
                    flagSemaforo = 1;
                    tbcNotaSaida.SelectedIndex = 1;
                    txtCodigoItensNotaFiscal.Select();
                    conector_find_itemNfSaidaTotais();
                    if (valida == 1)
                    {
                        switch (auxModalidadeFrete)
                        {
                            case 9:
                                lbTransporteRazaoNotaFiscal.Text = "";
                                lbCodigoAnttNotaFiscal.Text = "";
                                lbPlacaVeiculoNotaFiscal.Text = "";
                                lbTransporteUfNotaFiscal.Text = "";
                                lbUfVeiculoNotaFiscal.Text = "";
                                lbTransporteCNPJNotaFiscal.Text = "";
                                lbTransporteINSNotaFiscal.Text = "";
                                lbTransporteMunicipioNotaFiscal.Text = "";
                                lbTransporteLogradouroNotaFiscal.Text = "";
                                lbTransporteQuantidadeNotaFiscal.Text = "";
                                lbTransporteEspecieNotaFiscal.Text = "";
                                lbTransporteNumeroNotaFiscal.Text = "";
                                lbTransporteLogradouroNotaFiscal.Text = "";
                                break;
                            default:
                                conector_inc_nfinfortransporte();
                                if (auxIdNfTransportadora != "")
                                {
                                    conector_inc_nfInforVeiculo();   
                                }
                                break;
                        }
                    }
                }
            }
        }
        protected void conector_inc_nfItem()
        {
            auxConsistencia = 0;
            try
            {
                banco.abreConexao();
                banco.startTransaction("conector_inc_nfItem");
                banco.addParametro("inc_idnf", auxIdNf);
                banco.addParametro("inc_idProduto",auxIdProduto);
                banco.addParametro("inc_valorLiquido", optionProdutoValorLiquido);
                banco.addParametro("inc_priceOriginal", optionProdutopriceOriginal);
                banco.addParametro("inc_priceVenda", optionProdutopriceVenda);
                banco.addParametro("inc_priceCusto",optionProdutopriceCusto);
                banco.addParametro("inc_estoque", optionProdutoestoque);
                banco.addParametro("inc_data",String.Format("{0:yyyyMMdd}", DateTime.Now));
                banco.addParametro("inc_peso", optionProdutopeso);
                banco.addParametro("inc_aliquota", txtAliquotaIcmsItensNotaFiscal.Text);//optionProdutoaliquota);
                banco.addParametro("inc_icms", txtValorIcmsItensNotaFiscal.Text);
                banco.addParametro("inc_baseCalculo", txtBaseIcmsItensNotaFiscal.Text);
                banco.addParametro("inc_reducao",optionProdutoreducao);
                banco.addParametro("inc_quantidade", txtQttyComercialTributavelItensNotaFiscal.Text);
                banco.addParametro("inc_idunidadeMedida", optionProdutoidunidadeMedida);
                banco.addParametro("inc_cfop", txtCFOPItensNotaFiscal.Text);
                banco.addParametro("inc_cstIcms", auxCstProduto);
                banco.addParametro("inc_cstPis",auxCstPisProduto);
                banco.addParametro("inc_valorPis", optionProdutovalorPis);
                banco.addParametro("inc_basePis", optionProdutobasePis);
                banco.addParametro("inc_cstCofins", auxCstCofinsProduto);
                banco.addParametro("inc_valorCofins",optionProdutovalorCofins);
                banco.addParametro("inc_baseCofins", optionProdutobaseCofins);
                banco.addParametro("inc_cstIpi",auxCstIpiProduto);
                banco.addParametro("inc_ipi", txtAliquotaIPIItensNotaFiscal.Text);
                banco.addParametro("inc_ipiValor", txtValorUnIPIItensNotaFiscal.Text);
                banco.addParametro("inc_valorIpi",txtValorIPIItensNotaFiscal.Text);
                banco.addParametro("inc_baseIpi", txtValorBaseCalculoIPIItensNotaFiscal.Text);
                banco.addParametro("inc_desconto", txtDescontoItensNotaFiscal.Text);
                banco.addParametro("inc_descontoValor", optionProdutodescontoValor);
                banco.addParametro("inc_acrescimo",txtOutrasDespesasAcessoriasItensNotaFiscal.Text);
                banco.addParametro("inc_acrescimoValor",optionProdutoacrescimoValor);
                banco.addParametro("inc_aliquotaIcmsSt", txtAliquotaIcmsSTItensNotaFiscal.Text);//Conferir questao aliquota do ST como vai no XML nfe
                banco.addParametro("inc_baseCalculoIcmsSubstituicao", txtBaseIcmsSTItensNotaFiscal.Text);
                banco.addParametro("inc_valorIcmsSubstituicao", txtValorIcmsSTItensNotaFiscal.Text);
                banco.addParametro("inc_reducaoIcmsSt", txtReducaoBaseCalculoSTItensNotaFiscal.Text);
                banco.addParametro("inc_margem", "0");//Conferir
                banco.addParametro("inc_valorTotalProduto", txtValorBrutoItensNotaFiscal.Text);
                banco.addParametro("inc_valorTotalNota", optionProdutovalorTotalNota);
                banco.addParametro("inc_valorTotalLiquido", optionProdutovalorTotalLiquido);
                banco.addParametro("inc_fornecedor", optionProdutoFornecedor);
                banco.addParametro("inc_idsetor", optionProdutoIdSetor);
                banco.addParametro("inc_tributacao", optionProdutoCodigoAliquota);
                banco.addParametro("inc_typeAliquota", optionProdutoTypeAliquota);
                banco.addParametro("inc_chaveEntrada", optionProdutoChaveEntradaItem);
                banco.addParametro("inc_valorFrete", txtTotalFreteItensNotaFiscal.Text);
                banco.addParametro("inc_genero", auxIdGenero);
                banco.addParametro("inc_seguro", txtTotalSeguroItensNotaFiscal.Text);
                banco.procedimentoRead();
                if (banco.retornaRead().Read() == true)
                {
                    auxIdNfItem = banco.retornaRead().GetString(0);
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message, "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                auxConsistencia = 1;
            }
            finally
            {
                banco.fechaConexao();
                if (auxConsistencia == 0)
                {
                    flagSemaforoItem = 0;
                    conector_find_itemNfSaida("1");
                    dgvItensNotaFiscal.Select();
                    txtCodigoItensNotaFiscal.ReadOnly = false;
                    zeraObjItem();
                    txtCodigoItensNotaFiscal.Select();
                    tbcNotaSaida.SelectedIndex = 1;
                    tbcTributosItemNota.SelectedIndex = 0;
                }
                else
                {

                    txtCodigoItensNotaFiscal.ReadOnly = false;
                    zeraObjItem();
                    txtCodigoItensNotaFiscal.Select();
                    flagSemaforoItem = 0;
                }
            }
        }

        protected void conector_alt_nfItem()
        {
            auxConsistencia = 0;
            try
            {
                banco.abreConexao();
                banco.startTransaction("conector_alt_nfItem");
                banco.addParametro("newidNfItem", auxIdNfItem);
                banco.addParametro("newidnf", auxIdNf);
                banco.addParametro("newidProduto", auxIdProduto);
                banco.addParametro("newvalorLiquido", optionProdutoValorLiquido);
                banco.addParametro("newpriceOriginal", optionProdutopriceOriginal);
                banco.addParametro("newpriceVenda", optionProdutopriceVenda);
                banco.addParametro("newpriceCusto", optionProdutopriceCusto);
                banco.addParametro("newestoque", optionProdutoestoque);
                banco.addParametro("newpeso", optionProdutopeso);
                banco.addParametro("newaliquota", optionProdutoaliquota);
                banco.addParametro("newicms", txtValorIcmsItensNotaFiscal.Text);
                banco.addParametro("newbaseCalculo", txtBaseIcmsItensNotaFiscal.Text);
                banco.addParametro("newreducao", optionProdutoreducao);
                banco.addParametro("newquantidade", txtQttyComercialTributavelItensNotaFiscal.Text);
                banco.addParametro("newidunidadeMedida", optionProdutoidunidadeMedida);
                banco.addParametro("newcfop", txtCFOPItensNotaFiscal.Text);
                banco.addParametro("newcstIcms", auxCstProduto);
                banco.addParametro("newcstPis", auxCstPisProduto);
                banco.addParametro("newvalorPis", optionProdutovalorPis);
                banco.addParametro("newbasePis", optionProdutobasePis);
                banco.addParametro("newcstCofins", auxCstCofinsProduto);
                banco.addParametro("newvalorCofins", optionProdutovalorCofins);
                banco.addParametro("newbaseCofins", optionProdutobaseCofins);
                banco.addParametro("newcstIpi", auxCstIpiProduto);
                banco.addParametro("newipi", txtAliquotaIPIItensNotaFiscal.Text);
                banco.addParametro("newipiValor", txtValorUnIPIItensNotaFiscal.Text);
                banco.addParametro("newvalorIpi", txtValorIPIItensNotaFiscal.Text);
                banco.addParametro("newbaseIpi", txtValorBaseCalculoIPIItensNotaFiscal.Text);
                banco.addParametro("newdesconto", txtDescontoItensNotaFiscal.Text);
                banco.addParametro("newdescontoValor", optionProdutodescontoValor);
                banco.addParametro("newacrescimo", txtOutrasDespesasAcessoriasItensNotaFiscal.Text);
                banco.addParametro("newacrescimoValor", optionProdutoacrescimoValor);
                banco.addParametro("newaliquotaIcmsSt", txtAliquotaIcmsSTItensNotaFiscal.Text);//Conferir questao aliquota do ST como vai no XML nfe
                banco.addParametro("newbaseCalculoIcmsSubstituicao", txtBaseIcmsSTItensNotaFiscal.Text);
                banco.addParametro("newvalorIcmsSubstituicao", txtValorIcmsSTItensNotaFiscal.Text);
                banco.addParametro("newreducaoIcmsSt", txtReducaoBaseCalculoSTItensNotaFiscal.Text);
                banco.addParametro("newmargem", "0");//Conferir
                banco.addParametro("newvalorTotalProduto", txtValorBrutoItensNotaFiscal.Text);
                banco.addParametro("newvalorTotalNota", optionProdutovalorTotalNota);
                banco.addParametro("newvalorTotalLiquido", optionProdutovalorTotalLiquido);
                banco.addParametro("newfornecedor", optionProdutoFornecedor);
                banco.addParametro("newidsetor", optionProdutoIdSetor);
                banco.addParametro("newtributacao", optionProdutoCodigoAliquota);
                banco.addParametro("newtypeAliquota", optionProdutoTypeAliquota);
                banco.addParametro("newchaveEntrada", optionProdutoChaveEntradaItem);
                banco.addParametro("newvalorFrete", txtTotalFreteItensNotaFiscal.Text);
                banco.addParametro("newIdGenero", auxIdGenero);
                banco.addParametro("newSeguro", txtTotalSeguroItensNotaFiscal.Text);
                banco.procedimentoRead();
                if (banco.retornaRead().Read() == true)
                {
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message, "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                auxConsistencia = 1;
            }
            finally
            {
                banco.fechaConexao();
                if (auxConsistencia == 0)
                {
                    flagSemaforoItem = 1;
                    conector_find_itemNfSaida("1");
                    dgvItensNotaFiscal.Select();
                    txtCodigoItensNotaFiscal.ReadOnly = false;
                    zeraObjItem();
                    txtCodigoItensNotaFiscal.Select();
                    tbcNotaSaida.SelectedIndex = 1;
                    tbcTributosItemNota.SelectedIndex = 0;
                }
            }
        }
        protected void conector_find_itemNfSaidaTotais()
        {
            string mask, text, tamanho;
            mask = "000000000";
            text = "";
            tamanho = "0";
            auxConsistencia = 0;
            try
            {
                banco.abreConexao();
                banco.startTransaction("conector_find_itemNfSaida");
                banco.addParametro("tipo", "3");
                banco.addParametro("find_loja", auxIdLoja);
                banco.addParametro("find", auxIdNf);
                banco.addParametro("seq", "0");
                banco.procedimentoRead();
                if (banco.retornaRead().Read() == true)
                {
                    lbCalculoImpostoValorTotalDaNotaFiscal.Text = banco.retornaRead().GetString(10);
                    lbCalculoImpostoValorTotalProdutoNotaFiscal.Text = banco.retornaRead().GetString(11);
                    lbCalculoImpostoIPINotaFiscal.Text = banco.retornaRead().GetString(12);
                    lbCalculoImpostoValorIcmsSubstituicaoNotaFiscal.Text = banco.retornaRead().GetString(8);
                    lbCalculoImpostoValorBaseIcmsSubstituicaoNotaFiscal.Text = banco.retornaRead().GetString(7);
                    lbCalculoImpostoValorIcmsNotaFiscal.Text = banco.retornaRead().GetString(6);
                    lbCalculoImpostoBaseCalculoNotaFiscal.Text = banco.retornaRead().GetString(5);
                    lbCalculoImpostoDespesasAcessoriasNotaFiscal.Text = banco.retornaRead().GetString(4);
                    lbCalculoImpostoDescontoNotaFiscal.Text = banco.retornaRead().GetString(3);
                    lbTransportePesoLiquidoNotaFiscal.Text = banco.retornaRead().GetString(2);
                    lbTransporteQuantidadeNotaFiscal.Text = banco.retornaRead().GetString(0);
                    text = banco.retornaRead().GetString(15);
                    txtSerieNotaFiscal.Text = banco.retornaRead().GetString(16);
                    tamanho = (mask.Length - text.Length).ToString();
                    mskNumberNotaFiscal.Text = (mask.Substring(0, Convert.ToInt32(tamanho)) + text);
                    lbCalculoImpostoValorFreteNotaFiscal.Text = banco.retornaRead().GetString(17);
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message, "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                auxConsistencia = 1;
            }
            finally
            {
                banco.fechaConexao();
                if (auxConsistencia == 0)
                {
                    conector_carrega_variaveisNota();
                }
            }
        }
        public void conector_find_pedido(string pedido)
        {
            try
            {
                auxConsistencia = 0;
                banco.abreConexao();
                banco.startTransaction("conector_find_pedido");
                banco.addParametro("find", pedido);
                banco.addParametro("tipo", "4");
                banco.addParametro("loja", "0");
                banco.addParametro("cliente", "0");
                banco.addParametro("situacao", "0");
                banco.addParametro("operacao", "0");
                banco.addParametro("dataInicial", String.Format("{0:yyyyMMdd}", DateTime.Now));
                banco.addParametro("dataFinal", String.Format("{0:yyyyMMdd}", DateTime.Now));
                banco.procedimentoRead();
                if (banco.retornaRead().Read() == true)
                {
                    auxDescricaoFaturamento = banco.retornaRead().GetString(0);
                    auxDescricaoDestino = banco.retornaRead().GetString(1);
                    auxDescricaoOrigem = banco.retornaRead().GetString(2);
                    auxIdPedido = banco.retornaRead().GetString(3);
                    auxIdCliente = banco.retornaRead().GetString(4);
                    lbDestinatarioUFNotaFiscal.Text = banco.retornaRead().GetString(6);
                    auxCondPgto = banco.retornaRead().GetString(7);
                    auxIdFuncionario = banco.retornaRead().GetString(8);
                    auxCondicaoMetodo = banco.retornaRead().GetString(9);
                    flagSemaforo = 0;
                }
            }
            catch (Exception erro)
            { MessageBox.Show(erro.Message, "Caro Usuário"); auxConsistencia = 1; }
            finally
            {
                banco.fechaConexao();
                if (auxConsistencia == 0)
                {
                    tslVendedor.Text = tslVendedor.Text + ": " + auxIdFuncionario;
                    if (auxCondPgto != "" && Convert.ToDouble(auxCondPgto) > 0)
                    {
                        conector_find_finalizadora(auxCondPgto);
                    }
                    else if (auxCondicaoMetodo != "")
                    {
                        conector_find_finalizadora(auxCondicaoMetodo);
                    }
                }
            }
        }
        public void conector_find_pedidoNf()
        {
            string texto = "";
            texto = " SELECT ifnull(idPedido,'0') from nf where nf.nf=";
            texto += auxIdNf;
            try
            {
                banco.abreConexao();
                banco.singleTransaction(texto);
                if (auxIdNf != "")
                {
                    banco.procedimentoRead();
                    if (banco.retornaRead().Read() == true)
                    {
                        auxIdPedido = Convert.ToInt32(banco.retornaRead().GetString(0)).ToString();
                    }
                    else { auxIdPedido = "0"; }
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message, "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                banco.fechaConexao();
            }
        }
        public int conector_verifica_totalEmUsoPedido()
        {
            int count = 0;
            string texto = "";
            texto = " select count(idPedido) from nf where idPedido=";
            texto += auxIdPedido;
            try
            {
                banco.abreConexao();
                banco.singleTransaction(texto);
                banco.procedimentoRead();
                if (banco.retornaRead().Read() == true)
                {
                    count = Convert.ToInt32(banco.retornaRead().GetString(0));
                }
                else count = 0;
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message, "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {

                banco.fechaConexao();
            }
            return count;
        }
        public void conector_update_status_reserva(string flagSinal, string flagPedido, string flagStore) //SET a reserva como sinal de transito vermelho ninguem poderá ter acesso verde liberado com uma zona critica, assim aplicando o controle de concorrencia.
        {
            if (auxIdPedido != "" && auxIdPedido != null)
            {
                auxConsistencia = 0;
                try
                {
                    banco.abreConexao();
                    banco.singleTransaction("update pedido set sinal=?tipo where idPedido = ?pedido and idLoja=?loja");
                    banco.addParametro("?tipo", flagSinal);
                    banco.addParametro("?pedido", flagPedido);
                    banco.addParametro("?loja", flagStore);
                    banco.procedimentoRead();

                }
                catch (Exception erro)
                {
                    MessageBox.Show(erro.Message, "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    auxConsistencia = 1;
                }
                finally
                {
                    banco.fechaConexao();
                    if (auxConsistencia == 0)
                    {
                        conector_find_status_reserva();
                    }
                }

            }
        }
        protected void conector_between_financeiroXitens(string nr_reserva, string nr_loja) //Atualiza situacao da reserva quanto a reserva ou futura
        {
            auxConsistencia = 0;
            try
            {
                banco.abreConexao();
                banco.startTransaction("conector_between_financeiroXitens");
                banco.addParametro("findPedido", nr_reserva);
                banco.addParametro("findLoja", nr_loja);
                banco.procedimentoRead();
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message, "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                auxConsistencia = 1;
            }
            finally
            {
                banco.fechaConexao();
                if (auxConsistencia == 0)
                {
                    conector_find_status_reserva();
                    sinal = 0;
                }
            }
        }
        public void conector_find_status_reserva()
        {
            auxConsistencia = 0;
            string texto = "";
            texto = " select sinal, flagCaixa, final from pedido where idPedido=";
            texto += auxIdPedido;
            try
            {
                banco.abreConexao();
                banco.singleTransaction(texto);
                banco.procedimentoRead();
                if (banco.retornaRead().Read() == true)
                {
                    auxSinalPedido = banco.retornaRead().GetString(0);
                    auxCaixaPedido = banco.retornaRead().GetString(1);
                    auxFinalPedido = banco.retornaRead().GetString(2);
                }
                else
                {
                    auxSinalPedido = "vermelho";
                    auxCaixaPedido = "0";
                    auxFinalPedido = "5";
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message, "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information); auxConsistencia = 1;
            }
            finally
            {
                banco.fechaConexao();
                if (auxConsistencia == 0)
                {
                    typeEmissaoNota = 1;
                }
            }
        }
        protected void conector_carrega_variaveisNota()
        {
            auxConsistencia = 0;
            try
            {
                banco.abreConexao();
                banco.startTransaction("conector_find_totaisImpSaida");
                banco.addParametro("tipo", "2");
                banco.addParametro("find", auxIdNf);
                banco.procedimentoRead();
                if (banco.retornaRead().Read() == true)
                {
                    auxCondPgto = banco.retornaRead().GetString(28);
                }


            }
            catch (Exception erro)
            { MessageBox.Show(erro.Message, "Messagem do Sistema"); auxConsistencia = 1; }
            finally
            {
                banco.fechaConexao();
                if (auxConsistencia == 0)
                {
                    conector_find_finalizadora(auxCondPgto);
                }
            }
        }
        protected void conector_update_gravaSaida() 
        {
            auxConsistencia = 0;
            try
            {
                banco.abreConexao();
                banco.startTransaction("conector_update_gravaSaida");
                banco.addParametro("VartypeDoc", "0");
                banco.addParametro("chaveNota", auxIdNf);
                banco.addParametro("chavePedido", auxIdPedido == null ? "0" : auxIdPedido);
                banco.addParametro("typeEst", txtTypeNotaFiscal.Text);
                banco.addParametro("cx", auxIdTerminal);
                banco.procedimentoRead();
            }
            catch (Exception erro)
            { MessageBox.Show(erro.Message, "Messagem do Sistema"); auxConsistencia = 1; }
            finally
            {
                banco.fechaConexao();
                if (auxConsistencia == 0)
                {
                    MessageBox.Show("Nota atualizada com sucesso..! ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    somenteLeitura(false);
                    btnImprimiNotaFiscal.Enabled = true;
                    btnValidaNfeNotaFiscal.Enabled = true;
                    btnGeraNotaFiscal.Enabled = false;
                    mskNumberNotaFiscal.ReadOnly = true;
                    txtSerieNotaFiscal.ReadOnly = true;
                    txtFolhaNotaFiscal.ReadOnly = true;
                    txtTypeNotaFiscal.ReadOnly = true;
                }
            }
        }
        protected void conector_find_totaisImpSaida()
        {
            dgvNota.Rows.Clear();
            auxConsistencia = 0;
            try
            {
                banco.abreConexao();
                banco.startTransaction("conector_find_totaisImpSaida");
                banco.addParametro("tipo", "1");
                banco.addParametro("find", auxIdNf);
                banco.procedimentoSet();
            }
            catch (Exception erro)
            { MessageBox.Show(erro.Message, "Messagem do Sistema"); auxConsistencia = 1;}
            finally
            {
                if (auxConsistencia == 0)
                {
                    countRows = banco.retornaSet().Tables[0].DefaultView.Count;
                    countField = banco.retornaSet().Tables[0].Columns.Count;
                    if (countRows > 0)
                    {
                        dgvNota.AllowUserToAddRows = false;
                        for (i = 0; i < countRows; i++)
                        {
                            dgvNota.Rows.Add();
                            for (j = 0; j < countField; j++)
                            {
                                dgvNota.Rows[i].Cells[j].Value = Convert.ToString(banco.retornaSet().Tables[0].Rows[i][j]);
                            }

                        }
                    }
                    else
                    {
                        if (dgvNota.RowCount < 1)
                        {
                            dgvNota.Rows.Add();   
                        }
                    }
                }
                banco.fechaConexao();
            }
        }
        protected void conector_inc_nfinfortransporte()
        {
            auxConsistencia = 0;
            try
            {
                banco.abreConexao();
                banco.startTransaction("conector_inc_nfinfortransporte");
                banco.addParametro("inc_nf", auxIdNf);
                banco.addParametro("inc_uf", optionUfTransportadora);
                banco.addParametro("inc_qtty_volume", optionVolumeQtty);
                banco.addParametro("inc_especie", optionVolumeEspecie);
                banco.addParametro("inc_marca", optionVolumeMarca);
                banco.addParametro("inc_numeracao", optionVolumePesoNumber);
                banco.addParametro("inc_pesoliquido", optionVolumePesoLiquido);
                banco.addParametro("inc_pesoBruto", optionVolumePesoBruto);
                banco.addParametro("inc_idtransportadora", optionCodigoTransportadora);
                banco.addParametro("inc_retencaoBaseCalculo", optionBaseCalculoFrete);
                banco.addParametro("inc_retencaoAliquotaFrete", optionAliquotaFrete);
                banco.addParametro("inc_retencaoIcmsFrete", optionIcmsFrete);
                banco.addParametro("inc_retencaoValorFrete", optionValorTotalFrete);
                banco.addParametro("inc_retencaoCfopFrete", optionCFOPFrete);
                banco.addParametro("inc_isentoIcms", optionIsentoIcms);
                banco.procedimentoRead();
                if (banco.retornaRead().Read() == true)
                {
                    auxIdNfTransportadora = banco.retornaRead().GetString(0);
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message, "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                auxConsistencia = 1;
            }
            finally
            {
                banco.fechaConexao();
                if (auxConsistencia == 0)
                {
                    flagSemaforo = 1;
                    tbcNotaSaida.SelectedIndex = 1;
                }
            }
        }
        protected void conector_alt_nfinfortransporte()
        {
            auxConsistencia = 0;
            try
            {
                banco.abreConexao();
                banco.startTransaction("conector_alt_nfinfortransporte");
                banco.addParametro("newnfinfortransporte", auxIdNfTransportadora);
                banco.addParametro("newnf", auxIdNf);
                banco.addParametro("newuf", optionUfTransportadora);
                banco.addParametro("newqtty_volume", optionVolumeQtty);
                banco.addParametro("newespecie", optionVolumeEspecie);
                banco.addParametro("newmarca", optionVolumeMarca);
                banco.addParametro("newnumeracao", optionVolumePesoNumber);
                banco.addParametro("newpesoliquido", optionVolumePesoLiquido);
                banco.addParametro("newpesoBruto", optionVolumePesoBruto);
                banco.addParametro("newidtransportadora", optionCodigoTransportadora);
                banco.addParametro("newretencaoBaseCalculo", optionBaseCalculoFrete);
                banco.addParametro("newretencaoAliquotaFrete", optionAliquotaFrete);
                banco.addParametro("newretencaoIcmsFrete", optionIcmsFrete);
                banco.addParametro("newretencaoValorFrete", optionValorTotalFrete);
                banco.addParametro("newretencaoCfopFrete", optionCFOPFrete);
                banco.addParametro("newisentoIcms", optionIsentoIcms);
                banco.procedimentoRead();
                if (banco.retornaRead().Read() == true)
                {
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message, "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                auxConsistencia = 1;
            }
            finally
            {
                banco.fechaConexao();
                if (auxConsistencia == 0)
                {
                    flagSemaforo = 1;
                }
            }
        }
        protected void conector_alt_nf()
        {
            auxNrNota = mskNumberNotaFiscal.Text;
            auxNrNota = mskNumberNotaFiscal.Text.Replace(".", "");
            auxNrNota = mskNumberNotaFiscal.Text.Replace(",", "");
            auxNrNota = auxNrNota.Replace("-", "");
            auxConsistencia = 0;
            try
            {
                banco.abreConexao();
                banco.startTransaction("conector_alt_nf");
                banco.addParametro("newnf", auxIdNf);
                banco.addParametro("newloja", alwaysVariables.Store);
                banco.addParametro("newidcliente", auxIdCliente);
                banco.addParametro("newidparamentro", auxIdParamentro);
                banco.addParametro("newidtransportadora", optionCodigoTransportadora);
                banco.addParametro("newcfop", txtCFOPNotaFiscal.Text);
                banco.addParametro("newidFuncionario", auxIdFuncionario); //auxIdFuncionario);
                banco.addParametro("newidusuario", alwaysVariables.Usuario);
                banco.addParametro("newidpedido", auxIdPedido);
                banco.addParametro("newnr_nota", auxNrNota);
                banco.addParametro("newserie", txtSerieNotaFiscal.Text);
                banco.addParametro("newacrescimo", auxPorcentagemAcrescimo);
                banco.addParametro("newbaseicms", lbCalculoImpostoBaseCalculoNotaFiscal.Text);
                banco.addParametro("newbaseIcmsIsento", auxCalculoBaseIcmsIsento);
                banco.addParametro("newvalorIcmsSubstituicao", lbCalculoImpostoValorBaseIcmsSubstituicaoNotaFiscal.Text);
                banco.addParametro("newbaseCalculoIcmsSubstituicao", lbCalculoImpostoValorIcmsSubstituicaoNotaFiscal.Text);
                banco.addParametro("newbaseIPI", auxCalculoBaseIPI);
                banco.addParametro("newbaseCofins", auxCalculoBaseCofins);
                banco.addParametro("newbasePis", auxCalculoBasePis);
                banco.addParametro("newemissao", String.Format("{0:yyyyMMdd}", dtpEmissaoNotaFiscal.Value));
                banco.addParametro("newsaida", String.Format("{0:yyyyMMdd}", dtpSaidaNotaFiscal.Value));
                banco.addParametro("newalteracao", String.Format("{0:yyyyMMdd}", DateTime.Now));
                banco.addParametro("newhora", "0000"); //Provisorio
                banco.addParametro("newdesconto", auxPorcentagemDesconto);
                banco.addParametro("newuf", lbDestinatarioUFNotaFiscal.Text);
                banco.addParametro("newitens", auxCalculoTotalItens);
                banco.addParametro("newseguro", auxCalculoSeguro);
                banco.addParametro("newfrete", lbCalculoImpostoValorFreteNotaFiscal.Text);
                banco.addParametro("newtypeFrete", auxModalidadeFrete.ToString());
                banco.addParametro("newvalorIcms", lbCalculoImpostoValorIcmsNotaFiscal.Text);
                banco.addParametro("newvalorIpi", lbCalculoImpostoIPINotaFiscal.Text);
                banco.addParametro("newvalorPis", auxCalculoValorPis);
                banco.addParametro("newvalorCofins", auxCalculoValorCofins);
                banco.addParametro("newacrecismoValor", auxCalculoValorAcrescimo);
                banco.addParametro("newdescontoValor", lbCalculoImpostoDescontoNotaFiscal.Text);
                banco.addParametro("newvalorTotalLiquido", auxCalculoValorTotalLiquido);
                banco.addParametro("newvalorTotalNota", lbCalculoImpostoValorTotalDaNotaFiscal.Text);
                banco.addParametro("newvalorTotalProdutos", lbCalculoImpostoValorTotalProdutoNotaFiscal.Text);
                banco.addParametro("newvolumes", auxTotalVolumeNf);
                banco.addParametro("newpeso", lbTransportePesoBrutoNotaFiscal.Text);
                banco.addParametro("newcontribuicaoSocial", auxCalculoContribucaoSocial);
                banco.addParametro("newquantidadePedido", auxQttyPedido);
                banco.addParametro("newquantidadeRecebida", auxQttyRecebida);
                banco.addParametro("newimpresso", "0"); //provisorio  == false ? "0" : "1"
                banco.addParametro("newnr_impressao", "0"); //provisorio
                banco.addParametro("newmodNotaFiscal", auxIdModeloNotaFiscal);
                banco.addParametro("newidSituacaoFiscal", auxIdSituacaoFiscal);
                banco.addParametro("newemitiNfe", "0"); //provisorio recoferir setting
                banco.addParametro("newtypenf", txtTypeNotaFiscal.Text);
                banco.addParametro("newMsg01", txtMsg01NotaFiscal.Text);
                banco.addParametro("newMsg02", txtMsg02NotaFiscal.Text);
                banco.addParametro("newMsg03", txtMsg03NotaFiscal.Text);
                banco.addParametro("newvalorTotaServico", lbISSQNNotaFiscal.Text);
                banco.addParametro("newnr_nota_entrada", auxIdEntrada);
                banco.addParametro("newserie_entrada", txtSerieNfEntradaNotaSaida.Text);
                banco.addParametro("newrestituicao", "n"); //provisorio
                banco.addParametro("newgeraDanfe", flagGeraDanfe); //provisorio
                banco.addParametro("newcondPgto", auxCondPgto);
                banco.procedimentoRead();
                if (banco.retornaRead().Read() == true)
                {

                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message, "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                auxConsistencia = 1;
            }
            finally
            {
                banco.fechaConexao();
                if (auxConsistencia == 0)
                {
                    if (Convert.ToDouble(auxNrNota) == 0)
                    {
                        conector_update_gravaSaida();
                    }
                    flagSemaforo = 1;
                    tbcNotaSaida.SelectedIndex = 1;
                    txtCodigoItensNotaFiscal.Select();
                    conector_find_itemNfSaidaTotais();
                    conector_find_totaisImpSaida();
                    switch (auxModalidadeFrete)
                    {
                        case 9:
                            lbTransporteRazaoNotaFiscal.Text = "";
                            lbCodigoAnttNotaFiscal.Text = "";
                            lbPlacaVeiculoNotaFiscal.Text = "";
                            lbTransporteUfNotaFiscal.Text = "";
                            lbUfVeiculoNotaFiscal.Text = "";
                            lbTransporteCNPJNotaFiscal.Text = "";
                            lbTransporteINSNotaFiscal.Text = "";
                            lbTransporteMunicipioNotaFiscal.Text = "";
                            lbTransporteLogradouroNotaFiscal.Text = "";
                            lbTransporteQuantidadeNotaFiscal.Text = "";
                            lbTransporteEspecieNotaFiscal.Text = "";
                            lbTransporteNumeroNotaFiscal.Text = "";
                            lbTransporteLogradouroNotaFiscal.Text = "";
                            break;
                        default:
                            conector_alt_nfinfortransporte();
                            if (auxIdNfTransportadora != "")
                            {
                                conector_alt_nfInforVeiculo();
                            }
                            break;
                    }
                }
            }
        }
        protected void conector_inc_nfInforVeiculo()
        {
            auxConsistencia = 0;
            try
            {
                banco.abreConexao();
                banco.startTransaction("conector_inc_nfInforVeiculo");
                banco.addParametro("inc_nf", auxIdNf);
                banco.addParametro("inc_nfInforTransporte", auxIdNfTransportadora);
                banco.addParametro("inc_uf", auxGridUFVeiculo);
                banco.addParametro("inc_placa", optionPlacaVeiculo);
                banco.addParametro("inc_rntc", optionAnttVeiculo);
                banco.addParametro("inc_idveiculo", optionIdVeiculo);
                banco.procedimentoRead();
                if (banco.retornaRead().Read() == true)
                {
                    auxIdNfVeiculo = banco.retornaRead().GetString(0);
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message, "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                auxConsistencia = 1;
            }
            finally
            {
                banco.fechaConexao();
                if (auxConsistencia == 0)
                {
                    flagSemaforo = 1;
                    tbcNotaSaida.SelectedIndex = 1;
                }
            }
        }
        protected void conector_alt_nfInforVeiculo()
        {
            auxConsistencia = 0;
            try
            {
                banco.abreConexao();
                banco.startTransaction("conector_alt_nfInforVeiculo");
                banco.addParametro("newnfInforVeiculo", auxIdNfVeiculo);
                banco.addParametro("newnf", auxIdNf);
                banco.addParametro("newnfInforTransporte", auxIdNfTransportadora);
                banco.addParametro("newuf", auxGridUFVeiculo);
                banco.addParametro("newplaca", optionPlacaVeiculo);
                banco.addParametro("newrntc", optionAnttVeiculo);
                banco.addParametro("newidveiculo", optionIdVeiculo);
                banco.procedimentoRead();
                if (banco.retornaRead().Read() == true)
                {
                    auxIdNfVeiculo = banco.retornaRead().GetString(0);
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message, "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                banco.fechaConexao();
                if (auxConsistencia == 0)
                {
                    flagSemaforo = 1;
                    tbcNotaSaida.SelectedIndex = 1;
                }
            }
        }
        //#################################################### End Banco #################################################################
        //#################################################### Controle single objetos ###################################################
        private void conector_valida_item(string item, int type)
        {
            if (cmbNaturezaOperacaoNotaFiscal.Text != "")
            {
                if (txtDescricaoFormaPgtoNotaFiscal.Text != "")
                {
                    if (auxIdNf != "" && Convert.ToDouble(auxIdNf)> 0)
                    {
                        txtCodigoItensNotaFiscal.Text = item;

                        int count = conector_verifica(txtCodigoItensNotaFiscal.Text == "" ? "0" : txtCodigoItensNotaFiscal.Text);

                        if (optionflagForceNfOrigem == "0")
                        {
                            if (txtCodigoItensNotaFiscal.Text != "")
                            {
                                conector_find_codBarra(txtCodigoItensNotaFiscal.Text);

                                if (optionFlagRepetItemFaturamento == "1")
                                {
                                    if ((count >= 1) && (txtCodigoItensNotaFiscal.ReadOnly == false) && type == 1)
                                    {
                                        MessageBox.Show("Produto existe nesta nota!", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        txtCodigoItensNotaFiscal.Clear();
                                        txtCodigoItensNotaFiscal.Select();
                                        zeraObjItem();
                                        zeraVariavelRecalculo();
                                    }
                                    else { conector_find_produto(txtCodigoItensNotaFiscal.Text); }
                                }
                                else
                                {
                                    conector_find_produto(txtCodigoItensNotaFiscal.Text);
                                }
                            }
                        }
                        else
                        {
                            if (txtNumeroNfEntradaNotaSaida.Text != "")
                            {
                                if (txtCodigoItensNotaFiscal.Text != "")
                                {
                                    conector_find_codBarra(txtCodigoItensNotaFiscal.Text);

                                    if (optionFlagRepetItemFaturamento == "1")
                                    {
                                        if ((count >= 1) && (txtCodigoItensNotaFiscal.ReadOnly == false))
                                        {
                                            MessageBox.Show("Produto existe nesta nota!", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            txtCodigoItensNotaFiscal.Clear();
                                            txtCodigoItensNotaFiscal.Select();
                                            zeraObjItem();
                                            zeraVariavelRecalculo();
                                        }
                                        else { conector_find_produto(txtCodigoItensNotaFiscal.Text); }
                                    }
                                    else
                                    {
                                        conector_find_produto(txtCodigoItensNotaFiscal.Text);
                                    }
                                }
                            }
                            else
                            {
                                zeraObjItem();
                                MessageBox.Show("Nota fiscal de origem obrigatória e invalida!", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtNumeroNfEntradaNotaSaida.Clear();
                                tbcNotaSaida.SelectedIndex = 2;
                                txtNumeroNfEntradaNotaSaida.Select();

                            }
                        }

                    }
                    else
                    {
                        MessageBox.Show("Cabeçalho incompleto; pressione a tecla [F10 - Gera Cabeçalho] - para validar o cabeçalho!", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tbcNotaSaida.SelectedIndex = 0;
                    }
                }
                else
                {
                    MessageBox.Show("Forma de pagamento não definida!", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tbcNotaSaida.SelectedIndex = 0;
                    txtDescricaoFormaPgtoNotaFiscal.Select();
                }
            }
            else
            {
                MessageBox.Show("Natureza da operação não definida!", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tbcNotaSaida.SelectedIndex = 0;
                cmbNaturezaOperacaoNotaFiscal.Select();
            }
        }
        private void conector_carrega_pedido()
        {
            cmbNaturezaOperacaoNotaFiscal.CausesValidation = false;
            int x = 0;

            if (optionAtualizaComNfFaturamento == "1")
            {
                if (auxIdPedido == "" || auxIdPedido == "0" || auxIdPedido == null)
                {
                    pesquisaPedido findPedido = new pesquisaPedido();
                    if (findPedido.ShowDialog(this) == DialogResult.OK)
                    {
                        auxIdPedido = findPedido.IdPedido;
                        conector_find_pedido(findPedido.IdPedido); // variaveis do contrutor () - conector_obj_gera_notaFiscal_vs_reserva
                        x = conector_verifica_totalEmUsoPedido();
                        if (x == 0)
                        {
                            conector_find_status_reserva(); //carrega status e configurações do pedido
                            if (auxSinalPedido == "vermelho")
                            {
                                MessageBox.Show("Pedido travado por outro usuario, verifique a sua situação.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                auxIdPedido = null;
                            }
                            else
                            {
                                if (optionRestrigeFinanceiroFaturamento == "1")
                                {
                                    if (auxCaixaPedido == "1")
                                    {
                                        typeEmissaoNota = 1;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Pedido não passou pelo caixa, verifique o faturamento.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        typeEmissaoNota = 0;
                                        auxIdPedido = null;
                                    }
                                }
                                else
                                {
                                    /*if (optionRestrigeFinanceiroFaturamento == "0" && auxFinalPedido == "2")
                                    {
                                        conector_between_financeiroXitens(auxIdPedido, auxIdLoja);
                                    }*/

                                    if (auxFinalPedido == "3" || auxFinalPedido == "2")
                                    {
                                        if (typeEmissaoNota == 1)
                                        {
                                            conector_obj_gera_notaFiscal_vs_reserva(auxDescricaoFaturamento, auxDescricaoDestino, auxDescricaoOrigem, auxIdPedido, auxIdCliente, alwaysVariables.Store, "9");
                                        }
                                        //if (auxFinalPedido != "2") //Se pedido Faturado e emitiu a nota muda o status para finalizado
                                        //{
                                        if (sinal == 1) //gerou a nota
                                        {
                                            conector_between_financeiroXitens(auxIdPedido, auxIdLoja);
                                        }
                                        //}
                                    }
                                    else
                                    {
                                        MessageBox.Show("Situação da reserva impossibilita a emissão de uma nota fiscal, verifique o seu status.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        auxIdPedido = null;
                                    }
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Pedido encontrasse em uso por outra nota fiscal emitida impossível carrega-lô.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            auxIdPedido = null;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Nota possui pedido carregado, impossivel corregar novamente.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Operação não configurada para carregar a reservas, com isso insira os itens manualmente.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            cmbNaturezaOperacaoNotaFiscal.CausesValidation = true;
        }
        private void conector_obj_salve_nf()
        {
            auxConsistencia = 0;
            if (txtDescricaoItensNotaFiscal.Text == "")
            {
                if (cmbNaturezaOperacaoNotaFiscal.Text != "")
                {
                    if (txtCFOPNotaFiscal.Text != "")
                    {
                        if (lbDestinatarioRazaoNotaFiscal.Text != "")
                        {
                            if (lbDestinatarioUFNotaFiscal.Text != "")
                            {
                                if (lbDestinatarioCNPJNotaFiscal.Text != "")
                                {
                                    if (txtDescricaoFormaPgtoNotaFiscal.Text != "" && optionForcePagamentoFaturamento == "1")
                                    {
                                        if (mskNumberNotaFiscal.Text != "" || Convert.ToDouble(mskNumberNotaFiscal.Text) > 0)
                                        {
                                            if (flagSemaforo == 0)
                                            {
                                                conector_inc_nf();
                                            }
                                            else
                                            {
                                                conector_update_nf();
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Nota fiscal inserida, confira os impostos e atualize, impossível prosseguir! ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            tbcNotaSaida.SelectedIndex = 0;
                                            auxConsistencia = 1;
                                        }
                                    }
                                    else
                                    {
                                        if (txtDescricaoFormaPgtoNotaFiscal.Text == "")
                                        {
                                            if ((MessageBox.Show("Deseja inserir forma de pagamento não definida para prosseguir? ", "Caro Usúario", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes) && txtDescricaoFormaPgtoNotaFiscal.Text == "")
                                            {
                                                tbcNotaSaida.SelectedIndex = 0;
                                                auxConsistencia = 1;
                                            }
                                            else
                                            {
                                                if (mskNumberNotaFiscal.Text != "" || Convert.ToDouble(mskNumberNotaFiscal.Text) > 0)
                                                {
                                                    if (flagSemaforo == 0)
                                                    {
                                                        conector_inc_nf();
                                                    }
                                                    else
                                                    {
                                                        conector_update_nf();
                                                    }
                                                }
                                                else
                                                {
                                                    MessageBox.Show("Nota fiscal inserida, confira os impostos e atualize, impossível prosseguir! ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                    tbcNotaSaida.SelectedIndex = 0;
                                                    auxConsistencia = 1;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (mskNumberNotaFiscal.Text != "" || Convert.ToDouble(mskNumberNotaFiscal.Text) > 0)
                                            {
                                                if (flagSemaforo == 0)
                                                {
                                                    conector_inc_nf();
                                                }
                                                else
                                                {
                                                    conector_update_nf();
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show("Nota fiscal inserida, confira os impostos e atualize, impossível prosseguir! ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                tbcNotaSaida.SelectedIndex = 0;
                                                auxConsistencia = 1;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Documento de identificação incoerente 'CNPJ ou CPF ou IE', impossivel prosseguir! ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    auxConsistencia = 1;
                                }

                            }
                            else
                            {
                                MessageBox.Show("Cliente sem a federação a algum estado, impossível prosseguir! ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                auxConsistencia = 1;
                                cmbUnidadeDefaultItensNotaFiscal.Select();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Cliente Inválido, impossivel prosseguir! ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            auxConsistencia = 1;
                            btnConsultaClienteNotaFiscal.Select();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Nota fiscal não pode ser gerada sem CFOP, impossivel prosseguir! ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCFOPItensNotaFiscal.Select();
                        auxConsistencia = 1;
                    }
                }
                else
                {
                    MessageBox.Show("Natureza da operação inválida, impossivel prosseguir! ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbNaturezaOperacaoNotaFiscal.Select();
                    auxConsistencia = 1;
                }
            }
            else
            {
                MessageBox.Show("Possui um item carregado, verifique! ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tbcNotaSaida.SelectedIndex = 1;
                txtQttyComercialTributavelItensNotaFiscal.Select();
                auxConsistencia = 1;
            }
        }
        private void conector_update_nf()
        {
            Boolean valida = true;
            if (optionAtualizaComNfFaturamento == "1")
            {
                conector_find_pedidoNf();
                if (Convert.ToInt32(auxIdPedido) > 0)
                {
                    valida = true;
                }
                else
                {
                    MessageBox.Show("Tipo de operação exige uma reserva feita pelo terminal de venda. ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    valida = false;
                }
            }
            else
            {
                valida = true;
            }
            int count = conector_verifica("0");
            if (valida == true)
            {
                if (flagSemaforo == 0)
                {
                    if (MessageBox.Show("Nota fiscal ainda não foi gravada no banco de dados, deseja confirma-la.", "Caro Usúario", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        conector_obj_salve_nf();
                        if (auxConsistencia == 0)
                        {
                            MessageBox.Show("Esse tipo de nota exigem a inclusão de itens para sua geração propriamente dita! ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            tbcNotaSaida.SelectedIndex = 1;
                        }
                    }
                }
                else
                {
                    if (optionFlagParamentro != "w") //Diferente de serviço
                    {
                        if (count > 0)
                        {
                            flagGeraDanfe = "s";
                            conector_alt_nf();
                            if (optionFlagParamentro == "t")
                            {
                                if (optionFlagParamentro == "t" && Convert.ToDouble(mskNumberNotaFiscal.Text) > 0)
                                {
                                    if (optionIdOperacaoEntrada != "" && Convert.ToDouble(optionIdOperacaoEntrada) > 0)
                                    {
                                        string test = "";
                                        bool valida1 = false;
                                        bool modalidade = false;
                                        if (auxModalidadeFrete == 2)
                                        {
                                            modalidade = true;
                                        }
                                        else
                                        {
                                            modalidade = false;
                                        }
                                        if (Convert.ToDecimal(lbCalculoImpostoValorIcmsSubstituicaoNotaFiscal.Text.Replace(",", ".")) > 0)
                                        {
                                            valida1 = true;
                                        }
                                        else
                                        {
                                            valida1 = false;
                                        }
                                        conector_carrega_operacaoEntrada(optionIdOperacaoEntrada);
                                        if (optionUfEstado == "MG")
                                        {
                                            test = optionOEInCfopSugestãoOperacaoEntrada;
                                        }
                                        else
                                        {
                                            test = optionOEOnCfopSugestãoOperacaoEntrada;
                                        }
                                        string ipi="0.0";
                                        if (Convert.ToDecimal(lbCalculoImpostoIPINotaFiscal.Text.Replace(",",".")) > 0)
                                        {
                                            ipi = lbCalculoImpostoValorTotalProdutoNotaFiscal.Text.Replace(",", ".");
                                        }
                                        else
                                        {
                                            ipi = "0.00";
                                        }
                                        
                                       // entradaItensNotaEntrada findEntradaNota = new entradaItensNotaEntrada(auxIdNf,auxIdLoja, optionIdOperacaoEntrada, auxIdCliente, test, "0", mskNumberNotaFiscal.Text, txtSerieNotaFiscal.Text, lbCalculoImpostoValorTotalProdutoNotaFiscal.Text.Replace(",", "."), lbCalculoImpostoValorTotalDaNotaFiscal.Text.Replace(",", "."), optionLojaCgc, dtpEmissaoNotaFiscal.Value, lbCalculoImpostoDescontoNotaFiscal.Text.Replace(",", "."),ipi, lbCalculoImpostoValorBaseIcmsSubstituicaoNotaFiscal.Text.Replace(",", "."), lbCalculoImpostoIPINotaFiscal.Text.Replace(",", "."), lbCalculoImpostoValorIcmsSubstituicaoNotaFiscal.Text.Replace(",", "."), modalidade, lbEmitenteRazaoNotaFiscal.Text, valida1, lbCalculoImpostoBaseCalculoNotaFiscal.Text.Replace(",", "."), lbCalculoImpostoValorIcmsNotaFiscal.Text.Replace(",", "."), 0, auxIdSituacaoFiscal, auxIdModeloNotaFiscal, auxIdCodigoFiscal);
                                        //rever entradaItensNotaEntrada findEntradaNota = new entradaItensNotaEntrada(auxIdNf, auxIdCliente, optionIdOperacaoEntrada, auxIdLoja, test, "0", mskNumberNotaFiscal.Text, txtSerieNotaFiscal.Text, lbCalculoImpostoValorTotalProdutoNotaFiscal.Text.Replace(",", "."), lbCalculoImpostoValorTotalDaNotaFiscal.Text.Replace(",", "."), optionLojaCgc, dtpEmissaoNotaFiscal.Value, lbCalculoImpostoDescontoNotaFiscal.Text.Replace(",", "."), ipi, lbCalculoImpostoValorBaseIcmsSubstituicaoNotaFiscal.Text.Replace(",", "."), lbCalculoImpostoIPINotaFiscal.Text.Replace(",", "."), lbCalculoImpostoValorIcmsSubstituicaoNotaFiscal.Text.Replace(",", "."), modalidade, lbDestinatarioRazaoNotaFiscal.Text, valida1, lbCalculoImpostoBaseCalculoNotaFiscal.Text.Replace(",", "."), lbCalculoImpostoValorIcmsNotaFiscal.Text.Replace(",", "."), 0, auxIdSituacaoFiscal, auxIdModeloNotaFiscal, auxIdCodigoFiscal);
                                    }
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Quantidade de item invalida para geração da nota fiscal, favor rever a situação! ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            tbcNotaSaida.SelectedIndex = 1;
                        }

                    }
                }
            }
        }
        private void conector_obj_removeItensNota()
        {
            if (cmbNaturezaOperacaoNotaFiscal.Text != "")
            {
                if (txtCFOPNotaFiscal.Text != "")
                {
                    if (lbDestinatarioRazaoNotaFiscal.Text != "")
                    {
                        if (lbDestinatarioUFNotaFiscal.Text != "")
                        {
                            if (lbDestinatarioCNPJNotaFiscal.Text != "")
                            {
                                ataque = 0;
                                if (auxIdNf != "" && auxIdNf != "0")
                                {
                                    if (auxIdProduto != "")
                                    {
                                        flagRetornoTotalItens = 0;
                                        conector_del_itemNfSaida();
                                        conector_find_itemNfSaida("1");
                                        zeraObjItem();
                                        conector_find_itemNfSaidaTotais();
                                        txtCodigoItensNotaFiscal.Select();
                                        tbcNotaSaida.SelectedIndex = 1;
                                        auxIdProduto = ""; //elimina a possibilidade escolha null.
                                    }
                                    else
                                    {
                                        MessageBox.Show("Item não selecionado, impossivel prosseguir! ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        tbcNotaSaida.SelectedIndex = 1;
                                        dgvItensNotaFiscal.Select();
                                    }
                                    
                                }
                                else
                                {
                                    MessageBox.Show("Informações incompletas para remoção do item, impossivel prosseguir! ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    tbcNotaSaida.SelectedIndex = 0;
                                }
                                

                                ataque = 1;
                            }
                            else
                            {
                                MessageBox.Show("Documento de identificação incoerente 'CNPJ ou CPF ou IE', impossivel prosseguir! ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }

                        }
                        else
                        {
                            MessageBox.Show("Cliente sem a federação a algum estado, impossível prosseguir! ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Cliente Inválido, impossivel prosseguir! ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Nota fiscal não pode ser gerada sem CFOP, impossivel prosseguir! ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Natureza da operação inválida, impossivel prosseguir! ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        void conector_obj_salve_item()
        {
            if (auxIdNf != "")
            {
                if (txtCodigoItensNotaFiscal.Text != "" && Convert.ToDouble(txtCodigoItensNotaFiscal.Text) > 0)
                {
                    if (txtQttyComercialTributavelItensNotaFiscal.Text != "" && txtQttyComercialTributavelItensNotaFiscal.Text != "0.00" && Convert.ToDouble(txtQttyComercialTributavelItensNotaFiscal.Text) > 0)
                    {
                        if (txtCFOPItensNotaFiscal.Text != "" && Convert.ToDouble(txtCFOPItensNotaFiscal.Text) > 0)
                        {
                            if (cmbUnidadeDefaultItensNotaFiscal.Text != "")
                            {
                                if (auxIdNf != "")
                                {
                                    if (flagSemaforoItem == 0)
                                    {
                                        conector_inc_nfItem();
                                        conector_find_itemNfSaidaTotais();
                                    }
                                    else
                                    {
                                        conector_alt_nfItem();
                                        conector_find_itemNfSaidaTotais();
                                    }
                                }
                                else
                                {
                                    tbcNotaSaida.SelectedIndex = 0;
                                    MessageBox.Show("Cabeçalho da nota incompleto, pressione F10 para validar! ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Tipo de unidade padrão não definido, impossível prosseguir! ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Codigo fiscal da operação inválido, impossível prosseguir! ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtCFOPItensNotaFiscal.Select();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Quantidade inválida, impossível prosseguir! ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtQttyComercialTributavelItensNotaFiscal.Select();
                    }

                }
                else
                {
                    MessageBox.Show("Codigo inválido, impossivel prosseguir! ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCodigoItensNotaFiscal.Select(); tbcTributosItemNota.SelectedIndex = 0;
                }
            }
            else
            {
                if (MessageBox.Show("Deseja validar o cabeçalho da nota antes de salvar?", "Caro Usúario", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    conector_obj_salve_nf();
                }
                else
                {
                    MessageBox.Show("Nota não pode ser salva,valide a nota fiscal pressionando a tecla [F10],\n devido ao cabeçalho da nota estar incomplento! ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    zeraObjItem();
                    tbcNotaSaida.SelectedIndex = 0;
                }
            }
        }
        void conector_find_obj_cliente()
        {
            int retorno;
            retorno = 0;
            typePepleo = new insertTypePepleo(auxIdLoja, auxIdUser, auxIdTerminal);
            if (typePepleo.ShowDialog(this) == DialogResult.OK)
            {
                retorno = typePepleo.GridLibera;
                switch (retorno)
                {
                    case 0:
                        //this.Hide();
                        consulta = new consultaCliente(auxIdLoja, auxIdUser, Convert.ToString(auxIdTerminal));
                        if (consulta.ShowDialog(this) == DialogResult.OK)
                        {
                            auxIdCliente = consulta.GridCodigo;
                            auxTipoPessoa = consulta.GridTypePessoa;
                            auxTypeAtividade = "1";

                            if ((consulta.GridTypePessoa == 1) || (consulta.GridTypePessoa == 3))
                            {
                                lbDestinatarioRazaoNotaFiscal.Text = consulta.GridNome;
                            }
                            else { lbDestinatarioRazaoNotaFiscal.Text = consulta.GridRazao; }
                        }
                        if (lbDestinatarioRazaoNotaFiscal.Text != "")
                        {
                            conector_carrega_cliente(auxIdCliente);
                        }
                        //this.Close();
                        break;
                    case 1:
                        consultaFornecedor = new pesquisaFornecedor();
                        if (consultaFornecedor.ShowDialog(this) == DialogResult.OK)
                        {
                            auxIdCliente = consultaFornecedor.Gridchave;
                            auxTypeAtividade = "2";
                            auxTipoPessoa = consultaFornecedor.TipoPessoa;

                            if ((consultaFornecedor.TipoPessoa == 1) || (consultaFornecedor.TipoPessoa == 3))
                            {
                                lbDestinatarioRazaoNotaFiscal.Text = consultaFornecedor.GridNomeFisica;
                            }
                            else { lbDestinatarioRazaoNotaFiscal.Text = consultaFornecedor.GridRazao; }
                            conector_carrega_cliente(auxIdCliente);
                        }
                        
                        
                        break;
                    case 3:
                        consultaLoja = new pesquisaLoja();
                        if (consultaLoja.ShowDialog(this) == DialogResult.OK)
                        {
                            auxIdCliente = consultaLoja.GridCodigo;
                            auxTypeAtividade = "7";
                            auxTipoPessoa = 2;
                            lbDestinatarioRazaoNotaFiscal.Text = consultaLoja.GridRazao;
                            conector_carrega_cliente(auxIdCliente);
                        }
                        break;
                }
            }
        }
        void conector_obj_gera_notaFiscal_vs_reserva(string descricaoFaturamento, string nome, string razao, string nr_reserva , string destino, string origem, string flagFrete)
        {
            int testCliente;

            //Item 1 - Carrega as configurações da origem
            conector_carrega_loja(origem);
            //Item 2 - Carrega as configurações do pedido
            cmbNaturezaOperacaoNotaFiscal.Text = descricaoFaturamento;
            //Item 3 - Verifica type de pessoa
            auxTipoPessoa = conector_verifica_typeCliente(destino);
            //Item 4 - Verifica se é cliente, loja ou fornecedor
            testCliente = conector_verifica_atividade(destino);
            switch (testCliente)
            {
                case 1: //Cliente
                            auxTypeAtividade = "1";
                            if ((auxTipoPessoa == 1) || (auxTipoPessoa == 3))
                            {
                                lbDestinatarioRazaoNotaFiscal.Text = nome;
                            }
                            else { lbDestinatarioRazaoNotaFiscal.Text = razao; }
                            if (lbDestinatarioRazaoNotaFiscal.Text != "")
                            {
                                conector_carrega_cliente(destino);
                            }          
                    break;
                case 7: //Loja
                    auxTypeAtividade = "7";
                    auxTipoPessoa = 2;
                    lbDestinatarioRazaoNotaFiscal.Text = razao;
                    conector_carrega_cliente(destino);
                    break;
                case 2: //Fornecedor
                    auxTypeAtividade = "2";
                    if (testCliente > 0)
                    {
                        auxTipoPessoa = testCliente;
                        lbDestinatarioRazaoNotaFiscal.Text = auxDescricaoOrigem;
                    }
                    else
                    {
                        auxTipoPessoa = consultaFornecedor.TipoPessoa;
                        if ((consultaFornecedor.TipoPessoa == 1) || (consultaFornecedor.TipoPessoa == 3))
                        {
                            lbDestinatarioRazaoNotaFiscal.Text = nome;
                        }
                        else { lbDestinatarioRazaoNotaFiscal.Text = razao; }
                    }
                    if (auxIdCliente != "")
                    {
                        conector_carrega_cliente(destino);
                    }
                    break;
            }
            //Item 5 - Gera a nota fiscal a partir do pedido
            if (cmbNaturezaOperacaoNotaFiscal.Text != "")
            {
                if (txtCFOPNotaFiscal.Text != "")
                {
                    if (lbDestinatarioRazaoNotaFiscal.Text != "")
                    {
                        if (lbDestinatarioUFNotaFiscal.Text != "")
                        {
                            if (lbDestinatarioCNPJNotaFiscal.Text != "")
                            {
                                if (flagSemaforo == 0)
                                {
                                    conector_gera_nfItem(nr_reserva, "1");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Documento de identificação incoerente 'CNPJ ou CPF ou IE', impossivel prosseguir! ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }

                        }
                        else
                        {
                            MessageBox.Show("Cliente sem a federação a algum estado, impossível prosseguir! ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Cliente inválido, impossivel prosseguir! ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Nota fiscal não pode ser gerada sem CFOP, impossivel prosseguir! ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Natureza da operação inválida, impossivel prosseguir! ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //Item 6 - Carrega parcial do cabeçalho da nota fiscal
            conector_find_itemNfSaidaTotais();
            //Item 7 - Atualiza o grid produto
                conector_find_itemNfSaida("1");
                dgvItensNotaFiscal.Select();
                txtCodigoItensNotaFiscal.ReadOnly = false;
                zeraObjItem();
                txtCodigoItensNotaFiscal.Select();
                tbcNotaSaida.SelectedIndex = 1;
                tbcTributosItemNota.SelectedIndex = 0;
        }

        void somenteLeitura(bool flag)
        {
            gpbInfoStore.Enabled = flag;
            gpbNotaFiscalOrigem.Enabled = flag;
            tbpDadosItemNotaFiscal.Enabled = flag;
            tbpIcmsItemNotaFiscal.Enabled = flag;
            txtDadosAdicionaisItemNota.Enabled = flag;
            tbpVeiculoNovoNotaFiscal.Enabled = flag;
            tbpArmamentoNotaFiscal.Enabled = flag;
            tbpMedicamentoNotaFiscal.Enabled = flag;
            tbpCombustivelNotaFiscal.Enabled = flag;
            tbpISSNQ.Enabled = flag;
            tbpTotalizadorImpostos.Enabled = flag;
            tbpIcmsItemNotaFiscal.Enabled = flag;
            tbpIpiItemNotaFiscal.Enabled = flag;
            tbcTributosItemNotaFiscal.Enabled = flag;
            tbcTributosItemNotaFiscal.Enabled = flag;
            tbpIrItemNotaFiscal.Enabled = flag;
            tbpISSQNItemNotaFiscal.Enabled = flag;
            btnPesquisaFormaPgtoNotaFiscal.Enabled = flag;
            cmbFreteNotaFiscal.Enabled = flag;
            btnCarregaPedidoNotaFiscal.Enabled = flag;
            btnConsultaClienteNotaFiscal.Enabled = flag;
            btnConsultaTransportadoraNotaFiscal.Enabled = flag;
            btnImprimiNotaFiscal.Enabled = flag;
            btnValidaNfeNotaFiscal.Enabled = flag;
            cmbNaturezaOperacaoNotaFiscal.Enabled = flag;
            txtCFOPNotaFiscal.Enabled = flag;
            btnPesquisaCFOPNotaFiscal.Enabled = flag;
            txtSerieNotaFiscal.Enabled = flag;
            txtFolhaNotaFiscal.Enabled = flag;
            mskNumberNotaFiscal.Enabled = flag;
            btnGeraNotaFiscal.Enabled = flag;
            txtTypeNotaFiscal.Enabled = flag;
            txtChaveNfeNotaFiscal.Enabled = flag;
            dtpEmissaoNotaFiscal.Enabled = flag;
            txtProtocoloNfeNotaFiscal.Enabled = flag;
            dtpSaidaNotaFiscal.Enabled = flag;
        }
        void zeraObj()
        {
            txtCFOPNotaFiscal.Clear();
            lbEmitenteRazaoNotaFiscal.Text = "";
            lbEmitenteBairroNotaFiscal.Text = "";
            lbEmitenteLagradouroNotaFiscal.Text = "";
            lbEmitenteMunicipioNotaFiscal.Text = "";
            lbEmitenteUfNotaFiscal.Text = "";
            lbEmitenteFoneNotaFiscal.Text = "";
            lbEmitenteCNPJNotaFiscal.Text = "";
            lbEmitenteCEPNotaFiscal.Text = "";
            txtTypeNotaFiscal.Clear();
            mskNumberNotaFiscal.Text = "000000000";
            txtFolhaNotaFiscal.Text = "1/1";
            txtSerieNotaFiscal.Clear();
            txtProtocoloNfeNotaFiscal.Clear();
            txtChaveNfeNotaFiscal.Clear();
            dtpValidacaoNfeNotaFiscal.Value = DateTime.Now;
            mskTimeNfeNotaFiscal.Clear();
            lbEmitenteINSNotaFiscal.Text = "";
            lbEmitenteINSSTNotaFiscal.Text = "";
            cmbNaturezaOperacaoNotaFiscal.SelectedIndex = -1;
            lbEmitenteCNPJNotaFiscal.Text = "";
            lbDestinatarioCEPNotaFiscal.Text = "";
            lbDestinatarioFoneNotaFiscal.Text = "";
            lbDestinatarioINSNotaFiscal.Text = "";
            lbDestinatarioUFNotaFiscal.Text = "";
            lbEmitenteINSSTNotaFiscal.Text = "";
            lbDestinatarioCNPJNotaFiscal.Text = "";
            lbDestinatarioRazaoNotaFiscal.Text = "";
            lbDestinatarioLogradouroNotaFiscal.Text = "";
            lbDestinatarioBairroNotaFiscal.Text = "";
            lbCalculoImpostoBaseCalculoNotaFiscal.Text = "0.00";
            lbCalculoImpostoValorIcmsNotaFiscal.Text = "0.00";
            lbCalculoImpostoValorIcmsSubstituicaoNotaFiscal.Text = "0.00";
            lbCalculoImpostoValorFreteNotaFiscal.Text = "0.00";
            lbCalculoImpostoValorDoSeguroNotaFiscal.Text = "0.00";
            lbCalculoImpostoDescontoNotaFiscal.Text = "0.00";
            lbCalculoImpostoDespesasAcessoriasNotaFiscal.Text = "0.00";
            lbCalculoImpostoIPINotaFiscal.Text = "0.00";
            dtpEmissaoNotaFiscal.Value = DateTime.Now;
            dtpSaidaNotaFiscal.Value = DateTime.Now;
            mskTimeNfNotaFiscal.Clear();
            lbCalculoImpostoValorTotalProdutoNotaFiscal.Text = "0.00";
            lbCalculoImpostoValorTotalDaNotaFiscal.Text = "0.00";
            lbCalculoImpostoValorBaseIcmsSubstituicaoNotaFiscal.Text = "0.00";
            cmbFreteNotaFiscal.SelectedIndex = -1;
            lbTransporteRazaoNotaFiscal.Text = "";
            lbCodigoAnttNotaFiscal.Text = "";
            lbPlacaVeiculoNotaFiscal.Text = "";
            lbTransporteUfNotaFiscal.Text = "";
            lbUfVeiculoNotaFiscal.Text = "";
            lbTransporteCNPJNotaFiscal.Text = "";
            lbTransporteINSNotaFiscal.Text = "";
            lbTransporteMunicipioNotaFiscal.Text = "";
            lbTransporteLogradouroNotaFiscal.Text = "";
            lbTransporteQuantidadeNotaFiscal.Text = "";
            lbTransporteEspecieNotaFiscal.Text = "";
            lbTransporteNumeroNotaFiscal.Text = "";
            lbTransporteLogradouroNotaFiscal.Text = "";
            btnPesquisaFormaPgtoNotaFiscal.Text = "";
            txtDescricaoFormaPgtoNotaFiscal.Text = "";
            lbISSQNNotaFiscal.Text = "";
            lbISSQNBaseCalculoNotaFiscal.Text = "";
            lbISSQNINSSTNotaFiscal.Text = "";
            lbEmitenteINSMunicipalNotaFiscal.Text = "";
            lbTransportePesoLiquidoNotaFiscal.Text = "";
            lbTransportePesoBrutoNotaFiscal.Text = "";

            txtCodigoItensNotaFiscal.Clear();
            //btnPesquisaItensNotaFiscal
            txtDescricaoItensNotaFiscal.Clear();
            txtCodigoNCMItensNotaFiscal.Clear();
            txtCFOPItensNotaFiscal.Clear();
            //btnPesquisaCFOPItensNotaFiscal
            cmbUnidadeDefaultItensNotaFiscal.SelectedIndex = -1;
            txtQttyComercialTributavelItensNotaFiscal.Text = "0.00";
            txtValorUnComercialTributarioItensNotaFiscal.Text = "0.00";
            txtTotalFreteItensNotaFiscal.Text = "0.00";
            txtOutrasDespesasAcessoriasItensNotaFiscal.Text = "0.00";
            txtDescontoItensNotaFiscal.Text = "0.00";
            txtTotalSeguroItensNotaFiscal.Text = "0.00";
            txtEANBarraItensNotaFiscal.Clear();
            txtValorBrutoItensNotaFiscal.Text = "0.00";
            txtIdPedidoItensNotaFiscal.Clear();
            cmbTypeEspecificoItensNotaFiscal.SelectedIndex = -1;
            txtValorBaseCalculoImportItensNotaFiscal.Text = "0.00";
            txtValorDespesasAduaneirasImportItensNotaFiscal.Text = "0.00";
            txtValorIOFImportItensNotaFiscal.Text = "0.00";
            txtValorImpostoImportItensNotaFiscal.Text = "0.00";
            cmbSituacaoTributariaCofinsItensNotaFiscal.SelectedIndex = -1;
            cmbTipoCalculoCofinsItensNotaFiscal.SelectedIndex = -1;
            cmbTipoCalculoStCofinsItensNotaFiscal.SelectedIndex = -1;
            txtValorBaseCalculoCofinsItensNotaFiscal.Text = "0.00";
            txtAliquotaPercentualCofinsItensNotaFiscal.Text = "0.00";
            txtAliquotaMoedaCofinsItensNotaFiscal.Text = "0.00";
            txtQttyVendidaCofinsItensNotaFiscal.Text = "0.00";
            txtValorCofinsItensNotaFiscal.Text = "0.00";
            txtValorCofinsStCofinsItensNotaFiscal.Text = "0.00";
            txtQttyVendidaStCofinsItensNotaFiscal.Text = "0.00";
            txtAliquotaMoedaStCofinsItensNotaFiscal.Text = "0.00";
            txtAliquotaPercentualStCofinsItensNotaFiscal.Text = "0.00";
            txtValorBaseCalculoStCofinsItensNotaFiscal.Text = "0.00";
            rbICMSItensNotaFiscal.Checked = true;
            rbISSNQItensNotaFiscal.Checked = false;
            cmbSituacaoTributariaPISItensNotaFiscal.SelectedIndex = -1;
            cmbTipoCalculoPISItensNotaFiscal.SelectedIndex = -1;
            cmbTipoCalculoSTPISItensNotaFiscal.SelectedIndex = -1;
            txtValorBaseCalculoPISItensNotaFiscal.Text = "0.00";
            txtAliquotaPorcentagemPISItensNotaFiscal.Text = "0.00";
            txtAliquotaMoedaPISItensNotaFiscal.Text = "0.00";
            txtQttyVendidaPISItensNotaFiscal.Text = "0.00";
            txtValorPISItensNotaFiscal.Text = "0.00";
            txtValorStPISItensNotaFiscal.Text = "0.00";
            txtQttyVendidaStPISItensNotaFiscal.Text = "0.00";
            txtAliquotaMoedaStPISItensNotaFiscal.Text = "0.00";
            txtAliquotaStPercentualPISItensNotaFiscal.Text = "0.00";
            txtValorBaseCalculoStPISItensNotaFiscal.Text = "0.00";
            cmbSituacaoTributariaIPIItensNotaFiscal.SelectedIndex = -1;
            txtCodigoEnquadramentoIPIItensNotaFiscal.Text = "0.00";
            txtQttySeloControleIPIItensNotaFiscal.Text = "0.00";
            txtValorIPIItensNotaFiscal.Text = "0.00";
            txtValorUnIPIItensNotaFiscal.Text = "0.00";
            txtQttyTotalUnDefaultIPIItensNotaFiscal.Text = "0.00";
            txtAliquotaIPIItensNotaFiscal.Text = "0.00";
            txtValorBaseCalculoIPIItensNotaFiscal.Text = "0.00";
            cmbTipoCalculoIPIItensNotaFiscal.SelectedIndex = -1;
            txtSeloControleIPIItensNotaFiscal.Text = "0.00";
            mskCNPJProdutorIPIItensNotaFiscal.Clear();
            txtClasseEnquadramentoIPIItensNotaFiscal.Text = "0.00";
            if (optionLojaIdPais == "30")
            {
                cmbTributoOrigemItensNotaFiscal.Text = "Nacional";
            }
            else
            {
                //rever paramentro de importação
            }
            cmbTributoSituacaoTributariaItensNotaFiscal.SelectedIndex = -1;
            cmbTributoRegimeItensNotaFiscal.SelectedIndex = -1;
            txtBaseIcmsItensNotaFiscal.Text = "0.00";
            txtAliquotaIcmsItensNotaFiscal.Text = "0.00";
            txtValorIcmsItensNotaFiscal.Text = "0.00";
            txtDadosAdicionaisCabecalhoNotaFiscal.Text = "0.00";
            txtAliquotaIcmsItensNotaFiscal.Text = "0.00";
        }
        void zeraObjItem()
        {
            flagSemaforoItem = 0;
            txtReducaoBaseCalculoItensNotaFiscal.Text = "0.00";
            txtAliquotaIcmsItensNotaFiscal.Text = "0.00";
            txtCodigoItensNotaFiscal.Clear();
            //btnPesquisaItensNotaFiscal
            txtDescricaoItensNotaFiscal.Clear();
            txtCodigoNCMItensNotaFiscal.Clear();
            txtCFOPItensNotaFiscal.Clear();
            //btnPesquisaCFOPItensNotaFiscal
            cmbUnidadeDefaultItensNotaFiscal.SelectedIndex = -1;
            txtQttyComercialTributavelItensNotaFiscal.Text = "0.00";
            txtValorUnComercialTributarioItensNotaFiscal.Text = "0.00";
            txtTotalFreteItensNotaFiscal.Text = "0.00";
            txtOutrasDespesasAcessoriasItensNotaFiscal.Text = "0.00";
            txtDescontoItensNotaFiscal.Text = "0.00";
            txtTotalSeguroItensNotaFiscal.Text = "0.00";
            txtEANBarraItensNotaFiscal.Clear();
            txtValorBrutoItensNotaFiscal.Text = "0.00";
            txtIdPedidoItensNotaFiscal.Clear();
            cmbTypeEspecificoItensNotaFiscal.SelectedIndex = -1;
            txtValorBaseCalculoImportItensNotaFiscal.Text = "0.00";
            txtValorDespesasAduaneirasImportItensNotaFiscal.Text = "0.00";
            txtValorIOFImportItensNotaFiscal.Text = "0.00";
            txtValorImpostoImportItensNotaFiscal.Text = "0.00";
            cmbSituacaoTributariaCofinsItensNotaFiscal.SelectedIndex = -1;
            cmbTipoCalculoCofinsItensNotaFiscal.SelectedIndex = -1;
            cmbTipoCalculoStCofinsItensNotaFiscal.SelectedIndex = -1;
            txtValorBaseCalculoCofinsItensNotaFiscal.Text = "0.00";
            txtAliquotaPercentualCofinsItensNotaFiscal.Text = "0.00";
            txtAliquotaMoedaCofinsItensNotaFiscal.Text = "0.00";
            txtQttyVendidaCofinsItensNotaFiscal.Text = "0.00";
            txtValorCofinsItensNotaFiscal.Text = "0.00";
            txtValorCofinsStCofinsItensNotaFiscal.Text = "0.00";
            txtQttyVendidaStCofinsItensNotaFiscal.Text = "0.00";
            txtAliquotaMoedaStCofinsItensNotaFiscal.Text = "0.00";
            txtAliquotaPercentualStCofinsItensNotaFiscal.Text = "0.00";
            txtValorBaseCalculoStCofinsItensNotaFiscal.Text = "0.00";
            rbICMSItensNotaFiscal.Checked = true;
            rbISSNQItensNotaFiscal.Checked = false;
            cmbSituacaoTributariaPISItensNotaFiscal.SelectedIndex = -1;
            cmbTipoCalculoPISItensNotaFiscal.SelectedIndex = -1;
            cmbTipoCalculoSTPISItensNotaFiscal.SelectedIndex = -1;
            txtValorBaseCalculoPISItensNotaFiscal.Text = "0.00";
            txtAliquotaPorcentagemPISItensNotaFiscal.Text = "0.00";
            txtAliquotaMoedaPISItensNotaFiscal.Text = "0.00";
            txtQttyVendidaPISItensNotaFiscal.Text = "0.00";
            txtValorPISItensNotaFiscal.Text = "0.00";
            txtValorStPISItensNotaFiscal.Text = "0.00";
            txtQttyVendidaStPISItensNotaFiscal.Text = "0.00";
            txtAliquotaMoedaStPISItensNotaFiscal.Text = "0.00";
            txtAliquotaStPercentualPISItensNotaFiscal.Text = "0.00";
            txtValorBaseCalculoStPISItensNotaFiscal.Text = "0.00";
            cmbSituacaoTributariaIPIItensNotaFiscal.SelectedIndex = -1;
            txtCodigoEnquadramentoIPIItensNotaFiscal.Text = "0.00";
            txtQttySeloControleIPIItensNotaFiscal.Text = "0.00";
            txtValorIPIItensNotaFiscal.Text = "0.00";
            txtValorUnIPIItensNotaFiscal.Text = "0.00";
            txtQttyTotalUnDefaultIPIItensNotaFiscal.Text = "0.00";
            txtAliquotaIPIItensNotaFiscal.Text = "0.00";
            txtValorBaseCalculoIPIItensNotaFiscal.Text = "0.00";
            cmbTipoCalculoIPIItensNotaFiscal.SelectedIndex = -1;
            txtSeloControleIPIItensNotaFiscal.Text = "0.00";
            mskCNPJProdutorIPIItensNotaFiscal.Clear();
            txtClasseEnquadramentoIPIItensNotaFiscal.Text = "0.00";
            cmbTributoSituacaoTributariaItensNotaFiscal.SelectedIndex = -1;
            txtBaseIcmsItensNotaFiscal.Text = "0.00";
            txtAliquotaIcmsItensNotaFiscal.Text = "0.00";
            txtValorIcmsItensNotaFiscal.Text = "0.00";
            txtDadosAdicionaisCabecalhoNotaFiscal.Text = "0.00";
            txtAliquotaIcmsSTItensNotaFiscal.Text = "0.00";
            txtReducaoBaseCalculoSTItensNotaFiscal.Text = "0.00";
            txtBaseIcmsSTItensNotaFiscal.Text = "0.00";
            txtValorIcmsSTItensNotaFiscal.Text = "0.00";
        }
        void statusObjIPI(Boolean flag)
        {
            cmbSituacaoTributariaIPIItensNotaFiscal.Enabled = flag;
            txtClasseEnquadramentoIPIItensNotaFiscal.Enabled = flag;
            txtClasseEnquadramentoIPIItensNotaFiscal.Enabled = flag;
            txtCodigoEnquadramentoIPIItensNotaFiscal.Enabled = flag;
            mskCNPJProdutorIPIItensNotaFiscal.Enabled = flag;
            txtQttySeloControleIPIItensNotaFiscal.Enabled = flag;
            txtSeloControleIPIItensNotaFiscal.Enabled = flag;
            cmbTipoCalculoIPIItensNotaFiscal.Enabled = flag;
            txtValorBaseCalculoIPIItensNotaFiscal.Enabled = flag;
            txtAliquotaIPIItensNotaFiscal.Enabled = flag;
            txtQttyTotalUnDefaultIPIItensNotaFiscal.Enabled = flag;
            txtValorIPIItensNotaFiscal.Enabled = flag;
            txtValorUnIPIItensNotaFiscal.Enabled = flag;
        }
        void statusObj(Boolean flag)
        {
            txtAliquotaIcmsItensNotaFiscal.ReadOnly = flag;
            txtCFOPNotaFiscal.ReadOnly = flag;
            txtTypeNotaFiscal.ReadOnly = flag;
            mskNumberNotaFiscal.ReadOnly = flag;
            txtFolhaNotaFiscal.ReadOnly = flag;
            txtSerieNotaFiscal.ReadOnly = flag;
            txtProtocoloNfeNotaFiscal.ReadOnly = flag;
            txtChaveNfeNotaFiscal.ReadOnly = flag;
            mskTimeNfeNotaFiscal.ReadOnly = flag;
            mskTimeNfNotaFiscal.ReadOnly = flag;
            txtDescricaoFormaPgtoNotaFiscal.Enabled = flag;
            txtCodigoItensNotaFiscal.ReadOnly = flag;
            txtDescricaoItensNotaFiscal.ReadOnly = flag;
            txtCodigoNCMItensNotaFiscal.ReadOnly = flag;
            txtCFOPItensNotaFiscal.ReadOnly = flag;
            txtQttyComercialTributavelItensNotaFiscal.ReadOnly = flag;
            txtValorUnComercialTributarioItensNotaFiscal.ReadOnly = flag;
            txtTotalFreteItensNotaFiscal.ReadOnly = flag;
            txtOutrasDespesasAcessoriasItensNotaFiscal.ReadOnly = flag;
            txtDescontoItensNotaFiscal.ReadOnly = flag;
            txtTotalSeguroItensNotaFiscal.ReadOnly = flag;
            txtEANBarraItensNotaFiscal.ReadOnly = flag;
            txtValorBrutoItensNotaFiscal.ReadOnly = flag;
            txtIdPedidoItensNotaFiscal.ReadOnly = flag;
            txtValorBaseCalculoImportItensNotaFiscal.ReadOnly = flag;
            txtValorDespesasAduaneirasImportItensNotaFiscal.ReadOnly = flag;
            txtValorIOFImportItensNotaFiscal.ReadOnly = flag;
            txtValorImpostoImportItensNotaFiscal.ReadOnly = flag;
            txtValorBaseCalculoCofinsItensNotaFiscal.ReadOnly = flag;
            txtAliquotaPercentualCofinsItensNotaFiscal.ReadOnly = flag;
            txtAliquotaMoedaCofinsItensNotaFiscal.ReadOnly = flag;
            txtQttyVendidaCofinsItensNotaFiscal.ReadOnly = flag;
            txtValorCofinsItensNotaFiscal.ReadOnly = flag;
            txtValorCofinsStCofinsItensNotaFiscal.ReadOnly = flag;
            txtQttyVendidaStCofinsItensNotaFiscal.ReadOnly = flag;
            txtAliquotaMoedaStCofinsItensNotaFiscal.ReadOnly = flag;
            txtAliquotaPercentualStCofinsItensNotaFiscal.ReadOnly = flag;
            txtValorBaseCalculoStCofinsItensNotaFiscal.ReadOnly = flag;
            txtValorBaseCalculoPISItensNotaFiscal.ReadOnly = flag;
            txtAliquotaPorcentagemPISItensNotaFiscal.ReadOnly = flag;
            txtAliquotaMoedaPISItensNotaFiscal.ReadOnly = flag;
            txtQttyVendidaPISItensNotaFiscal.ReadOnly = flag;
            txtValorPISItensNotaFiscal.ReadOnly = flag;
            txtValorStPISItensNotaFiscal.ReadOnly = flag;
            txtQttyVendidaStPISItensNotaFiscal.ReadOnly = flag;
            txtAliquotaMoedaStPISItensNotaFiscal.ReadOnly = flag;
            txtAliquotaStPercentualPISItensNotaFiscal.ReadOnly = flag;
            txtValorBaseCalculoStPISItensNotaFiscal.ReadOnly = flag;
            txtCodigoEnquadramentoIPIItensNotaFiscal.ReadOnly = flag;
            txtQttySeloControleIPIItensNotaFiscal.ReadOnly = flag;
            txtValorIPIItensNotaFiscal.ReadOnly = flag;
            txtValorUnIPIItensNotaFiscal.ReadOnly = flag;
            txtQttyTotalUnDefaultIPIItensNotaFiscal.ReadOnly = flag;
            txtAliquotaIPIItensNotaFiscal.ReadOnly = flag;
            txtValorBaseCalculoIPIItensNotaFiscal.ReadOnly = flag;
            txtSeloControleIPIItensNotaFiscal.ReadOnly = flag;
            mskCNPJProdutorIPIItensNotaFiscal.ReadOnly = flag;
            txtClasseEnquadramentoIPIItensNotaFiscal.ReadOnly = flag;
            txtBaseIcmsItensNotaFiscal.ReadOnly = flag;
            txtAliquotaIcmsItensNotaFiscal.ReadOnly = flag;
            txtValorIcmsItensNotaFiscal.ReadOnly = flag;
            cmbNaturezaOperacaoNotaFiscal.Enabled = flag;
            txtDadosAdicionaisCabecalhoNotaFiscal.ReadOnly = flag;
            if (flag == true) //Modifica situação para funcao Enabled
            {
                flag = false;
            }
            else
            {
                flag = true;
            }
            dtpValidacaoNfeNotaFiscal.Enabled = flag;
            dtpEmissaoNotaFiscal.Enabled = flag;
            dtpSaidaNotaFiscal.Enabled = flag;
            btnPesquisaFormaPgtoNotaFiscal.Enabled = flag;
            btnPesquisaItensNotaFiscal.Enabled = flag;
            btnPesquisaCFOPItensNotaFiscal.Enabled = flag;
            cmbUnidadeDefaultItensNotaFiscal.Enabled = flag;
            cmbFreteNotaFiscal.Enabled = flag;
            cmbTypeEspecificoItensNotaFiscal.Enabled = flag;
            rbICMSItensNotaFiscal.Enabled = flag;
            rbISSNQItensNotaFiscal.Enabled = flag;
            cmbSituacaoTributariaPISItensNotaFiscal.Enabled = flag;
            cmbTipoCalculoPISItensNotaFiscal.Enabled = flag;
            cmbTipoCalculoSTPISItensNotaFiscal.Enabled = flag;
            cmbSituacaoTributariaIPIItensNotaFiscal.Enabled = flag;
            cmbTipoCalculoIPIItensNotaFiscal.Enabled = flag;
            cmbTributoOrigemItensNotaFiscal.Enabled = flag;
            cmbTributoSituacaoTributariaItensNotaFiscal.Enabled = flag;
            cmbTributoRegimeItensNotaFiscal.Enabled = flag;
            cmbSituacaoTributariaCofinsItensNotaFiscal.Enabled = flag;
            cmbTipoCalculoCofinsItensNotaFiscal.Enabled = flag;
            cmbTipoCalculoStCofinsItensNotaFiscal.Enabled = flag;
        }
        void zeraVariavelRecalculo()
        {
            auxCalculoValorPis = "0.00";
            auxCalculoValorCofins = "0.00";
            auxCalculoValorAcrescimo = "0.00";
            auxPorcentagemAcrescimo = "0.00";
            auxCalculoValorTotalLiquido = "0.00";
            auxCalculoSeguro = "0.00";
            auxCalculoBasePis = "0.00";
            auxCalculoBaseCofins = "0.00";
            auxCalculoBaseIcmsIsento = "0.00";
            auxCalculoTotalItens = "0.00";
            auxPorcentagemDesconto = "0.00";
            auxCalculoBaseIPI = "0.00";
            auxTotalVolumeNf = "0.00";
            auxCalculoContribucaoSocial = "0.00";
            auxQttyPedido = "0.00";
            auxQttyRecebida = "0.00";
        }

        //#################################################### end controle single #######################################################
        private void button12_Click(object sender, EventArgs e)
        {
        }

        private void danfe_Load(object sender, EventArgs e)
        {
            auxIdLoja = alwaysVariables.Store;
            auxTypeAtividade = "1";
            statusObjIPI(false);
            retorno = 0;
            //statusObj(true);
            conector_carrega_loja(auxIdLoja);
            cmbNaturezaOperacaoNotaFiscal.Select();
            cmbFreteNotaFiscal.SelectedIndex = 3;
            zeraVariavelRecalculo();
            preenche_combo_paramentroFaturamento();
            conector_find_complementoFiscal("1", "");
            conector_find_complementoFiscal("4", "");
            conector_find_complementoFiscal("7", "");
            conector_find_cst();
            conector_find_un();
            if (flagSemaforo == 1)
            {   
             int testCliente = 0;
            //Item 1 - Carrega as configurações da origem
            conector_carrega_loja(alwaysVariables.Store);
            //Item 2 - Carrega as configurações do pedido
            cmbNaturezaOperacaoNotaFiscal.Text = auxDescricaoFaturamento;
            //Item 3 - Verifica type de pessoa
            auxTipoPessoa = conector_verifica_typeCliente(auxIdCliente);
            //Item 4 - Verifica se é cliente, loja ou fornecedor
            testCliente = conector_verifica_atividade(auxIdCliente);
            switch (testCliente)
            {
                case 1: //Cliente
                            auxTypeAtividade = "1";
                            if ((auxTipoPessoa == 1) || (auxTipoPessoa == 3))
                            {
                                lbDestinatarioRazaoNotaFiscal.Text = auxDescricaoDestino;
                            }
                            else { lbDestinatarioRazaoNotaFiscal.Text = auxDescricaoOrigem; }
                            if (lbDestinatarioRazaoNotaFiscal.Text != "")
                            {
                                conector_carrega_cliente(auxIdCliente);
                            }          
                    break;
                case 7: //Loja
                    auxTypeAtividade = "7";
                    auxTipoPessoa = 2;
                    lbDestinatarioRazaoNotaFiscal.Text = auxDescricaoOrigem;
                    conector_carrega_cliente(auxIdCliente);
                    break;
                case 2: //Fornecedor
                    auxTypeAtividade = "2";
                    if (testCliente > 0)
                    {
                        auxTipoPessoa = testCliente;
                        lbDestinatarioRazaoNotaFiscal.Text = auxDescricaoOrigem;
                    }
                    else
                    {
                        auxTipoPessoa = consultaFornecedor.TipoPessoa;
                        if ((consultaFornecedor.TipoPessoa == 1) || (consultaFornecedor.TipoPessoa == 3))
                        {
                            lbDestinatarioRazaoNotaFiscal.Text = auxDescricaoDestino;
                        }
                        else { lbDestinatarioRazaoNotaFiscal.Text = auxDescricaoOrigem; }
                    }
                    if (auxIdCliente != "")
                    {
                        conector_carrega_cliente(auxIdCliente);
                    }
                    break;

            }
            //Item 5 - Gera a nota fiscal a partir do pedido -- neste caso não ocorre

            //Item 6 - Carrega parcial do cabeçalho da nota fiscal
            conector_find_itemNfSaidaTotais();
            //Item 7 - Atualiza o grid produto
                conector_find_itemNfSaida("1");
                dgvItensNotaFiscal.Select();
                txtCodigoItensNotaFiscal.ReadOnly = false;
                zeraObjItem();
                txtCodigoItensNotaFiscal.Select();
                tbcNotaSaida.SelectedIndex = 1;
                tbcTributosItemNota.SelectedIndex = 0;
            }
            else
            {
                //begin
                auxIdUser = alwaysVariables.Usuario;
                auxIdTerminal = alwaysVariables.Terminal;
                //end -- provisorio a testes
            }
            if (typeEmissaoNota == 1)
            {
                conector_obj_gera_notaFiscal_vs_reserva(auxDescricaoFaturamento, auxDescricaoDestino, auxDescricaoOrigem, auxIdPedido, auxIdCliente, alwaysVariables.Store, "9");
            }
            if (dgvItensNotaFiscal.RowCount < 1)
            {
                dgvItensNotaFiscal.Rows.Add();
            }
            sinal = 0;
        }

        private void label85_Click(object sender, EventArgs e)
        {

        }

        private void btnConsultaClienteNotaFiscal_Click(object sender, EventArgs e)
        {
            conector_find_obj_cliente();
        }

        private void btnConsultaTransportadoraNotaFiscal_Click(object sender, EventArgs e)
        {
            int count = conector_full_verificaItem();
            int test;
            test = conector_verifica_dataFrete(auxIdNf);
            if ((count > 0 && auxModalidadeFrete == 9) || (auxModalidadeFrete != 9))
            {
                if (test > 0)
                {
                    test = 1;
                }
                findTransporte = new consultaTransporte(auxModalidadeFrete, cmbFreteNotaFiscal.Text, test, auxIdNfTransportadora, auxIdNf);
                if (findTransporte.ShowDialog(this) == DialogResult.OK)
                {
                    auxModalidadeFrete = findTransporte.GridModalidade;
                    cmbFreteNotaFiscal.Text = findTransporte.GridDescricaoTipoFrete;
                    if (auxModalidadeFrete != 9)
                    {
                        optionCodigoTransportadora = findTransporte.GridCodigo;
                        optionRazaoTransportadora = findTransporte.GridRazao;
                        optionCnpjTransportadora = findTransporte.GridCnpj;
                        optionIeTransportadora = findTransporte.Gridie;
                        optionInativaTransportadora = findTransporte.GridStatus;
                        optionAbreviaturaTransportadora = findTransporte.Gridabreviatura;
                        optionCodigoMunicipioTransportadora = findTransporte.Gridsped;
                        optionCepTransportadora = findTransporte.Gridcep;
                        optionEnderecoTransportadora = findTransporte.GridEndereco;
                        optionBairroTransportadora = findTransporte.Gridbairro;
                        optionNumberTransportadora = findTransporte.GridNumber;
                        optionCidadeTransportadora = findTransporte.Gridmunicipio;
                        optionUfTransportadora = findTransporte.GridUf;
                        optionComplementoTransportadora = findTransporte.GridComplemento;
                        auxIdUfTransportadora = findTransporte.GrididEstado;
                        if (optionInativaTransportadora != "" && optionInativaTransportadora != "1")
                        {
                            lbTransporteRazaoNotaFiscal.Text = optionRazaoTransportadora;
                            lbTransporteLogradouroNotaFiscal.Text = optionEnderecoTransportadora + ", " + optionNumberTransportadora;
                            lbTransporteMunicipioNotaFiscal.Text = optionCidadeTransportadora;
                            lbUfVeiculoNotaFiscal.Text = optionUfTransportadora;
                            lbTransporteCNPJNotaFiscal.Text = optionCnpjTransportadora;
                            lbTransporteINSNotaFiscal.Text = optionIeTransportadora;
                        }
                        optionCFOPFrete = findTransporte.GridCFOPFrete;
                        optionDescricaoTipoFrete = findTransporte.GridDescricaoTipoFrete;
                        optionBaseCalculoFrete = findTransporte.GridBaseCalculo;
                        optionIcmsFrete = findTransporte.GridIcmsFrete;
                        optionValorTotalFrete = findTransporte.GridValorTotalFrete;
                        optionIdVeiculo = findTransporte.GridIdVeiculo;
                        optionVolumeQtty = findTransporte.GridVolumeQtty;
                        optionVolumePesoNumber = findTransporte.GridVolumePesoNumber;
                        optionVolumePesoBruto = findTransporte.GridVolumePesoBruto;
                        optionVolumePesoLiquido = findTransporte.GridVolumePesoLiquido;
                        optionVolumeMarca = findTransporte.GridVolumeMarca;
                        optionVolumeEspecie = findTransporte.GridVolumeEspecie;
                        optionAliquotaFrete = findTransporte.GridAliquota;
                        auxGrididestado = findTransporte.GrididEstado;
                        optionAnttVeiculo = findTransporte.GridANTTVeiculo;
                        optionPlacaVeiculo = findTransporte.GridPlacaVeiculo;
                        auxGridUFVeiculo = findTransporte.GrididUFVeiculo;
                        optionIsentoIcms = findTransporte.GridIsentoIcms;

                        lbCodigoAnttNotaFiscal.Text = optionAnttVeiculo;
                        lbPlacaVeiculoNotaFiscal.Text = optionPlacaVeiculo;
                        lbUfVeiculoNotaFiscal.Text = auxGridUFVeiculo;

                        lbTransportePesoLiquidoNotaFiscal.Text = optionVolumePesoLiquido;
                        lbTransportePesoBrutoNotaFiscal.Text = optionVolumePesoBruto;
                        lbTransporteNumeroNotaFiscal.Text = optionVolumePesoNumber;
                        lbTransporteEspecieNotaFiscal.Text = optionVolumeEspecie;
                        lbTransporteQuantidadeNotaFiscal.Text = optionVolumeQtty;
                        lbCalculoImpostoValorFreteNotaFiscal.Text = optionValorTotalFrete;
                    }
                    else
                    {
                        MessageBox.Show("Modalidade Frete , consta como '9 - sem frete' ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cmbFreteNotaFiscal.Select();
                        optionCodigoTransportadora = findTransporte.GridCodigo;
                        optionRazaoTransportadora = findTransporte.GridRazao;
                        lbTransporteRazaoNotaFiscal.Text = optionRazaoTransportadora;
                    }
                    if (test == 1)
                    {
                        conector_alt_nfinfortransporte();
                        if (auxIdNfTransportadora != "")
                        {
                            conector_alt_nfInforVeiculo();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Modalidade '9 - sem frete', nota fiscal não possui produtos para transportar.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtTypeNotaFiscal_Validated(object sender, EventArgs e)
        {
            if (txtTypeNotaFiscal.Text == "1" || txtTypeNotaFiscal.Text == "2")
            {
                btnPesquisaCFOPItensNotaFiscal.Select();
            }
            else
            {
                MessageBox.Show("Tipo de nota invalido, deve ser '1 - Saída' ou '2 - Entrada'. ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTypeNotaFiscal.Clear();
                txtTypeNotaFiscal.Select();
            }
        }

        private void btnPesquisaCFOPNotaFiscal_Click(object sender, EventArgs e)
        {
            findCFOP = new pesquisaCFOP();
            if (findCFOP.ShowDialog(this) == DialogResult.OK)
            {
                if (txtTypeNotaFiscal.Text == "1" || txtTypeNotaFiscal.Text == "2")
                {
                    auxIdCFOP = findCFOP.GridCodigo;
                    auxTypeCFOP = findCFOP.GridInput;
                    if (auxTypeCFOP == "saida")
                    {
                        if (txtTypeNotaFiscal.Text == "1")
                        {
                            txtCFOPNotaFiscal.Text = auxIdCFOP;
                        }
                        else
                        {
                            MessageBox.Show("CFOP inválido para saída de mercadoria. ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else if (auxTypeCFOP == "entrada")
                    {
                        if (txtTypeNotaFiscal.Text == "2")
                        {
                            txtCFOPNotaFiscal.Text = auxIdCFOP;
                        }
                        else
                        {
                            MessageBox.Show("CFOP inválido para entrada de mercadoria. ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Tipo de nota invalido, deve ser '1 - Saída' ou '2 - Entrada'. ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTypeNotaFiscal.Clear();
                    txtTypeNotaFiscal.Select();
                }
            }
        }

        private void txtCFOPNotaFiscal_Validated(object sender, EventArgs e)
        {
            if (txtCFOPNotaFiscal.Text != "")
            {
                conector_find_CFOP(txtCFOPNotaFiscal.Text,0);
                if (cmbNaturezaOperacaoNotaFiscal.Text != "")
                {
                    txtSerieNotaFiscal.Select();
                }
            }
            else
            {
                MessageBox.Show("CFOP inválido para saída de mercadoria. ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnPesquisaCFOPItensNotaFiscal.Select();
                tbcNotaSaida.SelectedIndex = 0;
            }
        }

        private void cmbFreteNotaFiscal_SelectedIndexChanged(object sender, EventArgs e)
        {
            auxModalidadeFrete = Convert.ToInt16(cmbFreteNotaFiscal.Text.Substring(0, 2));
            switch (auxModalidadeFrete)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 9:
                    lbTransporteRazaoNotaFiscal.Text = "";
                    lbCodigoAnttNotaFiscal.Text = "";
                    lbPlacaVeiculoNotaFiscal.Text = "";
                    lbTransporteUfNotaFiscal.Text = "";
                    lbUfVeiculoNotaFiscal.Text = "";
                    lbTransporteCNPJNotaFiscal.Text = "";
                    lbTransporteINSNotaFiscal.Text = "";
                    lbTransporteMunicipioNotaFiscal.Text = "";
                    lbTransporteLogradouroNotaFiscal.Text = "";
                    lbTransporteQuantidadeNotaFiscal.Text = "";
                    lbTransporteEspecieNotaFiscal.Text = "";
                    lbTransporteNumeroNotaFiscal.Text = "";
                    lbTransporteLogradouroNotaFiscal.Text = "";
                    break;
            }
        }

        private void cmbNaturezaOperacaoNotaFiscal_Validated(object sender, EventArgs e)
        {
            if (cmbNaturezaOperacaoNotaFiscal.Text == "")
            {
                MessageBox.Show("Natureza da operação deve ser informada. ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tbcNotaSaida.SelectedIndex = 0;
                cmbNaturezaOperacaoNotaFiscal.Select();
            }
            else
            {
                btnPesquisaCFOPItensNotaFiscal.Select();
                tbcNotaSaida.SelectedIndex = 0;
            }
        }

        private void cmbNaturezaOperacaoNotaFiscal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbNaturezaOperacaoNotaFiscal.Text != "")
            {
                auxConsistencia = 0;
                try
                {
                    banco.abreConexao();
                    banco.startTransaction("conector_find_paramentroFaturamento");
                    banco.addParametro("tipo", "3");
                    banco.addParametro("find", cmbNaturezaOperacaoNotaFiscal.Text);
                    banco.procedimentoSet();
                }
                catch (Exception erro)
                { MessageBox.Show(erro.Message, "Erro não identificado, entre contato como revendedor"); auxConsistencia = 1; }
                finally
                {
                    if (auxConsistencia == 0)
                    {
                        countRows = banco.retornaSet().Tables.Count;
                        if (countRows > 0)
                        {
                            auxIdParamentro = Convert.ToString(banco.retornaSet().Tables[0].Rows[0][0]);
                        }
                    }
                    banco.fechaConexao();
                    if (auxConsistencia == 0)
                    {
                        if (auxIdParamentro != "")
                        {
                            conector_carrega_paramentroFaturamento(auxIdParamentro);
                        }
                    }
                } btnPesquisaCFOPNotaFiscal.Select();
            }
        }

        private void danfe_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ProcessTabKey(true);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
            else if (e.Alt && e.KeyCode == Keys.C && tbcNotaSaida.SelectedIndex == 0)
            {
                conector_carrega_pedido();
            }
            else if (e.KeyCode == Keys.C && tbcNotaSaida.SelectedIndex == 0)
            {
                conector_carrega_pedido();
            }
            else if (e.KeyCode == Keys.F11 && tbcNotaSaida.SelectedIndex == 0) //find cliente
            {
                conector_find_obj_cliente();
                tbcNotaSaida.SelectedIndex = 0;
            }
            else if (e.KeyCode == Keys.F8 && tbcNotaSaida.SelectedIndex == 0)
            {
                pesquisaFinalizadora codigoFinalizadora = new pesquisaFinalizadora();
                if (codigoFinalizadora.ShowDialog(this) == DialogResult.OK)
                {
                    auxCondPgto = codigoFinalizadora.GridCodigo;
                    conector_find_finalizadora(auxCondPgto);
                }
            }
            else if (e.KeyCode == Keys.F5 && tbcNotaSaida.SelectedIndex == 1 && tbcTributosItemNota.SelectedIndex == 0) //Salve item) //Excluir Item
            {
                flagRetornoTotalItens = conector_full_verificaItem();
                if ((flagRetornoTotalItens > 0))
                {
                    ataque = 0;

                    conector_obj_removeItensNota();

                    ataque = 1;

                }
                else
                {
                    MessageBox.Show("Nota não possui nenhum item selecionado, impossivel prosseguir! ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (e.KeyCode == Keys.F10)
            {
                conector_obj_salve_nf();
            }
            else if ((e.KeyCode == Keys.F4 && e.KeyCode != Keys.Alt) && tbcNotaSaida.SelectedIndex == 1 && tbcTributosItemNota.SelectedIndex == 0) //Salve item)
            {
                if (auxIdNf == "")
                {
                    zeraObjItem();
                    optionProdutoDescricao = "";
                    txtCodigoItensNotaFiscal.ReadOnly = false;
                    txtCFOPItensNotaFiscal.CausesValidation = false;
                    txtCodigoItensNotaFiscal.Select();
                    txtCFOPItensNotaFiscal.CausesValidation = true;
                    tbcNotaSaida.SelectedIndex = 1;
                }/*
                else
                {
                    //
                    if (MessageBox.Show("Deseja validar o cabeçalho da nota antes de inserir qualquer item?", "Caro Usúario", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    { 
                        conector_obj_salve_nf(); 
                    }
                    else
                    {
                        MessageBox.Show("Item não pode ser inserido,valide a nota fiscal pressionando a tecla [F10],\n devido ao cabeçalho da nota estar incomplento! ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        zeraObjItem();
                        tbcNotaSaida.SelectedIndex = 0;
                    }
                }*/
            }
            else if (e.KeyCode == Keys.F2 && tbcNotaSaida.SelectedIndex == 1 && tbcTributosItemNota.SelectedIndex == 0) //Salve item) //Find Item
            {
                if (auxIdNf != "")
                {
                    pesquisaProd = new pesquisaProduto();
                    if (pesquisaProd.ShowDialog(this) == DialogResult.OK)
                    {
                        auxIdProduto = pesquisaProd.Produto;
                        txtCodigoItensNotaFiscal.Text = auxIdProduto;
                        int count = conector_verifica(auxIdProduto);
                        conector_find_codBarra(auxIdProduto);
                        if (optionFlagRepetItemFaturamento == "1")
                        {
                            if (count >= 1)
                            {
                                MessageBox.Show("Produto existe nesta nota fiscal!", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtCodigoItensNotaFiscal.Clear();
                                txtCodigoItensNotaFiscal.Select();
                                zeraObjItem();
                                zeraVariavelRecalculo();
                            }
                            else { conector_find_produto(auxIdProduto); }
                        }
                        else { conector_find_produto(auxIdProduto); }
                    }
                }
            }
            else if (e.KeyCode == Keys.F12 && tbcNotaSaida.SelectedIndex == 1 && tbcTributosItemNota.SelectedIndex == 0) //Salve item
            {
                conector_obj_salve_item();
            }
            else
            {
                cmbNaturezaOperacaoNotaFiscal.CausesValidation = false;
                cmbNaturezaOperacaoNotaFiscal.CausesValidation = true;
            }
        }

        private void danfe_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void cmbSituacaoTributariaPISItensNotaFiscal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSituacaoTributariaIPIItensNotaFiscal.Text != "")
            {
                countField = 0;
                countRows = 0;
                try
                {
                    banco.abreConexao();
                    banco.startTransaction("conector_find_complementoFiscal");
                    banco.addParametro("tipo", "5");
                    banco.addParametro("find", cmbSituacaoTributariaPISItensNotaFiscal.Text);
                    banco.procedimentoSet();
                    countRows = banco.retornaSet().Tables[0].DefaultView.Count;
                }
                catch (Exception erro)
                { MessageBox.Show(erro.Message, "Messagem do Sistema"); }
                finally
                {
                    if (countRows > 0)
                    {
                        auxCstPisProduto = Convert.ToString(banco.retornaSet().Tables[0].Rows[0][0]);   
                    }
                    banco.fechaConexao();
                }
            }//end if
        }

        private void cmbSituacaoTributariaIPIItensNotaFiscal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSituacaoTributariaIPIItensNotaFiscal.Text != "")
            {
                countField = 0;
                countRows = 0;
                try
                {
                    banco.abreConexao();
                    banco.startTransaction("conector_find_complementoFiscal");
                    banco.addParametro("tipo", "2");
                    banco.addParametro("find", cmbSituacaoTributariaIPIItensNotaFiscal.Text);
                    banco.procedimentoSet();
                    countRows = banco.retornaSet().Tables[0].DefaultView.Count;
                }
                catch (Exception erro)
                { MessageBox.Show(erro.Message, "Messagem do Sistema"); }
                finally
                {
                    if (countRows > 0)
                    {
                        auxCstIpiProduto = Convert.ToString(banco.retornaSet().Tables[0].Rows[0][0]);   
                    }
                    banco.fechaConexao();
                }
            }//end if
        }

        private void cmbSituacaoTributariaCofinsItensNotaFiscal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSituacaoTributariaCofinsItensNotaFiscal.Text != "")
            {
                countField = 0;
                countRows = 0;
                try
                {
                    banco.abreConexao();
                    banco.startTransaction("conector_find_complementoFiscal");
                    banco.addParametro("tipo", "7");
                    banco.addParametro("find", cmbSituacaoTributariaCofinsItensNotaFiscal.Text);
                    banco.procedimentoSet();
                    countRows = banco.retornaSet().Tables[0].DefaultView.Count;
                }
                catch (Exception erro)
                { MessageBox.Show(erro.Message, "Messagem do Sistema"); }
                finally
                {
                    if (countRows > 0)
                    {
                        auxCstCofinsProduto = Convert.ToString(banco.retornaSet().Tables[0].Rows[0][0]);    
                    }
                    
                    banco.fechaConexao();
                }
            }//end if
        }

        private void cmbTributoSituacaoTributariaItensNotaFiscal_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cmbTributoSituacaoTributariaItensNotaFiscal.Text != "")
            {
                auxCstProduto = cmbTributoSituacaoTributariaItensNotaFiscal.Text.Substring(0, 3);
            }
                
        }

        private void cmbUnidadeDefaultItensNotaFiscal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbUnidadeDefaultItensNotaFiscal.Text != "")
            {
                auxConsistencia = 0;
                countField = 0;
                countRows = 0;
                try
                {
                    banco.abreConexao();
                    banco.startTransaction("conector_find_un");
                    banco.addParametro("tipo", "1");
                    banco.addParametro("find", cmbUnidadeDefaultItensNotaFiscal.Text);
                    banco.procedimentoSet();
                }
                catch (Exception erro)
                { MessageBox.Show(erro.Message, "Erro não identificado, entre contato como revendedor"); auxConsistencia = 1; }
                finally
                {
                    if (auxConsistencia == 0)
                    {
                        countRows = banco.retornaSet().Tables[0].DefaultView.Count;
                        if (countRows > 0)
                        {
                            auxIdUnidade = Convert.ToString(banco.retornaSet().Tables[0].Rows[0][0]);
                        }
                    }
                    banco.fechaConexao();
                }
            }//end if
        }
        private void conector_find_codBarra(string barra)
        {
            try
            {
                banco.abreConexao();
                banco.startTransaction("conector_find_codBarra");
                banco.addParametro("tipo", "3");
                banco.addParametro("find", barra);
                banco.procedimentoRead();
                if (banco.retornaRead().Read() == true)
                {
                    optionBarraItemNotaFiscal = banco.retornaRead().GetString(0);
                    auxMultiplicador = banco.retornaRead().GetString(1);
                    auxUnItemNotaFiscal = banco.retornaRead().GetString(2);
                    auxIdUnItemNotaFiscal = banco.retornaRead().GetString(7);
                    optionIdBarraItemNotaFiscal = banco.retornaRead().GetString(8);
                }
            }
            catch (Exception erro) { MessageBox.Show(erro.Message, "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            finally
            {
                banco.fechaConexao();
                cmbUnidadeDefaultItensNotaFiscal.Text = auxUnItemNotaFiscal;
                txtEANBarraItensNotaFiscal.Text = optionBarraItemNotaFiscal;
            }
        }
        private void btnPesquisaItensNotaFiscal_Click(object sender, EventArgs e)
        {
            if (lbDestinatarioRazaoNotaFiscal.Text != "")
            {
                int count = 0;
                if (optionflagForceNfOrigem == "0")
                {
                    pesquisaProd = new pesquisaProduto();
                    if (pesquisaProd.ShowDialog(this) == DialogResult.OK)
                    {
                        auxIdProduto = pesquisaProd.Produto;
                        txtCodigoItensNotaFiscal.Text = auxIdProduto;
                        count = conector_verifica(auxIdProduto);
                        conector_find_codBarra(auxIdProduto);
                        if (optionFlagRepetItemFaturamento == "1")
                        {
                            if (count >= 1)
                            {
                                MessageBox.Show("Produto existe nesta nota fiscal!", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtCodigoItensNotaFiscal.Clear();
                                txtCodigoItensNotaFiscal.Select();
                                zeraObjItem();
                                zeraVariavelRecalculo();
                            }
                            else { conector_find_produto(auxIdProduto); }
                        }
                        else { conector_find_produto(auxIdProduto); }
                    }
                }
                else
                {
                    if (txtNumeroNfEntradaNotaSaida.Text != "")
                    {
                        if (txtCodigoItensNotaFiscal.Text != "")
                        {
                            conector_find_codBarra(txtCodigoItensNotaFiscal.Text);

                            if (optionFlagRepetItemFaturamento == "1")
                            {
                                if ((count >= 1) && (txtCodigoItensNotaFiscal.ReadOnly == false))
                                {
                                    MessageBox.Show("Produto existe nesta nota!", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    txtCodigoItensNotaFiscal.Clear();
                                    txtCodigoItensNotaFiscal.Select();
                                    zeraObjItem();
                                    zeraVariavelRecalculo();
                                }
                                else { conector_find_produto(txtCodigoItensNotaFiscal.Text); }
                            }
                            else
                            {
                                conector_find_produto(txtCodigoItensNotaFiscal.Text);
                            }
                        }
                    }
                    else
                    {
                        zeraObjItem();
                        MessageBox.Show("Nota fiscal de origem obrigatória e invalida!", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtNumeroNfEntradaNotaSaida.Clear();
                        tbcNotaSaida.SelectedIndex = 2;
                        txtNumeroNfEntradaNotaSaida.Select();

                    }
                }
            }
            else
            {
                MessageBox.Show("Cabeçalho incompleto, valide a nota para prosseguir.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tbcNotaSaida.SelectedIndex = 0;
            }
        }

        private void txtCodigoItensNotaFiscal_Validated(object sender, EventArgs e)
        {
            if (lbDestinatarioRazaoNotaFiscal.Text != "")
            {
                if (Convert.ToDouble(auxIdNf == "" ? "0" : auxIdNf) > 0 && auxIdNf != "")
                {
                    int count = conector_verifica(txtCodigoItensNotaFiscal.Text == "" ? "0" : txtCodigoItensNotaFiscal.Text);
                    if (optionflagForceNfOrigem == "0")
                    {
                        if (txtCodigoItensNotaFiscal.Text != "" && Convert.ToDouble(txtCodigoItensNotaFiscal.Text) > 0)
                        {
                            conector_find_codBarra(txtCodigoItensNotaFiscal.Text);

                            if (optionFlagRepetItemFaturamento == "1")
                            {
                                if ((count >= 1) && (txtCodigoItensNotaFiscal.ReadOnly == false))
                                {
                                    MessageBox.Show("Produto existe nesta nota!", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    txtCodigoItensNotaFiscal.Clear();
                                    txtCodigoItensNotaFiscal.Select();
                                    zeraObjItem();
                                    zeraVariavelRecalculo();
                                }
                                else { conector_find_produto(txtCodigoItensNotaFiscal.Text); }
                            }
                            else
                            {
                                conector_find_produto(txtCodigoItensNotaFiscal.Text);
                            }
                        }
                    }
                    else
                    {
                        if (txtNumeroNfEntradaNotaSaida.Text != "")
                        {
                            if (txtCodigoItensNotaFiscal.Text != "")
                            {
                                conector_find_codBarra(txtCodigoItensNotaFiscal.Text);

                                if (optionFlagRepetItemFaturamento == "1")
                                {
                                    if ((count >= 1) && (txtCodigoItensNotaFiscal.ReadOnly == false))
                                    {
                                        MessageBox.Show("Produto existe nesta nota!", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        txtCodigoItensNotaFiscal.Clear();
                                        txtCodigoItensNotaFiscal.Select();
                                        zeraObjItem();
                                        zeraVariavelRecalculo();
                                    }
                                    else { conector_find_produto(txtCodigoItensNotaFiscal.Text); }
                                }
                                else
                                {
                                    conector_find_produto(txtCodigoItensNotaFiscal.Text);
                                }
                            }
                        }
                        else
                        {
                            zeraObjItem();
                            MessageBox.Show("Nota fiscal de origem obrigatória e invalida!", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtNumeroNfEntradaNotaSaida.Clear();
                            tbcNotaSaida.SelectedIndex = 2;
                            txtNumeroNfEntradaNotaSaida.Select();

                        }
                    }
                }
                else
                {
                                tbcNotaSaida.SelectedIndex = 0;
                                MessageBox.Show("Cabeçalho da nota incompleto, pressione F10 para validar! ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Cabeçalho incompleto, valide a nota para prosseguir.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tbcNotaSaida.SelectedIndex = 0;
            }
        }

        private void linkConfirmaItemPedido_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            txtCodigoItensNotaFiscal.ReadOnly = false;
            zeraObjItem();
            txtCodigoItensNotaFiscal.Select();
            flagSemaforoItem = 0;
        }

        private void txtCodigoItensNotaFiscal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (Char)Keys.Back)
            {
                e.Handled = true;
                return;
            }
            if (e.KeyChar == '.' && posSeparator > -1)
            {
                e.Handled = true;
                return;
            }
        }

        private void txtCFOPItensNotaFiscal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (Char)Keys.Back)
            {
                e.Handled = true;
                return;
            }
            if (e.KeyChar == '.' && posSeparator > -1)
            {
                e.Handled = true;
                return;
            }
        }

        private void txtCodigoNCMItensNotaFiscal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (Char)Keys.Back)
            {
                e.Handled = true;
                return;
            }
            if (e.KeyChar == '.' && posSeparator > -1)
            {
                e.Handled = true;
                return;
            }
        }

        private void txtIdPedidoItensNotaFiscal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (Char)Keys.Back)
            {
                e.Handled = true;
                return;
            }
            if (e.KeyChar == '.' && posSeparator > -1)
            {
                e.Handled = true;
                return;
            }
        }

        private void txtCFOPNotaFiscal_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCFOPNotaFiscal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (Char)Keys.Back)
            {
                e.Handled = true;
                return;
            }
            if (e.KeyChar == '.' && posSeparator > -1)
            {
                e.Handled = true;
                return;
            }
        }

        private void txtTypeNotaFiscal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (Char)Keys.Back)
            {
                e.Handled = true;
                return;
            }
            if (e.KeyChar == '.' && posSeparator > -1)
            {
                e.Handled = true;
                return;
            }
        }

        private void mskNumberNotaFiscal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (Char)Keys.Back)
            {
                e.Handled = true;
                return;
            }
            if (e.KeyChar == '.' && posSeparator > -1)
            {
                e.Handled = true;
                return;
            }
        }

        private void txtEANBarraItensNotaFiscal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (Char)Keys.Back)
            {
                e.Handled = true;
                return;
            }
            if (e.KeyChar == '.' && posSeparator > -1)
            {
                e.Handled = true;
                return;
            }
        }

        private void txtCodigoEnquadramentoIPIItensNotaFiscal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (Char)Keys.Back)
            {
                e.Handled = true;
                return;
            }
            if (e.KeyChar == '.' && posSeparator > -1)
            {
                e.Handled = true;
                return;
            }
        }

        private void txtCFOPItensNotaFiscal_Validated(object sender, EventArgs e)
        {
            if (lbDestinatarioRazaoNotaFiscal.Text != "")
            {
                if (txtDescricaoItensNotaFiscal.Text != "")
                {
                    if (txtCFOPItensNotaFiscal.Text != "")
                    {
                        conector_find_CFOP(txtCFOPItensNotaFiscal.Text, 1);

                    }
                    else
                    {
                        MessageBox.Show("CFOP inválido, favor defini-lo para prosseguir!", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCFOPItensNotaFiscal.Clear();
                        txtCFOPItensNotaFiscal.Select();
                    }

                }
                else
                {
                    MessageBox.Show("Impossível designar um CFOP, sem escolher um item para prosseguir.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCFOPItensNotaFiscal.Clear();
                    txtCodigoItensNotaFiscal.Select();
                }
            }
            else
            {
                MessageBox.Show("Cabeçalho incompleto, valide a nota para prosseguir.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tbcNotaSaida.SelectedIndex = 0;
            }
        }

        private void btnPesquisaCFOPItensNotaFiscal_Click(object sender, EventArgs e)
        {
            if (lbDestinatarioRazaoNotaFiscal.Text != "")
            {
                findCFOP = new pesquisaCFOP();
                if (findCFOP.ShowDialog(this) == DialogResult.OK)
                {
                    if (txtTypeNotaFiscal.Text == "1" || txtTypeNotaFiscal.Text == "2")
                    {
                        auxIdCFOPItemNotaFiscal = findCFOP.GridCodigo;
                        if (auxTypeCFOP == "saida")
                        {
                            if (txtTypeNotaFiscal.Text == "1")
                            {
                                txtCFOPItensNotaFiscal.Text = auxIdCFOPItemNotaFiscal;
                            }
                            else
                            {
                                MessageBox.Show("CFOP inválido para saída de mercadoria. ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else if (auxTypeCFOP == "entrada")
                        {
                            if (txtTypeNotaFiscal.Text == "2")
                            {
                                txtCFOPItensNotaFiscal.Text = auxIdCFOPItemNotaFiscal;
                            }
                            else
                            {
                                MessageBox.Show("CFOP inválido para entrada de mercadoria. ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Tipo de nota invalido, deve ser '1 - Saída' ou '2 - Entrada'. ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtTypeNotaFiscal.Clear();
                        txtTypeNotaFiscal.Select();
                    }
                }
            }
            else
            {
                MessageBox.Show("Cabeçalho incompleto, valide a nota para prosseguir.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tbcNotaSaida.SelectedIndex = 0;
            }
        }

        private void txtQttyComercialTributavelItensNotaFiscal_Validated(object sender, EventArgs e)
        {
            if (txtQttyComercialTributavelItensNotaFiscal.Text != "")
            {
                auxNumeric = Convert.ToDecimal(txtQttyComercialTributavelItensNotaFiscal.Text);//currency
                flagNumeric = String.Format("{0:F2}", auxNumeric);
                txtQttyComercialTributavelItensNotaFiscal.Text = flagNumeric.Replace(",", ".").Trim();
            }
            else
            {
                txtQttyComercialTributavelItensNotaFiscal.Text = "0.00";
            }
            

            if (txtDescontoItensNotaFiscal.Text != "") { discount = Convert.ToDecimal(txtDescontoItensNotaFiscal.Text); } else { discount = 0; }
            if (txtOutrasDespesasAcessoriasItensNotaFiscal.Text != "") { despesas = Convert.ToDecimal(txtOutrasDespesasAcessoriasItensNotaFiscal.Text); } else { despesas = 0; }
            
            if (txtQttyComercialTributavelItensNotaFiscal.Text == "" && txtQttyComercialTributavelItensNotaFiscal.Text == "0.00" && Convert.ToDecimal(txtQttyComercialTributavelItensNotaFiscal.Text) <= 0)
            {
                MessageBox.Show("Quantidade obrigatória.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtQttyComercialTributavelItensNotaFiscal.Select();
            }
            else
            {
                if (txtValorUnComercialTributarioItensNotaFiscal.Text != "" && txtValorUnComercialTributarioItensNotaFiscal.Text != "0.00" && Convert.ToDecimal(txtValorUnComercialTributarioItensNotaFiscal.Text) > 0)
                {
                    //Update 0
                    txtValorBrutoItensNotaFiscal.Text = calculoImpostos.calcSumProduto(Convert.ToDecimal(txtValorUnComercialTributarioItensNotaFiscal.Text), Convert.ToDecimal(txtQttyComercialTributavelItensNotaFiscal.Text), despesas, discount).ToString();
                    if (txtValorBrutoItensNotaFiscal.Text != "")
                    {
                        auxNumeric = Convert.ToDecimal(txtValorBrutoItensNotaFiscal.Text);//currency
                        flagNumeric = String.Format("{0:F2}", auxNumeric);
                        txtValorBrutoItensNotaFiscal.Text = flagNumeric.Replace(",", ".").Trim();
                    }
                    if (optionProdutocsttypeCst == "s")
                    {
                        optionSomaProdutoFinanceiro = calculoImpostos.calcSumProduto(Convert.ToDecimal(txtValorUnComercialTributarioItensNotaFiscal.Text), Convert.ToDecimal(txtQttyComercialTributavelItensNotaFiscal.Text), Convert.ToDecimal(txtOutrasDespesasAcessoriasItensNotaFiscal.Text), 0).ToString();
                        
                        if (optionProdutocstCalcBc == "1")
                        {
                            optionProdutobaseCalculoIcmsSubstituicao = calculo.calcBaseCalculoST(Convert.ToDecimal(optionSomaProdutoFinanceiro), Convert.ToDecimal(optionProdutoMVAAjusteMva)).ToString();
                            //######################################################Base destacada ST################################################
                            //Update 1 st
                            txtBaseIcmsItensNotaFiscal.Text = calculoImpostos.calcBaseCalculo(Convert.ToDecimal(txtValorBrutoItensNotaFiscal.Text), Convert.ToDecimal(optionProdutoreducao)).ToString();
                            if (txtBaseIcmsItensNotaFiscal.Text != "")
                            {
                                auxNumeric = Convert.ToDecimal(txtBaseIcmsItensNotaFiscal.Text);//currency
                                flagNumeric = String.Format("{0:F2}", auxNumeric);
                                txtBaseIcmsItensNotaFiscal.Text = flagNumeric.Replace(",", ".").Trim();
                            }

                            //Update 2 st
                            txtValorIcmsItensNotaFiscal.Text = calculoImpostos.calcIcms(Convert.ToDecimal(txtValorBrutoItensNotaFiscal.Text), Convert.ToDecimal(optionProdutoaliquota)).ToString();
                            if (txtValorIcmsItensNotaFiscal.Text != "")
                            {
                                auxNumeric = Convert.ToDecimal(txtValorIcmsItensNotaFiscal.Text);//currency
                                flagNumeric = String.Format("{0:F2}", auxNumeric);
                                txtValorIcmsItensNotaFiscal.Text = flagNumeric.Replace(",", ".").Trim();
                            }
                            //######################################################End Base destacada ST###########################################
                            if (optionProdutocstCalcRed == "1")
                            {
                                optionSomaProdutoFinanceiro = calculoImpostos.calcBaseCalculo(Convert.ToDecimal(optionSomaProdutoFinanceiro), Convert.ToDecimal(optionProdutoreducao)).ToString();
                                optionProdutobaseCalculoIcmsSubstituicao = calculo.calcBaseCalculoST(Convert.ToDecimal(optionSomaProdutoFinanceiro), Convert.ToDecimal(optionProdutoMVAAjusteMva)).ToString();
                            }
                            else
                            {
                                txtReducaoBaseCalculoItensNotaFiscal.Text = "0.00";
                            }
                            optionProdutoIcmsSt = calculo.calcIcmsST(Convert.ToDecimal(optionProdutobaseCalculoIcmsSubstituicao), Convert.ToDecimal(optionProdutoMVAInterna)).ToString();
                            optionProdutoIcmsStRecolher = calculo.calcIcmsArecolher(Convert.ToDecimal(optionProdutoIcmsSt), calculoImpostos.calcIcms(Convert.ToDecimal(txtValorBrutoItensNotaFiscal.Text), Convert.ToDecimal(optionProdutoaliquota))).ToString();

                            txtAliquotaIcmsSTItensNotaFiscal.Text = optionProdutoMVAInterna;

                            auxNumeric = Convert.ToDecimal(optionProdutobaseCalculoIcmsSubstituicao);//currency Convertendo
                            flagNumeric = String.Format("{0:F2}", auxNumeric);
                            txtBaseIcmsSTItensNotaFiscal.Text = flagNumeric.Replace(",", ".").Trim();

                            txtReducaoBaseCalculoSTItensNotaFiscal.Text = optionProdutoreducao;

                            auxNumeric = Convert.ToDecimal(optionProdutoIcmsStRecolher);//currency Convertendo
                            flagNumeric = String.Format("{0:F2}", auxNumeric);
                            txtValorIcmsSTItensNotaFiscal.Text = flagNumeric.Replace(",", ".").Trim();

                        }
                        else
                        {
                            /*Alterado Flavio Rever*/
                            //txtBaseIcmsSTItensNotaFiscal.Text = Math.Round(Convert.ToDecimal(calculoImpostos.calcSumProduto(Convert.ToDecimal(txtValorUnComercialTributarioItensNotaFiscal.Text), Convert.ToDecimal(txtQttyComercialTributavelItensNotaFiscal.Text), despesas, discount).ToString()),3).ToString();
                            txtBaseIcmsSTItensNotaFiscal.Text = "0.00";
                            txtBaseIcmsItensNotaFiscal.Text = "0.00";
                            txtReducaoBaseCalculoItensNotaFiscal.Text = "0.00";
                        }
                        
                        if (optionProdutocstModalidade == "m")
                        {
                            
                        }
                    }
                    else
                    {
                        //Update 1
                        txtBaseIcmsItensNotaFiscal.Text = calculoImpostos.calcBaseCalculo(Convert.ToDecimal(txtValorBrutoItensNotaFiscal.Text), Convert.ToDecimal(optionProdutoreducao)).ToString();
                        if (txtBaseIcmsItensNotaFiscal.Text != "")
                        {
                            auxNumeric = Convert.ToDecimal(txtBaseIcmsItensNotaFiscal.Text);//currency
                            flagNumeric = String.Format("{0:F2}", auxNumeric);
                            txtBaseIcmsItensNotaFiscal.Text = flagNumeric.Replace(",", ".").Trim();
                        }
                        //Update 2
                        txtValorIcmsItensNotaFiscal.Text = calculoImpostos.calcIcms(Convert.ToDecimal(txtValorBrutoItensNotaFiscal.Text), Convert.ToDecimal(optionProdutoaliquota)).ToString();
                        if (txtValorIcmsItensNotaFiscal.Text != "")
                        {
                            auxNumeric = Convert.ToDecimal(txtValorIcmsItensNotaFiscal.Text);//currency
                            flagNumeric = String.Format("{0:F2}", auxNumeric);
                            txtValorIcmsItensNotaFiscal.Text = flagNumeric.Replace(",", ".").Trim();
                        }

                        //Update 3 PIS
                        if (optionProdutoPisSt == "n")
                        {
                            optionProdutobasePis = txtValorBrutoItensNotaFiscal.Text;
                            flagNumeric = String.Format("{0:F2}", calculoImpostos.calcPis(Convert.ToDecimal(txtValorBrutoItensNotaFiscal.Text), Convert.ToDecimal(optionLojaaliquotaPis)));
                            optionProdutovalorPis = flagNumeric.Replace(",", ".").Trim();
                        }
                        else
                        {
                            optionProdutobasePis = "0.00";
                            optionProdutovalorPis = "0.00";
                        }


                        //Update 4 COFINS
                        if (optionProdutoPisCofinsSt == "n")
                        {
                            optionProdutobaseCofins = txtValorBrutoItensNotaFiscal.Text;
                            flagNumeric = String.Format("{0:F2}", calculoImpostos.calcCofins(Convert.ToDecimal(txtValorBrutoItensNotaFiscal.Text), Convert.ToDecimal(optionLojaAliquotaCofins)));
                            optionProdutovalorCofins = flagNumeric.Replace(",", ".").Trim();
                        }
                        else
                        {
                            optionProdutobaseCofins = "0.00";
                            optionProdutovalorCofins = "0.00";
                        }

                    }
                    //Update 5 calc valor total liquido
                    if (optionProdutopriceCusto != "")
                    {
                        flagNumeric = calculoImpostos.calcSumProdutoLiquido(Convert.ToDecimal(txtValorUnComercialTributarioItensNotaFiscal.Text), Convert.ToDecimal(txtQttyComercialTributavelItensNotaFiscal.Text), despesas, discount, 0, Convert.ToDecimal(txtValorIPIItensNotaFiscal.Text), 0).ToString(); //revisar
                        optionProdutovalorTotalLiquido = flagNumeric.Replace(",", ".").Trim();
                        //optionProdutovalorTotalNota = flagNumeric.Replace(",", ".").Trim();
                    }
                    //Update 6 total Total Nota
                    if (optionProdutopriceCusto != "")
                    {
                        flagNumeric = calculoImpostos.calcSumProdutoNota(Convert.ToDecimal(txtValorUnComercialTributarioItensNotaFiscal.Text), Convert.ToDecimal(txtQttyComercialTributavelItensNotaFiscal.Text), despesas, discount, 0, Convert.ToDecimal(txtValorIPIItensNotaFiscal.Text), 0).ToString(); //revisar
                        optionProdutovalorTotalNota = flagNumeric.Replace(",", ".").Trim();
                    }
                    //Update 7 total Total Nota
                    if (optionProdutoValorLiquido != "")
                    {
                        flagNumeric = calculoImpostos.calcSumProdutoLiquido(Convert.ToDecimal(txtValorUnComercialTributarioItensNotaFiscal.Text), 1, despesas, discount, 0, Convert.ToDecimal(txtValorIPIItensNotaFiscal.Text), 0).ToString(); //revisar
                        optionProdutoValorLiquido = flagNumeric.Replace(",", ".").Trim();
                    }   
                }
                txtDescontoItensNotaFiscal.Select();
            }
        }

        private void txtDescontoItensNotaFiscal_Validated(object sender, EventArgs e)
        {
            if (txtDescontoItensNotaFiscal.Text != "")
            {
                auxNumeric = Convert.ToDecimal(txtDescontoItensNotaFiscal.Text);//currency
                flagNumeric = String.Format("{0:F2}", auxNumeric);
                txtDescontoItensNotaFiscal.Text = flagNumeric.Replace(",", ".").Trim();
            }
            else
            {
                txtDescontoItensNotaFiscal.Text = "0.00";
            }

            if (txtQttyComercialTributavelItensNotaFiscal.Text != "") { qtty = Convert.ToDecimal(txtQttyComercialTributavelItensNotaFiscal.Text); } else { qtty = 0; }
            if (txtOutrasDespesasAcessoriasItensNotaFiscal.Text != "") { despesas = Convert.ToDecimal(txtOutrasDespesasAcessoriasItensNotaFiscal.Text); } else { despesas = 0; }

            if (txtDescontoItensNotaFiscal.Text == "" && Convert.ToDecimal(txtDescontoItensNotaFiscal.Text) < 0)
            {
                MessageBox.Show("Desconto Inválido.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDescontoItensNotaFiscal.Select();
            }
            else
            {
                if (txtValorUnComercialTributarioItensNotaFiscal.Text != "" && txtValorUnComercialTributarioItensNotaFiscal.Text != "0.00" && Convert.ToDecimal(txtValorUnComercialTributarioItensNotaFiscal.Text) > 0 && qtty > 0)
                {
                    //Update 0
                    txtValorBrutoItensNotaFiscal.Text = calculoImpostos.calcSumProduto(Convert.ToDecimal(txtValorUnComercialTributarioItensNotaFiscal.Text), qtty, despesas, Convert.ToDecimal(txtDescontoItensNotaFiscal.Text)).ToString();

                    if (txtValorBrutoItensNotaFiscal.Text != "")
                    {
                        auxNumeric = Convert.ToDecimal(txtValorBrutoItensNotaFiscal.Text);//currency
                        flagNumeric = String.Format("{0:F2}", auxNumeric);
                        txtValorBrutoItensNotaFiscal.Text = flagNumeric.Replace(",", ".").Trim();

                        if (optionProdutocsttypeCst == "s")
                        {
                            optionSomaProdutoFinanceiro = calculoImpostos.calcSumProduto(Convert.ToDecimal(txtValorUnComercialTributarioItensNotaFiscal.Text), Convert.ToDecimal(txtQttyComercialTributavelItensNotaFiscal.Text), Convert.ToDecimal(txtOutrasDespesasAcessoriasItensNotaFiscal.Text), 0).ToString();

                            if (optionProdutocstCalcBc == "1")
                            {
                                optionProdutobaseCalculoIcmsSubstituicao = calculo.calcBaseCalculoST(Convert.ToDecimal(optionSomaProdutoFinanceiro), Convert.ToDecimal(optionProdutoMVAAjusteMva)).ToString();
                                //######################################################Base destacada ST################################################
                                //Update 1 st
                                txtBaseIcmsItensNotaFiscal.Text = calculoImpostos.calcBaseCalculo(Convert.ToDecimal(txtValorBrutoItensNotaFiscal.Text), Convert.ToDecimal(optionProdutoreducao)).ToString();
                                if (txtBaseIcmsItensNotaFiscal.Text != "")
                                {
                                    auxNumeric = Convert.ToDecimal(txtBaseIcmsItensNotaFiscal.Text);//currency
                                    flagNumeric = String.Format("{0:F2}", auxNumeric);
                                    txtBaseIcmsItensNotaFiscal.Text = flagNumeric.Replace(",", ".").Trim();
                                }

                                //Update 2 st
                                txtValorIcmsItensNotaFiscal.Text = calculoImpostos.calcIcms(Convert.ToDecimal(txtValorBrutoItensNotaFiscal.Text), Convert.ToDecimal(optionProdutoaliquota)).ToString();
                                if (txtValorIcmsItensNotaFiscal.Text != "")
                                {
                                    auxNumeric = Convert.ToDecimal(txtValorIcmsItensNotaFiscal.Text);//currency
                                    flagNumeric = String.Format("{0:F2}", auxNumeric);
                                    txtValorIcmsItensNotaFiscal.Text = flagNumeric.Replace(",", ".").Trim();
                                }
                                //######################################################End Base destacada ST###########################################
                                if (optionProdutocstCalcRed == "1")
                                {
                                    optionSomaProdutoFinanceiro = calculoImpostos.calcBaseCalculo(Convert.ToDecimal(optionSomaProdutoFinanceiro), Convert.ToDecimal(optionProdutoreducao)).ToString();
                                    optionProdutobaseCalculoIcmsSubstituicao = calculo.calcBaseCalculoST(Convert.ToDecimal(optionSomaProdutoFinanceiro), Convert.ToDecimal(optionProdutoMVAAjusteMva)).ToString();
                                }
                                else
                                {
                                    txtReducaoBaseCalculoItensNotaFiscal.Text = "0.00";
                                }
                                optionProdutoIcmsSt = calculo.calcIcmsST(Convert.ToDecimal(optionProdutobaseCalculoIcmsSubstituicao), Convert.ToDecimal(optionProdutoMVAInterna)).ToString();
                                optionProdutoIcmsStRecolher = calculo.calcIcmsArecolher(Convert.ToDecimal(optionProdutoIcmsSt), calculoImpostos.calcIcms(Convert.ToDecimal(txtValorBrutoItensNotaFiscal.Text), Convert.ToDecimal(optionProdutoaliquota))).ToString();

                                txtAliquotaIcmsSTItensNotaFiscal.Text = optionProdutoMVAInterna;

                                auxNumeric = Convert.ToDecimal(optionProdutobaseCalculoIcmsSubstituicao);//currency Convertendo
                                flagNumeric = String.Format("{0:F2}", auxNumeric);
                                txtBaseIcmsSTItensNotaFiscal.Text = flagNumeric.Replace(",", ".").Trim();

                                txtReducaoBaseCalculoSTItensNotaFiscal.Text = optionProdutoreducao;

                                auxNumeric = Convert.ToDecimal(optionProdutoIcmsStRecolher);//currency Convertendo
                                flagNumeric = String.Format("{0:F2}", auxNumeric);
                                txtValorIcmsSTItensNotaFiscal.Text = flagNumeric.Replace(",", ".").Trim();
                            }
                            else
                            {
                                txtBaseIcmsItensNotaFiscal.Text = "0.00";
                                txtReducaoBaseCalculoItensNotaFiscal.Text = "0.00";
                            }

                            if (optionProdutocstModalidade == "m")
                            {

                            }
                        }
                        else
                        {
                            //Update 1
                            txtBaseIcmsItensNotaFiscal.Text = calculoImpostos.calcBaseCalculo(Convert.ToDecimal(txtValorBrutoItensNotaFiscal.Text), Convert.ToDecimal(optionProdutoreducao)).ToString();
                            if (txtBaseIcmsItensNotaFiscal.Text != "")
                            {
                                auxNumeric = Convert.ToDecimal(txtBaseIcmsItensNotaFiscal.Text);//currency
                                flagNumeric = String.Format("{0:F2}", auxNumeric);
                                txtBaseIcmsItensNotaFiscal.Text = flagNumeric.Replace(",", ".").Trim();
                            }
                            //Update 2
                            txtValorIcmsItensNotaFiscal.Text = calculoImpostos.calcIcms(Convert.ToDecimal(txtValorBrutoItensNotaFiscal.Text), Convert.ToDecimal(optionProdutoaliquota)).ToString();
                            if (txtValorIcmsItensNotaFiscal.Text != "")
                            {
                                auxNumeric = Convert.ToDecimal(txtValorIcmsItensNotaFiscal.Text);//currency
                                flagNumeric = String.Format("{0:F2}", auxNumeric);
                                txtValorIcmsItensNotaFiscal.Text = flagNumeric.Replace(",", ".").Trim();
                            }
                            //Update 3 PIS
                            if (optionProdutoPisSt == "n")
                            {
                                optionProdutobasePis = txtValorBrutoItensNotaFiscal.Text;
                                flagNumeric = String.Format("{0:F2}", calculoImpostos.calcPis(Convert.ToDecimal(txtValorBrutoItensNotaFiscal.Text), Convert.ToDecimal(optionLojaaliquotaPis)));
                                optionProdutovalorPis = flagNumeric.Replace(",", ".").Trim();
                            }
                            else
                            {
                                optionProdutobasePis = "0.00";
                                optionProdutovalorPis = "0.00";
                            }


                            //Update 4 COFINS
                            if (optionProdutoPisCofinsSt == "n")
                            {
                                optionProdutobaseCofins = txtValorBrutoItensNotaFiscal.Text;
                                flagNumeric = String.Format("{0:F2}", calculoImpostos.calcCofins(Convert.ToDecimal(txtValorBrutoItensNotaFiscal.Text), Convert.ToDecimal(optionLojaAliquotaCofins)));
                                optionProdutovalorCofins = flagNumeric.Replace(",", ".").Trim();
                            }
                            else
                            {
                                optionProdutobaseCofins = "0.00";
                                optionProdutovalorCofins = "0.00";
                            }
                        }
                        //Update 5 Valor Desconto
                        flagNumeric = String.Format("{0:F2}", calculoImpostos.calcValueDiscount((Convert.ToDecimal(txtValorUnComercialTributarioItensNotaFiscal.Text) * Convert.ToDecimal(txtQttyComercialTributavelItensNotaFiscal.Text)), Convert.ToDecimal(txtDescontoItensNotaFiscal.Text)));
                        optionProdutodescontoValor = flagNumeric.Replace(",", ".").Trim();
                        //Update 6 Valor Despesas
                        flagNumeric = String.Format("{0:F2}", calculoImpostos.calcValueDespesas((Convert.ToDecimal(txtValorUnComercialTributarioItensNotaFiscal.Text) * Convert.ToDecimal(txtQttyComercialTributavelItensNotaFiscal.Text)), Convert.ToDecimal(txtOutrasDespesasAcessoriasItensNotaFiscal.Text)));
                        optionProdutoacrescimoValor = flagNumeric.Replace(",", ".").Trim();

                        //Update 7 total liquido
                        if (optionProdutopriceCusto != "")
                        {
                            flagNumeric = calculoImpostos.calcSumProdutoLiquido(Convert.ToDecimal(txtValorUnComercialTributarioItensNotaFiscal.Text), Convert.ToDecimal(txtQttyComercialTributavelItensNotaFiscal.Text), despesas, Convert.ToDecimal(txtDescontoItensNotaFiscal.Text), 0, Convert.ToDecimal(txtValorIPIItensNotaFiscal.Text), 0).ToString();
                            optionProdutovalorTotalLiquido = flagNumeric.Replace(",", ".").Trim();
                        }
                        //Update 8 total Total Nota
                        if (optionProdutopriceCusto != "")
                        {
                            flagNumeric = calculoImpostos.calcSumProdutoNota(Convert.ToDecimal(txtValorUnComercialTributarioItensNotaFiscal.Text), Convert.ToDecimal(txtQttyComercialTributavelItensNotaFiscal.Text), despesas, Convert.ToDecimal(txtDescontoItensNotaFiscal.Text), 0, Convert.ToDecimal(txtValorIPIItensNotaFiscal.Text), 0).ToString(); //revisar
                            optionProdutovalorTotalNota = flagNumeric.Replace(",", ".").Trim();
                        }
                        //Update 9 total Total Nota
                        if (optionProdutopriceCusto != "")
                        {
                            flagNumeric = calculoImpostos.calcSumProdutoLiquido(Convert.ToDecimal(txtValorUnComercialTributarioItensNotaFiscal.Text), 1, despesas, Convert.ToDecimal(txtDescontoItensNotaFiscal.Text), 0, Convert.ToDecimal(txtValorIPIItensNotaFiscal.Text), 0).ToString();
                            optionProdutoValorLiquido = flagNumeric.Replace(",", ".").Trim();
                        }
                    }
                }
            }
        }

        private void txtOutrasDespesasAcessoriasItensNotaFiscal_Validated(object sender, EventArgs e)
        {
            if (txtOutrasDespesasAcessoriasItensNotaFiscal.Text != "")
            {
                auxNumeric = Convert.ToDecimal(txtOutrasDespesasAcessoriasItensNotaFiscal.Text);//currency
                flagNumeric = String.Format("{0:F2}", auxNumeric);
                txtOutrasDespesasAcessoriasItensNotaFiscal.Text = flagNumeric.Replace(",", ".").Trim();
            }
            else
            {
                txtOutrasDespesasAcessoriasItensNotaFiscal.Text = "0.00";
            }

            if (txtQttyComercialTributavelItensNotaFiscal.Text != "") { qtty = Convert.ToDecimal(txtQttyComercialTributavelItensNotaFiscal.Text); } else { qtty = 0; }
            if (txtDescontoItensNotaFiscal.Text != "") { discount = Convert.ToDecimal(txtDescontoItensNotaFiscal.Text); } else { discount = 0; }

            if (txtOutrasDespesasAcessoriasItensNotaFiscal.Text == "" && Convert.ToDecimal(txtOutrasDespesasAcessoriasItensNotaFiscal.Text) < 0)
            {
                MessageBox.Show("Despesas Inválido.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtOutrasDespesasAcessoriasItensNotaFiscal.Select();
            }
            else
            {
                if (txtValorUnComercialTributarioItensNotaFiscal.Text != "" && txtValorUnComercialTributarioItensNotaFiscal.Text != "0.00" && Convert.ToDecimal(txtValorUnComercialTributarioItensNotaFiscal.Text) > 0 && qtty > 0)
                {
                    //Update 0
                    txtValorBrutoItensNotaFiscal.Text = calculoImpostos.calcSumProduto(Convert.ToDecimal(txtValorUnComercialTributarioItensNotaFiscal.Text), qtty,Convert.ToDecimal(txtOutrasDespesasAcessoriasItensNotaFiscal.Text), discount).ToString();

                    if (txtValorBrutoItensNotaFiscal.Text != "")
                    {
                        auxNumeric = Convert.ToDecimal(txtValorBrutoItensNotaFiscal.Text);//currency
                        flagNumeric = String.Format("{0:F2}", auxNumeric);
                        txtValorBrutoItensNotaFiscal.Text = flagNumeric.Replace(",", ".").Trim();
                    }

                    if (optionProdutocsttypeCst == "s")
                    {
                        optionSomaProdutoFinanceiro = calculoImpostos.calcSumProduto(Convert.ToDecimal(txtValorUnComercialTributarioItensNotaFiscal.Text), Convert.ToDecimal(txtQttyComercialTributavelItensNotaFiscal.Text), Convert.ToDecimal(txtOutrasDespesasAcessoriasItensNotaFiscal.Text), 0).ToString();

                        if (optionProdutocstCalcBc == "1")
                        {
                            optionProdutobaseCalculoIcmsSubstituicao = calculo.calcBaseCalculoST(Convert.ToDecimal(optionSomaProdutoFinanceiro), Convert.ToDecimal(optionProdutoMVAAjusteMva)).ToString();
                            //######################################################Base destacada ST################################################
                            //Update 1 st
                            txtBaseIcmsItensNotaFiscal.Text = calculoImpostos.calcBaseCalculo(Convert.ToDecimal(txtValorBrutoItensNotaFiscal.Text), Convert.ToDecimal(optionProdutoreducao)).ToString();
                            if (txtBaseIcmsItensNotaFiscal.Text != "")
                            {
                                auxNumeric = Convert.ToDecimal(txtBaseIcmsItensNotaFiscal.Text);//currency
                                flagNumeric = String.Format("{0:F2}", auxNumeric);
                                txtBaseIcmsItensNotaFiscal.Text = flagNumeric.Replace(",", ".").Trim();
                            }

                            //Update 2 st
                            txtValorIcmsItensNotaFiscal.Text = calculoImpostos.calcIcms(Convert.ToDecimal(txtValorBrutoItensNotaFiscal.Text), Convert.ToDecimal(optionProdutoaliquota)).ToString();
                            if (txtValorIcmsItensNotaFiscal.Text != "")
                            {
                                auxNumeric = Convert.ToDecimal(txtValorIcmsItensNotaFiscal.Text);//currency
                                flagNumeric = String.Format("{0:F2}", auxNumeric);
                                txtValorIcmsItensNotaFiscal.Text = flagNumeric.Replace(",", ".").Trim();
                            }
                            //######################################################End Base destacada ST###########################################
                            if (optionProdutocstCalcRed == "1")
                            {
                                optionSomaProdutoFinanceiro = calculoImpostos.calcBaseCalculo(Convert.ToDecimal(optionSomaProdutoFinanceiro), Convert.ToDecimal(optionProdutoreducao)).ToString();
                                optionProdutobaseCalculoIcmsSubstituicao = calculo.calcBaseCalculoST(Convert.ToDecimal(optionSomaProdutoFinanceiro), Convert.ToDecimal(optionProdutoMVAAjusteMva)).ToString();
                            }
                            else
                            {
                                txtReducaoBaseCalculoItensNotaFiscal.Text = "0.00";
                            }
                            optionProdutoIcmsSt = calculo.calcIcmsST(Convert.ToDecimal(optionProdutobaseCalculoIcmsSubstituicao), Convert.ToDecimal(optionProdutoMVAInterna)).ToString();
                            optionProdutoIcmsStRecolher = calculo.calcIcmsArecolher(Convert.ToDecimal(optionProdutoIcmsSt), calculoImpostos.calcIcms(Convert.ToDecimal(txtValorBrutoItensNotaFiscal.Text), Convert.ToDecimal(optionProdutoaliquota))).ToString();

                            txtAliquotaIcmsSTItensNotaFiscal.Text = optionProdutoMVAInterna;

                            auxNumeric = Convert.ToDecimal(optionProdutobaseCalculoIcmsSubstituicao);//currency Convertendo
                            flagNumeric = String.Format("{0:F2}", auxNumeric);
                            txtBaseIcmsSTItensNotaFiscal.Text = flagNumeric.Replace(",", ".").Trim();

                            txtReducaoBaseCalculoSTItensNotaFiscal.Text = optionProdutoreducao;

                            auxNumeric = Convert.ToDecimal(optionProdutoIcmsStRecolher);//currency Convertendo
                            flagNumeric = String.Format("{0:F2}", auxNumeric);
                            txtValorIcmsSTItensNotaFiscal.Text = flagNumeric.Replace(",", ".").Trim();



                        }
                        else
                        {
                            txtBaseIcmsItensNotaFiscal.Text = "0.00";
                            txtReducaoBaseCalculoItensNotaFiscal.Text = "0.00";
                        }

                        if (optionProdutocstModalidade == "m")
                        {

                        }
                    }
                    else
                    {
                        //Update 1
                        txtBaseIcmsItensNotaFiscal.Text = calculoImpostos.calcBaseCalculo(Convert.ToDecimal(txtValorBrutoItensNotaFiscal.Text), Convert.ToDecimal(optionProdutoreducao)).ToString();
                        if (txtBaseIcmsItensNotaFiscal.Text != "")
                        {
                            auxNumeric = Convert.ToDecimal(txtBaseIcmsItensNotaFiscal.Text);//currency
                            flagNumeric = String.Format("{0:F2}", auxNumeric);
                            txtBaseIcmsItensNotaFiscal.Text = flagNumeric.Replace(",", ".").Trim();
                        }
                        //Update 2
                        txtValorIcmsItensNotaFiscal.Text = calculoImpostos.calcIcms(Convert.ToDecimal(txtValorBrutoItensNotaFiscal.Text), Convert.ToDecimal(optionProdutoaliquota)).ToString();
                        if (txtValorIcmsItensNotaFiscal.Text != "")
                        {
                            auxNumeric = Convert.ToDecimal(txtValorIcmsItensNotaFiscal.Text);//currency
                            flagNumeric = String.Format("{0:F2}", auxNumeric);
                            txtValorIcmsItensNotaFiscal.Text = flagNumeric.Replace(",", ".").Trim();
                        }
                        //Update 3 PIS
                        if (optionProdutoPisSt == "n")
                        {
                            optionProdutobasePis = txtValorBrutoItensNotaFiscal.Text;
                            flagNumeric = String.Format("{0:F2}", calculoImpostos.calcPis(Convert.ToDecimal(txtValorBrutoItensNotaFiscal.Text), Convert.ToDecimal(optionLojaaliquotaPis)));
                            optionProdutovalorPis = flagNumeric.Replace(",", ".").Trim();
                        }
                        else
                        {
                            optionProdutobasePis = "0.00";
                            optionProdutovalorPis = "0.00";
                        }


                        //Update 4 COFINS
                        if (optionProdutoPisCofinsSt == "n")
                        {
                            optionProdutobaseCofins = txtValorBrutoItensNotaFiscal.Text;
                            flagNumeric = String.Format("{0:F2}", calculoImpostos.calcCofins(Convert.ToDecimal(txtValorBrutoItensNotaFiscal.Text), Convert.ToDecimal(optionLojaAliquotaCofins)));
                            optionProdutovalorCofins = flagNumeric.Replace(",", ".").Trim();
                        }
                        else
                        {
                            optionProdutobaseCofins = "0.00";
                            optionProdutovalorCofins = "0.00";
                        }
                        //Update 5 Valor Desconto
                        optionProdutodescontoValor = String.Format("{0:F2}", calculoImpostos.calcValueDiscount((Convert.ToDecimal(txtValorUnComercialTributarioItensNotaFiscal.Text) * Convert.ToDecimal(txtQttyComercialTributavelItensNotaFiscal.Text)), Convert.ToDecimal(txtDescontoItensNotaFiscal.Text)));
                        //Update 6 Valor Despesas
                        optionProdutoacrescimoValor = String.Format("{0:F2}", calculoImpostos.calcValueDespesas((Convert.ToDecimal(txtValorUnComercialTributarioItensNotaFiscal.Text) * Convert.ToDecimal(txtQttyComercialTributavelItensNotaFiscal.Text)), Convert.ToDecimal(txtOutrasDespesasAcessoriasItensNotaFiscal.Text)));
                        //Update 7 custo liquido
                        if (optionProdutopriceCusto != "")
                        {
                            flagNumeric = calculoImpostos.calcSumProdutoLiquido(Convert.ToDecimal(txtValorUnComercialTributarioItensNotaFiscal.Text), Convert.ToDecimal(txtQttyComercialTributavelItensNotaFiscal.Text), Convert.ToDecimal(txtOutrasDespesasAcessoriasItensNotaFiscal.Text), discount, 0, Convert.ToDecimal(txtValorIPIItensNotaFiscal.Text), 0).ToString(); //revisar
                            optionProdutovalorTotalLiquido = flagNumeric.Replace(",", ".").Trim();
                        }
                        //Update 8 custo liquido
                        if (optionProdutopriceCusto != "")
                        {
                            flagNumeric = calculoImpostos.calcSumProdutoNota(Convert.ToDecimal(txtValorUnComercialTributarioItensNotaFiscal.Text), Convert.ToDecimal(txtQttyComercialTributavelItensNotaFiscal.Text), Convert.ToDecimal(txtOutrasDespesasAcessoriasItensNotaFiscal.Text), discount, 0, Convert.ToDecimal(txtValorIPIItensNotaFiscal.Text), 0).ToString(); //revisar
                            optionProdutovalorTotalNota = flagNumeric.Replace(",", ".").Trim();
                        }
                        //Update 9 total Total Nota
                        if (optionProdutopriceCusto != "")
                        {
                            flagNumeric = calculoImpostos.calcSumProdutoLiquido(Convert.ToDecimal(txtValorUnComercialTributarioItensNotaFiscal.Text), 1, Convert.ToDecimal(txtOutrasDespesasAcessoriasItensNotaFiscal.Text), discount, 0, Convert.ToDecimal(txtValorIPIItensNotaFiscal.Text), 0).ToString(); //revisar
                            optionProdutoValorLiquido = flagNumeric.Replace(",", ".").Trim();
                        }
                    }
                }
            }
        }

        private void txtTotalSeguroItensNotaFiscal_Validated(object sender, EventArgs e)
        {
            if (txtTotalSeguroItensNotaFiscal.Text != "")
            {
                auxNumeric = Convert.ToDecimal(txtTotalSeguroItensNotaFiscal.Text);//currency
                flagNumeric = String.Format("{0:F2}", auxNumeric);
                txtTotalSeguroItensNotaFiscal.Text = flagNumeric.Replace(",", ".").Trim();
            }
            else
            {
                txtTotalSeguroItensNotaFiscal.Text = "0.00";
            }
        }

        private void txtValorUnComercialTributarioItensNotaFiscal_Validated(object sender, EventArgs e)
        {
            if (txtValorUnComercialTributarioItensNotaFiscal.Text != "")
            {
                auxNumeric = Convert.ToDecimal(txtValorUnComercialTributarioItensNotaFiscal.Text);//currency
                flagNumeric = String.Format("{0:F2}", auxNumeric);
                txtValorUnComercialTributarioItensNotaFiscal.Text = flagNumeric.Replace(",", ".").Trim();
            }
            else
            {
                txtValorUnComercialTributarioItensNotaFiscal.Text = "0.00";
            }
        }

        private void txtTotalFreteItensNotaFiscal_Validated(object sender, EventArgs e)
        {
            if (auxModalidadeFrete != 9)
            {
                if (lbTransporteRazaoNotaFiscal.Text != "")
                {
                    if (txtTotalFreteItensNotaFiscal.Text != "")
                    {
                        auxNumeric = Convert.ToDecimal(txtTotalFreteItensNotaFiscal.Text);//currency
                        flagNumeric = String.Format("{0:F2}", auxNumeric);
                        txtTotalFreteItensNotaFiscal.Text = flagNumeric.Replace(",", ".").Trim();
                    }
                    else
                    {
                        txtTotalFreteItensNotaFiscal.Text = "0.00";
                    }
                }
                else
                {
                    MessageBox.Show("Não há transportadora carregada para alimentar tal campo.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTotalFreteItensNotaFiscal.Text = "0.00";
                    tbcNotaSaida.SelectedIndex = 0;
                }
            }
            else
            {
                MessageBox.Show("Modalidade [9 - Sem Frete] selecionada, impossível prosseguir.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTotalFreteItensNotaFiscal.Text = "0.00";
            }
        }

        private void txtValorBrutoItensNotaFiscal_Validated(object sender, EventArgs e)
        {
            if (txtValorBrutoItensNotaFiscal.Text != "")
            {
                auxNumeric = Convert.ToDecimal(txtValorBrutoItensNotaFiscal.Text);//currency
                flagNumeric = String.Format("{0:F2}", auxNumeric);
                txtValorBrutoItensNotaFiscal.Text = flagNumeric.Replace(",", ".").Trim();
            }
            else
            {
                txtValorBrutoItensNotaFiscal.Text = "0.00";
            }
        }

        private void txtValorIcmsItensNotaFiscal_Validated(object sender, EventArgs e)
        {
            if (txtValorIcmsItensNotaFiscal.Text != "")
            {
                auxNumeric = Convert.ToDecimal(txtValorIcmsItensNotaFiscal.Text);//currency
                flagNumeric = String.Format("{0:F2}", auxNumeric);
                txtValorIcmsItensNotaFiscal.Text = flagNumeric.Replace(",", ".").Trim();
            }
            else
            {
                txtValorIcmsItensNotaFiscal.Text = "0.00";
            }
        }

        private void txtBaseIcmsItensNotaFiscal_Validated(object sender, EventArgs e)
        {
            if (txtBaseIcmsItensNotaFiscal.Text != "")
            {
                auxNumeric = Convert.ToDecimal(txtBaseIcmsItensNotaFiscal.Text);//currency
                flagNumeric = String.Format("{0:F2}", auxNumeric);
                txtBaseIcmsItensNotaFiscal.Text = flagNumeric.Replace(",", ".").Trim();
            }
            else
            {
                txtBaseIcmsItensNotaFiscal.Text = "0.00";
            }
        }

        private void txtReducaoBaseCalculoItensNotaFiscal_Validated(object sender, EventArgs e)
        {
            if (txtReducaoBaseCalculoItensNotaFiscal.Text != "")
            {
                auxNumeric = Convert.ToDecimal(txtReducaoBaseCalculoItensNotaFiscal.Text);//currency
                flagNumeric = String.Format("{0:F2}", auxNumeric);
                txtReducaoBaseCalculoItensNotaFiscal.Text = flagNumeric.Replace(",", ".").Trim();
            }
            else
            {
                txtReducaoBaseCalculoItensNotaFiscal.Text = "0.00";
            }
        }

        private void txtAliquotaIcmsItensNotaFiscal_Validated(object sender, EventArgs e)
        {
            if (txtAliquotaIcmsItensNotaFiscal.Text != "")
            {
                auxNumeric = Convert.ToDecimal(txtAliquotaIcmsItensNotaFiscal.Text);//currency
                flagNumeric = String.Format("{0:F2}", auxNumeric);
                txtAliquotaIcmsItensNotaFiscal.Text = flagNumeric.Replace(",", ".").Trim();
            }
            else
            {
                txtAliquotaIcmsItensNotaFiscal.Text = "0.00";
            }
        }

        private void lnkSalvarItemNotaFiscal_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            conector_obj_salve_item();
        }

        private void txtAliquotaIPIItensNotaFiscal_Validated(object sender, EventArgs e)
        {
            if (txtAliquotaIPIItensNotaFiscal.Text != "")
            {
                auxNumeric = Convert.ToDecimal(txtAliquotaIPIItensNotaFiscal.Text);//currency
                flagNumeric = String.Format("{0:F2}", auxNumeric);
                txtAliquotaIPIItensNotaFiscal.Text = flagNumeric.Replace(",", ".").Trim();
            }
            else
            {
                txtAliquotaIPIItensNotaFiscal.Text = "0.00";
            }

            if (txtQttyComercialTributavelItensNotaFiscal.Text != "") { discount = Convert.ToDecimal(txtQttyComercialTributavelItensNotaFiscal.Text); } else { qtty = 0; }
            if (txtDescontoItensNotaFiscal.Text != "") { discount = Convert.ToDecimal(txtDescontoItensNotaFiscal.Text); } else { discount = 0; }
            if (txtOutrasDespesasAcessoriasItensNotaFiscal.Text != "") { despesas = Convert.ToDecimal(txtOutrasDespesasAcessoriasItensNotaFiscal.Text); } else { despesas = 0; }

            if (txtAliquotaIPIItensNotaFiscal.Text == "" || txtAliquotaIPIItensNotaFiscal.Text == "0.00" || Convert.ToDecimal(txtAliquotaIPIItensNotaFiscal.Text) <= 0)
            {
                MessageBox.Show("Quantidade obrigatória.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAliquotaIPIItensNotaFiscal.Select();
            }
            else
            {
                if (txtValorUnComercialTributarioItensNotaFiscal.Text != "" && txtValorUnComercialTributarioItensNotaFiscal.Text != "0.00" && Convert.ToDecimal(txtValorUnComercialTributarioItensNotaFiscal.Text) > 0)
                {
                    //Update 0
                    txtValorBrutoItensNotaFiscal.Text = calculoImpostos.calcSumProduto(Convert.ToDecimal(txtValorUnComercialTributarioItensNotaFiscal.Text), qtty, despesas, discount).ToString();
                    if (txtValorBrutoItensNotaFiscal.Text != "")
                    {
                        auxNumeric = Convert.ToDecimal(txtValorBrutoItensNotaFiscal.Text);//currency
                        flagNumeric = String.Format("{0:F2}", auxNumeric);
                        txtValorBrutoItensNotaFiscal.Text = flagNumeric.Replace(",", ".").Trim();
                    }
                    if (optionProdutocstCalcBc == "0")
                    {
                        txtBaseIcmsItensNotaFiscal.Text = "0.00";
                    }
                    else
                    {
                        //Update 1
                        txtBaseIcmsItensNotaFiscal.Text = calculoImpostos.calcBaseCalculo(Convert.ToDecimal(txtValorBrutoItensNotaFiscal.Text), Convert.ToDecimal(optionProdutoreducao)).ToString();
                        if (txtBaseIcmsItensNotaFiscal.Text != "")
                        {
                            auxNumeric = Convert.ToDecimal(txtBaseIcmsItensNotaFiscal.Text);//currency
                            flagNumeric = String.Format("{0:F2}", auxNumeric);
                            txtBaseIcmsItensNotaFiscal.Text = flagNumeric.Replace(",", ".").Trim();
                        }
                        //Update 2
                        txtValorIcmsItensNotaFiscal.Text = calculoImpostos.calcIcms(Convert.ToDecimal(txtValorBrutoItensNotaFiscal.Text), Convert.ToDecimal(optionProdutoaliquota)).ToString();
                        if (txtValorIcmsItensNotaFiscal.Text != "")
                        {
                            auxNumeric = Convert.ToDecimal(txtValorIcmsItensNotaFiscal.Text);//currency
                            flagNumeric = String.Format("{0:F2}", auxNumeric);
                            txtValorIcmsItensNotaFiscal.Text = flagNumeric.Replace(",", ".").Trim();
                        }

                        //Update 3 PIS
                        if (optionProdutoPisSt == "n")
                        {
                            optionProdutobasePis = txtValorBrutoItensNotaFiscal.Text;
                            flagNumeric = String.Format("{0:F2}", calculoImpostos.calcPis(Convert.ToDecimal(txtValorBrutoItensNotaFiscal.Text), Convert.ToDecimal(optionLojaaliquotaPis)));
                            optionProdutovalorPis = flagNumeric.Replace(",", ".").Trim();
                        }
                        else
                        {
                            optionProdutobasePis = "0.00";
                            optionProdutovalorPis = "0.00";
                        }


                        //Update 4 COFINS
                        if (optionProdutoPisCofinsSt == "n")
                        {
                            optionProdutobaseCofins = txtValorBrutoItensNotaFiscal.Text;
                            flagNumeric = String.Format("{0:F2}", calculoImpostos.calcCofins(Convert.ToDecimal(txtValorBrutoItensNotaFiscal.Text), Convert.ToDecimal(optionLojaAliquotaCofins)));
                            optionProdutovalorCofins = flagNumeric.Replace(",", ".").Trim();
                        }
                        else
                        {
                            optionProdutobaseCofins = "0.00";
                            optionProdutovalorCofins = "0.00";
                        }
                        //Update 5 total liquido
                        if (optionProdutopriceCusto != "")
                        {
                            flagNumeric = calculoImpostos.calcSumProdutoLiquido(Convert.ToDecimal(txtValorUnComercialTributarioItensNotaFiscal.Text), qtty, despesas, discount, 0, Convert.ToDecimal(txtValorIPIItensNotaFiscal.Text), 0).ToString(); //revisar
                            optionProdutovalorTotalLiquido = flagNumeric.Replace(",", ".").Trim();
                            optionProdutovalorTotalNota = flagNumeric.Replace(",", ".").Trim();
                        }
                        //Update 5 total Nota
                        if (optionProdutopriceCusto != "")
                        {
                            flagNumeric = calculoImpostos.calcSumProdutoNota(Convert.ToDecimal(txtValorUnComercialTributarioItensNotaFiscal.Text),qtty, despesas, discount, 0, Convert.ToDecimal(txtValorIPIItensNotaFiscal.Text), 0).ToString(); //revisar
                            optionProdutovalorTotalNota = flagNumeric.Replace(",", ".").Trim();
                        }

                        //Update 5 total unitario liquido
                        if (optionProdutopriceCusto != "")
                        {
                            flagNumeric = calculoImpostos.calcSumProdutoLiquido(Convert.ToDecimal(txtValorUnComercialTributarioItensNotaFiscal.Text), 1, despesas, discount, 0, Convert.ToDecimal(txtValorIPIItensNotaFiscal.Text), 0).ToString(); //revisar
                            optionProdutoValorLiquido = flagNumeric.Replace(",", ".").Trim();
                        }

                    }

                }
                txtDescontoItensNotaFiscal.Select();
            }
        }

        private void dgvItensNotaFiscal_Click(object sender, EventArgs e)
        {
            flagRetornoTotalItens = conector_full_verificaItem();
            if ((flagRetornoTotalItens > 0))
            {
                auxIdProduto = dgvItensNotaFiscal.CurrentRow.Cells[1].Value.ToString();
                auxIdNfItem = dgvItensNotaFiscal.CurrentRow.Cells[0].Value.ToString();
                flagSemaforoItem = 1;
                txtQttyComercialTributavelItensNotaFiscal.Select();
                    ataque = 0;
                    //Concluir parciais
                    if (auxIdProduto != "" && Convert.ToDouble(auxIdProduto) > 0)
                    {
                        txtCodigoItensNotaFiscal.ReadOnly = true;
                        conector_valida_item(auxIdProduto, 0);  
                    }
                    conector_corregaConfig_itemNfSaida();
                    conector_find_itemNfSaidaTotais();
                    ataque = 1;

                
            }
        }

        private void dgvItensNotaFiscal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((flagRetornoTotalItens > 0))
            {
                auxIdProduto = dgvItensNotaFiscal.CurrentRow.Cells[1].Value.ToString();
                auxIdNfItem = dgvItensNotaFiscal.CurrentRow.Cells[0].Value.ToString();
                if (auxIdProduto != "")
                {
                    txtCodigoItensNotaFiscal.Clear();
                    txtCodigoItensNotaFiscal.Select();
                    zeraObjItem();
                    zeraVariavelRecalculo();
                    conector_find_produto(auxIdProduto);
                }
                conector_find_itemNfSaidaTotais();
                flagSemaforoItem = 1;
            }
        }

        private void dgvItensNotaFiscal_KeyDown(object sender, KeyEventArgs e)
        {
            if ((flagRetornoTotalItens > 0) && dgvItensNotaFiscal.CurrentRow != null)
            {
                auxIdProduto = dgvItensNotaFiscal.CurrentRow.Cells[1].Value.ToString();
                auxIdNfItem = dgvItensNotaFiscal.CurrentRow.Cells[0].Value.ToString();
                ataque = 0;
                //Concluir parciais
                if (auxIdProduto != "")
                {
                    txtCodigoItensNotaFiscal.Clear();
                    txtCodigoItensNotaFiscal.Select();
                    zeraObjItem();
                    zeraVariavelRecalculo();
                    conector_find_produto(auxIdProduto);
                }
                conector_corregaConfig_itemNfSaida();
                //conector_find_totaisNotaFiscal();
                ataque = 1;
            }
        }

        private void lnkProcuraProdutoItemNotaFiscal_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pesquisaProd = new pesquisaProduto();
            if (pesquisaProd.ShowDialog(this) == DialogResult.OK)
            {
                auxIdProduto = pesquisaProd.Produto;
                txtCodigoItensNotaFiscal.Text = auxIdProduto;
                int count = conector_verifica(auxIdProduto);
                conector_find_codBarra(auxIdProduto);
                if (optionFlagRepetItemFaturamento == "1")
                {
                    if (count >= 1)
                    {
                        MessageBox.Show("Produto existe nesta nota fiscal!", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCodigoItensNotaFiscal.Clear();
                        txtCodigoItensNotaFiscal.Select();
                        zeraObjItem();
                        zeraVariavelRecalculo();
                    }
                    else { conector_find_produto(auxIdProduto); }
                }
                else { conector_find_produto(auxIdProduto); }
            }
        }

        private void btnInserirItensNotaFiscal_Click(object sender, EventArgs e)
        {

            txtCodigoItensNotaFiscal.ReadOnly = false;
            zeraObjItem();
            txtCodigoItensNotaFiscal.Select();
            flagSemaforoItem = 0;
        }

        private void btnSalvarItensNotaFiscal_Click(object sender, EventArgs e)
        {
            if (txtCodigoItensNotaFiscal.Text != "" && Convert.ToDouble(txtCodigoItensNotaFiscal.Text) > 0)
            {
                if (txtQttyComercialTributavelItensNotaFiscal.Text != "" && txtQttyComercialTributavelItensNotaFiscal.Text != "0.00" && Convert.ToDouble(txtQttyComercialTributavelItensNotaFiscal.Text) > 0)
                {
                    if (txtCFOPItensNotaFiscal.Text != "" && Convert.ToDouble(txtCFOPItensNotaFiscal.Text) > 0)
                    {
                        if (cmbUnidadeDefaultItensNotaFiscal.Text != "")
                        {
                            if (auxIdNf != "")
                            {
                                if (flagSemaforoItem == 0)
                                {
                                    conector_inc_nfItem();
                                    conector_find_itemNfSaidaTotais();
                                }
                                else
                                {
                                    conector_alt_nfItem();
                                    conector_find_itemNfSaidaTotais();
                                }
                            }
                            else
                            {
                                tbcNotaSaida.SelectedIndex = 0;
                                MessageBox.Show("Cabeçalho da nota incompleto, pressione F10 para validar! ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Tipo de unidade padrão não definido, impossível prosseguir! ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Codigo fiscal da operação inválido, impossível prosseguir! ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCFOPItensNotaFiscal.Select();
                    }
                }
                else
                {
                    MessageBox.Show("Quantidade inválida, impossível prosseguir! ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtQttyComercialTributavelItensNotaFiscal.Select();
                }

            }
            else
            {
                MessageBox.Show("Codigo inválido, impossivel prosseguir! ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCodigoItensNotaFiscal.Select();
            }
        }

        private void txtAliquotaIPIItensNotaFiscal_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtFolhaNotaFiscal_Validated(object sender, EventArgs e)
        {
            btnConsultaClienteNotaFiscal.Select();
        }

        private void txtQttyComercialTributavelItensNotaFiscal_KeyPress(object sender, KeyPressEventArgs e)
        {
            posSeparator = txtQttyComercialTributavelItensNotaFiscal.Text.IndexOf(".");
            if (e.KeyChar == ',') // Se for digitado "," ser  alterado para "."
                e.KeyChar = '.';
            //Se não for um numero e não  tecla backspace e nao for virgula, barra:
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (Char)Keys.Back && e.KeyChar != '.')
            {
                e.Handled = true;
                return;
            }
            if (e.KeyChar == '.' && posSeparator > -1)
            {
                e.Handled = true;
                return;
            }
        }

        private void txtDescontoItensNotaFiscal_KeyPress(object sender, KeyPressEventArgs e)
        {
            posSeparator = txtDescontoItensNotaFiscal.Text.IndexOf(".");
            if (e.KeyChar == ',') // Se for digitado "," ser  alterado para "."
                e.KeyChar = '.';
            //Se não for um numero e não  tecla backspace e nao for virgula, barra:
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (Char)Keys.Back && e.KeyChar != '.')
            {
                e.Handled = true;
                return;
            }
            if (e.KeyChar == '.' && posSeparator > -1)
            {
                e.Handled = true;
                return;
            }
        }

        private void txtValorUnComercialTributarioItensNotaFiscal_KeyPress(object sender, KeyPressEventArgs e)
        {
            posSeparator = txtValorUnComercialTributarioItensNotaFiscal.Text.IndexOf(".");
            if (e.KeyChar == ',') // Se for digitado "," ser  alterado para "."
                e.KeyChar = '.';
            //Se não for um numero e não  tecla backspace e nao for virgula, barra:
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (Char)Keys.Back && e.KeyChar != '.')
            {
                e.Handled = true;
                return;
            }
            if (e.KeyChar == '.' && posSeparator > -1)
            {
                e.Handled = true;
                return;
            }
        }

        private void txtTotalFreteItensNotaFiscal_KeyPress(object sender, KeyPressEventArgs e)
        {
            posSeparator = txtTotalFreteItensNotaFiscal.Text.IndexOf(".");
            if (e.KeyChar == ',') // Se for digitado "," ser  alterado para "."
                e.KeyChar = '.';
            //Se não for um numero e não  tecla backspace e nao for virgula, barra:
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (Char)Keys.Back && e.KeyChar != '.')
            {
                e.Handled = true;
                return;
            }
            if (e.KeyChar == '.' && posSeparator > -1)
            {
                e.Handled = true;
                return;
            }
        }

        private void txtOutrasDespesasAcessoriasItensNotaFiscal_KeyPress(object sender, KeyPressEventArgs e)
        {
            posSeparator = txtOutrasDespesasAcessoriasItensNotaFiscal.Text.IndexOf(".");
            if (e.KeyChar == ',') // Se for digitado "," ser  alterado para "."
                e.KeyChar = '.';
            //Se não for um numero e não  tecla backspace e nao for virgula, barra:
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (Char)Keys.Back && e.KeyChar != '.')
            {
                e.Handled = true;
                return;
            }
            if (e.KeyChar == '.' && posSeparator > -1)
            {
                e.Handled = true;
                return;
            }
        }

        private void txtBaseIcmsItensNotaFiscal_KeyPress(object sender, KeyPressEventArgs e)
        {
            posSeparator = txtBaseIcmsItensNotaFiscal.Text.IndexOf(".");
            if (e.KeyChar == ',') // Se for digitado "," ser  alterado para "."
                e.KeyChar = '.';
            //Se não for um numero e não  tecla backspace e nao for virgula, barra:
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (Char)Keys.Back && e.KeyChar != '.')
            {
                e.Handled = true;
                return;
            }
            if (e.KeyChar == '.' && posSeparator > -1)
            {
                e.Handled = true;
                return;
            }
        }

        private void txtValorIcmsItensNotaFiscal_KeyPress(object sender, KeyPressEventArgs e)
        {
            posSeparator = txtValorIcmsItensNotaFiscal.Text.IndexOf(".");
            if (e.KeyChar == ',') // Se for digitado "," ser  alterado para "."
                e.KeyChar = '.';
            //Se não for um numero e não  tecla backspace e nao for virgula, barra:
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (Char)Keys.Back && e.KeyChar != '.')
            {
                e.Handled = true;
                return;
            }
            if (e.KeyChar == '.' && posSeparator > -1)
            {
                e.Handled = true;
                return;
            }
        }

        private void txtBaseIcmsSTItensNotaFiscal_KeyPress(object sender, KeyPressEventArgs e)
        {
            posSeparator = txtBaseIcmsSTItensNotaFiscal.Text.IndexOf(".");
            if (e.KeyChar == ',') // Se for digitado "," ser  alterado para "."
                e.KeyChar = '.';
            //Se não for um numero e não  tecla backspace e nao for virgula, barra:
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (Char)Keys.Back && e.KeyChar != '.')
            {
                e.Handled = true;
                return;
            }
            if (e.KeyChar == '.' && posSeparator > -1)
            {
                e.Handled = true;
                return;
            }
        }

        private void txtValorIcmsSTItensNotaFiscal_KeyPress(object sender, KeyPressEventArgs e)
        {
            posSeparator = txtValorIcmsSTItensNotaFiscal.Text.IndexOf(".");
            if (e.KeyChar == ',') // Se for digitado "," ser  alterado para "."
                e.KeyChar = '.';
            //Se não for um numero e não  tecla backspace e nao for virgula, barra:
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (Char)Keys.Back && e.KeyChar != '.')
            {
                e.Handled = true;
                return;
            }
            if (e.KeyChar == '.' && posSeparator > -1)
            {
                e.Handled = true;
                return;
            }
        }

        private void tbcTributosItemNotaFiscal_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtValorBaseCalculoIPIItensNotaFiscal_KeyPress(object sender, KeyPressEventArgs e)
        {
            posSeparator = txtValorBaseCalculoIPIItensNotaFiscal.Text.IndexOf(".");
            if (e.KeyChar == ',') // Se for digitado "," ser  alterado para "."
                e.KeyChar = '.';
            //Se não for um numero e não  tecla backspace e nao for virgula, barra:
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (Char)Keys.Back && e.KeyChar != '.')
            {
                e.Handled = true;
                return;
            }
            if (e.KeyChar == '.' && posSeparator > -1)
            {
                e.Handled = true;
                return;
            }
        }

        private void txtAliquotaIPIItensNotaFiscal_KeyPress(object sender, KeyPressEventArgs e)
        {
            posSeparator = txtAliquotaIPIItensNotaFiscal.Text.IndexOf(".");
            if (e.KeyChar == ',') // Se for digitado "," ser  alterado para "."
                e.KeyChar = '.';
            //Se não for um numero e não  tecla backspace e nao for virgula, barra:
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (Char)Keys.Back && e.KeyChar != '.')
            {
                e.Handled = true;
                return;
            }
            if (e.KeyChar == '.' && posSeparator > -1)
            {
                e.Handled = true;
                return;
            }
        }

        private void txtQttyTotalUnDefaultIPIItensNotaFiscal_KeyPress(object sender, KeyPressEventArgs e)
        {
            posSeparator = txtQttyTotalUnDefaultIPIItensNotaFiscal.Text.IndexOf(".");
            if (e.KeyChar == ',') // Se for digitado "," ser  alterado para "."
                e.KeyChar = '.';
            //Se não for um numero e não  tecla backspace e nao for virgula, barra:
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (Char)Keys.Back && e.KeyChar != '.')
            {
                e.Handled = true;
                return;
            }
            if (e.KeyChar == '.' && posSeparator > -1)
            {
                e.Handled = true;
                return;
            }
        }

        private void txtValorUnIPIItensNotaFiscal_KeyPress(object sender, KeyPressEventArgs e)
        {
            posSeparator = txtValorUnIPIItensNotaFiscal.Text.IndexOf(".");
            if (e.KeyChar == ',') // Se for digitado "," ser  alterado para "."
                e.KeyChar = '.';
            //Se não for um numero e não  tecla backspace e nao for virgula, barra:
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (Char)Keys.Back && e.KeyChar != '.')
            {
                e.Handled = true;
                return;
            }
            if (e.KeyChar == '.' && posSeparator > -1)
            {
                e.Handled = true;
                return;
            }
        }

        private void txtValorIPIItensNotaFiscal_KeyPress(object sender, KeyPressEventArgs e)
        {
            posSeparator = txtValorIPIItensNotaFiscal.Text.IndexOf(".");
            if (e.KeyChar == ',') // Se for digitado "," ser  alterado para "."
                e.KeyChar = '.';
            //Se não for um numero e não  tecla backspace e nao for virgula, barra:
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (Char)Keys.Back && e.KeyChar != '.')
            {
                e.Handled = true;
                return;
            }
            if (e.KeyChar == '.' && posSeparator > -1)
            {
                e.Handled = true;
                return;
            }
        }

        private void btnCalculoSugestaoIPIItemNotaFiscal_Click(object sender, EventArgs e)
        {
            if (txtCodigoItensNotaFiscal.Text != "")
            {
                if (txtQttyComercialTributavelItensNotaFiscal.Text != "" && txtQttyComercialTributavelItensNotaFiscal.Text != "0.00" && Convert.ToDecimal(txtQttyComercialTributavelItensNotaFiscal.Text) > 0)
                {
                    cmbTipoCalculoIPIItensNotaFiscal.SelectedIndex = -1;
                    statusObjIPI(true);
                    cmbSituacaoTributariaIPIItensNotaFiscal.SelectedIndex = -1;
                    txtClasseEnquadramentoIPIItensNotaFiscal.Clear();
                    txtClasseEnquadramentoIPIItensNotaFiscal.Select();
                    txtCodigoEnquadramentoIPIItensNotaFiscal.Clear();
                    mskCNPJProdutorIPIItensNotaFiscal.Clear();
                    txtQttySeloControleIPIItensNotaFiscal.Clear();
                    txtSeloControleIPIItensNotaFiscal.Clear();
                    if (txtValorBrutoItensNotaFiscal.Text != "0.00")
                    {
                        txtValorBaseCalculoIPIItensNotaFiscal.Text = txtValorBrutoItensNotaFiscal.Text;
                        txtAliquotaIPIItensNotaFiscal.Text = optionProdutoipi;
                        txtQttyTotalUnDefaultIPIItensNotaFiscal.Text = txtQttyComercialTributavelItensNotaFiscal.Text;
                        txtValorIPIItensNotaFiscal.Text = optionProdutoipiValor;
                        txtValorUnIPIItensNotaFiscal.Text = String.Format("{0:F2}", calculoImpostos.calcValueIPI(Convert.ToDecimal(txtValorBaseCalculoIPIItensNotaFiscal.Text), Convert.ToDecimal(optionProdutoipi)));
                        txtValorIPIItensNotaFiscal.Text = String.Format("{0:F2}", calculoImpostos.calcValueIPI(Convert.ToDecimal(txtValorBaseCalculoIPIItensNotaFiscal.Text), Convert.ToDecimal(optionProdutoipi)));
                    }
                }
                else
                {
                    MessageBox.Show("Quantidade minima não devida, ou igual a zero! ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tbcNotaSaida.SelectedIndex = 1;
                    txtQttyComercialTributavelItensNotaFiscal.Select();
                }
            }
            else
            {
                MessageBox.Show("Produto Inválido! ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tbcNotaSaida.SelectedIndex = 1;
                txtCodigoItensNotaFiscal.Select();
            }
            txtValorIPIItensNotaFiscal.Select();
        }

        private void cmbTipoCalculoIPIItensNotaFiscal_SelectedIndexChanged(object sender, EventArgs e)
        {
            string test;
            if (cmbTipoCalculoIPIItensNotaFiscal.Text != "")
            {
                test = cmbTipoCalculoIPIItensNotaFiscal.Text.Substring(0, 2);
                if (test == "00")
                {
                    txtValorBaseCalculoIPIItensNotaFiscal.Text = "0.00";
                    txtAliquotaIPIItensNotaFiscal.Text = "0.00";
                    txtValorUnIPIItensNotaFiscal.Text = "0.00";
                    txtQttyTotalUnDefaultIPIItensNotaFiscal.Text = "0.00";
                    txtQttyTotalUnDefaultIPIItensNotaFiscal.Enabled = false;
                    txtValorUnIPIItensNotaFiscal.Enabled = false;
                    txtValorBaseCalculoIPIItensNotaFiscal.Select();
                    txtValorBaseCalculoIPIItensNotaFiscal.Enabled = true;
                    txtAliquotaIPIItensNotaFiscal.Enabled = true;
                }
                else
                {
                    txtValorBaseCalculoIPIItensNotaFiscal.Text = "0.00";
                    txtAliquotaIPIItensNotaFiscal.Text = "0.00";
                    txtValorUnIPIItensNotaFiscal.Text = "0.00";
                    txtQttyTotalUnDefaultIPIItensNotaFiscal.Text = "0.00";
                    txtAliquotaIPIItensNotaFiscal.Enabled = false;
                    txtValorBaseCalculoIPIItensNotaFiscal.Enabled = false;
                    txtValorUnIPIItensNotaFiscal.Enabled = true;
                    txtQttyTotalUnDefaultIPIItensNotaFiscal.Enabled = true;
                    txtValorUnIPIItensNotaFiscal.Select();

                }
                txtValorIPIItensNotaFiscal.Text = "0.00";
            }
        }

        private void btnVerificaExistenciaNfEntradaNotaSaida_Validated(object sender, EventArgs e)
        {

        }

        private void btnVerificaExistenciaNfEntradaNotaSaida_Click(object sender, EventArgs e)
        {
            if (flagSemaforo == 1)
            {
                if (lbDestinatarioRazaoNotaFiscal.Text != "")
                {
                    if (txtNumeroNfEntradaNotaSaida.Text != "")
                    {
                        openNotaEntrada = new consultaNota(txtNumeroNfEntradaNotaSaida.Text, txtSerieNfEntradaNotaSaida.Text, auxIdLoja, auxIdCliente);
                        if (openNotaEntrada.ShowDialog(this) == DialogResult.OK)
                        {
                            if (openNotaEntrada.GridChave != null)
                            {
                                lbmsgRetornoEntradaNota.Visible = true;
                                lbmsgRetornoEntradaNota.Text = openNotaEntrada.GridTotal;
                                txtSerieNfEntradaNotaSaida.Text = openNotaEntrada.GridSerie;
                                txtNumeroNfEntradaNotaSaida.Text = openNotaEntrada.GridNota;
                                auxIdEntrada = openNotaEntrada.GridChave;
                                if (flagSemaforo == 1)
                                {
                                    //conector_alt_nf();
                                    if (auxIdNf != "" && auxIdNf != null && Convert.ToDouble(auxIdNf) > 0)
                                    {
                                        auxConsistencia = 0;
                                        try
                                        {
                                            banco.abreConexao();
                                            banco.singleTransaction("update nf set nr_nota_entrada=?nf, serie_entrada=?serie where nf.nf=?id and Loja=?store and idCliente=?client");
                                            banco.addParametro("?nf", txtNumeroNfEntradaNotaSaida.Text);
                                            banco.addParametro("?serie", txtSerieNfEntradaNotaSaida.Text);
                                            banco.addParametro("?id", auxIdNf);
                                            banco.addParametro("?store", auxIdLoja);
                                            banco.addParametro("?client", auxIdCliente);
                                            banco.procedimentoRead();

                                        }
                                        catch (Exception erro)
                                        {
                                            MessageBox.Show(erro.Message, "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                                }
                                btnCarregaItemDevolucao.Visible = true;
                            }
                            else
                            {
                                MessageBox.Show("Não existe nota de entrada com o numero " + txtNumeroNfEntradaNotaSaida.Text + " para o fornecedor " + lbDestinatarioRazaoNotaFiscal.Text + ". Favor conferir! ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                lbmsgRetornoEntradaNota.Text = "TEXTO";
                                txtSerieNfEntradaNotaSaida.Clear();
                                txtNumeroNfEntradaNotaSaida.Clear();
                                tbcNotaSaida.SelectedIndex = 0;
                            }
                        }
                        else
                        {
                            if (lbmsgRetornoEntradaNota.Text == "TEXTO" || lbmsgRetornoEntradaNota.Text == "")
                            {
                                MessageBox.Show("Não existe nota de entrada com o numero " + txtNumeroNfEntradaNotaSaida.Text + " para o fornecedor " + lbDestinatarioRazaoNotaFiscal.Text + ". Favor conferir! ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                lbmsgRetornoEntradaNota.Text = "TEXTO";
                                txtSerieNfEntradaNotaSaida.Clear();
                                txtNumeroNfEntradaNotaSaida.Clear();
                                tbcNotaSaida.SelectedIndex = 0;
                            }
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Nenhuma pessoa [CLIENTE OU FORNECEDOR] foi carregado para a busca do documento a ser devolvido.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tbcNotaSaida.SelectedIndex = 0;
                }
            }
            else
            {
                MessageBox.Show("Validação da nota não concluida, pressione [ F10 ] para prosseguir.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tbcNotaSaida.SelectedIndex = 0;

            }
        }

        private void txtValorIPIItensNotaFiscal_Validated(object sender, EventArgs e)
        {
            if (txtValorIPIItensNotaFiscal.Text != "" && txtValorIPIItensNotaFiscal.Text != "0.00")
            {
                if (optionProdutoValorLiquido != "")
                {
                    flagNumeric = calculoImpostos.calcSumProdutoNota(Convert.ToDecimal(txtValorUnComercialTributarioItensNotaFiscal.Text), Convert.ToDecimal(txtQttyComercialTributavelItensNotaFiscal.Text), despesas, discount, 0, Convert.ToDecimal(txtAliquotaIPIItensNotaFiscal.Text), 0).ToString(); //revisar
                    optionProdutovalorTotalNota = flagNumeric.Replace(",", ".").Trim();
                    conector_obj_salve_item();
                }
            }
        }

        private void btnCarregaItemDevolucao_Click(object sender, EventArgs e)
        {
            int test = 0;
            devItem = new itemDevolucao(auxIdLoja, auxIdEntrada);
            if (devItem.ShowDialog(this) == DialogResult.Retry)
            {
                countRows = devItem.countLinhas;
                if (countRows > 0)
                {
                    test = countRows;
                    returnSet = devItem.setDados;
                    for (int x = 0; x < test; x++)
                    {
                        int count = conector_verifica(returnSet.Tables[0].Rows[x][0].ToString() == "" ? "0" : returnSet.Tables[0].Rows[x][0].ToString());

                        if (count >= 1)
                        {
                            MessageBox.Show("Produto existe nesta nota fiscal!", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtCodigoItensNotaFiscal.Clear();
                            txtCodigoItensNotaFiscal.Select();
                            zeraObjItem();
                            zeraVariavelRecalculo();
                        }
                        else
                        {
                            conector_find_produto(returnSet.Tables[0].Rows[x][0].ToString());

                            try
                            {
                                banco.abreConexao();
                                banco.startTransaction("conector_inc_nfItem");
                                banco.addParametro("inc_idnf", auxIdNf);
                                banco.addParametro("inc_idProduto", returnSet.Tables[0].Rows[x][0].ToString());
                                banco.addParametro("inc_valorLiquido", optionProdutoValorLiquido);
                                banco.addParametro("inc_priceOriginal", returnSet.Tables[0].Rows[x][32].ToString());
                                banco.addParametro("inc_priceVenda", returnSet.Tables[0].Rows[x][7].ToString());
                                banco.addParametro("inc_priceCusto", optionProdutopriceCusto);
                                banco.addParametro("inc_estoque", returnSet.Tables[0].Rows[x][71].ToString());
                                banco.addParametro("inc_data", String.Format("{0:yyyyMMdd}", DateTime.Now));
                                banco.addParametro("inc_peso", optionProdutopeso);
                                banco.addParametro("inc_aliquota", returnSet.Tables[0].Rows[x][26].ToString());
                                banco.addParametro("inc_icms", returnSet.Tables[0].Rows[x][42].ToString());
                                banco.addParametro("inc_baseCalculo", returnSet.Tables[0].Rows[x][73].ToString());
                                banco.addParametro("inc_reducao", returnSet.Tables[0].Rows[x][27].ToString());
                                banco.addParametro("inc_quantidade", returnSet.Tables[0].Rows[x][33].ToString());
                                banco.addParametro("inc_idunidadeMedida", returnSet.Tables[0].Rows[x][22].ToString());
                                banco.addParametro("inc_cfop", returnSet.Tables[0].Rows[x][62].ToString());
                                banco.addParametro("inc_cstIcms", returnSet.Tables[0].Rows[x][59].ToString());
                                banco.addParametro("inc_cstPis", returnSet.Tables[0].Rows[x][57].ToString());
                                banco.addParametro("inc_valorPis", returnSet.Tables[0].Rows[x][44].ToString());
                                banco.addParametro("inc_basePis", returnSet.Tables[0].Rows[x][67].ToString());
                                banco.addParametro("inc_cstCofins", returnSet.Tables[0].Rows[x][58].ToString());
                                banco.addParametro("inc_valorCofins", returnSet.Tables[0].Rows[x][44].ToString());
                                banco.addParametro("inc_baseCofins", returnSet.Tables[0].Rows[x][68].ToString());
                                banco.addParametro("inc_cstIpi", returnSet.Tables[0].Rows[x][57].ToString());
                                banco.addParametro("inc_ipi", returnSet.Tables[0].Rows[x][31].ToString());
                                banco.addParametro("inc_ipiValor", txtValorUnIPIItensNotaFiscal.Text);
                                banco.addParametro("inc_valorIpi", returnSet.Tables[0].Rows[x][35].ToString());
                                banco.addParametro("inc_baseIpi", txtValorBaseCalculoIPIItensNotaFiscal.Text);
                                banco.addParametro("inc_desconto", returnSet.Tables[0].Rows[x][12].ToString());
                                banco.addParametro("inc_descontoValor", returnSet.Tables[0].Rows[x][15].ToString());
                                banco.addParametro("inc_acrescimo", returnSet.Tables[0].Rows[x][19].ToString());
                                banco.addParametro("inc_acrescimoValor", optionProdutoacrescimoValor);
                                banco.addParametro("inc_aliquotaIcmsSt", txtAliquotaIcmsSTItensNotaFiscal.Text);//Conferir questao aliquota do ST como vai no XML nfe
                                banco.addParametro("inc_baseCalculoIcmsSubstituicao", returnSet.Tables[0].Rows[x][66].ToString());
                                banco.addParametro("inc_valorIcmsSubstituicao", returnSet.Tables[0].Rows[x][65].ToString());
                                banco.addParametro("inc_reducaoIcmsSt", txtReducaoBaseCalculoSTItensNotaFiscal.Text);
                                banco.addParametro("inc_margem", "0");//Conferir
                                banco.addParametro("inc_valorTotalProduto", (Convert.ToDecimal(returnSet.Tables[0].Rows[x][7].ToString()) * Convert.ToDecimal(returnSet.Tables[0].Rows[x][33].ToString())).ToString());
                                banco.addParametro("inc_valorTotalNota", returnSet.Tables[0].Rows[x][9].ToString());
                                banco.addParametro("inc_valorTotalLiquido", returnSet.Tables[0].Rows[x][6].ToString());
                                banco.addParametro("inc_fornecedor", optionProdutoFornecedor);
                                banco.addParametro("inc_idsetor", optionProdutoIdSetor);
                                banco.addParametro("inc_tributacao", optionProdutoCodigoAliquota);
                                banco.addParametro("inc_typeAliquota", returnSet.Tables[0].Rows[x][80].ToString());
                                banco.addParametro("inc_chaveEntrada", returnSet.Tables[0].Rows[x][4].ToString());
                                banco.procedimentoRead();
                                if (banco.retornaRead().Read() == true)
                                {
                                    auxIdNfItem = banco.retornaRead().GetString(0);
                                }
                            }
                            catch (Exception erro)
                            {
                                MessageBox.Show(erro.Message, "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            finally
                            {
                                banco.fechaConexao();
                                if (auxConsistencia == 0)
                                {
                                    flagSemaforoItem = 1;
                                    conector_find_itemNfSaida("1");
                                    dgvItensNotaFiscal.Select();
                                    txtCodigoItensNotaFiscal.ReadOnly = false;
                                    zeraObjItem();
                                    conector_find_itemNfSaidaTotais();
                                    tbcNotaSaida.SelectedIndex = 1;
                                    tbcTributosItemNota.SelectedIndex = 0;
                                    txtCodigoItensNotaFiscal.Select();
                                }
                            }
                        }
                    }
                }
            }
            
        }

        private void btnTrashItensNotaFiscal_Click(object sender, EventArgs e)
        {
            flagRetornoTotalItens = conector_full_verificaItem();
            if ((flagRetornoTotalItens > 0))
            {
                ataque = 0;

                conector_obj_removeItensNota();

                ataque = 1;


            }
        }

        private void linkExcluirItemNotaFiscal_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            flagRetornoTotalItens = conector_full_verificaItem();
            if ((flagRetornoTotalItens > 0))
            {
                ataque = 0;

                conector_obj_removeItensNota();

                ataque = 1;


            }
        }

        private void btnGeraNotaFiscal_Click(object sender, EventArgs e)
        {
            conector_update_nf();
        }

        private void btnImprimiNotaFiscal_Click(object sender, EventArgs e)
        {

        }

        private void btnFecharDanfe_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }

        private void btnCarregaPedidoNotaFiscal_Click(object sender, EventArgs e)
        {
                conector_carrega_pedido();
                sinal = 0;
            
        }

        private void btnPesquisaFormaPgtoNotaFiscal_Click(object sender, EventArgs e)
        {
            pesquisaFinalizadora codigoFinalizadora = new pesquisaFinalizadora();
            if (codigoFinalizadora.ShowDialog(this) == DialogResult.OK)
            {
                auxCondPgto = codigoFinalizadora.GridCodigo;
                conector_find_finalizadora(auxCondPgto);
            }
        }

        private void btnValidaNfeNotaFiscal_Click(object sender, EventArgs e)
        {
            conector_find_itemNfSaida("5");    
            //conector_carrega_pedido();   
            //transmiteNfe.geraNotaFiscalEletronica(
        }

        private void btnCabecalhoNotaFiscal_Click(object sender, EventArgs e)
        {
            conector_obj_salve_nf();
        }

        private void btnPesquisaCFOPNotaFiscal_Enter(object sender, EventArgs e)
        {
            findCFOP = new pesquisaCFOP();
            if (findCFOP.ShowDialog(this) == DialogResult.OK)
            {
                if (txtTypeNotaFiscal.Text == "1" || txtTypeNotaFiscal.Text == "2")
                {
                    auxIdCFOP = findCFOP.GridCodigo;
                    auxTypeCFOP = findCFOP.GridInput;
                    if (auxTypeCFOP == "saida")
                    {
                        if (txtTypeNotaFiscal.Text == "1")
                        {
                            txtCFOPNotaFiscal.Text = auxIdCFOP;
                        }
                        else
                        {
                            MessageBox.Show("CFOP inválido para saída de mercadoria. ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else if (auxTypeCFOP == "entrada")
                    {
                        if (txtTypeNotaFiscal.Text == "2")
                        {
                            txtCFOPNotaFiscal.Text = auxIdCFOP;
                        }
                        else
                        {
                            MessageBox.Show("CFOP inválido para entrada de mercadoria. ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Tipo de nota invalido, deve ser '1 - Saída' ou '2 - Entrada'. ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTypeNotaFiscal.Clear();
                    txtTypeNotaFiscal.Select();
                }
            }
        }

        private void tbpDadosCabecalhoNotaFiscal_Click(object sender, EventArgs e)
        {

        }

        private void cmbTributoOrigemItensNotaFiscal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTributoOrigemItensNotaFiscal.Text != "")
            {
                auxOrigemItem = cmbTributoOrigemItensNotaFiscal.Text.Substring(0,2);
            }
        }
    }
}
