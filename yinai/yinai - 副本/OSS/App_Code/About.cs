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
using Glaer.Trade.B2C.BLL.CMS;

/// <summary>
///About 的摘要说明
/// </summary>
public class About {
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private IAbout MyBLL;

    public About() {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        MyBLL = AboutFactory.CreateAbout();
    }

    public void AddAbout()
    {
        int About_ID = tools.CheckInt(Request.Form["About_ID"]);
        int About_IsActive = tools.CheckInt(Request.Form["About_IsActive"]);
        string About_Title = tools.CheckStr(Request.Form["About_Title"]);
        string About_Sign = tools.CheckStr(Request.Form["About_Sign"]);
        string About_Content = Request.Form["About_Content"];
        int About_Sort = tools.CheckInt(Request.Form["About_Sort"]);

        if (About_Title == "") { Public.Msg("error", "错误信息", "请填写标题", false, "{back}"); return; }

        AboutInfo entity = new AboutInfo();
        entity.About_ID = About_ID;
        entity.About_IsActive = About_IsActive;
        entity.About_Title = About_Title;
        entity.About_Sign = About_Sign;
        entity.About_Content = About_Content;
        entity.About_Sort = About_Sort;
        entity.About_Site = Public.GetCurrentSite();

        if (MyBLL.AddAbout(entity,Public.GetUserPrivilege())) {
            Public.Msg("positive", "操作成功", "操作成功", true, "about_add.aspx");
        }
        else {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void EditAbout()
    {

        int About_ID = tools.CheckInt(Request.Form["About_ID"]);
        int About_IsActive = tools.CheckInt(Request.Form["About_IsActive"]);
        string About_Title = tools.CheckStr(Request.Form["About_Title"]);
        string About_Sign = tools.CheckStr(Request.Form["About_Sign"]);
        string About_Content = Request.Form["About_Content"];
        int About_Sort = tools.CheckInt(Request.Form["About_Sort"]);

        if (About_Title == "") { Public.Msg("error", "错误信息", "请填写标题", false, "{back}"); return; }

        AboutInfo entity = new AboutInfo();
        entity.About_ID = About_ID;
        entity.About_IsActive = About_IsActive;
        entity.About_Title = About_Title;
        entity.About_Sign = About_Sign;
        entity.About_Content = About_Content;
        entity.About_Sort = About_Sort;
        entity.About_Site = Public.GetCurrentSite();

        if (MyBLL.EditAbout(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "about_list.aspx");
        }
        else {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void DelAbout()
    {
        int About_ID = tools.CheckInt(Request.QueryString["About_ID"]);
        if (MyBLL.DelAbout(About_ID, Public.GetUserPrivilege()) > 0)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "about_list.aspx");
        }
        else {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public AboutInfo GetAboutByID(int cate_id)
    {
        return MyBLL.GetAboutByID(cate_id, Public.GetUserPrivilege());
    }

    public string GetAbouts()
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        string keyword = tools.CheckStr(Request["keyword"]);

        Query.ParamInfos.Add(new ParamInfo("AND", "str", "AboutInfo.About_Site", "=", Public.GetCurrentSite()));
        if (keyword.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "AboutInfo.About_Title", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "AboutInfo.About_Sign", "like", keyword));
        }
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyBLL.GetPageInfo(Query, Public.GetUserPrivilege());

        IList<AboutInfo> entitys = MyBLL.GetAbouts(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (AboutInfo entity in entitys)
            {
                jsonBuilder.Append("{\"AboutInfo.About_ID\":" + entity.About_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.About_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.About_Title));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.About_Sign));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.About_Sort);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");

                if (Public.CheckPrivilege("b15dd1c4-d9c5-4b09-b7c2-3ef4d24af7ef"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\" alt=\\\"修改\\\"> <a href=\\\"about_edit.aspx?about_id=" + entity.About_ID + "\\\" title=\\\"修改\\\">修改</a>");
                }

                if (Public.CheckPrivilege("622c8cf4-0cae-47f7-bd02-19bd8b5c169d"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('about_do.aspx?action=move&about_id=" + entity.About_ID + "')\\\" title=\\\"删除\\\">删除</a>");
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
        else {
            return null;
        }
    }

}
