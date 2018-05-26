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
    public partial class consultaFone : Form
    {
        public consultaFone()
        {
            InitializeComponent();
        }
        public consultaFone(string aux)
        {
            InitializeComponent();
            fone = aux;

        }
        //###################Variaveis encapsuladas################################
        private int i, j, countField, countRows, auxTipopessoa; //variavel do loop.
        private dados banco = new dados();
        private int auxConsistencia = 0;
        private DataSet setBanco;
        private string fone;
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
                auxCodigo= value;
            }
        }
        //###############End Properties

        //####################Procedimento de banco################################
        public void conectorPDV_find_consulta()
        {
            texto = " select ";
            texto += " tab.idCliente AS Codigo, ";
            texto += " if(tab.idtipoPessoa != 3, if(tab.idtipoPessoa=1,(tab1.nome),tab2.razao),tab3.nome) as 'Nome', ";
            texto += " mid(tab6.ddd,1,4) as DDD, ";
            texto += " tab6.telefone as Telefone ";
            texto += " from ";
            texto += " conectorPDV.cliente tab ";
            texto += " left join conectorPDV.fone tab6 on(tab.idCliente = tab6.idCliente) ";
            texto += " left join conectorPDV.fisica tab1 on(tab.idcliente = tab1.idcliente) ";
            texto += " left join conectorPDV.juridica tab2 on(tab.idcliente = tab2.idcliente) ";
            texto += " left join (select rural.idcliente,rural.cpf as cpf_1,rural.nome,rural.nascimento,rural.ie,rural.idsexo,rural.identidade,rural.idcivil,sexo.descricao as sexo1,civil.descricao as civil1 from conectorPDV.rural ";
            texto += " inner join conectorPDV.sexo on(rural.idsexo = sexo.idsexo) ";
            texto += " inner join conectorPDV.civil on(rural.idcivil = civil.idcivil)) as tab3 ";
            texto += " on(tab.idcliente = tab3.idcliente) ";
            texto += " where tab6.telefone = ";
            texto += fone;
            try
            {
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
                    dgvConsultaFone.DataSource = banco.retornaSet().Tables[0].DefaultView;   
                }
                
                banco.fechaConexao();
                //MessageBox.Show("Carrega...", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        //####################End Banco
        private void dgvConsultaFone_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void consultaFone_Load(object sender, EventArgs e)
        {
            conectorPDV_find_consulta();
        }

        private void dgvConsultaFone_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvConsultaFone.RowCount > 1 && dgvConsultaFone.CurrentRow.Cells[0].Value != null)
            {
                auxCodigo = dgvConsultaFone.CurrentRow.Cells[0].Value.ToString();
            }
        }

        private void dgvConsultaFone_DoubleClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }

        private void consultaFone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Dispose();
            }
        }

        private void dgvConsultaFone_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Dispose();
        }

        private void dgvConsultaFone_KeyUp(object sender, KeyEventArgs e)
        {
            if (dgvConsultaFone.RowCount > 1 && dgvConsultaFone.CurrentRow.Cells[0].Value != null)
            {
                auxCodigo = dgvConsultaFone.CurrentRow.Cells[0].Value.ToString();
            }
        }
    }
}
