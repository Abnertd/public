using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Reflection;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public class SupplierFavoritesFactory
    {
        public static ISupplierFavorites CreateSupplierFavorites()
        {
            string path = ConfigurationManager.AppSettings["BLLSupplierFavorites"];
            string classname = path + ".SupplierFavorites";
            return (ISupplierFavorites)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
