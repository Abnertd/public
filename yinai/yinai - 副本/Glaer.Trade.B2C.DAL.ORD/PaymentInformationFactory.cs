using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.ORD
{
    public class PaymentInformationFactory
    {
        public static IPaymentInformation CreatePaymentInformation()
        {
            string path = ConfigurationManager.AppSettings["DALPaymentInformation"];
            string classname = path + ".PaymentInformation";
            return (IPaymentInformation)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
