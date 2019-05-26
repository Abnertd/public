using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.Util.Tools;

namespace Glaer.Trade.B2C.DAL.Sys
{
    public interface IRBACUser
    {
        bool AddRBACUser(RBACUserInfo entity);

        bool EditRBACUser(RBACUserInfo entity);

        int DelRBACUser(int ID);

        RBACUserInfo GetRBACUserByID(int ID);

        RBACUserInfo GetRBACUserByName(string UserName);

        IList<RBACUserInfo> GetRBACUsers(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);

        IList<RBACRoleInfo> GetRoleListByUser(int User_ID);

        bool EditUserPassword(string UserPassword, int UserID);
    }

    public interface IRBACUserGroup
    {
        bool AddRBACUserGroup(RBACUserGroupInfo entity);

        bool EditRBACUserGroup(RBACUserGroupInfo entity);

        int DelRBACUserGroup(int ID);

        RBACUserGroupInfo GetRBACUserGroupByID(int ID);

        IList<RBACUserGroupInfo> GetRBACUserGroups(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);
    }

}