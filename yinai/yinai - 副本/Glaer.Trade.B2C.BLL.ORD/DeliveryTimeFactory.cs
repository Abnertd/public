using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.ORD
{
    public class DeliveryTimeFactory
    {
        public static IDeliveryTime CreateDeliveryTime()
        {
            string path = ConfigurationManager.AppSettings["BLLDeliveryTime"];
            string classname = path + ".DeliveryTime";
            return (IDeliveryTime)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
