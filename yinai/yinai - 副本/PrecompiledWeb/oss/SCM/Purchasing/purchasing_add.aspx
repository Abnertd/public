<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.Util.SQLHelper" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    
    int Purchasing_ID, Purchasing_Type, Purchasing_DepotID, Purchasing_SupplierID, Purchasing_Amount, Purchasing_Checkout;
    string Purchasing_ProductCode, Purchasing_BatchNumber, Purchasing_Operator, Purchasing_Note, action;
    double Purchasing_Price, Purchasing_TotalPrice;
    DateTime Purchasing_Tradetime;

    string titleName = "进货单管理";
    
    private ITools tools;
    private ISQLHelper DBHelper;

    private SCMDepot depot;
    private SCMSupplier supplier;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        
        
        tools = ToolsFactory.CreateTools();

        depot = new SCMDepot();
        supplier = new SCMSupplier();

        Purchasing_Type = tools.CheckInt(Request.QueryString["Purchasing_Type"]);
        
        action = tools.CheckStr(Request.QueryString["action"]);
        if (action == "renew") {
            Public.CheckLogin("a56c96f7-fb31-4944-a248-45a8ad3c4398/a133f0cd-9a5e-4d02-ad94-9e0c0424d66d/87a8726b-5113-46ec-845c-6bd377935196/bfc31928-a7f1-45e0-bd3e-88a4268593ce");
            
            DBHelper = SQLHelperFactory.CreateSQLHelper();
            Purchasing_ID = tools.CheckInt(Request.QueryString["Purchasing_ID"]);
            string SqlList = "SELECT * FROM SCM_Purchasing WHERE Purchasing_ID=" + Purchasing_ID;
            SqlDataReader RdrList = null;
            try {
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read()) {
                    Purchasing_ID = tools.NullInt(RdrList["Purchasing_ID"]);
                    Purchasing_Type = tools.NullInt(RdrList["Purchasing_Type"]);
                    Purchasing_DepotID = tools.NullInt(RdrList["Purchasing_DepotID"]);
                    Purchasing_SupplierID = tools.NullInt(RdrList["Purchasing_SupplierID"]);
                    Purchasing_ProductCode = tools.NullStr(RdrList["Purchasing_ProductCode"]);
                    Purchasing_Price = tools.NullDbl(RdrList["Purchasing_Price"]);
                    Purchasing_Amount = Math.Abs(tools.NullInt(RdrList["Purchasing_Amount"]));
                    Purchasing_TotalPrice = tools.NullDbl(RdrList["Purchasing_TotalPrice"]);
                    Purchasing_BatchNumber = tools.NullStr(RdrList["Purchasing_BatchNumber"]);
                    Purchasing_Operator = tools.NullStr(RdrList["Purchasing_Operator"]);
                    Purchasing_Checkout = tools.NullInt(RdrList["Purchasing_Checkout"]);
                    Purchasing_Tradetime = tools.NullDate(RdrList["Purchasing_Tradetime"]);
                    Purchasing_Note = tools.NullStr(RdrList["Purchasing_Note"]);
                }
                else {
                    Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
                    Response.End();
                }
            }
            catch (Exception ex) { throw ex; }
            finally { if (RdrList != null) { RdrList.Close(); RdrList = null; } }

            action = "renew";
        }
        else {
            Public.CheckLogin("7cf163d1-e650-4f92-b5ad-69c9fde076d4/81979be3-1310-4ad3-b044-efe68dc4902e/6f566c26-20d6-4768-a180-492310e5b8cd/dc81bffa-f5f8-411c-b3f5-435702bb2df8");
            
            action = "new";
            Purchasing_ProductCode = tools.CheckStr(Request.QueryString["Product_Code"]);
            Purchasing_Amount = 1;
            Purchasing_Price = 0.00;
            Purchasing_TotalPrice = 0.00;
            Purchasing_Tradetime = DateTime.Today;
            Purchasing_Note = "";
        }
        
        switch (Purchasing_Type)
        {
            case 1:
                Purchasing_Type = 1;
                titleName = "进货单管理";
                break;
            case 2:
                Purchasing_Type = 2;
                titleName = "出库单管理";
                break;
            case 3:
                Purchasing_Type = 3;
                titleName = "退货单管理";
                break;
            case 4:
                Purchasing_Type = 4;
                titleName = "报损单管理";
                break;
            case 5:
                Purchasing_Type = 5;
                titleName = "盘点单管理";
                break;
        }
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>无标题文档</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
<script src="/Scripts/jquery.js" type="text/javascript"></script>

<link type="text/css" href="/Scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
<script src="/Scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
<script src="/Scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script>
<script src="/Scripts/jquery.zxxbox.3.0.js" type="text/javascript"></script>

