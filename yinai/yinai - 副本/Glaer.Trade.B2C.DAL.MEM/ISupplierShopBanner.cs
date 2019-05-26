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
    public interface ISupplierShopBanner
    {
        bool AddSupplierShopBanner(SupplierShopBannerInfo entity);

        bool EditSupplierShopBanner(SupplierShopBannerInfo entity);

        int DelSupplierShopBanner(int ID);

        SupplierShopBannerInfo GetSupplierShopBannerByID(int ID);

        IList<SupplierShopBannerInfo> GetSupplierShopBanners(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);
    }

}
