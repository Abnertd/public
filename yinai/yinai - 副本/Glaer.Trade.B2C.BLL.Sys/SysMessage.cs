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
    public class SysMessage : ISysMessage
    {
        protected DAL.Sys.ISysMessage MyDAL;
        protected IRBAC RBAC;

        public SysMessage()
        {
            MyDAL = DAL.Sys.SysMessageFactory.CreateSysMessage();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddSysMessage(SysMessageInfo entity)
        {
            return MyDAL.AddSysMessage(entity);
        }

        public virtual bool EditSysMessage(SysMessageInfo entity)
        {
            return MyDAL.EditSysMessage(entity);
        }

        public virtual int DelSysMessage(int ID)
        {
            return MyDAL.DelSysMessage(ID);
        }

        public virtual SysMessageInfo GetSysMessageByID(int ID)
        {
            return MyDAL.GetSysMessageByID(ID);
        }

        public virtual IList<SysMessageInfo> GetSysMessages(QueryInfo Query)
        {
            return MyDAL.GetSysMessages(Query);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }

        public IList<SysMessageInfo> GetSysMessages(int Message_Type, int Message_UserType, int Message_ReceiveID)
        {
            return MyDAL.GetSysMessages(Message_Type,Message_UserType,Message_ReceiveID);
        }
    }
}

