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
    supplier.Supplier_Login_Check("/supplier/Delivery_Way_Add.aspx");
    Addr addr = new Addr();
    if (tools.NullInt(Session["supplier_ishaveshop"]) == 0)
    {
        pub.Msg("error", "错误信息", "您还没有开通店铺", false, "/supplier/index.aspx");
    }
    
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="配送方式添加 - 我是卖家 - " + pub.SEO_TITLE()%></title>
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
		    您现在的位置 > <a href="/">首页</a> > <a href="index.aspx">会员中心</a> > <a href="Delivery_Way_Add.aspx">配送方式添加</a>
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
                                    <form name="formadd" method="post" action="/supplier/delivery_way_do.aspx">
      <table width="100%" border="0" cellspacing="0" cellpadding="5" id="apply_1_content" class="table_padding_5">
      
            <tr>
                <td align="right" style="line-height: 24px;" class="t12_53">
                    配送方式名称
                </td>
                <td align="left">
                    <input name="Delivery_Way_Name" type="text" id="Delivery_Way_Name" class="txt_border"
                        size="40" maxlength="100" value="" />
                    <span class="t14_red">*</span>
                </td>
            </tr>
            <tr>
                <td align="right" style="line-height: 24px;" class="t12_53">
                    费用方式
                </td>
                <td align="left">
                   <input type="radio" name="delivery_way_feetype" value="0" onclick="if(this.checked){samefee.style.display='';weightfee.style.display='none';weight.style.display='none';}" checked />统一计费 &nbsp; &nbsp; <input type="radio" name="delivery_way_feetype" value="1"  onclick="if(this.checked){samefee.style.display='none';weightfee.style.display='';weight.style.display='';}"/>按重量计费
                    <span class="t14_red">*</span>
                </td>
            </tr>
            <tr id="samefee" name="samefee">
                <td align="right" style="line-height: 24px;" class="t12_53">
                    费用
                </td>
                <td align="left">
                    <input name="delivery_way_fee" type="text" size="10" id="delivery_way_fee" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" value="0" />
                    <span class="t14_red">*</span>
                </td>
            </tr>
            <tr id="weight" style="display:none;">
                <td align="right" style="line-height: 24px;" class="t12_53">
                    重量
                </td>
                <td align="left">
                     首重重量 
            <select name="Delivery_Way_InitialWeight">
              <option value="500">500克</option>
              <option value="1000">1公斤</option>
              <option value="1200">1.2公斤</option>
              <option value="2000">2公斤</option>
              <option value="5000">5公斤</option>
              <option value="10000">10公斤</option>
              <option value="20000">20公斤</option>
              <option value="50000">50公斤</option>
            </select>
            续费重量 
            <select name="Delivery_Way_UpWeight">
                <option value="500">500克</option>
                <option value="1000">1公斤</option>
                <option value="1200">1.2公斤</option>
                <option value="2000">2公斤</option>
                <option value="5000">5公斤</option>
                <option value="10000">10公斤</option>
                <option value="20000">20公斤</option>
                <option value="50000">50公斤</option>
            </select>
                    <span class="t14_red">*</span>
                </td>
            </tr>
            <tr id="weightfee" style="display:none;">
                <td align="right" style="line-height: 24px;" class="t12_53">
                    费用
                </td>
                <td align="left">
                     首重重量 
            首重费用 <input name="Delivery_Way_InitialFee" type="text" id="Delivery_Way_InitialFee" size="9" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" value="0" /> &nbsp; &nbsp; 续重费用 <input name="Delivery_Way_UpFee" type="text" id="Delivery_Way_UpFee"  size="9" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" value="0"/>
                    <span class="t14_red">*</span>
                </td>
            </tr>
            <tr>
                <td align="right" style="line-height: 24px;" class="t12_53">
                    排序
                </td>
                <td align="left">
                    <input name="Delivery_Way_Sort" type="text" id="Delivery_Way_Sort" size="10" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="10" value="1" />
                </td>
            </tr>
            <tr>
                <td align="right" style="line-height: 24px;" class="t12_53">
                    启用
                </td>
                <td align="left">
                    <input name="Delivery_Way_Status" type="radio" value="1" checked="checked"/>是 <input type="radio" name="Delivery_Way_Status" value="0"/>否
                    <span class="t14_red">*</span>
                </td>
            </tr>
            <tr>
                <td align="right" style="line-height: 24px;" class="t12_53">
                    支持货到付款
                </td>
                <td align="left">
                    <input name="Delivery_Way_Cod" type="radio" value="1"/>是 <input type="radio" name="Delivery_Way_Cod" value="0" checked="checked"/>否
                    <span class="t14_red">*</span>
                </td>
            </tr>
             <tr>
                <td align="right" style="line-height: 24px;" class="t12_53">
                    备注说明
                </td>
                <td align="left">
                   <textarea cols="50" id="Delivery_Way_Intro" name="Delivery_Way_Intro" rows="5"></textarea>
                </td>
            </tr>
        <tr>
              <td align="right" class="t12_53">
                  </td>
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
