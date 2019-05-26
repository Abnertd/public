<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    private Category myApp;
    private int cate_parentid;
    private ITools tools;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("402897ef-7473-4abd-9425-6220a61be7bf");
        myApp = new Category();
        tools=ToolsFactory.CreateTools();
        cate_parentid = tools.CheckInt(Request["parent_id"]);
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
      <td class="content_title">添加商品分类</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="/Product/Category_Do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">分类名称</td>
          <td class="cell_content"><input name="Cate_Name" type="text" id="Cate_Name" size="50" maxlength="100" /></td>
        </tr>
        <tr>
          <td class="cell_title">上级分类</td>
          <td class="cell_content">
          <select name="Cate_ParentID" id="Cate_ParentID">
            <option value="0">选择父类别</option>
            <% =myApp.SelectCategoryOption(0, cate_parentid, "", 0)%>
          </select></td>
        </tr>
        <tr>
          <td class="cell_title">默认参数</td>
          <td class="cell_content">
          <select name="Cate_TypeID" id="Cate_TypeID">
            <option value="0">通用参数</option>
            <% =myApp.ProductTypeOption(0)%>
          </select></td>
        </tr>
        <tr>
          <td class="cell_title">分类图标</td>
          <td class="cell_content"><iframe id="iframe_upload" src="<% =Application["upload_server_url"]%>/public/uploadify.aspx?App=category&formname=formadd&frmelement=Cate_Img&rtvalue=1&rturl=<% =Application["upload_server_return_admin"]%>" width="100%" height="100" frameborder="0" scrolling="no"></iframe><input name="Cate_Img" type="hidden" id="Cate_Img" /></td>
        </tr>
        <tr id="tr_Cate_Img" style="display:none;">
          <td class="cell_title"></td>
          <td class="cell_content"><img src="" id="img_Cate_Img" /></td>
        </tr>
        <tr>
          <td class="cell_title">类别排序</td>
          <td class="cell_content"><input name="Cate_Sort" type="text" id="Cate_Sort" value="1" size="10" maxlength="10"  onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')"/>
            <span class="tip">数字越小越靠前</span></td>
        </tr>
        <tr>
          <td class="cell_title">启用该分类</td>
          <td class="cell_content"><input name="Cate_IsActive" type="radio" id="radio" value="1" checked="checked"/>是<input type="radio" name="Cate_IsActive" id="radio2" value="0"/>否 </td>
        </tr>
        <tr>
          <td class="cell_title">设置为常用分类</td>
          <td class="cell_content"><input name="Cate_IsFrequently" type="radio" id="radio3" value="1" checked="checked" />
是
  <input type="radio" name="Cate_IsFrequently" id="radio4" value="0" />
否</td>
        </tr>
        <tr>
          <td class="cell_title">分类文件路径</td>
          <td class="cell_content"><input name="Cate_SEO_Path" type="text" id="Cate_SEO_Path" size="50" maxlength="50" /></td>
        </tr>
        <tr>
          <td class="cell_title">TITLE<br />
            (页面标题)</td>
          <td class="cell_content"><input name="Cate_SEO_Title" type="text" id="Cate_SEO_Title" size="50" maxlength="200" /></td>
        </tr>
        <tr>
          <td class="cell_title">META_KEYWORDS<br />
            (页面关键词)</td>
          <td class="cell_content"><input name="Cate_SEO_Keyword" type="text" id="Cate_SEO_Keyword" size="50" maxlength="200" /></td>
        </tr>
        <tr>
          <td class="cell_title">META_DESCRIPTION<br />
            (页面描述)</td>
          <td class="cell_content"><textarea name="Cate_SEO_Description" cols="50" rows="5" id="Cate_SEO_Description"></textarea></td>
        </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="action" name="action" value="new" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存分类" />
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location='category.aspx';"/></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
  </table>
</div>
</body>
</html>
