using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;

namespace Glaer.Trade.B2C.DAL.Sys
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
