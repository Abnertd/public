using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;

namespace Glaer.Trade.B2C.DAL.Sys
{
    public interface IRBACResourceGroup
    {
        bool AddRBACResourceGroup(RBACResourceGroupInfo entity);

        bool EditRBACResourceGroup(RBACResourceGroupInfo entity);

        int DelRBACResourceGroup(int ID);

        RBACResourceGroupInfo GetRBACResourceGroupByID(int ID);

        IList<RBACResourceGroupInfo> GetRBACResourceGroups(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);
    }

    public interface IRBACResource
    {
        bool AddRBACResource(RBACResourceInfo entity);

        bool EditRBACResource(RBACResourceInfo entity);

        int DelRBACResource(int ID);

        RBACResourceInfo GetRBACResourceByID(int ID);

        IList<RBACResourceInfo> GetRBACResources(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);
    }

}
