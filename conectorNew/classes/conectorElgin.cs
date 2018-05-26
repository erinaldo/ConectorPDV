using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.InteropServices;
namespace conectorElgin
{
    // ERROR: Not supported in C#: OptionDeclaration
    static class elginECF
    {
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        //===============================================================================
        //********************************************************************************
        //
        //                      DECLARAÇÃO DAS FUNÇÕES DA Elgin.DLL
        //                      Ultima atualização: 30/10/2009
        //                      Elgin.dll v.1.0.0.13
        //
        //********************************************************************************
        //===============================================================================}
        public static extern short Elgin_AberturaDoDia(string ValorCompra, string FormaPagamento);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_AbreComprovanteNaoFiscalVinculado(string FormaPagamento, string Valor, string NumeroCupom);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_AbreComprovanteNaoFiscalVinculadoMFD(string FormaPagamento, string Valor, string NumeroCupom, string CGC, string Nome, string Endereco);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_AbreCupom(string CGC_CPF);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_AbreCupomMFD(string CGC, string Nome, string Endereco);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_AbrePortaSerial();
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_AbreRecebimentoNaoFiscalMFD(string CGC, string Nome, string Endereco);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_AbreRelatorioGerencial();
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_AbreRelatorioGerencialMFD(string Indice);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_AcionaGaveta();
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_AcionaGuilhotinaMFD(short TipoCorte);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_AcrescimoDescontoItemMFD(string Item, string AcrescimoDesconto, string TipoAcrescimoDesconto, string ValorAcrescimoDesconto);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_AcrescimoDescontoSubtotalMFD(string cFlag, string cTipo, string cValor);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_AcrescimoDescontoSubtotalRecebimentoMFD(string cFlag, string cTipo, string cValor);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_AcrescimoItemNaoFiscalMFD(string strNroItem, string strAcrescDesc, string strTipoAcrescDesc, string strValor);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_Acrescimos(string ValorAcrescimos);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_AlteraSimboloMoeda(string SimboloMoeda);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_AtivaDesativaVendaUmaLinhaMFD(short iFlag);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_AumentaDescricaoItem(string Descricao);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_Autenticacao();
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_AutenticacaoMFD(string Linhas, string Texto);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_CancelaAcrescimoDescontoItemMFD(string cFlag, string cItem);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_CancelaAcrescimoDescontoSubtotalMFD(string cFlag);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_CancelaAcrescimoDescontoSubtotalRecebimentoMFD(string cFlag);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_CancelaAcrescimoNaoFiscalMFD(string strNumeroItem, string strAcrecDesc);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_CancelaCupom();
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_CancelaCupomMFD(string CGC, string Nome, string Endereco);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_CancelaImpressaoCheque();
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_CancelaItemAnterior();
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_CancelaItemGenerico(string NumeroItem);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_CancelaItemNaoFiscalMFD(string strNroItem);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_Cancelamentos(string ValorCancelamentos);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_CancelaRecebimentoNaoFiscalMFD(string CGC, string Nome, string Endereco);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_CGC_IE(string CGC, string IE);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_ClicheProprietario(string Cliche);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_CNPJ_IE(string CNPJ, string IE);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_CNPJMFD(string CNPJ);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_CodigoBarrasCODABARMFD(string Codigo);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_CodigoBarrasCODE128MFD(string Codigo);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_CodigoBarrasCODE39MFD(string Codigo);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_CodigoBarrasCODE93MFD(string Codigo);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_CodigoBarrasEAN13MFD(string Codigo);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_CodigoBarrasEAN8MFD(string Codigo);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_CodigoBarrasISBNMFD(string Codigo);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_CodigoBarrasITFMFD(string Codigo);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_CodigoBarrasMSIMFD(string Codigo);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_CodigoBarrasPLESSEYMFD(string Codigo);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_CodigoBarrasUPCAMFD(string Codigo);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_CodigoBarrasUPCEMFD(string Codigo);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_ComprovantesNaoFiscaisNaoEmitidosMFD(string Comprovantes);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_ConfiguraCodigoBarrasMFD(short Altura, short Largura, short pos, short Fonte, short Margem);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_ContadorComprovantesCreditoMFD(string Comprovantes);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_ContadorCupomFiscalMFD(string CuponsEmitidos);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_ContadoresTotalizadoresNaoFiscais(string Contadores);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_ContadoresTotalizadoresNaoFiscaisMFD(string Contadores);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_ContadorFitaDetalheMFD(string ContadorFita);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_ContadorOperacoesNaoFiscaisCanceladasMFD(string OperacoesCanceladas);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_ContadorRelatoriosGerenciaisMFD(string Relatorios);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_CupomAdicionalMFD();
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_DadosSintegra(string DataInicial, string DataFinal);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_DadosUltimaReducao(string DadosReducao);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_DadosUltimaReducaoMFD(string DadosReducao);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_DataHoraImpressora(string Data, string Hora);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_DataHoraReducao(string Data, string Hora);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_DataHoraSoftwareBasico(string DataSW, string HoraSW);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_DataHoraUltimoDocumentoMFD(string cDataHora);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_DataMovimento(string Data);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_DataMovimentoUltimaReducaoMFD(string cDataMovimento);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_Descontos(string ValorDescontos);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_DownloadMF(string Arquivo);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_DownloadMFD(string Arquivo, string TipoDownload, string ParametroInicial, string ParametroFinal, string UsuarioECF);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_EfetuaFormaPagamento(string FormaPagamento, string ValorFormaPagamento);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_EfetuaFormaPagamentoDescricaoForma(string FormaPagamento, string ValorFormaPagamento, string DescricaoFormaPagto);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_EfetuaFormaPagamentoMFD(string FormaPagamento, string ValorFormaPagamento, string Parcelas, string DescricaoFormaPagto);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_EfetuaRecebimentoNaoFiscalMFD(string IndiceTotalizador, string ValorRecebimento);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_EspacoEntreLinhas(short Dots);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_EstornoFormasPagamento(string FormaOrigem, string FormaDestino, string Valor);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_EstornoNaoFiscalVinculadoMFD(string CGC, string Nome, string Endereco);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_ExecutaComando(string Comando, string parametros);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_ExecutaLeitura(string Comando, string parametros, ref string retorno);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_FechaComprovanteNaoFiscalVinculado();
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_FechaCupom(string FormaPagamento, string AcrescimoDesconto, string TipoAcrescimoDesconto, string ValorAcrescimoDesconto, string ValorPago, string Mensagem);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_FechaCupomResumido(string FormaPagamento, string Mensagem);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_FechamentoDoDia();
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_FechaPortaSerial();
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_FechaRecebimentoNaoFiscalMFD(string Mensagem);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_FechaRelatorioGerencial();
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_FlagsFiscais(ref short Flag);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_FlagsFiscaisStr(string FlagFiscal);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_FormatoDadosMFD(string ArquivoOrigem, string ArquivoDestino, string TipoFormato, string TipoDownload, string ParametroInicial, string ParametroFinal, string UsuarioECF);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_GrandeTotal(string GrandeTotal);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_GrandeTotalUltimaReducaoMFD(string cGT);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_HabilitaDesabilitaRetornoEstendidoMFD(string FlagRetorno);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_IdentificaConsumidor(string CNPJ_CPF, string Nome, string Endereco);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_ImprimeCheque(string Banco, string Valor, string Favorecido, string Cidade, string Data, string Mensagem);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_ImprimeChequeMFD(string NumeroBanco, string Valor, string Favorecido, string Cidade, string Data, string Mensagem, string ImpressaoVerso, string Linhas);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_ImprimeConfiguracoesImpressora();
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_ImprimeCopiaCheque();
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_ImprimeDepartamentos();
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_IncluiCidadeFavorecido(string Cidade, string Favorecido);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_IniciaFechamentoCupom(string AcrescimoDesconto, string TipoAcrescimoDesconto, string ValorAcrescimoDesconto);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_IniciaFechamentoCupomMFD(string AcrescimoDesconto, string TipoAcrescimoDesconto, string ValorAcrescimo, string ValorDesconto);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_IniciaFechamentoRecebimentoNaoFiscalMFD(string AcrescimoDesconto, string TipoAcrescimoDesconto, string ValorAcrescimo, string ValorDesconto);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_InicioFimCOOsMFD(string cCOOIni, string cCOOFim);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_InicioFimGTsMFD(object cGTIni, string cGTFim);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_InscricaoEstadualMFD(string InscricaoEstadual);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_InscricaoMunicipalMFD(string InscricaoMunicipal);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_LeArquivoRetorno(string sCupom);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_LeIndicadores(ref short indicador);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_LeituraCheque(string CodigoCMC7);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_LeituraMemoriaFiscalData(string DataInicial, string DataFinal, string FlagLeitura);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_LeituraMemoriaFiscalReducao(string ReducaoInicial, string ReducaoFinal, string FlagLeitura);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_LeituraMemoriaFiscalSerialData(string DataInicial, string DataFinal, string FlagLeitura);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_LeituraMemoriaFiscalSerialReducao(string ReducaoInicial, string ReducaoFinal, string FlagLeitura);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_LeituraX();
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_LeituraXSerial();
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_LeNomeRelatorioGerencial(string Codigo, string NomeRelatorio);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_LinhasEntreCupons(short Linhas);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_MapaResumo();
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_MapaResumoMFD();
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_MarcaModeloTipoImpressoraMFD(ref string Marca, ref string Modelo, ref string Tipo);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_MinutosEmitindoDocumentosFiscaisMFD(string Minutos);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_MinutosImprimindo(string Minutos);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_MinutosLigada(string Minutos);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_ModeloImpressora(string ModeloImpressora);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_NomeiaDepartamento(short Indice, string Departamento);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_NomeiaRelatorioGerencialMFD(string Indice, string Descricao);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_NomeiaTotalizadorNaoSujeitoIcms(short Indice, string Totalizador);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_NumeroCaixa(string NumeroCaixa);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_NumeroCupom(string NumeroCupom);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_NumeroCuponsCancelados(string NumeroCancelamentos);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_NumeroIntervencoes(string NumeroIntervencoes);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_NumeroLoja(string NumeroLoja);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_NumeroOperacoesNaoFiscais(string NumeroOperacoes);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_NumeroReducoes(string NumeroReducoes);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_NumeroSerie(string NumeroSerie);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_NumeroSerieCriptografado(string NumeroSerie);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_NumeroSerieDescriptografado(string NumeroSerieCriptografado, string NumeroSerieDesCriptografado);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_NumeroSerieMemoriaMFD(string NumeroSerieMFD);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_NumeroSubstituicoesProprietario(string NumeroSubstituicoes);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_PercentualLivreMFD(string cMemoriaLivre);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_ProgramaAliquota(string Aliquota, short ICMS_ISS);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_ProgramaArredondamento();
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_ProgramaBaudRate(string BaudRate);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_ProgramaCaracterAutenticacao(string parametros);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_ProgramaFormaPagamentoMFD(string FormaPagto, string OperacaoTef);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_ProgramaHorarioVerao();
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_ProgramaIdAplicativoMFD(string NomeAplicativo);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_ProgramaMoedaPlural(string MoedaPlural);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_ProgramaMoedaSingular(string MoedaSingular);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_ProgramaOperador(string NomeOperador);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_ProgramaTruncamento();
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_RecebimentoNaoFiscal(string IndiceTotalizador, string Valor, string FormaPagamento);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_ReducaoZ(string Data, string Hora);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_ReducoesRestantesMFD(string Reducoes);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_RegistrosTipo60();
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_ReimpressaoNaoFiscalVinculadoMFD();
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_RelatorioGerencial(string Texto);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_RelatorioSintegraMFD(short iRelatorios, string cArquivo, string cMes, string cAno, string cRazaoSocial, string cEndereco, string cNumero, string cComplemento, string cBairro, string cCidade,
        string cCEP, string cTelefone, string cFax, string cContato);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_RelatorioTipo60Analitico();
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_RelatorioTipo60AnaliticoMFD();
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_RelatorioTipo60Mestre();
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_ResetaImpressora();
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_RetornoAliquotas(string Aliquotas);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_RetornoImpressora(ref short i, string ErrorMsg);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_Sangria(string Valor);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_SegundaViaNaoFiscalVinculadoMFD();
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_SimboloMoeda(string SimboloMoeda);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_StatusEstendidoMFD(ref short iStatus);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_SubTotal(string SubTotal);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_SubTotalComprovanteNaoFiscalMFD(string cSubTotal);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_Suprimento(string Valor, string FormaPagamento);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_TamanhoTotalMFD(string cTamanhoMFD);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_TempoOperacionalMFD(string TempoOperacional);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_TerminaFechamentoCupom(string Mensagem);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_TerminaFechamentoCupomCodigoBarrasMFD(string cMensagem, string cTipoCodigo, string cCodigo, short iAltura, short iLargura, short iPosicaoCaracteres, short iFonte, short iMargem, short iCorrecaoErros, short iColunas);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_TotalDiaTroco(string TotalDiaTroco);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_TotalDocTroco(string TotalDocTroco);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_TotalLivreMFD(string cMemoriaLivre);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_UltimoItemVendido(string NumeroItem);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_UsaComprovanteNaoFiscalVinculado(string Texto);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_UsaRelatorioGerencialMFD(string Texto);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_ValorFormaPagamento(string FormaPagamento, string Valor);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_ValorFormaPagamentoMFD(string FormaPagamento, string Valor);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_ValorPagoUltimoCupom(string ValorCupom);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_ValorTotalizadorNaoFiscal(string Totalizador, string Valor);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_ValorTotalizadorNaoFiscalMFD(string Totalizador, string Valor);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_VendaBruta(string VendaBruta);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_VendaLiquida(string VendaLiquida);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_VendeItem(string Codigo, string Descricao, string Aliquota, string TipoQuantidade, string Quantidade, short CasasDecimais, string ValorUnitario, string TipoDesconto, string Desconto);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_VendeItemDepartamento(string Codigo, string Descricao, string Aliquota, string ValorUnitario, string Quantidade, string Acrescimo, string Desconto, string IndiceDepartamento, string UnidadeMedida);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_VerificaAliquotasICMS(string Flag);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_VerificaAliquotasIss(string Flag);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_VerificaDepartamentos(string Departamentos);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_VerificaEstadoGaveta(ref short EstadoGaveta);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_VerificaEstadoGavetaStr(string EstadoGaveta);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_VerificaEstadoImpressora(ref short ACK, ref short ST1, ref short ST2);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_VerificaEstadoImpressoraMFD(ref short ACK, ref short ST1, ref short ST2, ref short ST3);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_VerificaEstadoImpressoraStr(string ACK, string ST1, string ST2);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_VerificaFormasPagamento(string Formas);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_VerificaFormasPagamentoMFD(string FormasPagamento);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_VerificaImpressoraLigada();
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_VerificaIndiceAliquotasICMS(string Flag);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_VerificaIndiceAliquotasIss(string Flag);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_VerificaModoOperacao(string Modo);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_VerificaRecebimentoNaoFiscal(string Recebimentos);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_VerificaRecebimentoNaoFiscalMFD(string Recebimentos);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_VerificaReducaoZAutomatica(ref short Flag);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_VerificaRelatorioGerencialMFD(string Relatorios);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_VerificaSensorPoucoPapelMFD(string Flag);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_VerificaStatusCheque(ref short StatusCheque);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_VerificaTipoImpressora(ref short TipoImpressora);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_VerificaTipoImpressoraStr(string TipoImpressora);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_VerificaTotalizadoresNaoFiscais(string Totalizadores);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_VerificaTotalizadoresNaoFiscaisMFD(string Totalizadores);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_VerificaTotalizadoresParciais(string Totalizadores);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_VerificaTotalizadoresParciaisMFD(string Totalizadores);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_VerificaTruncamento(string Flag);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_VerificaZPendente(ref short Flag);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Elgin_VersaoFirmware(string VersaoFirmware);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short RFD_ConvertedaMFD(string CRZ);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short RFD_ConvertedaMFDData(string DataInicial, string DataFinal);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Wind_AcionaGaveta();
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Wind_AcionaGuilhotina(short Modo);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Wind_AcionaGuilhotinaParcial(short Modo);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Wind_AjustaLarguraPapel(short LarguraPapel);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Wind_ConfiguraCodigoBarras(short Altura, short Largura, short PosicaoCaracteres, short Fonte, short Margem);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Wind_EnviaBuffer(string Buffer);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Wind_EnviaBufferFormatado(string Buffer, short TipoLetra, short Italico, short Sublinhado, short Expandido, short Enfatizado);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Wind_EnviaComando(string Buffer, short TamanhoBuffer);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Wind_ImprimeBitmap(string NomeArquivo, short Modo);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Wind_ImprimeBmpEspecial(string NomeArquivo, short EscalaX, short EscalaY, short Angulo);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Wind_ImprimeCodigoBarrasCODABAR(string Codigo);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Wind_ImprimeCodigoBarrasCODE128(string Codigo);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Wind_ImprimeCodigoBarrasCODE39(string Codigo);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Wind_ImprimeCodigoBarrasCODE93(string Codigo);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Wind_ImprimeCodigoBarrasEAN13(string Codigo);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Wind_ImprimeCodigoBarrasEAN8(string Codigo);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Wind_ImprimeCodigoBarrasISBN(string Codigo);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Wind_ImprimeCodigoBarrasITF(string Codigo);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Wind_ImprimeCodigoBarrasMSI(string Codigo);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Wind_ImprimeCodigoBarrasPDF417(short NivelCorrecaoErros, short Altura, short Largura, short Colunas, string Codigo);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Wind_ImprimeCodigoBarrasPLESSEY(string Codigo);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Wind_ImprimeCodigoBarrasUPCA(string Codigo);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Wind_ImprimeCodigoBarrasUPCE(string Codigo);
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Wind_VerificaEstadoGaveta();
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Wind_VerificaFimPapel();
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short Wind_VerificaPoucoPapel();
        [DllImport("Elgin.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]

        //SPED Fiscal
        public static extern short Elgin_GeraRegistrosSpedMFD(string sArquivoMFD, string sArquivoTXT, string sDataInicial, string sDataFinal, string sPerfil, string sCFOP, string sCODOBSFiscal, string AliqPIS, string sAliqCOFINS);

        //===============================================================================
        //********************************************************************************
        //
        //                   DECLARAÇÃO DAS FUNÇÕES GLOBAIS DO DemoFit
        //
        //********************************************************************************
        //===============================================================================}
        public static bool TrataRetorno(short iRetorno, bool bMostraMsg = true)
        {
            string strMsgErro = null;
            bool bRetorno = false;

            bRetorno = false;
            if ((iRetorno != 1))
            {
                switch (iRetorno)
                {
                    case 0:
                        if ((ObtemRetornoECF(ref strMsgErro) & bMostraMsg))
                        {
                            Interaction.MsgBox(strMsgErro, MsgBoxStyle.Critical, "DemoFit32 - Erro na comunicação.");
                        }
                        else if ((bMostraMsg))
                        {
                            Interaction.MsgBox("Erro na comunicação.", MsgBoxStyle.Critical, "DemoFit32");
                        }
                        break;
                    case -2:
                        Interaction.MsgBox("Parâmetro inválido na função.", MsgBoxStyle.Critical, "DemoFit32");
                        break;
                    case -4:
                        Interaction.MsgBox("O arquivo de inicialização Elgin.ini não foi encontrado no diretório de sistema do Windows.", MsgBoxStyle.Critical, "DemoFit32");
                        break;
                    case -5:
                        Interaction.MsgBox("Erro ao abrir a porta de comunicação.", MsgBoxStyle.Critical, "DemoFit32");
                        break;
                    case -27:
                        Interaction.MsgBox("Status da impressora diferente de 6,0,0 (ACK, ByVal ST1 e ST2).", MsgBoxStyle.Critical, "DemoFit32");
                        break;
                    default:
                        Interaction.MsgBox("Ocorreu um erro desconhecido. Erro nº " + Convert.ToString(iRetorno), MsgBoxStyle.Critical, "DemoFit32");
                        break;
                }
            }
            else if ((bMostraMsg))
            {
                Interaction.MsgBox("Operação realizada com sucesso", MsgBoxStyle.OkOnly, "DemoFit");
                bRetorno = true;
            }

            AtualizaStatus();
            return bRetorno;
        }

        public static bool ObtemRetornoECF(ref string strMensagemErro)
        {
            short iRetorno = 0;
            short iCodErro = 0;
            string strErroMsg = null;
            bool bSucesso = false;

            strErroMsg = Strings.Space(100);

            iRetorno = Elgin_RetornoImpressora(ref iCodErro, strErroMsg);

            strMensagemErro = "Erro nº: " + Convert.ToString(iCodErro) + " - " + strErroMsg;

            if ((iRetorno == 1))
            {
                bSucesso = true;
            }
            else
            {
                bSucesso = false;
            }

            return bSucesso;

        }

        public static void AtualizaStatus()
        {
            short iRetorno = 0;
            short iST1 = 0;
            short iST2 = 0;
            short iACK = 0;

            //iRetorno = Elgin.VerificaStatusImpressora(iACK, iST1, iST2);

            //UPGRADE_WARNING: Lower bound of collection frmDemoFit.stsBarraStatus.Panels has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"
            //frmDemoFit.stsBarraStatus.Items.Item(2).Text = "ACK = " + Conversion.Str(iACK) + ", ST1 = " + Conversion.Str(iST1) + ", ST2 = " + Conversion.Str(iST2);
        }
    }
}