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
    public interface IMemberCertType
    {
        bool AddMemberCertType(MemberCertTypeInfo entity);

        bool EditMemberCertType(MemberCertTypeInfo entity);

        int DelMemberCertType(int ID);

        MemberCertTypeInfo GetMemberCertTypeByID(int ID);

        IList<MemberCertTypeInfo> GetMemberCertTypes(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);

    }

    public interface IMemberCert
    {
        bool AddMemberCert(MemberCertInfo entity);

        bool EditMemberCert(MemberCertInfo entity);

        int DelMemberCert(int ID);

        MemberCertInfo GetMemberCertByID(int ID);

        IList<MemberCertInfo> GetMemberCerts(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);
    }
}
