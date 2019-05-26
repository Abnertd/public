using System;
using System.Text;
using System.Data;
using System.Configuration;
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
using Glaer.Trade.B2C.BLL.Product;

/// <summary>
///Help 的摘要说明
/// </summary>
public class ProductNotify
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private IProductNotify MyBLL;
    private IProduct MyProduct;
    private IMail mail;
    Product ProApp = new Product();

    public ProductNotify()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        MyBLL = ProductNotifyFactory.CreateProductNotify();
        MyProduct = ProductFactory.CreateProduct();
        mail = MailFactory.CreateMail();
    }

    public void ProductNotifySend()
    {
        int Product_Notify_ID = tools.CheckInt(Request.QueryString["Notify_ID"]);
        ProductNotifyInfo entity = MyBLL.GetProductNotifyByID(Product_Notify_ID, Public.GetUserPrivilege());
        if (entity != null)
        { 
            ProductInfo productinfo = MyProduct.GetProductByID(entity.Product_Notify_ProductID, Public.GetUserPrivilege());
                if (productinfo != null)
                {
                    Sendmail(entity.Product_Notify_Email, Application["site_name"].ToString() + "到货通知", productinfo.Product_Name + "已到货！", Application["site_name"].ToString() + "通知您，" + productinfo.Product_Name + "已到货，期待您的选购！");
                    entity.Product_Notify_IsNotify = 1;
                    MyBLL.EditProductNotify(entity, Public.GetUserPrivilege());
                }
        }
        Public.Msg("positive", "操作成功", "操作成功", true, "Product_Notify_list.aspx");
    }

    public void DelProductNotify()
    {
        int Product_Notify_ID = tools.CheckInt(Request.QueryString["Notify_ID"]);
        if (MyBLL.DelProductNotify(Product_Notify_ID,Public.GetUserPrivilege()) > 0)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "Product_Notify_list.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }



    public string GetProductNotifys()
    {
        string product_code, product_name, product_spec, product_maker;

        string keyword = tools.CheckStr(Request["keyword"]);
        int isnotify = tools.CheckInt(Request["isnotify"]);
        string productidstr, memberidstr;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        if (isnotify == 1)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductNotifyInfo.Product_Notify_IsNotify", "=", "1"));
        }
        else if (isnotify == 2)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductNotifyInfo.Product_Notify_IsNotify", "=", "0"));
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductNotifyInfo.Product_Notify_Site", "=", Public.GetCurrentSite()));
        if (keyword.Length > 0)
        {
            
            productidstr = ProApp.GetProductIDByKeyword(keyword);

            
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductNotifyInfo.Product_Notify_Email", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR)", "int", "ProductNotifyInfo.Product_Notify_ProductID", "in", productidstr));
        }
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyBLL.GetPageInfo(Query, Public.GetUserPrivilege());

        IList<ProductNotifyInfo> entitys = MyBLL.GetProductNotifys(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            ProductInfo productinfo;
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (ProductNotifyInfo entity in entitys)
            {
                productinfo = MyProduct.GetProductByID(entity.Product_Notify_ProductID, Public.GetUserPrivilege());
                if (productinfo != null)
                {
                    product_code = productinfo.Product_Code;
                    product_name = productinfo.Product_Name;
                    product_spec = productinfo.Product_Spec;
                    product_maker = productinfo.Product_Maker;
                }
                else
                {
                    product_code = "";
                    product_name = "";
                    product_spec = "";
                    product_maker = "";
                }
                jsonBuilder.Append("{\"ProductNotifyInfo.Product_Notify_ID\":" + entity.Product_Notify_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Product_Notify_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Product_Notify_Email));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(product_code));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(product_name));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(product_spec));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(product_maker));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.Product_Notify_IsNotify ==1)
                {
                    jsonBuilder.Append("已通知");
                }
                else
                {
                    jsonBuilder.Append("未通知");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Product_Notify_Addtime);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (Public.CheckPrivilege("98dd8cbd-8ea7-4a59-89ec-988f149c16bb")&&entity.Product_Notify_IsNotify ==0)
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_mail.gif\\\" alt=\\\"发送\\\"> <a href=\\\"product_notify_do.aspx?action=send_notify&notify_id=" + entity.Product_Notify_ID + "\\\" title=\\\"发送\\\">发送到货通知</a>");
                }
                if (Public.CheckPrivilege("d5183803-ddfa-4a0b-8319-bed75950a08c"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('product_notify_do.aspx?action=move&notify_id=" + entity.Product_Notify_ID + "')\\\" title=\\\"删除\\\">删除</a>");
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
        else
        {
            return null;
        }
    }

    #region "邮件处理"


    //邮件发送处理过程
    public int Sendmail(string mailto, string mailsubject, string mailbodytitle, string mailbody)
    {

        //-------------------------------------定义邮件设置---------------------------------
        int mformat = 0;

        //-------------------------------------定义邮件模版---------------------------------
        string MailBody_Temp = null;
        MailBody_Temp = "";
        MailBody_Temp = MailBody_Temp + "<html><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=GB2312\" /></head>";
        MailBody_Temp = MailBody_Temp + "<body>";
        MailBody_Temp = MailBody_Temp + "<DIV class=mailHeader><SPAN class=MailBody_title>{MailBody_title}</SPAN></DIV>";
        MailBody_Temp = MailBody_Temp + "<DIV class=mailContent>";
        MailBody_Temp = MailBody_Temp + "{MailBody_content}";
        MailBody_Temp = MailBody_Temp + "<p><br><B>{sys_config_site_name}</B><br>欲了解更多信息，请访问<a href='{sys_config_site_url}'>{sys_config_site_url}</a> 或致电{sys_config_site_tel}</P></DIV>";
        MailBody_Temp = MailBody_Temp + "<DIV class=mailFooter><P class=comments>&copy; {sys_config_site_name}</P></DIV>";
        MailBody_Temp = MailBody_Temp + "<style type=text/css>";
        MailBody_Temp = MailBody_Temp + "P {FONT-SIZE: 14px; MARGIN: 10px 0px 5px; LINE-HEIGHT: 130%; FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif}";
        MailBody_Temp = MailBody_Temp + "td {FONT-SIZE: 12px; LINE-HEIGHT: 150%; FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif}";
        MailBody_Temp = MailBody_Temp + "BODY {BORDER-RIGHT: 0px; PADDING-RIGHT: 0px; BORDER-TOP: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 0px; BORDER-LEFT: 0px; PADDING-TOP: 0px; BORDER-BOTTOM: 0px; FONT-FAMILY: Arial, Verdana, Helvetica, sans-serif }";
        MailBody_Temp = MailBody_Temp + "UL {MARGIN-TOP: 0px; FONT-SIZE: 14px; LINE-HEIGHT: 130%; FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif}";
        MailBody_Temp = MailBody_Temp + ".comments {FONT-SIZE: 12px; MARGIN: 0px; COLOR: gray; LINE-HEIGHT: 130%}";
        MailBody_Temp = MailBody_Temp + ".mailHeader {PADDING-RIGHT: 23px; PADDING-LEFT: 23px; PADDING-BOTTOM: 10px; COLOR: #003366; PADDING-TOP: 10px; BORDER-BOTTOM: #7a8995 1px solid; BACKGROUND-COLOR: #ebebeb}";
        MailBody_Temp = MailBody_Temp + ".mailContent {PADDING-RIGHT: 23px; PADDING-LEFT: 23px; PADDING-BOTTOM: 23px; PADDING-TOP: 11px}";
        MailBody_Temp = MailBody_Temp + ".mailFooter {PADDING-RIGHT: 23px; BORDER-TOP: #bbbbbb 1px solid; PADDING-LEFT: 23px; PADDING-BOTTOM: 11px; PADDING-TOP: 11px}";
        MailBody_Temp = MailBody_Temp + ".MailBody_title {  font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 20px; font-weight: bold; color: #009900}";
        MailBody_Temp = MailBody_Temp + "A:visited { COLOR: #105bac} A:hover { COLOR: orange} .img_border { border: 1px solid #E5E5E5}";
        MailBody_Temp = MailBody_Temp + ".highLight { BACKGROUND-COLOR: #FFFFCC; PADDING: 15px; FONT-FAMILY: Arial, Verdana, Helvetica, sans-serif}</style>";
        MailBody_Temp = MailBody_Temp + "</body><html>";

        //------------------------------------开始发送过程------------------------------------
        string body = "";
        switch (mformat)
        {
            case 0:
                //HTML格式
                body = "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=GB2312\" />" + MailBody_Temp;
                body = body.Replace("{MailBody_title}", mailbodytitle);
                body = body.Replace("{MailBody_content}", mailbody);
                break;
            case 1:
                //纯文本格式
                body = mailbody;
                break;
        }

        body = replace_sys_config(body);

        // ERROR: Not supported in C#: OnErrorStatement
        try
        {
            mail.From = Application["Mail_From"].ToString();
            mail.Replyto = Application["Mail_Replyto"].ToString();
            mail.FromName = Application["Mail_FromName"].ToString();
            mail.Server = Application["Mail_Server"].ToString();
            //邮件格式 0=支持HTML,1=纯文本
            mail.ServerUsername = Application["Mail_ServerUserName"].ToString(); ;
            mail.ServerPassword = Application["Mail_ServerPassWord"].ToString();
            mail.ServerPort = tools.CheckInt(Application["Mail_ServerPort"].ToString());
            if (tools.CheckInt(Application["Mail_EnableSsl"].ToString()) == 0)
            {
                mail.EnableSsl = false;
            }
            else
            {
                mail.EnableSsl = true;
            }
            mail.Encode = Application["Mail_Encode"].ToString();

            if (mail.SendEmail(mailto, mailsubject, body))
            {
                return 1;
            }
            else
            {
                return 0;
            }

        }
        catch (Exception ex)
        {
            return 0;
        }



    }

    //替换系统变量
    public string replace_sys_config(string replacestr)
    {
        string functionReturnValue;
        functionReturnValue = replacestr;
        functionReturnValue = functionReturnValue.Replace("{sys_config_site_name}", Application["site_name"].ToString());
        functionReturnValue = functionReturnValue.Replace("{sys_config_site_url}", Application["site_url"].ToString());
        functionReturnValue = functionReturnValue.Replace("{sys_config_site_tel}", Application["site_tel"].ToString());
        return functionReturnValue;
    }

    #endregion

    
}

