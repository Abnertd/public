using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public interface ILogistics
    {
        bool AddLogistics(LogisticsInfo entity, RBACUserInfo UserPrivilege);

        bool EditLogistics(LogisticsInfo entity, RBACUserInfo UserPrivilege);

        int DelLogistics(int ID, RBACUserInfo UserPrivilege);

        LogisticsInfo GetLogisticsByID(int ID, RBACUserInfo UserPrivilege);

        LogisticsInfo GetLogisticsByNickName(string NickName, RBACUserInfo UserPrivilege);

        IList<LogisticsInfo> GetLogisticss(QueryInfo Query, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);

    }
}
