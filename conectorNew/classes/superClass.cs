using System;
using System.Globalization;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using conectorBema; 20052015
//using conectorECFBema;
using System.IO;
using System.IO.Ports;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
//#######################Modulo Web/Net
using System.Net;
using System.Web;
using System.ServiceModel;
using System.ServiceModel.Web;

//#######################End Modulo Web/Net

namespace conectorPDV001.MG001
{
    class superClass
    {
        public superClass(string find)
        {
            store=find;
        }
        private conector_full_variable alwaysVariables = new conector_full_variable();
        private int i, countField, countRows; //variavel do loop.
        private int auxConsistencia = 0;
        private string store = "0";
        private processamento imports = new processamento();
        private super_dados exports = new super_dados();

        #region // Metodos Web
        //############################################WEB - Import Método da API#####################################################
        [DllImport("wininet.dll")]
        private extern static Boolean InternetGetConnectedState(out int Description, int ReservedValue);
        // Um método que verifica se esta conectado
        public static Boolean IsConnected()
        {
            int Description;
            return InternetGetConnectedState(out Description, 0);
        }
        //############################################END WEB Import Método da API###################################################
        public static string PingHost(string args)//RECEBE COMO PARAMENTRO A URL SERVICE
        {
            HttpWebResponse res = null;

            try
            {
                // Create a request to the passed URI.  
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(args);
                req.Credentials = CredentialCache.DefaultNetworkCredentials;

                // Get the response object.  
                res = (HttpWebResponse)req.GetResponse();

                return "Service Up";
            }
            catch (Exception e)
            {
                msgInfo msg = new msgInfo("Source : " + e.Source + " Exception Source \n" + "Message : " + e.Message + " Exception Message \n" + " --->  Host Unavailable  <--- ");
                if (e.Message.ToString() == "Impossível conectar-se ao servidor remoto")
                {
                    return "Host Unavailable";
                }
                //MessageBox.Show("Source : " + e.Source, "Exception Source", MessageBoxButtons.OK);
                //MessageBox.Show("Message : " + e.Message, "Exception Message", MessageBoxButtons.OK);
                return "Host Unavailable";
            }
        }
        //#############################################################Metodos De Controle Webservice########################################
        #endregion

        #region // Autentica CepBairro

        protected void conector_autentica_cepBairro(string cep)
        {
            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                imports.singleTransaction("update cepBairro set importCarga=1 where importCarga=0 and idCepBairro=?cep;");
                imports.addParametro("?cep", cep);
                imports.procedimentoRead();
                if (imports.retornaRead().Read() == true)
                {
                }

            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                }
                imports.fechaConexao();
            }
        }
        public void conector_autentica_cep()
        {
            string[,] autentic; //Matriz Bidimencionada
            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                imports.singleTransaction("select * from cepBairro where importCarga in(0)");
                imports.procedimentoSet();
            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = imports.retornaSet().Tables[0].Columns.Count;
                    countRows = imports.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        autentic = new string[countRows, countField];
                        for (int i = 0; i < countRows; i++)//Linha
                        {
                            for (int j = 0; j < countField; j++) //Coluna
                            {
                                autentic[i, j] = Convert.ToString(imports.retornaSet().Tables[0].Rows[i][j]);
                            }
                        }
                        imports.fechaConexao();
                        for (int i = 0; i < autentic.GetLength(0); i++)//Linha
                        {
                            if (conectorWEB_replace_cepbairro(autentic[i, 0], autentic[i, 1], autentic[i, 2], autentic[i, 3], autentic[i, 4], autentic[i, 5], autentic[i, 6], autentic[i, 7]) == 1)
                            {
                                conector_autentica_cepBairro(autentic[i, 0]);
                            }
                        }
                    }
                }
                imports.fechaConexao();
            }
        }
        protected int conectorWEB_replace_cepbairro(string replace_idcepbairro,
           string replace_cep,
           string replace_idcepCity,
           string replace_idestado,
           string replace_bairro,
           string replace_logradouro,
           string replace_complemento,
           string replace_uf)
        {
            int aux = 0;
            auxConsistencia = 0;
            try
            {
                exports.abreConexao();
                exports.iniciarTransacao();
                exports.startTransaction("conectorWEB_replace_cepbairro");
                exports.addParametro("replace_idcepbairro", replace_idcepbairro);
                exports.addParametro("replace_cep", replace_cep);
                exports.addParametro("replace_idcepCity", replace_idcepCity);
                exports.addParametro("replace_idestado", replace_idestado);
                exports.addParametro("replace_bairro", replace_bairro);
                exports.addParametro("replace_logradouro", replace_logradouro);
                exports.addParametro("replace_complemento", replace_complemento);
                exports.addParametro("replace_uf", replace_uf);
                exports.procedimentoRead();
                if (exports.retornaRead().Read() == true)
                {
                    aux = 1;
                }
                exports.fechaRead();
                exports.commit();
            }
            catch (Exception erro)
            {
                exports.rollback();
                auxConsistencia = 1;
            }
            finally
            {
                exports.fechaConexao();
                if (auxConsistencia == 0)
                {
                }
            }
            return aux;
        }
        #endregion
    
        #region //Autenticação Produto Estoques

        protected void conector_autentica_produtoExtrato(string carga, string store)
        {
            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                imports.singleTransaction("update produtoExtrato set importCarga=1 where idProdutoExtrato=?carga and importCarga=0 and idLoja=?store");
                imports.addParametro("?carga", carga);
                imports.addParametro("?store", store);
                imports.procedimentoRead();
                if (imports.retornaRead().Read() == true)
                {
                }

            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                }
                imports.fechaConexao();
            }
        }
        public void conector_autentica_extrato()
        {
            string[,] autentic; //Matriz Bidimencionada
            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                imports.singleTransaction("select * from produtoExtrato where importCarga in(0) and idLoja=?store");
                imports.addParametro("?store", store);
                imports.procedimentoSet();
            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = imports.retornaSet().Tables[0].Columns.Count;
                    countRows = imports.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        autentic = new string[countRows, countField];
                        for (int i = 0; i < countRows; i++)//Linha
                        {
                            for (int j = 0; j < countField; j++) //Coluna
                            {
                                autentic[i, j] = Convert.ToString(imports.retornaSet().Tables[0].Rows[i][j]);
                            }
                        }
                        imports.fechaConexao();
                        for (int i = 0; i < autentic.GetLength(0); i++)//Linha
                        {
                            if (conectorWEB_replace_produtoExtrato(autentic[i, 0], autentic[i, 1], autentic[i, 2], autentic[i, 3], autentic[i, 4], autentic[i, 5], autentic[i, 6], autentic[i, 7], autentic[i, 8], autentic[i, 9], autentic[i, 10], autentic[i, 11], autentic[i, 12], autentic[i, 13], autentic[i, 14], autentic[i, 15]) == 1)
                            {
                                conector_autentica_produtoExtrato(autentic[i, 0], autentic[i, 2]);
                            }
                        }
                    }
                }
                imports.fechaConexao();
            }
        }

        protected int conectorWEB_replace_produtoExtrato(string replace_idprodutoExtrato,
              string replace_idProduto,
              string replace_idloja,
              string replace_historico,
              string replace_typeAritimetico,
              string replace_quantidade,
              string replace_saldo,
              string replace_cupom,
              string replace_pdv,
              string replace_custoLiquido,
              string replace_custoMedio,
              string replace_priceVenda,
              string replace_margem,
              string replace_idtypeMovimentacao,
              string replace_movimento,
              string replace_origem)
        {
            int aux = 0;
            auxConsistencia = 0;
            try
            {
                exports.abreConexao();
                exports.iniciarTransacao();
                exports.startTransaction("conectorWEB_replace_produtoExtrato");
                exports.addParametro("replace_idprodutoExtrato", replace_idprodutoExtrato);
                exports.addParametro("replace_idProduto", replace_idProduto);
                exports.addParametro("replace_idloja", replace_idloja);
                exports.addParametro("replace_historico", replace_historico);
                exports.addParametro("replace_typeAritimetico", replace_typeAritimetico);
                exports.addParametro("replace_quantidade", replace_quantidade.Replace(",", "."));
                exports.addParametro("replace_saldo", replace_saldo.Replace(",", "."));
                exports.addParametro("replace_cupom", replace_cupom);
                exports.addParametro("replace_pdv", replace_pdv);
                exports.addParametro("replace_custoLiquido", replace_custoLiquido.Replace(",", "."));
                exports.addParametro("replace_custoMedio", replace_custoMedio.Replace(",", "."));
                exports.addParametro("replace_priceVenda", replace_priceVenda.Replace(",", "."));
                exports.addParametro("replace_margem", replace_margem.Replace(",", "."));
                exports.addParametro("replace_idtypeMovimentacao", replace_idtypeMovimentacao);
                exports.addParametro("replace_movimento", String.Format("{0:yyyyMMdd}", Convert.ToDateTime(replace_movimento)));
                exports.addParametro("replace_origem", replace_origem); 
                exports.addParametro("replace_importCarga", "0");
                exports.procedimentoRead();
                if (exports.retornaRead().Read() == true)
                {
                    aux = 1;
                }
                exports.fechaRead();
                exports.commit();
            }
            catch (Exception erro)
            {
                exports.rollback();
                auxConsistencia = 1;
            }
            finally
            {
                exports.fechaConexao();
                if (auxConsistencia == 0)
                {
                }
            }
            return aux;
        }



        protected void conector_autentica_produtoMovimento(string carga, string store)
        {
            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                imports.singleTransaction("update produtoMovimento set importCarga=1 where idExtrato=?carga and importCarga=0 and idLoja=?store");
                imports.addParametro("?carga", carga);
                imports.addParametro("?store", store);
                imports.procedimentoRead();
                if (imports.retornaRead().Read() == true)
                {
                }

            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                }
                imports.fechaConexao();
            }
        }
        public void conector_autentica_movimento()
        {
            string[,] autentic; //Matriz Bidimencionada
            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                imports.singleTransaction("select * from produtoMovimento where importCarga in(0) and idLoja=?store");
                imports.addParametro("?store", store);
                imports.procedimentoSet();
            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = imports.retornaSet().Tables[0].Columns.Count;
                    countRows = imports.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        autentic = new string[countRows, countField];
                        for (int i = 0; i < countRows; i++)//Linha
                        {
                            for (int j = 0; j < countField; j++) //Coluna
                            {
                                autentic[i, j] = Convert.ToString(imports.retornaSet().Tables[0].Rows[i][j]);
                            }
                        }
                        imports.fechaConexao();
                        for (int i = 0; i < autentic.GetLength(0); i++)//Linha
                        {
                            if (conectorWEB_replace_produtoMovimento(autentic[i, 0], autentic[i, 1], autentic[i, 2], autentic[i, 3], autentic[i, 4], autentic[i, 5], autentic[i, 6], autentic[i, 7], autentic[i, 8], autentic[i, 9], autentic[i, 10], autentic[i, 11], autentic[i, 12], autentic[i, 13], autentic[i, 14], autentic[i, 15], autentic[i, 16], autentic[i, 17], autentic[i, 18], autentic[i, 19], autentic[i, 20], autentic[i, 21], autentic[i, 22], autentic[i, 23],
                                 autentic[i, 24], autentic[i, 25], autentic[i, 26], autentic[i, 27], autentic[i, 28], autentic[i, 29], autentic[i, 30], autentic[i, 31], autentic[i, 32], autentic[i, 33], autentic[i, 34], autentic[i, 35], autentic[i, 36], autentic[i, 37], autentic[i,38], autentic[i, 39], autentic[i, 40], autentic[i, 41], autentic[i, 42], autentic[i, 43], autentic[i, 44], autentic[i, 45], autentic[i, 46], autentic[i, 47], autentic[i, 48], autentic[i, 49], autentic[i, 50]) == 1)
                            {
                                conector_autentica_produtoMovimento(autentic[i, 0], autentic[i, 2]);
                            }
                        }
                    }
                }
                imports.fechaConexao();
            }
        }

        protected int conectorWEB_replace_produtoMovimento(string replace_idExtrato,
                                            string replace_idProduto,
                                            string replace_idloja,
                                            string replace_idterminal,
                                            string replace_data,
                                            string replace_sequencia,
                                            string replace_cupom,
                                            string replace_pessoa,
                                            string replace_custoLiquido,
                                            string replace_fornecedor,
                                            string replace_setor,
                                            string replace_grupo,
                                            string replace_categoria,
                                            string replace_margem,
                                            string replace_lucro,
                                            string replace_precoVenda,
                                            string replace_precoCusto,
                                            string replace_valorTotal,
                                            string replace_icms,
                                            string replace_cofins,
                                            string replace_pis,
                                            string replace_aliquota,
                                            string replace_reducao,
                                            string replace_creditoPis,
                                            string replace_creditoCofins,
                                            string replace_promocao,
                                            string replace_time,
                                            string replace_discount,
                                            string replace_vendedor,
                                            string replace_metodo,
                                            string replace_historico,
                                            string replace_qttyCancel,
                                            string replace_precoOriginal,
                                            string replace_finalizadora,
                                            string replace_quantidade,
                                            string replace_custoOperacional,
                                            string replace_custoReposicao,
                                            string replace_tributacao,
                                            string replace_tipoProcesso,
                                            string replace_custoTransferencia,
                                            string replace_pedido,
                                            string replace_tipoPromocao,
                                            string replace_nota,
                                            string replace_serie,
                                            string replace_modelo,
                                            string replace_situacao,
                                            string replace_aliquotaFim,
                                            string replace_tipoAliquota, string replace_idProdutoEmbalagem, string replace_barra, string replace_idUnidadeMedida)
        {
            int aux = 0;
            auxConsistencia = 0;
            try
            {
                exports.abreConexao();
                exports.iniciarTransacao();
                exports.startTransaction("conectorWEB_replace_produtoMovimento");
                exports.addParametro("replace_idExtrato", replace_idExtrato);
                exports.addParametro("replace_idProduto", replace_idProduto);
                exports.addParametro("replace_idloja", replace_idloja);
                exports.addParametro("replace_idterminal", replace_idterminal);
                exports.addParametro("replace_data", String.Format("{0:yyyyMMdd}", Convert.ToDateTime(replace_data)));
                exports.addParametro("replace_sequencia", replace_sequencia);
                exports.addParametro("replace_cupom", replace_cupom);
                exports.addParametro("replace_pessoa", replace_pessoa);
                exports.addParametro("replace_custoLiquido", replace_custoLiquido.Replace(",", "."));
                exports.addParametro("replace_fornecedor", replace_fornecedor);
                exports.addParametro("replace_setor", replace_setor);
                exports.addParametro("replace_grupo", replace_grupo);
                exports.addParametro("replace_categoria", replace_categoria);
                exports.addParametro("replace_margem", replace_margem.Replace(",", "."));
                exports.addParametro("replace_lucro", replace_lucro.Replace(",", "."));
                exports.addParametro("replace_precoVenda", replace_precoVenda.Replace(",", "."));
                exports.addParametro("replace_precoCusto", replace_precoCusto.Replace(",", "."));
                exports.addParametro("replace_valorTotal", replace_valorTotal.Replace(",", "."));
                exports.addParametro("replace_icms", replace_icms.Replace(",", "."));
                exports.addParametro("replace_cofins", replace_cofins.Replace(",", "."));
                exports.addParametro("replace_pis", replace_pis.Replace(",", "."));
                exports.addParametro("replace_aliquota", replace_aliquota);
                exports.addParametro("replace_reducao", replace_reducao.Replace(",", "."));
                exports.addParametro("replace_creditoPis", replace_creditoPis.Replace(",", "."));
                exports.addParametro("replace_creditoCofins", replace_creditoCofins.Replace(",", "."));
                exports.addParametro("replace_promocao", replace_promocao.Replace(",", "."));
                exports.addParametro("replace_time", replace_time);
                exports.addParametro("replace_discount", replace_discount.Replace(",", "."));
                exports.addParametro("replace_vendedor", replace_vendedor);
                exports.addParametro("replace_metodo", replace_metodo);
                exports.addParametro("replace_historico", replace_historico);
                exports.addParametro("replace_qttyCancel", replace_qttyCancel.Replace(",", "."));
                exports.addParametro("replace_precoOriginal", replace_precoOriginal.Replace(",", "."));
                exports.addParametro("replace_finalizadora", replace_finalizadora);
                exports.addParametro("replace_quantidade", replace_quantidade.Replace(",", "."));
                exports.addParametro("replace_custoOperacional", replace_custoOperacional.Replace(",", "."));
                exports.addParametro("replace_custoReposicao", replace_custoReposicao.Replace(",", "."));
                exports.addParametro("replace_tributacao", replace_tributacao);
                exports.addParametro("replace_tipoProcesso", replace_tipoProcesso);
                exports.addParametro("replace_custoTransferencia", replace_custoTransferencia.Replace(",", "."));
                exports.addParametro("replace_pedido", replace_pedido);
                exports.addParametro("replace_tipoPromocao", replace_tipoPromocao);
                exports.addParametro("replace_nota", replace_nota);
                exports.addParametro("replace_serie", replace_serie);
                exports.addParametro("replace_modelo", replace_modelo);
                exports.addParametro("replace_situacao", replace_situacao);
                exports.addParametro("replace_aliquotaFim", replace_aliquotaFim.Replace(",", "."));
                exports.addParametro("replace_tipoAliquota", replace_tipoAliquota);
                exports.addParametro("replace_idProdutoEmbalagem", replace_idProdutoEmbalagem);
                exports.addParametro("replace_barra", replace_barra);
                exports.addParametro("replace_idUnidadeMedida", replace_idUnidadeMedida);
                exports.procedimentoRead();
                if (exports.retornaRead().Read() == true)
                {
                    aux = 1;
                }
                exports.fechaRead();
                exports.commit();
            }
            catch (Exception erro)
            {
                exports.rollback();
                auxConsistencia = 1;
            }
            finally
            {
                exports.fechaConexao();
                if (auxConsistencia == 0)
                {
                }
            }
            return aux;
        }
        #endregion

        #region //Autenticação Money
        protected int conectorWeb_replace_money(string replace_idpedido,
            string replace_idloja,
            string replace_idfinalizadora,
            string replace_idcliente,
            string replace_idmetodo,
            string replace_idfuncionario,
            string replace_emissao,
            string replace_valorTotal,
            string replace_terminal,
            string replace_observacao,
            string replace_faturado,
            string replace_transacao,
            string replace_cupom,
            string replace_numeroNota,
            string replace_serie,
            string replace_situacao)
        {
            int aux = 0;
            auxConsistencia = 0;
            try
            {
                exports.abreConexao();
                exports.iniciarTransacao();
                exports.startTransaction("conectorWeb_replace_money");
                exports.addParametro("replace_idpedido", replace_idpedido);
                exports.addParametro("replace_idloja", replace_idloja);
                exports.addParametro("replace_idfinalizadora", replace_idfinalizadora);
                exports.addParametro("replace_idcliente", replace_idcliente == "" ? "0" : replace_idcliente);
                exports.addParametro("replace_idmetodo", replace_idmetodo);
                exports.addParametro("replace_idfuncionario", replace_idfuncionario);
                exports.addParametro("replace_emissao", String.Format("{0:yyyyMMdd}",  Convert.ToDateTime(replace_emissao)));
                exports.addParametro("replace_valorTotal", replace_valorTotal.Replace(",", "."));
                exports.addParametro("replace_terminal", replace_terminal);
                exports.addParametro("replace_observacao", replace_observacao);
                exports.addParametro("replace_faturado", replace_faturado.Replace(",", "."));
                exports.addParametro("replace_transacao", replace_transacao);
                exports.addParametro("replace_cupom", replace_cupom);
                exports.addParametro("replace_numeroNota", replace_numeroNota);
                exports.addParametro("replace_serie", replace_serie);
                exports.addParametro("replace_situacao", replace_situacao);
                exports.procedimentoRead();
                if (exports.retornaRead().Read() == true)
                {
                    aux = 1;
                }
                exports.fechaRead();
                exports.commit();
            }
            catch (Exception erro)
            {
                exports.rollback();
                auxConsistencia = 1;
            }
            finally
            {
                exports.fechaConexao();
                if (auxConsistencia == 0)
                {
                }
            }
            return aux;
        }
        public void conector_autentica()
        {
            string[,] autentic; //Matriz Bidimencionada
            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                imports.singleTransaction("select * from money where importCarga in(0) and idLoja=?store");
                imports.addParametro("?store", store);
                imports.procedimentoSet();
            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = imports.retornaSet().Tables[0].Columns.Count;
                    countRows = imports.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        autentic = new string[countRows, countField];
                        for (int i = 0; i < countRows; i++)//Linha
                        {
                            for (int j = 0; j < countField; j++) //Coluna
                            {
                                autentic[i, j] = Convert.ToString(imports.retornaSet().Tables[0].Rows[i][j]);
                            }
                        }
                        imports.fechaConexao();
                        for (int i = 0; i < autentic.GetLength(0); i++)//Linha
                        {
                            if(conectorWeb_replace_money(autentic[i, 0], autentic[i, 1], autentic[i, 2], autentic[i, 3], autentic[i, 4], autentic[i, 5], autentic[i, 6], autentic[i, 7], autentic[i, 8], autentic[i, 9], autentic[i, 10], autentic[i, 11], autentic[i, 12], autentic[i, 13], autentic[i, 14], autentic[i, 15]) == 1)
                            {
                                conector_autentica_baixa(autentic[i, 0], autentic[i, 1], autentic[i, 11]);
                            }
                        }
                    }
                }
                imports.fechaConexao();
            }
        }

        protected void conector_autentica_baixa(string carga, string store, string transa)
        {
            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                imports.singleTransaction("update money set importCarga=1 where idpedido=?carga and importCarga=0 and idLoja=?store and transacao=?transa");
                imports.addParametro("?carga", carga);
                imports.addParametro("?store", store);
                imports.addParametro("?transa", transa);
                imports.procedimentoRead();
                if (imports.retornaRead().Read() == true)
                {
                }

            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                }
                imports.fechaConexao();
            }
        }

