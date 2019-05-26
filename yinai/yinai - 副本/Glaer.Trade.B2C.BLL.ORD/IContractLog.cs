using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;

namespace Glaer.Trade.B2C.BLL.ORD
{
    public interface IContractLog
    {
        bool AddContractLog(ContractLogInfo entity);

        bool EditContractLog(ContractLogInfo entity);

        int DelContractLog(int ID);

        ContractLogInfo GetContractLogByID(int ID);

        IList<ContractLogInfo> GetContractLogs(QueryInfo Query);

        IList<ContractLogInfo> GetContractLogsByContractID(int Contract_ID);

        PageInfo GetPageInfo(QueryInfo Query);

    }
}
