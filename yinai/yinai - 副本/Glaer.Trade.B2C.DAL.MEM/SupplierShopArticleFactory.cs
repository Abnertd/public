using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public class SupplierShopArticleFactory
    {
        public static ISupplierShopArticle CreateSupplierShopArticle()
        {
            string path = ConfigurationManager.AppSettings["DALSupplier"];
            string classname = path + ".SupplierShopArticle";
            return (ISupplierShopArticle)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
