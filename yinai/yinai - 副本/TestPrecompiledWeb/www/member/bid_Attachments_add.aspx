<%@ Page Language="C#" %>

<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<% 
    Session["Cur_Position"] = Session["Home_Position"] = "";
    Session["Position"] = "";
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    Member member = new Member();
    Bid Mybid=new Bid();
    int BID = tools.CheckInt(Request["BID"]);
    member.Member_Login_Check("/member/bid_Attachments_add.aspx?BID=" + BID);
    BidInfo entity = Mybid.GetBidByID(BID);
    if(entity!=null)
    {
        if (entity.Bid_MemberID != tools.NullInt(Session["member_id"]) || entity.Bid_MemberID == 0)
        {
            Response.Redirect("/member/bid_list.aspx");
        }
        if(entity.Bid_Status>0||entity.Bid_Type==1)
        {
            Response.Redirect("/member/bid_list.aspx");
        }
    }
    else
    {
        Response.Redirect("/member/bid_list.aspx");
    }
%>


<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%="添加附件 - 我是买家 - " + pub.SEO_TITLE()%></title>
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
        function Check_Path()
        {
            if ($("#Bid_Attachments_Path").val() == "" || $("#Bid_Attachments_Path").val() == null)
            {
                alert("请先上传附件");
                return false;
            }
            else
            {
                return true;
            }

            
        }
    </script>
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
            <div class="position">当前位置 > <a href="/member/index.aspx">我是买家</a> > <a href="/member/bid_list.aspx">招标列表</a> ><span>添加附件</span></div>
            <div class="partd_1">
                <div class="pd_left">
                    <% =member.Member_Left_HTML(1, 2) %>
                </div>
                <div class="pd_right">

                    <div class="blk02">
                        <h2>
                            <ul>
                                <li class="on">添加附件<img src="/images/icon15.jpg"></li>
                            </ul>
                        </h2>
                        <form name="frm_bid" id="frm_bid" method="post" action="/member/bid_do.aspx">
                        <div class="b02_main">
                            <ul style="width:850px;">
<%--                                <li><span><i>*</i>序号：</span><label><input name="Bid_Attachments_Sort" id="Bid_Attachments_Sort" type="text" style="width: 298px;" value="1" /></label></li>
                                <div class="clear"></div>--%>
                                <li><span><i>*</i>附件名称：</span><label><input name="Bid_Attachments_Name" id="Bid_Attachments_Name" type="text" style="width: 298px;" value="" /></label></li>
                                <div class="clear"></div>
<%--                                <li><span><i>*</i>文件格式：</span><label><input name="Bid_Attachments_format" id="Bid_Attachments_format" type="text" style="width: 298px;" value="" /></label></li>
                                <div class="clear"></div>
                                <li><span><i>*</i>大小：</span><label><input name="Bid_Attachments_Size" id="Bid_Attachments_Size" type="text" style="width: 298px;" value="" /></label></li>
                                <div class="clear"></div>--%>
                                <li><span><i></i>说明：</span><label><input name="Bid_Attachments_Remarks" id="Bid_Attachments_Remarks" type="text" style="width: 298px;" value="" /></label></li>
                                <div class="clear"></div>
                                <li><span><i></i>备注：</span><label>支持上传大小3M以内，jpg、gif、png、doc、docx、xls、xlsx格式的文件</label></li>
                                <div class="clear"></div>
                                <li><span><i>*</i>上传附件：</span><label><iframe id="iframe2" src="<% =Application["Upload_Server_URL"]%>/public/uploadify.aspx?App=Bid&formname=frm_bid&frmelement=Bid_Attachments_Path&rtvalue=1&rturl=<% =Application["Upload_Server_Return_WWW"]%>" width="100%" height="90" frameborder="0" scrolling="no"></iframe></label></li>
                                <div class="clear"></div>
                                <li><a href="javascript:void(0);" onclick="if(Check_Path()){$('#frm_bid').submit();}"  class="a05" style="background-color:none;background-image:url(../images/save_buttom.jpg); width:79px;height:28px;"></a></li>
                            </ul>
                            
                            <div class="clear"></div>
                        </div>
                            <input name="action" type="hidden" id="action" value="addAttachments" />
                            <input name="Bid_Attachments_Path" type="hidden" id="Bid_Attachments_Path" value="" />
                            
                            <input name="Bid_Attachments_BidID" type="hidden" id="Bid_Attachments_BidID" value="<%=BID %>" />
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
