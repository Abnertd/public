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
    public interface ISupplierMessage
    {
        bool AddSupplierMessage(SupplierMessageInfo entity, RBACUserInfo UserPrivilege);

        bool EditSupplierMessage(SupplierMessageInfo entity, RBACUserInfo UserPrivilege);

        int DelSupplierMessage(int ID, RBACUserInfo UserPrivilege);

        SupplierMessageInfo GetSupplierMessageByID(int ID, RBACUserInfo UserPrivilege);

        IList<SupplierMessageInfo> GetSupplierMessages(QueryInfo Query, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);

    }
}
