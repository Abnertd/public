using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.ORD
{
    public class DeliveryTimeFactory
    {
        public static IDeliveryTime CreateDeliveryTime()
        {
            string path = ConfigurationManager.AppSettings["DALDeliveryTime"];
            string classname = path + ".DeliveryTime";
            return (IDeliveryTime)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
