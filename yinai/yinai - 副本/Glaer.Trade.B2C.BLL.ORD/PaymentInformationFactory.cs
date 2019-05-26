using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.ORD
{
    public class PaymentInformationFactory
    {
        public static IPaymentInformation CreatePaymentInformation()
        {
            string path = ConfigurationManager.AppSettings["BLLPaymentInformation"];
            string classname = path + ".PaymentInformation";
            return (IPaymentInformation)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
