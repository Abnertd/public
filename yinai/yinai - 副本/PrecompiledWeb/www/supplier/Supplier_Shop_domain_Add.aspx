<%@ Page Language="C#" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<%@ Register Src="~/Public/Top.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>

<% 
    Public_Class pub = new Public_Class();
    string Supplier_Bank_Name = "";
    string Supplier_Bank_NetWork = "";
    string Supplier_Bank_SName = "";

    ITools tools = ToolsFactory.CreateTools();
    Supplier supplier = new Supplier();

    supplier.Supplier_Login_Check("/supplier/Supplier_Shop_Domain_add.aspx");
    if (tools.NullInt(Session["supplier_ishaveshop"]) == 0)
    {
        pub.Msg("error", "错误信息", "您还没有开通店铺", false, "/supplier/index.aspx");
    }
    SupplierShopInfo entity = supplier.GetSupplierShopBySupplierID(tools.NullInt(Session["supplier_id"]));
    if (entity == null)
    {
        pub.Msg("error", "错误信息", "记录不存在", false, "{back}");
        Response.End();
    }
%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="店铺域名申请 - 会员中心 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    
   <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script src="/scripts/jquery-extend-AdAdvance3.js" type="text/javascript"></script>
   
</head>
<body>

    <uctop:top ID="top1" runat="server" />
    
    
    <!--主体 开始-->
<div class="content">
      <!--位置说明 开始-->
      <div class="position">
		   您现在的位置 > <a href="/">首页</a> > <a href="index.aspx">会员中心</a> > <a href="Supplier_Shop_Domain_add.aspx">店铺域名申请</a>
	</div>
      <div class="clear"></div>
      <!--位置说明 结束-->
      <div class="parth">
            <div class="ph_left">
                     <% supplier.Get_Supplier_Left_HTML(2, 13); %>
            </div>
            <div class="ph_right">
                  <div class="blk13">
                        <h2>店铺域名申请</h2>
                        <div class="main">
                             
               <form name="frm_account_profile" method="post" action="/supplier/Supplier_Shop_Domain_do.aspx">
            <table width="100%" border="0" cellspacing="0" cellpadding="5" class="table_padding_5">
       <%--if (entity.Shop_Type == 1)
         { %>
       <tr>
          <td width="100" align="right" class="t12_53">域名类型：</td>
          <td>
          <input type="radio" name="Shop_Domain_Type" value="0" checked onclick="$('#domainspan').html('<%=Supplier.GetShopDomain()%>');"/> 默认域名
          <input type="radio" name="Shop_Domain_Type" value="1" onclick="$('#domainspan').html('');"/> 顶级域名
              <span class="t12_red">*</span></td>
        </tr>
        <%} --%>
        <tr>
          <td width="100" align="right" class="t12_53">店铺域名：</td>
          <td>http://<input name="Shop_Domain_Name" type="text" class="txt_border" id="Shop_Domain_Name" size="20" value="" maxlength="100" /><span id="domainspan"><%=supplier.GetShopDomain()%></span>
              <span class="t12_red">*</span></td>
        </tr>
        
        
        <tr>
          <td align="right" class="t12_53">&nbsp;</td>
          <td><span class="table_v_title">
            <input name="action" type="hidden" id="action" value="domain_add" />
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
