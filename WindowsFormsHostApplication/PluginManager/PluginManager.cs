/*
 *  @author İsmail KOCACAN
 */

using Plugin;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Security.Policy;
using System.Collections.Generic;

public class PluginManager : IDisposable
{

    private static object _lock = new object();

    private static PluginManager pm;

    public string pluginDirPath { get; set; }

    private List<string> pluginFilePathList;

    /// <summary>
    /// assembly içersindeki tipleri tutmak için...
    /// </summary>
    private List<Type> pluginTypeList = new List<Type>();

    private Type pluginType = typeof(IPlugin);

    /// <summary>
    /// interface ile interfec'i implement eden sınıfların,intance larını tuttuğumuz liste
    /// </summary>
    private Dictionary<IPlugin, object> pluginInstanceMap = new Dictionary<IPlugin, object>();

    /// <summary>
    /// hangi assembly,hangi domaine yüklediğimizi tutmak için
    /// </summary>
    private Dictionary<Assembly, AppDomain> assemblyDomainMap = new Dictionary<Assembly, AppDomain>();




    public Assembly[] getAssemblyDomainList()
    {
        //return assemblyDomainMap.Values.ToList<AppDomain>()[1].GetAssemblies();
        return AppDomain.CurrentDomain.GetAssemblies();
    }


    public Assembly createAppDomainAndLoadAssembly(string assemblyFilePath)
    {
        //replace method with method object.(CreateAppDomainAndLoadAssemblyMo)
        Tuple<Assembly, AppDomain> map = new CreateAppDomainAndLoadAssemblyMo(this).execute(assemblyFilePath);
        this.assemblyDomainMap.Add(map.Item1, map.Item2);
        return map.Item1;
    }



    #region test

    /// <summary>
    /// Plugin assembly dosyasını yeni bir domaine yükler.
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    /*  public Assembly createAppDomainAndLoadAssembly(string assemblyFilePath)
      {
          AppDomainSetup setup = new AppDomainSetup();
          setup.ConfigurationFile = AppDomain.CurrentDomain.BaseDirectory;
          setup.ApplicationBase = AppDomain.CurrentDomain.BaseDirectory;

          string domainName = System.IO.Path.GetFileNameWithoutExtension(assemblyFilePath);
          string fileName2 = System.IO.Path.GetFileName(assemblyFilePath);
          AppDomain domain = AppDomain.CreateDomain(domainName, null, setup);

          Type type = typeof(ProxyDomain);
          ProxyDomain proxy = (ProxyDomain)domain.CreateInstanceAndUnwrap(type.Assembly.FullName, type.FullName);

          Assembly assembly = proxy.loadAssembly(assemblyFilePath);

          this.assemblyDomainMap.Add(assembly, domain);
          return assembly;
      }
    */

    //public Assembly addAssemblyToDomainFrom2(string filePath)
    //{
    //    try
    //    {
    //        ProxyDomain domain = new ProxyDomain();
    //        Assembly assembly = domain.loadAssembly(filePath);
    //        assemblyDomainMap.Add(assembly, domain);
    //        return assembly;
    //    }
    //    catch (Exception e)
    //    {
    //        return null;
    //    }
    //}



    //public Assembly addAssemblyToDomainFrom(string filePath)
    //{
    //http://stackoverflow.com/questions/3136371/load-independent-assembly-in-a-another-app-domain

    //    string pluginDir = pluginDirPath + "\\";
    //    AppDomainSetup setup = new AppDomainSetup();
    //    setup.ConfigurationFile = AppDomain.CurrentDomain.BaseDirectory + "WindowsFormsHostApplication.exe.config";
    //    setup.ApplicationBase = AppDomain.CurrentDomain.BaseDirectory;

    //    string domainName = System.IO.Path.GetFileNameWithoutExtension(filePath);
    //    string fileName2 = System.IO.Path.GetFileName(filePath);
    //    AppDomain domain = AppDomain.CreateDomain(domainName, null, setup);
    //    string path = domain.BaseDirectory;
    //    AssemblyName assemblyName = new AssemblyName();
    //    assemblyName.CodeBase = fileName2;

    //    Assembly assembly;
    //    try
    //    {
    //        assembly = domain.Load(assemblyName);
    //    }
    //    catch (Exception e)
    //    {
    //        throw e;
    //    }

    //    return assembly;
    //}
    #endregion

    public PluginManager(string pluginDirPath)
    {
        this.pluginDirPath = pluginDirPath;
    }



    public void Dispose()
    {
        pm.Dispose();
    }

