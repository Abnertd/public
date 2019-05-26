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
    public interface ISupplierShopArticle
    {
        bool AddSupplierShopArticle(SupplierShopArticleInfo entity);

        bool EditSupplierShopArticle(SupplierShopArticleInfo entity);

        int DelSupplierShopArticle(int ID);

        SupplierShopArticleInfo GetSupplierShopArticleByID(int ID);

        SupplierShopArticleInfo GetSupplierShopArticleByIDSupplier(int ID, int Supplier_ID);

        IList<SupplierShopArticleInfo> GetSupplierShopArticles(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);

    }
}
