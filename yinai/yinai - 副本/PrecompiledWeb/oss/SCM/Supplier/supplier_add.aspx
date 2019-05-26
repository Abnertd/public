<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.Util.SQLHelper" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    
    private string Supplier_Name, Supplier_Address, Supplier_Tel, Supplier_Contact, action;
    private int Supplier_ID;

    private ITools tools;
    private ISQLHelper DBHelper;
    protected void Page_Load(object sender, EventArgs e)
    {
        tools = ToolsFactory.CreateTools();
        
        action = tools.CheckStr(Request.QueryString["action"]);
        if (action == "renew")
        {
            Public.CheckLogin("3306c908-ff91-4e6e-8c46-8157cd5b6e4a");
            
            DBHelper = SQLHelperFactory.CreateSQLHelper();
            Supplier_ID = tools.CheckInt(Request.QueryString["Supplier_ID"]);
            string SqlList = "SELECT * FROM SCM_Supplier WHERE Supplier_ID=" + Supplier_ID;
            SqlDataReader RdrList = null;
            try
            {
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    Supplier_ID = tools.NullInt(RdrList["Supplier_ID"]);
                    Supplier_Name = tools.NullStr(RdrList["Supplier_Name"]);
                    Supplier_Address = tools.NullStr(RdrList["Supplier_Address"]);
                    Supplier_Tel = tools.NullStr(RdrList["Supplier_Tel"]);
                    Supplier_Contact = tools.NullStr(RdrList["Supplier_Contact"]);
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
            Public.CheckLogin("83c81946-5b6d-48cf-9e5a-17b4807d18ff");
            
            action = "new"; 
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
      <td class="content_title">供应商管理</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="supplier_do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">供应商名称</td>
          <td class="cell_content"><input name="Supplier_Name" type="text" id="Supplier_Name" size="50" maxlength="100" value="<%=Supplier_Name%>" /></td>
        </tr>
        <tr>
          <td class="cell_title">地址</td>
          <td class="cell_content"><input name="Supplier_Address" type="text" id="Supplier_Address" size="60" maxlength="300" value="<%=Supplier_Address%>" /></td>
        </tr>
        <tr>
          <td class="cell_title">电话</td>
          <td class="cell_content"><input name="Supplier_Tel" type="text" id="Supplier_Tel" size="50" maxlength="300" value="<%=Supplier_Tel%>" /></td>
        </tr>
        <tr>
          <td class="cell_title">联系人</td>
          <td class="cell_content"><input name="Supplier_Contact" type="text" id="Supplier_Contact" size="50" maxlength="300" value="<%=Supplier_Contact%>" /></td>
        </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="action" name="action" value="<% =action%>" />
            <input type="hidden" id="Supplier_ID" name="Supplier_ID" value="<% =Supplier_ID%>" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location='supplier_list.aspx';"/></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
  </table>
</div>
</body>
</html>