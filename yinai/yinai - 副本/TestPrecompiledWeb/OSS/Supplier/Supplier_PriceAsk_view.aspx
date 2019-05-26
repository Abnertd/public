<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="System.Collections.Generic" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

    private Supplier myApp;
    private ITools tools;
    private string PriceAsk_ReplyContent, PriceAsk_Title, PriceAsk_Content, PriceAsk_Name, PriceAsk_Phone, Supplier_CompanyName,Product_Name;
    private int PriceAsk_ID, PriceAsk_ProductID, PriceAsk_Quantity, PriceAsk_IsReply;
    private DateTime PriceAsk_DeliveryDate, PriceAsk_ReplyTime, PriceAsk_AddTime;
    private double PriceAsk_Price;
    private SupplierInfo supplierinfo;
    private ProductInfo productinfo;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("249d2ad4-45f4-4945-8e78-d18c79053106");
        myApp = new Supplier();
        tools = ToolsFactory.CreateTools();
        PriceAsk_ID = tools.CheckInt(Request.QueryString["PriceAsk_ID"]);
        SupplierPriceAskInfo entity = myApp.GetSupplierPriceAskByID(PriceAsk_ID);
        if (entity == null)
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        else
        {
            PriceAsk_ProductID = entity.PriceAsk_ProductID;
            PriceAsk_Quantity = entity.PriceAsk_Quantity;
            PriceAsk_ReplyContent = entity.PriceAsk_ReplyContent;
            PriceAsk_ReplyTime = entity.PriceAsk_ReplyTime;
            PriceAsk_Title = entity.PriceAsk_Title;
            PriceAsk_AddTime = entity.PriceAsk_AddTime;
            PriceAsk_Content = entity.PriceAsk_Content;
            PriceAsk_DeliveryDate = entity.PriceAsk_DeliveryDate;
            PriceAsk_IsReply = entity.PriceAsk_IsReply;
            PriceAsk_Name = entity.PriceAsk_Name;
            PriceAsk_Phone = entity.PriceAsk_Phone;
            PriceAsk_Price = entity.PriceAsk_Price;
            supplierinfo = myApp.GetSupplierByID(entity.PriceAsk_MemberID);
            if (supplierinfo != null)
            {
                Supplier_CompanyName = supplierinfo.Supplier_CompanyName;
            }
            else
            {
                Supplier_CompanyName = "";
            }
            productinfo = myApp.GetProductByID(entity.PriceAsk_ProductID);
            if (productinfo != null)
            {
                Product_Name = productinfo.Product_Name;
            }
            else
            {
                Product_Name = "";
            }
        }
        
    }

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>无标题文档</title>
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
      <td class="content_title">询价信息查看</td>
    </tr>
    
      <tr><td class="opt_content">
      <form id="formadd" name="formadd" method="post" action="/supplier/Supplier_PriceAsk_do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table" id="frm_optitem_1">
        <tr>
          <td class="cell_title">产品名称</td>
          <td class="cell_content"><%=Product_Name %></td>
        </tr>
        <tr>
          <td class="cell_title">询价标题</td>
          <td class="cell_content">
          <%=PriceAsk_Title%>
          </td>
        </tr>
        <tr>
          <td class="cell_title">询价人</td>
          <td class="cell_content">
          <%=Supplier_CompanyName%>
          </td>
        </tr>
        <tr>
          <td class="cell_title">联系人</td>
          <td class="cell_content">
          <%=PriceAsk_Name%>
          </td>
        </tr>
        <tr>
          <td class="cell_title">联系手机</td>
          <td class="cell_content">
          <%=PriceAsk_Phone%>
          </td>
        </tr>
        <tr>
          <td class="cell_title">交货时间</td>
          <td class="cell_content">
          <%=PriceAsk_DeliveryDate.ToShortDateString()%>
          </td>
        </tr>
        <tr>
          <td class="cell_title">意向采购数量</td>
          <td class="cell_content">
          <%=PriceAsk_Quantity%>
          </td>
        </tr>
        <tr>
          <td class="cell_title">意向采购单价</td>
          <td class="cell_content">
          <%=Public.DisplayCurrency(PriceAsk_Price)%>
          </td>
        </tr>
        <tr>
          <td class="cell_title">询价内容</td>
          <td class="cell_content">
          <%=PriceAsk_Content%>
          </td>
        </tr>
        <tr>
          <td class="cell_title">询价回复</td>
          <td class="cell_content">
          <textarea name="PriceAsk_ReplyContent" rows="5" cols="50"><%=PriceAsk_ReplyContent %></textarea>
          </td>
        </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="PriceAsk_ID" name="PriceAsk_ID" value="<% =PriceAsk_ID%>" />
            <input type="hidden" id="action" name="action" value="renew" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="history.go(-1);"/></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
  </table>

</div>
</body>
</html>
