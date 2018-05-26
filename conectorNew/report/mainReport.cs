using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Windows.Forms;
using MySql.Data.MySqlClient;

namespace conector
{
    public partial class mainReport : Form
    {
        public mainReport()
        {
            InitializeComponent();

            try
            {
                conexao = new MySqlConnection("server=" + alwaysVariables.LocalHost + ";user id=" + alwaysVariables.UserName + ";password=" + alwaysVariables.Senha + ";database=" + alwaysVariables.Schema);
            }
            catch (Exception e)
            {
                MessageBox.Show("Messagem do Sistema", e.Message);
            }
        }
        //#################################################Bloco de Instancia de Variaveis Encapsuladas######################################################
        private conector.report.conectorDataSet resultado = new conector.report.conectorDataSet();
        //"server=" + alwaysVariables.LocalHost + ";user id=" + alwaysVariables.UserName + ";password=" + alwaysVariables.Senha + ";database=" + alwaysVariables.Schema
        //private MySqlConnection conexao = new MySqlConnection(conector.Properties.Settings.Default.conectorBanco); //Instancia DataSet
        private conector_full_variable alwaysVariables = new conector_full_variable();
        private MySqlConnection conexao;// = new MySqlConnection("server=" + alwaysVariables.LocalHost + ";user id=" + alwaysVariables.UserName + ";password=" + alwaysVariables.Senha + ";database=" + alwaysVariables.Schema);
        private MySqlCommand comando;
        private MySqlParameter myparamentro;
        private MySqlParameter myparamentro1;
        private MySqlParameter myparamentro2;
        private MySqlParameter myparamentro3;
        private MySqlParameter myparamentro4;
        private MySqlParameter myparamentro5;
        private MySqlParameter myparamentro6;
        private MySqlParameter myparamentro7;
        //#################################################End Bloco de Instancia de Variaveis Encapsuladas##################################################

        //#################################################Metodo, Funções e Proparteis #####################################################################
        public void retornoListaCliente()
        {

            conector.report.clienteLista relato = new conector.report.clienteLista();
            conexao.Open();
            comando = new MySqlCommand("conector_report_cliente", conexao);
            comando.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter value = new MySqlDataAdapter(comando);
            value.Fill(resultado, "conector_report_cliente");
            relato.SetDataSource(resultado);
            relato.Refresh();
            reportView.ReportSource = relato;
            this.Show();
        }
        public void retornoHistoricoCliente()
        {

            conector.report.historicoClienteFull relato = new conector.report.historicoClienteFull();
            conexao.Open();
            comando = new MySqlCommand("conector_report_historico", conexao);
            comando.CommandType = CommandType.StoredProcedure;
            myparamentro = new MySqlParameter();
            myparamentro1 = new MySqlParameter();
            myparamentro2 = new MySqlParameter();
            myparamentro3 = new MySqlParameter();
            myparamentro4 = new MySqlParameter();
            myparamentro.ParameterName = "find_loja";
            myparamentro.Value = "1";
            comando.Parameters.Add(myparamentro);
            myparamentro1.ParameterName = "find_cliente";
            myparamentro1.Value = "3";
            comando.Parameters.Add(myparamentro1);
            myparamentro2.ParameterName = "escolha";
            myparamentro2.Value = "0";
            comando.Parameters.Add(myparamentro2);
            myparamentro3.ParameterName = "di";
            myparamentro3.Value = "20120101";
            comando.Parameters.Add(myparamentro3);
            myparamentro4.ParameterName = "df";
            myparamentro4.Value = String.Format("{0:yyyyMMdd}", DateTime.Now);
            comando.Parameters.Add(myparamentro4);
            MySqlDataAdapter value = new MySqlDataAdapter(comando);
            try
            {
                value.Fill(resultado, "conector_report_historico");
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.ToString(),"Erro");
            }
            
