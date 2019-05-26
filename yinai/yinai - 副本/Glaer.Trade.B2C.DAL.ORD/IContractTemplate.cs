using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;

namespace Glaer.Trade.B2C.DAL.ORD
{
    public interface IContractTemplate
    {
        bool AddContractTemplate(ContractTemplateInfo entity);

        bool EditContractTemplate(ContractTemplateInfo entity);

        int DelContractTemplate(int ID);

        ContractTemplateInfo GetContractTemplateByID(int ID);

        ContractTemplateInfo GetContractTemplateBySign(string Sign);

        IList<ContractTemplateInfo> GetContractTemplates(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);
    }

}
