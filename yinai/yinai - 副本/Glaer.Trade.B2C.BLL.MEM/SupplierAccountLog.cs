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
    public class SupplierAccountLog : ISupplierAccountLog
    {
        protected DAL.MEM.ISupplierAccountLog MyDAL;
        protected IRBAC RBAC;

        public SupplierAccountLog()
        {
            MyDAL = DAL.MEM.SupplierAccountLogFactory.CreateSupplierAccountLog();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddSupplierAccountLog(SupplierAccountLogInfo entity)
        {
            return MyDAL.AddSupplierAccountLog(entity);
        }

        public virtual bool EditSupplierAccountLog(SupplierAccountLogInfo entity)
        {
            return MyDAL.EditSupplierAccountLog(entity);
        }

        public virtual int DelSupplierAccountLog(int ID)
        {
            return MyDAL.DelSupplierAccountLog(ID);
        }

        public virtual SupplierAccountLogInfo GetSupplierAccountLogByID(int ID)
        {
            return MyDAL.GetSupplierAccountLogByID(ID);
        }

        public virtual IList<SupplierAccountLogInfo> GetSupplierAccountLogBySupplierIDType(int Supplier_ID, int Account_Log_Type)
        {
            return MyDAL.GetSupplierAccountLogBySupplierIDType(Supplier_ID, Account_Log_Type);
        }

        public virtual IList<SupplierAccountLogInfo> GetSupplierAccountLogs(QueryInfo Query)
        {
            return MyDAL.GetSupplierAccountLogs(Query);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }

    }

}

