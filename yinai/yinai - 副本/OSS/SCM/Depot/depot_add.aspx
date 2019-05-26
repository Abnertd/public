<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.Util.SQLHelper" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    
    private string Depot_Name, action;
    private int Depot_ID, Depot_Sort;

    private ITools tools;
    private ISQLHelper DBHelper;
    protected void Page_Load(object sender, EventArgs e)
    {
        tools = ToolsFactory.CreateTools();
        
        action = tools.CheckStr(Request.QueryString["action"]);
        if (action == "renew")
        {
            Public.CheckLogin("8fad33fd-ec7f-47e4-bb94-0aca729a5e46");
            
            DBHelper = SQLHelperFactory.CreateSQLHelper();
            Depot_ID = tools.CheckInt(Request.QueryString["Depot_ID"]);
            string SqlList = "SELECT * FROM SCM_Depot WHERE Depot_ID=" + Depot_ID;
            SqlDataReader RdrList = null;
            try
            {
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    Depot_ID = tools.NullInt(RdrList["Depot_ID"]);
                    Depot_Name = tools.NullStr(RdrList["Depot_Name"]);
                    Depot_Sort = tools.NullInt(RdrList["Depot_Sort"]);
                }
                else
                {
                    Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
                    Response.End();
                }
            }
            catch (Exception ex) { throw ex; }
            finally { if (RdrList != null) { RdrList.Close(); RdrList = null; } }

            action = "renew";
        }
        else {
            Public.CheckLogin("db79d1a3-cb45-46b2-bf9c-a7f35de307d7");
            
            action = "new";
            Depot_ID = 0;
            Depot_Name = "";
            Depot_Sort = 1;
        }
        
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>无标题文档</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
<script src="/Scripts/jquery.js" type="text/javascript"></script>
</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">仓库管理</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="depot_do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">仓库名称</td>
          <td class="cell_content"><input name="Depot_Name" type="text" id="Depot_Name" size="50" maxlength="100" value="<%=Depot_Name%>" /></td>
        </tr>
        <tr>
          <td class="cell_title">排序</td>
          <td class="cell_content"><input name="Depot_Sort" type="text" id="Depot_Sort" size="10" maxlength="100" value="<%=Depot_Sort%>" /></td>
        </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="action" name="action" value="<% =action%>" />
            <input type="hidden" id="Depot_ID" name="Depot_ID" value="<% =Depot_ID%>" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location='depot_list.aspx';"/></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
  </table>
</div>
</body>
</html>