    /// <summary>
    /// Class Library dosyalarını yüklemek için kullanılan method.
    /// </summary>
    private void loadPlugins()
    {
        if (!Directory.Exists(pluginDirPath))
            throw new Exception(string.Format("{0} yolu bulunamadı !", pluginDirPath));

        string[] filePaths = Directory.GetFiles(pluginDirPath, "*.dll");
        this.pluginFilePathList = new List<string>(filePaths);

        foreach (string filePath in pluginFilePathList)
            loadPluginFromFilePath(filePath);
    }



    /// <summary>
    /// Plugin dosyasını yükler.
    /// </summary>
    /// <param name="filePath"></param>
    public Type loadPluginFromFilePath(string filePath)
    {
        Type resultType = null;
        Assembly asm = loadAssembly(filePath);
        Type[] types = getTypeListInPlugin(asm);
        foreach (Type typ in types)
        {
            if (typeIsIPluginInterfaceImplement(typ))
            {
                pluginTypeList.Add(typ);
                resultType = typ;
                break;
            }
        }
        return resultType;
    }

    /// <summary>
    /// Parametre ile verilen tipin,IPlugin interface'i implement edip,etmediğini kontrol eder.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private Boolean typeIsIPluginInterfaceImplement(Type type)
    {
        return (type.GetInterface(pluginType.FullName) != null);
    }

    /// <summary>
    /// Yüklenen plugin'lerden intance oluşturan method.
    /// </summary>
    public void initPluginIntances()
    {
        this.loadPlugins();
        foreach (Type type in pluginTypeList)
        {
            object intance = initInstance(type);
            IPlugin plugin = (IPlugin)intance;
            // her plugini yüklediğimizde initialization methodunu çalıştırıyoruz.
            // default değerlerin atanması sağlanabilir.
            plugin.initialization();
            pluginInstanceMap.Add(plugin, (IPlugin)intance);
        }


        foreach (string filePath in pluginFilePathList)
        {
            Type type = loadPluginFromFilePath(filePath);
            initPluginInstance(type);
        }
    }


    public void initPluginInstance(Type type)
    {
        try
        {
            object intance = initInstance(type);
            IPlugin plugin = (IPlugin)intance;
            plugin.initialization();
            pluginInstanceMap.Add(plugin, (IPlugin)intance);
        }
        catch (PluginLoadException ex)
        {
            throw ex;
        }
    }


    /// <summary>
    /// Type'den yeni bir instance oluşturan method.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private object initInstance(Type type)
    {
        return Activator.CreateInstance(type);
    }

    /// <summary>
    /// Yüklenen tüm pluginleri unload eder.
    /// </summary>
    public void releasePlugins()
    {
        foreach (AppDomain domain in assemblyDomainMap.Values)
        {
            AppDomain.Unload(domain);
        }
    }

    /// <summary>
    /// Assembly içersindeki tip listesini döndüren method.
    /// </summary>
    /// <param name="asm"></param>
    /// <returns></returns>
    private Type[] getTypeListInPlugin(Assembly asm)
    {
        return asm.GetTypes();
    }

    /// <summary>
    /// Yolu verilen Assembly dosyasını belleğe yükler.
    /// </summary>
    /// <param name="pluginFilePath"></param>
    /// <returns></returns>
    private Assembly loadAssembly(string pluginFilePath)
    {
        Assembly assembly = createAppDomainAndLoadAssembly(pluginFilePath);
        return assembly;
    }

    /// <summary>
    /// Plugin listesini döndüren method.
    /// </summary>
    /// <returns></returns>
    public List<IPlugin> getPluginList()
    {
        return pluginInstanceMap.Keys.AsEnumerable<IPlugin>().ToList<IPlugin>();
    }

    /// <summary>
    /// Plugin sayısını döndüren method.
    /// </summary>
    /// <returns></returns>
    public int getPluginCount()
    {
        return this.pluginInstanceMap.Count();
    }

    /// <summary>
    /// PluginManager için indexer
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public IPlugin this[int index]
    {
        get
        {
            return pluginInstanceMap.Keys.ElementAt(index);
        }
    }

    /// <summary>
    /// PluginManager sınıfından bir instance döndürür.(singleton)
    /// </summary>
    /// <param name="pluginDir">Plugin'lerin yükleneceği dizin</param>
    /// <returns></returns>
    public static PluginManager getInstance(string pluginDir)
    {
        if (pluginManagerIsNull())
        {
            lock (_lock)
            {
                if (pluginManagerIsNull())
                {
                    pm = new PluginManager(pluginDir);
                }
            }
        }
        return pm;
    }

    /// <summary>
    ///  
    /// </summary>
    /// <returns></returns>
    private static bool pluginManagerIsNull()
    {
        return pm == null;
    }

}
