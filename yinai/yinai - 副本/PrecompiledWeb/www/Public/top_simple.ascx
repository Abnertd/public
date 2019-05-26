<%@ Control Language="C#" ClassName="top_simple" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<script runat="server">
    //DateTime startdate = DateTime.Now;
    ITools tools = ToolsFactory.CreateTools();
    Public_Class pub = new Public_Class();
    Cart cart = new Cart();
    Glaer.Trade.Util.SQLHelper.SQLHelper DBHelper = new Glaer.Trade.Util.SQLHelper.SQLHelper();
    public bool IsIndex { get; set; }
    string key_word = "";
    string Member_Email = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        key_word = pub.CheckXSS(tools.CheckStr(Request["key_word"]));
        if (key_word == "输入关键词进行搜索")
        {
            key_word = "";
        }
        if (key_word == "")
        {
            key_word = "输入关键词进行搜索";
        }
    }
</script>



<%
    AD ad = new AD();

    ITools tools = ToolsFactory.CreateTools();
    Product product = new Product();
    Member member = new Member();
    Supplier supplier = new Supplier();
    SysMessage messageclass = new SysMessage();
    
    string member_id = tools.CheckStr(Session["member_id"].ToString());
    int MemberMessageNum = messageclass.GetMessageNum(1);//29
    int SupplierMessageNum = messageclass.GetMessageNum(2);
    if (member_id != "0")
    {
        string supplier_id = tools.CheckStr(DBHelper.ExecuteScalar("select Member_SupplierID from Member where Member_ID=" + member_id + "").ToString());

        Session["supplier_id"] = supplier_id;
        string supplierid = Session["supplier_id"].ToString();
        Glaer.Trade.B2C.Model.SupplierInfo supplierinfo = supplier.GetSupplierByID();
        if (supplierinfo != null)
        {
            Session["supplier_auditstatus"] = supplierinfo.Supplier_AuditStatus;
            Session["supplier_ishaveshop"] = supplierinfo.Supplier_IsHaveShop;
        }

    }

    Glaer.Trade.B2C.Model.MemberInfo memberinfo = member.GetMemberByID();
    if (memberinfo != null)
    {
        Member_Email = memberinfo.Member_Email;
    }
    string index_css = "";

    if (IsIndex)
    {
        index_css = "nav_left";
    }
    else
    {
        index_css = "nav_left nav_left02 ";
    }

%>

<script type="text/javascript">

    function checksearch() {
        var top_keyword = $("#top_keyword").val();


        if (top_keyword.length == 0 || top_keyword == "输入关键词进行搜索") {
            alert('输入关键词进行搜索');
            return false;

        }
        else {

            return top_keyword;
        }
    }

    function search_type_click(type) {
        var top_keyword = $("#top_keyword").val();
        var currtext = "";
        var frmURL = "";
        if (type == 1) {
            $("#s_name").text("商品");

            frmURL = "/Product/product_search.aspx?top_keyword=" + top_keyword + "";
        }
        else {
            $("#s_name").text("店铺");
            frmURL = "/supplier/suppliers_seach.aspx?top_keyword=" + top_keyword + "";
        }
        $('#frm_search').attr('action', frmURL);
    };



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


    $(function () {
        $(document).keydown(function (event) {
            if (event.keyCode == 13) {
                $("#subimtBtn").click();
            }
        });
    });



    // 加入收藏夹
    //function SetFavorite() {
    //    var sTitle = document.title;
    //    var sURL = document.location.href;
    //    try {
    //        window.external.addFavorite(sURL, sTitle);
    //    } catch (e) {
    //        try {
    //            window.sidebar.addPanel(sTitle, sURL, "");
    //        } catch (e) {
    //            alert("加入收藏失败,请使用Ctrl+D进行添加.");
    //        }
    //    }
    //}



    //设为首页
    //function SetHome(obj, url) {
    //    try {
    //        obj.style.behavior = 'url(#default#homepage)';
    //        obj.setHomePage(url);
    //    } catch (e) {
    //        if (window.netscape) {
    //            try {
    //                netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect");
    //            } catch (e) {
    //                alert("抱歉，此操作被浏览器拒绝！\n\n请在浏览器地址栏输入“about:config”并回车然后将[signed.applets.codebase_principal_support]设置为'true'");
    //            }
    //        } else {
    //            alert("抱歉，您所使用的浏览器无法完成此操作。\n\n您需要手动将【" + url + "】设置为首页。");
    //        }
    //    }
    //}

