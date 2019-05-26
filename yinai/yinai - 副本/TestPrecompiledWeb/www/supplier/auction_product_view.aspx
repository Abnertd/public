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
    int ProductID = tools.CheckInt(Request["ProductID"]);
    supplier.Supplier_Login_Check("/member/Bid_Product_edit.aspx?ProductID=" + ProductID);
    SupplierInfo supplierinfo = supplier.GetSupplierByID();
    if (supplierinfo != null)
    {
        if (tools.NullInt(Session["supplier_ishaveshop"]) == 0)
        {
            pub.Msg("error", "错误信息", "您还没有开通店铺", false, "/supplier/index.aspx");
        }
    }
    
    BidProductInfo entity = Mybid.GetBidProductByID(ProductID);
    if(entity!=null)
    {
        BidInfo bidinfo = Mybid.GetBidByID(entity.Bid_BidID);
        if (bidinfo != null)
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
    <title><%="商品清单 - " + pub.SEO_TITLE()%></title>
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
            <div class="position">当前位置 > <a href="/supplier/index.aspx">我是卖家</a> > <a href="/supplier/auction_list.aspx">拍卖列表</a> ><span>商品清单</span></div>
            <div class="partd_1">
                <div class="pd_left">
                    <% supplier.Get_Supplier_Left_HTML(5, 3); %>
                </div>
                <div class="pd_right">

                    <div class="blk02">
                        <h2>
                            <ul>
                                <li class="on">商品清单<img src="/images/icon15.jpg"></li>
                            </ul>
                            <div class="clear"></div>
                            <span><a href="javascript:void(0);" onclick="history.go(-1);">返回</a></span>

                        </h2>

                        <div class="b02_main">
                            <ul style="width:850px;">
<%--                                <li><span><i></i>序号：</span><label><%=entity.Bid_Product_Sort %></label></li>
                                <div class="clear"></div>--%>
                                <li><span><i></i>产品编号：</span><label><%=entity.Bid_Product_Code %></label></li>
                                <div class="clear"></div>
                                <li><span><i></i>产品名称：</span><label><%=entity.Bid_Product_Name %></label></li>
                                <div class="clear"></div>
                                <li><span><i></i>型号规格：</span><label><%=entity.Bid_Product_Spec %></label></li>
                                <div class="clear"></div>
                                <li><span><i></i>品牌：</span><label><%=entity.Bid_Product_Brand %></label></li>
                                <div class="clear"></div>
                                <li><span><i></i>计量单位：</span><label><%=entity.Bid_Product_Unit %></label></li>
                                <div class="clear"></div>
                                <li><span><i></i>产品数量：</span><label><%=entity.Bid_Product_Amount %></label></li>
                                <div class="clear"></div>
                                <li><span><i></i>物流信息：</span><label><%=entity.Bid_Product_Delivery %></label></li>
                                <div class="clear"></div>
                                <li><span><i></i>起标价格：</span><label><%=pub.FormatCurrency( entity.Bid_Product_StartPrice) %></label></li>
                                <div class="clear"></div>
                                <li><span><i></i>商品图片：</span><label><img id="img_Bid_Product_Img" style="width:200px; height:200px;" src="<%=Application["Upload_Server_URL"]+entity.Bid_Product_Img %>"/></label></li>
                                <div class="clear"></div>
                                <li><span><i></i>备注：</span><label><%=entity.Bid_Product_Remark %></label></li>
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
