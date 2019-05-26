using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;

namespace Glaer.Trade.B2C.DAL.Sys
{
    public interface IRBACPrivilege
    {
        bool AddRBACPrivilege(RBACPrivilegeInfo entity);

        bool EditRBACPrivilege(RBACPrivilegeInfo entity);

        int DelRBACPrivilege(string ID);

        RBACPrivilegeInfo GetRBACPrivilegeByID(string ID);

        IList<RBACPrivilegeInfo> GetRBACPrivileges(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);
    }

}
