<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="System.Collections.Generic" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    private Promotion myApp;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("b2170460-e90d-4b4f-89b6-c88b75c2989b");
        myApp = new Promotion();
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
<script src="/Scripts/common.js" type="text/javascript"></script>
<script src="/Scripts/promotion.js" type="text/javascript"></script>
<script src="/Scripts/jquery.js" type="text/javascript"></script>
<script src="/Scripts/jquery.zxxbox.3.0.js" type="text/javascript"></script>
<link type="text/css" href="/Scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
<script src="/Scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
<script src="/Scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script>
<script type="text/javascript">
<!--
change_inputcss();
function add_gift(group)
{
    var maxnum=$("#maxnum_" + group).val(); 
    maxnum=parseInt(String(maxnum));
    $.ajaxSetup({async: false});
    
    
    $("#gift_item").load("promotion_favor_gift_do.aspx?action=addgift&group=" + group + "&maxnum=" + (maxnum+1) + "&timer=" + Math.random());
    $("#gift_more" + group).html($("#gift_more" + group).html()+$("#gift_item").html());
    $("#maxnum_" + group).val((maxnum+1));
}

function del_gift(obj)
{
    $("#" + obj).html("");
    $("#" + obj).hide();
}

function add_item()
{
    var group=$("#group").val(); 
    group=parseInt(String(group));
    $.ajaxSetup({async: false});
    
    
    $("#gift_item").load("promotion_favor_gift_do.aspx?action=additem&group=" + (group+1) + "&timer=" + Math.random());
    $("#gift_content").html($("#gift_content").html()+$("#gift_item").html());
    $("#group").val((group+1));
    //alert($("#group").val());
}

function del_item(obj)
{
    for(var i=0;i< $('div').length;i++)
        {
            if($('div:eq('+i+')').attr('id').indexOf(obj) >= 0) {
                $('div:eq('+i+')').html("");
                $('div:eq('+i+')').hide();
            }
        }
        //alert($("#group").val());
}

//-->
</script>
<style>
    .cell_content div
    {
    	padding:5px 0px;
    	}
</style>
</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">添加赠品优惠</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="/promotion/Promotion_Favor_gift_do.aspx" onsubmit="javascript:return check_favor_gift();">
      <input type="hidden" name="favor_target" value="1" id="favor_target1" />
      
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
  
        <tr>
			      <td class="cell_title">
                      优惠标题</td>
			      <td class="cell_content"><input name="Promotion_Gift_Title" type="text" id="Promotion_Gift_Title" size="40" maxlength="50" onblur="check_blank('Promotion_Gift_Title')"> <span id="tip_Promotion_Gift_Title"></span></td>
			    </tr>
			    <tr id="pro_tr">
			      <td class="cell_title">
                      范围选择</td>
			      <td class="cell_content"> 
			      <input type="radio" name="favor_productall" id="favor_productall0" checked value="0" onclick="iniproduct(0);"/>指定产品  <a href="" id="btn_product"><input type="button" value="选择" class="bt_orange"/></a><input type="hidden" name="favor_productid" id="favor_productid" />
                  <span id="tip_target"></span>
                  <div class="div_picker" id="product_picker"><span class="pickertip">全部产品</span></div>
                  </td>
			    </tr>
			    <tr>
	              <td class="cell_title">
	              针对会员
                      </td>
	              <td class="cell_content"> <%myApp.Member_Grade_Check("Member_Grade", "0"); %> &nbsp; <span id="tip_membergrade"></span></td>
	            </tr> 
			    <tr>
			      <td class="cell_title">
                      优惠排除</td>
			      <td class="cell_content"> <input type="checkbox" name="Promotion_Gift_Group" value="1" /> 团购产品  <input type="checkbox" name="Promotion_Gift_Limit" value="1" /> 限时产品
                  </td>
			    </tr>
			    <tr>
			      <td class="cell_title">
                      叠加使用</td>
			      <td class="cell_content"> <input type="radio" name="Promotion_Gift_IsRepeat" value="1" /> 支持  <input type="radio" name="Promotion_Gift_IsRepeat" value="0" checked /> 不支持
                  </td>
			    </tr>
			    <%
                    int i;

         %>
			     <tr>
			      <td class="cell_title">
                      设置赠品</td>
			      <td class="cell_content" id="gift_content">
			     <div id="gift_1">购买金额 ≥ <input type="text" name="favor_buy_money1" value="0" size="10"/>  &nbsp;购买数量 ≥ <input type="text" name="favor_buy_amount1" value="0" size="10"/><input name="maxnum_1" id="maxnum_1" type="hidden" value="1" />  <a href="javascript:void(0);" onclick="add_item();"><span class="t12_blue">添加可选项目</span></a></div>
                  
                  <div id="gift_1_1"><%myApp.Promotion_Gift("promotion_gift1_1",0); %>&nbsp; 赠送数量 <input type="text" name="favor_buy_gift1_amount_1" value="0" size="10"/> <a href="javascript:void(0);" onclick="add_gift(1);"><span class="t12_blue">添加赠品</span></a></div> 
			       <span id="gift_more1"></span>
                  </td>
			    </tr>

			    
			    
			    <tr>
			      <td class="cell_title">
			      有效时间
                      </td>
			      <td class="cell_content"> <input type="text" class="input_calendar" name="Promotion_Gift_Starttime" id="StartDate" maxlength="10" readonly="readonly" /> - <input type="text" class="input_calendar" name="Promotion_Gift_Endtime" id="EndDate" maxlength="10" readonly="readonly" /><span id="tip_valid"></span>
                  <script type="text/javascript">
                      $(document).ready(function() {
                          $("#StartDate").datepicker({ numberOfMonths: 1 });
                          $("#EndDate").datepicker({ numberOfMonths: 1 });
                      });
                  </script>
                  
          	</td>
			    </tr>
			    <tr>
          <td class="cell_title">优先级</td>
          <td class="cell_content"><input name="Favor_Sort" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" type="text" id="Favor_Sort" size="10" maxlength="10" value="1" />
          <span class="tip">数字越小越靠前</span></td>
        </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="action" name="action" value="new" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存" /><input name="group" id="group" type="hidden" value="1" />
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location='promotion_favor_gift_list.aspx';"/></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
  </table>
</div>
<div id="gift_item" style="display:none"></div>
<script type="text/javascript">
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

</script>
<script src="/Scripts/promotion.js" type="text/javascript"></script>
</body>
</html>
