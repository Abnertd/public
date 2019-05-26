using System;
using System.Collections.Generic;

namespace Glaer.Trade.B2C.Model
{
    /// <summary>
    /// 订单实体类
    /// </summary>
    public class OrdersInfo
    {
        private int _Orders_ID;
        private string _Orders_SN;
        private int _Orders_Type;
        private int _Orders_ContractID;
        private int _Orders_BuyerType;
        private int _Orders_BuyerID;
        private int _Orders_SysUserID;
        private int _Orders_Status;
        private int _Orders_ERPSyncStatus;
        private int _Orders_PaymentStatus;
        private DateTime _Orders_PaymentStatus_Time;
        private int _Orders_DeliveryStatus;
        private DateTime _Orders_DeliveryStatus_Time;
        private int _Orders_InvoiceStatus;
        private int _Orders_Fail_SysUserID;
        private string _Orders_Fail_Note;
        private DateTime _Orders_Fail_Addtime;
        private int _Orders_IsReturnCoin;
        private double _Orders_Total_MKTPrice;
        private double _Orders_Total_Price;
        private double _Orders_Total_Freight;
        private int _Orders_Total_Coin;
        private int _Orders_Total_UseCoin;
        private double _Orders_Total_PriceDiscount;
        private double _Orders_Total_FreightDiscount;
        private string _Orders_Total_PriceDiscount_Note;
        private string _Orders_Total_FreightDiscount_Note;
        private double _Orders_Total_AllPrice;
        private int _Orders_Address_ID;
        private string _Orders_Address_Country;
        private string _Orders_Address_State;
        private string _Orders_Address_City;
        private string _Orders_Address_County;
        private string _Orders_Address_StreetAddress;
        private string _Orders_Address_Zip;
        private string _Orders_Address_Name;
        private string _Orders_Address_Phone_Countrycode;
        private string _Orders_Address_Phone_Areacode;
        private string _Orders_Address_Phone_Number;
        private string _Orders_Address_Mobile;
        private int _Orders_Delivery_Time_ID;
        private int _Orders_Delivery;
        private string _Orders_Delivery_Name;
        private int _Orders_Payway;
        private string _Orders_Payway_Name;
        private int _Orders_PayType;
        private string _Orders_PayType_Name;
        private string _Orders_Note;
        private string _Orders_Admin_Note;
        private int _Orders_Admin_Sign;
        private string _Orders_Site;
        private int _Orders_SourceType;
        private string _Orders_Source;
        private string _Orders_VerifyCode;
        private int _U_Orders_IsMonitor;
        private DateTime _Orders_Addtime;
        private string _Orders_From;
        private double _Orders_Account_Pay;
        private int _Orders_IsEvaluate;
        private int _Orders_IsSettling;
        private int _Orders_SupplierID = 0;
        private int _Orders_PurchaseID;
        private int _Orders_PriceReportID;
        private int _Orders_MemberStatus;
        private DateTime _Orders_MemberStatus_Time;
        private int _Orders_SupplierStatus;
        private DateTime _Orders_SupplierStatus_Time;
        private string _Orders_ContractAdd;
        private double _Orders_ApplyCreditAmount;
        private string _Orders_AgreementNo;
        private int _Orders_LoanTermID;
        private int _Orders_LoanMethodID;
        private double _Orders_Fee;
        private double _Orders_MarginFee;
        private double _Orders_FeeRate;
        private double _Orders_MarginRate;
        private string _Orders_cashier_url;
        private string _Orders_Loan_proj_no;
        private int _Orders_Responsible;
        private int _Orders_IsShow;



