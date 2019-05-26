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
    supplier.Supplier_Login_Check("/supplier/Supplier_PayBack_Apply_Add.aspx");
%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="申请账户退款 - 会员中心 - " + pub.SEO_TITLE()%></title>
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
		    您现在的位置 > <a href="/">首页</a> > <a href="index.aspx">会员中心</a> > <a href="Supplier_PayBack_Apply_Add.aspx">申请账户退款</a>
	</div>
      <div class="clear"></div>
      <!--位置说明 结束-->
      <div class="parth">
            <div class="ph_left">
                     <% supplier.Get_Supplier_Left_HTML(1, 11); %>
            </div>
            <div class="ph_right">
                  <div class="blk13">
                        <h2>申请账户退款</h2>
                        <div class="main">
                               
                     <form name="formadd" method="post" action="/supplier/Supplier_PayBack_Apply_do.aspx">
                     <table width="100%" border="0" cellspacing="0" cellpadding="5" id="apply_1_content" class="table_padding_5">
                        <tr>
                            <td align="right" style="line-height: 24px;" class="t12_53">
                                申请退款类型
                            </td>
                            <td align="left">
                                <input type="radio" name="Supplier_PayBack_Apply_Type" value="1" checked/> 会员费
                                <input type="radio" name="Supplier_PayBack_Apply_Type" value="2" /> 保证金
                                <input type="radio" name="Supplier_PayBack_Apply_Type" value="3" /> 推广费 
                                <span class="t14_red">*</span>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="line-height: 24px;" class="t12_53">
                                申请退款金额
                            </td>
                            <td align="left">
                                <input name="Supplier_PayBack_Apply_Amount" type="text" id="Supplier_PayBack_Apply_Amount" class="txt_border"
                                    size="20" maxlength="100" value=""  onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')"/>
                                <span class="t14_red">*</span>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="line-height: 24px;" class="t12_53">
                                申请退款备注
                            </td>
                            <td align="left">
                                <textarea name="Supplier_PayBack_Apply_Note" cols="50" rows="5"></textarea> 最多不超过200个字符
                                <span class="t14_red">*</span>
                            </td>
                        </tr>
                        <tr>
                          <td></td>
                          <td><input name="action" type="hidden" id="action" value="new"><input name="btn_submit" type="image" src="/images/save.jpg" /></td>
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
