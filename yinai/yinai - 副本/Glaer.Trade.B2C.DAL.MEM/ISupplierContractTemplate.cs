using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public interface ISupplierContractTemplate
    {
        bool AddSupplierContractTemplate(SupplierContractTemplateInfo entity);

        bool EditSupplierContractTemplate(SupplierContractTemplateInfo entity);

        int DelSupplierContractTemplate(int ID);

        SupplierContractTemplateInfo GetSupplierContractTemplateByID(int ID);

        IList<SupplierContractTemplateInfo> GetSupplierContractTemplates(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);
    }

}
