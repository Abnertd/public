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
    public interface ISupplierTag
    {
        bool AddSupplierTag(SupplierTagInfo entity, RBACUserInfo UserPrivilege);

        bool EditSupplierTag(SupplierTagInfo entity, RBACUserInfo UserPrivilege);

        int DelSupplierTag(int ID, RBACUserInfo UserPrivilege);

        SupplierTagInfo GetSupplierTagByID(int ID, RBACUserInfo UserPrivilege);

        IList<SupplierTagInfo> GetSupplierTags(QueryInfo Query, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);

        bool AddSupplierRelateTag(SupplierRelateTagInfo entity);

        int DelSupplierRelateTagBySupplierID(int Supplier_ID);

        IList<SupplierRelateTagInfo> GetSupplierRelateTagsBySupplierID(int Supplier_ID);
    }
}
