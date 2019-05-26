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
    public interface ISupplierShopPages
    {
        bool AddSupplierShopPages(SupplierShopPagesInfo entity);

        bool EditSupplierShopPages(SupplierShopPagesInfo entity);

        int DelSupplierShopPages(int ID);

        SupplierShopPagesInfo GetSupplierShopPagesByID(int ID);

        SupplierShopPagesInfo GetSupplierShopPagesByIDSign(string Sign, int Supplier_ID);

        IList<SupplierShopPagesInfo> GetSupplierShopPagess(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);

    }
}
