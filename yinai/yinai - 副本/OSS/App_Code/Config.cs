using System;
using System.Text;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.Util.Mail;
using Glaer.Trade.B2C.BLL.Sys;

/// <summary>
///Addr 的摘要说明
/// </summary>
public class Config
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private IConfig MyBLL;

    public Config()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        MyBLL = ConfigFactory.CreateConfig();
    }

    public void AddConfig()
    {
        int Sys_Config_ID = tools.CheckInt(Request.Form["Sys_Config_ID"]);
        string Upload_Server_URL = tools.CheckStr(Request.Form["Upload_Server_URL"]);
        string Upload_Server_Return_WWW = tools.CheckStr(Request.Form["Upload_Server_Return_WWW"]);
        string Upload_Server_Return_Admin = tools.CheckStr(Request.Form["Upload_Server_Return_Admin"]);
        string Upload_Server_Return_Supplier = tools.CheckStr(Request.Form["Upload_Server_Return_Supplier"]);
        string Language_Define = tools.CheckStr(Request.Form["Language_Define"]);
        string Site_DomainName = tools.CheckStr(Request.Form["Site_DomainName"]);
        string Site_Name = tools.CheckStr(Request.Form["Site_Name"]);
        string Site_URL = tools.CheckStr(Request.Form["Site_URL"]);
        string Site_Logo = tools.CheckStr(Request.Form["Site_Logo"]);
        string Site_Tel = tools.CheckStr(Request.Form["Site_Tel"]);
        string Site_Fax = tools.CheckStr(Request.Form["Site_Fax"]);
        string Site_Email = tools.CheckStr(Request.Form["Site_Email"]);
        string Site_Keyword = tools.CheckStr(Request.Form["Site_Keyword"]);
        string Site_Description = tools.CheckStr(Request.Form["Site_Description"]);
        string Site_Title = tools.CheckStr(Request.Form["Site_Title"]);
        string Mail_Server = tools.CheckStr(Request.Form["Mail_Server"]);
        int Mail_ServerPort = tools.CheckInt(Request.Form["Mail_ServerPort"]);
        int Mail_EnableSsl = tools.CheckInt(Request.Form["Mail_EnableSsl"]);
        string Mail_ServerUserName = tools.CheckStr(Request.Form["Mail_ServerUserName"]);
        string Mail_ServerPassWord = tools.CheckStr(Request.Form["Mail_ServerPassWord"]);
        string Mail_FromName = tools.CheckStr(Request.Form["Mail_FromName"]);
        string Mail_From = tools.CheckStr(Request.Form["Mail_From"]);
        string Mail_Replyto = tools.CheckStr(Request.Form["Mail_Replyto"]);
        string Mail_Encode = tools.CheckStr(Request.Form["Mail_Encode"]);
        string Coin_Name = tools.CheckStr(Request.Form["Coin_Name"]);
        int Coin_Rate = tools.CheckInt(Request.Form["Coin_Rate"]);
        int Trade_Contract_IsActive = tools.CheckInt(Request.Form["Trade_Contract_IsActive"]);
        string Sys_Config_Site = tools.CheckStr(Request.Form["Sys_Config_Site"]);
        int Static_IsEnable = tools.CheckInt(Request.Form["Static_IsEnable"]);
        string Chinabank_Code = tools.CheckStr(Request.Form["Chinabank_Code"]);
        string Chinabank_Key = tools.CheckStr(Request.Form["Chinabank_Key"]);
        string Alipay_Email = tools.CheckStr(Request.Form["Alipay_Email"]);
        string Alipay_Code = tools.CheckStr(Request.Form["Alipay_Code"]);
        string Alipay_Key = tools.CheckStr(Request.Form["Alipay_Key"]);
        string Tenpay_Code = tools.CheckStr(Request.Form["Tenpay_Code"]);
        string Tenpay_Key = tools.CheckStr(Request.Form["Tenpay_Key"]);
        string CreditPayment_Code = tools.CheckStr(Request.Form["CreditPayment_Code"]);
        int RepidBuy_IsEnable = tools.CheckInt(Request.Form["RepidBuy_IsEnable"]);
        int Coupon_UsedAmount = tools.CheckInt(Request.Form["Coupon_UsedAmount"]);
        string Sys_Sensitive_Keyword = tools.NullStr(Request.Form["Sys_Sensitive_Keyword"]);
        string Shop_Second_Domain = tools.NullStr(Request.Form["Shop_Second_Domain"]);
        double Keyword_Adv_MinPrice = tools.NullDbl(Request.Form["Keyword_Adv_MinPrice"]);
        double Instant_SilverPrice = tools.NullDbl(Request.Form["Instant_SilverPrice"]);
        double Instant_GoldPrice = tools.NullDbl(Request["Instant_GoldPrice"]);

        ConfigInfo entity = new ConfigInfo();
        entity.Sys_Config_ID = Sys_Config_ID;
        entity.Upload_Server_URL = Upload_Server_URL;
        entity.Upload_Server_Return_WWW = Upload_Server_Return_WWW;
        entity.Upload_Server_Return_Admin = Upload_Server_Return_Admin;
        entity.Upload_Server_Return_Supplier = Upload_Server_Return_Supplier;
        entity.Language_Define = Language_Define;
        entity.Site_DomainName = Site_DomainName;
        entity.Site_Name = Site_Name;
        entity.Site_URL = Site_URL;
        entity.Site_Logo = Site_Logo;
        entity.Site_Tel = Site_Tel;
        entity.Site_Fax = Site_Fax;
        entity.Site_Email = Site_Email;
        entity.Site_Keyword = Site_Keyword;
        entity.Site_Description = Site_Description;
        entity.Site_Title = Site_Title;
        entity.Mail_Server = Mail_Server;
        entity.Mail_ServerPort = Mail_ServerPort;
        entity.Mail_EnableSsl = Mail_EnableSsl;
        entity.Mail_ServerUserName = Mail_ServerUserName;
        entity.Mail_ServerPassWord = Mail_ServerPassWord;
        entity.Mail_FromName = Mail_FromName;
        entity.Mail_From = Mail_From;
        entity.Mail_Replyto = Mail_Replyto;
        entity.Mail_Encode = Mail_Encode;
        entity.Coin_Name = Coin_Name;
        entity.Coin_Rate = Coin_Rate;
        entity.Trade_Contract_IsActive = Trade_Contract_IsActive;
        entity.Sys_Config_Site = Sys_Config_Site;
        entity.Static_IsEnable = Static_IsEnable;
        entity.Chinabank_Code = Chinabank_Code;
        entity.Chinabank_Key = Chinabank_Key;
        entity.Alipay_Email = Alipay_Email;
        entity.Alipay_Code = Alipay_Code;
        entity.Alipay_Key = Alipay_Key;
        entity.Tenpay_Code = Tenpay_Code;
        entity.Tenpay_Key = Tenpay_Key;
        entity.CreditPayment_Code = CreditPayment_Code;
        entity.RepidBuy_IsEnable = RepidBuy_IsEnable;
        entity.Coupon_UsedAmount = Coupon_UsedAmount;
        entity.Sys_Sensitive_Keyword = Sys_Sensitive_Keyword;
        entity.Shop_Second_Domain = Shop_Second_Domain;
        entity.Keyword_Adv_MinPrice = Keyword_Adv_MinPrice;
        entity.Instant_GoldPrice = Instant_GoldPrice;
        entity.Instant_SilverPrice = Instant_SilverPrice;

        try
        {
            if (MyBLL.AddConfig(entity, Public.GetUserPrivilege()))
            {
                //更新系统缓存
                Sys_UpdateApplication();

                Public.Msg("positive", "操作成功", "操作成功", true, "system.aspx");
            }
            else
            {
                Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void EditConfig()
    {
        int Sys_Config_ID = tools.CheckInt(Request.Form["Sys_Config_ID"]);
        string Upload_Server_URL = tools.CheckStr(Request.Form["Upload_Server_URL"]);
        string Upload_Server_Return_WWW = tools.CheckStr(Request.Form["Upload_Server_Return_WWW"]);
        string Upload_Server_Return_Admin = tools.CheckStr(Request.Form["Upload_Server_Return_Admin"]);
        string Upload_Server_Return_Supplier = tools.CheckStr(Request.Form["Upload_Server_Return_Supplier"]);
        string Language_Define = tools.CheckStr(Request.Form["Language_Define"]);
        string Site_DomainName = tools.CheckStr(Request.Form["Site_DomainName"]);
        string Site_Name = tools.CheckStr(Request.Form["Site_Name"]);
        string Site_URL = tools.CheckStr(Request.Form["Site_URL"]);
        string Site_Logo = tools.CheckStr(Request.Form["Site_Logo"]);
        string Site_Tel = tools.CheckStr(Request.Form["Site_Tel"]);
        string Site_Fax = tools.CheckStr(Request.Form["Site_Fax"]);
        string Site_Email = tools.CheckStr(Request.Form["Site_Email"]);
        string Site_Keyword = tools.CheckStr(Request.Form["Site_Keyword"]);
        string Site_Description = tools.CheckStr(Request.Form["Site_Description"]);
        string Site_Title = tools.CheckStr(Request.Form["Site_Title"]);
        string Mail_Server = tools.CheckStr(Request.Form["Mail_Server"]);
        int Mail_ServerPort = tools.CheckInt(Request.Form["Mail_ServerPort"]);
        int Mail_EnableSsl = tools.CheckInt(Request.Form["Mail_EnableSsl"]);
        string Mail_ServerUserName = tools.CheckStr(Request.Form["Mail_ServerUserName"]);
        string Mail_ServerPassWord = tools.CheckStr(Request.Form["Mail_ServerPassWord"]);
        string Mail_FromName = tools.CheckStr(Request.Form["Mail_FromName"]);
        string Mail_From = tools.CheckStr(Request.Form["Mail_From"]);
        string Mail_Replyto = tools.CheckStr(Request.Form["Mail_Replyto"]);
        string Mail_Encode = tools.CheckStr(Request.Form["Mail_Encode"]);
        string Coin_Name = tools.CheckStr(Request.Form["Coin_Name"]);
        int Coin_Rate = tools.CheckInt(Request.Form["Coin_Rate"]);
        int Trade_Contract_IsActive = tools.CheckInt(Request.Form["Trade_Contract_IsActive"]);
        string Sys_Config_Site = tools.CheckStr(Request.Form["Sys_Config_Site"]);
        int Static_IsEnable = tools.CheckInt(Request.Form["Static_IsEnable"]);
        string Chinabank_Code = tools.CheckStr(Request.Form["Chinabank_Code"]);
        string Chinabank_Key = tools.CheckStr(Request.Form["Chinabank_Key"]);
        string Alipay_Email = tools.CheckStr(Request.Form["Alipay_Email"]);
        string Alipay_Code = tools.CheckStr(Request.Form["Alipay_Code"]);
        string Alipay_Key = tools.CheckStr(Request.Form["Alipay_Key"]);
        string Tenpay_Code = tools.CheckStr(Request.Form["Tenpay_Code"]);
        string Tenpay_Key = tools.CheckStr(Request.Form["Tenpay_Key"]);
        string CreditPayment_Code = tools.CheckStr(Request.Form["CreditPayment_Code"]);
        int RepidBuy_IsEnable = tools.CheckInt(Request.Form["RepidBuy_IsEnable"]);
        int Coupon_UsedAmount = tools.CheckInt(Request.Form["Coupon_UsedAmount"]);
        string Sys_Sensitive_Keyword = tools.NullStr(Request.Form["Sys_Sensitive_Keyword"]);
        string Shop_Second_Domain = tools.NullStr(Request.Form["Shop_Second_Domain"]);
        double Keyword_Adv_MinPrice = tools.NullDbl(Request.Form["Keyword_Adv_MinPrice"]);
        double Instant_SilverPrice = tools.NullDbl(Request.Form["Instant_SilverPrice"]);
        double Instant_GoldPrice = tools.NullDbl(Request["Instant_GoldPrice"]);


        ConfigInfo entity = new ConfigInfo();
        entity.Sys_Config_ID = Sys_Config_ID;
        entity.Upload_Server_URL = Upload_Server_URL;
        entity.Upload_Server_Return_WWW = Upload_Server_Return_WWW;
        entity.Upload_Server_Return_Admin = Upload_Server_Return_Admin;
        entity.Upload_Server_Return_Supplier = Upload_Server_Return_Supplier;
        entity.Language_Define = Language_Define;
        entity.Site_DomainName = Site_DomainName;
        entity.Site_Name = Site_Name;
        entity.Site_URL = Site_URL;
        entity.Site_Logo = Site_Logo;
        entity.Site_Tel = Site_Tel;
        entity.Site_Fax = Site_Fax;
        entity.Site_Email = Site_Email;
        entity.Site_Keyword = Site_Keyword;
        entity.Site_Description = Site_Description;
        entity.Site_Title = Site_Title;
        entity.Mail_Server = Mail_Server;
        entity.Mail_ServerPort = Mail_ServerPort;
        entity.Mail_EnableSsl = Mail_EnableSsl;
        entity.Mail_ServerUserName = Mail_ServerUserName;
        entity.Mail_ServerPassWord = Mail_ServerPassWord;
        entity.Mail_FromName = Mail_FromName;
        entity.Mail_From = Mail_From;
        entity.Mail_Replyto = Mail_Replyto;
        entity.Mail_Encode = Mail_Encode;
        entity.Coin_Name = Coin_Name;
        entity.Coin_Rate = Coin_Rate;
        entity.Trade_Contract_IsActive = Trade_Contract_IsActive;
        entity.Sys_Config_Site = Sys_Config_Site;
        entity.Static_IsEnable = Static_IsEnable;
        entity.Chinabank_Code = Chinabank_Code;
        entity.Chinabank_Key = Chinabank_Key;
        entity.Alipay_Email = Alipay_Email;
        entity.Alipay_Code = Alipay_Code;
        entity.Alipay_Key = Alipay_Key;
        entity.Tenpay_Code = Tenpay_Code;
        entity.Tenpay_Key = Tenpay_Key;
        entity.CreditPayment_Code = CreditPayment_Code;
        entity.RepidBuy_IsEnable = RepidBuy_IsEnable;
        entity.Coupon_UsedAmount = Coupon_UsedAmount;
        entity.Sys_Sensitive_Keyword = Sys_Sensitive_Keyword;
        entity.Shop_Second_Domain = Shop_Second_Domain;
        entity.Keyword_Adv_MinPrice = Keyword_Adv_MinPrice;
        entity.Instant_GoldPrice = Instant_GoldPrice;
        entity.Instant_SilverPrice = Instant_SilverPrice;

        try
        {
            if (MyBLL.EditConfig(entity, Public.GetUserPrivilege()))
            {
                //更新系统缓存
                Sys_UpdateApplication();

                Public.Msg("positive", "操作成功", "操作成功", true, "system.aspx");
            }
            else
            {
                Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void DelConfig()
    {
        int Site_ID = tools.CheckInt(Request["Site_ID"]);

        if (MyBLL.DelConfig(Site_ID, Public.GetUserPrivilege()) > 0)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "system.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public ConfigInfo GetConfigByID(int id)
    {
        return MyBLL.GetConfigByID(id, Public.GetUserPrivilege());
    }

    public ConfigInfo GetConfigBySite(string Site)
    {
        return MyBLL.GetConfigBySite(Site, Public.GetUserPrivilege());
    }

    public ConfigInfo GetConfigByDomainName(string DomainName)
    {
        return MyBLL.GetConfigByDomainName(DomainName, Public.GetUserPrivilege());
    }

    public void Sys_UpdateApplication()
    {
        ConfigInfo entity = GetConfigByID(1);
        if (entity == null)
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        else
        {
            Application["Sys_Config_ID"] = entity.Sys_Config_ID;
            Application["Upload_Server_URL"] = entity.Upload_Server_URL;
            Application["Upload_Server_Return_WWW"] = entity.Upload_Server_Return_WWW;
            Application["Upload_Server_Return_Admin"] = entity.Upload_Server_Return_Admin;
            Application["Upload_Server_Return_Supplier"] = entity.Upload_Server_Return_Supplier;
            Application["Language_Define"] = entity.Language_Define;
            Application["Site_DomainName"] = entity.Site_DomainName;
            Application["Site_Name"] = entity.Site_Name;
            Application["Site_URL"] = entity.Site_URL;
            Application["Site_Logo"] = entity.Site_Logo;
            Application["Site_Tel"] = entity.Site_Tel;
            Application["Site_Fax"] = entity.Site_Fax;
            Application["Site_Email"] = entity.Site_Email;
            Application["Site_Keyword"] = entity.Site_Keyword;
            Application["Site_Description"] = entity.Site_Description;
            Application["Site_Title"] = entity.Site_Title;
            Application["Mail_Server"] = entity.Mail_Server;
            Application["Mail_ServerPort"] = entity.Mail_ServerPort;
            Application["Mail_EnableSsl"] = entity.Mail_EnableSsl;
            Application["Mail_ServerUserName"] = entity.Mail_ServerUserName;
            Application["Mail_ServerPassWord"] = entity.Mail_ServerPassWord;
            Application["Mail_FromName"] = entity.Mail_FromName;
            Application["Mail_From"] = entity.Mail_From;
            Application["Mail_Replyto"] = entity.Mail_Replyto;
            Application["Mail_Encode"] = entity.Mail_Encode;
            Application["Coin_Name"] = entity.Coin_Name;
            Application["Coin_Rate"] = entity.Coin_Rate;
            Application["Trade_Contract_IsActive"] = entity.Trade_Contract_IsActive;
            Application["Sys_Config_Site"] = entity.Sys_Config_Site;
            Application["Alipay_Email"] = entity.Alipay_Email;
            Application["Alipay_Code"] = entity.Alipay_Code;
            Application["Alipay_Key"] = entity.Alipay_Key;
            Application["Tenpay_Code"] = entity.Tenpay_Code;
            Application["Tenpay_Key"] = entity.Tenpay_Key;
            Application["Sys_Sensitive_Keyword"] = entity.Sys_Sensitive_Keyword;
            Application["Shop_Second_Domain"] = entity.Shop_Second_Domain;
            Application["Keyword_Adv_MinPrice"] = entity.Keyword_Adv_MinPrice;
            Application["Instant_GoldPrice"] = entity.Instant_GoldPrice;
            Application["Instant_SilverPrice"] = entity.Instant_SilverPrice;
        }

    }

    public void Sys_UpdateApplication(string Site)
    {
        ConfigInfo entity = GetConfigBySite(Site);
        if (entity == null)
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        else
        {
            Application["Sys_Config_ID"] = entity.Sys_Config_ID;
            Application["Upload_Server_URL"] = entity.Upload_Server_URL;
            Application["Upload_Server_Return_WWW"] = entity.Upload_Server_Return_WWW;
            Application["Upload_Server_Return_Admin"] = entity.Upload_Server_Return_Admin;
            Application["Upload_Server_Return_Supplier"] = entity.Upload_Server_Return_Supplier;
            Application["Language_Define"] = entity.Language_Define;
            Application["Site_DomainName"] = entity.Site_DomainName;
            Application["Site_Name"] = entity.Site_Name;
            Application["Site_URL"] = entity.Site_URL;
            Application["Site_Logo"] = entity.Site_Logo;
            Application["Site_Tel"] = entity.Site_Tel;
            Application["Site_Fax"] = entity.Site_Fax;
            Application["Site_Email"] = entity.Site_Email;
            Application["Site_Keyword"] = entity.Site_Keyword;
            Application["Site_Description"] = entity.Site_Description;
            Application["Site_Title"] = entity.Site_Title;
            Application["Mail_Server"] = entity.Mail_Server;
            Application["Mail_ServerPort"] = entity.Mail_ServerPort;
            Application["Mail_EnableSsl"] = entity.Mail_EnableSsl;
            Application["Mail_ServerUserName"] = entity.Mail_ServerUserName;
            Application["Mail_ServerPassWord"] = entity.Mail_ServerPassWord;
            Application["Mail_FromName"] = entity.Mail_FromName;
            Application["Mail_From"] = entity.Mail_From;
            Application["Mail_Replyto"] = entity.Mail_Replyto;
            Application["Mail_Encode"] = entity.Mail_Encode;
            Application["Coin_Name"] = entity.Coin_Name;
            Application["Coin_Rate"] = entity.Coin_Rate;
            Application["Trade_Contract_IsActive"] = entity.Trade_Contract_IsActive;
            Application["Sys_Config_Site"] = entity.Sys_Config_Site;
            Application["Alipay_Email"] = entity.Alipay_Email;
            Application["Alipay_Code"] = entity.Alipay_Code;
            Application["Alipay_Key"] = entity.Alipay_Key;
            Application["Tenpay_Code"] = entity.Tenpay_Code;
            Application["Tenpay_Key"] = entity.Tenpay_Key;
            Application["Sys_Sensitive_Keyword"] = entity.Sys_Sensitive_Keyword;
            Application["Shop_Second_Domain"] = entity.Shop_Second_Domain;
            Application["Keyword_Adv_MinPrice"] = entity.Keyword_Adv_MinPrice;
            Application["Instant_GoldPrice"] = entity.Instant_GoldPrice;
            Application["Instant_SilverPrice"] = entity.Instant_SilverPrice;
        }

    }

    public string GetConfigs()
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ConfigInfo.Sys_Config_ID", ">", "0"));
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyBLL.GetPageInfo(Query, Public.GetUserPrivilege());

        IList<ConfigInfo> entitys = MyBLL.GetConfigs(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (ConfigInfo entity in entitys)
            {
                jsonBuilder.Append("{\"ConfigInfo.Sys_Config_ID\":" + entity.Sys_Config_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Sys_Config_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Site_Name));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Site_DomainName));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Site_URL));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Sys_Config_Site));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (Public.CheckPrivilege("ef84a07f-6281-4f54-84f9-c345adf9d765"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\" alt=\\\"修改\\\"> <a href=\\\"system_edit.aspx?site_id=" + entity.Sys_Config_ID + "\\\" title=\\\"修改\\\">修改</a>");
                }

                if (Public.CheckPrivilege("ef84a07f-6281-4f54-84f9-c345adf9d765"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('system_do.aspx?action=move&site_id=" + entity.Sys_Config_ID + "')\\\" title=\\\"删除\\\">删除</a>");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        else { return null; }

    }

    public string optionSite(string currentsite)
    {
        string strHTML = string.Empty;

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        IList<ConfigInfo> entitys = MyBLL.GetConfigs(Query, Public.GetUserPrivilege());

        if (entitys == null)
            return "";

        foreach (ConfigInfo entity in entitys)
        {
            strHTML += "<option value=\"" + entity.Sys_Config_Site + "\" " + (entity.Sys_Config_Site == currentsite ? "selected" : "") + " >" + entity.Site_Name + "</option>";
        }

        return strHTML;
    }

}
