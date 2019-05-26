<%@ Control Language="C#" ClassName="PayHTML" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<% 
    
    Public_Class pub = new Public_Class();
    ITools tools;
    tools = ToolsFactory.CreateTools();
    Pay pay = new Pay();
    Cart cart = new Cart();
    Orders order = new Orders();
    Member MEM = new Member();    
    string sn = tools.CheckStr(Request["sn"]);
    int order_id = 0;
    int paywayid = 0;
    OrdersInfo entity = order.GetOrdersInfoBySN(sn);
    string order_sn = "";
    if (entity != null)
    {
        order_sn = entity.Orders_SN;
        order_id = entity.Orders_ID;
        paywayid = entity.Orders_Payway;
        if (entity.Orders_PaymentStatus != 0 || entity.Orders_BuyerID != tools.NullInt(Session["member_id"]))
        {
            Response.Redirect("/index.aspx");
        }
    }
    else
    {
        Response.Redirect("/index.aspx");
    }     
    
    %>

    <style type="text/css">
#div_hk p{height:30px; line-height:30px; color:#333333;}
.a{border-bottom:1px solid #8fd58f;}
.b{ background-image:url(/images/opt.jpg);line-height:30px;text-align:center;width:102px;height:30px; background-repeat:no-repeat;
    font-size:14px;font-weight:bold;color:#278429; cursor:pointer;}
.c{cursor:pointer;width:102px;text-align:center;border-bottom:1px solid #8fd58f;font-size:14px; cursor:pointer;}
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
<form name="form1" id="form1" method="post" action="/pay/pay_do.aspx" target="_blank">
<%if (paywayid == 8)
  { %>
<div id="div_zf" style=" border:1px solid #8FD58F; border-top:0px; display:none;">
<%}
  else
  { %>
<div id="div_zf" style=" border:1px solid #8FD58F; border-top:0px;">
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
<td style="width:20%; height:50px;"><input type="radio" value="99BILL" onclick="$('#pay_bank').attr('value','');" name="pay_type" style=" margin-top:10px; margin-right:11px; float:left;" /><img style=" float:left;" src="/images/logo_99bill.gif" /></td>
<td style="width:20%; height:50px;"><input type="radio" value="CMPAY" name="pay_type" style=" margin-top:10px; margin-right:11px; float:left;" /><img style=" float:left;" alt="手机支付" title="手机支付" src="/images/logo_cmpay.gif" /></td>
<td style="width:20%; height:50px;"><input type="radio" value="CHINAPAY" name="pay_type" onclick="$('#pay_bank').attr('value','');" style=" margin-top:10px; margin-right:11px; float:left;" /><img style=" float:left;" alt="银联" title="银联" src="/images/logo_chinapaykj.jpg" /></td>
</tr>
</table>
</div>
<% if (paywayid != 8)
   { %>
<div id="div_hk" style=" border:1px solid #8FD58F; border-top:0px; padding-bottom:10px; display:none;">
<%}
   else
   { %>
<div id="div_hk" style=" border:1px solid #8FD58F; border-top:0px; padding-bottom:10px;">
<%} %>
<p style="height:35px; line-height:35px; font-size:13px; padding-left:26px; color:#333333; padding-top:10px;">温馨提示：您付款成功后务必及时通知我们，以便我们能及时为您发货。</p>
<%=cart.GetPayWayByActive()%>
</div>
<div style=" font-size:14px; background-color:#ffffff; color:#333333; padding-top:10px;">
<%if (paywayid == 8)
  { %>
<img src="/images/zhifu.jpg" onclick="$('#form1').submit();" id="img_zf" style=" float:right; cursor:pointer; display:none; " />
<%}else{ %>
<img src="/images/zhifu.jpg" onclick="$('#form1').submit();" id="img_zf" style=" float:right; cursor:pointer;" />
<%} %>
<p style="height:42px; line-height:42px; float:right; margin-bottom:10px;">完成支付后，您可以：<a href="/member/order_detail.aspx?orders_sn=<%=order_sn %>" class="a_t12_orange">查看订单详情</a>&nbsp;&nbsp;<a href="/index.aspx" class="a_t12_orange">继续购物</a>&nbsp;&nbsp;&nbsp;&nbsp;</p></div>
<input type="hidden" name="Order_ID" value="<%=order_id %>" />

</form>   
</div>



