<%@ Control Language="C#" ClassName="Top" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%
    ITools tools = ToolsFactory.CreateTools();
    Cart cart = new Cart();
    Shop shop = new Shop();
    shop.Shop_Initial();
%>

<!--弹出菜单 start-->
<script type="text/javascript">

    function keywordfocus() {
        var top_keyword = $("#top_keyword").val();
        if (top_keyword == "输入关键词进行搜索") {
            $("#top_keyword").val('');
        }
    }

    function keywordblur() {
        var top_keyword = $("#top_keyword").val();
        if (top_keyword.length == 0) {
            $("#top_keyword").val('输入关键词进行搜索');
        }
    }
    function checksearch() {
        var top_keyword = $("#top_keyword").val();
        if (top_keyword.length == 0 || top_keyword == "输入关键词进行搜索") {
            alert('输入关键词进行搜索');
            return false;
        }
    }

    $(function () {
        $(".span_box p").click(function (e) {
            var searchtype = $(this).text();
            $("#searchtype").html(searchtype);
            //tools.NullStr(Application["Site_URL"]).TrimEnd('/')

            //var actionURL = "http://www.yinaifin.com/product/search.aspx";
            var actionURL = "http://www.yinaifin.com/product/product_search.aspx";
            if (searchtype == "店铺") {
                actionURL = "http://www.yinaifin.com/shop/search.aspx"
            }
            $("#frm_top_search").attr("action", actionURL);
        });
    });

</script>
<!--弹出菜单 end-->

