using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;

namespace Glaer.Trade.B2C.BLL.ORD
{
    public interface IContract
    {
        bool AddContract(ContractInfo entity, RBACUserInfo UserPrivilege);

        bool EditContract(ContractInfo entity, RBACUserInfo UserPrivilege);

        ContractInfo GetContractByID(int ID, RBACUserInfo UserPrivilege);

        ContractInfo GetContractBySn(string Sn, RBACUserInfo UserPrivilege);

        IList<ContractInfo> GetContracts(QueryInfo Query, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);

        int GetContractAmount(string Status, string Sn_Front, RBACUserInfo UserPrivilege);

        bool AddContractInvoice(ContractInvoiceInfo entity);

        bool EditContractInvoice(ContractInvoiceInfo entity);

        int DelContractInvoice(int ID);

        ContractInvoiceInfo GetContractInvoiceByID(int ID);

        ContractInvoiceInfo GetContractInvoiceByContractID(int ID);

        bool AddContractInvoiceApply(ContractInvoiceApplyInfo entity);

        bool EditContractInvoiceApply(ContractInvoiceApplyInfo entity);

        int DelContractInvoiceApply(int ID);

        ContractInvoiceApplyInfo GetContractInvoiceApplyByID(int ID);

        IList<ContractInvoiceApplyInfo> GetContractInvoiceApplysByContractID(int Contract_ID);

        double Get_Contract_PayedAmount(int Contract_ID);
    }
}
