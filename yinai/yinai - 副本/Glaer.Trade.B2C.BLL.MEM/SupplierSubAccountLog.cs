using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.RBAC;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public class SupplierSubAccountLog : ISupplierSubAccountLog
    {
        protected DAL.MEM.ISupplierSubAccountLog MyDAL;
        protected IRBAC RBAC;

        public SupplierSubAccountLog()
        {
            MyDAL = DAL.MEM.SupplierSubAccountLogFactory.CreateSupplierSubAccountLog();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddSupplierSubAccountLog(SupplierSubAccountLogInfo entity)
        {
            return MyDAL.AddSupplierSubAccountLog(entity);
        }

        public virtual bool EditSupplierSubAccountLog(SupplierSubAccountLogInfo entity)
        {
            return MyDAL.EditSupplierSubAccountLog(entity);
        }

        public virtual int DelSupplierSubAccountLog(int ID)
        {
            return MyDAL.DelSupplierSubAccountLog(ID);
        }

        public virtual SupplierSubAccountLogInfo GetSupplierSubAccountLogByID(int ID
            )
        {
            return MyDAL.GetSupplierSubAccountLogByID(ID);
        }

        public virtual IList<SupplierSubAccountLogInfo> GetSupplierSubAccountLogs(QueryInfo Query)
        {
            return MyDAL.GetSupplierSubAccountLogs(Query);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }

    }
}
