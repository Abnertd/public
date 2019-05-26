using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;

namespace Glaer.Trade.B2C.DAL.Sys
{
    public interface IRBACRole
    {
        bool AddRBACRole(RBACRoleInfo entity);

        bool EditRBACRole(RBACRoleInfo entity);

        int DelRBACRole(int ID);

        RBACRoleInfo GetRBACRoleByID(int ID);

        IList<RBACRoleInfo> GetRBACRoles(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);

        IList<RBACPrivilegeInfo> GetPrivilegeListByRole(int Role_ID);
    }

}
