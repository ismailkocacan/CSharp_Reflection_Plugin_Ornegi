using Plugin;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace WindowsFormsHostApplication
{


    public partial class frmMain : Form
    {
        // IPlugin interface tip özelliğine sahip,özelleştirilmiş bir PluginButton :)
        // Farklı bir görsel sınıfda olabilirdi... menuıtem vs
        public class PluginButton : Button
        {
            public IPlugin plugin { get; set; }
        }

        private string pluginDir = Application.StartupPath + "\\plugins";

        PluginManager pluginManager;

        public frmMain()
        {
            InitializeComponent();
        }

        public PluginButton addButton(IPlugin plugin)
        {
            PluginButton btn = new PluginButton();
            btn.plugin = plugin;
            btn.Text = btn.plugin.pluginName;
            btn.Click += btn_Click;
            btn.Height = 50;
            btn.Width = 150;
            layoutPanel.Controls.Add(btn);
            return btn;
        }

        private void btn_Click(object sender, EventArgs e)
        {
            PluginButton btn = (PluginButton)sender;
            btn.plugin.executeSql();
        }

        private void loadPlugins()
        {
            pluginManager = PluginManager.getInstance(pluginDir);
            pluginManager.initPluginIntances();
            foreach (IPlugin plugin in pluginManager.getPluginList())
                addButton(plugin);
        }



        private void frmMain_Load(object sender, EventArgs e)
        {
            string currentDir = System.Environment.CurrentDirectory;
            this.loadPlugins();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void btnIkinciPluginiCalistir_Click(object sender, EventArgs e)
        {
            //2. plugini çalıştır
            pluginManager[1].executeSql();
        }

        private void btnUnloadPlugin_Click(object sender, EventArgs e)
        {
            Assembly[] list = pluginManager.getAssemblyDomainList();

            

            pluginManager.releasePlugins();
        }
    }
}
