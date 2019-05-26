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
    member.Member_Login_Check("/member/account_invoice.aspx");
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="发票管理 - 我是买家 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/scripts/jquery.js"></script>
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
        .main table td {
            padding: 5px;
        }

        .step_num_off {
            font-family: "Verdana", "Arial", "Helvetica", "sans-serif";
            font-size: 24px;
            font-style: italic;
            font-weight: bold;
            color: #B9B9B9;
            line-height: 150%;
        }

        .buttonupload {
            background: url("/images/buttonSkinAL.gif") repeat-x scroll center bottom #FEEEB1;
            border: 1px solid #F39D24;
            color: #5E2708;
            cursor: pointer;
            font-size: 12px;
            font-weight: normal;
            height: 20px;
            padding: 0 2px;
        }
    </style>
</head>
<body>
    <uctop:top ID="top1" runat="server" />
    <!--主体 开始-->
    <div class="webwrap">
    <div class="content02" style="margin-bottom:20px;">
       
            <!--位置说明 开始-->
            <div class="position">当前位置 > <a href="/index.aspx">首页</a> > <a href="/member/">我是买家</a> > 辅助功能 > <strong>发票管理</strong></div>
            <!--位置说明 结束-->
            <div class="partd_1">
                <div class="pd_left">               
                        <%=member.Member_Left_HTML(4, 6) %>                  
                </div>
                <div class="pd_right">
                    <div class="blk14_1" style="margin-top:0px;">
                   <h2>发票管理</h2>
                    <div class="b14_1_main">
                        <%member.Member_Invoice(); %>
                    </div></div>
                </div>
            </div>
            <div class="clear">
            </div>
      
    </div>
        </div>
    <!--主体 结束-->
  <ucbottom:bottom runat="server" ID="Bottom" />
</body>
</html>
