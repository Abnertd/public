<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">
    
    private ITools tools;
    private Contract myApp;
    int Contract_ID, Divided_ID;
    string Paymen_Name, Payment_Note, Contract_SN;
    double Payment_Amount;
    
    //会员信息定义
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("all");

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
            Contract_SN=contractinfo.Contract_SN;
            if (contractinfo.Contract_Source == 0)
            {
                Public.Msg("error", "错误信息", "该合同无法执行此操作", false, "{back}");
                Response.End();
            }
            IList<OrdersInfo> ordersinfos = myApp.GetOrderssByContractID(contractinfo.Contract_ID);
            if (contractinfo.Contract_Status != 0 || ordersinfos != null)
            {
                Public.Msg("error", "错误信息", "该合同无法执行此操作,请检查合同状态及是否包含订单", false, "{back}");
                Response.End();
            }
        }
        else
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
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
      <td class="content_title">合同[<%=Contract_SN%>]取消</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="Form1" name="frm_delivery" method="post" action="contract_do.aspx">
			<table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
			<tr>
				<td width="100" height="23" align="right" class="cell_title">取消原因</td>
				<td align="left" class="cell_content"><textarea name="contract_close_note" id="contract_close_note" cols="45" rows="5"></textarea></td>
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
            <input name="button" type="submit" class="bt_orange" id="Submit1" value="保存" />
				<input type="hidden" name="Contract_SN" value="<%=Contract_SN%>" /> 
				<input name="action" type="hidden" id="action" value="contract_cancel" />
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