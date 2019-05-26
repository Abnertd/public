using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.DAL;
using Glaer.Trade.B2C.RBAC;

namespace Glaer.Trade.B2C.BLL.ORD
{
    public class ContractLog : IContractLog
    {
        protected DAL.ORD.IContractLog MyDAL;
        protected IRBAC RBAC;

        public ContractLog()
        {
            MyDAL = DAL.ORD.ContractLogFactory.CreateContractLog();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddContractLog(ContractLogInfo entity)
        {
            return MyDAL.AddContractLog(entity);
        }

        public virtual bool EditContractLog(ContractLogInfo entity)
        {
            return MyDAL.EditContractLog(entity);
        }

        public virtual int DelContractLog(int ID)
        {
            return MyDAL.DelContractLog(ID);
        }

        public virtual ContractLogInfo GetContractLogByID(int ID)
        {
            return MyDAL.GetContractLogByID(ID);
        }

        public virtual IList<ContractLogInfo> GetContractLogs(QueryInfo Query)
        {
            return MyDAL.GetContractLogs(Query);
        }

        public virtual IList<ContractLogInfo> GetContractLogsByContractID(int Contract_ID)
        {
            return MyDAL.GetContractLogsByContractID(Contract_ID);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }

    }

}
