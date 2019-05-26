<%@ Page Language="C#" CodePage="65001"%>
<%SysMenu Menu = new SysMenu(); 

   Statistic myApp = new Statistic();%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Top</title>
    <link href="/CSS/Style.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function() {
            $("#switchleft").bind("click", function(){
                if($("#mainframeset", parent.document).attr("cols") == "210,*"){
                    $("#mainframeset", parent.document).attr("cols", "0,*");
                }else{
                    $("#mainframeset", parent.document).attr("cols", "210,*");
                }
            }); 
        });
    </script>
    <script src="/Scripts/msclass.js" type="text/javascript"></script>
    <script src="/Scripts/common.js" type="text/javascript"></script>
</head>
<body>


<table width="100%" border="0" cellspacing="0" cellpadding="0" style="margin:0px auto 10px auto ; background-color:#000000;">
  <tr>
    <td colspan="2"><table width="100%" border="0" cellspacing="0" cellpadding="5">
      <tr>
        <td width="300"><a href="/"><img src="/images/logo.gif" border="0" alt="Trade OSS" /></a></td>
        <td>
        
        <div class="top_scrollinfo" id="top_scroll">
        <div id="scroll_content">
        <span><img src="/images/ico_info.gif" align="absmiddle" /> <a href="/Supplier/Supplier_list.aspx" target="main">供应商注册待审核数量 <b><%=myApp.GetUnAuditSupplierAmount()%></b> 个</a></span>
        <span><img src="/images/ico_info.gif" align="absmiddle" /> <a href="/supplier/supplier_cert_list.aspx" target="main">待审核供应商资质数量 <b><%=myApp.GetUnAuditSupplierCertAmount()%></b> 个</a></span>
        <span><img src="/images/ico_info.gif" align="absmiddle" /> <a href="/member/feedback_list.aspx?isreply=2" target="main">会员提交未回复留言信息 <b><%=myApp.GetUnReplyMessageAmount()%></b> 个</a></span>
        <span><img src="/images/ico_info.gif" align="absmiddle" /> <a href="/Product/product.aspx?listtype=unAudit" target="main">待审核产品信息 <b><%=myApp.GetUnAuditProductAmount()%></b> 个</a></span>
        <span><img src="/images/ico_info.gif" align="absmiddle" /> <a href="/stock/stockout.aspx?isprocess=2" target="main">未处理缺货登记信息 <b><%=myApp.GetUnProcessStockoutAmount()%></b> 个</a></span>
        </div>
        </div>
   <%--     <script>srcoll_left_right_Control(false,0,"","","top_scroll","scroll_content",500,30,30,100,6000);</script>--%>
    <%--        <script>srcoll_left_right_Control_chayou(false, 2, "", "", "top_scroll", "scroll_content", 500, 30, 200, 1, 4, 0);</script>--%>
        </td>
        <td align="right" valign="middle">
        <table border="0" cellspacing="8" cellpadding="0">
          <tr>
            <td align="right" style="display:none"><select id="changesite" name="changesite" onchange="if(this.options[this.selectedIndex].value != ''){parent.window.navigate('/index.aspx?site=' + this.options[this.selectedIndex].value);}"><%// =new Config().optionSite(Session["CurrentSite"].ToString()) %></select></td>
            <td align="right" class="twhite">
            <img src="/images/icon_employee.png" alt="Glaer" /> 欢迎您 <%=Session["User_Name"]%>
            <img src="/images/icon_logout.png" alt="Glaer" /> <a href="/logindo.aspx?action=loginout" class="twhite">退出</a>
            <a href="/user/editpassword.aspx?action=loginout" class="twhite" target="main">修改密码</a>
            </td>
          </tr>
        </table>
        </td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td class="nav_bg">
    <table border="0" cellspacing="0" cellpadding="0" style="margin-left:10px; height:35px;" id="nav_table">
      <tr>
        <td id="dashboard" class="nav_on" onclick="menuChange(this);"><a href="javascript:;">我的桌面</a></td>
        <td class="nav_vline"></td>
        <%--<%if (Menu.Sys_Menu_Display_Top(10))
          { %>
        <td id="purchase" class="nav_off" onclick="menuChange(this);"><a href="javascript:;">商机管理</a></td>
        <td class="nav_vline"></td>
        <%} %>--%>
        <%if (Menu.Sys_Menu_Display_Top(1))
          { %>
        <td id="orders" class="nav_off" onclick="menuChange(this);"><a href="javascript:;">交易管理</a></td>
        <td class="nav_vline"></td>
        <%} %>
        <%if (Menu.Sys_Menu_Display_Top(2))
          { %>
        <td id="product" class="nav_off" onclick="menuChange(this);"><a href="javascript:;">商品管理</a></td>
        <td class="nav_vline"></td>
        <%} %>
         <%if (Menu.Sys_Menu_Display_Top(3))
          { %>
        <td id="member" class="nav_off" onclick="menuChange(this);"><a href="javascript:;">采购商管理</a></td>
        <td class="nav_vline"></td>
          <%} %>
          <%if (Menu.Sys_Menu_Display_Top(9))
            { %>
        <td id="supplier" class="nav_off" onclick="menuChange(this);"><a href="javascript:;">供应商管理</a></td>
        <td class="nav_vline"></td>
        <%} %>
        <%if (Menu.Sys_Menu_Display_Top(4))
          { %>
        <td id="marketing" class="nav_off" onclick="menuChange(this);"><a href="javascript:;">营销推广</a></td>
        <td class="nav_vline"></td>
        <%} %>
        <%if (Menu.Sys_Menu_Display_Top(5))
          { %>
        <td id="statistical" class="nav_off" onclick="menuChange(this);"><a href="javascript:;">统计报表</a></td>
        <td class="nav_vline"></td>
        <%} %>
        <%if (Menu.Sys_Menu_Display_Top(6))
          { %>
        <td id="content" class="nav_off" onclick="menuChange(this);"><a href="javascript:;">内容管理</a></td>
        <td class="nav_vline"></td>
        <%} %>
        
        <%--<%if (Menu.Sys_Menu_Display_Top(7))
          { %>
        <td id="scm" class="nav_off" onclick="menuChange(this);"><a href="javascript:;">进销存</a></td>
        <td class="nav_vline"></td>
        <%} %>--%>
        
        <%if (Menu.Sys_Menu_Display_Top(8))
          { %>
        <td id="system" class="nav_off" onclick="menuChange(this);"><a href="javascript:;">系统管理</a></td>
        <%} %>
        
      </tr>
    </table>
    </td>
    <td class="nav_bg" align="right">
    <table border="0" cellspacing="0" cellpadding="0" style="margin-right:10px; height:35px;">
      <tr>
        <td><img src="/images/icon_switch.png" border="0" align="middle" hspace="3" alt="Glaer" /></td>
        <td><a href="javascript:;" id="switchleft">展开/关闭侧栏</a></td>
      </tr>
    </table>
    </td>
  </tr>
