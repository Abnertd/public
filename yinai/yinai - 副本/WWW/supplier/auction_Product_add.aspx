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
    int BID = tools.CheckInt(Request["BID"]);
    supplier.Supplier_Login_Check("/supplier/auction_Product_add.aspx?BID=" + BID);
    double Supplier_Account;
    Supplier_Account = 0;
    SupplierInfo supplierinfo = supplier.GetSupplierByID();
    if (supplierinfo != null)
    {
        Supplier_Account = supplierinfo.Supplier_Security_Account;
        if (tools.NullInt(Session["supplier_ishaveshop"]) == 0)
        {
            pub.Msg("error", "错误信息", "您还没有开通店铺", false, "/supplier/index.aspx");
        }
    }

    BidInfo entity = Mybid.GetBidByID(BID);
    if(entity!=null)
    {
        if (entity.Bid_MemberID != tools.NullInt(Session["member_id"]) || entity.Bid_MemberID == 0)
        {
            Response.Redirect("/supplier/auction_list.aspx");
        }
        if(entity.Bid_Status>0)
        {
            Response.Redirect("/supplier/auction_list.aspx");
        }
        if (entity.Bid_Type == 0)
        {
            Response.Redirect("/supplier/auction_list.aspx");
        }
        if(entity.BidProducts!=null)
        {
            if(entity.BidProducts.Count>0)
            {
                pub.Msg("error", "错误信息", "拍卖商品只能添加一个", false, "{back}");
            }
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
    <title><%="发布拍卖商品 - " + pub.SEO_TITLE()%></title>
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
    <style type="text/css">
        #mask_div {
            display: none;
            position: absolute;
            z-index: 1001;
            height: 100%;
            width: 100%;
            left: 0px;
            overflow-y: hidden;
            background: #000000;
            filter: Alpha(opacity=30);
            opacity: 0.3;
        }

        #html-div {
            z-index: 9999;
            width: 602px;
            background-color: #ffffff;
            position: absolute;
            left: 50%;
            top: 50%;
            margin: 0px auto;
            padding: 0px;
        }
    </style>
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
        <script>
            function AddProduct(obj) {

                $("#html-div").show();
                $("#mask_div").css('height', $("body").height() + 20);
                $("#html-div").html("<div style=\"text-align:center;padding-top:150px; \">加载中..</div>");
                $.ajax({
                    type: "get",
                    global: false,
                    async: false,
                    dataType: "html",
                    url: encodeURI("/supplier/tender_product.aspx?productid=" + obj + "&timer=" + Math.random()),
                    success: function (data) {
                        $("#html-div").html(data);
                    },
                    error: function () {
                        alert("Error Script");
                    }
                });
                if ($("#html-div").html() != "") {
                    var _scrollHeight = $(document).scrollTop(), //获取当前窗口距离页面顶部高度 
        _windowHeight = $(window).height(), //获取当前窗口高度 
        _windowWidth = $(window).width(), //获取当前窗口宽度 
        _popupHeight = $("#html-div").height(), //获取弹出层高度 
        _popupWeight = $("#html-div").width(); //获取弹出层宽度 
                    _posiTop = (_windowHeight - _popupHeight) / 2 + _scrollHeight;
                    _posiLeft = (_windowWidth - _popupWeight) / 2;
                    $("#html-div").css({ "left": _posiLeft + "px", "top": "200px", "display": "block" }); //设置position 
                    $("#mask_div").show();
                }
            }
            function dialog_loginhide() {
                $("#html-div").css('left', '50%');
                $("#html-div").css('top', '50%');
                $("#html-div").hide();
                $("#mask_div").hide();
            }

            function Product(obj, id, name) {
                $("#Bid_Product_ProductID").val(id);
                $("#Bid_Product_Name").val(name);
            }
    </script>
