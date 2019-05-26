<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    private Promotion myApp;
    private ITools tools;
    private PromotionLimitInfo entity;
    private ProductInfo product;
    private Product productlcass;
    int limit_id, Promotion_Limit_ProductID, limit_amount, limit_limit,group_id;
    string product_code, product_name, product_spec, product_maker, Promotion_Limit_Starttime, Promotion_Limit_Endtime,Grade_Str;
    double product_price, Promotion_Limit_Price;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("34b7b99f-451c-4c0b-8da1-e3ba000891a8");
        tools = ToolsFactory.CreateTools();
        myApp = new Promotion();
        productlcass=new Product();
        product_code = "";
        product_maker = "";
        product_spec = "";
        product_name = "";
        product_price = 0;
        group_id = 0;
        limit_id = tools.CheckInt(Request["promotion_limit_id"]);
        entity = myApp.GetPromotionLimitByID(limit_id);
        if (entity == null)
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        else
        {
            product = productlcass.GetProductByID(entity.Promotion_Limit_ProductID);
            if (product != null)
            {
                product_code = product.Product_Code;
                product_name = product.Product_Name;
                product_spec = product.Product_Spec;
                product_maker = product.Product_Maker;
                product_price = product.Product_Price;
            }
            group_id = entity.Promotion_Limit_GroupID;
            Promotion_Limit_ProductID = entity.Promotion_Limit_ProductID;
            Promotion_Limit_Price = entity.Promotion_Limit_Price;
            Promotion_Limit_Starttime = entity.Promotion_Limit_Starttime.ToString();
            Promotion_Limit_Endtime = entity.Promotion_Limit_Endtime.ToString();
            limit_amount = entity.Promotion_Limit_Amount;
            limit_limit = entity.Promotion_Limit_Limit;
            Grade_Str=myApp.Get_Limit_MemberGrade_String(entity.PromotionLimitMemberGrades);
            
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
      <td class="content_title">限时促销</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="/promotion/Promotion_limit_do.aspx" onsubmit="javascript:return check_membergrade('Member_Grade');">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
         <tr>
	      <td class="cell_title">
	      促销分组
              </td>
	      <td class="cell_content"> <%myApp.GetPromotionLimitGroups(group_id); %></td>
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
	              针对会员
                      </td>
	              <td class="cell_content"> <%myApp.Member_Grade_Check("Member_Grade", Grade_Str); %> &nbsp; <span id="tip_membergrade"></span></td>
	            </tr> 
	            
        <tr>
			      <td class="cell_title">
                      限时价格</td>
			      <td class="cell_content"><input name="Promotion_Limit_Price" type="text" id="Promotion_Limit_Price" value="<%=Promotion_Limit_Price %>" size="40" maxlength="50"></td>
			    </tr>
			    <%--<tr>
			      <td class="cell_title">
                      参与数量</td>
			      <td class="cell_content"><input name="Promotion_Limit_Amount" type="text" id="Promotion_Limit_Amount" value="<%=limit_amount %>" size="40" maxlength="50"></td>
			    </tr>--%>
			    <tr>
			      <td class="cell_title">
                      每单限购数量</td>
			      <td class="cell_content"><input name="Promotion_Limit_Limit" type="text" id="Promotion_Limit_Limit" value="<%=limit_limit %>" size="40" maxlength="50"></td>
			    </tr>
        <tr>
			      <td class="cell_title">
			      开始时间
                      </td>
			      <td class="cell_content"> <input name="Promotion_Limit_Starttime" value="<%=Promotion_Limit_Starttime %>" type="text" id="Promotion_Limit_Starttime" size="40" maxlength="50"> &nbsp; </td>
			    </tr>   
			    <tr>
			      <td class="cell_title">
			      结束时间
                      </td>
			      <td class="cell_content"> <input name="Promotion_Limit_Endtime" value="<%=Promotion_Limit_Endtime %>" type="text" id="Promotion_Limit_Endtime" size="40" maxlength="50"></td>
			    </tr> 
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="action" name="action" value="renew" />
            <input type="hidden" id="promotion_limit_id" name="promotion_limit_id" value="<%=limit_id %>" />
            <input type="hidden" id="Promotion_Limit_ProductID" name="Promotion_Limit_ProductID" value="<%=Promotion_Limit_ProductID %>" />
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
