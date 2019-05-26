using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;

namespace Glaer.Trade.B2C.BLL.Sys
{
    public interface IRBACRole
    {
        bool AddRBACRole(RBACRoleInfo entity, RBACUserInfo UserPrivilege);

        bool EditRBACRole(RBACRoleInfo entity, RBACUserInfo UserPrivilege);

        int DelRBACRole(int ID, RBACUserInfo UserPrivilege);

        RBACRoleInfo GetRBACRoleByID(int ID, RBACUserInfo UserPrivilege);

        IList<RBACRoleInfo> GetRBACRoles(QueryInfo Query, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);
    }
}
