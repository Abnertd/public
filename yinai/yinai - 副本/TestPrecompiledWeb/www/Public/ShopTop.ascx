<%@ Control Language="C#" ClassName="ShopTop" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<%
    ITools tools = ToolsFactory.CreateTools();
    Product product = new Product();
    Supplier supplier = new Supplier();
    Member member = new Member();
    SupplierShopInfo shopInfo = null;
    Cart cart = new Cart();
    string shopName = "", shopUrl = "", shopBanner = "";
    int product_id = tools.CheckInt(Request["product_id"]);

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

    ProductInfo productinfo = product.GetProductByID(product_id);
    if (productinfo != null)
    {
        if (productinfo.Product_IsAudit == 1 && productinfo.Product_IsInsale == 1)
        {
            shopInfo = supplier.GetSupplierShopBySupplierID(productinfo.Product_SupplierID);
            if (shopInfo != null)
            {
                shopName = shopInfo.Shop_Name;
                shopUrl = supplier.GetShopURL(shopInfo.Shop_Domain);
                
                if (shopInfo.Shop_Banner_Img != null && shopInfo.Shop_Banner_Img.Length > 0)
                    shopBanner = Application["Upload_Server_URL"] + shopInfo.Shop_Banner_Img;
                
            }
        }
    }
%>


<!--头部 开始-->
<div class="sz_head" id="h_top">
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
                        <dd>
                            <p><a href="/supplier/order_list.aspx">我的订单</a></p>
                            <p><a href="/supplier/supplier_sysmessage.aspx">站内信息</a></p>
                        </dd>
                    </dl>
                    <%} else{%>
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
                        <a href="/member/member_favorites.aspx" target="_blank" class="a01">收藏夹</a>
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
                            <img src="/images/手机端.png" /><p>扫描二维码<br />
                                下载手机端</p>
                        </div>
                    </li>
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
                        Response.Write("您好，" + Session["supplier_nickname"] + "，欢迎光临" + Application["Site_Name"] + "！<span><a href=\"/supplier/login_do.aspx?action=logout\">退出</a></span>");
                    }
                    else if (tools.NullStr(Session["member_logined"]) == "True")
                    {
                        Response.Write("您好，" + Session["member_nickname"] + "，欢迎光临" + Application["Site_Name"] + "！ <span><a href=\"/member/login_do.aspx?action=logout\">退出</a></span>");
                    }
                    else
                    {
                        Response.Write("您好，欢迎光临" + Application["Site_Name"] + "！ <span><a href=\"/register.htm\">免费注册</a> | <a href=\"/login.htm\">立即登录</a> | &nbsp;&nbsp;客服电话：<strong>400-882-6621</strong></span>");
                    }
                %>

            </b>
        </div>
    </div>
    <!--顶部 结束-->
    <div class="head" style="padding: 15px 0;">
        <div class="logo" style="width: 195px;"><a href="/">
            <img src="/images/logo03.jpg" /></a></div>
        <dl class="dst03">
            <dt><a href="<%=shopUrl %>" target="_blank"><%=shopName %></a></dt>
            <dd><span>品牌直销</span><a href="javascript:;" onclick="NTKF.im_openInPageChat('sz_<%=productinfo.Product_SupplierID+1000 %>_9999','<%=productinfo.Product_ID %>');"><img style="width:74px;" src="/images/icon04.png" /></a></dd>
        </dl>
        <dl class="dst04">
            <dt>入驻第<strong><%=new Public_Class().DateDiffYear(shopInfo.Shop_Addtime,DateTime.Now) %></strong>年</dt>
            <dd>信用评价：<img src="/images/x.jpg" /><img src="/images/x.jpg" /><img src="/images/x.jpg" /><img src="/images/x.jpg" /><img src="/images/x.jpg" /></dd>
        </dl>
        <div class="search" style="float: right;">
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
                Response.Write("		<input name=\"keyword\" id=\"top_keyword\" type=\"text\"  value=\"" + keyword + "\" onblur=\"if (this.value=='') this.value=this.defaultValue\" onfocus=\"if (this.value==this.defaultValue) this.value=''\" autocomplete=\"off\" />");
                Response.Write("	</div>");

                Response.Write("<div class=\"sea_info02\">");
                Response.Write("	<span id=\"searchtype\">" + searchtype + "</span>");
                Response.Write("	<div class=\"span_box\">");
                Response.Write("		<p><a  href=\"javascript:void(0);\" onclick=\"search_type_click(1);\">商品</a></p>");
                Response.Write("		<p><a  href=\"javascript:void(0);\" onclick=\"search_type_click(2);\">店铺</a></p>");
                Response.Write("	</div>");
                Response.Write("</div>");

                Response.Write("	<div class=\"sea_info03\"><a href=\"javascript:void(0)\" onclick=\"$('#frm_top_search').submit()\">搜 索</a></div>");
                Response.Write("</form>");
            %>
        </div>

        <div class="clear"></div>
    </div>

    <% if (shopBanner.Length>0) {%>
    <div class="ad03" style="background-image: url(<%=shopBanner%>); background-repeat: no-repeat; background-position: top center; height: 117px; width: 100%; margin: 0;">
        <div class="ad03_main" style="background-image: url(<%=shopBanner%>); background-repeat: no-repeat; background-position: top center; height: 117px; width: 1200px; margin: 0 auto;"></div>
    </div>
    <%} %>

    <div class="nav02">
        <div class="nav_main02">
            <div class="nav_left02">
                <span>行业市场</span>
                <div class="menu menu02">
                    <div class="dropList" id="0">
                        <%=product.Home_Left_Cate() %>
                    </div>
                </div>
            </div>
            <div class="nav_right02">
                <ul>
                    <li <%=Session["SubPosition"]=="TradeIndex"?"class=\"on\"":"" %>><a href="/TradeIndex.htm">首页</a></li>
                    <li <%=Session["SubPosition"]=="Suppliers"?"class=\"on\"":"" %>><a href="/suppliers.htm">供货商</a></li>
                    <li <%=Session["SubPosition"]=="NewProduct"?"class=\"on\"":"" %>><a href="/product/new_product.htm">新品上线</a></li>
                    <li <%=Session["SubPosition"]=="Brand"?"class=\"on\"":"" %>><a href="/purchase/brand_joined.htm">品牌加盟</a></li>
                    <li <%=Session["SubPosition"]=="Xindai"?"class=\"on\"":"" %>><a href="javascript:;" >信贷中心</a></li>
                    <li <%=Session["SubPosition"]=="Purchase"?"class=\"on\"":"" %>><a href="/purchase/purchase_index.htm">大宗采购</a></li>
                    <li <%=Session["SubPosition"]=="jewelrys"?"class=\"on\"":"" %>><a href="/jewelrys.htm">珠宝精品库</a></li>
                </ul>
                <div class="clear"></div>
            </div>
            <div class="clear"></div>
        </div>
    </div>
</div>
<script>
    function search_type_click(type) {
        var currtext = "";

        var frmURL = "";
        if (type == 1) {
            $("#searchtype").text("商品");
            frmURL = "/product/search.htm";
        }
        else if (type == 2) {
            $("#searchtype").text("店铺");
            frmURL = "/shop/search.htm";
        }
        MM_findObj("frm_top_search").action = frmURL;
    };


</script>
<!--头部 结束-->