</table>


<script type="text/javascript">
    function menuChange(currobj){
        //var menuArray = new Array("dashboard", "orders", "product", "member", "marketing", "statistical", "content", "system");
        var menuArray = $("#nav_table").find("td");
        for (var i = 0; i < menuArray.length; i++){
            if (menuArray[i].className == "nav_on"){
                menuArray[i].className = "nav_off";
            }
        }
        currobj.className = "nav_on";
        
        var mainTarget = "/main.aspx", leftTarget = "/public/left.aspx";
        
		switch (currobj.id){
			case "dashboard" : 
			    mainTarget = "/main.aspx"; 
			    leftTarget = "/public/left.aspx?channel=0";
			    break;
			case "purchase":
			    mainTarget = "/supplier/Supplier_Purchase_list.aspx?purchase_type=0";
			    leftTarget = "/public/left.aspx?channel=10";
			    break;
			case "orders" : 
			    mainTarget = "/orders/orders_list.aspx"; 
			    leftTarget = "/public/left.aspx?channel=1";
			    break;
			case "product" : 
			    mainTarget = "/product/Product.aspx";
			    leftTarget = "/public/left.aspx?channel=2";
			    break;
			case "member" : 
			    mainTarget = "/member/member_list.aspx"; 
			    leftTarget = "/public/left.aspx?channel=3";
			    break;
			case "supplier":
			    mainTarget = "/supplier/supplier_list.aspx";
			    leftTarget = "/public/left.aspx?channel=9";
			    break;
			case "marketing" : 
			    mainTarget = "/ad/ad.aspx"; 
			    leftTarget = "/public/left.aspx?channel=4";
			    break;
			case "statistical" : 
			    mainTarget = "/statistical/salesvolume.aspx";
			    leftTarget = "/public/left.aspx?channel=5";
			    break;
			case "content" : 
			    mainTarget = "/about/about_list.aspx";
			    leftTarget = "/public/left.aspx?channel=6";
			    break;
			case "system" : 
			    mainTarget = "/system.aspx"; 
			    leftTarget = "/public/left.aspx?channel=8"; 
			    break;
			case "scm" : 
			    mainTarget = "/scm/stockalert.aspx"; 
			    leftTarget = "/public/left.aspx?channel=7";
			    break;
			default :break;  
		}
		
	    window.parent.main.location.href = mainTarget; 
        window.parent.left.location.href = leftTarget;
    }
</script>
</body>
</html>
