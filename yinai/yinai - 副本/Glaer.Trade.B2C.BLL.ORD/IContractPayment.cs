using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;

namespace Glaer.Trade.B2C.BLL.ORD
{
    public interface IContractPayment
    {
        bool AddContractPayment(ContractPaymentInfo entity);

        bool EditContractPayment(ContractPaymentInfo entity);

        int DelContractPayment(int ID);

        ContractPaymentInfo GetContractPaymentByID(int ID);

        ContractPaymentInfo GetContractPaymentBySN(string SN);

        IList<ContractPaymentInfo> GetContractPayments(QueryInfo Query);

        IList<ContractPaymentInfo> GetContractPaymentsByContractID(int ID);

        PageInfo GetPageInfo(QueryInfo Query);

    }
}
