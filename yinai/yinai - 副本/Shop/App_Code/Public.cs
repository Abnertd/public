using System;
using System.Text;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.Util.Mail;
using Glaer.Trade.B2C.BLL.MEM;
using Glaer.Trade.B2C.BLL.Product;
using Glaer.Trade.B2C.BLL.SAL;
using Glaer.Trade.Util.Http;
using System.Net;


/// <summary>
///Product 的摘要说明
/// </summary>
public class Public_Class
{
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private IMail mail;
    private IMember MyMem;
    private IProductPrice Myprice;
    private IMemberGrade Mygrade;
    private IPromotionLimit MyLimit;
    private IHttpHelper httpHelper;
    private IJsonHelper jsonHelper;

    private string GoldAPI;

    public Public_Class()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;
        MyLimit = PromotionLimitFactory.CreatePromotionLimit();
        tools = ToolsFactory.CreateTools();

        MyMem = MemberFactory.CreateMember();
        Myprice = ProductPriceFactory.CreateProductPrice();
        Mygrade = MemberGradeFactory.CreateMemberGrade();
        mail = MailFactory.CreateMail();
        httpHelper = HttpHelperFactory.CreateHttpHelper();
        jsonHelper = JsonHelperFactory.CreateJsonHelper();

        GoldAPI = ConfigurationManager.AppSettings["GoldAPI"];
    }

    /// <summary>
    /// 信息提示窗口
    /// </summary>
    /// <param name="msgtype">信息类型</param>
    /// <param name="msgtitle">信息头</param>
    /// <param name="msgcontent">信息内容</param>
    /// <param name="autoredirect">自动转向</param>
    /// <param name="autoredirecttime">停留时间</param>
    /// <param name="redirecturl">转向URL</param>
    public void Msg(string msgtype, string msgtitle, string msgcontent, bool autoredirect, int autoredirecttime, string redirecturl)
    {
        string msgtype_img = "";
        switch (msgtype)
        {
            case "error":
                msgtype_img = "<img src=\"/images/msg-error.gif\">";
                break;
            case "info":
                msgtype_img = "<img src=\"/images/msg-info.gif\">";
                break;
            case "positive":
                msgtype_img = "<img src=\"/images/msg-positive.gif\">";
                break;
        }

        string Mhtml;
        Mhtml = "<head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />";
        Mhtml += "<link rel=\"stylesheet\" href=\"/css/msg.css\" type=\"text/css\">";
        Mhtml += "<title>" + msgtitle + "</title>";
        if (autoredirect)
        {
            Mhtml += "<meta http-equiv=\"refresh\" content=\"" + autoredirecttime + ";URL=" + redirecturl + "\">";
        }

        Mhtml += "</head><body>";
        Mhtml += "<table width=\"100%\" height=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
        Mhtml += "  <tr>";
        Mhtml += "    <td align=\"center\" valign=\"middle\"><table width=\"350\" border=\"0\" cellspacing=\"0\" cellpadding=\"5\" bgcolor=\"#FFFFFF\">";
        Mhtml += "      <tr><td height=\"30\" class=\"msg_title\">" + msgtitle + "</td></tr>";
        Mhtml += "      <tr>";
        Mhtml += "        <td align=\"left\" class=\"msg_content_border\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
        Mhtml += "          <tr><td height=\"20\" align=\"right\" valign=\"middle\" colspan=\"2\"></td></tr>";
        Mhtml += "          <tr>";
        Mhtml += "            <td width=\"60\" align=\"center\" valign=\"middle\">" + msgtype_img + "</td>";
        Mhtml += "            <td align=\"left\" valign=\"middle\" class=\"msg_content\">" + msgcontent + "</td>";
        Mhtml += "          </tr>";

        if (redirecturl == "{back}")
        {
            Mhtml += "          <tr>";
            Mhtml += "            <td height=\"30\" align=\"right\" valign=\"middle\" colspan=\"2\"><input type=\"button\" name=\"ok\" id=\"btn_ok\" value=\"确定\" class=\"msg_btn\" onclick=\"javascript:history.go(-1);\"></td>";
            Mhtml += "          </tr>";
        }
        else
        {
            Mhtml += "          <tr>";
            Mhtml += "            <td height=\"30\" align=\"right\" valign=\"middle\" colspan=\"2\"><input type=\"button\" name=\"ok\" id=\"btn_ok\" value=\"确定\" class=\"msg_btn\" onclick=\"location.href='" + redirecturl + "';\"></td>";
            Mhtml += "          </tr>";
        }
        Mhtml += "<script type=\"text/javascript\"> ";
        Mhtml += "	function document.onkeydown() { if(event.keyCode==13){document.getElementById(\"btn_ok\").click(); return false;} } ";
        Mhtml += "</script>";
        Mhtml += "        </table></td>";
        Mhtml += "        </tr>";
        Mhtml += "    </table></td>";
        Mhtml += "  </tr>";
        Mhtml += "</table>";
        Mhtml += "</body>";
        Mhtml += "</html>";

        System.Web.HttpContext.Current.Response.Write(Mhtml);
        System.Web.HttpContext.Current.Response.End();
    }

    /// <summary>
    /// 信息提示窗口
    /// </summary>
    /// <param name="msgtype">信息类型</param>
    /// <param name="msgtitle">信息头</param>
    /// <param name="msgcontent">信息内容</param>
    /// <param name="autoredirect">自动转向</param>
    /// <param name="redirecturl">转向URL</param>
    public void Msg(string msgtype, string msgtitle, string msgcontent, bool autoredirect, string redirecturl)
    {
        Msg(msgtype, msgtitle, msgcontent, autoredirect, 3, redirecturl);
    }

    public int DateDiffYear(DateTime DateTime1, DateTime DateTime2)
    {
        int Year = 1;
        int i = 0;
        try
        {
            i = DateTime2.Year - DateTime1.Year;
            if (i > 0)
            {
                Year = i;
            }
            if (Year == 0)
            {
                Year = 1;
            }
        }
        catch
        {

        }
        return Year;
    }

    /// <summary>
    /// Ajax提示
    /// </summary>
    /// <param name="msgtype"></param>
    /// <param name="msg"></param>

    public void Tip(string msgtype, string msg)
    {
        string msgtype_img = null;
        string table_class = null;
        msgtype_img = "";
        table_class = "";
        switch (msgtype)
        {
            case "error":
                msgtype_img = "<img src=\"/images/tip-error.gif\" hspace=\"5\" align=\"absmiddle\">";
                table_class = "tip_bg_error";
                break;
            case "info":
                msgtype_img = "<img src=\"/images/tip-info.gif\" hspace=\"5\" align=\"absmiddle\">";
                table_class = "tip_bg_info";
                break;
            case "positive":
                msgtype_img = "<img src=\"/images/tip-positive.gif\" hspace=\"5\" align=\"absmiddle\">";
                table_class = "tip_bg_positive";
                break;
            case "right":
                msgtype_img = "<img src=\"/images/icon_success.gif\" hspace=\"5\" align=\"absmiddle\">";
                table_class = "tip_bg_positive";
                break;
        }

        string HtmlStr = null;
        HtmlStr = "<table border=\"0\" cellspacing=\"2\" cellpadding=\"0\"><tr><td class=\"" + table_class + "\">";
        HtmlStr += msgtype_img + msg;
        HtmlStr += "</td></tr></table>";
        Response.Write(HtmlStr);
    }


    /// <summary>
    /// SEO标题
    /// </summary>
    /// <returns></returns>
    public string SEO_TITLE()
    {
        return Application["Site_Name"] + " - " + Application["Site_Title"];
    }

    /// <summary>
    /// 格式化货币
    /// </summary>
    /// <param name="price"></param>
    /// <returns></returns>
    public string FormatCurrency(double InVal)
    {
        if (InVal == null)
            return "NULL";
        try { return "￥" + InVal.ToString("0.00"); }
        catch { return "￥0.00"; }
    }

    /// <summary>
    /// 格式化商品名称：商品名+副标题
    /// </summary>
    /// <param name="Product_name"></param>
    /// <param name="Product_subname"></param>
    /// <returns></returns>
    public string Format_Product_Name(string Product_name, string Product_subname)
    {
        if (Product_subname != "")
        {
            return Product_name + "<span class=\"t12_red\">" + Product_subname + "</span>";
        }
        else
        {
            return Product_name;
        }
    }

    /// <summary>
    /// 分页（div）
    /// </summary>
    /// <param name="pagecount">总页数</param>
    /// <param name="currentpage">当前页</param>
    /// <param name="pageurl">分页url</param>
    /// <param name="pagesize">每页显示条数</param>
    /// <param name="recordcount">总条数</param>
    //public void Page(int pagecount, int currentpage, string pageurl, int pagesize, int recordcount)
    //{
    //    int ipage = 0;

    //    StringBuilder strHTML = new StringBuilder();

    //    strHTML.Append("<div class=\"page03\">");
    //    if (currentpage <= 1)
    //    {
    //        strHTML.Append("<a>< 上一页</a>");
    //    }
    //    else
    //    {
    //        strHTML.Append("<a href=\"" + pageurl + "&page=" + (currentpage - 1).ToString() + "\" >< 上一页</a>");
    //    }

    //    if (pagecount <= 6)
    //    {
    //        for (ipage = 1; ipage <= pagecount; ipage++)
    //        {
    //            if (currentpage == ipage)
    //            {
    //                strHTML.Append("<a href=\"" + pageurl + "&page=" + ipage + "\">" + ipage + "</a>");
    //            }
    //            else
    //            {
    //                strHTML.Append("<a href=\"" + pageurl + "&page=" + ipage + "\">" + ipage + "</a>");
    //            }
    //        }
    //    }
    //    else if (pagecount > 6)
    //    {
    //        if (currentpage < 4)
    //        {
    //            for (ipage = 1; ipage <= 6; ipage++)
    //            {
    //                strHTML.Append("<a href=\"" + pageurl + "&page=" + ipage + "\">" + ipage + "</a>");
    //            }
    //        }
    //        else
    //        {
    //            for (ipage = currentpage - 3; ipage <= currentpage + 2; ipage++)
    //            {
    //                strHTML.Append("<a href=\"" + pageurl + "&page=" + ipage + "\">" + ipage + "</a>");
    //            }
    //        }
    //        strHTML.Append("...");
    //    }

    //    if (currentpage == pagecount)
    //    {
    //        strHTML.Append("<a>下一页 ></a> ");
    //    }
    //    else
    //    {
    //        strHTML.Append("<a href=\"" + pageurl + "&page=" + (currentpage + 1).ToString() + "\" >下一页 ></a> ");
    //    }
    //    strHTML.Append("&nbsp;&nbsp;&nbsp;&nbsp;共<strong>" + pagecount + "</strong>页");
    //    strHTML.Append("&nbsp;&nbsp;&nbsp;&nbsp; 到第<input name=\"\" type=\"text\" value=\"" + currentpage + "\" />页 <a href=\"javascript:;\" onclick=\"$('#frm_page').submit();\" class=\"a18\">跳 转</a></div>");

    //    Response.Write(strHTML.ToString());
    //}

    //public void Page(int pagecount, int currentpage, string pageurl, int pagesize, int recordcount)
    //{
    //    int ipage = 0;

    //    StringBuilder strHTML = new StringBuilder();

    //    strHTML.Append("<div class=\"page03\">");
    //    if (currentpage <= 1)
    //    {
    //        strHTML.Append("<a>< 上一页</a>");
    //    }
    //    else
    //    {
    //        strHTML.Append("<a href=\"" + pageurl + "&page=" + (currentpage - 1).ToString() + "\" >< 上一页</a>");
    //    }

    //    if (pagecount <= 12)
    //    {
    //        for (ipage = 1; ipage <= pagecount; ipage++)
    //        {
    //            if (currentpage == ipage)
    //            {
    //                strHTML.Append("<a href=\"javascript:;\" class=\"on\">" + ipage + "</a>");
    //            }
    //            else
    //            {
    //                strHTML.Append("<a href=\"" + pageurl + "&page=" + ipage + "\">" + ipage + "</a>");
    //            }
    //        }
    //    }
    //    else if (pagecount > 7 & pagecount < 16)
    //    {
    //        if (currentpage < 9)
    //        {
    //            for (ipage = 1; ipage <= 10; ipage++)
    //            {
    //                if (currentpage == ipage)
    //                {
    //                    strHTML.Append("<a href=\"javascript:;\" class=\"on\">" + ipage + "</a>");
    //                }
    //                else
    //                {
    //                    strHTML.Append("<a href=\"" + pageurl + "&page=" + ipage + "\">" + ipage + "</a>");
    //                }
    //            }
    //            strHTML.Append("...");
    //            for (ipage = pagecount - 1; ipage <= pagecount; ipage++)
    //            {
    //                if (currentpage == ipage)
    //                {
    //                    strHTML.Append("<a href=\"javascript:;\" class=\"on\">" + ipage + "</a>");
    //                }
    //                else
    //                {
    //                    strHTML.Append("<a href=\"" + pageurl + "&page=" + ipage + "\">" + ipage + "</a>");
    //                }
    //            }
    //        }
    //        else
    //        {
    //            for (ipage = 1; ipage <= 2; ipage++)
    //            {
    //                if (currentpage == ipage)
    //                {
    //                    strHTML.Append("<a href=\"javascript:;\" class=\"on\">" + ipage + "</a>");
    //                }
    //                else
    //                {
    //                    strHTML.Append("<a href=\"" + pageurl + "&page=" + ipage + "\">" + ipage + "</a>");
    //                }
    //            }
    //            strHTML.Append("...");
    //            for (ipage = pagecount - 9; ipage <= pagecount; ipage++)
    //            {
    //                if (currentpage == ipage)
    //                {
    //                    strHTML.Append("<a href=\"javascript:;\" class=\"on\">" + ipage + "</a>");
    //                }
    //                else
    //                {
    //                    strHTML.Append("<a href=\"" + pageurl + "&page=" + ipage + "\">" + ipage + "</a>");
    //                }
    //            }
    //        }
    //    }
    //    else if (pagecount >= 16)
    //    {
    //        if (currentpage < 9)
    //        {
    //            for (ipage = 1; ipage <= 10; ipage++)
    //            {
    //                if (currentpage == ipage)
    //                {
    //                    strHTML.Append("<a href=\"javascript:;\" class=\"on\">" + ipage + "</a>");
    //                }
    //                else
    //                {
    //                    strHTML.Append("<a href=\"" + pageurl + "&page=" + ipage + "\">" + ipage + "</a>");
    //                }
    //            }
    //            strHTML.Append("...");
    //            for (ipage = pagecount - 1; ipage <= pagecount; ipage++)
    //            {
    //                if (currentpage == ipage)
    //                {
    //                    strHTML.Append("<a href=\"javascript:;\" class=\"on\">" + ipage + "</a>");
    //                }
    //                else
    //                {
    //                    strHTML.Append("<a href=\"" + pageurl + "&page=" + ipage + "\">" + ipage + "</a>");
    //                }
    //            }
    //        }
    //        else if (currentpage + 7 > pagecount)
    //        {
    //            for (ipage = 1; ipage <= 2; ipage++)
    //            {
    //                if (currentpage == ipage)
    //                {
    //                    strHTML.Append("<a href=\"javascript:;\" class=\"on\">" + ipage + "</a>");
    //                }
    //                else
    //                {
    //                    strHTML.Append("<a href=\"" + pageurl + "&page=" + ipage + "\">" + ipage + "</a>");
    //                }
    //            }
    //            strHTML.Append("...");
    //            for (ipage = pagecount - 9; ipage <= pagecount; ipage++)
    //            {
    //                if (currentpage == ipage)
    //                {
    //                    strHTML.Append("<a href=\"javascript:;\" class=\"on\">" + ipage + "</a>");
    //                }
    //                else
    //                {
    //                    strHTML.Append("<a href=\"" + pageurl + "&page=" + ipage + "\">" + ipage + "</a>");
    //                }
    //            }
    //        }
    //        else
    //        {
    //            for (ipage = 1; ipage <= 2; ipage++)
    //            {
    //                if (currentpage == ipage)
    //                {
    //                    strHTML.Append("<a href=\"javascript:;\" class=\"on\">" + ipage + "</a>");
    //                }
    //                else
    //                {
    //                    strHTML.Append("<a href=\"" + pageurl + "&page=" + ipage + "\">" + ipage + "</a>");
    //                }
    //            }
    //            strHTML.Append("...");
    //            for (ipage = currentpage - 5; ipage <= currentpage + 4; ipage++)
    //            {
    //                if (currentpage == ipage)
    //                {
    //                    strHTML.Append("<a href=\"javascript:;\" class=\"on\">" + ipage + "</a>");
    //                }
    //                else
    //                {
    //                    strHTML.Append("<a href=\"" + pageurl + "&page=" + ipage + "\">" + ipage + "</a>");
    //                }
    //            }
    //            strHTML.Append("...");
    //            for (ipage = pagecount - 1; ipage <= pagecount; ipage++)
    //            {
    //                if (currentpage == ipage)
    //                {
    //                    strHTML.Append("<a href=\"javascript:;\" class=\"on\">" + ipage + "</a>");
    //                }
    //                else
    //                {
    //                    strHTML.Append("<a href=\"" + pageurl + "&page=" + ipage + "\">" + ipage + "</a>");
    //                }
    //            }
    //        }
    //    }

    //    if (currentpage == pagecount)
    //    {
    //        strHTML.Append("<a>下一页 ></a> ");
    //    }
    //    else
    //    {
    //        strHTML.Append("<a href=\"" + pageurl + "&page=" + (currentpage + 1).ToString() + "\" >下一页 ></a> ");
    //    }
    //    strHTML.Append("&nbsp;&nbsp;&nbsp;&nbsp;共<strong>" + pagecount + "</strong>页");
    //    strHTML.Append("<form action=\"" + pageurl + "\" method=\"post\" id=\"frm_page\" style=\"display:inline-block;\">");
    //    strHTML.Append("&nbsp;&nbsp;&nbsp;&nbsp; 到第<input name=\"page\" type=\"text\" value=\"" + currentpage + "\" />页 <a href=\"javascript:;\" onclick=\"$('#frm_page').submit();\" class=\"a18\">跳 转</a>");
    //    strHTML.Append("</form>");
    //    strHTML.Append("</div>");

    //    Response.Write(strHTML.ToString());
    //}
    /// <summary>
    /// 分页（div）
    /// </summary>
    /// <param name="pagecount">总页数</param>
    /// <param name="currentpage">当前页</param>
    /// <param name="pageurl">分页url</param>
    /// <param name="pagesize">每页显示条数</param>
    /// <param name="recordcount">总条数</param>
    public void Page(int pagecount, int currentpage, string pageurl, int pagesize, int recordcount)
    {
        int ipage = 0;

        StringBuilder strHTML = new StringBuilder();

        strHTML.Append("<div class=\"page_yn\">");

        if (currentpage <= 1)
        {
            strHTML.Append("<a><</a>");
        }
        else
        {
            strHTML.Append("<a href=\"" + pageurl + "&page=" + (currentpage - 1).ToString() + "\" ><</a>");
        }

        if (pagecount <= 12)
        {
            for (ipage = 1; ipage <= pagecount; ipage++)
            {
                if (currentpage == ipage)
                {
                    strHTML.Append("<a href=\"javascript:;\" class=\"on\">" + ipage + "</a>");
                }
                else
                {
                    strHTML.Append("<a href=\"" + pageurl + "&page=" + ipage + "\">" + ipage + "</a>");
                }
            }
        }
        else if (pagecount > 7 & pagecount < 16)
        {
            if (currentpage < 9)
            {
                for (ipage = 1; ipage <= 10; ipage++)
                {
                    if (currentpage == ipage)
                    {
                        strHTML.Append("<a href=\"javascript:;\" class=\"on\">" + ipage + "</a>");
                    }
                    else
                    {
                        strHTML.Append("<a href=\"" + pageurl + "&page=" + ipage + "\">" + ipage + "</a>");
                    }
                }
                strHTML.Append("<span>...</span>");
                for (ipage = pagecount - 1; ipage <= pagecount; ipage++)
                {
                    if (currentpage == ipage)
                    {
                        strHTML.Append("<a href=\"javascript:;\" class=\"on\">" + ipage + "</a>");
                    }
                    else
                    {
                        strHTML.Append("<a href=\"" + pageurl + "&page=" + ipage + "\">" + ipage + "</a>");
                    }
                }
            }
            else
            {
                for (ipage = 1; ipage <= 2; ipage++)
                {
                    if (currentpage == ipage)
                    {
                        strHTML.Append("<a href=\"javascript:;\" class=\"on\">" + ipage + "</a>");
                    }
                    else
                    {
                        strHTML.Append("<a href=\"" + pageurl + "&page=" + ipage + "\">" + ipage + "</a>");
                    }
                }
                strHTML.Append("<span>...</span>");
                for (ipage = pagecount - 9; ipage <= pagecount; ipage++)
                {
                    if (currentpage == ipage)
                    {
                        strHTML.Append("<a href=\"javascript:;\" class=\"on\">" + ipage + "</a>");
                    }
                    else
                    {
                        strHTML.Append("<a href=\"" + pageurl + "&page=" + ipage + "\">" + ipage + "</a>");
                    }
                }
            }
        }
        else if (pagecount >= 16)
        {
            if (currentpage < 9)
            {
                for (ipage = 1; ipage <= 10; ipage++)
                {
                    if (currentpage == ipage)
                    {
                        strHTML.Append("<a href=\"javascript:;\" class=\"on\">" + ipage + "</a>");
                    }
                    else
                    {
                        strHTML.Append("<a href=\"" + pageurl + "&page=" + ipage + "\">" + ipage + "</a>");
                    }
                }
                strHTML.Append("<span>...</span>");
                for (ipage = pagecount - 1; ipage <= pagecount; ipage++)
                {
                    if (currentpage == ipage)
                    {
                        strHTML.Append("<a href=\"javascript:;\" class=\"on\">" + ipage + "</a>");
                    }
                    else
                    {
                        strHTML.Append("<a href=\"" + pageurl + "&page=" + ipage + "\">" + ipage + "</a>");
                    }
                }
            }
            else if (currentpage + 7 > pagecount)
            {
                for (ipage = 1; ipage <= 2; ipage++)
                {
                    if (currentpage == ipage)
                    {
                        strHTML.Append("<a href=\"javascript:;\" class=\"on\">" + ipage + "</a>");
                    }
                    else
                    {
                        strHTML.Append("<a href=\"" + pageurl + "&page=" + ipage + "\">" + ipage + "</a>");
                    }
                }
                strHTML.Append("<span>...</span>");
                for (ipage = pagecount - 9; ipage <= pagecount; ipage++)
                {
                    if (currentpage == ipage)
                    {
                        strHTML.Append("<a href=\"javascript:;\" class=\"on\">" + ipage + "</a>");
                    }
                    else
                    {
                        strHTML.Append("<a href=\"" + pageurl + "&page=" + ipage + "\">" + ipage + "</a>");
                    }
                }
            }
            else
            {
                for (ipage = 1; ipage <= 2; ipage++)
                {
                    if (currentpage == ipage)
                    {
                        strHTML.Append("<a href=\"javascript:;\" class=\"on\">" + ipage + "</a>");
                    }
                    else
                    {
                        strHTML.Append("<a href=\"" + pageurl + "&page=" + ipage + "\">" + ipage + "</a>");
                    }
                }
                strHTML.Append("<span>...</span>");
                for (ipage = currentpage - 5; ipage <= currentpage + 4; ipage++)
                {
                    if (currentpage == ipage)
                    {
                        strHTML.Append("<a href=\"javascript:;\" class=\"on\">" + ipage + "</a>");
                    }
                    else
                    {
                        strHTML.Append("<a href=\"" + pageurl + "&page=" + ipage + "\">" + ipage + "</a>");
                    }
                }
                strHTML.Append("...");
                for (ipage = pagecount - 1; ipage <= pagecount; ipage++)
                {
                    if (currentpage == ipage)
                    {
                        strHTML.Append("<a href=\"javascript:;\" class=\"on\">" + ipage + "</a>");
                    }
                    else
                    {
                        strHTML.Append("<a href=\"" + pageurl + "&page=" + ipage + "\">" + ipage + "</a>");
                    }
                }
            }
        }

        if (currentpage == pagecount)
        {
            strHTML.Append("<a>></a> ");
        }
        else
        {
            strHTML.Append("<a href=\"" + pageurl + "&page=" + (currentpage + 1).ToString() + "\" >></a> ");
        }
        strHTML.Append("</div>");

        Response.Write(strHTML.ToString());
    }
    /// <summary>
    /// 商品列表页专用
    /// </summary>
    /// <param name="pagecount"></param>
    /// <param name="currentpage"></param>
    /// <param name="pagesize"></param>
    /// <param name="recordcount"></param>
    public void Page(int pagecount, int currentpage, int pagesize, int recordcount)
    {
        int ipage = 0;
        Response.Write("<table border='0' cellspacing='1' cellpadding='0' height='26'>");
        Response.Write("<tr align='center' valign='middle'>");

        Response.Write("<td class=");
        if (currentpage <= 1)
        {
            Response.Write("page_off");
            Response.Write(">");
            Response.Write("&#171; 上一页");
        }
        else
        {
            Response.Write("page_on");
            Response.Write(">");
            Response.Write("<a href='javascript:;' onclick=\"javascript:filter_setvalue('page'," + (currentpage - 1) + ");\" class='page_on_t'>");
            Response.Write("&#171; 上一页");
            Response.Write("</a>");
        }
        Response.Write("</td>");
        if (pagecount <= 12)
        {
            for (ipage = 1; ipage <= pagecount; ipage++)
            {
                Response.Write("<td class=");
                if (currentpage == ipage)
                {
                    Response.Write("page_current");
                }
                else
                {
                    Response.Write("page_num");
                }
                Response.Write(">");
                if (currentpage == ipage)
                {
                    Response.Write(ipage);
                }
                else
                {
                    Response.Write("<a href='javascript:;' onclick=\"javascript:filter_setvalue('page'," + ipage + ");\" class='page_num_t'>" + ipage + "</a>");
                }
                Response.Write("</td>");
            }
        }
        else if (pagecount > 12 & pagecount < 16)
        {
            if (currentpage < 9)
            {
                for (ipage = 1; ipage <= 10; ipage++)
                {
                    Response.Write("<td class=");
                    if (currentpage == ipage)
                    {
                        Response.Write("page_current");
                    }
                    else
                    {
                        Response.Write("page_num");
                    }
                    Response.Write(">");
                    if (currentpage == ipage)
                    {
                        Response.Write(ipage);
                    }
                    else
                    {
                        Response.Write("<a href='javascript:;' onclick=\"javascript:filter_setvalue('page'," + ipage + ");\" class='page_num_t'>" + ipage + "</a>");
                    }
                    Response.Write("</td>");
                }
                Response.Write("<td class=page_omit>");
                Response.Write("&#8230;");
                Response.Write("</td>");
                for (ipage = pagecount - 1; ipage <= pagecount; ipage++)
                {
                    Response.Write("<td class=");
                    if (currentpage == ipage)
                    {
                        Response.Write("page_current");
                    }
                    else
                    {
                        Response.Write("page_num");
                    }
                    Response.Write(">");
                    if (currentpage == ipage)
                    {
                        Response.Write(ipage);
                    }
                    else
                    {
                        Response.Write("<a href='javascript:;' onclick=\"javascript:filter_setvalue('page'," + ipage + ");\" class='page_num_t'>" + ipage + "</a>");
                    }
                    Response.Write("</td>");
                }
            }
            else
            {
                for (ipage = 1; ipage <= 2; ipage++)
                {
                    Response.Write("<td class=");
                    if (currentpage == ipage)
                    {
                        Response.Write("page_current");
                    }
                    else
                    {
                        Response.Write("page_num");
                    }
                    Response.Write(">");
                    if (currentpage == ipage)
                    {
                        Response.Write(ipage);
                    }
                    else
                    {
                        Response.Write("<a href='javascript:;' onclick=\"javascript:filter_setvalue('page'," + ipage + ");\" class='page_num_t'>" + ipage + "</a>");
                    }
                    Response.Write("</td>");
                }
                Response.Write("<td class=page_omit>");
                Response.Write("&#8230;");
                Response.Write("</td>");
                for (ipage = pagecount - 9; ipage <= pagecount; ipage++)
                {
                    Response.Write("<td class=");
                    if (currentpage == ipage)
                    {
                        Response.Write("page_current");
                    }
                    else
                    {
                        Response.Write("page_num");
                    }
                    Response.Write(">");
                    if (currentpage == ipage)
                    {
                        Response.Write(ipage);
                    }
                    else
                    {
                        Response.Write("<a href='javascript:;' onclick=\"javascript:filter_setvalue('page'," + ipage + ");\" class='page_num_t'>" + ipage + "</a>");
                    }
                    Response.Write("</td>");
                }
            }
        }
        else if (pagecount >= 16)
        {
            if (currentpage < 9)
            {
                for (ipage = 1; ipage <= 10; ipage++)
                {
                    Response.Write("<td class=");
                    if (currentpage == ipage)
                    {
                        Response.Write("page_current");
                    }
                    else
                    {
                        Response.Write("page_num");
                    }
                    Response.Write(">");
                    if (currentpage == ipage)
                    {
                        Response.Write(ipage);
                    }
                    else
                    {
                        Response.Write("<a href='javascript:;' onclick=\"javascript:filter_setvalue('page'," + ipage + ");\" class='page_num_t'>" + ipage + "</a>");
                    }
                    Response.Write("</td>");
                }
                Response.Write("<td class=page_omit>");
                Response.Write("&#8230;");
                Response.Write("</td>");
                for (ipage = pagecount - 1; ipage <= pagecount; ipage++)
                {
                    Response.Write("<td class=");
                    if (currentpage == ipage)
                    {
                        Response.Write("page_current");
                    }
                    else
                    {
                        Response.Write("page_num");
                    }
                    Response.Write(">");
                    if (currentpage == ipage)
                    {
                        Response.Write(ipage);
                    }
                    else
                    {
                        Response.Write("<a href='javascript:;' onclick=\"javascript:filter_setvalue('page'," + ipage + ");\" class='page_num_t'>" + ipage + "</a>");
                    }
                    Response.Write("</td>");
                }
            }
            else if (currentpage + 7 > pagecount)
            {
                for (ipage = 1; ipage <= 2; ipage++)
                {
                    Response.Write("<td class=");
                    if (currentpage == ipage)
                    {
                        Response.Write("page_current");
                    }
                    else
                    {
                        Response.Write("page_num");
                    }
                    Response.Write(">");
                    if (currentpage == ipage)
                    {
                        Response.Write(ipage);
                    }
                    else
                    {
                        Response.Write("<a href='javascript:;' onclick=\"javascript:filter_setvalue('page'," + ipage + ");\" class='page_num_t'>" + ipage + "</a>");
                    }
                    Response.Write("</td>");
                }
                Response.Write("<td class=page_omit>");
                Response.Write("&#8230;");
                Response.Write("</td>");
                for (ipage = pagecount - 9; ipage <= pagecount; ipage++)
                {
                    Response.Write("<td class=");
                    if (currentpage == ipage)
                    {
                        Response.Write("page_current");
                    }
                    else
                    {
                        Response.Write("page_num");
                    }
                    Response.Write(">");
                    if (currentpage == ipage)
                    {
                        Response.Write(ipage);
                    }
                    else
                    {
                        Response.Write("<a href='javascript:;' onclick=\"javascript:filter_setvalue('page'," + ipage + ");\" class='page_num_t'>" + ipage + "</a>");
                    }
                    Response.Write("</td>");
                }
            }
            else
            {
                for (ipage = 1; ipage <= 2; ipage++)
                {
                    Response.Write("<td class=");
                    if (currentpage == ipage)
                    {
                        Response.Write("page_current");
                    }
                    else
                    {
                        Response.Write("page_num");
                    }
                    Response.Write(">");
                    if (currentpage == ipage)
                    {
                        Response.Write(ipage);
                    }
                    else
                    {
                        Response.Write("<a href='javascript:;' onclick=\"javascript:filter_setvalue('page'," + ipage + ");\" class='page_num_t'>" + ipage + "</a>");
                    }
                    Response.Write("</td>");
                }
                Response.Write("<td class=page_omit>");
                Response.Write("&#8230;");
                Response.Write("</td>");
                for (ipage = currentpage - 5; ipage <= currentpage + 4; ipage++)
                {
                    Response.Write("<td class=");
                    if (currentpage == ipage)
                    {
                        Response.Write("page_current");
                    }
                    else
                    {
                        Response.Write("page_num");
                    }
                    Response.Write(">");
                    if (currentpage == ipage)
                    {
                        Response.Write(ipage);
                    }
                    else
                    {
                        Response.Write("<a href='javascript:;' onclick=\"javascript:filter_setvalue('page'," + ipage + ");\" class='page_num_t'>" + ipage + "</a>");
                    }
                    Response.Write("</td>");
                }
                Response.Write("<td class=page_omit>");
                Response.Write("&#8230;");
                Response.Write("</td>");
                for (ipage = pagecount - 1; ipage <= pagecount; ipage++)
                {
                    Response.Write("<td class=");
                    if (currentpage == ipage)
                    {
                        Response.Write("page_current");
                    }
                    else
                    {
                        Response.Write("page_num");
                    }
                    Response.Write(">");
                    if (currentpage == ipage)
                    {
                        Response.Write(ipage);
                    }
                    else
                    {
                        Response.Write("<a href='javascript:;' onclick=\"javascript:filter_setvalue('page'," + ipage + ");\" class='page_num_t'>" + ipage + "</a>");
                    }
                    Response.Write("</td>");
                }
            }
        }
        Response.Write("<td class=");
        if (currentpage == pagecount)
        {
            Response.Write("page_off");
            Response.Write(">");
            Response.Write("下一页 &#187;");
        }
        else
        {
            Response.Write("page_on");
            Response.Write(">");
            Response.Write("<a href='javascript:;' onclick=\"javascript:filter_setvalue('page'," + (currentpage + 1) + ");\" class='page_on_t'>");
            Response.Write("下一页 &#187;");
            Response.Write("</a>");
        }
        Response.Write("</td>");
        Response.Write("</tr>");
        Response.Write("</table>");
    }

    /// <summary>
    /// 转换图片地址
    /// </summary>
    /// <param name="imgpath"></param>
    /// <param name="returntype"></param>
    /// <returns></returns>
    public string FormatImgURL(string imgpath, string returntype)
    {
        string tmpimg = "";
        string tmpimg1 = ""; ;
        imgpath = tools.NullStr(imgpath);

        switch (returntype)
        {
            case "original":
                if (imgpath != "/images/detail_no_pic.gif" && imgpath.Length > 0)
                {
                    tmpimg = "";
                }
                else
                {
                    tmpimg = Application["site_url"] + imgpath;
                }
                break;
            case "fullpath":
                if (imgpath != "/images/detail_no_pic.gif" && imgpath.Length > 0)
                {
                    tmpimg = Application["upload_server_url"] + imgpath;
                }
                else
                {
                    tmpimg = "/images/detail_no_pic.gif";
                }
                break;
            case "thumbnail":
                if (imgpath != "/images/detail_no_pic.gif" && imgpath.Length > 0)
                {
                    tmpimg1 = imgpath;
                    foreach (string tmp in imgpath.Split('/'))
                    {
                        tmpimg1 = tmp;
                    }
                    tmpimg1 = imgpath.Replace(tmpimg1, "s_" + tmpimg1);
                    tmpimg = Application["upload_server_url"] + tmpimg1;
                }
                else
                {
                    tmpimg = "/images/detail_no_pic.gif";
                }
                break;

        }
        return tmpimg;
    }

    /// <summary>
    /// 生成随机码
    /// </summary>
    /// <returns></returns>
    public string Createvkey()
    {
        string strSource = "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z";
        string[] strArray = strSource.Split(',');

        string strKey = "";
        Random ran = new Random();
        for (int i = 0; i < 64; i++) { strKey += strArray[ran.Next(62)]; }
        ran = null;

        return strKey;
    }

    public string Createvkey(int length)
    {
        string strSource = "0,1,2,3,4,5,6,7,8,9";
        string[] strArray = strSource.Split(',');

        string strKey = "";
        Random ran = new Random();
        for (int i = 0; i < length; i++) { strKey += strArray[ran.Next(9)]; }
        ran = null;

        return strKey;
    }

    public string CheckRadio(string input_value, string default_value)
    {
        if (input_value == default_value)
        {
            return " checked";
        }
        else
        {
            return "";
        }
    }

    public string CheckSelect(string input_value, string default_value)
    {
        if (input_value == default_value)
        {
            return "  selected=\"selected\" ";
        }
        else
        {
            return "";
        }
    }

    public string InIMGCode(string imgPath)
    {
        return "<img src=\"" + imgPath + "\" align=\"absmiddle\" hspace=\"5\" />";
    }

    //检查手机号
    public bool Checkmobile_back(string check_str)
    {
        bool result = true;
        if (check_str.Length != 11)
        {
            result = false;
        }
        if (result)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("1[0-9]{10}");
            result = regex.IsMatch(check_str);
        }
        return result;
    }

    //检查手机号或固定电话
    public bool Checkmobile(string check_str)
    {
        bool result = true;
        if (check_str.Length < 11)
        {
            result = false;
        }
        if (result)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"(^(0[0-9]{2,3}\-)?([2-9][0-9]{6,7})+(\-[0-9]{1,4})?$)|(^((\(\d{3}\))|(\d{3}\-))?(1[358]\d{9})$)");
            result = regex.IsMatch(check_str);
        }
        return result;
    }

    //获取会员等级信息
    public IList<MemberGradeInfo> GetMemberGrades()
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberGradeInfo.Member_Grade_Site", "=", "CN"));
        IList<MemberGradeInfo> membergrade = Mygrade.GetMemberGrades(Query, CreateUserPrivilege("1c955ea6-881f-48d8-ba8d-c5aa7ce9cfea"));
        return membergrade;
    }

    //获取会员获取积分
    public int Get_Member_Coin(double Product_Price)
    {
        int coin_amount = 0;
        coin_amount = (int)(Product_Price * tools.CheckFloat(Application["Coin_Rate"].ToString()));

        int member_grade;
        if (tools.CheckInt(Session["member_id"].ToString()) > 0)
        {

            MemberInfo member = MyMem.GetMemberByID(tools.CheckInt(Session["member_id"].ToString()), CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
            if (member != null)
            {
                member_grade = member.Member_Grade;
                //检查会员等级优惠
                IList<MemberGradeInfo> membergrade = GetMemberGrades();
                if (membergrade != null)
                {
                    foreach (MemberGradeInfo entity in membergrade)
                    {
                        if (member_grade == entity.Member_Grade_ID)
                        {
                            coin_amount = (int)(Product_Price * entity.Member_Grade_CoinRate);
                        }
                    }
                }
            }
        }
        return coin_amount;
    }

    //检查商品限时促销
    public PromotionLimitInfo GetPromotionLimitByProductID(int product_id)
    {
        PromotionLimitInfo limitinfo = null;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionLimitInfo.Promotion_Limit_ProductID", "=", product_id.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(s,{PromotionLimitInfo.Promotion_Limit_Starttime}, GETDATE())", ">=", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(s,{PromotionLimitInfo.Promotion_Limit_Endtime}, GETDATE())", "<=", "0"));
        Query.OrderInfos.Add(new OrderInfo("PromotionLimitInfo.Promotion_Limit_Price", "asc"));
        IList<PromotionLimitInfo> entitys = MyLimit.GetPromotionLimits(Query, CreateUserPrivilege("22d21441-155a-4dc5-aec6-dcf5bdedd5cf"));
        if (entitys != null)
        {
            foreach (PromotionLimitInfo entity in entitys)
            {
                limitinfo = entity;
                break;
            }
            return limitinfo;
        }
        else
        {
            return null;
        }
    }

    //获取会员价格(促销商品价格按照会员等级进行划分   Promotion_Limit_MemberGrade（限时促销适用会员等级） )  
    public double Get_Member_PriceByMemberGrade(int product_id, double product_price)
    {
        int GradeID = 1;
        MemberGradeInfo GradeInfo = Mygrade.GetMemberDefaultGrade();
        if (GradeInfo != null)
        {
            GradeID = GradeInfo.Member_Grade_ID;
        }
        double member_price = product_price;
        int member_grade = 0;
        bool match = false;
        PromotionLimitInfo limitinfo = GetPromotionLimitByProductID(product_id);
        if (limitinfo != null)
        {
            if (limitinfo.PromotionLimitMemberGrades != null)
            {
                foreach (PromotionLimitMemberGradeInfo gradeinfo in limitinfo.PromotionLimitMemberGrades)
                {
                    if ((tools.NullInt(Session["member_id"]) == 0 && gradeinfo.Promotion_Limit_MemberGrade_Grade == GradeID) || (gradeinfo.Promotion_Limit_MemberGrade_Grade == tools.NullInt(Session["member_grade"]) && tools.NullInt(Session["member_id"]) > 0))
                    {
                        match = true;
                        member_price = limitinfo.Promotion_Limit_Price;
                    }
                }
            }
        }
        if (match)
        {
            return member_price;
        }
        else
        {
            if (tools.NullInt(Session["member_id"].ToString()) > 0)
            {

                MemberInfo member = MyMem.GetMemberByID(tools.CheckInt(Session["member_id"].ToString()), CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
                if (member != null)
                {
                    member_grade = member.Member_Grade;
                    //检查会员等级优惠
                    IList<MemberGradeInfo> membergrade = GetMemberGrades();
                    if (membergrade != null)
                    {
                        foreach (MemberGradeInfo entity in membergrade)
                        {
                            if (member_grade == entity.Member_Grade_ID)
                            {
                                member_price = (product_price * entity.Member_Grade_Percent) / 100;
                            }
                        }
                    }
                    //检查商品等级价格
                    if (product_id > 0)
                    {
                        IList<ProductPriceInfo> productprice = Myprice.GetProductPrices(product_id);
                        if (productprice != null)
                        {
                            foreach (ProductPriceInfo entity in productprice)
                            {
                                if (member_grade == entity.Product_Price_MemberGradeID)
                                {
                                    member_price = entity.Product_Price_Price;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                IList<MemberGradeInfo> membergrade = GetMemberGrades();
                if (membergrade != null)
                {
                    foreach (MemberGradeInfo entity in membergrade)
                    {
                        if (entity.Member_Grade_Default == 1)
                        {
                            member_grade = entity.Member_Grade_ID;
                            member_price = (product_price * entity.Member_Grade_Percent) / 100;
                        }
                    }
                }

                //检查商品等级价格
                if (product_id > 0)
                {
                    IList<ProductPriceInfo> productprice = Myprice.GetProductPrices(product_id);
                    if (productprice != null)
                    {
                        foreach (ProductPriceInfo entity in productprice)
                        {
                            if (member_grade == entity.Product_Price_MemberGradeID)
                            {
                                member_price = entity.Product_Price_Price;
                            }
                        }
                    }
                }
            }
            return tools.CheckFloat(member_price.ToString("0.00"));
        }
    }


    //获取会员价格
    public double Get_Member_Price(int product_id, double product_price)
    {
        
        double member_price = product_price;
        int supplier_grade = 0;
        bool match = false;
        PromotionLimitInfo limitinfo = GetPromotionLimitByProductID(product_id);
        if (limitinfo != null)
        {
            match = true;
            member_price = limitinfo.Promotion_Limit_Price;
        }


        if (match)
        {
            return member_price;
        }
        else
        {
            if (tools.NullInt(Session["member_id"].ToString()) > 0)
            {
                MemberInfo member = MyMem.GetMemberByID(tools.NullInt(Session["member_id"]), CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
                if (member != null)
                {
                    supplier_grade = member.Member_Grade;
                    //检查会员等级优惠
                    IList<MemberGradeInfo> membergrade = GetMemberGrades();
                    if (membergrade != null)
                    {
                        foreach (MemberGradeInfo entity in membergrade)
                        {
                            if (supplier_grade == entity.Member_Grade_ID)
                            {
                                member_price = (product_price * entity.Member_Grade_Percent) / 100;
                            }
                        }
                    }
                    //检查商品等级价格
                    if (product_id > 0)
                    {
                        IList<ProductPriceInfo> productprice = Myprice.GetProductPrices(product_id);
                        if (productprice != null)
                        {
                            foreach (ProductPriceInfo entity in productprice)
                            {
                                if (supplier_grade == entity.Product_Price_MemberGradeID)
                                {
                                    member_price = entity.Product_Price_Price;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                IList<MemberGradeInfo> membergrade = GetMemberGrades();
                if (membergrade != null)
                {
                    foreach (MemberGradeInfo entity in membergrade)
                    {
                        if (entity.Member_Grade_Default == 1)
                        {
                            supplier_grade = entity.Member_Grade_ID;
                            member_price = (product_price * entity.Member_Grade_Percent) / 100;
                        }
                    }
                }

                //检查商品等级价格
                if (product_id > 0)
                {
                    IList<ProductPriceInfo> productprice = Myprice.GetProductPrices(product_id);
                    if (productprice != null)
                    {
                        foreach (ProductPriceInfo entity in productprice)
                        {
                            if (supplier_grade == entity.Product_Price_MemberGradeID)
                            {
                                member_price = entity.Product_Price_Price;
                            }
                        }
                    }
                }
            }
            return tools.CheckFloat(member_price.ToString("0.00"));
        }
    }

    //获取会员价格
    public double Get_Member_Grade_Price(int product_id, double product_price)
    {
        double member_price = product_price;
        int member_grade = 0;

        if (tools.CheckInt(Session["member_id"].ToString()) > 0)
        {

            MemberInfo member = MyMem.GetMemberByID(tools.CheckInt(Session["member_id"].ToString()), CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
            if (member != null)
            {
                member_grade = member.Member_Grade;
                //检查会员等级优惠
                IList<MemberGradeInfo> membergrade = GetMemberGrades();
                if (membergrade != null)
                {
                    foreach (MemberGradeInfo entity in membergrade)
                    {
                        if (member_grade == entity.Member_Grade_ID)
                        {
                            member_price = (product_price * entity.Member_Grade_Percent) / 100;
                        }
                    }
                }
                //检查商品等级价格
                if (product_id > 0)
                {
                    IList<ProductPriceInfo> productprice = Myprice.GetProductPrices(product_id);
                    if (productprice != null)
                    {
                        foreach (ProductPriceInfo entity in productprice)
                        {
                            if (member_grade == entity.Product_Price_MemberGradeID)
                            {
                                member_price = entity.Product_Price_Price;
                            }
                        }
                    }
                }
            }
        }
        else
        {
            IList<MemberGradeInfo> membergrade = GetMemberGrades();
            if (membergrade != null)
            {
                foreach (MemberGradeInfo entity in membergrade)
                {
                    if (entity.Member_Grade_Default == 1)
                    {
                        member_grade = entity.Member_Grade_ID;
                        member_price = (product_price * entity.Member_Grade_Percent) / 100;
                    }
                }
            }

            //检查商品等级价格
            if (product_id > 0)
            {
                IList<ProductPriceInfo> productprice = Myprice.GetProductPrices(product_id);
                if (productprice != null)
                {
                    foreach (ProductPriceInfo entity in productprice)
                    {
                        if (member_grade == entity.Product_Price_MemberGradeID)
                        {
                            member_price = entity.Product_Price_Price;
                        }
                    }
                }
            }
        }
        return tools.CheckFloat(member_price.ToString("0.00"));
    }

    //获取会员等级价格
    public double Get_MemberGrade_Price(int member_Grade, double product_price)
    {
        double member_price = product_price;
        int member_grade = 0;
        IList<MemberGradeInfo> membergrade = GetMemberGrades();
        if (membergrade != null)
        {
            foreach (MemberGradeInfo entity in membergrade)
            {
                if (entity.Member_Grade_ID == member_Grade)
                {
                    member_grade = entity.Member_Grade_ID;
                    member_price = (product_price * entity.Member_Grade_Percent) / 100;
                }
            }
        }
        return tools.CheckFloat(member_price.ToString("0.00"));
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

    /// <summary>
    /// 创建一个用户权限实例
    /// </summary>
    /// <param name="PrivilegeCode">权限代码</param>
    /// <returns></returns>
    public RBACUserInfo CreateUserPrivilege(string PrivilegeCode)
    {
        RBACUserInfo UserInfo = new RBACUserInfo();

        UserInfo.RBACRoleInfos = new List<RBACRoleInfo>();
        UserInfo.RBACRoleInfos.Add(new RBACRoleInfo());

        UserInfo.RBACRoleInfos[0].RBACPrivilegeInfos = new List<RBACPrivilegeInfo>();
        UserInfo.RBACRoleInfos[0].RBACPrivilegeInfos.Add(new RBACPrivilegeInfo());

        UserInfo.RBACRoleInfos[0].RBACPrivilegeInfos[0].RBAC_Privilege_ID = PrivilegeCode;

        return UserInfo;
    }


    //物流公司代码
    public string GetDeliveryCode(string typeCom)
    {
        switch (typeCom)
        {
            case "AAE全球专递":
                typeCom = "aae";
                break;
            case "安捷快递":
                typeCom = "anjiekuaidi";
                break;
            case "安信达快递":
                typeCom = "anxindakuaixi";
                break;
            case "百福东方":
                typeCom = "baifudongfang";
                break;
            case "彪记快递":
                typeCom = "biaojikuaidi";
                break;
            case "BHT":
                typeCom = "bht";
                break;
            case "希伊艾斯快递":
                typeCom = "cces";
                break;
            case "中国东方":
                typeCom = "Coe";
                break;
            case "长宇物流":
                typeCom = "changyuwuliu";
                break;
            case "大田物流":
                typeCom = "datianwuliu";
                break;
            case "德邦物流":
                typeCom = "debangwuliu";
                break;
            case "DPEX":
                typeCom = "dpex";
                break;
            case "DHL":
                typeCom = "dhl";
                break;
            case "D速快递":
                typeCom = "dsukuaidi";
                break;
            case "fedex":
                typeCom = "fedex";
                break;
            case "飞康达物流":
                typeCom = "feikangda";
                break;
            case "凤凰快递":
                typeCom = "fenghuangkuaidi";
                break;
            case "港中能达物流":
                typeCom = "ganzhongnengda";
                break;
            case "广东邮政物流":
                typeCom = "guangdongyouzhengwuliu";
                break;
            case "汇通快递":
                typeCom = "huitongkuaidi";
                break;
            case "恒路物流":
                typeCom = "hengluwuliu";
                break;
            case "华夏龙物流":
                typeCom = "huaxialongwuliu";
                break;
            case "佳怡物流":
                typeCom = "jiayiwuliu";
                break;
            case "京广速递":
                typeCom = "jinguangsudikuaijian";
                break;
            case "急先达":
                typeCom = "jixianda";
                break;
            case "加运美":
                typeCom = "jiayunmeiwuliu";
                break;
            case "快捷速递":
                typeCom = "kuaijiesudi";
                break;
            case "联昊通物流":
                typeCom = "lianhaowuliu";
                break;
            case "龙邦物流":
                typeCom = "longbanwuliu";
                break;
            case "民航快递":
                typeCom = "minghangkuaidi";
                break;
            case "配思货运":
                typeCom = "peisihuoyunkuaidi";
                break;
            case "全晨快递":
                typeCom = "quanchenkuaidi";
                break;
            case "全际通物流":
                typeCom = "quanjitong";
                break;
            case "全日通快递":
                typeCom = "quanritongkuaidi";
                break;
            case "全一快递":
                typeCom = "quanyikuaidi";
                break;
            case "盛辉物流":
                typeCom = "shenghuiwuliu";
                break;
            case "速尔物流":
                typeCom = "suer";
                break;
            case "盛丰物流":
                typeCom = "shengfengwuliu";
                break;
            case "天地华宇":
                typeCom = "tiandihuayu";
                break;
            case "天天快递":
                typeCom = "tiantian";
                break;
            case "TNT":
                typeCom = "tnt";
                break;
            case "UPS":
                typeCom = "ups";
                break;
            case "万家物流":
                typeCom = "wanjiawuliu";
                break;
            case "文捷航空速递":
                typeCom = "wenjiesudi";
                break;
            case "伍圆速递":
                typeCom = "wuyuansudi";
                break;
            case "万象物流":
                typeCom = "wanxiangwuliu";
                break;
            case "新邦物流":
                typeCom = "xinbangwuliu";
                break;
            case "信丰物流":
                typeCom = "xinfengwuliu";
                break;
            case "星晨急便":
                typeCom = "xingchengjibian";
                break;
            case "鑫飞鸿物流":
                typeCom = "xinhongyukuaidi";
                break;
            case "亚风速递":
                typeCom = "yafengsudi";
                break;
            case "一邦速递":
                typeCom = "yibangwuliu";
                break;
            case "优速物流":
                typeCom = "youshuwuliu";
                break;
            case "远成物流":
                typeCom = "yuanchengwuliu";
                break;
            case "圆通速递":
                typeCom = "yuantong";
                break;
            case "源伟丰快递":
                typeCom = "yuanweifeng";
                break;
            case "元智捷诚快递":
                typeCom = "yuanzhijiecheng";
                break;
            case "越丰物流":
                typeCom = "yuefengwuliu";
                break;
            case "韵达快递":
                typeCom = "yunda";
                break;
            case "源安达":
                typeCom = "yuananda";
                break;
            case "运通快递":
                typeCom = "yuntongkuaidi";
                break;
            case "宅急送":
                typeCom = "zhaijisong";
                break;
            case "中铁快运":
                typeCom = "zhongtiewuliu";
                break;
            case "中通速递":
                typeCom = "zhongtong";
                break;
            case "中邮物流":
                typeCom = "zhongyouwuliu";
                break;
            case "申通":
                typeCom = "shentong";
                break;
            case "汇通":
                typeCom = "huitongkuaidi";
                break;
            case "联邦快递":
                typeCom = "fedex";
                break;
            case "顺丰速递":
                typeCom = "shunfeng";
                break;
            case "EMS特快专递":
                typeCom = "ems";
                break;
        }
        return typeCom;
    }

    //物流公司选择
    public string Delivery_Company_Select(string select_name, string Code)
    {

        string way_list = "";
        way_list = "<select name=\"" + select_name + "\">";
        for (int i = 1; i <= 10; i++)
        {
            string wayname = GetDeliveryWay(i);
            if (wayname == Code)
            {
                way_list += "<option selected=\"selected\" value=\"" + wayname + "\">" + wayname + "</option>";
            }
            else
            {
                way_list += "<option value=\"" + wayname + "\">" + wayname + "</option>";
            }
        }
        way_list = way_list + "</select>";
        return way_list;
    }

    private string GetDeliveryWay(int id)
    {
        string typeCom = "";
        switch (id)
        {
            case 1:
                typeCom = "宅急送";
                break;
            case 2:
                typeCom = "顺丰速递";
                break;
            case 3:
                typeCom = "EMS特快专递";
                break;
            case 4:
                typeCom = "韵达快递";
                break;
            case 5:
                typeCom = "天天快递";
                break;
            case 6:
                typeCom = "中铁快运";
                break;
            case 7:
                typeCom = "中通速递";
                break;
            case 8:
                typeCom = "申通";
                break;
            case 9:
                typeCom = "汇通快递";
                break;
            case 10:
                typeCom = "圆通速递";
                break;
        }
        return typeCom;
    }

    /// <summary>
    /// 获得当前站点标识
    /// </summary>
    /// <returns></returns>
    public string GetCurrentSite()
    {
        if (tools.NullStr(Session["CurrentSite"]).Length > 0)
            return tools.NullStr(Session["CurrentSite"]);
        else
            return "CN";
    }

    /// <summary>
    /// 邮编检测
    /// </summary>
    /// <param name="zip">带检测字符串</param>
    /// <returns></returns>
    public bool Check_ZipCode(string zip)
    {
        if (zip.Length != 6)
        {
            return false;
        }
        else
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("[0-9]{6}");
            return regex.IsMatch(zip);
        }
    }

    #region 取中文首字母

    /// <summary>
    /// 得到汉字首字母
    /// </summary>
    /// <param name="paramChinese"></param>
    /// <returns></returns>
    public string GetFirstLetter(string paramChinese)
    {
        string strTemp = "";
        int iLen = paramChinese.Length;
        int i = 0;

        for (i = 0; i <= iLen - 1; i++)
        {
            strTemp += GetCharSpellCode(paramChinese.Substring(i, 1));
        }

        return strTemp;

    }

    /// <summary>    
    /// 得到一个汉字的拼音第一个字母，如果是一个英文字母则直接返回大写字母    
    /// </summary>    
    /// <param name="CnChar">单个汉字</param>    
    /// <returns>单个大写字母</returns>    
    private string GetCharSpellCode(string paramChar)
    {
        long iCnChar;

        byte[] ZW = System.Text.Encoding.Default.GetBytes(paramChar);

        //如果是字母，则直接返回    
        if (ZW.Length == 1)
        {
            return paramChar.ToUpper();
        }
        else
        {
            // get the array of byte from the single char    
            int i1 = (short)(ZW[0]);
            int i2 = (short)(ZW[1]);
            iCnChar = i1 * 256 + i2;
        }

        //expresstion    
        //table of the constant list    
        // 'A'; //45217..45252    
        // 'B'; //45253..45760    
        // 'C'; //45761..46317    
        // 'D'; //46318..46825    
        // 'E'; //46826..47009    
        // 'F'; //47010..47296    
        // 'G'; //47297..47613    

        // 'H'; //47614..48118    
        // 'J'; //48119..49061    
        // 'K'; //49062..49323    
        // 'L'; //49324..49895    
        // 'M'; //49896..50370    
        // 'N'; //50371..50613    
        // 'O'; //50614..50621    
        // 'P'; //50622..50905    
        // 'Q'; //50906..51386    

        // 'R'; //51387..51445    
        // 'S'; //51446..52217    
        // 'T'; //52218..52697    
        //没有U,V    
        // 'W'; //52698..52979    
        // 'X'; //52980..53640    
        // 'Y'; //53689..54480    
        // 'Z'; //54481..55289    

        // iCnChar match the constant    
        if ((iCnChar >= 45217) && (iCnChar <= 45252))
        {
            return "A";
        }
        else if ((iCnChar >= 45253) && (iCnChar <= 45760))
        {
            return "B";
        }
        else if ((iCnChar >= 45761) && (iCnChar <= 46317))
        {
            return "C";
        }
        else if ((iCnChar >= 46318) && (iCnChar <= 46825))
        {
            return "D";
        }
        else if ((iCnChar >= 46826) && (iCnChar <= 47009))
        {
            return "E";
        }
        else if ((iCnChar >= 47010) && (iCnChar <= 47296))
        {
            return "F";
        }
        else if ((iCnChar >= 47297) && (iCnChar <= 47613))
        {
            return "G";
        }
        else if ((iCnChar >= 47614) && (iCnChar <= 48118))
        {
            return "H";
        }
        else if ((iCnChar >= 48119) && (iCnChar <= 49061))
        {
            return "J";
        }
        else if ((iCnChar >= 49062) && (iCnChar <= 49323))
        {
            return "K";
        }
        else if ((iCnChar >= 49324) && (iCnChar <= 49895))
        {
            return "L";
        }
        else if ((iCnChar >= 49896) && (iCnChar <= 50370))
        {
            return "M";
        }

        else if ((iCnChar >= 50371) && (iCnChar <= 50613))
        {
            return "N";
        }
        else if ((iCnChar >= 50614) && (iCnChar <= 50621))
        {
            return "O";
        }
        else if ((iCnChar >= 50622) && (iCnChar <= 50905))
        {
            return "P";
        }
        else if ((iCnChar >= 50906) && (iCnChar <= 51386))
        {
            return "Q";
        }
        else if ((iCnChar >= 51387) && (iCnChar <= 51445))
        {
            return "R";
        }
        else if ((iCnChar >= 51446) && (iCnChar <= 52217))
        {
            return "S";
        }
        else if ((iCnChar >= 52218) && (iCnChar <= 52697))
        {
            return "T";
        }
        else if ((iCnChar >= 52698) && (iCnChar <= 52979))
        {
            return "W";
        }
        else if ((iCnChar >= 52980) && (iCnChar <= 53688))
        {
            return "X";
        }
        else if ((iCnChar >= 53689) && (iCnChar <= 54480))
        {
            return "Y";
        }
        else if ((iCnChar >= 54481) && (iCnChar <= 55289))
        {
            return "Z";
        }
        else return ("?");
    }

    /// <summary>
    /// 取第一个字的首字母
    /// </summary>
    /// <param name="paramChinese"></param>
    /// <returns></returns>
    public string GetFirstWordLetter(string paramChinese)
    {
        if (paramChinese != null && paramChinese.Length > 0)
        {
            return GetCharSpellCode(paramChinese.Substring(0, 1));
        }
        else
        {
            return paramChinese;
        }
    }

    #endregion

    public double GetProductPrice(double Product_ManualFee, double Product_Weight)
    {
        double product_price = 0;

        product_price = (tools.NullDbl(Application["Au99.99"]) + Product_ManualFee) * Product_Weight;

        return tools.NullDbl(product_price.ToString("0.00"));
    }

    public string GetGoldJson()
    {
        CookieCollection cookie = new CookieCollection();
        string url = GoldAPI + "?key=62c4edfa50bd4efe8811cdcbbd81d3e6";
        return httpHelper.GetResponseString(httpHelper.CreateGetHttpResponse(url, 120000, "", cookie));
    }

    public void GetGoldPrice()
    {
        GoldJsonInfo JsonInfo = null;
        IList<GoldResultInfo> listResult = null;

        string strJson = GetGoldJson();

        try
        {
            JsonInfo = jsonHelper.JSONToObject<GoldJsonInfo>(strJson);
            if (JsonInfo.Error_code == 0 && JsonInfo.Reason == "成功" && JsonInfo.Result != null)
            {
                listResult = JsonInfo.Result;
                foreach (GoldResultInfo entity in listResult)
                {
                    if (entity.Variety == "Au99.99" && entity.Latestpri != "0.00")
                    {
                        Application["Au99.99"] = entity.Latestpri;
                        Application["Au99.99_limit"] = entity.Limit;
                        Application["Au99.99_time"] = entity.Time;
                    }
                    else if (entity.Variety == "Au99.95" && entity.Latestpri != "0.00")
                    {
                        Application["Au99.95"] = entity.Latestpri;
                        Application["Au99.95_limit"] = entity.Limit;
                        Application["Au99.95_time"] = entity.Time;
                    }
                    else if (entity.Variety == "Ag99.99" && entity.Latestpri != "0.00")
                    {
                        Application["Ag99.99"] = entity.Latestpri;
                        Application["Ag99.99_limit"] = entity.Limit;
                        Application["Ag99.99_time"] = entity.Time;
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    /// <summary>
    /// 将百分比转换成小数
    /// </summary>
    /// <param name="perc">百分比值，可纯为数值，或都加上%号的表示，如：65|65%</param>
    /// <returns></returns>
    public static decimal PerctangleToDecimal(string perc)
    {
        try
        {
            return decimal.Parse(perc.TrimEnd('%')) / 100;
        }
        catch
        {
            return 1;
        }
    }

    public string Index_GoldPrice()
    {
        StringBuilder strHTML = new StringBuilder();

        decimal Au_limit = PerctangleToDecimal(tools.NullStr(Application["Au99.99_limit"]));
        decimal Ag_limit = PerctangleToDecimal(tools.NullStr(Application["Ag99.99_limit"]));

        strHTML.Append("<li>基础金价");

        if (Au_limit > 0 && tools.NullDbl(Application["Au99.99"]) > 0)
        {
            if (tools.NullDbl(Application["Au99.99"]) > 0)
            {
                strHTML.Append("<span>" + Math.Round(tools.NullDbl(Application["Au99.99"]), 2) + "</span><i>" + Au_limit + "");
            }
            else
            {
                strHTML.Append("<span>" + Math.Round(tools.NullDbl(Application["Instant_GoldPrice"]), 2) + "</span><i>" + Au_limit + "");
            }
            strHTML.Append("<img src=\"/images/icon05.jpg\">");
        }
        else
        {
            if (tools.NullDbl(Application["Au99.99"]) > 0)
            {
                strHTML.Append("<span>" + Math.Round(tools.NullDbl(Application["Au99.99"]), 2) + "</span><i>" + Au_limit + "");
            }
            else
            {
                strHTML.Append("<span>" + Math.Round(tools.NullDbl(Application["Instant_GoldPrice"]), 2) + "</span><i>" + Au_limit + "");
            }
            strHTML.Append("<img src=\"/images/icon06.jpg\">");
        }
        strHTML.Append("</i></li>");


        strHTML.Append("<li>基础银价");

        if (Ag_limit > 0 && tools.NullDbl(Application["Ag99.99"]) > 0)
        {
            if (tools.NullDbl(Application["Ag99.99"]) > 0)
            {
                strHTML.Append("<span>" + Math.Round(tools.NullDbl(Application["Ag99.99"]) / 1000, 2) + "</span><i>" + Ag_limit + "");
            }
            else
            {
                strHTML.Append("<span>" + Math.Round(tools.NullDbl(Application["Instant_SilverPrice"]) / 1000, 2) + "</span><i>" + Ag_limit + "");
            }
            strHTML.Append("<img src=\"/images/icon05.jpg\">");
        }
        else
        {
            if (tools.NullDbl(Application["Ag99.99"]) > 0)
            {
                strHTML.Append("<span>" + Math.Round(tools.NullDbl(Application["Ag99.99"]) / 1000, 2) + "</span><i>" + Ag_limit + "");
            }
            else
            {
                strHTML.Append("<span>" + Math.Round(tools.NullDbl(Application["Instant_SilverPrice"]) / 1000, 2) + "</span><i>" + Ag_limit + "");
            }
            strHTML.Append("<img src=\"/images/icon06.jpg\">");
        }
        strHTML.Append("</i></li>");

        return strHTML.ToString();
    }


    public string GetWWW()
    {
        //return "http://www" + Domain ;
        return tools.NullStr(Application["Site_URL"]);
    }

}
