<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    Sources myApp;
    ITools tools;
    
    string Sources_Name, Sources_Code, Sources_Site;
    int Sources_ID;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("485f0ee2-f5a3-41a0-a778-68a87c5b5d89");

        myApp = new Sources();
        tools = ToolsFactory.CreateTools();

        Sources_ID = tools.CheckInt(Request.QueryString["Sources_ID"]);
        SourcesInfo entity = myApp.GetSourcesByID(Sources_ID);
        if (entity == null) {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        else {
            Sources_ID = entity.Sources_ID;
            Sources_Name = entity.Sources_Name;
            Sources_Code = entity.Sources_Code;
            Sources_Site = entity.Sources_Site;
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
      <td class="content_title">来源管理</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="sources_do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">来源名称</td>
          <td class="cell_content"><input name="Sources_Name" type="text" id="Sources_Name" size="50" maxlength="100" value="<% =Sources_Name%>" /></td>
        </tr>
        <tr>
          <td class="cell_title">来源代码</td>
          <td class="cell_content"><input name="Sources_Code" type="text" id="Sources_Code" size="50" maxlength="100" value="<% =Sources_Code%>" /></td>
        </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="action" name="action" value="renew" />
            <input type="hidden" id="Sources_ID" name="Sources_ID" value="<% =Sources_ID%>" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location='sources_list.aspx';"/></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
  </table>
</div>
</body>
</html>