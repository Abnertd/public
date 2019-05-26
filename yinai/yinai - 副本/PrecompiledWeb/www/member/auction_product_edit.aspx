<%@ Page Language="C#" %>

<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/HomeBottom.ascx" TagPrefix="ucbottom" TagName="bottom" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<% 
    Session["Cur_Position"] = Session["Home_Position"] = "";
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    Member member = new Member();
    Bid Mybid=new Bid();
    int ProductID = tools.CheckInt(Request["ProductID"]);
    member.Member_Login_Check("/member/auction_Product_edit.aspx?ProductID=" + ProductID);
    BidProductInfo entity = Mybid.GetBidProductByID(ProductID);
    if(entity!=null)
    {
        
    }
    else
    {
        Response.Redirect("/member/auction_list.aspx");
    }
%>


<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%="修改商品清单 - " + pub.SEO_TITLE()%></title>
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
</head>
<body>
    <uctop:top runat="server" ID="HomeTop" />
    <div class="webwrap">
        <div class="content02" style="margin-bottom: 20px;">
            <div class="position">当前位置 >  <a href="/member/index.aspx">我是买家</a> > <a href="/member/auction_list.aspx">拍卖列表</a> ><span>修改商品清单</span></div>
            <div class="partd_1">
                <div class="pd_left">
                    <% =member.Member_Left_HTML(2, 2) %>
                </div>
                <div class="pd_right">

                    <div class="blk02">
                        <h2>
                            <ul>
                                <li class="on">修改商品清单<img src="/images/icon15.jpg"></li>
                            </ul>
                        </h2>
                        <form name="frm_bid" id="frm_bid" method="post" action="/member/bid_do.aspx">
                        <div class="b02_main">
                            <ul style="width:850px;">
                                
                                <li><span><i>*</i>产品名称：</span><label><input name="Bid_Product_Name" id="Bid_Product_Name" type="text" style="width: 298px;" value="<%=entity.Bid_Product_Name %>" /></label></li>
                                <div class="clear"></div>
                                <li><span><i>*</i>型号规格：</span><label><input name="Bid_Product_Spec" id="Bid_Product_Spec" type="text" style="width: 298px;" value="<%=entity.Bid_Product_Spec %>" /></label></li>
                                <div class="clear"></div>
                               
                                <li><span><i>*</i>计量单位：</span><label><input name="Bid_Product_Unit" id="Bid_Product_Unit" type="text" style="width: 298px;" value="<%=entity.Bid_Product_Unit %>" /></label></li>
                                <div class="clear"></div>
                                <li><span><i>*</i>产品数量：</span><label><input name="Bid_Product_Amount" id="Bid_Product_Amount" type="text" style="width: 298px;" value="<%=entity.Bid_Product_Amount %>" /></label></li>
                                <div class="clear"></div>
                                <li><span><i>*</i>物流信息：</span><label><input name="Bid_Product_Delivery" id="Bid_Product_Delivery" type="text" style="width: 298px;" value="<%=entity.Bid_Product_Delivery %>" /></label></li>
                                <div class="clear"></div>
                                <li><span><i>*</i>起标价格：</span><label><input name="Bid_Product_StartPrice" id="Bid_Product_StartPrice" type="text" style="width: 298px;" value="<%=entity.Bid_Product_StartPrice %>" /></label></li>
                                <div class="clear"></div>
                                <li><span><i>*</i>商品图片：</span><label><img id="img_Bid_Product_Img" style="width:200px; height:200px;" src="<%=Application["Upload_Server_URL"]+entity.Bid_Product_Img %>"/></label></li>
                                <div class="clear"></div>
                                <li><span><i>*</i>上传图片：</span><label><iframe id="iframe2" src="<% =Application["Upload_Server_URL"]%>/public/uploadify.aspx?App=BidProduct&formname=frm_bid&frmelement=Bid_Product_Img&rtvalue=1&rturl=<% =Application["Upload_Server_Return_WWW"]%>" width="100%" height="90" frameborder="0" scrolling="no"></iframe></label></li>
                                <div class="clear"></div>
                                <li><span><i></i>备注：</span><label><input name="Bid_Product_Remark" id="Bid_Product_Remark" type="text" style="width: 298px;" value="<%=entity.Bid_Product_Remark %>" /></label></li>
                                <div class="clear"></div>

                                <li><a href="javascript:void(0);" onclick="$('#frm_bid').submit();"  class="a05">保存商品</a></li>
                            </ul>
                            
                            <div class="clear"></div>
                        </div>
                            <input name="action" type="hidden" id="action" value="editAuctionProduct" />
                            <input name="Bid_Product_Img" type="hidden" id="Bid_Product_Img" value="<%=entity.Bid_Product_Img %>" />
                            <input name="ProductID" type="hidden" id="ProductID" value="<%=ProductID %>" />
                            <input name="Bid_BidID" type="hidden" id="Bid_BidID" value="<%=entity.Bid_BidID %>" />
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
