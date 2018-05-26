using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace conectorPDV001
{
    class crediario
    {
        private decimal valorPrincipal; //P é o valor principal
        public decimal ValueMain
        {
            get
            {
                return valorPrincipal;
            }
            set
            {
                valorPrincipal = value;
            }
        }
        private decimal indice; //indice é a taxa unitária 
        public decimal Taxa
        {
            get
            {
                return indice;
            }
            set
            {
                indice = value;
            }
        }
        private Int32 periodo = 0; //dias, mes, ano
        public Int32 Periodo
        {
            get
            {
                return periodo;
            }
            set
            {
                periodo = value;
            }
        }
        private decimal returnJurosSingle = 0;
        public decimal JurosSingle   
        {
            get
            {
                return returnJurosSingle;
            }
            set
            {
                returnJurosSingle = value;
            }
        }

        private decimal valueSingle = 0;
        public decimal Single
        {
            get
            {
                return valueSingle;
            }
            set
            {
                valueSingle = value;
            }
        }

        private decimal valueMora = 0;
        public decimal Mora
        {
            get
            {
                return valueMora;
            }
            set
            {
                valueMora = value;
            }
        }

        private decimal returnJurosMora = 0;
        public decimal JurosMora
        {
            get
            {
                return returnJurosMora;
            }
            set
            {
                returnJurosMora = value;
            }
        }
        public decimal conector_calc_single(decimal p, decimal i, Int32 n)
        {
            valorPrincipal = p;
            indice = i;
            periodo = n;
            Single = (decimal)Math.Pow((double)(1 + (i / 100)), (double)periodo);
            //returnJurosSingle = valorPrincipal * (decimal)Math.Pow((double)(1 + (i / 100)), (double)periodo);
            return Single;
        }
        public decimal conector_calc_mora(decimal p, decimal i)
        {
            valorPrincipal = p;
            indice = i;
            Mora = ((valorPrincipal * indice) / 100);
            //returnJurosMora = valorPrincipal + ((valorPrincipal * indice) / 100);
            return Mora;
        }
        public decimal conector_sum_acrescimo()
        {
            return Mora + Single;
        }
        public decimal conector_sum_reajustado(decimal p)
        {
            return p + Mora + Single;
        }
        public decimal conector_sum_reajustado(decimal p, decimal d)
        {
            return (p + Mora + Single) - d;
        }
        public decimal conector_margem_discount(decimal discount, decimal parcela, decimal acrescimo)
        {
            return (discount/(parcela + acrescimo))*100;
        }
        public decimal conector_troco(decimal v, decimal m)
        {
            decimal resto = 0;
            resto = m - v;
            if (resto  <= 0)
            {
                return 0;
            }
            else
            {
                return resto;
            }
        }
        public decimal conector_restante(decimal v, decimal m)
        {
            decimal restante = 0;
            restante = v - m;
            if (restante <= 0)
            {
                return 0;
            }
            else
            {
                return restante;
            }
        }
    }
}
