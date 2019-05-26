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
    public class ProductNotify : IProductNotify
    {
        protected DAL.Product.IProductNotify MyDAL;
        protected IRBAC RBAC;

        public ProductNotify()
        {
            MyDAL = DAL.Product.ProductNotifyFactory.CreateProductNotify();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddProductNotify(ProductNotifyInfo entity)
        {
            return MyDAL.AddProductNotify(entity);
        }

        public virtual bool EditProductNotify(ProductNotifyInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "98dd8cbd-8ea7-4a59-89ec-988f149c16bb"))
            {
                return MyDAL.EditProductNotify(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：98dd8cbd-8ea7-4a59-89ec-988f149c16bb错误");
            }

        }

        public virtual int DelProductNotify(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "d5183803-ddfa-4a0b-8319-bed75950a08c"))
            {
                return MyDAL.DelProductNotify(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：d5183803-ddfa-4a0b-8319-bed75950a08c错误");
            }
        }

        public virtual ProductNotifyInfo GetProductNotifyByID(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "e996b26f-2c14-482f-b5f6-955f38b50b3f"))
            {
                return MyDAL.GetProductNotifyByID(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：e996b26f-2c14-482f-b5f6-955f38b50b3f错误");
            }
        }

        public virtual IList<ProductNotifyInfo> GetProductNotifys(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "e996b26f-2c14-482f-b5f6-955f38b50b3f"))
            {
                return MyDAL.GetProductNotifys(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：e996b26f-2c14-482f-b5f6-955f38b50b3f错误");
            }
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "e996b26f-2c14-482f-b5f6-955f38b50b3f"))
            {
                return MyDAL.GetPageInfo(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：e996b26f-2c14-482f-b5f6-955f38b50b3f错误");
            }
        }

    }
}

