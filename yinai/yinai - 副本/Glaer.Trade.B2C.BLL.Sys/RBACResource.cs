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
    public class RBACResourceGroup : IRBACResourceGroup
    {
        protected DAL.Sys.IRBACResourceGroup MyDAL;
        protected IRBAC RBAC;

        public RBACResourceGroup()
        {
            MyDAL = DAL.Sys.RBACResourceFactory.CreateRBACResourceGroup();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddRBACResourceGroup(RBACResourceGroupInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "a63248fd-532f-40a8-850d-d217c5ddd38a"))
            {
                return MyDAL.AddRBACResourceGroup(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：a63248fd-532f-40a8-850d-d217c5ddd38a错误");
            }
        }

        public virtual bool EditRBACResourceGroup(RBACResourceGroupInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "a63248fd-532f-40a8-850d-d217c5ddd38a"))
            {
                return MyDAL.EditRBACResourceGroup(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：a63248fd-532f-40a8-850d-d217c5ddd38a错误");
            }
        }

        public virtual int DelRBACResourceGroup(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "a63248fd-532f-40a8-850d-d217c5ddd38a"))
            {
                return MyDAL.DelRBACResourceGroup(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：a63248fd-532f-40a8-850d-d217c5ddd38a错误");
            }
        }

        public virtual RBACResourceGroupInfo GetRBACResourceGroupByID(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "a63248fd-532f-40a8-850d-d217c5ddd38a"))
            {
                return MyDAL.GetRBACResourceGroupByID(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：a63248fd-532f-40a8-850d-d217c5ddd38a错误");
            }
        }

        public virtual IList<RBACResourceGroupInfo> GetRBACResourceGroups(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "a63248fd-532f-40a8-850d-d217c5ddd38a"))
            {
                return MyDAL.GetRBACResourceGroups(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：a63248fd-532f-40a8-850d-d217c5ddd38a错误");
            }
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "a63248fd-532f-40a8-850d-d217c5ddd38a"))
            {
                return MyDAL.GetPageInfo(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：a63248fd-532f-40a8-850d-d217c5ddd38a错误");
            }
        }

    }

    public class RBACResource : IRBACResource
    {
        protected DAL.Sys.IRBACResource MyDAL;
        protected IRBAC RBAC;

        public RBACResource()
        {
            MyDAL = DAL.Sys.RBACResourceFactory.CreateRBACResource();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddRBACResource(RBACResourceInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "7e0cb63c-935a-4414-a0bb-5c1b7e259d92"))
            {
                return MyDAL.AddRBACResource(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：7e0cb63c-935a-4414-a0bb-5c1b7e259d92错误");
            }
        }

        public virtual bool EditRBACResource(RBACResourceInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "7e0cb63c-935a-4414-a0bb-5c1b7e259d92"))
            {
                return MyDAL.EditRBACResource(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：7e0cb63c-935a-4414-a0bb-5c1b7e259d92错误");
            }
        }

        public virtual int DelRBACResource(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "7e0cb63c-935a-4414-a0bb-5c1b7e259d92"))
            {
                return MyDAL.DelRBACResource(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：7e0cb63c-935a-4414-a0bb-5c1b7e259d92错误");
            }
        }

        public virtual RBACResourceInfo GetRBACResourceByID(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "7e0cb63c-935a-4414-a0bb-5c1b7e259d92"))
            {
                return MyDAL.GetRBACResourceByID(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：7e0cb63c-935a-4414-a0bb-5c1b7e259d92错误");
            }
        }

        public virtual IList<RBACResourceInfo> GetRBACResources(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "7e0cb63c-935a-4414-a0bb-5c1b7e259d92"))
            {
                return MyDAL.GetRBACResources(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：7e0cb63c-935a-4414-a0bb-5c1b7e259d92错误");
            }
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "7e0cb63c-935a-4414-a0bb-5c1b7e259d92"))
            {
                return MyDAL.GetPageInfo(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：7e0cb63c-935a-4414-a0bb-5c1b7e259d92错误");
            }
        }

    }

}
