using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.ORD
{
    public class ContractLogFactory
    {
        public static IContractLog CreateContractLog()
        {
            string path = ConfigurationManager.AppSettings["BLLContract"];
            string classname = path + ".ContractLog";
            return (IContractLog)Assembly.Load(path).CreateInstance(classname);
        }

    }
}