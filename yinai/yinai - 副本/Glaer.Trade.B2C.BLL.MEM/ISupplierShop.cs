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
    public interface ISupplierShop
    {
        bool AddSupplierShop(SupplierShopInfo entity);

        bool EditSupplierShop(SupplierShopInfo entity);

        int DelSupplierShop(int ID);

        SupplierShopInfo GetSupplierShopByID(int ID);

        SupplierShopInfo GetSupplierShopBySupplierID(int ID);

        SupplierShopInfo GetSupplierShopByDomain(string Domain);

        IList<SupplierShopInfo> GetSupplierShops(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);

        void SaveShopCategory(int Shop_ID, string[] extends);

        string GetShopCategory(int Shop_ID);

    }

    public interface ISupplierShopDomain
    {
        bool AddSupplierShopDomain(SupplierShopDomainInfo entity);

        bool EditSupplierShopDomain(SupplierShopDomainInfo entity);

        int DelSupplierShopDomain(int ID);

        SupplierShopDomainInfo GetSupplierShopDomainByID(int ID);

        IList<SupplierShopDomainInfo> GetSupplierShopDomains(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);

    }

}
