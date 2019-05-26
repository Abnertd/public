using System;
using System.Collections.Generic;
using System.Text;

namespace Glaer.Trade.B2C.Model
{
    public class MemberInfo
    {
        private int _Member_ID;
        private int _Member_Type;
        private string _Member_Email;
        private int _Member_Emailverify;
        private string _Member_LoginMobile;
        private int _Member_LoginMobileverify;
        private string _Member_NickName;
        private string _Member_Password;
        private string _Member_VerifyCode;
        private int _Member_LoginCount;
        private string _Member_LastLogin_IP;
        private DateTime _Member_LastLogin_Time;
        private int _Member_CoinCount;
        private int _Member_CoinRemain;
        private DateTime _Member_Addtime;
        private int _Member_Trash;
        private int _Member_Grade;
        private double _Member_Account;
        private double _Member_Frozen;
        private int _Member_AllowSysEmail;
        private string _Member_Site;
        private string _Member_Source;
        private string _Member_RegIP;
        private int _Member_Status;
        private int _Member_AuditStatus;
        private int _Member_Cert_Status;
        private string _Member_VfinanceID;
        private string _Member_ERP_StoreID;
        private int _Member_SupplierID;
        private string _Member_Company_Introduce;
        private string _Member_Company_Contact;

        
        private MemberProfileInfo _MemberProfileInfo;
        private IList<MemberRelateCertInfo> _MemberRelateCertInfos;

        public int Member_ID
        {
            get { return _Member_ID; }
            set { _Member_ID = value; }
        }

        public int Member_Type
        {
            get { return _Member_Type; }
            set { _Member_Type = value; }
        }

