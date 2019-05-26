using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public interface ISupplierPurchase
    {
        bool AddSupplierPurchase(SupplierPurchaseInfo entity, RBACUserInfo UserPrivilege);

        bool EditSupplierPurchase(SupplierPurchaseInfo entity, RBACUserInfo UserPrivilege);

        int DelSupplierPurchase(int ID, RBACUserInfo UserPrivilege);

        SupplierPurchaseInfo GetSupplierPurchaseByID(int ID, RBACUserInfo UserPrivilege);

        IList<SupplierPurchaseInfo> GetSupplierPurchasesList(QueryInfo Query, RBACUserInfo UserPrivilege);

        IList<SupplierPurchaseInfo> GetSupplierPurchases(QueryInfo Query, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);

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
        bool AddSupplierPurchaseCategory(SupplierPurchaseCategoryInfo entity, RBACUserInfo UserPrivilege);

        bool EditSupplierPurchaseCategory(SupplierPurchaseCategoryInfo entity, RBACUserInfo UserPrivilege);

        int DelSupplierPurchaseCategory(int ID, RBACUserInfo UserPrivilege);

        SupplierPurchaseCategoryInfo GetSupplierPurchaseCategoryByID(int ID, RBACUserInfo UserPrivilege);

        IList<SupplierPurchaseCategoryInfo> GetSupplierPurchaseCategorys(QueryInfo Query, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);

        IList<SupplierPurchaseCategoryInfo> GetSubSupplierPurchaseCategorys(int Cate_ID, string SiteSign, RBACUserInfo UserPrivilege);

        string SelectSupplierPurchaseCategoryOption(int Cate_ID, int selectVal, string appendTag, int shieldID, string SiteSign, RBACUserInfo UserPrivilege);

        string DisplaySupplierPurchaseCategoryRecursion(int cate_id, string href, RBACUserInfo UserPrivilege);

        string Get_All_SubSupplierPurchaseCateID(int Cate_ID);
    }
}
