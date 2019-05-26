using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.Product
{
    public class StockoutBookingFactory
    {
        public static IStockoutBooking CreateStockoutBooking()
        {
            string path = ConfigurationManager.AppSettings["DALStockoutBooking"];
            string classname = path + ".StockoutBooking";
            return (IStockoutBooking)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