        public string Member_Email
        {
            get { return _Member_Email; }
            set { _Member_Email = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public int Member_Emailverify
        {
            get { return _Member_Emailverify; }
            set { _Member_Emailverify = value; }
        }

        public string Member_LoginMobile
        {
            get { return _Member_LoginMobile; }
            set { _Member_LoginMobile = value.Length > 20 ? value.Substring(0, 20) : value.ToString(); }
        }

        public int Member_LoginMobileverify
        {
            get { return _Member_LoginMobileverify; }
            set { _Member_LoginMobileverify = value; }
        }

        public string Member_NickName
        {
            get { return _Member_NickName; }
            set { _Member_NickName = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Member_Password
        {
            get { return _Member_Password; }
            set { _Member_Password = value.Length > 64 ? value.Substring(0, 64) : value.ToString(); }
        }

        public string Member_VerifyCode
        {
            get { return _Member_VerifyCode; }
            set { _Member_VerifyCode = value.Length > 128 ? value.Substring(0, 128) : value.ToString(); }
        }

        public int Member_LoginCount
        {
            get { return _Member_LoginCount; }
            set { _Member_LoginCount = value; }
        }

        public string Member_LastLogin_IP
        {
            get { return _Member_LastLogin_IP; }
            set { _Member_LastLogin_IP = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public DateTime Member_LastLogin_Time
        {
            get { return _Member_LastLogin_Time; }
            set { _Member_LastLogin_Time = value; }
        }

        public int Member_CoinCount
        {
            get { return _Member_CoinCount; }
            set { _Member_CoinCount = value; }
        }

        public int Member_CoinRemain
        {
            get { return _Member_CoinRemain; }
            set { _Member_CoinRemain = value; }
        }

        public DateTime Member_Addtime
        {
            get { return _Member_Addtime; }
            set { _Member_Addtime = value; }
        }

        public int Member_Trash
        {
            get { return _Member_Trash; }
            set { _Member_Trash = value; }
        }

        public int Member_Grade
        {
            get { return _Member_Grade; }
            set { _Member_Grade = value; }
        }

        public double Member_Account
        {
            get { return _Member_Account; }
            set { _Member_Account = value; }
        }

        public double Member_Frozen
        {
            get { return _Member_Frozen; }
            set { _Member_Frozen = value; }
        }

        public int Member_AllowSysEmail
        {
            get { return _Member_AllowSysEmail; }
            set { _Member_AllowSysEmail = value; }
        }

        public string Member_Site
        {
            get { return _Member_Site; }
            set { _Member_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Member_Source
        {
            get { return _Member_Source; }
            set { _Member_Source = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public int Member_Status
        {
            get { return _Member_Status; }
            set { _Member_Status = value; }
        }

        public string Member_RegIP
        {
            get { return _Member_RegIP; }
            set { _Member_RegIP = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int Member_AuditStatus
        {
            get { return _Member_AuditStatus; }
            set { _Member_AuditStatus = value; }
        }

        public int Member_Cert_Status
        {
            get { return _Member_Cert_Status; }
            set { _Member_Cert_Status = value; }
        }

        public string Member_VfinanceID
        {
            get { return _Member_VfinanceID; }
            set { _Member_VfinanceID = value.Length > 20 ? value.Substring(0, 20) : value.ToString(); }
        }

        public string Member_ERP_StoreID
        {
            get { return _Member_ERP_StoreID; }
            set { _Member_ERP_StoreID = value; }
        }

        public int Member_SupplierID
        {
            get { return _Member_SupplierID; }
            set { _Member_SupplierID = value; }
        }
        public string Member_Company_Introduce
        {
            get { return _Member_Company_Introduce; }
            set { _Member_Company_Introduce =value; }
        }
        public string Member_Company_Contact
        {
            get { return _Member_Company_Contact; }
            set { _Member_Company_Contact = value; }
        }


        public MemberProfileInfo MemberProfileInfo
        {
            get { return _MemberProfileInfo; }
            set { _MemberProfileInfo = value; }
        }

        public IList<MemberRelateCertInfo> MemberRelateCertInfos
        {
            get { return _MemberRelateCertInfos; }
            set { _MemberRelateCertInfos = value; }
        }
    }

    public class MemberLogInfo
    {
        private int _Log_ID;
        private int _Log_Member_ID;
        private string _Log_Member_Action;
        private DateTime _Log_Addtime;

        public int Log_ID
        {
            get { return _Log_ID; }
            set { _Log_ID = value; }
        }

        public int Log_Member_ID
        {
            get { return _Log_Member_ID; }
            set { _Log_Member_ID = value; }
        }

        public string Log_Member_Action
        {
            get { return _Log_Member_Action; }
            set { _Log_Member_Action = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public DateTime Log_Addtime
        {
            get { return _Log_Addtime; }
            set { _Log_Addtime = value; }
        }

    }

    public class MemberRelateCertInfo
    {
        private int _ID;
        private int _MemberID;
        private int _CertID;
        private string _Img;

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

        public int CertID
        {
            get { return _CertID; }
            set { _CertID = value; }
        }

        public string Img
        {
            get { return _Img; }
            set { _Img = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }
    }

    public class MemberPurchaseInfo
    {
        private int _Purchase_ID;
        private int _Purchase_MemberID;
        private string _Purchase_Title;
        private string _Purchase_Img;
        private double _Purchase_Amount;
        private string _Purchase_Unit;
        private DateTime _Purchase_Validity;
        private string _Purchase_Intro;
        private int _Purchase_Status;
        private DateTime _Purchase_Addtime;
        private string _Purchase_Site;

        public int Purchase_ID
        {
            get { return _Purchase_ID; }
            set { _Purchase_ID = value; }
        }

        public int Purchase_MemberID
        {
            get { return _Purchase_MemberID; }
            set { _Purchase_MemberID = value; }
        }

        public string Purchase_Title
        {
            get { return _Purchase_Title; }
            set { _Purchase_Title = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Purchase_Img
        {
            get { return _Purchase_Img; }
            set { _Purchase_Img = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }

        public double Purchase_Amount
        {
            get { return _Purchase_Amount; }
            set { _Purchase_Amount = value; }
        }

        public string Purchase_Unit
        {
            get { return _Purchase_Unit; }
            set { _Purchase_Unit = value.Length > 10 ? value.Substring(0, 10) : value.ToString(); }
        }

        public DateTime Purchase_Validity
        {
            get { return _Purchase_Validity; }
            set { _Purchase_Validity = value; }
        }

        public string Purchase_Intro
        {
            get { return _Purchase_Intro; }
            set { _Purchase_Intro = value.Length > 1000 ? value.Substring(0, 1000) : value.ToString(); }
        }

        public int Purchase_Status
        {
            get { return _Purchase_Status; }
            set { _Purchase_Status = value; }
        }

        public DateTime Purchase_Addtime
        {
            get { return _Purchase_Addtime; }
            set { _Purchase_Addtime = value; }
        }

        public string Purchase_Site
        {
            get { return _Purchase_Site; }
            set { _Purchase_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }
    }

    public class MemberPurchaseReplyInfo
    {
        private int _Reply_ID;
        private int _Reply_PurchaseID;
        private int _Reply_SupplierID;
        private string _Reply_Title;
        private string _Reply_Content;
        private string _Reply_Contactman;
        private string _Reply_Mobile;
        private string _Reply_Email;
        private DateTime _Reply_Addtime;

        public int Reply_ID
        {
            get { return _Reply_ID; }
            set { _Reply_ID = value; }
        }

        public int Reply_PurchaseID
        {
            get { return _Reply_PurchaseID; }
            set { _Reply_PurchaseID = value; }
        }

        public int Reply_SupplierID
        {
            get { return _Reply_SupplierID; }
            set { _Reply_SupplierID = value; }
        }

        public string Reply_Title
        {
            get { return _Reply_Title; }
            set { _Reply_Title = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Reply_Content
        {
            get { return _Reply_Content; }
            set { _Reply_Content = value.Length > 1000 ? value.Substring(0, 1000) : value.ToString(); }
        }

        public string Reply_Contactman
        {
            get { return _Reply_Contactman; }
            set { _Reply_Contactman = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Reply_Mobile
        {
            get { return _Reply_Mobile; }
            set { _Reply_Mobile = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Reply_Email
        {
            get { return _Reply_Email; }
            set { _Reply_Email = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public DateTime Reply_Addtime
        {
            get { return _Reply_Addtime; }
            set { _Reply_Addtime = value; }
        }
    }


    #region ERP用户绑定
    public class ERPBildingJsonInfo
    {
        public bool result { get; set; }

        public string storeid { get; set; }

        public string msg { get; set; }

        public ERPBildingErrorInfo err { get; set; }
    }

    public class ERPBildingErrorInfo
    {
        public string code { get; set; }

        public string message { get; set; }

        public string name { get; set; }
    }

    #endregion


    #region 对话列表

    /// <summary>
    /// 对话列表状态信息
    /// </summary>
    public class MessageStatusInfo
    {
        /// <summary>
        /// 返回的状态码
        /// </summary>
        public string code { get; set; }
         
        /// <summary>
        /// 返回的状态码信息
        /// </summary>
        public string message { get; set; }
    }

    /// <summary>
    /// 对话列表分页信息
    /// </summary>
    public class MessagePageInfo
    {
        /// <summary>
        /// 当前页数
        /// </summary>
        public int page { get; set; }

        /// <summary>
        /// 总共多少页
        /// </summary>
        public int totalpage { get; set; }

        /// <summary>
        /// 每页显示条数
        /// </summary>
        public int pagenum { get; set; }

        /// <summary>
        /// 总共多少条
        /// </summary>
        public int totalnum { get; set; }
    }

    /// <summary>
    /// 咨询会话信息
    /// </summary>
    public class MessageChatmessageInfo
    {
        /// <summary>
        /// 会话id
        /// </summary>
        public string sessionid { get; set; }

        /// <summary>
        /// 聊天客服id
        /// </summary>
        public string kfid { get; set; }

        /// <summary>
        /// 聊天客户id
        /// </summary>
        public string userid { get; set; }

        /// <summary>
        /// 聊天发起人id
        /// </summary>
        public string startchattingid { get; set; }

        /// <summary>
        /// 聊天结束人id，会话未结束，该项为空
        /// </summary>
        public string endchattingid { get; set; }

        /// <summary>
        /// 客服消息条数
        /// </summary>
        public int kfmsgnum { get; set; }

        /// <summary>
        /// 客人消息条数
        /// </summary>
        public int guestmsgnum { get; set; }

        /// <summary>
        /// 会话开始时间
        /// </summary>
        public string starttime { get; set; }

        /// <summary>
        /// 客人名字
        /// </summary>
        public string guestname { get; set; }

        /// <summary>
        /// 本次会话的消息总数（不包含系统消息）
        /// </summary>
        public int msgnum { get; set; }

        /// <summary>
        /// 会话发起页面
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// 会话持续时间，如果会话当前还未结束，该项为0
        /// </summary>
        public int timeslong { get; set; }

        /// <summary>
        /// 用户所属城市
        /// </summary>
        public string city { get; set; }

        /// <summary>
        /// 用户所属省份
        /// </summary>
        public string province { get; set; }

        public IList<DatacogradientMessageInfo> messages { get; set; }
    } 

    /// <summary>
    /// 留言信息
    /// </summary>
    public class MessageLeavemessageInfo
    {
        /// <summary>
        /// 留言id
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public string userid { get; set; }

        /// <summary>
        /// 客服id
        /// </summary>
        public string kfid { get; set; }

        /// <summary>
        /// 用户电话
        /// </summary>
        public string phone { get; set; }

        /// <summary>
        /// 留言内容
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// 留言时间
        /// </summary>
        public int time { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string username { get; set; }

        /// <summary>
        /// 用户邮箱
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// 留言发起页
        /// </summary>
        public string pageurl { get; set; }

        /// <summary>
        /// 来源
        /// </summary>
        public string source { get; set; }

        /// <summary>
        /// ip
        /// </summary>
        public string ip { get; set; }

        /// <summary>
        /// 用户所属城市
        /// </summary>
        public string city { get; set; }

        /// <summary>
        /// 用户所属省份
        /// </summary>
        public string province { get; set; }

        /// <summary>
        /// 关键词
        /// </summary>
        public string keyword { get; set; }

        /// <summary>
        /// 接待组id
        /// </summary>
        public int settingid { get; set; }

        /// <summary>
        /// 已处理    0=未处理   1=已处理
        /// </summary>
        public int isread { get; set; }

        /// <summary>
        /// 留言处理时间
        /// </summary>
        public int deal_time { get; set; }

        /// <summary>
        /// 处理内容
        /// </summary>
        public string deal { get; set; }

        /// <summary>
        /// 处理该留言客服
        /// </summary>
        public string dealing_kfid { get; set; }

        /// <summary>
        /// 留言处理方式
        /// </summary>
        public string isreadtype { get; set; }

    }

    /// <summary>
    /// 聊窗信息
    /// </summary>
    public class MessageOpenchatwindowInfo
    {
        /// <summary>
        /// id
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 企业id
        /// </summary>
        public string siteid { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public string userid { get; set; }

        /// <summary>
        /// 客服id
        /// </summary>
        public string kfid { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public int time { get; set; }

        /// <summary>
        /// 发起页
        /// </summary>
        public string pageurl { get; set; }

        /// <summary>
        /// 来源
        /// </summary>
        public string source { get; set; }

        /// <summary>
        /// ip
        /// </summary>
        public string ip { get; set; }

        /// <summary>
        /// 用户所属城市
        /// </summary>
        public string city { get; set; }

        /// <summary>
        /// 用户所属省份
        /// </summary>
        public string province { get; set; }

        /// <summary>
        /// 关键词
        /// </summary>
        public string keyword { get; set; }

        /// <summary>
        /// 接待组id
        /// </summary>
        public int settingid { get; set; }

        /// <summary>
        /// 1：打开聊窗进入咨询；2：打开聊窗进入留言
        /// </summary>
        public int chatwindowtype { get; set; }

    }

    /// <summary>
    /// 咨询返回信息
    /// </summary>
    public class MessageChatmessageJsonInfo
    {
        public MessageStatusInfo status { get; set; }

        public MessagePageInfo pages { get; set; }

        public IList<MessageChatmessageInfo> datalist { get; set; }
    }

    /// <summary>
    /// 留言返回信息
    /// </summary>
    public class MessageLeavemessageJsonInfo
    {
        public MessageStatusInfo status { get; set; }

        public MessagePageInfo pages { get; set; }

        public IList<MessageLeavemessageInfo> datalist { get; set; }
    }

    /// <summary>
    /// 聊窗返回信息
    /// </summary>
    public class MessageOpenchatwindowJsonInfo
    {
        public MessageStatusInfo status { get; set; }

        public MessagePageInfo pages { get; set; }

        public IList<MessageOpenchatwindowInfo> datalist { get; set; }
    }

    #endregion

    #region  消息列表

    public class DatacogradientJsonInfo
    {
        public MessageStatusInfo status { get; set; }

        public MessageChatmessageInfo chatInfo { get; set; } 
    }

    public class DatacogradientMessageInfo
    {
        public string id { get; set; }

        public string sessionid { get; set; }

        public string sourceid { get; set; }

        public string content { get; set; }

        public string time { get; set; }

        public string name { get; set; }

        public string srcname { get; set; }

        public string date { get; set; }

        public int typemark { get; set; }

    }

    #endregion

}
