using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.SQLHelper;

/// <summary>
///SCMDepot 的摘要说明
/// </summary>
public class SCMDepot
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private ISQLHelper DBHelper;

    public SCMDepot()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        DBHelper = SQLHelperFactory.CreateSQLHelper();
    }

    public void AddDepot()
    {
        int Depot_ID = tools.CheckInt(Request.Form["Depot_ID"]);
        string Depot_Name = tools.CheckStr(Request.Form["Depot_Name"]);
        int Depot_Sort = tools.CheckInt(Request.Form["Depot_Sort"]);

        if (Depot_Name.Length == 0) {
            Public.Msg("error", "错误信息", "请填写仓库名称", false, "{back}");
            return;
        }

        string SqlAdd = "INSERT INTO SCM_Depot (Depot_Name, Depot_Sort)";
        SqlAdd += "VALUES('" + Depot_Name + "', " + Depot_Sort + ")";
        try {
            DBHelper.ExecuteNonQuery(SqlAdd);
        }
        catch (Exception ex) {
            throw ex;
        }
        Public.Msg("positive", "操作成功", "操作成功", true, "depot_list.aspx");
    }

    public void EditDepot()
    {
        int Depot_ID = tools.CheckInt(Request.Form["Depot_ID"]);
        string Depot_Name = tools.CheckStr(Request.Form["Depot_Name"]);
        int Depot_Sort = tools.CheckInt(Request.Form["Depot_Sort"]);

        if (Depot_Name.Length == 0) {
            Public.Msg("error", "错误信息", "请填写仓库名称", false, "{back}");
            return;
        }

        string SqlAdd = "UPDATE SCM_Depot SET Depot_Name = '" + Depot_Name + "', Depot_Sort = " + Depot_Sort + " WHERE Depot_ID = " + Depot_ID;
        try {
            DBHelper.ExecuteNonQuery(SqlAdd);
        }
        catch (Exception ex) {
            throw ex;
        }
        Public.Msg("positive", "操作成功", "操作成功", true, "depot_list.aspx");
    }

    public void DelDepot()
    {
        int Depot_ID = tools.CheckInt(Request.QueryString["Depot_ID"]);
        try {
            DBHelper.ExecuteNonQuery("DELETE FROM SCM_Depot WHERE Depot_ID = " + Depot_ID);
        }
        catch (Exception ex) {
            throw ex;
        }
        Public.Msg("positive", "操作成功", "操作成功", true, "depot_list.aspx");
    }

    public string GetDepots()
    {
        int Depot_ID, Depot_Sort;
        string Depot_Name;

        int PageSize = tools.CheckInt(Request["rows"]);
        int CurrentPage = tools.CheckInt(Request["page"]);
        int RecordCount = 0;
        int PageCount = 0;

        string SqlField = "Depot_ID, Depot_Name, Depot_Sort";
        string SqlTable = "SCM_Depot";
        string SqlOrder = "ORDER BY " + tools.CheckStr(Request["sidx"]) + " " + tools.CheckStr(Request["sord"]);
        string SqlParam = " WHERE Depot_ID > 0";

        string SqlCount = "SELECT COUNT(Depot_ID) FROM " + SqlTable + " " + SqlParam;

        SqlDataReader RdrList = null;
        try {
            RecordCount = (int)DBHelper.ExecuteScalar(SqlCount);
            PageCount = tools.CalculatePages(RecordCount, PageSize);
            CurrentPage = tools.DeterminePage(CurrentPage, PageCount);

            string SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);

            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + CurrentPage + ",\"total\":" + PageCount + ",\"records\":" + RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            RdrList = DBHelper.ExecuteReader(SqlList);

            while (RdrList.Read()) {
                Depot_ID = tools.NullInt(RdrList["Depot_ID"]);
                Depot_Name = tools.NullStr(RdrList["Depot_Name"]);
                Depot_Sort = tools.NullInt(RdrList["Depot_Sort"]);

                jsonBuilder.Append("{\"Depot_ID\":" + Depot_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(Depot_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(Depot_Name));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Depot_Sort);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (Public.CheckPrivilege("8fad33fd-ec7f-47e4-bb94-0aca729a5e46"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\" alt=\\\"修改\\\"> <a href=\\\"depot_add.aspx?action=renew&depot_id=" + Depot_ID + "\\\" title=\\\"修改\\\">修改</a>");
                }
                if (Public.CheckPrivilege("0a198c2c-1dda-4ead-b195-c4083a7cc9fd"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('depot_do.aspx?action=move&depot_id=" + Depot_ID + "')\\\" title=\\\"删除\\\">删除</a>");
                }
                jsonBuilder.Append("\"");

                jsonBuilder.Append("]},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        catch (Exception ex) {
            throw ex;
            return "";
        }
        finally {
            if (RdrList != null) {
                RdrList.Close();
                RdrList = null;
            }
        }
    }

    public string DepotOption(int selectValue)
    {
        string strHTML = "";
        string SqlList = "SELECT Depot_ID, Depot_Name FROM SCM_Depot ORDER BY Depot_ID DESC";
        SqlDataReader RdrList = null;
        try {
            RdrList = DBHelper.ExecuteReader(SqlList);
            while (RdrList.Read()) {
                if (tools.NullInt(RdrList[0]) == selectValue) {
                    strHTML += "<option value=\"" + tools.NullInt(RdrList[0]) + "\" selected=\"selected\">" + tools.NullStr(RdrList[1]) + "</option>";
                }
                else {
                    strHTML += "<option value=\"" + tools.NullInt(RdrList[0]) + "\">" + tools.NullStr(RdrList[1]) + "</option>";
                }
            }
        }
        catch (Exception ex) {
            throw ex;
        }
        finally {
            if (RdrList != null) {
                RdrList.Close();
                RdrList = null;
            }
        }
        return strHTML;
    }

    public string GetNameByID(int ID)
    {
        string SqlList = "SELECT Depot_Name FROM SCM_Depot WHERE Depot_ID = " + ID;
        try
        {
            return tools.NullStr(DBHelper.ExecuteScalar(SqlList));
        }
        catch (Exception ex)
        {
            return "--";
        }
    }
}
