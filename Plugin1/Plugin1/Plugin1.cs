using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Plugin;
using System.Windows.Forms;

namespace Plugin1
{
    public class Plugin1 : IPlugin
    {
        public string author
        {
            get { return "İsmail Kocacan"; }
        }

        public string description
        {
            get { return "Hesaplama İle ilgili bir plugin"; }
        }

        public bool executeSql()
        {
            MessageBox.Show("Hesaplama SQL çalıştı");
            return true;
        }

        public void finalization()
        {
            MessageBox.Show("Plugin 1 finalization oldu");
        }

        public void initialization()
        {
            MessageBox.Show("Plugin 1 initialization oldu");
        }

        public string pluginName
        {
            get { return "Hesapla 1"; }
        }
    }
}
