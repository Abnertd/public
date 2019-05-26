<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="System.Collections.Generic" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    private Promotion myApp;
    int rule_id;
    PromotionCouponRuleInfo entity=null;
    private ITools tools;
    private Member member;
    private string cate_str,brand_str,product_str;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("e5a32e42-426a-4202-818a-ad20a980b4cb");
        myApp = new Promotion();
        member = new Member();
        tools=ToolsFactory.CreateTools();
        rule_id = tools.CheckInt(Request["Coupon_Rule_ID"]);
        if (rule_id > 0)
            {
                entity = myApp.GetPromotionCouponRuleByID(rule_id);
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
            cate_str = myApp.Get_CouponRule_Cate_String(entity.PromotionCouponRuleCates);
            brand_str = myApp.Get_CouponRule_Brand_String(entity.PromotionCouponRuleBrands);
            product_str = myApp.Get_CouponRule_Product_String(entity.PromotionCouponRuleProducts);
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

</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">优惠券/代金券派发规则查看</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="/promotion/Promotion_Favor_Coupon_do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        
        <tr>
			      <td class="cell_title">
                      优惠标题</td>
			      <td class="cell_content"><%=entity.Coupon_Rule_Title %></td>
			    </tr>
			    <%if (entity.Coupon_Rule_Target == 0)
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
         {         %>
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
                      使用条件</td>
			      <td class="cell_content"> 当购买总金额 ≥ <%=entity.Coupon_Rule_Payline %> 时
                  </td>
			    </tr>
			    <tr>
			      <td class="cell_title">
                      优惠金额</td>
			      <td class="cell_content"> 
			      
			      <%
                      if (entity.Coupon_Rule_Manner == 1)
                      {
                          Response.Write(entity.Coupon_Rule_Price + "元");
                      }
                      else
                      {
                          Response.Write(entity.Coupon_Rule_Percent + "%");
                      }         
			                    %>
                  </td>
			    </tr>
			    
			    
			    <tr>
			      <td class="cell_title">
			      有效时间
                      </td>
			      <td class="cell_content"> <%=entity.Coupon_Rule_Valid %> 天 </td>
			    </tr>
			    <tr>
			      <td class="cell_title">
			      优惠备注
                      </td>
			      <td class="cell_content"> <%=entity.Coupon_Rule_Note %> </td>
			    </tr>
			    
			    
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">

             <input name="button" type="button" class="bt_grey" id="button" value="返回" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="history.go(-1);"/></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
  </table>
</div>
</body>
</html>
