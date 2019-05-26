using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.Sys
{
    public class RBACPrivilegeFactory
    {
        public static IRBACPrivilege CreateRBACPrivilege()
        {
            string path = ConfigurationManager.AppSettings["DALRBACUser"];
            string classname = path + ".RBACPrivilege";
            return (IRBACPrivilege)Assembly.Load(path).CreateInstance(classname);
        }

    }

}
