using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.Product
{
    public class ShoppingAskFactory
    {
        public static IShoppingAsk CreateShoppingAsk()
        {
            string path = ConfigurationManager.AppSettings["BLLShoppingAsk"];
            string classname = path + ".ShoppingAsk";
            return (IShoppingAsk)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
