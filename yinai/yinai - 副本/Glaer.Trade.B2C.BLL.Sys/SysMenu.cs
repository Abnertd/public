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
    public class SysMenu : ISysMenu
    {
        protected DAL.Sys.ISysMenu MyDAL;
        protected IRBAC RBAC;

        public SysMenu()
        {
            MyDAL = DAL.Sys.SysMenuFactory.CreateSysMenu();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddSysMenu(SysMenuInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "c9ce4dd0-6391-4fb9-aa99-f37c23c04a8a"))
            {
                return MyDAL.AddSysMenu(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：c9ce4dd0-6391-4fb9-aa99-f37c23c04a8a错误");
            }
        }

        public virtual bool EditSysMenu(SysMenuInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "7daf4ba3-15af-4c7f-a9f5-ab0f9413ff08"))
            {
                return MyDAL.EditSysMenu(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：7daf4ba3-15af-4c7f-a9f5-ab0f9413ff08错误");
            }
        }

        public virtual int DelSysMenu(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "e5e043cc-5085-41f9-b406-808c319b3a70"))
            {
                return MyDAL.DelSysMenu(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：e5e043cc-5085-41f9-b406-808c319b3a70错误");
            }
        }

        public virtual SysMenuInfo GetSysMenuByID(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "4d14d977-e839-4322-ae0d-fa257030dd2b"))
            {
                return MyDAL.GetSysMenuByID(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：4d14d977-e839-4322-ae0d-fa257030dd2b错误");
            }
        }

        public virtual IList<SysMenuInfo> GetSysMenus(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "4d14d977-e839-4322-ae0d-fa257030dd2b"))
            {
                return MyDAL.GetSysMenus(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：4d14d977-e839-4322-ae0d-fa257030dd2b错误");
            }
        }

        public virtual IList<SysMenuInfo> GetSysMenusSub(int Menu_ParentID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "4d14d977-e839-4322-ae0d-fa257030dd2b"))
            {
                return MyDAL.GetSysMenusSub(Menu_ParentID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：4d14d977-e839-4322-ae0d-fa257030dd2b错误");
            }
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "4d14d977-e839-4322-ae0d-fa257030dd2b"))
            {
                return MyDAL.GetPageInfo(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：4d14d977-e839-4322-ae0d-fa257030dd2b错误");
            }
        }

    }
}

