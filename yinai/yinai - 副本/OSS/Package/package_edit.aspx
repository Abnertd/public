<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    
    Package myApp;
    ITools tools;
    
    string Package_Name;
    int Package_ID, Package_IsInsale, Package_StockAmount, Package_Weight, Package_Sort;
    DateTime Package_Addtime;
    double Package_Price;
    IList<PackageProductInfo> PPINFO;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("5666872b-2113-490b-a41f-a7a65083324a");
        myApp = new Package();
        tools = ToolsFactory.CreateTools();

        Session["PackageProductInfo"] = new List<PackageProductInfo>();

        Package_ID = tools.CheckInt(Request.QueryString["Package_ID"]);
        PackageInfo entity = myApp.GetPackageByID(Package_ID);
        if (entity == null) {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        else {
            Package_ID = entity.Package_ID;
            Package_Name = entity.Package_Name;
            Package_IsInsale = entity.Package_IsInsale;
            Package_StockAmount = entity.Package_StockAmount;
            Package_Weight = entity.Package_Weight;
            Package_Price = entity.Package_Price;
            Package_Sort = entity.Package_Sort;
            Package_Addtime = entity.Package_Addtime;
            PPINFO = entity.PackageProductInfos;
            
            if (PPINFO != null) { Session["PackageProductInfo"] = PPINFO; }
        }
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>无标题文档</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
<script src="/Scripts/jquery.js" type="text/javascript"></script>
<script type="text/javascript">
	function SelectProduct(){
		window.open ("selectproduct.aspx", "运营支撑系统", "height=560, width=600, toolbar=no, menubar=no, scrollbars=yes, resizable=no, location=no, status=no,top=100,left=300")
	}
	function product_del(product_id){
	    $.ajax({
	        url: encodeURI("package_do.aspx?action=product_del&product_id="+ product_id +"&timer="+Math.random()),
		    type: "get", 
		    global: false, 
		    async: false,
		    dataType: "html",
		    success:function(data){
			    $("#yhnr").html(data);
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
      <td class="content_title">捆绑销售修改</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="package_do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">名称</td>
          <td class="cell_content"><input name="Package_Name" type="text" id="Package_Name" size="50" maxlength="50" value="<% =Package_Name%>" />  <span class="t12_red">*</span></td>
        </tr>
        <tr>
          <td class="cell_title">排序</td>
          <td class="cell_content"><input name="Package_Sort" type="text" id="Package_Sort" size="10" maxlength="10" value="<% =Package_Sort%>" /></td>
        </tr>
        <tr style="display:none">
          <td class="cell_title">库存</td>
          <td class="cell_content"><input name="Package_StockAmount" type="text" id="Package_StockAmount" size="30" maxlength="10" value="<% =Package_StockAmount%>" /></td>
        </tr>
        <tr>
          <td class="cell_title">重量</td>
          <td class="cell_content"><input name="Package_Weight" type="text" id="Package_Weight" size="30" maxlength="10" value="<% =Package_Weight%>" /></td>
        </tr>
        <tr>
          <td class="cell_title">捆绑价格</td>
          <td class="cell_content"><input name="Package_Price" type="text" id="Package_Price" size="30" maxlength="10" value="<% =Package_Price%>" /></td>
        </tr>
        <tr>
          <td class="cell_title">上架</td>
          <td class="cell_content"><input name="Package_IsInsale" type="radio" value="1" <% =Public.CheckedRadio(Package_IsInsale.ToString(), "1")%> />是 <input type="radio" name="Package_IsInsale" value="0" <% =Public.CheckedRadio(Package_IsInsale.ToString(), "0")%>/>否</td>
        </tr>
        <tr>
          <td class="cell_title" valign="top">商品选择</td>
          <td class="cell_content"><div id="yhnr"><% =myApp.ShowProduct()%></div></td>
        </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="action" name="action" value="renew" />
            <input type="hidden" id="Package_ID" name="Package_ID" value="<% =Package_ID%>" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location='package_list.aspx';"/></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
  </table>
</div>
</body>
</html>