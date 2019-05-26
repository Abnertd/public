using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;

namespace Glaer.Trade.B2C.DAL.ORD
{
    public interface IOrdersBackApplyProduct
    {
        bool AddOrdersBackApplyProduct(OrdersBackApplyProductInfo entity);

        bool EditOrdersBackApplyProduct(OrdersBackApplyProductInfo entity);

        int DelOrdersBackApplyProduct(int ID);

        int DelOrdersBackApplyProductByApplyID(int ID); 

        OrdersBackApplyProductInfo GetOrdersBackApplyProductByID(int ID);

        IList<OrdersBackApplyProductInfo> GetOrdersBackApplyProductByApplyID(int ID);

        PageInfo GetPageInfo(QueryInfo Query);
    }

}
