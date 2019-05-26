using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.DAL;
using Glaer.Trade.B2C.RBAC;

namespace Glaer.Trade.B2C.BLL.ORD
{
    public class ContractDividedPayment : IContractDividedPayment
    {
        protected DAL.ORD.IContractDividedPayment MyDAL;
        protected IRBAC RBAC;

        public ContractDividedPayment()
        {
            MyDAL = DAL.ORD.ContractDividedPaymentFactory.CreateContractDividedPayment();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddContractDividedPayment(ContractDividedPaymentInfo entity)
        {
            return MyDAL.AddContractDividedPayment(entity);
        }

        public virtual bool EditContractDividedPayment(ContractDividedPaymentInfo entity)
        {
            return MyDAL.EditContractDividedPayment(entity);
        }

        public virtual int DelContractDividedPayment(int ID)
        {
            return MyDAL.DelContractDividedPayment(ID);
        }

        public virtual ContractDividedPaymentInfo GetContractDividedPaymentByID(int ID)
        {
            return MyDAL.GetContractDividedPaymentByID(ID);
        }

        public virtual IList<ContractDividedPaymentInfo> GetContractDividedPaymentsByContractID(int ContractID)
        {
            return MyDAL.GetContractDividedPaymentsByContractID(ContractID);
        }

        public virtual IList<ContractDividedPaymentInfo> GetContractDividedPayments(QueryInfo Query)
        {
            return MyDAL.GetContractDividedPayments(Query);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }

    }

}
