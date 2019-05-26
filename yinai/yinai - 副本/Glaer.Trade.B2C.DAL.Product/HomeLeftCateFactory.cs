using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.Product
{
    public class HomeLeftCateFactory
    {
        public static IHomeLeftCate CreateHomeLeftCate()
        {
            string path = ConfigurationManager.AppSettings["DALCategory"];
            string classname = path + ".HomeLeftCate";
            return (IHomeLeftCate)Assembly.Load(path).CreateInstance(classname);
        }

    }

}

