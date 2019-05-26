using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.Product
{
    public class ProductReviewConfigFactory
    {
        public static IProductReviewConfig CreateProductReviewConfig()
        {
            string path = ConfigurationManager.AppSettings["DALProductReviewConfig"];
            string classname = path + ".ProductReviewConfig";
            return (IProductReviewConfig)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
