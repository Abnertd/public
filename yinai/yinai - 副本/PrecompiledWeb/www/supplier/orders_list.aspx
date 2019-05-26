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
    supplier.Supplier_Login_Check("/supplier/order_list.aspx?type=" + type);

    //string action=tools.NullStr(Request.QueryString["action"]);
    string date_start, date_end;
    string orders_sn;
    orders_sn = tools.CheckStr(Request["orders_sn"]);

    if (type != "unprocessed" && type != "processing" && type != "success" && type != "faiture")
    {
        type = "all";
    }
    date_start = tools.FormatDate(DateTime.Now, "yyyy-MM-dd");
    date_end = tools.FormatDate(DateTime.Now, "yyyy-MM-dd");
    string title = "";
    title = "我的订单";
    
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>
        <%=title + " - 我是卖家 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
      <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/layer/layer.js"></script>
    <script type="text/javascript" src="/scripts/common.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script src="/scripts/jquery-extend-AdAdvance3.js" type="text/javascript"></script>
    <link type="text/css" href="/Scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script>

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

    <script type="text/javascript">
        function check_all() {
            if ($("#check_all").attr('checked')) {
                $("input[name='check']").attr('checked', 'checked');
            }
            else {
                $("input[name='check']").attr('checked', '');
            }
        }
        $(function () {
            $("#export").click(function () {
                var checkedValues = new Array();
                $("input[name='check']").each(function () {
                    if ($(this).is(':checked')) {
                        checkedValues.push($(this).val());
                    }
                });
                strDelGoodID = checkedValues.join(',')
                location.href = 'orders_do.aspx?action=ordergoodsexport&orders_id=' + strDelGoodID;
            })
            $("#ordersexport").click(function () {
                var checkedValues = new Array();
                $("input[name='check']").each(function () {
                    if ($(this).is(':checked')) {
                        checkedValues.push($(this).val());
                    }
                });
                strDelGoodID = checkedValues.join(',')
                location.href = 'orders_do.aspx?action=ordersexport&orders_id=' + strDelGoodID;
            })
        })


        function OrderSearch(object) {
            var keyword = $("#" + object).val();
            var orderDate = $("#orderDate").val();
            var orderStatus = $("#orderStatus").val();

            if (keyword == "订单号/商品名称/商品编号") {
                alert('请输入查询关键词！');
            } else {
                window.location.href = "/supplier/order_list.aspx?keyword=" + keyword + "&orderDate=" + orderDate + "&orderStatus=" + orderStatus;
            }
        }

        function OrderDate() {
            var sDate = $("#orderDate").val();
            if (sDate == 0) {
                sDate = 1;
            }
            var oState = $("#orderStatus").val();
            window.location = "/supplier/order_list.aspx?orderDate=" + sDate + "&orderStatus=" + oState;
        }

        function OrderStatus() {
            var sDate = $("#orderDate").val();
            var oState = $("#orderStatus").val();
            window.location = "/supplier/order_list.aspx?orderDate=" + sDate + "&orderStatus=" + oState;
        }

    </script>
    
</head>
<body>
    <uctop:top ID="top1" runat="server" />
    <!--主体 开始-->
    <div class="webwrap">
    <div class="content02" style="background-color: #FFF;">

            <!--位置说明 开始-->
            <div class="position">当前位置 > <a href="/supplier/">我是卖家</a> > 交易管理 > <strong>我的订单</strong></div>
            <!--位置说明 结束-->
                <div class="partd_1">
                <div class="pd_left">
                  <%--  <div class="blk12">--%>
                        <% supplier.Get_Supplier_Left_HTML(1, 1); %>
                  <%--  </div>--%>
                </div>
                <div class="pd_right">
                    <div class="blk14_1" style="margin-top:0px;">
                    <h2>我的订单</h2>

                    <% supplier.Orders_List(); %>
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
