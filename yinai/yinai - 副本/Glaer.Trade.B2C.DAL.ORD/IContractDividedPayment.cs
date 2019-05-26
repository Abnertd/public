using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;

namespace Glaer.Trade.B2C.DAL.ORD
{
    public interface IContractDividedPayment
    {
        bool AddContractDividedPayment(ContractDividedPaymentInfo entity);

        bool EditContractDividedPayment(ContractDividedPaymentInfo entity);

        int DelContractDividedPayment(int ID);

        ContractDividedPaymentInfo GetContractDividedPaymentByID(int ID);

        IList<ContractDividedPaymentInfo> GetContractDividedPaymentsByContractID(int ContractID);

        IList<ContractDividedPaymentInfo> GetContractDividedPayments(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);
    }

}
