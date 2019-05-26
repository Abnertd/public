using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;

namespace Glaer.Trade.B2C.BLL.Sys
{
    public interface IRBACResourceGroup
    {
        bool AddRBACResourceGroup(RBACResourceGroupInfo entity, RBACUserInfo UserPrivilege);

        bool EditRBACResourceGroup(RBACResourceGroupInfo entity, RBACUserInfo UserPrivilege);

        int DelRBACResourceGroup(int ID, RBACUserInfo UserPrivilege);

        RBACResourceGroupInfo GetRBACResourceGroupByID(int ID, RBACUserInfo UserPrivilege);

        IList<RBACResourceGroupInfo> GetRBACResourceGroups(QueryInfo Query, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);

    }

    public interface IRBACResource
    {
        bool AddRBACResource(RBACResourceInfo entity, RBACUserInfo UserPrivilege);

        bool EditRBACResource(RBACResourceInfo entity, RBACUserInfo UserPrivilege);

        int DelRBACResource(int ID, RBACUserInfo UserPrivilege);

        RBACResourceInfo GetRBACResourceByID(int ID, RBACUserInfo UserPrivilege);

        IList<RBACResourceInfo> GetRBACResources(QueryInfo Query, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);
    }

}
