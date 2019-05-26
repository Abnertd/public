using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;

namespace Glaer.Trade.B2C.DAL.ORD
{
    public interface IPayType
    {
        bool AddPayType(PayTypeInfo entity);

        bool EditPayType(PayTypeInfo entity);

        int DelPayType(int ID);

        PayTypeInfo GetPayTypeByID(int ID);

        IList<PayTypeInfo> GetPayTypes(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);
    }

}
