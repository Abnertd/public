<%@ Control Language="C#" ClassName="public_top" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<%
    ITools tools = ToolsFactory.CreateTools();
    Product product = new Product();
    AD ad = new AD();
    Cart cart = new Cart();
    Member member = new Member();

    string keyword = tools.CheckStr(Request["keyword"]);
    keyword = Server.UrlDecode(keyword);
    if (keyword == "输入关键词进行搜索")
    {
        keyword = "";
    }
    if (keyword == "")
    {
        keyword = "输入关键词进行搜索";
    }
    keyword = keyword.Replace("\"", "&quot;").ToString();

    int search_type_id = tools.CheckInt(Request["search_type_id"]);
%>

<!--弹出菜单 start-->
<script type="text/javascript">

    function keywordfocus() {
        var top_keyword = $("#top_keyword").val();
        if (top_keyword == "输入关键词进行搜索") {
            $("#top_keyword").val('');
        }
    }

    function keywordfocus2() {
        var top_keyword = $("#top_keyword2");

        if (top_keyword.val() == "输入关键词进行搜索") {
            top_keyword.val('');
        }
    }

    function keywordfocus3() {
        var top_keyword = $("#top_keyword3");

        if (top_keyword.val() == "输入关键词进行搜索") {
            top_keyword.val('');
        }
    }

    function keywordblur() {
        var top_keyword = $("#top_keyword").val();
        if (top_keyword.length == 0) {
            $("#top_keyword").val('输入关键词进行搜索');
        }
    }
    function keywordblur2() {
        var top_keyword = $("#top_keyword2").val();
        if (top_keyword.length == 0) {
            $("#top_keyword2").val('输入关键词进行搜索');
        }
    }
    function keywordblur3() {
        var top_keyword = $("#top_keyword3").val();
        if (top_keyword.length == 0) {
            $("#top_keyword3").val('输入关键词进行搜索');
        }
    }
    function checksearch() {
        var top_keyword = $("#top_keyword").val();
        if (top_keyword.length == 0 || top_keyword == "输入关键词进行搜索") {
            alert('输入关键词进行搜索');
            return false;
        }
    }
    function checksearch2() {
        var top_keyword = $("#top_keyword2").val();
        if (top_keyword.length == 0 || top_keyword == "输入关键词进行搜索") {
            alert('输入关键词进行搜索');
            return false;
        }
    }
    function checksearch3() {
        var top_keyword = $("#top_keyword3").val();
        if (top_keyword.length == 0 || top_keyword == "输入关键词进行搜索") {
            alert('输入关键词进行搜索');
            return false;
        }
    }
</script>
<!--弹出菜单 end-->

