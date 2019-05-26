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
    public interface IProductTypeExtend
    {
        bool AddProductTypeExtend(ProductTypeExtendInfo entity);

        bool EditProductTypeExtend(ProductTypeExtendInfo entity);

        int DelProductTypeExtend(int ID);

        ProductTypeExtendInfo GetProductTypeExtendByID(int ID);

        IList<ProductTypeExtendInfo> GetProductTypeExtends(int ID);

        PageInfo GetPageInfo(QueryInfo Query);
    }
}
