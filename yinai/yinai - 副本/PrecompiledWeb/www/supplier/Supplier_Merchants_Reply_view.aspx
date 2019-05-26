<%@ Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%--<%@ Register Src="~/Public/Supplier_Top.ascx" TagPrefix="uctop" TagName="top" %>--%>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>
<% 
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    Supplier supplier = new Supplier();


    int Message_ID = tools.NullInt(Request["Message_ID"]);
    supplier.Supplier_Login_Check("/supplier/supplier_merchants_reply.aspx");

    SupplierMerchantsMessageInfo entity = supplier.GetSupplierMerchantsMessageInfo(Message_ID);

    if (entity ==null)
    {
        Response.Redirect("/supplier/index.aspx");
    }
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="品牌加盟 - 我是卖家 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script type="text/javascript" src="/scripts/My97DatePicker/WdatePicker.js"></script>
    <script src="/scripts/jquery-extend-AdAdvance3.js" type="text/javascript"></script>
    <script type="text/javascript" src="/scripts/common.js"></script>
    <!--滑动门 开始-->
    <script type="text/javascript" src="/scripts/hdtab.js"></script>
    <script type="text/javascript">

        function check_supplier_nickname(object) //1
        {
            $.ajaxSetup({ async: false });
            $("#" + object + "_tip").load("/supplier/register_do.aspx?action=checknickname&val=" + encodeURIComponent($("#" + object).val()) + "&timer=" + Math.random());
            if ($("#" + object + "_tip").html().indexOf("cc0000") > 0) {
                return false;
            }
            else {
                return true;
            }
        }
    </script>

</head>
<body>
    <%--<uctop:top ID="top1" runat="server" />--%>
    <!--主体 开始-->
    <div class="content02" style="background-color: #FFF;">
        <div class="content02_main" style="background-color: #FFF;">
            <!--位置说明 开始-->
            <div class="position">当前位置 > <a href="/supplier/">我是卖家</a> > 店铺管理 > <strong>品牌加盟</strong></div>
            <!--位置说明 结束-->
            <div class="partc">
                <div class="pc_left">
                    <div class="blk12">
                        <%supplier.Get_Supplier_Left_HTML(2, 8); %>
                    </div>
                </div>
                <div class="pc_right">
                    <div class="title03">品牌加盟</div>
                    <div class="blk17">
                        <form name="frm_reply" id="frm_reply" method="post" action="/supplier/merchants_do.aspx">

                            <table width="100%" border="0" cellspacing="0" cellpadding="5" class="table_padding_5">
                                <tr>
                                    <td width="92" class="name">内容
                                    </td>
                                    <td width="801">
                                        <%=entity.Message_Content %>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="92" class="name">联系人
                                    </td>
                                    <td width="801">
                                        <%=entity.Message_Contactman %>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="92" class="name">手机
                                    </td>
                                    <td width="801">
                                        <%=entity.Message_ContactMobile %>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="92" class="name">Email
                                    </td>
                                    <td width="801">
                                        <%=entity.Message_ContactEmail %>
                                    </td>
                                </tr>
                            </table>
                        </form>
                    </div>
                </div>
                <div class="clear">
                </div>
            </div>
        </div>
    </div>
    <!--主体 结束-->
  <ucbottom:bottom runat="server" ID="Bottom" />
</body>
</html>
