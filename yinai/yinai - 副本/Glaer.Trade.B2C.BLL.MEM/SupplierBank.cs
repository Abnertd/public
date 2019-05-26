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
    public class SupplierBank : ISupplierBank
    {
        protected DAL.MEM.ISupplierBank MyDAL;
        protected IRBAC RBAC;

        public SupplierBank()
        {
            MyDAL = DAL.MEM.SupplierBankFactory.CreateSupplierBank();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddSupplierBank(SupplierBankInfo entity)
        {
            return MyDAL.AddSupplierBank(entity);
        }

        public virtual bool EditSupplierBank(SupplierBankInfo entity)
        {
            return MyDAL.EditSupplierBank(entity);
        }

        public virtual int DelSupplierBank(int ID)
        {
            return MyDAL.DelSupplierBank(ID);
        }

        public virtual SupplierBankInfo GetSupplierBankByID(int ID)
        {
            return MyDAL.GetSupplierBankByID(ID);
        }

        public virtual SupplierBankInfo GetSupplierBankBySupplierID(int ID)
        {
            return MyDAL.GetSupplierBankBySupplierID(ID);
        }

        public virtual IList<SupplierBankInfo> GetSupplierBanks(QueryInfo Query)
        {
            return MyDAL.GetSupplierBanks(Query);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }

    }

}
