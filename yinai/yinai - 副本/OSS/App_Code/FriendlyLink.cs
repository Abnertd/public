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
using Glaer.Trade.B2C.BLL.SAL;

/// <summary>
///FriendlyLink 的摘要说明
/// </summary>

public class FriendlyLinkCate
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private IFriendlyLinkCate MyBLL;

    public FriendlyLinkCate()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        MyBLL = FriendlyLinkFactory.CreateFriendlyLinkCate();
    }

    public void AddFriendlyLinkCate()
    {
        int FriendlyLink_Cate_ID = tools.CheckInt(Request.Form["FriendlyLink_Cate_ID"]);
        string FriendlyLink_Cate_Name = tools.CheckStr(Request.Form["FriendlyLink_Cate_Name"]);
        int FriendlyLink_Cate_Sort = tools.CheckInt(Request.Form["FriendlyLink_Cate_Sort"]);

        if (FriendlyLink_Cate_Name == null || FriendlyLink_Cate_Name == "") {
            Public.Msg("error", "错误信息", "请填写分类名称", false, "{back}");
            return;
        }

        FriendlyLinkCateInfo entity = new FriendlyLinkCateInfo();
        entity.FriendlyLink_Cate_ID = FriendlyLink_Cate_ID;
        entity.FriendlyLink_Cate_Name = FriendlyLink_Cate_Name;
        entity.FriendlyLink_Cate_Sort = FriendlyLink_Cate_Sort;
        entity.FriendlyLink_Cate_Site = Public.GetCurrentSite();

        if (MyBLL.AddFriendlyLinkCate(entity,Public.GetUserPrivilege())) {
            Public.Msg("positive", "操作成功", "操作成功", true, "friendlylink_cate_list.aspx");
        }
        else {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void EditFriendlyLinkCate()
    {

        int FriendlyLink_Cate_ID = tools.CheckInt(Request.Form["FriendlyLink_Cate_ID"]);
        string FriendlyLink_Cate_Name = tools.CheckStr(Request.Form["FriendlyLink_Cate_Name"]);
        int FriendlyLink_Cate_Sort = tools.CheckInt(Request.Form["FriendlyLink_Cate_Sort"]);

        if (FriendlyLink_Cate_Name == null || FriendlyLink_Cate_Name == "") {
            Public.Msg("error", "错误信息", "请填写分类名称", false, "{back}");
            return;
        }

        FriendlyLinkCateInfo entity = new FriendlyLinkCateInfo();
        entity.FriendlyLink_Cate_ID = FriendlyLink_Cate_ID;
        entity.FriendlyLink_Cate_Name = FriendlyLink_Cate_Name;
        entity.FriendlyLink_Cate_Sort = FriendlyLink_Cate_Sort;
        entity.FriendlyLink_Cate_Site = Public.GetCurrentSite();

        if (MyBLL.EditFriendlyLinkCate(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "friendlylink_cate_list.aspx");
        }
        else {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void DelFriendlyLinkCate()
    {
        int FriendlyLink_Cate_ID = tools.CheckInt(Request.QueryString["FriendlyLink_Cate_ID"]);
        if (MyBLL.DelFriendlyLinkCate(FriendlyLink_Cate_ID, Public.GetUserPrivilege()) > 0)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "friendlylink_cate_list.aspx");
        }
        else {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public FriendlyLinkCateInfo GetFriendlyLinkCateByID(int cate_id)
    {
        return MyBLL.GetFriendlyLinkCateByID(cate_id, Public.GetUserPrivilege());
    }

    public string GetFriendlyLinkCates()
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        string keyword = tools.CheckStr(Request["keyword"]);
        if (keyword.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "FriendlyLinkCateInfo.FriendlyLink_Cate_Name", "like", keyword));
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "FriendlyLinkCateInfo.FriendlyLink_Cate_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyBLL.GetPageInfo(Query, Public.GetUserPrivilege());

        IList<FriendlyLinkCateInfo> entitys = MyBLL.GetFriendlyLinkCates(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (FriendlyLinkCateInfo entity in entitys)
            {
                jsonBuilder.Append("{\"FriendlyLinkCateInfo.FriendlyLink_Cate_ID\":" + entity.FriendlyLink_Cate_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.FriendlyLink_Cate_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.FriendlyLink_Cate_Name));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.FriendlyLink_Cate_Sort);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\" alt=\\\"修改\\\"> <a href=\\\"friendlylink_cate_edit.aspx?friendlylink_cate_id=" + entity.FriendlyLink_Cate_ID + "\\\" title=\\\"修改\\\">修改</a> <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('friendlylink_cate_do.aspx?action=move&friendlylink_cate_id=" + entity.FriendlyLink_Cate_ID + "')\\\" title=\\\"删除\\\">删除</a>");
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

}

public class FriendlyLink
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private IFriendlyLink MyBLL;
    private IFriendlyLinkCate MyCBLL;

    public FriendlyLink()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        MyBLL = FriendlyLinkFactory.CreateFriendlyLink();
        MyCBLL = FriendlyLinkFactory.CreateFriendlyLinkCate();
    }

    public void AddFriendlyLink()
    {
        int FriendlyLink_ID = tools.CheckInt(Request.Form["FriendlyLink_ID"]);
        int FriendlyLink_CateID = tools.CheckInt(Request.Form["FriendlyLink_CateID"]);
        string FriendlyLink_Name = tools.CheckStr(Request.Form["FriendlyLink_Name"]);
        string FriendlyLink_Img = tools.CheckStr(Request.Form["FriendlyLink_Img"]);
        string FriendlyLink_URL = tools.CheckStr(Request.Form["FriendlyLink_URL"]);
        int FriendlyLink_IsActive = tools.CheckInt(Request.Form["FriendlyLink_IsActive"]);
        int FriendlyLink_IsImg = 0;
        int FriendlyLink_Sort = tools.CheckInt(Request.Form["FriendlyLink_Sort"]);

        if (FriendlyLink_CateID == 0) { Public.Msg("error", "错误信息", "请选择所属类别", false, "{back}"); return; }
        if (FriendlyLink_Name == "") { Public.Msg("error", "错误信息", "请填写连接名称", false, "{back}"); return; }

        if (FriendlyLink_Img.Length > 0) { FriendlyLink_IsImg = 1; }

        FriendlyLinkInfo entity = new FriendlyLinkInfo();
        entity.FriendlyLink_ID = FriendlyLink_ID;
        entity.FriendlyLink_CateID = FriendlyLink_CateID;
        entity.FriendlyLink_Name = FriendlyLink_Name;
        entity.FriendlyLink_Img = FriendlyLink_Img;
        entity.FriendlyLink_URL = FriendlyLink_URL;
        entity.FriendlyLink_IsActive = FriendlyLink_IsActive;
        entity.FriendlyLink_IsImg = FriendlyLink_IsImg;
        entity.FriendlyLink_Site = Public.GetCurrentSite();
        entity.FriendlyLink_Sort = FriendlyLink_Sort;

        if (MyBLL.AddFriendlyLink(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "friendlylink_add.aspx");
        }
        else {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void EditFriendlyLink()
    {
        int FriendlyLink_ID = tools.CheckInt(Request.Form["FriendlyLink_ID"]);
        int FriendlyLink_CateID = tools.CheckInt(Request.Form["FriendlyLink_CateID"]);
        string FriendlyLink_Name = tools.CheckStr(Request.Form["FriendlyLink_Name"]);
        string FriendlyLink_Img = tools.CheckStr(Request.Form["FriendlyLink_Img"]);
        string FriendlyLink_URL = tools.CheckStr(Request.Form["FriendlyLink_URL"]);
        int FriendlyLink_IsActive = tools.CheckInt(Request.Form["FriendlyLink_IsActive"]);
        int FriendlyLink_IsImg = 0;
        int FriendlyLink_Sort = tools.CheckInt(Request.Form["FriendlyLink_Sort"]);

        if (FriendlyLink_CateID == 0) { Public.Msg("error", "错误信息", "请选择所属类别", false, "{back}"); return; }
        if (FriendlyLink_Name == "") { Public.Msg("error", "错误信息", "请填写连接名称", false, "{back}"); return; }

        if (FriendlyLink_Img.Length > 0) { FriendlyLink_IsImg = 1; }

        FriendlyLinkInfo entity = new FriendlyLinkInfo();
        entity.FriendlyLink_ID = FriendlyLink_ID;
        entity.FriendlyLink_CateID = FriendlyLink_CateID;
        entity.FriendlyLink_Name = FriendlyLink_Name;
        entity.FriendlyLink_Img = FriendlyLink_Img;
        entity.FriendlyLink_URL = FriendlyLink_URL;
        entity.FriendlyLink_IsActive = FriendlyLink_IsActive;
        entity.FriendlyLink_IsImg = FriendlyLink_IsImg;
        entity.FriendlyLink_Site = Public.GetCurrentSite();
        entity.FriendlyLink_Sort = FriendlyLink_Sort;

        if (MyBLL.EditFriendlyLink(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "friendlylink_list.aspx");
        }
        else {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void DelFriendlyLink()
    {
        int FriendlyLink_ID = tools.CheckInt(Request.QueryString["FriendlyLink_ID"]);
        if (MyBLL.DelFriendlyLink(FriendlyLink_ID, Public.GetUserPrivilege()) > 0)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "FriendlyLink_list.aspx");
        }
        else {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public FriendlyLinkInfo GetFriendlyLinkByID(int cate_id)
    {
        return MyBLL.GetFriendlyLinkByID(cate_id, Public.GetUserPrivilege());
    }

    public string GetFriendlyLinks()
    {
        FriendlyLinkCateInfo cateEntity;
        string keyword = tools.CheckStr(Request["keyword"]);
        int CateID = tools.CheckInt(Request["CateID"]);
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        if (CateID>0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "FriendlyLinkInfo.FriendlyLink_CateID", "=", CateID.ToString()));
        }
        if (keyword.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "FriendlyLinkInfo.FriendlyLink_Name", "like", keyword));
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "FriendlyLinkInfo.FriendlyLink_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyBLL.GetPageInfo(Query, Public.GetUserPrivilege());

        IList<FriendlyLinkInfo> entitys = MyBLL.GetFriendlyLinks(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (FriendlyLinkInfo entity in entitys)
            {
                jsonBuilder.Append("{\"FriendlyLinkInfo.FriendlyLink_ID\":" + entity.FriendlyLink_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.FriendlyLink_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.FriendlyLink_Name));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                cateEntity = MyCBLL.GetFriendlyLinkCateByID(entity.FriendlyLink_CateID, Public.GetUserPrivilege());
                if (cateEntity != null) { jsonBuilder.Append(Public.JsonStr(cateEntity.FriendlyLink_Cate_Name)); }
                else { jsonBuilder.Append(entity.FriendlyLink_ID); }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.FriendlyLink_Sort);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (Public.CheckPrivilege("363bfb90-0d0b-42ae-af25-54004fd061e3"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\" alt=\\\"修改\\\"> <a href=\\\"friendlylink_edit.aspx?friendlylink_id=" + entity.FriendlyLink_ID + "\\\" title=\\\"修改\\\">修改</a>");
                }

                if (Public.CheckPrivilege("be7c3360-d8c7-4343-8171-4a54a85ca5a5"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('friendlylink_do.aspx?action=move&friendlylink_id=" + entity.FriendlyLink_ID + "')\\\" title=\\\"删除\\\">删除</a>");
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

    public string CategoryOption(int selectValue)
    {
        string strHTML = "";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "FriendlyLinkCateInfo.FriendlyLink_Cate_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo("FriendlyLinkCateInfo.FriendlyLink_Cate_ID", "DESC"));
        IList<FriendlyLinkCateInfo> entitys = MyCBLL.GetFriendlyLinkCates(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            foreach (FriendlyLinkCateInfo entity in entitys)
            {
                if (entity.FriendlyLink_Cate_ID == selectValue) {
                    strHTML += "<option value=\"" + entity.FriendlyLink_Cate_ID + "\" selected=\"selected\">" + entity.FriendlyLink_Cate_Name + "</option>";
                }
                else {
                    strHTML += "<option value=\"" + entity.FriendlyLink_Cate_ID + "\">" + entity.FriendlyLink_Cate_Name + "</option>";
                }
            }
        }
        return strHTML;
    }

}
