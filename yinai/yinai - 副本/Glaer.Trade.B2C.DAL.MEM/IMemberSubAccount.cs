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
    public interface IMemberSubAccount
    { 
        bool AddMemberSubAccount(MemberSubAccountInfo entity);

        bool EditMemberSubAccount(MemberSubAccountInfo entity);

        int DelMemberSubAccount(int ID);

        MemberSubAccountInfo GetMemberSubAccountByID(int ID);

        IList<MemberSubAccountInfo> GetMemberSubAccounts(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);
    }

    public interface IMemberSubAccountLog
    {
        bool AddMemberSubAccountLog(MemberSubAccountLogInfo entity);

        bool EditMemberSubAccountLog(MemberSubAccountLogInfo entity);

        int DelMemberSubAccountLog(int ID);

        MemberSubAccountLogInfo GetMemberSubAccountLogByID(int ID);

        IList<MemberSubAccountLogInfo> GetMemberSubAccountLogs(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);
    }
}
