<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<script runat="server">

    string keyword = "";
    string defaultkey = "";
    private ITools tools;
    int shop_type = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("1392d14a-6746-4167-804a-d04a2f81d226");
        tools=ToolsFactory.CreateTools();
        keyword = tools.CheckStr(Request["keyword"]);
        shop_type = tools.CheckInt(Request["shop_type"]);
        if (keyword != "输入店铺名称、域名、店铺号进行搜索" && keyword != "")
        {
            keyword = keyword;
        }
        else
        {
            keyword = "输入店铺名称、域名、店铺号进行搜索";
        }
        if (keyword == "输入店铺名称、域名、店铺号进行搜索")
        {
            defaultkey = "";
        }
        else
        {
            defaultkey = keyword;
        }
        
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>店铺管理</title>
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
      <td class="content_title">店铺名称</td>
    </tr>
   <%-- <tr>
      <td class="content_content">
      <table width="100%" border="0" cellpadding="0" cellspacing="0">
      <tr><td valign="top" height="26" class="opt_foot">
      <table border="0" cellpadding="0" cellspacing="0">
      <tr>
      <td class="opt_gap">&nbsp;</td>
      <td class="<%if(shop_type==0){Response.Write("opt_cur");}else{Response.Write("opt_uncur");} %>">
      <%=Public.Page_Option("?shop_type=0&keyword=" + Server.UrlEncode(defaultkey), "全部")%>
      </td>
      <td class="opt_gap">&nbsp;</td>
      <td class="<%if(shop_type==1){Response.Write("opt_cur");}else{Response.Write("opt_uncur");} %>">
      <%=Public.Page_Option("?shop_type=1&keyword=" + Server.UrlEncode(defaultkey), "体验店铺")%>
      </td>
      <td class="opt_gap">&nbsp;</td>
      <td class="<%if(shop_type==2){Response.Write("opt_cur");}else{Response.Write("opt_uncur");} %>">
      <%=Public.Page_Option("?shop_type=2&keyword=" + Server.UrlEncode(defaultkey), "展示店铺")%>
      </td>
      <td class="opt_gap">&nbsp;</td>
      <td class="<%if(shop_type==3){Response.Write("opt_cur");}else{Response.Write("opt_uncur");} %>">
      <%=Public.Page_Option("?shop_type=3&keyword=" + Server.UrlEncode(defaultkey) + "", "销售店铺")%></td>

      </tr>--%>
      </table>
      </td></tr>
      <tr><td>
    <form action="Shop_list.aspx" method="post" name="frm_sch" id="frm_sch" >
    <table width="100%" border="0" cellspacing="0" cellpadding="5">
			<tr >
			<td><span class="left_nav">搜索</span> 
            
				<input type="text" name="keyword" size="50" onfocus="if(this.value=='输入店铺名称、域名、店铺号进行搜索'){this.value='';}"  id="keyword" value="<% =keyword %>" /> <input type="submit" name="btn_sch" class="btn_01" id="btn_sch" value="搜索" /></td>
			</tr>
		</table>
            </form>
    </td></tr>
    <tr>
      <td>
        <table id="list"></table>
        <div id="pager"></div>
            <script type="text/javascript">
                jQuery("#list").jqGrid({
                    url: 'shop_do.aspx?action=list&keyword=<%=Server.UrlEncode(defaultkey) %>&shop_type=<%=shop_type %>',
                    datatype: "json",
                    colNames: ['店铺名称', '供应商名称', '店铺域名', '店铺号', '店铺类型','店铺样式','店铺状态', '操作'],
                    colModel: [

				{ name: 'SupplierShopInfo.Shop_Name', index: 'SupplierShopInfo.Shop_Name', align: 'center' },
                { width: 100, name: 'SupplierShopInfo.Shop_SupplierID', index: 'SupplierShopInfo.Shop_SupplierID', align: 'center' },
				{ name: 'SupplierShopInfo.Shop_Domain', index: 'SupplierShopInfo.Shop_Domain', align: 'center' },
				{ width: 100, name: 'SupplierShopInfo.Shop_Code', index: 'SupplierShopInfo.Shop_Code', align: 'center' },
				{width: 80,  name: 'SupplierShopInfo.Shop_Type', index: 'SupplierShopInfo.Shop_Type', align: 'center' },
				{ width: 80, name: 'SupplierShopInfo.Shop_Css', index: 'SupplierShopInfo.Shop_Css', align: 'center' },
				{ width: 80, name: 'SupplierShopInfo.Shop_Status', index: 'SupplierShopInfo.Shop_Status', align: 'center' },
				{ width: 50, name: 'Operate', index: 'Operate', align: 'center', sortable: false },
			],
                    sortname: 'SupplierShopInfo.Shop_ID',
                    sortorder: "desc",
                    rowNum: GetrowNum(),
                    rowList: GetrowList(),
                    pager: 'pager',
                    multiselect: true,
                    viewsortcols: [true, 'horizontal', true],
                    width: getTotalWidth() - 35,
                    height: "100%"
                });
        </script>
       <%-- <div style="margin-top:5px;">
        <input type="button" id="Button3" class="bt_orange" value="设为体验店铺" onclick="location.href='shop_do.aspx?action=shopupgrade1&shop_id='+jQuery('#list').jqGrid('getGridParam','selarrrow');" /> 
        <input type="button" id="Button1" class="bt_orange" value="设为展示店铺" onclick="location.href='shop_do.aspx?action=shopupgrade2&shop_id='+jQuery('#list').jqGrid('getGridParam','selarrrow');" /> 
        <input type="button" id="Button2" class="bt_orange" value="设为销售店铺" onclick="location.href='shop_do.aspx?action=shopupgrade3&shop_id='+jQuery('#list').jqGrid('getGridParam','selarrrow');" /> 
        </div>--%>
      </td>
    </tr>
  </table>
  </td>
    </tr>
  </table>
</div>
</body>
</html>