using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.RBAC;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.B2C.DAL;

namespace Glaer.Trade.B2C.BLL.Product
{
    public class ProductAuditReason : IProductAuditReason
    {
        protected DAL.Product.IProductAuditReason MyDAL;
        protected IRBAC RBAC;

        public ProductAuditReason()
        {
            MyDAL = DAL.Product.ProductAuditReasonFactory.CreateProductAuditReason();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddProductAuditReason(ProductAuditReasonInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "a71b2324-aa1c-46c8-8525-742f96b44755"))
            {
                return MyDAL.AddProductAuditReason(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：a71b2324-aa1c-46c8-8525-742f96b44755错误");
            }
        }

        public virtual bool EditProductAuditReason(ProductAuditReasonInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "78d18ad2-7c45-4a9c-9a53-cbe50562c242"))
            {
                return MyDAL.EditProductAuditReason(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：78d18ad2-7c45-4a9c-9a53-cbe50562c242错误");
            }
        }

        public virtual int DelProductAuditReason(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "158a1875-7682-4781-97ef-7f31e39280c1"))
            {
                return MyDAL.DelProductAuditReason(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：158a1875-7682-4781-97ef-7f31e39280c1错误");
            }
        }

        public virtual ProductAuditReasonInfo GetProductAuditReasonByID(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "a1db5d4d-d497-42b6-992e-0420d6cdc446"))
            {
                return MyDAL.GetProductAuditReasonByID(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：a1db5d4d-d497-42b6-992e-0420d6cdc446错误");
            }
        }

        public virtual IList<ProductAuditReasonInfo> GetProductAuditReasons(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "a1db5d4d-d497-42b6-992e-0420d6cdc446"))
            {
                return MyDAL.GetProductAuditReasons(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：a1db5d4d-d497-42b6-992e-0420d6cdc446错误");
            }
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "a1db5d4d-d497-42b6-992e-0420d6cdc446"))
            {
                return MyDAL.GetPageInfo(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：a1db5d4d-d497-42b6-992e-0420d6cdc446错误");
            }
        }

        public virtual bool AddProductDenyReason(ProductDenyReasonInfo entity)
        {
            return MyDAL.AddProductDenyReason(entity);
        }

        public virtual int DelProductDenyReason(int ID)
        {
            return MyDAL.DelProductDenyReason(ID);
        }

        public virtual IList<ProductDenyReasonInfo> GetProductDenyReasons(int Product_ID)
        {
            return MyDAL.GetProductDenyReasons(Product_ID);
        }

    }
}

