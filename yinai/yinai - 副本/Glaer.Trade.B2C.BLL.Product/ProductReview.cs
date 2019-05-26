using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.Util.Mail;
using Glaer.Trade.B2C.DAL;
using Glaer.Trade.B2C.RBAC;

namespace Glaer.Trade.B2C.BLL.Product
{
    public class ProductReview : IProductReview
    {
        protected DAL.Product.IProductReview MyDAL;
        protected IRBAC RBAC;

        public ProductReview()
        {
            MyDAL = DAL.Product.ProductReviewFactory.CreateProductReview();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddProductReview(ProductReviewInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "0b385bc8-aea0-4792-afb6-935e96a8aa3c"))
            {
                return MyDAL.AddProductReview(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：0b385bc8-aea0-4792-afb6-935e96a8aa3c错误");
            }
        }

        public virtual bool EditProductReview(ProductReviewInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "cb1e9c33-7ac5-4939-8520-a0e192cb0129"))
            {
                return MyDAL.EditProductReview(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：cb1e9c33-7ac5-4939-8520-a0e192cb0129错误");
            }
        }

        public virtual int DelProductReview(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "3f48e253-5e00-44ce-8a9e-7475134bfd18"))
            {
                return MyDAL.DelProductReview(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：3f48e253-5e00-44ce-8a9e-7475134bfd18错误");
            }
        }

        public virtual ProductReviewInfo GetProductReviewByID(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "cb1e9c33-7ac5-4939-8520-a0e192cb0129"))
            {
                return MyDAL.GetProductReviewByID(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：cb1e9c33-7ac5-4939-8520-a0e192cb0129错误");
            }
        }

        public virtual IList<ProductReviewInfo> GetProductReviews(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "cb1e9c33-7ac5-4939-8520-a0e192cb0129"))
            {
                return MyDAL.GetProductReviews(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：cb1e9c33-7ac5-4939-8520-a0e192cb0129错误");
            }
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "cb1e9c33-7ac5-4939-8520-a0e192cb0129"))
            {
                return MyDAL.GetPageInfo(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：cb1e9c33-7ac5-4939-8520-a0e192cb0129错误");
            }
        }

        public virtual int GetProductReviewValidCount(int Product_ID)
        {
            return MyDAL.GetProductReviewValidCount(Product_ID);
        }

        public virtual int GetProductStarCount(int Product_ID) 
        {
            return MyDAL.GetProductStarCount(Product_ID);
        }

        public virtual bool UpdateProductReviewINfo(int Product_ID, double Review_Average, int Review_Count, int Review_validCount)
        {
            return MyDAL.UpdateProductReviewINfo(Product_ID, Review_Average, Review_Count, Review_validCount);
        }

    }
}

