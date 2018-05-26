using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
//#######################Modulo Web/Net
using System.Net;
using System.Web;
//#######################End Modulo Web/Net

namespace conectorPDV001
{
    public partial class consultaCliente : Form
    {
        public consultaCliente()
        {
            InitializeComponent();
        }
        public consultaCliente(string loja, string usuario, string pdv)
        {
            InitializeComponent();
            auxIdLoja = loja;
            auxIdUsuario = usuario;
            auxIdTerminal = pdv;
        }
        public consultaCliente(string loja, string usuario, string pdv, bool leitura, string client)
        {
            InitializeComponent();
            auxIdLoja = loja;
            auxIdUsuario = usuario;
            auxIdTerminal = pdv;
            auxIdCliente = client;
            sinal = 5;
        }
        //##########################Bloco de variaveis encapsuladas#########################
        private string flagString = "";
        private Int32 sinal = 0; 
        private string verifica;
        private string newtelefone;
        private string newddd;
        private string newGridcepCliente;
        private string newGridObs;
        private string newGridTypeFisicaCliente;
        private string newGridTypeJuricaCliente;
        private string newGridTypeRuralCliente;
        private string newGridCodigoCliente;
        private string newGridNameCliente;
        private string newGridInativoCliente;
        private string newGridRazaoCliente;
        private string newGridFantasiaCliente;
        private string newGridLogradouroCliente;
        private string newGridRgCliente;
        private string newGridIeCliente;
        private string newGridCpfCliente;
        private string auxGridCpfCliente;
        private string newGridCnpjCliente;
        private string auxGridCnpjCliente;
        private string newGridHomePageCliente;
        private string newGridBairroCliente;
        private string newGridCityCliente;
        private string newGridComplementoCliente;
        private string newGridUsuarioCadastroCliente;
        private string newGridUsuarioCadastroAlteracaoCliente;
        private string newGridDataNascCliente;
        private string newGridDataInclusaoCliente;
        private string newGridCodMunCliente;
        private string newGridNumber;
        private conector_full_variable alwaysVariables = new conector_full_variable();
        private int i, countField, countRows; //variavel do loop.
        private Int16 flagTipopessoa;
        private Int16 flagTipoFornecedor;
        private Int16 flagAtividade = 1;
        private Int16 flagSemaforo;
        private Int16 flagUsuario;
        private Int16 auxConsistencia;
        private Int16 flagValidation = 0;  //1 valida 0 nao valida
        private Int16 flagTest = 0;
        private int posSeparator;
        private string auxNome, texto; //contagem nome fisica
        private string auxCpfCnpj;
        private string auxIdEndereco;
        private string auxIdCliente;
        private string auxIdLoja;
        private string auxIdUsuario;
        private string auxIdEstado;
        private string auxUfEstado;
        private string auxIdTerminal;
        private string auxIdSexy;
        private string auxIdCivil;
        private string auxIdCepCliente;
        private dados banco = new dados();
        private cep dadoscep;
        private consultaFone openFone;
        private consultaClienteNome openName;
        private pequisaMuncipio findMunicipo;
        private pesquisaPessoa pesquisaCliente;
        private telefone telefoneCliente;
        private validation cpf_cnpj = new validation();
        //############Variaveis Cliente Web
        private int ataque = 0;
        private int liberaWeb = 0;
        private DataSet dsRCliente;
        private DataSet dsRFisica;
        private DataSet dsRRural;
        private DataSet dsRJuridica;
        private DataSet dsRRisco;
        private DataSet dsRReferencia;
        private DataSet dsRProfissional;
        private DataSet dsREntrega;
        private DataSet dsRCobranca;
        private DataSet dsREndereco;
        private DataSet dsRFone;
        private DataSet matriz = new DataSet();  //Matriz de retornos locais conector
        //##########################End Variaveis##################

        //##########################Bloco Properteis########################################

        public string GridCodigo
        {
            get
            {
                return auxIdCliente;
            }
            set
            {
                auxIdCliente = value;
            }
        }
        public string GridEstado
        {
            get
            {
                return auxUfEstado;
            }
            set
            {
                auxUfEstado = value;
            }
        }
        public string GridNome
        {
            get
            {
                return newGridNameCliente;
            }
            set
            {
                newGridNameCliente = value;
            }
        }
        public string GridRazao
        {
            get
            {
                return newGridRazaoCliente;
            }
            set
            {
                newGridRazaoCliente = value;
            }
        }
        public Int16 GridTypePessoa
        {
            get
            {
                return flagTipopessoa;
            }
            set
            {
                flagTipopessoa = value;
            }
        }
        public string GridCep
        {
            get
            {
                return newGridcepCliente;
            }
            set
            {
                newGridcepCliente = value;
            }
        }
        public string GridLogradouro
        {
            get
            {
                return newGridLogradouroCliente;
            }
            set
            {
                newGridLogradouroCliente = value;
            }
        }
        public string GridNumero
        {
            get
            {
                return newGridNumber;
            }
            set
            {
                newGridNumber = value;
            }
        }
        public string GridCidade
        {
            get
            {
                return newGridCityCliente;
            }
            set
            {
                newGridCityCliente = value;
            }
        }
        public string GridCNPJ
        {
            get
            {
                return auxGridCnpjCliente;
            }
            set
            {
                auxGridCnpjCliente = value;
            }
        }
        public string GridCPF
        {
            get
            {
                return auxGridCpfCliente;
            }
            set
            {
                auxGridCpfCliente = value;
            }
        }
        //##########################End Properteis

