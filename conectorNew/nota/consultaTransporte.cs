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
    public partial class consultaTransporte : Form
    {
        public consultaTransporte()
        {
            InitializeComponent();
        }
        public consultaTransporte(Int16 modalidade, string combo, int semaforo, string idTransporte, string idNf)
        {

            InitializeComponent();
            auxModalidadeFrete = modalidade;
            cmbFreteConsultaTransporte.Text = combo;

            if (auxModalidadeFrete == 9)
            {
                statusObj(false);
            }
            else
            {
                statusObj(true);
            }
            flagSemaforo = semaforo;
            if (semaforo == 1)
            {
                auxIdNf = idNf;
                auxIdNfTransportadora = idTransporte;   
            }
            
        }
        //########################################Variaveis encapsuladas - Parametro configuracao Transportadora############
        private int i, j, countField, countRows, auxConsistencia; //variavel do loop.
        private int flagSemaforo = 0;
        private dados banco = new dados();
        private pesquisaCFOP findCFOP;
        private pesquisaVeiculo findVeiculo;
        private string auxInativoProduto;
        private Decimal auxNumeric;
        private string flagNumeric;
        private int posSeparator;
        private string auxIdNf = "";
        private string auxIdNfTransportadora = "";
        private string auxIdTransportadora;
        private string optionCodigoTransportadora = "0";
        private string optionRazaoTransportadora;
        private string optionCnpjTransportadora;
        private string optionIeTransportadora;
        private string optionAbreviaturaTransportadora;
        private string optionCodigoMunicipioTransportadora;
        private string optionCepTransportadora;
        private string optionidCepBairroTransportadora;
        private string optionInativaTransportadora;
        private string optionEnderecoTransportadora;
        private string optionBairroTransportadora;
        private string optionNumberTransportadora;
        private string optionCidadeTransportadora;
        private string optionUfTransportadora;
        private string optionComplementoTransportadora;
        private string auxIdUfTransportadora;
        private string auxIdEnderecoTransportadora;
        private pesquisaTransportadora findTransporte;
        private Int16 auxModalidadeFrete;
        private string optionCFOPFrete;
        private string optionDescricaoTipoFrete;
        private string optionBaseCalculoFrete = "0.00";
        private string optionAliquotaFrete = "0.00";
        private string optionIcmsFrete = "0.00";
        private string optionValorTotalFrete = "0.00";
        private string optionIdVeiculo;
        private string optionVolumeQtty;
        private string optionVolumePesoNumber;
        private string optionVolumePesoBruto;
        private string optionVolumePesoLiquido;
        private string optionVolumeMarca;
        private string optionVolumeEspecie;
        private string auxGrididestado;
        private string optionPlacaVeiculo;
        private string optionDescricaoVeiculo;
        private string optionRntcVeiculo;
        private string auxGridUFVeiculo;
        private string optionIsentoIcms;

        //########################################End Variaveis ############################################################
        //########################################Proparteis  ############################################################
        public string GridUf
        {
            get
            {
                return optionUfTransportadora;
            }
            set
            {
                optionUfTransportadora = value;
            }
        }
        public string GridAliquota
        {
            get
            {
                return optionAliquotaFrete;
            }
            set
            {
                optionAliquotaFrete = value;
            }
        }
        public string GridDescricaoVeiculo
        {
            get
            {
                return optionDescricaoVeiculo;
            }
            set
            {
                optionDescricaoVeiculo = value;
            }
        }
        public Int16 GridModalidade
        {
            get
            {
                return auxModalidadeFrete;
            }
            set
            {
                auxModalidadeFrete = value;
            }
        }
        public string GridIsentoIcms
        {
            get
            {
                return optionIsentoIcms;
            }
            set
            {
                optionIsentoIcms = value;
            }
        }
        public string GrididUFVeiculo
        {
            get
            {
                return auxGridUFVeiculo;
            }
            set
            {
                auxGridUFVeiculo = value;
            }
        }
        public string GrididEstado
        {
            get
            {
                return auxGrididestado;
            }
            set
            {
                auxGrididestado = value;
            }
        }
        public string GridNumber
        {
            get
            {
                return optionNumberTransportadora;
            }
            set
            {
                optionNumberTransportadora = value;
            }
        }
        public string GridComplemento
        {
            get
            {
                return optionComplementoTransportadora;
            }
            set
            {
                optionComplementoTransportadora = value;
            }
        }
        public string GridEndereco
        {
            get
            {
                return optionEnderecoTransportadora;
            }
            set
            {
                optionEnderecoTransportadora = value;
            }
        }
        public string Gridbairro
        {
            get
            {
                return optionBairroTransportadora;
            }
            set
            {
                optionBairroTransportadora = value;
            }
        }
        public string Gridsped
        {
            get
            {
                return optionCodigoMunicipioTransportadora;
            }
            set
            {
                optionCodigoMunicipioTransportadora = value;
            }
        }
        public string Gridabreviatura
        {
            get
            {
                return optionAbreviaturaTransportadora;
            }
            set
            {
                optionAbreviaturaTransportadora = value;
            }
        }
        public string Gridmunicipio
        {
            get
            {
                return optionCidadeTransportadora;
            }
            set
            {
                optionCidadeTransportadora = value;
            }
        }
        public string Gridcep
        {
            get
            {
                return optionCepTransportadora;
            }
            set
            {
                optionCepTransportadora = value;
            }
        }
        public string Gridie
        {
            get
            {
                return optionIeTransportadora;
            }
            set
            {
                optionIeTransportadora = value;
            }
        }
        public string GridCnpj
        {
            get
            {
                return optionCnpjTransportadora;
            }
            set
            {
                optionCnpjTransportadora = value;
            }
        }
        public string GridCodigo
        {
            get
            {
                return optionCodigoTransportadora;
            }
            set
            {
                optionCodigoTransportadora = value;
            }
        }
        public string GridRazao
        {
            get
            {
                return optionRazaoTransportadora;
            }
            set
            {
                optionRazaoTransportadora = value;
            }
        }

        public string GridStatus
        {
            get
            {
                return optionInativaTransportadora;
            }
            set
            {
                optionInativaTransportadora = value;
            }
        }

        //########################################New property
        public string GridCFOPFrete
        {
            get
            {
                return optionCFOPFrete;
            }
            set
            {
                optionCFOPFrete = value;
            }
        }
        public string GridDescricaoTipoFrete
        {
            get
            {
                return optionDescricaoTipoFrete;
            }
            set
            {
                optionDescricaoTipoFrete = value;
            }
        }
        public string GridBaseCalculo
        {
            get
            {
                return optionBaseCalculoFrete;
            }
            set
            {
                optionBaseCalculoFrete = value;
            }
        }
        public string GridIcmsFrete
        {
            get
            {
                return optionIcmsFrete;
            }
            set
            {
                optionIcmsFrete = value;
            }
        }

        public string GridValorTotalFrete
        {
            get
            {
                return optionValorTotalFrete;
            }
            set
            {
                optionValorTotalFrete = value;
            }
        }
        public string GridIdVeiculo
        {
            get
            {
                return optionIdVeiculo;
            }
            set
            {
                optionIdVeiculo = value;
            }
        }
        public string GridPlacaVeiculo
        {
            get
            {
                return optionPlacaVeiculo;
            }
            set
            {
                optionPlacaVeiculo = value;
            }
        }
        public string GridANTTVeiculo
        {
            get
            {
                return optionRntcVeiculo;
            }
            set
            {
                optionRntcVeiculo = value;
            }
        }
        
        public string GridVolumeQtty
        {
            get
            {
                return optionVolumeQtty;
            }
            set
            {
                optionVolumeQtty = value;
            }
        }
        public string GridVolumeEspecie
        {
            get
            {
                return optionVolumeEspecie;
            }
            set
            {
                optionVolumeEspecie = value;
            }
        }
        public string GridVolumeMarca
        {
            get
            {
                return optionVolumeMarca;
            }
            set
            {
                optionVolumeMarca = value;
            }
        }
        public string GridVolumePesoLiquido
        {
            get
            {
                return optionVolumePesoLiquido;
            }
            set
            {
                optionVolumePesoLiquido = value;
            }
        }
        public string GridVolumePesoBruto
        {
            get
            {
                return optionVolumePesoBruto;
            }
            set
            {
                optionVolumePesoBruto = value;
            }
        }
        public string GridVolumePesoNumber
        {
            get
            {
                return optionVolumePesoNumber;
            }
            set
            {
                optionVolumePesoNumber = value;
            }
        }


        //####################################################End proparty##################################################
        //##################################################Procedimento simples com objetos################################
        void clearObj()
        {
            txtIdTransportadoraConsultaTransporte.Clear();
            txtDescricaoTransportadoraConsultaTransporte.Clear();
            chkIsentoIcmsConsultaTransporte.Checked = false;
            txtBaseCalculoConsultaTransporte.Text = "0.00";
            txtIcmsConsultaTransporte.Text = "0.00";
            txtValorTotaldoFreteConsultaTransporte.Text = "0.00";
            txtCFOPConsultaTransporte.Clear();
            txtIdVeiculoConsultaTransporte.Clear();
            txtDescricaoVeiculoConsultaTransporte.Clear();
            txtQttyVolumeConsultaTransporte.Clear();
            txtEspecieConsultaTransporte.Text = "DIVERSOS";
            txtMarcaVolumeConsultaTransporte.Clear();
            txtNumeracaoConsultaTransporte.Clear();
            txtPesoBrutoVolumeConsultaTransporte.Text = "0.00";
            txtPesoLiquidoVolumeConsultaTransporte.Text = "0.00";
            txtAliquotaFrete.Text = "0.00";
        }
        void statusObj(Boolean flag)
        {
            txtIdTransportadoraConsultaTransporte.Enabled = flag;
            txtDescricaoTransportadoraConsultaTransporte.Enabled = flag;
            txtBaseCalculoConsultaTransporte.Enabled = flag;
            txtIcmsConsultaTransporte.Enabled = flag;
            txtValorTotaldoFreteConsultaTransporte.Enabled = flag;
            txtCFOPConsultaTransporte.Enabled = flag;
            txtIdVeiculoConsultaTransporte.Enabled = flag;
            txtDescricaoVeiculoConsultaTransporte.Enabled = flag;
            txtQttyVolumeConsultaTransporte.Enabled = flag;
            txtEspecieConsultaTransporte.Enabled = flag;
            txtMarcaVolumeConsultaTransporte.Enabled = flag;
            txtNumeracaoConsultaTransporte.Enabled = flag;
            txtPesoBrutoVolumeConsultaTransporte.Enabled = flag;
            txtPesoLiquidoVolumeConsultaTransporte.Enabled = flag;
            txtAliquotaFrete.Enabled = flag;
            chkIsentoIcmsConsultaTransporte.Enabled = true;
        }
        //##################################################End Simples ####################################################
        //##################################################Procedimento de banco de dados##################################
        public void conector_find_CFOP(string flagCfop)
        {


            try
            {
                banco.abreConexao();
                banco.startTransaction("conector_find_CFOP");
                banco.addParametro("parametro", "7");
                banco.addParametro("find", flagCfop);
                banco.procedimentoRead();
                if (banco.retornaRead().Read() == true)
                {
                    //newStatusCFOP = banco.retornaRead().GetString(12) == "0" ? "false" : "true";
                    optionCFOPFrete = banco.retornaRead().GetString(0);
                }
                else
                {
                    txtCFOPConsultaTransporte.Clear();
                    MessageBox.Show("CFOP invalido!", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            catch (Exception erro)
            { MessageBox.Show(erro.Message, "Caro Usuário"); }
            finally
            {
                banco.fechaConexao();

            }
        }
        public void conector_carrega_transporte(string id)
        {

            try
            {
                banco.abreConexao();
                banco.startTransaction("conector_find_transportadora");
                banco.addParametro("tipo", "1");
                banco.addParametro("find", id);
                banco.procedimentoRead();
                if (banco.retornaRead().Read() == true)
                {
                    optionCodigoTransportadora = banco.retornaRead().GetString(0);
                    optionRazaoTransportadora = banco.retornaRead().GetString(1);
                    optionCnpjTransportadora = banco.retornaRead().GetString(2);
                    optionIeTransportadora = banco.retornaRead().GetString(3);
                    optionCepTransportadora = banco.retornaRead().GetString(4);
                    optionCidadeTransportadora = banco.retornaRead().GetString(5);
                    optionAbreviaturaTransportadora = banco.retornaRead().GetString(6);
                    optionCodigoMunicipioTransportadora = banco.retornaRead().GetString(7);
                    optionInativaTransportadora = banco.retornaRead().GetString(8);
                    optionBairroTransportadora = banco.retornaRead().GetString(9);
                    optionEnderecoTransportadora = banco.retornaRead().GetString(10);
                    optionComplementoTransportadora = banco.retornaRead().GetString(11);
                    optionNumberTransportadora = banco.retornaRead().GetString(12);
                    optionUfTransportadora = banco.retornaRead().GetString(14);
                    auxIdUfTransportadora = banco.retornaRead().GetString(13);
                }
                else { txtIdTransportadoraConsultaTransporte.Clear(); txtDescricaoTransportadoraConsultaTransporte.Clear(); }
            }
            catch (Exception erro)
            { MessageBox.Show(erro.Message, "Caro Usuário"); }
            finally
            {
                banco.fechaConexao();
                if (optionRazaoTransportadora != "")
                {
                    txtDescricaoTransportadoraConsultaTransporte.Text = optionRazaoTransportadora;
                    txtIdTransportadoraConsultaTransporte.Text = optionCodigoTransportadora;
                }
            }
        }
        public void conector_find_frete(string idTransporte, string idNf)
        {
            try
            {
                banco.abreConexao();
                banco.startTransaction("conector_find_frete");
                banco.addParametro("tipo", "1");
                banco.addParametro("findTransporte", idTransporte);
                banco.addParametro("findNf", idNf);
                banco.procedimentoRead();
                if (banco.retornaRead().Read() == true)
                {
                    txtIdTransportadoraConsultaTransporte.Text = banco.retornaRead().GetString(10);
                    txtDescricaoTransportadoraConsultaTransporte.Text = banco.retornaRead().GetString(20);
                    chkIsentoIcmsConsultaTransporte.Checked = banco.retornaRead().GetString(16) == "0" ? false : true;
                    txtBaseCalculoConsultaTransporte.Text = banco.retornaRead().GetString(11);
                    txtAliquotaFrete.Text = banco.retornaRead().GetString(12);
                    txtIcmsConsultaTransporte.Text = banco.retornaRead().GetString(13);
                    txtIdVeiculoConsultaTransporte.Text = banco.retornaRead().GetString(21);
                    txtValorTotaldoFreteConsultaTransporte.Text = banco.retornaRead().GetString(14);
                    txtCFOPConsultaTransporte.Text = banco.retornaRead().GetString(15);
                    optionUfTransportadora = banco.retornaRead().GetString(3);
                    auxGridUFVeiculo = banco.retornaRead().GetString(17);
                    optionPlacaVeiculo = banco.retornaRead().GetString(18);
                    optionRntcVeiculo = banco.retornaRead().GetString(19);
                    optionRntcVeiculo = banco.retornaRead().GetString(19);
                    txtQttyVolumeConsultaTransporte.Text = banco.retornaRead().GetString(4);
                    txtPesoBrutoVolumeConsultaTransporte.Text = banco.retornaRead().GetString(9);
                    txtPesoLiquidoVolumeConsultaTransporte.Text = banco.retornaRead().GetString(8);
                    txtNumeracaoConsultaTransporte.Text = banco.retornaRead().GetString(7);
                    txtMarcaVolumeConsultaTransporte.Text = banco.retornaRead().GetString(6);
                    txtEspecieConsultaTransporte.Text = banco.retornaRead().GetString(5);
                }
            }
            catch (Exception erro)
            { MessageBox.Show(erro.Message, "Caro Usuário"); }
            finally
            {
                banco.fechaConexao();
                if (txtIdVeiculoConsultaTransporte.Text != "")
                {
                    conector_find_veiculo(txtIdVeiculoConsultaTransporte.Text);
                }
            }
        }
        public void conector_find_veiculo(string id)
        {
            try
            {
                banco.abreConexao();
                banco.startTransaction("conector_find_veiculo");
                banco.addParametro("tipo", "7");
                banco.addParametro("find", id);
                banco.procedimentoRead();
                if (banco.retornaRead().Read() == true)
                {
                    optionIdVeiculo = banco.retornaRead().GetString(0);
                    optionDescricaoVeiculo = banco.retornaRead().GetString(1);
                    auxGridUFVeiculo = banco.retornaRead().GetString(3);
                    optionPlacaVeiculo = banco.retornaRead().GetString(2);
                    optionRntcVeiculo = banco.retornaRead().GetString(6);
                    
                }
            }
            catch (Exception erro)
            { MessageBox.Show(erro.Message, "Caro Usuário"); }
            finally
            {
                banco.fechaConexao();
                if (optionDescricaoVeiculo != "")
                {
                    txtDescricaoVeiculoConsultaTransporte.Text = optionDescricaoVeiculo;
                    txtIdVeiculoConsultaTransporte.Text = optionIdVeiculo;
                }
            }
        }
        //###############################################################END Banco de dados#################################
        private void consultaTransporte_Load(object sender, EventArgs e)
        {
            clearObj();
            chkIsentoIcmsConsultaTransporte.Checked = true;
            if (flagSemaforo == 1)
            {
                conector_find_frete(auxIdNfTransportadora, auxIdNf);
            }
        }

        private void btnPesquisaTransportadoraConsultaTransporte_Click(object sender, EventArgs e)
        {
            findTransporte = new pesquisaTransportadora();
            if (findTransporte.ShowDialog(this) == DialogResult.OK)
            {
                    optionCodigoTransportadora = findTransporte.GridCodigo;
                    optionRazaoTransportadora = findTransporte.GridRazao;
                    optionCnpjTransportadora = findTransporte.GridCnpj;
                    optionIeTransportadora = findTransporte.Gridie;
                    optionInativaTransportadora = findTransporte.GridStatus;
                    optionAbreviaturaTransportadora = findTransporte.Gridabreviatura;
                    optionCodigoMunicipioTransportadora = findTransporte.Gridsped;
                    optionCepTransportadora = findTransporte.Gridcep;
                    optionEnderecoTransportadora = findTransporte.GridEndereco;
                    optionBairroTransportadora = findTransporte.Gridbairro;
                    optionNumberTransportadora = findTransporte.GridNumber;
                    optionCidadeTransportadora = findTransporte.Gridmunicipio;
                    optionUfTransportadora = findTransporte.GridUf;
                    optionComplementoTransportadora = findTransporte.GridComplemento;
                    auxIdUfTransportadora = findTransporte.GrididEstado;
                    if (optionInativaTransportadora != "" && optionInativaTransportadora != "1")
                    {
                        txtDescricaoTransportadoraConsultaTransporte.Text = optionRazaoTransportadora;
                        txtIdTransportadoraConsultaTransporte.Text = optionCodigoTransportadora;
                    }
                }
            }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (auxModalidadeFrete == 9)
            {
                this.DialogResult = DialogResult.OK;
                this.Dispose();
            }
            else
            {
                if (txtCFOPConsultaTransporte.Text != "")
                {
                    if (txtDescricaoTransportadoraConsultaTransporte.Text != "")
                    {
                        if (txtDescricaoVeiculoConsultaTransporte.Text != "")
                        {
                            if (MessageBox.Show("A revisão sobre a quantidade de itens transportados, peso e espécie foi verificada?", "Observação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                            {
                                this.DialogResult = DialogResult.OK;
                                this.Dispose();
                            }
                            else
                            {
                                tabConsultaTransporte.SelectedIndex = 1;
                                txtQttyVolumeConsultaTransporte.Select();
                            }

                        }
                        else
                        {
                            MessageBox.Show("Situação do transporte implica na escolha de algum veiculo para realização do transporte!", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            tabConsultaTransporte.SelectedIndex = 0;
                            txtIdVeiculoConsultaTransporte.Select();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Situação do transporte implica na escolha de alguma transportadora!", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        tabConsultaTransporte.SelectedIndex = 0;
                        txtIdTransportadoraConsultaTransporte.Select();
                    }
                }
                else
                {
                    MessageBox.Show("Modalidade selecionada implica na escolha de pelo menos um CFOP para designar o frete!", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tabConsultaTransporte.SelectedIndex = 0;
                    txtIdTransportadoraConsultaTransporte.Select();
                }
            }
        }

        private void consultaTransporte_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
            else if (e.KeyCode == Keys.F10)
            {
                if (auxModalidadeFrete == 9)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Dispose();
                }
                else 
                {
                    if (txtDescricaoTransportadoraConsultaTransporte.Text != "")
                    {
                        if (txtDescricaoVeiculoConsultaTransporte.Text != "")
                        {
                            if (MessageBox.Show("A revisão sobre a quantidade de itens transportados, peso e espécie foi verificada?", "Observação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                            {
                                this.DialogResult = DialogResult.OK;
                                this.Dispose();
                            }
                            else
                            {
                                tabConsultaTransporte.SelectedIndex = 1;
                                txtQttyVolumeConsultaTransporte.Select();
                            }
                            
                        }
                        else
                        {
                            MessageBox.Show("Situação do transporte implica na escolha de algum veiculo para realização do transporte!", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            tabConsultaTransporte.SelectedIndex = 0;
                            txtIdVeiculoConsultaTransporte.Select();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Situação do transporte implica na escolha de alguma transportadora!", "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        tabConsultaTransporte.SelectedIndex = 0;
                        txtIdTransportadoraConsultaTransporte.Select();
                    }
                }
                
            }
        }


        

        private void btnFindTransportadoraConsultaTransporte_Click(object sender, EventArgs e)
        {
            if (txtIdTransportadoraConsultaTransporte.Text != "")
            {
                conector_carrega_transporte(txtIdTransportadoraConsultaTransporte.Text);
                if (txtDescricaoTransportadoraConsultaTransporte.Text != "")
                {
                    txtDescricaoTransportadoraConsultaTransporte.Select();
                }
            }
            else
            {
                txtIdTransportadoraConsultaTransporte.Select();
            }
        }

        private void txtBaseCalculoConsultaTransporte_Validated_1(object sender, EventArgs e)
        {
            if (txtBaseCalculoConsultaTransporte.Text != "")
            {
                auxNumeric = Convert.ToDecimal(txtBaseCalculoConsultaTransporte.Text);//currency
                flagNumeric = String.Format("{0:F2}", auxNumeric);
                txtBaseCalculoConsultaTransporte.Text = flagNumeric.Replace(",", ".").Trim();
                optionBaseCalculoFrete = txtBaseCalculoConsultaTransporte.Text;
            }
            else
            {
                optionBaseCalculoFrete = "0.00";
                txtBaseCalculoConsultaTransporte.Text = "0.00";
            }
        }

        private void txtIcmsConsultaTransporte_Validated(object sender, EventArgs e)
        {
            if (txtIcmsConsultaTransporte.Text != "")
            {
                auxNumeric = Convert.ToDecimal(txtIcmsConsultaTransporte.Text);//currency
                flagNumeric = String.Format("{0:F2}", auxNumeric);
                txtIcmsConsultaTransporte.Text = flagNumeric.Replace(",", ".").Trim();
                optionIcmsFrete = txtIcmsConsultaTransporte.Text;
            }
            else
            {
                optionIcmsFrete = "0.00";
                txtIcmsConsultaTransporte.Text = "0.00";
            }
            if (Convert.ToDecimal(txtBaseCalculoConsultaTransporte.Text.Replace(",",".")) > 0)
            {
                try
                {
                    if (txtBaseCalculoConsultaTransporte.Text != "" && txtBaseCalculoConsultaTransporte.Text != "0.00")
                    {
                        txtAliquotaFrete.Text = (String.Format("{0:F2}", (Convert.ToDecimal(txtIcmsConsultaTransporte.Text) / Convert.ToDecimal(txtBaseCalculoConsultaTransporte.Text)) * 100)).ToString();
                    }
                }
                catch (DivideByZeroException zero)
                {
                    MessageBox.Show(zero.Message, "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void txtValorTotaldoFreteConsultaTransporte_Validated(object sender, EventArgs e)
        {
            if (txtValorTotaldoFreteConsultaTransporte.Text != "")
            {
                auxNumeric = Convert.ToDecimal(txtValorTotaldoFreteConsultaTransporte.Text);//currency
                flagNumeric = String.Format("{0:F2}", auxNumeric);
                txtValorTotaldoFreteConsultaTransporte.Text = flagNumeric.Replace(",", ".").Trim();
                optionValorTotalFrete = txtValorTotaldoFreteConsultaTransporte.Text;
            }
            else
            {
                optionValorTotalFrete = "0.00";
                txtValorTotaldoFreteConsultaTransporte.Text = "0.00";
            }
        }

        private void txtValorTotaldoFreteConsultaTransporte_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            posSeparator = txtValorTotaldoFreteConsultaTransporte.Text.IndexOf(".");
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

        private void txtIcmsConsultaTransporte_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            posSeparator = txtIcmsConsultaTransporte.Text.IndexOf(".");
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

        private void txtBaseCalculoConsultaTransporte_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            posSeparator = txtBaseCalculoConsultaTransporte.Text.IndexOf(".");
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

        private void txtPesoBrutoVolumeConsultaTransporte_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            posSeparator = txtPesoBrutoVolumeConsultaTransporte.Text.IndexOf(".");
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

        private void txtPesoLiquidoVolumeConsultaTransporte_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            posSeparator = txtPesoLiquidoVolumeConsultaTransporte.Text.IndexOf(".");
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
       private void txtIdTransportadoraConsultaTransporte_Validated_1(object sender, EventArgs e)
        {
            if (txtIdTransportadoraConsultaTransporte.Text != "")
            {
                conector_carrega_transporte(txtIdTransportadoraConsultaTransporte.Text);
            }
        }

        private void txtCFOPConsultaTransporte_Validated_1(object sender, EventArgs e)
        {
            if (txtCFOPConsultaTransporte.Text != "")
            {
                conector_find_CFOP(txtCFOPConsultaTransporte.Text);
            }
        }

        private void btnCFOPConsultaFrete_Click_1(object sender, EventArgs e)
        {
            findCFOP = new pesquisaCFOP();
            if (findCFOP.ShowDialog(this) == DialogResult.OK)
            {
                optionCFOPFrete = findCFOP.GridCodigo;

                txtCFOPConsultaTransporte.Text = optionCFOPFrete;
            }
        }

        private void chkIsentoIcmsConsultaTransporte_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsentoIcmsConsultaTransporte.Checked == true)
            {
                optionIsentoIcms = "1";
                txtBaseCalculoConsultaTransporte.ReadOnly = true;
                txtIcmsConsultaTransporte.ReadOnly = true;
                txtValorTotaldoFreteConsultaTransporte.ReadOnly = true;
                txtCFOPConsultaTransporte.ReadOnly = true;
                btnCFOPConsultaFrete.Enabled = false;
                txtAliquotaFrete.Enabled = false;
            }
            else
            {
                optionIsentoIcms = "0";
                txtAliquotaFrete.Enabled = true;
                txtAliquotaFrete.ReadOnly = false;
                txtBaseCalculoConsultaTransporte.ReadOnly = false;
                txtIcmsConsultaTransporte.ReadOnly = false;
                txtValorTotaldoFreteConsultaTransporte.ReadOnly = false;
                txtCFOPConsultaTransporte.ReadOnly = false;
                btnCFOPConsultaFrete.Enabled = true;
            }
        }

        private void cmbFreteConsultaTransporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            auxModalidadeFrete = Convert.ToInt16(cmbFreteConsultaTransporte.Text.Substring(0, 2));
            switch (auxModalidadeFrete)
            {
                case 0:
                    clearObj();
                    statusObj(true);
                    txtIdTransportadoraConsultaTransporte.Select();
                    break;
                case 1:
                    clearObj();
                    statusObj(true);
                    txtIdTransportadoraConsultaTransporte.Select();
                    break;
                case 2:
                    clearObj();
                    statusObj(true);
                    txtIdTransportadoraConsultaTransporte.Select();
                    break;
                case 9:
                    clearObj();
                    statusObj(false);
                    break;
            }
            if (cmbFreteConsultaTransporte.Text != "")
            {
                optionDescricaoTipoFrete = cmbFreteConsultaTransporte.Text;
                chkIsentoIcmsConsultaTransporte.Checked = true;
            }
        }

        private void txtIdVeiculoConsultaTransporte_Validated(object sender, EventArgs e)
        {
            if (txtIdVeiculoConsultaTransporte.Text != "")
            {
                conector_find_veiculo(txtIdVeiculoConsultaTransporte.Text);
                
            }
        }

        private void btnPesquisaVeiculoConsultaTransporte_Click(object sender, EventArgs e)
        {
            findVeiculo = new pesquisaVeiculo();
            if (findVeiculo.ShowDialog(this) == DialogResult.OK)
            {
                optionIdVeiculo = findVeiculo.GridCodigo;
                optionDescricaoVeiculo = findVeiculo.GridDescricao;
                auxGridUFVeiculo = findVeiculo.GridUf;
                optionPlacaVeiculo = findVeiculo.GridPlaca;
                if (optionDescricaoVeiculo != "")
                {
                    txtDescricaoVeiculoConsultaTransporte.Text = optionDescricaoVeiculo;
                    txtIdVeiculoConsultaTransporte.Text = optionIdVeiculo;
                }
            }
        }

        private void txtIdTransportadoraConsultaTransporte_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            //Se não for um numero e não  tecla backspace e nao for virgula, barra:
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (Char)Keys.Back)
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

        private void txtCFOPConsultaTransporte_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            //Se não for um numero e não  tecla backspace e nao for virgula, barra:
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (Char)Keys.Back)
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

        private void txtIdVeiculoConsultaTransporte_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            //Se não for um numero e não  tecla backspace e nao for virgula, barra:
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (Char)Keys.Back)
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

        private void txtPesoLiquidoVolumeConsultaTransporte_Validated(object sender, EventArgs e)
        {
            if (txtPesoLiquidoVolumeConsultaTransporte.Text != "")
            {
                auxNumeric = Convert.ToDecimal(txtPesoLiquidoVolumeConsultaTransporte.Text);//currency
                flagNumeric = String.Format("{0:F3}", auxNumeric);
                txtPesoLiquidoVolumeConsultaTransporte.Text = flagNumeric.Replace(",", ".").Trim();
                optionVolumePesoLiquido = txtPesoLiquidoVolumeConsultaTransporte.Text;
            }
            else
            {
                optionVolumePesoLiquido = "0.000";
                txtPesoLiquidoVolumeConsultaTransporte.Text = "0.000";
            }
        }

        private void txtPesoBrutoVolumeConsultaTransporte_Validated(object sender, EventArgs e)
        {
            if (txtPesoBrutoVolumeConsultaTransporte.Text != "")
            {
                auxNumeric = Convert.ToDecimal(txtPesoBrutoVolumeConsultaTransporte.Text);//currency
                flagNumeric = String.Format("{0:F3}", auxNumeric);
                txtPesoBrutoVolumeConsultaTransporte.Text = flagNumeric.Replace(",", ".").Trim();
                optionVolumePesoBruto = txtPesoBrutoVolumeConsultaTransporte.Text;
            }
            else
            {
                optionVolumePesoBruto = "0.000";
                txtPesoBrutoVolumeConsultaTransporte.Text = "0.000";
            }
        }

        private void txtQttyVolumeConsultaTransporte_Validated(object sender, EventArgs e)
        {
            if (txtQttyVolumeConsultaTransporte.Text != "")
            {
                optionVolumeQtty = txtQttyVolumeConsultaTransporte.Text;
            }
        }

        private void txtQttyVolumeConsultaTransporte_KeyPress(object sender, KeyPressEventArgs e)
        {
            posSeparator = txtQttyVolumeConsultaTransporte.Text.IndexOf(".");
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

        private void txtEspecieConsultaTransporte_Validated(object sender, EventArgs e)
        {
            if (txtEspecieConsultaTransporte.Text != "")
            {
                optionVolumeEspecie = txtEspecieConsultaTransporte.Text;
            }
        }

        private void txtMarcaVolumeConsultaTransporte_Validated(object sender, EventArgs e)
        {
            if (txtMarcaVolumeConsultaTransporte.Text != "")
            {
                optionVolumeMarca = txtMarcaVolumeConsultaTransporte.Text;
            }
        }

        private void txtNumeracaoConsultaTransporte_Validated(object sender, EventArgs e)
        {
            if (txtNumeracaoConsultaTransporte.Text != "")
            {
                optionVolumePesoNumber = txtNumeracaoConsultaTransporte.Text;
            }
        }

        private void chkIsentoIcmsConsultaTransporte_Validated(object sender, EventArgs e)
        {
            
        }

        private void lnkProcuraTransporte_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clearObj();
            statusObj(false);
            chkIsentoIcmsConsultaTransporte.Checked = true;
            cmbFreteConsultaTransporte.Text = "9 - Sem Frete";
        }

        private void lnkCancelaTransporte_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Dispose();
        }

        private void btnFindVeiculoConsultaTransporte_Click(object sender, EventArgs e)
        {
            if (txtIdVeiculoConsultaTransporte.Text != "")
            {
                conector_find_veiculo(txtIdVeiculoConsultaTransporte.Text);
            }
        }

        private void txtAliquotaFrete_Validated(object sender, EventArgs e)
        {
            if (txtAliquotaFrete.Text != "")
            {
                auxNumeric = Convert.ToDecimal(txtAliquotaFrete.Text);//currency
                flagNumeric = String.Format("{0:F2}", auxNumeric);
                txtAliquotaFrete.Text = flagNumeric.Replace(",", ".").Trim();
                optionAliquotaFrete = txtAliquotaFrete.Text;
            }
            else
            {
                optionAliquotaFrete = "0.00";
                txtAliquotaFrete.Text = "0.00";
            }
            if (Convert.ToDecimal(txtBaseCalculoConsultaTransporte.Text.Replace(",",".")) > 0)
            {
                txtIcmsConsultaTransporte.Text = (String.Format("{0:F2}", (Convert.ToDecimal(txtBaseCalculoConsultaTransporte.Text) * Convert.ToDecimal(txtAliquotaFrete.Text)) / 100)).ToString();   
            }
        }

        private void btnFindTransportadoraConsultaTransporte_Click_1(object sender, EventArgs e)
        {

        }

        private void tabConsultaTransporte_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                ProcessTabKey(true);
            }
        }

    }
}
