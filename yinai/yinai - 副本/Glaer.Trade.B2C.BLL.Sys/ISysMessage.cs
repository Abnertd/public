using System;
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
