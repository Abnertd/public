using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.Util.Mail;
using Glaer.Trade.B2C.DAL;

namespace Glaer.Trade.B2C.BLL.Product
{
    public interface IShoppingAsk
    {
        bool AddShoppingAsk(ShoppingAskInfo entity, RBACUserInfo UserPrivilege);

        bool EditShoppingAsk(ShoppingAskInfo entity, RBACUserInfo UserPrivilege);

        int DelShoppingAsk(int ID, RBACUserInfo UserPrivilege);

        ShoppingAskInfo GetShoppingAskByID(int ID, RBACUserInfo UserPrivilege);

        IList<ShoppingAskInfo> GetShoppingAsks(QueryInfo Query, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);

    }
}
