using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.Sys
{
    public class RBACResourceFactory
    {
        public static IRBACResourceGroup CreateRBACResourceGroup()
        {
            string path = ConfigurationManager.AppSettings["DALRBACUser"];
            string classname = path + ".RBACResourceGroup";
            return (IRBACResourceGroup)Assembly.Load(path).CreateInstance(classname);
        }

        public static IRBACResource CreateRBACResource()
        {
            string path = ConfigurationManager.AppSettings["DALRBACUser"];
            string classname = path + ".RBACResource";
            return (IRBACResource)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
