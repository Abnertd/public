<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.Util.SQLHelper" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">
    
    private SCMConfig myApp;
    private ITools tools;
    private ISQLHelper DBHelper;
    private int Config_ID, Config_DefaultSupplier, Config_DefaultDepot, Config_AlertAmount;

    private SCMDepot depot;
    private SCMSupplier supplier;

    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("051ab0f1-56b4-4658-994e-b43cdbeb07f1");
        
        myApp = new SCMConfig();
        tools = ToolsFactory.CreateTools();
        DBHelper = SQLHelperFactory.CreateSQLHelper();

        depot = new SCMDepot();
        supplier = new SCMSupplier();

        string act = Request.Form["action"];
        if (act == "renew") {
            myApp.Config_Edit();
            //myApp.Config_Initialize();
            myApp = null;
        }
        
        string SqlList = "SELECT TOP 1 * FROM SCM_Config ORDER BY Config_ID DESC";
        SqlDataReader RdrList = null;
        bool recordExist = false;
        RdrList = DBHelper.ExecuteReader(SqlList);
        if (RdrList.Read()) {
            Config_ID = tools.NullInt(RdrList["Config_ID"]);
            Config_DefaultSupplier = tools.NullInt(RdrList["Config_DefaultSupplier"]);
            Config_DefaultDepot = tools.NullInt(RdrList["Config_DefaultDepot"]);
            Config_AlertAmount = tools.NullInt(RdrList["Config_AlertAmount"]);
            recordExist = true;
        }
        RdrList.Close();
        RdrList = null;

        if (!recordExist) {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
    }
</script>



<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>无标题文档</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
<script src="/Scripts/common.js" type="text/javascript"></script>
<script src="/Scripts/jquery.js" type="text/javascript"></script>
</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">系统管理</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="system.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">默认供应商</td>
          <td class="cell_content">
          <select id="Config_DefaultSupplier" name="Config_DefaultSupplier">
            <% =supplier.SupplierOption(Config_DefaultSupplier)%>
          </select></td>
        </tr>
        <tr>
          <td class="cell_title">默认仓库</td>
          <td class="cell_content">
          <select id="Config_DefaultDepot" name="Config_DefaultDepot">
            <% =depot.DepotOption(Config_DefaultDepot)%>
          </select></td>
        </tr>
        <tr>
          <td class="cell_title">库存预警</td>
          <td class="cell_content"><input name="Config_AlertAmount" type="text" size="10" maxlength="100" value="<% =Config_AlertAmount%>" /></td>
        </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="action" name="action" value="renew" />
            <input type="hidden" id="Config_ID" name="Config_ID" value="<% =Config_ID%>" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存" /></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
  </table>
</div>
</body>
</html>