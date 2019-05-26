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
    public interface ISysState
    {
        bool AddSysState(SysStateInfo entity);

        bool EditSysState(SysStateInfo entity);

        int DelSysState(int ID);

        SysStateInfo GetSysStateByID(int ID);

        IList<SysStateInfo> GetSysStates(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);
    }

}
