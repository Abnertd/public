using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.ORD
{
    public class ContractFactory
    {
        public static IContract CreateContract()
        {
            string path = ConfigurationManager.AppSettings["BLLContract"];
            string classname = path + ".Contract";
            return (IContract)Assembly.Load(path).CreateInstance(classname);
        }

    }
}