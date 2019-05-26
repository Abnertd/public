using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;

namespace Glaer.Trade.B2C.BLL.ORD
{
    public interface IPayType
    {
        bool AddPayType(PayTypeInfo entity, RBACUserInfo UserPrivilege);

        bool EditPayType(PayTypeInfo entity, RBACUserInfo UserPrivilege);

        int DelPayType(int ID, RBACUserInfo UserPrivilege);

        PayTypeInfo GetPayTypeByID(int ID, RBACUserInfo UserPrivilege);

        IList<PayTypeInfo> GetPayTypes(QueryInfo Query, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);

    }
}
