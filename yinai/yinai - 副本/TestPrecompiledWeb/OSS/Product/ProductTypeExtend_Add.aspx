<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<script runat="server">

    private ProductType producttype;
    private ITools tools;

    private int ProductType_ID;
    private string ProductType_Name;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("847e8136-fd2f-4834-86b7-f2c984705eff");
        
        producttype = new ProductType();
        tools = ToolsFactory.CreateTools();

        ProductType_ID = tools.CheckInt(Request.QueryString["ProductType_ID"]);
        if (ProductType_ID == 0)
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        ProductTypeInfo entity = producttype.GetProductTypeByID(ProductType_ID);
        if (entity == null) {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        } else {
            ProductType_ID = entity.ProductType_ID;
            ProductType_Name = entity.ProductType_Name;
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
      <td class="content_title">添加扩展属性</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="/Product/Producttypeextend_Do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
      <tr>
          <td class="cell_title">类型名称</td>
          <td class="cell_content"><%=ProductType_Name %></td>
        </tr>
        <tr>
          <td class="cell_title">属性名称</td>
          <td class="cell_content"><input name="ProductTypeExtend_Name" type="text" id="ProductTypeExtend_Name" size="50" maxlength="100" /></td>
        </tr>
        <tr>
          <td class="cell_title">后台输入方式</td>
          <td class="cell_content"><input name="ProductTypeExtend_Display" type="radio" id="ProductTypeExtend_Display1" value="1" checked="checked"/>文本框 <input type="radio" name="ProductTypeExtend_Display" id="ProductTypeExtend_Display2" value="0" />下拉菜单 <%--<input type="radio" name="ProductTypeExtend_Display" id="ProductTypeExtend_Display3" value="2"/>HTML--%></td>
        </tr>
        <tr>
          <td class="cell_title">筛选项</td>
          <td class="cell_content"><input name="ProductType_Extend_IsSearch" type="radio" id="ProductType_Extend_IsSearch1" value="0" checked="checked"/>否 <input type="radio" name="ProductType_Extend_IsSearch" id="ProductType_Extend_IsSearch" value="1"/>是 </td>
        </tr>
        <tr>
          <td class="cell_title">属性选项</td>
          <td class="cell_content">
              <%--<input name="ProductTypeExtend_Options" type="radio" id="ProductTypeExtend_Options1" value="1"/>客户仅可见--%> 
              <input type="radio" name="ProductTypeExtend_Options" id="ProductTypeExtend_Options2" value="2"  checked="checked"/>客户选择 
              <%--<input type="radio" name="ProductTypeExtend_Options" id="ProductTypeExtend_Options3" value="3"/>客户输入--%> 

          </td>
        </tr>
        <tr>
          <td class="cell_title">启用该类型</td>
          <td class="cell_content"><input name="ProductTypeExtend_IsActive" type="radio" id="ProductTypeExtend_IsActive1" value="1" checked="checked"/>是 <input type="radio" name="ProductTypeExtend_IsActive" id="ProductTypeExtend_IsActive2" value="0"/>否 </td>
        </tr>
        <tr>
          <td class="cell_title">前端聚合项</td>
          <td class="cell_content"><input name="ProductType_Extend_Gather" type="radio" id="ProductType_Extend_Gather0" value="0" checked="checked" />否 <input type="radio" name="ProductType_Extend_Gather" id="ProductType_Extend_Gather1" value="1" />主聚合项 <input type="radio" name="ProductType_Extend_Gather" id="ProductType_Extend_Gather2" value="2"/>从聚合项</td>
        </tr>
        <tr>
          <td class="cell_title">前端聚合项表现形式</td>
          <td class="cell_content"><input name="ProductType_Extend_DisplayForm" type="radio" id="ProductType_Extend_DisplayForm0" value="0"/>下拉菜单 <input type="radio" name="ProductType_Extend_DisplayForm" id="ProductType_Extend_DisplayForm1" value="1" checked="checked" />文字平铺 <input type="radio" name="ProductType_Extend_DisplayForm" id="ProductType_Extend_DisplayForm2" value="2"/>图片平铺 <input type="radio" name="ProductType_Extend_DisplayForm" id="ProductType_Extend_DisplayForm3" value="3"/>图文平铺</td>
        </tr>
        <tr>
          <td class="cell_title">类型排序</td>
          <td class="cell_content"><input name="ProductTypeExtend_Sort"  onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" type="text" id="ProductTypeExtend_Sort" value="1" size="10" maxlength="10" />
            <span class="tip">数字越小越靠前</span></td>
        </tr>
        <tr>
          <td class="cell_title">默认值</td>
          <td class="cell_content"><textarea name="ProductTypeExtend_Default" rows="5" cols="50"></textarea> <span class="tip">各项间以“|”分隔</span></td>
        </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="action" name="action" value="new" />
            <input type="hidden" id="ProductType_ID" name="ProductType_ID" value="<%=ProductType_ID %>" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存属性" />
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';"  onclick="history.go(-1);"/></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
  </table>
</div>
</body>
</html>
