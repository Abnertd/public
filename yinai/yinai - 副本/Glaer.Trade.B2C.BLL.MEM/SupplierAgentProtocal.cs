using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glaer.Trade.B2C.RBAC;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public class SupplierAgentProtocal : ISupplierAgentProtocal
    {
        protected DAL.MEM.ISupplierAgentProtocal MyDAL;
        protected IRBAC RBAC;

        public SupplierAgentProtocal()
        {
            MyDAL = DAL.MEM.SupplierAgentProtocalFactory.CreateSupplierAgentProtocal();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddSupplierAgentProtocal(SupplierAgentProtocalInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "e0920e95-65fa-4e3c-9dd6-2794ccc45782"))
            {
                return MyDAL.AddSupplierAgentProtocal(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：e0920e95-65fa-4e3c-9dd6-2794ccc45782错误");
            } 
       
        }

        public virtual bool EditSupplierAgentProtocal(SupplierAgentProtocalInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "7abc095a-d322-4312-861c-aecb6088c3bb"))
            {
                return MyDAL.EditSupplierAgentProtocal(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：7abc095a-d322-4312-861c-aecb6088c3bb错误");
            } 
           
        }

        public virtual int DelSupplierAgentProtocal(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "ac6547d8-b986-4626-8476-7859dce1ab02"))
            {
                return MyDAL.DelSupplierAgentProtocal(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：ac6547d8-b986-4626-8476-7859dce1ab02错误");
            } 
           
        }

        public virtual SupplierAgentProtocalInfo GetSupplierAgentProtocalByID(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "0aab7822-e327-4dcd-bc30-4cbf289067e4"))
            {
                return MyDAL.GetSupplierAgentProtocalByID(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：0aab7822-e327-4dcd-bc30-4cbf289067e4错误");
            } 
           
        }
        public virtual SupplierAgentProtocalInfo GetSupplierAgentProtocalByPurchaseID(int PurchaseID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "0aab7822-e327-4dcd-bc30-4cbf289067e4"))
            {
                return MyDAL.GetSupplierAgentProtocalByPurchaseID(PurchaseID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：0aab7822-e327-4dcd-bc30-4cbf289067e4错误");
            } 
           
        }

        public virtual IList<SupplierAgentProtocalInfo> GetSupplierAgentProtocals(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "0aab7822-e327-4dcd-bc30-4cbf289067e4"))
            {
                return MyDAL.GetSupplierAgentProtocals(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：0aab7822-e327-4dcd-bc30-4cbf289067e4错误");
            } 
          
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "0aab7822-e327-4dcd-bc30-4cbf289067e4"))
            {
                return MyDAL.GetPageInfo(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：0aab7822-e327-4dcd-bc30-4cbf289067e4错误");
            } 
           
        }

    }
}
