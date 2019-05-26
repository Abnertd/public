<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    
    ITools tools;
    Contract myApp;
    
    string Orders_Payment_DocNo, Orders_Payment_Name, Orders_Payment_Note, titleName;
    int Orders_Payment_ID, Orders_Payment_OrdersID, Orders_Payment_PaymentStatus, Orders_Payment_SysUserID;
    DateTime Orders_Payment_Addtime;
    double Orders_Payment_Amount;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("all");
        
        tools = ToolsFactory.CreateTools();
        myApp = new Contract();

        Orders_Payment_ID = tools.CheckInt(Request.QueryString["Contract_Payment_ID"]);
        ContractPaymentInfo entity = myApp.GetContractPaymentByID(Orders_Payment_ID);
        if (entity == null) {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        else {
            Orders_Payment_ID = entity.Contract_Payment_ID;
            Orders_Payment_OrdersID = entity.Contract_Payment_ContractID;
            Orders_Payment_PaymentStatus = entity.Contract_Payment_PaymentStatus;
            Orders_Payment_SysUserID = entity.Contract_Payment_SysUserID;
            Orders_Payment_DocNo = entity.Contract_Payment_DocNo;
            Orders_Payment_Name = entity.Contract_Payment_Name;
            Orders_Payment_Amount = entity.Contract_Payment_Amount;
            Orders_Payment_Note = entity.Contract_Payment_Note;
            Orders_Payment_Addtime = entity.Contract_Payment_Addtime;
        }

        titleName = "付款单管理"; 
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
      <td class="content_title"><%=titleName%>[<%=Orders_Payment_DocNo%>]</td>
    </tr>
    <tr>
      <td class="content_content">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">支付金额</td>
          <td class="cell_content t12_red"><% =Public.DisplayCurrency(Orders_Payment_Amount)%></td>
        </tr>
        <tr>
          <td class="cell_title">支付方式</td>
          <td class="cell_content"><% =Orders_Payment_Name%></td>
        </tr>
        <tr>
            <td class="cell_title">支付状态</td>
          <td class="cell_content"><%=myApp.ContractPaymentsStatus(Orders_Payment_PaymentStatus)%></td>
        </tr>
        <tr>
          <td class="cell_title">支付备注</td>
          <td class="cell_content"><% =Orders_Payment_Note%></td>
        </tr>
        <tr>
        </tr>
        <tr>
          <td class="cell_title">支付时间</td>
          <td class="cell_content"><% =Orders_Payment_Addtime%></td>
        </tr>
      </table>
      <div style="text-align:right; margin:10px 0px;"><input name="button" type="submit" class="bt_orange" id="button" value="返回" onclick="history.go(-1);" /></div>
        </td>
    </tr>
  </table>
</div>
</body>
</html>