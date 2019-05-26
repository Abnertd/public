<%@ Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>
<% 
    
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    Member member = new Member();
    member.Member_Login_Check("/member/member_shop_favorites.aspx");
    
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>
        <%="我的店铺收藏 - 我是买家 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
   <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/scripts/1.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script src="/scripts/jquery-extend-AdAdvance3.js" type="text/javascript"></script>
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
    <style type="text/css">
        .yz_blk19_main img
        {
            display: inline;
        }
    </style>
</head>
<body>
    <uctop:top ID="top1" runat="server" />
    <div class="content02">
        <div class="content02_main">
            <!--位置说明 开始-->
            <div class="webwrap">
            <div class="position">当前位置 > <a href="/tradeindex.aspx">首页</a> > <a href="/member/">采购商用户中心</a> > 交易管理 > <strong>我的店铺收藏</strong></div>
            <div class="clear">
            </div>
            <!--位置说明 结束-->
           
                 <div class="partd_1" >
                <div class="pd_left">                
                        <%=member.Member_Left_HTML(1, 6) %> 
                </div>
                <div class="pd_right">
                    <div class="blk14_1">
                    <h2>我的店铺收藏</h2>
                    <div class="blk07_sz">
                        <div class="b07_main">
                            <div class="b07_info04">
                                <%member.Member_Shop_Favorites("list", 5);%>
                            </div>
                        </div>
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
