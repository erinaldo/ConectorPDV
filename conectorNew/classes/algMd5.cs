using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Reflection;

namespace conectorPDV001
{
    class algMd5
        
    {
        private settings hash = new settings();

        public string retornoFileMD5(string caminho)
        {
            string retorno = "";
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(caminho))
                {
                    md5.ComputeHash(Encoding.Default.GetBytes(caminho));
                    //md5.ComputeHash(stream);
                    retorno = BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower();
                }
                return retorno;
            }
        }
        public string getHash
        {
            get
            {
                return getMd5(hash.CNPJ);
            }
            set
            {
               hash.CNPJ = value;
            }
        }

        public string GetMd5Sum(string str)
        {
            byte[] input = ASCIIEncoding.ASCII.GetBytes(str);
            byte[] output = MD5.Create().ComputeHash(input);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < output.Length; i++)
            {
                sb.Append(output[i].ToString("X2"));
            }

            return sb.ToString();
        }

        static string getMd5(string input)
        {
            //Cria a instancia MD5CryptoServiceProvider object
            MD5 senha = MD5.Create();
            int count;
            //Cria uma string de Array
            byte[] data = senha.ComputeHash(Encoding.Default.GetBytes(input));
            //Concateno o Array em Hexadecimal
            StringBuilder coleta = new StringBuilder();
            for ( count = 0; count < data.Length; count++)
            {
                coleta.Append(data[count].ToString("x2"));
            }
            //retorno o Hash
            return coleta.ToString();
        }
    }
}
