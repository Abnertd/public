using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.Sys
{
    public class RBACPrivilegeFactory
    {
        public static IRBACPrivilege CreateRBACPrivilege()
        {
            string path = ConfigurationManager.AppSettings["BLLRBACUser"];
            string classname = path + ".RBACPrivilege";
            return (IRBACPrivilege)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
