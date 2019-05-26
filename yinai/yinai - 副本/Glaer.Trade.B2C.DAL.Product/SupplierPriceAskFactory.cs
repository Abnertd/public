using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Reflection;

namespace Glaer.Trade.B2C.DAL.Product
{
    public class SupplierPriceAskFactory
    {
        public static ISupplierPriceAsk CreateSupplierPriceAsk()
        {
            string path = ConfigurationManager.AppSettings["DALCategory"];
            string classname = path + ".SupplierPriceAsk";
            return (ISupplierPriceAsk)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
