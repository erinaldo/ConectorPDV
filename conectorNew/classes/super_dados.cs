using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

namespace conectorPDV001.MG001
{

    class super_dados
    {
        // Declaração das funções não gerenciadas: GetPrivateProfileString e 
        // WritePrivateProfileString
        [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileString")]
        private static extern int GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, int nSize, string lpFileName);
        [DllImport("kernel32.dll", EntryPoint = "WritePrivateProfileString")]
        private static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);
        private string fileSecret;
        //################################################BEGIN Variaveis Encapsuladas#################################################################################################
        protected string usuario;
        protected string ip;
        protected string passwd;
        protected string baseDados;
        private MySqlConnection conexao;
        private MySqlDataReader drResultado;
        private DataSet dsResultado;
        private MySqlParameter parametro;
        private MySqlCommand comando;
        private string flagDrOuDs="";
        private conector_full_variable alwaysVariables = new conector_full_variable();
        private MySqlTransaction transacao; //'transacao para controle de transacao commit e rollback'
        //################################################END Variaveis Encapsuladas###################################################################################################
        //################################################BEGIN METODO CONSTRUTOR####################################################################################################
        public super_dados()
        {
            conectaBanco();
        }

        private void carregaArquivo()
        {
            if (File.Exists("c:\\windows\\soberanu.ini"))
            {
                fileSecret = Path.Combine(Directory.GetCurrentDirectory(), "c:\\windows\\soberanu.ini");
            }
            else
            {
                fileSecret = Path.Combine(Directory.GetCurrentDirectory(), "soberanu.ini");
            }
            alwaysVariables.Schema = getValue("banco", "schema", fileSecret);
            alwaysVariables.UserName = getValue("banco", "username", fileSecret);
            alwaysVariables.Senha = getValue("banco", "password", fileSecret);
            alwaysVariables.LocalHostSuper = getValue("banco", "super_server", fileSecret);
            alwaysVariables.Store = getValue("loja", "store", fileSecret);
        }

        private void conectaBanco(/*string usuario, string senha, string ip, string banco*/)
        {
            carregaArquivo();
            try
            {
                conexao = new MySqlConnection("server=" + alwaysVariables.LocalHostSuper + ";user id=" + alwaysVariables.UserName + ";password=" + alwaysVariables.Senha + ";database=" + alwaysVariables.Schema);                
                //Properties.Settings.Default.conectorBanco = "server=" + alwaysVariables.LocalHost + ";user id=" + alwaysVariables.UserName + ";password=" + alwaysVariables.Senha + ";database=" + alwaysVariables.Schema;
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message, "Messagem do Sistema");
                //global::System.Windows.Forms.o
            }
        }

        public super_dados(string userName, string localHosts, string senha, string schema)
        {
            if (File.Exists("c:\\windows\\soberanu.ini"))
            {
                fileSecret = Path.Combine(Directory.GetCurrentDirectory(), "c:\\windows\\soberanu.ini");
            }
            else
            {
                fileSecret = Path.Combine(Directory.GetCurrentDirectory(), "soberanu.ini");
            }
            try
            {
                usuario = userName;
                ip = localHosts;
                passwd = senha;
                baseDados = schema;
                conexao = new MySqlConnection("server=" + ip + ";user id=" + usuario + ";password=" + passwd + ";database=" + baseDados);
                //Properties.Settings.Default.conectorBanco = "server=" + alwaysVariables.LocalHost + ";user id=" + alwaysVariables.UserName + ";password=" + alwaysVariables.Senha + ";database=" + alwaysVariables.Schema;
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message , "Messagem do Sistema");
            }
        }
        //################################################END METODO CONSTRUTOR######################################################################################################################
        //################################################BEGIN METODO/PROCEDIMENTO DE BANCO DE DADOS################################################################################################
        public static string getFile(string localizacao)
        {
            if (localizacao.IndexOf("\\conector") != -1)
            {
                localizacao = localizacao.Replace("\\conector", "");
            }
            return localizacao;
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
        /*public static string espelho_conexao()//(string local, string user, string pass, string schema)
        {
            //return "server=" + alwaysVariables.LocalHost + ";user id=" + alwaysVariables.UserName + ";password=" + alwaysVariables.Senha + ";database=" + alwaysVariables.Schema;
            return "server= ;user id=root;password=c3r3br0;database=conector";
        }*/
        
        public Boolean abreConexao()
        {
            Boolean ok = false;
            try
            {
                conexao.Open();
                ok = true;
            }
            catch(Exception e)
            {
                // MessageBox.Show("INFORMAÇÃO DO SISTEMA => [ IMPOSSÍVEL ESTABELECER CONEXÃO COM O BANCO DE DADOS ] <= CLICK OK PARA ENCERRAR A OPERAÇÃO.", "ERRO FATAL PERCA DE CONEXÃO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ok = false;
                alwaysVariables.trava = 1;
            }
            return ok;
        }
        public void fechaConexao()
        {
            conexao.Close();
        }
        public int statusSchema()
        {
            if (conexao.State == ConnectionState.Open)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public void fechaRead()
        {
            try
            {
                drResultado.Close();         //fechando o dataRead quando for chamado pelo "DR"
            }
            catch (Exception e)
            {
                MessageBox.Show("Messagem do Sistema", e.Message);
            }
        }

        public void procedimentoRead()
        {
            flagDrOuDs = "DR";
            drResultado = comando.ExecuteReader();
        }
        public void procedimentoText()
        {
            comando.ExecuteNonQuery();
        }
        public void procedimentoSet()
        {
            flagDrOuDs = "DS";
            MySqlDataAdapter data = new MySqlDataAdapter(comando);
            dsResultado = new DataSet();
            data.Fill(dsResultado);
        }
        public DataSet retornaSet()
        {
            return dsResultado;
        }
        public MySqlDataReader retornaRead()
        {
            return drResultado;
        }
        public void startTransaction(string procedimento)
        {
            comando = new MySqlCommand(procedimento, conexao);
            comando.CommandType = System.Data.CommandType.StoredProcedure;
        }
        public void singleTransaction(string instrucao)
        {
            comando = new MySqlCommand(instrucao, conexao);
            comando.CommandType = System.Data.CommandType.Text;
        }
        public void addParametro(string nome, string valor)
        {
            parametro = new MySqlParameter();
            parametro.ParameterName = nome;
            parametro.Value = valor;
            comando.Parameters.Add(parametro);
        }
        public void iniciarTransacao()
        {
            transacao = conexao.BeginTransaction();           //'indico o inicio da transacao'
        }
        public void commit()               //'Realizo a transaçao do Disco Rigido
        {
           transacao.Commit();
        }
        public void rollback()               //'Ocorreu algum problema e desfaz a transaçaão
        {
            try
            {
                transacao.Rollback();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
    //################################################END METODO/PROCEDIMENTO DE BANCO DE DADOS####################################################################################################
}
