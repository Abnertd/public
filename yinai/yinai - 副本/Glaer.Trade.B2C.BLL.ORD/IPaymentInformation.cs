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
    public interface IPaymentInformation
    {
        bool AddPaymentInformation(PaymentInformationInfo entity);

        bool EditPaymentInformation(PaymentInformationInfo entity);

        int DelPaymentInformation(int ID);

        PaymentInformationInfo GetPaymentInformationByID(int ID);

        IList<PaymentInformationInfo> GetPaymentInformations(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);

    }
}
