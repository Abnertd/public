using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;

namespace Glaer.Trade.B2C.DAL.Sys
{
    public interface ISysCity
    {
        bool AddSysCity(SysCityInfo entity);

        bool EditSysCity(SysCityInfo entity);

        int DelSysCity(int ID);

        SysCityInfo GetSysCityByID(int ID);

        IList<SysCityInfo> GetSysCitys(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);
    }

}
