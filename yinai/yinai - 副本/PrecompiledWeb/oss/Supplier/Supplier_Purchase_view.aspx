<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="System.Collections.Generic" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

    private Supplier myApp;
    private ITools tools;
    private Addr addr;
    private string Purchase_Address, Purchase_Attachment, Purchase_City, Purchase_County,Purchase_State,Purchase_Title,Purchase_Intro,Supplier_CompanyName,cateName;
    private int Supplier_Purchase_ID, Purchase_TypeID, Purchase_SupplierID, Purchase_IsPublic;
    private DateTime Purchase_DeliveryTime, Purchase_ValidDate;
    private SupplierInfo supplierinfo;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("c197743d-e397-4d11-b6fc-07d1d24aa774");
        myApp = new Supplier();
        tools = ToolsFactory.CreateTools();
        addr = new Addr();
        int cateID = 0;
        Supplier_Purchase_ID = tools.CheckInt(Request.QueryString["Purchase_ID"]);
        Session["MessageSupplierInfo"] = myApp.GetSupplierPurchasePrivate(Supplier_Purchase_ID);
        SupplierPurchaseInfo entity = myApp.GetSupplierPurchaseByID(Supplier_Purchase_ID);
        if (entity == null)
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        else
        {
            Purchase_Address = entity.Purchase_Address;
            Purchase_Attachment = entity.Purchase_Attachment;
            Purchase_SupplierID = entity.Purchase_SupplierID;
            Purchase_City = entity.Purchase_City;
            Purchase_County = entity.Purchase_County;
            Purchase_DeliveryTime = entity.Purchase_DeliveryTime;
            Purchase_Intro = entity.Purchase_Intro;
            Purchase_State = entity.Purchase_State;
            Purchase_Title = entity.Purchase_Title;
            Purchase_TypeID = entity.Purchase_TypeID;
            Purchase_ValidDate = entity.Purchase_ValidDate;
            Purchase_IsPublic=entity.Purchase_IsPublic;
            cateID = entity.Purchase_CateID;
            supplierinfo = myApp.GetSupplierByID(entity.Purchase_SupplierID);
            if (supplierinfo != null)
            {
                Supplier_CompanyName = supplierinfo.Supplier_CompanyName;
            }
            else
            {
                Supplier_CompanyName = "";
            }
            Session["MessageSupplierInfo"] = myApp.GetSupplierPurchasePrivate(Supplier_Purchase_ID);

            SupplierPurchaseCategoryInfo category = myApp.GetSupplierPurchaseCategoryByID(cateID);
            if (category != null)
            {
                cateName = category.Cate_Name;
            }
        }
        
    }

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>采购申请查看</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
<script src="/Scripts/jquery.js" type="text/javascript"></script>
<script src="/Scripts/common.js" type="text/javascript"></script>
<script src="/Scripts/jquery.zxxbox.3.0.js" type="text/javascript"></script>
<script language="javascript">
btn_scroll_move();
</script>
<script type="text/javascript">
<!--

    function SelectSupplier() {
        window.open("selectsupplier.aspx?timer=" + Math.random(), "运营支撑系统", "height=560, width=600, toolbar=no, menubar=no, scrollbars=yes, resizable=no, location=no, status=no,top=100,left=300")
    }
    function supplier_del(supplier_id) {
        $.ajax({
            url: encodeURI("supplier_do.aspx?action=supplier_del&supplier_id=" + supplier_id + "&timer=" + Math.random()),
            type: "get",
            global: false,
            async: false,
            dataType: "html",
            success: function (data) {
                $("#yhnr").html(data);
            },
            error: function () {
                alert("Error Script");
            }
        });
    }
