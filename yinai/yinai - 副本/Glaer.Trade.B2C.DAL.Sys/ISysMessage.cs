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
    public interface ISysMessage
    {
        bool AddSysMessage(SysMessageInfo entity);

        bool EditSysMessage(SysMessageInfo entity);

        int DelSysMessage(int ID);

        SysMessageInfo GetSysMessageByID(int ID);

        IList<SysMessageInfo> GetSysMessages(int Message_Type, int Message_UserType, int Message_ReceiveID);

        IList<SysMessageInfo> GetSysMessages(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);
    }
}
