using System;

namespace Glaer.Trade.B2C.Model
{
    public class ConfigInfo
    {
        private int _Sys_Config_ID;
        private string _Upload_Server_URL;
        private string _Upload_Server_Return_WWW;
        private string _Upload_Server_Return_Admin;
        private string _Upload_Server_Return_Supplier;
        private string _Language_Define;
        private string _Site_DomainName;	
        private string _Site_Name;
        private string _Site_URL;
        private string _Site_Logo;
        private string _Site_Tel;
        private string _Site_Fax;
        private string _Site_Email;
        private string _Site_Keyword;
        private string _Site_Description;
        private string _Site_Title;
        private string _Mail_Server;
        private int _Mail_ServerPort;
        private int _Mail_EnableSsl;
        private string _Mail_ServerUserName;
        private string _Mail_ServerPassWord;
        private string _Mail_FromName;
        private string _Mail_From;
        private string _Mail_Replyto;
        private string _Mail_Encode;
        private string _Coin_Name;
        private int _Coin_Rate;
        private string _Sys_Config_Site;
        private int _Trade_Contract_IsActive;
        private int _Static_IsEnable;
        private string _Chinabank_Code;
        private string _Chinabank_Key;
        private string _Alipay_Email;
        private string _Alipay_Code;
        private string _Alipay_Key;
        private string _Tenpay_Code;
        private string _Tenpay_Key;
        private string _CreditPayment_Code;
        private int _RepidBuy_IsEnable;
        private int _Coupon_UsedAmount;
        private string _Shop_Second_Domain;
        private string _Sys_Sensitive_Keyword;
        private double _Keyword_Adv_MinPrice;
        private double _Instant_GoldPrice;
        private double _Instant_SilverPrice;
        private string _Sys_Delivery_Code;

        public int Sys_Config_ID
        {
            get { return _Sys_Config_ID; }
            set { _Sys_Config_ID = value; }
        }

