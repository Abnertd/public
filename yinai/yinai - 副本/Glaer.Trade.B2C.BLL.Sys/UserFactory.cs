using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.Sys
{
    public class RBACUserFactory
    {
        public static IRBACUser CreateRBACUser()
        {
            string path = ConfigurationManager.AppSettings["BLLRBACUser"];
            string classname = path + ".RBACUser";
            return (IRBACUser)Assembly.Load(path).CreateInstance(classname);
        }

        public static IRBACUserGroup CreateRBACUserGroup()
        {
            string path = ConfigurationManager.AppSettings["BLLRBACUser"];
            string classname = path + ".RBACUserGroup";
            return (IRBACUserGroup)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