        /// <summary>
        /// 主键，编号
        /// </summary>
        public int Orders_ID
        {
            get { return _Orders_ID; }
            set { _Orders_ID = value; }
        }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string Orders_SN
        {
            get { return _Orders_SN; }
            set { _Orders_SN = value.Length > 20 ? value.Substring(0, 20) : value.ToString(); }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Orders_Type
        {
            get { return _Orders_Type; }
            set { _Orders_Type = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Orders_ContractID
        {
            get { return _Orders_ContractID; }
            set { _Orders_ContractID = value; }
        }

        public int Orders_BuyerType
        {
            get { return _Orders_BuyerType; }
            set { _Orders_BuyerType = value; }
        }
        /// <summary>
        /// 购买人编号
        /// </summary>
        public int Orders_BuyerID
        {
            get { return _Orders_BuyerID; }
            set { _Orders_BuyerID = value; }
        }
        /// <summary>
        /// 客服编号
        /// </summary>
        public int Orders_SysUserID
        {
            get { return _Orders_SysUserID; }
            set { _Orders_SysUserID = value; }
        }
        /// <summary>
        /// 订单状态（0：未确认；1：已确认；2：交易成功；3：交易失败；4：申请退换货；5：申请退款）
        /// </summary>
        public int Orders_Status
        {
            get { return _Orders_Status; }
            set { _Orders_Status = value; }
        }
        /// <summary>
        /// Erp同步状态
        /// </summary>
        public int Orders_ERPSyncStatus
        {
            get { return _Orders_ERPSyncStatus; }
            set { _Orders_ERPSyncStatus = value; }
        }
        /// <summary>
        /// 支付状态（0：未支付；1：已支付；2：已退款；3：退款处理中；4：已支付）
        /// </summary>
        public int Orders_PaymentStatus
        {
            get { return _Orders_PaymentStatus; }
            set { _Orders_PaymentStatus = value; }
        }
        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime Orders_PaymentStatus_Time
        {
            get { return _Orders_PaymentStatus_Time; }
            set { _Orders_PaymentStatus_Time = value; }
        }
        /// <summary>
        /// 配送状态（0:未发货；1：已发货；2：已签收；3已拒收；4退货处理中；5以退货；6；配货中；7：已备货）
        /// </summary>
        public int Orders_DeliveryStatus
        {
            get { return _Orders_DeliveryStatus; }
            set { _Orders_DeliveryStatus = value; }
        }
        /// <summary>
        /// 配送时间
        /// </summary>
        public DateTime Orders_DeliveryStatus_Time
        {
            get { return _Orders_DeliveryStatus_Time; }
            set { _Orders_DeliveryStatus_Time = value; }
        }
        /// <summary>
        /// 发票状态（0：未开票；1：已开票；2：已退票；3：不需要发票）
        /// </summary>
        public int Orders_InvoiceStatus
        {
            get { return _Orders_InvoiceStatus; }
            set { _Orders_InvoiceStatus = value; }
        }

        public int Orders_Fail_SysUserID
        {
            get { return _Orders_Fail_SysUserID; }
            set { _Orders_Fail_SysUserID = value; }
        }

        public string Orders_Fail_Note
        {
            get { return _Orders_Fail_Note; }
            set { _Orders_Fail_Note = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }

        public DateTime Orders_Fail_Addtime
        {
            get { return _Orders_Fail_Addtime; }
            set { _Orders_Fail_Addtime = value; }
        }

        public int Orders_IsReturnCoin
        {
            get { return _Orders_IsReturnCoin; }
            set { _Orders_IsReturnCoin = value; }
        }

        public double Orders_Total_MKTPrice
        {
            get { return _Orders_Total_MKTPrice; }
            set { _Orders_Total_MKTPrice = value; }
        }

        public double Orders_Total_Price
        {
            get { return _Orders_Total_Price; }
            set { _Orders_Total_Price = value; }
        }

        public double Orders_Total_Freight
        {
            get { return _Orders_Total_Freight; }
            set { _Orders_Total_Freight = value; }
        }

        public int Orders_Total_Coin
        {
            get { return _Orders_Total_Coin; }
            set { _Orders_Total_Coin = value; }
        }

        public int Orders_Total_UseCoin
        {
            get { return _Orders_Total_UseCoin; }
            set { _Orders_Total_UseCoin = value; }
        }

        public double Orders_Total_PriceDiscount
        {
            get { return _Orders_Total_PriceDiscount; }
            set { _Orders_Total_PriceDiscount = value; }
        }

        public double Orders_Total_FreightDiscount
        {
            get { return _Orders_Total_FreightDiscount; }
            set { _Orders_Total_FreightDiscount = value; }
        }

        public string Orders_Total_PriceDiscount_Note
        {
            get { return _Orders_Total_PriceDiscount_Note; }
            set { _Orders_Total_PriceDiscount_Note = value.Length > 300 ? value.Substring(0, 300) : value.ToString(); }
        }

        public string Orders_Total_FreightDiscount_Note
        {
            get { return _Orders_Total_FreightDiscount_Note; }
            set { _Orders_Total_FreightDiscount_Note = value.Length > 300 ? value.Substring(0, 300) : value.ToString(); }
        }

        public double Orders_Total_AllPrice
        {
            get { return _Orders_Total_AllPrice; }
            set { _Orders_Total_AllPrice = value; }
        }

        public int Orders_Address_ID
        {
            get { return _Orders_Address_ID; }
            set { _Orders_Address_ID = value; }
        }

        public string Orders_Address_Country
        {
            get { return _Orders_Address_Country; }
            set { _Orders_Address_Country = value.Length > 8 ? value.Substring(0, 8) : value.ToString(); }
        }

        public string Orders_Address_State
        {
            get { return _Orders_Address_State; }
            set { _Orders_Address_State = value.Length > 8 ? value.Substring(0, 8) : value.ToString(); }
        }

        public string Orders_Address_City
        {
            get { return _Orders_Address_City; }
            set { _Orders_Address_City = value.Length > 8 ? value.Substring(0, 8) : value.ToString(); }
        }

        public string Orders_Address_County
        {
            get { return _Orders_Address_County; }
            set { _Orders_Address_County = value.Length > 8 ? value.Substring(0, 8) : value.ToString(); }
        }

        public string Orders_Address_StreetAddress
        {
            get { return _Orders_Address_StreetAddress; }
            set { _Orders_Address_StreetAddress = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string Orders_Address_Zip
        {
            get { return _Orders_Address_Zip; }
            set { _Orders_Address_Zip = value.Length > 20 ? value.Substring(0, 20) : value.ToString(); }
        }

        public string Orders_Address_Name
        {
            get { return _Orders_Address_Name; }
            set { _Orders_Address_Name = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string Orders_Address_Phone_Countrycode
        {
            get { return _Orders_Address_Phone_Countrycode; }
            set { _Orders_Address_Phone_Countrycode = value.Length > 20 ? value.Substring(0, 20) : value.ToString(); }
        }

        public string Orders_Address_Phone_Areacode
        {
            get { return _Orders_Address_Phone_Areacode; }
            set { _Orders_Address_Phone_Areacode = value.Length > 20 ? value.Substring(0, 20) : value.ToString(); }
        }

        public string Orders_Address_Phone_Number
        {
            get { return _Orders_Address_Phone_Number; }
            set { _Orders_Address_Phone_Number = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string Orders_Address_Mobile
        {
            get { return _Orders_Address_Mobile; }
            set { _Orders_Address_Mobile = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public int Orders_Delivery_Time_ID
        {
            get { return _Orders_Delivery_Time_ID; }
            set { _Orders_Delivery_Time_ID = value; }
        }

        public int Orders_Delivery
        {
            get { return _Orders_Delivery; }
            set { _Orders_Delivery = value; }
        }

        public string Orders_Delivery_Name
        {
            get { return _Orders_Delivery_Name; }
            set { _Orders_Delivery_Name = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int Orders_Payway
        {
            get { return _Orders_Payway; }
            set { _Orders_Payway = value; }
        }

        public string Orders_Payway_Name
        {
            get { return _Orders_Payway_Name; }
            set { _Orders_Payway_Name = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int Orders_PayType
        {
            get { return _Orders_PayType; }
            set { _Orders_PayType = value; }
        }

        public string Orders_PayType_Name
        {
            get { return _Orders_PayType_Name; }
            set { _Orders_PayType_Name = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Orders_Note
        {
            get { return _Orders_Note; }
            set { _Orders_Note = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string Orders_Admin_Note
        {
            get { return _Orders_Admin_Note; }
            set { _Orders_Admin_Note = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }

        public int Orders_Admin_Sign
        {
            get { return _Orders_Admin_Sign; }
            set { _Orders_Admin_Sign = value; }
        }

        public string Orders_Site
        {
            get { return _Orders_Site; }
            set { _Orders_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int Orders_SourceType
        {
            get { return _Orders_SourceType; }
            set { _Orders_SourceType = value; }
        }

        public string Orders_Source
        {
            get { return _Orders_Source; }
            set { _Orders_Source = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string Orders_VerifyCode
        {
            get { return _Orders_VerifyCode; }
            set { _Orders_VerifyCode = value.Length > 128 ? value.Substring(0, 128) : value.ToString(); }
        }

        public int U_Orders_IsMonitor
        {
            get { return _U_Orders_IsMonitor; }
            set { _U_Orders_IsMonitor = value; }
        }

        public DateTime Orders_Addtime
        {
            get { return _Orders_Addtime; }
            set { _Orders_Addtime = value; }
        }

        public string Orders_From
        {
            get { return _Orders_From; }
            set { _Orders_From = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public double Orders_Account_Pay
        {
            get { return _Orders_Account_Pay; }
            set { _Orders_Account_Pay = value; }
        }

        public int Orders_IsEvaluate
        {
            get { return _Orders_IsEvaluate; }
            set { _Orders_IsEvaluate = value; }
        }

        public int Orders_IsSettling
        {
            get { return _Orders_IsSettling; }
            set { _Orders_IsSettling = value; }
        }

        public int Orders_SupplierID
        {
            get { return _Orders_SupplierID; }
            set { _Orders_SupplierID = value; }
        }

        public int Orders_PurchaseID
        {
            get { return _Orders_PurchaseID; }
            set { _Orders_PurchaseID = value; }
        }

        public int Orders_PriceReportID
        {
            get { return _Orders_PriceReportID; }
            set { _Orders_PriceReportID = value; }
        }

        public int Orders_MemberStatus
        {
            get { return _Orders_MemberStatus; }
            set { _Orders_MemberStatus = value; }
        }

        public DateTime Orders_MemberStatus_Time
        {
            get { return _Orders_MemberStatus_Time; }
            set { _Orders_MemberStatus_Time = value; }
        }

        public int Orders_SupplierStatus
        {
            get { return _Orders_SupplierStatus; }
            set { _Orders_SupplierStatus = value; }
        }

        public DateTime Orders_SupplierStatus_Time
        {
            get { return _Orders_SupplierStatus_Time; }
            set { _Orders_SupplierStatus_Time = value; }
        }

        public string Orders_ContractAdd
        {
            get { return _Orders_ContractAdd; }
            set { _Orders_ContractAdd = value; }
        }

        public double Orders_ApplyCreditAmount
        {
            get { return _Orders_ApplyCreditAmount; }
            set { _Orders_ApplyCreditAmount = value; }
        }

        public string Orders_AgreementNo
        {
            get { return _Orders_AgreementNo; }
            set { _Orders_AgreementNo = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int Orders_LoanTermID
        {
            get { return _Orders_LoanTermID; }
            set { _Orders_LoanTermID = value; }
        }

        public int Orders_LoanMethodID
        {
            get { return _Orders_LoanMethodID; }
            set { _Orders_LoanMethodID = value; }
        }

        public double Orders_Fee
        {
            get { return _Orders_Fee; }
            set { _Orders_Fee = value; }
        }

        public double Orders_MarginFee
        {
            get { return _Orders_MarginFee; }
            set { _Orders_MarginFee = value; }
        }

        public double Orders_FeeRate
        {
            get { return _Orders_FeeRate; }
            set { _Orders_FeeRate = value; }
        }

        public double Orders_MarginRate
        {
            get { return _Orders_MarginRate; }
            set { _Orders_MarginRate = value; }
        }

        public string Orders_cashier_url
        {
            get { return _Orders_cashier_url; }
            set { _Orders_cashier_url = value; }
        }

        public string Orders_Loan_proj_no
        {
            get { return _Orders_Loan_proj_no; }
            set { _Orders_Loan_proj_no = value; }
        }
        public int Orders_Responsible
        {
            get { return _Orders_Responsible; }
            set { _Orders_Responsible = value; }
        }


        public int Orders_IsShow
        {
            get { return _Orders_IsShow; }
            set { _Orders_IsShow = value; }
        }
    }

    /// <summary>
    /// 订单物品实体类
    /// </summary>
    public class OrdersGoodsInfo
    {
        private int _Orders_Goods_ID;
        private int _Orders_Goods_Type;
        private int _Orders_Goods_ParentID;
        private int _Orders_Goods_OrdersID;
        private int _Orders_Goods_Product_ID;
        private int _Orders_Goods_Product_SupplierID;
        private string _Orders_Goods_Product_Code;
        private int _Orders_Goods_Product_CateID;
        private int _Orders_Goods_Product_BrandID;
        private string _Orders_Goods_Product_Name;
        private string _Orders_Goods_Product_Img;
        private double _Orders_Goods_Product_Price;
        private double _Orders_Goods_Product_MKTPrice;
        private string _Orders_Goods_Product_Maker;
        private string _Orders_Goods_Product_Spec;
        private string _Orders_Goods_Product_DeliveryDate;
        private string _Orders_Goods_Product_AuthorizeCode;
        private string _U_Orders_Goods_Product_BatchCode;
        private string _U_Orders_Goods_Product_BuyChannel;
        private int _U_Orders_Goods_Product_BuyAmount;
        private double _U_Orders_Goods_Product_BuyPrice;
        private double _Orders_Goods_Product_brokerage;
        private double _Orders_Goods_Product_SalePrice;
        private double _Orders_Goods_Product_PurchasingPrice;
        private int _Orders_Goods_Product_Coin;
        private int _Orders_Goods_Product_IsFavor;
        private int _Orders_Goods_Product_UseCoin;
        private double _Orders_Goods_Amount;

        public int Orders_Goods_ID
        {
            get { return _Orders_Goods_ID; }
            set { _Orders_Goods_ID = value; }
        }

        public int Orders_Goods_Type
        {
            get { return _Orders_Goods_Type; }
            set { _Orders_Goods_Type = value; }
        }

        public int Orders_Goods_ParentID
        {
            get { return _Orders_Goods_ParentID; }
            set { _Orders_Goods_ParentID = value; }
        }

        public int Orders_Goods_OrdersID
        {
            get { return _Orders_Goods_OrdersID; }
            set { _Orders_Goods_OrdersID = value; }
        }

        public int Orders_Goods_Product_ID
        {
            get { return _Orders_Goods_Product_ID; }
            set { _Orders_Goods_Product_ID = value; }
        }

        public int Orders_Goods_Product_SupplierID
        {
            get { return _Orders_Goods_Product_SupplierID; }
            set { _Orders_Goods_Product_SupplierID = value; }
        }

        public string Orders_Goods_Product_Code
        {
            get { return _Orders_Goods_Product_Code; }
            set { _Orders_Goods_Product_Code = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int Orders_Goods_Product_CateID
        {
            get { return _Orders_Goods_Product_CateID; }
            set { _Orders_Goods_Product_CateID = value; }
        }

        public int Orders_Goods_Product_BrandID
        {
            get { return _Orders_Goods_Product_BrandID; }
            set { _Orders_Goods_Product_BrandID = value; }
        }

        public string Orders_Goods_Product_Name
        {
            get { return _Orders_Goods_Product_Name; }
            set { _Orders_Goods_Product_Name = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string Orders_Goods_Product_Img
        {
            get { return _Orders_Goods_Product_Img; }
            set { _Orders_Goods_Product_Img = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public double Orders_Goods_Product_Price
        {
            get { return _Orders_Goods_Product_Price; }
            set { _Orders_Goods_Product_Price = value; }
        }

        public double Orders_Goods_Product_MKTPrice
        {
            get { return _Orders_Goods_Product_MKTPrice; }
            set { _Orders_Goods_Product_MKTPrice = value; }
        }

        public string Orders_Goods_Product_Maker
        {
            get { return _Orders_Goods_Product_Maker; }
            set { _Orders_Goods_Product_Maker = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string Orders_Goods_Product_Spec
        {
            get { return _Orders_Goods_Product_Spec; }
            set { _Orders_Goods_Product_Spec = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Orders_Goods_Product_DeliveryDate
        {
            get { return _Orders_Goods_Product_DeliveryDate; }
            set { _Orders_Goods_Product_DeliveryDate = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Orders_Goods_Product_AuthorizeCode
        {
            get { return _Orders_Goods_Product_AuthorizeCode; }
            set { _Orders_Goods_Product_AuthorizeCode = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string U_Orders_Goods_Product_BatchCode
        {
            get { return _U_Orders_Goods_Product_BatchCode; }
            set { _U_Orders_Goods_Product_BatchCode = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string U_Orders_Goods_Product_BuyChannel
        {
            get { return _U_Orders_Goods_Product_BuyChannel; }
            set { _U_Orders_Goods_Product_BuyChannel = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public int U_Orders_Goods_Product_BuyAmount
        {
            get { return _U_Orders_Goods_Product_BuyAmount; }
            set { _U_Orders_Goods_Product_BuyAmount = value; }
        }

        public double U_Orders_Goods_Product_BuyPrice
        {
            get { return _U_Orders_Goods_Product_BuyPrice; }
            set { _U_Orders_Goods_Product_BuyPrice = value; }
        }

        public double Orders_Goods_Product_brokerage
        {
            get { return _Orders_Goods_Product_brokerage; }
            set { _Orders_Goods_Product_brokerage = value; }
        }

        public double Orders_Goods_Product_SalePrice
        {
            get { return _Orders_Goods_Product_SalePrice; }
            set { _Orders_Goods_Product_SalePrice = value; }
        }

        public double Orders_Goods_Product_PurchasingPrice
        {
            get { return _Orders_Goods_Product_PurchasingPrice; }
            set { _Orders_Goods_Product_PurchasingPrice = value; }
        }

        public int Orders_Goods_Product_Coin
        {
            get { return _Orders_Goods_Product_Coin; }
            set { _Orders_Goods_Product_Coin = value; }
        }

        public int Orders_Goods_Product_IsFavor
        {
            get { return _Orders_Goods_Product_IsFavor; }
            set { _Orders_Goods_Product_IsFavor = value; }
        }

        public int Orders_Goods_Product_UseCoin
        {
            get { return _Orders_Goods_Product_UseCoin; }
            set { _Orders_Goods_Product_UseCoin = value; }
        }

        public double Orders_Goods_Amount
        {
            get { return _Orders_Goods_Amount; }
            set { _Orders_Goods_Amount = value; }
        }

        

        

    }


    public class OrdersContractInfo
    {
        private int _ID;
        private string _SN;
        private string _Name;
        private int _Orders_ID;
        private string _Path;
        private DateTime _Addtime;
        private string _Site;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string SN
        {
            get { return _SN; }
            set { _SN = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Name
        {
            get { return _Name; }
            set { _Name = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public int Orders_ID
        {
            get { return _Orders_ID; }
            set { _Orders_ID = value; }
        }

        public string Path
        {
            get { return _Path; }
            set { _Path = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }

        public DateTime Addtime
        {
            get { return _Addtime; }
            set { _Addtime = value; }
        }

        public string Site
        {
            get { return _Site; }
            set { _Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }
    }


    public class OrdersLoanApplyInfo
    {
        private int _ID;
        private int _MemberID;
        private string _Orders_SN;
        private string _Loan_proj_No;
        private double _Loan_Amount;
        private double _Interest_Rate;
        private string _Interest_Rate_Unit;
        private double _Trem;
        private string _Trem_Unit;
        private double _Fee_Amount;
        private string _Repay_Method;
        private double _Margin_Amount;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public int MemberID
        {
            get { return _MemberID; }
            set { _MemberID = value; }
        }

        public string Orders_SN
        {
            get { return _Orders_SN; }
            set { _Orders_SN = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Loan_proj_No
        {
            get { return _Loan_proj_No; }
            set { _Loan_proj_No = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public double Loan_Amount
        {
            get { return _Loan_Amount; }
            set { _Loan_Amount = value; }
        }

        public double Interest_Rate
        {
            get { return _Interest_Rate; }
            set { _Interest_Rate = value; }
        }

        public string Interest_Rate_Unit
        {
            get { return _Interest_Rate_Unit; }
            set { _Interest_Rate_Unit = value.Length > 10 ? value.Substring(0, 10) : value.ToString(); }
        }

        public double Trem
        {
            get { return _Trem; }
            set { _Trem = value; }
        }

        public string Trem_Unit
        {
            get { return _Trem_Unit; }
            set { _Trem_Unit = value.Length > 10 ? value.Substring(0, 10) : value.ToString(); }
        }

        public double Fee_Amount
        {
            get { return _Fee_Amount; }
            set { _Fee_Amount = value; }
        }

        public string Repay_Method
        {
            get { return _Repay_Method; }
            set { _Repay_Method = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public double Margin_Amount
        {
            get { return _Margin_Amount; }
            set { _Margin_Amount = value; }
        }
    }
}
