<%@ Page Language="C#" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<%@ Register Src="~/Public/Top.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>

<% 
    
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    Supplier supplier = new Supplier();
    supplier.Supplier_Login_Check("/supplier/Delivery_Way_List.aspx");
    Addr addr = new Addr();
    int Delivery_Way_ID;

    if (tools.NullInt(Session["supplier_ishaveshop"]) == 0)
    {
        pub.Msg("error", "错误信息", "您还没有开通店铺", false, "/supplier/index.aspx");
    }
    Delivery_Way_ID = tools.CheckInt(Request.QueryString["Delivery_Way_ID"]);
    DeliveryWayInfo entity = supplier.GetDeliveryWayByID(Delivery_Way_ID);
    if (entity == null)
    {
        pub.Msg("error", "错误信息", "记录不存在", false, "{back}");
        Response.End();
    }
    else
    {
        if (entity.Delivery_Way_SupplierID != tools.NullInt(Session["supplier_id"]))
        {
            pub.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
    }
    
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="配送方式管理 - 我是卖家 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script src="/scripts/jquery-extend-AdAdvance3.js" type="text/javascript"></script>
    <script type="text/javascript" src="/scripts/common.js"></script>
   
</head>
<body>

    <uctop:top ID="top1" runat="server" />
    

    
    <!--主体 开始-->
<div class="content">
      <!--位置说明 开始-->
      <div class="position">
		     您现在的位置 > <a href="/">首页</a> > <a href="index.aspx">会员中心</a> > <a href="Delivery_Way_List.aspx">配送方式管理</a>
	</div>
      <div class="clear"></div>
      <!--位置说明 结束-->
      <div class="parth">
            <div class="ph_left">
                       <% supplier.Get_Supplier_Left_HTML(2, 9); %>
            </div>
            <div class="ph_right">
                  <div class="blk13">
                        <h2>配送方式管理</h2>
                        <div class="main">
                                            
<form id="formadd" name="formadd" method="post" action="delivery_way_do.aspx">
             <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
          <td align="left">指定配送地区：<span id="textDiv"><%=addr.SelectAddressDelivery("textDiv", "District_State", "District_City", "District_County", "", "", "")%></span>
          <input name="save" type="submit" class="buttonupload" id="save" value="保存" align="absmiddle" />

          </td>
        </tr>
        <tr><td height="10"></td></tr>
      </table>
             <input type="hidden" id="District_Country" name="District_Country" value="1" />
        <input type="hidden" id="District_State" name="District_State" value="" />
        <input type="hidden" id="District_City" name="District_City" value=""/>
        <input type="hidden" id="District_County" name="District_County" value=""/>
        
        <input type="hidden" id="action" name="action" value="add" />
        <input type="hidden" id="District_DeliveryWayID" name="District_DeliveryWayID" value="<% =Delivery_Way_ID%>" />
         </form>
           <%supplier.GetDeliveryWayDistricts();%>
                    
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
