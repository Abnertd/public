<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%  Public.CheckLogin("db71e6f9-f858-4469-b45e-b6ab55412853");%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    private Promotion myApp;
    private ITools tools;
    private PromotionFavorFeeInfo favorfee;
    private string favor_title, favor_note;
    private double Promotion_Fee_Payline, Promotion_Fee_Price;
    private int Promotion_Fee_Manner, favor_id, Promotion_Fee_Sort, Promotion_Fee_Target;
    private string Promotion_Fee_Starttime, Promotion_Fee_Endtime, State_str, Cate_Str, Delivery_Str, Payway_Str, Product_Str,Brand_Str,Grade_Str;
    protected void Page_Load(object sender, EventArgs e)
    {
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
            favor_note = favorfee.Promotion_Fee_Note;
            State_str = myApp.Get_Fee_State_String(favorfee.PromotionFavorFeeDistricts);
            Cate_Str = myApp.Get_Fee_Cate_String(favorfee.PromotionFavorFeeCates);
            Product_Str = myApp.Get_Fee_Product_String(favorfee.PromotionFavorFeeProducts);
            Delivery_Str = myApp.Get_Fee_Delivery_String(favorfee.PromotionFavorFeeDeliverys);
            Payway_Str = myApp.Get_Fee_Payway_String(favorfee.PromotionFavorFeePayways);
            Brand_Str = myApp.Get_Fee_Brand_String(favorfee.PromotionFavorFeeBrands);
            Grade_Str = myApp.Get_Fee_MemberGrade_String(favorfee.PromotionFavorFeeMemberGrades);
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
    $("#valid_product").load("Promotion_Except_do.aspx?action=feeexceptproduct&promotion_type=fee&promotion_id="+promotion_id+"&product_id="+product_id+"&timer="+Math.random());
    $("#except_product").load("Promotion_Except_do.aspx?action=refreshexceptfee&promotion_id="+promotion_id+"&timer="+Math.random());
}
function promotionvalid(promotion_type,promotion_id,product_id)
{
    $.ajaxSetup({async: false});
    $("#valid_product").load("Promotion_Except_do.aspx?action=feevalidproduct&promotion_type=fee&promotion_id="+promotion_id+"&product_id="+product_id+"&timer="+Math.random());
    $("#except_product").load("Promotion_Except_do.aspx?action=refreshexceptfee&promotion_id="+promotion_id+"&timer="+Math.random());
}
btn_scroll_move();
//-->
</script>
</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">运费优惠查看</td>
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
			      <td class="cell_content"><%=favor_title %></td>
			    </tr>
			    <%if (Promotion_Fee_Target == 0)
         { %>
			    <tr>
			      <td class="cell_title">
                      产品类别</td>
			      <td class="cell_content">
			      <%=myApp.Cate_Name_String(Cate_Str)%>
                  </td>
			    </tr>
			    <tr>
			      <td class="cell_title">
                      产品品牌</td>
			      <td class="cell_content">
			      <%if (Brand_Str != "0")
           {
               Response.Write(myApp.GetBrand_Name(Brand_Str));
           }
           else
           {
               Response.Write("所有品牌");
           }  %>
                  </td>
			    </tr>
			    <%}
         else
         { %>
			    <tr>
			      <td class="cell_title">
                      适用产品</td>
			      <td class="cell_content">
			      <%=myApp.Product_Name_String(Product_Str)%>
                  </td>
			    </tr>
			    <%} %>
			    <tr>
			      <td class="cell_title">
                      配送方式</td>
			      <td class="cell_content">
			      <%=myApp.Delivery_Name_String(Delivery_Str) %>
                  </td>
			    </tr>
			    <tr>
			      <td class="cell_title">
                      支付方式</td>
			      <td class="cell_content">
			      <%=myApp.Payway_Name_String(Payway_Str) %>
                  </td>
			    </tr>
			    <tr>
			      <td class="cell_title">
                      针对会员</td>
			      <td class="cell_content">
			      <%
			      if (Grade_Str != "0")
           {
               Response.Write(myApp.GetMemberGrade_Name(Grade_Str));
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
			      <td class="cell_content"> 当购买总金额 ≥ <%=Promotion_Fee_Payline %>  
			      <%if (Promotion_Fee_Manner == 0)
           {
               Response.Write("免全部运费");
           }
           else
           {
               Response.Write("优惠" + Promotion_Fee_Price + "元");
           }          %>
			
                  </td>
			    </tr>
			    <tr>
			      <td class="cell_title">
                      适用地区</td>
			      <td class="cell_content"> 
			      <%=myApp.State_Name_String(State_str)%>
			        
                  </td>
			    </tr>
			    <tr>
			      <td class="cell_title">
			      有效时间
                      </td>
			      <td class="cell_content"> <%=Promotion_Fee_Starttime + " 至 " + Promotion_Fee_Endtime %></td>
			    </tr>
			    <tr>
			      <td class="cell_title">
			      优先级
                      </td>
			      <td class="cell_content"> <%=Promotion_Fee_Sort%></td>
			    </tr>
			    <tr>
			      <td class="cell_title">
			      优惠备注
                      </td>
			      <td class="cell_content"> <%=favor_note%></td>
			    </tr>
			    </table>
			    <table width="100%" border="0" style="display:none;" cellpadding="0" cellspacing="0" class="cell_table" id="frm_optitem_2">
			    <tr>
			      <td class="cell_title" valign="top">
			      适用产品
                      </td>
			      <td class="cell_content" id="valid_product"> <%=myApp.ShowFee_FavorProduct("valid",favor_id) %></td>
			    </tr>
			    </table>
			    <table width="100%" border="0" style="display:none;" cellpadding="0" cellspacing="0" class="cell_table" id="frm_optitem_3">
			    <tr>
			      <td class="cell_title" valign="top">
			      排除产品
                      </td>
			      <td class="cell_content" id="except_product"> <%=myApp.ShowFee_FavorProduct("except",favor_id) %></td>
			    </tr>
      </table>

        <div class="foot_gapdiv"></div>
      <div class="float_option_div" id="float_option_div">
      <input name="button" type="button" class="bt_grey" id="button1" value="返回列表" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location='promotion_favor_fee_list.aspx';"/>
    </div>
        </td>
    </tr>
  </table>
  </td>
    </tr>
  </table>
</div>
</body>
</html>
