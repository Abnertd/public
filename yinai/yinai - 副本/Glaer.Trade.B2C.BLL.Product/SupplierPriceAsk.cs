using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.RBAC;

namespace Glaer.Trade.B2C.BLL.Product
{
    public class SupplierPriceAsk : ISupplierPriceAsk
    {
        protected DAL.Product.ISupplierPriceAsk MyDAL;
        protected IRBAC RBAC;

        public SupplierPriceAsk()
        {
            MyDAL = DAL.Product.SupplierPriceAskFactory.CreateSupplierPriceAsk();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddSupplierPriceAsk(SupplierPriceAskInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "db32b459-6e76-4ce9-816d-0ca7b1dea952"))
            {
                return MyDAL.AddSupplierPriceAsk(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：db32b459-6e76-4ce9-816d-0ca7b1dea952错误");
            } 
        }

        public virtual bool EditSupplierPriceAsk(SupplierPriceAskInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "19578feb-813c-49cd-83c3-65c51bb05b09"))
            {
                return MyDAL.EditSupplierPriceAsk(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：19578feb-813c-49cd-83c3-65c51bb05b09错误");
            } 
        }

        public virtual int DelSupplierPriceAsk(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "48800724-3e45-45d6-9d93-6ad5ab6eb91e"))
            {
                return MyDAL.DelSupplierPriceAsk(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：48800724-3e45-45d6-9d93-6ad5ab6eb91e错误");
            } 
        }

        public virtual int DelSupplierPriceAskByProductID(int ProductID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "48800724-3e45-45d6-9d93-6ad5ab6eb91e"))
            {
                return MyDAL.DelSupplierPriceAskByProductID(ProductID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：48800724-3e45-45d6-9d93-6ad5ab6eb91e错误");
            }
        }

        public virtual SupplierPriceAskInfo GetSupplierPriceAskByID(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "249d2ad4-45f4-4945-8e78-d18c79053106"))
            {
                return MyDAL.GetSupplierPriceAskByID(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：249d2ad4-45f4-4945-8e78-d18c79053106错误");
            } 
        }

        public virtual IList<SupplierPriceAskInfo> GetSupplierPriceAsks(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "249d2ad4-45f4-4945-8e78-d18c79053106"))
            {
                return MyDAL.GetSupplierPriceAsks(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：249d2ad4-45f4-4945-8e78-d18c79053106错误");
            }
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "249d2ad4-45f4-4945-8e78-d18c79053106"))
            {
                return MyDAL.GetPageInfo(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：249d2ad4-45f4-4945-8e78-d18c79053106错误");
            }
        }

    }
}
