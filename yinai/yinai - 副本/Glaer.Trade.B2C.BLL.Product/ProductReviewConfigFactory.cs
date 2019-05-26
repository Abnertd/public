using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.Product
{
    public class ProductReviewConfigFactory
    {
        public static IProductReviewConfig CreateProductReviewConfig()
        {
            string path = ConfigurationManager.AppSettings["BLLProductReviewConfig"];
            string classname = path + ".ProductReviewConfig";
            return (IProductReviewConfig)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
