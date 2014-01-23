using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Plugin;

namespace Plugin2
{
    public class Plugin2 : IPlugin
    {
        public string author
        {
            get { return "Kemal Bayat"; }
        }

        public string description
        {
            get { return "Kemal Bayatın yazdığı bayat plugin"; }
        }

        public bool executeSql()
        {
            MessageBox.Show("Kemalin kodları çalıştı");
            return true;
        }

        public void finalization()
        {
            MessageBox.Show("Plugin 2 finalization oldu");
        }

        public void initialization()
        {
            MessageBox.Show("Plugin 2 initialization oldu");
        }

        public string pluginName
        {
            get { return "Hesapla 2"; }
        }
    }
}
