<%@ Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%--<%@ Register Src="~/Public/MEM_Top.ascx" TagPrefix="uctop" TagName="top" %>--%>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>



<% 
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    Member member = new Member();
    Session["Cur_Position"] = Session["Home_Position"] = "";
    member.Member_Login_Check("/member/loan_project.aspx");
    string title = "";
    title = "我的信贷申请";
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%=title + " - 我是买家 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="/css/index3.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/scripts/jquery.js"></script>
      <script type="text/javascript" src="/scripts/common.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script src="/scripts/jquery-extend-AdAdvance3.js" type="text/javascript"></script>
    <link type="text/css" href="/Scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script>
    <script type="text/javascript">

        function LoanProjectSearch(object)
        {
            var keyword = $("#" + object).val();
            var loanStatus = $("#loanStatus").val();

            if (keyword == "订单号/申请单号") {
                alert('请输入查询关键词！');
            } else {
                window.location.href = "/member/loan_project.aspx?keyword=" + keyword + "&loanStatus=" + loanStatus;
            }
        }

        function changeLoanStatus()
        {
            var oState = $("#loanStatus").val();
            window.location = "/member/loan_project.aspx?loanStatus=" + oState;
        }

    </script>

</head>
<body>
 <%--   <uctop:top ID="top1" runat="server" />--%>

    <!--主体 开始-->
    <div class="content02">
        <div class="content02_main">
            <!--位置说明 开始-->
            <div class="position"><a href="/tradeindex.aspx" >首页</a> > <a href="/member/">采购商用户中心</a> > 信贷管理 > <strong>我的信贷申请</strong></div>
            <!--位置说明 结束-->
            <div class="partc">
                <div class="pc_left">
                    <div class="blk12">
                        <%=member.Member_Left_HTML(2,1) %>
                    </div>
                </div>
                <div class="pc_right">
                    <div class="title03">我的信贷申请</div>

                    <%=member.Member_Loan_Project_Search() %>

                    <div class="blk08">
                        <%member.Member_Loan_Project(15); %>
                    </div>
                </div>
                <div class="clear"></div>
            </div>
        </div>
    </div>
    <!--主体 结束-->

  <ucbottom:bottom runat="server" ID="Bottom" />
</body>
</html>
