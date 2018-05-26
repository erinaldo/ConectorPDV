using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
// Implementações lib's
using System.IO;
using System.Security.Cryptography;

namespace conectorPDV001
{
    class crypt
    {
        public crypt()
        {
            cspp.KeyContainerName = keyName;
            rsa = new RSACryptoServiceProvider(cspp);
            rsa.PersistKeyInCsp = true;
        }

        public crypt(string crz, string cro)
        {
            cspp.KeyContainerName = keyName;
            rsa = new RSACryptoServiceProvider(cspp);
            rsa.PersistKeyInCsp = true;
            fiscal_last_reducao_crz = crz;
            fiscal_last_reducao_cro = cro;
        }
        //###############################################VARIÁVEL CONSTANTE#########################################################
        private conector_full_variable alwaysVariables = new conector_full_variable();
        const string folderECF = @"c:\conector\ECF\";
        const string folderMFDGrand = @"c:\conector\MFD\Grand\";
        const string KeyFile = @"c:\conector\MFD\Grand\secret.pem";
        const string keyName = "F1AV10 R063R10 D@ 51LV4 - C0N3CT0R";
        //###############################################VARIÁVEL CONSTANTE#########################################################
        //###################################################END CONSTANTE##########################################################
        //###############################################VARIÁVEL ENCAPSULADAS######################################################
        // Declare CspParmeters and RsaCryptoServiceProvider
        // objects with global scope of your Form class.
        private acesso banco = new acesso();
        private algMd5 key = new algMd5();
        private conectorExport0202 export;
        private CspParameters cspp = new CspParameters();
        private RSACryptoServiceProvider rsa;
        string retorno = "";
        private List<string> import = new List<string>();
        List<string> mensagemLinha = new List<string>();
        private conexaoECF functionECF = new conexaoECF();
        private int auxConsistencia = 0;
        private string fiscal_GT_compare = new string('\x20', 18);
        private int fiscal_retorno = 0;
        private int i, j, countField, countRows; //variavel do loop.
        private string LinhaArquivo = "";
        private string fiscal_numero_serie = new string('\x20', 20);
        private string fiscal_GT_Crypt = new string('\x20', 20);
        private string fiscal_last_reducao_crz = new string('\x20', 4);
        private string fiscal_last_reducao_cro = new string('\x20', 4);
        private string fiscal_MSG = "";

