using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.Product
{
    public class ProductReviewFactory
    {
        public static IProductReview CreateProductReview()
        {
            string path = ConfigurationManager.AppSettings["BLLProductReview"];
            string classname = path + ".ProductReview";
            return (IProductReview)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
