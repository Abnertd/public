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
    public class MemberInvoice : IMemberInvoice
    {
        protected DAL.MEM.IMemberInvoice MyDAL;
        protected IRBAC RBAC;

        public MemberInvoice()
        {
            MyDAL = DAL.MEM.MemberInvoiceFactory.CreateMemberInvoice();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddMemberInvoice(MemberInvoiceInfo entity)
        {
            return MyDAL.AddMemberInvoice(entity);
        }

        public virtual bool EditMemberInvoice(MemberInvoiceInfo entity)
        {
            return MyDAL.EditMemberInvoice(entity);
        }

        public virtual int DelMemberInvoice(int ID)
        {
            return MyDAL.DelMemberInvoice(ID);
        }

        public virtual MemberInvoiceInfo GetMemberInvoiceByID(int ID)
        {
            return MyDAL.GetMemberInvoiceByID(ID);
        }

        public virtual IList<MemberInvoiceInfo> GetMemberInvoices(QueryInfo Query)
        {
            return MyDAL.GetMemberInvoices(Query);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }

        public IList<MemberInvoiceInfo> GetMemberInvoicesByMemberID(int MemberID)
        {
            return MyDAL.GetMemberInvoicesByMemberID(MemberID);
        }
    }
}
