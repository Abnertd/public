using System;
using System.Collections.Generic;

namespace Glaer.Trade.B2C.Model
{
    public class ProductInfo
    {
        private int _Product_ID;
        private string _Product_Code;
        private int _Product_CateID;
        private int _Product_BrandID;
        private int _Product_TypeID;
        private int _Product_SupplierID;
        private int _Product_Supplier_CommissionCateID;
        private string _Product_Name;
        private string _Product_NameInitials;
        private string _Product_SubName;
        private string _Product_SubNameInitials;
        private double _Product_MKTPrice;
        private double _Product_GroupPrice;
        private double _Product_PurchasingPrice;
        private double _Product_Price;
        private string _Product_PriceUnit;
        private string _Product_Unit;
        private int _Product_GroupNum;
        private string _Product_Note;
        private string _Product_NoteColor;
        private string _Product_Audit_Note;
        private double _Product_Weight;
        private string _Product_Img;
        private string _Product_Publisher;
        private int _Product_StockAmount;
        private int _Product_SaleAmount;
        private int _Product_Review_Count;
        private int _Product_Review_ValidCount;
        private double _Product_Review_Average;
        private int _Product_IsInsale;
        private int _Product_IsGroupBuy;
        private int _Product_IsCoinBuy;
        private int _Product_IsFavor;
        private int _Product_IsGift;
        private int _Product_IsGiftCoin;
        private double _Product_Gift_Coin;
        private int _Product_CoinBuy_Coin;
        private int _Product_IsAudit;
        private DateTime _Product_Addtime;
        private string _Product_Intro;
        private int _Product_AlertAmount;
        private int _Product_UsableAmount;
        private int _Product_IsNoStock;
        private string _Product_Spec;
        private string _Product_Maker;
        private string _Product_Description;
        private int _Product_Sort;
        private int _Product_QuotaAmount;
        private int _Product_IsListShow;
        private string _Product_GroupCode;
        private int _Product_Hits;
        private string _Product_Site;
        private string _Product_SEO_Title;
        private string _Product_SEO_Keyword;
        private string _Product_SEO_Description;
        private string _U_Product_Parameters;
        private int _U_Product_SalesByProxy = 0;
        private int _U_Product_Shipper = 0;
        private string _U_Product_DeliveryCycle;
        private int _U_Product_MinBook;
        private int _Product_PriceType;
        private double _Product_ManualFee;
        private string _Product_LibraryImg;
        private string _Product_State_Name;
        private string _Product_City_Name;
        private string _Product_County_Name;

        public int Product_PriceType
        {
            get { return _Product_PriceType; }
            set { _Product_PriceType = value; }
        }

        public int Product_ID
        {
            get { return _Product_ID; }
            set { _Product_ID = value; }
        }

