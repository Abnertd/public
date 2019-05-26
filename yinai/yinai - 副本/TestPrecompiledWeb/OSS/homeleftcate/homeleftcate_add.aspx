<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.Util.SQLHelper" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    
    private int Parent_ID;

    private HomeLeftCate myApp;
    
    private string action;
    private ITools tools;
    private ISQLHelper DBHelper;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("8738cd22-6808-4fdd-94f4-d9bb51b64509");
        tools = ToolsFactory.CreateTools();

        myApp = new HomeLeftCate();
        Parent_ID = tools.CheckInt(Request.QueryString["Parent_ID"]);
        
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>首页左侧分类添加</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
<script src="/Scripts/jquery.js" type="text/javascript"></script>
<script type="text/javascript" src="/public/fckeditor/fckeditor.js"></script>
</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">首页左侧分类添加</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="homeleftcate_do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">父类别</td>
          <td class="cell_content">
          <select id="Home_Left_Cate_ParentID" name="Home_Left_Cate_ParentID">
            <option value="0">选择父类别</option>
            <% =myApp.CateOption(0, Parent_ID, "", 0)%>
          </select></td>
        </tr>
        <tr>
          <td class="cell_title">类别名称</td>
          <td class="cell_content"><input name="Home_Left_Cate_Name" type="text" id="Home_Left_Cate_Name" size="50" maxlength="30" value="" /></td>
        </tr>
        <tr>
          <td class="cell_title">链接地址</td>
          <td class="cell_content"><input name="Home_Left_Cate_URL" type="text" id="Home_Left_Cate_URL" size="50" maxlength="200" value="" /></td>
        </tr>
        <tr>
          <td class="cell_title">分类图标</td>
          <td class="cell_content"><iframe id="iframe_upload" src="<% =Application["upload_server_url"]%>/public/uploadify.aspx?App=category&formname=formadd&frmelement=Home_Left_Cate_Img&rtvalue=1&rturl=<% =Application["upload_server_return_admin"]%>" width="100%" height="100" frameborder="0" scrolling="no"></iframe></td>
        </tr>
        <tr id="tr_Home_Left_Cate_Img" style="display:none;">
          <td class="cell_title"><input name="Home_Left_Cate_Img" type="hidden" id="Home_Left_Cate_Img" value="" /></td>
          <td class="cell_content"><img src="" id="img_Home_Left_Cate_Img" /></td>
        </tr>
        <tr>
          <td class="cell_title">排序</td>
          <td class="cell_content"><input name="Home_Left_Cate_Sort" type="text" id="Home_Left_Cate_Sort" size="10" maxlength="100" value="" /></td>
        </tr>
        <tr>
          <td class="cell_title">对应分类ID</td>
          <td class="cell_content"><input name="Home_Left_Cate_CateID" type="text" id="Home_Left_Cate_CateID" size="10" maxlength="100" value="" /></td>
        </tr>
        <tr>
          <td class="cell_title">启用</td>
          <td class="cell_content"><input name="Home_Left_Cate_Active" type="radio" id="radio" value="1" checked/>是<input type="radio" name="Home_Left_Cate_Active" id="radio2" value="0" />否</td>
        </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="action" name="action" value="new" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location='homeleftcate_list.aspx';"/></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
  </table>
</div>
</body>
</html>