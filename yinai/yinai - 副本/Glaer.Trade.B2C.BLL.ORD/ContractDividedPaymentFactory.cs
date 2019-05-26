using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.ORD
{
    public class ContractDividedPaymentFactory
    {
        public static IContractDividedPayment CreateContractDividedPayment()
        {
            string path = ConfigurationManager.AppSettings["BLLContract"];
            string classname = path + ".ContractDividedPayment";
            return (IContractDividedPayment)Assembly.Load(path).CreateInstance(classname);
        }

    }
}