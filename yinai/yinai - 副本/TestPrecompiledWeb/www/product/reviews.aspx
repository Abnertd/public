<%@ Page Language="C#" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%--<%@ Register Src="~/Public/Top.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>--%>
<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>
<%
    Response.Expires = -1;

    //静态化配置
    PageURL pageurl = new PageURL(int.Parse(Application["Static_IsEnable"].ToString()));

    Product product = new Product();
    ITools tools = ToolsFactory.CreateTools();
    Public_Class pub = new Public_Class();
    if (tools.CheckInt(Application["Product_Review_Config_Power"].ToString()) > 0)
    {
        //会员登录检查
    }

    int cate_id = 0;
    int product_id = 0;
    int validcount = 0;
    double average_grade = 0;
    string product_name = "";

    product_id = tools.CheckInt(Request["product_id"]);
    if (product_id == 0)
    {
        Response.Redirect("/index.aspx");
    }
    ProductInfo productinfo = product.GetProductByID(product_id);
    if (productinfo != null)
    {
        if (productinfo.Product_IsAudit == 1)
        {
            product_name = productinfo.Product_Name;
            cate_id = productinfo.Product_CateID;
            average_grade = productinfo.Product_Review_Average;
            validcount = productinfo.Product_Review_ValidCount;
        }
        else
        {
            Response.Redirect("/index.aspx");
        }
    }
    else
    {
        Response.Redirect("/index.aspx");
    }

    if (tools.CheckInt(Application["Product_Review_Config_Power"].ToString()) == 2)
    {
        //商品购买检查
    }

    int first_cate = product.Get_First_CateID(cate_id);
    product.Set_Cate_Session(first_cate);
    product.Recent_View_Add(product_id);

    string title = productinfo.Product_SEO_Title;
    if (title == "")
    {
        title = productinfo.Product_Name;
    }

    string star = tools.CheckStr(Request.QueryString["star"]);
    if (star == "1,2,3,4,5")
        star = "1";
    else if (star == "4,5")
        star = "2";
    else if (star == "3")
        star = "3";
    else if (star == "1,2")
        star = "4";
    
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>
        <%=title + " - " + pub.SEO_TITLE()%></title>
    <meta name="Keywords" content="<% = productinfo.Product_SEO_Keyword%>" />
    <meta name="Description" content="<%=productinfo.Product_SEO_Description%>" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <%--    <link href="/css/index3.css" rel="stylesheet" type="text/css" />
    <link href="/css/page.css" rel="stylesheet" type="text/css" />--%>
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script type="text/javascript" src="/scripts/common.js"></script>
    <script type="text/javascript" src="/scripts/jquery-extend-AdAdvance2.js"></script>
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
    <%-- <style type="text/css">
        .pagedisplay{ height:28px; overflow:hidden;}
        .pagedisplay table{ float:right; margin-right:2px;}
    </style>
    
    <script type="text/javascript">
        $(function() { $("#reviews_iframe", window.parent.document).attr("height", $("body").height() + 10); })
    </script>--%>
</head>
<body>
    <uctop:top ID="top1" runat="server" />
    <div class="content02" style="background-color: #FFF;">
        <div class="content02_main" style="background-color: #FFF;">
            <!--位置说明 开始-->
            <div class="position"><a href="/">首页</a> <%=product.Get_Cate_Nav(cate_id,"&nbsp;>&nbsp;")%> > <strong><%=productinfo.Product_Name%></strong></div>
            <!--位置说明 结束-->
            <div class="partl">
                <div class="pl_left">

                    <!--销量排行 开始-->
                    <div class="blk28">
                        <h2>本类推荐</h2>
                        <div class="b28_main02">
                            <%--<%=product.Product_LeftSale_Product(10, cate_id) %>--%>
                            <ul>
                                <%product.Similar_Product(cate_id);%>
                            </ul>
                        </div>
                    </div>
                    <!--销量排行 结束-->
                    <!--浏览记录 开始-->
               <%--     <div class="blk28" style="margin-top: 15px;">
                        <h2>浏览记录</h2>
                        <div class="b28_main02" style="padding: 0 10px;">
                            <%=product.Product_Left_LastView_Product(4)%>
                        </div>
                    </div>--%>
                     <div class="blk09" style="height: auto">
                    <h2>浏览记录<a href="javascript:void(0);" onclick="clear_product_view_history()"></a></h2>
                    <div class="b09_main" id="div_product_view_history">
                        <ul>
                            <%=product.Member_Index_Right_LastView_Product() %>
                        </ul>
                    </div>
                </div>
                    <!--浏览记录 结束-->
                </div>
                <div class="pl_right">
                    <div class="blk30" style="border: 1px solid #cccccc; margin-top: 0px;">
                        <h2>全部评论</h2>
                        <div class="b33_main03">
                            <div class="b33_m03_info04">
                                <% product.Product_Review_List(productinfo.Product_ID, tools.CheckStr(Request.QueryString["star"])); %>
                            </div>
                        </div>
                    </div>


                </div>
                <div class="clear">
                </div>
            </div>
        </div>
    </div>

  <ucbottom:bottom runat="server" ID="Bottom" />

</body>
</html>
