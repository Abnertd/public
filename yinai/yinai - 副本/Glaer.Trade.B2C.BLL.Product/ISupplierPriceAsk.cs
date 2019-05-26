using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;

namespace Glaer.Trade.B2C.BLL.Product
{
    public interface ISupplierPriceAsk
    {
        bool AddSupplierPriceAsk(SupplierPriceAskInfo entity, RBACUserInfo UserPrivilege);

        bool EditSupplierPriceAsk(SupplierPriceAskInfo entity, RBACUserInfo UserPrivilege);

        int DelSupplierPriceAsk(int ID, RBACUserInfo UserPrivilege);

        int DelSupplierPriceAskByProductID(int ProductID, RBACUserInfo UserPrivilege);

        SupplierPriceAskInfo GetSupplierPriceAskByID(int ID, RBACUserInfo UserPrivilege);

        IList<SupplierPriceAskInfo> GetSupplierPriceAsks(QueryInfo Query, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);

    }
}
