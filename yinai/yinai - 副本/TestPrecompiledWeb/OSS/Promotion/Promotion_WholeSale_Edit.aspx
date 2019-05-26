<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    private Promotion myApp;
    private ITools tools;
    private PromotionWholeSaleInfo entity;
    private ProductInfo product;
    private Product productlcass;
    int Wholesale_id, Promotion_WholeSale_ProductID, Promotion_WholeSale_GroupID, WholeSale_amount, limit_limit, group_id;
    string product_code, product_name, product_spec, product_maker, Promotion_Limit_Starttime, Promotion_Limit_Endtime,Grade_Str;
    double product_price, Promotion_WholeSale_Price;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("6f2255a8-caae-4e2e-9228-e9b61fd3ce99");
        tools = ToolsFactory.CreateTools();
        myApp = new Promotion();
        productlcass=new Product();
        product_code = "";
        product_maker = "";
        product_spec = "";
        product_name = "";
        product_price = 0;
        Promotion_WholeSale_GroupID = 0;
        group_id = 0;
        Wholesale_id = tools.CheckInt(Request["Promotion_WholeSale_ID"]);
        entity = myApp.GetPromotionWholeSaleByID(Wholesale_id);
        if (entity == null)
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        else
        {
            product = productlcass.GetProductByID(entity.Promotion_WholeSale_ProductID);
            if (product != null)
            {
                product_code = product.Product_Code;
                product_name = product.Product_Name;
                product_spec = product.Product_Spec;
                product_maker = product.Product_Maker;
                product_price = product.Product_Price;
            }
            Promotion_WholeSale_GroupID = entity.Promotion_WholeSale_GroupID;
            Promotion_WholeSale_ProductID = entity.Promotion_WholeSale_ProductID;
            Promotion_WholeSale_Price = entity.Promotion_WholeSale_Price;
            WholeSale_amount = entity.Promotion_WholeSale_MinAmount;
            
        }
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>无标题文档</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
<script src="/Scripts/jquery.js" type="text/javascript"></script>
<link type="text/css" href="/Scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
<script src="/Scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
<script src="/Scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script>
<script src="/Scripts/common.js" type="text/javascript"></script>
<script src="/Scripts/promotion.js" type="text/javascript"></script>
<script type="text/javascript">
change_inputcss();
</script>
</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">批发促销</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="/promotion/Promotion_WholeSale_do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
      <tr>
	      <td class="cell_title">
	      促销分组
              </td>
	      <td class="cell_content"> <%myApp.GetPromotionWholeSaleGroups(Promotion_WholeSale_GroupID); %></td>
	    </tr>
        <tr>
          <td class="cell_title" valign="top">商品编号</td>
          <td class="cell_content"><% =product_code%></td>
        </tr> 
			<tr>
          <td class="cell_title" valign="top">商品名称</td>
          <td class="cell_content"><% =product_name%></td>
        </tr> 
        <tr>
          <td class="cell_title" valign="top">规格</td>
          <td class="cell_content"><% =product_spec%></td>
        </tr> 

			<tr>
          <td class="cell_title" valign="top">生产企业</td>
          <td class="cell_content"><% =product_maker%></td>
        </tr> 
        <tr>
          <td class="cell_title" valign="top">本站价格</td>
          <td class="cell_content"><% =product_price%></td>
        </tr> 
	            
        <tr>
			      <td class="cell_title">
                      限时价格</td>
			      <td class="cell_content"><input name="Promotion_WholeSale_Price" type="text" id="Promotion_WholeSale_Price" value="<%=Promotion_WholeSale_Price %>" size="40" maxlength="50"></td>
			    </tr>
			    <tr>
			      <td class="cell_title">
                      最小购买数量</td>
			      <td class="cell_content"><input name="Promotion_WholeSale_MinAmount" type="text" id="Promotion_WholeSale_MinAmount" value="<%=WholeSale_amount %>" size="40" maxlength="50"></td>
			    </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="action" name="action" value="renew" />
            <input type="hidden" id="Promotion_WholeSale_ID" name="Promotion_WholeSale_ID" value="<%=Wholesale_id %>" />
            <input type="hidden" id="Promotion_WholeSale_ProductID" name="Promotion_WholeSale_ProductID" value="<%=Promotion_WholeSale_ProductID %>" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location='promotion_list.aspx';"/></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
  </table>
</div>
</body>
</html>
