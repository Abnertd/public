using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.ORD
{
    public class ContractTemplateFactory
    {
        public static IContractTemplate CreateContractTemplate()
        {
            string path = ConfigurationManager.AppSettings["DALContract"];
            string classname = path + ".ContractTemplate";
            return (IContractTemplate)Assembly.Load(path).CreateInstance(classname);
        }

    }
}

