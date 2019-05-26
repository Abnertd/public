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
    public interface ISupplierShopCss
    {
        bool AddSupplierShopCss(SupplierShopCssInfo entity, RBACUserInfo UserPrivilege);

        bool EditSupplierShopCss(SupplierShopCssInfo entity, RBACUserInfo UserPrivilege);

        int DelSupplierShopCss(int ID, RBACUserInfo UserPrivilege);

        SupplierShopCssInfo GetSupplierShopCssByID(int ID, RBACUserInfo UserPrivilege);

        IList<SupplierShopCssInfo> GetSupplierShopCsss(QueryInfo Query, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);

        bool AddSupplierShopCssRelateSupplier(SupplierShopCssRelateSupplierInfo entity);

        int DelSupplierShopCssRelateSupplierBySupplierID(int ID);

        int DelSupplierShopCssRelateSupplierByCssID(int Css_ID);

        IList<SupplierShopCssRelateSupplierInfo> GetSupplierShopCssRelateSuppliers(int Relate_SupplierID);

        IList<SupplierShopCssRelateSupplierInfo> GetSupplierShopCssRelateSuppliersByCss(int Css_ID);

    }
}