            relato.SetDataSource(resultado);
            relato.Refresh();
            reportView.ReportSource = relato;
            conexao.Close();
            this.Show();
        }
        public void retornoListaProduto()
        {

            conector.report.listaProduto relato = new conector.report.listaProduto();
            conexao.Open();
            comando = new MySqlCommand("conector_report_listProduto", conexao);
            comando.CommandType = CommandType.StoredProcedure;
            myparamentro = new MySqlParameter();
            myparamentro1 = new MySqlParameter();
            myparamentro2 = new MySqlParameter();
            myparamentro3 = new MySqlParameter();
            myparamentro4 = new MySqlParameter();
            myparamentro.ParameterName = "find";
            myparamentro.Value = "1";
            comando.Parameters.Add(myparamentro);
            myparamentro1.ParameterName = "findIdSetor";
            myparamentro1.Value = "0";
            comando.Parameters.Add(myparamentro1);
            myparamentro2.ParameterName = "findIdGrupo";
            myparamentro2.Value = "0";
            comando.Parameters.Add(myparamentro2);
            myparamentro3.ParameterName = "findIdCategoria";
            myparamentro3.Value = "0";
            comando.Parameters.Add(myparamentro3);
            myparamentro4.ParameterName = "findIdFornecedor";
            myparamentro4.Value = "0";
            comando.Parameters.Add(myparamentro4);
            MySqlDataAdapter value = new MySqlDataAdapter(comando);
            try
            {
                value.Fill(resultado, "conector_report_listProduto");
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.ToString(), "Erro");
            }

