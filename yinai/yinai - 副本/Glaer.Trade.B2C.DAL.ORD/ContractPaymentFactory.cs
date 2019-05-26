using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.ORD
{
    public class ContractPaymentFactory
    {
        public static IContractPayment CreateContractPayment()
        {
            string path = ConfigurationManager.AppSettings["DALContract"];
            string classname = path + ".ContractPayment";
            return (IContractPayment)Assembly.Load(path).CreateInstance(classname);
        }

    }
}

