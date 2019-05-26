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
    public class MemberCertType : IMemberCertType
    {
        protected DAL.MEM.IMemberCertType MyDAL;
        protected IRBAC RBAC;

        public MemberCertType()
        {
            MyDAL = DAL.MEM.MemberCertTypeFactory.CreateMemberCertType();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddMemberCertType(MemberCertTypeInfo entity)
        {
            return MyDAL.AddMemberCertType(entity);
        }

        public virtual bool EditMemberCertType(MemberCertTypeInfo entity)
        {
            return MyDAL.EditMemberCertType(entity);
        }

        public virtual int DelMemberCertType(int ID)
        {
            return MyDAL.DelMemberCertType(ID);
        }

        public virtual MemberCertTypeInfo GetMemberCertTypeByID(int ID)
        {
            return MyDAL.GetMemberCertTypeByID(ID);
        }

        public virtual IList<MemberCertTypeInfo> GetMemberCertTypes(QueryInfo Query)
        {
            return MyDAL.GetMemberCertTypes(Query);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }
    }

    public class MemberCert : IMemberCert
    {
        protected DAL.MEM.IMemberCert MyDAL;
        protected IRBAC RBAC;

        public MemberCert()
        {
            MyDAL = DAL.MEM.MemberCertFactory.CreateMemberCert();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddMemberCert(MemberCertInfo entity)
        {
            return MyDAL.AddMemberCert(entity);
        }

        public virtual bool EditMemberCert(MemberCertInfo entity)
        {
            return MyDAL.EditMemberCert(entity);
        }

        public virtual int DelMemberCert(int ID)
        {
            return MyDAL.DelMemberCert(ID);
        }

        public virtual MemberCertInfo GetMemberCertByID(int ID)
        {
            return MyDAL.GetMemberCertByID(ID);
        }

        public virtual IList<MemberCertInfo> GetMemberCerts(QueryInfo Query)
        {
            return MyDAL.GetMemberCerts(Query);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }
    }
}

