using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.RBAC;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.B2C.DAL;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public interface ILogisticsLine
    {
        bool AddLogisticsLine(LogisticsLineInfo entity);

        bool EditLogisticsLine(LogisticsLineInfo entity);

        int DelLogisticsLine(int ID);

        LogisticsLineInfo GetLogisticsLineByID(int ID);

        IList<LogisticsLineInfo> GetLogisticsLines(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);

    }
}
