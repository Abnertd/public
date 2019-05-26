using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.ORD
{
    public class ContractFactory
    {
        public static IContract CreateContract()
        {
            string path = ConfigurationManager.AppSettings["DALContract"];
            string classname = path + ".Contract";
            return (IContract)Assembly.Load(path).CreateInstance(classname);
        }

    }
}

