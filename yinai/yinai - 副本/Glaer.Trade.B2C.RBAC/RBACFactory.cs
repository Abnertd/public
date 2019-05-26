using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.RBAC
{
    public class RBACFactory
    {
        public static IRBAC CreateRBAC()
        {
            string path = ConfigurationManager.AppSettings["BLLRBAC"];
            string classname = path + ".RBAC";
            return (IRBAC)Assembly.Load(path).CreateInstance(classname);
        }
    }
}
