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
    public class OrdersAccompanying : IOrdersAccompanying
    {
        protected DAL.ORD.IOrdersAccompanying MyDAL;
        protected IRBAC RBAC;

        public OrdersAccompanying()
        {
            MyDAL = DAL.ORD.OrdersAccompanyingFactory.CreateOrdersAccompanying();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddOrdersAccompanying(OrdersAccompanyingInfo entity,string[] imgArr)
        {
            return MyDAL.AddOrdersAccompanying(entity, imgArr);
        }

        public virtual bool EditOrdersAccompanying(OrdersAccompanyingInfo entity)
        {
            return MyDAL.EditOrdersAccompanying(entity);
        }

        public virtual int DelOrdersAccompanying(int ID)
        {
            return MyDAL.DelOrdersAccompanying(ID);
        }

        public virtual OrdersAccompanyingInfo GetOrdersAccompanyingByID(int ID)
        {
            return MyDAL.GetOrdersAccompanyingByID(ID);
        }

        public virtual IList<OrdersAccompanyingInfo> GetOrdersAccompanyings(QueryInfo Query)
        {
            return MyDAL.GetOrdersAccompanyings(Query);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }

        public OrdersAccompanyingInfo GetOrdersAccompanyingBySN(string sn)
        {
            return MyDAL.GetOrdersAccompanyingBySN(sn);
        }


        public IList<OrdersAccompanyingInfo> GetOrdersAccompanyingsByOrders(int Orders_ID)
        {
            return MyDAL.GetOrdersAccompanyingsByOrders(Orders_ID);
        }


        public IList<OrdersAccompanyingImgInfo> GetOrdersAccompanyingImgsByAccompanyID(int Accompanying_ID)
        {
            return MyDAL.GetOrdersAccompanyingImgsByAccompanyID(Accompanying_ID);
        }
    }

    public class OrdersAccompanyingGoods : IOrdersAccompanyingGoods
    {
        protected DAL.ORD.IOrdersAccompanyingGoods MyDAL;
        protected IRBAC RBAC;

        public OrdersAccompanyingGoods()
        {
            MyDAL = DAL.ORD.OrdersAccompanyingGoodsFactory.CreateOrdersAccompanyingGoods();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddOrdersAccompanyingGoods(OrdersAccompanyingGoodsInfo entity)
        {
            return MyDAL.AddOrdersAccompanyingGoods(entity);
        }

        public virtual bool EditOrdersAccompanyingGoods(OrdersAccompanyingGoodsInfo entity)
        {
            return MyDAL.EditOrdersAccompanyingGoods(entity);
        }

        public virtual int DelOrdersAccompanyingGoods(int ID)
        {
            return MyDAL.DelOrdersAccompanyingGoods(ID);
        }

        public virtual OrdersAccompanyingGoodsInfo GetOrdersAccompanyingGoodsByID(int ID)
        {
            return MyDAL.GetOrdersAccompanyingGoodsByID(ID);
        }

        public virtual IList<OrdersAccompanyingGoodsInfo> GetOrdersAccompanyingGoodss(QueryInfo Query)
        {
            return MyDAL.GetOrdersAccompanyingGoodss(Query);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }
    }
}

