using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.ORD
{
    public class ContractDividedPaymentFactory
    {
        public static IContractDividedPayment CreateContractDividedPayment()
        {
            string path = ConfigurationManager.AppSettings["DALContract"];
            string classname = path + ".ContractDividedPayment";
            return (IContractDividedPayment)Assembly.Load(path).CreateInstance(classname);
        }

    }
}

