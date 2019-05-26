using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.Sys
{
    public class RBACRoleFactory
    {
        public static IRBACRole CreateRBACRole()
        {
            string path = ConfigurationManager.AppSettings["BLLRBACUser"].ToString();
            string classname = path + ".RBACRole";
            return (IRBACRole)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
