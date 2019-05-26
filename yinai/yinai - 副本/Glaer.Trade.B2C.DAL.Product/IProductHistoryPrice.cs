using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.Util.Tools;

namespace Glaer.Trade.B2C.DAL.Product
{
    public interface IProductHistoryPrice
    {
        bool AddProductHistoryPrice(ProductHistoryPriceInfo entity);

        bool EditProductHistoryPrice(ProductHistoryPriceInfo entity);

        int DelProductHistoryPrice(int ID);

        ProductHistoryPriceInfo GetProductHistoryPriceByID(int ID);

        IList<ProductHistoryPriceInfo> GetProductHistoryPrices(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);
    }
}
