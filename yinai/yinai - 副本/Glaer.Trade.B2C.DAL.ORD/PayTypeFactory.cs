using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.ORD
{
    public class PayTypeFactory
    {
        public static IPayType CreatePayType()
        {
            string path = ConfigurationManager.AppSettings["DALORD"];
            string classname = path + ".PayType";
            return (IPayType)Assembly.Load(path).CreateInstance(classname);
        }

    }
}

