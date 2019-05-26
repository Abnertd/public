using System;
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
/// 进销存配置信息
/// </summary>
public class SCMConfig
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private ISQLHelper DBHelper;

    public SCMConfig()
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

    public void Config_Edit()
    {
        int Config_ID, Config_DefaultSupplier, Config_DefaultDepot, Config_AlertAmount;
        Config_ID = tools.CheckInt(Request.Form["Config_ID"]);
        Config_DefaultSupplier = tools.CheckInt(Request.Form["Config_DefaultSupplier"]);
        Config_DefaultDepot = tools.CheckInt(Request.Form["Config_DefaultDepot"]);
        Config_AlertAmount = tools.CheckInt(Request.Form["Config_AlertAmount"]);

        string SqlAdd = "SELECT * FROM SCM_Config WHERE Config_ID = " + Config_ID;
        DataTable DtAdd = null;
        DataRow DrAdd = null;
        try {
            DtAdd = DBHelper.Query(SqlAdd);
            if (DtAdd.Rows.Count > 0) {
                DrAdd = DtAdd.Rows[0];
                DrAdd["Config_DefaultSupplier"] = Config_DefaultSupplier;
                DrAdd["Config_DefaultDepot"] = Config_DefaultDepot;
                DrAdd["Config_AlertAmount"] = Config_AlertAmount;
                DBHelper.SaveChanges(SqlAdd, DtAdd);
            }
            Config_Initialize();
            Public.Msg("positive", "操作成功", "操作成功", true, "system.aspx");
        }
        catch (Exception ex) {
            throw ex;
        }
        finally {
            DtAdd.Dispose();
            DtAdd = null;
            DrAdd = null;
        }
    }

    public void Config_Initialize()
    {
        string SqlList = "SELECT TOP 1 * FROM SCM_Config ORDER BY Config_ID DESC";
        SqlDataReader RdrList = null;
        try {
            RdrList = DBHelper.ExecuteReader(SqlList);
            if (RdrList.Read()) {
                Application["DefaultSupplier"] = tools.NullInt(RdrList["Config_DefaultSupplier"]);
                Application["DefaultDepot"] = tools.NullInt(RdrList["Config_DefaultDepot"]);
                Application["AlertAmount"] = tools.NullInt(RdrList["Config_AlertAmount"]);
            }
        }
        catch (Exception ex) {
            throw ex;
        }
        finally {
            RdrList.Close();
            RdrList = null;
        }

    }

}
