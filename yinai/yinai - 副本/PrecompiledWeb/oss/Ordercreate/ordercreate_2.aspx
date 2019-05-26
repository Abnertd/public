<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">
    
    private ITools tools;
    private OrdersProcess MyApp;
    int member_id;
    Addr addr;
    private Member Mymem;
    MemberInfo memberinfo = null;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["total_price"] = 0;
        Session["orders_create"] = true;
        Public.CheckLogin("9d8d62af-29b1-4302-957c-268732fc15b4");

        tools = ToolsFactory.CreateTools();
        MyApp = new OrdersProcess();
        Mymem = new Member();
        addr = new Addr();

        member_id = tools.CheckInt(Request["member_id"]);
        if (MyApp.Cart_Product_Count() == 0)
        {
            Public.Msg("error", "错误提示", "请选择要购买的产品！", false, "ordercreate_1.aspx");
            Response.End();
        }
        if (member_id == 0)
        {
            Public.Msg("error", "错误提示", "请选择购买用户！", false, "ordercreate_1.aspx");
            Response.End();
        }
        memberinfo = Mymem.GetMemberByID(member_id);
        if (memberinfo != null)
        {
            member_id = memberinfo.Member_ID;
        }
        else
        {
            member_id = 0;
        }
        if (member_id == 0)
        {
            Public.Msg("error", "错误提示", "请选择购买用户！", false, "ordercreate_1.aspx");
            Response.End();
        }

        //更新会员价格
        MyApp.Cart_Price_Update(member_id);
        
    }

    protected void Page_UnLoad(object sender, EventArgs e) {
        tools = null;
        memberinfo = null;
        MyApp = null;
        Mymem = null;

    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>无标题文档</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
<script src="/Scripts/jquery.js" type="text/javascript"></script>
<script src="/Scripts/common.js" type="text/javascript"></script>
<link type="text/css" href="/Scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
<script src="/Scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
<script src="/Scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script>
<script src="/Public/ckeditor/ckeditor.js" type="text/javascript"></script>
<link href="/Scripts/jqGrid/css/ui.jqgrid.css" rel="stylesheet" type="text/css" />
<script src="/Scripts/jqGrid/grid.locale-zh_CN.js" type="text/javascript"></script>
<script src="/Scripts/jqGrid/jquery.jqGrid.min.js" type="text/javascript"></script>

<style type="text/css">
    
    .tablebg_green{ background:#390;}
    .tablebg_green td{ background:#FFF;}
</style>

</head>
<body>

<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">收货信息</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="frm_order" name="frm_order" method="post" action="ordercreate_do.aspx">
			<table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
			  <tr>
				<td width="100" height="23" align="right" class="cell_title">地址选择</td>
				<td align="left" class="cell_content">
				<%MyApp.Member_Address(member_id); %>
				<div id="other_addr" style="display:none">
				<table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
				<tr>
				<td width="100" height="23" align="right">省份</td>
				<td align="left" class="cell_content">
				<input type="hidden" name="Orders_Address_Country" id="Orders_Address_Country" value="CN" />
				<input type="hidden" id="Orders_Address_State" name="Orders_Address_State" value="">
                <input type="hidden" id="Orders_Address_City" name="Orders_Address_City" value="">
                <input type="hidden" id="Orders_Address_County" name="Orders_Address_County" value="">
				<span id="textDiv"><%=addr.UOD_SelectAddress("textDiv", "Orders_Address_State", "Orders_Address_City", "Orders_Address_County", "", "", "")%></span> <span class="Required">*</span></td>
			  </tr>
			  <tr>
				<td width="100" height="23" align="right">收货地址</td>
				<td align="left" class="cell_content"><input name="Orders_Address_StreetAddress" type="text" id="Orders_Address_StreetAddress" size="60" maxlength="100" value=""/> <span class="Required">*</span></td>
			  </tr>
			  <tr>
				<td width="100" height="23" align="right">邮编</td>
				<td align="left" class="cell_content"><input name="Orders_Address_Zip" type="text" id="Orders_Address_Zip" size="20" maxlength="10" value=""/> <span class="Required">*</span></td>
			  </tr>
			  <tr>
				<td width="100" height="23" align="right">收货人姓名</td>
				<td align="left" class="cell_content"><input name="Orders_Address_Name" type="text" id="Orders_Address_Name" size="20" maxlength="50" value=""/> <span class="Required">*</span></td>
			  </tr>
			  <tr>
				<td width="100" height="23" align="right">联系电话</td>
				
				<td align="left" class="cell_content">
				<input name="Orders_Address_Phone_Countrycode" type="hidden" id="Orders_Address_Phone_Countrycode" value="+86" />
				<input name="Orders_Address_Phone_Number" type="text" id="Orders_Address_Phone_Number" size="20" maxlength="20" value=""/> <span class="Required">*</span></td>
			  </tr>
			    <tr>
				<td width="100" height="23" align="right">手机</td>
				<td align="left" class="cell_content"><input name="Orders_Address_Mobile" type="text" id="Orders_Address_Mobile" size="20" maxlength="20" value=""/> <span class="Required">*</span></td>
			  </tr>
				</table>
				</div>
				</td>
			  </tr>
			  <tr>
				<td width="100" height="23" align="right" class="cell_title">配送方式</td>
				<td align="left" class="cell_content" id="delivery_area">请选择收货地址
				</td>
			  </tr>
			  <tr>
				<td width="100" height="23" align="right" class="cell_title">支付方式</td>
				<td align="left" class="cell_content">
				<%=MyApp.Pay_Way_Select(1)%>
				</td>
			  </tr>
			  <tr>
				<td width="100" height="23" align="right" class="cell_title">送货时间</td>
				<td align="left" class="cell_content">
				<%=MyApp.DeliveryTime_Select()%>
				</td>
			  </tr>
			  <tr>
				<td width="100" height="23" align="right" class="cell_title">发票选项</td>
				<td align="left" class="cell_content">
				<table border="0" cellspacing="0" cellpadding="5">
				<tr>        
				<td height="30">发票类型</td>        
				<td><input type="radio" name="ticket_type" value="0" onclick="MM_findObj('common_ticket').style.display='none';"> 不需要发票 <input type="radio" name="ticket_type" value="1" onclick="MM_findObj('common_ticket').style.display='';" checked> 普通发票 </td>      
				</tr>        
				</table>        
				<table border="0" cellspacing="0" cellpadding="5" id="common_ticket">      
				<tr>        
				<td>发票抬头</td>        
				<td><input type="text" name="ticket_title" value="个人" size="40"> <span class="t12_red">*</span> 小于100个字符</td>      
				</tr>    
				</table>
				</td>
			  </tr>
			  <tr>
				<td width="100" height="23" align="right" class="cell_title">订单备注</td>
				<td align="left" class="cell_content">
				<input type="text" name="order_note" size="60" />
				</td>
			  </tr>
			  <tr>
				<td width="100" height="23" align="right" class="cell_title">管理员备注</td>
				<td align="left" class="cell_content">
				<input type="text" name="admin_note" size="60" />
				</td>
			  </tr>
			  <tr>
				<td width="100" height="23" align="right" class="cell_title">商品清单</td>
				<td align="left" class="cell_content">
				<%=MyApp.Orders_Goods_List( 0, true)%>
				</td>
			  </tr>
			  <tr>
				<td colspan="2">
				<table width="100%" border="0" cellspacing="0" cellpadding="0">
				<tr>
				
				<td width="100" height="23" align="right" class="cell_title">运费</td>
				<td align="left" class="cell_content" id="freight_fee">
				<%=Public.DisplayCurrency(0)%>
				</td>
				<td width="100" height="23" align="right" class="cell_title">总金额</td>
				<td align="left" class="cell_content" id="order_price">
				<%=Public.DisplayCurrency(tools.CheckFloat(Session["total_price"].ToString()))%>
				</td>
			  </tr>
			  <tr>
				<td width="100" height="23" align="right" class="cell_title">优惠金额</td>
				<td align="left" class="cell_content">
				<%=Public.DisplayCurrency(0)%>
				</td>
				<td width="100" height="23" align="right" class="cell_title">优惠备注</td>
				<td align="left" class="cell_content">
				
				</td>
			  </tr>
				</table>
				</td>
				</td>
			  </tr>
			</table>
			<table width="100%" border="0" cellspacing="0" cellpadding="5">
              <tr>
                <td align="right">
                <span class="t12_red">配送方式和支付方式修改后结果要匹配</span> <input name="button" type="submit" class="bt_orange" id="Submit2" value="保存订单" />
				    <input name="action" type="hidden" id="action" value="order_create" />
				    <input name="member_id" type="hidden" id="member_id" value="<%=member_id %>" />
                 </td>
              </tr>
            </table>
			</form>
      </td>
    </tr>
  </table>
</div>

<script type="text/javascript">

    
    function displaySubGoods(id){
        $("#subgoods_"+ id).toggle();
        if ($("#subgoods_"+ id).css("display") == "none"){ 
            $("#subicon_"+ id).attr("src", "/images/display_close.gif");
        }
        else{
            $("#subicon_"+ id).attr("src", "/images/display_open.gif");
        }
    }
</script>

</body>
</html>