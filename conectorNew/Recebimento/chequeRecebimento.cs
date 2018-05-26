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
    public partial class chequeRecebimento : Form
    {
        public chequeRecebimento()
        {
            InitializeComponent();
            typeInsert = 0;
        }
        public chequeRecebimento(string client, string valorParcela, int semaforo, string store, string consultor, int insert)
        {
            InitializeComponent();
            valorFull = valorParcela;
            auxIdCliente = client;
            auxIdLoja = store;
            auxIdFuncionario = consultor;
            flagSemaforo = semaforo;
            if (semaforo == 0)
            {
                objStatus(true);
            }
            else
            {
                objStatus(false);
            }
            txtBancoChequeRecebimento.Select();
            typeInsert = insert;
            this.BackColor = System.Drawing.Color.Blue;
        }
        //#################################################################Variaveis de Ambiente#####################################################
        private int ataque; //0 aberto 1 fechado
        private decimal fator;
        private int posSeparator, auxConsistencia;
        private dados banco = new dados();
        private int flagSemaforo, i, j, countField, countRows; //variavel do loop.
        private conector_full_variable alwaysVariables = new conector_full_variable();
        private string auxBanco, auxIdPedido, valorFull;
        private string auxIdLoja;
        private Int16 auxConfirma = 0;
        private string auxDescricaoBanco;
        private string auxIdCheque;
        private string auxNrParcelas;
        private string auxOrigemCheque = "p"; //p proprio ; t terceiros
        private string auxTypeCheque = "p"; //p a prazao; v a vista
        private string auxLancamento;
        private string auxIdFuncionario = "0";
        private DateTime updateDate = DateTime.Now; //Recebe o acrescimo ou descrecimo na data atual para seguir a atualização.
        private Decimal auxNumeric;
        private string flagNumeric;
        private string auxIdCliente;
        private string auxDescricaoStore;
        private string auxSerie;
        private string auxValue = "0.00";
        private string auxCheque;
        private string auxConta;
        private string auxPrazo;
        private DateTime auxDate;
        private int typeInsert = 0; //Tipo de insercao no 0 banco 1 no vetor
        //#################################################################End Variaveis#############################################################
        //########################################################## Bloco de Properties#############################################################
        public string GridValue
        {
            get
            {
                return auxValue;
            }
            set
            {
                auxValue = value;
            }
        }
        public string GridCodigoBanco
        {
            get
            {
                return auxBanco;
            }
            set
            {
                auxBanco = value;
            }
        }
        public string GridSerie
        {
            get
            {
                return auxSerie;
            }
            set
            {
                auxSerie = value;
            }
        }
        private string auxAgencia;
        public string GridAgencia
        {
            get
            {
                return auxAgencia;
            }
            set
            {
                auxAgencia = value;
            }
        }
        public string GridConta
        {
            get
            {
                return auxConta;
            }
            set
            {
                auxConta = value;
            }
        }
        public string GridNumeroCheque
        {
            get
            {
                return auxCheque;
            }
            set
            {
                auxCheque = value;
            }
        }
        public string GridPrazo
        {
            get
            {
                return auxPrazo;
            }
            set
            {
                auxPrazo = value;
            }
        }
        public string GridTypeCheque
        {
            get
            {
                return auxTypeCheque;
            }
            set
            {
                auxTypeCheque = value;
            }
        }
        public string GridLancamento
        {
            get
            {
                return auxLancamento;
            }
            set
            {
                auxLancamento = value;
            }
        }
        public string GridOrigemCheque
        {
            get
            {
                return auxOrigemCheque;
            }
            set
            {
                auxOrigemCheque = value;
            }
        }
        public DateTime GridVencimento
        {
            get
            {
                return auxDate;
            }
            set
            {
                auxDate = value;
            }
        }
        //##########################################################END Bloco de Properties##########################################################
        //#################################################################Metodos de controle#######################################################
        void conector_obj_grava()
        {
            updateDate = DateTime.Now;
            TimeSpan newDias = updateDate.Subtract(dtpVencimentoChequeRecebimento.Value);
            TimeSpan newDiasEmissao = updateDate.Subtract(dtpEmissaoChequeRecebimento.Value);
            if (cmbRazaoBancoChequeRecebimento.Text != "")
            {
                if (txtAgenciaChequeRecebimento.Text != "")
                {
                    if (txtContaDacChequeRecebimento.Text != "")
                    {
                        if (txtValorChequeRecebimento.Text != "" && Convert.ToDecimal(txtValorChequeRecebimento.Text) > 0 && txtValorChequeRecebimento.Text != "0.00")
                        {
                            if (txtPrazoChequeRecebimento.Text != "")
                            {
                                if (auxTypeCheque == "p")
                                {
                                    if (((Convert.ToInt32(newDiasEmissao.Days) - Convert.ToInt32(newDias.Days))) >= 0)
                                    {
                                            if (flagSemaforo == 0)
                                            {
                                                conectorPDV_inc_cheque();
                                            }
                                            else
                                            {
                                                conectorPDV_alt_cheque();
                                            }
                                    }
                                    else
                                    {
                                        msgInfo msg = new msgInfo("Caro Cliente - " + "Vencimento dever ser superior a emissao, favor altera-lô."); msg.ShowDialog();
                                        dtpVencimentoChequeRecebimento.Select();
                                    }
                                }
                                else
                                {
                                    dtpVencimentoChequeRecebimento.Value = DateTime.Now; //cheque a vista
                                    if (flagSemaforo == 0)
                                    {
                                        conectorPDV_inc_cheque();
                                    }
                                    else
                                    {
                                        conectorPDV_alt_cheque();
                                    }
                                }
                            }
                            else
                            {
                                msgInfo msg = new msgInfo("Caro Cliente - " + "Prazo inválido"); msg.ShowDialog();
                                txtPrazoChequeRecebimento.Select();
                            }
                        }
                        else
                        {
                            msgInfo msg = new msgInfo("Caro Cliente - " + "O valor do cheque deve ser preenchido."); msg.ShowDialog();
                            txtValorChequeRecebimento.Select();
                        }
                    }
                    else
                    {
                        msgInfo msg = new msgInfo("Caro Cliente - " + "Conta corrente invalida ou não informada."); msg.ShowDialog();
                        txtContaDacChequeRecebimento.Select();
                    }
                }
                else
                {
                    msgInfo msg = new msgInfo("Caro Cliente - " + "Agencia invalida ou não informada."); msg.ShowDialog();
                    txtAgenciaChequeRecebimento.Select();
                }
            }
            else
            {
                msgInfo msg = new msgInfo("Caro Cliente - " + "Banco sem descrição significa que o codigo do banco está informado incorretamento, favor selecionar o banco correto."); msg.ShowDialog();
                cmbRazaoBancoChequeRecebimento.Select();
            }
        }
        void objClear()
        {
            txtLeituraCMB7ChequeRecebimento.Clear();
            txtBancoChequeRecebimento.Clear();
            txtSerieChequeChequeRecebimento.Clear();
            txtAgenciaChequeRecebimento.Clear();
            txtContaDacChequeRecebimento.Clear();
            cmbRazaoBancoChequeRecebimento.SelectedIndex = -1;
            txtNumberChequeRecebimento.Clear();
            //txtValorChequeRecebimento.Text = "0.00";
            dtpEmissaoChequeRecebimento.Value = DateTime.Now;
            txtCidadeBancoChequeRecebimento.Clear();
            cmbAvistaAprazoChequeRecebimento.Text = "A PRAZO";
            txtPrazoChequeRecebimento.Text = "1";
            dtpVencimentoChequeRecebimento.Value = DateTime.Now;
            txtHistoricoChequeRecebimento.Clear();
            rbChequeChequeRecebimento.Checked = true;
            rbChequeTerceiroChequeRecebimento.Checked = false;
            cmbRazaoBancoChequeRecebimento.SelectedIndex = -1;
        }
        void objStatus(bool flag)
        {
            txtLeituraCMB7ChequeRecebimento.Enabled = flag;
            txtBancoChequeRecebimento.Enabled = flag;
            txtSerieChequeChequeRecebimento.Enabled = flag;
            txtAgenciaChequeRecebimento.Enabled = flag;
            txtContaDacChequeRecebimento.Enabled = flag;
            cmbRazaoBancoChequeRecebimento.Enabled = flag;
            txtNumberChequeRecebimento.Enabled = flag;
            txtValorChequeRecebimento.Enabled = flag;
            dtpEmissaoChequeRecebimento.Enabled = flag;
            txtCidadeBancoChequeRecebimento.Enabled = flag;
            cmbAvistaAprazoChequeRecebimento.Enabled = flag;
            txtPrazoChequeRecebimento.Enabled = flag;
            dtpVencimentoChequeRecebimento.Enabled = flag;
            txtHistoricoChequeRecebimento.Enabled = flag;
            rbChequeChequeRecebimento.Checked = true;
            rbChequeTerceiroChequeRecebimento.Checked = false;
            cmbRazaoBancoChequeRecebimento.Enabled = flag;
        }
        //#################################################################End Metodos de controle###################################################
        //#################################################################Procedimento de banco#####################################################
        public void conectorPDV_find_banco()
        {
            try
            {
                auxConsistencia = 0;
                banco.abreConexao();
                banco.startTransaction("conectorPDV_find_banco");
                banco.addParametro("tipo", "2");
                banco.addParametro("find", txtBancoChequeRecebimento.Text);
                banco.procedimentoRead();
                if (banco.retornaRead().Read() == true)
                {
                    auxBanco = banco.retornaRead().GetString(1);
                    auxDescricaoBanco = banco.retornaRead().GetString(1);
                }

            }
            catch (Exception erro)
            { msgInfo msg = new msgInfo("Caro Cliente - " + erro.Message); msg.ShowDialog(); auxConsistencia = 1; }
            finally
            {
                banco.fechaConexao();
                if (auxConsistencia == 0)
                {
                    if (auxDescricaoBanco != "")
                    {
                        cmbRazaoBancoChequeRecebimento.Text = auxDescricaoBanco;
                    }
                }
            }
        }
        public void conectorPDV_inc_cheque()
        {
            auxConsistencia = 0;
            try
            {
                banco.abreConexao();
                banco.startTransaction("conectorPDV_inc_cheque");
                banco.addParametro("inc_banco", txtBancoChequeRecebimento.Text);
                banco.addParametro("inc_idloja", auxIdLoja);
                banco.addParametro("inc_idcliente", auxIdCliente);
                banco.addParametro("inc_typeRecebimento", auxOrigemCheque);
                banco.addParametro("inc_contaCorrente", txtContaDacChequeRecebimento.Text);
                banco.addParametro("inc_serie", txtSerieChequeChequeRecebimento.Text);
                banco.addParametro("inc_agencia", txtAgenciaChequeRecebimento.Text);
                banco.addParametro("inc_typeCheque", auxTypeCheque);
                banco.addParametro("inc_prazo", txtPrazoChequeRecebimento.Text);
                banco.addParametro("inc_emissao", String.Format("{0:yyyyMMdd}", dtpEmissaoChequeRecebimento.Value));
                banco.addParametro("inc_vencimento", String.Format("{0:yyyyMMdd}", dtpVencimentoChequeRecebimento.Value));
                banco.addParametro("inc_cityBanco", txtCidadeBancoChequeRecebimento.Text);
                banco.addParametro("inc_numberCheque", txtNumberChequeRecebimento.Text);
                banco.addParametro("inc_valueCheque", txtValorChequeRecebimento.Text);
                banco.addParametro("inc_historico", txtHistoricoChequeRecebimento.Text);
                banco.addParametro("inc_typeLancamento", auxLancamento);
                banco.addParametro("inc_pagamento", "");//informação de baixa ficarão como default
                banco.addParametro("inc_observacao", "");//informação de baixa ficarão como default
                banco.addParametro("inc_idUsuarioLiberacao", "");//informação de baixa ficarão como default
                banco.addParametro("inc_motivoLiberacao", ""); //informação de baixa ficarão como default
                banco.addParametro("inc_cmc7", txtLeituraCMB7ChequeRecebimento.Text);
                banco.addParametro("inc_idusuario", alwaysVariables.Usuario);
                banco.addParametro("inc_alteracao", String.Format("{0:yyyyMMdd}", DateTime.Now));
                banco.addParametro("inc_reserva", "0");
                banco.addParametro("inc_origem", "r");
                banco.addParametro("inc_doc", "0");
                banco.addParametro("inc_terminal", alwaysVariables.Terminal);
                banco.addParametro("inc_funcionario", auxIdFuncionario);
                banco.procedimentoRead();
                if (banco.retornaRead().Read() == true)
                {
                    auxIdCheque = banco.retornaRead().GetString(0);                    
                }
                else
                {
                    auxIdCheque = "0";
                }
            }
            catch (Exception erro)
            { msgInfo msg = new msgInfo("Caro Cliente - " + erro.Message); msg.ShowDialog(); auxConsistencia = 1; }
            finally
            {
                banco.fechaConexao();
                if (auxIdCheque != "0")
                {
                    if (auxConsistencia == 0)
                    {
                        flagSemaforo = 0;
                        objClear();
                        objStatus(false);
                        auxConfirma = 1;
                    }
                }
                else
                {
                    msgInfo msg = new msgInfo("Caro Cliente - " + "Este cheque já possui cadastro no sistema, favor conferir com o administrativo"); msg.ShowDialog();
                    auxConfirma = 0;
                }
                txtBancoChequeRecebimento.Select();

            }
        }
        public void conectorPDV_alt_cheque()
        {
            auxConsistencia = 0;
            try
            {
                banco.abreConexao();
                banco.startTransaction("conectorPDV_alt_cheque");
                banco.addParametro("newidcheque", auxIdCheque);
                banco.addParametro("newbanco", txtBancoChequeRecebimento.Text);
                banco.addParametro("newidloja", auxIdLoja);
                banco.addParametro("newidcliente", auxIdCliente);
                banco.addParametro("newtypeRecebimento", auxOrigemCheque);
                banco.addParametro("newcontaCorrente", txtContaDacChequeRecebimento.Text);
                banco.addParametro("newserie", txtSerieChequeChequeRecebimento.Text);
                banco.addParametro("newagencia", txtAgenciaChequeRecebimento.Text);
                banco.addParametro("newtypeCheque", auxTypeCheque);
                banco.addParametro("newprazo", txtPrazoChequeRecebimento.Text);
                banco.addParametro("newemissao", String.Format("{0:yyyyMMdd}", dtpEmissaoChequeRecebimento.Value));
                banco.addParametro("newvencimento", String.Format("{0:yyyyMMdd}", dtpVencimentoChequeRecebimento.Value));
                banco.addParametro("newcityBanco", txtCidadeBancoChequeRecebimento.Text);
                banco.addParametro("newnumberCheque", txtNumberChequeRecebimento.Text);
                banco.addParametro("newvalueCheque", txtValorChequeRecebimento.Text);
                banco.addParametro("newhistorico", txtHistoricoChequeRecebimento.Text);
                banco.addParametro("newtypeLancamento", auxLancamento);
                banco.addParametro("newpagamento", "");//informação de baixa ficarão como default
                banco.addParametro("newobservacao", "");//informação de baixa ficarão como default
                banco.addParametro("newidUsuarioLiberacao", "");//informação de baixa ficarão como default
                banco.addParametro("newmotivoLiberacao", ""); //informação de baixa ficarão como default
                banco.addParametro("newcmc7", txtLeituraCMB7ChequeRecebimento.Text);
                banco.addParametro("newidusuario", alwaysVariables.Usuario);
                banco.addParametro("newalteracao", String.Format("{0:yyyyMMdd}", DateTime.Now));
                banco.addParametro("newreserva", auxIdPedido);
                banco.procedimentoRead();
                if (banco.retornaRead().Read() == true)
                {
                }
            }
            catch (Exception erro)
            { msgInfo msg = new msgInfo("Caro Cliente - " + erro.Message); msg.ShowDialog(); auxConsistencia = 1; }
            finally
            {
                banco.fechaConexao();
                if (auxConsistencia == 0)
                {
                    flagSemaforo = 0;
                    objClear();
                    objStatus(false);
                    auxConfirma = 1;
                } txtBancoChequeRecebimento.Select();
            }
        }
        public void conector_carrega_banco()
        {

            try
            {
                auxConsistencia = 0;
                banco.abreConexao();
                banco.startTransaction("conectorPDV_find_banco");
                banco.addParametro("tipo", "1");
                banco.addParametro("find", "");
                banco.procedimentoSet();
            }
            catch (Exception erro)
            { msgInfo msg = new msgInfo("Caro Cliente - " + erro.Message); msg.ShowDialog(); auxConsistencia = 1; }
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
                            cmbRazaoBancoChequeRecebimento.Items.Add(banco.retornaSet().Tables[0].Rows[i][1]);
                        }
                    }
                }
                banco.fechaConexao();
            }
        }
        public void conector_carrega_lancamento()
        {

            try
            {
                auxConsistencia = 0;
                banco.abreConexao();
                banco.startTransaction("conectorPDV_find_lacamentoCheque");
                banco.addParametro("tipo", "1");
                banco.addParametro("find", "");
                banco.procedimentoSet();
            }
            catch (Exception erro)
            { msgInfo msg = new msgInfo("Caro Cliente - " + erro.Message); msg.ShowDialog(); auxConsistencia = 1; }
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
                            cmbTipoLancamentoChequeRecebimento.Items.Add(banco.retornaSet().Tables[0].Rows[i][2]);
                        }
                    }
                }
                banco.fechaConexao();
            }
        }
        public void conector_carrega_loja()
        {
            cmbLojaReceberaChequeRecebimento.Items.Clear();
            auxConsistencia = 0;
            try
            {
                banco.abreConexao();
                banco.startTransaction("conectorPDV_find_loja");
                banco.addParametro("tipo", "3");
                banco.addParametro("find_loja", "0");
                banco.procedimentoSet();
            }
            catch (Exception erro)
            { msgInfo msg = new msgInfo("Caro Cliente - " + erro.Message); msg.ShowDialog(); auxConsistencia = 1; }
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
                            cmbLojaReceberaChequeRecebimento.Items.Add(banco.retornaSet().Tables[0].Rows[i][1]);
                        }
                    }
                }
                banco.fechaConexao();
            }
        }

        private void txtBancoChequeRecebimento_Validated(object sender, EventArgs e)
        {
            if (txtBancoChequeRecebimento.Text != "")
            {
                conectorPDV_find_banco();
            }
        }

        private void cmbRazaoBancoChequeRecebimento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbRazaoBancoChequeRecebimento.Text != "")
            {
                try
                {
                    auxConsistencia = 0;
                    banco.abreConexao();
                    banco.startTransaction("conectorPDV_find_banco");
                    banco.addParametro("tipo", "3");
                    banco.addParametro("find", cmbRazaoBancoChequeRecebimento.Text);
                    banco.procedimentoSet();
                }
                catch (Exception erro)
                { msgInfo msg = new msgInfo("Caro Cliente - " + erro.Message); msg.ShowDialog(); auxConsistencia = 1; }
                finally
                {
                    if (auxConsistencia == 0)
                    {
                        auxBanco = Convert.ToString(banco.retornaSet().Tables[0].Rows[0][0]);   
                    }
                    banco.fechaConexao();
                    if (auxBanco != "")
                    {
                        txtBancoChequeRecebimento.Text = auxBanco;
                    }
                }
            }//end if
        }

        private void txtValorChequeRecebimento_Validated(object sender, EventArgs e)
        {
            if (txtValorChequeRecebimento.Text != "")
            {
                auxNumeric = Convert.ToDecimal(txtValorChequeRecebimento.Text);//currency
                flagNumeric = String.Format("{0:F2}", auxNumeric);
                txtValorChequeRecebimento.Text = flagNumeric.Replace(",", ".").Trim();
                txtPrazoChequeRecebimento.Select();
            }
            else
            {
                txtValorChequeRecebimento.Text = "0.00";
            }
        }

        private void cmbAvistaAprazoChequeRecebimento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAvistaAprazoChequeRecebimento.Text != "")
            {
                if (cmbAvistaAprazoChequeRecebimento.Text == "A PRAZO")
                {
                    auxTypeCheque = "p";
                }
                else
                {
                    auxTypeCheque = "v";
                }
            }
        }

        private void cmbLojaReceberaChequeRecebimento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbLojaReceberaChequeRecebimento.Text != "")
            {
                try
                {
                    auxConsistencia = 0;
                    banco.abreConexao();
                    banco.startTransaction("conectorPDV_find_loja");
                    banco.addParametro("tipo", "2");
                    banco.addParametro("find_loja", cmbLojaReceberaChequeRecebimento.Text);
                    banco.procedimentoSet();
                }
                catch (Exception erro)
                { msgInfo msg = new msgInfo("Caro Cliente - " + erro.Message); msg.ShowDialog(); auxConsistencia = 1; }
                finally
                {
                    if (auxConsistencia == 0)
                    {
                        auxIdLoja = Convert.ToString(banco.retornaSet().Tables[0].Rows[0][0]);   
                    }
                    banco.fechaConexao();
                }
            }//end if
        }

        private void txtPrazoChequeRecebimento_Validated(object sender, EventArgs e)
        {
            DateTime dateOld = dtpVencimentoChequeRecebimento.Value;
            if (txtPrazoChequeRecebimento.Text != "")
            {
                //auxNumeric = Convert.ToDecimal(txtPrazoGeraCheque.Text);//currency
                //flagNumeric = String.Format("{0:F2}", auxNumeric);
                //txtPrazoGeraCheque.Text = flagNumeric.Replace(",", ".").Trim();
            }
            else
            {
                txtPrazoChequeRecebimento.Text = "1";
            }

            if (txtPrazoChequeRecebimento.Text != "" && txtPrazoChequeRecebimento.Text != "0")
            {
                dtpVencimentoChequeRecebimento.Value = updateDate.AddDays(Convert.ToDouble(txtPrazoChequeRecebimento.Text));
            }
            else
            {
                dtpVencimentoChequeRecebimento.Value = dateOld;
            }
        }

        private void cmbTipoLancamentoChequeRecebimento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTipoLancamentoChequeRecebimento.Text != "")
            {
                auxLancamento = cmbTipoLancamentoChequeRecebimento.Text.Substring(0, 2);
            }
        }

        private void rbChequeChequeRecebimento_CheckedChanged(object sender, EventArgs e)
        {
            auxOrigemCheque = "p";
        }

        private void rbChequeTerceiroChequeRecebimento_CheckedChanged(object sender, EventArgs e)
        {
            auxOrigemCheque = "t";
            if (rbChequeTerceiroChequeRecebimento.Checked == true)
            {
                btnLiberaTerceiroChequeRecebimento.Visible = true;
            }
            else
            {
                btnLiberaTerceiroChequeRecebimento.Visible = false;
            }
        }

        private void btnCancelaChequeRecebimento_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Dispose();
        }

        private void btnConfirmaAlteracaoChequeRecebimento_Click(object sender, EventArgs e)
        {
            if (typeInsert == 0)
            {
                conector_obj_grava();
                if (auxConfirma == 1)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Dispose();
                }
                else
                {
                    objClear();
                    txtBancoChequeRecebimento.Select();
                }
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                this.Dispose();
            }
        }

        private void chequeRecebimento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Dispose();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                ProcessTabKey(true);
            }
            else if (e.KeyCode == Keys.F5)
            {
                if (typeInsert == 0)
                {
                    conector_obj_grava();
                    if (auxConfirma == 1)
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Dispose();
                    }
                    else
                    {
                        objClear();
                        txtBancoChequeRecebimento.Select();
                    }
                }
                else
                {
                    this.DialogResult = DialogResult.OK;
                    this.Dispose();
                }
            }
        }

        private void chequeRecebimento_Load(object sender, EventArgs e)
        {
            auxConsistencia = 0;
            auxIdLoja = alwaysVariables.Store;
            conector_carrega_banco();
            conector_carrega_loja();
            //#############Store descricao
            if (cmbLojaReceberaChequeRecebimento.Text == "")
            {
                try
                {
                    auxConsistencia = 0;
                    banco.abreConexao();
                    banco.startTransaction("conectorPDV_find_loja");
                    banco.addParametro("tipo", "1");
                    banco.addParametro("find_loja", alwaysVariables.Store);
                    banco.procedimentoRead();
                    if (banco.retornaRead().Read() == true)
                    {
                        auxDescricaoStore = banco.retornaRead().GetString(1);
                    }
                }
                catch (Exception erro)
                {
                    msgInfo msg = new msgInfo("Caro Cliente - " + erro.Message); msg.ShowDialog(); auxConsistencia = 1;
                }
                finally
                {
                    if (auxConsistencia == 0)
                    {
                        auxIdLoja = Convert.ToString(banco.retornaSet().Tables[0].Rows[0][0]);
                        auxTypeCheque = "p";
                    }
                    banco.fechaConexao();
                    if (auxConsistencia == 0)
                    {
                        cmbLojaReceberaChequeRecebimento.Text = auxDescricaoStore;   
                    }
                }
            }//end if
            //#############End Descricao
            conector_carrega_lancamento();
            objClear();
            //lbkLeituraCMC7GeraCheque.Text = valorFull;
            auxOrigemCheque = "p";
            cmbAvistaAprazoChequeRecebimento.Text = "A PRAZO";
            dtpVencimentoChequeRecebimento.Value = updateDate.AddDays(1);
            txtBancoChequeRecebimento.Select();
            cmbTipoLancamentoChequeRecebimento.SelectedIndex = 0;
            txtValorChequeRecebimento.Text = valorFull;
            txtBancoChequeRecebimento.Select();
        }

        private void txtAgenciaChequeRecebimento_TextChanged(object sender, EventArgs e)
        {
            auxAgencia = txtAgenciaChequeRecebimento.Text;
        }

        private void txtBancoChequeRecebimento_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtSerieChequeChequeRecebimento_TextChanged(object sender, EventArgs e)
        {
            auxSerie = txtSerieChequeChequeRecebimento.Text;
        }

        private void txtValorChequeRecebimento_TextChanged(object sender, EventArgs e)
        {
            auxValue = txtValorChequeRecebimento.Text;
        }

        private void txtNumberChequeRecebimento_TextChanged(object sender, EventArgs e)
        {
            auxCheque = txtNumberChequeRecebimento.Text;
        }

        private void txtContaDacChequeRecebimento_TextChanged(object sender, EventArgs e)
        {
            auxConta = txtContaDacChequeRecebimento.Text;
        }

        private void dtpVencimentoChequeRecebimento_ValueChanged(object sender, EventArgs e)
        {
            auxDate = dtpVencimentoChequeRecebimento.Value;
        }

        private void txtPrazoChequeRecebimento_TextChanged(object sender, EventArgs e)
        {
            auxPrazo = txtPrazoChequeRecebimento.Text;
        }
    }
}
