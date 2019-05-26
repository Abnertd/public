<%@ Page Language="C#" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%--<%@ Register Src="~/Public/MEM_Top.ascx" TagPrefix="uctop" TagName="top" %>--%>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>
<%
    
    ITools tools = ToolsFactory.CreateTools();
    AD ad = new AD();
    Public_Class pub = new Public_Class();
    Product product = new Product();

    string SET_Title = "所有商品分类";
    string Keywords = tools.NullStr(Application["Site_Keyword"]);
    string Description = tools.NullStr(Application["Site_Description"]);

    string keyword = tools.CheckStr(Request["keyword"]);
    if (keyword == null || keyword.Length == 0)
    {
        pub.Msg("info", "信息提示", "请输入要查询的关键词", false, "{back}");
    }
    else
    {
        product.AddKeywordRanking(1,keyword);
    }
   
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%=SET_Title%></title>
    <meta name="Keywords" content="<% =Keywords%>" />
    <meta name="Description" content="<%=Description%>" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/shop.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script type="text/javascript" src="/scripts/common.js"></script>

<script  type="text/javascript">

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
<%--    <uctop:top ID="top1" runat="server" />--%>

    <!--主体 开始-->
    <div class="content02" style="background-color:#FFF;">
        <div class="content02_main" style="background-color:#FFF;">
            <!--位置说明 开始-->
            <div class="position"><a href="/">首页</a> > <strong>所有类目</strong></div>
            <!--位置说明 结束-->

            <div class="parto">
                <%
                    new Shop().SearchShops();
                %>
            </div>
        </div>
    </div>
    <!--主体 结束-->

    

  <ucbottom:bottom runat="server" ID="Bottom" />
</body>
</html>