            relato.SetDataSource(resultado);
            relato.Refresh();
            reportView.ReportSource = relato;
            conexao.Close();
            this.Show();
        }
        public void retornoListaSetorProduto()
        {

            conector.report.listaSetorProduto relato = new conector.report.listaSetorProduto();
            conexao.Open();
            comando = new MySqlCommand("conector_report_detalhesSetor", conexao);
            comando.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter value = new MySqlDataAdapter(comando);
            try
            {
                value.Fill(resultado, "conector_report_detalhesSetor");
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.ToString(), "Erro");
            }

            relato.SetDataSource(resultado);
            relato.Refresh();
            reportView.ReportSource = relato;
            conexao.Close();
            this.Show();
        }
        public void retornoListaGrupoProduto()
        {

            conector.report.listaGrupo relato = new conector.report.listaGrupo();
            conexao.Open();
            comando = new MySqlCommand("conector_report_detalhesGrupo", conexao);
            comando.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter value = new MySqlDataAdapter(comando);
            try
            {
                value.Fill(resultado, "conector_report_detalhesGrupo");
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.ToString(), "Erro");
            }

            relato.SetDataSource(resultado);
            relato.Refresh();
            reportView.ReportSource = relato;
            conexao.Close();
            this.Show();
        }
        public void retornoListaCategoriaProduto()
        {

            conector.report.listaCategoria relato = new conector.report.listaCategoria();
            conexao.Open();
            comando = new MySqlCommand("conector_report_detalhesCategoria", conexao);
            comando.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter value = new MySqlDataAdapter(comando);
            try
            {
                value.Fill(resultado, "conector_report_detalhesCategoria");
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.ToString(), "Erro");
            }

            relato.SetDataSource(resultado);
            relato.Refresh();
            reportView.ReportSource = relato;
            conexao.Close();
            this.Show();
        }
        public void romaneioItemEntrada()
        {
            conector.report.romaneioItemEntrada relato = new conector.report.romaneioItemEntrada();
            conexao.Open();
            comando = new MySqlCommand("conector_report_itemEntrada", conexao);
            comando.CommandType = CommandType.StoredProcedure;
            myparamentro = new MySqlParameter();
            myparamentro1 = new MySqlParameter();
            myparamentro2 = new MySqlParameter();
            myparamentro3 = new MySqlParameter();
            myparamentro.ParameterName = "store";
            myparamentro.Value = "1";
            comando.Parameters.Add(myparamentro);
            myparamentro1.ParameterName = "find";
            myparamentro1.Value = "0";
            comando.Parameters.Add(myparamentro1);
            myparamentro2.ParameterName = "di";
            myparamentro2.Value = "20120101";
            comando.Parameters.Add(myparamentro2);
            myparamentro3.ParameterName = "df";
            myparamentro3.Value = String.Format("{0:yyyyMMdd}", DateTime.Now);
            comando.Parameters.Add(myparamentro3);
            MySqlDataAdapter value = new MySqlDataAdapter(comando);
            try
            {
                value.Fill(resultado, "conector_report_itemEntrada");
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.ToString(), "Erro");
            }

            relato.SetDataSource(resultado);
            relato.Refresh();
            reportView.ReportSource = relato;
            conexao.Close();
            this.Show();
        }
        public void detalhesItemEntrada()
        {
            conector.report.destalhesEntrada relato = new conector.report.destalhesEntrada();
            conexao.Open();
            comando = new MySqlCommand("conector_report_detalhesEntrada", conexao);
            comando.CommandType = CommandType.StoredProcedure;
            myparamentro = new MySqlParameter();
            myparamentro1 = new MySqlParameter();
            myparamentro.ParameterName = "store";
            myparamentro.Value = "1";
            comando.Parameters.Add(myparamentro);
            myparamentro1.ParameterName = "find";
            myparamentro1.Value = "295";
            comando.Parameters.Add(myparamentro1);
            MySqlDataAdapter value = new MySqlDataAdapter(comando);
            try
            {
                value.Fill(resultado, "conector_report_detalhesEntrada");
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.ToString(), "Erro");
            }

            relato.SetDataSource(resultado);
            relato.Refresh();
            reportView.ReportSource = relato;
            conexao.Close();
            this.Show();
        }
        public void listaCompra()
        {
            conector.report.listaCompra relato = new conector.report.listaCompra();
            conexao.Open();
            comando = new MySqlCommand("conector_report_compra", conexao);
            comando.CommandType = CommandType.StoredProcedure;
            myparamentro = new MySqlParameter();
            myparamentro1 = new MySqlParameter();
            myparamentro2 = new MySqlParameter();
            myparamentro3 = new MySqlParameter();
            myparamentro4 = new MySqlParameter();
            myparamentro5 = new MySqlParameter();
            myparamentro.ParameterName = "find";
            myparamentro.Value = "0";
            comando.Parameters.Add(myparamentro);
            myparamentro1.ParameterName = "di";
            myparamentro1.Value = "20120101";
            comando.Parameters.Add(myparamentro1);
            myparamentro2.ParameterName = "df";
            myparamentro2.Value = String.Format("{0:yyyyMMdd}", DateTime.Now);
            comando.Parameters.Add(myparamentro2);
            myparamentro3.ParameterName = "typeCompra";
            myparamentro3.Value = "0";
            comando.Parameters.Add(myparamentro3);
            myparamentro4.ParameterName = "loja";
            myparamentro4.Value = "0";
            comando.Parameters.Add(myparamentro4);
            myparamentro5.ParameterName = "cliente";
            myparamentro5.Value = "0";
            comando.Parameters.Add(myparamentro5);
            MySqlDataAdapter value = new MySqlDataAdapter(comando);
            try
            {
                value.Fill(resultado, "conector_report_compra");
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.ToString(), "Erro");
            }

            relato.SetDataSource(resultado);
            relato.Refresh();
            reportView.ReportSource = relato;
            conexao.Close();
            this.Show();
        }
        public void vendaSet()
        {
            conector.report.vendaSetor relato = new conector.report.vendaSetor();
            conexao.Open();
            comando = new MySqlCommand("conector_report_vendaItem", conexao);
            comando.CommandType = CommandType.StoredProcedure;
            myparamentro = new MySqlParameter();
            myparamentro1 = new MySqlParameter();
            myparamentro.ParameterName = "di";
            myparamentro.Value = String.Format("{0:yyyyMMdd}", DateTime.Now); ;
            comando.Parameters.Add(myparamentro);
            myparamentro1.ParameterName = "df";
            myparamentro1.Value = String.Format("{0:yyyyMMdd}", DateTime.Now); ;
            comando.Parameters.Add(myparamentro1);
            MySqlDataAdapter value = new MySqlDataAdapter(comando);
            try
            {
                value.Fill(resultado, "conector_report_vendaItem");
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.ToString(), "Erro");
            }

            relato.SetDataSource(resultado);
            relato.Refresh();
            reportView.ReportSource = relato;
            conexao.Close();
            this.Show();
        }
        public void resumoEstoqueTotal()
        {
            conector.report.resumoEstoque relato = new conector.report.resumoEstoque();
            conexao.Open();
            comando = new MySqlCommand("conector_report_resumoEstoque", conexao);
            comando.CommandType = CommandType.StoredProcedure;
            myparamentro = new MySqlParameter();
            myparamentro.ParameterName = "findLoja";
            myparamentro.Value = "1";
            comando.Parameters.Add(myparamentro);
            MySqlDataAdapter value = new MySqlDataAdapter(comando);
            try
            {
                value.Fill(resultado, "conector_report_resumoEstoque");
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.ToString(), "Erro");
            }

            relato.SetDataSource(resultado);
            relato.Refresh();
            reportView.ReportSource = relato;
            conexao.Close();
            this.Show();
        }
        public void pedidoReserva(string store, string reserva)
        {

            conector.report.pedidoVenda relato = new conector.report.pedidoVenda();
            conexao.Open();
            comando = new MySqlCommand("conector_report_reserva", conexao);
            comando.CommandType = CommandType.StoredProcedure;
            myparamentro = new MySqlParameter();
            myparamentro1 = new MySqlParameter();
            myparamentro.ParameterName = "find_loja";
            myparamentro.Value = store;
            comando.Parameters.Add(myparamentro);
            myparamentro1.ParameterName = "find_pedido";
            myparamentro1.Value = reserva;
            comando.Parameters.Add(myparamentro1);
            MySqlDataAdapter value = new MySqlDataAdapter(comando);
            try
            {
                value.Fill(resultado, "conector_report_reserva");
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.ToString(), "Erro");
            }

            relato.SetDataSource(resultado);
            relato.Refresh();
            reportView.ReportSource = relato;
            conexao.Close();
            this.Show();
        }
        public void listaContasPagar()
        {
            conector.report.listaPagar relato = new conector.report.listaPagar();
            conexao.Open();
            comando = new MySqlCommand("conector_report_pagar", conexao);
            comando.CommandType = CommandType.StoredProcedure;
            myparamentro = new MySqlParameter();
            myparamentro1 = new MySqlParameter();
            myparamentro2 = new MySqlParameter();
            myparamentro3 = new MySqlParameter();
            myparamentro4 = new MySqlParameter();
            myparamentro5 = new MySqlParameter();
            myparamentro6 = new MySqlParameter();
            myparamentro7 = new MySqlParameter();
            myparamentro.ParameterName = "find";
            myparamentro.Value = "o";
            comando.Parameters.Add(myparamentro);
            myparamentro1.ParameterName = "di";
            myparamentro1.Value = "20120101";
            comando.Parameters.Add(myparamentro1);
            myparamentro2.ParameterName = "df";
            myparamentro2.Value = String.Format("{0:yyyyMMdd}", DateTime.Now);
            comando.Parameters.Add(myparamentro2);
            myparamentro3.ParameterName = "store";
            myparamentro3.Value = "0";
            comando.Parameters.Add(myparamentro3);
            myparamentro4.ParameterName = "pessoa";
            myparamentro4.Value = "0";
            comando.Parameters.Add(myparamentro4);
            myparamentro5.ParameterName = "escolha";
            myparamentro5.Value = "0";
            comando.Parameters.Add(myparamentro5);
            myparamentro6.ParameterName = "paga";
            myparamentro6.Value = "3";
            comando.Parameters.Add(myparamentro6);
            MySqlDataAdapter value = new MySqlDataAdapter(comando);
            try
            {
                value.Fill(resultado, "conector_report_pagar");
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.ToString(), "Erro");
            }

            relato.SetDataSource(resultado);
            relato.Refresh();
            reportView.ReportSource = relato;
            conexao.Close();
            this.Show();
        }

        public void listaEntradas()
        {
            conector.report.listaEntradas relato = new conector.report.listaEntradas();
            conexao.Open();
            comando = new MySqlCommand("conector_report_entrada", conexao);
            comando.CommandType = CommandType.StoredProcedure;
            myparamentro = new MySqlParameter();
            myparamentro1 = new MySqlParameter();
            myparamentro2 = new MySqlParameter();
            myparamentro3 = new MySqlParameter();
            myparamentro4 = new MySqlParameter();
            myparamentro5 = new MySqlParameter();
            myparamentro6 = new MySqlParameter();
            myparamentro7 = new MySqlParameter();
            myparamentro.ParameterName = "findStatus";
            myparamentro.Value = "0";
            comando.Parameters.Add(myparamentro);
            myparamentro1.ParameterName = "findCliente";
            myparamentro1.Value = "1";
            comando.Parameters.Add(myparamentro1);
            myparamentro2.ParameterName = "findLoja";
            myparamentro2.Value = "0";
            comando.Parameters.Add(myparamentro2);
            myparamentro3.ParameterName = "findNota";
            myparamentro3.Value = "0";
            comando.Parameters.Add(myparamentro3);
            myparamentro4.ParameterName = "findCfop";
            myparamentro4.Value = "0";
            comando.Parameters.Add(myparamentro4);
            myparamentro5.ParameterName = "di";
            myparamentro5.Value = "20120101";
            comando.Parameters.Add(myparamentro5);
            myparamentro6.ParameterName = "df";
            myparamentro6.Value = String.Format("{0:yyyyMMdd}", DateTime.Now);
            comando.Parameters.Add(myparamentro6);
            MySqlDataAdapter value = new MySqlDataAdapter(comando);
            try
            {
                value.Fill(resultado, "conector_report_entrada");
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.ToString(), "Erro");
            }

            relato.SetDataSource(resultado);
            relato.Refresh();
            reportView.ReportSource = relato;
            conexao.Close();
            this.Show();
        }
        
        public void Extrato(string VarConta, DateTime di, DateTime df)
        {
            conector.report.extratoConta relato = new conector.report.extratoConta();
            conexao.Open();
            comando = new MySqlCommand("conector_report_extrato", conexao);
            comando.CommandType = CommandType.StoredProcedure;
            myparamentro = new MySqlParameter();
            myparamentro1 = new MySqlParameter();
            myparamentro2 = new MySqlParameter();
            myparamentro3 = new MySqlParameter();
            myparamentro4 = new MySqlParameter();
            myparamentro5 = new MySqlParameter();
            myparamentro6 = new MySqlParameter();
            myparamentro7 = new MySqlParameter();
            myparamentro.ParameterName = "escolha";
            myparamentro.Value = "2";
            comando.Parameters.Add(myparamentro);
            myparamentro1.ParameterName = "di";
            myparamentro1.Value = String.Format("{0:yyyyMMdd}", di); ;
            comando.Parameters.Add(myparamentro1);
            myparamentro2.ParameterName = "df";
            myparamentro2.Value = String.Format("{0:yyyyMMdd}", df);
            comando.Parameters.Add(myparamentro2);
            myparamentro3.ParameterName = "conciliacao";
            myparamentro3.Value = "t";
            comando.Parameters.Add(myparamentro3);
            myparamentro4.ParameterName = "conta";
            myparamentro4.Value = VarConta;
            comando.Parameters.Add(myparamentro4);
            myparamentro5.ParameterName = "chave";
            myparamentro5.Value = "0";
            comando.Parameters.Add(myparamentro5);
            myparamentro6.ParameterName = "VarValor";
            myparamentro6.Value = "0";
            comando.Parameters.Add(myparamentro6);
            myparamentro7.ParameterName = "VarLancamento";
            myparamentro7.Value = "0";
            comando.Parameters.Add(myparamentro7);
            MySqlDataAdapter value = new MySqlDataAdapter(comando);
            try
            {
                value.Fill(resultado, "conector_report_extrato");
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.ToString(), "Erro");
            }

            relato.SetDataSource(resultado);
            relato.Refresh();
            reportView.ReportSource = relato;
            conexao.Close();
            this.Show();
        }
        public void emiti_Boleto(string idBoleto)
        {
            conector.report.BoletoLayout relato = new conector.report.BoletoLayout();
            conexao.Open();
            comando = new MySqlCommand("conector_report_boletos", conexao);
            comando.CommandType = CommandType.StoredProcedure;
            myparamentro = new MySqlParameter();
            myparamentro.ParameterName = "find";
            myparamentro.Value = idBoleto;
            comando.Parameters.Add(myparamentro);
            MySqlDataAdapter value = new MySqlDataAdapter(comando);
            try
            {
                value.Fill(resultado, "conector_report_boletos");
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.ToString(), "Erro");
            }

            relato.SetDataSource(resultado);
            relato.Refresh();
            reportView.ReportSource = relato;
            conexao.Close();
            this.Show();
        }

        private void mainReport_Load(object sender, EventArgs e)
        {

        }

        private void mainReport_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                ProcessTabKey(true);
            }
            else if (e.KeyCode == Keys.F1)
            {
            }
        }
        //#################################################End Metodo, Funções e Proparteis #################################################################
    }
}
