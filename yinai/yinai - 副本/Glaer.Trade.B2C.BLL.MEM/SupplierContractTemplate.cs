using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.DAL;
using Glaer.Trade.B2C.RBAC;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public class SupplierContractTemplate : ISupplierContractTemplate
    {
        protected DAL.MEM.ISupplierContractTemplate MyDAL;
        protected IRBAC RBAC;

        public SupplierContractTemplate()
        {
            MyDAL = DAL.MEM.SupplierContractTemplateFactory.CreateSupplierContractTemplate();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddSupplierContractTemplate(SupplierContractTemplateInfo entity)
        {
            return MyDAL.AddSupplierContractTemplate(entity);
        }

        public virtual bool EditSupplierContractTemplate(SupplierContractTemplateInfo entity)
        {
            return MyDAL.EditSupplierContractTemplate(entity);
        }

        public virtual int DelSupplierContractTemplate(int ID)
        {
            return MyDAL.DelSupplierContractTemplate(ID);
        }

        public virtual SupplierContractTemplateInfo GetSupplierContractTemplateByID(int ID)
        {
            return MyDAL.GetSupplierContractTemplateByID(ID);
        }

        public virtual IList<SupplierContractTemplateInfo> GetSupplierContractTemplates(QueryInfo Query)
        {
            return MyDAL.GetSupplierContractTemplates(Query);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }

    }

}
