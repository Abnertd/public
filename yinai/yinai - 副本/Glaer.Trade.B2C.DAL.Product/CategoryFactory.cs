using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.Product
{
    public class CategoryFactory
    {
        public static ICategory CreateCategory()
        {
            string path = ConfigurationManager.AppSettings["DALCategory"];
            string classname = path + ".Category";
            return (ICategory)Assembly.Load(path).CreateInstance(classname);
        }
    }

}
