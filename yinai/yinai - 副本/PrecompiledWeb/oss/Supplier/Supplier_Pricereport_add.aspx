<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

    private Supplier myApp;
    private ITools tools;
    private string Purchase_Title;
    private int Purchase_ID;
    private SupplierPurchaseInfo purchaseinfo;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("6a12664e-4eeb-4259-b7b5-904044194067");
        myApp = new Supplier();
        tools = ToolsFactory.CreateTools();
        Purchase_ID = tools.CheckInt(Request.QueryString["Purchase_ID"]);
        purchaseinfo = myApp.GetSupplierPurchaseByID(Purchase_ID);
        if (purchaseinfo == null)
        {

            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        else
        {
            Purchase_Title = purchaseinfo.Purchase_Title;
            if (purchaseinfo.Purchase_Status != 2)
            {
                Public.Msg("error", "错误信息", "该信息未通过审核", false, "{back}");
                Response.End();
            }
            if (purchaseinfo.Purchase_IsActive != 1)
            {
                Public.Msg("error", "错误信息", "该信息已被挂起", false, "{back}");
                Response.End();
            }
            if (purchaseinfo.Purchase_SupplierID == 0)
            {
                Public.Msg("error", "错误信息", "不能对平台发布申请进行报价", false, "{back}");
                Response.End();
            }
            if ( purchaseinfo.Purchase_Trash != 0)
            {
                Public.Msg("error", "错误信息", "该信息已被移至回收站", false, "{back}");
                Response.End();
            }
            if (purchaseinfo.Purchase_ValidDate < tools.NullDate(DateTime.Now.ToShortDateString()))
            {
                Public.Msg("error", "错误信息", "该信息已过期", false, "{back}");
                Response.End();
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
<link type="text/css" href="/Scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
<script src="/Scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
<script src="/Scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script>
<script language="javascript">
    btn_scroll_move();
</script>
</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">创建报价信息</td>
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
          <td class="cell_title">报价标题</td>
          <td class="cell_content">
          <input name="PriceReport_Title" type="text" id="PriceReport_Title" style="width:200px;" /> 
          <span class="t12_red">*</span>
          </td>
        </tr>
        <tr>
          <td class="cell_title">联系人</td>
          <td class="cell_content">
          <input name="PriceReport_Name" type="text" id="PriceReport_Name" style="width:200px;" />
          <span class="t12_red">*</span>
          </td>
        </tr>
        <tr>
          <td class="cell_title">联系手机</td>
          <td class="cell_content">
          <input name="PriceReport_Phone" type="text" id="PriceReport_Phone" style="width:200px;" />
          <span class="t12_red">*</span>
          </td>
        </tr>
        <tr>
          <td class="cell_title">交货时间</td>
          <td class="cell_content">
          <input name="PriceReport_DeliveryDate" readonly type="text" id="PriceReport_DeliveryDate" style="width:80px;" />
          <script type="text/javascript">
              $(document).ready(function () {
                  $("#PriceReport_DeliveryDate").datepicker({ numberOfMonths: 1 });
              });
          	</script>
            <span class="t12_red">*</span>
          </td>
        </tr>
      </table>
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table" id="frm_optitem_2" style="display:none;">
       <tr>
          <td class="cell_title">报价清单</td>
          <td class="cell_content">
          <%=myApp.ShowSupplierPriceReportDetailForm(Purchase_ID)%>
          </td>
        </tr>
        </table>
        
        <div class="foot_gapdiv"></div>
      <div class="float_option_div" id="float_option_div">
            <input type="hidden" id="Purchase_ID" name="Purchase_ID" value="<% =Purchase_ID%>" />
            <input type="hidden" id="action" name="action" value="new" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="history.go(-1);"/>
    </div>
        
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
