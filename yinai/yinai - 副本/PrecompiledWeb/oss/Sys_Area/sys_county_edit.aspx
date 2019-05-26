<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

    SysCity MyCity;
    SysCounty MyCounty;
    private ITools tools;
    int county_id = 0;
    SysCountyInfo entity = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("23d8b0bc-4121-47f8-9010-da4bbeff07e0");
        MyCity = new SysCity();
        MyCounty = new SysCounty();
        tools = ToolsFactory.CreateTools();
        county_id = tools.NullInt(Request["county_id"]);
        entity = MyCounty.GetSysCountyByID(county_id);
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
<title>地区修改</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
<script src="/Scripts/jquery.js" type="text/javascript"></script>
</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">地区修改</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="sys_state_do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">地区名称</td>
          <td class="cell_content"><input name="Sys_County_CN" type="text" id="Sys_County_CN" value="<%=entity.Sys_County_CN %>" size="50" /></td>
        </tr>
        <tr>
          <td class="cell_title">所属城市</td>
          <td class="cell_content"><%MyCity.getSelectCitys(entity.Sys_County_CityCode); %></td>
        </tr>
        <tr>
          <td class="cell_title">是否启用</td>
          <td class="cell_content"><input name="Sys_County_IsActive" type="radio" id="Sys_County_IsActive" value="1"  <% =Public.CheckedRadio(entity.Sys_County_IsActive.ToString(), "1")%>/>是<input type="radio" name="Sys_City_IsActive" id="Sys_City_IsActive1" value="0" <% =Public.CheckedRadio(entity.Sys_County_IsActive.ToString(), "0")%>/>否 </td>
        </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="action" name="action" value="countyrenew" />
            <input type="hidden" id="Sys_County_ID" name="Sys_County_ID" value="<%=county_id %>" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location='Sys_County_list.aspx';"/></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
  </table>
</div>
</body>
</html>