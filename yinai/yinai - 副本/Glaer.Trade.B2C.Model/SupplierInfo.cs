using System;
using System.Collections.Generic;
using System.Text;

namespace Glaer.Trade.B2C.Model
{
    public class SupplierInfo
    {
        private int _Supplier_ID;
        private int _Supplier_Type;
        private int _Supplier_GradeID;
        private string _Supplier_Nickname;
        private string _Supplier_Email;
        private string _Supplier_Password;
        private string _Supplier_CompanyName;
        private string _Supplier_County;
        private string _Supplier_City;
        private string _Supplier_State;
        private string _Supplier_Country;
        private string _Supplier_Address;
        private string _Supplier_Phone;
        private string _Supplier_Fax;
        private string _Supplier_Zip;
        private string _Supplier_Contactman;
        private string _Supplier_Mobile;
        private int _Supplier_IsHaveShop;
        private int _Supplier_IsApply;
        private int _Supplier_ShopType;
        private int _Supplier_Mode;
        private int _Supplier_DeliveryMode;
        private double _Supplier_Account;
        private double _Supplier_Adv_Account;
        private double _Supplier_Security_Account;
        private double _Supplier_CreditLimit;
        private double _Supplier_CreditLimitRemain;
        private int _Supplier_CreditLimit_Expires;
        private double _Supplier_TempCreditLimit;
        private double _Supplier_TempCreditLimitRemain;
        private string _Supplier_TempCreditLimit_ContractSN;
        private int _Supplier_TempCreditLimit_Expires;
        private int _Supplier_CoinCount;
        private int _Supplier_CoinRemain;
        private int _Supplier_Status;
        private int _Supplier_AuditStatus;
        private int _Supplier_Cert_Status;
        private int _Supplier_LoginCount;
        private int _Supplier_CertType;
        private string _Supplier_LoginIP;
        private DateTime _Supplier_Lastlogintime;
        private string _Supplier_VerifyCode;
        private string _Supplier_RegIP;
        private DateTime _Supplier_Addtime;
        private int _Supplier_AllowSysMessage;
        private string _Supplier_SysMobile;
        private int _Supplier_AllowSysEmail;
        private string _Supplier_SysEmail;
        private int _Supplier_AllowOrderEmail;
        private int _Supplier_Trash;
        private int _Supplier_FavorMonth;
        private double _Supplier_AgentRate;
        private string _Supplier_Site;
        private IList<SupplierRelateCertInfo> _SupplierRelateCertInfos;
        private int _Supplier_Emailverify;
        private int _Supplier_ContractID;
        private string _Supplier_SealImg;
        private string _Supplier_VfinanceID;
        private string _Supplier_Corporate;
        private string _Supplier_CorporateMobile;
        private double _Supplier_RegisterFunds;
        private string _Supplier_BusinessCode;
        private string _Supplier_OrganizationCode;
        private string _Supplier_TaxationCode;
        private string _Supplier_BankAccountCode;
        private int _Supplier_IsAuthorize;
        private int _Supplier_IsTrademark;
        private string _Supplier_ServicesPhone;
        private int _Supplier_OperateYear;
        private string _Supplier_ContactEmail;
        private string _Supplier_ContactQQ;
        private string _Supplier_Category;
        private int _Supplier_SaleType;
        //private string _Supplier_Introduce;
        private int _Supplier_MerchantMar_Status;


        private int _Supplier_SQSISActive;

        public int Supplier_ID
        {
            get { return _Supplier_ID; }
            set { _Supplier_ID = value; }
        }

        public int Supplier_Type
        {
            get { return _Supplier_Type; }
            set { _Supplier_Type = value; }
        }

        public int Supplier_GradeID
        {
            get { return _Supplier_GradeID; }
            set { _Supplier_GradeID = value; }
        }

