using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.Product
{
    public class ProductAuditReasonFactory
    {
        public static IProductAuditReason CreateProductAuditReason()
        {
            string path = ConfigurationManager.AppSettings["DALProductAuditReason"];
            string classname = path + ".ProductAuditReason";
            return (IProductAuditReason)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
