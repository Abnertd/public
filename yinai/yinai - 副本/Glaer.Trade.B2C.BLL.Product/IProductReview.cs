using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.Util.Mail;
using Glaer.Trade.B2C.DAL;

namespace Glaer.Trade.B2C.BLL.Product
{
    public interface IProductReview
    {
        bool AddProductReview(ProductReviewInfo entity, RBACUserInfo UserPrivilege);

        bool EditProductReview(ProductReviewInfo entity, RBACUserInfo UserPrivilege);

        int DelProductReview(int ID, RBACUserInfo UserPrivilege);

        ProductReviewInfo GetProductReviewByID(int ID, RBACUserInfo UserPrivilege);

        IList<ProductReviewInfo> GetProductReviews(QueryInfo Query, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);

        int GetProductReviewValidCount(int Product_ID);

        int GetProductStarCount(int Product_ID);

        bool UpdateProductReviewINfo(int Product_ID, double Review_Average, int Review_Count, int Review_validCount);

    }
}