<!--顶部 开始-->
<div class="top">
    <div class="top_main">
        <div class="top_right">
            <ul>
                <li style="width: 90px; padding: 0;">
                    <%if (tools.NullStr(Session["member_logined"]) == "True")
                      {%>
                    <dl class="zkn_dst1">
                        <dt><a href="/member/">我的易耐</a></dt>
                        <dd>
                            <p><a href="/member/order_list.aspx">我的订单</a></p>
                            <p><a href="/member/member_favorites.aspx">我的收藏</a></p>
                            <p><a href="/member/feedback.aspx">我的咨询</a></p>
                            <p><a href="/member/Message_List.aspx">站内信息</a></p>
                        </dd>
                    </dl>
                    <%}
                      else if (tools.NullStr(Session["supplier_logined"]) == "True")
                      {%>
                    <dl class="zkn_dst1">
                        <dt><a href="/supplier/">我的易耐</a></dt>
                        <%if (tools.NullInt(Session["account_id"]) == 0)
                          { %>
                        <dd>
                            <p><a href="/supplier/order_list.aspx">我的订单</a></p>
                            <p><a href="/supplier/supplier_sysmessage.aspx">站内信息</a></p>
                        </dd>
                        <%} %>
                    </dl>
                    <%}
                      else
                      {%>
                    <dl class="zkn_dst1">
                        <dt><a href="/member/">我的易耐</a></dt>
                        <dd>
                            <p><a href="/member/order_list.aspx">我的订单</a></p>
                            <p><a href="/member/member_favorites.aspx">我的收藏</a></p>
                            <p><a href="/member/feedback.aspx">我的咨询</a></p>
                            <p><a href="/member/Message_List.aspx">站内信息</a></p>
                        </dd>
                    </dl>
                    <%} %>
                </li>
                <li class="li01">
                    <a href="/member/member_favorites.aspx" class="a01">收藏夹</a>
                    <%member.Member_Top_Favoriets(); %>
                </li>

                <%if (tools.NullStr(Session["member_logined"]) == "True")
                  {%>
                <%--<li><a href="/login.htm">采购商入口</a></li>--%>
                <li><a href="/cart/my_cart.aspx" onmouseover="update_top_cartcount();">我的进货单(<span id="cart_count"><%=cart.My_Cart_Count() %></span>)</a></li>
                <%}
                  else if (tools.NullStr(Session["supplier_logined"]) == "True")
                  {%>
                <li><a href="/login.htm?u_type=1">供应商入口</a></li>
                <%}
                  else
                  { %>
                <li><a href="/login.htm">采购商入口</a></li>
                <li><a href="/login.htm?u_type=1">供应商入口</a></li>
                <%} %>

                <li class="li02">
                    <a href="javascript:;">手机版<img src="/images/icon01.png" /></a>
                    <div class="li02_box">
                        <img src="/images/手机端.png" /><p>
                            扫描二维码<br />
                            下载手机端
                        </p>
                    </div>
                </li>
                <%--<li style="background-image: none; padding: 0;">客户电话：<strong>400-882-6621</strong></li>--%>
                <li style="width: 100px; padding: 0;">
                        <dl class="zkn_dst1">
                        <dt><a>客户服务</a></dt>                  
                        <dd>
                            <p onclick="NTKF.im_openInPageChat('sz_1000_9999');"><a href="javascript:void(0);">在线客服</a></p>
                            <p onclick="NTKF.im_openInPageChat('sz_1000_9999');"><a href="javascript:void(0);">售后服务</a></p>
                            <p><a href="/help/index.aspx">帮助中心</a></p>
                            <p>客户电话</p>
                            <p style="white-space:nowrap;padding:0 0 0 0px;">400-882-6621</p>
                        </dd>
                    </dl>
                </li>
            </ul>
            <div class="clear"></div>
        </div>
        <b>
            <%
                if (tools.NullStr(Session["supplier_logined"]) == "True")
                {
                    if ( Session["supplier_subnickname"].ToString()!=null)
                    {
                        Response.Write("您 好，" + Session["supplier_subnickname"] + "，欢迎光临" + Application["Site_Name"] + "！  <a href=\"/supplier/login_do.aspx?action=logout\">退出</a>");
                    }
                    else
                    {
                        Response.Write("您 好，" + Session["supplier_nickname"] + "，欢迎光临" + Application["Site_Name"] + "！  <a href=\"/supplier/login_do.aspx?action=logout\">退出</a>");
                    }
                   
                }
                else if (tools.NullStr(Session["member_logined"]) == "True")
                {
                    Response.Write("您好，" + Session["member_nickname"] + "，欢迎光临" + Application["Site_Name"] + "！ <a href=\"/member/login_do.aspx?action=logout\">退出</a>");
                }
                else
                {
                    Response.Write("您好，欢迎光临" + Application["Site_Name"] + "！ <a href=\"/register.htm\">[免费注册]</a> <a href=\"/login.htm\">[立即登录]</a>");
                }
            %>
        </b>
    </div>
</div>

<script type="text/javascript">

    $(function () {
        $(".span_box p").click(function (e) {
            var searchtype = $(this).text();
            $("#searchtype").html(searchtype);

            var actionURL = "/product/search.htm";
            if (searchtype == "店铺") {
                actionURL = "/shop/search.htm"
            }
            $("#frm_top_search").attr("action", actionURL);
        });
    });
</script>

<!--顶部 结束-->
<div class="head">
    <div class="logo">
        <a href="/index.aspx">
            <img src="/images/logo.jpg" /></a>
    </div>
    <div class="search">

        <%
            string actionURL = "/product/search.htm";
            string searchtype = "商品";
            if (Request.Path == "/shop/search.htm")
            {
                actionURL = "/shop/search.htm";
                searchtype = "店铺";
            }

            Response.Write("<form name=\"frm_search\" id=\"frm_top_search\" action=\"" + actionURL + "\" method=\"get\" onsubmit=\"return checksearch('#top_keyword');\">");
            Response.Write("	<div class=\"sea_info01\">");
            Response.Write("		<input name=\"keyword\" id=\"top_keyword\" type=\"text\"  value=\"" + keyword + "\" onblur=\"keywordblur()\" onfocus=\"keywordfocus()\"  />");
            Response.Write("	</div>");

            Response.Write("<div class=\"sea_info02\">");
            Response.Write("	<span id=\"searchtype\">" + searchtype + "</span>");
            Response.Write("	<div class=\"span_box\">");
            Response.Write("		<p><a>商品</a></p>");
            Response.Write("		<p><a>店铺</a></p>");
            Response.Write("	</div>");
            Response.Write("</div>");

            Response.Write("	<div class=\"sea_info03\"><a href=\"javascript:void(0)\" onclick=\"$('#frm_top_search').submit()\">搜 索</a></div>");
            Response.Write("</form>");
        %>
    </div>
    <div class="head_right">
        <ul>
            <li>
                <img src="/images/icon03.jpg" />数据魔方</li>
            <li>
                <img src="/images/icon02.jpg" />信贷买单</li>
            <li>
                <img src="/images/top_icon01.png" height="36" />诚信保证</li>
        </ul>
        <div class="clear"></div>
    </div>
    <div class="clear"></div>
</div>
