using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.RBAC;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.B2C.DAL;

namespace Glaer.Trade.B2C.BLL.Product
{
    public interface IProductAuditReason
    {
        bool AddProductAuditReason(ProductAuditReasonInfo entity, RBACUserInfo UserPrivilege);

        bool EditProductAuditReason(ProductAuditReasonInfo entity, RBACUserInfo UserPrivilege);

        int DelProductAuditReason(int ID, RBACUserInfo UserPrivilege);

        ProductAuditReasonInfo GetProductAuditReasonByID(int ID, RBACUserInfo UserPrivilege);

        IList<ProductAuditReasonInfo> GetProductAuditReasons(QueryInfo Query, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);

        bool AddProductDenyReason(ProductDenyReasonInfo entity);

        int DelProductDenyReason(int ID);

        IList<ProductDenyReasonInfo> GetProductDenyReasons(int Product_ID);

    }
}
