<%@ Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<%--<%@ Register Src="~/Public/MEM_Top.ascx" TagPrefix="uctop" TagName="top" %>--%>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>

<script runat="server">
    
    Public_Class pub = new Public_Class();
    ITools tools;
    Member member = null;

    private string Purchase_Title, Purchase_Img, Purchase_Unit, Purchase_Intro, Purchase_Site;
    private int Purchase_ID, Purchase_MemberID, Purchase_Status;
    private DateTime Purchase_Validity, Purchase_Addtime;
    private double Purchase_Amount;

    protected void Page_Load(object sender, EventArgs e)
    {
        tools = ToolsFactory.CreateTools();
        member = new Member();
        member.Member_Login_Check("/member/Purchase_list.aspx");
        Purchase_ID = tools.CheckInt(Request.QueryString["Purchase_ID"]);
        MemberPurchaseInfo entity = member.GetMemberPurchaseByID(Purchase_ID);
        if (entity == null)
        {
            pub.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        else
        {
            Purchase_ID = entity.Purchase_ID;
            Purchase_MemberID = entity.Purchase_MemberID;
            Purchase_Title = entity.Purchase_Title;
            Purchase_Img = entity.Purchase_Img;
            Purchase_Amount = entity.Purchase_Amount;
            Purchase_Unit = entity.Purchase_Unit;
            Purchase_Validity = entity.Purchase_Validity;
            Purchase_Intro = entity.Purchase_Intro;
            Purchase_Status = entity.Purchase_Status;
            Purchase_Addtime = entity.Purchase_Addtime;
            Purchase_Site = entity.Purchase_Site;
        }
    }
</script>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="采购信息修改 - 我是买家 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />

    <link href="/css/index.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script src="/scripts/common.js" type="text/javascript"></script>
    <script src="/scripts/jquery-extend-AdAdvance3.js" type="text/javascript"></script>
    <script type="text/javascript" src="/scripts/My97DatePicker/WdatePicker.js"></script>
    <script src="/scripts/supplier.js" type="text/javascript"></script>
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
</head>
<body>

<%--    <uctop:top ID="top1" runat="server" />--%>




    <!--主体 开始-->
    <div class="content02">
        <div class="content02_main">
            <!--位置说明 开始-->
            <div class="position"><a href="/tradeindex.aspx">首页</a> > <a href="/supplier/">采购商用户中心</a> > 辅助功能 > <strong>采购信息修改</strong></div>
            <!--位置说明 结束-->
            <div class="partc">
                <div class="pc_left">
                    <div class="blk12">
                        <% =member.Member_Left_HTML(3, 7) %>
                    </div>
                </div>
                <div class="pc_right">
                    <div class="title03">采购信息修改</div>
                    <div class="blk17">
                        <form name="formadd" id="formadd" method="post" action="/member/fav_do.aspx">
                            <table width="100%" border="0" cellspacing="0" cellpadding="5" id="apply_1_content" class="table_padding_5">
                                <tr>
                                    <td width="92" class="name">采购标题
                                    </td>
                                    <td width="801">
                                        <input name="Purchase_Title" type="text" id="Purchase_Title" style="width: 300px;" class="input01" value="<%=Purchase_Title %>" /><i>*</i>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="92" class="name">采购数量
                                    </td>
                                    <td width="801">
                                        <input name="Purchase_Amount" type="text" id="Purchase_Amount" style="width: 300px;" class="input01" value="<%=Purchase_Amount %>" />
                                        <select name="Purchase_Unit" style="width: 160px;">
                                            <option value="克" <%=Purchase_Unit=="克"?"selected":" " %>>克</option>
                                            <option value="个" <%=Purchase_Unit=="个"?"selected":" " %>>个</option>
                                        </select><i>*</i>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="92" class="name">有效期
                                    </td>
                                    <td width="801">
                                        <input name="Purchase_Validity" type="text" id="Purchase_Validity" style="width: 195px;" onclick="WdatePicker({ skin: 'twoer', dateFmt: 'yyyy-MM-dd' })" class="Wdate input" readonly="readonly" value="<%=Purchase_Validity.ToString("yyyy-MM-dd") %>" /><i>*</i>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="92" class="name">预览图片
                                    </td>
                                    <td width="801">
                                        <iframe id="iframe_upload" src="<% =Application["upload_server_url"]%>/public/uploadify.aspx?App=merchants&formname=formadd&frmelement=Purchase_Img&rtvalue=1&rturl=<% =Application["upload_server_return_WWW"]%>" width="100%" height="90" frameborder="0" scrolling="no"></iframe>
                                        <input name="Purchase_Img" type="hidden" id="Purchase_Img" value="<%=Purchase_Img %>" /><i>*</i>
                                    </td>
                                </tr>
                                <tr id="tr_Purchase_Img" style="display:<%if (Purchase_Img == "") { Response.Write("none"); }%>;">
                                    <td width="92" class="name"></td>
                                    <td width="801">
                                        <img src="<%=pub.FormatImgURL(Purchase_Img,"thumbnail") %>" id="img_Purchase_Img" width="320" height="320" /></td>
                                </tr>
                                <tr>
                                    <td width="92" class="name">采购说明
                                    </td>
                                    <td width="801">
                                        <textarea name="Purchase_Intro" id="Purchase_Intro" cols="80" rows="5"><%=Purchase_Intro %></textarea>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="92" class="name"></td>
                                    <td width="801">
                                        <input name="action" type="hidden" id="action" value="PurchaseEdit">
                                        <input name="Purchase_ID" type="hidden" id="Purchase_ID" value="<%=Purchase_ID %>">
                                        <a href="javascript:void();" onclick="$('#formadd').submit();" class="a11">保 存</a></td>
                                </tr>
                            </table>


                        </form>

                    </div>
                </div>

                <div class="clear"></div>
            </div>
        </div>
    </div>
    <!--主体 结束-->

  <ucbottom:bottom runat="server" ID="Bottom" />

</body>
</html>