#endregion

        #region //Transmissao Reserva
        public void conector_autentica_pedido(string chave, string store)
        {
            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                imports.singleTransaction("update pedido set importCarga=1 where idpedido=?chave and importCarga=0 and idLoja=?store");
                imports.addParametro("?chave", chave);
                imports.addParametro("?store", store);
                imports.procedimentoRead();
                if (imports.retornaRead().Read() == true)
                {
                }

            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                }
                imports.fechaConexao();
            }
        }
        public int conector_pedido()
        {
            string[,] vetor = new string[0, 0];
            string[,] recarga = new string[0,0]; //Reserva
            string[,] recarga01 = new string[0, 0]; //PedidoItens
            string[,] recarga02; //PedidoFinanceiro
            string[,] recarga03;
            string[,] recarga04;
            string[,] recarga05;
            string[,] recarga06;
            string[,] recarga07;
            bool valida = false;
            //Pedido
            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                imports.singleTransaction("SELECT * FROM pedido where idLoja=?store and importCarga=0");
                imports.addParametro("?store", store);
                imports.procedimentoSet();

            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = imports.retornaSet().Tables[0].Columns.Count;
                    countRows = imports.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        vetor = new string[countRows, 3];
                        recarga = new string[countRows, countField];
                        for (int i = 0; i < countRows; i++)//Linha
                        {
                            for (int j = 0; j < countField; j++) //Coluna
                            {
                                recarga[i, j] = Convert.ToString(imports.retornaSet().Tables[0].Rows[i][j]);
                            }
                        }
                        imports.fechaConexao();
                        for (int i = 0; i < recarga.GetLength(0); i++)//Linha
                        {
                            if (conectorWEB_replace_reserva(recarga[i, 0], recarga[i, 1], recarga[i, 2], recarga[i, 3], recarga[i, 4], recarga[i, 5], recarga[i, 6], recarga[i, 7], recarga[i, 8], recarga[i, 9], recarga[i, 10], recarga[i, 11], recarga[i, 12], recarga[i, 13], recarga[i, 14], recarga[i, 15], recarga[i, 16], recarga[i, 17], recarga[i, 18], recarga[i, 19], recarga[i, 20], recarga[i, 21], recarga[i, 22], recarga[i, 23], recarga[i, 24], recarga[i, 25], recarga[i, 26], recarga[i, 27], recarga[i, 28], recarga[i, 29], recarga[i, 30]) == true)
                            {
                                vetor[i, 0] = recarga[i, 0];
                                vetor[i, 1] = recarga[i, 1];
                                vetor[i, 2] = "#";
                            }
                        }
                    }
                }

                imports.fechaConexao();
            }
            //PedidoItens
            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                imports.singleTransaction("SELECT tab1.* FROM pedido tab inner join pedidoItens  tab1 on(tab.idLoja=tab1.idLoja and tab.idPedido = tab1.idPedido) where tab.idLoja=?store and importCarga=0");
                imports.addParametro("?store", store);
                imports.procedimentoSet();

            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = imports.retornaSet().Tables[0].Columns.Count;
                    countRows = imports.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        recarga01 = new string[countRows, countField];
                        for (int i = 0; i < countRows; i++)//Linha
                        {
                            for (int j = 0; j < countField; j++) //Coluna
                            {
                                recarga01[i, j] = Convert.ToString(imports.retornaSet().Tables[0].Rows[i][j]);
                            }
                        }
                        imports.fechaConexao();
                        for (int i = 0; i < recarga01.GetLength(0); i++)//Linha
                        {
                            if(conectorWEB_replace_pedidoItens(recarga01[i, 0], recarga01[i, 1], recarga01[i, 2], recarga01[i, 3], recarga01[i, 4], recarga01[i, 5], recarga01[i, 6], recarga01[i, 7], recarga01[i, 8], recarga01[i, 9], recarga01[i, 10], recarga01[i, 11],recarga01[i, 12],recarga01[i, 13],recarga01[i, 14]) == true)
                            {
                                for (int j = 0; j < vetor.GetLength(0); j++)
                                {
                                    if (vetor[j, 0] == recarga01[i, 0])
                                    {
                                        vetor[j, 2] = "*";
                                        break;
                                    }   
                                }
                            }
                        }
                    }
                }
                imports.fechaConexao();
            }

            //PedidoFinanceiro
            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                imports.singleTransaction("SELECT tab1.* FROM pedido tab inner join pedidofinanceiro  tab1 on(tab.idLoja=tab1.idLoja and tab.idPedido = tab1.idPedido) where tab.idLoja=?store and importCarga=0");
                imports.addParametro("?store", store);
                imports.procedimentoSet();

            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = imports.retornaSet().Tables[0].Columns.Count;
                    countRows = imports.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        recarga02 = new string[countRows, countField];
                        for (int i = 0; i < countRows; i++)//Linha
                        {
                            for (int j = 0; j < countField; j++) //Coluna
                            {
                                recarga02[i, j] = Convert.ToString(imports.retornaSet().Tables[0].Rows[i][j]);
                            }
                        }
                        imports.fechaConexao();
                        for (int i = 0; i < recarga02.GetLength(0); i++)//Linha
                        {
                            conectorWEB_replace_pedidoFinanceiro(recarga02[i, 0], recarga02[i, 1], recarga02[i, 2], recarga02[i, 3], recarga02[i, 4], recarga02[i, 5], recarga02[i, 6], recarga02[i, 7], recarga02[i, 8], recarga02[i, 9], recarga02[i, 10], recarga02[i, 11]);
                        }
                    }
                }
                imports.fechaConexao();
            }

            //Pedido Crediario
            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                imports.singleTransaction("SELECT tab1.* FROM pedido tab inner join pedidocrediario  tab1 on(tab.idLoja=tab1.idLoja and tab.idPedido = tab1.idPedido) where tab.idLoja=?store and importCarga=0");
                imports.addParametro("?store", store);
                imports.procedimentoSet();

            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = imports.retornaSet().Tables[0].Columns.Count;
                    countRows = imports.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        recarga03 = new string[countRows, countField];
                        for (int i = 0; i < countRows; i++)//Linha
                        {
                            for (int j = 0; j < countField; j++) //Coluna
                            {
                                recarga03[i, j] = Convert.ToString(imports.retornaSet().Tables[0].Rows[i][j]);
                            }
                        }
                        imports.fechaConexao();
                        for (int i = 0; i < recarga03.GetLength(0); i++)//Linha
                        {
                            conectorWEB_replace_pedidoCrediario(recarga03[i, 0], recarga03[i, 1], recarga03[i, 2], recarga03[i, 3], recarga03[i, 4], recarga03[i, 5], recarga03[i, 6], recarga03[i, 7], recarga03[i, 8], recarga03[i, 9], recarga03[i, 10], recarga03[i, 11], recarga03[i, 12]);
                        }
                    }
                }
                imports.fechaConexao();
            }

            //Pedido Cartao
            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                imports.singleTransaction("SELECT tab1.* FROM pedido tab inner join pedidocartao  tab1 on(tab.idLoja=tab1.idLoja and tab.idPedido = tab1.idPedido) where tab.idLoja=?store and importCarga=0");
                imports.addParametro("?store", store);
                imports.procedimentoSet();

            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = imports.retornaSet().Tables[0].Columns.Count;
                    countRows = imports.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        recarga04 = new string[countRows, countField];
                        for (int i = 0; i < countRows; i++)//Linha
                        {
                            for (int j = 0; j < countField; j++) //Coluna
                            {
                                recarga04[i, j] = Convert.ToString(imports.retornaSet().Tables[0].Rows[i][j]);
                            }
                        }
                        imports.fechaConexao();
                        for (int i = 0; i < recarga04.GetLength(0); i++)//Linha
                        {
                            conectorWEB_replace_pedidoCartao(recarga04[i, 0], recarga04[i, 1],recarga04[i, 2],recarga04[i, 3],recarga04[i, 4],recarga04[i, 5],recarga04[i, 6],recarga04[i, 7],recarga04[i, 8]);
                        }
                    }
                }
                imports.fechaConexao();
            }

            //Pedido Cheque
            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                imports.singleTransaction("SELECT tab1.* FROM pedido tab inner join pedidocheque  tab1 on(tab.idLoja=tab1.idLoja and tab.idPedido = tab1.idPedido) where tab.idLoja=?store and importCarga=0");
                imports.addParametro("?store", store);
                imports.procedimentoSet();

            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = imports.retornaSet().Tables[0].Columns.Count;
                    countRows = imports.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        recarga05 = new string[countRows, countField];
                        for (int i = 0; i < countRows; i++)//Linha
                        {
                            for (int j = 0; j < countField; j++) //Coluna
                            {
                                recarga05[i, j] = Convert.ToString(imports.retornaSet().Tables[0].Rows[i][j]);
                            }
                        }
                        imports.fechaConexao();
                        for (int i = 0; i < recarga05.GetLength(0); i++)//Linha
                        {
                            conectorWEB_replace_pedidoCheque(recarga05[i, 0], recarga05[i, 1], recarga05[i, 2], recarga05[i, 3], recarga05[i, 4], recarga05[i, 5], recarga05[i, 6], recarga05[i, 7], recarga05[i, 8],recarga05[i, 9],recarga05[i, 10]);
                        }
                    }
                }
                imports.fechaConexao();
            }
            //Pedido Boleto
            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                imports.singleTransaction("SELECT tab1.* FROM pedido tab inner join  pedidoboleto  tab1 on(tab.idLoja=tab1.cedente and tab.idPedido = tab1.idPedido) where tab.idLoja=?store and importCarga=0");
                imports.addParametro("?store", store);
                imports.procedimentoSet();

            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = imports.retornaSet().Tables[0].Columns.Count;
                    countRows = imports.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        recarga06 = new string[countRows, countField];
                        for (int i = 0; i < countRows; i++)//Linha
                        {
                            for (int j = 0; j < countField; j++) //Coluna
                            {
                                recarga06[i, j] = Convert.ToString(imports.retornaSet().Tables[0].Rows[i][j]);
                            }
                        }
                        imports.fechaConexao();
                        for (int i = 0; i < recarga06.GetLength(0); i++)//Linha
                        {
                            conectorWEB_replace_pedidoBoleto(recarga06[i, 0], recarga06[i, 1], recarga06[i, 2], recarga06[i, 3], recarga06[i, 4], recarga06[i, 5], recarga06[i, 6], recarga06[i, 7], recarga06[i, 8], recarga06[i, 9]);
                        }
                    }
                }
                imports.fechaConexao();
            }
            //Pedido Convenio
            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                imports.singleTransaction("SELECT tab1.* FROM pedido tab inner join pedidoconvenio  tab1 on(tab.idLoja=tab1.idLoja and tab.idPedido = tab1.idPedido) where tab.idLoja=?store and importCarga=0");
                imports.addParametro("?store", store);
                imports.procedimentoSet();

            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = imports.retornaSet().Tables[0].Columns.Count;
                    countRows = imports.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        recarga07 = new string[countRows, countField];
                        for (int i = 0; i < countRows; i++)//Linha
                        {
                            for (int j = 0; j < countField; j++) //Coluna
                            {
                                recarga07[i, j] = Convert.ToString(imports.retornaSet().Tables[0].Rows[i][j]);
                            }
                        }
                        imports.fechaConexao();
                        for (int i = 0; i < recarga07.GetLength(0); i++)//Linha
                        {
                            conectorWEB_replace_pedidoBoleto(recarga07[i, 0], recarga07[i, 1], recarga07[i, 2], recarga07[i, 3], recarga07[i, 4], recarga07[i, 5], recarga07[i, 6], recarga07[i, 7], recarga07[i, 8], recarga07[i, 9]);
                        }
                    }
                }
                imports.fechaConexao();
            }
            //Pedido Entrega
            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                imports.singleTransaction("SELECT tab1.* FROM pedido tab inner join pedidoentrega  tab1 on(tab.idLoja=tab1.idLoja and tab.idPedido = tab1.idPedido) where tab.idLoja=?store and importCarga=0");
                imports.addParametro("?store", store);
                imports.procedimentoSet();

            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = imports.retornaSet().Tables[0].Columns.Count;
                    countRows = imports.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        recarga = new string[countRows, countField];
                        for (int i = 0; i < countRows; i++)//Linha
                        {
                            for (int j = 0; j < countField; j++) //Coluna
                            {
                                recarga[i, j] = Convert.ToString(imports.retornaSet().Tables[0].Rows[i][j]);
                            }
                        }
                        imports.fechaConexao();
                        for (int i = 0; i < recarga.GetLength(0); i++)//Linha
                        {
                            //conector_update_lineMD5_P2(recarga[i, 1], key.GetMd5Sum(export.registro_tipo_p2("P2", recarga[i, 0], recarga[i, 1], recarga[i, 2], recarga[i, 3], recarga[i, 4], recarga[i, 5], recarga[i, 6], recarga[i, 7], recarga[i, 8])));
                        }
                    }
                }
                imports.fechaConexao();
            }

            //Pedido ParcelaBoleto
            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                imports.singleTransaction("SELECT tab1.* FROM pedido tab inner join pedidoparcelaboleto  tab1 on(tab.idLoja=tab1.cedente and tab.idPedido = tab1.idPedido) where tab.idLoja=?store and importCarga=0");
                imports.addParametro("?store", store);
                imports.procedimentoSet();

            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = imports.retornaSet().Tables[0].Columns.Count;
                    countRows = imports.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        recarga = new string[countRows, countField];
                        for (int i = 0; i < countRows; i++)//Linha
                        {
                            for (int j = 0; j < countField; j++) //Coluna
                            {
                                recarga[i, j] = Convert.ToString(imports.retornaSet().Tables[0].Rows[i][j]);
                            }
                        }
                        imports.fechaConexao();
                        for (int i = 0; i < recarga.GetLength(0); i++)//Linha
                        {
                            //conector_update_lineMD5_P2(recarga[i, 1], key.GetMd5Sum(export.registro_tipo_p2("P2", recarga[i, 0], recarga[i, 1], recarga[i, 2], recarga[i, 3], recarga[i, 4], recarga[i, 5], recarga[i, 6], recarga[i, 7], recarga[i, 8])));
                        }
                    }
                }
                imports.fechaConexao();
            }

            //Pedido Parcela cartao
            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                imports.singleTransaction("SELECT tab1.* FROM pedido tab inner join pedidoparcelacartao  tab1 on(tab.idLoja=tab1.idLoja and tab.idPedido = tab1.idPedido) where tab.idLoja=?store and importCarga=0");
                imports.addParametro("?store", store);
                imports.procedimentoSet();

            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = imports.retornaSet().Tables[0].Columns.Count;
                    countRows = imports.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        recarga = new string[countRows, countField];
                        for (int i = 0; i < countRows; i++)//Linha
                        {
                            for (int j = 0; j < countField; j++) //Coluna
                            {
                                recarga[i, j] = Convert.ToString(imports.retornaSet().Tables[0].Rows[i][j]);
                            }
                        }
                        imports.fechaConexao();
                        for (int i = 0; i < recarga.GetLength(0); i++)//Linha
                        {
                            //conector_update_lineMD5_P2(recarga[i, 1], key.GetMd5Sum(export.registro_tipo_p2("P2", recarga[i, 0], recarga[i, 1], recarga[i, 2], recarga[i, 3], recarga[i, 4], recarga[i, 5], recarga[i, 6], recarga[i, 7], recarga[i, 8])));
                        }
                    }
                }
                imports.fechaConexao();
            }

            //Pedido Parcela crediario
            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                imports.singleTransaction("SELECT tab1.* FROM pedido tab inner join pedidoparcelacrediario  tab1 on(tab.idLoja=tab1.idLoja and tab.idPedido = tab1.idPedido) where tab.idLoja=?store and importCarga=0");
                imports.addParametro("?store", store);
                imports.procedimentoSet();

            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = imports.retornaSet().Tables[0].Columns.Count;
                    countRows = imports.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        recarga = new string[countRows, countField];
                        for (int i = 0; i < countRows; i++)//Linha
                        {
                            for (int j = 0; j < countField; j++) //Coluna
                            {
                                recarga[i, j] = Convert.ToString(imports.retornaSet().Tables[0].Rows[i][j]);
                            }
                        }
                        imports.fechaConexao();
                        for (int i = 0; i < recarga.GetLength(0); i++)//Linha
                        {
                            //conector_update_lineMD5_P2(recarga[i, 1], key.GetMd5Sum(export.registro_tipo_p2("P2", recarga[i, 0], recarga[i, 1], recarga[i, 2], recarga[i, 3], recarga[i, 4], recarga[i, 5], recarga[i, 6], recarga[i, 7], recarga[i, 8])));
                        }
                    }
                }
                imports.fechaConexao();
            }

            //Pedido Parcela cheque
            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                imports.singleTransaction("SELECT tab1.* FROM pedido tab inner join pedidoparcelacheque  tab1 on(tab.idLoja=tab1.idLoja and tab.idPedido = tab1.idPedido) where tab.idLoja=?store and importCarga=0");
                imports.addParametro("?store", store);
                imports.procedimentoSet();

            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = imports.retornaSet().Tables[0].Columns.Count;
                    countRows = imports.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        recarga = new string[countRows, countField];
                        for (int i = 0; i < countRows; i++)//Linha
                        {
                            for (int j = 0; j < countField; j++) //Coluna
                            {
                                recarga[i, j] = Convert.ToString(imports.retornaSet().Tables[0].Rows[i][j]);
                            }
                        }
                        imports.fechaConexao();
                        for (int i = 0; i < recarga.GetLength(0); i++)//Linha
                        {
                            //conector_update_lineMD5_P2(recarga[i, 1], key.GetMd5Sum(export.registro_tipo_p2("P2", recarga[i, 0], recarga[i, 1], recarga[i, 2], recarga[i, 3], recarga[i, 4], recarga[i, 5], recarga[i, 6], recarga[i, 7], recarga[i, 8])));
                        }
                    }
                }
                imports.fechaConexao();
            }
            //Pedido Parcela convenio
            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                imports.singleTransaction("SELECT tab1.* FROM pedido tab inner join pedidoparcelaconvenio  tab1 on(tab.idLoja=tab1.idLoja and tab.idPedido = tab1.idPedido) where tab.idLoja=?store and importCarga=0");
                imports.addParametro("?store", store);
                imports.procedimentoSet();

            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = imports.retornaSet().Tables[0].Columns.Count;
                    countRows = imports.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        recarga = new string[countRows, countField];
                        for (int i = 0; i < countRows; i++)//Linha
                        {
                            for (int j = 0; j < countField; j++) //Coluna
                            {
                                recarga[i, j] = Convert.ToString(imports.retornaSet().Tables[0].Rows[i][j]);
                            }
                        }
                        imports.fechaConexao();
                        for (int i = 0; i < recarga.GetLength(0); i++)//Linha
                        {
                            //conector_update_lineMD5_P2(recarga[i, 1], key.GetMd5Sum(export.registro_tipo_p2("P2", recarga[i, 0], recarga[i, 1], recarga[i, 2], recarga[i, 3], recarga[i, 4], recarga[i, 5], recarga[i, 6], recarga[i, 7], recarga[i, 8])));
                        }
                    }
                }
                imports.fechaConexao();
            }
            for (i = 0; i < vetor.GetLength(0); i++)
            {
                if (recarga.GetLength(0) > 0 && recarga01.GetLength(0) > 0)
                {
                    if (vetor[i, 2] == "*")
                    {
                        conector_autentica_pedido(recarga[i, 0], recarga[i, 1]);
                    }
                }
            }

            return 0;
        }
        protected bool conectorWEB_replace_reserva(string idPedido,
                                                      string idloja,
                                                      string idfuncionario,
                                                      string idcliente,
                                                      string pdv,
                                                      string idparamentro,
                                                      string idmetodo,
                                                      string status,
                                                      string usuario,
                                                      string emissao,
                                                      string expiracao,
                                                      string discount,
                                                      string valorLiquido,
                                                      string valorTotal,
                                                      string qttyItens,
                                                      string frete,
                                                      string observacao,
                                                      string geraEntrega,
                                                      string geraMontagem,
                                                      string sinal,
                                                       string flagCaixa,
                                                       string final,
                                                       string impresso,
                                                       string entrada,
                                                       string condicao,
                                                       string cripto,
                                                       string flagFormaFinalizacao,
                                                       string sequenciaDav,
                                                       string sequenciaPreVenda,
                                                       string numeroCupom,
                                                       string importCarga
            )
        {
            bool valida = false;
            try
            {
                auxConsistencia = 0;
                exports.abreConexao();
                exports.startTransaction("conectorWEB_replace_reserva");
                exports.addParametro("replace_idpedido", idPedido);
                exports.addParametro("replace_idLoja", idloja);
                exports.addParametro("replace_idfuncionario", idfuncionario);
                exports.addParametro("replace_idcliente", idcliente);
                exports.addParametro("replace_pdv", pdv.Replace("\0", "").Trim());
                exports.addParametro("replace_idparamentro", idparamentro);
                exports.addParametro("replace_idmetodo", idmetodo);
                exports.addParametro("replace_status", status);
                exports.addParametro("replace_usuario", usuario);
                exports.addParametro("replace_emissao", String.Format("{0:yyyyMMdd}", Convert.ToDateTime(emissao)));
                exports.addParametro("replace_expiracao", String.Format("{0:yyyyMMdd}", Convert.ToDateTime(expiracao)));
                exports.addParametro("replace_discount", discount.Replace(",", "."));
                exports.addParametro("replace_valorLiquido", valorLiquido.Replace(",", "."));
                exports.addParametro("replace_valorTotal", valorTotal.Replace(",", "."));
                exports.addParametro("replace_qttyItens", qttyItens.Replace(",", "."));
                exports.addParametro("replace_frete", frete.Replace(",", "."));
                exports.addParametro("replace_observacao", observacao);
                exports.addParametro("replace_geraEntrega", geraEntrega);
                exports.addParametro("replace_geraMontagem", geraMontagem);
                exports.addParametro("replace_sinal", sinal);
                exports.addParametro("replace_flagCaixa", flagCaixa);
                exports.addParametro("replace_final", final);
                exports.addParametro("replace_impresso", impresso);
                exports.addParametro("replace_entrada", String.Format("{0:yyyyMMdd}", Convert.ToDateTime(entrada.Replace(",", ".") == "" ? DateTime.Now.ToShortDateString() : entrada.Replace(",", "."))));
                exports.addParametro("replace_condicao", condicao);
                exports.addParametro("replace_cripto", cripto);
                exports.addParametro("replace_flagFormaFinalizacao", flagFormaFinalizacao);
                exports.addParametro("replace_sequenciaDav", sequenciaDav == "" ? "0" : sequenciaDav);
                exports.addParametro("replace_sequenciaPreVenda", sequenciaPreVenda == "" ? "0" : sequenciaPreVenda);
                exports.addParametro("replace_numeroCupom", numeroCupom);
                exports.addParametro("replace_importCarga", importCarga);
                exports.procedimentoRead();
                /*if (exports.retornaRead().Read() == true)
                {
                    valida = true;
                }*/

                valida = true;

                exports.fechaRead();
                //exports.commit();
            }
            catch (Exception erro)
            {
                auxConsistencia = 1;
                valida = false;
            }
            finally
            {
                exports.fechaConexao();
                if (auxConsistencia == 0)
                {
                }
            }
            return valida;
        }

        protected bool conectorWEB_replace_pedidoFinanceiro(string idPedido,
                      string idLoja,
                      string idFinalizadora,
                      string idMetodo,
                      string idTerminal,
                      string idfuncionario,
                      string emissao,
                      string referencia,
                      string encargos,
                      string entrada,
                      string parcelas,
                      string observacao)
        {
            bool valida = false;
            try
            {
                auxConsistencia = 0;
                exports.abreConexao();
                exports.startTransaction("conectorWEB_replace_pedidoFinanceiro");
                exports.addParametro("replace_idpedido", idPedido);
                exports.addParametro("replace_idLoja", idLoja);
                exports.addParametro("replace_idFinalizadora", idFinalizadora);
                exports.addParametro("replace_idMetodo", idMetodo);
                exports.addParametro("replace_idTerminal", idTerminal);
                exports.addParametro("replace_idFuncionario", idfuncionario);
                exports.addParametro("replace_emissao", String.Format("{0:yyyyMMdd}", Convert.ToDateTime(emissao)));
                exports.addParametro("replace_referencia", referencia.Replace(",", "."));
                exports.addParametro("replace_encargos", encargos.Replace(",", "."));
                exports.addParametro("replace_entrada", entrada.Replace(",", "."));
                exports.addParametro("replace_numeroParcelas", parcelas);
                exports.addParametro("replace_observacao", observacao);
                exports.procedimentoRead();
                /*if (exports.retornaRead().Read() == true)
                {
                    valida = true;
                }*/
                valida = true;
                exports.fechaRead();
                //exports.commit();
            }
            catch (Exception erro)
            {
                valida = false;
                auxConsistencia = 1;
            }
            finally
            {
                exports.fechaConexao();
            }
            return valida;
        }
        protected bool conectorWEB_replace_pedidoCheque(string idPedido,
              string idLoja,
              string idFinalizadora,
              string idCliente,
              string idMetodo,
              string referencia,
              string encargos,
              string parcelas,
              string emissao,
              string _finally,
              string observacao)
        {
            bool valida = false;
            try
            {
                auxConsistencia = 0;
                exports.abreConexao();
                exports.startTransaction("conectorWEB_replace_pedidoCheque");
                exports.addParametro("replace_idpedido", idPedido);
                exports.addParametro("replace_idLoja", idLoja);
                exports.addParametro("replace_idFinalizadora", idFinalizadora);
                exports.addParametro("replace_idCliente", idCliente);
                exports.addParametro("replace_idMetodo", idMetodo);
                exports.addParametro("replace_referencia", referencia.Replace(",", "."));
                exports.addParametro("replace_encargos", encargos.Replace(",", "."));
                exports.addParametro("replace_numeroCheques", parcelas);
                exports.addParametro("replace_emissao", String.Format("{0:yyyyMMdd}", Convert.ToDateTime(emissao)));
                exports.addParametro("replace_finally", _finally);
                exports.addParametro("replace_observacao", observacao);
                exports.procedimentoRead();
                /*if (exports.retornaRead().Read() == true)
                {
                    valida = true;
                }*/
                valida = true;
                exports.fechaRead();
                //exports.commit();
            }
            catch (Exception erro)
            {
                valida = false;
                auxConsistencia = 1;
            }
            finally
            {
                exports.fechaConexao(); 
            }
            return valida;
        }
        protected bool conectorWEB_replace_pedidoBoleto(string idPedido,
                    string cedente,
                    string idFinalizadora,
                    string sacador,
                    string idMetodo,
                    string referencia,
                    string encargos,
                    string parcelas,
                    string emissao,
                    string idFuncionario)
        {
            bool valida = false;

            try
            {
                auxConsistencia = 0;
                exports.abreConexao();
                exports.startTransaction("conectorWEB_replace_pedidoBoleto");
                exports.addParametro("replace_idpedido", idPedido);
                exports.addParametro("replace_cedente", cedente);
                exports.addParametro("replace_idFinalizadora", idFinalizadora);
                exports.addParametro("replace_sacador", sacador);
                exports.addParametro("replace_idMetodo", idMetodo);
                exports.addParametro("replace_referencia", referencia.Replace(",", "."));
                exports.addParametro("replace_encargos", encargos.Replace(",", "."));
                exports.addParametro("replace_numeroParcelas", parcelas);
                exports.addParametro("replace_emissao", String.Format("{0:yyyyMMdd}", Convert.ToDateTime(emissao)));
                exports.addParametro("replace_funcionario", idFuncionario);
                exports.procedimentoRead();
                /*if (exports.retornaRead().Read() == true)
                {
                    valida = true;
                }*/
                valida = true;
                exports.fechaRead();
                //exports.commit();
            }
            catch (Exception erro)
            {
                valida = false;
                auxConsistencia = 1;
            }
            finally
            {
                exports.fechaConexao();
            }
            return valida;
        }
        protected bool conectorWEB_replace_pedidoCartao(string idPedido,
                string idLoja,
                string idFinalizadora,
                string idAdministradora,
                string idMetodo,
                string parcelas,
                string emissao,
                string referencia,
                string encargos)
        {
            bool valida = false;
            try
            {
                auxConsistencia = 0;
                exports.abreConexao();
                exports.startTransaction("conectorWEB_replace_pedidoCartao");
                exports.addParametro("replace_idpedido", idPedido);
                exports.addParametro("replace_idLoja", idLoja);
                exports.addParametro("replace_idFinalizadora", idFinalizadora);
                exports.addParametro("replace_idAdministradora", idAdministradora);
                exports.addParametro("replace_idMetodo", idMetodo);
                exports.addParametro("replace_numeroParcelas", parcelas);
                exports.addParametro("replace_emissao", String.Format("{0:yyyyMMdd}", Convert.ToDateTime(emissao)));
                exports.addParametro("replace_referencia", referencia.Replace(",", "."));
                exports.addParametro("replace_encargos", encargos.Replace(",", "."));
                exports.procedimentoRead();
                /*if (exports.retornaRead().Read() == true)
                {
                    valida = true;
                }*/
                valida = true;
                exports.fechaRead();
                //exports.commit();
            }
            catch (Exception erro)
            {
                valida = false;
                auxConsistencia = 1;
            }
            finally
            {
                exports.fechaConexao(); 
            }
            return valida;
        }
        protected bool conectorWEB_replace_pedidoConvenio(string idPedido,
                    string idLoja,
                    string idFinalizadora,
                    string idCliente,
                    string idConvenio,
                    string idMetodo,
                    string parcelas,
                    string emissao,
                    string referencia,
                    string encargos)
        {
            bool valida = false;
            try
            {
                auxConsistencia = 0;
                exports.abreConexao();
                exports.startTransaction("conectorWEB_replace_pedidoConvenio");
                exports.addParametro("replace_idpedido", idPedido);
                exports.addParametro("replace_idLoja", idLoja);
                exports.addParametro("replace_idFinalizadora", idFinalizadora);
                exports.addParametro("replace_idCliente", idCliente);
                exports.addParametro("replace_convenio", idConvenio);
                exports.addParametro("replace_idMetodo", idMetodo);
                exports.addParametro("replace_numeroParcelas", parcelas);
                exports.addParametro("replace_emissao", String.Format("{0:yyyyMMdd}", Convert.ToDateTime(emissao)));
                exports.addParametro("replace_referencia", referencia.Replace(",", "."));
                exports.addParametro("replace_encargos", encargos.Replace(",", "."));
                exports.procedimentoRead();
                /*if (exports.retornaRead().Read() == true)
                {
                    valida = true;
                }*/
                valida = true;
                exports.fechaRead();
                //exports.commit();
            }
            catch (Exception erro)
            {
                valida = false;
                auxConsistencia = 1;
            }
            finally
            {
                exports.fechaConexao();
            }
            return valida;
        }
        protected bool conectorWEB_replace_pedidoCrediario(string idPedido,
                    string idLoja,
                    string idFinalizadora,
                    string idCliente,
                    string idMetodo,
                    string idFuncionario,
                    string parcelas,
                    string emissao,
                    string entrada,
                    string encargos,
                    string referencia,
                    string observacao,
                    string _finally)
        {
            bool valida = false;

            try
            {
                auxConsistencia = 0;
                exports.abreConexao();
                exports.startTransaction("conectorWEB_replace_pedidoCrediario");
                exports.addParametro("replace_idpedido", idPedido);
                exports.addParametro("replace_idLoja", idLoja);
                exports.addParametro("replace_idFinalizadora", idFinalizadora);
                exports.addParametro("replace_idCliente", idCliente);
                exports.addParametro("replace_idMetodo", idMetodo);
                exports.addParametro("replace_idFuncionario", idFuncionario);
                exports.addParametro("replace_numeroParcelas", parcelas);
                exports.addParametro("replace_emissao", String.Format("{0:yyyyMMdd}", Convert.ToDateTime(emissao)));
                exports.addParametro("replace_entrada", entrada.Replace(",", "."));
                exports.addParametro("replace_encargos", encargos.Replace(",", "."));
                exports.addParametro("replace_referencia", referencia.Replace(",", "."));
                exports.addParametro("replace_observacao", observacao);
                exports.addParametro("replace_finally", _finally);
                exports.procedimentoRead();
                /*if (exports.retornaRead().Read() == true)
                {
                    valida = true;
                }*/
                valida = true;
                exports.fechaRead();
                //exports.commit();
            }
            catch (Exception erro)
            {
                valida = false;
                auxConsistencia = 1;
            }
            finally
            {
                exports.fechaConexao();
            }
            return valida;
        }
        protected bool conectorWEB_replace_pedidoItens(string idPedido,
                                      string idLoja,
                                      string idProduto,
                                      string sequencial,
                                      string idfuncionario,
                                      string situacao,
                                      string quantidade,
                                      string priceLiquido,
                                      string price,
                                      string valorDiscount,
                                      string priceFull,
                                      string idEan,
                                      string codigoBarra,
                                      string idUnidadeMedida, string promocional)
        {
            bool valida = false;

            try
            {
                auxConsistencia = 0;
                exports.abreConexao();
                exports.startTransaction("conectorWEB_replace_pedidoItens");
                exports.addParametro("replace_idpedido", idPedido);
                exports.addParametro("replace_idLoja", idLoja);
                exports.addParametro("replace_idProduto", idProduto);
                exports.addParametro("replace_sequencial", sequencial);
                exports.addParametro("replace_idfuncionario", idfuncionario);
                exports.addParametro("replace_situacao", situacao);
                exports.addParametro("replace_quantidade", quantidade.Replace(",", "."));
                exports.addParametro("replace_priceLiquido", priceLiquido.Replace(",", "."));
                exports.addParametro("replace_price", price.Replace(",", "."));
                exports.addParametro("replace_valorDiscount", valorDiscount.Replace(",", "."));
                exports.addParametro("replace_priceFull", priceFull.Replace(",", "."));
                exports.addParametro("replace_idEan", idEan);
                exports.addParametro("replace_codigoBarra", codigoBarra);
                exports.addParametro("replace_idunidadeMedida", idUnidadeMedida);
                exports.addParametro("replace_promocional", promocional);
                exports.procedimentoRead();
                /*if (exports.retornaRead().Read() == true)
                {
                    valida = true;
                }*/
                valida = true;
                exports.fechaRead();
                //exports.commit();
            }
            catch (Exception erro)
            {
                valida = false;
                string e = erro.ToString();
                auxConsistencia = 1;
            }
            finally
            {
                exports.fechaConexao();
                if (auxConsistencia == 0)
                {
                }
            }
            return valida;
        }
        #endregion

        #region //Transmissao Entradas

        public void conector_entradas()
        {
            string[,] vetor = new string[0, 0];
            string[,] recarga = new string[0, 0]; 
            string[,] recarga01 = new string[0, 0];
            string[,] recarga02; 
            string[,] recarga03;

            bool valida = false;

            //Entradas
            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                imports.singleTransaction("select * from entrada tab where tab.idLoja=?store and tab.importCarga=0 and status=1");
                imports.addParametro("?store", store);
                imports.procedimentoSet();

            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = imports.retornaSet().Tables[0].Columns.Count;
                    countRows = imports.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        vetor = new string[countRows, 3];
                        recarga = new string[countRows, countField];
                        for (int i = 0; i < countRows; i++)//Linha
                        {
                            for (int j = 0; j < countField; j++) //Coluna
                            {
                                recarga[i, j] = Convert.ToString(imports.retornaSet().Tables[0].Rows[i][j]);
                            }
                        }
                        imports.fechaConexao();
                        for (int i = 0; i < recarga.GetLength(0); i++)//Linha
                        {
                            if (conectorWEB_replace_entrada(recarga[i, 0], recarga[i, 1], recarga[i, 2], recarga[i, 3], recarga[i, 4], recarga[i, 5], recarga[i, 6], recarga[i, 7], recarga[i, 8], recarga[i, 9], recarga[i, 10], recarga[i, 11], recarga[i, 12], recarga[i, 13], recarga[i, 14], recarga[i, 15], recarga[i, 16], recarga[i, 17], recarga[i, 18], recarga[i, 19], recarga[i, 20], recarga[i, 21], recarga[i, 22], recarga[i, 23], recarga[i, 24], recarga[i, 25], recarga[i, 26], recarga[i, 27], recarga[i, 28], recarga[i, 29], recarga[i, 30], recarga[i, 31], recarga[i, 32], recarga[i, 33], recarga[i, 34], recarga[i, 35],
                                recarga[i, 36], recarga[i, 37], recarga[i, 38], recarga[i, 39], recarga[i, 40], recarga[i, 41], recarga[i, 42], recarga[i, 43], recarga[i, 44], recarga[i, 45], recarga[i, 46], recarga[i, 47], recarga[i, 48], recarga[i, 49], recarga[i, 50], recarga[i, 51], recarga[i, 52], recarga[i, 53], recarga[i, 54], recarga[i, 55], recarga[i, 56], recarga[i, 57], recarga[i, 58], recarga[i, 59], recarga[i, 60], recarga[i, 61], recarga[i, 62], recarga[i, 63], recarga[i, 64], recarga[i, 65], recarga[i, 66], recarga[i, 67]) == true)
                            {
                                vetor[i, 0] = recarga[i, 0];
                                vetor[i, 1] = recarga[i, 1];
                                vetor[i, 2] = "#";
                            }
                        }
                    }
                }

                imports.fechaConexao();
            }

            //EntradaItens
            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                imports.singleTransaction("select tab1.* from nf tab inner join nfItem tab1 on(tab.nf = tab1.idNf) where tab.importCarga=0 and tab.loja=?store and tab.statusNf = 1 and tab.status=1");
                imports.addParametro("?store", store);
                imports.procedimentoSet();

            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = imports.retornaSet().Tables[0].Columns.Count;
                    countRows = imports.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        recarga01 = new string[countRows, countField];
                        for (int i = 0; i < countRows; i++)//Linha
                        {
                            for (int j = 0; j < countField; j++) //Coluna
                            {
                                recarga01[i, j] = Convert.ToString(imports.retornaSet().Tables[0].Rows[i][j]);
                            }
                        }
                        imports.fechaConexao();
                        for (int i = 0; i < recarga01.GetLength(0); i++)//Linha
                        {
                            if (conectorWEB_replace_entradaItem(recarga[i, 0], recarga[i, 1], recarga[i, 2], recarga[i, 3], recarga[i, 4], recarga[i, 5], recarga[i, 6], recarga[i, 7], recarga[i, 8], recarga[i, 9], recarga[i, 10], recarga[i, 11], recarga[i, 12], recarga[i, 13], recarga[i, 14], recarga[i, 15], recarga[i, 16], recarga[i, 17], recarga[i, 18], recarga[i, 19], recarga[i, 20], recarga[i, 21], recarga[i, 22], recarga[i, 23], recarga[i, 24], recarga[i, 25], recarga[i, 26], recarga[i, 27], recarga[i, 28], recarga[i, 29], recarga[i, 30], recarga[i, 31], recarga[i, 32], recarga[i, 33], recarga[i, 34], recarga[i, 35],
                                recarga[i, 36], recarga[i, 37], recarga[i, 38], recarga[i, 39], recarga[i, 40], recarga[i, 41], recarga[i, 42], recarga[i, 43], recarga[i, 44], recarga[i, 45], recarga[i, 46], recarga[i, 47], recarga[i, 48], recarga[i, 49], recarga[i, 50], recarga[i, 51], recarga[i, 52], recarga[i, 53], recarga[i, 54], recarga[i, 55], recarga[i, 56], recarga[i, 57], recarga[i, 58], recarga[i, 59], recarga[i, 60], recarga[i, 61], recarga[i, 62], recarga[i, 63], recarga[i, 64], recarga[i, 65], recarga[i, 66], recarga[i, 67],
                                recarga[i, 68], recarga[i, 69], recarga[i, 70], recarga[i, 71], recarga[i, 72], recarga[i, 73], recarga[i, 74], recarga[i, 75], recarga[i, 76], recarga[i, 77], recarga[i, 78], recarga[i, 79], recarga[i, 80], recarga[i, 81], recarga[i, 82], recarga[i, 83], recarga[i, 84], recarga[i, 85]) == true)
                            {
                                for (int j = 0; j < vetor.GetLength(0); j++)
                                {
                                    if (vetor[j, 0] == recarga01[i, 0])
                                    {
                                        vetor[j, 2] = "*";
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                imports.fechaConexao();
            }

            //Entrada Financeiro
            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                imports.singleTransaction("select tab1.* from entrada tab inner join entradaFinanceiro tab1 on(tab.idEntrada = tab1.idEntrada) where tab.idLoja=?store and tab.importCarga=0 and tab.status=1");
                imports.addParametro("?store", store);
                imports.procedimentoSet();

            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = imports.retornaSet().Tables[0].Columns.Count;
                    countRows = imports.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        recarga02 = new string[countRows, countField];
                        for (int i = 0; i < countRows; i++)//Linha
                        {
                            for (int j = 0; j < countField; j++) //Coluna
                            {
                                recarga02[i, j] = Convert.ToString(imports.retornaSet().Tables[0].Rows[i][j]);
                            }
                        }
                        imports.fechaConexao();
                        for (int i = 0; i < recarga02.GetLength(0); i++)//Linha
                        {
                            conectorWEB_replace_entradaFinanceiro(recarga02[i, 0], recarga02[i, 1], recarga02[i, 2], recarga02[i, 3], recarga02[i, 4], recarga02[i, 5], recarga02[i, 6], recarga02[i, 7], recarga02[i, 8], recarga02[i, 9], recarga02[i, 10], recarga02[i, 11], recarga02[i, 12], recarga02[i, 13], recarga02[i, 14], recarga02[i, 15], recarga02[i, 16], recarga02[i, 17]);
                        }
                    }
                }
                imports.fechaConexao();
            }
            //Entradas Impostos
            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                imports.singleTransaction("select tab1.* from entrada tab inner join entradaImp tab1 on(tab.idEntrada = tab1.idEntrada) where tab.idLoja=?store and tab.importCarga=0 and tab.status=1");
                imports.addParametro("?store", store);
                imports.procedimentoSet();

            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = imports.retornaSet().Tables[0].Columns.Count;
                    countRows = imports.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        recarga03 = new string[countRows, countField];
                        for (int i = 0; i < countRows; i++)//Linha
                        {
                            for (int j = 0; j < countField; j++) //Coluna
                            {
                                recarga03[i, j] = Convert.ToString(imports.retornaSet().Tables[0].Rows[i][j]);
                            }
                        }
                        imports.fechaConexao();
                        for (int i = 0; i < recarga03.GetLength(0); i++)//Linha
                        {
                            conectorWEB_replace_entradaImp(recarga03[i, 0], recarga03[i, 1], recarga03[i, 2], recarga03[i, 3], recarga03[i, 4], recarga03[i, 5], recarga03[i, 6], recarga03[i, 7], recarga03[i, 8], recarga03[i, 9], recarga03[i, 10], recarga03[i, 11], recarga03[i, 12], recarga03[i, 13], recarga03[i, 14]);
                        }
                    }
                }
                imports.fechaConexao();
            }

            for (i = 0; i < vetor.GetLength(0); i++)
            {
                if (recarga.GetLength(0) > 0 && recarga01.GetLength(0) > 0)
                {
                    if (vetor[i, 2] == "*")
                    {
                        conector_autentica_entrada_baixa(recarga[i, 0], recarga[i, 1]);
                    }
                }
            }
        }

        public void conector_autentica_entrada()
        {
            string[,] autentic; //Matriz Bidimencionada
            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                imports.singleTransaction("select * from entrada where importCarga in(0) and idloja=?store");
                imports.addParametro("?store", store);
                imports.procedimentoSet();
            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = imports.retornaSet().Tables[0].Columns.Count;
                    countRows = imports.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        autentic = new string[countRows, countField];
                        for (int i = 0; i < countRows; i++)//Linha
                        {
                            for (int j = 0; j < countField; j++) //Coluna
                            {
                                autentic[i, j] = Convert.ToString(imports.retornaSet().Tables[0].Rows[i][j]);
                            }
                        }
                        imports.fechaConexao();
                        for (int i = 0; i < autentic.GetLength(0); i++)//Linha
                        {
                            conector_autentica_entrada_baixa(autentic[i, 0], autentic[i, 1]);
                        }
                    }
                }
                imports.fechaConexao();
            }
        }
        public void conector_autentica_entrada_baixa(string chave, string store)
        {
            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                imports.singleTransaction("update entrada set importCarga=1 where idEntrada=?chave and importCarga=0 and status=1 and idLoja=?store");
                imports.addParametro("?chave", chave);
                imports.addParametro("?store", store);
                imports.procedimentoRead();
                if (imports.retornaRead().Read() == true)
                {
                }

            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                }
                imports.fechaConexao();
            }
        }
        protected bool conectorWEB_replace_entradaImp(string replace_identradaImp,
        string replace_identrada,
        string replace_cfop,
        string replace_cst,
        string replace_aliquota,
        string replace_reducao,
        string replace_valorApurado,
        string replace_valorInformado,
        string replace_impostoApurado,
        string replace_impostoInformado,
        string replace_baseIsentoApurado,
        string replace_baseIsentoInformado,
        string replace_baseIpiApurado,
        string replace_valorIpiApurado,
        string replace_tipo
)
        {
            bool valida = false;

            try
            {
                auxConsistencia = 0;
                exports.abreConexao();
                exports.startTransaction("conectorWEB_replace_entradaImp");
                exports.addParametro("replace_identradaImp", replace_identradaImp);
                exports.addParametro("replace_identrada", replace_identrada);
                exports.addParametro("replace_cfop", replace_cfop);
                exports.addParametro("replace_cst", replace_cst);
                exports.addParametro("replace_aliquota", replace_aliquota);
                exports.addParametro("replace_reducao", replace_reducao.Replace(",", "."));
                exports.addParametro("replace_valorApurado", replace_valorApurado.Replace(",", "."));
                exports.addParametro("replace_valorInformado", replace_valorInformado.Replace(",", "."));
                exports.addParametro("replace_impostoApurado", replace_impostoApurado.Replace(",", "."));
                exports.addParametro("replace_impostoInformado", replace_impostoInformado.Replace(",", "."));
                exports.addParametro("replace_baseIsentoApurado", replace_baseIsentoApurado.Replace(",", "."));
                exports.addParametro("replace_baseIsentoInformado", replace_baseIsentoInformado.Replace(",", "."));
                exports.addParametro("replace_baseIpiApurado", replace_baseIpiApurado.Replace(",", "."));
                exports.addParametro("replace_valorIpiApurado", replace_valorIpiApurado.Replace(",", "."));
                exports.addParametro("replace_tipo", replace_tipo);
                exports.procedimentoRead();
                /*if (exports.retornaRead().Read() == true)
                {
                    valida = true;
                }*/
                valida = true;
                exports.fechaRead();
                //exports.commit();
            }
            catch (Exception erro)
            {
                valida = false;
                auxConsistencia = 1;
            }
            finally
            {
                exports.fechaConexao();
                if (auxConsistencia == 0)
                {
                }
            }
            return valida;
        }
        protected bool conectorWEB_replace_entradaFinanceiro(string replace_identradaFinanceiro,
                                                                string replace_identrada,
                                                                string replace_idfinalizadora,
                                                                string replace_documento,
                                                                string replace_prazo,
                                                                string replace_aceite,
                                                                string replace_vencimento,
                                                                string replace_competencia,
                                                                string replace_emissaoDocumento,
                                                                string replace_desconto,
                                                                string replace_financeiro,
                                                                string replace_valor,
                                                                string replace_parcela,
                                                                string replace_idbanco,
                                                                string replace_nossoNumero,
                                                                string replace_conta,
                                                                string replace_cedente,
                                                                string replace_formaPgto
        )
        {
            bool valida = false;

            try
            {
                auxConsistencia = 0;
                exports.abreConexao();
                exports.startTransaction("conectorWEB_replace_entradaFinanceiro");
                exports.addParametro("replace_identradaFinanceiro", replace_identradaFinanceiro);
                exports.addParametro("replace_identrada", replace_identrada);
                exports.addParametro("replace_idfinalizadora", replace_idfinalizadora);
                exports.addParametro("replace_documento", replace_documento);
                exports.addParametro("replace_prazo", replace_prazo);
                exports.addParametro("replace_aceite", replace_aceite);
                exports.addParametro("replace_vencimento", replace_vencimento);
                exports.addParametro("replace_competencia", replace_competencia);
                exports.addParametro("replace_emissaoDocumento",String.Format("{0:yyyyMMdd}",  Convert.ToDateTime(replace_emissaoDocumento)));
                exports.addParametro("replace_desconto", replace_desconto.Replace(",", "."));
                exports.addParametro("replace_financeiro", replace_financeiro.Replace(",", "."));
                exports.addParametro("replace_valor", replace_valor.Replace(",", "."));
                exports.addParametro("replace_parcela", replace_parcela);
                exports.addParametro("replace_idbanco", replace_idbanco);
                exports.addParametro("replace_nossoNumero", replace_nossoNumero);
                exports.addParametro("replace_conta", replace_conta);
                exports.addParametro("replace_cedente", replace_cedente);
                exports.addParametro("replace_formaPgto", replace_formaPgto);
                exports.procedimentoRead();
                /*if (exports.retornaRead().Read() == true)
                {
                    valida = true;
                }*/
                valida = true;
                exports.fechaRead();
                //exports.commit();
            }
            catch (Exception erro)
            {
                valida = false;
                auxConsistencia = 1;
            }
            finally
            {
                exports.fechaConexao();
                if (auxConsistencia == 0)
                {
                }
            }
            return valida;
        }

        protected bool conectorWEB_replace_entradaItem(string replace_idEntradaItem,
          string replace_identrada,
          string replace_idProduto,
          string replace_custoLiquido,
          string replace_custoBruto,
          string replace_custoNota,
          string replace_custoNotaTotal,
          string replace_custoReposicao,
          string replace_custoTotal,
          string replace_discount1,
          string replace_discount2,
          string replace_discount3,
          string replace_discountValue,
          string replace_acrescimo,
          string replace_bonificacao,
          string replace_despesasNaoTributadas,
          string replace_despesasTributadas,
          string replace_chaveEmbalagem,
          string replace_barra,
          string replace_idunidadeMedida,
          string replace_entregue,
          string replace_vendo,
          string replace_financeiro,
          string replace_frete,
          string replace_icmsEntrada,
          string replace_reducaoEntrada,
          string replace_icmsFrete,
          string replace_icmsSaida,
          string replace_reducaoSaida,
          string replace_ipi,
          string replace_priceVenda,
          string replace_quantidade,
          string replace_sugestao,
          string replace_valorIpi,
          string replace_substituicao,
          string replace_lucro,
          string replace_lucroValor,
          string replace_lucroBruto,
          string replace_quantidadeRecebida,
          string replace_quantidadePendente,
          string replace_creditoIcms,
          string replace_creditoPis,
          string replace_creditoCofins,
          string replace_valorFinanceiro,
          string replace_valorFrete,
          string replace_sumFrete,
          string replace_numeroNota,
          string replace_serie,
          string replace_diferencaQtty,
          string replace_diferencaCustoLiqNota,
          string replace_diferencaCustoTotalNota,
          string replace_dateInsert,
          string replace_margem,
          string replace_sumSt,
          string replace_cstIpi,
          string replace_cstPis,
          string replace_cstCofins,
          string replace_cstIcms,
          string replace_icmsCreditoSt,
          string replace_icmsTotalSt,
          string replace_cfop,
          string replace_cteInterna,
          string replace_cteInterestadual,
          string replace_valorIcmsSubstituicao,
          string replace_baseCalculoSubstituicao,
          string replace_basePis,
          string replace_baseCofins,
          string replace_valorPis,
          string replace_valorCofins,
          string replace_estoque,
          string replace_valorIpiTotal,
          string replace_baseIcms,
          string replace_custoLiquidoAnterior,
          string replace_lucroAnterior,
          string replace_custoTransferencia,
          string replace_bonificacaoValor,
          string replace_validade,
          string replace_freteValor,
          string replace_flagRecebe,
          string replace_typeAliquota,
          string replace_porcentagemTransf,
          string replace_chaveItemCompra,
          string replace_custoMedio,
          string replace_novoPrecoVenda,
          string replace_sequencia,
          string replace_spedNcm
        )
        {
            bool valida = false;

            try
            {
                auxConsistencia = 0;
                exports.abreConexao();
                exports.startTransaction("conectorWEB_replace_entradaItem");
                  exports.addParametro("replace_idEntradaItem", replace_idEntradaItem);
                  exports.addParametro("replace_identrada", replace_identrada);
                  exports.addParametro("replace_idProduto", replace_idProduto);
                  exports.addParametro("replace_custoLiquido", replace_custoLiquido.Replace(",", "."));
                  exports.addParametro("replace_custoBruto", replace_custoBruto.Replace(",", "."));
                  exports.addParametro("replace_custoNota", replace_custoNota.Replace(",", "."));
                  exports.addParametro("replace_custoNotaTotal", replace_custoNotaTotal.Replace(",", "."));
                  exports.addParametro("replace_custoReposicao", replace_custoReposicao.Replace(",", "."));
                  exports.addParametro("replace_custoTotal", replace_custoTotal.Replace(",", "."));
                  exports.addParametro("replace_discount1", replace_discount1.Replace(",", "."));
                  exports.addParametro("replace_discount2", replace_discount2.Replace(",", "."));
                  exports.addParametro("replace_discount3", replace_discount3.Replace(",", "."));
                  exports.addParametro("replace_discountValue", replace_discountValue.Replace(",", "."));
                  exports.addParametro("replace_acrescimo", replace_acrescimo.Replace(",", "."));
                  exports.addParametro("replace_bonificacao", replace_bonificacao.Replace(",", "."));
                  exports.addParametro("replace_despesasNaoTributadas", replace_despesasNaoTributadas.Replace(",", "."));
                  exports.addParametro("replace_despesasTributadas", replace_despesasTributadas.Replace(",", "."));
                  exports.addParametro("replace_chaveEmbalagem", replace_chaveEmbalagem);
                  exports.addParametro("replace_barra", replace_barra);
                  exports.addParametro("replace_idunidadeMedida", replace_idunidadeMedida);
                  exports.addParametro("replace_entregue", replace_entregue);
                  exports.addParametro("replace_vendo", replace_vendo.Replace(",", "."));
                  exports.addParametro("replace_financeiro", replace_financeiro.Replace(",", "."));
                  exports.addParametro("replace_frete", replace_frete.Replace(",", "."));
                  exports.addParametro("replace_icmsEntrada", replace_icmsEntrada.Replace(",", "."));
                  exports.addParametro("replace_reducaoEntrada", replace_reducaoEntrada.Replace(",", "."));
                  exports.addParametro("replace_icmsFrete", replace_icmsFrete.Replace(",", "."));
                  exports.addParametro("replace_icmsSaida", replace_icmsSaida.Replace(",", "."));
                  exports.addParametro("replace_reducaoSaida", replace_reducaoSaida.Replace(",", "."));
                  exports.addParametro("replace_ipi", replace_ipi.Replace(",", "."));
                  exports.addParametro("replace_priceVenda", replace_priceVenda.Replace(",", "."));
                  exports.addParametro("replace_quantidade", replace_quantidade.Replace(",", "."));
                  exports.addParametro("replace_sugestao", replace_sugestao.Replace(",", "."));
                  exports.addParametro("replace_valorIpi", replace_valorIpi.Replace(",", "."));
                  exports.addParametro("replace_substituicao", replace_substituicao.Replace(",", "."));
                  exports.addParametro("replace_lucro", replace_lucro.Replace(",", "."));
                  exports.addParametro("replace_lucroValor", replace_lucroValor.Replace(",", "."));
                  exports.addParametro("replace_lucroBruto", replace_lucroBruto.Replace(",", "."));
                  exports.addParametro("replace_quantidadeRecebida", replace_quantidadeRecebida.Replace(",", "."));
                  exports.addParametro("replace_quantidadePendente", replace_quantidadePendente.Replace(",", "."));
                  exports.addParametro("replace_creditoIcms", replace_creditoIcms.Replace(",", "."));
                  exports.addParametro("replace_creditoPis", replace_creditoPis.Replace(",", "."));
                  exports.addParametro("replace_creditoCofins", replace_creditoCofins.Replace(",", "."));
                  exports.addParametro("replace_valorFinanceiro", replace_valorFinanceiro.Replace(",", "."));
                  exports.addParametro("replace_valorFrete", replace_valorFrete.Replace(",", "."));
                  exports.addParametro("replace_sumFrete", replace_sumFrete.Replace(",", "."));
                  exports.addParametro("replace_numeroNota", replace_numeroNota.Replace(",", "."));
                  exports.addParametro("replace_serie", replace_serie);
                  exports.addParametro("replace_diferencaQtty", replace_diferencaQtty.Replace(",", "."));
                  exports.addParametro("replace_diferencaCustoLiqNota", replace_diferencaCustoLiqNota.Replace(",", "."));
                  exports.addParametro("replace_diferencaCustoTotalNota", replace_diferencaCustoTotalNota.Replace(",", "."));
                  exports.addParametro("replace_dateInsert", String.Format("{0:yyyyMMdd}", Convert.ToDateTime(replace_dateInsert)));
                  exports.addParametro("replace_margem", replace_margem);
                  exports.addParametro("replace_sumSt", replace_sumSt);
                  exports.addParametro("replace_cstIpi", replace_cstIpi);
                  exports.addParametro("replace_cstPis", replace_cstPis);
                  exports.addParametro("replace_cstCofins", replace_cstCofins);
                  exports.addParametro("replace_cstIcms", replace_cstIcms);
                  exports.addParametro("replace_icmsCreditoSt", replace_icmsCreditoSt);
                  exports.addParametro("replace_icmsTotalSt", replace_icmsTotalSt);
                  exports.addParametro("replace_cfop", replace_cfop);
                  exports.addParametro("replace_cteInterna", replace_cteInterna);
                  exports.addParametro("replace_cteInterestadual", replace_cteInterestadual.Replace(",", "."));
                  exports.addParametro("replace_valorIcmsSubstituicao", replace_valorIcmsSubstituicao.Replace(",", "."));
                  exports.addParametro("replace_baseCalculoSubstituicao", replace_baseCalculoSubstituicao.Replace(",", "."));
                  exports.addParametro("replace_basePis", replace_basePis.Replace(",", "."));
                  exports.addParametro("replace_baseCofins", replace_baseCofins.Replace(",", "."));
                  exports.addParametro("replace_valorPis", replace_valorPis.Replace(",", "."));
                  exports.addParametro("replace_valorCofins", replace_valorCofins.Replace(",", "."));
                  exports.addParametro("replace_estoque", replace_estoque);
                  exports.addParametro("replace_valorIpiTotal", replace_valorIpiTotal.Replace(",", ".").Replace(",", "."));
                  exports.addParametro("replace_baseIcms", replace_baseIcms.Replace(",", "."));
                  exports.addParametro("replace_custoLiquidoAnterior", replace_custoLiquidoAnterior.Replace(",", "."));
                  exports.addParametro("replace_lucroAnterior", replace_lucroAnterior.Replace(",", "."));
                  exports.addParametro("replace_custoTransferencia", replace_custoTransferencia.Replace(",", "."));
                  exports.addParametro("replace_bonificacaoValor", replace_bonificacaoValor.Replace(",", "."));
                  exports.addParametro("replace_validade", replace_validade.Replace(",", "."));
                  exports.addParametro("replace_freteValor", replace_freteValor.Replace(",", "."));
                  exports.addParametro("replace_flagRecebe", replace_flagRecebe);
                  exports.addParametro("replace_typeAliquota", replace_typeAliquota);
                  exports.addParametro("replace_porcentagemTransf", replace_porcentagemTransf.Replace(",", "."));
                  exports.addParametro("replace_chaveItemCompra", replace_chaveItemCompra);
                  exports.addParametro("replace_custoMedio", replace_custoMedio.Replace(",", "."));
                  exports.addParametro("replace_novoPrecoVenda", replace_novoPrecoVenda.Replace(",", "."));
                  exports.addParametro("replace_sequencia", replace_sequencia);
                  exports.addParametro("replace_spedNcm",replace_spedNcm);
                exports.procedimentoRead();
                /*if (exports.retornaRead().Read() == true)
                {
                    valida = true;
                }*/
                valida = true;
                exports.fechaRead();
                //exports.commit();
            }
            catch (Exception erro)
            {
                valida = false;
                auxConsistencia = 1;
            }
            finally
            {
                exports.fechaConexao();
                if (auxConsistencia == 0)
                {
                }
            }
            return valida;
        }
        protected bool conectorWEB_replace_entrada(
               string replace_identrada,
   string replace_idloja,
   string replace_idcliente,
   string replace_cfop,
   string replace_idOperacao,
   string replace_idSituacaoFiscal,
   string replace_idCodigoFiscal,
   string replace_modNotaFiscal,
   string replace_idUsuario,
   string replace_emissao,
   string replace_entrada,
   string replace_alteracao,
   string replace_idUsuarioAlteracao,
   string replace_nr_nota,
   string replace_serie,
   string replace_typeNota,
   string replace_idCompra,
   string replace_sumSt,
   string replace_substituicao,
   string replace_creditoIcms,
   string replace_bonificacao,
   string replace_conhecimentoFrete,
   string replace_idtransportadora,
   string replace_sumFrete,
   string replace_frete,
   string replace_valorFrete,
   string replace_icmsFrete,
   string replace_vendo,
   string replace_vendoValor,
   string replace_incideFinanProduto,
   string replace_financeiro,
   string replace_financeiroValor,
   string replace_baseCalculoIcms,
   string replace_baseCalculoIpi,
   string replace_valorIcmsSubstuicao,
   string replace_baseCalculoSubstituicao,
   string replace_totalProdutoSubstituicao,
   string replace_valorPis,
   string replace_valorCofins,
   string replace_baseCofins,
   string replace_basePis,
   string replace_discountFinalNota,
   string replace_valorDesconto,
   string replace_porcetagemDesconto,
   string replace_valorDespesasAcessorias,
   string replace_porcentagemDespesasAcessorias,
   string replace_typeFrete,
   string replace_totalItens,
   string replace_volume,
   string replace_valorDivergenciaFinal,
   string replace_valorTotalCompra,
   string replace_valorTotalBonificacao,
   string replace_valorTotalIpi,
   string replace_valorTotalCustoLiquido,
   string replace_valorTotalNota,
   string replace_valorTotalProduto,
   string replace_valorTotalVenda,
   string replace_informacaoComplementares,
   string replace_impApurado,
   string replace_impInformado,
   string replace_valueApurado,
   string replace_valueInformado,
   string replace_valueIpiApurado,
   string replace_totalBaseCalculoSubstituicao,
   string replace_totalIcmsST,
   string replace_status,
   string replace_baseFrete,
   string replace_chave_nfe
    )
        {
            bool valida = false;

            try
            {
                auxConsistencia = 0;
                exports.abreConexao();
                exports.startTransaction("conectorWEB_replace_entrada");
                exports.addParametro("replace_identrada", replace_identrada);
                exports.addParametro("replace_idloja", replace_idloja);
                exports.addParametro("replace_idcliente", replace_idcliente);
                exports.addParametro("replace_cfop", replace_cfop);
                exports.addParametro("replace_idOperacao", replace_idOperacao);
                exports.addParametro("replace_idSituacaoFiscal", replace_idSituacaoFiscal);
                exports.addParametro("replace_idCodigoFiscal", replace_idCodigoFiscal);
                exports.addParametro("replace_modNotaFiscal", replace_modNotaFiscal);
                exports.addParametro("replace_idUsuario", replace_idUsuario);
                exports.addParametro("replace_emissao", String.Format("{0:yyyyMMdd}", Convert.ToDateTime(replace_emissao)));
                exports.addParametro("replace_entrada",String.Format("{0:yyyyMMdd}", Convert.ToDateTime(replace_entrada)));
                exports.addParametro("replace_alteracao",String.Format("{0:yyyyMMdd}", Convert.ToDateTime(replace_alteracao)));
                exports.addParametro("replace_idUsuarioAlteracao", replace_idUsuarioAlteracao);
                exports.addParametro("replace_nr_nota", replace_nr_nota);
                exports.addParametro("replace_serie", replace_serie);
                exports.addParametro("replace_typeNota", replace_typeNota);
                exports.addParametro("replace_idCompra", replace_idCompra);
                exports.addParametro("replace_sumSt", replace_sumSt);
                exports.addParametro("replace_substituicao", replace_substituicao.Replace(",", "."));
                exports.addParametro("replace_creditoIcms", replace_creditoIcms.Replace(",", "."));
                exports.addParametro("replace_bonificacao", replace_bonificacao.Replace(",", "."));
                exports.addParametro("replace_conhecimentoFrete", replace_conhecimentoFrete);
                exports.addParametro("replace_idtransportadora", replace_idtransportadora);
                exports.addParametro("replace_sumFrete", replace_sumFrete.Replace(",", "."));
                exports.addParametro("replace_frete", replace_frete.Replace(",", "."));
                exports.addParametro("replace_valorFrete", replace_valorFrete.Replace(",", "."));
                exports.addParametro("replace_icmsFrete", replace_icmsFrete.Replace(",", "."));
                exports.addParametro("replace_vendo", replace_vendo.Replace(",", "."));
                exports.addParametro("replace_vendoValor", replace_vendoValor.Replace(",", "."));
                exports.addParametro("replace_incideFinanProduto", replace_incideFinanProduto.Replace(",", "."));
                exports.addParametro("replace_financeiro", replace_financeiro.Replace(",", "."));
                exports.addParametro("replace_financeiroValor", replace_financeiroValor.Replace(",", "."));
                exports.addParametro("replace_baseCalculoIcms", replace_baseCalculoIcms.Replace(",", "."));
                exports.addParametro("replace_baseCalculoIpi", replace_baseCalculoIpi.Replace(",", "."));
                exports.addParametro("replace_valorIcmsSubstuicao", replace_valorIcmsSubstuicao.Replace(",", "."));
                exports.addParametro("replace_baseCalculoSubstituicao", replace_baseCalculoSubstituicao.Replace(",", "."));
                exports.addParametro("replace_totalProdutoSubstituicao", replace_totalProdutoSubstituicao.Replace(",", "."));
                exports.addParametro("replace_valorPis", replace_valorPis.Replace(",", "."));
                exports.addParametro("replace_valorCofins", replace_valorCofins.Replace(",", "."));
                exports.addParametro("replace_baseCofins", replace_baseCofins.Replace(",", "."));
                exports.addParametro("replace_basePis", replace_basePis.Replace(",", "."));
                exports.addParametro("replace_discountFinalNota", replace_discountFinalNota.Replace(",", "."));
                exports.addParametro("replace_valorDesconto", replace_valorDesconto.Replace(",", "."));
                exports.addParametro("replace_porcetagemDesconto", replace_porcetagemDesconto.Replace(",", "."));
                exports.addParametro("replace_valorDespesasAcessorias", replace_valorDespesasAcessorias.Replace(",", "."));
                exports.addParametro("replace_porcentagemDespesasAcessorias", replace_porcentagemDespesasAcessorias.Replace(",", "."));
                exports.addParametro("replace_typeFrete", replace_typeFrete);
                exports.addParametro("replace_totalItens", replace_totalItens.Replace(",", "."));
                exports.addParametro("replace_volume", replace_volume.Replace(",", "."));
                exports.addParametro("replace_valorDivergenciaFinal", replace_valorDivergenciaFinal.Replace(",", "."));
                exports.addParametro("replace_valorTotalCompra", replace_valorTotalCompra.Replace(",", "."));
                exports.addParametro("replace_valorTotalBonificacao", replace_valorTotalBonificacao.Replace(",", "."));
                exports.addParametro("replace_valorTotalIpi", replace_valorTotalIpi.Replace(",", "."));
                exports.addParametro("replace_valorTotalCustoLiquido", replace_valorTotalCustoLiquido.Replace(",", "."));
                exports.addParametro("replace_valorTotalNota", replace_valorTotalNota.Replace(",", "."));
                exports.addParametro("replace_valorTotalProduto", replace_valorTotalProduto.Replace(",", "."));
                exports.addParametro("replace_valorTotalVenda", replace_valorTotalVenda.Replace(",", "."));
                exports.addParametro("replace_informacaoComplementares", replace_informacaoComplementares);
                exports.addParametro("replace_impApurado", replace_impApurado.Replace(",", "."));
                exports.addParametro("replace_impInformado", replace_impInformado.Replace(",", "."));
                exports.addParametro("replace_valueApurado", replace_valueApurado.Replace(",", "."));
                exports.addParametro("replace_valueInformado", replace_valueInformado.Replace(",", "."));
                exports.addParametro("replace_valueIpiApurado", replace_valueIpiApurado.Replace(",", "."));
                exports.addParametro("replace_totalBaseCalculoSubstituicao", replace_totalBaseCalculoSubstituicao.Replace(",", "."));
                exports.addParametro("replace_totalIcmsST", replace_totalIcmsST.Replace(",", "."));
                exports.addParametro("replace_status", replace_status);
                exports.addParametro("replace_baseFrete", replace_baseFrete.Replace(",", "."));
                exports.addParametro("replace_chave_nfe", replace_chave_nfe);
                exports.procedimentoRead();
                /*if (exports.retornaRead().Read() == true)
                {
                    valida = true;
                }*/
                valida = true;
                exports.fechaRead();
                //exports.commit();
            }
            catch (Exception erro)
            {
                valida = false;
                auxConsistencia = 1;
            }
            finally
            {
                exports.fechaConexao();
                if (auxConsistencia == 0)
                {
                }
            }
            return valida;
        }

        #endregion

        #region //Transmissao Nf Saidas

        public void conector_saidas()
        {
            string[,] vetor = new string[0, 0];
            string[,] recarga = new string[0, 0];
            string[,] recarga01 = new string[0, 0];
            string[,] recarga02;
            string[,] recarga03;

            bool valida = false;

            //Nf Saidas
            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                imports.singleTransaction("select tab.* from nf tab where tab.importCarga=0 and tab.loja=?store and tab.statusNf = 1");
                imports.addParametro("?store", store);
                imports.procedimentoSet();

            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = imports.retornaSet().Tables[0].Columns.Count;
                    countRows = imports.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        vetor = new string[countRows, 3];
                        recarga = new string[countRows, countField];
                        for (int i = 0; i < countRows; i++)//Linha
                        {
                            for (int j = 0; j < countField; j++) //Coluna
                            {
                                recarga[i, j] = Convert.ToString(imports.retornaSet().Tables[0].Rows[i][j]);
                            }
                        }
                        imports.fechaConexao();
                        for (int i = 0; i < recarga.GetLength(0); i++)//Linha
                        {
                            if (conectorWEB_replace_nf(recarga[i, 0], recarga[i, 1], recarga[i, 2], recarga[i, 3], recarga[i, 4], recarga[i, 5], recarga[i, 6], recarga[i, 7], recarga[i, 8], recarga[i, 9], recarga[i, 10], recarga[i, 11], recarga[i, 12], recarga[i, 13], recarga[i, 14], recarga[i, 15], recarga[i, 16], recarga[i, 17], recarga[i, 18], recarga[i, 19], recarga[i, 20], recarga[i, 21], recarga[i, 22], recarga[i, 23], recarga[i, 24], recarga[i, 25], recarga[i, 26], recarga[i, 27], recarga[i, 28], recarga[i, 29], recarga[i, 30], recarga[i, 31], recarga[i, 32], recarga[i, 33], recarga[i, 34],recarga[i, 35],
                                recarga[i, 36], recarga[i, 37], recarga[i, 38], recarga[i, 39], recarga[i, 40], recarga[i, 41], recarga[i, 42], recarga[i, 43], recarga[i, 44], recarga[i, 45], recarga[i, 46], recarga[i, 47], recarga[i, 48], recarga[i, 49], recarga[i, 50], recarga[i, 51], recarga[i, 52], recarga[i, 53], recarga[i, 54], recarga[i, 55], recarga[i, 56], recarga[i, 57], recarga[i, 58], recarga[i, 59], recarga[i, 60], recarga[i, 61], recarga[i, 62], recarga[i, 63], recarga[i, 64], recarga[i, 65], recarga[i, 66], recarga[i, 67], recarga[i, 68]) == true)
                            {
                                vetor[i, 0] = recarga[i, 0];
                                vetor[i, 1] = recarga[i, 1];
                                vetor[i, 2] = "#";
                            }
                        }
                    }
                }

                imports.fechaConexao();
            }

            //NfItens
            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                imports.singleTransaction("select tab1.* from nf tab inner join nfItem tab1 on(tab.nf = tab1.idNf) where tab.importCarga=0 and tab.loja=?store and tab.statusNf = 1");
                imports.addParametro("?store", store);
                imports.procedimentoSet();

            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = imports.retornaSet().Tables[0].Columns.Count;
                    countRows = imports.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        recarga01 = new string[countRows, countField];
                        for (int i = 0; i < countRows; i++)//Linha
                        {
                            for (int j = 0; j < countField; j++) //Coluna
                            {
                                recarga01[i, j] = Convert.ToString(imports.retornaSet().Tables[0].Rows[i][j]);
                            }
                        }
                        imports.fechaConexao();
                        for (int i = 0; i < recarga01.GetLength(0); i++)//Linha
                        {
                            if (conectorWEB_replace_nfItem(recarga01[i, 0], recarga01[i, 1], recarga01[i, 2], recarga01[i, 3], recarga01[i, 4], recarga01[i, 5], recarga01[i, 6], recarga01[i, 7], recarga01[i, 8], recarga01[i, 9], recarga01[i, 10], recarga01[i, 11], recarga01[i, 12], recarga01[i, 13], recarga01[i, 14], recarga01[i, 15], recarga01[i, 16], recarga01[i, 17], recarga01[i, 18], recarga01[i, 19], recarga01[i, 20], recarga01[i, 21], recarga01[i, 22], recarga01[i, 23], recarga01[i, 24], recarga01[i, 25], recarga01[i, 26], recarga01[i, 27], recarga01[i, 28], recarga01[i, 29], recarga01[i, 30], recarga01[i, 31], recarga01[i, 32], recarga01[i, 33], recarga01[i, 34], recarga01[i, 35],
                                recarga01[i, 36], recarga01[i, 37], recarga01[i, 38], recarga01[i, 39], recarga01[i, 40], recarga01[i, 41], recarga01[i, 42], recarga01[i, 43], recarga01[i, 44], recarga01[i, 45], recarga01[i, 46], recarga01[i, 47], recarga01[i, 48], recarga01[i, 49], recarga01[i, 50], recarga01[i, 51], recarga01[i, 52]) == true)
                            {
                                for (int j = 0; j < vetor.GetLength(0); j++)
                                {
                                    if (vetor[j, 0] == recarga01[i, 1])
                                    {
                                        vetor[j, 2] = "*";
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                imports.fechaConexao();
            }

            //Saidas Financeiro
            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                imports.singleTransaction("select tab1.* from nf tab inner join nfpgto tab1 on(tab.nf = tab1.idNf) where tab.importCarga=0 and tab.loja=?store and tab.statusNf = 1");
                imports.addParametro("?store", store);
                imports.procedimentoSet();

            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = imports.retornaSet().Tables[0].Columns.Count;
                    countRows = imports.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        recarga02 = new string[countRows, countField];
                        for (int i = 0; i < countRows; i++)//Linha
                        {
                            for (int j = 0; j < countField; j++) //Coluna
                            {
                                recarga02[i, j] = Convert.ToString(imports.retornaSet().Tables[0].Rows[i][j]);
                            }
                        }
                        imports.fechaConexao();
                        for (int i = 0; i < recarga02.GetLength(0); i++)//Linha
                        {
                            conectorWEB_replace_nfpgto(recarga02[i, 0], recarga02[i, 1], recarga02[i, 2], recarga02[i, 3], recarga02[i, 4], recarga02[i, 5], recarga02[i, 6], recarga02[i, 7], recarga02[i, 8], recarga02[i, 9], recarga02[i, 10], recarga02[i, 11], recarga02[i, 12], recarga02[i, 13]);
                        }
                    }
                }
                imports.fechaConexao();
            }
            //Saidas Impostos
            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                imports.singleTransaction("select tab1.* from nf tab inner join nfImposto tab1 on(tab.nf = tab1.idNf) where tab.importCarga=0 and tab.loja=?store and tab.statusNf = 1;");
                imports.addParametro("?store", store);
                imports.procedimentoSet();

            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = imports.retornaSet().Tables[0].Columns.Count;
                    countRows = imports.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        recarga03 = new string[countRows, countField];
                        for (int i = 0; i < countRows; i++)//Linha
                        {
                            for (int j = 0; j < countField; j++) //Coluna
                            {
                                recarga03[i, j] = Convert.ToString(imports.retornaSet().Tables[0].Rows[i][j]);
                            }
                        }
                        imports.fechaConexao();
                        for (int i = 0; i < recarga03.GetLength(0); i++)//Linha
                        {
                            conectorWeb_replace_nfimposto(recarga03[i, 0], recarga03[i, 1], recarga03[i, 2], recarga03[i, 3], recarga03[i, 4], recarga03[i, 5], recarga03[i, 6], recarga03[i, 7], recarga03[i, 8], recarga03[i, 9], recarga03[i, 10], recarga03[i, 11], recarga03[i, 12], recarga03[i, 13], recarga03[i, 14]);
                        }
                    }
                }
                imports.fechaConexao();
            }
            for (i = 0; i < vetor.GetLength(0); i++)
            {
                if (recarga.GetLength(0) > 0 && recarga01.GetLength(0) > 0)
                {
                    if (vetor[i, 2] == "*")
                    {
                        conector_autentica_nf_baixa(recarga[i, 0], recarga[i, 1]);
                    }
                }
            }
        }

        public void conector_autentica_nf()
        {
            string[,] autentic; //Matriz Bidimencionada
            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                imports.singleTransaction("select * from nf where importCarga in(0) and loja=?store");
                imports.addParametro("?store", store);
                imports.procedimentoSet();
            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = imports.retornaSet().Tables[0].Columns.Count;
                    countRows = imports.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        autentic = new string[countRows, countField];
                        for (int i = 0; i < countRows; i++)//Linha
                        {
                            for (int j = 0; j < countField; j++) //Coluna
                            {
                                autentic[i, j] = Convert.ToString(imports.retornaSet().Tables[0].Rows[i][j]);
                            }
                        }
                        imports.fechaConexao();
                        for (int i = 0; i < autentic.GetLength(0); i++)//Linha
                        {
                            conector_autentica_nf_baixa(autentic[i, 0], autentic[i, 1]);
                        }
                    }
                }
                imports.fechaConexao();
            }
        }
        public void conector_autentica_nf_baixa(string chave, string store)
        {
            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                imports.singleTransaction("update nf tab set tab.importCarga=1 where tab.nf=?chave and tab.importCarga=0 and tab.statusNf=1 and tab.loja=?store");
                imports.addParametro("?chave", chave);
                imports.addParametro("?store", store);
                imports.procedimentoRead();
                if (imports.retornaRead().Read() == true)
                {
                }

            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                }
                imports.fechaConexao();
            }
        }
        protected bool conectorWEB_replace_nf(string replace_nf,
                 string replace_loja,
                 string replace_idcliente,
                 string replace_idparamentro,
                 string replace_idtransportadora,
                 string replace_cfop,
                 string replace_idFuncionario,
                 string replace_idusuario,
                 string replace_idpedido,
                 string replace_nr_nota,
                 string replace_serie,
                 string replace_acrescimo,
                 string replace_baseIcms,
                 string replace_baseIcmsIsento,
                 string replace_valorIcmsSubstituicao,
                 string replace_baseCalculoIcmsSubstituicao,
                 string replace_baseIPI,
                 string replace_baseCofins,
                 string replace_basePis,
                 string replace_emissao,
                 string replace_saida,
                 string replace_alteracao,
                 string replace_hora,
                 string replace_desconto,
                 string replace_uf,
                 string replace_itens,
                 string replace_seguro,
                 string replace_frete,
                 string replace_typeFrete,
                 string replace_valorIcms,
                 string replace_valorIpi,
                 string replace_valorPis,
                 string replace_valorCofins,
                 string replace_acrecismoValor,
                 string replace_descontoValor,
                 string replace_valorTotalLiquido,
                 string replace_valorTotalNota,
                 string replace_valorTotalProdutos,
                 string replace_volumes,
                 string replace_peso,
                 string replace_contribuicaoSocial,
                 string replace_quantidadePedido,
                 string replace_quantidadeRecebida,
                 string replace_impresso,
                 string replace_nr_impressao,
                 string replace_idTable_Codigo,
                 string replace_modNotaFiscal,
                 string replace_idSituacaoFiscal,
                 string replace_emitiNfe,
                 string replace_typenf,
                 string replace_msg01,
                 string replace_msg02,
                 string replace_msg03,
                 string replace_valorTotaServico,
                 string replace_nr_nota_entrada,
                 string replace_serie_entrada,
                 string replace_statusNf,
                 string replace_restituicao,
                 string replace_iss,
                 string replace_impostoRenda,
                 string replace_funrural,
                 string replace_valorTotalFunrural,
                 string replace_geraDanfe,
                 string replace_condPgto,
                 string replace_chave_nfe,
                 string replace_protocolo,
                 string replace_motivo,
                 string replace_versaoNfe,
                 string replace_dataHoraRecbNfe)
        {
            bool valida = false;

            try
            {
                auxConsistencia = 0;
                exports.abreConexao();
                exports.startTransaction("conectorWEB_replace_nf");
                 exports.addParametro("replace_nf", replace_nf);
                 exports.addParametro("replace_loja", replace_loja);
                 exports.addParametro("replace_idcliente", replace_idcliente);
                 exports.addParametro("replace_idparamentro", replace_idparamentro);
                 exports.addParametro("replace_idtransportadora", replace_idtransportadora);
                 exports.addParametro("replace_cfop", replace_cfop);
                 exports.addParametro("replace_idFuncionario", replace_idFuncionario);
                 exports.addParametro("replace_idusuario", replace_idusuario);
                 exports.addParametro("replace_idpedido", replace_idpedido);
                 exports.addParametro("replace_nr_nota", replace_nr_nota);
                 exports.addParametro("replace_serie", replace_serie);
                 exports.addParametro("replace_acrescimo", replace_acrescimo.Replace(",", "."));
                 exports.addParametro("replace_baseIcms", replace_baseIcms.Replace(",", "."));
                 exports.addParametro("replace_baseIcmsIsento", replace_baseIcmsIsento.Replace(",", "."));
                 exports.addParametro("replace_valorIcmsSubstituicao", replace_valorIcmsSubstituicao.Replace(",", "."));
                 exports.addParametro("replace_baseCalculoIcmsSubstituicao", replace_baseCalculoIcmsSubstituicao.Replace(",", "."));
                 exports.addParametro("replace_baseIPI", replace_baseIPI.Replace(",", "."));
                 exports.addParametro("replace_baseCofins", replace_baseCofins.Replace(",", "."));
                 exports.addParametro("replace_basePis", replace_basePis.Replace(",", "."));
                 exports.addParametro("replace_emissao", String.Format("{0:yyyyMMdd}", Convert.ToDateTime(replace_emissao)));
                 exports.addParametro("replace_saida", String.Format("{0:yyyyMMdd}", Convert.ToDateTime(replace_saida)));
                 exports.addParametro("replace_alteracao", String.Format("{0:yyyyMMdd}", Convert.ToDateTime(replace_alteracao)));
                 exports.addParametro("replace_hora", replace_hora);
                 exports.addParametro("replace_desconto", replace_desconto.Replace(",", "."));
                 exports.addParametro("replace_uf", replace_uf);
                 exports.addParametro("replace_itens", replace_itens.Replace(",", "."));
                 exports.addParametro("replace_seguro", replace_seguro.Replace(",", "."));
                 exports.addParametro("replace_frete", replace_frete.Replace(",", "."));
                 exports.addParametro("replace_typeFrete", replace_typeFrete);
                 exports.addParametro("replace_valorIcms", replace_valorIcms.Replace(",", "."));
                 exports.addParametro("replace_valorIpi", replace_valorIpi.Replace(",", "."));
                 exports.addParametro("replace_valorPis", replace_valorPis.Replace(",", "."));
                 exports.addParametro("replace_valorCofins", replace_valorCofins.Replace(",", "."));
                 exports.addParametro("replace_acrecismoValor", replace_acrecismoValor.Replace(",", "."));
                 exports.addParametro("replace_descontoValor", replace_descontoValor.Replace(",", "."));
                 exports.addParametro("replace_valorTotalLiquido", replace_valorTotalLiquido.Replace(",", "."));
                 exports.addParametro("replace_valorTotalNota", replace_valorTotalNota.Replace(",", "."));
                 exports.addParametro("replace_valorTotalProdutos", replace_valorTotalProdutos.Replace(",", "."));
                 exports.addParametro("replace_volumes", replace_volumes.Replace(",", "."));
                 exports.addParametro("replace_peso", replace_peso.Replace(",", "."));
                 exports.addParametro("replace_contribuicaoSocial", replace_contribuicaoSocial.Replace(",", "."));
                 exports.addParametro("replace_quantidadePedido", replace_quantidadePedido.Replace(",", "."));
                 exports.addParametro("replace_quantidadeRecebida", replace_quantidadeRecebida.Replace(",", "."));
                 exports.addParametro("replace_impresso", replace_impresso.Replace(",", "."));
                 exports.addParametro("replace_nr_impressao", replace_nr_impressao.Replace(",", "."));
                 exports.addParametro("replace_idTable_Codigo", replace_idTable_Codigo.Replace(",", "."));
                 exports.addParametro("replace_modNotaFiscal", replace_modNotaFiscal);
                 exports.addParametro("replace_idSituacaoFiscal", replace_idSituacaoFiscal);
                 exports.addParametro("replace_emitiNfe", replace_emitiNfe);
                 exports.addParametro("replace_typenf", replace_typenf);
                 exports.addParametro("replace_msg01", replace_msg01);
                 exports.addParametro("replace_msg02", replace_msg02);
                 exports.addParametro("replace_msg03", replace_msg03);
                 exports.addParametro("replace_valorTotaServico", replace_valorTotaServico.Replace(",", "."));
                 exports.addParametro("replace_nr_nota_entrada", replace_nr_nota_entrada.Replace(",", "."));
                 exports.addParametro("replace_serie_entrada", replace_serie_entrada.Replace(",", "."));
                 exports.addParametro("replace_statusNf", replace_statusNf);
                 exports.addParametro("replace_restituicao", replace_restituicao.Replace(",", "."));
                 exports.addParametro("replace_iss", replace_iss.Replace(",", "."));
                 exports.addParametro("replace_impostoRenda", replace_impostoRenda.Replace(",", "."));
                 exports.addParametro("replace_funrural", replace_funrural.Replace(",", "."));
                 exports.addParametro("replace_valorTotalFunrural", replace_valorTotalFunrural.Replace(",", "."));
                 exports.addParametro("replace_geraDanfe", replace_geraDanfe);
                 exports.addParametro("replace_condPgto", replace_condPgto);
                 exports.addParametro("replace_chave_nfe", replace_chave_nfe);
                 exports.addParametro("replace_protocolo", replace_protocolo);
                 exports.addParametro("replace_motivo", replace_motivo);
                 exports.addParametro("replace_versaoNfe", replace_versaoNfe);
                 if (replace_dataHoraRecbNfe == "")
                 {
                     exports.addParametro("replace_dataHoraRecbNfe", String.Format("{0:yyyyMMdd}", DateTime.Now));
                 }
                 else
                 {
                     exports.addParametro("replace_dataHoraRecbNfe", String.Format("{0:yyyyMMdd}", Convert.ToDateTime(replace_dataHoraRecbNfe)));
                 }
                 
                exports.procedimentoRead();
                /*if (exports.retornaRead().Read() == true)
                {
                    valida = true;
                }*/
                valida = true;
                exports.fechaRead();
                //exports.commit();
            }
            catch (Exception erro)
            {
                valida = false;
                auxConsistencia = 1;
            }
            finally
            {
                exports.fechaConexao();
                if (auxConsistencia == 0)
                {
                }
            }
            return valida;
        }


        protected bool conectorWEB_replace_nfItem(string replace_idnfItem, string replace_idnf,
                string replace_idProduto,
                string replace_valorLiquido,
                string replace_priceOriginal,
                string replace_priceVenda,
                string replace_priceCusto,
                string replace_estoque,
                string replace_data,
                string replace_peso,
                string replace_aliquota,
                string replace_icms,
                string replace_baseCalculo,
                string replace_reducao,
                string replace_quantidade,
                string replace_idunidadeMedida,
                string replace_cfop,
                string replace_cstIcms,
                string replace_cstPis,
                string replace_valorPis,
                string replace_basePis,
                string replace_cstCofins,
                string replace_valorCofins,
                string replace_baseCofins,
                string replace_cstIpi,
                string replace_ipi,
                string replace_ipiValor,
                string replace_valorIpi,
                string replace_baseIpi,
                string replace_desconto,
                string replace_descontoValor,
                string replace_acrescimo,
                string replace_acrescimoValor,
                string replace_aliquotaIcmsSt,
                string replace_baseCalculoIcmsSubstituicao,
                string replace_valorIcmsSubstituicao,
                string replace_reducaoIcmsSt,
                string replace_margem,
                string replace_valorTotalProduto,
                string replace_valorTotalNota,
                string replace_valorTotalLiquido,
                string replace_fornecedor,
                string replace_idsetor,
                string replace_tributacao,
                string replace_frete,
                string replace_valorFrete,
                string replace_typeAliquota,
                string replace_chaveEntrada,
                string replace_sequencia,
                string replace_seguro,
                string replace_idGenero,
                string replace_origemMercadoria,
                string replace_quantidadeCanc)
        {
            bool valida = false;

            try
            {
                auxConsistencia = 0;
                exports.abreConexao();
                exports.startTransaction("conectorWEB_replace_nfItem");
                exports.addParametro("replace_idnfItem", replace_idnfItem);
                exports.addParametro("replace_idnf", replace_idnf);
                exports.addParametro("replace_idProduto", replace_idProduto);
                exports.addParametro("replace_valorLiquido", replace_valorLiquido.Replace(",", "."));
                exports.addParametro("replace_priceOriginal", replace_priceOriginal.Replace(",", "."));
                exports.addParametro("replace_priceVenda", replace_priceVenda.Replace(",", "."));
                exports.addParametro("replace_priceCusto", replace_priceCusto.Replace(",", "."));
                exports.addParametro("replace_estoque", replace_estoque.Replace(",", "."));
                exports.addParametro("replace_data", String.Format("{0:yyyyMMdd}", Convert.ToDateTime(replace_data)));
                exports.addParametro("replace_peso", replace_peso.Replace(",", "."));
                exports.addParametro("replace_aliquota", replace_aliquota.Replace(",", "."));
                exports.addParametro("replace_icms", replace_icms.Replace(",", "."));
                exports.addParametro("replace_baseCalculo", replace_baseCalculo.Replace(",", "."));
                exports.addParametro("replace_reducao", replace_reducao.Replace(",", "."));
                exports.addParametro("replace_quantidade", replace_quantidade.Replace(",", "."));
                exports.addParametro("replace_idunidadeMedida", replace_idunidadeMedida);
                exports.addParametro("replace_cfop", replace_cfop);
                exports.addParametro("replace_cstIcms", replace_cstIcms);
                exports.addParametro("replace_cstPis", replace_cstPis);
                exports.addParametro("replace_valorPis", replace_valorPis.Replace(",", "."));
                exports.addParametro("replace_basePis", replace_basePis.Replace(",", "."));
                exports.addParametro("replace_cstCofins", replace_cstCofins);
                exports.addParametro("replace_valorCofins", replace_valorCofins.Replace(",", "."));
                exports.addParametro("replace_baseCofins", replace_baseCofins.Replace(",", "."));
                exports.addParametro("replace_cstIpi", replace_cstIpi);
                exports.addParametro("replace_ipi", replace_ipi);
                exports.addParametro("replace_ipiValor", replace_ipiValor.Replace(",", "."));
                exports.addParametro("replace_valorIpi", replace_valorIpi.Replace(",", "."));
                exports.addParametro("replace_baseIpi", replace_baseIpi.Replace(",", "."));
                exports.addParametro("replace_desconto", replace_desconto.Replace(",", "."));
                exports.addParametro("replace_descontoValor", replace_descontoValor.Replace(",", "."));
                exports.addParametro("replace_acrescimo", replace_acrescimo.Replace(",", "."));
                exports.addParametro("replace_acrescimoValor", replace_acrescimoValor.Replace(",", "."));
                exports.addParametro("replace_aliquotaIcmsSt", replace_aliquotaIcmsSt.Replace(",", "."));
                exports.addParametro("replace_baseCalculoIcmsSubstituicao", replace_baseCalculoIcmsSubstituicao.Replace(",", "."));
                exports.addParametro("replace_valorIcmsSubstituicao", replace_valorIcmsSubstituicao.Replace(",", "."));
                exports.addParametro("replace_reducaoIcmsSt", replace_reducaoIcmsSt.Replace(",", "."));
                exports.addParametro("replace_margem", replace_margem.Replace(",", "."));
                exports.addParametro("replace_valorTotalProduto", replace_valorTotalProduto.Replace(",", "."));
                exports.addParametro("replace_valorTotalNota", replace_valorTotalNota.Replace(",", "."));
                exports.addParametro("replace_valorTotalLiquido", replace_valorTotalLiquido.Replace(",", "."));
                exports.addParametro("replace_fornecedor", replace_fornecedor);
                exports.addParametro("replace_idsetor", replace_idsetor);
                exports.addParametro("replace_tributacao", replace_tributacao.Replace(",", "."));
                exports.addParametro("replace_frete", replace_frete.Replace(",", "."));
                exports.addParametro("replace_valorFrete", replace_valorFrete.Replace(",", "."));
                exports.addParametro("replace_typeAliquota", replace_typeAliquota);
                exports.addParametro("replace_chaveEntrada", replace_chaveEntrada);
                exports.addParametro("replace_sequencia", replace_sequencia);
                exports.addParametro("replace_seguro", replace_seguro.Replace(",", "."));
                exports.addParametro("replace_idGenero", replace_idGenero);
                exports.addParametro("replace_origemMercadoria", replace_origemMercadoria);
                exports.addParametro("replace_quantidadeCanc", replace_quantidadeCanc.Replace(",", "."));
                exports.procedimentoRead();
                /*if (exports.retornaRead().Read() == true)
                {
                    valida = true;
                }*/
                valida = true;
                exports.fechaRead();
                //exports.commit();
            }
            catch (Exception erro)
            {
                valida = false;
                auxConsistencia = 1;
            }
            finally
            {
                exports.fechaConexao();
                if (auxConsistencia == 0)
                {
                }
            }
            return valida;
        }


        protected bool conectorWeb_replace_nfimposto(string replace_nfImposto,
                                                      string replace_idnf,
                                                      string replace_cfop,
                                                      string replace_cstIcms,
                                                      string replace_aliquota,
                                                      string replace_imposto,
                                                      string replace_baseIcmsIsentos,
                                                      string replace_reducao,
                                                      string replace_baseCalculo,
                                                      string replace_icms,
                                                      string replace_baseCalculoIcmsSt,
                                                      string replace_icmsSt,
                                                      string replace_valorIpi,
                                                      string replace_typeAliquota,
                                                      string replace_valor)
        {
            bool valida = false;

            try
            {
                auxConsistencia = 0;
                exports.abreConexao();
                exports.startTransaction("conectorWeb_replace_nfimposto");
                exports.addParametro("replace_nfImposto", replace_nfImposto);
                exports.addParametro("replace_idnf", replace_idnf);
                exports.addParametro("replace_cfop", replace_cfop);
                exports.addParametro("replace_cstIcms", replace_cstIcms);
                exports.addParametro("replace_aliquota", replace_aliquota.Replace(",", "."));
                exports.addParametro("replace_imposto", replace_imposto.Replace(",", "."));
                exports.addParametro("replace_baseIcmsIsentos", replace_baseIcmsIsentos.Replace(",", "."));
                exports.addParametro("replace_reducao", replace_reducao.Replace(",", "."));
                exports.addParametro("replace_baseCalculo", replace_baseCalculo.Replace(",", "."));
                exports.addParametro("replace_icms", replace_icms.Replace(",", "."));
                exports.addParametro("replace_baseCalculoIcmsSt", replace_baseCalculoIcmsSt.Replace(",", "."));
                exports.addParametro("replace_icmsSt", replace_icmsSt.Replace(",", "."));
                exports.addParametro("replace_valorIpi", replace_valorIpi.Replace(",", "."));
                exports.addParametro("replace_typeAliquota", replace_typeAliquota);
                exports.addParametro("replace_valor", replace_valor.Replace(",", "."));
                exports.procedimentoRead();
                /*if (exports.retornaRead().Read() == true)
                {
                    valida = true;
                }*/
                valida = true;
                exports.fechaRead();
                //exports.commit();
            }
            catch (Exception erro)
            {
                valida = false;
                auxConsistencia = 1;
            }
            finally
            {
                exports.fechaConexao();
                if (auxConsistencia == 0)
                {
                }
            }
            return valida;
        }

        protected bool conectorWEB_replace_nfpgto(string replace_idNf,
              string replace_sequencia,
              string replace_idfinalizadora,
              string replace_valor,
              string replace_desconto,
              string replace_acrescimo,
              string replace_valorDesconto,
              string replace_valorAcrescimo,
              string replace_troco,
              string replace_card_CNPJ,
              string replace_card_bandeira,
              string replace_card_aut,
              string replace_funcionario,
              string replace_metodo)
        {
            bool valida = false;

            try
            {
                auxConsistencia = 0;
                exports.abreConexao();
                exports.startTransaction("conectorWEB_replace_nfpgto");
                exports.addParametro("replace_idNf", replace_idNf);
                exports.addParametro("replace_sequencia", replace_sequencia);
                exports.addParametro("replace_idfinalizadora", replace_idfinalizadora);
                exports.addParametro("replace_valor", replace_valor.Replace(",", "."));
                exports.addParametro("replace_desconto", replace_desconto.Replace(",", "."));
                exports.addParametro("replace_acrescimo", replace_acrescimo.Replace(",", "."));
                exports.addParametro("replace_valorDesconto", replace_valorDesconto.Replace(",", "."));
                exports.addParametro("replace_valorAcrescimo", replace_valorAcrescimo.Replace(",", "."));
                exports.addParametro("replace_troco", replace_troco.Replace(",", "."));
                exports.addParametro("replace_card_CNPJ", replace_card_CNPJ);
                exports.addParametro("replace_card_bandeira", replace_card_bandeira);
                exports.addParametro("replace_card_aut", replace_card_aut);
                exports.addParametro("replace_funcionario", replace_funcionario);
                exports.addParametro("replace_metodo", replace_metodo);

                exports.procedimentoRead();
                /*if (exports.retornaRead().Read() == true)
                {
                    valida = true;
                }*/
                valida = true;
                exports.fechaRead();
                //exports.commit();
            }
            catch (Exception erro)
            {
                valida = false;
                auxConsistencia = 1;
            }
            finally
            {
                exports.fechaConexao();
                if (auxConsistencia == 0)
                {
                }
            }
            return valida;
        }
        #endregion

        #region //Transmissao Pessoas

        public void conector_pessoa()
        {
            string[,] vetor = new string[0, 0];
            string[,] recargaCliente = new string[0, 0]; //Reserva
            string[,] recargaFisica = new string[0, 0]; //PedidoItens
            string[,] recargaRural; //PedidoFinanceiro
            string[,] recargaJuridica;
            string[,] recargaFone;
            string[,] recargaEndereco;
            string[,] recargaCobranca;
            string[,] recargaEntrega;
            string[,] recargaRisco;
            string[,] recargaProfissional;
            string[,] recargaReferencia;
            string[,] recargaFornecedorComercial;
            string[,] recargaFornecedorFiscal;
            string[,] recargaFornecedorInformacao;

            //Cadastro de Cliente
            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                imports.singleTransaction("select tab.* from cliente tab where tab.importCarga=0 and tab.idCliente=?store");
                imports.addParametro("?store", store);
                imports.procedimentoSet();
            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = imports.retornaSet().Tables[0].Columns.Count;
                    countRows = imports.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        vetor = new string[countRows, 3];
                        recargaCliente = new string[countRows, countField];
                        for (int i = 0; i < countRows; i++)//Linha
                        {
                            for (int j = 0; j < countField; j++) //Coluna
                            {
                                recargaCliente[i, j] = Convert.ToString(imports.retornaSet().Tables[0].Rows[i][j]);
                            }
                        }
                        imports.fechaConexao();
                        for (int i = 0; i < recargaCliente.GetLength(0); i++)//Linha
                        {
                            if (conectorPDV_replace_cliente(recargaCliente[i, 0], recargaCliente[i, 1], recargaCliente[i, 2], recargaCliente[i, 3], recargaCliente[i, 4], recargaCliente[i, 5], recargaCliente[i, 6], recargaCliente[i, 7], recargaCliente[i, 8], recargaCliente[i, 9], recargaCliente[i, 10], recargaCliente[i, 11], recargaCliente[i, 12], recargaCliente[i, 13]) == 0)
                            {
                                vetor[i, 0] = recargaCliente[i, 0];
                                vetor[i, 1] = recargaCliente[i, 1];
                                vetor[i, 2] = "#";
                            }
                        }
                    }
                }
                imports.fechaConexao();
            }

            //Cadastro de cliente fisica
            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                imports.singleTransaction("select tab1.* from cliente tab  inner join fisica tab1 on(tab.idCliente = tab1.idCliente) where tab.importCarga=0");
                imports.addParametro("?store", store);
                imports.procedimentoSet();
            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = imports.retornaSet().Tables[0].Columns.Count;
                    countRows = imports.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        recargaFisica = new string[countRows, countField];
                        for (int i = 0; i < countRows; i++)//Linha
                        {
                            for (int j = 0; j < countField; j++) //Coluna
                            {
                                recargaFisica[i, j] = Convert.ToString(imports.retornaSet().Tables[0].Rows[i][j]);
                            }
                        }
                        imports.fechaConexao();
                        for (int i = 0; i < recargaFisica.GetLength(0); i++)//linha
                        {
                            if (conectorPDV_replace_fisica(recargaFisica[i, 0], recargaFisica[i, 1], recargaFisica[i, 2], recargaFisica[i, 3], recargaFisica[i, 4], recargaFisica[i, 5], recargaFisica[i, 6], recargaFisica[i, 7]) == 0)
                            {
                                for (int j = 0; j < vetor.GetLength(0); j++)
                                {
                                    if (vetor[j, 0] == recargaFisica[i, 0])
                                    {
                                        vetor[j, 2] = "*";
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                imports.fechaConexao();
            }

            //Cadastro de cliente juridica
            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                imports.singleTransaction("select tab1.* from cliente tab  inner join juridica tab1 on(tab.idCliente = tab1.idCliente) where tab.importCarga=0  and tab.idLoja=?store");
                imports.addParametro("?store", store);
                imports.procedimentoSet();
            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = imports.retornaSet().Tables[0].Columns.Count;
                    countRows = imports.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        recargaJuridica = new string[countRows, countField];
                        for (int i = 0; i < countRows; i++)//Linha
                        {
                            for (int j = 0; j < countField; j++) //Coluna
                            {
                                recargaJuridica[i, j] = Convert.ToString(imports.retornaSet().Tables[0].Rows[i][j]);
                            }
                        }
                        imports.fechaConexao();
                        for (int i = 0; i < recargaJuridica.GetLength(0); i++)//linha
                        {
                            if (conectorPDV_replace_juridica(recargaJuridica[i, 0], recargaJuridica[i, 1], recargaJuridica[i, 2], recargaJuridica[i, 3], recargaJuridica[i, 4], recargaJuridica[i, 5], recargaJuridica[i, 6], recargaJuridica[i, 7]) == 0)
                            {
                                for (int j = 0; j < vetor.GetLength(0); j++)
                                {
                                    if (vetor[j, 0] == recargaJuridica[i, 0])
                                    {
                                        vetor[j, 2] = "*";
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                imports.fechaConexao();
            }
            //Cadastro de cliente rural
            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                imports.singleTransaction("select tab1.* from cliente tab  inner join rural tab1 on(tab.idCliente = tab1.idCliente) where tab.importCarga=0 and tab.idLoja=?store");
                imports.addParametro("?store", store);
                imports.procedimentoSet();
            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = imports.retornaSet().Tables[0].Columns.Count;
                    countRows = imports.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        recargaRural = new string[countRows, countField];
                        for (int i = 0; i < countRows; i++)//Linha
                        {
                            for (int j = 0; j < countField; j++) //Coluna
                            {
                                recargaRural[i, j] = Convert.ToString(imports.retornaSet().Tables[0].Rows[i][j]);
                            }
                        }
                        imports.fechaConexao();
                        for (int i = 0; i < recargaRural.GetLength(0); i++)//linha
                        {
                            if (conectorPDV_replace_rural(recargaRural[i, 0], recargaRural[i, 1], recargaRural[i, 2], recargaRural[i, 3], recargaRural[i, 4], recargaRural[i, 5], recargaRural[i, 6], recargaRural[i, 7], recargaRural[i, 8]) == 0)
                            {
                                for (int j = 0; j < vetor.GetLength(0); j++)
                                {
                                    if (vetor[j, 0] == recargaRural[i, 0])
                                    {
                                        vetor[j, 2] = "*";
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                imports.fechaConexao();
            }

            //Cadastro de endereco
            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                imports.singleTransaction("select tab1.* from cliente tab  inner join endereco tab1 on(tab.idCliente = tab1.idCliente) where tab.importCarga=0 and tab.idLoja=?store");
                imports.addParametro("?store", store);
                imports.procedimentoSet();
            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = imports.retornaSet().Tables[0].Columns.Count;
                    countRows = imports.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        recargaEndereco = new string[countRows, countField];
                        for (int i = 0; i < countRows; i++)//Linha
                        {
                            for (int j = 0; j < countField; j++) //Coluna
                            {
                                recargaEndereco[i, j] = Convert.ToString(imports.retornaSet().Tables[0].Rows[i][j]);
                            }
                        }
                        imports.fechaConexao();
                        for (int i = 0; i < recargaEndereco.GetLength(0); i++)//linha
                        {
                            if (conectorPDV_replace_endereco(recargaEndereco[i, 0], recargaEndereco[i, 1], recargaEndereco[i, 2], recargaEndereco[i, 3], recargaEndereco[i, 4], recargaEndereco[i, 5], recargaEndereco[i, 6], recargaEndereco[i, 7], recargaEndereco[i, 8], recargaEndereco[i, 9], recargaEndereco[i, 10], recargaEndereco[i, 11]) == 0)
                            {
                            }
                        }
                    }
                }
                imports.fechaConexao();
            }
            //Cadastro de Fone
            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                imports.singleTransaction("select tab1.* from cliente tab  inner join fone tab1 on(tab.idCliente = tab1.idCliente) where tab.importCarga=0 and tab.idLoja=?store");
                imports.addParametro("?store", store);
                imports.procedimentoSet();
            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = imports.retornaSet().Tables[0].Columns.Count;
                    countRows = imports.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        recargaFone = new string[countRows, countField];
                        for (int i = 0; i < countRows; i++)//Linha
                        {
                            for (int j = 0; j < countField; j++) //Coluna
                            {
                                recargaFone[i, j] = Convert.ToString(imports.retornaSet().Tables[0].Rows[i][j]);
                            }
                        }
                        imports.fechaConexao();
                        for (int i = 0; i < recargaFone.GetLength(0); i++)//linha
                        {
                            if (conectorPDV_replace_fone(recargaFone[i, 0], recargaFone[i, 1], recargaFone[i, 2], recargaFone[i, 3], recargaFone[i, 4], recargaFone[i, 5], recargaFone[i, 6], recargaFone[i, 7], recargaFone[i, 8]) == 0)
                            {
                            }
                        }
                    }
                }
                imports.fechaConexao();
            }

            //Cadastro de Cobranca
            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                imports.singleTransaction("select tab1.* from cliente tab  inner join clienteCobranca tab1 on(tab.idCliente = tab1.idCliente) where tab.importCarga=0 and tab.idLoja=?store");
                imports.addParametro("?store", store);
                imports.procedimentoSet();
            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = imports.retornaSet().Tables[0].Columns.Count;
                    countRows = imports.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        recargaCobranca = new string[countRows, countField];
                        for (int i = 0; i < countRows; i++)//Linha
                        {
                            for (int j = 0; j < countField; j++) //Coluna
                            {
                                recargaCobranca[i, j] = Convert.ToString(imports.retornaSet().Tables[0].Rows[i][j]);
                            }
                        }
                        imports.fechaConexao();
                        for (int i = 0; i < recargaCobranca.GetLength(0); i++)//linha
                        {
                            if (conectorPDV_replace_clienteCobranca(recargaCobranca[i, 0], recargaCobranca[i, 1], recargaCobranca[i, 2], recargaCobranca[i, 3], recargaCobranca[i, 4]) == 0)
                            {
                            }
                        }
                    }
                }
                imports.fechaConexao();
            }

            //Cadastro de Entrega
            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                imports.singleTransaction("select tab1.* from cliente tab  inner join clienteEntrega tab1 on(tab.idCliente = tab1.idCliente) where tab.importCarga=0 and tab.idLoja=?store");
                imports.addParametro("?store", store);
                imports.procedimentoSet();
            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = imports.retornaSet().Tables[0].Columns.Count;
                    countRows = imports.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        recargaEntrega = new string[countRows, countField];
                        for (int i = 0; i < countRows; i++)//Linha
                        {
                            for (int j = 0; j < countField; j++) //Coluna
                            {
                                recargaEntrega[i, j] = Convert.ToString(imports.retornaSet().Tables[0].Rows[i][j]);
                            }
                        }
                        imports.fechaConexao();
                        for (int i = 0; i < recargaEntrega.GetLength(0); i++)//linha
                        {
                            if (conectorPDV_replace_clienteEntrega(recargaEntrega[i, 0], recargaEntrega[i, 1], recargaEntrega[i, 2], recargaEntrega[i, 3], recargaEntrega[i, 4]) == 0)
                            {
                            }
                        }
                    }
                }
                imports.fechaConexao();
            }


            //Cadastro de Risco
            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                imports.singleTransaction("select tab1.* from cliente tab  inner join clienteRisco tab1 on(tab.idCliente = tab1.idCliente) where tab.importCarga=0 and tab.idLoja=?store");
                imports.addParametro("?store", store);
                imports.procedimentoSet();
            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = imports.retornaSet().Tables[0].Columns.Count;
                    countRows = imports.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        recargaRisco = new string[countRows, countField];
                        for (int i = 0; i < countRows; i++)//Linha
                        {
                            for (int j = 0; j < countField; j++) //Coluna
                            {
                                recargaRisco[i, j] = Convert.ToString(imports.retornaSet().Tables[0].Rows[i][j]);
                            }
                        }
                        imports.fechaConexao();
                        for (int i = 0; i < recargaRisco.GetLength(0); i++)//linha
                        {
                            if (conectorPDV_replace_clienteRisco(recargaRisco[i, 0], recargaRisco[i, 1], recargaRisco[i, 2], recargaRisco[i, 3], recargaRisco[i, 4], recargaRisco[i, 5], recargaRisco[i, 6], recargaRisco[i, 7], recargaRisco[i, 8], recargaRisco[i, 9], recargaRisco[i, 10], recargaRisco[i, 11], recargaRisco[i, 12], recargaRisco[i, 13], recargaRisco[i, 14], recargaRisco[i, 15]) == 0)
                            {
                            }
                        }
                    }
                }
                imports.fechaConexao();
            }
            //Cadastro de Profissional
            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                imports.singleTransaction("select tab1.* from cliente tab  inner join clienteProfissional tab1 on(tab.idCliente = tab1.idCliente) where tab.importCarga=0 and tab.idLoja=?store");
                imports.addParametro("?store", store);
                imports.procedimentoSet();
            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = imports.retornaSet().Tables[0].Columns.Count;
                    countRows = imports.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        recargaProfissional = new string[countRows, countField];
                        for (int i = 0; i < countRows; i++)//Linha
                        {
                            for (int j = 0; j < countField; j++) //Coluna
                            {
                                recargaProfissional[i, j] = Convert.ToString(imports.retornaSet().Tables[0].Rows[i][j]);
                            }
                        }
                        imports.fechaConexao();
                        for (int i = 0; i < recargaProfissional.GetLength(0); i++)//linha
                        {
                            if (conectorPDV_replace_clienteProfissional(recargaProfissional[i, 0], recargaProfissional[i, 1], recargaProfissional[i, 2], recargaProfissional[i, 3], recargaProfissional[i, 4], recargaProfissional[i, 5], recargaProfissional[i, 6], recargaProfissional[i, 7], recargaProfissional[i, 8]) == 0)
                            {
                            }
                        }
                    }
                }
                imports.fechaConexao();
            }

            //Cadastro de Referencia
            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                imports.singleTransaction("select tab1.* from cliente tab  inner join clienteReferencia tab1 on(tab.idCliente = tab1.idCliente) where tab.importCarga=0 and tab.idLoja=?store");
                imports.addParametro("?store", store);
                imports.procedimentoSet();
            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = imports.retornaSet().Tables[0].Columns.Count;
                    countRows = imports.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        recargaReferencia = new string[countRows, countField];
                        for (int i = 0; i < countRows; i++)//Linha
                        {
                            for (int j = 0; j < countField; j++) //Coluna
                            {
                                recargaReferencia[i, j] = Convert.ToString(imports.retornaSet().Tables[0].Rows[i][j]);
                            }
                        }
                        imports.fechaConexao();
                        for (int i = 0; i < recargaReferencia.GetLength(0); i++)//linha
                        {
                            if (conectorPDV_replace_clienteReferencia(recargaReferencia[i, 0], recargaReferencia[i, 1], recargaReferencia[i, 2], recargaReferencia[i, 3], recargaReferencia[i, 4], recargaReferencia[i, 5], recargaReferencia[i, 6], recargaReferencia[i, 7], recargaReferencia[i, 8], recargaReferencia[i, 9]) == 0)
                            {
                            }
                        }
                    }
                }
                imports.fechaConexao();
            }
            //Cadastro de Fornecedor Comercial
            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                imports.singleTransaction("select tab1.* from cliente tab  inner join paramentro_fornecedor_comercial tab1 on(tab.idCliente = tab1.idCliente) where tab.importCarga=0 and tab.idLoja=?store");
                imports.addParametro("?store", store);
                imports.procedimentoSet();
            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = imports.retornaSet().Tables[0].Columns.Count;
                    countRows = imports.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        recargaFornecedorComercial = new string[countRows, countField];
                        for (int i = 0; i < countRows; i++)//Linha
                        {
                            for (int j = 0; j < countField; j++) //Coluna
                            {
                                recargaFornecedorComercial[i, j] = Convert.ToString(imports.retornaSet().Tables[0].Rows[i][j]);
                            }
                        }
                        imports.fechaConexao();
                        for (int i = 0; i < recargaFornecedorComercial.GetLength(0); i++)//linha
                        {
                            if (conectorWeb_replace_paramentro_fornecedor_comercial(recargaFornecedorComercial[i, 0], recargaFornecedorComercial[i, 1], recargaFornecedorComercial[i, 2], recargaFornecedorComercial[i, 3], recargaFornecedorComercial[i, 4], recargaFornecedorComercial[i, 5], recargaFornecedorComercial[i, 6], recargaFornecedorComercial[i, 7], recargaFornecedorComercial[i, 8], recargaFornecedorComercial[i, 9], recargaFornecedorComercial[i, 10], recargaFornecedorComercial[i, 11]) == 0)
                            {
                            }
                        }
                    }
                }
                imports.fechaConexao();
            }

            //Cadastro de Fornecedor fiscal
            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                imports.singleTransaction("select tab1.* from cliente tab  inner join paramentro_fornecedor_fiscal tab1 on(tab.idCliente = tab1.idCliente) where tab.importCarga=0 and tab.idLoja=?store");
                imports.addParametro("?store", store);
                imports.procedimentoSet();
            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = imports.retornaSet().Tables[0].Columns.Count;
                    countRows = imports.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        recargaFornecedorFiscal = new string[countRows, countField];
                        for (int i = 0; i < countRows; i++)//Linha
                        {
                            for (int j = 0; j < countField; j++) //Coluna
                            {
                                recargaFornecedorFiscal[i, j] = Convert.ToString(imports.retornaSet().Tables[0].Rows[i][j]);
                            }
                        }
                        imports.fechaConexao();
                        for (int i = 0; i < recargaFornecedorFiscal.GetLength(0); i++)//linha
                        {
                            if (conectorWEB_replace_paramentro_fornecedor_fiscal(recargaFornecedorFiscal[i, 0], recargaFornecedorFiscal[i, 1], recargaFornecedorFiscal[i, 2], recargaFornecedorFiscal[i, 3], recargaFornecedorFiscal[i, 4], recargaFornecedorFiscal[i, 5], recargaFornecedorFiscal[i, 6]) == 0)
                            {
                            }
                        }
                    }
                }
                imports.fechaConexao();
            }

            //Cadastro de Fornecedor Informacao
            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                imports.singleTransaction("select tab1.* from cliente tab  inner join paramentro_fornecedor_informacao tab1 on(tab.idCliente = tab1.idCliente) where tab.importCarga=0 and tab.idLoja=?store");
                imports.addParametro("?store", store);
                imports.procedimentoSet();
            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = imports.retornaSet().Tables[0].Columns.Count;
                    countRows = imports.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        recargaFornecedorInformacao = new string[countRows, countField];
                        for (int i = 0; i < countRows; i++)//Linha
                        {
                            for (int j = 0; j < countField; j++) //Coluna
                            {
                                recargaFornecedorInformacao[i, j] = Convert.ToString(imports.retornaSet().Tables[0].Rows[i][j]);
                            }
                        }
                        imports.fechaConexao();
                        for (int i = 0; i < recargaFornecedorInformacao.GetLength(0); i++)//linha
                        {
                            if (conectorWeb_replace_paramentro_fornecedor_comercial(recargaFornecedorInformacao[i, 0], recargaFornecedorInformacao[i, 1], recargaFornecedorInformacao[i, 2], recargaFornecedorInformacao[i, 3], recargaFornecedorInformacao[i, 4], recargaFornecedorInformacao[i, 5], recargaFornecedorInformacao[i, 6], recargaFornecedorInformacao[i, 7], recargaFornecedorInformacao[i, 8], recargaFornecedorInformacao[i, 9], recargaFornecedorInformacao[i, 10], recargaFornecedorInformacao[i, 10]) == 0)
                            {
                            }
                        }
                    }
                }
                imports.fechaConexao();
            }
        }

        protected int conectorPDV_replace_cliente(string idCliente,
                    string idLoja,
                    string idTipoPessoa,
                    string idUsuario,
                    string idAtividade,
                    string observacao,
                    string dataEmissao,
                    string dataAlteracao,
                    string idEstado,
                    string uf,
                    string status,
                    string idSpedMunicipio,
                    string idPais,
                    string liberacao)
        {
            try
            {
                auxConsistencia = 0;
                exports.abreConexao();
                exports.startTransaction("conectorPDV_replace_cliente");
                exports.addParametro("replace_idcliente", idCliente);
                exports.addParametro("replace_idloja", idLoja);
                exports.addParametro("replace_idtipoPessoa", idTipoPessoa);
                exports.addParametro("replace_idusuario", idUsuario);
                exports.addParametro("replace_idatividade", idAtividade);
                exports.addParametro("replace_observacao", observacao);
                exports.addParametro("replace_dataEmissao", String.Format("{0:yyyyMMdd}", Convert.ToDateTime(dataEmissao)));
                exports.addParametro("replace_dataAlteracao", String.Format("{0:yyyyMMdd}", Convert.ToDateTime(dataAlteracao)));
                exports.addParametro("replace_idestado", idEstado);
                exports.addParametro("replace_uf", uf);
                exports.addParametro("replace_status", status);
                exports.addParametro("replace_idSpedMunicipio", idSpedMunicipio);
                exports.addParametro("replace_idPais", idPais);
                exports.addParametro("replace_liberacao", liberacao);
                exports.procedimentoRead();
            }
            catch (Exception erro)
            {
                auxConsistencia = 1;
            }
            finally
            {
                exports.fechaConexao(); 
            }
            return auxConsistencia;
        }
        protected int conectorPDV_replace_fisica(string idCliente,
            string cpf,
            string idAtividade,
            string nome,
            string nascimento,
            string idSexo,
            string idEntidade,
            string idCivil)
        {
            try
            {
                auxConsistencia = 0;
                 exports.abreConexao();
                exports.startTransaction("conectorPDV_replace_fisica");
                exports.addParametro("replace_idcliente", idCliente);
                exports.addParametro("replace_cpf", cpf);
                exports.addParametro("replace_idAtividade", idAtividade);
                exports.addParametro("replace_nome", nome);
                exports.addParametro("replace_nascimento", String.Format("{0:yyyyMMdd}", Convert.ToDateTime(nascimento)));
                exports.addParametro("replace_idSexo", idSexo);
                exports.addParametro("replace_idEntidade", idEntidade);
                exports.addParametro("replace_idCivil", idCivil);
                exports.procedimentoRead();
            }
            catch (Exception erro)
            {
                auxConsistencia = 1;
            }
            finally
            {
                exports.fechaConexao(); 
            }
            return auxConsistencia;
        }

        protected int conectorPDV_replace_rural(string idCliente,
                        string cpf,
                        string idAtividade,
                        string nome,
                        string ie,
                        string idEntidade,
                        string nascimento,
                        string idSexo,
                        string idCivil)
        {
            try
            {
                auxConsistencia = 0;
                exports.abreConexao();
                exports.startTransaction("conectorPDV_replace_rural");
                exports.addParametro("replace_idcliente", idCliente);
                exports.addParametro("replace_cpf", cpf);
                exports.addParametro("replace_idAtividade", idAtividade);
                exports.addParametro("replace_nome", nome);
                exports.addParametro("replace_ie", ie);
                exports.addParametro("replace_idEntidade", idEntidade);
                exports.addParametro("replace_nascimento", String.Format("{0:yyyyMMdd}", Convert.ToDateTime(nascimento)));
                exports.addParametro("replace_idSexo", idSexo);
                exports.addParametro("replace_idCivil", idCivil);
                exports.procedimentoRead();
            }
            catch (Exception erro)
            {
                 auxConsistencia = 1;
            }
            finally
            {
                exports.fechaConexao(); 
            }
            return auxConsistencia;
        }
        protected int conectorPDV_replace_juridica(string idCliente,
                        string cnpj,
                        string idAtividade,
                        string razao,
                        string fantasia,
                        string ie,
                        string dataAbertura,
                        string idTipofornecedor)
        {
            try
            {
                auxConsistencia = 0;
                exports.abreConexao();
                exports.startTransaction("conectorPDV_replace_juridica");
                exports.addParametro("replace_idcliente", idCliente);
                exports.addParametro("replace_cnpj", cnpj);
                exports.addParametro("replace_idAtividade", idAtividade);
                exports.addParametro("replace_razao", razao);
                exports.addParametro("replace_fantasia", fantasia);
                exports.addParametro("replace_ie", ie);
                exports.addParametro("replace_dataAbertura", String.Format("{0:yyyyMMdd}", Convert.ToDateTime(dataAbertura)));
                exports.addParametro("replace_idTipoFornecedor", idTipofornecedor);
                exports.procedimentoRead();
            }
            catch (Exception erro)
            {
                 auxConsistencia = 1;
            }
            finally
            {
                exports.fechaConexao();
            }
            return auxConsistencia;
        }
        protected int conectorPDV_replace_fone(string fone, string idCliente,
                string idAtividade,
                string ddd,
                string telefone,
                string ie,
                string idTypeFone,
                string complemento,
                string priori)
        {
            try
            {
                auxConsistencia = 0;
                exports.abreConexao();
                exports.startTransaction("conectorPDV_replace_fone");
                exports.addParametro("replace_idFone", fone);
                exports.addParametro("replace_idcliente", idCliente);
                exports.addParametro("replace_idAtividade", idAtividade);
                exports.addParametro("replace_ddd", ddd);
                exports.addParametro("replace_telefone", telefone);
                exports.addParametro("replace_ramal", ie);
                exports.addParametro("replace_idTypeFone", idTypeFone);
                exports.addParametro("replace_complemento", complemento);
                exports.addParametro("replace_priori", priori);
                exports.procedimentoRead();
            }
            catch (Exception erro)
            {
                auxConsistencia = 1;
            }
            finally
            {
                exports.fechaConexao(); 
            }
            return auxConsistencia;
        }
        protected int conectorPDV_replace_endereco(string idEndereco,
                    string idCliente,
                    string sequencia,
                    string cep,
                    string idcepbairro,
                    string idEnderecoType,
                    string bairro,
                    string logradouro,
                    string complemento,
                    string municipio,
                    string estado,
                    string numero)
        {
            try
            {
                auxConsistencia = 0;
                exports.abreConexao();
                exports.startTransaction("conectorPDV_replace_endereco");
                exports.addParametro("replace_idEndereco", idEndereco);
                exports.addParametro("replace_idcliente", idCliente);
                exports.addParametro("replace_sequencia", sequencia);
                exports.addParametro("replace_cep", cep);
                exports.addParametro("replace_idcepbairro", idcepbairro);
                exports.addParametro("replace_idEnderecoType", idEnderecoType);
                exports.addParametro("replace_bairro", bairro);
                exports.addParametro("replace_logradouro", logradouro);
                exports.addParametro("replace_complemento", complemento);
                exports.addParametro("replace_municipio", municipio);
                exports.addParametro("replace_estado", estado);
                exports.addParametro("replace_numero", numero);
                exports.procedimentoRead();
            }
            catch (Exception erro)
            {
                auxConsistencia = 1;
            }
            finally
            {
                exports.fechaConexao(); 
            }
            return auxConsistencia;
        }

        protected int conectorPDV_replace_clienteProfissional(string clienteProfissional, string idCliente,
            string idEndereco,
            string empresa,
            string salarioDeclarado,
            string salarioComprovado,
            string idprofissao,
            string idEscolaridade,
            string default1)
        {
            try
            {
                auxConsistencia = 0;
                exports.abreConexao();
                exports.startTransaction("conectorPDV_replace_clienteProfissional");
                exports.addParametro("replace_idClienteProfissional", clienteProfissional);
                exports.addParametro("replace_idcliente", idCliente);
                exports.addParametro("replace_idendereco", idEndereco);
                exports.addParametro("replace_empresa", empresa);
                exports.addParametro("replace_salarioDeclarado", salarioDeclarado.Replace(",", "."));
                exports.addParametro("replace_salarioComprovado", salarioComprovado.Replace(",", "."));
                exports.addParametro("replace_idprofissao", idprofissao);
                exports.addParametro("replace_idEscolaridade", idEscolaridade);
                exports.addParametro("replace_default1", default1);
                exports.procedimentoRead();
            }
            catch (Exception erro)
            {
                auxConsistencia = 1;
            }
            finally
            {
                exports.fechaConexao();
            }
            return auxConsistencia;
        }

        protected int conectorPDV_replace_clienteReferencia(string clienteReferencia, string idCliente,
                            string idtypeReferencia,
                            string empresaContato,
                            string contato,
                            string ddd,
                            string fone,
                            string ramal,
                            string data, string observacao)
        {
            try
            {
                auxConsistencia = 0;
                exports.abreConexao();
                exports.startTransaction("conectorPDV_replace_clienteReferencia");
                exports.addParametro("replace_idReferencia", clienteReferencia);
                exports.addParametro("replace_idcliente", idCliente);
                exports.addParametro("replace_idtypeReferencia", idtypeReferencia);
                exports.addParametro("replace_empresaContato", empresaContato);
                exports.addParametro("replace_Contato", contato);
                exports.addParametro("replace_ddd", ddd);
                exports.addParametro("replace_fone", fone);
                exports.addParametro("replace_ramal", ramal);
                exports.addParametro("replace_data", String.Format("{0:yyyyMMdd}", Convert.ToDateTime(data)));
                exports.addParametro("replace_observacao", observacao);
                exports.procedimentoRead();
            }
            catch (Exception erro)
            {
                auxConsistencia = 1;
            }
            finally
            {
                exports.fechaConexao(); 
            }
            return auxConsistencia;
        }
        protected int conectorPDV_replace_clienteEntrega(string clienteEntrega,
                    string endereco,
                    string pessoa,
                    string seq,
                    string default1
)
        {
            try
            {
                auxConsistencia = 0;
                exports.abreConexao();
                exports.startTransaction("conectorPDV_replace_clienteEntrega");
                exports.addParametro("replace_idClienteEntrega", clienteEntrega);
                exports.addParametro("replace_idEndereco", endereco);
                exports.addParametro("replace_idCliente", pessoa);
                exports.addParametro("replace_sequencia", seq);
                exports.addParametro("replace_default1", default1);
                exports.procedimentoRead();
            }
            catch (Exception erro)
            {
                auxConsistencia = 1;
            }
            finally
            {
                exports.fechaConexao();
            }
            return auxConsistencia;
        }
        protected int conectorPDV_replace_clienteCobranca(string clienteCobranca,
                            string endereco,
                            string pessoa,
                            string seq,
                            string default1
        )
        {
            try
            {
                auxConsistencia = 0;
                exports.abreConexao();
                exports.startTransaction("conectorPDV_replace_clienteCobranca");
                exports.addParametro("replace_idClienteCobranca", clienteCobranca);
                exports.addParametro("replace_idEndereco", endereco);
                exports.addParametro("replace_idCliente", pessoa);
                exports.addParametro("replace_sequencia", seq);
                exports.addParametro("replace_default1", default1);
                exports.procedimentoRead();
            }
            catch (Exception erro)
            {
                auxConsistencia = 1;
            }
            finally
            {
                exports.fechaConexao(); 
            }
            return auxConsistencia;
        }

        protected int conectorPDV_replace_clienteRisco(string risco, string idCliente,
                    string cooperado,
                    string preferencial,
                    string limiteCheque,
                    string onlyHourCheque,
                    string convenio,
                    string pagador,
                    string limiteEstouro,
            string limiteConvenio,
            string noteCobrancaConvenio,
            string typePrazo,
            string diaEncerramento, string diaFechamento, string prazoDias, string motivo)
        {
            try
            {
                auxConsistencia = 0;
                exports.abreConexao();
                exports.startTransaction("conectorPDV_replace_clienteRisco");
                exports.addParametro("replace_idclienteRisco", risco);
                exports.addParametro("replace_idcliente", idCliente);
                exports.addParametro("replace_cooperado", cooperado);
                exports.addParametro("replace_preferencial", preferencial);
                exports.addParametro("replace_limiteCheque", limiteCheque.Replace(",", "."));
                exports.addParametro("replace_onlyHourCheque", onlyHourCheque);
                exports.addParametro("replace_convenio", convenio.Replace(",", "."));
                exports.addParametro("replace_pagador", pagador);
                exports.addParametro("replace_limiteEstouro", limiteEstouro.Replace(",", "."));
                exports.addParametro("replace_limiteConvenio", limiteConvenio);
                exports.addParametro("replace_noteCobrancaConvenio", noteCobrancaConvenio.Replace(",", "."));
                exports.addParametro("replace_typePrazo", typePrazo);
                exports.addParametro("replace_diaEncerramento", diaEncerramento);
                exports.addParametro("replace_diaFechamento", diaFechamento);
                exports.addParametro("replace_prazoDias", prazoDias);
                exports.addParametro("replace_motivo", motivo);
                exports.procedimentoRead();
            }
            catch (Exception erro)
            {
                auxConsistencia = 1;
            }
            finally
            {
                exports.fechaConexao();
            }
            return auxConsistencia;
        }

        protected int conectorWEB_replace_paramentro_fornecedor_informacao(
         string replace_idInformacao,
         string replace_idcliente,
         string replace_typeTroca,
         string replace_typeFrete,
         string replace_porcentagemFrete,
         string replace_lastVisita,
         string replace_nextVisita,
         string replace_devPagar,
         string replace_bloquearEntregaFiscal,
         string replace_representante,
         string replace_idoperacao,
         string replace_forceCompra,
         string replace_nameSugestao,
         string replace_passwdSugestao,
         string replace_observacao,
         string replace_typeFornecedor)
        {
            try
            {
                auxConsistencia = 0;
                exports.abreConexao();
                exports.startTransaction("conectorWEB_replace_paramentro_fornecedor_informacao");
                exports.addParametro("replace_idInformacao", replace_idInformacao);
                exports.addParametro("replace_idcliente", replace_idcliente);
                exports.addParametro("replace_typeTroca", replace_typeTroca);
                exports.addParametro("replace_typeFrete", replace_typeFrete);
                exports.addParametro("replace_porcentagemFrete", replace_porcentagemFrete.Replace(",", "."));
                exports.addParametro("replace_lastVisita", replace_lastVisita);
                exports.addParametro("replace_nextVisita", replace_nextVisita);
                exports.addParametro("replace_devPagar", replace_devPagar);
                exports.addParametro("replace_bloquearEntregaFiscal", replace_bloquearEntregaFiscal);
                exports.addParametro("replace_representante", replace_representante);
                exports.addParametro("replace_idoperacao", replace_idoperacao);
                exports.addParametro("replace_forceCompra", replace_forceCompra);
                exports.addParametro("replace_nameSugestao", replace_nameSugestao);
                exports.addParametro("replace_passwdSugestao", replace_passwdSugestao);
                exports.addParametro("replace_observacao", replace_observacao);
                exports.addParametro("replace_typeFornecedor", replace_typeFornecedor);
                exports.procedimentoRead();
            }
            catch (Exception erro)
            {
                auxConsistencia = 1;
            }
            finally
            {
                exports.fechaConexao();
            }
            return auxConsistencia;
        }

        protected int conectorWEB_replace_paramentro_fornecedor_fiscal(
                     string replace_idfiscal,
            string replace_idcliente,
            string replace_forceIcms,
            string replace_forcePis,
            string replace_forceCofins,
            string replace_descatadaSt,
            string replace_typeGrade)
        {
            try
            {
                auxConsistencia = 0;
                exports.abreConexao();
                exports.startTransaction("conectorWEB_replace_paramentro_fornecedor_fiscal");
                exports.addParametro("replace_idfiscal", replace_idfiscal);
                exports.addParametro("replace_idcliente", replace_idcliente);
                exports.addParametro("replace_forceIcms", replace_forceIcms);
                exports.addParametro("replace_forcePis", replace_forcePis);
                exports.addParametro("replace_forceCofins", replace_forceCofins);
                exports.addParametro("replace_descatadaSt", replace_descatadaSt);
                exports.addParametro("replace_typeGrade", replace_typeGrade);
                exports.procedimentoRead();
            }
            catch (Exception erro)
            {
                auxConsistencia = 1;
            }
            finally
            {
                exports.fechaConexao();
            }
            return auxConsistencia;
        }

        protected int conectorWeb_replace_paramentro_fornecedor_comercial(
                     string replace_idComercial,
          string replace_idcliente,
          string replace_visita,
          string replace_analiseCompra,
          string replace_minVolume,
          string replace_valueBay,
          string replace_comprador,
          string replace_prazoEntrega,
          string replace_formaPgto,
          string replace_banco,
          string replace_agencia,
          string replace_contaCorrente
            )
        {
            try
            {
                auxConsistencia = 0;
                exports.abreConexao();
                exports.startTransaction("conectorWeb_replace_paramentro_fornecedor_comercial");
                exports.addParametro("replace_idComercial", replace_idComercial);
                exports.addParametro("replace_idcliente", replace_idcliente);
                exports.addParametro("replace_visita", replace_visita);
                exports.addParametro("replace_analiseCompra", replace_analiseCompra.Replace(",", "."));
                exports.addParametro("replace_minVolume", replace_minVolume.Replace(",", "."));
                exports.addParametro("replace_valueBay", replace_valueBay.Replace(",", "."));
                exports.addParametro("replace_comprador", replace_comprador);
                exports.addParametro("replace_prazoEntrega", replace_prazoEntrega.Replace(",", "."));
                exports.addParametro("replace_formaPgto", replace_formaPgto.Replace(",", "."));
                exports.addParametro("replace_banco", replace_banco);
                exports.addParametro("replace_agencia", replace_agencia);
                exports.addParametro("replace_contaCorrente", replace_contaCorrente.Replace(",", "."));
                exports.procedimentoRead();
            }
            catch (Exception erro)
            {
                auxConsistencia = 1;
            }
            finally
            {
                exports.fechaConexao();
            }
            return auxConsistencia;
        }

        #endregion

        #region //Funções da base produto

        protected int conectorWeb_replace_produto(string replace_idProduto,
              string replace_nome,
              string replace_nomePdv,
              string replace_nomeFull,
              string replace_observacao,
              string replace_status,
              string replace_dataInclusao,
              string replace_dataAlteracao,
              string replace_idsetor,
              string replace_idgrupo,
              string replace_idcategoria,
              string replace_idfornecedor,
              string replace_idusuario,
              string replace_qttyObrigatoria,
              string replace_qttyMaxima,
              string replace_descontoIndividual,
              string replace_restrito,
              string replace_idunidade,
              string replace_tipo,
              string replace_incideIpi,
              string replace_flagCompra,
              string replace_inputCfop,
              string replace_outputCfop,
              string replace_idUsuarioAlt,
              string replace_permitiMultiplicacao,
              string replace_origemMercadoria,
              string replace_idMarca,
              string replace_referencia,
              string replace_composto,
              string replace_chaveComposto)
        {
            int aux = 0;
            auxConsistencia = 0;
            try
            {
                exports.abreConexao();
                exports.iniciarTransacao();
                exports.startTransaction("conectorWeb_replace_produto");
                exports.addParametro("replace_idProduto", replace_idProduto);
                exports.addParametro("replace_nome", replace_nome);
                exports.addParametro("replace_nomePdv", replace_nomePdv);
                exports.addParametro("replace_nomeFull", replace_nomeFull);
                exports.addParametro("replace_observacao", replace_observacao);
                exports.addParametro("replace_status", replace_status);
                exports.addParametro("replace_dataInclusao", replace_dataInclusao);
                exports.addParametro("replace_dataAlteracao", replace_dataAlteracao);
                exports.addParametro("replace_idsetor", replace_idsetor);
                exports.addParametro("replace_idgrupo", replace_idgrupo);
                exports.addParametro("replace_idcategoria", replace_idcategoria);
                exports.addParametro("replace_idfornecedor", replace_idfornecedor);
                exports.addParametro("replace_idusuario", replace_idusuario);
                exports.addParametro("replace_qttyObrigatoria", replace_qttyObrigatoria.Replace(",", "."));
                exports.addParametro("replace_qttyMaxima", replace_qttyMaxima.Replace(",", "."));
                exports.addParametro("replace_descontoIndividual", replace_descontoIndividual.Replace(",", "."));
                exports.addParametro("replace_restrito", replace_restrito);
                exports.addParametro("replace_idunidade", replace_idunidade);
                exports.addParametro("replace_tipo", replace_tipo);
                exports.addParametro("replace_incideIpi", replace_incideIpi.Replace(",", "."));
                exports.addParametro("replace_flagCompra", replace_flagCompra);
                exports.addParametro("replace_inputCfop", replace_inputCfop);
                exports.addParametro("replace_outputCfop", replace_outputCfop);
                exports.addParametro("replace_idUsuarioAlt", replace_idUsuarioAlt);
                exports.addParametro("replace_permitiMultiplicacao", replace_permitiMultiplicacao.Replace(",", "."));
                exports.addParametro("replace_origemMercadoria", replace_origemMercadoria);
                exports.addParametro("replace_idMarca", replace_idMarca);
                exports.addParametro("replace_referencia", replace_referencia.Replace(",", "."));
                exports.addParametro("replace_composto", replace_composto.Replace(",", "."));
                exports.addParametro("replace_chaveComposto", replace_chaveComposto);
                exports.procedimentoRead();
                if (exports.retornaRead().Read() == true)
                {
                    aux = 1;
                }
                exports.fechaRead();
                exports.commit();
            }
            catch (Exception erro)
            {
                exports.rollback();
                auxConsistencia = 1;
            }
            finally
            {
                exports.fechaConexao();
                if (auxConsistencia == 0)
                {
                }
            }
            return aux;
        }

        protected int conectorWeb_replace_produtoembalagem(string replace_idprodutoEmbalagem,
                      string replace_idProduto,
                      string replace_barra,
                      string replace_idunidadeMedida,
                      string replace_quantidade,
                      string replace_defaultVenda,
                      string replace_defaultCompra,
                      string replace_defaultTransferencia,
                      string replace_status,
                      string replace_typeEan)
        {
            int aux = 0;
            auxConsistencia = 0;
            try
            {
                exports.abreConexao();
                exports.iniciarTransacao();
                exports.startTransaction("conectorWeb_replace_produtoembalagem");
                exports.addParametro("replace_idprodutoEmbalagem", replace_idprodutoEmbalagem);
                exports.addParametro("replace_idProduto", replace_idProduto);
                exports.addParametro("replace_barra", replace_barra);
                exports.addParametro("replace_idunidadeMedida", replace_idunidadeMedida);
                exports.addParametro("replace_quantidade", replace_quantidade.Replace(",", "."));
                exports.addParametro("replace_defaultVenda", replace_defaultVenda);
                exports.addParametro("replace_defaultCompra", replace_defaultCompra);
                exports.addParametro("replace_defaultTransferencia", replace_defaultTransferencia);
                exports.addParametro("replace_status", replace_status);
                exports.addParametro("replace_typeEan", replace_typeEan);
                exports.procedimentoRead();
                if (exports.retornaRead().Read() == true)
                {
                    aux = 1;
                }
                exports.fechaRead();
                exports.commit();
            }
            catch (Exception erro)
            {
                exports.rollback();
                auxConsistencia = 1;
            }
            finally
            {
                exports.fechaConexao();
                if (auxConsistencia == 0)
                {
                }
            }
            return aux;
        }

        protected int conectorWeb_replace_produtoimpostos(
                      string replace_idProduto,
                      string replace_idloja,
                      string replace_tributacao,
                      string replace_tributacao1,
                      string replace_tributacao2,
                      string replace_idpisCofins,
                      string replace_cst,
                      string replace_cstSaida,
                      string replace_cstEntrada,
                      string replace_pauta,
                      string replace_ipi,
                      string replace_ipiValor,
                      string replace_spedNcm,
                      string replace_impMercadoInterno,
                      string replace_csosn,
                      string replace_idtypeItem,
                      string replace_idGenero,
                      string replace_reducaoCalcEspecial,
                      string replace_ippt)
        {
            int aux = 0;
            auxConsistencia = 0;
            try
            {
                exports.abreConexao();
                exports.iniciarTransacao();
                exports.startTransaction("conectorWeb_replace_produtoimpostos");
                exports.addParametro("replace_idProduto", replace_idProduto);
                exports.addParametro("replace_idloja", replace_idloja);
                exports.addParametro("replace_tributacao", replace_tributacao);
                exports.addParametro("replace_tributacao1", replace_tributacao1);
                exports.addParametro("replace_tributacao2", replace_tributacao2);
                exports.addParametro("replace_idpisCofins", replace_idpisCofins);
                exports.addParametro("replace_cst", replace_cst);
                exports.addParametro("replace_cstSaida", replace_cstSaida);
                exports.addParametro("replace_cstEntrada", replace_cstEntrada);
                exports.addParametro("replace_pauta", replace_pauta.Replace(",", "."));
                exports.addParametro("replace_ipi", replace_ipi.Replace(",", "."));
                exports.addParametro("replace_ipiValor", replace_ipiValor.Replace(",", "."));
                exports.addParametro("replace_spedNcm", replace_spedNcm);
                exports.addParametro("replace_impMercadoInterno", replace_impMercadoInterno.Replace(",", "."));
                exports.addParametro("replace_csosn", replace_csosn);
                exports.addParametro("replace_idtypeItem", replace_idtypeItem);
                exports.addParametro("replace_idGenero", replace_idGenero);
                exports.addParametro("replace_reducaoCalcEspecial", replace_reducaoCalcEspecial.Replace(",", "."));
                exports.addParametro("replace_ippt", replace_ippt);

                exports.procedimentoRead();
                if (exports.retornaRead().Read() == true)
                {
                    aux = 1;
                }
                exports.fechaRead();
                exports.commit();
            }
            catch (Exception erro)
            {
                exports.rollback();
                auxConsistencia = 1;
            }
            finally
            {
                exports.fechaConexao();
                if (auxConsistencia == 0)
                {
                }
            }
            return aux;
        }

        protected int conectorWeb_replace_produtoprice(string replace_idProduto,
                      string replace_idloja,
                      string replace_priceFull,
                      string replace_priceVenda,
                      string replace_pricePendente,
                      string replace_creditoIcms,
                      string replace_creditoRedIcms,
                      string replace_creditoPis,
                      string replace_creditoCofins,
                      string replace_creditoOutros,
                      string replace_primeiroDesconto,
                      string replace_segundoDesconto,
                      string replace_terceiroDesconto,
                      string replace_debitoIcms,
                      string replace_debitoRedIcms,
                      string replace_lucroLiquido,
                      string replace_lucroBruto,
                      string replace_custoBruto,
                      string replace_custoliquido,
                      string replace_custoMedio,
                      string replace_IpiPorcentagem,
                      string replace_moedaIpi,
                      string replace_moedaFrete,
                      string replace_fretePorcentagem,
                      string replace_comissao,
                      string replace_priceSugestao,
                      string replace_substituicaoPorcetagem,
                      string replace_acrescimoSubstituicao,
                      string replace_moedaSubstituicao,
                      string replace_bonificacaoDesconto,
                      string replace_moedaBonificacao,
                      string replace_margem,
                      string replace_descontoMaximo,
                      string replace_despesasTributadas,
                      string replace_despesaNaoTributadas,
                      string replace_contribuicao,
                      string replace_vendo,
                      string replace_financeiro,
                      string replace_despesaFixa,
                      string replace_statusPrice,
                      string replace_chaveEntrada,
                      string replace_flagCompraLoja,
                      string replace_origem,
                      string replace_usuarioAltCusto,
                      string replace_usuarioAltPrice,
                      string replace_pendente,
                      string replace_pendenteNota,
                      string replace_pendenteFornecedor,
                      string replace_embalagem,
                      string replace_custoTransferencia,
                      string replace_icmsFrete,
                      string replace_acrescimo,
                      string replace_descontoValor,
                      string replace_validade,
                      string replace_margemBruta,
                      string replace_margemLiquida,
                      string replace_custoAnterior,
                      string replace_precoVendaAnterior,
                      string replace_margemAnterior,
                      string replace_precoPromocao,
                      string replace_trunca)
        {
            int aux = 0;
            auxConsistencia = 0;
            try
            {
                exports.abreConexao();
                exports.iniciarTransacao();
                exports.startTransaction("conectorWeb_replace_produtoprice");
                exports.addParametro("replace_idProduto", replace_idProduto);
                exports.addParametro("replace_idloja", replace_idloja);
                exports.addParametro("replace_priceFull", replace_priceFull.Replace(",", "."));
                exports.addParametro("replace_priceVenda", replace_priceVenda.Replace(",", "."));
                exports.addParametro("replace_pricePendente", replace_pricePendente.Replace(",", "."));
                exports.addParametro("replace_creditoIcms", replace_creditoIcms.Replace(",", "."));
                exports.addParametro("replace_creditoRedIcms", replace_creditoRedIcms.Replace(",", "."));
                exports.addParametro("replace_creditoPis", replace_creditoPis.Replace(",", "."));
                exports.addParametro("replace_creditoCofins", replace_creditoCofins.Replace(",", "."));
                exports.addParametro("replace_creditoOutros", replace_creditoOutros.Replace(",", "."));
                exports.addParametro("replace_primeiroDesconto", replace_primeiroDesconto.Replace(",", "."));
                exports.addParametro("replace_segundoDesconto", replace_segundoDesconto.Replace(",", "."));
                exports.addParametro("replace_terceiroDesconto", replace_terceiroDesconto.Replace(",", "."));
                exports.addParametro("replace_debitoIcms", replace_debitoIcms.Replace(",", "."));
                exports.addParametro("replace_debitoRedIcms", replace_debitoRedIcms.Replace(",", "."));
                exports.addParametro("replace_lucroLiquido", replace_lucroLiquido.Replace(",", "."));
                exports.addParametro("replace_lucroBruto", replace_lucroBruto.Replace(",", "."));
                exports.addParametro("replace_custoBruto", replace_custoBruto.Replace(",", "."));
                exports.addParametro("replace_custoliquido", replace_custoliquido.Replace(",", "."));
                exports.addParametro("replace_custoMedio", replace_custoMedio.Replace(",", "."));
                exports.addParametro("replace_IpiPorcentagem", replace_IpiPorcentagem.Replace(",", "."));
                exports.addParametro("replace_moedaIpi", replace_moedaIpi.Replace(",", "."));
                exports.addParametro("replace_moedaFrete", replace_moedaFrete.Replace(",", "."));
                exports.addParametro("replace_fretePorcentagem", replace_fretePorcentagem.Replace(",", "."));
                exports.addParametro("replace_comissao", replace_comissao.Replace(",", "."));
                exports.addParametro("replace_priceSugestao", replace_priceSugestao.Replace(",", "."));
                exports.addParametro("replace_substituicaoPorcetagem", replace_substituicaoPorcetagem.Replace(",", "."));
                exports.addParametro("replace_acrescimoSubstituicao", replace_acrescimoSubstituicao.Replace(",", "."));
                exports.addParametro("replace_moedaSubstituicao", replace_moedaSubstituicao.Replace(",", "."));
                exports.addParametro("replace_bonificacaoDesconto", replace_bonificacaoDesconto.Replace(",", "."));
                exports.addParametro("replace_moedaBonificacao", replace_moedaBonificacao.Replace(",", "."));
                exports.addParametro("replace_margem", replace_margem.Replace(",", "."));
                exports.addParametro("replace_descontoMaximo", replace_descontoMaximo.Replace(",", "."));
                exports.addParametro("replace_despesasTributadas", replace_despesasTributadas.Replace(",", "."));
                exports.addParametro("replace_despesaNaoTributadas", replace_despesaNaoTributadas.Replace(",", "."));
                exports.addParametro("replace_contribuicao", replace_contribuicao.Replace(",", "."));
                exports.addParametro("replace_vendo", replace_vendo.Replace(",", "."));
                exports.addParametro("replace_financeiro", replace_financeiro.Replace(",", "."));
                exports.addParametro("replace_despesaFixa", replace_despesaFixa.Replace(",", "."));
                exports.addParametro("replace_statusPrice", replace_statusPrice.Replace(",", "."));
                exports.addParametro("replace_chaveEntrada", replace_chaveEntrada.Replace(",", "."));
                exports.addParametro("replace_flagCompraLoja", replace_flagCompraLoja);
                exports.addParametro("replace_origem", replace_origem);
                exports.addParametro("replace_usuarioAltCusto", replace_usuarioAltCusto.Replace(",", "."));
                exports.addParametro("replace_usuarioAltPrice", replace_usuarioAltPrice.Replace(",", "."));
                exports.addParametro("replace_pendente", replace_pendente);
                exports.addParametro("replace_pendenteNota", replace_pendenteNota.Replace(",", "."));
                exports.addParametro("replace_pendenteFornecedor", replace_pendenteFornecedor);
                exports.addParametro("replace_embalagem", replace_embalagem);
                exports.addParametro("replace_custoTransferencia", replace_custoTransferencia.Replace(",", "."));
                exports.addParametro("replace_icmsFrete", replace_icmsFrete.Replace(",", "."));
                exports.addParametro("replace_acrescimo", replace_acrescimo.Replace(",", "."));
                exports.addParametro("replace_descontoValor", replace_descontoValor.Replace(",", "."));
                exports.addParametro("replace_validade", replace_validade.Replace(",", "."));
                exports.addParametro("replace_margemBruta", replace_margemBruta.Replace(",", "."));
                exports.addParametro("replace_margemLiquida", replace_margemLiquida.Replace(",", "."));
                exports.addParametro("replace_custoAnterior", replace_custoAnterior.Replace(",", "."));
                exports.addParametro("replace_precoVendaAnterior", replace_precoVendaAnterior.Replace(",", "."));
                exports.addParametro("replace_margemAnterior", replace_margemAnterior.Replace(",", "."));
                exports.addParametro("replace_precoPromocao", replace_precoPromocao.Replace(",", "."));
                exports.addParametro("replace_trunca", replace_trunca.Replace(",", "."));
                exports.procedimentoRead();
                if (exports.retornaRead().Read() == true)
                {
                    aux = 1;
                }
                exports.fechaRead();
                exports.commit();
            }
            catch (Exception erro)
            {
                exports.rollback();
                auxConsistencia = 1;
            }
            finally
            {
                exports.fechaConexao();
                if (auxConsistencia == 0)
                {
                }
            }
            return aux;
        }

        protected int conectorWeb_replace_produtoscompostos(string replace_chave,
                      string replace_itens,
                      string replace_loja,
                      string replace_atualizacusto,
                      string replace_atualizavenda,
                      string replace_calculavenda,
                      string replace_codigo,
                      string replace_exporta,
                      string replace_fator,
                      string replace_lucro,
                      string replace_lucronormal,
                      string replace_movimentaitens,
                      string replace_totalcusto,
                      string replace_totalcustonormal,
                      string replace_totalreposicao,
                      string replace_totalreposicaonormal,
                      string replace_totalvenda,
                      string replace_totalvendanormal,
                      string replace_volumes,
                      string replace_pdvbaixaitens,
                      string replace_tipo,
                      string replace_modofazer,
                      string replace_movimentaprincipalitens,
                      string replace_nmovprinc,
                      string replace_movimentaitenspedinterno,
                      string replace_fechtodiarioproducao)
        {
            int aux = 0;
            auxConsistencia = 0;
            try
            {
                exports.abreConexao();
                exports.iniciarTransacao();
                exports.startTransaction("conectorWeb_replace_produtoscompostos");
                exports.addParametro("replace_chave", replace_chave);
                exports.addParametro("replace_itens", replace_itens);
                exports.addParametro("replace_loja", replace_loja);
                exports.addParametro("replace_atualizacusto", replace_atualizacusto);
                exports.addParametro("replace_atualizavenda", replace_atualizavenda);
                exports.addParametro("replace_calculavenda", replace_calculavenda);
                exports.addParametro("replace_codigo", replace_codigo);
                exports.addParametro("replace_exporta", replace_exporta);
                exports.addParametro("replace_fator", replace_fator);
                exports.addParametro("replace_lucro", replace_lucro.Replace(",", "."));
                exports.addParametro("replace_lucronormal", replace_lucronormal.Replace(",", "."));
                exports.addParametro("replace_movimentaitens", replace_movimentaitens.Replace(",", "."));
                exports.addParametro("replace_totalcusto", replace_totalcusto.Replace(",", "."));
                exports.addParametro("replace_totalcustonormal", replace_totalcustonormal.Replace(",", "."));
                exports.addParametro("replace_totalreposicao", replace_totalreposicao.Replace(",", "."));
                exports.addParametro("replace_totalreposicaonormal", replace_totalreposicaonormal.Replace(",", "."));
                exports.addParametro("replace_totalvenda", replace_totalvenda.Replace(",", "."));
                exports.addParametro("replace_totalvendanormal", replace_totalvendanormal.Replace(",", "."));
                exports.addParametro("replace_volumes", replace_volumes.Replace(",", "."));
                exports.addParametro("replace_pdvbaixaitens", replace_pdvbaixaitens.Replace(",", "."));
                exports.addParametro("replace_tipo", replace_tipo);
                exports.addParametro("replace_modofazer", replace_modofazer);
                exports.addParametro("replace_movimentaprincipalitens", replace_movimentaprincipalitens);
                exports.addParametro("replace_nmovprinc", replace_nmovprinc);
                exports.addParametro("replace_movimentaitenspedinterno", replace_movimentaitenspedinterno);
                exports.addParametro("replace_fechtodiarioproducao", replace_fechtodiarioproducao);
                exports.procedimentoRead();
                if (exports.retornaRead().Read() == true)
                {
                    aux = 1;
                }
                exports.fechaRead();
                exports.commit();
            }
            catch (Exception erro)
            {
                exports.rollback();
                auxConsistencia = 1;
            }
            finally
            {
                exports.fechaConexao();
                if (auxConsistencia == 0)
                {
                }
            }
            return aux;
        }


        protected int conectorWeb_replace_produtostore(string replace_idProduto,
                      string replace_idloja,
                      string replace_mix)
        {
            int aux = 0;
            auxConsistencia = 0;
            try
            {
                exports.abreConexao();
                exports.iniciarTransacao();
                exports.startTransaction("conectorWeb_replace_produtostore");
                exports.addParametro("replace_idProduto", replace_idProduto);
                exports.addParametro("replace_idloja", replace_idloja);
                exports.addParametro("replace_mix", replace_mix);
                exports.procedimentoRead();
                if (exports.retornaRead().Read() == true)
                {
                    aux = 1;
                }
                exports.fechaRead();
                exports.commit();
            }
            catch (Exception erro)
            {
                exports.rollback();
                auxConsistencia = 1;
            }
            finally
            {
                exports.fechaConexao();
                if (auxConsistencia == 0)
                {
                }
            }
            return aux;
        }

        #endregion

        #region //Autenticacao Cadastro de Produtos

        public void conector_autentica_produto(string chave)
        {
            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                imports.singleTransaction("update produto set importCarga=1 where idProduto=?chave and importCarga=0");
                imports.addParametro("?chave", chave);
                imports.addParametro("?store", store);
                imports.procedimentoRead();
                if (imports.retornaRead().Read() == true)
                {
                }

            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                }
                imports.fechaConexao();
            }
        }
        public void conector_produtos()
        {
            string[,] vetor = new string[0, 0];
            string[,] recargaProduto = new string[0, 0]; //Reserva
            string[,] recargaProdutoPrice = new string[0, 0]; //PedidoItens
            string[,] recargaProdutoImpostos; //PedidoFinanceiro
            string[,] recargaProdutoEmbalagem;
            string[,] recargaProdutoCompostos;
            string[,] recargaStore;

            //Cadastro de Produto
            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                imports.singleTransaction("select * from produto where importCarga=0");
                imports.procedimentoSet();
            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = imports.retornaSet().Tables[0].Columns.Count;
                    countRows = imports.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        vetor = new string[countRows, 3];
                        recargaProduto = new string[countRows, countField];
                        for (int i = 0; i < countRows; i++)//Linha
                        {
                            for (int j = 0; j < countField; j++) //Coluna
                            {
                                recargaProduto[i, j] = Convert.ToString(imports.retornaSet().Tables[0].Rows[i][j]);
                            }
                        }
                        imports.fechaConexao();
                        for (int i = 0; i < recargaProduto.GetLength(0); i++)//Linha
                        {
                            if (conectorWeb_replace_produto(recargaProduto[i, 0], recargaProduto[i, 1], recargaProduto[i, 2], recargaProduto[i, 3], recargaProduto[i, 4], recargaProduto[i, 5], recargaProduto[i, 6], recargaProduto[i, 7], recargaProduto[i, 8], recargaProduto[i, 9], recargaProduto[i, 10], recargaProduto[i, 11], 
                                recargaProduto[i, 12], recargaProduto[i, 13],recargaProduto[i, 14],recargaProduto[i, 15],recargaProduto[i, 16],
                                recargaProduto[i, 17],recargaProduto[i, 18],recargaProduto[i, 19],recargaProduto[i, 20],recargaProduto[i, 21],
                                recargaProduto[i, 22],recargaProduto[i, 23],recargaProduto[i, 24],recargaProduto[i, 25],recargaProduto[i, 26],
                                recargaProduto[i, 27],recargaProduto[i, 28],recargaProduto[i, 29]) == 0)
                            {
                                vetor[i, 0] = recargaProduto[i, 0];
                                vetor[i, 1] = alwaysVariables.Store;
                                vetor[i, 2] = "#";
                            }
                        }
                    }
                }
                imports.fechaConexao();
            }

            //Cadastro de Produto preco
            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                imports.singleTransaction("select tab1.* from produto tab inner join produtoPrice tab1 on(tab.idProduto = tab1.idProduto) where tab.importCarga=0 and tab1.idLoja=?store");
                imports.addParametro("?store", store);
                imports.procedimentoSet();
            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = imports.retornaSet().Tables[0].Columns.Count;
                    countRows = imports.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        recargaProdutoPrice = new string[countRows, countField];
                        for (int i = 0; i < countRows; i++)//Linha
                        {
                            for (int j = 0; j < countField; j++) //Coluna
                            {
                                recargaProdutoPrice[i, j] = Convert.ToString(imports.retornaSet().Tables[0].Rows[i][j]);
                            }
                        }
                        imports.fechaConexao();
                        for (int i = 0; i < recargaProdutoPrice.GetLength(0); i++)//linha
                        {
                            if (conectorWeb_replace_produtoprice(recargaProdutoPrice[i, 0], recargaProdutoPrice[i, 1], recargaProdutoPrice[i, 2], recargaProdutoPrice[i, 3], recargaProdutoPrice[i, 4], recargaProdutoPrice[i, 5], recargaProdutoPrice[i, 6], recargaProdutoPrice[i, 7],
                                recargaProdutoPrice[i, 8], recargaProdutoPrice[i, 9], recargaProdutoPrice[i, 10], recargaProdutoPrice[i, 11], recargaProdutoPrice[i, 12], recargaProdutoPrice[i, 13], recargaProdutoPrice[i, 14], recargaProdutoPrice[i, 15],
                                recargaProdutoPrice[i, 16], recargaProdutoPrice[i, 17], recargaProdutoPrice[i, 18], recargaProdutoPrice[i, 19], recargaProdutoPrice[i, 20], recargaProdutoPrice[i, 21], recargaProdutoPrice[i, 22], recargaProdutoPrice[i, 23],
                                recargaProdutoPrice[i, 24], recargaProdutoPrice[i, 25], recargaProdutoPrice[i, 26], recargaProdutoPrice[i, 27], recargaProdutoPrice[i, 28], recargaProdutoPrice[i, 29], recargaProdutoPrice[i, 30], recargaProdutoPrice[i, 31],
                                recargaProdutoPrice[i, 32], recargaProdutoPrice[i, 33], recargaProdutoPrice[i, 34], recargaProdutoPrice[i, 35], recargaProdutoPrice[i, 36], recargaProdutoPrice[i, 37], recargaProdutoPrice[i, 38], recargaProdutoPrice[i, 39], recargaProdutoPrice[i, 40],
                                recargaProdutoPrice[i, 41], recargaProdutoPrice[i, 42], recargaProdutoPrice[i, 43], recargaProdutoPrice[i, 44], recargaProdutoPrice[i, 45], recargaProdutoPrice[i, 46], recargaProdutoPrice[i, 47], recargaProdutoPrice[i, 48], recargaProdutoPrice[i, 49],
                                recargaProdutoPrice[i, 50], recargaProdutoPrice[i, 51], recargaProdutoPrice[i, 52], recargaProdutoPrice[i, 53], recargaProdutoPrice[i, 54], recargaProdutoPrice[i, 55], recargaProdutoPrice[i, 56], recargaProdutoPrice[i, 57], recargaProdutoPrice[i, 58],
                                recargaProdutoPrice[i, 59], recargaProdutoPrice[i, 60]) == 0)
                            {
                                for (int j = 0; j < vetor.GetLength(0); j++)
                                {
                                    if (vetor[j, 0] == recargaProdutoPrice[i, 0])
                                    {
                                        vetor[j, 2] = "*";
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                imports.fechaConexao();
            }

            //Cadastro de Produto Impostos
            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                imports.singleTransaction("select tab1.* from produto tab inner join produtoImpostos tab1 on(tab.idProduto = tab1.idProduto) where tab.importCarga=0 and tab1.idLoja=?store");
                imports.addParametro("?store", store);
                imports.procedimentoSet();
            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = imports.retornaSet().Tables[0].Columns.Count;
                    countRows = imports.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        recargaProdutoImpostos = new string[countRows, countField];
                        for (int i = 0; i < countRows; i++)//Linha
                        {
                            for (int j = 0; j < countField; j++) //Coluna
                            {
                                recargaProdutoImpostos[i, j] = Convert.ToString(imports.retornaSet().Tables[0].Rows[i][j]);
                            }
                        }
                        imports.fechaConexao();
                        for (int i = 0; i < recargaProdutoImpostos.GetLength(0); i++)//linha
                        {
                            if (conectorWeb_replace_produtoimpostos(recargaProdutoImpostos[i, 0], recargaProdutoImpostos[i, 1], recargaProdutoImpostos[i, 2], recargaProdutoImpostos[i, 3], recargaProdutoImpostos[i, 4], recargaProdutoImpostos[i, 5], recargaProdutoImpostos[i, 6], recargaProdutoImpostos[i, 7],
                                recargaProdutoImpostos[i, 8], recargaProdutoImpostos[i, 9], recargaProdutoImpostos[i, 10], recargaProdutoImpostos[i, 11], recargaProdutoImpostos[i, 12], recargaProdutoImpostos[i, 13], recargaProdutoImpostos[i, 14], recargaProdutoImpostos[i, 15],
                                recargaProdutoImpostos[i, 16], recargaProdutoImpostos[i, 17], recargaProdutoImpostos[i, 18]) == 0)
                            {
                                for (int j = 0; j < vetor.GetLength(0); j++)
                                {
                                    if (vetor[j, 0] == recargaProdutoImpostos[i, 0])
                                    {
                                        vetor[j, 2] = "*";
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                imports.fechaConexao();
            }



            //Cadastro de Produto embalagem EAN
            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                imports.singleTransaction("select tab1.* from produto tab inner join produtoembalagem tab1 on(tab.idProduto = tab1.idProduto) where tab.importCarga=0 and tab1.idLoja=?store");
                imports.addParametro("?store", store);
                imports.procedimentoSet();
            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = imports.retornaSet().Tables[0].Columns.Count;
                    countRows = imports.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        recargaProdutoEmbalagem = new string[countRows, countField];
                        for (int i = 0; i < countRows; i++)//Linha
                        {
                            for (int j = 0; j < countField; j++) //Coluna
                            {
                                recargaProdutoEmbalagem[i, j] = Convert.ToString(imports.retornaSet().Tables[0].Rows[i][j]);
                            }
                        }
                        imports.fechaConexao();
                        for (int i = 0; i < recargaProdutoEmbalagem.GetLength(0); i++)//linha
                        {
                            if (conectorWeb_replace_produtoembalagem(recargaProdutoEmbalagem[i, 0], recargaProdutoEmbalagem[i, 1], recargaProdutoEmbalagem[i, 2], recargaProdutoEmbalagem[i, 3], recargaProdutoEmbalagem[i, 4], recargaProdutoEmbalagem[i, 5], recargaProdutoEmbalagem[i, 6], recargaProdutoEmbalagem[i, 7],
                                recargaProdutoEmbalagem[i, 8], recargaProdutoEmbalagem[i, 9]) == 0)
                            {
                                for (int j = 0; j < vetor.GetLength(0); j++)
                                {
                                    if (vetor[j, 0] == recargaProdutoEmbalagem[i, 0])
                                    {
                                        vetor[j, 2] = "*";
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                imports.fechaConexao();
            }



            //Cadastro de Produto Store
            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                imports.singleTransaction("select tab1.* from produto tab inner join produtoStore tab1 on(tab.idProduto = tab1.idProduto) where tab.importCarga=0 and tab1.idLoja=?store");
                imports.addParametro("?store", store);
                imports.procedimentoSet();
            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = imports.retornaSet().Tables[0].Columns.Count;
                    countRows = imports.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        recargaStore = new string[countRows, countField];
                        for (int i = 0; i < countRows; i++)//Linha
                        {
                            for (int j = 0; j < countField; j++) //Coluna
                            {
                                recargaStore[i, j] = Convert.ToString(imports.retornaSet().Tables[0].Rows[i][j]);
                            }
                        }
                        imports.fechaConexao();
                        for (int i = 0; i < recargaStore.GetLength(0); i++)//linha
                        {
                            if (conectorWeb_replace_produtostore(recargaStore[i, 0], recargaStore[i, 1], recargaStore[i, 2]) == 0)
                            {
                                for (int j = 0; j < vetor.GetLength(0); j++)
                                {
                                    if (vetor[j, 0] == recargaStore[i, 0])
                                    {
                                        vetor[j, 2] = "*";
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                imports.fechaConexao();
            }

            //Cadastro de Produto Compostos
            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                imports.singleTransaction("select tab1.* from produto tab inner join produtoscompostos tab1 on(tab.idProduto = tab1.chave) where tab.importCarga=0 and tab1.loja=?store");
                imports.addParametro("?store", store);
                imports.procedimentoSet();
            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = imports.retornaSet().Tables[0].Columns.Count;
                    countRows = imports.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        recargaProdutoCompostos = new string[countRows, countField];
                        for (int i = 0; i < countRows; i++)//Linha
                        {
                            for (int j = 0; j < countField; j++) //Coluna
                            {
                                recargaProdutoCompostos[i, j] = Convert.ToString(imports.retornaSet().Tables[0].Rows[i][j]);
                            }
                        }
                        imports.fechaConexao();
                        for (int i = 0; i < recargaProdutoCompostos.GetLength(0); i++)//linha
                        {
                            if (conectorWeb_replace_produtoscompostos(recargaProdutoCompostos[i, 0], recargaProdutoCompostos[i, 1], recargaProdutoCompostos[i, 2], recargaProdutoCompostos[i, 3], recargaProdutoCompostos[i, 4], recargaProdutoCompostos[i, 5], recargaProdutoCompostos[i, 6], recargaProdutoCompostos[i, 7],
                                recargaProdutoCompostos[i, 8], recargaProdutoCompostos[i, 9], recargaProdutoCompostos[i, 10], recargaProdutoCompostos[i, 11], recargaProdutoCompostos[i, 12], recargaProdutoCompostos[i, 13], recargaProdutoCompostos[i, 14], recargaProdutoCompostos[i, 15],
                                recargaProdutoCompostos[i, 16], recargaProdutoCompostos[i, 17], recargaProdutoCompostos[i, 18], recargaProdutoCompostos[i, 19], recargaProdutoCompostos[i, 20], recargaProdutoCompostos[i, 21], recargaProdutoCompostos[i, 22], recargaProdutoCompostos[i, 23],
                                recargaProdutoCompostos[i, 24], recargaProdutoCompostos[i, 25]) == 0)
                            {
                                for (int j = 0; j < vetor.GetLength(0); j++)
                                {
                                    if (vetor[j, 0] == recargaProdutoCompostos[i, 0])
                                    {
                                        vetor[j, 2] = "*";
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                imports.fechaConexao();
            }

            for (i = 0; i < vetor.GetLength(0); i++)
            {
                if (recargaProduto.GetLength(0) > 0)
                {
                    if (vetor[i, 2] == "*")
                    {
                        conector_autentica_produto(recargaProduto[i, 0]);
                    }
                }
            }
        }
        #endregion

        #region //Complemento Terminal

        public int conectorWeb_replace_terminal(string replace_idterminal,
                                                string replace_idloja,
                                                string replace_idtypeTerminal,
                                                string replace_descricao,
                                                string replace_flagDesconto,
                                                string replace_status,
                                                string replace_operacao,
                                                string replace_multiTarefa
            )
        {
            auxConsistencia = 0;
            try
            {
                exports.abreConexao();
                exports.iniciarTransacao();
                exports.startTransaction("conectorWeb_replace_terminal");
                exports.addParametro("replace_idterminal", replace_idterminal);
                exports.addParametro("replace_idloja", replace_idloja);
                exports.addParametro("replace_idtypeTerminal", replace_idtypeTerminal);
                exports.addParametro("replace_descricao", replace_descricao);
                exports.addParametro("replace_flagDesconto", replace_flagDesconto);
                exports.addParametro("replace_status", replace_status);
                exports.addParametro("replace_operacao", replace_operacao);
                exports.addParametro("replace_multiTarefa", replace_multiTarefa);
                exports.procedimentoRead();
                if (replace_idtypeTerminal != "5")
                {
                    if (exports.retornaRead().Read() == true)
                    {
                        auxConsistencia = Convert.ToInt32(exports.retornaRead().GetString(0));
                    }
                }
                exports.fechaRead();
                exports.commit();

            }
            catch (Exception erro)
            {
                exports.rollback();
                //throw new Exception("ERRO BANCO DE DADOS: " + erro.Message.ToString());
                auxConsistencia = 1;
            }
            finally
            {
                exports.fechaConexao();

            }
            return auxConsistencia;
        }

        public void conectorWeb_replace_terminalecfconfig(
                                                         string replace_terminal,
                                                         string replace_caixa,
                                                         string replace_ipCaixa,
                                                         string replace_abeturaTroco,
                                                         string replace_imprimiCheque,
                                                         string replace_timeBlock,
                                                         string replace_blockTime,
                                                         string replace_trocaMercadoria,
                                                         string replace_carneRecebe,
                                                         string replace_codigoEmpresaTef,
                                                         string replace_trocoMax,
                                                         string replace_serie,
                                                         string replace_utilizaTeclado,
                                                         string replace_utilizaTef,
                                                         string replace_utilizaBalanca,
                                                         string replace_utilizaEcf,
                                                         string replace_portTef,
                                                         string replace_portBalanca,
                                                         string replace_portEcf,
                                                         string replace_funcaoProgramada,
                                                         string replace_msgTef,
                                                         string replace_idModeloEcf,
                                                         string replace_statusPdv,
                                                         string replace_autentica,
                                                         string replace_emiteVinculo,
                                                         string replace_vinculoCrediario,
                                                         string replace_vinculoConvenio,
                                                         string replace_vinculoCartaoCredito,
                                                         string replace_vinculoCartaoDebito,
                                                         string replace_typeTef,
                                                         string replace_alertaSangria,
                                                         string replace_valueSangria, string auxIdTypeTerminal
        )
        {
            try
            {
                exports.abreConexao();
                exports.iniciarTransacao();
                exports.startTransaction("conectorWeb_replace_terminalecfconfig");
                exports.addParametro("replace_terminal", replace_terminal);
                exports.addParametro("replace_caixa", replace_caixa);
                exports.addParametro("replace_ipCaixa", replace_ipCaixa);
                exports.addParametro("replace_abeturaTroco", replace_abeturaTroco);
                exports.addParametro("replace_imprimiCheque", replace_imprimiCheque);
                exports.addParametro("replace_timeBlock", replace_timeBlock);
                exports.addParametro("replace_blockTime", replace_blockTime);
                exports.addParametro("replace_trocaMercadoria", replace_trocaMercadoria);
                exports.addParametro("replace_carneRecebe", replace_carneRecebe);
                exports.addParametro("replace_codigoEmpresaTef", replace_codigoEmpresaTef);
                exports.addParametro("replace_trocoMax", replace_trocoMax.Replace(",", "."));
                exports.addParametro("replace_serie", replace_serie);
                exports.addParametro("replace_utilizaTeclado", replace_utilizaTeclado);
                exports.addParametro("replace_utilizaTef", replace_utilizaTef);
                exports.addParametro("replace_utilizaBalanca", replace_utilizaBalanca);
                exports.addParametro("replace_utilizaEcf", replace_utilizaEcf);
                exports.addParametro("replace_portTef", replace_portTef);
                exports.addParametro("replace_portBalanca", replace_portBalanca);
                exports.addParametro("replace_portEcf", replace_portEcf);
                exports.addParametro("replace_funcaoProgramada", replace_funcaoProgramada);
                exports.addParametro("replace_msgTef", replace_msgTef);
                exports.addParametro("replace_idModeloEcf", replace_idModeloEcf);
                exports.addParametro("replace_statusPdv", replace_statusPdv);
                exports.addParametro("replace_autentica", replace_autentica);
                exports.addParametro("replace_emiteVinculo", replace_emiteVinculo);
                exports.addParametro("replace_vinculoCrediario", replace_vinculoCrediario);
                exports.addParametro("replace_vinculoConvenio", replace_vinculoConvenio);
                exports.addParametro("replace_vinculoCartaoCredito", replace_vinculoCartaoCredito);
                exports.addParametro("replace_vinculoCartaoDebito", replace_vinculoCartaoDebito);
                exports.addParametro("replace_typeTef", replace_typeTef);
                exports.addParametro("replace_alertaSangria", replace_alertaSangria);
                exports.addParametro("replace_valueSangria", replace_valueSangria.Replace(",", "."));
                if (auxIdTypeTerminal == "5")
                {
                    exports.procedimentoRead();
                }
                exports.fechaRead();
                exports.commit();

            }
            catch (Exception e)
            {
                exports.rollback();
                //throw new Exception("ERRO BANCO DE DADOS: " + e.Message.ToString());
                auxConsistencia = 1;
            }
            finally
            {
                exports.fechaConexao();
                if (auxConsistencia == 0)
                {
                }
            }
        }
        public int conector_passwd(string newPasswd, string hash, string idFuncionario)
        {
            auxConsistencia = 0;
            try
            {
                exports.abreConexao();
                exports.iniciarTransacao();
                exports.singleTransaction("update funcionario set passwd=AES_ENCRYPT(?str,?passwd) where idfuncionario=?Funcionario");
                exports.addParametro("?str", newPasswd);
                exports.addParametro("?passwd", hash);
                exports.addParametro("?Funcionario", idFuncionario);
                exports.procedimentoText();
                exports.commit();
            }
            catch (Exception e)
            {
                exports.rollback();
                //throw new Exception("ERRO BANCO DE DADOS: " + e.Message.ToString());
                auxConsistencia = 1;

            }
            finally
            {
                exports.fechaConexao();
                if (auxConsistencia == 0)
                {
                }
            }
            return auxConsistencia;
        }
        #endregion

        #region //Complemento Usuario

        public int conectorWeb_replace_usuario(
            string replace_idusuario,
              string replace_descricao,
              string replace_login,
              string replace_passwd,
              string replace_terminalVenda,
              string replace_terminalConsulta,
              string replace_terminalECF,
              string replace_terminalAnaliseCredito,
              string replace_terminalMataBurro,
              string replace_status,
              string replace_supervisor,
              string replace_cadastro,
              string replace_onlyLogon,
              string replace_defaultLoja
                                    )
        {
            try
            {
                auxConsistencia = 0;
                exports.abreConexao();
                exports.iniciarTransacao();
                exports.startTransaction("conectorWeb_replace_usuario");
                exports.addParametro("replace_idusuario", replace_idusuario);
                exports.addParametro("replace_descricao", replace_descricao);
                exports.addParametro("replace_login", replace_login);
                exports.addParametro("replace_passwd", replace_passwd);
                exports.addParametro("replace_terminalVenda", replace_terminalVenda);
                exports.addParametro("replace_terminalConsulta", replace_terminalConsulta);
                exports.addParametro("replace_terminalECF", replace_terminalECF);
                exports.addParametro("replace_terminalAnaliseCredito", replace_terminalAnaliseCredito);
                exports.addParametro("replace_terminalMataBurro", replace_terminalMataBurro);
                exports.addParametro("replace_status", replace_status);
                exports.addParametro("replace_supervisor", replace_supervisor);
                exports.addParametro("replace_cadastro", replace_cadastro);
                exports.addParametro("replace_onlyLogon", replace_onlyLogon);
                exports.addParametro("replace_defaultLoja", replace_defaultLoja);
                exports.procedimentoRead();
                if (exports.retornaRead().Read() == true)
                {
                    auxConsistencia = Convert.ToInt32(exports.retornaRead().GetString(0));
                }
                exports.fechaRead();
                exports.commit();
            }
            catch (Exception e)
            {
                exports.rollback();
//                throw new Exception("ERRO BANCO DE DADOS: " + e.Message.ToString());
                auxConsistencia = 1;

            }
            finally
            {
                exports.fechaConexao();
            }
            return auxConsistencia;
        }
        public void conectorWeb_replace_configuracao(string replace_idconfiguracao,
                                                            string replace_idusuario,
                                                            string replace_administradoraCartao,
                                                            string replace_banco,
                                                            string replace_caixa,
                                                            string replace_cargo,
                                                            string replace_cep,
                                                            string replace_convenios,
                                                            string replace_codicaoPgto,
                                                            string replace_contaCorrente,
                                                            string replace_complementoFiscal,
                                                            string replace_cliente,
                                                            string replace_escolaridade,
                                                            string replace_feriados,
                                                            string replace_finalizadoras,
                                                            string replace_fornecedor,
                                                            string replace_funcionario,
                                                            string replace_loja,
                                                            string replace_metodos,
                                                            string replace_profissao,
                                                            string replace_representante,
                                                            string replace_telefone,
                                                            string replace_terminal,
                                                            string replace_transportadora,
                                                            string replace_usuario,
                                                            string replace_veiculo,
                                                            string replace_produto,
                                                            string replace_setor,
                                                            string replace_grupo,
                                                            string replace_categoria,
                                                            string replace_compra,
                                                            string replace_maximo,
                                                            string replace_entrada,
                                                            string replace_precificacao,
                                                            string replace_transferencia,
                                                            string replace_movimentacaoEstoque,
                                                            string replace_saldoEstoque,
                                                            string replace_zeraEstoque,
                                                            string replace_operacaoEntrada,
                                                            string replace_tipoProduto,
                                                            string replace_trocaProduto,
                                                            string replace_contasReceber,
                                                            string replace_cartaoCredito,
                                                            string replace_cheque,
                                                            string replace_crediario,
                                                            string replace_devolucao,
                                                            string replace_caixaCadastro,
                                                            string replace_sitegra,
                                                            string replace_notaFiscal,
                                                            string replace_sped,
                                                            string replace_apuracaoImposto,
                                                            string replace_mapaResumo,
                                                            string replace_cfop,
                                                            string replace_aliquotaFiscal,
                                                            string replace_operacaoFaturamento,
                                                            string replace_mataBurro,
                                                            string replace_configuraNotaFiscal,
                                                            string replace_processamento,
                                                            string replace_tesouraria,
                                                            string replace_cupomFiscal,
                                                            string replace_controleReservas,
                                                            string replace_analiseCredito,
                                                            string replace_pdvSingle,
                                                            string replace_contasPagar,
                                                            string replace_trocaSenha,
                                                            string replace_liberacao,
                                                            string replace_cargas,
                                                            string replace_interfacePdv,
                                                            string replace_dre,
                                                            string replace_fluxoCaixa,
                                                            string replace_flashReserva,
                                                            string replace_flashVenda,
                                                            string replace_relatorios,
                                                            string replace_chequeDevolvido,
                                                            string replace_convenio,
                                                            string replace_log,
                                                            string replace_inclusao,
                                                            string replace_alteracao,
                                                            string replace_menuCadastro,
                                                            string replace_menuProduto,
                                                            string replace_menuFinanceiro,
                                                            string replace_menuFiscal,
                                                            string replace_menuFaturamento,
                                                            string replace_menuPagar,
                                                            string replace_menuUtilitario,
                                                            string replace_menuContabil,
                                                            string replace_menuVenda,
                                                            string replace_menuRelatorio,
                                                            string replace_inventario,
                                                            string replace_estoqueRede,
                                                            string replace_saldoCrediario,
                                                            string replace_CrediarioContrato,
                                                            string replace_CrediarioResumoContabil,
                                                            string replace_CrediarioInadimplencia,
                                                            string replace_CrediarioConfiguracao,
                                                            string replace_menuBoletos,
                                                            string replace_tableFiscal,
                                                            string replace_precoIndividual,
                                                            string replace_composto,
                                                            string replace_movimentoEstoque,
                                                            string replace_tipoPromocao,
                                                            string replace_promocao,
                                                            string replace_precoGrupo,
                                                            string replace_rota,
                                                            string replace_boletoBancario)
        {
            try
            {
                auxConsistencia = 0;
                exports.abreConexao();
                exports.iniciarTransacao();
                exports.startTransaction("conectorWeb_replace_configuracao");
                exports.addParametro("replace_idconfiguracao", replace_idconfiguracao);
                exports.addParametro("replace_idusuario", replace_idusuario);
                exports.addParametro("replace_administradoraCartao", replace_administradoraCartao);
                exports.addParametro("replace_banco", replace_banco);
                exports.addParametro("replace_caixa", replace_caixa);
                exports.addParametro("replace_cargo", replace_cargo);
                exports.addParametro("replace_cep", replace_cep);
                exports.addParametro("replace_convenios", replace_convenios);
                exports.addParametro("replace_codicaoPgto", replace_codicaoPgto);
                exports.addParametro("replace_contaCorrente", replace_contaCorrente);
                exports.addParametro("replace_complementoFiscal", replace_complementoFiscal);
                exports.addParametro("replace_cliente", replace_cliente);
                exports.addParametro("replace_escolaridade", replace_escolaridade);
                exports.addParametro("replace_feriados", replace_feriados);
                exports.addParametro("replace_finalizadoras", replace_finalizadoras);
                exports.addParametro("replace_fornecedor", replace_fornecedor);
                exports.addParametro("replace_funcionario", replace_funcionario);
                exports.addParametro("replace_loja", replace_loja);
                exports.addParametro("replace_metodos", replace_metodos);
                exports.addParametro("replace_profissao", replace_profissao);
                exports.addParametro("replace_representante", replace_representante);
                exports.addParametro("replace_telefone", replace_telefone);
                exports.addParametro("replace_terminal", replace_terminal);
                exports.addParametro("replace_transportadora", replace_transportadora);
                exports.addParametro("replace_usuario", replace_usuario);
                exports.addParametro("replace_veiculo", replace_veiculo);
                exports.addParametro("replace_produto", replace_produto);
                exports.addParametro("replace_setor", replace_setor);
                exports.addParametro("replace_grupo", replace_grupo);
                exports.addParametro("replace_categoria", replace_categoria);
                exports.addParametro("replace_compra", replace_compra);
                exports.addParametro("replace_maximo", replace_maximo);
                exports.addParametro("replace_entrada", replace_entrada);
                exports.addParametro("replace_precificacao", replace_precificacao);
                exports.addParametro("replace_transferencia", replace_transferencia);
                exports.addParametro("replace_movimentacaoEstoque", replace_movimentacaoEstoque);
                exports.addParametro("replace_saldoEstoque", replace_saldoEstoque);
                exports.addParametro("replace_zeraEstoque", replace_zeraEstoque);
                exports.addParametro("replace_operacaoEntrada", replace_operacaoEntrada);
                exports.addParametro("replace_tipoProduto", replace_tipoProduto);
                exports.addParametro("replace_trocaProduto", replace_trocaProduto);
                exports.addParametro("replace_contasReceber", replace_contasReceber);
                exports.addParametro("replace_cartaoCredito", replace_cartaoCredito);
                exports.addParametro("replace_cheque", replace_cheque);
                exports.addParametro("replace_crediario", replace_crediario);
                exports.addParametro("replace_devolucao", replace_devolucao);
                exports.addParametro("replace_caixaCadastro", replace_caixaCadastro);
                exports.addParametro("replace_sitegra", replace_sitegra);
                exports.addParametro("replace_notaFiscal", replace_notaFiscal);
                exports.addParametro("replace_sped", replace_sped);
                exports.addParametro("replace_apuracaoImposto", replace_apuracaoImposto);
                exports.addParametro("replace_mapaResumo", replace_mapaResumo);
                exports.addParametro("replace_cfop", replace_cfop);
                exports.addParametro("replace_aliquotaFiscal", replace_aliquotaFiscal);
                exports.addParametro("replace_operacaoFaturamento", replace_operacaoFaturamento);
                exports.addParametro("replace_mataBurro", replace_mataBurro);
                exports.addParametro("replace_configuraNotaFiscal", replace_configuraNotaFiscal);
                exports.addParametro("replace_processamento", replace_processamento);
                exports.addParametro("replace_tesouraria", replace_tesouraria);
                exports.addParametro("replace_cupomFiscal", replace_cupomFiscal);
                exports.addParametro("replace_controleReservas", replace_controleReservas);
                exports.addParametro("replace_analiseCredito", replace_analiseCredito);
                exports.addParametro("replace_pdvSingle", replace_pdvSingle);
                exports.addParametro("replace_contasPagar", replace_contasPagar);
                exports.addParametro("replace_trocaSenha", replace_trocaSenha);
                exports.addParametro("replace_liberacao", replace_liberacao);
                exports.addParametro("replace_cargas", replace_cargas);
                exports.addParametro("replace_interfacePdv", replace_interfacePdv);
                exports.addParametro("replace_dre", replace_dre);
                exports.addParametro("replace_fluxoCaixa", replace_fluxoCaixa);
                exports.addParametro("replace_flashReserva", replace_flashReserva);
                exports.addParametro("replace_flashVenda", replace_flashVenda);
                exports.addParametro("replace_relatorios", replace_relatorios);
                exports.addParametro("replace_chequeDevolvido", replace_chequeDevolvido);
                exports.addParametro("replace_convenio", replace_convenio);
                exports.addParametro("replace_log", replace_log);
                exports.addParametro("replace_inclusao", replace_inclusao);
                exports.addParametro("replace_alteracao", replace_alteracao);
                exports.addParametro("replace_menuCadastro", replace_menuCadastro);
                exports.addParametro("replace_menuProduto", replace_menuProduto);
                exports.addParametro("replace_menuFinanceiro", replace_menuFinanceiro);
                exports.addParametro("replace_menuFiscal", replace_menuFiscal);
                exports.addParametro("replace_menuFaturamento", replace_menuFaturamento);
                exports.addParametro("replace_menuPagar", replace_menuPagar);
                exports.addParametro("replace_menuUtilitario", replace_menuUtilitario);
                exports.addParametro("replace_menuContabil", replace_menuContabil);
                exports.addParametro("replace_menuVenda", replace_menuVenda);
                exports.addParametro("replace_menuRelatorio", replace_menuRelatorio);
                exports.addParametro("replace_inventario", replace_inventario);
                exports.addParametro("replace_estoqueRede", replace_estoqueRede);
                exports.addParametro("replace_saldoCrediario", replace_saldoCrediario);
                exports.addParametro("replace_CrediarioContrato", replace_CrediarioContrato);
                exports.addParametro("replace_CrediarioResumoContabil", replace_CrediarioResumoContabil);
                exports.addParametro("replace_CrediarioInadimplencia", replace_CrediarioInadimplencia);
                exports.addParametro("replace_CrediarioConfiguracao", replace_CrediarioConfiguracao);
                exports.addParametro("replace_menuBoletos", replace_menuBoletos);
                exports.addParametro("replace_tableFiscal", replace_tableFiscal);
                exports.addParametro("replace_precoIndividual", replace_precoIndividual);
                exports.addParametro("replace_composto", replace_composto);
                exports.addParametro("replace_movimentoEstoque", replace_movimentoEstoque);
                exports.addParametro("replace_tipoPromocao", replace_tipoPromocao);
                exports.addParametro("replace_promocao", replace_promocao);
                exports.addParametro("replace_precoGrupo", replace_precoGrupo);
                exports.addParametro("replace_rota", replace_rota);
                exports.addParametro("replace_boletoBancario", replace_boletoBancario);
                exports.procedimentoRead();
                exports.fechaRead();
                exports.commit();
            }
            catch (Exception e)
            {
                exports.rollback();
                auxConsistencia = 1;
            }
            finally
            {
                exports.fechaConexao();
                if (auxConsistencia == 0)
                {
                }
            }
        }

        public void conector_passwd_usuario(string newPasswd, string hash, string idUser)
        {
            int teste = 0;
            try
            {
                exports.abreConexao();
                exports.iniciarTransacao();
                exports.singleTransaction("update usuario set passwd=AES_ENCRYPT(?str,?passwd) where idUsuario=?Usuario");
                exports.addParametro("?str", newPasswd);
                exports.addParametro("?passwd", hash);
                exports.addParametro("?Usuario", idUser);
                exports.procedimentoText();
                exports.commit();
            }
            catch (Exception erro)
            {
                exports.rollback();
                teste = 1;
            }
            finally
            {
                exports.fechaConexao();
                if (teste == 0)
                {
                }

            }
        }
        #endregion


        #region //TransmissaoCupom
        public void conector_saidas_cupom(int tipo)
        {
            timedate fuso = new timedate();
            string[,] recarga03 = new string[0, 0];//Fechamento
            string[,] recarga04 = new string[0, 0];//MovimentoCaixa
            string[,] recarga05 = new string[0, 0];//MovimentoDia
            string[,] recarga06 = new string[0, 0];//Cartao
            string[,] recarga07 = new string[0, 0];//Crediario
            string[,] recarga08 = new string[0, 0];//Cheque
            string[,] recarga09 = new string[0, 0];//Convenio
            string[,] vetor = new string[0, 0];
            string[,] vetor00 = new string[0, 0];
            string[,] vetor01 = new string[0, 0];
            string[,] vetor02 = new string[0, 0];
            string[,] vetor03 = new string[0, 0];

            switch (tipo)
            {
                case 1:
                    //Fechamento Caixa
                    try
                    {
                        auxConsistencia = 0;
                        imports.abreConexao();
                        imports.singleTransaction("select tab.* from fechamentoCaixa tab where tab.importCarga=0 and tab.loja=?store");
                        imports.addParametro("?store", store);
                        imports.procedimentoSet();

                    }
                    catch (Exception erro) { auxConsistencia = 1; }
                    finally
                    {
                        if (auxConsistencia == 0)
                        {
                            countField = imports.retornaSet().Tables[0].Columns.Count;
                            countRows = imports.retornaSet().Tables[0].DefaultView.Count;
                            if (countRows > 0)
                            {
                                recarga03 = new string[countRows, countField];
                                for (int i = 0; i < countRows; i++)//Linha
                                {
                                    for (int j = 0; j < countField; j++) //Coluna
                                    {
                                        recarga03[i, j] = Convert.ToString(imports.retornaSet().Tables[0].Rows[i][j]);
                                    }
                                }
                                imports.fechaConexao();
                                for (int i = 0; i < recarga03.GetLength(0); i++)//Linha
                                {
                                    if (conectorWEB_replace_fechamento_caixa(recarga03[i, 0], recarga03[i, 1],
                                        recarga03[i, 2], recarga03[i, 3],
                                        recarga03[i, 4], recarga03[i, 5], recarga03[i, 6],
                                        recarga03[i, 7], recarga03[i, 8],
                                        recarga03[i, 9], recarga03[i, 10],
                                        recarga03[i, 11], recarga03[i, 12],
                                        recarga03[i, 13], recarga03[i, 14],
                                        recarga03[i, 15], recarga03[i, 16],
                                        recarga03[i, 17], recarga03[i, 18],
                                        recarga03[i, 19], recarga03[i, 20]) == true)
                                    {

                                    }
                                }
                            }
                        }

                        imports.fechaConexao();
                    }
                    break;
                case 2:
                    //Fechamento Caixa
                    try
                    {
                        string title = "";
                        auxConsistencia = 0;
                        title = imports.abreConexao();
                        if (title == "IMPOSSÍVEL ESTABELECER CONEXÃO")
                        {
                            auxConsistencia = 1;
                            return;
                        }
                        imports.singleTransaction("select tab.* from movimentoCaixa tab where tab.importaCarga=0 and tab.loja=?store");
                        imports.addParametro("?store", store);
                        imports.procedimentoSet();

                    }
                    catch (Exception erro) { auxConsistencia = 1; }
                    finally
                    {
                        if (auxConsistencia == 0)
                        {
                            countField = imports.retornaSet().Tables[0].Columns.Count;
                            countRows = imports.retornaSet().Tables[0].DefaultView.Count;
                            if (countRows > 0)
                            {
                                recarga04 = new string[countRows, countField];
                                for (int i = 0; i < countRows; i++)//Linha
                                {
                                    for (int j = 0; j < countField; j++) //Coluna
                                    {
                                        recarga04[i, j] = Convert.ToString(imports.retornaSet().Tables[0].Rows[i][j]);
                                    }
                                }
                                imports.fechaConexao();
                                for (int i = 0; i < recarga04.GetLength(0); i++)//Linha
                                {
                                    if (conectorWEB_replace_movimento_caixa(recarga04[i, 0], recarga04[i, 1],
                                        recarga04[i, 2], recarga04[i, 3],
                                        recarga04[i, 4], recarga04[i, 5], recarga04[i, 6],
                                        recarga04[i, 7], recarga04[i, 8],
                                        recarga04[i, 9], recarga04[i, 10],
                                        recarga04[i, 11], recarga04[i, 12],
                                        recarga04[i, 13], recarga04[i, 14],
                                        recarga04[i, 15], recarga04[i, 16],
                                        recarga04[i, 17], recarga04[i, 18],
                                        recarga04[i, 19], recarga04[i, 20], recarga04[i, 21], recarga04[i, 22]) == true)
                                    {

                                    }
                                }
                            }
                        }

                        imports.fechaConexao();
                    }
                    break;
                case 3:
                    //Movimento Caixa
                    try
                    {
                        auxConsistencia = 0;
                        imports.abreConexao();
                        imports.singleTransaction("select tab.* from movimentodia tab where tab.importaCarga=0 and tab.idloja=?store");
                        imports.addParametro("?store", store);
                        imports.procedimentoSet();

                    }
                    catch (Exception erro) { auxConsistencia = 1; }
                    finally
                    {
                        if (auxConsistencia == 0)
                        {
                            countField = imports.retornaSet().Tables[0].Columns.Count;
                            countRows = imports.retornaSet().Tables[0].DefaultView.Count;
                            if (countRows > 0)
                            {
                                recarga05 = new string[countRows, countField];
                                for (int i = 0; i < countRows; i++)//Linha
                                {
                                    for (int j = 0; j < countField; j++) //Coluna
                                    {
                                        recarga05[i, j] = Convert.ToString(imports.retornaSet().Tables[0].Rows[i][j]);
                                    }
                                }
                                imports.fechaConexao();
                                for (int i = 0; i < recarga05.GetLength(0); i++)//Linha
                                {
                                    if (conectorWEB_replace_movimento_dia(recarga05[i, 0], recarga05[i, 1],
                                        recarga05[i, 2], recarga05[i, 3],
                                        recarga05[i, 4], recarga05[i, 5], recarga05[i, 6],
                                        recarga05[i, 7], recarga05[i, 8],
                                        recarga05[i, 9], recarga05[i, 10],
                                        recarga05[i, 11], recarga05[i, 12],
                                        recarga05[i, 13], recarga05[i, 14],
                                        recarga05[i, 15], recarga05[i, 16]) == true)
                                    {

                                    }
                                }
                            }
                        }

                        imports.fechaConexao();
                    }
                    break;

                case 4:
                    //Cartao
                    try
                    {
                        string title = "";
                        auxConsistencia = 0;
                        if (imports.statusSchema() == 1)
                        {
                            auxConsistencia = 1;
                            return;
                        }

                        title = imports.abreConexao();
                        if (title == "IMPOSSÍVEL ESTABELECER CONEXÃO")
                        {
                            auxConsistencia = 1;
                            return;
                        }
                        imports.singleTransaction("select tab.* from cartao tab where tab.importaCarga=0 and tab.idloja=?store");
                        imports.addParametro("?store", store);
                        imports.procedimentoSet();
                    }
                    catch (Exception erro) { auxConsistencia = 1; }
                    finally
                    {
                        if (auxConsistencia == 0)
                        {
                            countField = imports.retornaSet().Tables[0].Columns.Count;
                            countRows = imports.retornaSet().Tables[0].DefaultView.Count;
                            if (countRows > 0)
                            {
                                vetor = new string[countRows, 7];
                                recarga06 = new string[countRows, countField];
                                for (int i = 0; i < countRows; i++)//Linha
                                {
                                    for (int j = 0; j < countField; j++) //Coluna
                                    {
                                        recarga06[i, j] = Convert.ToString(imports.retornaSet().Tables[0].Rows[i][j]);
                                    }
                                }
                                imports.fechaConexao();
                                for (int i = 0; i < recarga06.GetLength(0); i++)//Linha
                                {
                                    if (conectorWEB_replace_cartao(recarga06[i, 0], recarga06[i, 1],
                                        recarga06[i, 2], recarga06[i, 3],
                                        recarga06[i, 4], recarga06[i, 5], recarga06[i, 6],
                                        recarga06[i, 7], recarga06[i, 8],
                                        recarga06[i, 9], recarga06[i, 10],
                                        recarga06[i, 11], recarga06[i, 12],
                                        recarga06[i, 13], recarga06[i, 14],
                                        recarga06[i, 15], recarga06[i, 16],
                                        recarga06[i, 17], recarga06[i, 18],
                                        recarga06[i, 19], recarga06[i, 20],
                                        recarga06[i, 21], recarga06[i, 22],
                                        recarga06[i, 23], recarga06[i, 24],
                                        recarga06[i, 25], recarga06[i, 26],
                                        recarga06[i, 27], recarga06[i, 28], recarga06[i, 29]) == true)
                                    {
                                        vetor[i, 0] = recarga06[i, 1];//id
                                        vetor[i, 1] = recarga06[i, 2];//Loja
                                        vetor[i, 2] = recarga06[i, 3];//inclusao
                                        vetor[i, 3] = recarga06[i, 5];//Cupom
                                        vetor[i, 4] = recarga06[i, 7];//terminal
                                        vetor[i, 5] = recarga06[i, 8];//emissao
                                        vetor[i, 6] = "#";

                                    }
                                }//Firts For
                                for (int i = 0; i < vetor.GetLength(0); i++)
                                {
                                    if (vetor[i, 6] == "#")
                                    {
                                        conector_autentica_cartao_baixa(vetor[i, 0], vetor[i, 3], vetor[i, 1], vetor[i, 4], vetor[i, 2], vetor[i, 5]);
                                    }
                                }
                            }
                        }

                        imports.fechaConexao();
                    }
                    break;
                    case 5:
                    //Crediario
                    try
                    {
                        auxConsistencia = 0;
                        imports.abreConexao();
                        imports.singleTransaction("select tab.* from crediario tab where tab.importaCarga=0 and tab.idloja=?store");
                        imports.addParametro("?store", store);
                        imports.procedimentoSet();
                    }
                    catch (Exception erro) { auxConsistencia = 1; }
                    finally
                    {
                        if (auxConsistencia == 0)
                        {
                            countField = imports.retornaSet().Tables[0].Columns.Count;
                            countRows = imports.retornaSet().Tables[0].DefaultView.Count;
                            if (countRows > 0)
                            {
                                vetor00 = new string[countRows, 2];
                                recarga07 = new string[countRows, countField];
                                for (int i = 0; i < countRows; i++)//Linha
                                {
                                    for (int j = 0; j < countField; j++) //Coluna
                                    {
                                        recarga07[i, j] = Convert.ToString(imports.retornaSet().Tables[0].Rows[i][j]);
                                    }
                                }
                                imports.fechaConexao();
                                for (int i = 0; i < recarga07.GetLength(0); i++)//Linha
                                {
                                    if (conectorWeb_replace_crediario(recarga07[i, 1],
                                        recarga07[i, 2], recarga07[i, 3],
                                        recarga07[i, 4], recarga07[i, 5], recarga07[i, 6],
                                        recarga07[i, 7], recarga07[i, 8],
                                        recarga07[i, 9], recarga07[i, 10],
                                        recarga07[i, 11], recarga07[i, 12],
                                        recarga07[i, 13], recarga07[i, 14],
                                        recarga07[i, 15], recarga07[i, 16],
                                        recarga07[i, 17], recarga07[i, 18],
                                        recarga07[i, 19], recarga07[i, 20],
                                        recarga07[i, 21], recarga07[i, 22],
                                        recarga07[i, 23], recarga07[i, 24]) == true)
                                    {
                                        vetor00[i, 0] = recarga07[i, 0];//idLoja
                                        vetor00[i, 1] = "#";

                                    }
                                }//Firts For
                                for (int i = 0; i < vetor00.GetLength(0); i++)
                                {
                                    if (vetor00[i, 1] == "#")
                                    {
                                        conector_autentica_crediario_baixa(vetor00[i, 0]);
                                    }
                                }
                            }
                        }

                        imports.fechaConexao();
                    }
                    break;
                    case 6:
                    //Cheque
                    try
                    {
                        auxConsistencia = 0;
                        imports.abreConexao();
                        imports.singleTransaction("select tab.* from cheque tab where tab.importaCarga=0 and tab.idloja=?store");
                        imports.addParametro("?store", store);
                        imports.procedimentoSet();
                    }
                    catch (Exception erro) { auxConsistencia = 1; }
                    finally
                    {
                        if (auxConsistencia == 0)
                        {
                            countField = imports.retornaSet().Tables[0].Columns.Count;
                            countRows = imports.retornaSet().Tables[0].DefaultView.Count;
                            if (countRows > 0)
                            {
                                vetor01 = new string[countRows, 2];
                                recarga08 = new string[countRows, countField];
                                for (int i = 0; i < countRows; i++)//Linha
                                {
                                    for (int j = 0; j < countField; j++) //Coluna
                                    {
                                        recarga08[i, j] = Convert.ToString(imports.retornaSet().Tables[0].Rows[i][j]);
                                    }
                                }
                                imports.fechaConexao();
                                for (int i = 0; i < recarga08.GetLength(0); i++)//Linha
                                {
                                    if (conectorWeb_replace_cheque(
                                        recarga08[i, 0], recarga08[i, 1],
                                        recarga08[i, 2], recarga08[i, 3],
                                        recarga08[i, 4], recarga08[i, 5], recarga08[i, 6],
                                        recarga08[i, 7], recarga08[i, 8],
                                        recarga08[i, 9], recarga08[i, 10],
                                        recarga08[i, 11], recarga08[i, 12],
                                        recarga08[i, 13], recarga08[i, 14],
                                        recarga08[i, 15], recarga08[i, 16],
                                        recarga08[i, 17], recarga08[i, 18],
                                        recarga08[i, 19], recarga08[i, 20],
                                        recarga08[i, 21], recarga08[i, 22],
                                        recarga08[i, 23], recarga08[i, 24],
                                        recarga08[i, 25], recarga08[i, 26],
                                        recarga08[i, 27], recarga08[i, 28],
                                        recarga08[i, 29], recarga08[i, 30],
                                        recarga08[i, 31], recarga08[i, 32],
                                        recarga08[i, 33], recarga08[i, 34],
                                        recarga08[i, 35], recarga08[i, 36],
                                        recarga08[i, 37]) == true)
                                    {
                                        vetor01[i, 0] = recarga08[i, 0];//chave
                                        vetor01[i, 1] = "#";

                                    }
                                }//Firts For
                                for (int i = 0; i < vetor01.GetLength(0); i++)
                                {
                                    if (vetor01[i, 1] == "#")
                                    {
                                        conector_autentica_cheque_baixa(vetor01[i, 0]);
                                    }
                                }
                            }
                        }

                        imports.fechaConexao();
                    }
                    break;
                case 7://Convenio
                    break;
                default:
                    break;
            }
        }
        public void conector_saidas_cupom()
        {
            string[,] vetor = new string[0, 0];
            string[,] vetor00 = new string[0, 0];
            string[,] vetor01 = new string[0, 0];
            string[,] recarga = new string[0, 0];//cabecalho
            string[,] recarga01 = new string[0, 0];//Detalhes
            string[,] recarga02 = new string[0, 0];//MovimentoCupom

            bool valida = false;

            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                imports.singleTransaction("select tab.* from cupom_cabecalho tab where tab.importCarga=0 and tab.loja=?store");
                imports.addParametro("?store", store);
                imports.procedimentoSet();

            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = imports.retornaSet().Tables[0].Columns.Count;
                    countRows = imports.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        vetor = new string[countRows, 5];
                        recarga = new string[countRows, countField];
                        for (int i = 0; i < countRows; i++)//Linha
                        {
                            for (int j = 0; j < countField; j++) //Coluna
                            {
                                recarga[i, j] = Convert.ToString(imports.retornaSet().Tables[0].Rows[i][j]);
                            }
                        }
                        imports.fechaConexao();
                        for (int i = 0; i < recarga.GetLength(0); i++)//Linha
                        {
                            if (conectorWEB_replace_cupom_cabecalho(recarga[i, 0], recarga[i, 1], recarga[i, 2], recarga[i, 3], 
                                recarga[i, 4], recarga[i, 5], recarga[i, 6], 
                                recarga[i, 7], recarga[i, 8], 
                                recarga[i, 9], recarga[i, 10], 
                                recarga[i, 11], recarga[i, 12], 
                                recarga[i, 13], recarga[i, 14], 
                                recarga[i, 15], recarga[i, 16], 
                                recarga[i, 17], recarga[i, 18], 
                                recarga[i, 19], recarga[i, 20], 
                                recarga[i, 21], recarga[i, 22], 
                                recarga[i, 23], recarga[i, 24], 
                                recarga[i, 25], recarga[i, 26], 
                                recarga[i, 27], recarga[i, 28], 
                                recarga[i, 29], recarga[i, 30], 
                                recarga[i, 31], recarga[i, 32], 
                                recarga[i, 33], recarga[i, 34], 
                                recarga[i, 35], recarga[i, 36], 
                                recarga[i, 37], recarga[i, 38]) == true)
                            {
                                vetor[i, 0] = recarga[i, 0];
                                vetor[i, 1] = recarga[i, 1];
                                vetor[i, 2] = recarga[i, 2];
                                vetor[i, 3] = recarga[i, 3];
                                vetor[i, 4] = "#";
                            }
                        }
                    }
                }

                imports.fechaConexao();
            }

            //cupom_detalhes
            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                //imports.singleTransaction("select tab1.* from cupom_cabecalho tab inner join cupom_detalhes tab1 on(tab.numeroCupom = tab1.numeroCupom and tab.loja = tab1.loja and tab.terminal = tab1.terminal and tab.dataVenda = tab1.dataVenda) where tab1.importaCarga=0 and tab.loja=?store");
                imports.singleTransaction("select * from conectorPDV.cupom_detalhes  where importaCarga=0 and loja=?store");
                imports.addParametro("?store", store);
                imports.procedimentoSet();

            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = imports.retornaSet().Tables[0].Columns.Count;
                    countRows = imports.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        vetor01 = new string[countRows, 5];
                        recarga01 = new string[countRows, countField];
                        for (int i = 0; i < countRows; i++)//Linha
                        {
                            for (int j = 0; j < countField; j++) //Coluna
                            {
                                recarga01[i, j] = Convert.ToString(imports.retornaSet().Tables[0].Rows[i][j]);
                            }
                        }
                        imports.fechaConexao();
                        for (int i = 0; i < recarga01.GetLength(0); i++)//Linha
                        {
                            if (conectorWEB_replace_cupom_detalhes(recarga01[i, 0], recarga01[i, 1], recarga01[i, 2], recarga01[i, 3], recarga01[i, 4], recarga01[i, 5], recarga01[i, 6], recarga01[i, 7], recarga01[i, 8], recarga01[i, 9], recarga01[i, 10], recarga01[i, 11], recarga01[i, 12], recarga01[i, 13], recarga01[i, 14], recarga01[i, 15], recarga01[i, 16], recarga01[i, 17], recarga01[i, 18], recarga01[i, 19], recarga01[i, 20], recarga01[i, 21], recarga01[i, 22], recarga01[i, 23], recarga01[i, 24], recarga01[i, 25], recarga01[i, 26], recarga01[i, 27], recarga01[i, 28], recarga01[i, 29], recarga01[i, 30], recarga01[i, 31], recarga01[i, 32], recarga01[i, 33], recarga01[i, 34], recarga01[i, 35],
                                recarga01[i, 36], recarga01[i, 37], recarga01[i, 38], recarga01[i, 39],recarga01[i, 40],recarga01[i, 41]) == true)
                            {
                                vetor01[i, 0] = recarga01[i, 0];
                                vetor01[i, 1] = recarga01[i, 1];
                                vetor01[i, 2] = recarga01[i, 2];
                                vetor01[i, 3] = recarga01[i, 3];
                                vetor01[i, 4] = "#";
                            }
                        }
                    }
                }
                imports.fechaConexao();
            }

            //cupom_movimento
            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                //imports.singleTransaction("select tab1.* from cupom_cabecalho tab inner join cupom_movimento tab1 on(tab.numeroCupom = tab1.numeroCupom and tab.loja = tab1.loja and tab.terminal = tab1.terminal and tab.dataVenda = tab1.dataVenda) where tab1.importaCarga=0 and tab.loja=?store");
                imports.singleTransaction("select * from conectorPDV.cupom_movimento  where importaCarga=0 and loja=?store");
                imports.addParametro("?store", store);
                imports.procedimentoSet();

            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = imports.retornaSet().Tables[0].Columns.Count;
                    countRows = imports.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        vetor00 = new string[countRows, 5];
                        recarga02 = new string[countRows, countField];
                        for (int i = 0; i < countRows; i++)//Linha
                        {
                            for (int j = 0; j < countField; j++) //Coluna
                            {
                                recarga02[i, j] = Convert.ToString(imports.retornaSet().Tables[0].Rows[i][j]);
                            }
                        }
                        imports.fechaConexao();
                        for (int i = 0; i < recarga02.GetLength(0); i++)//Linha
                        {
                            if (conectorWEB_replace_cupom_movimento(recarga02[i, 0], recarga02[i, 1], 
                                recarga02[i, 2], recarga02[i, 3], 
                                recarga02[i, 4], recarga02[i, 5], 
                                recarga02[i, 6], recarga02[i, 7], 
                                recarga02[i, 8], recarga02[i, 9], 
                                recarga02[i, 10], recarga02[i, 11], 
                                recarga02[i, 12], recarga02[i, 13], 
                                recarga02[i, 14], recarga02[i, 15], 
                                recarga02[i, 16], recarga02[i, 17], 
                                recarga02[i, 18], recarga02[i, 19], 
                                recarga02[i, 20], recarga02[i, 21], 
                                recarga02[i, 22], recarga02[i, 23],
                                recarga02[i, 24], recarga02[i, 25], 
                                recarga02[i, 26], recarga02[i, 27], 
                                recarga02[i, 28], recarga02[i, 29], 
                                recarga02[i, 30], recarga02[i, 31]) == true)
                            {
                                vetor00[i, 0] = recarga02[i, 0];
                                vetor00[i, 1] = recarga02[i, 1];
                                vetor00[i, 2] = recarga02[i, 2];
                                vetor00[i, 3] = recarga02[i, 3];
                                vetor00[i, 4] = "#";
                            }
                        }
                    }
                }
                imports.fechaConexao();
            }

            for (int i = 0; i < vetor.GetLength(0); i++)
            {
                if (recarga.GetLength(0) > 0 && recarga01.GetLength(0) > 0)
                {
                    if (vetor[i, 4] == "#")
                    {
                        conector_autentica_cupom_fiscal_cabecalho_baixa(recarga[i, 0], recarga[i, 1],recarga[i, 2],recarga[i, 3]);
                    }
                }
            }

            for (int i = 0; i < vetor01.GetLength(0); i++)
            {
                if (vetor01[i, 4] == "#")
                {
                    conector_autentica_cupom_fiscal_detalhes_baixa(recarga01[i, 0], recarga01[i, 1], recarga01[i, 2], recarga01[i, 3]);
                }
            }

            for (int i = 0; i < vetor00.GetLength(0); i++)
            {
                if (vetor00[i, 4] == "#")
                {
                    conector_autentica_cupom_fiscal_movimento_baixa(recarga02[i, 0], recarga02[i, 1], recarga02[i, 2], recarga02[i, 3]);
                }
            }
            alwaysVariables.trava = 0;
        }

        public void conector_autentica_cupom_fiscal_cabecalho_baixa(string chave, string store, string pdv, string dat)
        {
            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                imports.singleTransaction("update cupom_cabecalho tab set tab.importCarga=1 where tab.numeroCupom=?chave and tab.importCarga=0 and tab.loja=?store and tab.terminal=?terminal and tab.dataVenda=?dat");
                imports.addParametro("?chave", chave);
                imports.addParametro("?store", store);
                imports.addParametro("?terminal", pdv);
                imports.addParametro("?dat", String.Format("{0:yyyyMMdd}", Convert.ToDateTime(dat)));
                imports.procedimentoRead();
                if (imports.retornaRead().Read() == true)
                {
                }

            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                }
                imports.fechaConexao();
            }
        }

        public void conector_autentica_cupom_fiscal_detalhes_baixa(string chave, string store, string pdv, string dat)
        {
            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                imports.singleTransaction("update cupom_detalhes tab set tab.importaCarga=1 where tab.numeroCupom=?chave and tab.importaCarga=0 and tab.loja=?store and tab.terminal=?terminal and tab.dataVenda=?dat");
                imports.addParametro("?chave", chave);
                imports.addParametro("?store", store);
                imports.addParametro("?terminal", pdv);
                imports.addParametro("?dat", String.Format("{0:yyyyMMdd}", Convert.ToDateTime(dat)));
                imports.procedimentoRead();
                if (imports.retornaRead().Read() == true)
                {
                }

            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                }
                imports.fechaConexao();
            }
        }

        public void conector_autentica_cupom_fiscal_movimento_baixa(string chave, string store, string pdv, string dat)
        {
            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                imports.singleTransaction("update cupom_movimento tab set tab.importaCarga=1 where tab.numeroCupom=?chave and tab.importaCarga=0 and tab.loja=?store and tab.terminal=?terminal and tab.dataVenda=?dat");
                imports.addParametro("?chave", chave);
                imports.addParametro("?store", store);
                imports.addParametro("?terminal", pdv);
                imports.addParametro("?dat", String.Format("{0:yyyyMMdd}", Convert.ToDateTime(dat)));
                imports.procedimentoRead();
                if (imports.retornaRead().Read() == true)
                {
                }
            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                }
                imports.fechaConexao();
            }
        }

        public void conector_autentica_cartao_baixa(string chave, string cupom, string store, string pdv, string dat, string dat1)
        {
            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                imports.singleTransaction("update CARTAO set importaCarga=1 where idAdministradora=?chave and idLoja=?store and inclusao=?dat and cupom=?cupom and terminal=?pdv and emissao=?dat1");
                imports.addParametro("?chave", chave);
                imports.addParametro("?store", store);
                imports.addParametro("?pdv", pdv);
                imports.addParametro("?cupom", cupom);
                imports.addParametro("?dat", String.Format("{0:yyyyMMdd}", Convert.ToDateTime(dat)));
                imports.addParametro("?dat1", String.Format("{0:yyyyMMdd}", Convert.ToDateTime(dat1)));
                imports.procedimentoRead();
                if (imports.retornaRead().Read() == true)
                {
                }

            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                }
                imports.fechaConexao();
            }
        }

        public void conector_autentica_crediario_baixa(string chave)
        {
            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                imports.singleTransaction("update crediario set importaCarga=1 where idcrediario=?dat1");
                imports.addParametro("?chave", chave);
                imports.procedimentoRead();
                if (imports.retornaRead().Read() == true)
                {
                }
            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                }
                imports.fechaConexao();
            }
        }

        public void conector_autentica_cheque_baixa(string chave)
        {
            try
            {
                auxConsistencia = 0;
                imports.abreConexao();
                imports.singleTransaction("update Cheque set importaCarga=1 where idCheque=?chave");
                imports.addParametro("?chave", chave);
                imports.procedimentoRead();
                if (imports.retornaRead().Read() == true)
                {
                }

            }
            catch (Exception erro) { auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                }
                imports.fechaConexao();
            }
        }
        protected bool conectorWEB_replace_cupom_cabecalho(
  string replace_numeroCupom,
  string replace_loja,
  string replace_terminal,
  string replace_dataVenda,
  string replace_cliente,
  string replace_hora,
  string replace_acrescimo,
  string replace_desconto,
  string replace_cancelados,
  string replace_totalLiquido,
  string replace_totalBruto,
  string replace_operador,
  string replace_situacao,
  string replace_documentoCPF_CNPJ,
  string replace_usuario_cancelamento,
  string replace_motivo_cancelamento,
  string replace_totalCancelado,
  string replace_convenio,
  string replace_conveniado,
  string replace_numero_cartao,
  string replace_forma_recebimento,
  string replace_dataVencimento,
  string replace_encargos,
  string replace_diasAtraso,
  string replace_contrato,
  string replace_parcela,
  string replace_totalParcela,
  string replace_geraEntrega,
  string replace_lagradouro_entrega,
  string replace_datetimefinalvenda,
  string replace_notaFiscal,
  string replace_serie,
  string replace_retorno,
  string replace_totalItens,
  string replace_pedido,
  string replace_ccf,
  string replace_cripto,
  string replace_modelo_ecf,
  string replace_importCarga
            )
        {
            bool valida = false;
            try
            {
                auxConsistencia = 0;

                if (alwaysVariables.trava == 1)
                {
                    return false;
                }
                else
                {
                    if(exports.abreConexao() == false)
                    {
                        return false;
                    }
                }
                exports.startTransaction("conectorWEB_replace_cupom_cabecalho");
                exports.addParametro("replace_numeroCupom", replace_numeroCupom);
                exports.addParametro("replace_loja", replace_loja);
                exports.addParametro("replace_terminal", replace_terminal);
                exports.addParametro("replace_dataVenda", String.Format("{0:yyyyMMdd}", Convert.ToDateTime(replace_dataVenda)));
                exports.addParametro("replace_cliente", replace_cliente == "" ? "0" : replace_cliente);
                exports.addParametro("replace_hora", replace_hora);
                exports.addParametro("replace_acrescimo", replace_acrescimo.Replace(",", "."));
                exports.addParametro("replace_desconto", replace_desconto.Replace(",", "."));
                exports.addParametro("replace_cancelados", replace_cancelados);
                exports.addParametro("replace_totalLiquido", replace_totalLiquido.Replace(",", "."));
                exports.addParametro("replace_totalBruto", replace_totalBruto.Replace(",", "."));
                exports.addParametro("replace_operador", replace_operador);
                exports.addParametro("replace_situacao", replace_situacao);
                exports.addParametro("replace_documentoCPF_CNPJ", replace_documentoCPF_CNPJ);
                exports.addParametro("replace_usuario_cancelamento", replace_usuario_cancelamento);
                exports.addParametro("replace_motivo_cancelamento", replace_motivo_cancelamento);
                exports.addParametro("replace_totalCancelado", replace_totalCancelado.Replace(",", "."));
                exports.addParametro("replace_convenio", replace_convenio);
                exports.addParametro("replace_conveniado", replace_conveniado);
                exports.addParametro("replace_numero_cartao", replace_numero_cartao);
                exports.addParametro("replace_forma_recebimento", replace_forma_recebimento);
                exports.addParametro("replace_dataVencimento", replace_dataVencimento);
                exports.addParametro("replace_encargos", replace_encargos.Replace(",", "."));
                exports.addParametro("replace_diasAtraso", replace_diasAtraso);
                exports.addParametro("replace_contrato", replace_contrato);
                exports.addParametro("replace_parcela", replace_parcela.Replace(",", "."));
                exports.addParametro("replace_totalParcela", replace_totalParcela.Replace(",", "."));
                exports.addParametro("replace_geraEntrega", replace_geraEntrega);
                exports.addParametro("replace_lagradouro_entrega", replace_lagradouro_entrega);
                exports.addParametro("replace_datetimefinalvenda", replace_datetimefinalvenda);
                exports.addParametro("replace_notaFiscal", replace_notaFiscal);
                exports.addParametro("replace_serie", replace_serie);
                exports.addParametro("replace_retorno", replace_retorno);
                exports.addParametro("replace_totalItens", replace_totalItens.Replace(",", "."));
                exports.addParametro("replace_pedido", replace_pedido);
                exports.addParametro("replace_ccf", replace_ccf);
                exports.addParametro("replace_cripto", replace_cripto);
                exports.addParametro("replace_modelo_ecf", replace_modelo_ecf);
                exports.addParametro("replace_importCarga", "1");
                exports.procedimentoRead();
                valida = true;
                exports.fechaRead();
            }
            catch (Exception erro)
            {
                valida = false;
                auxConsistencia = 1;
            }
            finally
            {
                exports.fechaConexao();
                if (auxConsistencia == 0)
                {
                }
            }
            return valida;
        }


        protected bool conectorWEB_replace_cupom_detalhes(
                        string replace_numeroCupom,
                        string replace_loja,
                        string replace_terminal,
                        string replace_dataVenda,
                        string replace_sequencia,
                        string replace_produto,
                        string replace_barra,
                        string replace_descricaoProduto,
                        string replace_quantidade,
                        string replace_priceVenda,
                        string replace_desconto,
                        string replace_total,
                        string replace_vendedor,
                        string replace_usuarioCancelamento,
                        string replace_motivoCancelamento,
                        string replace_trunca,
                        string replace_icms,
                        string replace_tipoCodigo,
                        string replace_unidade,
                        string replace_valorDesconto,
                        string replace_priceCusto,
                        string replace_acrescimo,
                        string replace_tributacao,
                        string replace_reserva,
                        string replace_metodo,
                        string replace_statusProduto,
                        string replace_codigoProduto,
                        string replace_valorAcrescimo,
                        string replace_situacao,
                        string replace_ippt,
                        string replace_ccf,
                        string replace_aliquota,
                        string replace_tipoTributacao,
                        string replace_cripto,
                        string replace_cfop,
                        string replace_cstIcms,
                        string replace_modelo_ecf,
                        string replace_upCupom,
                        string replace_quantidadeCanc,
                        string replace_importaCarga, string replace_idEan, string replace_quantidadeEAN)
        {
            bool valida = false;

            try
            {
                auxConsistencia = 0;
                if (alwaysVariables.trava == 1)
                {
                    return false;
                }
                exports.abreConexao();
                exports.startTransaction("conectorWEB_replace_cupom_detalhes");
                exports.addParametro("replace_numeroCupom", replace_numeroCupom);
                exports.addParametro("replace_loja", replace_loja);
                exports.addParametro("replace_terminal", replace_terminal);
                exports.addParametro("replace_dataVenda", String.Format("{0:yyyyMMdd}", Convert.ToDateTime(replace_dataVenda)));
                exports.addParametro("replace_sequencia", replace_sequencia);
                exports.addParametro("replace_produto", replace_produto);
                exports.addParametro("replace_barra", replace_barra);
                exports.addParametro("replace_descricaoProduto", replace_descricaoProduto);
                exports.addParametro("replace_quantidade", replace_quantidade.Replace(",","."));
                exports.addParametro("replace_priceVenda", replace_priceVenda.Replace(",", "."));
                exports.addParametro("replace_desconto", replace_desconto.Replace(",", "."));
                exports.addParametro("replace_total", replace_total.Replace(",", "."));
                exports.addParametro("replace_vendedor", replace_vendedor);
                exports.addParametro("replace_usuarioCancelamento", replace_usuarioCancelamento);
                exports.addParametro("replace_motivoCancelamento", replace_motivoCancelamento);
                exports.addParametro("replace_trunca", replace_trunca);
                exports.addParametro("replace_icms", replace_icms.Replace(",", "."));
                exports.addParametro("replace_tipoCodigo", replace_tipoCodigo);
                exports.addParametro("replace_unidade", replace_unidade);
                exports.addParametro("replace_valorDesconto", replace_valorDesconto.Replace(",", "."));
                exports.addParametro("replace_priceCusto", replace_priceCusto.Replace(",", "."));
                exports.addParametro("replace_acrescimo", replace_acrescimo.Replace(",", "."));
                exports.addParametro("replace_tributacao", replace_tributacao);
                exports.addParametro("replace_reserva", replace_reserva.Replace(",", "."));
                exports.addParametro("replace_metodo", replace_metodo);
                exports.addParametro("replace_statusProduto", replace_statusProduto);
                exports.addParametro("replace_codigoProduto", replace_codigoProduto.Replace(",", "."));
                exports.addParametro("replace_valorAcrescimo", replace_valorAcrescimo.Replace(",", "."));
                exports.addParametro("replace_situacao", replace_situacao);
                exports.addParametro("replace_ippt", replace_ippt);
                exports.addParametro("replace_ccf", replace_ccf);
                exports.addParametro("replace_aliquota", replace_aliquota.Replace(",", "."));
                exports.addParametro("replace_tipoTributacao", replace_tipoTributacao);
                exports.addParametro("replace_cripto", replace_cripto);
                exports.addParametro("replace_cfop", replace_cfop.Replace(",", "."));
                exports.addParametro("replace_cstIcms", replace_cstIcms);
                exports.addParametro("replace_modelo_ecf", replace_modelo_ecf);
                exports.addParametro("replace_upCupom", replace_upCupom.Replace(",", "."));
                exports.addParametro("replace_quantidadeCanc", replace_quantidadeCanc.Replace(",", "."));
                exports.addParametro("replace_importaCarga", "1");
                exports.addParametro("replace_idEan", replace_idEan);
                exports.addParametro("replace_quantidadeEAN", replace_quantidadeEAN.Replace(",", "."));
                exports.procedimentoRead();
                valida = true;
                exports.fechaRead();
            }
            catch (Exception erro)
            {
                valida = false;
                auxConsistencia = 1;
            }
            finally
            {
                exports.fechaConexao();
                if (auxConsistencia == 0)
                {
                }
            }
            return valida;
        }
        protected bool conectorWEB_replace_cupom_movimento(

                       string replace_numeroCupom,
                       string replace_loja,
                       string replace_terminal,
                       string replace_dataVenda,
                       string replace_sequencia,
                       string replace_finalizadora,
                       string replace_valor,
                       string replace_troco,
                       string replace_juros,
                       string replace_autentica,
                       string replace_convenio,
                       string replace_conveniado,
                       string replace_documento_not_fiscal,
                       string replace_chequeDeposito,
                       string replace_chequeNumero,
                       string replace_chequeConta,
                       string replace_chequeAgencia,
                       string replace_documentoCPF_CNPJ,
                       string replace_numeroCartao,
                       string replace_parcelamentoCartao,
                       string replace_tipoCartao,
                       string replace_usuario,
                       string replace_banco,
                       string replace_ccf,
                       string replace_cripto,
                       string replace_descricaoFinalizadora,
                       string replace_md5_finalizadora,
                       string replace_situacao,
                       string replace_modelo_ecf,
                       string replace_tipoDoc,
                       string replace_sequencia_mov,
                       string replace_importaCarga

        )
        {
            bool valida = false;

            try
            {
                auxConsistencia = 0;
                if (alwaysVariables.trava == 1)
                {
                    //return false;
                }
                exports.abreConexao();
                exports.startTransaction("conectorWEB_replace_cupom_movimento");
                exports.addParametro("replace_numeroCupom", replace_numeroCupom);
                exports.addParametro("replace_loja", replace_loja);
                exports.addParametro("replace_terminal", replace_terminal);
                exports.addParametro("replace_dataVenda", String.Format("{0:yyyyMMdd}", Convert.ToDateTime(replace_dataVenda)));
                exports.addParametro("replace_sequencia", replace_sequencia);
                exports.addParametro("replace_finalizadora", replace_finalizadora);
                exports.addParametro("replace_valor", replace_valor.Replace(",", "."));
                exports.addParametro("replace_troco", replace_troco.Replace(",", "."));
                exports.addParametro("replace_juros", replace_juros.Replace(",", "."));
                exports.addParametro("replace_autentica", replace_autentica);
                exports.addParametro("replace_convenio", replace_convenio);
                exports.addParametro("replace_conveniado", replace_conveniado);
                exports.addParametro("replace_documento_not_fiscal", replace_documento_not_fiscal);
                exports.addParametro("replace_chequeDeposito", replace_chequeDeposito);
                exports.addParametro("replace_chequeNumero", replace_chequeNumero);
                exports.addParametro("replace_chequeConta", replace_chequeConta);
                exports.addParametro("replace_chequeAgencia", replace_chequeAgencia);
                exports.addParametro("replace_documentoCPF_CNPJ", replace_documentoCPF_CNPJ);
                exports.addParametro("replace_numeroCartao", replace_numeroCartao);
                exports.addParametro("replace_parcelamentoCartao", replace_parcelamentoCartao.Replace(",", "."));
                exports.addParametro("replace_tipoCartao", replace_tipoCartao);
                exports.addParametro("replace_usuario", replace_usuario);
                exports.addParametro("replace_banco", replace_banco);
                exports.addParametro("replace_ccf", replace_ccf);
                exports.addParametro("replace_cripto", replace_cripto);
                exports.addParametro("replace_descricaoFinalizadora", replace_descricaoFinalizadora);
                exports.addParametro("replace_md5_finalizadora", replace_md5_finalizadora);
                exports.addParametro("replace_situacao", replace_situacao);
                exports.addParametro("replace_modelo_ecf", replace_modelo_ecf);
                exports.addParametro("replace_tipoDoc", replace_tipoDoc);
                exports.addParametro("replace_sequencia_mov", replace_sequencia_mov);
                exports.addParametro("replace_importaCarga", "1");
                exports.procedimentoRead();
                valida = true;
                exports.fechaRead();
            }
            catch (Exception erro)
            {
                valida = false;
                auxConsistencia = 1;
            }
            finally
            {
                exports.fechaConexao();
                if (auxConsistencia == 0)
                {
                }
            }
            return valida;
        }

        protected bool conectorWEB_replace_fechamento_caixa(
                     string replace_dataMovimento,
                     string replace_loja,
                     string replace_terminal,
                     string replace_sequencia,
                     string replace_funcionario,
                     string replace_dataHoraSaida,
                     string replace_grandeTotalBegin,
                     string replace_grandeTotalEnd,
                     string replace_contadorInicial,
                     string replace_contadorFinal,
                     string replace_cancelado,
                     string replace_desconto,
                     string replace_numeroCupom,
                     string replace_numeroClienteAtendidos,
                     string replace_contadorReducao,
                     string replace_contadorCancelados,
                     string replace_contadorNaoFiscal,
                     string replace_totalNotaFiscal,
                     string replace_totalSangrias,
                     string replace_cripto,
                     string replace_importCarga)
        {
            bool valida = false;
            try
            {
                auxConsistencia = 0;
                if (alwaysVariables.trava == 1)
                {
                    return false;
                }
                exports.abreConexao();
                exports.startTransaction("conectorWEB_replace_fechamento_caixa");
                exports.addParametro("replace_dataMovimento", String.Format("{0:yyyyMMdd}", Convert.ToDateTime(replace_dataMovimento)));
                exports.addParametro("replace_loja", replace_loja);
                exports.addParametro("replace_terminal", replace_terminal);
                exports.addParametro("replace_sequencia", replace_sequencia);
                exports.addParametro("replace_funcionario", replace_funcionario);
                if (replace_dataHoraSaida == "")
                {
                    exports.addParametro("replace_dataHoraSaida", replace_dataHoraSaida);
                }
                else
                {
                    exports.addParametro("replace_dataHoraSaida", String.Format("{0:yyyyMMdd}", Convert.ToDateTime(replace_dataHoraSaida)));
                }

                exports.addParametro("replace_grandeTotalBegin", replace_grandeTotalBegin.Replace(",", "."));
                exports.addParametro("replace_grandeTotalEnd", replace_grandeTotalEnd.Replace(",", "."));
                exports.addParametro("replace_contadorInicial", replace_contadorInicial.Replace(",", "."));
                exports.addParametro("replace_contadorFinal", replace_contadorFinal.Replace(",", "."));
                exports.addParametro("replace_cancelado", replace_cancelado.Replace(",", "."));
                exports.addParametro("replace_desconto", replace_desconto);
                exports.addParametro("replace_numeroCupom", replace_numeroCupom);
                exports.addParametro("replace_numeroClienteAtendidos", replace_numeroClienteAtendidos);
                exports.addParametro("replace_contadorReducao", replace_contadorReducao);
                exports.addParametro("replace_contadorCancelados", replace_contadorCancelados);
                exports.addParametro("replace_contadorNaoFiscal", replace_contadorNaoFiscal);
                exports.addParametro("replace_totalNotaFiscal", replace_totalNotaFiscal.Replace(",", "."));
                exports.addParametro("replace_totalSangrias", replace_totalSangrias.Replace(",", "."));
                exports.addParametro("replace_cripto", replace_cripto);
                exports.addParametro("replace_importCarga", "1");

                exports.procedimentoRead();
                valida = true;
                exports.fechaRead();
            }
            catch (Exception erro)
            {
                valida = false;
                auxConsistencia = 1;
            }
            finally
            {
                exports.fechaConexao();
                if (auxConsistencia == 0)
                {
                }
            }
            return valida;
        }

        protected bool conectorWEB_replace_movimento_dia(
           string replace_idloja,
           string replace_numeroCaixa,
           string replace_movimento,
           string replace_operador,
           string replace_numero_serie,
           string replace_mf_adicional,
           string replace_modelo_ecf,
           string replace_crz,
           string replace_coo,
           string replace_cro,
           string replace_dataEmissao,
           string replace_horaEmissao,
           string replace_venda_bruta,
           string replace_par_desconto,
           string replace_cripto,
           string replace_cancelamento,
           string replace_importaCarga
)
        {
            bool valida = false;
            try
            {
                auxConsistencia = 0;
                if (alwaysVariables.trava == 1)
                {
                    return false;
                }
                exports.abreConexao();
                exports.startTransaction("conectorWEB_replace_movimento_dia");
                exports.addParametro("replace_idloja", replace_idloja);
                exports.addParametro("replace_numeroCaixa", replace_numeroCaixa);
                exports.addParametro("replace_movimento", String.Format("{0:yyyyMMdd}", Convert.ToDateTime(replace_movimento)));
                exports.addParametro("replace_operador", replace_operador);
                exports.addParametro("replace_numero_serie", replace_numero_serie);
                exports.addParametro("replace_mf_adicional", replace_mf_adicional);
                exports.addParametro("replace_modelo_ecf", replace_modelo_ecf);
                exports.addParametro("replace_crz", replace_crz);
                exports.addParametro("replace_coo", replace_coo);
                exports.addParametro("replace_cro", replace_cro);
                exports.addParametro("replace_dataEmissao", replace_dataEmissao);
                exports.addParametro("replace_horaEmissao", replace_horaEmissao);
                exports.addParametro("replace_venda_bruta", replace_venda_bruta.Replace(",", "."));
                exports.addParametro("replace_par_desconto", replace_par_desconto.Replace(",", "."));
                exports.addParametro("replace_cripto", replace_cripto);
                exports.addParametro("replace_cancelamento", replace_cancelamento.Replace(",", "."));
                exports.addParametro("replace_importaCarga", "1");
                exports.procedimentoRead();
                valida = true;
                exports.fechaRead();
            }
            catch (Exception erro)
            {
                valida = false;
                auxConsistencia = 1;
            }
            finally
            {
                exports.fechaConexao();
                if (auxConsistencia == 0)
                {
                }
            }
            return valida;
        }
        protected bool conectorWEB_replace_movimento_caixa(
                  string replace_dataMovimento,
                  string replace_loja,
                  string replace_terminal,
                  string replace_finalizadora,
                  string replace_funcionario,
                  string replace_tipoCall,
                  string replace_sequencia,
                  string replace_hora,
                  string replace_valor,
                  string replace_sangria,
                  string replace_abertura,
                  string replace_vale,
                  string replace_fundo_caixa,
                  string replace_juros,
                  string replace_situacao,
                  string replace_quantidade,
                  string replace_doacao,
                  string replace_devolucao,
                  string replace_troco,
                  string replace_cancelamento,
                  string replace_nr_quantidade,
                  string replace_desconto,
                  string replace_importaCarga
)
        {
            bool valida = false;
            try
            {
                if (alwaysVariables.trava == 1)
                {
                    return false;
                }
                auxConsistencia = 0;
                exports.abreConexao();
                exports.startTransaction("conectorWEB_replace_movimento_caixa");
                exports.addParametro("replace_dataMovimento", String.Format("{0:yyyyMMdd}", Convert.ToDateTime(replace_dataMovimento)));
                exports.addParametro("replace_loja", replace_loja);
                exports.addParametro("replace_terminal", replace_terminal);
                exports.addParametro("replace_finalizadora", replace_finalizadora);
                exports.addParametro("replace_funcionario", replace_funcionario);
                exports.addParametro("replace_tipoCall", replace_tipoCall);
                exports.addParametro("replace_sequencia", replace_sequencia);
                exports.addParametro("replace_hora", replace_hora);
                exports.addParametro("replace_valor", replace_valor.Replace(",", "."));
                exports.addParametro("replace_sangria", replace_sangria.Replace(",", "."));
                exports.addParametro("replace_abertura", replace_abertura);
                exports.addParametro("replace_vale", replace_vale.Replace(",", "."));
                exports.addParametro("replace_fundo_caixa", replace_fundo_caixa.Replace(",", "."));
                exports.addParametro("replace_juros", replace_juros.Replace(",", "."));
                exports.addParametro("replace_situacao", replace_situacao);
                exports.addParametro("replace_quantidade", replace_quantidade.Replace(",", "."));
                exports.addParametro("replace_doacao", replace_doacao.Replace(",", "."));
                exports.addParametro("replace_devolucao", replace_devolucao.Replace(",", "."));
                exports.addParametro("replace_troco", replace_troco.Replace(",", "."));
                exports.addParametro("replace_cancelamento", replace_cancelamento.Replace(",", "."));
                exports.addParametro("replace_nr_quantidade", replace_nr_quantidade.Replace(",", "."));
                exports.addParametro("replace_desconto", replace_desconto.Replace(",", "."));
                exports.addParametro("replace_importaCarga", "1");

                exports.procedimentoRead();
                valida = true;
                exports.fechaRead();
            }
            catch (Exception erro)
            {
                valida = false;
                auxConsistencia = 1;
            }
            finally
            {
                exports.fechaConexao();
                if (auxConsistencia == 0)
                {
                }
            }
            return valida;
        }
        protected bool conectorWEB_replace_cartao( string replace_idcartao,
                                string replace_idAdministradora,
                                string replace_idloja,
                                string replace_inclusao,
                                string replace_alteracao,
                                string replace_cupom,
                                string replace_pedido,
                                string replace_terminal,
                                string replace_emissao,
                                string replace_vencimento,
                                string replace_pagamento,
                                string replace_status,
                                string replace_observacao,
                                string replace_parcela,
                                string replace_qttyParcela,
                                string replace_typeCartao,
                                string replace_bandeira,
                                string replace_valor,
                                string replace_prazo,
                                string replace_networkCard,
                                string replace_conferencia,
                                string replace_origem,
                                string replace_batimento,
                                string replace_envio,
                                string replace_idconectCard,
                                string replace_valorLiquido,
                                string replace_taxaValor,
                                string replace_idFuncionario,
                                string replace_chave_nota, 
                                string replace_importaCarga)
        {
            bool valida = false;

            try
            {
                auxConsistencia = 0;
                if (alwaysVariables.trava == 1)
                {
                    return false;
                }
                    exports.abreConexao();
                    exports.startTransaction("conectorWEB_replace_cartao");
                    exports.addParametro("replace_idCartao", replace_idcartao);
                    exports.addParametro("replace_idAdministradora",replace_idAdministradora);
                    exports.addParametro("replace_idloja",replace_idloja);
                    exports.addParametro("replace_inclusao", String.Format("{0:yyyyMMdd}", Convert.ToDateTime(replace_inclusao)));
                    exports.addParametro("replace_alteracao",replace_alteracao);
                    exports.addParametro("replace_cupom", replace_cupom);
                    exports.addParametro("replace_pedido", replace_pedido);
                    exports.addParametro("replace_terminal", replace_terminal);
                    exports.addParametro("replace_emissao", String.Format("{0:yyyyMMdd}", Convert.ToDateTime(replace_emissao)));
                    exports.addParametro("replace_vencimento", String.Format("{0:yyyyMMdd}", Convert.ToDateTime(replace_vencimento)));
                    exports.addParametro("replace_pagamento", replace_pagamento);
                    exports.addParametro("replace_status", replace_status);
                    exports.addParametro("replace_observacao", replace_observacao);
                    exports.addParametro("replace_parcela", replace_parcela.Replace(",", "."));
                    exports.addParametro("replace_qttyParcela", replace_qttyParcela.Replace(",", "."));
                    exports.addParametro("replace_typeCartao", replace_typeCartao);
                    exports.addParametro("replace_bandeira", replace_bandeira);
                    exports.addParametro("replace_valor", replace_valor.Replace(",", "."));
                    exports.addParametro("replace_prazo", replace_prazo.Replace(",", "."));
                    exports.addParametro("replace_networkCard", replace_networkCard);
                    exports.addParametro("replace_conferencia", replace_conferencia);
                    exports.addParametro("replace_origem", replace_origem);
                    exports.addParametro("replace_batimento", replace_batimento);
                    exports.addParametro("replace_envio", replace_envio);
                    exports.addParametro("replace_idconectCard", replace_idconectCard);
                    exports.addParametro("replace_valorLiquido", replace_valorLiquido.Replace(",", "."));
                    exports.addParametro("replace_taxaValor", replace_taxaValor.Replace(",", "."));
                    exports.addParametro("replace_idFuncionario", replace_idFuncionario);
                    exports.addParametro("replace_chave_nota", replace_chave_nota);
                    exports.addParametro("replace_importaCarga", "1");
                    exports.procedimentoRead();
                    valida = true;
                    exports.fechaRead();
            }
            catch (Exception erro)
            {
                valida = false;
                auxConsistencia = 1;
            }
            finally
            {
                exports.fechaConexao();
                if (auxConsistencia == 0)
                {
                }
            }
            return valida;
        }

        protected bool conectorWeb_replace_crediario(
          string replace_idloja,
          string replace_idmetodo,
          string replace_idcliente,
          string replace_idfuncionario,
          string replace_emissao,
          string replace_valueEntry,
          string replace_total,
          string replace_encargos,
          string replace_status,
          string replace_observacao,
          string replace_entrada,
          string replace_tac,
          string replace_cet,
          string replace_taxa,
          string replace_conferencia,
          string replace_pedido,
          string replace_batimento,
          string replace_parcelamento,
          string replace_cupom,
          string replace_terminal,
          string replace_idUsuarioInc,
          string replace_idUsuarioLastAlt,
          string replace_dateAlt,
          string replace_chave_nota)
        {
            bool valida = false;

            try
            {
                auxConsistencia = 0;
                if (alwaysVariables.trava == 1)
                {
                    return false;
                }
                exports.abreConexao();
                exports.startTransaction("conectorWEB_replace_crediario");
                exports.addParametro("replace_idloja", replace_idloja);
                exports.addParametro("replace_idmetodo", replace_idmetodo);
                exports.addParametro("replace_idcliente", replace_idcliente);
                exports.addParametro("replace_idfuncionario", replace_idfuncionario);
                exports.addParametro("replace_emissao", String.Format("{0:yyyyMMdd}",  Convert.ToDateTime(replace_emissao)));
                exports.addParametro("replace_valueEntry", replace_valueEntry.Replace(",", "."));
                exports.addParametro("replace_total", replace_total.Replace(",", "."));
                exports.addParametro("replace_encargos", replace_encargos);
                exports.addParametro("replace_status", replace_status);
                exports.addParametro("replace_observacao", replace_observacao);
                exports.addParametro("replace_entrada", replace_entrada.Replace(",", "."));
                exports.addParametro("replace_tac", replace_tac.Replace(",", "."));
                exports.addParametro("replace_cet", replace_cet);
                exports.addParametro("replace_taxa", replace_taxa.Replace(",", "."));
                exports.addParametro("replace_conferencia", replace_conferencia);
                exports.addParametro("replace_pedido", replace_pedido);
                exports.addParametro("replace_batimento", replace_batimento);
                exports.addParametro("replace_parcelamento", replace_parcelamento.Replace(",", "."));
                exports.addParametro("replace_cupom", replace_cupom);
                exports.addParametro("replace_terminal", replace_terminal);
                exports.addParametro("replace_idUsuarioInc", replace_idUsuarioInc);
                exports.addParametro("replace_idUsuarioLastAlt", replace_idUsuarioLastAlt);
                exports.addParametro("replace_dateAlt", replace_dateAlt);
                exports.addParametro("replace_chave_nota", replace_chave_nota);
                exports.procedimentoRead();
                valida = true;
                exports.fechaRead();
            }
            catch (Exception erro)
            {
                valida = false;
                auxConsistencia = 1;
            }
            finally
            {
                exports.fechaConexao();
                if (auxConsistencia == 0)
                {
                }
            }
            return valida;
        }


        protected bool conectorWeb_replace_cheque(
            string replace_idcheque,
          string replace_banco,
          string replace_idloja,
          string replace_idcliente,
          string replace_typeRecebimento,
          string replace_contaCorrente,
          string replace_serie,
          string replace_agencia,
          string replace_typeCheque,
          string replace_prazo,
          string replace_emissao,
          string replace_vencimento,
          string replace_cityBanco,
          string replace_numberCheque,
          string replace_valueCheque,
          string replace_historico,
          string replace_typeLancamento,
          string replace_pagamento,
          string replace_observacao,
          string replace_idUsuarioLiberacao,
          string replace_motivoLiberacao,
          string replace_cmc7,
          string replace_idusuario,
          string replace_alteracao,
          string replace_reserva,
          string replace_conferencia,
          string replace_origem,
          string replace_cupom,
          string replace_terminal,
          string replace_typeBaixa,
          string replace_caixa,
          string replace_contaBaixa,
          string replace_flagPagamento,
          string replace_valorPago,
          string replace_motivo,
          string replace_idfuncionario,
          string replace_finalizadoraCaixa,
          string replace_chave_nota)
        {
            bool valida = false;

            try
            {
                auxConsistencia = 0;
                if (alwaysVariables.trava == 1)
                {
                    return false;
                }
                exports.abreConexao();
                exports.startTransaction("conectorWEB_replace_cheque");
                exports.addParametro("replace_idcheque", replace_idcheque);
                exports.addParametro("replace_banco", replace_banco);
                exports.addParametro("replace_idloja", replace_idloja);
                exports.addParametro("replace_idcliente", replace_idcliente);
                exports.addParametro("replace_typeRecebimento", replace_typeRecebimento);
                exports.addParametro("replace_contaCorrente", replace_contaCorrente);
                exports.addParametro("replace_serie", replace_serie);
                exports.addParametro("replace_agencia", replace_agencia);
                exports.addParametro("replace_typeCheque", replace_typeCheque);
                exports.addParametro("replace_prazo", replace_prazo);
                exports.addParametro("replace_emissao", String.Format("{0:yyyyMMdd}", Convert.ToDateTime(replace_emissao)));
                exports.addParametro("replace_vencimento", String.Format("{0:yyyyMMdd}", Convert.ToDateTime(replace_vencimento)));
                exports.addParametro("replace_cityBanco", replace_cityBanco);
                exports.addParametro("replace_numberCheque", replace_numberCheque);
                exports.addParametro("replace_valueCheque", replace_valueCheque.Replace(",", "."));
                exports.addParametro("replace_historico", replace_historico);
                exports.addParametro("replace_typeLancamento", replace_typeLancamento);
                exports.addParametro("replace_pagamento", replace_pagamento);
                exports.addParametro("replace_observacao", replace_observacao);
                exports.addParametro("replace_idUsuarioLiberacao", replace_idUsuarioLiberacao);
                exports.addParametro("replace_motivoLiberacao", replace_motivoLiberacao);
                exports.addParametro("replace_cmc7", replace_cmc7);
                exports.addParametro("replace_idusuario", replace_idusuario);
                exports.addParametro("replace_alteracao", replace_alteracao);
                exports.addParametro("replace_reserva", replace_reserva);
                exports.addParametro("replace_conferencia", replace_conferencia);
                exports.addParametro("replace_origem", replace_origem);
                exports.addParametro("replace_cupom", replace_cupom);
                exports.addParametro("replace_terminal", replace_terminal);
                exports.addParametro("replace_typeBaixa", replace_typeBaixa);
                exports.addParametro("replace_caixa", replace_caixa);
                exports.addParametro("replace_contaBaixa", replace_contaBaixa);
                exports.addParametro("replace_flagPagamento", replace_flagPagamento);
                exports.addParametro("replace_valorPago", replace_valorPago.Replace(",", "."));
                exports.addParametro("replace_motivo", replace_motivo);
                exports.addParametro("replace_idfuncionario", replace_idfuncionario);
                exports.addParametro("replace_finalizadoraCaixa", replace_finalizadoraCaixa);
                exports.addParametro("replace_chave_nota", replace_chave_nota);
                exports.addParametro("replace_importaCarga", "1");
                exports.procedimentoRead();
                valida = true;
                exports.fechaRead();
            }
            catch (Exception erro)
            {
                valida = false;
                auxConsistencia = 1;
            }
            finally
            {
                exports.fechaConexao();
                if (auxConsistencia == 0)
                {
                }
            }
            return valida;
        }

        #endregion
    }

}
