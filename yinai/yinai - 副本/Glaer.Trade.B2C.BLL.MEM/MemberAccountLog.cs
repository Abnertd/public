using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.Util.Mail;
using Glaer.Trade.B2C.DAL;
using Glaer.Trade.B2C.RBAC;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public class MemberAccountLog : IMemberAccountLog
    {
        protected DAL.MEM.IMemberAccountLog MyDAL;
        protected IRBAC RBAC;

        public MemberAccountLog()
        {
            MyDAL = DAL.MEM.MemberAccountLogFactory.CreateMemberAccountLog();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddMemberAccountOrders(MemberAccountOrdersInfo entity)
        {
            return MyDAL.AddMemberAccountOrders(entity);
        }

        public virtual bool EditMemberAccountOrders(MemberAccountOrdersInfo entity)
        {
            return MyDAL.EditMemberAccountOrders(entity);
        }

        public virtual int DelMemberAccountOrders(int ID)
        {
            return MyDAL.DelMemberAccountOrders(ID);
        }

        public virtual MemberAccountOrdersInfo GetMemberAccountOrdersByOrdersSN(string OrdersSN)
        {
            return MyDAL.GetMemberAccountOrdersByOrdersSN(OrdersSN);
        }

        public virtual bool AddMemberAccountLog(MemberAccountLogInfo entity)
        {
            return MyDAL.AddMemberAccountLog(entity);
        }

        public virtual bool EditMemberAccountLog(MemberAccountLogInfo entity)
        {
            return MyDAL.EditMemberAccountLog(entity);
        }

        public virtual int DelMemberAccountLog(int ID)
        {
            return MyDAL.DelMemberAccountLog(ID);
        }

        public virtual MemberAccountLogInfo GetMemberAccountLogByID(int ID)
        {
            return MyDAL.GetMemberAccountLogByID(ID);
        }

        public virtual IList<MemberAccountLogInfo> GetMemberAccountLogs(QueryInfo Query)
        {
            return MyDAL.GetMemberAccountLogs(Query);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }

    }

}

