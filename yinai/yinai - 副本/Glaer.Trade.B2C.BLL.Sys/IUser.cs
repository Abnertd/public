using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.RBAC;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.B2C.DAL;

namespace Glaer.Trade.B2C.BLL.Sys
{
    public interface IRBACUser
    {
        bool AddRBACUser(RBACUserInfo entity, RBACUserInfo UserPrivilege);

        bool EditRBACUser(RBACUserInfo entity, RBACUserInfo UserPrivilege);

        int DelRBACUser(int ID, RBACUserInfo UserPrivilege);

        RBACUserInfo GetRBACUserByID(int ID, RBACUserInfo UserPrivilege);

        RBACUserInfo GetRBACUserByName(string UserName, RBACUserInfo UserPrivilege);

        IList<RBACUserInfo> GetRBACUsers(QueryInfo Query, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);

        bool EditUserPassword(string UserPassword, int UserID);
    }

    public interface IRBACUserGroup
    {
        bool AddRBACUserGroup(RBACUserGroupInfo entity, RBACUserInfo UserPrivilege);

        bool EditRBACUserGroup(RBACUserGroupInfo entity, RBACUserInfo UserPrivilege);

        int DelRBACUserGroup(int ID, RBACUserInfo UserPrivilege);

        RBACUserGroupInfo GetRBACUserGroupByID(int ID, RBACUserInfo UserPrivilege);

        IList<RBACUserGroupInfo> GetRBACUserGroups(QueryInfo Query, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);
    }
}
