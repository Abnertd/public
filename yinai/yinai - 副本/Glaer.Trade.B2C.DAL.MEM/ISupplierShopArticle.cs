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
