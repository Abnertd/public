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
///Help 的摘要说明
/// </summary>
public class HelpCate
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private IHelpCate MyBLL;
    public HelpCate() 
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        MyBLL = HelpFactory.CreateHelpCate();
    }

    public void AddHelpCate()
    {
        int Help_Cate_ID = tools.CheckInt(Request.Form["Help_Cate_ID"]);
        int Help_Cate_ParentID = tools.CheckInt(Request.Form["Help_Cate_ParentID"]);
        string Help_Cate_Name = tools.CheckStr(Request.Form["Help_Cate_Name"]);
        int Help_Cate_Sort = tools.CheckInt(Request.Form["Help_Cate_Sort"]);

        if (Help_Cate_Name == "") { Public.Msg("error", "错误信息", "请填写类别名称", false, "{back}"); return; }

        HelpCateInfo entity = new HelpCateInfo();
        entity.Help_Cate_ID = Help_Cate_ID;
        entity.Help_Cate_ParentID = Help_Cate_ParentID;
        entity.Help_Cate_Name = Help_Cate_Name;
        entity.Help_Cate_Sort = Help_Cate_Sort;
        entity.Help_Cate_Site = Public.GetCurrentSite();

        if (MyBLL.AddHelpCate(entity,Public.GetUserPrivilege())) {
            Public.Msg("positive", "操作成功", "操作成功", true, "help_cate_list.aspx");
        }
        else {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void EditHelpCate()
    {
        int Help_Cate_ID = tools.CheckInt(Request.Form["Help_Cate_ID"]);
        int Help_Cate_ParentID = tools.CheckInt(Request.Form["Help_Cate_ParentID"]);
        string Help_Cate_Name = tools.CheckStr(Request.Form["Help_Cate_Name"]);
        int Help_Cate_Sort = tools.CheckInt(Request.Form["Help_Cate_Sort"]);

        if (Help_Cate_Name == "") { Public.Msg("error", "错误信息", "请填写类别名称", false, "{back}"); return; }

        HelpCateInfo entity = new HelpCateInfo();
        entity.Help_Cate_ID = Help_Cate_ID;
        entity.Help_Cate_ParentID = Help_Cate_ParentID;
        entity.Help_Cate_Name = Help_Cate_Name;
        entity.Help_Cate_Sort = Help_Cate_Sort;
        entity.Help_Cate_Site = Public.GetCurrentSite();

        if (MyBLL.EditHelpCate(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "help_cate_list.aspx");
        }
        else {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void DelHelpCate()
    {
        int Help_Cate_ID = tools.CheckInt(Request.QueryString["Help_Cate_ID"]);
        if (MyBLL.DelHelpCate(Help_Cate_ID, Public.GetUserPrivilege()) > 0)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "help_cate_list.aspx");
        }
        else {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public HelpCateInfo GetHelpCateByID(int cate_id)
    {
        return MyBLL.GetHelpCateByID(cate_id, Public.GetUserPrivilege());
    }

    public string GetHelpCates()
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        string keyword = tools.CheckStr(Request["keyword"]);
        if (keyword.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "HelpCateInfo.Help_Cate_Name", "like", keyword));
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "HelpCateInfo.Help_Cate_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyBLL.GetPageInfo(Query, Public.GetUserPrivilege());

        IList<HelpCateInfo> entitys = MyBLL.GetHelpCates(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (HelpCateInfo entity in entitys)
            {
                jsonBuilder.Append("{\"HelpCateInfo.Help_Cate_ID\":" + entity.Help_Cate_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Help_Cate_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Help_Cate_Name);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Help_Cate_Sort);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (Public.CheckPrivilege("a0059a41-e628-4625-a67a-9da2f8b20fe1"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\" alt=\\\"修改\\\"> <a href=\\\"help_cate_edit.aspx?help_cate_id=" + entity.Help_Cate_ID + "\\\" title=\\\"修改\\\">修改</a>");
                }
                if (Public.CheckPrivilege("b14f283a-740b-48e1-b243-60105b87a9a6"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('help_cate_do.aspx?action=move&help_cate_id=" + entity.Help_Cate_ID + "')\\\" title=\\\"删除\\\">删除</a>");
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

    public string HelpCateOption(int selectValue)
    {
        string strHTML = "";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "HelpCateInfo.Help_Cate_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo("HelpCateInfo.Help_Cate_ID", "DESC"));
        IList<HelpCateInfo> entitys = MyBLL.GetHelpCates(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            foreach (HelpCateInfo entity in entitys)
            {
                if (entity.Help_Cate_ID == selectValue) {
                    strHTML += "<option value=\"" + entity.Help_Cate_ID + "\" selected=\"selected\">" + entity.Help_Cate_Name + "</option>";
                }
                else {
                    strHTML += "<option value=\"" + entity.Help_Cate_ID + "\">" + entity.Help_Cate_Name + "</option>";
                }
            }
        }
        return strHTML;
    }
}

public class Help
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private IHelp MyBLL;
    private HelpCate cate;
    public Help()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        MyBLL = HelpFactory.CreateHelp();
        cate = new HelpCate();
    }

    public void AddHelp()
    {
        int Help_ID = tools.CheckInt(Request.Form["Help_ID"]);
        int Help_CateID = tools.CheckInt(Request.Form["Help_CateID"]);
        int Help_IsFAQ = tools.CheckInt(Request.Form["Help_IsFAQ"]);
        int Help_IsActive = tools.CheckInt(Request.Form["Help_IsActive"]);
        string Help_Title = tools.CheckStr(Request.Form["Help_Title"]);
        int Help_Sort = tools.CheckInt(Request.Form["Help_Sort"]);
        string Help_Content = Request.Form["Help_Content"];

        if (Help_CateID == 0) { Public.Msg("error", "错误信息", "请选择类别", false, "{back}"); return; }
        if (Help_Title == "") { Public.Msg("error", "错误信息", "请填写帮助主题", false, "{back}"); return; }

        HelpInfo entity = new HelpInfo();
        entity.Help_ID = Help_ID;
        entity.Help_CateID = Help_CateID;
        entity.Help_IsFAQ = Help_IsFAQ;
        entity.Help_IsActive = Help_IsActive;
        entity.Help_Title = Help_Title;
        entity.Help_Content = Help_Content;
        entity.Help_Sort = Help_Sort;
        entity.Help_Site = Public.GetCurrentSite();

        if (MyBLL.AddHelp(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "Help_add.aspx");
        }
        else {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void EditHelp()
    {
        int Help_ID = tools.CheckInt(Request.Form["Help_ID"]);
        int Help_CateID = tools.CheckInt(Request.Form["Help_CateID"]);
        int Help_IsFAQ = tools.CheckInt(Request.Form["Help_IsFAQ"]);
        int Help_IsActive = tools.CheckInt(Request.Form["Help_IsActive"]);
        string Help_Title = tools.CheckStr(Request.Form["Help_Title"]);
        string Help_Content = Request.Form["Help_Content"];
        int Help_Sort = tools.CheckInt(Request.Form["Help_Sort"]);

        if (Help_CateID == 0) { Public.Msg("error", "错误信息", "请选择类别", false, "{back}"); return; }
        if (Help_Title == "") { Public.Msg("error", "错误信息", "请填写帮助主题", false, "{back}"); return; }

        HelpInfo entity = new HelpInfo();
        entity.Help_ID = Help_ID;
        entity.Help_CateID = Help_CateID;
        entity.Help_IsFAQ = Help_IsFAQ;
        entity.Help_IsActive = Help_IsActive;
        entity.Help_Title = Help_Title;
        entity.Help_Content = Help_Content;
        entity.Help_Sort = Help_Sort;
        entity.Help_Site = Public.GetCurrentSite();

        if (MyBLL.EditHelp(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "Help_list.aspx");
        }
        else {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void DelHelp()
    {
        int Help_ID = tools.CheckInt(Request.QueryString["Help_ID"]);
        if (MyBLL.DelHelp(Help_ID, Public.GetUserPrivilege()) > 0)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "Help_list.aspx");
        }
        else {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public HelpInfo GetHelpByID(int cate_id)
    {
        return MyBLL.GetHelpByID(cate_id, Public.GetUserPrivilege());
    }

    public string GetHelps()
    {
        string keyword = tools.CheckStr(Request["keyword"]);
        int CateID = tools.CheckInt(Request["CateID"]);
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        if (CateID > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "HelpInfo.Help_CateID", "=", CateID.ToString()));
        }
        if (keyword.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "HelpInfo.Help_Title", "like", keyword));
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "HelpInfo.Help_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyBLL.GetPageInfo(Query, Public.GetUserPrivilege());

        HelpCateInfo CateInfo;

        IList<HelpInfo> entitys = MyBLL.GetHelps(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (HelpInfo entity in entitys)
            {
                CateInfo = cate.GetHelpCateByID(entity.Help_CateID);

                jsonBuilder.Append("{\"HelpInfo.Help_ID\":" + entity.Help_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Help_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Help_Title));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (CateInfo != null) { jsonBuilder.Append(CateInfo.Help_Cate_Name); }
                else { jsonBuilder.Append(entity.Help_CateID); }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Help_Sort);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (Public.CheckPrivilege("14422eb0-8367-45e1-b955-c40aee162094"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\" alt=\\\"修改\\\"> <a href=\\\"help_edit.aspx?help_id=" + entity.Help_ID + "\\\" title=\\\"修改\\\">修改</a>");
                }
                if (Public.CheckPrivilege("c8585704-c4d5-40e8-8f5c-89940b5d7dfc"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('help_do.aspx?action=move&help_id=" + entity.Help_ID + "')\\\" title=\\\"删除\\\">删除</a>");
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

}