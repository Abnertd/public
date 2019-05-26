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
    public interface ISupplierPayBackApply
    {
        bool AddSupplierPayBackApply(SupplierPayBackApplyInfo entity);

        bool EditSupplierPayBackApply(SupplierPayBackApplyInfo entity, RBACUserInfo UserPrivilege);

        int DelSupplierPayBackApply(int ID, RBACUserInfo UserPrivilege);

        SupplierPayBackApplyInfo GetSupplierPayBackApplyByID(int ID, RBACUserInfo UserPrivilege);

        IList<SupplierPayBackApplyInfo> GetSupplierPayBackApplys(QueryInfo Query, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);

    }


}

