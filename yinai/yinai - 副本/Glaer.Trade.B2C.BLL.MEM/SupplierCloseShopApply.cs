using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.RBAC;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.B2C.DAL;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public class SupplierCloseShopApply : ISupplierCloseShopApply
    {
        protected DAL.MEM.ISupplierCloseShopApply MyDAL;
        protected IRBAC RBAC;

        public SupplierCloseShopApply()
        {
            MyDAL = DAL.MEM.SupplierCloseShopApplyFactory.CreateSupplierCloseShopApply();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddSupplierCloseShopApply(SupplierCloseShopApplyInfo entity)
        {
            return MyDAL.AddSupplierCloseShopApply(entity);
        }

        public virtual bool EditSupplierCloseShopApply(SupplierCloseShopApplyInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "0791905a-1212-4fa5-8708-b835cc03c4a3"))
            {
                return MyDAL.EditSupplierCloseShopApply(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：0791905a-1212-4fa5-8708-b835cc03c4a3错误");
            } 
        }

        public virtual int DelSupplierCloseShopApply(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "bd8d861d-dca1-4e52-84a9-013c68e3134d"))
            {
                return MyDAL.DelSupplierCloseShopApply(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：bd8d861d-dca1-4e52-84a9-013c68e3134d错误");
            } 
        }

        public virtual SupplierCloseShopApplyInfo GetSupplierCloseShopApplyByID(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "81e0af57-348d-4565-9e73-7146b3116b8c"))
            {
                return MyDAL.GetSupplierCloseShopApplyByID(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：81e0af57-348d-4565-9e73-7146b3116b8c错误");
            } 
        }

        public virtual IList<SupplierCloseShopApplyInfo> GetSupplierCloseShopApplys(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "81e0af57-348d-4565-9e73-7146b3116b8c"))
            {
                return MyDAL.GetSupplierCloseShopApplys(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：81e0af57-348d-4565-9e73-7146b3116b8c错误");
            } 
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "81e0af57-348d-4565-9e73-7146b3116b8c"))
            {
                return MyDAL.GetPageInfo(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：81e0af57-348d-4565-9e73-7146b3116b8c错误");
            } 
        }

    }



}