</head>
<body>
        <div id="dialog-message" title="" style="display: none;">
    </div>
    <div id="html-div" title="" style="display: none;">
    </div>
    <div id="mask_div">
    </div>
    <uctop:top runat="server" ID="HomeTop" />
    <div class="webwrap">
        <div class="content02" style="margin-bottom: 20px;">
            <div class="position">当前位置 > <a href="/supplier/index.aspx">我是卖家</a> > <a href="/supplier/auction_list.aspx">拍卖列表</a> ><span>发布拍卖商品</span></div>
            <div class="partd_1">
                <div class="pd_left">
                    <% supplier.Get_Supplier_Left_HTML(5, 4); %>
                </div>
                <div class="pd_right">

                    <div class="blk02">
                        <h2>
                            <ul>
                                <li class="on">发布拍卖商品<img src="/images/icon15.jpg"></li>
                            </ul>
                        </h2>
                        <form name="frm_bid" id="frm_bid" method="post" action="/supplier/auction_do.aspx">
                        <div class="b02_main">
                            <ul style="width:850px;">
                               <%-- <li><span><i>*</i>产品编号：</span><label><input name="Bid_Product_Code" id="Bid_Product_Code" type="text" style="width: 298px;" value="" /></label></li>
                                <div class="clear"></div>--%>
                                <li><span><i>*</i>产品名称：</span><label><input name="Bid_Product_Name" id="Bid_Product_Name" type="text" style="width: 298px;" value=""  readonly="readonly"/> &nbsp;&nbsp;&nbsp;&nbsp;<a style="background-image:url(../images/a_bg01.jpg); background-repeat:no-repeat; width:74px; height:26px; font-size:12px; font-weight:normal; text-align:center; line-height:26px; color:#333; display:inline-block; vertical-align:middle; margin-right:7px;" href="javascript:void(0);" onclick="AddProduct(0);">选择产品</a></label></li>
                                <div class="clear"></div>
                                <li><span><i>*</i>型号规格：</span><label><input name="Bid_Product_Spec" id="Bid_Product_Spec" type="text" style="width: 298px;" value="" /></label></li>
                                <div class="clear"></div>
                               <%-- <li><span><i>*</i>品牌：</span><label><input name="Bid_Product_Brand" id="Bid_Product_Brand" type="text" style="width: 298px;" value="" /></label></li>
                                <div class="clear"></div>--%>
                                <li><span><i>*</i>计量单位：</span><label><input name="Bid_Product_Unit" id="Bid_Product_Unit" type="text" style="width: 298px;" value="" /></label></li>
                                <div class="clear"></div>
                                <li><span><i>*</i>产品数量：</span><label><input name="Bid_Product_Amount" id="Bid_Product_Amount" type="text" style="width: 298px;" value="" /></label></li>
                                <div class="clear"></div>
                                <li><span><i>*</i>物流信息：</span><label><input name="Bid_Product_Delivery" id="Bid_Product_Delivery" type="text" style="width: 298px;" value="" /></label></li>
                                <div class="clear"></div>
                                <li><span><i>*</i>起标价格：</span><label><input name="Bid_Product_StartPrice" id="Bid_Product_StartPrice" type="text" style="width: 298px;" value="" /></label></li>
                                <div class="clear"></div>
                                <li><span><i>*</i>上传图片：</span>
                                    <iframe src="<% =Application["Upload_Server_URL"]%>/public/uploadify.aspx?App=BidProduct&formname=frm_bid&frmelement=Bid_Product_Img&rtvalue=1&rturl=<% =Application["Upload_Server_Return_WWW"]%>" width="100%" height="90" frameborder="0" scrolling="no"></iframe></li>
                                <div class="clear"></div>
                                <li id="tr_Bid_Product_Img" style="display:none;"><span>&nbsp;</span><label><img id="img_Bid_Product_Img" style="width:200px; height:200px;"/></label></li>

                                <li><span><i></i>备注：</span><label><input name="Bid_Product_Remark" id="Bid_Product_Remark" type="text" style="width: 298px;" value="" /></label></li>
                                <div class="clear"></div>

                                <li><a href="javascript:void(0);" onclick="$('#frm_bid').submit();"  class="a05">保存商品</a></li>
                            </ul>
                            
                            <div class="clear"></div>
                        </div>
                            <input name="action" type="hidden" id="action" value="addAuctionProduct" />
                            <input name="Bid_Product_Sort" id="Bid_Product_Sort" type="hidden" value="1" />
                            <input name="Bid_BidID" type="hidden" id="Bid_BidID" value="<%=BID %>" />
                            <input name="Bid_Product_Img" type="hidden" id="Bid_Product_Img" value="" />
                            <input name="Bid_Product_ProductID" id="Bid_Product_ProductID" type="hidden" value=""/>
                            </form>
                    </div>
                </div>
            </div>
            <div class="clear"></div>
        </div>
    </div>
    <ucbottom:bottom runat="server" ID="Bottom" />
</body>
</html>
