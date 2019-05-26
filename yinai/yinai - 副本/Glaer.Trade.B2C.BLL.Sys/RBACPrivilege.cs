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
    public class RBACPrivilege : IRBACPrivilege
    {
        protected DAL.Sys.IRBACPrivilege MyDAL;
        protected IRBAC RBAC;

        public RBACPrivilege()
        {
            MyDAL = DAL.Sys.RBACPrivilegeFactory.CreateRBACPrivilege();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddRBACPrivilege(RBACPrivilegeInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "df7e7e2e-bbe2-48b0-976c-17a74c4a45e6"))
            {
                return MyDAL.AddRBACPrivilege(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：df7e7e2e-bbe2-48b0-976c-17a74c4a45e6错误");
            }
        }

        public virtual bool EditRBACPrivilege(RBACPrivilegeInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "51be7b46-e0f7-46dd-b0b2-a462fcb907ae"))
            {
                return MyDAL.EditRBACPrivilege(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：51be7b46-e0f7-46dd-b0b2-a462fcb907ae错误");
            }
        }

        public virtual int DelRBACPrivilege(string ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "1030465e-7113-4db6-9b3c-da21aca07748"))
            {
                return MyDAL.DelRBACPrivilege(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：1030465e-7113-4db6-9b3c-da21aca07748错误");
            }
        }

        public virtual RBACPrivilegeInfo GetRBACPrivilegeByID(string ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "147d21e2-7989-44e7-8b08-0c64797c2513"))
            {
                return MyDAL.GetRBACPrivilegeByID(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：147d21e2-7989-44e7-8b08-0c64797c2513错误");
            }
        }

        public virtual IList<RBACPrivilegeInfo> GetRBACPrivileges(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "147d21e2-7989-44e7-8b08-0c64797c2513"))
            {
                return MyDAL.GetRBACPrivileges(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：147d21e2-7989-44e7-8b08-0c64797c2513错误");
            }
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "147d21e2-7989-44e7-8b08-0c64797c2513"))
            {
                return MyDAL.GetPageInfo(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：147d21e2-7989-44e7-8b08-0c64797c2513错误");
            }
        }

    }

}
