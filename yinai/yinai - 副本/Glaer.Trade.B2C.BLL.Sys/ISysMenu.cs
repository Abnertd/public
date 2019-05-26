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
    public interface ISysMenu
    {
        bool AddSysMenu(SysMenuInfo entity, RBACUserInfo UserPrivilege);

        bool EditSysMenu(SysMenuInfo entity, RBACUserInfo UserPrivilege);

        int DelSysMenu(int ID, RBACUserInfo UserPrivilege);

        SysMenuInfo GetSysMenuByID(int ID, RBACUserInfo UserPrivilege);

        IList<SysMenuInfo> GetSysMenus(QueryInfo Query, RBACUserInfo UserPrivilege);

        IList<SysMenuInfo> GetSysMenusSub(int Menu_ParentID, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);

    }
}
