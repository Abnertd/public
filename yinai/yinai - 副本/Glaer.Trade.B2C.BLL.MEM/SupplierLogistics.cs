using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.RBAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public class SupplierLogistics : ISupplierLogistics
    {
        protected DAL.MEM.ISupplierLogistics MyDAL;
        protected IRBAC RBAC;

        public SupplierLogistics()
        {
            MyDAL = DAL.MEM.SupplierLogisticsFactory.CreateSupplierLogistics();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddSupplierLogistics(SupplierLogisticsInfo entity)
        {
            return MyDAL.AddSupplierLogistics(entity);
        }

        public virtual bool EditSupplierLogistics(SupplierLogisticsInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "65632742-f14a-4e44-8f7d-64e56c866da4"))
            {
            return MyDAL.EditSupplierLogistics(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：65632742-f14a-4e44-8f7d-64e56c866da4错误");
            }
        }

        public virtual int DelSupplierLogistics(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "f8ed06ef-22c6-4223-995d-0593bccabf8f"))
            {
            return MyDAL.DelSupplierLogistics(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：f8ed06ef-22c6-4223-995d-0593bccabf8f错误");
            }
        }

        public virtual SupplierLogisticsInfo GetSupplierLogisticsByID(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "64bb04aa-9b78-4c41-ae9c-e94f57581e22"))
            {
            return MyDAL.GetSupplierLogisticsByID(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：64bb04aa-9b78-4c41-ae9c-e94f57581e22错误");
            }
        }

        public virtual IList<SupplierLogisticsInfo> GetSupplierLogisticss(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "64bb04aa-9b78-4c41-ae9c-e94f57581e22"))
            {
            return MyDAL.GetSupplierLogisticss(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：64bb04aa-9b78-4c41-ae9c-e94f57581e22错误");
            }
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "64bb04aa-9b78-4c41-ae9c-e94f57581e22"))
            {
            return MyDAL.GetPageInfo(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：64bb04aa-9b78-4c41-ae9c-e94f57581e22错误");
            }
        }

    }

    public class LogisticsTender : ILogisticsTender
    {
        protected DAL.MEM.ILogisticsTender MyDAL;
        protected IRBAC RBAC;

        public LogisticsTender()
        {
            MyDAL = DAL.MEM.LogisticsTenderFactory.CreateLogisticsTender();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddLogisticsTender(LogisticsTenderInfo entity)
        {
            return MyDAL.AddLogisticsTender(entity);
        }

        public virtual bool EditLogisticsTender(LogisticsTenderInfo entity)
        {
            return MyDAL.EditLogisticsTender(entity);
        }

        public virtual int DelLogisticsTender(int ID)
        {
            return MyDAL.DelLogisticsTender(ID);
        }

        public virtual LogisticsTenderInfo GetLogisticsTenderByID(int ID)
        {
            return MyDAL.GetLogisticsTenderByID(ID);
        }

        public virtual IList<LogisticsTenderInfo> GetLogisticsTenders(QueryInfo Query)
        {
            return MyDAL.GetLogisticsTenders(Query);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }

    }
}
