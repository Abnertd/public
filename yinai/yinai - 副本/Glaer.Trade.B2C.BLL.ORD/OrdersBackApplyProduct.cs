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
    public class OrdersBackApplyProduct : IOrdersBackApplyProduct
    {
        protected DAL.ORD.IOrdersBackApplyProduct MyDAL;
        protected IRBAC RBAC;

        public OrdersBackApplyProduct()
        {
            MyDAL = DAL.ORD.OrdersBackApplyProductFactory.CreateOrdersBackApplyProduct();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddOrdersBackApplyProduct(OrdersBackApplyProductInfo entity)
        {
            return MyDAL.AddOrdersBackApplyProduct(entity);
        }

        public virtual bool EditOrdersBackApplyProduct(OrdersBackApplyProductInfo entity)
        {
            return MyDAL.EditOrdersBackApplyProduct(entity);
        }

        public virtual int DelOrdersBackApplyProduct(int ID)
        {
            return MyDAL.DelOrdersBackApplyProduct(ID);
        }

        public virtual int DelOrdersBackApplyProductByApplyID(int ID)
        {
            return MyDAL.DelOrdersBackApplyProductByApplyID(ID);
        }

        public virtual OrdersBackApplyProductInfo GetOrdersBackApplyProductByID(int ID)
        {
            return MyDAL.GetOrdersBackApplyProductByID(ID);
        }

        public virtual IList<OrdersBackApplyProductInfo> GetOrdersBackApplyProductByApplyID(int ID)
        {
            return MyDAL.GetOrdersBackApplyProductByApplyID(ID);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }

    }

}
