using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;

namespace Glaer.Trade.B2C.DAL.ORD
{
    /// <summary>
    /// 支付方式定义
    /// </summary>
    public interface IPayWay
    {
        bool AddPayWay(PayWayInfo entity);

        bool EditPayWay(PayWayInfo entity);

        int DelPayWay(int ID);

        PayWayInfo GetPayWayByID(int ID);

        IList<PayWayInfo> GetPayWays(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);

        IList<PayInfo> GetPaysBySite(string siteCode);

        PayInfo GetPayByID(int ID);
    }

}
