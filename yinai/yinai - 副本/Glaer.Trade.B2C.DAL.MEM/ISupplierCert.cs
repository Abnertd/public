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
    public interface ISupplierCertType
    {
        bool AddSupplierCertType(SupplierCertTypeInfo entity);

        bool EditSupplierCertType(SupplierCertTypeInfo entity);

        int DelSupplierCertType(int ID);

        SupplierCertTypeInfo GetSupplierCertTypeByID(int ID);

        IList<SupplierCertTypeInfo> GetSupplierCertTypes(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);
    }

    public interface ISupplierCert
    {
        bool AddSupplierCert(SupplierCertInfo entity);

        bool EditSupplierCert(SupplierCertInfo entity);

        int DelSupplierCert(int ID);

        SupplierCertInfo GetSupplierCertByID(int ID);

        IList<SupplierCertInfo> GetSupplierCerts(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);
    }
}
