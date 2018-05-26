using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Net;
using System.Web;
using System.Web.Services.Description;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.CodeDom;
using System.CodeDom.Compiler;
using MessagingToolkit;
using MessagingToolkit.QRCode.Codec;

namespace conectorPDV001
{
    public class geraNFe
    {
        string chaveAcessoGeradaNfe;
        int qtdNFe;
        Int16 tipoRequisicao; // 1 - Recepção | 2 - Emissão
        int contador;
        int contadorGlobal;

        #region Envia Cartão Correção
        public void enviaCartaCorrecao(dadosCartaCorrecao cartaCorrecao)
        {
            XmlDocument xmlNfe = new XmlDocument();
            XmlElement xmlNfeElement;
            regrasNegocioNfe regrasNfe = new regrasNegocioNfe();
            XmlNode xmlNfeNode = xmlNfe.CreateXmlDeclaration("1.0", "UTF-8", null);
            xmlNfe.AppendChild(xmlNfeNode);

            // ----------------------------------------------------------------               
            xmlNfeElement = xmlNfe.CreateElement("envEvento");
            xmlNfeElement.SetAttribute("versao", "3.10");
            xmlNfeNode = xmlNfeElement;
            xmlNfe.AppendChild(xmlNfeNode);
            // ---------------------------------------------------------------- fim

            // ----------------------------------------------------------------               
            xmlNfeElement = xmlNfe.CreateElement("idLote");
            xmlNfeElement.InnerText = cartaCorrecao.idLote;
            XmlNode xmlNfeNodeEnvEventoIdLote = xmlNfeElement;
            xmlNfeNode.AppendChild(xmlNfeNodeEnvEventoIdLote);
            xmlNfe.AppendChild(xmlNfeNode);
            // ---------------------------------------------------------------- fim

            // ----------------------------------------------------------------               
            xmlNfeElement = xmlNfe.CreateElement("evento");
            xmlNfeElement.SetAttribute("versao", "3.10");
            XmlNode xmlNfeNodeEnvEventoIdLoteEvento = xmlNfeElement;
            xmlNfeNode.AppendChild(xmlNfeNodeEnvEventoIdLoteEvento);
            xmlNfe.AppendChild(xmlNfeNode);
            // ---------------------------------------------------------------- fim

            // ----------------------------------------------------------------               
            xmlNfeElement = xmlNfe.CreateElement("infEvent");
            XmlNode xmlNfeNodeEnvEventoIdLoteEventoInfEvent = xmlNfeElement;
            xmlNfeNodeEnvEventoIdLoteEvento.AppendChild(xmlNfeNodeEnvEventoIdLoteEventoInfEvent);
            xmlNfe.AppendChild(xmlNfeNode);
            // ---------------------------------------------------------------- fim

            // ----------------------------------------------------------------               
            xmlNfeElement = xmlNfe.CreateElement("Id");
            xmlNfeElement.InnerText = cartaCorrecao.Id;
            XmlNode xmlNfeNodeEnvEventoIdLoteEventoInfEventId = xmlNfeElement;
            xmlNfeNodeEnvEventoIdLoteEventoInfEvent.AppendChild(xmlNfeNodeEnvEventoIdLoteEventoInfEventId);
            xmlNfe.AppendChild(xmlNfeNode);
            // ---------------------------------------------------------------- fim

            // ----------------------------------------------------------------               
            xmlNfeElement = xmlNfe.CreateElement("cOrgao");
            xmlNfeElement.InnerText = cartaCorrecao.cOrgao;
            XmlNode xmlNfeNodeEnvEventoIdLoteEventoInfEventCOrgao = xmlNfeElement;
            xmlNfeNodeEnvEventoIdLoteEventoInfEvent.AppendChild(xmlNfeNodeEnvEventoIdLoteEventoInfEventCOrgao);
            xmlNfe.AppendChild(xmlNfeNode);
            // ---------------------------------------------------------------- 

            // ----------------------------------------------------------------               
            xmlNfeElement = xmlNfe.CreateElement("tpAmb");
            xmlNfeElement.InnerText = cartaCorrecao.cOrgao;
            XmlNode xmlNfeNodeEnvEventoIdLoteEventoInfEventTpAmb = xmlNfeElement;
            xmlNfeNodeEnvEventoIdLoteEventoInfEvent.AppendChild(xmlNfeNodeEnvEventoIdLoteEventoInfEventTpAmb);
            xmlNfe.AppendChild(xmlNfeNode);
            // ---------------------------------------------------------------- fim

            // ----------------------------------------------------------------               
            xmlNfeElement = xmlNfe.CreateElement("CNPJ");
            xmlNfeElement.InnerText = cartaCorrecao.CNPJ;
            XmlNode xmlNfeNodeEnvEventoIdLoteEventoInfEventCNPJ = xmlNfeElement;
            xmlNfeNodeEnvEventoIdLoteEventoInfEvent.AppendChild(xmlNfeNodeEnvEventoIdLoteEventoInfEventCNPJ);
            xmlNfe.AppendChild(xmlNfeNode);
            // ---------------------------------------------------------------- fim

            // ----------------------------------------------------------------               
            xmlNfeElement = xmlNfe.CreateElement("CPF");
            xmlNfeElement.InnerText = cartaCorrecao.CPF;
            XmlNode xmlNfeNodeEnvEventoIdLoteEventoInfEventCPF = xmlNfeElement;
            xmlNfeNodeEnvEventoIdLoteEventoInfEvent.AppendChild(xmlNfeNodeEnvEventoIdLoteEventoInfEventCPF);
            xmlNfe.AppendChild(xmlNfeNode);
            // ---------------------------------------------------------------- fim

            // ----------------------------------------------------------------               
            xmlNfeElement = xmlNfe.CreateElement("chNFe");
            xmlNfeElement.InnerText = cartaCorrecao.chNFe;
            XmlNode xmlNfeNodeEnvEventoIdLoteEventoInfEventChNFe = xmlNfeElement;
            xmlNfeNodeEnvEventoIdLoteEventoInfEvent.AppendChild(xmlNfeNodeEnvEventoIdLoteEventoInfEventChNFe);
            xmlNfe.AppendChild(xmlNfeNode);
            // ---------------------------------------------------------------- fim

            // ----------------------------------------------------------------               
            xmlNfeElement = xmlNfe.CreateElement("dhEvento");
            xmlNfeElement.InnerText = cartaCorrecao.dhEvento;
            XmlNode xmlNfeNodeEnvEventoIdLoteEventoInfEventDhEvento = xmlNfeElement;
            xmlNfeNodeEnvEventoIdLoteEventoInfEvent.AppendChild(xmlNfeNodeEnvEventoIdLoteEventoInfEventDhEvento);
            xmlNfe.AppendChild(xmlNfeNode);
            // ---------------------------------------------------------------- fim

            // ----------------------------------------------------------------               
            xmlNfeElement = xmlNfe.CreateElement("tpEvento");
            xmlNfeElement.InnerText = cartaCorrecao.tpEvento;
            XmlNode xmlNfeNodeEnvEventoIdLoteEventoInfEventTpEvento = xmlNfeElement;
            xmlNfeNodeEnvEventoIdLoteEventoInfEvent.AppendChild(xmlNfeNodeEnvEventoIdLoteEventoInfEventTpEvento);
            xmlNfe.AppendChild(xmlNfeNode);
            // ---------------------------------------------------------------- fim

            // ----------------------------------------------------------------               
            xmlNfeElement = xmlNfe.CreateElement("nSeqEvento");
            xmlNfeElement.InnerText = cartaCorrecao.nSeqEvento;
            XmlNode xmlNfeNodeEnvEventoIdLoteEventoInfEventNSeqEvento = xmlNfeElement;
            xmlNfeNodeEnvEventoIdLoteEventoInfEvent.AppendChild(xmlNfeNodeEnvEventoIdLoteEventoInfEventNSeqEvento);
            xmlNfe.AppendChild(xmlNfeNode);
            // ---------------------------------------------------------------- fim

            // ----------------------------------------------------------------               
            xmlNfeElement = xmlNfe.CreateElement("verEvento");
            xmlNfeElement.InnerText = cartaCorrecao.verEvento;
            XmlNode xmlNfeNodeEnvEventoIdLoteEventoInfEventVerEvento = xmlNfeElement;
            xmlNfeNodeEnvEventoIdLoteEventoInfEvent.AppendChild(xmlNfeNodeEnvEventoIdLoteEventoInfEventVerEvento);
            xmlNfe.AppendChild(xmlNfeNode);
            // ---------------------------------------------------------------- fim

            // ----------------------------------------------------------------               
            xmlNfeElement = xmlNfe.CreateElement("detEvento");
            xmlNfeElement.InnerText = cartaCorrecao.detEvento;
            XmlNode xmlNfeNodeEnvEventoIdLoteEventoInfEventDetEvento = xmlNfeElement;
            xmlNfeNodeEnvEventoIdLoteEventoInfEvent.AppendChild(xmlNfeNodeEnvEventoIdLoteEventoInfEventDetEvento);
            xmlNfe.AppendChild(xmlNfeNode);
            // ---------------------------------------------------------------- fim

            // ----------------------------------------------------------------               
            xmlNfeElement = xmlNfe.CreateElement("versao");
            xmlNfeElement.InnerText = cartaCorrecao.versao;
            XmlNode xmlNfeNodeEnvEventoIdLoteEventoInfEventDetEventoVersao = xmlNfeElement;
            xmlNfeNodeEnvEventoIdLoteEventoInfEventDetEvento.AppendChild(xmlNfeNodeEnvEventoIdLoteEventoInfEventDetEventoVersao);
            xmlNfe.AppendChild(xmlNfeNode);
            // ---------------------------------------------------------------- fim

            // ----------------------------------------------------------------               
            xmlNfeElement = xmlNfe.CreateElement("descEvento");
            xmlNfeElement.InnerText = cartaCorrecao.descEvento;
            XmlNode xmlNfeNodeEnvEventoIdLoteEventoInfEventDetEventoDescEvento = xmlNfeElement;
            xmlNfeNodeEnvEventoIdLoteEventoInfEventDetEvento.AppendChild(xmlNfeNodeEnvEventoIdLoteEventoInfEventDetEventoDescEvento);
            xmlNfe.AppendChild(xmlNfeNode);
            // ---------------------------------------------------------------- fim

            // ----------------------------------------------------------------               
            xmlNfeElement = xmlNfe.CreateElement("xCorrecao");
            xmlNfeElement.InnerText = cartaCorrecao.xCorrecao;
            XmlNode xmlNfeNodeEnvEventoIdLoteEventoInfEventDetEventoDescXCorrecao = xmlNfeElement;
            xmlNfeNodeEnvEventoIdLoteEventoInfEventDetEvento.AppendChild(xmlNfeNodeEnvEventoIdLoteEventoInfEventDetEventoDescXCorrecao);
            xmlNfe.AppendChild(xmlNfeNode);
            // ---------------------------------------------------------------- fim

            // ----------------------------------------------------------------               
            xmlNfeElement = xmlNfe.CreateElement("xCondUso");
            xmlNfeElement.InnerText = cartaCorrecao.xCondUso;
            XmlNode xmlNfeNodeEnvEventoIdLoteEventoInfEventDetEventoDescXCondUso = xmlNfeElement;
            xmlNfeNodeEnvEventoIdLoteEventoInfEventDetEvento.AppendChild(xmlNfeNodeEnvEventoIdLoteEventoInfEventDetEventoDescXCondUso);
            xmlNfe.AppendChild(xmlNfeNode);
            // ---------------------------------------------------------------- fim

            // ----------------------------------------------------------------               
            xmlNfeElement = xmlNfe.CreateElement("Signature");
            XmlNode xmlNfeNodeEnvEventoIdLoteEventoInfEventDetEventoSignature = xmlNfeElement;
            xmlNfeNodeEnvEventoIdLoteEventoInfEvent.AppendChild(xmlNfeNodeEnvEventoIdLoteEventoInfEventDetEventoSignature);
            xmlNfe.AppendChild(xmlNfeNode);
            // ---------------------------------------------------------------- fim
        }
        #endregion

        #region Consulta Contribuinte
        public void consultaContribuinte(dadosCadastroContribuinte cadastroContribuinte)
        {
            XmlDocument xmlNfe = new XmlDocument();
            XmlElement xmlNfeElement;
            regrasNegocioNfe regrasNfe = new regrasNegocioNfe();
            XmlNode xmlNfeNode = xmlNfe.CreateXmlDeclaration("1.0", "UTF-8", null);
            xmlNfe.AppendChild(xmlNfeNode);

            // ----------------------------------------------------------------               
            xmlNfeElement = xmlNfe.CreateElement("ConsCad");
            xmlNfeElement.SetAttribute("versao", "3.10");
            xmlNfeNode = xmlNfeElement;
            xmlNfe.AppendChild(xmlNfeNode);
            // ---------------------------------------------------------------- fim

            // ----------------------------------------------------------------               
            xmlNfeElement = xmlNfe.CreateElement("infCons");
            XmlNode xmlNfeNodeConsCadInfCons = xmlNfeElement;
            xmlNfeNode.AppendChild(xmlNfeNodeConsCadInfCons);
            xmlNfe.AppendChild(xmlNfeNode);
            // ---------------------------------------------------------------- fim

            // ----------------------------------------------------------------               
            xmlNfeElement = xmlNfe.CreateElement("xServ");
            xmlNfeElement.InnerText = "CONS-CAD";
            XmlNode xmlNfeNodeConsCadInfConsXServ = xmlNfeElement;
            xmlNfeNodeConsCadInfCons.AppendChild(xmlNfeNodeConsCadInfConsXServ);
            xmlNfe.AppendChild(xmlNfeNode);
            // ---------------------------------------------------------------- fim

            // ----------------------------------------------------------------               
            xmlNfeElement = xmlNfe.CreateElement("UF");
            xmlNfeElement.InnerText = cadastroContribuinte.UF;
            XmlNode xmlNfeNodeConsCadInfConsUF = xmlNfeElement;
            xmlNfeNodeConsCadInfCons.AppendChild(xmlNfeNodeConsCadInfConsUF);
            xmlNfe.AppendChild(xmlNfeNode);
            // ---------------------------------------------------------------- 

            // ----------------------------------------------------------------               
            xmlNfeElement = xmlNfe.CreateElement("IE");
            xmlNfeElement.InnerText = cadastroContribuinte.IE;
            XmlNode xmlNfeNodeConsCadInfConsIE = xmlNfeElement;
            xmlNfeNodeConsCadInfCons.AppendChild(xmlNfeNodeConsCadInfConsIE);
            xmlNfe.AppendChild(xmlNfeNode);
            // ---------------------------------------------------------------- fim

            // ----------------------------------------------------------------               
            xmlNfeElement = xmlNfe.CreateElement("CNPJ");
            xmlNfeElement.InnerText = cadastroContribuinte.CNPJ;
            XmlNode xmlNfeNodeConsCadInfConsCNPJ = xmlNfeElement;
            xmlNfeNodeConsCadInfCons.AppendChild(xmlNfeNodeConsCadInfConsCNPJ);
            xmlNfe.AppendChild(xmlNfeNode);
            // ---------------------------------------------------------------- fim

            // ----------------------------------------------------------------               
            xmlNfeElement = xmlNfe.CreateElement("CPF");
            xmlNfeElement.InnerText = cadastroContribuinte.CPF;
            XmlNode xmlNfeNodeConsCadInfConsCPF = xmlNfeElement;
            xmlNfeNodeConsCadInfCons.AppendChild(xmlNfeNodeConsCadInfConsCPF);
            xmlNfe.AppendChild(xmlNfeNode);
            // ---------------------------------------------------------------- fim
        }
        #endregion

        #region Consulta Status
        public void consultaStatus(dadosStatusServidor statusServidor)
        {
            XmlDocument xmlNfe = new XmlDocument();
            XmlElement xmlNfeElement;
            regrasNegocioNfe regrasNfe = new regrasNegocioNfe();
            XmlNode xmlNfeNode = xmlNfe.CreateXmlDeclaration("1.0", "UTF-8", null);
            xmlNfe.AppendChild(xmlNfeNode);

            // ----------------------------------------------------------------               
            xmlNfeElement = xmlNfe.CreateElement("consStatServ");
            xmlNfeElement.SetAttribute("versao", "3.10");
            xmlNfeNode = xmlNfeElement;
            xmlNfe.AppendChild(xmlNfeNode);
            // ---------------------------------------------------------------- fim

            // ----------------------------------------------------------------               
            xmlNfeElement = xmlNfe.CreateElement("tpAmb");
            xmlNfeElement.InnerText = statusServidor.tpAmb;
            XmlNode xmlNfeNodeConsStatServTpAmb = xmlNfeElement;
            xmlNfeNode.AppendChild(xmlNfeNodeConsStatServTpAmb);
            xmlNfe.AppendChild(xmlNfeNode);
            // ---------------------------------------------------------------- fim

            // ----------------------------------------------------------------               
            xmlNfeElement = xmlNfe.CreateElement("cUF");
            xmlNfeElement.InnerText = statusServidor.cUF;
            XmlNode xmlNfeNodeConsStatServCUF = xmlNfeElement;
            xmlNfeNode.AppendChild(xmlNfeNodeConsStatServCUF);
            xmlNfe.AppendChild(xmlNfeNode);
            // ---------------------------------------------------------------- fim

            // ----------------------------------------------------------------               
            xmlNfeElement = xmlNfe.CreateElement("xServ");
            xmlNfeElement.InnerText = "STATUS";
            XmlNode xmlNfeNodeConsStatServXServ = xmlNfeElement;
            xmlNfeNode.AppendChild(xmlNfeNodeConsStatServXServ);
            xmlNfe.AppendChild(xmlNfeNode);
            // ---------------------------------------------------------------- fim
        }
        #endregion

        #region Consulta Situação da NF-e
        public void consultaSituacaoNfe(dadosProtocoloNFe protocoloNFe)
        {
            XmlDocument xmlNfe = new XmlDocument();
            XmlElement xmlNfeElement;
            regrasNegocioNfe regrasNfe = new regrasNegocioNfe();
            XmlNode xmlNfeNode = xmlNfe.CreateXmlDeclaration("1.0", "UTF-8", null);
            xmlNfe.AppendChild(xmlNfeNode);

            // ----------------------------------------------------------------               
            xmlNfeElement = xmlNfe.CreateElement("consSitNFe");
            xmlNfeElement.SetAttribute("versao", protocoloNFe.versao);
            xmlNfeNode = xmlNfeElement;
            xmlNfe.AppendChild(xmlNfeNode);
            // ---------------------------------------------------------------- fim

            // ----------------------------------------------------------------               
            xmlNfeElement = xmlNfe.CreateElement("tpAmb");
            xmlNfeElement.InnerText = protocoloNFe.tpAmb;
            XmlNode xmlNfeNodeConsSitNFeTpAmb = xmlNfeElement;
            xmlNfeNode.AppendChild(xmlNfeNodeConsSitNFeTpAmb);
            xmlNfe.AppendChild(xmlNfeNode);
            // ---------------------------------------------------------------- fim

            // ----------------------------------------------------------------               
            xmlNfeElement = xmlNfe.CreateElement("xServ");
            xmlNfeElement.InnerText = "CONSULTAR";
            XmlNode xmlNfeNodeConsSitNFeXServ = xmlNfeElement;
            xmlNfeNode.AppendChild(xmlNfeNodeConsSitNFeXServ);
            xmlNfe.AppendChild(xmlNfeNode);
            // ---------------------------------------------------------------- fim

            // ----------------------------------------------------------------               
            xmlNfeElement = xmlNfe.CreateElement("chNFe");
            xmlNfeElement.InnerText = protocoloNFe.xServ;
            XmlNode xmlNfeNodeConsSitNFeChNFe = xmlNfeElement;
            xmlNfeNode.AppendChild(xmlNfeNodeConsSitNFeChNFe);
            xmlNfe.AppendChild(xmlNfeNode);
            // ---------------------------------------------------------------- fim
        }
        #endregion

        #region Inutilização da NF-e
        public void inutilizacaoNFe(dadosInutilizacaoNFe inutilizacaoNFe)
        {
            XmlDocument xmlNfe = new XmlDocument();
            XmlElement xmlNfeElement;
            regrasNegocioNfe regrasNfe = new regrasNegocioNfe();
            XmlNode xmlNfeNode = xmlNfe.CreateXmlDeclaration("1.0", "UTF-8", null);
            xmlNfe.AppendChild(xmlNfeNode);

            // ----------------------------------------------------------------               
            xmlNfeElement = xmlNfe.CreateElement("inutNFe");
            xmlNfeElement.SetAttribute("versao", "3.10");
            xmlNfeNode = xmlNfeElement;
            xmlNfe.AppendChild(xmlNfeNode);
            // ---------------------------------------------------------------- fim

            // ----------------------------------------------------------------                
            xmlNfeElement = xmlNfe.CreateElement("infInut");
            xmlNfeElement.SetAttribute("Id", inutilizacaoNFe.Id);
            XmlNode xmlNfeNodeInfInut = xmlNfeElement;
            xmlNfeNode.AppendChild(xmlNfeNodeInfInut);
            xmlNfe.AppendChild(xmlNfeNode);
            // ---------------------------------------------------------------- fim

            // ----------------------------------------------------------------                
            xmlNfeElement = xmlNfe.CreateElement("tpAmb");
            xmlNfeElement.InnerText = inutilizacaoNFe.tpAmb;
            XmlNode xmlNfeNodeInfInutTpAmb = xmlNfeElement;
            xmlNfeNodeInfInut.AppendChild(xmlNfeNodeInfInutTpAmb);
            xmlNfe.AppendChild(xmlNfeNode);
            // ---------------------------------------------------------------- fim

            // ----------------------------------------------------------------                
            xmlNfeElement = xmlNfe.CreateElement("xServ");
            xmlNfeElement.InnerText = "INUTILIZAR";
            XmlNode xmlNfeNodeInfInutXServ = xmlNfeElement;
            xmlNfeNodeInfInut.AppendChild(xmlNfeNodeInfInutXServ);
            xmlNfe.AppendChild(xmlNfeNode);
            // ---------------------------------------------------------------- fim

            // ----------------------------------------------------------------                
            xmlNfeElement = xmlNfe.CreateElement("cUF");
            xmlNfeElement.InnerText = inutilizacaoNFe.cUF;
            XmlNode xmlNfeNodeInfInutCUF = xmlNfeElement;
            xmlNfeNodeInfInut.AppendChild(xmlNfeNodeInfInutCUF);
            xmlNfe.AppendChild(xmlNfeNode);
            // ---------------------------------------------------------------- fim

            // ----------------------------------------------------------------                
            xmlNfeElement = xmlNfe.CreateElement("ano");
            xmlNfeElement.InnerText = inutilizacaoNFe.ano;
            XmlNode xmlNfeNodeInfInutAno = xmlNfeElement;
            xmlNfeNodeInfInut.AppendChild(xmlNfeNodeInfInutAno);
            xmlNfe.AppendChild(xmlNfeNode);
            // ---------------------------------------------------------------- fim

            // ----------------------------------------------------------------                
            xmlNfeElement = xmlNfe.CreateElement("CNPJ");
            xmlNfeElement.InnerText = inutilizacaoNFe.CNPJ;
            XmlNode xmlNfeNodeInfInutCNPJ = xmlNfeElement;
            xmlNfeNodeInfInut.AppendChild(xmlNfeNodeInfInutCNPJ);
            xmlNfe.AppendChild(xmlNfeNode);
            // ---------------------------------------------------------------- fim

            // ----------------------------------------------------------------                
            xmlNfeElement = xmlNfe.CreateElement("mod");
            xmlNfeElement.InnerText = inutilizacaoNFe.mod;
            XmlNode xmlNfeNodeInfInutMod = xmlNfeElement;
            xmlNfeNodeInfInut.AppendChild(xmlNfeNodeInfInutMod);
            xmlNfe.AppendChild(xmlNfeNode);
            // ---------------------------------------------------------------- fim

            // ----------------------------------------------------------------                
            xmlNfeElement = xmlNfe.CreateElement("serie");
            xmlNfeElement.InnerText = inutilizacaoNFe.serie;
            XmlNode xmlNfeNodeInfInutSerie = xmlNfeElement;
            xmlNfeNodeInfInut.AppendChild(xmlNfeNodeInfInutSerie);
            xmlNfe.AppendChild(xmlNfeNode);
            // ---------------------------------------------------------------- fim

            // ----------------------------------------------------------------                
            xmlNfeElement = xmlNfe.CreateElement("nNFIni");
            xmlNfeElement.InnerText = inutilizacaoNFe.nNFIni;
            XmlNode xmlNfeNodeInfInutNNFIni = xmlNfeElement;
            xmlNfeNodeInfInut.AppendChild(xmlNfeNodeInfInutNNFIni);
            xmlNfe.AppendChild(xmlNfeNode);
            // ---------------------------------------------------------------- fim

            // ----------------------------------------------------------------                
            xmlNfeElement = xmlNfe.CreateElement("nNFFin");
            xmlNfeElement.InnerText = inutilizacaoNFe.nNFFin;
            XmlNode xmlNfeNodeInfInutNNFFin = xmlNfeElement;
            xmlNfeNodeInfInut.AppendChild(xmlNfeNodeInfInutNNFFin);
            xmlNfe.AppendChild(xmlNfeNode);
            // ---------------------------------------------------------------- fim

            // ----------------------------------------------------------------                
            xmlNfeElement = xmlNfe.CreateElement("xJust");
            xmlNfeElement.InnerText = inutilizacaoNFe.xJust;
            XmlNode xmlNfeNodeInfInutXJust = xmlNfeElement;
            xmlNfeNodeInfInut.AppendChild(xmlNfeNodeInfInutXJust);
            xmlNfe.AppendChild(xmlNfeNode);
            // ---------------------------------------------------------------- fim

            // ----------------------------------------------------------------                
            xmlNfeElement = xmlNfe.CreateElement("Signature");
            xmlNfeElement.InnerText = inutilizacaoNFe.Signature;
            XmlNode xmlNfeNodeSignature = xmlNfeElement;
            xmlNfeNode.AppendChild(xmlNfeNodeSignature);
            xmlNfe.AppendChild(xmlNfeNode);
            // ---------------------------------------------------------------- fim
        }
        #endregion

        // alterar caminho para salvar cancelamento
        #region Cancela NF-e
        public void cancelaNFe(dadosCancelaNfe cancelaNfe)
        {
            XmlDocument xmlNfe = new XmlDocument();
            XmlElement xmlNfeElement;
            regrasNegocioNfe regrasNfe = new regrasNegocioNfe();
            XmlNode xmlNfeNode = xmlNfe.CreateXmlDeclaration("1.0", "UTF-8", null);
            xmlNfe.AppendChild(xmlNfeNode);

            // ----------------------------------------------------------------
            xmlNfeElement = xmlNfe.CreateElement("cancNFe");
            xmlNfeElement.SetAttribute("versao", "3.10");
            xmlNfeNode = xmlNfeElement;
            xmlNfe.AppendChild(xmlNfeNode);
            // ---------------------------------------------------------------- fim

            // ----------------------------------------------------------------                
            xmlNfeElement = xmlNfe.CreateElement("infCanc");
            xmlNfeElement.SetAttribute("Id", cancelaNfe.Id);
            XmlNode xmlNfeNodeInfCanc = xmlNfeElement;
            xmlNfeNode.AppendChild(xmlNfeNodeInfCanc);
            xmlNfe.AppendChild(xmlNfeNode);
            // ---------------------------------------------------------------- fim

            // ----------------------------------------------------------------                
            xmlNfeElement = xmlNfe.CreateElement("tpAmb");
            xmlNfeElement.InnerText = cancelaNfe.tpAmb;
            XmlNode xmlNfeNodeInfCancTpAmb = xmlNfeElement;
            xmlNfeNodeInfCanc.AppendChild(xmlNfeNodeInfCancTpAmb);
            xmlNfe.AppendChild(xmlNfeNode);
            // ---------------------------------------------------------------- fim

            // ----------------------------------------------------------------                
            xmlNfeElement = xmlNfe.CreateElement("xServ");
            xmlNfeElement.InnerText = "CANCELAR";
            XmlNode xmlNfeNodeInfCancXServ = xmlNfeElement;
            xmlNfeNodeInfCanc.AppendChild(xmlNfeNodeInfCancXServ);
            xmlNfe.AppendChild(xmlNfeNode);
            // ---------------------------------------------------------------- fim

            // ----------------------------------------------------------------                
            xmlNfeElement = xmlNfe.CreateElement("chNFe");
            xmlNfeElement.InnerText = cancelaNfe.chNFe;
            XmlNode xmlNfeNodeInfCancChNFe = xmlNfeElement;
            xmlNfeNodeInfCanc.AppendChild(xmlNfeNodeInfCancChNFe);
            xmlNfe.AppendChild(xmlNfeNode);
            // ---------------------------------------------------------------- fim

            // ----------------------------------------------------------------                
            xmlNfeElement = xmlNfe.CreateElement("nProt");
            xmlNfeElement.InnerText = cancelaNfe.nProt;
            XmlNode xmlNfeNodeInfCancNProt = xmlNfeElement;
            xmlNfeNodeInfCanc.AppendChild(xmlNfeNodeInfCancNProt);
            xmlNfe.AppendChild(xmlNfeNode);
            // ---------------------------------------------------------------- fim

            // ----------------------------------------------------------------                
            xmlNfeElement = xmlNfe.CreateElement("xJust");
            xmlNfeElement.InnerText = cancelaNfe.xJust;
            XmlNode xmlNfeNodeInfCancXJust = xmlNfeElement;
            xmlNfeNodeInfCanc.AppendChild(xmlNfeNodeInfCancNProt);
            xmlNfe.AppendChild(xmlNfeNode);
            // ---------------------------------------------------------------- fim

            // ----------------------------------------------------------------                
            xmlNfeElement = xmlNfe.CreateElement("Signature");
            xmlNfeElement.InnerText = cancelaNfe.Signature;
            XmlNode xmlNfeNodeSignature = xmlNfeElement;
            xmlNfeNode.AppendChild(xmlNfeNodeSignature);
            xmlNfe.AppendChild(xmlNfeNode);
            // ---------------------------------------------------------------- fim

            //xmlNfe.Save(@"\nfe\cancelanfe\" + DateTime.Today.ToString().Replace("-", string.Empty) + ".xml");
            //xmlNfe.Save(@"D:\Alexandre\Work\ERPRO\Documentos\Notas Fiscais\" + DateTime.Today.ToString().Replace("-", string.Empty) + ".xml");
            assinaturaNfe assina = new assinaturaNfe();
            webserviceInutilizacaoHomo.NfeInutilizacao2 inutilizaNfe = new webserviceInutilizacaoHomo.NfeInutilizacao2();
            assina.assinarNfe("", "Signature", cancelaNfe.Id, chaveAcessoGeradaNfe);
            X509Certificate2Collection colecaoCertificado = assina.carregaCertificados();
            if (colecaoCertificado.Count > 0)
            {
                inutilizaNfe.ClientCertificates.Add((assina.carregaCertificados())[0]);
                //regrasNfe.verificaMensagemRetornoRecepcao(inutilizaNfe.nfeInutilizacaoNF2(xmlNfe.DocumentElement));
            }
        }
        #endregion

        #region Consulta XML - Processamento em Lote
        public void consultaXml(dadosConsultaProcessamentoLote consultaProcessamentoLote)
        {
            XmlDocument xmlNfe = new XmlDocument();
            XmlElement xmlNfeElement;
            regrasNegocioNfe regrasNfe = new regrasNegocioNfe();
            XmlNode xmlNfeNode = xmlNfe.CreateXmlDeclaration("1.0", "UTF-8", null);
            xmlNfe.AppendChild(xmlNfeNode);

            // ----------------------------------------------------------------               
            xmlNfeElement = xmlNfe.CreateElement("consReciNFe");
            xmlNfeElement.SetAttribute("versao", "3.10");
            xmlNfeNode = xmlNfeElement;
            xmlNfe.AppendChild(xmlNfeNode);
            // ---------------------------------------------------------------- fim

            // ----------------------------------------------------------------                
            xmlNfeElement = xmlNfe.CreateElement("tpAmb");
            xmlNfeElement.InnerText = consultaProcessamentoLote.tpAmb;
            XmlNode xmlNfeNodeTpAmb = xmlNfeElement;
            xmlNfeNode.AppendChild(xmlNfeNodeTpAmb);
            xmlNfe.AppendChild(xmlNfeNode);
            // ---------------------------------------------------------------- fim

            // ----------------------------------------------------------------                
            xmlNfeElement = xmlNfe.CreateElement("nRec");
            xmlNfeElement.InnerText = consultaProcessamentoLote.nRec;
            XmlNode xmlNfeNodeNRec = xmlNfeElement;
            xmlNfeNode.AppendChild(xmlNfeNodeNRec);
            xmlNfe.AppendChild(xmlNfeNode);
            // ---------------------------------------------------------------- fim

            assinaturaNfe assina = new assinaturaNfe();
            webserviceRetAutorizacaoLoteHomo.NfeRetAutorizacao respConsulta = new webserviceRetAutorizacaoLoteHomo.NfeRetAutorizacao();
            X509Certificate2Collection colecaoCertificado = assina.carregaCertificados();
            if (colecaoCertificado.Count > 0)
            {
                respConsulta.ClientCertificates.Add((assina.carregaCertificados())[0]);
                regrasNfe.verificaMensagemRetornoRecepcao(respConsulta.nfeRetAutorizacaoLote(xmlNfe.DocumentElement));
            }
        }
        #endregion

        /// <summary>
        /// Gera o lote da NF-e versão 3.10
        /// </summary>
        /// <param name="tipoMetodo">Parâmetro que define o tipo de cabeçalho que será gerado para a NF-e.  1 -  Recepção da NF-e (envio da nota fiscal gerada para o servidor da Receita Federal) | 2 - Distribuição da NF-e (XML enviado para o cliente) </param>
        /// <param name="listaIdentificacaoNfe">Dados de identificação da NF-e</param>
        /// <param name="listaDocumentoFiscalDiferenciado"> Dados de documento fiscal referenciado</param>
        /// <param name="listaIdentificacaoEmitenteNfe">Dados do emitente da NF-e</param>
        /// <param name="listaIdentificacaoDestinatarioNfe">Dados do destinatário</param>
        /// <param name="listaIdentificacaoLocalRetirada">Dados da retirada</param>
        /// <param name="listaIdentificacaoLocalEntrega">Dados do local de entrega</param>
        /// <param name="listaAutorizacaoObterXml">Dados para autorização para gerar XML</param>
        /// <param name="listaDetalhamentoProdutosNfe">Dados detalhes dos produtos/serviços da NF-e</param>
        /// <param name="listaProdutosServicosNfe">Dados listando os produtos/serviços</param>
        /// <param name="listaProdutosServicosDeclaracaoImportacao">Dados de declaração de importação</param>
        /// <param name="listaProdutosServicosGrupoExportacao">Dados de grupos de exportação</param>
        /// <param name="listaProdutosServicosPedidoCompra">Dados de pedidos de compra</param>
        /// <param name="listaProdutosServicosGrupoDiversos">Dados de grupos diversos</param>
        /// <param name="listaDetalhamentoEspecificoVeiculosNovos"> Dados específicos para veículos novos</param>
        /// <param name="listaDetalhamentoEspecificoMedicamentoMateriasPrimasFarmaceuticas">Dados específicos para materias primas farmaceuticas</param>
        /// <param name="listaDetalhamentoEspecificoArmamentos">Dados específicos para armamentos</param>
        /// <param name="listaDetalhamentoEspecificoCombustiveis">Dados específicos para combustíveis</param>
        /// <param name="listaDetalhamentoEspecificoOperacaoPapelImune">Dados específicos para operações com papel imune</param>
        /// <param name="listaTributosIncidentesProdutoServico">Dados de tributos</param>
        /// <param name="listaICMSNormalST">Dados de ICMS normal ST</param>
        /// <param name="listaImpostoProdutosIndustrializados">Dados de IPI (imposto sobre produtos industrializados)</param>
        /// <param name="listaImpostoImportacao"> Dados de II (imposto de importação)</param>
        /// <param name="listaPis">Dados de PIS</param>
        /// <param name="listaPisST">Dados de PIS ST</param>
        /// <param name="listaCofins">Dados de COFINS</param>
        /// <param name="listaCofinsST">Dados de CONFINS ST</param>
        /// <param name="listaISSQN">Dados de ISSQN</param>
        /// <param name="listaTributosDevolvidos">Dados de tributos devolvidos</param>
        /// <param name="listaInformacoesAdicionais">Dados de informações adicionais</param>
        /// <param name="listaTotalNFe">Dados do total da NF-e</param>
        /// <param name="listaTotalNFeISSQN">Dados do total do ISSQN na NF-e</param>
        /// <param name="listaTotalNFeRetencaoTributos">Dados de retenção de produtos</param>
        /// <param name="listaInformacoesTransporteNFe">Dados de informações de transporte</param>
        /// <param name="listaDadosCobranca">Dados de cobrança</param>
        /// <param name="listaFormasPagamento">Dados de forma de pagamento</param>
        /// <param name="listaInformacoesAdicionaisNFe">Dados de informações adicionais</param>
        /// <param name="listaInformacoesComercioExterior">Dados de informações do comércio exterior</param>
        /// <param name="listaInformacoesCompras">Dados informações de compras</param>
        /// <param name="listaInformacoesRegistroAquisicaoCana">Dados de registros de aquisição de cana</param>
        /// <param name="listaAssinatura">Assinatura da NF-e</param>
        public dadosConsultaProcessamentoLote geraNotaFiscalEletronica(Int16 tipoMetodo,
            List<dadosIdentificacaoNfe> listaIdentificacaoNfe,
            List<dadosDocumentoFiscalDiferenciado> listaDocumentoFiscalDiferenciado,
            List<dadosIdentificacaoEmitenteNfe> listaIdentificacaoEmitenteNfe,
            List<dadosIdentificacaoDestinatárioNfe> listaIdentificacaoDestinatarioNfe,
            List<dadosIdentificacaoLocalRetirada> listaIdentificacaoLocalRetirada,
            List<dadosIdentificacaoLocalEntrega> listaIdentificacaoLocalEntrega,
            List<dadosAutorizacaoObterXml> listaAutorizacaoObterXml,
            List<dadosDetalhamentoProdutosNfe> listaDetalhamentoProdutosNfe,
            List<dadosProdutosServicosNfe> listaProdutosServicosNfe,
            List<dadosProdutosServicosDeclaracaoImportacao> listaProdutosServicosDeclaracaoImportacao,
            List<dadosProdutosServicosGrupoExportacao> listaProdutosServicosGrupoExportacao,
            List<dadosProdutosServicosPedidoCompra> listaProdutosServicosPedidoCompra,
            List<dadosProdutosServicosGrupoDiversos> listaProdutosServicosGrupoDiversos,
            List<dadosDetalhamentoEspecificoVeiculosNovos> listaDetalhamentoEspecificoVeiculosNovos,
            List<dadosDetalhamentoEspecificoMedicamentoMateriasPrimasFarmaceuticas> listaDetalhamentoEspecificoMedicamentoMateriasPrimasFarmaceuticas,
            List<dadosDetalhamentoEspecificoArmamentos> listaDetalhamentoEspecificoArmamentos,
            List<dadosDetalhamentoEspecificoCombustiveis> listaDetalhamentoEspecificoCombustiveis,
            List<dadosDetalhamentoEspecificoOperacaoPapelImune> listaDetalhamentoEspecificoOperacaoPapelImune,
            List<dadosTributosIncidentesProdutoServico> listaTributosIncidentesProdutoServico,
            List<dadosICMSNormalST> listaICMSNormalST,
            List<dadosImpostoProdutosIndustrializados> listaImpostoProdutosIndustrializados,
            List<dadosImpostoImportacao> listaImpostoImportacao,
            List<dadosPis> listaPis,
            List<dadosPisST> listaPisST,
            List<dadosCofins> listaCofins,
            List<dadosCofinsST> listaCofinsST,
            List<dadosISSQN> listaISSQN,
            List<dadosTributosDevolvidos> listaTributosDevolvidos,
            List<dadosInformacoesAdicionais> listaInformacoesAdicionais,
            List<dadosTotalNFe> listaTotalNFe,
            List<dadosTotalNFeISSQN> listaTotalNFeISSQN,
            List<dadosTotalNFeRetencaoTributos> listaTotalNFeRetencaoTributos,
            List<dadosInformacoesTransporteNFe> listaInformacoesTransporteNFe,
            List<dadosDadosCobranca> listaDadosCobranca,
            List<dadosFormasPagamento> listaFormasPagamento,
            List<dadosInformacoesAdicionaisNFe> listaInformacoesAdicionaisNFe,
            List<dadosInformacoesComercioExterior> listaInformacoesComercioExterior,
            List<dadosInformacoesCompras> listaInformacoesCompras,
            List<dadosInformacoesRegistroAquisicaoCana> listaInformacoesRegistroAquisicaoCana,
            List<dadosAssinatura> listaAssinatura)
        {
            tipoRequisicao = tipoMetodo;
            #region Lista de variáveis
            dadosIdentificacaoNfe identificacaoNfe = new dadosIdentificacaoNfe(null,
                null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);

            dadosDocumentoFiscalDiferenciado documentoFiscalDiferenciado = new dadosDocumentoFiscalDiferenciado(null, null, null,
                null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);

            dadosIdentificacaoEmitenteNfe IdentificacaoEmitenteNfe = new dadosIdentificacaoEmitenteNfe(null, null, null, null, null,
                null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);

            dadosIdentificacaoFiscoEmitenteNfe IdentificacaoFiscoEmitenteNfe = new dadosIdentificacaoFiscoEmitenteNfe(null, null,
                null, null, null, null, null, null, null, null, null);

            dadosIdentificacaoDestinatárioNfe IdentificacaoDestinatarioNfe = new dadosIdentificacaoDestinatárioNfe(null, null, null, null,
                null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);

            dadosIdentificacaoLocalRetirada IdentificacaoLocalRetirada = new dadosIdentificacaoLocalRetirada(null, null, null,
                null, null, null, null, null, null);

            dadosIdentificacaoLocalEntrega IdentificacaoLocalEntrega = new dadosIdentificacaoLocalEntrega(null, null, null,
                null, null, null, null, null, null);

            dadosAutorizacaoObterXml AutorizacaoObterXml = new dadosAutorizacaoObterXml(null, null, null);

            dadosDetalhamentoProdutosNfe DetalhamentoProdutosNfe = new dadosDetalhamentoProdutosNfe(null, null);

            dadosProdutosServicosNfe ProdutosServicosNfe = new dadosProdutosServicosNfe(null, null, null, null, null, null, null,
                null, null, null, null, null, null, null, null, null, null, null, null, null);

            dadosProdutosServicosDeclaracaoImportacao ProdutosServicosDeclaracaoImportacao = new dadosProdutosServicosDeclaracaoImportacao(null,
                null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);

            dadosProdutosServicosGrupoExportacao ProdutosServicosGrupoExportacao = new dadosProdutosServicosGrupoExportacao(null, null,
                null, null, null);

            dadosProdutosServicosPedidoCompra ProdutosServicosPedidoCompra = new dadosProdutosServicosPedidoCompra(null, null);

            dadosProdutosServicosGrupoDiversos ProdutosServicosGrupoDiversos = new dadosProdutosServicosGrupoDiversos(null);

            dadosDetalhamentoEspecificoVeiculosNovos DetalhamentoEspecificoVeiculosNovos = new dadosDetalhamentoEspecificoVeiculosNovos(null, null,
                null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);

            dadosDetalhamentoEspecificoMedicamentoMateriasPrimasFarmaceuticas DetalhamentoEspecificoMedicamentoMateriasPrimasFarmaceuticas = new dadosDetalhamentoEspecificoMedicamentoMateriasPrimasFarmaceuticas(
                null, null, null, null, null, null);

            dadosDetalhamentoEspecificoArmamentos DetalhamentoEspecificoArmamentos = new dadosDetalhamentoEspecificoArmamentos(null,
                null, null, null, null);

            dadosDetalhamentoEspecificoCombustiveis DetalhamentoEspecificoCombustiveis = new dadosDetalhamentoEspecificoCombustiveis(
                null, null, null, null, null, null, null, null);

            dadosDetalhamentoEspecificoOperacaoPapelImune DetalhamentoEspecificoOperacaoPapelImune = new dadosDetalhamentoEspecificoOperacaoPapelImune(null);

            dadosTributosIncidentesProdutoServico TributosIncidentesProdutoServico = new dadosTributosIncidentesProdutoServico(null);

            dadosICMSNormalST ICMSNormalST = new dadosICMSNormalST(null, null, null, null, null, null, null, null, null, null, null, null,
                null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,
                null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,
                null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,
                null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,
                null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,
                null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);

            dadosImpostoProdutosIndustrializados ImpostoProdutosIndustrializados = new dadosImpostoProdutosIndustrializados(null, null,
                null, null, null, null, null, null, null, null, null);

            dadosImpostoImportacao ImpostoImportacao = new dadosImpostoImportacao(null, null, null, null);

            dadosPis Pis = new dadosPis(null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);

            dadosPisST PisST = new dadosPisST(null, null, null, null, null);

            dadosCofins Cofins = new dadosCofins(null, null, null, null, null, null, null, null, null, null, null, null,
                null, null, null);

            dadosCofinsST CofinsST = new dadosCofinsST(null, null, null, null, null);

            dadosISSQN ISSQN = new dadosISSQN(null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);

            dadosTributosDevolvidos TributosDevolvidos = new dadosTributosDevolvidos(null, null, null);

            dadosInformacoesAdicionais InformacoesAdicionais = new dadosInformacoesAdicionais(null);

            dadosTotalNFe TotalNFe = new dadosTotalNFe(null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);

            dadosTotalNFeISSQN TotalNFeISSQN = new dadosTotalNFeISSQN(null, null, null, null, null, null, null, null, null, null, null, null);

            dadosTotalNFeRetencaoTributos TotalNFeRetencaoTributos = new dadosTotalNFeRetencaoTributos(null, null,
                null, null, null, null, null);

            dadosInformacoesTransporteNFe InformacoesTransporteNFe = new dadosInformacoesTransporteNFe(null, null, null, null, null,
                null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,
                null, null, null, null, null, null);

            dadosDadosCobranca DadosCobranca = new dadosDadosCobranca(null, null, null, null, null, null, null, null);

            dadosFormasPagamento FormasPagamento = new dadosFormasPagamento(null, null, null, null, null, null);

            dadosInformacoesAdicionaisNFe InformacoesAdicionaisNFe = new dadosInformacoesAdicionaisNFe(null, null, null, null,
                null, null, null, null, null);

            dadosInformacoesComercioExterior InformacoesComercioExterior = new dadosInformacoesComercioExterior(null, null, null);

            dadosInformacoesCompras InformacoesCompras = new dadosInformacoesCompras(null, null, null);

            dadosInformacoesRegistroAquisicaoCana InformacoesRegistroAquisicaoCana = new dadosInformacoesRegistroAquisicaoCana(null, null,
                null, null, null, null, null, null, null, null, null, null, null);

            dadosAssinatura Assinatura = new dadosAssinatura(null, null, null, null);

            dadosEnvioNfeLote EnvioNfeLote = new dadosEnvioNfeLote(null, null);
            #endregion
            //gera o código de acesso antes de começar a montar a NF

            try
            {
                //cria documento
                XmlDocument xmlNfe = new XmlDocument();
                XmlElement xmlNfeElement;
                regrasNegocioNfe regrasNfe = new regrasNegocioNfe();

                dadosChaveAcessoNfe chaveAcessoNfe = new dadosChaveAcessoNfe(listaIdentificacaoNfe[0].ide_cUF, DateTime.Today.Year.ToString().Substring(2, 2) + DateTime.Today.Month.ToString("00"), listaIdentificacaoEmitenteNfe[0].emit_CNPJ.ToString(), listaIdentificacaoNfe[0].ide_mod, listaIdentificacaoNfe[0].ide_serie.PadLeft(3, '0'), listaIdentificacaoNfe[0].ide_nNF.PadLeft(9, '0'), tipoRequisicao.ToString(), regrasNfe.geraCodigoNumerico(), null);
                chaveAcessoGeradaNfe = regrasNfe.geraChaveAcesso(chaveAcessoNfe);

                // ------------------ ITENS COM VALOR PADRÃO, CALCULADOS OU GERADOS --------------

                // ------------------ FIM

                //configura o cabeçalho com versao e tipo de codificaçao
                XmlNode xmlNfeNode = xmlNfe.CreateXmlDeclaration("1.0", "UTF-8", null);
                xmlNfe.AppendChild(xmlNfeNode);


                if (tipoRequisicao == 2)
                {
                    // ---------------------------------------------------------------- início nfeProc -- n1                
                    xmlNfeElement = xmlNfe.CreateElement(null, "nfeProc", "http://www.portalfiscal.inf.br/nfe");
                    xmlNfeElement.SetAttribute("versao", "3.10");
                    xmlNfeNode = xmlNfeElement;
                    xmlNfe.AppendChild(xmlNfeNode);
                    // ---------------------------------------------------------------- fim
                }

                if (tipoRequisicao == 1) //Recepção NFe
                {
                    // ---------------------------------------------------------------- início nfeProc -- n1                
                    xmlNfeElement = xmlNfe.CreateElement("enviNFe");
                    xmlNfeElement.SetAttribute("xmlns", "http://www.portalfiscal.inf.br/nfe");
                    xmlNfeElement.SetAttribute("versao", "3.10");
                    xmlNfeNode = xmlNfeElement;
                    xmlNfe.AppendChild(xmlNfeNode);
                    // ---------------------------------------------------------------- fim

                    // ---------------------------------------------------------------- início nfeProc -- n1                
                    xmlNfeElement = xmlNfe.CreateElement("idLote");
                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos("55", 2, 1, 15, false, null, "idLote");
                    XmlNode xmlNfeNodeIdLote = xmlNfeElement;
                    xmlNfeNode.AppendChild(xmlNfeNodeIdLote);
                    xmlNfe.AppendChild(xmlNfeNode);
                    // ---------------------------------------------------------------- fim

                    // ---------------------------------------------------------------- início indSinc -- n1                
                    xmlNfeElement = xmlNfe.CreateElement("indSinc");
                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos("1", 2, 1, 1, false, null, "indSinc");
                    XmlNode xmlNfeNodeIndSinc = xmlNfeElement;
                    xmlNfeNode.AppendChild(xmlNfeNodeIndSinc);
                    xmlNfe.AppendChild(xmlNfeNode);
                    // ---------------------------------------------------------------- fim
                }

                qtdNFe = 0;
                contadorGlobal = listaIdentificacaoNfe.Count >= 50 ? 50 : listaIdentificacaoNfe.Count;
                for (int i = 0; i < contadorGlobal; i++)
                {
                    // ---------------------------------------------------------------- inicio NFe -- Raiz
                    xmlNfeElement = xmlNfe.CreateElement("NFe");
                    xmlNfeElement.SetAttribute("xmlns", "http://www.portalfiscal.inf.br/nfe");
                    XmlNode xmlNfeNodeNF = xmlNfeElement;
                    xmlNfeNode.AppendChild(xmlNfeNodeNF);
                    //if (tipoRequisicao == 1)
                    //    xmlNfeNodeEnviNFe.AppendChild(xmlNfeNodeNF);
                    //else
                    //    xmlNfeNode.AppendChild(xmlNfeNodeNF);
                    xmlNfe.AppendChild(xmlNfeNode);
                    //----------------------------------------------------------------- fim

                    #region #A Dados da Nota Fiscal Eletrônica
                    // ---------------------------------------------------------------- inicio infNFe -- A01 - fRaiz
                    xmlNfeElement = xmlNfe.CreateElement("infNFe");
                    xmlNfeElement.SetAttribute("versao", "3.10"); //versão -- A02 - Atributo,
                    xmlNfeElement.SetAttribute("Id", "NFe" + chaveAcessoGeradaNfe); //Id -- A03 - Atributo
                    XmlNode xmlNfeNodeInfNfe = xmlNfeElement;
                    xmlNfeNodeNF.AppendChild(xmlNfeNodeInfNfe);
                    xmlNfe.AppendChild(xmlNfeNode);
                    //----------------------------------------------------------------- fim

                    //// ---------------------------------------------------------------- inicio pk_nItem -- A04 - f-A01
                    //xmlNfeElement = xmlNfe.CreateElement("pk_nItem");
                    //XmlNode xmlNfeNodePk_nItem = xmlNfeElement;
                    //xmlNfeNodeNF.AppendChild(xmlNfeNodePk_nItem);
                    //xmlNfe.AppendChild(xmlNfeNode);
                    ////----------------------------------------------------------------- fim
                    #endregion

                    #region #B Identificação da Nota Fiscal eletrônica
                    identificacaoNfe = listaIdentificacaoNfe[i];
                    identificacaoNfe.ide_cNF = chaveAcessoNfe.codigoNumero; // passa o parâmetro de código de acesso gerado para a lista
                    // ---------------------------------------------------------------- inicio ide -- B01 - f-A01
                    xmlNfeElement = xmlNfe.CreateElement("ide");
                    XmlNode xmlNfeNodeIde = xmlNfeElement;
                    xmlNfeNodeInfNfe.AppendChild(xmlNfeNodeIde);
                    xmlNfe.AppendChild(xmlNfeNode);
                    //----------------------------------------------------------------- fim

                    // ---------------------------------------------------------------- inicio cUF -- B02 - f-B01
                    xmlNfeElement = xmlNfe.CreateElement("cUF");
                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(identificacaoNfe.ide_cUF, 2, 2, 2, false, null, "B02");
                    XmlNode xmlNfeNodeCuf = xmlNfeElement;
                    xmlNfeNodeIde.AppendChild(xmlNfeNodeCuf);
                    xmlNfe.AppendChild(xmlNfeNode);
                    //----------------------------------------------------------------- fim

                    // ---------------------------------------------------------------- inicio cNF -- B03 - f-B01
                    xmlNfeElement = xmlNfe.CreateElement("cNF");
                    xmlNfeElement.InnerText = identificacaoNfe.ide_cNF;
                    XmlNode xmlNfeNodeCnf = xmlNfeElement;
                    xmlNfeNodeIde.AppendChild(xmlNfeNodeCnf);
                    xmlNfe.AppendChild(xmlNfeNode);
                    //----------------------------------------------------------------- fim

                    // ---------------------------------------------------------------- inicio natOp -- B04 - B01
                    xmlNfeElement = xmlNfe.CreateElement("natOp");
                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(identificacaoNfe.ide_natOp, 2, 1, 60, false, null, "B04");
                    XmlNode xmlNfeNodeNatOp = xmlNfeElement;
                    xmlNfeNodeIde.AppendChild(xmlNfeNodeNatOp);
                    xmlNfe.AppendChild(xmlNfeNode);
                    //----------------------------------------------------------------- fim

                    // ---------------------------------------------------------------- inicio indPag -- B05 - B01
                    if (regrasNfe.verificaIndPag(identificacaoNfe.ide_indPag))
                    {
                        xmlNfeElement = xmlNfe.CreateElement("indPag");
                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(identificacaoNfe.ide_indPag, 2, 1, 1, false, null, "B05");
                        XmlNode xmlNfeNodeIndPag = xmlNfeElement;
                        xmlNfeNodeIde.AppendChild(xmlNfeNodeIndPag);
                        xmlNfe.AppendChild(xmlNfeNode);
                    }
                    else
                    {

                        return null;
                    }
                    //----------------------------------------------------------------- fim

                    // ---------------------------------------------------------------- inicio mod -- B06 - f-B01
                    if (regrasNfe.verificaModeloNfe(identificacaoNfe.ide_mod))
                    {
                        xmlNfeElement = xmlNfe.CreateElement("mod");
                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(identificacaoNfe.ide_mod, 2, 1, 2, false, null, "B06");
                        XmlNode xmlNfeNodeMod = xmlNfeElement;
                        xmlNfeNodeIde.AppendChild(xmlNfeNodeMod);
                        xmlNfe.AppendChild(xmlNfeNode);
                    }
                    else
                    {

                        return null;
                    }
                    //----------------------------------------------------------------- fim

                    // ---------------------------------------------------------------- inicio serie -- B07 - f-B01
                    if (regrasNfe.verificaSerie(identificacaoNfe.ide_serie))
                    {
                        xmlNfeElement = xmlNfe.CreateElement("serie");
                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(identificacaoNfe.ide_serie, 2, 1, 3, false, null, "B07");
                        XmlNode xmlNfeNodeSerie = xmlNfeElement;
                        xmlNfeNodeIde.AppendChild(xmlNfeNodeSerie);
                        xmlNfe.AppendChild(xmlNfeNode);
                    }
                    else
                    {

                        return null;
                    }
                    //----------------------------------------------------------------- fim

                    // ---------------------------------------------------------------- inicio nNF -- B08 - f-B01
                    xmlNfeElement = xmlNfe.CreateElement("nNF");
                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(identificacaoNfe.ide_nNF, 2, 1, 9, false, null, "B08");
                    XmlNode xmlNfeNodeNnf = xmlNfeElement;
                    xmlNfeNodeIde.AppendChild(xmlNfeNodeNnf);
                    xmlNfe.AppendChild(xmlNfeNode);
                    //----------------------------------------------------------------- fim

                    // ---------------------------------------------------------------- inicio nNF -- B09 - f-B01
                    xmlNfeElement = xmlNfe.CreateElement("dhEmi");
                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(identificacaoNfe.ide_dhEmi, 4, null, null, false, null, "B09");
                    XmlNode xmlNfeNodeDhEmi = xmlNfeElement;
                    xmlNfeNodeIde.AppendChild(xmlNfeNodeDhEmi);
                    xmlNfe.AppendChild(xmlNfeNode);
                    //----------------------------------------------------------------- fim

                    // ---------------------------------------------------------------- inicio dSaiEnt -- B10 - f-B01
                    if (regrasNfe.verificaTipoModelo55NFe(identificacaoNfe.ide_mod))
                    {
                        if (regrasNfe.verificaCampoVazio(identificacaoNfe.ide_dhSaiEnt))
                        {
                            xmlNfeElement = xmlNfe.CreateElement("dhSaiEnt");
                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(identificacaoNfe.ide_dhSaiEnt, 4, null, null, false, null, "B10");
                            XmlNode xmlNfeNodeDhSaiEnt = xmlNfeElement;
                            xmlNfeNodeIde.AppendChild(xmlNfeNodeDhSaiEnt);
                            xmlNfe.AppendChild(xmlNfeNode);
                        }
                    }
                    //----------------------------------------------------------------- fim

                    // ---------------------------------------------------------------- inicio tpNF -- B11 - f-B01
                    if (regrasNfe.verificaTipoOperacao(identificacaoNfe.ide_tpNF))
                    {
                        xmlNfeElement = xmlNfe.CreateElement("tpNF");
                        xmlNfeElement.InnerText = identificacaoNfe.ide_tpNF;
                        XmlNode xmlNfeNodeTpNf = xmlNfeElement;
                        xmlNfeNodeIde.AppendChild(xmlNfeNodeTpNf);
                        xmlNfe.AppendChild(xmlNfeNode);
                    }
                    else
                    {

                        return null;
                    }
                    //----------------------------------------------------------------- fim

                    // ---------------------------------------------------------------- inicio idDest -- B11a - f-B01
                    if (regrasNfe.verificaIdentificadorDestino(identificacaoNfe.ide_idDest))
                    {
                        xmlNfeElement = xmlNfe.CreateElement("idDest");
                        xmlNfeElement.InnerText = identificacaoNfe.ide_idDest;
                        XmlNode xmlNfeNodeIdDest = xmlNfeElement;
                        xmlNfeNodeIde.AppendChild(xmlNfeNodeIdDest);
                        xmlNfe.AppendChild(xmlNfeNode);
                    }
                    else
                    {

                        return null;
                    }
                    //----------------------------------------------------------------- fim

                    // ---------------------------------------------------------------- inicio cMunFG -- B12 - f-B01
                    if (regrasNfe.verificaOperacaoExterior(identificacaoNfe.ide_cMunFG))
                        identificacaoNfe.ide_cMunFG = regrasNfe.preencheCodigoMunicipioExterior();

                    if (regrasNfe.verificaMunicipioEstado(identificacaoNfe.ide_cMunFG, identificacaoNfe.ide_cUF) || identificacaoNfe.ide_cMunFG.Equals(regrasNfe.preencheCodigoMunicipioExterior()))
                    {
                        xmlNfeElement = xmlNfe.CreateElement("cMunFG");
                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(identificacaoNfe.ide_cMunFG, 2, 1, 7, false, null, "B12");
                        XmlNode xmlNfeNodeCMunFG = xmlNfeElement;
                        xmlNfeNodeIde.AppendChild(xmlNfeNodeCMunFG);
                        xmlNfe.AppendChild(xmlNfeNode);
                    }
                    else
                    {

                        return null;
                    }
                    //----------------------------------------------------------------- fim

                    // ---------------------------------------------------------------- inicio tpImp -- B21 - f-B01
                    if (regrasNfe.verificaTipoImpressao(identificacaoNfe.ide_tpImp))
                    {
                        xmlNfeElement = xmlNfe.CreateElement("tpImp");
                        xmlNfeElement.InnerText = identificacaoNfe.ide_tpImp;
                        XmlNode xmlNfeNodeTpImp = xmlNfeElement;
                        xmlNfeNodeIde.AppendChild(xmlNfeNodeTpImp);
                        xmlNfe.AppendChild(xmlNfeNode);
                    }
                    else
                    {
                        return null;
                    }
                    //----------------------------------------------------------------- fim

                    // ---------------------------------------------------------------- inicio tpEmis -- B22 - f-B01
                    if (regrasNfe.verificaTipoEmissao(identificacaoNfe.ide_tpEmis))
                    {
                        xmlNfeElement = xmlNfe.CreateElement("tpEmis");
                        xmlNfeElement.InnerText = identificacaoNfe.ide_tpEmis;
                        XmlNode xmlNfeNodeTpEmis = xmlNfeElement;
                        xmlNfeNodeIde.AppendChild(xmlNfeNodeTpEmis);
                        xmlNfe.AppendChild(xmlNfeNode);
                    }
                    else
                    {
                        return null;
                    }
                    //----------------------------------------------------------------- fim

                    // ---------------------------------------------------------------- inicio cDV -- B23 - f-B01
                    identificacaoNfe.ide_cDV = regrasNfe.preencheDigitoVerificadorChaveAcesso(chaveAcessoGeradaNfe);

                    xmlNfeElement = xmlNfe.CreateElement("cDV");
                    xmlNfeElement.InnerText = identificacaoNfe.ide_cDV;
                    XmlNode xmlNfeNodeCDv = xmlNfeElement;
                    xmlNfeNodeIde.AppendChild(xmlNfeNodeCDv);
                    xmlNfe.AppendChild(xmlNfeNode);
                    //----------------------------------------------------------------- fim

                    // ---------------------------------------------------------------- inicio tpAmb -- B24 - f-B01
                    if (regrasNfe.tipoAmbiente(identificacaoNfe.ide_tpAmb))
                    {
                        xmlNfeElement = xmlNfe.CreateElement("tpAmb");
                        xmlNfeElement.InnerText = identificacaoNfe.ide_tpAmb;
                        XmlNode xmlNfeNodeTpAmb = xmlNfeElement;
                        xmlNfeNodeIde.AppendChild(xmlNfeNodeTpAmb);
                        xmlNfe.AppendChild(xmlNfeNode);
                    }
                    else
                    {
                        return null;
                    }
                    //----------------------------------------------------------------- fim

                    // ---------------------------------------------------------------- inicio finNFe -- B25 - f-B01
                    if (regrasNfe.verifcaFinalidadeEmissao(identificacaoNfe.ide_finNFe))
                    {
                        if (regrasNfe.verificaFinalidadeEmissaoComplementar(identificacaoNfe.ide_finNFe))
                        {
                            if (!regrasNfe.verificaCnpjEmissaoComplementar(IdentificacaoEmitenteNfe.emit_CNPJ, documentoFiscalDiferenciado.ide_nFref_refNfe_refNF_CNPJ))
                                return null;
                        }
                        xmlNfeElement = xmlNfe.CreateElement("finNFe");
                        xmlNfeElement.InnerText = identificacaoNfe.ide_finNFe;
                        XmlNode xmlNfeNodeFinNFe = xmlNfeElement;
                        xmlNfeNodeIde.AppendChild(xmlNfeNodeFinNFe);
                        xmlNfe.AppendChild(xmlNfeNode);
                    }
                    else
                    {

                        return null;
                    }
                    //----------------------------------------------------------------- fim

                    // ---------------------------------------------------------------- inicio indFinal -- B25a - f-B01
                    xmlNfeElement = xmlNfe.CreateElement("indFinal");
                    xmlNfeElement.InnerText = identificacaoNfe.ide_indFinal;
                    XmlNode xmlNfeNodeIndFinal = xmlNfeElement;
                    xmlNfeNodeIde.AppendChild(xmlNfeNodeIndFinal);
                    xmlNfe.AppendChild(xmlNfeNode);
                    //----------------------------------------------------------------- fim

                    // ---------------------------------------------------------------- inicio indPres -- B25b - f-B01
                    xmlNfeElement = xmlNfe.CreateElement("indPres");
                    xmlNfeElement.InnerText = identificacaoNfe.ide_indPres;
                    XmlNode xmlNfeNodeIndPres = xmlNfeElement;
                    xmlNfeNodeIde.AppendChild(xmlNfeNodeIndPres);
                    xmlNfe.AppendChild(xmlNfeNode);
                    //----------------------------------------------------------------- fim

                    // ---------------------------------------------------------------- inicio procEmi -- B26 - f-B01
                    if (regrasNfe.verificaProcessoEmissao(identificacaoNfe.ide_procEmi))
                    {
                        xmlNfeElement = xmlNfe.CreateElement("procEmi");
                        xmlNfeElement.InnerText = identificacaoNfe.ide_procEmi;
                        XmlNode xmlNfeNodeProcEmi = xmlNfeElement;
                        xmlNfeNodeIde.AppendChild(xmlNfeNodeProcEmi);
                        xmlNfe.AppendChild(xmlNfeNode);
                    }
                    else
                    {

                        return null;
                    }
                    //----------------------------------------------------------------- fim

                    // ---------------------------------------------------------------- inicio verProc -- B27 - f-B01
                    xmlNfeElement = xmlNfe.CreateElement("verProc");
                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(identificacaoNfe.ide_verProc, 2, 1, 20, false, null, "B27");
                    XmlNode xmlNfeNodeVerProc = xmlNfeElement;
                    xmlNfeNodeIde.AppendChild(xmlNfeNodeVerProc);
                    xmlNfe.AppendChild(xmlNfeNode);
                    //----------------------------------------------------------------- fim

                    if (!regrasNfe.verificaGrupoIdentificacaoNFe(identificacaoNfe.ide_dhCont, identificacaoNfe.ide_xJust))
                    {
                        //// ---------------------------------------------------------------- inicio erproNFe -- B27.1 - B01
                        //xmlNfeElement = xmlNfe.CreateElement("erproNFe");
                        //XmlNode xmlNfeNodeXB27 = xmlNfeElement;
                        //xmlNfeNodeIde.AppendChild(xmlNfeNodeXB27);
                        //xmlNfe.AppendChild(xmlNfeNode);
                        ////----------------------------------------------------------------- fim

                        if (!identificacaoNfe.ide_tpEmis.Equals("1"))
                        {
                            // ---------------------------------------------------------------- inicio dhCont -- B28 - f-B01
                            xmlNfeElement = xmlNfe.CreateElement("dhCont");
                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(identificacaoNfe.ide_dhCont, 4, null, null, false, null, "B28");
                            XmlNode xmlNfeNodeDhCont = xmlNfeElement;
                            xmlNfeNodeIde.AppendChild(xmlNfeNodeDhCont);
                            xmlNfe.AppendChild(xmlNfeNode);
                            //----------------------------------------------------------------- fim

                            // ---------------------------------------------------------------- inicio xJust -- B29 - f-B01
                            xmlNfeElement = xmlNfe.CreateElement("xJust");
                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(identificacaoNfe.ide_xJust, 2, 15, 256, false, null, "B29");
                            XmlNode xmlNfeNodeXJust = xmlNfeElement;
                            xmlNfeNodeIde.AppendChild(xmlNfeNodeXJust);
                            xmlNfe.AppendChild(xmlNfeNode);
                            //----------------------------------------------------------------- fim
                        }
                    }
                    #endregion

                    #region #BA - Documento Fiscal Referenciado
                    contador = listaDocumentoFiscalDiferenciado.Count >= 500 ? 500 : listaDocumentoFiscalDiferenciado.Count;
                    for (int j = 0; j < contador; j++)
                    {
                        documentoFiscalDiferenciado = listaDocumentoFiscalDiferenciado[j];
                        if (regrasNfe.verificaTipoModelo55NFe(identificacaoNfe.ide_mod))
                        {
                            if (regrasNfe.verificaFinalidadeEmissaoComplementar(identificacaoNfe.ide_finNFe))
                            {
                                // ---------------------------------------------------------------- inicio NFref -- BA01 - f-B01
                                xmlNfeElement = xmlNfe.CreateElement("NFref");
                                XmlNode xmlNfeNodeNFref = xmlNfeElement;
                                xmlNfeNodeIde.AppendChild(xmlNfeNodeNFref);
                                xmlNfe.AppendChild(xmlNfeNode);
                                //----------------------------------------------------------------- fim

                                // ---------------------------------------------------------------- inicio efNFe -- BA02 - f-BA01
                                dadosChaveAcessoNfe chaveAcesso = new dadosChaveAcessoNfe(xmlNfeNodeCuf.InnerText, xmlNfeNodeDhEmi.InnerText,
                                    documentoFiscalDiferenciado.ide_nFref_refNfe_refNF_CNPJ, documentoFiscalDiferenciado.ide_nFref_refNfe_refNF_mod,
                                    documentoFiscalDiferenciado.ide_nFref_refNfe_refNF_serie, documentoFiscalDiferenciado.ide_nFref_refNfe_refNF_nNF,
                                    identificacaoNfe.ide_tpEmis, regrasNfe.geraCodigoNumerico(), null);

                                documentoFiscalDiferenciado.ide_nFref_refNFe = regrasNfe.geraChaveAcesso(chaveAcesso);

                                xmlNfeElement = xmlNfe.CreateElement("refNFe");
                                xmlNfeElement.InnerText = documentoFiscalDiferenciado.ide_nFref_refNFe;
                                XmlNode xmlNfeNodeNFrefRefNFe = xmlNfeElement;
                                xmlNfeNodeNFref.AppendChild(xmlNfeNodeNFrefRefNFe);
                                xmlNfe.AppendChild(xmlNfeNode);
                                //----------------------------------------------------------------- fim

                                if (regrasNfe.verificaTipoModelo(documentoFiscalDiferenciado.ide_nFref_refNfe_refNF_cUF, documentoFiscalDiferenciado.ide_nFref_refNfe_refNF_CNPJ))
                                {
                                    // ---------------------------------------------------------------- inicio efNFe -- BA03 - f-BA01
                                    xmlNfeElement = xmlNfe.CreateElement("refNF");
                                    XmlNode xmlNfeNodeNFrefrefNF = xmlNfeElement;
                                    xmlNfeNodeNFref.AppendChild(xmlNfeNodeNFrefrefNF);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio cUF -- BA04 - f-BA03
                                    xmlNfeElement = xmlNfe.CreateElement("cUF");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(documentoFiscalDiferenciado.ide_nFref_refNfe_refNF_cUF, 2, 2, 2, false, null, "BA04");
                                    XmlNode xmlNfeNodeNFrefRefNFcUF = xmlNfeElement;
                                    xmlNfeNodeNFrefrefNF.AppendChild(xmlNfeNodeNFrefRefNFcUF);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio AAMM -- BA05 - f-BA03
                                    documentoFiscalDiferenciado.ide_nFref_refNfe_refNF_AAMM = regrasNfe.validaAnoMes(identificacaoNfe.ide_dhEmi);
                                    xmlNfeElement = xmlNfe.CreateElement("AAMM");
                                    xmlNfeElement.InnerText = documentoFiscalDiferenciado.ide_nFref_refNfe_refNF_AAMM;
                                    XmlNode xmlNfeNodeNFrefRefNFAAMM = xmlNfeElement;
                                    xmlNfeNodeNFrefrefNF.AppendChild(xmlNfeNodeNFrefRefNFAAMM);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio CNPJ -- BA06 - f-BA03
                                    xmlNfeElement = xmlNfe.CreateElement("CNPJ");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(documentoFiscalDiferenciado.ide_nFref_refNfe_refNF_CNPJ, 2, 14, 14, false, null, "BA06");
                                    XmlNode xmlNfeNodeNFrefRefNFCNPJ = xmlNfeElement;
                                    xmlNfeNodeNFrefrefNF.AppendChild(xmlNfeNodeNFrefRefNFCNPJ);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio mod -- BA07 - f-BA03
                                    xmlNfeElement = xmlNfe.CreateElement("mod");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(documentoFiscalDiferenciado.ide_nFref_refNfe_refNF_mod, 2, 2, 2, false, null, "BA07");
                                    XmlNode xmlNfeNodeNFrefRefNFmod = xmlNfeElement;
                                    xmlNfeNodeNFrefrefNF.AppendChild(xmlNfeNodeNFrefRefNFmod);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio serie -- BA08 - f-BA03
                                    documentoFiscalDiferenciado.ide_nFref_refNfe_refNF_serie = regrasNfe.validaSerie(documentoFiscalDiferenciado.ide_nFref_refNfe_refNF_serie);
                                    xmlNfeElement = xmlNfe.CreateElement("serie");
                                    xmlNfeElement.InnerText = documentoFiscalDiferenciado.ide_nFref_refNfe_refNF_serie;
                                    XmlNode xmlNfeNodeNFrefRefNFserie = xmlNfeElement;
                                    xmlNfeNodeNFrefrefNF.AppendChild(xmlNfeNodeNFrefRefNFserie);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio nNF -- BA09 - f-BA03
                                    xmlNfeElement = xmlNfe.CreateElement("nNF");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(documentoFiscalDiferenciado.ide_nFref_refNfe_refNF_nNF, 2, 1, 9, false, null, "BA09");
                                    XmlNode xmlNfeNodeNFrefRefNFnNF = xmlNfeElement;
                                    xmlNfeNodeNFrefrefNF.AppendChild(xmlNfeNodeNFrefRefNFnNF);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim
                                }
                                if (regrasNfe.verificaTipoModelo(documentoFiscalDiferenciado.ide_nFref_refNfe_refNFP_cUF, documentoFiscalDiferenciado.ide_nFref_refNfe_refNFP_CNPJ))
                                {
                                    // ---------------------------------------------------------------- inicio refNFP -- BA10 - f-BA01
                                    xmlNfeElement = xmlNfe.CreateElement("refNFP");
                                    XmlNode xmlNfeNodeNFrefRefNFP = xmlNfeElement;
                                    xmlNfeNodeNFref.AppendChild(xmlNfeNodeNFrefRefNFP);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio cUF -- BA11 - f-BA10
                                    xmlNfeElement = xmlNfe.CreateElement("cUF");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(documentoFiscalDiferenciado.ide_nFref_refNfe_refNFP_cUF, 2, 2, 2, false, null, "BA11");
                                    XmlNode xmlNfeNodeNFrefRefNFPcUF = xmlNfeElement;
                                    xmlNfeNodeNFrefRefNFP.AppendChild(xmlNfeNodeNFrefRefNFPcUF);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio AAMM -- BA12 - f-BA10
                                    documentoFiscalDiferenciado.ide_nFref_refNfe_refNFP_AAMM = regrasNfe.validaAnoMes(identificacaoNfe.ide_dhEmi);
                                    xmlNfeElement = xmlNfe.CreateElement("AAMM");
                                    xmlNfeElement.InnerText = documentoFiscalDiferenciado.ide_nFref_refNfe_refNFP_AAMM;
                                    XmlNode xmlNfeNodeNFrefRefNFPAAMM = xmlNfeElement;
                                    xmlNfeNodeNFrefRefNFP.AppendChild(xmlNfeNodeNFrefRefNFPAAMM);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio CNPJ -- BA13 - f-BA10
                                    documentoFiscalDiferenciado.ide_nFref_refNfe_refNFP_CNPJ = regrasNfe.verificaTipoPessoaCNPJ(documentoFiscalDiferenciado.ide_nFref_refNfe_refNFP_CNPJ, documentoFiscalDiferenciado.ide_nFref_refNfe_refNFP_CPF);
                                    xmlNfeElement = xmlNfe.CreateElement("CNPJ");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(documentoFiscalDiferenciado.ide_nFref_refNfe_refNFP_CNPJ, 2, 14, 14, false, null, "BA13");
                                    XmlNode xmlNfeNodeNFrefRefNFPCNPJ = xmlNfeElement;
                                    xmlNfeNodeNFrefRefNFP.AppendChild(xmlNfeNodeNFrefRefNFPCNPJ);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio CPF -- BA14 - f-BA10
                                    documentoFiscalDiferenciado.ide_nFref_refNfe_refNFP_CPF = regrasNfe.verificaTipoPessoaCPF(documentoFiscalDiferenciado.ide_nFref_refNfe_refNFP_CPF, documentoFiscalDiferenciado.ide_nFref_refNfe_refNFP_CNPJ, null);
                                    xmlNfeElement = xmlNfe.CreateElement("CPF");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(documentoFiscalDiferenciado.ide_nFref_refNfe_refNFP_CPF, 2, 11, 11, false, null, "BA14");
                                    XmlNode xmlNfeNodeNFrefRefNFPCPF = xmlNfeElement;
                                    xmlNfeNodeNFrefRefNFP.AppendChild(xmlNfeNodeNFrefRefNFPCPF);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio CPF -- BA15 - f-BA10
                                    xmlNfeElement = xmlNfe.CreateElement("IE");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(documentoFiscalDiferenciado.ide_nFref_refNfe_refNFP_IE, 2, 2, 14, false, null, "BA15");
                                    XmlNode xmlNfeNodeNFrefRefNFPIE = xmlNfeElement;
                                    xmlNfeNodeNFrefRefNFP.AppendChild(xmlNfeNodeNFrefRefNFPIE);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio mod -- BA16 - f-BA10
                                    if (regrasNfe.verificaModeloProdutoRural(documentoFiscalDiferenciado.ide_nFref_refNfe_refNFP_mod))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("mod");
                                        xmlNfeElement.InnerText = documentoFiscalDiferenciado.ide_nFref_refNfe_refNFP_mod;
                                        XmlNode xmlNfeNodeNFrefRefNFPmod = xmlNfeElement;
                                        xmlNfeNodeNFrefRefNFP.AppendChild(xmlNfeNodeNFrefRefNFPmod);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    else
                                    {

                                        return null;
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio serie -- BA17 - f-BA10
                                    documentoFiscalDiferenciado.ide_nFref_refNfe_refNFP_serie = regrasNfe.validaSerie(documentoFiscalDiferenciado.ide_nFref_refNfe_refNFP_serie);
                                    xmlNfeElement = xmlNfe.CreateElement("serie");
                                    xmlNfeElement.InnerText = documentoFiscalDiferenciado.ide_nFref_refNfe_refNFP_serie;
                                    XmlNode xmlNfeNodeNFrefRefNFPserie = xmlNfeElement;
                                    xmlNfeNodeNFrefRefNFP.AppendChild(xmlNfeNodeNFrefRefNFPserie);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio nNF -- BA18 - f-BA10
                                    xmlNfeElement = xmlNfe.CreateElement("nNF");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(documentoFiscalDiferenciado.ide_nFref_refNfe_refNFP_nNF, 2, 1, 6, false, null, "BA18");
                                    XmlNode xmlNfeNodeNFrefRefNFPnNF = xmlNfeElement;
                                    xmlNfeNodeNFrefRefNFP.AppendChild(xmlNfeNodeNFrefRefNFPnNF);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim
                                }

                                // ---------------------------------------------------------------- inicio refCTe -- BA19 - f-BA01
                                documentoFiscalDiferenciado.ide_nFref_refNfe_refNFP_refCTe = chaveAcessoGeradaNfe;
                                xmlNfeElement = xmlNfe.CreateElement("refCTe");
                                xmlNfeElement.InnerText = documentoFiscalDiferenciado.ide_nFref_refNfe_refNFP_refCTe;
                                XmlNode xmlNfeNodeNFrefRefCTe = xmlNfeElement;
                                xmlNfeNodeNFref.AppendChild(xmlNfeNodeNFrefRefCTe);
                                xmlNfe.AppendChild(xmlNfeNode);
                                //----------------------------------------------------------------- 

                                // ---------------------------------------------------------------- inicio refECF -- BA20 - f-BA01
                                xmlNfeElement = xmlNfe.CreateElement("refECF");
                                XmlNode xmlNfeNodeNFrefRefECF = xmlNfeElement;
                                xmlNfeNodeNFref.AppendChild(xmlNfeNodeNFrefRefECF);
                                xmlNfe.AppendChild(xmlNfeNode);
                                //----------------------------------------------------------------- fim

                                // ---------------------------------------------------------------- inicio mod -- BA21 - f-BA20
                                if (regrasNfe.verificaModeloDocumentoFiscalReferenciado(documentoFiscalDiferenciado.ide_nFref_refNfe_refECF_mod))
                                {
                                    xmlNfeElement = xmlNfe.CreateElement("mod");
                                    xmlNfeElement.InnerText = documentoFiscalDiferenciado.ide_nFref_refNfe_refECF_mod;
                                    XmlNode xmlNfeNodeNFrefRefECFmod = xmlNfeElement;
                                    xmlNfeNodeNFrefRefECF.AppendChild(xmlNfeNodeNFrefRefECFmod);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                }
                                else
                                {

                                    return null;
                                }
                                //----------------------------------------------------------------- 

                                // ---------------------------------------------------------------- inicio nECF -- BA22 - f-BA20
                                xmlNfeElement = xmlNfe.CreateElement("nECF");
                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(documentoFiscalDiferenciado.ide_nFref_refNfe_refECF_nECF, 2, 3, 3, true, null, "BA22");
                                XmlNode xmlNfeNodeNFrefRefECFnECF = xmlNfeElement;
                                xmlNfeNodeNFrefRefECF.AppendChild(xmlNfeNodeNFrefRefECFnECF);
                                xmlNfe.AppendChild(xmlNfeNode);
                                //----------------------------------------------------------------- fim

                                // ---------------------------------------------------------------- inicio nCOO -- BA23 - f-BA20
                                xmlNfeElement = xmlNfe.CreateElement("nCOO");
                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(documentoFiscalDiferenciado.ide_nFref_refNfe_refECF_nCOO, 2, 3, 3, true, null, "BA23");
                                XmlNode xmlNfeNodeNFrefRefECFnCOO = xmlNfeElement;
                                xmlNfeNodeNFrefRefECF.AppendChild(xmlNfeNodeNFrefRefECFnCOO);
                                xmlNfe.AppendChild(xmlNfeNode);
                                //----------------------------------------------------------------- fim
                            }
                        }
                    }
                    #endregion

                    #region #C - Identificação do Emitente da Nota Fiscal eletrônica
                    IdentificacaoEmitenteNfe = listaIdentificacaoEmitenteNfe[i];
                    // ---------------------------------------------------------------- inicio emit -- C01 - A01
                    xmlNfeElement = xmlNfe.CreateElement("emit");
                    XmlNode xmlNfeNodeEmit = xmlNfeElement;
                    xmlNfeNodeInfNfe.AppendChild(xmlNfeNodeEmit);
                    xmlNfe.AppendChild(xmlNfeNode);
                    //----------------------------------------------------------------- fim

                    // ---------------------------------------------------------------- inicio CNPJ -- C02 - f-C01
                    if (regrasNfe.verificaCampoVazio(IdentificacaoEmitenteNfe.emit_CNPJ))
                    {
                        xmlNfeElement = xmlNfe.CreateElement("CNPJ");
                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoEmitenteNfe.emit_CNPJ, 2, 14, 14, false, null, "C02");
                        XmlNode xmlNfeNodeEmitCNPJ = xmlNfeElement;
                        xmlNfeNodeEmit.AppendChild(xmlNfeNodeEmitCNPJ);
                        xmlNfe.AppendChild(xmlNfeNode);
                    }
                    //----------------------------------------------------------------- fim

                    // ---------------------------------------------------------------- inicio CPF -- C02a - f-C01
                    if (regrasNfe.verificaCampoVazio(IdentificacaoEmitenteNfe.emit_CPF))
                    {
                        xmlNfeElement = xmlNfe.CreateElement("CPF");
                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoEmitenteNfe.emit_CPF, 2, 11, 11, false, null, "C02a");
                        XmlNode xmlNfeNodeEmitCPF = xmlNfeElement;
                        xmlNfeNodeEmit.AppendChild(xmlNfeNodeEmitCPF);
                        xmlNfe.AppendChild(xmlNfeNode);
                    }
                    //----------------------------------------------------------------- fim

                    // ---------------------------------------------------------------- inicio xNome -- C03 - f-C01
                    xmlNfeElement = xmlNfe.CreateElement("xNome");
                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoEmitenteNfe.emit_xNome, 2, 2, 60, false, null, "C03");
                    XmlNode xmlNfeNodeEmitXNome = xmlNfeElement;
                    xmlNfeNodeEmit.AppendChild(xmlNfeNodeEmitXNome);
                    xmlNfe.AppendChild(xmlNfeNode);
                    //----------------------------------------------------------------- fim

                    // ---------------------------------------------------------------- inicio xFant -- C04 - f-C01
                    if (regrasNfe.verificaCampoVazio(IdentificacaoEmitenteNfe.emit_xFant))
                    {
                        xmlNfeElement = xmlNfe.CreateElement("xFant");
                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoEmitenteNfe.emit_xFant, 2, 1, 60, false, null, "C04");
                        XmlNode xmlNfeNodeEmitXFant = xmlNfeElement;
                        xmlNfeNodeEmit.AppendChild(xmlNfeNodeEmitXFant);
                        xmlNfe.AppendChild(xmlNfeNode);
                    }
                    //----------------------------------------------------------------- fim

                    // ---------------------------------------------------------------- inicio enderEmit -- C05 - f-C01
                    xmlNfeElement = xmlNfe.CreateElement("enderEmit");
                    XmlNode xmlNfeNodeEmitEnderEmit = xmlNfeElement;
                    xmlNfeNodeEmit.AppendChild(xmlNfeNodeEmitEnderEmit);
                    xmlNfe.AppendChild(xmlNfeNode);
                    //----------------------------------------------------------------- fim

                    // ---------------------------------------------------------------- inicio xLgr -- C06 - f-C05
                    xmlNfeElement = xmlNfe.CreateElement("xLgr");
                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoEmitenteNfe.emit_enderEmi_xLgr, 2, 1, 60, false, null, "C06");
                    XmlNode xmlNfeNodeEmitXLgr = xmlNfeElement;
                    xmlNfeNodeEmitEnderEmit.AppendChild(xmlNfeNodeEmitXLgr);
                    xmlNfe.AppendChild(xmlNfeNode);
                    //----------------------------------------------------------------- 

                    // ---------------------------------------------------------------- inicio nro -- C07 - f-C05
                    xmlNfeElement = xmlNfe.CreateElement("nro");
                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoEmitenteNfe.emit_enderEmi_nro, 2, 1, 60, false, null, "C07");
                    XmlNode xmlNfeNodeEmitNro = xmlNfeElement;
                    xmlNfeNodeEmitEnderEmit.AppendChild(xmlNfeNodeEmitNro);
                    xmlNfe.AppendChild(xmlNfeNode);
                    //----------------------------------------------------------------- fim

                    // ---------------------------------------------------------------- inicio xCpl -- C08 - f-C05
                    if (regrasNfe.verificaCampoVazio(IdentificacaoEmitenteNfe.emit_enderEmi_xCpl))
                    {
                        xmlNfeElement = xmlNfe.CreateElement("xCpl");
                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoEmitenteNfe.emit_enderEmi_xCpl, 2, 1, 60, false, null, "C08");
                        XmlNode xmlNfeNodeEmitXCpl = xmlNfeElement;
                        xmlNfeNodeEmitEnderEmit.AppendChild(xmlNfeNodeEmitXCpl);
                        xmlNfe.AppendChild(xmlNfeNode);
                    }
                    //----------------------------------------------------------------- fim

                    // ---------------------------------------------------------------- inicio xBairro -- C09 - f-C05
                    xmlNfeElement = xmlNfe.CreateElement("xBairro");
                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoEmitenteNfe.emit_enderEmi_xBairro, 2, 2, 60, false, null, "C09");
                    XmlNode xmlNfeNodeEmitXBairro = xmlNfeElement;
                    xmlNfeNodeEmitEnderEmit.AppendChild(xmlNfeNodeEmitXBairro);
                    xmlNfe.AppendChild(xmlNfeNode);
                    //----------------------------------------------------------------- fim

                    // ---------------------------------------------------------------- inicio cMun -- C10 - f-C05
                    xmlNfeElement = xmlNfe.CreateElement("cMun");
                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoEmitenteNfe.emit_enderEmi_cMun, 2, 7, 7, true, null, "C10");
                    XmlNode xmlNfeNodeEmitCMun = xmlNfeElement;
                    xmlNfeNodeEmitEnderEmit.AppendChild(xmlNfeNodeEmitCMun);
                    xmlNfe.AppendChild(xmlNfeNode);
                    //----------------------------------------------------------------- fim

                    // ---------------------------------------------------------------- inicio cMun -- C11 - f-C05
                    xmlNfeElement = xmlNfe.CreateElement("xMun");
                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoEmitenteNfe.emit_enderEmi_xMun, 2, 2, 60, false, null, "C11");
                    XmlNode xmlNfeNodeEmitXMun = xmlNfeElement;
                    xmlNfeNodeEmitEnderEmit.AppendChild(xmlNfeNodeEmitXMun);
                    xmlNfe.AppendChild(xmlNfeNode);
                    //----------------------------------------------------------------- fim

                    // ---------------------------------------------------------------- inicio UF -- C12 - f-C05
                    xmlNfeElement = xmlNfe.CreateElement("UF");
                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoEmitenteNfe.emit_enderEmi_UF, 2, 2, 2, false, null, "C12");
                    XmlNode xmlNfeNodeEmitUF = xmlNfeElement;
                    xmlNfeNodeEmitEnderEmit.AppendChild(xmlNfeNodeEmitUF);
                    xmlNfe.AppendChild(xmlNfeNode);
                    //----------------------------------------------------------------- fim

                    // ---------------------------------------------------------------- inicio CEP -- C13 - f-C05
                    IdentificacaoEmitenteNfe.emit_enderEmi_CEP = regrasNfe.validaCep(IdentificacaoEmitenteNfe.emit_enderEmi_CEP);
                    xmlNfeElement = xmlNfe.CreateElement("CEP");
                    xmlNfeElement.InnerText = IdentificacaoEmitenteNfe.emit_enderEmi_CEP;
                    XmlNode xmlNfeNodeEmitCEP = xmlNfeElement;
                    xmlNfeNodeEmitEnderEmit.AppendChild(xmlNfeNodeEmitCEP);
                    xmlNfe.AppendChild(xmlNfeNode);
                    //----------------------------------------------------------------- fim

                    // ---------------------------------------------------------------- inicio cPais -- C14 - f-C05
                    xmlNfeElement = xmlNfe.CreateElement("cPais");
                    xmlNfeElement.InnerText = "1058";
                    XmlNode xmlNfeNodeEmitCPais = xmlNfeElement;
                    xmlNfeNodeEmitEnderEmit.AppendChild(xmlNfeNodeEmitCPais);
                    xmlNfe.AppendChild(xmlNfeNode);
                    //----------------------------------------------------------------- fim

                    // ---------------------------------------------------------------- inicio xPais -- C15 - f-C05
                    xmlNfeElement = xmlNfe.CreateElement("xPais");
                    xmlNfeElement.InnerText = "Brasil";
                    XmlNode xmlNfeNodeEmitXPais = xmlNfeElement;
                    xmlNfeNodeEmitEnderEmit.AppendChild(xmlNfeNodeEmitXPais);
                    xmlNfe.AppendChild(xmlNfeNode);
                    //----------------------------------------------------------------- fim

                    // ---------------------------------------------------------------- inicio fone -- C16 - f-C05
                    if (regrasNfe.verificaCampoVazio(IdentificacaoEmitenteNfe.emit_enderEmi_fone))
                    {
                        xmlNfeElement = xmlNfe.CreateElement("fone");
                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoEmitenteNfe.emit_enderEmi_fone, 2, 6, 14, false, null, "C16");
                        XmlNode xmlNfeNodeEmitFone = xmlNfeElement;
                        xmlNfeNodeEmitEnderEmit.AppendChild(xmlNfeNodeEmitFone);
                        xmlNfe.AppendChild(xmlNfeNode);
                    }
                    //----------------------------------------------------------------- fim

                    // ---------------------------------------------------------------- inicio IE -- C17 - f-C01
                    xmlNfeElement = xmlNfe.CreateElement("IE");
                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoEmitenteNfe.emit_enderEmi_IE, 2, 2, 14, false, null, "C17");
                    XmlNode xmlNfeNodeIE = xmlNfeElement;
                    xmlNfeNodeEmit.AppendChild(xmlNfeNodeIE);
                    xmlNfe.AppendChild(xmlNfeNode);
                    //----------------------------------------------------------------- fim

                    // ---------------------------------------------------------------- inicio IEST -- C18 - f-C01
                    if (regrasNfe.verificaTipoModelo55NFe(identificacaoNfe.ide_mod))
                    {
                        if (regrasNfe.verificaCampoVazio(IdentificacaoEmitenteNfe.emit_enderEmi_IEST))
                        {
                            xmlNfeElement = xmlNfe.CreateElement("IEST");
                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoEmitenteNfe.emit_enderEmi_IEST, 2, 2, 14, false, null, "C18");
                            XmlNode xmlNfeNodeIEST = xmlNfeElement;
                            xmlNfeNodeEmit.AppendChild(xmlNfeNodeIEST);
                            xmlNfe.AppendChild(xmlNfeNode);
                        }
                    }
                    //----------------------------------------------------------------- fim

                    if (!regrasNfe.verificaEnderecoEmitenteNFeConjulgada(IdentificacaoEmitenteNfe.emit_enderEmi_IM, IdentificacaoEmitenteNfe.emit_enderEmi_CRT))
                    {
                        //// ---------------------------------------------------------------- inicio IEST -- C18.1 - f-C01
                        //xmlNfeElement = xmlNfe.CreateElement("erproNFe");
                        //XmlNode xmlNfeNodeXC18 = xmlNfeElement;
                        //xmlNfeNodeEmit.AppendChild(xmlNfeNodeXC18);
                        //xmlNfe.AppendChild(xmlNfeNode);
                        ////----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio IM -- C19 - f-f-C01
                        xmlNfeElement = xmlNfe.CreateElement("IM");
                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoEmitenteNfe.emit_enderEmi_IM, 2, 1, 15, false, null, "C19");
                        XmlNode xmlNfeNodeIM = xmlNfeElement;
                        xmlNfeNodeEmit.AppendChild(xmlNfeNodeIM);
                        xmlNfe.AppendChild(xmlNfeNode);
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio CNAE -- C20 - f-f-C01
                        if (!regrasNfe.verificaIscricaoMunicipalInformada(IdentificacaoEmitenteNfe.emit_enderEmi_IM))
                        {
                            if (regrasNfe.verificaCnae(IdentificacaoEmitenteNfe.emit_enderEmi_CNAE))
                            {
                                xmlNfeElement = xmlNfe.CreateElement("CNAE");
                                xmlNfeElement.InnerText = IdentificacaoEmitenteNfe.emit_enderEmi_CNAE;
                                XmlNode xmlNfeNodeCNAE = xmlNfeElement;
                                xmlNfeNodeEmit.AppendChild(xmlNfeNodeCNAE);
                                xmlNfe.AppendChild(xmlNfeNode);
                            }
                        }
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio CRT -- C21 - f-f-C01
                        if (regrasNfe.verificaRegimeTributario(IdentificacaoEmitenteNfe.emit_enderEmi_CRT))
                        {
                            xmlNfeElement = xmlNfe.CreateElement("CRT");
                            xmlNfeElement.InnerText = IdentificacaoEmitenteNfe.emit_enderEmi_CRT;
                            XmlNode xmlNfeNodeCRT = xmlNfeElement;
                            xmlNfeNodeEmit.AppendChild(xmlNfeNodeCRT);
                            xmlNfe.AppendChild(xmlNfeNode);
                        }
                        else
                        {

                            return null;
                        }
                        //----------------------------------------------------------------- fim
                    }
                    #endregion

                    #region #D - Identificação do Fisco Emitente da NF-e
                    if (false)
                    {
                        // ---------------------------------------------------------------- inicio avulsa -- D01 - f-A01
                        xmlNfeElement = xmlNfe.CreateElement("avulsa");
                        XmlNode xmlNfeNodeAvulsa = xmlNfeElement;
                        xmlNfeNodeInfNfe.AppendChild(xmlNfeNodeAvulsa);
                        xmlNfe.AppendChild(xmlNfeNode);
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio CNPJ -- D02 - f-D01
                        xmlNfeElement = xmlNfe.CreateElement("CNPJ");
                        //xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoFiscoEmitenteNfe.avulsa_CNPJ,2,14,14,false, null);
                        XmlNode xmlNfeNodeAvulsaCNPJ = xmlNfeElement;
                        xmlNfeNodeAvulsa.AppendChild(xmlNfeNodeAvulsaCNPJ);
                        xmlNfe.AppendChild(xmlNfeNode);
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio xOrgao -- D03 - f-D01
                        xmlNfeElement = xmlNfe.CreateElement("xOrgao");
                        //xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoFiscoEmitenteNfe.avulsa_xOrgao,2,1,60,false,null);
                        XmlNode xmlNfeNodeAvulsaXOrgao = xmlNfeElement;
                        xmlNfeNodeAvulsa.AppendChild(xmlNfeNodeAvulsaXOrgao);
                        xmlNfe.AppendChild(xmlNfeNode);
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio matr -- D04 - f-D01
                        xmlNfeElement = xmlNfe.CreateElement("matr");
                        //xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoFiscoEmitenteNfe.avulsa_matr,2,1,60,false,null);
                        XmlNode xmlNfeNodeAvulsaMatr = xmlNfeElement;
                        xmlNfeNodeAvulsa.AppendChild(xmlNfeNodeAvulsaMatr);
                        xmlNfe.AppendChild(xmlNfeNode);
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio xAgente -- D05 - f-D01
                        xmlNfeElement = xmlNfe.CreateElement("xAgente");
                        //xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoFiscoEmitenteNfe.avulsa_xAgente,2,1,60,false,null);
                        XmlNode xmlNfeNodeAvulsaXAgente = xmlNfeElement;
                        xmlNfeNodeAvulsa.AppendChild(xmlNfeNodeAvulsaXAgente);
                        xmlNfe.AppendChild(xmlNfeNode);
                        //----------------------------------------------------------------- 

                        // ---------------------------------------------------------------- inicio fone -- D06 - f-D01
                        xmlNfeElement = xmlNfe.CreateElement("fone");
                        //xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoFiscoEmitenteNfe.avulsa_fone,2,6,14,false,null);
                        XmlNode xmlNfeNodeAvulsaFone = xmlNfeElement;
                        xmlNfeNodeAvulsa.AppendChild(xmlNfeNodeAvulsaFone);
                        xmlNfe.AppendChild(xmlNfeNode);
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio UF -- D07 - f-D01
                        xmlNfeElement = xmlNfe.CreateElement("UF");
                        //xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoFiscoEmitenteNfe.avulsa_UF,2,2,2,false,null);
                        XmlNode xmlNfeNodeAvulsaUF = xmlNfeElement;
                        xmlNfeNodeAvulsa.AppendChild(xmlNfeNodeAvulsaUF);
                        xmlNfe.AppendChild(xmlNfeNode);
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio nDAR -- D08 - f-D01
                        xmlNfeElement = xmlNfe.CreateElement("nDAR");
                        //xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoFiscoEmitenteNfe.avulsa_nDAR, 2, 1, 60, false, null);
                        XmlNode xmlNfeNodeAvulsaNDAR = xmlNfeElement;
                        xmlNfeNodeAvulsa.AppendChild(xmlNfeNodeAvulsaNDAR);
                        xmlNfe.AppendChild(xmlNfeNode);
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio dEmi -- D09 - f-D01
                        xmlNfeElement = xmlNfe.CreateElement("dEmi");
                        //xmlNfeElement.InnerText = IdentificacaoFiscoEmitenteNfe.avulsa_dEmi;
                        XmlNode xmlNfeNodeAvulsaDEmi = xmlNfeElement;
                        xmlNfeNodeAvulsa.AppendChild(xmlNfeNodeAvulsaDEmi);
                        xmlNfe.AppendChild(xmlNfeNode);
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio vDAR -- D10 - f-D01
                        xmlNfeElement = xmlNfe.CreateElement("vDAR");
                        //xmlNfeElement.InnerText = IdentificacaoFiscoEmitenteNfe.avulsa_vDAR;
                        XmlNode xmlNfeNodeAvulsaVDAR = xmlNfeElement;
                        xmlNfeNodeAvulsa.AppendChild(xmlNfeNodeAvulsaVDAR);
                        xmlNfe.AppendChild(xmlNfeNode);
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio repEmi -- D11 - f-D01
                        xmlNfeElement = xmlNfe.CreateElement("repEmi");
                        //xmlNfeElement.InnerText = IdentificacaoFiscoEmitenteNfe.avulsa_repEmi;
                        XmlNode xmlNfeNodeAvulsaRepEmi = xmlNfeElement;
                        xmlNfeNodeAvulsa.AppendChild(xmlNfeNodeAvulsaRepEmi);
                        xmlNfe.AppendChild(xmlNfeNode);
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio dPag -- D12 - f-D01
                        xmlNfeElement = xmlNfe.CreateElement("dPag");
                        //xmlNfeElement.InnerText = IdentificacaoFiscoEmitenteNfe.avulsa_dPag;
                        XmlNode xmlNfeNodeAvulsaDPag = xmlNfeElement;
                        xmlNfeNodeAvulsa.AppendChild(xmlNfeNodeAvulsaDPag);
                        xmlNfe.AppendChild(xmlNfeNode);
                        //----------------------------------------------------------------- fim
                    }
                    #endregion

                    #region #E - Identificação do Destinatário da Nota Fiscal eletrônica - NFe - Modelo 55
                    IdentificacaoDestinatarioNfe = listaIdentificacaoDestinatarioNfe[i];
                    if (regrasNfe.verificaTipoModelo55NFe(identificacaoNfe.ide_mod))
                    {
                        // ---------------------------------------------------------------- inicio dest -- E01 - f-A01
                        xmlNfeElement = xmlNfe.CreateElement("dest");
                        XmlNode xmlNfeNodeDest = xmlNfeElement;
                        xmlNfeNodeInfNfe.AppendChild(xmlNfeNodeDest);
                        xmlNfe.AppendChild(xmlNfeNode);
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio CNPJ -- E02 - f-E01
                        if (regrasNfe.verificaCampoVazio(IdentificacaoDestinatarioNfe.dest_CNPJ))
                        {
                            xmlNfeElement = xmlNfe.CreateElement("CNPJ");
                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoDestinatarioNfe.dest_CNPJ, 2, 14, 14, false, null, "E02");
                            XmlNode xmlNfeNodeDestCNPJ = xmlNfeElement;
                            xmlNfeNodeDest.AppendChild(xmlNfeNodeDestCNPJ);
                            xmlNfe.AppendChild(xmlNfeNode);
                        }
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio CPF -- E03 - f-E01
                        //IdentificacaoDestinatarioNfe.dest_CPF = regrasNfe.verificaTipoPessoaCPFDestinatario(IdentificacaoDestinatarioNfe.dest_CNPJ, IdentificacaoDestinatarioNfe.dest_CPF, IdentificacaoDestinatarioNfe.dest_enderDest_cPais);
                        if (regrasNfe.verificaCampoVazio(IdentificacaoDestinatarioNfe.dest_CPF))
                        {
                            xmlNfeElement = xmlNfe.CreateElement("CPF");
                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoDestinatarioNfe.dest_CPF, 2, 11, 11, false, null, "E03");
                            XmlNode xmlNfeNodeDestCPF = xmlNfeElement;
                            xmlNfeNodeDest.AppendChild(xmlNfeNodeDestCPF);
                            xmlNfe.AppendChild(xmlNfeNode);
                        }
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio idEstrangeiro -- E03a - f-E01
                        if (regrasNfe.verificaCampoVazio(IdentificacaoDestinatarioNfe.dest_idEstrangeiro))
                        {
                            if (regrasNfe.verificaIdEstrangeiro(IdentificacaoDestinatarioNfe.dest_idEstrangeiro, IdentificacaoDestinatarioNfe.dest_enderDest_cPais))
                            {
                                xmlNfeElement = xmlNfe.CreateElement("idEstrangeiro");
                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoDestinatarioNfe.dest_idEstrangeiro, 2, 5, 20, false, null, "E03a");
                                XmlNode xmlNfeNodeDestIdEstrangeiro = xmlNfeElement;
                                xmlNfeNodeDest.AppendChild(xmlNfeNodeDestIdEstrangeiro);
                                xmlNfe.AppendChild(xmlNfeNode);
                            }
                        }
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio xNome -- E04 - f-E01
                        if (regrasNfe.verificaNomeDestinatario(IdentificacaoDestinatarioNfe.dest_xNome, identificacaoNfe.ide_mod))
                        {
                            xmlNfeElement = xmlNfe.CreateElement("xNome");
                            xmlNfeElement.InnerText = IdentificacaoDestinatarioNfe.dest_xNome;
                            XmlNode xmlNfeNodeDestXNome = xmlNfeElement;
                            xmlNfeNodeDest.AppendChild(xmlNfeNodeDestXNome);
                            xmlNfe.AppendChild(xmlNfeNode);
                        }
                        else
                        {

                            return null;
                        }
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio enderDest -- E05 - f-E01
                        xmlNfeElement = xmlNfe.CreateElement("enderDest");
                        XmlNode xmlNfeNodedestEnderDest = xmlNfeElement;
                        xmlNfeNodeDest.AppendChild(xmlNfeNodedestEnderDest);
                        xmlNfe.AppendChild(xmlNfeNode);
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio xLgr -- E06 - f-E05
                        xmlNfeElement = xmlNfe.CreateElement("xLgr");
                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoDestinatarioNfe.dest_enderDest_xLgr, 2, 2, 60, false, null, "E06");
                        XmlNode xmlNfeNodeDestXLgr = xmlNfeElement;
                        xmlNfeNodedestEnderDest.AppendChild(xmlNfeNodeDestXLgr);
                        xmlNfe.AppendChild(xmlNfeNode);
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio nro -- E07 - f-E05
                        xmlNfeElement = xmlNfe.CreateElement("nro");
                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoDestinatarioNfe.dest_enderDest_nro, 2, 1, 60, false, null, "E07");
                        XmlNode xmlNfeNodeDestNro = xmlNfeElement;
                        xmlNfeNodedestEnderDest.AppendChild(xmlNfeNodeDestNro);
                        xmlNfe.AppendChild(xmlNfeNode);
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio xCpl -- E08 - f-E05
                        if (regrasNfe.verificaCampoVazio(IdentificacaoDestinatarioNfe.dest_enderDest_xCpl))
                        {
                            xmlNfeElement = xmlNfe.CreateElement("xCpl");
                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoDestinatarioNfe.dest_enderDest_xCpl, 2, 1, 60, false, null, "E08");
                            XmlNode xmlNfeNodeDestXCpl = xmlNfeElement;
                            xmlNfeNodedestEnderDest.AppendChild(xmlNfeNodeDestXCpl);
                            xmlNfe.AppendChild(xmlNfeNode);
                        }
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio xBairro -- E09 - f-E05
                        xmlNfeElement = xmlNfe.CreateElement("xBairro");
                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoDestinatarioNfe.dest_enderDest_xBairro, 2, 2, 60, false, null, "E09");
                        XmlNode xmlNfeNodeDestXBairro = xmlNfeElement;
                        xmlNfeNodedestEnderDest.AppendChild(xmlNfeNodeDestXBairro);
                        xmlNfe.AppendChild(xmlNfeNode);
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio cMun -- E10 - f-E05
                        xmlNfeElement = xmlNfe.CreateElement("cMun");
                        //xmlNfeElement.InnerText = regrasNfe.validaCodigoCidadeExterior(IdentificacaoDestinatarioNfe.dest_enderDest_cMun, IdentificacaoDestinatarioNfe.dest_enderDest_cMun);
                        xmlNfeElement.InnerText = regrasNfe.validaCodigoCidadeExterior(IdentificacaoDestinatarioNfe.dest_enderDest_cPais, IdentificacaoDestinatarioNfe.dest_enderDest_cMun);
                        XmlNode xmlNfeNodeDestCMun = xmlNfeElement;
                        xmlNfeNodedestEnderDest.AppendChild(xmlNfeNodeDestCMun);
                        xmlNfe.AppendChild(xmlNfeNode);
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio xMun -- E11 - f-E05
                        xmlNfeElement = xmlNfe.CreateElement("xMun");
                        //xmlNfeElement.InnerText = regrasNfe.validaNomeCidadeExterior(IdentificacaoDestinatarioNfe.dest_enderDest_xMun, IdentificacaoDestinatarioNfe.dest_enderDest_xMun);
                        xmlNfeElement.InnerText = regrasNfe.validaNomeCidadeExterior(IdentificacaoDestinatarioNfe.dest_enderDest_cPais, IdentificacaoDestinatarioNfe.dest_enderDest_xMun);
                        XmlNode xmlNfeNodeDestXMun = xmlNfeElement;
                        xmlNfeNodedestEnderDest.AppendChild(xmlNfeNodeDestXMun);
                        xmlNfe.AppendChild(xmlNfeNode);
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio UF -- E12 - f-E05
                        xmlNfeElement = xmlNfe.CreateElement("UF");
                        //xmlNfeElement.InnerText = regrasNfe.validaUfExterior(IdentificacaoDestinatarioNfe.dest_enderDest_UF, IdentificacaoDestinatarioNfe.dest_enderDest_UF);
                        xmlNfeElement.InnerText = regrasNfe.validaUfExterior(IdentificacaoDestinatarioNfe.dest_enderDest_cPais, IdentificacaoDestinatarioNfe.dest_enderDest_UF);
                        XmlNode xmlNfeNodeDestUF = xmlNfeElement;
                        xmlNfeNodedestEnderDest.AppendChild(xmlNfeNodeDestUF);
                        xmlNfe.AppendChild(xmlNfeNode);
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio CEP -- E13 - f-E05
                        if (regrasNfe.verificaCampoVazio(IdentificacaoDestinatarioNfe.dest_enderDest_CEP))
                        {
                            xmlNfeElement = xmlNfe.CreateElement("CEP");
                            xmlNfeElement.InnerText = regrasNfe.validaCep(IdentificacaoDestinatarioNfe.dest_enderDest_CEP);
                            XmlNode xmlNfeNodeDestCEP = xmlNfeElement;
                            xmlNfeNodedestEnderDest.AppendChild(xmlNfeNodeDestCEP);
                            xmlNfe.AppendChild(xmlNfeNode);
                        }
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio cPais -- E14 - f-E05
                        if (regrasNfe.verificaCampoVazio(IdentificacaoDestinatarioNfe.dest_enderDest_cPais))
                        {
                            xmlNfeElement = xmlNfe.CreateElement("cPais");
                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoDestinatarioNfe.dest_enderDest_cPais, 2, 2, 4, false, null, "E14");
                            XmlNode xmlNfeNodeDestCPais = xmlNfeElement;
                            xmlNfeNodedestEnderDest.AppendChild(xmlNfeNodeDestCPais);
                            xmlNfe.AppendChild(xmlNfeNode);
                        }
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio xPais -- E15 - f-E05
                        if (regrasNfe.verificaCampoVazio(IdentificacaoDestinatarioNfe.dest_enderDest_xPais))
                        {
                            xmlNfeElement = xmlNfe.CreateElement("xPais");
                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoDestinatarioNfe.dest_enderDest_xPais, 2, 2, 60, false, null, "E15");
                            XmlNode xmlNfeNodeDestXPais = xmlNfeElement;
                            xmlNfeNodedestEnderDest.AppendChild(xmlNfeNodeDestXPais);
                            xmlNfe.AppendChild(xmlNfeNode);
                        }
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio fone -- E16 - f-E05
                        if (regrasNfe.verificaCampoVazio(IdentificacaoDestinatarioNfe.dest_enderDest_fone))
                        {
                            xmlNfeElement = xmlNfe.CreateElement("fone");
                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoDestinatarioNfe.dest_enderDest_fone, 2, 6, 14, false, null, "E16");
                            XmlNode xmlNfeNodeDestFone = xmlNfeElement;
                            xmlNfeNodedestEnderDest.AppendChild(xmlNfeNodeDestFone);
                            xmlNfe.AppendChild(xmlNfeNode);
                        }
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio indIEDest -- E16a - f-E01
                        IdentificacaoDestinatarioNfe.dest_enderDest_indIEDest = regrasNfe.validaIndicadorIeDestinatarioExterior(IdentificacaoDestinatarioNfe.dest_enderDest_indIEDest, IdentificacaoDestinatarioNfe.dest_enderDest_cPais);
                        if (regrasNfe.verificaIndicadorIeDestinatario(IdentificacaoDestinatarioNfe.dest_enderDest_indIEDest))
                        {
                            xmlNfeElement = xmlNfe.CreateElement("indIEDest");
                            xmlNfeElement.InnerText = IdentificacaoDestinatarioNfe.dest_enderDest_indIEDest;
                            XmlNode xmlNfeNodeDestIndIEDest = xmlNfeElement;
                            xmlNfeNodeDest.AppendChild(xmlNfeNodeDestIndIEDest);
                            xmlNfe.AppendChild(xmlNfeNode);
                        }
                        else
                        {

                            return null;
                        }
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio IE -- E17 - f-E01
                        if (regrasNfe.verificaTipoIndicadorDestinatoarioIE(IdentificacaoDestinatarioNfe.dest_enderDest_indIEDest))
                        {
                            if (regrasNfe.verificaCampoVazio(IdentificacaoDestinatarioNfe.dest_enderDest_IE))
                            {
                                xmlNfeElement = xmlNfe.CreateElement("IE");
                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoDestinatarioNfe.dest_enderDest_IE, 2, 2, 14, false, null, "E17");
                                XmlNode xmlNfeNodeDestIE = xmlNfeElement;
                                xmlNfeNodeDest.AppendChild(xmlNfeNodeDestIE);
                                xmlNfe.AppendChild(xmlNfeNode);
                            }
                        }
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio ISUF -- E18 - f-E01
                        if (regrasNfe.verificaCampoVazio(IdentificacaoDestinatarioNfe.dest_enderDest_ISUF))
                        {
                            xmlNfeElement = xmlNfe.CreateElement("ISUF");
                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoDestinatarioNfe.dest_enderDest_ISUF, 2, 8, 9, false, null, "E18");
                            XmlNode xmlNfeNodeDestISUF = xmlNfeElement;
                            xmlNfeNodeDest.AppendChild(xmlNfeNodeDestISUF);
                            xmlNfe.AppendChild(xmlNfeNode);
                        }
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio ISUF -- E18a - f-E01
                        if (regrasNfe.verificaCampoVazio(IdentificacaoDestinatarioNfe.dest_enderDest_IM))
                        {
                            xmlNfeElement = xmlNfe.CreateElement("IM");
                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoDestinatarioNfe.dest_enderDest_IM, 2, 1, 15, false, null, "E18a");
                            XmlNode xmlNfeNodeDestIM = xmlNfeElement;
                            xmlNfeNodeDest.AppendChild(xmlNfeNodeDestIM);
                            xmlNfe.AppendChild(xmlNfeNode);
                        }
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio email -- E19 - f-E01
                        if (regrasNfe.verificaCampoVazio(IdentificacaoDestinatarioNfe.dest_enderDest_email))
                        {
                            xmlNfeElement = xmlNfe.CreateElement("email");
                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoDestinatarioNfe.dest_enderDest_email, 2, 1, 60, false, null, "E19");
                            XmlNode xmlNfeNodeDestEmail = xmlNfeElement;
                            xmlNfeNodeDest.AppendChild(xmlNfeNodeDestEmail);
                            xmlNfe.AppendChild(xmlNfeNode);
                        }
                    }
                    //----------------------------------------------------------------- fim
                    #endregion

                    #region #E - Identificação do Destinatário da Nota Fiscal eletrônica - NFe - Modelo 65

                    if (!regrasNfe.verificaTipoModelo55NFe(identificacaoNfe.ide_mod))
                    {
                        if (!regrasNfe.verrificaPreenchimentoIdentificacaoDestinatarioNFe(IdentificacaoDestinatarioNfe.dest_CNPJ, IdentificacaoDestinatarioNfe.dest_CPF, IdentificacaoDestinatarioNfe.dest_idEstrangeiro))
                        {
                            IdentificacaoDestinatarioNfe = listaIdentificacaoDestinatarioNfe[i];
                            // ---------------------------------------------------------------- inicio dest -- E01 - f-A01
                            xmlNfeElement = xmlNfe.CreateElement("dest");
                            XmlNode xmlNfeNodeDest = xmlNfeElement;
                            xmlNfeNodeInfNfe.AppendChild(xmlNfeNodeDest);
                            xmlNfe.AppendChild(xmlNfeNode);
                            //----------------------------------------------------------------- fim

                            // ---------------------------------------------------------------- inicio CNPJ -- E02 - f-E01
                            //IdentificacaoDestinatarioNfe.dest_CNPJ = regrasNfe.verificaTipoPessoaCNPJDestinatario(IdentificacaoDestinatarioNfe.dest_CNPJ, IdentificacaoDestinatarioNfe.dest_CPF, IdentificacaoDestinatarioNfe.dest_enderDest_cPais);

                            if (regrasNfe.verificaCampoVazio(IdentificacaoDestinatarioNfe.dest_CNPJ))
                            {
                                xmlNfeElement = xmlNfe.CreateElement("CNPJ");
                                xmlNfeElement.InnerText = IdentificacaoDestinatarioNfe.dest_CNPJ;
                                XmlNode xmlNfeNodeDestCNPJ = xmlNfeElement;
                                xmlNfeNodeDest.AppendChild(xmlNfeNodeDestCNPJ);
                                xmlNfe.AppendChild(xmlNfeNode);
                            }
                            //----------------------------------------------------------------- fim

                            // ---------------------------------------------------------------- inicio CPF -- E03 - f-E01
                            //IdentificacaoDestinatarioNfe.dest_CPF = regrasNfe.verificaTipoPessoaCPFDestinatario(IdentificacaoDestinatarioNfe.dest_CNPJ, IdentificacaoDestinatarioNfe.dest_CPF, IdentificacaoDestinatarioNfe.dest_enderDest_cPais);
                            if (regrasNfe.verificaCampoVazio(IdentificacaoDestinatarioNfe.dest_CPF))
                            {
                                xmlNfeElement = xmlNfe.CreateElement("CPF");
                                xmlNfeElement.InnerText = IdentificacaoDestinatarioNfe.dest_CPF;
                                XmlNode xmlNfeNodeDestCPF = xmlNfeElement;
                                xmlNfeNodeDest.AppendChild(xmlNfeNodeDestCPF);
                                xmlNfe.AppendChild(xmlNfeNode);
                            }
                            //----------------------------------------------------------------- fim

                            // ---------------------------------------------------------------- inicio idEstrangeiro -- E03a - f-E01
                            if (regrasNfe.verificaIdEstrangeiro(IdentificacaoDestinatarioNfe.dest_idEstrangeiro, IdentificacaoDestinatarioNfe.dest_enderDest_cPais))
                            {
                                xmlNfeElement = xmlNfe.CreateElement("idEstrangeiro");
                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoDestinatarioNfe.dest_idEstrangeiro, 2, 5, 20, false, null, "E03a");
                                XmlNode xmlNfeNodeDestIdEstrangeiro = xmlNfeElement;
                                xmlNfeNodeDest.AppendChild(xmlNfeNodeDestIdEstrangeiro);
                                xmlNfe.AppendChild(xmlNfeNode);
                            }

                            //----------------------------------------------------------------- fim

                            // ---------------------------------------------------------------- inicio xNome -- E04 - f-E01
                            if (regrasNfe.verificaNomeDestinatario(IdentificacaoDestinatarioNfe.dest_xNome, identificacaoNfe.ide_mod))
                            {
                                xmlNfeElement = xmlNfe.CreateElement("xNome");
                                xmlNfeElement.InnerText = IdentificacaoDestinatarioNfe.dest_xNome;
                                XmlNode xmlNfeNodeDestXNome = xmlNfeElement;
                                xmlNfeNodeDest.AppendChild(xmlNfeNodeDestXNome);
                                xmlNfe.AppendChild(xmlNfeNode);
                            }
                            else
                            {

                                return null;
                            }
                            //----------------------------------------------------------------- fim

                            // ---------------------------------------------------------------- inicio enderDest -- E05 - f-E01
                            xmlNfeElement = xmlNfe.CreateElement("enderDest");
                            XmlNode xmlNfeNodedestEnderDest = xmlNfeElement;
                            xmlNfeNodeDest.AppendChild(xmlNfeNodedestEnderDest);
                            xmlNfe.AppendChild(xmlNfeNode);
                            //----------------------------------------------------------------- 

                            // ---------------------------------------------------------------- inicio xLgr -- E06 - f-E05
                            xmlNfeElement = xmlNfe.CreateElement("xLgr");
                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoDestinatarioNfe.dest_enderDest_xLgr, 2, 2, 60, false, null, "E06");
                            XmlNode xmlNfeNodeDestXLgr = xmlNfeElement;
                            xmlNfeNodedestEnderDest.AppendChild(xmlNfeNodeDestXLgr);
                            xmlNfe.AppendChild(xmlNfeNode);
                            //----------------------------------------------------------------- fim

                            // ---------------------------------------------------------------- inicio nro -- E07 - f-E05
                            xmlNfeElement = xmlNfe.CreateElement("nro");
                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoDestinatarioNfe.dest_enderDest_nro, 2, 1, 60, false, null, "E07");
                            XmlNode xmlNfeNodeDestNro = xmlNfeElement;
                            xmlNfeNodedestEnderDest.AppendChild(xmlNfeNodeDestNro);
                            xmlNfe.AppendChild(xmlNfeNode);
                            //----------------------------------------------------------------- fim

                            // ---------------------------------------------------------------- inicio xCpl -- E08 - f-E05
                            if (regrasNfe.verificaCampoVazio(IdentificacaoDestinatarioNfe.dest_enderDest_xCpl))
                            {
                                xmlNfeElement = xmlNfe.CreateElement("xCpl");
                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoDestinatarioNfe.dest_enderDest_xCpl, 2, 1, 60, false, null, "E08");
                                XmlNode xmlNfeNodeDestXCpl = xmlNfeElement;
                                xmlNfeNodedestEnderDest.AppendChild(xmlNfeNodeDestXCpl);
                                xmlNfe.AppendChild(xmlNfeNode);
                            }
                            //----------------------------------------------------------------- fim

                            // ---------------------------------------------------------------- inicio xBairro -- E09 - f-E05
                            xmlNfeElement = xmlNfe.CreateElement("xBairro");
                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoDestinatarioNfe.dest_enderDest_xBairro, 2, 2, 60, false, null, "E09");
                            XmlNode xmlNfeNodeDestXBairro = xmlNfeElement;
                            xmlNfeNodedestEnderDest.AppendChild(xmlNfeNodeDestXBairro);
                            xmlNfe.AppendChild(xmlNfeNode);
                            //----------------------------------------------------------------- fim

                            // ---------------------------------------------------------------- inicio cMun -- E10 - f-E05
                            xmlNfeElement = xmlNfe.CreateElement("cMun");
                            xmlNfeElement.InnerText = regrasNfe.validaCodigoCidadeExterior(IdentificacaoDestinatarioNfe.dest_enderDest_cMun, IdentificacaoDestinatarioNfe.dest_enderDest_cMun);
                            XmlNode xmlNfeNodeDestCMun = xmlNfeElement;
                            xmlNfeNodedestEnderDest.AppendChild(xmlNfeNodeDestCMun);
                            xmlNfe.AppendChild(xmlNfeNode);
                            //----------------------------------------------------------------- fim

                            // ---------------------------------------------------------------- inicio xMun -- E11 - f-E05
                            xmlNfeElement = xmlNfe.CreateElement("xMun");
                            xmlNfeElement.InnerText = regrasNfe.validaNomeCidadeExterior(IdentificacaoDestinatarioNfe.dest_enderDest_xMun, IdentificacaoDestinatarioNfe.dest_enderDest_xMun);
                            XmlNode xmlNfeNodeDestXMun = xmlNfeElement;
                            xmlNfeNodedestEnderDest.AppendChild(xmlNfeNodeDestXMun);
                            xmlNfe.AppendChild(xmlNfeNode);
                            //----------------------------------------------------------------- fim

                            // ---------------------------------------------------------------- inicio UF -- E12 - f-E05
                            xmlNfeElement = xmlNfe.CreateElement("UF");
                            xmlNfeElement.InnerText = regrasNfe.validaUfExterior(IdentificacaoDestinatarioNfe.dest_enderDest_UF, IdentificacaoDestinatarioNfe.dest_enderDest_UF);
                            XmlNode xmlNfeNodeDestUF = xmlNfeElement;
                            xmlNfeNodedestEnderDest.AppendChild(xmlNfeNodeDestUF);
                            xmlNfe.AppendChild(xmlNfeNode);
                            //----------------------------------------------------------------- fim

                            // ---------------------------------------------------------------- inicio CEP -- E13 - f-E05
                            if (regrasNfe.verificaCampoVazio(IdentificacaoDestinatarioNfe.dest_enderDest_CEP))
                            {
                                xmlNfeElement = xmlNfe.CreateElement("CEP");
                                xmlNfeElement.InnerText = regrasNfe.validaCep(IdentificacaoDestinatarioNfe.dest_enderDest_CEP);
                                XmlNode xmlNfeNodeDestCEP = xmlNfeElement;
                                xmlNfeNodedestEnderDest.AppendChild(xmlNfeNodeDestCEP);
                                xmlNfe.AppendChild(xmlNfeNode);
                            }
                            //----------------------------------------------------------------- fim

                            // ---------------------------------------------------------------- inicio cPais -- E14 - f-E05
                            if (regrasNfe.verificaCampoVazio(IdentificacaoDestinatarioNfe.dest_enderDest_cPais))
                            {
                                xmlNfeElement = xmlNfe.CreateElement("cPais");
                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoDestinatarioNfe.dest_enderDest_cPais, 2, 2, 4, false, null, "E14");
                                XmlNode xmlNfeNodeDestCPais = xmlNfeElement;
                                xmlNfeNodedestEnderDest.AppendChild(xmlNfeNodeDestCPais);
                                xmlNfe.AppendChild(xmlNfeNode);
                            }
                            //----------------------------------------------------------------- fim

                            // ---------------------------------------------------------------- inicio xPais -- E15 - f-E05
                            if (regrasNfe.verificaCampoVazio(IdentificacaoDestinatarioNfe.dest_enderDest_xPais))
                            {
                                xmlNfeElement = xmlNfe.CreateElement("xPais");
                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoDestinatarioNfe.dest_enderDest_xPais, 2, 2, 60, false, null, "E15");
                                XmlNode xmlNfeNodeDestXPais = xmlNfeElement;
                                xmlNfeNodedestEnderDest.AppendChild(xmlNfeNodeDestXPais);
                                xmlNfe.AppendChild(xmlNfeNode);
                            }
                            //----------------------------------------------------------------- fim

                            // ---------------------------------------------------------------- inicio fone -- E16 - f-E05
                            if (regrasNfe.verificaCampoVazio(IdentificacaoDestinatarioNfe.dest_enderDest_fone))
                            {
                                xmlNfeElement = xmlNfe.CreateElement("fone");
                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoDestinatarioNfe.dest_enderDest_fone, 2, 6, 14, false, null, "E16");
                                XmlNode xmlNfeNodeDestFone = xmlNfeElement;
                                xmlNfeNodedestEnderDest.AppendChild(xmlNfeNodeDestFone);
                                xmlNfe.AppendChild(xmlNfeNode);
                            }
                            //----------------------------------------------------------------- fim

                            // ---------------------------------------------------------------- inicio indIEDest -- E16a - f-E01
                            IdentificacaoDestinatarioNfe.dest_enderDest_indIEDest = "9";
                            xmlNfeElement = xmlNfe.CreateElement("indIEDest");
                            xmlNfeElement.InnerText = IdentificacaoDestinatarioNfe.dest_enderDest_indIEDest;
                            XmlNode xmlNfeNodeDestIndIEDest = xmlNfeElement;
                            xmlNfeNodeDest.AppendChild(xmlNfeNodeDestIndIEDest);
                            xmlNfe.AppendChild(xmlNfeNode);
                            //----------------------------------------------------------------- fim

                            // ---------------------------------------------------------------- inicio IE -- E17 - f-E01
                            //if (regrasNfe.verificaTipoIndicadorDestinatoarioIE(IdentificacaoDestinatarioNfe.dest_enderDest_indIEDest))
                            //{
                            //    if (regrasNfe.verificaCampoVazio(IdentificacaoDestinatarioNfe.dest_enderDest_IE))
                            //    {
                            //        xmlNfeElement = xmlNfe.CreateElement("IE");
                            //        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoDestinatarioNfe.dest_enderDest_IE, 2, 2, 14, false, null);
                            //        XmlNode xmlNfeNodeDestIE = xmlNfeElement;
                            //        xmlNfeNodeDest.AppendChild(xmlNfeNodeDestIE);
                            //        xmlNfe.AppendChild(xmlNfeNode);
                            //    }
                            //}
                            //----------------------------------------------------------------- fim

                            // ---------------------------------------------------------------- inicio ISUF -- E18 - f-E01
                            //if (regrasNfe.verificaCampoVazio(IdentificacaoDestinatarioNfe.dest_enderDest_ISUF))
                            //{
                            //    xmlNfeElement = xmlNfe.CreateElement("ISUF");
                            //    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoDestinatarioNfe.dest_enderDest_ISUF, 2, 8, 9, false, null);
                            //    XmlNode xmlNfeNodeDestISUF = xmlNfeElement;
                            //    xmlNfeNodeDest.AppendChild(xmlNfeNodeDestISUF);
                            //    xmlNfe.AppendChild(xmlNfeNode);
                            //}
                            //----------------------------------------------------------------- fim

                            // ---------------------------------------------------------------- inicio ISUF -- E18a - f-E01
                            if (regrasNfe.verificaCampoVazio(IdentificacaoDestinatarioNfe.dest_enderDest_IM))
                            {
                                xmlNfeElement = xmlNfe.CreateElement("IM");
                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoDestinatarioNfe.dest_enderDest_IM, 2, 1, 15, false, null, "E18a");
                                XmlNode xmlNfeNodeDestIM = xmlNfeElement;
                                xmlNfeNodeDest.AppendChild(xmlNfeNodeDestIM);
                                xmlNfe.AppendChild(xmlNfeNode);
                            }
                            //----------------------------------------------------------------- fim

                            // ---------------------------------------------------------------- inicio email -- E19 - f-E01
                            if (regrasNfe.verificaCampoVazio(IdentificacaoDestinatarioNfe.dest_enderDest_email))
                            {
                                xmlNfeElement = xmlNfe.CreateElement("email");
                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoDestinatarioNfe.dest_enderDest_email, 2, 1, 60, false, null, "E19");
                                XmlNode xmlNfeNodeDestEmail = xmlNfeElement;
                                xmlNfeNodeDest.AppendChild(xmlNfeNodeDestEmail);
                                xmlNfe.AppendChild(xmlNfeNode);
                            }
                            //----------------------------------------------------------------- fim
                        }
                    }
                    #endregion

                    #region #F Identificação do Local de Retirada
                    if (regrasNfe.verificaEnderecoRetiradaIgualRemetente(IdentificacaoEmitenteNfe.emit_enderEmi_xLgr,
                        IdentificacaoEmitenteNfe.emit_enderEmi_nro, IdentificacaoEmitenteNfe.emit_enderEmi_xBairro,
                        IdentificacaoEmitenteNfe.emit_enderEmi_cMun, IdentificacaoLocalRetirada.retirada_xLgr,
                        IdentificacaoLocalRetirada.retirada_nro, IdentificacaoLocalRetirada.retirada_xBairro, IdentificacaoLocalRetirada.retirada_cMun))
                    {
                        IdentificacaoLocalRetirada = listaIdentificacaoLocalRetirada[i];
                        // ---------------------------------------------------------------- inicio retirada -- F01 - f-A01
                        xmlNfeElement = xmlNfe.CreateElement("retirada");
                        XmlNode xmlNfeNodeRetirada = xmlNfeElement;
                        xmlNfeNodeInfNfe.AppendChild(xmlNfeNodeRetirada);
                        xmlNfe.AppendChild(xmlNfeNode);
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio CNPJ -- F02 - f-F01
                        if (regrasNfe.verificaCampoVazio(IdentificacaoLocalRetirada.retirada_CNPJ))
                        {
                            xmlNfeElement = xmlNfe.CreateElement("CNPJ");
                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoLocalRetirada.retirada_CNPJ, 2, 14, 14, false, null, "F02");
                            XmlNode xmlNfeNodeRetiradaCNPJ = xmlNfeElement;
                            xmlNfeNodeRetirada.AppendChild(xmlNfeNodeRetiradaCNPJ);
                            xmlNfe.AppendChild(xmlNfeNode);
                        }
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio CPF -- F02a - f-F01
                        if (regrasNfe.verificaCampoVazio(IdentificacaoLocalRetirada.retirada_CPF))
                        {
                            xmlNfeElement = xmlNfe.CreateElement("CPF");
                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoLocalRetirada.retirada_CPF, 2, 11, 11, false, null, "F02a");
                            XmlNode xmlNfeNodeRetiradaCPF = xmlNfeElement;
                            xmlNfeNodeRetirada.AppendChild(xmlNfeNodeRetiradaCPF);
                            xmlNfe.AppendChild(xmlNfeNode);
                        }
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio xLgr -- F03 - f-F01
                        xmlNfeElement = xmlNfe.CreateElement("xLgr");
                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoLocalRetirada.retirada_xLgr, 2, 2, 60, false, null, "F03");
                        XmlNode xmlNfeNodeRetiradaXLgr = xmlNfeElement;
                        xmlNfeNodeRetirada.AppendChild(xmlNfeNodeRetiradaXLgr);
                        xmlNfe.AppendChild(xmlNfeNode);
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio nro -- F04 - f-F01
                        xmlNfeElement = xmlNfe.CreateElement("nro");
                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoLocalRetirada.retirada_nro, 2, 1, 60, false, null, "F04");
                        XmlNode xmlNfeNodeRetiradaNro = xmlNfeElement;
                        xmlNfeNodeRetirada.AppendChild(xmlNfeNodeRetiradaNro);
                        xmlNfe.AppendChild(xmlNfeNode);
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio xCpl -- F05 - f-F01
                        if (regrasNfe.verificaCampoVazio(IdentificacaoLocalRetirada.retirada_xCpl))
                        {
                            xmlNfeElement = xmlNfe.CreateElement("xCpl");
                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoLocalRetirada.retirada_xCpl, 2, 1, 60, false, null, "F05");
                            XmlNode xmlNfeNodeRetiradaXCpl = xmlNfeElement;
                            xmlNfeNodeRetirada.AppendChild(xmlNfeNodeRetiradaXCpl);
                            xmlNfe.AppendChild(xmlNfeNode);
                        }
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio xBairro -- F06 - f-F01
                        xmlNfeElement = xmlNfe.CreateElement("xBairro");
                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoLocalRetirada.retirada_xBairro, 2, 2, 60, false, null, "F06");
                        XmlNode xmlNfeNodeRetiradaXBairro = xmlNfeElement;
                        xmlNfeNodeRetirada.AppendChild(xmlNfeNodeRetiradaXBairro);
                        xmlNfe.AppendChild(xmlNfeNode);
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio cMun -- F07 - f-F01
                        xmlNfeElement = xmlNfe.CreateElement("cMun");
                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoLocalRetirada.retirada_cMun, 2, 7, 7, false, null, "F07");
                        XmlNode xmlNfeNodeRetiradaCMun = xmlNfeElement;
                        xmlNfeNodeRetirada.AppendChild(xmlNfeNodeRetiradaCMun);
                        xmlNfe.AppendChild(xmlNfeNode);
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio xMun -- F08 - f-F01
                        xmlNfeElement = xmlNfe.CreateElement("xMun");
                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoLocalRetirada.retirada_xMun, 2, 2, 60, false, null, "F08");
                        XmlNode xmlNfeNodeRetiradaXMun = xmlNfeElement;
                        xmlNfeNodeRetirada.AppendChild(xmlNfeNodeRetiradaXMun);
                        xmlNfe.AppendChild(xmlNfeNode);
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio UF -- F09 - f-F01
                        xmlNfeElement = xmlNfe.CreateElement("UF");
                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoLocalRetirada.retirada_UF, 2, 2, 2, false, null, "F09");
                        XmlNode xmlNfeNodeRetiradaUF = xmlNfeElement;
                        xmlNfeNodeRetirada.AppendChild(xmlNfeNodeRetiradaUF);
                        xmlNfe.AppendChild(xmlNfeNode);
                        //----------------------------------------------------------------- fim
                    }
                    #endregion

                    #region #G - Identificação do Local de Entrega
                    if (regrasNfe.verificaEnderecoRetiradaIgualRemetente(IdentificacaoDestinatarioNfe.dest_enderDest_xLgr,
                        IdentificacaoDestinatarioNfe.dest_enderDest_nro, IdentificacaoDestinatarioNfe.dest_enderDest_xBairro,
                        IdentificacaoDestinatarioNfe.dest_enderDest_cMun, IdentificacaoLocalEntrega.entrega_xLgr,
                        IdentificacaoLocalEntrega.entrega_nro, IdentificacaoLocalEntrega.entrega_xBairro, IdentificacaoLocalEntrega.entrega_cMun))
                    {
                        IdentificacaoLocalEntrega = listaIdentificacaoLocalEntrega[i];
                        // ---------------------------------------------------------------- inicio entrega -- G01 - A01
                        xmlNfeElement = xmlNfe.CreateElement("entrega");
                        XmlNode xmlNfeNodeEntrega = xmlNfeElement;
                        xmlNfeNodeInfNfe.AppendChild(xmlNfeNodeEntrega);
                        xmlNfe.AppendChild(xmlNfeNode);
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio CNPJ -- G02 - f-G01
                        if (regrasNfe.verificaCampoVazio(IdentificacaoLocalEntrega.entrega_CNPJ))
                        {
                            xmlNfeElement = xmlNfe.CreateElement("CNPJ");
                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoLocalEntrega.entrega_CNPJ, 2, 14, 14, false, null, "G02");
                            XmlNode xmlNfeNodeEntregaCNPJ = xmlNfeElement;
                            xmlNfeNodeEntrega.AppendChild(xmlNfeNodeEntregaCNPJ);
                            xmlNfe.AppendChild(xmlNfeNode);
                        }
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio CPF -- G02a - f-G01
                        if (regrasNfe.verificaCampoVazio(IdentificacaoLocalEntrega.entrega_CPF))
                        {
                            xmlNfeElement = xmlNfe.CreateElement("CPF");
                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoLocalEntrega.entrega_CPF, 2, 11, 11, false, null, "G02a");
                            XmlNode xmlNfeNodeEntregaCPF = xmlNfeElement;
                            xmlNfeNodeEntrega.AppendChild(xmlNfeNodeEntregaCPF);
                            xmlNfe.AppendChild(xmlNfeNode);
                        }
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio xLgr -- G03 - f-G01
                        xmlNfeElement = xmlNfe.CreateElement("xLgr");
                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoLocalEntrega.entrega_xLgr, 2, 2, 6, false, null, "G03");
                        XmlNode xmlNfeNodeEntregaXLgr = xmlNfeElement;
                        xmlNfeNodeEntrega.AppendChild(xmlNfeNodeEntregaXLgr);
                        xmlNfe.AppendChild(xmlNfeNode);
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio nro -- G04 - f-G01
                        xmlNfeElement = xmlNfe.CreateElement("nro");
                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoLocalEntrega.entrega_nro, 2, 1, 60, false, null, "G04");
                        XmlNode xmlNfeNodeEntregaNro = xmlNfeElement;
                        xmlNfeNodeEntrega.AppendChild(xmlNfeNodeEntregaNro);
                        xmlNfe.AppendChild(xmlNfeNode);
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio xCpl -- G05 - f-G01
                        if (regrasNfe.verificaCampoVazio(IdentificacaoLocalEntrega.entrega_xCpl))
                        {
                            xmlNfeElement = xmlNfe.CreateElement("xCpl");
                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoLocalEntrega.entrega_xCpl, 2, 1, 60, false, null, "G05");
                            XmlNode xmlNfeNodeEntregaXCpl = xmlNfeElement;
                            xmlNfeNodeEntrega.AppendChild(xmlNfeNodeEntregaXCpl);
                            xmlNfe.AppendChild(xmlNfeNode);
                        }
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio xBairro -- G06 - f-G01
                        xmlNfeElement = xmlNfe.CreateElement("xBairro");
                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoLocalEntrega.entrega_xBairro, 2, 2, 60, false, null, "G06");
                        XmlNode xmlNfeNodeEntregaXBairro = xmlNfeElement;
                        xmlNfeNodeEntrega.AppendChild(xmlNfeNodeEntregaXBairro);
                        xmlNfe.AppendChild(xmlNfeNode);
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio cMun -- G07 - f-G01
                        //IdentificacaoLocalEntrega.entrega_cMun = regrasNfe.validaCodigoCidadeExterior();
                        xmlNfeElement = xmlNfe.CreateElement("cMun");
                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoLocalEntrega.entrega_cMun, 2, 7, 7, false, null, "G07");
                        XmlNode xmlNfeNodeEntregaCMun = xmlNfeElement;
                        xmlNfeNodeEntrega.AppendChild(xmlNfeNodeEntregaCMun);
                        xmlNfe.AppendChild(xmlNfeNode);
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio xMun -- G08 - f-G01
                        xmlNfeElement = xmlNfe.CreateElement("xMun");
                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoLocalEntrega.entrega_xMun, 2, 2, 60, false, null, "G08");
                        XmlNode xmlNfeNodeEntregaXMun = xmlNfeElement;
                        xmlNfeNodeEntrega.AppendChild(xmlNfeNodeEntregaXMun);
                        xmlNfe.AppendChild(xmlNfeNode);
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio UF -- G09 - f-G01
                        xmlNfeElement = xmlNfe.CreateElement("UF");
                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(IdentificacaoLocalEntrega.entrega_UF, 2, 2, 2, false, null, "G09");
                        XmlNode xmlNfeNodeEntregaUF = xmlNfeElement;
                        xmlNfeNodeEntrega.AppendChild(xmlNfeNodeEntregaUF);
                        xmlNfe.AppendChild(xmlNfeNode);
                        //----------------------------------------------------------------- fim
                    }
                    #endregion

                    #region #GA - Autorização para obter XML
                    contador = 0;
                    for (int j = 0; j < listaAutorizacaoObterXml.Count; j++)
                    {
                        if (listaAutorizacaoObterXml[j].flagNumeroNota == i && contador <= 10)
                        {
                            contador++;
                            if (regrasNfe.verificaCampoVazio(AutorizacaoObterXml.autXML_CNPJ) && regrasNfe.verificaCampoVazio(AutorizacaoObterXml.autXML_CPF))
                            {
                                // ---------------------------------------------------------------- inicio autXML -- G50 - f-A01
                                xmlNfeElement = xmlNfe.CreateElement("autXML");
                                XmlNode xmlNfeNodeAutXML = xmlNfeElement;
                                xmlNfeNodeInfNfe.AppendChild(xmlNfeNodeAutXML);
                                xmlNfe.AppendChild(xmlNfeNode);
                                //----------------------------------------------------------------- fim

                                // ---------------------------------------------------------------- inicio CNPJ -- G51 - f-G50
                                xmlNfeElement = xmlNfe.CreateElement("CNPJ");
                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(AutorizacaoObterXml.autXML_CNPJ, 2, 14, 14, false, null, "G51");
                                XmlNode xmlNfeNodeAutXMLCNPJ = xmlNfeElement;
                                xmlNfeNodeAutXML.AppendChild(xmlNfeNodeAutXMLCNPJ);
                                xmlNfe.AppendChild(xmlNfeNode);
                                //----------------------------------------------------------------- fim

                                // ---------------------------------------------------------------- inicio CPF -- G52 - f-G50
                                xmlNfeElement = xmlNfe.CreateElement("CPF");
                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(AutorizacaoObterXml.autXML_CPF, 2, 11, 11, false, null, "G52");
                                XmlNode xmlNfeNodeAutXMLCPF = xmlNfeElement;
                                xmlNfeNodeAutXML.AppendChild(xmlNfeNodeAutXMLCPF);
                                xmlNfe.AppendChild(xmlNfeNode);
                                //----------------------------------------------------------------- fim
                            }
                        }
                    }
                    #endregion

                    for (int j = 0; j < listaDetalhamentoProdutosNfe.Count; j++)
                    {
                        if (j == 990)
                            break;
                        if (listaDetalhamentoProdutosNfe[j].flagNumeroNota == i)
                        {
                            #region #H - Detalhamento de Produtos e Serviços da NF-e
                            DetalhamentoProdutosNfe = listaDetalhamentoProdutosNfe[j];
                            // ---------------------------------------------------------------- inicio det -- H01 - f-A01
                            xmlNfeElement = xmlNfe.CreateElement("det");
                            xmlNfeElement.SetAttribute("nItem", regrasNfe.verificaNumeroItem(DetalhamentoProdutosNfe.det_nItem) ? DetalhamentoProdutosNfe.det_nItem : ""); //nItem -- H02 - f-H01
                            XmlNode xmlNfeNodeDet = xmlNfeElement;
                            xmlNfeNodeInfNfe.AppendChild(xmlNfeNodeDet);
                            xmlNfe.AppendChild(xmlNfeNode);
                            //----------------------------------------------------------------- fim
                            #endregion

                            #region #I - Produtos e Serviços da NF-e
                            ProdutosServicosNfe = listaProdutosServicosNfe[j];
                            // ---------------------------------------------------------------- inicio prod -- I01 - f-H01
                            xmlNfeElement = xmlNfe.CreateElement("prod");
                            XmlNode xmlNfeNodeDetProd = xmlNfeElement;
                            xmlNfeNodeDet.AppendChild(xmlNfeNodeDetProd);
                            xmlNfe.AppendChild(xmlNfeNode);
                            //----------------------------------------------------------------- fim

                            // ---------------------------------------------------------------- inicio cProd -- I02 - f-I01
                            xmlNfeElement = xmlNfe.CreateElement("cProd");
                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ProdutosServicosNfe.prod_cProd, 2, 1, 60, false, null, "I02");
                            XmlNode xmlNfeNodeDetProdCProd = xmlNfeElement;
                            xmlNfeNodeDetProd.AppendChild(xmlNfeNodeDetProdCProd);
                            xmlNfe.AppendChild(xmlNfeNode);
                            //----------------------------------------------------------------- fim

                            // ---------------------------------------------------------------- inicio cEAN -- I03 - f-I01
                            if (regrasNfe.verificaCampoVazio(ProdutosServicosNfe.prod_cEAN))
                            {
                                if (regrasNfe.verificaCodigoEan(ProdutosServicosNfe.prod_cEAN))
                                {
                                    xmlNfeElement = xmlNfe.CreateElement("cEAN");
                                    xmlNfeElement.InnerText = ProdutosServicosNfe.prod_cEAN;
                                    XmlNode xmlNfeNodeDetProdCEAN = xmlNfeElement;
                                    xmlNfeNodeDetProd.AppendChild(xmlNfeNodeDetProdCEAN);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                }
                                else
                                {
                                    return null;
                                }
                            }
                            //----------------------------------------------------------------- fim

                            // ---------------------------------------------------------------- inicio xProd -- I04 - f-I01
                            xmlNfeElement = xmlNfe.CreateElement("xProd");
                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ProdutosServicosNfe.prod_xProd, 2, 1, 120, false, null, "I04");
                            XmlNode xmlNfeNodeDetProdXProd = xmlNfeElement;
                            xmlNfeNodeDetProd.AppendChild(xmlNfeNodeDetProdXProd);
                            xmlNfe.AppendChild(xmlNfeNode);
                            //----------------------------------------------------------------- fim

                            // ---------------------------------------------------------------- inicio NCM -- I05 - f-I01
                            if (regrasNfe.verificaNCM(ProdutosServicosNfe.prod_NCM))
                            {
                                xmlNfeElement = xmlNfe.CreateElement("NCM");
                                xmlNfeElement.InnerText = ProdutosServicosNfe.prod_NCM;
                                XmlNode xmlNfeNodeDetProdNCM = xmlNfeElement;
                                xmlNfeNodeDetProd.AppendChild(xmlNfeNodeDetProdNCM);
                                xmlNfe.AppendChild(xmlNfeNode);
                            }
                            else
                            {

                                return null;
                            }
                            //----------------------------------------------------------------- fim

                            // ---------------------------------------------------------------- inicio NVE -- I05a - f-I01
                            if (regrasNfe.verificaCampoVazio(ProdutosServicosNfe.prod_NVE))
                            {
                                xmlNfeElement = xmlNfe.CreateElement("NVE");
                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ProdutosServicosNfe.prod_NVE, 2, 1, 8, false, null, "I05a");
                                XmlNode xmlNfeNodeDetProdNVE = xmlNfeElement;
                                xmlNfeNodeDetProd.AppendChild(xmlNfeNodeDetProdNVE);
                                xmlNfe.AppendChild(xmlNfeNode);
                            }
                            //----------------------------------------------------------------- fim

                            // ---------------------------------------------------------------- inicio EXTIPI -- I06 - f-I01
                            if (regrasNfe.verificaCampoVazio(ProdutosServicosNfe.prod_EXTIPI))
                            {
                                xmlNfeElement = xmlNfe.CreateElement("EXTIPI");
                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ProdutosServicosNfe.prod_EXTIPI, 2, 2, 3, true, null, "I06");
                                XmlNode xmlNfeNodeDetProdEXTIPI = xmlNfeElement;
                                xmlNfeNodeDetProd.AppendChild(xmlNfeNodeDetProdEXTIPI);
                                xmlNfe.AppendChild(xmlNfeNode);
                            }
                            //----------------------------------------------------------------- fim

                            // ---------------------------------------------------------------- inicio CFOP -- I08 - f-I01
                            xmlNfeElement = xmlNfe.CreateElement("CFOP");
                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ProdutosServicosNfe.prod_CFOP, 2, 4, 4, false, null, "I08");
                            XmlNode xmlNfeNodeDetProdCFOP = xmlNfeElement;
                            xmlNfeNodeDetProd.AppendChild(xmlNfeNodeDetProdCFOP);
                            xmlNfe.AppendChild(xmlNfeNode);
                            //----------------------------------------------------------------- fim

                            // ---------------------------------------------------------------- inicio uCom -- I09 - f-I01
                            xmlNfeElement = xmlNfe.CreateElement("uCom");
                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ProdutosServicosNfe.prod_uCom, 2, 1, 6, false, null, "I09");
                            XmlNode xmlNfeNodeDetProdUCom = xmlNfeElement;
                            xmlNfeNodeDetProd.AppendChild(xmlNfeNodeDetProdUCom);
                            xmlNfe.AppendChild(xmlNfeNode);
                            //----------------------------------------------------------------- fim

                            // ---------------------------------------------------------------- inicio qCom -- I10 - f-I01
                            xmlNfeElement = xmlNfe.CreateElement("qCom");
                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ProdutosServicosNfe.prod_qCom, 2, 0, 4, false, null, "I10");
                            XmlNode xmlNfeNodeDetProdQCom = xmlNfeElement;
                            xmlNfeNodeDetProd.AppendChild(xmlNfeNodeDetProdQCom);
                            xmlNfe.AppendChild(xmlNfeNode);
                            //----------------------------------------------------------------- fim

                            // ---------------------------------------------------------------- inicio vUnCom -- I10a - f-I01
                            xmlNfeElement = xmlNfe.CreateElement("vUnCom");
                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ProdutosServicosNfe.prod_vUnCom, 2, 0, 10, false, null, "I10a");
                            XmlNode xmlNfeNodeDetProdVUnCom = xmlNfeElement;
                            xmlNfeNodeDetProd.AppendChild(xmlNfeNodeDetProdVUnCom);
                            xmlNfe.AppendChild(xmlNfeNode);
                            //----------------------------------------------------------------- fim

                            // ---------------------------------------------------------------- inicio vProd -- I11 - f-I01
                            xmlNfeElement = xmlNfe.CreateElement("vProd");
                            xmlNfeElement.InnerText = ProdutosServicosNfe.prod_vProd;
                            XmlNode xmlNfeNodeDetProdVProd = xmlNfeElement;
                            xmlNfeNodeDetProd.AppendChild(xmlNfeNodeDetProdVProd);
                            xmlNfe.AppendChild(xmlNfeNode);
                            //----------------------------------------------------------------- fim

                            // ---------------------------------------------------------------- inicio cEANTrib -- I12 - f-I01

                            if (regrasNfe.verificaCodigoEanTrib(ProdutosServicosNfe.prod_cEANTrib))
                            {
                                xmlNfeElement = xmlNfe.CreateElement("cEANTrib");
                                xmlNfeElement.InnerText = ProdutosServicosNfe.prod_cEANTrib;
                                XmlNode xmlNfeNodeDetProdCEANTrib = xmlNfeElement;
                                xmlNfeNodeDetProd.AppendChild(xmlNfeNodeDetProdCEANTrib);
                                xmlNfe.AppendChild(xmlNfeNode);
                            }
                            else
                            {
                                return null;
                            }
                            //----------------------------------------------------------------- fim

                            // ---------------------------------------------------------------- inicio uTrib -- I13 - f-I01
                            xmlNfeElement = xmlNfe.CreateElement("uTrib");
                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ProdutosServicosNfe.prod_uTrib, 2, 1, 6, false, null, "I13");
                            XmlNode xmlNfeNodeDetProdUTrib = xmlNfeElement;
                            xmlNfeNodeDetProd.AppendChild(xmlNfeNodeDetProdUTrib);
                            xmlNfe.AppendChild(xmlNfeNode);
                            //----------------------------------------------------------------- 

                            // ---------------------------------------------------------------- inicio qTrib -- I14 - f-I01
                            xmlNfeElement = xmlNfe.CreateElement("qTrib");
                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ProdutosServicosNfe.prod_qTrib, 2, 0, 4, false, null, "I14");
                            XmlNode xmlNfeNodeDetProdQTrib = xmlNfeElement;
                            xmlNfeNodeDetProd.AppendChild(xmlNfeNodeDetProdQTrib);
                            xmlNfe.AppendChild(xmlNfeNode);
                            //----------------------------------------------------------------- fim

                            // ---------------------------------------------------------------- inicio vUnTrib -- I14a - f-I01
                            xmlNfeElement = xmlNfe.CreateElement("vUnTrib");
                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ProdutosServicosNfe.prod_vUnTrib, 2, 0, 10, false, null, "I14a");
                            XmlNode xmlNfeNodeDetProdVUnTrib = xmlNfeElement;
                            xmlNfeNodeDetProd.AppendChild(xmlNfeNodeDetProdVUnTrib);
                            xmlNfe.AppendChild(xmlNfeNode);
                            //----------------------------------------------------------------- fim

                            // ---------------------------------------------------------------- inicio vFrete -- I15 - f-I01
                            if (regrasNfe.verificaCampoVazio(ProdutosServicosNfe.prod_vFrete))
                            {
                                xmlNfeElement = xmlNfe.CreateElement("vFrete");
                                xmlNfeElement.InnerText = ProdutosServicosNfe.prod_vFrete;
                                XmlNode xmlNfeNodeDetProdVFrete = xmlNfeElement;
                                xmlNfeNodeDetProd.AppendChild(xmlNfeNodeDetProdVFrete);
                                xmlNfe.AppendChild(xmlNfeNode);
                            }
                            //----------------------------------------------------------------- fim

                            // ---------------------------------------------------------------- inicio vSeg -- I16 - f-I01
                            if (regrasNfe.verificaCampoVazio(ProdutosServicosNfe.prod_vSeg))
                            {
                                xmlNfeElement = xmlNfe.CreateElement("vSeg");
                                xmlNfeElement.InnerText = ProdutosServicosNfe.prod_vSeg;
                                XmlNode xmlNfeNodeDetProdVSeg = xmlNfeElement;
                                xmlNfeNodeDetProd.AppendChild(xmlNfeNodeDetProdVSeg);
                                xmlNfe.AppendChild(xmlNfeNode);
                            }
                            //----------------------------------------------------------------- fim

                            // ---------------------------------------------------------------- inicio vDesc -- I17 - f-I01
                            if (regrasNfe.verificaCampoVazio(ProdutosServicosNfe.prod_vDesc))
                            {
                                xmlNfeElement = xmlNfe.CreateElement("vDesc");
                                xmlNfeElement.InnerText = ProdutosServicosNfe.prod_vDesc;
                                XmlNode xmlNfeNodeDetProdVDesc = xmlNfeElement;
                                xmlNfeNodeDetProd.AppendChild(xmlNfeNodeDetProdVDesc);
                                xmlNfe.AppendChild(xmlNfeNode);
                            }
                            //----------------------------------------------------------------- fim

                            // ---------------------------------------------------------------- inicio vOutro -- I17a - f-I01
                            if (regrasNfe.verificaCampoVazio(ProdutosServicosNfe.prod_vOutro))
                            {
                                xmlNfeElement = xmlNfe.CreateElement("vOutro");
                                xmlNfeElement.InnerText = ProdutosServicosNfe.prod_vOutro;
                                XmlNode xmlNfeNodeDetProdVOutro = xmlNfeElement;
                                xmlNfeNodeDetProd.AppendChild(xmlNfeNodeDetProdVOutro);
                                xmlNfe.AppendChild(xmlNfeNode);
                            }
                            //----------------------------------------------------------------- fim

                            // ---------------------------------------------------------------- inicio indTot -- I17b - f122
                            if (regrasNfe.verificaIndicadorTotal(ProdutosServicosNfe.prod_indTot))
                            {
                                xmlNfeElement = xmlNfe.CreateElement("indTot");
                                xmlNfeElement.InnerText = ProdutosServicosNfe.prod_indTot;
                                XmlNode xmlNfeNodeDetProdIndTot = xmlNfeElement;
                                xmlNfeNodeDetProd.AppendChild(xmlNfeNodeDetProdIndTot);
                                xmlNfe.AppendChild(xmlNfeNode);
                            }
                            //----------------------------------------------------------------- fim
                            #endregion

                            #region #I01 - Produtos e Serviços / Declaração de Importação
                            for (int y = 0; y < listaProdutosServicosDeclaracaoImportacao.Count; y++)
                            {
                                if (y == 100)
                                    break;
                                if (listaProdutosServicosDeclaracaoImportacao[y].numeroItem.Equals(DetalhamentoProdutosNfe.det_nItem))
                                {
                                    ProdutosServicosDeclaracaoImportacao = listaProdutosServicosDeclaracaoImportacao[y];
                                    // ---------------------------------------------------------------- inicio DI -- I18 - I01
                                    xmlNfeElement = xmlNfe.CreateElement("DI");
                                    XmlNode xmlNfeNodeDetProdDI = xmlNfeElement;
                                    xmlNfeNodeDetProd.AppendChild(xmlNfeNodeDetProdDI);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio nDI -- I19 - f-I18
                                    xmlNfeElement = xmlNfe.CreateElement("nDI");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ProdutosServicosDeclaracaoImportacao.DI_nDI, 2, 1, 12, false, null, "I19");
                                    XmlNode xmlNfeNodeDetProdDInDI = xmlNfeElement;
                                    xmlNfeNodeDetProdDI.AppendChild(xmlNfeNodeDetProdDInDI);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio dDI -- I20 - f-I18
                                    xmlNfeElement = xmlNfe.CreateElement("dDI");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ProdutosServicosDeclaracaoImportacao.DI_dDI, 3, null, null, false, null, "I20");
                                    XmlNode xmlNfeNodeDetProdDIdDI = xmlNfeElement;
                                    xmlNfeNodeDetProdDI.AppendChild(xmlNfeNodeDetProdDIdDI);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio xLocDesemb -- I21 - f-I18
                                    xmlNfeElement = xmlNfe.CreateElement("xLocDesemb");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ProdutosServicosDeclaracaoImportacao.DI_xLocDesemb, 2, 1, 60, false, null, "I21");
                                    XmlNode xmlNfeNodeDetProdDIxLocDesemb = xmlNfeElement;
                                    xmlNfeNodeDetProdDI.AppendChild(xmlNfeNodeDetProdDIxLocDesemb);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio UFDesemb -- I22 - f-I18
                                    xmlNfeElement = xmlNfe.CreateElement("UFDesemb");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ProdutosServicosDeclaracaoImportacao.DI_UFDesemb, 2, 2, 2, false, null, "I22");
                                    XmlNode xmlNfeNodeDetProdDIUFDesemb = xmlNfeElement;
                                    xmlNfeNodeDetProdDI.AppendChild(xmlNfeNodeDetProdDIUFDesemb);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio dDesemb -- I23 - f-I18
                                    xmlNfeElement = xmlNfe.CreateElement("dDesemb");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ProdutosServicosDeclaracaoImportacao.DI_dDesemb, 3, null, null, false, null, "I23");
                                    XmlNode xmlNfeNodeDetProdDIdDesemb = xmlNfeElement;
                                    xmlNfeNodeDetProdDI.AppendChild(xmlNfeNodeDetProdDIdDesemb);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio tpViaTransp -- I23a - f-I18
                                    if (regrasNfe.verificaTipoTransporteInternacional(ProdutosServicosDeclaracaoImportacao.DI_tpViaTransp))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("tpViaTransp");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ProdutosServicosDeclaracaoImportacao.DI_tpViaTransp, 2, 2, 2, true, null, "I23a");
                                        XmlNode xmlNfeNodeDetProdDItpViaTransp = xmlNfeElement;
                                        xmlNfeNodeDetProdDI.AppendChild(xmlNfeNodeDetProdDItpViaTransp);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio vAFRMM -- I23b - f-I18
                                    if (regrasNfe.verificaCampoVazio(ProdutosServicosDeclaracaoImportacao.DI_vAFRMM))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("vAFRMM");
                                        xmlNfeElement.InnerText = ProdutosServicosDeclaracaoImportacao.DI_vAFRMM;
                                        XmlNode xmlNfeNodeDetProdDIvAFRMM = xmlNfeElement;
                                        xmlNfeNodeDetProdDI.AppendChild(xmlNfeNodeDetProdDIvAFRMM);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    else
                                    {

                                        return null;
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio tpIntermedio -- I23c - f-I18
                                    if (regrasNfe.verificaTipoIntermedioTransporteInternacional(ProdutosServicosDeclaracaoImportacao.DI_tpIntermedio))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("tpIntermedio");
                                        xmlNfeElement.InnerText = ProdutosServicosDeclaracaoImportacao.DI_tpIntermedio;
                                        XmlNode xmlNfeNodeDetProdDItpIntermedio = xmlNfeElement;
                                        xmlNfeNodeDetProdDI.AppendChild(xmlNfeNodeDetProdDItpIntermedio);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    else
                                    {

                                        return null;
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio CNPJ -- I23d - f-I18
                                    if (regrasNfe.verificaCnpjObrigatorioDeclaracaoImportacao(ProdutosServicosDeclaracaoImportacao.DI_tpIntermedio) || regrasNfe.verificaCampoVazio(ProdutosServicosDeclaracaoImportacao.DI_CNPJ))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("CNPJ");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ProdutosServicosDeclaracaoImportacao.DI_CNPJ, 2, 14, 14, false, null, "I23d");
                                        XmlNode xmlNfeNodeDetProdDICNPJ = xmlNfeElement;
                                        xmlNfeNodeDetProdDI.AppendChild(xmlNfeNodeDetProdDICNPJ);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio UFTerceiro -- I23e - f-I18
                                    if (regrasNfe.verificaCnpjObrigatorioDeclaracaoImportacao(ProdutosServicosDeclaracaoImportacao.DI_tpIntermedio) || regrasNfe.verificaCampoVazio(ProdutosServicosDeclaracaoImportacao.DI_UFTerceiro))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("UFTerceiro");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ProdutosServicosDeclaracaoImportacao.DI_UFTerceiro, 2, 2, 2, false, null, "I23e");
                                        XmlNode xmlNfeNodeDetProdDIUFTerceiro = xmlNfeElement;
                                        xmlNfeNodeDetProdDI.AppendChild(xmlNfeNodeDetProdDIUFTerceiro);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio cExportador -- I24 - f-I18
                                    xmlNfeElement = xmlNfe.CreateElement("cExportador");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ProdutosServicosDeclaracaoImportacao.DI_cExportador, 2, 1, 60, false, null, "I24");
                                    XmlNode xmlNfeNodeDetProdDIdcExportador = xmlNfeElement;
                                    xmlNfeNodeDetProdDI.AppendChild(xmlNfeNodeDetProdDIdcExportador);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio adi -- I25 - f-I18
                                    xmlNfeElement = xmlNfe.CreateElement("adi");
                                    XmlNode xmlNfeNodeDetProdDIadi = xmlNfeElement;
                                    xmlNfeNodeDetProdDI.AppendChild(xmlNfeNodeDetProdDIadi);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio nAdicao -- I26 - f-I25
                                    xmlNfeElement = xmlNfe.CreateElement("nAdicao");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ProdutosServicosDeclaracaoImportacao.DI_adi_nAdicao, 2, 1, 3, false, null, "I26");
                                    XmlNode xmlNfeNodeDetProdDIdcExportadorNAdicao = xmlNfeElement;
                                    xmlNfeNodeDetProdDIadi.AppendChild(xmlNfeNodeDetProdDIdcExportadorNAdicao);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- 

                                    // ---------------------------------------------------------------- inicio nSeqAdicC -- I27 - f-I25
                                    xmlNfeElement = xmlNfe.CreateElement("nSeqAdicC");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ProdutosServicosDeclaracaoImportacao.DI_adi_nSeqAdicC, 2, 1, 3, false, null, "I27");
                                    XmlNode xmlNfeNodeDetProdDIdcExportadorNSeqAdicC = xmlNfeElement;
                                    xmlNfeNodeDetProdDIadi.AppendChild(xmlNfeNodeDetProdDIdcExportadorNSeqAdicC);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio cFabricante -- I28 - f-I25
                                    xmlNfeElement = xmlNfe.CreateElement("cFabricante");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ProdutosServicosDeclaracaoImportacao.DI_adi_cFabricante, 2, 1, 60, false, null, "I28");
                                    XmlNode xmlNfeNodeDetProdDIdcExportadorCFabricante = xmlNfeElement;
                                    xmlNfeNodeDetProdDIadi.AppendChild(xmlNfeNodeDetProdDIdcExportadorCFabricante);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio vDescDI -- I29 - f-I25
                                    if (regrasNfe.verificaCampoVazio(ProdutosServicosDeclaracaoImportacao.DI_adi_vDescDI))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("vDescDI");
                                        xmlNfeElement.InnerText = ProdutosServicosDeclaracaoImportacao.DI_adi_vDescDI;
                                        XmlNode xmlNfeNodeDetProdDIdcExportadorVDescDI = xmlNfeElement;
                                        xmlNfeNodeDetProdDIadi.AppendChild(xmlNfeNodeDetProdDIdcExportadorVDescDI);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio nDraw -- I29a - f-I25
                                    if (regrasNfe.verificaCampoVazio(ProdutosServicosDeclaracaoImportacao.DI_adi_nDraw))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("nDraw");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ProdutosServicosDeclaracaoImportacao.DI_adi_nDraw, 2, 11, 11, false, null, "I29a");
                                        XmlNode xmlNfeNodeDetProdDIdcExportadorNDraw = xmlNfeElement;
                                        xmlNfeNodeDetProdDIadi.AppendChild(xmlNfeNodeDetProdDIdcExportadorNDraw);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    //----------------------------------------------------------------- fim
                                }
                            }
                            #endregion

                            #region #I03 - Produtos e Serviços / Grupo deExportação
                            if (!regrasNfe.verificaDrawbackInformado(ProdutosServicosGrupoExportacao.detExport_nDraw))
                            {
                                for (int q = 0; q < listaProdutosServicosGrupoExportacao.Count; q++)
                                {
                                    if (listaProdutosServicosGrupoExportacao[q].numeroItem.Equals(DetalhamentoProdutosNfe.det_nItem))
                                    {
                                        ProdutosServicosGrupoExportacao = listaProdutosServicosGrupoExportacao[q];
                                        // ---------------------------------------------------------------- inicio detExport -- I50 - f-I01
                                        xmlNfeElement = xmlNfe.CreateElement("detExport");
                                        XmlNode xmlNfeNodeDetProdDetExport = xmlNfeElement;
                                        xmlNfeNodeDetProd.AppendChild(xmlNfeNodeDetProdDetExport);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio nDraw -- I51 - f-I50
                                        if (regrasNfe.verificaCampoVazio(ProdutosServicosGrupoExportacao.detExport_nDraw))
                                        {
                                            xmlNfeElement = xmlNfe.CreateElement("nDraw");
                                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ProdutosServicosGrupoExportacao.detExport_nDraw, 2, 11, 11, false, null, "I51");
                                            XmlNode xmlNfeNodeDetProdDetExportNDraw = xmlNfeElement;
                                            xmlNfeNodeDetProdDetExport.AppendChild(xmlNfeNodeDetProdDetExportNDraw);
                                            xmlNfe.AppendChild(xmlNfeNode);
                                        }
                                        //----------------------------------------------------------------- fim
                                        if (!regrasNfe.verificaGrupoExportacaoIndireta(ProdutosServicosGrupoExportacao.detExport_exportInd_nRE, ProdutosServicosGrupoExportacao.detExport_exportInd_chNFe, ProdutosServicosGrupoExportacao.detExport_exportInd_qExport))
                                        {

                                            // ---------------------------------------------------------------- inicio exportInd -- I52 - f-I50
                                            xmlNfeElement = xmlNfe.CreateElement("exportInd");
                                            XmlNode xmlNfeNodeDetProdDetExportExportInd = xmlNfeElement;
                                            xmlNfeNodeDetProdDetExport.AppendChild(xmlNfeNodeDetProdDetExportExportInd);
                                            xmlNfe.AppendChild(xmlNfeNode);
                                            //----------------------------------------------------------------- fim

                                            // ---------------------------------------------------------------- inicio nRE -- I53 - f-I52
                                            xmlNfeElement = xmlNfe.CreateElement("nRE");
                                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ProdutosServicosGrupoExportacao.detExport_exportInd_nRE, 2, 12, 12, false, null, "I53");
                                            XmlNode xmlNfeNodeDetProdDetExportNRE = xmlNfeElement;
                                            xmlNfeNodeDetProdDetExportExportInd.AppendChild(xmlNfeNodeDetProdDetExportNRE);
                                            xmlNfe.AppendChild(xmlNfeNode);
                                            //----------------------------------------------------------------- fim

                                            // ---------------------------------------------------------------- inicio chNFe -- I54 - f-I52
                                            xmlNfeElement = xmlNfe.CreateElement("chNFe");
                                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ProdutosServicosGrupoExportacao.detExport_exportInd_chNFe, 2, 44, 44, false, null, "I54");
                                            XmlNode xmlNfeNodeDetProdDetExportChNFe = xmlNfeElement;
                                            xmlNfeNodeDetProdDetExportExportInd.AppendChild(xmlNfeNodeDetProdDetExportChNFe);
                                            xmlNfe.AppendChild(xmlNfeNode);
                                            //----------------------------------------------------------------- fim

                                            // ---------------------------------------------------------------- inicio qExport -- I55 - f-I52
                                            if (regrasNfe.verificaCampoVazio(ProdutosServicosGrupoExportacao.detExport_exportInd_qExport))
                                            {
                                                xmlNfeElement = xmlNfe.CreateElement("qExport");
                                                xmlNfeElement.InnerText = ProdutosServicosGrupoExportacao.detExport_exportInd_qExport;
                                                XmlNode xmlNfeNodeDetProdDetExportQExport = xmlNfeElement;
                                                xmlNfeNodeDetProdDetExportExportInd.AppendChild(xmlNfeNodeDetProdDetExportQExport);
                                                xmlNfe.AppendChild(xmlNfeNode);
                                            }
                                            else
                                            {
                                                regrasNfe.mensagemAlerta("Campo qExport de preenchimento obrigatório.");

                                                return null;
                                            }
                                            //----------------------------------------------------------------- fim

                                        }
                                    }
                                }
                            }
                            #endregion

                            #region #I05 - Produtos e Serviços/ Pedido de Compra
                            if (listaProdutosServicosPedidoCompra.Count > 0)
                            {
                                ProdutosServicosPedidoCompra = listaProdutosServicosPedidoCompra[j];
                                // ---------------------------------------------------------------- inicio xPed -- I60 - f-I01
                                if (regrasNfe.verificaCampoVazio(ProdutosServicosPedidoCompra.DI_xPed))
                                {
                                    xmlNfeElement = xmlNfe.CreateElement("xPed");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ProdutosServicosPedidoCompra.DI_xPed, 2, 1, 15, false, null, "I60");
                                    XmlNode xmlNfeNodeDetProdXPed = xmlNfeElement;
                                    xmlNfeNodeDetProd.AppendChild(xmlNfeNodeDetProdXPed);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                }
                                //----------------------------------------------------------------- fim

                                // ---------------------------------------------------------------- inicio nItemPed -- I61 - f-I01
                                if (regrasNfe.verificaCampoVazio(ProdutosServicosPedidoCompra.DI_nItemPed))
                                {
                                    xmlNfeElement = xmlNfe.CreateElement("nItemPed");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ProdutosServicosPedidoCompra.DI_nItemPed, 2, 6, 6, false, null, "I61");
                                    XmlNode xmlNfeNodeDetProdNItemPed = xmlNfeElement;
                                    xmlNfeNodeDetProd.AppendChild(xmlNfeNodeDetProdNItemPed);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                }
                                //----------------------------------------------------------------- fim
                            }

                            #endregion

                            #region #I07 - Produtos e Serviços/ Grupo Diversos
                            if (listaProdutosServicosGrupoDiversos.Count > 0)
                            {
                                ProdutosServicosGrupoDiversos = listaProdutosServicosGrupoDiversos[j];
                                // ---------------------------------------------------------------- inicio nFCI -- I70 - f-I01
                                if (regrasNfe.verificaCampoVazio(ProdutosServicosGrupoDiversos.DI_nFCI))
                                {
                                    xmlNfeElement = xmlNfe.CreateElement("nFCI");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ProdutosServicosGrupoDiversos.DI_nFCI, 2, 36, 36, false, null, "I70");
                                    XmlNode xmlNfeNodeDetProdNFCI = xmlNfeElement;
                                    xmlNfeNodeDetProd.AppendChild(xmlNfeNodeDetProdNFCI);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                }
                                //----------------------------------------------------------------- fim
                            }
                            #endregion

                            #region #J - Produto Específico
                            //// ---------------------------------------------------------------- inicio erproNFe -- I90 - f-I01
                            //xmlNfeElement = xmlNfe.CreateElement("erproNFe");
                            //XmlNode xmlNfeNodeXI90 = xmlNfeElement;
                            //xmlNfeNodeDetProd.AppendChild(xmlNfeNodeXI90);
                            //xmlNfe.AppendChild(xmlNfeNode);
                            ////----------------------------------------------------------------- fim
                            #endregion

                            #region #JA - Detalhamento Específico de Veículos novos
                            if (regrasNfe.verificaPreenchimentoVeiculosNovos(listaDetalhamentoEspecificoVeiculosNovos))
                            {
                                DetalhamentoEspecificoVeiculosNovos = listaDetalhamentoEspecificoVeiculosNovos[j];
                                // ---------------------------------------------------------------- inicio veicProd -- J01 - f-I01
                                xmlNfeElement = xmlNfe.CreateElement("veicProd");
                                XmlNode xmlNfeNodeDetProdVeicProd = xmlNfeElement;
                                xmlNfeNodeDetProd.AppendChild(xmlNfeNodeDetProdVeicProd);
                                xmlNfe.AppendChild(xmlNfeNode);
                                //----------------------------------------------------------------- fim

                                // ---------------------------------------------------------------- inicio tpOp -- J02 - f-J01
                                if (regrasNfe.verificaTipoOperacaoVeiculosNovos(DetalhamentoEspecificoVeiculosNovos.veicProd_tpOp))
                                {
                                    xmlNfeElement = xmlNfe.CreateElement("tpOp");
                                    xmlNfeElement.InnerText = DetalhamentoEspecificoVeiculosNovos.veicProd_tpOp;
                                    XmlNode xmlNfeNodeDetProdVeicProdTpOp = xmlNfeElement;
                                    xmlNfeNodeDetProdVeicProd.AppendChild(xmlNfeNodeDetProdVeicProdTpOp);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                }
                                else
                                {

                                    return null;
                                }
                                //----------------------------------------------------------------- fim

                                // ---------------------------------------------------------------- inicio chassi -- J03 - f-J01
                                xmlNfeElement = xmlNfe.CreateElement("chassi");
                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(DetalhamentoEspecificoVeiculosNovos.veicProd_chassi, 2, 14, 14, false, null, "J03");
                                XmlNode xmlNfeNodeDetProdVeicProdChassi = xmlNfeElement;
                                xmlNfeNodeDetProdVeicProd.AppendChild(xmlNfeNodeDetProdVeicProdChassi);
                                xmlNfe.AppendChild(xmlNfeNode);
                                //----------------------------------------------------------------- fim

                                // ---------------------------------------------------------------- inicio cCor -- J04 - f-J01
                                xmlNfeElement = xmlNfe.CreateElement("cCor");
                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(DetalhamentoEspecificoVeiculosNovos.veicProd_cCor, 2, 1, 4, false, null, "");
                                XmlNode xmlNfeNodeDetProdVeicProdCCor = xmlNfeElement;
                                xmlNfeNodeDetProdVeicProd.AppendChild(xmlNfeNodeDetProdVeicProdCCor);
                                xmlNfe.AppendChild(xmlNfeNode);
                                //----------------------------------------------------------------- fim

                                // ---------------------------------------------------------------- inicio xCor -- J05 - f-J01
                                xmlNfeElement = xmlNfe.CreateElement("xCor");
                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(DetalhamentoEspecificoVeiculosNovos.veicProd_xCor, 2, 1, 40, false, null, "J05");
                                XmlNode xmlNfeNodeDetProdVeicProdXCor = xmlNfeElement;
                                xmlNfeNodeDetProdVeicProd.AppendChild(xmlNfeNodeDetProdVeicProdXCor);
                                xmlNfe.AppendChild(xmlNfeNode);
                                //----------------------------------------------------------------- fim

                                // ---------------------------------------------------------------- inicio pot -- J06 - f-J01
                                xmlNfeElement = xmlNfe.CreateElement("pot");
                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(DetalhamentoEspecificoVeiculosNovos.veicProd_pot, 2, 1, 4, false, null, "J06");
                                XmlNode xmlNfeNodeDetProdVeicProdPot = xmlNfeElement;
                                xmlNfeNodeDetProdVeicProd.AppendChild(xmlNfeNodeDetProdVeicProdPot);
                                xmlNfe.AppendChild(xmlNfeNode);
                                //----------------------------------------------------------------- fim

                                // ---------------------------------------------------------------- inicio cilin -- J07 - f-J01
                                xmlNfeElement = xmlNfe.CreateElement("cilin");
                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(DetalhamentoEspecificoVeiculosNovos.veicProd_cilin, 2, 1, 4, false, null, "J07");
                                XmlNode xmlNfeNodeDetProdVeicProdCilin = xmlNfeElement;
                                xmlNfeNodeDetProdVeicProd.AppendChild(xmlNfeNodeDetProdVeicProdCilin);
                                xmlNfe.AppendChild(xmlNfeNode);
                                //----------------------------------------------------------------- fim

                                // ---------------------------------------------------------------- inicio pesoL -- J08 - f-J01
                                xmlNfeElement = xmlNfe.CreateElement("pesoL");
                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(DetalhamentoEspecificoVeiculosNovos.veicProd_pesoL, 2, 1, 9, false, null, "J08");
                                XmlNode xmlNfeNodeDetProdVeicProdPesoL = xmlNfeElement;
                                xmlNfeNodeDetProdVeicProd.AppendChild(xmlNfeNodeDetProdVeicProdPesoL);
                                xmlNfe.AppendChild(xmlNfeNode);
                                //----------------------------------------------------------------- fim

                                // ---------------------------------------------------------------- inicio pesoB -- J09 - f-J01
                                xmlNfeElement = xmlNfe.CreateElement("pesoB");
                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(DetalhamentoEspecificoVeiculosNovos.veicProd_pesoB, 2, 1, 9, false, null, "J09");
                                XmlNode xmlNfeNodeDetProdVeicProdPesoB = xmlNfeElement;
                                xmlNfeNodeDetProdVeicProd.AppendChild(xmlNfeNodeDetProdVeicProdPesoB);
                                xmlNfe.AppendChild(xmlNfeNode);
                                //----------------------------------------------------------------- fim

                                // ---------------------------------------------------------------- inicio nSerie -- J10 - f-J01
                                xmlNfeElement = xmlNfe.CreateElement("nSerie");
                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(DetalhamentoEspecificoVeiculosNovos.veicProd_nSerie, 2, 1, 9, false, null, "J10");
                                XmlNode xmlNfeNodeDetProdVeicProdNSerie = xmlNfeElement;
                                xmlNfeNodeDetProdVeicProd.AppendChild(xmlNfeNodeDetProdVeicProdNSerie);
                                xmlNfe.AppendChild(xmlNfeNode);
                                //----------------------------------------------------------------- fim

                                // ---------------------------------------------------------------- inicio tpComb -- J11 - f-J01
                                xmlNfeElement = xmlNfe.CreateElement("tpComb");
                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(DetalhamentoEspecificoVeiculosNovos.veicProd_tpComb, 2, 1, 2, false, null, "J11");
                                XmlNode xmlNfeNodeDetProdVeicProdTpComb = xmlNfeElement;
                                xmlNfeNodeDetProdVeicProd.AppendChild(xmlNfeNodeDetProdVeicProdTpComb);
                                xmlNfe.AppendChild(xmlNfeNode);
                                //----------------------------------------------------------------- fim

                                // ---------------------------------------------------------------- inicio nMotor -- J12 - f-J01
                                xmlNfeElement = xmlNfe.CreateElement("nMotor");
                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(DetalhamentoEspecificoVeiculosNovos.veicProd_nMotor, 2, 1, 21, false, null, "J12");
                                XmlNode xmlNfeNodeDetProdVeicProdNMotor = xmlNfeElement;
                                xmlNfeNodeDetProdVeicProd.AppendChild(xmlNfeNodeDetProdVeicProdNMotor);
                                xmlNfe.AppendChild(xmlNfeNode);
                                //----------------------------------------------------------------- 

                                // ---------------------------------------------------------------- inicio CMT -- J13 - f-J01
                                xmlNfeElement = xmlNfe.CreateElement("CMT");
                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(DetalhamentoEspecificoVeiculosNovos.veicProd_CMT, 2, 1, 9, false, null, "J13");
                                XmlNode xmlNfeNodeDetProdVeicProdCMT = xmlNfeElement;
                                xmlNfeNodeDetProdVeicProd.AppendChild(xmlNfeNodeDetProdVeicProdCMT);
                                xmlNfe.AppendChild(xmlNfeNode);
                                //----------------------------------------------------------------- fim

                                // ---------------------------------------------------------------- inicio dist -- J14 - f-J01
                                xmlNfeElement = xmlNfe.CreateElement("dist");
                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(DetalhamentoEspecificoVeiculosNovos.veicProd_dist, 2, 1, 4, false, null, "J14");
                                XmlNode xmlNfeNodeDetProdVeicProdDist = xmlNfeElement;
                                xmlNfeNodeDetProdVeicProd.AppendChild(xmlNfeNodeDetProdVeicProdDist);
                                xmlNfe.AppendChild(xmlNfeNode);
                                //----------------------------------------------------------------- 

                                // ---------------------------------------------------------------- inicio anoMod -- J16 - f-J01
                                xmlNfeElement = xmlNfe.CreateElement("anoMod");
                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(DetalhamentoEspecificoVeiculosNovos.veicProd_anoMod, 2, 4, 4, false, null, "J16");
                                XmlNode xmlNfeNodeDetProdVeicProdAnoMod = xmlNfeElement;
                                xmlNfeNodeDetProdVeicProd.AppendChild(xmlNfeNodeDetProdVeicProdAnoMod);
                                xmlNfe.AppendChild(xmlNfeNode);
                                //----------------------------------------------------------------- fim

                                // ---------------------------------------------------------------- inicio anoFab -- J17 - f-J01
                                xmlNfeElement = xmlNfe.CreateElement("anoFab");
                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(DetalhamentoEspecificoVeiculosNovos.veicProd_anoFab, 2, 4, 4, false, null, "J17");
                                XmlNode xmlNfeNodeDetProdVeicProdAnoFab = xmlNfeElement;
                                xmlNfeNodeDetProdVeicProd.AppendChild(xmlNfeNodeDetProdVeicProdAnoFab);
                                xmlNfe.AppendChild(xmlNfeNode);
                                //----------------------------------------------------------------- fim

                                // ---------------------------------------------------------------- inicio tpPint -- J18 - f-J01
                                xmlNfeElement = xmlNfe.CreateElement("tpPint");
                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(DetalhamentoEspecificoVeiculosNovos.veicProd_tpPint, 2, 1, 1, false, null, "J18");
                                XmlNode xmlNfeNodeDetProdVeicProdTpPint = xmlNfeElement;
                                xmlNfeNodeDetProdVeicProd.AppendChild(xmlNfeNodeDetProdVeicProdTpPint);
                                xmlNfe.AppendChild(xmlNfeNode);
                                //----------------------------------------------------------------- fim

                                // ---------------------------------------------------------------- inicio tpVeic -- J19 - f-J01
                                xmlNfeElement = xmlNfe.CreateElement("tpVeic");
                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(DetalhamentoEspecificoVeiculosNovos.veicProd_tpVeic, 2, 1, 2, false, null, "J19");
                                XmlNode xmlNfeNodeDetProdVeicProdTpVeic = xmlNfeElement;
                                xmlNfeNodeDetProdVeicProd.AppendChild(xmlNfeNodeDetProdVeicProdTpVeic);
                                xmlNfe.AppendChild(xmlNfeNode);
                                //----------------------------------------------------------------- fim

                                // ---------------------------------------------------------------- inicio espVeic -- J20 - f-J01
                                if (regrasNfe.verificaEspecieVeiculo(DetalhamentoEspecificoVeiculosNovos.veicProd_espVeic))
                                {
                                    xmlNfeElement = xmlNfe.CreateElement("espVeic");
                                    xmlNfeElement.InnerText = DetalhamentoEspecificoVeiculosNovos.veicProd_espVeic;
                                    XmlNode xmlNfeNodeDetProdVeicProdEspVeic = xmlNfeElement;
                                    xmlNfeNodeDetProdVeicProd.AppendChild(xmlNfeNodeDetProdVeicProdEspVeic);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                }
                                else
                                {

                                    return null;
                                }
                                //----------------------------------------------------------------- fim

                                // ---------------------------------------------------------------- inicio VIN -- J21 - f-J01
                                if (regrasNfe.verificaVin(DetalhamentoEspecificoVeiculosNovos.veicProd_VIN))
                                {
                                    xmlNfeElement = xmlNfe.CreateElement("VIN");
                                    xmlNfeElement.InnerText = DetalhamentoEspecificoVeiculosNovos.veicProd_VIN;
                                    XmlNode xmlNfeNodeDetProdVeicProdVIN = xmlNfeElement;
                                    xmlNfeNodeDetProdVeicProd.AppendChild(xmlNfeNodeDetProdVeicProdVIN);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                }
                                else
                                {

                                    return null;
                                }
                                //----------------------------------------------------------------- fim

                                // ---------------------------------------------------------------- inicio condVeic -- J22 - f-J01
                                if (regrasNfe.veriricaCondicaoVeiculo(DetalhamentoEspecificoVeiculosNovos.veicProd_condVeic))
                                {
                                    xmlNfeElement = xmlNfe.CreateElement("condVeic");
                                    xmlNfeElement.InnerText = DetalhamentoEspecificoVeiculosNovos.veicProd_condVeic;
                                    XmlNode xmlNfeNodeDetProdVeicProdCondVeic = xmlNfeElement;
                                    xmlNfeNodeDetProdVeicProd.AppendChild(xmlNfeNodeDetProdVeicProdCondVeic);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                }
                                else
                                {

                                    return null;
                                }
                                //----------------------------------------------------------------- fim

                                // ---------------------------------------------------------------- inicio cMod -- J23 - f-J01
                                xmlNfeElement = xmlNfe.CreateElement("cMod");
                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(DetalhamentoEspecificoVeiculosNovos.veicProd_cMod, 2, 1, 6, false, null, "J23");
                                XmlNode xmlNfeNodeDetProdVeicProdCMod = xmlNfeElement;
                                xmlNfeNodeDetProdVeicProd.AppendChild(xmlNfeNodeDetProdVeicProdCMod);
                                xmlNfe.AppendChild(xmlNfeNode);
                                //----------------------------------------------------------------- fim

                                // ---------------------------------------------------------------- inicio cCorDENATRAN -- J24 - f-J01
                                if (regrasNfe.verificaCodigoCorDenatran(DetalhamentoEspecificoVeiculosNovos.veicProd_cCorDENATRAN))
                                {
                                    xmlNfeElement = xmlNfe.CreateElement("cCorDENATRAN");
                                    xmlNfeElement.InnerText = DetalhamentoEspecificoVeiculosNovos.veicProd_cCorDENATRAN;
                                    XmlNode xmlNfeNodeDetProdVeicProdCCorDENATRAN = xmlNfeElement;
                                    xmlNfeNodeDetProdVeicProd.AppendChild(xmlNfeNodeDetProdVeicProdCCorDENATRAN);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                }
                                else
                                {

                                    return null;
                                }
                                //----------------------------------------------------------------- fim

                                // ---------------------------------------------------------------- inicio lota -- J25 - f-J01
                                xmlNfeElement = xmlNfe.CreateElement("lota");
                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(DetalhamentoEspecificoVeiculosNovos.veicProd_lota, 2, 1, 3, false, null, "J25");
                                XmlNode xmlNfeNodeDetProdVeicProdLota = xmlNfeElement;
                                xmlNfeNodeDetProdVeicProd.AppendChild(xmlNfeNodeDetProdVeicProdLota);
                                xmlNfe.AppendChild(xmlNfeNode);
                                //----------------------------------------------------------------- fim

                                // ---------------------------------------------------------------- inicio tpRest -- J26 - f-J01
                                if (regrasNfe.verificaTipoRestricao(DetalhamentoEspecificoVeiculosNovos.veicProd_tpRest))
                                {
                                    xmlNfeElement = xmlNfe.CreateElement("tpRest");
                                    xmlNfeElement.InnerText = DetalhamentoEspecificoVeiculosNovos.veicProd_tpRest;
                                    XmlNode xmlNfeNodeDetProdVeicProdTpRest = xmlNfeElement;
                                    xmlNfeNodeDetProdVeicProd.AppendChild(xmlNfeNodeDetProdVeicProdTpRest);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                }
                                else
                                {

                                    return null;
                                }
                                //----------------------------------------------------------------- fim
                            }

                            #endregion

                            #region #K - Detalhamento Específico de Medicamentoe de matérias-primas farmacêuticas
                            if (regrasNfe.verificaDetalhamentoEspecificoMedicamento(listaDetalhamentoEspecificoMedicamentoMateriasPrimasFarmaceuticas))
                            {
                                for (int w = 0; w < listaDetalhamentoEspecificoMedicamentoMateriasPrimasFarmaceuticas.Count; w++)
                                {
                                    if (w == 500)
                                        break;
                                    if (listaDetalhamentoEspecificoMedicamentoMateriasPrimasFarmaceuticas[w].numeroItem.Equals(DetalhamentoProdutosNfe.det_nItem))
                                    {
                                        DetalhamentoEspecificoMedicamentoMateriasPrimasFarmaceuticas = listaDetalhamentoEspecificoMedicamentoMateriasPrimasFarmaceuticas[w];
                                        // ---------------------------------------------------------------- inicio med -- K01 - f-I90
                                        xmlNfeElement = xmlNfe.CreateElement("med");
                                        XmlNode xmlNfeNodeDetProdMed = xmlNfeElement;
                                        xmlNfeNodeDetProd.AppendChild(xmlNfeNodeDetProdMed);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio nLote -- K02 - f-K01
                                        xmlNfeElement = xmlNfe.CreateElement("nLote");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(DetalhamentoEspecificoMedicamentoMateriasPrimasFarmaceuticas.med_nLote, 2, 1, 20, false, null, "K02");
                                        XmlNode xmlNfeNodeDetProdMedNLote = xmlNfeElement;
                                        xmlNfeNodeDetProdMed.AppendChild(xmlNfeNodeDetProdMedNLote);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio nLote -- K03 - f-K01
                                        xmlNfeElement = xmlNfe.CreateElement("qLote");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(DetalhamentoEspecificoMedicamentoMateriasPrimasFarmaceuticas.med_qLote, 2, 1, 8, false, null, "K03");
                                        XmlNode xmlNfeNodeDetProdMedQLote = xmlNfeElement;
                                        xmlNfeNodeDetProdMed.AppendChild(xmlNfeNodeDetProdMedQLote);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio dFab -- K04 - f-K01
                                        xmlNfeElement = xmlNfe.CreateElement("dFab");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(DetalhamentoEspecificoMedicamentoMateriasPrimasFarmaceuticas.med_dFab, 3, null, null, false, null, "K04");
                                        XmlNode xmlNfeNodeDetProdMedDFab = xmlNfeElement;
                                        xmlNfeNodeDetProdMed.AppendChild(xmlNfeNodeDetProdMedDFab);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio dVal -- K05 - f-K01
                                        xmlNfeElement = xmlNfe.CreateElement("dVal");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(DetalhamentoEspecificoMedicamentoMateriasPrimasFarmaceuticas.med_dVal, 3, null, null, false, null, "K05");
                                        XmlNode xmlNfeNodeDetProdMedDVal = xmlNfeElement;
                                        xmlNfeNodeDetProdMed.AppendChild(xmlNfeNodeDetProdMedDVal);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio vPMC -- K06 - f-K01
                                        xmlNfeElement = xmlNfe.CreateElement("vPMC");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(DetalhamentoEspecificoMedicamentoMateriasPrimasFarmaceuticas.med_vPMC, 2, 1, 13, false, null, "K06");
                                        XmlNode xmlNfeNodeDetProdMedVPMC = xmlNfeElement;
                                        xmlNfeNodeDetProdMed.AppendChild(xmlNfeNodeDetProdMedVPMC);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim
                                    }
                                }
                            }

                            #endregion

                            #region #L - Detalhamento Específico de Armamentos
                            if (regrasNfe.verificaDetalhamentoEspecificoArmamentos(listaDetalhamentoEspecificoArmamentos))
                            {
                                for (int e = 0; e < listaDetalhamentoEspecificoArmamentos.Count; e++)
                                {
                                    if (e == 500)
                                        break;
                                    if (listaDetalhamentoEspecificoArmamentos[e].numeroItem.Equals(DetalhamentoProdutosNfe.det_nItem))
                                    {
                                        // ---------------------------------------------------------------- inicio arma -- L01 - f-I90
                                        xmlNfeElement = xmlNfe.CreateElement("arma");
                                        XmlNode xmlNfeNodeDetProdArma = xmlNfeElement;
                                        xmlNfeNodeDetProd.AppendChild(xmlNfeNodeDetProdArma);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio tpArma -- L02 - f-L01
                                        if (regrasNfe.verificaTipoArma(DetalhamentoEspecificoArmamentos.arma_tpArma))
                                        {
                                            xmlNfeElement = xmlNfe.CreateElement("tpArma");
                                            xmlNfeElement.InnerText = DetalhamentoEspecificoArmamentos.arma_tpArma;
                                            XmlNode xmlNfeNodeDetProdArmaTpArma = xmlNfeElement;
                                            xmlNfeNodeDetProdArma.AppendChild(xmlNfeNodeDetProdArmaTpArma);
                                            xmlNfe.AppendChild(xmlNfeNode);
                                        }
                                        else
                                        {

                                            return null;
                                        }
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio nSerie -- L03 - f-L01
                                        xmlNfeElement = xmlNfe.CreateElement("nSerie");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(DetalhamentoEspecificoArmamentos.arma_nSerie, 2, 1, 15, false, null, "L03");
                                        XmlNode xmlNfeNodeDetProdArmaNSerie = xmlNfeElement;
                                        xmlNfeNodeDetProdArma.AppendChild(xmlNfeNodeDetProdArmaNSerie);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio nCano -- L04 - f-L01
                                        xmlNfeElement = xmlNfe.CreateElement("nCano");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(DetalhamentoEspecificoArmamentos.arma_nCano, 2, 1, 15, false, null, "L04");
                                        XmlNode xmlNfeNodeDetProdArmaNCano = xmlNfeElement;
                                        xmlNfeNodeDetProdArma.AppendChild(xmlNfeNodeDetProdArmaNCano);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio descr -- L05 - f-L01
                                        xmlNfeElement = xmlNfe.CreateElement("descr");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(DetalhamentoEspecificoArmamentos.arma_descr, 2, 1, 256, false, null, "L05");
                                        XmlNode xmlNfeNodeDetProdArmaDescr = xmlNfeElement;
                                        xmlNfeNodeDetProdArma.AppendChild(xmlNfeNodeDetProdArmaDescr);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim
                                    }
                                }
                            }
                            #endregion

                            #region #LA - Detalhamento Específico de Combustíveis
                            if (regrasNfe.verificaDetalhamentoEspecificoCombustiveis(listaDetalhamentoEspecificoCombustiveis))
                            {
                                DetalhamentoEspecificoCombustiveis = listaDetalhamentoEspecificoCombustiveis[j];
                                // ---------------------------------------------------------------- inicio comb -- L101 - f-I90
                                xmlNfeElement = xmlNfe.CreateElement("comb");
                                XmlNode xmlNfeNodeDetProdComb = xmlNfeElement;
                                xmlNfeNodeDetProd.AppendChild(xmlNfeNodeDetProdComb);
                                xmlNfe.AppendChild(xmlNfeNode);
                                //----------------------------------------------------------------- fim

                                // ---------------------------------------------------------------- inicio cProdANP -- L102 - f-L101
                                xmlNfeElement = xmlNfe.CreateElement("cProdANP");
                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(DetalhamentoEspecificoCombustiveis.comb_cProdANP, 2, 9, 9, false, null, "L102");
                                XmlNode xmlNfeNodeDetProdCombCProdANP = xmlNfeElement;
                                xmlNfeNodeDetProdComb.AppendChild(xmlNfeNodeDetProdCombCProdANP);
                                xmlNfe.AppendChild(xmlNfeNode);
                                //----------------------------------------------------------------- fim

                                // ---------------------------------------------------------------- inicio pMixGN -- L102a - f-L101
                                if (regrasNfe.verificaCampoVazio(DetalhamentoEspecificoCombustiveis.comb_pMixGN))
                                {
                                    xmlNfeElement = xmlNfe.CreateElement("pMixGN");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(DetalhamentoEspecificoCombustiveis.comb_pMixGN, 2, 7, 7, false, null, "L102a");
                                    XmlNode xmlNfeNodeDetProdCombPMixGN = xmlNfeElement;
                                    xmlNfeNodeDetProdComb.AppendChild(xmlNfeNodeDetProdCombPMixGN);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                }
                                //----------------------------------------------------------------- fim

                                // ---------------------------------------------------------------- inicio CODIF -- L103 - f-L101
                                if (regrasNfe.verificaCampoVazio(DetalhamentoEspecificoCombustiveis.comb_CODIF))
                                {
                                    xmlNfeElement = xmlNfe.CreateElement("CODIF");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(DetalhamentoEspecificoCombustiveis.comb_CODIF, 2, 1, 21, false, null, "L103");
                                    XmlNode xmlNfeNodeDetProdCombCODIF = xmlNfeElement;
                                    xmlNfeNodeDetProdComb.AppendChild(xmlNfeNodeDetProdCombCODIF);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                }
                                //----------------------------------------------------------------- fim

                                // ---------------------------------------------------------------- inicio qTemp -- L104 - f-L101
                                if (regrasNfe.verificaCampoVazio(DetalhamentoEspecificoCombustiveis.comb_qTemp))
                                {
                                    xmlNfeElement = xmlNfe.CreateElement("qTemp");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(DetalhamentoEspecificoCombustiveis.comb_qTemp, 2, 1, 17, false, null, "L104");
                                    XmlNode xmlNfeNodeDetProdCombQTemp = xmlNfeElement;
                                    xmlNfeNodeDetProdComb.AppendChild(xmlNfeNodeDetProdCombQTemp);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                }
                                //----------------------------------------------------------------- fim

                                // ---------------------------------------------------------------- inicio UFCons -- L120 - f-L101
                                xmlNfeElement = xmlNfe.CreateElement("UFCons");
                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(DetalhamentoEspecificoCombustiveis.comb_UFCons, 2, 2, 2, false, null, "L120");
                                XmlNode xmlNfeNodeDetProdCombUFCons = xmlNfeElement;
                                xmlNfeNodeDetProdComb.AppendChild(xmlNfeNodeDetProdCombUFCons);
                                xmlNfe.AppendChild(xmlNfeNode);
                                //----------------------------------------------------------------- fim

                                // ---------------------------------------------------------------- inicio CIDE -- L105 - f-L101
                                xmlNfeElement = xmlNfe.CreateElement("CIDE");
                                XmlNode xmlNfeNodeDetProdCombCIDE = xmlNfeElement;
                                xmlNfeNodeDetProdComb.AppendChild(xmlNfeNodeDetProdCombCIDE);
                                xmlNfe.AppendChild(xmlNfeNode);
                                //----------------------------------------------------------------- fim

                                // ---------------------------------------------------------------- inicio qBCprod -- L106 - f-L105
                                xmlNfeElement = xmlNfe.CreateElement("qBCprod");
                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(DetalhamentoEspecificoCombustiveis.comb_CIDE_qBCProd, 2, 1, 17, false, null, "L106");
                                XmlNode xmlNfeNodeDetProdCombCIDEQBCprod = xmlNfeElement;
                                xmlNfeNodeDetProdCombCIDE.AppendChild(xmlNfeNodeDetProdCombCIDEQBCprod);
                                xmlNfe.AppendChild(xmlNfeNode);
                                //----------------------------------------------------------------- fim

                                // ---------------------------------------------------------------- inicio vAliqProd -- L107 - f-L105
                                xmlNfeElement = xmlNfe.CreateElement("vAliqProd");
                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(DetalhamentoEspecificoCombustiveis.comb_CIDE_vAliqProd, 2, 1, 16, false, null, "L107");
                                XmlNode xmlNfeNodeDetProdCombCIDEVAliqProd = xmlNfeElement;
                                xmlNfeNodeDetProdCombCIDE.AppendChild(xmlNfeNodeDetProdCombCIDEVAliqProd);
                                xmlNfe.AppendChild(xmlNfeNode);
                                //----------------------------------------------------------------- 

                                // ---------------------------------------------------------------- inicio vCIDE -- L108 - f-L105
                                xmlNfeElement = xmlNfe.CreateElement("vCIDE");
                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(DetalhamentoEspecificoCombustiveis.comb_CIDE_vCIDE, 2, 1, 16, false, null, "L108");
                                XmlNode xmlNfeNodeDetProdCombCIDEvCIDE = xmlNfeElement;
                                xmlNfeNodeDetProdCombCIDE.AppendChild(xmlNfeNodeDetProdCombCIDEvCIDE);
                                xmlNfe.AppendChild(xmlNfeNode);
                                //----------------------------------------------------------------- fim
                            }
                            #endregion

                            #region #LB - Detalhamento Específico para Operação com Papel Imune
                            if (regrasNfe.verificaDetalhamentoEspecificoOperacaoPapelImune(listaDetalhamentoEspecificoOperacaoPapelImune))
                            {
                                DetalhamentoEspecificoOperacaoPapelImune = listaDetalhamentoEspecificoOperacaoPapelImune[j];
                                // ---------------------------------------------------------------- inicio nRECOPI -- L109 - f-I90
                                xmlNfeElement = xmlNfe.CreateElement("nRECOPI");
                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(DetalhamentoEspecificoOperacaoPapelImune.nRECOPI, 2, 20, 20, false, null, "L109");
                                XmlNode xmlNfeNodeDetProdNRECOPI = xmlNfeElement;
                                xmlNfeNodeDetProd.AppendChild(xmlNfeNodeDetProdNRECOPI);
                                xmlNfe.AppendChild(xmlNfeNode);
                                //----------------------------------------------------------------- fim
                            }
                            #endregion

                            #region #M - Tributos incidentes no Produto ou Serviço

                            TributosIncidentesProdutoServico = listaTributosIncidentesProdutoServico[j];
                            // ---------------------------------------------------------------- inicio imposto -- M01 - H01
                            xmlNfeElement = xmlNfe.CreateElement("imposto");
                            XmlNode xmlNfeNodeDetImposto = xmlNfeElement;
                            xmlNfeNodeDet.AppendChild(xmlNfeNodeDetImposto);
                            xmlNfe.AppendChild(xmlNfeNode);
                            //----------------------------------------------------------------- fim

                            // ---------------------------------------------------------------- inicio vTotTrib -- M02 - f-M01
                            xmlNfeElement = xmlNfe.CreateElement("vTotTrib");
                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(TributosIncidentesProdutoServico.imposto_vTotTrib, 2, 1, 16, false, null, "M02");
                            XmlNode xmlNfeNodeDetImpostoVTotTrib = xmlNfeElement;
                            xmlNfeNodeDetImposto.AppendChild(xmlNfeNodeDetImpostoVTotTrib);
                            xmlNfe.AppendChild(xmlNfeNode);
                            //----------------------------------------------------------------- fim

                            #endregion

                            #region #N - ICMS Normal e ST
                            if (!regrasNfe.verificaIssqn(listaISSQN))
                            {
                                ICMSNormalST = listaICMSNormalST[j];
                                // ---------------------------------------------------------------- inicio ICMS -- N01 - f-M01
                                xmlNfeElement = xmlNfe.CreateElement("ICMS");
                                XmlNode xmlNfeNodeDetImpostoICMS = xmlNfeElement;
                                xmlNfeNodeDetImposto.AppendChild(xmlNfeNodeDetImpostoICMS);
                                xmlNfe.AppendChild(xmlNfeNode);
                                //----------------------------------------------------------------- fim

                                if (regrasNfe.verificaIcms("00", ICMSNormalST.ICMS_ICMS00_CST))
                                {
                                    // ---------------------------------------------------------------- inicio ICMS00 -- N02 - f-N01
                                    xmlNfeElement = xmlNfe.CreateElement("ICMS00");
                                    XmlNode xmlNfeNodeDetImpostoICMSICMS00 = xmlNfeElement;
                                    xmlNfeNodeDetImpostoICMS.AppendChild(xmlNfeNodeDetImpostoICMSICMS00);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio orig -- N011 - f-N02
                                    if (regrasNfe.verificaOrigemMercadoriaIcms(ICMSNormalST.ICMS_ICMS00_orig))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("orig");
                                        xmlNfeElement.InnerText = ICMSNormalST.ICMS_ICMS00_orig;
                                        XmlNode xmlNfeNodeDetImpostoICMSorig = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMS00.AppendChild(xmlNfeNodeDetImpostoICMSorig);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    else
                                    {

                                        return null;
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio CST -- N12 - f-N02
                                    xmlNfeElement = xmlNfe.CreateElement("CST");
                                    xmlNfeElement.InnerText = "00";
                                    XmlNode xmlNfeNodeDetImpostoICMSCST = xmlNfeElement;
                                    xmlNfeNodeDetImpostoICMSICMS00.AppendChild(xmlNfeNodeDetImpostoICMSCST);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio modBC -- N13 - f-N02
                                    if (regrasNfe.verificaModalidadeBaseCalculo(ICMSNormalST.ICMS_ICMS00_modBC))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("modBC");
                                        xmlNfeElement.InnerText = ICMSNormalST.ICMS_ICMS00_modBC;
                                        XmlNode xmlNfeNodeDetImpostoICMSmodBC = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMS00.AppendChild(xmlNfeNodeDetImpostoICMSmodBC);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    else
                                    {

                                        return null;
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio vBC -- N15 - f-N02
                                    xmlNfeElement = xmlNfe.CreateElement("vBC");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMS00_vBC, 2, 1, 16, false, null, "N15");
                                    XmlNode xmlNfeNodeDetImpostoICMSvBC = xmlNfeElement;
                                    xmlNfeNodeDetImpostoICMSICMS00.AppendChild(xmlNfeNodeDetImpostoICMSvBC);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio pICMS -- N16 - f-N02
                                    xmlNfeElement = xmlNfe.CreateElement("pICMS");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMS00_pICMS, 2, 1, 8, false, null, "N16");
                                    XmlNode xmlNfeNodeDetImpostoICMSpICMS = xmlNfeElement;
                                    xmlNfeNodeDetImpostoICMSICMS00.AppendChild(xmlNfeNodeDetImpostoICMSpICMS);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- 

                                    // ---------------------------------------------------------------- inicio vICMS -- N17 - f-N02
                                    xmlNfeElement = xmlNfe.CreateElement("vICMS");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMS00_vICMS, 2, 1, 16, false, null, "N17");
                                    XmlNode xmlNfeNodeDetImpostoICMSvICMS = xmlNfeElement;
                                    xmlNfeNodeDetImpostoICMSICMS00.AppendChild(xmlNfeNodeDetImpostoICMSvICMS);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim
                                }

                                if (regrasNfe.verificaIcms("10", ICMSNormalST.ICMS_ICMS10_CST))
                                {
                                    // ---------------------------------------------------------------- inicio ICMS10 -- N03 - f-N01
                                    xmlNfeElement = xmlNfe.CreateElement("ICMS10");
                                    XmlNode xmlNfeNodeDetImpostoICMSICMS10 = xmlNfeElement;
                                    xmlNfeNodeDetImpostoICMS.AppendChild(xmlNfeNodeDetImpostoICMSICMS10);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio orig -- N11 - f-N03
                                    if (regrasNfe.verificaOrigemMercadoriaIcms(ICMSNormalST.ICMS_ICMS10_orig))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("orig");
                                        xmlNfeElement.InnerText = ICMSNormalST.ICMS_ICMS10_orig;
                                        XmlNode xmlNfeNodeDetImpostoICMSICMS10orig = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMS10.AppendChild(xmlNfeNodeDetImpostoICMSICMS10orig);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    else
                                    {

                                        return null;
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio CST -- N12 - f-N03
                                    xmlNfeElement = xmlNfe.CreateElement("CST");
                                    xmlNfeElement.InnerText = "10";
                                    XmlNode xmlNfeNodeDetImpostoICMSICMS10CST = xmlNfeElement;
                                    xmlNfeNodeDetImpostoICMSICMS10.AppendChild(xmlNfeNodeDetImpostoICMSICMS10CST);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio modBC -- N13 - f-N03
                                    if (regrasNfe.verificaModalidadeBaseCalculo(ICMSNormalST.ICMS_ICMS10_modBC))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("modBC");
                                        xmlNfeElement.InnerText = ICMSNormalST.ICMS_ICMS10_modBC;
                                        XmlNode xmlNfeNodeDetImpostoICMSICMS10modBC = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMS10.AppendChild(xmlNfeNodeDetImpostoICMSICMS10modBC);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    else
                                    {

                                        return null;
                                    }
                                    //----------------------------------------------------------------- 

                                    // ---------------------------------------------------------------- inicio vBC -- N15 - f-N03
                                    xmlNfeElement = xmlNfe.CreateElement("vBC");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMS10_vBC, 2, 1, 16, false, null, "N15");
                                    XmlNode xmlNfeNodeDetImpostoICMSICMS10vBC = xmlNfeElement;
                                    xmlNfeNodeDetImpostoICMSICMS10.AppendChild(xmlNfeNodeDetImpostoICMSICMS10vBC);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio pICMS -- N16 - f-N03
                                    xmlNfeElement = xmlNfe.CreateElement("pICMS");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMS10_pICMS, 2, 1, 8, false, null, "N16");
                                    XmlNode xmlNfeNodeDetImpostoICMSICMS10pICMS = xmlNfeElement;
                                    xmlNfeNodeDetImpostoICMSICMS10.AppendChild(xmlNfeNodeDetImpostoICMSICMS10pICMS);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio vICMS -- N17 - f-N03
                                    xmlNfeElement = xmlNfe.CreateElement("vICMS");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMS10_vICMS, 2, 1, 16, false, null, "N17");
                                    XmlNode xmlNfeNodeDetImpostoICMSICMS10vICMS = xmlNfeElement;
                                    xmlNfeNodeDetImpostoICMSICMS10.AppendChild(xmlNfeNodeDetImpostoICMSICMS10vICMS);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio modBCST -- N18 - f-N03
                                    if (regrasNfe.verificaModalidadeBaseCalculoST(ICMSNormalST.ICMS_ICMS10_modBCST))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("modBCST");
                                        xmlNfeElement.InnerText = ICMSNormalST.ICMS_ICMS10_modBCST;
                                        XmlNode xmlNfeNodeDetImpostoICMSICMS10modBCST = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMS10.AppendChild(xmlNfeNodeDetImpostoICMSICMS10modBCST);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    else
                                    {

                                        return null;
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio pMVAST -- N19 - f-N03
                                    if (regrasNfe.verificaCampoVazio(ICMSNormalST.ICMS_ICMS10_pMVAST))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("pMVAST");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMS10_pMVAST, 2, 1, 8, false, null, "N19");
                                        XmlNode xmlNfeNodeDetImpostoICMSICMS10pMVAST = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMS10.AppendChild(xmlNfeNodeDetImpostoICMSICMS10pMVAST);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    //----------------------------------------------------------------- 

                                    // ---------------------------------------------------------------- inicio pRedBCST -- N20 - f-N03
                                    if (regrasNfe.verificaCampoVazio(ICMSNormalST.ICMS_ICMS10_pRedBCST))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("pRedBCST");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMS10_pRedBCST, 2, 1, 8, false, null, "N20");
                                        XmlNode xmlNfeNodeDetImpostoICMSICMS10pRedBCST = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMS10.AppendChild(xmlNfeNodeDetImpostoICMSICMS10pRedBCST);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio vBCST -- N21 - f-N03
                                    xmlNfeElement = xmlNfe.CreateElement("vBCST");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMS10_vBCST, 2, 1, 16, false, null, "N21");
                                    XmlNode xmlNfeNodeDetImpostoICMSICMS10vBCST = xmlNfeElement;
                                    xmlNfeNodeDetImpostoICMSICMS10.AppendChild(xmlNfeNodeDetImpostoICMSICMS10vBCST);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio pICMSST -- N22 - f-N03
                                    xmlNfeElement = xmlNfe.CreateElement("pICMSST");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMS10_pICMSST, 2, 1, 8, false, null, "N22");
                                    XmlNode xmlNfeNodeDetImpostoICMSICMS10pICMSST = xmlNfeElement;
                                    xmlNfeNodeDetImpostoICMSICMS10.AppendChild(xmlNfeNodeDetImpostoICMSICMS10pICMSST);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio vICMSST -- N23 - f-N03
                                    xmlNfeElement = xmlNfe.CreateElement("vICMSST");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMS10_vICMSST, 2, 1, 16, false, null, "N23");
                                    XmlNode xmlNfeNodeDetImpostoICMSICMS10vICMSST = xmlNfeElement;
                                    xmlNfeNodeDetImpostoICMSICMS10.AppendChild(xmlNfeNodeDetImpostoICMSICMS10vICMSST);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim
                                }

                                if (regrasNfe.verificaIcms("20", ICMSNormalST.ICMS_ICMS20_CST))
                                {
                                    // ---------------------------------------------------------------- inicio ICMS20 -- N04 - f-N01
                                    xmlNfeElement = xmlNfe.CreateElement("ICMS20");
                                    XmlNode xmlNfeNodeDetImpostoICMSICMS20 = xmlNfeElement;
                                    xmlNfeNodeDetImpostoICMS.AppendChild(xmlNfeNodeDetImpostoICMSICMS20);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio ICMS20 -- N11 - f-N04
                                    if (regrasNfe.verificaOrigemMercadoriaIcms(ICMSNormalST.ICMS_ICMS20_orig))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("orig");
                                        xmlNfeElement.InnerText = ICMSNormalST.ICMS_ICMS20_orig;
                                        XmlNode xmlNfeNodeDetImpostoICMSICMS20orig = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMS20.AppendChild(xmlNfeNodeDetImpostoICMSICMS20orig);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    else
                                    {

                                        return null;
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio CST -- N12 - f-N04
                                    xmlNfeElement = xmlNfe.CreateElement("CST");
                                    xmlNfeElement.InnerText = "20";
                                    XmlNode xmlNfeNodeDetImpostoICMSICMS20CST = xmlNfeElement;
                                    xmlNfeNodeDetImpostoICMSICMS20.AppendChild(xmlNfeNodeDetImpostoICMSICMS20CST);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio modBC -- N13 - f-N04
                                    if (regrasNfe.verificaModalidadeBaseCalculo(ICMSNormalST.ICMS_ICMS20_modBC))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("modBC");
                                        xmlNfeElement.InnerText = ICMSNormalST.ICMS_ICMS20_modBC;
                                        XmlNode xmlNfeNodeDetImpostoICMSICMS20modBC = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMS20.AppendChild(xmlNfeNodeDetImpostoICMSICMS20modBC);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    else
                                    {

                                        return null;
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio pRedBC -- N14 - f-N04
                                    xmlNfeElement = xmlNfe.CreateElement("pRedBC");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMS20_pRedBC, 2, 1, 8, false, null, "N14");
                                    XmlNode xmlNfeNodeDetImpostoICMSICMS20pRedBC = xmlNfeElement;
                                    xmlNfeNodeDetImpostoICMSICMS20.AppendChild(xmlNfeNodeDetImpostoICMSICMS20pRedBC);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio vBC -- N15 - f-N04
                                    xmlNfeElement = xmlNfe.CreateElement("vBC");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMS20_vBC, 2, 1, 16, false, null, "N15");
                                    XmlNode xmlNfeNodeDetImpostoICMSICMS20vBC = xmlNfeElement;
                                    xmlNfeNodeDetImpostoICMSICMS20.AppendChild(xmlNfeNodeDetImpostoICMSICMS20vBC);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio pICMS -- N16 - f-N04
                                    xmlNfeElement = xmlNfe.CreateElement("pICMS");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMS20_pICMS, 2, 1, 8, false, null, "N16");
                                    XmlNode xmlNfeNodeDetImpostoICMSICMS20pICMS = xmlNfeElement;
                                    xmlNfeNodeDetImpostoICMSICMS20.AppendChild(xmlNfeNodeDetImpostoICMSICMS20pICMS);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio vICMS -- N17 - f-N04
                                    xmlNfeElement = xmlNfe.CreateElement("vICMS");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMS20_vICMS, 2, 1, 16, false, null, "N17");
                                    XmlNode xmlNfeNodeDetImpostoICMSICMS20vICMS = xmlNfeElement;
                                    xmlNfeNodeDetImpostoICMSICMS20.AppendChild(xmlNfeNodeDetImpostoICMSICMS20vICMS);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim
                                    if (regrasNfe.verificaMotivoDesoneracaoIcms20(ICMSNormalST.ICMS_ICMS20_motDesICMS))
                                    {
                                        //// ---------------------------------------------------------------- inicio erproNFe -- N27.1 - f-N04
                                        //xmlNfeElement = xmlNfe.CreateElement("erproNFe");
                                        //XmlNode xmlNfeNodeXN27_1_1 = xmlNfeElement;
                                        //xmlNfeNodeDetImpostoICMSICMS20.AppendChild(xmlNfeNodeXN27_1_1);
                                        //xmlNfe.AppendChild(xmlNfeNode);
                                        ////----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio vICMSDeson -- N27a - f-N04
                                        xmlNfeElement = xmlNfe.CreateElement("vICMSDeson");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMS20_vICMSDeson, 2, 1, 16, false, null, "N27a");
                                        XmlNode xmlNfeNodeXN27_1_1vICMSDeson = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMS20.AppendChild(xmlNfeNodeXN27_1_1vICMSDeson);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio vICMSDeson -- N28 - f-N04
                                        xmlNfeElement = xmlNfe.CreateElement("motDesICMS");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMS20_motDesICMS, 2, 1, 2, false, null, "N28");
                                        XmlNode xmlNfeNodeXN27_1_1motDesICMS = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMS20.AppendChild(xmlNfeNodeXN27_1_1motDesICMS);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim
                                    }
                                }

                                if (regrasNfe.verificaIcms("30", ICMSNormalST.ICMS_ICMS30_CST))
                                {
                                    // ---------------------------------------------------------------- inicio ICMS30 -- N05 - f-N01
                                    xmlNfeElement = xmlNfe.CreateElement("ICMS30");
                                    XmlNode xmlNfeNodeDetImpostoICMSICMS30 = xmlNfeElement;
                                    xmlNfeNodeDetImpostoICMS.AppendChild(xmlNfeNodeDetImpostoICMSICMS30);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio orig -- N11 - f-N05
                                    if (regrasNfe.verificaOrigemMercadoriaIcms(ICMSNormalST.ICMS_ICMS30_orig))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("orig");
                                        xmlNfeElement.InnerText = ICMSNormalST.ICMS_ICMS30_orig;
                                        XmlNode xmlNfeNodeDetImpostoICMSICMS30orig = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMS30.AppendChild(xmlNfeNodeDetImpostoICMSICMS30orig);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio orig -- N12 - f-N05
                                    xmlNfeElement = xmlNfe.CreateElement("CST");
                                    xmlNfeElement.InnerText = "30";
                                    XmlNode xmlNfeNodeDetImpostoICMSICMS30CST = xmlNfeElement;
                                    xmlNfeNodeDetImpostoICMSICMS30.AppendChild(xmlNfeNodeDetImpostoICMSICMS30CST);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio modBCST -- N18 - f-N05
                                    if (regrasNfe.verificaModalidadeBaseCalculoST(ICMSNormalST.ICMS_ICMS30_modBCST))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("modBCST");
                                        xmlNfeElement.InnerText = ICMSNormalST.ICMS_ICMS30_modBCST;
                                        XmlNode xmlNfeNodeDetImpostoICMSICMS30modBCST = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMS30.AppendChild(xmlNfeNodeDetImpostoICMSICMS30modBCST);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    else
                                    {

                                        return null;
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio pMVAST -- N19 - f-N05
                                    if (regrasNfe.verificaCampoVazio(ICMSNormalST.ICMS_ICMS30_pMVAST))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("pMVAST");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMS30_pMVAST, 2, 1, 8, false, null, "N19");
                                        XmlNode xmlNfeNodeDetImpostoICMSICMS30pMVAST = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMS30.AppendChild(xmlNfeNodeDetImpostoICMSICMS30pMVAST);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio pRedBCST -- N20 - f-N05
                                    if (regrasNfe.verificaCampoVazio(ICMSNormalST.ICMS_ICMS30_pRedBCST))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("pRedBCST");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMS30_pRedBCST, 2, 1, 8, false, null, "N20");
                                        XmlNode xmlNfeNodeDetImpostoICMSICMS30pRedBCST = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMS30.AppendChild(xmlNfeNodeDetImpostoICMSICMS30pRedBCST);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio vBCST -- N21 - f-N05
                                    xmlNfeElement = xmlNfe.CreateElement("vBCST");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMS30_vBCST, 2, 1, 16, false, null, "N21");
                                    XmlNode xmlNfeNodeDetImpostoICMSICMS30vBCST = xmlNfeElement;
                                    xmlNfeNodeDetImpostoICMSICMS30.AppendChild(xmlNfeNodeDetImpostoICMSICMS30vBCST);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio pICMSST -- N22 - f-N05
                                    xmlNfeElement = xmlNfe.CreateElement("pICMSST");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMS30_pICMSST, 2, 1, 8, false, null, "N22");
                                    XmlNode xmlNfeNodeDetImpostoICMSICMS30pICMSST = xmlNfeElement;
                                    xmlNfeNodeDetImpostoICMSICMS30.AppendChild(xmlNfeNodeDetImpostoICMSICMS30pICMSST);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio vICMSST -- N23 - f-N05
                                    xmlNfeElement = xmlNfe.CreateElement("vICMSST");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMS30_vICMSST, 2, 1, 16, false, null, "N23");
                                    XmlNode xmlNfeNodeDetImpostoICMSICMS30vICMSST = xmlNfeElement;
                                    xmlNfeNodeDetImpostoICMSICMS30.AppendChild(xmlNfeNodeDetImpostoICMSICMS30vICMSST);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim
                                    if (regrasNfe.verificaMotivoDesoneracaoIcms30(ICMSNormalST.ICMS_ICMS30_motDesICMS))
                                    {

                                        //// ---------------------------------------------------------------- inicio erproNFe -- N27.1 - f-N05
                                        //xmlNfeElement = xmlNfe.CreateElement("erproNFe");
                                        //XmlNode xmlNfeNodeXN27_1_2 = xmlNfeElement;
                                        //xmlNfeNodeDetImpostoICMSICMS30.AppendChild(xmlNfeNodeXN27_1_2);
                                        //xmlNfe.AppendChild(xmlNfeNode);
                                        ////----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio vICMSDeson -- N27a - f-N05
                                        xmlNfeElement = xmlNfe.CreateElement("vICMSDeson");
                                        xmlNfeElement.InnerText = ICMSNormalST.ICMS_ICMS30_vICMSDeson;
                                        XmlNode xmlNfeNodeXN27_1_2VICMSDeson = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMS30.AppendChild(xmlNfeNodeXN27_1_2VICMSDeson);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio motDesICMS -- N28 - f-N05
                                        xmlNfeElement = xmlNfe.CreateElement("motDesICMS");
                                        xmlNfeElement.InnerText = ICMSNormalST.ICMS_ICMS30_motDesICMS;
                                        XmlNode xmlNfeNodeXN27_1_2MotDesICMS = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMS30.AppendChild(xmlNfeNodeXN27_1_2MotDesICMS);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim
                                    }
                                }

                                if (regrasNfe.verificaIcms("40", ICMSNormalST.ICMS_ICMS40_CST) || regrasNfe.verificaIcms("41", ICMSNormalST.ICMS_ICMS40_CST) || regrasNfe.verificaIcms("50", ICMSNormalST.ICMS_ICMS40_CST))
                                {
                                    // ---------------------------------------------------------------- inicio ICMS40 -- N06 - f-N01
                                    xmlNfeElement = xmlNfe.CreateElement("ICMS40");
                                    XmlNode xmlNfeNodeDetImpostoICMSICMS40 = xmlNfeElement;
                                    xmlNfeNodeDetImpostoICMS.AppendChild(xmlNfeNodeDetImpostoICMSICMS40);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio orig -- N11 - f-N06
                                    if (regrasNfe.verificaOrigemMercadoriaIcms(ICMSNormalST.ICMS_ICMS40_orig))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("orig");
                                        xmlNfeElement.InnerText = ICMSNormalST.ICMS_ICMS40_orig;
                                        XmlNode xmlNfeNodeDetImpostoICMSICMS40orig = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMS40.AppendChild(xmlNfeNodeDetImpostoICMSICMS40orig);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    else
                                    {

                                        return null;
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio CST -- N12 - f-N06
                                    xmlNfeElement = xmlNfe.CreateElement("CST");
                                    xmlNfeElement.InnerText = ICMSNormalST.ICMS_ICMS40_CST;
                                    XmlNode xmlNfeNodeDetImpostoICMSICMS40CST = xmlNfeElement;
                                    xmlNfeNodeDetImpostoICMSICMS40.AppendChild(xmlNfeNodeDetImpostoICMSICMS40CST);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    if (regrasNfe.verificaMotivoDesoneracaoIcms40(ICMSNormalST.ICMS_ICMS40_motDesICMS))
                                    {
                                        //// ---------------------------------------------------------------- inicio erproNFe -- N27.1 - f-N06
                                        //xmlNfeElement = xmlNfe.CreateElement("erproNFe");
                                        //XmlNode xmlNfeNodeX127_1_3 = xmlNfeElement;
                                        //xmlNfeNodeDetImpostoICMSICMS40.AppendChild(xmlNfeNodeX127_1_3);
                                        //xmlNfe.AppendChild(xmlNfeNode);
                                        ////----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio vICMSDeson -- N27a - f-N06
                                        xmlNfeElement = xmlNfe.CreateElement("vICMSDeson");
                                        xmlNfeElement.InnerText = ICMSNormalST.ICMS_ICMS40_vICMSDeson;
                                        XmlNode xmlNfeNodeX127_1_3vICMSDeson = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMS40.AppendChild(xmlNfeNodeX127_1_3vICMSDeson);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio motDesICMS -- N28 - f-N06
                                        xmlNfeElement = xmlNfe.CreateElement("motDesICMS");
                                        xmlNfeElement.InnerText = ICMSNormalST.ICMS_ICMS40_motDesICMS;
                                        XmlNode xmlNfeNodeX127_1_3motDesICMS = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMS40.AppendChild(xmlNfeNodeX127_1_3motDesICMS);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim
                                    }
                                }

                                if (regrasNfe.verificaIcms("51", ICMSNormalST.ICMS_ICMS51_CST))
                                {
                                    // ---------------------------------------------------------------- inicio ICMS51 -- N07 - f-N01
                                    xmlNfeElement = xmlNfe.CreateElement("ICMS51");
                                    XmlNode xmlNfeNodeDetImpostoICMSICMS51 = xmlNfeElement;
                                    xmlNfeNodeDetImpostoICMS.AppendChild(xmlNfeNodeDetImpostoICMSICMS51);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio orig -- N11 - f-N07
                                    if (regrasNfe.verificaOrigemMercadoriaIcms(ICMSNormalST.ICMS_ICMS51_orig))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("orig");
                                        xmlNfeElement.InnerText = ICMSNormalST.ICMS_ICMS51_orig;
                                        XmlNode xmlNfeNodeDetImpostoICMSICMS51orig = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMS51.AppendChild(xmlNfeNodeDetImpostoICMSICMS51orig);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    else
                                    {

                                        return null;
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio CST -- N12 - f-N07
                                    xmlNfeElement = xmlNfe.CreateElement("CST");
                                    xmlNfeElement.InnerText = "51";
                                    XmlNode xmlNfeNodeDetImpostoICMSICMS51CST = xmlNfeElement;
                                    xmlNfeNodeDetImpostoICMSICMS51.AppendChild(xmlNfeNodeDetImpostoICMSICMS51CST);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio modBC -- N13 - f-N07
                                    if (regrasNfe.verificaCampoVazio(ICMSNormalST.ICMS_ICMS51_modBC))
                                    {
                                        if (regrasNfe.verificaModalidadeBaseCalculo(ICMSNormalST.ICMS_ICMS51_modBC))
                                        {
                                            xmlNfeElement = xmlNfe.CreateElement("modBC");
                                            xmlNfeElement.InnerText = ICMSNormalST.ICMS_ICMS51_modBC;
                                            XmlNode xmlNfeNodeDetImpostoICMSICMS51modBC = xmlNfeElement;
                                            xmlNfeNodeDetImpostoICMSICMS51.AppendChild(xmlNfeNodeDetImpostoICMSICMS51modBC);
                                            xmlNfe.AppendChild(xmlNfeNode);
                                        }
                                        else
                                        {

                                            return null;
                                        }
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio modBC -- N14 - f-N07
                                    if (regrasNfe.verificaCampoVazio(ICMSNormalST.ICMS_ICMS51_pRedBC))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("pRedBC");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMS51_pRedBC, 2, 1, 8, false, null, "N14");
                                        XmlNode xmlNfeNodeDetImpostoICMSICMS51pRedBC = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMS51.AppendChild(xmlNfeNodeDetImpostoICMSICMS51pRedBC);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio vBC -- N15 - f-N07
                                    if (regrasNfe.verificaCampoVazio(ICMSNormalST.ICMS_ICMS51_vBC))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("vBC");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMS51_vBC, 2, 1, 16, false, null, "N15");
                                        XmlNode xmlNfeNodeDetImpostoICMSICMS51vBC = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMS51.AppendChild(xmlNfeNodeDetImpostoICMSICMS51vBC);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio pICMS -- N16 - f-N07
                                    if (regrasNfe.verificaCampoVazio(ICMSNormalST.ICMS_ICMS51_pICMS))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("pICMS");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMS51_pICMS, 2, 1, 8, false, null, "N16");
                                        XmlNode xmlNfeNodeDetImpostoICMSICMS51pICMS = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMS51.AppendChild(xmlNfeNodeDetImpostoICMSICMS51pICMS);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio vICMSOp -- N16a - f-N07
                                    if (regrasNfe.verificaCampoVazio(ICMSNormalST.ICMS_ICMS51_vICMSOp))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("vICMSOp");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMS51_vICMSOp, 2, 1, 16, false, null, "N16a");
                                        XmlNode xmlNfeNodeDetImpostoICMSICMS51vICMSOp = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMS51.AppendChild(xmlNfeNodeDetImpostoICMSICMS51vICMSOp);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio pDif -- N16b - f-N07
                                    if (regrasNfe.verificaCampoVazio(ICMSNormalST.ICMS_ICMS51_pDif))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("pDif");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMS51_pDif, 2, 1, 8, false, null, "N16b");
                                        XmlNode xmlNfeNodeDetImpostoICMSICMS51pDif = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMS51.AppendChild(xmlNfeNodeDetImpostoICMSICMS51pDif);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio vICMSDif -- N16c - f-N07
                                    if (regrasNfe.verificaCampoVazio(ICMSNormalST.ICMS_ICMS51_vICMSDif))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("vICMSDif");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMS51_vICMSDif, 2, 1, 16, false, null, "N16c");
                                        XmlNode xmlNfeNodeDetImpostoICMSICMS51vICMSDif = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMS51.AppendChild(xmlNfeNodeDetImpostoICMSICMS51vICMSDif);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio vICMS -- N17 - f-N07
                                    if (regrasNfe.verificaCampoVazio(ICMSNormalST.ICMS_ICMS51_vICMS))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("vICMS");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMS51_vICMS, 2, 1, 16, false, null, "N17");
                                        XmlNode xmlNfeNodeDetImpostoICMSICMS51vICMS = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMS51.AppendChild(xmlNfeNodeDetImpostoICMSICMS51vICMS);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    //----------------------------------------------------------------- fim
                                }

                                if (regrasNfe.verificaIcms("60", ICMSNormalST.ICMS_ICMS60_CST))
                                {
                                    // ---------------------------------------------------------------- inicio ICMS60 -- N08 - f-N01
                                    xmlNfeElement = xmlNfe.CreateElement("ICMS60");
                                    XmlNode xmlNfeNodeDetImpostoICMSICMS60 = xmlNfeElement;
                                    xmlNfeNodeDetImpostoICMS.AppendChild(xmlNfeNodeDetImpostoICMSICMS60);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio orig -- N11 - f-N08
                                    if (regrasNfe.verificaOrigemMercadoriaIcms(ICMSNormalST.ICMS_ICMS60_orig))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("orig");
                                        xmlNfeElement.InnerText = ICMSNormalST.ICMS_ICMS60_orig;
                                        XmlNode xmlNfeNodeDetImpostoICMSICMS60orig = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMS60.AppendChild(xmlNfeNodeDetImpostoICMSICMS60orig);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    else
                                    {

                                        return null;
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio CST -- N12 - f-N08
                                    xmlNfeElement = xmlNfe.CreateElement("CST");
                                    xmlNfeElement.InnerText = "60";
                                    XmlNode xmlNfeNodeDetImpostoICMSICMS60CST = xmlNfeElement;
                                    xmlNfeNodeDetImpostoICMSICMS60.AppendChild(xmlNfeNodeDetImpostoICMSICMS60CST);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    if (!regrasNfe.verificaSequenciaIcms60(ICMSNormalST.ICMS_ICMS60_vBCSTRet, ICMSNormalST.ICMS_ICMS60_vICMSSTRet))
                                    {
                                        //// ---------------------------------------------------------------- inicio erproNFe -- N25.1 - f-N08
                                        //xmlNfeElement = xmlNfe.CreateElement("erproNFe");
                                        //XmlNode xmlNfeNodeX25_1 = xmlNfeElement;
                                        //xmlNfeNodeDetImpostoICMSICMS60.AppendChild(xmlNfeNodeX25_1);
                                        //xmlNfe.AppendChild(xmlNfeNode);
                                        ////----------------------------------------------------------------- 

                                        // ---------------------------------------------------------------- inicio vBCSTRet -- N26 - f-N08
                                        xmlNfeElement = xmlNfe.CreateElement("vBCSTRet");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMS60_vBCSTRet, 2, 1, 16, false, null, "N26");
                                        XmlNode xmlNfeNodeX25_1vBCSTRet = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMS60.AppendChild(xmlNfeNodeX25_1vBCSTRet);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio vICMSSTRet -- N27 - f-N08
                                        xmlNfeElement = xmlNfe.CreateElement("vICMSSTRet");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMS60_vICMSSTRet, 2, 1, 16, false, null, "N27");
                                        XmlNode xmlNfeNodeX25_1vICMSSTRet = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMS60.AppendChild(xmlNfeNodeX25_1vICMSSTRet);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim
                                    }
                                }

                                if (regrasNfe.verificaIcms("70", ICMSNormalST.ICMS_ICMS70_CST))
                                {
                                    // ---------------------------------------------------------------- inicio ICMS70 -- N09 - f-N01
                                    xmlNfeElement = xmlNfe.CreateElement("ICMS70");
                                    XmlNode xmlNfeNodeDetImpostoICMSICMS70 = xmlNfeElement;
                                    xmlNfeNodeDetImpostoICMS.AppendChild(xmlNfeNodeDetImpostoICMSICMS70);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio orig -- N11 - f-N09
                                    if (regrasNfe.verificaOrigemMercadoriaIcms(ICMSNormalST.ICMS_ICMS70_orig))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("orig");
                                        xmlNfeElement.InnerText = ICMSNormalST.ICMS_ICMS70_orig;
                                        XmlNode xmlNfeNodeDetImpostoICMSICMS70orig = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMS70.AppendChild(xmlNfeNodeDetImpostoICMSICMS70orig);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    else
                                    {

                                        return null;
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio CST -- N12 - f-N09
                                    xmlNfeElement = xmlNfe.CreateElement("CST");
                                    xmlNfeElement.InnerText = "70";
                                    XmlNode xmlNfeNodeDetImpostoICMSICMS70CST = xmlNfeElement;
                                    xmlNfeNodeDetImpostoICMSICMS70.AppendChild(xmlNfeNodeDetImpostoICMSICMS70CST);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio modBC -- N13 - f-N09
                                    if (regrasNfe.verificaModalidadeBaseCalculo(ICMSNormalST.ICMS_ICMS70_modBC))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("modBC");
                                        xmlNfeElement.InnerText = ICMSNormalST.ICMS_ICMS70_modBC;
                                        XmlNode xmlNfeNodeDetImpostoICMSICMS70modBC = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMS70.AppendChild(xmlNfeNodeDetImpostoICMSICMS70modBC);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    else
                                    {

                                        return null;
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio pRedBC -- N14 - f-N09
                                    xmlNfeElement = xmlNfe.CreateElement("pRedBC");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMS70_pRedBC, 2, 1, 8, false, null, "N14");
                                    XmlNode xmlNfeNodeDetImpostoICMSICMS70pRedBC = xmlNfeElement;
                                    xmlNfeNodeDetImpostoICMSICMS70.AppendChild(xmlNfeNodeDetImpostoICMSICMS70pRedBC);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio vBC -- N15 - f-N09
                                    xmlNfeElement = xmlNfe.CreateElement("vBC");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMS70_vBC, 2, 1, 16, false, null, "N15");
                                    XmlNode xmlNfeNodeDetImpostoICMSICMS70vBC = xmlNfeElement;
                                    xmlNfeNodeDetImpostoICMSICMS70.AppendChild(xmlNfeNodeDetImpostoICMSICMS70vBC);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio vBC -- N16 - f-N09
                                    xmlNfeElement = xmlNfe.CreateElement("pICMS");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMS70_pICMS, 2, 1, 8, false, null, "N16");
                                    XmlNode xmlNfeNodeDetImpostoICMSICMS70pICMS = xmlNfeElement;
                                    xmlNfeNodeDetImpostoICMSICMS70.AppendChild(xmlNfeNodeDetImpostoICMSICMS70pICMS);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio vICMS -- N17 - f-N09
                                    xmlNfeElement = xmlNfe.CreateElement("vICMS");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMS70_vICMS, 2, 1, 16, false, null, "N17");
                                    XmlNode xmlNfeNodeDetImpostoICMSICMS70vICMS = xmlNfeElement;
                                    xmlNfeNodeDetImpostoICMSICMS70.AppendChild(xmlNfeNodeDetImpostoICMSICMS70vICMS);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio modBCST -- N18 - f-N09
                                    if (regrasNfe.verificaModalidadeBaseCalculo(ICMSNormalST.ICMS_ICMS70_modBCST))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("modBCST");
                                        xmlNfeElement.InnerText = ICMSNormalST.ICMS_ICMS70_modBCST;
                                        XmlNode xmlNfeNodeDetImpostoICMSICMS70modBCST = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMS70.AppendChild(xmlNfeNodeDetImpostoICMSICMS70modBCST);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    else
                                    {

                                        return null;
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio pMVAST -- N19 - f-N09
                                    if (regrasNfe.verificaCampoVazio(ICMSNormalST.ICMS_ICMS70_pMVAST))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("pMVAST");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMS70_pMVAST, 2, 1, 8, false, null, "N19");
                                        XmlNode xmlNfeNodeDetImpostoICMSICMS70pMVAST = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMS70.AppendChild(xmlNfeNodeDetImpostoICMSICMS70pMVAST);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio pRedBCST -- N20 - f-N09
                                    if (regrasNfe.verificaCampoVazio(ICMSNormalST.ICMS_ICMS70_pMVAST))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("pRedBCST");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMS70_pRedBCST, 2, 1, 8, false, null, "N20");
                                        XmlNode xmlNfeNodeDetImpostoICMSICMS70pRedBCST = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMS70.AppendChild(xmlNfeNodeDetImpostoICMSICMS70pRedBCST);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio vBCST -- N21 - f-N09
                                    xmlNfeElement = xmlNfe.CreateElement("vBCST");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMS70_vBCST, 2, 1, 16, false, null, "N21");
                                    XmlNode xmlNfeNodeDetImpostoICMSICMS70vBCST = xmlNfeElement;
                                    xmlNfeNodeDetImpostoICMSICMS70.AppendChild(xmlNfeNodeDetImpostoICMSICMS70vBCST);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio pICMSST -- N22 - f-N09
                                    xmlNfeElement = xmlNfe.CreateElement("pICMSST");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMS70_pICMSST, 2, 1, 8, false, null, "N22");
                                    XmlNode xmlNfeNodeDetImpostoICMSICMS70pICMSST = xmlNfeElement;
                                    xmlNfeNodeDetImpostoICMSICMS70.AppendChild(xmlNfeNodeDetImpostoICMSICMS70pICMSST);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio vICMSST -- N23 - f-N09
                                    xmlNfeElement = xmlNfe.CreateElement("vICMSST");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMS70_vICMSST, 2, 1, 16, false, null, "N23");
                                    XmlNode xmlNfeNodeDetImpostoICMSICMS70vICMSST = xmlNfeElement;
                                    xmlNfeNodeDetImpostoICMSICMS70.AppendChild(xmlNfeNodeDetImpostoICMSICMS70vICMSST);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim
                                    if (regrasNfe.verificaSequenciaIcms70Desonerado(ICMSNormalST.ICMS_ICMS70_motDesICMS))
                                    {

                                        //// ---------------------------------------------------------------- inicio erproNFe -- N27.1 - f-N09
                                        //xmlNfeElement = xmlNfe.CreateElement("erproNFe");
                                        //XmlNode xmlNfeNodeXN27_1_3 = xmlNfeElement;
                                        //xmlNfeNodeDetImpostoICMSICMS70.AppendChild(xmlNfeNodeXN27_1_3);
                                        //xmlNfe.AppendChild(xmlNfeNode);
                                        ////----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio vICMSDeson -- N27a - f-N09
                                        xmlNfeElement = xmlNfe.CreateElement("vICMSDeson");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMS70_vICMSDeson, 2, 1, 16, false, null, "N27a");
                                        XmlNode xmlNfeNodeXN27_1_3vICMSDeson = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMS70.AppendChild(xmlNfeNodeXN27_1_3vICMSDeson);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio motDesICMS -- N28 - f-N09
                                        xmlNfeElement = xmlNfe.CreateElement("motDesICMS");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMS70_motDesICMS, 2, 1, 2, true, null, "N28");
                                        XmlNode xmlNfeNodeXN27_1_3motDesICMS = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMS70.AppendChild(xmlNfeNodeXN27_1_3motDesICMS);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim
                                    }
                                }

                                if (regrasNfe.verificaIcms("90", ICMSNormalST.ICMS_ICMS90_CST))
                                {
                                    // ---------------------------------------------------------------- inicio ICMS90 -- N10 - f-N01
                                    xmlNfeElement = xmlNfe.CreateElement("ICMS90");
                                    XmlNode xmlNfeNodeDetImpostoICMSICMS90 = xmlNfeElement;
                                    xmlNfeNodeDetImpostoICMS.AppendChild(xmlNfeNodeDetImpostoICMSICMS90);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio orig -- N11 - f-N10
                                    if (regrasNfe.verificaOrigemMercadoriaIcms(ICMSNormalST.ICMS_ICMS90_orig))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("orig");
                                        xmlNfeElement.InnerText = ICMSNormalST.ICMS_ICMS90_orig;
                                        XmlNode xmlNfeNodeDetImpostoICMSICMS90orig = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMS90.AppendChild(xmlNfeNodeDetImpostoICMSICMS90orig);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    else
                                    {

                                        return null;
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio CST -- N12 - f-N10
                                    xmlNfeElement = xmlNfe.CreateElement("CST");
                                    xmlNfeElement.InnerText = "90";
                                    XmlNode xmlNfeNodeDetImpostoICMSICMS90CST = xmlNfeElement;
                                    xmlNfeNodeDetImpostoICMSICMS90.AppendChild(xmlNfeNodeDetImpostoICMSICMS90CST);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    //// ---------------------------------------------------------------- inicio erproNFe -- N12.1 - f-N10
                                    //xmlNfeElement = xmlNfe.CreateElement("erproNFe");
                                    //XmlNode xmlNfeNodeX12_1 = xmlNfeElement;
                                    //xmlNfeNodeDetImpostoICMSICMS90.AppendChild(xmlNfeNodeX12_1);
                                    //xmlNfe.AppendChild(xmlNfeNode);
                                    ////----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio modBC -- N13 - f-N10
                                    if (regrasNfe.verificaModalidadeBaseCalculo(ICMSNormalST.ICMS_ICMS90_modBC))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("modBC");
                                        xmlNfeElement.InnerText = ICMSNormalST.ICMS_ICMS90_modBC;
                                        XmlNode xmlNfeNodeX12_1modBC = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMS90.AppendChild(xmlNfeNodeX12_1modBC);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    else
                                    {

                                        return null;
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio vBC -- N15 - f-N10
                                    xmlNfeElement = xmlNfe.CreateElement("vBC");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMS90_vBC, 2, 1, 16, false, null, "N15");
                                    XmlNode xmlNfeNodeX12_1vBC = xmlNfeElement;
                                    xmlNfeNodeDetImpostoICMSICMS90.AppendChild(xmlNfeNodeX12_1vBC);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio pRedBC -- N14 - f-N10
                                    if (regrasNfe.verificaCampoVazio(ICMSNormalST.ICMS_ICMS90_pRedBC))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("pRedBC");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMS90_pRedBC, 2, 1, 8, false, null, "N14");
                                        XmlNode xmlNfeNodeX12_1pRedBC = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMS90.AppendChild(xmlNfeNodeX12_1pRedBC);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio pICMS -- N16 - f-N10
                                    xmlNfeElement = xmlNfe.CreateElement("pICMS");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMS90_pICMS, 2, 1, 8, false, null, "N16");
                                    XmlNode xmlNfeNodeX12_1pICMS = xmlNfeElement;
                                    xmlNfeNodeDetImpostoICMSICMS90.AppendChild(xmlNfeNodeX12_1pICMS);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio vICMS -- N17 - f-N10
                                    xmlNfeElement = xmlNfe.CreateElement("vICMS");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMS90_vICMS, 2, 1, 16, false, null, "N17");
                                    XmlNode xmlNfeNodeX12_1vICMS = xmlNfeElement;
                                    xmlNfeNodeDetImpostoICMSICMS90.AppendChild(xmlNfeNodeX12_1vICMS);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    //// ---------------------------------------------------------------- inicio erproNFe -- N12.1 - f-N10
                                    //xmlNfeElement = xmlNfe.CreateElement("erproNFe");
                                    //XmlNode xmlNfeNodeX17_1 = xmlNfeElement;
                                    //xmlNfeNodeDetImpostoICMSICMS90.AppendChild(xmlNfeNodeX17_1);
                                    //xmlNfe.AppendChild(xmlNfeNode);
                                    ////----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio modBCST -- N18 - f-N10
                                    if (regrasNfe.verificaModalidadeBaseCalculoST(ICMSNormalST.ICMS_ICMS90_modBCST))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("modBCST");
                                        xmlNfeElement.InnerText = ICMSNormalST.ICMS_ICMS90_modBCST;
                                        XmlNode xmlNfeNodeX17_1modBCST = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMS90.AppendChild(xmlNfeNodeX17_1modBCST);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    else
                                    {

                                        return null;
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio pMVAST -- N19 - f-N10
                                    if (regrasNfe.verificaCampoVazio(ICMSNormalST.ICMS_ICMS90_pMVAST))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("pMVAST");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMS90_pMVAST, 2, 1, 8, false, null, "N19");
                                        XmlNode xmlNfeNodeX17_1pMVAST = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMS90.AppendChild(xmlNfeNodeX17_1pMVAST);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio pRedBCST -- N20 - f-N10
                                    if (regrasNfe.verificaCampoVazio(ICMSNormalST.ICMS_ICMS90_pRedBCST))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("pRedBCST");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMS90_pRedBCST, 2, 1, 8, false, null, "N20");
                                        XmlNode xmlNfeNodeX17_1pRedBCST = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMS90.AppendChild(xmlNfeNodeX17_1pRedBCST);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio vBCST -- N21 - f-N17.1
                                    xmlNfeElement = xmlNfe.CreateElement("vBCST");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMS90_vBCST, 2, 1, 16, false, null, "N21");
                                    XmlNode xmlNfeNodeX17_1vBCST = xmlNfeElement;
                                    xmlNfeNodeDetImpostoICMSICMS90.AppendChild(xmlNfeNodeX17_1vBCST);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio pICMSST -- N22 - f-N10
                                    xmlNfeElement = xmlNfe.CreateElement("pICMSST");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMS90_pICMSST, 2, 1, 8, false, null, "N22");
                                    XmlNode xmlNfeNodeX17_1pICMSST = xmlNfeElement;
                                    xmlNfeNodeDetImpostoICMSICMS90.AppendChild(xmlNfeNodeX17_1pICMSST);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio vICMSST -- N23 - f-N10
                                    xmlNfeElement = xmlNfe.CreateElement("vICMSST");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMS90_vICMSST, 2, 1, 16, false, null, "N23");
                                    XmlNode xmlNfeNodeX17_1vICMSST = xmlNfeElement;
                                    xmlNfeNodeDetImpostoICMSICMS90.AppendChild(xmlNfeNodeX17_1vICMSST);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    if (regrasNfe.verificaSequenciaIcms90Desonerado(ICMSNormalST.ICMS_ICMS90_motDesICMS))
                                    {
                                        //// ---------------------------------------------------------------- inicio erproNFe -- N27.1 - f-N10
                                        //xmlNfeElement = xmlNfe.CreateElement("erproNFe");
                                        //XmlNode xmlNfeNodeX27_1 = xmlNfeElement;
                                        //xmlNfeNodeDetImpostoICMSICMS90.AppendChild(xmlNfeNodeX27_1);
                                        //xmlNfe.AppendChild(xmlNfeNode);
                                        ////----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio vICMSDeson -- N27a - f-N10
                                        xmlNfeElement = xmlNfe.CreateElement("vICMSDeson");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMS90_vICMSDeson, 2, 1, 16, false, null, "N27a");
                                        XmlNode xmlNfeNodeX27_1vICMSDeson = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMS90.AppendChild(xmlNfeNodeX27_1vICMSDeson);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio motDesICMS -- N28 - f-N10
                                        xmlNfeElement = xmlNfe.CreateElement("motDesICMS");
                                        xmlNfeElement.InnerText = ICMSNormalST.ICMS_ICMS90_motDesICMS;
                                        XmlNode xmlNfeNodeX27_1motDesICMS = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMS90.AppendChild(xmlNfeNodeX27_1motDesICMS);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim
                                    }

                                    // ---------------------------------------------------------------- inicio ICMSPart -- N10a - f-N01
                                    xmlNfeElement = xmlNfe.CreateElement("ICMSPart");
                                    XmlNode xmlNfeNodeDetImpostoICMSICMSPart = xmlNfeElement;
                                    xmlNfeNodeDetImpostoICMS.AppendChild(xmlNfeNodeDetImpostoICMSICMSPart);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio orig -- N11 - f-N10a
                                    if (regrasNfe.verificaOrigemMercadoriaIcms(ICMSNormalST.ICMS_ICMSPart_orig))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("orig");
                                        xmlNfeElement.InnerText = ICMSNormalST.ICMS_ICMSPart_orig;
                                        XmlNode xmlNfeNodeDetImpostoICMSICMSPartOrig = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMSPart.AppendChild(xmlNfeNodeDetImpostoICMSICMSPartOrig);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    else
                                    {

                                        return null;
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio CST -- N12 - f-N10a
                                    if (regrasNfe.verificaTributacaoIcmsGrupoPartilha(ICMSNormalST.ICMS_ICMSPart_CST))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("CST");
                                        xmlNfeElement.InnerText = ICMSNormalST.ICMS_ICMSPart_CST;
                                        XmlNode xmlNfeNodeDetImpostoICMSICMSPartCST = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMSPart.AppendChild(xmlNfeNodeDetImpostoICMSICMSPartCST);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    else
                                    {

                                        return null;
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio modBC -- N13 - f-N10a
                                    if (regrasNfe.verificaModalidadeBaseCalculo(ICMSNormalST.ICMS_ICMSPart_modBC))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("modBC");
                                        xmlNfeElement.InnerText = ICMSNormalST.ICMS_ICMSPart_modBC;
                                        XmlNode xmlNfeNodeDetImpostoICMSICMSPartModBC = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMSPart.AppendChild(xmlNfeNodeDetImpostoICMSICMSPartModBC);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    else
                                    {

                                        return null;
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio vBC -- N15 - f-N10a
                                    xmlNfeElement = xmlNfe.CreateElement("vBC");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMSPart_vBC, 2, 1, 16, false, null, "N15");
                                    XmlNode xmlNfeNodeDetImpostoICMSICMSPartVBC = xmlNfeElement;
                                    xmlNfeNodeDetImpostoICMSICMSPart.AppendChild(xmlNfeNodeDetImpostoICMSICMSPartVBC);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio pRedBC -- N14 - f-N10a
                                    if (regrasNfe.verificaCampoVazio(ICMSNormalST.ICMS_ICMSPart_pRedBC))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("pRedBC");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMSPart_pRedBC, 2, 1, 8, false, null, "N14");
                                        XmlNode xmlNfeNodeDetImpostoICMSICMSPartPRedBC = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMSPart.AppendChild(xmlNfeNodeDetImpostoICMSICMSPartPRedBC);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio pICMS -- N16 - f-N10a
                                    xmlNfeElement = xmlNfe.CreateElement("pICMS");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMSPart_pICMS, 2, 1, 8, false, null, "N16");
                                    XmlNode xmlNfeNodeDetImpostoICMSICMSPartPICMS = xmlNfeElement;
                                    xmlNfeNodeDetImpostoICMSICMSPart.AppendChild(xmlNfeNodeDetImpostoICMSICMSPartPICMS);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio vICMS -- N17 - f-N10a
                                    xmlNfeElement = xmlNfe.CreateElement("vICMS");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMSPart_vICMS, 2, 1, 16, false, null, "N17");
                                    XmlNode xmlNfeNodeDetImpostoICMSICMSPartVICMS = xmlNfeElement;
                                    xmlNfeNodeDetImpostoICMSICMSPart.AppendChild(xmlNfeNodeDetImpostoICMSICMSPartVICMS);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio modBCST -- N18 - f-N10a
                                    if (regrasNfe.verificaModalidadeBaseCalculoST(ICMSNormalST.ICMS_ICMSPart_modBCST))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("modBCST");
                                        xmlNfeElement.InnerText = ICMSNormalST.ICMS_ICMSPart_modBCST;
                                        XmlNode xmlNfeNodeDetImpostoICMSICMSPartModBCST = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMSPart.AppendChild(xmlNfeNodeDetImpostoICMSICMSPartModBCST);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    else
                                    {

                                        return null;
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio pMVAST -- N19 - f-N10a
                                    if (regrasNfe.verificaCampoVazio(ICMSNormalST.ICMS_ICMSPart_pMVAST))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("pMVAST");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMSPart_pMVAST, 2, 1, 8, false, null, "N19");
                                        XmlNode xmlNfeNodeDetImpostoICMSICMSPartPMVAST = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMSPart.AppendChild(xmlNfeNodeDetImpostoICMSICMSPartPMVAST);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio pRedBCST -- N20 - f-N10a
                                    if (regrasNfe.verificaCampoVazio(ICMSNormalST.ICMS_ICMSPart_pRedBCST))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("pRedBCST");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMSPart_pRedBCST, 2, 1, 8, false, null, "N20");
                                        XmlNode xmlNfeNodeDetImpostoICMSICMSPartPRedBCST = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMSPart.AppendChild(xmlNfeNodeDetImpostoICMSICMSPartPRedBCST);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio vBCST -- N21 - f-N10a
                                    xmlNfeElement = xmlNfe.CreateElement("vBCST");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMSPart_vBCST, 2, 1, 16, false, null, "N21");
                                    XmlNode xmlNfeNodeDetImpostoICMSICMSPartVBCST = xmlNfeElement;
                                    xmlNfeNodeDetImpostoICMSICMSPart.AppendChild(xmlNfeNodeDetImpostoICMSICMSPartVBCST);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio pICMSST -- N22 - f-N10a
                                    xmlNfeElement = xmlNfe.CreateElement("pICMSST");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMSPart_pICMSST, 2, 1, 8, false, null, "N22");
                                    XmlNode xmlNfeNodeDetImpostoICMSICMSPartPICMSST = xmlNfeElement;
                                    xmlNfeNodeDetImpostoICMSICMSPart.AppendChild(xmlNfeNodeDetImpostoICMSICMSPartPICMSST);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio vICMSST -- N23 - f-N10a
                                    xmlNfeElement = xmlNfe.CreateElement("vICMSST");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMSPart_vICMSST, 2, 1, 16, false, null, "N23");
                                    XmlNode xmlNfeNodeDetImpostoICMSICMSPartVICMSST = xmlNfeElement;
                                    xmlNfeNodeDetImpostoICMSICMSPart.AppendChild(xmlNfeNodeDetImpostoICMSICMSPartVICMSST);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio pBCOp -- N25 - f-N10a
                                    xmlNfeElement = xmlNfe.CreateElement("pBCOp");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMSPart_pBCOp, 2, 1, 8, false, null, "N25");
                                    XmlNode xmlNfeNodeDetImpostoICMSICMSPartPBCOp = xmlNfeElement;
                                    xmlNfeNodeDetImpostoICMSICMSPart.AppendChild(xmlNfeNodeDetImpostoICMSICMSPartPBCOp);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio UFST -- N24 - f-N10a
                                    xmlNfeElement = xmlNfe.CreateElement("UFST");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMSPart_UFST, 2, 2, 2, false, null, "N24");
                                    XmlNode xmlNfeNodeDetImpostoICMSICMSPartUFST = xmlNfeElement;
                                    xmlNfeNodeDetImpostoICMSICMSPart.AppendChild(xmlNfeNodeDetImpostoICMSICMSPartUFST);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    if (regrasNfe.verificaIcms("41", ICMSNormalST.ICMS_ICMSST_CST))
                                    {
                                        // ---------------------------------------------------------------- inicio ICMSST -- N10b - f-N01
                                        xmlNfeElement = xmlNfe.CreateElement("ICMSST");
                                        XmlNode xmlNfeNodeDetImpostoICMSICMSST = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMS.AppendChild(xmlNfeNodeDetImpostoICMSICMSST);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio orig -- N11 - f-N10b
                                        if (regrasNfe.verificaOrigemMercadoriaIcms(ICMSNormalST.ICMS_ICMSST_orig))
                                        {
                                            xmlNfeElement = xmlNfe.CreateElement("orig");
                                            xmlNfeElement.InnerText = ICMSNormalST.ICMS_ICMSST_orig;
                                            XmlNode xmlNfeNodeDetImpostoICMSICMSSTorig = xmlNfeElement;
                                            xmlNfeNodeDetImpostoICMSICMSST.AppendChild(xmlNfeNodeDetImpostoICMSICMSSTorig);
                                            xmlNfe.AppendChild(xmlNfeNode);
                                        }
                                        else
                                        {

                                            return null;
                                        }
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio CST -- N12 - f-N10b
                                        xmlNfeElement = xmlNfe.CreateElement("CST");
                                        xmlNfeElement.InnerText = "41";
                                        XmlNode xmlNfeNodeDetImpostoICMSICMSSTCST = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMSST.AppendChild(xmlNfeNodeDetImpostoICMSICMSSTCST);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio vBCSTRet -- N26 - f-N10b
                                        xmlNfeElement = xmlNfe.CreateElement("vBCSTRet");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMSST_vBCSTRet, 2, 1, 16, false, null, "N26");
                                        XmlNode xmlNfeNodeDetImpostoICMSICMSSTvBCSTRet = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMSST.AppendChild(xmlNfeNodeDetImpostoICMSICMSSTvBCSTRet);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio vICMSSTRet -- N27 - f-N10b
                                        xmlNfeElement = xmlNfe.CreateElement("vICMSSTRet");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMSST_vICMSSTRet, 1, 1, 16, false, null, "N27");
                                        XmlNode xmlNfeNodeDetImpostoICMSICMSSTvICMSSTRet = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMSST.AppendChild(xmlNfeNodeDetImpostoICMSICMSSTvICMSSTRet);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio vBCSTDest -- N31 - f-N10b
                                        xmlNfeElement = xmlNfe.CreateElement("vBCSTDest");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMSST_vBCSTDest, 2, 1, 16, false, null, "N31");
                                        XmlNode xmlNfeNodeDetImpostoICMSICMSSTvBCSTDest = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMSST.AppendChild(xmlNfeNodeDetImpostoICMSICMSSTvBCSTDest);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio vICMSSTDest -- N32 - f-N10b
                                        xmlNfeElement = xmlNfe.CreateElement("vICMSSTDest");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMSST_vICMSSTDest, 2, 1, 16, false, null, "N32");
                                        XmlNode xmlNfeNodeDetImpostoICMSICMSSTvICMSSTDest = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMSST.AppendChild(xmlNfeNodeDetImpostoICMSICMSSTvICMSSTDest);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim
                                    }

                                    if (regrasNfe.verificaIcms("101", ICMSNormalST.ICMS_ICMSSN101_CSOSN))
                                    {
                                        // ---------------------------------------------------------------- inicio ICMSSN101 -- N10c - f-N01
                                        xmlNfeElement = xmlNfe.CreateElement("ICMSSN101");
                                        XmlNode xmlNfeNodeDetImpostoICMSICMSSN101 = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMS.AppendChild(xmlNfeNodeDetImpostoICMSICMSSN101);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio orig -- N11 - f-N10c
                                        if (regrasNfe.verificaOrigemMercadoriaIcms(ICMSNormalST.ICMS_ICMSSN101_orig))
                                        {
                                            xmlNfeElement = xmlNfe.CreateElement("orig");
                                            xmlNfeElement.InnerText = ICMSNormalST.ICMS_ICMSSN101_orig;
                                            XmlNode xmlNfeNodeDetImpostoICMSICMSSN101orig = xmlNfeElement;
                                            xmlNfeNodeDetImpostoICMSICMSSN101.AppendChild(xmlNfeNodeDetImpostoICMSICMSSN101orig);
                                            xmlNfe.AppendChild(xmlNfeNode);
                                        }
                                        else
                                        {

                                            return null;
                                        }
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio CSOSN -- N12a - f-N10c
                                        xmlNfeElement = xmlNfe.CreateElement("CSOSN");
                                        xmlNfeElement.InnerText = "101";
                                        XmlNode xmlNfeNodeDetImpostoICMSICMSSN101CSOSN = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMSSN101.AppendChild(xmlNfeNodeDetImpostoICMSICMSSN101CSOSN);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio pCredSN -- N29 - f-N10c
                                        xmlNfeElement = xmlNfe.CreateElement("pCredSN");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMSSN101_pCredSN, 2, 1, 8, false, null, "N29");
                                        XmlNode xmlNfeNodeDetImpostoICMSICMSSN101pCredSN = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMSSN101.AppendChild(xmlNfeNodeDetImpostoICMSICMSSN101pCredSN);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio pCredSN -- N30 - f-N10c
                                        xmlNfeElement = xmlNfe.CreateElement("vCredICMSSN");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMSSN101_vCredICMSSN, 2, 1, 16, false, null, "N30");
                                        XmlNode xmlNfeNodeDetImpostoICMSICMSSN101vCredICMSSN = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMSSN101.AppendChild(xmlNfeNodeDetImpostoICMSICMSSN101vCredICMSSN);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim
                                    }

                                    if (regrasNfe.verificaIcms("102", ICMSNormalST.ICMS_ICMSSN102_CSOSN))
                                    {
                                        // ---------------------------------------------------------------- inicio ICMSSN102 -- N10d - f-N01
                                        xmlNfeElement = xmlNfe.CreateElement("ICMSSN102");
                                        XmlNode xmlNfeNodeDetImpostoICMSICMSSN102 = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMS.AppendChild(xmlNfeNodeDetImpostoICMSICMSSN102);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio orig -- N11 - f-N10d
                                        if (regrasNfe.verificaOrigemMercadoriaIcms(ICMSNormalST.ICMS_ICMSSN102_orig))
                                        {
                                            xmlNfeElement = xmlNfe.CreateElement("orig");
                                            xmlNfeElement.InnerText = ICMSNormalST.ICMS_ICMSSN102_orig;
                                            XmlNode xmlNfeNodeDetImpostoICMSICMSSN102orig = xmlNfeElement;
                                            xmlNfeNodeDetImpostoICMSICMSSN102.AppendChild(xmlNfeNodeDetImpostoICMSICMSSN102orig);
                                            xmlNfe.AppendChild(xmlNfeNode);
                                        }
                                        else
                                        {

                                            return null;
                                        }
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio CSOSN -- N12a - f-N10d
                                        xmlNfeElement = xmlNfe.CreateElement("CSOSN");
                                        xmlNfeElement.InnerText = "102";
                                        XmlNode xmlNfeNodeDetImpostoICMSICMSSN102CSOSN = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMSSN102.AppendChild(xmlNfeNodeDetImpostoICMSICMSSN102CSOSN);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim
                                    }

                                    if (regrasNfe.verificaIcms("201", ICMSNormalST.ICMS_ICMSSN201_CSOSN))
                                    {
                                        // ---------------------------------------------------------------- inicio ICMSSN201 -- N10e - f-N01
                                        xmlNfeElement = xmlNfe.CreateElement("ICMSSN201");
                                        XmlNode xmlNfeNodeDetImpostoICMSICMSSN201 = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMS.AppendChild(xmlNfeNodeDetImpostoICMSICMSSN201);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio orig -- N11 - f-N10e
                                        xmlNfeElement = xmlNfe.CreateElement("orig");
                                        xmlNfeElement.InnerText = ICMSNormalST.ICMS_ICMSSN201_orig;
                                        XmlNode xmlNfeNodeDetImpostoICMSICMSSN201orig = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMSSN201.AppendChild(xmlNfeNodeDetImpostoICMSICMSSN201orig);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio CSOSN -- N12a - f-N10e
                                        xmlNfeElement = xmlNfe.CreateElement("CSOSN");
                                        xmlNfeElement.InnerText = ICMSNormalST.ICMS_ICMSSN201_CSOSN;
                                        XmlNode xmlNfeNodeDetImpostoICMSICMSSN201CSOSN = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMSSN201.AppendChild(xmlNfeNodeDetImpostoICMSICMSSN201CSOSN);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio modBCST -- N18 - f-N10e
                                        if (regrasNfe.verificaModalidadeBaseCalculoST(ICMSNormalST.ICMS_ICMSSN201_modBCST))
                                        {
                                            xmlNfeElement = xmlNfe.CreateElement("modBCST");
                                            xmlNfeElement.InnerText = ICMSNormalST.ICMS_ICMSSN201_modBCST;
                                            XmlNode xmlNfeNodeDetImpostoICMSICMSSN201modBCST = xmlNfeElement;
                                            xmlNfeNodeDetImpostoICMSICMSSN201.AppendChild(xmlNfeNodeDetImpostoICMSICMSSN201modBCST);
                                            xmlNfe.AppendChild(xmlNfeNode);
                                        }
                                        else
                                        {

                                            return null;
                                        }
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio pMVAST -- N19 - f-N10e
                                        if (regrasNfe.verificaCampoVazio(ICMSNormalST.ICMS_ICMSSN201_pMVAST))
                                        {
                                            xmlNfeElement = xmlNfe.CreateElement("pMVAST");
                                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMSSN201_pMVAST, 2, 1, 8, false, null, "N19");
                                            XmlNode xmlNfeNodeDetImpostoICMSICMSSN201pMVAST = xmlNfeElement;
                                            xmlNfeNodeDetImpostoICMSICMSSN201.AppendChild(xmlNfeNodeDetImpostoICMSICMSSN201pMVAST);
                                            xmlNfe.AppendChild(xmlNfeNode);
                                        }
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio pRedBCST -- N20 - f-N10e
                                        if (regrasNfe.verificaCampoVazio(ICMSNormalST.ICMS_ICMSSN201_pRedBCST))
                                        {
                                            xmlNfeElement = xmlNfe.CreateElement("pRedBCST");
                                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMSSN201_pRedBCST, 2, 1, 8, false, null, "N20");
                                            XmlNode xmlNfeNodeDetImpostoICMSICMSSN201pRedBCST = xmlNfeElement;
                                            xmlNfeNodeDetImpostoICMSICMSSN201.AppendChild(xmlNfeNodeDetImpostoICMSICMSSN201pRedBCST);
                                            xmlNfe.AppendChild(xmlNfeNode);
                                        }
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio vBCST -- N21 - f-N10e
                                        xmlNfeElement = xmlNfe.CreateElement("vBCST");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMSSN201_vBCST, 2, 1, 16, false, null, "N21");
                                        XmlNode xmlNfeNodeDetImpostoICMSICMSSN201vBCST = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMSSN201.AppendChild(xmlNfeNodeDetImpostoICMSICMSSN201vBCST);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio pICMSST -- N22 - f-N10e
                                        xmlNfeElement = xmlNfe.CreateElement("pICMSST");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMSSN201_pICMSST, 1, 1, 8, false, null, "N22");
                                        XmlNode xmlNfeNodeDetImpostoICMSICMSSN201pICMSST = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMSSN201.AppendChild(xmlNfeNodeDetImpostoICMSICMSSN201pICMSST);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio vICMSST -- N23 - f-N10e
                                        xmlNfeElement = xmlNfe.CreateElement("vICMSST");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMSSN201_vICMSST, 2, 1, 16, false, null, "N23");
                                        XmlNode xmlNfeNodeDetImpostoICMSICMSSN201vICMSST = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMSSN201.AppendChild(xmlNfeNodeDetImpostoICMSICMSSN201vICMSST);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio pCredSN -- N29 - f-N10e
                                        xmlNfeElement = xmlNfe.CreateElement("pCredSN");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMSSN201_pCredSN, 2, 1, 8, false, null, "N29");
                                        XmlNode xmlNfeNodeDetImpostoICMSICMSSN201pCredSN = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMSSN201.AppendChild(xmlNfeNodeDetImpostoICMSICMSSN201pCredSN);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio vCredICMSSN -- N30 - f-N10e
                                        xmlNfeElement = xmlNfe.CreateElement("vCredICMSSN");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMSSN201_vCredICMSSN, 2, 1, 16, false, null, "N30");
                                        XmlNode xmlNfeNodeDetImpostoICMSICMSSN201vCredICMSSN = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMSSN201.AppendChild(xmlNfeNodeDetImpostoICMSICMSSN201vCredICMSSN);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim
                                    }

                                    if (regrasNfe.verificaIcms("202", ICMSNormalST.ICMS_ICMSSN202_CSOSN) || regrasNfe.verificaIcms("203", ICMSNormalST.ICMS_ICMSSN202_CSOSN))
                                    {
                                        // ---------------------------------------------------------------- inicio ICMSSN202 -- N10f - f-N01
                                        xmlNfeElement = xmlNfe.CreateElement("ICMSSN202");
                                        XmlNode xmlNfeNodeDetImpostoICMSICMSSN202 = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMS.AppendChild(xmlNfeNodeDetImpostoICMSICMSSN202);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio orig -- N11 - f-N10f
                                        if (regrasNfe.verificaOrigemMercadoriaIcms(ICMSNormalST.ICMS_ICMSSN202_orig))
                                        {
                                            xmlNfeElement = xmlNfe.CreateElement("orig");
                                            xmlNfeElement.InnerText = ICMSNormalST.ICMS_ICMSSN202_orig;
                                            XmlNode xmlNfeNodeDetImpostoICMSICMSSN202orig = xmlNfeElement;
                                            xmlNfeNodeDetImpostoICMSICMSSN202.AppendChild(xmlNfeNodeDetImpostoICMSICMSSN202orig);
                                            xmlNfe.AppendChild(xmlNfeNode);
                                        }
                                        else
                                        {

                                            return null;
                                        }

                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio CSOSN -- N12a - f-N10f
                                        xmlNfeElement = xmlNfe.CreateElement("CSOSN");
                                        xmlNfeElement.InnerText = ICMSNormalST.ICMS_ICMSSN202_CSOSN;
                                        XmlNode xmlNfeNodeDetImpostoICMSICMSSN202CSOSN = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMSSN202.AppendChild(xmlNfeNodeDetImpostoICMSICMSSN202CSOSN);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio modBCST -- N18 - f-N10f
                                        if (regrasNfe.verificaModalidadeBaseCalculoST(ICMSNormalST.ICMS_ICMSSN202_modBCST))
                                        {
                                            xmlNfeElement = xmlNfe.CreateElement("modBCST");
                                            xmlNfeElement.InnerText = ICMSNormalST.ICMS_ICMSSN202_modBCST;
                                            XmlNode xmlNfeNodeDetImpostoICMSICMSSN202modBCST = xmlNfeElement;
                                            xmlNfeNodeDetImpostoICMSICMSSN202.AppendChild(xmlNfeNodeDetImpostoICMSICMSSN202modBCST);
                                            xmlNfe.AppendChild(xmlNfeNode);
                                        }
                                        else
                                        {

                                            return null;
                                        }
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio pMVAST -- N19 - f-N10f
                                        if (regrasNfe.verificaCampoVazio(ICMSNormalST.ICMS_ICMSSN202_pMVAST))
                                        {
                                            xmlNfeElement = xmlNfe.CreateElement("pMVAST");
                                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMSSN202_pMVAST, 2, 1, 8, false, null, "N19");
                                            XmlNode xmlNfeNodeDetImpostoICMSICMSSN202pMVAST = xmlNfeElement;
                                            xmlNfeNodeDetImpostoICMSICMSSN202.AppendChild(xmlNfeNodeDetImpostoICMSICMSSN202pMVAST);
                                            xmlNfe.AppendChild(xmlNfeNode);
                                        }
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio pRedBCST -- N20 - f-N10f
                                        if (regrasNfe.verificaCampoVazio(ICMSNormalST.ICMS_ICMSSN202_pRedBCST))
                                        {
                                            xmlNfeElement = xmlNfe.CreateElement("pRedBCST");
                                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMSSN202_pRedBCST, 2, 1, 8, false, null, "N20");
                                            XmlNode xmlNfeNodeDetImpostoICMSICMSSN202pRedBCST = xmlNfeElement;
                                            xmlNfeNodeDetImpostoICMSICMSSN202.AppendChild(xmlNfeNodeDetImpostoICMSICMSSN202pRedBCST);
                                            xmlNfe.AppendChild(xmlNfeNode);
                                        }
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio vBCST -- N21 - f-N10f
                                        xmlNfeElement = xmlNfe.CreateElement("vBCST");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMSSN202_vBCST, 2, 1, 16, false, null, "N21");
                                        XmlNode xmlNfeNodeDetImpostoICMSICMSSN202vBCST = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMSSN202.AppendChild(xmlNfeNodeDetImpostoICMSICMSSN202vBCST);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio pICMSST -- N22 - f-N10f
                                        xmlNfeElement = xmlNfe.CreateElement("pICMSST");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMSSN202_pICMSST, 2, 1, 8, false, null, "N22");
                                        XmlNode xmlNfeNodeDetImpostoICMSICMSSN202pICMSST = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMSSN202.AppendChild(xmlNfeNodeDetImpostoICMSICMSSN202pICMSST);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio vICMSST -- N23 - f-N10f
                                        xmlNfeElement = xmlNfe.CreateElement("vICMSST");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMSSN202_vICMSST, 2, 1, 16, false, null, "N23");
                                        XmlNode xmlNfeNodeDetImpostoICMSICMSSN202vICMSST = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMSSN202.AppendChild(xmlNfeNodeDetImpostoICMSICMSSN202vICMSST);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim
                                    }

                                    if (regrasNfe.verificaIcms("500", ICMSNormalST.ICMS_ICMSSN500_CSOSN))
                                        // ---------------------------------------------------------------- inicio ICMSSN500 -- N10g - f-N01
                                        xmlNfeElement = xmlNfe.CreateElement("ICMSSN500");
                                    XmlNode xmlNfeNodeDetImpostoICMSICMSSN500 = xmlNfeElement;
                                    xmlNfeNodeDetImpostoICMS.AppendChild(xmlNfeNodeDetImpostoICMSICMSSN500);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio orig -- N11 - f-N10g
                                    if (regrasNfe.verificaOrigemMercadoriaIcms(ICMSNormalST.ICMS_ICMSSN500_orig))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("orig");
                                        xmlNfeElement.InnerText = ICMSNormalST.ICMS_ICMSSN500_orig;
                                        XmlNode xmlNfeNodeDetImpostoICMSICMSSN500orig = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMSSN500.AppendChild(xmlNfeNodeDetImpostoICMSICMSSN500orig);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    else
                                    {

                                        return null;
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio orig -- N12a - f-N10g
                                    xmlNfeElement = xmlNfe.CreateElement("CSOSN");
                                    xmlNfeElement.InnerText = "500";
                                    XmlNode xmlNfeNodeDetImpostoICMSICMSSN500CSOSN = xmlNfeElement;
                                    xmlNfeNodeDetImpostoICMSICMSSN500.AppendChild(xmlNfeNodeDetImpostoICMSICMSSN500CSOSN);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    //// ---------------------------------------------------------------- inicio erproNFe -- N25.1 - f-N10g
                                    //xmlNfeElement = xmlNfe.CreateElement("erproNFe");
                                    //XmlNode xmlNfeNodeXN25_1 = xmlNfeElement;
                                    //xmlNfeNodeDetImpostoICMSICMSSN500.AppendChild(xmlNfeNodeXN25_1);
                                    //xmlNfe.AppendChild(xmlNfeNode);
                                    ////----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio vBCSTRet -- N126 - f-N10g
                                    xmlNfeElement = xmlNfe.CreateElement("vBCSTRet");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMSSN500_vBCSTRet, 2, 1, 16, false, null, "N26");
                                    XmlNode xmlNfeNodeXN25_1vBCSTRet = xmlNfeElement;
                                    xmlNfeNodeDetImpostoICMSICMSSN500.AppendChild(xmlNfeNodeXN25_1vBCSTRet);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio vICMSSTRet -- N27 - f-N10g
                                    xmlNfeElement = xmlNfe.CreateElement("vICMSSTRet");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMSSN500_vICMSSTRet, 2, 1, 16, false, null, "N27");
                                    XmlNode xmlNfeNodeXN25_1vICMSSTRet = xmlNfeElement;
                                    xmlNfeNodeDetImpostoICMSICMSSN500.AppendChild(xmlNfeNodeXN25_1vICMSSTRet);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    if (regrasNfe.verificaIcms("900", ICMSNormalST.ICMS_ICMSSN900_CSOSN))
                                    {
                                        // ---------------------------------------------------------------- inicio ICMSSN900 -- N10h - f-N01
                                        xmlNfeElement = xmlNfe.CreateElement("ICMSSN900");
                                        XmlNode xmlNfeNodeDetImpostoICMSICMSSN900 = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMS.AppendChild(xmlNfeNodeDetImpostoICMSICMSSN900);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio orig -- N11 - f-N10h
                                        if (regrasNfe.verificaOrigemMercadoriaIcms(ICMSNormalST.ICMS_ICMSSN900_orig))
                                        {
                                            xmlNfeElement = xmlNfe.CreateElement("orig");
                                            xmlNfeElement.InnerText = ICMSNormalST.ICMS_ICMSSN900_orig;
                                            XmlNode xmlNfeNodeDetImpostoICMSICMSSN900orig = xmlNfeElement;
                                            xmlNfeNodeDetImpostoICMSICMSSN900.AppendChild(xmlNfeNodeDetImpostoICMSICMSSN900orig);
                                            xmlNfe.AppendChild(xmlNfeNode);
                                        }
                                        else
                                        {

                                            return null;
                                        }
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio CSOSN -- N12a - f-N10h
                                        xmlNfeElement = xmlNfe.CreateElement("CSOSN");
                                        xmlNfeElement.InnerText = "900";
                                        XmlNode xmlNfeNodeDetImpostoICMSICMSSN900CSOSN = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMSSN900.AppendChild(xmlNfeNodeDetImpostoICMSICMSSN900CSOSN);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim

                                        //// ---------------------------------------------------------------- inicio erproNFe -- N17.1 - f-N10h
                                        //xmlNfeElement = xmlNfe.CreateElement("erproNFe");
                                        //XmlNode xmlNfeNodeX17_1_1 = xmlNfeElement;
                                        //xmlNfeNodeDetImpostoICMSICMSSN900.AppendChild(xmlNfeNodeX17_1_1);
                                        //xmlNfe.AppendChild(xmlNfeNode);
                                        ////----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio modBC -- N13 - f-N10h
                                        if (regrasNfe.verificaModalidadeBaseCalculo(ICMSNormalST.ICMS_ICMSSN900_modBC))
                                        {
                                            xmlNfeElement = xmlNfe.CreateElement("modBC");
                                            xmlNfeElement.InnerText = ICMSNormalST.ICMS_ICMSSN900_modBC;
                                            XmlNode xmlNfeNodeX17_1_1modBC = xmlNfeElement;
                                            xmlNfeNodeDetImpostoICMSICMSSN900.AppendChild(xmlNfeNodeX17_1_1modBC);
                                            xmlNfe.AppendChild(xmlNfeNode);
                                        }
                                        else
                                        {

                                            return null;
                                        }
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio vBC -- N15 - f-N10h
                                        xmlNfeElement = xmlNfe.CreateElement("vBC");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMSSN900_vBC, 2, 1, 16, false, null, "N15");
                                        XmlNode xmlNfeNodeX17_1_1vBC = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMSSN900.AppendChild(xmlNfeNodeX17_1_1vBC);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio pRedBC -- N14 - f-N10h
                                        if (regrasNfe.verificaCampoVazio(ICMSNormalST.ICMS_ICMSSN900_pRedBC))
                                        {
                                            xmlNfeElement = xmlNfe.CreateElement("pRedBC");
                                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMSSN900_pRedBC, 2, 1, 8, false, null, "N14");
                                            XmlNode xmlNfeNodeX17_1_1pRedBC = xmlNfeElement;
                                            xmlNfeNodeDetImpostoICMSICMSSN900.AppendChild(xmlNfeNodeX17_1_1pRedBC);
                                            xmlNfe.AppendChild(xmlNfeNode);
                                        }
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio pICMS -- N16 - f-N10h
                                        xmlNfeElement = xmlNfe.CreateElement("pICMS");
                                        xmlNfeElement.InnerText = ICMSNormalST.ICMS_ICMSSN900_pICMS;
                                        XmlNode xmlNfeNodeX17_1_1pICMS = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMSSN900.AppendChild(xmlNfeNodeX17_1_1pICMS);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio vICMS -- N17 - f-N10h
                                        xmlNfeElement = xmlNfe.CreateElement("vICMS");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMSSN900_vICMS, 2, 1, 8, false, null, "N17");
                                        XmlNode xmlNfeNodeX17_1_1vICMS = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMSSN900.AppendChild(xmlNfeNodeX17_1_1vICMS);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim

                                        //// ---------------------------------------------------------------- inicio erproNFe -- N17.1 - f-N10h
                                        //xmlNfeElement = xmlNfe.CreateElement("erproNFe");
                                        //XmlNode xmlNfeNodeX17_1_2 = xmlNfeElement;
                                        //xmlNfeNodeDetImpostoICMSICMSSN900.AppendChild(xmlNfeNodeX17_1_2);
                                        //xmlNfe.AppendChild(xmlNfeNode);
                                        ////----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio modBCST -- N18 - f-N10h
                                        if (regrasNfe.verificaModalidadeBaseCalculoST(ICMSNormalST.ICMS_ICMSSN900_modBCST))
                                        {
                                            xmlNfeElement = xmlNfe.CreateElement("modBCST");
                                            xmlNfeElement.InnerText = ICMSNormalST.ICMS_ICMSSN900_modBCST;
                                            XmlNode xmlNfeNodeX17_1_2modBCST = xmlNfeElement;
                                            xmlNfeNodeDetImpostoICMSICMSSN900.AppendChild(xmlNfeNodeX17_1_2modBCST);
                                            xmlNfe.AppendChild(xmlNfeNode);
                                        }
                                        else
                                        {

                                            return null;
                                        }
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio pMVAST -- N19 - f-N10h
                                        if (regrasNfe.verificaCampoVazio(ICMSNormalST.ICMS_ICMSSN900_pMVAST))
                                        {
                                            xmlNfeElement = xmlNfe.CreateElement("pMVAST");
                                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMSSN900_pMVAST, 2, 1, 8, false, null, "N19");
                                            XmlNode xmlNfeNodeX17_1_2pMVAST = xmlNfeElement;
                                            xmlNfeNodeDetImpostoICMSICMSSN900.AppendChild(xmlNfeNodeX17_1_2pMVAST);
                                            xmlNfe.AppendChild(xmlNfeNode);
                                        }
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio pRedBCST -- N20 - f-N10h
                                        if (regrasNfe.verificaCampoVazio(ICMSNormalST.ICMS_ICMSSN900_pRedBCST))
                                        {
                                            xmlNfeElement = xmlNfe.CreateElement("pRedBCST");
                                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMSSN900_pRedBCST, 2, 1, 8, false, null, "N20");
                                            XmlNode xmlNfeNodeX17_1_2pRedBCST = xmlNfeElement;
                                            xmlNfeNodeDetImpostoICMSICMSSN900.AppendChild(xmlNfeNodeX17_1_2pRedBCST);
                                            xmlNfe.AppendChild(xmlNfeNode);
                                        }
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio vBCST -- N21 - f-N10h
                                        xmlNfeElement = xmlNfe.CreateElement("vBCST");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMSSN900_vBCST, 2, 1, 16, false, null, "N21");
                                        XmlNode xmlNfeNodeX17_1_2vBCST = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMSSN900.AppendChild(xmlNfeNodeX17_1_2vBCST);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio pICMSST -- N22 - f-N10h
                                        xmlNfeElement = xmlNfe.CreateElement("pICMSST");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMSSN900_pICMSST, 2, 1, 8, false, null, "N22");
                                        XmlNode xmlNfeNodeX17_1_2pICMSST = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMSSN900.AppendChild(xmlNfeNodeX17_1_2pICMSST);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio vICMSST -- N23 - f-N10h
                                        xmlNfeElement = xmlNfe.CreateElement("vICMSST");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMSSN900_vICMSST, 2, 1, 16, false, null, "N23");
                                        XmlNode xmlNfeNodeX17_1_2vICMSST = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMSSN900.AppendChild(xmlNfeNodeX17_1_2vICMSST);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim

                                        //// ---------------------------------------------------------------- inicio erproNFe -- N27.1 - f-N10h
                                        //xmlNfeElement = xmlNfe.CreateElement("erproNFe");
                                        //XmlNode xmlNfeNodeX27_1_1 = xmlNfeElement;
                                        //xmlNfeNodeDetImpostoICMSICMSSN900.AppendChild(xmlNfeNodeX27_1_1);
                                        //xmlNfe.AppendChild(xmlNfeNode);
                                        ////----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio pCredSN -- N29 - f-N10h
                                        xmlNfeElement = xmlNfe.CreateElement("pCredSN");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMSSN900_pCredSN, 2, 1, 8, false, null, "N29");
                                        XmlNode xmlNfeNodeX27_1_1pCredSN = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMSSN900.AppendChild(xmlNfeNodeX27_1_1pCredSN);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio vCredICMSSN -- N30 - f-N10h
                                        xmlNfeElement = xmlNfe.CreateElement("vCredICMSSN");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ICMSNormalST.ICMS_ICMSSN900_vCredICMSSN, 2, 1, 16, false, null, "N30");
                                        XmlNode xmlNfeNodeX27_1_1vCredICMSSN = xmlNfeElement;
                                        xmlNfeNodeDetImpostoICMSICMSSN900.AppendChild(xmlNfeNodeX27_1_1vCredICMSSN);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim
                                    }
                                }
                            }
                            #endregion

                            #region #O - Imposto sobre Produtos Industrializados
                            if (regrasNfe.verificaIssqn(listaISSQN))
                            {
                                if (regrasNfe.verificaTipoModelo55NFe(identificacaoNfe.ide_mod))
                                {
                                    if (listaImpostoProdutosIndustrializados.Count > 0)
                                    {
                                        ImpostoProdutosIndustrializados = listaImpostoProdutosIndustrializados[j];
                                        if (regrasNfe.verificaIpiInformado(ImpostoProdutosIndustrializados))
                                        {
                                            // ---------------------------------------------------------------- inicio imposto -- O01 - M01
                                            xmlNfeElement = xmlNfe.CreateElement("IPI");
                                            XmlNode xmlNfeNodeDetImpostoIPI = xmlNfeElement;
                                            xmlNfeNodeDetImposto.AppendChild(xmlNfeNodeDetImpostoIPI);
                                            xmlNfe.AppendChild(xmlNfeNode);
                                            //----------------------------------------------------------------- fim

                                            // ---------------------------------------------------------------- inicio clEnq -- O02 - O01
                                            if (regrasNfe.verificaCampoVazio(ImpostoProdutosIndustrializados.IPI_clEnq))
                                            {
                                                xmlNfeElement = xmlNfe.CreateElement("clEnq");
                                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ImpostoProdutosIndustrializados.IPI_clEnq, 2, 1, 5, false, null, "O02");
                                                XmlNode xmlNfeNodeDetImpostoIPIclEnq = xmlNfeElement;
                                                xmlNfeNodeDetImpostoIPI.AppendChild(xmlNfeNodeDetImpostoIPIclEnq);
                                                xmlNfe.AppendChild(xmlNfeNode);
                                            }
                                            //----------------------------------------------------------------- fim

                                            // ---------------------------------------------------------------- inicio CNPJProd -- O03 - O01
                                            if (regrasNfe.verificaCampoVazio(ImpostoProdutosIndustrializados.IPI_CNPJProd))
                                            {
                                                xmlNfeElement = xmlNfe.CreateElement("CNPJProd");
                                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ImpostoProdutosIndustrializados.IPI_CNPJProd, 2, 14, 14, false, null, "O03");
                                                XmlNode xmlNfeNodeDetImpostoIPICNPJProd = xmlNfeElement;
                                                xmlNfeNodeDetImpostoIPI.AppendChild(xmlNfeNodeDetImpostoIPICNPJProd);
                                                xmlNfe.AppendChild(xmlNfeNode);
                                            }
                                            //----------------------------------------------------------------- fim

                                            // ---------------------------------------------------------------- inicio cSelo -- O04 - O01
                                            if (regrasNfe.verificaCampoVazio(ImpostoProdutosIndustrializados.IPI_cSelo))
                                            {
                                                xmlNfeElement = xmlNfe.CreateElement("cSelo");
                                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ImpostoProdutosIndustrializados.IPI_cSelo, 2, 1, 60, false, null, "O04");
                                                XmlNode xmlNfeNodeDetImpostoIPIcSelo = xmlNfeElement;
                                                xmlNfeNodeDetImpostoIPI.AppendChild(xmlNfeNodeDetImpostoIPIcSelo);
                                                xmlNfe.AppendChild(xmlNfeNode);
                                            }
                                            //----------------------------------------------------------------- fim

                                            // ---------------------------------------------------------------- inicio qSelo -- O05 - O01
                                            if (regrasNfe.verificaCampoVazio(ImpostoProdutosIndustrializados.IPI_qSelo))
                                            {
                                                xmlNfeElement = xmlNfe.CreateElement("qSelo");
                                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ImpostoProdutosIndustrializados.IPI_qSelo, 2, 1, 12, false, null, "O05");
                                                XmlNode xmlNfeNodeDetImpostoIPIqSelo = xmlNfeElement;
                                                xmlNfeNodeDetImpostoIPI.AppendChild(xmlNfeNodeDetImpostoIPIqSelo);
                                                xmlNfe.AppendChild(xmlNfeNode);
                                            }
                                            //----------------------------------------------------------------- fim

                                            // ---------------------------------------------------------------- inicio cEnq -- O06 - O01
                                            xmlNfeElement = xmlNfe.CreateElement("cEnq");
                                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ImpostoProdutosIndustrializados.IPI_cEnq, 2, 1, 3, false, null, "Ò06");
                                            XmlNode xmlNfeNodeDetImpostoIPIcEnq = xmlNfeElement;
                                            xmlNfeNodeDetImpostoIPI.AppendChild(xmlNfeNodeDetImpostoIPIcEnq);
                                            xmlNfe.AppendChild(xmlNfeNode);
                                            //----------------------------------------------------------------- fim

                                            if (regrasNfe.verificaGrupoCstIpiTributado(ImpostoProdutosIndustrializados.IPI_IPITrib_CST))
                                            {
                                                // ---------------------------------------------------------------- inicio IPITrib -- O07 - O01
                                                xmlNfeElement = xmlNfe.CreateElement("IPITrib");
                                                XmlNode xmlNfeNodeDetImpostoIPIIPITrib = xmlNfeElement;
                                                xmlNfeNodeDetImpostoIPI.AppendChild(xmlNfeNodeDetImpostoIPIIPITrib);
                                                xmlNfe.AppendChild(xmlNfeNode);
                                                //----------------------------------------------------------------- fim

                                                // ---------------------------------------------------------------- inicio CST -- O09 - O07
                                                xmlNfeElement = xmlNfe.CreateElement("CST");
                                                xmlNfeElement.InnerText = ImpostoProdutosIndustrializados.IPI_IPITrib_CST;
                                                XmlNode xmlNfeNodeDetImpostoIPIIPITribCST = xmlNfeElement;
                                                xmlNfeNodeDetImpostoIPIIPITrib.AppendChild(xmlNfeNodeDetImpostoIPIIPITribCST);
                                                xmlNfe.AppendChild(xmlNfeNode);
                                                //----------------------------------------------------------------- fim

                                                //// ---------------------------------------------------------------- inicio erproNFe -- O09.1 - O07
                                                //xmlNfeElement = xmlNfe.CreateElement("erproNFe");
                                                //XmlNode xmlNfeNodeXO09_1 = xmlNfeElement;
                                                //xmlNfeNodeDetImpostoIPIIPITrib.AppendChild(xmlNfeNodeXO09_1);
                                                //xmlNfe.AppendChild(xmlNfeNode);
                                                ////----------------------------------------------------------------- fim

                                                // ---------------------------------------------------------------- inicio vBC -- O10 - O07
                                                xmlNfeElement = xmlNfe.CreateElement("vBC");
                                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ImpostoProdutosIndustrializados.IPI_IPITrib_vBC, 2, 1, 16, false, null, "O10");
                                                XmlNode xmlNfeNodeXO09_1vBC = xmlNfeElement;
                                                xmlNfeNodeDetImpostoIPIIPITrib.AppendChild(xmlNfeNodeXO09_1vBC);
                                                xmlNfe.AppendChild(xmlNfeNode);
                                                //----------------------------------------------------------------- fim

                                                // ---------------------------------------------------------------- inicio pIPI -- O13 - O07
                                                xmlNfeElement = xmlNfe.CreateElement("pIPI");
                                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ImpostoProdutosIndustrializados.IPI_IPITrib_vIPI, 2, 1, 8, false, null, "O13");
                                                XmlNode xmlNfeNodeXO09_1pIPI = xmlNfeElement;
                                                xmlNfeNodeDetImpostoIPIIPITrib.AppendChild(xmlNfeNodeXO09_1pIPI);
                                                xmlNfe.AppendChild(xmlNfeNode);
                                                //----------------------------------------------------------------- fim

                                                //// ---------------------------------------------------------------- inicio erproNFe -- O13.1 - O07
                                                //xmlNfeElement = xmlNfe.CreateElement("erproNFe");
                                                //XmlNode xmlNfeNodeXO13_1 = xmlNfeElement;
                                                //xmlNfeNodeDetImpostoIPIIPITrib.AppendChild(xmlNfeNodeXO13_1);
                                                //xmlNfe.AppendChild(xmlNfeNode);
                                                ////----------------------------------------------------------------- fim

                                                // ---------------------------------------------------------------- inicio qUnid -- O11 - O07
                                                xmlNfeElement = xmlNfe.CreateElement("qUnid");
                                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ImpostoProdutosIndustrializados.IPI_IPITrib_qUnid, 2, 1, 16, false, null, "O11");
                                                XmlNode xmlNfeNodeXO13_1qUnid = xmlNfeElement;
                                                xmlNfeNodeDetImpostoIPIIPITrib.AppendChild(xmlNfeNodeXO13_1qUnid);
                                                xmlNfe.AppendChild(xmlNfeNode);
                                                //----------------------------------------------------------------- fim

                                                // ---------------------------------------------------------------- inicio vUnid -- O12 - O07
                                                xmlNfeElement = xmlNfe.CreateElement("vUnid");
                                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ImpostoProdutosIndustrializados.IPI_IPITrib_vUnid, 2, 1, 15, false, null, "O12");
                                                XmlNode xmlNfeNodeXO13_1vUnid = xmlNfeElement;
                                                xmlNfeNodeDetImpostoIPIIPITrib.AppendChild(xmlNfeNodeXO13_1vUnid);
                                                xmlNfe.AppendChild(xmlNfeNode);
                                                //----------------------------------------------------------------- fim

                                                // ---------------------------------------------------------------- inicio vIPI -- O14 - O07
                                                xmlNfeElement = xmlNfe.CreateElement("vIPI");
                                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ImpostoProdutosIndustrializados.IPI_IPITrib_vIPI, 2, 1, 16, false, null, "O14");
                                                XmlNode xmlNfeNodeXO13_1vIPI = xmlNfeElement;
                                                xmlNfeNodeDetImpostoIPIIPITrib.AppendChild(xmlNfeNodeXO13_1vIPI);
                                                xmlNfe.AppendChild(xmlNfeNode);
                                                //----------------------------------------------------------------- fim
                                            }

                                            if (regrasNfe.verificaGrupoCstIpiNt(ImpostoProdutosIndustrializados.IPI_IPINT_CST))
                                            {
                                                // ---------------------------------------------------------------- inicio IPINT -- O08 - O01
                                                xmlNfeElement = xmlNfe.CreateElement("IPINT");
                                                XmlNode xmlNfeNodeDetImpostoIPIIPINT = xmlNfeElement;
                                                xmlNfeNodeDetImpostoIPI.AppendChild(xmlNfeNodeDetImpostoIPIIPINT);
                                                xmlNfe.AppendChild(xmlNfeNode);
                                                //----------------------------------------------------------------- fim

                                                // ---------------------------------------------------------------- inicio CST -- O09 - O08
                                                xmlNfeElement = xmlNfe.CreateElement("CST");
                                                xmlNfeElement.InnerText = ImpostoProdutosIndustrializados.IPI_IPINT_CST;
                                                XmlNode xmlNfeNodeDetImpostoIPICST = xmlNfeElement;
                                                xmlNfeNodeDetImpostoIPI.AppendChild(xmlNfeNodeDetImpostoIPICST);
                                                xmlNfe.AppendChild(xmlNfeNode);
                                                //----------------------------------------------------------------- fim
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion

                            #region #P - Imposto de Importação
                            if (regrasNfe.verificaIssqn(listaISSQN))
                            {
                                if (regrasNfe.verificaTipoModelo55NFe(identificacaoNfe.ide_mod))
                                {
                                    if (listaImpostoImportacao.Count > 0)
                                    {
                                        ImpostoImportacao = listaImpostoImportacao[j];
                                        if (regrasNfe.verificaImpostoImportacaoInformado(ImpostoImportacao))
                                        {
                                            // ---------------------------------------------------------------- inicio II -- P01 - M01
                                            xmlNfeElement = xmlNfe.CreateElement("II");
                                            XmlNode xmlNfeNodeDetImpostoII = xmlNfeElement;
                                            xmlNfeNodeDetImposto.AppendChild(xmlNfeNodeDetImpostoII);
                                            xmlNfe.AppendChild(xmlNfeNode);
                                            //----------------------------------------------------------------- fim

                                            // ---------------------------------------------------------------- inicio vBC -- P02 - P01
                                            xmlNfeElement = xmlNfe.CreateElement("vBC");
                                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ImpostoImportacao.II_vBC, 2, 1, 16, false, null, "P02");
                                            XmlNode xmlNfeNodeDetImpostoIIvBC = xmlNfeElement;
                                            xmlNfeNodeDetImpostoII.AppendChild(xmlNfeNodeDetImpostoIIvBC);
                                            xmlNfe.AppendChild(xmlNfeNode);
                                            //----------------------------------------------------------------- fim

                                            // ---------------------------------------------------------------- inicio vDespAdu -- P03 - P01
                                            xmlNfeElement = xmlNfe.CreateElement("vDespAdu");
                                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ImpostoImportacao.II_vDespAdu, 2, 1, 16, false, null, "P03");
                                            XmlNode xmlNfeNodeDetImpostoIIvDespAdu = xmlNfeElement;
                                            xmlNfeNodeDetImpostoII.AppendChild(xmlNfeNodeDetImpostoIIvDespAdu);
                                            xmlNfe.AppendChild(xmlNfeNode);
                                            //----------------------------------------------------------------- fim

                                            // ---------------------------------------------------------------- inicio vII -- P04 - P01
                                            xmlNfeElement = xmlNfe.CreateElement("vII");
                                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ImpostoImportacao.II_vII, 2, 1, 16, false, null, "P04");
                                            XmlNode xmlNfeNodeDetImpostoIIvII = xmlNfeElement;
                                            xmlNfeNodeDetImpostoII.AppendChild(xmlNfeNodeDetImpostoIIvII);
                                            xmlNfe.AppendChild(xmlNfeNode);
                                            //----------------------------------------------------------------- fim

                                            // ---------------------------------------------------------------- inicio vIOF -- P05 - P01
                                            xmlNfeElement = xmlNfe.CreateElement("vIOF");
                                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ImpostoImportacao.II_vIOF, 2, 1, 16, false, null, "P05");
                                            XmlNode xmlNfeNodeDetImpostoIIvIOF = xmlNfeElement;
                                            xmlNfeNodeDetImpostoII.AppendChild(xmlNfeNodeDetImpostoIIvIOF);
                                            xmlNfe.AppendChild(xmlNfeNode);
                                            //----------------------------------------------------------------- fim
                                        }
                                    }
                                }
                            }
                            #endregion

                            #region #Q - PIS
                            if (listaPis.Count > 0 && regrasNfe.verificaLinhaPisNula(listaPis,j))
                            {
                                Pis = listaPis[j];
                                // ---------------------------------------------------------------- inicio PIS -- Q01 - M01
                                xmlNfeElement = xmlNfe.CreateElement("PIS");
                                XmlNode xmlNfeNodeDetImpostoPIS = xmlNfeElement;
                                xmlNfeNodeDetImposto.AppendChild(xmlNfeNodeDetImpostoPIS);
                                xmlNfe.AppendChild(xmlNfeNode);
                                //----------------------------------------------------------------- fim

                                if (regrasNfe.verificaCampoVazio(Pis.PIS_PISAliq_CST))
                                {
                                    // ---------------------------------------------------------------- inicio PISAliq -- Q02 - Q01
                                    xmlNfeElement = xmlNfe.CreateElement("PISAliq");
                                    XmlNode xmlNfeNodeDetImpostoPISPISAliq = xmlNfeElement;
                                    xmlNfeNodeDetImpostoPIS.AppendChild(xmlNfeNodeDetImpostoPISPISAliq);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio CST -- Q06 - Q02
                                    if (regrasNfe.verificaSituacaoTributadaPis(Pis.PIS_PISAliq_CST))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("CST");
                                        xmlNfeElement.InnerText = Pis.PIS_PISAliq_CST;
                                        XmlNode xmlNfeNodeDetImpostoPISPISAliqCST = xmlNfeElement;
                                        xmlNfeNodeDetImpostoPISPISAliq.AppendChild(xmlNfeNodeDetImpostoPISPISAliqCST);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    else
                                    {

                                        return null;
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio vBC -- Q07 - Q02
                                    xmlNfeElement = xmlNfe.CreateElement("vBC");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(Pis.PIS_PISAliq_vBC, 2, 1, 16, false, null, "Q07");
                                    XmlNode xmlNfeNodeDetImpostoPISPISAliqVBC = xmlNfeElement;
                                    xmlNfeNodeDetImpostoPISPISAliq.AppendChild(xmlNfeNodeDetImpostoPISPISAliqVBC);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio pPIS -- Q08 - Q02
                                    xmlNfeElement = xmlNfe.CreateElement("pPIS");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(Pis.PIS_PISAliq_pPIS, 2, 1, 8, false, null, "Q08");
                                    XmlNode xmlNfeNodeDetImpostoPISPISAliqPPIS = xmlNfeElement;
                                    xmlNfeNodeDetImpostoPISPISAliq.AppendChild(xmlNfeNodeDetImpostoPISPISAliqPPIS);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio vPIS -- Q09 - Q02
                                    xmlNfeElement = xmlNfe.CreateElement("vPIS");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(Pis.PIS_PISAliq_vPIS, 2, 1, 16, false, null, "Q09");
                                    XmlNode xmlNfeNodeDetImpostoPISPISAliqVPIS = xmlNfeElement;
                                    xmlNfeNodeDetImpostoPISPISAliq.AppendChild(xmlNfeNodeDetImpostoPISPISAliqVPIS);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim
                                }

                                if (regrasNfe.verificaCampoVazio(Pis.PIS_PISQtde_CST))
                                {
                                    // ---------------------------------------------------------------- inicio PISQtde -- Q03 - Q01
                                    xmlNfeElement = xmlNfe.CreateElement("PISQtde");
                                    XmlNode xmlNfeNodeDetImpostoPISPISQtde = xmlNfeElement;
                                    xmlNfeNodeDetImpostoPIS.AppendChild(xmlNfeNodeDetImpostoPISPISQtde);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio CST -- Q06 - Q03
                                    xmlNfeElement = xmlNfe.CreateElement("CST");
                                    xmlNfeElement.InnerText = "03";
                                    XmlNode xmlNfeNodeDetImpostoPISPISQtdeCST = xmlNfeElement;
                                    xmlNfeNodeDetImpostoPISPISQtde.AppendChild(xmlNfeNodeDetImpostoPISPISQtdeCST);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio qBCProd -- Q10 - Q03
                                    xmlNfeElement = xmlNfe.CreateElement("qBCProd");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(Pis.PIS_PISQtde_qBCProd, 2, 1, 16, false, null, "Q10");
                                    XmlNode xmlNfeNodeDetImpostoPISPISQtdeQBCProd = xmlNfeElement;
                                    xmlNfeNodeDetImpostoPISPISQtde.AppendChild(xmlNfeNodeDetImpostoPISPISQtdeQBCProd);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio vAliqProd -- Q11 - Q03
                                    xmlNfeElement = xmlNfe.CreateElement("vAliqProd");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(Pis.PIS_PISQtde_vAliqProd, 2, 1, 16, false, null, "Q11");
                                    XmlNode xmlNfeNodeDetImpostoPISPISQtdeVAliqProd = xmlNfeElement;
                                    xmlNfeNodeDetImpostoPISPISQtde.AppendChild(xmlNfeNodeDetImpostoPISPISQtdeVAliqProd);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio vPIS -- Q09 - Q03
                                    xmlNfeElement = xmlNfe.CreateElement("vPIS");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(Pis.PIS_PISQtde_vPIS, 2, 1, 16, false, null, "Q09");
                                    XmlNode xmlNfeNodeDetImpostoPISPISQtdeVPIS = xmlNfeElement;
                                    xmlNfeNodeDetImpostoPISPISQtde.AppendChild(xmlNfeNodeDetImpostoPISPISQtdeVPIS);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                }

                                if (regrasNfe.verificaCampoVazio(Pis.PIS_PISNT_CST))
                                {
                                    // ---------------------------------------------------------------- inicio PISNT -- Q04 - Q01
                                    xmlNfeElement = xmlNfe.CreateElement("PISNT");
                                    XmlNode xmlNfeNodeDetImpostoPISPISNT = xmlNfeElement;
                                    xmlNfeNodeDetImpostoPIS.AppendChild(xmlNfeNodeDetImpostoPISPISNT);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio CST -- Q06 - Q04
                                    if (regrasNfe.verificaSituacaoNaoTributadaPis(Pis.PIS_PISNT_CST))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("CST");
                                        xmlNfeElement.InnerText = Pis.PIS_PISNT_CST;
                                        XmlNode xmlNfeNodeDetImpostoPISPISNTCST = xmlNfeElement;
                                        xmlNfeNodeDetImpostoPISPISNT.AppendChild(xmlNfeNodeDetImpostoPISPISNTCST);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    else
                                    {

                                        return null;
                                    }
                                    //----------------------------------------------------------------- fim
                                }

                                if (regrasNfe.verificaCampoVazio(Pis.PIS_PISOutr_CST))
                                {
                                    // ---------------------------------------------------------------- inicio PISOutr -- Q05 - Q01
                                    xmlNfeElement = xmlNfe.CreateElement("PISOutr");
                                    XmlNode xmlNfeNodeDetImpostoPISPISOutr = xmlNfeElement;
                                    xmlNfeNodeDetImpostoPIS.AppendChild(xmlNfeNodeDetImpostoPISPISOutr);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio CST -- Q06 - Q05
                                    if (regrasNfe.verificaSituacaoTributadaOutros(Pis.PIS_PISOutr_CST))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("CST");
                                        xmlNfeElement.InnerText = Pis.PIS_PISOutr_CST;
                                        XmlNode xmlNfeNodeDetImpostoPISPISOutrCST = xmlNfeElement;
                                        xmlNfeNodeDetImpostoPISPISOutr.AppendChild(xmlNfeNodeDetImpostoPISPISOutrCST);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    else
                                    {

                                        return null;
                                    }
                                    //----------------------------------------------------------------- fim

                                    if (regrasNfe.verificaCalculoPisPercentual(Pis.PIS_PISOutr_vBC, Pis.PIS_PISOutr_pPIS))
                                    {
                                        //// ---------------------------------------------------------------- inicio erproNFe -- Q06.1 - Q05
                                        //xmlNfeElement = xmlNfe.CreateElement("erproNFe");
                                        //XmlNode xmlNfeNodeXQ06_1 = xmlNfeElement;
                                        //xmlNfeNodeDetImpostoPISPISOutr.AppendChild(xmlNfeNodeXQ06_1);
                                        //xmlNfe.AppendChild(xmlNfeNode);
                                        ////----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio vBC -- Q07 - Q05
                                        xmlNfeElement = xmlNfe.CreateElement("vBC");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(Pis.PIS_PISOutr_vBC, 2, 1, 16, false, null, "Q07");
                                        XmlNode xmlNfeNodeXQ06_1vBC = xmlNfeElement;
                                        xmlNfeNodeDetImpostoPISPISOutr.AppendChild(xmlNfeNodeXQ06_1vBC);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio pPIS -- Q08 - Q05
                                        xmlNfeElement = xmlNfe.CreateElement("pPIS");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(Pis.PIS_PISOutr_pPIS, 2, 1, 8, false, null, "Q08");
                                        XmlNode xmlNfeNodeXQ06_1pPIS = xmlNfeElement;
                                        xmlNfeNodeDetImpostoPISPISOutr.AppendChild(xmlNfeNodeXQ06_1pPIS);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim
                                    }

                                    if (regrasNfe.verificaCalculoPisValor(Pis.PIS_PISOutr_qBCProd, Pis.PIS_PISOutr_vAliqProd, Pis.PIS_PISOutr_vPIS))
                                    {
                                        //// ---------------------------------------------------------------- inicio erproNFe -- Q08.1 - Q05
                                        //xmlNfeElement = xmlNfe.CreateElement("erproNFe");
                                        //XmlNode xmlNfeNodeXQ08_1 = xmlNfeElement;
                                        //xmlNfeNodeDetImpostoPISPISOutr.AppendChild(xmlNfeNodeXQ08_1);
                                        //xmlNfe.AppendChild(xmlNfeNode);
                                        ////----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio qBCProd -- Q10 - Q05
                                        xmlNfeElement = xmlNfe.CreateElement("qBCProd");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(Pis.PIS_PISOutr_qBCProd, 2, 1, 17, false, null, "Q10");
                                        XmlNode xmlNfeNodeXQ08_1qBCProd = xmlNfeElement;
                                        xmlNfeNodeDetImpostoPISPISOutr.AppendChild(xmlNfeNodeXQ08_1qBCProd);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio vAliqProd -- Q11 - Q05
                                        xmlNfeElement = xmlNfe.CreateElement("vAliqProd");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(Pis.PIS_PISOutr_vAliqProd, 2, 1, 16, false, null, "Q11");
                                        XmlNode xmlNfeNodeXQ08_1vAliqProd = xmlNfeElement;
                                        xmlNfeNodeDetImpostoPISPISOutr.AppendChild(xmlNfeNodeXQ08_1vAliqProd);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio vPIS -- Q09 - Q05
                                        xmlNfeElement = xmlNfe.CreateElement("vPIS");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(Pis.PIS_PISOutr_vPIS, 2, 1, 16, false, null, "Q09");
                                        XmlNode xmlNfeNodeDetImpostoPISPISOutrvPIS = xmlNfeElement;
                                        xmlNfeNodeDetImpostoPISPISOutr.AppendChild(xmlNfeNodeDetImpostoPISPISOutrvPIS);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim
                                    }
                                }
                            }
                            #endregion

                            #region #R - PIS ST
                            if (regrasNfe.verificaTipoModelo55NFe(identificacaoNfe.ide_mod))
                            {
                                if (regrasNfe.verificaPisSt(listaPisST) && regrasNfe.verificaLinhaPisNula(listaPisST, j))
                                {
                                    PisST = listaPisST[j];
                                    // ---------------------------------------------------------------- inicio PISST -- R01 - M01
                                    xmlNfeElement = xmlNfe.CreateElement("PISST");
                                    XmlNode xmlNfeNodeDetImpostoPISST = xmlNfeElement;
                                    xmlNfeNodeDetImposto.AppendChild(xmlNfeNodeDetImpostoPISST);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    if (regrasNfe.verificaCalculoPisPercentual(PisST.PISST_vBC, PisST.PISST_pPIS))
                                    {
                                        //// ---------------------------------------------------------------- inicio erproNFe -- R01.1 - R01
                                        //xmlNfeElement = xmlNfe.CreateElement("erproNFe");
                                        //XmlNode xmlNfeNodeXR01_1 = xmlNfeElement;
                                        //xmlNfeNodeDetImpostoPISST.AppendChild(xmlNfeNodeXR01_1);
                                        //xmlNfe.AppendChild(xmlNfeNode);
                                        ////----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio vBC -- R02 - R01
                                        xmlNfeElement = xmlNfe.CreateElement("vBC");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(PisST.PISST_vBC, 2, 1, 16, false, null, "R02");
                                        XmlNode xmlNfeNodeXR01_1vBC = xmlNfeElement;
                                        xmlNfeNodeDetImpostoPISST.AppendChild(xmlNfeNodeXR01_1vBC);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio pPIS -- R03 - R01
                                        xmlNfeElement = xmlNfe.CreateElement("pPIS");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(PisST.PISST_pPIS, 2, 1, 8, false, null, "R03");
                                        XmlNode xmlNfeNodeXR01_1pPIS = xmlNfeElement;
                                        xmlNfeNodeDetImpostoPISST.AppendChild(xmlNfeNodeXR01_1pPIS);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim
                                    }

                                    if (regrasNfe.verificaCalculoPisValor(PisST.PISST_qBCProd, PisST.PISST_vAliqProd, PisST.PISST_vPIS))
                                    {
                                        //// ---------------------------------------------------------------- inicio erproNFe -- R03.1 - R01
                                        //xmlNfeElement = xmlNfe.CreateElement("erproNFe");
                                        //XmlNode xmlNfeNodeXR03_1 = xmlNfeElement;
                                        //xmlNfeNodeDetImpostoPISST.AppendChild(xmlNfeNodeXR03_1);
                                        //xmlNfe.AppendChild(xmlNfeNode);
                                        ////----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio qBCProd -- R04 - R01
                                        xmlNfeElement = xmlNfe.CreateElement("qBCProd");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(PisST.PISST_qBCProd, 2, 1, 17, false, null, "R04");
                                        XmlNode xmlNfeNodeXR03_1qBCProd = xmlNfeElement;
                                        xmlNfeNodeDetImpostoPISST.AppendChild(xmlNfeNodeXR03_1qBCProd);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio vAliqProd -- R05 - R01
                                        xmlNfeElement = xmlNfe.CreateElement("vAliqProd");
                                        xmlNfeElement.InnerText = PisST.PISST_vAliqProd;
                                        XmlNode xmlNfeNodeXR03_1vAliqProd = xmlNfeElement;
                                        xmlNfeNodeDetImpostoPISST.AppendChild(xmlNfeNodeXR03_1vAliqProd);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio vPIS -- R06 - R01
                                        xmlNfeElement = xmlNfe.CreateElement("vPIS");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(PisST.PISST_vPIS, 2, 1, 16, false, null, "R06");
                                        XmlNode xmlNfeNodeXR03_1vPIS = xmlNfeElement;
                                        xmlNfeNodeDetImpostoPISST.AppendChild(xmlNfeNodeXR03_1vPIS);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim
                                    }
                                }
                            }
                            #endregion

                            #region #S - COFINS
                            if (regrasNfe.verificaCofinsInformado(listaCofins) && regrasNfe.verificaLinhaCofinsNula(listaCofins,j))
                            {
                                Cofins = listaCofins[j];
                                // ---------------------------------------------------------------- inicio COFINS -- S01 - M01
                                xmlNfeElement = xmlNfe.CreateElement("COFINS");
                                XmlNode xmlNfeNodeDetImpostoCOFINS = xmlNfeElement;
                                xmlNfeNodeDetImposto.AppendChild(xmlNfeNodeDetImpostoCOFINS);
                                xmlNfe.AppendChild(xmlNfeNode);
                                //----------------------------------------------------------------- fim

                                if (regrasNfe.verificaCampoVazio(Cofins.COFINS_COFINSAliq_CST))
                                {
                                    // ---------------------------------------------------------------- inicio COFINSAliq -- S02 - S01
                                    xmlNfeElement = xmlNfe.CreateElement("COFINSAliq");
                                    XmlNode xmlNfeNodeDetImpostoCOFINSCOFINSAliq = xmlNfeElement;
                                    xmlNfeNodeDetImpostoCOFINS.AppendChild(xmlNfeNodeDetImpostoCOFINSCOFINSAliq);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio CST -- S06 - S02
                                    if (regrasNfe.verificaSituacaoTributadaCofins(Cofins.COFINS_COFINSAliq_CST))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("CST");
                                        xmlNfeElement.InnerText = Cofins.COFINS_COFINSAliq_CST;
                                        XmlNode xmlNfeNodeDetImpostoCOFINSCOFINSAliqCST = xmlNfeElement;
                                        xmlNfeNodeDetImpostoCOFINSCOFINSAliq.AppendChild(xmlNfeNodeDetImpostoCOFINSCOFINSAliqCST);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    else
                                    {

                                        return null;
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio vBC -- S07 - S02
                                    xmlNfeElement = xmlNfe.CreateElement("vBC");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(Cofins.COFINS_COFINSAliq_vBC, 2, 1, 16, false, null, "S07");
                                    XmlNode xmlNfeNodeDetImpostoCOFINSCOFINSAliqVBC = xmlNfeElement;
                                    xmlNfeNodeDetImpostoCOFINSCOFINSAliq.AppendChild(xmlNfeNodeDetImpostoCOFINSCOFINSAliqVBC);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio pCOFINS -- S08 - S02
                                    xmlNfeElement = xmlNfe.CreateElement("pCOFINS");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(Cofins.COFINS_COFINSAliq_pCOFINS, 2, 1, 8, false, null, "S08");
                                    XmlNode xmlNfeNodeDetImpostoCOFINSCOFINSAliqPCOFINS = xmlNfeElement;
                                    xmlNfeNodeDetImpostoCOFINSCOFINSAliq.AppendChild(xmlNfeNodeDetImpostoCOFINSCOFINSAliqPCOFINS);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio vCOFINS -- S11 - S02
                                    xmlNfeElement = xmlNfe.CreateElement("vCOFINS");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(Cofins.COFINS_COFINSAliq_vCOFINS, 2, 1, 16, false, null, "S11");
                                    XmlNode xmlNfeNodeDetImpostoCOFINSCOFINSAliqVCOFINS = xmlNfeElement;
                                    xmlNfeNodeDetImpostoCOFINSCOFINSAliq.AppendChild(xmlNfeNodeDetImpostoCOFINSCOFINSAliqVCOFINS);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim
                                }

                                if (regrasNfe.verificaCampoVazio(Cofins.COFINS_COFINSQtde_CST))
                                {
                                    // ---------------------------------------------------------------- inicio COFINSQtde -- S03 - S01
                                    xmlNfeElement = xmlNfe.CreateElement("COFINSQtde");
                                    XmlNode xmlNfeNodeDetImpostoCOFINSCOFINSQtde = xmlNfeElement;
                                    xmlNfeNodeDetImpostoCOFINS.AppendChild(xmlNfeNodeDetImpostoCOFINSCOFINSQtde);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio CST -- S06 - S03
                                    xmlNfeElement = xmlNfe.CreateElement("CST");
                                    xmlNfeElement.InnerText = "03";
                                    XmlNode xmlNfeNodeDetImpostoCOFINSCOFINSQtdeCST = xmlNfeElement;
                                    xmlNfeNodeDetImpostoCOFINSCOFINSQtde.AppendChild(xmlNfeNodeDetImpostoCOFINSCOFINSQtdeCST);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio qBCProd -- S09 - S03
                                    xmlNfeElement = xmlNfe.CreateElement("qBCProd");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(Cofins.COFINS_COFINSQtde_qBCProd, 2, 1, 17, false, null, "S09");
                                    XmlNode xmlNfeNodeDetImpostoCOFINSCOFINSQtdeQBCProd = xmlNfeElement;
                                    xmlNfeNodeDetImpostoCOFINSCOFINSQtde.AppendChild(xmlNfeNodeDetImpostoCOFINSCOFINSQtdeQBCProd);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio vAliqProd -- S10 - S03
                                    xmlNfeElement = xmlNfe.CreateElement("vAliqProd");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(Cofins.COFINS_COFINSQtde_vAliqProd, 2, 1, 16, false, null, "S10");
                                    XmlNode xmlNfeNodeDetImpostoCOFINSCOFINSQtdeVAliqProd = xmlNfeElement;
                                    xmlNfeNodeDetImpostoCOFINSCOFINSQtde.AppendChild(xmlNfeNodeDetImpostoCOFINSCOFINSQtdeVAliqProd);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio vCOFINS -- S11 - S03
                                    xmlNfeElement = xmlNfe.CreateElement("vCOFINS");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(Cofins.COFINS_COFINSQtde_vCOFINS, 2, 1, 16, false, null, "S11");
                                    XmlNode xmlNfeNodeDetImpostoCOFINSCOFINSQtdeVCOFINS = xmlNfeElement;
                                    xmlNfeNodeDetImpostoCOFINSCOFINSQtde.AppendChild(xmlNfeNodeDetImpostoCOFINSCOFINSQtdeVCOFINS);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim
                                }

                                if (regrasNfe.verificaCampoVazio(Cofins.COFINS_COFINSNT_CST))
                                {
                                    // ---------------------------------------------------------------- inicio COFINSNT -- S04 - S01
                                    xmlNfeElement = xmlNfe.CreateElement("COFINSNT");
                                    XmlNode xmlNfeNodeDetImpostoCOFINSCOFINSNT = xmlNfeElement;
                                    xmlNfeNodeDetImpostoCOFINS.AppendChild(xmlNfeNodeDetImpostoCOFINSCOFINSNT);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio COFINSNT -- S06 - S04
                                    if (regrasNfe.verificaSituacaoNaoTributadaCofins(Cofins.COFINS_COFINSNT_CST))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("CST");
                                        xmlNfeElement.InnerText = Cofins.COFINS_COFINSNT_CST;
                                        XmlNode xmlNfeNodeDetImpostoCOFINSCOFINSNTCST = xmlNfeElement;
                                        xmlNfeNodeDetImpostoCOFINSCOFINSNT.AppendChild(xmlNfeNodeDetImpostoCOFINSCOFINSNTCST);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    else
                                    {

                                        return null;
                                    }
                                    //----------------------------------------------------------------- fim
                                }

                                if (regrasNfe.verificaCampoVazio(Cofins.COFINS_COFINSOutr_CST))
                                {
                                    // ---------------------------------------------------------------- inicio COFINSOutr -- S05 - S01
                                    xmlNfeElement = xmlNfe.CreateElement("COFINSOutr");
                                    XmlNode xmlNfeNodeDetImpostoCOFINSCOFINSOutr = xmlNfeElement;
                                    xmlNfeNodeDetImpostoCOFINS.AppendChild(xmlNfeNodeDetImpostoCOFINSCOFINSOutr);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio CST -- S06 - S05
                                    if (regrasNfe.verificaSituacaoTributadaCofinsOutros(Cofins.COFINS_COFINSOutr_CST))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("CST");
                                        xmlNfeElement.InnerText = Cofins.COFINS_COFINSOutr_CST;
                                        XmlNode xmlNfeNodeDetImpostoCOFINSCOFINSOutrCST = xmlNfeElement;
                                        xmlNfeNodeDetImpostoCOFINSCOFINSOutr.AppendChild(xmlNfeNodeDetImpostoCOFINSCOFINSOutrCST);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    else
                                    {

                                        return null;
                                    }
                                    //----------------------------------------------------------------- fim

                                    //// ---------------------------------------------------------------- inicio erproNFe -- S06.1 - S05
                                    //xmlNfeElement = xmlNfe.CreateElement("erproNFe");
                                    //XmlNode xmlNfeNodeXS06_1 = xmlNfeElement;
                                    //xmlNfeNodeDetImpostoCOFINSCOFINSOutr.AppendChild(xmlNfeNodeXS06_1);
                                    //xmlNfe.AppendChild(xmlNfeNode);
                                    ////----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio vBC -- S07 - S05
                                    xmlNfeElement = xmlNfe.CreateElement("vBC");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(Cofins.COFINS_COFINSOutr_vBC, 2, 1, 16, false, null, "S07");
                                    XmlNode xmlNfeNodeXS06_1vBC = xmlNfeElement;
                                    xmlNfeNodeDetImpostoCOFINSCOFINSOutr.AppendChild(xmlNfeNodeXS06_1vBC);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio pCOFINS -- S08 - S05
                                    xmlNfeElement = xmlNfe.CreateElement("pCOFINS");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(Cofins.COFINS_COFINSOutr_pCOFINS, 2, 1, 8, false, null, "S08");
                                    XmlNode xmlNfeNodeXS06_1pCOFINS = xmlNfeElement;
                                    xmlNfeNodeDetImpostoCOFINSCOFINSOutr.AppendChild(xmlNfeNodeXS06_1pCOFINS);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    //// ---------------------------------------------------------------- inicio erproNFe -- S08.1 - S05
                                    //xmlNfeElement = xmlNfe.CreateElement("erproNFe");
                                    //XmlNode xmlNfeNodeXS08_1 = xmlNfeElement;
                                    //xmlNfeNodeDetImpostoCOFINSCOFINSOutr.AppendChild(xmlNfeNodeXS08_1);
                                    //xmlNfe.AppendChild(xmlNfeNode);
                                    ////----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio qBCProd -- S09 - S05
                                    xmlNfeElement = xmlNfe.CreateElement("qBCProd");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(Cofins.COFINS_COFINSOutr_qBCProd, 2, 1, 17, false, null, "S09");
                                    XmlNode xmlNfeNodeXS08_1qBCProd = xmlNfeElement;
                                    xmlNfeNodeDetImpostoCOFINSCOFINSOutr.AppendChild(xmlNfeNodeXS08_1qBCProd);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio vAliqProd -- S10 - S05
                                    xmlNfeElement = xmlNfe.CreateElement("vAliqProd");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(Cofins.COFINS_COFINSOutr_vAliqProd, 2, 1, 16, false, null, "S10");
                                    XmlNode xmlNfeNodeXS08_1vAliqProd = xmlNfeElement;
                                    xmlNfeNodeDetImpostoCOFINSCOFINSOutr.AppendChild(xmlNfeNodeXS08_1vAliqProd);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio vCOFINS -- S11 - S05
                                    xmlNfeElement = xmlNfe.CreateElement("vCOFINS");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(Cofins.COFINS_COFINSOutr_vCOFINS, 2, 1, 16, false, null, "S11");
                                    XmlNode xmlNfeNodeXS08_1vCOFINS = xmlNfeElement;
                                    xmlNfeNodeDetImpostoCOFINSCOFINSOutr.AppendChild(xmlNfeNodeXS08_1vCOFINS);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim
                                }
                            }
                            #endregion

                            #region #T - COFINS ST
                            if (regrasNfe.verificaTipoModelo55NFe(identificacaoNfe.ide_mod))
                            {
                                if (regrasNfe.verificaCofinsSt(listaCofinsST) && regrasNfe.verificaLinhaCofinsNula(listaCofinsST, j))
                                {
                                    CofinsST = listaCofinsST[j];
                                    // ---------------------------------------------------------------- inicio COFINSST -- T01 - M01
                                    xmlNfeElement = xmlNfe.CreateElement("COFINSST");
                                    XmlNode xmlNfeNodeDetImpostoCOFINSST = xmlNfeElement;
                                    xmlNfeNodeDetImposto.AppendChild(xmlNfeNodeDetImpostoCOFINSST);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    //// ---------------------------------------------------------------- inicio erproNFe -- T01.1 - T01
                                    //xmlNfeElement = xmlNfe.CreateElement("erproNFe");
                                    //XmlNode xmlNfeNodeXT01_1 = xmlNfeElement;
                                    //xmlNfeNodeDetImpostoCOFINSST.AppendChild(xmlNfeNodeXT01_1);
                                    //xmlNfe.AppendChild(xmlNfeNode);
                                    ////----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio vBC -- T02 - T01
                                    xmlNfeElement = xmlNfe.CreateElement("vBC");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(CofinsST.COFINSST_vBC, 2, 1, 16, false, null, "T02");
                                    XmlNode xmlNfeNodeXT01_1vBC = xmlNfeElement;
                                    xmlNfeNodeDetImpostoCOFINSST.AppendChild(xmlNfeNodeXT01_1vBC);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio pCOFINS -- T03 - T01
                                    xmlNfeElement = xmlNfe.CreateElement("pCOFINS");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(CofinsST.COFINSST_pCOFINS, 2, 1, 8, false, null, "T03");
                                    XmlNode xmlNfeNodeXT01_1pCOFINS = xmlNfeElement;
                                    xmlNfeNodeDetImpostoCOFINSST.AppendChild(xmlNfeNodeXT01_1pCOFINS);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    //// ---------------------------------------------------------------- inicio erproNFe -- T03.1 - T01
                                    //xmlNfeElement = xmlNfe.CreateElement("erproNFe");
                                    //XmlNode xmlNfeNodeXT03_1 = xmlNfeElement;
                                    //xmlNfeNodeDetImpostoCOFINSST.AppendChild(xmlNfeNodeXT03_1);
                                    //xmlNfe.AppendChild(xmlNfeNode);
                                    ////----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio qBCProd -- T04 - T01
                                    xmlNfeElement = xmlNfe.CreateElement("qBCProd");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(CofinsST.COFINSST_qBCProd, 2, 1, 17, false, null, "T04");
                                    XmlNode xmlNfeNodeXT03_1qBCProd = xmlNfeElement;
                                    xmlNfeNodeDetImpostoCOFINSST.AppendChild(xmlNfeNodeXT03_1qBCProd);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio vAliqProd -- T05 - T01
                                    xmlNfeElement = xmlNfe.CreateElement("vAliqProd");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(CofinsST.COFINSST_vAliqProd, 2, 1, 16, false, null, "T05");
                                    XmlNode xmlNfeNodeXT03_1vAliqProd = xmlNfeElement;
                                    xmlNfeNodeDetImpostoCOFINSST.AppendChild(xmlNfeNodeXT03_1vAliqProd);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio vCOFINS -- T06 - T01
                                    xmlNfeElement = xmlNfe.CreateElement("vCOFINS");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(CofinsST.COFINSST_vCOFINS, 2, 1, 16, false, null, "T06");
                                    XmlNode xmlNfeNodeXT03_1vCOFINS = xmlNfeElement;
                                    xmlNfeNodeDetImpostoCOFINSST.AppendChild(xmlNfeNodeXT03_1vCOFINS);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim
                                }
                            }
                            #endregion

                            #region #U - ISSQN
                            if (regrasNfe.verificaIssqn(listaISSQN))
                            {
                                ISSQN = listaISSQN[j];
                                if (regrasNfe.verificaIssqnExclusivo(ICMSNormalST, ImpostoProdutosIndustrializados, ImpostoImportacao))
                                {
                                    // ---------------------------------------------------------------- inicio ISSQN -- U01 - M01
                                    xmlNfeElement = xmlNfe.CreateElement("ISSQN");
                                    XmlNode xmlNfeNodeDetImpostoISSQN = xmlNfeElement;
                                    xmlNfeNodeDetImposto.AppendChild(xmlNfeNodeDetImpostoISSQN);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio vBC -- U02 - U01
                                    xmlNfeElement = xmlNfe.CreateElement("vBC");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ISSQN.ISSQN_vBC, 2, 1, 16, false, null, "U02");
                                    XmlNode xmlNfeNodeDetImpostoISSQNvBC = xmlNfeElement;
                                    xmlNfeNodeDetImpostoISSQN.AppendChild(xmlNfeNodeDetImpostoISSQNvBC);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio vAliq -- U03 - U01
                                    xmlNfeElement = xmlNfe.CreateElement("vAliq");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ISSQN.ISSQN_vAliq, 1, 1, 8, false, null, "U03");
                                    XmlNode xmlNfeNodeDetImpostoISSQNvAliq = xmlNfeElement;
                                    xmlNfeNodeDetImpostoISSQN.AppendChild(xmlNfeNodeDetImpostoISSQNvAliq);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio vISSQN -- U04 - U01
                                    xmlNfeElement = xmlNfe.CreateElement("vISSQN");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ISSQN.ISSQN_vISSQN, 1, 1, 16, false, null, "U04");
                                    XmlNode xmlNfeNodeDetImpostoISSQNvISSQN = xmlNfeElement;
                                    xmlNfeNodeDetImpostoISSQN.AppendChild(xmlNfeNodeDetImpostoISSQNvISSQN);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio cMunFG -- U05 - U01
                                    xmlNfeElement = xmlNfe.CreateElement("cMunFG");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ISSQN.ISSQN_cMunFG, 1, 7, 7, false, null, "U05");
                                    XmlNode xmlNfeNodeDetImpostoISSQNcMunFG = xmlNfeElement;
                                    xmlNfeNodeDetImpostoISSQN.AppendChild(xmlNfeNodeDetImpostoISSQNcMunFG);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio cListServ -- U06 - U01
                                    xmlNfeElement = xmlNfe.CreateElement("cListServ");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ISSQN.ISSQN_cListServ, 1, 5, 5, false, null, "U06");
                                    XmlNode xmlNfeNodeDetImpostoISSQNcListServ = xmlNfeElement;
                                    xmlNfeNodeDetImpostoISSQN.AppendChild(xmlNfeNodeDetImpostoISSQNcListServ);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio vDeducao -- U07 - U01
                                    if (regrasNfe.verificaCampoVazio(ISSQN.ISSQN_vDeducao))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("vDeducao");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ISSQN.ISSQN_vDeducao, 1, 1, 16, false, null, "U07");
                                        XmlNode xmlNfeNodeDetImpostoISSQNvDeducao = xmlNfeElement;
                                        xmlNfeNodeDetImpostoISSQN.AppendChild(xmlNfeNodeDetImpostoISSQNvDeducao);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio vOutro -- U08 - U01
                                    if (regrasNfe.verificaCampoVazio(ISSQN.ISSQN_vOutro))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("vOutro");
                                        xmlNfeElement.InnerText = ISSQN.ISSQN_vOutro;
                                        XmlNode xmlNfeNodeDetImpostoISSQNvOutro = xmlNfeElement;
                                        xmlNfeNodeDetImpostoISSQN.AppendChild(xmlNfeNodeDetImpostoISSQNvOutro);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio vDescIncond -- U09 - U01
                                    if (regrasNfe.verificaCampoVazio(ISSQN.ISSQN_vDescIncond))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("vDescIncond");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ISSQN.ISSQN_vDescIncond, 1, 1, 16, false, null, "U09");
                                        XmlNode xmlNfeNodeDetImpostoISSQNvDescIncond = xmlNfeElement;
                                        xmlNfeNodeDetImpostoISSQN.AppendChild(xmlNfeNodeDetImpostoISSQNvDescIncond);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio vDescCond -- U10 - U01
                                    if (regrasNfe.verificaCampoVazio(ISSQN.ISSQN_vDescCond))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("vDescCond");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ISSQN.ISSQN_vDescCond, 1, 1, 16, false, null, "U10");
                                        XmlNode xmlNfeNodeDetImpostoISSQNvDescCond = xmlNfeElement;
                                        xmlNfeNodeDetImpostoISSQN.AppendChild(xmlNfeNodeDetImpostoISSQNvDescCond);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio vISSRet -- U11 - U01
                                    if (regrasNfe.verificaCampoVazio(ISSQN.ISSQN_vISSRet))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("vISSRet");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ISSQN.ISSQN_vISSRet, 1, 1, 16, false, null, "U11");
                                        XmlNode xmlNfeNodeDetImpostoISSQNvISSRet = xmlNfeElement;
                                        xmlNfeNodeDetImpostoISSQN.AppendChild(xmlNfeNodeDetImpostoISSQNvISSRet);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio indISS -- U12 - U01
                                    if (regrasNfe.verificaIndicadorExigibilidadeIss(ISSQN.ISSQN_indISS))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("indISS");
                                        xmlNfeElement.InnerText = ISSQN.ISSQN_indISS;
                                        XmlNode xmlNfeNodeDetImpostoISSQNindISS = xmlNfeElement;
                                        xmlNfeNodeDetImpostoISSQN.AppendChild(xmlNfeNodeDetImpostoISSQNindISS);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    else
                                    {

                                        return null;
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio cServico -- U13 - U01
                                    if (regrasNfe.verificaCampoVazio(ISSQN.ISSQN_cServico))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("cServico");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ISSQN.ISSQN_cServico, 1, 1, 20, false, null, "U13");
                                        XmlNode xmlNfeNodeDetImpostoISSQNcServico = xmlNfeElement;
                                        xmlNfeNodeDetImpostoISSQN.AppendChild(xmlNfeNodeDetImpostoISSQNcServico);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio cMun -- U14 - U01
                                    if (regrasNfe.verificaCampoVazio(ISSQN.ISSQN_cMun))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("cMun");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ISSQN.ISSQN_cMun, 1, 7, 7, false, null, "U14");
                                        XmlNode xmlNfeNodeDetImpostoISSQNcMun = xmlNfeElement;
                                        xmlNfeNodeDetImpostoISSQN.AppendChild(xmlNfeNodeDetImpostoISSQNcMun);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio cPais -- U15 - U01
                                    if (regrasNfe.verificaMunicipioIssqn(ISSQN.ISSQN_cMun))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("cPais");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(ISSQN.ISSQN_cPais, 1, 4, 4, false, null, "U15");
                                        XmlNode xmlNfeNodeDetImpostoISSQNcPais = xmlNfeElement;
                                        xmlNfeNodeDetImpostoISSQN.AppendChild(xmlNfeNodeDetImpostoISSQNcPais);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio nProcesso -- U16 - U01
                                    if (regrasNfe.verificaNumeroProcesso(ISSQN.ISSQN_indISS))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("nProcesso");
                                        xmlNfeElement.InnerText = ISSQN.ISSQN_nProcesso;
                                        XmlNode xmlNfeNodeDetImpostoISSQNnProcesso = xmlNfeElement;
                                        xmlNfeNodeDetImpostoISSQN.AppendChild(xmlNfeNodeDetImpostoISSQNnProcesso);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio indIncentivo -- U17 - U01
                                    if (regrasNfe.verificaIndicadorIncentivoFiscal(ISSQN.ISSQN_indIncentivo))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("indIncentivo");
                                        xmlNfeElement.InnerText = ISSQN.ISSQN_indIncentivo;
                                        XmlNode xmlNfeNodeDetImpostoISSQNindIncentivo = xmlNfeElement;
                                        xmlNfeNodeDetImpostoISSQN.AppendChild(xmlNfeNodeDetImpostoISSQNindIncentivo);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    else
                                    {

                                        return null;
                                    }
                                }
                            }
                            //----------------------------------------------------------------- fim
                            #endregion

                            #region #UA - Tributos Devolvidos(para o item da NF-e)
                            if (regrasNfe.verificaTributosDevolvido(listaTributosDevolvidos))
                            {
                                TributosDevolvidos = listaTributosDevolvidos[j];
                                // ---------------------------------------------------------------- inicio impostoDevol -- U50 - H01
                                xmlNfeElement = xmlNfe.CreateElement("impostoDevol");
                                XmlNode xmlNfeNodeDetImpostoDevol = xmlNfeElement;
                                xmlNfeNodeDet.AppendChild(xmlNfeNodeDetImpostoDevol);
                                xmlNfe.AppendChild(xmlNfeNode);
                                //----------------------------------------------------------------- fim

                                // ---------------------------------------------------------------- inicio pDevol -- U51 - U50
                                if (regrasNfe.verificaPorcentagemMercadoriaDevolvida(TributosDevolvidos.impostoDevol_pDevol))
                                {
                                    xmlNfeElement = xmlNfe.CreateElement("pDevol");
                                    xmlNfeElement.InnerText = TributosDevolvidos.impostoDevol_pDevol;
                                    XmlNode xmlNfeNodeDetImpostoDevolPDevol = xmlNfeElement;
                                    xmlNfeNodeDetImpostoDevol.AppendChild(xmlNfeNodeDetImpostoDevolPDevol);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                }
                                else
                                {

                                    return null;
                                }
                                //----------------------------------------------------------------- fim

                                // ---------------------------------------------------------------- inicio IPI -- U60 - U50
                                xmlNfeElement = xmlNfe.CreateElement("IPI");
                                XmlNode xmlNfeNodeDetImpostoDevolIPI = xmlNfeElement;
                                xmlNfeNodeDetImpostoDevol.AppendChild(xmlNfeNodeDetImpostoDevolIPI);
                                xmlNfe.AppendChild(xmlNfeNode);
                                //----------------------------------------------------------------- fim

                                // ---------------------------------------------------------------- inicio vIPIDevol -- U61 - U60
                                xmlNfeElement = xmlNfe.CreateElement("vIPIDevol");
                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(TributosDevolvidos.impostoDevol_vIPIDevol, 1, 1, 16, false, null, "U61");
                                XmlNode xmlNfeNodeDetImpostoDevolIPIvIPIDevol = xmlNfeElement;
                                xmlNfeNodeDetImpostoDevolIPI.AppendChild(xmlNfeNodeDetImpostoDevolIPIvIPIDevol);
                                xmlNfe.AppendChild(xmlNfeNode);
                                //----------------------------------------------------------------- fim
                            }
                            #endregion

                            #region #V - Informações adicionais(para o item da NF-e)
                            if (listaInformacoesAdicionais.Count > 0)
                            {
                                InformacoesAdicionais = listaInformacoesAdicionais[j];
                                // ---------------------------------------------------------------- inicio nfAdProd -- V01 - H01
                                if (regrasNfe.verificaCampoVazio(InformacoesAdicionais.ISSQN_infAdProd))
                                {
                                    xmlNfeElement = xmlNfe.CreateElement("nfAdProd");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(InformacoesAdicionais.ISSQN_infAdProd, 1, 1, 500, false, null, "V01");
                                    XmlNode xmlNfeNodeDetNfAdProd = xmlNfeElement;
                                    xmlNfeNodeDet.AppendChild(xmlNfeNodeDetNfAdProd);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                }
                                //----------------------------------------------------------------- fim
                            }
                            #endregion
                        }
                    }
                    #region #W - Total da NF-e

                    TotalNFe = listaTotalNFe[i];
                    // ---------------------------------------------------------------- inicio total -- W01 - A01
                    xmlNfeElement = xmlNfe.CreateElement("total");
                    XmlNode xmlNfeNodeInfNfeTotal = xmlNfeElement;
                    xmlNfeNodeInfNfe.AppendChild(xmlNfeNodeInfNfeTotal);
                    xmlNfe.AppendChild(xmlNfeNode);
                    //----------------------------------------------------------------- fim

                    // ---------------------------------------------------------------- inicio ICMSTot -- W02 - W01
                    xmlNfeElement = xmlNfe.CreateElement("ICMSTot");
                    XmlNode xmlNfeNodeInfNfeTotalICMSTot = xmlNfeElement;
                    xmlNfeNodeInfNfeTotal.AppendChild(xmlNfeNodeInfNfeTotalICMSTot);
                    xmlNfe.AppendChild(xmlNfeNode);
                    //----------------------------------------------------------------- fim

                    // ---------------------------------------------------------------- inicio vBC -- W03 - W02
                    xmlNfeElement = xmlNfe.CreateElement("vBC");
                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(TotalNFe.total_ICMSTot_vBC, 1, 1, 16, false, null, "W03");
                    XmlNode xmlNfeNodeInfNfeTotalICMSTotVBC = xmlNfeElement;
                    xmlNfeNodeInfNfeTotalICMSTot.AppendChild(xmlNfeNodeInfNfeTotalICMSTotVBC);
                    xmlNfe.AppendChild(xmlNfeNode);
                    //----------------------------------------------------------------- fim

                    // ---------------------------------------------------------------- inicio vICMS -- W04 - W02
                    xmlNfeElement = xmlNfe.CreateElement("vICMS");
                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(TotalNFe.total_ICMSTot_vICMS, 1, 1, 16, false, null, "W04");
                    XmlNode xmlNfeNodeInfNfeTotalICMSTotVICMS = xmlNfeElement;
                    xmlNfeNodeInfNfeTotalICMSTot.AppendChild(xmlNfeNodeInfNfeTotalICMSTotVICMS);
                    xmlNfe.AppendChild(xmlNfeNode);
                    //----------------------------------------------------------------- fim

                    // ---------------------------------------------------------------- inicio vICMSDeson -- W04a - W02
                    xmlNfeElement = xmlNfe.CreateElement("vICMSDeson");
                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(TotalNFe.total_ICMSTot_vICMSDeson, 1, 1, 16, false, null, "W04a");
                    XmlNode xmlNfeNodeInfNfeTotalICMSTotVICMSDeson = xmlNfeElement;
                    xmlNfeNodeInfNfeTotalICMSTot.AppendChild(xmlNfeNodeInfNfeTotalICMSTotVICMSDeson);
                    xmlNfe.AppendChild(xmlNfeNode);
                    //----------------------------------------------------------------- fim

                    // ---------------------------------------------------------------- inicio vBCST -- W05 - W02
                    xmlNfeElement = xmlNfe.CreateElement("vBCST");
                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(TotalNFe.total_ICMSTot_vBCST, 1, 1, 16, false, null, "W05");
                    XmlNode xmlNfeNodeInfNfeTotalICMSTotVBCST = xmlNfeElement;
                    xmlNfeNodeInfNfeTotalICMSTot.AppendChild(xmlNfeNodeInfNfeTotalICMSTotVBCST);
                    xmlNfe.AppendChild(xmlNfeNode);
                    //----------------------------------------------------------------- fim

                    // ---------------------------------------------------------------- inicio vST -- W06 - W02
                    xmlNfeElement = xmlNfe.CreateElement("vST");
                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(TotalNFe.total_ICMSTot_vST, 1, 1, 16, false, null, "W06");
                    XmlNode xmlNfeNodeInfNfeTotalICMSTotVST = xmlNfeElement;
                    xmlNfeNodeInfNfeTotalICMSTot.AppendChild(xmlNfeNodeInfNfeTotalICMSTotVST);
                    xmlNfe.AppendChild(xmlNfeNode);
                    //----------------------------------------------------------------- fim

                    // ---------------------------------------------------------------- inicio vProd -- W07 - W02
                    xmlNfeElement = xmlNfe.CreateElement("vProd");
                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(TotalNFe.total_ICMSTot_vProd, 1, 1, 16, false, null, "W07");
                    XmlNode xmlNfeNodeInfNfeTotalICMSTotVProd = xmlNfeElement;
                    xmlNfeNodeInfNfeTotalICMSTot.AppendChild(xmlNfeNodeInfNfeTotalICMSTotVProd);
                    xmlNfe.AppendChild(xmlNfeNode);
                    //----------------------------------------------------------------- fim

                    // ---------------------------------------------------------------- inicio vFrete -- W08 - W02
                    xmlNfeElement = xmlNfe.CreateElement("vFrete");
                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(TotalNFe.total_ICMSTot_vFrete, 1, 1, 16, false, null, "W08");
                    XmlNode xmlNfeNodeInfNfeTotalICMSTotVFrete = xmlNfeElement;
                    xmlNfeNodeInfNfeTotalICMSTot.AppendChild(xmlNfeNodeInfNfeTotalICMSTotVFrete);
                    xmlNfe.AppendChild(xmlNfeNode);
                    //----------------------------------------------------------------- fim

                    // ---------------------------------------------------------------- inicio vSeg -- W09 - W02
                    xmlNfeElement = xmlNfe.CreateElement("vSeg");
                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(TotalNFe.total_ICMSTot_vSeg, 1, 1, 16, false, null, "W09");
                    XmlNode xmlNfeNodeInfNfeTotalICMSTotVSeg = xmlNfeElement;
                    xmlNfeNodeInfNfeTotalICMSTot.AppendChild(xmlNfeNodeInfNfeTotalICMSTotVSeg);
                    xmlNfe.AppendChild(xmlNfeNode);
                    //----------------------------------------------------------------- fim

                    // ---------------------------------------------------------------- inicio vDesc -- W10 - W02
                    xmlNfeElement = xmlNfe.CreateElement("vDesc");
                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(TotalNFe.total_ICMSTot_vDesc, 1, 1, 16, false, null, "W10");
                    XmlNode xmlNfeNodeInfNfeTotalICMSTotVDesc = xmlNfeElement;
                    xmlNfeNodeInfNfeTotalICMSTot.AppendChild(xmlNfeNodeInfNfeTotalICMSTotVDesc);
                    xmlNfe.AppendChild(xmlNfeNode);
                    //----------------------------------------------------------------- fim

                    // ---------------------------------------------------------------- inicio vII -- W11 - W02
                    xmlNfeElement = xmlNfe.CreateElement("vII");
                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(TotalNFe.total_ICMSTot_vII, 1, 1, 16, false, null, "W11");
                    XmlNode xmlNfeNodeInfNfeTotalICMSTotVII = xmlNfeElement;
                    xmlNfeNodeInfNfeTotalICMSTot.AppendChild(xmlNfeNodeInfNfeTotalICMSTotVII);
                    xmlNfe.AppendChild(xmlNfeNode);
                    //----------------------------------------------------------------- fim

                    // ---------------------------------------------------------------- inicio vIPI -- W12 - W02
                    xmlNfeElement = xmlNfe.CreateElement("vIPI");
                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(TotalNFe.total_ICMSTot_vIPI, 1, 1, 16, false, null, "W12");
                    XmlNode xmlNfeNodeInfNfeTotalICMSTotVIPI = xmlNfeElement;
                    xmlNfeNodeInfNfeTotalICMSTot.AppendChild(xmlNfeNodeInfNfeTotalICMSTotVIPI);
                    xmlNfe.AppendChild(xmlNfeNode);
                    //----------------------------------------------------------------- fim

                    // ---------------------------------------------------------------- inicio vPIS -- W13 - W02
                    xmlNfeElement = xmlNfe.CreateElement("vPIS");
                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(TotalNFe.total_ICMSTot_vPIS, 1, 1, 16, false, null, "W13");
                    XmlNode xmlNfeNodeInfNfeTotalICMSTotVPIS = xmlNfeElement;
                    xmlNfeNodeInfNfeTotalICMSTot.AppendChild(xmlNfeNodeInfNfeTotalICMSTotVPIS);
                    xmlNfe.AppendChild(xmlNfeNode);
                    //----------------------------------------------------------------- fim

                    // ---------------------------------------------------------------- inicio vCOFINS -- W14 - W02
                    xmlNfeElement = xmlNfe.CreateElement("vCOFINS");
                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(TotalNFe.total_ICMSTot_vCOFINS, 1, 1, 16, false, null, "W14");
                    XmlNode xmlNfeNodeInfNfeTotalICMSTotVCOFINS = xmlNfeElement;
                    xmlNfeNodeInfNfeTotalICMSTot.AppendChild(xmlNfeNodeInfNfeTotalICMSTotVCOFINS);
                    xmlNfe.AppendChild(xmlNfeNode);
                    //----------------------------------------------------------------- fim

                    // ---------------------------------------------------------------- inicio vOutro -- W15 - W02
                    xmlNfeElement = xmlNfe.CreateElement("vOutro");
                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(TotalNFe.total_ICMSTot_vOutro, 1, 1, 16, false, null, "W15");
                    XmlNode xmlNfeNodeInfNfeTotalICMSTotVOutro = xmlNfeElement;
                    xmlNfeNodeInfNfeTotalICMSTot.AppendChild(xmlNfeNodeInfNfeTotalICMSTotVOutro);
                    xmlNfe.AppendChild(xmlNfeNode);
                    //----------------------------------------------------------------- fim

                    // ---------------------------------------------------------------- inicio vNF -- W16 - W02
                    xmlNfeElement = xmlNfe.CreateElement("vNF");
                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(TotalNFe.total_ICMSTot_vNF, 1, 1, 16, false, null, "W16");
                    XmlNode xmlNfeNodeInfNfeTotalICMSTotVNF = xmlNfeElement;
                    xmlNfeNodeInfNfeTotalICMSTot.AppendChild(xmlNfeNodeInfNfeTotalICMSTotVNF);
                    xmlNfe.AppendChild(xmlNfeNode);
                    //----------------------------------------------------------------- fim

                    // ---------------------------------------------------------------- inicio vTotTrib -- W16a - W02
                    xmlNfeElement = xmlNfe.CreateElement("vTotTrib");
                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(TotalNFe.total_ICMSTot_vTotTrib, 1, 1, 16, false, null, "W16a");
                    XmlNode xmlNfeNodeInfNfeTotalICMSTotVTotTrib = xmlNfeElement;
                    xmlNfeNodeInfNfeTotalICMSTot.AppendChild(xmlNfeNodeInfNfeTotalICMSTotVTotTrib);
                    xmlNfe.AppendChild(xmlNfeNode);
                    //----------------------------------------------------------------- fim
                    #endregion

                    #region #W01 - Total da NF-e / ISSQN
                    if (regrasNfe.verificaGrupoTotaisReferenteIssqn(listaTotalNFeISSQN))
                    {
                        TotalNFeISSQN = listaTotalNFeISSQN[i];
                        // ---------------------------------------------------------------- inicio ISSQNtot -- W17 - W01
                        xmlNfeElement = xmlNfe.CreateElement("ISSQNtot");
                        XmlNode xmlNfeNodeInfNfeTotalISSQNtot = xmlNfeElement;
                        xmlNfeNodeInfNfeTotal.AppendChild(xmlNfeNodeInfNfeTotalISSQNtot);
                        xmlNfe.AppendChild(xmlNfeNode);
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio vServ -- W18 - W17
                        if (regrasNfe.verificaCampoVazio(TotalNFeISSQN.total_ISSQNtot_vServ))
                        {
                            xmlNfeElement = xmlNfe.CreateElement("vServ");
                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(TotalNFeISSQN.total_ISSQNtot_vServ, 1, 1, 16, false, null, "W18");
                            XmlNode xmlNfeNodeInfNfeTotalISSQNtotVServ = xmlNfeElement;
                            xmlNfeNodeInfNfeTotalISSQNtot.AppendChild(xmlNfeNodeInfNfeTotalISSQNtotVServ);
                            xmlNfe.AppendChild(xmlNfeNode);
                        }
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio vBC -- W19 - W17
                        if (regrasNfe.verificaCampoVazio(TotalNFeISSQN.total_ISSQNtot_vBC))
                        {
                            xmlNfeElement = xmlNfe.CreateElement("vBC");
                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(TotalNFeISSQN.total_ISSQNtot_vBC, 1, 1, 16, false, null, "W19");
                            XmlNode xmlNfeNodeInfNfeTotalISSQNtotVBC = xmlNfeElement;
                            xmlNfeNodeInfNfeTotalISSQNtot.AppendChild(xmlNfeNodeInfNfeTotalISSQNtotVBC);
                            xmlNfe.AppendChild(xmlNfeNode);
                        }
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio vISS -- W20 - W17
                        if (regrasNfe.verificaCampoVazio(TotalNFeISSQN.total_ISSQNtot_vISS))
                        {
                            xmlNfeElement = xmlNfe.CreateElement("vISS");
                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(TotalNFeISSQN.total_ISSQNtot_vISS, 1, 1, 16, false, null, "W20");
                            XmlNode xmlNfeNodeInfNfeTotalISSQNtotVISS = xmlNfeElement;
                            xmlNfeNodeInfNfeTotalISSQNtot.AppendChild(xmlNfeNodeInfNfeTotalISSQNtotVISS);
                            xmlNfe.AppendChild(xmlNfeNode);
                        }
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio vPIS -- W21 - W17
                        if (regrasNfe.verificaCampoVazio(TotalNFeISSQN.total_ISSQNtot_vPIS))
                        {
                            xmlNfeElement = xmlNfe.CreateElement("vPIS");
                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(TotalNFeISSQN.total_ISSQNtot_vPIS, 1, 1, 16, false, null, "W21");
                            XmlNode xmlNfeNodeInfNfeTotalISSQNtotVPIS = xmlNfeElement;
                            xmlNfeNodeInfNfeTotalISSQNtot.AppendChild(xmlNfeNodeInfNfeTotalISSQNtotVPIS);
                            xmlNfe.AppendChild(xmlNfeNode);
                        }
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio vCOFINS -- W22 - W17
                        if (regrasNfe.verificaCampoVazio(TotalNFeISSQN.total_ISSQNtot_vCOFINS))
                        {
                            xmlNfeElement = xmlNfe.CreateElement("vCOFINS");
                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(TotalNFeISSQN.total_ISSQNtot_vCOFINS, 1, 1, 16, false, null, "W22");
                            XmlNode xmlNfeNodeInfNfeTotalISSQNtotVCOFINS = xmlNfeElement;
                            xmlNfeNodeInfNfeTotalISSQNtot.AppendChild(xmlNfeNodeInfNfeTotalISSQNtotVCOFINS);
                            xmlNfe.AppendChild(xmlNfeNode);
                        }
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio dCompet -- W22a - W17
                        if (regrasNfe.verificaCampoVazio(TotalNFeISSQN.total_ISSQNtot_dCompet))
                        {
                            xmlNfeElement = xmlNfe.CreateElement("dCompet");
                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(TotalNFeISSQN.total_ISSQNtot_dCompet, 2, 8, 8, false, null, "W22a");
                            XmlNode xmlNfeNodeInfNfeTotalISSQNtotDCompet = xmlNfeElement;
                            xmlNfeNodeInfNfeTotalISSQNtot.AppendChild(xmlNfeNodeInfNfeTotalISSQNtotDCompet);
                            xmlNfe.AppendChild(xmlNfeNode);
                        }
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio vDeducao -- W22b - W17
                        if (regrasNfe.verificaCampoVazio(TotalNFeISSQN.total_ISSQNtot_dCompet))
                        {
                            xmlNfeElement = xmlNfe.CreateElement("vDeducao");
                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(TotalNFeISSQN.total_ISSQNtot_vDeducao, 1, 1, 16, false, null, "W22b");
                            XmlNode xmlNfeNodeInfNfeTotalISSQNtotVDeducao = xmlNfeElement;
                            xmlNfeNodeInfNfeTotalISSQNtot.AppendChild(xmlNfeNodeInfNfeTotalISSQNtotVDeducao);
                            xmlNfe.AppendChild(xmlNfeNode);
                        }
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio vOutro -- W22c - W17
                        if (regrasNfe.verificaCampoVazio(TotalNFeISSQN.total_ISSQNtot_vOutro))
                        {
                            xmlNfeElement = xmlNfe.CreateElement("vOutro");
                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(TotalNFeISSQN.total_ISSQNtot_vOutro, 1, 1, 16, false, null, "W22c");
                            XmlNode xmlNfeNodeInfNfeTotalISSQNtotVOutro = xmlNfeElement;
                            xmlNfeNodeInfNfeTotalISSQNtot.AppendChild(xmlNfeNodeInfNfeTotalISSQNtotVOutro);
                            xmlNfe.AppendChild(xmlNfeNode);
                        }
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio vDescIncond -- W22d - W17
                        if (regrasNfe.verificaCampoVazio(TotalNFeISSQN.total_ISSQNtot_vDescIncond))
                        {
                            xmlNfeElement = xmlNfe.CreateElement("vDescIncond");
                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(TotalNFeISSQN.total_ISSQNtot_vDescIncond, 1, 1, 16, false, null, "W22d");
                            XmlNode xmlNfeNodeInfNfeTotalISSQNtotVDescIncond = xmlNfeElement;
                            xmlNfeNodeInfNfeTotalISSQNtot.AppendChild(xmlNfeNodeInfNfeTotalISSQNtotVDescIncond);
                            xmlNfe.AppendChild(xmlNfeNode);
                        }
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio vDescCond -- W22e - W17
                        if (regrasNfe.verificaCampoVazio(TotalNFeISSQN.total_ISSQNtot_vDescCond))
                        {
                            xmlNfeElement = xmlNfe.CreateElement("vDescCond");
                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(TotalNFeISSQN.total_ISSQNtot_vDescCond, 1, 1, 16, false, null, "W22e");
                            XmlNode xmlNfeNodeInfNfeTotalISSQNtotVDescCond = xmlNfeElement;
                            xmlNfeNodeInfNfeTotalISSQNtot.AppendChild(xmlNfeNodeInfNfeTotalISSQNtotVDescCond);
                            xmlNfe.AppendChild(xmlNfeNode);
                        }
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio vISSRet -- W22f - W17
                        if (regrasNfe.verificaCampoVazio(TotalNFeISSQN.total_ISSQNtot_vISSRet))
                        {
                            xmlNfeElement = xmlNfe.CreateElement("vISSRet");
                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(TotalNFeISSQN.total_ISSQNtot_vISSRet, 1, 1, 16, false, null, "W22f");
                            XmlNode xmlNfeNodeInfNfeTotalISSQNtotVISSRet = xmlNfeElement;
                            xmlNfeNodeInfNfeTotalISSQNtot.AppendChild(xmlNfeNodeInfNfeTotalISSQNtotVISSRet);
                            xmlNfe.AppendChild(xmlNfeNode);
                        }
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio cRegTrib -- W22g - W17
                        if (regrasNfe.verificaCampoVazio(TotalNFeISSQN.total_ISSQNtot_vISSRet))
                        {
                            if (regrasNfe.verificaCodigoRegimeEspecialTributacao(TotalNFeISSQN.total_ISSQNtot_cRegTrib))
                            {
                                xmlNfeElement = xmlNfe.CreateElement("cRegTrib");
                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(TotalNFeISSQN.total_ISSQNtot_cRegTrib, 2, 2, 2, true, null, "W22g");
                                XmlNode xmlNfeNodeInfNfeTotalISSQNtotCRegTrib = xmlNfeElement;
                                xmlNfeNodeInfNfeTotalISSQNtot.AppendChild(xmlNfeNodeInfNfeTotalISSQNtotCRegTrib);
                                xmlNfe.AppendChild(xmlNfeNode);
                            }
                            else
                            {

                                return null;
                            }
                        }
                        //----------------------------------------------------------------- fim
                    }
                    #endregion

                    #region #W02 - Total da NF-e / Retenção de Tributos
                    if (regrasNfe.verificaTotalNfeRetencaoProduto(listaTotalNFeRetencaoTributos))
                    {
                        TotalNFeRetencaoTributos = listaTotalNFeRetencaoTributos[i];
                        // ---------------------------------------------------------------- inicio retTrib -- W23 - W01
                        xmlNfeElement = xmlNfe.CreateElement("retTrib");
                        XmlNode xmlNfeNodeInfNfeTotalRetTrib = xmlNfeElement;
                        xmlNfeNodeInfNfeTotal.AppendChild(xmlNfeNodeInfNfeTotalRetTrib);
                        xmlNfe.AppendChild(xmlNfeNode);
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio vRetPIS -- W24 - W23
                        if (regrasNfe.verificaCampoVazio(TotalNFeRetencaoTributos.total_retTrib_vRetPIS))
                        {
                            xmlNfeElement = xmlNfe.CreateElement("vRetPIS");
                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(TotalNFeRetencaoTributos.total_retTrib_vRetPIS, 1, 1, 16, false, null, "W24");
                            XmlNode xmlNfeNodeInfNfeTotalRetTribVRetPIS = xmlNfeElement;
                            xmlNfeNodeInfNfeTotalRetTrib.AppendChild(xmlNfeNodeInfNfeTotalRetTribVRetPIS);
                            xmlNfe.AppendChild(xmlNfeNode);
                        }
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio vRetCOFINS -- W25 - W23
                        if (regrasNfe.verificaCampoVazio(TotalNFeRetencaoTributos.total_retTrib_vRetCOFINS))
                        {
                            xmlNfeElement = xmlNfe.CreateElement("vRetCOFINS");
                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(TotalNFeRetencaoTributos.total_retTrib_vRetCOFINS, 1, 1, 16, false, null, "W25");
                            XmlNode xmlNfeNodeInfNfeTotalRetTribVRetCOFINS = xmlNfeElement;
                            xmlNfeNodeInfNfeTotalRetTrib.AppendChild(xmlNfeNodeInfNfeTotalRetTribVRetCOFINS);
                            xmlNfe.AppendChild(xmlNfeNode);
                        }
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio vRetCSLL -- W26 - W23
                        if (regrasNfe.verificaCampoVazio(TotalNFeRetencaoTributos.total_retTrib_vRetCSLL))
                        {
                            xmlNfeElement = xmlNfe.CreateElement("vRetCSLL");
                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(TotalNFeRetencaoTributos.total_retTrib_vRetCSLL, 1, 1, 16, false, null, "W26");
                            XmlNode xmlNfeNodeInfNfeTotalRetTribVRetCSLL = xmlNfeElement;
                            xmlNfeNodeInfNfeTotalRetTrib.AppendChild(xmlNfeNodeInfNfeTotalRetTribVRetCSLL);
                            xmlNfe.AppendChild(xmlNfeNode);
                        }
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio vBCIRRF -- W27 - W23
                        if (regrasNfe.verificaCampoVazio(TotalNFeRetencaoTributos.total_retTrib_vBCIRRF))
                        {
                            xmlNfeElement = xmlNfe.CreateElement("vBCIRRF");
                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(TotalNFeRetencaoTributos.total_retTrib_vBCIRRF, 1, 1, 16, false, null, "W27");
                            XmlNode xmlNfeNodeInfNfeTotalRetTribVBCIRRF = xmlNfeElement;
                            xmlNfeNodeInfNfeTotalRetTrib.AppendChild(xmlNfeNodeInfNfeTotalRetTribVBCIRRF);
                            xmlNfe.AppendChild(xmlNfeNode);
                        }
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio vIRRF -- W28 - W23
                        if (regrasNfe.verificaCampoVazio(TotalNFeRetencaoTributos.total_retTrib_vIRRF))
                        {
                            xmlNfeElement = xmlNfe.CreateElement("vIRRF");
                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(TotalNFeRetencaoTributos.total_retTrib_vIRRF, 1, 1, 2, false, null, "W28");
                            XmlNode xmlNfeNodeInfNfeTotalRetTribVIRRF = xmlNfeElement;
                            xmlNfeNodeInfNfeTotalRetTrib.AppendChild(xmlNfeNodeInfNfeTotalRetTribVIRRF);
                            xmlNfe.AppendChild(xmlNfeNode);
                        }
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio vBCRetPrev -- W29 - W23
                        if (regrasNfe.verificaCampoVazio(TotalNFeRetencaoTributos.total_retTrib_vRetCSLL))
                        {
                            xmlNfeElement = xmlNfe.CreateElement("vBCRetPrev");
                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(TotalNFeRetencaoTributos.total_retTrib_vBCRetPrev, 1, 1, 16, false, null, "W29");
                            XmlNode xmlNfeNodeInfNfeTotalRetTribVBCRetPrev = xmlNfeElement;
                            xmlNfeNodeInfNfeTotalRetTrib.AppendChild(xmlNfeNodeInfNfeTotalRetTribVBCRetPrev);
                            xmlNfe.AppendChild(xmlNfeNode);
                        }
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio vRetPrev -- W30 - W23
                        if (regrasNfe.verificaCampoVazio(TotalNFeRetencaoTributos.total_retTrib_vRetPrev))
                        {
                            xmlNfeElement = xmlNfe.CreateElement("vRetPrev");
                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(TotalNFeRetencaoTributos.total_retTrib_vRetPrev, 1, 1, 16, false, null, "W30");
                            XmlNode xmlNfeNodeInfNfeTotalRetTribVRetPrev = xmlNfeElement;
                            xmlNfeNodeInfNfeTotalRetTrib.AppendChild(xmlNfeNodeInfNfeTotalRetTribVRetPrev);
                            xmlNfe.AppendChild(xmlNfeNode);
                        }
                        //----------------------------------------------------------------- fim
                    }
                    #endregion

                    #region #X - Informações do Transporte da NF-e
                    if (listaInformacoesTransporteNFe.Count > 0)
                    {
                        InformacoesTransporteNFe = listaInformacoesTransporteNFe[i];
                        // ---------------------------------------------------------------- inicio transp -- X01 - A01
                        xmlNfeElement = xmlNfe.CreateElement("transp");
                        XmlNode xmlNfeNodeInfNfeTransp = xmlNfeElement;
                        xmlNfeNodeInfNfe.AppendChild(xmlNfeNodeInfNfeTransp);
                        xmlNfe.AppendChild(xmlNfeNode);
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio modFrete -- X02 - X01
                        if (regrasNfe.verificaModalidadeFrete(InformacoesTransporteNFe.transp_modFrete))
                        {
                            xmlNfeElement = xmlNfe.CreateElement("modFrete");
                            xmlNfeElement.InnerText = InformacoesTransporteNFe.transp_modFrete;
                            XmlNode xmlNfeNodeInfNfeTranspModFrete = xmlNfeElement;
                            xmlNfeNodeInfNfeTransp.AppendChild(xmlNfeNodeInfNfeTranspModFrete);
                            xmlNfe.AppendChild(xmlNfeNode);
                        }
                        else
                        {

                            return null;
                        }
                        //----------------------------------------------------------------- fim

                        // ---------------------------------------------------------------- inicio transporta -- X03 - X02
                        if (!regrasNfe.verificaGrupoTransportador(InformacoesTransporteNFe.transp_transporta_CNPJ, InformacoesTransporteNFe.transp_transporta_CPF, InformacoesTransporteNFe.transp_transporta_xNome, InformacoesTransporteNFe.transp_transporta_IE, InformacoesTransporteNFe.transp_transporta_xEnder, InformacoesTransporteNFe.transp_transporta_xMun, InformacoesTransporteNFe.transp_transporta_UF))
                        {
                            xmlNfeElement = xmlNfe.CreateElement("transporta");
                            XmlNode xmlNfeNodeInfNfeTranspModFreteTransporta = xmlNfeElement;
                            xmlNfeNodeInfNfeTranspModFreteTransporta.AppendChild(xmlNfeNodeInfNfeTranspModFreteTransporta);
                            xmlNfe.AppendChild(xmlNfeNode);
                            //----------------------------------------------------------------- fim

                            // ---------------------------------------------------------------- inicio CNPJ -- X04 - X03
                            if (regrasNfe.verificaCampoVazio(InformacoesTransporteNFe.transp_transporta_CNPJ))
                            {
                                xmlNfeElement = xmlNfe.CreateElement("CNPJ");
                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(InformacoesTransporteNFe.transp_transporta_CNPJ, 2, 14, 14, false, null, "X04");
                                XmlNode xmlNfeNodeInfNfeTranspModFreteTransportaCNPJ = xmlNfeElement;
                                xmlNfeNodeInfNfeTranspModFreteTransporta.AppendChild(xmlNfeNodeInfNfeTranspModFreteTransportaCNPJ);
                                xmlNfe.AppendChild(xmlNfeNode);
                            }
                            //----------------------------------------------------------------- fim

                            // ---------------------------------------------------------------- inicio CPF -- X05 - X03
                            if (regrasNfe.verificaCampoVazio(InformacoesTransporteNFe.transp_transporta_CPF))
                            {
                                xmlNfeElement = xmlNfe.CreateElement("CPF");
                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(InformacoesTransporteNFe.transp_transporta_CPF, 2, 11, 11, false, null, "X05");
                                XmlNode xmlNfeNodeInfNfeTranspModFreteTransportaCPF = xmlNfeElement;
                                xmlNfeNodeInfNfeTranspModFreteTransporta.AppendChild(xmlNfeNodeInfNfeTranspModFreteTransportaCPF);
                                xmlNfe.AppendChild(xmlNfeNode);
                            }
                            //----------------------------------------------------------------- fim

                            // ---------------------------------------------------------------- inicio xNome -- X06 - X03
                            if (regrasNfe.verificaCampoVazio(InformacoesTransporteNFe.transp_transporta_xNome))
                            {
                                xmlNfeElement = xmlNfe.CreateElement("xNome");
                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(InformacoesTransporteNFe.transp_transporta_xNome, 2, 2, 60, false, null, "X06");
                                XmlNode xmlNfeNodeInfNfeTranspModFreteTransportaXNome = xmlNfeElement;
                                xmlNfeNodeInfNfeTranspModFreteTransporta.AppendChild(xmlNfeNodeInfNfeTranspModFreteTransportaXNome);
                                xmlNfe.AppendChild(xmlNfeNode);
                            }
                            //----------------------------------------------------------------- fim

                            // ---------------------------------------------------------------- inicio IE -- X07 - X03
                            if (regrasNfe.verificaCampoVazio(InformacoesTransporteNFe.transp_transporta_IE))
                            {
                                xmlNfeElement = xmlNfe.CreateElement("IE");
                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(InformacoesTransporteNFe.transp_transporta_IE, 2, 2, 14, false, null, "X07");
                                XmlNode xmlNfeNodeInfNfeTranspModFreteTransportaIE = xmlNfeElement;
                                xmlNfeNodeInfNfeTranspModFreteTransporta.AppendChild(xmlNfeNodeInfNfeTranspModFreteTransportaIE);
                                xmlNfe.AppendChild(xmlNfeNode);
                            }
                            //----------------------------------------------------------------- fim

                            // ---------------------------------------------------------------- inicio xEnder -- X08 - X03
                            if (regrasNfe.verificaCampoVazio(InformacoesTransporteNFe.transp_transporta_xEnder))
                            {
                                xmlNfeElement = xmlNfe.CreateElement("xEnder");
                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(InformacoesTransporteNFe.transp_transporta_xEnder, 2, 1, 60, false, null, "X08");
                                XmlNode xmlNfeNodeInfNfeTranspModFreteTransportaXEnder = xmlNfeElement;
                                xmlNfeNodeInfNfeTranspModFreteTransporta.AppendChild(xmlNfeNodeInfNfeTranspModFreteTransportaXEnder);
                                xmlNfe.AppendChild(xmlNfeNode);
                            }
                            //----------------------------------------------------------------- fim

                            // ---------------------------------------------------------------- inicio xMun -- X09 - X03
                            if (regrasNfe.verificaCampoVazio(InformacoesTransporteNFe.transp_transporta_xMun))
                            {
                                xmlNfeElement = xmlNfe.CreateElement("xMun");
                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(InformacoesTransporteNFe.transp_transporta_xMun, 2, 1, 60, false, null, "X09");
                                XmlNode xmlNfeNodeInfNfeTranspModFreteTransportaXMun = xmlNfeElement;
                                xmlNfeNodeInfNfeTranspModFreteTransporta.AppendChild(xmlNfeNodeInfNfeTranspModFreteTransportaXMun);
                                xmlNfe.AppendChild(xmlNfeNode);
                            }
                            //----------------------------------------------------------------- fim

                            // ---------------------------------------------------------------- inicio UF -- X10 - X03
                            if (regrasNfe.verificaCampoVazio(InformacoesTransporteNFe.transp_transporta_UF))
                            {
                                xmlNfeElement = xmlNfe.CreateElement("UF");
                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(InformacoesTransporteNFe.transp_transporta_UF, 2, 2, 2, false, null, "X10");
                                XmlNode xmlNfeNodeInfNfeTranspModFreteTransportaUF = xmlNfeElement;
                                xmlNfeNodeInfNfeTranspModFreteTransporta.AppendChild(xmlNfeNodeInfNfeTranspModFreteTransportaUF);
                                xmlNfe.AppendChild(xmlNfeNode);
                            }
                            //----------------------------------------------------------------- fim
                        }

                        if (regrasNfe.verificaTipoModelo55NFe(identificacaoNfe.ide_mod))
                        {
                            if (!regrasNfe.verificaGrupoRetencaoIcmsTransporte(InformacoesTransporteNFe.transp_retTransp_vServ,
                                InformacoesTransporteNFe.transp_retTransp_vBCRet, InformacoesTransporteNFe.transp_retTransp_pICMSRet,
                                InformacoesTransporteNFe.transp_retTransp_vICMSRet, InformacoesTransporteNFe.transp_retTransp_CFOP,
                                InformacoesTransporteNFe.transp_retTransp_cMunFG))
                            {
                                // ---------------------------------------------------------------- inicio retTransp -- X11 - X01
                                xmlNfeElement = xmlNfe.CreateElement("retTransp");
                                XmlNode xmlNfeNodeInfNfeTranspModRetTransp = xmlNfeElement;
                                xmlNfeNodeInfNfeTransp.AppendChild(xmlNfeNodeInfNfeTranspModRetTransp);
                                xmlNfe.AppendChild(xmlNfeNode);
                                //----------------------------------------------------------------- fim

                                // ---------------------------------------------------------------- inicio vServ -- X12 - X11
                                xmlNfeElement = xmlNfe.CreateElement("vServ");
                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(InformacoesTransporteNFe.transp_retTransp_vServ, 1, 1, 16, false, null, "X12");
                                XmlNode xmlNfeNodeInfNfeTranspModRetTranspVServ = xmlNfeElement;
                                xmlNfeNodeInfNfeTranspModRetTransp.AppendChild(xmlNfeNodeInfNfeTranspModRetTranspVServ);
                                xmlNfe.AppendChild(xmlNfeNode);
                                //----------------------------------------------------------------- fim

                                // ---------------------------------------------------------------- inicio vBCRet -- X13 - X11
                                xmlNfeElement = xmlNfe.CreateElement("vBCRet");
                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(InformacoesTransporteNFe.transp_retTransp_vBCRet, 1, 1, 16, false, null, "X13");
                                XmlNode xmlNfeNodeInfNfeTranspModRetTranspVBCRet = xmlNfeElement;
                                xmlNfeNodeInfNfeTranspModRetTransp.AppendChild(xmlNfeNodeInfNfeTranspModRetTranspVBCRet);
                                xmlNfe.AppendChild(xmlNfeNode);
                                //----------------------------------------------------------------- fim

                                // ---------------------------------------------------------------- inicio pICMSRet  -- X14 - X11
                                xmlNfeElement = xmlNfe.CreateElement("pICMSRet");
                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(InformacoesTransporteNFe.transp_retTransp_pICMSRet, 2, 1, 8, false, null, "X14");
                                XmlNode xmlNfeNodeInfNfeTranspModRetTranspPICMSRet = xmlNfeElement;
                                xmlNfeNodeInfNfeTranspModRetTransp.AppendChild(xmlNfeNodeInfNfeTranspModRetTranspPICMSRet);
                                xmlNfe.AppendChild(xmlNfeNode);
                                //----------------------------------------------------------------- fim

                                // ---------------------------------------------------------------- inicio vICMSRet  -- X15 - X11
                                xmlNfeElement = xmlNfe.CreateElement("vICMSRet");
                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(InformacoesTransporteNFe.transp_retTransp_vICMSRet, 1, 1, 16, false, null, "X15");
                                XmlNode xmlNfeNodeInfNfeTranspModRetTranspVICMSRet = xmlNfeElement;
                                xmlNfeNodeInfNfeTranspModRetTransp.AppendChild(xmlNfeNodeInfNfeTranspModRetTranspVICMSRet);
                                xmlNfe.AppendChild(xmlNfeNode);
                                //----------------------------------------------------------------- fim

                                // ---------------------------------------------------------------- inicio CFOP  -- X16 - X11
                                xmlNfeElement = xmlNfe.CreateElement("CFOP");
                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(InformacoesTransporteNFe.transp_retTransp_CFOP, 2, 4, 4, false, null, "X16");
                                XmlNode xmlNfeNodeInfNfeTranspModRetTranspCFOP = xmlNfeElement;
                                xmlNfeNodeInfNfeTranspModRetTransp.AppendChild(xmlNfeNodeInfNfeTranspModRetTranspCFOP);
                                xmlNfe.AppendChild(xmlNfeNode);
                                //----------------------------------------------------------------- fim

                                // ---------------------------------------------------------------- inicio cMunFG  -- X17 - X11
                                xmlNfeElement = xmlNfe.CreateElement("cMunFG");
                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(InformacoesTransporteNFe.transp_retTransp_cMunFG, 2, 7, 7, false, null, "X17");
                                XmlNode xmlNfeNodeInfNfeTranspModRetTranspCMunFG = xmlNfeElement;
                                xmlNfeNodeInfNfeTranspModRetTransp.AppendChild(xmlNfeNodeInfNfeTranspModRetTranspCMunFG);
                                xmlNfe.AppendChild(xmlNfeNode);
                                //----------------------------------------------------------------- fim
                            }

                            if (!regrasNfe.verificaGrupoVeiculoTransporte(InformacoesTransporteNFe.transp_veicTransp_placa, InformacoesTransporteNFe.transp_veicTransp_UF))
                            {
                                //// ---------------------------------------------------------------- inicio erproNFe -- X17.1 - X01
                                //xmlNfeElement = xmlNfe.CreateElement("erproNFe");
                                //XmlNode xmlNfeNodeXX17_1 = xmlNfeElement;
                                //xmlNfeNodeInfNfeTransp.AppendChild(xmlNfeNodeXX17_1);
                                //xmlNfe.AppendChild(xmlNfeNode);
                                ////----------------------------------------------------------------- fim

                                // ---------------------------------------------------------------- inicio veicTransp -- X18 - X01
                                xmlNfeElement = xmlNfe.CreateElement("veicTransp");
                                XmlNode xmlNfeNodeXX17_1veicTransp = xmlNfeElement;
                                xmlNfeNodeInfNfeTransp.AppendChild(xmlNfeNodeXX17_1veicTransp);
                                xmlNfe.AppendChild(xmlNfeNode);
                                //----------------------------------------------------------------- fim

                                // ---------------------------------------------------------------- inicio placa -- X19 - X18
                                xmlNfeElement = xmlNfe.CreateElement("placa");
                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(InformacoesTransporteNFe.transp_veicTransp_placa, 2, 7, 7, false, null, "X19");
                                XmlNode xmlNfeNodeXX17_1veicTranspPlaca = xmlNfeElement;
                                xmlNfeNodeXX17_1veicTransp.AppendChild(xmlNfeNodeXX17_1veicTranspPlaca);
                                xmlNfe.AppendChild(xmlNfeNode);
                                //----------------------------------------------------------------- fim

                                // ---------------------------------------------------------------- inicio UF -- X20 - X18
                                xmlNfeElement = xmlNfe.CreateElement("UF");
                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(InformacoesTransporteNFe.transp_veicTransp_UF, 2, 2, 2, false, null, "X20");
                                XmlNode xmlNfeNodeXX17_1veicTranspUF = xmlNfeElement;
                                xmlNfeNodeXX17_1veicTransp.AppendChild(xmlNfeNodeXX17_1veicTranspUF);
                                xmlNfe.AppendChild(xmlNfeNode);
                                //----------------------------------------------------------------- fim

                                // ---------------------------------------------------------------- inicio RNTC -- X21 - X18
                                if (regrasNfe.verificaCampoVazio(InformacoesTransporteNFe.transp_veicTransp_RNTC))
                                {
                                    xmlNfeElement = xmlNfe.CreateElement("RNTC");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(InformacoesTransporteNFe.transp_veicTransp_RNTC, 2, 1, 20, false, null, "X21");
                                    XmlNode xmlNfeNodeXX17_1veicTranspRNTC = xmlNfeElement;
                                    xmlNfeNodeXX17_1veicTransp.AppendChild(xmlNfeNodeXX17_1veicTranspRNTC);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                }
                                //----------------------------------------------------------------- fim
                            }

                            if (!regrasNfe.verificaGrupoVeiculoTransporte(InformacoesTransporteNFe.transp_reboque_placa, InformacoesTransporteNFe.transp_reboque_UF))
                            {
                                for (int t = 0; t < listaInformacoesTransporteNFe.Count; t++)
                                {
                                    if (t == 5)
                                        break;
                                    if (listaInformacoesTransporteNFe[t].numeroNotaFiscal.Equals(identificacaoNfe.ide_nNF))
                                    {
                                        // ---------------------------------------------------------------- inicio reboque -- X22 - X17.1
                                        xmlNfeElement = xmlNfe.CreateElement("reboque");
                                        XmlNode xmlNfeNodeXX17_1reboque = xmlNfeElement;
                                        xmlNfeNodeXX17_1reboque.AppendChild(xmlNfeNodeXX17_1reboque);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio placa -- X23 - X22
                                        xmlNfeElement = xmlNfe.CreateElement("placa");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(InformacoesTransporteNFe.transp_reboque_placa, 2, 7, 7, false, null, "X23");
                                        XmlNode xmlNfeNodeXX17_1reboquePlaca = xmlNfeElement;
                                        xmlNfeNodeXX17_1reboque.AppendChild(xmlNfeNodeXX17_1reboquePlaca);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio UF -- X24 - X22
                                        xmlNfeElement = xmlNfe.CreateElement("UF");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(InformacoesTransporteNFe.transp_reboque_UF, 2, 2, 2, false, null, "X24");
                                        XmlNode xmlNfeNodeXX17_1reboqueUF = xmlNfeElement;
                                        xmlNfeNodeXX17_1reboque.AppendChild(xmlNfeNodeXX17_1reboqueUF);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio RNTC -- X25 - X22
                                        if (regrasNfe.verificaCampoVazio(InformacoesTransporteNFe.transp_reboque_RNTC))
                                        {
                                            xmlNfeElement = xmlNfe.CreateElement("RNTC");
                                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(InformacoesTransporteNFe.transp_reboque_RNTC, 2, 1, 20, false, null, "X25");
                                            XmlNode xmlNfeNodeXX17_1reboqueRNTC = xmlNfeElement;
                                            xmlNfeNodeXX17_1reboque.AppendChild(xmlNfeNodeXX17_1reboqueRNTC);
                                            xmlNfe.AppendChild(xmlNfeNode);
                                        }
                                        //----------------------------------------------------------------- fim
                                    }

                                    // ---------------------------------------------------------------- inicio vagao -- X25a - X22
                                    if (regrasNfe.verificaCampoVazio(InformacoesTransporteNFe.transp_reboque_vagao))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("vagao");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(InformacoesTransporteNFe.transp_reboque_vagao, 2, 1, 20, false, null, "X25a");
                                        XmlNode xmlNfeNodeXX17_1reboqueVagao = xmlNfeElement;
                                        xmlNfeNodeXX17_1reboqueVagao.AppendChild(xmlNfeNodeXX17_1reboqueVagao);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio balsa -- X25b - X22
                                    if (regrasNfe.verificaCampoVazio(InformacoesTransporteNFe.transp_reboque_balsa))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("balsa");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(InformacoesTransporteNFe.transp_reboque_balsa, 2, 1, 20, false, null, "X25b");
                                        XmlNode xmlNfeNodeXX17_1reboqueBalsa = xmlNfeElement;
                                        xmlNfeNodeXX17_1reboqueBalsa.AppendChild(xmlNfeNodeXX17_1reboqueBalsa);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    //----------------------------------------------------------------- fim
                                }
                            }
                        }

                        if (!regrasNfe.verificaGrupoVolumes(InformacoesTransporteNFe.transp_vol_qVol, InformacoesTransporteNFe.transp_vol_esp, InformacoesTransporteNFe.transp_vol_marca, InformacoesTransporteNFe.transp_vol_nVol, InformacoesTransporteNFe.transp_vol_pesoL, InformacoesTransporteNFe.transp_vol_pesoB))
                        {
                            for (int y = 0; y < listaInformacoesTransporteNFe.Count; y++)
                            {
                                if (y == 5000)
                                    break;
                                if (listaInformacoesTransporteNFe[y].numeroNotaFiscal.Equals(identificacaoNfe.ide_nNF))
                                {
                                    // ---------------------------------------------------------------- inicio vol -- X26 - X01
                                    xmlNfeElement = xmlNfe.CreateElement("vol");
                                    XmlNode xmlNfeNodeInfNfeTranspVol = xmlNfeElement;
                                    xmlNfeNodeInfNfeTransp.AppendChild(xmlNfeNodeInfNfeTranspVol);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio qVol -- X27 - X26
                                    if (regrasNfe.verificaCampoVazio(InformacoesTransporteNFe.transp_vol_qVol))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("qVol");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(InformacoesTransporteNFe.transp_vol_qVol, 2, 1, 15, false, null, "X27");
                                        XmlNode xmlNfeNodeInfNfeTranspVolQVol = xmlNfeElement;
                                        xmlNfeNodeInfNfeTranspVol.AppendChild(xmlNfeNodeInfNfeTranspVolQVol);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio esp -- X28 - X26
                                    if (regrasNfe.verificaCampoVazio(InformacoesTransporteNFe.transp_vol_esp))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("esp");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(InformacoesTransporteNFe.transp_vol_esp, 2, 1, 60, false, null, "X28");
                                        XmlNode xmlNfeNodeInfNfeTranspVolEsp = xmlNfeElement;
                                        xmlNfeNodeInfNfeTranspVol.AppendChild(xmlNfeNodeInfNfeTranspVolEsp);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio marca -- X29 - X26
                                    if (regrasNfe.verificaCampoVazio(InformacoesTransporteNFe.transp_vol_marca))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("marca");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(InformacoesTransporteNFe.transp_vol_marca, 2, 1, 60, false, null, "X29");
                                        XmlNode xmlNfeNodeInfNfeTranspVolMarca = xmlNfeElement;
                                        xmlNfeNodeInfNfeTranspVol.AppendChild(xmlNfeNodeInfNfeTranspVolMarca);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio nVol -- X30 - X26
                                    if (regrasNfe.verificaCampoVazio(InformacoesTransporteNFe.transp_vol_nVol))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("nVol");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(InformacoesTransporteNFe.transp_vol_nVol, 2, 1, 60, false, null, "X30");
                                        XmlNode xmlNfeNodeInfNfeTranspVolNVol = xmlNfeElement;
                                        xmlNfeNodeInfNfeTranspVol.AppendChild(xmlNfeNodeInfNfeTranspVolNVol);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio pesoL -- X31 - X26
                                    if (regrasNfe.verificaCampoVazio(InformacoesTransporteNFe.transp_vol_pesoL))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("pesoL");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(InformacoesTransporteNFe.transp_vol_pesoL, 2, 1, 16, false, null, "X31");
                                        XmlNode xmlNfeNodeInfNfeTranspVolPesoL = xmlNfeElement;
                                        xmlNfeNodeInfNfeTranspVol.AppendChild(xmlNfeNodeInfNfeTranspVolPesoL);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio pesoB -- X32 - X26
                                    if (regrasNfe.verificaCampoVazio(InformacoesTransporteNFe.transp_vol_pesoB))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("pesoB");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(InformacoesTransporteNFe.transp_vol_pesoB, 2, 1, 16, false, null, "X32");
                                        XmlNode xmlNfeNodeInfNfeTranspVolPesoB = xmlNfeElement;
                                        xmlNfeNodeInfNfeTranspVol.AppendChild(xmlNfeNodeInfNfeTranspVolPesoB);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    //----------------------------------------------------------------- fim
                                }
                            }
                        }

                        if (!regrasNfe.verificaGrupoLacre(InformacoesTransporteNFe.transp_vol_lacres_nLacre))
                        {
                            for (int u = 0; u < listaInformacoesTransporteNFe.Count; u++)
                            {
                                if (u == 5000)
                                    break;
                                if (listaInformacoesTransporteNFe[u].numeroNotaFiscal.Equals(identificacaoNfe.ide_nNF))
                                {
                                    // ---------------------------------------------------------------- inicio lacres -- X33 - X26
                                    xmlNfeElement = xmlNfe.CreateElement("lacres");
                                    XmlNode xmlNfeNodeInfNfeTranspVolLacres = xmlNfeElement;
                                    xmlNfeNodeInfNfeTranspVolLacres.AppendChild(xmlNfeNodeInfNfeTranspVolLacres);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio nLacre -- X34 - X33
                                    xmlNfeElement = xmlNfe.CreateElement("nLacre");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(InformacoesTransporteNFe.transp_vol_lacres_nLacre, 2, 1, 60, false, null, "X34");
                                    XmlNode xmlNfeNodeInfNfeTranspVolLacresNLacre = xmlNfeElement;
                                    xmlNfeNodeInfNfeTranspVolLacres.AppendChild(xmlNfeNodeInfNfeTranspVolLacresNLacre);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim
                                }
                            }
                        }
                    }
                    #endregion

                    #region #Y - Dados da Cobrança
                    if (regrasNfe.verificaTipoModelo55NFe(identificacaoNfe.ide_mod))
                    {
                        if (listaDadosCobranca.Count > 0)
                        {
                            DadosCobranca = listaDadosCobranca[i];
                            if (!regrasNfe.verificaGrupoCobrancaFatura(DadosCobranca.cobr_fat_nFat, DadosCobranca.cobr_fat_vOrig, DadosCobranca.cobr_fat_vDesc, DadosCobranca.cobr_fat_vLiq) || !regrasNfe.verificaGrupoCobrancaDuplicata(DadosCobranca.cobr_dup_nDup, DadosCobranca.cobr_dup_dVenc, DadosCobranca.cobr_dup_vDup))
                            {
                                // ---------------------------------------------------------------- inicio cobr -- Y01 - A01
                                xmlNfeElement = xmlNfe.CreateElement("cobr");
                                XmlNode xmlNfeNodeInfNfeCobr = xmlNfeElement;
                                xmlNfeNodeInfNfe.AppendChild(xmlNfeNodeInfNfeCobr);
                                xmlNfe.AppendChild(xmlNfeNode);
                                //----------------------------------------------------------------- fim

                                if (!regrasNfe.verificaGrupoCobrancaFatura(DadosCobranca.cobr_fat_nFat, DadosCobranca.cobr_fat_vOrig, DadosCobranca.cobr_fat_vDesc, DadosCobranca.cobr_fat_vLiq))
                                {
                                    // ---------------------------------------------------------------- inicio fat -- Y02 - Y01
                                    xmlNfeElement = xmlNfe.CreateElement("fat");
                                    XmlNode xmlNfeNodeInfNfeCobrFat = xmlNfeElement;
                                    xmlNfeNodeInfNfeCobr.AppendChild(xmlNfeNodeInfNfeCobrFat);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio nFat -- Y03 - Y02
                                    if (regrasNfe.verificaCampoVazio(DadosCobranca.cobr_fat_nFat))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("nFat");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(DadosCobranca.cobr_fat_nFat, 2, 1, 60, false, null, "Y03");
                                        XmlNode xmlNfeNodeInfNfeCobrFatNFat = xmlNfeElement;
                                        xmlNfeNodeInfNfeCobrFat.AppendChild(xmlNfeNodeInfNfeCobrFatNFat);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio vOrig -- Y04 - Y02
                                    if (regrasNfe.verificaCampoVazio(DadosCobranca.cobr_fat_vOrig))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("vOrig");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(DadosCobranca.cobr_fat_vOrig, 1, 1, 16, false, null, "Y04");
                                        XmlNode xmlNfeNodeInfNfeCobrFatVOrig = xmlNfeElement;
                                        xmlNfeNodeInfNfeCobrFat.AppendChild(xmlNfeNodeInfNfeCobrFatVOrig);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio vDesc -- Y05 - Y02
                                    if (regrasNfe.verificaCampoVazio(DadosCobranca.cobr_fat_vDesc))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("vDesc");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(DadosCobranca.cobr_fat_vDesc, 1, 1, 16, false, null, "Y05");
                                        XmlNode xmlNfeNodeInfNfeCobrFatVDesc = xmlNfeElement;
                                        xmlNfeNodeInfNfeCobrFat.AppendChild(xmlNfeNodeInfNfeCobrFatVDesc);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio vLiq -- Y06 - Y02
                                    if (regrasNfe.verificaCampoVazio(DadosCobranca.cobr_fat_vLiq))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("vLiq");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(DadosCobranca.cobr_fat_vLiq, 1, 1, 16, false, null, "Y06");
                                        XmlNode xmlNfeNodeInfNfeCobrFatVLiq = xmlNfeElement;
                                        xmlNfeNodeInfNfeCobrFat.AppendChild(xmlNfeNodeInfNfeCobrFatVLiq);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    //----------------------------------------------------------------- fim
                                }

                                if (!regrasNfe.verificaGrupoCobrancaDuplicata(DadosCobranca.cobr_dup_nDup, DadosCobranca.cobr_dup_dVenc, DadosCobranca.cobr_dup_vDup))
                                {
                                    for (int o = 0; o < listaDadosCobranca.Count; o++)
                                    {
                                        if (o == 120)
                                            break;
                                        if (listaDadosCobranca[o].numeroNotaFiscal.Equals(identificacaoNfe.ide_nNF))
                                        {
                                            // ---------------------------------------------------------------- inicio dup -- Y07 - Y01
                                            xmlNfeElement = xmlNfe.CreateElement("dup");
                                            XmlNode xmlNfeNodeInfNfeCobrDup = xmlNfeElement;
                                            xmlNfeNodeInfNfeCobr.AppendChild(xmlNfeNodeInfNfeCobrDup);
                                            xmlNfe.AppendChild(xmlNfeNode);
                                            //----------------------------------------------------------------- fim

                                            // ---------------------------------------------------------------- inicio nDup -- Y08 - Y07
                                            if (regrasNfe.verificaCampoVazio(DadosCobranca.cobr_dup_nDup))
                                            {
                                                xmlNfeElement = xmlNfe.CreateElement("nDup");
                                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(DadosCobranca.cobr_dup_nDup, 2, 1, 60, false, null, "Y08");
                                                XmlNode xmlNfeNodeInfNfeCobrDupNDup = xmlNfeElement;
                                                xmlNfeNodeInfNfeCobrDup.AppendChild(xmlNfeNodeInfNfeCobrDupNDup);
                                                xmlNfe.AppendChild(xmlNfeNode);
                                            }
                                            //----------------------------------------------------------------- fim

                                            // ---------------------------------------------------------------- inicio dVenc -- Y09 - Y07
                                            if (regrasNfe.verificaCampoVazio(DadosCobranca.cobr_dup_dVenc))
                                            {
                                                xmlNfeElement = xmlNfe.CreateElement("dVenc");
                                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(DadosCobranca.cobr_dup_dVenc, 3, null, null, false, null, "Y09");
                                                XmlNode xmlNfeNodeInfNfeCobrDupDVenc = xmlNfeElement;
                                                xmlNfeNodeInfNfeCobrDup.AppendChild(xmlNfeNodeInfNfeCobrDupDVenc);
                                                xmlNfe.AppendChild(xmlNfeNode);
                                            }
                                            //----------------------------------------------------------------- fim

                                            // ---------------------------------------------------------------- inicio vDup -- Y10 - Y07
                                            xmlNfeElement = xmlNfe.CreateElement("vDup");
                                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(DadosCobranca.cobr_dup_vDup, 1, 1, 16, false, null, "Y10");
                                            XmlNode xmlNfeNodeInfNfeCobrDupVDup = xmlNfeElement;
                                            xmlNfeNodeInfNfeCobrDup.AppendChild(xmlNfeNodeInfNfeCobrDupVDup);
                                            xmlNfe.AppendChild(xmlNfeNode);
                                            //----------------------------------------------------------------- fim
                                        }
                                    }
                                }
                            }
                        }
                    }
                    #endregion

                    #region #YA - Formas de Pagamento
                    if (!regrasNfe.verificaTipoModelo55NFe(identificacaoNfe.ide_mod))
                    {
                        for (int r = 0; r < listaFormasPagamento.Count; r++)
                        {
                            FormasPagamento = listaFormasPagamento[r];
                            if (r == 100)
                                break;
                            if (listaFormasPagamento[r].numeroNotaFiscal.Equals(identificacaoNfe.ide_cNF))
                            {
                                // ---------------------------------------------------------------- inicio pag -- YA01 - A01
                                xmlNfeElement = xmlNfe.CreateElement("pag");
                                XmlNode xmlNfeNodeInfNfePag = xmlNfeElement;
                                xmlNfeNodeInfNfe.AppendChild(xmlNfeNodeInfNfePag);
                                xmlNfe.AppendChild(xmlNfeNode);
                                //----------------------------------------------------------------- fim

                                // ---------------------------------------------------------------- inicio tPag -- YA02 - YA01
                                if (regrasNfe.verificaTipoFormaPagamento(FormasPagamento.pag_tPag))
                                {
                                    xmlNfeElement = xmlNfe.CreateElement("tPag");
                                    xmlNfeElement.InnerText = FormasPagamento.pag_tPag;
                                    XmlNode xmlNfeNodeInfNfePagTPag = xmlNfeElement;
                                    xmlNfeNodeInfNfePag.AppendChild(xmlNfeNodeInfNfePagTPag);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                }
                                else
                                {
                                }

                                return null;
                                //----------------------------------------------------------------- fim

                                // ---------------------------------------------------------------- inicio vPag -- YA03 - YA01
                                xmlNfeElement = xmlNfe.CreateElement("vPag");
                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(FormasPagamento.pag_vPag, 1, 1, 16, false, null, "YA03");
                                XmlNode xmlNfeNodeInfNfePagVPag = xmlNfeElement;
                                xmlNfeNodeInfNfePag.AppendChild(xmlNfeNodeInfNfePagVPag);
                                xmlNfe.AppendChild(xmlNfeNode);
                                //----------------------------------------------------------------- fim

                                if (!regrasNfe.verificaGrupoCartoes(FormasPagamento.pag_card_CNPJ, FormasPagamento.pag_card_tBand, FormasPagamento.pag_card_cAut))
                                {
                                    // ---------------------------------------------------------------- inicio card -- YA04 - YA01
                                    xmlNfeElement = xmlNfe.CreateElement("card");
                                    XmlNode xmlNfeNodeInfNfePagCard = xmlNfeElement;
                                    xmlNfeNodeInfNfePag.AppendChild(xmlNfeNodeInfNfePagCard);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio CNPJ -- YA05 - YA04
                                    xmlNfeElement = xmlNfe.CreateElement("CNPJ");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(FormasPagamento.pag_card_CNPJ, 2, 14, 14, false, null, "YA05");
                                    XmlNode xmlNfeNodeInfNfePagCardCNPJ = xmlNfeElement;
                                    xmlNfeNodeInfNfePagCard.AppendChild(xmlNfeNodeInfNfePagCardCNPJ);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio tBand -- YA06 - YA04
                                    if (regrasNfe.verificaTipoBandeira(FormasPagamento.pag_card_tBand))
                                    {
                                        xmlNfeElement = xmlNfe.CreateElement("tBand");
                                        xmlNfeElement.InnerText = FormasPagamento.pag_card_tBand;
                                        XmlNode xmlNfeNodeInfNfePagCardTBand = xmlNfeElement;
                                        xmlNfeNodeInfNfePagCard.AppendChild(xmlNfeNodeInfNfePagCardTBand);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                    }
                                    else
                                    {

                                        return null;
                                    }
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio cAut -- YA07 - YA04
                                    xmlNfeElement = xmlNfe.CreateElement("cAut");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(FormasPagamento.pag_card_cAut, 2, 1, 20, false, null, "YA07");
                                    XmlNode xmlNfeNodeInfNfePagCardCAut = xmlNfeElement;
                                    xmlNfeNodeInfNfePagCard.AppendChild(xmlNfeNodeInfNfePagCardCAut);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim
                                }
                            }
                        }
                    }
                    #endregion

                    #region #Z - Informações Adicionais da NF-e
                    if (listaInformacoesAdicionais.Count > 0)
                    {
                        if (!regrasNfe.verificaGrupoInformacoesAdicionais(InformacoesAdicionaisNFe.infAdic_infAdFisco, InformacoesAdicionaisNFe.infAdic_infCpl) ||
                            !regrasNfe.verificaCampoLivreContribuinte(InformacoesAdicionaisNFe.infAdic_obsCont_xCampo, InformacoesAdicionaisNFe.infAdic_obsCont_xTexto) ||
                            !regrasNfe.verificaGrupoProcessoReferenciado(InformacoesAdicionaisNFe.infAdic_procRef_nProc, InformacoesAdicionaisNFe.infAdic_procRef_indProc))
                        {
                            InformacoesAdicionais = listaInformacoesAdicionais[i];
                            // ---------------------------------------------------------------- inicio infAdic -- Z01 - A01
                            xmlNfeElement = xmlNfe.CreateElement("infAdic");
                            XmlNode xmlNfeNodeInfNfeInfAdic = xmlNfeElement;
                            xmlNfeNodeInfNfe.AppendChild(xmlNfeNodeInfNfeInfAdic);
                            xmlNfe.AppendChild(xmlNfeNode);
                            //----------------------------------------------------------------- fim

                            if (!regrasNfe.verificaGrupoInformacoesAdicionais(InformacoesAdicionaisNFe.infAdic_infAdFisco, InformacoesAdicionaisNFe.infAdic_infCpl))
                            {
                                // ---------------------------------------------------------------- inicio infAdFisco -- Z02 - Z01
                                if (regrasNfe.verificaCampoVazio(InformacoesAdicionaisNFe.infAdic_infAdFisco))
                                {
                                    xmlNfeElement = xmlNfe.CreateElement("infAdFisco");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(InformacoesAdicionaisNFe.infAdic_infAdFisco, 2, 1, 2000, false, null, "Z02");
                                    XmlNode xmlNfeNodeInfNfeInfAdicInfAdFisco = xmlNfeElement;
                                    xmlNfeNodeInfNfeInfAdic.AppendChild(xmlNfeNodeInfNfeInfAdicInfAdFisco);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                }
                                //----------------------------------------------------------------- fim

                                // ---------------------------------------------------------------- inicio infCpl -- Z03 - Z01
                                if (regrasNfe.verificaCampoVazio(InformacoesAdicionaisNFe.infAdic_infCpl))
                                {
                                    xmlNfeElement = xmlNfe.CreateElement("infCpl");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(InformacoesAdicionaisNFe.infAdic_infCpl, 2, 1, 5000, false, null, "Z03");
                                    XmlNode xmlNfeNodeInfNfeInfAdicInfCpl = xmlNfeElement;
                                    xmlNfeNodeInfNfeInfAdic.AppendChild(xmlNfeNodeInfNfeInfAdicInfCpl);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                }
                                //----------------------------------------------------------------- fim
                            }

                            if (!regrasNfe.verificaCampoLivreContribuinte(InformacoesAdicionaisNFe.infAdic_obsCont_xCampo, InformacoesAdicionaisNFe.infAdic_obsCont_xTexto))
                            {
                                for (int p = 0; p < listaInformacoesAdicionaisNFe.Count; p++)
                                {
                                    if (p == 10)
                                        break;
                                    if (listaInformacoesAdicionaisNFe[p].numeroNotaFiscal.Equals(identificacaoNfe.ide_nNF))
                                    {
                                        // ---------------------------------------------------------------- inicio obsCont -- Z04 - Z01
                                        xmlNfeElement = xmlNfe.CreateElement("obsCont");
                                        xmlNfeElement.SetAttribute(null, "xCampo", regrasNfe.validacaoPadraoCampos(InformacoesAdicionaisNFe.infAdic_obsCont_xCampo, 2, 1, 20, false, null, "Z04")); //xCampo -- Z05 - Z04
                                        XmlNode xmlNfeNodeInfNfeInfAdicObsCont = xmlNfeElement;
                                        xmlNfeNodeInfNfeInfAdicObsCont.AppendChild(xmlNfeNodeInfNfeInfAdicObsCont);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio xTexto -- Z06 - Z04
                                        xmlNfeElement = xmlNfe.CreateElement("xTexto");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(InformacoesAdicionaisNFe.infAdic_obsCont_xTexto, 2, 1, 60, false, null, "Z06");
                                        XmlNode xmlNfeNodeInfNfeInfAdicObsContXTexto = xmlNfeElement;
                                        xmlNfeNodeInfNfeInfAdicObsCont.AppendChild(xmlNfeNodeInfNfeInfAdicObsContXTexto);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim
                                    }
                                }
                            }

                            // ---------------------------------------------------------------- inicio obsFisco -- Z07 - Z01
                            xmlNfeElement = xmlNfe.CreateElement("obsFisco");
                            xmlNfeElement.SetAttribute(null, "xCampo", ""); //xCampo -- Z08 - Z07
                            XmlNode xmlNfeNodeInfNfeInfAdicObsFisco = xmlNfeElement;
                            xmlNfeNodeInfNfeInfAdicObsFisco.AppendChild(xmlNfeNodeInfNfeInfAdicObsFisco);
                            xmlNfe.AppendChild(xmlNfeNode);
                            //----------------------------------------------------------------- fim

                            // ---------------------------------------------------------------- inicio xCampo -- Z09 - Z07
                            xmlNfeElement = xmlNfe.CreateElement("xTexto");
                            xmlNfeElement.InnerText = InformacoesAdicionaisNFe.infAdic_obsFisco_xTexto;
                            XmlNode xmlNfeNodeInfNfeInfAdicObsFiscoXTexto = xmlNfeElement;
                            xmlNfeNodeInfNfeInfAdicObsFisco.AppendChild(xmlNfeNodeInfNfeInfAdicObsFiscoXTexto);
                            xmlNfe.AppendChild(xmlNfeNode);
                            //----------------------------------------------------------------- fim

                            if (!regrasNfe.verificaGrupoProcessoReferenciado(InformacoesAdicionaisNFe.infAdic_procRef_nProc, InformacoesAdicionaisNFe.infAdic_procRef_indProc))
                            {
                                for (int s = 0; s < listaInformacoesAdicionaisNFe.Count; s++)
                                {
                                    if (s == 100)
                                        break;
                                    if (listaInformacoesAdicionaisNFe[s].numeroNotaFiscal.Equals(identificacaoNfe.ide_nNF))
                                    {
                                        // ---------------------------------------------------------------- inicio procRef -- Z10 - Z01
                                        xmlNfeElement = xmlNfe.CreateElement("procRef");
                                        XmlNode xmlNfeNodeInfNfeInfAdicProcRef = xmlNfeElement;
                                        xmlNfeNodeInfNfeInfAdic.AppendChild(xmlNfeNodeInfNfeInfAdicProcRef);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio nProc -- Z11 - Z10
                                        xmlNfeElement = xmlNfe.CreateElement("nProc");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(InformacoesAdicionaisNFe.infAdic_procRef_nProc, 2, 1, 60, false, null, "Z11");
                                        XmlNode xmlNfeNodeInfNfeInfAdicProcRefNProc = xmlNfeElement;
                                        xmlNfeNodeInfNfeInfAdicProcRef.AppendChild(xmlNfeNodeInfNfeInfAdicProcRefNProc);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio indProc -- Z12 - Z10
                                        if (regrasNfe.verificaIdentificadorOrigemProcesso(InformacoesAdicionaisNFe.infAdic_procRef_indProc))
                                        {
                                            xmlNfeElement = xmlNfe.CreateElement("indProc");
                                            xmlNfeElement.InnerText = InformacoesAdicionaisNFe.infAdic_procRef_indProc;
                                            XmlNode xmlNfeNodeInfNfeInfAdicProcRefIndProc = xmlNfeElement;
                                            xmlNfeNodeInfNfeInfAdicProcRef.AppendChild(xmlNfeNodeInfNfeInfAdicProcRefIndProc);
                                            xmlNfe.AppendChild(xmlNfeNode);
                                        }
                                        else
                                        {

                                            return null;
                                        }
                                        //----------------------------------------------------------------- fim
                                    }
                                }
                            }
                        }
                    }
                    #endregion

                    #region #ZA - Informações de Comércio Exterior
                    if (listaInformacoesComercioExterior.Count > 0)
                    {
                        InformacoesComercioExterior = listaInformacoesComercioExterior[i];
                        if (!regrasNfe.verificaInformacoesComercioExterior(InformacoesComercioExterior.exporta_UFSaidaPais, InformacoesComercioExterior.exporta_xLocExporta))
                        {
                            // ---------------------------------------------------------------- inicio exporta -- ZA01 - A01
                            xmlNfeElement = xmlNfe.CreateElement("exporta");
                            XmlNode xmlNfeNodeInfNfeExporta = xmlNfeElement;
                            xmlNfeNodeInfNfe.AppendChild(xmlNfeNodeInfNfeExporta);
                            xmlNfe.AppendChild(xmlNfeNode);
                            //----------------------------------------------------------------- fim

                            // ---------------------------------------------------------------- inicio UFSaidaPais -- ZA02 - ZA01
                            xmlNfeElement = xmlNfe.CreateElement("UFSaidaPais");
                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(InformacoesComercioExterior.exporta_UFSaidaPais, 2, 2, 2, false, null, "ZA02");
                            XmlNode xmlNfeNodeInfNfeExportaUFSaidaPais = xmlNfeElement;
                            xmlNfeNodeInfNfeExporta.AppendChild(xmlNfeNodeInfNfeExportaUFSaidaPais);
                            xmlNfe.AppendChild(xmlNfeNode);
                            //----------------------------------------------------------------- fim

                            // ---------------------------------------------------------------- inicio xLocExporta -- ZA03 - ZA01
                            xmlNfeElement = xmlNfe.CreateElement("xLocExporta");
                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(InformacoesComercioExterior.exporta_xLocExporta, 2, 1, 60, false, null, "ZA03");
                            XmlNode xmlNfeNodeInfNfeExportaXLocExporta = xmlNfeElement;
                            xmlNfeNodeInfNfeExporta.AppendChild(xmlNfeNodeInfNfeExportaXLocExporta);
                            xmlNfe.AppendChild(xmlNfeNode);
                            //----------------------------------------------------------------- fim

                            // ---------------------------------------------------------------- inicio xLocDespacho -- ZA04 - ZA01
                            if (regrasNfe.verificaCampoVazio(InformacoesComercioExterior.exporta_xLocDespacho))
                            {
                                xmlNfeElement = xmlNfe.CreateElement("xLocDespacho");
                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(InformacoesComercioExterior.exporta_xLocDespacho, 2, 1, 60, false, null, "ZA04");
                                XmlNode xmlNfeNodeInfNfeExportaXLocDespacho = xmlNfeElement;
                                xmlNfeNodeInfNfeExporta.AppendChild(xmlNfeNodeInfNfeExportaXLocDespacho);
                                xmlNfe.AppendChild(xmlNfeNode);
                            }
                            //----------------------------------------------------------------- fim
                        }
                    }
                    #endregion

                    #region #ZB - Informações de Compras
                    if (regrasNfe.verificaTipoModelo55NFe(identificacaoNfe.ide_mod))
                    {
                        if (listaInformacoesCompras.Count > 0)
                        {
                            InformacoesCompras = listaInformacoesCompras[i];
                            if (!regrasNfe.verificaInformacoesCompra(InformacoesCompras.compra_xNEmp, InformacoesCompras.compra_xPed, InformacoesCompras.compra_xCont))
                            {
                                // ---------------------------------------------------------------- inicio compra -- ZB01 - A01
                                xmlNfeElement = xmlNfe.CreateElement("compra");
                                XmlNode xmlNfeNodeInfNfeCompra = xmlNfeElement;
                                xmlNfeNodeInfNfe.AppendChild(xmlNfeNodeInfNfeCompra);
                                xmlNfe.AppendChild(xmlNfeNode);
                                //----------------------------------------------------------------- fim

                                // ---------------------------------------------------------------- inicio xNEmp -- ZB02 - A01
                                if (regrasNfe.verificaCampoVazio(InformacoesCompras.compra_xNEmp))
                                {
                                    xmlNfeElement = xmlNfe.CreateElement("xNEmp");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(InformacoesCompras.compra_xNEmp, 2, 1, 22, false, null, "ZB02");
                                    XmlNode xmlNfeNodeInfNfeCompraXNEmp = xmlNfeElement;
                                    xmlNfeNodeInfNfeCompra.AppendChild(xmlNfeNodeInfNfeCompraXNEmp);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                }
                                //----------------------------------------------------------------- fim

                                // ---------------------------------------------------------------- inicio xPed -- ZB03 - ZB01
                                if (regrasNfe.verificaCampoVazio(InformacoesCompras.compra_xPed))
                                {
                                    xmlNfeElement = xmlNfe.CreateElement("xPed");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(InformacoesCompras.compra_xPed, 2, 1, 60, false, null, "ZB03");
                                    XmlNode xmlNfeNodeInfNfeCompraXPed = xmlNfeElement;
                                    xmlNfeNodeInfNfeCompra.AppendChild(xmlNfeNodeInfNfeCompraXPed);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                }
                                //----------------------------------------------------------------- fim

                                // ---------------------------------------------------------------- inicio xCont -- ZB04 - ZB01
                                if (regrasNfe.verificaCampoVazio(InformacoesCompras.compra_xCont))
                                {
                                    xmlNfeElement = xmlNfe.CreateElement("xCont");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(InformacoesCompras.compra_xCont, 2, 1, 60, false, null, "ZB04");
                                    XmlNode xmlNfeNodeInfNfeCompraXCont = xmlNfeElement;
                                    xmlNfeNodeInfNfeCompra.AppendChild(xmlNfeNodeInfNfeCompraXCont);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                }
                                //----------------------------------------------------------------- fim
                            }
                        }
                    }
                    #endregion

                    #region #ZC - Informações do Registro de Aquisição de Cana
                    if (listaInformacoesRegistroAquisicaoCana.Count > 0)
                    {
                        InformacoesRegistroAquisicaoCana = listaInformacoesRegistroAquisicaoCana[i];
                        if (!regrasNfe.verificaInformacoesRegistroAquisicaoCana(InformacoesRegistroAquisicaoCana.cana_safra, InformacoesRegistroAquisicaoCana.cana_ref))
                        {
                            // ---------------------------------------------------------------- inicio cana -- ZC01 - A01
                            xmlNfeElement = xmlNfe.CreateElement("cana");
                            XmlNode xmlNfeNodeInfNfeCana = xmlNfeElement;
                            xmlNfeNodeInfNfe.AppendChild(xmlNfeNodeInfNfeCana);
                            xmlNfe.AppendChild(xmlNfeNode);
                            //----------------------------------------------------------------- fim

                            // ---------------------------------------------------------------- inicio safra -- ZC02 - ZC01
                            xmlNfeElement = xmlNfe.CreateElement("safra");
                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(InformacoesRegistroAquisicaoCana.cana_safra, 2, 4, 9, false, null, "ZC02");
                            XmlNode xmlNfeNodeInfNfeCanaSafra = xmlNfeElement;
                            xmlNfeNodeInfNfeCana.AppendChild(xmlNfeNodeInfNfeCanaSafra);
                            xmlNfe.AppendChild(xmlNfeNode);
                            //----------------------------------------------------------------- fim

                            // ---------------------------------------------------------------- inicio ref -- ZC03 - ZC01
                            xmlNfeElement = xmlNfe.CreateElement("ref");
                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(InformacoesRegistroAquisicaoCana.cana_ref, 2, 1, 7, false, null, "ZC03");
                            XmlNode xmlNfeNodeInfNfeCanaRef = xmlNfeElement;
                            xmlNfeNodeInfNfeCana.AppendChild(xmlNfeNodeInfNfeCanaRef);
                            xmlNfe.AppendChild(xmlNfeNode);
                            //----------------------------------------------------------------- fim

                            XmlNode xmlNfeNodeInfNfeCanaForDia = null;
                            for (int d = 0; d < listaInformacoesRegistroAquisicaoCana.Count; d++)
                            {
                                if (d == 31)
                                    break;
                                if (InformacoesRegistroAquisicaoCana.numeroNotaFiscal.Equals(identificacaoNfe.ide_nNF))
                                {
                                    // ---------------------------------------------------------------- inicio forDia -- ZC04 - ZC01
                                    xmlNfeElement = xmlNfe.CreateElement("forDia");
                                    xmlNfeElement.SetAttribute(null, "dia", regrasNfe.validacaoPadraoCampos(InformacoesRegistroAquisicaoCana.cana_forDia_dia, 2, 1, 2, false, null, "ZC04")); //dia -- ZC05 - ZC04
                                    xmlNfeNodeInfNfeCanaForDia = xmlNfeElement;
                                    xmlNfeNodeInfNfeCana.AppendChild(xmlNfeNodeInfNfeCanaForDia);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim

                                    // ---------------------------------------------------------------- inicio qtde -- ZC06 - ZC04
                                    xmlNfeElement = xmlNfe.CreateElement("qtde");
                                    xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(InformacoesRegistroAquisicaoCana.cana_forDia_qtde, 2, 1, 22, false, null, "ZC06");
                                    XmlNode xmlNfeNodeInfNfeCanaForDiaQtde = xmlNfeElement;
                                    xmlNfeNodeInfNfeCanaForDia.AppendChild(xmlNfeNodeInfNfeCanaForDiaQtde);
                                    xmlNfe.AppendChild(xmlNfeNode);
                                    //----------------------------------------------------------------- fim
                                }
                            }

                            // ---------------------------------------------------------------- inicio qTotMes -- ZC07 - ZC04
                            xmlNfeElement = xmlNfe.CreateElement("qTotMes");
                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(InformacoesRegistroAquisicaoCana.cana_forDia_qTotMes, 2, 1, 22, false, null, "ZC07");
                            XmlNode xmlNfeNodeInfNfeCanaForDiaQTotMes = xmlNfeElement;
                            xmlNfeNodeInfNfeCanaForDia.AppendChild(xmlNfeNodeInfNfeCanaForDiaQTotMes);
                            xmlNfe.AppendChild(xmlNfeNode);
                            //----------------------------------------------------------------- fim

                            // ---------------------------------------------------------------- inicio qTotAnt -- ZC08 - ZC04
                            xmlNfeElement = xmlNfe.CreateElement("qTotAnt");
                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(InformacoesRegistroAquisicaoCana.cana_forDia_qTotAnt, 2, 1, 22, false, null, "ZC08");
                            XmlNode xmlNfeNodeInfNfeCanaForDiaQTotAnt = xmlNfeElement;
                            xmlNfeNodeInfNfeCanaForDia.AppendChild(xmlNfeNodeInfNfeCanaForDiaQTotAnt);
                            xmlNfe.AppendChild(xmlNfeNode);
                            //----------------------------------------------------------------- fim

                            // ---------------------------------------------------------------- inicio qTotGer -- ZC09 - ZC04
                            xmlNfeElement = xmlNfe.CreateElement("qTotGer");
                            xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(InformacoesRegistroAquisicaoCana.cana_forDia_qTotGer, 2, 1, 22, false, null, "ZC09");
                            XmlNode xmlNfeNodeInfNfeCanaForDiaQTotGer = xmlNfeElement;
                            xmlNfeNodeInfNfeCanaForDia.AppendChild(xmlNfeNodeInfNfeCanaForDiaQTotGer);
                            xmlNfe.AppendChild(xmlNfeNode);
                            //----------------------------------------------------------------- fim

                            if (!regrasNfe.verificaGrupoDeducoes(InformacoesRegistroAquisicaoCana.cana_deduc_xDed,
                                InformacoesRegistroAquisicaoCana.cana_deduc_vDed, xmlNfeElement.InnerText = InformacoesRegistroAquisicaoCana.cana_deduc_vFor,
                                xmlNfeElement.InnerText = InformacoesRegistroAquisicaoCana.cana_deduc_vTotDed, xmlNfeElement.InnerText = InformacoesRegistroAquisicaoCana.cana_deduc_vLiqFor))
                            {
                                for (int f = 0; f < listaInformacoesRegistroAquisicaoCana.Count; f++)
                                {
                                    if (listaInformacoesRegistroAquisicaoCana[f].numeroNotaFiscal.Equals(identificacaoNfe.ide_nNF))
                                    {
                                        // ---------------------------------------------------------------- inicio deduc -- ZC10 - ZC01
                                        xmlNfeElement = xmlNfe.CreateElement("deduc");
                                        XmlNode xmlNfeNodeInfNfeCanaForDeduc = xmlNfeElement;
                                        xmlNfeNodeInfNfeCana.AppendChild(xmlNfeNodeInfNfeCanaForDeduc);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio xDed -- ZC11 - ZC10
                                        xmlNfeElement = xmlNfe.CreateElement("xDed");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(InformacoesRegistroAquisicaoCana.cana_deduc_xDed, 2, 1, 60, false, null, "ZC11");
                                        XmlNode xmlNfeNodeInfNfeCanaForXDed = xmlNfeElement;
                                        xmlNfeNodeInfNfeCanaForDeduc.AppendChild(xmlNfeNodeInfNfeCanaForXDed);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim

                                        // ---------------------------------------------------------------- inicio vDed -- ZC12 - ZC10
                                        xmlNfeElement = xmlNfe.CreateElement("vDed");
                                        xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(InformacoesRegistroAquisicaoCana.cana_deduc_vDed, 1, 1, 16, false, null, "ZC12");
                                        XmlNode xmlNfeNodeInfNfeCanaForVDed = xmlNfeElement;
                                        xmlNfeNodeInfNfeCanaForDeduc.AppendChild(xmlNfeNodeInfNfeCanaForVDed);
                                        xmlNfe.AppendChild(xmlNfeNode);
                                        //----------------------------------------------------------------- fim
                                    }
                                }

                                // ---------------------------------------------------------------- inicio vDed -- ZC13 - ZC01
                                xmlNfeElement = xmlNfe.CreateElement("vFor");
                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(InformacoesRegistroAquisicaoCana.cana_deduc_vFor, 1, 1, 16, false, null, "ZC13");
                                XmlNode xmlNfeNodeInfNfeCanaVFor = xmlNfeElement;
                                xmlNfeNodeInfNfeCana.AppendChild(xmlNfeNodeInfNfeCanaVFor);
                                xmlNfe.AppendChild(xmlNfeNode);
                                //----------------------------------------------------------------- fim

                                // ---------------------------------------------------------------- inicio vTotDed -- ZC14 - ZC01
                                xmlNfeElement = xmlNfe.CreateElement("vTotDed");
                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(InformacoesRegistroAquisicaoCana.cana_deduc_vTotDed, 1, 1, 16, false, null, "ZC14");
                                XmlNode xmlNfeNodeInfNfeCanaVTotDed = xmlNfeElement;
                                xmlNfeNodeInfNfeCana.AppendChild(xmlNfeNodeInfNfeCanaVTotDed);
                                xmlNfe.AppendChild(xmlNfeNode);
                                //----------------------------------------------------------------- fim

                                // ---------------------------------------------------------------- inicio vLiqFor -- ZC15 - ZC01
                                xmlNfeElement = xmlNfe.CreateElement("vLiqFor");
                                xmlNfeElement.InnerText = regrasNfe.validacaoPadraoCampos(InformacoesRegistroAquisicaoCana.cana_deduc_vLiqFor, 1, 1, 16, false, null, "ZC15");
                                XmlNode xmlNfeNodeInfNfeCanaVLiqFor = xmlNfeElement;
                                xmlNfeNodeInfNfeCana.AppendChild(xmlNfeNodeInfNfeCanaVLiqFor);
                                xmlNfe.AppendChild(xmlNfeNode);
                                //----------------------------------------------------------------- fim
                            }
                        }
                    }
                    #endregion

                    #region #ZZ - Assinatura (Signature)
                    //if (listaAssinatura.Count > 0)
                    //{
                    //assinatura da NFe
                    //----------------------------------------------------------------- início assinatura

                    //Assinatura = listaAssinatura[i];

                    //xmlNfeElement = xmlNfe.CreateElement("Signature");
                    //xmlNfeElement.SetAttribute("xmlns", "http://www.w3.org/2000/09/xmldsig#");
                    //XmlNode xmlSignatureNode = xmlNfeElement;
                    //xmlNfeNodeNF.AppendChild(xmlSignatureNode);
                    //xmlNfe.AppendChild(xmlNfeNode);

                    //xmlNfeElement = xmlNfe.CreateElement("SignedInfo");
                    //XmlNode xmlSignedInfoNode = xmlNfeElement;
                    //xmlSignatureNode.AppendChild(xmlSignedInfoNode);
                    //xmlNfe.AppendChild(xmlNfeNode);

                    //xmlNfeElement = xmlNfe.CreateElement("CanonicalizationMethod");
                    //xmlNfeElement.SetAttribute("Algorithm", "http://www.w3.org/TR/2001/REC-xml-c14n-20010315");
                    //XmlNode xmlCanonicalizationMethodNode = xmlNfeElement;
                    //xmlSignedInfoNode.AppendChild(xmlCanonicalizationMethodNode);
                    //xmlNfe.AppendChild(xmlNfeNode);

                    //xmlNfeElement = xmlNfe.CreateElement("SignatureMethod");
                    //xmlNfeElement.SetAttribute("Algorithm", "http://www.w3.org/2000/09/xmldsig#rsa-sha1");
                    //XmlNode xmlSignatureMethodNode = xmlNfeElement;
                    //xmlSignedInfoNode.AppendChild(xmlSignatureMethodNode);
                    //xmlNfe.AppendChild(xmlNfeNode);

                    //xmlNfeElement = xmlNfe.CreateElement("Reference");
                    //xmlNfeElement.SetAttribute("URI", "#NFe" + chaveAcessoNfe);
                    //XmlNode xmlReferenceNode = xmlNfeElement;
                    //xmlSignedInfoNode.AppendChild(xmlReferenceNode);
                    //xmlNfe.AppendChild(xmlNfeNode);

                    ////xmlNfeElement = xmlNfe.CreateElement("Transform");
                    ////XmlNode xmlTransformsNode = xmlNfeElement;
                    ////xmlReferenceNode.AppendChild(xmlTransformsNode);
                    ////xmlNfe.AppendChild(xmlNfeNode);

                    //xmlNfeElement = xmlNfe.CreateElement("Transform");
                    //xmlNfeElement.SetAttribute("Algorithm", "http://www.w3.org/2000/09/xmldsig#enveloped-signature");
                    //XmlNode xmlTransformNode = xmlNfeElement;
                    //xmlReferenceNode.AppendChild(xmlTransformNode);
                    //xmlNfe.AppendChild(xmlNfeNode);

                    ////xmlNfeElement = xmlNfe.CreateElement("Transform");
                    ////xmlNfeElement.SetAttribute("Algorithm", "http://www.w3.org/TR/2001/REC-xml-c14n-20010315");
                    ////xmlTransformNode = xmlNfeElement;
                    ////xmlTransformNode.AppendChild(xmlTransformsNode);
                    ////xmlNfe.AppendChild(xmlTransformNode);

                    ////for (int g = 0; g < listaAssinatura.Count; g++)
                    ////{
                    ////    if (Assinatura.numeroNotaFiscal.Equals(identificacaoNfe.ide_nNF))
                    ////    {
                    //        xmlNfeElement = xmlNfe.CreateElement("XPath");
                    //        xmlNfeElement.InnerText = Assinatura.xPath;
                    //        XmlNode xmlXPathNode = xmlNfeElement;
                    //        xmlReferenceNode.AppendChild(xmlXPathNode);
                    //        xmlNfe.AppendChild(xmlNfeNode);
                    ////    }
                    ////}

                    //xmlNfeElement = xmlNfe.CreateElement("DigestMethod");
                    //xmlNfeElement.SetAttribute("Algorithm", "http://www.w3.org/2000/09/xmldsig#sha1");
                    //XmlNode xmlDigestMethodNode = xmlNfeElement;
                    //xmlReferenceNode.AppendChild(xmlDigestMethodNode);
                    //xmlNfe.AppendChild(xmlNfeNode);

                    //xmlNfeElement = xmlNfe.CreateElement("DigestValue");
                    //XmlNode xmlDigestValueNode = xmlNfeElement;
                    //xmlDigestValueNode.InnerText = Assinatura.digestValue;
                    //xmlReferenceNode.AppendChild(xmlDigestValueNode);
                    //xmlNfe.AppendChild(xmlNfeNode);

                    //xmlNfeElement = xmlNfe.CreateElement("SignatureValue");
                    //XmlNode xmlSignatureValueNode = xmlNfeElement;
                    //xmlSignatureValueNode.InnerText = "";
                    //xmlSignatureNode.AppendChild(xmlSignatureValueNode);
                    //xmlNfe.AppendChild(xmlNfeNode);

                    //xmlNfeElement = xmlNfe.CreateElement("KeyInfo");
                    //XmlNode xmlKeyInfoNode = xmlNfeElement;
                    //xmlSignatureNode.AppendChild(xmlKeyInfoNode);
                    //xmlNfe.AppendChild(xmlNfeNode);

                    //xmlNfeElement = xmlNfe.CreateElement("X509Data");
                    //XmlNode xmlX509DataNode = xmlNfeElement;
                    //xmlKeyInfoNode.AppendChild(xmlX509DataNode);
                    //xmlNfe.AppendChild(xmlNfeNode);

                    //xmlNfeElement = xmlNfe.CreateElement("X509Certificate");
                    //XmlNode xmlX509CertificateNode = xmlNfeElement;
                    //xmlX509CertificateNode.InnerText = Assinatura.x509Certificate;
                    //xmlX509DataNode.AppendChild(xmlX509CertificateNode);
                    //xmlNfe.AppendChild(xmlNfeNode);

                    //----------------------------------------------------------------- fim da assinatura
                    //}
                    #endregion

                    qtdNFe++;
                    if (qtdNFe == 50 || listaIdentificacaoNfe.Count == i)
                    {
                        //envia o lote com 50 unidades
                        if (qtdNFe == 50)
                            contadorGlobal += 50;
                        qtdNFe = 0;
                    }
                } //fim do looping 50 unidades

                return regrasNfe.enviaNotaFiscalServidor(xmlNfe, chaveAcessoGeradaNfe);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return new dadosConsultaProcessamentoLote(false, null, null);
        }
    }

    #region Classe de dados

    public class dadosChaveAcessoNfe
    {
        public string codUf { get; set; } //02 caracteres
        public string dataEmissao { get; set; } //04 caracteres - formato: AAMM
        public string cnpjEmitente { get; set; } //14 caracteres
        public string modeloNF { get; set; } //02 caracteres
        public string serieNF { get; set; } //03 caracteres
        public string numeroNfe { get; set; } //09 caracteres
        public string formaEmissao { get; set; } //01 caracter
        public string codigoNumero { get; set; } //08 caracteres
        public string dv { get; set; } // 01 caracter - dígito verificador

        public dadosChaveAcessoNfe(
            string _codUF,
            string _dataEmissao,
            string _cnpjEmitente,
            string _modeloNF,
            string _serieNF,
            string _numeroNfe,
            string _formaEmissao,
            string _codigoNumero,
            string _dv)
        {
            codUf = _codUF;
            dataEmissao = _dataEmissao;
            cnpjEmitente = _cnpjEmitente;
            modeloNF = _modeloNF;
            serieNF = _serieNF;
            numeroNfe = _numeroNfe;
            formaEmissao = _formaEmissao;
            codigoNumero = _codigoNumero;
            dv = _dv;
        }
    }

    public class dadosAssinaturaNfe
    {
        public string codUf { get; set; } //02 caracteres
        public string dataEmissao { get; set; } //04 caracteres - formato: AAMM
        public string cnpjEmitente { get; set; } //14 caracteres
        public string modeloNF { get; set; } //02 caracteres
        public string serieNF { get; set; } //03 caracteres
        public string numeroNfe { get; set; } //09 caracteres
        public string codigoNumero { get; set; } //09 caracteres
        public string dv { get; set; } // 01 caracter - dígito verificador

        public dadosAssinaturaNfe(
            string _codUF,
            string _dataEmissao,
            string _cnpjEmitente,
            string _modeloNF,
            string _serieNF,
            string _numeroNfe,
            string _codigoNumero,
            string _dv)
        {
            codUf = _codUF;
            dataEmissao = _dataEmissao;
            cnpjEmitente = _cnpjEmitente;
            modeloNF = _modeloNF;
            serieNF = _serieNF;
            numeroNfe = _numeroNfe;
            codigoNumero = _codigoNumero;
            dv = _dv;
        }
    }

    public class dadosIdentificacaoNfe
    {
        public string ide_cUF { get; set; }
        public string ide_cNF { get; set; }
        public string ide_natOp { get; set; }
        public string ide_indPag { get; set; }
        public string ide_mod { get; set; }
        public string ide_serie { get; set; }
        public string ide_nNF { get; set; }
        public string ide_dhEmi { get; set; }
        public string ide_dhSaiEnt { get; set; }
        public string ide_tpNF { get; set; }
        public string ide_idDest { get; set; }
        public string ide_cMunFG { get; set; }
        public string ide_tpImp { get; set; }
        public string ide_tpEmis { get; set; }
        public string ide_cDV { get; set; }
        public string ide_tpAmb { get; set; }
        public string ide_finNFe { get; set; }
        public string ide_indFinal { get; set; }
        public string ide_indPres { get; set; }
        public string ide_procEmi { get; set; }
        public string ide_verProc { get; set; }
        public string ide_dhCont { get; set; }
        public string ide_xJust { get; set; }

        public dadosIdentificacaoNfe(
            string _ide_cUF,
            string _ide_cNF,
            string _ide_natOp,
            string _ide_indPag,
            string _ide_mod,
            string _ide_serie,
            string _ide_nNF,
            string _ide_dhEmi,
            string _ide_dhSaiEnt,
            string _ide_tpNF,
            string _ide_idDest,
            string _ide_cMunFG,
            string _ide_tpImp,
            string _ide_tpEmis,
            string _ide_cDV,
            string _ide_tpAmb,
            string _ide_finNFe,
            string _ide_indFinal,
            string _ide_indPres,
            string _ide_procEmi,
            string _ide_verProc,
            string _ide_dhCont,
            string _ide_xJust)
        {
            ide_cUF = _ide_cUF;
            ide_cNF = _ide_cNF;
            ide_natOp = _ide_natOp;
            ide_indPag = _ide_indPag;
            ide_mod = _ide_mod;
            ide_serie = _ide_serie;
            ide_nNF = _ide_nNF;
            ide_dhEmi = _ide_dhEmi;
            ide_dhSaiEnt = _ide_dhSaiEnt;
            ide_tpNF = _ide_tpNF;
            ide_idDest = _ide_idDest;
            ide_cMunFG = _ide_cMunFG;
            ide_tpImp = _ide_tpImp;
            ide_tpEmis = _ide_tpEmis;
            ide_cDV = _ide_cDV;
            ide_tpAmb = _ide_tpAmb;
            ide_finNFe = _ide_finNFe;
            ide_indFinal = _ide_indFinal;
            ide_indPres = _ide_indPres;
            ide_procEmi = _ide_procEmi;
            ide_verProc = _ide_verProc;
            ide_dhCont = _ide_dhCont;
            ide_xJust = _ide_xJust;
        }
    }

    public class dadosDocumentoFiscalDiferenciado
    {
        public string ide_nFref_refNFe { get; set; }
        public string ide_nFref_refNfe_refNF_cUF { get; set; }
        public string ide_nFref_refNfe_refNF_AAMM { get; set; }
        public string ide_nFref_refNfe_refNF_CNPJ { get; set; }
        public string ide_nFref_refNfe_refNF_mod { get; set; }
        public string ide_nFref_refNfe_refNF_serie { get; set; }
        public string ide_nFref_refNfe_refNF_nNF { get; set; }
        public string ide_nFref_refNfe_refNFP_cUF { get; set; }
        public string ide_nFref_refNfe_refNFP_AAMM { get; set; }
        public string ide_nFref_refNfe_refNFP_CNPJ { get; set; }
        public string ide_nFref_refNfe_refNFP_CPF { get; set; }
        public string ide_nFref_refNfe_refNFP_IE { get; set; }
        public string ide_nFref_refNfe_refNFP_mod { get; set; }
        public string ide_nFref_refNfe_refNFP_serie { get; set; }
        public string ide_nFref_refNfe_refNFP_nNF { get; set; }
        public string ide_nFref_refNfe_refNFP_refCTe { get; set; }
        public string ide_nFref_refNfe_refECF_mod { get; set; }
        public string ide_nFref_refNfe_refECF_nECF { get; set; }
        public string ide_nFref_refNfe_refECF_nCOO { get; set; }


        public dadosDocumentoFiscalDiferenciado(
            string _ide_nFref_refNFe,
            string _ide_nFref_refNfe_refNF_cUF,
            string _ide_nFref_refNfe_refNF_AAMM,
            string _ide_nFref_refNfe_refNF_CNPJ,
            string _ide_nFref_refNfe_refNF_mod,
            string _ide_nFref_refNfe_refNF_serie,
            string _ide_nFref_refNfe_refNF_nNF,
            string _ide_nFref_refNfe_refNFP_cUF,
            string _ide_nFref_refNfe_refNFP_AAMM,
            string _ide_nFref_refNfe_refNFP_CNPJ,
            string _ide_nFref_refNfe_refNFP_CPF,
            string _ide_nFref_refNfe_refNFP_IE,
            string _ide_nFref_refNfe_refNFP_mod,
            string _ide_nFref_refNfe_refNFP_serie,
            string _ide_nFref_refNfe_refNFP_nNF,
            string _ide_nFref_refNfe_refNFP_refCTe,
            string _ide_nFref_refNfe_refECF_mod,
            string _ide_nFref_refNfe_refECF_nECF,
            string _ide_nFref_refNfe_refECF_nCOO
            )
        {
            ide_nFref_refNFe = _ide_nFref_refNFe;
            ide_nFref_refNfe_refNF_cUF = _ide_nFref_refNfe_refNF_cUF;
            ide_nFref_refNfe_refNF_AAMM = _ide_nFref_refNfe_refNF_AAMM;
            ide_nFref_refNfe_refNF_CNPJ = ide_nFref_refNfe_refNF_CNPJ;
            ide_nFref_refNfe_refNF_mod = _ide_nFref_refNfe_refNF_mod;
            ide_nFref_refNfe_refNF_serie = _ide_nFref_refNfe_refNF_serie;
            ide_nFref_refNfe_refNF_nNF = ide_nFref_refNfe_refNF_nNF;
            ide_nFref_refNfe_refNFP_cUF = _ide_nFref_refNfe_refNFP_cUF;
            ide_nFref_refNfe_refNFP_AAMM = _ide_nFref_refNfe_refNFP_AAMM;
            ide_nFref_refNfe_refNFP_CNPJ = _ide_nFref_refNfe_refNFP_CNPJ;
            ide_nFref_refNfe_refNFP_CPF = _ide_nFref_refNfe_refNFP_CPF;
            ide_nFref_refNfe_refNFP_IE = _ide_nFref_refNfe_refNFP_IE;
            ide_nFref_refNfe_refNFP_mod = _ide_nFref_refNfe_refNFP_mod;
            ide_nFref_refNfe_refNFP_serie = _ide_nFref_refNfe_refNFP_serie;
            ide_nFref_refNfe_refNFP_nNF = _ide_nFref_refNfe_refNFP_nNF;
            ide_nFref_refNfe_refNFP_refCTe = _ide_nFref_refNfe_refNFP_refCTe;
            ide_nFref_refNfe_refECF_mod = _ide_nFref_refNfe_refECF_mod;
            ide_nFref_refNfe_refECF_nECF = _ide_nFref_refNfe_refECF_nECF;
            ide_nFref_refNfe_refECF_nCOO = _ide_nFref_refNfe_refECF_nCOO;
        }
    }

    public class dadosIdentificacaoEmitenteNfe
    {
        public string emit_CNPJ { get; set; }
        public string emit_CPF { get; set; }
        public string emit_xNome { get; set; }
        public string emit_xFant { get; set; }
        public string emit_enderEmi_xLgr { get; set; }
        public string emit_enderEmi_nro { get; set; }
        public string emit_enderEmi_xCpl { get; set; }
        public string emit_enderEmi_xBairro { get; set; }
        public string emit_enderEmi_cMun { get; set; }
        public string emit_enderEmi_xMun { get; set; }
        public string emit_enderEmi_UF { get; set; }
        public string emit_enderEmi_CEP { get; set; }
        public string emit_enderEmi_cPais { get; set; }
        public string emit_enderEmi_xPais { get; set; }
        public string emit_enderEmi_fone { get; set; }
        public string emit_enderEmi_IE { get; set; }
        public string emit_enderEmi_IEST { get; set; }
        public string emit_enderEmi_IM { get; set; }
        public string emit_enderEmi_CNAE { get; set; }
        public string emit_enderEmi_CRT { get; set; }


        public dadosIdentificacaoEmitenteNfe(
            string _emit_CNPJ,
            string _emit_CPF,
            string _emit_xNome,
            string _emit_xFant,
            string _emit_enderEmi_xLgr,
            string _emit_enderEmi_nro,
            string _emit_enderEmi_xCpl,
            string _emit_enderEmi_xBairro,
            string _emit_enderEmi_cMun,
            string _emit_enderEmi_xMun,
            string _emit_enderEmi_UF,
            string _emit_enderEmi_CEP,
            string _emit_enderEmi_cPais,
            string _emit_enderEmi_xPais,
            string _emit_enderEmi_fone,
            string _emit_enderEmi_IE,
            string _emit_enderEmi_IEST,
            string _emit_enderEmi_IM,
            string _emit_enderEmi_CNAE,
            string _emit_enderEmi_CRT
            )
        {
            emit_CNPJ = _emit_CNPJ;
            emit_CPF = _emit_CPF;
            emit_xNome = _emit_xNome;
            emit_xFant = _emit_xFant;
            emit_enderEmi_xLgr = _emit_enderEmi_xLgr;
            emit_enderEmi_nro = _emit_enderEmi_nro;
            emit_enderEmi_xCpl = _emit_enderEmi_xCpl;
            emit_enderEmi_xBairro = _emit_enderEmi_xBairro;
            emit_enderEmi_cMun = _emit_enderEmi_cMun;
            emit_enderEmi_xMun = _emit_enderEmi_xMun;
            emit_enderEmi_UF = _emit_enderEmi_UF;
            emit_enderEmi_CEP = _emit_enderEmi_CEP;
            emit_enderEmi_cPais = _emit_enderEmi_cPais;
            emit_enderEmi_xPais = _emit_enderEmi_xPais;
            emit_enderEmi_fone = _emit_enderEmi_fone;
            emit_enderEmi_IE = _emit_enderEmi_IE;
            emit_enderEmi_IEST = _emit_enderEmi_IEST;
            emit_enderEmi_IM = _emit_enderEmi_IM;
            emit_enderEmi_CNAE = _emit_enderEmi_CNAE;
            emit_enderEmi_CRT = _emit_enderEmi_CRT;
        }
    }

    public class dadosIdentificacaoFiscoEmitenteNfe
    {
        public string avulsa_CNPJ { get; set; }
        public string avulsa_xOrgao { get; set; }
        public string avulsa_matr { get; set; }
        public string avulsa_xAgente { get; set; }
        public string avulsa_fone { get; set; }
        public string avulsa_UF { get; set; }
        public string avulsa_nDAR { get; set; }
        public string avulsa_dEmi { get; set; }
        public string avulsa_vDAR { get; set; }
        public string avulsa_repEmi { get; set; }
        public string avulsa_dPag { get; set; }


        public dadosIdentificacaoFiscoEmitenteNfe(
            string _avulsa_CNPJ,
            string _avulsa_xOrgao,
            string _avulsa_matr,
            string _avulsa_xAgente,
            string _avulsa_fone,
            string _avulsa_UF,
            string _avulsa_nDAR,
            string _avulsa_dEmi,
            string _avulsa_vDAR,
            string _avulsa_repEmi,
            string _avulsa_dPag

            )
        {
            avulsa_CNPJ = _avulsa_CNPJ;
            avulsa_xOrgao = _avulsa_xOrgao;
            avulsa_matr = _avulsa_matr;
            avulsa_xAgente = _avulsa_xAgente;
            avulsa_fone = _avulsa_fone;
            avulsa_UF = _avulsa_UF;
            avulsa_nDAR = _avulsa_nDAR;
            avulsa_dEmi = _avulsa_dEmi;
            avulsa_vDAR = _avulsa_vDAR;
            avulsa_repEmi = _avulsa_repEmi;
            avulsa_dPag = _avulsa_dPag;
        }
    }

    public class dadosIdentificacaoDestinatárioNfe
    {
        public string dest_CNPJ { get; set; }
        public string dest_CPF { get; set; }
        public string dest_idEstrangeiro { get; set; }
        public string dest_xNome { get; set; }
        public string dest_enderDest_xLgr { get; set; }
        public string dest_enderDest_nro { get; set; }
        public string dest_enderDest_xCpl { get; set; }
        public string dest_enderDest_xBairro { get; set; }
        public string dest_enderDest_cMun { get; set; }
        public string dest_enderDest_xMun { get; set; }
        public string dest_enderDest_UF { get; set; }
        public string dest_enderDest_CEP { get; set; }
        public string dest_enderDest_cPais { get; set; }
        public string dest_enderDest_xPais { get; set; }
        public string dest_enderDest_fone { get; set; }
        public string dest_enderDest_indIEDest { get; set; }
        public string dest_enderDest_IE { get; set; }
        public string dest_enderDest_ISUF { get; set; }
        public string dest_enderDest_IM { get; set; }
        public string dest_enderDest_email { get; set; }


        public dadosIdentificacaoDestinatárioNfe(
            string _dest_CNPJ,
            string _dest_CPF,
            string _dest_idEstrangeiro,
            string _dest_xNome,
            string _dest_enderDest_xLgr,
            string _dest_enderDest_nro,
            string _dest_enderDest_xCpl,
            string _dest_enderDest_xBairro,
            string _dest_enderDest_cMun,
            string _dest_enderDest_xMun,
            string _dest_enderDest_UF,
            string _dest_enderDest_CEP,
            string _dest_enderDest_cPais,
            string _dest_enderDest_xPais,
            string _dest_enderDest_fone,
            string _dest_enderDest_indIEDest,
            string _dest_enderDest_IE,
            string _dest_enderDest_ISUF,
            string _dest_enderDest_IM,
            string _dest_enderDest_email
            )
        {
            dest_CNPJ = _dest_CNPJ;
            dest_CPF = _dest_CPF;
            dest_idEstrangeiro = _dest_idEstrangeiro;
            dest_xNome = _dest_xNome;
            dest_enderDest_xLgr = _dest_enderDest_xLgr;
            dest_enderDest_nro = _dest_enderDest_nro;
            dest_enderDest_xCpl = _dest_enderDest_xCpl;
            dest_enderDest_xBairro = _dest_enderDest_xBairro;
            dest_enderDest_cMun = _dest_enderDest_cMun;
            dest_enderDest_xMun = _dest_enderDest_xMun;
            dest_enderDest_UF = _dest_enderDest_UF;
            dest_enderDest_CEP = _dest_enderDest_CEP;
            dest_enderDest_cPais = _dest_enderDest_cPais;
            dest_enderDest_xPais = _dest_enderDest_xPais;
            dest_enderDest_fone = _dest_enderDest_fone;
            dest_enderDest_indIEDest = _dest_enderDest_indIEDest;
            dest_enderDest_IE = _dest_enderDest_IE;
            dest_enderDest_ISUF = _dest_enderDest_ISUF;
            dest_enderDest_IM = _dest_enderDest_IM;
            dest_enderDest_email = _dest_enderDest_email;
        }
    }

    public class dadosIdentificacaoLocalRetirada
    {
        public string retirada_CNPJ { get; set; }
        public string retirada_CPF { get; set; }
        public string retirada_xLgr { get; set; }
        public string retirada_nro { get; set; }
        public string retirada_xCpl { get; set; }
        public string retirada_xBairro { get; set; }
        public string retirada_cMun { get; set; }
        public string retirada_xMun { get; set; }
        public string retirada_UF { get; set; }

        public dadosIdentificacaoLocalRetirada(
            string _retirada_CNPJ,
            string _retirada_CPF,
            string _retirada_xLgr,
            string _retirada_nro,
            string _retirada_xCpl,
            string _retirada_xBairro,
            string _retirada_cMun,
            string _retirada_xMun,
            string _retirada_UF
            )
        {
            retirada_CNPJ = _retirada_CNPJ;
            retirada_CPF = _retirada_CPF;
            retirada_xLgr = _retirada_xLgr;
            retirada_nro = _retirada_nro;
            retirada_xCpl = _retirada_xCpl;
            retirada_xBairro = _retirada_xBairro;
            retirada_cMun = _retirada_cMun;
            retirada_xMun = _retirada_xMun;
            retirada_UF = _retirada_UF;
        }
    }

    public class dadosIdentificacaoLocalEntrega
    {
        public string entrega_CNPJ { get; set; }
        public string entrega_CPF { get; set; }
        public string entrega_xLgr { get; set; }
        public string entrega_nro { get; set; }
        public string entrega_xCpl { get; set; }
        public string entrega_xBairro { get; set; }
        public string entrega_cMun { get; set; }
        public string entrega_xMun { get; set; }
        public string entrega_UF { get; set; }

        public dadosIdentificacaoLocalEntrega(
            string _entrega_CNPJ,
            string _entrega_CPF,
            string _entrega_xLgr,
            string _entrega_nro,
            string _entrega_xCpl,
            string _entrega_xBairro,
            string _entrega_cMun,
            string _entrega_xMun,
            string _entrega_UF
            )
        {
            entrega_CNPJ = _entrega_CNPJ;
            entrega_CPF = _entrega_CPF;
            entrega_xLgr = _entrega_xLgr;
            entrega_nro = _entrega_nro;
            entrega_xCpl = _entrega_xCpl;
            entrega_xBairro = _entrega_xBairro;
            entrega_cMun = _entrega_cMun;
            entrega_xMun = _entrega_xMun;
            entrega_UF = _entrega_UF;
        }
    }

    public class dadosAutorizacaoObterXml
    {
        public string autXML_CNPJ { get; set; }
        public string autXML_CPF { get; set; }
        public int? flagNumeroNota { get; set; }


        public dadosAutorizacaoObterXml(
            string _autXML_CNPJ,
            string _autXML_CPF,
            int? _flagNumeroNota
            )
        {
            autXML_CNPJ = _autXML_CNPJ;
            autXML_CPF = _autXML_CPF;
            flagNumeroNota = _flagNumeroNota;
        }
    }

    public class dadosDetalhamentoProdutosNfe
    {
        public string det_nItem { get; set; }
        public int? flagNumeroNota { get; set; }

        public dadosDetalhamentoProdutosNfe(
            string _det_nItem,
            int? _flagNumeroNota
            )
        {
            det_nItem = _det_nItem;
            flagNumeroNota = _flagNumeroNota;
        }
    }

    public class dadosProdutosServicosNfe
    {
        public string prod_cProd { get; set; }
        public string prod_cEAN { get; set; }
        public string prod_xProd { get; set; }
        public string prod_NCM { get; set; }
        public string prod_NVE { get; set; }
        public string prod_EXTIPI { get; set; }
        public string prod_CFOP { get; set; }
        public string prod_uCom { get; set; }
        public string prod_qCom { get; set; }
        public string prod_vUnCom { get; set; }
        public string prod_vProd { get; set; }
        public string prod_cEANTrib { get; set; }
        public string prod_uTrib { get; set; }
        public string prod_qTrib { get; set; }
        public string prod_vUnTrib { get; set; }
        public string prod_vFrete { get; set; }
        public string prod_vSeg { get; set; }
        public string prod_vDesc { get; set; }
        public string prod_vOutro { get; set; }
        public string prod_indTot { get; set; }


        public dadosProdutosServicosNfe(
            string _prod_cProd,
            string _prod_cEAN,
            string _prod_xProd,
            string _prod_NCM,
            string _prod_NVE,
            string _prod_EXTIPI,
            string _prod_CFOP,
            string _prod_uCom,
            string _prod_qCom,
            string _prod_vUnCom,
            string _prod_vProd,
            string _prod_cEANTrib,
            string _prod_uTrib,
            string _prod_qTrib,
            string _prod_vUnTrib,
            string _prod_vFrete,
            string _prod_vSeg,
            string _prod_vDesc,
            string _prod_vOutro,
            string _prod_indTot
            )
        {
            prod_cProd = _prod_cProd;
            prod_cEAN = _prod_cEAN;
            prod_xProd = _prod_xProd;
            prod_NCM = _prod_NCM;
            prod_NVE = _prod_NVE;
            prod_EXTIPI = _prod_EXTIPI;
            prod_CFOP = _prod_CFOP;
            prod_uCom = _prod_uCom;
            prod_qCom = _prod_qCom;
            prod_vUnCom = _prod_vUnCom;
            prod_vProd = _prod_vProd;
            prod_cEANTrib = _prod_cEANTrib;
            prod_uTrib = _prod_uTrib;
            prod_qTrib = _prod_qTrib;
            prod_vUnTrib = _prod_vUnTrib;
            prod_vFrete = _prod_vFrete;
            prod_vSeg = _prod_vSeg;
            prod_vDesc = _prod_vDesc;
            prod_vOutro = _prod_vOutro;
            prod_indTot = _prod_indTot;
        }
    }

    public class dadosProdutosServicosDeclaracaoImportacao
    {
        public string DI_nDI { get; set; }
        public string DI_dDI { get; set; }
        public string DI_xLocDesemb { get; set; }
        public string DI_UFDesemb { get; set; }
        public string DI_dDesemb { get; set; }
        public string DI_tpViaTransp { get; set; }
        public string DI_vAFRMM { get; set; }
        public string DI_tpIntermedio { get; set; }
        public string DI_CNPJ { get; set; }
        public string DI_UFTerceiro { get; set; }
        public string DI_cExportador { get; set; }
        public string DI_adi_nAdicao { get; set; }
        public string DI_adi_nSeqAdicC { get; set; }
        public string DI_adi_cFabricante { get; set; }
        public string DI_adi_vDescDI { get; set; }
        public string DI_adi_nDraw { get; set; }
        public string numeroItem { get; set; }

        public dadosProdutosServicosDeclaracaoImportacao(
            string _DI_nDI,
            string _DI_dDI,
            string _DI_xLocDesemb,
            string _DI_UFDesemb,
            string _DI_dDesemb,
            string _DI_tpViaTransp,
            string _DI_vAFRMM,
            string _DI_tpIntermedio,
            string _DI_CNPJ,
            string _DI_UFTerceiro,
            string _DI_cExportador,
            string _DI_adi_nAdicao,
            string _DI_adi_nSeqAdicC,
            string _DI_adi_cFabricante,
            string _DI_adi_vDescDI,
            string _DI_adi_nDraw,
            string _numeroItem
            )
        {
            DI_nDI = _DI_nDI;
            DI_dDI = _DI_dDI;
            DI_xLocDesemb = _DI_xLocDesemb;
            DI_UFDesemb = _DI_UFDesemb;
            DI_dDesemb = _DI_dDesemb;
            DI_tpViaTransp = _DI_tpViaTransp;
            DI_vAFRMM = _DI_vAFRMM;
            DI_tpIntermedio = _DI_tpIntermedio;
            DI_CNPJ = _DI_CNPJ;
            DI_UFTerceiro = _DI_UFTerceiro;
            DI_cExportador = _DI_cExportador;
            DI_adi_nAdicao = _DI_adi_nAdicao;
            DI_adi_nSeqAdicC = _DI_adi_nSeqAdicC;
            DI_adi_cFabricante = _DI_adi_cFabricante;
            DI_adi_vDescDI = _DI_adi_vDescDI;
            DI_adi_nDraw = _DI_adi_nDraw;
            numeroItem = _numeroItem;
        }
    }

    public class dadosProdutosServicosGrupoExportacao
    {
        public string detExport_nDraw { get; set; }
        public string detExport_exportInd_nRE { get; set; }
        public string detExport_exportInd_chNFe { get; set; }
        public string detExport_exportInd_qExport { get; set; }
        public string numeroItem { get; set; }

        public dadosProdutosServicosGrupoExportacao(
            string _detExport_nDraw,
            string _detExport_exportInd_nRE,
            string _detExport_exportInd_chNFe,
            string _detExport_exportInd_qExport,
            string _numeroItem
            )
        {
            detExport_nDraw = _detExport_nDraw;
            detExport_exportInd_nRE = _detExport_exportInd_nRE;
            detExport_exportInd_chNFe = _detExport_exportInd_chNFe;
            detExport_exportInd_qExport = _detExport_exportInd_qExport;
            numeroItem = _numeroItem;
        }
    }

    public class dadosProdutosServicosPedidoCompra
    {
        public string DI_xPed { get; set; }
        public string DI_nItemPed { get; set; }

        public dadosProdutosServicosPedidoCompra(
            string _DI_xPed,
            string _DI_nItemPed
            )
        {
            DI_xPed = _DI_xPed;
            DI_nItemPed = _DI_nItemPed;
        }
    }

    public class dadosProdutosServicosGrupoDiversos
    {
        public string DI_nFCI { get; set; }

        public dadosProdutosServicosGrupoDiversos(
            string _DI_nFCI
            )
        {
            DI_nFCI = _DI_nFCI;
        }
    }

    public class dadosDetalhamentoEspecificoVeiculosNovos
    {
        public string veicProd_tpOp { get; set; }
        public string veicProd_chassi { get; set; }
        public string veicProd_cCor { get; set; }
        public string veicProd_xCor { get; set; }
        public string veicProd_pot { get; set; }
        public string veicProd_cilin { get; set; }
        public string veicProd_pesoL { get; set; }
        public string veicProd_pesoB { get; set; }
        public string veicProd_nSerie { get; set; }
        public string veicProd_tpComb { get; set; }
        public string veicProd_nMotor { get; set; }
        public string veicProd_CMT { get; set; }
        public string veicProd_dist { get; set; }
        public string veicProd_anoMod { get; set; }
        public string veicProd_anoFab { get; set; }
        public string veicProd_tpPint { get; set; }
        public string veicProd_tpVeic { get; set; }
        public string veicProd_espVeic { get; set; }
        public string veicProd_VIN { get; set; }
        public string veicProd_condVeic { get; set; }
        public string veicProd_cMod { get; set; }
        public string veicProd_cCorDENATRAN { get; set; }
        public string veicProd_lota { get; set; }
        public string veicProd_tpRest { get; set; }

        public dadosDetalhamentoEspecificoVeiculosNovos(
            string _veicProd_tpOp,
            string _veicProd_chassi,
            string _veicProd_cCor,
            string _veicProd_xCor,
            string _veicProd_pot,
            string _veicProd_cilin,
            string _veicProd_pesoL,
            string _veicProd_pesoB,
            string _veicProd_nSerie,
            string _veicProd_tpComb,
            string _veicProd_nMotor,
            string _veicProd_CMT,
            string _veicProd_dist,
            string _veicProd_anoMod,
            string _veicProd_anoFab,
            string _veicProd_tpPint,
            string _veicProd_tpVeic,
            string _veicProd_espVeic,
            string _veicProd_VIN,
            string _veicProd_condVeic,
            string _veicProd_cMod,
            string _veicProd_cCorDENATRAN,
            string _veicProd_lota,
            string _veicProd_tpRest
            )
        {
            veicProd_tpOp = _veicProd_tpOp;
            veicProd_chassi = _veicProd_chassi;
            veicProd_cCor = _veicProd_cCor;
            veicProd_xCor = _veicProd_xCor;
            veicProd_pot = _veicProd_pot;
            veicProd_cilin = _veicProd_cilin;
            veicProd_pesoL = _veicProd_pesoL;
            veicProd_pesoB = _veicProd_pesoB;
            veicProd_nSerie = _veicProd_nSerie;
            veicProd_tpComb = _veicProd_tpComb;
            veicProd_nMotor = _veicProd_nMotor;
            veicProd_CMT = _veicProd_CMT;
            veicProd_dist = _veicProd_dist;
            veicProd_anoMod = _veicProd_anoMod;
            veicProd_anoFab = _veicProd_anoFab;
            veicProd_tpPint = _veicProd_tpPint;
            veicProd_tpVeic = _veicProd_tpVeic;
            veicProd_espVeic = _veicProd_espVeic;
            veicProd_VIN = _veicProd_VIN;
            veicProd_condVeic = _veicProd_condVeic;
            veicProd_cMod = _veicProd_cMod;
            veicProd_cCorDENATRAN = _veicProd_cCorDENATRAN;
            veicProd_lota = _veicProd_lota;
            veicProd_tpRest = _veicProd_tpRest;
        }
    }

    public class dadosDetalhamentoEspecificoMedicamentoMateriasPrimasFarmaceuticas
    {
        public string med_nLote { get; set; }
        public string med_qLote { get; set; }
        public string med_dFab { get; set; }
        public string med_dVal { get; set; }
        public string med_vPMC { get; set; }
        public string numeroItem { get; set; }

        public dadosDetalhamentoEspecificoMedicamentoMateriasPrimasFarmaceuticas(
            string _med_nLote,
            string _med_qLote,
            string _med_dFab,
            string _med_dVal,
            string _med_vPMC,
            string _numeroItem)
        {
            med_nLote = _med_nLote;
            med_qLote = _med_qLote;
            med_dFab = _med_dFab;
            med_dVal = _med_dVal;
            med_vPMC = _med_vPMC;
            numeroItem = _numeroItem;
        }
    }

    public class dadosDetalhamentoEspecificoArmamentos
    {
        public string arma_tpArma { get; set; }
        public string arma_nSerie { get; set; }
        public string arma_nCano { get; set; }
        public string arma_descr { get; set; }
        public string numeroItem { get; set; }

        public dadosDetalhamentoEspecificoArmamentos(
            string _arma_tpArma,
            string _arma_nSerie,
            string _arma_nCano,
            string _arma_descr,
            string _numeroItem)
        {
            arma_tpArma = _arma_tpArma;
            arma_nSerie = _arma_nSerie;
            arma_nCano = _arma_nCano;
            arma_descr = _arma_descr;
            numeroItem = _numeroItem;
        }
    }

    public class dadosDetalhamentoEspecificoCombustiveis
    {
        public string comb_cProdANP { get; set; }
        public string comb_pMixGN { get; set; }
        public string comb_CODIF { get; set; }
        public string comb_qTemp { get; set; }
        public string comb_UFCons { get; set; }
        public string comb_CIDE_qBCProd { get; set; }
        public string comb_CIDE_vAliqProd { get; set; }
        public string comb_CIDE_vCIDE { get; set; }

        public dadosDetalhamentoEspecificoCombustiveis(
            string _comb_cProdANP,
            string _comb_pMixGN,
            string _comb_CODIF,
            string _comb_qTemp,
            string _comb_UFCons,
            string _comb_CIDE_qBCProd,
            string _comb_CIDE_vAliqProd,
            string _comb_CIDE_vCIDE
            )
        {
            comb_cProdANP = _comb_cProdANP;
            comb_pMixGN = _comb_pMixGN;
            comb_CODIF = _comb_CODIF;
            comb_qTemp = _comb_qTemp;
            comb_UFCons = _comb_UFCons;
            comb_CIDE_qBCProd = _comb_CIDE_qBCProd;
            comb_CIDE_vAliqProd = _comb_CIDE_vAliqProd;
            comb_CIDE_vCIDE = _comb_CIDE_vCIDE;
        }
    }

    public class dadosDetalhamentoEspecificoOperacaoPapelImune
    {
        public string nRECOPI { get; set; }

        public dadosDetalhamentoEspecificoOperacaoPapelImune(string _nRECOPI)
        {
            nRECOPI = _nRECOPI;
        }
    }

    public class dadosTributosIncidentesProdutoServico
    {
        public string imposto_vTotTrib { get; set; }

        public dadosTributosIncidentesProdutoServico(
            string _imposto_vTotTrib)
        {
            imposto_vTotTrib = _imposto_vTotTrib;
        }
    }

    public class dadosICMSNormalST
    {
        public string ICMS_ICMS00_orig { get; set; }
        public string ICMS_ICMS00_CST { get; set; }
        public string ICMS_ICMS00_modBC { get; set; }
        public string ICMS_ICMS00_vBC { get; set; }
        public string ICMS_ICMS00_pICMS { get; set; }
        public string ICMS_ICMS00_vICMS { get; set; }
        public string ICMS_ICMS10_orig { get; set; }
        public string ICMS_ICMS10_CST { get; set; }
        public string ICMS_ICMS10_modBC { get; set; }
        public string ICMS_ICMS10_vBC { get; set; }
        public string ICMS_ICMS10_pICMS { get; set; }
        public string ICMS_ICMS10_vICMS { get; set; }
        public string ICMS_ICMS10_modBCST { get; set; }
        public string ICMS_ICMS10_pMVAST { get; set; }
        public string ICMS_ICMS10_pRedBCST { get; set; }
        public string ICMS_ICMS10_vBCST { get; set; }
        public string ICMS_ICMS10_pICMSST { get; set; }
        public string ICMS_ICMS10_vICMSST { get; set; }
        public string ICMS_ICMS20_orig { get; set; }
        public string ICMS_ICMS20_CST { get; set; }
        public string ICMS_ICMS20_modBC { get; set; }
        public string ICMS_ICMS20_pRedBC { get; set; }
        public string ICMS_ICMS20_vBC { get; set; }
        public string ICMS_ICMS20_pICMS { get; set; }
        public string ICMS_ICMS20_vICMS { get; set; }
        public string ICMS_ICMS20_vICMSDeson { get; set; }
        public string ICMS_ICMS20_motDesICMS { get; set; }
        public string ICMS_ICMS30_orig { get; set; }
        public string ICMS_ICMS30_CST { get; set; }
        public string ICMS_ICMS30_modBCST { get; set; }
        public string ICMS_ICMS30_pMVAST { get; set; }
        public string ICMS_ICMS30_pRedBCST { get; set; }
        public string ICMS_ICMS30_vBCST { get; set; }
        public string ICMS_ICMS30_pICMSST { get; set; }
        public string ICMS_ICMS30_vICMSST { get; set; }
        public string ICMS_ICMS30_vICMSDeson { get; set; }
        public string ICMS_ICMS30_motDesICMS { get; set; }
        public string ICMS_ICMS40_orig { get; set; }
        public string ICMS_ICMS40_CST { get; set; }
        public string ICMS_ICMS40_vICMSDeson { get; set; }
        public string ICMS_ICMS40_motDesICMS { get; set; }
        public string ICMS_ICMS51_orig { get; set; }
        public string ICMS_ICMS51_CST { get; set; }
        public string ICMS_ICMS51_modBC { get; set; }
        public string ICMS_ICMS51_pRedBC { get; set; }
        public string ICMS_ICMS51_vBC { get; set; }
        public string ICMS_ICMS51_pICMS { get; set; }
        public string ICMS_ICMS51_vICMSOp { get; set; }
        public string ICMS_ICMS51_pDif { get; set; }
        public string ICMS_ICMS51_vICMSDif { get; set; }
        public string ICMS_ICMS51_vICMS { get; set; }
        public string ICMS_ICMS60_orig { get; set; }
        public string ICMS_ICMS60_CST { get; set; }
        public string ICMS_ICMS60_vBCSTRet { get; set; }
        public string ICMS_ICMS60_vICMSSTRet { get; set; }
        public string ICMS_ICMS70_orig { get; set; }
        public string ICMS_ICMS70_CST { get; set; }
        public string ICMS_ICMS70_modBC { get; set; }
        public string ICMS_ICMS70_pRedBC { get; set; }
        public string ICMS_ICMS70_vBC { get; set; }
        public string ICMS_ICMS70_pICMS { get; set; }
        public string ICMS_ICMS70_vICMS { get; set; }
        public string ICMS_ICMS70_modBCST { get; set; }
        public string ICMS_ICMS70_pMVAST { get; set; }
        public string ICMS_ICMS70_pRedBCST { get; set; }
        public string ICMS_ICMS70_vBCST { get; set; }
        public string ICMS_ICMS70_pICMSST { get; set; }
        public string ICMS_ICMS70_vICMSST { get; set; }
        public string ICMS_ICMS70_vICMSDeson { get; set; }
        public string ICMS_ICMS70_motDesICMS { get; set; }
        public string ICMS_ICMS90_orig { get; set; }
        public string ICMS_ICMS90_CST { get; set; }
        public string ICMS_ICMS90_modBC { get; set; }
        public string ICMS_ICMS90_vBC { get; set; }
        public string ICMS_ICMS90_pRedBC { get; set; }
        public string ICMS_ICMS90_pICMS { get; set; }
        public string ICMS_ICMS90_vICMS { get; set; }
        public string ICMS_ICMS90_modBCST { get; set; }
        public string ICMS_ICMS90_pMVAST { get; set; }
        public string ICMS_ICMS90_pRedBCST { get; set; }
        public string ICMS_ICMS90_vBCST { get; set; }
        public string ICMS_ICMS90_pICMSST { get; set; }
        public string ICMS_ICMS90_vICMSST { get; set; }
        public string ICMS_ICMS90_vICMSDeson { get; set; }
        public string ICMS_ICMS90_motDesICMS { get; set; }
        public string ICMS_ICMSPart_orig { get; set; }
        public string ICMS_ICMSPart_CST { get; set; }
        public string ICMS_ICMSPart_modBC { get; set; }
        public string ICMS_ICMSPart_vBC { get; set; }
        public string ICMS_ICMSPart_pRedBC { get; set; }
        public string ICMS_ICMSPart_pICMS { get; set; }
        public string ICMS_ICMSPart_vICMS { get; set; }
        public string ICMS_ICMSPart_modBCST { get; set; }
        public string ICMS_ICMSPart_pMVAST { get; set; }
        public string ICMS_ICMSPart_pRedBCST { get; set; }
        public string ICMS_ICMSPart_vBCST { get; set; }
        public string ICMS_ICMSPart_pICMSST { get; set; }
        public string ICMS_ICMSPart_vICMSST { get; set; }
        public string ICMS_ICMSPart_pBCOp { get; set; }
        public string ICMS_ICMSPart_UFST { get; set; }
        public string ICMS_ICMSST_orig { get; set; }
        public string ICMS_ICMSST_CST { get; set; }
        public string ICMS_ICMSST_vBCSTRet { get; set; }
        public string ICMS_ICMSST_vICMSSTRet { get; set; }
        public string ICMS_ICMSST_vBCSTDest { get; set; }
        public string ICMS_ICMSST_vICMSSTDest { get; set; }
        public string ICMS_ICMSSN101_orig { get; set; }
        public string ICMS_ICMSSN101_CSOSN { get; set; }
        public string ICMS_ICMSSN101_pCredSN { get; set; }
        public string ICMS_ICMSSN101_vCredICMSSN { get; set; }
        public string ICMS_ICMSSN102_orig { get; set; }
        public string ICMS_ICMSSN102_CSOSN { get; set; }
        public string ICMS_ICMSSN201_orig { get; set; }
        public string ICMS_ICMSSN201_CSOSN { get; set; }
        public string ICMS_ICMSSN201_modBCST { get; set; }
        public string ICMS_ICMSSN201_pMVAST { get; set; }
        public string ICMS_ICMSSN201_pRedBCST { get; set; }
        public string ICMS_ICMSSN201_vBCST { get; set; }
        public string ICMS_ICMSSN201_pICMSST { get; set; }
        public string ICMS_ICMSSN201_vICMSST { get; set; }
        public string ICMS_ICMSSN201_pCredSN { get; set; }
        public string ICMS_ICMSSN201_vCredICMSSN { get; set; }
        public string ICMS_ICMSSN202_orig { get; set; }
        public string ICMS_ICMSSN202_CSOSN { get; set; }
        public string ICMS_ICMSSN202_modBCST { get; set; }
        public string ICMS_ICMSSN202_pMVAST { get; set; }
        public string ICMS_ICMSSN202_pRedBCST { get; set; }
        public string ICMS_ICMSSN202_vBCST { get; set; }
        public string ICMS_ICMSSN202_pICMSST { get; set; }
        public string ICMS_ICMSSN202_vICMSST { get; set; }
        public string ICMS_ICMSSN500_orig { get; set; }
        public string ICMS_ICMSSN500_CSOSN { get; set; }
        public string ICMS_ICMSSN500_vBCSTRet { get; set; }
        public string ICMS_ICMSSN500_vICMSSTRet { get; set; }
        public string ICMS_ICMSSN900_orig { get; set; }
        public string ICMS_ICMSSN900_CSOSN { get; set; }
        public string ICMS_ICMSSN900_modBC { get; set; }
        public string ICMS_ICMSSN900_vBC { get; set; }
        public string ICMS_ICMSSN900_pRedBC { get; set; }
        public string ICMS_ICMSSN900_pICMS { get; set; }
        public string ICMS_ICMSSN900_vICMS { get; set; }
        public string ICMS_ICMSSN900_modBCST { get; set; }
        public string ICMS_ICMSSN900_pMVAST { get; set; }
        public string ICMS_ICMSSN900_pRedBCST { get; set; }
        public string ICMS_ICMSSN900_vBCST { get; set; }
        public string ICMS_ICMSSN900_pICMSST { get; set; }
        public string ICMS_ICMSSN900_vICMSST { get; set; }
        public string ICMS_ICMSSN900_pCredSN { get; set; }
        public string ICMS_ICMSSN900_vCredICMSSN { get; set; }

        public dadosICMSNormalST(
            string _ICMS_ICMS00_orig,
            string _ICMS_ICMS00_CST,
            string _ICMS_ICMS00_modBC,
            string _ICMS_ICMS00_vBC,
            string _ICMS_ICMS00_pICMS,
            string _ICMS_ICMS00_vICMS,
            string _ICMS_ICMS10_orig,
            string _ICMS_ICMS10_CST,
            string _ICMS_ICMS10_modBC,
            string _ICMS_ICMS10_vBC,
            string _ICMS_ICMS10_pICMS,
            string _ICMS_ICMS10_vICMS,
            string _ICMS_ICMS10_modBCST,
            string _ICMS_ICMS10_pMVAST,
            string _ICMS_ICMS10_pRedBCST,
            string _ICMS_ICMS10_vBCST,
            string _ICMS_ICMS10_pICMSST,
            string _ICMS_ICMS10_vICMSST,
            string _ICMS_ICMS20_orig,
            string _ICMS_ICMS20_CST,
            string _ICMS_ICMS20_modBC,
            string _ICMS_ICMS20_pRedBC,
            string _ICMS_ICMS20_vBC,
            string _ICMS_ICMS20_pICMS,
            string _ICMS_ICMS20_vICMSDeson,
            string _ICMS_ICMS20_motDesICMS,
            string _ICMS_ICMS30_orig,
            string _ICMS_ICMS30_CST,
            string _ICMS_ICMS30_modBCST,
            string _ICMS_ICMS30_pMVAST,
            string _ICMS_ICMS30_pRedBCST,
            string _ICMS_ICMS30_vBCST,
            string _ICMS_ICMS30_pICMSST,
            string _ICMS_ICMS30_vICMSST,
            string _ICMS_ICMS30_vICMSDeson,
            string _ICMS_ICMS30_motDesICMS,
            string _ICMS_ICMS40_orig,
            string _ICMS_ICMS40_CST,
            string _ICMS_ICMS40_vICMSDeson,
            string _ICMS_ICMS40_motDesICMS,
            string _ICMS_ICMS51_orig,
            string _ICMS_ICMS51_CST,
            string _ICMS_ICMS51_modBC,
            string _ICMS_ICMS51_pRedBC,
            string _ICMS_ICMS51_vBC,
            string _ICMS_ICMS51_pICMS,
            string _ICMS_ICMS51_vICMSOp,
            string _ICMS_ICMS51_pDif,
            string _ICMS_ICMS51_vICMSDif,
            string _ICMS_ICMS51_vICMS,
            string _ICMS_ICMS60_CST,
            string _ICMS_ICMS60_vBCSTRet,
            string _ICMS_ICMS60_vICMSSTRet,
            string _ICMS_ICMS70_orig,
            string _ICMS_ICMS70_CST,
            string _ICMS_ICMS70_modBC,
            string _ICMS_ICMS70_pRedBC,
            string _ICMS_ICMS70_vBC,
            string _ICMS_ICMS70_pICMS,
            string _ICMS_ICMS70_vICMS,
            string _ICMS_ICMS70_modBCST,
            string _ICMS_ICMS70_pMVAST,
            string _ICMS_ICMS70_pRedBCST,
            string _ICMS_ICMS70_vBCST,
            string _ICMS_ICMS70_pICMSST,
            string _ICMS_ICMS70_vICMSST,
            string _ICMS_ICMS70_vICMSDeson,
            string _ICMS_ICMS70_motDesICMS,
            string _ICMS_ICMS90_orig,
            string _ICMS_ICMS90_CST,
            string _ICMS_ICMS90_modBC,
            string _ICMS_ICMS90_vBC,
            string _ICMS_ICMS90_pRedBC,
            string _ICMS_ICMS90_pICMS,
            string _ICMS_ICMS90_vICMS,
            string _ICMS_ICMS90_modBCST,
            string _ICMS_ICMS90_pMVAST,
            string _ICMS_ICMS90_pRedBCST,
            string _ICMS_ICMS90_vBCST,
            string _ICMS_ICMS90_pICMSST,
            string _ICMS_ICMS90_vICMSST,
            string _ICMS_ICMS90_vICMSDeson,
            string _ICMS_ICMS90_motDesICMS,
            string _ICMS_ICMSPart_orig,
            string _ICMS_ICMSPart_CST,
            string _ICMS_ICMSPart_modBC,
            string _ICMS_ICMSPart_vBC,
            string _ICMS_ICMSPart_pRedBC,
            string _ICMS_ICMSPart_pICMS,
            string _ICMS_ICMSPart_vICMS,
            string _ICMS_ICMSPart_modBCST,
            string _ICMS_ICMSPart_pMVAST,
            string _ICMS_ICMSPart_pRedBCST,
            string _ICMS_ICMSPart_vBCST,
            string _ICMS_ICMSPart_pICMSST,
            string _ICMS_ICMSPart_vICMSST,
            string _ICMS_ICMSPart_pBCOp,
            string _ICMS_ICMSPart_UFST,
            string _ICMS_ICMSST_orig,
            string _ICMS_ICMSST_CST,
            string _ICMS_ICMSST_vBCSTRet,
            string _ICMS_ICMSST_vICMSSTRet,
            string _ICMS_ICMSST_vBCSTDest,
            string _ICMS_ICMSST_vICMSSTDest,
            string _ICMS_ICMSSN101_orig,
            string _ICMS_ICMSSN101_CSOSN,
            string _ICMS_ICMSSN101_pCredSN,
            string _ICMS_ICMSSN101_vCredICMSSN,
            string _ICMS_ICMSSN102_orig,
            string _ICMS_ICMSSN102_CSOSN,
            string _ICMS_ICMSSN201_orig,
            string _ICMS_ICMSSN201_CSOSN,
            string _ICMS_ICMSSN201_modBCST,
            string _ICMS_ICMSSN201_pMVAST,
            string _ICMS_ICMSSN201_pRedBCST,
            string _ICMS_ICMSSN201_vBCST,
            string _ICMS_ICMSSN201_pICMSST,
            string _ICMS_ICMSSN201_vICMSST,
            string _ICMS_ICMSSN201_pCredSN,
            string _ICMS_ICMSSN201_vCredICMSSN,
            string _ICMS_ICMSSN202_orig,
            string _ICMS_ICMSSN202_CSOSN,
            string _ICMS_ICMSSN202_modBCST,
            string _ICMS_ICMSSN202_pMVAST,
            string _ICMS_ICMSSN202_pRedBCST,
            string _ICMS_ICMSSN202_vBCST,
            string _ICMS_ICMSSN202_pICMSST,
            string _ICMS_ICMSSN202_vICMSST,
            string _ICMS_ICMSSN500_orig,
            string _ICMS_ICMSSN500_CSOSN,
            string _ICMS_ICMSSN500_vBCSTRet,
            string _ICMS_ICMSSN500_vICMSSTRet,
            string _ICMS_ICMSSN900_orig,
            string _ICMS_ICMSSN900_CSOSN,
            string _ICMS_ICMSSN900_modBC,
            string _ICMS_ICMSSN900_vBC,
            string _ICMS_ICMSSN900_pRedBC,
            string _ICMS_ICMSSN900_pICMS,
            string _ICMS_ICMSSN900_vICMS,
            string _ICMS_ICMSSN900_modBCST,
            string _ICMS_ICMSSN900_pMVAST,
            string _ICMS_ICMSSN900_pRedBCST,
            string _ICMS_ICMSSN900_vBCST,
            string _ICMS_ICMSSN900_pICMSST,
            string _ICMS_ICMSSN900_vICMSST,
            string _ICMS_ICMSSN900_pCredSN,
            string _ICMS_ICMSSN900_vCredICMSSN
            )
        {
            ICMS_ICMS00_orig = _ICMS_ICMS00_orig;
            ICMS_ICMS00_CST = _ICMS_ICMS00_CST;
            ICMS_ICMS00_modBC = _ICMS_ICMS00_modBC;
            ICMS_ICMS00_vBC = _ICMS_ICMS00_vBC;
            ICMS_ICMS00_pICMS = _ICMS_ICMS00_pICMS;
            ICMS_ICMS00_vICMS = _ICMS_ICMS00_vICMS;
            ICMS_ICMS10_orig = _ICMS_ICMS10_orig;
            ICMS_ICMS10_CST = _ICMS_ICMS10_CST;
            ICMS_ICMS10_modBC = _ICMS_ICMS10_modBC;
            ICMS_ICMS10_vBC = _ICMS_ICMS10_vBC;
            ICMS_ICMS10_pICMS = _ICMS_ICMS10_pICMS;
            ICMS_ICMS10_vICMS = _ICMS_ICMS10_vICMS;
            ICMS_ICMS10_modBCST = _ICMS_ICMS10_modBCST;
            ICMS_ICMS10_pMVAST = _ICMS_ICMS10_pMVAST;
            ICMS_ICMS10_pRedBCST = _ICMS_ICMS10_pRedBCST;
            ICMS_ICMS10_vBCST = _ICMS_ICMS10_vBCST;
            ICMS_ICMS10_pICMSST = _ICMS_ICMS10_pICMSST;
            ICMS_ICMS10_vICMSST = _ICMS_ICMS10_vICMSST;
            ICMS_ICMS20_orig = _ICMS_ICMS20_orig;
            ICMS_ICMS20_CST = _ICMS_ICMS20_CST;
            ICMS_ICMS20_modBC = _ICMS_ICMS20_modBC;
            ICMS_ICMS20_pRedBC = _ICMS_ICMS20_pRedBC;
            ICMS_ICMS20_vBC = _ICMS_ICMS20_vBC;
            ICMS_ICMS20_pICMS = _ICMS_ICMS20_pICMS;
            ICMS_ICMS20_vICMSDeson = _ICMS_ICMS20_vICMSDeson;
            ICMS_ICMS20_motDesICMS = _ICMS_ICMS20_motDesICMS;
            ICMS_ICMS30_orig = _ICMS_ICMS30_orig;
            ICMS_ICMS30_CST = _ICMS_ICMS30_CST;
            ICMS_ICMS30_modBCST = _ICMS_ICMS30_modBCST;
            ICMS_ICMS30_pMVAST = _ICMS_ICMS30_pMVAST;
            ICMS_ICMS30_pRedBCST = _ICMS_ICMS30_pRedBCST;
            ICMS_ICMS30_vBCST = _ICMS_ICMS30_vBCST;
            ICMS_ICMS30_pICMSST = _ICMS_ICMS30_pICMSST;
            ICMS_ICMS30_vICMSST = _ICMS_ICMS30_vICMSST;
            ICMS_ICMS30_vICMSDeson = _ICMS_ICMS30_vICMSDeson;
            ICMS_ICMS30_motDesICMS = _ICMS_ICMS30_motDesICMS;
            ICMS_ICMS40_orig = _ICMS_ICMS40_orig;
            ICMS_ICMS40_CST = _ICMS_ICMS40_CST;
            ICMS_ICMS40_vICMSDeson = _ICMS_ICMS40_vICMSDeson;
            ICMS_ICMS40_motDesICMS = _ICMS_ICMS40_motDesICMS;
            ICMS_ICMS51_orig = _ICMS_ICMS51_orig;
            ICMS_ICMS51_CST = _ICMS_ICMS51_CST;
            ICMS_ICMS51_modBC = _ICMS_ICMS51_modBC;
            ICMS_ICMS51_pRedBC = _ICMS_ICMS51_pRedBC;
            ICMS_ICMS51_vBC = _ICMS_ICMS51_vBC;
            ICMS_ICMS51_pICMS = _ICMS_ICMS51_pICMS;
            ICMS_ICMS51_vICMSOp = _ICMS_ICMS51_vICMSOp;
            ICMS_ICMS51_pDif = _ICMS_ICMS51_pDif;
            ICMS_ICMS51_vICMSDif = _ICMS_ICMS51_vICMSDif;
            ICMS_ICMS51_vICMS = _ICMS_ICMS51_vICMS;
            ICMS_ICMS60_CST = _ICMS_ICMS60_CST;
            ICMS_ICMS60_vBCSTRet = _ICMS_ICMS60_vBCSTRet;
            ICMS_ICMS60_vICMSSTRet = _ICMS_ICMS60_vICMSSTRet;
            ICMS_ICMS70_orig = _ICMS_ICMS70_orig;
            ICMS_ICMS70_CST = _ICMS_ICMS70_CST;
            ICMS_ICMS70_modBC = _ICMS_ICMS70_modBC;
            ICMS_ICMS70_pRedBC = _ICMS_ICMS70_pRedBC;
            ICMS_ICMS70_vBC = _ICMS_ICMS70_vBC;
            ICMS_ICMS70_pICMS = _ICMS_ICMS70_pICMS;
            ICMS_ICMS70_vICMS = _ICMS_ICMS70_vICMS;
            ICMS_ICMS70_modBCST = _ICMS_ICMS70_modBCST;
            ICMS_ICMS70_pMVAST = _ICMS_ICMS70_pMVAST;
            ICMS_ICMS70_pRedBCST = _ICMS_ICMS70_pRedBCST;
            ICMS_ICMS70_vBCST = _ICMS_ICMS70_vBCST;
            ICMS_ICMS70_pICMSST = _ICMS_ICMS70_pICMSST;
            ICMS_ICMS70_vICMSST = _ICMS_ICMS70_vICMSST;
            ICMS_ICMS70_vICMSDeson = _ICMS_ICMS70_vICMSDeson;
            ICMS_ICMS70_motDesICMS = _ICMS_ICMS70_motDesICMS;
            ICMS_ICMS90_orig = _ICMS_ICMS90_orig;
            ICMS_ICMS90_CST = _ICMS_ICMS90_CST;
            ICMS_ICMS90_modBC = _ICMS_ICMS90_modBC;
            ICMS_ICMS90_vBC = _ICMS_ICMS90_vBC;
            ICMS_ICMS90_pRedBC = _ICMS_ICMS90_pRedBC;
            ICMS_ICMS90_pICMS = _ICMS_ICMS90_pICMS;
            ICMS_ICMS90_vICMS = _ICMS_ICMS90_vICMS;
            ICMS_ICMS90_modBCST = _ICMS_ICMS90_modBCST;
            ICMS_ICMS90_pMVAST = _ICMS_ICMS90_pMVAST;
            ICMS_ICMS90_pRedBCST = _ICMS_ICMS90_pRedBCST;
            ICMS_ICMS90_vBCST = _ICMS_ICMS90_vBCST;
            ICMS_ICMS90_pICMSST = _ICMS_ICMS90_pICMSST;
            ICMS_ICMS90_vICMSST = _ICMS_ICMS90_vICMSST;
            ICMS_ICMS90_vICMSDeson = _ICMS_ICMS90_vICMSDeson;
            ICMS_ICMS90_motDesICMS = _ICMS_ICMS90_motDesICMS;
            ICMS_ICMSPart_orig = _ICMS_ICMSPart_orig;
            ICMS_ICMSPart_CST = _ICMS_ICMSPart_CST;
            ICMS_ICMSPart_modBC = _ICMS_ICMSPart_modBC;
            ICMS_ICMSPart_vBC = _ICMS_ICMSPart_vBC;
            ICMS_ICMSPart_pRedBC = _ICMS_ICMSPart_pRedBC;
            ICMS_ICMSPart_pICMS = _ICMS_ICMSPart_pICMS;
            ICMS_ICMSPart_vICMS = _ICMS_ICMSPart_vICMS;
            ICMS_ICMSPart_modBCST = _ICMS_ICMSPart_modBCST;
            ICMS_ICMSPart_pMVAST = _ICMS_ICMSPart_pMVAST;
            ICMS_ICMSPart_pRedBCST = _ICMS_ICMSPart_pRedBCST;
            ICMS_ICMSPart_vBCST = _ICMS_ICMSPart_vBCST;
            ICMS_ICMSPart_pICMSST = _ICMS_ICMSPart_pICMSST;
            ICMS_ICMSPart_vICMSST = _ICMS_ICMSPart_vICMSST;
            ICMS_ICMSPart_pBCOp = _ICMS_ICMSPart_pBCOp;
            ICMS_ICMSPart_UFST = _ICMS_ICMSPart_UFST;
            ICMS_ICMSST_orig = _ICMS_ICMSST_orig;
            ICMS_ICMSST_CST = _ICMS_ICMSST_CST;
            ICMS_ICMSST_vBCSTRet = _ICMS_ICMSST_vBCSTRet;
            ICMS_ICMSST_vICMSSTRet = _ICMS_ICMSST_vICMSSTRet;
            ICMS_ICMSST_vBCSTDest = _ICMS_ICMSST_vBCSTDest;
            ICMS_ICMSST_vICMSSTDest = _ICMS_ICMSST_vICMSSTDest;
            ICMS_ICMSSN101_orig = _ICMS_ICMSSN101_orig;
            ICMS_ICMSSN101_CSOSN = _ICMS_ICMSSN101_CSOSN;
            ICMS_ICMSSN101_pCredSN = _ICMS_ICMSSN101_pCredSN;
            ICMS_ICMSSN101_vCredICMSSN = _ICMS_ICMSSN101_vCredICMSSN;
            ICMS_ICMSSN102_orig = _ICMS_ICMSSN102_orig;
            ICMS_ICMSSN102_CSOSN = _ICMS_ICMSSN102_CSOSN;
            ICMS_ICMSSN201_orig = _ICMS_ICMSSN201_orig;
            ICMS_ICMSSN201_CSOSN = _ICMS_ICMSSN201_CSOSN;
            ICMS_ICMSSN201_modBCST = _ICMS_ICMSSN201_modBCST;
            ICMS_ICMSSN201_pMVAST = _ICMS_ICMSSN201_pMVAST;
            ICMS_ICMSSN201_pRedBCST = _ICMS_ICMSSN201_pRedBCST;
            ICMS_ICMSSN201_vBCST = _ICMS_ICMSSN201_vBCST;
            ICMS_ICMSSN201_pICMSST = _ICMS_ICMSSN201_pICMSST;
            ICMS_ICMSSN201_vICMSST = _ICMS_ICMSSN201_vICMSST;
            ICMS_ICMSSN201_pCredSN = _ICMS_ICMSSN201_pCredSN;
            ICMS_ICMSSN201_vCredICMSSN = _ICMS_ICMSSN201_vCredICMSSN;
            ICMS_ICMSSN202_orig = _ICMS_ICMSSN202_orig;
            ICMS_ICMSSN202_CSOSN = _ICMS_ICMSSN202_CSOSN;
            ICMS_ICMSSN202_modBCST = _ICMS_ICMSSN202_modBCST;
            ICMS_ICMSSN202_pMVAST = _ICMS_ICMSSN202_pMVAST;
            ICMS_ICMSSN202_pRedBCST = _ICMS_ICMSSN202_pRedBCST;
            ICMS_ICMSSN202_vBCST = _ICMS_ICMSSN202_vBCST;
            ICMS_ICMSSN202_pICMSST = _ICMS_ICMSSN202_pICMSST;
            ICMS_ICMSSN202_vICMSST = _ICMS_ICMSSN202_vICMSST;
            ICMS_ICMSSN500_orig = _ICMS_ICMSSN500_orig;
            ICMS_ICMSSN500_CSOSN = _ICMS_ICMSSN500_CSOSN;
            ICMS_ICMSSN500_vBCSTRet = _ICMS_ICMSSN500_vBCSTRet;
            ICMS_ICMSSN500_vICMSSTRet = _ICMS_ICMSSN500_vICMSSTRet;
            ICMS_ICMSSN900_orig = _ICMS_ICMSSN900_orig;
            ICMS_ICMSSN900_CSOSN = _ICMS_ICMSSN900_CSOSN;
            ICMS_ICMSSN900_modBC = _ICMS_ICMSSN900_modBC;
            ICMS_ICMSSN900_vBC = _ICMS_ICMSSN900_vBC;
            ICMS_ICMSSN900_pRedBC = _ICMS_ICMSSN900_pRedBC;
            ICMS_ICMSSN900_pICMS = _ICMS_ICMSSN900_pICMS;
            ICMS_ICMSSN900_vICMS = _ICMS_ICMSSN900_vICMS;
            ICMS_ICMSSN900_modBCST = _ICMS_ICMSSN900_modBCST;
            ICMS_ICMSSN900_pMVAST = _ICMS_ICMSSN900_pMVAST;
            ICMS_ICMSSN900_pRedBCST = _ICMS_ICMSSN900_pRedBCST;
            ICMS_ICMSSN900_vBCST = _ICMS_ICMSSN900_vBCST;
            ICMS_ICMSSN900_pICMSST = _ICMS_ICMSSN900_pICMSST;
            ICMS_ICMSSN900_vICMSST = _ICMS_ICMSSN900_vICMSST;
            ICMS_ICMSSN900_pCredSN = _ICMS_ICMSSN900_pCredSN;
            ICMS_ICMSSN900_vCredICMSSN = _ICMS_ICMSSN900_vCredICMSSN;
        }
    }

    public class dadosImpostoProdutosIndustrializados
    {
        public string IPI_clEnq { get; set; }
        public string IPI_CNPJProd { get; set; }
        public string IPI_cSelo { get; set; }
        public string IPI_qSelo { get; set; }
        public string IPI_cEnq { get; set; }
        public string IPI_IPITrib_CST { get; set; }
        public string IPI_IPITrib_vBC { get; set; }
        public string IPI_IPITrib_pIPI { get; set; }
        public string IPI_IPITrib_qUnid { get; set; }
        public string IPI_IPITrib_vUnid { get; set; }
        public string IPI_IPITrib_vIPI { get; set; }
        public string IPI_IPINT_CST { get; set; }

        public dadosImpostoProdutosIndustrializados(
            string _IPI_clEnq,
            string _IPI_CNPJProd,
            string _IPI_qSelo,
            string _IPI_cEnq,
            string _IPI_IPITrib_CST,
            string _IPI_IPITrib_vBC,
            string _IPI_IPITrib_pIPI,
            string _IPI_IPITrib_qUnid,
            string _IPI_IPITrib_vUnid,
            string _IPI_IPITrib_vIPI,
            string _IPI_IPINT_CST
            )
        {
            IPI_clEnq = _IPI_clEnq;
            IPI_CNPJProd = _IPI_CNPJProd;
            IPI_qSelo = _IPI_qSelo;
            IPI_cEnq = _IPI_cEnq;
            IPI_IPITrib_CST = _IPI_IPITrib_CST;
            IPI_IPITrib_vBC = _IPI_IPITrib_vBC;
            IPI_IPITrib_pIPI = _IPI_IPITrib_pIPI;
            IPI_IPITrib_qUnid = _IPI_IPITrib_qUnid;
            IPI_IPITrib_vUnid = _IPI_IPITrib_vUnid;
            IPI_IPITrib_vIPI = _IPI_IPITrib_vIPI;
            IPI_IPINT_CST = _IPI_IPINT_CST;
        }
    }

    public class dadosImpostoImportacao
    {
        public string II_vBC { get; set; }
        public string II_vDespAdu { get; set; }
        public string II_vII { get; set; }
        public string II_vIOF { get; set; }

        public dadosImpostoImportacao(
            string _II_vBC,
            string _II_vDespAdu,
            string _II_vII,
            string _II_vIOF
            )
        {
            II_vBC = _II_vBC;
            II_vDespAdu = _II_vDespAdu;
            II_vII = _II_vII;
            II_vIOF = _II_vIOF;
        }
    }

    public class dadosPis
    {
        public string PIS_PISAliq_CST { get; set; }
        public string PIS_PISAliq_vBC { get; set; }
        public string PIS_PISAliq_pPIS { get; set; }
        public string PIS_PISAliq_vPIS { get; set; }
        public string PIS_PISQtde_CST { get; set; }
        public string PIS_PISQtde_qBCProd { get; set; }
        public string PIS_PISQtde_vAliqProd { get; set; }
        public string PIS_PISQtde_vPIS { get; set; }
        public string PIS_PISNT_CST { get; set; }
        public string PIS_PISOutr_CST { get; set; }
        public string PIS_PISOutr_vBC { get; set; }
        public string PIS_PISOutr_pPIS { get; set; }
        public string PIS_PISOutr_qBCProd { get; set; }
        public string PIS_PISOutr_vAliqProd { get; set; }
        public string PIS_PISOutr_vPIS { get; set; }

        public dadosPis(
            string _PIS_PISAliq_CST,
            string _PIS_PISAliq_vBC,
            string _PIS_PISAliq_pPIS,
            string _PIS_PISAliq_vPIS,
            string _PIS_PISQtde_CST,
            string _PIS_PISQtde_qBCProd,
            string _PIS_PISQtde_vAliqProd,
            string _PIS_PISQtde_vPIS,
            string _PIS_PISNT_CST,
            string _PIS_PISOutr_CST,
            string _PIS_PISOutr_vBC,
            string _PIS_PISOutr_pPIS,
            string _PIS_PISOutr_qBCProd,
            string _PIS_PISOutr_vAliqProd,
            string _PIS_PISOutr_vPIS
            )
        {
            PIS_PISAliq_CST = _PIS_PISAliq_CST;
            PIS_PISAliq_vBC = _PIS_PISAliq_vBC;
            PIS_PISAliq_pPIS = _PIS_PISAliq_pPIS;
            PIS_PISAliq_vPIS = _PIS_PISAliq_vPIS;
            PIS_PISQtde_CST = _PIS_PISQtde_CST;
            PIS_PISQtde_qBCProd = _PIS_PISQtde_qBCProd;
            PIS_PISQtde_vAliqProd = _PIS_PISQtde_vAliqProd;
            PIS_PISQtde_vPIS = _PIS_PISQtde_vPIS;
            PIS_PISNT_CST = _PIS_PISNT_CST;
            PIS_PISOutr_CST = _PIS_PISOutr_CST;
            PIS_PISOutr_vBC = _PIS_PISOutr_vBC;
            PIS_PISOutr_pPIS = _PIS_PISOutr_pPIS;
            PIS_PISOutr_qBCProd = _PIS_PISOutr_qBCProd;
            PIS_PISOutr_vAliqProd = _PIS_PISOutr_vAliqProd;
            PIS_PISOutr_vPIS = _PIS_PISOutr_vPIS;
        }
    }

    public class dadosPisST
    {
        public string PISST_vBC { get; set; }
        public string PISST_pPIS { get; set; }
        public string PISST_qBCProd { get; set; }
        public string PISST_vAliqProd { get; set; }
        public string PISST_vPIS { get; set; }

        public dadosPisST(
            string _PISST_vBC,
            string _PISST_pPIS,
            string _PISST_vBCqBCProd,
            string _PISST_vAliqProd,
            string _PISST_vPIS
            )
        {
            PISST_vBC = _PISST_vBC;
            PISST_pPIS = _PISST_pPIS;
            PISST_vBC = _PISST_vBC;
            PISST_vAliqProd = _PISST_vAliqProd;
            PISST_vPIS = _PISST_vPIS;
        }
    }

    public class dadosCofins
    {
        public string COFINS_COFINSAliq_CST { get; set; }
        public string COFINS_COFINSAliq_vBC { get; set; }
        public string COFINS_COFINSAliq_pCOFINS { get; set; }
        public string COFINS_COFINSAliq_vCOFINS { get; set; }
        public string COFINS_COFINSQtde_CST { get; set; }
        public string COFINS_COFINSQtde_qBCProd { get; set; }
        public string COFINS_COFINSQtde_vAliqProd { get; set; }
        public string COFINS_COFINSQtde_vCOFINS { get; set; }
        public string COFINS_COFINSNT_CST { get; set; }
        public string COFINS_COFINSOutr_CST { get; set; }
        public string COFINS_COFINSOutr_vBC { get; set; }
        public string COFINS_COFINSOutr_pCOFINS { get; set; }
        public string COFINS_COFINSOutr_qBCProd { get; set; }
        public string COFINS_COFINSOutr_vAliqProd { get; set; }
        public string COFINS_COFINSOutr_vCOFINS { get; set; }


        public dadosCofins(
            string _COFINS_COFINSAliq_CST,
            string _COFINS_COFINSAliq_vBC,
            string _COFINS_COFINSAliq_pCOFINS,
            string _COFINS_COFINSAliq_vCOFINS,
            string _COFINS_COFINSQtde_CST,
            string _COFINS_COFINSQtde_qBCProd,
            string _COFINS_COFINSQtde_vAliqProd,
            string _COFINS_COFINSQtde_vCOFINS,
            string _COFINS_COFINSNT_CST,
            string _COFINS_COFINSOutr_CST,
            string _COFINS_COFINSOutr_vBC,
            string _COFINS_COFINSOutr_pCOFINS,
            string _COFINS_COFINSOutr_qBCProd,
            string _COFINS_COFINSOutr_vAliqProd,
            string _COFINS_COFINSOutr_vCOFINS
            )
        {
            COFINS_COFINSAliq_CST = _COFINS_COFINSAliq_CST;
            COFINS_COFINSAliq_vBC = _COFINS_COFINSAliq_vBC;
            COFINS_COFINSAliq_pCOFINS = _COFINS_COFINSAliq_pCOFINS;
            COFINS_COFINSAliq_vCOFINS = _COFINS_COFINSAliq_vCOFINS;
            COFINS_COFINSQtde_CST = _COFINS_COFINSQtde_CST;
            COFINS_COFINSQtde_qBCProd = _COFINS_COFINSQtde_qBCProd;
            COFINS_COFINSQtde_vAliqProd = _COFINS_COFINSQtde_vAliqProd;
            COFINS_COFINSQtde_vCOFINS = _COFINS_COFINSQtde_vCOFINS;
            COFINS_COFINSNT_CST = _COFINS_COFINSNT_CST;
            COFINS_COFINSOutr_CST = _COFINS_COFINSOutr_CST;
            COFINS_COFINSOutr_vBC = _COFINS_COFINSOutr_vBC;
            COFINS_COFINSOutr_pCOFINS = _COFINS_COFINSOutr_pCOFINS;
            COFINS_COFINSOutr_qBCProd = _COFINS_COFINSOutr_qBCProd;
            COFINS_COFINSOutr_vAliqProd = _COFINS_COFINSOutr_vAliqProd;
            COFINS_COFINSOutr_vCOFINS = _COFINS_COFINSOutr_vCOFINS;
        }
    }

    public class dadosCofinsST
    {
        public string COFINSST_vBC { get; set; }
        public string COFINSST_pCOFINS { get; set; }
        public string COFINSST_qBCProd { get; set; }
        public string COFINSST_vAliqProd { get; set; }
        public string COFINSST_vCOFINS { get; set; }

        public dadosCofinsST(
            string _COFINSST_vBC,
            string _COFINSST_pCOFINS,
            string _COFINSST_qBCProd,
            string _COFINSST_vAliqProd,
            string _COFINSST_vCOFINS
            )
        {
            COFINSST_vBC = _COFINSST_vBC;
            COFINSST_pCOFINS = _COFINSST_pCOFINS;
            COFINSST_qBCProd = _COFINSST_qBCProd;
            COFINSST_vAliqProd = _COFINSST_vAliqProd;
            COFINSST_vCOFINS = _COFINSST_vCOFINS;
        }
    }

    public class dadosISSQN
    {
        public string ISSQN_vBC { get; set; }
        public string ISSQN_vAliq { get; set; }
        public string ISSQN_vISSQN { get; set; }
        public string ISSQN_cMunFG { get; set; }
        public string ISSQN_cListServ { get; set; }
        public string ISSQN_vDeducao { get; set; }
        public string ISSQN_vOutro { get; set; }
        public string ISSQN_vDescIncond { get; set; }
        public string ISSQN_vDescCond { get; set; }
        public string ISSQN_vISSRet { get; set; }
        public string ISSQN_indISS { get; set; }
        public string ISSQN_cServico { get; set; }
        public string ISSQN_cMun { get; set; }
        public string ISSQN_cPais { get; set; }
        public string ISSQN_nProcesso { get; set; }
        public string ISSQN_indIncentivo { get; set; }

        public dadosISSQN(
            string _ISSQN_vBC,
            string _ISSQN_vAliq,
            string _ISSQN_vISSQN,
            string _ISSQN_cMunFG,
            string _ISSQN_cListServ,
            string _ISSQN_vDeducao,
            string _ISSQN_vOutro,
            string _ISSQN_vDescIncond,
            string _ISSQN_vDescCond,
            string _ISSQN_vISSRet,
            string _ISSQN_indISS,
            string _ISSQN_cServico,
            string _ISSQN_cMun,
            string _ISSQN_cPais,
            string _ISSQN_nProcesso,
            string _ISSQN_indIncentivo
            )
        {
            ISSQN_vBC = _ISSQN_vBC;
            ISSQN_vAliq = _ISSQN_vAliq;
            ISSQN_vISSQN = _ISSQN_vISSQN;
            ISSQN_cMunFG = _ISSQN_cMunFG;
            ISSQN_cListServ = _ISSQN_cListServ;
            ISSQN_vDeducao = _ISSQN_vDeducao;
            ISSQN_vOutro = _ISSQN_vOutro;
            ISSQN_vDescIncond = _ISSQN_vDescIncond;
            ISSQN_vDescCond = _ISSQN_vDescCond;
            ISSQN_vISSRet = _ISSQN_vISSRet;
            ISSQN_indISS = _ISSQN_indISS;
            ISSQN_cServico = _ISSQN_cServico;
            ISSQN_cMun = _ISSQN_cMun;
            ISSQN_cPais = _ISSQN_cPais;
            ISSQN_nProcesso = _ISSQN_nProcesso;
            ISSQN_indIncentivo = _ISSQN_indIncentivo;
        }
    }

    public class dadosTributosDevolvidos
    {
        public string impostoDevol_pDevol { get; set; }
        public string impostoDevol_IPI { get; set; }
        public string impostoDevol_vIPIDevol { get; set; }

        public dadosTributosDevolvidos(
            string _impostoDevol_pDevol,
            string _impostoDevol_IPI,
            string _impostoDevol_vIPIDevol
            )
        {
            impostoDevol_pDevol = _impostoDevol_pDevol;
            impostoDevol_IPI = _impostoDevol_IPI;
            impostoDevol_vIPIDevol = _impostoDevol_vIPIDevol;
        }
    }

    public class dadosInformacoesAdicionais
    {
        public string ISSQN_infAdProd { get; set; }

        public dadosInformacoesAdicionais(
            string _ISSQN_infAdProd
            )
        {
            ISSQN_infAdProd = _ISSQN_infAdProd;
        }
    }

    public class dadosTotalNFe
    {
        public string total_ICMSTot_vBC { get; set; }
        public string total_ICMSTot_vICMS { get; set; }
        public string total_ICMSTot_vICMSDeson { get; set; }
        public string total_ICMSTot_vBCST { get; set; }
        public string total_ICMSTot_vST { get; set; }
        public string total_ICMSTot_vProd { get; set; }
        public string total_ICMSTot_vFrete { get; set; }
        public string total_ICMSTot_vSeg { get; set; }
        public string total_ICMSTot_vDesc { get; set; }
        public string total_ICMSTot_vII { get; set; }
        public string total_ICMSTot_vIPI { get; set; }
        public string total_ICMSTot_vPIS { get; set; }
        public string total_ICMSTot_vCOFINS { get; set; }
        public string total_ICMSTot_vOutro { get; set; }
        public string total_ICMSTot_vNF { get; set; }
        public string total_ICMSTot_vTotTrib { get; set; }

        public dadosTotalNFe(
            string _total_ICMSTot_vBC,
            string _total_ICMSTot_vICMS,
            string _total_ICMSTot_vICMSDeson,
            string _total_ICMSTot_vBCST,
            string _total_ICMSTot_vST,
            string _total_ICMSTot_vProd,
            string _total_ICMSTot_vFrete,
            string _total_ICMSTot_vSeg,
            string _total_ICMSTot_vDesc,
            string _total_ICMSTot_vII,
            string _total_ICMSTot_vIPI,
            string _total_ICMSTot_vPIS,
            string _total_ICMSTot_vCOFINS,
            string _total_ICMSTot_vOutro,
            string _total_ICMSTot_vNF,
            string _total_ICMSTot_vTotTrib
            )
        {
            total_ICMSTot_vBC = _total_ICMSTot_vBC;
            total_ICMSTot_vICMS = _total_ICMSTot_vICMS;
            total_ICMSTot_vICMSDeson = _total_ICMSTot_vICMSDeson;
            total_ICMSTot_vBCST = _total_ICMSTot_vBCST;
            total_ICMSTot_vST = _total_ICMSTot_vST;
            total_ICMSTot_vProd = _total_ICMSTot_vProd;
            total_ICMSTot_vFrete = _total_ICMSTot_vFrete;
            total_ICMSTot_vSeg = _total_ICMSTot_vSeg;
            total_ICMSTot_vDesc = _total_ICMSTot_vDesc;
            total_ICMSTot_vII = _total_ICMSTot_vII;
            total_ICMSTot_vIPI = _total_ICMSTot_vIPI;
            total_ICMSTot_vPIS = _total_ICMSTot_vPIS;
            total_ICMSTot_vCOFINS = _total_ICMSTot_vCOFINS;
            total_ICMSTot_vOutro = _total_ICMSTot_vOutro;
            total_ICMSTot_vNF = _total_ICMSTot_vNF;
            total_ICMSTot_vTotTrib = _total_ICMSTot_vTotTrib;
        }
    }

    public class dadosTotalNFeISSQN
    {
        public string total_ISSQNtot_vServ { get; set; }
        public string total_ISSQNtot_vBC { get; set; }
        public string total_ISSQNtot_vISS { get; set; }
        public string total_ISSQNtot_vPIS { get; set; }
        public string total_ISSQNtot_vCOFINS { get; set; }
        public string total_ISSQNtot_dCompet { get; set; }
        public string total_ISSQNtot_vDeducao { get; set; }
        public string total_ISSQNtot_vOutro { get; set; }
        public string total_ISSQNtot_vDescIncond { get; set; }
        public string total_ISSQNtot_vDescCond { get; set; }
        public string total_ISSQNtot_vISSRet { get; set; }
        public string total_ISSQNtot_cRegTrib { get; set; }

        public dadosTotalNFeISSQN(
            string _total_ISSQNtot_vServ,
            string _total_ISSQNtot_vBC,
            string _total_ISSQNtot_vISS,
            string _total_ISSQNtot_vPIS,
            string _total_ISSQNtot_vCOFINS,
            string _total_ISSQNtot_dCompet,
            string _total_ISSQNtot_vDeducao,
            string _total_ISSQNtot_vOutro,
            string _total_ISSQNtot_vDescIncond,
            string _total_ISSQNtot_vDescCond,
            string _total_ISSQNtot_vISSRet,
            string _total_ISSQNtot_cRegTrib
            )
        {
            total_ISSQNtot_vServ = _total_ISSQNtot_vServ;
            total_ISSQNtot_vBC = _total_ISSQNtot_vBC;
            total_ISSQNtot_vISS = _total_ISSQNtot_vISS;
            total_ISSQNtot_vPIS = _total_ISSQNtot_vPIS;
            total_ISSQNtot_vCOFINS = _total_ISSQNtot_vCOFINS;
            total_ISSQNtot_dCompet = _total_ISSQNtot_dCompet;
            total_ISSQNtot_vDeducao = _total_ISSQNtot_vDeducao;
            total_ISSQNtot_vOutro = _total_ISSQNtot_vOutro;
            total_ISSQNtot_vDescIncond = _total_ISSQNtot_vDescIncond;
            total_ISSQNtot_vDescCond = _total_ISSQNtot_vDescCond;
            total_ISSQNtot_vISSRet = _total_ISSQNtot_vISSRet;
            total_ISSQNtot_cRegTrib = _total_ISSQNtot_cRegTrib;
        }
    }

    public class dadosTotalNFeRetencaoTributos
    {
        public string total_retTrib_vRetPIS { get; set; }
        public string total_retTrib_vRetCOFINS { get; set; }
        public string total_retTrib_vRetCSLL { get; set; }
        public string total_retTrib_vBCIRRF { get; set; }
        public string total_retTrib_vIRRF { get; set; }
        public string total_retTrib_vBCRetPrev { get; set; }
        public string total_retTrib_vRetPrev { get; set; }

        public dadosTotalNFeRetencaoTributos(
            string _total_retTrib_vRetPIS,
            string _total_retTrib_vRetCOFINS,
            string _total_retTrib_vRetCSLL,
            string _total_retTrib_vBCIRRF,
            string _total_retTrib_vIRRF,
            string _total_retTrib_vBCRetPrev,
            string _total_retTrib_vRetPrev
            )
        {
            total_retTrib_vRetPIS = _total_retTrib_vRetPIS;
            total_retTrib_vRetCOFINS = _total_retTrib_vRetCOFINS;
            total_retTrib_vRetCSLL = _total_retTrib_vRetCSLL;
            total_retTrib_vBCIRRF = _total_retTrib_vBCIRRF;
            total_retTrib_vIRRF = _total_retTrib_vIRRF;
            total_retTrib_vBCRetPrev = _total_retTrib_vBCRetPrev;
            total_retTrib_vRetPrev = _total_retTrib_vRetPrev;
        }
    }

    public class dadosInformacoesTransporteNFe
    {
        public string transp_modFrete { get; set; }
        public string transp_transporta_CNPJ { get; set; }
        public string transp_transporta_CPF { get; set; }
        public string transp_transporta_xNome { get; set; }
        public string transp_transporta_IE { get; set; }
        public string transp_transporta_xEnder { get; set; }
        public string transp_transporta_xMun { get; set; }
        public string transp_transporta_UF { get; set; }
        public string transp_retTransp_vServ { get; set; }
        public string transp_retTransp_vBCRet { get; set; }
        public string transp_retTransp_pICMSRet { get; set; }
        public string transp_retTransp_vICMSRet { get; set; }
        public string transp_retTransp_CFOP { get; set; }
        public string transp_retTransp_cMunFG { get; set; }
        public string transp_veicTransp_placa { get; set; }
        public string transp_veicTransp_UF { get; set; }
        public string transp_veicTransp_RNTC { get; set; }
        public string transp_reboque_placa { get; set; }
        public string transp_reboque_UF { get; set; }
        public string transp_reboque_RNTC { get; set; }
        public string transp_reboque_vagao { get; set; }
        public string transp_reboque_balsa { get; set; }
        public string transp_vol_qVol { get; set; }
        public string transp_vol_esp { get; set; }
        public string transp_vol_marca { get; set; }
        public string transp_vol_nVol { get; set; }
        public string transp_vol_pesoL { get; set; }
        public string transp_vol_pesoB { get; set; }
        public string transp_vol_lacres_nLacre { get; set; }
        public string numeroNotaFiscal { get; set; }

        public dadosInformacoesTransporteNFe(
            string _transp_modFrete,
            string _transp_transporta_CNPJ,
            string _transp_transporta_xNome,
            string _transp_transporta_IE,
            string _transp_transporta_xEnder,
            string _transp_transporta_xMun,
            string _transp_transporta_UF,
            string _transp_retTransp_vServ,
            string _transp_retTransp_vBCRet,
            string _transp_retTransp_pICMSRet,
            string _transp_retTransp_vICMSRet,
            string _transp_retTransp_CFOP,
            string _transp_retTransp_cMunFG,
            string _transp_veicTransp_placa,
            string _transp_veicTransp_UF,
            string _transp_veicTransp_RNTC,
            string _transp_reboque_placa,
            string _transp_reboque_UF,
            string _transp_reboque_RNTC,
            string _transp_reboque_vagao,
            string _transp_reboque_balsa,
            string _transp_vol_qVol,
            string _transp_vol_esp,
            string _transp_vol_marca,
            string _transp_vol_nVol,
            string _transp_vol_pesoL,
            string _transp_vol_pesoB,
            string _transp_vol_lacres_nLacre,
            string _numeroNotaFiscal
            )
        {
            transp_modFrete = _transp_modFrete;
            transp_transporta_CNPJ = _transp_transporta_CNPJ;
            transp_transporta_xNome = _transp_transporta_xNome;
            transp_transporta_IE = _transp_transporta_IE;
            transp_transporta_xEnder = _transp_transporta_xEnder;
            transp_transporta_xMun = _transp_transporta_xMun;
            transp_transporta_UF = _transp_transporta_UF;
            transp_retTransp_vServ = _transp_retTransp_vServ;
            transp_retTransp_vBCRet = _transp_retTransp_vBCRet;
            transp_retTransp_pICMSRet = _transp_retTransp_pICMSRet;
            transp_retTransp_vICMSRet = _transp_retTransp_vICMSRet;
            transp_retTransp_CFOP = _transp_retTransp_CFOP;
            transp_retTransp_cMunFG = _transp_retTransp_cMunFG;
            transp_veicTransp_placa = _transp_veicTransp_placa;
            transp_veicTransp_UF = _transp_veicTransp_UF;
            transp_veicTransp_RNTC = _transp_veicTransp_RNTC;
            transp_reboque_placa = _transp_reboque_placa;
            transp_reboque_UF = _transp_reboque_UF;
            transp_reboque_RNTC = _transp_reboque_RNTC;
            transp_reboque_vagao = _transp_reboque_vagao;
            transp_reboque_balsa = _transp_reboque_balsa;
            transp_vol_qVol = _transp_vol_qVol;
            transp_vol_esp = _transp_vol_esp;
            transp_vol_marca = _transp_vol_marca;
            transp_vol_nVol = _transp_vol_nVol;
            transp_vol_pesoL = _transp_vol_pesoL;
            transp_vol_pesoB = _transp_vol_pesoB;
            transp_vol_lacres_nLacre = _transp_vol_lacres_nLacre;
            numeroNotaFiscal = _numeroNotaFiscal;
        }
    }

    public class dadosDadosCobranca
    {
        public string cobr_fat_nFat { get; set; }
        public string cobr_fat_vOrig { get; set; }
        public string cobr_fat_vDesc { get; set; }
        public string cobr_fat_vLiq { get; set; }
        public string cobr_dup_nDup { get; set; }
        public string cobr_dup_dVenc { get; set; }
        public string cobr_dup_vDup { get; set; }
        public string numeroNotaFiscal { get; set; }

        public dadosDadosCobranca(
            string _cobr_fat_nFat,
            string _cobr_fat_vOrig,
            string _cobr_fat_vDesc,
            string _cobr_fat_vLiq,
            string _cobr_dup_nDup,
            string _cobr_dup_dVenc,
            string _cobr_dup_vDup,
            string _numeroNotaFiscal
            )
        {
            cobr_fat_nFat = _cobr_fat_nFat;
            cobr_fat_vOrig = _cobr_fat_vOrig;
            cobr_fat_vDesc = _cobr_fat_vDesc;
            cobr_fat_vLiq = _cobr_fat_vLiq;
            cobr_dup_nDup = _cobr_dup_nDup;
            cobr_dup_dVenc = _cobr_dup_dVenc;
            cobr_dup_vDup = _cobr_dup_vDup;
            numeroNotaFiscal = _numeroNotaFiscal;
        }
    }

    public class dadosFormasPagamento
    {
        public string pag_tPag { get; set; }
        public string pag_vPag { get; set; }
        public string pag_card_CNPJ { get; set; }
        public string pag_card_tBand { get; set; }
        public string pag_card_cAut { get; set; }
        public string numeroNotaFiscal { get; set; }


        public dadosFormasPagamento(
            string _pag_tPag,
            string _pag_vPag,
            string _pag_card_CNPJ,
            string _pag_card_tBand,
            string _pag_card_cAut,
            string _numeroNotaFiscal
            )
        {
            pag_tPag = _pag_tPag;
            pag_vPag = _pag_vPag;
            pag_card_CNPJ = _pag_card_CNPJ;
            pag_card_tBand = _pag_card_tBand;
            pag_card_cAut = _pag_card_cAut;
            numeroNotaFiscal = _numeroNotaFiscal;
        }
    }

    public class dadosInformacoesAdicionaisNFe
    {
        public string infAdic_infAdFisco { get; set; }
        public string infAdic_infCpl { get; set; }
        public string infAdic_obsCont_xCampo { get; set; }
        public string infAdic_obsCont_xTexto { get; set; }
        public string infAdic_obsFisco_xCampo { get; set; }
        public string infAdic_obsFisco_xTexto { get; set; }
        public string infAdic_procRef_nProc { get; set; }
        public string infAdic_procRef_indProc { get; set; }
        public string numeroNotaFiscal { get; set; }


        public dadosInformacoesAdicionaisNFe(
            string _infAdic_infAdFisco,
            string _infAdic_infCpl,
            string _infAdic_obsCont_xCampo,
            string _infAdic_obsCont_xTexto,
            string _infAdic_obsFisco_xCampo,
            string _infAdic_obsFisco_xTexto,
            string _infAdic_procRef_nProc,
            string _infAdic_procRef_indProc,
            string _numeroNotaFiscal
            )
        {
            infAdic_infAdFisco = _infAdic_infAdFisco;
            infAdic_infCpl = _infAdic_infCpl;
            infAdic_obsCont_xCampo = _infAdic_obsCont_xCampo;
            infAdic_obsCont_xTexto = _infAdic_obsCont_xTexto;
            infAdic_obsFisco_xCampo = _infAdic_obsFisco_xCampo;
            infAdic_obsFisco_xTexto = _infAdic_obsFisco_xTexto;
            infAdic_procRef_nProc = _infAdic_procRef_nProc;
            infAdic_procRef_indProc = _infAdic_procRef_indProc;
            numeroNotaFiscal = _numeroNotaFiscal;
        }
    }

    public class dadosInformacoesComercioExterior
    {
        public string exporta_UFSaidaPais { get; set; }
        public string exporta_xLocExporta { get; set; }
        public string exporta_xLocDespacho { get; set; }

        public dadosInformacoesComercioExterior(
            string _exporta_UFSaidaPais,
            string _exporta_xLocExporta,
            string _exporta_xLocDespacho
            )
        {
            exporta_UFSaidaPais = _exporta_UFSaidaPais;
            exporta_xLocExporta = _exporta_xLocExporta;
            exporta_xLocDespacho = _exporta_xLocDespacho;
        }
    }

    public class dadosInformacoesCompras
    {
        public string compra_xNEmp { get; set; }
        public string compra_xPed { get; set; }
        public string compra_xCont { get; set; }

        public dadosInformacoesCompras(
            string _compra_xNEmp,
            string _compra_xPed,
            string _compra_xCont

            )
        {
            compra_xNEmp = _compra_xNEmp;
            compra_xPed = _compra_xPed;
            compra_xCont = _compra_xCont;
        }
    }

    public class dadosInformacoesRegistroAquisicaoCana
    {
        public string cana_safra { get; set; }
        public string cana_ref { get; set; }
        public string cana_forDia_dia { get; set; }
        public string cana_forDia_qtde { get; set; }
        public string cana_forDia_qTotMes { get; set; }
        public string cana_forDia_qTotAnt { get; set; }
        public string cana_forDia_qTotGer { get; set; }
        public string cana_deduc_xDed { get; set; }
        public string cana_deduc_vDed { get; set; }
        public string cana_deduc_vFor { get; set; }
        public string cana_deduc_vTotDed { get; set; }
        public string cana_deduc_vLiqFor { get; set; }
        public string numeroNotaFiscal { get; set; }

        public dadosInformacoesRegistroAquisicaoCana(
            string _cana_safra,
            string _cana_ref,
            string _cana_forDia_dia,
            string _cana_forDia_qtde,
            string _cana_forDia_qTotMes,
            string _cana_forDia_qTotAnt,
            string _cana_forDia_qTotGer,
            string _cana_deduc_xDed,
            string _cana_deduc_vDed,
            string _cana_deduc_vFor,
            string _cana_deduc_vTotDed,
            string _cana_deduc_vLiqFor,
            string _numeroNotaFiscal
            )
        {
            cana_safra = _cana_safra;
            cana_ref = _cana_ref;
            cana_forDia_dia = _cana_forDia_dia;
            cana_forDia_qtde = _cana_forDia_qtde;
            cana_forDia_qTotMes = _cana_forDia_qTotMes;
            cana_forDia_qTotAnt = _cana_forDia_qTotAnt;
            cana_forDia_qTotGer = _cana_forDia_qTotGer;
            cana_deduc_xDed = _cana_deduc_xDed;
            cana_deduc_vDed = _cana_deduc_vDed;
            cana_deduc_vFor = _cana_deduc_vFor;
            cana_deduc_vTotDed = _cana_deduc_vTotDed;
            cana_deduc_vLiqFor = _cana_deduc_vLiqFor;
            numeroNotaFiscal = _numeroNotaFiscal;
        }
    }

    public class dadosEnvioNfeLote
    {
        public string idlote { get; set; }
        public string indSinc { get; set; }

        public dadosEnvioNfeLote(
            string _idlote, string _indSinc
            )
        {
            idlote = _idlote;
            indSinc = _indSinc;
        }
    }

    public class dadosAssinatura
    {
        public string xPath { get; set; }
        public string digestValue { get; set; }
        public string x509Certificate { get; set; }
        public string numeroNotaFiscal { get; set; }

        public dadosAssinatura(
            string _xPath,
            string _digestValue,
            string _x509Certificate,
            string _numeroNotaFiscal)
        {
            xPath = _xPath;
            digestValue = _digestValue;
            x509Certificate = _x509Certificate;
            numeroNotaFiscal = _numeroNotaFiscal;
        }
    }

    public class dadosConsultaProcessamentoLote
    {
        public bool statusEnvio { get; set; }
        public string tpAmb { get; set; }
        public string nRec { get; set; }

        public dadosConsultaProcessamentoLote(
            bool _statusEnvio,
            string _tpAmb,
            string _nRec
            )
        {
            statusEnvio = _statusEnvio;
            tpAmb = _tpAmb;
            nRec = _nRec;
        }
    }

    public class dadosCancelaNfe
    {
        public string Id { get; set; }
        public string tpAmb { get; set; }
        public string xServ { get; set; }
        public string chNFe { get; set; }
        public string nProt { get; set; }
        public string xJust { get; set; }
        public string Signature { get; set; }

        public dadosCancelaNfe(
            string _Id,
            string _tpAmb,
            string _xServ,
            string _chNFe,
            string _nProt,
            string _xJust,
            string _Signature
            )
        {
            Id = _Id;
            tpAmb = _tpAmb;
            xServ = _xServ;
            chNFe = _chNFe;
            nProt = _nProt;
            xJust = _xJust;
            Signature = _Signature;
        }
    }

    public class dadosInutilizacaoNFe
    {
        public string Id { get; set; }
        public string tpAmb { get; set; }
        public string xServ { get; set; }
        public string cUF { get; set; }
        public string ano { get; set; }
        public string CNPJ { get; set; }
        public string mod { get; set; }
        public string serie { get; set; }
        public string nNFIni { get; set; }
        public string nNFFin { get; set; }
        public string xJust { get; set; }
        public string Signature { get; set; }

        public dadosInutilizacaoNFe(
            string _Id,
            string _tpAmb,
            string _xServ,
            string _cUF,
            string _ano,
            string _CNPJ,
            string _mod,
            string _serie,
            string _nNFIni,
            string _nNFFin,
            string _xJust,
            string _Signature
            )
        {
            Id = _Id;
            tpAmb = _tpAmb;
            xServ = _xServ;
            cUF = _cUF;
            ano = _ano;
            CNPJ = _CNPJ;
            mod = _mod;
            serie = _serie;
            nNFIni = _nNFIni;
            nNFFin = _nNFFin;
            xJust = _xJust;
            Signature = _Signature;
        }
    }

    public class dadosProtocoloNFe
    {
        public string versao { get; set; }
        public string tpAmb { get; set; }
        public string xServ { get; set; }
        public string chNFe { get; set; }

        public dadosProtocoloNFe(
            string _versao,
            string _tpAmb,
            string _xServ,
            string _chNFe
            )
        {
            versao = _versao;
            tpAmb = _tpAmb;
            xServ = _xServ;
            chNFe = _chNFe;
        }
    }

    public class dadosStatusServidor
    {
        public string tpAmb { get; set; }
        public string cUF { get; set; }
        public string xServ { get; set; }

        public dadosStatusServidor(
            string _tpAmb,
            string _cUF,
            string _xServ
            )
        {
            tpAmb = _tpAmb;
            cUF = _cUF;
            xServ = _xServ;
        }
    }

    public class dadosCadastroContribuinte
    {
        public string xServ { get; set; }
        public string UF { get; set; }
        public string IE { get; set; }
        public string CNPJ { get; set; }
        public string CPF { get; set; }

        public dadosCadastroContribuinte(
            string _xServ,
            string _UF,
            string _IE,
            string _CNPJ,
            string _CPF
            )
        {
            xServ = _xServ;
            UF = _UF;
            IE = _IE;
            CNPJ = _CNPJ;
            CPF = _CPF;
        }
    }

    public class dadosCartaCorrecao
    {
        public string idLote { get; set; }
        public string Id { get; set; }
        public string cOrgao { get; set; }
        public string tpAmb { get; set; }
        public string CNPJ { get; set; }
        public string CPF { get; set; }
        public string chNFe { get; set; }
        public string dhEvento { get; set; }
        public string tpEvento { get; set; }
        public string nSeqEvento { get; set; }
        public string verEvento { get; set; }
        public string detEvento { get; set; }
        public string versao { get; set; }
        public string descEvento { get; set; }
        public string xCorrecao { get; set; }
        public string xCondUso { get; set; }

        public dadosCartaCorrecao(
            string _idLote,
            string _Id,
            string _cOrgao,
            string _tpAmb,
            string _CNPJ,
            string _CPF,
            string _chNFe,
            string _dhEvento,
            string _tpEvento,
            string _nSeqEvento,
            string _verEvento,
            string _detEvento,
            string _versao,
            string _descEvento,
            string _xCorrecao,
            string _xCondUso
            )
        {
            idLote = _idLote;
            Id = _Id;
            cOrgao = _cOrgao;
            tpAmb = _tpAmb;
            CNPJ = _CNPJ;
            CPF = _CPF;
            chNFe = _chNFe;
            dhEvento = _dhEvento;
            tpEvento = _tpEvento;
            nSeqEvento = _nSeqEvento;
            verEvento = _verEvento;
            detEvento = _detEvento;
            versao = _versao;
            descEvento = _descEvento;
            xCorrecao = _xCorrecao;
            xCondUso = _xCondUso;

        }
    }

    #endregion

    #region Classe de Regras de negócio


    public class regrasNegocioNfe
    {
        /// <summary>
        /// Gera a chave de acesso da NFe.
        /// </summary>
        /// <param name="dadosChave">Conjunto de dados para a criação da chave de acesso.</param>
        /// <returns>Chave de acesso</returns>
        public string geraChaveAcesso(dadosChaveAcessoNfe dadosChave)
        {
            if (dadosChave.codUf.Trim().Length == 2)
            {
                if (dadosChave.dataEmissao.Trim().Length == 4)
                {
                    if (dadosChave.cnpjEmitente.Trim().Length == 14)
                    {
                        if (dadosChave.modeloNF.Trim().Length == 2)
                        {
                            if (dadosChave.serieNF.Trim().Length == 3)
                            {
                                if (dadosChave.numeroNfe.Length == 9)
                                {
                                    if (dadosChave.formaEmissao.Length == 1)
                                    {
                                        if (dadosChave.codigoNumero.Trim().Length == 8)
                                        {
                                            string codigoFinal = "";
                                            int somaTotal = 0; //nome do item no documento: Ponderação
                                            int multiplicador = 2; // nome do item no documento: Peso
                                            int resto;

                                            codigoFinal = dadosChave.codUf;
                                            codigoFinal += dadosChave.dataEmissao;
                                            codigoFinal += dadosChave.cnpjEmitente;
                                            codigoFinal += dadosChave.modeloNF;
                                            codigoFinal += dadosChave.serieNF;
                                            codigoFinal += dadosChave.numeroNfe;
                                            codigoFinal += dadosChave.formaEmissao;
                                            codigoFinal += dadosChave.codigoNumero;

                                            for (int i = 42; i >= 0; i--)
                                            {
                                                if (multiplicador == 10)
                                                    multiplicador = 2;
                                                somaTotal += Convert.ToInt32(codigoFinal[i].ToString()) * multiplicador;
                                                multiplicador++;
                                            }

                                            resto = somaTotal % 11;

                                            if (resto == 0 || resto == 1)
                                            {
                                                codigoFinal += "0";
                                            }
                                            else
                                            {
                                                codigoFinal += (11 - resto).ToString();
                                            }

                                            return codigoFinal;
                                        }
                                        else
                                        {
                                            mensagemAlerta("Erro ao gerar o código de acesso da NF-e. Código da NF-e Inválida.");
                                            return "";
                                        }
                                    }
                                    else
                                    {
                                        mensagemAlerta("Erro ao gerar o código de acesso da NF-e. Forma de emissão da NF-e Inválida.");
                                        return "";
                                    }
                                }
                                else
                                {
                                    mensagemAlerta("Erro ao gerar o código de acesso da NF-e. Número da NF-e Inválida.");
                                    return "";
                                }
                            }
                            else
                            {
                                mensagemAlerta("Erro ao gerar o código de acesso da NF-e. Série da NF-e Inválida.");
                                return "";
                            }
                        }
                        else
                        {
                            mensagemAlerta("Erro ao gerar o código de acesso da NF-e. Modelo da NF-e Inválida.");
                            return "";
                        }
                    }
                    else
                    {
                        mensagemAlerta("Erro ao gerar o código de acesso da NF-e. CNPJ inválido.");
                        return "";
                    }
                }
                else
                {
                    mensagemAlerta("Erro ao gerar o código de acesso da NF-e. Data inválida.");
                    return "";
                }
            }
            else
            {
                mensagemAlerta("Erro ao gerar o código de acesso da NF-e. Código de estado inválido.");
                return "";
            }
        }

        /// <summary>
        /// Realiza a validação dos valores informados. A validação pode ser realizada em diferentes tipos de campo, definida pelo tipo imformado.
        /// </summary>
        /// <param name="texto">Valor da string que será validada</param>
        /// <param name="tpCampo">Tipo de campo. 1 - Decimal (Valor) | 2 - Alfanumérico | 3 -  Data | 4 - Data/Hora | 5 - Hora</param>
        /// <param name="qtd">Quantidade de caracteres limite. Se informado "0" (zero) ou null o valor será 60, conforme manual do contribuinte versão 5, e NT 2013.005.</param>
        /// <param name="autocompletar">Informar se o método deve auto completar a quantidade máxima de caracteres.</param>
        /// <param name="caracterAutocompletar">Caractere que irá auto completar a string. Se informado "" (vazio) ou null o valor utilizado pelo sistema será "0" (zero).</param>
        /// <returns>Retorna o valor validado, ou "" (vazio) cajo não seja possível validar os dados</returns>
        public string validacaoPadraoCampos(string texto, Int16 tpCampo, int? qtdMin, int? qtdMax, bool autocompletar, string caracterAutocompletar, string codigoCampo)
        {
            if (texto != null)
            {
                texto = texto.Trim();
                if (texto.Length > 0)
                {
                    //verifica se a opção selecionada está entre 0 e 5 (valor referente ao código dos tipos de campos)
                    if (tpCampo > 0 && tpCampo < 6)
                    {
                        //realiza validação de campos NÚMERICOS
                        if (tpCampo == 1)
                        {
                            texto = texto.Replace(",", ".");
                            return texto;
                        }
                        else
                        {
                            //realiza validação de campos ALFANUMÉRICOS
                            if (tpCampo == 2)
                            {
                                if (qtdMin == null || qtdMin < 0)
                                    qtdMin = 1;
                                if (texto.Length >= qtdMin || autocompletar == true)
                                {
                                    int qtdCaracteresInicial = texto.Length;
                                    int qtdCaracteresFinal;
                                    //remove os caracteres especiais do texto (acentuação)
                                    texto = removeCaracteresEspeciais(texto);
                                    //troca os seguintes caracteres: <,>,&,",' pela sequencia escape
                                    texto = converteCaracteresEspeciais(texto);

                                    qtdCaracteresFinal = texto.Length;

                                    if (qtdMax == 0 || qtdMax == null)
                                    {
                                        qtdMax = 60;
                                    }

                                    //caso seja realizada troca de caracteres pela sequencia escape, toda a sequência conta como 1 caracter
                                    if (qtdCaracteresInicial < qtdCaracteresFinal)
                                        qtdMax += qtdCaracteresFinal - qtdCaracteresInicial;

                                    //caso qtdMax não informada para campos alfanuméricos o valor default é :60 caracteres
                                    if (autocompletar == true)
                                    {
                                        if (caracterAutocompletar == "" || caracterAutocompletar == null)
                                        {
                                            caracterAutocompletar = "0";
                                        }
                                        while (texto.Length < qtdMax)
                                        {
                                            texto = caracterAutocompletar + texto;
                                        }
                                    }
                                    return texto;
                                }
                                else
                                {
                                    mensagemAlerta("Quantidade mínima de caracteres não alcançado, mínimo de " + qtdMin + " caracteres. Campo: " + codigoCampo);
                                }
                            }
                            else
                            {
                                // realiza validação para campos DATA.
                                if (tpCampo == 3)
                                {
                                    try
                                    {
                                        DateTime data = DateTime.ParseExact(texto.Replace("/", "-"), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                                        texto = data.ToString("yyyy-MM-dd");
                                        return texto;
                                    }
                                    catch (Exception e)
                                    {
                                        mensagemAlerta("Data no formato incorreto! Erro: " + e.Message + " Campo: " + codigoCampo);
                                        return "";
                                    }
                                }
                                else
                                {
                                    // realiza validação para campos DATA/HORA.
                                    if (tpCampo == 4)
                                    {
                                        try
                                        {
                                            DateTime data = DateTime.ParseExact(texto.Replace("/", "-"), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                                            texto = data.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'-'03:00''");
                                            return texto;
                                        }
                                        catch (Exception e)
                                        {
                                            mensagemAlerta("Data/hora no formato incorreto! Erro: " + e.Message + " Campo: " + codigoCampo);
                                            return "";
                                        }
                                    }
                                    else
                                    {
                                        // realiza validação para campos HORA.
                                        if (tpCampo == 5)
                                        {
                                            try
                                            {
                                                DateTime data = DateTime.ParseExact(texto, "HH:mm:ss", CultureInfo.InvariantCulture);
                                                texto = data.ToString("HH:mm:ss"); ;
                                                return texto;
                                            }
                                            catch (Exception e)
                                            {
                                                mensagemAlerta("Hora no formato incorreto! Erro: " + e.Message + " Campo: " + codigoCampo);
                                                return "";
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        mensagemAlerta("Tipo de campo inválido! Selecione 1 (numérico), 2 (alfanumérico), ou 3 (data). Campo: " + codigoCampo);
                    }
                }
                else
                {
                    string mensagem = "Quantidade de caracteres inválido, tamanho ideal: " + qtdMax + " caracteres. Atual: " + texto.Trim().Length + " Campo: " + codigoCampo;
                    mensagemAlerta(mensagem);
                }
            }
            return "";
        }

        /// <summary>
        /// Realiza a troca de caracteres especiais com acentuação por caracteres sem acentuação.
        /// </summary>
        /// <param name="texto">Texto que será realizada a operação.</param>
        /// <returns></returns>
        public string removeCaracteresEspeciais(string texto)
        {
            if (!string.IsNullOrEmpty(texto))
            {
                string nomeEditado = texto.Normalize(NormalizationForm.FormD);
                StringBuilder sb = new StringBuilder();
                for (int k = 0; k < nomeEditado.Length; k++)
                {
                    UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(nomeEditado[k]);
                    if (uc != UnicodeCategory.NonSpacingMark)
                    {
                        sb.Append(nomeEditado[k]);
                    }
                }
                nomeEditado = sb.ToString();
                return nomeEditado;
            }
            return "";
        }

        /// <summary>
        /// Realiza a troca os seguintes caracteres: <,>,&,",' pela sequencia escape.
        /// </summary>
        /// <param name="texto">Texto que será analizado.</param>
        /// <returns></returns>
        public string converteCaracteresEspeciais(string texto)
        {
            texto = texto.Replace("<", "&lt;");
            texto = texto.Replace(">", "&gt;");
            texto = texto.Replace("&", "&amp;");
            texto = texto.Replace("\"", "&quot;");
            texto = texto.Replace("\'", "&#39;");

            return texto;
        }

        /// <summary>
        /// Gera código numérico de 8 dígitos.
        /// </summary>
        /// <returns>Código numérico de 8 dígitos.</returns>
        public string geraCodigoNumerico()
        {
            Random geraNumero = new Random();
            return geraNumero.Next(10000000, 99999999).ToString();
        }

        /// <summary>
        /// Verifica se o indicador é 0 - Pagamento a vista, 1 - pagamento a prazo ou 2 - outros.
        /// </summary>
        /// <param name="texto">Valor para verificação.</param>
        /// <returns>True ou false.</returns>
        public bool verificaIndPag(string texto)
        {
            if (texto != null)
            {
                if (texto.Equals("0") || texto.Equals("1") || texto.Equals("2"))
                    return true;
                else
                    mensagemAlerta("Indicador de pagamento inválido");
            }
            return false;

        }

        /// <summary>
        /// Verifica se o modelo informado é válido.
        /// </summary>
        /// <param name="texto">Valor para verificação.</param>
        /// <returns>True ou false.</returns>
        public bool verificaModeloNfe(string texto)
        {
            if (texto.Equals("55") || texto.Equals("65"))
                return true;
            else
                mensagemAlerta("Modelo da NFe inválido");
            return false;
        }

        /// <summary>
        /// Verifica se a série da nota é válida. Válido de 0 a 889 | 900 a 999.
        /// </summary>
        /// <param name="texto">Valor para verificação.</param>
        /// <returns>True ou false</returns>
        public bool verificaSerie(string texto)
        {
            if ((Convert.ToInt32(texto) >= 0 && Convert.ToInt32(texto) <= 889) || (Convert.ToInt32(texto) >= 900 && Convert.ToInt32(texto) <= 999))
                return true;
            else
                mensagemAlerta("Série da NF inválida. Válido de 0 a 889 | 900 a 999.");
            return false;
        }

        /// <summary>
        /// Verifica se o tipo de operação é válido. 0 - Entrada | 1 - Saída
        /// </summary>
        /// <param name="texto">Valor para verificação</param>
        /// <returns>True or false</returns>
        public bool verificaTipoOperacao(string texto)
        {
            if (texto.Equals("0") || texto.Equals("1"))
                return true;
            else
                mensagemAlerta("Tipo de operação inválido. Permitido 0 - entrada | 1 - saída.");
            return false;
        }

        /// <summary>
        /// Verifica se o identificador de destino é válido. 1 - Operação interna | 2 - Operação interestadual | 3 - Operação com exterior
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public bool verificaIdentificadorDestino(string texto)
        {
            if (texto.Equals("1") || texto.Equals("2") || texto.Equals("3"))
                return true;
            else
                mensagemAlerta("Identificador de destino inválido. Permitido 1 - Operação interna | 2 - Operação interestadual | 3 - Operação com exterior");

            return false;
        }

        /// <summary>
        /// Verifica se a cidade informada pertence ao estao emitente da NFe.
        /// </summary>
        /// <param name="codigoMunicipio">Código do município de origem da NFe.</param>
        /// <param name="codigoEstado">Código do estado de origem da NFe.</param>
        /// <returns>True ou false</returns>
        public bool verificaMunicipioEstado(string codigoMunicipio, string codigoEstado)
        {
            if ((codigoMunicipio.Length == 2 || codigoMunicipio.Length == 7) && codigoEstado.Length == 2)
            {
                if (codigoMunicipio.Length == 7)
                    codigoMunicipio = codigoMunicipio.Substring(0, 2);

                if (codigoMunicipio.Equals(codigoEstado))
                    return true;
                else
                    return false;
            }
            else
            {
                mensagemAlerta("código do município ou estado está incorreto.");
            }
            return false;
        }

        /// <summary>
        /// Verifica se a operação está sendo realizada no exterior.
        /// </summary>
        /// <param name="texto">Valor para verificação.</param>
        /// <returns>True ou false</returns>
        public bool verificaOperacaoExterior(string texto)
        {
            if (texto.Equals("3"))
                return true;
            return false;
        }

        /// <summary>
        /// Retorna o código de município para operações no exterior.
        /// </summary>
        /// <returns></returns>
        public string preencheCodigoMunicipioExterior()
        {
            return "9999999";
        }

        /// <summary>
        /// Verifica se o tipo de impressão do DANFE é válido. Permitido 0 - Sem geração de DANFE | 1 - DANFE normal, Retrato | 2 - DANFE normal, Paisagem | 3 - DANFE Simplificado | 4 - DANFE NFC-e | 5 - DANFE NFC-e em mensagem eletrônica
        /// </summary>
        /// <param name="texto">Valor para validação.</param>
        /// <returns>True ou false</returns>
        public bool verificaTipoImpressao(string texto)
        {
            try
            {
                int valor = Convert.ToInt32(texto);

                if (valor >= 0 && valor <= 5)
                    return true;
                else
                    mensagemAlerta("Valor informado inválido. Informar valor de 0 a 5.");
            }
            catch (Exception erro)
            {
                mensagemAlerta("Valor informado inválido. Informar valor de 0 a 5. Erro: " + erro.Message);
            }
            return false;
        }

        /// <summary>
        /// Verifica se o tipo de emissão é válido. Permitido 1 - Emissão normal | 2 - Contingência FS-IA | 3 - Contingência SCAN | 4 - Contingência DPEC | 5 - Contingência FS-DA | 6 - Contingência SVC-AN | 7 - Contingência SVC-RS | 9 - Contingência off-line da NFC
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public bool verificaTipoEmissao(string texto)
        {
            if (texto.Equals("1") || texto.Equals("2") || texto.Equals("3") || texto.Equals("4") || texto.Equals("5") || texto.Equals("6") || texto.Equals("7") || texto.Equals("9"))
                return true;
            else
                mensagemAlerta("Valor informado inválido.");
            return false;
        }

        /// <summary>
        /// Retorna o valor do dígito verificador da chave de acesso
        /// </summary>
        /// <param name="chaveAcesso">Valor da chave de acesso</param>
        /// <returns>Dígito verificador</returns>
        public string preencheDigitoVerificadorChaveAcesso(string chaveAcesso)
        {
            if (chaveAcesso.Length == 44)
                return chaveAcesso[43].ToString();
            else
                mensagemAlerta("Chave de acesso inválida.");
            return "";
        }

        /// <summary>
        /// Verifica se o tipo de ambiente é válido. Permitido 1 - Produção | 2 - Homologação
        /// </summary>
        /// <param name="texto">Valor a ser verificado</param>
        /// <returns>True ou false</returns>
        public bool tipoAmbiente(string texto)
        {
            if (texto.Equals("1") || texto.Equals("2"))
                return true;
            else
                mensagemAlerta("Tipo de ambiente inválido.");
            return false;
        }

        /// <summary>
        /// Verifica se a finalidade de emissão informada é válida. Permitido 1 - NF-e normal | 2 - NF-e complementar | 3 - NF-e de ajuste | 4 - Devolução/Retorno
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public bool verifcaFinalidadeEmissao(string texto)
        {
            int valor = Convert.ToInt32(texto);
            if (valor >= 1 && valor <= 4)
                return true;
            else
                mensagemAlerta("Finalidade informada inválida. Informar valor entre 1 e 4");
            return false;
        }

        /// <summary>
        /// Verifica se a finalidde da emissão é complementar.
        /// </summary>
        /// <param name="texto">Valor para verificação.</param>
        /// <returns>True ou false</returns>
        public bool verificaFinalidadeEmissaoComplementar(string texto)
        {
            if (texto.Equals("2"))
                return true;
            return false;
        }

        /// <summary>
        /// Verifica se o CNPJ da NF complementar é igual ao CNPJ da NF principal.
        /// </summary>
        /// <param name="texto">Valor do CNPJ 1</param>
        /// <param name="texto2">Valor do CNPJ 2</param>
        /// <returns>True ou false</returns>
        public bool verificaCnpjEmissaoComplementar(string texto, string texto2)
        {
            if (texto.Equals(texto2))
                return true;
            else
                mensagemAlerta("CNPJ do emissor da nota complementar diferente da nota primária.");
            return false;
        }

        /// <summary>
        /// Verifica se o valor de processo de emissão é valido. Permitido 0 - Emissão de NF-e com aplicativo do contribuinte | 1 - Emissão de NF-e avulsa pelo Fisco | 2 - Emissão de NF-e avulsa, pelo contribuinte com seucertificado digital, através do site do Fisco | 3 - Emissão NF-e pelo contribuinte com aplicativo fornecido pelo Fisco.
        /// </summary>
        /// <param name="texto">Valor para verificação</param>
        /// <returns>True ou false</returns>
        public bool verificaProcessoEmissao(string texto)
        {
            try
            {
                int valor;
                valor = Convert.ToInt32(texto);
                if (valor >= 0 && valor <= 3)
                    return true;
                else
                {
                    mensagemAlerta("Valor de processo de emissão inválido. Valores permitidos: 0 - Emissão de NF-e com aplicativo do contribuinte | 1 - Emissão de NF-e avulsa pelo Fisco | 2 - Emissão de NF-e avulsa, pelo contribuinte com seucertificado digital, através do site do Fisco | 3 - Emissão NF-e pelo contribuinte com aplicativo fornecido pelo Fisco.");
                }
            }
            catch (Exception erro)
            {
                mensagemAlerta("Valor de processo de emissão inválido. Valores permitidos: entre 0 e 3. Erro: " + erro);
            }
            return false;
        }

        /// <summary>
        /// Valida o formato de data hora baseado na string de seguinte formato: dd-MM-yyyy HH:mm:ss
        /// </summary>
        /// <param name="texto">Valor para formatação</param>
        /// <returns>Valor formatado</returns>
        public string validaAnoMes(string texto)
        {
            string anoMes;
            anoMes = texto.Substring(9, 2);
            anoMes += texto.Substring(4, 2);
            return anoMes;
        }

        /// <summary>
        /// Valida o valor a ser inesrido no cmapo de modelo da NF. Caso não seja informado insere "0".
        /// </summary>
        /// <param name="texto">Valor para verificação.</param>
        /// <returns>Valor informado ou "0"</returns>
        public string validaSerie(string texto)
        {
            int tamanho = texto.Length;
            if (tamanho >= 1 && tamanho <= 3)
            {
                if (string.IsNullOrEmpty(texto))
                    return "0";
            }
            else
            {
                mensagemAlerta("Valor inválido. Favor informar valor de 1 a 3.");
            }
            return texto;
        }

        /// <summary>
        /// Verifica se os dados do sistema foram enviados para preencher o modelo 01(comércio) ou 04(rural).
        /// </summary>
        /// <param name="texto1">Valor para verificação.</param>
        /// <param name="texto2">Valor para verificação.</param>
        /// <returns>True ou false</returns>
        public bool verificaTipoModelo(string texto1, string texto2)
        {
            if (!string.IsNullOrEmpty(texto1) && string.IsNullOrEmpty(texto2))
                return true;
            return false;
        }

        /// <summary>
        /// Verifica se o CPF foi preenchido e auto completa os campos de CNPJ com zeros.
        /// </summary>
        /// <param name="CNPJ">Valor do CNPJ</param>
        /// <param name="CPF">Valor do CPF</param>
        /// <returns>Retorna o valor do CPF com zeros</returns>
        public string verificaTipoPessoaCPF(string CNPJ, string CPF, string tipoNota)
        {
            if (tipoNota.Equals("1") || string.IsNullOrEmpty(tipoNota))
            {
                if (CNPJ.Trim().Length == 14 && (CPF.Trim().Length == 0 || CPF == null))
                    return "00000000000";
                else
                    mensagemAlerta("Valores de CNPJ e/ou CPF inválidos.");
                return CPF;
            }
            else
            {
                mensagemAlerta("CPF só pode ser informado para o NFe Avulsa.");
                return "";
            }
        }

        /// <summary>
        /// Verifica se o CNPJ foi preenchido e auto completa os campos de CPF com zeros.
        /// </summary>
        /// <param name="CNPJ">Valor do CPF</param>
        /// <param name="CPF"></param>
        /// <returns></returns>
        public string verificaTipoPessoaCNPJ(string CNPJ, string CPF)
        {
            if (CPF.Trim().Length == 11 && (string.IsNullOrEmpty(CNPJ)))
                return "00000000000000";
            else
                mensagemAlerta("Valores de CNPJ e/ou CPF inválidos.");
            return CNPJ;
        }

        /// <summary>
        /// Verifica se o modelo na seção de produtor rural esta enquadrada no modelo correto. Permitido 4=NF de Produtor; 01=NF (v2.0).
        /// </summary>
        /// <param name="texto">Valor para verificação.</param>
        /// <returns>True ou false</returns>
        public bool verificaModeloProdutoRural(string texto)
        {
            if (texto.Equals("01") || texto.Equals("04"))
                return true;
            else
                mensagemAlerta("Modelo de nota inválido no momento. Permitidos 01 e 04.");
            return false;
        }

        /// <summary>
        /// Verifica se o modelo do documento fiscal referenciado é válido. Permitido 2B - Cupom Fiscal emitido por máquina registradora (não ECF) | 2C - Cupom Fiscal PDV |  2D - Cupom Fiscal (emitido por ECF)
        /// </summary>
        /// <param name="texto">Valor que será verificado.</param>
        /// <returns>True ou false</returns>
        public bool verificaModeloDocumentoFiscalReferenciado(string texto)
        {
            if (texto.Equals("2B") || texto.Equals("2C") || texto.Equals("2D"))
                return true;
            else
                mensagemAlerta("Modelo do documento fiscal referenciado inválido. Permitido 2B, 2C ou 2D");
            return false;
        }

        /// <summary>
        /// Mostra mensagem na tela em uma janela de alerta.
        /// </summary>
        /// <param name="mensagem">Mensagem que será exibida.</param>
        public void mensagemAlerta(string mensagem)
        {
            MessageBox.Show(mensagem.Trim());
        }

        /// <summary>
        /// Valida o CEP, caso o mesmo sejá inválido retornará o valor "00000000".
        /// </summary>
        /// <param name="texto">Valor para validação.</param>
        /// <returns>Valor do CEP</returns>
        public string validaCep(string texto)
        {
            texto = texto.Trim().Replace(".", "").Replace("-", "");
            if (texto.Length == 8)
                if (texto.Equals("99999999"))
                    return "00000000";
                else
                    return texto;
            else
                mensagemAlerta("00000000");
            return "";

        }

        /// <summary>
        /// Verifica se os campos para NFe Conjugada seja emitida.
        /// </summary>
        /// <param name="IM">Valor da inscrição Municipal</param>
        /// <param name="CRT"> Valor do CRT</param>
        /// <returns>True ou false</returns>
        public bool verificaEnderecoEmitenteNFeConjulgada(string IM, string CRT)
        {
            if (string.IsNullOrEmpty(IM.Trim()) && string.IsNullOrEmpty(CRT.Trim()))
                return true;
            return false;
        }

        /// <summary>
        /// Verifica se a Inscrição Municipal foi preenchida.
        /// </summary>
        /// <param name="IM">Isncrição Municipal</param>
        /// <returns>True ou false</returns>
        public bool verificaIscricaoMunicipalInformada(string IM)
        {
            if (string.IsNullOrEmpty(IM.Trim()))
                return true;
            return false;
        }

        /// <summary>
        /// Verifica se o Cnae está preenchido e possui 7 dígitos
        /// </summary>
        /// <param name="texto">Valor para verificação</param>
        /// <returns>True ou false</returns>
        public bool verificaCnae(string texto)
        {
            if (texto != null)
            {
                if (texto.Length == 7)
                {
                    return true;
                }
                else
                {
                    if (texto.Length >= 0 && texto.Length < 7)
                    {
                        mensagemAlerta("Cnae inválido.");
                        return false;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Verifica se o valor do Regime Tributário é válido.
        /// </summary>
        /// <param name="texto">Valor para verificação</param>
        /// <returns>True ou false</returns>
        public bool verificaRegimeTributario(string texto)
        {
            if (texto != null)
            {
                if (texto.Equals("1") || texto.Equals("2") || texto.Equals("3"))
                    return true;
                else
                    mensagemAlerta("Valor de Regime Tributário inválido. Permitido valores de 1 a 3.");
            }
            return false;
        }

        /// <summary>
        /// Verifica se o CPF foi preenchido e auto completa os campos de CNPJ com zeros no destinatário.
        /// </summary>
        /// <param name="CNPJ">Valor do CNPJ</param>
        /// <param name="CPF">Valor do CPF</param>
        /// <returns>Retorna o valor do CPF com zeros</returns>
        public string verificaTipoPessoaCPFDestinatario(string CNPJ, string CPF, string codigoPais)
        {
            if (codigoPais.Equals("1058"))
            {
                if (CNPJ.Trim().Length == 14 && (CPF.Trim().Length == 0 || CPF == null))
                    return "";
                else
                    mensagemAlerta("Valores de CNPJ e/ou CPF inválidos.");
                return CPF;
            }
            return "";
        }

        /// <summary>
        /// Verifica se o CNPJ foi preenchido e auto completa os campos de CPF com zeros no destinatário.
        /// </summary>
        /// <param name="CNPJ">Valor do CPF</param>
        /// <param name="CPF">Valor do CPF</param>
        /// <returns>Retorna o valor do CNPJ</returns>
        public string verificaTipoPessoaCNPJDestinatario(string CNPJ, string CPF, string codigoPais)
        {
            if (codigoPais.Equals("1058"))
            {
                if (CPF.Trim().Length == 11 && (CNPJ.Trim().Length == 0 || CNPJ == null))
                    return "";
                else
                    mensagemAlerta("Valores de CNPJ e/ou CPF inválidos.");
                return CNPJ;
            }
            return "";
        }

        /// <summary>
        /// Verifica se o valor do campo está vazio.
        /// </summary>
        /// <param name="texto">Valor que será verificado.</param>
        /// <returns>True ou false</returns>
        public bool verificaCampoVazio(string texto)
        {
            if (!string.IsNullOrEmpty(texto))
                return true;
            return false;
        }

        /// <summary>
        /// Verifica se a NFe está sendo transmitida para um estrangeiro e se seu documento foi informado
        /// </summary>
        /// <param name="Id">Id do estrangeiro (documento)</param>
        /// <param name="codigoPais">Código do pais do destinatário</param>
        /// <returns>True ou false</returns>
        public bool verificaIdEstrangeiro(string Id, string codigoPais)
        {
            if (Id != null && codigoPais != null)
            {
                if (!codigoPais.Equals("1058"))
                {
                    if (Id.Trim().Length > 0)
                        return true;
                    else
                        mensagemAlerta("Favor informar um documento válido para o estrangeiro.");
                }
            }
            return false;
        }

        /// <summary>
        /// Verifica se o nome foi preenchido para as notas de modelo 55.
        /// </summary>
        /// <param name="nome">Nome do destinatário.</param>
        /// <param name="tipoNota">Modelo da NF.</param>
        /// <returns>True ou false</returns>
        public bool verificaNomeDestinatario(string nome, string tipoNota)
        {
            if (nome != null && tipoNota != null)
            {
                if (tipoNota.Equals("55"))
                {
                    if (nome.Trim().Length > 2 && nome.Trim().Length <= 60)
                        return true;
                    else
                        mensagemAlerta("Nome de destinatário obrigatório para modelo 55 da NFe.");
                }
            }
            return false;
        }

        /// <summary>
        /// Verifica se o modelo da NFe emitida é o 55.
        /// </summary>
        /// <param name="texto">Valor para verificação</param>
        /// <returns>True ou false</returns>
        public bool verificaTipoModelo55NFe(string texto)
        {
            if (texto.Equals("55"))
                return true;
            return false;
        }

        /// <summary>
        /// Valida o código da cidade em operações com o exterior.
        /// </summary>
        /// <param name="codigoPais">Codigo do país.</param>
        /// <param name="codigoCidade">Código da cidade.</param>
        /// <returns>Valor do código da cidade</returns>
        public string validaCodigoCidadeExterior(string codigoPais, string codigoCidade)
        {
            if (!codigoPais.Equals("1058"))
                return "9999999";
            else
                if (codigoCidade.Length == 7)
                    return codigoCidade;
                else
                    mensagemAlerta("Código de cidade inválido");
            return "";

        }

        /// <summary>
        /// Valida o nome da cidade em operações com o exterior.
        /// </summary>
        /// <param name="codigoPais">Código do país.</param>
        /// <param name="nomeCidade">Nome da cidade.</param>
        /// <returns>Valor do nome da cidade</returns>
        public string validaNomeCidadeExterior(string codigoPais, string nomeCidade)
        {
            if (!codigoPais.Equals("1058"))
                return "EXTERIOR";
            else
                if (nomeCidade.Trim().Length >= 2 && nomeCidade.Trim().Length <= 60)
                    return nomeCidade.Trim();
                else
                    mensagemAlerta("Nome de cidade de destinatário inválida.");
            return nomeCidade;
        }

        /// <summary>
        /// Valida o UF em operações com o exterior.
        /// </summary>
        /// <param name="codigoPais">Código do país.</param>
        /// <param name="nomeUF">UF</param>
        /// <returns></returns>
        public string validaUfExterior(string codigoPais, string nomeUF)
        {
            if (!codigoPais.Equals("1058"))
                return "EX";
            else
                if (nomeUF.Length == 2)
                    return nomeUF;
                else
                    mensagemAlerta("UF de destinatário informado inválido.");
            return "";
        }

        /// <summary>
        /// Verifica se o indicador de IE de destinatário é válido. Permitido 1 - Contribuinte ICMS (informar a IE do destinatário) | 2 - Contribuinte isento de Inscrição no cadastro de Contribuintes do ICMS | 9 - Não Contribuinte, que pode ou não possuir Inscrição Estadual no Cadastro de Contribuintes do ICMS.
        /// </summary>
        /// <param name="texto">Valor para verificação.</param>
        /// <returns>True ou false</returns>
        public bool verificaIndicadorIeDestinatario(string texto)
        {
            texto = texto.Trim();
            if (texto.Equals("1") || texto.Equals("2") || texto.Equals("9"))
                return true;
            else
                mensagemAlerta("Valor de indicador de IE de destinatário inválido");
            return false;
        }

        /// <summary>
        /// Valida o indicador de destinatário para operações no exterior.
        /// </summary>
        /// <param name="indicadorIE">Indicador de destinatário</param>
        /// <param name="codigoPais">Código do país</param>
        /// <returns>Caso seja exterior retorna 9, se não retorna valor atual</returns>
        public string validaIndicadorIeDestinatarioExterior(string indicadorIE, string codigoPais)
        {
            if (!codigoPais.Equals("1058"))
                return "9";
            return indicadorIE;
        }

        /// <summary>
        /// Verifica o tipo de indicador de destinatário.
        /// </summary>
        /// <param name="texto">Valor para verificação.</param>
        /// <returns>True ou false</returns>
        public bool verificaTipoIndicadorDestinatoarioIE(string texto)
        {
            if (!texto.Equals("2"))
                return true;
            return false;
        }

        /// <summary>
        /// Verifica se foi informado um código para destinatário na NF modelo 65 (NFCe)
        /// </summary>
        /// <param name="CNPJ">CNPJ</param>
        /// <param name="CPF">CPF</param>
        /// <param name="idEstrnageiro">Id estrangeiro</param>
        /// <returns></returns>
        public bool verrificaPreenchimentoIdentificacaoDestinatarioNFe(string CNPJ, string CPF, string idEstrangeiro)
        {
            if (string.IsNullOrEmpty(CNPJ) && string.IsNullOrEmpty(CPF) && string.IsNullOrEmpty(idEstrangeiro))
                return true;
            return false;
        }

        /// <summary>
        /// Verifica se o endereço de retirada é o mesmo do remetente
        /// </summary>
        /// <param name="ruaRemetente">Rua do remetente</param>
        /// <param name="numeroRemetente">Numero do estabelicimento do remetente</param>
        /// <param name="bairroRemetente">Bairro do remetente</param>
        /// <param name="cidadeRemetente">Cidade do remetente</param>
        /// <param name="ruaRetirada">Rua para retirada</param>
        /// <param name="numeroRetirada">Numero do estabelecimento da retirada</param>
        /// <param name="bairroRetirada">Bairro da retirada</param>
        /// <param name="cidadeRetirada">Cidade da retirada</param>
        /// <returns>True ou false</returns>
        public bool verificaEnderecoRetiradaIgualRemetente(string ruaRemetente, string numeroRemetente, string bairroRemetente, string cidadeRemetente, string ruaRetirada, string numeroRetirada, string bairroRetirada, string cidadeRetirada)
        {
            if (ruaRemetente.Equals(ruaRetirada) && numeroRemetente.Equals(numeroRetirada) && bairroRemetente.Equals(bairroRetirada) && cidadeRemetente.Equals(cidadeRetirada))
                return true;
            return false;
        }

        /// <summary>
        /// Verifica se o grupo de Identificação da NFe será preenchido.
        /// </summary>
        /// <param name="dataContigencia">Data de contingência.</param>
        /// <param name="justificativa">Jusiticativa</param>
        /// <returns>True ou false</returns>
        public bool verificaGrupoIdentificacaoNFe(string dataContigencia, string justificativa)
        {
            if (string.IsNullOrEmpty(dataContigencia) && string.IsNullOrEmpty(justificativa))
                return true;
            return false;
        }

        /// <summary>
        /// Verifica se o número do produto é válido. Permitido valores de 1 a 990.
        /// </summary>
        /// <param name="texto">Valor para verificação.</param>
        /// <returns>True ou false</returns>
        public bool verificaNumeroItem(string texto)
        {
            try
            {
                Int16 valor = Convert.ToInt16(texto);
                if (valor >= 1 && valor <= 990)
                    return true;
                else
                    mensagemAlerta("Valor inválido. Somente valores de 1 a 990.");
            }
            catch (Exception erro)
            {
                mensagemAlerta("Valor infomado inválido. Erro:" + erro.Message);
            }
            return false;
        }

        /// <summary>
        /// Verifica se a quantidade de caracteres valor do código EAN é válido. Permitido: 0, 8, 12, 13 e 14.
        /// </summary>
        /// <param name="texto">Valor para verificação.</param>
        /// <returns>True ou false</returns>
        public bool verificaCodigoEan(string texto)
        {
            return verificaQtdCaracteresCodigoEan(texto);
        }

        /// <summary>
        /// Verifica se a quantidade de caracteres valor do código EANTRIB foi informado, caso sim ele é válidado. Permitido: 0, 8, 12, 13 e 14.
        /// </summary>
        /// <param name="texto">Valor para verificação.</param>
        /// <returns>True ou false</returns>
        public bool verificaCodigoEanTrib(string texto)
        {
            if (string.IsNullOrEmpty(texto))
                return true;

            return verificaQtdCaracteresCodigoEan(texto);
        }

        /// <summary>
        /// Verifica se a quantidade de caracteres informado é válida. Permitido: 0, 8, 12, 13 e 14.
        /// </summary>
        /// <param name="texto"> Valor para verificação.</param>
        /// <returns>True ou false</returns>
        public bool verificaQtdCaracteresCodigoEan(string texto)
        {
            int qtd = texto.Length;
            if (qtd == 0 || qtd == 8 || qtd == 12 || qtd == 13 || qtd == 14)
                return true;
            return false;
        }

        /// <summary>
        /// Verifica se o código NCM é válido. Permitido 2 ou 8 caracteres.
        /// </summary>
        /// <param name="texto">Valor para verificação.</param>
        /// <returns>True ou false</returns>
        public bool verificaNCM(string texto)
        {
            if (!string.IsNullOrEmpty(texto))
            {
                int qtd = texto.Length;
                if (qtd == 2 || qtd == 8)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Verifica se o valor do indicador de soma no total é válido.Permitido 0 - Valor do item (vProd) não compõe o valor total da NF-e | 1 - Valor do item (vProd) compõe o valor total da NF-e (vProd) (v2.0) 
        /// </summary>
        /// <param name="texto">Valor para verificação</param>
        /// <returns>True ou false</returns>
        public bool verificaIndicadorTotal(string texto)
        {
            if (texto.Equals("0") || texto.Equals("1"))
                return true;
            else
                mensagemAlerta("Valor inválido para o indicador de soma no total. Permitido 0 ou 1.");
            return false;
        }

        /// <summary>
        /// Verifica se o valor para o Tipo de Transporte Internacional é válido. Permitido 1 - Marítima | 2 - Fluvial | 3 - Lacustre | 4 - Aérea | 5 - Postal | 6 - Ferroviária | 7 - Rodoviária | 8 - Conduto / Rede Transmissão | 9 - Meios Próprios | 10 - Entrada / Saída ficta.
        /// </summary>
        /// <param name="texto">Valor para verificação.</param>
        /// <returns>True ou false</returns>
        public bool verificaTipoTransporteInternacional(string texto)
        {
            try
            {
                int qtd = Convert.ToInt32(texto);
                if (qtd >= 1 && qtd <= 10)
                    return true;
                else
                    mensagemAlerta("Valor inválido para o tipo de transporte internacional. Permitido valores de 1 a 10.");
            }
            catch (Exception erro)
            {
                mensagemAlerta("Valor para o tipo de tranporte internacional inválido.");
            }
            return false;
        }

        /// <summary>
        /// Verifica se o valor de Intermédio de Transporte Internacional é válido. Permitido 1 - Importação por conta própria | 2 - Importação por conta e ordem | 3 - Importação por encomenda.
        /// </summary>
        /// <param name="texto">Valor para verificação.</param>
        /// <returns>True ou false</returns>
        public bool verificaTipoIntermedioTransporteInternacional(string texto)
        {
            if (texto.Equals("1") || texto.Equals("2") || texto.Equals("3"))
                return true;
            else
                mensagemAlerta("Valor de intermdio de transporte internacional inválido. Permitido valores de 1 a 3.");
            return false;
        }

        /// <summary>
        /// Verifica se o CNPJ é obrigatório. Caso o valor de Tipo de intermédio seja 2 - Importação por conta e ordem | 3 - Importação por encomenda o preenchimento é obrigatório.
        /// </summary>
        /// <param name="tipoIntermedio">Valor para verificação.</param>
        /// <returns>True ou false</returns>
        public bool verificaCnpjObrigatorioDeclaracaoImportacao(string tipoIntermedio)
        {
            if (tipoIntermedio.Equals("2") || tipoIntermedio.Equals("3"))
                return true;
            return false;
        }

        /// <summary>
        /// Verifica se Drawback foi preenchido e permite preenchimento do Grupo de Exprotação.
        /// </summary>
        /// <param name="texto">Valor para vefificação.</param>
        /// <returns>True ou false</returns>
        public bool verificaDrawbackInformado(string texto)
        {
            if (string.IsNullOrEmpty(texto))
                return true;
            return false;
        }

        /// <summary>
        /// Verifica se os dados para preencher  o Grupo Sobre Exportação Indireta estão preenchidos.
        /// </summary>
        /// <param name="numeroRegistro">Valor do número de registro.</param>
        /// <param name="chaveAcesso">Valor do chave de acesso.</param>
        /// <param name="quantidadeExportado">Valor da quantidade real exportado.</param>
        /// <returns>True ou false</returns>
        public bool verificaGrupoExportacaoIndireta(string numeroRegistro, string chaveAcesso, string quantidadeExportado)
        {
            if (string.IsNullOrEmpty(numeroRegistro) || string.IsNullOrEmpty(chaveAcesso) || string.IsNullOrEmpty(quantidadeExportado))
                return true;
            return false;
        }

        /// <summary>
        /// Verifica se os detalhamento para carros foi preenchido.
        /// </summary>
        /// <param name="dados">Valor para verificação.</param>
        /// <returns>True ou false</returns>
        public bool verificaPreenchimentoVeiculosNovos(List<dadosDetalhamentoEspecificoVeiculosNovos> dados)
        {
            if (dados.Count > 0)
                return true;
            return false;
        }

        /// <summary>
        /// Verifica se o tipo de operação de veículos novos é válida. Permitido 1 - Venda concessionária | 2 - Faturamento direto para consumidor final | 3 - Venda direta para grandes consumidores (frotista,governo, ...) | 0 - Outros
        /// </summary>
        /// <param name="texto">Valor para verificação.</param>
        /// <returns>True ou false</returns>
        public bool verificaTipoOperacaoVeiculosNovos(string texto)
        {
            if (texto.Equals("0") || texto.Equals("1") || texto.Equals("2") || texto.Equals("3"))
                return true;
            else
                mensagemAlerta("Valor do Tipo de operação de Veículos Novos é inválido.");
            return false;
        }

        /// <summary>
        /// Verifica se o valor da Espécie de Veículo é valido. Permitido 1 - PASSAGEIRO | 2 - CARGA | 3 - MISTO | 4 - CORRIDA | 5 - TRAÇÃO | 6 - ESPECIAL
        /// </summary>
        /// <param name="texto">Valor para veririfação.</param>
        /// <returns>True ou false</returns>
        public bool verificaEspecieVeiculo(string texto)
        {
            try
            {
                Int16 valor = Convert.ToInt16(texto);
                if (valor >= 1 && valor <= 6)
                    return true;
            }
            catch (Exception erro)
            {
                mensagemAlerta("Valor de Espécie de Veículo inválido. Válido somente valores de 1 a 6. Erro: " + erro.Message);
            }
            return false;
        }

        /// <summary>
        /// Verifica se o VIN é válido. Permitido R - Remarcado | N - Norma
        /// </summary>
        /// <param name="texto">Valor para verificação.</param>
        /// <returns>True ou false</returns>
        public bool verificaVin(string texto)
        {
            if ((texto.Equals("N") || texto.Equals("n")) && (texto.Equals("R") || texto.Equals("r")))
                return true;
            return false;
        }

        /// <summary>
        /// Verifica se o valor da condição do veículo é válido. Permitido 1 - Acabado | 2 - Inacabado | 3 - Semiacabado
        /// </summary>
        /// <param name="texto">Valor para a verificação.</param>
        /// <returns>True ou false</returns>
        public bool veriricaCondicaoVeiculo(string texto)
        {
            if (texto.Equals("1") || texto.Equals("2") || texto.Equals("3"))
                return true;
            return false;
        }

        /// <summary>
        /// Verifica se o valor do código da cor do Denatran é válido. Permitido 01 - AMARELO | 02 - AZUL | 03 - BEGE | 04 - BRANCA | 05 - CINZA | 06 - DOURADA | 07 - GRENÁ | 08 - LARANJA | 09 - MARROM | 10 - PRATA | 11 - PRETA | 12 - ROSA | 13 - ROXA | 14 - VERDE | 15 - VERMELHA | 16 - FANTASIA 
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public bool verificaCodigoCorDenatran(string texto)
        {
            try
            {
                Int16 valor = Convert.ToInt16(texto);
                if (valor >= 1 && valor <= 16)
                    return true;
                return false;
            }
            catch (Exception erro)
            {
                mensagemAlerta("Valor do código da cor do Denatran é inválida. Permitido valores de 1 a 16. Erro: " + erro.Message);
            }
            return false;
        }

        /// <summary>
        /// Verificação do código do tipo de restrição. Permitido 0 - Não há | 1 - Alienação Fiduciária | 2 - Arrendamento Mercantil | 3 - Reserva de Domínio | 4 - Penhor de Veículos | 9 - Outras
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public bool verificaTipoRestricao(string texto)
        {
            if (texto.Equals("0") || texto.Equals("1") || texto.Equals("2") || texto.Equals("3") || texto.Equals("4") || texto.Equals("9"))
                return true;
            else
                mensagemAlerta("Valor do tipo de restrição inválido. Permitido os valores 0, 1, 2, 3, 4 e 9");
            return false;
        }

        /// <summary>
        /// Verifica se o grupo de medicamentos será preenchido. Caso seja nulo ele ignora o grupo de tags para medicamentos.
        /// </summary>
        /// <param name="dados">Valor para a verificação.</param>
        /// <returns>True ou false</returns>
        public bool verificaDetalhamentoEspecificoMedicamento(List<dadosDetalhamentoEspecificoMedicamentoMateriasPrimasFarmaceuticas> dados)
        {
            if (dados.Count > 0)
                return true;
            return false;
        }

        /// <summary>
        /// Verifica se o grupo de armas será preenchido. Caso seja nulo ele ignora o grupo de tags para medicamentos.
        /// </summary>
        /// <param name="dados">Valor para a verificação.</param>
        /// <returns>True ou false</returns>
        public bool verificaDetalhamentoEspecificoArmamentos(List<dadosDetalhamentoEspecificoArmamentos> dados)
        {
            if (dados.Count > 0)
                return true;
            return false;
        }

        /// <summary>
        /// Verifica se o valor do tipo de arma é valido: Permitido 0 - Uso permitido | 1 - Uso restrito.
        /// </summary>
        /// <param name="texto">Valor para verificação.</param>
        /// <returns>True ou false</returns>
        public bool verificaTipoArma(string texto)
        {
            if (texto.Equals("0") || texto.Equals("1"))
                return true;
            else
                mensagemAlerta("Valor do tipo de arma inválido. Permitido 0 - Uso permitido | 1 - Uso restrito");
            return false;
        }

        /// <summary>
        /// Verifica se o grupo de combustíveis será preenchido. Caso seja nulo ele ignora o grupo de tags para combustíveis.
        /// </summary>
        /// <param name="dados">Valor para a verificação.</param>
        /// <returns>True ou false</returns>
        public bool verificaDetalhamentoEspecificoCombustiveis(List<dadosDetalhamentoEspecificoCombustiveis> dados)
        {
            if (dados.Count > 0)
                return true;
            return false;
        }

        /// <summary>
        /// Verifica se o grupo de Operação com Papel Imune será preenchido. Caso seja nulo ele ignora o grupo de tags para Operação com Papel Imune.
        /// </summary>
        /// <param name="dados">Valor para a verificação.</param>
        /// <returns>True ou false</returns>
        public bool verificaDetalhamentoEspecificoOperacaoPapelImune(List<dadosDetalhamentoEspecificoOperacaoPapelImune> dados)
        {
            if (dados.Count > 0)
                return true;
            return false;
        }

        /// <summary>
        /// Verifica se o o valor da Origem da Mercadoria do ICMS é válido. Permitido 0 - Nacional, exceto as indicadas nos códigos 3, 4, 5 e 8 | 1 - Estrangeira - Importação direta, exceto a indicada no código 6 | 2 - Estrangeira - Adquirida no mercado interno, exceto a indicada no código 7 | 3 - Nacional, mercadoria ou bem com Conteúdo de Importação superior a 40% e inferior ou igual a 70% | 4 - Nacional, cuja produção tenha sido feita em conformidade com os processos produtivos básicos deque tratam as legislações citadas nos Ajustes | 5 - Nacional, mercadoria ou bem com Conteúdo de Importação inferior ou igual a 40% | 6 - Estrangeira - Importação direta, sem similar nacional, constante em lista da CAMEX e gás natural | 7 - Estrangeira - Adquirida no mercado interno, semsimilar nacional, constante lista CAMEX e gás natural | 8 - Nacional, mercadoria ou bem com Conteúdo de Importação superior a 70%
        /// </summary>
        /// <param name="texto">Valor para verificação.</param>
        /// <returns>True ou false</returns>
        public bool verificaOrigemMercadoriaIcms(string texto)
        {
            try
            {
                Int16 valor = Convert.ToInt16(texto);
                if (valor >= 0 && valor <= 8)
                    return true;
            }
            catch (Exception erro)
            {
                mensagemAlerta("Valor para Origem de Mercadoria de ICMS inválido. Permitido valores de 0 a 8. Erro: " + erro.Message);
            }
            return false;
        }

        /// <summary>
        /// Verifica se o ICMS inserido na NFe é o mesmo que o informado pelo sistema.
        /// </summary>
        /// <param name="icmsAtual">ICMS que será inserido na NFe</param>
        /// <param name="icmsInformado">ICMS informado pelo sistema para gerar a NFe</param>
        /// <returns></returns>
        public bool verificaIcms(string icmsAtual, string icmsInformado)
        {
            if (icmsAtual.Equals(icmsInformado))
                return true;
            return false;
        }

        /// <summary>
        /// Verifica se o valor da Modalida de Base de Cálculo é válido. Permitido 0 - Margem Valor Agregado (%) | 1 - Pauta (Valor) | 2 - Preço Tabelado Máx. (valor) | 3 - Valor da operação
        /// </summary>
        /// <param name="texto">Valor para verificação.</param>
        /// <returns>True e false</returns>
        public bool verificaModalidadeBaseCalculo(string texto)
        {
            if (texto.Equals("0") || texto.Equals("1") || texto.Equals("2") || texto.Equals("3"))
                return true;
            else
                mensagemAlerta("Valor para Modelidade de Base de Calculo inválido.");
            return false;
        }

        /// <summary>
        /// Verifica se o valor da Modalida de Base de Cálculo com substituição (ST) é válido. Permitido 0 - Preço tabelado ou máximo sugerido | 1 - Lista Negativa (valor) | 2 - Lista Positiva (valor) | 3 - Lista Neutra (valor) | 4 - Margem Valor Agregado (%) | 5 - Pauta (valor)
        /// </summary>
        /// <param name="texto">Valor para verificação.</param>
        /// <returns>True e false</returns>
        public bool verificaModalidadeBaseCalculoST(string texto)
        {
            if (texto.Equals("0") || texto.Equals("1") || texto.Equals("2") || texto.Equals("3") || texto.Equals("4") || texto.Equals("5"))
                return true;
            else
                mensagemAlerta("Valor para Modelidade de Base de Calculo com substituição (ST) inválido.");
            return false;
        }

        /// <summary>
        /// Verifica se o valor do Motivo da Desoneração de ICMS 20. Permitido 3 - Uso na agropecuária | 9 - Outros | 12 - Órgão de fomento e desenvolvimento agropecuário.
        /// </summary>
        /// <param name="texto">Valor para verificação.</param>
        /// <returns>True ou false</returns>
        public bool verificaMotivoDesoneracaoIcms20(string texto)
        {
            if (texto != null && texto.Trim() != "")
            {
                try
                {
                    Int16 valor = Convert.ToInt16(texto);
                    if (valor == 3 || valor == 9 || valor == 12)
                        return true;
                }
                catch (Exception erro)
                {
                    mensagemAlerta("Valor do motivo de desoneração de ICMS 20 informado inválido. Valor deve ser numérico");
                }
            }
            return false;
        }

        /// <summary>
        /// Verifica se o valor do Motivo da Desoneração de ICMS 30. Permitido 6 - Utilitários e Motocicletas da Amazônia Ocidental e Áreas de Livre Comércio (Resolução 714/88 e 790/94 – CONTRAN e suas alterações) | 7 - SUFRAMA | 9 - Outros.
        /// </summary>
        /// <param name="texto">Valor para verificação.</param>
        /// <returns>True ou false</returns>
        public bool verificaMotivoDesoneracaoIcms30(string texto)
        {
            if (texto != null && texto.Trim() != "")
            {
                try
                {
                    Int16 valor = Convert.ToInt16(texto);
                    if (valor == 6 || valor == 7 || valor == 9)
                        return true;
                }
                catch (Exception erro)
                {
                    mensagemAlerta("Valor do motivo de desoneração de ICMS 30 informado inválido. Valor deve ser numérico");
                }
            }
            return false;
        }

        /// <summary>
        /// Verifica se o valor do Motivo da Desoneração de ICMS 40. Permitido 1 - Táxi | 2 - Deficiente Físico *** Eliminado*** | 3 - Produtor Agropecuário | 4 - Frotista/Locadora | 5 - Diplomático/Consular | 6 - Utilitários e Motocicletas da Amazônia Ocidental e Áreas de Livre Comércio (Resolução 714/88 e 790/94 – CONTRAN e suas alterações) | 7 - SUFRAMA | 8 - Venda a Órgão Público | 9 - Outros. (NT 2011/004) | 10 - Deficiente Condutor (Convênio ICMS 38/12) | 11 - Deficiente Não Condutor (Convênio ICMS 38/12).
        /// </summary>
        /// <param name="texto">Valor para verificação.</param>
        /// <returns>True ou false</returns>
        public bool verificaMotivoDesoneracaoIcms40(string texto)
        {
            if (texto != null && texto.Trim() != "")
            {
                try
                {
                    Int16 valor = Convert.ToInt16(texto);
                    if (valor >= 1 || valor <= 11)
                        return true;
                }
                catch (Exception erro)
                {
                    mensagemAlerta("Valor do motivo de desoneração de ICMS 40 informado inválido. Valor deve ser numérico");
                }
            }
            return false;
        }

        /// <summary>
        /// Verifica os valores da sequência de ICMS 60 foram preenchidos.
        /// </summary>
        /// <param name="baseCalculo">Valor da base de cálculo</param>
        /// <param name="icmsSt">Valor da substituição de ICMS</param>
        /// <returns>True ou false</returns>
        public bool verificaSequenciaIcms60(string baseCalculo, string icmsSt)
        {
            if (string.IsNullOrEmpty(baseCalculo) && string.IsNullOrEmpty(icmsSt))
                return true;
            return false;
        }

        /// <summary>
        /// Verifica se o valor do ICMS 70 desonerado é inválido. Permitido 3 - Uso na agropecuária | 9 - Outros | 12 - Órgão de fomento e desenvolvimento agropecuária.
        /// </summary>
        /// <param name="texto">Valor para verificação.</param>
        /// <returns>True ou false</returns>
        public bool verificaSequenciaIcms70Desonerado(string texto)
        {
            if (!string.IsNullOrEmpty(texto))
            {
                try
                {
                    Int16 valor = Convert.ToInt16(texto);
                    if (valor == 3 || valor == 9 || valor == 12)
                        return true;

                }
                catch (Exception erro)
                {
                    mensagemAlerta("Valor de sequencia de desoneraçao para ICMS 70 inválido. Erro: " + erro.Message);
                }
            }
            return false;
        }

        /// <summary>
        /// Verifica se o valor do ICMS 90 desonerado é inválido. Permitido 3 - Uso na agropecuária | 9 - Outros | 12 - Órgão de fomento e desenvolvimento agropecuária.
        /// </summary>
        /// <param name="texto">Valor para verificação.</param>
        /// <returns>True ou false</returns>
        public bool verificaSequenciaIcms90Desonerado(string texto)
        {
            if (!string.IsNullOrEmpty(texto))
            {
                try
                {
                    Int16 valor = Convert.ToInt16(texto);
                    if (valor == 3 || valor == 9 || valor == 12)
                        return true;

                }
                catch (Exception erro)
                {
                    mensagemAlerta("Valor de sequencia de desoneraçao para ICMS 90 inválido. Erro " + erro.Message);
                }
            }
            return false;
        }

        /// <summary>
        /// Verifica se o valor do tipo de ICMS do Grupo de Partilha informado é válido. Permitido 10 - Tributada e com cobrança do ICMS por substituição tributária | 90 - Outros.
        /// </summary>
        /// <param name="texto">Valor para verificação.</param>
        /// <returns>True ou false</returns>
        public bool verificaTributacaoIcmsGrupoPartilha(string texto)
        {
            if (texto.Equals("10") || texto.Equals("90"))
                return true;
            else
                mensagemAlerta("Valor do tipo de tributação do ICMS do Grupo de Partilha inválido.");
            return false;
        }

        /// <summary>
        /// Verifica se os dados de IPI foram informados
        /// </summary>
        /// <param name="dados">Valor para verificação.</param>
        /// <returns>True ou false</returns>
        public bool verificaIpiInformado(dadosImpostoProdutosIndustrializados dados)
        {
            if (dados != null)
                return true;
            return false;
        }

        /// <summary>
        /// Verifica se o IPI foi informado, caso sim ele verifica se o NCM possui os 8 caracteres obrigatórios nessa situação.
        /// </summary>
        /// <param name="dados">Dados do IPI</param>
        /// <param name="NCM">Valor do NCM</param>
        /// <returns></returns>
        public bool verificaImpostoProdutoInsdustrializadoNcm(dadosImpostoProdutosIndustrializados dados, string NCM)
        {
            if (dados != null)
            {
                if (NCM.Length == 8)
                    return true;
                else
                    mensagemAlerta("Quando o IPI é informado o campo de NCM deve obrigatóriamente ser informado completo, código com 8 caracteres.");
            }
            return false;
        }

        /// <summary>
        /// Verifica se o Grupo do CST de IPI tributado informado corresponde ao grupo de dados correto. Valores 00, 49, 50 e 99.
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public bool verificaGrupoCstIpiTributado(string texto)
        {
            if (texto.Equals("00") || texto.Equals("49") || texto.Equals("50") || texto.Equals("99"))
                return true;
            return false;
        }

        /// <summary>
        /// Verifica se o grupo
        /// </summary>
        /// <param name="texto">Valor para verificação.</param>
        /// <returns>True ou false</returns>
        public bool verificaGrupoCstIpiNt(string texto)
        {
            if (texto.Equals("01") || texto.Equals("02") || texto.Equals("03") || texto.Equals("04") || texto.Equals("51") || texto.Equals("52") || texto.Equals("53") || texto.Equals("54") || texto.Equals("55"))
                return true;
            return false;
        }

        /// <summary>
        /// Verifica se o imposto de importação foi informado.
        /// </summary>
        /// <param name="dados">Valor para verificação.</param>
        /// <returns>True ou false</returns>
        public bool verificaImpostoImportacaoInformado(dadosImpostoImportacao dados)
        {
            if (dados != null)
                return true;
            return false;
        }

        /// <summary>
        /// Verifica se a situação tributada do PIS é válida. Permitido 01 - Operação Tributável (base de cálculo = valor da operação alíquota normal (cumulativo/não cumulativo)) | 02 - Operação Tributável (base de cálculo = valor da operação (alíquota diferenciada))
        /// </summary>
        /// <param name="texto">Valor para verificação.</param>
        /// <returns>True ou false</returns>
        public bool verificaSituacaoTributadaPis(string texto)
        {
            if (texto.Equals("01") || texto.Equals("02"))
                return true;
            else
                mensagemAlerta("Valor de situação tributária de PIS é inválida. Permitido somente 01 e 02");
            return false;
        }

        /// <summary>
        /// Verifica se a situação NÃO tributada do PIS é válida. Permitido 04 - Operação Tributável (tributação monofásica (alíquota zero)) | 05 - Operação Tributável (Substituição Tributária) | 06 - Operação Tributável (alíquota zero) | 07 - Operação Isenta da Contribuição | 08 - Operação Sem Incidência da Contribuição | 09 - Operação com Suspensão da Contribuição
        /// </summary>
        /// <param name="texto">Valor para verificação.</param>
        /// <returns>True ou false</returns>
        public bool verificaSituacaoNaoTributadaPis(string texto)
        {
            if (texto.Equals("04") || texto.Equals("05") || texto.Equals("06") || texto.Equals("07") || texto.Equals("08") || texto.Equals("") || texto.Equals("09"))
                return true;
            else
                mensagemAlerta("Valor informado para a situação NÃO tributada do PIS é inválida. Permitido 04, 05, 06, 07, 08 e 09");
            return false;
        }

        /// <summary>
        /// Verifica se a situação tributada do PIS Outrosn é válida. Permitido 49 - Outras Operações de Saída | 50 - Operação com Direito a Crédito - Vinculada Exclusivamente a Receita Tributada no Mercado Interno | 51 - Operação com Direito a Crédito - Vinculada Exclusivamente a Receita Não Tributada no Mercado Interno| 52 - Operação com Direito a Crédito – Vinculada Exclusivamente a Receita de Exportação | 53 - Operação com Direito a Crédito - Vinculada a Receitas Tributadas e Não-Tributadas no Mercado Interno | 54 - Operação com Direito a Crédito - Vinculada a Receitas Tributadas no Mercado Interno e de Exportação | 55 - Operação com Direito a Crédito - Vinculada a Receitas Não-Tributadas no Mercado Interno e de Exportação | 56 - Operação com Direito a Crédito - Vinculada a Receitas Tributadas e Não-Tributadas no Mercado Interno, e de Exportação | 60 - Crédito Presumido - Operação de Aquisição Vinculada Exclusivamente a Receita Tributada no Mercado Interno | 61 - Crédito Presumido - Operação de Aquisição Vinculada Exclusivamente a Receita Não-Tributada noMercado Interno | 62 - Crédito Presumido - Operação de Aquisição Vinculada Exclusivamente a Receita de Exportação | 63 - Crédito Presumido - Operação de Aquisição Vinculada a Receitas Tributadas e Não-Tributadas noMercado Interno | 64 - Crédito Presumido - Operação de Aquisição Vinculada a Receitas Tributadas no Mercado Interno e de Exportação | 65 - Crédito Presumido - Operação de Aquisição Vinculada a Receitas Não-Tributadas no Mercado Interno e de Exportação | 66 - Crédito Presumido - Operação de Aquisição Vinculada a Receitas Tributadas e Não-Tributadas noMercado Interno, e de Exportação | 67 - Crédito Presumido - Outras Operações | 70 - Operação de Aquisição sem Direito a Crédito | 71 - Operação de Aquisição com Isenção | 72 - Operação de Aquisição com Suspensão | 73 - Operação de Aquisição a Alíquota Zero | 74 - Operação de Aquisição sem Incidência da Contribuição | 75 - Operação de Aquisição por Substituição Tributária | 98 - Outras Operações de Entrada | 99 - Outras Operações
        /// </summary>
        /// <param name="texto">Valor para verificação.</param>
        /// <returns>True ou false</returns>
        public bool verificaSituacaoTributadaOutros(string texto)
        {
            if (texto.Equals("49") || texto.Equals("50") || texto.Equals("51") || texto.Equals("52") || texto.Equals("53") || texto.Equals("54") || texto.Equals("55") || texto.Equals("56") || texto.Equals("60") || texto.Equals("61") || texto.Equals("62") || texto.Equals("63") || texto.Equals("64") || texto.Equals("65") || texto.Equals("66") || texto.Equals("67") || texto.Equals("70") || texto.Equals("71") || texto.Equals("72") || texto.Equals("73") || texto.Equals("74") || texto.Equals("75") || texto.Equals("98") || texto.Equals("99"))
                return true;
            else
                mensagemAlerta("Valor informando para  situação tributária do PIS");
            return false;
        }

        /// <summary>
        /// Verifica se os valores da base de calculo do Pis é em percentual.
        /// </summary>
        /// <param name="baseCalculo">Base de cálculo.</param>
        /// <param name="aliquota">Aliquota.</param>
        /// <returns>True ou false</returns>
        public bool verificaCalculoPisPercentual(string baseCalculo, string aliquota)
        {
            if (!baseCalculo.Equals("") && baseCalculo != null && !aliquota.Equals("") && aliquota != null)
                return true;
            return false;
        }

        /// <summary>
        /// Verifica se os valores da base de calculo do Pis é em valor.
        /// </summary>
        /// <param name="quantidade">Quantidade.</param>
        /// <param name="valor">Valor.</param>
        /// <param name="aliquota">Aliquota.</param>
        /// <returns>True ou false</returns>
        public bool verificaCalculoPisValor(string quantidade, string valor, string aliquota)
        {
            if (!quantidade.Equals("") && quantidade != null && !aliquota.Equals("") && aliquota != null && !valor.Equals("") && valor != null)
                return true;
            return false;
        }

        /// <summary>
        /// Verifica se o valor de substituição do PIS foi informado.
        /// </summary>
        /// <param name="dados">Valor para verificação.</param>
        /// <returns>True ou false</returns>
        public bool verificaPisSt(List<dadosPisST> dados)
        {
            if (dados.Count > 0)
                return true;
            return false;
        }

        /// <summary>
        /// Verifica se a linha verifica é nula. (O campo verificado para indicar a utilização da linha é PISST_vBC. Se for nulo ou estiver em branco ele pula a linha.)
        /// </summary>
        /// <param name="dados">Lista de dados PIS ST</param>
        /// <param name="indice">Índice da linha que será verificada</param>
        /// <returns></returns>
        public bool verificaLinhaPisNula(List<dadosPisST> dados, int indice)
        {
            if (string.IsNullOrEmpty(dados[indice].PISST_vBC))
                return false;
            return true;
        }

        /// <summary>
        /// Verifica se a linha verifica é nula. (O campo verificado para indicar a utilização da linha é PIS_vBC. Se for nulo ou estiver em branco ele pula a linha.)
        /// </summary>
        /// <param name="dados">Lista de dados PIS</param>
        /// <param name="indice">Índice da linha que será verificada</param>
        /// <returns></returns>
        public bool verificaLinhaPisNula(List<dadosPis> dados, int indice)
        {
            if (string.IsNullOrEmpty(dados[indice].PIS_PISAliq_vBC) && string.IsNullOrEmpty(dados[indice].PIS_PISOutr_vBC) && string.IsNullOrEmpty(dados[indice].PIS_PISNT_CST) && string.IsNullOrEmpty(dados[indice].PIS_PISQtde_CST))
                return false;
            return true;
        }

        /// <summary>
        /// Verifica se os dados de COFINS foram informados.
        /// </summary>
        /// <param name="dados">Valor para verificação.</param>
        /// <returns>True ou false</returns>
        public bool verificaCofinsInformado(List<dadosCofins> dados)
        {
            if (dados.Count > 0)
                return true;
            return false;
        }

        /// <summary>
        /// Verifica se a linha verifica é nula. (Os campos verificados para indicar a utilização da linha é COFINS_COFINSAliq_CST, COFINS_COFINSOutr_CST, COFINS_COFINSNT_CST e COFINS_COFINSQtde_CST. Se for nulo ou estiver em branco ele pula a linha.)
        /// </summary>
        /// <param name="dados">Lista de dados COFINS</param>
        /// <param name="indice">Índice da linha que será verificada</param>
        /// <returns></returns>
        public bool verificaLinhaCofinsNula(List<dadosCofins> dados, int indice)
        {
            if (string.IsNullOrEmpty(dados[indice].COFINS_COFINSAliq_CST) && string.IsNullOrEmpty(dados[indice].COFINS_COFINSOutr_CST) && string.IsNullOrEmpty(dados[indice].COFINS_COFINSNT_CST) && string.IsNullOrEmpty(dados[indice].COFINS_COFINSQtde_CST))
                return false;
            return true;
        }

        /// <summary>
        /// Verifica se a linha verifica é nula. (O campo verificado para indicar a utilização da linha é COFINSST_vBC. Se for nulo ou estiver em branco ele pula a linha.)
        /// </summary>
        /// <param name="dados">Lista de dados COFINS ST</param>
        /// <param name="indice">Índice da linha que será verificada</param>
        /// <returns></returns>
        public bool verificaLinhaCofinsNula(List<dadosCofinsST> dados, int indice)
        {
            if (string.IsNullOrEmpty(dados[indice].COFINSST_vBC))
                return false;
            return true;
        }

        /// <summary>
        /// Verifica se a Situação Tributada do COFINS é válida. Permitido 01 - Operação Tributável (base de cálculo = valor da operação alíquota normal (cumulativo/não cumulativo)) | 02 - Operação Tributável (base de cálculo = valor da operação (alíquota diferenciada))
        /// </summary>
        /// <param name="texto">Valor para verificação.</param>
        /// <returns>True ou false</returns>
        public bool verificaSituacaoTributadaCofins(string texto)
        {
            if (texto.Equals("01") || texto.Equals("02"))
                return true;
            else
                mensagemAlerta("Valor da Situação Tributária do COFINS é inválida. Permitido 01 ou 02");
            return false;
        }

        /// <summary>
        /// Verifica se a Situação NÃO Tributada do COFINS é válida. 04 - Operação Tributável (tributação monofásica, alíquota zero) | 05 - Operação Tributável (Substituição Tributária) | 06 - Operação Tributável (alíquota zero) | 07 - Operação Isenta da Contribuição | 08 - Operação Sem Incidência da Contribuição | 09 - Operação com Suspensão da Contribuição
        /// </summary>
        /// <param name="texto">Valor para verificação.</param>
        /// <returns>True ou false</returns>
        public bool verificaSituacaoNaoTributadaCofins(string texto)
        {
            if (texto.Equals("04") || texto.Equals("05") || texto.Equals("06") || texto.Equals("07") || texto.Equals("08") || texto.Equals("09"))
                return true;
            else
                mensagemAlerta("Valor da situação NÃO tributada do COFINS é inválida. Permitido 04, 05, 06, 07, 08 ou 09.");
            return false;
        }

        /// <summary>
        /// Verifica se a Situação Tributária do COFINS outros é válida. Permitido 49 - Outras Operações de Saída | 50 - Operação com Direito a Crédito - Vinculada Exclusivamente a Receita Tributada no Mercado Interno | 51 - Operação com Direito a Crédito - Vinculada Exclusivamente a Receita Não Tributada no Mercado Interno | 52 - Operação com Direito a Crédito – Vinculada Exclusivamente a Receita de Exportação | 53 - Operação com Direito a Crédito - Vinculada a Receitas Tributadas e Não-Tributadas no Mercado Interno | 54 - Operação com Direito a Crédito - Vinculada a Receitas Tributadas no Mercado Interno e de Exportação | 55 - Operação com Direito a Crédito - Vinculada a Receitas Não-Tributadas no Mercado Interno e de Exportação | 56 - Operação com Direito a Crédito - Vinculada a Receitas Tributadas e Não-Tributadas no Mercado Interno, e de Exportação | 60 - Crédito Presumido - Operação de Aquisição Vinculada Exclusivamente a Receita Tributada no Mercado Interno | 61 - Crédito Presumido - Operação de Aquisição Vinculada Exclusivamente a Receita Não-Tributada noMercado Interno | 62 - Crédito Presumido - Operação de Aquisição Vinculada Exclusivamente a Receita de Exportação | 63 - Crédito Presumido - Operação de Aquisição Vinculada a Receitas Tributadas e Não-Tributadas noMercado Interno | 64 - Crédito Presumido - Operação de Aquisição Vinculada a Receitas Tributadas no Mercado Interno e de Exportação | 65 - Crédito Presumido - Operação de Aquisição Vinculada a Receitas Não-Tributadas no Mercado Interno e de Exportação | 66 - Crédito Presumido - Operação de Aquisição Vinculada a Receitas Tributadas e Não-Tributadas noMercado Interno, e de Exportação | 67 - Crédito Presumido - Outras Operações | 70 - Operação de Aquisição sem Direito a Crédito | 71 - Operação de Aquisição com Isenção | 72 - Operação de Aquisição com Suspensão | 73 - Operação de Aquisição a Alíquota Zero | 74 - Operação de Aquisição sem Incidência da Contribuição | 75 - Operação de Aquisição por Substituição Tributária | 98 - Outras Operações de Entrada | 99 - Outras Operações.
        /// </summary>
        /// <param name="texto">Valor para verificação.</param>
        /// <returns>True ou false</returns>
        public bool verificaSituacaoTributadaCofinsOutros(string texto)
        {
            if (texto.Equals("49") || texto.Equals("50") || texto.Equals("51") || texto.Equals("52") || texto.Equals("53") || texto.Equals("54") || texto.Equals("55") || texto.Equals("56") || texto.Equals("60") || texto.Equals("61") || texto.Equals("62") || texto.Equals("63") || texto.Equals("64") || texto.Equals("65") || texto.Equals("66") || texto.Equals("67") || texto.Equals("70") || texto.Equals("71") || texto.Equals("72") || texto.Equals("73") || texto.Equals("74") || texto.Equals("75") || texto.Equals("98") || texto.Equals("99"))
                return true;
            else
                mensagemAlerta("Valor da Situação tributada do COFINS para Outros é inválida. Permitido 49 - Outras Operações de Saída | 50 - Operação com Direito a Crédito - Vinculada Exclusivamente a Receita Tributada no Mercado Interno | 51 - Operação com Direito a Crédito - Vinculada Exclusivamente a Receita Não Tributada no Mercado Interno | 52 - Operação com Direito a Crédito – Vinculada Exclusivamente a Receita de Exportação | 53 - Operação com Direito a Crédito - Vinculada a Receitas Tributadas e Não-Tributadas no Mercado Interno | 54 - Operação com Direito a Crédito - Vinculada a Receitas Tributadas no Mercado Interno e de Exportação | 55 - Operação com Direito a Crédito - Vinculada a Receitas Não-Tributadas no Mercado Interno e de Exportação | 56 - Operação com Direito a Crédito - Vinculada a Receitas Tributadas e Não-Tributadas no Mercado Interno, e de Exportação | 60 - Crédito Presumido - Operação de Aquisição Vinculada Exclusivamente a Receita Tributada no Mercado Interno | 61 - Crédito Presumido - Operação de Aquisição Vinculada Exclusivamente a Receita Não-Tributada noMercado Interno | 62 - Crédito Presumido - Operação de Aquisição Vinculada Exclusivamente a Receita de Exportação | 63 - Crédito Presumido - Operação de Aquisição Vinculada a Receitas Tributadas e Não-Tributadas noMercado Interno | 64 - Crédito Presumido - Operação de Aquisição Vinculada a Receitas Tributadas no Mercado Interno e de Exportação | 65 - Crédito Presumido - Operação de Aquisição Vinculada a Receitas Não-Tributadas no Mercado Interno e de Exportação | 66 - Crédito Presumido - Operação de Aquisição Vinculada a Receitas Tributadas e Não-Tributadas noMercado Interno, e de Exportação | 67 - Crédito Presumido - Outras Operações | 70 - Operação de Aquisição sem Direito a Crédito | 71 - Operação de Aquisição com Isenção | 72 - Operação de Aquisição com Suspensão | 73 - Operação de Aquisição a Alíquota Zero | 74 - Operação de Aquisição sem Incidência da Contribuição | 75 - Operação de Aquisição por Substituição Tributária | 98 - Outras Operações de Entrada | 99 - Outras Operações");
            return false;
        }

        /// <summary>
        /// Verifica se o valor do COFINS com substituição foi informado.
        /// </summary>
        /// <param name="dados">Valor para verificação.</param>
        /// <returns>True ou false</returns>
        public bool verificaCofinsSt(List<dadosCofinsST> dados)
        {
            if (dados.Count > 0)
                return true;
            return false;
        }

        /// <summary>
        /// Verifica se o grupo de dados o ISSQN foi preenchido.
        /// </summary>
        /// <param name="dados">Valor para verificação.</param>
        /// <returns>True ou false</returns>
        public bool verificaIssqn(List<dadosISSQN> dados)
        {
            if (dados.Count > 0)
                return true;
            return false;
        }

        /// <summary>
        /// Verifica se os impostos de ICMS, IPI e II foram preenchidos. Caso não ISSQN pode ser preenchido.
        /// </summary>
        /// <param name="icms">Valor do ICMS.</param>
        /// <param name="ipi">Valor do IPI.</param>
        /// <param name="importacao">Valor de II</param>
        /// <returns>True ou false</returns>
        public bool verificaIssqnExclusivo(dadosICMSNormalST icms, dadosImpostoProdutosIndustrializados ipi, dadosImpostoImportacao importacao)
        {
            if (icms == null && ipi == null && importacao == null)
                return true;
            return false;
        }

        /// <summary>
        /// Verifica se o valor do Indicador de Exigibilidade do ISS é válido. Permitido 1 - Exigível | 2 - Não incidência | 3 - Isenção | 4 - Exportação | 5 - Imunidade | 6 - Exigibilidade Suspensa por Decisão Judicial | 7 - Exigibilidade Suspensa por Processo Administrativo
        /// </summary>
        /// <param name="texto">Valor para verificação.</param>
        /// <returns>True ou false</returns>
        public bool verificaIndicadorExigibilidadeIss(string texto)
        {
            try
            {
                Int16 valor = Convert.ToInt16(texto);
                if (valor >= 1 && valor <= 7)
                    return true;
            }
            catch (Exception erro)
            {
                mensagemAlerta("Valor informado para o Indicador de Exigibilidade do ISS é inválido, valor permitido de 1 a 7. Erro: " + erro.Message);
            }
            return false;
        }

        /// <summary>
        /// Verifica se a cidade informada para o ISSQN é no exterior. Caso seja o código do país deve ser preenchido.
        /// </summary>
        /// <param name="texto">Valor para verificação.</param>
        /// <returns>True ou false</returns>
        public bool verificaMunicipioIssqn(string texto)
        {
            if (texto.Equals("9999999"))
                return true;
            return false;
        }

        /// <summary>
        /// Verifica se o Numero de processo tem que ser preenchido. Caso o valor do Indicador de Exigibilidade seja suspenso, ou seja, 6 ou 7
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public bool verificaNumeroProcesso(string texto)
        {
            if (texto.Equals("6") || texto.Equals("7"))
                return true;
            return false;
        }

        /// <summary>
        /// Verifica se o valor do Indicador de Incentivo Fiscal é válido. Permitido 1 - Sim | 2 - Não
        /// </summary>
        /// <param name="texto">Valor para verificação.</param>
        /// <returns>True ou false</returns>
        public bool verificaIndicadorIncentivoFiscal(string texto)
        {
            if (texto.Equals("1") || texto.Equals("2"))
                return true;
            else
                mensagemAlerta("Valor para o Indicador de incentivo fiscal é inválido. Premitido os valores 1 ou 2.");
            return false;
        }

        /// <summary>
        /// Verifica se o o grupo Tributos Devolvidos foi informado.
        /// </summary>
        /// <param name="dados">Valor para verificação.</param>
        /// <returns>True ou false</returns>
        public bool verificaTributosDevolvido(List<dadosTributosDevolvidos> dados)
        {
            if (dados.Count > 0)
                return true;
            return false;
        }

        /// <summary>
        /// Verifica se o valor da porcentagem de mercadorias devolvidas é inválido.
        /// </summary>
        /// <param name="texto">Valor para verificação.</param>
        /// <returns>True ou false</returns>
        public bool verificaPorcentagemMercadoriaDevolvida(string texto)
        {
            try
            {
                decimal valor = Convert.ToDecimal(texto);
                if (valor <= 100 && valor > 0)
                    return true;
                else
                    mensagemAlerta("O valor informado para a porcentagem de mercadorias devolvidas é inválido. Ele deve ser maior que 0 e menor que 100.");
            }
            catch (Exception erro)
            {
                mensagemAlerta("Valor informado para a porcentagem de mercadorias devolvidas está em um formado inválido.");
            }
            return false;
        }

        /// <summary>
        /// Verifica se o grupo de Totais Referente ao ISSQN foi informado.
        /// </summary>
        /// <param name="dados">Valor para verificação.</param>
        /// <returns>True ou false</returns>
        public bool verificaGrupoTotaisReferenteIssqn(List<dadosTotalNFeISSQN> dados)
        {
            if (dados.Count > 0)
                return true;
            return false;
        }

        /// <summary>
        /// Verifica se o valor do Código de Regime Especial de Tributação é válido. Permitido 1 - Microempresa Municipal | 2 - Estimativa | 3 - Sociedade de Profissionais | 4 - Cooperativa | 5 - Microempresário Individual (MEI) | 6 - Microempresário e Empresa de Pequeno Porte (ME/EPP)
        /// </summary>
        /// <param name="texto">Valor para verificação.</param>
        /// <returns>true ou false</returns>
        public bool verificaCodigoRegimeEspecialTributacao(string texto)
        {
            try
            {
                Int16 valor = Convert.ToInt16(texto);
                if (valor >= 1 && valor <= 6)
                    return true;
                else
                    mensagemAlerta("Valor do Código de Regime Especial de Tributação é inválido. Permitido valores de 1 a 7.Valor do ");
            }
            catch (Exception erro)
            {
                mensagemAlerta("Valor do Código de Regime Especial de Tributação é inválido. Permitido valores de 1 a 7.");
            }
            return false;
        }

        /// <summary>
        /// Verifique se o grupo de Total da NFe / Retenção de Produto foi informado.
        /// </summary>
        /// <param name="dados">Valor para verificação.</param>
        /// <returns>True ou false</returns>
        public bool verificaTotalNfeRetencaoProduto(List<dadosTotalNFeRetencaoTributos> dados)
        {
            if (dados.Count > 0)
                return true;
            return false;
        }

        /// <summary>
        /// Verifica se o valor da modalidade do frete é válido. Permitido 0 - Por conta do emitente | 1 - Por conta do destinatário/remetente | 2 - Por conta de terceiros | 9 - Sem frete. (V2.0)
        /// </summary>
        /// <param name="texto">Valor para verificação.</param>
        /// <returns>True ou false</returns>
        public bool verificaModalidadeFrete(string texto)
        {
            if (texto.Equals("0") || texto.Equals("1") || texto.Equals("2") || texto.Equals("9"))
                return true;
            else
                mensagemAlerta("Valor da modadlidade do frete inválido. Permitido 0,1,2 ou 9");
            return false;
        }

        /// <summary>
        /// Verifica se o grupo de informações de Transpotador foi preenchida.
        /// </summary>
        /// <param name="cnpj">CNPJ</param>
        /// <param name="cpf">CPF</param>
        /// <param name="nome">Nome</param>
        /// <param name="ie">Inscrição Estadual</param>
        /// <param name="endereco">Endereço</param>
        /// <param name="municipio">Município</param>
        /// <param name="estado">Estado</param>
        /// <returns></returns>
        public bool verificaGrupoTransportador(string cnpj, string cpf, string nome, string ie, string endereco, string municipio, string estado)
        {
            if (string.IsNullOrEmpty(cnpj) && string.IsNullOrEmpty(cpf) && string.IsNullOrEmpty(nome) && string.IsNullOrEmpty(ie) && string.IsNullOrEmpty(endereco) && string.IsNullOrEmpty(municipio) && string.IsNullOrEmpty(estado))
                return true;
            return false;
        }

        /// <summary>
        /// Verifica se o grupo de Retenção de ICMS de Transporte foi informado.
        /// </summary>
        /// <param name="valorSevico">Valor do Serviço</param>
        /// <param name="baseCalculo">Base de cálculo</param>
        /// <param name="aliquota">Alíquota</param>
        /// <param name="valorIcms">Valor do ICMS</param>
        /// <param name="cfop">CFOP</param>
        /// <param name="codigoMunicipio">Código do município</param>
        /// <returns>True ou false</returns>
        public bool verificaGrupoRetencaoIcmsTransporte(string valorSevico, string baseCalculo, string aliquota, string valorIcms, string cfop, string codigoMunicipio)
        {
            if (string.IsNullOrEmpty(valorSevico) && string.IsNullOrEmpty(baseCalculo) && string.IsNullOrEmpty(aliquota) && string.IsNullOrEmpty(valorIcms) && string.IsNullOrEmpty(cfop) && string.IsNullOrEmpty(codigoMunicipio))
                return true;
            return false;
        }

        /// <summary>
        /// Verifica se o grupo de Veículo de Transporte foi informado
        /// </summary>
        /// <param name="placa">Placa</param>
        /// <param name="uf">UF</param>
        /// <returns>True ou false</returns>
        public bool verificaGrupoVeiculoTransporte(string placa, string uf)
        {
            if (string.IsNullOrEmpty(placa) && string.IsNullOrEmpty(uf))
                return true;
            return false;
        }

        /// <summary>
        /// Verifica se o grupo Volumes foi informado.
        /// </summary>
        /// <param name="quantidadeVolume">Quantidade de volumes</param>
        /// <param name="especieVolume">Espécie do Volume</param>
        /// <param name="marca">Marca</param>
        /// <param name="numeroVolume">Número do Volume</param>
        /// <param name="pesoLiquido">Peso líquido</param>
        /// <param name="pesoBruto">Peso bruto</param>
        /// <returns></returns>
        public bool verificaGrupoVolumes(string quantidadeVolume, string especieVolume, string marca, string numeroVolume, string pesoLiquido, string pesoBruto)
        {
            if (string.IsNullOrEmpty(quantidadeVolume) && string.IsNullOrEmpty(especieVolume) && string.IsNullOrEmpty(marca) && string.IsNullOrEmpty(numeroVolume) && string.IsNullOrEmpty(pesoLiquido) && string.IsNullOrEmpty(pesoBruto))
                return true;
            return false;
        }

        /// <summary>
        /// Verifica se o o grupo lacres foi informado.
        /// </summary>
        /// <param name="lacre">Lacre</param>
        /// <returns>True ou false</returns>
        public bool verificaGrupoLacre(string lacre)
        {
            if (string.IsNullOrEmpty(lacre))
                return true;
            return false;
        }

        /// <summary>
        /// Verifica se o grupo Fatura do grupo de Cobrança foi preenchido.
        /// </summary>
        /// <param name="numeroFatura">Número da fatura</param>
        /// <param name="valorOriginalFatura">Valor original da fatura</param>
        /// <param name="valorDesconto">Valor do desconto</param>
        /// <param name="valorLiquido">Valor líquido</param>
        /// <returns>True ou false</returns>
        public bool verificaGrupoCobrancaFatura(string numeroFatura, string valorOriginalFatura, string valorDesconto, string valorLiquido)
        {
            if (string.IsNullOrEmpty(numeroFatura) && string.IsNullOrEmpty(valorOriginalFatura) && string.IsNullOrEmpty(valorDesconto) && string.IsNullOrEmpty(valorLiquido))
                return true;
            return false;
        }

        /// <summary>
        /// Verifica se o grupo Duplicata do grupo de CObrança foi preenchido.
        /// </summary>
        /// <param name="numeroDuplicata">Número da duplicata</param>
        /// <param name="dataVencimento">Data de vencimento</param>
        /// <param name="valorDuplicata">Valor da duplicata</param>
        /// <returns></returns>
        public bool verificaGrupoCobrancaDuplicata(string numeroDuplicata, string dataVencimento, string valorDuplicata)
        {
            if (string.IsNullOrEmpty(numeroDuplicata) && string.IsNullOrEmpty(dataVencimento) && string.IsNullOrEmpty(valorDuplicata))
                return true;
            return false;
        }

        /// <summary>
        /// Verifica se o valor do tipo de pagamento é válido. Permitido 01 - Dinheiro | 02 - Cheque | 03 - Cartão de Crédito | 04 - Cartão de Débito | 05 - Crédito Loja | 10 - Vale Alimentação | 11 - Vale Refeição | 12 - Vale Presente | 13 - Vale Combustível | 99 - Outros
        /// </summary>
        /// <param name="texto">Valor para verificação.</param>
        /// <returns>True ou false</returns>
        public bool verificaTipoFormaPagamento(string texto)
        {
            if (texto.Equals("01") || texto.Equals("02") || texto.Equals("03") || texto.Equals("04") || texto.Equals("05") || texto.Equals("10") || texto.Equals("11") || texto.Equals("12") || texto.Equals("13") || texto.Equals("99"))
                return true;
            else
                mensagemAlerta("O valor informado para o Tipo de Forma de Pagamento é inválido. Permitido 01,02,03,04,05,10,11,12,13 ou 99.");
            return false;
        }

        /// <summary>
        /// Verifica se o grupo Cartão foi informado.
        /// </summary>
        /// <param name="cnpj">CNPJ</param>
        /// <param name="bandeira">Bandeira</param>
        /// <param name="codigoAutorizacao">Código de autorização</param>
        /// <returns></returns>
        public bool verificaGrupoCartoes(string cnpj, string bandeira, string codigoAutorizacao)
        {
            if (string.IsNullOrEmpty(cnpj) && string.IsNullOrEmpty(bandeira) && string.IsNullOrEmpty(codigoAutorizacao))
                return true;
            return false;
        }

        /// <summary>
        /// Verifica se o valor do tipo de bandeira é válido. Permitido 01 - Visa | 02 - Mastercard | 03 - American Express | 04 - Sorocred | 99 - Outros
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public bool verificaTipoBandeira(string texto)
        {
            if (texto.Equals("01") || texto.Equals("02") || texto.Equals("03") || texto.Equals("04") || texto.Equals("99"))
                return true;
            else
                mensagemAlerta("Valor do Tipo de Bandeira inválido. Permitido 01, 02, 03, 04 ou 99.");
            return false;
        }

        /// <summary>
        /// Verifica se o grupo de Informações Adicionais foi informado.
        /// </summary>
        /// <param name="informacoesAdicionais">Informações adicionais</param>
        /// <param name="informacoesComplementares">informações complementares</param>
        /// <returns>True ou false</returns>
        public bool verificaGrupoInformacoesAdicionais(string informacoesAdicionais, string informacoesComplementares)
        {
            if (string.IsNullOrEmpty(informacoesAdicionais) && string.IsNullOrEmpty(informacoesComplementares))
                return true;
            return false;
        }

        /// <summary>
        /// Verifica se o grupo de informações Livre para o Contribuinte foi informado.
        /// </summary>
        /// <param name="identificacaoCampo">Identificação do campo</param>
        /// <param name="conteudoCampo">Conteúdo do campo</param>
        /// <returns>True ou false</returns>
        public bool verificaCampoLivreContribuinte(string identificacaoCampo, string conteudoCampo)
        {
            if (string.IsNullOrEmpty(identificacaoCampo) && string.IsNullOrEmpty(conteudoCampo))
                return true;
            return false;
        }

        /// <summary>
        /// Verifica se o grupo de informações Livre para o Fisco foi informado.
        /// </summary>
        /// <param name="identificacaoCampo">Identificação do campo</param>
        /// <param name="conteudoCampo">Conteúdo do campo</param>
        /// <returns>True ou false</returns>
        public bool verificaCampoLivreFisco(string identificacaoCampo, string conteudoCampo)
        {
            if (identificacaoCampo.Length > 0 || conteudoCampo.Length > 0)
                return true;
            return false;
        }

        /// <summary>
        /// Verifica se o grupo de Processo Referenciado foi informado.
        /// </summary>
        /// <param name="identificadoProcesso">Identificador do processo ou ato concessório</param>
        /// <param name="indicadorOrigem">Indicador origem do processo</param>
        /// <returns>True ou false</returns>
        public bool verificaGrupoProcessoReferenciado(string identificadoProcesso, string indicadorOrigem)
        {
            if (string.IsNullOrEmpty(identificadoProcesso) && string.IsNullOrEmpty(indicadorOrigem))
                return true;
            return false;
        }

        /// <summary>
        /// Verifica se o valor o Indentificador de Origem do Processo é válido. Permitido 0 - SEFAZ | 1 - Justiça Federal | 2 - Justiça Estadual | 3 - Secex/RFB | 9 - Outros
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public bool verificaIdentificadorOrigemProcesso(string texto)
        {
            if (texto.Equals("0") || texto.Equals("1") || texto.Equals("2") || texto.Equals("3") || texto.Equals("9"))
                return true;
            else
                mensagemAlerta("Valor para o Identificador de Origem de Processo é válido. Permitido 0, 1, 2, 3 ou 9.");
            return false;
        }

        /// <summary>
        /// Verifica se o grupo Informações de Comércio Exterior foi informado.
        /// </summary>
        /// <param name="uf">UF</param>
        /// <param name="localEmbarque">local de embarque</param>
        /// <returns></returns>
        public bool verificaInformacoesComercioExterior(string uf, string localEmbarque)
        {
            if (string.IsNullOrEmpty(localEmbarque) && string.IsNullOrEmpty(uf))
                return true;
            return false;
        }

        /// <summary>
        /// Verifica se o grupo de Informações de Compra foi informado.
        /// </summary>
        /// <param name="notaEmpenho">nota de empenho</param>
        /// <param name="pedido">Pedido</param>
        /// <param name="contrato">Contrato</param>
        /// <returns>True ou false</returns>
        public bool verificaInformacoesCompra(string notaEmpenho, string pedido, string contrato)
        {
            if (string.IsNullOrEmpty(notaEmpenho) && string.IsNullOrEmpty(pedido) && string.IsNullOrEmpty(contrato))
                return true;
            return false;
        }

        /// <summary>
        /// Verifica se o grupo de Informações de Registro de Aquisição de Cana foi informado.
        /// </summary>
        /// <param name="safra">Safra</param>
        /// <param name="referencia">Refência(mes e ano)</param>
        /// <returns></returns>
        public bool verificaInformacoesRegistroAquisicaoCana(string safra, string referencia)
        {
            if (string.IsNullOrEmpty(safra) && string.IsNullOrEmpty(referencia))
                return true;
            return false;
        }

        /// <summary>
        /// Verifica se o grupo de Deduções foi informado.
        /// </summary>
        /// <param name="descricaoDeducao">Descrição</param>
        /// <param name="valorDeducao">Valor da dedução</param>
        /// <param name="valorFornecimento">Valor do fornecimento</param>
        /// <param name="valorTotal">Valor total</param>
        /// <param name="valorLiquido">Valor liquido</param>
        /// <returns>True ou false</returns>
        public bool verificaGrupoDeducoes(string descricaoDeducao, string valorDeducao, string valorFornecimento, string valorTotal, string valorLiquido)
        {
            if (string.IsNullOrEmpty(descricaoDeducao) && string.IsNullOrEmpty(valorDeducao) && string.IsNullOrEmpty(valorFornecimento) && string.IsNullOrEmpty(valorTotal) && string.IsNullOrEmpty(valorLiquido))
                return true;
            return false;
        }

        /// <summary>
        /// Verifica a mensagem de retorno de retorno da Recepção da NF-e enviada
        /// </summary>
        /// <param name="retornoXml">XML de retorno</param>
        /// <returns>True ou false</returns>
        public bool verificaMensagemRetornoRecepcao(XmlNode retornoXml)
        {
            string codigoMensagem = string.Empty;
            string mensagemServidor = string.Empty;
            string numeroRecibo = string.Empty;
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(retornoXml.OuterXml);
            foreach (XmlNode xn in xml)
            {
                if (xn["cStat"] != null)
                    codigoMensagem = xn["cStat"].InnerText;
                if (xn["xMotivo"] != null)
                    mensagemServidor = xn["xMotivo"].InnerText;
                //if(xn["xMotivo"] != null)
                //numeroRecibo = xn["xMotivo"].InnerText;
            }


            mensagemAlerta("Número do recibo: " + numeroRecibo);
            mensagemAlerta(mensagemServidor);

            return false;
        }

        /// <summary>
        /// Gera o arquivo com a estrutura de comunicação SOAP baseada no retorno WSDL do servidor.
        /// </summary>
        /// <param name="wsdlPath"></param>
        /// <param name="outputFilePath"></param>
        public void geraXmlSoap(string wsdlPath, string outputFilePath)
        {
            if (File.Exists(wsdlPath) == false)
            {
                return;
            }

            ServiceDescription wsdlDescription = ServiceDescription.Read(wsdlPath);
            ServiceDescriptionImporter wsdlImporter = new ServiceDescriptionImporter();

            wsdlImporter.ProtocolName = "Soap12";
            wsdlImporter.AddServiceDescription(wsdlDescription, null, null);
            wsdlImporter.Style = ServiceDescriptionImportStyle.Server;

            wsdlImporter.CodeGenerationOptions = CodeGenerationOptions.GenerateProperties;

            CodeNamespace codeNamespace = new CodeNamespace();
            CodeCompileUnit codeUnit = new CodeCompileUnit();
            codeUnit.Namespaces.Add(codeNamespace);

            ServiceDescriptionImportWarnings importWarning = wsdlImporter.Import(codeNamespace, codeUnit);

            if (importWarning == 0)
            {
                StringBuilder stringBuilder = new StringBuilder();
                StringWriter stringWriter = new StringWriter(stringBuilder);

                CodeDomProvider codeProvider = CodeDomProvider.CreateProvider("CSharp");
                codeProvider.GenerateCodeFromCompileUnit(codeUnit, stringWriter, new CodeGeneratorOptions());

                stringWriter.Close();

                File.WriteAllText(outputFilePath, stringBuilder.ToString(), Encoding.UTF8);
            }
            else
            {
                mensagemAlerta(importWarning.ToString());
            }
        }

        /// <summary>
        /// Envia os dados para o servidor da SEFAZ, incluindo o SOAP.
        /// </summary>
        /// <param name="documento"></param>
        /// <param name="chaveAcessoGeradaNfe"></param>
        /// <returns></returns>
        public dadosConsultaProcessamentoLote enviaNotaFiscalServidor(XmlDocument documento, string chaveAcessoGeradaNfe)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(documento.OuterXml);
            string estadoNota = string.Empty;
            string modeloNota = string.Empty;
            String caminho = string.Empty;
            XmlNodeList listaNode = xml.GetElementsByTagName("ide");
            StringBuilder xmlCompleto = new StringBuilder();

            foreach (XmlNode xn in listaNode)
            {
                if (xn["cUF"] != null && estadoNota.Equals(string.Empty))
                {
                    estadoNota = xn["cUF"].InnerText;
                }
                if (xn["mod"] != null)
                {
                    modeloNota = xn["mod"].InnerText;
                }
            }

            if (modeloNota.Equals("55"))
            {
                caminho = Path.GetFullPath("recepcaonfelote");
                DirectoryInfo dirInfo = new DirectoryInfo(caminho);
                if (!dirInfo.Exists)
                    Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + @"/recepcaonfelote");

                documento.Save(caminho + "\\" + chaveAcessoGeradaNfe + "-env-lot.xml");
                assinaturaNfe assinarNotaFiscal = new assinaturaNfe();
                assinarNotaFiscal.assinarNfe(caminho + "\\" + chaveAcessoGeradaNfe + "-env-lot.xml", "Signature", "NFe", chaveAcessoGeradaNfe);
                documento.Load(caminho + "\\" + chaveAcessoGeradaNfe + "-env-lot.xml");
            }
            else if (modeloNota.Equals("65"))
            {
                caminho = Path.GetFullPath("recepcaonfcelote");
                DirectoryInfo dirInfo = new DirectoryInfo(caminho);
                if (!dirInfo.Exists)
                    Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + @"/recepcaonfcelote");

                documento.Save(caminho + "\\" + chaveAcessoGeradaNfe + "-env-lot.xml");
                assinaturaNfe assinarNotaFiscal = new assinaturaNfe();
                assinarNotaFiscal.assinarNfe(caminho + "\\" + chaveAcessoGeradaNfe + "-env-lot.xml", "Signature", "NFe", chaveAcessoGeradaNfe);
                documento.Load(caminho + "\\" + chaveAcessoGeradaNfe + "-env-lot.xml");
            }
            else
            {
                mensagemAlerta("Modelo da Nota Fiscal é inválido.");
            }

            if (estadoNota.Equals("31"))
            {
                webserviceAutorizacaoLoteHomo.NfeAutorizacao wsEnviaLoteNfe = new webserviceAutorizacaoLoteHomo.NfeAutorizacao();

                xmlCompleto.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                xmlCompleto.Append("<soap12:Envelope xmlns:soap12=\"http://www.w3.org/2003/05/soap-envelope\"");
                xmlCompleto.Append(" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"");
                xmlCompleto.Append(" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">");
                xmlCompleto.Append("<soap12:Header>");
                xmlCompleto.Append("<nfeCabecMsg");
                xmlCompleto.Append(" xmlns=\"http://www.portalfiscal.inf.br/nfe/wsdl/NfeAutorizacao\">");
                xmlCompleto.Append("<cUF>31</cUF>");
                xmlCompleto.Append("<versaoDados>3.10</versaoDados>");
                xmlCompleto.Append("</nfeCabecMsg>");
                xmlCompleto.Append("</soap12:Header>");
                xmlCompleto.Append("<soap12:Body>");
                xmlCompleto.Append("<nfeDadosMsg");
                xmlCompleto.Append(" xmlns=\"http://www.portalfiscal.inf.br/nfe/wsdl/NfeAutorizacao\">");
                xmlCompleto.Append(documento.OuterXml.Replace("<?xml version=\"1.0\" encoding=\"UTF-8\"?>", string.Empty)); //remove o cabeçalho criado no início do documento para validação do formato realizada pelo .NET;
                xmlCompleto.Append("</nfeDadosMsg>");
                xmlCompleto.Append("</soap12:Body>");
                xmlCompleto.Append("</soap12:Envelope>");

                documento.LoadXml(xmlCompleto.ToString());

                assinaturaNfe assina = new assinaturaNfe();

                X509Certificate2Collection colecaoCertificado = assina.carregaCertificados();
                if (colecaoCertificado.Count > 0)
                {
                    dadosConsultaProcessamentoLote dadosConsulta = new dadosConsultaProcessamentoLote(false, null, null);
                    wsEnviaLoteNfe.ClientCertificates.Add((assina.carregaCertificados())[0]);
                    XmlNode retornoAutorizacao;
                    try
                    {
                        retornoAutorizacao = wsEnviaLoteNfe.NfeAutorizacaoLote(documento.DocumentElement);
                        dadosConsulta.statusEnvio = verificaMensagemRetornoRecepcao(retornoAutorizacao);
                    }
                    catch (Exception err)
                    {
                        string bof = err.ToString();
                    }

                    Timer delay = new Timer();
                    delay.Interval = 5000;
                    //delay.Start();

                    dadosConsulta.nRec = "";
                    dadosConsulta.tpAmb = "2";
                    return dadosConsulta;
                }
                else
                {
                    mensagemAlerta("Certificado não localizado.");
                }
            }
            else if (estadoNota.Equals("13"))
            {
                br.gov.am.sefaz.homnfe.NfeAutorizacao wsEnviaLoteNfe = new br.gov.am.sefaz.homnfe.NfeAutorizacao();

                xmlCompleto.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                xmlCompleto.Append("<soap12:Envelope xmlns:soap12=\"http://www.w3.org/2003/05/soap-envelope\"");
                xmlCompleto.Append(" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"");
                xmlCompleto.Append(" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">");
                xmlCompleto.Append("<soap12:Header>");
                xmlCompleto.Append("<nfeCabecMsg");
                xmlCompleto.Append(" xmlns=\"http://www.portalfiscal.inf.br/nfe/wsdl/NfeAutorizacao/nfeAutorizacaoLote\">");
                xmlCompleto.Append("<cUF>13</cUF>");
                xmlCompleto.Append("<versaoDados>3.10</versaoDados>");
                xmlCompleto.Append("</nfeCabecMsg>");
                xmlCompleto.Append("</soap12:Header>");
                xmlCompleto.Append("<soap12:Body>");
                xmlCompleto.Append("<nfeDadosMsg");
                xmlCompleto.Append(" xmlns=\"http://www.portalfiscal.inf.br/nfe/wsdl/NfeAutorizacao/nfeAutorizacaoLote\">");
                xmlCompleto.Append(documento.OuterXml.Replace("<?xml version=\"1.0\" encoding=\"UTF-8\"?>", string.Empty)); //remove o cabeçalho criado no início do documento para validação do formato realizada pelo .NET;
                xmlCompleto.Append("</nfeDadosMsg>");
                xmlCompleto.Append("</soap12:Body>");
                xmlCompleto.Append("</soap12:Envelope>");

                documento.LoadXml(xmlCompleto.ToString());

                assinaturaNfe assina = new assinaturaNfe();

                X509Certificate2Collection colecaoCertificado = assina.carregaCertificados();
                if (colecaoCertificado.Count > 0)
                {
                    dadosConsultaProcessamentoLote dadosConsulta = new dadosConsultaProcessamentoLote(false, null, null);
                    wsEnviaLoteNfe.ClientCertificates.Add((assina.carregaCertificados())[0]);
                    XmlNode retornoAutorizacao;
                    retornoAutorizacao = wsEnviaLoteNfe.nfeAutorizacaoLote(documento.DocumentElement);
                    dadosConsulta.statusEnvio = verificaMensagemRetornoRecepcao(retornoAutorizacao);

                    Timer delay = new Timer();
                    delay.Interval = 5000;
                    //delay.Start();

                    dadosConsulta.nRec = "";
                    dadosConsulta.tpAmb = "2";

                    return dadosConsulta;
                }
                else
                {
                    mensagemAlerta("Certificado não localizado.");
                }
            }
            else if (estadoNota.Equals("43"))
            {
                br.gov.rs.sefaz.nfe.homologacao.NfeAutorizacao wsEnviaLoteNfe = new br.gov.rs.sefaz.nfe.homologacao.NfeAutorizacao();

                xmlCompleto.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                xmlCompleto.Append("<soap12:Envelope xmlns:soap12=\"http://www.w3.org/2003/05/soap-envelope\"");
                xmlCompleto.Append(" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"");
                xmlCompleto.Append(" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">");
                xmlCompleto.Append("<soap12:Header>");
                xmlCompleto.Append("<nfeCabecMsg");
                xmlCompleto.Append(" xmlns=\"http://www.portalfiscal.inf.br/nfe/wsdl/NfeAutorizacao/nfeAutorizacaoLote\">");
                xmlCompleto.Append("<cUF>43</cUF>");
                xmlCompleto.Append("<versaoDados>3.10</versaoDados>");
                xmlCompleto.Append("</nfeCabecMsg>");
                xmlCompleto.Append("</soap12:Header>");
                xmlCompleto.Append("<soap12:Body>");
                xmlCompleto.Append("<nfeDadosMsg");
                xmlCompleto.Append(" xmlns=\"http://www.portalfiscal.inf.br/nfe/wsdl/NfeAutorizacao/nfeAutorizacaoLote\">");
                xmlCompleto.Append(documento.OuterXml.Replace("<?xml version=\"1.0\" encoding=\"UTF-8\"?>", string.Empty)); //remove o cabeçalho criado no início do documento para validação do formato realizada pelo .NET;
                xmlCompleto.Append("</nfeDadosMsg>");
                xmlCompleto.Append("</soap12:Body>");
                xmlCompleto.Append("</soap12:Envelope>");

                documento.LoadXml(xmlCompleto.ToString());

                assinaturaNfe assina = new assinaturaNfe();

                X509Certificate2Collection colecaoCertificado = assina.carregaCertificados();
                if (colecaoCertificado.Count > 0)
                {
                    dadosConsultaProcessamentoLote dadosConsulta = new dadosConsultaProcessamentoLote(false, null, null);
                    wsEnviaLoteNfe.ClientCertificates.Add((assina.carregaCertificados())[0]);
                    XmlNode retornoAutorizacao;
                    retornoAutorizacao = wsEnviaLoteNfe.nfeAutorizacaoLote(documento.DocumentElement);
                    dadosConsulta.statusEnvio = verificaMensagemRetornoRecepcao(retornoAutorizacao);

                    Timer delay = new Timer();
                    delay.Interval = 5000;
                    //delay.Start();

                    dadosConsulta.nRec = "";
                    dadosConsulta.tpAmb = "2";

                    return dadosConsulta;
                }
                else
                {
                    mensagemAlerta("Certificado não localizado.");
                }
            }

            //Gera arquivo de análise dos wsdl para identificar os métodos.
            //regrasNfe.geraXmlSoap(Path.GetFullPath("webserviceAutorizacaoLoteHomo") + "\\NfeAutorizacao.wsdl", caminho + "\\" + chaveAcessoGeradaNfe + "-env-lot.xml");

            return new dadosConsultaProcessamentoLote(false, null, null);
        }
    }
    #endregion

    #region Classe de Assinatura
    public class assinaturaNfe
    {
        public X509Certificate2Collection carregaCertificados()
        {
            // Pega todos os certificados do usuario.
            X509Store store = new X509Store(StoreLocation.CurrentUser);
            try
            {
                store.Open(OpenFlags.ReadOnly);
                X509Certificate2Collection colecaoCertificados = store.Certificates;
                // verifica a data de validade do cetificado.
                X509Certificate2Collection colecaoCertificadosValidados = colecaoCertificados.Find(X509FindType.FindByTimeValid, DateTime.Now, true);
                return colecaoCertificadosValidados;
            }
            finally
            {
                store.Close();
            }
        }

        /// <summary>
        /// Assina digitalmente os arquivos XML para operações relacionadas a NF-e. Ex: Recepção e cancelamento.
        /// </summary>
        /// <param name="arqXMLAssinar">Caminho do arquivo que deseja assinar</param>
        /// <param name="tagAssinatura">Nome da tag de assinatura do XML</param>
        /// <param name="tagAtributoId">Tag que possui o atributo ID do documento XML</param>
        public void assinarNfe(string arqXMLAssinar, string tagAssinatura, string tagAtributoId, string chaveAcesso)
        {
            StreamReader SR = null;
            assinaturaNfe assinatura = new assinaturaNfe();
            X509Certificate2Collection colecao = assinatura.carregaCertificados();
            if (colecao.Count > 0)
            {
                X509Certificate2 x509Cert = colecao[0];
                try
                {
                    //Abrir o arquivo XML a ser assinado e ler o seu conteúdo
                    SR = File.OpenText(arqXMLAssinar);
                    string xmlString = SR.ReadToEnd();
                    SR.Close();
                    SR = null;

                    // Create a new XML document.
                    XmlDocument doc = new XmlDocument();

                    // Format the document to ignore white spaces.
                    doc.PreserveWhitespace = false;

                    // Load the passed XML file using it’s name.
                    doc.LoadXml(xmlString);

                    //if (doc.GetElementsByTagName(tagAssinatura).Count == 0)
                    //{
                    //    throw new Exception("A tag de assinatura " + tagAssinatura.Trim() + " não existe no XML.");
                    //}
                    //else if (doc.GetElementsByTagName(tagAtributoId).Count == 0)
                    //{
                    //    throw new Exception("A tag de assinatura " + tagAtributoId.Trim() + " não existe no XML.");
                    //}
                    //// Existe mais de uma tag a ser assinada
                    //else
                    //{
                    XmlDocument XMLDoc;


                    XmlNodeList lists = doc.GetElementsByTagName(tagAtributoId);


                    foreach (XmlNode nodes in lists)
                    {
                        foreach (XmlNode childNodes in nodes.ChildNodes)
                        {
                            if (!nodes.Name.Equals(tagAtributoId))
                                continue;

                            // Create a reference to be signed
                            Reference reference = new Reference();
                            reference.Uri = "#" + chaveAcesso;

                            // pega o uri que deve ser assinada                                       
                            XmlElement childElemen = (XmlElement)childNodes;
                            if (childElemen.GetAttributeNode("Id") != null)
                            {
                                reference.Uri = "#" + childElemen.GetAttributeNode("Id").Value;
                            }
                            else if (childElemen.GetAttributeNode("id") != null)
                            {
                                reference.Uri = "#" + childElemen.GetAttributeNode("id").Value;
                            }

                            // Create a SignedXml object.
                            SignedXml signedXml = new SignedXml(doc);

                            // Add the key to the SignedXml document
                            signedXml.SigningKey = x509Cert.PrivateKey;

                            // Add an enveloped transformation to the reference.
                            XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
                            reference.AddTransform(env);

                            XmlDsigC14NTransform c14 = new XmlDsigC14NTransform();
                            reference.AddTransform(c14);

                            // Add the reference to the SignedXml object.
                            signedXml.AddReference(reference);

                            // Create a new KeyInfo object
                            KeyInfo keyInfo = new KeyInfo();

                            // Load the certificate into a KeyInfoX509Data object
                            // and add it to the KeyInfo object.
                            keyInfo.AddClause(new KeyInfoX509Data(x509Cert));

                            // Add the KeyInfo object to the SignedXml object.
                            signedXml.KeyInfo = keyInfo;
                            signedXml.ComputeSignature();

                            // Get the XML representation of the signature and save
                            // it to an XmlElement object.
                            XmlElement xmlDigitalSignature = signedXml.GetXml();

                            // Gravar o elemento no documento XML
                            nodes.AppendChild(doc.ImportNode(xmlDigitalSignature, true));
                            break;
                        }
                    }

                    XMLDoc = new XmlDocument();
                    XMLDoc.PreserveWhitespace = false;
                    XMLDoc = doc;

                    // Atualizar a string do XML já assinada
                    string StringXMLAssinado = XMLDoc.OuterXml;

                    // Gravar o XML Assinad no HD
                    StreamWriter SW_2 = File.CreateText(arqXMLAssinar);
                    SW_2.Write(StringXMLAssinado);
                    SW_2.Close();
                    //}
                }
                catch (System.Security.Cryptography.CryptographicException ex)
                {
                    throw new Exception("Verifique se o Certificado Digital está diponível neste computador ou na rede." + ex.ToString());// #12342 concatenar com a mensagem original
                }
                finally
                {
                    if (SR != null)
                        SR.Close();
                }
            }
        }
    }
    #endregion
}