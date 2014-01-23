using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

public class CreateAppDomainAndLoadAssemblyMo
{
    private PluginManager pluginManager;

    public CreateAppDomainAndLoadAssemblyMo(PluginManager pluginManager)
    {
        this.pluginManager = pluginManager;
    }

    public Tuple<Assembly,AppDomain> execute(string assemblyFilePath)
    {
        AppDomainSetup setup = new AppDomainSetup() { PrivateBinPath = "plugins" };
        string domainName = System.IO.Path.GetFileNameWithoutExtension(assemblyFilePath);
        string fileName2 = System.IO.Path.GetFileName(assemblyFilePath);
        AppDomain domain = AppDomain.CreateDomain(domainName, null, setup);
        Assembly assembly = domain.Load(AssemblyName.GetAssemblyName(assemblyFilePath));
        return new Tuple<Assembly,AppDomain>(assembly,domain);
    }
}