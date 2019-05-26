<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="System.Collections.Generic" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    private Promotion myApp;
    PromotionFavorGiftInfo entity = null;
    private ITools tools;
    private string cate_str, brand_str, product_str,grade_str;
    int gift_id = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("15bb07a5-83ee-4157-94c2-693bc4312d74");
        myApp = new Promotion();
        tools = ToolsFactory.CreateTools();

        gift_id = tools.CheckInt(Request["Promotion_gift_ID"]);

        entity = myApp.GetPromotionFavorGiftByID(gift_id);
        if (entity == null)
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        cate_str = myApp.Get_Gift_Cate_String(entity.Promotion_Gift_Cates);
        brand_str = myApp.Get_Gift_Brand_String(entity.Promotion_Gift_Brands);
        product_str = myApp.Get_Gift_Product_String(entity.Promotion_Gift_Products);
        grade_str = myApp.Get_Gift_MemberGrade_String(entity.PromotionFavorGiftMemberGrades);
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
    $("#valid_product").load("Promotion_Except_do.aspx?action=giftexceptproduct&promotion_type=gift&promotion_id="+promotion_id+"&product_id="+product_id+"&timer="+Math.random());
    $("#except_product").load("Promotion_Except_do.aspx?action=refreshexceptgift&promotion_id="+promotion_id+"&timer="+Math.random());
}
function promotionvalid(promotion_type,promotion_id,product_id)
{
    $.ajaxSetup({async: false});
    $("#valid_product").load("Promotion_Except_do.aspx?action=giftvalidproduct&promotion_type=gift&promotion_id="+promotion_id+"&product_id="+product_id+"&timer="+Math.random());
    $("#except_product").load("Promotion_Except_do.aspx?action=refreshexceptgift&promotion_id="+promotion_id+"&timer="+Math.random());
}
btn_scroll_move();
//-->
</script>
</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">赠品优惠</td>
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
			      <td class="cell_content"><%=entity.Promotion_Gift_Title %></td>
			    </tr>
			    <%if (entity.Promotion_Gift_Target == 0)
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
                      产品信息</td>
			      <td class="cell_content">
			      <%
                    if (product_str != "0")
                    {
                        Response.Write(myApp.GetProductName(product_str));
                    }
                    else
                    {
                        Response.Write("所有商品");
                    }         
                        %>
			      
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
                      优惠排除</td>
			      <td class="cell_content"> 
			      <%
                    if (entity.Promotion_Gift_Group == 1)
                    {
                        Response.Write("团购产品 &nbsp;");
                    }
                    if(entity.Promotion_Gift_Limit ==1)
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
                    if (entity.Promotion_Gift_IsRepeat == 1)
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
                      设置赠品</td>
			      <td class="cell_content t12_red" style="line-height:25px;">
			     <%
			
                     int iniamount=0;
                     double inibuyamount=0;                  
                     if (entity.Promotion_Gift_Amounts != null)
                     {
                         foreach (PromotionFavorGiftAmountInfo subamount in entity.Promotion_Gift_Amounts)
                         {
                             if (subamount.Gift_Amount_Amount > 0)
                             {


                                 if (subamount.Gift_Amount_Amount == iniamount && iniamount > 0)
                                 {
                                     Response.Write("或 ");
                                 }
                                 else
                                 {
                                     Response.Write("购买数量>=" + subamount.Gift_Amount_Amount + " ");
                                 }
                                 Response.Write("赠送：");
                                 if (subamount.Promotion_Gift_Gifts != null)
                                 {
                                     foreach (PromotionFavorGiftGiftInfo subgift in subamount.Promotion_Gift_Gifts)
                                     {
                                         Response.Write(myApp.GetProductName(subgift.Gift_ProductID.ToString()));
                                         Response.Write(" * " + subgift.Gift_Amount + "; ");
                                     }
                                 }
                                 Response.Write("<br>");
                             }
                             else
                             {
                                 
                                 if (subamount.Gift_Amount_BuyAmount == inibuyamount && inibuyamount > 0)
                                 {
                                     Response.Write("或 ");
                                 }
                                 else
                                 {
                                     Response.Write("购买金额>=" + Public.DisplayCurrency(subamount.Gift_Amount_BuyAmount) + " ");
                                 }
                                 Response.Write("赠送：");
                                 if (subamount.Promotion_Gift_Gifts != null)
                                 {
                                     foreach (PromotionFavorGiftGiftInfo subgift in subamount.Promotion_Gift_Gifts)
                                     {
                                         Response.Write(myApp.GetProductName(subgift.Gift_ProductID.ToString()));
                                         Response.Write(" * " + subgift.Gift_Amount + "; ");
                                     }
                                 }
                                 Response.Write("<br>");
                             }
                             iniamount = subamount.Gift_Amount_Amount;
                             inibuyamount = subamount.Gift_Amount_BuyAmount;
                         }
                     }                   %>
                  </td>
			    </tr>

			    
			    
			    <tr>
			      <td class="cell_title">
			      有效时间
                      </td>
			      <td class="cell_content"> <%=entity.Promotion_Gift_Starttime.ToShortDateString() + " 至 " + entity.Promotion_Gift_Endtime.ToShortDateString() %></td>
			    </tr>
			    <tr>
			      <td class="cell_title">
			      状态
                      </td>
			      <td class="cell_content"> <%

                                                if (tools.NullDate(entity.Promotion_Gift_Starttime.ToShortDateString()) > tools.NullDate(DateTime.Now.ToShortDateString()))
                                                {
                                                    Response.Write("未开始");
                                                }
                                                else if (tools.NullDate(entity.Promotion_Gift_Endtime.ToShortDateString()) < tools.NullDate(DateTime.Now.ToShortDateString()))
                                                {
                                                    Response.Write("已过期");
                                                }
                                                else
                                                {
                                                    Response.Write("正常");
                                                }
			       %> </td>
			    </tr>
			    </table>
			    <table width="100%" border="0" style="display:none;" cellpadding="0" cellspacing="0" class="cell_table" id="frm_optitem_2">
			    <tr>
			      <td class="cell_title" valign="top">
			      适用产品
                      </td>
			      <td class="cell_content" id="valid_product"> <%=myApp.ShowGift_FavorProduct("valid", gift_id)%></td>
			    </tr>
			   </table>
			    <table width="100%" border="0" style="display:none;" cellpadding="0" cellspacing="0" class="cell_table" id="frm_optitem_3">
			    <tr>
			      <td class="cell_title" valign="top">
			      排除产品
                      </td>
			      <td class="cell_content" id="except_product"> <%=myApp.ShowGift_FavorProduct("except", gift_id)%></td>
			    </tr>
      </table>

        <div class="foot_gapdiv"></div>
      <div class="float_option_div" id="float_option_div">
      <input name="button" type="button" class="bt_grey" id="button1" value="返回列表" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location='promotion_favor_gift_list.aspx';"/>
    </div>
        </td>
    </tr>
  </table>
  </td>
    </tr>
  </table>
</div>
<div id="gift_item" style="display:none"></div>
</body>
</html>
