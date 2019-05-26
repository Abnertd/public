using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glaer.Trade.B2C.RBAC;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public class SupplierSubAccount : ISupplierSubAccount
    {
        protected DAL.MEM.ISupplierSubAccount MyDAL;
        protected IRBAC RBAC;

        public SupplierSubAccount()
        {
            MyDAL = DAL.MEM.SupplierSubAccountFactory.CreateSupplierSubAccount();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddSupplierSubAccount(SupplierSubAccountInfo entity)
        {
            return MyDAL.AddSupplierSubAccount(entity);
        }

        public virtual bool EditSupplierSubAccount(SupplierSubAccountInfo entity)
        {
            return MyDAL.EditSupplierSubAccount(entity);
        }

        public virtual int DelSupplierSubAccount(int ID)
        {
            return MyDAL.DelSupplierSubAccount(ID);
        }

        public virtual SupplierSubAccountInfo SubAccountLogin(string name)
        {
            return MyDAL.GetSupplierSubAccountByName(name);
        }

        public virtual SupplierSubAccountInfo GetSupplierSubAccountByID(int ID)
        {
            return MyDAL.GetSupplierSubAccountByID(ID);
        }

        public virtual IList<SupplierSubAccountInfo> GetSupplierSubAccounts(QueryInfo Query)
        {
            return MyDAL.GetSupplierSubAccounts(Query);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }

    }
}
