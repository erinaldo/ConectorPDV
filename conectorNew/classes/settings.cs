using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace conectorPDV001
{
    class settings
    {
        public settings()
        {
            try
            {
                auxConsistencia = 0;
                banco.abreConexao();
                if(banco.statusSchema() == 1)
                {
                    banco.startTransaction("conectorPDV_find_loja");
                    banco.addParametro("tipo", "1");
                    banco.addParametro("find_loja", alwaysVariables.Store);
                    banco.procedimentoRead();
                    if (banco.retornaRead().Read() == true)
                    {
                        TypeEmpresa = banco.retornaRead().GetString(14);
                        CNPJ = banco.retornaRead().GetString(3);
                        alwaysVariables.RazaoStore = banco.retornaRead().GetString(1);
                        alwaysVariables.UF = banco.retornaRead().GetString(8);
                    }
                }
                else
                {
                    auxConsistencia = 1;
                    msgInfo msg = new msgInfo(1, "IMPOSSÍVEL ESTABELECER CONEXÃO"); msg.ShowDialog();
                }
            }
            catch (Exception)
            { auxConsistencia = 1; }
            finally
            {
                banco.fechaConexao();
                if (auxConsistencia == 0)
                {
                    if (CNPJ != null)
                    {
                        alwaysVariables.CNPJ = banco.retornaRead().GetString(3);   
                    }
                }
            }
        }
        //################################################Bloco Variaveis Encapsuladas###########################################
        private dados banco = new dados();
        static string globalTypeEmpresa;
        static string auxCnpj;
        private conector_full_variable alwaysVariables = new conector_full_variable();
        private int auxConsistencia = 0;
        //################################################End Bloco Variaveis Encapsuladas#######################################
        public string TypeEmpresa
        {
            get
            {
                return globalTypeEmpresa;
            }
            set
            {
                globalTypeEmpresa = value;
            }
        }
        public string CNPJ
        {
            get
            {
                return auxCnpj;
            }
            set
            {
                auxCnpj = value;
            }
        }
    }
}
