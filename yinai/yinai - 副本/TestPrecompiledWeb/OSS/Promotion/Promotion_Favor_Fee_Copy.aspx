<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">
    private Promotion myApp;
    private ITools tools;
    private PromotionFavorFeeInfo favorfee;
    private string favor_title;
    private double Promotion_Fee_Payline, Promotion_Fee_Price;
    private int Promotion_Fee_Manner, favor_id, Promotion_Fee_Sort, Promotion_Fee_Target;
    private string Promotion_Fee_Starttime, Promotion_Fee_Endtime, State_str, Cate_Str,Brand_Str, Delivery_Str, Payway_Str, Product_Str,Grade_Str;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("7cd745a6-dfce-4f33-8453-a64cc07c44c9");
        tools=ToolsFactory.CreateTools();
        favor_id =tools.CheckInt(Request["favor_id"]);

        myApp = new Promotion();
        favorfee = new PromotionFavorFeeInfo();
        favorfee = myApp.GetPromotionFavorFeeByID(favor_id);
        if (favorfee != null)
        {
            favor_title = favorfee.Promotion_Fee_Title;
            Promotion_Fee_Target = favorfee.Promotion_Fee_Target;
            Promotion_Fee_Payline = favorfee.Promotion_Fee_Payline;
            Promotion_Fee_Price = favorfee.Promotion_Fee_Price;
            Promotion_Fee_Manner = favorfee.Promotion_Fee_Manner;
            Promotion_Fee_Starttime = favorfee.Promotion_Fee_Starttime.ToShortDateString();
            Promotion_Fee_Endtime = favorfee.Promotion_Fee_Endtime.ToShortDateString();
            Promotion_Fee_Sort = favorfee.Promotion_Fee_Sort;
            State_str = myApp.Get_Fee_State_String(favorfee.PromotionFavorFeeDistricts);
            Cate_Str = myApp.Get_Fee_Cate_String(favorfee.PromotionFavorFeeCates);
            Brand_Str = myApp.Get_Fee_Brand_String(favorfee.PromotionFavorFeeBrands);
            Product_Str = myApp.Get_Fee_Product_String(favorfee.PromotionFavorFeeProducts);
            Delivery_Str = myApp.Get_Fee_Delivery_String(favorfee.PromotionFavorFeeDeliverys);
            Payway_Str = myApp.Get_Fee_Payway_String(favorfee.PromotionFavorFeePayways);
            Grade_Str = myApp.Get_Fee_MemberGrade_String(favorfee.PromotionFavorFeeMemberGrades);
            if (State_str == "0")
            {
                State_str = "";
            }
            if (Cate_Str == "0")
            {
                Cate_Str = "";
            }
            if (Brand_Str == "0")
            {
                Brand_Str = "";
            }
            if (Product_Str == "0")
            {
                Product_Str = "";
            }
            if (Delivery_Str == "0")
            {
                Delivery_Str = "";
            }
            if (Payway_Str == "0")
            {
                Payway_Str = "";
            }
            if (Grade_Str == "0")
            {
                Grade_Str = "";
            }
            Session["selected_cateid"] = Cate_Str;
            Session["selected_brandid"] = Brand_Str;
            Session["selected_productid"] = Product_Str;
            Session["selected_deliveryid"] = Delivery_Str;
            Session["selected_paywayid"] = Payway_Str;
        }
        else
        {
            Public.Msg("error", "错误提示", "记录不存在！", false, "{back}");
            
        }
    }
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>无标题文档</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
<script src="/Scripts/common.js" type="text/javascript"></script>
<script src="/Scripts/jquery.js" type="text/javascript"></script>
<script src="/Scripts/jquery.zxxbox.3.0.js" type="text/javascript"></script>
<link type="text/css" href="/Scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
<script src="/Scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
<script src="/Scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script>
<script type="text/javascript">
change_inputcss();
</script>
</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">添加运费优惠</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="/promotion/Promotion_Favor_Fee_do.aspx" onsubmit="javascript:return check_favor_fee();">
      <input type="hidden" name="favor_target" value="1" id="favor_target1" />
      
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
			      <td class="cell_title">
                      优惠标题</td>
			      <td class="cell_content"><input name="Promotion_Fee_Title" type="text" id="Promotion_Fee_Title" size="40" maxlength="50" value="<%=favor_title %>" onblur="check_blank('Promotion_Fee_Title')"> <span id="tip_Promotion_Fee_Title"></span></td>
			    </tr>
			    
			    <tr id="pro_tr"  <%if(Promotion_Fee_Target==0){Response.Write("style='display:none;'");} %>>
			      <td class="cell_title">
                      范围选择</td>
			      <td class="cell_content"> 
			      <input type="radio" name="favor_productall" id="favor_productall0" value="0" onclick="iniproduct(0);" <%if(Product_Str != ""){Response.Write("checked");} %>/>指定产品  <a href="" id="btn_product"><input type="button" value="选择" class="bt_orange"/></a><input type="hidden" name="favor_productid" id="favor_productid" />
                  <span id="tip_target"></span>
                  <div class="div_picker" id="product_picker">
                      <%
                          if (Product_Str == "")
                          {
                              Response.Write("<span class=\"pickertip\">全部产品</span>");
                          }
                          else
                          {
                              Response.Write(myApp.ShowProduct(Product_Str));
                          }          
                      %>
                  </div>
                  </td>
			    </tr>
			    <tr>
			      <td class="cell_title">
                      配送方式</td>
			      <td class="cell_content">
			      <input type="radio" name="favor_deliveryall" id="favor_deliveryall1" value="1" onclick="inidelivery(1);" <%if(Delivery_Str == ""){Response.Write("checked");} %>/>全部配送方式 <input type="radio" name="favor_deliveryall" id="favor_deliveryall0" value="0" onclick="inidelivery(0);" <%if(Delivery_Str != ""){Response.Write("checked");} %>/>指定配送方式  <a href="" id="btn_delivery"><input type="button" value="选择" class="bt_orange"/></a><input type="hidden" name="favor_deliveryid" id="favor_deliveryid" /> <span id="tip_favor_deliveryid"></span>
			      <div class="div_picker" id="delivery_picker">
                      <%
                          if (Delivery_Str == "")
                          {
                              Response.Write("<span class=\"pickertip\">全部配送方式</span>");
                          }
                          else
                          {
                              Response.Write(myApp.ShowDelivery(Delivery_Str));
                          }          
                      %>
			      </div>
                  </td>
			    </tr>
			    <tr>
			      <td class="cell_title">
                      支付方式</td>
			      <td class="cell_content">
			      <input type="radio" name="favor_paywayall" id="favor_paywayall1" value="1" onclick="inipayway(1);" <%if(Payway_Str == ""){Response.Write("checked");} %>/>全部支付方式 <input type="radio" name="favor_paywayall" id="favor_paywayall0" value="0" onclick="inipayway(0);" <%if(Payway_Str != ""){Response.Write("checked");} %>/>指定支付方式  <a href="" id="btn_payway"><input type="button" value="选择" class="bt_orange"/></a><input type="hidden" name="favor_paywayid" id="favor_paywayid" /> <span id="tip_favor_paywayid"></span>
			      <div class="div_picker" id="payway_picker">
                      <%
                          if (Payway_Str == "")
                          {
                              Response.Write("<span class=\"pickertip\">全部支付方式</span>");
                          }
                          else
                          {
                              Response.Write(myApp.ShowPayway(Payway_Str));
                          }          
                      %>
			      </div>
                  </td>
			    </tr>
			    <tr>
	              <td class="cell_title">
	              针对会员
                      </td>
	              <td class="cell_content"> <%myApp.Member_Grade_Check("Member_Grade", Grade_Str); %> &nbsp; <span id="tip_membergrade"></span></td>
	            </tr> 
			    <tr>
			      <td class="cell_title">
                      使用条件</td>
			      <td class="cell_content"> 当购买总金额 ≥ <input type="text" name="Promotion_Fee_Payline" id="Promotion_Fee_Payline" value="<%=Promotion_Fee_Payline %>" size="10" onblur="check_payline('Promotion_Fee_Payline')"/> 时可免去 <input id="Promotion_Fee_Manner1" name="Promotion_Fee_Manner" type="radio" value="0"  <%if(Promotion_Fee_Manner==0){Response.Write("checked");} %> /> 所有运费 <input id="Promotion_Fee_Manner2" name="Promotion_Fee_Manner" type="radio" value="1"  <%if(Promotion_Fee_Manner==1){Response.Write("checked");} %>/> 指定金额 <input type="text" size="10" name="Promotion_Fee_Price" id="Promotion_Fee_Price" value="<%=Promotion_Fee_Price %>"/> 元 <span id="tip_Promotion_Fee_Payline"> </span> <span id="tip_Promotion_Fee_Price"></span>
                  </td>
			    </tr>
			    <tr>
			      <td class="cell_title">
                      适用地区</td>
			      <td class="cell_content"> 
			      <%=myApp.State_Select("favor_province",State_str) %>
			        <input type="checkbox" name="favor_provinceall" value="1" <%if (State_str==""){Response.Write("Checked");} %> /> 全部省份 <span id="tip_state"></span>
                  </td>
			    </tr>
			    <tr>
			      <td class="cell_title">
			      有效时间
                      </td>
			      <td class="cell_content"> <input type="text" class="input_calendar" name="Promotion_Fee_Starttime" id="StartDate" maxlength="10" readonly="readonly" value="<%=Promotion_Fee_Starttime %>" /> - <input type="text" class="input_calendar" name="Promotion_Fee_Endtime" id="EndDate" maxlength="10" readonly="readonly" value="<%=Promotion_Fee_Endtime %>" /><span id="tip_valid"></span>
          	
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
          <td class="cell_content"><input name="Favor_Sort" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" type="text" id="Favor_Sort" size="10" maxlength="10" value="<%=Promotion_Fee_Sort %>" />
          <span class="tip">数字越小越靠前</span></td>
        </tr>
        <tr>
			      <td class="cell_title">
			      优惠备注
                      </td>
			      <td class="cell_content"> <input type="text" name="Promotion_Fee_Note" id="Promotion_Fee_Note" value="" size="40"/> <span class="tip">使用该优惠时记录的说明信息,如：因购买金额超过{payline}元,根据优惠政策：{favor_title},优惠运费￥{favorprice}元</span>
          	    </td>
			    </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="action" name="action" value="new" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location='promotion_favor_fee_list.aspx';"/></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
  </table>
</div>
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
$("#btn_delivery").click(function(){
    $("#btn_delivery").attr("href","delivery_check.aspx?timer=" + Math.random());						   
});
$("#btn_payway").click(function(){
    $("#btn_payway").attr("href","payway_check.aspx?timer=" + Math.random());						   
});
$("#btn_payway").zxxbox({height:450, width:600,title:'',bar:false,btnclose: false});
$("#btn_delivery").zxxbox({height:450, width:600,title:'',bar:false,btnclose: false});
$("#btn_product").zxxbox({height:450, width:600,title:'',bar:false,btnclose: false});
$("#btn_brand").zxxbox({height:450, width:600,title:'',bar:false,btnclose: false});
$("#btn_cate").zxxbox({height:300, width:600,title:'',bar:false,btnclose: false});

</script>
<script src="/Scripts/promotion.js" type="text/javascript"></script>
</body>
</html>
