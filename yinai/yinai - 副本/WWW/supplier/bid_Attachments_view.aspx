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
    Bid Mybid=new Bid();
    int AttID = tools.CheckInt(Request["AttID"]);
    supplier.Supplier_Login_Check("/supplier/bid_Attachments_view.aspx?AttID=" + AttID);
    SupplierInfo supplierinfo = supplier.GetSupplierByID();
    if (supplierinfo != null)
    {
        if (tools.NullInt(Session["supplier_ishaveshop"]) == 0)
        {
            pub.Msg("error", "错误信息", "您还没有开通店铺", false, "/supplier/index.aspx");
        }
    }
    
    BidAttachmentsInfo entity = Mybid.GetBidAttachmentsByID(AttID);
    if (entity != null)
    {
        BidInfo bidinfo = Mybid.GetBidByID(entity.Bid_Attachments_BidID);
        if (entity != null)
        {
            if (bidinfo.Bid_MemberID != tools.NullInt(Session["member_id"]) || bidinfo.Bid_MemberID == 0)
            {
                Response.Redirect("/supplier/auction_list.aspx");
            }
            if (bidinfo.Bid_Status > 0)
            {
                Response.Redirect("/supplier/auction_list.aspx");
            }
            if (bidinfo.Bid_Type == 0)
            {
                Response.Redirect("/supplier/auction_list.aspx");
            }
        }
        else
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
    <title><%="查看附件 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
        <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <script src="/scripts/hdtab.js"></script>
    <script src="/scripts/1.js"></script>
    <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/layer/layer.js"></script>
    <script type="text/javascript" src="/scripts/common.js"></script>
    <script type="text/javascript" src="/scripts/supplier.js"></script>
        <script type="text/javascript" src="/Public/kindeditor/kindeditor-all.js"></script>
    <script type="text/javascript" src="/Public/kindeditor/lang/zh_CN.js"></script>
    <link href="/scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
    <script src="/scripts/jquery-ui/jquery-ui.js"></script>
    <script src="/scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js"></script>
            <script>
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
        <style>
        .b02_main ul li { padding:0px;line-height:30px;}
    </style>
</head>
<body>
    <uctop:top runat="server" ID="HomeTop" />
    <div class="webwrap">
        <div class="content02" style="margin-bottom: 20px;">
            <div class="position">当前位置 > <a href="/">首页</a> > <a href="/supplier/index.aspx">商家中心</a> > <a href="/supplier/auction_list.aspx">拍卖列表</a> ><span>查看附件</span></div>
            <div class="partd_1">
                <div class="pd_left">
                    <% supplier.Get_Supplier_Left_HTML(5, 3); %>
                </div>
                <div class="pd_right">

                    <div class="blk02">
                        <h2>
                            <ul>
                                <li class="on">查看附件<img src="/images/icon15.jpg"></li>
                            </ul>
                            <span><a href="javascript:void(0);" onclick="history.go(-1);">返回</a></span>
                        </h2>

                        <div class="b02_main">
                            <ul style="width:850px;">
                                <li><span><i></i>序号：</span><label><%=entity.Bid_Attachments_Sort %></label></li>
                                <div class="clear"></div>
                                <li><span><i></i>附件名称：</span><label><%=entity.Bid_Attachments_Name %></label></li>
                                <div class="clear"></div>
                                <li><span><i></i>文件格式：</span><label><%=entity.Bid_Attachments_format %></label></li>
                                <div class="clear"></div>
                                <li><span><i></i>大小：</span><label><%=entity.Bid_Attachments_Size %></label></li>
                                <div class="clear"></div>
                                <li><span><i></i>说明：</span><label><%=entity.Bid_Attachments_Remarks %></label></li>
                                <div class="clear"></div>
                                <li><span><i></i>附件：</span><label><a href="<%=Application["Upload_Server_URL"]+entity.Bid_Attachments_Path %>" target="_blank" style="color: #ff6600;">点击查看</a></label></li>
                                <div class="clear"></div>

                            </ul>
                            
                            <div class="clear"></div>
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
