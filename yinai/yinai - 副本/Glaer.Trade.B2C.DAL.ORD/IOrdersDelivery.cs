using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;

namespace Glaer.Trade.B2C.DAL.ORD
{
    public interface IOrdersDelivery
    {
        bool AddOrdersDelivery(OrdersDeliveryInfo entity);

        bool EditOrdersDelivery(OrdersDeliveryInfo entity);

        int DelOrdersDelivery(int ID);

        OrdersDeliveryInfo GetOrdersDeliveryByID(int ID);

        OrdersDeliveryInfo GetOrdersDeliveryBySn(string SN);

        OrdersDeliveryInfo GetOrdersDeliveryByOrdersID(int Orders_ID, int Delivery_Status);

        IList<OrdersDeliveryInfo> GetOrdersDeliverys(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);

        bool AddOrdersDeliveryGoods(IList<OrdersDeliveryGoodsInfo> entitys);

        bool EditOrdersDeliveryGoods(OrdersDeliveryGoodsInfo entity);

        int DelOrdersDeliveryGoods(int ID);

        OrdersDeliveryGoodsInfo GetOrdersDeliveryGoodsByID(int ID);

        IList<OrdersDeliveryGoodsInfo> GetOrdersDeliveryGoods(int DeliveryID);
    }

}
