using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.Util.Tools;

namespace Glaer.Trade.B2C.DAL.ORD
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
