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

using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.BLL.Sys;

/// <summary>
///System 的摘要说明
/// </summary>
public class Sys
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private IEncrypt encrypt;
    private IRBACUser MyBLL;
    private IRBACRole MyRoleBLL;
    

    public Sys() {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        encrypt = EncryptFactory.CreateEncrypt();
        MyBLL = RBACUserFactory.CreateRBACUser();
        MyRoleBLL = RBACRoleFactory.CreateRBACRole();
    }

    /// <summary>
    /// 创建一个用户权限实例
    /// </summary>
    /// <param name="PrivilegeCode">权限代码</param>
    /// <returns></returns>
    public RBACUserInfo CreateUserLoginPrivilege()
    {
        RBACUserInfo UserInfo = new RBACUserInfo();

        UserInfo.RBACRoleInfos = new List<RBACRoleInfo>();
        UserInfo.RBACRoleInfos.Add(new RBACRoleInfo());

        UserInfo.RBACRoleInfos[0].RBACPrivilegeInfos = new List<RBACPrivilegeInfo>();

        UserInfo.RBACRoleInfos[0].RBACPrivilegeInfos.Add(new RBACPrivilegeInfo());
        UserInfo.RBACRoleInfos[0].RBACPrivilegeInfos.Add(new RBACPrivilegeInfo());

        UserInfo.RBACRoleInfos[0].RBACPrivilegeInfos[0].RBAC_Privilege_ID = "f7fb595e-75cf-4dd2-8557-fadfa5756058";
        UserInfo.RBACRoleInfos[0].RBACPrivilegeInfos[1].RBAC_Privilege_ID = "b47f8b43-cd62-4afc-8538-9acc6ba2a762";
       
        return UserInfo;
    }

    public void login()
    {
        string verifycode, username, password, userremember;
        verifycode = tools.CheckStr(Request["verifycode"]).ToLower();
        if (Session["Trade_Verify"] ==null || verifycode == "" || verifycode != Session["Trade_Verify"].ToString()) {
            Response.Redirect("login.aspx?tip=ErrorVerifyCode");
            return;
        }
        username = tools.CheckStr(Request.Form["username"]);
        password = tools.CheckStr(Request.Form["password"]);
        password = encrypt.MD5(password);

        userremember = tools.CheckStr(Request.Form["userremember"]);

        RBACUserInfo userInfo = MyBLL.GetRBACUserByName(username, CreateUserLoginPrivilege());
        if (userInfo != null) {
            if (userInfo.RBAC_User_Password == password) {
                Session["UserLogin"] = "true";
                Session["User_ID"] = userInfo.RBAC_User_ID;
                Session["User_GroupID"] = userInfo.RBAC_User_GroupID;
                Session["User_Name"] = userInfo.RBAC_User_Name;
                Session["User_LastLogin"] = userInfo.RBAC_User_LastLogin;
                Session["User_LastLoginIP"] = userInfo.RBAC_User_LastLoginIP;
                Session["User_Addtime"] = userInfo.RBAC_User_Addtime;
                Session["User_Privilege"] = userInfo.RBACRoleInfos;
                userInfo.RBAC_User_LastLogin = DateTime.Now;
                userInfo.RBAC_User_LastLoginIP = Request.UserHostAddress;
                MyBLL.EditRBACUser(userInfo, CreateUserLoginPrivilege());

                Session["UserPrivilege"] = userInfo;

                Response.Cookies["username"].Expires = DateTime.Now.AddYears(1);
                if (userremember == "1") { Response.Cookies["username"].Value = Server.UrlEncode(username); }
                else { Response.Cookies["username"].Value = ""; }

                Response.Redirect("index.aspx");
                return;
            }
            else {
                Session["UserLogin"] = "false";
                Response.Redirect("login.aspx?tip=ErrorInfo");
                return;
            }
        }
        else {
            Session["UserLogin"] = "false";
            Response.Redirect("login.aspx?tip=ErrorInfo");
            return;
        }
    }

    public void loginout() 
    {
        Session["UserLogin"] = "false";
        Session["User_ID"] = 0;
        Session["User_GroupID"] = 0;
        Session["User_Name"] = "";
        Session["User_Addtime"] = null;

        Response.Write("<script type\"text/javascript\">");
        Response.Write("parent.location.href='/login.aspx?time='+ new Date().getTime();");
        Response.Write("</script>");
    }


    public void AddRBACUser()
    {
        int RBAC_User_ID = tools.CheckInt(Request.Form["RBAC_User_ID"]);
        int RBAC_User_GroupID = tools.CheckInt(Request.Form["RBAC_User_GroupID"]);
        string RBAC_User_Name = tools.CheckStr(Request.Form["RBAC_User_Name"]);
        string RBAC_User_Password = tools.CheckStr(Request.Form["RBAC_User_Password"]);
        string RBAC_User_Password_Confirm = tools.CheckStr(Request.Form["RBAC_User_Password_Confirm"]);

        if (RBAC_User_Name == "" || RBAC_User_Password == "") { Public.Msg("error", "错误信息", "请输入用户名或密码", false, "{back}"); return; }
        if (RBAC_User_Password != RBAC_User_Password_Confirm) { Public.Msg("error", "错误信息", "两次输入密码不一致", false, "{back}"); return; }

        string[] strRole = tools.CheckStr(Request.Form["role_id"]).Split(',');
        IList<RBACRoleInfo> roleList = new List<RBACRoleInfo>();
        RBACRoleInfo role;
        foreach (string role_id in strRole) {
            if (role_id != "") {
                role = new RBACRoleInfo();
                role.RBAC_Role_ID = int.Parse(role_id);
                roleList.Add(role);
                role = null;
            }
        }

        RBACUserInfo entity = new RBACUserInfo();
        entity.RBAC_User_ID = RBAC_User_ID;
        entity.RBAC_User_GroupID = RBAC_User_GroupID;
        entity.RBAC_User_Name = RBAC_User_Name;
        entity.RBAC_User_Password = encrypt.MD5(RBAC_User_Password);
        entity.RBAC_User_LastLogin = DateTime.Now;
        entity.RBAC_User_LastLoginIP = Request.UserHostAddress;
        entity.RBAC_User_Addtime = DateTime.Now;
        entity.RBAC_User_Site = Public.GetCurrentSite();
        entity.RBACRoleInfos = roleList;

        if (MyBLL.AddRBACUser(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "user_list.aspx");
        }
        else {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void EditRBACUser()
    {

        int RBAC_User_ID = tools.CheckInt(Request.Form["RBAC_User_ID"]);
        int RBAC_User_GroupID = tools.CheckInt(Request.Form["RBAC_User_GroupID"]);
        string RBAC_User_Password = tools.CheckStr(Request.Form["RBAC_User_Password"]);
        string RBAC_User_Password_Confirm = tools.CheckStr(Request.Form["RBAC_User_Password_Confirm"]);

        string[] strRole = tools.CheckStr(Request.Form["role_id"]).Split(',');
        IList<RBACRoleInfo> roleList = new List<RBACRoleInfo>();
        RBACRoleInfo role;
        foreach (string role_id in strRole) {
            if (role_id != "") {
                role = new RBACRoleInfo();
                role.RBAC_Role_ID = int.Parse(role_id);
                roleList.Add(role);
                role = null;
            }
        }

        RBACUserInfo entity = MyBLL.GetRBACUserByID(RBAC_User_ID, Public.GetUserPrivilege());

        if (entity == null) { Public.Msg("error", "错误信息", "该用户不存在", false, "{back}"); return; }

        if (RBAC_User_Password != "")  {
            if (RBAC_User_Password != RBAC_User_Password_Confirm) {
                Public.Msg("error", "错误信息", "两次输入密码不一致", false, "{back}"); return;
            }
            entity.RBAC_User_Password = encrypt.MD5(RBAC_User_Password);
        }

        entity.RBAC_User_ID = RBAC_User_ID;
        entity.RBAC_User_GroupID = RBAC_User_GroupID;
        entity.RBAC_User_Site = Public.GetCurrentSite();
        entity.RBACRoleInfos = roleList;

        if (MyBLL.EditRBACUser(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "user_list.aspx");
        }
        else {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void DelRBACUser()
    {
        int RBAC_User_ID = tools.CheckInt(Request.QueryString["RBAC_User_ID"]);
        if (MyBLL.DelRBACUser(RBAC_User_ID, Public.GetUserPrivilege()) > 0)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "user_list.aspx");
        }
        else {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public RBACUserInfo GetRBACUserByID(int cate_id) {
        return MyBLL.GetRBACUserByID(cate_id, Public.GetUserPrivilege());
    }

    public string GetRBACUsers()
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "RBACUserInfo.RBAC_User_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyBLL.GetPageInfo(Query, Public.GetUserPrivilege());

        IList<RBACUserInfo> entitys = MyBLL.GetRBACUsers(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (RBACUserInfo entity in entitys)
            {
                jsonBuilder.Append("{\"RBACUserInfo.RBAC_User_ID\":" + entity.RBAC_User_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.RBAC_User_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.RBAC_User_Name);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.RBAC_User_LastLogin);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.RBAC_User_LastLoginIP);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.RBAC_User_Addtime);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\" alt=\\\"修改\\\"> <a href=\\\"user_edit.aspx?rbac_user_id=" + entity.RBAC_User_ID + "\\\" title=\\\"修改\\\">修改</a> <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('user_do.aspx?action=move&rbac_user_id=" + entity.RBAC_User_ID + "')\\\" title=\\\"删除\\\">删除</a>");
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

    public string DisplayRoleCheckbox(IList<RBACRoleInfo> roles)
    {
        StringBuilder strHTML = new StringBuilder();
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "RBACRoleInfo.RBAC_Role_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo("RBACRoleInfo.RBAC_Role_ID", "DESC"));
        IList<RBACRoleInfo> entitys = MyRoleBLL.GetRBACRoles(Query, Public.GetUserPrivilege());
        Query = null;
        if (entitys != null)
        {
            strHTML.Append("<table cellspacing=\"0\" cellpadding=\"0\" border=\"0\">");
            strHTML.Append("<tr>");
            strHTML.Append("    <td>");
            foreach (RBACRoleInfo entity in entitys)
            {
                strHTML.Append("<input type=\"checkbox\" name=\"role_id\" id=\"role_id" + entity.RBAC_Role_ID + "\" value=\"" + entity.RBAC_Role_ID + "\" " + RoleChecked(entity.RBAC_Role_ID, roles) + "/>" + entity.RBAC_Role_Name + "&nbsp;");
            }
            strHTML.Append("    </td>");
            strHTML.Append("</tr>");
            strHTML.Append("</table>");
        }
        return strHTML.ToString();
    }

    public string RoleChecked(int Role_ID, IList<RBACRoleInfo> roles)
    {
        string valExt = "";
        try
        {
            if (roles != null)
            {
                foreach (RBACRoleInfo entity in roles)
                {
                    if (entity.RBAC_Role_ID == Role_ID)
                    {
                        valExt = "checked=\"checked\"";
                    }
                }
            }
        }
        catch (Exception ex) { }

        return valExt;
    }

    public void EditPassword()
    {
        string RBAC_User_Password = tools.CheckStr(Request.Form["RBAC_User_Password"]);
        string RBAC_User_Password_Confirm = tools.CheckStr(Request.Form["RBAC_User_Password_Confirm"]);

        if (RBAC_User_Password != RBAC_User_Password_Confirm) { Public.Msg("error", "错误信息", "两次输入密码不一致", false, "{back}"); return; }

        if (MyBLL.EditUserPassword(encrypt.MD5(RBAC_User_Password), (int)Session["User_ID"]))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "/main.aspx");
        }
        else 
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

}

