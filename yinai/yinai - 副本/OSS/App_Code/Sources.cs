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
using Glaer.Trade.B2C.BLL.Sys;

/// <summary>
///Sources 的摘要说明
/// </summary>
public class Sources
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private ISources MyBLL;

    public Sources()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        MyBLL = SourcesFactory.CreateSources();
    }

    public void AddSources()
    {
        int Sources_ID = tools.CheckInt(Request.Form["Sources_ID"]);
        string Sources_Name = tools.CheckStr(Request.Form["Sources_Name"]);
        string Sources_Code = tools.CheckStr(Request.Form["Sources_Code"]);

        SourcesInfo entity = new SourcesInfo();
        entity.Sources_ID = Sources_ID;
        entity.Sources_Name = Sources_Name;
        entity.Sources_Code = Sources_Code;
        entity.Sources_Site = Public.GetCurrentSite();

        if (MyBLL.AddSources(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "sources_add.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void EditSources()
    {
        int Sources_ID = tools.CheckInt(Request.Form["Sources_ID"]);
        string Sources_Name = tools.CheckStr(Request.Form["Sources_Name"]);
        string Sources_Code = tools.CheckStr(Request.Form["Sources_Code"]);

        SourcesInfo entity = new SourcesInfo();
        entity.Sources_ID = Sources_ID;
        entity.Sources_Name = Sources_Name;
        entity.Sources_Code = Sources_Code;
        entity.Sources_Site = Public.GetCurrentSite();

        if (MyBLL.EditSources(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "sources_list.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void DelSources()
    {
        int Sources_ID = tools.CheckInt(Request.QueryString["Sources_ID"]);
        if (MyBLL.DelSources(Sources_ID, Public.GetUserPrivilege()) > 0)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "sources_list.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public SourcesInfo GetSourcesByID(int cate_id)
    {
        return MyBLL.GetSourcesByID(cate_id, Public.GetUserPrivilege());
    }

    public string GetSourcess()
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SourcesInfo.Sources_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyBLL.GetPageInfo(Query, Public.GetUserPrivilege());

        IList<SourcesInfo> entitys = MyBLL.GetSourcess(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (SourcesInfo entity in entitys)
            {
                jsonBuilder.Append("{\"SourcesInfo.Sources_ID\":" + entity.Sources_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Sources_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Sources_Name));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Sources_Code);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (Public.CheckPrivilege("485f0ee2-f5a3-41a0-a778-68a87c5b5d89"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\" alt=\\\"修改\\\"> <a href=\\\"sources_edit.aspx?sources_id=" + entity.Sources_ID + "\\\" title=\\\"修改\\\">修改</a>");
                }

                if (Public.CheckPrivilege("820c7d2f-a000-4122-858b-ff98a77c7eb1"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('sources_do.aspx?action=move&sources_id=" + entity.Sources_ID + "')\\\" title=\\\"删除\\\">删除</a>");
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

    public string GetSourcesName(string Sources_Code)
    {
        SourcesInfo entity = MyBLL.GetSourcesByCode(Sources_Code);
        if (entity == null)
        {
            return "";
        }
        else
        {
            return entity.Sources_Name;
        }
    }

}
