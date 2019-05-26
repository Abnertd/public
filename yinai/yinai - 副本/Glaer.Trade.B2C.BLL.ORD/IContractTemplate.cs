using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;

namespace Glaer.Trade.B2C.BLL.ORD
{

    public interface IContractTemplate
    {
        bool AddContractTemplate(ContractTemplateInfo entity, RBACUserInfo UserPrivilege);

        bool EditContractTemplate(ContractTemplateInfo entity, RBACUserInfo UserPrivilege);

        int DelContractTemplate(int ID, RBACUserInfo UserPrivilege);

        ContractTemplateInfo GetContractTemplateByID(int ID, RBACUserInfo UserPrivilege);

        ContractTemplateInfo GetContractTemplateBySign(string Sign, RBACUserInfo UserPrivilege);

        IList<ContractTemplateInfo> GetContractTemplates(QueryInfo Query, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);

    }
}
