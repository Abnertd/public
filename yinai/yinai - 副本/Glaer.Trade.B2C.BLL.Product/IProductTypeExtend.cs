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
