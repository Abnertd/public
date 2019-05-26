using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public class SupplierShopArticleFactory
    {
        public static ISupplierShopArticle CreateSupplierShopArticle()
        {
            string path = ConfigurationManager.AppSettings["BLLSupplier"];
            string classname = path + ".SupplierShopArticle";
            return (ISupplierShopArticle)Assembly.Load(path).CreateInstance(classname);
        }

    }



}