public class RBACUserGroup
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private IEncrypt encrypt;
    private IRBACUserGroup MyBLL;

    public RBACUserGroup()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        encrypt = EncryptFactory.CreateEncrypt();
        MyBLL = RBACUserFactory.CreateRBACUserGroup();
    }


    public void AddRBACUserGroup()
    {
        int RBAC_UserGroup_ID = tools.CheckInt(Request.Form["RBAC_UserGroup_ID"]);
        string RBAC_UserGroup_Name = tools.CheckStr(Request.Form["RBAC_UserGroup_Name"]);
        int RBAC_UserGroup_ParentID = tools.CheckInt(Request.Form["RBAC_UserGroup_ParentID"]);

        RBACUserGroupInfo entity = new RBACUserGroupInfo();
        entity.RBAC_UserGroup_ID = RBAC_UserGroup_ID;
        entity.RBAC_UserGroup_Name = RBAC_UserGroup_Name;
        entity.RBAC_UserGroup_ParentID = RBAC_UserGroup_ParentID;
        entity.RBAC_UserGroup_Site = Public.GetCurrentSite();

        if (MyBLL.AddRBACUserGroup(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "usergroup_add.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void EditRBACUserGroup()
    {
        int RBAC_UserGroup_ID = tools.CheckInt(Request.Form["RBAC_UserGroup_ID"]);
        string RBAC_UserGroup_Name = tools.CheckStr(Request.Form["RBAC_UserGroup_Name"]);
        int RBAC_UserGroup_ParentID = tools.CheckInt(Request.Form["RBAC_UserGroup_ParentID"]);

        RBACUserGroupInfo entity = new RBACUserGroupInfo();
        entity.RBAC_UserGroup_ID = RBAC_UserGroup_ID;
        entity.RBAC_UserGroup_Name = RBAC_UserGroup_Name;
        entity.RBAC_UserGroup_ParentID = RBAC_UserGroup_ParentID;
        entity.RBAC_UserGroup_Site = Public.GetCurrentSite();

        if (MyBLL.EditRBACUserGroup(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "usergroup_list.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void DelRBACUserGroup()
    {
        int RBAC_UserGroup_ID = tools.CheckInt(Request.QueryString["RBAC_UserGroup_ID"]);
        if (MyBLL.DelRBACUserGroup(RBAC_UserGroup_ID, Public.GetUserPrivilege()) > 0)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "usergroup_list.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public RBACUserGroupInfo GetRBACUserGroupByID(int cate_id) {
        return MyBLL.GetRBACUserGroupByID(cate_id, Public.GetUserPrivilege());
    }

    public string GetRBACUserGroups()
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "RBACUserGroupInfo.RBAC_UserGroup_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyBLL.GetPageInfo(Query, Public.GetUserPrivilege());

        IList<RBACUserGroupInfo> entitys = MyBLL.GetRBACUserGroups(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (RBACUserGroupInfo entity in entitys)
            {
                jsonBuilder.Append("{\"RBACUserGroupInfo.RBAC_UserGroup_ID\":" + entity.RBAC_UserGroup_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.RBAC_UserGroup_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.RBAC_UserGroup_Name);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\" alt=\\\"修改\\\"> <a href=\\\"usergroup_edit.aspx?rbac_usergroup_id=" + entity.RBAC_UserGroup_ID + "\\\" title=\\\"修改\\\">修改</a> <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('usergroup_do.aspx?action=move&rbac_usergroup_id=" + entity.RBAC_UserGroup_ID + "')\\\" title=\\\"删除\\\">删除</a>");
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

    public string UserGroupOption(int selectValue)
    {
        string strHTML = "";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "RBACUserGroupInfo.RBAC_UserGroup_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo("RBACUserGroupInfo.RBAC_UserGroup_ID", "DESC"));
        IList<RBACUserGroupInfo> entitys = MyBLL.GetRBACUserGroups(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            foreach (RBACUserGroupInfo entity in entitys)
            {
                if (entity.RBAC_UserGroup_ID == selectValue) {
                    strHTML += "<option value=\"" + entity.RBAC_UserGroup_ID + "\" selected=\"selected\">" + entity.RBAC_UserGroup_Name + "</option>";
                }
                else {
                    strHTML += "<option value=\"" + entity.RBAC_UserGroup_ID + "\">" + entity.RBAC_UserGroup_Name + "</option>";
                }
            }
        }
        return strHTML;
    }

}

public class SysMenu
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private ISysMenu MyBLL;

    public SysMenu()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        MyBLL = SysMenuFactory.CreateSysMenu();
    }

    //添加菜单
    public virtual void AddSysMenu()
    {
        int Sys_Menu_ID = 0;
        int Sys_Menu_Channel = tools.CheckInt(Request.Form["Sys_Menu_Channel"]);
        string Sys_Menu_Name = tools.CheckStr(Request.Form["Sys_Menu_Name"]);
        int Sys_Menu_ParentID = tools.CheckInt(Request.Form["Sys_Menu_ParentID"]);
        string Sys_Menu_Privilege = tools.CheckStr(Request.Form["Sys_Menu_Privilege"]);
        string Sys_Menu_Icon = tools.CheckStr(Request.Form["Sys_Menu_Icon"]);
        string Sys_Menu_Url = tools.CheckStr(Request.Form["Sys_Menu_Url"]);
        int Sys_Menu_Target = tools.CheckInt(Request.Form["Sys_Menu_Target"]);

        int Sys_Menu_IsDefault = tools.CheckInt(Request.Form["Sys_Menu_IsDefault"]);
        int Sys_Menu_IsCommon = tools.CheckInt(Request.Form["Sys_Menu_IsCommon"]);
        int Sys_Menu_IsActive = tools.CheckInt(Request.Form["Sys_Menu_IsActive"]);
        int Sys_Menu_Sort = tools.CheckInt(Request.Form["Sys_Menu_Sort"]);
        string Sys_Menu_Site = Public.GetCurrentSite();

        if (Sys_Menu_Name == "")
        {
            Public.Msg("error", "错误信息", "请填写菜单项名称", false, "{back}");
        }

        SysMenuInfo entity = new SysMenuInfo();
        entity.Sys_Menu_ID = Sys_Menu_ID;
        entity.Sys_Menu_Channel = Sys_Menu_Channel;
        entity.Sys_Menu_Name = Sys_Menu_Name;
        entity.Sys_Menu_ParentID = Sys_Menu_ParentID;
        entity.Sys_Menu_Privilege = Sys_Menu_Privilege;
        entity.Sys_Menu_Icon = Sys_Menu_Icon;
        entity.Sys_Menu_Url = Sys_Menu_Url;
        entity.Sys_Menu_Target = Sys_Menu_Target;
        entity.Sys_Menu_IsSystem = 0;
        entity.Sys_Menu_IsDefault = Sys_Menu_IsDefault;
        entity.Sys_Menu_IsCommon = Sys_Menu_IsCommon;
        entity.Sys_Menu_IsActive = Sys_Menu_IsActive;
        entity.Sys_Menu_Sort = Sys_Menu_Sort;
        entity.Sys_Menu_Site = Sys_Menu_Site;

        if (MyBLL.AddSysMenu(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "Sys_Menu_add.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    //栏目选择
    public virtual void Select_Channel(int Channel_ID)
    { 
        int i=1;
        string channel_name = "交易管理,商品管理,采购商管理,营销推广,统计报表,内容管理,进销存,系统管理,供应商管理,商机管理";
        Response.Write("<select name=\"Sys_Menu_Channel\">");
        foreach (string substr in channel_name.Split(','))
        {
            if (substr.Length > 0)
            {
                if (Channel_ID == i)
                {
                    Response.Write("<option value=\"" + i + "\" selected>" + substr + "</option>");
                }
                else
                {
                    Response.Write("<option value=\"" + i + "\">" + substr + "</option>");
                }
            }
            i=i+1;
        }
          
          Response.Write("</select>");
    }

    //栏目选择
    public virtual void Select_Menu_Channel(int Channel_ID)
    {
        int i = 1;
        string channel_name = "交易管理,商品管理,采购商管理,营销推广,统计报表,内容管理,进销存,系统管理,供应商管理,商机管理";
        Response.Write("<select name=\"Sys_Menu_Channel\" onchange=\"$('#menu_div').load('sys_menu_do.aspx?action=changemenu&channel='+$(this).val()+'&timer='+Math.random())\">");
        foreach (string substr in channel_name.Split(','))
        {
            if (substr.Length > 0)
            {
                if (Channel_ID == i)
                {
                    Response.Write("<option value=\"" + i + "\" selected>" + substr + "</option>");
                }
                else
                {
                    Response.Write("<option value=\"" + i + "\">" + substr + "</option>");
                }
            }
            i = i + 1;
        }

        Response.Write("</select>");
    }

    //所属菜单选择
    public virtual void Select_Menu_Parent(string Select_Name, int Parent_ID, int Channel_ID)
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SysMenuInfo.Sys_Menu_Site", "=", Public.GetCurrentSite()));
        if (Channel_ID > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "SysMenuInfo.Sys_Menu_Channel", "=", Channel_ID.ToString()));
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SysMenuInfo.Sys_Menu_ParentID", "=", "0"));
        Query.OrderInfos.Add(new OrderInfo("SysMenuInfo.Sys_Menu_ID", "Desc"));
        Response.Write("<select name=\"" + Select_Name + "\">");
        Response.Write("<option value=\"0\">请选择</option>");
        IList<SysMenuInfo> entitys = MyBLL.GetSysMenus(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            foreach (SysMenuInfo entity in entitys)
            {
                if (Parent_ID == entity.Sys_Menu_ID)
                {
                    Response.Write("<option value=\"" + entity.Sys_Menu_ID + "\" selected>" + entity.Sys_Menu_Name + "</option>");
                }
                else
                {
                    Response.Write("<option value=\"" + entity.Sys_Menu_ID + "\">" + entity.Sys_Menu_Name + "</option>");
                }
            }
        }
        Response.Write("</select>");
    }

    //获取栏目名称
    public virtual string Get_Channel_Name(int Channel_ID)
    { 
        string channel_name="";
        switch (Channel_ID)
        {
            case 1:
                channel_name = "交易管理";
                break;
            case 2:
                channel_name="商品管理";
                break;
            case 3:
                channel_name = "采购商管理";
                break;
            case 4:
                channel_name="营销推广";
                break;
            case 5:
                channel_name="统计报表";
                break;
            case 6:
                channel_name="内容管理";
                break;
            case 7:
                channel_name="进销存";
                break;
            case 8:
                channel_name="系统管理";
                break;
            case 9:
                channel_name = "供应商管理";
                break;
            case 10:
                channel_name = "商机管理";
                break;
            
        }
        return channel_name;
    }

    //菜单列表
    public string GetSysMenus()
    {
        int channel_id;
        channel_id = tools.CheckInt(Request["channel_id"]);
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);

        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SysMenuInfo.Sys_Menu_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SysMenuInfo.Sys_Menu_IsSystem", "=", "0"));
        if (channel_id > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "SysMenuInfo.Sys_Menu_Channel", "=", channel_id.ToString()));
        }
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyBLL.GetPageInfo(Query, Public.GetUserPrivilege());
        SysMenuInfo menuinfo;
        IList<SysMenuInfo> entitys = MyBLL.GetSysMenus(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (SysMenuInfo entity in entitys)
            {
                menuinfo = MyBLL.GetSysMenuByID(entity.Sys_Menu_ParentID,Public.GetUserPrivilege());
                jsonBuilder.Append("{\"SysMenuInfo.Sys_Menu_ID\":" + entity.Sys_Menu_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Sys_Menu_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Sys_Menu_Name));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(Get_Channel_Name(entity.Sys_Menu_Channel)));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (menuinfo != null)
                {
                    jsonBuilder.Append(Public.JsonStr(menuinfo.Sys_Menu_Name));
                }
                else
                {
                    jsonBuilder.Append("");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Sys_Menu_Privilege);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Sys_Menu_Url);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.Sys_Menu_Target == 0)
                {
                    jsonBuilder.Append("框架内");
                }
                else
                {
                    jsonBuilder.Append("新窗口");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.Sys_Menu_IsDefault == 0)
                {
                    jsonBuilder.Append("否");
                }
                else
                {
                    jsonBuilder.Append("是");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.Sys_Menu_IsCommon == 0)
                {
                    jsonBuilder.Append("否");
                }
                else
                {
                    jsonBuilder.Append("是"); 
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.Sys_Menu_IsActive == 0)
                {
                    jsonBuilder.Append("否");
                }
                else
                {
                    jsonBuilder.Append("是");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Sys_Menu_Sort);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (Public.CheckPrivilege("c9ce4dd0-6391-4fb9-aa99-f37c23c04a8a")&&entity.Sys_Menu_IsSystem==0)
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\"> <a href=\\\"sys_menu_edit.aspx?menu_id=" + entity.Sys_Menu_ID + "\\\" title=\\\"修改\\\">修改</a>");
                }
                if (Public.CheckPrivilege("e5e043cc-5085-41f9-b406-808c319b3a70") && entity.Sys_Menu_IsSystem == 0)
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('sys_menu_do.aspx?action=move&menu_id=" + entity.Sys_Menu_ID + "')\\\" title=\\\"删除\\\">删除</a>");
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

    //根据编号获取菜单信息
    public SysMenuInfo GetSysMenuByID(int ID)
    {
        return MyBLL.GetSysMenuByID(ID, Public.GetUserPrivilege());
    }

    //修改菜单
    public virtual void EditSysMenu()
    {
        int Sys_Menu_ID = tools.CheckInt(Request.Form["Sys_Menu_ID"]);
        int Sys_Menu_Channel = tools.CheckInt(Request.Form["Sys_Menu_Channel"]);
        string Sys_Menu_Name = tools.CheckStr(Request.Form["Sys_Menu_Name"]);
        int Sys_Menu_ParentID = tools.CheckInt(Request.Form["Sys_Menu_ParentID"]);
        string Sys_Menu_Privilege = tools.CheckStr(Request.Form["Sys_Menu_Privilege"]);
        string Sys_Menu_Icon = tools.CheckStr(Request.Form["Sys_Menu_Icon"]);
        string Sys_Menu_Url = tools.CheckStr(Request.Form["Sys_Menu_Url"]);
        int Sys_Menu_Target = tools.CheckInt(Request.Form["Sys_Menu_Target"]);
        int Sys_Menu_IsDefault = tools.CheckInt(Request.Form["Sys_Menu_IsDefault"]);
        int Sys_Menu_IsCommon = tools.CheckInt(Request.Form["Sys_Menu_IsCommon"]);
        int Sys_Menu_IsActive = tools.CheckInt(Request.Form["Sys_Menu_IsActive"]);
        int Sys_Menu_Sort = tools.CheckInt(Request.Form["Sys_Menu_Sort"]);
        string Sys_Menu_Site = Public.GetCurrentSite();

        if (Sys_Menu_Name == "")
        {
            Public.Msg("error", "错误信息", "请填写菜单项名称", false, "{back}");
        }

        SysMenuInfo entity = new SysMenuInfo();
        entity.Sys_Menu_ID = Sys_Menu_ID;
        entity.Sys_Menu_Channel = Sys_Menu_Channel;
        entity.Sys_Menu_Name = Sys_Menu_Name;
        entity.Sys_Menu_ParentID = Sys_Menu_ParentID;
        entity.Sys_Menu_Privilege = Sys_Menu_Privilege;
        entity.Sys_Menu_Icon = Sys_Menu_Icon;
        entity.Sys_Menu_Url = Sys_Menu_Url;
        entity.Sys_Menu_Target = Sys_Menu_Target;
        entity.Sys_Menu_IsSystem = 0;
        entity.Sys_Menu_IsDefault = Sys_Menu_IsDefault;
        entity.Sys_Menu_IsCommon = Sys_Menu_IsCommon;
        entity.Sys_Menu_IsActive = Sys_Menu_IsActive;
        entity.Sys_Menu_Sort = Sys_Menu_Sort;
        entity.Sys_Menu_Site = Sys_Menu_Site;

        if (MyBLL.EditSysMenu(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "Sys_Menu_list.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    //删除菜单项
    public void DelSysMenu()
    {
        int Sys_Menu_ID = tools.CheckInt(Request.QueryString["menu_id"]);
        if (MyBLL.DelSysMenu(Sys_Menu_ID, Public.GetUserPrivilege()) > 0)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "Sys_Menu_list.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void Sys_Menu_Display(int Channel_ID)
    {
        string Menu_Item;
        string menu_target;
        string default_css;
        bool Menu_Display = false;
        int num = 0;
        Response.Write("<table width=\"190\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" id=\"left_menu\">");
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SysMenuInfo.Sys_Menu_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SysMenuInfo.Sys_Menu_IsActive", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SysMenuInfo.Sys_Menu_ParentID", "=", "0"));
        if (Channel_ID > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "SysMenuInfo.Sys_Menu_Channel", "=", Channel_ID.ToString()));
        }
        else
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "SysMenuInfo.Sys_Menu_IsCommon", "=", "1"));
        }
        Query.OrderInfos.Add(new OrderInfo("SysMenuInfo.Sys_Menu_Sort", "Asc"));
        IList<SysMenuInfo> entitys = MyBLL.GetSysMenus(Query, Public.GetUserPrivilege());
        IList<SysMenuInfo> entity_sub;
        if (entitys != null)
        {
            foreach (SysMenuInfo entity in entitys)
            {
                num = num + 1;
                Menu_Item = "";
                Menu_Display = false;
                Menu_Item = Menu_Item + "<tr>";
                Menu_Item = Menu_Item + "<td align=\"left\" class=\"menu_title\" onclick=\"menu_fold('menu_" + entity.Sys_Menu_ID + "');\"  id=\"menu_" + entity.Sys_Menu_ID + "\"><img src=\"/Images/" + entity.Sys_Menu_Icon + "\"/>" + entity.Sys_Menu_Name + "</td>";
                Menu_Item = Menu_Item + "</tr>";
                Menu_Item = Menu_Item + "<tr>";
                Menu_Item = Menu_Item + "<td height=\"5\"></td>";
                Menu_Item = Menu_Item + "</tr>";
                if (num == 1)
                {
                    Menu_Item = Menu_Item + "<tr class=\"a1\" id=\"menu_" + entity.Sys_Menu_ID + "_list\">";
                }
                else
                {
                    Menu_Item = Menu_Item + "<tr class=\"a2\" style=\"display:none;\" id=\"menu_" + entity.Sys_Menu_ID + "_list\">";
                }
                Menu_Item = Menu_Item + "<td>";
                Menu_Item = Menu_Item + "<table width=\"190\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\">";
                entity_sub = MyBLL.GetSysMenusSub(entity.Sys_Menu_ID, Public.GetUserPrivilege());
                if (entity_sub != null)
                {
                    foreach (SysMenuInfo ent in entity_sub)
                    {

                        if ((Public.CheckPrivilege(ent.Sys_Menu_Privilege) || ent.Sys_Menu_Privilege == "") && ent.Sys_Menu_IsActive == 1)
                        {
                            if ((Channel_ID == 0 && ent.Sys_Menu_IsCommon == 1) || Channel_ID > 0)
                            {
                                Menu_Display = true;
                                Menu_Item = Menu_Item + "<tr>";
                                if (ent.Sys_Menu_Target == 0)
                                {
                                    menu_target = "main";
                                }
                                else
                                {
                                    menu_target = "_blank";
                                }
                                if (ent.Sys_Menu_IsDefault == 0 || Channel_ID == 0)
                                {
                                    default_css = "menu_item";
                                }
                                else
                                {
                                    default_css = "menu_itemon";
                                }
                                if (ent.Sys_Menu_Url.IndexOf("?") > 0)
                                {
                                    Menu_Item = Menu_Item + "  <td align=\"left\" class=\"" + default_css + "\"><a href=\"" + ent.Sys_Menu_Url + "&menu_id=" + ent.Sys_Menu_ID + "\" onclick=\"change_menu($(this));\" target=\"" + menu_target + "\">" + ent.Sys_Menu_Name + "</a></td>";
                                }
                                else
                                {
                                    Menu_Item = Menu_Item + "  <td align=\"left\" class=\"" + default_css + "\"><a href=\"" + ent.Sys_Menu_Url + "?menu_id=" + ent.Sys_Menu_ID + "\" onclick=\"change_menu($(this));\" target=\"" + menu_target + "\">" + ent.Sys_Menu_Name + "</a></td>";
                                }
                                Menu_Item = Menu_Item + "</tr>";
                            }
                        }
                    }
                }
                Menu_Item = Menu_Item + "</table>";
                Menu_Item = Menu_Item + "</td>";
                Menu_Item = Menu_Item + "</tr>";
                if (Menu_Display)
                {
                    Response.Write(Menu_Item);
                }
            }
        }
        Response.Write("</table>");
    }


    public bool Sys_Menu_Display_Top(int Channel_ID)
    {
        bool flag = false;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SysMenuInfo.Sys_Menu_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SysMenuInfo.Sys_Menu_IsActive", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SysMenuInfo.Sys_Menu_ParentID", "=", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SysMenuInfo.Sys_Menu_Channel", "=", Channel_ID.ToString()));
        Query.OrderInfos.Add(new OrderInfo("SysMenuInfo.Sys_Menu_Sort", "Asc"));
        IList<SysMenuInfo> entitys = MyBLL.GetSysMenus(Query, Public.GetUserPrivilege());
        IList<SysMenuInfo> entity_sub;
        if (entitys != null)
        {
            foreach (SysMenuInfo entity in entitys)
            {
                entity_sub = MyBLL.GetSysMenusSub(entity.Sys_Menu_ID, Public.GetUserPrivilege());
                if (entity_sub != null)
                {
                    foreach (SysMenuInfo ent in entity_sub)
                    {
                        if (Public.CheckPrivilege(ent.Sys_Menu_Privilege)&&ent.Sys_Menu_ParentID!=0 && ent.Sys_Menu_IsActive == 1)
                        {
                            flag = true;
                            break;
                        }
                    }
                }
                if (flag == true)
                    break;
            }
        }
        return flag;
 
    }

    public virtual string Page_Menu_Title(int Menu_ID)
    {
        string menu_title = "";
        SysMenuInfo entity = GetSysMenuByID(Menu_ID);
        if (entity != null)
        {
            menu_title = entity.Sys_Menu_Name;

            SysMenuInfo parent = GetSysMenuByID(entity.Sys_Menu_ParentID);
            if (parent != null)
            {
                menu_title = parent.Sys_Menu_Name + " > " + menu_title;

                menu_title = Get_Channel_Name(parent.Sys_Menu_Channel) + " > " + menu_title;
            }
            else
            {
                menu_title = Get_Channel_Name(entity.Sys_Menu_Channel) + " > " + menu_title;
            }
        }

        return menu_title;
    }
}

