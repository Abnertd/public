using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.Util.Mail;
using Glaer.Trade.B2C.DAL;

namespace Glaer.Trade.B2C.BLL.Product
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
