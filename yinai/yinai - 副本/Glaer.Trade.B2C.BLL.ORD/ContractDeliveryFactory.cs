using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.ORD
{
    public class ContractDeliveryFactory
    {
        public static IContractDelivery CreateContractDelivery()
        {
            string path = ConfigurationManager.AppSettings["BLLContract"];
            string classname = path + ".ContractDelivery";
            return (IContractDelivery)Assembly.Load(path).CreateInstance(classname);
        }

    }
}

