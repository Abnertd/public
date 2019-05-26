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
    public class LogisticsLine : ILogisticsLine
    {
        protected DAL.MEM.ILogisticsLine MyDAL;
        protected IRBAC RBAC;

        public LogisticsLine()
        {
            MyDAL = DAL.MEM.LogisticsLineFactory.CreateLogisticsLine();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddLogisticsLine(LogisticsLineInfo entity)
        {
            return MyDAL.AddLogisticsLine(entity);
        }

        public virtual bool EditLogisticsLine(LogisticsLineInfo entity)
        {
            return MyDAL.EditLogisticsLine(entity);
        }

        public virtual int DelLogisticsLine(int ID)
        {
            return MyDAL.DelLogisticsLine(ID);
        }

        public virtual LogisticsLineInfo GetLogisticsLineByID(int ID)
        {
            return MyDAL.GetLogisticsLineByID(ID);
        }

        public virtual IList<LogisticsLineInfo> GetLogisticsLines(QueryInfo Query)
        {
            return MyDAL.GetLogisticsLines(Query);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }

    }
}

