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
///SCMSupplier 的摘要说明
/// </summary>
public class SCMSupplier
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private ISQLHelper DBHelper;

    public SCMSupplier()
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

    public void AddSupplier()
    {
        int Supplier_ID = tools.CheckInt(Request.Form["Supplier_ID"]);
        string Supplier_Name = tools.CheckStr(Request.Form["Supplier_Name"]);
        string Supplier_Address = tools.CheckStr(Request.Form["Supplier_Address"]);
        string Supplier_Tel = tools.CheckStr(Request.Form["Supplier_Tel"]);
        string Supplier_Contact = tools.CheckStr(Request.Form["Supplier_Contact"]);

        if (Supplier_Name.Length == 0 || Supplier_Address.Length == 0 || Supplier_Tel.Length == 0) {
            Public.Msg("error", "错误信息", "请填写供应商名称/地址/电话", false, "{back}");
            return;
        }

        string SqlAdd = "INSERT INTO SCM_Supplier (Supplier_Name, Supplier_Address, Supplier_Tel, Supplier_Contact)";
        SqlAdd += "VALUES('" + Supplier_Name + "', '" + Supplier_Address + "', '" + Supplier_Tel + "', '" + Supplier_Contact + "')";
        try {
            DBHelper.ExecuteNonQuery(SqlAdd);
        }
        catch (Exception ex) {
            throw ex;
        }

        Public.Msg("positive", "操作成功", "操作成功", true, "supplier_list.aspx");
    }

    public void EditSupplier()
    {
        int Supplier_ID = tools.CheckInt(Request.Form["Supplier_ID"]);
        string Supplier_Name = tools.CheckStr(Request.Form["Supplier_Name"]);
        string Supplier_Address = tools.CheckStr(Request.Form["Supplier_Address"]);
        string Supplier_Tel = tools.CheckStr(Request.Form["Supplier_Tel"]);
        string Supplier_Contact = tools.CheckStr(Request.Form["Supplier_Contact"]);

        if (Supplier_Name.Length == 0 || Supplier_Address.Length == 0 || Supplier_Tel.Length == 0) {
            Public.Msg("error", "错误信息", "请填写供应商名称/地址/电话", false, "{back}");
            return;
        }

        string SqlAdd = "UPDATE SCM_Supplier SET Supplier_Name = '" + Supplier_Name + "', Supplier_Address = '" + Supplier_Address + "'";
        SqlAdd += ", Supplier_Tel = '" + Supplier_Tel + "', Supplier_Contact = '" + Supplier_Contact + "'";
        SqlAdd += "WHERE Supplier_ID=" + Supplier_ID;
        try {
            DBHelper.ExecuteNonQuery(SqlAdd);
        }
        catch (Exception ex) {
            throw ex;
        }

        Public.Msg("positive", "操作成功", "操作成功", true, "supplier_list.aspx");
    }

    public void DelSupplier()
    {
        int Supplier_ID = tools.CheckInt(Request.QueryString["Supplier_ID"]);
        try {
            DBHelper.ExecuteNonQuery("DELETE FROM SCM_Supplier WHERE Supplier_ID = " + Supplier_ID);
        }
        catch (Exception ex) {
            throw ex;
        }

        Public.Msg("positive", "操作成功", "操作成功", true, "supplier_list.aspx");
    }

    public string GetSuppliers()
    {
        int Supplier_ID;
        string Supplier_Name, Supplier_Address, Supplier_Tel, Supplier_Contact;

        int PageSize = tools.CheckInt(Request["rows"]);
        int CurrentPage = tools.CheckInt(Request["page"]);
        int RecordCount = 0;
        int PageCount = 0;

        string SqlField = "Supplier_ID, Supplier_Name, Supplier_Address, Supplier_Tel, Supplier_Contact";
        string SqlTable = "SCM_Supplier";
        string SqlOrder = "ORDER BY " + tools.CheckStr(Request["sidx"]) + " " + tools.CheckStr(Request["sord"]);
        string SqlParam = " WHERE Supplier_ID > 0";

        string SqlCount = "SELECT COUNT(Supplier_ID) FROM " + SqlTable + " " + SqlParam;

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
                Supplier_ID = tools.NullInt(RdrList["Supplier_ID"]);
                Supplier_Name = tools.NullStr(RdrList["Supplier_Name"]);
                Supplier_Address = tools.NullStr(RdrList["Supplier_Address"]);
                Supplier_Tel = tools.NullStr(RdrList["Supplier_Tel"]);
                Supplier_Contact = tools.NullStr(RdrList["Supplier_Contact"]);

                jsonBuilder.Append("{\"Supplier_ID\":" + Supplier_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(Supplier_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(Supplier_Name));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(Supplier_Address));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(Supplier_Tel));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(Supplier_Contact));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");

                if (Public.CheckPrivilege("3306c908-ff91-4e6e-8c46-8157cd5b6e4a"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\" alt=\\\"修改\\\"> <a href=\\\"supplier_add.aspx?action=renew&supplier_id=" + Supplier_ID + "\\\" title=\\\"修改\\\">修改</a>");
                }
                if (Public.CheckPrivilege("21de0894-85a2-4719-8088-774f1a815f43"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('supplier_do.aspx?action=move&supplier_id=" + Supplier_ID + "')\\\" title=\\\"删除\\\">删除</a>");
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

    public string SupplierOption(int selectValue)
    {
        string strHTML = "";
        string SqlList = "SELECT Supplier_ID, Supplier_Name FROM SCM_Supplier ORDER BY Supplier_ID DESC";
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
        string SqlList = "SELECT Supplier_Name FROM SCM_Supplier WHERE Supplier_ID = " + ID;
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
