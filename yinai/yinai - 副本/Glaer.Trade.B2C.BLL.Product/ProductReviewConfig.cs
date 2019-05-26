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
    public class ProductReviewConfig : IProductReviewConfig
    {
        protected DAL.Product.IProductReviewConfig MyDAL;
        protected IRBAC RBAC;

        public ProductReviewConfig()
        {
            MyDAL = DAL.Product.ProductReviewConfigFactory.CreateProductReviewConfig();
            RBAC = RBACFactory.CreateRBAC();
        }


        public virtual bool EditProductReviewConfig(ProductReviewConfigInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "b948d76d-944c-4a97-82dc-a3917ce6dcd9"))
            {
                return MyDAL.EditProductReviewConfig(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：b948d76d-944c-4a97-82dc-a3917ce6dcd9错误");
            }
        }

        public virtual ProductReviewConfigInfo GetProductReviewConfig(RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "b948d76d-944c-4a97-82dc-a3917ce6dcd9"))
            {
                return MyDAL.GetProductReviewConfig();
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：b948d76d-944c-4a97-82dc-a3917ce6dcd9错误");
            }
        }


    }
}

