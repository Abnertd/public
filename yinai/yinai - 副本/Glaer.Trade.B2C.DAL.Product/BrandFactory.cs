using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.Product
{
    public class BrandFactory
    {
        public static IBrand CreateBrand()
        {
            string path = ConfigurationManager.AppSettings["DALBrand"];
            string classname = path + ".Brand";
            return (IBrand)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
