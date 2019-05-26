using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;

namespace Glaer.Trade.B2C.BLL.MEM
{

    public interface ISupplierContractTemplate
    {
        bool AddSupplierContractTemplate(SupplierContractTemplateInfo entitye);

        bool EditSupplierContractTemplate(SupplierContractTemplateInfo entitye);

        int DelSupplierContractTemplate(int IDe);

        SupplierContractTemplateInfo GetSupplierContractTemplateByID(int IDe);

        IList<SupplierContractTemplateInfo> GetSupplierContractTemplates(QueryInfo Querye);

        PageInfo GetPageInfo(QueryInfo Querye);

    }
}
