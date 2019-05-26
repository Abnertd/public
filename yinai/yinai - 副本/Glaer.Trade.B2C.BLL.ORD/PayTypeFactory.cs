using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.ORD
{
    public class PayTypeFactory
    {
        public static IPayType CreatePayType()
        {
            string path = ConfigurationManager.AppSettings["BLLORD"];
            string classname = path + ".PayType";
            return (IPayType)Assembly.Load(path).CreateInstance(classname);
        }

    }
}