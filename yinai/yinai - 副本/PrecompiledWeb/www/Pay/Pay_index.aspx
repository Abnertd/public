<%@ Page Language="C#" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<%@ Register src="../Public/Top.ascx" tagname="Top" tagprefix="uc1" %>
<%@ Register src="../Public/Bottom.ascx" tagname="Bottom" tagprefix="uc2" %>
<%@ Register src="../Public/PayHTML.ascx" tagname="PayHTML" tagprefix="uc3" %>
<% 
    Public_Class pub = new Public_Class();
    ITools tools;
    tools = ToolsFactory.CreateTools();
    Pay pay=new Pay();
    Cart cart = new Cart();
    Orders orders = new Orders();
    Member MEM = new Member();
    int paywayid, payway_cod;
    string btn_value = "现在支付";
    payway_cod = 0;
    paywayid = 0;
    string sn = tools.CheckStr(Request["sn"]);
    int order_id = 0;
    double orders_price = 0;
    string payway_name, delivery_name;
    payway_name = "";
    delivery_name = "";
    string order_sn = "";
        OrdersInfo ordersinfo = orders.GetOrdersInfoBySN(sn);
        if (ordersinfo != null)
        {
            if (ordersinfo.Orders_BuyerID == tools.CheckInt(Session["member_id"].ToString()) && ordersinfo.Orders_PaymentStatus == 0)
            {
                order_sn = ordersinfo.Orders_SN;
                order_id = ordersinfo.Orders_ID;
                orders_price = ordersinfo.Orders_Total_AllPrice;
                paywayid = ordersinfo.Orders_Payway;
                payway_name = ordersinfo.Orders_Payway_Name;
                delivery_name = ordersinfo.Orders_Delivery_Name;
            }
            else
            {
                Response.Redirect("/member/index.aspx");
            }
        }
        else
        {
            Response.Redirect("/member/index.aspx");
        }
        PayWayInfo payway = orders.GetPayWayByID(paywayid);
        if (payway != null)
        {
            payway_cod = payway.Pay_Way_Cod;
        }
        if (payway_cod == 1)
        {
            btn_value = "支付说明";
        }
    
    
    //MEM.Member_Login_Check("/pay/pay_index.aspx?sn=" + sn);
     
    
    %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title><%="订单支付 - " + pub.SEO_TITLE()%></title>
     <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
