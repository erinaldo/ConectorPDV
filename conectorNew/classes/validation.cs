using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace conectorPDV001
{
    class validation
    {
        
        public string calculo_cpf(string cpf)
        {
            //Bloco de Variaveis
        //### inicio rotina verifica cpf ###
        //## cpf invalido para cpf digitado tamanho menor que 11 ##
        int  i  = 0;
        int  j;
        int  valor  = 0;
        int  total  = 0;
        int  dv ; //digito verificador
        //'## calculo primeirov(j=0) e segundo(j=1) digito verificador ###'

            for(j = 0; j <= 1; j++)
        {
            total = 0;
            for (i = 0; i < (9+j); i++)
            {
                valor = Convert.ToInt32(cpf.Substring(i, 1));
                total = total + (valor * (10 - i + j));
            }
            
                if ((total % 11) < 2)
                {dv=0;}
                else { dv = (11 - (total % 11)); }
                if (Convert.ToInt32(cpf.Substring(9 + j,1))!= dv)
                {
                    return "0";
                }
                
        }
            return "1";
        }
        public string calculo_cgc(string cgc)
        {
            int i = 1;
            int j;
            int valor = 0;
            int total = 0; //'total soma que sera divida por 11'
            int dv; //valor da string na posicao auxcnpj e digito verificador'
            int[] pesos = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };//valores do pesos
            do
            {
                total = 0;
                for (j = 0; j < (13-i); j++)
                {
                    valor = Convert.ToInt32(cgc.Substring(j, 1));
                    total = total + pesos[j + i] * valor;

                }
                if ((total % 11) < 2)
                {
                    dv = 0;
                }
                else { dv = (11 - (total % 11)); }
                if(Convert.ToInt32(cgc.Substring(13-i,1)) != dv)
                {
                    return "0";
                }
                i--;
            } while (i >= 0);
            return "1";
        }
    }
}
