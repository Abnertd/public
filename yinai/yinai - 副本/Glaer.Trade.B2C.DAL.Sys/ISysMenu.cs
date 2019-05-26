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
    public interface ISysMenu
    {
        bool AddSysMenu(SysMenuInfo entity);

        bool EditSysMenu(SysMenuInfo entity);

        int DelSysMenu(int ID);

        SysMenuInfo GetSysMenuByID(int ID);

        IList<SysMenuInfo> GetSysMenus(QueryInfo Query);

        IList<SysMenuInfo> GetSysMenusSub(int Menu_ParentID);

        PageInfo GetPageInfo(QueryInfo Query);
    }
}
