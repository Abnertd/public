using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.ORD
{
    public class ContractTemplateFactory
    {
        public static IContractTemplate CreateContractTemplate()
        {
            string path = ConfigurationManager.AppSettings["BLLContract"];
            string classname = path + ".ContractTemplate";
            return (IContractTemplate)Assembly.Load(path).CreateInstance(classname);
        }

    }
}