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
    public interface IOrdersInvoice
    {
        bool AddOrdersInvoice(OrdersInvoiceInfo entity);

        bool EditOrdersInvoice(OrdersInvoiceInfo entity);

        int DelOrdersInvoice(int ID);

        OrdersInvoiceInfo GetOrdersInvoiceByID(int ID);

        IList<OrdersInvoiceInfo> GetOrdersInvoices(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);

        OrdersInvoiceInfo GetOrdersInvoiceByOrdersID(int ID);

    }
}
