<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

    int Purchasing_Type = 1;
    string titleName = "进货单管理", Product_Code;
    ITools tools;
    protected void Page_Load(object sender, EventArgs e)
    {
        tools = ToolsFactory.CreateTools();
        Purchasing_Type = tools.CheckInt(Request.QueryString["Purchasing_Type"]);
        Product_Code = tools.CheckStr(Request.Form["Product_Code"]);
        
        switch (Purchasing_Type)
        { 
            case 1:
                Public.CheckLogin("8b46f99a-d4ef-42aa-b593-795525ab4bab");
                
                Purchasing_Type = 1;
                titleName = "进货单管理";
                break;
            case 2:
                Public.CheckLogin("ad01bd92-647f-40d7-8985-845fc46d832f");
                
                Purchasing_Type = 2;
                titleName = "出库单管理";
                break;
            case 3:
                Public.CheckLogin("0ca87d96-8bfd-4f62-88bb-ef256b41cfdb");
                
                Purchasing_Type = 3;
                titleName = "退货单管理";
                break;
            case 4:
                Public.CheckLogin("70eb9566-dddf-425f-8074-8638a9089f08");
                
                Purchasing_Type = 4;
                titleName = "报损单管理";
                break;
            case 5:
                Public.CheckLogin("849805bd-ba21-4508-a803-9e0e5cc33b66");
                
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
<script src="/Scripts/common.js" type="text/javascript"></script>

<link type="text/css" href="/Scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
<script src="/Scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
<script src="/Scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script>
<script src="/Public/ckeditor/ckeditor.js" type="text/javascript"></script>

<link href="/Scripts/jqGrid/css/ui.jqgrid.css" rel="stylesheet" type="text/css" />
<script src="/Scripts/jqGrid/grid.locale-zh_CN.js" type="text/javascript"></script>
<script src="/Scripts/jqGrid/jquery.jqGrid.min.js" type="text/javascript"></script>
</head>
<body>

<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title"><% =titleName%></td>
    </tr>
    <tr>
      <td class="content_content">
        <form id="frmsearch" method="post" action="purchasing_list.aspx?Purchasing_Type=<%=Purchasing_Type%>">
        <table>
            <tr>
                <td>搜索 <input name="Product_Code" value="<% =Product_Code%>" /> <input name="save" type="submit" class="bt_orange" value="搜索" /></td>
            </tr>
        </table>
        </form>
        
        <table id="list"></table>
        <div id="pager"></div>
        <script type="text/javascript">
        jQuery("#list").jqGrid({
        url: 'purchasing_do.aspx?action=list&Purchasing_Type=<%=Purchasing_Type%>&Product_Code=<% =Server.UrlEncode(Request["Product_Code"])%>',
			datatype: "json",
            colNames: ['ID', '所属仓库', '供应商', '商品编码', '商品名称','规格','产地', '价格', '数量', '总价', '批号','时间', '操作人', '结款状态', "操作"],
            colModel: [
				{width:40,  name: 'Purchasing_ID', index: 'Purchasing_ID', align: 'center'},
				{width:70,  name: 'Purchasing_DepotID', index: 'Purchasing_DepotID', align: 'center'},
				{  name: 'Purchasing_SupplierID', index: 'Purchasing_SupplierID', align: 'center'},
				{width:80, align: 'center', name: 'Purchasing_ProductCode', index: 'Purchasing_ProductCode', align: 'center'},
				{ width: 100, name: 'Product_Name', index: 'Product_Name', align: 'center' },
				{ width: 55, name: 'Product_spec', index: 'Product_spec', align: 'center' },
				{ width: 55, name: 'Product_maker', index: 'Product_maker', align: 'center' },
				{width:55,  name: 'Purchasing_Price', index: 'Purchasing_Price', align: 'center'},
				{width:30, name: 'Purchasing_Amount', index: 'Purchasing_Amount', align: 'center'},
				{width:70,  name: 'Purchasing_TotalPrice', index: 'Purchasing_TotalPrice', align: 'center'},
				{width:80,  name: 'Purchasing_BatchNumber', index: 'Purchasing_BatchNumber', align: 'center'},
				{width:70,  name: 'Purchasing_Addtime', index: 'Purchasing_Addtime', align: 'center'},
				{width:50,  name: 'Purchasing_Operator', index: 'Purchasing_Operator', align: 'center'},
				{width:60,name: 'Purchasing_Checkout', index: 'Purchasing_Checkout', align: 'center'},
				{width:100,  name: 'Operate', index: 'Operate', align: 'center', sortable:false},
			],
            sortname: 'Purchasing_ID',
			sortorder: "desc",
			rowNum: GetrowNum(),
			rowList: GetrowList(), 
			pager: 'pager',
			multiselect: false,
			viewsortcols: [false,'horizontal',true],
			width: getTotalWidth() - 35,
			height: "100%"
        });
        </script>
        <div style="margin-top:5px;">
        
        <input type="button" id="Button1" class="bt_orange" value="导出全部单据" onclick="location.href='purchasing_do.aspx?action=export&nostock=0&purchasing_type=<%=Purchasing_Type%>&product_code=<% =Server.UrlEncode(Request["Product_Code"])%>'" /> 
        
        </div>
      </td>
    </tr>
  </table>
</div>
</body>
</html>