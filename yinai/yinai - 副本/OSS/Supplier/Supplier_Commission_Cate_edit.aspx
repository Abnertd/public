<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<% Public.CheckLogin("deaa9168-3ffc-42c3-bb94-829fbf7f2e22");
   Supplier supplier = new Supplier();
   ITools tools;
   tools = ToolsFactory.CreateTools();
   int cate_id = tools.CheckInt(Request["cate_id"]);
   int supplier_id=0;
   string cate_name="";
   double cate_amount = 0;
   SupplierCommissionCategoryInfo entity = supplier.GetSupplierCommissionCategoryByID(cate_id);
   if (entity != null)
   {
       supplier_id = entity.Supplier_Commission_Cate_SupplierID;
       cate_name = entity.Supplier_Commission_Cate_Name;
       cate_amount = entity.Supplier_Commission_Cate_Amount;
   }
   else
   {
       Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
       Response.End();
   }
   string Supplier_Name="";
   SupplierInfo supplierinfo = supplier.GetSupplierByID(supplier_id);
   if (supplierinfo != null)
   {
       Supplier_Name = supplierinfo.Supplier_CompanyName;
   }
    %>

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
      <td class="content_title">佣金分类</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="/Supplier/Supplier_Commission_Cate_do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">供应商</td>
          <td class="cell_content"><%=Supplier_Name%></td>
        </tr>
        <tr>
          <td class="cell_title">佣金分类名称</td>
          <td class="cell_content"><input name="Supplier_Commission_Cate_Name" type="text" id="Supplier_Commission_Cate_Name" size="50" value="<%=cate_name %>" maxlength="100" /></td>
        </tr>
        <tr>
          <td class="cell_title">佣金百分比</td>
          <td class="cell_content"><input name="Supplier_Commission_Cate_Amount" type="text" id="Supplier_Commission_Cate_Amount" value="<%=cate_amount %>" size="50" maxlength="50" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" /></td>
        </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="working" name="action" value="renew" />
            <input type="hidden" id="Supplier_Commission_Cate_SupplierID" name="Supplier_Commission_Cate_SupplierID" value="<%=supplier_id %>" />
            <input type="hidden" id="Supplier_Commission_Cate_ID" name="Supplier_Commission_Cate_ID" value="<%=cate_id %>" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location='Supplier_list.aspx';" /></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
  </table>
</div>
</body>
</html>
