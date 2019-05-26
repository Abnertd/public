﻿using System;
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
