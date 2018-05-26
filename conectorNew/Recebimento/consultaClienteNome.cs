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
    public partial class consultaClienteNome : Form
    {
        public consultaClienteNome()
        {
            InitializeComponent();
        }
        public consultaClienteNome(string aux, string aux1)
        {
            InitializeComponent();
            nome = aux;
            digito = aux1;
        }
        //###################Variaveis encapsuladas################################
        private int i, j, countField, countRows, auxTipopessoa; //variavel do loop.
        private dados banco = new dados();
        private DataSet setBanco;
        private int auxConsistencia = 0;
        private string nome, digito;
        private string texto;
        private string auxCodigo;
        //###################End Variaveis######
        // Bloco de Properties, encapsulamento variaveis
        public string GridCodigo
        {
            get
            {
                return auxCodigo;
            }
            set
            {
                auxCodigo = value;
            }
        }
        //###############End Properties

        //####################Procedimento de banco################################
        
        public void conectorPDV_find_consultaNome()
        {
            texto = " select idcliente as Codigo, nome,cpf, nascimento from fisica where nome like concat('";
            texto +=  nome;
            texto += "' ,'%') ";
            try
            {
                auxConsistencia = 0;
                banco.abreConexao();
                banco.singleTransaction(texto);
                banco.procedimentoSet();
            }
            catch (Exception erro)
            {
                auxConsistencia = 1;
                msgInfo msg = new msgInfo("Caro Cliente - " + erro.Message); msg.ShowDialog();
            }
            finally
            {
                if (auxConsistencia == 0)
                {
                    dgvConsultaNome.DataSource = banco.retornaSet().Tables[0].DefaultView;
                }
                banco.fechaConexao();
                //MessageBox.Show("Carrega...", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public void conectorPDV_find_consultaRazao()
        {
            texto = " select idcliente as Codigo, razao,cnpj, ie from juridica where razao like concat('";
            texto += nome;
            texto += "' ,'%') ";
            try
            {
                auxConsistencia = 0;
                banco.abreConexao();
                banco.singleTransaction(texto);
                banco.procedimentoSet();
            }
            catch (Exception erro)
            {
                msgInfo msg = new msgInfo("Caro Cliente - " + erro.Message); msg.ShowDialog(); auxConsistencia = 1;
            }
            finally
            {
                if (auxConsistencia == 0)
                {
                    dgvConsultaNome.DataSource = banco.retornaSet().Tables[0].DefaultView;   
                }
                banco.fechaConexao();
                //MessageBox.Show("Carrega...", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        //####################End Banco
        private void consultaClienteNome_Load(object sender, EventArgs e)
        {
            switch (digito)
            {
                case "7":
                    conectorPDV_find_consultaRazao();
                    break;
                case "4":
                    conectorPDV_find_consultaNome();
                    break;
            }
            
        }

        private void dgvConsultaNome_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvConsultaNome.RowCount > 1 && dgvConsultaNome.CurrentRow.Cells[0].Value != null)
            {
                auxCodigo = dgvConsultaNome.CurrentRow.Cells[0].Value.ToString();
            }
        }

        private void dgvConsultaNome_DoubleClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }

        private void consultaClienteNome_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Dispose();
            }
        }

        private void dgvConsultaNome_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }

        private void dgvConsultaNome_KeyUp(object sender, KeyEventArgs e)
        {
            if (dgvConsultaNome.RowCount > 1 && dgvConsultaNome.CurrentRow.Cells[0].Value != null)
            {
                auxCodigo = dgvConsultaNome.CurrentRow.Cells[0].Value.ToString();
            }
        }
    }
}
