<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="System.Collections.Generic" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    private Promotion myApp;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("fa1deb07-54f5-48b8-a8ff-d94c3c8990be");
        myApp = new Promotion();
        Session["selected_memberid"] = "";
        Session["selected_cateid"] = "";
        Session["selected_brandid"] = "";
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
</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">添加优惠券/代金券派发规则</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="/promotion/Promotion_Coupon_Rule_do.aspx" onsubmit="javascript:return check_favor_couponrule();">
      <input type="hidden" name="favor_target" value="1" id="favor_target1" />
      
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
			      <td class="cell_title">
                      优惠标题</td>
			      <td class="cell_content"><input name="Promotion_Coupon_Title" type="text" id="Promotion_Coupon_Title" size="40" maxlength="50" onblur="check_blank('Promotion_Coupon_Title')"> <span id="tip_Promotion_Coupon_Title"></span></td>
			    </tr>
			    <tr id="pro_tr">
			      <td class="cell_title">
                      范围选择</td>
			      <td class="cell_content"> 
			      <input type="radio" name="favor_productall" id="favor_productall0" value="0" checked onclick="iniproduct(0);"/>指定产品  <a href="" id="btn_product"><input type="button" value="选择" class="bt_orange"/></a><input type="hidden" name="favor_productid" id="favor_productid" />
                  <span id="tip_target"></span>
                  <div class="div_picker" id="product_picker"><span class="pickertip">全部产品</span></div>
                  </td>
			    </tr>
			    <tr>
			      <td class="cell_title">
                      使用条件</td>
			      <td class="cell_content"> 当购买总金额 ≥ <input type="text" name="Promotion_Coupon_Payline" id="Promotion_Coupon_Payline" value="0" size="10" onblur="check_payline('Promotion_Coupon_Payline')"/> 时 <span id="tip_Promotion_Coupon_Payline"></span>
                  </td>
			    </tr>
			    <tr>
			      <td class="cell_title">
                      优惠金额</td>
			      <td class="cell_content"> <input id="Promotion_Coupon_Manner1" name="Promotion_Coupon_Manner" type="radio" value="1" checked="CHECKED" /> 指定金额 <input type="text" size="10" id="Promotion_Coupon_Price" name="Promotion_Coupon_Price" value="0.00"/> 元 <input id="Promotion_Coupon_Manner2" name="Promotion_Coupon_Manner" type="radio" value="2" /> 优惠百分比 <input type="text" size="10" id="Promotion_Coupon_Percent" name="Promotion_Coupon_Percent" value="0"/> %   <span id="tip_Promotion_Coupon_Price"></span> <span id="tip_Promotion_Coupon_Percent"></span>
                  </td>
			    </tr>
			    
			    <tr>
			      <td class="cell_title">
			      有效期
                      </td>
			      <td class="cell_content"> <input type="text" name="favor_valid" id="favor_valid" value="1" size="10"/> 天 <span class="tip">有效天数应大于0</span>
          	    </td>
			    </tr>
			    <tr>
			      <td class="cell_title">
			      优惠备注
                      </td>
			      <td class="cell_content"> <input type="text" name="Promotion_Coupon_Note" id="Text1" value="" size="40"/> <span class="tip">使用该优惠时记录的说明信息,如：因购买金额超过{payline}元,使用优惠券：券号：{coupon_code}；验证码：{verifycode},价格优惠￥{favorprice}元</span>
          	    </td>
			    </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="action" name="action" value="new" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location='promotion_favor_coupon_list.aspx';"/></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
  </table>
</div>
<script type="text/javascript">
$("#btn_member").click(function(){
    $("#btn_member").attr("href","member_check.aspx?memberid="+ $("#favor_memberid").val()+"&timer=" + Math.random());						   
});
$("#btn_cate").click(function(){
    $("#btn_cate").attr("href","cate_check.aspx?cateid="+ $("#favor_cateid").val()+"&timer=" + Math.random());						   
});
$("#btn_brand").click(function(){
    $("#btn_brand").attr("href","brand_check.aspx?cateid="+ $("#favor_cateid").val() +"&timer=" + Math.random());						   
});
$("#btn_product").click(function(){
    $("#btn_product").attr("href","product_check.aspx?timer=" + Math.random());						   
});
$("#btn_product").zxxbox({height:450, width:600,title:'',bar:false,btnclose: false});
$("#btn_brand").zxxbox({height:450, width:600,title:'',bar:false,btnclose: false});
$("#btn_cate").zxxbox({height:300, width:600,title:'',bar:false,btnclose: false});
$("#btn_member").zxxbox({height:450, width:600,title:'',bar:false,btnclose: false});
</script>
<script src="/Scripts/promotion.js" type="text/javascript"></script>
</body>
</html>
