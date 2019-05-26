<%@ Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>



<% 
    
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    //Member member = new Member();
    Supplier supplier = new Supplier();
    Contract contract = new Contract();
    Glaer.Trade.B2C.BLL.ORD.IContract Icontract = null;
    Icontract = Glaer.Trade.B2C.BLL.ORD.ContractFactory.CreateContract();
    Session["Cur_Position"] = Session["Home_Position"] = "";
    int menu = tools.NullInt(Request["menu"]);
    string orders_sn = tools.CheckStr(Request["orders_sn"]);
    //member.Member_Login_Check("/member/order_view.aspx?orders_sn=" + orders_sn);
    supplier.Supplier_Login_Check("/supplier/order_view.aspx?orders_sn=" + orders_sn);

    string srcString = "";
    int Contract_ID = 0;

    OrdersInfo entity = supplier.GetSupplierOrdersInfoBySN(orders_sn);
    if (entity == null)
    {
        pub.Msg("error", "错误信息", "记录不存在", false, "/supplier/order_list.aspx");
    }
    if (entity.Orders_SupplierID != tools.NullInt(Session["supplier_id"]))
    {
        pub.Msg("error", "错误信息", "记录不存在", false, "/supplier/order_detail.aspx?orders_sn=" + entity.Orders_SN + "");
    }



    ContractInfo ContractInfo = Icontract.GetContractByID(entity.Orders_ContractID, pub.CreateUserPrivilege("a3465003-08b3-4a31-9103-28d16c57f2c8"));
    if (ContractInfo != null)
    {
        Contract_ID = ContractInfo.Contract_ID;



        srcString = new Supplier().ReplaceOrdersContract_SupplierNew(entity, ContractInfo.Contract_ID);

    }
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="合同详情 - 我是卖家 - " + pub.SEO_TITLE()%></title>
    <meta name="Keywords" content="<% = Application["Site_Keyword"]%>" />
    <meta name="Description" content="<%=Application["Site_Description"]%>" />

    <link href="/css/index.css" rel="stylesheet" type="text/css" />

    <!--layer弹出框.js .css引用 开始-->
    <link href="../layer.m/layer.m.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery-1.5.1.min.js" charset="gb2312"></script>
    <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/layer/layer.js"></script>
    <!--layer弹出框.js .css引用 结束-->

    <script src="/scripts/member.js" type="text/javascript"></script>
    <script type="text/javascript" src="/scripts/common.js"></script>





    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="../css/index_newadd.css" rel="stylesheet" />

    <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script src="/js/hdtab.js" type="text/javascript"></script>

    <script type="text/javascript">
        window.onload = function () {
            var SDmodel = new scrollDoor();
            SDmodel.sd(["a01", "a02", "a03", "a04"], ["aa01", "aa02", "aa03", "aa04"], "on", " ");
            SDmodel.sd(["b01", "b02"], ["bb01", "bb02"], "on", " ");
        }

        function sum(amount_id, price_id, sum_id) {
            var sumSP = $("#" + sum_id);// 获得ID为first标签的jQuery对象 
            var Price = $("#" + price_id);// 获得ID为first标签的jQuery对象  
            var Amount = $("#" + amount_id);// 获得ID为first标签的jQuery对象  


            var num1 = Price.val();// 取得first对象的值  
            var num2 = Amount.val();// 取得second对象的值  
            var sum = (num1) * (num2);

            sumSP.text(sum);


        }


    </script>
    <!--滑动门 结束-->
    <!--示范一个公告层 开始-->
    <script type="text/javascript">
        function SignUpNow() {
            layer.open({
                type: 2
   , title: false //不显示标题栏
                //, closeBtn: false
   , area: ['480px;', '340px']

   , shade: 0.8
   , id: 'LAY_layuipro' //设定一个id，防止重复弹出
   , resize: false
   , btnAlign: 'c'
   , moveType: 1 //拖拽模式，0或者1              
                , content: ("/Bid/SignUpPopup.aspx")
            });
        }
    </script>
    <!--示范一个公告层 结束-->
    <script src="/js/1.js" type="text/javascript"></script>
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
            padding: 3px;
        }

        textarea, td, div, input {
            font-size: 19px;
        }


        .blk14_1 span a {
            color: #ffffff;
            float: right;
            font-size: 12px;
            font-weight: normal;
            margin-right: 10px;
        }

        table, tr, td, th {
            margin: 15px auto;
        }
    </style>
    <script type="text/javascript">
        function turnnewpage(url) {
            location.href = url
        }
    </script>
