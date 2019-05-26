using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public interface ISupplierSubAccountLog
    {
        bool AddSupplierSubAccountLog(SupplierSubAccountLogInfo entity);

        bool EditSupplierSubAccountLog(SupplierSubAccountLogInfo entity);

        int DelSupplierSubAccountLog(int ID);

        SupplierSubAccountLogInfo GetSupplierSubAccountLogByID(int ID);

        IList<SupplierSubAccountLogInfo> GetSupplierSubAccountLogs(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);
    }
}
