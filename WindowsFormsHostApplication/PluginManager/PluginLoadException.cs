using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class PluginLoadException : Exception
{
    public PluginLoadException(string message)
        : base("Plugin yüklenemedi amk !")
    {

    }
}