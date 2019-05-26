<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">
    
    private ITools tools;
    private Contract myApp;
    int Contract_ID, Contract_Payway_ID;
    string Paymen_Name, Payment_Note, Contract_SN;
    double Payment_Amount, Contract_AllPrice, Contract_PayedAmount;
    
    //会员信息定义
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("f0a5a5f7-c145-4a58-9780-205b406d266e");

        tools = ToolsFactory.CreateTools();
        myApp = new Contract();
       
        Contract_ID = tools.CheckInt(Request["contract_id"]);

        if (Contract_ID == 0 )
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }

        ContractInfo contractinfo = myApp.GetContractByID(Contract_ID);
        if (contractinfo != null)
        {
            if (contractinfo.Contract_Status != 1 || (contractinfo.Contract_Payment_Status > 1))
            {
                Contract_SN = "";
                Contract_ID = 0;
            }
            Contract_SN = contractinfo.Contract_SN;
            Contract_Payway_ID = contractinfo.Contract_Payway_ID;
            Contract_AllPrice = contractinfo.Contract_AllPrice;
            Contract_PayedAmount = myApp.Get_Contract_PayedAmount(contractinfo.Contract_ID);
        }
        else
        {
            Contract_ID = 0;
        }

        if (Contract_ID == 0)
        {
            Public.Msg("error", "错误信息", "无法执行此操作", false, "/contract/Contract_list.aspx");
        }
        
        
    }

    protected void Page_UnLoad(object sender, EventArgs e) {
        tools = null;
        myApp = null;
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
      <td class="content_title">合同支付</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="Form1" name="frm_delivery" method="post" action="contract_do.aspx">
			<table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
            <tr>
				<td width="100" align="right" class="cell_title">合同总价</td>
				<td  class="cell_content"><%=Public.DisplayCurrency(Contract_AllPrice) %></td>
			  </tr>
			<tr>
				<td width="100" align="right" class="cell_title">已支付费用</td>
				<td class="cell_content"><%=Public.DisplayCurrency(Contract_PayedAmount)%></td>
			  </tr>
			  <tr>
				<td width="100" align="right" class="cell_title">待支付金额</td>
				<td class="cell_content"><%=Public.DisplayCurrency(Contract_AllPrice - Contract_PayedAmount)%></td>
			  </tr>
			  <tr>
				<td width="100" align="right" class="cell_title">支付方式</td>
				<td class="cell_content"><%=myApp.Payway_Select("Contract_PaymentID", Contract_Payway_ID)%></td>
			  </tr>
			  <tr>
				<td width="100" align="right" class="cell_title">支付金额</td>
				<td class="cell_content"><input type="text" name="Contract_Payment_Amount" class="txt_border" id="Contract_Payment_Amount" value="<%=Math.Round(Contract_AllPrice - Contract_PayedAmount, 2, MidpointRounding.AwayFromZero)%>" style="width:100px;" /></td>
			  </tr>
			  <tr>
				<td width="100"class="cell_title" align="right">支付备注</td>
				<td class="cell_content"><textarea name="Contract_Payment_Note" id="Contract_Payment_Note" cols="50" rows="5"></textarea></td>
			  </tr>
			  <tr>
				<td width="100"class="cell_title" align="right">上传支付凭据</td>
				<td class="cell_content">
                <iframe id="iframe_upload" src="<% =Application["upload_server_url"]%>/public/uploadify.aspx?App=attachment&formname=Form1&frmelement=Contract_Payment_Attachment&rtvalue=1&rturl=<% =Application["upload_server_return_admin"]%>" width="100%" height="20" frameborder="0" scrolling="no"></iframe>
                </td>
			  </tr>
			  <tr>
				<td></td>
				<td align="left">
				
				</td>
			  </tr>
			</table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input name="Contract_Payment_Attachment" type="hidden" id="Contract_Payment_Attachment" />
            <input name="button" type="submit" class="bt_orange" id="Submit1" value="保存" />
				<input name="action" type="hidden" id="action" value="contract_pay" />
                <input name="contract_sn" type="hidden" id="contract_sn" value="<%=Contract_SN %>" />
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