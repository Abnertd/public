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
    public interface ISupplier
    {
        bool AddSupplier(SupplierInfo entity, RBACUserInfo UserPrivilege);

        bool EditSupplier(SupplierInfo entity, RBACUserInfo UserPrivilege);

        bool UpdateSupplierLogin(int Supplier_ID, int Count, string Remote_IP, RBACUserInfo UserPrivilege);

        int DelSupplier(int ID, RBACUserInfo UserPrivilege);

        SupplierInfo GetSupplierByID(int ID, RBACUserInfo UserPrivilege);

        SupplierInfo GetSupplierByEmail(string Email, RBACUserInfo UserPrivilege);


        SupplierInfo SupplierLogin(string name, RBACUserInfo UserPrivilege);

        IList<SupplierInfo> GetSuppliers(QueryInfo Query, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);

        bool EditSupplierDeliveryFee(SupplierDeliveryFeeInfo entity);

        int DelSupplierDeliveryFee(int Supplier_ID, int Delivery_ID);

        SupplierDeliveryFeeInfo GetSupplierDeliveryFeeByID(int Supplier_ID, int Delivery_ID);

        bool AddSupplierRelateCert(SupplierRelateCertInfo entity);

        bool EditSupplierRelateCert(SupplierRelateCertInfo entity);

        int DelSupplierRelateCertBySupplierID(int ID);

    }

    public interface ISupplierCommissionCategory
    {
        bool AddSupplierCommissionCategory(SupplierCommissionCategoryInfo entity, RBACUserInfo UserPrivilege);

        bool EditSupplierCommissionCategory(SupplierCommissionCategoryInfo entity, RBACUserInfo UserPrivilege);

        int DelSupplierCommissionCategory(int ID, RBACUserInfo UserPrivilege);

        SupplierCommissionCategoryInfo GetSupplierCommissionCategoryByID(int ID, RBACUserInfo UserPrivilege);

        IList<SupplierCommissionCategoryInfo> GetSupplierCommissionCategorys(QueryInfo Query, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);

    }

    public interface ISupplierMerchants
    {
        bool AddSupplierMerchants(SupplierMerchantsInfo entity);

        bool EditSupplierMerchants(SupplierMerchantsInfo entity);

        int DelSupplierMerchants(int ID);

        SupplierMerchantsInfo GetSupplierMerchantsByID(int ID);

        IList<SupplierMerchantsInfo> GetSupplierMerchantss(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);

    }

    public interface ISupplierMerchantsMessage
    {
        bool AddSupplierMerchantsMessage(SupplierMerchantsMessageInfo entity);

        bool EditSupplierMerchantsMessage(SupplierMerchantsMessageInfo entity);

        int DelSupplierMerchantsMessage(int ID);

        SupplierMerchantsMessageInfo GetSupplierMerchantsMessageByID(int ID);

        IList<SupplierMerchantsMessageInfo> GetSupplierMerchantsMessages(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);

    }

    public interface ISupplierMargin
    {
        bool AddSupplierMargin(SupplierMarginInfo entity);

        bool EditSupplierMargin(SupplierMarginInfo entity);

        int DelSupplierMargin(int ID);

        SupplierMarginInfo GetSupplierMarginByID(int ID);

        SupplierMarginInfo GetSupplierMarginByTypeID(int Type_ID);

        IList<SupplierMarginInfo> GetSupplierMargins(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);

    }
}
