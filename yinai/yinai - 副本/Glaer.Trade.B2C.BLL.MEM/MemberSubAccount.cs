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
    public class MemberSubAccount : IMemberSubAccount
    {
        protected DAL.MEM.IMemberSubAccount MyDAL;
        protected IRBAC RBAC;
         
        public MemberSubAccount()
        {
            MyDAL = DAL.MEM.MemberSubAccountFactory.CreateMemberSubAccount();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddMemberSubAccount(MemberSubAccountInfo entity)
        {
            return MyDAL.AddMemberSubAccount(entity);
        }

        public virtual bool EditMemberSubAccount(MemberSubAccountInfo entity)
        {
            return MyDAL.EditMemberSubAccount(entity);
        }

        public virtual int DelMemberSubAccount(int ID)
        {
            return MyDAL.DelMemberSubAccount(ID);
        }

        public virtual MemberSubAccountInfo GetMemberSubAccountByID(int ID)
        {
            return MyDAL.GetMemberSubAccountByID(ID);
        }

        public virtual IList<MemberSubAccountInfo> GetMemberSubAccounts(QueryInfo Query)
        {
            return MyDAL.GetMemberSubAccounts(Query);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }
    }

    public class MemberSubAccountLog : IMemberSubAccountLog
    {
        protected DAL.MEM.IMemberSubAccountLog MyDAL;
        protected IRBAC RBAC;

        public MemberSubAccountLog()
        {
            MyDAL = DAL.MEM.MemberSubAccountFactory.CreateMemberSubAccountLog();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddMemberSubAccountLog(MemberSubAccountLogInfo entity)
        {
            return MyDAL.AddMemberSubAccountLog(entity);
        }

        public virtual bool EditMemberSubAccountLog(MemberSubAccountLogInfo entity)
        {
            return MyDAL.EditMemberSubAccountLog(entity);
        }

        public virtual int DelMemberSubAccountLog(int ID)
        {
            return MyDAL.DelMemberSubAccountLog(ID);
        }

        public virtual MemberSubAccountLogInfo GetMemberSubAccountLogByID(int ID)
        {
            return MyDAL.GetMemberSubAccountLogByID(ID);
        }

        public virtual IList<MemberSubAccountLogInfo> GetMemberSubAccountLogs(QueryInfo Query)
        {
            return MyDAL.GetMemberSubAccountLogs(Query);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }
    }
}

