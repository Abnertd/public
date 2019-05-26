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

using Glaer.Trade.Util.SQLHelper;

using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.Util.Mail;
using Glaer.Trade.B2C.BLL.SAL;



/// <summary>
///Product 的摘要说明
/// </summary>
public class Promotion
{
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private IPromotion MyBLL;
    private ITools tools;
    private Public_Class pub;
    private IPromotionLimit MyLimits;
    private Product myproduct;

    public Promotion()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        
        tools = ToolsFactory.CreateTools();
        MyBLL = PromotionFactory.CreatePromotion();
        pub = new Public_Class();
        myproduct = new Product();
        MyLimits = PromotionLimitFactory.CreatePromotionLimit();
    }

    //根据编号获取促销专题
    public PromotionInfo GetPromotionByID(int ID)
    {
        return MyBLL.GetPromotionByID(ID, pub.CreateUserPrivilege("0b16441f-dc42-4fd0-b8f1-0f8a80146771"));
    }

    public void GetPromotionLimitis()
    {
        string page_url = "";
        int curr_page = tools.CheckInt(Request["page"]);
        if (curr_page < 1)
        {
            curr_page = 1;
        }
        page_url = "/Promotion/Promotioning.aspx?flag=1";
        int i = 0;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 100;
        Query.CurrentPage = curr_page;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionLimitInfo.Promotion_Limit_Site", "=", "CN"));
        Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(s,{PromotionLimitInfo.Promotion_Limit_Starttime},'" + DateTime.Now + "')", ">=", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(s,{PromotionLimitInfo.Promotion_Limit_Endtime},'" + DateTime.Now + "')", "<=", "0"));
        Query.OrderInfos.Add(new OrderInfo("PromotionLimitInfo.Promotion_Limit_Starttime", "ASC"));
        IList<PromotionLimitInfo> entitys = MyLimits.GetPromotionLimits(Query, pub.CreateUserPrivilege("22d21441-155a-4dc5-aec6-dcf5bdedd5cf"));
        PageInfo pageinfo = MyLimits.GetPageInfo(Query, pub.CreateUserPrivilege("22d21441-155a-4dc5-aec6-dcf5bdedd5cf"));
        if (entitys != null)
        {
            Response.Write(" <div id=\"webwrap\">");
            Response.Write(" <div class=\"blk01\">");
            Response.Write(" <ul class=\"lst1\">");
            foreach (PromotionLimitInfo entity in entitys)
            {
                i++;
                Response.Write("<li><p class=\"p1\" id=\"limit_tip_" + i + "\" name=\"limit_tip\">");

                TimeSpan span1 = entity.Promotion_Limit_Endtime - DateTime.Now;
                int timespan;
                timespan = (span1.Days * (24 * 3600)) + (span1.Hours * 3600) + (span1.Minutes * 60) + (span1.Seconds);
                Response.Write("               ");

                Response.Write("<script>updatetime1(" + timespan + ",'limit_tip_" + i + "')</script>");
                Response.Write("</p>");
                ProductInfo productinfo = myproduct.GetProductByID(entity.Promotion_Limit_ProductID);
                if (productinfo != null)
                {
                    Response.Write("<a class=\"a2\" href=\"/Product/detail.aspx?Product_ID=" + entity.Promotion_Limit_ProductID + "\" target=\"_blank\" alt=\"" + productinfo.Product_Name + "\" title=\"" + productinfo.Product_Name + "\"><img src=\"" + pub.FormatImgURL(productinfo.Product_Img, "fullpath") + "\" width=\"170\" height=\"170\" onload=\"javascript:AutosizeImage(this,170,170);\"></a><p class=\"p2\"><a href=\"/Product/detail.aspx?Product_ID=" + entity.Promotion_Limit_ProductID + "\" target=\"_blank\" alt=\"" + productinfo.Product_Name + "\" title=\"" + productinfo.Product_Name + "\">" + productinfo.Product_Name + "</a></p>");
                    Response.Write("<p>原价:<span class=\"sp01\">￥" + pub.FormatCurrency(myproduct.Get_Member_Price(productinfo.Product_ID, productinfo.Product_Price)) + "</span></p>");
                }

                Response.Write(" <p>抢购价: <strong>" + pub.FormatCurrency(entity.Promotion_Limit_Price) + "</strong></p>");
                Response.Write("<a href=\"/Product/detail.aspx?Product_ID=" + entity.Promotion_Limit_ProductID + "\" target=\"_blank\" class=\"a1\"></a></li>");

            }
            Response.Write("</ul><div class=\"clear\"></div>");
            Response.Write("<div class=\"clear\"></div>");
            Response.Write("</div>");
            Response.Write("<table width=\"940px\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\">");
            Response.Write("<tr><td height=\"10\"></td></tr>");
            Response.Write("<tr><td align=\"right\">");
            pub.Page(pageinfo.PageCount, pageinfo.CurrentPage, page_url, pageinfo.PageSize, pageinfo.RecordCount);
            Response.Write("</td></tr>");
            Response.Write("</table>");
            Response.Write("<div class=\"clear\"></div>");
            Response.Write("</div>");


        }
        else
        {
            Response.Write("<div style=\"width:960px; margin:0 auto; text-align:center; height:40px; line-height:40px;\">暂无相关信息</div>");
        }

    }


}
