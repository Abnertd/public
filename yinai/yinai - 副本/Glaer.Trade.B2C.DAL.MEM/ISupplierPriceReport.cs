using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public interface ISupplierPriceReport
    {
        bool AddSupplierPriceReport(SupplierPriceReportInfo entity);

        bool EditSupplierPriceReport(SupplierPriceReportInfo entity);

        int DelSupplierPriceReport(int ID);

        SupplierPriceReportInfo GetSupplierPriceReportByID(int ID);

        IList<SupplierPriceReportInfo> GetSupplierPriceReports(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);
    }

    public interface ISupplierPriceReportDetail
    {
        bool AddSupplierPriceReportDetail(SupplierPriceReportDetailInfo entity);

        bool EditSupplierPriceReportDetail(SupplierPriceReportDetailInfo entity);

        int DelSupplierPriceReportDetail(int ID);

        int DelSupplierPriceReportDetailByPriceReportID(int ID);

        SupplierPriceReportDetailInfo GetSupplierPriceReportDetailByID(int ID);
        SupplierPriceReportDetailInfo GetSupplierPriceReportDetailByPurchaseDetailID(int Detail_PurchaseID, int Detail_PurchaseDetailID);

        IList<SupplierPriceReportDetailInfo> GetSupplierPriceReportDetailsByPriceReportID(int ID);

        IList<SupplierPriceReportDetailInfo> GetSupplierPriceReportDetails(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);
    }
}
