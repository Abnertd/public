<%@ Page Language="C#" CodePage="65001" %>

<%SysMenu Menu = new SysMenu();

  Statistic myApp = new Statistic();%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Top</title>
    <link href="/CSS/Style.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            $("#switchleft").bind("click", function () {
                if ($("#mainframeset", parent.document).attr("cols") == "210,*") {
                    $("#mainframeset", parent.document).attr("cols", "0,*");
                } else {
                    $("#mainframeset", parent.document).attr("cols", "210,*");
                }
            });
        });
    </script>
    <script src="/Scripts/msclass.js" type="text/javascript"></script>
    <script src="/Scripts/common.js" type="text/javascript"></script>
</head>
<body>


    <div class="head">
        <div class="head_info">
            <div class="head_info_right">
                <ul>
                    <li><a href="/user/editpassword.aspx?action=loginout" target="main">修改密码</a></li>
                    <li><a href="/logindo.aspx?action=loginout">
                        <img src="/images/icon03.png">退出</a></li>
                    <li>
                        <img src="/images/icon02.png">欢迎您 <%=Session["User_Name"]%></li>
                </ul>
                <div class="clear"></div>
            </div>

            <div class="logo2">
                <img src="/images/logo.jpg">
            </div>

         <%--   <div class="word" id="top_scroll">
                <div id="scroll_content"> 
                    <span style="height: 29px;">
                        <img src="/images/icon01.png" align="absmiddle" />
                        <a href="/Supplier/Supplier_list.aspx" target="main">供应商注册待审核数量 <b style="color: red;"><%=myApp.GetUnAuditSupplierAmount()%></b> 个</a></span>
                    <span style="height: 29px;">
                        <img src="/images/icon01.png" align="absmiddle" />
                        <a href="/supplier/supplier_cert_list.aspx" target="main">待审核供应商资质数量 <b style="color: red;"><%=myApp.GetUnAuditSupplierCertAmount()%></b> 个</a></span>
                    <span style="height: 29px;">
                        <img src="/images/icon01.png" align="absmiddle" />
                        <a href="/member/feedback_list.aspx?isreply=2" target="main">会员提交未回复留言信息 <b style="color: red;"><%=myApp.GetUnReplyMessageAmount()%></b> 个</a></span>
                    <span style="height: 29px;">
                        <img src="/images/icon01.png" align="absmiddle" />
                        <a href="/Product/product.aspx?listtype=unAudit" target="main">待审核产品信息 <b style="color: red;"><%=myApp.GetUnAuditProductAmount()%></b> 个</a></span>
                    <span style="height: 29px;">
                        <img src="/images/icon01.png" align="absmiddle" />
                        <a href="/stock/stockout.aspx?isprocess=2" target="main">未处理缺货登记信息 <b style="color: red;"><%=myApp.GetUnProcessStockoutAmount()%></b> 个</a></span>


                </div>
            </div>--%>


        </div>
        <div class="nav" id="nav_div">
            <ul>
                <li id="dashboard" class="on" onclick="menuChange(this);"><a href="javascript:;">我的桌面</a></li>
                <li>|</li>

                <%if (Menu.Sys_Menu_Display_Top(1))
                  { %>
                <li id="orders" class="off" onclick="menuChange(this);"><a href="javascript:;">交易管理</a></li>
                <li>|</li>
                <%} %>

                <%if (Menu.Sys_Menu_Display_Top(2))
                  { %>
                <li id="product" class="off" onclick="menuChange(this);"><a href="javascript:;">商品管理</a></li>
                <li>|</li>
                <%} %>

                <%if (Menu.Sys_Menu_Display_Top(3))
                  { %>
                <li id="member" class="off" onclick="menuChange(this);"><a href="javascript:;">采购商管理</a></li>
                <li>|</li>
                <%} %>


                <%if (Menu.Sys_Menu_Display_Top(9))
                  { %>
                <li id="supplier" class="off" onclick="menuChange(this);"><a href="javascript:;">供应商管理</a></li>
                <li>|</li>
                <%} %>


                <%if (Menu.Sys_Menu_Display_Top(4))
                  { %>
                <li id="marketing" class="off" onclick="menuChange(this);"><a href="javascript:;">营销推广</a></li>
                <li>|</li>
                <%} %>


                <%if (Menu.Sys_Menu_Display_Top(5))
                  { %>
                <li id="statistical" class="off" onclick="menuChange(this);"><a href="javascript:;">统计报表</a></li>
                <li>|</li>
                <%} %>


                <%if (Menu.Sys_Menu_Display_Top(6))
                  { %>
                <li id="content" class="off" onclick="menuChange(this);"><a href="javascript:;">内容管理</a></li>
                <li>|</li>
                <%} %>

                <%if (Menu.Sys_Menu_Display_Top(8))
                  { %>
                <li id="system" class="off" onclick="menuChange(this);"><a href="javascript:;">系统管理</a></li>
                <%} %>
            </ul>
            <div class="clear"></div>
        </div>
    </div>


    <script type="text/javascript">
        function menuChange(currobj) {
            //var menuArray = new Array("dashboard", "orders", "product", "member", "marketing", "statistical", "content", "system");
            var menuArray = $("#nav_div").find("li");
            for (var i = 0; i < menuArray.length; i++) {
                if (menuArray[i].className == "on") {
                    menuArray[i].className = "off";
                }
            }
            currobj.className = "on";

            var mainTarget = "/main.aspx", leftTarget = "/public/left.aspx";

            switch (currobj.id) {
                case "dashboard":
                    mainTarget = "/main.aspx";
                    leftTarget = "/public/left.aspx?channel=0";
                    break;
                case "purchase":
                    mainTarget = "/supplier/Supplier_Purchase_list.aspx?purchase_type=0";
                    leftTarget = "/public/left.aspx?channel=10";
                    break;
                case "orders":
                    mainTarget = "/orders/orders_list.aspx";
                    leftTarget = "/public/left.aspx?channel=1";
                    break;
                case "product":
                    mainTarget = "/product/Product.aspx";
                    leftTarget = "/public/left.aspx?channel=2";
                    break;
                case "member":
                    mainTarget = "/member/member_list.aspx";
                    leftTarget = "/public/left.aspx?channel=3";
                    break;
                case "supplier":
                    mainTarget = "/supplier/supplier_list.aspx";
                    leftTarget = "/public/left.aspx?channel=9";
                    break;
                case "marketing":
                    mainTarget = "/ad/ad.aspx";
                    leftTarget = "/public/left.aspx?channel=4";
                    break;
                case "statistical":
                    mainTarget = "/statistical/salesvolume.aspx";
                    leftTarget = "/public/left.aspx?channel=5";
                    break;
                case "content":
                    mainTarget = "/about/about_list.aspx";
                    leftTarget = "/public/left.aspx?channel=6";
                    break;
                case "system":
                    mainTarget = "/system.aspx";
                    leftTarget = "/public/left.aspx?channel=8";
                    break;
                case "scm":
                    mainTarget = "/scm/stockalert.aspx";
                    leftTarget = "/public/left.aspx?channel=7";
                    break;
                default: break;
            }

            window.parent.main.location.href = mainTarget;
            window.parent.left.location.href = leftTarget;
        }
    </script>


    
    <%-- /************control_enabel：是否启用按钮控制ID************/
    /************direction：滚动方向：0上 1下 2左 3右************/
    /************left_div：左控制按钮ID************/
    /************right_div：右控制按钮ID************/
    /************scroll_body：循环主体容器ID************/
    /************scroll_content：循环主体内容容器ID************/
    /************total_width：循环体总宽度************/
    /************total_height：循环体总高度************/
    /************scroll_width：每次循环宽度（0为翻屏）************/
    /************scroll_speed：循环速度步长（越大越慢）************/
	/************wait_time：翻屏等待时间************/--%>

              <%--  <script>srcoll_left_right_Control(false, 0, "", "", "top_scroll", "scroll_content", 500, 29, 29, 100, 60);</script>--%>
</body>
</html>