        //############################################WEB - Import Método da API############################################################################
        [DllImport("wininet.dll")]
        private extern static Boolean InternetGetConnectedState(out int Description, int ReservedValue);
        // Um método que verifica se esta conectado
        public static Boolean IsConnected()
        {
            int Description;
            return InternetGetConnectedState(out Description, 0);
        }
        //############################################END WEB Import Método da API##########################################################################
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
                MessageBox.Show("Source : " + e.Source, "Exception Source", MessageBoxButtons.OK);
                return "Host Unavailable";
            }
        }

        void conectorWeb_fisica(string aux)
        {
            liberaWeb = -1;
            WebConectorServer.Service MyConector1 = new WebConectorServer.Service(alwaysVariables.UrlWebConector);
            ataque = 0;
            string test = PingHost(MyConector1.Url);
            if (test == null) { test = "Host Unavailable"; }
            else if (MyConector1.Url.IndexOf("localhost") != -1)
            {
                test = "Service Up";
            }
            
            if (test != "Host Unavailable" && aux != "")
            {
                dsRFisica = MyConector1.ObterMainPepleoFisica(aux);
                countRows = dsRFisica.Tables[0].DefaultView.Count;
                if (countRows > 0)
                {
                    MyConector1.Dispose();
                    conectorWeb_processamento_service(Convert.ToString(dsRFisica.Tables[0].Rows[0][0]));
                }
                else
                {
                    MessageBox.Show("Cliente não encontrado.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("HOST UNAVAILABLE.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            MyConector1.Dispose();
        }

        void conectorWeb_juridica(string aux)
        {
            liberaWeb = -1;
            WebConectorServer.Service MyConector1 = new WebConectorServer.Service(alwaysVariables.UrlWebConector);
            ataque = 0;
            string test = PingHost(MyConector1.Url);
            if (test == null) { test = "Host Unavailable"; }
            else if (MyConector1.Url.IndexOf("localhost") != -1)
            {
                test = "Service Up";
            }

            if (test != "Host Unavailable" && aux != "")
            {
                dsRJuridica = MyConector1.ObterMainPepleoJuridica(aux);
                countRows = dsRJuridica.Tables[0].DefaultView.Count;
                if (countRows > 0)
                {
                    MyConector1.Dispose();
                    conectorWeb_processamento_service(Convert.ToString(dsRJuridica.Tables[0].Rows[0][0]));
                }
                else
                {
                    MessageBox.Show("Cliente não encontrado.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("HOST UNAVAILABLE.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            MyConector1.Dispose();
        }

        void conectorWeb_codigo(string aux)
        {
            liberaWeb = -1;
            WebConectorServer.Service MyConector1 = new WebConectorServer.Service(alwaysVariables.UrlWebConector);
            ataque = 0;
                string test = PingHost(MyConector1.Url);
                if (test == null) { test = "Host Unavailable"; }
                else if (MyConector1.Url.IndexOf("localhost") != -1)
                {
                    test = "Service Up";
                }
                
                if (test != "Host Unavailable" && aux != "")
                {
                    dsRCliente = MyConector1.ObterMainPepleo(aux);
                    countRows = dsRCliente.Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        Convert.ToString(dsRCliente.Tables[0].Rows[0][0]);
                        countRows = dsRCliente.Tables[0].DefaultView.Count;
                        if (countRows > 0)
                        {
                            MyConector1.Dispose();
                            conectorWeb_processamento_service(Convert.ToString(dsRCliente.Tables[0].Rows[0][0]));
                        }
                        else
                        {
                            MessageBox.Show("Cliente não encontrado.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("HOST UNAVAILABLE.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                MyConector1.Dispose();
        }
        void conectorWeb_processamento_service(string aux)
        {

            if (rdCodigoSimplificado.Checked == true)
            {
                if (txtCodigoCliente.Text != "" && Convert.ToDouble(txtCodigoCliente.Text) > 0)
                {
                    conector_find_type_cliente();
                    if (auxConsistencia == 0)
                    {
                        btnCarregarClienteSimplificado.Select();
                    }
                    if (liberaWeb == 0)
                    {
                        return;
                    }
                }
            }
            else if(rdCPFSimplificado.Checked == true)
            {
                    if ((flagTipopessoa == 1) || (flagTipopessoa == 3))
                    {
                        newGridCpfCliente = mskCpfCliente.Text;
                        newGridCpfCliente = mskCpfCliente.Text.Replace(".", "");
                        newGridCpfCliente = newGridCpfCliente.Replace("-", "");
                        newGridCpfCliente = newGridCpfCliente.Replace("/", "");
                        newGridCpfCliente = newGridCpfCliente.Replace(",", "");
                        if (newGridCpfCliente.Length != 11)
                        {
                            MessageBox.Show("CPF incorreto, preencha o campo corretamente.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            conector_find_type_cliente();
                            if (auxConsistencia == 0)
                            {
                                btnCarregarClienteSimplificado.Select();
                            }
                            if (liberaWeb == 0)
                            {
                                return;
                            }
                        }
                    }


            }
            else if(rdCNPJSimplificado.Checked == true)
            {
                if (flagTipopessoa == 2)
                {
                    newGridCnpjCliente = mskCnpjCliente.Text;
                    newGridCnpjCliente = mskCnpjCliente.Text.Replace(".", "");
                    newGridCnpjCliente = newGridCnpjCliente.Replace("-", "");
                    newGridCnpjCliente = newGridCnpjCliente.Replace("/", "");
                    newGridCnpjCliente = newGridCnpjCliente.Replace(",", "");
                    if (newGridCnpjCliente.Length != 14)
                    {
                        MessageBox.Show("CNPJ incorreto, preencha o campo corretamente.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        auxCpfCnpj = cpf_cnpj.calculo_cgc(newGridCnpjCliente);
                        if (auxCpfCnpj == "0")
                        {
                            MessageBox.Show("CNPJ incorreto, preencha o campo corretamente.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //mskCnpjCliente.SelectAll();
                            //mskCnpjCliente.Focus();
                        }
                        else
                        {
                            conector_find_type_cliente();
                            if (auxConsistencia == 0)
                            {
                                btnCarregarClienteSimplificado.Select();
                            }
                            if (liberaWeb == 0)
                            {
                                return;
                            }
                            else
                            {
                                MessageBox.Show("Uma consulta na matriz foi verificada ao servidor principal a ocorrencia de um cliente cadastrado com esse documento, este serviço irá cadastra-ló via webservice.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);       
                            }
                        }
                    }
                }
            }
            else
            {

            }

                WebConectorServer.Service MyConectorMain = new WebConectorServer.Service(alwaysVariables.UrlWebConector);
                MyConectorMain.Timeout = 10000;//Segundos 
                ataque = 0;
                string test = PingHost(MyConectorMain.Url);
                if (test == null) { test = "Host Unavailable"; }
                else if (MyConectorMain.Url.IndexOf("localhost") != -1)
                {
                    test = "Service Up";
                }
                
                if (test != "Host Unavailable" && aux != "")
                {
                    dsRCliente = MyConectorMain.ObterMainPepleo(aux);
                    countRows = dsRCliente.Tables[0].DefaultView.Count;
                    if (countRows == 1 && ataque == 0)
                    {
                        if ((ataque = conectorPDV_replace_cliente(Convert.ToString(dsRCliente.Tables[0].Rows[0][0]),
                             Convert.ToString(dsRCliente.Tables[0].Rows[0][1]),
                             Convert.ToString(dsRCliente.Tables[0].Rows[0][2]),
                             Convert.ToString(dsRCliente.Tables[0].Rows[0][3]),
                             Convert.ToString(dsRCliente.Tables[0].Rows[0][4]),
                             Convert.ToString(dsRCliente.Tables[0].Rows[0][5]),
                             Convert.ToString(dsRCliente.Tables[0].Rows[0][6]),
                             Convert.ToString(dsRCliente.Tables[0].Rows[0][7]),
                             Convert.ToString(dsRCliente.Tables[0].Rows[0][8]),
                             Convert.ToString(dsRCliente.Tables[0].Rows[0][9]),
                             Convert.ToString(dsRCliente.Tables[0].Rows[0][10]),
                             Convert.ToString(dsRCliente.Tables[0].Rows[0][11]),
                             Convert.ToString(dsRCliente.Tables[0].Rows[0][12]),
                             Convert.ToString(dsRCliente.Tables[0].Rows[0][13])
                            )) == 0)
                        {

                        }
                        switch (Convert.ToString(dsRCliente.Tables[0].Rows[0][2]))
                        {
                            case "1":
                                dsRFisica = MyConectorMain.ObterMainPepleoFisica(aux);
                                countRows = dsRFisica.Tables[0].DefaultView.Count;
                                if (countRows == 1)
                                {
                                    if ((ataque = conectorPDV_replace_fisica(Convert.ToString(dsRFisica.Tables[0].Rows[0][0]),
                                                                Convert.ToString(dsRFisica.Tables[0].Rows[0][1]),
                                                                Convert.ToString(dsRFisica.Tables[0].Rows[0][2]),
                                                                Convert.ToString(dsRFisica.Tables[0].Rows[0][3]),
                                                                Convert.ToString(dsRFisica.Tables[0].Rows[0][4]),
                                                                Convert.ToString(dsRFisica.Tables[0].Rows[0][5]),
                                                                Convert.ToString(dsRFisica.Tables[0].Rows[0][6]),
                                                                Convert.ToString(dsRFisica.Tables[0].Rows[0][7]))) == 0)
                                    {

                                    }
                                }
                                break;
                            case "2":
                                dsRJuridica = MyConectorMain.ObterMainPepleoJuridica(aux);
                                countRows = dsRJuridica.Tables[0].DefaultView.Count;
                                if (countRows == 1)
                                { //index 2.2
                                    if ((ataque = conectorPDV_replace_juridica(Convert.ToString(dsRJuridica.Tables[0].Rows[0][0]),
                                                                Convert.ToString(dsRJuridica.Tables[0].Rows[0][1]),
                                                                Convert.ToString(dsRJuridica.Tables[0].Rows[0][2]),
                                                                Convert.ToString(dsRJuridica.Tables[0].Rows[0][3]),
                                                                Convert.ToString(dsRJuridica.Tables[0].Rows[0][4]),
                                                                Convert.ToString(dsRJuridica.Tables[0].Rows[0][5]),
                                                                Convert.ToString(dsRJuridica.Tables[0].Rows[0][6]),
                                                                Convert.ToString(dsRJuridica.Tables[0].Rows[0][7]))) == 0)
                                    {

                                    }
                                }
                                break;
                            case "3":
                                dsRRural = MyConectorMain.ObterMainPepleoRural(aux);
                                countRows = dsRRural.Tables[0].DefaultView.Count;
                                if (countRows == 1)
                                { //index 2.2
                                    if ((ataque = conectorPDV_replace_rural(Convert.ToString(dsRRural.Tables[0].Rows[0][0]),
                                                                Convert.ToString(dsRRural.Tables[0].Rows[0][1]),
                                                                Convert.ToString(dsRRural.Tables[0].Rows[0][2]),
                                                                Convert.ToString(dsRRural.Tables[0].Rows[0][3]),
                                                                Convert.ToString(dsRRural.Tables[0].Rows[0][4]),
                                                                Convert.ToString(dsRRural.Tables[0].Rows[0][5]),
                                                                Convert.ToString(dsRRural.Tables[0].Rows[0][6]),
                                                                Convert.ToString(dsRRural.Tables[0].Rows[0][7]),
                                                                Convert.ToString(dsRRural.Tables[0].Rows[0][8]))) == 0)
                                    {

                                    }
                                }
                                break;
                            default:
                                ataque = 2;
                                break;
                        }
                    }
                    if (ataque == 0)
                    {
                        dsREndereco = MyConectorMain.ObterEndereco(aux);
                        countRows = dsREndereco.Tables[0].DefaultView.Count;
                        if (countRows >= 1 && ataque == 0)
                        { //index 2.3
                            for (int i = 0; i < countRows; i++)
                            {
                                if ((ataque = conectorPDV_replace_endereco(Convert.ToString(dsREndereco.Tables[0].Rows[i][0]),
                                                            Convert.ToString(dsREndereco.Tables[0].Rows[i][1]),
                                                            Convert.ToString(dsREndereco.Tables[0].Rows[i][2]),
                                                            Convert.ToString(dsREndereco.Tables[0].Rows[i][3]),
                                                            Convert.ToString(dsREndereco.Tables[0].Rows[i][4]),
                                                            Convert.ToString(dsREndereco.Tables[0].Rows[i][5]),
                                                            Convert.ToString(dsREndereco.Tables[0].Rows[i][6]),
                                                            Convert.ToString(dsREndereco.Tables[0].Rows[i][7]),
                                                            Convert.ToString(dsREndereco.Tables[0].Rows[i][8]),
                                                            Convert.ToString(dsREndereco.Tables[0].Rows[i][9]),
                                                            Convert.ToString(dsREndereco.Tables[0].Rows[i][10]),
                                                            Convert.ToString(dsREndereco.Tables[0].Rows[i][11]))) == 0)
                                {

                                }
                            }
                        }

                        dsRFone = MyConectorMain.ObterFone(aux);
                        countRows = dsRFone.Tables[0].DefaultView.Count;
                        if (countRows >= 1 && ataque == 0)
                        {//index 2.4
                            for (int i = 0; i < countRows; i++)
                            {
                                if ((ataque = conectorPDV_replace_fone(Convert.ToString(dsRFone.Tables[0].Rows[i][0]),
                                                            Convert.ToString(dsRFone.Tables[0].Rows[i][1]),
                                                            Convert.ToString(dsRFone.Tables[0].Rows[i][2]),
                                                            Convert.ToString(dsRFone.Tables[0].Rows[i][3]),
                                                            Convert.ToString(dsRFone.Tables[0].Rows[i][4]),
                                                            Convert.ToString(dsRFone.Tables[0].Rows[i][5]),
                                                            Convert.ToString(dsRFone.Tables[0].Rows[i][6]),
                                                            Convert.ToString(dsRFone.Tables[0].Rows[i][7]),
                                                            Convert.ToString(dsRFone.Tables[0].Rows[i][8]))) == 0)
                                {

                                }
                            }
                        }

                        dsRProfissional = MyConectorMain.ObterPepleoProfissional(aux);
                        countRows = dsRProfissional.Tables[0].DefaultView.Count;
                        if (countRows >= 1 && ataque == 0)
                        { //index 2.5
                            for (int i = 0; i < countRows; i++)
                            {
                                if ((ataque = conectorPDV_replace_clienteProfissional(Convert.ToString(dsRProfissional.Tables[0].Rows[i][0]),
                                                            Convert.ToString(dsRProfissional.Tables[0].Rows[i][1]),
                                                            Convert.ToString(dsRProfissional.Tables[0].Rows[i][2]),
                                                            Convert.ToString(dsRProfissional.Tables[0].Rows[i][3]),
                                                            Convert.ToString(dsRProfissional.Tables[0].Rows[i][4]),
                                                            Convert.ToString(dsRProfissional.Tables[0].Rows[i][5]),
                                                            Convert.ToString(dsRProfissional.Tables[0].Rows[i][6]),
                                                            Convert.ToString(dsRProfissional.Tables[0].Rows[i][7]),
                                                            Convert.ToString(dsRProfissional.Tables[0].Rows[i][8]))) == 0)
                                {

                                }
                            }
                        }

                        dsRRisco = MyConectorMain.ObterPepleoRisco(aux);
                        countRows = dsRRisco.Tables[0].DefaultView.Count;
                        if (countRows >= 1 && ataque == 0)
                        { //index 2.6
                            for (int i = 0; i < countRows; i++)
                            {
                                if ((ataque = conectorPDV_replace_clienteRisco(Convert.ToString(dsRRisco.Tables[0].Rows[i][0]),
                                                            Convert.ToString(dsRRisco.Tables[0].Rows[i][1]),
                                                            Convert.ToString(dsRRisco.Tables[0].Rows[i][2]),
                                                            Convert.ToString(dsRRisco.Tables[0].Rows[i][3]),
                                                            Convert.ToString(dsRRisco.Tables[0].Rows[i][4]),
                                                            Convert.ToString(dsRRisco.Tables[0].Rows[i][5]),
                                                            Convert.ToString(dsRRisco.Tables[0].Rows[i][6]),
                                                            Convert.ToString(dsRRisco.Tables[0].Rows[i][7]),
                                                            Convert.ToString(dsRRisco.Tables[0].Rows[i][8]),
                                                            Convert.ToString(dsRRisco.Tables[0].Rows[i][9]),
                                                            Convert.ToString(dsRRisco.Tables[0].Rows[i][10]),
                                                            Convert.ToString(dsRRisco.Tables[0].Rows[i][11]),
                                                            Convert.ToString(dsRRisco.Tables[0].Rows[i][12]),
                                                            Convert.ToString(dsRRisco.Tables[0].Rows[i][13]),
                                                            Convert.ToString(dsRRisco.Tables[0].Rows[i][14]),
                                                            Convert.ToString(dsRRisco.Tables[0].Rows[i][15]))) == 0)
                                {

                                }
                            }
                        }

                        dsRReferencia = MyConectorMain.ObterPepleoReferencia(aux);
                        countRows = dsRReferencia.Tables[0].DefaultView.Count;
                        if (countRows >= 1 && ataque == 0)
                        { //index 2.7
                            for (int i = 0; i < countRows; i++)
                            {
                                if ((ataque = conectorPDV_replace_clienteReferencia(Convert.ToString(dsRReferencia.Tables[0].Rows[i][0]),
                                                            Convert.ToString(dsRReferencia.Tables[0].Rows[i][1]),
                                                            Convert.ToString(dsRReferencia.Tables[0].Rows[i][2]),
                                                            Convert.ToString(dsRReferencia.Tables[0].Rows[i][3]),
                                                            Convert.ToString(dsRReferencia.Tables[0].Rows[i][4]),
                                                            Convert.ToString(dsRReferencia.Tables[0].Rows[i][5]),
                                                            Convert.ToString(dsRReferencia.Tables[0].Rows[i][6]),
                                                            Convert.ToString(dsRReferencia.Tables[0].Rows[i][7]),
                                                            Convert.ToString(dsRReferencia.Tables[0].Rows[i][8]),
                                                            Convert.ToString(dsRReferencia.Tables[0].Rows[i][9])
                                                                                            )) == 0)
                                {

                                }
                            }
                        }


                        dsRCobranca = MyConectorMain.ObterPepleoCobranca(aux);
                        countRows = dsRCobranca.Tables[0].DefaultView.Count;
                        if (countRows >= 1 && ataque == 0)
                        { //index 2.8
                            for (int i = 0; i < countRows; i++)
                            {
                                if ((ataque = conectorPDV_replace_clienteCobranca(Convert.ToString(dsRCobranca.Tables[0].Rows[i][0]),
                                                            Convert.ToString(dsRCobranca.Tables[0].Rows[i][1]),
                                                            Convert.ToString(dsRCobranca.Tables[0].Rows[i][2]),
                                                            Convert.ToString(dsRCobranca.Tables[0].Rows[i][3]),
                                                            Convert.ToString(dsRCobranca.Tables[0].Rows[i][4])
                                                                                            )) == 0)
                                {

                                }
                            }
                        }
                        dsREntrega = MyConectorMain.ObterPepleoEntrega(aux);
                        countRows = dsREndereco.Tables[0].DefaultView.Count;
                        if (countRows >= 1 && ataque == 0)
                        { //index 2.9
                            for (int i = 0; i < countRows; i++)
                            {
                                if ((ataque = conectorPDV_replace_clienteEntrega(Convert.ToString(dsREndereco.Tables[0].Rows[i][0]),
                                                            Convert.ToString(dsREndereco.Tables[0].Rows[i][1]),
                                                            Convert.ToString(dsREndereco.Tables[0].Rows[i][2]),
                                                            Convert.ToString(dsREndereco.Tables[0].Rows[i][3]),
                                                            Convert.ToString(dsREndereco.Tables[0].Rows[i][4])
                                                                                            )) == 0)
                                {

                                }
                            }
                        }
                    }
                }
                if (ataque == 0)//recursividade
                {
                    flagString = "tab.idCliente = " + aux; //only codigo.
                    conector_find_type_cliente();
                }
                MyConectorMain.Dispose();
            if (auxConsistencia == 0)
            {
                btnCarregarClienteSimplificado.Select();
            }
        }


        void conector_webservice_reverso_cliente(string aux, string store, string tipoPessoa, string auxIe, string auxEntidade, string auxEndereco, string auxPais, ref string retornoId)
        {
            WebConectorServer.Service MyConector = new WebConectorServer.Service(alwaysVariables.UrlWebConector);
            //WebConectorServer.Service MyConector = new WebConectorServer.Service();
            auxConsistencia = 0;
            int w = 0;

            try
            {
                auxConsistencia = 0;
                retornoId = auxIdCliente = MyConector.InserirPessoa(aux, store, tipoPessoa, alwaysVariables.Usuario, flagAtividade.ToString(), "Inserido ON-LINE", DateTime.Now.ToShortDateString(), null,
                    auxIdEstado, auxUfEstado, "0", newGridCodMunCliente, auxPais, null).ToString();
                string test = "";
                if (flagTipopessoa == 1 || flagTipopessoa == 3)
                {
                    test = newGridCpfCliente;
                }
                else
                {
                    test = newGridCnpjCliente;
                }
                switch (flagTipopessoa)
                {
                    case 1:
                        MyConector.InserirFisica(auxIdCliente, test, flagAtividade.ToString(), txtNomeCliente.Text, dtpDataNascCliente.Value.ToShortDateString(), auxIdSexy, auxEntidade, auxIdCivil);
                        break;
                    case 2:
                        MyConector.InserirJuridica(auxIdCliente, test, flagAtividade.ToString(), txtRazaoCliente.Text, txtRazaoCliente.Text, auxIe, dtpDataNascCliente.Value.ToShortDateString(), "4");
                        break;
                    case 3:
                        MyConector.InserirRural(auxIdCliente, test, flagAtividade.ToString(), txtNomeCliente.Text, auxIe, auxEntidade, dtpDataNascCliente.Value.ToShortDateString(), auxIdSexy, auxIdCivil);
                        break;
                }

                if (auxIdCliente != "" && Convert.ToDouble(auxIdCliente) > 0 && flagSemaforo == 0)
                {
                    MyConector.InserirEndereco(auxEndereco, auxIdCliente, "1", newGridcepCliente, auxIdCepCliente, "1", txtBairroCliente.Text, newGridLogradouroCliente, txtComplementoCliente.Text, txtCityCliente.Text, auxUfEstado, mskNumberCliente.Text);

                    conector_webservice_cliente(auxIdCliente, true);
                    flagSemaforo = 1;
                }
                else
                {
                    MyConector.InserirEndereco(auxEndereco, auxIdCliente, "1", newGridcepCliente, auxIdCepCliente, "1", txtBairroCliente.Text, newGridLogradouroCliente, txtComplementoCliente.Text, txtCityCliente.Text, auxUfEstado, mskNumberCliente.Text);
                }
                flagSemaforo = 1;
                //countRowsWebCliente = dsRCliente.Tables[0].DefaultView.Count;
            }
            catch (Exception e)
            {
                auxConsistencia = 1;
                MessageBox.Show("ERRO FATAL - " + "CONEXÃO COM WEBSERVICE FALHOU, REPITA A OPERAÇÃO E VERIFIQUE O STATUS DA INTERNET.", "Caro Usuário", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            MyConector.Dispose();
        }


        void conector_webservice_cliente(string aux, bool valida)
        {
            int countRowsWebCliente = 0;
            auxConsistencia = 0;
            ataque = 0;
            //############Variaveis Cliente Web
            DataSet dsRCliente = new DataSet();
            DataSet dsRFisica = new DataSet();
            DataSet dsRRural = new DataSet();
            DataSet dsRJuridica = new DataSet();
            DataSet dsRRisco = new DataSet();
            DataSet dsRReferencia = new DataSet();
            DataSet dsRProfissional = new DataSet();
            DataSet dsREntrega = new DataSet();
            DataSet dsRCobranca = new DataSet();
            DataSet dsREndereco = new DataSet();
            DataSet dsRFone = new DataSet();
            DataSet matriz = new DataSet();
            int w = 0;


            WebConectorServer.Service MyConector = new WebConectorServer.Service(alwaysVariables.UrlWebConector);
            //WebConector.Service MyConector = new WebConector.Service();
            //Matriz de retornos locais conectorPDV


            if ((valida == true) || (MessageBox.Show("Cliente não cadastrado, deseja atualizá-lo?", "Observação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes && ataque == 0))
            {
                if (auxConsistencia == 0)
                {
                    try
                    {
                        auxConsistencia = 0;
                        dsRCliente = MyConector.ObterMainPepleo(aux);
                        countRowsWebCliente = dsRCliente.Tables[0].DefaultView.Count;
                    }
                    catch (Exception e)
                    {
                        auxConsistencia = 1;
                        MessageBox.Show("ERRO FATAL - " + "CONEXÃO COM WEBSERVICE FALHOU, REPITA A OPERAÇÃO E VERIFIQUE O STATUS DA INTERNET.", "Caro Usuário", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                if (countRowsWebCliente == 1 && ataque == 0 && auxConsistencia == 0)//Cadastro do Cliente 
                {
                    if ((ataque = conectorPDV_replace_cliente(
                         Convert.ToString(dsRCliente.Tables[0].Rows[w][0]),
                         Convert.ToString(dsRCliente.Tables[0].Rows[w][1]),
                         Convert.ToString(dsRCliente.Tables[0].Rows[w][2]),
                         Convert.ToString(dsRCliente.Tables[0].Rows[w][3]),
                         Convert.ToString(dsRCliente.Tables[0].Rows[w][4]),
                         Convert.ToString(dsRCliente.Tables[0].Rows[w][5]),
                         Convert.ToString(dsRCliente.Tables[0].Rows[w][6]),
                         Convert.ToString(dsRCliente.Tables[0].Rows[w][7]),
                         Convert.ToString(dsRCliente.Tables[0].Rows[w][8]),
                         Convert.ToString(dsRCliente.Tables[0].Rows[w][9]),
                         Convert.ToString(dsRCliente.Tables[0].Rows[w][10]),
                         Convert.ToString(dsRCliente.Tables[0].Rows[w][11]),
                         Convert.ToString(dsRCliente.Tables[0].Rows[w][12]),
                         Convert.ToString(dsRCliente.Tables[0].Rows[w][13])
                        )) == 0)
                    {
                        //pgbWaitReservaConectorCF.Value = 3;
                    }
                    switch (Convert.ToString(dsRCliente.Tables[0].Rows[w][2]))//Tipo de pessoa
                    {
                        case "1":
                            if (auxConsistencia == 0)
                            {
                                try
                                {
                                    auxConsistencia = 0;
                                    dsRFisica = MyConector.ObterMainPepleoFisica(aux);
                                    countRowsWebCliente = dsRFisica.Tables[0].DefaultView.Count;
                                }
                                catch (Exception e)
                                {
                                    auxConsistencia = 1;
                                    MessageBox.Show("ERRO FATAL - " + "CONEXÃO COM WEBSERVICE FALHOU, REPITA A OPERAÇÃO E VERIFIQUE O STATUS DA INTERNET.", "Caro Usuário", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            if (countRowsWebCliente == 1 && auxConsistencia == 0)
                            {
                                if ((ataque = conectorPDV_replace_fisica(Convert.ToString(dsRFisica.Tables[0].Rows[w][0]),
                                                            Convert.ToString(dsRFisica.Tables[0].Rows[w][1]),
                                                            Convert.ToString(dsRFisica.Tables[0].Rows[w][2]),
                                                            Convert.ToString(dsRFisica.Tables[0].Rows[w][3]),
                                                            Convert.ToString(dsRFisica.Tables[0].Rows[w][4]),
                                                            Convert.ToString(dsRFisica.Tables[0].Rows[w][5]),
                                                            Convert.ToString(dsRFisica.Tables[0].Rows[w][6]),
                                                            Convert.ToString(dsRFisica.Tables[0].Rows[w][7]))) == 0)
                                {
                                    //pgbWaitReservaConectorCF.Value = 3;
                                }
                            }
                            break;
                        case "2":
                            if (auxConsistencia == 0)
                            {
                                try
                                {
                                    auxConsistencia = 0;
                                    dsRJuridica = MyConector.ObterMainPepleoJuridica(aux);
                                    countRowsWebCliente = dsRJuridica.Tables[0].DefaultView.Count;
                                }
                                catch (Exception e)
                                {
                                    auxConsistencia = 1;
                                    MessageBox.Show("ERRO FATAL - " + "CONEXÃO COM WEBSERVICE FALHOU, REPITA A OPERAÇÃO E VERIFIQUE O STATUS DA INTERNET.", "Caro Usuário", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            if (countRowsWebCliente == 1 && auxConsistencia == 0)
                            { //index 2.2
                                if ((ataque = conectorPDV_replace_juridica(Convert.ToString(dsRJuridica.Tables[0].Rows[w][0]),
                                                            Convert.ToString(dsRJuridica.Tables[0].Rows[w][1]),
                                                            Convert.ToString(dsRJuridica.Tables[0].Rows[w][2]),
                                                            Convert.ToString(dsRJuridica.Tables[0].Rows[w][3]),
                                                            Convert.ToString(dsRJuridica.Tables[0].Rows[w][4]),
                                                            Convert.ToString(dsRJuridica.Tables[0].Rows[w][5]),
                                                            Convert.ToString(dsRJuridica.Tables[0].Rows[w][6]),
                                                            Convert.ToString(dsRJuridica.Tables[0].Rows[w][7]))) == 0)
                                {
                                    //pgbWaitReservaConectorCF.Value = 3;
                                }
                            }
                            break;
                        case "3":
                            if (auxConsistencia == 0)
                            {
                                try
                                {
                                    auxConsistencia = 0;
                                    dsRRural = MyConector.ObterMainPepleoRural(aux);
                                    countRowsWebCliente = dsRRural.Tables[0].DefaultView.Count;
                                }
                                catch (Exception e)
                                {
                                    auxConsistencia = 1;
                                    MessageBox.Show("ERRO FATAL - " + "CONEXÃO COM WEBSERVICE FALHOU, REPITA A OPERAÇÃO E VERIFIQUE O STATUS DA INTERNET.", "Caro Usuário", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            if (countRowsWebCliente == 1 && auxConsistencia == 0)
                            { //index 2.2
                                if ((ataque = conectorPDV_replace_rural(Convert.ToString(dsRRural.Tables[0].Rows[w][0]),
                                                            Convert.ToString(dsRRural.Tables[0].Rows[w][1]),
                                                            Convert.ToString(dsRRural.Tables[0].Rows[w][2]),
                                                            Convert.ToString(dsRRural.Tables[0].Rows[w][3]),
                                                            Convert.ToString(dsRRural.Tables[0].Rows[w][4]),
                                                            Convert.ToString(dsRRural.Tables[0].Rows[w][5]),
                                                            Convert.ToString(dsRRural.Tables[0].Rows[w][6]),
                                                            Convert.ToString(dsRRural.Tables[0].Rows[w][7]),
                                                            Convert.ToString(dsRRural.Tables[0].Rows[w][8]))) == 0)
                                {
                                    //pgbWaitReservaConectorCF.Value = 3;
                                }
                            }
                            break;
                        default:
                            ataque = 2;
                            break;
                    }
                }
                if (ataque == 0 && auxConsistencia == 0)
                {
                    if (auxConsistencia == 0)
                    {
                        try
                        {
                            auxConsistencia = 0;
                            dsREndereco = MyConector.ObterEndereco(aux);
                            countRowsWebCliente = dsREndereco.Tables[0].DefaultView.Count;
                        }
                        catch (Exception e)
                        {
                            auxConsistencia = 1;
                            MessageBox.Show("ERRO FATAL - " + "CONEXÃO COM WEBSERVICE FALHOU, REPITA A OPERAÇÃO E VERIFIQUE O STATUS DA INTERNET.", "Caro Usuário", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    if (countRowsWebCliente >= 1 && ataque == 0 && auxConsistencia == 0)
                    { //index 2.3
                        for (w = 0; w < countRowsWebCliente; w++)
                        {
                            if ((ataque = conectorPDV_replace_endereco(Convert.ToString(dsREndereco.Tables[0].Rows[w][0]),
                                                        Convert.ToString(dsREndereco.Tables[0].Rows[w][1]),
                                                        Convert.ToString(dsREndereco.Tables[0].Rows[w][2]),
                                                        Convert.ToString(dsREndereco.Tables[0].Rows[w][3]),
                                                        Convert.ToString(dsREndereco.Tables[0].Rows[w][4]),
                                                        Convert.ToString(dsREndereco.Tables[0].Rows[w][5]),
                                                        Convert.ToString(dsREndereco.Tables[0].Rows[w][6]),
                                                        Convert.ToString(dsREndereco.Tables[0].Rows[w][7]),
                                                        Convert.ToString(dsREndereco.Tables[0].Rows[w][8]),
                                                        Convert.ToString(dsREndereco.Tables[0].Rows[w][9]),
                                                        Convert.ToString(dsREndereco.Tables[0].Rows[w][10]),
                                                        Convert.ToString(dsREndereco.Tables[0].Rows[w][11]))) == 0)
                            {
                                //pgbWaitReservaConectorCF.Value = 4;
                            }
                        }
                    }
                    if (auxConsistencia == 0)
                    {
                        try
                        {
                            auxConsistencia = 0;
                            dsRFone = MyConector.ObterFone(aux);
                            countRowsWebCliente = dsRFone.Tables[0].DefaultView.Count;
                        }
                        catch (Exception e)
                        {
                            auxConsistencia = 1;
                            MessageBox.Show("ERRO FATAL - " + "CONEXÃO COM WEBSERVICE FALHOU, REPITA A OPERAÇÃO E VERIFIQUE O STATUS DA INTERNET.", "Caro Usuário", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    if (countRowsWebCliente >= 1 && ataque == 0 && auxConsistencia == 0)
                    {//index 2.4
                        for (w = 0; w < countRowsWebCliente; w++)
                        {
                            if ((ataque = conectorPDV_replace_fone(Convert.ToString(dsRFone.Tables[0].Rows[w][0]),
                                                        Convert.ToString(dsRFone.Tables[0].Rows[w][1]),
                                                        Convert.ToString(dsRFone.Tables[0].Rows[w][2]),
                                                        Convert.ToString(dsRFone.Tables[0].Rows[w][3]),
                                                        Convert.ToString(dsRFone.Tables[0].Rows[w][4]),
                                                        Convert.ToString(dsRFone.Tables[0].Rows[w][5]),
                                                        Convert.ToString(dsRFone.Tables[0].Rows[w][6]),
                                                        Convert.ToString(dsRFone.Tables[0].Rows[w][7]),
                                                        Convert.ToString(dsRFone.Tables[0].Rows[w][8]))) == 0)
                            {
                                //pgbWaitReservaConectorCF.Value = 5;
                            }
                        }
                    }
                    if (auxConsistencia == 0)
                    {
                        try
                        {
                            auxConsistencia = 0;
                            dsRProfissional = MyConector.ObterPepleoProfissional(aux);
                            countRowsWebCliente = dsRProfissional.Tables[0].DefaultView.Count;
                        }
                        catch (Exception e)
                        {
                            auxConsistencia = 1;
                            MessageBox.Show("ERRO FATAL - " + "CONEXÃO COM WEBSERVICE FALHOU, REPITA A OPERAÇÃO E VERIFIQUE O STATUS DA INTERNET.", "Caro Usuário", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    if (countRowsWebCliente >= 1 && ataque == 0)
                    { //index 2.5
                        for (w = 0; w < countRowsWebCliente; w++)
                        {
                            if ((ataque = conectorPDV_replace_clienteProfissional(Convert.ToString(dsRProfissional.Tables[0].Rows[w][0]),
                                                        Convert.ToString(dsRProfissional.Tables[0].Rows[w][1]),
                                                        Convert.ToString(dsRProfissional.Tables[0].Rows[w][2]),
                                                        Convert.ToString(dsRProfissional.Tables[0].Rows[w][3]),
                                                        Convert.ToString(dsRProfissional.Tables[0].Rows[w][4]),
                                                        Convert.ToString(dsRProfissional.Tables[0].Rows[w][5]),
                                                        Convert.ToString(dsRProfissional.Tables[0].Rows[w][6]),
                                                        Convert.ToString(dsRProfissional.Tables[0].Rows[w][7]),
                                                        Convert.ToString(dsRProfissional.Tables[0].Rows[w][8]))) == 0)
                            {
                                //pgbWaitReservaConectorCF.Value = 6;
                            }
                        }
                    }
                    if (auxConsistencia == 0)
                    {
                        try
                        {
                            auxConsistencia = 0;
                            dsRRisco = MyConector.ObterPepleoRisco(aux);
                            countRowsWebCliente = dsRRisco.Tables[0].DefaultView.Count;
                        }
                        catch (Exception e)
                        {
                            auxConsistencia = 1;
                            MessageBox.Show("ERRO FATAL - " + "CONEXÃO COM WEBSERVICE FALHOU, REPITA A OPERAÇÃO E VERIFIQUE O STATUS DA INTERNET.", "Caro Usuário", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    if (countRowsWebCliente >= 1 && ataque == 0 && auxConsistencia == 0)
                    { //index 2.6
                        for (w = 0; w < countRowsWebCliente; w++)
                        {
                            if ((ataque = conectorPDV_replace_clienteRisco(Convert.ToString(dsRRisco.Tables[0].Rows[w][0]),
                                                        Convert.ToString(dsRRisco.Tables[0].Rows[w][1]),
                                                        Convert.ToString(dsRRisco.Tables[0].Rows[w][2]),
                                                        Convert.ToString(dsRRisco.Tables[0].Rows[w][3]),
                                                        Convert.ToString(dsRRisco.Tables[0].Rows[w][4]),
                                                        Convert.ToString(dsRRisco.Tables[0].Rows[w][5]),
                                                        Convert.ToString(dsRRisco.Tables[0].Rows[w][6]),
                                                        Convert.ToString(dsRRisco.Tables[0].Rows[w][7]),
                                                        Convert.ToString(dsRRisco.Tables[0].Rows[w][8]),
                                                        Convert.ToString(dsRRisco.Tables[0].Rows[w][9]),
                                                        Convert.ToString(dsRRisco.Tables[0].Rows[w][10]),
                                                        Convert.ToString(dsRRisco.Tables[0].Rows[w][11]),
                                                        Convert.ToString(dsRRisco.Tables[0].Rows[w][12]),
                                                        Convert.ToString(dsRRisco.Tables[0].Rows[w][13]),
                                                        Convert.ToString(dsRRisco.Tables[0].Rows[w][14]),
                                                        Convert.ToString(dsRRisco.Tables[0].Rows[w][15]))) == 0)
                            {
                                //pgbWaitReservaConectorCF.Value = 7;
                            }
                        }
                    }
                    if (auxConsistencia == 0)
                    {
                        try
                        {
                            auxConsistencia = 0;
                            dsRReferencia = MyConector.ObterPepleoReferencia(aux);
                            countRowsWebCliente = dsRReferencia.Tables[0].DefaultView.Count;
                        }
                        catch (Exception e)
                        {
                            auxConsistencia = 1;
                            MessageBox.Show("ERRO FATAL - " + "CONEXÃO COM WEBSERVICE FALHOU, REPITA A OPERAÇÃO E VERIFIQUE O STATUS DA INTERNET.", "Caro Usuário", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    if (countRowsWebCliente >= 1 && ataque == 0 && auxConsistencia == 0)
                    { //index 2.7
                        for (w = 0; w < countRowsWebCliente; w++)
                        {
                            if ((ataque = conectorPDV_replace_clienteReferencia(Convert.ToString(dsRReferencia.Tables[0].Rows[w][0]),
                                                        Convert.ToString(dsRReferencia.Tables[0].Rows[w][1]),
                                                        Convert.ToString(dsRReferencia.Tables[0].Rows[w][2]),
                                                        Convert.ToString(dsRReferencia.Tables[0].Rows[w][3]),
                                                        Convert.ToString(dsRReferencia.Tables[0].Rows[w][4]),
                                                        Convert.ToString(dsRReferencia.Tables[0].Rows[w][5]),
                                                        Convert.ToString(dsRReferencia.Tables[0].Rows[w][6]),
                                                        Convert.ToString(dsRReferencia.Tables[0].Rows[w][7]),
                                                        Convert.ToString(dsRReferencia.Tables[0].Rows[w][8]),
                                                        Convert.ToString(dsRReferencia.Tables[0].Rows[w][9])
                                                                                        )) == 0)
                            {
                                //pgbWaitReservaConectorCF.Value = 8;
                            }
                        }
                    }
                    if (auxConsistencia == 0)
                    {
                        try
                        {
                            auxConsistencia = 0;
                            dsRCobranca = MyConector.ObterPepleoCobranca(aux);
                            countRowsWebCliente = dsRCobranca.Tables[0].DefaultView.Count;
                        }
                        catch (Exception e)
                        {
                            auxConsistencia = 1;
                            MessageBox.Show("ERRO FATAL - " + "CONEXÃO COM WEBSERVICE FALHOU, REPITA A OPERAÇÃO E VERIFIQUE O STATUS DA INTERNET.", "Caro Usuário", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                    }
                    if (countRowsWebCliente >= 1 && ataque == 0 && auxConsistencia == 0)
                    { //index 2.8
                        for (w = 0; w < countRowsWebCliente; w++)
                        {
                            if ((ataque = conectorPDV_replace_clienteCobranca(Convert.ToString(dsRCobranca.Tables[0].Rows[w][0]),
                                                        Convert.ToString(dsRCobranca.Tables[0].Rows[w][1]),
                                                        Convert.ToString(dsRCobranca.Tables[0].Rows[w][2]),
                                                        Convert.ToString(dsRCobranca.Tables[0].Rows[w][3]),
                                                        Convert.ToString(dsRCobranca.Tables[0].Rows[w][4])
                                                                                        )) == 0)
                            {
                                //pgbWaitReservaConectorCF.Value = 9;
                            }
                        }
                    }
                    if (auxConsistencia == 0)
                    {
                        try
                        {
                            auxConsistencia = 0;
                            dsREntrega = MyConector.ObterPepleoEntrega(aux);
                            countRowsWebCliente = dsREndereco.Tables[0].DefaultView.Count;
                        }
                        catch (Exception e)
                        {
                            auxConsistencia = 1;
                            MessageBox.Show("ERRO FATAL - " + "CONEXÃO COM WEBSERVICE FALHOU, REPITA A OPERAÇÃO E VERIFIQUE O STATUS DA INTERNET.", "Caro Usuário", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    if (countRowsWebCliente >= 1 && ataque == 0 && auxConsistencia == 0)
                    { //index 2.9
                        for (w = 0; w < countRowsWebCliente; w++)
                        {
                            if ((ataque = conectorPDV_replace_clienteEntrega(Convert.ToString(dsREndereco.Tables[0].Rows[w][0]),
                                                        Convert.ToString(dsREndereco.Tables[0].Rows[w][1]),
                                                        Convert.ToString(dsREndereco.Tables[0].Rows[w][2]),
                                                        Convert.ToString(dsREndereco.Tables[0].Rows[w][3]),
                                                        Convert.ToString(dsREndereco.Tables[0].Rows[w][4])
                                                                                        )) == 0)
                            {
                                //pgbWaitReservaConectorCF.Value = 10;
                            }
                        }
                    }

                }
                if (auxConsistencia == 0 && ataque == 0)
                {
                    MessageBox.Show("Cliente cadastrado com sucesso...!", "Caro Usuário", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //conector_visible_web("0");
                    //conector_carrega_cliente(aux);
                }
            }
            else
            {
                ataque = 1;
                MessageBox.Show("OPERAÇÃO CANCELADA...!", "Caro Usuário", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            MyConector.Dispose();
        }


        //#############################################################Metodos De Controle Webservice######################################################

        //##########################Bloco controle de objetos###############################
        private void conector_find_type_cliente()
        {
            if (rdCodigoSimplificado.Checked == true || rdCPFSimplificado.Checked == true || rdCNPJSimplificado.Checked == true || rdNomeSimplificado.Checked == true || rdTelefoneSimplificado.Checked == true || rdIdentidadeSimplificado.Checked == true || rdRazaoSimplificado.Checked == true)
            {
                zera_variavel();
                flagSemaforo = 1;
                switch (flagTest)//O switch busca as opções em que a busca necessita carrega o grid separadamente.
                {
                    case 1:
                        flagString = " tab.idCliente =  " + txtCodigoCliente.Text;
                        if (txtCodigoCliente.Text != null && txtCodigoCliente.Text != "")
                        {
                            conector_prepare_pessoa();
                        }
                        break;
                    case 7:
                        if (rdRazaoSimplificado.Checked == true)
                        {
                            if (txtRazaoCliente.Text != "")
                            {
                                verifica = conector_find_consultaNumberRazao();
                                if ((verifica != "1") && (verifica != "0"))
                                {
                                    openName = new consultaClienteNome(txtNomeCliente.Text, Convert.ToString(flagTest));
                                    if (openName.ShowDialog(this) == DialogResult.OK)
                                    {
                                        auxIdCliente = openName.GridCodigo;
                                        flagString = " tab.idCliente =  " + auxIdCliente;
                                        if (auxIdCliente != null && auxIdCliente != "")
                                        {
                                            conector_prepare_pessoa();
                                        }
                                    }
                                }
                                else { conector_prepare_pessoa(); }
                            }
                            else { MessageBox.Show("Razao inválida.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        }
                        else
                        {
                            MessageBox.Show("Selecione a opção de consulta 'RAZÃO'.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtRazaoCliente.Select();
                        }
                        break;
                    case 4:
                        if (rdNomeSimplificado.Checked == true)
                        {
                            if (txtNomeCliente.Text != "")
                            {
                                verifica = conector_find_consultaNumberName();
                                if ((verifica != "1") && (verifica != "0"))
                                {
                                    openName = new consultaClienteNome(txtNomeCliente.Text, Convert.ToString(flagTest));
                                    if (openName.ShowDialog(this) == DialogResult.OK)
                                    {
                                        auxIdCliente = openName.GridCodigo;
                                        flagString = " tab.idCliente =  " + auxIdCliente;
                                        if (auxIdCliente != null && auxIdCliente != "")
                                        {
                                            conector_prepare_pessoa();   
                                        }
                                    }
                                }
                                else { conector_prepare_pessoa(); }
                            }
                            else
                            {
                                MessageBox.Show("Nome inválido.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtNomeCliente.Select();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Selecione a opção de consulta 'NOME'.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtNomeCliente.Select();
                        }
                        break;
                    case 5:
                        if (rdTelefoneSimplificado.Checked == true)
                        {
                            if (newtelefone.Trim().Length == 8)
                            {
                                verifica = conector_find_consultaNumberFone();
                                if ((verifica != "1") && (verifica != "0"))
                                {

                                    openFone = new consultaFone(newtelefone);
                                    if (openFone.ShowDialog(this) == DialogResult.OK)
                                    {
                                        auxIdCliente = openFone.GridCodigo;
                                        flagString = " tab.idCliente =  " + auxIdCliente;
                                        if (auxIdCliente != null && auxIdCliente != "")
                                        {
                                            conector_prepare_pessoa();   
                                        }
                                    }
                                }
                                else { conector_prepare_pessoa(); }
                            }
                            else
                            {
                                MessageBox.Show("Telefone inválido.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                mskTelefoneSimplificado.Select();
                            }

                        }
                        else
                        {
                            MessageBox.Show("Selecione a opção de consulta 'TELEFONE'.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            mskTelefoneSimplificado.Select();
                        }
                        break;
                    default:
                        conector_prepare_pessoa();
                        break;
                }
            }
            else
            {
                MessageBox.Show("Opção de consulta inválida favor, seleciona-lá.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                rdCodigoSimplificado.Select();
                flagString = "";
            }
            if (rdCodigoSimplificado.Checked == true) { flagString = txtCodigoCliente.Text; }
            if (rdCPFSimplificado.Checked == true)
            {
                flagValidation = 1;
                newGridCpfCliente = mskCpfCliente.Text;
                newGridCpfCliente = mskCpfCliente.Text.Replace(".", "");
                newGridCpfCliente = mskCpfCliente.Text.Replace(",", "");
                newGridCpfCliente = newGridCpfCliente.Replace("-", "");
                newGridCpfCliente = newGridCpfCliente.Replace("/", "");
                flagString = " tab1.cpf = " + newGridCpfCliente;
            }
            if (rdCNPJSimplificado.Checked == true)
            {
                flagValidation = 1;
                newGridCnpjCliente = mskCnpjCliente.Text;
                newGridCnpjCliente = mskCnpjCliente.Text.Replace(".", "");
                newGridCnpjCliente = newGridCnpjCliente.Replace("-", "");
                newGridCnpjCliente = newGridCnpjCliente.Replace("/", "");
                newGridCnpjCliente = newGridCnpjCliente.Replace(",", "");
                flagString = " tab2.cnpj =  " + newGridCnpjCliente;
            }
            if (rdNomeSimplificado.Checked == true) { flagString = " tab1.nome like concat('" + txtNomeCliente.Text + "','%') "; }
            if (rdTelefoneSimplificado.Checked == true)
            {
                newtelefone = mskTelefoneSimplificado.Text;
                newtelefone = mskTelefoneSimplificado.Text.Replace(".", "");
                newtelefone = newtelefone.Replace("-", "");
                newtelefone = newtelefone.Replace("/", "");
                newtelefone = newtelefone.Replace(",", "");
                flagString = "  tab1.idcliente in (select tab.idCliente from cliente tab left join fone tab6 on(tab.idCliente = tab6.idCliente) where tab6.telefone=" + newtelefone + ")";
            }
            if (rdIdentidadeSimplificado.Checked == true)
            {
                if (flagTipopessoa == 1)
                {
                    flagString = " tab1.identidade =  " + txtRgCliente.Text + " and tab.idtipoPessoa=1";
                }
                else
                {
                    flagString = " tab3.identidade =  " + txtRgCliente.Text + " and tab.idtipoPessoa=3";
                }
            }
            if (rdRazaoSimplificado.Checked == true)
            {
                flagString = "tab2.razao like concat('" + txtRazaoCliente.Text + "','%') ";
            }

        }
        private void alteraIconesCancelar()
        {
            txtCodigoMunicipioCliente.Text = newGridCodMunCliente;
            txtBairroCliente.Text = newGridBairroCliente;
            txtCityCliente.Text = newGridCityCliente;
            mskNumberCliente.Text = newGridNumber;
            txtComplementoCliente.Text = newGridComplementoCliente;
            txtEnderecoCliente.Text = newGridLogradouroCliente;
            mskCepCliente.Text = newGridcepCliente;
            cmbUfCliente.Text = auxUfEstado;
            txtCodigoCliente.Text = auxIdCliente;
            txtCodigoMunicipioCliente.Text = newGridCodMunCliente;
            txtNomeCliente.Text = newGridNameCliente;
            txtRazaoCliente.Text = newGridRazaoCliente;
            mskCpfCliente.Text = auxGridCpfCliente;
            txtRgCliente.Text = newGridRgCliente;
            mskCnpjCliente.Text = auxGridCnpjCliente;
            txtIeCliente.Text = newGridIeCliente;
            if (chkSimplificadoCadastroCliente.Checked == true)
            {
                inserirClienteSimplificado.Enabled = true;
                procuraClienteSimplificado.Enabled = true;
                salvarAlteracaoClienteSimplificado.Enabled = false;
                cancelarClienteSimplificado.Enabled = false;
            }
            else
            {
                inserirClienteSimplificado.Enabled = false;
                procuraClienteSimplificado.Enabled = true;
                salvarAlteracaoClienteSimplificado.Enabled = false;
                cancelarClienteSimplificado.Enabled = false;
            }
            if (txtCodigoCliente.Text != "")
            {
                flagSemaforo = 1;
            }
        }
        void reabilitaRadio()
        {
            rdbPessoaFisicaCliente.Enabled = true;
            rdbPessoaJuridicaCliente.Enabled = true;
            rbPessoaRuralCliente.Enabled = true;
        }
        void insertClient()
        {
            auxIdCivil = "9";
            auxIdSexy = "3";
            auxConsistencia = 0;
            txtCodigoCliente.ReadOnly = true;
            flagValidation = 0;
            rdbPessoaFisicaCliente.Enabled = true;
            rbPessoaRuralCliente.Enabled = true;
            rdbPessoaJuridicaCliente.Enabled = true;
            if ((rbPessoaRuralCliente.Checked == true) || (rdbPessoaJuridicaCliente.Checked == true) || (rdbPessoaFisicaCliente.Checked == true))
            {
                auxConsistencia = 0;
                clearObj();
                alteraIconesIncluir();
                statusObj(true);
                txtNomeCliente.Select();
                flagSemaforo = 0;
                auxIdCepCliente = "0";
                auxIdEstado = "0";
                auxUfEstado = "";
                if (rdbPessoaFisicaCliente.Checked == true)
                {
                    flagTipopessoa = 1;
                    txtRazaoCliente.Clear();
                    txtRazaoCliente.ReadOnly = true;
                    mskCnpjCliente.Clear();
                    mskCnpjCliente.ReadOnly = true;
                    txtIeCliente.ReadOnly = true;
                    mskCpfCliente.ReadOnly = false;
                    txtRgCliente.ReadOnly = false;
                }
                else if (rdbPessoaJuridicaCliente.Checked == true)
                {
                    flagTipopessoa = 2;
                    txtRazaoCliente.ReadOnly = false;
                    mskCnpjCliente.ReadOnly = false;
                    txtIeCliente.ReadOnly = false;
                    txtRgCliente.Clear();
                    mskCpfCliente.Clear();
                    mskCpfCliente.ReadOnly = true;
                    txtRgCliente.ReadOnly = true;
                    txtRazaoCliente.Select();
                }
                else if (rbPessoaRuralCliente.Checked == true)
                {
                    flagTipopessoa = 3;
                    txtRazaoCliente.Clear();
                    txtRazaoCliente.ReadOnly = true;
                    mskCnpjCliente.Clear();
                    mskCnpjCliente.ReadOnly = true;
                    txtIeCliente.Clear();
                    txtIeCliente.ReadOnly = false;
                    mskCpfCliente.ReadOnly = false;
                    txtRgCliente.ReadOnly = false;
                }

            }
            else { MessageBox.Show("Qual o tipo de pessoa você deseja cadastrar.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information); 
            }
            
        }
        void clearObj()
        {
            mskDDDSimplificado.Clear();
            mskTelefoneSimplificado.Clear();
            mskNumberCliente.Clear();
            txtCodigoCliente.Clear();
            txtNomeCliente.Clear();
            txtRazaoCliente.Clear();
            mskCpfCliente.Clear();
            mskCnpjCliente.Clear();
            txtRgCliente.Clear();
            txtIeCliente.Clear();
            dtpDataNascCliente.Value = DateTime.Now;
            mskCepCliente.Clear();
            txtEnderecoCliente.Clear();
            txtCodigoMunicipioCliente.Clear();
            txtEnderecoCliente.Clear();
            txtBairroCliente.Clear();
            txtCityCliente.Clear();
            cmbUfCliente.SelectedIndex = -1;
            txtComplementoCliente.Clear();
        }
        void saveClient()
        {
            auxConsistencia = 0;
            flagValidation = 0;
            newGridCnpjCliente = mskCnpjCliente.Text;
            newGridCnpjCliente = mskCnpjCliente.Text.Replace(".", "");
            newGridCnpjCliente = newGridCnpjCliente.Replace("-", "");
            newGridCnpjCliente = newGridCnpjCliente.Replace("/", "");
            newGridCnpjCliente = newGridCnpjCliente.Replace(",", "");

            newGridCpfCliente = mskCpfCliente.Text;
            newGridCpfCliente = mskCpfCliente.Text.Replace(".", "");
            newGridCpfCliente = newGridCpfCliente.Replace("-", "");
            newGridCpfCliente = newGridCpfCliente.Replace("/", "");
            newGridCpfCliente = newGridCpfCliente.Replace(",", "");

            newGridcepCliente = mskCepCliente.Text;
            newGridcepCliente = mskCepCliente.Text.Replace(".", "");
            newGridcepCliente = newGridcepCliente.Replace("-", "");
            newGridcepCliente = newGridcepCliente.Replace(",", "");
            if ((flagTipopessoa == 1) || (flagTipopessoa == 3))
            {
                if (((Convert.ToInt32(DateTime.Now.Year) - Convert.ToInt32(dtpDataNascCliente.Value.Year))) >= 18)
                {
                    if (((flagTipopessoa == 1) || (flagTipopessoa == 3)) && (mskCepCliente.Text != "") && (newGridcepCliente.Trim().Length == 8))
                    {
                        if (((flagTipopessoa == 1) || (flagTipopessoa == 3)) && (txtNomeCliente.Text != ""))
                        {
                            if (((flagTipopessoa == 1) || (flagTipopessoa == 3)) && (txtCodigoMunicipioCliente.Text != ""))
                            {
                                if (((flagTipopessoa == 1) || (flagTipopessoa == 3)) && (mskCpfCliente.Text != "") && (newGridCpfCliente.Trim().Length == 11))
                                {

                                    if (((flagTipopessoa == 1) || (flagTipopessoa == 3)) && (cmbUfCliente.Text != ""))
                                    {
                                        if (alwaysVariables.ImportCarga == 1 || 1 == 1)//Flavio Utilizar Recursividade
                                        {
                                            string aux = "0";
                                            conector_webservice_reverso_cliente("0", alwaysVariables.Store, flagTipopessoa.ToString(), txtIeCliente.Text, txtRgCliente.Text, "0", "30", ref aux);
                                            if (aux != null && aux != "" && Convert.ToInt32(aux) > 0)
                                            {
                                                flagTest = 1;
                                                txtCodigoCliente.Text = aux;
                                                if (txtCodigoCliente.Text != "" && Convert.ToDouble(txtCodigoCliente.Text) > 0)
                                                {
                                                    conector_find_type_cliente();
                                                    if (auxConsistencia == 0)
                                                    {
                                                        btnCarregarClienteSimplificado.Select();
                                                    }
                                                    if (liberaWeb == 0)
                                                    {
                                                        return;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (flagSemaforo == 0)
                                            {
                                                conector_inc_cliente();
                                                if (flagTipopessoa == 1)
                                                {
                                                    conector_inc_fisica();
                                                    rdbPessoaFisicaCliente.Enabled = true;
                                                    rbPessoaRuralCliente.Enabled = false;
                                                    rdbPessoaJuridicaCliente.Enabled = false;
                                                }
                                                else if (flagTipopessoa == 2)
                                                {
                                                    conector_inc_juridica();
                                                    rdbPessoaFisicaCliente.Enabled = false;
                                                    rbPessoaRuralCliente.Enabled = false;
                                                    rdbPessoaJuridicaCliente.Enabled = true;
                                                }
                                                else if (flagTipopessoa == 3)
                                                {
                                                    conector_inc_rural();
                                                    rdbPessoaFisicaCliente.Enabled = false;
                                                    rbPessoaRuralCliente.Enabled = true;
                                                    rdbPessoaJuridicaCliente.Enabled = false;
                                                }
                                            }
                                            else
                                            {
                                                conector_alt_cliente();
                                                if (flagTipopessoa == 1)
                                                {
                                                    conector_alt_fisica();
                                                }
                                                else if (flagTipopessoa == 2)
                                                {
                                                    conector_alt_juridica();
                                                }
                                                else if (flagTipopessoa == 3)
                                                {
                                                    conector_alt_rural();
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Favor preencher o estado do cliente.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        cmbUfCliente.Select();
                                    }

                                }
                                else
                                {
                                    MessageBox.Show("CPF não preenchido, favor preencher.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    mskCpfCliente.Select();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Codigo do Municipio não preenchido, favor preencher.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtCodigoMunicipioCliente.Select();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Nome não preenchido, favor preencher.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtNomeCliente.Select();
                        }
                    }
                    else
                    {
                        MessageBox.Show("CEP invalido, favor preencher de acordo com o seu municipio.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtNomeCliente.Select();
                    }

                }
                else
                {
                    MessageBox.Show("Menos de 18 anos, cancele o cadastro.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                txtNomeCliente.Select();
                switch (flagTest)
                {
                    case 1:
                        txtCodigoCliente.Select();
                        txtCodigoCliente.Enabled = true;
                        txtCodigoCliente.ReadOnly = false;
                        break;
                    case 2:
                        mskCpfCliente.Select();
                        mskCpfCliente.Enabled = true;
                        mskCpfCliente.ReadOnly = false;
                        break;
                    case 3:
                        mskCnpjCliente.Select();
                        mskCnpjCliente.Enabled = true;
                        mskCnpjCliente.ReadOnly = false;
                        break;
                    case 4:
                        txtNomeCliente.Select();
                        txtNomeCliente.Enabled = true;
                        txtNomeCliente.ReadOnly = false;
                        break;
                    case 5:
                        mskTelefoneSimplificado.Select();
                        mskTelefoneSimplificado.Enabled = true;
                        mskTelefoneSimplificado.ReadOnly = false;
                        break;
                    case 6:
                        txtRgCliente.Select();
                        txtRgCliente.Enabled = true;
                        txtRgCliente.ReadOnly = false;
                        break;
                    case 7:
                        txtRazaoCliente.Select();
                        txtRazaoCliente.Enabled = true;
                        txtRazaoCliente.ReadOnly = false;
                        break;
                    default:
                        break;
                }
                mskTelefoneSimplificado.CausesValidation = true;

            }
            else if (flagTipopessoa == 2)
            {
                if ((mskCepCliente.Text != "") && (newGridcepCliente.Length == 8))
                {
                    if ((txtRazaoCliente.Text != ""))
                    {
                        if ((txtCodigoMunicipioCliente.Text != ""))
                        {
                            if ((mskCnpjCliente.Text != "") && (newGridCnpjCliente.Length == 14))
                            {
                                if ((cmbUfCliente.Text != ""))
                                {
                                    if (flagSemaforo == 0)
                                    {
                                        conector_inc_cliente();
                                        if (flagTipopessoa == 1)
                                        {
                                            conector_inc_fisica();
                                        }
                                        else if (flagTipopessoa == 2)
                                        {
                                            conector_inc_juridica();
                                        }
                                        else if (flagTipopessoa == 3)
                                        {
                                            conector_inc_rural();
                                        }
                                    }
                                    else
                                    {
                                        conector_alt_cliente();
                                        if (flagTipopessoa == 1)
                                        {
                                            conector_alt_fisica();
                                        }
                                        else if (flagTipopessoa == 2)
                                        {
                                            conector_alt_juridica();
                                        }
                                        else if (flagTipopessoa == 3)
                                        {
                                            conector_alt_rural();
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Favor preencher o estado do cliente.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    cmbUfCliente.Select();
                                }
                            }
                            else
                            {
                                MessageBox.Show("CNPJ não preenchido, favor preencher.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                mskCpfCliente.Select();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Codigo do Municipio não preenchido, favor preencher.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtCodigoMunicipioCliente.Select();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Razao não preenchida, favor preencher.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtNomeCliente.Select();
                    }
                }
                else
                {
                    MessageBox.Show("CEP invalido, favor preencher de acordo com o seu municipio.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNomeCliente.Select();
                }

                txtRazaoCliente.Select();
            }
            chkSimplificadoCadastroCliente.CausesValidation = false;
            chkSimplificadoCadastroCliente.Checked = false;
            chkSimplificadoCadastroCliente.CausesValidation = true;
        }
        void zera_variavel()
        {
            if (flagSemaforo == 0)
            {
                auxConsistencia = 0;
                auxIdEndereco = "";
                auxIdCliente = "";
                auxIdLoja = "";
                auxIdEstado = "";
                auxUfEstado = "";
                auxIdSexy = "";
                auxIdCivil = "";
                auxIdCepCliente = "";
            }
            newGridcepCliente = "";
            newGridObs = "";
            newGridTypeFisicaCliente = "";
            newGridTypeJuricaCliente = "";
            newGridTypeRuralCliente = "";
            newGridCodigoCliente = "";
            newGridNameCliente = "";
            newGridInativoCliente = "";
            newGridRazaoCliente = "";
            newGridFantasiaCliente = "";
            newGridLogradouroCliente = "";
            newGridRgCliente = "";
            newGridIeCliente = "";
            newGridCpfCliente = "";
            newGridCnpjCliente = "";
            newGridHomePageCliente = "";
            newGridBairroCliente = "";
            newGridCityCliente = "";
            newGridComplementoCliente = "";
            newGridUsuarioCadastroCliente = "";
            newGridUsuarioCadastroAlteracaoCliente = "";
            newGridDataNascCliente = "";
            newGridDataInclusaoCliente = "";
            newGridCodMunCliente = "";
            newGridNumber = "";
        }
        void controleObj()
        {
            txtCodigoMunicipioCliente.Text = newGridCodMunCliente;
            txtBairroCliente.Text = newGridBairroCliente;
            txtCityCliente.Text = newGridCityCliente;
            mskNumberCliente.Text = newGridNumber;
            txtComplementoCliente.Text = newGridComplementoCliente;
            txtEnderecoCliente.Text = newGridLogradouroCliente;
            mskCepCliente.Text = newGridcepCliente;
            cmbUfCliente.Text = auxUfEstado;
            txtCodigoCliente.Text = auxIdCliente;
            txtCodigoMunicipioCliente.Text = newGridCodMunCliente;
            txtNomeCliente.Text = newGridNameCliente;
            txtRazaoCliente.Text = newGridRazaoCliente;
            mskCpfCliente.Text = auxGridCpfCliente;
            txtRgCliente.Text = newGridRgCliente;
            mskCnpjCliente.Text = auxGridCnpjCliente;
            txtIeCliente.Text = newGridIeCliente;
            txtCodigoMunicipioCliente.Text = newGridCodMunCliente;
            
        }
        void statusObj(Boolean flag)
        {
            mskDDDSimplificado.Enabled = flag;
            mskTelefoneSimplificado.Enabled = flag;
            mskNumberCliente.Enabled = flag;
            txtCodigoCliente.Enabled = flag;
            txtNomeCliente.Enabled = flag;
            txtRazaoCliente.Enabled = flag;
            mskCpfCliente.Enabled = flag;
            mskCnpjCliente.Enabled = flag;
            txtRgCliente.Enabled = flag;
            txtIeCliente.Enabled = flag;
            dtpDataNascCliente.Enabled = flag;
            mskCepCliente.Enabled = flag;
            btnPesquisaCliente.Enabled = flag;
            txtEnderecoCliente.Enabled = flag;
            txtCodigoMunicipioCliente.Enabled = flag;
            btnLocalizaCodMunicipio.Enabled = flag;
            txtEnderecoCliente.Enabled = flag;
            txtBairroCliente.Enabled = flag;
            txtCityCliente.Enabled = flag;
            cmbUfCliente.Enabled = flag;
            txtComplementoCliente.Enabled = flag;
            btnHistoricoClienteComprasSimplificado.Enabled = flag;
            btnSPCClienteSimplificado.Enabled = flag;
            btnResumoClienteSimplificado.Enabled = flag;
            btnConvenioClienteSimplificado.Enabled = flag;
            btnReferenciaClienteSimplificado.Enabled = flag;
            btnCarregarClienteSimplificado.Enabled = flag;
            btnSalvarClienteSimplificado.Enabled = flag;
            //btnCancelarClienteSimplificado.Enabled = flag;
            //btnLimparClienteSimplificado.Enabled = flag;
        }
        void statusObjRadio(Boolean flag)
        {
            rdCodigoSimplificado.Enabled = flag;
            rdCPFSimplificado.Enabled = flag;
            rdCNPJSimplificado.Enabled = flag;
            rdNomeSimplificado.Enabled = flag;
            rdRazaoSimplificado.Enabled = flag;
            rdTelefoneSimplificado.Enabled = flag;
            rdIdentidadeSimplificado.Enabled = flag;
            btnPesquisaSimplificada.Enabled = flag;
        }
        private void alteraIconesIncluir()
        {
            if (chkSimplificadoCadastroCliente.Checked == true)
            {
                inserirClienteSimplificado.Enabled = false;
                cancelarClienteSimplificado.Enabled = true;
                procuraClienteSimplificado.Enabled = false;
                salvarAlteracaoClienteSimplificado.Enabled = true;
            }
            else
            {
                inserirClienteSimplificado.Enabled = false;
                cancelarClienteSimplificado.Enabled = false;
                procuraClienteSimplificado.Enabled = true;
                salvarAlteracaoClienteSimplificado.Enabled = false;
            }
        }
        private void alteraIconesSalvar()
        {
            if (chkSimplificadoCadastroCliente.Checked == true)
            {
                cancelarClienteSimplificado.Enabled = false;
                inserirClienteSimplificado.Enabled = true;
                procuraClienteSimplificado.Enabled = true;
                salvarAlteracaoClienteSimplificado.Enabled = false;
            }
            else
            {
                inserirClienteSimplificado.Enabled = false;
                cancelarClienteSimplificado.Enabled = false;
                procuraClienteSimplificado.Enabled = true;
                salvarAlteracaoClienteSimplificado.Enabled = false;
            }
            flagSemaforo = 1;
        }
        //#########################End controle#############

        //########################Bloco de procedimento de banco##################################
        public void conector_find_adress()
        {
            auxConsistencia = 0;
            countRows = 0;
            try
            {
                banco.abreConexao();
                banco.iniciarTransacao();
                banco.startTransaction("conectorPDV_find_address");
                banco.addParametro("find_cliente", auxIdCliente);
                banco.addParametro("find_seq", "1");
                banco.procedimentoSet();
                banco.commit();
            }
            catch (Exception erro)
            {
                banco.rollback();
                MessageBox.Show(erro.Message, "Caro Usuario."); auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = banco.retornaSet().Tables[0].Columns.Count;
                    countRows = banco.retornaSet().Tables[0].DefaultView.Count;
                    if (banco.retornaSet().Tables.Count > 0)
                    {
                        if (countRows > 0)
                        {
                            auxIdEndereco = Convert.ToString(banco.retornaSet().Tables[0].Rows[0][6]);
                            auxUfEstado = Convert.ToString(banco.retornaSet().Tables[0].Rows[0][3]);
                            newGridBairroCliente = Convert.ToString(banco.retornaSet().Tables[0].Rows[0][0]);
                            newGridCityCliente = Convert.ToString(banco.retornaSet().Tables[0].Rows[0][2]);
                            newGridNumber = Convert.ToString(banco.retornaSet().Tables[0].Rows[0][4]);
                            newGridComplementoCliente = Convert.ToString(banco.retornaSet().Tables[0].Rows[0][1]);
                            newGridLogradouroCliente = Convert.ToString(banco.retornaSet().Tables[0].Rows[0][10]);
                            newGridcepCliente = Convert.ToString(banco.retornaSet().Tables[0].Rows[0][5]);
                            auxIdCepCliente = Convert.ToString(banco.retornaSet().Tables[0].Rows[0][8]);
                        }
                    }
                }
            }
            banco.fechaConexao();
            txtCodigoMunicipioCliente.Text = newGridCodMunCliente;
            txtBairroCliente.Text = newGridBairroCliente;
            txtCityCliente.Text = newGridCityCliente;
            mskNumberCliente.Text = newGridNumber;
            txtComplementoCliente.Text = newGridComplementoCliente;
            txtEnderecoCliente.Text = newGridLogradouroCliente;
            mskCepCliente.Text = newGridcepCliente;
            cmbUfCliente.Text = auxUfEstado;
        }
        public string conector_find_consultaNumberName()
        {
            texto = " select count(*) as test from fisica where nome like concat('";
            texto += txtNomeCliente.Text;
            texto += "' ,'%') ";
            try
            {
                banco.abreConexao();
                banco.iniciarTransacao();
                banco.singleTransaction(texto);
                banco.procedimentoRead();
                banco.retornaRead().Read();
                auxNome = banco.retornaRead().GetString(0);
                banco.fechaRead();
                banco.commit();
            }
            catch (Exception erro)
            {
                banco.rollback();

                MessageBox.Show(erro.Message, "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {

                banco.fechaConexao();
            }
            return auxNome;
        }
        public string conector_verifica_estado(string id, string mun)
        {
            string test = "0";
            try
            {
                banco.abreConexao();
                banco.iniciarTransacao();
                banco.singleTransaction("SELECT count(*) FROM spedmunicipio where idSpedMunicipio=?mun and idEstado=?id");
                banco.addParametro("?mun", mun);
                banco.addParametro("?id", id);
                banco.procedimentoRead();
                if (banco.retornaRead().Read() == true)
                {
                    test = banco.retornaRead().GetString(0);
                }
                else test = "0";
                banco.fechaRead();
                banco.commit();
            }
            catch (Exception erro)
            {
                banco.rollback();
                MessageBox.Show(erro.Message, "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            finally
            {
                banco.fechaConexao();
            }
            return test;
        }
        public string conector_find_consultaNumberRazao()
        {
            texto = " select count(*) as test from juridica where razao like concat('";
            texto += txtRazaoCliente.Text;
            texto += "' ,'%') ";
            try
            {
                banco.abreConexao();
                banco.iniciarTransacao();
                banco.singleTransaction(texto);
                banco.procedimentoRead();
                banco.retornaRead().Read();
                auxNome = banco.retornaRead().GetString(0);
                banco.fechaRead();
                banco.commit();
            }
            catch (Exception erro)
            {
                banco.rollback();

                MessageBox.Show(erro.Message, "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {

                banco.fechaConexao();
            }
            return auxNome;
        }
        public string conector_find_consultaNumberFone()
        {
            texto = " select ";
            texto += " count(*) as Test ";
            texto += " from ";
            texto += " cliente tab ";
            texto += " left join fone tab6 on(tab.idCliente = tab6.idCliente) ";
            texto += " left join fisica tab1 on(tab.idcliente = tab1.idcliente) ";
            texto += " left join juridica tab2 on(tab.idcliente = tab2.idcliente) ";
            texto += " left join (select rural.idcliente,rural.cpf as cpf_1,rural.nome,rural.nascimento,rural.ie,rural.idsexo,rural.identidade,rural.idcivil,sexo.descricao as sexo1,civil.descricao as civil1 from rural ";
            texto += " inner join sexo on(rural.idsexo = sexo.idsexo) ";
            texto += " inner join civil on(rural.idcivil = civil.idcivil)) as tab3 ";
            texto += " on(tab.idcliente = tab3.idcliente) ";
            texto += " where tab6.telefone = ";
            texto += newtelefone;
            try
            {
                banco.abreConexao();
                banco.iniciarTransacao();
                banco.singleTransaction(texto);
                banco.procedimentoRead();
                banco.retornaRead().Read();
                auxNome = banco.retornaRead().GetString(0);
                banco.fechaRead();
                banco.commit();
            }
            catch (Exception erro)
            {
                banco.rollback();
                MessageBox.Show(erro.Message, "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {

                banco.fechaConexao();
            }
            return auxNome;
        }

        protected void conector_alt_cliente()
        {
            auxConsistencia = 0;
            try
            {
                banco.abreConexao();
                banco.iniciarTransacao();
                banco.startTransaction("conectorPDV_alt_cliente");
                banco.addParametro("alt_idcliente", auxIdCliente);
                banco.addParametro("alt_idloja", auxIdLoja);
                banco.addParametro("alt_idtipoPessoa", Convert.ToString(flagTipopessoa));
                banco.addParametro("alt_idusuario", "1");//Provisorio
                banco.addParametro("alt_idatividade", Convert.ToString(flagAtividade));
                banco.addParametro("alt_observacao", "");
                banco.addParametro("alt_dataEmissao", String.Format("{0:yyyyMMdd}", DateTime.Now));
                banco.addParametro("alt_dataAlteracao", String.Format("{0:yyyyMMdd}", DateTime.Now));
                banco.addParametro("alt_idestado", auxIdEstado);
                banco.addParametro("alt_uf", cmbUfCliente.Text);
                banco.addParametro("alt_codMun", txtCodigoMunicipioCliente.Text);
                banco.addParametro("alt_status", "0");
                banco.addParametro("send_id", auxIdEndereco);
                banco.addParametro("send_cep", newGridcepCliente);
                banco.addParametro("send_idcepbairro", auxIdCepCliente);
                banco.addParametro("send_idenderecoType", "1");
                banco.addParametro("send_bairro", txtBairroCliente.Text);
                banco.addParametro("send_logradouro", txtEnderecoCliente.Text);
                banco.addParametro("send_complemento", txtComplementoCliente.Text);
                banco.addParametro("send_municipio", txtCityCliente.Text);
                banco.addParametro("send_estado", cmbUfCliente.Text);
                banco.addParametro("send_numero", mskNumberCliente.Text);
                banco.procedimentoRead();
                banco.fechaRead();
                banco.commit();
            }
            catch (Exception erro)
            {
                banco.rollback();
                MessageBox.Show(erro.Message, "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                auxConsistencia = 1;
                alteraIconesIncluir();
            }
            finally
            {
                banco.fechaConexao();
                //controle_objetos();
                if (auxConsistencia == 0)
                {
                    flagSemaforo = 1;
                    alteraIconesSalvar();
                }
            }
        }
        private void conector_alt_fisica()
        {
            auxConsistencia = 0;
            try
            {
                banco.abreConexao();
                banco.iniciarTransacao();
                banco.startTransaction("conectorPDV_alt_fisica");
                banco.addParametro("alt_idcliente", auxIdCliente);
                banco.addParametro("alt_cpf", newGridCpfCliente);
                banco.addParametro("alt_idatividade", Convert.ToString(flagAtividade));
                banco.addParametro("alt_nome", txtNomeCliente.Text);
                banco.addParametro("alt_nascimento", String.Format("{0:yyyyMMdd}", dtpDataNascCliente.Value));
                banco.addParametro("alt_idsexo", auxIdSexy);
                banco.addParametro("alt_identidade", txtRgCliente.Text);
                banco.addParametro("alt_idcivil", auxIdCivil);
                banco.procedimentoRead();
                banco.fechaRead();
                banco.commit();
            }
            catch (Exception erro)
            {
                banco.rollback();
                MessageBox.Show(erro.Message, "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                auxConsistencia = 1;
                alteraIconesIncluir();
            }
            finally
            {
                banco.fechaConexao();
                //controle_objetos();
                if (auxConsistencia == 0)
                {
                    flagSemaforo = 1;
                    alteraIconesSalvar();
                }
            }
        }
        private void conector_alt_juridica()
        {
            auxConsistencia = 0;
            try
            {
                banco.abreConexao();
                banco.iniciarTransacao();
                banco.startTransaction("conectorPDV_alt_juridica");
                banco.addParametro("alt_idcliente", auxIdCliente);
                banco.addParametro("alt_cnpj", newGridCnpjCliente);
                banco.addParametro("alt_idatividade", Convert.ToString(flagAtividade));
                banco.addParametro("alt_razao", txtRazaoCliente.Text);
                banco.addParametro("alt_fantasia", txtRazaoCliente.Text);
                banco.addParametro("alt_ie", txtIeCliente.Text);
                banco.addParametro("alt_dataAbertura", String.Format("{0:yyyyMMdd}", DateTime.Now));
                banco.addParametro("alt_idTipoFornecedor", Convert.ToString(flagTipoFornecedor));
                banco.procedimentoRead();
                banco.fechaRead();
                banco.commit();
            }
            catch (Exception erro)
            {
                banco.rollback();
                MessageBox.Show(erro.Message, "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                auxConsistencia = 1;
                alteraIconesIncluir();
            }
            finally
            {
                banco.fechaConexao();
                //controle_objetos();
                if (auxConsistencia == 0)
                {
                    flagSemaforo = 1;
                    alteraIconesSalvar();
                }
            }
        }
        private void conector_alt_rural()
        {
            auxConsistencia = 0;
            try
            {
                banco.abreConexao();
                banco.iniciarTransacao();
                banco.startTransaction("conectorPDV_alt_rural");
                banco.addParametro("alt_idcliente", auxIdCliente);
                banco.addParametro("alt_cpf", newGridCpfCliente);
                banco.addParametro("alt_idatividade", Convert.ToString(flagAtividade));
                banco.addParametro("alt_nome", txtNomeCliente.Text);
                banco.addParametro("alt_identidade", txtRgCliente.Text);
                banco.addParametro("alt_ie", txtIeCliente.Text);
                banco.addParametro("alt_nascimento", String.Format("{0:yyyyMMdd}", dtpDataNascCliente.Value));
                banco.addParametro("alt_idsexo", auxIdSexy);
                banco.addParametro("alt_idcivil", auxIdCivil);
                banco.procedimentoRead();
                banco.fechaRead();
                banco.commit();
            }
            catch (Exception erro)
            {
                banco.rollback();
                MessageBox.Show(erro.Message, "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                auxConsistencia = 1;
                //alteraIconesIncluir();
            }
            finally
            {
                banco.fechaConexao();
                //controle_objetos();
                if (auxConsistencia == 0)
                {
                    flagSemaforo = 1;
                    alteraIconesSalvar();
                }
            }
        }

        public void conector_inc_rural()
        {
            auxConsistencia = 0;
            try
            {
                banco.abreConexao();
                banco.iniciarTransacao();
                banco.startTransaction("conectorPDV_inc_rural");
                banco.addParametro("inc_idCliente", auxIdCliente);
                banco.addParametro("inc_cpf", newGridCpfCliente);
                banco.addParametro("inc_idatividade", Convert.ToString(flagAtividade));
                banco.addParametro("inc_nome", txtNomeCliente.Text);
                banco.addParametro("inc_ie", txtIeCliente.Text);
                banco.addParametro("inc_identidade", txtRgCliente.Text);
                banco.addParametro("inc_nascimento", String.Format("{0:yyyyMMdd}", dtpDataNascCliente.Value));
                banco.addParametro("inc_idsexo", auxIdSexy);
                banco.addParametro("inc_idcivil", auxIdCivil);
                banco.procedimentoRead();
                banco.fechaRead();
                banco.commit();
            }
            catch (Exception erro)
            {
                banco.rollback();
                MessageBox.Show("Existe cliente com esse CFP cadastrado no sitema => " + erro.Message, "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                auxConsistencia = 1;
                alteraIconesIncluir();
            }
            finally
            {
                banco.fechaConexao();
                if (auxConsistencia == 0)
                {
                    flagSemaforo = 1;
                    alteraIconesSalvar();
                }
            }
        }
        public void conector_inc_juridica()
        {
            auxConsistencia = 0;
            try
            {
                banco.abreConexao();
                banco.iniciarTransacao();
                banco.startTransaction("conectorPDV_inc_juridica");
                banco.addParametro("inc_idcliente", auxIdCliente);
                banco.addParametro("inc_cnpj", newGridCnpjCliente);
                banco.addParametro("inc_idatividade", Convert.ToString(flagAtividade));
                banco.addParametro("inc_razao", txtRazaoCliente.Text);
                banco.addParametro("inc_fantasia", txtRazaoCliente.Text);
                banco.addParametro("inc_ie", txtIeCliente.Text);
                banco.addParametro("inc_dataAtertura", String.Format("{0:yyyyMMdd}", DateTime.Now));
                banco.addParametro("inc_idTipofornecedor", "8");
                banco.procedimentoRead();
                banco.fechaRead();
                banco.commit();
            }
            catch (Exception erro)
            {
                banco.rollback();
                MessageBox.Show("Existe cliente com esse CNPJ cadastrado no sitema => " + erro.Message, "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                auxConsistencia = 1;
                alteraIconesIncluir();
            }
            finally
            {
                banco.fechaConexao();
                if (auxConsistencia == 0)
                {
                    flagSemaforo = 1;
                   alteraIconesSalvar();
                }
            }
        }
        public void conector_inc_fisica()
        {
            auxConsistencia = 0;
            try
            {
                banco.abreConexao();
                banco.iniciarTransacao();
                banco.startTransaction("conectorPDV_inc_fisica");
                banco.addParametro("inc_idCliente", auxIdCliente);
                banco.addParametro("inc_cpf", newGridCpfCliente);
                banco.addParametro("inc_idatividade", Convert.ToString(flagAtividade));
                banco.addParametro("inc_nome", txtNomeCliente.Text);
                banco.addParametro("inc_nascimento", String.Format("{0:yyyyMMdd}", dtpDataNascCliente.Value));
                banco.addParametro("inc_idsexo", auxIdSexy);
                banco.addParametro("inc_identidade", txtRgCliente.Text);
                banco.addParametro("inc_idcivil", auxIdCivil);
                banco.addParametro("inc_idTipofornecedor", "8");
                banco.procedimentoRead();
                banco.fechaRead();
                banco.commit();
            }
            catch (Exception erro)
            {
                banco.rollback();
                MessageBox.Show("Erro fatal => " + erro.Message, "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                auxConsistencia = 1;
                alteraIconesIncluir();
            }
            finally
            {
                banco.fechaConexao();
                if (auxConsistencia == 0)
                {
                    flagSemaforo = 1;
                    alteraIconesSalvar();
                }
            }
        }
        private void conector_find_pessoa(string aux)
        {
            try
            {
                auxConsistencia = 0;
                banco.abreConexao();
                banco.iniciarTransacao();
                banco.startTransaction("conectorPDV_find_cliente");
                //banco.startTransaction("conector_find_cliente_New");
                banco.addParametro("tipo", 13.ToString());
                banco.addParametro("find_cliente", aux);
                banco.addParametro("tipo_cliente", "");
                banco.addParametro("find_atividade", "0");
                banco.procedimentoRead();
                if (banco.retornaRead().Read() == true)
                {
                    auxIdCliente = banco.retornaRead().GetString(0);
                    if (Convert.ToInt16(banco.retornaRead().GetString(15)) == 2)
                    {
                        newGridRazaoCliente = banco.retornaRead().GetString(1);
                        auxGridCnpjCliente = newGridCnpjCliente = banco.retornaRead().GetString(2);
                        newGridIeCliente = banco.retornaRead().GetString(3);
                    }
                    else
                    {
                        newGridNameCliente = banco.retornaRead().GetString(1);
                        auxGridCpfCliente = newGridCpfCliente = banco.retornaRead().GetString(2);
                        newGridRgCliente = banco.retornaRead().GetString(3);
                        auxIdSexy = banco.retornaRead().GetString(5);
                        auxIdCivil = banco.retornaRead().GetString(7);
                        newGridDataNascCliente = banco.retornaRead().GetString(4);
                        dtpDataNascCliente.Value = Convert.ToDateTime(banco.retornaRead().GetString(4));
                    }
                    auxIdLoja = banco.retornaRead().GetString(9);
                    // tslLoja.Text = tslLoja.Text + ": " + auxIdLoja; 
                    newGridInativoCliente = banco.retornaRead().GetString(10);
                    newGridFantasiaCliente = banco.retornaRead().GetString(12);
                    auxUfEstado = banco.retornaRead().GetString(13);
                    flagTipoFornecedor = Convert.ToInt16(banco.retornaRead().GetString(14));
                    flagTipopessoa = Convert.ToInt16(banco.retornaRead().GetString(15));
                    flagUsuario = Convert.ToInt16(banco.retornaRead().GetString(16));
                    flagAtividade = Convert.ToInt16(banco.retornaRead().GetString(17));
                    auxIdLoja = banco.retornaRead().GetString(9);
                    auxIdEstado = banco.retornaRead().GetString(21);
                    newGridObs = banco.retornaRead().GetString(18);
                    newGridDataInclusaoCliente = banco.retornaRead().GetString(19);
                    newGridCodMunCliente = banco.retornaRead().GetString(55);
                    newGridIeCliente = banco.retornaRead().GetString(22);
                }
                banco.fechaRead();
                banco.commit();
            }
            catch (Exception erro)
            {
                banco.rollback();
                MessageBox.Show(erro.Message, "Caro cliente"); auxConsistencia = 1; }
            finally
            {
                banco.fechaConexao();
                if (auxConsistencia == 0) 
                {
                    if (newGridInativoCliente == "1")
                    {
                        MessageBox.Show("Cliente Inátivo", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        auxConsistencia = 1;
                    }
                }
            }

        }
        private void conector_find_fone_read()
        {
            try
            {
                banco.abreConexao();
                banco.iniciarTransacao();
                banco.startTransaction("conectorPDV_find_fone");
                banco.addParametro("find", auxIdCliente);
                banco.addParametro("tipo", "1");
                banco.procedimentoRead();
                if (banco.retornaRead().Read() == true)
                {
                    banco.retornaRead().Read();
                    newddd = banco.retornaRead().GetString(1);
                    newtelefone = banco.retornaRead().GetString(2);
                }
                banco.fechaRead();
                banco.commit();
            }
            catch (Exception erro)
            {
                banco.rollback();
                MessageBox.Show(erro.Message, "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information); auxConsistencia = 1;
            }
            finally
            {
                banco.fechaConexao();
                if (auxConsistencia == 0)
                {
                    if ((auxIdCliente != "") && (txtCodigoCliente.Text == ""))
                    {
                        mskDDDSimplificado.Text = newddd;
                        mskTelefoneSimplificado.Text = newtelefone;
                    }
                }
            }
        }
        public void conector_inc_cliente()
        {
            auxConsistencia = 0;
            try
            {
                banco.abreConexao();
                banco.iniciarTransacao();
                banco.startTransaction("conectorPDV_inc_cliente");
                banco.addParametro("inc_idloja", auxIdLoja);
                banco.addParametro("inc_idtipoPessoa", Convert.ToString(flagTipopessoa));
                banco.addParametro("inc_idusuario", "1");//Provisorio
                banco.addParametro("inc_idatividade", Convert.ToString(flagAtividade));
                banco.addParametro("inc_observacao", "Cadastro Simplificado - Favor revisá-lo.");
                banco.addParametro("inc_dataEmissao", String.Format("{0:yyyyMMdd}", DateTime.Now));
                banco.addParametro("inc_dataAlteracao", String.Format("{0:yyyyMMdd}", DateTime.Now));
                banco.addParametro("inc_idestado", auxIdEstado);
                banco.addParametro("inc_uf", cmbUfCliente.Text);
                banco.addParametro("inc_codMun", newGridCodMunCliente);
                banco.addParametro("inc_status", "0");
                banco.addParametro("send_cep", newGridcepCliente);
                banco.addParametro("send_idcepbairro", auxIdCepCliente == "" ? "0" : auxIdCepCliente);
                banco.addParametro("send_idenderecoType", "1");
                banco.addParametro("send_bairro", txtBairroCliente.Text);
                banco.addParametro("send_logradouro", txtEnderecoCliente.Text);
                banco.addParametro("send_complemento", txtComplementoCliente.Text);
                banco.addParametro("send_municipio", txtCityCliente.Text);
                banco.addParametro("send_estado", cmbUfCliente.Text);
                banco.addParametro("send_numero", mskNumberCliente.Text);
                banco.procedimentoRead();
                if (banco.retornaRead().Read() == true)
                {
                    auxIdCliente = banco.retornaRead().GetString(1);
                    auxIdEndereco = banco.retornaRead().GetString(0);
                }
                banco.fechaRead();
                banco.commit();
            }
            catch (Exception erro)
            {
                banco.rollback();
                MessageBox.Show(erro.Message, "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                auxConsistencia = 1;
               alteraIconesIncluir();
            }
            finally
            {
                banco.fechaConexao();
              //  controle_objetos();
                txtCodigoCliente.Text = auxIdCliente;
                if (auxConsistencia == 0)
                {
                    flagSemaforo = 1;
                    alteraIconesSalvar();
                }
            }
        }   
        public void conector_find_estado()
        {
            auxConsistencia = 0;
            cmbUfCliente.Items.Clear();
            auxConsistencia = 0;
            try
            {
                banco.abreConexao();
                banco.iniciarTransacao();
                banco.startTransaction("conectorPDV_find_estado");
                banco.addParametro("tipo", "1");
                banco.addParametro("find", "0");
                banco.procedimentoSet();
                banco.commit();
            }
            catch (Exception erro)
            {
                banco.rollback();
                MessageBox.Show(erro.Message, "Erro não identificado, entre contato como revendedor"); auxConsistencia = 1; }
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
                            cmbUfCliente.Items.Add(banco.retornaSet().Tables[0].Rows[i][2]);
                        }
                    }
                }
                banco.fechaConexao();
            }
        }
        //Pesquisa o cliente pela formação da clausula where e carrega as configurações
        private void conector_prepare_pessoa()
        {
            liberaWeb = -1;
            try
            {
                auxConsistencia = 0;
                banco.abreConexao();
                banco.iniciarTransacao();
                banco.startTransaction("conectorPDV_prepare_pessoa");
                banco.addParametro("string1", flagString == "" ? "0" : flagString);
                banco.procedimentoRead();
                if (banco.retornaRead().Read() == true)
                {
                    auxIdCliente = banco.retornaRead().GetString(0);
                    liberaWeb = 0;
                    newGridNameCliente = banco.retornaRead().GetString(1);
                    newGridRazaoCliente = banco.retornaRead().GetString(2);
                    auxGridCnpjCliente = banco.retornaRead().GetString(4);
                    auxGridCpfCliente = banco.retornaRead().GetString(5);
                    newGridRgCliente = banco.retornaRead().GetString(6);
                    newGridIeCliente = banco.retornaRead().GetString(7);
                    newGridFantasiaCliente = banco.retornaRead().GetString(8);
                    auxUfEstado = banco.retornaRead().GetString(14);
                    flagTipopessoa = Convert.ToInt16(banco.retornaRead().GetString(22));
                    if (flagTipopessoa == 1 || flagTipopessoa == 2)
                    {
                        newGridDataNascCliente = banco.retornaRead().GetString(11);
                    }
                    else
                    {
                        newGridDataNascCliente = banco.retornaRead().GetString(17);
                        newGridNameCliente = banco.retornaRead().GetString(3);
                        auxGridCpfCliente = banco.retornaRead().GetString(15);
                        newGridRgCliente = banco.retornaRead().GetString(16);
                    }
                    flagTipoFornecedor = Convert.ToInt16(banco.retornaRead().GetString(20));
                    flagUsuario = Convert.ToInt16(banco.retornaRead().GetString(23));
                    newGridObs = banco.retornaRead().GetString(25);
                    newGridCodMunCliente = banco.retornaRead().GetString(35);
                    mskDDDSimplificado.Text = banco.retornaRead().GetString(36);
                    mskTelefoneSimplificado.Text = banco.retornaRead().GetString(37);
                    //rdCodigoSimplificado.Checked = false;
                    txtCodigoCliente.ReadOnly = true;

                    if (Convert.ToInt16(flagTipopessoa) == 1)
                    {
                        rdbPessoaFisicaCliente.Checked = true;
                        rdbPessoaFisicaCliente.Enabled = true;
                        rbPessoaRuralCliente.Enabled = false;
                        rdbPessoaJuridicaCliente.Enabled = false;
                        dtpDataNascCliente.Value = Convert.ToDateTime(newGridDataNascCliente);
                    }
                    else if (Convert.ToInt16(flagTipopessoa) == 2)
                    {
                        rdbPessoaFisicaCliente.Enabled = false;
                        rbPessoaRuralCliente.Enabled = false;
                        rdbPessoaJuridicaCliente.Checked = true;
                        rdbPessoaJuridicaCliente.Enabled = true;

                    }
                    else if (Convert.ToInt16(flagTipopessoa) == 3)
                    {
                        rdbPessoaFisicaCliente.Enabled = false;
                        rbPessoaRuralCliente.Checked = true;
                        rbPessoaRuralCliente.Enabled = true;
                        rdbPessoaJuridicaCliente.Enabled = false;
                        if (newGridDataNascCliente != "")
                        {
                            dtpDataNascCliente.Value = Convert.ToDateTime(newGridDataNascCliente);

                        }
                    }
                    flagSemaforo = 1;
                    if (chkSimplificadoCadastroCliente.Checked == true)
                    {
                        inserirClienteSimplificado.Enabled = true;
                        procuraClienteSimplificado.Enabled = true;
                        salvarAlteracaoClienteSimplificado.Enabled = false;
                        cancelarClienteSimplificado.Enabled = false;
                    }
                    else
                    {
                        inserirClienteSimplificado.Enabled = false;
                        procuraClienteSimplificado.Enabled = true;
                        salvarAlteracaoClienteSimplificado.Enabled = false;
                        cancelarClienteSimplificado.Enabled = false;
                    }
                    txtCodigoCliente.Select();
                }

                else {
                    txtCodigoCliente.Clear();
                    MessageBox.Show("Não existe cliente cadastrado com essa informação.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCodigoCliente.CausesValidation = true;
                    txtComplementoCliente.Select();//reavalida
                    auxConsistencia = 1;
                }
                banco.fechaRead();
                banco.commit();
            }
            catch (Exception erro)
            {
                banco.rollback();
                MessageBox.Show(erro.Message, "Caro Usuário"); auxConsistencia = 1; }
            finally
            {
                banco.fechaConexao();
                if (auxConsistencia == 0)
                {
                    if ((verifica == "0") || (verifica == ""))
                    {
                        statusObj(false);
                    }
                    else
                    {
                        if (auxConsistencia == 0)
                        {
                            statusObj(true);
                            conector_find_adress();
                            conector_find_fone_read();
                            controleObj();
                            if (chkSimplificadoCadastroCliente.Checked == true)
                            {
                                inserirClienteSimplificado.Enabled = true;
                                procuraClienteSimplificado.Enabled = true;
                                salvarAlteracaoClienteSimplificado.Enabled = false;
                                cancelarClienteSimplificado.Enabled = false;
                            }
                            else
                            {
                                inserirClienteSimplificado.Enabled = false;
                                procuraClienteSimplificado.Enabled = true;
                                salvarAlteracaoClienteSimplificado.Enabled = false;
                                cancelarClienteSimplificado.Enabled = false;
                            }
                        }
                    }
                }
            }
            if (auxConsistencia == 1)
            {
                txtCodigoCliente.Select();
            }
            else
            {
                txtCodigoCliente.CausesValidation = false;
                mskTelefoneSimplificado.Select();
                txtCodigoCliente.CausesValidation = true;
            }
        }
        //########################End banco

        //#######################################################Procedimento de banco - WEBSERVICE#############################################################
        protected int conectorPDV_replace_cliente(string idCliente, //rever problema de data como emissao e nasc
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
                banco.abreConexao();
                banco.startTransaction("conectorPDV_replace_cliente");
                banco.addParametro("replace_idcliente", idCliente);
                banco.addParametro("replace_idloja", idLoja);
                banco.addParametro("replace_idtipoPessoa", idTipoPessoa);
                banco.addParametro("replace_idusuario", idUsuario);
                banco.addParametro("replace_idatividade", idAtividade);
                banco.addParametro("replace_observacao", observacao);
                banco.addParametro("replace_dataEmissao", String.Format("{0:yyyyMMdd}", Convert.ToDateTime(dataEmissao)));
                banco.addParametro("replace_dataAlteracao", String.Format("{0:yyyyMMdd}", Convert.ToDateTime(dataAlteracao)));
                banco.addParametro("replace_idestado", idEstado);
                banco.addParametro("replace_uf", uf);
                banco.addParametro("replace_status", status);
                banco.addParametro("replace_idSpedMunicipio", idSpedMunicipio);
                banco.addParametro("replace_idPais", idPais);
                banco.addParametro("replace_liberacao", liberacao);
                banco.procedimentoRead();
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message, "Caro Usuario."); auxConsistencia = 1; 
            }
            finally
            {
                banco.fechaConexao();
            }
            return auxConsistencia;
        }
        #region
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
                banco.abreConexao();
                banco.startTransaction("conectorPDV_replace_fisica");
                banco.addParametro("replace_idcliente", idCliente);
                banco.addParametro("replace_cpf", cpf);
                banco.addParametro("replace_idAtividade", idAtividade);
                banco.addParametro("replace_nome", nome);
                banco.addParametro("replace_nascimento", String.Format("{0:yyyyMMdd}", Convert.ToDateTime(nascimento)));
                banco.addParametro("replace_idSexo", idSexo);
                banco.addParametro("replace_idEntidade", idEntidade);
                banco.addParametro("replace_idCivil", idCivil);
                banco.procedimentoRead();
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message, "Caro Usuario."); auxConsistencia = 1; 
            }
            finally
            {
                banco.fechaConexao();
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
                banco.abreConexao();
                banco.startTransaction("conectorPDV_replace_rural");
                banco.addParametro("replace_idcliente", idCliente);
                banco.addParametro("replace_cpf", cpf);
                banco.addParametro("replace_idAtividade", idAtividade);
                banco.addParametro("replace_nome", nome);
                banco.addParametro("replace_ie", ie);
                banco.addParametro("replace_idEntidade", idEntidade);
                banco.addParametro("replace_nascimento", String.Format("{0:yyyyMMdd}", Convert.ToDateTime(nascimento)));
                banco.addParametro("replace_idSexo", idSexo);
                banco.addParametro("replace_idCivil", idCivil);
                banco.procedimentoRead();
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message, "Caro Usuario."); auxConsistencia = 1; 
            }
            finally
            {
                banco.fechaConexao();
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
                banco.abreConexao();
                banco.startTransaction("conectorPDV_replace_juridica");
                banco.addParametro("replace_idcliente", idCliente);
                banco.addParametro("replace_cnpj", cnpj);
                banco.addParametro("replace_idAtividade", idAtividade);
                banco.addParametro("replace_razao", razao);
                banco.addParametro("replace_fantasia", fantasia);
                banco.addParametro("replace_ie", ie);
                banco.addParametro("replace_dataAbertura", String.Format("{0:yyyyMMdd}", Convert.ToDateTime(dataAbertura)));
                banco.addParametro("replace_idTipoFornecedor", idTipofornecedor);
                banco.procedimentoRead();
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message, "Caro Usuario."); auxConsistencia = 1; 
            }
            finally
            {
                banco.fechaConexao();
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
                banco.abreConexao();
                banco.startTransaction("conectorPDV_replace_fone");
                banco.addParametro("replace_idFone", fone);
                banco.addParametro("replace_idcliente", idCliente);
                banco.addParametro("replace_idAtividade", idAtividade);
                banco.addParametro("replace_ddd", ddd);
                banco.addParametro("replace_telefone", telefone);
                banco.addParametro("replace_ramal", ie);
                banco.addParametro("replace_idfonetype", idTypeFone);
                banco.addParametro("replace_complemento", complemento);
                banco.addParametro("replace_priori", priori);
                banco.procedimentoRead();
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message, "Caro Usuario."); auxConsistencia = 1; 
            }
            finally
            {
                banco.fechaConexao();
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
                banco.abreConexao();
                banco.startTransaction("conectorPDV_replace_endereco");
                banco.addParametro("replace_idEndereco", idEndereco);
                banco.addParametro("replace_idcliente", idCliente);
                banco.addParametro("replace_sequencia", sequencia);
                banco.addParametro("replace_cep", cep);
                banco.addParametro("replace_idcepbairro", idcepbairro);
                banco.addParametro("replace_idEnderecoType", idEnderecoType);
                banco.addParametro("replace_bairro", bairro);
                banco.addParametro("replace_logradouro", logradouro);
                banco.addParametro("replace_complemento", complemento);
                banco.addParametro("replace_municipio", municipio);
                banco.addParametro("replace_estado", estado);
                banco.addParametro("replace_numero", numero);
                banco.procedimentoRead();
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message, "Caro Usuario."); auxConsistencia = 1; 
            }
            finally
            {
                banco.fechaConexao();
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
                banco.abreConexao();
                banco.startTransaction("conectorPDV_replace_clienteProfissional");
                banco.addParametro("replace_idClienteProfissional", clienteProfissional);
                banco.addParametro("replace_idcliente", idCliente);
                banco.addParametro("replace_idendereco", idEndereco);
                banco.addParametro("replace_empresa", empresa);
                banco.addParametro("replace_salarioDeclarado", salarioDeclarado);
                banco.addParametro("replace_salarioComprovado", salarioComprovado);
                banco.addParametro("replace_idprofissao", idprofissao);
                banco.addParametro("replace_idEscolaridade", idEscolaridade);
                banco.addParametro("replace_default1", default1);
                banco.procedimentoRead();
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message, "Caro Usuario."); auxConsistencia = 1; 
            }
            finally
            {
                banco.fechaConexao();
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
                banco.abreConexao();
                banco.startTransaction("conectorPDV_replace_clienteReferencia");
                banco.addParametro("replace_idReferencia", clienteReferencia);
                banco.addParametro("replace_idcliente", idCliente);
                banco.addParametro("replace_idtypeReferencia", idtypeReferencia);
                banco.addParametro("replace_empresaContato", empresaContato);
                banco.addParametro("replace_Contato", contato);
                banco.addParametro("replace_ddd", ddd);
                banco.addParametro("replace_fone", fone);
                banco.addParametro("replace_ramal", ramal);
                banco.addParametro("replace_data", String.Format("{0:yyyyMMdd}", Convert.ToDateTime(data)));
                banco.addParametro("replace_observacao", observacao);
                banco.procedimentoRead();
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message, "Caro Usuario."); auxConsistencia = 1; 
            }
            finally
            {
                banco.fechaConexao();
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
                banco.abreConexao();
                banco.startTransaction("conectorPDV_replace_clienteEntrega");
                banco.addParametro("replace_idClienteEntrega", clienteEntrega);
                banco.addParametro("replace_idEndereco", endereco);
                banco.addParametro("replace_idCliente", pessoa);
                banco.addParametro("replace_sequencia", seq);
                banco.addParametro("replace_default1", default1);
                banco.procedimentoRead();
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message, "Caro Usuario."); auxConsistencia = 1; 
            }
            finally
            {
                banco.fechaConexao();
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
                banco.abreConexao();
                banco.startTransaction("conectorPDV_replace_clienteCobranca");
                banco.addParametro("replace_idClienteCobranca", clienteCobranca);
                banco.addParametro("replace_idEndereco", endereco);
                banco.addParametro("replace_idCliente", pessoa);
                banco.addParametro("replace_sequencia", seq);
                banco.addParametro("replace_default1", default1);
                banco.procedimentoRead();
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message, "Caro Usuario."); auxConsistencia = 1; 
            }
            finally
            {
                banco.fechaConexao();
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
                banco.abreConexao();
                banco.startTransaction("conectorPDV_replace_clienteRisco");
                banco.addParametro("replace_idclienteRisco", risco);
                banco.addParametro("replace_idcliente", idCliente);
                banco.addParametro("replace_cooperado", cooperado);
                banco.addParametro("replace_preferencial", preferencial);
                banco.addParametro("replace_limiteCheque", limiteCheque);
                banco.addParametro("replace_onlyHourCheque", onlyHourCheque);
                banco.addParametro("replace_convenio", convenio);
                banco.addParametro("replace_pagador", pagador);
                banco.addParametro("replace_limiteEstouro", limiteEstouro);
                banco.addParametro("replace_limiteConvenio", limiteConvenio);
                banco.addParametro("replace_noteCobrancaConvenio", noteCobrancaConvenio);
                banco.addParametro("replace_typePrazo", typePrazo);
                banco.addParametro("replace_diaEncerramento", diaEncerramento);
                banco.addParametro("replace_diaFechamento", diaFechamento);
                banco.addParametro("replace_prazoDias", prazoDias);
                banco.addParametro("replace_motivo", motivo);
                banco.procedimentoRead();
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message, "Caro Usuario."); auxConsistencia = 1; 
            }
            finally
            {
                banco.fechaConexao();
            }
            return auxConsistencia;
        }
        #endregion
        //#######################################################END Procedimento de banco - WEBSERVICE#########################################################
        private void consultaCliente_Load(object sender, EventArgs e)
        {
            flagSemaforo = 1;
            flagValidation = 0;
            auxConsistencia = 0;
            statusObj(false);
            conector_find_estado();
            auxIdCivil = "9";
            auxIdSexy = "3";
            tslUser.Text = tslUser.Text + ": " + auxIdUsuario;
            tslLoja.Text = tslLoja.Text + ": " + auxIdLoja;
            tslTerminal.Text = tslTerminal.Text + ": " + auxIdTerminal;
            alteraIconesSalvar();
            rdCodigoSimplificado.Checked = true;
            txtCodigoCliente.Select();
            if (sinal == 5)
            {
                flagString = " tab.idCliente =  " + auxIdCliente;
                conector_prepare_pessoa();
                tlsMenuConsultaCliente.Enabled = false;
                statusObj(false);
                statusObjRadio(false);
                btnPesquisaCliente.Enabled = false;
                btnLimparClienteSimplificado.Enabled = false;
            }

            if (alwaysVariables.UrlWebConector != "")
            {
                WebConectorServer.Service MyConector1 = new WebConectorServer.Service(alwaysVariables.UrlWebConector);
                ataque = 0;
                string test = PingHost(MyConector1.Url);
                if (test == null) { test = "Host Unavailable"; }
                else if (MyConector1.Url.IndexOf("localhost") != -1)
                {
                    test = "Service Up";
                }

                if (test != "Host Unavailable")
                { }
                else
                {
                    msgInfo msg = new msgInfo("WEBSERVICE-CONECTOR! OFF-LINE NOVOS CADASTROS NÃO SERÃO ACEITO PELO SERVIDOR...! RECONECTE O SISTEMA AO WEBSERVICE...!"); msg.ShowDialog();
                    this.Dispose();
                    this.DialogResult = DialogResult.Cancel;
                }
                MyConector1.Dispose();
            }
        }

        private void rdCPFSimplificado_CheckedChanged(object sender, EventArgs e)
        {
            flagTest = 2;
            zera_variavel();
            clearObj();
            flagTipopessoa = 1;
            statusObj(false);
            mskCpfCliente.Enabled = true;
            txtCodigoCliente.ReadOnly = true;
            alteraIconesSalvar();
            mskCpfCliente.Select();
        }

        private void rdCNPJSimplificado_CheckedChanged(object sender, EventArgs e)
        {
            flagTest = 3;
            zera_variavel();
            clearObj();
            statusObj(false);
            mskCnpjCliente.Enabled = true;
            txtCodigoCliente.ReadOnly = true;
            flagTipopessoa = 2;
            alteraIconesSalvar();
            mskCnpjCliente.Select();
        }

        private void rdNomeRazaoSimplificado_CheckedChanged(object sender, EventArgs e)
        {
            clearObj();
            zera_variavel();
            statusObj(false);
            txtNomeCliente.Enabled = true;
            txtCodigoCliente.ReadOnly = true;
            txtNomeCliente.Select();
        }

        private void rdIdentidadeSimplificado_CheckedChanged(object sender, EventArgs e)
        {
            rdbPessoaFisicaCliente.Checked = false;
            rdbPessoaJuridicaCliente.Checked = false;
            rbPessoaRuralCliente.Checked = false;
            flagTest = 6;
            flagTipopessoa = 1;
            zera_variavel();
            clearObj();
            statusObj(false);
            txtRgCliente.Enabled = true;
            txtCodigoCliente.ReadOnly = true;
            alteraIconesSalvar();
            txtRgCliente.Select();
        }

        private void txtCodigoCliente_TextChanged(object sender, EventArgs e)
        {
            if (txtCodigoCliente.Text == "")
            {
                flagTest = 1;
                zera_variavel();
                clearObj();
                statusObj(false);
                txtCodigoCliente.Enabled = true;
                txtCodigoCliente.ReadOnly = false;
                alteraIconesSalvar();
                txtCodigoCliente.Select();
                flagString = "tab.idCliente = 0";
            }
            else
            {
                flagString = "tab.idCliente = " + txtCodigoCliente.Text;
            }
        }

        private void inserirClienteSimplificado_Click(object sender, EventArgs e)
        {
            reabilitaRadio();
            insertClient();
            chkSimplificadoCadastroCliente.Checked = false;
        }

        private void rdCodigoSimplificado_CheckedChanged(object sender, EventArgs e)
        {
            flagTest = 1;
            zera_variavel();
            clearObj();
            statusObj(false);
            txtCodigoCliente.Enabled = true;
            txtCodigoCliente.ReadOnly = false;
            alteraIconesSalvar();
            txtCodigoCliente.Select();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            
        }

        private void btnPesquisaSimplificada_Click(object sender, EventArgs e)
        {
            zera_variavel();
            txtCodigoCliente.CausesValidation = false;
            conector_find_type_cliente();
            txtCodigoCliente.CausesValidation = true;
            btnCarregarClienteSimplificado.Select();

        }

        private void cmbUfCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbUfCliente.Text != "")
            {
                auxConsistencia = 0;
                try
                {
                    banco.abreConexao();
                    banco.iniciarTransacao();
                    banco.startTransaction("conectorPDV_find_estado");
                    banco.addParametro("tipo", "2");
                    banco.addParametro("find", cmbUfCliente.Text);
                    banco.procedimentoSet();
                    banco.commit();
                }
                catch (Exception erro)
                {
                    banco.rollback();
                    MessageBox.Show(erro.Message, "Erro não identificado, entre contato como revendedor"); auxConsistencia = 1; }
                finally
                {
                    if (auxConsistencia == 0)
                    {
                        countRows = banco.retornaSet().Tables[0].DefaultView.Count;
                        if (countRows > 0)
                        {

                            auxIdEstado = Convert.ToString(banco.retornaSet().Tables[0].Rows[0][0]);
                            auxUfEstado = Convert.ToString(banco.retornaSet().Tables[0].Rows[0][2]);
                        }
                    }
                    banco.fechaConexao();
                }
            }//end if
            alteraIconesIncluir();
        }

        private void btnPesquisaCliente_Click(object sender, EventArgs e)
        {
            if (chkSimplificadoCadastroCliente.Checked == true)
            {
                newGridcepCliente = mskCepCliente.Text.Replace(".", "");
                newGridcepCliente = newGridcepCliente.Replace("-", "");
                newGridcepCliente = newGridcepCliente.Replace("/", "");
                newGridcepCliente = newGridcepCliente.Replace(",", "");
                if ((newGridcepCliente != "     ") && (newGridcepCliente.Trim().Length == 8))
                {
                    cep dadoscep = new cep(newGridcepCliente);
                    if (dadoscep.ShowDialog(this) == DialogResult.OK)
                    {

                        auxUfEstado = dadoscep.uf;
                        newGridComplementoCliente = dadoscep.complemento;
                        newGridLogradouroCliente = dadoscep.logradouro;
                        newGridBairroCliente = dadoscep.bairro;
                        newGridCityCliente = dadoscep.city;
                        newGridCodMunCliente = dadoscep.CodigoMunicipio;
                        auxIdCepCliente = dadoscep.auxIdCep;
                        txtBairroCliente.Text = newGridBairroCliente;
                        txtEnderecoCliente.Text = newGridLogradouroCliente;
                        cmbUfCliente.Text = auxUfEstado;
                        txtComplementoCliente.Text = newGridComplementoCliente;
                        txtCodigoMunicipioCliente.Text = newGridCodMunCliente;
                        txtCityCliente.Text = newGridCityCliente;
                        mskNumberCliente.Select();

                    }
                }
                else { MessageBox.Show("Preencha com um CEP valido, para consulta nos correios.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            }
            else { MessageBox.Show("Situação do modulo, somente consulta.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void btnLocalizaCodMunicipio_Click(object sender, EventArgs e)
        {
            if (chkSimplificadoCadastroCliente.Checked == true)
            {
                pequisaMuncipio findMunicipio = new pequisaMuncipio("spedMunicipio");
                if (findMunicipio.ShowDialog(this) == DialogResult.OK)
                {

                    newGridCodMunCliente = findMunicipio.GridCodigoMunicipio;
                    newGridCityCliente = findMunicipio.GridCity;

                    txtCodigoMunicipioCliente.Text = newGridCodMunCliente;
                    txtCityCliente.Text = newGridCityCliente;

                }
            }
            else { MessageBox.Show("Situação do modulo, somente consulta.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information); }

            txtCodigoMunicipioCliente.Select();
        }

        private void rdTelefoneSimplificado_CheckedChanged(object sender, EventArgs e)
        {
            flagTest = 5;
            zera_variavel();
            clearObj();
            statusObj(false);
            mskTelefoneSimplificado.Enabled = true;
            txtCodigoCliente.ReadOnly = true;
            newtelefone = "";
            alteraIconesSalvar();
            mskTelefoneSimplificado.Select();
        }

        private void rdbPessoaFisicaCliente_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbPessoaFisicaCliente.Checked == true)
            {
                txtCodigoCliente.ReadOnly = true;
                flagTipopessoa = 1;
                flagString = " tab1.identidade =  " + txtRgCliente.Text + " and tab.idtipoPessoa=1";
                txtRazaoCliente.Clear();
                txtRazaoCliente.ReadOnly = true;
                mskCnpjCliente.Clear();
                mskCnpjCliente.ReadOnly = true;
                txtIeCliente.ReadOnly = true;
                txtNomeCliente.ReadOnly = false;
                mskCpfCliente.ReadOnly = false;
                txtRgCliente.ReadOnly = false;
                txtNomeCliente.Select();
            }
            if (flagSemaforo != 0)
            {
                //alteraIconesIncluir();
            }
        }

        private void rdbPessoaJuridicaCliente_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbPessoaJuridicaCliente.Checked == true)
            {
                txtCodigoCliente.ReadOnly = true;
                flagTipopessoa = 2;
                txtRazaoCliente.ReadOnly = false;
                mskCnpjCliente.ReadOnly = false;
                txtIeCliente.ReadOnly = false;
                txtNomeCliente.Clear();
                txtNomeCliente.ReadOnly = true;
                txtRgCliente.Clear();
                mskCpfCliente.Clear();
                mskCpfCliente.ReadOnly = true;
                txtRgCliente.ReadOnly = true;
                txtRazaoCliente.Select();
            }
            if (flagSemaforo != 0)
            {
                //alteraIconesIncluir();
            }
        }

        private void rbPessoaRuralCliente_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPessoaRuralCliente.Checked == true)
            {
                txtCodigoCliente.ReadOnly = true;
                flagTipopessoa = 3;
                if (rdIdentidadeSimplificado.Checked == true)
                {
                    flagString = " tab3.identidade =  " + txtRgCliente.Text + " and tab.idtipoPessoa=3";   
                }
                if (rdNomeSimplificado.Checked == true)
                {
                    flagString = "tab3.nome like concat('" + txtNomeCliente.Text + "','%') ";
                }
                txtRazaoCliente.Clear();
                txtRazaoCliente.ReadOnly = true;
                mskCnpjCliente.Clear();
                mskCnpjCliente.ReadOnly = true;
                txtIeCliente.Clear();
                txtIeCliente.ReadOnly = false;
                txtNomeCliente.ReadOnly = false;
                mskCpfCliente.ReadOnly = false;
                txtRgCliente.ReadOnly = false;
                txtNomeCliente.Select();
            }
            if (flagSemaforo != 0)
            {
             //   alteraIconesIncluir();
            }
        }

        private void chkSimplificado_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSimplificadoCadastroCliente.CausesValidation == true)
            {
                if (chkSimplificadoCadastroCliente.Checked == true)
                {
                    if ((txtCodigoCliente.Text != "" && txtNomeCliente.Text != "") || (txtCodigoCliente.Text != "" && txtRazaoCliente.Text != ""))
                    {
                        alteraIconesSalvar();
                        clearObj();
                        statusObj(true);
                        flagSemaforo = 0;
                        zera_variavel();
                    }
                    else
                    {
                        insertClient();
                        flagSemaforo = 0;
                        statusObjRadio(false);
                        clearObj();
                    }
                }
                else
                {
                    clearObj();
                    statusObj(false);
                    txtCodigoCliente.ReadOnly = true;
                    statusObjRadio(true);
                }
                txtCodigoCliente.ReadOnly = true;
                reabilitaRadio();
            }
        }

        private void btnCancelarClienteSimplificado_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLimparClienteSimplificado_Click(object sender, EventArgs e)
        {
            mskTelefoneSimplificado.CausesValidation = false;
            flagSemaforo = 1;
            flagValidation = 0;
            auxConsistencia = 0;
            statusObj(false);
            zera_variavel();
            clearObj();
            auxIdCivil = "9";
            auxIdSexy = "3";
            alteraIconesSalvar();
            rdCodigoSimplificado.Checked = true;
            txtCodigoCliente.Select();
            if (sinal == 5)
            {
                flagString = " tab.idCliente =  " + auxIdCliente;
                conector_prepare_pessoa();
                tlsMenuConsultaCliente.Enabled = false;
                statusObj(false);
                statusObjRadio(false);
                btnPesquisaCliente.Enabled = false;
                btnLimparClienteSimplificado.Enabled = false;
            }
            switch (flagTest)
            {
                case 1:
                    txtCodigoCliente.Select();
                    txtCodigoCliente.Enabled = true;
                    txtCodigoCliente.ReadOnly = false;
                    break;
                case 2:
                    mskCpfCliente.Select();
                    mskCpfCliente.Enabled = true;
                    mskCpfCliente.ReadOnly = false;
                    break;
                case 3:
                    mskCnpjCliente.Select();
                    mskCnpjCliente.Enabled = true;
                    mskCnpjCliente.ReadOnly = false;
                    break;
                case 4:
                    txtNomeCliente.Select();
                    txtNomeCliente.Enabled = true;
                    txtNomeCliente.ReadOnly = false;
                    break;
                case 5:
                    mskTelefoneSimplificado.Select();
                    mskTelefoneSimplificado.Enabled = true;
                    mskTelefoneSimplificado.ReadOnly = false;
                    break;
                case 6:
                    txtRgCliente.Select();
                    txtRgCliente.Enabled = true;
                    txtRgCliente.ReadOnly = false;
                    break;
                case 7:
                    txtRazaoCliente.Select();
                    txtRazaoCliente.Enabled = true;
                    txtRazaoCliente.ReadOnly = false;
                    break;
                default:
                    break;
            }
            mskTelefoneSimplificado.CausesValidation = true;
        }

        private void salvarAlteracaoClienteSimplificado_Click(object sender, EventArgs e)
        {
            string test;
            if (txtCodigoMunicipioCliente.Text != "")
            {
                if (cmbUfCliente.Text != "")
                {
                    test = conector_verifica_estado(auxIdEstado, newGridCodMunCliente);
                    if (test == "1")
                    {
                        saveClient();
                    }
                    else
                    {
                        MessageBox.Show("Cidade não faz parte do estado favor alterar de acordo com a tabela de cadastro de municipios!", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    
                }
                else
                {
                    MessageBox.Show("Impossível salvar um cadastro sem a escolha de um estado!", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("Cidade não preenchida, favor preenchê-la.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        private void mskCpfCliente_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            flagValidation = 1;
            alteraIconesIncluir();
        }

        private void mskCpfCliente_TextChanged(object sender, EventArgs e)
        {
            flagValidation = 1;
            alteraIconesIncluir();
            newGridCpfCliente = mskCpfCliente.Text;
            newGridCpfCliente = mskCpfCliente.Text.Replace(".", "");
            newGridCpfCliente = newGridCpfCliente.Replace("-", "");
            newGridCpfCliente = newGridCpfCliente.Replace("/", "");
            newGridCpfCliente = newGridCpfCliente.Replace(",", "");
            flagString = " tab1.cpf = " + newGridCpfCliente;
        }

        private void mskCnpjCliente_TextChanged(object sender, EventArgs e)
        {
            flagValidation = 1;
            alteraIconesIncluir();
            newGridCnpjCliente = mskCnpjCliente.Text;
            newGridCnpjCliente = mskCnpjCliente.Text.Replace(".", "");
            newGridCnpjCliente = newGridCnpjCliente.Replace("-", "");
            newGridCnpjCliente = newGridCnpjCliente.Replace("/", "");
            newGridCnpjCliente = newGridCnpjCliente.Replace(",", "");
            flagString = " tab2.cnpj =  " + newGridCnpjCliente;
        }

        private void mskCnpjCliente_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            flagValidation = 1;
        }

        private void txtIeCliente_TextChanged(object sender, EventArgs e)
        {
            alteraIconesIncluir();
        }

        private void txtRgCliente_TextChanged(object sender, EventArgs e)
        {
            alteraIconesIncluir();
            if (flagTipopessoa == 1)
            {
                flagString = " tab1.identidade =  " + txtRgCliente.Text + " and tab.idtipoPessoa=1";
            }
            else { flagString = " tab3.identidade =  " + txtRgCliente.Text + " and tab.idtipoPessoa=3";
            }
        }

        private void rdNomeSimplificado_CheckedChanged(object sender, EventArgs e)
        {
            flagTest = 4;
            zera_variavel();
            clearObj();
            statusObj(false);
            txtNomeCliente.Enabled = true;
            txtCodigoCliente.ReadOnly = true;
            alteraIconesSalvar();
            txtNomeCliente.Select();
        }

        private void rdRazaoSimplificado_CheckedChanged(object sender, EventArgs e)
        {
            flagTest = 7;
            clearObj();
            zera_variavel();
            statusObj(false);
            txtRazaoCliente.Enabled = true;
            txtCodigoCliente.ReadOnly = true;
            alteraIconesSalvar();
            txtRazaoCliente.Select();
        }

        private void txtNomeCliente_TextChanged(object sender, EventArgs e)
        {
            flagString = " tab1.nome like concat('" + txtNomeCliente.Text + "','%') ";
            alteraIconesIncluir();
        }

        private void mskTelefoneSimplificado_TextChanged(object sender, EventArgs e)
        {
            newtelefone = mskTelefoneSimplificado.Text;
            newtelefone = mskTelefoneSimplificado.Text.Replace(".", "");
            newtelefone = newtelefone.Replace("-", "");
            newtelefone = newtelefone.Replace("/", "");
            newtelefone = newtelefone.Replace(",", "");
            flagString = "  tab1.idcliente in (select tab.idCliente from cliente tab left join fone tab6 on(tab.idCliente = tab6.idCliente) where tab6.telefone=" + newtelefone + ")";
            
        }

        private void mskDDDSimplificado_TextChanged(object sender, EventArgs e)
        {
            newddd = mskDDDSimplificado.Text;
            newddd = mskDDDSimplificado.Text.Replace(".", "");
            newddd = newddd.Replace("-", "");
            newddd = newddd.Replace(",", "");
            newddd = newddd.Replace("/", "");
            newddd = newddd.Replace("(", "");
            newddd = newddd.Replace(")", "");
        }

        private void txtRazaoCliente_TextChanged(object sender, EventArgs e)
        {
            flagString = "tab2.razao like concat('" + txtRazaoCliente.Text + "','%') ";
            alteraIconesIncluir();
        }

        private void pesquisarClienteSimplificado_Click(object sender, EventArgs e)
        {
            chkSimplificadoCadastroCliente.Checked = false;
            pesquisaCliente = new pesquisaPessoa();
            if (pesquisaCliente.ShowDialog(this) == DialogResult.OK)
            {
                auxIdCliente = pesquisaCliente.Gridchave;
                conector_find_pessoa(auxIdCliente);
                if (auxConsistencia == 0)
                {
                    auxConsistencia = 0;
                    try
                    {
                        banco.abreConexao();
                        banco.iniciarTransacao();
                        banco.startTransaction("conectorPDV_find_address");
                        banco.addParametro("find_cliente", auxIdCliente);
                        banco.addParametro("find_seq", "1");
                        banco.procedimentoSet();
                        banco.commit();
                    }
                    catch (Exception erro)
                    {
                        banco.rollback(); 
                        MessageBox.Show(erro.Message, "Caro Usuario."); auxConsistencia = 1; }
                    finally
                    {
                        if (auxConsistencia == 0)
                        {
                            countRows = banco.retornaSet().Tables[0].DefaultView.Count;
                            if (countRows > 0)
                            {
                                auxUfEstado = Convert.ToString(banco.retornaSet().Tables[0].Rows[0][3]);
                                newGridBairroCliente = Convert.ToString(banco.retornaSet().Tables[0].Rows[0][0]);
                                newGridCityCliente = Convert.ToString(banco.retornaSet().Tables[0].Rows[0][2]);
                                newGridNumber = Convert.ToString(banco.retornaSet().Tables[0].Rows[0][4]);
                                newGridComplementoCliente = Convert.ToString(banco.retornaSet().Tables[0].Rows[0][1]);
                                newGridLogradouroCliente = Convert.ToString(banco.retornaSet().Tables[0].Rows[0][10]);
                                newGridcepCliente = Convert.ToString(banco.retornaSet().Tables[0].Rows[0][5]);
                            }
                        }

                    }
                    banco.fechaConexao();
                    if (auxConsistencia == 0)
                    {
                        txtCodigoMunicipioCliente.Text = newGridCodMunCliente;
                        txtBairroCliente.Text = newGridBairroCliente;
                        txtCityCliente.Text = newGridCityCliente;
                        mskNumberCliente.Text = newGridNumber;
                        txtComplementoCliente.Text = newGridComplementoCliente;
                        txtEnderecoCliente.Text = newGridLogradouroCliente;
                        mskCepCliente.Text = newGridcepCliente;
                        cmbUfCliente.Text = auxUfEstado;
                        txtCodigoCliente.Text = auxIdCliente;
                        txtCodigoMunicipioCliente.Text = newGridCodMunCliente;
                        txtNomeCliente.Text = newGridNameCliente;
                        txtRazaoCliente.Text = newGridRazaoCliente;
                        mskCpfCliente.Text = auxGridCpfCliente;
                        txtRgCliente.Text = newGridRgCliente;
                        mskCnpjCliente.Text = auxGridCnpjCliente;
                        txtIeCliente.Text = newGridIeCliente;

                        if (txtCodigoCliente.Text != "")
                        {
                            statusObj(true);
                        }
                        else
                        {
                            statusObj(false);
                            clearObj();
                        }
                        if (Convert.ToInt16(flagTipopessoa) == 1)
                        {
                            rdbPessoaFisicaCliente.Checked = true;
                            rdbPessoaFisicaCliente.Enabled = true;
                            rbPessoaRuralCliente.Enabled = false;
                            rdbPessoaJuridicaCliente.Enabled = false;

                        }
                        else if (Convert.ToInt16(flagTipopessoa) == 2)
                        {
                            rdbPessoaFisicaCliente.Enabled = false;
                            rbPessoaRuralCliente.Enabled = false;
                            rdbPessoaJuridicaCliente.Checked = true;
                            rdbPessoaJuridicaCliente.Enabled = true;

                        }
                        else if (Convert.ToInt16(flagTipopessoa) == 3)
                        {
                            rdbPessoaFisicaCliente.Enabled = false;
                            rbPessoaRuralCliente.Checked = true;
                            rbPessoaRuralCliente.Enabled = true;
                            rdbPessoaJuridicaCliente.Enabled = false;
                        }
                        flagSemaforo = 1;
                        if (chkSimplificadoCadastroCliente.Checked == true)
                        {
                            inserirClienteSimplificado.Enabled = true;
                            procuraClienteSimplificado.Enabled = true;
                            salvarAlteracaoClienteSimplificado.Enabled = false;
                            cancelarClienteSimplificado.Enabled = false;
                        }
                        else
                        {
                            inserirClienteSimplificado.Enabled = false;
                            procuraClienteSimplificado.Enabled = true;
                            salvarAlteracaoClienteSimplificado.Enabled = false;
                            cancelarClienteSimplificado.Enabled = false;
                        }
                    }
                }
            }
        }

        private void btnConsultaFoneSimplicado_Click(object sender, EventArgs e)
        {
            if (rdTelefoneSimplificado.Checked == true && flagTest == 5)
            {
                if (newtelefone.Trim().Length == 8)
                {
                    openFone = new consultaFone(newtelefone);
                    
                    if (openFone.ShowDialog(this) == DialogResult.OK)
                    {
                        auxIdCliente = openFone.GridCodigo;
                        flagString = " tab.idCliente =  " + auxIdCliente;
                        mskTelefoneSimplificado.CausesValidation = false;
                        conector_prepare_pessoa();
                        mskTelefoneSimplificado.CausesValidation = true;
                    }
                }
                else { MessageBox.Show("Telefone inválido.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information); }

            }
            else { MessageBox.Show("Selecione a opção de consulta 'TELEFONE'.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            
        }

        private void mskTelefoneSimplificado_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            alteraIconesIncluir();
        }

        private void txtCodigoCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            posSeparator = txtCodigoCliente.Text.IndexOf(".");
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

        private void telefoneClienteSimplificado_Click(object sender, EventArgs e)
        {
            string auxNome = "";
            if (txtCodigoCliente.Text != "")
            {
                if (txtNomeCliente.Text != "")
                {
                    auxNome = txtNomeCliente.Text;
                }
                else if (txtRazaoCliente.Text != "")
                {
                    auxNome = txtRazaoCliente.Text;
                }
                telefoneCliente = new telefone(auxIdCliente, auxNome, Convert.ToString(flagAtividade));
                if (telefoneCliente.ShowDialog(this) == DialogResult.OK)
                {
                }
            }
            else { MessageBox.Show("É necessario carregar ou inserir um novo cliente.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void cancelarClienteSimplificado_Click(object sender, EventArgs e)
        {
            chkSimplificadoCadastroCliente.Checked = false;
            if ((txtCodigoCliente.Text != "") && (cmbUfCliente.Text != ""))
            {
                statusObj(true);
            }
            else
            {
                statusObj(false);
                zera_variavel();
                cmbUfCliente.SelectedIndex = -1;
                auxCpfCnpj = "";
                auxIdEndereco = "";
                auxIdCliente = "";
                auxIdLoja = "";
                auxIdEstado = "";
                auxUfEstado = "";
                auxIdSexy = "";
                auxIdCivil = "";
                auxIdCepCliente = "";
            }

            if ((flagTipopessoa == 1) && (txtCodigoCliente.Text != ""))
            {
                rdbPessoaFisicaCliente.Enabled = true;
                rbPessoaRuralCliente.Enabled = false;
                rdbPessoaJuridicaCliente.Enabled = false;
            }
            else if ((flagTipopessoa == 2) && (txtCodigoCliente.Text != ""))
            {
                rdbPessoaFisicaCliente.Enabled = false;
                rbPessoaRuralCliente.Enabled = false;
                rdbPessoaJuridicaCliente.Enabled = true;
            }
            else if ((flagTipopessoa == 3) && (txtCodigoCliente.Text != ""))
            {
                rdbPessoaFisicaCliente.Enabled = false;
                rbPessoaRuralCliente.Enabled = true;
                rdbPessoaJuridicaCliente.Enabled = false;
            }
            else if (txtCodigoCliente.Text == "")
            {
                rdbPessoaFisicaCliente.Enabled = true;
                rbPessoaRuralCliente.Enabled = true;
                rdbPessoaJuridicaCliente.Enabled = true;
                rdbPessoaFisicaCliente.Checked = false;
                rbPessoaRuralCliente.Checked = false;
                rdbPessoaJuridicaCliente.Checked = false;
            }
            alteraIconesCancelar();
        }

        private void chkSimplificado_Click(object sender, EventArgs e)
        {
            if ((rdbPessoaFisicaCliente.Checked == false) && (rdbPessoaJuridicaCliente.Checked == false) && (rbPessoaRuralCliente.Checked == false))
            {
                clearObj();
                statusObj(false);
                txtCodigoCliente.ReadOnly = true;
                statusObjRadio(true);
                chkSimplificadoCadastroCliente.Checked = false;
                alteraIconesSalvar();
                if (rdCodigoSimplificado.Checked == true)
                {
                    flagTest = 1;
                    zera_variavel();
                    clearObj();
                    statusObj(false);
                    txtCodigoCliente.Enabled = true;
                    txtCodigoCliente.ReadOnly = false;
                    alteraIconesSalvar();
                    txtCodigoCliente.Select();
                }
            }
        }

        private void mskCepCliente_TextChanged(object sender, EventArgs e)
        {
            alteraIconesIncluir();
        }

        private void txtEnderecoCliente_TextChanged(object sender, EventArgs e)
        {
            alteraIconesIncluir();
        }

        private void txtBairroCliente_TextChanged(object sender, EventArgs e)
        {
            alteraIconesIncluir();
        }

        private void txtCityCliente_TextChanged(object sender, EventArgs e)
        {
            alteraIconesIncluir();
        }

        private void txtComplementoCliente_TextChanged(object sender, EventArgs e)
        {
            alteraIconesIncluir();
        }

        private void mskCnpjCliente_Validated(object sender, EventArgs e)
        {
            if (flagValidation == 1)
            {
                if (flagTipopessoa == 2)
                {
                    newGridCnpjCliente = mskCnpjCliente.Text;
                    newGridCnpjCliente = mskCnpjCliente.Text.Replace(".", "");
                    newGridCnpjCliente = newGridCnpjCliente.Replace("-", "");
                    newGridCnpjCliente = newGridCnpjCliente.Replace("/", "");
                    newGridCnpjCliente = newGridCnpjCliente.Replace(",", "");
                    if (newGridCnpjCliente.Length != 14)
                    {
                        MessageBox.Show("CNPJ incorreto, preencha o campo corretamente.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        auxCpfCnpj = cpf_cnpj.calculo_cgc(newGridCnpjCliente);
                        if (auxCpfCnpj == "0")
                        {
                            MessageBox.Show("CNPJ incorreto, preencha o campo corretamente.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //mskCnpjCliente.SelectAll();
                            //mskCnpjCliente.Focus();
                        }
                    }
                }
            } flagValidation = 0;
        }

        private void mskCpfCliente_Validated(object sender, EventArgs e)
        {
            if (flagValidation == 1)
            {
                if ((flagTipopessoa == 1) || (flagTipopessoa == 3))
                {
                    newGridCpfCliente = mskCpfCliente.Text;
                    newGridCpfCliente = mskCpfCliente.Text.Replace(".", "");
                    newGridCpfCliente = newGridCpfCliente.Replace("-", "");
                    newGridCpfCliente = newGridCpfCliente.Replace("/", "");
                    newGridCpfCliente = newGridCpfCliente.Replace(",", "");
                    if (newGridCpfCliente.Length != 11)
                    {
                        MessageBox.Show("CPF incorreto, preencha o campo corretamente.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        auxCpfCnpj = cpf_cnpj.calculo_cpf(newGridCpfCliente);
                        if (auxCpfCnpj == "0")
                        {
                            MessageBox.Show("CPF incorreto, preencha o campo corretamente.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                    }
                }
            } flagValidation = 0;
        }

        private void btnCarregarClienteSimplificado_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }

        private void btnSalvarClienteSimplificado_Click(object sender, EventArgs e)
        {
            saveClient();
        }

        private void consultaCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                ProcessTabKey(true);
            }
            else if (e.KeyCode == Keys.F10)
            {
                /*
                if (txtNomeCliente.Text != "" || txtRazaoCliente.Text != "")
                {
                    if ((auxIdCliente != "" && auxIdCliente != "0" && auxIdCliente != null) && txtCodigoCliente.Text != "")
                    {
                        historicoCompra historicos = new historicoCompra(auxIdCliente, txtNomeCliente.Text == "" ? txtRazaoCliente.Text : txtNomeCliente.Text, 1);
                        historicos.Show();
                    }
                    else
                    {
                        MessageBox.Show("Selecione um cliente para consulta.", "Caro Cliente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Selecione um cliente para consulta.", "Caro Cliente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }*/
                msgInfo msg = new msgInfo(1, "Caro Cliente - FUNÇÃO INATIVA ANALISE FEITA PELA RETAGUARDA"); msg.ShowDialog();
            }
        }

        private void txtCodigoCliente_Validated(object sender, EventArgs e)
        {
            if (txtCodigoCliente.Text != "" && Convert.ToDouble(txtCodigoCliente.Text) > 0)
            {
                conector_find_type_cliente();
                if (auxConsistencia == 0)
                {
                    btnCarregarClienteSimplificado.Select();
                }
            }
        }

        private void btnHistoricoClienteComprasSimplificado_Click(object sender, EventArgs e)
        {
            msgInfo msg = new msgInfo(1, "Caro Cliente - FUNÇÃO INATIVA ANALISE FEITA PELA RETAGUARDA"); msg.ShowDialog();
            /*
            if (txtNomeCliente.Text != "" || txtRazaoCliente.Text != "")
            {
                if ((auxIdCliente != "" && auxIdCliente != "0" && auxIdCliente != null) && txtCodigoCliente.Text != "")
                {
                    historicoCompra historicos = new historicoCompra(auxIdCliente, txtNomeCliente.Text  == "" ? txtRazaoCliente.Text : txtNomeCliente.Text, 1);
                    historicos.Show();
                }
                else
                {
                    MessageBox.Show("Selecione um cliente para consulta.", "Caro Cliente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Selecione um cliente para consulta.", "Caro Cliente", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
             */
        }

        private void btnResumoClienteSimplificado_Click(object sender, EventArgs e)
        {
            msgInfo msg = new msgInfo(1,"Caro Cliente - FUNÇÃO INATIVA ANALISE FEITA PELA RETAGUARDA"); msg.ShowDialog();
            /*if (txtNomeCliente.Text != "" || txtRazaoCliente.Text != "")
            {
                if ((auxIdCliente != "" && auxIdCliente != "0" && auxIdCliente != null) && txtCodigoCliente.Text != "")
                {
                    historicoCompra historicos = new historicoCompra(auxIdCliente, txtNomeCliente.Text == "" ? txtRazaoCliente.Text : txtNomeCliente.Text, 1);
                    historicos.Show();
                }
                else
                {
                    MessageBox.Show("Selecione um cliente para consulta.", "Caro Cliente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Selecione um cliente para consulta.", "Caro Cliente", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }*/
        }

        private void mskNumberCliente_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            alteraIconesIncluir();
        }

        private void btnSPCClienteSimplificado_Click(object sender, EventArgs e)
        {
            msgInfo msg = new msgInfo(1, "Caro Cliente - FUNÇÃO INATIVA ANALISE FEITA PELA RETAGUARDA"); msg.ShowDialog();
            /*if (txtCodigoCliente.Text != "")
            {
                newGridCpfCliente = mskCpfCliente.Text;
                newGridCpfCliente = newGridCpfCliente.Replace(".", "");
                newGridCpfCliente = newGridCpfCliente.Replace(",", "");
                newGridCpfCliente = newGridCpfCliente.Replace("-", "");
                newGridCpfCliente = newGridCpfCliente.Replace("/", "");
                consultaSpcSerasa spcserasa = new consultaSpcSerasa(Convert.ToInt16(flagTipopessoa), auxIdCliente, newGridCpfCliente, txtNomeCliente.Text);
                spcserasa.Show();
            }
            else
            {
                MessageBox.Show("Cliente inválido, ou não existe no cadastro! ", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);                
                txtCodigoCliente.Select();
            }*/
        }

        private void btnReferenciaClienteSimplificado_Click(object sender, EventArgs e)
        {
            if (Convert.ToDouble(auxIdCliente) > 0)
            {
                //consultaReferencia referencia = new consultaReferencia(auxIdCliente);
                //referencia.Show();   
            }
        }

        private void btnConvenioClienteSimplificado_Click(object sender, EventArgs e)
        {
            if (Convert.ToDouble(auxIdCliente) > 0)
            {
                msgInfo msg = new msgInfo(1, "Caro Cliente - FUNÇÃO INATIVA ANALISE FEITA PELA RETAGUARDA"); msg.ShowDialog();
                //consultaConvenio fiado01 = new consultaConvenio(auxIdCliente);
                //fiado01.Show();
            }
        }

        private void btnConsultaWebCliente_Click(object sender, EventArgs e)
        {

        }

        private void btnConsultaWebJuridicaCliente_Click(object sender, EventArgs e)
        {
            if (rdCNPJSimplificado.Checked == true)
            {
                if (flagTipopessoa == 2)
                {
                    newGridCnpjCliente = mskCnpjCliente.Text;
                    newGridCnpjCliente = mskCnpjCliente.Text.Replace(".", "");
                    newGridCnpjCliente = newGridCnpjCliente.Replace("-", "");
                    newGridCnpjCliente = newGridCnpjCliente.Replace("/", "");
                    newGridCnpjCliente = newGridCnpjCliente.Replace(",", "");
                    if (newGridCnpjCliente.Length != 14)
                    {
                        MessageBox.Show("CNPJ incorreto, preencha o campo corretamente.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        conectorWeb_juridica(newGridCnpjCliente);
                    }
                }
            }
        }

        private void btnConsultaWebFisicaCliente_Click(object sender, EventArgs e)
        {
            if (rdCPFSimplificado.Checked == true)
            {
                if ((flagTipopessoa == 1) || (flagTipopessoa == 3))
                {
                    newGridCpfCliente = mskCpfCliente.Text;
                    newGridCpfCliente = mskCpfCliente.Text.Replace(".", "");
                    newGridCpfCliente = newGridCpfCliente.Replace("-", "");
                    newGridCpfCliente = newGridCpfCliente.Replace("/", "");
                    newGridCpfCliente = newGridCpfCliente.Replace(",", "");
                    if (newGridCpfCliente.Length != 11)
                    {
                        MessageBox.Show("CPF incorreto, preencha o campo corretamente.", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        conectorWeb_fisica(newGridCpfCliente);
                    }
                }
            }
        }

        private void btnConsultaWebCodigoCliente_Click(object sender, EventArgs e)
        {
            if (rdCodigoSimplificado.Checked == true)
            {
                if (txtCodigoCliente.Text != "")
                {
                    conectorWeb_codigo(txtCodigoCliente.Text);
                }
            }
        }
    }
}
