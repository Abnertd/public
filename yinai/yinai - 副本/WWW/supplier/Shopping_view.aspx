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
    myApp.Supplier_Login_Check("/supplier/Shopping_view.aspx");

    int apply_id = tools.CheckInt(Request["apply_id"]);
    string cateName = " -- ";
    if (apply_id == 0)
    {
        Response.Redirect("/supplier/Shopping_list.aspx");
    }

    SupplierPurchaseInfo spinfo = myApp.GetSupplierPurchaseByID(apply_id);
    if (spinfo == null)
    {
        Response.Redirect("/supplier/Shopping_list.aspx");
    }
    else
    {
        if (spinfo.Purchase_SupplierID != tools.NullInt(Session["supplier_id"]))
        {
            Response.Redirect("/supplier/Shopping_list.aspx");
        }

        SupplierPurchaseCategoryInfo category = myApp.GetSupplierPurchaseCategoryByID(spinfo.Purchase_CateID);
        if (category != null)
        {
            cateName = category.Cate_Name;
        }
    }
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>
        <%="采购申请查看 - 会员中心 - " + pub.SEO_TITLE()%></title>
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
</head>
<body>
    <uctop:top ID="top1" runat="server" />
    <!--主体 开始-->
    <div class="content">
        <!--位置说明 开始-->
        <div class="position">
            您现在的位置 > <a href="/">首页</a> > <a href="index.aspx">会员中心</a> > <span>采购申请查看</span>
        </div>
        <div class="clear">
        </div>
        <!--位置说明 结束-->
        <div class="parth">
            <div class="ph_left">
                <% myApp.Get_Supplier_Left_HTML(5, 2); %>
            </div>
            <div class="ph_right">
                <div class="blk13">
                    <h2>
                        采购申请查看</h2>
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
                            <table width="100%" border="0" cellspacing="0" cellpadding="5" id="apply_1_content"
                                class="table_padding_5">
                                <tr>
                                    <td align="right" width="100" style="line-height: 24px;" class="t12_53">
                                        采购类型
                                    </td>
                                    <td align="left">
                                        <%
                                            pub.GetPurchaseType(spinfo.Purchase_TypeID);
                                            
                                        %>
                                    </td>
                                </tr>
                                   <tr>
                                    <td align="right" width="100" style="line-height: 24px;" class="t12_53">
                                        采购分类
                                    </td>
                                    <td align="left">
                                        <%=cateName %>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="line-height: 24px;" class="t12_53">
                                        采购标题
                                    </td>
                                    <td align="left">
                                        <%=spinfo.Purchase_Title %>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="line-height: 24px;" class="t12_53">
                                        采购类别
                                    </td>
                                    <td align="left">
                                        <%=spinfo.Purchase_CateID %>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="line-height: 24px;" class="t12_53">
                                        交货时间
                                    </td>
                                    <td align="left">
                                        <%=spinfo.Purchase_DeliveryTime.ToString("yyyy-MM-dd") %>
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
                                                    <%=addr.DisplayAddress(spinfo.Purchase_State, spinfo.Purchase_City, spinfo.Purchase_County) + " " + spinfo.Purchase_Address%>
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
                                        <%=spinfo.Purchase_ValidDate.ToString("yyyy-MM-dd") %>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="line-height: 24px;" class="t12_53">
                                        附件路径
                                    </td>
                                    <td align="left">
                                        <%=Application["Upload_Server_URL"] + spinfo.Purchase_Attachment%>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="line-height: 24px;" class="t12_53">
                                        采购描述
                                    </td>
                                    <td align="left">
                                        <%=spinfo.Purchase_Intro %>
                                    </td>
                                </tr>
                            </table>
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" id="apply_2_content"
                                style="display: none">
                                <tr>
                                    <td>
                                    <table width="100%" cellpadding="0" align="center" cellspacing="1" style="background:#dadada; margin-top:10px;" ><tr style="background:url(/images/ping.jpg); height:30px;">  <th align="center" valign="middle">产品名称</th>  <th align="center" valign="middle" >规格/单位</th>  <th width="120" align="center" valign="middle">采购数量</th>  <th width="110" align="center" valign="middle">采购单价</th></tr>

                                        <%
                                            IList<SupplierPurchaseDetailInfo> entitys = myApp.GetSupplierPurchaseDetailsByApplyID(apply_id);
                                            int i = 0;
                                            if (entitys != null)
                                            {

                                                foreach (SupplierPurchaseDetailInfo entity in entitys)
                                                {
                                                    i++;
                                        %>
                                        <tr bgcolor="#FFFFFF" height="35"><td align="center" valign="middle"><%=entity.Detail_Name %></td><td align="center" valign="middle"><%=entity.Detail_Spec %></td><td align="center" valign="middle"><%=entity.Detail_Amount %></td><td align="center" valign="middle"> <%=entity.Detail_Price %></td></tr>
                                        <%
                                           
}
                                   }
                                        %>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <table width="100%" border="0" cellspacing="0" cellpadding="5" class="table_padding_5">
                                <tr>
                                    <td align="center">
                                    </td>
                                </tr>
                            </table>
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
