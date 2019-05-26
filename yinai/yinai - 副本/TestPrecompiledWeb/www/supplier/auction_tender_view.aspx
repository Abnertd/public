<%@ Page Language="C#" %>

<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<% 
    Session["Cur_Position"] = Session["Home_Position"] = "";
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    Supplier supplier = new Supplier();
    Bid Mybid = new Bid();
    int TenderID = tools.CheckInt(Request["TenderID"]);
    supplier.Supplier_Login_Check("/supplier/auction_tender_view.aspx?TenderID=" + TenderID);
    SupplierInfo supplierinfo = supplier.GetSupplierByID();
    if (supplierinfo != null)
    {
        if (tools.NullInt(Session["supplier_ishaveshop"]) == 0)
        {
            pub.Msg("error", "错误信息", "您还没有开通店铺", false, "/supplier/index.aspx");
        }
    }
    TenderInfo Tender = Mybid.GetTenderByID(TenderID);
    int GetBidTimes = 0;
    BidInfo entity = null;
    if (Tender != null)
    {
        entity = Mybid.GetBidByID(Tender.Tender_BidID);
        GetBidTimes = Mybid.GetBidTimes(Tender.Tender_BidID, Tender.Tender_SupplierID);
    }
    else
    {
        Response.Redirect("/supplier/auction_list.aspx");
    }
    ;
    if (entity != null)
    {
        if (entity.Bid_MemberID != tools.NullInt(Session["member_id"]) || entity.Bid_MemberID == 0)
        {
            Response.Redirect("/supplier/auction_list.aspx");
        }
    }
    else
    {
        Response.Redirect("/supplier/auction_list.aspx");
    }
%>


<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%="竞价详情 - " + pub.SEO_TITLE()%></title>
    <meta name="Keywords" content="<% = Application["Site_Keyword"]%>" />
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <script src="/scripts/hdtab.js"></script>
    <script src="/scripts/1.js"></script>
    <script type="text/javascript" src="/scripts/common.js"></script>
    <script type="text/javascript" src="/Public/kindeditor/kindeditor-all.js"></script>
    <script type="text/javascript" src="/Public/kindeditor/lang/zh_CN.js"></script>
    <link href="/scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
    <script src="/scripts/jquery-ui/jquery-ui.js"></script>
    <script src="/scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js"></script>
   <%-- <style>
        .b14_1_main table td.name {
            border-left:none;
        }
        .b14_1_main table td {
            border-bottom:none;
        }
    </style>--%>
</head>
<body>
    <uctop:top runat="server" ID="HomeTop" />

    <div class="webwrap">
        <div class="content02" style="margin-bottom: 20px;">
            <div class="position">当前位置 > <a href="/">首页</a> > <a href="/supplier/index.aspx">供应商用户中心</a> > <a href="/supplier/auction_list.aspx">拍卖列表</a> ><span>竞价详情</span></div>
            <div class="partd_1">
                <div class="pd_left">
                    <% supplier.Get_Supplier_Left_HTML(5, 3); %>
                </div>
                <div class="pd_right">


                    <div class="blk14_1" style="margin-top: 1px;">
                        <h2>竞价详情</h2>
                        <div class="main">
                            <div class="b14_1_main">
                                <table width="973" border="0" cellspacing="0" cellpadding="0">
                                    <tr>

                                        <td width="100" class="name">报价单位</td>
                                        <td width="300"><span><a><%=Mybid.Supplier_CompanyName(Tender.Tender_SupplierID) %></a></span></td>
                                        <td width="100" class="name">竞价次数</td>
                                        <td width="400"><span><a><%=GetBidTimes %></a>次</span></td>
                                    </tr>
                                    <%--<tr>
                                        <td class="name">竞价总金额</td>
                                        <td><span><a><%=pub.FormatCurrency(Tender.Tender_AllPrice) %></a></span></td>
                                        <td class="name">竞价时间</td>
                                        <td><%=Tender.Tender_Addtime.ToString("yyyy-MM-dd HH:mm") %></td>
                                    </tr>
                                    <tr>
                                        <td class="name">是否中标</td>
                                        <td><span><a><%=Mybid.IsWin(Tender.Tender_Status,Tender.Tender_IsWin) %></a></span></td>
                                        <td></td>
                                        <td></td>
                                    </tr>--%>
                                </table>


                                <h3 style="padding: 10px;">竞价清单</h3>
                                <table width="975" border="0" cellspacing="0" cellpadding="0" style="border: 1px solid #eeeeee;">
                                    <tr>

                                        <td style="width:139px;" class="name">产品名称</td>
                                        <td style="width:139px;" class="name">型号规格</td>

                                        <td style="width:139px;" class="name">计量单位</td>
                                        <td style="width:139px;" class="name">产品数量</td>
                                        <td style="width:139px;" class="name">起标价格</td>
                                        <td style="width:139px;" class="name">单价竞价</td>
                                       <%--  <td width="100" class="name">排名</td>--%>
                                          <td style="width:139px;" class="name">操作</td>
                                    </tr>

                                    <%Mybid.AuctionTenderProductList(entity.BidProducts, Tender.TenderProducts); %>
                                </table>
                            </div>

                           <%if(entity.Bid_SupplierID==0) {%>
                            <div class="b02_main" >
                                <form name="frm_bid" id="frm_bid" method="post" action="/supplier/auction_do.aspx">
                                <ul style="width:850px;">
                                    <li><a class="a05" href="javascript:void(0);" onclick="$('#frm_bid').submit();" >中标</a></li>
                                    </ul>
                                    <input name="action" type="hidden" id="action" value="winadd" />
                                    <input name="BidID" type="hidden" id="BidID" value="<%=entity.Bid_ID %>" />
                                    <input name="TenderID" type="hidden" id="TenderID" value="<%=Tender.Tender_ID %>" />
                            </form>
                                </div>
                                <div class="clear"></div>
                            <%} %>
                        </div>


                    </div>
                </div>

            </div>
            <div class="clear"></div>
        </div>
    </div>
    <ucbottom:bottom runat="server" ID="Bottom" />
</body>
</html>
