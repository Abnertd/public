using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.Product
{
    public class ShoppingAskFactory
    {
        public static IShoppingAsk CreateShoppingAsk()
        {
            string path = ConfigurationManager.AppSettings["DALShoppingAsk"];
            string classname = path + ".ShoppingAsk";
            return (IShoppingAsk)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
