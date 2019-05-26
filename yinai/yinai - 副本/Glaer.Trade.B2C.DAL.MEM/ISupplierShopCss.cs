using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.Util.Tools;


namespace Glaer.Trade.B2C.DAL.MEM
{
    public interface ISupplierShopCss
    {
        bool AddSupplierShopCss(SupplierShopCssInfo entity);

        bool EditSupplierShopCss(SupplierShopCssInfo entity);

        int DelSupplierShopCss(int ID);

        SupplierShopCssInfo GetSupplierShopCssByID(int ID);

        IList<SupplierShopCssInfo> GetSupplierShopCsss(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);

        bool AddSupplierShopCssRelateSupplier(SupplierShopCssRelateSupplierInfo entity);

        int DelSupplierShopCssRelateSupplierBySupplierID(int ID);

        int DelSupplierShopCssRelateSupplierByCssID(int Css_ID);

        IList<SupplierShopCssRelateSupplierInfo> GetSupplierShopCssRelateSuppliers(int Relate_SupplierID);

        IList<SupplierShopCssRelateSupplierInfo> GetSupplierShopCssRelateSuppliersByCss(int Css_ID);
    }


}
