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
    public class OrdersInvoice : IOrdersInvoice
    {
        protected DAL.ORD.IOrdersInvoice MyDAL;

        public OrdersInvoice()
        {
            MyDAL = DAL.ORD.OrdersInvoiceFactory.CreateOrdersInvoice();
        }

        public virtual bool AddOrdersInvoice(OrdersInvoiceInfo entity)
        {
            return MyDAL.AddOrdersInvoice(entity);
        }

        public virtual bool EditOrdersInvoice(OrdersInvoiceInfo entity)
        {
            return MyDAL.EditOrdersInvoice(entity);
        }

        public virtual int DelOrdersInvoice(int ID)
        {
            return MyDAL.DelOrdersInvoice(ID);
        }

        public virtual OrdersInvoiceInfo GetOrdersInvoiceByID(int ID)
        {
            return MyDAL.GetOrdersInvoiceByID(ID);
        }

        public virtual OrdersInvoiceInfo GetOrdersInvoiceByOrdersID(int ID)
        {
            return MyDAL.GetOrdersInvoiceByOrdersID(ID);
        }

        public virtual IList<OrdersInvoiceInfo> GetOrdersInvoices(QueryInfo Query)
        {
            return MyDAL.GetOrdersInvoices(Query);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }

    }
}