<!--头部 开始-->
<div class="sz_head">
    <!--顶部 开始-->
    <div class="top" id="h_top">
        <div class="top_main">
            <div class="top_right">
                <ul>
                    <li style="width: 90px; padding: 0;">
                        <dl class="zkn_dst1">
                            <dt><a href="<%=tools.NullStr(Application["Site_URL"]).TrimEnd('/') %>/member/">我的易耐</a></dt>
                            <dd>
                                <p><a href="<%=tools.NullStr(Application["Site_URL"]).TrimEnd('/') %>/member/order_list.aspx">我的订单</a></p>
                             <%--   <p><a href="<%=tools.NullStr(Application["Site_URL"]).TrimEnd('/') %>/member/member_favorites.aspx">我的收藏</a></p>--%>
                                <p><a href="<%=tools.NullStr(Application["Site_URL"]).TrimEnd('/') %>/member/feedback.aspx">我的咨询</a></p>                               
                                <p><a href="<%=tools.NullStr(Application["Site_URL"]).TrimEnd('/') %>/member/Message_List.aspx">站内信息</a></p>
                            </dd>
                        </dl>
                    </li>
                    <li class="li01">
                        <a href="<%=Application["Site_URL"] %>/member/member_favorites.aspx" class="a01">收藏夹</a>
                        <% new Member().Member_Top_Favoriets(); %>
                    </li>




                    <%if (tools.NullStr(Session["member_logined"]) == "True")
                      {%>
                    <li><a href="<%=tools.NullStr(Application["Site_URL"]).TrimEnd('/') %>/login.aspx">我是买家</a></li>
                    <%}
                      else if (tools.NullStr(Session["supplier_logined"]) == "True")
                      {%>
                    <li><a href="<%=tools.NullStr(Application["Site_URL"]).TrimEnd('/') %>/login.aspx?u_type=1">我是卖家</a></li>
                    <%}
                      else
                      { %>
                    <li><a href="<%=tools.NullStr(Application["Site_URL"]).TrimEnd('/') %>/login.aspx">我是买家</a></li>
                    <li><a href="<%=tools.NullStr(Application["Site_URL"]).TrimEnd('/') %>/login.aspx?u_type=1">我是卖家</a></li>
                    <%} %>


                    <li class="li02">
                        <a href="javascript:;">手机版<img src="/images/icon01.png" /></a>
                        <div class="li02_box">
                            <img src="/images/网站.png" /><p>
                                扫描二维码<br />
                                下载手机端
                            </p>
                        </div>
                    </li>
                    <li style="background-image: none; padding: 0;">客户电话：<strong>400-882-6621</strong></li>
                </ul>
                <div class="clear"></div>
            </div>
            <b>
                <%
                    if (tools.NullStr(Session["supplier_logined"]) == "True")
                    {
                        Response.Write("您好，" + Session["member_nicknames"] + "， 欢迎光临" + Application["Site_Name"] + "！  <a href=\"" + tools.NullStr(Application["Site_URL"]).TrimEnd('/') + "/supplier/login_do.aspx?action=logout\">退出</a>");
                       
                    }
                    else if (tools.NullStr(Session["member_logined"]) == "True")
                    {
                        Response.Write("您好，" + Session["member_nicknames"] + " ，欢迎光临" + Application["Site_Name"] + "！ <a href=\"" + tools.NullStr(Application["Site_URL"]).TrimEnd('/') + "/member/login_do.aspx?action=logout\">退出</a>");
                    }
                    else
                    {
                        Response.Write("您好，欢迎光临" + Application["Site_Name"] + "！ <a href=\"" + tools.NullStr(Application["Site_URL"]).TrimEnd('/') + "/register.aspx\">[免费注册]</a> <a href=\"" + tools.NullStr(Application["Site_URL"]).TrimEnd('/') + "/login.aspx\">[立即登录]</a>");
                    }


                

                  
                %>
            </b>
        </div>
    </div>
    <!--顶部 结束-->
    <div class="head" style="padding: 15px 0;">
        <div class="logo" style="width: 195px;">
            <a href="<%=Application["Site_URL"] %>">
                <img src="images/new_logo.jpg"></a>
        </div>
        <dl class="dst03">
            <dt><a href="/" title="<%=shop.Shop_Name %>"><%=shop.Shop_Name %></a></dt>
            <dd><span>品牌直销</span><%--<a href="javascript:void();" onclick="NTKF.im_openInPageChat('sz_<%=tools.NullInt(Session["shop_supplier_id"])+1000 %>_9999');"><img style="width:74px;" src="images/icon04.png"></a>--%></dd>
        </dl>
        <dl class="dst04">
            <dt>入驻第<strong><%=shop.SettledYear(Convert.ToInt32(Session["shop_supplier_id"])) %></strong>年</dt>
            <%--<dt>入驻第<strong><%=  new Public_Class().DateDiffYear(Convert.ToDateTime( Session["shop_supplier_id"]), DateTime.Now); %></strong>年</dt>--%>
            <dd>信用评价：<%--<img src="images/x.jpg"><img src="images/x.jpg"><img src="images/x.jpg"><img src="images/x.jpg"><img src="images/x.jpg">--%>
           <%  shop.shop_evaluate_info_AVG(); %>      
            </dd>
        </dl>
        <div class="search" style="float: right;">

            <%
                string actionURL = tools.NullStr(Application["Site_URL"]).TrimEnd('/') + "/product/product_search.aspx";
              
                string searchtype = "商品";
                if (Request.Path == "/shop/search.aspx")
                {
                    actionURL = tools.NullStr(Application["Site_URL"]).TrimEnd('/') + "/shop/search.aspx";
                    searchtype = "店铺";
                }

                Response.Write("<form name=\"frm_search\" id=\"frm_top_search\" action=\"" + actionURL + "\" method=\"get\" onsubmit=\"return checksearch('#top_keyword');\">");
                Response.Write("	<div class=\"sea_info01\">");
                Response.Write("		<input name=\"keyword\" id=\"top_keyword\" type=\"text\"  value=\"输入关键词进行搜索\" onblur=\"keywordblur()\" onfocus=\"keywordfocus()\"  />");
                Response.Write("	</div>");

                //Response.Write("<div class=\"sea_info02\">");
                //Response.Write("	<span id=\"searchtype\">" + searchtype + "</span>");
                //Response.Write("	<div class=\"span_box\">");
                //Response.Write("		<p><a>商品</a></p>");
                //Response.Write("		<p><a>店铺</a></p>");
                //Response.Write("	</div>");
                //Response.Write("</div>");

                Response.Write("	<div class=\"sea_info03\"><a href=\"javascript:void(0)\" onclick=\"$('#frm_top_search').submit()\">搜 索</a></div>");
                Response.Write("</form>");
            %>
        </div>

        <div class="clear"></div>
    </div>

    <%
        if (shop.Shop_Banner_IsActive == 1)
        {
            Response.Write(shop.Shop_banner);
        }

        string Pages_Nav = shop.Get_Supplier_Pages_Nav();

        string currentOnMenu = Convert.ToString(Session["shop_page"]);
        switch (currentOnMenu)
        {
            case "index":
            case "evaluate":
            case "ask":
            case "article":
            case "product":
            case "Consultation":
                break;
            default:
                if (!Regex.IsMatch(Pages_Nav, "class=\"on\""))
                {
                    currentOnMenu = "index";
                }
                break;
        }
        if (shop.Shop_TopNav_IsActive == 1)
        {
        Response.Write("<div class=\"nav03\">");
        Response.Write("	<div class=\"nav03_main\">");
        Response.Write("		  <ul>");
        Response.Write("			  <li" + (currentOnMenu.Equals("index") ? " class=\"on\"" : "") + "><a href=\"/\">店铺首页</a></li>");
        Response.Write("			  <li" + (currentOnMenu.Equals("product") ? " class=\"on\"" : "") + "><a href=\"/category.aspx\">全部商品</a></li>");
        Response.Write("			  <li" + (currentOnMenu.Equals("ask") ? " class=\"on\"" : "") + "><a href=\"/Shop_Contact_info.aspx\">联系我们</a></li>");  
        Response.Write("			  <li" + (currentOnMenu.Equals("article") ? " class=\"on\"" : "") + "><a href=\"/shop_article_list.aspx\">店铺公告</a></li>");
        Response.Write("			  <li" + (currentOnMenu.Equals("Consultation") ? " class=\"on\"" : "") + "><a href=\"/shop_ask.aspx\">留言反馈</a></li>");
        Response.Write(Pages_Nav);
        Response.Write("		  </ul>");
        Response.Write("		  <div class=\"clear\"></div>");
        Response.Write("	</div>");
        Response.Write("</div>");
        Session["shop_page"] = null;
        }
    %>
</div>
<!--头部 结束-->
