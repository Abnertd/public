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
    public class ProductTag : IProductTag
    {
        protected DAL.Product.IProductTag MyDAL;
        protected IRBAC RBAC;

        public ProductTag()
        {
            MyDAL = DAL.Product.ProductTagFactory.CreateProductTag();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddProductTag(ProductTagInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "2f1d706e-3356-494d-821c-c4173a923328"))
            {
                return MyDAL.AddProductTag(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：2f1d706e-3356-494d-821c-c4173a923328错误");
            }
        }

        public virtual bool EditProductTag(ProductTagInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "2fea26b6-bbb2-44d8-9b46-0b1aed1cc47f"))
            {
                return MyDAL.EditProductTag(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：2fea26b6-bbb2-44d8-9b46-0b1aed1cc47f错误");
            }
        }

        public virtual int DelProductTag(int cate_id, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "7b8b58e2-e509-4e6c-a68e-0361225cefa6"))
            {
                return MyDAL.DelProductTag(cate_id);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：7b8b58e2-e509-4e6c-a68e-0361225cefa6错误");
            }
        }

        public virtual ProductTagInfo GetProductTagByID(int Cate_ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "ed87eb87-dade-4fbc-804c-c139c1cbe9c8"))
            {
                return MyDAL.GetProductTagByID(Cate_ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：ed87eb87-dade-4fbc-804c-c139c1cbe9c8错误");
            }
        }

        public virtual ProductTagInfo GetProductTagByValue(string tag_Value, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "ed87eb87-dade-4fbc-804c-c139c1cbe9c8"))
            {
                return MyDAL.GetProductTagByValue(tag_Value);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：ed87eb87-dade-4fbc-804c-c139c1cbe9c8错误");
            }
        }

        public virtual IList<ProductTagInfo> GetProductTags(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "ed87eb87-dade-4fbc-804c-c139c1cbe9c8"))
            {
                return MyDAL.GetProductTags(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：ed87eb87-dade-4fbc-804c-c139c1cbe9c8错误");
            }
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "ed87eb87-dade-4fbc-804c-c139c1cbe9c8"))
            {
                return MyDAL.GetPageInfo(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：ed87eb87-dade-4fbc-804c-c139c1cbe9c8错误");
            }
        }

        public virtual int AddProductRelateTag(string Product_RelateTag_ProductID, int Product_RelateTag_TagID)
        {
            return MyDAL.AddProductRelateTag(Product_RelateTag_ProductID, Product_RelateTag_TagID);
        }

    }

}
