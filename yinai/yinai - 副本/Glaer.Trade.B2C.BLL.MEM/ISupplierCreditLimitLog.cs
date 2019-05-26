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
    public interface ISupplierCreditLimitLog
    {
        bool AddSupplierCreditLimitLog(SupplierCreditLimitLogInfo entity);

        bool EditSupplierCreditLimitLog(SupplierCreditLimitLogInfo entity);

        int DelSupplierCreditLimitLog(int ID);

        SupplierCreditLimitLogInfo GetSupplierCreditLimitLogByID(int ID);

        IList<SupplierCreditLimitLogInfo> GetSupplierCreditLimitLogBySupplierIDType(int Supplier_ID, int CreditLimit_Log_Type);

        IList<SupplierCreditLimitLogInfo> GetSupplierCreditLimitLogs(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);
    }
}
