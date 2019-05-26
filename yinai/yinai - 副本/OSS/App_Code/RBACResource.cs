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
///RBACResource 的摘要说明
/// </summary>
public class RBACResource
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private IRBACResource MyBLL;
    private IRBACResourceGroup MyGroupBLL;

    public RBACResource()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        MyBLL = RBACResourceFactory.CreateRBACResource();
        MyGroupBLL = RBACResourceFactory.CreateRBACResourceGroup();

    }

    public void AddRBACResourceGroup()
    {
        int RBAC_ResourceGroup_ID = tools.CheckInt(Request.Form["RBAC_ResourceGroup_ID"]);
        string RBAC_ResourceGroup_Name = tools.CheckStr(Request.Form["RBAC_ResourceGroup_Name"]);
        int RBAC_ResourceGroup_ParentID = tools.CheckInt(Request.Form["RBAC_ResourceGroup_ParentID"]);

        RBACResourceGroupInfo entity = new RBACResourceGroupInfo();
        entity.RBAC_ResourceGroup_ID = RBAC_ResourceGroup_ID;
        entity.RBAC_ResourceGroup_Name = RBAC_ResourceGroup_Name;
        entity.RBAC_ResourceGroup_ParentID = RBAC_ResourceGroup_ParentID;
        entity.RBAC_ResourceGroup_Site = "CN";

        if (MyGroupBLL.AddRBACResourceGroup(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "group_list.aspx");
        }
        else {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void EditRBACResourceGroup()
    {

        int RBAC_ResourceGroup_ID = tools.CheckInt(Request.Form["RBAC_ResourceGroup_ID"]);
        string RBAC_ResourceGroup_Name = tools.CheckStr(Request.Form["RBAC_ResourceGroup_Name"]);
        int RBAC_ResourceGroup_ParentID = tools.CheckInt(Request.Form["RBAC_ResourceGroup_ParentID"]);

        RBACResourceGroupInfo entity = new RBACResourceGroupInfo();
        entity.RBAC_ResourceGroup_ID = RBAC_ResourceGroup_ID;
        entity.RBAC_ResourceGroup_Name = RBAC_ResourceGroup_Name;
        entity.RBAC_ResourceGroup_ParentID = RBAC_ResourceGroup_ParentID;
        entity.RBAC_ResourceGroup_Site = "CN";


        if (MyGroupBLL.EditRBACResourceGroup(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "group_list.aspx");
        }
        else {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void DelRBACResourceGroup() {
        int RBAC_ResourceGroup_ID = tools.CheckInt(Request.QueryString["RBAC_ResourceGroup_ID"]);
        if (MyGroupBLL.DelRBACResourceGroup(RBAC_ResourceGroup_ID, Public.GetUserPrivilege()) > 0)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "group_list.aspx");
        }
        else {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public RBACResourceGroupInfo GetRBACResourceGroupByID(int cate_id) {
        return MyGroupBLL.GetRBACResourceGroupByID(cate_id, Public.GetUserPrivilege());
    }

    public string GetRBACResourceGroups()
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "RBACResourceGroupInfo.RBAC_ResourceGroup_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyGroupBLL.GetPageInfo(Query, Public.GetUserPrivilege());

        IList<RBACResourceGroupInfo> entitys = MyGroupBLL.GetRBACResourceGroups(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (RBACResourceGroupInfo entity in entitys)
            {
                jsonBuilder.Append("{\"RBACResourceGroupInfo.RBAC_ResourceGroup_ID\":" + entity.RBAC_ResourceGroup_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.RBAC_ResourceGroup_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.RBAC_ResourceGroup_Name);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\" alt=\\\"修改\\\"> <a href=\\\"group_list.aspx?action=renew&rbac_resourcegroup_id=" + entity.RBAC_ResourceGroup_ID + "\\\" title=\\\"修改\\\">修改</a> <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('group_do.aspx?action=move&rbac_resourcegroup_id=" + entity.RBAC_ResourceGroup_ID + "')\\\" title=\\\"删除\\\">删除</a>");
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

    public string ResourceGroupOption(int selectValue)
    {
        string strHTML = "";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "RBACResourceGroupInfo.RBAC_ResourceGroup_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("RBACResourceGroupInfo.RBAC_ResourceGroup_ID", "DESC"));
        IList<RBACResourceGroupInfo> entitys = MyGroupBLL.GetRBACResourceGroups(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            foreach (RBACResourceGroupInfo entity in entitys)
            {
                if (entity.RBAC_ResourceGroup_ID == selectValue) {
                    strHTML += "<option value=\"" + entity.RBAC_ResourceGroup_ID + "\" selected=\"selected\">" + entity.RBAC_ResourceGroup_Name + "</option>";
                }
                else {
                    strHTML += "<option value=\"" + entity.RBAC_ResourceGroup_ID + "\">" + entity.RBAC_ResourceGroup_Name + "</option>";
                }
            }
        }
        return strHTML;
    }

    public void AddRBACResource()
    {
        int RBAC_Resource_ID = tools.CheckInt(Request.Form["RBAC_Resource_ID"]);
        int RBAC_Resource_GroupID = tools.CheckInt(Request.Form["RBAC_Resource_GroupID"]);
        string RBAC_Resource_Name = tools.CheckStr(Request.Form["RBAC_Resource_Name"]);

        RBACResourceInfo entity = new RBACResourceInfo();
        entity.RBAC_Resource_ID = RBAC_Resource_ID;
        entity.RBAC_Resource_GroupID = RBAC_Resource_GroupID;
        entity.RBAC_Resource_Name = RBAC_Resource_Name;
        entity.RBAC_Resource_Site = "CN";

        if (MyBLL.AddRBACResource(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "resource_list.aspx");
        }
        else {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void EditRBACResource()
    {

        int RBAC_Resource_ID = tools.CheckInt(Request.Form["RBAC_Resource_ID"]);
        int RBAC_Resource_GroupID = tools.CheckInt(Request.Form["RBAC_Resource_GroupID"]);
        string RBAC_Resource_Name = tools.CheckStr(Request.Form["RBAC_Resource_Name"]);

        RBACResourceInfo entity = new RBACResourceInfo();
        entity.RBAC_Resource_ID = RBAC_Resource_ID;
        entity.RBAC_Resource_GroupID = RBAC_Resource_GroupID;
        entity.RBAC_Resource_Name = RBAC_Resource_Name;
        entity.RBAC_Resource_Site = "CN";

        if (MyBLL.EditRBACResource(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "resource_list.aspx");
        }
        else {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void DelRBACResource()
    {
        int RBAC_Resource_ID = tools.CheckInt(Request.QueryString["RBAC_Resource_ID"]);
        if (MyBLL.DelRBACResource(RBAC_Resource_ID, Public.GetUserPrivilege()) > 0)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "resource_list.aspx");
        }
        else {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public RBACResourceInfo GetRBACResourceByID(int cate_id)
    {
        return MyBLL.GetRBACResourceByID(cate_id, Public.GetUserPrivilege());
    }

    public string GetRBACResources()
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "RBACResourceInfo.RBAC_Resource_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        RBACResourceGroupInfo GroupInfo;

        PageInfo pageinfo = MyBLL.GetPageInfo(Query, Public.GetUserPrivilege());

        IList<RBACResourceInfo> entitys = MyBLL.GetRBACResources(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (RBACResourceInfo entity in entitys)
            {
                GroupInfo = MyGroupBLL.GetRBACResourceGroupByID(entity.RBAC_Resource_GroupID, Public.GetUserPrivilege());

                jsonBuilder.Append("{\"RBACResourceInfo.RBAC_Resource_ID\":" + entity.RBAC_Resource_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.RBAC_Resource_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.RBAC_Resource_Name);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (GroupInfo != null) { jsonBuilder.Append(GroupInfo.RBAC_ResourceGroup_Name); }
                else { jsonBuilder.Append(entity.RBAC_Resource_GroupID); }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\" alt=\\\"修改\\\"> <a href=\\\"resource_list.aspx?action=renew&rbac_resource_id=" + entity.RBAC_Resource_ID + "\\\" title=\\\"修改\\\">修改</a> <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('resource_do.aspx?action=move&rbac_resource_id=" + entity.RBAC_Resource_ID + "')\\\" title=\\\"删除\\\">删除</a>");
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
