using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.ORD
{
    public class ContractLogFactory
    {
        public static IContractLog CreateContractLog()
        {
            string path = ConfigurationManager.AppSettings["DALContract"];
            string classname = path + ".ContractLog";
            return (IContractLog)Assembly.Load(path).CreateInstance(classname);
        }

    }
}

