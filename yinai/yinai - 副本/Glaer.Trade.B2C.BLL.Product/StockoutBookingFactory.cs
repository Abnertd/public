using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.Product
{
    public class StockoutBookingFactory
    {
        public static IStockoutBooking CreateStockoutBooking()
        {
            string path = ConfigurationManager.AppSettings["BLLStockoutBooking"];
            string classname = path + ".StockoutBooking";
            return (IStockoutBooking)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
