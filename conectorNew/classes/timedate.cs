using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Globalization;
using System.Threading;

namespace conectorPDV001
{
    class timedate
    {
        public timedate()
        {
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en");
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;
        }
    }
}
