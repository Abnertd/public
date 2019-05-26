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
