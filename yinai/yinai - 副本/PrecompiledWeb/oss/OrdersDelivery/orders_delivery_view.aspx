<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    
    ITools tools;
    OrdersDelivery myApp;

    string Orders_Delivery_DocNo, Orders_Delivery_Name,Orders_Delivery_companyName,Orders_Delivery_Code, Orders_Delivery_Note, titleName;
    int Orders_Delivery_ID, Orders_Delivery_OrdersID, Orders_Delivery_DeliveryStatus, Orders_Delivery_SysUserID;
    DateTime Orders_Delivery_Addtime;
    double Orders_Delivery_Amount;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("f606309a-2aa9-42e3-9d45-e0f306682a29");
        tools = ToolsFactory.CreateTools();
        myApp = new OrdersDelivery();

        Orders_Delivery_ID = tools.CheckInt(Request.QueryString["Orders_Delivery_ID"]);
        OrdersDeliveryInfo entity = myApp.GetOrdersDeliveryByID(Orders_Delivery_ID);
        if (entity == null) {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        else {
            Orders_Delivery_ID = entity.Orders_Delivery_ID;
            Orders_Delivery_OrdersID = entity.Orders_Delivery_OrdersID;
            Orders_Delivery_DeliveryStatus = entity.Orders_Delivery_DeliveryStatus;
            Orders_Delivery_SysUserID = entity.Orders_Delivery_SysUserID;
            Orders_Delivery_DocNo = entity.Orders_Delivery_DocNo;
            Orders_Delivery_Name = entity.Orders_Delivery_Name;
            Orders_Delivery_companyName = entity.Orders_Delivery_companyName;
            Orders_Delivery_Code = entity.Orders_Delivery_Code;
            Orders_Delivery_Amount = entity.Orders_Delivery_Amount;
            Orders_Delivery_Note = entity.Orders_Delivery_Note;
            Orders_Delivery_Addtime = entity.Orders_Delivery_Addtime;
        }

        if (Orders_Delivery_DeliveryStatus == 5) { titleName = "退货单管理"; }
        else { titleName = "发货单管理"; }
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        myApp = null;
        tools = null;
    }
    
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>无标题文档</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
<script src="/Scripts/common.js" type="text/javascript"></script>
<script src="/Scripts/jquery.js" type="text/javascript"></script>
</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title"><%=titleName%>[<%=Orders_Delivery_DocNo%>]</td>
    </tr>
    <tr>
      <td class="content_content">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">物流费用</td>
          <td class="cell_content t12_red"><% =Public.DisplayCurrency(Orders_Delivery_Amount)%></td>
        </tr>
        <%--<tr>
          <td class="cell_title">配送方式</td>
          <td class="cell_content"><% =Orders_Delivery_Name%></td>
        </tr>--%>
        <tr>
          <td class="cell_title">物流公司</td>
          <td class="cell_content"><% =Orders_Delivery_companyName%></td>
        </tr>
        <tr>
          <td class="cell_title">物流单号</td>
          <td class="cell_content"><% =Orders_Delivery_Code%></td>
        </tr>

        <tr>
          <td class="cell_title">备注</td>
          <td class="cell_content"><% =Orders_Delivery_Note%></td>
        </tr>
        <tr>
          <td class="cell_title">时间</td>
          <td class="cell_content"><% =Orders_Delivery_Addtime%></td>
        </tr>
      </table>
      <%myApp.Delivery_Goods_List(Orders_Delivery_OrdersID, Orders_Delivery_ID, 0); %>
      <div style="text-align:right; margin:10px 0px;"><input name="button" type="submit" class="bt_orange" id="button" value="返回" onclick="history.go(-1);" /></div>
        </td>
    </tr>
  </table>
</div>
</body>
</html>