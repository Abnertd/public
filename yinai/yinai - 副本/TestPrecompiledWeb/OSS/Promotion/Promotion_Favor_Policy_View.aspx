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
    private string cate_str,brand_str,product_str,grade_str;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("b71d572b-93b5-462f-ad32-76f97f4fb8f4");
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
            cate_str = myApp.Get_Policy_Cate_String(entity.PromotionFavorPolicyCates);

            brand_str = myApp.Get_Policy_Brand_String(entity.PromotionFavorPolicyBrands);

            product_str = myApp.Get_Policy_Product_String(entity.PromotionFavorPolicyProducts);

            grade_str = myApp.Get_Policy_MemberGrade_String(entity.PromotionFavorPolicyMemberGrades);
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
<script type="text/javascript">
<!--
function promotionexcept(promotion_type,promotion_id,product_id)
{
    $.ajaxSetup({async: false});
    $("#valid_product").load("Promotion_Except_do.aspx?action=policyexceptproduct&promotion_type=policy&promotion_id="+promotion_id+"&product_id="+product_id+"&timer="+Math.random());
    $("#except_product").load("Promotion_Except_do.aspx?action=refreshexceptpolicy&promotion_id="+promotion_id+"&timer="+Math.random());
}
function promotionvalid(promotion_type,promotion_id,product_id)
{
    $.ajaxSetup({async: false});
    $("#valid_product").load("Promotion_Except_do.aspx?action=policyvalidproduct&promotion_type=policy&promotion_id="+promotion_id+"&product_id="+product_id+"&timer="+Math.random());
    $("#except_product").load("Promotion_Except_do.aspx?action=refreshexceptpolicy&promotion_id="+promotion_id+"&timer="+Math.random());
}
btn_scroll_move();
//-->
</script>
</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">优惠政策查看</td>
    </tr>
    <tr>
      <td class="content_content">
      <table width="100%" border="0" cellpadding="0" cellspacing="0">
      <tr><td valign="top" height="31" class="opt_foot">
      <table border="0" cellpadding="0" cellspacing="0">
      <tr>
      <td class="opt_gap">&nbsp;</td>
      <td class="opt_cur" id="frm_opt_1">
      <%=Public.Page_ScriptOption("choose_opt(1,3);", "基本信息")%></td>
      <td class="opt_gap">&nbsp;</td>
      <td class="opt_uncur" id="frm_opt_2">
      <%=Public.Page_ScriptOption("choose_opt(2,3);", "适用产品")%></td>
      <td class="opt_gap">&nbsp;</td>
      <td class="opt_uncur" id="frm_opt_3">
      <%=Public.Page_ScriptOption("choose_opt(3,3);", "排除产品")%></td>
      </tr>
      </table>
      </td></tr>
    <tr>
      <td class="opt_content">
     <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table" id="frm_optitem_1">
        
        <tr>
			      <td class="cell_title">
                      优惠标题</td>
			      <td class="cell_content"><%=entity.Promotion_Policy_Title %></td>
			    </tr>
			    <%if (entity.Promotion_Policy_Target == 0)
         { %>
			    <tr>
			      <td class="cell_title">
                      产品类别</td>
			      <td class="cell_content"><%
                    if (cate_str != "0")
                    {
                        Response.Write(myApp.GetCateName(cate_str));
                    }
                    else
                    {
                        Response.Write("所有类别");
                    }                         %>
                  </td>
			    </tr>
			    <tr>
			      <td class="cell_title">
                      产品品牌</td>
			      <td class="cell_content">
			      <%
                    if (brand_str != "0")
                    {
                        Response.Write(myApp.GetBrand_Name(brand_str));
                    }
                    else
                    {
                        Response.Write("所有品牌");
                    }                         %>
			      
                  </td>
			    </tr>
			   <%}
         else
         { %>
			    <tr>
			      <td class="cell_title">
                      适用产品</td>
			      <td class="cell_content">
			      <%
             
                    if (product_str != "0")
                    {
                        Response.Write(myApp.GetProductName(product_str));
                    }
                    else
                    {
                        Response.Write("所有产品");
                    }                         %>
			      
                  </td>
			    </tr>
			    <%} %>
			    <tr>
			      <td class="cell_title">
                      针对会员</td>
			      <td class="cell_content">
			      <%
			      if (grade_str != "0")
           {
               Response.Write(myApp.GetMemberGrade_Name(grade_str));
           }
           else
           {
               Response.Write("不针对任何会员");
           } %>
                  </td>
			    </tr>
			    <tr>
			      <td class="cell_title">
                      使用条件</td>
			      <td class="cell_content"> 当购买总金额 ≥ <%=entity.Promotion_Policy_Payline %> 时
                  </td>
			    </tr>
			     <tr>
			      <td class="cell_title">
                      优惠排除</td>
			      <td class="cell_content"> 
			      <%
                                                if (entity.Promotion_Policy_Group == 1)
                                                {
                                                    Response.Write("团购产品 &nbsp;");
                                                }
                                                if(entity.Promotion_Policy_Limit ==1)
                                                {
                                                    Response.Write("限时产品");
                                                }         
			                                   %>   
                  </td>
			    </tr>
			    <tr>
			      <td class="cell_title">
                      叠加使用</td>
			      <td class="cell_content"> 
			      <%
                    if (entity.Promotion_Policy_IsRepeat == 1)
                    {
                        Response.Write("支持");
                    }
                    else
                    {
                        Response.Write("不支持");
                    }         
                   %> 
                  </td>
			    </tr>
			    <tr>
			      <td class="cell_title">
                      优惠方式</td>
			      <td class="cell_content"> 
			      
			      <%
                      if (entity.Promotion_Policy_Manner == 1)
                      {
                          Response.Write(entity.Promotion_Policy_Price + "元");
                      }
                      else if(entity.Promotion_Policy_Manner ==2)
                      {
                          Response.Write(entity.Promotion_Policy_Percent + "折");
                      }
    else
    {
        //Response.Write("派发一张满" + entity.Promotion_Policy_Couponpayline + "元，优惠" + entity.Promotion_Policy_Price + "元，有效期" + entity.Promotion_Policy_Valid + "天的优惠券/代金券");
        Response.Write("派发优惠券 <a href=\"Promotion_Coupon_Rule_view.aspx?Coupon_Rule_ID=" + entity.Promotion_Policy_CouponRuleID + "\"><span class=\"t12_blue\">查看优惠券派发规则</span></a>");
    }                           
			                    %>
                  </td>
			    </tr>
			    
			   
			    <tr>
			      <td class="cell_title">
			      有效时间
                      </td>
			      <td class="cell_content"> <%=entity.Promotion_Policy_Starttime.ToShortDateString() + " 至 " + entity.Promotion_Policy_Endtime.ToShortDateString() %> </td>
			    </tr>
			    <tr>
			      <td class="cell_title">
			      状态
                      </td>
			      <td class="cell_content"> <%

                                                if (tools.NullDate(entity.Promotion_Policy_Starttime.ToShortDateString()) > tools.NullDate(DateTime.Now.ToShortDateString()))
                                                {
                                                    Response.Write("未开始");
                                                }
                                                else if (tools.NullDate(entity.Promotion_Policy_Endtime.ToShortDateString()) < tools.NullDate(DateTime.Now.ToShortDateString()))
                                                {
                                                    Response.Write("已过期");
                                                }
                                                else
                                                {
                                                    Response.Write("正常");
                                                }
			       %> </td>
			    </tr>
			    <tr>
			      <td class="cell_title">
                      优先级</td>
			      <td class="cell_content"> <%=entity.Promotion_Policy_Sort %> 
                  </td>
                </tr>
                <tr>
			      <td class="cell_title">
                      优惠备注</td>
			      <td class="cell_content"> <%=entity.Promotion_Policy_Note %> 
                  </td>
                </tr>
                </table>
			    <table width="100%" border="0" style="display:none;" cellpadding="0" cellspacing="0" class="cell_table" id="frm_optitem_2">
			    <tr>
			      <td class="cell_title" valign="top">
			      适用产品
                      </td>
			      <td class="cell_content" id="valid_product"> <%=myApp.ShowPolicy_FavorProduct("valid", policy_id)%></td>
			    </tr>
			   </table>
			    <table width="100%" border="0" style="display:none;" cellpadding="0" cellspacing="0" class="cell_table" id="frm_optitem_3">
			    <tr>
			      <td class="cell_title" valign="top">
			      排除产品
                      </td>
			      <td class="cell_content" id="except_product"> <%=myApp.ShowPolicy_FavorProduct("except", policy_id)%></td>
			    </tr>
      </table>

        <div class="foot_gapdiv"></div>
      <div class="float_option_div" id="float_option_div">
      <input name="button" type="button" class="bt_grey" id="button1" value="返回列表" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location='promotion_favor_policy_list.aspx';"/>
    </div>
        </td>
    </tr>
  </table>
</div>
</body>
</html>