        private static void AddText(FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }
        void exportSecret()
        {
            Directory.CreateDirectory(folderMFDGrand);
            if (File.Exists(KeyFile))
            {
                File.Delete(KeyFile);
            }
            StreamWriter sw = new StreamWriter(KeyFile, false);
            sw.Write(rsa.ToXmlString(false));
            sw.Close();
        }
        string importSecret()
        {
            string retorno="";
            StreamReader sr = new StreamReader(KeyFile);
            cspp.KeyContainerName = keyName;
            rsa = new RSACryptoServiceProvider(cspp);
            string keytxt = sr.ReadToEnd();
            rsa.FromXmlString(keytxt);
            rsa.PersistKeyInCsp = true;
            retorno = cspp.KeyContainerName;
            sr.Close();
            return retorno;
        }
        public void encryptFile(string inFile,string name, Int16 tipo)
        {
            string path = @"c:\temp";
            if (tipo == 0)
            {
                path = folderMFDGrand + name + ".txt";
            } else if (tipo == -1)
            {
                path = "C:\\conector\\MFD\\arquivos\\" + name + ".txt";
            }
            else
            {
                path = inFile;
            }
            // Delete the file if it exists.
            if (File.Exists(path) && tipo == 0)
            {
                File.Delete(path);

            }
            if (tipo == 0)
            {
                
                using (FileStream fs = File.Create(path))
                {
                    AddText(fs, inFile);
                    AddText(fs, "\n");
                    AddText(fs, alwaysVariables.Serie);
                    //for (int i = 1; i < inFile.Length; i++)
                    //{
                      //  AddText(fs, Convert.ToChar(i).ToString());
                    //}
                }
            }

            // Create instance of Rijndael for
            // symetric encryption of the data.
            RijndaelManaged rjndl = new RijndaelManaged();
            rjndl.KeySize = 256; //Obtém ou define o tamanho, em bits, a chave secreta usada pelo algoritmo simétrico
            rjndl.BlockSize = 256;  //Obtém ou define o tamanho do bloco, em bits, a operação de criptografia.
            rjndl.Mode = CipherMode.CBC;
            ICryptoTransform transform = rjndl.CreateEncryptor();

            // Use RSACryptoServiceProvider to
            // enrypt the Rijndael key.
            // rsa is previously instantiated: 
            rsa = new RSACryptoServiceProvider(cspp);
            byte[] keyEncrypted = rsa.Encrypt(rjndl.Key, false);

            // Create byte arrays to contain
            // the length values of the key and IV.
            byte[] LenK = new byte[4];
            byte[] LenIV = new byte[4];

            int lKey = keyEncrypted.Length;
            LenK = BitConverter.GetBytes(lKey);
            int lIV = rjndl.IV.Length; //Obtém ou define o vetor de inicialização (IV) para o algoritmo simétrico
            LenIV = BitConverter.GetBytes(lIV);

            // Write the following to the FileStream
            // for the encrypted file (outFs):
            // - length of the key
            // - length of the IV
            // - ecrypted key
            // - the IV
            // - the encrypted cipher content

            int startFileName = inFile.LastIndexOf("\\") + 1;
            // Change the file's extension to ".enc"
            //string outFile = folderSlave + path.Substring(startFileName, path.LastIndexOf(".") - startFileName) + ".enc";
            string outFile = @"c:\temp\.temp.enc";
            if (tipo == 0)
            {
                outFile = path.Substring(startFileName, path.LastIndexOf(".") - startFileName) + ".enc";
            }
            else if (tipo == -1)
            {
                outFile = path.Substring(0, path.LastIndexOf(".") ) + ".enc";
            }
            else
            {
                outFile = path.Replace(".txt", ".enc");
            }


            using (FileStream outFs = new FileStream(outFile, FileMode.Create))
            {

                outFs.Write(LenK, 0, 4);
                outFs.Write(LenIV, 0, 4);
                outFs.Write(keyEncrypted, 0, lKey);
                outFs.Write(rjndl.IV, 0, lIV);

                // Now write the cipher text using
                // a CryptoStream for encrypting.
                using (CryptoStream outStreamEncrypted = new CryptoStream(outFs, transform, CryptoStreamMode.Write))
                {

                    // By encrypting a chunk at
                    // a time, you can save memory
                    // and accommodate large files.
                    int count = 0;
                    int offset = 0;

                    // blockSizeBytes can be any arbitrary size.
                    int blockSizeBytes = rjndl.BlockSize / 8;
                    byte[] data = new byte[blockSizeBytes];
                    int bytesRead = 0;

                    using (FileStream inFs = new FileStream(path, FileMode.Open))
                    {
                        do
                        {
                            count = inFs.Read(data, 0, blockSizeBytes);
                            offset += count;
                            outStreamEncrypted.Write(data, 0, count);
                            bytesRead += blockSizeBytes;
                        }
                        while (count > 0);
                        inFs.Close();
                    }
                    outStreamEncrypted.FlushFinalBlock();
                    outStreamEncrypted.Close();
                }
                outFs.Close();
                //readFile(path);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
        }
        public string descryptFile(string path, string name)
        {
            string retorno = "";
            // Create instance of Rijndael for
            // symetric decryption of the data.
            RijndaelManaged rjndl = new RijndaelManaged();
            rjndl.KeySize = 256;
            rjndl.BlockSize = 256;
            rjndl.Mode = CipherMode.CBC; //Obtém ou define o modo de operação do algoritmo simétrico. 

            // Create byte arrays to get the length of
            // the encrypted key and IV.
            // These values were stored as 4 bytes each
            // at the beginning of the encrypted package.
            byte[] LenK = new byte[4];
            byte[] LenIV = new byte[4];

            // Consruct the file name for the decrypted file.
            string outFile = path.Substring(0, path.LastIndexOf(".")) + ".txt";
            try
            {

                // Use FileStream objects to read the encrypted
                // file (inFs) and save the decrypted file (outFs).
                using (FileStream inFs = new FileStream(path.Substring(0, path.LastIndexOf(".")) + ".enc", FileMode.Open))
                {

                    inFs.Seek(0, SeekOrigin.Begin);
                    inFs.Seek(0, SeekOrigin.Begin);
                    inFs.Read(LenK, 0, 3);
                    inFs.Seek(4, SeekOrigin.Begin);
                    inFs.Read(LenIV, 0, 3);

                    // Convert the lengths to integer values.
                    int lenK = BitConverter.ToInt32(LenK, 0);
                    int lenIV = BitConverter.ToInt32(LenIV, 0);

                    // Determine the start postition of
                    // the ciphter text (startC)
                    // and its length(lenC).
                    int startC = lenK + lenIV + 8;
                    int lenC = (int)inFs.Length - startC;

                    // Create the byte arrays for
                    // the encrypted Rijndael key,
                    // the IV, and the cipher text.
                    byte[] KeyEncrypted = new byte[lenK];
                    byte[] IV = new byte[lenIV];

                    // Extract the key and IV
                    // starting from index 8
                    // after the length values.
                    inFs.Seek(8, SeekOrigin.Begin);
                    inFs.Read(KeyEncrypted, 0, lenK);
                    inFs.Seek(8 + lenK, SeekOrigin.Begin).ToString().TrimEnd();
                    inFs.Read(IV, 0, lenIV);
                    //Directory.CreateDirectory(path.Substring(0, path.LastIndexOf(".")));
                    // Use RSACryptoServiceProvider
                    // to decrypt the Rijndael key.
                    //rsa = new RSACryptoServiceProvider();
                    byte[] KeyDecrypted = rsa.Decrypt(KeyEncrypted, false);
                    // Decrypt the key.
                    ICryptoTransform transform = rjndl.CreateDecryptor(KeyDecrypted, IV);

                    // Decrypt the cipher text from
                    // from the FileSteam of the encrypted
                    // file (inFs) into the FileStream
                    // for the decrypted file (outFs).
                    using (FileStream outFs = new FileStream(outFile, FileMode.Create))
                    {

                        int count = 0;
                        int offset = 0;

                        // blockSizeBytes can be any arbitrary size.
                        int blockSizeBytes = rjndl.BlockSize / 8;
                        byte[] data = new byte[blockSizeBytes];


                        // By decrypting a chunk a time,
                        // you can save memory and
                        // accommodate large files.

                        // Start at the beginning
                        // of the cipher text.
                        inFs.Seek(startC, SeekOrigin.Begin);
                        using (CryptoStream outStreamDecrypted = new CryptoStream(outFs, transform, CryptoStreamMode.Write))
                        {
                            do
                            {
                                count = inFs.Read(data, 0, blockSizeBytes);
                                offset += count;
                                outStreamDecrypted.Write(data, 0, count);
                            }
                            while (count > 0);

                            outStreamDecrypted.FlushFinalBlock();
                            outStreamDecrypted.Close();
                        }
                        outFs.Close();
                    }
                    //Open the stream and read it back.
                    using (FileStream fs = File.OpenRead(outFile))
                    {
                        byte[] b = new byte[1024];
                        UTF7Encoding temp = new UTF7Encoding(true);
                        while (fs.Read(b, 0, b.Length) > 0)
                        {
                            retorno += temp.GetString(b);
                        }
                    
                    }


                    if (File.Exists(folderMFDGrand + name + ".txt"))
                    {
                        alwaysVariables.ArqTotal = new List<string>();
                        using (StreamReader texto = new StreamReader(folderMFDGrand + name + ".txt", System.Text.Encoding.Default, true))
                        {
                            while ((LinhaArquivo = texto.ReadToEnd()) != null)
                            {
                                //alwaysVariables.ArqTotal.Add(LinhaArquivo);
                                string rte = LinhaArquivo;
                                if (name == "\\MD5")
                                {
                                    rte = rte.Replace("\r\n","").Trim();
                                }
                                int i = 0;
                                int j = 0;
                                int count = rte.Length;
                                int inverso = count / 42;
                                while (inverso > j)
                                {
                                    alwaysVariables.ArqTotal.Add(rte.Substring(i, 42));
                                    i = i + 42;
                                    j++;
                                }
                                if (rte.Length == 82)
                                {//Preenche o CRZ, CRO e VENDA BRUTA do arquivo arquivo GT
                                    alwaysVariables.ArqTotal.Add(rte.Substring(44, 38));
                                }
                                break;
                            }
                        }
                        if (name != "\\MD5")
                        {
                            System.IO.File.Copy(folderMFDGrand + name + ".txt", folderMFDGrand + "temp" + ".txt");   
                            File.Delete(folderMFDGrand + name + ".txt");   
                        }
                    }


                    inFs.Close();
                }
            }
            catch (Exception erro)
            {
                File.Delete(folderMFDGrand + "\\temp" + ".txt");
                if("\\MD5" == name)
                {
                    File.Delete(folderMFDGrand + "MD5.enc");
                    msgInfo msg = new msgInfo(1, "CARO USUÁRIO - " + "ELIMINANDO FONTE CORROMPIDA. \n"); msg.ShowDialog();
                }
                return  "000000000000000000000";//msgInfo msg = new msgInfo(1,"Caro Cliente - " + "ERRO FATAL GRANDE TOTAL INVÁLIDO! SOLICITE A AUTORIZAÇÃO PARA GERAÇÃO DO GRANDE TOTAL. \n"+ "\n  ANALISE: " + erro.ToString()); msg.ShowDialog();
            }
            if (retorno == "")
            {
                File.Delete(folderMFDGrand + "\\temp" + ".txt");
                retorno = "000000000000000000000";
            }
            
            return retorno;
        }
        public bool conectorPDV_aut_grandTotal(string gt)
        {
            alwaysVariables.Gt_Valido = false;
            File.Delete(folderMFDGrand + "\\temp" + ".txt");
            if (File.Exists(folderMFDGrand + "\\grandFullPDV" + ".enc"))
            {
                descryptFile(folderMFDGrand + "\\grandFullPDV" + ".enc", "\\grandFullPDV");
                if (File.Exists(folderMFDGrand + "\\temp" + ".txt"))
                {
                    retorno = "";
                    import = new List<string>();
                    //Conversao de Carateres
                    using (StreamReader texto = new StreamReader(folderMFDGrand + "\\temp.txt", System.Text.Encoding.Default, true))
                    {
                        while ((LinhaArquivo = texto.ReadToEnd()) != null)
                        {
                            string rte = LinhaArquivo;
                            int i = 0;
                            int j = 0;
                            int count = rte.Length;
                            int inverso = count / 42;
                            while (inverso > j)
                            {
                                import.Add(rte.Substring(i, 42));
                                i = i + 42;
                                j++;
                            }
                            break;
                        }
                    }

                    
                    using (FileStream fs = File.OpenRead(folderMFDGrand + "\\temp" + ".txt"))
                    {
                        fiscal_GT_compare = new string('\x20', 18);
                        byte[] b = new byte[1024];
                        UTF7Encoding temp = new UTF7Encoding(true);
                        while (fs.Read(b, 0, b.Length) > 0)
                        {
                            retorno = temp.GetString(b).Replace("\0", "").Trim();
                            functionECF.conectorECF_GrandeTotalDescriptografado(alwaysVariables.ModeloEcf, alwaysVariables.ArqTotal[j].Substring(24, alwaysVariables.ArqTotal[j].Length - 24), ref fiscal_GT_compare, ref fiscal_MSG, ref fiscal_retorno, alwaysVariables.ECF_Ligada);
                            //fiscal_retorno = conectorECF.Bematech_FI_GrandeTotalDescriptografado(import[j].Substring(24, import[j].Length - 24), ref fiscal_GT_compare);

                            if (gt == fiscal_GT_compare)
                            {
                                alwaysVariables.Gt_Valido = true;
                            }
                        }
                        fs.Close();
                    }
                }
                File.Delete(folderMFDGrand + "\\temp" + ".txt");
            }
            else
            {
                if (Convert.ToDouble(gt) == Convert.ToDouble(alwaysVariables.GT_BANCO) && fiscal_last_reducao_cro == alwaysVariables.GT_CRO &&
                    fiscal_last_reducao_crz == alwaysVariables.GT_CRZ)
                {
                    fiscal_GT_Crypt = new string('\x20', 20);
                    functionECF.conectorECF_GrandeTotal_Crypt(alwaysVariables.ModeloEcf, ref fiscal_GT_Crypt, ref fiscal_MSG, ref fiscal_retorno, alwaysVariables.ECF_Ligada);
                    //fiscal_retorno = conectorECF.Bematech_FI_GrandeTotalCriptografado(ref fiscal_GT_Crypt);
                    conectorPDV_serie(alwaysVariables.PAF_total + "\\grandFullPDV" + ".txt", "0");
                    File.Delete(alwaysVariables.PAF_total + "\\grandFullPDV" + ".txt");
                    if (File.Exists(folderMFDGrand + "\\grandFullPDV" + ".enc"))
                    {
                        msgInfo msg = new msgInfo(1, "Caro Usuário: Foi identifica uma inconsistencia com o grande e reparado pos conferencia entre hardware e sistema, com isso você volta a operar sem problemas. " + folderMFDGrand + "\\grandFullPDV" + ".enc"); msg.ShowDialog();
                    }
                    alwaysVariables.Gt_Valido = true;
                }
            }

            return alwaysVariables.Gt_Valido;
        }
        public bool conectorPDV_aut_serial()
        {
            alwaysVariables.Serie_Valido = false;
               if (File.Exists(folderMFDGrand + "\\MD5" + ".enc"))
                {
                    descryptFile(folderMFDGrand + "\\MD5" + ".enc", "\\MD5");
                    if (File.Exists(folderMFDGrand + "\\MD5" + ".txt"))
                    {
                        retorno = "";
                        string mensagem = "";
                        using (FileStream fs = File.OpenRead(folderMFDGrand + "\\MD5" + ".txt"))
                        {
                            byte[] b = new byte[1024];
                            UTF7Encoding temp = new UTF7Encoding(true);
                            while (fs.Read(b, 0, b.Length) > 0)
                            {
                                retorno = temp.GetString(b).Replace("\0", "").Trim();
                                StringReader strReader = new StringReader(retorno);

                                while ((mensagem = strReader.ReadLine()) != null)
                                {
                                    if (alwaysVariables.Serie_Hash == mensagem && alwaysVariables.Serie_Hash_Hard == mensagem)
                                    {
                                        alwaysVariables.Serie_Valido = true;//Verifica serie impressora valido
                                        break;
                                    }
                                    if (alwaysVariables.Serie.Replace("\"", "").Trim() == "\"EMULADOR       \"".Replace("\"", "").Trim())
                                    {
                                        alwaysVariables.Serie_Valido = true;//Verifica serie impressora valido
                                        break;
                                    }
                                }
                            }
                            fs.Close();
                        }
                    }
                    File.Delete(folderMFDGrand + "\\MD5" + ".txt");
                }
                else
                {
                   conectorPDV_aut(alwaysVariables.CNPJ, folderMFDGrand + "\\MD5" + ".txt"); //cryptografia.encryptFile(alwaysVariables.MD5_Main, "\\MD5", 0);
                }
                return alwaysVariables.Serie_Valido;
        }
        public bool conectorPDV_verifica_serial()
        {
            alwaysVariables.Serie_Valido = false;
            if (File.Exists(folderMFDGrand + "\\MD5" + ".enc"))
            {
                string retorno = descryptFile(folderMFDGrand + "\\MD5" + ".enc", "\\MD5");
                if (retorno != "" && retorno != null && retorno != "" && retorno != "000000000000000000000")
                {
                    if (File.Exists(folderMFDGrand + "\\MD5" + ".enc"))
                    {

                        string mensagem = "";
                        using (FileStream fs = File.OpenRead(folderMFDGrand + "\\MD5" + ".txt"))
                        {
                            byte[] b = new byte[1024];
                            UTF7Encoding temp = new UTF7Encoding(true);
                            while (fs.Read(b, 0, b.Length) > 0)
                            {
                                retorno = temp.GetString(b).Replace("\0", "").Trim();
                                StringReader strReader = new StringReader(retorno);

                                while ((mensagem = strReader.ReadLine()) != null)
                                {
                                    if (alwaysVariables.Serie_Hash == mensagem && alwaysVariables.Serie_Hash_Hard == mensagem)
                                    {
                                        alwaysVariables.Serie_Valido = true;//Verifica serie impressora valido
                                    }
                                    if (alwaysVariables.Serie.Replace("\"", "").Trim() == "\"EMULADOR       \"".Replace("\"", "").Trim())
                                    {
                                        alwaysVariables.Serie_Valido = true;//Verifica serie impressora valido   
                                    }
                                }
                            }
                            fs.Close();
                        }
                        /*using (FileStream fs = File.OpenRead(folderMFDGrand + "\\MD5" + ".txt"))
                        {
                            byte[] b = new byte[1024];
                            UTF7Encoding temp = new UTF7Encoding(true);
                            retorno = temp.GetString(b).Replace("\0", "").Trim();
                            StringReader strReader = new StringReader(retorno);
                            
                            while (fs.Read(b, 0, b.Length) > 0)
                            {
                                if (alwaysVariables.Serie_Hash == retorno && alwaysVariables.Serie_Hash_Hard == retorno)
                                {
                                    alwaysVariables.Serie_Valido = true;
                                }
                            }
                            fs.Close();
                        }*/
                    }
                    else
                    {
                        return alwaysVariables.Serie_Valido = false; //erro de registro não conseguiu descryt
                    }
                }
                else
                {
                    File.Delete(folderMFDGrand + "\\MD5" + ".txt");
                    File.Delete(folderMFDGrand + "\\temp" + ".txt");
                    return alwaysVariables.Serie_Valido = false;
                }
                File.Delete(folderMFDGrand + "\\MD5" + ".txt");
                File.Delete(folderMFDGrand + "\\temp" + ".txt");
            }
            else
            {
                return alwaysVariables.Serie_Valido = false;
            }
            return alwaysVariables.Serie_Valido;
        }
        private bool conectorPDV_aut(string cnpj, string caminho)
        {
            int auxConsistencia = 0;
            try
            {
                banco.abreConexao();
                banco.startTransaction("conectorPDV_aut");
                banco.addParametro("find", cnpj);
                banco.procedimentoSet();
            }
            catch (Exception erro)
            { MessageBox.Show(erro.Message, "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    StreamWriter sw = new StreamWriter(caminho, true, Encoding.ASCII);
                    countField = banco.retornaSet().Tables[0].Columns.Count;
                    countRows = banco.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        string retorno = "";
                        for (int i = 0; i < countRows; i++)//Linha
                        {
                            for (int j = 0; j < countField; j++) //Coluna
                            {
                                retorno=key.GetMd5Sum(banco.retornaSet().Tables[0].Rows[i][j].ToString().Substring(4, 20));
                                sw.Write(retorno + "\r\n");
                                if (alwaysVariables.Serie_Hash == retorno && alwaysVariables.Serie_Hash_Hard == retorno)
	                            {
		                                alwaysVariables.Serie_Valido = true;
                                }
                            }
                        }
                    }
                    sw.Close();
                }
                banco.fechaConexao();
                if (auxConsistencia == 0)
                {
                    encryptFile(alwaysVariables.PAF_total + "\\MD5" + ".txt", "MD5", 2);
                }
            }
            if (File.Exists(caminho))
            {
                File.Delete(caminho);
            }
            return alwaysVariables.Serie_Valido;
        }

