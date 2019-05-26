<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<% Public.CheckLogin("97f86571-d7ce-4fce-add5-f444c3a89ce3");
   Supplier supplier = new Supplier();
    Shop shop = new Shop();
   ITools tools;
   tools = ToolsFactory.CreateTools();
   int Shop_Type = 0;
   int Supplier_ID = tools.CheckInt(Request["Supplier_ID"]);
   SupplierInfo supplierinfo = supplier.GetSupplierByID(Supplier_ID);
   if (supplierinfo != null)
   {
       SupplierShopInfo entity = shop.GetSupplierShopBySupplierID(Supplier_ID);
       if (entity != null)
       {
           Shop_Type = entity.Shop_Type;
       }
       else
       {
           Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
           Response.End();
       }
   }
   else
   {
       Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
       Response.End();
   }

   
    %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>店铺升级</title>
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
      <td class="content_title">店铺升级</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="/shop/shop_do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">店铺类型</td>
          <td class="cell_content">
          <input type="radio" name="Shop_Type" value="3" <%=Public.CheckedRadio("3",Shop_Type.ToString()) %>/> 销售店铺
          <input type="radio" name="Shop_Type" value="2" <%=Public.CheckedRadio("2",Shop_Type.ToString()) %>/> 展示店铺
          <input type="radio" name="Shop_Type" value="1" <%=Public.CheckedRadio("1",Shop_Type.ToString()) %>/> 体验店铺
          <span class="t12_red">*</span>
          </td>
        </tr>
        <%--<tr>
          <td class="cell_title">升级说明</td>
          <td class="cell_content">
          <%if (Supplier_Margin_ID > 0)
            { %>
            <table border="0" cellspacing="1" cellpadding="8" bgcolor="#333333">
                    <tr bgcolor="#0161AD"><td width="100"></td><td align="center" width="120" class="t12_white">保证金最低额</td><td align="center" width="120" class="t12_white">会员费最低额</td><td align="center" width="120" class="t12_white">月费用</td></tr>
                    
                    <tr bgcolor="#ffffff"><td align="center">销售店铺</td><td align="center"><%=Public.DisplayCurrency(margininfo.Supplier_Margin_SecurityMoney1) %></td><td align="center"><%=Public.DisplayCurrency(margininfo.Supplier_Margin_Account1) %></td><td align="center"><%=Public.DisplayCurrency(margininfo.Supplier_Margin_Fee1) %></td></tr>
                    <tr bgcolor="#ffffff"><td align="center">展示店铺</td><td align="center"><%=Public.DisplayCurrency(margininfo.Supplier_Margin_SecurityMoney2) %></td><td align="center"><%=Public.DisplayCurrency(margininfo.Supplier_Margin_Account2) %></td><td align="center"><%=Public.DisplayCurrency(margininfo.Supplier_Margin_Fee2) %></td></tr>
                    <tr bgcolor="#ffffff"><td align="center">体验店铺</td><td align="center"><%=Public.DisplayCurrency(margininfo.Supplier_Margin_SecurityMoney3) %></td><td align="center"><%=Public.DisplayCurrency(margininfo.Supplier_Margin_Account3) %></td><td align="center"><%=Public.DisplayCurrency(margininfo.Supplier_Margin_Fee3) %></td></tr>
                    </table>
            <%} %>
          </td>
        </tr>--%>
        
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="working" name="action" value="upgrade" />
            <input type="hidden" id="Supplier_ID" name="Supplier_ID" value="<%=Supplier_ID %>" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="history.go(-1);" /></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
  </table>
</div>
</body>
</html>
