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
    public class RBACUser : IRBACUser
    {
        protected DAL.Sys.IRBACUser MyDAL;
        protected IRBAC RBAC;

        public RBACUser()
        {
            MyDAL = DAL.Sys.RBACUserFactory.CreateRBACUser();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddRBACUser(RBACUserInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "7d494fee-ce23-4c47-9579-7191665865f4"))
            {
                return MyDAL.AddRBACUser(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：7d494fee-ce23-4c47-9579-7191665865f4错误");
            }
        }

        public virtual bool EditRBACUser(RBACUserInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "b47f8b43-cd62-4afc-8538-9acc6ba2a762"))
            {
                return MyDAL.EditRBACUser(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：b47f8b43-cd62-4afc-8538-9acc6ba2a762错误");
            }
        }

        public virtual int DelRBACUser(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "3498a173-9641-4bf1-996b-624e944ad209"))
            {
                return MyDAL.DelRBACUser(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：3498a173-9641-4bf1-996b-624e944ad209错误");
            }
        }

        public virtual RBACUserInfo GetRBACUserByID(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "f7fb595e-75cf-4dd2-8557-fadfa5756058"))
            {
                return MyDAL.GetRBACUserByID(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：f7fb595e-75cf-4dd2-8557-fadfa5756058错误");
            }
        }

        public virtual RBACUserInfo GetRBACUserByName(string UserName, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "f7fb595e-75cf-4dd2-8557-fadfa5756058"))
            {
                return MyDAL.GetRBACUserByName(UserName);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：f7fb595e-75cf-4dd2-8557-fadfa5756058错误");
            }
        }

        public virtual IList<RBACUserInfo> GetRBACUsers(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "f7fb595e-75cf-4dd2-8557-fadfa5756058"))
            {
                return MyDAL.GetRBACUsers(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：f7fb595e-75cf-4dd2-8557-fadfa5756058错误");
            }
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "f7fb595e-75cf-4dd2-8557-fadfa5756058"))
            {
                return MyDAL.GetPageInfo(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：f7fb595e-75cf-4dd2-8557-fadfa5756058错误");
            }
        }

        public virtual bool EditUserPassword(string UserPassword, int UserID)
        {
            return MyDAL.EditUserPassword(UserPassword, UserID);
        }

    }

    public class RBACUserGroup : IRBACUserGroup
    {
        protected DAL.Sys.IRBACUserGroup MyDAL;
        protected IRBAC RBAC;

        public RBACUserGroup()
        {
            MyDAL = DAL.Sys.RBACUserFactory.CreateRBACUserGroup();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddRBACUserGroup(RBACUserGroupInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "a2f95df4-346a-47b2-a112-1f8e3f062298"))
            {
                return MyDAL.AddRBACUserGroup(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：a2f95df4-346a-47b2-a112-1f8e3f062298错误");
            }
        }

        public virtual bool EditRBACUserGroup(RBACUserGroupInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "a2f95df4-346a-47b2-a112-1f8e3f062298"))
            {
                return MyDAL.EditRBACUserGroup(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：a2f95df4-346a-47b2-a112-1f8e3f062298错误");
            }
        }

        public virtual int DelRBACUserGroup(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "a2f95df4-346a-47b2-a112-1f8e3f062298"))
            {
                return MyDAL.DelRBACUserGroup(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：a2f95df4-346a-47b2-a112-1f8e3f062298错误");
            }
        }

        public virtual RBACUserGroupInfo GetRBACUserGroupByID(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "a2f95df4-346a-47b2-a112-1f8e3f062298"))
            {
                return MyDAL.GetRBACUserGroupByID(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：a2f95df4-346a-47b2-a112-1f8e3f062298错误");
            }
        }

        public virtual IList<RBACUserGroupInfo> GetRBACUserGroups(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "a2f95df4-346a-47b2-a112-1f8e3f062298"))
            {
                return MyDAL.GetRBACUserGroups(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：a2f95df4-346a-47b2-a112-1f8e3f062298错误");
            }
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "a2f95df4-346a-47b2-a112-1f8e3f062298"))
            {
                return MyDAL.GetPageInfo(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：a2f95df4-346a-47b2-a112-1f8e3f062298错误");
            }
        }

    }


}

