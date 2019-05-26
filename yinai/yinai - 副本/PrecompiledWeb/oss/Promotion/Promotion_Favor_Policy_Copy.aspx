<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="System.Collections.Generic" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    private Promotion myApp;
    int policy_id,i;
    PromotionFavorPolicyInfo entity=null;
    private ITools tools;
    private Member member;
    private string Cate_Str, Brand_Str, Product_Str,Grade_Str;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("6ee2adbb-590f-4c59-b7b3-da7266977106");
        myApp = new Promotion();
        member = new Member();
        tools=ToolsFactory.CreateTools();
        policy_id = tools.CheckInt(Request["promotion_policy_id"]);
        if (policy_id > 0)
            {
                entity = myApp.GetPromotionFavorPolicyByID(policy_id);
                if (entity == null)
                {
                    Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
                    Response.End();
                }   
            }
            if (entity == null)
            {
                Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
                Response.End();
            }
            Cate_Str = myApp.Get_Policy_Cate_String(entity.PromotionFavorPolicyCates);

            if (Cate_Str == "0")
            {
                Cate_Str = "";
            }

            Brand_Str = myApp.Get_Policy_Brand_String(entity.PromotionFavorPolicyBrands);

            if (Brand_Str == "0")
            {
                Brand_Str = "";
            }

            Product_Str = myApp.Get_Policy_Product_String(entity.PromotionFavorPolicyProducts);

            if (Product_Str == "0")
            {
                Product_Str = "";
            }

            Grade_Str = myApp.Get_Policy_MemberGrade_String(entity.PromotionFavorPolicyMemberGrades);

            if (Grade_Str == "0")
            {
                Grade_Str = "";
            }
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
<script type="text/javascript">
change_inputcss();
</script>
</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">添加优惠政策</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="/promotion/Promotion_Favor_Policy_do.aspx" onsubmit="javascript:return check_favor_policy();">
      <input type="hidden" name="favor_target" value="1" id="favor_target1" />
      
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
  
        <tr>
			      <td class="cell_title">
                      优惠标题</td>
			      <td class="cell_content"><input name="Promotion_Policy_Title" type="text" id="Promotion_Policy_Title" size="40" maxlength="50" value="<%=entity.Promotion_Policy_Title %>" onblur="check_blank('Promotion_Policy_Title')"> <span id="tip_Promotion_Policy_Title"></span></td>
			    </tr>
			    <tr id="pro_tr">
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
	              针对会员
                      </td>
	              <td class="cell_content"> <%myApp.Member_Grade_Check("Member_Grade", Grade_Str); %> &nbsp; <span id="tip_membergrade"></span></td>
	            </tr> 
			    <tr>
			      <td class="cell_title">
                      使用条件</td>
			      <td class="cell_content"> 当购买总金额 ≥ <input type="text" name="Promotion_Policy_Payline" id="Promotion_Policy_Payline" value="<%=entity.Promotion_Policy_Payline %>" size="10" onblur="check_payline('Promotion_Policy_Payline')"/> 时 <span id="tip_Promotion_Policy_Payline"></span>
                  </td>
			    </tr>
			    <tr>
			      <td class="cell_title">
                      优惠排除</td>
			      <td class="cell_content"> <input type="checkbox" name="Promotion_Policy_Group" value="1" <%if (entity.Promotion_Policy_Group==1){Response.Write("Checked");} %>/> 团购产品  <input type="checkbox" name="Promotion_Policy_Limit" value="1" <%if (entity.Promotion_Policy_Limit==1){Response.Write("Checked");} %>/> 限时产品
                  </td>
			    </tr>
			    <tr>
			      <td class="cell_title">
                      叠加使用</td>
			      <td class="cell_content"> <input type="radio" name="Promotion_Policy_IsRepeat" value="1" <%if (entity.Promotion_Policy_IsRepeat==1){Response.Write("Checked");} %>/> 支持  <input type="radio" name="Promotion_Policy_IsRepeat" value="0" <%if (entity.Promotion_Policy_IsRepeat==0){Response.Write("Checked");} %> /> 不支持
                  </td>
			    </tr>
			    <tr>
			      <td class="cell_title">
                      优惠金额</td>
			      <td class="cell_content"> <input id="Promotion_Policy_Manner1" name="Promotion_Policy_Manner" type="radio" value="1" <%if (entity.Promotion_Policy_Manner==1){Response.Write("Checked");} %> /> 指定金额 <input type="text" size="10" name="Promotion_Policy_Price" id="Promotion_Policy_Price" value="<%if (entity.Promotion_Policy_Manner==1){Response.Write(entity.Promotion_Policy_Price);}else{Response.Write("0.00");} %>"/> 元 <input id="Promotion_Policy_Manner2" name="Promotion_Policy_Manner" type="radio" value="2" <%if (entity.Promotion_Policy_Manner==2){Response.Write("Checked");} %>/> 优惠百分比 <input type="text" size="10" id="Promotion_Policy_Percent" name="Promotion_Policy_Percent" value="<%=entity.Promotion_Policy_Percent %>"/> %   <span id="tip_Promotion_Policy_Price"></span> <span id="tip_Promotion_Policy_Percent"></span>
                  </td>
			    </tr>
			    <tr>
			      <td class="cell_title">
                      </td>
			      <td class="cell_content"> <input id="Promotion_Policy_Manner3" name="Promotion_Policy_Manner" type="radio" value="3" <%if (entity.Promotion_Policy_Manner==3){Response.Write("Checked");} %>/> 应用派发优惠券规则 <%myApp.PromotionCouponRule_Select(entity.Promotion_Policy_CouponRuleID); %> 
                  </td>
			    </tr>
			    <tr>
			      <td class="cell_title">
			      有效时间
                      </td>
			      <td class="cell_content"> <input type="text" class="input_calendar" name="Promotion_Policy_Starttime" id="StartDate" maxlength="10" readonly="readonly" value="<%=entity.Promotion_Policy_Starttime.ToShortDateString() %>" /> - <input type="text" class="input_calendar" name="Promotion_Policy_Endtime" id="EndDate" maxlength="10" readonly="readonly" value="<%=entity.Promotion_Policy_Endtime.ToShortDateString() %>" /><span id="tip_valid"></span>
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
          <td class="cell_content"><input name="Favor_Sort" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" type="text" id="Favor_Sort" size="10" maxlength="10" value="<%=entity.Promotion_Policy_Sort %>" />
          <span class="tip">数字越小越靠前</span></td>
        </tr>
        <tr>
			      <td class="cell_title">
			      优惠备注
                      </td>
			      <td class="cell_content"> <input type="text" name="Promotion_Fee_Note" id="Promotion_Fee_Note" value="" size="40"/> <span class="tip">使用该优惠时记录的说明信息,如：因购买金额超过{payline}元,根据优惠政策：{favor_title},价格优惠￥{favorprice}元</span>
          	    </td>
			    </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="action" name="action" value="new" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location='promotion_favor_policy_list.aspx';"/></td>
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
$("#btn_product").zxxbox({height:450, width:600,title:'',bar:false,btnclose: false});
$("#btn_brand").zxxbox({height:450, width:600,title:'',bar:false,btnclose: false});
$("#btn_cate").zxxbox({height:300, width:600,title:'',bar:false,btnclose: false});

</script>
<script src="/Scripts/promotion.js" type="text/javascript"></script>
</body>
</html>
