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
        Public.CheckLogin("0dd17a70-862d-4e57-9b45-897b98e8a858");
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
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">名称</td>
          <td class="cell_content"><% =Package_Name%></td>
        </tr>
        <tr>
          <td class="cell_title">捆绑价格</td>
          <td class="cell_content"><%=Public.DisplayCurrency(Package_Price)%></td>
        </tr>
        <tr>
          <td class="cell_title">上架</td>
          <td class="cell_content"><% if (Package_IsInsale == 1) { Response.Write("上架"); } else { Response.Write("下架"); }%></td>
        </tr>
        <tr>
          <td class="cell_title" valign="top">商品选择</td>
          <td class="cell_content"><div id="yhnr"><% =myApp.GetPackageProduct()%></div></td>
        </tr>
      </table>
        
        </td>
    </tr>
  </table>
</div>
</body>
</html>