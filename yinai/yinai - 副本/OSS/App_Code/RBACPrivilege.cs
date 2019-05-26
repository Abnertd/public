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
///RBACPrivilege 的摘要说明
/// </summary>
public class RBACPrivilege
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private IRBACPrivilege MyBLL;
    private IRBACResource MyResBLL;

    public RBACPrivilege()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        MyBLL = RBACPrivilegeFactory.CreateRBACPrivilege();
        MyResBLL = RBACResourceFactory.CreateRBACResource();
    }

    public void AddRBACPrivilege()
    {
        int RBAC_Privilege_ResourceID = tools.CheckInt(Request.Form["RBAC_Privilege_ResourceID"]);
        string RBAC_Privilege_Name = tools.CheckStr(Request.Form["RBAC_Privilege_Name"]);
        int RBAC_Privilege_IsActive = tools.CheckInt(Request.Form["RBAC_Privilege_IsActive"]);

        RBACPrivilegeInfo entity = new RBACPrivilegeInfo();
        entity.RBAC_Privilege_ID = "0";
        entity.RBAC_Privilege_ResourceID = RBAC_Privilege_ResourceID;
        entity.RBAC_Privilege_Name = RBAC_Privilege_Name;
        entity.RBAC_Privilege_IsActive = 1;
        entity.RBAC_Privilege_Addtime = DateTime.Now;

        if (MyBLL.AddRBACPrivilege(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "privilege_list.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void EditRBACPrivilege()
    {

        string RBAC_Privilege_ID = tools.CheckStr(Request.Form["RBAC_Privilege_ID"]);
        int RBAC_Privilege_ResourceID = tools.CheckInt(Request.Form["RBAC_Privilege_ResourceID"]);
        string RBAC_Privilege_Name = tools.CheckStr(Request.Form["RBAC_Privilege_Name"]);
        int RBAC_Privilege_IsActive = tools.CheckInt(Request.Form["RBAC_Privilege_IsActive"]);

        RBACPrivilegeInfo entity = GetRBACPrivilegeByID(RBAC_Privilege_ID);
        if (entity != null)
        {
            entity.RBAC_Privilege_ID = RBAC_Privilege_ID;
            entity.RBAC_Privilege_ResourceID = RBAC_Privilege_ResourceID;
            entity.RBAC_Privilege_Name = RBAC_Privilege_Name;


            if (MyBLL.EditRBACPrivilege(entity, Public.GetUserPrivilege()))
            {
                Public.Msg("positive", "操作成功", "操作成功", true, "privilege_list.aspx");
            }
            else
            {
                Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void DelRBACPrivilege()
    {
        string RBAC_Privilege_ID = tools.CheckStr(Request.QueryString["RBAC_Privilege_ID"]);
        if (MyBLL.DelRBACPrivilege(RBAC_Privilege_ID, Public.GetUserPrivilege()) > 0)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "privilege_list.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public RBACPrivilegeInfo GetRBACPrivilegeByID(string id)
    {
        return MyBLL.GetRBACPrivilegeByID(id, Public.GetUserPrivilege());
    }

    public string GetRBACPrivileges()
    {
        int RBAC_Privilege_ResourceID = tools.CheckInt(Request.QueryString["RBAC_Privilege_ResourceID"]);
        string keyword = tools.CheckStr(Request["keyword"]);
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "RBACPrivilegeInfo.RBAC_Privilege_IsActive", "=", "1"));
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        if (RBAC_Privilege_ResourceID > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "RBACPrivilegeInfo.RBAC_Privilege_ResourceID", "=", RBAC_Privilege_ResourceID.ToString()));
        }
        if (keyword != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "RBACPrivilegeInfo.RBAC_Privilege_ID", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "RBACPrivilegeInfo.RBAC_Privilege_Name", "like", keyword));
        }
        PageInfo pageinfo = MyBLL.GetPageInfo(Query, Public.GetUserPrivilege());

        RBACResourceInfo ResInfo;

        IList<RBACPrivilegeInfo> entitys = MyBLL.GetRBACPrivileges(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (RBACPrivilegeInfo entity in entitys)
            {
                ResInfo = MyResBLL.GetRBACResourceByID(entity.RBAC_Privilege_ResourceID, Public.GetUserPrivilege());

                jsonBuilder.Append("{\"RBACPrivilegeInfo.RBAC_Privilege_ID\":\"" + entity.RBAC_Privilege_ID + "\",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.RBAC_Privilege_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.RBAC_Privilege_Name);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (ResInfo != null) { jsonBuilder.Append(ResInfo.RBAC_Resource_Name); }
                else { jsonBuilder.Append(entity.RBAC_Privilege_ResourceID); }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.RBAC_Privilege_Addtime);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");

                if (Public.CheckPrivilege("51be7b46-e0f7-46dd-b0b2-a462fcb907ae"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\" alt=\\\"修改\\\"> <a href=\\\"privilege_list.aspx?action=renew&rbac_privilege_id=" + entity.RBAC_Privilege_ID + "\\\" title=\\\"修改\\\">修改</a>");
                }

                if (Public.CheckPrivilege("1030465e-7113-4db6-9b3c-da21aca07748"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('privilege_do.aspx?action=move&rbac_privilege_id=" + entity.RBAC_Privilege_ID + "')\\\" title=\\\"删除\\\">删除</a>");
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

    public string ResourceOption(int selectValue)
    {
        string strHTML = "";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "RBACResourceInfo.RBAC_Resource_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("RBACResourceInfo.RBAC_Resource_ID", "DESC"));
        IList<RBACResourceInfo> entitys = MyResBLL.GetRBACResources(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            foreach (RBACResourceInfo entity in entitys)
            {
                if (entity.RBAC_Resource_ID == selectValue)
                {
                    strHTML += "<option value=\"" + entity.RBAC_Resource_ID + "\" selected=\"selected\">" + entity.RBAC_Resource_Name + "</option>";
                }
                else
                {
                    strHTML += "<option value=\"" + entity.RBAC_Resource_ID + "\">" + entity.RBAC_Resource_Name + "</option>";
                }
            }
        }
        return strHTML;
    }

}
