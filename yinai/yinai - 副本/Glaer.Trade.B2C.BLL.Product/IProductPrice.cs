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
    public interface IProductPrice
    {
        bool AddProductPrice(ProductPriceInfo entity);

        bool EditProductPrice(ProductPriceInfo entity);

        int DelProductPrice(int ID);

        ProductPriceInfo GetProductPriceByID(int ID);

        IList<ProductPriceInfo> GetProductPrices(int Product_ID);

        PageInfo GetPageInfo(QueryInfo Query);

    }
}
