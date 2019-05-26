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
    title = "我的信贷账户";
%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%=title + " - 我是买家 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script type="text/javascript" src="/scripts/common.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script src="/scripts/Echarts/esl.js" type="text/javascript"></script> 
    <script src="/scripts/Echarts/echarts.js" type="text/javascript"></script> 
    <script type="text/javascript">

        require(
           [
               'echarts',
               'echarts/chart/pie' //按需加载图表关于线性图、折线图的部分
           ],
           DrawCharts//异步加载的回调函数绘制图表
       );

        function DrawCharts(ec) {
            DrawLoanAccount(ec);
        }

        function DrawLoanAccount(ec) {
            //--- 饼图 ---
            var myChart = ec.init(document.getElementById('loan_account'));
            //图表显示提示信息
            myChart.showLoading({
                text: "图表数据正在努力加载...",
                x: 'left'
            });
            //定义图表options
            var options = {
                tooltip: {
                    trigger: 'item',
                    formatter: "{a} <br/>{b} : {c} ({d}%)"
                },
                legend: {
                    orient: 'vertical',
                    x: 'left',
                    data: ['已使用额度', '申请中额度', '可用额度']
                },
                toolbox: {
                    show: true,
                    feature: {
                        restore: { show: false },
                        saveAsImage: { show: false }
                    }
                },
                calculable: false,
                series: []
            };

            //通过Ajax获取数据
            $.ajax({
                type: "post",
                async: false, //同步执行
                url: "/member/account_do.aspx?action=loan_account",
                dataType: "json", //返回数据形式为json
                success: function (result) {
                    if (result) {
                        //将返回的category和series对象赋值给options对象内的category和series
                        //因为xAxis是一个数组 这里需要是xAxis[i]的形式
                        options.series = eval(result.series);
                        myChart.hideLoading();
                        myChart.setOption(options);
                    }
                },
                error: function (errorMsg) {
                    alert("图表请求数据失败，请稍后再试！");
                }
            });
        }

    </script>
</head>
<body>
    <!--头部 开始-->
    <%--<uctop:top ID="top1" runat="server" />--%>
    <!--头部 结束-->
    <!--主体 开始-->
    <div class="content02">
        <div class="content02_main">
            <!--位置说明 开始-->
            <div class="position"><a href="/tradeindex.aspx">首页</a> > <a href="/member/">采购商用户中心</a> > 信贷管理 > <strong>我的信贷账户</strong></div>
            <!--位置说明 结束-->
            <div class="partc">
                <div class="pc_left">
                    <div class="blk12">
                        <%=member.Member_Left_HTML(2,3) %>
                    </div>
                </div>
                <div class="pc_right">
                    <div class="title03">我的信贷账户</div>
                    <div class="blk07">
                        <div class="b07_info02" style="border-top: none;">
                            <div id="loan_account" style="height:400px;">

                            </div>
                        </div>
                    </div>
                </div>
                <div class="clear"></div>
            </div>
        </div>
    </div>
    <!--主体 结束-->
    <!--尾部 开始-->
  <ucbottom:bottom runat="server" ID="Bottom" />
    <!--尾部 结束-->
</body>
</html>
