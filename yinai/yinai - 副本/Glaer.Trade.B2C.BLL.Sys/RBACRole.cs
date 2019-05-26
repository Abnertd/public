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
    public class RBACRole : IRBACRole
    {
        protected DAL.Sys.IRBACRole MyDAL;
        protected IRBAC RBAC;

        public RBACRole()
        {
            MyDAL = DAL.Sys.RBACRoleFactory.CreateRBACRole();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddRBACRole(RBACRoleInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "71268a79-72ee-4892-a8f8-cf02b13c312a"))
            {
                return MyDAL.AddRBACRole(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：71268a79-72ee-4892-a8f8-cf02b13c312a错误");
            }
        }

        public virtual bool EditRBACRole(RBACRoleInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "4df5eb30-ee06-49a4-b119-4c72e5dfaebc"))
            {
                return MyDAL.EditRBACRole(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：4df5eb30-ee06-49a4-b119-4c72e5dfaebc错误");
            }
        }

        public virtual int DelRBACRole(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "3cfb485b-375e-4b4a-af15-1cf74946e333"))
            {
                return MyDAL.DelRBACRole(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：3cfb485b-375e-4b4a-af15-1cf74946e333错误");
            }
        }

        public virtual RBACRoleInfo GetRBACRoleByID(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "8b470aa6-c158-4b70-8e7f-c640af462cf1"))
            {
                return MyDAL.GetRBACRoleByID(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：8b470aa6-c158-4b70-8e7f-c640af462cf1错误");
            }
        }

        public virtual IList<RBACRoleInfo> GetRBACRoles(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "8b470aa6-c158-4b70-8e7f-c640af462cf1"))
            {
                return MyDAL.GetRBACRoles(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：8b470aa6-c158-4b70-8e7f-c640af462cf1错误");
            }
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "8b470aa6-c158-4b70-8e7f-c640af462cf1"))
            {
                return MyDAL.GetPageInfo(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：8b470aa6-c158-4b70-8e7f-c640af462cf1错误");
            }
        }

    }

}