<script type="text/javascript">
    function totalprice_count() {
        var price = $("#Purchasing_Price").val();
        var amount = $("#Purchasing_Amount").val();
        $("#Purchasing_TotalPrice").val(Math.round(price * amount * 100) / 100);
    }
    function ViewProductDetail(product_code) {
        $.ajax({
            url: encodeURI("purchasing_do.aspx?action=productdetail&product_code=" + product_code + "&timer=" + Math.random()),
            async: false,
            dataType: "html",
            success: function(data) {
                $("#div_productdetail").html(data);
            },
            error: function() {
                alert("Error Script");
            }
        });
    }

    $(document).ready(function() {
        $("#btn_opt").click(function() { $("#btn_opt").attr("href", "/scm/Purchasing/PurchasingProduct.aspx?timer=" + Math.random()); });
        $("#btn_opt").zxxbox({ height: 390, width: 600, title: '', bar: false, btnclose: false });
    });
    
</script>
</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title"><%=titleName%></td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="purchasing_do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">商品编号</td>
          <td class="cell_content"><input name="Purchasing_ProductCode" type="text" id="Purchasing_ProductCode" size="20" maxlength="50" value="<%=Purchasing_ProductCode%>" onblur="ViewProductDetail(this.value);" /> <a href="javascript:void(0);" id="btn_opt">选择商品</a></td>
        </tr>
        <tr>
          <td class="cell_title">商品信息</td>
          <td class="cell_content"><div id="div_productdetail"></div></td>
        </tr>
        <tr>
          <td class="cell_title">所属仓库</td>
          <td class="cell_content">
          <select id="Purchasing_DepotID" name="Purchasing_DepotID">
            <% =depot.DepotOption(Purchasing_DepotID)%>
          </select></td>
        </tr>
        <tr>
          <td class="cell_title">供应商</td>
          <td class="cell_content">
          <select id="Purchasing_SupplierID" name="Purchasing_SupplierID">
            <% =supplier.SupplierOption(Purchasing_SupplierID)%>
          </select></td>
        </tr>
        <tr>
          <td class="cell_title">单价</td>
          <td class="cell_content"><input name="Purchasing_Price" type="text" id="Purchasing_Price" size="10" maxlength="20" value="<%=Purchasing_Price%>" onblur="totalprice_count();" /></td>
        </tr>
        <tr>
          <td class="cell_title">数量</td>
          <td class="cell_content"><input name="Purchasing_Amount" type="text" id="Purchasing_Amount" size="10" maxlength="20" value="<%=Purchasing_Amount%>" onblur="totalprice_count();" /></td>
        </tr> 
        <tr>
          <td class="cell_title">总价</td>
          <td class="cell_content"><input name="Purchasing_TotalPrice" type="text" id="Purchasing_TotalPrice" size="20" readonly="readonly" value="<%=Purchasing_TotalPrice%>" /></td>
        </tr> 
        <tr>
          <td class="cell_title">批号</td>
          <td class="cell_content"><input name="Purchasing_BatchNumber" type="text" id="Purchasing_BatchNumber" size="50" maxlength="100" value="<%=Purchasing_BatchNumber%>" /></td>
        </tr>
        <tr>
          <td class="cell_title">结款状态</td>
          <td class="cell_content"><input name="Purchasing_Checkout" type="radio" id="Purchasing_Checkout1" value="1" <% =Public.CheckedRadio(Purchasing_Checkout.ToString(), "1")%>/>已结款 <input type="radio" name="Purchasing_Checkout" id="Purchasing_Checkout0" value="0" <% =Public.CheckedRadio(Purchasing_Checkout.ToString(), "0")%>/>未结款</td>
        </tr>
        <tr>
          <td class="cell_title">时间</td>
          <td class="cell_content"><input type="text" class="input_calendar" name="Purchasing_Tradetime" id="Purchasing_Tradetime" maxlength="10" readonly="readonly" value="<% =Purchasing_Tradetime.ToShortDateString()%>" />
              <script type="text/javascript">
                  $(document).ready(function() { $("#Purchasing_Tradetime").datepicker({ numberOfMonths: 2 }); });
              </script>
          </td>
        </tr>
        <tr>
          <td class="cell_title">备注</td>
          <td class="cell_content"><textarea name="Purchasing_Note" id="Purchasing_Note" cols="60" rows="6"><%=Purchasing_Note%></textarea></td>
        </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="action" name="action" value="<% =action%>" />
            <input type="hidden" id="Purchasing_ID" name="Purchasing_ID" value="<% =Purchasing_ID%>" />
            <input type="hidden" id="Purchasing_Type" name="Purchasing_Type" value="<% =Purchasing_Type%>" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location='purchasing_list.aspx';"/></td>
          </tr>
        </table>
        <%if (Purchasing_ProductCode.Length > 0) { Response.Write("<script type=\"text/javascript\">ViewProductDetail('" + Purchasing_ProductCode + "')</script>"); } %>
        </form>
        </td>
    </tr>
  </table>
</div>
</body>
</html>