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
    
    if (tools.NullInt(Session["supplier_ishaveshop"]) == 0)
    {
        pub.Msg("error", "错误信息", "您还没有开通店铺", false, "/supplier/index.aspx");
    }
    string Delivery_Way_Name, Delivery_Way_Url, Delivery_Way_Intro, Delivery_Way_Site, Delivery_Way_Img;
    int Delivery_Way_ID, Delivery_Way_Sort, Delivery_Way_InitialWeight, Delivery_Way_UpWeight, Delivery_Way_FeeType, Delivery_Way_Status, Delivery_Way_Cod;
    double Delivery_Way_Fee, Delivery_Way_InitialFee, Delivery_Way_UpFee;
    Delivery_Way_Name = "";
    Delivery_Way_FeeType = 0;
    Delivery_Way_Fee = 0;
    Delivery_Way_InitialFee = 0;
    Delivery_Way_UpFee = 0;
    Delivery_Way_Sort = 0;
    Delivery_Way_InitialWeight = 0;
    Delivery_Way_UpWeight = 0;
    Delivery_Way_Status = 0;
    Delivery_Way_Intro = "";
    Delivery_Way_Cod = 0;

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
        Delivery_Way_ID = entity.Delivery_Way_ID;
        Delivery_Way_Name = entity.Delivery_Way_Name;
        Delivery_Way_Sort = entity.Delivery_Way_Sort;
        Delivery_Way_InitialWeight = entity.Delivery_Way_InitialWeight;
        Delivery_Way_UpWeight = entity.Delivery_Way_UpWeight;
        Delivery_Way_FeeType = entity.Delivery_Way_FeeType;
        Delivery_Way_Fee = entity.Delivery_Way_Fee;
        Delivery_Way_InitialFee = entity.Delivery_Way_InitialFee;
        Delivery_Way_UpFee = entity.Delivery_Way_UpFee;
        Delivery_Way_Status = entity.Delivery_Way_Status;
        Delivery_Way_Cod = entity.Delivery_Way_Cod;
        Delivery_Way_Img = entity.Delivery_Way_Img;
        Delivery_Way_Url = entity.Delivery_Way_Url;
        Delivery_Way_Intro = entity.Delivery_Way_Intro;
        Delivery_Way_Site = entity.Delivery_Way_Site;
    }
    
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="配送方式修改 - 会员中心 - " + pub.SEO_TITLE()%></title>
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
                             
                          <form name="formadd" method="post" action="/supplier/delivery_way_do.aspx">
      <table width="100%" border="0" cellspacing="0" cellpadding="5" id="apply_1_content" class="table_padding_5">
      
            <tr>
                <td align="right" style="line-height: 24px;" class="t12_53">
                    配送方式名称
                </td>
                <td align="left">
                    <input name="Delivery_Way_Name" type="text" id="Delivery_Way_Name" class="txt_border"
                        size="40" maxlength="100" value="<% =Delivery_Way_Name%>" />
                    <span class="t14_red">*</span>
                </td>
            </tr>
            <tr>
                <td align="right" style="line-height: 24px;" class="t12_53">
                    费用方式
                </td>
                <td align="left">
                   <input type="radio" name="delivery_way_feetype" value="0" onclick="if(this.checked){samefee.style.display='';weightfee.style.display='none';weight.style.display='none';}" <% =pub.CheckRadio(Delivery_Way_FeeType.ToString(), "0")%> />统一计费 &nbsp; &nbsp; <input type="radio" name="delivery_way_feetype" value="1"  onclick="if(this.checked){samefee.style.display='none';weightfee.style.display='';weight.style.display='';}" <% =pub.CheckRadio(Delivery_Way_FeeType.ToString(), "1")%>/>按重量计费
                    <span class="t14_red">*</span>
                </td>
            </tr>
            <tr id="samefee" name="samefee"<% if (Delivery_Way_FeeType == 1) { Response.Write("style=\"display:none;\""); } %>>
                <td align="right" style="line-height: 24px;" class="t12_53">
                    费用
                </td>
                <td align="left">
                    <input name="delivery_way_fee" type="text" size="10" id="delivery_way_fee" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" value="<% =Delivery_Way_Fee%>" />
                    <span class="t14_red">*</span>
                </td>
            </tr>
            <tr id="weight" <% if (Delivery_Way_FeeType == 0) { Response.Write("style=\"display:none;\""); } %>>
                <td align="right" style="line-height: 24px;" class="t12_53">
                    重量
                </td>
                <td align="left">
                     首重重量 
            <select name="Delivery_Way_InitialWeight">
              <option value="500" <% =pub.CheckSelect(Delivery_Way_InitialWeight.ToString(), "500")%>>500克</option>
              <option value="1000" <% =pub.CheckSelect(Delivery_Way_InitialWeight.ToString(), "1000")%>>1公斤</option>
              <option value="1200" <% =pub.CheckSelect(Delivery_Way_InitialWeight.ToString(), "1200")%>>1.2公斤</option>
              <option value="2000" <% =pub.CheckSelect(Delivery_Way_InitialWeight.ToString(), "2000")%>>2公斤</option>
              <option value="5000" <% =pub.CheckSelect(Delivery_Way_InitialWeight.ToString(), "5000")%>>5公斤</option>
              <option value="10000" <% =pub.CheckSelect(Delivery_Way_InitialWeight.ToString(), "10000")%>>10公斤</option>
              <option value="20000" <% =pub.CheckSelect(Delivery_Way_InitialWeight.ToString(), "20000")%>>20公斤</option>
              <option value="50000" <% =pub.CheckSelect(Delivery_Way_InitialWeight.ToString(), "50000")%>>50公斤</option>
            </select>
            续费重量 
            <select name="Delivery_Way_UpWeight">
                <option value="500" <% =pub.CheckSelect(Delivery_Way_UpWeight.ToString(), "500")%>>500克</option>
              <option value="1000" <% =pub.CheckSelect(Delivery_Way_UpWeight.ToString(), "1000")%>>1公斤</option>
              <option value="1200" <% =pub.CheckSelect(Delivery_Way_UpWeight.ToString(), "1200")%>>1.2公斤</option>
              <option value="2000" <% =pub.CheckSelect(Delivery_Way_UpWeight.ToString(), "2000")%>>2公斤</option>
              <option value="5000" <% =pub.CheckSelect(Delivery_Way_UpWeight.ToString(), "5000")%>>5公斤</option>
              <option value="10000" <% =pub.CheckSelect(Delivery_Way_UpWeight.ToString(), "10000")%>>10公斤</option>
              <option value="20000" <% =pub.CheckSelect(Delivery_Way_UpWeight.ToString(), "20000")%>>20公斤</option>
              <option value="50000" <% =pub.CheckSelect(Delivery_Way_UpWeight.ToString(), "50000")%>>50公斤</option>
            </select>
                    <span class="t14_red">*</span>
                </td>
            </tr>
            <tr id="weightfee" <% if (Delivery_Way_FeeType == 0) { Response.Write("style=\"display:none;\""); } %>>
                <td align="right" style="line-height: 24px;" class="t12_53">
                    费用
                </td>
                <td align="left">
                     首重重量 
            首重费用 <input name="Delivery_Way_InitialFee" type="text" id="Delivery_Way_InitialFee" size="9" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" value="<% =Delivery_Way_InitialFee%>" /> &nbsp; &nbsp; 续重费用 <input name="Delivery_Way_UpFee" type="text" id="Delivery_Way_UpFee"  size="9" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" value="<% =Delivery_Way_UpFee%>"/>
                    <span class="t14_red">*</span>
                </td>
            </tr>
            <tr>
                <td align="right" style="line-height: 24px;" class="t12_53">
                    排序
                </td>
                <td align="left">
                    <input name="Delivery_Way_Sort" type="text" id="Delivery_Way_Sort" size="10" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="10" value="<% =Delivery_Way_Sort%>" />
                </td>
            </tr>
            <tr>
                <td align="right" style="line-height: 24px;" class="t12_53">
                    启用
                </td>
                <td align="left">
                    <input name="Delivery_Way_Status" type="radio" value="1" <% =pub.CheckRadio(Delivery_Way_Status.ToString(), "1")%>/>是 <input type="radio" name="Delivery_Way_Status" value="0" <% =pub.CheckRadio(Delivery_Way_Status.ToString(), "0")%>/>否
                    <span class="t14_red">*</span>
                </td>
            </tr>
            <tr>
                <td align="right" style="line-height: 24px;" class="t12_53">
                    支持货到付款
                </td>
                <td align="left">
                    <input name="Delivery_Way_Cod" type="radio" value="1" <% =pub.CheckRadio(Delivery_Way_Cod.ToString(), "1")%>/>是 <input type="radio" name="Delivery_Way_Cod" value="0" <% =pub.CheckRadio(Delivery_Way_Cod.ToString(), "0")%>/>否
                    <span class="t14_red">*</span>
                </td>
            </tr>
             <tr>
                <td align="right" style="line-height: 24px;" class="t12_53">
                    备注说明
                </td>
                <td align="left">
                   <textarea cols="50" id="Delivery_Way_Intro" name="Delivery_Way_Intro" rows="5"><% =Delivery_Way_Intro%></textarea>
                </td>
            </tr>
        <tr>
              <td align="right" class="t12_53">
                  </td>
              <td>
              <input name="Delivery_Way_ID" type="hidden" id="Delivery_Way_ID" value="<%=Delivery_Way_ID %>">
              <input name="action" type="hidden" id="action" value="renew">
              <input name="btn_submit" type="image" src="/images/save.jpg" />
              </td>
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
