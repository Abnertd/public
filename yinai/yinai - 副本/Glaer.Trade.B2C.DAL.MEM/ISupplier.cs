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
    public interface ISupplier
    {
        bool AddSupplier(SupplierInfo entity);

        bool EditSupplier(SupplierInfo entity);

        bool UpdateSupplierLogin(int Supplier_ID, int Count, string Remote_IP);

        int DelSupplier(int ID);

        SupplierInfo GetSupplierByID(int ID);

        SupplierInfo GetSupplierByEmail(string Email);

        SupplierInfo SupplierLogin(string name);

        IList<SupplierInfo> GetSuppliers(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);

        bool EditSupplierDeliveryFee(SupplierDeliveryFeeInfo entity);

        int DelSupplierDeliveryFee(int Supplier_ID, int Delivery_ID);

        SupplierDeliveryFeeInfo GetSupplierDeliveryFeeByID(int Supplier_ID, int Delivery_ID);

        bool AddSupplierRelateCert(SupplierRelateCertInfo entity);

        bool EditSupplierRelateCert(SupplierRelateCertInfo entity);

        int DelSupplierRelateCertBySupplierID(int ID);
    }

    public interface ISupplierCommissionCategory
    {
        bool AddSupplierCommissionCategory(SupplierCommissionCategoryInfo entity);

        bool EditSupplierCommissionCategory(SupplierCommissionCategoryInfo entity);

        int DelSupplierCommissionCategory(int ID);

        SupplierCommissionCategoryInfo GetSupplierCommissionCategoryByID(int ID);

        IList<SupplierCommissionCategoryInfo> GetSupplierCommissionCategorys(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);
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
