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
