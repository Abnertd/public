<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.B2C.BLL.Product" %>
<%@ Import Namespace="Glaer.Trade.Util.SQLHelper" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    
    int Purchasing_ID, Purchasing_Type, Purchasing_DepotID, Purchasing_SupplierID, Purchasing_Amount, Purchasing_Checkout;
    string Purchasing_ProductCode, Purchasing_BatchNumber, Purchasing_Operator, Purchasing_Note, action;
    double Purchasing_Price, Purchasing_TotalPrice;
    string Product_Spec, Product_Maker;
    DateTime Purchasing_Tradetime;

    string titleName = "进货单管理";
    
    private ITools tools;
    private ISQLHelper DBHelper;
    private IProduct product;

    private SCMDepot depot;
    private SCMSupplier supplier;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        
        
        tools = ToolsFactory.CreateTools();

        depot = new SCMDepot();
        supplier = new SCMSupplier();
        product = ProductFactory.CreateProduct();
        Product_Spec = "";
        Product_Maker = "";

        Purchasing_Type = tools.CheckInt(Request.QueryString["Purchasing_Type"]);
        

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
                    ProductInfo productinfo = product.GetProductByCode(Purchasing_ProductCode, Public.GetCurrentSite(), Public.GetUserPrivilege());
                    if (productinfo != null)
                    {
                        Product_Spec = productinfo.Product_Spec;
                        Product_Maker = productinfo.Product_Maker;
                    }
                }
                else {
                    Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
                    Response.End();
                }
            }
            catch (Exception ex) { throw ex; }
            finally { if (RdrList != null) { RdrList.Close(); RdrList = null; } }

            action = "renew";
        
        
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

<script type="text/javascript">
function totalprice_count(){
    var price = $("#Purchasing_Price").val();
    var amount = $("#Purchasing_Amount").val();
    $("#Purchasing_TotalPrice").val( Math.round(price * amount *100)/100);
}
function ViewProductDetail(product_code){
    $.ajax({
        url: encodeURI("purchasing_do.aspx?action=productdetail&product_code="+ product_code +"&timer="+ Math.random()), 
        async: false,
        dataType: "html",
		success: function(data){
            $("#div_productdetail").html(data);
		},
		error: function (){
			alert("Error Script");
		}
    });
}
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
          <td class="cell_title">所属仓库</td>
          <td class="cell_content">
          <%=depot.GetNameByID(Purchasing_DepotID) %>
          </td>
        </tr>
        <tr>
          <td class="cell_title">供应商</td>
          <td class="cell_content">
          <%=supplier.GetNameByID(Purchasing_SupplierID) %>
          </td>
        </tr>
        <tr>
          <td class="cell_title">商品编号</td>
          <td class="cell_content"><%=Purchasing_ProductCode%></td>
        </tr>
        <tr>
          <td class="cell_title">规格</td>
          <td class="cell_content"><%=Product_Spec%></td>
        </tr>
        <tr>
          <td class="cell_title">产地</td>
          <td class="cell_content"><%=Product_Maker%></td>
        </tr>
        <tr>
          <td class="cell_title">数量</td>
          <td class="cell_content"><%=Purchasing_Amount%></td>
        </tr> 
        <tr>
          <td class="cell_title">总价</td>
          <td class="cell_content"><%=Purchasing_TotalPrice%></td>
        </tr> 
        <tr>
          <td class="cell_title">批号</td>
          <td class="cell_content"><%=Purchasing_BatchNumber%></td>
        </tr>
        <tr>
          <td class="cell_title">结款状态</td>
          <td class="cell_content">
          <%
              if (Purchasing_Checkout == 1)
              {
                  Response.Write("已结算");
              }
              else
              {
                  Response.Write("未结算");
              } %>
          </td>
        </tr>
        <tr>
          <td class="cell_title">时间</td>
          <td class="cell_content"><% =Purchasing_Tradetime.ToShortDateString()%>
          </td>
        </tr>
        <tr>
          <td class="cell_title">备注</td>
          <td class="cell_content"><%=Purchasing_Note%></td>
        </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">

             <input name="button" type="button" class="bt_grey" id="button" value="返回" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="history.go(-1);"/></td>
          </tr>
        </table>

        </td>
    </tr>
  </table>
</div>
</body>
</html>