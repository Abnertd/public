using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.RBAC;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.B2C.DAL;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public interface ISupplierCloseShopApply
    {
        bool AddSupplierCloseShopApply(SupplierCloseShopApplyInfo entity);

        bool EditSupplierCloseShopApply(SupplierCloseShopApplyInfo entity, RBACUserInfo UserPrivilege);

        int DelSupplierCloseShopApply(int ID, RBACUserInfo UserPrivilege);

        SupplierCloseShopApplyInfo GetSupplierCloseShopApplyByID(int ID, RBACUserInfo UserPrivilege);

        IList<SupplierCloseShopApplyInfo> GetSupplierCloseShopApplys(QueryInfo Query, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);

    }

    


}

