using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.Product
{
    public class PackageFactory
    {
        public static IPackage CreatePackage()
        {
            string path = ConfigurationManager.AppSettings["BLLCategory"];
            string classname = path + ".Package";
            return (IPackage)Assembly.Load(path).CreateInstance(classname);
        }
    }
}