        public string Upload_Server_URL
        {
            get { return _Upload_Server_URL; }
            set { _Upload_Server_URL = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string Upload_Server_Return_WWW
        {
            get { return _Upload_Server_Return_WWW; }
            set { _Upload_Server_Return_WWW = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string Upload_Server_Return_Admin
        {
            get { return _Upload_Server_Return_Admin; }
            set { _Upload_Server_Return_Admin = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string Upload_Server_Return_Supplier
        {
            get { return _Upload_Server_Return_Supplier; }
            set { _Upload_Server_Return_Supplier = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string Language_Define
        {
            get { return _Language_Define; }
            set { _Language_Define = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string Site_DomainName
        {
            get { return _Site_DomainName; }
            set { _Site_DomainName = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Site_Name
        {
            get { return _Site_Name; }
            set { _Site_Name = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Site_URL
        {
            get { return _Site_URL; }
            set { _Site_URL = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Site_Logo
        {
            get { return _Site_Logo; }
            set { _Site_Logo = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Site_Tel
        {
            get { return _Site_Tel; }
            set { _Site_Tel = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Site_Fax
        {
            get { return _Site_Fax; }
            set { _Site_Fax = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Site_Email
        {
            get { return _Site_Email; }
            set { _Site_Email = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Site_Keyword
        {
            get { return _Site_Keyword; }
            set { _Site_Keyword = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }

        public string Site_Description
        {
            get { return _Site_Description; }
            set { _Site_Description = value.Length > 500 ? value.Substring(0, 500) : value.ToString(); }
        }

        public string Site_Title
        {
            get { return _Site_Title; }
            set { _Site_Title = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }

        public string Mail_Server
        {
            get { return _Mail_Server; }
            set { _Mail_Server = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int Mail_ServerPort
        {
            get { return _Mail_ServerPort; }
            set { _Mail_ServerPort = value; }
        }

        public int Mail_EnableSsl
        {
            get { return _Mail_EnableSsl; }
            set { _Mail_EnableSsl = value; }
        }

        public string Mail_ServerUserName
        {
            get { return _Mail_ServerUserName; }
            set { _Mail_ServerUserName = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Mail_ServerPassWord
        {
            get { return _Mail_ServerPassWord; }
            set { _Mail_ServerPassWord = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Mail_FromName
        {
            get { return _Mail_FromName; }
            set { _Mail_FromName = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Mail_From
        {
            get { return _Mail_From; }
            set { _Mail_From = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Mail_Replyto
        {
            get { return _Mail_Replyto; }
            set { _Mail_Replyto = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Mail_Encode
        {
            get { return _Mail_Encode; }
            set { _Mail_Encode = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Coin_Name
        {
            get { return _Coin_Name; }
            set { _Coin_Name = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int Coin_Rate
        {
            get { return _Coin_Rate; }
            set { _Coin_Rate = value; }
        }

        public string Sys_Config_Site
        {
            get { return _Sys_Config_Site; }
            set { _Sys_Config_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int Trade_Contract_IsActive
        {
            get { return _Trade_Contract_IsActive; }
            set { _Trade_Contract_IsActive = value; }
        }

        public int Static_IsEnable
        {
            get { return _Static_IsEnable; }
            set { _Static_IsEnable = value; }
        }

        public string Chinabank_Code
        {
            get { return _Chinabank_Code; }
            set { _Chinabank_Code = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Chinabank_Key
        {
            get { return _Chinabank_Key; }
            set { _Chinabank_Key = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Alipay_Email
        {
            get { return _Alipay_Email; }
            set { _Alipay_Email = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string Alipay_Code
        {
            get { return _Alipay_Code; }
            set { _Alipay_Code = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Alipay_Key
        {
            get { return _Alipay_Key; }
            set { _Alipay_Key = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Tenpay_Code
        {
            get { return _Tenpay_Code; }
            set { _Tenpay_Code = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Tenpay_Key
        {
            get { return _Tenpay_Key; }
            set { _Tenpay_Key = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string CreditPayment_Code
        {
            get { return _CreditPayment_Code; }
            set { _CreditPayment_Code = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int RepidBuy_IsEnable
        {
            get { return _RepidBuy_IsEnable; }
            set { _RepidBuy_IsEnable = value; }
        }

        public int Coupon_UsedAmount
        {
            get { return _Coupon_UsedAmount; }
            set { _Coupon_UsedAmount = value; }
        }

        public string Sys_Sensitive_Keyword
        {
            get { return _Sys_Sensitive_Keyword; }
            set { _Sys_Sensitive_Keyword = value; }
        }

        public string Shop_Second_Domain
        {
            get { return _Shop_Second_Domain; }
            set { _Shop_Second_Domain = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public double Keyword_Adv_MinPrice
        {
            get { return _Keyword_Adv_MinPrice; }
            set { _Keyword_Adv_MinPrice = value; }
        }

        public double Instant_GoldPrice
        {
            get { return _Instant_GoldPrice; }
            set { _Instant_GoldPrice = value; }
        }

        public double Instant_SilverPrice
        {
            get { return _Instant_SilverPrice; }
            set { _Instant_SilverPrice = value; }
        }

        public string Sys_Delivery_Code
        {
            get { return _Sys_Delivery_Code; }
            set { _Sys_Delivery_Code = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

    }

    public class SysInterfaceLogInfo
    {
        private int _Log_ID;
        private int _Log_Type;
        private string _Log_Action;
        private string _Log_Result;
        private string _Log_Parameters;
        private string _Log_Remark;
        private DateTime _Log_Addtime;

        public int Log_ID
        {
            get { return _Log_ID; }
            set { _Log_ID = value; }
        }

        public int Log_Type
        {
            get { return _Log_Type; }
            set { _Log_Type = value; }
        }

        public string Log_Action
        {
            get { return _Log_Action; }
            set { _Log_Action = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Log_Result
        {
            get { return _Log_Result; }
            set { _Log_Result = value; }
        }

        public string Log_Parameters
        {
            get { return _Log_Parameters; }
            set { _Log_Parameters = value.Length > 1000 ? value.Substring(0, 1000) : value.ToString(); }
        }

        public string Log_Remark
        {
            get { return _Log_Remark; }
            set { _Log_Remark = value; }
        }

        public DateTime Log_Addtime
        {
            get { return _Log_Addtime; }
            set { _Log_Addtime = value; }
        }
    }
}
