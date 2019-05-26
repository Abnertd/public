using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public interface IMemberAccountLog
    {
        bool AddMemberAccountOrders(MemberAccountOrdersInfo entity);

        bool EditMemberAccountOrders(MemberAccountOrdersInfo entity);

        int DelMemberAccountOrders(int ID);

        MemberAccountOrdersInfo GetMemberAccountOrdersByOrdersSN(string OrdersSN);

        bool AddMemberAccountLog(MemberAccountLogInfo entity);

        bool EditMemberAccountLog(MemberAccountLogInfo entity);

        int DelMemberAccountLog(int ID);

        MemberAccountLogInfo GetMemberAccountLogByID(int ID);

        IList<MemberAccountLogInfo> GetMemberAccountLogs(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);

    }

}
