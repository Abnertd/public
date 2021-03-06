﻿<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

    private Bid myApp;
    private ITools tools;

    private string Bid_Product_Code, Bid_Product_Name, Bid_Product_Spec, Bid_Product_Brand, Bid_Product_Unit, Bid_Product_Delivery, Bid_Product_Remark, Bid_Product_Img;
    private int Bid_Product_ID, Bid_BidID, Bid_Product_Sort, Bid_Product_Amount, Bid_Product_ProductID;

    private double Bid_Product_StartPrice;

    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("3c1e3fb0-9ad4-4a25-afa3-5ebc7b706039");
        myApp = new Bid();
        tools = ToolsFactory.CreateTools();

        Bid_Product_ID = tools.CheckInt(Request.QueryString["productID"]);
        BidProductInfo entity = myApp.GetBidProductByID(Bid_Product_ID);
        if (entity == null)
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        else
        {
            Bid_Product_ID = entity.Bid_Product_ID;
            Bid_BidID = entity.Bid_BidID;
            Bid_Product_Sort = entity.Bid_Product_Sort;
            Bid_Product_Code = entity.Bid_Product_Code;
            Bid_Product_Name = entity.Bid_Product_Name;
            Bid_Product_Spec = entity.Bid_Product_Spec;
            Bid_Product_Brand = entity.Bid_Product_Brand;
            Bid_Product_Unit = entity.Bid_Product_Unit;
            Bid_Product_Amount = entity.Bid_Product_Amount;
            Bid_Product_Delivery = entity.Bid_Product_Delivery;
            Bid_Product_Remark = entity.Bid_Product_Remark;
            Bid_Product_StartPrice = entity.Bid_Product_StartPrice;
            Bid_Product_Img = entity.Bid_Product_Img;
            Bid_Product_ProductID = entity.Bid_Product_ProductID;

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
    <link href="/scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
    <script src="/scripts/jquery-ui/jquery-ui.js"></script>
    <script src="/scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js"></script>
</head>
<body>
    <div class="content_div">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
            <tr>
                <td class="content_title">修改招标商品</td>
            </tr>
            <tr>
                <td class="content_content">
                    <form id="formadd" name="formadd" method="post" action="bid_do.aspx">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
<%--                            <tr>
                                <td class="cell_title">序号</td>
                                <td class="cell_content">
                                    <input name="Bid_Product_Sort" type="text" id="Bid_Product_Sort" size="50" maxlength="100" value="<%=Bid_Product_Sort %>" /></td>
                            </tr>--%>

                           <%-- <tr>
                                <td class="cell_title">物料编号</td>
                                <td class="cell_content">
                                    <input name="Bid_Product_Code" type="text" id="Bid_Product_Code" size="50" maxlength="100" value="<%=Bid_Product_Code %>" /></td>
                            </tr>--%>
                            <tr>
                                <td class="cell_title">物料名称</td>
                                <td class="cell_content">
                                    <input name="Bid_Product_Name" type="text" id="Bid_Product_Name" size="50" maxlength="100" value="<%=Bid_Product_Name %>" /></td>
                            </tr>
                            <tr>
                                <td class="cell_title">型号规格</td>
                                <td class="cell_content">
                                    <input name="Bid_Product_Spec" type="text" id="Bid_Product_Spec" size="50" maxlength="100" value="<%=Bid_Product_Spec %>" /></td>
                            </tr>
                           <%-- <tr>
                                <td class="cell_title">品牌</td>
                                <td class="cell_content">
                                    <input name="Bid_Product_Brand" type="text" id="Bid_Product_Brand" size="50" maxlength="100" value="<%=Bid_Product_Brand %>" /></td>
                            </tr>--%>
                            <tr>
                                <td class="cell_title">计量单位</td>
                                <td class="cell_content">
                                    <input name="Bid_Product_Unit" type="text" id="Bid_Product_Unit" size="50" maxlength="100" value="<%=Bid_Product_Unit %>" /></td>
                            </tr>

                            <tr>
                                <td class="cell_title">采购数量</td>
                                <td class="cell_content">
                                    <input name="Bid_Product_Amount" type="text" id="Bid_Product_Amount" size="50" maxlength="100" value="<%=Bid_Product_Amount %>" /></td>
                            </tr>
                            <tr>
                                <td class="cell_title">物流信息</td>
                                <td class="cell_content">
                                    <input name="Bid_Product_Delivery" type="text" id="Bid_Product_Delivery" size="50" maxlength="100" value="<%=Bid_Product_Delivery %>" /></td>
                            </tr>
                            <tr>
                                <td class="cell_title">备注</td>
                                <td class="cell_content">
                                    <input name="Bid_Product_Remark" type="text" id="Bid_Product_Remark" size="50" maxlength="100" value="<%=Bid_Product_Remark %>" /></td>
                            </tr>
                        </table>
                        <table width="100%" border="0" cellspacing="0" cellpadding="5">
                            <tr>
                                <td align="right">
                                    
                                    <input type="hidden" id="ProductID" name="ProductID" value="<%=Bid_Product_ID %>" />
                                     <input type="hidden" id="Bid_BidID" name="Bid_BidID" value="<%=Bid_BidID %>" />
                                    <input type="hidden" id="action" name="action" value="renewProduct" />
                                    <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
                                    <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location = 'bid_edit.aspx?list=1&BID=<%=Bid_BidID%>';" /></td>
                            </tr>
                        </table>
                    </form>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