<meta name="Description" content="<%=Application["Site_Description"]%>" />
<link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/scripts/common.js"></script>
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <style type="text/css">
#div_hk p{height:30px; line-height:30px; color:#333333;}
.a{border-bottom:1px solid #8fd58f;}
.b{ background-image:url(/images/opt.jpg);line-height:30px;text-align:center;width:102px;height:30px; background-repeat:no-repeat;
    font-size:14px;font-weight:bold;color:#278429; cursor:pointer;}
.c{cursor:pointer;width:102px;text-align:center;border-bottom:1px solid #8fd58f;font-size:14px; cursor:pointer;}
    </style>
        <style type="text/css">
        .img_border{ border:1px solid #d5d5d5;}
        .table_filter {margin-top:10px;}
        .pay_intro {line-height:25px; padding:10px;}
        .table_filter td{padding:6px;}
    </style>
</head>
<body>
<uc1:Top ID="Top1" runat="server" />
<div class="yz_content">
    <div class="yz_content_main">
        <!--位置说明 开始-->
        <div class="yz_position">
            您现在的位置 > <a href="/">首页</a> > <a href="index.aspx">会员中心</a> > 订单支付
        </div>

<div style="width:980px; margin:0 auto;">
    	
       <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" class="table_filter" style="border:1px solid #cccccc">
    	<tr><td>
    	
       <table width="95%" border="0" align="center" cellpadding="5" cellspacing="0">
       <tr><td height="5"></td></tr>
        <tr>
	        <td align="center">
	        <%if (btn_value == "现在支付")
           { %>
	        <img src="/images/tijiao_success1.jpg" />
	        <%}else{ %>
	        <img src="/images/tijiao_success.jpg" />
	        <%} %>
	        </td>
        </tr>
       </table>
       
       <table width="93%" border="0" align="center" cellpadding="6" cellspacing="1" style="border:1px solid #cccccc">
    	<tr bgcolor="#cccccc">
    	<td width="25%" align="center" class="t12_black">订单号</td>
    	<td width="25%" align="center" class="t12_black">需支付的金额（元）</td>
    	<td width="25%" align="center" class="t12_black">支付方式</td>
    	<td width="25%" align="center" class="t12_black">配送方式</td>
    	</tr>
    	<tr>
    	<td width="25%" align="center" style="border-right:1px solid #DCDFE5; font-weight:normal;" class="t12_black"><%=sn%></td>
    	<td width="25%" align="center" style="border-right:1px solid #DCDFE5; font-weight:normal;" class="t12_black"><span class="t12_red"><%=pub.FormatCurrency(orders_price)%></span></td>
    	<td width="25%" align="center" style="border-right:1px solid #DCDFE5; font-weight:normal;" class="t12_black"><%=payway_name%></td>
    	<td width="25%" align="center" class="t12_black" style="font-weight:normal;"><%=delivery_name%></td>
    	</tr>
    	</table>
    	
      <table width="93%" border="0" align="center" cellpadding="5" cellspacing="0">
       <tr><td height="0"></td></tr>
        <tr>
	        <%if (btn_value == "现在支付")
          { %>
	        <td style="line-height:20px; font-size:13px; font-weight:bold; color:#FF6600;">还差一步,请立即支付(请您在72小时内付清款项,否则订单会被自动取消)</td>
        <%}
      else
      { %>
          <td style="line-height:20px; font-size:13px;">您可以在“<a href="/member/order_processing.aspx" class="a_t12_blue" style=" font-weight:bold;">我的订单</a>”中查看或取消您的订单，由于系统需进行订单预处理，您可能不会立刻查询到刚提交的订单</td>
	   <%} %>
        </tr>  
        <tr><td height="0"></td></tr>      
       </table>       
       </td></tr>
       </table>
    </div>
    <style type="text/css">
#div_hk p{height:30px; line-height:30px; color:#333333;}
.a{border-bottom:1px solid #cccccc;}
.b{ background-image:url(/images/opt.jpg);line-height:30px;text-align:center;width:102px;height:30px; background-repeat:no-repeat;
    font-size:14px;font-weight:bold;color:#666666; cursor:pointer;}
.c{cursor:pointer;width:102px;text-align:center;border-bottom:1px solid #cccccc;font-size:14px; cursor:pointer;}
    </style>

<div style="width:960px; margin:0 auto; margin-top:0px; padding-top:0px;">
<table style="width:960px; margin-top:10px;" border="0" cellpadding="0" cellspacing="0">
  <tr>
    <td class="a" width="125" style="font-weight:bold; color:#333333; font-size:14px; padding-left:25px;">请选择支付方式</td>
    <%if (paywayid == 8)
      { %>
    <td class="c" id="td_zf"><a onmouseover="$('#td_hk').attr('class','c');$('#td_zf').attr('class','b');$('#img_zf').show();$('#div_zf').show();$('#div_hk').hide();">在线支付</a></td>
    <td class="b" id="td_hk"><a onmouseover="$('#td_hk').attr('class','b');$('#td_zf').attr('class','c');$('#img_zf').hide();$('#div_zf').hide();$('#div_hk').show();">银行汇款</a></td>
    <%}
      else
      { %>
    <td class="b" id="td_zf"><a onmouseover="$('#td_hk').attr('class','c');$('#td_zf').attr('class','b');$('#img_zf').show();$('#div_zf').show();$('#div_hk').hide();">在线支付</a></td>
    <td class="c" id="td_hk"><a onmouseover="$('#td_hk').attr('class','b');$('#td_zf').attr('class','c');$('#img_zf').hide();$('#div_zf').hide();$('#div_hk').show();">银行汇款</a></td>
    <%} %>
    <td class="a">&nbsp;</td>
  </tr>
</table>
<form name="form1" id="form1" method="post" action="/pay/pay_do.aspx">
<%if (paywayid == 8)
  { %>
<div id="div_zf" style=" border:1px solid #cccccc; border-top:0px; display:none;">
<%}
  else
  { %>
<div id="div_zf" style=" border:1px solid #cccccc; border-top:0px;">
<%} %>
<p style="height:35px; line-height:35px; font-size:13px; padding-left:26px; color:#333333; padding-top:10px;">国内银行卡或信用卡</p>
<input type="hidden" value="0001" name="pay_bank" id="pay_bank" />
<table style="width:800px; margin-left:50px; margin:0 auto;" border="0" cellpadding="0" cellspacing="0">
<tr>
<td style="width:20%; height:50px;"><input type="radio" value="CHINAPAY" checked="checked" onclick="$('#pay_bank').attr('value','0001');" name="pay_type" style=" margin-top:10px; margin-right:11px; float:left;" /><img style=" float:left;" alt="银联" title="银联" src="/images/logo_chinapay.jpg" /></td>
<td style="width:20%; height:50px;"><input type="radio" name="pay_type" value="99BILL" onclick="$('#pay_bank').attr('value','ABC');" style=" margin-top:10px; float:left; margin-right:11px;"/><img alt="中国农业银行" title="中国农业银行" src="/images/logo_99bill_abc.gif" /></td>
<td style="width:20%; height:50px;"><input type="radio" name="pay_type" value="99BILL" onclick="$('#pay_bank').attr('value','BOC');" style=" margin-top:10px; float:left; margin-right:11px;"/><img alt="中国银行" title="中国银行" src="/images/logo_99bill_boc.gif" /></td>
<td style="width:20%; height:50px;"><input type="radio" name="pay_type" value="99BILL" onclick="$('#pay_bank').attr('value','CCB');" style=" margin-top:10px; float:left; margin-right:11px;"/><img alt="中国建设银行" title="中国建设银行" src="/images/logo_ccb_b2c.gif" /></td>
<td style="width:20%; height:50px;"><input type="radio" name="pay_type" value="99BILL" onclick="$('#pay_bank').attr('value','CEB');" style=" margin-top:10px; float:left; margin-right:11px;"/><img alt="中国光大银行" title="中国光大银行" src="/images/logo_99bill_ceb.gif" /></td>
</tr>
<tr>
<td style="width:20%; height:50px;"><input type="radio" name="pay_type" value="99BILL" onclick="$('#pay_bank').attr('value','CMB');" style=" margin-top:10px; float:left; margin-right:11px;"/><img alt="招商银行" title="招商银行" src="/images/logo_cmb.gif" /></td>
<td style="width:20%; height:50px;"><input type="radio" name="pay_type" value="99BILL" onclick="$('#pay_bank').attr('value','ICBC');" style=" margin-top:10px; float:left; margin-right:11px;"/><img alt="中国工商银行" title="中国工商银行" src="/images/logo_icbc_perbank_b2c.gif" /></td>
<td style="width:20%; height:50px;"><input type="radio" name="pay_type" value="99BILL" onclick="$('#pay_bank').attr('value','CMBC');" style=" margin-top:10px; float:left; margin-right:11px;"/><img alt="中国民生银行" title="中国民生银行" src="/images/logo_alipay_cmbc.gif" /></td>
<td style="width:20%; height:50px;"><input type="radio" name="pay_type" value="99BILL" onclick="$('#pay_bank').attr('value','CIB');" style=" margin-top:10px; float:left; margin-right:11px;"/><img alt="兴业银行" title="兴业银行" src="/images/logo_99bill_cib.gif" /></td>
<td style="width:20%; height:50px;"><input type="radio" name="pay_type" value="99BILL" onclick="$('#pay_bank').attr('value','SPDB');" style=" margin-top:10px; float:left; margin-right:11px;"/><img alt="上海浦东发展银行" title="上海浦东发展银行" src="/images/logo_alipay_spdb.gif" /></td>
</tr>
<tr>
<td style="width:20%; height:50px;"><input type="radio" name="pay_type" value="99BILL" onclick="$('#pay_bank').attr('value','CITIC');" style=" margin-top:10px; float:left; margin-right:11px;"/><img alt="中信银行" title="中信银行" src="/images/logo_99bill_citic.gif" /></td>
<td style="width:20%; height:50px;"><input type="radio" name="pay_type" value="99BILL" onclick="$('#pay_bank').attr('value','GDB');" style=" margin-top:10px; float:left; margin-right:11px;"/><img alt="广东发展银行" title="广东发展银行" src="/images/logo_99bill_gdb.gif" /></td>
<td style="width:20%; height:50px;"><input type="radio" name="pay_type" value="99BILL" onclick="$('#pay_bank').attr('value','HZB');" style=" margin-top:10px; float:left; margin-right:11px;"/><img alt="杭州银行" title="杭州银行" src="/images/logo_99bill_hzb.gif" /></td>
<td style="width:20%; height:50px;"><input type="radio" name="pay_type" value="99BILL" onclick="$('#pay_bank').attr('value','SDB');" style=" margin-top:10px; float:left; margin-right:11px;"/><img alt="深圳发展银行" title="深圳发展银行" src="/images/logo_99bill_sdb.gif" /></td>
<td style="width:20%; height:50px;"><input type="radio" name="pay_type" value="99BILL" onclick="$('#pay_bank').attr('value','BCOM');" style=" margin-top:10px; float:left; margin-right:11px;"/><img alt="中国交通银行" title="中国交通银行" src="/images/logo_alipay_comm.gif" /></td>
</tr>
</table>
<p style="height:35px; line-height:35px; font-size:13px; padding-left:26px; color:#333333; padding-top:0px;">支付平台</p>
<table style="width:800px; margin:0 auto;" border="0" cellpadding="0" cellspacing="0">
<tr>
<td style="width:20%; height:50px;"><input type="radio" value="ALIPAY" name="pay_type" style=" margin-top:10px; float:left; margin-right:11px;"/><img style=" float:left;" src="/images/logo_alipay.gif" /></td>
<td style="width:20%; height:50px;"><input type="radio" value="TENPAY" name="pay_type" style=" margin-top:10px; margin-right:11px; float:left;" /><img style=" float:left;" src="/images/logo_tenpay.gif" /></td>
<td style="width:20%; height:50px;"><input type="radio" value="CMPAY" name="pay_type" style=" margin-top:10px; margin-right:11px; float:left;" /><img style=" float:left;" alt="手机支付" title="手机支付" src="/images/logo_cmpay.gif" /></td>
<td style="width:20%; height:50px;"><input type="radio" value="CHINAPAY" name="pay_type" onclick="$('#pay_bank').attr('value','');" style=" margin-top:10px; margin-right:11px; float:left;" /><img style=" float:left;" alt="银联" title="银联" src="/images/logo_chinapaykj.jpg" /></td>
<td style="width:20%; height:50px;"></td>
</tr>
</table>
</div>
<% if (paywayid != 8)
   { %>
<div id="div_hk" style=" border:1px solid #cccccc; border-top:0px; padding-bottom:10px; display:none;">
<%}
   else
   { %>
<div id="div_hk" style=" border:1px solid #cccccc; border-top:0px; padding-bottom:10px;">
<%} %>
<p style="height:35px; line-height:35px; font-size:13px; padding-left:26px; color:#333333; padding-top:10px;">温馨提示：您付款成功后务必及时通知我们，以便我们能及时为您发货。</p>
<%=cart.GetPayWayByActive()%>
</div>
<div style=" font-size:14px; background-color:#ffffff; color:#333333; padding-top:10px;">
<% if (paywayid == 8)
   { %>
<img src="/images/zhifu.jpg" onclick="$('#form1').submit();" id="img_zf" style=" float:right; cursor:pointer; display:none; " />
<%}
   else
   { %>
<img src="/images/zhifu.jpg" onclick="$('#form1').submit();" id="img_zf" style=" float:right; cursor:pointer; " />
<%} %>
<p style="height:42px; line-height:42px; float:right;">完成支付后，您可以：<a href="/member/order_detail.aspx?orders_sn=<%=order_sn %>" class="a_t12_orange">查看订单详情</a>&nbsp;&nbsp;<a href="/index.aspx" class="a_t12_orange">继续购物</a>&nbsp;&nbsp;&nbsp;&nbsp;</p></div>
<input type="hidden" name="Order_ID" value="<%=order_id %>" />

</form>
<div class="clear"></div>   
</div> 
</div>
</div>   
    <uc2:Bottom ID="Page_Bottom" runat="server" />

</body>
</html>

