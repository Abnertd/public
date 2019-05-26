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
    Contract contract = new Contract();
    Glaer.Trade.B2C.BLL.ORD.IContract Icontract = null;
    Icontract = Glaer.Trade.B2C.BLL.ORD.ContractFactory.CreateContract();
    Session["Cur_Position"] = Session["Home_Position"] = "";
    int menu = tools.NullInt(Request["menu"]);
    string orders_sn = tools.CheckStr(Request["orders_sn"]);
    member.Member_Login_Check("/member/order_view.aspx?orders_sn=" + orders_sn);

    string srcString = "";
    int Contract_ID = 0;

    OrdersInfo entity = member.GetOrdersInfoBySN(orders_sn);
    if (entity == null)
    {
        pub.Msg("error", "错误信息", "记录不存在", false, "/supplier/order_list.aspx");
    }
    if (entity.Orders_BuyerID != tools.NullInt(Session["member_id"]))
    {
        pub.Msg("error", "错误信息", "记录不存在", false, "/member/order_delivery_list.aspx?payment=1&menu=1");
    }



    ContractInfo ContractInfo = Icontract.GetContractByID(entity.Orders_ContractID, pub.CreateUserPrivilege("a3465003-08b3-4a31-9103-28d16c57f2c8"));
    if (ContractInfo != null)
    {
        Contract_ID=ContractInfo.Contract_ID;
        srcString = ContractInfo.Contract_Note;
    }
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="订单详情 - 我是买家 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/layer/layer.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script type="text/javascript" src="/scripts/common.js"></script>



  
    <script type="text/javascript" src="/Public/kindeditor/kindeditor-all.js"></script>
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
            <div class="position">当前位置 > <a href="/member/index.aspx">我是买家</a> > <strong>订单详情</strong></div>
            <!--位置说明 结束-->
            <div class="partd_1">
                <div class="pd_left">
                    <%=member.Member_Left_HTML(1,1) %>
                </div>
                <div class="pd_right" style="width: 972px;">
                  <%--  修改合同--%>
                   <%-- <%if ((ContractInfo.Contract_Confirm_Status == 0) && (ContractInfo.Contract_Status == 0))
                      {%>--%>
                    <div class="blk14_1" style="margin-top: 0px;">
                        <h2 style="margin-bottom: 10px;">合同修改</h2>
                        <form id="formadd" name="formadd" method="post" action="/Contract/Contract_do.aspx?contract_id=<%=Contract_ID %>&&orders_sn=<%=orders_sn %>"> 
                            <table>
                                <tr>
                                    <td>
                                        <textarea cols="80" id="Contract_Note" name="Contract_Note" rows="16"> <%=srcString.ToString() %></textarea>
                                        <script type="text/javascript">
                                            var About_ContentEditor;
                                            KindEditor.ready(function (K) {
                                                About_ContentEditor = K.create('#Contract_Note', {
                                                    //width: '100%',
                                                    width:'968px;',
                                                    height: '500px',
                                                    filterMode: false,
                                                    afterBlur: function () { this.sync(); }
                                                });
                                            });
                                        </script>
                                    </td>
                                </tr>
                            </table>
                            <table width="100%" border="0" cellspacing="0" cellpadding="5">
                                <tr>
                                    <td align="right">
                                        <input type="hidden" id="action" name="action" value="contract_save" />
                                   
                                            <input name="contract_save" type="submit" class="bt_orange" id="contract_save" value="保存" />
                                        <input name="button" type="button" class="bt_grey" id="button" value="返回" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location = '/member/order_view.aspx?orders_sn=<%=orders_sn%>';" /></td>
                                </tr>
                            </table>
                        </form>
                    </div>
                  <%--  <%} %>--%>

                  

                </div>
                <div class="clear"></div>
            </div>
        </div>
    </div>
    <!--主体 结束-->
    <div class="clear"></div>
  <ucbottom:bottom runat="server" ID="Bottom" />

</body>
</html>
