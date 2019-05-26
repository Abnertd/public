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
///Notice 的摘要说明
/// </summary>
public class Notice
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private INotice MyBLL;
    NoticeCate noticeCate;

    public Notice()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        MyBLL = NoticeFactory.CreateNotice();

        noticeCate = new NoticeCate();
    }

    public void AddNotice()
    {
        int Notice_ID = tools.CheckInt(Request.Form["Notice_ID"]);
        int Notice_Cate = tools.CheckInt(Request.Form["Notice_Cate"]);
        int Notice_IsHot = tools.CheckInt(Request.Form["Notice_IsHot"]);
        int Notice_IsAudit = tools.CheckInt(Request.Form["Notice_IsAudit"]);
        int Notice_SysUserID = tools.CheckInt(Request.Form["Notice_SysUserID"]);
        int Notice_SellerID = tools.CheckInt(Request.Form["Notice_SellerID"]);
        string Notice_Title = tools.CheckStr(Request.Form["Notice_Title"]);
        string Notice_Content = Request.Form["Notice_Content"];

        if (Notice_Cate == 0) { Public.Msg("error", "错误信息", "请选择类别！", false, "{back}"); return; }
        if (Notice_Title == "") { Public.Msg("error", "错误信息", "请填写公告主题", false, "{back}"); return; }


        NoticeInfo entity = new NoticeInfo();
        entity.Notice_ID = Notice_ID;
        entity.Notice_Cate = Notice_Cate;
        entity.Notice_IsHot = Notice_IsHot;
        entity.Notice_IsAudit = Notice_IsAudit;
        entity.Notice_SysUserID = Notice_SysUserID;
        entity.Notice_SellerID = Notice_SellerID;
        entity.Notice_Title = Notice_Title;
        entity.Notice_Content = Notice_Content;
        entity.Notice_Addtime = DateTime.Now;
        entity.Notice_Site = Public.GetCurrentSite();

        if (MyBLL.AddNotice(entity,Public.GetUserPrivilege())) {
            Public.Msg("positive", "操作成功", "操作成功", true, "notice_list.aspx");
        }
        else {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void EditNotice()
    {
        int Notice_ID = tools.CheckInt(Request.Form["Notice_ID"]);
        int Notice_Cate = tools.CheckInt(Request.Form["Notice_Cate"]);
        int Notice_IsHot = tools.CheckInt(Request.Form["Notice_IsHot"]);
        int Notice_IsAudit = tools.CheckInt(Request.Form["Notice_IsAudit"]);
        int Notice_SysUserID = tools.CheckInt(Request.Form["Notice_SysUserID"]);
        int Notice_SellerID = tools.CheckInt(Request.Form["Notice_SellerID"]);
        string Notice_Title = tools.CheckStr(Request.Form["Notice_Title"]);
        string Notice_Content = Request.Form["Notice_Content"];

        if (Notice_Cate == 0) { Public.Msg("error", "错误信息", "请选择类别！", false, "{back}"); return; }
        if (Notice_Title == "") { Public.Msg("error", "错误信息", "请填写公告主题", false, "{back}"); return; }

        NoticeInfo entity = new NoticeInfo();
        entity.Notice_ID = Notice_ID;
        entity.Notice_Cate = Notice_Cate;
        entity.Notice_IsHot = Notice_IsHot;
        entity.Notice_IsAudit = Notice_IsAudit;
        entity.Notice_SysUserID = Notice_SysUserID;
        entity.Notice_SellerID = Notice_SellerID;
        entity.Notice_Title = Notice_Title;
        entity.Notice_Content = Notice_Content;
        entity.Notice_Addtime = DateTime.Now;
        entity.Notice_Site = Public.GetCurrentSite();

        if (MyBLL.EditNotice(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "notice_list.aspx");
        }
        else {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void DelNotice()
    {
        int notice_id = tools.CheckInt(Request.QueryString["notice_id"]);
        if (MyBLL.DelNotice(notice_id, Public.GetUserPrivilege()) > 0)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "notice_list.aspx");
        }
        else {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public NoticeInfo GetNoticeByID(int cate_id)
    {
        return MyBLL.GetNoticeByID(cate_id, Public.GetUserPrivilege());
    }

    public string GetNotices()
    {
        string keyword = tools.CheckStr(Request["keyword"]);
        int CateID = tools.CheckInt(Request["CateID"]);
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        if (CateID > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "NoticeInfo.Notice_Cate", "=", CateID.ToString()));
        }
        if (keyword.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "NoticeInfo.Notice_Title", "like", keyword));
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "NoticeInfo.Notice_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyBLL.GetPageInfo(Query, Public.GetUserPrivilege());

        NoticeCateInfo CateInfo;

        IList<NoticeInfo> entitys = MyBLL.GetNotices(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (NoticeInfo entity in entitys)
            {
                CateInfo = noticeCate.GetNoticeCateByID(entity.Notice_Cate);

                jsonBuilder.Append("{\"NoticeInfo.Notice_ID\":" + entity.Notice_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Notice_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Notice_Title));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");

                if (CateInfo != null) { jsonBuilder.Append(Public.JsonStr(CateInfo.Notice_Cate_Name)); }
                else { jsonBuilder.Append(entity.Notice_Cate); }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (Public.CheckPrivilege("34e5a2e1-5126-4a1f-ad23-dbe7f9e7528a"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\" alt=\\\"修改\\\"> <a href=\\\"notice_edit.aspx?notice_id=" + entity.Notice_ID + "\\\" title=\\\"修改\\\">修改</a>");
                }

                if (Public.CheckPrivilege("2c551863-a2bd-44a8-aef9-512784f0f4a0"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('notice_do.aspx?action=move&notice_id=" + entity.Notice_ID + "')\\\" title=\\\"删除\\\">删除</a>");
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
        else { return null;}

    }

}

public class NoticeCate
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private INoticeCate MyBLL;

    public NoticeCate()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        MyBLL = NoticeFactory.CreateNoticeCate();
    }

    public void AddNoticeCate()
    {
        int Notice_Cate_ID = tools.CheckInt(Request.Form["Notice_Cate_ID"]);
        string Notice_Cate_Name = tools.CheckStr(Request.Form["Notice_Cate_Name"]);
        int Notice_Cate_Sort = tools.CheckInt(Request.Form["Notice_Cate_Sort"]);

        if (Notice_Cate_Name == "") { Public.Msg("error", "错误信息", "请填写类别名称", false, "{back}"); return; }

        NoticeCateInfo entity = new NoticeCateInfo();
        entity.Notice_Cate_ID = Notice_Cate_ID;
        entity.Notice_Cate_Name = Notice_Cate_Name;
        entity.Notice_Cate_Sort = Notice_Cate_Sort;
        entity.Notice_Cate_Site = Public.GetCurrentSite();

        if (MyBLL.AddNoticeCate(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "notice_cate_list.aspx");
        }
        else {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void EditNoticeCate()
    {
        int Notice_Cate_ID = tools.CheckInt(Request.Form["Notice_Cate_ID"]);
        string Notice_Cate_Name = tools.CheckStr(Request.Form["Notice_Cate_Name"]);
        int Notice_Cate_Sort = tools.CheckInt(Request.Form["Notice_Cate_Sort"]);

        if (Notice_Cate_Name == "") { Public.Msg("error", "错误信息", "请填写类别名称", false, "{back}"); return; }

        NoticeCateInfo entity = new NoticeCateInfo();
        entity.Notice_Cate_ID = Notice_Cate_ID;
        entity.Notice_Cate_Name = Notice_Cate_Name;
        entity.Notice_Cate_Sort = Notice_Cate_Sort;
        entity.Notice_Cate_Site = Public.GetCurrentSite();

        if (MyBLL.EditNoticeCate(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "notice_cate_list.aspx");
        }
        else {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void DelNoticeCate()
    {
        int notice_cate_id = tools.CheckInt(Request.QueryString["notice_cate_id"]);
        if (MyBLL.DelNoticeCate(notice_cate_id, Public.GetUserPrivilege()) > 0)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "notice_cate_list.aspx");
        }
        else {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public NoticeCateInfo GetNoticeCateByID(int cate_id)
    {
        return MyBLL.GetNoticeCateByID(cate_id, Public.GetUserPrivilege());
    }

    public string GetNoticeCates()
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        string keyword = tools.CheckStr(Request["keyword"]);
        if (keyword.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "NoticeCateInfo.Notice_Cate_Name", "like", keyword));
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "NoticeCateInfo.Notice_Cate_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyBLL.GetPageInfo(Query, Public.GetUserPrivilege());

        IList<NoticeCateInfo> entitys = MyBLL.GetNoticeCates(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (NoticeCateInfo entity in entitys)
            {
                jsonBuilder.Append("{\"NoticeCateInfo.Notice_Cate_ID\":" + entity.Notice_Cate_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Notice_Cate_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Notice_Cate_Name));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Notice_Cate_Sort);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (Public.CheckPrivilege("705ff0e0-daa6-4649-bf27-20142c21ba9e"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\" alt=\\\"修改\\\"> <a href=\\\"notice_cate_edit.aspx?notice_cate_id=" + entity.Notice_Cate_ID + "\\\" title=\\\"修改\\\">修改</a>");
                }

                if (Public.CheckPrivilege("e2e67cd1-dd5c-4c63-962a-fdbd0d7dc6a8"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('notice_cate_do.aspx?action=move&notice_cate_id=" + entity.Notice_Cate_ID + "')\\\" title=\\\"删除\\\">删除</a>");
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

    public string NoticeCateOption(int selectValue)
    {
        string strHTML = "";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "NoticeCateInfo.Notice_Cate_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo("NoticeCateInfo.Notice_Cate_ID", "DESC"));
        IList<NoticeCateInfo> entitys = MyBLL.GetNoticeCates(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            foreach (NoticeCateInfo entity in entitys) {
                if (entity.Notice_Cate_ID == selectValue) {
                    strHTML += "<option value=\"" + entity.Notice_Cate_ID + "\" selected=\"selected\">" + entity.Notice_Cate_Name + "</option>";
                }
                else {
                    strHTML += "<option value=\"" + entity.Notice_Cate_ID + "\">" + entity.Notice_Cate_Name + "</option>";
                }
            }
        }
        return strHTML;
    }
}