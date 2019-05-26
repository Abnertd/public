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
    public interface IProductAuditReason
    {
        bool AddProductAuditReason(ProductAuditReasonInfo entity);

        bool EditProductAuditReason(ProductAuditReasonInfo entity);

        int DelProductAuditReason(int ID);

        ProductAuditReasonInfo GetProductAuditReasonByID(int ID);

        IList<ProductAuditReasonInfo> GetProductAuditReasons(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);

        bool AddProductDenyReason(ProductDenyReasonInfo entity);

        int DelProductDenyReason(int ID);

        IList<ProductDenyReasonInfo> GetProductDenyReasons(int Product_ID);
    }
}