        public bool conectorPDV_GT(string mov, string store, string cx, string sq, string operador, string printer)
        {
            int auxConsistencia = 0;
            bool valida = false;
            try
            {
                banco.abreConexao();
                banco.startTransaction("conectorPDV_GT");
                banco.addParametro("mov", mov);
                banco.addParametro("store", store.Replace("\0", "").Trim());
                banco.addParametro("cx", cx.Replace("\0", "").Trim());
                banco.addParametro("sq", sq);
                banco.addParametro("operador", operador);
                banco.addParametro("printer", printer.Replace("\0","").Trim());
                banco.procedimentoRead();
                if (banco.retornaRead().Read() == true)
                {
                    alwaysVariables.GT_BANCO = banco.retornaRead().GetString(0);
                    alwaysVariables.GT_CRZ = banco.retornaRead().GetString(1);
                    alwaysVariables.GT_CRO = banco.retornaRead().GetString(2);
                }
            }
            catch (Exception erro)
            { 
                MessageBox.Show(erro.Message, "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); 
                auxConsistencia = 1; valida = false; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    valida = true;
                }
                banco.fechaConexao();
            }
            return valida;
        }

        private string readFile(string path)
        {
            string retorno = "";
            using (FileStream fs = File.OpenRead(path))
            {
                byte[] b = new byte[1024];
                UTF8Encoding temp = new UTF8Encoding(true);
                while (fs.Read(b, 0, b.Length) > 0)
                {
                    retorno = (temp.GetString(b));
                }
            }
            return retorno;
        }
        public void conectorPDV_serie(string caminho, string ms)
        {
            StreamWriter sw = new StreamWriter(caminho, true, Encoding.ASCII);
            fiscal_GT_Crypt = new string('\x20', 20);
            fiscal_numero_serie = new string('\x20', 20);
            string fiscal_NumReducoes = new string('\x20', 4);
            functionECF.conectorECF_numero_serie(alwaysVariables.ModeloEcf, ref fiscal_numero_serie, ref fiscal_MSG, ref fiscal_retorno, alwaysVariables.ECF_Ligada);
            //fiscal_retorno = conectorECF.Bematech_FI_NumeroSerieMFD(ref fiscal_numero_serie);
            functionECF.conectorECF_GrandeTotal_Crypt(alwaysVariables.ModeloEcf, ref fiscal_GT_Crypt, ref fiscal_MSG, ref fiscal_retorno, alwaysVariables.ECF_Ligada);
            //fiscal_retorno = conectorECF.Bematech_FI_GrandeTotalCriptografado(ref fiscal_GT_Crypt);
            string fiscal_vendaBruta = new string('\x20', 18);
            string fiscal_reducao_cro = new string('\x20', 4);

            functionECF.conectorECF_VendaBruta(alwaysVariables.ModeloEcf, ref fiscal_vendaBruta, ref fiscal_MSG, ref fiscal_retorno, alwaysVariables.ECF_Ligada);
            //fiscal_retorno = conectorECF.Bematech_FI_VendaBruta(ref fiscal_vendaBruta);
            //CRZ
            functionECF.conectorECF_NumeroReducoes(alwaysVariables.ModeloEcf, ref fiscal_NumReducoes, ref fiscal_MSG, ref fiscal_retorno, alwaysVariables.ECF_Ligada);
            //fiscal_retorno = conectorECF.Bematech_FI_NumeroReducoes(ref fiscal_NumReducoes);
            //CRO
            functionECF.conectorECF_NumeroIntervencoes(alwaysVariables.ModeloEcf, ref fiscal_reducao_cro, ref fiscal_MSG, ref fiscal_retorno, alwaysVariables.ECF_Ligada);
            //fiscal_retorno = conectorECF.Bematech_FI_NumeroIntervencoes(ref fiscal_reducao_cro);

            auxConsistencia = 0;
            countField = 0;
            countRows = 0;
            try
            {
                if (banco.statusSchema() == 1)
                {
                    return;
                }
                banco.abreConexao();
                banco.startTransaction("conectorPDV_serie");
                if (fiscal_retorno == 1)
                {
                    banco.procedimentoSet();
                }
                else
                {
                    auxConsistencia = fiscal_retorno;
                }


            }
            catch (Exception erro)
            {
                auxConsistencia = 1;
            }
            finally
            {
                export = new conectorExport0202();
                countField = banco.retornaSet().Tables[0].Columns.Count;
                countRows = banco.retornaSet().Tables[0].DefaultView.Count;
                if (countRows > 0)
                {
                    string[,] matrizGrd = new string[countRows, countField];
                    for (int i = 0; i < countRows; i++)//Linha
                    {
                        for (int j = 0; j < countField; j++) //Coluna
                        {
                            matrizGrd[i, j] = Convert.ToString(banco.retornaSet().Tables[0].Rows[i][j]);
                        }
                        sw.Write(export.registro_serie(matrizGrd[i, 0], matrizGrd[i, 1], "0" , fiscal_NumReducoes , fiscal_reducao_cro, fiscal_vendaBruta + "\r\n"));
                    }
                }
                banco.fechaConexao();
                if (auxConsistencia == 0)
                {
                    sw.Close();
                    mensagemLinha = new List<string>();
                    using (StreamReader texto = new StreamReader(caminho))
                    {
                        string mensagem = "";
                        while ((mensagem = texto.ReadLine()) != null)
                        {
                            if (mensagem.Substring(4, 20) == fiscal_numero_serie)
                            {
                                mensagem = mensagem.Substring(0, 24) + fiscal_GT_Crypt;
                                mensagemLinha.Add(mensagem);
                            }
                            else
                            {
                                mensagemLinha.Add(mensagem);
                            }
                        }

                        texto.Close();

                        if (File.Exists(folderMFDGrand + "\\grandFullPDV" + ".txt"))
                        {
                            File.Delete(folderMFDGrand + "\\grandFullPDV" + ".txt");
                        }
                        StreamWriter final = new StreamWriter(folderMFDGrand + "\\grandFullPDV" + ".txt", true, Encoding.Default);

                        for (int j = 0; j < mensagemLinha.Count; j++)
                        {
                            final.Write(export.registro_serie_unica(mensagemLinha[j], fiscal_NumReducoes, fiscal_reducao_cro, fiscal_vendaBruta + "\r\n"));
                        }
                        final.Close();

                        encryptFile(alwaysVariables.PAF_total + "\\grandFullPDV" + ".txt", "grandFullPDV", 2);
                        if (ms == "1")
                        {
                            msgInfo msg = new msgInfo(1, "Caro Usuário: Arquivo gerado, caminho: " + folderMFDGrand + "\\grandFullPDV" + ".enc"); msg.ShowDialog();
                        }
                    }
                }
            }
            sw.Close();
        }
    }
}
