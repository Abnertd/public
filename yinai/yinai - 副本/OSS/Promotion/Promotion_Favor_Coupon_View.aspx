<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="System.Collections.Generic" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    private Promotion myApp;
    int coupon_id;
    PromotionFavorCouponInfo entity=null;
    private ITools tools;
    private Member member;
    private string cate_str,brand_str,product_str;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("18cde8c2-8be5-4b15-b057-795726189795");
        myApp = new Promotion();
        member = new Member();
        tools=ToolsFactory.CreateTools();
        coupon_id = tools.CheckInt(Request["promotion_coupon_id"]);
            if(coupon_id>0)
            {
                entity = myApp.GetPromotionFavorCouponByID(coupon_id);
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
            cate_str = myApp.Get_Coupon_Cate_String(entity.PromotionFavorCouponCates);
            brand_str = myApp.Get_Coupon_Brand_String(entity.PromotionFavorCouponBrands);
            product_str = myApp.Get_Coupon_Product_String(entity.PromotionFavorCouponProducts);
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
      <td class="content_title">优惠券/代金券查看</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="/promotion/Promotion_Favor_Coupon_do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        
        <tr>
			      <td class="cell_title">
                      优惠标题</td>
			      <td class="cell_content"><%=entity.Promotion_Coupon_Title %></td>
			    </tr>
			    <tr>
			      <td class="cell_title">
                      卡号</td>
			      <td class="cell_content"> <%=entity.Promotion_Coupon_Code %> 
                  </td>
			    </tr>
			    <tr>
			      <td class="cell_title">
                      验证码</td>
			      <td class="cell_content"> <%=entity.Promotion_Coupon_Verifycode %>
                  </td>
			    </tr>
			    <%if (entity.Promotion_Coupon_Target == 0)
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
          <td class="cell_title" valign="top">适用会员</td>
          <td class="cell_content">
          <%if (entity.Promotion_Coupon_Member_ID == 0)
            {
                Response.Write("所有会员");
            }
            else
            {
                MemberInfo memberinfo=member.GetMemberByID(entity.Promotion_Coupon_Member_ID);
                if (memberinfo != null)
                {
                    Response.Write(memberinfo.Member_NickName);
                }
            }%>
          </td>
        </tr>
			    <tr>
			      <td class="cell_title">
                      使用条件</td>
			      <td class="cell_content"> 当购买总金额 ≥ <%=entity.Promotion_Coupon_Payline %> 时
                  </td>
			    </tr>
			    <tr>
			      <td class="cell_title">
                      优惠金额</td>
			      <td class="cell_content"> 
			      
			      <%
                      if (entity.Promotion_Coupon_Manner == 1)
                      {
                          Response.Write(entity.Promotion_Coupon_Price + "元");
                      }
                      else
                      {
                          Response.Write(entity.Promotion_Coupon_Percent + "%");
                      }         
			                    %>
                  </td>
			    </tr>
			    
			    <tr>
			      <td class="cell_title">
                      使用次数</td>
			      <td class="cell_content"> 
			      <%
                                                if (entity.Promotion_Coupon_Amount == 1)
                                                {
                                                    Response.Write("仅可使用一次");
                                                }
                                                else
                                                {
                                                    Response.Write("不限次数");
                                                }         
			                                   %>   
                  </td>
			    </tr>
			    <tr>
			      <td class="cell_title">
			      有效时间
                      </td>
			      <td class="cell_content"> <%=entity.Promotion_Coupon_Starttime.ToShortDateString() + " - " + entity.Promotion_Coupon_Endtime.ToShortDateString() %> </td>
			    </tr>
			    <tr>
			      <td class="cell_title">
			      状态
                      </td>
			      <td class="cell_content"> <%
                                                if (entity.Promotion_Coupon_Isused == 1)
                                                {
                                                    Response.Write("已使用");
                                                }
                                                else if (tools.NullDate(entity.Promotion_Coupon_Starttime.ToShortDateString()) > tools.NullDate(DateTime.Now.ToShortDateString()))
                                                {
                                                    Response.Write("未开始");
                                                }
                                                else if (tools.NullDate(entity.Promotion_Coupon_Endtime.ToShortDateString()) < tools.NullDate(DateTime.Now.ToShortDateString()))
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
			      使用次数
                      </td>
			      <td class="cell_content"> <%=entity.Promotion_Coupon_UseAmount %> </td>
			    </tr>
			    <tr>
			      <td class="cell_title">
			      添加时间
                      </td>
			      <td class="cell_content"> <%=entity.Promotion_Coupon_Addtime %> </td>
			    </tr>
			    <tr>
			      <td class="cell_title">
			      优惠备注
                      </td>
			      <td class="cell_content"> <%=entity.Promotion_Coupon_Note %> </td>
			    </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">

             <input name="button" type="button" class="bt_grey" id="button" value="返回" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location='promotion_favor_coupon_list.aspx';"/></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
  </table>
</div>
</body>
</html>
