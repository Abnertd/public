using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.RBAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public class Logistics : ILogistics
    {
        protected DAL.MEM.ILogistics MyDAL;
        protected IRBAC RBAC;

        public Logistics()
        {
            MyDAL = DAL.MEM.LogisticsFactory.CreateLogistics();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddLogistics(LogisticsInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "5793c951-d2ee-45f8-9c31-35fcb853819d"))
            {
            return MyDAL.AddLogistics(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：5793c951-d2ee-45f8-9c31-35fcb853819d错误");
            }
        }

        public virtual bool EditLogistics(LogisticsInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "bd38ff8b-f627-44ec-9275-39c9df7425e1"))
            {
            return MyDAL.EditLogistics(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：bd38ff8b-f627-44ec-9275-39c9df7425e1错误");
            }
        }

        public virtual int DelLogistics(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "dcfc8ade-7987-40c0-8591-a33c2a603e61"))
            {
            return MyDAL.DelLogistics(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：dcfc8ade-7987-40c0-8591-a33c2a603e61错误");
            }
        }

        public virtual LogisticsInfo GetLogisticsByID(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "8426b82b-1be6-4d27-84a7-9d45597be557"))
            {
            return MyDAL.GetLogisticsByID(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：8426b82b-1be6-4d27-84a7-9d45597be557错误");
            }
        }

        public virtual LogisticsInfo GetLogisticsByNickName(string NickName, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "8426b82b-1be6-4d27-84a7-9d45597be557"))
            {
            return MyDAL.GetLogisticsByNickName(NickName);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：8426b82b-1be6-4d27-84a7-9d45597be557错误");
            }
        }

        public virtual IList<LogisticsInfo> GetLogisticss(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "8426b82b-1be6-4d27-84a7-9d45597be557"))
            {
            return MyDAL.GetLogisticss(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：8426b82b-1be6-4d27-84a7-9d45597be557错误");
            }
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "8426b82b-1be6-4d27-84a7-9d45597be557"))
            {
            return MyDAL.GetPageInfo(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：8426b82b-1be6-4d27-84a7-9d45597be557错误");
            }
        }

    }
}
