using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.DAL;

namespace Glaer.Trade.B2C.BLL.ORD
{
    public class OrdersPayment : IOrdersPayment
    {
        protected DAL.ORD.IOrdersPayment MyDAL;

        public OrdersPayment() {
            MyDAL = DAL.ORD.OrdersPaymentFactory.CreateOrdersPayment();
        }

        public virtual bool AddOrdersPayment(OrdersPaymentInfo entity) {
            return MyDAL.AddOrdersPayment(entity);
        }

        public virtual bool EditOrdersPayment(OrdersPaymentInfo entity) {
            return MyDAL.EditOrdersPayment(entity);
        }

        public virtual int DelOrdersPayment(int ID) {
            return MyDAL.DelOrdersPayment(ID);
        }

        public virtual OrdersPaymentInfo GetOrdersPaymentByID(int ID) {
            return MyDAL.GetOrdersPaymentByID(ID);
        }

        public virtual OrdersPaymentInfo GetOrdersPaymentByOrdersID(int Orders_ID, int Payment_Status)
        {
            return MyDAL.GetOrdersPaymentByOrdersID(Orders_ID, Payment_Status);
        }

        public virtual OrdersPaymentInfo GetOrdersPaymentBySn(string SN)
        {
            return MyDAL.GetOrdersPaymentBySn(SN);
        }

        public virtual IList<OrdersPaymentInfo> GetOrdersPayments(QueryInfo Query) {
            return MyDAL.GetOrdersPayments(Query);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query) {
            return MyDAL.GetPageInfo(Query);
        }

        public virtual bool AddMemberPayLog(MemberPayLogInfo entity)
        {
            return MyDAL.AddMemberPayLog(entity);
        }

        public virtual IList<MemberPayLogInfo> GetMemberPayLogByOrdersSn(string Sn)
        {
            return MyDAL.GetMemberPayLogByOrdersSn(Sn);
        }

        public IList<OrdersPaymentInfo> GetOrdersPaymentsByOrdersID(int OrdersID)
        {
            return MyDAL.GetOrdersPaymentsByOrdersID(OrdersID);
        }
    }
}
