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
    private Addr Addr;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("59ac1c26-ba95-42c9-b418-fae8465f6e94");
        myApp = new Supplier();
        tools = ToolsFactory.CreateTools();
        Addr = new Addr();
        
        
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
<script type="text/javascript" src="/Public/ckeditor/ckeditor.js"></script>
<script language="javascript">
    btn_scroll_move();
</script>
</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">创建代理采购信息</td>
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
      <%=Public.Page_ScriptOption("choose_opt(2,2);", "采购清单")%></td>
      </tr>
      </table>
      </td></tr>
      <tr><td class="opt_content">
      <form id="formadd" name="formadd" method="post" action="/supplier/Supplier_Purchase_do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table" id="frm_optitem_1">
        <tr>
          <td class="cell_title">采购类型</td>
          <td class="cell_content">代理采购</td>
        </tr>
        <tr>
          <td class="cell_title">采购标题</td>
          <td class="cell_content">
          <input name="Purchase_Title" type="text" id="Purchase_Title" style="width:200px;" /> 
          <span class="t12_red">*</span>
          </td>
        </tr>
        <tr>
          <td class="cell_title">采购分类</td>
          <td class="cell_content">
          <span id="main_cate"><%=myApp.Purchase_Category_Select(0, "main_cate")%></span>
          <span class="t12_red">*</span>
          </td>
        </tr>
        <tr>
          <td class="cell_title">交货时间</td>
          <td class="cell_content">
          <input type="text" name="Purchase_DeliveryTime" id="Purchase_DeliveryTime"
                                            maxlength="10" readonly="readonly" value="" style="width:80px;" />
          <span class="t12_red">*</span>
          <script type="text/javascript">
              $(document).ready(function () {
                  $("#Purchase_DeliveryTime").datepicker({ numberOfMonths: 1 });
              });
          	</script>
          </td>
        </tr>
        <tr>
          <td class="cell_title">交货地点</td>
          <td class="cell_content">
          <span id="textDiv"><%=Addr.SelectAddress("textDiv", "Purchase_State", "Purchase_City", "Purchase_County", "", "", "")%></span>
          <input name="Purchase_Address" type="text" id="Purchase_Address" style="width:200px;" /> 
            <span class="t12_red">*</span>
          </td>
        </tr>
        <tr>
          <td class="cell_title">有效期</td>
          <td class="cell_content">
         <input type="text" name="Purchase_ValidDate" id="Purchase_ValidDate"
                                            maxlength="10" readonly="readonly" value="" style="width:80px;" />
          <script type="text/javascript">
              $(document).ready(function () {
                  $("#Purchase_ValidDate").datepicker({ numberOfMonths: 1 });
              });
          	</script>
            <span class="t12_red">*</span>
          </td>
          <tr>
          <td class="cell_title">上传附件</td>
          <td class="cell_content">
         <iframe id="iframe1" src="<% =Application["Upload_Server_URL"]%>/public/uploadify.aspx?App=attachment&formname=formadd&frmelement=Purchase_Attachment&rtvalue=1&rturl=<% =Application["upload_server_return_admin"]%>"
                                            width="242" height="100" frameborder="0" scrolling="no" style="margin-left: 10px;">
                                        </iframe>
          </td>
          <tr>
          <td class="cell_title">附件路径</td>
          <td class="cell_content">
         <input name="Purchase_Attachment" type="text" id="Purchase_Attachment" readonly style="width:200px;" /> 
        
          </td>
          <tr>
          <td class="cell_title">上传图片</td>
          <td class="cell_content">
         <iframe id="iframe2" src="<% =Application["Upload_Server_URL"]%>/public/uploadify.aspx?App=content&formname=formadd&frmelement=Purchase_Intro&rtvalue=1&rturl=<% =Application["upload_server_return_admin"]%>"
                                            width="242" height="100" frameborder="0" scrolling="no">
                                        </iframe>
          </td>
          <tr>
          <td class="cell_title">采购描述</td>
          <td class="cell_content">
          <textarea cols="80" id="Purchase_Intro" name="Purchase_Intro" rows="16"></textarea>
                                        <script type="text/javascript">
                                            CKEDITOR.replace('Purchase_Intro');
                                        </script>
          </td>
        </tr>
      </table>
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table" id="frm_optitem_2" style="display:none;">
       <tr>
          <td class="cell_title">报价清单</td>
          <td class="cell_content">
         <%=myApp.ShowSupplierPurchaseForm(0)%>
          <input type="hidden" value="5" name="Purchase_Amount" />
         

          </td>
        </tr>
        </table>
        
        <div class="foot_gapdiv"></div>
      <div class="float_option_div" id="float_option_div">
       
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
