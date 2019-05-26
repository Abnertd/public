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
    public interface ISysCounty
    {
        bool AddSysCounty(SysCountyInfo entity);

        bool EditSysCounty(SysCountyInfo entity);

        int DelSysCounty(int ID);

        SysCountyInfo GetSysCountyByID(int ID);

        IList<SysCountyInfo> GetSysCountys(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);

    }

}