</script>





<!--主体 开始-->
<div id="head_box">
    <!--顶部 开始-->
    <div class="top">
        <div class="top_main">
            <div class="top_main_right">
                <ul>
                    <%if (tools.NullStr(Session["Logistics_Logined"]) == "False")
                      { %>                  
                    <li class="li01">

                        <%int cart_amount = cart.My_Cart_Count(); %>
                        <a href="/cart/my_cart.aspx">采购清单 <span><%=cart_amount %></span></a>
                        <div class="li_box">
                            <% cart.My_Cart_ProductInfo();%>
                        </div>
                    </li>
                    <li class="li01" style="background-image:none;">

                      
                        <a href="javascript:void(0);">消息通知<span>&nbsp;<%= MemberMessageNum+SupplierMessageNum%>&nbsp;</span>条</a>
                        <div class="li_box" style="width:125px;height:70px;">
                            <div class="li_box_main">
                               



                                                                 <a href="/member/message_list.aspx?action=list" style="background-image:none;padding-left:24px;width:128px;">买家消息<span>&nbsp;<%= MemberMessageNum%>&nbsp;</span>条</a>
                                 <a href="/supplier/supplier_sysmessage.aspx" style="background-image:none;padding-left:24px;width:128px;">卖家消息<span>&nbsp;<%=SupplierMessageNum %>&nbsp;</span>条</a>
                               
                            </div>
                           
                        </div>
                    </li>
                
                    <li class="li03">
                        <a href="/member/index.aspx">我是买家</a>
                    </li>

                    <li class="li03">
                        <a href="/supplier/index.aspx">我是卖家</a>
                    </li>
                    <%} %>
                    <li class="li03">
                        <a href="/about/index.aspx?sign=aboutus" target="_blank">关于我们</a>
                    </li>                

                    <li class="li04">
                        <a href="javascript:;">手机端</a>
                        <div class="li_box04">
                            <img src="/images/yinaishangcheng.png">
                            <p>
                                下载易耐网平台<br>
                                手机版
                            </p>
                        </div>
                    </li>
                    <li class="li05">

                        <a target="_blank" href="/International/index.aspx">国际站</a>
                    </li>
                </ul>
                <div class="clear"></div>
            </div>
            <%             
                if ((tools.NullStr(Session["member_logined"]) == "True"))
                {
                    //Response.Write(Session["url_after_login"] + "1");
                    Response.Write("<b>您好，" + (Session["member_nicknames"].ToString().Length > 0 ? Session["member_nicknames"] : Member_Email)
   + "，欢迎光临" + Application["Site_Name"] + "！ <span> <a href=\"/member/login_do.aspx?action=logout\">退出</a></span></b>");
                }
               else if (tools.NullStr(Session["supplier_sublogined"]) == "True")
                {
                    //Response.Write(Session["member_nicknames"].ToString() + "2");
                     Response.Write("<b>您好，" + (Session["member_nicknames"].ToString().Length > 0 ? Session["member_nicknames"] : Member_Email)
   + "，欢迎光临" + Application["Site_Name"] + "！ <span> <a href=\"/member/login_do.aspx?action=logout\">退出</a></span></b>");
                }
                else if ((tools.NullStr(Session["member_logined"]) == "True") && (tools.NullStr(Session["supplier_logined"]) == "True"))
                {
                    //Response.Write(Session["member_nicknames"].ToString() + "3");
                    Response.Write("<b>您好，" + (Session["member_nicknames"].ToString().Length > 0 ? Session["member_nicknames"] : Member_Email)
   + "，欢迎光临" + Application["Site_Name"] + "！ <span> <a href=\"/member/login_do.aspx?action=logout\">退出</a></span></b>");
                }
                else if (tools.NullStr(Session["Logistics_Logined"]) == "True")
                {
                    Response.Write("<b>您好，" + Session["Logistics_CompanyName"] + "，欢迎光临" + Application["Site_Name"] + "！ <span> <a href=\"/Logistics/Logistics_do.aspx?action=Logout\">退出</a></span></b>");
                }
                else
                {
                    //Response.Write(Session["url_after_login"]);          
                    Response.Write(" <b>您好，欢迎登录易耐网平台！ <a href=\"/login.aspx\" >[请登录]</a> <a href=\"/register.aspx\" >[免费注册]</a> </b>");
                }
            %>
        </div>
    </div>
    <!--顶部 结束-->
