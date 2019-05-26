using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;

namespace Glaer.Trade.B2C.BLL.ORD
{
    /// <summary>
    /// 付款单接口定义
    /// </summary>
    public interface IOrdersPayment
    {
        bool AddOrdersPayment(OrdersPaymentInfo entity);

        bool EditOrdersPayment(OrdersPaymentInfo entity);

        int DelOrdersPayment(int ID);

        OrdersPaymentInfo GetOrdersPaymentByID(int ID);

        OrdersPaymentInfo GetOrdersPaymentByOrdersID(int Orders_ID, int Payment_Status);

        OrdersPaymentInfo GetOrdersPaymentBySn(string SN);

        IList<OrdersPaymentInfo> GetOrdersPayments(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);

        bool AddMemberPayLog(MemberPayLogInfo entity);

        IList<MemberPayLogInfo> GetMemberPayLogByOrdersSn(string Sn);

        IList<OrdersPaymentInfo> GetOrdersPaymentsByOrdersID(int OrdersID);

    }
}
