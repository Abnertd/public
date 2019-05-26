<%@ Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Register Src="~/Public/Top.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>
<% 
    
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    Supplier myApp = new Supplier();
    Addr addr = new Addr();
    myApp.Supplier_Login_Check("/supplier/Shopping_add.aspx");

    
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>
        <%="发布采购申请 - 会员中心 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link type="text/css" href="/Scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script src="/scripts/jquery-extend-AdAdvance3.js" type="text/javascript"></script>
    <script src="/scripts/common.js" type="text/javascript"></script>
    <script type="text/javascript" src="/scripts/supplier.js"></script>
    <script src="/Scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script>
    <script src="/Public/ckeditor/ckeditor.js" type="text/javascript"></script>
    <style type="text/css">
    .zkw_title21{margin-bottom:10px;}
    </style>
</head>
<body>
    <uctop:top ID="top1" runat="server" />
    <!--主体 开始-->
    <div class="content">
        <!--位置说明 开始-->
        <div class="position">
            您现在的位置 > <a href="/">首页</a> > <a href="index.aspx">会员中心</a> > <a href="Shopping_add.aspx">
                发布采购申请</a>
        </div>
        <div class="clear">
        </div>
        <!--位置说明 结束-->
        <div class="parth">
            <div class="ph_left">
                <% myApp.Get_Supplier_Left_HTML(5, 1); %>
            </div>
            <div class="ph_right">
                <div class="blk13">
                    <h2>
                        发布采购申请</h2>
                    <div class="main">
                        <div class="zkw_order">
                            <h3 class="zkw_title21">
                                <%
                                    StringBuilder strHTML = new StringBuilder();
                                    strHTML.Append("<ul class=\"zkw_lst31\">");
                                    strHTML.Append("	<li class=\"on\" id=\"apply_1\" onclick=\"Set_Tab('apply',1,2,'on','');\">基本信息</li>");
                                    strHTML.Append("	<li id=\"apply_2\" onclick=\"Set_Tab('apply',2,2,'on','');\">采购清单</li>");
                                    strHTML.Append("</ul><div class=\"clear\"></div>");
                                    Response.Write(strHTML.ToString());
                                %>
                            </h3>
                            <form name="formadd" id="formadd" method="post" action="/supplier/shopping_do.aspx">
                            <table width="100%" border="0" cellspacing="0" cellpadding="5" id="apply_1_content"
                                class="table_padding_5">
                                <tr>
                                    <td align="right" style="line-height: 24px;" class="t12_53">
                                        采购类型
                                    </td>
                                    <td align="left">
                                        <input type="radio" value="0" name="Purchase_TypeID" checked="checked" />定制采购<input
                                            type="radio" value="1" name="Purchase_TypeID" />代理采购 <span class="t12_red">*</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="line-height: 24px;" class="t12_53">
                                        采购标题
                                    </td>
                                    <td align="left">
                                        <input name="Purchase_Title" type="text" id="Purchase_Title" class="txt_border"
                                             maxlength="100" value="" />
                                        <span class="t12_red">*</span>
                                    </td>
                                </tr>
                                 <tr>
                <td align="right" style="line-height: 24px;" class="t12_53">
                    采购分类
                </td>
                <td align="left">
                    <span id="main_cate"><%=myApp.Purchase_Category_Select(0, "main_cate")%></span>
            <span id="div_Purchase_Cate"></span>
                    <span class="t12_red">*</span>
                </td>
            </tr>
                                <tr>
                                    <td align="right" style="line-height: 24px;" class="t12_53">
                                        交货时间
                                    </td>
                                    <td align="left">
                                        <input type="text" class="input_calendar" name="Purchase_DeliveryTime" id="Purchase_DeliveryTime"
                                            maxlength="10" readonly="readonly" value="" />
                                        <span class="t12_red">*</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="line-height: 24px;" class="t12_53">
                                        交货地点
                                    </td>
                                    <td align="left">
                                        <table border="0" cellspacing="0" cellpadding="2">
                                            <tr>
                                                <td id="div_area" align="left">
                                                    <%=addr.SelectAddress("div_area", "Purchase_State", "Purchase_City", "Purchase_County", "", "", "")%>
                                                </td>
                                                <td align="left">
                                                    <input name="Purchase_Address" type="text" id="Text1" class="txt_border" size="40"
                                                        maxlength="100" value="" />
                                                    <span class="t12_red">*</span>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="line-height: 24px;" class="t12_53">
                                        有效期
                                    </td>
                                    <td align="left">
                                        <input type="text" class="input_calendar" name="Purchase_ValidDate" id="Purchase_ValidDate"
                                            maxlength="10" readonly="readonly" value="" />
                                        <script type="text/javascript">
                                            $(document).ready(function () {
                                                $("#Purchase_DeliveryTime").datepicker({ numberOfMonths: 1 });
                                                $("#Purchase_ValidDate").datepicker({ numberOfMonths: 1 });
                                            });
                                        </script>
                                        <span class="t12_red">*</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="line-height: 24px;" class="t12_53">
                                        上传附件
                                    </td>
                                    <td align="left">
                                        <iframe id="iframe1" src="<% =Application["Upload_Server_URL"]%>/public/uploadify.aspx?App=attachment&formname=formadd&frmelement=Purchase_Attachment&rtvalue=1&rturl=<% =Application["Upload_Server_Return_WWW"]%>"
                                            width="242" height="90" frameborder="0" scrolling="no" style="margin-left: 10px;">
                                        </iframe>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="line-height: 24px;" class="t12_53">
                                        附件路径
                                    </td>
                                    <td align="left">
                                       
                                        <input name="Purchase_Attachment" id="Purchase_Attachment" class="txt_border"
                                           size="40" type="text" value="" readonly="readonly" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="line-height: 24px;" class="t12_53">
                                        上传图片
                                    </td>
                                    <td align="left">
                                        <iframe id="iframe2" src="<% =Application["Upload_Server_URL"]%>/public/uploadify.aspx?App=content&formname=formadd&frmelement=Purchase_Intro&rtvalue=1&rturl=<% =Application["Upload_Server_Return_WWW"]%>"
                                            width="242" height="90" frameborder="0" scrolling="no" style="margin-left: 10px;">
                                        </iframe>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="line-height: 24px;" class="t12_53">
                                        采购描述
                                    </td>
                                    <td align="left">
                                        <textarea cols="80" id="Purchase_Intro" name="Purchase_Intro" rows="16"></textarea>
                                        <script type="text/javascript">
                                            CKEDITOR.replace('Purchase_Intro');
                                        </script>
                                    </td>
                                </tr>
                                

                            </table>
                               <table width="100%" border="0" cellspacing="0" cellpadding="0" id="apply_2_content" style="display:none">
                               <tr><td>
                               <%
                                   int i=0;
                                   for (i = 1; i <= 5; i++)
                                 { %>
                                    <table width="100%" border="0" cellspacing="5" cellpadding="5">
                                   <tr><td><b>商品信息<%=i %>：</b></td></tr>
                                     <tr>
                                        <td align="right" style="line-height: 24px;" width="100" class="t12_53">
                                            产品名称：
                                        </td>
                                        <td width="270">
                                             <input type="text" class="txt_border" name="Detail_Name<%=i %>" id="Detail_Name<%=i %>" /> <span class="t12_red">*</span>
                                        </td>
                                        <td align="right" style="line-height: 24px;" width="100" class="t12_53">
                                            规格/单位：
                                        </td>
                                        <td>
                                           <input type="text" class="txt_border" name="Detail_Spec<%=i %>" id="Detail_Spec<%=i %>" /> <span class="t12_red">*</span>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td align="right" style="line-height: 24px;" class="t12_53">
                                            采购数量：
                                        </td>
                                        <td>
                                             <input type="text" class="txt_border" name="Detail_Amount<%=i %>" id="Detail_Amount<%=i %>" /> <span class="t12_red">*</span>
                                        </td>
                                        <td align="right" style="line-height: 24px;" class="t12_53">
                                            采购单价：
                                        </td>
                                        <td>
                                           <input type="text" class="txt_border" name="Detail_Price<%=i %>" id="Detail_Price<%=i %>" /> <span class="t12_red">*</span>
                                        </td>
                                    </tr>
                                </table>
                                <%} %>
                                <input type="hidden" value="<%=i %>" name="Purchase_Amount" />
                               </td></tr>
                               </table>
                               
                               
                          <table width="100%" border="0" cellspacing="0" cellpadding="5" class="table_padding_5">
                           <tr>
                                    <td align="center">
                                        <input name="action" type="hidden" id="action" value="shop_apply" />
                                       
                                         <input type="submit" class="buttonSkinA" value="保存草稿" onClick="this.form.action.value='shop_apply';" />
                                          <input type="submit" class="buttonSkinA" value="发布"onClick="this.form.action.value='shop_apply_1';" />
                                           
                                    </td>
                                </tr>
                            </table>

                            </form>
                        </div>
                    </div>
                </div>
            </div>
            <div class="clear">
            </div>
        </div>
    </div>
    <!--主体 结束-->
  <ucbottom:bottom runat="server" ID="Bottom" />
</body>
</html>
