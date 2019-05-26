using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public interface ISupplierPurchase
    {
        bool AddSupplierPurchase(SupplierPurchaseInfo entity);

        bool EditSupplierPurchase(SupplierPurchaseInfo entity);

        int DelSupplierPurchase(int ID);

        SupplierPurchaseInfo GetSupplierPurchaseByID(int ID);

        IList<SupplierPurchaseInfo> GetSupplierPurchasesList(QueryInfo Query);

        IList<SupplierPurchaseInfo> GetSupplierPurchases(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);

        bool AddSupplierPurchasePrivate(SupplierPurchasePrivateInfo entity);

        int DelSupplierPurchasePrivateByPurchase(int ID);

        IList<SupplierPurchasePrivateInfo> GetSupplierPurchasePrivatesByPurchase(int ID);
        bool GetSupplierPurchasePrivatesByPurchaseSupplier(int PurchaseID, int SupplierID);
    }
    public interface ISupplierPurchaseDetail
    {
        bool AddSupplierPurchaseDetail(SupplierPurchaseDetailInfo entity);

        bool EditSupplierPurchaseDetail(SupplierPurchaseDetailInfo entity);

        int DelSupplierPurchaseDetail(int ID);

        int DelSupplierPurchaseDetailByPurchaseID(int Apply_ID);

        SupplierPurchaseDetailInfo GetSupplierPurchaseDetailByID(int ID);

        IList<SupplierPurchaseDetailInfo> GetSupplierPurchaseDetails(QueryInfo Query);

        IList<SupplierPurchaseDetailInfo> GetSupplierPurchaseDetailsByPurchaseID(int Apply_ID);

        PageInfo GetPageInfo(QueryInfo Query);
    }

    public interface ISupplierPurchaseCategory
    {
        bool AddSupplierPurchaseCategory(SupplierPurchaseCategoryInfo entity);

        bool EditSupplierPurchaseCategory(SupplierPurchaseCategoryInfo entity);

        int DelSupplierPurchaseCategory(int ID);

        SupplierPurchaseCategoryInfo GetSupplierPurchaseCategoryByID(int ID);

        IList<SupplierPurchaseCategoryInfo> GetSupplierPurchaseCategorys(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);

        int GetSubSupplierPurchaseCateCount(int Cate_ID, string SiteSign);

        IList<SupplierPurchaseCategoryInfo> GetSubSupplierPurchaseCategorys(int Cate_ID, string SiteSign);

        string Get_All_SubSupplierPurchaseCateID(int Cate_ID);
    }
}
