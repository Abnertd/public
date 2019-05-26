<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%
    
    Public.CheckLogin("1392d14a-6746-4167-804a-d04a2f81d226");
    Shop shop = new Shop();
    Supplier supplier =new Supplier();
    ITools tools = ToolsFactory.CreateTools();
    string Shop_Article_Title, Shop_Article_Content, Shop_Article_Site, Supplier_Name;
    int Shop_Article_ID, Shop_Article_SupplierID, Shop_Article_IsActive, Shop_Article_Hits;

    Supplier_Name = "";
    Shop_Article_Title = "";
    Shop_Article_Content = "";
        tools = ToolsFactory.CreateTools();

        Shop_Article_ID = tools.CheckInt(Request.QueryString["Shop_Article_ID"]);
        SupplierShopArticleInfo entity = shop.GetSupplierShopArticleByID(Shop_Article_ID);
        if (entity == null) {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        else
        {
            Shop_Article_ID = entity.Shop_Article_ID;
            Shop_Article_Title = entity.Shop_Article_Title;
            Shop_Article_SupplierID = entity.Shop_Article_SupplierID;
            Shop_Article_Content = entity.Shop_Article_Content;
            Shop_Article_IsActive = entity.Shop_Article_IsActive;
            Shop_Article_Hits = entity.Shop_Article_Hits;
            Shop_Article_Site = entity.Shop_Article_Site;
            SupplierInfo supplierinfo = supplier.GetSupplierByID(entity.Shop_Article_SupplierID);
            if (supplierinfo!=null)
            {
                Supplier_Name = supplierinfo.Supplier_CompanyName;
            }
        }

    %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>店铺活动查看</title>
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
      <td class="content_title">店铺活动查看</td>
    </tr>
    <tr>
      <td class="content_content">
      
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">

        <tr>
          <td class="cell_title">标题</td>
          <td class="cell_content"><%=Shop_Article_Title%></td>
          </tr>
          <tr>
          <td class="cell_title">供应商名称</td>
          <td class="cell_content"><%=Supplier_Name %></td>
        </tr>
        <tr>
          <td class="cell_title">内容</td>
          <td class="cell_content"><%=Shop_Article_Content%></td>
          </tr>
        
      </table>
       </td>
    </tr>
  </table>
  
 <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">

             <input name="button" type="button" class="bt_orange" id="button1" value="返回" onclick="location='Shop_Article_list.aspx';" /></td>
          </tr>
        </table>
  
        </form>
   
</div>
</body>
</html>
