using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.Util.Tools;

namespace Glaer.Trade.B2C.DAL.Product
{
    public interface IProductReview
    {
        bool AddProductReview(ProductReviewInfo entity);

        bool EditProductReview(ProductReviewInfo entity);

        int DelProductReview(int ID);

        ProductReviewInfo GetProductReviewByID(int ID);

        IList<ProductReviewInfo> GetProductReviews(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);

        int GetProductReviewValidCount(int Product_ID);

        int GetProductStarCount(int Product_ID);

        bool UpdateProductReviewINfo(int Product_ID, double Review_Average, int Review_Count, int Review_validCount);
    }
}
