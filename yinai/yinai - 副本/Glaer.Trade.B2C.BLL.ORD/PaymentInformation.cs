using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.RBAC;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.B2C.DAL;

namespace Glaer.Trade.B2C.BLL.ORD
{
    public class PaymentInformation : IPaymentInformation
    {
        protected DAL.ORD.IPaymentInformation MyDAL;
        protected IRBAC RBAC;

        public PaymentInformation()
        {
            MyDAL = DAL.ORD.PaymentInformationFactory.CreatePaymentInformation();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddPaymentInformation(PaymentInformationInfo entity)
        {
            return MyDAL.AddPaymentInformation(entity);
        }

        public virtual bool EditPaymentInformation(PaymentInformationInfo entity)
        {
            return MyDAL.EditPaymentInformation(entity);
        }

        public virtual int DelPaymentInformation(int ID)
        {
            return MyDAL.DelPaymentInformation(ID);
        }

        public virtual PaymentInformationInfo GetPaymentInformationByID(int ID)
        {
            return MyDAL.GetPaymentInformationByID(ID);
        }

        public virtual IList<PaymentInformationInfo> GetPaymentInformations(QueryInfo Query)
        {
            return MyDAL.GetPaymentInformations(Query);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }

    }
}

