<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<script runat="server">

    private Category myApp;
    private ITools tools;

    private int Cate_ID, Cate_ParentID, Cate_TypeID,Cate_ProductTypeID, Cate_Sort, Cate_IsFrequently, Cate_IsActive, Cate_Count_Brand, Cate_Count_Product;
    private string Cate_Name, Cate_Img, Cate_SEO_Path, Cate_SEO_Title, Cate_SEO_Keyword, Cate_SEO_Description, Cate_Site;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("2dcee4f1-71e1-4cbd-afa3-470f0b554fd0");
        
        myApp = new Category();
        tools = ToolsFactory.CreateTools();
        
        Cate_ID = tools.CheckInt(Request.QueryString["Cate_ID"]);
        CategoryInfo entity = myApp.GetCategoryByID(Cate_ID);
        if (entity == null) {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        } else {
            Cate_ID = entity.Cate_ID;
            Cate_ParentID = entity.Cate_ParentID;
            Cate_Name = entity.Cate_Name;
            Cate_TypeID = entity.Cate_TypeID;
            Cate_Img = entity.Cate_Img;
            Cate_ProductTypeID = entity.Cate_ProductTypeID;
            Cate_Sort = entity.Cate_Sort;
            Cate_IsFrequently = entity.Cate_IsFrequently;
            Cate_IsActive = entity.Cate_IsActive;
            Cate_Count_Brand = entity.Cate_Count_Brand;
            Cate_Count_Product = entity.Cate_Count_Product;
            Cate_SEO_Path = entity.Cate_SEO_Path;
            Cate_SEO_Title = entity.Cate_SEO_Title;
            Cate_SEO_Keyword = entity.Cate_SEO_Keyword;
            Cate_SEO_Description = entity.Cate_SEO_Description;
            Cate_Site = entity.Cate_Site;
        }
    }
</script>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>无标题文档</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
<script src="/Scripts/jquery.js" type="text/javascript"></script>
<link type="text/css" href="/Scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
<script src="/Scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
<script src="/Scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script>
<script src="/Public/ckeditor/ckeditor.js" type="text/javascript"></script>
</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">修改商品分类</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="/Product/Category_Do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">分类名称</td>
          <td class="cell_content"><input name="Cate_Name" type="text" id="Cate_Name" size="50" maxlength="100" value="<% =Cate_Name %>" /></td>
        </tr>
        <tr>
          <td class="cell_title">上级分类</td>
          <td class="cell_content"><select name="Cate_ParentID" id="Cate_ParentID">
            <option value="0">选择父类别</option>
            <% =myApp.SelectCategoryOption(0, Cate_ParentID, "", Cate_ID)%>
          </select></td>
        </tr>
        <tr>
          <td class="cell_title">默认参数</td>
          <td class="cell_content">
          <select name="Cate_TypeID" id="Cate_TypeID">
            <option value="0">通用参数</option>
            <% =myApp.ProductTypeOption(Cate_TypeID)%>
          </select></td>
        </tr>
        <tr>
          <td class="cell_title">分类图标</td>
          <td class="cell_content"><iframe id="iframe_upload" src="<% =Application["upload_server_url"]%>/public/uploadify.aspx?App=category&formname=formadd&frmelement=Cate_Img&rtvalue=1&rturl=<% =Application["upload_server_return_admin"]%>" width="100%" height="100" frameborder="0" scrolling="no"></iframe>
          <input name="Cate_Img" type="hidden" id="Cate_Img" value="<% =Cate_Img %>" /></td>
        </tr>
        <tr id="tr_Cate_Img" <% if (Cate_Img.Length == 0) { Response.Write("style=\"display:none;\""); } %>>
          <td class="cell_title"></td>
          <td class="cell_content"><img src="<% =Public.FormatImgURL(Cate_Img, "fullpath") %>" id="img_Cate_Img" /></td>
        </tr>
        <tr>
          <td class="cell_title">类别排序</td>
          <td class="cell_content"><input name="Cate_Sort" type="text" id="Cate_Sort" size="10" maxlength="10" value="<% =Cate_Sort %>" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" />
            <span class="tip">数字越小越靠前</span></td>
        </tr>
        <tr>
          <td class="cell_title">启用该分类</td>
          <td class="cell_content"><input name="Cate_IsActive" type="radio" id="radio" value="1" <% =Public.CheckedRadio(Cate_IsActive.ToString(), "1")%>/>是<input type="radio" name="Cate_IsActive" id="radio2" value="0" <% =Public.CheckedRadio(Cate_IsActive.ToString(), "0")%>/>否 </td>
        </tr>
        <tr>
          <td class="cell_title">设置为常用分类</td>
          <td class="cell_content"><input name="Cate_IsFrequently" type="radio" id="radio3" value="1" <% =Public.CheckedRadio(Cate_IsFrequently.ToString(), "1")%> />
是
  <input type="radio" name="Cate_IsFrequently" id="radio4" value="0" <% =Public.CheckedRadio(Cate_IsFrequently.ToString(), "0")%>/>
否</td>
        </tr>
        <tr>
          <td class="cell_title">分类文件路径</td>
          <td class="cell_content"><input name="Cate_SEO_Path" type="text" id="Cate_SEO_Path" size="50" maxlength="50" value="<% =Cate_SEO_Path %>" /></td>
        </tr>
        <tr>
          <td class="cell_title">TITLE<br />
            (页面标题)</td>
          <td class="cell_content"><input name="Cate_SEO_Title" type="text" id="Cate_SEO_Title" size="50" maxlength="200" value="<% =Cate_SEO_Title %>" /></td>
        </tr>
        <tr>
          <td class="cell_title">META_KEYWORDS<br />
            (页面关键词)</td>
          <td class="cell_content"><input name="Cate_SEO_Keyword" type="text" id="Cate_SEO_Keyword" size="50" maxlength="200" value="<% =Cate_SEO_Keyword %>" /></td>
        </tr>
        <tr>
          <td class="cell_title">META_DESCRIPTION<br />
            (页面描述)</td>
          <td class="cell_content"><textarea name="Cate_SEO_Description" cols="50" rows="5" id="Cate_SEO_Description"><% =Cate_SEO_Description%></textarea></td>
        </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="Cate_ID" name="Cate_ID" value="<% =Cate_ID %>" />
            <input type="hidden" id="action" name="action" value="renew" />
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