        public string Product_Code
        {
            get { return _Product_Code; }
            set { _Product_Code = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int Product_CateID
        {
            get { return _Product_CateID; }
            set { _Product_CateID = value; }
        }

        public int Product_BrandID
        {
            get { return _Product_BrandID; }
            set { _Product_BrandID = value; }
        }

        public int Product_TypeID
        {
            get { return _Product_TypeID; }
            set { _Product_TypeID = value; }
        }

        public int Product_SupplierID
        {
            get { return _Product_SupplierID; }
            set { _Product_SupplierID = value; }
        }

        public int Product_Supplier_CommissionCateID
        {
            get { return _Product_Supplier_CommissionCateID; }
            set { _Product_Supplier_CommissionCateID = value; }
        }

        public string Product_Name
        {
            get { return _Product_Name; }
            set { _Product_Name = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string Product_NameInitials
        {
            get { return _Product_NameInitials; }
            set { _Product_NameInitials = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string Product_SubName
        {
            get { return _Product_SubName; }
            set { _Product_SubName = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string Product_SubNameInitials
        {
            get { return _Product_SubNameInitials; }
            set { _Product_SubNameInitials = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public double Product_MKTPrice
        {
            get { return _Product_MKTPrice; }
            set { _Product_MKTPrice = value; }
        }

        public double Product_GroupPrice
        {
            get { return _Product_GroupPrice; }
            set { _Product_GroupPrice = value; }
        }

        public double Product_PurchasingPrice
        {
            get { return _Product_PurchasingPrice; }
            set { _Product_PurchasingPrice = value; }
        }

        public double Product_Price
        {
            get { return _Product_Price; }
            set { _Product_Price = value; }
        }

        public string Product_PriceUnit
        {
            get { return _Product_PriceUnit; }
            set { _Product_PriceUnit = value.Length > 10 ? value.Substring(0, 10) : value.ToString(); }
        }

        public string Product_Unit
        {
            get { return _Product_Unit; }
            set { _Product_Unit = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int Product_GroupNum
        {
            get { return _Product_GroupNum; }
            set { _Product_GroupNum = value; }
        }

        public string Product_Note
        {
            get { return _Product_Note; }
            set { _Product_Note = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string Product_NoteColor
        {
            get { return _Product_NoteColor; }
            set { _Product_NoteColor = value.Length > 8 ? value.Substring(0, 8) : value.ToString(); }
        }

        public string Product_Audit_Note
        {
            get { return _Product_Audit_Note; }
            set { _Product_Audit_Note = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }

        public double Product_Weight
        {
            get { return _Product_Weight; }
            set { _Product_Weight = value; }
        }

        public string Product_Img
        {
            get { return _Product_Img; }
            set { _Product_Img = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string Product_Publisher
        {
            get { return _Product_Publisher; }
            set { _Product_Publisher = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int Product_StockAmount
        {
            get { return _Product_StockAmount; }
            set { _Product_StockAmount = value; }
        }

        public int Product_SaleAmount
        {
            get { return _Product_SaleAmount; }
            set { _Product_SaleAmount = value; }
        }

        public int Product_Review_Count
        {
            get { return _Product_Review_Count; }
            set { _Product_Review_Count = value; }
        }

        public int Product_Review_ValidCount
        {
            get { return _Product_Review_ValidCount; }
            set { _Product_Review_ValidCount = value; }
        }

        public double Product_Review_Average
        {
            get { return _Product_Review_Average; }
            set { _Product_Review_Average = value; }
        }

        public int Product_IsInsale
        {
            get { return _Product_IsInsale; }
            set { _Product_IsInsale = value; }
        }

        public int Product_IsGroupBuy
        {
            get { return _Product_IsGroupBuy; }
            set { _Product_IsGroupBuy = value; }
        }

        public int Product_IsCoinBuy
        {
            get { return _Product_IsCoinBuy; }
            set { _Product_IsCoinBuy = value; }
        }

        public int Product_IsFavor
        {
            get { return _Product_IsFavor; }
            set { _Product_IsFavor = value; }
        }

        public int Product_IsGift
        {
            get { return _Product_IsGift; }
            set { _Product_IsGift = value; }
        }

        public int Product_IsGiftCoin
        {
            get { return _Product_IsGiftCoin; }
            set { _Product_IsGiftCoin = value; }
        }

        public double Product_Gift_Coin
        {
            get { return _Product_Gift_Coin; }
            set { _Product_Gift_Coin = value; }
        }

        public int Product_CoinBuy_Coin
        {
            get { return _Product_CoinBuy_Coin; }
            set { _Product_CoinBuy_Coin = value; }
        }

        public int Product_IsAudit
        {
            get { return _Product_IsAudit; }
            set { _Product_IsAudit = value; }
        }

        public DateTime Product_Addtime
        {
            get { return _Product_Addtime; }
            set { _Product_Addtime = value; }
        }

        public string Product_Intro
        {
            get { return _Product_Intro; }
            set { _Product_Intro = value; }
        }

        public int Product_AlertAmount
        {
            get { return _Product_AlertAmount; }
            set { _Product_AlertAmount = value; }
        }

        public int Product_UsableAmount
        {
            get { return _Product_UsableAmount; }
            set { _Product_UsableAmount = value; }
        }

        public int Product_IsNoStock
        {
            get { return _Product_IsNoStock; }
            set { _Product_IsNoStock = value; }
        }

        public string Product_Spec
        {
            get { return _Product_Spec; }
            set { _Product_Spec = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string Product_Maker
        {
            get { return _Product_Maker; }
            set { _Product_Maker = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }

        public string Product_Description
        {
            get { return _Product_Description; }
            set { _Product_Description = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }

        public int Product_Sort
        {
            get { return _Product_Sort; }
            set { _Product_Sort = value; }
        }

        public int Product_QuotaAmount
        {
            get { return _Product_QuotaAmount; }
            set { _Product_QuotaAmount = value; }
        }

        public int Product_IsListShow
        {
            get { return _Product_IsListShow; }
            set { _Product_IsListShow = value; }
        }

        public string Product_GroupCode
        {
            get { return _Product_GroupCode; }
            set { _Product_GroupCode = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int Product_Hits
        {
            get { return _Product_Hits; }
            set { _Product_Hits = value; }
        }
        public string Product_Site
        {
            get { return _Product_Site; }
            set { _Product_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Product_SEO_Title
        {
            get { return _Product_SEO_Title; }
            set { _Product_SEO_Title = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }

        public string Product_SEO_Keyword
        {
            get { return _Product_SEO_Keyword; }
            set { _Product_SEO_Keyword = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }

        public string Product_SEO_Description
        {
            get { return _Product_SEO_Description; }
            set { _Product_SEO_Description = value.Length > 500 ? value.Substring(0, 500) : value.ToString(); }
        }

        public string U_Product_Parameters
        {
            get { return _U_Product_Parameters; }
            set { _U_Product_Parameters = value; }
        }

        public int U_Product_SalesByProxy
        {
            get { return _U_Product_SalesByProxy; }
            set { _U_Product_SalesByProxy = value; }
        }

        public int U_Product_Shipper
        {
            get { return _U_Product_Shipper; }
            set { _U_Product_Shipper = value; }
        }

        public string U_Product_DeliveryCycle
        {
            get { return _U_Product_DeliveryCycle; }
            set { _U_Product_DeliveryCycle = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public int U_Product_MinBook
        {
            get { return _U_Product_MinBook; }
            set { _U_Product_MinBook = value; }
        }

        public double Product_ManualFee
        {
            get { return _Product_ManualFee; }
            set { _Product_ManualFee = value; }
        }

        public string Product_LibraryImg
        {
            get { return _Product_LibraryImg; }
            set { _Product_LibraryImg = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }
        public string Product_State_Name
        {
            get { return _Product_State_Name; }
            set { _Product_State_Name = value.Length > 8 ? value.Substring(0, 8) : value.ToString(); }
        }
        public string Product_City_Name
        {
            get { return _Product_City_Name; }
            set { _Product_City_Name = value.Length > 8 ? value.Substring(0, 8) : value.ToString(); }

        }
        public string Product_County_Name
        {
            get { return _Product_County_Name; }
            set { _Product_County_Name = value.Length > 8 ? value.Substring(0, 8) : value.ToString(); }

        }
    }

    public class ProductExtendInfo 
    { 
        private int _Product_ID;
        private int _Extent_ID;
        private string _Extend_Value;
        private string _Extend_Img;

        public int Product_ID{
            get { return _Product_ID; }
            set { _Product_ID = value; }
        }

        public int Extent_ID {
            get { return _Extent_ID; }
            set { _Extent_ID = value; }
        }

        public string Extend_Value {
            get { return _Extend_Value; }
            set { _Extend_Value = value.Length > 1000 ? value.Substring(0, 1000) : value.ToString(); }
        }

        public string Extend_Img
        {
            get { return _Extend_Img; }
            set { _Extend_Img = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }
    }

    public class ProductStockInfo
    {
        private int _Product_Stock_Amount;
        private int _Product_Stock_IsNoStock;

        public int Product_Stock_Amount
        {
            get { return _Product_Stock_Amount; }
            set { _Product_Stock_Amount = value; }
        }

        public int Product_Stock_IsNoStock
        {
            get { return _Product_Stock_IsNoStock; }
            set { _Product_Stock_IsNoStock = value; }
        }
    }

    public class ProductDenyReasonInfo
    {
        private int _Product_Deny_Reason_ID;
        private int _Product_Deny_Reason_ProductID;
        private int _Product_Deny_Reason_AuditReasonID;

        public int Product_Deny_Reason_ID
        {
            get { return _Product_Deny_Reason_ID; }
            set { _Product_Deny_Reason_ID = value; }
        }

        public int Product_Deny_Reason_ProductID
        {
            get { return _Product_Deny_Reason_ProductID; }
            set { _Product_Deny_Reason_ProductID = value; }
        }

        public int Product_Deny_Reason_AuditReasonID
        {
            get { return _Product_Deny_Reason_AuditReasonID; }
            set { _Product_Deny_Reason_AuditReasonID = value; }
        }

    }

    public class ProductWholeSalePriceInfo
    {
        private int _Product_WholeSalePrice_ID;
        private int _Product_WholeSalePrice_ProductID;
        private int _Product_WholeSalePrice_MinAmount;
        private int _Product_WholeSalePrice_MaxAmount;
        private double _Product_WholeSalePrice_Price;
        private int _Product_WholeSalePrice_IsActive;
        private string _Product_WholeSalePrice_Site;

        public int Product_WholeSalePrice_ID
        {
            get { return _Product_WholeSalePrice_ID; }
            set { _Product_WholeSalePrice_ID = value; }
        }

        public int Product_WholeSalePrice_ProductID
        {
            get { return _Product_WholeSalePrice_ProductID; }
            set { _Product_WholeSalePrice_ProductID = value; }
        }

        public int Product_WholeSalePrice_MinAmount
        {
            get { return _Product_WholeSalePrice_MinAmount; }
            set { _Product_WholeSalePrice_MinAmount = value; }
        }

        public int Product_WholeSalePrice_MaxAmount
        {
            get { return _Product_WholeSalePrice_MaxAmount; }
            set { _Product_WholeSalePrice_MaxAmount = value; }
        }

        public double Product_WholeSalePrice_Price
        {
            get { return _Product_WholeSalePrice_Price; }
            set { _Product_WholeSalePrice_Price = value; }
        }

        public int Product_WholeSalePrice_IsActive
        {
            get { return _Product_WholeSalePrice_IsActive; }
            set { _Product_WholeSalePrice_IsActive = value; }
        }

        public string Product_WholeSalePrice_Site
        {
            get { return _Product_WholeSalePrice_Site; }
            set { _Product_WholeSalePrice_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

       
    }

}