</div>
<!--头部 开始-->
<div class="head">
    <div class="logo">
        <a href="/index.aspx" target="_blank">
            <img src="/images/logo.jpg"></a>
    </div>
    <div class="search">
        <form name="frm_search" id="frm_search" action="/Product/product_search.aspx" method="get" onsubmit="return checksearch();">
            <div class="search_left">
                <div class="search_info">
                    <span id="s_name">商品</span>
                    <div class="search_box">
                        <p><a href="javascript:void(0);" onclick="search_type_click(1);">商品</a></p>
                        <p><a href="javascript:void(0);" onclick="search_type_click(2);">店铺</a></p>
                    </div>
                </div>
                <input id="top_keyword" name="key_word" value="<%=key_word%>" type="text" onblur="keywordblur()" onfocus="keywordfocus()" />
                <div class="clear"></div>
            </div>
            <div class="search_right"><a href="javascript:void(0);" onclick="$('#frm_search').submit();">搜 索</a></div>
            <div class="clear"></div>
            <b>热搜商品：<%=ad.AD_Show("Top_Search_Keyword", "", "keyword", 0)%></b>
        </form>
    </div>
    <div class="head_right">
        <ul>
            <li>
                <img src="/images/icon04.jpg"></li>
            <li>
                <img src="/images/icon05.jpg"></li>
            <li>
                <img src="/images/icon06.jpg"></li>
        </ul>
        <div class="clear"></div>
    </div>
    <div class="clear"></div>
</div>
<!--头部 结束-->
<!--导航 开始-->
<div class="nav">
    <div class="nav_main">
        <div class="<%=index_css %>">
            <strong>
                <img src="/images/icon07.jpg">全部商品分类</strong>
            <div class="nav_info">
                <div class="testbox">
                    <%=product.Home_Left_Cate() %>
                </div>
                <p style="border-top: 1px solid #fd8a61;"><a href="/product/category.aspx" style="color: #fff;" target="_blank">查看全部分类 ></a></p>
            </div>
        </div>
        <div class="nav_right">
            <ul>
                <li <%=(Convert.ToString(Session["Position"]) == "Home" ? " class=\"on\" " :" ") %>><a href="/Index.aspx" class="a_nav">首 页</a></li>
                <li <%=(Convert.ToString(Session["Position"]) == "Category" ? " class=\"on\" " :" ") %>><a href="/product/category.aspx" class="a_nav">商城选购</a></li>

                <li <%=(Convert.ToString(Session["Position"]) == "Bid" ? " class=\"on\" " :" ") %>><a href="/bid/">招标拍卖</a></li>
                <li <%=(Convert.ToString(Session["Position"]) == "Logistics" ? " class=\"on\" " :" ") %>><a href="/Logistics/">仓储物流</a></li>
                <li <%=(Convert.ToString(Session["Position"]) == "financialservice" ? " class=\"on\" " :" ") %>><a href="/Financial/index.aspx" class="a_nav">金融中心</a><img src="/images/icon10.jpg"></li>


                <li <%=(Convert.ToString(Session["Position"]) == "IndustryInformation" ? " class=\"on\" " :" ") %>><a href="/article/Index.aspx" class="a_nav">行情资讯</a></li>
                <li class="tel">400-8108-802</li>
            </ul>
        </div>
        <div class="clear"></div>
    </div>
</div>
<!--导航 结束-->
<%--<%=(DateTime.Now-startdate).TotalMilliseconds %>--%>
