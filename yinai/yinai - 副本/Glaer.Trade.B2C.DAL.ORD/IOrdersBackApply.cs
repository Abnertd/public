using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.Util.Tools;

namespace Glaer.Trade.B2C.DAL.ORD
{
    public interface IOrdersBackApply
    {
        bool AddOrdersBackApply(OrdersBackApplyInfo entity);

        bool EditOrdersBackApply(OrdersBackApplyInfo entity);

        int DelOrdersBackApply(int ID);

        OrdersBackApplyInfo GetOrdersBackApplyByID(int ID);

        IList<OrdersBackApplyInfo> GetOrdersBackApplys(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);
    }
}
