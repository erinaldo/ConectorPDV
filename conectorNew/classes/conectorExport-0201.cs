using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace conectorPDV001
{
    class conectorExport0201
    {
        public conectorExport0201()
        {
            fileSecret = Path.Combine(Directory.GetCurrentDirectory(), "c:\\windows\\soberanu.ini");

            mensagemLinha = new List<string>();
            if (File.Exists(folderCrypt + "\\soberanu" + ".enc"))
            {
                cryptografia.descryptFile(folderCrypt + "soberanu" + ".enc", "\\soberanu");

                if (File.Exists(folderCrypt + "\\soberanu" + ".enc"))
                {
                    File.Delete(folderCrypt + "soberanu" + ".enc");
                }
                using (StreamReader texto = new StreamReader(folderCrypt + "soberanu.txt"))
                {
                    string mensagem = "";
                    while ((mensagem = texto.ReadLine()) != null)
                    {
                        mensagemLinha.Add(mensagem);
                    }
                    texto.Close();
                }
                if (mensagemLinha.Count == 0)
                {
                    StreamWriter final = new StreamWriter("c:\\windows\\soberanu.ini", true, Encoding.UTF8);

                    for (int j = 0; j < mensagemLinha.Count; j++)
                    {
                        final.Write(mensagemLinha[j] + "\r\n");
                    }
                    final.Close();
                }
                cryptografia.encryptFile(folderCrypt + "\\soberanu" + ".txt", "soberanu", 2);
            }
            else if (File.Exists(@"C:\windows\soberanu.ini"))
            {
                if (File.Exists(folderCrypt + "\\soberanu" + ".txt"))
                {
                    File.Delete(folderCrypt + "\\soberanu" + ".txt");
                }
                using (StreamReader texto = new StreamReader(@"C:\windows\soberanu.ini"))
                {
                    string mensagem = "";
                    while ((mensagem = texto.ReadLine()) != null)
                    {
                        mensagemLinha.Add(mensagem);
                    }
                    texto.Close();
                }

                StreamWriter final = new StreamWriter(folderCrypt + "\\soberanu" + ".txt", true, Encoding.UTF8);

                for (int j = 0; j < mensagemLinha.Count; j++)
                {
                    final.Write(mensagemLinha[j] + "\r\n");
                }
                final.Close();

                cryptografia.encryptFile(folderCrypt + "\\soberanu" + ".txt", "soberanu", 2);
            }
            fileSecret = Path.Combine(Directory.GetCurrentDirectory(), "c:\\windows\\soberanu.ini");

            alwaysVariables.PAF_Contato = getValue("SoftwareHouse", "contato", fileSecret);
            alwaysVariables.PAF_contatoCom = getValue("SoftwareHouse", "contatoCom", fileSecret);
            alwaysVariables.PAF_laudo = getValue("SoftwareHouse", "laudo", fileSecret);
            alwaysVariables.PAF_Endereco = getValue("SoftwareHouse", "Endereco", fileSecret);
            alwaysVariables.PAF_NumeroAplicativo = getValue("SoftwareHouse", "NumeroAplicativo", fileSecret);
            alwaysVariables.PAF_CNPJ = getValue("SoftwareHouse", "CNPJ", fileSecret);
            alwaysVariables.PAF_IE = getValue("SoftwareHouse", "IE", fileSecret);
            alwaysVariables.PAF_IM = getValue("SoftwareHouse", "IM", fileSecret);
            alwaysVariables.PAF_RAZAO = getValue("SoftwareHouse", "RazaoSocial", fileSecret);
            alwaysVariables.PAF_TELEFONE = getValue("SoftwareHouse", "Fone", fileSecret);
            alwaysVariables.PAF_Versao = getValue("SoftwareHouse", "Versao", fileSecret);
            alwaysVariables.PAF_Versao_Spec = getValue("SoftwareHouse", "Versao_spec", fileSecret);
            alwaysVariables.PAF_total = getValue("banco_smartPDV", "Total", fileSecret);

        }

        //#########################################################Variavel Enpsulada########################################################
        ProcessStartInfo ProcessInfo;
        Process myProcess;
        private dados banco = new dados();
        private crypt cryptografia = new crypt();
        const string folder = @"c:\";
        private Int32 countField = 0;
        private Int32 countRows = 0;
        private Int32 posSeparator;
        private string[,] matriz; //Matriz Bidimencionada
        List<string> mensagemLinha;
        const string folderCrypt = @"c:\conector\PDV\";
        string[] vetorInatial = new string[50] { "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#" };
        private int i, j;
        private int auxConsistencia;
        private int _count = 0;
        private string fileSecret;
        private conector_full_variable alwaysVariables = new conector_full_variable();

        [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileString")]
        private static extern int GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, int nSize, string lpFileName);
        [DllImport("kernel32.dll", EntryPoint = "WritePrivateProfileString")]
        private static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);
        //#########################################################End Variavel Enpsulada####################################################

        //######################################################### Classes e Metodos #######################################################
        public int count
        {
            get { return _count; }
            set { _count = value; }
        }

        public static string getValue(string secao, string chave, string fileName)
        {
            int carateres = 256;
            StringBuilder buffer = new StringBuilder(carateres);
            string sdefault = "";
            if (GetPrivateProfileString(secao, chave, sdefault, buffer, carateres, fileName) != 0)
            {
                return buffer.ToString();
            }
            else
            {
                // Verifica o último erro Win32.
                int err = Marshal.GetLastWin32Error();
                return null;
            }
        }

        public static bool writeValue(string secao, string chave, string value, string filename)
        {
            return WritePrivateProfileString(secao, chave, value, filename);
        }
        public Boolean iniciaGeracaoFile(string caminho, [MarshalAs(UnmanagedType.VBByRefStr)] ref string referencia, string title)
        {
            Directory.CreateDirectory(caminho);
            Boolean retorno = false;
            posSeparator = caminho.IndexOf(".");
            if (posSeparator == -1)
            {
                //referencia = caminho + "\\" + title + String.Format("{0:yyyyMMdd-HHmmss}", DateTime.Now) + ".txt";
                referencia = caminho + "\\" + title + String.Format("{0:yyyyMMdd}", DateTime.Now) + ".txt";
            }
            else
            {
                referencia = caminho.Substring(0, caminho.LastIndexOf(".")) + ".txt";
            }
            if (!File.Exists(referencia))
            {
                File.Delete(referencia);
                if (!File.Exists(referencia))
                {
                    StreamWriter sw = new StreamWriter(referencia, false);
                    //sw.Write("");
                    retorno = true;
                    sw.Close();   
                }
            }
            else
            {
                retorno = false;
            }
            return retorno;
        }
        public Boolean formacao_registro(Boolean valida, string caminho, string[] auxTipo, string di, string df, string store, string finalidade)
        {
            Boolean retorno = valida;
            posSeparator = caminho.IndexOf(".");

            if (File.Exists(caminho))
            {
                StreamWriter sw = new StreamWriter(caminho, true, Encoding.ASCII);
                auxConsistencia = 0;
                countField = 0;
                countRows = 0;
                
            }
            return retorno;
        }
        public string registro_serie(string caixa, string serie, string grandeTotal)
        {
            return caixa.PadLeft(4, '0') + serie.PadRight(20, ' ') + grandeTotal.PadLeft(21, '0');
        }
        public string registro_serie_unica(string grandeTotal)
        {
            return grandeTotal;
        }
        public string registro_tipo_n1(string type, string cnpj, string inscricao, string inscricao_municipal, string razao)
        {

            return type.PadRight(2, ' ') + cnpj.PadLeft(14, '0') + inscricao.PadRight(14, ' ') + inscricao_municipal.PadRight(14, ' ') + razao.PadRight(50, ' ');
        }
        public string registro_tipo_u1(string type, string cnpj, string inscricao, string inscricao_municipal, string razao)
        {

            return type.PadRight(2, ' ') + cnpj.PadLeft(14, '0') + inscricao.PadRight(14, ' ') + inscricao_municipal.PadRight(14, ' ') + razao.PadRight(50, ' ');
        }

        public string registro_tipo_n2(string type, string laudo_paf, string nome_paf, string versao_paf)
        {
            return type.PadRight(2, ' ') + laudo_paf.PadRight(10, ' ') + nome_paf.PadRight(50, ' ') + versao_paf.PadRight(10, ' ');
        }

        public string registro_tipo_n3(string type, string nome_file, string aut_md5)
        {
            return type.PadRight(2, ' ') + nome_file.PadRight(50, ' ') + aut_md5.PadRight(32, ' ');
        }

        public string registro_tipo_n9(string type, string cnpj_mf, string insc, string total)
        {
            return type.PadRight(2, ' ') + cnpj_mf.PadLeft(14, '0') + insc.PadRight(14, ' ') + total.PadLeft(6,'0');
        }

        public string registro_tipo_d01(string type, string cnpj, string inscricao, string inscricao_municipal, string razao)
        {

            return type.PadRight(2, ' ') + cnpj.PadLeft(14, '0') + inscricao.PadRight(14, ' ') + inscricao_municipal.PadRight(14, ' ') + razao.PadRight(50,' ');
        }

        public string registro_tipo_d02(string type, string cnpj, string numero_fab_ecf, string letra_mf,string tipo_ecf, string marca_ecf, string modelo_ecf, string coo, string numero_dav, string data_dav, string title_dav, string valor_total_dav, string coo_vinculado, string numero_seq, string nome_adquirente, string cnpj_cpf_adquirente)
        {
            return type.PadRight(2, ' ') + cnpj.PadLeft(14, '0') + numero_fab_ecf.PadRight(20, ' ') + letra_mf.PadRight(1, ' ') + tipo_ecf.PadRight(7, ' ') + marca_ecf.PadRight(20, ' ') + modelo_ecf.PadRight(20, ' ') + coo.PadLeft(6, '0') + numero_dav.PadRight(13, ' ') + data_dav.PadRight(8, ' ') + title_dav.PadRight(30, ' ') + valor_total_dav.PadLeft(8, '0') + coo_vinculado.PadLeft(6, '0') + numero_seq.PadLeft(3, '0') + nome_adquirente.PadRight(40, ' ') + cnpj_cpf_adquirente.PadLeft(14, '0');
        }

        public string registro_tipo_d03(string type, string numero_dav, string data_inclusao, string numero_item, string codigo_produto_servico, string descricao, string quantidade, string unidade, string valor_unitario, string desconto_sobre_item, string acrescimo_about_item, string valor_total_liquido, string substituicao_trib, string aliquota, string indicador_canc_parcial, string casas_dec_qtty, string casas_dec_valor)
        {
            //return type.PadRight(2, ' ') + numero_dav.PadLeft(13, '0') + data_inclusao.PadRight(8, ' ') + numero_item.PadLeft(3, '0') + codigo_produto_servico.PadRight(14, ' ') + descricao.PadRight(100, ' ') + quantidade.PadLeft(7, '0') + unidade.PadRight(3, ' ') + valor_unitario.PadLeft(8, '0') + desconto_sobre_item.PadLeft(8, '0') + acrescimo_about_item.PadLeft(8, '0') + valor_total_liquido.PadLeft(14, '0') + totalizador_parcial.PadRight(7, ' ') + indicador_canc_parcial.PadRight(1, ' ');
            return type.PadRight(2, ' ') + numero_dav.PadLeft(13, '0') + data_inclusao.PadRight(8, ' ') + numero_item.PadLeft(3, '0') + codigo_produto_servico.PadRight(14, ' ') + descricao.PadRight(100, ' ') + quantidade.PadLeft(7, '0') + unidade.PadRight(3, ' ') + valor_unitario.PadLeft(8, '0') + desconto_sobre_item.PadLeft(8, '0') + acrescimo_about_item.PadLeft(8, '0') + valor_total_liquido.PadLeft(14, '0') + substituicao_trib.PadRight(1, ' ') + aliquota.PadLeft(4, '0') + indicador_canc_parcial.PadRight(1, ' ') + casas_dec_qtty.PadLeft(1, '0') + casas_dec_valor.PadLeft(1, '0');
            //COTEPE 02/20 - return type.PadRight(2, ' ') + numero_dav.PadLeft(13, '0') + data_inclusao.PadRight(8, ' ') + numero_item.PadLeft(3, '0') + codigo_produto_servico.PadRight(14, ' ') + descricao.PadRight(100, ' ') + quantidade.PadLeft(7, '0') + unidade.PadRight(6, ' ') + valor_unitario.PadLeft(8, '0') + desconto_sobre_item.PadLeft(8, '0') + acrescimo_about_item.PadLeft(8, '0') + valor_total_liquido.PadLeft(14, '0') /*situacao tributaria + aliquota + indicador de cancelamento + qtty_casas_decimal + casas decimais do valor unitario*/ + retirar => totalizador_parcial.PadRight(7, ' ') + indicador_canc_parcial.PadRight(1, ' ');
        }

        public string registro_tipo_d03_item(string descricao_iteme_dav)
        {
            return descricao_iteme_dav.PadRight(21, ' ');
        }

        public string registro_tipo_d04(string type, string numero_dav, string data_alt, string hora_alt, string codigo_produto_servico, string descricao, string quantidade, string unidade, string valor_unitario, string desconto_sobre_item, string acrescimo_about_item, string valor_total_liquido, string st, string aliq, string indicador, string casa_dec_qtty, string casa_dec_valor, string tipoAlt)
        {
            return type.PadRight(2, ' ') + numero_dav.PadLeft(13, '0') + data_alt.PadLeft(8, '0') + hora_alt.PadLeft(6,'0') + codigo_produto_servico.PadRight(14, ' ') + descricao.PadRight(100, ' ') + quantidade.PadLeft(7, '0') + unidade.PadRight(3, ' ') + valor_unitario.PadLeft(8, '0') + desconto_sobre_item.PadLeft(8, '0') + acrescimo_about_item.PadLeft(8, '0') + valor_total_liquido.PadLeft(14, '0') + st.PadRight(1, ' ') + aliq.PadRight(4, ' ') + indicador.PadRight(1, ' ') + casa_dec_qtty.PadLeft(1,'0') + casa_dec_valor.PadLeft(1,'0') + tipoAlt.PadRight(1,' ');
        }

        public string registro_tipo_d09(string type, string cnpj, string inscricao, string total_registros_d2, string total_registros_d3)
        {
            return type.PadLeft(2, '0') + cnpj.PadLeft(14, '0') + inscricao.PadRight(14, ' ') + total_registros_d2.PadLeft(6, '0') + total_registros_d2.PadLeft(6, '0');
        }

        public string registro_tipo_p2(string type, string cnpj, string codigo, string descricao, string unidade, string iat , string ippt, string situacao, string aliquota, string valor_un)
        {
            return type.PadRight(2, ' ') + cnpj.PadLeft(14, '0') + codigo.PadRight(14, ' ') + descricao.PadRight(50, ' ') + unidade.PadRight(6, ' ') + iat.PadRight(1, ' ') + ippt.PadRight(1, ' ') + situacao.PadRight(1, ' ') + aliquota.PadLeft(4, '0') + valor_un.PadLeft(12,'0');
        }
        public string registro_tipo_e1(string type, string cnpj, string inscricao_estadual, string inscricao_municipal, string razao)
        {
            return type.PadRight(2, ' ') + cnpj.PadLeft(14, '0') + inscricao_estadual.PadRight(14, ' ') + inscricao_municipal.PadRight(14, ' ') + razao.PadRight(50, ' ');
        }

        public string registro_tipo_e2(string type, string cnpj, string codigo_mercadoria, string descricao_mercadoria, string unidade, string mensuracao_estoque, string quantidade)
        {
            return type.PadRight(2, ' ') + cnpj.PadLeft(14, '0') + codigo_mercadoria.PadRight(14, ' ') + descricao_mercadoria.PadRight(50, ' ') + unidade.PadRight(6, ' ') + mensuracao_estoque.PadRight(1, ' ') + quantidade.PadLeft(9, '0');
        }

        public string registro_tipo_e3(string type, string fabricacao, string adicional, string tipo_ecf, string marca, string modelo, string data, string hora)
        {
            return type.PadRight(2, ' ') + fabricacao.PadRight(20, ' ') + adicional.PadRight(1, ' ') + tipo_ecf.PadRight(7, ' ') + marca.PadRight(20, ' ') + modelo.PadRight(20, ' ') + data.PadRight(8, ' ') + hora.PadRight(6, ' ');
        }

        public string registro_tipo_e9(string type, string cnpj_mf, string inscricao, string total_registro_e2)
        {
            return type.PadLeft(2, '0') + cnpj_mf.PadLeft(14, '0') + inscricao.PadRight(14, ' ') + total_registro_e2.PadLeft(6, '0');
        }

        public string registro_tipo_a2(string type, string data, string meio_pgto, string tipo, string valor)
        {
            return type.PadRight(2, ' ') + data.PadRight(8, ' ') + meio_pgto.PadRight(25, ' ') + tipo.PadRight(1, ' ') + valor.PadLeft(12, '0');
        }

        public string registro_tipo_h2(string type, string cnpj, string serie, string md_ad, string tipo, string marca, string modelo, string coo, string ccf, string values, string data, string cpf, string title)
        {
            return type.PadRight(2, ' ') + cnpj.PadRight(14, ' ') + serie.PadRight(20,' ') + md_ad.PadRight(1, ' ') + tipo.PadRight(7, ' ') + marca.PadRight(20, ' ') + modelo.PadRight(20, ' ') + coo.PadLeft(6, '0') + ccf.PadLeft(6, '0') + values.PadLeft(13, '0') + data.PadLeft(8, '0') + cpf.PadRight(14, ' ') + title.PadRight(7, '0');
            // COTEPE 02/02 return type.PadRight(2, ' ') + cnpj.PadRight(14, ' ') + serie.PadRight(20, ' ') + md_ad.PadRight(1, ' ') + tipo.PadRight(7, ' ') + marca.PadRight(20, ' ') + modelo.PadRight(20, ' ') + coo.PadLeft(6, '0') + ccf.PadLeft(6, '0') + values.PadLeft(13, '0') + data.PadLeft(8, '0') + cpf.PadRight(14, ' ') + title.PadRight(7, '0') /*add cnpj da entidade recebedora da doacao*/;
        }

        public string registro_tipo_r01(string type, string nr_fabricacao, string letra_mf, string tipo_ecf, string marca_ecf, string modelo_ecf, string versao_sb, string data_inst_sb, string hora_inst_sb, string numero_seq_ecf, string cnpj_user, string inscr_user, string cnpj_desenvolvedor, string insc_desenvolvedor, string insc_municipal_desenvolvedor, string denominacao_desenvolvedor, string nome_pafecf, string versao_pafecf, string md5_pafecf, string di, string df, string versao_espec_pafecf)
        {
            return type.PadRight(3, ' ') + nr_fabricacao.PadRight(20, ' ') + letra_mf.Replace("\0", "").Trim().PadRight(1, ' ') + tipo_ecf.PadRight(7, ' ') + marca_ecf.PadRight(20, ' ') + modelo_ecf.PadRight(20, ' ') + versao_sb.Replace("\0", "").Trim().PadRight(10, ' ') + data_inst_sb.PadRight(8, ' ') + hora_inst_sb.PadRight(6, ' ') + numero_seq_ecf.PadLeft(3, '0') + cnpj_user.PadLeft(14, '0') + inscr_user.PadRight(14, ' ') + cnpj_desenvolvedor.PadLeft(14, '0') + insc_desenvolvedor.PadRight(14, ' ') + insc_municipal_desenvolvedor.PadLeft(14, '0') + denominacao_desenvolvedor.PadRight(40, ' ') + nome_pafecf.PadRight(40, ' ') + versao_pafecf.PadRight(10, ' ') + md5_pafecf.Replace("\0", "").Trim().PadRight(32, ' ') + di.Replace("/", "").PadRight(8, ' ') + df.Replace("/", "").PadRight(8, ' ') + versao_espec_pafecf.PadRight(4, ' ');
        }

        public string registro_tipo_r02(string type, string nr_fabricacao, string letra_mf, string modelo_ecf, string numero_user, string crz, string coo, string cro, string data_do_movimento, string data_emissao, string hora_emissao, string venda_bruta, string par_incide_desconto)
        {
            return type.PadRight(3, ' ') + nr_fabricacao.PadRight(20, ' ') + letra_mf.PadRight(1, ' ') + modelo_ecf.PadRight(20, ' ') + numero_user.PadLeft(2, '0') + crz.PadLeft(6, '0') + coo.PadLeft(6, '0') + cro.PadLeft(6, '0') + data_do_movimento.PadRight(8, ' ') + data_emissao.PadRight(8, ' ') + hora_emissao.PadRight(6, ' ') + venda_bruta.PadLeft(14, '0') + par_incide_desconto.PadRight(1, ' ');
        }
        public string registro_tipo_r03(string type, string nr_fabricacao, string letra_mf, string modelo_ecf, string numero_user, string crz, string totalizador_parcial, string valor_acumulado_parcial)
        {
            return type.PadRight(3, ' ') + nr_fabricacao.PadRight(20, ' ') + letra_mf.PadRight(1, ' ') + modelo_ecf.PadRight(20, ' ') + numero_user.PadLeft(2, '0') + crz.PadLeft(6, '0') + totalizador_parcial.PadRight(7, ' ') + valor_acumulado_parcial.PadLeft(13, '0');
        }
        public string registro_tipo_r04(string type, string nr_fabricacao, string letra_mf, string modelo_ecf, string numero_user, string ccf_cvc, string coo, string data_emissao, string subtotal_documento, string desconto_subtotal, string indicador_tipo_desconto, string acrescimo_subtotal, string indicador_tipo_acrescimo,string valor_total_liquido, string indicador_cancelados, string cancelamento_acresc_subtotal, string ordem_aplic_desc_acres, string nome_adquirente, string cnpj_adquirente)
        {
            return type.PadRight(3, ' ') + nr_fabricacao.PadRight(20, ' ') + letra_mf.PadRight(1, ' ') + modelo_ecf.PadRight(20, ' ') + numero_user.PadLeft(2, '0') + ccf_cvc.PadLeft(6, '0') + coo.PadLeft(6, '0') + data_emissao.PadRight(8, ' ') + subtotal_documento.PadLeft(14, '0') + desconto_subtotal.PadLeft(13, '0') + indicador_tipo_desconto.PadRight(1, ' ')+ acrescimo_subtotal.PadLeft(13,'0') + indicador_tipo_acrescimo.PadRight(1, ' ') + valor_total_liquido.PadLeft(14, '0') + indicador_cancelados.PadRight(1, ' ') + cancelamento_acresc_subtotal.PadLeft(13, '0') + ordem_aplic_desc_acres.PadRight(1, ' ') + nome_adquirente.PadRight(40, ' ') + cnpj_adquirente.PadLeft(14, '0');
        }
        public string registro_tipo_r05(string type, string nr_fabricacao, string letra_mf, string modelo_ecf, string numero_user, string coo, string ccf_cvc, string numero_item, string codigo_produto_servico, string descricao, string quantidade, string unidade_medida, string valor_unitario, string desconto_item, string acrescimo_item, string valor_full_liquido, string totalizador_parcial, string indicador_cancelamento, string quantidade_cancelada, string valor_cancelamento, string cancelaAcrescimo, string iat, string ippt, string casas_decimais_qtty, string casas_decimais_valor)
        {
            return type.PadRight(3, ' ') + nr_fabricacao.PadRight(20, ' ') + letra_mf.PadRight(1, ' ') + modelo_ecf.PadRight(20, ' ') + numero_user.PadLeft(2, '0') + coo.PadLeft(6, '0') + ccf_cvc.PadLeft(6, '0') + numero_item.PadLeft(3, '0') + codigo_produto_servico.PadRight(14, ' ') + descricao.PadRight(100, ' ') + quantidade.PadLeft(7, '0') + unidade_medida.PadRight(3, ' ') + valor_unitario.PadLeft(8, '0') + desconto_item.PadLeft(8, '0') + acrescimo_item.PadLeft(8, '0') + valor_full_liquido.PadLeft(14, '0') + totalizador_parcial.PadRight(7, ' ') + indicador_cancelamento.PadRight(1, ' ') + quantidade.PadLeft(7, '0') + valor_cancelamento.PadLeft(13, '0') + cancelaAcrescimo.PadLeft(13, '0') + iat.PadRight(1, ' ') + ippt.PadRight(1, ' ') + casas_decimais_qtty.PadLeft(1, '0') + casas_decimais_valor.PadLeft(1, '0');   
        }
        public string registro_tipo_r06(string type, string nr_fabricacao, string letra_mf, string modelo_ecf, string numero_user, string coo, string gnf, string grg, string cdc, string denomicao, string data_final, string hora_final)
        {
            return type.PadRight(3, ' ') + nr_fabricacao.PadRight(20, ' ') + letra_mf.PadRight(1, ' ') + modelo_ecf.PadRight(20, ' ') + numero_user.PadLeft(2, '0') + coo.PadLeft(6, '0') + gnf.PadLeft(6, '0') + grg.PadLeft(6, '0')+ cdc.PadLeft(4, '0') + denomicao.PadRight(2, ' ') + data_final.PadLeft(8, ' ') + hora_final.PadLeft(6, ' ');
        }

        public string registro_tipo_r07(string type, string nr_fabricacao, string letra_mf, string modelo_ecf, string numero_user, string coo, string ccf, string gnf, string meio_pgto, string valor_pago, string indicador_estorno, string valor_estorno)
        {
            return type.PadRight(3, ' ') + nr_fabricacao.PadRight(20, ' ') + letra_mf.PadRight(1, ' ') + modelo_ecf.PadRight(20, ' ') + numero_user.PadLeft(2, '0') + coo.PadLeft(6, '0') + ccf.PadLeft(6, '0') + gnf.PadLeft(6, '0') + meio_pgto.PadRight(15, ' ') + valor_pago.PadLeft(13, '0') + indicador_estorno.PadRight(1, ' ') + valor_estorno.PadLeft(13,'0');
        }
        //ANEXO
        public string registro_tipo_rA7(string DESCRICAO)
        {
            return DESCRICAO.PadRight(30, ' ');
        }

        public string registro_encerrantes_c1(string type, string cnpj, string inscricao, string insc_municipal, string razao)
        {
            return type.PadRight(2, ' ') + cnpj.PadLeft(14, '0') + inscricao.PadRight(14, ' ') + insc_municipal.PadRight(14, ' ') + razao.PadRight(50,' ');
        }
        public string registro_encerrantes_c2(string type, string cnpj, string id_abastecimento, string tanque, string bomba, string bico, string combustivel, string data_abastecimento, string hora_abastecimento, string encerrantes_inicial, string encerrantes_final, string status_abastecimento, string nr_fab_ecf, string data, string hora, string coo, string nr_nota_fiscal, string volume_comercializado)
        {
            return type.PadRight(2, ' ') + cnpj.PadLeft(14, '0') + id_abastecimento.PadRight(15, ' ') + tanque.PadRight(3, ' ') + bomba.PadRight(3, ' ') + bico.PadRight(3, ' ') + combustivel.PadRight(20, ' ') + data_abastecimento.PadRight(8, ' ') + hora_abastecimento.PadRight(6, ' ') + encerrantes_inicial.PadLeft(15, '0') + encerrantes_final.PadLeft(15, '0') + status_abastecimento.PadRight(10, ' ') + nr_fab_ecf.PadRight(20, ' ') + data.PadRight(8, ' ') + hora.PadRight(6, ' ') + coo.PadLeft(6, '0') + nr_nota_fiscal.PadLeft(6, '0') + volume_comercializado.PadLeft(6, '0');
        }
        public string registro_encerrantes_c9(string type, string cnpj_mf, string inscricao, string total_registro_c2)
        {
            return type.PadLeft(2, '0') + cnpj_mf.PadLeft(14, '0') + inscricao.PadRight(14, ' ') + total_registro_c2.PadLeft(6, '0');
        }
        public string registro_ead(string type, string hash)
        {
            return type.PadRight(3,' ') + hash.PadRight(256, ' ');
        }
        //######################################################### End Classes e Metodos ###################################################
    }
}