//-->
</script>
</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">采购申请查看</td>
    </tr>
    <tr>
      <td class="content_content">
       <table width="100%" border="0" cellpadding="0" cellspacing="0">
      <tr><td valign="top" height="31" class="opt_foot">
      <table border="0" cellpadding="0" cellspacing="0">
      <tr>
      <td class="opt_gap">&nbsp;</td>
      <td class="opt_cur" id="frm_opt_1">
      <%=Public.Page_ScriptOption("choose_opt(1,3);", "基本信息")%></td>
      <td class="opt_gap">&nbsp;</td>
      <td class="opt_uncur" id="frm_opt_2">
      <%=Public.Page_ScriptOption("choose_opt(2,3);", "采购清单")%></td>
      <td class="opt_gap">&nbsp;</td>
      <td class="opt_uncur" id="frm_opt_3">
      <%=Public.Page_ScriptOption("choose_opt(3,3);", "报价范围")%></td>
      </tr>
      </table>
      </td></tr>
      <tr><td class="opt_content">
      <form id="formadd" name="formadd" method="post" action="/supplier/Supplier_Purchase_do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table" id="frm_optitem_1">
        <tr>
          <td class="cell_title">采购标题</td>
          <td class="cell_content"><%=Purchase_Title %></td>
        </tr>
        <tr>
          <td class="cell_title">采购类型</td>
          <td class="cell_content">
          <%if (Purchase_TypeID == 0)
            {
                Response.Write("定制采购");
            }
            else
            {
                Response.Write("代理采购");
            } %>
          </td>
        </tr>
         <tr>
          <td class="cell_title">采购分类</td>
          <td class="cell_content">
          <%=cateName %>
          </td>
        </tr>
        <tr>
          <td class="cell_title">发布人</td>
          <td class="cell_content">
          <%=Supplier_CompanyName%>
          </td>
        </tr>
        <tr>
          <td class="cell_title">交换时间</td>
          <td class="cell_content">
          <%=Purchase_DeliveryTime.ToShortDateString()%>
          </td>
        </tr>
        <tr>
          <td class="cell_title">交换地点</td>
          <td class="cell_content">
          <%=addr.DisplayAddress(Purchase_State,Purchase_City,Purchase_County)+" "+ Purchase_Address%>
          </td>
        </tr>
        <tr>
          <td class="cell_title">有效时间</td>
          <td class="cell_content">
          <%=Purchase_ValidDate.ToShortDateString()%>
          </td>
        </tr>
        <tr>
          <td class="cell_title">附件路径</td>
          <td class="cell_content">
          <%if(Purchase_Attachment.Length>0)
            {
                Response.Write(Application["Upload_Server_URL"] + Purchase_Attachment);
            }%>
          </td>
        </tr>
        <tr>
          <td class="cell_title">采购描述</td>
          <td class="cell_content">
          <%=Purchase_Intro%>
          </td>
        </tr>
      </table>
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table" id="frm_optitem_2" style="display:none;">
       <tr>
          <td class="cell_title">采购清单</td>
          <td class="cell_content">
          <%=myApp.ShowSupplierPurchaseDetail(Supplier_Purchase_ID) %>
          </td>
        </tr>
        </table>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table" id="frm_optitem_3" style="display:none;">
       <tr>
          <td class="cell_title">报价范围</td>
          <td class="cell_content">
          <input type="radio" name="Purchase_IsPublic" value="1" <% =Public.CheckedRadio(Purchase_IsPublic.ToString(), "1")%> /> 公开报价
          <input type="radio" name="Purchase_IsPublic" value="0" <% =Public.CheckedRadio(Purchase_IsPublic.ToString(), "0")%>/> 指定卖家报价
          </td>
        </tr>
        <tr>
          <td class="cell_title">指定卖家</td>
          <td class="cell_content">
          <div id="yhnr"><% =myApp.ShowSupplier()%></div>
          </td>
        </tr>
        </table>
        <div class="foot_gapdiv"></div>
      <div class="float_option_div" id="float_option_div">
            <input type="hidden" id="Purchase_ID" name="Purchase_ID" value="<% =Supplier_Purchase_ID%>" />
            <input type="hidden" id="action" name="action" value="update" />
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
