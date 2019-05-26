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
    public class SupplierPayBackApply : ISupplierPayBackApply
    {
        protected DAL.MEM.ISupplierPayBackApply MyDAL;
        protected IRBAC RBAC;

        public SupplierPayBackApply()
        {
            MyDAL = DAL.MEM.SupplierPayBackApplyFactory.CreateSupplierPayBackApply();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddSupplierPayBackApply(SupplierPayBackApplyInfo entity)
        {
            return MyDAL.AddSupplierPayBackApply(entity);
        }

        public virtual bool EditSupplierPayBackApply(SupplierPayBackApplyInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "479e01e0-d543-47c2-a229-52e9eb847886"))
            {
                return MyDAL.EditSupplierPayBackApply(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：479e01e0-d543-47c2-a229-52e9eb847886错误");
            } 
        }

        public virtual int DelSupplierPayBackApply(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "70939c0f-4e76-4f4a-9d6c-cff9e11e27ec"))
            {
                return MyDAL.DelSupplierPayBackApply(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：70939c0f-4e76-4f4a-9d6c-cff9e11e27ec错误");
            } 
        }

        public virtual SupplierPayBackApplyInfo GetSupplierPayBackApplyByID(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "b90823db-e737-4ae9-b428-1494717b85c7"))
            {
                return MyDAL.GetSupplierPayBackApplyByID(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：b90823db-e737-4ae9-b428-1494717b85c7错误");
            } 
        }

        public virtual IList<SupplierPayBackApplyInfo> GetSupplierPayBackApplys(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "b90823db-e737-4ae9-b428-1494717b85c7"))
            {
                return MyDAL.GetSupplierPayBackApplys(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：b90823db-e737-4ae9-b428-1494717b85c7错误");
            } 
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "b90823db-e737-4ae9-b428-1494717b85c7"))
            {
                return MyDAL.GetPageInfo(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：b90823db-e737-4ae9-b428-1494717b85c7错误");
            } 
        }

    }


}

