using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public interface ISupplierPriceReport
    {
        bool AddSupplierPriceReport(SupplierPriceReportInfo entity, RBACUserInfo UserPrivilege);

        bool EditSupplierPriceReport(SupplierPriceReportInfo entity, RBACUserInfo UserPrivilege);

        int DelSupplierPriceReport(int ID, RBACUserInfo UserPrivilege);

        SupplierPriceReportInfo GetSupplierPriceReportByID(int ID, RBACUserInfo UserPrivilege);

        IList<SupplierPriceReportInfo> GetSupplierPriceReports(QueryInfo Query, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);

    }
    public interface ISupplierPriceReportDetail
    {
        bool AddSupplierPriceReportDetail(SupplierPriceReportDetailInfo entity, RBACUserInfo UserPrivilege);

        bool EditSupplierPriceReportDetail(SupplierPriceReportDetailInfo entity, RBACUserInfo UserPrivilege);

        int DelSupplierPriceReportDetail(int ID, RBACUserInfo UserPrivilege);

        int DelSupplierPriceReportDetailByPriceReportID(int ID);

        SupplierPriceReportDetailInfo GetSupplierPriceReportDetailByID(int ID, RBACUserInfo UserPrivilege);

        IList<SupplierPriceReportDetailInfo> GetSupplierPriceReportDetailsByPriceReportID(int ID);
        SupplierPriceReportDetailInfo GetSupplierPriceReportDetailByPurchaseDetailID(int Detail_PurchaseID, int Detail_PurchaseDetailID);

        IList<SupplierPriceReportDetailInfo> GetSupplierPriceReportDetails(QueryInfo Query, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);

    }
}
