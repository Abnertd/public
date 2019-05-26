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
    public interface ISupplierAccountLog
    {
        bool AddSupplierAccountLog(SupplierAccountLogInfo entity);

        bool EditSupplierAccountLog(SupplierAccountLogInfo entity);

        int DelSupplierAccountLog(int ID);

        SupplierAccountLogInfo GetSupplierAccountLogByID(int ID);

        IList<SupplierAccountLogInfo> GetSupplierAccountLogBySupplierIDType(int Supplier_ID, int Account_Log_Type);

        IList<SupplierAccountLogInfo> GetSupplierAccountLogs(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);
    }
}
