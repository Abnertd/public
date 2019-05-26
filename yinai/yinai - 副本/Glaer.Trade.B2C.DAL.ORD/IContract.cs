using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;

namespace Glaer.Trade.B2C.DAL.ORD
{
    public interface IContract
    {
        bool AddContract(ContractInfo entity);

        bool EditContract(ContractInfo entity);

        int DelContract(int ID);

        ContractInfo GetContractByID(int ID);

        ContractInfo GetContractBySn(string Sn);

        IList<ContractInfo> GetContracts(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);

        int GetContractAmount(string Status, string Sn_Front);

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
