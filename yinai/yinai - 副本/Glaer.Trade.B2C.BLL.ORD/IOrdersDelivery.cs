using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;

namespace Glaer.Trade.B2C.BLL.ORD
{
    public interface IOrdersDelivery
    {
        bool AddOrdersDelivery(OrdersDeliveryInfo entity, RBACUserInfo UserPrivilege);

        bool EditOrdersDelivery(OrdersDeliveryInfo entity, RBACUserInfo UserPrivilege);

        int DelOrdersDelivery(int ID, RBACUserInfo UserPrivilege);

        OrdersDeliveryInfo GetOrdersDeliveryByID(int ID, RBACUserInfo UserPrivilege);

        OrdersDeliveryInfo GetOrdersDeliveryBySn(string SN, RBACUserInfo UserPrivilege);

        OrdersDeliveryInfo GetOrdersDeliveryByOrdersID(int Orders_ID, int Delivery_Status, RBACUserInfo UserPrivilege);

        IList<OrdersDeliveryInfo> GetOrdersDeliverys(QueryInfo Query, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);

        bool AddOrdersDeliveryGoods(IList<OrdersDeliveryGoodsInfo> entitys);

        bool EditOrdersDeliveryGoods(OrdersDeliveryGoodsInfo entity);

        int DelOrdersDeliveryGoods(int ID);

        OrdersDeliveryGoodsInfo GetOrdersDeliveryGoodsByID(int ID);

        IList<OrdersDeliveryGoodsInfo> GetOrdersDeliveryGoods(int DeliveryID);
    }
}
