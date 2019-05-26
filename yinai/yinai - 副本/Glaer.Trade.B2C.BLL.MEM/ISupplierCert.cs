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
    public interface ISupplierCert
    {
        bool AddSupplierCert(SupplierCertInfo entity, RBACUserInfo UserPrivilege);

        bool EditSupplierCert(SupplierCertInfo entity, RBACUserInfo UserPrivilege);

        int DelSupplierCert(int ID, RBACUserInfo UserPrivilege);

        SupplierCertInfo GetSupplierCertByID(int ID, RBACUserInfo UserPrivilege);

        IList<SupplierCertInfo> GetSupplierCerts(QueryInfo Query, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);

    }

    public interface ISupplierCertType
    {
        bool AddSupplierCertType(SupplierCertTypeInfo entity);

        bool EditSupplierCertType(SupplierCertTypeInfo entity);

        int DelSupplierCertType(int ID);

        SupplierCertTypeInfo GetSupplierCertTypeByID(int ID);

        IList<SupplierCertTypeInfo> GetSupplierCertTypes(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);

    }
}

