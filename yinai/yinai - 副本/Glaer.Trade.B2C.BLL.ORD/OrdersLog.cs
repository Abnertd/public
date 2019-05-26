using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.DAL;

namespace Glaer.Trade.B2C.BLL.ORD
{
    public class OrdersLog : IOrdersLog
    {
        protected DAL.ORD.IOrdersLog MyDAL;

        public OrdersLog() {
            MyDAL = DAL.ORD.OrdersLogFactory.CreateOrdersLog();
        }

        public virtual bool AddOrdersLog(OrdersLogInfo entity) {
            return MyDAL.AddOrdersLog(entity);
        }

        public virtual bool EditOrdersLog(OrdersLogInfo entity) {
            return MyDAL.EditOrdersLog(entity);
        }

        public virtual int DelOrdersLog(int ID) {
            return MyDAL.DelOrdersLog(ID);
        }

        public virtual OrdersLogInfo GetOrdersLogByID(int ID) {
            return MyDAL.GetOrdersLogByID(ID);
        }

        public virtual IList<OrdersLogInfo> GetOrdersLogsByOrdersID(int ID) {
            return MyDAL.GetOrdersLogsByOrdersID(ID);
        }

       
      

    }
}
