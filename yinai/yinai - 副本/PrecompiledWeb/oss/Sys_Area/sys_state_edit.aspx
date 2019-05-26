<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    SysState MyApp;
    private ITools tools;
    int state_id = 0;
    SysStateInfo entity = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("6364a1f1-8b6d-43cb-a2eb-268ff86b4840");
        MyApp = new SysState();
        tools = ToolsFactory.CreateTools();
        state_id = tools.NullInt(Request["state_id"]);
        entity = MyApp.GetSysStateByID(state_id);
        if (entity == null)
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>省份修改</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
<script src="/Scripts/jquery.js" type="text/javascript"></script>
</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">省份修改</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="sys_state_do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">省份名称</td>
          <td class="cell_content"><input name="Sys_State_CN" type="text" id="Sys_State_CN" value="<%=entity.Sys_State_CN %>" size="50" /></td>
        </tr>
        <tr>
          <td class="cell_title">是否启用</td>
          <td class="cell_content"><input name="Sys_State_IsActive" type="radio" id="Sys_State_IsActive" value="1"  <% =Public.CheckedRadio(entity.Sys_State_IsActive.ToString(), "1")%>/>是<input type="radio" name="Sys_State_IsActive" id="Sys_State_IsActive1" value="0" <% =Public.CheckedRadio(entity.Sys_State_IsActive.ToString(), "0")%>/>否 </td>
        </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="action" name="action" value="staterenew" />
            <input type="hidden" id="Sys_State_ID" name="Sys_State_ID" value="<%=state_id %>" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location='Sys_State_list.aspx';"/></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
  </table>
</div>
</body>
</html>