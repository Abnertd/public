using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.DAL;
using Glaer.Trade.B2C.RBAC;

namespace Glaer.Trade.B2C.BLL.ORD
{
    public class ContractPayment : IContractPayment
    {
        protected DAL.ORD.IContractPayment MyDAL;
        protected IRBAC RBAC;

        public ContractPayment()
        {
            MyDAL = DAL.ORD.ContractPaymentFactory.CreateContractPayment();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddContractPayment(ContractPaymentInfo entity)
        {
            return MyDAL.AddContractPayment(entity);
        }

        public virtual bool EditContractPayment(ContractPaymentInfo entity)
        {
            return MyDAL.EditContractPayment(entity);
        }

        public virtual int DelContractPayment(int ID)
        {
            return MyDAL.DelContractPayment(ID);
        }

        public virtual ContractPaymentInfo GetContractPaymentByID(int ID)
        {
            return MyDAL.GetContractPaymentByID(ID);
        }

        public virtual ContractPaymentInfo GetContractPaymentBySN(string SN)
        {
            return MyDAL.GetContractPaymentBySN(SN);
        }

        public virtual IList<ContractPaymentInfo> GetContractPayments(QueryInfo Query)
        {
            return MyDAL.GetContractPayments(Query);
        }

        public virtual IList<ContractPaymentInfo> GetContractPaymentsByContractID(int ID)
        {
            return MyDAL.GetContractPaymentsByContractID(ID);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }

    }

}
