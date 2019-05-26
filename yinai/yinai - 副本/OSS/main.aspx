<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    private Statistic myApp;

    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("all");

        myApp = new Statistic();
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
  
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>无标题文档</title>
    
    <link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
    <script src="/Scripts/jquery.js" type="text/javascript"></script>
    <style type="text/css">
        .list_head_bg {
            text-align: right;
            width: 150px;
        }

        .list_td_bg {
            text-align: left;
        }

            .list_td_bg a {
                font-size: 16px;
                font-weight: bold;
                color: #f90;
            }
    </style>
</head>
<body>
    <div class="content_div">
        <table width="100%" border="0" cellpadding="5" cellspacing="0" class="content_table">
            <tr>
                <td class="content_title">我的桌面</td>
            </tr>
            <tr>
                <td>
                    <table width="100%" cellspacing="10" style="border: dotted 0px #ccc;">
                        <tr>

                            <td style="vertical-align: top;">
                                <div style="margin: 5px; font-weight: bold;">今日业务量</div>
                                <table width="100%" class="list_table_bg" cellspacing="1" cellpadding="5">


                                    <tr>
                                        <td class="list_head_bg">今日新增供应商</td>
                                        <td class="list_td_bg"><a href="/supplier/supplier_list.aspx"><%=myApp.GetSupplierCount("today")%>个</a></td>
                                    </tr>
                                    <tr>
                                        <td class="list_head_bg">昨日新增会员</td>
                                        <td class="list_td_bg"><a href="/supplier/supplier_list.aspx"><%=myApp.GetSupplierCount("yesterday")%>个</a></td>
                                    </tr>
                                </table>
                            </td>
                            <td style="vertical-align: top;">
                                <div style="margin: 5px; font-weight: bold;">待处理事务</div>
                                <table width="100%" class="list_table_bg" cellspacing="1" cellpadding="5">
                                    <tr>
                                        <td class="list_head_bg">未处理订单</td>
                                        <td class="list_td_bg"><a href="/orders/orders_list.aspx?orders_status=0"><% =myApp.GetOrdersCount("unconfirm")%>个</a></td>
                                    </tr>



                                  <%--  <tr>
                                        <td class="list_head_bg">供应商注册待审核数量</td>
                                        <td class="list_td_bg"> <a href="/Supplier/Supplier_list.aspx" target="main"><%=myApp.GetUnAuditSupplierAmount()%>个</a></td>
                                    </tr>--%>


                                    <tr>
                                        <td class="list_head_bg">待审核供应商数量</td>
                                        <td class="list_td_bg"><a href="/supplier/supplier_list.aspx" target="main"><%=myApp.GetUnAuditSupplierCertAmount_new()%>个</a></td>
                                    </tr>                                    <tr>
                                        <td class="list_head_bg">待审核物流发布</td>
                                        <td class="list_td_bg"><a href="/supplier/supplier_list.aspx" target="main"><%=myApp.GetUnAuditSupplierCertAmount_new()%>个</a></td>
                                    </tr>


                                    <tr>
                                        <td class="list_head_bg">会员提交未回复留言信息</td>
                                        <td class="list_td_bg"> <a href="/member/feedback_list.aspx?isreply=2" target="main"><%=myApp.GetUnReplyMessageAmount()%>个</a></td>
                                    </tr>

                                    <tr>
                                        <td class="list_head_bg">会员提交未回复投诉信息</td>
                                        <td class="list_td_bg"> <a href="/supplier/feedback_list.aspx?listtype=complain" target="main"><%=myApp.GetComplaintMail()%>个</a></td>
                                    </tr>
                                    
                                    <tr>
                                        <td class="list_head_bg">待审核招标拍卖</td>
                                        <td class="list_td_bg"> <a href="/Bid/Bid_list.aspx" target="main"><%=myApp.GetUnAuditBidAmount()%>个</a></td>
                                    </tr>
                                    
                                    <tr>
                                        <td class="list_head_bg">待审核物流发布</td>
                                        <td class="list_td_bg"> <a href="/Logistics/Supplier_Logistics_List.aspx" target="main"><%=myApp.GetUnAuditLogisticsPost()%>个</a></td>
                                    </tr>



                                    



                         <%--             <tr>
                                        <td class="list_head_bg">待审核产品信息</td>
                                        <td class="list_td_bg"> <a href="/Product/product.aspx?listtype=unAudit" target="main"><%=myApp.GetUnAuditProductAmount()%>个</a></td>
                                    </tr>



                                      <tr>
                                        <td class="list_head_bg">招标拍卖待审核</td>
                                        <td class="list_td_bg"> <a href="/stock/stockout.aspx?isprocess=2" target="main"><%=myApp.GetUnProcessStockoutAmount()%>个</a></td>
                                    </tr>

                                      <tr>
                                        <td class="list_head_bg">物流待审核</td>
                                        <td class="list_td_bg"> <a href="/stock/stockout.aspx?isprocess=2" target="main"><%=myApp.GetUnProcessStockoutAmount()%>个</a></td>
                                    </tr>

                                      <tr>
                                        <td class="list_head_bg">投诉信息</td>
                                        <td class="list_td_bg"> <a href="/stock/stockout.aspx?isprocess=2" target="main"><%=myApp.GetUnProcessStockoutAmount()%>个</a></td>
                                    </tr>--%>
              

                                    <%-- <div class="word" id="top_scroll">
                <div id="scroll_content"> 
                    <span style="height: 29px;">
                        <img src="/images/icon01.png" align="absmiddle" />
                        <a href="/Supplier/Supplier_list.aspx" target="main">供应商注册待审核数量 <b style="color: red;"><%=myApp.GetUnAuditSupplierAmount()%></b> 个</a></span>
                    <span style="height: 29px;">
                        <img src="/images/icon01.png" align="absmiddle" />
                        <a href="/supplier/supplier_cert_list.aspx" target="main">待审核供应商资质数量 <b style="color: red;"><%=myApp.GetUnAuditSupplierCertAmount()%></b> 个</a></span>
                    <span style="height: 29px;">
                        <img src="/images/icon01.png" align="absmiddle" />
                        <a href="/member/feedback_list.aspx?isreply=2" target="main">会员提交未回复留言信息 <b style="color: red;"><%=myApp.GetUnReplyMessageAmount()%></b> 个</a></span>
                    <span style="height: 29px;">
                        <img src="/images/icon01.png" align="absmiddle" />
                        <a href="/Product/product.aspx?listtype=unAudit" target="main">待审核产品信息 <b style="color: red;"><%=myApp.GetUnAuditProductAmount()%></b> 个</a></span>
                    <span style="height: 29px;">
                        <img src="/images/icon01.png" align="absmiddle" />
                        <a href="/stock/stockout.aspx?isprocess=2" target="main">未处理缺货登记信息 <b style="color: red;"><%=myApp.GetUnProcessStockoutAmount()%></b> 个</a></span>


                </div>
            </div>--%>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <div style="margin: 5px 0px; font-size: 14px; font-weight: bold;">最近30天销售额</div>

                    <div style="border-bottom: dotted 2px #ccc;"></div>

                    <div>
                        <img src="" id="salechart" />
                    </div>
                    <script type="text/javascript"> $("#salechart")[0].src = "/public/chart.aspx?type=index&width=" + ($(document).width() - 36) + "&height=300";</script>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
