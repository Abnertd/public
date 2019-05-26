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
    supplier.Supplier_Login_Check("/supplier/bid_Attachments_edit.aspx?AttID=" + AttID);
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
    <title><%="修改附件 - " + pub.SEO_TITLE()%></title>
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
    <script>
        function Check_Path() {
            if ($("#Bid_Attachments_Path").val() == "" || $("#Bid_Attachments_Path").val() == null) {
                alert("请先上传附件");
                return false;
            }
            else {
                return true;
            }


        }
    </script>
</head>
<body>
    <uctop:top runat="server" ID="HomeTop" />
    <div class="webwrap">
        <div class="content02" style="margin-bottom: 20px;">
            <div class="position">当前位置 > <a href="/">首页</a> > <a href="/supplier/index.aspx">供货商会员中心</a> > <a href="/supplier/auction_list.aspx">拍卖列表</a> ><span>修改附件</span></div>
            <div class="partd_1">
                <div class="pd_left">
                    <% supplier.Get_Supplier_Left_HTML(5, 4); %>
                </div>
                <div class="pd_right">

                    <div class="blk02">
                        <h2>
                            <ul>
                                <li class="on">修改附件<img src="/images/icon15.jpg"></li>
                            </ul>
                        </h2>
                        <form name="frm_bid" id="frm_bid" method="post" action="/supplier/auction_do.aspx">
                        <div class="b02_main">
                            <ul style="width:850px;">
                                <li><span><i>*</i>序号：</span><label><input name="Bid_Attachments_Sort" id="Bid_Attachments_Sort" type="text" style="width: 298px;" value="<%=entity.Bid_Attachments_Sort %>" /></label></li>
                                <div class="clear"></div>
                                <li><span><i>*</i>附件名称：</span><label><input name="Bid_Attachments_Name" id="Bid_Attachments_Name" type="text" style="width: 298px;" value="<%=entity.Bid_Attachments_Name %>" /></label></li>
                                <div class="clear"></div>
                                <li><span><i>*</i>文件格式：</span><label><input name="Bid_Attachments_format" id="Bid_Attachments_format" type="text" style="width: 298px;" value="<%=entity.Bid_Attachments_format %>" /></label></li>
                                <div class="clear"></div>
                                <li><span><i>*</i>大小：</span><label><input name="Bid_Attachments_Size" id="Bid_Attachments_Size" type="text" style="width: 298px;" value="<%=entity.Bid_Attachments_Size %>" /></label></li>
                                <div class="clear"></div>
                                <li><span><i>*</i>说明：</span><label><input name="Bid_Attachments_Remarks" id="Bid_Attachments_Remarks" type="text" style="width: 298px;" value="<%=entity.Bid_Attachments_Remarks %>" /></label></li>
                                <div class="clear"></div>
                                <li><span><i>*</i>附件：</span><label><a href="<%=Application["Upload_Server_URL"]+entity.Bid_Attachments_Path %>" target="_blank">点击查看</a></label></li>
                                <div class="clear"></div>

                                <li><span><i>*</i>重新上传：</span><label><iframe id="iframe2" src="<% =Application["Upload_Server_URL"]%>/public/uploadify.aspx?App=Bid&formname=frm_bid&frmelement=Bid_Attachments_Path&rtvalue=1&rturl=<% =Application["Upload_Server_Return_WWW"]%>" width="100%" height="90" frameborder="0" scrolling="no"></iframe></label></li>
                                <div class="clear"></div>
                                <li><a href="javascript:void(0);" onclick="if(Check_Path()){$('#frm_bid').submit();}"  class="a05">保存附件</a></li>
                            </ul>
                            
                            <div class="clear"></div>
                        </div>
                            <input name="action" type="hidden" id="action" value="editAttachments" />
                            <input name="Bid_Attachments_Path" type="hidden" id="Bid_Attachments_Path" value="<%=entity.Bid_Attachments_Path %>" />
                            
                            
                            <input name="Bid_Attachments_ID" type="hidden" id="Bid_Attachments_ID" value="<%=AttID %>" />
                            <input name="Bid_Attachments_BidID" type="hidden" id="Bid_Attachments_BidID" value="<%=entity.Bid_Attachments_BidID %>" />
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
