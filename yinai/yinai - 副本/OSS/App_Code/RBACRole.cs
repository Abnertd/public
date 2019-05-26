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
///RBACRole 的摘要说明
/// </summary>
public class RBACRole
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private IRBACRole MyBLL;

    private IRBACResourceGroup MyResGrp;
    private IRBACResource MyRes;
    private IRBACPrivilege MyPri;

    public RBACRole()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        MyBLL = RBACRoleFactory.CreateRBACRole();

        MyResGrp = RBACResourceFactory.CreateRBACResourceGroup();
        MyRes = RBACResourceFactory.CreateRBACResource();
        MyPri = RBACPrivilegeFactory.CreateRBACPrivilege();
    }

    public void AddRBACRole()
    {
        int RBAC_Role_ID = tools.CheckInt(Request.Form["RBAC_Role_ID"]);
        string RBAC_Role_Name = tools.CheckStr(Request.Form["RBAC_Role_Name"]);
        string RBAC_Role_Description = tools.CheckStr(Request.Form["RBAC_Role_Description"]);
        int RBAC_Role_IsSystem = tools.CheckInt(Request.Form["RBAC_Role_IsSystem"]);

        string[] strPrivilege = tools.CheckStr(Request.Form["privilege_id"]).Split(',');
        IList<RBACPrivilegeInfo> privilegeList = new List<RBACPrivilegeInfo>();
        RBACPrivilegeInfo privilege;
        foreach (string privilege_id in strPrivilege)
        {
            if (privilege_id != "")
            {
                privilege = new RBACPrivilegeInfo();
                privilege.RBAC_Privilege_ID = privilege_id;
                privilegeList.Add(privilege);
                privilege = null;
            }
        }

        RBACRoleInfo entity = new RBACRoleInfo();
        entity.RBAC_Role_ID = RBAC_Role_ID;
        entity.RBAC_Role_Name = RBAC_Role_Name;
        entity.RBAC_Role_Description = RBAC_Role_Description;
        entity.RBAC_Role_IsSystem = RBAC_Role_IsSystem;
        entity.RBAC_Role_Site = "CN";
        entity.RBACPrivilegeInfos = privilegeList;

        privilegeList = null;

        if (MyBLL.AddRBACRole(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "RBAC_Role_add.aspx");
        }
        else {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void EditRBACRole()
    {
        int RBAC_Role_ID = tools.CheckInt(Request.Form["RBAC_Role_ID"]);
        string RBAC_Role_Name = tools.CheckStr(Request.Form["RBAC_Role_Name"]);
        string RBAC_Role_Description = tools.CheckStr(Request.Form["RBAC_Role_Description"]);
        int RBAC_Role_IsSystem = tools.CheckInt(Request.Form["RBAC_Role_IsSystem"]);

        string[] strPrivilege = tools.CheckStr(Request.Form["privilege_id"]).Split(',');
        IList<RBACPrivilegeInfo> privilegeList = new List<RBACPrivilegeInfo>();
        RBACPrivilegeInfo privilege;
        foreach (string privilege_id in strPrivilege)
        {
            if (privilege_id != "")
            {
                privilege = new RBACPrivilegeInfo();
                privilege.RBAC_Privilege_ID = privilege_id;
                privilegeList.Add(privilege);
                privilege = null;
            }
        }

        RBACRoleInfo entity = new RBACRoleInfo();
        entity.RBAC_Role_ID = RBAC_Role_ID;
        entity.RBAC_Role_Name = RBAC_Role_Name;
        entity.RBAC_Role_Description = RBAC_Role_Description;
        entity.RBAC_Role_IsSystem = RBAC_Role_IsSystem;
        entity.RBAC_Role_Site = "CN";
        entity.RBACPrivilegeInfos = privilegeList;

        privilegeList = null;

        if (MyBLL.EditRBACRole(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "RBAC_Role_list.aspx");
        }
        else {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void DelRBACRole()
    {
        int RBAC_Role_ID = tools.CheckInt(Request.QueryString["RBAC_Role_ID"]);
        if (MyBLL.DelRBACRole(RBAC_Role_ID, Public.GetUserPrivilege()) > 0)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "rbac_role_list.aspx");
        }
        else {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public RBACRoleInfo GetRBACRoleByID(int cate_id)
    {
        return MyBLL.GetRBACRoleByID(cate_id, Public.GetUserPrivilege());
    }

    public string GetRBACRoles()
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "RBACRoleInfo.RBAC_Role_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyBLL.GetPageInfo(Query, Public.GetUserPrivilege());

        IList<RBACRoleInfo> entitys = MyBLL.GetRBACRoles(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (RBACRoleInfo entity in entitys)
            {

                jsonBuilder.Append("{\"RBACRoleInfo.RBAC_Role_ID\":" + entity.RBAC_Role_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.RBAC_Role_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.RBAC_Role_Name);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (Public.CheckPrivilege("4df5eb30-ee06-49a4-b119-4c72e5dfaebc"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\" alt=\\\"修改\\\"> <a href=\\\"rbac_role_edit.aspx?rbac_role_id=" + entity.RBAC_Role_ID + "\\\" title=\\\"修改\\\">修改</a>");
                }
                if (Public.CheckPrivilege("3cfb485b-375e-4b4a-af15-1cf74946e333"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('rbac_role_do.aspx?action=move&rbac_role_id=" + entity.RBAC_Role_ID + "')\\\" title=\\\"删除\\\">删除</a>");
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

    public string DisplayResourceGroup(int Role_ID)
    {
        StringBuilder strHTML = new StringBuilder();

        IList<RBACPrivilegeInfo> privileges = null;
        RBACRoleInfo roleInfo = GetRBACRoleByID(Role_ID);
        if (roleInfo != null) { privileges = roleInfo.RBACPrivilegeInfos; }

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "RBACResourceGroupInfo.RBAC_ResourceGroup_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("RBACResourceGroupInfo.RBAC_ResourceGroup_ID", "DESC"));
        IList<RBACResourceGroupInfo> entitys = MyResGrp.GetRBACResourceGroups(Query, Public.GetUserPrivilege());
        Query = null;
        if (entitys != null)
        {
            strHTML.Append("<table cellspacing=\"5\" cellpadding=\"0\" border=\"0\">");
            foreach (RBACResourceGroupInfo entity in entitys)
            {
                strHTML.Append("<tr>");
                strHTML.Append("    <td style=\"font-size:14px;font-weight:bold;border-bottom:1px solid #ccc;\">" + entity.RBAC_ResourceGroup_Name + "</td>");
                strHTML.Append("</tr>");

                strHTML.Append("<tr>");
                strHTML.Append("    <td style=\"padding-left:14px;\">" + DisplayResource(entity.RBAC_ResourceGroup_ID, privileges) + "</td>");
                strHTML.Append("</tr>");
            }
            strHTML.Append("</table>");
        }
        entitys = null;
        return strHTML.ToString();
    
    }

    public string DisplayResource(int Group_ID, IList<RBACPrivilegeInfo> privileges)
    {
        StringBuilder strHTML = new StringBuilder();
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "RBACResourceInfo.RBAC_Resource_Site", "=", "CN"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "RBACResourceInfo.RBAC_Resource_GroupID", "=", Group_ID.ToString()));
        Query.OrderInfos.Add(new OrderInfo("RBACResourceInfo.RBAC_Resource_ID", "DESC"));
        IList<RBACResourceInfo> entitys = MyRes.GetRBACResources(Query, Public.GetUserPrivilege());
        Query = null;
        if (entitys != null)
        {
            strHTML.Append("<table cellspacing=\"0\" cellpadding=\"0\" border=\"0\">");
            foreach (RBACResourceInfo entity in entitys)
            {
                strHTML.Append("<tr>");
                strHTML.Append("    <td style=\"font-size:12px;font-weight:bold;border-bottom:1px solid #ccc;\">" + entity.RBAC_Resource_Name + " <input type=\"checkbox\" name=\"" + entity.RBAC_Resource_ID + "_all\" id=\"" + entity.RBAC_Resource_ID + "_all\" onclick=\"getCheckBoxSelect('" + entity.RBAC_Resource_ID + "_all');\"/> 全选</td>");
                strHTML.Append("</tr>");

                strHTML.Append("<tr>");
                strHTML.Append("    <td>" + DisplayPrivilege(entity.RBAC_Resource_ID, privileges) + "</td>");
                strHTML.Append("</tr>");
            }
            strHTML.Append("</table>");
        }
        return strHTML.ToString();

    }

    public string DisplayPrivilege(int Resource_ID, IList<RBACPrivilegeInfo> privileges)
    {
        StringBuilder strHTML = new StringBuilder();
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "RBACPrivilegeInfo.RBAC_Privilege_ResourceID", "=", Resource_ID.ToString()));
        Query.OrderInfos.Add(new OrderInfo("RBACPrivilegeInfo.RBAC_Privilege_Name", "DESC"));
        IList<RBACPrivilegeInfo> entitys = MyPri.GetRBACPrivileges(Query, Public.GetUserPrivilege());
        Query = null;
        if (entitys != null)
        {
            strHTML.Append("<table cellspacing=\"0\" cellpadding=\"0\" border=\"0\">");
            strHTML.Append("<tr>");
            strHTML.Append("    <td>");
            foreach (RBACPrivilegeInfo entity in entitys)
            {
                strHTML.Append("<input type=\"checkbox\" name=\"privilege_id\" id=\"privilege_id" + Resource_ID + "_all" + entity.RBAC_Privilege_ID + "\" value=\"" + entity.RBAC_Privilege_ID + "\" " + PrivilegeChecked(entity.RBAC_Privilege_ID, privileges) + "/>" + entity.RBAC_Privilege_Name + "&nbsp;");
            }
            strHTML.Append("    </td>");
            strHTML.Append("</tr>");
            strHTML.Append("</table>");
        }
        return strHTML.ToString();
    }


    public string PrivilegeChecked(string Privilege_ID, IList<RBACPrivilegeInfo> privilegeList)
    {
        string valExt = "";
        try
        {
            if (privilegeList != null)
            {
                foreach (RBACPrivilegeInfo entity in privilegeList)
                {
                    if (entity.RBAC_Privilege_ID == Privilege_ID)
                    {
                        valExt = "checked=\"checked\"";
                    }
                }
            }
        }
        catch (Exception ex) { throw ex; }

        return valExt;
    }

}
