<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">

    private Supplier myApp;
    private ITools tools;
    private Addr addr;
    private OrdersProcess MyApp;
    private string PriceReport_ReplyContent, PriceReport_Title, PriceReport_Content, PriceReport_Name, PriceReport_Phone, Supplier_CompanyName, Purchase_Title;
    private int PriceReport_ID, PriceReport_PurchaseID, PriceReport_Quantity, PriceReport_IsReply, PriceReport_MemberID, PriceReport_AuditStatus;
    private DateTime PriceReport_DeliveryDate, PriceReport_ReplyTime, PriceReport_AddTime;
    private double PriceReport_Price;
    private SupplierInfo supplierinfo;
    private SupplierPurchaseInfo purchaseinfo;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("9d8d62af-29b1-4302-957c-268732fc15b4");
        MyApp = new OrdersProcess();
        myApp = new Supplier();
        tools = ToolsFactory.CreateTools();
        addr = new Addr();
        PriceReport_ID = tools.CheckInt(Request.QueryString["PriceReport_ID"]);
        SupplierPriceReportInfo entity = myApp.GetSupplierPriceReportByID(PriceReport_ID);
        if (entity == null)
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        else
        {
            if (entity.PriceReport_AuditStatus != 1)
            {
                Public.Msg("error", "错误信息", "当前报价信息未审核通过！", false, "{back}");
                Response.End();
            }
            if (entity.PriceReport_MemberID == 0)
            {
                Public.Msg("error", "错误信息", "不能针对平台报价进行采购！", false, "{back}");
                Response.End();
            }
            PriceReport_PurchaseID = entity.PriceReport_PurchaseID;

            
            if (entity.PriceReport_MemberID > 0)
            {
                supplierinfo = myApp.GetSupplierByID(entity.PriceReport_MemberID);
                if (supplierinfo != null)
                {
                    Supplier_CompanyName = supplierinfo.Supplier_CompanyName;
                }
                else
                {
                    Supplier_CompanyName = "";
                }
            }
            purchaseinfo = myApp.GetSupplierPurchaseByID(entity.PriceReport_PurchaseID);
            if (purchaseinfo != null)
            {
                Purchase_Title = purchaseinfo.Purchase_Title;
                if (purchaseinfo.Purchase_Trash == 1 || purchaseinfo.Purchase_Status != 2 || purchaseinfo.Purchase_ValidDate < tools.NullDate(DateTime.Now.ToShortDateString()))
                {
                    Public.Msg("error", "错误信息", "无效的采购申请信息！", false, "{back}");
                    Response.End();
                }
            }
            else
            {
                Public.Msg("error", "错误信息", "无效的采购申请信息！", false, "{back}");
                Response.End();
            }
        }
        Session["total_price"] = 0;
        
    }

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>无标题文档</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
<script src="/Scripts/jquery.js" type="text/javascript"></script>
<script src="/Scripts/common.js" type="text/javascript"></script>
<link type="text/css" href="/Scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
<script src="/Scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
<script src="/Scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script>
<script src="/Public/ckeditor/ckeditor.js" type="text/javascript"></script>
<link href="/Scripts/jqGrid/css/ui.jqgrid.css" rel="stylesheet" type="text/css" />
<script src="/Scripts/jqGrid/grid.locale-zh_CN.js" type="text/javascript"></script>
<script src="/Scripts/jqGrid/jquery.jqGrid.min.js" type="text/javascript"></script>

<style type="text/css">
    
    .tablebg_green{ background:#390;}
    .tablebg_green td{ background:#FFF;}
</style>

</head>
<body>

<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">创建采购订单</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="frm_order" name="frm_order" method="post" action="purchase_orders_add2.aspx">
			<table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
			  <tr>
				<td width="100" height="23" align="right" class="cell_title">选择商品</td>
				<td align="left" class="cell_content">
				<%
                    Response.Write(MyApp.Purchase_Goods_List(PriceReport_PurchaseID, PriceReport_ID,  true));
			                 %>
				</td>
			  </tr>
			</table>
			<table width="100%" border="0" cellspacing="0" cellpadding="5">
              <tr>
                <td align="right">
                <input name="button" type="submit" class="bt_orange" id="Submit2" value="确认" />
				    <input name="PriceReport_ID" type="hidden" id="PriceReport_ID" value="<%=PriceReport_ID %>" />
                 </td>
              </tr>
            </table>
			</form>
      </td>
    </tr>
  </table>
</div>



</body>
</html>