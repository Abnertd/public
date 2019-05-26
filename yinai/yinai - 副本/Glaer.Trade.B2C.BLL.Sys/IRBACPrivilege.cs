using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;

namespace Glaer.Trade.B2C.BLL.Sys
{
    public interface IRBACPrivilege
    {
        bool AddRBACPrivilege(RBACPrivilegeInfo entity, RBACUserInfo UserPrivilege);

        bool EditRBACPrivilege(RBACPrivilegeInfo entity, RBACUserInfo UserPrivilege);

        int DelRBACPrivilege(string ID, RBACUserInfo UserPrivilege);

        RBACPrivilegeInfo GetRBACPrivilegeByID(string ID, RBACUserInfo UserPrivilege);

        IList<RBACPrivilegeInfo> GetRBACPrivileges(QueryInfo Query, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);
    }

}
