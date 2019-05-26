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
    public class SupplierInvoice : ISupplierInvoice
    {
        protected DAL.MEM.ISupplierInvoice MyDAL;
        protected IRBAC RBAC;

        public SupplierInvoice()
        {
            MyDAL = DAL.MEM.SupplierInvoiceFactory.CreateSupplierInvoice();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddSupplierInvoice(SupplierInvoiceInfo entity)
        {
            return MyDAL.AddSupplierInvoice(entity);
        }

        public virtual bool EditSupplierInvoice(SupplierInvoiceInfo entity)
        {
            return MyDAL.EditSupplierInvoice(entity);
        }

        public virtual int DelSupplierInvoice(int ID)
        {
            return MyDAL.DelSupplierInvoice(ID);
        }

        public virtual SupplierInvoiceInfo GetSupplierInvoiceByID(int ID)
        {
            return MyDAL.GetSupplierInvoiceByID(ID);
        }

        public virtual IList<SupplierInvoiceInfo> GetSupplierInvoicesBySupplierID(int SupplierID)
        {
            return MyDAL.GetSupplierInvoicesBySupplierID(SupplierID);
        }

        public virtual IList<SupplierInvoiceInfo> GetSupplierInvoices(QueryInfo Query)
        {
            return MyDAL.GetSupplierInvoices(Query);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }

    }



}

