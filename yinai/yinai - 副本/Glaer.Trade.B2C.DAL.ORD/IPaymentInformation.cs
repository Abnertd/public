using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.Util.Tools;
//using Glaer.Trade.Util.TraceError;

namespace Glaer.Trade.B2C.DAL.ORD
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
