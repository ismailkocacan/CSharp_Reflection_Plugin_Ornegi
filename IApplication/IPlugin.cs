using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plugin
{
    public interface IPlugin
    {
        string pluginName { get; }
        string description { get; }
        string author { get; }

        Boolean executeSql();

        void initialization();
        void finalization();
    }

}
