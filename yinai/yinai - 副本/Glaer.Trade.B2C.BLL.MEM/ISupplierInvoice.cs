using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.RBAC;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.B2C.DAL;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public interface ISupplierInvoice
    {
        bool AddSupplierInvoice(SupplierInvoiceInfo entity);

        bool EditSupplierInvoice(SupplierInvoiceInfo entity);

        int DelSupplierInvoice(int ID);

        SupplierInvoiceInfo GetSupplierInvoiceByID(int ID);

        IList<SupplierInvoiceInfo> GetSupplierInvoicesBySupplierID(int SupplierID);

        IList<SupplierInvoiceInfo> GetSupplierInvoices(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);

    }
}
