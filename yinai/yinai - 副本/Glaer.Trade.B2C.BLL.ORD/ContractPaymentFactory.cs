using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.ORD
{
    public class ContractPaymentFactory
    {
        public static IContractPayment CreateContractPayment()
        {
            string path = ConfigurationManager.AppSettings["BLLContract"];
            string classname = path + ".ContractPayment";
            return (IContractPayment)Assembly.Load(path).CreateInstance(classname);
        }

    }
}