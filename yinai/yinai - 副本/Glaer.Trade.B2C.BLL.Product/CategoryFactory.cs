using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.Product
{
    public class CategoryFactory
    {
        public static ICategory CreateCategory()
        {
            string path = ConfigurationManager.AppSettings["BLLCategory"];
            string classname = path + ".Category";
            return (ICategory)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
