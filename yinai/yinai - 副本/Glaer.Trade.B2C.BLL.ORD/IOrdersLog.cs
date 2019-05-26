using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;

namespace Glaer.Trade.B2C.BLL.ORD
{
    public interface IOrdersLog
    {
        bool AddOrdersLog(OrdersLogInfo entity);

        bool EditOrdersLog(OrdersLogInfo entity);

        int DelOrdersLog(int ID);

        OrdersLogInfo GetOrdersLogByID(int ID);

        IList<OrdersLogInfo> GetOrdersLogsByOrdersID(int ID);
    }
}
