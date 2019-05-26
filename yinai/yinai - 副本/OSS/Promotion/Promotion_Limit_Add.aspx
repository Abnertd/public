<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    private Promotion myApp;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("33713ac1-24a8-40af-b122-b60c1109f347");
        myApp = new Promotion();
        Session["selected_productid"] = "";
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>无标题文档</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
<script src="/Scripts/jquery.js" type="text/javascript"></script>
<script src="/Scripts/jquery.zxxbox.3.0.js" type="text/javascript"></script>
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
      <td class="content_title">添加限时促销</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="/promotion/Promotion_limit_do.aspx" onsubmit="javascript:return check_membergrade('Member_Grade');">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
	      <td class="cell_title">
	      促销分组
              </td>
	      <td class="cell_content"> <%myApp.GetPromotionLimitGroups(0); %></td>
	    </tr>
			<tr>
          <td class="cell_title" valign="top">商品选择</td>
          <td class="cell_content">
          
          
          <div id="product_picker"><% =myApp.Limit_ShowProduct()%></div>
          <a href="" id="btn_product"><input type="button" value="添加" class="bt_orange"></a>
          <input type="hidden" name="favor_productid" id="favor_productid" /></td>
        </tr> 
        <tr>
	      <td class="cell_title">
	      针对会员
              </td>
	      <td class="cell_content"> <%myApp.Member_Grade_Check("Member_Grade", "0"); %> &nbsp; <span id="tip_membergrade"></span></td>
	    </tr> 
        <tr>
	      <td class="cell_title">
	      开始时间
              </td>
	      <td class="cell_content"> <input name="Promotion_Limit_Starttime" value="<%=DateTime.Now %>" type="text" id="Promotion_Limit_Starttime" size="40" maxlength="50"> &nbsp; </td>
	    </tr>   
	    <tr>
	      <td class="cell_title">
	      结束时间
              </td>
	      <td class="cell_content"> <input name="Promotion_Limit_Endtime" value="<%=DateTime.Now %>" type="text" id="Promotion_Limit_Endtime" size="40" maxlength="50"></td>
	    </tr> 
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="action" name="action" value="new" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location='promotion_list.aspx';"/></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
  </table>
</div>
<script type="text/javascript">
$("#btn_product").click(function(){
    $("#btn_product").attr("href","product_check.aspx?timer=" + Math.random());						   
});
$("#btn_product").zxxbox({height:450, width:600,title:'',bar:false,btnclose: false});

</script>
<script src="/Scripts/promotion.js" type="text/javascript"></script>
</body>
</html>
