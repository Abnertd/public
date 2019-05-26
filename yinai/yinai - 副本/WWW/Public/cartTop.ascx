<%@ Control Language="C#" ClassName="cartTop" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%

    ITools tools = ToolsFactory.CreateTools();
    Product product = new Product();

    AD ad = new AD();
    Cart cart = new Cart();
    Member member = new Member();
    string Member_Email = "";
    MemberInfo memberinfo = member.GetMemberByID();
    if (memberinfo != null)
    {
        Member_Email = memberinfo.Member_Email;
    }


    Supplier supplier = new Supplier();
    SupplierInfo supplierinfo = supplier.GetSupplierByID();
    string Shop_Domain = "";
    Glaer.Trade.B2C.BLL.MEM.ISupplierShop MyShop = Glaer.Trade.B2C.BLL.MEM.SupplierShopFactory.CreateSupplierShop();
    Public_Class pub = new Public_Class();
    if (supplierinfo != null)
    {
        SupplierShopInfo shopinfos = MyShop.GetSupplierShopBySupplierID(supplierinfo.Supplier_ID);
        if (shopinfos != null)
        {

            Shop_Domain = pub.GetShopDomain(shopinfos.Shop_Domain);
        }
    }
     %>

<!--顶部 开始-->
<script type="text/javascript">


    //  加入收藏 <a onclick="AddFavorite(window.location,document.title)">加入收藏</a>   
    //function addToFavorite(d, c) {

    //    if (document.all) {
    //        window.external.AddFavorite(d, c);
    //    } else {
    //        if (window.sidebar) {
    //            window.sidebar.addPanel(c, d, "");
    //        } else {
    //            alert("对不起，您的浏览器不支持此操作!\n请您使用菜单栏或Ctrl+D收藏本站。");
    //        }
    //    }
    //}


    //设为首页 
    //function setHome(url) {

    //    if (document.all) {
    //        document.body.style.behavior = 'url(#default#homepage)';

    //        document.body.setHomePage(url);

    //    } else if (window.sidebar) {

    //        if (window.netscape) {

    //            try {

    //                netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect");

    //            } catch (e) {

    //                alert("该操作被浏览器拒绝，如果想启用该功能，请在地址栏内输入 about:config,然后将项 signed.applets.codebase_principal_support 值该为true");

    //            }

    //        }

    //        if (window.confirm("你确定要设置" + url + "为首页吗？") == 1) {

    //            var prefs = Components.classes['@mozilla.org/preferences-service;1'].getService(Components.interfaces.nsIPrefBranch);

    //            prefs.setCharPref('browser.startup.homepage', url);

    //        }

    //    }

    //}
</script>
<div class="top">
            <div class="top_main" style="width:1000px;">
                  <div class="top_main_right">
                   
                <ul>
                    <li class="li01">
                        <%int cart_amount = cart.My_Cart_Count(); %>
                        <a href="/cart/my_cart.aspx">购物清单 <span><%=cart_amount %></span></a>
                        <div class="li_box">                           
                            <% cart.My_Cart_ProductInfo();%>   
                        </div>
                    </li>
                    <li class="li03">
                        <a href="/member/index.aspx">我是买家</a>
                      <%--  <div class="li_box02">
                            <p><a href="/member/order_list.aspx" target="_blank">我的订单</a></p>
                            <p><a href="/member/member_favorites.aspx" target="_blank">商品收藏</a></p>
                            <p><a href="/member/message_list.aspx" target="_blank">消息通知</a></p>
                        </div>--%>
                    </li>

                    <li class="li03">
                        <a href="/supplier/index.aspx">我是卖家</a>
                    </li>
                    <li class="li03">
                        <a href="/about/index.aspx?sign=aboutus" target="_blank">关于我们</a>
                    </li>
                  <%--  <li class="li03">
                        <a href="javascript:void(0);" onclick="setHome('<%= Application["Site_URL"]%>');">设为首页</a>
                    </li>--%>
                   <%-- <li class="li03">

                        <a href="javascript:void(0);" onclick="addToFavorite('<%= Application["Site_URL"]%>','<%=Application["Site_Name"] %>');">加入收藏</a>
                    </li>--%>
                    <li class="li04">
                        <a href="javascript:;">手机端</a>
                        <div class="li_box04">
                            <img src="/images/网站.png">
                            <p>
                                下载易耐网平台<br>
                                手机版
                            </p>
                        </div>
                    </li>
                </ul>
                <div class="clear"></div>
            </div>
            <%
                //if (tools.NullStr(Session["supplier_logined"]) == "True")
                //{
                //    Response.Write("<b>您好，" + Session["supplier_nickname"] + "，欢迎光临" + Application["Site_Name"] + "！<span> <a href=\"/supplier/login_do.aspx?action=logout\">退出</a></span></b>");
                //}
                //else if (tools.NullStr(Session["member_logined"]) == "True")
                //{
                //    Response.Write("<b>您好，" + Session["member_nickname"] + "，欢迎光临" + Application["Site_Name"] + "！ <span> <a href=\"/member/login_do.aspx?action=logout\">退出</a></span></b>");
                //}
                //else
                //{
                //    Response.Write("<b>您好，欢迎光临" + Application["Site_Name"] + "！ <span><a href=\"/login.aspx\">立即登录</a> | <a href=\"/register.aspx\" >免费注册</a>&nbsp;&nbsp;客服电话：<strong>400-882-6621</strong></span></b>");
                //}
                if (tools.NullStr(Session["member_logined"]) == "True")
                {

                    //Response.Write(Session["supplier_id"]);
                    Response.Write("<b>您好，" + (Session["member_nickname"].ToString().Length > 0 ? Session["member_nickname"] : Member_Email)
   + "，欢迎光临" + Application["Site_Name"] + "！ <span> <a href=\"/member/login_do.aspx?action=logout\">退出</a></span></b>");
                }
                else if (tools.NullStr(Session["Logistics_Logined"]) == "True")
                {
                    Response.Write("<b>您好，" + Session["Logistics_CompanyName"] + "，欢迎光临" + Application["Site_Name"] + "！ <span> <a href=\"/Logistics/Logistics_do.aspx?action=Logout\">退出</a></span></b>");
                }
                else
                {
                    Response.Write(" <b>您好，欢迎登录易耐网平台！ <a href=\"/login.aspx\" >[请登录]</a> <a href=\"/register.aspx\" >[免费注册]</a> </b>");
                }
            %>
            </div>
      </div>
<!--顶部 结束-->
