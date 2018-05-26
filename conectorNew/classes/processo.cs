using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Diagnostics;
using System.IO;

namespace conectorPDV001
{
    public static class processo
    {
        public static void exeProcesso(string stringExe, string stringMD5, Int16 tipo, ref string detalhes_md5)
        {
            algMd5 key = new algMd5();
            ProcessStartInfo ProcessInfo;
            Process myProcess;
            ProcessInfo = new ProcessStartInfo("cmd.exe", "/K " + stringExe);
            ProcessInfo.CreateNoWindow = true;
            ProcessInfo.UseShellExecute = true;
            ProcessInfo.WindowStyle = ProcessWindowStyle.Hidden;
            myProcess = Process.Start(ProcessInfo);
            try
            {
                if (tipo == 0)
                { myProcess.WaitForExit(); }
                else
                {
                    System.Threading.Thread.Sleep(18000);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                myProcess.Close();
            }
            if (stringMD5 != "#")
            {
                detalhes_md5 = key.retornoFileMD5(stringMD5);
            }
            if (myProcess != null)
            {
                myProcess.Close();
            }
            if (File.Exists(stringExe))
            {
                try
                {
                    File.Delete(stringExe);
                }
                catch (Exception)
                {

                }
            }
        }
    }
}
