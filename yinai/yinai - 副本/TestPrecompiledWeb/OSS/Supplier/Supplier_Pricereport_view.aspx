<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

    private Supplier myApp;
    private ITools tools;
    private string PriceReport_ReplyContent, PriceReport_Title, PriceReport_Content, PriceReport_Name, PriceReport_Phone, Supplier_CompanyName, Purchase_Title, PriceReport_Note;
    private int PriceReport_ID, PriceReport_PurchaseID, PriceReport_Quantity, PriceReport_IsReply, PriceReport_MemberID, PriceReport_AuditStatus;
    private DateTime PriceReport_DeliveryDate, PriceReport_ReplyTime, PriceReport_AddTime;
    private double PriceReport_Price;
    private SupplierInfo supplierinfo;
    private SupplierPurchaseInfo purchaseinfo;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("6a12664e-4eeb-4259-b7b5-904044194067");
        myApp = new Supplier();
        tools = ToolsFactory.CreateTools();
        PriceReport_ID = tools.CheckInt(Request.QueryString["PriceReport_ID"]);
        SupplierPriceReportInfo entity = myApp.GetSupplierPriceReportByID(PriceReport_ID);
        if (entity == null)
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        else
        {
            PriceReport_PurchaseID = entity.PriceReport_PurchaseID;
            PriceReport_ReplyContent = entity.PriceReport_ReplyContent;
            PriceReport_ReplyTime = entity.PriceReport_ReplyTime;
            PriceReport_Title = entity.PriceReport_Title;
            PriceReport_AddTime = entity.PriceReport_AddTime;
            PriceReport_DeliveryDate = entity.PriceReport_DeliveryDate;
            PriceReport_IsReply = entity.PriceReport_IsReply;
            PriceReport_Name = entity.PriceReport_Name;
            PriceReport_Phone = entity.PriceReport_Phone;
            PriceReport_MemberID = entity.PriceReport_MemberID;
            PriceReport_AuditStatus = entity.PriceReport_AuditStatus;
            PriceReport_Note = entity.PriceReport_Note;
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
            else
            {
                Supplier_CompanyName = "平台";
            }
            purchaseinfo = myApp.GetSupplierPurchaseByID(entity.PriceReport_PurchaseID);
            if (purchaseinfo != null)
            {
                Purchase_Title = purchaseinfo.Purchase_Title;
            }
            else
            {
                Purchase_Title = "";
            }
        }
        
    }

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>报价信息查看</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
<script src="/Scripts/jquery.js" type="text/javascript"></script>
<script src="/Scripts/common.js" type="text/javascript"></script>
<script src="/Scripts/jquery.zxxbox.3.0.js" type="text/javascript"></script>
<script language="javascript">
    btn_scroll_move();
</script>
</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">报价信息查看</td>
    </tr>
    <tr>
      <td class="content_content">
       <table width="100%" border="0" cellpadding="0" cellspacing="0">
      <tr><td valign="top" height="31" class="opt_foot">
      <table border="0" cellpadding="0" cellspacing="0">
      <tr>
      <td class="opt_gap">&nbsp;</td>
      <td class="opt_cur" id="frm_opt_1">
      <%=Public.Page_ScriptOption("choose_opt(1,2);", "基本信息")%></td>
      <td class="opt_gap">&nbsp;</td>
      <td class="opt_uncur" id="frm_opt_2">
      <%=Public.Page_ScriptOption("choose_opt(2,2);", "报价清单")%></td>
      </tr>
      </table>
      </td></tr>
      <tr><td class="opt_content">
      <form id="formadd" name="formadd" method="post" action="/supplier/Supplier_Pricereport_do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table" id="frm_optitem_1">
        <tr>
          <td class="cell_title">采购申请标题</td>
          <td class="cell_content"><%=Purchase_Title%></td>
        </tr>
        
        <tr>
          <td class="cell_title">报价人</td>
          <td class="cell_content">
          <%=Supplier_CompanyName%>
          </td>
        </tr>
        <tr>
          <td class="cell_title">联系人</td>
          <td class="cell_content">
          <%=PriceReport_Name%>
          </td>
        </tr>
        <tr>
          <td class="cell_title">联系手机</td>
          <td class="cell_content">
          <%=PriceReport_Phone%>
          </td>
        </tr>
        <tr>
          <td class="cell_title">交货时间</td>
          <td class="cell_content">
          <%=PriceReport_DeliveryDate.ToShortDateString()%>
          </td>
        </tr>
        <tr>
          <td class="cell_title">报价备注</td>
          <td class="cell_content">
          <%=PriceReport_Note%>
          </td>
        </tr>
        <tr>
          <td class="cell_title">报价回复</td>
          <td class="cell_content">
          <%if (PriceReport_IsReply ==1 || PriceReport_AuditStatus == 2 || (PriceReport_MemberID == 0 && purchaseinfo.Purchase_TypeID == 0) || (purchaseinfo.Purchase_TypeID == 1 && purchaseinfo.Purchase_SupplierID != 0))
            { %>
              <%=PriceReport_ReplyContent%>
          <%}
            else
            { %>
            <textarea name="PriceReport_ReplyContent" rows="5" cols="50"><%=PriceReport_ReplyContent%></textarea>
          <%} %>
          </td>
        </tr>
      </table>
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table" id="frm_optitem_2" style="display:none;">
       <tr>
          <td class="cell_title">报价清单</td>
          <td class="cell_content">
          <%=myApp.ShowSupplierPriceReportDetail(PriceReport_PurchaseID,PriceReport_ID)%>
          </td>
        </tr>
        </table>
        
        <div class="foot_gapdiv"></div>
      <div class="float_option_div" id="float_option_div">
            <input type="hidden" id="PriceReport_ID" name="PriceReport_ID" value="<% =PriceReport_ID%>" />
            <input type="hidden" id="action" name="action" value="renew" />
              <%if (PriceReport_IsReply ==0 && PriceReport_AuditStatus != 2 && ((PriceReport_MemberID != 0 && purchaseinfo.Purchase_TypeID == 0) || (purchaseinfo.Purchase_TypeID == 1 && PriceReport_MemberID != 0 && purchaseinfo.Purchase_SupplierID == 0)))
                { %>
            <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
            
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="history.go(-1);"/>
    <%}else{ %>
    <input name="button" type="button" class="bt_grey" id="button1" value="返回" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="history.go(-1);"/>
        <%} %></div>
        </form>
        </td>
    </tr>
  </table>
        </td>
    </tr>
  </table>
</div>

</body>
</html>
