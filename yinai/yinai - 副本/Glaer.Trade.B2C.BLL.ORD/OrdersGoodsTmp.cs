using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.Util.Mail;
using Glaer.Trade.B2C.DAL;

namespace Glaer.Trade.B2C.BLL.ORD
{
    public class OrdersGoodsTmp : IOrdersGoodsTmp
    {
        protected DAL.ORD.IOrdersGoodsTmp MyDAL;

        public OrdersGoodsTmp()
        {
            MyDAL = DAL.ORD.OrdersGoodsTmpFactory.CreateOrdersGoodsTmp();
        }

        public virtual bool AddOrdersGoodsTmp(OrdersGoodsTmpInfo entity)
        {
            return MyDAL.AddOrdersGoodsTmp(entity);
        }

        public virtual bool EditOrdersGoodsTmp(OrdersGoodsTmpInfo entity)
        {
            return MyDAL.EditOrdersGoodsTmp(entity);
        }

        public virtual int DelOrdersGoodsTmp(int ID, int goods_type, int parent_id, string sessionid)
        {
            return MyDAL.DelOrdersGoodsTmp(ID, goods_type,parent_id,sessionid);
        }

        public virtual int ClearOrdersGoodsTmp(string sessionid)
        {
            return MyDAL.ClearOrdersGoodsTmp(sessionid);
        }

        public virtual int ClearOrdersGoodsTmp(string sessionid, int supplyid)
        {
            return MyDAL.ClearOrdersGoodsTmp(sessionid, supplyid);
        }

        public virtual int ClearOrdersGoodsTmpByOrdersID(int Orders_ID)
        {
            return MyDAL.ClearOrdersGoodsTmpByOrdersID(Orders_ID);
        }

        public virtual int ClearOrdersGoodsTmpByGoodsID(string Goods_IDs, int supplier_id)
        {
            return MyDAL.ClearOrdersGoodsTmpByGoodsID(Goods_IDs,supplier_id);
        }

        public virtual int ClearOrdersGoodsTmpByGoodsID(string Goods_IDs)
        {
            return MyDAL.ClearOrdersGoodsTmpByGoodsID(Goods_IDs);
        }

        public virtual OrdersGoodsTmpInfo GetOrdersGoodsTmpByID(int ID)
        {
            return MyDAL.GetOrdersGoodsTmpByID(ID);
        }

        public virtual IList<OrdersGoodsTmpInfo> GetOrdersGoodsTmps(QueryInfo Query)
        {
            return MyDAL.GetOrdersGoodsTmps(Query);
        }

        public virtual IList<OrdersGoodsTmpInfo> GetOrdersGoodsTmpsByOrdersID(int Orders_ID)
        {
            return MyDAL.GetOrdersGoodsTmpsByOrdersID(Orders_ID);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }

        public virtual int Get_Orders_Goods_Amount(string sessionid, int product_id)
        {
            return MyDAL.Get_Orders_Goods_Amount(sessionid, product_id);
        }

        public virtual int Get_Orders_Goods_TypeAmount(string sessionid, int product_id, int product_type)
        {
            return MyDAL.Get_Orders_Goods_TypeAmount(sessionid, product_id, product_type);
        }

        public virtual int Get_Orders_Goods_PackageAmount(string sessionid, int package_id)
        {
            return MyDAL.Get_Orders_Goods_PackageAmount(sessionid, package_id);
        }

        public virtual int Get_Orders_Goods_ParentID(string sessionid, int product_id, int product_type)
        {
            return MyDAL.Get_Orders_Goods_ParentID(sessionid, product_id, product_type);
        }

        public string GetOrdersGoodsTmpSupplierID(string sessionid, int buyer_id)
        {
            return MyDAL.GetOrdersGoodsTmpSupplierID(sessionid,buyer_id);
        }
    }
}

