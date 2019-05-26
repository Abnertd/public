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
    public interface IOrdersAccompanying
    {
        bool AddOrdersAccompanying(OrdersAccompanyingInfo entity, string[] imgArr);

        bool EditOrdersAccompanying(OrdersAccompanyingInfo entity);

        int DelOrdersAccompanying(int ID);

        OrdersAccompanyingInfo GetOrdersAccompanyingByID(int ID);

        OrdersAccompanyingInfo GetOrdersAccompanyingBySN(string sn);

        IList<OrdersAccompanyingInfo> GetOrdersAccompanyings(QueryInfo Query);

        IList<OrdersAccompanyingInfo> GetOrdersAccompanyingsByOrders(int Orders_ID);

        IList<OrdersAccompanyingImgInfo> GetOrdersAccompanyingImgsByAccompanyID(int Accompanying_ID);

        PageInfo GetPageInfo(QueryInfo Query);

    }

    public interface IOrdersAccompanyingGoods
    {
        bool AddOrdersAccompanyingGoods(OrdersAccompanyingGoodsInfo entity);

        bool EditOrdersAccompanyingGoods(OrdersAccompanyingGoodsInfo entity);

        int DelOrdersAccompanyingGoods(int ID);

        OrdersAccompanyingGoodsInfo GetOrdersAccompanyingGoodsByID(int ID);

        IList<OrdersAccompanyingGoodsInfo> GetOrdersAccompanyingGoodss(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);

    }
}
