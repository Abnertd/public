<%@ Page Language="C#" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<%@ Register Src="~/Public/Top.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>

<% 
    
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    Session["Cur_Position"] = Session["Home_Position"] = "";
    Supplier supplier = new Supplier();
    int Supplier_Margin_ID = 0;
    double Supplier_Margin_Money = 0.00;
    double Supplier_Margin_MemberMoney = 0.00;
    double Supplier_Margin_PromotionMoney = 0.00;
    SupplierInfo entity = supplier.GetSupplierByID();
    if (entity != null)
    {
        if (entity.Supplier_Cert_Status != 2)
        {
            pub.Msg("error", "错误信息", "您尚未通过商家资质审核", false, "/supplier/index.aspx");
        }
    }
    
    supplier.Supplier_Login_Check("/supplier/Supplier_Account_Add.aspx");
    
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="账户充值 - 会员中心 - " + pub.SEO_TITLE()%></title>
      <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
 
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script src="/scripts/common.js" type="text/javascript"></script>
    <script src="/scripts/jquery-extend-AdAdvance3.js" type="text/javascript"></script>
    
    <style type="text/css">
        .yz_blk19_main img { display:inline; }
    </style>
</head>
<body>

    <uctop:top ID="top1" runat="server" />
    
    <!--主体 开始-->
<div class="content">
      <!--位置说明 开始-->
      <div class="position">
		  您现在的位置 > <a href="/">首页</a> > <a href="index.aspx">会员中心</a> > <a href="Supplier_Account_Add.aspx">账户充值</a>
	</div>
      <div class="clear"></div>
      <!--位置说明 结束-->
      <div class="parth">
            <div class="ph_left">
                     <% supplier.Get_Supplier_Left_HTML(1, 6); %>
            </div>
            <div class="ph_right">
                  <div class="blk13">
                        <h2>账户充值</h2>
                        <div class="main">
                                   
        <form name="frm_account_profile" method="post" action="/supplier/account_do.aspx">
        <table width="100%" border="0" cellspacing="0" cellpadding="5" class="table_padding_5">
        <tr>
          <td width="100" align="right" class="t12_53">账户类型</td>
          <td align="left">
          <input type="radio" value="0" name="Account_Type" checked onclick="getmoney('0');"/> 会员费 <input type="radio" onclick="getmoney('1');" value="1" name="Account_Type" /> 保证金 <input type="radio" onclick="getmoney('2');" value="2" name="Account_Type" /> 推广费
              <span class="t12_red">*</span></td>
              <input type="hidden" id="Supplier_Margin_Money" value="<%=Supplier_Margin_Money %>" />
              <input type="hidden" id="Supplier_Margin_MemberMoney" value="<%=Supplier_Margin_MemberMoney %>" />
              <input type="hidden" id="Supplier_Margin_PromotionMoney" value="<%=Supplier_Margin_PromotionMoney %>" />
        </tr>
        <tr>
          <td align="right" class="t12_53">充值金额</td>
          <td>
          <span id="span_Account_Amount"></span>
          <input name="Account_Amount" type="text" class="txt_border" id="Account_Amount" size="10" maxlength="20" value=""/>
              <span class="t12_red">*</span></td>
        </tr>
        <tr>
          <td align="right" class="t12_53">支付方式</td>
          <td><input type="radio" value="0" name="pay_Type" checked /> 支付宝
              <span class="t12_red">*</span></td>
        </tr>
        <tr>
          <td align="right" class="t12_53">验证码</td>
          <td><input name="verifycode" type="text" onfocus="$('#var_img').attr('src','/Public/verifycode.aspx?timer='+Math.random());" class="txt_border" id="verifycode" size="10" maxlength="10" /> <span class="t12_red">*</span> <img id="var_img" alt="看不清？换一张" title="看不清？换一张" src="/public/verifycode.aspx" onclick="this.src='../Public/verifycode.aspx?timer='+Math.random();" style="cursor:pointer" align="absmiddle" /></td>
        </tr>
        <tr>
          <td align="right" class="t12_53">&nbsp;</td>
          <td><span class="tip">
            <div id="tip_verifycode"></div>
          </span></td>
        </tr>
        <tr>
          <td align="right" class="t12_53">&nbsp;</td>
          <td><span class="table_v_title">
            <input name="action" type="hidden" id="action" value="account_add" />
            <input name="btn_submit" type="image" src="/images/submit.gif" />
          </span></td>
        </tr>
        
      </table>
        </form>  
                        </div>
                  </div>
            </div>
            <div class="clear"></div>
      </div> 
</div>
<!--主体 结束-->
 
  <ucbottom:bottom runat="server" ID="Bottom" />
    
</body>
</html>
