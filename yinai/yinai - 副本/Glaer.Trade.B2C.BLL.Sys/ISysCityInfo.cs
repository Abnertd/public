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
