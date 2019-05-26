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
   public interface IMemberInvoice
    {
        bool AddMemberInvoice (MemberInvoiceInfo entity);

        bool EditMemberInvoice (MemberInvoiceInfo entity);
        
        int DelMemberInvoice(int ID);

        MemberInvoiceInfo GetMemberInvoiceByID(int ID);

        IList<MemberInvoiceInfo> GetMemberInvoices(QueryInfo Query);

        IList<MemberInvoiceInfo> GetMemberInvoicesByMemberID(int MemberID);

        PageInfo GetPageInfo(QueryInfo Query);
    }
}
