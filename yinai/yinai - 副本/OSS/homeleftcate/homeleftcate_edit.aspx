<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    
    private string Home_Left_Cate_Name, Home_Left_Cate_URL, Home_Left_Cate_Img, Home_Left_Cate_Site;
    private int Home_Left_Cate_ID, Home_Left_Cate_ParentID, Home_Left_Cate_Sort, Home_Left_Cate_Active, Home_Left_Cate_CateID;

    private HomeLeftCate myApp;
    
    private string action;
    private ITools tools;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("de88931b-4a5b-4bb7-8f68-4975ad26e59c");
        tools = ToolsFactory.CreateTools();

        myApp = new HomeLeftCate();
        Home_Left_Cate_ID = tools.CheckInt(Request.QueryString["Home_Left_Cate_ID"]);
        HomeLeftCateInfo entity = myApp.GetHomeLeftCateByID(Home_Left_Cate_ID);
        if (entity != null)
        {
            Home_Left_Cate_ID = entity.Home_Left_Cate_ID;
            Home_Left_Cate_ParentID = entity.Home_Left_Cate_ParentID;
            Home_Left_Cate_CateID = entity.Home_Left_Cate_CateID;
            Home_Left_Cate_Name = entity.Home_Left_Cate_Name;
            Home_Left_Cate_URL = entity.Home_Left_Cate_URL;
            Home_Left_Cate_Img = entity.Home_Left_Cate_Img;
            Home_Left_Cate_Sort = entity.Home_Left_Cate_Sort;
            Home_Left_Cate_Active = entity.Home_Left_Cate_Active;
            Home_Left_Cate_Site = entity.Home_Left_Cate_Site;
        }
        else
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>首页左侧分类修改</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
<script src="/Scripts/jquery.js" type="text/javascript"></script>
<script type="text/javascript" src="/public/fckeditor/fckeditor.js"></script>
</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">首页左侧分类修改</td>
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
            <% =myApp.CateOption(0, Home_Left_Cate_ParentID, "", Home_Left_Cate_ID)%>
          </select></td>
        </tr>
        <tr>
          <td class="cell_title">类别名称</td>
          <td class="cell_content"><input name="Home_Left_Cate_Name" type="text" id="Home_Left_Cate_Name" size="50" maxlength="30" value="<%=Home_Left_Cate_Name%>" /></td>
        </tr>
        <tr>
          <td class="cell_title">链接地址</td>
          <td class="cell_content"><input name="Home_Left_Cate_URL" type="text" id="Home_Left_Cate_URL" size="50" maxlength="200" value="<%=Home_Left_Cate_URL%>" /></td>
        </tr>
        <tr>
          <td class="cell_title">分类图标</td>
          <td class="cell_content"><iframe id="iframe_upload" src="<% =Application["upload_server_url"]%>/public/uploadify.aspx?App=category&formname=formadd&frmelement=Home_Left_Cate_Img&rtvalue=1&rturl=<% =Application["upload_server_return_admin"]%>" width="100%" height="100" frameborder="0" scrolling="no"></iframe></td>
        </tr>
        <tr id="tr_Home_Left_Cate_Img" style="display:none;">
          <td class="cell_title"><input name="Home_Left_Cate_Img" type="hidden" id="Home_Left_Cate_Img" value="<%=Home_Left_Cate_Img%>" /></td>
          <td class="cell_content"><img src="" id="img_Home_Left_Cate_Img" /></td>
        </tr>
        <tr>
          <td class="cell_title">排序</td>
          <td class="cell_content"><input name="Home_Left_Cate_Sort" type="text" id="Home_Left_Cate_Sort" size="10" maxlength="100" value="<%=Home_Left_Cate_Sort%>" /></td>
        </tr>
        <tr>
          <td class="cell_title">对应分类ID</td>
          <td class="cell_content"><input name="Home_Left_Cate_CateID" type="text" id="Home_Left_Cate_CateID" size="10" maxlength="100" value="<%=Home_Left_Cate_CateID%>" /></td>
        </tr>
        <tr>
          <td class="cell_title">启用</td>
          <td class="cell_content"><input name="Home_Left_Cate_Active" type="radio" id="radio" value="1" <% =Public.CheckedRadio(Home_Left_Cate_Active.ToString(), "1")%>/>是<input type="radio" name="Home_Left_Cate_Active" id="radio2" value="0" <% =Public.CheckedRadio(Home_Left_Cate_Active.ToString(), "0")%>/>否</td>
        </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="action" name="action" value="renew" />
            <input type="hidden" id="Home_Left_Cate_ID" name="Home_Left_Cate_ID" value="<% =Home_Left_Cate_ID%>" />
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