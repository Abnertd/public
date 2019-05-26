<%@ Page Language="C#" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Register Src="~/Public/Top.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>
<%
    
     
    ITools tools = ToolsFactory.CreateTools();
    AD ad = new AD();
    CMS cms = new CMS();
    Public_Class pub = new Public_Class();
    Product product = new Product();
    string productviewcookie = tools.CheckStr(Request["productviewcookie"]);
    if (productviewcookie == "clear")
    {
        Request.Cookies["product_viewhistory_zh-cn"].Value = null;
    }
    string cate_name = "";
    int cate_id;
    int id = 0;
    int cate_parentid = 0;
    int cate_typeid = 0;
    string filter_value;
    string SET_Title = pub.SEO_TITLE();
    string Keywords = tools.NullStr(Application["Site_Keyword"]);
    string Description = tools.NullStr(Application["Site_Description"]);
    cate_id = tools.CheckInt(Request["cate_id"]);
    filter_value = product.product_filter_value();
    int parentid = 0;
    CategoryInfo category = product.GetCategoryByID(cate_id);

    int cate_top_id = product.Get_TopCate(cate_id);
    switch (cate_top_id)
    {
        case 230:
            Session["Position"] = "cate";
            break;
        case 231:
            Session["Position"] = "cate2";
            break;
        case 232:
            Session["Position"] = "cate3";
            break;
        case 233:
            Session["Position"] = "cate4";
            break;
        default:
            Session["Position"] = "cate";
            break;
    }


    if (category != null)
    {
        if (category.Cate_IsActive == 1)
        {
            cate_name = category.Cate_Name;
            if (category.Cate_SEO_Title == "")
            {
                SET_Title = category.Cate_Name + " - " + pub.SEO_TITLE();
            }
            else
            {
                SET_Title = category.Cate_SEO_Title;
                Keywords = category.Cate_SEO_Keyword;
                Description = category.Cate_SEO_Description;
            }
            cate_parentid = category.Cate_ParentID;
            cate_typeid = category.Cate_TypeID;
            id = cate_id;
        }
    }

    parentid = product.getCateParentID(cate_id);
    Session["SubPosition"] = "NewProduct";
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>
        <%= "新品上线" + SET_Title%></title>
    <meta name="Keywords" content="<% =Keywords%>" />
    <meta name="Description" content="<%=Description%>" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/layer/layer.js"></script>
    <script type="text/javascript" src="/scripts/common.js"></script>
    <script type="text/javascript">

        function switchTag(content) {

            if (document.getElementById(content).className == "hidecontent") {
                document.getElementById(content).className = "";
            }
            else { document.getElementById(content).className = "hidecontent"; }

        }
    </script>
    <script type="text/javascript">
        function show_hiddendiv() {
            document.getElementById("hidden_div").style.display = 'block';
            document.getElementById("_strHref").href = 'javascript:hidden_showdiv();';
            document.getElementById("_strSpan").innerHTML = "<img src=/images/icon02_2.png>";

            SetCookie("extend_div", "show");
        }
        function hidden_showdiv() {
            document.getElementById("hidden_div").style.display = 'none';
            document.getElementById("_strHref").href = 'javascript:show_hiddendiv();';
            document.getElementById("_strSpan").innerHTML = "<img src=/images/icon02.png>";

            SetCookie("extend_div", "hide");
        }
    </script>
</head>
<body>
    <uctop:top ID="top1" runat="server" />

    <div class="content" style="margin-top: 0;">
        <div class="content_main">
            <!--位置说明 开始-->
            <div class="position"><a href="/">首页</a> > <strong>新品上线</strong></div>
            <!--位置说明 结束-->
            <!--广告 开始-->
            <div class="ad" style="height: 161px; overflow: hidden;"><%=ad.AD_Show("NewProduct_List_AD","","cycle",0) %></div>
            <!--广告 结束-->
            <div class="partb">
                <!--筛选 开始-->
                <%product.NewProduct_Filter(cate_id, cate_parentid, cate_typeid, "/product/new_product.htm"); %>
                <!--筛选 结束-->
                <h3 class="title02">
                    <ul>
                        <li class="on"><a href="javascript:;" target="_blank">默认</a></li>
                        <li><span>排序</span></li>
                    </ul>
                    今前发布新品<strong><%=product.GetNewProductCount(cate_id) %></strong>款
                       <div class="clear"></div>
                </h3>
                <!--列表 开始-->
                <div class="list_02">
                        <%product.NewProduct_List("retail", cate_id, 4); %>
                </div>
                <!--列表 结束-->
            </div>
        </div>
    </div>

  <ucbottom:bottom runat="server" ID="Bottom" />
</body>
</html>