public class SysState
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private ISysState MyBLL;

    public SysState()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        MyBLL = SysStateFactory.CreateSysState();
    }

    public string GetSysStates()
    {
        string keyword = tools.CheckStr(Request["keyword"]);
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        if (keyword != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "SysStateInfo.Sys_State_CN", "%like%", keyword));
        }
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyBLL.GetPageInfo(Query);

        IList<SysStateInfo> entitys = MyBLL.GetSysStates(Query);
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (SysStateInfo entity in entitys)
            {
                jsonBuilder.Append("{\"SysStateInfo.Sys_State_ID\":" + entity.Sys_State_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Sys_State_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Sys_State_CN);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Sys_State_Code);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\" alt=\\\"修改\\\"> <a href=\\\"sys_state_edit.aspx?state_id=" + entity.Sys_State_ID + "\\\" title=\\\"修改\\\">修改</a> <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('sys_state_do.aspx?action=statemove&Sys_State_ID=" + entity.Sys_State_ID + "')\\\" title=\\\"删除\\\">删除</a>");
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

    public SysStateInfo GetSysStateByID(int ID)
    {
        return MyBLL.GetSysStateByID(ID);
    }


    public virtual void AddSysState()
    {
        string Sys_State_CN = tools.CheckStr(Request.Form["Sys_State_CN"]);
        int Sys_State_IsActive = tools.CheckInt(Request.Form["Sys_State_IsActive"]);

        SysStateInfo entity = new SysStateInfo();
        entity.Sys_State_CN = Sys_State_CN;
        entity.Sys_State_IsActive = Sys_State_IsActive;

        if (MyBLL.AddSysState(entity))
        {
            
            Public.Msg("positive", "操作成功", "操作成功", true, "Sys_State_add.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual void EditSysState()
    {

        int Sys_State_ID = tools.CheckInt(Request.Form["Sys_State_ID"]);
        string Sys_State_CN = tools.CheckStr(Request.Form["Sys_State_CN"]);
        int Sys_State_IsActive = tools.CheckInt(Request.Form["Sys_State_IsActive"]);

        SysStateInfo entity = GetSysStateByID(Sys_State_ID);
        if (entity != null)
        {
            entity.Sys_State_ID = Sys_State_ID;
            entity.Sys_State_CountryCode = "1";
            entity.Sys_State_CN = Sys_State_CN;
            entity.Sys_State_IsActive = Sys_State_IsActive;
        }

        if (MyBLL.EditSysState(entity))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "Sys_State_list.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual void DelSysState() {
        int Sys_State_ID = tools.CheckInt(Request.QueryString["Sys_State_ID"]);
        if (MyBLL.DelSysState(Sys_State_ID) > 0)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "Sys_State_list.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void getSelectStates(string code)
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SysStateInfo.Sys_State_IsActive", "=", "1"));
        Query.OrderInfos.Add(new OrderInfo("SysStateInfo.Sys_State_ID", "DESC"));
        IList<SysStateInfo> entitys = MyBLL.GetSysStates(Query);
        if (entitys != null)
        {
            Response.Write("<select name=\"Sys_City_StateCode\"><option value=\"0\">请选择省份</option>");
            foreach (SysStateInfo entity in entitys)
            {
                if (code == entity.Sys_State_Code)
                {
                    Response.Write("<option value=\"" + entity.Sys_State_Code + "\" selected>" + entity.Sys_State_CN + "</option>");
                }
                else
                {
                    Response.Write("<option value=\"" + entity.Sys_State_Code + "\">" + entity.Sys_State_CN + "</option>");
                }
            }
            Response.Write("</select>");
        }

    }

}



public class SysCity
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private ISysCity MyBLL;
    private SysState mystate;

    public SysCity()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        MyBLL = SysCityFactory.CreateSysCity();
        mystate = new SysState();
    }

    public string GetSysCitys()
    {
        string keyword = tools.CheckStr(Request["keyword"]);
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        if (keyword != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "SysCityInfo.Sys_City_CN", "%like%", keyword));
        }
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyBLL.GetPageInfo(Query);

        IList<SysCityInfo> entitys = MyBLL.GetSysCitys(Query);
        if (entitys != null)
        {
            SysStateInfo sysstate = null;
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (SysCityInfo entity in entitys)
            {
                jsonBuilder.Append("{\"SysCityInfo.Sys_City_ID\":" + entity.Sys_City_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Sys_City_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Sys_City_CN);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Sys_City_Code);
                jsonBuilder.Append("\",");

                sysstate = mystate.GetSysStateByID(tools.NullInt(entity.Sys_City_StateCode));
                jsonBuilder.Append("\"");
                if (sysstate != null)
                {
                    jsonBuilder.Append(sysstate.Sys_State_CN);
                }
                else
                {
                    jsonBuilder.Append("--");
                }
                
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\" alt=\\\"修改\\\"> <a href=\\\"sys_city_edit.aspx?city_id=" + entity.Sys_City_ID + "\\\" title=\\\"修改\\\">修改</a> <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('sys_state_do.aspx?action=citymove&Sys_City_ID=" + entity.Sys_City_ID + "')\\\" title=\\\"删除\\\">删除</a>");
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

    public virtual void AddSysCity()
    {
        int Sys_City_ID = tools.CheckInt(Request.Form["Sys_City_ID"]);
        string Sys_City_StateCode = tools.CheckStr(Request.Form["Sys_City_StateCode"]);
        string Sys_City_CN = tools.CheckStr(Request.Form["Sys_City_CN"]);
        int Sys_City_IsActive = tools.CheckInt(Request.Form["Sys_City_IsActive"]);

        SysCityInfo entity = new SysCityInfo();
        entity.Sys_City_ID = Sys_City_ID;
        entity.Sys_City_StateCode = Sys_City_StateCode;
        entity.Sys_City_CN = Sys_City_CN;
        entity.Sys_City_IsActive = Sys_City_IsActive;

        if (MyBLL.AddSysCity(entity))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "Sys_City_add.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual void EditSysCity()
    {

        int Sys_City_ID = tools.CheckInt(Request.Form["Sys_City_ID"]);
        string Sys_City_StateCode = tools.CheckStr(Request.Form["Sys_City_StateCode"]);
        string Sys_City_CN = tools.CheckStr(Request.Form["Sys_City_CN"]);
        int Sys_City_IsActive = tools.CheckInt(Request.Form["Sys_City_IsActive"]);

        SysCityInfo entity = MyBLL.GetSysCityByID(Sys_City_ID);
        if (entity != null)
        {
            entity.Sys_City_ID = Sys_City_ID;
            entity.Sys_City_StateCode = Sys_City_StateCode;
            entity.Sys_City_CN = Sys_City_CN;
            entity.Sys_City_IsActive = Sys_City_IsActive;
        }

        if (MyBLL.EditSysCity(entity))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "Sys_City_list.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual void DelSysCity()
    {
        int Sys_City_ID = tools.CheckInt(Request.QueryString["Sys_City_ID"]);
        if (MyBLL.DelSysCity(Sys_City_ID) > 0)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "Sys_City_list.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual SysCityInfo GetSysCityByID(int cate_id)
    {
        return MyBLL.GetSysCityByID(cate_id);
    }


    public void getSelectCitys(string code)
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SysCityInfo.Sys_City_IsActive", "=", "1"));
        Query.OrderInfos.Add(new OrderInfo("SysCityInfo.Sys_City_ID", "DESC"));
        IList<SysCityInfo> entitys = MyBLL.GetSysCitys(Query);
        if (entitys != null)
        {
            Response.Write("<select name=\"Sys_County_CityCode\"><option value=\"0\">请选择城市</option>");
            foreach (SysCityInfo entity in entitys)
            {
                if (code == entity.Sys_City_Code)
                {
                    Response.Write("<option value=\"" + entity.Sys_City_Code + "\" selected>" + entity.Sys_City_CN + "</option>");
                }
                else
                {
                    Response.Write("<option value=\"" + entity.Sys_City_Code + "\">" + entity.Sys_City_CN + "</option>");
                }
            }
            Response.Write("</select>");
        }

    }

}



public class SysCounty
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private ISysCounty MyBLL;
    private SysCity mycity;

    public SysCounty()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        MyBLL = SysCountyFactory.CreateSysCounty();
        mycity = new SysCity();
    }

    public string GetSysCountys()
    {
        string keyword = tools.CheckStr(Request["keyword"]);
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        if (keyword != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "SysCountyInfo.Sys_County_CN", "%like%", keyword));
        }
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyBLL.GetPageInfo(Query);

        IList<SysCountyInfo> entitys = MyBLL.GetSysCountys(Query);
        if (entitys != null)
        {
            SysCityInfo syscity = null;
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (SysCountyInfo entity in entitys)
            {
                jsonBuilder.Append("{\"SysCountyInfo.Sys_County_ID\":" + entity.Sys_County_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Sys_County_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Sys_County_CN);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Sys_County_Code);
                jsonBuilder.Append("\",");

                syscity = mycity.GetSysCityByID(tools.NullInt(entity.Sys_County_CityCode));
                jsonBuilder.Append("\"");
                if (syscity != null)
                {
                    jsonBuilder.Append(syscity.Sys_City_CN);
                }
                else
                {
                    jsonBuilder.Append("--");
                }

                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\" alt=\\\"修改\\\"> <a href=\\\"sys_county_edit.aspx?county_id=" + entity.Sys_County_ID + "\\\" title=\\\"修改\\\">修改</a> <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('sys_state_do.aspx?action=countymove&Sys_County_ID=" + entity.Sys_County_ID + "')\\\" title=\\\"删除\\\">删除</a>");
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

    public virtual void AddSysCounty()
    {
        string Sys_County_CityCode = tools.CheckStr(Request.Form["Sys_County_CityCode"]);
        string Sys_County_CN = tools.CheckStr(Request.Form["Sys_County_CN"]);
        int Sys_County_IsActive = tools.CheckInt(Request.Form["Sys_County_IsActive"]);

        SysCountyInfo entity = new SysCountyInfo();
        entity.Sys_County_CityCode = Sys_County_CityCode;
        entity.Sys_County_CN = Sys_County_CN;
        entity.Sys_County_IsActive = Sys_County_IsActive;

        if (MyBLL.AddSysCounty(entity))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "Sys_County_add.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual void EditSysCounty()
    {
        int Sys_County_ID = tools.CheckInt(Request.Form["Sys_County_ID"]);
        string Sys_County_CityCode = tools.CheckStr(Request.Form["Sys_County_CityCode"]);
        string Sys_County_CN = tools.CheckStr(Request.Form["Sys_County_CN"]);
        int Sys_County_IsActive = tools.CheckInt(Request.Form["Sys_County_IsActive"]);

        SysCountyInfo entity = MyBLL.GetSysCountyByID(Sys_County_ID);
        if (entity != null)
        {
            entity.Sys_County_CityCode = Sys_County_CityCode;
            entity.Sys_County_CN = Sys_County_CN;
            entity.Sys_County_IsActive = Sys_County_IsActive;
        }

        if (MyBLL.EditSysCounty(entity))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "Sys_County_list.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual void DelSysCounty()
    {
        int Sys_County_ID = tools.CheckInt(Request.QueryString["Sys_County_ID"]);
        if (MyBLL.DelSysCounty(Sys_County_ID) > 0)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "Sys_County_list.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual SysCountyInfo GetSysCountyByID(int cate_id)
    {
        return MyBLL.GetSysCountyByID(cate_id);
    }





}