</head>
<body>

    <uctop:top ID="top1" runat="server" />

    <!--主体 开始-->
    <div class="webwrap">
        <div class="content02">

            <!--位置说明 开始-->
            <div class="position">当前位置 > <a href="/member/index.aspx">我是卖家</a> > <strong>订单详情</strong></div>
            <!--位置说明 结束-->
            <div class="partd_1">
                <div class="pd_left">
                    <%-- <%=member.Member_Left_HTML(1,1) %>--%>
                    <% supplier.Get_Supplier_Left_HTML(1, 1); %>
                </div>
                <div class="pd_right" style="width: 972px;">
                    <%-- 查看合同--%>

                    <div class="blk14_1" style="margin-top: 0px;">

                        <h2 style="margin-bottom: 10px;">合同预览<span> <a href="javascript:void();" name="btn_print" type="button" class="btn_01" id="Button4" value="合同下载" onclick="window.open('Contract_view.aspx?action=print&contract_id=<% =Contract_ID %>    ')">合同下载</a></span></h2>
                        <form id="form1" name="formadd" method="post" action="/Contract/Contract_do.aspx?contract_id=<%=Contract_ID %>&&orders_sn=<%=orders_sn %>">
                            <table>
                                <tr>
                                    <td>
                                        <%Response.Write(Server.HtmlDecode(srcString));%> 
                                    </td>
                                </tr>
                            </table>
                        </form>
                    </div>
                    <%--    <%} %>--%>
                </div>
                <div class="clear"></div>
            </div>
        </div>
    </div>
    <%--右侧浮动弹框 开始--%>
    <div id="leftsead">
        <ul>
            <li>
                <a href="javascript:void(0);" onclick="SignUpNow();">
                    <div class="hides" style="width: 130px; height: 50px; display: none;" id="qq">
                        <div class="hides" id="p1">
                            <img src="/images/nav_1_1.png" />
                        </div>
                    </div>
                    <img src="/images/nav_1.png" width="57px" height="50px" class="shows" />
                </a>
            </li>
            <li id="tel">
                <a href="javascript:void(0)">
                    <div class="hides" style="width: 130px; height: 50px; display: none;" id="tels">
                        <div class="hides" id="p2">
                            <img src="/images/nav_2_1.png">
                        </div>

                    </div>
                    <img src="/images/nav_2.png" width="57px" height="50px" class="shows" />
                </a>
            </li>
            <li id="btn">
                <a id="top_btn" href="http://wpa.qq.com/msgrd?v=3&uin=3558103267&site=qq&menu=yes" target="_blank">
                    <div class="hides" style="width: 130px; height: 50px; display: none">
                        <div class="hides" id="p3">
                            <img src="/images/nav_3_1.png" width="130px;" height="50px" id="Img1" />
                        </div>
                    </div>
                    <img src="/images/nav_3.png" width="57px" height="50px" class="shows" />
                </a>
            </li>
            <li id="Li1">
                <a href="#top">
                    <div class="hides" style="width: 130px; display: none" id="Div1">
                        <div class="hides" id="p4">
                            <img src="/images/nav_4_1.png" width="130px;" height="50px" />
                        </div>
                    </div>
                    <img src="/images/nav_4.png" width="57px" height="50px" class="shows" />
                </a>
            </li>
        </ul>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#leftsead a").hover(function () {

                $(this).children("div.hides").show();
                $(this).children("img.shows").hide();
                $(this).children("div.hides").animate({ marginRight: '0px' }, '0');

            }, function () {
                $(this).children("div.hides").animate({ marginRight: '-130px' }, 0, function () { $(this).hide(); $(this).next("img.shows").show(); });
            });
            $("#top_btn").click(function () { if (scroll == "off") return; $("html,body").animate({ scrollTop: 0 }, 600); });
        });
    </script>
    <%--右侧浮动弹框 结束--%>
    <!--主体 结束-->
    <div class="clear"></div>
    <ucbottom:bottom runat="server" ID="Bottom" />

</body>
</html>
