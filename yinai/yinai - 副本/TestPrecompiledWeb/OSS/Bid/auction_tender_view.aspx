<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="System.Collections.Generic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    
private Bid myApp;
private ITools tools;

private string Tender_SN;
private int Tender_ID,Tender_SupplierID,Tender_BidID,Tender_IsWin,Tender_Status,Tender_IsRefund,Tender_IsProduct;
private DateTime Tender_Addtime;
private double Tender_AllPrice;
private IList<TenderProductInfo> TenderProducts = null;
protected void Page_Load(object sender, EventArgs e)
{
    Public.CheckLogin("db8de73b-9ac0-476e-866e-892dd35589c5");
    myApp = new Bid();
    tools = ToolsFactory.CreateTools();
    
    Tender_ID = tools.CheckInt(Request.QueryString["tenderid"]);
    TenderInfo entity = myApp.GetTenderByID(Tender_ID);
    if (entity == null) {
        Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
        Response.End();
    } else {
        Tender_ID = entity.Tender_ID;
Tender_SupplierID = entity.Tender_SupplierID;
Tender_BidID = entity.Tender_BidID;
Tender_Addtime = entity.Tender_Addtime;
Tender_IsWin = entity.Tender_IsWin;
Tender_Status = entity.Tender_Status;
Tender_AllPrice = entity.Tender_AllPrice;
Tender_IsRefund = entity.Tender_IsRefund;
Tender_SN = entity.Tender_SN;
Tender_IsProduct = entity.Tender_IsProduct;
TenderProducts = entity.TenderProducts;
    }
}

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>无标题文档</title>
    <link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
    <script src="/Scripts/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" src="/Public/kindeditor/kindeditor-all.js"></script>
    <script type="text/javascript" src="/Public/kindeditor/lang/zh_CN.js"></script>
    <script src="/Scripts/common.js" type="text/javascript"></script>
    <script>
        function Audit(obj) {
            $("#action").val(obj);
            $('#formadd').submit();
        }

    </script>
</head>
<body>
    <div class="content_div">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
            <tr>
                <td class="content_title">竞价详情</td>
            </tr>
            <tr>
                <td valign="top" height="31" class="opt_foot">
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="opt_gap">&nbsp;</td>
                            <td class="opt_cur" id="frm_opt_1"><%=Public.Page_ScriptOption("choose_opt(1,2);", "基本信息")%></td>


                            <td class="opt_gap">&nbsp;</td>
                            <td class="opt_uncur" id="frm_opt_2"><%=Public.Page_ScriptOption("choose_opt(2,2);", "竞价清单")%></td>


                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="content_content">
                    <form id="formadd" name="formadd" method="post" action="/bid/bid_do.aspx">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table" id="frm_optitem_1">
                        <tr>
                            <td class="cell_title">竞价单位</td>
                            <td class="cell_content"><% =myApp.SupplierName(Tender_SupplierID)%></td>
                        </tr>
                        <tr>
                            <td class="cell_title">竞价总金额</td>
                            <td class="cell_content"><% =Public.DisplayCurrency(Tender_AllPrice)%></td>
                        </tr>

                        <tr>
                            <td class="cell_title">竞价时间</td>
                            <td class="cell_content"><%=Tender_Addtime %></td>
                        </tr>

                         <tr>
                            <td class="cell_title">是否中标</td>
                            <td class="cell_content"><%=myApp.IsWin(Tender_Status,Tender_IsWin)%></td>
                        </tr>

                        
                    </table>

                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table" id="frm_optitem_2" style="display: none;">
                        <tr>
                       <%--     <td class="cell_title" style="text-align:center;">产品编号</td>--%>
                            <td class="cell_title" style="text-align:center;">产品名称</td>
                            <td class="cell_title" style="text-align:center;">型号规格</td>
                          <%--  <td class="cell_title" style="text-align:center;">品牌</td>--%>
                            <td class="cell_title" style="text-align:center;">计量单位</td>
                            <td class="cell_title" style="text-align:center;">产品数量</td>
                            <td class="cell_title" style="text-align:center;">物流信息</td>
                            <td class="cell_title" style="text-align:center;">备注</td>
                            <td class="cell_title" style="text-align:center;">起标价格</td>
                            <td class="cell_title" style="text-align:center;">单价竞价</td>

                        </tr>
                        <%myApp.TenderProductList(Tender_BidID, TenderProducts, Tender_IsWin, 1); %>
                    </table>
                                <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
             <input name="button" type="button" class="bt_grey" id="button" value="返回" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="history.go(-1);"/></td>
          </tr>
        </table>
                        </form>
                </td>
            </tr>
        </table>

    </div>
</body>
</html>
