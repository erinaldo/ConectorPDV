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
    public partial class cep : Form
    {
        public cep()
        {
            InitializeComponent();
        }
        //Contrutor com passagem de paramentro
        public cep(string auxCep)
            :base()
        {
            newCep = auxCep;
            this.InitializeComponent();
        }
        //#######################################################Bloco de variaveis encapsuladas##############################################################
        private dados banco = new dados();
        private string newCep;
        private string newComplemento;
        private string newLogradouro;
        private string newBairro;
        private string newUf;
        private string newCity;
        private string newCodigoMunicipio;
        private string newIdCep;
        private int auxConsistencia = 0;
        //#################################################### Bloco de Properties, encapsulamento variaveis###################################################
        public string CodigoMunicipio
        {
            get
            {
                return newCodigoMunicipio;
            }
            set
            {
                newCodigoMunicipio = value;
            }
        }
        public string auxIdCep
        {
            get
            {
                return newIdCep;
            }
            set
            {
                newIdCep = value;
            }
        }
        public string city
        {
            get
            {
                return newCity;
            }
            set
            {
                newCity = value;
            }
        }
        public string uf
        {
            get
            {
                return newUf;
            }
            set
            {
                newUf = value;
            }
        }
        public string logradouro
        {
            get
            {
                return newLogradouro;
            }
            set
            {
                newLogradouro = value;
            }
        }
        public string bairro
        {
            get
            {
                return newBairro;
            }
            set
            {
                newBairro = value;
            }
        }
        public string complemento
        {
            get
            {
                return newComplemento;
            }
            set
            {
                newComplemento = value;
            }
        }

        //#########################################################Bloco Procedimentos de Banco###############################################################
        private void conectorPDV_find_cep()
        {
            try {
                banco.abreConexao();
                banco.startTransaction("conectorPDV_find_cep");
                banco.addParametro("find_cep", newCep);
                banco.procedimentoRead();
                banco.retornaRead().Read();
                bairro = banco.retornaRead().GetString(2);
                logradouro = banco.retornaRead().GetString(1);
                uf = banco.retornaRead().GetString(5);
                city = banco.retornaRead().GetString(4);
                complemento = banco.retornaRead().GetString(3);
                CodigoMunicipio = banco.retornaRead().GetString(6);
                newIdCep = banco.retornaRead().GetString(7);
            }
            catch (Exception erro) { msgInfo msg = new msgInfo("Caro Cliente - " + erro.Message); msg.ShowDialog(); auxConsistencia = 1; }
            finally { 
                banco.fechaConexao();
                if (auxConsistencia == 0)
                {
                    txtRuaAvenidaCep.Text = logradouro;
                    txtCidadeCep.Text = city;
                    txtBairroCep.Text = bairro;
                    txtComplementoCep.Text = complemento;
                    txtUfCep.Text = uf;
                    txtCodMun.Text = CodigoMunicipio;   
                }
            };
        }
        

        private void cep_Load(object sender, EventArgs e)
        {
            conectorPDV_find_cep();
            //conector_find_municipio();
        }

        private void btnCarregarCep_Click(object sender, EventArgs e)
        {
           
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }

        private void txtRuaAvenidaCep_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void cep_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Dispose();
            }
        }
    }
}
