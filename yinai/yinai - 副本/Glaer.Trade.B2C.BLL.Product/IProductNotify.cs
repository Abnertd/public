using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.RBAC;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.B2C.DAL;

namespace Glaer.Trade.B2C.BLL.Product
{
    public interface IProductNotify
    {
        bool AddProductNotify(ProductNotifyInfo entity);

        bool EditProductNotify(ProductNotifyInfo entity, RBACUserInfo UserPrivilege);

        int DelProductNotify(int ID, RBACUserInfo UserPrivilege);

        ProductNotifyInfo GetProductNotifyByID(int ID, RBACUserInfo UserPrivilege);

        IList<ProductNotifyInfo> GetProductNotifys(QueryInfo Query, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);

    }
}
