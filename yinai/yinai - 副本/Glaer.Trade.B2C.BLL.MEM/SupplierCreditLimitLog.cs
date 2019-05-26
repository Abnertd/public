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
    public class SupplierCreditLimitLog : ISupplierCreditLimitLog
    {
        protected DAL.MEM.ISupplierCreditLimitLog MyDAL;
        protected IRBAC RBAC;

        public SupplierCreditLimitLog()
        {
            MyDAL = DAL.MEM.SupplierCreditLimitLogFactory.CreateSupplierCreditLimitLog();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddSupplierCreditLimitLog(SupplierCreditLimitLogInfo entity)
        {
            return MyDAL.AddSupplierCreditLimitLog(entity);
        }

        public virtual bool EditSupplierCreditLimitLog(SupplierCreditLimitLogInfo entity)
        {
            return MyDAL.EditSupplierCreditLimitLog(entity);
        }

        public virtual int DelSupplierCreditLimitLog(int ID)
        {
            return MyDAL.DelSupplierCreditLimitLog(ID);
        }

        public virtual SupplierCreditLimitLogInfo GetSupplierCreditLimitLogByID(int ID)
        {
            return MyDAL.GetSupplierCreditLimitLogByID(ID);
        }

        public virtual IList<SupplierCreditLimitLogInfo> GetSupplierCreditLimitLogBySupplierIDType(int Supplier_ID, int CreditLimit_Log_Type)
        {
            return MyDAL.GetSupplierCreditLimitLogBySupplierIDType(Supplier_ID, CreditLimit_Log_Type);
        }

        public virtual IList<SupplierCreditLimitLogInfo> GetSupplierCreditLimitLogs(QueryInfo Query)
        {
            return MyDAL.GetSupplierCreditLimitLogs(Query);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }

    }

}