        public string Supplier_Nickname
        {
            get { return _Supplier_Nickname; }
            set { _Supplier_Nickname = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        } 

        public string Supplier_Email
        {
            get { return _Supplier_Email; }
            set { _Supplier_Email = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string Supplier_Password
        {
            get { return _Supplier_Password; }
            set { _Supplier_Password = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Supplier_CompanyName
        {
            get { return _Supplier_CompanyName; }
            set { _Supplier_CompanyName = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string Supplier_County
        {
            get { return _Supplier_County; }
            set { _Supplier_County = value.Length > 8 ? value.Substring(0, 8) : value.ToString(); }
        }

        public string Supplier_City
        {
            get { return _Supplier_City; }
            set { _Supplier_City = value.Length > 8 ? value.Substring(0, 8) : value.ToString(); }
        }

        public string Supplier_State
        {
            get { return _Supplier_State; }
            set { _Supplier_State = value.Length > 8 ? value.Substring(0, 8) : value.ToString(); }
        }

        public string Supplier_Country
        {
            get { return _Supplier_Country; }
            set { _Supplier_Country = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Supplier_Address
        {
            get { return _Supplier_Address; }
            set { _Supplier_Address = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string Supplier_Phone
        {
            get { return _Supplier_Phone; }
            set { _Supplier_Phone = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Supplier_Fax
        {
            get { return _Supplier_Fax; }
            set { _Supplier_Fax = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Supplier_Zip
        {
            get { return _Supplier_Zip; }
            set { _Supplier_Zip = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Supplier_Contactman
        {
            get { return _Supplier_Contactman; }
            set { _Supplier_Contactman = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Supplier_Mobile
        {
            get { return _Supplier_Mobile; }
            set { _Supplier_Mobile = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int Supplier_IsHaveShop
        {
            get { return _Supplier_IsHaveShop; }
            set { _Supplier_IsHaveShop = value; }
        }

        public int Supplier_IsApply
        {
            get { return _Supplier_IsApply; }
            set { _Supplier_IsApply = value; }
        }

        public int Supplier_ShopType
        {
            get { return _Supplier_ShopType; }
            set { _Supplier_ShopType = value; }
        }

        public int Supplier_Mode
        {
            get { return _Supplier_Mode; }
            set { _Supplier_Mode = value; }
        }

        public int Supplier_DeliveryMode
        {
            get { return _Supplier_DeliveryMode; }
            set { _Supplier_DeliveryMode = value; }
        }

        public double Supplier_Account
        {
            get { return _Supplier_Account; }
            set { _Supplier_Account = value; }
        }

        public double Supplier_Adv_Account
        {
            get { return _Supplier_Adv_Account; }
            set { _Supplier_Adv_Account = value; }
        }

        public double Supplier_Security_Account
        {
            get { return _Supplier_Security_Account; }
            set { _Supplier_Security_Account = value; }
        }

        public double Supplier_CreditLimit
        {
            get { return _Supplier_CreditLimit; }
            set { _Supplier_CreditLimit = value; }
        }

        public double Supplier_CreditLimitRemain
        {
            get { return _Supplier_CreditLimitRemain; }
            set { _Supplier_CreditLimitRemain = value; }
        }

        public int Supplier_CreditLimit_Expires
        {
            get { return _Supplier_CreditLimit_Expires; }
            set { _Supplier_CreditLimit_Expires = value; }
        }

        public double Supplier_TempCreditLimit
        {
            get { return _Supplier_TempCreditLimit; }
            set { _Supplier_TempCreditLimit = value; }
        }

        public double Supplier_TempCreditLimitRemain
        {
            get { return _Supplier_TempCreditLimitRemain; }
            set { _Supplier_TempCreditLimitRemain = value; }
        }

        public string Supplier_TempCreditLimit_ContractSN
        {
            get { return _Supplier_TempCreditLimit_ContractSN; }
            set { _Supplier_TempCreditLimit_ContractSN = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int Supplier_TempCreditLimit_Expires
        {
            get { return _Supplier_TempCreditLimit_Expires; }
            set { _Supplier_TempCreditLimit_Expires = value; }
        }

        public int Supplier_CoinCount
        {
            get { return _Supplier_CoinCount; }
            set { _Supplier_CoinCount = value; }
        }

        public int Supplier_CoinRemain
        {
            get { return _Supplier_CoinRemain; }
            set { _Supplier_CoinRemain = value; }
        }

        public int Supplier_Status
        {
            get { return _Supplier_Status; }
            set { _Supplier_Status = value; }
        }

        public int Supplier_AuditStatus
        {
            get { return _Supplier_AuditStatus; }
            set { _Supplier_AuditStatus = value; }
        }

        public int Supplier_Cert_Status
        {
            get { return _Supplier_Cert_Status; }
            set { _Supplier_Cert_Status = value; }
        }

        public int Supplier_CertType
        {
            get { return _Supplier_CertType; }
            set { _Supplier_CertType = value; }
        }


        public int Supplier_LoginCount
        {
            get { return _Supplier_LoginCount; }
            set { _Supplier_LoginCount = value; }
        }

        public string Supplier_LoginIP
        {
            get { return _Supplier_LoginIP; }
            set { _Supplier_LoginIP = value.Length > 128 ? value.Substring(0, 128) : value.ToString(); }
        }

        public DateTime Supplier_Lastlogintime
        {
            get { return _Supplier_Lastlogintime; }
            set { _Supplier_Lastlogintime = value; }
        }

        public string Supplier_VerifyCode
        {
            get { return _Supplier_VerifyCode; }
            set { _Supplier_VerifyCode = value.Length > 128 ? value.Substring(0, 128) : value.ToString(); }
        }

        public string Supplier_RegIP
        {
            get { return _Supplier_RegIP; }
            set { _Supplier_RegIP = value.Length > 20 ? value.Substring(0, 20) : value.ToString(); }
        }

        public DateTime Supplier_Addtime
        {
            get { return _Supplier_Addtime; }
            set { _Supplier_Addtime = value; }
        }

        public int Supplier_AllowSysMessage
        {
            get { return _Supplier_AllowSysMessage; }
            set { _Supplier_AllowSysMessage = value; }
        }

        public string Supplier_SysMobile
        {
            get { return _Supplier_SysMobile; }
            set { _Supplier_SysMobile = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int Supplier_AllowSysEmail
        {
            get { return _Supplier_AllowSysEmail; }
            set { _Supplier_AllowSysEmail = value; }
        }

        public string Supplier_SysEmail
        {
            get { return _Supplier_SysEmail; }
            set { _Supplier_SysEmail = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }


        public int Supplier_AllowOrderEmail
        {
            get { return _Supplier_AllowOrderEmail; }
            set { _Supplier_AllowOrderEmail = value; }
        }

        public int Supplier_Trash
        {
            get { return _Supplier_Trash; }
            set { _Supplier_Trash = value; }
        }

        public int Supplier_FavorMonth
        {
            get { return _Supplier_FavorMonth; }
            set { _Supplier_FavorMonth = value; }
        }

        public double Supplier_AgentRate
        {
            get { return _Supplier_AgentRate; }
            set { _Supplier_AgentRate = value; }
        }

        public string Supplier_Site
        {
            get { return _Supplier_Site; }
            set { _Supplier_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public IList<SupplierRelateCertInfo> SupplierRelateCertInfos
        {
            get { return _SupplierRelateCertInfos; }
            set { _SupplierRelateCertInfos = value; }
        }

        public int Supplier_Emailverify
        {
            get { return _Supplier_Emailverify; }
            set { _Supplier_Emailverify = value; }
        }

        public int Supplier_ContractID
        {
            get { return _Supplier_ContractID; }
            set { _Supplier_ContractID = value; }
        }

        public string Supplier_SealImg
        {
            get { return _Supplier_SealImg; }
            set { _Supplier_SealImg = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }

        public string Supplier_VfinanceID
        {
            get { return _Supplier_VfinanceID; }
            set { _Supplier_VfinanceID = value.Length > 20 ? value.Substring(0, 20) : value.ToString(); }
        }

        public string Supplier_Corporate
        {
            get { return _Supplier_Corporate; }
            set { _Supplier_Corporate = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Supplier_CorporateMobile
        {
            get { return _Supplier_CorporateMobile; }
            set { _Supplier_CorporateMobile = value.Length > 20 ? value.Substring(0, 20) : value.ToString(); }
        }

        public double Supplier_RegisterFunds
        {
            get { return _Supplier_RegisterFunds; }
            set { _Supplier_RegisterFunds = value; }
        }

        public string Supplier_BusinessCode
        {
            get { return _Supplier_BusinessCode; }
            set { _Supplier_BusinessCode = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Supplier_OrganizationCode
        {
            get { return _Supplier_OrganizationCode; }
            set { _Supplier_OrganizationCode = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Supplier_TaxationCode
        {
            get { return _Supplier_TaxationCode; }
            set { _Supplier_TaxationCode = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Supplier_BankAccountCode
        {
            get { return _Supplier_BankAccountCode; }
            set { _Supplier_BankAccountCode = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int Supplier_IsAuthorize
        {
            get { return _Supplier_IsAuthorize; }
            set { _Supplier_IsAuthorize = value; }
        }

        public int Supplier_IsTrademark
        {
            get { return _Supplier_IsTrademark; }
            set { _Supplier_IsTrademark = value; }
        }

        public string Supplier_ServicesPhone
        {
            get { return _Supplier_ServicesPhone; }
            set { _Supplier_ServicesPhone = value.Length > 20 ? value.Substring(0, 20) : value.ToString(); }
        }

        public int Supplier_OperateYear
        {
            get { return _Supplier_OperateYear; }
            set { _Supplier_OperateYear = value; }
        }

        public string Supplier_ContactEmail
        {
            get { return _Supplier_ContactEmail; }
            set { _Supplier_ContactEmail = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string Supplier_ContactQQ
        {
            get { return _Supplier_ContactQQ; }
            set { _Supplier_ContactQQ = value.Length > 20 ? value.Substring(0, 20) : value.ToString(); }
        }

        public string Supplier_Category
        {
            get { return _Supplier_Category; }
            set { _Supplier_Category = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }

        public int Supplier_SaleType
        {
            get { return _Supplier_SaleType; }
            set { _Supplier_SaleType = value; }
        }
       
        //新加 中信接口
        /// <summary>
        /// 授权书审核
        /// </summary>
        public int Supplier_SQSISActive
        {
            get { return _Supplier_SQSISActive; }
            set { _Supplier_SQSISActive = value; }
        }
        public int Supplier_MerchantMar_Status
        {
            get { return _Supplier_MerchantMar_Status; }
            set { _Supplier_MerchantMar_Status = value; }
        }
    }

    public class SupplierCommissionCategoryInfo
    {
        private int _Supplier_Commission_Cate_ID;
        private int _Supplier_Commission_Cate_SupplierID;
        private string _Supplier_Commission_Cate_Name;
        private double _Supplier_Commission_Cate_Amount;
        private string _Supplier_Commission_Cate_Site;

        public int Supplier_Commission_Cate_ID
        {
            get { return _Supplier_Commission_Cate_ID; }
            set { _Supplier_Commission_Cate_ID = value; }
        }

        public int Supplier_Commission_Cate_SupplierID
        {
            get { return _Supplier_Commission_Cate_SupplierID; }
            set { _Supplier_Commission_Cate_SupplierID = value; }
        }

        public string Supplier_Commission_Cate_Name
        {
            get { return _Supplier_Commission_Cate_Name; }
            set { _Supplier_Commission_Cate_Name = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public double Supplier_Commission_Cate_Amount
        {
            get { return _Supplier_Commission_Cate_Amount; }
            set { _Supplier_Commission_Cate_Amount = value; }
        }

        public string Supplier_Commission_Cate_Site
        {
            get { return _Supplier_Commission_Cate_Site; }
            set { _Supplier_Commission_Cate_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

    }

    public class SupplierDeliveryFeeInfo
    {
        private int _Supplier_Delivery_Fee_ID;
        private int _Supplier_Delivery_Fee_SupplierID;
        private int _Supplier_Delivery_Fee_DeliveryID;
        private double _Supplier_Delivery_Fee_Amount;
        private int _Supplier_Delivery_Fee_Type;
        private int _Supplier_Delivery_Fee_InitialWeight;
        private int _Supplier_Delivery_Fee_UpWeight;
        private double _Supplier_Delivery_Fee_InitialFee;
        private double _Supplier_Delivery_Fee_UpFee;

        public int Supplier_Delivery_Fee_ID
        {
            get { return _Supplier_Delivery_Fee_ID; }
            set { _Supplier_Delivery_Fee_ID = value; }
        }

        public int Supplier_Delivery_Fee_SupplierID
        {
            get { return _Supplier_Delivery_Fee_SupplierID; }
            set { _Supplier_Delivery_Fee_SupplierID = value; }
        }

        public int Supplier_Delivery_Fee_DeliveryID
        {
            get { return _Supplier_Delivery_Fee_DeliveryID; }
            set { _Supplier_Delivery_Fee_DeliveryID = value; }
        }

        public double Supplier_Delivery_Fee_Amount
        {
            get { return _Supplier_Delivery_Fee_Amount; }
            set { _Supplier_Delivery_Fee_Amount = value; }
        }

        public int Supplier_Delivery_Fee_Type
        {
            get { return _Supplier_Delivery_Fee_Type; }
            set { _Supplier_Delivery_Fee_Type = value; }
        }

        public int Supplier_Delivery_Fee_InitialWeight
        {
            get { return _Supplier_Delivery_Fee_InitialWeight; }
            set { _Supplier_Delivery_Fee_InitialWeight = value; }
        }

        public int Supplier_Delivery_Fee_UpWeight
        {
            get { return _Supplier_Delivery_Fee_UpWeight; }
            set { _Supplier_Delivery_Fee_UpWeight = value; }
        }

        public double Supplier_Delivery_Fee_InitialFee
        {
            get { return _Supplier_Delivery_Fee_InitialFee; }
            set { _Supplier_Delivery_Fee_InitialFee = value; }
        }

        public double Supplier_Delivery_Fee_UpFee
        {
            get { return _Supplier_Delivery_Fee_UpFee; }
            set { _Supplier_Delivery_Fee_UpFee = value; }
        }

    }

    public class SupplierRelateCertInfo
    {
        private int _Cert_ID;
        private int _Cert_SupplierID;
        private int _Cert_CertID;
        private string _Cert_Img;
        private string _Cert_Img1;

        public int Cert_ID
        {
            get { return _Cert_ID; }
            set { _Cert_ID = value; }
        }

        public int Cert_SupplierID
        {
            get { return _Cert_SupplierID; }
            set { _Cert_SupplierID = value; }
        }

        public int Cert_CertID
        {
            get { return _Cert_CertID; }
            set { _Cert_CertID = value; }
        }

        public string Cert_Img
        {
            get { return _Cert_Img; }
            set { _Cert_Img = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Cert_Img1
        {
            get { return _Cert_Img1; }
            set { _Cert_Img1 = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

 
    }

    public class SupplierMerchantsInfo
    {
        private int _Merchants_ID;
        private int _Merchants_SupplierID;
        private string _Merchants_Name;
        private int _Merchants_Validity;
        private string _Merchants_Channel;
        private string _Merchants_Advantage;
        private string _Merchants_Trem;
        private string _Merchants_Intro;
        private string _Merchants_Img;
        private DateTime _Merchants_AddTime;
        private string _Merchants_Site;

        public int Merchants_ID
        {
            get { return _Merchants_ID; }
            set { _Merchants_ID = value; }
        }

        public int Merchants_SupplierID
        {
            get { return _Merchants_SupplierID; }
            set { _Merchants_SupplierID = value; }
        }

        public string Merchants_Name
        {
            get { return _Merchants_Name; }
            set { _Merchants_Name = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public int Merchants_Validity
        {
            get { return _Merchants_Validity; }
            set { _Merchants_Validity = value; }
        }

        public string Merchants_Channel
        {
            get { return _Merchants_Channel; }
            set { _Merchants_Channel = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Merchants_Advantage
        {
            get { return _Merchants_Advantage; }
            set { _Merchants_Advantage = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string Merchants_Trem
        {
            get { return _Merchants_Trem; }
            set { _Merchants_Trem = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string Merchants_Intro
        {
            get { return _Merchants_Intro; }
            set { _Merchants_Intro = value; }
        }

        public string Merchants_Img
        {
            get { return _Merchants_Img; }
            set { _Merchants_Img = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }

        public DateTime Merchants_AddTime
        {
            get { return _Merchants_AddTime; }
            set { _Merchants_AddTime = value; }
        }

        public string Merchants_Site
        {
            get { return _Merchants_Site; }
            set { _Merchants_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }
    }

    public class SupplierMerchantsMessageInfo
    {
        private int _Message_ID;
        private int _Message_MemberID;
        private int _Message_MerchantsID;
        private string _Message_Content;
        private string _Message_Contactman;
        private string _Message_ContactMobile;
        private string _Message_ContactEmail;
        private DateTime _Message_AddTime;

        public int Message_ID
        {
            get { return _Message_ID; }
            set { _Message_ID = value; }
        }

        public int Message_MemberID
        {
            get { return _Message_MemberID; }
            set { _Message_MemberID = value; }
        }

        public int Message_MerchantsID
        {
            get { return _Message_MerchantsID; }
            set { _Message_MerchantsID = value; }
        }

        public string Message_Content
        {
            get { return _Message_Content; }
            set { _Message_Content = value.Length > 1000 ? value.Substring(0, 1000) : value.ToString(); }
        }

        public string Message_Contactman
        {
            get { return _Message_Contactman; }
            set { _Message_Contactman = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Message_ContactMobile
        {
            get { return _Message_ContactMobile; }
            set { _Message_ContactMobile = value.Length > 20 ? value.Substring(0, 20) : value.ToString(); }
        }

        public string Message_ContactEmail
        {
            get { return _Message_ContactEmail; }
            set { _Message_ContactEmail = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public DateTime Message_AddTime
        {
            get { return _Message_AddTime; }
            set { _Message_AddTime = value; }
        }
    }

    public class SupplierMarginInfo
    {
        private int _Supplier_Margin_ID;
        private int _Supplier_Margin_Type;
        private double _Supplier_Margin_Amount;
        private string _Supplier_Margin_Site;

        public int Supplier_Margin_ID
        {
            get { return _Supplier_Margin_ID; }
            set { _Supplier_Margin_ID = value; }
        }

        public int Supplier_Margin_Type
        {
            get { return _Supplier_Margin_Type; }
            set { _Supplier_Margin_Type = value; }
        }

        public double Supplier_Margin_Amount
        {
            get { return _Supplier_Margin_Amount; }
            set { _Supplier_Margin_Amount = value; }
        }

        public string Supplier_Margin_Site
        {
            get { return _Supplier_Margin_Site; }
            set { _Supplier_Margin_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

       
    }


    #region 随行单推送
    public class SxdParamInfo
    {
        public string storeid { get; set; }

        public string userid { get; set; }

        public string orderno { get; set; }

        public string sxdid { get; set; }

        public IList<SxdGoodsInfo> goods { get; set; }

        public string[] picture { get; set; }

        public string sign { get; set; }

        public string sign_type{get;set;}
    }

    public class SxdGoodsInfo
    {
        public string name { get; set; }

        public string weight { get; set; }

        public string amount { get; set; }

        public string cost { get; set; }
    }


    public class SxdJsonInfo
    {
        public bool result { get; set; }

        public string msg { get; set; }

        public SxdErrorInfo err { get; set; }
    }

    public class SxdErrorInfo
    {
        public string code { get; set; }

        public string message { get; set; }
    }
    #endregion
}
