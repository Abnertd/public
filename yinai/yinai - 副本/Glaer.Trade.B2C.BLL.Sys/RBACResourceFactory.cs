using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.Sys
{
    public class RBACResourceFactory
    {
        public static IRBACResourceGroup CreateRBACResourceGroup()
        {
            string path = ConfigurationManager.AppSettings["BLLRBACUser"];
            string classname = path + ".RBACResourceGroup";
            return (IRBACResourceGroup)Assembly.Load(path).CreateInstance(classname);
        }

        public static IRBACResource CreateRBACResource()
        {
            string path = ConfigurationManager.AppSettings["BLLRBACUser"];
            string classname = path + ".RBACResource";
            return (IRBACResource)Assembly.Load(path).CreateInstance(classname);
        }

    }
}