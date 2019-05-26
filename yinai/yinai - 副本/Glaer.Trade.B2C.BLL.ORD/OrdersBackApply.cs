using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.RBAC;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.B2C.DAL;

namespace Glaer.Trade.B2C.BLL.ORD
{
    public class OrdersBackApply : IOrdersBackApply
    {
        protected DAL.ORD.IOrdersBackApply MyDAL;
        protected IRBAC RBAC;

        public OrdersBackApply()
        {
            MyDAL = DAL.ORD.OrdersBackApplyFactory.CreateOrdersBackApply();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddOrdersBackApply(OrdersBackApplyInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "a0cbae74-b212-4983-b6b5-9e3e44835aa7"))
            {
                return MyDAL.AddOrdersBackApply(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：a0cbae74-b212-4983-b6b5-9e3e44835aa7错误");
            }
        }

        public virtual bool EditOrdersBackApply(OrdersBackApplyInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "1f9e3d6c-2229-4894-891b-13e73dd2e593"))
            {
                return MyDAL.EditOrdersBackApply(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：1f9e3d6c-2229-4894-891b-13e73dd2e593错误");
            }
        }

        public virtual int DelOrdersBackApply(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "2a5f3eef-36a5-4d2a-83cc-3a4ff9f084ed"))
            {
                return MyDAL.DelOrdersBackApply(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：2a5f3eef-36a5-4d2a-83cc-3a4ff9f084ed错误");
            }
        }

        public virtual OrdersBackApplyInfo GetOrdersBackApplyByID(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "aaa944b1-6068-42cd-82b5-d7f4841ecf45"))
            {
                return MyDAL.GetOrdersBackApplyByID(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：aaa944b1-6068-42cd-82b5-d7f4841ecf45错误");
            }
        }

        public virtual IList<OrdersBackApplyInfo> GetOrdersBackApplys(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "aaa944b1-6068-42cd-82b5-d7f4841ecf45"))
            {
                return MyDAL.GetOrdersBackApplys(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：aaa944b1-6068-42cd-82b5-d7f4841ecf45错误");
            }
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "aaa944b1-6068-42cd-82b5-d7f4841ecf45"))
            {
                return MyDAL.GetPageInfo(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：aaa944b1-6068-42cd-82b5-d7f4841ecf45错误");
            }
        }

    }
}

