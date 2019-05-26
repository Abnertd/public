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
    public interface IOrdersBackApply
    {
        bool AddOrdersBackApply(OrdersBackApplyInfo entity, RBACUserInfo UserPrivilege);

        bool EditOrdersBackApply(OrdersBackApplyInfo entity, RBACUserInfo UserPrivilege);

        int DelOrdersBackApply(int ID, RBACUserInfo UserPrivilege);

        OrdersBackApplyInfo GetOrdersBackApplyByID(int ID, RBACUserInfo UserPrivilege);

        IList<OrdersBackApplyInfo> GetOrdersBackApplys(QueryInfo Query, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);

    }
}
