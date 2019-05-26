using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.Util.Tools;


namespace Glaer.Trade.B2C.DAL.MEM
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
