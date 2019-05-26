<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%
    
    Public.CheckLogin("1392d14a-6746-4167-804a-d04a2f81d226");
    Shop shop = new Shop();
    Supplier supplier = new Supplier();
    int shop_id, Shop_Recommend, Shop_SupplierID;
   ITools tools;
   string Supplier_Name="";
   string Email = "";
   string Css_Name = "";
   Shop_Recommend = 0;
   Shop_SupplierID = 0;
   tools = ToolsFactory.CreateTools();
   shop_id = tools.CheckInt(Request["shop_id"]);
   SupplierShopInfo entity = shop.GetSupplierShopByID(shop_id);
   if (entity == null)
   {
       Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
       Response.End();
   }
   else
   {
       Shop_Recommend = entity.Shop_Recommend;
       SupplierInfo supplierinfo = supplier.GetSupplierByID(entity.Shop_SupplierID);
       if (supplierinfo != null)
       {
           Shop_SupplierID = entity.Shop_SupplierID;
           Supplier_Name = supplierinfo.Supplier_CompanyName;
           Email = supplierinfo.Supplier_Email;
           
           
       }

       SupplierShopCssInfo cssinfo = shop.GetSupplierShopCssByID(entity.Shop_Css);
       if (cssinfo != null)
       {
           Css_Name = cssinfo.Shop_Css_Title;
       }
   }
    %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>店铺管理</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
<script src="/Scripts/jquery.js" type="text/javascript"></script>
<script src="/Scripts/common.js" type="text/javascript"></script>
<link type="text/css" href="/Scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
<script src="/Scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
<script src="/Scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script>

</head>
<body>
<div class="content_div">
<form id="formadd" name="formadd" method="post" action="/Shop/Shop_Do.aspx">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">店铺管理</td>
    </tr>
    <tr>
      <td class="content_content">
      
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">

        <tr>
          <td class="cell_title">店铺名称</td>
          <td class="cell_content"><input type="text" size="50" name="Shop_Name" value="<%=entity.Shop_Name %>" /></td>
          </tr>
          <tr>
          <td class="cell_title">店铺号</td>
          <td class="cell_content"><input type="text" name="Shop_Code" value="<%=entity.Shop_Code%>"/></td>
          </tr>
          
          <tr>
          <td class="cell_title">供应商名称</td>
          <td class="cell_content"><%=Supplier_Name %></td>
        </tr>
        <tr>
          <td class="cell_title">供应商Email</td>
          <td class="cell_content"><%=Email%></td>
          </tr>
        <tr>
          <td class="cell_title">店铺域名</td>
          <td class="cell_content">http://<input type="text" name="Shop_Domain" value="<%=entity.Shop_Domain%>"/><%="." + Application["Shop_Second_Domain"]%></td>
          </tr>
          <tr>
          <td class="cell_title">店铺推荐</td>
          <td class="cell_content"><input name="Shop_Recommend" type="radio" value="1" <% =Public.CheckedRadio(Shop_Recommend.ToString(), "1")%> />推荐 <input type="radio" name="Shop_Recommend" value="0" <% =Public.CheckedRadio(Shop_Recommend.ToString(), "0")%>/>不推荐</td>
          </tr>
          <tr>
          <td class="cell_title">Banner标题</td>
          <td class="cell_content"><%=entity.Shop_Banner_Title %></td>
        </tr>
        <tr>
          <td class="cell_title">店铺样式</td>
          <td class="cell_content"><%=Css_Name%></td>
          </tr>
          <tr>
          <td class="cell_title">主营产品</td>
          <td class="cell_content"><%=entity.Shop_MainProduct %></td>
        </tr>
        <tr>
          <td class="cell_title">店铺状态</td>
          <td class="cell_content"><input name="Shop_Status" type="radio" id="Shop_Status1" value="1" <% =Public.CheckedRadio(entity.Shop_Status.ToString(), "1")%>/> 启用 <input name="Shop_Status" type="radio" id="Shop_Status" value="0" <% =Public.CheckedRadio(entity.Shop_Status.ToString(), "0")%>/> 不启用</td>
        </tr>
        <tr>
          <tr>
          <td class="cell_title">SEO标题</td>
          <td class="cell_content"><%=entity.Shop_SEO_Title %></td>
        </tr>
          <tr>
          <td class="cell_title">SEO关键词</td>
          <td class="cell_content"><%=entity.Shop_SEO_Keyword %></td>
        </tr>
                <tr>
          <td class="cell_title">SEO介绍</td>
          <td class="cell_content"><%=entity.Shop_SEO_Description%></td>
          </tr>
      </table>
       </td>
    </tr>
  </table>
  
  <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
                <input type="hidden" id="Hidden1" name="action" value="renew" />
            
            <input name="save" type="submit" class="bt_orange" id="Submit1" value="保存" />

            <input type="hidden" id="Hidden2" name="shop_id" value="<%=shop_id %>" />
             <input name="button" type="button" class="bt_orange" id="button1" value="返回" onclick="location='Shop_list.aspx';" /></td>
          </tr>
        </table>
  
        </form>
   
</div>
</body>
</html>
