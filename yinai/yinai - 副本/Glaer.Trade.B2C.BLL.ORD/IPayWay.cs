using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;

namespace Glaer.Trade.B2C.BLL.ORD
{
    /// <summary>
    /// 支付方式定义
    /// </summary>
    public interface IPayWay
    {
        bool AddPayWay(PayWayInfo entity, RBACUserInfo UserPrivilege);

        bool EditPayWay(PayWayInfo entity, RBACUserInfo UserPrivilege);

        int DelPayWay(int ID, RBACUserInfo UserPrivilege);

        PayWayInfo GetPayWayByID(int ID, RBACUserInfo UserPrivilege);

        IList<PayWayInfo> GetPayWays(QueryInfo Query, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);

        IList<PayInfo> GetPaysBySite(string siteCode, RBACUserInfo UserPrivilege);

        PayInfo GetPayByID(int ID, RBACUserInfo UserPrivilege);
    }
}
