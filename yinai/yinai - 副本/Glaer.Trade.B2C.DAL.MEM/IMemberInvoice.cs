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
    public interface IMemberInvoice
    {
        bool AddMemberInvoice(MemberInvoiceInfo entity);

        bool EditMemberInvoice(MemberInvoiceInfo entity);

        int DelMemberInvoice(int ID);

        MemberInvoiceInfo GetMemberInvoiceByID(int ID);

        IList<MemberInvoiceInfo> GetMemberInvoices(QueryInfo Query);

        IList<MemberInvoiceInfo> GetMemberInvoicesByMemberID(int MemberID);

        PageInfo GetPageInfo(QueryInfo Query);
    }
}
