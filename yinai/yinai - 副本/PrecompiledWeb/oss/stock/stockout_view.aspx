<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    private Product myApp;
    private ITools tools;

    private string product_name, product_intro, member_name,member_email,member_tel;
    private int stockout_id, stockout_isread;
    private DateTime stockout_addtime;

    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("6f896c98-c62f-43f6-a276-39e43697c771");

        myApp = new Product ();
        tools = ToolsFactory.CreateTools();

        stockout_id = tools.CheckInt(Request.QueryString["stockout_id"]);
        StockoutBookingInfo entity = myApp.GetStockoutByID(stockout_id);
        if (entity == null) {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        else {
            stockout_id = entity.Stockout_ID;
            product_name = entity.Stockout_Product_Name;
            product_intro = entity.Stockout_Product_Describe;
            member_name = entity.Stockout_Member_Name;
            member_email = entity.Stockout_Member_Email;
            member_tel = entity.Stockout_Member_Tel;
            stockout_isread = entity.Stockout_IsRead;
            stockout_addtime = entity.Stockout_Addtime ;
        }
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
      <td class="content_title">缺货登记</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="stockout_do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">商品名称</td>
          <td class="cell_content"><% =product_name%> [<%=stockout_addtime %>]</td>
        </tr>
        <tr>
          <td class="cell_title">商品描述</td>
          <td class="cell_content">
          <%=product_intro %></td>
        </tr>
        <tr>
          <td class="cell_title" valign="top">用户名</td>
          <td class="cell_content">
            <%=member_name %>
          </td>
        </tr>
        <tr>
          <td class="cell_title" valign="top">联系电话</td>
          <td class="cell_content">
            <%=member_tel %>
          </td>
        </tr>
        <tr>
          <td class="cell_title" valign="top">联系邮箱</td>
          <td class="cell_content">
            <%=member_email %>
          </td>
        </tr>
        <tr>
          <td class="cell_title">状态</td>
          <td class="cell_content">
          <% 
              if (stockout_isread == 1)
              {
                  Response.Write("已处理");
              }
              else
              {
                  Response.Write("未处理 <input type=\"submit\" class=\"bt_orange\" value=\"已处理\">");
              }
            %> </td>
        </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="action" name="action" value="processed" />
            <input type="hidden" id="stockout_id" name="stockout_id" value="<% =stockout_id%>" />
            
             <input name="button" type="button" class="bt_grey" id="button" value="返回" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location='stockout.aspx';"/></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
  </table>
</div>
</body>
</html>