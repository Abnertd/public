using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.Util.Tools;


namespace Glaer.Trade.B2C.DAL.MEM
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
