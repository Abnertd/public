<%@ Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>
<% 
    
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    Supplier supplier = new Supplier();
    Session["Cur_Position"] = Session["Home_Position"] = "";
    string type = tools.CheckStr(Request["type"]);
    supplier.Supplier_Login_Check("/supplier/mycontract_list.aspx?type=" + type);

    string date_start, date_end;
    string contract_sn;
    contract_sn = tools.CheckStr(Request["contract_sn"]);

    if (type != "temp" && type != "processing" && type != "success" && type != "faiture")
    {
        type = "all";
    }
    date_start = tools.FormatDate(DateTime.Now, "yyyy-MM-dd");
    date_end = tools.FormatDate(DateTime.Now, "yyyy-MM-dd");
    string title = "";
    title = "销售合同管理";
    
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>
        <%=title + " - 交易管理 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script src="/scripts/jquery-extend-AdAdvance3.js" type="text/javascript"></script>
    <link type="text/css" href="/Scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script>
    <script src="/scripts/common.js" type="text/javascript"></script>
    <!--弹出菜单 start-->
    <script type="text/javascript">
        $(document).ready(function () {
            var byt = $(".testbox li");
            var box = $(".boxshow")
            byt.hover(
                 function () {
                     $(this).find(".boxshow").show(); $(this).find(".a3").attr("class", "a3 a3h");
                 },
                function () {
                    $(this).find(".boxshow").hide(); $(this).find(".a3h").attr("class", "a3");
                }
            );
        });
    </script>

    <!--弹出菜单 end-->

</head>
<body>
    <uctop:top ID="top1" runat="server" />
    <!--主体 开始-->
    <div class="webwrap"> 
    <div class="content02" style="margin-bottom:20px;">
        <!--位置说明 开始-->
        <div class="position">
            您现在的位置 > <a href="/">首页</a> > <a href="index.aspx">我是卖家</a> > <a href="/supplier/mycontract_list.aspx">
                <%=title %></a>
        </div>
        <div class="clear">
        </div>
        <!--位置说明 结束-->
        <div class="partd_1">
            <div class="pd_left">
                <% supplier.Get_Supplier_Left_HTML(4, 7); %>
            </div>
            <div class="pd_right">
                <div class="blk14_1" style="margin-top:0px;">
                    <h2>销售合同管理</h2>
                    <div class="main">
                        <div class="zkw_order">
                            <h3 class="zkw_title21">
                                <%=supplier.Contract_TabControl(1,type)%>
                            </h3>
                            <% supplier.Contract_List(1, type); %>
                        </div>
                    </div>
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
