using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.DAL;
using Glaer.Trade.B2C.RBAC;

namespace Glaer.Trade.B2C.BLL.ORD
{
    public class PayWay : IPayWay
    {
        protected DAL.ORD.IPayWay MyDAL;
        protected IRBAC RBAC;
        public PayWay()
        {
            MyDAL = DAL.ORD.PayWayFactory.CreatePayWay();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddPayWay(PayWayInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "7950ffdc-827d-4432-a177-eb1d96ad4c3e"))
            {
                return MyDAL.AddPayWay(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：7950ffdc-827d-4432-a177-eb1d96ad4c3e错误");
            }
        }

        public virtual bool EditPayWay(PayWayInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "a47a2618-e716-44e3-95b4-bee4c21c34f3"))
            {
                return MyDAL.EditPayWay(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：a47a2618-e716-44e3-95b4-bee4c21c34f3错误");
            }
        }

        public virtual int DelPayWay(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "efcc1ead-ea67-4186-9fc9-4dca88d56c64"))
            {
                return MyDAL.DelPayWay(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：efcc1ead-ea67-4186-9fc9-4dca88d56c64错误");
            }
        }

        public virtual PayWayInfo GetPayWayByID(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "4484c144-8777-4852-a352-4a89ac5df06f"))
            {
                return MyDAL.GetPayWayByID(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：4484c144-8777-4852-a352-4a89ac5df06f错误");
            }
        }

        public virtual IList<PayWayInfo> GetPayWays(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "4484c144-8777-4852-a352-4a89ac5df06f"))
            {
                return MyDAL.GetPayWays(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：4484c144-8777-4852-a352-4a89ac5df06f错误");
            }
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "4484c144-8777-4852-a352-4a89ac5df06f"))
            {
                return MyDAL.GetPageInfo(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：4484c144-8777-4852-a352-4a89ac5df06f错误");
            }
        }

        public virtual IList<PayInfo> GetPaysBySite(string siteCode, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "4484c144-8777-4852-a352-4a89ac5df06f"))
            {
                return MyDAL.GetPaysBySite(siteCode);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：4484c144-8777-4852-a352-4a89ac5df06f错误");
            }
        }

        public virtual PayInfo GetPayByID(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "4484c144-8777-4852-a352-4a89ac5df06f"))
            {
                return MyDAL.GetPayByID(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：4484c144-8777-4852-a352-4a89ac5df06f错误");
            }
        }

    }

}
