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
    public interface IProductNotify
    {
        bool AddProductNotify(ProductNotifyInfo entity);

        bool EditProductNotify(ProductNotifyInfo entity);

        int DelProductNotify(int ID);

        ProductNotifyInfo GetProductNotifyByID(int ID);

        IList<ProductNotifyInfo> GetProductNotifys(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);
    }
}
