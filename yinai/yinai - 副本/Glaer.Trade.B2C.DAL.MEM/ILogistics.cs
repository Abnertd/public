using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public interface ILogistics
    {
        bool AddLogistics(LogisticsInfo entity);

        bool EditLogistics(LogisticsInfo entity);

        int DelLogistics(int ID);

        LogisticsInfo GetLogisticsByID(int ID);

        LogisticsInfo GetLogisticsByNickName(string NickName);

        IList<LogisticsInfo> GetLogisticss(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);
    }
}
