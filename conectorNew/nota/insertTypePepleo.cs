using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace conectorPDV001
{
    public partial class insertTypePepleo : Form
    {
        public insertTypePepleo()
        {
            InitializeComponent();
        }
        public insertTypePepleo( string loja, string users, string terminal)
        {
            InitializeComponent();
            auxIdLoja = loja;
            auxIdUser = users;
            auxIdTerminal = terminal;
        }
        //#############################Variaveis encapsuladas#######################################
        private consultaCliente consulta;
        private pesquisaLoja consultaLoja;
        private pesquisaFornecedor consultaFornecedor;
        private string auxIdLoja = "";
        private string auxIdTerminal = "";
        private string auxIdUser = "";
        private int flagLibera; //0 - cliente 1 - fornecedor 3 - store
        //#############################End variaveis################################################
        //#############################Bloco de procedimento single#################################
        void conector_exe_terminal(int aux) //Funcao não utilizada
        {
            switch (aux)
            {
                case 0:
                    this.Hide();
                    consulta = new consultaCliente(auxIdLoja, auxIdUser, Convert.ToString(auxIdTerminal));
                    consulta.ShowDialog();
                    this.Close();
                    break;
                case 1:
                    this.Hide();
                    consultaFornecedor = new pesquisaFornecedor();
                    consultaFornecedor.ShowDialog();
                    this.Close();
                    break;
                case 3:
                    this.Hide();
                    consultaLoja = new pesquisaLoja();
                    consultaLoja.ShowDialog();
                    this.Close();
                    break;
            }
        }
        //#############################End single
        public int GridLibera
        {
            get
            {
                return flagLibera;
            }
            set
            {
                flagLibera = value;
            }
        }
        
        private void btnFormConsultaCliente_Click(object sender, EventArgs e)
        {
            flagLibera = 0;
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }

        private void btnFormPesquisaCliente_Click(object sender, EventArgs e)
        {
            flagLibera = 1;
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }

        private void btnFormPesquisaLoja_Click(object sender, EventArgs e)
        {
            flagLibera = 3;
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }

        private void insertTypePepleo_KeyDown(object sender, KeyEventArgs e)
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

            }
        }
    }
}
